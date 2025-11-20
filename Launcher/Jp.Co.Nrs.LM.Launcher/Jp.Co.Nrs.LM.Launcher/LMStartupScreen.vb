Imports System.IO
Imports System.Xml
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Collections
Imports System.Threading

Imports Jp.Co.Nrs.Win.GUI
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Base


Public NotInheritable Class LMStartupScreen

    Private _StartupFlg As String

    ''Sub Main(ByVal args() As String)

    ''    'コマンドライン引数を取得
    ''    Dim _StartupFlg As String = args(0).ToString

    ''    Me.Show()

    ''End Sub

    'TODO: このフォームは、プロジェクト デザイナ ([プロジェクト] メニューの下の [プロパティ]) の [アプリケーション] タブを使用して、
    '  アプリケーションのスプラッシュ スクリーンとして簡単に設定することができます


    Private Sub LMStartupScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'アプリケーションのアセンブリ情報に従って、ランタイムにダイアログ テキストを設定します。  

        'TODO: [プロジェクト] メニューの下にある [プロジェクト プロパティ] ダイアログの [アプリケーション] ペインで、アプリケーションのアセンブリ情報を 
        '  カスタマイズします

        'アプリケーション タイトル
        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'アプリケーション タイトルがない場合は、拡張子なしのアプリケーション名を使用します
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        'デザイン時に書式設定文字列としてバージョン管理で設定されたテキストを使用して、バージョン情報の書式を
        '  設定します。これにより効率的なローカリゼーションが可能になります。
        '  ビルドおよび リビジョン情報は、次のコードを使用したり、バージョン管理のデザイン時のテキストを 
        '  "Version {0}.{1:00}.{2}.{3}" のように変更したりすることによって含めることができます。
        '  詳細については、ヘルプの String.Format() を参照してください。
        '
        '    Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        '著作権情報
        Copyright.Text = My.Application.Info.Copyright

    End Sub


    Private Sub SplashScreen1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        ''============================

        AddHandler Application.ThreadException, AddressOf LMStartupScreen.CatchAllException

        'Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Jp.Co.Nrs.LMA.GUI.dll")

        'Dim dllName As String = files(0)

        'Dim assemName As AssemblyName = New AssemblyName
        'assemName.Name = dllName

        'Dim loadedAssem As [Assembly] = [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

        Dim files As String() = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Jp.Co.Nrs.LM*.GUI.dll")

        For Each dllName As String In files

            [Assembly].LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllName))

        Next

        'Dim typeName As String = "Jp.Co.Nrs.LM.GUI.DummyLauncherH"

        'Dim handler As Object = loadedAssem.CreateInstance(typeName)

        Dim handler As Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

        'メニュー OR DummyLauncherの起動切り替えはここで行う！-----------------------------------------------------

        'handler = New Jp.Co.Nrs.LM.GUI.DummyLauncherH
        handler = New Jp.Co.Nrs.LM.GUI.LMA010H

        '----------------------------------------------------------------------------------------------------------

        Dim mi2 As MethodInfo = handler.GetType.GetMethod("Main")

        'Dim session As Object = loadedAssem.CreateInstance(mi2.GetParameters(0).ParameterType.FullName)
        'Dim session As Jp.Co.Nrs.LM.GUI.DummyLauncherSS = New Jp.Co.Nrs.LM.GUI.DummyLauncherSS

        ''メソッドオブジェクトを取得
        'Dim mi As MethodInfo = handler.GetType.GetMethod("StartGUI")

        ''引数オブジェクトを作成
        'Dim methodParams As Object() = {session}

        'mi.Invoke(handler, methodParams)
        '        Version.Text = System.String.Format(Version.Text, My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        Dim prm As LMFormData = New LMFormData
        prm.AppVersion = My.Application.Info.Version.ToString
        handler.StartGUI(prm)
        '======================

        Me.Hide()

    End Sub

    Public Shared Sub CatchAllException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
        MsgBox("システム管理者までご連絡ください。")
        Console.WriteLine(t.Exception.StackTrace)
    End Sub

End Class
