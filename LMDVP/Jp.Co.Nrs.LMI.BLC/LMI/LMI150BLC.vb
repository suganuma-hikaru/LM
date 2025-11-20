' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150  : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI150BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI150BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI150DAC = New LMI150DAC()

#End Region

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectData", ds)

    End Function

    ''' <summary>
    ''' NRS処理番号の最大値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNrsProcNo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectNrsProcNo", ds)

    End Function

#End Region '検索

#Region "登録"

    ''' <summary>
    ''' データ登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertData", ds)

    End Function

#End Region '登録

#Region "更新"

    ''' <summary>
    ''' データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateData", ds)

    End Function

#End Region '更新

#End Region 'Method

End Class
