' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA010H : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMA010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Validate As LMA010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gamen As LMA010G

    ''' <summary>
    ''' Sessionクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Session As LMA010S

    ''' <summary>
    ''' アプリケーションVerion
    ''' </summary>
    ''' <remarks></remarks>
    Private _AppVersion As String = String.Empty

    ''' <summary>
    ''' 画面パターン
    ''' </summary>
    ''' <remarks>ログイン画面・パスワード変更画面かを設定します</remarks>
    Private _FormPattern As String = String.Empty

    Friend Property FormPattern() As String
        Get
            Return _FormPattern
        End Get
        Set(ByVal value As String)
            _FormPattern = value
        End Set
    End Property

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

        'EXEから受け取ったVersionを格納
        Me._AppVersion = prm.AppVersion

        '画面パターンの格納(ログイン画面からの呼び出しの場合ログイン変更画面とする)
        If "LMA020".Equals(Me.RootPGID) Then
            Me._FormPattern = LMA010C.FORM_PATTERN_CHANGE_PWD
        Else
            Me._FormPattern = LMA010C.FORM_PATTERN_LOGIN
        End If

        'フォームの作成
        Dim frm As LMA010F = New LMA010F(Me)

        'Validateクラスの設定
        Me._Validate = New LMA010V(Me, frm)

        'Gamenクラスの設定
        Me._Gamen = New LMA010G(Me, frm)

        'Sessionクラスの設定
        Me._Session = New LMA010S()

        'システムデータの取得
        MyBase.CacheSystemData()

        '画面IDの設定
        MyBase.SetPGID("LMA010")

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タブインデックスの設定
        Call Me._Gamen.SetTabIndex()

        'コントロール個別設定
        Call Me._Gamen.SetControl(Me._AppVersion, Me._FormPattern)

        If LMA010C.FORM_PATTERN_LOGIN.Equals(Me._FormPattern) = True Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "G014", New String() {"Login information"})
            'フォームの表示
            frm.Show()
        Else
            'メッセージの表示
            MyBase.ShowMessage(frm, "G014", New String() {"New password"})
            'フォームの表示
            frm.ShowDialog()

        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' ログイン処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub LoginControl(ByVal frm As LMA010F)

        '入力チェック
        If Me._Validate.IsInputCheck(Me._FormPattern) = False Then
            Exit Sub
        End If

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面ロック
        MyBase.LockedControls(frm)

        'ユーザー認証処理を行う
        Dim ds As UserInfoDS = New UserInfoDS()
        Dim dt As DataTable = ds.Tables("S_USER")
        Dim row As DataRow = dt.NewRow()
        row("USER_CD") = frm.txtUserId.TextValue
        row("PW") = frm.txtPassword.TextValue
        dt.Rows.Add(row)

        Dim rtn As Boolean = MyBase.Login(ds)

        If rtn = False Then

            MyBase.ShowMessage(frm)

            MyBase.UnLockedControls(frm)

        Else

            'メッセージラベルのクリア
            MyBase.ClearMessageAria(frm)

            '認証ＯＫの場合、マスタキャッシュデータの取得を行う。
            Call MyBase.CacheMasterData()

            'メニュー画面に遷移する
            Dim prm As LMFormData = New LMFormData()
            prm.AppVersion = Me._AppVersion
            LMFormNavigate.NextFormNavigate(Me, "LMA020", prm)

            MyBase.UnLockedControls(frm)

            '画面を閉じる(シャットダウンしないで画面を閉じる)
            frm.CloseShutDown = False
            frm.Close()

        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' パスワード変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangePassword(ByVal frm As LMA010F)

        '入力チェック
        If Me._Validate.IsInputCheck(Me._FormPattern) = False Then
            Exit Sub
        End If

        'WSA呼出 (ユーザーマスタメンテの更新処理を利用)
        Dim prmDs As DataSet = Me.SetTCustData(Me._Session.SetPrameterDs(frm, Me.GetUserData()))
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM010BLF", "UpdateData", prmDs)

        'メッセージコードの判定
        If Me.IsErrorMessageExist = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, MyBase.GetMessageID())

            'フォーカスの設定
            frm.txtPassword.Focus()

        Else

            frm.CloseShutDown = False
            frm.Close()

        End If

    End Sub

    ''' <summary>
    ''' キャッシュのユーザ情報
    ''' </summary>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetUserData() As DataRow

        Return MyBase.GetLMCachedDataTable([Const].LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LMUserInfoManager.GetUserID(), "' "))(0)

    End Function

    ''' <summary>
    ''' 初期荷主情報を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetTCustData(ByVal ds As DataSet) As DataSet

        '初期荷主情報をキャッシュから設定
        Dim tcustDt As DataTable = ds.Tables(LMControlC.LMM010C_TABLE_NM_OUT2)
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable([Const].LMConst.CacheTBL.TCUST).Select(String.Concat("USER_CD = '", ds.Tables(LMControlC.LMM010C_TABLE_NM_IN).Rows(0).Item("USER_CD").ToString(), "' "))
        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max
            tcustDt.ImportRow(drs(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMA010F, ByVal e As FormClosingEventArgs)

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' プロセスを探し終了させる
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ShutDownLauncherProcesses()

        '指定EXEを探し終了させる 
        Dim pr As Process = Nothing

        '①ローカルで実行中のプロセス
        Dim localByName As Process() = Process.GetProcessesByName("Jp.Co.Nrs.LM.Launcher.vshost")

        '起動中のEXEを取得 
        For Each pr In localByName
            '指定のファイル名があれば終了する 
            If pr.ProcessName.Trim() = "Jp.Co.Nrs.LM.Launcher.vshost" Then
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

        '①ローカルで実行中のプロセス
        Dim clickOnceByName As Process() = Process.GetProcessesByName("LMS_2017")
        '起動中のEXEを取得 
        For Each pr In clickOnceByName
            '指定のファイル名(Jp.Co.Nrs.LM.Launcher.vshost)があれば終了する 
            If pr.ProcessName.Trim() = "LMS_2017" Then
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

    End Sub

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMA010F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        'ここから各処理を呼び出してください。 
        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' ＯＫボタン押下時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnOkClick(ByVal frm As LMA010F, ByVal e As System.EventArgs)

        If LMA010C.FORM_PATTERN_LOGIN.Equals(Me._FormPattern) = True Then
            Call Me.LoginControl(frm)
        Else
            Call Me.ChangePassword(frm)
        End If
    End Sub

    ''' <summary>
    ''' キャンセルボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnCancelClick(ByVal frm As LMA010F, ByVal e As System.EventArgs)

        'ここから各処理を呼び出してください。  
        If LMA010C.FORM_PATTERN_LOGIN.Equals(Me._FormPattern) = True Then
            frm.CloseShutDown = True
        Else
            frm.CloseShutDown = False
        End If

        frm.Close()

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
