' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI320BLF : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI320BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI320BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI320BLC = New LMI320BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoMake(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "DoMake", ds)

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing
        Dim printShubetu As String = ds.Tables("LMI320IN").Rows(0)("PRINT_SHUBETU").ToString()

        Select Case printShubetu

            Case "01" '請求明細書

                prtBlc = New Com.Base.BaseBLC() {New LMI650BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI650InData(ds)}

                'Notes1907により削除
                'Case "02" '請求鑑

                '    prtBlc = New Com.Base.BaseBLC() {New LMI660BLC(), New LMI661BLC(), New LMI662BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI660InData(ds), Me.SetDataSetLMI660InData(ds), Me.SetDataSetLMI660InData(ds)}

                'Notes1907により削除
                'Case "03" '売上仕入伝票

                '    prtBlc = New Com.Base.BaseBLC() {New LMI670BLC(), New LMI671BLC(), New LMI672BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI670InData(ds), Me.SetDataSetLMI670InData(ds), Me.SetDataSetLMI670InData(ds)}

                'Notes1907により削除
                'Case "04" '一括印刷

                '    prtBlc = New Com.Base.BaseBLC() {New LMI650BLC() _
                '                                   , New LMI660BLC(), New LMI661BLC(), New LMI662BLC() _
                '                                   , New LMI670BLC(), New LMI671BLC(), New LMI672BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMI650InData(ds) _
                '                         , Me.SetDataSetLMI660InData(ds), Me.SetDataSetLMI660InData(ds), Me.SetDataSetLMI660InData(ds) _
                '                         , Me.SetDataSetLMI670InData(ds), Me.SetDataSetLMI670InData(ds), Me.SetDataSetLMI670InData(ds)}

                'Notes1907により作成
            Case "02" '請求書
                prtBlc = New Com.Base.BaseBLC() {New LMI675BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI675InData(ds)}

                'Notes1907により作成
            Case "04" '一括印刷

                prtBlc = New Com.Base.BaseBLC() {New LMI650BLC() _
                                               , New LMI675BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI650InData(ds) _
                                     , Me.SetDataSetLMI675InData(ds)}

        End Select

        Dim rtnDs As DataSet = Nothing
        Dim max As Integer = prtBlc.Count - 1
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMI650DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI650InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMI650DS
        Dim inDt As DataTable = inDs.Tables("LMI650IN")
        Dim inDr As DataRow = Nothing
        Dim dr As DataRow() = ds.Tables("LMI320IN").Select
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            inDr = inDt.NewRow
            inDr.Item("NRS_BR_CD") = dr(i).Item("NRS_BR_CD").ToString()
            inDr.Item("SEKY_DATE") = dr(i).Item("SEIQ_DATE").ToString()
            inDr.Item("SEIQTO_CD") = dr(i).Item("SEIQTO_CD").ToString()
            inDt.Rows.Add(inDr)

        Next

        Return inDs

    End Function

    ''' <summary>
    ''' LMI660DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI660InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMI660DS
        Dim inDt As DataTable = inDs.Tables("LMI660IN")
        Dim inDr As DataRow = Nothing
        Dim dr As DataRow() = ds.Tables("LMI320IN").Select
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            inDr = inDt.NewRow
            inDr.Item("NRS_BR_CD") = dr(i).Item("NRS_BR_CD").ToString()
            inDr.Item("SEKY_DATE") = dr(i).Item("SEIQ_DATE").ToString()
            inDr.Item("SEIQTO_CD") = dr(i).Item("SEIQTO_CD").ToString()
            inDt.Rows.Add(inDr)

        Next

        Return inDs

    End Function

    ''' <summary>
    ''' LMI670DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI670InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMI670DS
        Dim inDt As DataTable = inDs.Tables("LMI670IN")
        Dim inDr As DataRow = Nothing
        Dim dr As DataRow() = ds.Tables("LMI320IN").Select
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            inDr = inDt.NewRow
            inDr.Item("NRS_BR_CD") = dr(i).Item("NRS_BR_CD").ToString()
            inDr.Item("SEIQTO_CD") = dr(i).Item("SEIQTO_CD").ToString()
            inDr.Item("SKYU_DATE") = dr(i).Item("SEIQ_DATE").ToString()
            inDt.Rows.Add(inDr)

        Next

        Return inDs

    End Function

    ''' <summary>
    ''' LMI675DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI675InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMI675DS
        Dim inDt As DataTable = inDs.Tables("LMI675IN")
        Dim inDr As DataRow = Nothing
        Dim dr As DataRow() = ds.Tables("LMI320IN").Select
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            inDr = inDt.NewRow
            inDr.Item("NRS_BR_CD") = dr(i).Item("NRS_BR_CD").ToString()
            inDr.Item("SKYU_DATE") = dr(i).Item("SEIQ_DATE").ToString()
            inDr.Item("SEIQTO_CD") = dr(i).Item("SEIQTO_CD").ToString()
            inDt.Rows.Add(inDr)

        Next

        Return inDs

    End Function


#End Region

End Class
