' ' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI550BLC : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI550BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI550BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

#End Region ' "Const"

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI550DAC = New LMI550DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SearchCount", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SearchSelect", ds)

        Return rtnDs

    End Function

#End Region ' "検索処理"

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateZaiTsmc(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdateZaiTsmc", ds)

        Return ds

    End Function

#End Region ' "更新処理"

#End Region ' "Method"

End Class
