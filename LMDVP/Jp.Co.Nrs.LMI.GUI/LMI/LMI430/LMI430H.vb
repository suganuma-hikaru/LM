' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.IO
Imports System.Reflection
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

''' <summary>
''' LMI430ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI430H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI430V = Nothing

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI430G = Nothing

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG = Nothing

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH = Nothing

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV = Nothing

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean = False


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly BLF_NAME As String = LMI430C.MY_FORM_ID & LMControlC.BLF

#End Region 'Field

#Region "Method"

#Region "初期処理"

    Private Property inRow As Object

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI430F = New LMI430F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI430G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI430V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI430C.EventShubetsu _
                           , ByVal frm As LMI430F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        Try

            '権限チェック
            If _V.IsAuthorityChk(eventShubetsu) = False Then
                MyBase.ShowMessage(frm, "E016")
                Exit Sub
            End If

            Select Case eventShubetsu

                Case LMI430C.EventShubetsu.LOAD_FILE    '取込

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    '入力チェック
                    If Me._V.IsSingleCheck(eventShubetsu) = False Then
                        '処理終了アクション
                        Me._LMIConH.EndAction(frm, LMI430C.MESSAGE_ID.NORMAL)
                        Exit Sub
                    End If

                    Me._PopupSkipFlg = False
                    If (Me.ShowPopupControl(frm _
                                           , frm.FunctionKey.F1ButtonName _
                                           , LMI430C.ActionType.LOAD_FILE)) Then
                        Call Me.RegisterLoadFile(frm)
                    End If

                Case LMI430C.EventShubetsu.SEARCH   ' 検索

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    '入力チェック
                    If Me._V.IsSingleCheck(eventShubetsu) = False Then
                        Exit Sub
                    End If

                    ' 一覧クリア要否指定
                    Dim doSpreadClear As Boolean = True

                    Me.SetSearchLimit()

                    Dim retry As Boolean = False

                    Do

                        Using result As DataSet = Me.GetLoadFileList(frm)

                            retry = False
                            'エラー時はメッセージを表示して終了   
                            If (MyBase.ShowMessage(frm) = MsgBoxResult.Ok) Then
                                retry = True
                                MyBase.SetForceOparation(retry)
                            End If

                            If (retry = False) Then
                                'スプレッドに設定
                                Me._G.SetSpread(frm.sprDetails _
                                              , result _
                                              , doSpreadClear)
                            End If
                        End Using
                    Loop While (retry)

                Case LMI430C.EventShubetsu.DELETE_SELECTED_ROW     '選択行削除

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    '選択チェック
                    If Me._V.IsSelectDataChk() = False Then
                        Exit Sub
                    End If

                    Me.DeleteRows(frm)

                Case LMI430C.EventShubetsu.CREATE_EXCEL     'Excel出力

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

#If False Then      'UPD 2017/04/24 daikoku
                    '選択チェック
                    If Me._V.IsSelectDataChk() = False Then
                        Exit Sub
                    End If

                    ' Excel作成
                    Call Me.CreateExcel(frm)
#Else
                    Dim temp As String = frm.cmbPrint.SelectedValue.ToString()

                    Select Case temp

                        Case "01"
                            '選択チェック
                            If Me._V.IsSelectDataChk() = False Then
                                Exit Sub
                            End If

                            ' Excel作成
                            Call Me.CreateExcel(frm)

                        Case "02"
                            '読取結果

                            Call CreateExcelReadResult(frm)
                    End Select
#End If

                Case LMI430C.EventShubetsu.OPEN_MASTER
                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    'カーソル位置の設定
                    Dim objNm As String = frm.FocusedControlName()

                    Me.ShowPopupControl(frm, objNm, LMI430C.ActionType.MASTER)

                Case LMI430C.EventShubetsu.CLOSE_FORM

                    frm.Close()
                    If (frm IsNot Nothing AndAlso _
                        frm.IsDisposed = False) Then
                        frm.Dispose()
                    End If

            End Select

        Finally

            If (frm IsNot Nothing AndAlso _
                frm.IsDisposed = False) Then

                '処理終了アクション
                Me._LMIConH.EndAction(frm, LMI430C.MESSAGE_ID.NORMAL)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI430F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.LOAD_FILE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.DELETE_SELECTED_ROW, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.SEARCH, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' Pause押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub PausePress(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.OPEN_MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.CREATE_EXCEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI430C.EventShubetsu.CLOSE_FORM, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI430F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMI430F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e.KeyCode = Keys.Enter)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け




#Region "処理"

#Region "検索"

    ''' <summary>
    ''' 取込済ファイルの一覧を取得する。
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLoadFileList(ByVal frm As LMI430F) As DataSet

        Using ds As New LMI430DS()

            Dim inRow As LMI430DS.LMI430INRow = ds.LMI430IN.NewLMI430INRow

            inRow.NRS_BR_CD = frm.cmbNrsBrCd.SelectedValue.ToString()
            inRow.CUST_CD_L = frm.txtCustCdL.TextValue
            inRow.CUST_CD_M = frm.txtCustCdM.TextValue
            inRow.INKA_DATE_FROM = frm.imdInkaDateFrom.TextValue
            inRow.INKA_DATE_TO = frm.imdInkaDateTo.TextValue

            ds.LMI430IN.AddLMI430INRow(inRow)

            Return MyBase.CallWSA(BLF_NAME _
                                , LMI430C.FUNCTION_NAME.SelectLoadedInkaCylFileList _
                                , ds)
        End Using

    End Function
#End Region

