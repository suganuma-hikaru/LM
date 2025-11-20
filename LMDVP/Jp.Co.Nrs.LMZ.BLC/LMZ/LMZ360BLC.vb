' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ360BLC : Tra-Net連携共通処理
'  作  成  者       :  kumakura
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ360BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ360BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _Dac As LMZ360DAC = New LMZ360DAC()

#End Region

#Region "Method"

#Region "データ取得"

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DataGet(ByVal ds As DataSet) As DataSet

        '運送(特大)データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsoLLData", ds)

        '運送(大)データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsoLData", ds)

        '運送(中)データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsoMData", ds)

        '支払運賃データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectShiharaiData", ds)

        Return ds

    End Function

#End Region

#Region "送信フラグ更新"

    ''' <summary>
    ''' 送信フラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdSoshinFlg(ByVal ds As DataSet) As DataSet

        '送信フラグ更新
        ds = MyBase.CallDAC(Me._Dac, "UpdSoshinFlg", ds)

        Return ds

    End Function

#End Region

#Region "データ送信"

    ''' <summary>
    ''' データ送信
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DataSend(ByVal ds As DataSet) As DataSet

        'データ送信
        ds = MyBase.CallDAC(Me._Dac, "DataSend", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
