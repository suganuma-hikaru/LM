' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI490H : ローム　棚卸対象商品リスト
'  作  成  者       :  kido
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Com.Utility
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop

''' <summary>
''' LMI490ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI490H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI490V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI490G

    ''' <summary>
    ''' パラメータ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

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
    ''' EXCELファイルディレクトリ
    ''' </summary>
    ''' <remarks></remarks>
    Private _rcvDir As String

    ''' <summary>
    ''' CANCELフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _cancelFlg As Boolean

#End Region

#Region "Method"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Dim frm As LMI490F = New LMI490F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI490G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI490V(Me, frm, Me._LMIconV, Me._LMIconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G006")

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

#Region "作成処理"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function MakeExcel(ByVal frm As LMI490F) As Boolean

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI490C.EventShubetsu.SAKUSEI) = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)
            Return False

        End If

        'DataSet設定
        Dim ds As DataSet = New LMI490DS()
        'ADD START 2018/11/27 要望管理002837
        Dim dr As DataRow = ds.Tables(LMI490C.TABLE_NM_IN).NewRow()
        ds.Tables(LMI490C.TABLE_NM_IN).Rows.Add(dr)
        'ADD END   2018/11/27 要望管理002837

        'EXCEL取込処理
        ds = Me.TorikomiExcel(frm, ds)

        '取り込みダイアログでキャンセル時
        If _cancelFlg = True Then
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm, "G006")

            Return False
        End If

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False
        End If
        If IsMessageExist() = True Then
            'メッセージ表示
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False
        End If

        'データセット
        Dim rtDs As DataSet = Me.SetDataSetInData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = Nothing
        rtnDs = MyBase.CallWSA(blf, LMI490C.SELECT_DATA, rtDs)

        'ADD START 2018/11/27 要望管理002837
        'メッセージ判定
        If IsMessageExist() = True Then
            'メッセージ表示
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False

        End If
        'ADD END   2018/11/27 要望管理002837

        'データ有無の判定
        Dim noData As Boolean = False
        If rtnDs.Tables(LMI490C.TABLE_NM_OUT).Rows.Count = 0 Then
            noData = True
        End If

        'データなしの場合メッセージ表示
        If noData = True Then

            MyBase.SetMessage("G001")

            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False

        End If

        'EXCELファイルチェック
        Dim xlsPath As String = String.Empty
        Dim xlsFileName As String = String.Empty
        Dim xlsSavePath As String = String.Empty
#If True Then   'ADD 2020/09/04 014227   【LMS】DDPの棚卸しリスト作成PG修正
        Dim kbn_cd As String = "01".ToString
        If frm.optCustDPP.Checked = True Then
            kbn_cd = "02"
        End If
#End If
#If False Then   'ADD 2020/09/04 014227   【LMS】DDPの棚卸しリスト作成PG修正
        Call Me.ExcelFileCheck(xlsPath, xlsFileName, xlsSavePath)
#Else
        Call Me.ExcelFileCheck(xlsPath, xlsFileName, xlsSavePath, kbn_cd)
#End If
        'メッセージ判定
        If IsMessageExist() = True Then
            'メッセージ表示
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False

        End If

        'EXCEL出力処理
        Call Me.MakeExcel01(rtnDs, xlsPath, xlsFileName, xlsSavePath)

        '処理終了アクション
        Call Me._LMIconH.EndAction(frm)

        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"棚卸対象商品リスト作成", ""})

        MyBase.ShowMessage(frm)

        Return True

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI490F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI490F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    ''' <summary>
    ''' 作成ボタン押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub btnMake(ByRef frm As LMI490F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '実行処理の呼び出し
        Call Me.MakeExcel(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "取込処理(EXCEL)"

    ''' <summary>
    ''' 取込処理(EXCEL)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function TorikomiExcel(ByVal frm As LMI490F, ByVal ds As DataSet) As DataSet

        Dim rcv_dir As String = String.Empty
        Dim work_dir As String = String.Empty

        '========= ファイル選択処理 =======
        'WindowsDialogインスタンス生成
        Dim ofd As New OpenFileDialog()

        'WindowsDialogのタイトル設定
        ofd.Title = "取込むファイルを選択してください"

        '[ファイルの種類]に表示される選択肢を制限
        Dim Delimiter As String = "*.xls;*.xlsx"

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

            _cancelFlg = False

        Else
            _cancelFlg = True

            Return ds
        End If

        '不要ダイアログのゴミ削除
        ofd.Dispose()

        arrCnt = objFiles.Count
        '========= ファイル選択処理 =======

        'ファイルパス取得(ファイルまでのフルパスからファイル分のパスを消去でディレクトリを確保)
        rcv_dir = ofd.FileNames(0).ToString()
        Dim rcv_dir2 As String = rcv_dir.Replace(objFiles(0).ToString(), "")
        _rcvDir = rcv_dir

        'ADD START 2018/11/27 要望管理002837
        Dim dr As DataRow = ds.Tables(LMI490C.TABLE_NM_IN).Rows(0)
        dr.Item("FILE_NAME") = objFiles(0).ToString()
        'ADD END   2018/11/27 要望管理002837

        '======================受信ファイル操作 -ED- ======================
        'コネクションリスト
        Dim arrCloser As ArrayList = New ArrayList

        'EXCELデータ取り込み
        ds = Me.SetDataTorikomiShosaiExcel(frm, ds, arrCloser)

        If MyBase.IsMessageStoreExist = True Then
            '各クローズ処理
            DoCloseAction(ds, arrCloser, 0)
            Return ds
        End If

        If IsMessageExist() = True Then
            '処理終了アクション
            DoCloseAction(ds, arrCloser, 0)
            Return ds
        End If

        Dim rtnFile_Name_Rcv As String = objFiles(0).ToString
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
            backDir = rcv_dir2
        End If

        'ダイアログのごみを破棄する
        sfd.Dispose()

        'ファイル名の変更
        noExtends = System.IO.Path.GetFileNameWithoutExtension(rcv_dir)
        rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), rtnFile_Name_Rcv.Replace(noExtends, ""))

        'ファイルのコピーを作成
        System.IO.File.Copy(rcv_dir, String.Concat(backDir, rtnFile_Name_Bak), True)

        '各クローズ処理
        DoCloseAction(ds, arrCloser, 0)

        'オリジナル削除
        System.IO.File.Delete(rcv_dir)

        Return ds

    End Function

#End Region

#Region "取込詳細データセット(EXCEL)"

    ''' <summary>
    ''' 取込詳細(EXCEL)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function SetDataTorikomiShosaiExcel(ByVal frm As LMI490F, ByVal ds As DataSet, ByRef arrCloser As ArrayList) As DataSet

        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList

        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 2

        Dim rowNoMin As Integer = 2                                                                 '行の開始数
        Dim colNoMax As Integer = 15                                                                '列の最大数
        Dim rowNoKey As Integer = 1                                                                 'Cashに登録されるまで、とりあえず１列目を設定

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
            MyBase.SetMessage("E048")
            Return ds
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

        Dim targetStr As String = String.Empty

        '２次元→DSにセットする
        For j As Integer = rowNoMin To rowNoMax

            If arrData(j, rowNoKey) Is Nothing Then
                Continue For
            Else
                If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                    Continue For
                End If
            End If

            dr = ds.Tables(LMI490C.TABLE_NM_IN_EXCEL).NewRow()

            dr("ERR_FLG") = "0"
            dr("REC_NO") = Convert.ToString(j)  'ADD 2018/11/27 要望管理002837

            '商品コード(B列)をDSに格納
            If arrData(j, 2) Is Nothing Then
                MyBase.SetMessageStore(LMI490C.GUIDANCE_KBN, "E001", New String() {String.Concat("商品コード(カラム2番目)")}, Convert.ToString(j), "", "")
                dr("GOODS_CD_CUST") = ""
                dr("ERR_FLG") = "1"
            Else
                dr("GOODS_CD_CUST") = arrData(j, 2).ToString().Trim()
                targetStr = dr.Item("GOODS_CD_CUST").ToString.Trim()
                If targetStr.Length > 20 Then
                    MyBase.SetMessageStore(LMI490C.GUIDANCE_KBN, "E518", New String() {String.Concat("商品コード(カラム2番目)[", targetStr, "]"), ""}, Convert.ToString(j), "", "")
                    dr("ERR_FLG") = "1"
                End If
            End If

            'DSにAdd
            ds.Tables(LMI490C.TABLE_NM_IN_EXCEL).Rows.Add(dr)

            gyoCount = gyoCount + 1

        Next

        'EXCEL明細データなし
        If gyoCount = 2 Then
            MyBase.SetMessage("E462")
        End If

        colection.Clear()
        colection.Add(xlCell)    '0
        colection.Add(xlSheet)   '1
        colection.Add(xlBook)    '2
        colection.Add(xlBooks)   '3
        colection.Add(xlApp)     '4

        arrCloser.Add(colection)

        Return ds

    End Function

#End Region

#Region "EXCELクローズ処理"

    ''' <summary>
    ''' 各種閉じる処理の実行
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <param name="arrCloser"></param>
    ''' <remarks></remarks>
    Private Sub DoCloseAction(ByVal rtDs As DataSet, ByVal arrCloser As ArrayList, ByVal nawRow As Integer)

        Dim connect As ArrayList = New ArrayList

        If arrCloser.Count = 0 Then
            Exit Sub
        End If

        If arrCloser(nawRow) Is Nothing Then
            Exit Sub
        End If

        'アレイの一行コピー
        connect.AddRange(CType(arrCloser(nawRow), Collections.ICollection))

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

