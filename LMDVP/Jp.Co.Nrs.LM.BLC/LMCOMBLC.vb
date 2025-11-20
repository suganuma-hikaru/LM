' ==========================================================================
'  システム名     : LM
'  サブシステム名 : LMCOM     : システム共通
'  プログラムID   : LMCOMBLC  : システム共通データ処理
'  作  成  者     : 大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMCOMBLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMCOMBLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMCOMDAC = New LMCOMDAC()

#End Region

#Region "共通データ処理"

    ''' <summary>
    ''' 商品明細マスタデータ処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsDetailsData(ByVal ds As DataSet) As DataSet

        '商品明細マスタのデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectGoodsDetailsData", ds)

        Return ds

    End Function

#End Region

End Class
