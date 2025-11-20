' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ340BLC : 入荷棟室ZONEチェック処理
'  作  成  者       :  asatsuma
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ340BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ340BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ340DAC = New LMZ340DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' チェック処理不要フラグ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckFlg(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCheckFlg", ds)

    End Function

    ''' <summary>
    ''' 貯蔵最大数チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckCapa(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCheckCapa", ds)

    End Function

    ''' <summary>
    ''' 商品数量計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCalcQty(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCalcQty", ds)

    End Function

    ''' <summary>
    ''' 属性系チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckAttr(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCheckAttr", ds)

    End Function

#End Region

#End Region

End Class
