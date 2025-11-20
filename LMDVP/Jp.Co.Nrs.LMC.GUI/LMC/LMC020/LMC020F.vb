' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC020C : 出荷データ編集
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMC020フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC020F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMC020H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMC020H)

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

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' 印刷押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click

        Call Me._Handler.btnJIKKOU_Click(Me)

    End Sub

    ''' <summary>
    ''' 印刷押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnPRINT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRINT.Click

        Call Me._Handler.btnPRINT_Click(Me)

    End Sub

    ''' <summary>
    ''' 新規（届先）押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Call Me._Handler.btnNew_Click(Me)

    End Sub

    ''' <summary>
    ''' 履歴照会押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnRireki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRireki.Click

        Call Me._Handler.btnRireki_Click(Me)

    End Sub

    ''' <summary>
    ''' 行追加（中）押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnROW_INS_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnROW_INS_M.Click

        Call Me._Handler.btnROW_INS_M_Click(Me)

    End Sub

    ''' <summary>
    ''' 行複写（中）押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnROW_COPY_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnROW_COPY_M.Click

        Call Me._Handler.btnROW_COPY_M_Click(Me)

    End Sub

    ''' <summary>
    ''' 行削除（中）押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnROW_DEL_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnROW_DEL_M.Click

        Call Me._Handler.btnROW_DEL_M_Click(Me)

    End Sub

    ''' <summary>
    ''' 行削除（小）押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnROW_DEL_S_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnROW_DEL_S.Click

        Call Me._Handler.btnROW_DEL_S_Click(Me)

    End Sub

    ''' <summary>
    ''' 一括変更押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub btnHenko_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHenko.Click

        Call Me._Handler.btnHenko_Click(Me)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing

        Call Me._Handler.ClosingForm(Me, e)

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

    'START YANAI 要望番号646
    '''' <summary>
    '''' スプレッドでDoubleClickした時に発生するイベント
    '''' </summary>
    '''' <param name="sender">イベント発生元オブジェクト</param>
    '''' <param name="e">イベント詳細</param>
    '''' <remarks></remarks>
    'Private Sub sprSyukkaM_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprSyukkaM.CellDoubleClick

    '    Call Me._Handler.sprSyukkaM_CellDoubleClick(Me, e)

    'End Sub
    'END YANAI 要望番号646

    ''' <summary>
    ''' スプレッドでロストフォーカスした時に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprSyukkaM_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprSyukkaM.LeaveCell

        Call Me._Handler.sprSyukkaM_LeaveCell(Me, e)

    End Sub

    ''' <summary>
    ''' 梱数の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numKonsu_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numKonsu.Leave

        Call Me._Handler.numKonsu_Leave(Me)

    End Sub

    ''' <summary>
    ''' 端数の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numHasu_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numHasu.Leave

        Call Me._Handler.numKonsu_Leave(Me)

    End Sub

    ''' <summary>
    ''' 数量の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numSouSuryo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numSouSuryo.Leave

        Call Me._Handler.numSouSuryo_Leave(Me)

    End Sub

    ''' <summary>
    ''' 入目の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numIrime_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numIrime.Leave

        Call Me._Handler.numIrime_Leave(Me)

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMC020F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If (Keys.Enter).Equals(e.KeyCode) = False Then
            'Enter押下イベント以外は終了
            Exit Sub
        End If

        If Me.ActiveControl.Enabled = False Then
            'アクティブコントロールがロックの場合

            'Tabキーが押された時と同じ動作をする。
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            Exit Sub
        End If

        'value値の設定
        Dim value As String = String.Empty
        Dim txtCtl As Win.InputMan.LMImTextBox = Nothing
        Select Case Me.ActiveControl.Name
            Case "numKonsu", "numHasu"  '梱数、端数
                Call Me._Handler.numKonsu_Leave(Me)

            Case "numSouSuryo"  '数量
                Call Me._Handler.numSouSuryo_Leave(Me)

            Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5", _
                 "txtDestSagyoM1", "txtDestSagyoM2", _
                 "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"   '作業
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    Select Case Me.ActiveControl.Name
                        Case "txtSagyoM1"
                            lblSagyoM1.TextValue = String.Empty
                        Case "txtSagyoM2"
                            lblSagyoM2.TextValue = String.Empty
                        Case "txtSagyoM3"
                            lblSagyoM3.TextValue = String.Empty
                        Case "txtSagyoM4"
                            lblSagyoM4.TextValue = String.Empty
                        Case "txtSagyoM5"
                            lblSagyoM5.TextValue = String.Empty
                        Case "txtDestSagyoM1"
                            lblDestSagyoM1.TextValue = String.Empty
                        Case "txtDestSagyoM2"
                            lblDestSagyoM2.TextValue = String.Empty
                        Case "txtSagyoL1"
                            lblSagyoL1.TextValue = String.Empty
                        Case "txtSagyoL2"
                            lblSagyoL2.TextValue = String.Empty
                        Case "txtSagyoL3"
                            lblSagyoL3.TextValue = String.Empty
                        Case "txtSagyoL4"
                            lblSagyoL4.TextValue = String.Empty
                        Case "txtSagyoL5"
                            lblSagyoL5.TextValue = String.Empty
                    End Select

                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If
                Call Me._Handler.sagyo_Enter(Me)

            Case "txtUriCd", "txtTodokesakiCd"  '売上先、届先
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    If ("txtUriCd").Equals(Me.ActiveControl.Name) = True Then
                        lblUriNm.TextValue = String.Empty
                        txtTodokesakiCd.Focus()
                    ElseIf ("txtTodokesakiCd").Equals(Me.ActiveControl.Name) = True Then
                        txtTodokesakiNm.TextValue = String.Empty
                        txtTodokeAdderss1.TextValue = String.Empty
                        txtTodokeAdderss2.TextValue = String.Empty

                        txtTodokeAdderss3.Focus()
                    End If
                    Exit Sub
                End If
                'START YANAI 要望番号481
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.destCd_Enter(Me)
                'END YANAI 要望番号481

            Case "txtSerchGoodsCd", "txtSerchGoodsNm", "txtSerchLot", _
                 "txtGoodsCdCust", "lblGoodsNm" '商品(検索条件の商品)
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If
                'START YANAI 要望番号481
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.search_Enter(Me)
                'END YANAI 要望番号481

            Case "txtUnsoCompanyCd", "txtUnsoSitenCd"   '運送会社・支店コード
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(txtUnsoCompanyCd.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(txtUnsoSitenCd.TextValue) = True Then
                    '運送会社・支店コードの両方共が入力されていない場合は、ポップを呼ばない
                    lblUnsoCompanyNm.TextValue = String.Empty
                    lblUnsoSitenNm.TextValue = String.Empty
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If

                'START YANAI 要望番号513
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.unsoCd_Enter(Me)
                'END YANAI 要望番号513

            Case "txtUnthinTariffCd"   '運送タリフ
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    lblUnthinTariffNm.TextValue = String.Empty
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If

                'START YANAI 要望番号513
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.unsoTariff_Enter(Me)
                'END YANAI 要望番号513

            Case "txtExtcTariffCd"   '割増タリフ
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If
                'START YANAI 要望番号481
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.wariTariff_Enter(Me)
                'END YANAI 要望番号481

                'START UMANO 要望番号1302 支払運賃に伴う修正。
            Case "txtPayUnthinTariffCd"   '支払運賃タリフ
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    lblPayUnthinTariffNm.TextValue = String.Empty
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If

                'START YANAI 要望番号513
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.shiharaiTariff_Enter(Me)
                'END YANAI 要望番号513

            Case "txtPayExtcTariffCd"   '支払割増タリフ
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(value) = True Then
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If
                'START YANAI 要望番号481
                'Call Me._Handler.FunctionKey10Press(Me, e)
                Call Me._Handler.shiharaiwariTariff_Enter(Me)
                'END YANAI 要望番号481
                'END UMANO 要望番号1302 支払運賃に伴う修正。

            Case "imdNounyuYoteiDate"   '納入予定日
                txtNisyuTyumonNo.Focus()
                Exit Sub

            Case "txtNisyuTyumonNo"   'オーダー番号
                txtKainusiTyumonNo.Focus()
                Exit Sub

            Case "txtKainusiTyumonNo"   '注文番号
                txtTodokesakiCd.Focus()
                Exit Sub

            Case "txtLotNo"   'ロット№
                txtSerialNo.Focus()

            Case "txtSerialNo"   'シリアル№
                If 0 < sprDtl.ActiveSheet.Rows.Count Then
                    numPkgCnt.Focus()
                ElseIf optCnt.Checked = True Then
                    numKonsu.Focus()
                Else
                    numSouSuryo.Focus()
                End If

            Case "numPkgCnt"   '梱包個数
                sprDtl.Focus()

            Case "numHasu"   '端数
                sprDtl.Focus()

            Case "numSouSuryo"   '数量
                sprDtl.Focus()

                '2014/01/30 輸出情報追加 START
            Case "txtShipperCd"   'shipper
                txtCtl = DirectCast(Me.Controls.Find(Me.ActiveControl.Name, True)(0), Win.InputMan.LMImTextBox)
                value = txtCtl.TextValue
                If String.IsNullOrEmpty(txtShipperCd.TextValue) = True Then
                    lblShipperNm.TextValue = String.Empty
                    'Tabキーが押された時と同じ動作をする。
                    Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
                    Exit Sub
                End If
                Call Me._Handler.search_Enter(Me)
                '2014/01/30 輸出情報追加 END

            Case Else

        End Select

        'Tabキーが押された時と同じ動作をする。
        Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント（個数）
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub optCnt_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles optCnt.Click

        Call Me._Handler.optCnt_Selected(Me)

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント（数量）
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub optAmt_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAmt.Click

        Call Me._Handler.optAmt_Selected(Me)

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント（小分け）
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub optKowake_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles optKowake.Click

        Call Me._Handler.optKowake_Selected(Me)

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント（サンプル）
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub optSample_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSample.Click

        Call Me._Handler.optSample_Selected(Me)

    End Sub

    ''' <summary>
    ''' 運送手配変更時
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbTehaiKbn_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTehaiKbn.SelectedValueChanged

        Call Me._Handler.cmbTehaiKbn_Selected(Me)

    End Sub

    ''' <summary>
    ''' タリフ分類区分変更時
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbTariffKbun_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTariffKbun.SelectedValueChanged

        Call Me._Handler.cmbTariffKbun_Selected(Me)

    End Sub

    ''' <summary>
    ''' 届先コードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtTodokesakiCd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTodokesakiCd.Leave

        Call Me._Handler.txtTodokesakiCd_Leave(Me)

    End Sub

    ''' <summary>
    ''' 届先区分変更時
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbTodokesaki_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTodokesaki.SelectedValueChanged

        Call Me._Handler.cmbTodokesaki_Selected(Me)

    End Sub

    ''' <summary>
    ''' 印刷種別変更時
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbPRINT_Selected(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPRINT.SelectedValueChanged

        Call Me._Handler.cmbPRINT_Selected(Me)

    End Sub


    ''' <summary>
    ''' 商品変更ボタン押下時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChangeGoods_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeGoods.Click

        Call Me._Handler.btnChangeGoods_Click(Me)

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtUnsoCompanyCd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUnsoCompanyCd.Leave

        Call Me._Handler.txtUnsoCompanyCd_Leave(Me)

    End Sub

    ''' <summary>
    ''' 運送会社部署コードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtUnsoSitenCd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUnsoSitenCd.Leave

        Call Me._Handler.txtUnsoCompanyCd_Leave(Me)

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    '追加開始 --- 2014.07.24 kikuchi
    ''' <summary>
    ''' 分析表添付ラジオボタンの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbBunsakiTmp_SelectedKey(sender As System.Object, e As System.EventArgs) Handles cmbBunsakiTmp.SelectedValueChanged

        'SelectedValueChanged/SelectedIndexChangedで下記処理を行うと、画面ロード時・データ保存時など意図しないタイミングで
        'cmbBunsakiTmp_Selected(Me)処理が実行されるため、ActiveControlの値を見て直前のアクションが"cmbBunsakiTmp"か見てからハンドラの処理を実行する。
        If Me.ActiveControl Is Nothing Then
            Return
        End If

        If Me.ActiveControl.Name = "cmbBunsakiTmp" Then
            Call Me._Handler.cmbBunsakiTmp_Selected(Me)
        End If

    End Sub
    '追加終了 --- 2014.07.24 kikuchi

    ''2015.07.08 協立化学　シッピング対応 追加START
    ' ''' <summary>
    ' ''' スプレッド(マーク情報)でロストフォーカスした時に発生するイベント
    ' ''' </summary>
    ' ''' <param name="sender">イベント発生元オブジェクト</param>
    ' ''' <param name="e">イベント詳細</param>
    ' ''' <remarks></remarks>
    'Private Sub sprMarkHed_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprMarkHed.LeaveCell

    '    Call Me._Handler.sprMarkHed_LeaveCell(Me, e)

    'End Sub

    ' ''' <summary>
    ' ''' 行追加（マークHED）押下時のイベントです。
    ' ''' </summary>
    ' ''' <param name="sender">イベント発生オブジェクト</param>
    ' ''' <param name="e">イベント詳細情報</param>
    ' ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    'Private Sub btnMarkRowIns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkRowIns.Click

    '    Call Me._Handler.btnMarkRowIns_Click(Me)

    'End Sub

    ' ''' <summary>
    ' ''' 行複写（マークHED）押下時のイベントです。
    ' ''' </summary>
    ' ''' <param name="sender">イベント発生オブジェクト</param>
    ' ''' <param name="e">イベント詳細情報</param>
    ' ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    'Private Sub btnMarkRowCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkRowCopy.Click

    '    Call Me._Handler.btnMarkRowCopy_Click(Me)

    'End Sub

    ' ''' <summary>
    ' ''' 行削除（マークHED）押下時のイベントです。
    ' ''' </summary>
    ' ''' <param name="sender">イベント発生オブジェクト</param>
    ' ''' <param name="e">イベント詳細情報</param>
    ' ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    'Private Sub btnMarkRowDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkRowDel.Click

    '    Call Me._Handler.btnMarkRowDel_Click(Me)

    'End Sub

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' スプレッド(マーク情報)でロストフォーカスした時に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub TabPage2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Leave

        Call Me._Handler.TabPage2_Leave(Me)

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加END

    '========================  ↑↑↑その他のイベント ↑↑↑========================
#End Region 'Method

    Private Sub numItakuKakaku_Load(sender As Object, e As EventArgs) Handles numKitakuKakaku.Load

    End Sub

    Private Sub txtTodokesakiCd_Load(sender As Object, e As EventArgs) Handles txtTodokesakiCd.Load

    End Sub
End Class
