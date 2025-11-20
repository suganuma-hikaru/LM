' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI250BLF : シリンダ番号チェック
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI250BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI250BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI610BLC = New LMI610BLC()

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(Me._Blc, "DoPrint", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