#Region "取込"

    ''' <summary>
    ''' 取込登録処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RegisterLoadFile(ByVal frm As LMI430F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Dim loadFilePath As String = ""

        Using dialog As New OpenFileDialog()
            dialog.Title = LMI430C.FILE_DIALOG.TITLE
            dialog.Filter = LMI430C.FILE_DIALOG.FILTER
            dialog.FilterIndex = 1
            dialog.Multiselect = False
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            If (dialog.ShowDialog() <> DialogResult.OK) Then
                Exit Sub
            End If

            loadFilePath = dialog.FileName
        End Using

        ' ファイル名
        If (Path.GetFileName(loadFilePath).Length > LMI430C.FILE_NAME_LENGTH) Then

            Me.ShowMessage(frm _
                         , "E482" _
                         , New String() {LMI430C.FILE_NAME_TEXT, LMI430C.FILE_NAME_LENGTH & LMI430C.CHARACTER_TEXT})

            Exit Sub
        End If


        ' エクセルファイルから必要な領域のデータを抽出
        Dim cellData(,) As Object = Me.ReadExcelFile(loadFilePath _
                                                   , LMI430C.CYL_FILE_FORMAT.TARGET_SHEET_NO _
                                                   , LMI430C.CYL_FILE_FORMAT.START_ROW_NO _
                                                   , LMI430C.CYL_FILE_FORMAT.START_COL_NO _
                                                   , LMI430C.CYL_FILE_FORMAT.COLUMN_COUNT)
        If (cellData Is Nothing) Then

            ' エラー
            MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})
            Exit Sub

        ElseIf (cellData.GetLength(LMI430C.ARRAY_INDEX.ROW_DIMENSION) > _
                LMI430C.CYL_FILE_FORMAT.MAX_ROW_COUNT) Then

            ' 取込データは、[%1]以下にしてください。
            MyBase.ShowMessage(frm _
                             , "E935" _
                             , New String() {LMI430C.CYL_FILE_FORMAT.MAX_ROW_COUNT.ToString()})
            Exit Sub

        End If

        Using loadData As DataSet = Me.CreateInsertDataSet(frm _
                                                       , cellData _
                                                       , Path.GetFileName(loadFilePath))
            'メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then
                MyBase.ShowMessage(frm, "E235")

                'EXCEL起動()
                MyBase.MessageStoreDownload()
                Exit Sub
            End If

            'エラー時はメッセージを表示して終了
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Exit Sub
            End If

            MyBase.CallWSA(BLF_NAME, LMI430C.FUNCTION_NAME.InsertCylinderData, loadData)

        End Using

        '処理終了アクション
        Me._LMIConH.EndAction(frm, LMI430C.MESSAGE_ID.NORMAL)


        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})


        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' 登録するシリンダーの妥当性を確認する。
    ''' </summary>
    ''' <param name="cylinder"></param>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCurrectCylinder(ByVal cylinder As LMI430DS.LMI430IN_CYLINDERRow _
                                     , ByVal rowIndex As Integer) As Boolean

        ' SetMessageStoreをVクラス内で利用できないため、本クラス内でチェック

        '行番号
        Dim rowNo As Integer = 0
        If (Int32.TryParse(cylinder.ROW_NO, rowNo) = False) Then

            ' [%1]は数値を入力してください。[%2]
            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                 , "E476" _
                                 , New String() {LMI430C.ROW_NO_TEXT, cylinder.ROW_NO} _
                                 , rowIndex.ToString())

            Return False

        ElseIf (cylinder.ROW_NO.Length > LMI430C.ROW_NO_LENGTH) Then

            ' [%1]は[%2]以下の値を入力してください。
            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                 , "E482" _
                                 , New String() {LMI430C.ROW_NO_TEXT, LMI430C.ROW_NO_LENGTH & LMI430C.CHARACTER_TEXT} _
                                 , rowIndex.ToString())

            Return False
        End If


        ' ガス種別
        If (cylinder.GAS_NAME.Length > LMI430C.GAS_NAME_LENGTH) Then

            ' [%1]は[%2]以下の値を入力してください。
            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                 , "E482" _
                                 , New String() {LMI430C.GAS_NAME_TEXT, LMI430C.GAS_NAME_LENGTH & LMI430C.CHARACTER_TEXT} _
                                 , rowIndex.ToString())

            Return False

        End If


        ' 容量
        If (cylinder.VOLUME.Length > LMI430C.VOLUME_LENGTH) Then

            ' [%1]は[%2]以下の値を入力してください。
            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                 , "E482" _
                                 , New String() {LMI430C.VOLUME_TEXT, LMI430C.VOLUME_LENGTH & LMI430C.CHARACTER_TEXT} _
                                 , rowIndex.ToString())

            Return False

        End If


        ' シリアル番号
        If (String.IsNullOrEmpty(cylinder.SERIAL_NO)) Then

            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                , "E001", New String() {LMI430C.SERIAL_NO_TEXT} _
                                , rowIndex.ToString())


            Return False

        ElseIf (cylinder.SERIAL_NO.Length > LMI430C.SERIAL_NO_LENGTH) Then

            ' [%1]は[%2]以下の値を入力してください。
            MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                 , "E482" _
                                 , New String() {LMI430C.SERIAL_NO_TEXT, LMI430C.SERIAL_NO_LENGTH & LMI430C.CHARACTER_TEXT} _
                                 , rowIndex.ToString())

            Return False

        End If

        Return True

    End Function


    ''' <summary>
    ''' シリアル番号が重複していないか確認する。
    ''' </summary>
    ''' <param name="serialNo">シリアル番号</param>
    ''' <param name="table">登録予定のシリンダー</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUniqueSerial(ByVal serialNo As String _
                                  , ByVal table As LMI430DS.LMI430IN_CYLINDERDataTable) As Boolean

        Return (table.AsEnumerable().Where(Function(r) r.SERIAL_NO.Equals(serialNo)).Count = 0)


    End Function


    ''' <summary>
    ''' 登録用のデータを設定する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="cellData"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateInsertDataSet(ByVal frm As LMI430F _
                                       , ByVal cellData As Object(,) _
                                       , ByVal fileName As String) As DataSet


        Dim createData As New LMI430DS()

        Dim nrsBrCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()

        For rowIndex As Integer = 1 To cellData.GetLength(LMI430C.ARRAY_INDEX.ROW_DIMENSION)

            Try
                Dim newCylinderRow As LMI430DS.LMI430IN_CYLINDERRow _
                    = createData.LMI430IN_CYLINDER.NewLMI430IN_CYLINDERRow

                newCylinderRow.NRS_BR_CD = nrsBrCd
                newCylinderRow.ROW_NO = If(cellData.GetValue(rowIndex, LMI430C.CYL_FILE_FORMAT.COLUMN_INDEX.ROW_NUMBER), "").ToString()
                newCylinderRow.GAS_NAME = If(cellData.GetValue(rowIndex, LMI430C.CYL_FILE_FORMAT.COLUMN_INDEX.GAS_NAME), "").ToString()
                newCylinderRow.VOLUME = If(cellData.GetValue(rowIndex, LMI430C.CYL_FILE_FORMAT.COLUMN_INDEX.VOLUME), "").ToString()
                newCylinderRow.SERIAL_NO = If(cellData.GetValue(rowIndex, LMI430C.CYL_FILE_FORMAT.COLUMN_INDEX.SERIAL_NO), "").ToString()


                If (Me.IsCurrectCylinder(newCylinderRow, rowIndex)) Then

                    If (Me.IsUniqueSerial(newCylinderRow.SERIAL_NO, createData.LMI430IN_CYLINDER) = False) Then

                        ' [%1]は重複しています。確認してください。[%2]
                        MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN _
                                             , "E496" _
                                             , New String() {LMI430C.SERIAL_NO_TEXT, newCylinderRow.SERIAL_NO} _
                                             , rowIndex.ToString())

                        Continue For
                    End If

                    createData.LMI430IN_CYLINDER.AddLMI430IN_CYLINDERRow(newCylinderRow)

                End If

            Catch ex As Exception

                Logger.WriteErrorLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, ex.Message, ex)

                ' 取込データに不正があります。確認してください。
                MyBase.SetMessageStore(LMI430C.GUIDANCE_KBN, "E100" _
                                     , Nothing _
                                     , rowIndex.ToString())


            End Try
        Next

        Dim newInRow As LMI430DS.LMI430INRow = createData.LMI430IN.NewLMI430INRow
        newInRow.NRS_BR_CD = nrsBrCd
        newInRow.CUST_CD_L = frm.txtCustCdL.TextValue
        newInRow.CUST_CD_M = frm.txtCustCdM.TextValue
        newInRow.INKA_DATE = frm.imdInkaDate.TextValue
        newInRow.REMARK_1 = frm.txtRemark1.TextValue
        newInRow.REMARK_2 = frm.txtRemark2.TextValue
        newInRow.REMARK_3 = frm.txtRemark3.TextValue

        newInRow.LOAD_FILE_NAME = fileName

        createData.LMI430IN.AddLMI430INRow(newInRow)


        Return createData


    End Function


#End Region

#Region "削除"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteRows(ByVal frm As LMI430F)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        If (Me.ShowMessage(frm, "C001", New String() {"削除"}) <> MsgBoxResult.Ok) Then
            Exit Sub
        End If

        Using ds As DataSet = SetInDataSelectedRow(frm)
            MyBase.CallWSA(BLF_NAME, LMI430C.FUNCTION_NAME.DeleteCylinderData, ds)
        End Using

        '処理終了アクション
        Me._LMIConH.EndAction(frm, LMI430C.MESSAGE_ID.NORMAL)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"削除処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

#End Region

#Region "Excel作成"

    ''' <summary>
    ''' Excel作成
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub CreateExcel(ByVal frm As LMI430F)

        Dim rtDs As DataSet = SetInDataSelectedRow(frm)


        ' 出力データ検索
        Dim selectResult As DataSet _
            = MyBase.CallWSA(BLF_NAME _
                           , LMI430C.FUNCTION_NAME.SelectInspectionData _
                           , rtDs)


        Dim saveFileSelectData As New LMI430DS()

        ' 検品データマージ
        saveFileSelectData.Merge(selectResult)

        Dim saveFolderPath As String = Me.GetSaveFolderPath()
        If (String.IsNullOrEmpty(saveFolderPath)) Then

            MyBase.ShowMessage(frm, "S001", New String() {"出力フォルダパスの取得"})

            Exit Sub

        End If

        For Each inRow As DataRow In rtDs.Tables(LMI430C.TABLE_NM.INPUT).Rows

            ' 印刷対象行取得
            Dim writeData As IEnumerable(Of LMI430DS.LMI430OUT_INSPECTION_DATARow) _
                = saveFileSelectData.LMI430OUT_INSPECTION_DATA _
                  .Where(Function(r) r.INKA_CYL_FILE_NO_L.Equals(inRow.Item(LMI430C.COL_NAME.INKA_CYL_FILE_NO_L)))

            Dim firstRow As LMI430DS.LMI430OUT_INSPECTION_DATARow = writeData.FirstOrDefault
            If (firstRow Is Nothing) Then

                Dim errRow As LMI430DS.LMI430INRow = DirectCast(inRow, LMI430DS.LMI430INRow)

                Me.SetMessageStore(LMI430C.GUIDANCE_KBN, "E011", Nothing, errRow.SPREAD_ROW_NO)

                Continue For
            End If

            ' ファイル名
            Dim saveFileName As String = Me.CreateSaveFileName(firstRow)

            ' シート名
            Dim sheetName As String = Me.CreateSheetName(firstRow)

            ' 出力データ
            Dim saveCellData(,) As String = Me.ConvertSaveCellData(writeData)

            ' Excelファイル保存
            If (Me.SaveExcelFile(saveFileName, saveFolderPath, sheetName, saveCellData) = False) Then

                Dim errRowNo As String = ""
                Dim errRow As LMI430DS.LMI430INRow = TryCast(inRow, LMI430DS.LMI430INRow)
                If (errRow IsNot Nothing) Then
                    errRowNo = errRow.SPREAD_ROW_NO
                End If

                Me.SetMessageStore(LMI430C.GUIDANCE_KBN, "E428" _
                                 , New String() {"保存中にエラーが発生した", "ファイル出力", ""} _
                                 , errRowNo)

            End If
        Next

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        System.Diagnostics.Process.Start(saveFolderPath)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

    ''' <summary>
    ''' Excel読み取り結果作成  ADD 2017/04/24
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub CreateExcelReadResult(ByVal frm As LMI430F)

        Dim rtDs As DataSet = SetReadResultSelectedRow(frm)


        ' 読取結果データ検索
        Dim selectResult As DataSet _
            = MyBase.CallWSA(BLF_NAME _
                           , LMI430C.FUNCTION_NAME.SelectReadResulData _
                           , rtDs)


        Dim saveFileSelectData As New LMI430DS()

        ' 読取データマージ
        saveFileSelectData.Merge(selectResult)

        Dim saveFolderPath As String = Me.GetSaveFolderPathRead()
        If (String.IsNullOrEmpty(saveFolderPath)) Then

            MyBase.ShowMessage(frm, "S001", New String() {"出力フォルダパスの取得"})

            Exit Sub

        End If

        ' 印刷対象行取得
        Dim writeData2 As IEnumerable(Of LMI430DS.LMI430OUT_READ_DATARow) _
            = saveFileSelectData.LMI430OUT_READ_DATA

        Dim firstRow2 As LMI430DS.LMI430OUT_READ_DATARow = writeData2.FirstOrDefault
        If (firstRow2 Is Nothing) Then

            MyBase.ShowMessage(frm, "E024")

            Exit Sub
        End If

        ' ファイル名
        Dim saveFileName As String = Me.CreateSaveFileName2(firstRow2)

        ' シート名
        Dim sheetName As String = Me.CreateSheetName2(firstRow2)

        ' 出力データ
        Dim saveCellData(,) As String = Me.ConvertSaveCellData2(writeData2)

        ' Excelファイル保存
        If (Me.SaveExcelFile(saveFileName, saveFolderPath, sheetName, saveCellData) = False) Then

            Me.SetMessageStore(LMI430C.GUIDANCE_KBN, "E428" _
                             , New String() {"保存中にエラーが発生した", "ファイル出力", ""}, "")

        End If

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        System.Diagnostics.Process.Start(saveFolderPath)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

    ''' <summary>
    ''' 検品結果一覧(DataRow)をExcel出力用の2次元配列へ変換する。
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertSaveCellData(ByVal rows As IEnumerable(Of LMI430DS.LMI430OUT_INSPECTION_DATARow)) As String(,)

        Dim excelData(rows.Count, LMI430C.SAVE_FILE_FORMAT.COLUMN_COUNT) As String

        Dim outRowCount As Integer = 0

        'タイトル列を設定
        For Each colIndex As LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX _
            In [Enum].GetValues(GetType(LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX))

            excelData(outRowCount, colIndex) = LMI430C.SAVE_FILE_FORMAT.TITLE(colIndex)
        Next

        If rows.Count = 0 Then
            Return excelData
        End If


        For Each row As LMI430DS.LMI430OUT_INSPECTION_DATARow In rows
            outRowCount += 1

            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.INKA_DATE) = row.INKA_DATE
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.SERIAL_NO) = row.SERIAL_NO
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.GAS_NAME) = row.GAS_NAME
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.VOLUME) = row.VOLUME
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.INSPECTION_USER_NM) = row.INSPECTION_USER_NM
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.IS_LOAD) = row.IS_LOAD
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX.IS_SCAN) = row.IS_SCAN
        Next

        Return excelData

    End Function


    ''' <summary>
    ''' 取込結果一覧(DataRow)をExcel出力用の2次元配列へ変換する。
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertSaveCellData2(ByVal rows As IEnumerable(Of LMI430DS.LMI430OUT_READ_DATARow)) As String(,)

        Dim excelData(rows.Count, LMI430C.SAVE_FILE_FORMAT.COLUMN_COUNT) As String

        Dim outRowCount As Integer = 0

        'タイトル列を設定
        For Each colIndex As LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2 _
            In [Enum].GetValues(GetType(LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2))

            excelData(outRowCount, colIndex) = LMI430C.SAVE_FILE_FORMAT.TITLE2(colIndex)
        Next

        If rows.Count = 0 Then
            Return excelData
        End If


        For Each row As LMI430DS.LMI430OUT_READ_DATARow In rows
            outRowCount += 1

            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.INKA_CYL_FILE_NO_L) = row.INKA_CYL_FILE_NO_L
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.SERIAL_NO) = row.SERIAL_NO
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.INSPECTION_DATE) = row.INSPECTION_DATE
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.INSPECTION_TIME) = row.INSPECTION_TIME
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.INSPECTION_USER_NM) = row.INSPECTION_USER_NM

            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.CUST_CD_L) = row.CUST_CD_L
            excelData(outRowCount, LMI430C.SAVE_FILE_FORMAT.COLUMN_INDEX2.CUST_CD_M) = row.CUST_CD_M

        Next

        Return excelData

    End Function

    ''' <summary>
    ''' 保存フォルダ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSaveFolderPath() As String

        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI430C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI430C.SAVE_FILE_KBN.KBN_CD)

        Dim folderPath As String = String.Empty
        If (saveInfoRow IsNot Nothing) Then

            folderPath = Path.Combine(saveInfoRow.Item(LMI430C.SAVE_FILE_KBN.FOLDER_PATH_COL).ToString() _
                                    , DateTime.Now.ToString("yyyyMMdd"))

            Directory.CreateDirectory(folderPath)
        End If


        Return folderPath

    End Function


    ''' <summary>
    ''' 保存ファイル名生成
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSaveFileName(ByVal row As LMI430DS.LMI430OUT_INSPECTION_DATARow) As String


        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI430C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI430C.SAVE_FILE_KBN.KBN_CD)

        Dim fileNamePrefix As String = ""
        If (saveInfoRow IsNot Nothing) Then
            fileNamePrefix = saveInfoRow.Item(LMI430C.SAVE_FILE_KBN.FILE_PREFIX_COL).ToString()
        End If


        Dim loadFileNameWithoutExt As String = Path.GetFileNameWithoutExtension(row.LOAD_FILE_NAME)

        Dim fileCreateTime As String = DateTime.Now.ToString("yyyyMMddhhmmssffff")

        ' 接頭文字 + 元のファイル名 + 出力日時 + 拡張子
        Return String.Format("{0}{1}_{2}{3}" _
                            , fileNamePrefix _
                            , loadFileNameWithoutExt _
                            , fileCreateTime _
                            , LMI430C.SAVE_FILE_FORMAT.SAVE_FILE_EXTENSION)


    End Function

    ''' <summary>
    ''' 保存シート名生成
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSheetName(ByVal row As LMI430DS.LMI430OUT_INSPECTION_DATARow) As String

        Const EXCEL_SHEET_PROHIBITED_CHARACTERS As String = ":\\?\[\]/*：￥＼？［］／＊"

        Const EXCE_SHEET_NAME_MAX_LENGTH As Integer = 31

        Dim sheetName As String = Left(String.Format(LMI430C.SAVE_FILE_FORMAT.SHEET_NAME_FORMAT _
                                                   , row.REMARK_1 _
                                                   , row.REMARK_2 _
                                                   , row.REMARK_3), EXCE_SHEET_NAME_MAX_LENGTH)


        Return Me.RemoveProhibitedCharacters(sheetName _
                                           , EXCEL_SHEET_PROHIBITED_CHARACTERS)

    End Function

    ''' <summary>
    ''' 禁止文字削除
    ''' </summary>
    ''' <param name="sheetName"></param>
    ''' <param name="prohibitedCharacters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RemoveProhibitedCharacters(ByVal sheetName As String _
                                              , ByVal prohibitedCharacters As String) As String


        Dim pattern As String = String.Format("[{0}]", prohibitedCharacters)

        Return System.Text.RegularExpressions.Regex.Replace(sheetName, pattern, "")


    End Function

    '----
    ''' <summary>
    ''' 読取結果保存フォルダ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSaveFolderPathRead() As String

        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI430C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI430C.SAVE_FILE_KBN.KBN_CD2)

        Dim folderPath As String = String.Empty
        If (saveInfoRow IsNot Nothing) Then

            folderPath = Path.Combine(saveInfoRow.Item(LMI430C.SAVE_FILE_KBN.FOLDER_PATH_COL).ToString() _
                                    , DateTime.Now.ToString("yyyyMMdd"))

            Directory.CreateDirectory(folderPath)
        End If

        Return folderPath

    End Function

    ''' <summary>
    ''' 保存ファイル名生成
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSaveFileName2(ByVal row As LMI430DS.LMI430OUT_READ_DATARow) As String


        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI430C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI430C.SAVE_FILE_KBN.KBN_CD2)

        Dim fileNamePrefix As String = ""
        If (saveInfoRow IsNot Nothing) Then
            fileNamePrefix = saveInfoRow.Item(LMI430C.SAVE_FILE_KBN.FILE_PREFIX_COL).ToString()
        End If


        'Dim loadFileNameWithoutExt As String = Path.GetFileNameWithoutExtension(row.LOAD_FILE_NAME)
        Dim loadFileNameWithoutExt As String = ""

        Dim fileCreateTime As String = DateTime.Now.ToString("yyyyMMddhhmmssffff")

        ' 接頭文字 + 元のファイル名 + 出力日時 + 拡張子
        Return String.Format("{0}{1}_{2}{3}" _
                            , fileNamePrefix _
                            , loadFileNameWithoutExt _
                            , fileCreateTime _
                            , LMI430C.SAVE_FILE_FORMAT.SAVE_FILE_EXTENSION)


    End Function

    ''' <summary>
    ''' 保存シート名生成
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSheetName2(ByVal row As LMI430DS.LMI430OUT_READ_DATARow) As String

        Const EXCEL_SHEET_PROHIBITED_CHARACTERS As String = ":\\?\[\]/*：￥＼？［］／＊"

        'Const EXCE_SHEET_NAME_MAX_LENGTH As Integer = 31

        Dim sheetName As String = "読取結果"

        Return Me.RemoveProhibitedCharacters(sheetName _
                                           , EXCEL_SHEET_PROHIBITED_CHARACTERS)

    End Function
    '----

