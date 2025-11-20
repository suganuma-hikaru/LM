' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG040H : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Win.Base      '2021/06/28 
Imports Jp.Co.Nrs.LM.Utility    '2021/06/28 
Imports System.Text
Imports System.IO

''' <summary>
''' LMG040ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG040G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMGControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMGControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Dim frm As LMG040F = New LMG040F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMGControlG(frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMGControlV(Me, DirectCast(frm, Form))

        'Handler共通クラスの設定
        Me._ControlH = New LMGControlH(DirectCast(frm, Form), "LMG040", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMG040G(Me, frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMG040V(Me, frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'Enter押下イベントの設定
        Call Me._ControlG.SetEnterEvent(frm.sprMeisai)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me.SetBaseMsg(frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length > 0 Then
            'スプレッドのSAP伝票番号を非表示
            frm.sprMeisai.ActiveSheet.Columns(LMG040C.SprColumnIndex.SAP_NO).Visible = False
            'スプレッドのSAP連携ユーザ名を非表示
            frm.sprMeisai.ActiveSheet.Columns(LMG040C.SprColumnIndex.SAP_OUT_USER_NM).Visible = False
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規取込処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowTorikomiEvent(ByVal frm As LMG040F)

        Dim eventShubetu As LMG040C.EventShubetsu = LMG040C.EventShubetsu.SINKI_TORIKOMI

        '画面遷移共通イベント
        Call Me.ShowCreatePage(eventShubetu, frm)

    End Sub

    ''' <summary>
    ''' 新規手書き処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowTegakiEvent(ByVal frm As LMG040F)

        Dim eventShubetu As LMG040C.EventShubetsu = LMG040C.EventShubetsu.SINKI_TEGAKI

        '画面遷移共通イベント
        Call Me.ShowCreatePage(eventShubetu, frm)

    End Sub

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.DELETE) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        '項目チェック
        If Me._V.IsDeleteInputChk(list) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "削除処理") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateDelete")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "UpdateDelete", ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動 
            MyBase.MessageStoreDownload()
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateDelete")

        MyBase.ShowMessage(frm, "G002", New String() {"削除処理", ""})

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KakuteiEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.KAKUTEI) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        '項目チェック
        If Me._V.IsKakuteiInputChk(list) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "確定処理") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateKakutei")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "UpdateKakutei", ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動 
            MyBase.MessageStoreDownload()
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateKakutei")

        MyBase.ShowMessage(frm, "G002", New String() {"確定処理", ""})

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ClearEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.CLEAR) = False Then
            Call Me.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        '項目チェック
        If Me._V.IsClearInputChk(list) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(frm, "初期化処理") = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateClear")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "UpdateClear", ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動 
            MyBase.MessageStoreDownload()
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateClear")

        MyBase.ShowMessage(frm, "G002", New String() {"初期化処理", ""})

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' SAP出力処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SapOutEvent(ByVal frm As LMG040F)

        '外部倉庫用ABP対策
        If String.IsNullOrEmpty(frm.FunctionKey.F7ButtonName) Then
            'ボタン名が未設定ならば使用不可として抜ける
            Return
        End If

        Dim shoriName As String = String.Concat(frm.FunctionKey.F7ButtonName, "処理")

        ' 処理開始アクション
        Call Me.StartAction(frm)

        ' 権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.SAPOUT) = False Then
            Call Me.EndAction(frm)  ' 終了処理
            Exit Sub
        End If

        ' チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        ' 項目チェック
        If Me._V.IsSapOutChk(list) = False Then
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' 処理続行確認
        If Me.ConfirmMsg(frm, String.Concat(shoriName, "を実行")) = False Then
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' DataSet 設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)
        ' 設定した DataSet 内の進捗区分を、更新する値に設定する。
        Dim dt As DataTable = ds.Tables(LMG040C.TABLE_NM_IN)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i).Item("STATE_KB") = LMG040C.STATE_KEIRI_TORIKOMI_ZUMI
        Next

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        ' ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapOut")

        '==========================
        ' WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "SapOut", ds)

        ' メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            Dim cntDt As DataTable = rtnDs.Tables(LMG040C.TABLE_NM_SAP_UPD_CNT)
            Dim sapUpdCnt As String = "0"
            If cntDt.Rows.Count > 0 Then
                sapUpdCnt = cntDt.Rows(0).Item("SAP_UPD_CNT").ToString()
            End If

            ' メッセージ出力（更新件数別制御）
            If sapUpdCnt.Equals("0") Then
                ' 0件の場合、エラーメッセージのみ出力
                MyBase.ShowMessage(frm, "E235")
            Else
                ' N件の場合、更新件数 + エラーメッセージを出力
                MyBase.ShowMessage(frm, "E534", New String() {sapUpdCnt, shoriName})
            End If

            ' Excel 起動 
            Call MyBase.MessageStoreDownload()
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapOut")

        MyBase.ShowMessage(frm, "G002", New String() {shoriName, ""})

        ' 終了処理
        Call Me.EndAction(frm)

        ' フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' SAP取消処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SapCancelEvent(ByVal frm As LMG040F)

        Dim shoriName As String = String.Concat(frm.FunctionKey.F8ButtonName, "処理")

        ' 処理開始アクション
        Call Me.StartAction(frm)

        ' 権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.SAPCANCEL) = False Then
            Call Me.EndAction(frm)  ' 終了処理
            Exit Sub
        End If

        ' チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        ' 項目チェック
        If Me._V.IsSapCancelChk(list) = False Then
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' 処理続行確認
        If Me.ConfirmMsg(frm, String.Concat(shoriName, "を実行")) = False Then
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' DataSet設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)
        ' 設定した DataSet 内の進捗区分を、更新する値に設定する。
        Dim dt As DataTable = ds.Tables(LMG040C.TABLE_NM_IN)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i).Item("STATE_KB") = LMG040C.STATE_INSATU_ZUMI
        Next

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        ' ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapCancel")

        '==========================
        ' WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "SapCancel", ds)

        ' メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            Dim cntDt As DataTable = rtnDs.Tables(LMG040C.TABLE_NM_SAP_UPD_CNT)
            Dim sapUpdCnt As String = "0"
            If cntDt.Rows.Count > 0 Then
                sapUpdCnt = cntDt.Rows(0).Item("SAP_UPD_CNT").ToString()
            End If

            ' メッセージ出力（更新件数別制御）
            If sapUpdCnt.Equals("0") Then
                ' 0件の場合、エラーメッセージのみ出力
                MyBase.ShowMessage(frm, "E235")
            Else
                ' N件の場合、更新件数 + エラーメッセージを出力
                MyBase.ShowMessage(frm, "E534", New String() {sapUpdCnt, shoriName})
            End If

            ' Excel 起動 
            Call MyBase.MessageStoreDownload()
            Call Me.EndAction(frm) ' 終了処理
            Exit Sub
        End If

        ' ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapCancel")

        MyBase.ShowMessage(frm, "G002", New String() {shoriName, ""})

        ' 終了処理
        Call Me.EndAction(frm)

        ' フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 請求データ出力処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SkyuCsvEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.SKYUCSV) = False Then
            '終了処理
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG040G.sprDetailDef.DEF.ColNo)

        '項目チェック
        If Not Me._V.IsSkyuCsvChk(list) Then
            '終了処理
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '処理続行確認
        If Not Me.ConfirmMsg(frm, String.Concat(frm.FunctionKey.F11ButtonName, "処理を実行")) Then
            '終了処理
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG040DS()
        Call Me.SetDataSetInUpdate(frm, ds, list)
        Dim dsUpd As DataSet = ds.Copy()

        'メッセージ情報を初期化
        MyBase.ClearMessageStoreData()

        '出力用データ抽出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SkyuCsvSelect")
        ds = MyBase.CallWSA("LMG040BLF", "SkyuCsvSelect", ds)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SkyuCsvSelect")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist Then
            MyBase.ShowMessage(frm, "S001", New String() {String.Concat(frm.FunctionKey.F11ButtonName, "処理")})
        End If

        'ファイル出力
        Dim ret As Boolean = SkyuCsvOutput(frm, ds)

        'フラグ更新
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SkyuCsvUpdate")
        dsUpd = MyBase.CallWSA("LMG040BLF", "SkyuCsvUpdate", dsUpd)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SkyuCsvUpdate")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist Then
            MyBase.ShowMessage(frm, "S001", New String() {String.Concat(frm.FunctionKey.F11ButtonName, "処理")})
        End If

        '処理完了メッセージ
        MyBase.ShowMessage(frm, "G002", New String() {String.Concat(frm.FunctionKey.F11ButtonName, "処理"), ""})

        '終了処理
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 請求データ出力処理（ファイル出力）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SkyuCsvOutput(ByVal frm As LMG040F, ByVal ds As DataSet) As Boolean

        Const DELIM As String = ","
        Dim dateTime As String = String.Concat(MyBase.GetSystemDateTime(0), Mid(MyBase.GetSystemDateTime(1), 1, 6))

        'CSVファイルの出力パスを取得（優先度：ユーザ毎＞共通）
        Dim filePath As String = String.Empty

        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat(
                "KBN_GROUP_CD = 'C003' ",
                "AND KBN_CD = '12' ",
                "AND KBN_NM3 = '", LMUserInfoManager.GetUserID, "'"))
        If kbnDr.Length = 0 Then
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat(
                "KBN_GROUP_CD = 'C003' ",
                "AND KBN_CD = '12' ",
                "AND KBN_NM3 = ''"))
            If kbnDr.Length = 0 Then
                MyBase.ShowMessage(frm, "E493", {"CSV出力情報", "区分マスタ", "(C003-12)"})
                Return False
            Else
                filePath = kbnDr(0).Item("KBN_NM1").ToString
            End If
        Else
            filePath = kbnDr(0).Item("KBN_NM1").ToString
        End If

        '抽出データからファイル出力の単位となるキー（請求書番号）を取得する
        Dim dtKey As DataTable = ds.Tables("LMG040CSVOUT").DefaultView.ToTable(True, "SKYU_NO")

        'キー情報のループ
        For i As Integer = 0 To dtKey.Rows.Count - 1
            '請求書番号
            Dim skyuNo As String = dtKey(i).Item("SKYU_NO").ToString

            '抽出データを請求書番号でフィルタ
            Dim rows As DataRow() = ds.Tables("LMG040CSVOUT").Select(String.Concat("SKYU_NO = '", skyuNo, "'"))

            '出力内容編集
            Dim outData As StringBuilder = New StringBuilder()
            outData.Append("LMS請求番号,請求日,請求先コード,摘要,品名コード,品名,請求額,課税区分,税率,支払期日")
            outData.Append(vbNewLine)
            For Each row As DataRow In rows
                outData.Append(SetDblQuotation(row("SKYU_NO").ToString))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("SKYU_DATE").ToString))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("SEIQTO_CD").ToString))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("TEKIYO").ToString))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("SEIQKMK_CD").ToString))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("SEIQKMK_NM").ToString))
                outData.Append(DELIM)

                Dim skyuGk As Decimal = Convert.ToDecimal(row("SKYU_GK").ToString)
                Dim taxKb As String = row("TAX_KB").ToString()
                Dim taxRate As Decimal = Convert.ToDecimal(row("TAX_RATE").ToString)

                If "1".Equals(taxKb) Then
                    '内税の明細の場合、明細の金額は税抜き計算を行う
                    '鑑編集画面(LMG050)と同様に、税額を小数点以下切り捨てで求めて請求額から引く
                    Dim taxGk As Decimal = Math.Floor(skyuGk * taxRate / (taxRate + 1))
                    skyuGk = skyuGk - taxGk
                End If

                outData.Append(SetDblQuotation(skyuGk.ToString("0")))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(taxKb))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(taxRate.ToString("0.000")))
                outData.Append(DELIM)
                outData.Append(SetDblQuotation(row("SHIHARAI_DATE").ToString))
                outData.Append(vbNewLine)
            Next

            'ファイル名の編集
            Dim fileName As String = String.Concat(skyuNo, "_", dateTime, ".csv")

            '文字コード
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

            'ファイルを開く
            System.IO.Directory.CreateDirectory(filePath)
            Dim sr As StreamWriter = New StreamWriter(String.Concat(filePath, "\", fileName), False, enc)

            '値の書き込み
            sr.Write(outData.ToString())

            'ファイルを閉じる
            sr.Close()
        Next

        Return True

    End Function

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.KENSAKU) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        '荷主名称・請求先名称の設定
        Call Me._G.SetSeqName()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMG040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.MST_SANSHO) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMG040C.EventShubetsu.MST_SANSHO) = False Then
            Call Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.SetReturnSeiqtoPop(frm, LMG040C.EventShubetsu.MST_SANSHO)

        'メッセージの表示
        Call Me.SetBaseMsg(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprDoubleClick(ByVal frm As LMG040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim eventShubetu As LMG040C.EventShubetsu = LMG040C.EventShubetsu.DOUBLE_CLICK

        '選択行の請求先番号を取得
        Dim skyuNo As String = Me._ControlV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(e.Row, LMG040G.sprDetailDef.SEIKYU_NO.ColNo))

#If True Then       'ADD 2018/08/10 依頼番号 : 002136  
        '選択行の削除フラグを取得
        Dim sysDelFlg As String = Me._ControlV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(e.Row, LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo))

