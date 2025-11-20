' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN080BLF : 欠品警告
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMN060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN080BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMN080BLC = New LMN080BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function Kensaku(ByVal ds As DataSet) As DataSet

        '倉庫コード、今回引当出荷オーダー数取得
        ds = MyBase.CallBLC(Me._Blc, "GetSOKO_CD_LIST", ds)

        '取得件数チェック（0件の場合エラー）
        If MyBase.GetResultCount = 0 Then
            'メッセージ設定
            MyBase.SetMessage("E024")
            Return ds
        End If

        '出荷予定品目数取得
        ds = MyBase.CallBLC(Me._Blc, "GetPLAN_HINMOKU_NB", ds)

        '欠品品目数、欠品危惧品目数取得
        ds = GetKEPPIN_NB(ds)

        '今回引当数取得
        ds = GetHIKIATE_NB(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 欠品品目数、欠品危惧品目数取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKEPPIN_NB(ByVal ds As DataSet) As DataSet

        '倉庫コードごとに取得
        'IN情報テーブル名取得
        Dim inName As String = "LMN080OUT_L"
        '倉庫毎の欠品品目数を取得
        Dim sokoNum As Integer = ds.Tables(inName).Rows.Count
        If sokoNum > 0 Then
            For i As Integer = 0 To sokoNum - 1

                '欠品商品コードリスト
                Dim keppinList As New ArrayList
                '欠品危惧商品コードリスト
                Dim preKeppinList As New ArrayList

                '欠品チェック対象明細データを取得
                'IN情報格納用データセット
                Dim inDs As DataSet = New LMN080DS()
                'IN情報を設定
                Dim inDr As DataRow = inDs.Tables(inName).NewRow()
                inDr("SCM_CUST_CD") = ds.Tables("LMN080IN").Rows(0).Item("SCM_CUST_CD").ToString()
                inDr("SOKO_CD") = ds.Tables(inName).Rows(i).Item("SOKO_CD").ToString()
                inDs.Tables(inName).Rows.Add(inDr)
                '明細データ取得
                inDs = MyBase.CallBLC(Me._Blc, "GetLMN080OUT_M", inDs)

                '明細テーブル名取得
                Dim meiName As String = "LMN080OUT_M"
                '明細データ一件毎に欠品数取得
                Dim meiNum As Integer = inDs.Tables(meiName).Rows.Count
                If meiNum > 0 Then
                    For j As Integer = 0 To meiNum - 1

                        '欠品チェックデータセット作成
                        Dim lmn810inName As String = "LMN810IN"
                        Dim lmn810ds As DataSet = New LMN810DS

                        '営業所接続情報設定
                        Dim brName As String = "BR_CD_LIST"
                        'フィルタ用営業所コード取得
                        Dim BrCd As String = ds.Tables(inName).Rows(i).Item("BR_CD").ToString()
                        '営業所接続情報抽出
                        Dim brDr As DataRow() = ds.Tables(brName).Select(String.Concat("BR_CD = '", BrCd, "'"))
                        lmn810ds.Tables(brName).ImportRow(brDr(0))

                        '欠品チェックパラメータ設定
                        Dim lmn810dr As DataRow = lmn810ds.Tables(lmn810inName).NewRow()
                        lmn810dr.Item("SCM_CUST_CD") = ds.Tables("LMN080IN").Rows(0).Item("SCM_CUST_CD").ToString()
                        lmn810dr.Item("LMS_CUST_CD") = brDr(0).Item("LMS_CUST_CD").ToString()
                        lmn810dr.Item("BR_CD") = ds.Tables(inName).Rows(i).Item("BR_CD").ToString()
                        lmn810dr.Item("SOKO_CD") = ds.Tables(inName).Rows(i).Item("SOKO_CD").ToString()
                        lmn810dr.Item("GOODS_CD_CUST") = inDs.Tables(meiName).Rows(j).Item("GOODS_CD_CUST").ToString()
                        lmn810dr.Item("OUTKA_DATE") = inDs.Tables(meiName).Rows(j).Item("OUTKA_DATE").ToString()
                        lmn810ds.Tables(lmn810inName).Rows.Add(lmn810dr)

                        'LMN810呼び出し
                        lmn810ds = MyBase.CallBLC(New LMN810BLC, "ChkKeppin", lmn810ds)

                        '欠品チェック
                        Dim outName As String = "LMN810OUT"
                        Dim lmn810out As DataTable = lmn810ds.Tables(outName)

                        '欠品の場合、欠品商品コードリストに設定
                        If Convert.ToInt32(lmn810out.Rows(0).Item("KEPPIN_NB")) > 0 Then

                            '欠品商品コード取得
                            Dim keppinGoodsCd As String = lmn810out.Rows(0).Item("GOODS_CD_CUST").ToString()
                            '存在フラグ(FALSE:同一商品コード無し TRUE:同一商品コード有り)
                            Dim existFlg As Boolean = False
                            '同一の商品コードの存在チェック
                            If keppinList.Count = 0 Then
                                existFlg = False
                            Else
                                For k As Integer = 0 To keppinList.Count - 1
                                    If keppinList(k).ToString() = keppinGoodsCd Then
                                        existFlg = True
                                        Exit For
                                    End If
                                Next
                            End If

                            '存在フラグ判定(FALSEの場合欠品商品コードリストに追加)
                            If existFlg = False Then
                                keppinList.Add(keppinGoodsCd)
                            End If

                            'リターンデータセットの明細(LMN080OUT_M)に設定
                            Dim keppinDr As DataRow = ds.Tables(meiName).NewRow()
                            keppinDr.Item("SOKO_CD") = inDs.Tables(meiName).Rows(j).Item("SOKO_CD").ToString()
                            keppinDr.Item("CUST_NM") = inDs.Tables(meiName).Rows(j).Item("CUST_NM").ToString()
                            keppinDr.Item("GOODS_CD_CUST") = inDs.Tables(meiName).Rows(j).Item("GOODS_CD_CUST").ToString()
                            keppinDr.Item("GOODS_NM") = inDs.Tables(meiName).Rows(j).Item("GOODS_NM").ToString()
                            keppinDr.Item("OUTKA_DATE") = inDs.Tables(meiName).Rows(j).Item("OUTKA_DATE").ToString()
                            keppinDr.Item("KEPPIN_NB") = lmn810out.Rows(0).Item("KEPPIN_NB").ToString()
                            keppinDr.Item("PLAN_ZAIKO_NB") = lmn810out.Rows(0).Item("PLAN_ZAIKO_NB").ToString()
                            keppinDr.Item("DETAIL_NB") = inDs.Tables(meiName).Rows(j).Item("DETAIL_NB").ToString()
                            keppinDr.Item("DEST_NM") = inDs.Tables(meiName).Rows(j).Item("DEST_NM").ToString()
                            keppinDr.Item("CUST_ORD_NO_L") = inDs.Tables(meiName).Rows(j).Item("CUST_ORD_NO_L").ToString()
                            ds.Tables(meiName).Rows.Add(keppinDr)

                        Else  '欠品でない場合、欠品危惧チェック
                            '欠品危惧チェック明細データ格納用データセット
                            Dim pkDs As DataSet = New LMN080DS()
                            Dim pkDr As DataRow = pkDs.Tables(meiName).NewRow()
                            pkDr.Item("SCM_CUST_CD") = ds.Tables("LMN080IN").Rows(0).Item("SCM_CUST_CD").ToString()
                            pkDr.Item("GOODS_CD_CUST") = inDs.Tables(meiName).Rows(j).Item("GOODS_CD_CUST").ToString()
                            pkDr.Item("SOKO_CD") = ds.Tables(inName).Rows(i).Item("SOKO_CD").ToString()
                            pkDs.Tables(meiName).Rows.Add(pkDr)

                            '欠品危惧チェック
                            pkDs = MyBase.CallBLC(Me._Blc, "CheckPreKeppin", pkDs)

                            '検索結果が1件以上存在する場合、欠品危惧品目として処理
                            If MyBase.GetResultCount > 0 Then

                                '欠品危惧商品コード取得
                                Dim preKeppinGoodsCd As String = lmn810out.Rows(0).Item("GOODS_CD_CUST").ToString()
                                '存在フラグ(FALSE:同一商品コード無し TRUE:同一商品コード有り)
                                Dim preExistFlg As Boolean = False
                                '同一の商品コードの存在チェック
                                If preKeppinList.Count = 0 Then
                                    preExistFlg = False
                                Else
                                    For l As Integer = 0 To preKeppinList.Count - 1
                                        If preKeppinList(l).ToString() = preKeppinGoodsCd Then
                                            preExistFlg = True
                                            Exit For
                                        End If
                                    Next
                                End If

                                '存在フラグ判定(FALSEの場合欠品危惧商品コードリストに追加)
                                If preExistFlg = False Then
                                    preKeppinList.Add(preKeppinGoodsCd)
                                End If

                                'リターンデータセットの明細(LMN080OUT_M)に設定
                                Dim prekeppinDr As DataRow = ds.Tables(meiName).NewRow()
                                prekeppinDr.Item("SOKO_CD") = inDs.Tables(meiName).Rows(j).Item("SOKO_CD").ToString()
                                prekeppinDr.Item("CUST_NM") = inDs.Tables(meiName).Rows(j).Item("CUST_NM").ToString()
                                prekeppinDr.Item("GOODS_CD_CUST") = inDs.Tables(meiName).Rows(j).Item("GOODS_CD_CUST").ToString()
                                prekeppinDr.Item("GOODS_NM") = inDs.Tables(meiName).Rows(j).Item("GOODS_NM").ToString()
                                prekeppinDr.Item("OUTKA_DATE") = inDs.Tables(meiName).Rows(j).Item("OUTKA_DATE").ToString()
                                prekeppinDr.Item("KEPPIN_NB") = lmn810out.Rows(0).Item("KEPPIN_NB").ToString()
                                prekeppinDr.Item("PLAN_ZAIKO_NB") = lmn810out.Rows(0).Item("PLAN_ZAIKO_NB").ToString()
                                prekeppinDr.Item("DETAIL_NB") = inDs.Tables(meiName).Rows(j).Item("DETAIL_NB").ToString()
                                prekeppinDr.Item("DEST_NM") = inDs.Tables(meiName).Rows(j).Item("DEST_NM").ToString()
                                prekeppinDr.Item("CUST_ORD_NO_L") = inDs.Tables(meiName).Rows(j).Item("CUST_ORD_NO_L").ToString()
                                ds.Tables(meiName).Rows.Add(prekeppinDr)

                            End If '欠品危惧品目処理

                        End If '欠品の場合

                    Next '明細ループ

                    '欠品品目数取得
                    ds.Tables(inName).Rows(i).Item("KEPPIN_NB") = keppinList.Count.ToString()
                    '欠品危惧品目数取得
                    ds.Tables(inName).Rows(i).Item("PRE_KEPPIN_NB") = preKeppinList.Count.ToString()

                End If '明細0件チェック

            Next '倉庫ループ

        End If '倉庫0件チェック

        Return ds

    End Function

    ''' <summary>
    ''' 今回引当数取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHIKIATE_NB(ByVal ds As DataSet) As DataSet

        '明細データテーブル名
        Dim meiTable As String = "LMN080OUT_M"
        Dim meiDt As DataTable = ds.Tables(meiTable)
        '明細データ一件ずつ今回引当数を取得
        Dim meiNum As Integer = meiDt.Rows.Count
        If meiNum > 0 Then
            For i As Integer = 0 To meiNum - 1

                '今回引当数
                Dim hikiateNb As Integer = 0
                '抽出用条件
                Dim GoodsCd As String = meiDt.Rows(i).Item("GOODS_CD_CUST").ToString()
                Dim OutkaDate As String = meiDt.Rows(i).Item("OUTKA_DATE").ToString()
                Dim filter As String = String.Concat("GOODS_CD_CUST = '", GoodsCd, "' AND OUTKA_DATE = '", OutkaDate, "'")

                '明細データ格納用データテーブル
                Dim dr As DataRow() = meiDt.Select(filter)
                '今回引当数取得
                Dim drNum As Integer = dr.Length
                If drNum > 0 Then
                    For j As Integer = 0 To drNum - 1
                        hikiateNb = hikiateNb + Convert.ToInt32(dr(j).Item("DETAIL_NB"))
                    Next
                End If

                '今回引当数設定
                meiDt.Rows(i).Item("HIKIATE_NB") = hikiateNb.ToString()

            Next

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