#Region "データセット"
    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMI490F, ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables(LMI490C.TABLE_NM_IN).Rows(0)  'MOD 2018/11/27 要望管理002837 NewRow()→Rows(0)
        Dim dr2 As DataRow = Nothing

        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString
            '' ds.Tables(LMI490C.TABLE_NM_IN).Rows.Add(dr)          'DEL 2018/11/27 要望管理002837

            '対象荷主コードの取得
#If True Then   'ADD 2020/09/03 014227   【LMS】DDPの棚卸しリスト作成PG修正
            Dim KBN_NM3 As String = String.Empty
            If frm.optCustDPP.Checked = True Then
                KBN_NM3 = "1"
            End If
#End If
            Dim kbnDr() As DataRow = Nothing
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'R020' AND ",
                                                                               "KBN_NM1 = '", .cmbEigyo.SelectedValue.ToString, "'",
                                                                               "AND KBN_NM3 = '", KBN_NM3.ToString, "'"))
            If kbnDr.Length > 0 Then
                Dim max As Integer = kbnDr.Length - 1
                For i As Integer = 0 To max
                    dr2 = ds.Tables(LMI490C.TABLE_NM_IN_CUST).NewRow()
                    dr2.Item("CUST_CD_L") = kbnDr(i).Item("KBN_NM2").ToString
                    ds.Tables(LMI490C.TABLE_NM_IN_CUST).Rows.Add(dr2)
                Next
            End If

            Return ds

        End With

    End Function

