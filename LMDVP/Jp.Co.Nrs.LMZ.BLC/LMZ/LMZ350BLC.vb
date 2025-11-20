' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ350BLC : 真荷主照会
'  作  成  者       :  hori
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ350BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ350BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ350DAC = New LMZ350DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' UNマスタデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' UNマスタデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 国取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboCountry(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectComboCountry", ds)

    End Function

#End Region

#End Region

End Class
