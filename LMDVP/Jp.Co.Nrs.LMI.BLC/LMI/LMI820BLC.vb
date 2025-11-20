' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI820  : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI820BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI820BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI820DAC = New LMI820DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' TXT出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectTXTメソッド呼出</remarks>
    Private Function SelectTXT(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectTXT", ds)

    End Function

#End Region

End Class
