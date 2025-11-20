' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI963F : 荷主自動振分画面(手動)（ハネウェル）
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI963フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI963F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMI963H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMI963H)

        MyBase.New()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"



    '''' <summary>
    '''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベントです。
    '''' </summary>
    '''' <param name="sender">イベント発生オブジェクト</param>
    '''' <param name="e">イベント詳細情報</param>
    '''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    'Private Sub Function9_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F9PressEvent

    '    Call Me._Handler.FunctionKey9Press(Me, e)

    'End Sub

    ''START YANAI 要望番号604
    '''' <summary>
    '''' ファンクション１０ボタン押下時およびファンクションキー１０押下時のイベントです。
    '''' </summary>
    '''' <param name="sender">イベント発生オブジェクト</param>
    '''' <param name="e">イベント詳細情報</param>
    '''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出してください。</remarks>
    'Private Sub Function10_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F10PressEvent

    '    Call Me._Handler.FunctionKey10Press(Me, e)

    'End Sub
    ''END YANAI 要望番号604

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


        If IsNothing(Me.Owner) = False Then
            Me.Owner.Activate()
            Me.Hide()
            Me.Owner.Update()
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
    ''' ダブルクリック処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprDetail_CellDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprDetail.CellDoubleClick

        If e.Row.Equals(0) = False Then

            Me._Handler.sprCellDoubleClick(Me, e.Row)

        End If

    End Sub

    ''' <summary>
    ''' キーダウンイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sprDetail.KeyDown

        With Me.sprDetail.Sheets(0)

            If .ActiveRowIndex.Equals(0) = False Then

                Me._Handler.sprCellDoubleClick(Me, .ActiveRowIndex)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 初期Focus設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LMI963F_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        Me._Handler.SetFoucus(Me)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class