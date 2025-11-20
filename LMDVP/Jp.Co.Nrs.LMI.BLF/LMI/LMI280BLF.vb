' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI280BLF : 最低荷役保証料・差額明細書印刷指示(日産物流)
'  作  成  者       :  中村好孝
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI280BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI280BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private _Print620 As LMI620BLC = New LMI620BLC()

#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 最低荷役保証料・差額明細書（日産物流）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMIInData(ds, New LMI620DS(), "LMI620IN")

        inDs.Merge(New RdPrevInfoDS)

        '
        inDs = MyBase.CallBLC(Me._Print620, "DoPrint", inDs)

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
        Dim setDr As DataRow = ds.Tables("LMI280IN").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        dr.Item("F_DATE") = setDr.Item("F_DATE").ToString()
        dr.Item("T_DATE") = setDr.Item("T_DATE").ToString()
        dt.Rows.Add(dr)

        Return inDs

    End Function
#End Region

#End Region

End Class
