' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI    : データ管理サブ
'  プログラムID     :  LMI500 : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMI500ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI500H
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

        Dim ds As DataSet = Me._LMIconH.SetInData(prm.ParamDataSet, New LMI500DS())

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

        'サーバアクセス
        Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(LMI500C.CLASS_NM, LMIControlC.BLF), LMI500C.ACTION_ID_SELECT_DATA, ds)

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        Dim dt As DataTable = rtnDs.Tables(LMI500C.TABLE_NM_LMI500SET)

        '並び順を変えない処理
        Dim drs As DataRow() = dt.Select("CUST_CD_S = '00' ")

        Dim max As Integer = dt.Rows.Count - 1
        Dim sMax As Integer = drs.Length - 1
        Dim hokokuDate As String = ds.Tables(LMI020C.TABLE_NM_IN).Rows(0).Item("REPORT_DATE").ToString()

        Dim fileNm1 As String = String.Concat(LMI500C.FILE_NM_SK, hokokuDate, ".xls")
        Dim fileNm2 As String = String.Concat(LMI500C.FILE_NM_DK, hokokuDate, ".xls")

        '2012/02/19 コメント化
        '→日次在庫報告用データ出力後、Excelを開かない仕様に変更によりfileNmは未使用になった為
        'Dim fileNm As String() = New String() {Me._LMIconH.GetFileNmAndAccess(fileNm1), Me._LMIconH.GetFileNmAndAccess(fileNm2)}

        'CustCdS = '00'のデータ作成
        Dim rtnResult As Boolean = Me.CreatePrintData(dt, 0, sMax, fileNm1)

        'CustCdS = '01'のデータ作成
        rtnResult = rtnResult AndAlso Me.CreatePrintData(dt, sMax + 1, max, fileNm2)

        '出力ファイルの表示
        '2012/02/19 コメント化
        '→日次在庫報告用データ出力後、Excelを開かない仕様に変更の為
        'rtnResult = rtnResult AndAlso Me._LMIconH.OutFileOpen(fileNm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="rowStart">開始位置</param>
    ''' <param name="rowEnd">終了位置</param>
    ''' <param name="fileName">ファイル名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CreatePrintData(ByVal dt As DataTable _
                                     , ByVal rowStart As Integer _
                                     , ByVal rowEnd As Integer _
                                     , ByVal fileName As String _
                                     ) As Boolean

        '開始位置と終了位置が不整合の場合、スルー
        If rowEnd < rowStart Then
            Return True
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim rtnResult As Boolean = False

        Try

            xlApp = New Excel.Application()
            xlBooks = xlApp.Workbooks

            '新規のファイルを開く場合
            xlBook = xlBooks.Add()
            xlSheets = xlBook.Worksheets
            xlSheet = DirectCast(xlSheets.Item(1), Excel.Worksheet)

            'エクセル非表示
            xlApp.Visible = False

            Dim titleValue As String() = New String() {"商品" _
                                                       , "SOURCD" _
                                                       , "GMC" _
                                                       , "品名" _
                                                       , "入番小" _
                                                       , "LOT" _
                                                       , "シリアル" _
                                                       , "個数" _
                                                       , "個数単位" _
                                                       , "数量" _
                                                       , "数量単位" _
                                                       , "状態" _
                                                       , "FRB" _
                                                       , "初期入庫日" _
                                                       }

            Dim xlRange As Excel.Range = Nothing

            'タイトルの設定
            '荷主コード(小)を考慮
            Dim colNo As Integer = dt.Columns.Count - 1
            xlRange = xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, colNo))
            xlRange = Me.SetStyleText(xlRange)
            xlRange.Value = titleValue
            Me._LMIconH.MRComObject(xlRange)

            'タイトル分プラス
            Dim rowNo As Integer = rowEnd - rowStart + 2

            'スタイル設定
            For i As Integer = 1 To colNo
                Me.SetStyleData(xlSheet.Range(xlSheet.Cells(2, i), xlSheet.Cells(rowNo, i)), i)
            Next

            '取得したデータの反映
            For i As Integer = rowStart To rowEnd

                'タイトル分プラス
                rowNo = i + 2 - rowStart
                xlRange = xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, colNo))
                xlRange.Value = dt.Rows(i).ItemArray
                Me._LMIconH.MRComObject(xlRange)

            Next

            '幅調整
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNo, colNo)).EntireColumn.AutoFit()

            ''取得したデータの反映
            'Dim rowNo As Integer = 0

            'Dim colMax As Integer = colNo - 1
            'For i As Integer = rowStart To rowEnd

            '    'タイトル分プラス
            '    rowNo = i + 2 - rowStart
            '    For j As Integer = 0 To colMax
            '        colNo = j + 1
            '        xlRange = xlSheet.Range(xlSheet.Cells(rowNo, colNo), xlSheet.Cells(rowNo, colNo))
            '        xlRange = Me.SetStyleData(xlRange, colNo)
            '        xlRange.Value = dt.Rows(i).Item(j)
            '        Me._LMIconH.MRComObject(xlRange)

            '    Next

            'Next

            'rtnResult = Me._LMIconH.SaveFlieDataAndPrintAction(xlApp, xlSheet, fileName)
            '保存先のファイルのパス
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E021'"))

            '荷主小コード取得
            Dim custCdS As String = String.Empty
            If (LMI500C.FILE_NM_SK).Equals(Mid(fileName, 1, 2)) = True Then
                custCdS = LMI500C.CUST_CD_S_00
            Else
                custCdS = LMI500C.CUST_CD_S_01
            End If

            Dim filePath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, _
                                                   MyBase.GetPGID, "\", _
                                                   custCdS, "\")

            rtnResult = Me._LMIconH.SaveFlieDataAndPrintAction2(xlApp, xlSheet, filePath, fileName)

        Catch ex As Exception

            MyBase.SetMessage("S002")
            Return False

        Finally

            Call Me._LMIconH.MRComObject(xlSheet)
            Call Me._LMIconH.MRComObject(xlSheets)

            xlBook.Close(False)
            Call Me._LMIconH.MRComObject(xlBook)
            Call Me._LMIconH.MRComObject(xlBooks)
            xlApp.Quit()
            Call Me._LMIconH.MRComObject(xlApp)

        End Try

        Return rtnResult

    End Function

    ''' <summary>
    ''' スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定する範囲</param>
    ''' <param name="colNo">列番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleData(ByVal xlRange As Excel.Range, ByVal colNo As Integer) As Excel.Range

        With xlRange

            Select Case colNo

                Case LMI500C.SheetColumnIndex.ZAI_NB

                    '個数
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleKosu(xlRange)

                Case LMI500C.SheetColumnIndex.ZAI_QT

                    '数量
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleSuryo(xlRange)

                Case Else

                    'その他
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleText(xlRange)

            End Select

        End With

        Return xlRange

    End Function

    ''' <summary>
    ''' 文字スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleText(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormatLocal = "@"
        Return xlRange
    End Function

    ''' <summary>
    ''' 個数スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleKosu(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormat = "#,##0"
        Return xlRange
    End Function

    ''' <summary>
    ''' 数量スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleSuryo(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormatLocal = "#,##0.000"
        Return xlRange
    End Function

#End Region

#End Region

End Class