#End If
        If sysDelFlg.Equals(LMConst.FLG.OFF) = True Then           ''ADD 2018/08/10依頼番号 : 002136  
            'データ削除チェックを行う
            If Me.ExistData(frm, skyuNo) = False Then
                Exit Sub
            End If
        End If


        '画面遷移共通イベント
        Call Me.ShowCreatePage(eventShubetu, frm, skyuNo, sysDelFlg)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG040C.EventShubetsu.ENTER) = False Then
            Me._ControlG.SetNextControl(frm.sprMeisai)  'タブ移動処理
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMG040C.EventShubetsu.ENTER) = False Then
            Me._ControlG.SetNextControl(frm.sprMeisai)  'タブ移動処理
            Exit Sub
        End If

        '未入力の場合、処理終了
        If String.IsNullOrEmpty(frm.txtSeikyuCd.TextValue) Then
            frm.lblSeikyuNm.TextValue = String.Empty
            Me._ControlG.SetNextControl(frm.sprMeisai)  'タブ移動処理
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.SetReturnSeiqtoPop(frm, LMG040C.EventShubetsu.ENTER)

        'タブ移動処理
        Me._ControlG.SetNextControl(frm.sprMeisai)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMG040F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMG040F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 画面遷移処理
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub ShowCreatePage(ByVal eventShubetsu As LMG040C.EventShubetsu _
                               , ByVal frm As LMG040F _
                               , Optional ByVal skyuNo As String = "" _
                               , Optional ByVal sysDelFlg As String = "0")

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            Exit Sub
        End If

        'パラメータ生成
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMG050DS()
        Dim prmDt As DataTable = prmDs.Tables(LMG040C.SHOW_PAGE)
        Dim prmDr As DataRow = prmDt.NewRow

        prmDr.Item("SKYU_NO") = skyuNo

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        prmDr.Item("LANG_FLG") = lgm.MessageLanguage()

        Select Case eventShubetsu
            Case LMG040C.EventShubetsu.SINKI_TORIKOMI
                prmDr.Item("CRT_KB") = LMGControlC.CRT_TORIKOMI
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'prmDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
                prmDr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
                '要望番号:1935 yamanaka 2013.03.08 Start
                prmDr.Item("SEIQTO_CD") = frm.txtSeikyuCd.TextValue
                prmDr.Item("SKYU_DATE") = frm.imdSeikyuYm.TextValue
                '要望番号:1935 yamanaka 2013.03.08 End

            Case LMG040C.EventShubetsu.SINKI_TEGAKI
                prmDr.Item("CRT_KB") = LMGControlC.CRT_TEGAKI
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'prmDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
                prmDr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
                '要望番号:1935 yamanaka 2013.03.08 Start
                prmDr.Item("SEIQTO_CD") = frm.txtSeikyuCd.TextValue
                prmDr.Item("SKYU_DATE") = frm.imdSeikyuYm.TextValue
                '要望番号:1935 yamanaka 2013.03.08 End

            Case LMG040C.EventShubetsu.DOUBLE_CLICK
                prmDr.Item("CRT_KB") = String.Empty

