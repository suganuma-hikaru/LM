' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMA       : メニュー
'  プログラムID     :  LMA030BLC : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMA030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMA030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMA030DAC = New LMA030DAC()

#End Region


#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectComboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectComboData", ds)

    End Function

#End Region

#End Region

End Class