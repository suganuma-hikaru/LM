' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 特定荷主機能
'  プログラムID     :  LMI490F : ローム　棚卸対象商品リスト
'  作  成  者       :  kido
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI490フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI490F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMI490H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付ける</remarks>
    Public Sub New(ByVal handlerClass As LMI490H)

        MyBase.New()

        'この呼び出しは、Windows フォームデザイナで必要
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付ける
        Me._Handler = handlerClass

    End Sub

#End Region

#Region "Method"

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

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' 作成ボタン押下時、発生イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMake.Click

        '実行ボタン押下時
        Call Me._Handler.btnMake(Me, e)

    End Sub

    Private Sub optOutkaPlanJisseki_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub optAllJisseki_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Friend Function optCUST2() As Object
        Throw New NotImplementedException()
    End Function

    Private Sub grpSakusei_Enter(sender As Object, e As EventArgs) Handles grpSakusei.Enter

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region

End Class
