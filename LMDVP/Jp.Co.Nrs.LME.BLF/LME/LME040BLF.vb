' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================

Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LME040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LME040BLC = New LME040BLC()

#End Region

#Region "Method"

#Region "初期検索処理"

    ''' <summary>
    ''' 対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '対象データの検索処理
        ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function HozonData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME040IN")
        Dim rtnResult As Boolean = False
        Dim rtnDs As DataSet = Nothing

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            MyBase.SetMessage(Nothing)

            '保存処理
            rtnDs = MyBase.CallBLC(Me._Blc, "HozonData", ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()
            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            Else
                'rtnDsを返すと、採番した値とかが設定されるので、dsを返す
                Return ds
            End If

        End Using

        Return rtnDs

    End Function

#End Region

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LME040IN"
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

            Case "01" '作業指示書

                prtBlc = New Com.Base.BaseBLC() {New LME520BLC()}
                setDs = New DataSet() {Me.SetDataSetLME520InData(ds)}

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
    ''' LME520DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLME520InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLME520InData(ds, New LME520DS(), "LME520IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLME520InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LME040IN").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("SAGYO_SIJI_NO") = setDr.Item("SAGYO_SIJI_NO").ToString()
        dr.Item("SAIHAKKO_FLG") = setDr.Item("SAIHAKKO_FLG").ToString()

        dt.Rows.Add(dr)

        Return inDs

    End Function

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME040IN")
        Dim rtnResult As Boolean = False
        Dim rtnDs As DataSet = Nothing

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            MyBase.SetMessage(Nothing)

            '作業指示 削除処理
            rtnDs = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()
            If rtnResult = True Then
            Else
                Return ds
            End If

            '現場作業指示 削除処理
            rtnDs = CancelTabletData(ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()
            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            Else
                Return ds
            End If

        End Using

        Return rtnDs

    End Function

    ''' <summary>
    ''' タブレットデータのキャンセル処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CancelTabletData(ByVal ds As DataSet) As DataSet

        Dim inDs As New LME800DS

        Dim nrsBrCd As String = ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString
        Dim sagyoSijiNo As String = ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("SAGYO_SIJI_NO").ToString

        Dim inDt As DataTable = inDs.Tables("LME800IN")
        Dim inDr As DataRow = inDt.NewRow
        inDr.Item("NRS_BR_CD") = nrsBrCd
        inDr.Item("SAGYO_SIJI_NO") = sagyoSijiNo
        inDr.Item("WH_TAB_STATUS_KB") = "00"    '現場指示：未指示
        inDr.Item("PROC_TYPE") = "02"           '処理区分：削除

        inDt.Rows.Add(inDr)

        Return MyBase.CallBLC(New LME800BLC(), LME800BLC.FUNCTION_NM.WH_SAGYO_SHIJI_CANCEL, inDs)

    End Function

#End Region

#End Region

End Class