#End Region

#Region "マスタ参照"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMI430F _
                                    , ByVal objNm As String _
                                    , ByVal actionType As LMI430C.ActionType) As Boolean

        With frm

            Select Case objNm


                Case .txtCustCdL.Name, .txtCustCdM.Name, .FunctionKey.F1ButtonName
                    'ヘッダの荷主マスタ参照時
                    '荷主マスタ照会画面をPOP呼出&戻り値設定
                    Return Me.SetCustPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ照会画面Pop処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetCustPop(ByVal frm As LMI430F _
                              , ByVal actionType As LMI430C.ActionType) As Boolean

        '荷主マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        '戻り値の設定
        If prm.ReturnFlg = True Then
            'LMZ260Cデータセット取得

            Dim table As New LMZ260DS.LMZ260OUTDataTable()
            table.Merge(prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT))

            Dim dr As LMZ260DS.LMZ260OUTRow = table.FirstOrDefault

            '当画面項目へセット
            With frm
                If (dr IsNot Nothing) Then
                    .txtCustCdL.TextValue = dr.CUST_CD_L
                    .txtCustCdM.TextValue = dr.CUST_CD_M
                    .lblCustNM.TextValue = _G.GetDispCustName(dr.CUST_NM_L, dr.CUST_NM_M)
                End If
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI430F _
                                 , ByVal actionType As LMI430C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As LMZ260DS = New LMZ260DS()
        Dim dr As LMZ260DS.LMZ260INRow = ds.LMZ260IN.NewLMZ260INRow

        With dr
            .NRS_BR_CD = frm.cmbNrsBrCd.SelectedValue.ToString

            If actionType = LMI430C.ActionType.ENTER OrElse _
               actionType = LMI430C.ActionType.LOAD_FILE Then
                .CUST_CD_L = frm.txtCustCdL.TextValue
                .CUST_CD_M = frm.txtCustCdM.TextValue
            End If
            .DEFAULT_SEARCH_FLG = LMConst.FLG.ON
            .HYOJI_KBN = LMZControlC.HYOJI_S
        End With

        ds.LMZ260IN.Rows.Add(dr)

        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, LMI430C.FORM_ID.M_CUST)

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)
        Return prm

    End Function

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMI430F)

        With frm

            '荷主名称（大）
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                    .txtCustCdM.TextValue = LMI430C.DEFAUL_CUST_CD_M
                End If
                .lblCustNM.TextValue = GetCachedCust(.txtCustCdL.TextValue _
                                                   , .txtCustCdM.TextValue _
                                                   , LMI430C.DEFAUL_CUST_CD_S _
                                                   , LMI430C.DEFAUL_CUST_CD_SS)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        Dim custRow As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).AsEnumerable() _
                                 .Where(Function(r) r.Item(LMI430C.COL_NAME.CUST_CD_L).Equals(custCdL) AndAlso _
                                                    r.Item(LMI430C.COL_NAME.CUST_CD_M).Equals(custCdM) AndAlso _
                                                    r.Item(LMI430C.COL_NAME.CUST_CD_S).Equals(custCdS) AndAlso _
                                                    r.Item(LMI430C.COL_NAME.CUST_CD_SS).Equals(custCdSS) AndAlso _
                                                    r.Item(LMI430C.COL_NAME.SYS_DEL_FLG).Equals(custCdL)).FirstOrDefault


        If (custRow IsNot Nothing) Then

            Return _G.GetDispCustName(custRow.Field(Of String)(LMI430C.COL_NAME.CUST_NM_L) _
                                    , custRow.Field(Of String)(LMI430C.COL_NAME.CUST_NM_M))
        End If

        Return String.Empty

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataSelectedRow(ByVal frm As LMI430F) As DataSet

        Dim selectedRowIdx As Integer = 0

        Dim inData As New LMI430DS

        With frm.sprDetails.ActiveSheet

            For Each checked As String In Me._V.GetCheckList()

                selectedRowIdx = Convert.ToInt32(checked)

                Dim newRow As LMI430DS.LMI430INRow = inData.LMI430IN.NewLMI430INRow

                newRow.NRS_BR_CD = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI430G.sprDetails.NRS_BR_CD.ColNo))
                newRow.INKA_CYL_FILE_NO_L = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI430G.sprDetails.INKA_CYL_FILE_NO_L.ColNo))
                newRow.LAST_UPD_DATE = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI430G.sprDetails.LAST_UPD_DATE.ColNo))
                newRow.LAST_UPD_TIME = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI430G.sprDetails.LAST_UPD_TIME.ColNo))
                newRow.SPREAD_ROW_NO = (selectedRowIdx + 1).ToString()

                inData.LMI430IN.AddLMI430INRow(newRow)
            Next

        End With


        Return inData

    End Function


    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>読取結果Excel用</remarks>
    Private Function SetReadResultSelectedRow(ByVal frm As LMI430F) As DataSet

        Dim selectedRowIdx As Integer = 0

        Dim inData As New LMI430DS

        Dim inRow As LMI430DS.LMI430INRow = inData.LMI430IN.NewLMI430INRow

        inRow.NRS_BR_CD = frm.cmbNrsBrCd.SelectedValue.ToString()
        inRow.CUST_CD_L = frm.txtCustCdL.TextValue
        inRow.CUST_CD_M = frm.txtCustCdM.TextValue
        inRow.INKA_DATE_FROM = frm.imdReadDateFrom.TextValue
        inRow.INKA_DATE_TO = frm.imdReadDateTo.TextValue

        inData.LMI430IN.AddLMI430INRow(inRow)

        Return inData

    End Function
