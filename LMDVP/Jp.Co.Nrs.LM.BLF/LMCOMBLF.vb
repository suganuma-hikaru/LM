' ==========================================================================
'  システム名      : LM
'  サブシステム名  : LMCOM    : システム共通
'  プログラムID    : LMCOMBLF : システム共通データ処理
'  作  成  者      : 大貫和正
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMCOMBLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMCOMBLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMCOMBLC = New LMCOMBLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 商品明細検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOutkaMメソッドに飛ぶ</remarks>
    Private Function SelectGoodsDetailsData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectGoodsDetailsData", ds)

    End Function

#End Region

End Class
