' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI850  : 日医工在庫照合データCSV作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI850BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI850BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI850DAC = New LMI850DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectCSVメソッド呼出</remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCSV", ds)

    End Function

#End Region

End Class