#End Region

#Region "ユーティリティ"

#Region "検索件数上限設定"


    ''' <summary>
    ''' 検索件数上限設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSearchLimit()

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        ' 閾値設定
        MyBase.SetLimitCount(Me.GetLimitCount())

        ' 最大件数設定
        MyBase.SetMaxResultCount(Me.GetMaxResultCount())

    End Sub


    ''' <summary>
    ''' 検索件数の取得確認用の閾値を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLimitCount() As Integer

        Const KBN_GROUP_CD_LIMIT_COUNT As String = "S054"
        Const KBN_CD_SEARCH_LIMIT_COUNT As String = "02"

        Dim limitRow As DataRow = Me.GetKbnRow(KBN_GROUP_CD_LIMIT_COUNT _
                                             , KBN_CD_SEARCH_LIMIT_COUNT)

        '閾値の取得
        Dim limitCount As Decimal = 0

        If (limitRow IsNot Nothing AndAlso _
            Decimal.TryParse(limitRow.Item(LMI430C.COL_NAME.VALUE1).ToString _
                           , limitCount) = False) Then

            limitCount = 1000
        End If


        Return Convert.ToInt32(limitCount)


    End Function


    ''' <summary>
    ''' 検索件数の最大値を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMaxResultCount() As Integer

        Const KBN_GROUP_CD_MAX_RESULT_COUNT As String = "M011"

        Dim maxResultRow As DataRow = Me.GetKbnRowByKbnNm1(KBN_GROUP_CD_MAX_RESULT_COUNT _
                                                         , MyBase.GetPGID())

        '最大値の取得
        Dim maxCount As Decimal = 0

        If (maxResultRow IsNot Nothing AndAlso _
            Decimal.TryParse(maxResultRow.Item(LMI430C.COL_NAME.VALUE1).ToString _
                           , maxCount) = False) Then

            maxCount = 1000
        End If


        Return Convert.ToInt32(maxCount)


    End Function


