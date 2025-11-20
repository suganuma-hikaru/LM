' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970  : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI970BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI970BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI970DAC = New LMI970DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_TARGET As String = "LMI970SENDUNCHIN_TARGET"

    ''' <summary>
    ''' 処理制御データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL As String = "LMI970PROC_CTRL"

    ''' <summary>
    ''' 運賃明細送信データ元データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_DATA As String = "LMI970SENDUNCHIN_DATA"


#End Region

#Region "検索処理"

    ''' <summary>
    ''' 対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'メッセージコードの設定
        Call Me.SetSelectErrMes(MyBase.GetResultCount())

        Return ds

    End Function

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

    'ADD START 2019/05/30 要望管理006030
#Region "単価更新処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ単価一括更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIraiTanka(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return ds

    End Function

#End Region
    'ADD END   2019/05/30 要望管理006030

#Region "保存処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIrai(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return ds

    End Function

#End Region

#Region "実績作成処理"

    ''' <summary>
    ''' 日産物流運賃明細送信データの登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendUnchin(ByVal ds As DataSet) As DataSet

        Dim dtSendUnchinTarget As DataTable = ds.Tables(LMI970BLC.TABLE_NM_SENDUNCHIN_TARGET)
        Dim drSendUnchinTarget As DataRow
        Dim max As Integer = dtSendUnchinTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI970BLC.TABLE_NM_PROC_CTRL)
        Dim drProcCtrlData As DataRow = ds.Tables(LMI970BLC.TABLE_NM_PROC_CTRL).NewRow
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        Dim dtSendUnchinData As DataTable = ds.Tables(LMI970BLC.TABLE_NM_SENDUNCHIN_DATA)
        Dim drSendUnchinData As DataRow

        For i As Integer = 0 To max

            drSendUnchinTarget = dtSendUnchinTarget.Rows(i)

            '処理制御テーブルに現在処理行を設定
            dtProcCtrlData.Rows(0).Item("ROW_IDX") = i

            '工場管理番号の重複チェック（日産物流輸送依頼ＥＤＩ受信データ）
            ds = Me.DacAccess(ds, "SelectCntKojoKanriNoOfYusoIrai")

            If MyBase.GetResultCount() > 1 Then
                '複数件該当する場合はエラー
                MyBase.SetMessage("E494", New String() {String.Concat("工場管理番号 ", drSendUnchinTarget.Item("KOJO_KANRI_NO"), " "), "日産物流輸送依頼EDI受信データ", ""})
                Return ds
            End If

            '工場管理番号の重複チェック（日産物流出荷情報ＥＤＩ受信データ）
            ds = Me.DacAccess(ds, "SelectCntKojoKanriNoOfOutkaJoho")

            If MyBase.GetResultCount() > 1 Then
                '複数件該当する場合はエラー
                MyBase.SetMessage("E494", New String() {String.Concat("工場管理番号 ", drSendUnchinTarget.Item("KOJO_KANRI_NO"), " "), "日産物流出荷情報EDI受信データ", ""})
                Return ds
            End If

            '作成対象の運賃明細送信データの元データを取得
            ds = Me.DacAccess(ds, "SelectSendUnchinData")

            If dtSendUnchinData.Rows.Count <> 1 Then
                '該当データなしの場合はエラー
                MyBase.SetMessage("E011")
                Return ds
            End If

            drSendUnchinData = dtSendUnchinData.Rows(0)

            If String.IsNullOrEmpty(drSendUnchinData.Item("KOJO_KANRI_NO").ToString()) Then
                '日産物流出荷情報ＥＤＩ受信データが存在しない場合、エラー
                MyBase.SetMessage("E428", New String() {"日産物流出荷情報EDI受信データが存在しない", "、実績作成", String.Concat("[工場管理番号=", drSendUnchinTarget.Item("KOJO_KANRI_NO"), "]")})
                Return ds
            End If

            If String.IsNullOrEmpty(drSendUnchinData.Item("TAX_KB").ToString()) Then
                '運賃の税区分のコード変換に失敗した場合、エラー
                MyBase.SetMessage("E428", New String() {"運賃の課税区分が不正な", "、実績作成", String.Concat("[工場管理番号=", drSendUnchinTarget.Item("KOJO_KANRI_NO"), "]")})
                Return ds
            End If

            '運賃明細送信データの登録
            ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Next

        Return ds

    End Function

#End Region

    'ADD START 2019/05/30 要望管理006030
#Region "印刷フラグ更新処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ運送依頼書印刷フラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateIraishoPrintFlg(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return ds

    End Function

#End Region
    'ADD END   2019/05/30 要望管理006030

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

    ''' <summary>
    ''' 検索処理のエラーメッセージを設定
    ''' </summary>
    ''' <param name="count">件数</param>
    ''' <remarks></remarks>
    Private Sub SetSelectErrMes(ByVal count As Integer)

        'メッセージコードの設定
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

    End Sub

#End Region

End Class
