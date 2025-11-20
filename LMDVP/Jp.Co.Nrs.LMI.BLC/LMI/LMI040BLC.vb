' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI040  : 請求データ編集 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI040DAC = New LMI040DAC()

#End Region

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' データ件数検索
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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' データ重複検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInsertData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectInsertData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If 1 <= count Then
            '1件以上の場合
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 複写時の合算処理判定のための検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCopyData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectInsertData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If 1 <= count Then
            '1件以上の場合
            MyBase.SetMessage("W196")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' CSVデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCsvListData(ByVal ds As DataSet) As DataSet

        Dim csvKb As String = ds.Tables("LMI040IN").Rows(0).Item("CSV_KB").ToString()
        'If ("00").Equals(csvKb) = True Then
        '    '運賃データ送信ファイル作成
        '    ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataUNCHIN", ds)

        'ElseIf ("01").Equals(csvKb) = True Then
        '    '請求データ送信ファイル作成
        '    ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataGL", ds)

        'ElseIf ("02").Equals(csvKb) = True Then
        '    'FPDEデータファイル作成
        '    ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataFPDE_HIKAZEI", ds)
        '    ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataFPDE_KAZEI", ds)

        'End If
        If ("00").Equals(csvKb) = True Then
            'FPDEデータファイル作成
            ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataFPDE_HIKAZEI", ds)
            ds = MyBase.CallDAC(Me._Dac, "SelectCsvDataFPDE_KAZEI", ds)

        End If

        Return ds

    End Function

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Dim rtnBoolean As Boolean = Me.ServerChkJudge(ds, "InsertGLData")

        Return ds

    End Function

#End Region

#Region "編集登録"

    ''' <summary>
    ''' 編集登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '削除処理
        Dim rtnBoolean As Boolean = Me.ServerChkJudge(ds, "EditDelData")

        '新規追加処理
        rtnBoolean = rtnBoolean AndAlso Me.ServerChkJudge(ds, "InsertGLData")

        Return ds

    End Function

#End Region

#Region "合算更新登録"

    ''' <summary>
    ''' 合算更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GassanSaveAction(ByVal ds As DataSet) As DataSet

        '合算更新処理
        Dim rtnBoolean As Boolean = Me.ServerChkJudge(ds, "UpdateGLData")

        Return ds

    End Function

#End Region

#Region "削除"

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        '削除処理
        Dim rtnBoolean As Boolean = Me.ServerChkJudge(ds, "DeleteGLData")

        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        Dim rtnds As DataSet = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#End Region

End Class