#End Region


#Region "区分"


    ''' <summary>
    ''' Z_KBNから任意の一行を取得する。
    ''' </summary>
    ''' <param name="kbnGroupCd"></param>
    ''' <param name="kbnCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKbnRow(ByVal kbnGroupCd As String, ByVal kbnCd As String) As DataRow

        If (String.IsNullOrEmpty(kbnGroupCd) OrElse String.IsNullOrEmpty(kbnCd)) Then
            Return Nothing
        End If

        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable _
              .Where(Function(r) kbnGroupCd.Equals(r.Item(LMI430C.COL_NAME.KBN_GROUP_CD)) AndAlso _
                                 kbnCd.Equals(r.Item(LMI430C.COL_NAME.KBN_CD))).FirstOrDefault


    End Function

    ''' <summary>
    ''' Z_KBNから任意の一行を取得する。
    ''' </summary>
    ''' <param name="kbnGroupCd"></param>
    ''' <param name="kbnNm1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKbnRowByKbnNm1(ByVal kbnGroupCd As String, ByVal kbnNm1 As String) As DataRow

        If (String.IsNullOrEmpty(kbnGroupCd) OrElse String.IsNullOrEmpty(kbnNm1)) Then
            Return Nothing
        End If

        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable _
              .Where(Function(r) kbnGroupCd.Equals(r.Item(LMI430C.COL_NAME.KBN_GROUP_CD)) AndAlso _
                                 kbnNm1.Equals(r.Item(LMI430C.COL_NAME.KBN_NM1))).FirstOrDefault


    End Function

