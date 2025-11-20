' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB    : 入荷
'  プログラムID     :  LMB070 : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMB070フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB070F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMB070H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMB070H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function9_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F9PressEvent

        Call Me._Handler.FunctionKey9Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１１ボタン押下時およびファンクションキー１１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function11_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F11PressEvent

        Call Me._Handler.FunctionKey11Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function12_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

        Call Me._Handler.FunctionKey12Press(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing


        If Me.Owner Is Nothing = False Then

            Me.Owner.Activate()
            Me.Hide()
            Me.Owner.Update()

        End If

        Me._Handler.ClosingForm(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じた後に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks>Navigateクラスのインスタンス管理から登録解除します。</remarks>
    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        LMFormNavigate.Revoke(Me._Handler)

        Me.Dispose()

    End Sub

    ''' <summary>
    ''' Downloadボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDownLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click

        Call Me._Handler.btnDownLoad_Click(Me, e)

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMB070F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If (Keys.Enter).Equals(e.KeyCode) = False Then
            'Enter押下イベント以外は終了
            Exit Sub
        End If

        If Me.ActiveControl.Enabled = False Then
            'アクティブコントロールがロックの場合

            'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            Exit Sub
        End If

        'Tabキーが押された時と同じ動作をする。
        Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' 編集ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._Handler.btnEditClick(Me, sender, e)

    End Sub

    ''' <summary>
    ''' 保存ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._Handler.btnSaveClick(Me, sender, e)

    End Sub

    ''' <summary>
    ''' 全て選択ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAllSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._Handler.btnAllSelClick(Me, sender, e)

    End Sub

    ''' <summary>
    ''' 全てを表示ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAllDisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._Handler.btnAllDispClick(Me, sender, e)

    End Sub

    ''' <summary>
    ''' 画像クリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Friend Sub PictureBox_Click(ByVal sender As Object, ByVal e As EventArgs)

        Call Me._Handler.picThumbnailClick(Me, sender, e)

    End Sub

    ''' <summary>
    ''' 画像ダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Friend Sub PictureBox_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)

        Call Me._Handler.picThumbnailDoubleClick(Me, sender, e)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
