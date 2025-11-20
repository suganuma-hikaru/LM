'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ010F : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  Shinohara
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMJ010フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMJ010F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMJ010H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMJ010H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._H = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション７ボタン押下時およびファンクションキー８押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function7_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F7PressEvent

        Call Me._H.FunctionKey7Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１０ボタン押下時およびファンクションキー１０押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function10_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F10PressEvent

        Call Me._H.FunctionKey10Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function12_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

        Call Me._H.FunctionKey12Press(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じた後に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks>Navigateクラスのインスタンス管理から登録解除します。</remarks>
    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        LMFormNavigate.Revoke(Me._H)

        MyBase.Dispose()

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' 処理内容変更時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbShori_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShori.SelectedValueChanged
        Call Me._H.cmbShori_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' 請求日付コンボ変更時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbSeiqComb_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSeiqComb.SelectedValueChanged
        Call Me._H.cmbSeiqComb_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' キー押下イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMJ010F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Call Me._H.LMJ010F_KeyDown(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主コード(大)フォーカスインイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCdL_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdL.Enter
        Call Me._H.txtCustCdL_Enter(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主コード(大)フォーカスアウトイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCdL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdL.Leave
        Call Me._H.txtCustCdL_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)フォーカスインイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCdM_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdM.Enter
        Call Me._H.txtCustCdM_Enter(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)フォーカスアウトイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCdM_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdM.Leave
        Call Me._H.txtCustCdM_Leave(Me, e)
    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class