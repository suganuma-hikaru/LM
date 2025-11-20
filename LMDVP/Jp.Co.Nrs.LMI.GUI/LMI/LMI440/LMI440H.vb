' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI440  : 
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
''' LMI440ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI440H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI440V = Nothing

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI440G = Nothing

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
    Private ReadOnly BLF_NAME As String = LMI440C.MY_FORM_ID & LMControlC.BLF

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

        'フォームの作成
        Dim frm As LMI440F = New LMI440F(Me)

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
        Me._G = New LMI440G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI440V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))


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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI440C.EventShubetsu _
                           , ByVal frm As LMI440F)

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

                Case LMI440C.EventShubetsu.EXECUTE

                    '処理開始アクション
                    Me._LMIConH.StartAction(frm)

                    '入力チェック
                    If Me._V.IsSingleCheck(eventShubetsu) = False Then
                        '処理終了アクション
                        Me._LMIConH.EndAction(frm, LMI430C.MESSAGE_ID.NORMAL)
                        Exit Sub
                    End If


                    ' Excel作成
                    Call Me.EditExcel(frm)

                Case LMI440C.EventShubetsu.CLOSE_FORM

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
                Me._LMIConH.EndAction(frm, LMI440C.MESSAGE_ID.NORMAL)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI440F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI440F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI440C.EventShubetsu.EXECUTE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub



    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI440F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Me.ActionControl(LMI440C.EventShubetsu.CLOSE_FORM, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI440F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


#End Region 'イベント振分け




#Region "処理"



#Region " Excel編集出力"

    ''' <summary>
    ''' Excel編集
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditExcel(ByVal frm As LMI440F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

        Dim loadFilePath As String = ""

        Using dialog As New OpenFileDialog()
            dialog.Title = LMI440C.FILE_DIALOG.TITLE
            dialog.Filter = LMI440C.FILE_DIALOG.FILTER
            dialog.FilterIndex = 1
            dialog.Multiselect = False
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            If (dialog.ShowDialog() <> DialogResult.OK) Then
                Exit Sub
            End If

            loadFilePath = dialog.FileName
        End Using

        ' ファイル名
        If (Path.GetFileName(loadFilePath).Length > LMI440C.FILE_NAME_LENGTH) Then

            Me.ShowMessage(frm _
                         , "E482" _
                         , New String() {LMI440C.FILE_NAME_TEXT, LMI440C.FILE_NAME_LENGTH & LMI440C.CHARACTER_TEXT})

            Exit Sub
        End If


        ' エクセルファイルから必要な領域のデータを抽出
        Dim cellData(,) As Object = Me.ReadExcelFile(loadFilePath _
                                                   , LMI440C.READ_FILE_FORMAT.TARGET_SHEET_NO _
                                                   , LMI440C.READ_FILE_FORMAT.START_DATA_ROW_NO _
                                                   , LMI440C.READ_FILE_FORMAT.START_COL_NO _
                                                   , LMI440C.READ_FILE_FORMAT.COLUMN_COUNT)
        If (cellData Is Nothing) Then

            ' エラー
            MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})
            Exit Sub
        End If


        Dim resultTable As New LMI440DS.LMI440OUTDataTable
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

            Dim result As DataSet = MyBase.CallWSA(BLF_NAME, LMI440C.FUNCTION_NAME.SelectData, loadData)
            resultTable.Merge(result.Tables(resultTable.TableName))

        End Using

        ' 出力データ
        Dim editData As New Dictionary(Of Point, String)
        For Each row As LMI440DS.LMI440OUTRow In resultTable.AsEnumerable()
            Dim point As Point = New Point(row.ROW_NO, LMI440C.READ_FILE_FORMAT.COLUMN_INDEX.CARRIER_PASTE_IN_DATES)
            If (row IsNot Nothing AndAlso _
                Len(row.CARRIER_PASTE_IN_DATES) > 0) Then

                Dim carrDate As String = Com.Utility.DateFormatUtility.EditSlash(row.CARRIER_PASTE_IN_DATES)
                If (editData.Keys.Contains(point) = False) Then
                    editData.Add(point, carrDate)
                Else
                    If (editData(point).Contains(carrDate) = False) Then
                        editData(point) = String.Concat(editData(point), ", ", carrDate)
                    End If
                End If
            End If
        Next


        Dim saveFolderPath As String = Me.GetSaveFolderPath()
        If (String.IsNullOrEmpty(saveFolderPath)) Then

            MyBase.ShowMessage(frm, "S001", New String() {"出力フォルダパスの取得"})

            Exit Sub

        End If

        Dim saveFileName As String = Me.CreateSaveFileName(loadFilePath)



        If (Me.SaveAsEditExcelFile(loadFilePath _
                                 , saveFileName _
                                 , saveFolderPath _
                                 , LMI440C.READ_FILE_FORMAT.TARGET_SHEET_NO _
                                 , editData) = False) Then
            ' エラー
            Me.SetMessage("E428" _
                         , New String() {"保存中にエラーが発生した", "ファイル出力", ""})

        End If


        '処理終了アクション
        Me._LMIConH.EndAction(frm, LMI440C.MESSAGE_ID.NORMAL)


        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        ' 保存フォルダオープン
        System.Diagnostics.Process.Start(saveFolderPath)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"処理", ""})


        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' 登録用のデータを設定する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="cellData"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateInsertDataSet(ByVal frm As LMI440F _
                                       , ByVal cellData As Object(,) _
                                       , ByVal fileName As String) As DataSet


        Dim createData As New LMI440DS()



        For rowIndex As Integer = 1 To cellData.GetLength(LMI440C.ARRAY_INDEX.ROW_DIMENSION)

            Try
                Dim newRow As LMI440DS.LMI440IN_TRANSPORTRow = createData.LMI440IN_TRANSPORT.NewLMI440IN_TRANSPORTRow

                newRow.ROW_NO = rowIndex + 1
                newRow.DELIVERY_NOTE_NUMBER = Right(If(cellData.GetValue(rowIndex, LMI440C.READ_FILE_FORMAT.COLUMN_INDEX.DELIVERY_NOTE_NUMBER), "").ToString(), 9)
                newRow.SHIPMENT_DOCUMENT_NUMBER = Right(If(cellData.GetValue(rowIndex, LMI440C.READ_FILE_FORMAT.COLUMN_INDEX.SHIPMENT_DOCUMENT_NUMBER), "").ToString(), 8)

                If (String.IsNullOrWhiteSpace(newRow.DELIVERY_NOTE_NUMBER) = False OrElse _
                    String.IsNullOrWhiteSpace(newRow.SHIPMENT_DOCUMENT_NUMBER) = False) Then

                    createData.LMI440IN_TRANSPORT.AddLMI440IN_TRANSPORTRow(newRow)

                End If

            Catch ex As Exception

                Logger.WriteErrorLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, ex.Message, ex)

                ' 取込データに不正があります。確認してください。
                MyBase.SetMessageStore(LMI440C.GUIDANCE_KBN, "E100" _
                                     , Nothing _
                                     , rowIndex.ToString())
            End Try
        Next

        Dim fromDate As String = frm.imdArrPlanDateFrom.TextValue
        Dim toDate As String = frm.imdArrPlanDateTo.TextValue
        Dim dbName As String = Me.GetTransportDataBaseName()

        For Each row As DataRow In Me.GetKbnRows(LMI440C.CUST_CD_KBN.GROUP_CD)

            Dim newRow As LMI440DS.LMI440INRow = createData.LMI440IN.NewLMI440INRow

            newRow.NRS_BR_CD = If(row.Item(LMI440C.CUST_CD_KBN.NRS_BR_CD_COL), "").ToString()
            newRow.CUST_CD_L = If(row.Item(LMI440C.CUST_CD_KBN.CUST_CD_L_COL), "").ToString()
            newRow.CUST_CD_M = If(row.Item(LMI440C.CUST_CD_KBN.CUST_CD_M_COL), "").ToString()
            newRow.ARR_PLAN_DATE_FROM = fromDate
            newRow.ARR_PLAN_DATE_TO = toDate
            newRow.TRANSPORT_DB_NAME = Me.GetTransportDataBaseName()

            createData.LMI440IN.AddLMI440INRow(newRow)
        Next


        Return createData


    End Function



    ''' <summary>
    ''' 輸送データベース名取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransportDataBaseName() As String

        Dim dbName As String = ""
        Dim row As DataRow = Me.GetKbnRow(LMI440C.TRANSPORT_DB_NAME_KBN.GROUP_CD _
                                        , LMI440C.TRANSPORT_DB_NAME_KBN.KBN_CD)

        If (row IsNot Nothing) Then
            dbName = If(row.Item(LMI440C.TRANSPORT_DB_NAME_KBN.DB_NAME_COL), "").ToString()
        End If


        Return dbName


    End Function

#End Region


#Region "Excel作成"



    ''' <summary>
    ''' 保存フォルダ取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSaveFolderPath() As String

        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI440C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI440C.SAVE_FILE_KBN.KBN_CD)

        Dim folderPath As String = String.Empty
        If (saveInfoRow IsNot Nothing) Then

            folderPath = Path.Combine(saveInfoRow.Item(LMI440C.SAVE_FILE_KBN.FOLDER_PATH_COL).ToString() _
                                    , DateTime.Now.ToString("yyyyMMdd"))

            Directory.CreateDirectory(folderPath)
        End If


        Return folderPath

    End Function

    ''' <summary>
    ''' 保存ファイル名生成
    ''' </summary>
    ''' <param name="loadFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSaveFileName(ByVal loadFilePath As String) As String


        Dim saveInfoRow As DataRow = Me.GetKbnRow(LMI440C.SAVE_FILE_KBN.GROUP_CD _
                                                , LMI440C.SAVE_FILE_KBN.KBN_CD)

        Dim fileNamePrefix As String = ""
        If (saveInfoRow IsNot Nothing) Then
            fileNamePrefix = saveInfoRow.Item(LMI440C.SAVE_FILE_KBN.FILE_PREFIX_COL).ToString()
        End If

        Dim loadFileNameWithoutExt As String = Path.GetFileNameWithoutExtension(loadFilePath)
        Dim loadFileExtension As String = Path.GetExtension(loadFilePath)
        Dim fileCreateTime As String = DateTime.Now.ToString("yyyyMMddhhmmssffff")

        ' 接頭文字 + 元のファイル名 + 出力日時 + 拡張子
        Return String.Format("{0}{1}_{2}{3}" _
                            , fileNamePrefix _
                            , loadFileNameWithoutExt _
                            , fileCreateTime _
                            , loadFileExtension)


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


#End Region


#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataSelectedRow(ByVal frm As LMI440F) As DataSet

        Dim selectedRowIdx As Integer = 0

        Dim inData As New LMI440DS
#If False Then
        With frm.sprDetails.ActiveSheet

            For Each checked As String In Me._V.GetCheckList()

                selectedRowIdx = Convert.ToInt32(checked)

                Dim newRow As LMI440DS.LMI440INRow = inData.LMI440IN.NewLMI440INRow

                newRow.NRS_BR_CD = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI440G.sprDetails.NRS_BR_CD.ColNo))
                newRow.INKA_CYL_FILE_NO_L = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI440G.sprDetails.INKA_CYL_FILE_NO_L.ColNo))
                newRow.LAST_UPD_DATE = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI440G.sprDetails.LAST_UPD_DATE.ColNo))
                newRow.LAST_UPD_TIME = Me._LMIconV.GetCellValue(.Cells(selectedRowIdx, LMI440G.sprDetails.LAST_UPD_TIME.ColNo))
                newRow.SPREAD_ROW_NO = (selectedRowIdx + 1).ToString()

                inData.LMI440IN.AddLMI440INRow(newRow)
            Next

        End With
#End If

        Return inData

    End Function

#End Region

#Region "ユーティリティ"



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

        Return Me.GetKbnRows(kbnGroupCd).Where(Function(r) kbnCd.Equals(r.Item(LMI440C.COL_NAME.KBN_CD))).FirstOrDefault()


    End Function


    Private Function GetKbnRows(ByVal kbnGroupCd As String) As IEnumerable(Of DataRow)

        If (String.IsNullOrEmpty(kbnGroupCd)) Then
            Return Enumerable.Empty(Of DataRow)()
        End If

        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable _
              .Where(Function(r) kbnGroupCd.Equals(r.Item(LMI440C.COL_NAME.KBN_GROUP_CD)))


    End Function



    ''' <summary>
    ''' Z_KBNから任意の一行を取得する。
    ''' </summary>
    ''' <param name="kbnGroupCd"></param>
    ''' <param name="kbnNm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKbnRowByKbnNm1(ByVal kbnGroupCd As String _
                                     , ByVal kbnNm As String _
                                     , ByVal kbnNmColName As String) As DataRow

        If (String.IsNullOrEmpty(kbnGroupCd) OrElse String.IsNullOrEmpty(kbnNm)) Then
            Return Nothing
        End If

        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable _
              .Where(Function(r) kbnGroupCd.Equals(r.Item(LMI440C.COL_NAME.KBN_GROUP_CD)) AndAlso _
                                 kbnNm.Equals(r.Item(kbnNmColName))).FirstOrDefault


    End Function

