' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM080BLC : 運送会社マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM080BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM080DAC = New LMM080DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送会社マスタ更新対象データ件数検索
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
    ''' 運送会社マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '運送会社マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '担当者別荷主マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistUnsocoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistUnsocoM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 運送会社マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaUnsocoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsocoM", ds)

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' 運送会社マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '新規登録
        Call Me.InsertData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As Boolean

        '運送会社情報の新規登録
        Dim rtnResult As Boolean = Me.InsertUnsocoData(ds)

        '担当者荷主別情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelCustRptData(ds)

        '担当者荷主別情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertCustRptData(ds)

        Return rtnResult

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.UpdateData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        '運送会社情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateUnsocoData(ds)

        '運送会社荷主別送り状情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelCustRptData(ds)

        '運送会社荷主別送り状情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertCustRptData(ds)

        Return rtnResult

    End Function

#End Region

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '削除処理
        Call Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        '運送会社情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnsocoM")

        '運送会社荷主別送り状情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteCustRptM")

        Return rtnResult

    End Function

#End Region

#Region "存在チェック"

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function CheckZipM(ByVal ds As DataSet) As DataSet

    '    If String.IsNullOrEmpty(ds.Tables("LMM080IN").Rows(0).Item("ZIP").ToString()) = False Then

    '        'DACクラス呼出
    '        ds = MyBase.CallDAC(Me._Dac, "CheckExistZipM", ds)

    '        '処理件数による判定
    '        If MyBase.GetResultCount() = 0 Then
    '            '0件の場合
    '            MyBase.SetMessage("E079", New String() {"郵便番号マスタ", "郵便番号"})

    '        End If

    '    End If

    '    Return ds

    'End Function
    '2011.09.08 検証結果_導入時要望№1対応 END

#End Region

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#Region "ユーティリティ"

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

#Region "共通更新"

    ''' <summary>
    ''' 運送会社 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUnsocoData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertUnsocoM")

    End Function

    ''' <summary>
    ''' 運送会社 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsocoData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateUnsocoM")

    End Function


    ''' <summary>
    ''' 運送会社荷主別送り状 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelCustRptData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelCustRptM")

    End Function

    ''' <summary>
    ''' 運送会社荷主別送り状 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertCustRptData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertCustRptM")

    End Function

#End Region

#End Region

End Class
