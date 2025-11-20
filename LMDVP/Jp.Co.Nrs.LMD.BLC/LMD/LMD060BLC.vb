' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫サブシステム
'  プログラムID     :  LMD060    : 月末在庫履歴作成
'  作  成  者       :  [kim]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMD060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD060DAC = New LMD060DAC()

#End Region

#Region "アクションConst"

    ''' <summary>
    ''' 排他チェック用のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_HAITA As String = "SelectHaitaData"

    ''' <summary>
    ''' 在庫履歴データ削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE As String = "DeleteData"

    ''' <summary>
    ''' 在庫履歴データ検索前処理アクション（検索件数判別、出力行数、追加列数取得）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_PRE_SELECT As String = "SelectActionPre"

    ''' <summary>
    ''' 在庫履歴データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT As String = "SelectAction"

    ''' <summary>
    ''' 在庫履歴テーブルINSERT用、在庫トランザクションデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_JIKKOU As String = "JikkouAction"

#End Region 'アクションConst

    ''' <summary>
    ''' 検索OUTテーブル(検索結果格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMD060OUT"

#Region "検索処理"

    ''' <summary>
    ''' 検索の値取得（コントロール）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索データ件数などを取得</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '在庫履歴データ取得
        ds = Me.DacAccess(ds, LMD060BLC.ACTION_ID_SELECT)

        'メッセージ設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
            Return ds
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
            Return ds
        ElseIf Integer.Parse(ds.Tables(LMD060BLC.TABLE_NM_OUT).Rows(0)("ROW_CNT").ToString()) > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "更新処理（削除・実行）"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '削除処理
        ds = Me.DacAccess(ds, LMD060BLC.ACTION_ID_DELETE)
        'If Me.ServerChkJudge(ds, LMD060BLC.ACTION_ID_DELETE) = False Then
        '    '更新件数が0件の場合、処理失敗メッセージを設定する
        '    MyBase.SetMessage("S001", New String() {"削除"})
        'End If

        Return ds

    End Function

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JikkouAction(ByVal ds As DataSet) As DataSet

        '実行処理
        ds = Me.DacAccess(ds, LMD060BLC.ACTION_ID_JIKKOU)
        'If Me.ServerChkJudge(ds, LMD060BLC.ACTION_ID_JIKKOU) = False Then
        '    'エラーが存在する場合、処理失敗メッセージを設定する
        '    MyBase.SetMessage("S001", New String() {"実行"})
        'End If

        Return ds

    End Function

#End Region '更新処理（削除・実行）

#Region "チェック"

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        '排他対象データ取得
        ds = Me.DacAccess(ds, LMD060BLC.ACTION_ID_SELECT_HAITA)

        Return ds

    End Function

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

#End Region

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

End Class
