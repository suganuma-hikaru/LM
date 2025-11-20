' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM100F : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM100フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM100F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMM100H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMM100H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' ファンクション１ボタン押下時およびファンクションキー１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function1Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F1PressEvent

        Call Me._Handler.FunctionKey1Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション２ボタン押下時およびファンクションキー２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function2Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F2PressEvent

        Call Me._Handler.FunctionKey2Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション３ボタン押下時およびファンクションキー３押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function3Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F3PressEvent

        Call Me._Handler.FunctionKey3Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._Handler.FunctionKey4Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function5Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        Call Me._Handler.FunctionKey5Press(Me, e)

    End Sub

    'START YANAI 要望番号372
    ''' <summary>
    ''' ファンクション６ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function6Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F6PressEvent

        Call Me._Handler.FunctionKey6Press(Me, e)

    End Sub
    'END YANAI 要望番号372

    '2015.10.02 他荷主対応START
    ''' <summary>
    ''' ファンクション７ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function7Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F7PressEvent

        Call Me._Handler.FunctionKey7Press(Me, e)

    End Sub
    '2015.10.02 他荷主対応END

    ''' <summary>
    ''' ファンクション７ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function8Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F8PressEvent

        Call Me._Handler.FunctionKey8Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function9Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F9PressEvent

        Call Me._Handler.FunctionKey9Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１０ボタン押下時およびファンクションキー１０押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function10Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F10PressEvent

        Call Me._Handler.FunctionKey10Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１１ボタン押下時およびファンクションキー１１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function11Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F11PressEvent

        Call Me._Handler.FunctionKey11Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function12Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

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

        MyBase.Dispose()

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' ダブルクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprGoodsCellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprGoods.CellDoubleClick

        If e.Row <> 0 Then
            Me._Handler.sprCellDoubleClick(Me, e)
        End If

    End Sub

    ''' <summary>
    ''' キー押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMM100F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode.Equals(Keys.Enter) = False Then
            Exit Sub
        End If

        Call Me._Handler.LMM100FKeyDown(Me, e)

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRowAdd.Click

        Call Me._Handler.AddClick(Me, e)

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRowDel.Click

        Call Me._Handler.DelClick(Me, e)

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Call Me._Handler.PrintClick(Me, e)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprGoods_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprGoods.LeaveCell
        Call Me._Handler.sprGoodsLeaveCell(Me, e)
    End Sub

    ''' <summary>
    ''' 個数単位値変更時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub cmbKosuTani_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKosuTani.SelectedValueChanged
        Call Me._Handler.cmbKosuTani_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' 荷姿単位変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbHosotani_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbHosotani.SelectedValueChanged
        Call Me._Handler.cmbHosotani_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' 高圧ガス区分変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbKouathugas_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbKouathugas.SelectedValueChanged
        Call Me._Handler.cmbKouathugas_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' 消防危険品区分変更時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbShobokiken_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbShobokiken.SelectedValueChanged
        Call Me._Handler.cmbShobokiken_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(幅)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numWidth_Leave(sender As Object, e As EventArgs) Handles numWidth.Leave
        Call Me._Handler.numWidth_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(高さ)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numHeight_Leave(sender As Object, e As EventArgs) Handles numHeight.Leave
        Call Me._Handler.numHeight_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(奥行)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numDepth_Leave(sender As Object, e As EventArgs) Handles numDepth.Leave
        Call Me._Handler.numDepth_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(幅 一括)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numWidthBulk_Leave(sender As Object, e As EventArgs) Handles numWidthBulk.Leave
        Call Me._Handler.numWidthBulk_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(高さ 一括)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numHeightBulk_Leave(sender As Object, e As EventArgs) Handles numHeightBulk.Leave
        Call Me._Handler.numHeightBulk_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' カーソル移動時(奥行 一括)のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub numDepthBulk_Leave(sender As Object, e As EventArgs) Handles numDepthBulk.Leave
        Call Me._Handler.numDepthBulk_Leave(Me, e)
    End Sub

    ''' <summary>
    ''' 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Call Me._Handler.UpdateClick(Me, e)
    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region

    Private Sub blTitleAVAL_YN_Click(sender As Object, e As EventArgs) Handles blTitleAVAL_YN.Click

    End Sub

    Private Sub cmbAVAL_YN_Load(sender As Object, e As EventArgs) Handles cmbAVAL_YN.Load

    End Sub
End Class
