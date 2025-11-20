' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM090F : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM090フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM090F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMM090H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMM090H)

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
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._Handler.FunctionKey4Press(Me, e)

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
    Private Sub sprCustCellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprCust.CellDoubleClick

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
    Private Sub LMM090F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode.Equals(Keys.Enter) = False Then
            Exit Sub
        End If

        Call Me._Handler.LMM090FKeyDown(Me, e)

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

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 荷主明細用行追加ボタン押下イベント(大)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailAddClickL(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowAddL.Click

        Call Me._Handler.DetailAddLClick(Me, LMM090C.SprDetailObject.CUST_L, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行削除ボタン押下イベント(大)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailDelClickL(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowDelL.Click

        Call Me._Handler.DetailDelLClick(Me, LMM090C.SprDetailObject.CUST_L, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行追加ボタン押下イベント(中)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailAddClickM(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowAddM.Click

        Call Me._Handler.DetailAddLClick(Me, LMM090C.SprDetailObject.CUST_M, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行削除ボタン押下イベント(中)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailDelClickM(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowDelM.Click

        Call Me._Handler.DetailDelLClick(Me, LMM090C.SprDetailObject.CUST_M, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行追加ボタン押下イベント(小)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailAddClickS(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowAddS.Click

        Call Me._Handler.DetailAddLClick(Me, LMM090C.SprDetailObject.CUST_S, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行削除ボタン押下イベント(小)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailDelClickS(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowDelS.Click

        Call Me._Handler.DetailDelLClick(Me, LMM090C.SprDetailObject.CUST_S, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行追加ボタン押下イベント(極小)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailAddClickSS(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowAddSS.Click

        Call Me._Handler.DetailAddLClick(Me, LMM090C.SprDetailObject.CUST_SS, e)

    End Sub

    ''' <summary>
    ''' 荷主明細用行削除ボタン押下イベント(極小)
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnDetailDelClickSS(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailRowDelSS.Click

        Call Me._Handler.DetailDelLClick(Me, LMM090C.SprDetailObject.CUST_SS, e)

    End Sub
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' 編集(大)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditLClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditL.Click

        Call Me._Handler.EditLClick(Me, e)

    End Sub

    ''' <summary>
    ''' 編集(中)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditMClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditM.Click

        Call Me._Handler.EditMClick(Me, e)

    End Sub

    ''' <summary>
    ''' 編集(小)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditSClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditS.Click

        Call Me._Handler.EditSClick(Me, e)

    End Sub

    ''' <summary>
    ''' 編集(極小)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditSSClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSS.Click

        Call Me._Handler.EditSSClick(Me, e)

    End Sub

    ''' <summary>
    ''' 追加(中)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFukushaMClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFukushaM.Click

        Call Me._Handler.FukushaMClick(Me, e)

    End Sub

    ''' <summary>
    ''' 追加(小)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFukushaSClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFukushaS.Click

        Call Me._Handler.FukushaSClick(Me, e)

    End Sub

    ''' <summary>
    ''' 追加(極小)ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFukushaSSClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFukushaSS.Click

        Call Me._Handler.FukushaSSClick(Me, e)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprCust_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprCust.LeaveCell
        Call Me._Handler.sprCustLeaveCell(Me, e)
    End Sub

    ''' <summary>
    ''' セルのLeaveイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprCustPrt_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprCustPrt.LeaveCell

        Call Me._Handler.LeaveCell(Me, e)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region

    Private Sub tpgCustM_Click(sender As Object, e As EventArgs) Handles tpgCustM.Click

    End Sub

    Private Sub cmbHoshoMinKbn_Load(sender As Object, e As EventArgs) Handles cmbHoshoMinKbn.Load

    End Sub

    Private Sub LmTitleLabel2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbInitDateNyuka_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtYokomochiTarifShukka_Load(sender As Object, e As EventArgs) Handles txtYokomochiTarifShukka.Load

    End Sub

    Private Sub cmbInitDateShukka_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub pnlShukka_Enter(sender As Object, e As EventArgs) Handles pnlShukka.Enter

    End Sub

    Private Sub pnlNyuka_Enter(sender As Object, e As EventArgs) Handles pnlNyuka.Enter

    End Sub
End Class