#End Region

#Region "Enter処理"

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">処理を行う場合 True</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI430F, ByVal eventFlg As Boolean)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI430C.ActionType.ENTER)

        'Popを表示するかを判定
        rtnResult = rtnResult AndAlso Me.ChkOpenEnterAction(frm, objNm)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理

            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMIConH.StartAction(frm)

        '項目チェック：Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Me.ShowPopupControl(frm, objNm, LMI430C.ActionType.ENTER)

        '終了処理
        Call Me._LMIConH.EndAction(frm, Me.GetGMessage())


        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return LMI430C.MESSAGE_ID.NORMAL
    End Function

    ''' <summary>
    ''' Enter処理の特殊フォーカス移動
    ''' </summary>
    ''' <param name="eventFlg">Enterの場合、True</param>
    ''' <remarks></remarks>
    Private Sub NextFocusedControl(ByVal frm As LMI430F, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' Enter処理時にPopを表示するかを判定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:参照する False:参照しない</returns>
    ''' <remarks></remarks>
    Private Function ChkOpenEnterAction(ByVal frm As LMI430F, ByVal objNm As String) As Boolean

        With frm

            Select Case objNm


                Case .txtCustCdL.Name, .txtCustCdM.Name

                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        Return False
                    End If

                    Return True


            End Select

            Return Not String.IsNullOrEmpty(DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox).TextValue)

        End With
    End Function

