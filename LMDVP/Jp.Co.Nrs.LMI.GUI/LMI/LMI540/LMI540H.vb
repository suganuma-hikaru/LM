' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMHI     : 特定荷主機能
'  プログラムID     :  LMI540H : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI540ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI540H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI540V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI540G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 初期表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean

    ''' <summary>
    ''' 処理モード
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mode As Integer = LMI540C.Mode.INT

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        Me._ShokiFlg = True

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        ' 画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        ' フォームの作成
        Dim frm As LMI540F = New LMI540F(Me)

        ' キーイベントをフォームで受け取る
        frm.KeyPreview = True

        ' Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID())

        ' Validateクラスの設定
        Me._V = New LMI540V(Me, frm)

        ' Gamenクラスの設定
        Me._G = New LMI540G(Me, frm)

        ' G共通クラスの設定
        Me._LMIconG = New LMIControlG(frm)

        ' 処理モードの設定
        Me._Mode = LMI540C.Mode.INT

        ' フォームの初期化
        Call MyBase.InitControl(frm)

        ' 営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        ' ファンクションキーの設定
        Call Me._G.SetFunctionKey(Me._Mode)

        ' タブインデックスの設定
        Call Me._G.SetTabIndex()

        ' コントロール個別設定
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysdate(0))

        ' スプレッドの初期設定
        Call Me._G.InitSpread()

        ' メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        ' 画面の入力項目の制御
        Call Me._G.SetControlsStatus(Me._Mode)

        ' フォーカスの設定(ヘッダー部の先頭)
        Call Me._G.SetFocusHeader()

        ' フォームの表示
        frm.Show()

        ' カーソルを元に戻す
        Cursor.Current = Cursors.Default

        ' Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        ' Gamen共通クラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        ' 初期表示フラグの設定
        Me._ShokiFlg = False

    End Sub

#End Region