#If True Then       'ADD 2018/08/10 依頼番号 : 002136  
                If sysDelFlg.Equals(LMConst.FLG.ON) Then
                    '削除データ表示のとき
                    prmDr.Item("CRT_KB") = "99"

                End If
                prmDr.Item("SYS_DEL_FLG") = sysDelFlg.ToString
#End If
                prmDr.Item("NRS_BR_CD") = frm.lblBrCd.TextValue

        End Select

        prmDt.Rows.Add(prmDr)
        prm.ParamDataSet = prmDs

        '請求書作成(LMG050)を開く
        LMFormNavigate.NextFormNavigate(Me, "LMG050", prm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMG040F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble(
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02' AND SYS_DEL_FLG = '0'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble(
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)


        'DataSet設定
        Me._FindDs = New LMG040DS()
        Call SetDataSetInData(frm)

        'SPREAD(表示行)初期化
        frm.sprMeisai.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "SelectListData", Me._FindDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then     'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA("LMG040BLF", "SelectListData", Me._FindDs)

                    '検索成功時共通処理を行う
                    Call Me.SuccessSelect(frm, rtnDs)

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時共通処理を行う
                    Call Me.FailureSelect(frm)
                End If
            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時共通処理を行う
                Call Me.FailureSelect(frm)
            End If
        Else

            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMG040F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMG040C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '隠し項目に値設定
        Call Me._G.SetHead(dt.Rows(0))

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = MyBase.GetResultCount.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMG040F)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 請求先マスタ照会
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetReturnSeiqtoPop(ByVal frm As LMG040F, ByVal eventshubetsu As LMG040C.EventShubetsu)

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        '営業所が入力されている場合：画面項目、されていない場合：ログインユーザの営業所
        Dim br As String = frm.cmbBr.SelectedValue.ToString()
        If String.IsNullOrEmpty(br) Then

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'br = LMUserInfoManager.GetNrsBrCd
            br = frm.cmbBr.SelectedValue.ToString()

        End If

        With dr
            .Item("NRS_BR_CD") = br
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMG040C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("SEIQTO_CD") = frm.txtSeikyuCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'パラメータ設定 
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        '******* 返却パラメータの設定 *******
        If prm.ReturnFlg = True Then

            Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            '戻り値の設定
            frm.txtSeikyuCd.TextValue = rtnDr.Item("SEIQTO_CD").ToString()
            frm.lblSeikyuNm.TextValue = rtnDr.Item("SEIQTO_NM").ToString()

        End If

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal frm As LMG040F, ByVal msg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetBaseMsg(ByVal frm As LMG040F)

        If frm.sprMeisai.ActiveSheet.Rows.Count = 1 Then

            '検索が行われていない場合
            MyBase.ShowMessage(frm, "G007")
        Else
            '検索が行われている場合
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
        End If

    End Sub

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ExistData(ByVal frm As LMG040F, ByVal skyuNo As String) As Boolean

        Dim rtnResult As Boolean = False

        'DataSet設定
        Me._FindDs = New LMG040DS()
        Dim dr As DataRow = Me._FindDs.Tables(LMG040C.TABLE_NM_IN).NewRow()
        dr.Item("SKYU_NO") = skyuNo
        dr.Item("NRS_BR_CD") = frm.lblBrCd.TextValue
        Me._FindDs.Tables(LMG040C.TABLE_NM_IN).Rows.Add(dr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExistData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG040BLF", "ExistData", Me._FindDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)
            rtnResult = False

        Else
            rtnResult = True

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExistData")

        Return rtnResult

    End Function

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMG040F)

        Dim dr As DataRow = Me._FindDs.Tables(LMG040C.TABLE_NM_IN).NewRow()

        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue
        dr.Item("SKYU_MONTH") = Left(frm.imdSeikyuYm.TextValue, 6)
        dr.Item("SEIQTO_CD") = frm.txtSeikyuCd.TextValue
        dr.Item("SKYU_NO") = frm.txtSeikyuNo.TextValue
        dr.Item("MIKAKUTEI_FLG") = frm.chkMikakutei.GetBinaryValue()
        dr.Item("KAKUTEI_FLG") = frm.chkKakutei.GetBinaryValue()
        dr.Item("PRINT_FLG") = frm.chkInsatuZumi.GetBinaryValue()
        dr.Item("KEIRI_FLG") = frm.chkKeiriTorikomi.GetBinaryValue()
        dr.Item("KEIRI_TAISHOGAI_FLG") = frm.chkKeiriTorikomiTaishoGai.GetBinaryValue()
        dr.Item("KEIRI_MODOSHI_FLG") = frm.chkKeiriTorikeshi.GetBinaryValue()     'ADD 2018/08/09 依頼番号 : 002136  
        dr.Item("SKYU_CSV_FLG") = frm.chkSkyuCsvFlg.GetBinaryValue()

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        dr.Item("LANG_FLG") = lgm.MessageLanguage()

        With frm.sprMeisai.ActiveSheet

            dr.Item("SEIQTO_NM") = Me._ControlV.GetCellValue(.Cells(0, LMG040G.sprDetailDef.SEIKYU_NM.ColNo))
            dr.Item("CRT_KB") = Me._ControlV.GetCellValue(.Cells(0, LMG040G.sprDetailDef.CREATE_SYUBETU_NM.ColNo))
            dr.Item("RB_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMG040G.sprDetailDef.AKAKURO_NM.ColNo))
            dr.Item("SKYU_NO_RELATED") = Me._ControlV.GetCellValue(.Cells(0, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo))

            Me._FindDs.Tables(LMG040C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(確定対象データ格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">更新内容を格納するデータセット</param>
    ''' <param name="list">更新対象行を格納しているリスト</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInUpdate(ByVal frm As LMG040F _
                                   , ByVal ds As DataSet _
                                   , ByVal list As ArrayList)

        Dim dr As DataRow = Nothing
        Dim dt As DataTable = ds.Tables(LMG040C.TABLE_NM_IN)
        Dim rowIndex As Integer = 0

        Dim max As Integer = list.Count - 1
        For i As Integer = 0 To max

            rowIndex = Convert.ToInt32(list(i))

            dr = dt.NewRow()

            With frm.sprMeisai.ActiveSheet

                dr.Item("SKYU_NO") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.SEIKYU_NO.ColNo))
                dr.Item("SEIQTO_CD") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.SEIKYU_CD.ColNo))
                dr.Item("SKYU_DATE") = DateFormatUtility.DeleteSlash(Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo)))
                dr.Item("UNCHIN_IMP_FROM_DATE") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.UNCHIN_INV_FROM.ColNo))
                dr.Item("SAGYO_IMP_FROM_DATE") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.SAGYO_INV_FROM.ColNo))
                dr.Item("YOKOMOCHI_IMP_FROM_DATE") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.YOKOMOCHI_INV_FROM.ColNo))
                dr.Item("CRT_KB") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.CREATE_SYUBETU_KB.ColNo))
                dr.Item("SKYU_NO_RELATED") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo))
                dr.Item("RECORD_NO") = rowIndex
                'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
                dr.Item("STATE_KB") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.STATE_KB.ColNo))
                'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
                'ADD START 2023/04/10 依頼番号:036535
                Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
                dr.Item("LANG_FLG") = lgm.MessageLanguage()
                'ADD END 2023/04/10 依頼番号:036535

                '更新時共通項目
                dr.Item("NRS_BR_CD") = frm.lblBrCd.TextValue
                dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(rowIndex, LMG040G.sprDetailDef.UPD_TIME.ColNo))

            End With

            dt.Rows.Add(dr)

        Next

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規取込)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShowTorikomiEvent")

        '新規取込
        Call Me.ShowTorikomiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShowTorikomiEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(新規手書き)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShowTegakiEvent")

        '新規手書き
        Call Me.ShowTegakiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShowTegakiEvent")

    End Sub

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' F4押下時処理呼び出し(削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteEvent")

        '削除
        Call Me.DeleteEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteEvent")

    End Sub
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' F5押下時処理呼び出し(確定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "KakuteiEvent")

        '確定
        Call Me.KakuteiEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "KakuteiEvent")

    End Sub

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' F7押下時処理呼び出し(初期化)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        ' UPD 2021/04/08 依頼番号 : 019742
        'MyBase.Logger.StartLog(MyBase.GetType.Name, "ClearEvent")
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapOutEvent")

        ' UPD S 2021/04/08 依頼番号 : 019742
        '''''初期化
        ''''Call Me.ClearEvent(frm)
        ' SAP出力
        Call Me.SapOutEvent(frm)
        ' UPD E 2021/04/08 依頼番号 : 019742

        ' UPD 2021/04/08 依頼番号 : 019742
        'MyBase.Logger.EndLog(MyBase.GetType.Name, "ClearEvent")
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapOutEvent")

    End Sub
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' F8押下時処理呼び出し(初期化)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapCancelEvent")

        ' SAP取消
        Call Me.SapCancelEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapCancelEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListEvent")

        '検索
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        'マスタ参照
        Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    Friend Sub FunctionKey11Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SkyuCsvEvent")

        '請求データ出力
        Me.SkyuCsvEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SkyuCsvEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG040F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMG040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprDoubleClick(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMG040FKeyDown(ByVal frm As LMG040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMG040F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMG040F_KeyDown")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================
#End Region

#End Region

End Class