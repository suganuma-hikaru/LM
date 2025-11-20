' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA020H : メニュー
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMA020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gamen As LMA020G

    ''' <summary>
    ''' アプリケーションVersion
    ''' </summary>
    ''' <remarks></remarks>
    Private _AppVersion As String

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'メニューより受け取ったVersionを格納
        Me._AppVersion = prm.AppVersion

        'フォームの作成
        Dim frm As LMA020F = New LMA020F(Me)

        'Gamenクラスの設定
        Me._Gamen = New LMA020G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._Gamen.SetFunctionKey()

        'タブインデックスの設定
        Call Me._Gamen.SetTabIndex()

        'コントロール個別設定
        Call Me._Gamen.SetControl(Me._AppVersion)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G013")

        '画面の入力項目の制御
        Call Me._Gamen.SetControlsStatus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMA020F, ByVal e As FormClosingEventArgs)

        '画面終了チェック
        '他の画面が開いている場合チェック

        Dim rtn As MsgBoxResult

        If LMFormNavigate.IsStandAlone = False Then

            rtn = MyBase.ShowMessage(frm, "W014")
            If rtn = MsgBoxResult.Ok Then

            ElseIf rtn = MsgBoxResult.Cancel Then

                e.Cancel = True

            End If

        Else

            rtn = MyBase.ShowMessage(frm, "C001", New String() {"終了"})

            If rtn = MsgBoxResult.Ok Then

            ElseIf rtn = MsgBoxResult.Cancel Then

                e.Cancel = True

            End If

        End If

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' マスタデータをキャッシュする
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub GetCachedMasterData(ByVal frm As LMA020F)

        Call Me.CacheMasterData()

        '最新取得時刻の表示
        frm.lblTime.TextValue = My.Computer.Clock.LocalTime.ToString

    End Sub

    ''' <summary>
    ''' 画面呼出
    ''' </summary>
    ''' <param name="frm">LMA020F</param>
    ''' <param name="menuItem">クリックしたメニューアイテム</param>
    ''' <remarks></remarks>
    Private Sub NextForm(ByVal frm As LMA020F, ByVal menuItem As ToolStripMenuItem)

        LMFormNavigate.NextFormNavigate(Me, menuItem.Name, New LMFormData())
       
    End Sub

    ''' <summary>
    ''' ログイン画面を呼び出します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>この画面から呼び出せばパスワード変更モードになります。</remarks>
    Private Sub ShowLoginForm(ByVal frm As LMA020F)

        Dim prm As LMFormData = New LMFormData()

        prm.AppVersion = Me._AppVersion
        LMFormNavigate.NextFormNavigate(Me, "LMA010", prm)

    End Sub

    ''' <summary>
    ''' タイマー起動時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub StartTimer(ByVal frm As LMA020F)

        'タイマーストップ
        frm.timerMessage.Enabled = False

        Call MyBase.CacheSystemData()

        'タイマースタート
        frm.timerMessage.Enabled = True

    End Sub

    ''' <summary>
    ''' プロセスを探し終了させる
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ShutDownLauncherProcesses()

        '指定EXEを探し終了させる 
        Dim pr As Process

        '①ローカルで実行中のプロセス
        Dim localByName As Process() = Process.GetProcessesByName("Jp.Co.Nrs.LM.Launcher.vshost")
        '起動中のEXEを取得 
        For Each pr In localByName
            '指定のファイル名(Jp.Co.Nrs.LM.Launcher.vshost)があれば終了する 
            If Trim(pr.ProcessName) = "Jp.Co.Nrs.LM.Launcher.vshost" Then
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

        '①ローカルで実行中のプロセス   
        Dim clickOnceByName As Process() = Process.GetProcessesByName("LMS_2017")
        '起動中のEXEを取得 
        For Each pr In clickOnceByName
            '指定のファイル名(LMS_2017)があれば終了する 
            If Trim(pr.ProcessName) = "LMS_2017" Then
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

    End Sub

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMA020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        'ここから各処理を呼び出してください。  
        Call Me.ShowLoginForm(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMA020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        'ここから各処理を呼び出してください。
        Call Me.GetCachedMasterData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMA020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        'ここから各処理を呼び出してください。  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMA020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        'ここから各処理を呼び出してください。 
        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' メニューリストクリック時
    ''' </summary>
    ''' <param name="frm">LMA020F</param>
    ''' <param name="menuItem">ToolStripMenuItem</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Friend Sub DropDownMenuListClick(ByRef frm As LMA020F, ByVal menuItem As ToolStripMenuItem, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DropDownMenuListClick")

        Call Me.NextForm(frm, menuItem)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DropDownMenuListClick")

    End Sub

    ''' <summary>
    ''' メニューボタンクリック時
    ''' </summary>
    ''' <param name="frm">LMA020F</param>
    ''' <param name="menuItem">ToolStripMenuItem</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Friend Sub ToolStripMenuClick(ByRef frm As LMA020F, ByVal menuItem As ToolStripMenuItem, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DropDownMenuListClick")

        Call Me.NextForm(frm, menuItem)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DropDownMenuListClick")

    End Sub

    ''' <summary>
    ''' タイマー起動イベントです
    ''' </summary>
    ''' <param name="frm">NFA020F</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub TimerMessageTick(ByVal frm As LMA020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DropDownMenuListClick")

        Call Me.StartTimer(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DropDownMenuListClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
