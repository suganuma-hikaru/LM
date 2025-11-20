Imports System.IO
Imports System.Reflection
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread.Model

''' <summary>
''' DummyLauncherHクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' </histry>
Public Class DummyLauncherH
    Inherits LMBaseGUIHandler

    ''' <summary>
    ''' アプリケーションスタート
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Main(ByVal prm As LMFormData)

        Dim luncherForm As New DummyLauncherF()

        '画面初期値セット
        Call Me.SetFormData(luncherForm)

        ''システムデータの取得を行う
        Call Me.CacheSystemData()

        'デフォルトユーザーの認証を行う
        Call Me.ExcuteLogin(luncherForm)

        'キャッシュデータの取得を行う
        Call Me.CacheMasterData()

        'タブインデックスの設定
        With luncherForm
            .txtUserID.TabIndex = 0
            .txtPassword.TabIndex = 1
            .btnLogin.TabIndex = 2
            .cmbRecStatus.TabIndex = 3
            .cmbSkpFlg.TabIndex = 4
            .cmbRtnFlg.TabIndex = 5
        End With

        luncherForm.Show()

        luncherForm.Activate()

        luncherForm.ClassListBox.Focus()

        luncherForm.ClassListBox.Select()

        luncherForm.btnExecute.Enabled = True

        'メッセージ言語タイプを設定します。
        luncherForm.optJp.Checked = True

    End Sub

    ''' <summary>
    ''' 画面初期値を設定します。
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetFormData(ByVal frm As DummyLauncherF)

        '-------[AssemblyListBoxの構築] 自分のディレクトリのGUI.DLLを取得 ------------------------------
        Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*GUI*.dll")
        Dim fileList As ArrayList = New ArrayList
        For Each fileName As String In files
            Dim last As Integer = fileName.LastIndexOf(Path.DirectorySeparatorChar)
            fileList.Add(fileName.Substring(last + 1))
        Next

        frm.AssemblyListBox.DataSource = fileList
        If fileList.Count > 0 Then
            ClassLoad(frm)
        Else
            MsgBox("ロード可能なDLLがありません", MsgBoxStyle.Critical)
        End If
        files = Nothing
        '------------------------------------------------------------------------------------------------

        '-------[User情報の構築]端末ログインユーザー情報を取得 ------------------------------------------
        Dim str As String() = My.User.Name.Split(CChar("\"))
        ''frm.txtUserID.Text = str(1)
        ''frm.txtPassword.Text = str(1)
        frm.txtUserID.Text = "ADMIN"    '"ADMIN"   '"TESTUSER"
        frm.txtPassword.Text = "ADMIN"  '"ADMIN"   '"TESTUSER"
        Call Me.ViewUserInfo(frm)

        '------------------------------------------------------------------------------------------------

        '-------[cmbRecStatusの構築] --------------------------------------------------------------------
        Call Me.InitCmbRecStatus(frm)
        '------------------------------------------------------------------------------------------------

        frm.cmbSkpFlg.SelectedIndex = 1
        frm.cmbRtnFlg.SelectedIndex = 1
        frm.cmbMultiFlg.SelectedIndex = 1

        '上書きモード
        frm.pramView.EditModeReplace = True

    End Sub

    ''' <summary>
    ''' システムキャッシュデータを取得します。
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overloads Sub btnGetMasterClick(ByVal frm As DummyLauncherF)

        frm.ToolStripStatusLabel1.Text = ""
        frm.btnGetMaster.Enabled = False
        frm.btnExecute.Enabled = False

        Call Me.CacheMasterData()

        frm.btnGetMaster.Enabled = True
        frm.btnExecute.Enabled = True

        frm.ToolStripStatusLabel1.Text = "マスタ取得完了　" & My.Computer.Clock.LocalTime

    End Sub

    ''' <summary>
    ''' DLLリストボックス内で選ばれたDLL内のフォームを読み出し、フォームリストボックスに表示します
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Friend Sub ClassLoad(ByVal luncherForm As DummyLauncherF)
        Dim loadedAssem As [Assembly] = SelectedAssembly(luncherForm)
        Dim typeList As Collections.ArrayList = New ArrayList
        Dim types As Type() = loadedAssem.GetExportedTypes()

        For Each oneType As Type In types
            If oneType.IsSubclassOf(GetType(LMBaseGUIHandler)) Then
                If -1 = oneType.FullName.IndexOf("DummyLauncherH") Then

                    typeList.Add(oneType.FullName)

                End If
            End If
        Next

        If typeList.Count > 0 Then

            luncherForm.btnExecute.Visible = True
            luncherForm.ClassListBox.Focus()

            luncherForm.ClassListBox.DataSource = typeList

        Else
            luncherForm.btnExecute.Visible = False

            'MessageBox.Show("ロード可能なHandlerがありません", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' DLLリストボックス内で選ばれたDLLのインスタンスを取得します
    ''' </summary>
    ''' <param name="luncherForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectedAssembly(ByVal luncherForm As DummyLauncherF) As [Assembly]

        Dim dllName As String = luncherForm.AssemblyListBox.SelectedItem.ToString()

        Dim assemName As AssemblyName = New AssemblyName
        assemName.Name = dllName

        Dim loadedAssem As [Assembly] = [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

        Return loadedAssem
    End Function

    ''' <summary>
    ''' フォームリスト内で選択されたハンドラクラスを呼び出します
    ''' </summary>
    ''' <param name="luncherForm"></param>
    ''' <remarks></remarks>
    Friend Sub Execute(ByVal luncherForm As DummyLauncherF)

        luncherForm.btnExecute.Enabled = False

        Dim loadedAssem As [Assembly] = SelectedAssembly(luncherForm)

        '～～～～～～～～～～～～～　選択クラス呼び出し　～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～

        Dim typeName As String = luncherForm.ClassListBox.SelectedItem.ToString()

        Dim nextHandler As LMBaseGUIHandler = CType(loadedAssem.CreateInstance(typeName), LMBaseGUIHandler)

        '==============　ユーザー情報セット　==============

        Call Me.SetUserInfoData(luncherForm)

        '==============　ダミーデータセット　==============

        Dim mi2 As MethodInfo = nextHandler.GetType.GetMethod("Main")

        If 0 < mi2.GetParameters.Length Then

            Dim prm As LMFormData = CType(GetType(LMBaseGUIHandler).Assembly.CreateInstance(mi2.GetParameters(0).ParameterType.FullName), LMFormData)


            '==============　ダミーパラメータセット　==============

            SetDummyParam(luncherForm, prm)

            '==============　ダミーパラメータセット　==============

            Jp.Co.Nrs.LM.Base.LMFormNavigate.NextFormNavigate(nextHandler, typeName.Substring(17, 6) _
                                                                    , CType(prm, FormData))

            Dim ds As DataSet = prm.ParamDataSet

        Else

            Jp.Co.Nrs.LM.Base.LMFormNavigate.NextFormNavigate(nextHandler, typeName.Substring(17, 6) _
                                                                     , Nothing)
        End If


        luncherForm.btnExecute.Enabled = True

    End Sub

    ''' <summary>
    ''' ユーザー情報のデータテーブルのフィールドをスプレッドに表示します
    ''' </summary>
    ''' <param name="luncherForm"></param>
    ''' <remarks></remarks>
    Friend Sub ViewUserInfo(ByVal luncherForm As DummyLauncherF)

        luncherForm.btnLogin.Enabled = False

        '自分のディレクトリのDSL.DDL
        Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Jp.Co.Nrs.LM.Library.dll")

        'If 0 < files.Length Then

        '    Dim dllName As String = files(0).ToString()

        '    Dim assemName As AssemblyName = New AssemblyName
        '    assemName.Name = dllName

        '    Dim loadedAssemDSL As [Assembly] = [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

        '    Dim userDS As DataSet = CType(loadedAssemDSL.CreateInstance("Jp.Co.Nrs.LM.Library.UserInfoDS"), DataSet)

        '    If Not IsNothing(userDS) Then

        '        'データセットの内容を表示する
        '        Me.DispDataTableToSprUserPram(luncherForm, userDS)

        '    End If

        '    luncherForm.btnLogin.Enabled = True

        'End If

    End Sub


    ''' <summary>
    ''' ダミーパラメータ設定
    ''' </summary>
    ''' <param name="prm"></param>
    ''' <remarks>各画面のDataSetに設定します</remarks>
    Private Sub SetDummyParam(ByVal luncherForm As DummyLauncherF, ByRef prm As LMFormData)

        luncherForm.btnExecute.Enabled = False

        '自分のディレクトリのDSL.DLL
        Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*DSL*.dll")

        If 0 < files.Length Then

            Dim dllName As String = files(0).ToString()

            Dim assemName As AssemblyName = New AssemblyName
            assemName.Name = dllName

            Dim loadedAssemDSL As [Assembly] = [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

            Dim handlerName As String = luncherForm.ClassListBox.SelectedItem.ToString()

            Dim paramDS As DataSet = CType(loadedAssemDSL.CreateInstance("Jp.Co.Nrs.LM.DSL." & handlerName.Substring(17, 6) & "DS"), DataSet)
            If Not IsNothing(paramDS) Then

                'スプレッドの各シートから対応するデータセットのテーブルへデータを設定する
                For Each sheet As FarPoint.Win.Spread.SheetView In luncherForm.pramView.Sheets

                    Dim tbl As DataTable = paramDS.Tables(sheet.SheetName)

                    For i As Integer = 0 To sheet.RowCount - 1

                        Dim prmRow As DataRow = tbl.NewRow

                        With sheet

                            For j As Integer = 0 To tbl.Columns.Count - 1
                                prmRow(.ColumnHeader.Columns(j).Label) = .Cells(i, j).Text
                            Next

                        End With

                        tbl.Rows.Add(prmRow)

                    Next

                Next

            End If

            If Not String.IsNullOrEmpty(luncherForm.cmbRecStatus.Text) Then

                prm.RecStatus = CStr(luncherForm.cmbRecStatus.SelectedValue)

            End If

            prm.ReturnFlg = CBool(luncherForm.cmbRtnFlg.Text)
            prm.SkipFlg = CBool(luncherForm.cmbSkpFlg.Text)
            prm.MultiSelection = CBool(luncherForm.cmbMultiFlg.Text)

            prm.ParamDataSet = paramDS

        End If

    End Sub

    ''' <summary>
    ''' ユーザー情報設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>新規UserInfoDSにSpreadより設定しなおします</remarks>
    Private Sub SetUserInfoData(ByVal frm As DummyLauncherF)

        Dim ds As DataSet = New UserInfoDS
        Dim row As DataRow = ds.Tables("S_USER").NewRow

        'スプレッドの各シートから対応するデータセットのテーブルへデータを設定する
        For Each sheet As FarPoint.Win.Spread.SheetView In frm.sprUserPram.Sheets

            Dim tbl As DataTable = ds.Tables(sheet.SheetName)
            tbl.Rows.Add(tbl.NewRow())

            For i As Integer = 0 To sheet.RowCount - 1

                With sheet

                    For j As Integer = 0 To tbl.Columns.Count - 1
                        tbl.Rows(0)(.ColumnHeader.Columns(j).Label) = .Cells(i, j).Text
                    Next

                End With

            Next

            BaseGUIHandler.CachedUserInfoDataSet = ds

        Next

    End Sub

    ''' <summary>
    ''' フォームリスト内で選択されたハンドラクラスの引数のデータセット内容をスプレッドに表示します
    ''' </summary>
    ''' <param name="luncherForm"></param>
    ''' <remarks></remarks>
    Friend Sub ViewParam(ByVal luncherForm As DummyLauncherF)

        luncherForm.btnExecute.Enabled = False

        '自分のディレクトリのDSL.DLL
        Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*DSL*.dll")

        If 0 < files.Length Then

            Dim dllName As String = files(0).ToString()

            Dim assemName As AssemblyName = New AssemblyName
            assemName.Name = dllName

            Dim loadedAssemDSL As [Assembly] = [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

            Dim handlerName As String = luncherForm.ClassListBox.SelectedItem.ToString()

            Dim paramDS As DataSet = CType(loadedAssemDSL.CreateInstance("Jp.Co.Nrs.LM.DSL." & handlerName.Substring(17, 6) & "DS"), DataSet)
            If Not IsNothing(paramDS) Then

                'データセットの内容を表示する
                Me.DispDataTableToPramView(luncherForm, paramDS)

            End If

            luncherForm.btnExecute.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' データセットに定義されているINテーブルに対応したシートを作成し、スプレッドに表示する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub DispDataTableToPramView(ByVal luncherForm As DummyLauncherF, ByRef ds As DataSet)

        luncherForm.pramView.Sheets.Count = 0

        Dim tblNameList As ArrayList = New ArrayList

        'データセットに定義されているINテーブルをリストに追加する
        For Each tbl As DataTable In ds.Tables

            If "IN".Equals(Microsoft.VisualBasic.Right(tbl.TableName, 2)) Then

                tblNameList.Add(tbl.TableName)

            End If

        Next

        tblNameList.Sort()

        'データセットに定義されているINテーブルに対応したシートを作成する
        For Each tblName As String In tblNameList

            Dim newsheet As New FarPoint.Win.Spread.SheetView()

            newsheet.SheetName = tblName

            Dim tbl As DataTable = ds.Tables(tblName)

            newsheet.ColumnCount = tbl.Columns.Count

            'ヘッダーの設定
            For i As Integer = 0 To tbl.Columns.Count - 1
                newsheet.SetColumnLabel(0, i, tbl.Columns(i).ColumnName())
            Next

            'ヘッダーの高さ
            newsheet.ColumnHeader.Rows(0).Height = 30

            newsheet.RowCount = 1

            'セルに設定するスタイルの取得
            Dim cellStyle As FarPoint.Win.Spread.StyleInfo = LMSpreadUtility.GetTextCell(luncherForm.pramView, InputControl.ALL_MIX, 100, False)

            Dim styleModel As DefaultSheetStyleModel = _
                DirectCast(newsheet.Models.Style, DefaultSheetStyleModel)

            'セル設定
            For i As Integer = 0 To newsheet.ColumnCount - 1

                'セルスタイル設定
                styleModel.SetDirectInfo(0, i, cellStyle)

                newsheet.Columns(i).Width = 70

            Next

            ' 新規シートをコントロールに追加します。
            luncherForm.pramView.Sheets.Add(newsheet)

        Next

    End Sub

    ''' <summary>
    ''' UserInfoDsに対応したシートを作成し、スプレッドに表示する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub DispDataTableToSprUserPram(ByVal luncherForm As DummyLauncherF, ByRef ds As DataSet)

        luncherForm.sprUserPram.Sheets.Count = 0

        Dim tblNameList As ArrayList = New ArrayList

        'データセットに定義されているテーブルをリストに追加する
        For Each tbl As DataTable In ds.Tables

            If "S_USER".Equals(tbl.TableName) Then

                tblNameList.Add(tbl.TableName)

            End If

        Next

        tblNameList.Sort()

        'データセットに定義されているINテーブルに対応したシートを作成する
        For Each tblName As String In tblNameList

            Dim newsheet As New FarPoint.Win.Spread.SheetView()

            newsheet.SheetName = tblName

            Dim tbl As DataTable = ds.Tables(tblName)

            newsheet.ColumnCount = tbl.Columns.Count

            'ヘッダーの設定
            For i As Integer = 0 To tbl.Columns.Count - 1
                newsheet.SetColumnLabel(0, i, tbl.Columns(i).ColumnName())
            Next

            'ヘッダーの高さ
            newsheet.ColumnHeader.Rows(0).Height = 30

            newsheet.RowCount = 1

            'セルに設定するスタイルの取得
            Dim cellStyle As FarPoint.Win.Spread.StyleInfo = LMSpreadUtility.GetTextCell(luncherForm.sprUserPram, InputControl.ALL_MIX, 100, False)

            Dim styleModel As DefaultSheetStyleModel = _
                DirectCast(newsheet.Models.Style, DefaultSheetStyleModel)

            'セル設定
            For i As Integer = 0 To newsheet.ColumnCount - 1

                'セルスタイル設定
                styleModel.SetDirectInfo(0, i, cellStyle)

                newsheet.Columns(i).Width = 70

            Next

            ' 新規シートをコントロールに追加します。
            luncherForm.sprUserPram.Sheets.Add(newsheet)

        Next

        luncherForm.btnLogin.Enabled = True

    End Sub

    ''' <summary>
    ''' ログインボタンクリック時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnLoginClick(ByVal frm As DummyLauncherF)

        Dim rtn As Boolean = Me.ExcuteLogin(frm)
        If rtn = False Then
            MsgBox("ユーザーIDもしくはパスワードが一致しません。再度、入力してください。", MsgBoxStyle.Critical)
        Else
            MsgBox("認証ＯＫ！", MsgBoxStyle.Information)
        End If


    End Sub

    ''' <summary>
    ''' ログイン処理実行
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ExcuteLogin(ByVal frm As DummyLauncherF) As Boolean

        Dim ds As UserInfoDS = New UserInfoDS
        Dim row As DataRow = ds.Tables("S_USER").NewRow
        row("USER_CD") = frm.txtUserID.Text
        row("PW") = frm.txtPassword.Text
        ds.Tables("S_USER").Rows.Add(row)

        Dim rtn As Boolean = Me.Login(ds)

        If rtn = False Then
            Me.ClearMessageData()
        Else
            Call Me.SetUserInfoDsToSprUserPram(frm, BaseGUIHandler.CachedUserInfoDataSet)
        End If

        Return rtn

    End Function

    ''' <summary>
    ''' 取得したユーザー情報をスプレッドシートに設定する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetUserInfoDsToSprUserPram(ByVal frm As DummyLauncherF, ByVal ds As DataSet)

        'データセットの内容を表示する
        Me.DispDataTableToSprUserPram(frm, ds)

        Dim row As DataRow = ds.Tables("S_USER").Rows(0)

        With frm.sprUserPram.Sheets(0)

            'スプレッドの各シートから対応するデータセットのテーブルへデータを設定する
            For Each sheet As FarPoint.Win.Spread.SheetView In frm.sprUserPram.Sheets

                Dim tbl As DataTable = ds.Tables(sheet.SheetName)

                For i As Integer = 0 To sheet.RowCount - 1

                    With sheet

                        For j As Integer = 0 To tbl.Columns.Count - 1
                            .Cells(i, j).Text = tbl.Rows(0)(.ColumnHeader.Columns(j).Label).ToString
                        Next

                    End With

                Next

            Next

        End With

    End Sub

    ''' <summary>
    ''' デバッグプロセスを探し終了させる
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ShutDownLauncherProcesses()

        '指定EXEを探し終了させる 
        Dim localByName As Process() = Process.GetProcessesByName("Jp.Co.Nrs.LM.Launcher.vshost")
        Dim pr As Process
        '起動中のEXEを取得 
        For Each pr In localByName
            '指定のファイル名(Jp.Co.Nrs.LM.Launcher.vshost)があれば終了する 
            If pr.ProcessName = "Jp.Co.Nrs.LM.Launcher.vshost" Or pr.ProcessName = "Jp.Co.Nrs.LM.Launcher" Then
                '指定のウィンドウにクローズ メッセージを送信して、プロセスを終了 
                'MessageBox.Show("Jp.Co.Nrs.LM.Launcher.vshostを閉じます") 
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

        '①ローカルで実行中のプロセス
        Dim clickOnceByName As Process() = Process.GetProcessesByName("LMS")
        '起動中のEXEを取得 
        For Each pr In clickOnceByName
            '指定のファイル名(DEN)があれば終了する 
            If Trim(pr.ProcessName) = "LMS" Then
                pr.CloseMainWindow()
                pr.Kill()
            End If
        Next

    End Sub

    ''' <summary>
    ''' レコードステータスコンボボックスを構築します
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitCmbRecStatus(ByVal frm As DummyLauncherF)

        ' ArrayListにデータを追加します。
        Dim listData As ArrayList = New ArrayList()
        listData.Add(New RecordStatusData("", RecordStatus.INIT))
        listData.Add(New RecordStatusData("正常", RecordStatus.NOMAL_REC))
        listData.Add(New RecordStatusData("削除", RecordStatus.DELETE_REC))
        listData.Add(New RecordStatusData("新規", RecordStatus.NEW_REC))
        listData.Add(New RecordStatusData("複写", RecordStatus.COPY_REC))
        ' DataSourceプロパティを使ってArrayListをコントロールに追加します。
        frm.cmbRecStatus.DataSource = listData
        frm.cmbRecStatus.DisplayMember = "DisplayData"
        frm.cmbRecStatus.ValueMember = "ValueData"

    End Sub

End Class

' RecordStatusに設定するデータを保持するクラスを作成します。
''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class RecordStatusData
    Private _DisplayData As String
    Private _ValueData As String

    Public Sub New(ByVal display As String, ByVal vData As String)
        MyBase.New()
        Me._DisplayData = display
        Me._ValueData = vData
    End Sub

    Public ReadOnly Property DisplayData() As String
        Get
            Return _DisplayData
        End Get
    End Property

    Public ReadOnly Property ValueData() As String
        Get
            Return _ValueData
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.DisplayData + " - " + Me.ValueData.ToString()
    End Function

End Class
