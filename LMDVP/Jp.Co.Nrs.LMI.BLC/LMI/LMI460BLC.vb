' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI460  : ローム　請求先コード変更
'  作  成  者       :  [NAKAMURA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI460BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI460BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    '''' <summary>
    '''' 使用するDACクラスの生成
    '''' </summary>
    '''' <remarks></remarks>
    '''' 
    Private _Dac As LMI460DAC = New LMI460DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 運賃データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdDataUnchin(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        '運賃データの更新
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdDataUnchin", ds)

        Return ds

    End Function

#End Region

End Class
