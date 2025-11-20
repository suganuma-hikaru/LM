' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : 
'  プログラムID     :  LMH050BLF : 
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH050BLC = New LMH050BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 仮置き
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetSMessage(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "GetSMessage", ds)

    End Function

#End Region


#End Region

End Class
