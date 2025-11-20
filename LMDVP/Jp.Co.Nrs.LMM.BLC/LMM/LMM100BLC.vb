' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM100BLC : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
'2015.10.02 他荷主対応START
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

'2015.10.02 他荷主対応END

''' <summary>
''' LMM100BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM100BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM100DAC = New LMM100DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 商品マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistZaiko(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "ExistZaiko", ds)

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック(荷主コードS・SS 編集可否判定用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistCustZaiko(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "ExistCustZaiko", ds)

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 商品マスタ削除/復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '商品マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteGoodsData", ds)

        If MyBase.GetResultCount() > 0 Then

            '商品明細マスタ削除/復活
            ds = MyBase.CallDAC(Me._Dac, "DeleteGoodsDtlData", ds)

        End If

        Return ds

    End Function

#End Region

#Region "単価一括変更処理"

    ''' <summary>
    ''' 単価マスタ存在チェック(単価一括変更用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkTankaMIkkatu(ByVal ds As DataSet) As DataSet

        '単価マスタ存在チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistTankaM", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ一括更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateIkkatu(ByVal ds As DataSet) As DataSet

        '商品マスタ一括更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateIkkatu", ds)

        Return ds

    End Function

    'START YANAI 要望番号372
    ''' <summary>
    ''' 商品マスタの荷主一括更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNinushi(ByVal ds As DataSet) As DataSet

        '商品マスタの荷主一括更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateNinushi", ds)

        Return ds

    End Function
    'END YANAI 要望番号372

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkZaiT(ByVal ds As DataSet) As DataSet

        '在庫マスタ存在チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistZaiT", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

    '2015.10.02 他荷主対応START
#Region "他荷主処理"

    ''' <summary>
    ''' 商品マスタ存在チェック(振替先に商品があるかのチェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistGoodsMTaninusi(ByVal ds As DataSet) As DataSet

        '(振替先)商品マスタ存在チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistGoodsMTaninusi", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ新規追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertTaninusiMgoods(ByVal ds As DataSet) As DataSet

        '商品Keyを新規採番する
        Dim brCd As String = ds.Tables("LMM100OUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        ds.Tables("LMM100OUT").Rows(0).Item("GOODS_CD_NRS_FURI") = num.GetAutoCode(NumberMasterUtility.NumberKbn.GOODS_CD_NRS, Me, brCd)

        '(振替先)商品マスタ新規追加
        ds = MyBase.CallDAC(Me._Dac, "InsertTaninusiMgoods", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 振替対象商品マスタの新規追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertTaninusiFuriGoods(ByVal ds As DataSet) As DataSet

        '振替対象商品マスタの新規追加
        ds = MyBase.CallDAC(Me._Dac, "InsertTaninusiFuriGoods", ds)

        Return ds

    End Function

#End Region
    '2015.10.02 他荷主対応END

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    '''商品マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>In句設定は2000件が上限のため、複数回にわたり検索処理を行う</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '商品マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        '商品明細マスタ検索用にDataSet編集
        Dim outDt As DataTable = ds.Tables("LMM100OUT")
        Dim dtlDt As DataTable = ds.Tables("LMM100_GOODS_DETAILS")
        Dim userBr As String = ds.Tables("LMM100IN").Rows(0).Item("USER_BR_CD").ToString()
        Dim rtnDs As DataSet = New LMM100DS()
        Dim rtnDt As DataTable = rtnDs.Tables("LMM100IN")
        Dim rtnDr As DataRow = Nothing
        Dim dtlMax As Integer = 0

        Dim max As Integer = outDt.Rows.Count - 1
        Dim maxShou As Integer = outDt.Rows.Count \ 2000
        Dim maxMod As Integer = outDt.Rows.Count Mod 2000 - 1
        If maxShou > 0 Then
            '2000件ずつIN句を設定する
            For i As Integer = 0 To maxShou - 1

                For j As Integer = 0 + (2000 * i) To 1999 + (2000 * i)

                    rtnDr = rtnDt.NewRow()

                    rtnDr.Item("USER_BR_CD") = userBr
                    rtnDr.Item("NRS_BR_CD") = outDt.Rows(j).Item("NRS_BR_CD")
                    rtnDr.Item("GOODS_CD_NRS") = outDt.Rows(j).Item("GOODS_CD_NRS")

                    rtnDt.Rows.Add(rtnDr)
                Next

                '商品明細マスタ検索
                rtnDs = MyBase.CallDAC(Me._Dac, "SelectDtlListData", rtnDs)

                '取得結果を返却用DataSetに設定
                dtlMax = rtnDs.Tables("LMM100_GOODS_DETAILS").Rows.Count - 1
                For k As Integer = 0 To dtlMax
                    dtlDt.ImportRow(rtnDs.Tables("LMM100_GOODS_DETAILS").Rows(k))
                Next

                '返却用テーブルの初期化
                rtnDt.Rows.Clear()

            Next
        End If
        For i As Integer = 0 + (2000 * maxShou) To maxMod + (2000 * maxShou)

            rtnDr = rtnDt.NewRow()

            rtnDr.Item("USER_BR_CD") = userBr
            rtnDr.Item("NRS_BR_CD") = outDt.Rows(i).Item("NRS_BR_CD")
            rtnDr.Item("GOODS_CD_NRS") = outDt.Rows(i).Item("GOODS_CD_NRS")

            rtnDt.Rows.Add(rtnDr)
        Next
        '商品明細マスタ検索
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDtlListData", rtnDs)

        '取得結果を返却用DataSetに設定
        dtlMax = rtnDs.Tables("LMM100_GOODS_DETAILS").Rows.Count - 1
        For k As Integer = 0 To dtlMax
            dtlDt.ImportRow(rtnDs.Tables("LMM100_GOODS_DETAILS").Rows(k))
        Next

        Return ds

    End Function

#End Region

#Region "保存処理"

#Region "チェック"

    ''' <summary>
    ''' 単価マスタチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkTankaM(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMM100OUT"

        '単価グループコードが空の場合、処理終了
        If String.IsNullOrEmpty(ds.Tables(tableNm).Rows(0).Item("UP_GP_CD_1").ToString()) Then
            Return ds
        End If

        '単価マスタ存在チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistTankaM", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '単価マスタ保管料(温度管理有り)チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistTankaMOndoKbn", ds)
        If ds.Tables(tableNm).Rows(0).Item("ONDO_KB").Equals("02") Then
            Select Case MyBase.GetResultCount()
                Case 1 '現在適用中の単価レコードの保管料が0円
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.SetMessage("W253")
                    '20151029 tsunehira add End
                    'MyBase.SetMessage("W139", New String() {"単価マスタの温度管理ありの保管料単価"})
                Case 2 '未来適用分の単価レコードの保管料が0円
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.SetMessage("W254")
                    '20151029 tsunehira add End
                    'MyBase.SetMessage("W136", New String() {"未来の適用開始日の保管料単価"})
            End Select

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistGoodsM(ByVal ds As DataSet) As DataSet

        '商品マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistGoodsM", ds)

    End Function

    ''' <summary>
    ''' 商品マスタ重複チェック(荷主コード、商品コード関連チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistGoodsMCust(ByVal ds As DataSet) As DataSet

        '商品マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistGoodsMCust", ds)

    End Function

    ''' <summary>
    ''' 混在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MixKiwariChk(ByVal ds As DataSet) As DataSet

        '混在チェック
        ds = MyBase.CallDAC(Me._Dac, "MixKiwariChk", ds)

        Return ds

    End Function

#If True Then   'ADD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
    ''' <summary>
    ''' 荷主商品マスタ取得(荷主コード、荷主商品コードで取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetGoodsMcust(ByVal ds As DataSet) As DataSet

        '荷主商品マスタ取得
        Return MyBase.CallDAC(Me._Dac, "GetGoodsMcust", ds)

    End Function
#End If

    ''' <summary>
    ''' 坪貸請求先コード差異チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function TuboChk(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMM100OUT").Rows(0)

        If dr.Item("CHK_CUST_CD_S").Equals(dr.Item("CUST_CD_S")) = False _
        OrElse dr.Item("CHK_CUST_CD_SS").Equals(dr.Item("CUST_CD_SS")) = False Then
            '坪貸請求先コード差異チェック
            ds = MyBase.CallDAC(Me._Dac, "TuboChk", ds)
        End If

        Return ds

    End Function

#End Region

#Region "新規登録/更新"

    ''' <summary>
    ''' 商品マスタ/明細 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '商品マスタ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertGoodsM", ds)

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM100_GOODS_DETAILS").Rows.Count = 0 Then
            Return ds
        End If

        '商品明細新規登録
        ds = (MyBase.CallDAC(Me._Dac, "InsertGoodsMDtl", ds))

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ/明細 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        '商品マスタ更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateGoodsM", ds)

        If MyBase.GetResultCount() > 0 Then

            '商品明細物理削除
            ds = (MyBase.CallDAC(Me._Dac, "DeleteGoodsMDtl", ds))

            '更新登録する明細データがない場合、処理終了
            If ds.Tables("LMM100_GOODS_DETAILS").Rows.Count = 0 Then
                Return ds
            End If

            '商品明細新規登録
            ds = (MyBase.CallDAC(Me._Dac, "InsertGoodsMDtl", ds))

        End If

        Return ds

    End Function

#End Region

#End Region

#Region "危険品情報確認処理"
    ''' <summary>
    ''' 危険品情報確認処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ConfirmKikenGoods(ByVal ds As DataSet) As DataSet

        '商品マスタの更新処理
        ds = MyBase.CallDAC(Me._Dac, "ConfirmKikenGoods", ds)

        Return ds

    End Function
#End Region

#Region "容積一括更新処理"
    ''' <summary>
    ''' 容積一括更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateGoodsVolume(ByVal ds As DataSet) As DataSet

        '商品マスタの更新処理
        ds = MyBase.CallDAC(Me._Dac, "UpdateGoodsVolume", ds)

        Return ds

    End Function
#End Region

#Region "X-Track"

    ''' <summary>
    ''' X-Track用存在チェック（SKU）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function XTrackChk_SKU(ByVal ds As DataSet) As DataSet

        'X-Track用存在チェック（SKU）
        ds = MyBase.CallDAC(Me._Dac, "XTrackChk_SKU", ds)

        'レコードが取得できなければエラー
        If MyBase.GetResultCount() = 0 Then
            MyBase.SetMessage("E326", New String() {"X-Track取込み(SKU)", "X-Track"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' X-Track用存在チェック（原産国）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function XTrackChk_Gensan(ByVal ds As DataSet) As DataSet

        'X-Track用存在チェック（原産国）
        ds = MyBase.CallDAC(Me._Dac, "XTrackChk_Gensan", ds)

        'レコードが取得できなければエラー
        If MyBase.GetResultCount() = 0 Then
            MyBase.SetMessage("E326", New String() {"X-Track取込み(原産国)", "X-Track"})
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
