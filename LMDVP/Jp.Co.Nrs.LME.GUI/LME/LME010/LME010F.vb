' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME010F : 作業料明細書作成
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME010フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME010F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LME010H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LME010H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    'START YANAI 20120319　作業画面改造
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
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._Handler.FunctionKey4Press(Me, e)

    End Sub
    'END YANAI 20120319　作業画面改造

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

        Call Me._Handler.FunctionKey7Press(Me, e)

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
    ''' 変更項目選択時イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbEditList_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditList.SelectedValueChanged

        Me._Handler.cmbEditList_SelectedValueChanged(Me)

    End Sub

    ''' <summary>
    ''' 行削除イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRowDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowDel.Click

        Me._Handler.btnRowDel_Click(Me)

    End Sub

    ''' <summary>
    ''' 行複写イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRowCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowCopy.Click

        Me._Handler.btnRowCopy_Click(Me)

    End Sub

    ''' <summary>
    ''' 一括変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllChange.Click

        Me._Handler.btnAllChange_Click(Me)

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベントです。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LME010F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Enter Then

            Dim controlNm As String = String.Empty

            controlNm = Me.FocusedControlName()

            '荷主コード入力コントロールでEnterキーを押下時
            If Me.FocusedControlName() = "txtCustCD_L" OrElse Me.FocusedControlName() = "txtCustCD_M" Then

                If String.IsNullOrEmpty(Me.txtCustCD_L.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblCustNM_L.TextValue = String.Empty
                    Me.lblCustNM_M.TextValue = String.Empty
                End If
            End If

            '請求先コード入力コントロールでEnterキーを押下時
            If Me.FocusedControlName() = "txtSeikyuCD" Then
                If String.IsNullOrEmpty(Me.txtSeikyuCD.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblSeikyuNM.TextValue = String.Empty
                End If
            End If

            '作業コード入力コントロールでEnterキーを押下時
            If Me.FocusedControlName() = "txtSagyoCD" Then
                If String.IsNullOrEmpty(Me.txtSagyoCD.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblSagyoNM.TextValue = String.Empty
                End If
            End If

            '作業コード入力コントロールでEnterキーを押下時
            If Me.FocusedControlName() = "txtEditTxt" AndAlso Me.cmbEditList.SelectedValue.Equals("08") = True Then

                If String.IsNullOrEmpty(Me.txtEditTxt.TextValue) = False Then
                    'マスタ参照（F10)
                    Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                Else
                    Me.lblEditNM.TextValue = String.Empty
                End If
            End If


                'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

    End Sub


    ''' <summary>
    ''' 印刷イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Call Me._Handler.PrintAction(Me)
    End Sub

    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' スプレッド内データをダブルクリックした時、発生するイベントです。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprSagyo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprSagyo.CellDoubleClick

        Call Me._Handler.ActionControl(LME010C.EventShubetsu.DOUBLECLICK, Me)

    End Sub    'END YANAI 20120319　作業画面改造
    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
