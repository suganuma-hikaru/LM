' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI480H : 古河請求(ディック)
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
''' LMI480ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI480H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI480V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI480G

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
        Dim frm As LMI480F = New LMI480F(Me)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gクラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI480G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI480V(Me, frm, Me._LMIconV, Me._LMIconG)

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
        MyBase.ShowMessage(frm, "G007")

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

#Region "作成処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function MakeExcel(ByVal frm As LMI480F) As Boolean

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI480C.EventShubetsu.SAKUSEI) = False Then

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

        'データセット
        Dim rtDs As DataSet = Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = Nothing
        rtnDs = MyBase.CallWSA(blf, LMI480C.SELECT_DATA, rtDs)

        'データ有無の判定
        Dim noData As Boolean = False
        Dim SelectKb As String = frm.cmbSelectKb.SelectedValue.ToString
        Select Case SelectKb

            Case LMI480C.SEIKYU_DICG
                'DICG関係請求
                If rtnDs.Tables(LMI480C.TABLE_NM_OUT_0101).Rows.Count = 0 AndAlso _
                   rtnDs.Tables(LMI480C.TABLE_NM_OUT_0102).Rows.Count = 0 AndAlso _
                   rtnDs.Tables(LMI480C.TABLE_NM_OUT_0103).Rows.Count = 0 AndAlso _
                   rtnDs.Tables(LMI480C.TABLE_NM_OUT_0104).Rows.Count = 0 Then

                    noData = True

                End If

        End Select

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
        Call Me.ExcelFileCheck(SelectKb, xlsPath, xlsFileName, xlsSavePath)
        'メッセージ判定
        If IsMessageExist() = True Then
            'メッセージ表示
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me._LMIconH.EndAction(frm)

            Return False

        End If

        'EXCEL出力処理
        Select Case SelectKb

            Case LMI480C.SEIKYU_DICG
                'DICG関係請求
                Call Me.MakeExcel01(rtnDs, SelectKb, xlsPath, xlsFileName, xlsSavePath)

        End Select

#If True Then       'ADD 2019/05/27 005720【LMS】特定荷主機能_古河HBFN請求(4/10,22,25日分別してデータ反映)の修正依頼(古河佐藤所長)→自動化
        '①栃木地区最低保証：LMI593
        '②神奈川配送分横持：LMI594
        '③神奈川地区(横浜市･川崎市)固定車：LMI595

        'データセット
        '①栃木地区最低保証：LMI593
        rtnDs = Me.SetDataSetInDataRpt(rtnDs, frm, "C7")
        rtnDs = MyBase.CallWSA(blf, LMI480C.PRINT_DoPrint, rtnDs)

        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        'データセット
        '②神奈川配送分横持：LMI594
        rtDs = Me.SetDataSetInDataRpt(rtnDs, frm, "C8")
        rtnDs = MyBase.CallWSA(blf, LMI480C.PRINT_DoPrint, rtnDs)

        prevDt = rtnDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        'データセット
        '③神奈川地区(横浜市･川崎市)固定車：LMI595
        rtDs = Me.SetDataSetInDataRpt(rtnDs, frm, "C9")
        rtnDs = MyBase.CallWSA(blf, LMI480C.PRINT_DoPrint, rtnDs)

        prevDt = rtnDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If
#End If



        '処理終了アクション
        Call Me._LMIconH.EndAction(frm)

        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"請求EXCEL作成", ""})

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
    Friend Sub FunctionKey12Press(ByRef frm As LMI480F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI480F, ByVal e As FormClosingEventArgs)

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
    Friend Sub btnMake(ByRef frm As LMI480F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '実行処理の呼び出し
        Call Me.MakeExcel(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "データセット"
    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMI480F) As DataSet

        Dim ds As DataSet = New LMI480DS
        Dim dr As DataRow = ds.Tables(LMI480C.TABLE_NM_IN).NewRow()

        With frm

            dr.Item("NRS_BR_CD") = Base.LMUserInfoManager.GetNrsBrCd
            dr.Item("SELECT_KB") = .cmbSelectKb.SelectedValue.ToString

            Dim KikanDateYM As String = Left(DateFormatUtility.DeleteSlash(.imdKikanYM.TextValue), 6)
            dr.Item("KIKAN_F_DATE") = String.Concat(KikanDateYM, "01")
            'dr.Item("KIKAN_T_DATE") = String.Concat(KikanDateYM, "31")

            Dim outkaDate As String = MyBase.GetSystemDateTime(0)
            outkaDate = DateAdd("m", 1, Mid(KikanDateYM, 1, 4) + "/" + Mid(KikanDateYM, 5, 2) + "/01").ToString("yyyy/MM/dd")
            outkaDate = DateAdd("d", -1, outkaDate).ToString("yyyyMMdd")
            dr.Item("KIKAN_T_DATE") = outkaDate.ToString


            ds.Tables(LMI480C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

#If True Then   '2019/05/27 005720【LMS】特定荷主機能_古河HBFN請求(4/10,22,25日分別してデータ反映)の修正依頼(古河佐藤所長)→自動化
    Private Function SetDataSetInDataRpt(ByVal ds As DataSet, ByVal frm As LMI480F, ByVal PTN_ID As String) As DataSet

        'Dim ds As DataSet = New LMI480DS
        ds.Tables(LMI480C.TABLE_NM_IN).Clear()
        Dim dr As DataRow = ds.Tables(LMI480C.TABLE_NM_IN).NewRow()

        With frm

            dr.Item("NRS_BR_CD") = Base.LMUserInfoManager.GetNrsBrCd
            dr.Item("SELECT_KB") = .cmbSelectKb.SelectedValue.ToString

            Dim KikanDateYM As String = Left(DateFormatUtility.DeleteSlash(.imdKikanYM.TextValue), 6)
            dr.Item("KIKAN_F_DATE") = String.Concat(KikanDateYM, "01")
            dr.Item("KIKAN_T_DATE") = String.Concat(KikanDateYM, "31")


            If String.Empty.Equals(PTN_ID) = False Then
                dr.Item("PTN_ID") = PTN_ID.ToString.Trim
            End If

            ds.Tables(LMI480C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function
#End If

#End Region

    ''' <summary>
    ''' EXCELファイルチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExcelFileCheck(ByVal selectKB As String, ByRef xlsPath As String, ByRef xlsFileName As String, ByRef xlsSavePath As String)

        'ファイルパス取得
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S113' AND ", _
                                                                                                             "KBN_CD = '", selectKB, "'"))
        xlsPath = xlsPathKbn(0).Item("KBN_NM2").ToString
        xlsFileName = xlsPathKbn(0).Item("KBN_NM3").ToString
        xlsSavePath = xlsPathKbn(0).Item("KBN_NM4").ToString

        'フォルダの存在確認
        If System.IO.Directory.Exists(xlsPath) = False Then
            MyBase.SetMessage("E296", New String() {"EXCELテンプレートフォルダ"})
            Return
        End If

        'ファイルの存在確認
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = False Then
            MyBase.SetMessage("E492", New String() {"EXCELテンプレートフォルダ", "ファイル", ""})
            Return
        End If

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
    Private Function MakeExcel01(ByVal ds As DataSet, ByVal selectKB As String, ByVal xlsPath As String, ByVal xlsFileName As String, ByRef xlsSavePath As String) As Boolean

        'EXCEL起動
        Dim xlApp As New Excel.Application
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        'エクセルを開く
        xlBook = xlApp.Workbooks.Open(String.Concat(xlsPath, xlsFileName), UpdateLinks:=0)

        'シート1設定(神奈川配送分横持)
        Dim sheetName As String = "神奈川配送分横持"
        xlSheet = DirectCast(xlBook.Sheets(getSheetIndex(sheetName, xlBook.Sheets)), Excel.Worksheet)

        Dim rowMax As Integer = ds.Tables("LMI480OUT_0101").Rows.Count - 1
        Dim rowCnt As Integer = 17
        Dim dataRow As DataRow = Nothing

        For cnt As Integer = 0 To rowMax

            dataRow = ds.Tables("LMI480OUT_0101").Rows(cnt)
            xlSheet.Cells(rowCnt, 1) = dataRow("ARR_PLAN_DATE").ToString.Substring(4, 2)
            xlSheet.Cells(rowCnt, 2) = dataRow("ARR_PLAN_DATE").ToString.Substring(6, 2)
            xlSheet.Cells(rowCnt, 3) = "古河→海老名（" & dataRow("BIN").ToString & "）"
            xlSheet.Cells(rowCnt, 4) = dataRow("SUM_WT")
            xlSheet.Cells(rowCnt, 5) = dataRow("WT_TANI")
            xlSheet.Cells(rowCnt, 6) = dataRow("TANKA")
            xlSheet.Cells(rowCnt, 7) = dataRow("CHOKA")
            xlSheet.Cells(rowCnt, 9) = dataRow("KOSOKU_TSUKORYO")

            rowCnt = rowCnt + 1

        Next
#If False Then  'UPD 2019/05/28 MakeExcel01


        'シート2設定(神奈川配送分横持(聖亘提出用))
        sheetName = "神奈川配送分横持(聖亘提出用)"
        xlSheet = DirectCast(xlBook.Sheets(getSheetIndex(sheetName, xlBook.Sheets)), Excel.Worksheet)

        rowMax = ds.Tables("LMI480OUT_0102").Rows.Count - 1
        rowCnt = 5
        dataRow = Nothing

        For cnt As Integer = 0 To rowMax

            dataRow = ds.Tables("LMI480OUT_0102").Rows(cnt)
            xlSheet.Cells(rowCnt, 1) = dataRow("ARR_PLAN_DATE").ToString.Substring(4, 2)
            xlSheet.Cells(rowCnt, 2) = dataRow("ARR_PLAN_DATE").ToString.Substring(6, 2)
            xlSheet.Cells(rowCnt, 3) = "古河→海老名（" & dataRow("BIN").ToString & "）"
            xlSheet.Cells(rowCnt, 4) = dataRow("SUM_WT")
            xlSheet.Cells(rowCnt, 5) = dataRow("WT_TANI")

            rowCnt = rowCnt + 1

        Next

        'シート3設定(神奈川地区固定車)
        sheetName = "神奈川地区固定車"
        xlSheet = DirectCast(xlBook.Sheets(getSheetIndex(sheetName, xlBook.Sheets)), Excel.Worksheet)

        rowMax = ds.Tables("LMI480OUT_0103").Rows.Count - 1
        rowCnt = 17
        dataRow = Nothing

        For cnt As Integer = 0 To rowMax

            dataRow = ds.Tables("LMI480OUT_0103").Rows(cnt)
            xlSheet.Cells(rowCnt, 1) = dataRow("ARR_PLAN_DATE").ToString.Substring(4, 2)
            xlSheet.Cells(rowCnt, 2) = dataRow("ARR_PLAN_DATE").ToString.Substring(6, 2)
            xlSheet.Cells(rowCnt, 3) = "横浜・川崎市" & dataRow("BIN").ToString & "（２ｔ車）"
            xlSheet.Cells(rowCnt, 4) = dataRow("NUM")
            xlSheet.Cells(rowCnt, 5) = dataRow("TANI")
            xlSheet.Cells(rowCnt, 6) = dataRow("TANKA")
            xlSheet.Cells(rowCnt, 7) = dataRow("TOTAL")

            rowCnt = rowCnt + 1

        Next

        'シート4設定(栃木地区最低保証)
        sheetName = "栃木地区最低保証"
        xlSheet = DirectCast(xlBook.Sheets(getSheetIndex(sheetName, xlBook.Sheets)), Excel.Worksheet)

        rowMax = ds.Tables("LMI480OUT_0104").Rows.Count - 1
        rowCnt = 2
        dataRow = Nothing

        For cnt As Integer = 0 To rowMax

            dataRow = ds.Tables("LMI480OUT_0104").Rows(cnt)
            xlSheet.Cells(rowCnt, 1) = dataRow("OUTKA_PLAN_DATE").ToString
            xlSheet.Cells(rowCnt, 2) = dataRow("ARR_PLAN_DATE").ToString
            xlSheet.Cells(rowCnt, 3) = dataRow("DEST_NM")
            xlSheet.Cells(rowCnt, 4) = dataRow("DEST_AD_1")
            xlSheet.Cells(rowCnt, 5) = dataRow("UNSO_PKG_NB")
            xlSheet.Cells(rowCnt, 6) = dataRow("SEIQ_WT")
            xlSheet.Cells(rowCnt, 7) = dataRow("SEIQ_KYORI")
            xlSheet.Cells(rowCnt, 8) = dataRow("MINIMUM")
            xlSheet.Cells(rowCnt, 9) = dataRow("DECI_UNCHIN")
            xlSheet.Cells(rowCnt, 10) = dataRow("HOTENGAKU")
            xlSheet.Cells(rowCnt, 11) = dataRow("CUST_NM_M")

            rowCnt = rowCnt + 1

        Next
#End If
        '集計行の設定
        xlSheet.Cells(rowCnt, 10) = "=SUM(J2:J" & rowCnt - 1 & ")"

        '保存時の問合せのダイアログを非表示に設定
        xlApp.DisplayAlerts = False

        'エクセルの保存
        'xlBook.Save()
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