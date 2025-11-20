' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG010BLC : 保管料/荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMG010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG010DAC = New LMG010DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(件数取得)
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
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 単価未承認チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectChkApprovalTanka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectChkApprovalTanka", ds)

    End Function

    ''' <summary>
    ''' 変動保管料チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectChkVar(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectChkVar", ds)

    End Function

#End Region

#Region "前回計算取消処理"

    ''' <summary>
    ''' 前回計算取消処理（取得）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpDateDelSelect(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpDateDelSelectTable", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 前回計算取消処理（更新・削除）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateDel(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpDateDelTable", ds)

        Return ds

    End Function


#End Region

#Region "実行処理"

    ''' <summary>
    ''' 実行処理(登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertWorkHead(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckHaitaCUST", ds)

    End Function


    ''' <summary>
    ''' バッチ呼出処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CalcBatch(ByVal ds As DataSet)

        MyBase.CallDAC(Me._Dac, "CtlCalcHokanNiyaku", ds)

    End Sub

#End Region

#Region "排他チェック"

    ''' <summary>
    ''' 荷主マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaMCust(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckHaita", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ワークヘッダ排他
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaGHEAD(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "CheckHaitaGHEAD", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
