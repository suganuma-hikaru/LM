' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI170BLF : 運賃請求印刷指示(ゴードー)
'  作  成  者       :  umano
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI170BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI170BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print600 As LMI600BLC = New LMI600BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print601 As LMI601BLC = New LMI601BLC()


#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 運賃請求明細書（ゴードー20締用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintClose20(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMIInData(ds, New LMI600DS(), "LMI600IN")

        inDs.Merge(New RdPrevInfoDS)

        inDs = MyBase.CallBLC(Me._Print600, "DoPrint", inDs)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return inDs

        End If

        Return inDs

    End Function

    ''' <summary>
    ''' 運賃請求明細書（ゴードー末締用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintCloseMatsu(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMIInData(ds, New LMI601DS(), "LMI601IN")

        inDs.Merge(New RdPrevInfoDS)

        inDs = MyBase.CallBLC(Me._Print601, "DoPrint", inDs)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return inDs

        End If

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
    Private Function SetDataSetLMIInData(ByVal ds As DataSet, ByVal inDs As DataSet, _
                                         ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMI170IN").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        dr.Item("SEIQ_CD") = setDr.Item("SEIQ_CD").ToString()
        dr.Item("F_DATE") = setDr.Item("F_DATE").ToString()
        dr.Item("T_DATE") = setDr.Item("T_DATE").ToString()
        dr.Item("SEIQ_SYUBETU") = setDr.Item("SEIQ_SYUBETU").ToString()
        dr.Item("UNTIN_CALCULATION_KB") = setDr.Item("UNTIN_CALCULATION_KB").ToString()
        dr.Item("PRINT_TYPE") = setDr.Item("PRINT_TYPE").ToString()
        dt.Rows.Add(dr)

        Return inDs

    End Function
#End Region

#End Region

End Class
