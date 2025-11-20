' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC040C : 在庫引当
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMC040フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC040F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMC040H

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

    ''START YANAI 要望番号1006
    '''' <summary>
    ''''
    '''' </summary>
    '''' <remarks></remarks>
    'Private _KeysAddCheckOn As Boolean
    'END YANAI 要望番号1006

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMC040H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

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

    ''' <summary>
    ''' 数量の値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub numSyukkaSouAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numSyukkaSouAmt.Leave

        If numSyukkaSouAmt.ReadOnly = True Then
            Me._Suryo = Convert.ToDecimal(numSyukkaSouAmt.Value)
            Exit Sub
        End If

        If Me._Suryo.Equals(Convert.ToDecimal(numSyukkaSouAmt.Value)) = False Then
            Me._Suryo = Convert.ToDecimal(numSyukkaSouAmt.Value)
            Call Me._Handler.numSyukkaSuryo_Leave(Me)
        End If

    End Sub

    ''START YANAI 20111027 入り目対応
    '''' <summary>
    '''' 入目の値変更イベント
    '''' </summary>
    '''' <param name="sender">イベント発生元オブジェクト</param>
    '''' <param name="e">イベント詳細</param>
    '''' <remarks></remarks>
    ''Private Sub numIrime_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numIrime.Leave

    ''    If numIrime.ReadOnly = True Then
    ''        Me._Irime = Convert.ToDecimal(numIrime.Value)
    ''        Exit Sub
    ''    End If

    ''    If Me._Irime.Equals(Convert.ToDecimal(numIrime.Value)) = False Then
    ''        Me._Irime = Convert.ToDecimal(numIrime.Value)
    ''        Call Me._Handler.numIrime_Leave(Me)
    ''    End If

    ''End Sub
    ''END YANAI 20111027 入り目対応

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub sprZaiko_Change(ByVal sender As Object, ByVal e As System.EventArgs) Handles sprZaiko.Change

        If sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.HIKI_CNT.ColNo OrElse
            sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.HIKI_AMT.ColNo Then

            'START YANAI 要望番号1006
            'If _KeysAddCheckOn = True Then Return
            'END YANAI 要望番号1006

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

        If sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.DEF.ColNo Then

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

        'START YANAI 要望番号1006
        '_KeysAddCheckOn = False
        'END YANAI 要望番号1006

        If Not (e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Add) Then Exit Sub

        If Not sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.HIKI_CNT.ColNo Then Exit Sub

        If e.KeyCode = Keys.Add Then
            '「+」キーの場合チェックをオンオフのみ行う。                            
            Call Me._Handler.sprZaiko_KeysAdd(Me)
            'START YANAI 要望番号1006
            '_KeysAddCheckOn = True
            'END YANAI 要望番号1006
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
    Private Sub LMC040F_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If (Keys.Enter).Equals(e.KeyCode) = False Then
            'Enter押下イベント以外は終了
            Exit Sub
        End If

        If ("numSyukkaKosu").Equals(Me.ActiveControl.Name) = True Then
            'Call Me._Handler.numSyukkaKosu_Leave(Me)
            If numSyukkaHasu.ReadOnly = False Then
                '出荷端数が読み取り専用ではない場合
                numSyukkaHasu.Focus()
            Else
                '出荷端数が読み取り専用の場合
                sprZaiko.Focus()
                sprZaiko.ActiveSheet.SetActiveCell(1, LMC040G.sprZaiko.HIKI_CNT.ColNo)
            End If

        ElseIf ("numSyukkaHasu").Equals(Me.ActiveControl.Name) = True Then
            'Call Me._Handler.numSyukkaHasu_Leave(Me)
            sprZaiko.Focus()
            sprZaiko.ActiveSheet.SetActiveCell(1, LMC040G.sprZaiko.HIKI_CNT.ColNo)

        ElseIf ("numSyukkaSouAmt").Equals(Me.ActiveControl.Name) = True Then
            'Call Me._Handler.numSyukkaSuryo_Leave(Me)
            sprZaiko.Focus()
            sprZaiko.ActiveSheet.SetActiveCell(1, LMC040G.sprZaiko.HIKI_AMT.ColNo)

        ElseIf sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.DEF.ColNo OrElse
             sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.HIKI_CNT.ColNo OrElse
             sprZaiko.ActiveSheet.ActiveColumnIndex = LMC040G.sprZaiko.HIKI_AMT.ColNo Then
            Call Me._Handler.sprZaiko_Change(Me)
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        Else
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)

        End If

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
    ''' フォームがアクティブになるときに発生するイベント
    ''' </summary>
    ''' <param name="sender">イベント発生元オブジェクト</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LMC040F_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Call Me._Handler.LMC040F_Activated(Me)
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
