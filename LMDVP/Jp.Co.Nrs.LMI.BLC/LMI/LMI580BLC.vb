' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI580    : TSMC請求明細書
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI580BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI580BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI580DAC = New LMI580DAC()

#End Region

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' 印刷データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = ds.Copy()

        '印刷データ検索（ヘッダ）
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDataHead", ds)

        '印刷データ検索（メイン）
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDataMain", ds)

        '印刷データ検索（詳細）
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDataDetail", rtnDs)

        Return rtnDs

    End Function

#End Region

#End Region

End Class
