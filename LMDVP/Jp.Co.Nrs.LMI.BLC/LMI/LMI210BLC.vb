' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理（鈴木商館）
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI210BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI210BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI210DAC = New LMI210DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索データ取得処理（入荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuInkaData(ByVal ds As DataSet) As DataSet

        '入荷データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuInkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' '検索データ取得処理（出荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuOutkaData(ByVal ds As DataSet) As DataSet

        '出荷データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuOutkaData", ds)

        Return ds

    End Function
　
#End Region

End Class
