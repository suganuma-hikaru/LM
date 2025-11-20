' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMD     : 
'  プログラムID     :  LMD020F : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMD020フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD020F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMD020H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMD020H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション６ボタン押下時およびファンクションキー６押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function6_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F6PressEvent

        Call Me._Handler.FunctionKey6Press(Me, e)

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
    ''' Enter押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMD020F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Me._Handler.EnterKeyDown(Me, e)

    End Sub

    ''' <summary>
    ''' ◀ボタン押下時時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSprMoveLeft_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSprMoveLeft.Click

        Me._Handler.btnSprMoveLeft_Click(Me)

    End Sub

    ''' <summary>
    ''' ▶ボタン押下時時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSprMoveRight_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSprMoveRight.Click

        Me._Handler.btnSprMoveRight_Click(Me)

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下時時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLineDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLineDel.Click

        Me._Handler.btnLineDel_Click(Me)

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下時時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLineAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLineAdd.Click

        Me._Handler.btnLineAdd_Click(Me)

    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下時時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllChange.Click

        Me._Handler.btnAllChange_Click(Me)

    End Sub

    ''' <summary>
    ''' 営業所選択時
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbNrsBrCd_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNrsBrCd.SelectedValueChanged

        Call Me._Handler.cmbNrsBrCd_Change(Me)

    End Sub

    ''' <summary>
    ''' 移動元スプレッドシート縦スクロールイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprMoveBefor_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles sprMoveBefor.TopChange

        Call Me._Handler.sprMoveBefor_TopChange(Me, e)

    End Sub

    ''' <summary>
    ''' 移動先スプレッドシート縦スクロールイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprMoveAfter_TopChange(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs) Handles sprMoveAfter.TopChange

        Call Me._Handler.sprMoveAfter_TopChange(Me, e)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

    ''' <summary>
    ''' 移動元スプレッドのチェックボックスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprMoveBefor_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles sprMoveBefor.ButtonClicked
        If e.Column = LMD020C.SprColumnIndexMoveBefor.DEF Then
            Call Me._Handler.spdMoveBefor_Change(Me)
        End If
    End Sub

    ''' <summary>
    ''' ラジオボタン（複数移動）チェックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub optFukusuIdo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFukusuIdo.CheckedChanged

        Call Me._Handler.optFukusuIdo_CheckedChanged(Me)

    End Sub

    ''' <summary>
    ''' ラジオボタン（強制出庫）チェックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub optKyoseiShuko_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optKyoseiShuko.CheckedChanged

        Call Me._Handler.optKyoseiShuko_CheckedChanged(Me)

    End Sub

    ''' <summary>
    ''' 移動元スプレッドドラッグイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>ドラッグ範囲内のチェックボックスの設定と、移動先スプレッドの設定</remarks>
    Private Sub sprMoveBefor_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sprMoveBefor.MouseUp

        Call Me._Handler.sprMoveBefor_MouseUp(Me)

    End Sub

    Private Sub sprMoveAfter_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sprMoveAfter.MouseUp

        Call Me._Handler.sprMoveAfter_MouseUp(Me)

    End Sub

    ''' <summary>
    ''' 荷主(大)コードのフォーカスが移動したときイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCustCdL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustCdL.Leave

        ' 荷主(大)コードのフォーカスが移動
        Call Me._Handler.txtCustCdL_Leave(Me)

    End Sub

End Class
