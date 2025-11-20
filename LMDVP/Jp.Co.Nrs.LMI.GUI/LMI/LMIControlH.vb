' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI          : 
'  プログラムID     :  LMIControlH  : LMI画面 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.IO
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMIControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/07/04 ito
''' </histry>
Public Class LMIControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String
#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="pgid"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal pgid As String)

        MyBase.New()
        MyBase.SetPGID(pgid)

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal frm As Form, ByVal pgid As String)

        MyBase.New()
        MyBase.SetPGID(pgid)
        Me._Pgid = pgid

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As Form, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 処理終了メッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <remarks></remarks>
    Friend Sub SetMessageG002(ByVal frm As Form, ByVal msg1 As String, ByVal msg2 As String)

        MyBase.ShowMessage(frm, "G002", New String() {msg1, msg2})

    End Sub

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' 別PG起動処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="id">画面ID</param>
    ''' <param name="recType">レコードタイプ 初期値 = ""</param>
    ''' <param name="skipFlg">画面表示フラグ 初期値 = False</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Friend Function FormShow(ByVal ds As DataSet, ByVal id As String _
                             , Optional ByVal recType As String = "" _
                             , Optional ByVal skipFlg As Boolean = False) As LMFormData

        'パラメータ設定
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.RecStatus = recType
        prm.SkipFlg = skipFlg

        '画面起動
        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function ServerAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMIControlC.BLF), actionId, ds)

    End Function

    ''' <summary>
    ''' In情報を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetInData(ByVal ds As DataSet, ByVal setDs As DataSet) As DataSet
        Dim dt As DataTable = ds.Tables(LMI020C.TABLE_NM_IN).Copy
        setDs.Tables.Add(dt)
        dt = ds.Tables(LMI020C.TABLE_NM_COUNT).Copy
        setDs.Tables.Add(dt)
        Return setDs
    End Function

    ''' <summary>
    ''' ファイル保存 , 印刷処理
    ''' </summary>
    ''' <param name="xlApp">Excelアプリ</param>
    ''' <param name="xlSheet">Excelシート</param>
    ''' <param name="fileName">ファイル名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SaveFlieDataAndPrintAction(ByVal xlApp As Excel.Application _
                                                   , ByVal xlSheet As Excel.Worksheet _
                                                   , ByVal fileName As String _
                                                   ) As Boolean

        Dim fileNm As String = Me.GetFileNmAndAccess(fileName)

        '使用中かを判定
        If Me.CheckFileUsing(fileNm) = False Then
            MyBase.SetMessage("E353")
            Return False
        End If
        xlApp.DisplayAlerts = False
        xlSheet.SaveAs(fileNm)

        Return True

    End Function

    ''' <summary>
    ''' ファイル保存 , 印刷処理(デュポン在庫報告画面からのファイル作成時)
    ''' </summary>
    ''' <param name="xlApp">Excelアプリ</param>
    ''' <param name="xlSheet">Excelシート</param>
    ''' <param name="fileName">ファイル名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SaveFlieDataAndPrintAction2(ByVal xlApp As Excel.Application _
                                                   , ByVal xlSheet As Excel.Worksheet _
                                                   , ByVal filePath As String _
                                                   , ByVal fileName As String _
                                                   ) As Boolean

        '使用中かを判定
        If Me.CheckFileUsing(fileName) = False Then
            MyBase.SetMessage("E353")
            Return False
        End If
        xlApp.DisplayAlerts = False
        System.IO.Directory.CreateDirectory(filePath)
        xlSheet.SaveAs(String.Concat(filePath, fileName))

        Return True

    End Function

    ''' <summary>
    ''' ディレクトリ + ファイル名を取得
    ''' </summary>
    ''' <param name="fileNm">ファイル名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetFileNmAndAccess(ByVal fileNm As String) As String
        Return String.Concat(Me.GetDesktopAddress(), "\", fileNm)
    End Function

    ''' <summary>
    ''' ファイルが使用中かどうかをチェックする
    ''' </summary>
    ''' <param name="vstrFilePath">ファイルの絶対パス</param>
    ''' <returns>True:使用中ではないFalse:使用中</returns>
    ''' <remarks></remarks>
    Friend Function CheckFileUsing(ByVal vstrFilePath As String) As Boolean

        Dim file As FileInfo = New FileInfo(vstrFilePath)
        Dim strFileNameBK As String = file.Name

        'ファイルが存在しなければ、使用中ではない
        If file.Exists = False Then
            Return True
        End If

        Try
            'ファイル名を変更して、使用中かチェックする
            file.MoveTo(Path.Combine(file.DirectoryName, String.Concat(file.Name, ".BK")))

            'ファイル名を元に戻す
            file.MoveTo(Path.Combine(file.DirectoryName, strFileNameBK))

            'ファイル名の変更が成功したので、使用中ではない
            Return True

        Catch ex As Exception

            'ファイル名が変更できないので、使用中とする
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 作成ファイルの出力処理
    ''' </summary>
    ''' <param name="fileNm">絶対パス</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Friend Function OutFileOpen(ByVal fileNm As String()) As Boolean

        Dim file As FileInfo = Nothing

        Dim max As Integer = fileNm.Count - 1
        For i As Integer = 0 To max

            file = New FileInfo(fileNm(i))

            'ファイルが存在しない場合、スルー
            If file.Exists = False Then
                Continue For
            End If

            System.Diagnostics.Process.Start(fileNm(i))
        Next
        Call Me.MRComObject(file)
        Return True

    End Function

    ''' <summary>
    ''' インスタンス開放
    ''' </summary>
    ''' <typeparam name="T">型</typeparam>
    ''' <param name="objCom">開放するインスタンス</param>
    ''' <param name="force"></param>
    ''' <remarks></remarks>
    Friend Sub MRComObject(Of T As Class)(ByRef objCom As T, Optional ByVal force As Boolean = False)

        '既にインスタンスがない場合、スルー
        If objCom Is Nothing = True Then
            Return
        End If

        Try
            If System.Runtime.InteropServices.Marshal.IsComObject(objCom) = True Then

                If force = True Then

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objCom)

                Else

                    Dim count As Integer = System.Runtime.InteropServices.Marshal.ReleaseComObject(objCom)
                    Debug.WriteLine(count)  '0 以上が表示されたら、解放されていない分がある

                End If

            End If

        Finally

            objCom = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' デスクトップディレクトリを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDesktopAddress() As String
        Return System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    End Function

    'START YANAI 20120120 請求データ作成対応
    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form, Optional ByVal id As String = "G007")

        '画面解除
        MyBase.UnLockedControls(frm)

        'ガイダンスメッセージを表示
        Call Me.SetGMessage(frm, id)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="id">メッセージID</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As Form, ByVal id As String)

        'メッセージ欄に値がある場合、スルー
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = False Then
            Exit Sub
        End If

        MyBase.ShowMessage(frm, id)

    End Sub

    ''' <summary>
    ''' WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="BLF">BLFファイル名</param>
    ''' <param name="methodName">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByRef frm As Form, _
                                  ByVal BLF As String, _
                                  ByVal methodName As String, _
                                  ByRef rtDs As DataSet, _
                                  ByVal rc As Integer, _
                                  Optional ByVal mc As Integer = -1, _
                                  Optional ByVal skipMode As Boolean = False) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)

        Dim rtnDs As DataSet = MyBase.CallWSA(BLF, methodName, rtDs)

        If skipMode = True Then
            Return rtnDs
        End If

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(BLF, methodName, rtDs)

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me.FailureSelect(frm)
                    Return Nothing

                End If

            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時、共通処理を行う
                Call Me.FailureSelect(frm)
                Return rtnDs

            End If
        Else
            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub FailureSelect(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub
    'END YANAI 20120120 請求データ作成対応

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal frm As Form, ByVal objNm As String) As Win.InputMan.LMImTextBox
        Return DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)
    End Function

#End Region

End Class
