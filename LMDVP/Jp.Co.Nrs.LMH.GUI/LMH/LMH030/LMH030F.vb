' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH030F : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMH030フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH030F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMH030H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMH030H)

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
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function5_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        Call Me._Handler.FunctionKey5Press(Me, e)

    End Sub

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
    ''' ファンクション７ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function7_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F7PressEvent

        'Call Me._Handler.FunctionKey7Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション８ボタン押下時およびファンクションキー８押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function8_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F8PressEvent

        Call Me._Handler.FunctionKey8Press(Me, e)

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
    ''' 実行ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExe.Click

        Me._Handler.btnExe_Click(Me)

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Call Me._Handler.btnPrint_Click(Me)

    End Sub

    ''' <summary>
    ''' 一括変更ボタンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnIkkatuChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIkkatuChange.Click

        Me._Handler.btnIkkatuChange_Click(Me)

    End Sub

    ''' <summary>
    ''' 変更項目選択時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbIkkatuChangeKbn_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIkkatuChangeKbn.SelectedValueChanged


        Me._Handler.cmbIkkatuChangeKbn_SelectedValueChanged(Me)

    End Sub

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprCellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprEdiList.CellDoubleClick

        Me._Handler.sprCellDoubleClick(Me, e)

    End Sub

    '▼▼▼要望番号:467
    ''' <summary>
    ''' 出力ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        Me._Handler.btnOutput_Click(Me, e)

    End Sub

    ''' <summary>
    ''' CSV作成・印刷コンボ選択時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbOutput_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOutput.SelectedValueChanged

        Me._Handler.cmbOutput_SelectedValueChanged(Me)

    End Sub
    '▲▲▲要望番号:467

    '要望番号1061 2012.05.15 追加START
    ''' <summary>
    ''' 出力区分コンボ選択時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbOutputkb_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOutputKb.SelectedValueChanged

        Me._Handler.cmbOutputKb_SelectedValueChanged(Me)

    End Sub
    '要望番号1061 2012.05.15 追加END

    ''要望番号1991 2013.04.02 追加START
    '''' <summary>
    '''' 項目表示コンボ選択時のイベント
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cmbVisibleKb_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVisibleKb.SelectedValueChanged

    '    Me._Handler.cmbVisibleKb_SelectedValueChanged(Me)

    'End Sub
    ''要望番号1991 2013.04.02 追加END

    '========================  ↑↑↑その他のイベント ↑↑↑========================

    Private Sub chkStaMitouroku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaMitouroku.CheckedChanged
        Me._Handler.StatusControl(Me, sender)
    End Sub

    Private Sub chkStaTourokuzumi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaTourokuzumi.CheckedChanged
        Me._Handler.StatusControl(Me, sender)
    End Sub

    Private Sub chkStaJissekimi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaJissekimi.CheckedChanged
        Me._Handler.StatusControl(Me, sender)
    End Sub

    Private Sub chkStaJissekizumi1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaJissekiSakusei.CheckedChanged
        Me._Handler.StatusControl(Me, sender)
    End Sub

    Private Sub chkStaJissekizumi2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaJissekiSousin.CheckedChanged
        Me._Handler.StatusControl(Me, sender)
    End Sub

    Private Sub chkstaRedData_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkstaRedData.CheckedChanged
        Me._Handler.StatusControlHaita(Me, sender)
    End Sub

    Private Sub chkStaTorikesi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStaTorikesi.CheckedChanged
        Me._Handler.StatusControlHaita(Me, sender)
    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベントです。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMH010F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim controlNm As String = String.Empty

            controlNm = Me.FocusedControlName()

            '荷主コード入力コントロールでEnterキーを押下時
            If (Me.FocusedControlName() = "txtCustCD_L" OrElse Me.FocusedControlName() = "txtCustCD_M") Then

                If String.IsNullOrEmpty(Me.txtCustCD_L.TextValue) = False OrElse _
                   String.IsNullOrEmpty(Me.txtCustCD_M.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblCustNM_L.TextValue = String.Empty
                    Me.lblCustNM_M.TextValue = String.Empty
                End If


            End If

            '届先コード入力コントロールでEnterキーを押下時
            If (Me.FocusedControlName() = "txtTodokesakiCd") Then
                If String.IsNullOrEmpty(Me.txtTodokesakiCd.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblTodokesakiNM.TextValue = String.Empty
                End If
            End If

            '運送会社コード(一括変更)入力コントロールでEnterキーを押下時
            If (Me.FocusedControlName() = "txtEditMain" OrElse Me.FocusedControlName() = "txtEditSub") Then
                If String.IsNullOrEmpty(Me.txtEditMain.TextValue) = False OrElse _
                   String.IsNullOrEmpty(Me.txtEditSub.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblEditNm.TextValue = String.Empty
                End If
            End If

            '届先コード(一括変更)入力コントロールでEnterキーを押下時　　ADD 2018*/02/22
            If (Me.FocusedControlName() = "txtEditDestCD") Then
                If String.IsNullOrEmpty(Me.txtEditDestCD.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblTodokesakiNM.TextValue = String.Empty
                End If
            End If


            '担当者コード入力コントロールでEnterキーを押下時
            If (Me.FocusedControlName() = "txtTantouCd") Then
                If String.IsNullOrEmpty(Me.txtTantouCd.TextValue) = False Then
                    'キャッシュより取得
                    Call Me._Handler.LMH030F_KeyDown(Me, e)
                Else
                    Me.lblTantouNM.TextValue = String.Empty
                End If
            End If

            '表示検索日立物流配送指示で入力コントロールでEnterキーを押下時（ﾊﾞｰｺｰﾄﾞ時）ADD 2017/06/2
            If (Me.FocusedControlName() <> "txtBarCD") OrElse _
                ((Me.FocusedControlName() = "txtBarCD") _
                 And String.IsNullOrEmpty(Me.txtBarCD.TextValue) = True) Then

                'Tabキーが押された時と同じ動作をする。
                Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

            ElseIf (Me.FocusedControlName() = "txtBarCD") Then

                'EDI検索処理
                Call Me._Handler.ChackBarCD(Me, Me.txtBarCD.TextValue)

                'フォーカス設定
                Me.txtBarCD.Focus()

            End If

        End If

    End Sub

    ' ''' <summary>
    ' ''' ﾊﾞｰｺｰﾄﾞTextChangeイベント (ﾊﾞｰｺｰﾄﾞ読み込むとEnterキーで処理されるのでこの処理をやめる)
    ' ''' </summary>
    ' ''' <param name="sender">イベント発生元オブジェクト</param>
    ' ''' <param name="e">イベント詳細</param>
    ' ''' <remarks></remarks>
    'Private Sub txtBarCD_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBarCD.InputBoxTextChange

    '    'ﾊﾞｰｺｰﾄﾞ入力コントロール
    '    If (Me.FocusedControlName() = "txtBarCD") Then
    '        If String.IsNullOrEmpty(Me.txtBarCD.TextValue) = False Then

    '            '指定文字数か？
    '            If Len(Me.txtBarCD.TextValue) = LMH030C.DIC_HAISO_SIJI_MO.DIGIT_NUMBER Then
    '                'EDI検索処理
    '                Call Me._Handler.ChackBarCD(Me, Me.txtBarCD.TextValue)

    '                'フォーカス設定
    '                Me.txtBarCD.Focus()
    '            End If
    '        End If

    '    End If

    'End Sub
#End Region 'Method

    Private Sub cmbIkkatuChangeKbn_Load(sender As Object, e As EventArgs) Handles cmbIkkatuChangeKbn.Load

    End Sub

End Class
