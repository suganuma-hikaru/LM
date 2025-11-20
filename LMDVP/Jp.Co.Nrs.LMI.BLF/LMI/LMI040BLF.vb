' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI040  : 請求データ編集 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI040BLC = New LMI040BLC()

    Private Const LMG550PRT_KAGAMI As String = "01"
    Private Const LMG550PRT_SHUKEI As String = "02"
    Private Const LMG550PRT_SHUKEIKEIRI As String = "03"
#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            If ds Is Nothing Then
                '0件の時はメッセージを設定していないため、ここで判定を行う
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        'データ重複検索
        ds = MyBase.CallBLC(Me._Blc, "SelectInsertData", ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '新規登録処理
        Return Me.SaveComData(ds, "InsertSaveAction")

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "UpdateSaveAction")

    End Function

    ''' <summary>
    ''' 合算更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GassanSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "GassanSaveAction")

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        'データ削除処理
        ds = Me.SimpleScopeStartEnd(ds, "DeleteSaveAction")

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理共通
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveComData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '保存処理
        ds = Me.SimpleScopeStartEnd(ds, actionId)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '印刷処理
        Call Me.PrintData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 複写時の合算処理判定のための検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectCopyData(ByVal ds As DataSet) As DataSet

        'データ重複検索
        ds = MyBase.CallBLC(Me._Blc, "SelectCopyData", ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI040IN"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printType As String = dt.Rows(0).Item("PRINT_KB").ToString()

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        For i As Integer = 0 To max

            '印刷処理
            setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

            'プレビュー情報を設定
            If setRptDt Is Nothing = False Then
                cnt = setRptDt.Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(setRptDt.Rows(j))
                Next
            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        Select Case printType

            Case "01" 'デュポン請求データ入力（登録リスト）

                prtBlc = New Com.Base.BaseBLC() {New LMG560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMG560InData(ds)}

            Case "02" 'デュポン請求鑑

                prtBlc = New Com.Base.BaseBLC() {New LMG550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMG550InData(ds, LMG550PRT_KAGAMI)}

            Case "03" 'デュポン請求集計表
                prtBlc = New Com.Base.BaseBLC() {New LMG550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMG550InData(ds, LMG550PRT_SHUKEI)}

            Case "04" 'デュポン請求集計表経理用
                prtBlc = New Com.Base.BaseBLC() {New LMG550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMG550InData(ds, LMG550PRT_SHUKEIKEIRI)}

        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If
        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMG550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMG550InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMG550InData(ds, New LMG550DS(), "LMG550IN", printType)

    End Function

    ''' <summary>
    ''' LMG560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMG560InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMG560InData(ds, New LMG560DS(), "LMG560IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMG550InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, ByVal prtType As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMI040IN").Rows(0)
        dr.Item("PRT_TYPE") = prtType
        dr.Item("INV_DATE") = setDr.Item("SEKY_YM").ToString()
        dr.Item("DEPERT") = setDr.Item("DEPART").ToString()
        If LMG550PRT_KAGAMI.Equals(prtType) = True Then
            Select Case setDr.Item("PRINT_TYPE1").ToString()
                Case "01" '通常
                    dr.Item("KAGAMI_PRC") = "00"
                Case "02" 'ミスク
                    dr.Item("KAGAMI_PRC") = "01"
                Case "03" '通関
                    dr.Item("KAGAMI_PRC") = "03"
            End Select
        End If
        If LMG550PRT_SHUKEI.Equals(prtType) = True Then
            Select Case setDr.Item("PRINT_TYPE1").ToString()
                Case "05" '通常
                    dr.Item("MISK_PRC") = "00"
                Case "06" 'ミスク
                    dr.Item("MISK_PRC") = "01"
                Case "07" '全部
                    dr.Item("MISK_PRC") = "02"
            End Select
        End If
        dr.Item("PREVIEW_FLG") = "0"
        Select Case prtType
            Case LMG550PRT_KAGAMI
                dr.Item("RPT_NB") = "1"
            Case LMG550PRT_SHUKEI, LMG550PRT_SHUKEIKEIRI
                dr.Item("RPT_NB") = "1"
        End Select
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()

        dr.Item("PRT_TYPE2") = setDr.Item("PRINT_TYPE2").ToString()

        '20120925 要望管理1423
        dr.Item("MAIN_BR") = setDr.Item("MAIN_BR").ToString()
        '20120925 要望管理1430
        dr.Item("SEIQTO_KBN") = setDr.Item("SEIQTO_KBN").ToString()

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMG560InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMI040IN").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("SEKY_YM") = setDr.Item("SEKY_YM").ToString()
        dr.Item("DEPART") = setDr.Item("DEPART").ToString()
        dr.Item("SEKY_KMK") = setDr.Item("SEKY_KMK").ToString()
        dr.Item("FRB_CD") = setDr.Item("FRB_CD").ToString()
        dr.Item("SRC_CD") = setDr.Item("SRC_CD").ToString()
        dr.Item("COST_CENTER") = setDr.Item("COST_CENTER").ToString()
        dr.Item("MISK_CD") = setDr.Item("MISK_CD").ToString()
        '20120925 要望管理1423
        dr.Item("MAIN_BR") = setDr.Item("MAIN_BR").ToString()

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectCsvListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCsvListData", ds)

    End Function

    ''' <summary>
    ''' サーバ日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("SYS_DATETIME")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SimpleScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region

End Class