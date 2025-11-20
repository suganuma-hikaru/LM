' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI350  : 保管荷役明細(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI350BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI350BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI680BLC = New LMI680BLC()

#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 保管荷役明細
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "DoPrint", ds)

    End Function

#End Region

#End Region

End Class