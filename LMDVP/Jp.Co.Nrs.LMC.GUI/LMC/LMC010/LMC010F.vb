' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTM     : マスタメンテナンス
'  プログラムID     :  LMC010F : 会社マスタ
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMC010フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC010F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMC010H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMC010H)

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
    ''' 変更ボタン押下時、発生イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUnso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnso.Click

        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.HENKO, Me)

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時、発生イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.PRINT, Me)

    End Sub

    ''' <summary>
    ''' 実行ボタン押下時、発生イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click

        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.JIKKOU, Me)

    End Sub
#If True Then ' 名鉄対応(2499) 20160323 added inoue

    ''' <summary>
    ''' 運送会社帳票印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTrapoPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrapoPrint.Click

        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.TRAPOPRINT, Me)

    End Sub

#End If

    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 実行コンボ選択時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbJikkou_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbJikkou.SelectedValueChanged

        Me._Handler.cmbJikkou_SelectedValueChanged(Me)

    End Sub
    '社内入荷データ作成 terakawa End

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

                Case "txtCustCD"
                    If String.IsNullOrEmpty(Me.txtCustCD.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    Else
                        Me.lblCustNM.TextValue = String.Empty
                    End If
                Case "txtTrnCD"
                    If String.IsNullOrEmpty(Me.txtTrnCD.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(Me.txtTrnBrCD.TextValue) = True Then
                        Me.lblTrnNM.TextValue = String.Empty
                    End If
                    If String.IsNullOrEmpty(Me.txtTrnCD.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    End If
                Case "txtTrnBrCD"
                    If String.IsNullOrEmpty(Me.txtTrnCD.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(Me.txtTrnBrCD.TextValue) = True Then
                        Me.lblTrnNM.TextValue = String.Empty
                    End If
                    If String.IsNullOrEmpty(Me.txtTrnBrCD.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If

                    End If
                    '要望番号:1568 terakawa 2013.01.17 Start
                Case "txtShinkiCustCdL"
                    If String.IsNullOrEmpty(Me.txtShinkiCustCdL.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    End If
                Case "txtShinkiCustCdM"
                    If String.IsNullOrEmpty(Me.txtShinkiCustCdM.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    End If
                Case "txtShinkiTrnCd"
                    If String.IsNullOrEmpty(Me.txtShinkiTrnCd.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    End If
                Case "txtShinkiTrnBrCd"
                    If String.IsNullOrEmpty(Me.txtShinkiTrnBrCd.TextValue) = False Then
                        'マスタ参照（F10)
                        Call Me._Handler.FunctionKey10Press(Me, TryCast(e, System.Windows.Forms.KeyEventArgs))
                        'マスタ参照にて、エラーになった場合は営業所コンボにフォーカスがセットされているので処理を抜ける
                        If Me.ActiveControl.Name.ToString() = Me.cmbEigyo.Name Then
                            Exit Sub
                        End If
                    End If
                    '要望番号:1568 terakawa 2013.01.17 End
                Case "txtPicCD"
                    '担当者名取得
                    Call Me._Handler.SetTantoName(Me)

            End Select

            'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

    End Sub

    ''' <summary>
    ''' スプレッド内データをダブルクリックした時、発生するイベントです。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprDetail_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprDetail.CellDoubleClick

        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.DOUBLE_CLICK, Me)

    End Sub

    'START YANAI 要望番号917
    ''' <summary>
    ''' 未印刷の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbPrintSyubetu_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrintSyubetu.SelectedValueChanged
        Call Me._Handler.ActionControl(LMC010C.EventShubetsu.CMBPRINTSYUBETUCHENGE, Me)
    End Sub
    'END YANAI 要望番号917
    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