#End Region

    ''' <summary>
    ''' EXCELファイルチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExcelFileCheck(ByRef xlsPath As String, ByRef xlsFileName As String, ByRef xlsSavePath As String, ByVal kbn_cd As String)

        'ファイルパス取得
#If False Then   'ADD 2020/09/03 014227   【LMS】DDPの棚卸しリスト作成PG修正
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'R021' AND ",
                                                                                                       "KBN_CD = '01' "))
#Else
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'R021' AND ",
                                                                                                       "KBN_CD = '", kbn_cd.ToString, "'"))

#End If
        xlsPath = xlsPathKbn(0).Item("KBN_NM2").ToString
        xlsFileName = xlsPathKbn(0).Item("KBN_NM3").ToString
        xlsSavePath = xlsPathKbn(0).Item("KBN_NM4").ToString

        'フォルダの存在確認
        If System.IO.Directory.Exists(xlsSavePath) = False Then
            '無ければ作成する
            System.IO.Directory.CreateDirectory(xlsSavePath)
        End If

    End Sub

    ''' <summary>
    ''' EXCEL出力処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function MakeExcel01(ByVal ds As DataSet, ByVal xlsPath As String, ByVal xlsFileName As String, ByRef xlsSavePath As String) As Boolean

        'EXCEL起動
        Dim xlApp As New Excel.Application
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        'エクセルを新規作成
        xlApp.Workbooks.Add()
        xlBook = xlApp.Workbooks(1)
        xlSheet = CType(xlBook.Worksheets(1), Excel.Worksheet)
        'シート名設定
        xlSheet.Name = "棚卸対象商品リスト"

        Dim rowMax As Integer = ds.Tables("LMI490OUT").Rows.Count - 1
        Dim rowCnt As Integer = 2
        Dim dataRow As DataRow = Nothing

        '見出し行
        xlSheet.Cells(1, 1) = "荷主コード（大）"
        xlSheet.Cells(1, 2) = "商品コード"
        xlSheet.Cells(1, 3) = "商品名"
        xlSheet.Cells(1, 4) = "置場"
        xlSheet.Cells(1, 5) = "ロット№"
        xlSheet.Cells(1, 6) = "備考"
        xlSheet.Cells(1, 7) = "入目"
        xlSheet.Cells(1, 8) = "包装単位"
        xlSheet.Cells(1, 9) = "入数"
        xlSheet.Cells(1, 10) = "実予在庫個数"
        xlSheet.Cells(1, 11) = "引当中個数"
        xlSheet.Cells(1, 12) = "引当可能個数"

        For cnt As Integer = 0 To rowMax

            dataRow = ds.Tables("LMI490OUT").Rows(cnt)
            xlSheet.Cells(rowCnt, 1) = "'" & dataRow("CUST_CD_L").ToString
            xlSheet.Cells(rowCnt, 2) = "'" & dataRow("GOODS_CD_CUST").ToString
            xlSheet.Cells(rowCnt, 3) = "'" & dataRow("GOODS_NM_1").ToString
            xlSheet.Cells(rowCnt, 4) = dataRow("OKIBA")
            If String.IsNullOrEmpty(dataRow("LOT_NO").ToString) = False Then
                xlSheet.Cells(rowCnt, 5) = "'" & dataRow("LOT_NO").ToString
            End If
            If String.IsNullOrEmpty(dataRow("REMARK").ToString) = False Then
                xlSheet.Cells(rowCnt, 6) = "'" & dataRow("REMARK").ToString
            End If
            xlSheet.Cells(rowCnt, 7) = dataRow("IRIME")
            xlSheet.Cells(rowCnt, 8) = dataRow("PKG_UT")
            xlSheet.Cells(rowCnt, 9) = dataRow("IRISU")
            xlSheet.Cells(rowCnt, 10) = dataRow("PORA_ZAI_NB")
            xlSheet.Cells(rowCnt, 11) = dataRow("ALCTD_NB")
            xlSheet.Cells(rowCnt, 12) = dataRow("ALLOC_CAN_NB")

            rowCnt = rowCnt + 1

        Next

        '保存時の問合せのダイアログを非表示に設定
        xlApp.DisplayAlerts = False

        'エクセルの保存
        'TOアドレス設定
        Dim wkFileName As String() = Split(xlsFileName, ".")
        Dim xlsFileName2 As String = String.Empty
        xlsFileName2 = String.Concat(wkFileName(0), "_", DateTime.Now.ToString("yyyyMMddHHmmss"))
        xlBook.SaveAs(String.Concat(xlsSavePath, xlsFileName2))

        'ダイアログの表示設定を元に戻す
        xlApp.DisplayAlerts = True

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing
        xlBook.Close(False) 'Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

        Return True

    End Function

    ''' <summary>
    ''' ワークシート名のインデックスを返す
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getSheetIndex(ByVal sheetName As String, ByVal shs As Excel.Sheets) As Integer
        Dim i As Integer = 0
        For Each sh As Microsoft.Office.Interop.Excel.Worksheet In shs
            If sheetName = sh.Name Then
                Return i + 1
            End If
            i += 1
        Next
        Return 0
    End Function

#End Region

End Class