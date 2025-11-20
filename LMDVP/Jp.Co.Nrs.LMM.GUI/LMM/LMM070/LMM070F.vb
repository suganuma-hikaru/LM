' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM070F : 割増運賃マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM070フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM070F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMM070H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMM070H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション１ボタン押下時およびファンクションキー１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function1_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F1PressEvent

        Call Me._Handler.FunctionKey1Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション２ボタン押下時およびファンクションキー２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function2_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F2PressEvent

        Call Me._Handler.FunctionKey2Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション３ボタン押下時およびファンクションキー３押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function3_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F3PressEvent

        Call Me._Handler.FunctionKey3Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._Handler.FunctionKey4Press(Me, e)

    End Sub

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
    ''' ファンクション１０ボタン押下時およびファンクションキー１０押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function10_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F10PressEvent

        Call Me._Handler.FunctionKey10Press(Me, e)

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

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' ダブルクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprDetail_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprDetail.CellDoubleClick

        If e.Row <> 0 Then
            Me._Handler.sprCellDoubleClick(Me, e)
        End If
    End Sub

    ''' <summary>
    ''' キー押下イベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub LMM070F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Call Me._Handler.LMM070F_KeyDown(Me, e)

    End Sub

    ''' <summary>
    '''  冬期割増の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbWintExtcYn_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbWintExtcYn.SelectedValueChanged

        Call Me._Handler.cmbWintExtcYn_SelectedValueChanged(Me, e)

    End Sub

    ''' <summary>
    '''  Jisコードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtJisCd_TextValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJisCd.Leave

        Call Me._Handler.txtJisCd_TextValueChanged(Me, e)

    End Sub


    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprDetail_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprDetail.LeaveCell
        Call Me._Handler.sprDetail_LeaveCell(Me, e)
    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
