' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMD     : 
'  プログラムID     :  LMD040F : 
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMD040フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD040F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMD040H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMD040H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function5_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        Call Me._Handler.FunctionKey5Press(Me, e)

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
    ''' 現在庫スプレッドのチェックボックスクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprGenzaiko_ButtonClicked(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles sprGenzaiko.ButtonClicked

        If e.Row > 0 AndAlso e.Column = LMD040C.SprColumnIndex.DEF Then
            Call Me._Handler.SetSprCheckBox(Me, e)
        End If

    End Sub

    ''' <summary>
    ''' 現在庫スプレッドDragイベントキャンセル
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprMoveAfter_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sprGenzaiko.MouseUp

        Call Me._Handler.sprDragCancelAction(Me)

    End Sub

    ''' <summary>
    ''' タブ選択時に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub tabChanged_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles tabRireki.Selected

        Call Me._Handler.Changetab(Me, e)

    End Sub
    ''' <summary>
    ''' 入出荷履歴（入荷ごと）タブダブルクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprNyusyukkaN_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprNyusyukkaN.CellDoubleClick

        Call Me._Handler.NyukaDoubleClick(Me, e)

    End Sub
    ''' <summary>
    ''' 入出荷履歴（在庫ごと）タブダブルクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprNyusyukkaZ_CellClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprNyusyukkaZ.CellDoubleClick

        Call Me._Handler.ZaikoDoubleClick(Me, e)

    End Sub

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMD040F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Me._Handler.EnterKeyDown(Me, e, MyBase.FocusedControlName())

    End Sub
    ''' <summary>
    ''' 印刷ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Me._Handler.PrintBtnDown(Me, e)

    End Sub
    'ADD START 2019/8/27 依頼番号:007116,007119
    ''' <summary>
    ''' 実行ボタン押下時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecution.Click

        Me._Handler.ExecutionBtnDown(Me, e)

    End Sub

    ''' <summary>
    ''' 営業所コンボ変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbEigyo_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEigyo.SelectedValueChanged

#If False Then  'UPD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入
        '富士フイルム 和光純薬 大分工場の場合のみ表示
        If ("96".Equals(Me.cmbEigyo.SelectedValue.ToString) OrElse "98".Equals(Me.cmbEigyo.SelectedValue.ToString)) AndAlso "W01".Equals(Me.cmbSoko.SelectedValue.ToString) Then
#Else
#If False Then  'UPD 2023/12/25 039659【LMS・EDI・ハンディ】FFEM熊本工場 LMS新規導入に伴う新規構築
        '富士フイルム 和光純薬 大分工場の場合のみ表示から（足柄工場を追加する）
        If (("96".Equals(Me.cmbEigyo.SelectedValue.ToString) OrElse "98".Equals(Me.cmbEigyo.SelectedValue.ToString)) AndAlso "W01".Equals(Me.cmbSoko.SelectedValue.ToString) OrElse
            ("F2".Equals(Me.cmbEigyo.SelectedValue.ToString) AndAlso
                ("A60".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A61".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A63".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A70".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A72".Equals(Me.cmbSoko.SelectedValue.ToString)))) Then
#Else
        If Me._Handler.IsExecutionVisible(Me) Then
#End If
#End If
            cmbExecution.Visible = True
            btnExecution.Visible = True
        Else
            cmbExecution.Visible = False
            btnExecution.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' 倉庫コンボ変更時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbSoko_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSoko.SelectedValueChanged

#If False Then  'UPD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入
                '富士フイルム 和光純薬 大分工場の場合のみ表示
        If ("96".Equals(Me.cmbEigyo.SelectedValue.ToString) OrElse "98".Equals(Me.cmbEigyo.SelectedValue.ToString)) AndAlso "W01".Equals(Me.cmbSoko.SelectedValue.ToString) Then
#Else
#If False Then  'UPD 2023/12/25 039659【LMS・EDI・ハンディ】FFEM熊本工場 LMS新規導入に伴う新規構築
        '富士フイルム 和光純薬 大分工場の場合のみ表示
        If (("96".Equals(Me.cmbEigyo.SelectedValue.ToString) OrElse "98".Equals(Me.cmbEigyo.SelectedValue.ToString)) AndAlso "W01".Equals(Me.cmbSoko.SelectedValue.ToString) OrElse
            ("F2".Equals(Me.cmbEigyo.SelectedValue.ToString) AndAlso
                ("A60".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A61".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A63".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A70".Equals(Me.cmbSoko.SelectedValue.ToString) _
                OrElse "A72".Equals(Me.cmbSoko.SelectedValue.ToString)))) Then
#Else
        If Me._Handler.IsExecutionVisible(Me) Then
#End If
#End If

            cmbExecution.Visible = True
            btnExecution.Visible = True
        Else
            cmbExecution.Visible = False
            btnExecution.Visible = False
        End If

    End Sub
    'ADD END 2019/8/27 依頼番号:007116,007119

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
