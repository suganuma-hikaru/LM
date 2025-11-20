' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530F : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI530フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMI530H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付ける</remarks>
    Public Sub New(ByVal handlerClass As LMI530H)

        MyBase.New()

        'この呼び出しは、Windows フォームデザイナで必要
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付ける
        Me._Handler = handlerClass

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' ファンクション１１ボタン押下時およびファンクションキー１１押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function11Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F11PressEvent

        Call Me._Handler.FunctionKey11Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション１２ボタン押下時およびファンクションキー１２押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function12Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F12PressEvent

        Call Me._Handler.FunctionKey12Press(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs)

        Call Me._Handler.ClosingForm(Me, e)

    End Sub

    ''' <summary>
    ''' フォームが閉じた後に発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks>Navigateクラスのインスタンス管理から登録解除を行う</remarks>
    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        LMFormNavigate.Revoke(Me._Handler)

        Call MyBase.Dispose()

    End Sub

#End Region

End Class
