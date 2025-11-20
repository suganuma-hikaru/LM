' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM100BLF : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM100BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM100BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Private Const LMM500DS As String = "LMM500DS"
    Private Const LMM510DS As String = "LMM510DS"
    Private Const LMM500IN As String = "LMM500IN"
    Private Const LMM510IN As String = "LMM510IN"
    Private Const LMM500NM As String = "商品マスタ・請求関連印刷"
    Private Const LMM510NM As String = "商品マスタ一覧表"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM100BLC = New LMM100BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''商品マスタ編集時チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "HaitaChk", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        '在庫データ存在チェック
        ds = MyBase.CallBLC(Me._Blc, "ExistZaiko", ds)

        '在庫データ存在チェック(荷主コードS・SS 編集可否判定用)
        ds = MyBase.CallBLC(Me._Blc, "ExistCustZaiko", ds)

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 商品マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '在庫データ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ChkZaiT", ds)
        If MyBase.IsMessageExist() Then
            Exit Sub
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub

#End Region

#Region "単価一括変更処理"

    ''' <summary>
    ''' 単価一括変更処理(商品マスタ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateIkkatu(ByVal ds As DataSet) As DataSet

        '単価マスタ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ChkTankaMIkkatu", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateIkkatu", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

    'START YANAI 要望番号372
#Region "荷主一括変更処理"

    ''' <summary>
    ''' 荷主一括変更処理(商品マスタ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNinushi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMM100IN").Rows.Count - 1
        Dim rtnDs As DataSet = Nothing

        For i As Integer = 0 To max

            '値のクリア
            inTbl = setDs.Tables("LMM100IN")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMM100IN").Rows(i))

            '在庫データ存在チェック(荷主コードS・SS 編集可否判定用)
            rtnDs = MyBase.CallBLC(Me._Blc, "ExistCustZaiko", setDs)

            If rtnDs.Tables("LMM100CUST_ZAIKO").Rows().Count() > 0 Then
                MyBase.SetMessageStore("00" _
                                   , "E237" _
                                   , New String() {"在庫のある商品"} _
                                   , ds.Tables("LMM100IN").Rows(i).Item("RECORD_NO").ToString() _
                                   , "商品コード" _
                                   , ds.Tables("LMM100IN").Rows(i).Item("GOODS_CD_CUST").ToString())
            End If
        Next
        If MyBase.IsMessageStoreExist Then
            Return ds
        End If

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMM100IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMM100IN").Rows(i))

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "UpdateNinushi", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region
    'END YANAI 要望番号372

    '2015.10.02 他荷主対応START
#Region "他荷主処理"

    ''' <summary>
    ''' 他荷主処理(商品マスタ新規追加・振替対象商品マスタ新規追加)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertTaninusi(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMM100OUT").Rows.Count - 1
        Dim rtnDs As DataSet = Nothing
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                Dim rtnResult As Boolean = True

                '値のクリア
                inTbl = setDs.Tables("LMM100OUT")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMM100OUT").Rows(i))

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "ExistGoodsMTaninusi", setDs)

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "InsertTaninusiMgoods", setDs)

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "InsertTaninusiFuriGoods", setDs)

                ''エラーがあるかを判定
                'rtnResult = Not MyBase.IsMessageExist()

                'エラーがあるかを判定
                rowNo = Convert.ToInt32(ds.Tables("LMM100OUT").Rows(i).Item("RECORD_NO"))

                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    '1件でもエラーがある場合は処理を抜けて終了
                    rtnResult = False
                    Continue For
                End If

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region
    '2015.10.02 他荷主対応END

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 保存処理(商品マスタ、商品明細マスタ新規登録/更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '保存時共通チェック
            If Me.ChkSaveCommon(ds) = False Then
                Return ds
            End If

            '商品Keyの取得
            If Me.SetGoodsKeyNo(ds) = True Then

                '新規登録時チェックを行う
                If ChkInsertData(ds) = False Then
                    Return ds
                End If

                '新規登録処理を行う
                ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            Else

                '更新時チェックを行う
                If ChkUpdateData(ds) = False Then
                    Return ds
                End If

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", ds)

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理の場合、商品KEYを採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>TRUE:新規登録、FALSE:更新登録</returns>
    ''' <remarks></remarks>
    Private Function SetGoodsKeyNo(ByVal ds As DataSet, Optional ByVal CHK_FLG As String = "") As Boolean

        Dim dtGoods As DataTable = ds.Tables("LMM100OUT")
        Dim dtGoodsDtl As DataTable = ds.Tables("LMM100_GOODS_DETAILS")

        If String.IsNullOrEmpty(dtGoods.Rows(0).Item("GOODS_CD_NRS").ToString()) = False Then
            Return False
        End If

        If String.Empty.Equals(CHK_FLG) = False Then
            Return True
        End If
        '商品Keyを新規採番する
        Dim brCd As String = dtGoods.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim goodsKey As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.GOODS_CD_NRS, Me, brCd)

        '新規登録内容を設定する
        dtGoods.Rows(0).Item("GOODS_CD_NRS") = goodsKey
        Dim max As Integer = dtGoodsDtl.Rows.Count - 1
        For i As Integer = 0 To max
            dtGoodsDtl.Rows(i).Item("GOODS_CD_NRS") = goodsKey
        Next

        Return True

    End Function

    ''' <summary>
    ''' 新規登録時チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkInsertData(ByVal ds As DataSet) As Boolean

        '商品マスタ存在チェック
        ds = MyBase.CallBLC(Me._Blc, "ExistGoodsM", ds)
        If MyBase.IsMessageExist() Then
            Return False
        End If

        Return True

    End Function

#If True Then   'ADD 2020/02/27 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)

    ''' <summary>
    ''' 荷主商品コード、入数Save前チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsCustData(ByVal ds As DataSet) As Boolean

        Dim syori_FLG As String = LMConst.FLG.OFF
        'M_EDI_CUST存在チェック
        If ds.Tables("LMM100SAVE_CHK").Rows.Count > 0 Then
            If ds.Tables("LMM100SAVE_CHK").Rows(0).Item("EDI_CUST_FLG").ToString.Trim = "1" Then
                '商品マスタメンテ画面で保存押下時に、EDI対象荷主(M_EDI_CUSTに荷主コードが存在)の場合
                If ds.Tables("LMM100SAVE_CHK").Rows(0).Item("CUST_DETAILS_9ZFLG").ToString.Trim = "0" Then
                    Dim strSql As String = String.Concat("PKG_NB <> ", ds.Tables("LMM100OUT").Rows(0).Item("PKG_NB").ToString.Trim)
                    Dim chkDr As DataRow() = ds.Tables("LMM100GoodsCust").Select(strSql)
                    If chkDr.Length > 0 Then
                        '既に同じ商品コードで入数が異なる商品が登録済みの場合はエラーとする
                        'ただし新たな荷主用途区分「同一商品コード入数違い許可」を追加し、「1」が設定されている場合はエラーとしない
                        MyBase.SetMessage("E057")

                        syori_FLG = LMConst.FLG.ON
                    End If
                End If

            End If

        End If

        If syori_FLG = LMConst.FLG.OFF Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 新規登録時チェック(荷主商品コード)処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function GET_GOODSM_CUST(ByVal ds As DataSet) As DataSet

        '商品マスタ(荷主商品コード)取得
        ds = MyBase.CallBLC(Me._Blc, "GetGoodsMcust", ds)

        Return ds

    End Function

#End If

    ''' <summary>
    ''' 更新時チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkUpdateData(ByVal ds As DataSet) As Boolean

        Dim tableNm As String = "LMM100OUT"

        '坪貸し請求先コード差異チェック
        If Convert.ToInt32(ds.Tables(tableNm).Rows(0).Item("WARNING_FLG")) < 5 Then
            ds = MyBase.CallBLC(Me._Blc, "TuboChk", ds)
            If MyBase.IsMessageExist() Then
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時共通チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkSaveCommon(ByVal ds As DataSet) As Boolean

        Dim tableNm As String = "LMM100OUT"

        '単価マスタチェック
        ds = MyBase.CallBLC(Me._Blc, "ChkTankaM", ds)
        If MyBase.IsMessageExist() Then
            Return False
        End If

#If True Then   'ADD 2020/02/27 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
        '商品Keyの取得
        If Me.SetGoodsKeyNo(ds, "NG") = True Then
            '新規登録時チェックを行う
            '荷主商品コード・入り数チェックを行う
            If ChkGoodsCustData(ds) = False Then
                Return False
            End If

        Else
            '編集時、入数変更可の得
            If ds.Tables("LMM100SAVE_CHK").Rows.Count > 0 Then
                If ds.Tables("LMM100SAVE_CHK").Rows(0).Item("IRISU_EDIT_FLG").ToString.Trim = "1" Then
                    '荷主商品コード・入り数チェックを行う
                    If ChkGoodsCustData(ds) = False Then
                        Return False
                    End If

                End If
            End If
        End If

#End If

        '********** ワーニングチェック **********
        '重複チェック
        If Convert.ToInt32(ds.Tables(tableNm).Rows(0).Item("WARNING_FLG")) < 3 Then
            ds = MyBase.CallBLC(Me._Blc, "ExistGoodsMCust", ds)
            If MyBase.IsMessageExist() Then
                Return False
            End If
        End If

        '混在チェック
        If Convert.ToInt32(ds.Tables(tableNm).Rows(0).Item("WARNING_FLG")) < 4 Then
            ds = MyBase.CallBLC(Me._Blc, "MixKiwariChk", ds)
            If MyBase.IsMessageExist() Then
                Return False
            End If
        End If
        '***************************************+

        Return True

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = String.Empty
        Dim rptNm As String = String.Empty
        Dim prtBlc As Base.BLC.LMBaseBLC

        'データセット判定処理
        Select Case ds.DataSetName

            Case LMM100BLF.LMM500DS
                tableNm = LMM100BLF.LMM500IN
                rptNm = LMM100BLF.LMM500NM

            Case LMM100BLF.LMM510DS
                tableNm = LMM100BLF.LMM510IN
                rptNm = LMM100BLF.LMM510NM

            Case Else
                MyBase.SetMessage("E033")
                Return ds

        End Select

        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'BLC判定処理
        prtBlc = getBLC(tableNm)
        If prtBlc Is Nothing Then
            MyBase.SetMessage("E175", New String() {rptNm})
            Return ds
        End If

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            '条件の設定
            setDt.Clear()
            setDt.ImportRow(dt.Rows(i))

            setDs.Merge(New RdPrevInfoDS)

            '印刷処理
            setDs = MyBase.CallBLC(prtBlc, "DoPrint", setDs)

            rdPrevDt.Merge(setDs.Tables(LMConst.RD))

        Next

        setDs.Tables(LMConst.RD).Clear()
        setDs.Tables(LMConst.RD).Merge(rdPrevDt)
        'ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return setDs 'ds

    End Function

#End Region

#Region "BLC判定処理"

    ''' <summary>
    ''' BLC設定処理
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBLC(ByVal rptId As String) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        '帳票ID
        Select Case rptId

            '商品マスタ・請求関係印刷
            Case LMM100BLF.LMM500IN

                Dim blc500 As LMM500BLC = New LMM500BLC
                setBlc = blc500

                '商品マスタ一覧表
            Case LMM100BLF.LMM510IN

                Dim blc510 As LMM510BLC = New LMM510BLC
                setBlc = blc510

            Case Else
                setBlc = Nothing

        End Select


        Return setBlc

    End Function


#End Region

#Region "危険品確認処理"

    ''' <summary>
    ''' 危険品確認処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ConfirmKikenGoods(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMM100IN").Rows.Count - 1
        Dim rtnDs As DataSet = Nothing
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMM100IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMM100IN").Rows(i))

                Dim rtnResult As Boolean = True

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "ConfirmKikenGoods", setDs)

                'エラーがあるかを判定
                rowNo = Convert.ToInt32(ds.Tables("LMM100IN").Rows(i).Item("RECORD_NO"))

                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    '1件でもエラーがある場合は処理を抜けて終了
                    rtnResult = False
                    Continue For
                End If

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region

#Region "容積一括更新"

    ''' <summary>
    ''' 容積一括更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateGoodsVolume(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMM100OUT").Rows.Count - 1
        Dim rtnDs As DataSet = Nothing
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMM100OUT")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMM100OUT").Rows(i))

                Dim rtnResult As Boolean = True

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "UpdateGoodsVolume", setDs)

                'エラーがあるかを判定
                rowNo = Convert.ToInt32(ds.Tables("LMM100OUT").Rows(i).Item("RECORD_NO"))

                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    '1件でもエラーがある場合は処理を抜けて終了
                    rtnResult = False
                    Continue For
                End If

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

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
        Return MyBase.CallBLC(Me._Blc, "XTrackChk_SKU", ds)

    End Function

    ''' <summary>
    ''' X-Track用存在チェック（原産国）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function XTrackChk_Gensan(ByVal ds As DataSet) As DataSet

        'X-Track用存在チェック（原産国）
        Return MyBase.CallBLC(Me._Blc, "XTrackChk_Gensan", ds)

    End Function

#End Region

#End Region

End Class