#End Region


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


    Private Function SaveAsEditExcelFile(ByVal readFilePath As String _
                                       , ByVal savefileName As String _
                                       , ByVal saveFolderPath As String _
                                       , ByVal editSheetNo As Integer _
                                       , ByRef editData As Dictionary(Of Point, String)) As Boolean

        Dim isSuccess As Boolean = False


        If (String.IsNullOrEmpty(readFilePath) OrElse _
            IO.File.Exists(readFilePath) = False) Then
            Return isSuccess
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlWkSheet As Excel.Worksheet = Nothing
        Dim xlCells As Excel.Range = Nothing

        Dim editCells As Object = Nothing
        Dim editCellRange As Excel.Range = Nothing

        Try

            Dim saveFilePath As String = Path.Combine(saveFolderPath, savefileName)

            'ファイルの存在確認
            If System.IO.File.Exists(saveFilePath) = True Then

                '存在した場合は、削除して新規作成
                System.IO.File.Delete(saveFilePath)
            End If

            xlApp = New Excel.Application With
                    {.Visible = False,
                     .DisplayAlerts = False}

            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Open(readFilePath)
            xlSheets = xlBook.Sheets

            xlWkSheet = DirectCast(xlSheets(editSheetNo), Excel.Worksheet)
            xlCells = xlWkSheet.Cells

            For Each row As KeyValuePair(Of Point, String) In editData

                If (row.Key.IsEmpty OrElse _
                    row.Key.X = 0 OrElse _
                    row.Key.Y = 0) Then
                    Continue For
                End If

                Try
                    editCells = xlCells(row.Key.X, row.Key.Y)
                    editCellRange = DirectCast(editCells, Excel.Range)
                    editCellRange.Value = row.Value
                Finally
                    Me.ReleaseExcelObject(editCells)
                    Me.ReleaseExcelObject(editCellRange)
                End Try

            Next

            xlBook.SaveAs(saveFilePath, xlBook.FileFormat)
            xlBook.RefreshAll()

            isSuccess = True

        Catch ex As Exception

            Logger.WriteErrorLog(Me.GetType.Name _
                               , MethodBase.GetCurrentMethod.Name _
                               , ex.Message _
                               , ex)
        Finally

            ' 開放処理(ここに不足があるとExcelが残る)
            Me.ReleaseExcelObject(Of Excel.Range)(editCellRange)
            Me.ReleaseExcelObject(editCells)
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

