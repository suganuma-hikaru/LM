' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB020F : 入荷データ編集
'  作  成  者       :  [iwamoto]
' ==========================================================================

Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMB020フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB020F
    Friend WithEvents Calendar As GrapeCity.Win.Editors.DropDownCalendar

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMB020H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMB020H)

        MyBase.New()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._H = handlerClass

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

        Call Me._H.FunctionKey1Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション２ボタン押下時およびファンクションキー２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function2_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F2PressEvent

        Call Me._H.FunctionKey2Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション３ボタン押下時およびファンクションキー３押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function3_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F3PressEvent

        Call Me._H.FunctionKey3Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._H.FunctionKey4Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function5_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        '追加開始 2014.01.13 韓国CALT対応
        Call Me._H.FunctionKey5Press(Me, e)
        '追加終了 2014.01.13 韓国CALT対応

    End Sub

    ''' <summary>
    ''' ファンクション６ボタン押下時およびファンクションキー６押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function6_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F6PressEvent

        Call Me._H.FunctionKey6Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション７ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function7_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F7PressEvent

        Call Me._H.FunctionKey7Press(Me, e)

    End Sub

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
    ''' ファンクション９ボタン押下時およびファンクションキー８押下時のイベントです。
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
    ''' ファンクション１１ボタン押下時およびファンクションキー１１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function11_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F11PressEvent

        Call Me._H.FunctionKey11Press(Me, e)

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
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMB020F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Call Me._H.LMB020F_KeyDown(Me, e)

    End Sub

    'START YANAI 要望番号646
    '''' <summary>
    '''' スプレッド(下部)でDoubleClickした時に発生するイベント
    '''' </summary>
    '''' <param name="sender">イベント発生元オブジェクト</param>
    '''' <param name="e">イベント詳細</param>
    '''' <remarks></remarks>
    'Private Sub sprGoodsDef_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprGoodsDef.CellDoubleClick

    '    Call Me._H.sprGoodsDef_CellDoubleClick(Me, e)

    'End Sub
    'END YANAI 要望番号646

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Call Me._H.btnPrint_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 行削除(中)ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnRowDelM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowDelM.Click

        Call Me._H.btnRowDelM_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 行追加(中)ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnRowAddM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowAddM.Click

        Call Me._H.btnRowAddM_Click(Me, e)

        'F4キーを押下
        SendKeys.Send("{F4}")

    End Sub

    ''' <summary>
    ''' 行削除(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnRowDelS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowDelS.Click

        Call Me._H.btnRowDelS_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 行追加(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnRowAddS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowAddS.Click

        Call Me._H.btnRowAddS_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 行複写(小)ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRowCopyS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRowCopyS.Click

        Call Me._H.btnRowCopyS_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 分析票ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnCoaAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCoaAdd.Click

        Call Me._H.btnCoaAdd_Click(Me, e)

    End Sub

    ''' <summary>
    ''' イエローカートボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnYCardAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYCardAdd.Click

        Call Me._H.btnYCardAdd_Click(Me, e)

    End Sub

    ''' <summary>
    ''' 運送有無の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbUnchinUmu_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnchinUmu.SelectedValueChanged

        Call Me._H.cmbUnchinUmu_SelectedValueChanged(Me, e)

    End Sub

    ''' <summary>
    ''' 運送手配の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbUnchinKbn_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnchinKbn.SelectedValueChanged

        Call Me._H.cmbUnchinKbn_SelectedValueChanged(Me, e)

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprDetail_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprDetail.Change


        If sprDetail.ActiveSheet.ActiveColumnIndex = LMB020C.SprInkaSColumnIndex.NB OrElse
            sprDetail.ActiveSheet.ActiveColumnIndex = LMB020C.SprInkaSColumnIndex.HASU OrElse
            sprDetail.ActiveSheet.ActiveColumnIndex = LMB020C.SprInkaSColumnIndex.IRIME Then
            Call Me._H.sprDetail_Change(Me)
        End If

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprGoodsDef_LeaveCell(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles sprGoodsDef.LeaveCell
        Call Me._H.sprGoodsDef_LeaveCell(Me, e)
    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnHenko_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHenko.Click
        Call Me._H.btnHenko_Click(Me, e)
    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下イベント（置場）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAllChange_Click(sender As Object, e As EventArgs) Handles btnAllChange.Click
        Call Me._H.btnAllChange_Click(Me, e)
    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtUnsoCd_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUnsoCd.Leave

        Call Me._H.txtUnsoCd_Leave(Me)

    End Sub

    ''' <summary>
    ''' 運送会社部署コードの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub txtTrnBrCD_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTrnBrCD.Leave

        Call Me._H.txtUnsoCd_Leave(Me)

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    'ADD 2017/08/04 GHSラベル対応
    Private Sub cmbPrint_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrint.SelectedValueChanged, cmbJikkou.SelectedValueChanged

        Call Me._H.cmbPrint_SelectedValueChanged(Me, e)

    End Sub

    ''' <summary>
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnJikkou_Click(sender As Object, e As EventArgs) Handles btnJikkou.Click
        Call Me._H.btnJIKKOU_Click(Me)
    End Sub

    ''' <summary>
    ''' 画像登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnImgAdd_Click(sender As Object, e As EventArgs) Handles btnImgAdd.Click
        Call Me._H.btnAddImg_Click(Me)
    End Sub

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' 写真選択ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnPhotoSel_Click(sender As Object, e As EventArgs) Handles btnPhotoSel.Click
        Call Me._H.btnPhotoSel_Click(Me)
    End Sub
    'ADD 2022/11/07 倉庫写真アプリ対応 END

    ''' <summary>
    ''' 現場作業取込ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub btnWHSagyoTorikomi_Click(sender As Object, e As EventArgs) Handles btnWHSagyoTorikomi.Click
        Call Me._H.btnWHSagyoTorikomi_Click(Me)
    End Sub

    'Add Start 2019/10/09 要望管理007373
    ''' <summary>
    ''' 出荷止クリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub chkStopAlloc_Click(sender As Object, e As EventArgs) Handles chkStopAlloc.Click
        Call Me._H.chkStopAlloc_Click(Me, e)
    End Sub
    'Add End   2019/10/09 要望管理007373

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' スプレッドのセルダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub sprDetail_CellDoubleClick(sender As Object, e As CellClickEventArgs) Handles sprDetail.CellDoubleClick

        If sprDetail.ActiveSheet.ActiveColumnIndex = LMB020C.SprInkaSColumnIndex.ENT_PHOTO Then
            Call Me._H.sprDetail_CellDoubleClick(Me)
        End If

    End Sub
    'ADD 2022/11/07 倉庫写真アプリ対応 END

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
