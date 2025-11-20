' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060F   : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMN060フォームクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMN060H

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付ける</remarks>
    Public Sub New(ByVal handlerClass As LMN060H)

        MyBase.New()

        'この呼び出しは、Windows フォームデザイナで必要
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付ける
        Me._Handler = handlerClass

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' ファンクション５ボタン押下時およびファンクションキー５押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function5Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F5PressEvent

        Call Me._Handler.FunctionKey5Press(Me, e)

    End Sub

    ''' <summary>
    ''' ファンクション９ボタン押下時およびファンクションキー９押下時のイベント
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks>各イベントに対応するハンドラクラスのメソッドを呼び出す</remarks>
    Private Sub Function9Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FunctionKey.F9PressEvent

        Call Me._Handler.FunctionKey9Press(Me, e)

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

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' Spread行ダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprDetail_SpreadDoubleClick(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles sprDetail.SpreadDoubleClick

        Call Me._Handler.SpreadDoubleClick(Me, e)

    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================


#End Region

End Class
