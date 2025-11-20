' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMA     : メニュー
'  プログラムID     :  LMI010H : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI010G


    Private _ConG As LMIControlG

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        'フォームの作成
        Dim frm As LMI010F = New LMI010F(Me)

        Me._ConG = New LMIControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMI010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMI010G(Me, frm, Me._ConG)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(False)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '↓ データ取得の必要があればここにコーディングする。
        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Me.ShowMessage(frm, "G051")

        '画面の入力項目の制御
        Call _G.SetControlsStatus(False)

        ' 荷主名コンボボックスの選択肢表示制御
        Call _G.SetComboBoxCustNm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の遷移イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectionEvent(ByVal frm As LMI010F)

        '処理開始アクション
        Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI010C.EventShubetsu.SENTAKU) = False Then
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub
        End If

        '単項目チェック
        If Me._V.IsInputCheck() = False Then
            Me.ShowMessage(frm, "E199", New String() {"処理内容"})
            '処理終了アクション
            Me.EndAction(frm)
            Exit Sub
        End If

        'コンボボックスより区分CDを取得
        Dim KbnCd As String = frm.CmbShori.SelectedValue.ToString()

        '画面IDの取得
        Dim formId As String = Me.getShori(KbnCd)

        '画面遷移用空パラメータ作成
        Dim prm As LMFormData = New LMFormData()

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, formId, prm)

        '処理終了アクション
        Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' コンボボックスチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeCombobBox(ByVal frm As LMI010F)

        '処理開始アクション
        Me.StartAction(frm)

        If frm.CmbCustNm.SelectedValue().Equals("") Then
            Me._G.ClearComboBoxShori()

            '画面の入力項目の制御
            Call _G.SetControlsStatus(False)

            'ファンクションキーの設定
            Call _G.SetFunctionKey(False)

            'メッセージの表示
            Me.ShowMessage(frm, "G051")

            '処理終了アクション
            Me.EndAction(frm)

            Exit Sub
        End If
        Dim CustNm As String = frm.CmbCustNm.SelectedValue.ToString()

        If CustNm.Length = 1 Then
            CustNm = String.Concat("0", CustNm)
        End If

        Me._G.SetComboBoxShori(CustNm)

        'メッセージの表示
        Me.ShowMessage(frm, "G006")

        '処理終了アクション
        Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI010F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FormSelection")

        Call Me.SelectionEvent(frm)

        Logger.EndLog(Me.GetType.Name, "FormSelection")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI010F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' コンボボックスチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ChangeCombo(ByRef frm As LMI010F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeCombobBox")

        Me.ChangeCombobBox(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "内部Method"

    ''' <summary>
    ''' 画面ID取得
    ''' </summary>
    ''' <param name="kbncd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getShori(ByVal kbncd As String) As String

        '画面名より画面IDを取得する
        Dim sql As String = String.Concat("KBN_GROUP_CD = 'N019' ", " AND KBN_CD = '", kbncd, "' ")

        'キャッシュの検索
        Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)

        '画面IDの取得
        Return dr(0).Item(New String() {"KBN_NM3"}(0)).ToString()

    End Function

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '内部Method

#End Region 'Method

End Class