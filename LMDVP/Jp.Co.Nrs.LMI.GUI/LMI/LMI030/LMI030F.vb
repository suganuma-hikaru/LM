' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030F : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI030Fフォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI030F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI030H

#End Region 'Field
#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMI030H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._H = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション８ボタン押下時およびファンクションキー８押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function8_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F8PressEvent

        Call Me._H.FunctionKey8Press(Me, e)

    End Sub
    ''' <summary>
    ''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function9_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F9PressEvent

        Call Me._H.FunctionKey9Press(Me, e)

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
    ''' フォームが閉じる前に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing

        Call Me._H.ClosingForm(Me, e)

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
    ''' keyDownイベント（主にenter処理）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMC010F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim controlNm As String = Me.FocusedControlName()

            '荷主コード入力コントロールでEnterキーを押下時
            Select Case controlNm

                Case "txtCustCdL" '荷主コード(大)
                    If String.IsNullOrEmpty(Me.txtCustCdL.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._H.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmL.TextValue = String.Empty
                        Me.lblCustNmM.TextValue = String.Empty
                        Me.lblCustNmS.TextValue = String.Empty
                        Me.lblCustNmSS.TextValue = String.Empty
                    End If

                Case "txtCustCdM" '荷主コード(中)
                    If String.IsNullOrEmpty(Me.txtCustCdM.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._H.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmM.TextValue = String.Empty
                        Me.lblCustNmS.TextValue = String.Empty
                        Me.lblCustNmSS.TextValue = String.Empty
                    End If

                Case "txtCustCdS" '荷主コード(小)
                    If String.IsNullOrEmpty(Me.txtCustCdS.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._H.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmS.TextValue = String.Empty
                        Me.lblCustNmSS.TextValue = String.Empty
                    End If

                Case "txtCustCdSS" '荷主コード(極小)
                    If String.IsNullOrEmpty(Me.txtCustCdSS.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._H.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmSS.TextValue = String.Empty
                    End If

            End Select

            'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

    End Sub

    ''' <summary>
    ''' 作成押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMake.Click

        Call Me._H.btnMake_Click(Me)

    End Sub

    ''' <summary>
    ''' 印刷押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Call Me._H.btnPrint_Click(Me)

    End Sub
    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region

End Class