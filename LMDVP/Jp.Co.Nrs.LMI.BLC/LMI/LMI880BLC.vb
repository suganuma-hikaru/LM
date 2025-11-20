' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI880  : 篠崎運送月末在庫実績データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI880BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI880BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI880DAC = New LMI880DAC()

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

    ''' <summary>
    ''' CSV出力データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateCSV(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateCSV", ds)

    End Function

#End Region

End Class
