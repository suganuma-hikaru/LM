' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI360BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI360BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI360BLC = New LMI360BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function MakeData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '作成処理
            ds = MyBase.CallBLC(Me._Blc, "MakeData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI360IN"
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

            Case "01" '横持運賃(仮タイトル)

                prtBlc = New Com.Base.BaseBLC() {New LMI690BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI690InData(ds)}


                'Case "01" '一括

                '    prtBlc = New Com.Base.BaseBLC() {New LMI580BLC(), New LMI581BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI580InData(ds), Me.SetDataSetLMI581InData(ds)}

                'Case "02" '類別

                '    prtBlc = New Com.Base.BaseBLC() {New LMI580BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI580InData(ds)}

                'Case "03" '日別

                '    prtBlc = New Com.Base.BaseBLC() {New LMI581BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI581InData(ds)}

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
    ''' LMI690DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI690InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMI690InData(ds, New LMI690DS(), "LMI690IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI690InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMI360IN").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        dr.Item("F_DATE") = setDr.Item("DATE_FROM").ToString()
        dr.Item("T_DATE") = setDr.Item("DATE_TO").ToString()
        dr.Item("PRT_SYUBETU") = setDr.Item("PRINT_KB").ToString()
        'dr.Item("ROSEN_SYUBETU") = setDr.Item("PRINT_KB2").ToString()'おそらく使わない。

        dt.Rows.Add(dr)

        Return inDs

    End Function

#End Region

End Class