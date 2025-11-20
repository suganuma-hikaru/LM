' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI990BLF : サーテック　運賃明細作成
'  作  成  者       :  sia-minagawa
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI990BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI990BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

#End Region

#Region "Method"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI990OUT_PRT"

        Dim dt As DataTable = ds.Tables(tableNm)

        'レコードがない場合、スルー
        If dt.Rows.Count <= 0 Then
            MyBase.SetMessage("E070")   '印刷対象データがありませんでした。
            Return ds
        End If

        Dim printShubetsu As String = "01"  '01:運賃請求明細書

        'プレビュー用DataTableがない場合は追加
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If

        'プレビュー用DataTableをクリア
        Dim rdPrevDt As DataTable = ds.Tables(LMConst.RD)
        rdPrevDt.Clear()

        Dim doPrintOutDt As DataTable = Nothing

        '印刷処理
        doPrintOutDt = Me.DoPrint(ds, printShubetsu).Tables(LMConst.RD)

        If MyBase.IsMessageExist() Then
            Return ds
        End If

        'プレビュー情報をプレビュー用DataTableに設定
        If doPrintOutDt IsNot Nothing Then
            rdPrevDt.Merge(doPrintOutDt)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printShubetsu As String) As DataSet

        Dim prtBlcInDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        Select Case printShubetsu

            Case "01" '01:運賃請求明細書

                prtBlc = New Com.Base.BaseBLC() {New LMI991BLC()}
                prtBlcInDs = New DataSet() {Me.SetDataSetLMI991InData(ds)}

        End Select

        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To prtBlc.Count - 1

            prtBlcInDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", prtBlcInDs(i))

            If MyBase.IsMessageExist() Then
                Return rtnDs
            End If

            rdPrevDt.Merge(prtBlcInDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMI980DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI991InData(ByVal ds As DataSet) As DataSet

        'コピー元:LMI990DS
        Dim lmi990InDr As DataRow = ds.Tables("LMI990IN").Rows(0)
        Dim lmi990OutPrtDrs As DataRow() = ds.Tables("LMI990OUT_PRT").Select

        'コピー先:LMI991DS
        Dim lmi991Ds As DataSet = New LMI991DS

        'LMI990DS.LMI990IN → LMI991DS.LMI991IN
        Dim lmi991InDt As DataTable = lmi991Ds.Tables("LMI991IN")
        Dim lmi991InDr As DataRow = lmi991InDt.NewRow
        lmi991InDr.Item("NRS_BR_CD") = lmi990InDr.Item("NRS_BR_CD")
        lmi991InDt.Rows.Add(lmi991InDr)

        'LMI990DS.LMI990OUT_PRT → LMI991DS.LMI991OUT_PRT
        Dim lmi991OutPrtDt As DataTable = lmi991Ds.Tables("LMI991OUT_PRT")
        Dim lmi991OutPrtDr As DataRow = Nothing

        For i As Integer = 0 To lmi990OutPrtDrs.Length - 1

            lmi991OutPrtDr = lmi991OutPrtDt.NewRow
            lmi991OutPrtDr.Item("SHUKKO_DATE") = lmi990OutPrtDrs(i).Item("SHUKKO_DATE").ToString()
            lmi991OutPrtDr.Item("NONYU_SAKI") = lmi990OutPrtDrs(i).Item("NONYU_SAKI").ToString()
            lmi991OutPrtDr.Item("SHUKKO_SU") = lmi990OutPrtDrs(i).Item("SHUKKO_SU").ToString()
            lmi991OutPrtDr.Item("YUSO_RYO") = lmi990OutPrtDrs(i).Item("YUSO_RYO").ToString()

            lmi991OutPrtDr.Item("SEIKYU_DATE") = lmi990InDr.Item("SEIKYU_DATE").ToString()
            lmi991OutPrtDt.Rows.Add(lmi991OutPrtDr)

        Next

        Return lmi991Ds

    End Function

#End Region

End Class
