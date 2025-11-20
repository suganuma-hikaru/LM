' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI480BLF : 古河請求(ディック)
'  作  成  者       :  kido
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI480BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI480BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI480BLC = New LMI480BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)

        Return ds

    End Function

#End Region

#Region "印刷処理"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PRINT_DoPrint(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Blc, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function
#End Region

End Class
