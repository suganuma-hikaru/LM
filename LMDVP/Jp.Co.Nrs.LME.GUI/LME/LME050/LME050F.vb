' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME050  : 作業個数引当
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME050フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME050F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LME050H

    ''' <summary>
    ''' 梱数
    ''' </summary>
    ''' <remarks></remarks>
    Private _Konsu As Decimal

    ''' <summary>
    ''' 端数
    ''' </summary>
    ''' <remarks></remarks>
    Private _Hasu As Decimal

    ''' <summary>
    ''' 数量
    ''' </summary>
    ''' <remarks></remarks>
    Private _Suryo As Decimal

    ''' <summary>
    ''' 入目
    ''' </summary>
    ''' <remarks></remarks>
    Private _Irime As Decimal

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LME050H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

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

        If Me.Owner Is Nothing = False Then

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

        MyBase.Dispose()

    End Sub

    '========================  ↓↓↓その他のイベント ↓↓↓========================
    ''' <summary>
    ''' 梱数の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numSyukkaKosu_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numSyukkaKosu.Leave

        If numSyukkaKosu.ReadOnly = True Then
            Me._Konsu = Convert.ToDecimal(numSyukkaKosu.Value)
            Exit Sub
        End If

        If Me._Konsu.Equals(Convert.ToDecimal(numSyukkaKosu.Value)) = False Then
            Me._Konsu = Convert.ToDecimal(numSyukkaKosu.Value)
            Call Me._Handler.numSyukkaKosu_Leave(Me)
        End If

    End Sub

    ''' <summary>
    ''' 端数の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numSyukkaHasu_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numSyukkaHasu.Leave

        If numSyukkaHasu.ReadOnly = True Then
            Me._Hasu = Convert.ToDecimal(numSyukkaHasu.Value)
            Exit Sub
        End If

        If Me._Hasu.Equals(Convert.ToDecimal(numSyukkaHasu.Value)) = False Then
            Me._Hasu = Convert.ToDecimal(numSyukkaHasu.Value)
            Call Me._Handler.numSyukkaHasu_Leave(Me)
        End If

    End Sub

    'START YANAI 要望番号1090 指摘修正
    ''' <summary>
    ''' 入目の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numIrime_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numIrime.Leave

        If numIrime.ReadOnly = True Then
            Exit Sub
        End If

        Call Me._Handler.numIrime_Leave(Me)
        
    End Sub
    'END YANAI 要望番号1090 指摘修正

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprZaiko_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprZaiko.Change

        If sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.HIKI_CNT OrElse _
            sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.HIKI_AMT Then

            Call Me._Handler.sprZaiko_Change(Me)

        End If

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprZaiko_ButtonClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprZaiko.ButtonClicked

        If sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.DEF Then

            Call Me._Handler.sprZaiko_Change(Me)

        End If

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub sprZaiko_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sprZaiko.KeyDown

        If Not (e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Add) Then Exit Sub

        If Not sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.HIKI_CNT Then Exit Sub

        If e.KeyCode = Keys.Add Then
            '「+」キーの場合チェックをオンオフのみ行う。                            
            Call Me._Handler.sprZaiko_KeysAdd(Me)
            Return
        Else
            Call Me._Handler.sprZaiko_Change(Me)
        End If

        Call Me._Handler.FunctionKey11Press(Me, e)

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LME050F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If (Keys.Enter).Equals(e.KeyCode) = False Then
            'Enter押下イベント以外は終了
            Exit Sub
        End If

        If ("numSyukkaKosu").Equals(Me.ActiveControl.Name) = True Then
            If numSyukkaHasu.ReadOnly = False Then
                '端数が読み取り専用ではない場合
                numSyukkaHasu.Focus()
            Else
                '端数が読み取り専用の場合
                sprZaiko.Focus()
                sprZaiko.ActiveSheet.SetActiveCell(1, LME050G.sprZaiko.HIKI_CNT.ColNo)
            End If

        ElseIf ("numSyukkaHasu").Equals(Me.ActiveControl.Name) = True Then
            sprZaiko.Focus()
            sprZaiko.ActiveSheet.SetActiveCell(1, LME050G.sprZaiko.HIKI_CNT.ColNo)

        ElseIf ("numSyukkaSouAmt").Equals(Me.ActiveControl.Name) = True Then
            sprZaiko.Focus()
            sprZaiko.ActiveSheet.SetActiveCell(1, LME050G.sprZaiko.HIKI_AMT.ColNo)

        ElseIf sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.DEF OrElse _
             sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.HIKI_CNT OrElse _
             sprZaiko.ActiveSheet.ActiveColumnIndex = LME050C.SprZaikoColumnIndex.HIKI_AMT Then
            Call Me._Handler.sprZaiko_Change(Me)
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        Else
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

    End Sub

    ''' <summary>
    ''' フォームがアクティブになるときに発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LME050F_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Call Me._Handler.LME050F_Activated(Me)
    End Sub

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

    Private Sub sprZaiko_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprZaiko.EditModeOn

        Dim KeyDownHandler As KeyEventHandler = AddressOf sprZaiko_KeyDown
        AddHandler sprZaiko.EditingControl.KeyDown, KeyDownHandler

    End Sub

    Private Sub sprZaiko_EditModeOff(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprZaiko.EditModeOff

        Dim KeyDownHandler As KeyEventHandler = AddressOf sprZaiko_KeyDown
        RemoveHandler sprZaiko.EditingControl.KeyDown, KeyDownHandler

    End Sub

End Class
