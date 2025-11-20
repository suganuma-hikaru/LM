' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃
'  プログラムID     :  LMF010C : 運行・運送情報
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread.CellType
Imports FarPoint.Win

''' <summary>
''' LMF010フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF010F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMF010H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMF010H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._H = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"
    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' ファンクション１ボタン押下時およびファンクションキー１押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function1_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F1PressEvent

        Call Me._H.FunctionKey1Press(Me, e)

    End Sub
    '(2013.01.17)要望番号1617 --  END  --

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' ファンクション３ボタン押下時およびファンクションキー３押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function3_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F3PressEvent

        Call Me._H.FunctionKey3Press(Me, e)

    End Sub
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    '2022.08.22 追加START
    ''' <summary>
    ''' ファンクション４ボタン押下時およびファンクションキー４押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function4_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F4PressEvent

        Call Me._H.FunctionKey4Press(Me, e)

    End Sub
    '2022.08.22 追加END

    ''' <summary>
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function5_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        Call Me._H.FunctionKey5Press(Me, e)

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

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
    ''' <summary>
    ''' ファンクション８ボタン押下時およびファンクションキー７押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function8_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F8PressEvent

        Call Me._H.FunctionKey8Press(Me, e)

    End Sub
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベントです。
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
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベントです。
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    Private Sub Function12_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

        Call Me._H.FunctionKey12Press(Me, e)

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
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub sprUnsoUnkou_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprUnsoUnkou.CellDoubleClick
        Call Me._H.sprCellDoubleClick(Me, e)
    End Sub

    ''' <summary>
    ''' イベントオプションボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub optEvent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optEventY.CheckedChanged, optEventN.CheckedChanged
        Call Me._H.optEvent_CheckedChanged(Me, e)
    End Sub

    ''' <summary>
    ''' 修正項目変更時に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub cmbShuSei_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShuSei.SelectedValueChanged
        Call Me._H.cmbShuSei_SelectedValueChanged(Me, e)
    End Sub

    ''' <summary>
    ''' キー押下イベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub LMF010F_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Call Me._H.LMF010F_KeyDown(Me, e)
    End Sub

    ''' <summary>
    ''' ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub btnHenko_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHenko.Click
        Call Me._H.btnHenko_Click(Me, e)
    End Sub

    ''' <summary>
    ''' 運行編集ボタン押下イベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub btnUnkoEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnkoEdit.Click
        Call Me._H.btnUnkoEdit_Click(Me, e)
    End Sub

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Call Me._H.btnPrint_Click(Me, e)
    End Sub
    '2012.06.22 要望番号1189 追加END

    '2017/02/27 追加START
    ''' <summary>
    ''' 運送会社帳票印刷 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>

    Private Sub btnTrapoPrint_Click(sender As Object, e As EventArgs) Handles btnTrapoPrint.Click

        Call Me._H.btnTrapoPrint_Click(Me, e)
    End Sub

    '(2013.01.17)要望番号1774 -- START --
    Private Sub sprUnsoUnkou_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprUnsoUnkou.EditModeOff

        Dim iRow As Integer = sprUnsoUnkou.ActiveSheet.ActiveRowIndex
        Dim iCol As Integer = sprUnsoUnkou.ActiveSheet.ActiveColumnIndex
        If TypeOf (sprUnsoUnkou.ActiveSheet.GetCellType(iRow, iCol)) Is CheckBoxCellType Then
            Dim chkcell As FpCheckBox = CType(sprUnsoUnkou.EditingControl, FpCheckBox)
            'イベントハンドラ関連付け解除 
            RemoveHandler chkcell.CheckChanged, AddressOf chkcell_CheckChanged
        End If

    End Sub

    Private Sub sprUnsoUnkou_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprUnsoUnkou.EditModeOn
        Dim iRow As Integer = sprUnsoUnkou.ActiveSheet.ActiveRowIndex
        Dim iCol As Integer = sprUnsoUnkou.ActiveSheet.ActiveColumnIndex
        If TypeOf (sprUnsoUnkou.ActiveSheet.GetCellType(iRow, iCol)) Is CheckBoxCellType Then
            Dim chkcell As FpCheckBox = CType(sprUnsoUnkou.EditingControl, FpCheckBox)
            'イベントハンドラ関連付け 
            AddHandler chkcell.CheckChanged, AddressOf chkcell_CheckChanged
        End If

    End Sub

    Private Sub chkcell_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Call Me._H.chkcell_CheckChanged(Me, e)

    End Sub

    Private Sub sprUnsoUnkou_SelectionChanged(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.SelectionChangedEventArgs) Handles sprUnsoUnkou.SelectionChanged

        Call Me._H.chkcell_CheckChanged(Me, e)

    End Sub

    '(2013.01.17)要望番号1774 --  END  --

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method


End Class
