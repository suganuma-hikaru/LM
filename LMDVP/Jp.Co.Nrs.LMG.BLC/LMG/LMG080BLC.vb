' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG080BLC : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMG080BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG080DAC = New LMG080DAC()

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

#End Region

#Region "予約取消"

    ''' <summary>
    ''' 予約取消処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CancelUpDate(ByVal ds As DataSet) As DataSet

        '更新処理
        MyBase.CallDAC(Me._Dac, "CancelUpDate", ds)

        Return ds

    End Function

#End Region

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        '排他処理
        Return MyBase.CallDAC(Me._Dac, "CheckHaita", ds)

    End Function

#End Region

#Region "処理結果詳細"

    ''' <summary>
    ''' 処理結果詳細
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessResults(ByVal ds As DataSet) As DataSet
        ''元のデータ
        'Dim dt As DataTable = ds.Tables("LMG080IN_RESULTS")
        'Dim outDt As DataTable = ds.Tables("LMG080_OUT_RESULTS")
        'Dim max As Integer = dt.Rows.Count - 1

        ' ''別インスタンス
        'Dim setDs As DataSet = ds.Copy()
        'Dim inTbl As DataTable = setDs.Tables("LMG080IN_RESULTS")
        'Dim setDt As DataTable = setDs.Tables("LMG080_OUT_RESULTS")
        'Dim count As Integer = 0

        'Dim rtnResult As Boolean = True

        'For i As Integer = 0 To max

        '    '値のクリア
        '    setDs.Clear()

        '    '条件の設定
        '    inTbl.ImportRow(dt.Rows(i))

        MyBase.CallDAC(Me._Dac, "ProcessResults", ds)

        'count = MyBase.GetResultCount()

        ''データが取得できた場合、値を設定
        'If count < 1 = False Then
        '    For j As Integer = 0 To count - 1
        '        outDt.ImportRow(setDt.Rows(j))
        '    Next
        'End If

        'Next

        Return ds

    End Function

#End Region

#Region "強制実行"

    ''' <summary>
    ''' 強制実行
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckExecute(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckExecute", ds)

    End Function

#End Region

#Region "強制削除"

    ''' <summary>
    ''' 強制削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ForceDelete(ByVal ds As DataSet) As DataSet

        MyBase.CallDAC(Me._Dac, "ForceDelete_1", ds)

        'エラー判定
        If MyBase.GetResultCount() = 0 Then
            MyBase.SetMessage("E011")
            Return ds
        End If

        MyBase.CallDAC(Me._Dac, "ForceDelete_2", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
