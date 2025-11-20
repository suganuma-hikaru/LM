'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD070F : 在庫帳票印刷
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMD070フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD070F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMD070H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMD070H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"



    ''' <summary>
    ''' ファンクション７ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function7_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F7PressEvent

        Call Me._Handler.FunctionKey7Press(Me, e)

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
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs)

        '編集処理以外の場合画面を閉じる。
        If Me._Handler.CloseForm(Me) = False Then
            e.Cancel = True
            Exit Sub
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

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' キーダウンイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMD070F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'KeyDownのイベントへ
        Call Me._Handler.KeyDown(Me, e)
    End Sub

    ''' <summary>
    ''' 印刷コンボボックス変更時のイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbPrint_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrint.SelectedValueChanged
        '印刷コンボボックス変更時
        Call Me._Handler.Print(Me, e)
    End Sub

    ''' <summary>
    ''' 印刷コンボボックス(サブ)変更時のイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbPrintSub_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrintSub.SelectedValueChanged
        '印刷コンボボックス変更時
        Call Me._Handler.Print(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主(大)コードにフォーカスが移動したときイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCD_L_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdL.Enter
        Call Me._Handler.Got(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主(中)コードにフォーカスが移動したときイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCD_M_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdM.Enter
        Call Me._Handler.Got(Me, e)
    End Sub

    ''' <summary>
    ''' 荷主(大)コードのフォーカスが移動したときイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCD_L_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdL.Leave

        ' 荷主(大)コードのフォーカスが移動
        Call Me._Handler.Lost(Me, e)

    End Sub

    ''' <summary>
    ''' 荷主(中)コードのフォーカスが移動したときイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtCustCD_M_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdM.Leave

        ' 荷主(中)コードのフォーカスが移動
        Call Me._Handler.Lost(Me, e)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method


End Class