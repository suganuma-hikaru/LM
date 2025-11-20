' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI380  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI380フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI380F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMI380H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付ける</remarks>
    Public Sub New(ByVal handlerClass As LMI380H)

        MyBase.New()

        'この呼び出しは、Windows フォームデザイナで必要
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付ける
        Me._Handler = handlerClass

    End Sub

#End Region

#Region "Method"
    ''' <summary>
    ''' ファンクション１０ボタン押下時およびファンクションキー１０押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function10Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F10PressEvent

        Call Me._Handler.FunctionKey10Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function12Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

        Call Me._Handler.FunctionKey12Press(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs)

        Call Me._Handler.ClosingForm(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じた後に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks>Navigateクラスのインスタンス管理から登録解除を行う</remarks>
    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        LMFormNavigate.Revoke(Me._Handler)

        Call MyBase.Dispose()

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' キーダウンイベントです
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMI380F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then

            Dim controlNm As String = Me.FocusedControlName()

            '荷主コード入力コントロールでEnterキーを押下時
            Select Case controlNm

                Case "txtCustCdL" '荷主コード(大)
                    If String.IsNullOrEmpty(Me.txtCustCdL.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmL.TextValue = String.Empty
                        Me.lblCustNmM.TextValue = String.Empty
                    End If

                Case "txtCustCdM" '荷主コード(中)
                    If String.IsNullOrEmpty(Me.txtCustCdM.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                    Else
                        Me.lblCustNmM.TextValue = String.Empty
                    End If


            End Select

            'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    ''' <summary>
    ''' 実行押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnJikko_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikko.Click

        Call Me._Handler.btnJikko_Click(Me)

    End Sub

    ''' <summary>
    ''' 全営業所選択時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub optAllNrsBr_Check(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._Handler.opt_Check(Me)

    End Sub

    ''' <summary>
    ''' 各営業所選択時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub optOneNrsBr_Check(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOneNrsBr.Click

        Call Me._Handler.opt_Check(Me)

    End Sub

    ''' <summary>
    ''' 各荷主選択時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub optOneCust_Check(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOneCust.Click

        Call Me._Handler.opt_Check(Me)

    End Sub

    ''' <summary>
    ''' 実績作成済選択時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub optAllJisseki_Check(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllJisseki.Click

        Call Me._Handler.opt_Check(Me)

    End Sub

    ''' <summary>
    ''' 実績作成済選択時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub optOutkaPlanJisseki_Check(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOutkaPlanJisseki.CheckedChanged

        Call Me._Handler.opt_Check(Me)

    End Sub
    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region
   

End Class
