' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMH050BLC : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH050DAC = New LMH050DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 仮置きメッセージ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetSMessage(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "GetSMessage", ds)

    End Function

#End Region

#End Region

End Class
