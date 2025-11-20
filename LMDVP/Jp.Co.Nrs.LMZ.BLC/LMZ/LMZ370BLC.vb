' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ370BLC : 温度管理アラートチェック
'  作  成  者       :  
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ370BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ370BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ370DAC = New LMZ370DAC()

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

        For Each dr As DataRow In ds.Tables("LMZ370IN").Rows
            Dim dacDs As DataSet = ds.Clone()
            dacDs.Tables("LMZ370IN").ImportRow(dr)

            MyBase.CallDAC(Me._Dac, "SelectGoodsAndDetails", dacDs)

            For Each dacDr As DataRow In dacDs.Tables("LMZ370OUT").Rows
                ds.Tables("LMZ370OUT").ImportRow(dacDr)
            Next
        Next

        Return ds

    End Function

#End Region

#End Region

End Class
