' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ370BLF : 温度管理アラートチェック
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ370BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ370BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ370BLC = New LMZ370BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 温度管理アラートチェック対象レコード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsAndDetails(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectGoodsAndDetails", ds)

    End Function

#End Region

#End Region

End Class
