' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  GTA010F : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMA010フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA010F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMA010H

    ''' <summary>
    ''' 画面クローズの種類を格納します
    ''' </summary>
    ''' <remarks>True:プロセスごと強制シャットダウン Fasle:通常のクローズ </remarks>
    Private _CloseShutDown As Boolean

    Friend Property CloseShutDown() As Boolean
        Get
            Return _CloseShutDown
        End Get
        Set(ByVal value As Boolean)
            _CloseShutDown = value
        End Set
    End Property

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMA010H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

        'クローズフラグの初期化(初期値シャットダウンする)
        Me._CloseShutDown = True

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' フォームが閉じる前に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

        Me._Handler.ClosingForm(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じた後に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks>Navigateクラスのインスタンス管理から登録解除します。</remarks>
    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '閉じるイベントに応じ処理を振り分ける
        'ログインにて画面遷移する際に閉じる場合
        If Me._CloseShutDown = False Then
            LMFormNavigate.Revoke(Me._Handler)
            Me.Dispose()
        Else
            'キャンセル、×ボタンで画面を閉じる場合プロセスを全てシャットダウンする
            Me._Handler.ShutDownLauncherProcesses()
        End If

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' ＯＫボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Me._Handler.btnOkClick(Me, e)

    End Sub

    ''' <summary>
    ''' キャンセルボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me._Handler.btnCancelClick(Me, e)

    End Sub

    ''' <summary>
    ''' キーダウンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>エンターキーのみキャッチする</remarks>
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Enter Then

            If LMA010C.FORM_PATTERN_LOGIN.Equals(Me._Handler.FormPattern) = True Then

                If Me.ActiveControl.Name = "txtPassword" _
                    Or Me.ActiveControl.Name = "txtRePassword" Then

                    Call Me._Handler.btnOkClick(Me, New EventArgs)

                End If

                e.Handled = True

            End If

        End If

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region

    ''' <summary>
    ''' メッセージ言語選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub optLang_CheckedChanged(sender As Object, e As EventArgs) Handles optJp.CheckedChanged, optEn.CheckedChanged, optKo.CheckedChanged, optCn.CheckedChanged

        Select Case True
            Case optJp.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_JP)
            Case optEn.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_EN)
            Case optKo.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_KO)
            Case optCn.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_CN)
            Case Else
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_EN)
        End Select

    End Sub

End Class
