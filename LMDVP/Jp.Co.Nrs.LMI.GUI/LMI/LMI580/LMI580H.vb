' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI580H : TSMC請求明細書
'  作  成  者       :  [HORI]
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMI580ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI580
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        Dim ds As DataSet = prm.ParamDataSet.Copy()

        'ファイル作成処理
        Call Me.CreatePrintData(ds)

        'インスタンスの開放
        Call LMFormNavigate.Revoke(Me)

    End Sub

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function CreatePrintData(ByVal ds As DataSet) As Boolean

        '印刷データ検索
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI580BLF", "SelectPrintData", ds)

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        '印刷データの検索結果を編集
        For Each drH As DataRow In rtnDs.Tables("LMI580OUT_2").Rows
            Dim pltCylinder As String = drH.Item("PLT_CYLINDER").ToString()
            Dim asnNo As String = drH.Item("ASN_NO").ToString()

            If String.IsNullOrEmpty(pltCylinder) Then
                'LOT
                drH.Item("LOT_NO") = String.Empty

                'QUANTITY
                drH.Item("QUANTITY") = "1"

            Else
                Dim dtM = rtnDs.Tables("LMI580OUT_3").AsEnumerable().Where(Function(r) r.Item("PLT_NO").ToString = pltCylinder AndAlso r.Item("ASN_NO").ToString = asnNo).ToList()

                If dtM.Count > 0 Then
                    'LOT
                    Dim listLot As New List(Of String)
                    For Each r As DataRow In dtM
                        Dim lotNo As String = r.Item("LOT_NO").ToString()
                        If (Not String.IsNullOrEmpty(lotNo)) AndAlso (Not listLot.Contains(lotNo)) Then
                            listLot.Add(lotNo)
                        End If
                    Next
                    drH.Item("LOT_NO") = String.Join(", ", listLot)

                    'QUANTITY
                    drH.Item("QUANTITY") = If(dtM.Count = 0, 1, dtM.Count).ToString()
                End If
            End If
        Next

        '出力しない不要列を削除
        rtnDs.Tables("LMI580OUT_2").Columns.Remove("ASN_NO")
        rtnDs.Tables("LMI580OUT_3").Columns.Remove("LOT_NO")
        rtnDs.Tables("LMI580OUT_3").Columns.Remove("ASN_NO")

        'Excel出力
        Const xlRight = -4152
        Const xlCenter = -4108

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlRange As Excel.Range = Nothing

        Dim dtH As DataTable = Nothing
        Dim dtD As DataTable = Nothing
        Dim filePath As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim rtnResult As Boolean = True

        Try

            xlApp = New Excel.Application()
            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()
            xlSheets = xlBook.Worksheets

            'Excel非表示
            xlApp.Visible = False

            '==========================================================================
            '   シート1: Billing Statement
            '==========================================================================

            '使用するデータテーブル／シート
            dtH = rtnDs.Tables("LMI580OUT_1")
            dtD = rtnDs.Tables("LMI580OUT_2")
            xlSheet = DirectCast(xlSheets.Item(1), Excel.Worksheet)

            'シート全体の設定
            xlSheet.Name = "Billing Statement"
            xlSheet.Cells.Font.Name = "ＭＳ Ｐゴシック"
            xlSheet.Cells.Font.Size = 11

            '目盛線を非表示
            xlApp.ActiveWindow.DisplayGridlines = False

            With "明細部"

                '明細部(列タイトル)の開始行
                Dim begRowNo As Integer = 7

                'カレント行
                Dim rowNo As Integer = begRowNo

                '列タイトル
                Dim titleValue As String() = New String() {
                    String.Concat("Pallet No./Cylinder No.", vbLf, "/Tank No."),
                    "TSMC Material Number",
                    "Material Name",
                    "LOT",
                    "Quantity",
                    String.Concat("Packaging", vbLf, "HuType"),
                    String.Concat("Round", "/", vbLf, "One way"),
                    "Vendor Code",
                    "Vendor",
                    String.Concat("Inbound Date", vbLf, "入荷日"),
                    String.Concat("Outbound Date", vbLf, "出荷日"),
                    String.Concat("Empty Inbound Date", vbLf, "空入荷日"),
                    String.Concat("Empty Outbound Date", vbLf, "空出荷日"),
                    String.Concat("実際保", vbLf, "管日数"),
                    "標準日数",
                    String.Concat("Extend Days", vbLf, "(超過日数)"),
                    String.Concat("Unit Price", vbLf, "(セット料金)"),
                    String.Concat("Extension Fee Unit price", vbLf, "(延長料金単価)"),
                    "追加料金超過分",
                    String.Concat("Total Price", vbLf, "(合計料金)")
                    }

                xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, titleValue.Count)).Value = titleValue

                '明細行
                For i As Integer = 0 To dtD.Rows.Count - 1
                    rowNo += 1
                    xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, dtD.Columns.Count)).Value = dtD.Rows(i).ItemArray
                Next

                '(空行)
                rowNo += 1

                '合計行
                rowNo += 1
                xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, 2)).MergeCells = True
                xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, 1)).Value = "Total Price"

                xlSheet.Range(xlSheet.Cells(rowNo, 3), xlSheet.Cells(rowNo, 15)).Interior.Color = RGB(128, 128, 128)
                xlSheet.Range(xlSheet.Cells(rowNo, 18), xlSheet.Cells(rowNo, 18)).Interior.Color = RGB(128, 128, 128)

                xlSheet.Range(xlSheet.Cells(rowNo, 16), xlSheet.Cells(rowNo, 16)).Formula = String.Concat("=SUM(P8:P", (rowNo - 1).ToString, ")")
                xlSheet.Range(xlSheet.Cells(rowNo, 17), xlSheet.Cells(rowNo, 17)).Formula = String.Concat("=SUM(Q8:Q", (rowNo - 1).ToString, ")")
                xlSheet.Range(xlSheet.Cells(rowNo, 19), xlSheet.Cells(rowNo, 19)).Formula = String.Concat("=SUM(S8:S", (rowNo - 1).ToString, ")")
                xlSheet.Range(xlSheet.Cells(rowNo, 20), xlSheet.Cells(rowNo, 20)).Formula = String.Concat("=SUM(T8:T", (rowNo - 1).ToString, ")")

                '明細行,合計行のセルスタイル設定
                For i As Integer = 14 To 20
                    xlSheet.Range(xlSheet.Cells(begRowNo + 1, i), xlSheet.Cells(rowNo, i)).NumberFormat = "#,##0"
                Next

                '明細部全体の設定
                xlRange = xlSheet.Range(xlSheet.Cells(begRowNo, 1), xlSheet.Cells(rowNo, titleValue.Count))
                With xlRange
                    .Font.Size = 9
                    .HorizontalAlignment = xlCenter     '中央寄せ
                    .Borders.LineStyle = True           '罫線(格子)描画
                    .ColumnWidth = 200                  '改行防止のため列幅を一旦大きく広げる
                    .EntireColumn.AutoFit()             '列幅自動調整
                End With

                '欄外
                rowNo += 1
                xlRange = xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, 1))
                With xlRange
                    .Font.Size = 9
                    .Value = "（※）Packaging HuType = 1セット料金の貨物単位、個品=1PAL、シリンダー=1CY、ISOタンク=1CTR・1TNK"
                End With

            End With

            With "ヘッダ部"

                '※明細部を元に列幅調整をしたいのでヘッダ部は後

                '(先頭ダミー行)
                xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 1)).RowHeight = 7.5

                '請求先名
                xlRange = xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 1))
                With xlRange
                    .Font.Size = 11
                    .Value = String.Concat(dtH(0).Item("SEIQTO_NM").ToString, "　御中")
                End With

                '営業所名
                xlRange = xlSheet.Range(xlSheet.Cells(3, 20), xlSheet.Cells(3, 20))
                With xlRange
                    .Font.Size = 11
                    .HorizontalAlignment = xlRight
                    .Value = String.Concat(dtH(0).Item("NRS_BR_NM").ToString)
                End With

                '帳票タイトル
                xlRange = xlSheet.Range(xlSheet.Cells(4, 8), xlSheet.Cells(4, 8))
                With xlRange
                    .Font.Size = 20
                    .Font.Bold = True
                    .Font.Underline = True
                    .HorizontalAlignment = xlCenter
                    .Value = "Billing Statement"
                End With

                '請求範囲
                Dim begDate As DateTime = DateTime.ParseExact(dtH(0).Item("INV_DATE_FROM").ToString, "yyyyMMdd", Nothing)
                Dim endDate As DateTime = DateTime.ParseExact(dtH(0).Item("INV_DATE_TO").ToString, "yyyyMMdd", Nothing)

                xlRange = xlSheet.Range(xlSheet.Cells(4, 20), xlSheet.Cells(4, 20))
                With xlRange
                    .Font.Size = 11
                    .HorizontalAlignment = xlRight
                    .Value = String.Concat(begDate.ToString("自 yyyy 年 M 月 d 日"), endDate.ToString("至 yyyy 年 M 月 d 日"))
                End With

            End With

            '==========================================================================
            '   シート2: Billing Statement (Detail)
            '==========================================================================

            '使用するデータテーブル／シート
            dtH = rtnDs.Tables("LMI580OUT_1")
            dtD = rtnDs.Tables("LMI580OUT_3")
            xlSheets.Add(After:=xlSheets(xlSheets.Count))
            xlSheet = DirectCast(xlSheets.Item(2), Excel.Worksheet)

            'シート全体の設定
            xlSheet.Name = "Billing Statement (Detail)"
            xlSheet.Cells.Font.Name = "ＭＳ Ｐゴシック"
            xlSheet.Cells.Font.Size = 11

            '目盛線を非表示
            xlApp.ActiveWindow.DisplayGridlines = False

            With "明細部"

                '明細部(列タイトル)の開始行
                Dim begRowNo As Integer = 7

                'カレント行
                Dim rowNo As Integer = begRowNo

                '列タイトル
                Dim titleValue As String() = New String() {
                    "Pallet No.",
                    "Serial No.",
                    "TSMC Material Number",
                    "Material Name",
                    String.Concat("Packaging Type", vbLf, "(Box)"),
                    String.Concat("Round", "/", vbLf, "One way"),
                    "Vendor Code",
                    "Vendor",
                    String.Concat("Inbound Date", vbLf, "入荷日"),
                    String.Concat("Outbound Date", vbLf, "出荷日"),
                    String.Concat("Empty Inbound Date", vbLf, "空入荷日"),
                    String.Concat("Empty Outbound Date", vbLf, "空出荷日"),
                    String.Concat("実際保", vbLf, "管日数"),
                    "標準日数",
                    String.Concat("Extend Days", vbLf, "(超過日数)")
                    }

                xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, titleValue.Count)).Value = titleValue

                '明細行
                For i As Integer = 0 To dtD.Rows.Count - 1
                    rowNo += 1
                    xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, dtD.Columns.Count)).Value = dtD.Rows(i).ItemArray
                Next

                '明細行のセルスタイル設定
                For i As Integer = 13 To 15
                    xlSheet.Range(xlSheet.Cells(begRowNo + 1, i), xlSheet.Cells(rowNo, i)).NumberFormat = "#,##0"
                Next

                '明細部全体の設定
                xlRange = xlSheet.Range(xlSheet.Cells(begRowNo, 1), xlSheet.Cells(rowNo, titleValue.Count))
                With xlRange
                    .Font.Size = 9
                    .HorizontalAlignment = xlCenter     '中央寄せ
                    .Borders.LineStyle = True           '罫線(格子)描画
                    .ColumnWidth = 200                  '改行防止のため列幅を一旦大きく広げる
                    .EntireColumn.AutoFit()             '列幅自動調整
                End With

            End With

            With "ヘッダ部"

                '※明細部を元に列幅調整をしたいのでヘッダ部は後

                '(先頭ダミー行)
                xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, 1)).RowHeight = 7.5

                '請求先名
                xlRange = xlSheet.Range(xlSheet.Cells(2, 1), xlSheet.Cells(2, 1))
                With xlRange
                    .Font.Size = 11
                    .Value = String.Concat(dtH(0).Item("SEIQTO_NM").ToString, "　御中")
                End With

                '営業所名
                xlRange = xlSheet.Range(xlSheet.Cells(3, 15), xlSheet.Cells(3, 15))
                With xlRange
                    .Font.Size = 11
                    .HorizontalAlignment = xlRight
                    .Value = String.Concat(dtH(0).Item("NRS_BR_NM").ToString)
                End With

                '帳票タイトル
                xlRange = xlSheet.Range(xlSheet.Cells(4, 6), xlSheet.Cells(4, 6))
                With xlRange
                    .Font.Size = 20
                    .Font.Bold = True
                    .Font.Underline = True
                    .HorizontalAlignment = xlCenter
                    .Value = "Billing Statement (Detail)"
                End With

                '請求範囲
                Dim begDate As DateTime = DateTime.ParseExact(dtH(0).Item("INV_DATE_FROM").ToString, "yyyyMMdd", Nothing)
                Dim endDate As DateTime = DateTime.ParseExact(dtH(0).Item("INV_DATE_TO").ToString, "yyyyMMdd", Nothing)

                xlRange = xlSheet.Range(xlSheet.Cells(4, 15), xlSheet.Cells(4, 15))
                With xlRange
                    .Font.Size = 11
                    .HorizontalAlignment = xlRight
                    .Value = String.Concat(begDate.ToString("自 yyyy 年 M 月 d 日"), endDate.ToString("至 yyyy 年 M 月 d 日"))
                End With

            End With

            '==========================================================================
            '   ファイル保存
            '==========================================================================

            '先頭のシートをアクティブ
            DirectCast(xlSheets.Item(1), Excel.Worksheet).Activate()

            'ファイルの出力パスとファイル名を決定
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'E052' AND KBN_CD = '04'")
            If dr.Count = 0 Then
                MyBase.SetMessage("E326", New String() {"請求明細の保存先とファイル名", "区分マスタ(E052)"})
                Return False
            Else
                filePath = dr(0).Item("KBN_NM1").ToString()
                Dim sysData As String() = MyBase.GetSystemDateTime()
                'fileNm = String.Concat(dr(0).Item("KBN_NM2").ToString(), "_", sysData(0), "_", sysData(1), ".xlsx")
                fileNm = String.Concat(dr(0).Item("KBN_NM2").ToString(), ".xlsx")
            End If

            'ファイルを保存
            xlApp.DisplayAlerts = False
            System.IO.Directory.CreateDirectory(filePath)
            xlBook.SaveAs(String.Concat(filePath, fileNm))

        Catch ex As Exception
            MyBase.SetMessage("S002")
            Return False

        Finally
            Call Me._LMIconH.MRComObject(xlRange)
            Call Me._LMIconH.MRComObject(xlSheet)
            Call Me._LMIconH.MRComObject(xlSheets)
            xlBook.Close(False)
            Call Me._LMIconH.MRComObject(xlBook)
            Call Me._LMIconH.MRComObject(xlBooks)
            xlApp.Quit()
            Call Me._LMIconH.MRComObject(xlApp)
        End Try

        '保存したファイルを開く
        rtnResult = rtnResult AndAlso Me._LMIconH.OutFileOpen(New String() {String.Concat(filePath, fileNm)})

        Return rtnResult

    End Function

#End Region

#End Region

End Class
