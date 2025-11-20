' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI890  : NRC出荷／回収情報Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI890BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI890BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI890DAC = New LMI890DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' Excel出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectCSVメソッド呼出</remarks>
    Private Function SelectExcel(ByVal ds As DataSet) As DataSet

        '出荷実績
        ds = MyBase.CallDAC(Me._Dac, "SelectExcelShukka", ds)

        '返送実績
        ds = MyBase.CallDAC(Me._Dac, "SelectExcelHenso", ds)

        '長期未返却
        ds = MyBase.CallDAC(Me._Dac, "SelectExcelMihen", ds)

        Return ds

    End Function

#End Region

End Class
