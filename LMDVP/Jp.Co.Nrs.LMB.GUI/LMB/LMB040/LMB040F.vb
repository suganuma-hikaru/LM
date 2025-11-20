' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB    : 入荷
'  プログラムID     :  LMB040 : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMB040フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB040F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMB040H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMB040H)

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

    '追加開始 2015.05.14 要望番号2292
    ''' <summary>
    ''' 行削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnRowDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowDel.Click

        Call Me._Handler.btnRowDel_Click(Me, e)

    End Sub
    '追加終了 2015.05.14 要望番号2292

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    Private Sub sprWarning_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprDetail.CellDoubleClick

        Call Me._Handler.sprCellDoubleClick(Me, e)

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMB040F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

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

        'value値の設定
        Dim value As String = String.Empty
        Dim txtCtl As Win.InputMan.LMImTextBox = Nothing
        Select Case Me.ActiveControl.Name

            Case "txtSAGYO_USER_CD"
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    lblSAGYO_USER_NM.TextValue = String.Empty

                    imdSysEntDate.Focus()
                End If
                Call Me._Handler.SAGYO_USER_Enter(Me)
                Exit Sub
            Case Else

        End Select

        'Tabキーが押された時と同じ動作をする。
        Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