#End Region 'Enter処理

#Region "Excelファイル操作"

    ''' <summary>
    ''' Excelファイル読込
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadExcelFile(ByVal filePath As String _
                                 , ByVal sheetNo As Integer _
                                 , ByVal startRowNo As Integer _
                                 , ByVal startColNo As Integer _
                                 , ByVal columCount As Integer) As Object(,)

        If (String.IsNullOrEmpty(filePath) OrElse _
            IO.File.Exists(filePath) = False) Then
            Return Nothing
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlWkSheet As Excel.Worksheet = Nothing
        Dim xlCells As Excel.Range = Nothing
        Dim xlLastCell As Excel.Range = Nothing
        Dim startCell As Object = Nothing
        Dim endCell As Object = Nothing
        Dim xlReadCells As Excel.Range = Nothing

        Dim cellData(,) As Object = Nothing

        Try
            xlApp = New Excel.Application With
                    {.Visible = False,
                     .DisplayAlerts = False}

            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Open(filePath)
            xlSheets = xlBook.Sheets

            xlWkSheet = DirectCast(xlSheets(sheetNo), Excel.Worksheet)
            xlCells = xlWkSheet.Cells

            xlLastCell = xlCells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell)

            '  ToDo: 必要があれば荷主毎に切替
            Dim startPoint As New Point(startRowNo _
                                      , startColNo)
            Dim endPoint As New Point(xlLastCell.Row _
                                    , columCount)


            ' データを取り出す領域の開始位置と終了位置を設定
            startCell = xlCells(startPoint.X, startPoint.Y)
            endCell = xlCells(endPoint.X, endPoint.Y)

            ' 必要な領域を取得
            xlReadCells = xlWkSheet.Range(startCell, endCell)
            If (xlReadCells IsNot Nothing) Then

                Return TryCast(xlReadCells.Value, Object(,))
            End If

        Catch ex As Exception

            Logger.WriteErrorLog(Me.GetType.Name _
                               , MethodBase.GetCurrentMethod.Name _
                               , ex.Message _
                               , ex)
        Finally

            ' 開放処理(ここに不足があるとExcelが残る)
            Me.ReleaseExcelObject(Of Excel.Range)(xlReadCells)
            Me.ReleaseExcelObject(startCell)
            Me.ReleaseExcelObject(endCell)
            Me.ReleaseExcelObject(Of Excel.Range)(xlLastCell)
            Me.ReleaseExcelObject(Of Excel.Range)(xlCells)
            Me.ReleaseExcelObject(Of Excel.Worksheet)(xlWkSheet)
            If (xlSheets IsNot Nothing) Then
                For Each sheet As Excel.Worksheet In xlSheets
                    Me.ReleaseExcelObject(Of Excel.Worksheet)(sheet)
                Next
            End If
            Me.ReleaseExcelObject(Of Excel.Sheets)(xlSheets)
            Me.ReleaseExcelObject(Of Excel.Workbook)(xlBook)
            Me.ReleaseExcelObject(Of Excel.Workbooks)(xlBooks)
            Me.ReleaseExcelObject(Of Excel.Application)(xlApp)
            GC.Collect()

        End Try

        Return Nothing

    End Function

    ''' <summary>
    ''' Excelファイル保存
    ''' </summary>
    ''' <param name="savefileName"></param>
    ''' <param name="saveFolderPath"></param>
    ''' <param name="sheetName"></param>
    ''' <param name="saveData"></param>
    ''' <returns>成否</returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SaveExcelFile(ByVal savefileName As String _
                                 , ByVal saveFolderPath As String _
                                 , ByVal sheetName As String _
                                 , ByVal saveData As String(,)) As Boolean


        Dim isSuccess As Boolean = False

        If (saveData Is Nothing OrElse _
            saveData.Length = 0) Then

            Return isSuccess
        End If

        Dim saveFilePath As String = Path.Combine(saveFolderPath, savefileName)

        'ファイルの存在確認
        If System.IO.File.Exists(saveFilePath) = True Then
            '存在した場合は、削除して新規作成
            System.IO.File.Delete(saveFilePath)
        End If



        Const EXCEL_MAX_COL_COUNT As Integer = 16384 ' Excel2013以降
        Const EXCEL_MAX_ROW_COUNT As Integer = 1048576 ' Excel2013以降

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlWkSheet As Excel.Worksheet = Nothing
        Dim xlCells As Excel.Range = Nothing
        Dim startRange As Object = Nothing
        Dim endRange As Object = Nothing
        Dim xlWriteCells As Excel.Range = Nothing
        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing

        Try
            xlApp = New Excel.Application With
                    {.Visible = False,
                     .DisplayAlerts = False}

            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()

            ' シート番号
            Dim sheetNo As Integer = 1

            xlSheets = xlBook.Worksheets
            xlWkSheet = DirectCast(xlSheets(sheetNo), Excel.Worksheet)

            If (String.IsNullOrEmpty(sheetName) = False) Then
                xlWkSheet.Name = sheetName
            End If

            xlCells = xlWkSheet.Cells

            Dim startingPoint As New Point(1, 1)
            Dim endPoint As New Point(saveData.GetLength(LMI430C.ARRAY_INDEX.COL_DIMENSION) _
                                    , saveData.GetLength(LMI430C.ARRAY_INDEX.ROW_DIMENSION))

            If (endPoint.X > EXCEL_MAX_COL_COUNT OrElse _
                endPoint.Y > EXCEL_MAX_ROW_COUNT) Then

                ' ToDo: 行数がExcelの最大値を超えるようであれば、シート分割を実装

                Return isSuccess
            End If

            startRange = xlCells(startingPoint.Y, startingPoint.X)
            endRange = xlCells(endPoint.Y, endPoint.X)

            startCell = DirectCast(startRange, Excel.Range)
            endCell = DirectCast(endRange, Excel.Range)

            xlWriteCells = xlWkSheet.Range(startCell, endCell)
            xlWriteCells.Value = saveData

            xlBook.SaveAs(saveFilePath, LMI430C.SAVE_FILE_FORMAT.EXCEL_FORMAT)
            xlBook.RefreshAll()

            isSuccess = True

        Catch ex As Exception

            isSuccess = False

            Logger.WriteErrorLog(MyBase.GetType.Name _
                               , MethodBase.GetCurrentMethod.Name _
                               , ex.Message, ex)

        Finally

            ' 開放処理(ここに不足があるとExcelが残る)
            Me.ReleaseExcelObject(Of Excel.Range)(xlWriteCells)
            Me.ReleaseExcelObject(startRange)
            Me.ReleaseExcelObject(endRange)
            Me.ReleaseExcelObject(Of Excel.Range)(xlCells)
            Me.ReleaseExcelObject(Of Excel.Worksheet)(xlWkSheet)

            If (xlSheets IsNot Nothing) Then
                For Each sheet As Excel.Worksheet In xlSheets
                    Me.ReleaseExcelObject(Of Excel.Worksheet)(sheet)
                Next
            End If

            Me.ReleaseExcelObject(Of Excel.Sheets)(xlSheets)
            Me.ReleaseExcelObject(Of Excel.Workbook)(xlBook)
            Me.ReleaseExcelObject(Of Excel.Workbooks)(xlBooks)
            Me.ReleaseExcelObject(Of Excel.Application)(xlApp)
            GC.Collect()



        End Try

        Return isSuccess

    End Function



    ''' <summary>
    ''' Excel関連オブジェクト開放
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReleaseExcelObject(Of T)(ByRef obj As T) As Boolean

        Try

            If (obj IsNot Nothing AndAlso Marshal.IsComObject(obj)) Then

                ' Close, QUIT
                Select Case True
                    Case TypeOf obj Is Excel.Workbook
                        TryCast(obj, Excel.Workbook).RefreshAll()
                        TryCast(obj, Excel.Workbook).Close(False)
                    Case TypeOf obj Is Excel.Workbooks
                        TryCast(obj, Excel.Workbooks).Close()
                    Case TypeOf obj Is Excel.Application
                        TryCast(obj, Excel.Application).Quit()
                End Select

                If (Marshal.FinalReleaseComObject(obj) <> 0) Then

                    Return False
                End If

                obj = Nothing

            End If

            Return True

        Catch ex As Exception

            Return False
        End Try

    End Function

#End Region

#End Region
#End Region 'Method

End Class
