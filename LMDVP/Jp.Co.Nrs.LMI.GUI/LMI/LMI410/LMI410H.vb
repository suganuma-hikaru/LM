' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI410H : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Microsoft.Office.Interop

''' <summary>
''' LMI410ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI410H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI410F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI410V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI410G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _InDs As DataSet

    ''' <summary>
    '''検索条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 受信ファイルディレクトリ
    ''' </summary>
    ''' <remarks></remarks>
    Private _rcvDir As String

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMI410F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH("LMI410")

        'Gamenクラスの設定
        Me._G = New LMI410G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMI410V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '営業所は自営業所
        Me._G.SetcmbNrsBrCd()

        '営業所コード、荷主コードをセット
        Call Me._G.SetCustData()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        'ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'コントロール個別設定(EDI取込日、取込区分)
        Dim sysDate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetInitControl(Me.GetPGID(), Me._Frm, sysDate(0))

        '荷主名をキャッシュよりセット
        Me.SetCachedNameCust(Me._Frm)

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理(F9)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI410C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 取込処理(F1)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikomiEvent()

        'DataSet設定
        Dim ds As DataSet = New LMI410DS()
        Dim errHashTable As Hashtable = New Hashtable

        ''単項目チェック
        'If Me._V.IsTorikomiSingleCheck(Me._G) = False Then
        '    Call Me._ControlH.EndAction(Me._Frm) '終了処理
        '    Exit Sub
        'End If

        '取込処理
        Call Me.TorikomiByk(Me._Frm, ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'エラーエクセルの出力
            Call Me.OutputExcel(Me._Frm)
        Else
            '取込処理成功時処理
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"取込処理", String.Empty})
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 移動報告(移動実績作成)処理(F2)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiHokokuEvent()

        'DataSet設定
        Dim ds As DataSet = New LMI410DS()
        Dim errHashTable As Hashtable = New Hashtable

        '項目チェック
        If Me._V.IsHoukokuIdoChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Sub
        End If

        '関連チェック
        errHashTable = Me._V.IsJissekiKanrenCheck(ds)

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1

        '全行エラーの場合処理終了
        If Me._ChkList.Count = errHashTable.Count Then

            If ds.Tables("LMI410_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(ds)
                Call Me.OutputExcel(Me._Frm)
            End If

            Call Me._ControlH.EndAction(Me._Frm)
            Exit Sub
        End If

        Call Me.IdoHokoku(Me._Frm, errHashTable, ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'エラーエクセルの出力
            Call Me.OutputExcel(Me._Frm)
        Else
            '取込処理成功時処理
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"移動報告", String.Empty})
        End If

    End Sub

    ''' <summary>
    ''' 実行処理(BYK入出荷実績作成) 実行ボタン
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub exeEvent(ByVal frm As LMI410F)

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI410C.EventShubetsu.EXE) = False Then

            ''画面解除
            'MyBase.UnLockedControls(frm)

            ''Cursorを元に戻す
            'Cursor.Current = Cursors.Default()
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            Exit Sub
        End If

        '入力チェック
        If Me._V.IsExeCheck() = False Then

            ''画面解除
            'MyBase.UnLockedControls(Me._Frm)
            ''Cursorを元に戻す
            'Cursor.Current = Cursors.Default()
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)
            Exit Sub

        End If

        'DataSet設定
        Me._InDs = New LMI410DS()

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {frm.cmbHoukoku.SelectedText})

        If rtn = MsgBoxResult.Ok Then

        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Call Me._ControlH.EndAction(frm)
            Exit Sub
        End If

        'データセット
        Call Me.SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "exeEvent")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, "JIssekiData", Me._InDs)

        If MyBase.IsMessageStoreExist = True Then
            Call Me.OutputExcel(frm)
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

        ElseIf IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = True Then

                'エラーメッセージの場合
                MyBase.ShowMessage(frm)

                ''画面解除
                'MyBase.UnLockedControls(frm)

                ''Cursorを元に戻す
                'Cursor.Current = Cursors.Default()
                '処理終了アクション
                Call Me._ControlH.EndAction(frm)

                Exit Sub
            Else
                '帳票のエラーメッセージの場合
                MyBase.ShowMessage(frm)
                '処理終了アクション
                ''画面解除
                'MyBase.UnLockedControls(frm)

                ''Cursorを元に戻す
                'Cursor.Current = Cursors.Default()
                '処理終了アクション
                Call Me._ControlH.EndAction(frm)
                Exit Sub
            End If

        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)

        '終了メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {frm.cmbHoukoku.SelectedText, String.Empty})

    End Sub

    ''' <summary>
    ''' 一括変更ボタン処理(一括変更ボタン押下時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ikkatuEvent(ByVal frm As LMI410F)

        'DataSet設定
        Dim ds As DataSet = New LMI410DS()
        Dim errHashTable As Hashtable = New Hashtable

        '項目チェック
        If Me._V.IsIkkatuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Sub
        End If

        '関連チェック
        errHashTable = Me._V.IsJissekiKanrenCheck(ds)

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1

        '全行エラーの場合処理終了
        If Me._ChkList.Count = errHashTable.Count Then

            If ds.Tables("LMI410_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(ds)
                Call Me.OutputExcel(Me._Frm)
            End If

            Call Me._ControlH.EndAction(Me._Frm)
            Exit Sub
        End If

        Call Me.Ikkatuhenko(Me._Frm, errHashTable, ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'エラーエクセルの出力
            Call Me.OutputExcel(Me._Frm)
        Else
            '取込処理成功時処理
            MyBase.ShowMessage(Me._Frm, "G002", New String() {"一括変更", String.Empty})
        End If

    End Sub

    ''' <summary>
    ''' マスタ参照処理(Pause)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        '背景色クリア
        'Me._ControlG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI410C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMIControlC.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.StartAction()

        '項目チェック
        If Me._V.IsMasterShowInputChk(objNm) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop起動処理
        Call Me.ShowPopupControl(objNm)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI410C.SprIdoListColumnIndex.DEF

        Return Me._ControlV.SprSelectList(defNo, Me._Frm.sprIdoList)

    End Function

#End Region

#Region "取込処理(BYK)　主処理"

    ''' <summary>
    ''' 取込処理(BYK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikomiByk(ByVal frm As LMI410F, ByVal ds As DataSet)

        Dim rcv_dir As String = String.Empty
        Dim work_dir As String = String.Empty

        '========= ファイル選択処理 =======
        'WindowsDialogインスタンス生成
        Dim ofd As New OpenFileDialog()

        'WindowsDialogのタイトル設定
        ofd.Title = "取込むファイルを選択してください"

        '[ファイルの種類]に表示される選択肢を制限
        Dim Delimiter As String = "*.xlsx"

        Dim filter As String = String.Concat("Excelファイル(", Delimiter, ")|", Delimiter)
        ofd.Filter = filter
        ofd.FilterIndex = 1

        ofd.Multiselect = False

        'ファイル名取得
        Dim objFiles As ArrayList = New ArrayList
        Dim arrCnt As Integer = 0
        If ofd.ShowDialog() = DialogResult.OK Then
            For Each newArr As String In ofd.SafeFileNames
                objFiles.Add(newArr)
            Next
        Else
            Exit Sub
        End If

        '不要ダイアログのゴミ削除
        ofd.Dispose()

        arrCnt = objFiles.Count
        '========= ファイル選択処理 =======

        'ファイルパス取得(ファイルまでのフルパスからファイル分のパスを消去でディレクトリを確保)
        rcv_dir = ofd.FileNames(0).ToString()
        'rcv_dir = rcv_dir.Replace(objFiles(0).ToString(), "")
        _rcvDir = rcv_dir

        '======================受信ファイル操作 -ED- ======================
        'コネクションリスト
        Dim arrCloser As ArrayList = New ArrayList

        If Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, ds, arrCloser) = False Then
            '各クローズ処理
            DoCloseAction(ds, arrCloser, 0)
            Exit Sub
        End If

        ''関連チェック
        'If Me._V.IsTorikomiKanrenCheck(rtDs) = False Then
        '    Call Me._ControlH.EndAction(Me._Frm) '終了処理
        '    Exit Sub
        'End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiByk")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI410BLF", "TorikomiByk", ds)

        Dim rtnFile_Name_Rcv As String = String.Empty
        Dim rtnFile_Name_Bak As String = String.Empty
        Dim noExtends As String = String.Empty
        'ファイル保存ダイアログ
        Dim sfd As New FolderBrowserDialog

        'バックアップディレクトリ
        Dim backDir As String = String.Empty

        'ダイアログタイトル
        sfd.Description = "バックアップファイルを保存するフォルダを選択してください"

        'ファイル保存ダイアログ[初期ディレクトリ]
        sfd.RootFolder = Environment.SpecialFolder.Desktop

        '選択フォルダ設定
        sfd.SelectedPath = rcv_dir

        '新規フォルダ作成の許可

        'ダイアログ展開
        Dim dlogResult As DialogResult = sfd.ShowDialog()

        If dlogResult = DialogResult.OK Then
            'OKなら

            '選択ディレクトリ
            backDir = String.Concat(sfd.SelectedPath, "\")

        ElseIf dlogResult = DialogResult.Cancel Then
            'CANCELなら

            '取込時ディレクトリ
            backDir = rcv_dir
        End If

        'ダイアログのごみを破棄する
        sfd.Dispose()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            '[正常時処理]
            '受信ファイルのロックを解除 + オリジナルの削除＆コピーの作成

            'ファイル名の変更
            noExtends = System.IO.Path.GetFileNameWithoutExtension(String.Concat(rcv_dir, rtnFile_Name_Rcv))
            rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), rtnFile_Name_Rcv.Replace(noExtends, ""))

            'ファイルのコピーを作成
            System.IO.File.Copy(String.Concat(rcv_dir, rtnFile_Name_Rcv), String.Concat(backDir, rtnFile_Name_Bak), True)

            '各クローズ処理
            DoCloseAction(ds, arrCloser, 0)

            'オリジナル削除
            System.IO.File.Delete(String.Concat(rcv_dir, rtnFile_Name_Rcv))
        Else
            '[エラー時処理]
            '受信ファイルのロックを解除

            '各クローズ処理
            DoCloseAction(ds, arrCloser, 0)

            'エラーエクセルの出力
            Call Me.OutputExcel(Me._Frm)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiByk")

    End Sub

#Region "取込詳細データセット(EXCEL)"

    ''' <summary>
    ''' 取込明細(EXCEL)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiExcelStanderdEdition(ByVal frm As LMI410F, ByVal ds As DataSet, ByRef arrCloser As ArrayList) As Boolean

        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList

        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 5

        Dim rowNoMin As Integer = 5                                                                 '行の開始数
        Dim colNoMax As Integer = 17                                                                '列の最大数
        Dim rowNoKey As Integer = 1                                                                 'Cashに登録されるまで、とりあえず１列目を設定

        '荷主明細取得
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()       '営業所コード
        Dim custCdL As String = frm.txtCustCdL.TextValue.ToString()     '荷主コード(大)

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

        Try
            xlBook = xlBooks.Open(_rcvDir)
        Catch ex As Exception
            Me.ShowMessage(frm, "E048")
            Return False
        End Try

        'シート
        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

        xlApp.Visible = False

        '最大行を取得(rowNoKey列の最終入力行を取得)
        Dim rowNoMax As Integer = 0

        xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        rowNoMax = xlApp.ActiveCell.Row

        '２次元配列に取得する
        Dim arrData(,) As Object
        arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

        '移動タイプ(MOVE_TYPE)の取得(１行目の右3byte)
        Dim moveType As String = Right(arrData(1, 1).ToString().Trim(), 3)

        '２次元→DSにセットする
        For j As Integer = rowNoMin To rowNoMax

            If arrData(j, rowNoKey) Is Nothing Then

                Continue For
            Else
                If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                    Continue For
                End If
            End If

            dr = ds.Tables(LMI410C.TABLE_NM_H_IDOEDI_DTL_BYK).NewRow()

            'DSに格納
            For k As Integer = 1 To colNoMax


                dr("DEL_KB") = LMConst.FLG.OFF
                dr("CRT_DATE") = MyBase.GetSystemDateTime()(0)
                dr("FILE_NAME") = _rcvDir
                dr("REC_NO") = String.Format("{0:00000}", gyoCount)
                dr("GYO") = "000"
                dr("NRS_BR_CD") = brCd
                dr("MOVE_TYPE") = moveType

                If arrData(j, k) Is Nothing Then
                    Continue For
                End If

                Select Case k

                    Case 1
                        dr("ITEM_NO") = arrData(j, k).ToString().Trim()
                    Case 2
                        dr("POSTING_DATE") = arrData(j, k).ToString().Trim()
                    Case 3
                        dr("TEXT") = arrData(j, k).ToString().Trim()
                    Case 4
                        dr("CURRENT_MATERIAL") = arrData(j, k).ToString().Trim()
                    Case 5
                        dr("CURRENT_DESCRIPTION") = arrData(j, k).ToString().Trim()
                    Case 6
                        dr("CURRENT_PLANT") = arrData(j, k).ToString().Trim()
                    Case 7
                        dr("CURRENT_STORAGE_LOCATION") = arrData(j, k).ToString().Trim()
                    Case 8
                        dr("CURRENT_BATCH") = arrData(j, k).ToString().Trim()
                    Case 9
                        dr("CURRENT_QUANTITY") = arrData(j, k).ToString().Trim()
                    Case 10
                        dr("CURRENT_UOM") = arrData(j, k).ToString().Trim()
                    Case 11
                        dr("DESTINATION_MATERIAL") = arrData(j, k).ToString().Trim()
                    Case 12
                        dr("DESTINATION_DESCRIPTION") = arrData(j, k).ToString().Trim()
                    Case 13
                        dr("DESTINATION_PLANT") = arrData(j, k).ToString().Trim()
                    Case 14
                        dr("DESTINATION_STORAGE_LOCATION") = arrData(j, k).ToString().Trim()
                    Case 15
                        dr("DESTINATION_BATCH") = arrData(j, k).ToString().Trim()
                    Case 16
                        dr("DESTINATION_QUANTITY") = arrData(j, k).ToString().Trim()
                    Case 17
                        dr("DESTINATION_UOM") = arrData(j, k).ToString().Trim()

                End Select

            Next
            'DSにAdd
            ds.Tables(LMI410C.TABLE_NM_H_IDOEDI_DTL_BYK).Rows.Add(dr)

            gyoCount = gyoCount + 1

        Next

        If gyoCount = 5 Then
            Me.ShowMessage(frm, "E370")
            Return False
        End If

        colection.Clear()
        colection.Add(xlCell)    '0
        colection.Add(xlSheet)   '1
        colection.Add(xlBook)    '2
        colection.Add(xlBooks)   '3
        colection.Add(xlApp)     '4

        arrCloser.Add(colection)

        Return True

    End Function

#End Region

#Region "移動報告データセット"
    ''' <summary>
    ''' 移動報告データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataIdoHokoku(ByVal frm As LMI410F, ByVal rtDs As DataSet, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprIdoList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMI410IN_IDO_HOKOKU
                dr = rtDs.Tables(LMI410C.TABLE_NM_IN_IDO_HOKOKU).NewRow()
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.NRS_BR_CD.ColNo))
                dr.Item("CRT_DATE") = Replace(Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.CRT_DATE.ColNo)), "/", String.Empty)
                dr.Item("FILE_NAME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.REC_NO.ColNo))
                dr.Item("ROW_NO") = selectRow
                dr.Item("HAITA_SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.SYS_UPD_DATE.ColNo))
                dr.Item("HAITA_SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.SYS_UPD_TIME.ColNo))
                rtDs.Tables(LMI410C.TABLE_NM_IN_IDO_HOKOKU).Rows.Add(dr)

            Next

        End With

    End Sub

#End Region

#Region "一括変更データセット"
    ''' <summary>
    ''' 一括変更データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataIkkatuHenko(ByVal frm As LMI410F, ByVal rtDs As DataSet, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprIdoList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMI410IN_IKKATU_HENKO
                dr = rtDs.Tables(LMI410C.TABLE_NM_IN_IKKATU_HENKO).NewRow()
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.NRS_BR_CD.ColNo))
                dr.Item("CRT_DATE") = Replace(Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.CRT_DATE.ColNo)), "/", String.Empty)
                dr.Item("FILE_NAME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.FILE_NAME.ColNo))
                dr.Item("REC_NO") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.REC_NO.ColNo))
                dr.Item("HAITA_SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.SYS_UPD_DATE.ColNo))
                dr.Item("HAITA_SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI410G.sprIdoList.SYS_UPD_TIME.ColNo))
                dr.Item("CUST_CD_L") = frm.txtIkkatuCustL.TextValue()
                dr.Item("CUST_CD_M") = frm.txtIkkatuCustM.TextValue()
                dr.Item("POSTING_DATE") = frm.imdIkkatuDate.TextValue()
                dr.Item("ROW_NO") = selectRow
                rtDs.Tables(LMI410C.TABLE_NM_IN_IKKATU_HENKO).Rows.Add(dr)

            Next

        End With

    End Sub

#End Region

    ''' <summary>
    ''' 各種閉じる処理の実行
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <param name="arrCloser"></param>
    ''' <remarks></remarks>
    Private Sub DoCloseAction(ByVal rtDs As DataSet, ByVal arrCloser As ArrayList, ByVal nawRow As Integer)

        Dim connect As ArrayList = New ArrayList

        If arrCloser(nawRow) Is Nothing Then
            '間違いなくエラーです。
            Exit Sub
        End If

        'アレイの一行コピー
        connect.AddRange(CType(arrCloser(nawRow), Collections.ICollection))

        'コネクションの確認
        If arrCloser(nawRow) Is Nothing Then
            '間違いなくエラーです。
        End If

        '分解
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

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing

        xlBook.Close(False) 'Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing

        xlApp.DisplayAlerts = False
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

    End Sub

#End Region

#Region "移動報告(BYK)　主処理"

    ''' <summary>
    ''' 移動報告
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub IdoHokoku(ByVal frm As LMI410F, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"移動報告"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMI410_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMI410DS()
        Call Me.SetDataIdoHokoku(frm, rtDs, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "IdoHokoku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI410BLF", "IdoHokoku", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '入荷登録処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)
            Call Me._ControlH.EndAction(frm)
            Exit Sub
        Else

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "IdoHokoku")

        Call Me._ControlH.EndAction(frm)

    End Sub

#End Region

#Region "一括変更(BYK)　主処理"

    ''' <summary>
    ''' 一括変更ボタン押下時
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Ikkatuhenko(ByVal frm As LMI410F, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMI410_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMI410DS()
        Call Me.SetDataIkkatuHenko(frm, rtDs, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Ikkatuhenko")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI410BLF", "Ikkatuhenko", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '入荷登録処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)
            Call Me._ControlH.EndAction(frm)
            Exit Sub
        Else

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Ikkatuhenko")

        Call Me._ControlH.EndAction(frm)

    End Sub

#End Region

#Region "画面終了　主処理"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        Dim chkList As ArrayList = Me._V.getCheckList()

        If chkList.Count = 0 Then

            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                Exit Sub

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#End Region

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            If Me._V.IsAuthorityChk(LMI410C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMIControlC.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Call Me.StartAction()

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction()

        'マスタメンテ共通処理
        Me._ControlH.StartAction(Me._Frm)

        '背景色クリア
        'Me._ControlG.SetBackColor(Me._Frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData()

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '04'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)


        'DataSet設定
        Me._InDs = New LMI410DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(DirectCast(Me._Frm, Form), "LMI410BLF", "SelectListData", Me._InDs, lc)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI410C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprIdoList.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMI410C.TABLE_NM_OUT)
        Call Me._G.SetSpread(dt)

        '文字色を変更
        Call Me._G.SetSpreadColor(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprIdoList.CrearSpread()

    End Sub

    ''' <summary>
    ''' 荷主名取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCachedNameCust(ByVal frm As LMI410F)

        With frm

            Dim custCdL As String = .txtCustCdL.TextValue
            Dim custCdM As String = .txtCustCdM.TextValue

            '荷主名称
            .txtCustNm.TextValue = String.Empty
            If String.IsNullOrEmpty(custCdL) = False Then
                If String.IsNullOrEmpty(custCdM) = True Then
                    custCdM = "00"
                End If

                Dim custDr() As DataRow = Me._ControlG.SelectCustListDataRow(custCdL, custCdM)

                If 0 < custDr.Length Then
                    .txtCustNm.TextValue = custDr(0).Item("CUST_NM_L").ToString()
                End If
            End If
        End With
    End Sub

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String) As Boolean

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name _
                   , .txtCustCdM.Name _
                   , .txtIkkatuCustL.Name _
                   , .txtIkkatuCustM.Name
                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(Me._Frm, objNm)

            End Select

        End With

        Return True

    End Function

#Region "荷主マスタ"
    ''' <summary>
    ''' 荷主マスタ戻り値設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMI410F, ByVal objNm As String) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm

                Select objNm

                    Case .txtCustCdL.Name _
                       , .txtCustCdM.Name
                        frm.txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString                                                 '荷主コード（大）
                        frm.txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString                                                 '荷主コード（中）
                        frm.txtCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString, dr.Item("CUST_NM_M").ToString)    '荷主名

                    Case .txtIkkatuCustL.Name _
                       , .txtIkkatuCustM.Name
                        frm.txtIkkatuCustL.TextValue = dr.Item("CUST_CD_L").ToString                                                 '荷主コード（大）
                        frm.txtIkkatuCustM.TextValue = dr.Item("CUST_CD_M").ToString                                                 '荷主コード（中）
                        frm.lblIkkatuCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString, dr.Item("CUST_NM_M").ToString)    '荷主名

                End Select

            End With

        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI410F) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function
#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._InDs.Tables(LMI410C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprIdoList.ActiveSheet

            '検索条件を設定
            dr.Item("NRS_BR_CD") = Me._Frm.cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
            dr.Item("USER_CD") = Me._Frm.txtUserCd.TextValue
            dr.Item("CMB_SEARCH") = Me._Frm.cmbSearchDate.SelectedValue
            dr.Item("SEARCH_FROM") = Me._Frm.imdSearchDateFrom.TextValue
            dr.Item("SEARCH_TO") = Me._Frm.imdSearchDateTo.TextValue
            dr.Item("CMB_JISSEKI") = Me._Frm.cmbHoukoku.SelectedValue

            dr.Item("STATE_KBN") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.SAGYO_STATE_NM.ColNo))
            dr.Item("SYORI_KBN") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.SYORI_SUB.ColNo))
            dr.Item("FILE_NAME") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.FILE_NAME.ColNo))
            dr.Item("PRINT_KBN") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.PRINT_FLG.ColNo))
            dr.Item("TEXT") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.TEXT_NM.ColNo))
            dr.Item("CURRENT_MATERIAL") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.CURRENT_MATERIAL.ColNo))
            dr.Item("CURRENT_DESCRIPTION") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.CURRENT_DESCRIPTION.ColNo))
            dr.Item("CURRENT_GOODS_JOTAI") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.CURRENT_GOODS_JOTAI.ColNo))
            dr.Item("CURRENT_BATCH") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.CURRENT_BATCH.ColNo))
            dr.Item("DEST_NM") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.DEST_NM.ColNo))
            dr.Item("DESTINATION_MATERIAL") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.DESTINATION_MATERIAL.ColNo))
            dr.Item("DESTINATION_DESCRIPTION") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.DESTINATION_DESCRIPTION.ColNo))
            dr.Item("DESTINATION_GOODS_JOTAI") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.DESTINATION_GOODS_JOTAI.ColNo))
            dr.Item("DESTINATION_BATCH") = Me._ControlV.GetCellValue(.Cells(0, LMI410G.sprIdoList.DESTINATION_BATCH.ColNo))

            Me._InDs.Tables(LMI410C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMI410_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMI410_GUIERROR").Rows(i)

            If String.IsNullOrEmpty(dr("PARA1").ToString()) = False Then
                prm1 = dr("PARA1").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA2").ToString()) = False Then
                prm2 = dr("PARA2").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA3").ToString()) = False Then
                prm3 = dr("PARA3").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA4").ToString()) = False Then
                prm4 = dr("PARA4").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA5").ToString()) = False Then
                prm5 = dr("PARA5").ToString()
            End If
            MyBase.SetMessageStore(dr("GUIDANCE_ID").ToString() _
                     , dr("MESSAGE_ID").ToString() _
                     , New String() {prm1, prm2, prm3, prm4, prm5} _
                     , dr("ROW_NO").ToString() _
                     , dr("KEY_NM").ToString() _
                     , dr("KEY_VALUE").ToString())

        Next

        Return ds

    End Function

#End Region

#Region "EXCEL出力処理"

    Private Sub OutputExcel(ByVal frm As LMI410F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub


#End Region


#Region "各種コンボ値変更"

    Private Sub CmbChange(ByVal frm As LMI410F, ByVal cmbSyubetu As Integer)

        'ロック制御
        Call Me._G.LockDisp(frm, cmbSyubetu)

        '終了メッセージ表示
        MyBase.SetMessage("G007")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

    End Sub
#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(取込処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiEvent")

        Me.TorikomiEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiHokokuEvent")

        Me.JissekiHokokuEvent()

        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiHokokuEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI410F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================
    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI410FKeyDown(ByVal frm As LMI410F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMI410F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMI410F_KeyDown")

    End Sub

    ''' <summary>
    ''' 実績報告コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub cmbHoukoku(ByVal frm As LMI410F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CmbChange(frm, LMI410C.comboShubetsu.JISSEKI_CMB)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 検索・実績条件コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub cmbSearchDate(ByVal frm As LMI410F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CmbChange(frm, LMI410C.comboShubetsu.SEARCH_HOUKOKU_CMB)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 一括変更コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub cmbIkkatuKbn(ByVal frm As LMI410F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CmbChange(frm, LMI410C.comboShubetsu.IKKATU_CMB)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 実行ボタン押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub btnJikkou(ByRef frm As LMI410F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '実行処理の呼び出し
        Call Me.exeEvent(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatu_Click(ByRef frm As LMI410F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '一括変更処理の呼び出し
        Call Me.ikkatuEvent(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region
#End Region

End Class