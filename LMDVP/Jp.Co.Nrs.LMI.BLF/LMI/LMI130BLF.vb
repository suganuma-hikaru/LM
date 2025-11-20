' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI130BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI130BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI130BLC = New LMI130BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 追加対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectAddData(ByVal ds As DataSet) As DataSet

        '追加対象データの検索処理
        ds = MyBase.CallBLC(Me._Blc, "SelectAddData", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI130INOUT"
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

        '印刷処理
        setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt Is Nothing = False Then
            cnt = setRptDt.Rows.Count - 1
            For j As Integer = 0 To cnt
                rtnDt.ImportRow(setRptDt.Rows(j))
            Next
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 登録処理（作業レコード）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRecord(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "InsertSagyoRecord", ds)

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

            Case "01" '詰め合わせ明細書

                prtBlc = New Com.Base.BaseBLC() {New LMC650BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC650InData(ds)}

            Case "02" '荷札

                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds)}

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
    ''' LMC650DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC650InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMC650InData(ds, New LMC650DS(), "LMC650INOUT")

    End Function

    ''' <summary>
    ''' LMC550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMC550InData(ds, New LMC550DS(), "LMC550IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC650InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMI130INOUT").Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.NewRow()

            dr.Item("NRS_BR_CD") = ds.Tables("LMI130INOUT").Rows(i).Item("NRS_BR_CD").ToString()
            dr.Item("OUTKA_NO_L") = ds.Tables("LMI130INOUT").Rows(i).Item("OUTKA_NO_L").ToString()
            dr.Item("DEST_CD") = ds.Tables("LMI130INOUT").Rows(i).Item("DEST_CD").ToString()
            dr.Item("DEST_NM") = ds.Tables("LMI130INOUT").Rows(i).Item("DEST_NM").ToString()
            dr.Item("GOODS_CD_NRS") = ds.Tables("LMI130INOUT").Rows(i).Item("GOODS_CD_NRS").ToString()
            dr.Item("GOODS_CD_CUST") = ds.Tables("LMI130INOUT").Rows(i).Item("GOODS_CD_CUST").ToString()
            dr.Item("GOODS_NM_1") = ds.Tables("LMI130INOUT").Rows(i).Item("GOODS_NM_1").ToString()
            dr.Item("GOODS_NM_2") = ds.Tables("LMI130INOUT").Rows(i).Item("GOODS_NM_2").ToString()
            dr.Item("LT_DATE") = ds.Tables("LMI130INOUT").Rows(i).Item("LT_DATE").ToString()
            dr.Item("LOT_NO") = ds.Tables("LMI130INOUT").Rows(i).Item("LOT_NO").ToString()
            dr.Item("TSUME_NB") = ds.Tables("LMI130INOUT").Rows(i).Item("TSUME_NB").ToString()
            dr.Item("PRT_NB") = ds.Tables("LMI130INOUT").Rows(i).Item("PRT_NB").ToString()

            dt.Rows.Add(dr)
        Next

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
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim indr() As DataRow = ds.Tables("LMI130INOUT").Select(Nothing, "OUTKA_NO_L,OUTKA_NO_M,OUTKA_NO_S")
        Dim dr As DataRow = dt.NewRow()

        dr.Item("NRS_BR_CD") = indr(0).Item("NRS_BR_CD").ToString()
        dr.Item("OUTKA_NO_L") = indr(0).Item("OUTKA_NO_L").ToString()
        dr.Item("GOODS_CD_CUST") = indr(0).Item("GOODS_CD_CUST").ToString()
        dr.Item("GOODS_NM_1") = indr(0).Item("GOODS_NM_1").ToString()
        dr.Item("GOODS_NM_2") = indr(0).Item("GOODS_NM_2").ToString()
        dr.Item("LT_DATE") = indr(0).Item("LT_DATE").ToString()
        dr.Item("LOT_NO") = indr(0).Item("LOT_NO").ToString()
        dr.Item("GOODS_SYUBETU") = indr(0).Item("GOODS_SYUBETU").ToString()
        dr.Item("PRT_NB") = indr(0).Item("PRT_NB").ToString()

#If True Then       'ADD 2018/10/26 送り状バグ対応

        dr.Item("PRT_NB_FROM") = "1".ToString
        dr.Item("PRT_NB_TO") = indr(0).Item("PRT_NB").ToString()


#End If
        dr.Item("PTN_FLAG") = "2"

        dt.Rows.Add(dr)

        Return inDs

    End Function

#End Region

End Class