#Region "イベントコントロール"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI540C.EventShubetsu, ByVal frm As LMI540F)

        ' 画面初期化処理中は抜ける
        If Me._ShokiFlg Then
            Exit Sub
        End If

        ' 権限チェック
        If Not Me._V.IsAuthorityChk(eventShubetsu) Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        ' 処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        ' イベント種別による分岐
        Select Case eventShubetsu
            Case LMI540C.EventShubetsu.TORIKOMI
                ' 取込

                ' 入力チェック（単項目チェック）
                If Not Me._V.IsTorikomiSingleCheck() Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                ' 続行確認
                Dim rtn As MsgBoxResult
                rtn = Me.ShowMessage(frm, "C001", New String() {"取込処理"})

                If rtn = MsgBoxResult.Ok Then
                ElseIf rtn = MsgBoxResult.Cancel Then
                    Call MyBase.ShowMessage(frm, "G007")
                    Call EndAction2(frm)
                    Exit Sub
                End If

                Call Me.Torikomi(frm)

            Case LMI540C.EventShubetsu.SEARCH
                ' 検索

                ' 入力チェック（単項目チェック）
                If Not Me._V.IsSearchSingleCheck(Me._G) Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                ' 入力チェック（関連チェック）
                If Not Me._V.IsSearchKanrenCheck() Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                ' 検索処理
                Call Me.Search(frm)

            Case LMI540C.EventShubetsu.PRINT
                ' 印刷

                ' チェックされた行番号のリストを取得
                Dim chkList As ArrayList = New ArrayList()
                chkList = Me._V.getCheckList()

                ' 入力チェック（単項目チェック）
                If Not Me._V.IsPrintSingleCheck(chkList) Then
                    Call EndAction2(frm)
                    Exit Sub
                End If

                ' 印刷処理
                Call Me.Print(frm, chkList, LMI540C.cmbPrintUseKbnCd.IRAI_SHO)

        End Select

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ActionControl(LMI540C.EventShubetsu.TORIKOMI, frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        ' 検索処理
        Call ActionControl(LMI540C.EventShubetsu.SEARCH, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        ' 終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI540F)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call ActionControl(LMI540C.EventShubetsu.PRINT, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' Enter 押下
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Friend Sub FormKeyDownEnter(ByRef frm As LMI540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim controlNm As String = String.Empty

        controlNm = frm.FocusedControlName()

        '　荷主コード入力コントロールでEnterキーを押下時
        If (frm.FocusedControlName() = "txtCustCD_L" OrElse frm.FocusedControlName() = "txtCustCD_M") Then
            'If String.IsNullOrEmpty(frm.txtCustCD_L.TextValue) OrElse
            '        String.IsNullOrEmpty(frm.txtCustCD_M.TextValue) Then

            Dim custCdL As String = ""
            Dim custCdM As String = ""
            If String.IsNullOrEmpty(frm.txtCustCD_L.TextValue) = False Then
                custCdL = frm.txtCustCD_L.TextValue
            End If
            If String.IsNullOrEmpty(frm.txtCustCD_M.TextValue) = False Then
                custCdM = frm.txtCustCD_M.TextValue
            End If

            ' 荷主コード/荷主名 (大/中) 初期値設定
            Me._G.SetInitControlCust(frm, custCdL, custCdM)

            'End If

            ' Tabキーが押された時と同じ動作をする。
            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End If

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI540F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' SPREADのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMI540F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

#End Region

#Region "共通処理"

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="msgClear"></param>
    ''' <remarks>LMIControlHのEndAction後にメッセージクリアロジックを付加</remarks>
    Private Sub EndAction2(ByVal frm As Form, Optional ByVal id As String = "G007", Optional ByVal msgClear As Boolean = False)

        ' 処理終了アクション
        Call Me._LMIconH.EndAction(frm, id)

        ' メッセージをクリア
        If msgClear Then
            MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))
        End If

    End Sub

#End Region ' "共通処理"

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    Private Sub Torikomi(ByVal frm As LMI540F)

        Dim rtDs As DataSet = New LMI540DS()
        Dim rcvDir As String = String.Empty

        '========= ファイル選択処理 =======
        ' WindowsDialog インスタンス生成
        Dim ofd As New OpenFileDialog()

        Try

            ' WindowsDialog のタイトル設定
            ofd.Title = "取込むファイルを選択してください"

            Dim filter As String = "Excelファイル(*.xls?)|*.xls?"
            ofd.Filter = filter
            ofd.FilterIndex = 1

            ofd.Multiselect = False

            ' ファイル名取得
            Dim objFiles As ArrayList = New ArrayList
            Dim arrCnt As Integer = 0
            If ofd.ShowDialog() = DialogResult.OK Then
                For Each newArr As String In ofd.SafeFileNames
                    objFiles.Add(newArr)
                Next
            Else
                Exit Sub
            End If

            arrCnt = objFiles.Count
            '========= ファイル選択処理 =======

            'ファイルパス取得(ファイルまでのフルパスからファイル分のパスを消去でディレクトリを確保)
            rcvDir = ofd.FileNames(0).ToString()
            rcvDir = rcvDir.Replace(objFiles(0).ToString(), "")

            ' 取込ヘッダ設定
            SetDataTorikomiHed(frm, objFiles, rtDs)

            ' 関連チェック実行
            If Me._V.IsTorikomiKanrenCheck(rtDs, objFiles, ofd.SafeFileNames, rcvDir) = False Then
                Exit Sub
            End If

            Dim stTime As Date = Now

            ' コネクションリスト
            Dim arrCloser As ArrayList = New ArrayList

            rtDs = Me.SetDataTorikomiShosaiExcel(frm, rtDs, arrCloser, rcvDir, "Sheet1")

            ' ==== WSAクラス呼出 ====
            Dim rtnDs As DataSet = MyBase.CallWSA("LMI540BLF", "Torikomi", rtDs)

            Dim rtnFileDr As DataRow
            Dim rtnErrFlg As String
            Dim rtnFile_Name_Rcv As String = String.Empty
            Dim rtnFile_Name_Bak As String = String.Empty
            Dim noExtends As String = String.Empty

            ' ファイル保存ダイアログ
            Dim sfd As New FolderBrowserDialog

            ' バックアップディレクトリ
            Dim backDir As String = String.Empty


            ' ダイアログタイトル
            sfd.Description = "バックアップファイルを保存するフォルダを選択してください"

            ' ファイル保存ダイアログ[初期ディレクトリ]
            sfd.RootFolder = Environment.SpecialFolder.Desktop

            ' 選択フォルダ設定
            sfd.SelectedPath = rcvDir

            ' ダイアログ展開
            Dim dlogResult As DialogResult = sfd.ShowDialog()

            If dlogResult = DialogResult.OK Then
                ' OKなら

                ' 選択ディレクトリ
                backDir = String.Concat(sfd.SelectedPath, "\")

            ElseIf dlogResult = DialogResult.Cancel Then
                ' CANCELなら

                ' 取込時ディレクトリ
                backDir = rcvDir
            End If

            ' ダイアログのごみの破棄
            sfd.Dispose()

            ' ファイル保存

            For i As Integer = 0 To rtnDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows.Count - 1
                rtnFileDr = rtnDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows(i)

                rtnErrFlg = rtnFileDr.Item("ERR_FLG").ToString()
                rtnFile_Name_Rcv = rtnFileDr.Item("FILE_NAME_RCV").ToString()

                'エラーフラグ判定
                If rtnErrFlg.Equals("0") Then

                    ' [正常時処理]
                    ' 受信ファイルのロックを解除 + オリジナルの削除＆コピーの作成

                    ' ファイル名の変更
                    noExtends = System.IO.Path.GetFileNameWithoutExtension(String.Concat(rcvDir, rtnFile_Name_Rcv))
                    rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), rtnFile_Name_Rcv.Replace(noExtends, ""))

                    ' ファイルのコピーを作成
                    System.IO.File.Copy(String.Concat(rcvDir, rtnFile_Name_Rcv), String.Concat(backDir, rtnFile_Name_Bak), True)

                    ' Excel クローズ処理
                    DoCloseAction(arrCloser, i)

                    ' オリジナル削除
                    System.IO.File.Delete(String.Concat(rcvDir, rtnFile_Name_Rcv))

                    Me.aProcessKill("EXCEL", stTime)

                Else
                    ' [エラー時処理]
                    ' 受信ファイルのロックを解除

                    ' Excel クローズ処理
                    DoCloseAction(arrCloser, i)
                End If
            Next

            ' メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then
                ' エラーエクセルの出力
                Call Me.OutputExcel(frm)
            Else
                ' 取込処理成功時処理
                Call Me.SuccessTorikomi(frm, rtnDs)
            End If

        Finally
            ' 不要ダイアログのゴミ削除
            ofd.Dispose()

            Call EndAction2(frm)
        End Try

    End Sub

    ''' <summary>
    ''' 取込処理: 取込ヘッダ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="flNmArr"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    Private Function SetDataTorikomiHed(ByVal frm As LMI540F, ByVal flNmArr As ArrayList, ByVal rtDs As DataSet) As DataSet

        Dim dr As DataRow
        Dim sysDatetime As String = String.Empty
        Dim extention As String = String.Empty

        Dim ope_File_Name As String = String.Empty
        Dim backup_File_Name As String = String.Empty

        ' システム時間を取得
        sysDatetime = String.Concat(MyBase.GetSystemDateTime()(0), MyBase.GetSystemDateTime()(1)).Remove(14)

        ' 受信格納フォルダのファイル数だけデータセットを作成
        For Each stFilePath As String In flNmArr
            ' NewRow生成
            dr = rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).NewRow()

            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

            ' ファイル名取得
            dr("FILE_NAME_RCV") = stFilePath
            dr("ERR_FLG") = "9"

            ' 格納
            rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows.Add(dr)
        Next stFilePath

        Return rtDs

    End Function

    ''' <summary>
    ''' 取込処理: Excel 取込
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="arrCloser"></param>
    ''' <param name="rcvDir"></param>
    ''' <param name="sheet"></param>
    ''' <returns></returns>
    Private Function SetDataTorikomiShosaiExcel(ByVal frm As LMI540F, ByVal rtDs As DataSet,
                                                ByRef arrCloser As ArrayList,
                                                ByVal rcvDir As String,
                                                Optional ByVal sheet As Object = 1) As DataSet

        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList

        Dim dtHed As DataTable = rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED)
        Dim dr As DataRow
        Dim gyoCount As Integer = 0

        Dim rowNoMin As Integer = 2     ' 行の開始数
        Dim colNoMax As Integer = 20    ' 列の最大数
        Dim rowNoKey As Integer = 1     ' 値設定開始列

        Dim fileNm As String = String.Empty

        '-----------------------------------------------------------------------------------------------
        ' EXCELファイル用
        '-----------------------------------------------------------------------------------------------
        Dim xlApp As Excel.Application = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()

        xlBooks = xlApp.Workbooks

        ' EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            ' EXCEL OPEN
            fileNm = dtHed.Rows(i).Item("FILE_NAME_RCV").ToString()
            Try
                xlBook = xlBooks.Open(String.Concat(rcvDir, fileNm))
            Catch ex As Exception
                '例外がスローされたら処理強制終了
                rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows(i).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
                Return rtDs
            End Try

            Dim sheetExists As Boolean = False
            For Each st As Excel.Worksheet In xlBook.Worksheets
                If st.Name = sheet.ToString() Then
                    sheetExists = True
                    Exit For
                End If
            Next
            If Not sheetExists Then
                ' 指定のシートが存在しない場合

                ' 明細は明らかに空ファイルとなるので、HEDにエラーフラグを立てる
                rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows(i).Item("ERR_FLG") = "1"

                ' コネクションをファンクション外で閉じるための ArrayList 設定
                colection.Clear()
                colection.Add(xlCell)    '0
                colection.Add(xlSheet)   '1
                colection.Add(xlBook)    '2
                colection.Add(xlBooks)   '3
                colection.Add(xlApp)     '4

                arrCloser.Add(colection)

                Continue For
            End If

            ' シート
            xlSheet = DirectCast(xlBook.Worksheets(sheet), Excel.Worksheet)
            xlSheet.Activate()

            xlApp.Visible = False

            ' 最大行を取得(rowNoKey列の最終入力行を取得)
            Dim rowNoMax As Integer = 0

            xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()
            rowNoMax = xlApp.ActiveCell.Row

            ' 2次元配列に取得する
            Dim arrData(,) As Object
            arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

            ' 2次元配列→DS
            For j As Integer = rowNoMin To rowNoMax

                If arrData(j, rowNoKey) Is Nothing Then

                    Continue For

                Else
                    If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                        Continue For
                    End If
                End If

                gyoCount += 1
                dr = rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"

                ' DSに格納
                For k As Integer = 1 To colNoMax

                    If arrData(j, k) Is Nothing Then
                        dr(String.Concat("COLUMN_", k.ToString)) = String.Empty
                    Else
                        dr(String.Concat("COLUMN_", k.ToString)) = arrData(j, k).ToString().Trim()
                    End If
                Next

                ' DSにAdd
                rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_DTL).Rows.Add(dr)
            Next

            ' 空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables(LMI540C.TABLE_NM.TORIKOMI_HED).Rows(i).Item("ERR_FLG") = "1"
            End If

            ' 行カウントをリセット
            gyoCount = 0

            ' コネクションをファンクション外で閉じるための ArrayList 設定
            colection.Clear()
            colection.Add(xlCell)    '0
            colection.Add(xlSheet)   '1
            colection.Add(xlBook)    '2
            colection.Add(xlBooks)   '3
            colection.Add(xlApp)     '4

            arrCloser.Add(colection)

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 取込処理: Excel を閉じる処理の実行
    ''' </summary>
    ''' <param name="arrCloser"></param>
    ''' <remarks></remarks>
    Private Sub DoCloseAction(ByVal arrCloser As ArrayList, ByVal nawRow As Integer)

        Dim connect As ArrayList = New ArrayList


        If arrCloser(nawRow) Is Nothing Then
            Exit Sub
        End If

        'アレイの一行コピー
        connect.AddRange(CType(arrCloser(nawRow), Collections.ICollection))

        ' コネクションの確認
        If arrCloser(nawRow) Is Nothing Then
            ' 間違いなくエラーです。
        End If

        ' 分解
        '=============
        '(xlCell)  '0 
        '(xlSheet) '1 
        '(xlBook)  '2 
        '(xlBooks) '3 
        '(xlApp)   '4 
        '=============
        Dim xlApp As Excel.Application = DirectCast(connect(4), Excel.Application)
        Dim xlBook As Excel.Workbook = DirectCast(connect(2), Excel.Workbook)
        Dim xlBooks As Excel.Workbooks = DirectCast(connect(3), Excel.Workbooks)
        Dim xlSheet As Excel.Worksheet = DirectCast(connect(1), Excel.Worksheet)
        Dim xlCell As Excel.Range = DirectCast(connect(0), Excel.Range)

        If xlCell IsNot Nothing Then
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
            xlCell = Nothing
        End If

        If xlSheet IsNot Nothing Then
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
            xlSheet = Nothing
        End If

        xlBook.Close(False) ' Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing

        xlApp.DisplayAlerts = False
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

    End Sub

    ''' <summary>
    ''' 取込処理: プロセス終了処理
    ''' </summary>
    ''' <param name="pApplicationName"></param>
    ''' <param name="stTime"></param>
    ''' <returns></returns>
    Function aProcessKill(ByVal pApplicationName As String, ByVal stTime As Date) As Integer

        ' 戻り値は処理を実行開始した時間以降に起動されたアプリケーションを強制終了させた数
        Dim sdProcesses As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName(pApplicationName)
        aProcessKill = 0

        ' 取得できたプロセスからプロセス ID を取得する
        For Each sdProcess As System.Diagnostics.Process In sdProcesses

            If sdProcess.StartTime > stTime AndAlso sdProcess.HasExited = False Then
                sdProcess.Kill()
                aProcessKill += 1
                Do While sdProcess.HasExited = False
                    ' プロセスが切れていない場合は5秒待機
                    System.Threading.Thread.Sleep(5000)
                Loop
            End If

        Next

        sdProcesses = Nothing

    End Function

    ''' <summary>
    ''' 取込処理: エラーエクセルの出力
    ''' </summary>
    ''' <param name="frm"></param>
    Private Sub OutputExcel(ByVal frm As LMI540F)

        MyBase.ShowMessage(frm, "E235")
        ' EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

    ''' <summary>
    ''' 取込処理: 取込処理成功時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    Private Sub SuccessTorikomi(ByVal frm As LMI540F, ByVal ds As DataSet)

        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", String.Empty})

    End Sub

#End Region ' "取込処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="previewFlg">True:印刷プレビューを表示中</param>
    ''' <remarks></remarks>
    Private Sub Search(ByVal frm As LMI540F, Optional ByVal previewFlg As Boolean = False)

        ' 強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        ' スプレッドシート初期化
        frm.sprDetail.CrearSpread()

        ' 検索条件の DataSet への設定
        Dim rtDs As DataSet = New LMI540DS()
        If Not Me.SearchSetDataIn(frm, rtDs) Then
            MyBase.ShowMessage(frm, "E361")
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        ' WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Search")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI540BLF",
                "Search",
                rtDs,
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))),
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))
        MyBase.Logger.EndLog(MyBase.GetType.Name, "Search")

        ' 検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        ' 処理モードの設定
        Me._Mode = LMI540C.Mode.REF

        ' ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        ' フォーカスの設定
        If Not previewFlg Then
            '印刷プレビュー表示中以外
            Call Me._G.SetFocusDetail()
        End If

        ' 処理終了アクション
        Call EndAction2(frm)

        ' 終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        For i As Integer = 1 To frm.sprDetail.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(LMI540C.Mode.REF, i, True)
        Next

    End Sub

    ''' <summary>
    ''' 検索処理：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SearchSetDataIn(ByVal frm As LMI540F, ByRef rtDs As DataSet) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMI540C.TABLE_NM.IN_SEARCH).NewRow()

        'ヘッダー部
        With frm
            ' 営業所コード
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            ' 倉庫コード
            dr("WH_CD") = .cmbWare.SelectedValue
            ' 荷主コード(大)
            dr("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            ' 荷主コード(中)
            dr("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            ' 出荷日FROM
            dr("OUTKA_DATE_FROM") = .imdOutkaDateFrom.TextValue
            ' 出荷日TO
            dr("OUTKA_DATE_TO") = .imdOutkaDateTo.TextValue
        End With

        'スプレッドシート
        With frm.sprDetail.ActiveSheet
            ' KEY NO.
            dr("KEY_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.KEY_NO.ColNo)).Trim()
            ' オフラインNo.
            dr("OFFLINE_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.OFFLINE_NO.ColNo)).Trim()
            ' 出荷指示書
            dr("SHIZI_KB") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.SHIZI_KB_NM.ColNo)).Trim()
            ' 納品書
            dr("NOHIN_KB") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.NOHIN_KB_NM.ColNo)).Trim()
            ' 依頼日
            dr("IRAI_DATE") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.IRAI_DATE.ColNo)).Trim()
            ' 依頼者
            dr("IRAI_SYA") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.IRAI_SYA.ColNo)).Trim()
            ' 出荷/回収元
            dr("MOTO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.MOTO.ColNo)).Trim()
            ' 種別
            dr("SHUBETSU") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.SHUBETSU.ColNo)).Trim()
            ' 納品日
            dr("ARR_DATE") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ARR_DATE.ColNo)).Trim()
            ' 郵便番号
            dr("ZIP") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ZIP.ColNo)).Trim()
            ' 住所
            dr("DEST_AD") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.DEST_AD.ColNo)).Trim()
            ' 会社名
            dr("COMP_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.COMP_NM.ColNo)).Trim()
            ' 部署名
            dr("BUSYO_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.BUSYO_NM.ColNo)).Trim()
            ' 担当者名
            dr("TANTO_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.TANTO_NM.ColNo)).Trim()
            ' 電話番号
            dr("TEL") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.TEL.ColNo)).Trim()
            ' 品名
            dr("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_NM.ColNo)).Trim()
            ' 製造ロット
            dr("LOT_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.LOT_NO.ColNo)).Trim()
            ' 温度条件
            dr("ONDO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.ONDO.ColNo)).Trim()
            ' 毒劇物
            dr("DOKUGEKI") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.DOKUGEKI.ColNo)).Trim()
            ' 配送便
            dr("HAISO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.HAISO.ColNo)).Trim()
            ' SAP受注登録番号
            dr("SAP_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.SAP_NO.ColNo)).Trim()
            ' 備考欄
            dr("REMARK") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprDetailDef.REMARK.ColNo)).Trim()

        End With

        rtDs.Tables(LMI540C.TABLE_NM.IN_SEARCH).Rows.Add(dr)

        Return True

    End Function

#End Region ' "検索処理"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <param name="printShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub Print(ByVal frm As LMI540F, ByVal chkList As ArrayList, ByVal printShubetsu As String)

        Dim rtDs As DataSet = New LMI540DS()

        With frm.sprDetail
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                ' 印刷対象として検索条件を追加
                Call Me.PrintSetPrtDataIn(frm, rtDs, spIdx, printShubetsu)
            Next
        End With

        ' DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        ' 印刷処理
        With Nothing
            ' 印刷対象がある
            If rtDs.Tables(LMI540C.TABLE_NM.IN_PRINT).Rows.Count > 0 Then
                ' WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintAndUpdate")
                rtDs = MyBase.CallWSA("LMI540BLF", "PrintAndUpdate", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintAndUpdate")
            End If

            ' 印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                ' 印刷対象がない
                ' 通常の印刷実行時のみ
                Call MyBase.SetMessage("G021")
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        ' 終了処理
        If MyBase.IsMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中)
            Call Search(frm, True)
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="printShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrtDataIn(ByVal frm As LMI540F, ByRef rtDs As DataSet, ByVal spIdx As Integer, ByVal printShubetsu As String)

        Dim dr As DataRow = rtDs.Tables(LMI540C.TABLE_NM.IN_PRINT).NewRow()

        With frm.sprDetail.ActiveSheet
            ' 印刷する帳票
            dr("PRINT_SB") = printShubetsu
            ' KEY NO.
            dr("KEY_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.KEY_NO.ColNo)).Trim()
            ' 営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.NRS_BR_CD.ColNo)).Trim()
            ' 荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.CUST_CD_L.ColNo)).Trim()
            ' 荷主コード(中)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.CUST_CD_M.ColNo)).Trim()
            ' 依頼書出力
            dr("SHIZI_KB") = "01"
            '' 納品書出力(未使用となった)
            dr("NOHIN_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.NOHIN_KB.ColNo)).Trim()
            ' 更新日
            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.SYS_UPD_DATE.ColNo)).Trim()
            ' 更新時刻
            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprDetailDef.SYS_UPD_TIME.ColNo)).Trim()
        End With

        rtDs.Tables(LMI540C.TABLE_NM.IN_PRINT).Rows.Add(dr)

    End Sub

#End Region

#Region "画面データ取得成功時"

    ''' <summary>
    ''' 画面データ取得成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">取得結果DataSet</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMI540F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI540C.TABLE_NM.OUT)

        ' 画面解除
        Call MyBase.UnLockedControls(frm)

        ' スプレッドに取得データをセット
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        ' データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        ' カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
        End If

    End Sub

#End Region

#End Region 'Method

End Class