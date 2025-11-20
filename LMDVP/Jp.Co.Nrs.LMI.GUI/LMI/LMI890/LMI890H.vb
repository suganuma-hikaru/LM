' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI890  : NRC出荷／回収情報Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop

''' <summary>
''' LMI890ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI890H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

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

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Excel出力データ検索処理
        Dim rtnDs As DataSet = Me.SelectExcel(prmDs)

        'エラー時は終了
        If MyBase.IsMessageExist() = True Then
            prm.ReturnFlg = False
            Exit Sub
        End If

        'Excel出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeExcel(rtnDs, prmDs)

        'エラー時は終了
        If MyBase.IsMessageExist() = True Then
            prm.ReturnFlg = False
            Exit Sub
        End If

        prm.ReturnFlg = True

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' Excel出力データ検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectExcel(ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectExcel")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI890BLF", "SelectExcel", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectExcel")

        Return rtnDs

    End Function

    ''' <summary>
    ''' Excel作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    Private Function MakeExcel(ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean

        Dim max As Integer = 0

        If ds.Tables(LMI890C.TABLE_NM_OUT_SHUKKA).Rows.Count = 0 AndAlso _
            ds.Tables(LMI890C.TABLE_NM_OUT_HENSO).Rows.Count = 0 AndAlso _
            ds.Tables(LMI890C.TABLE_NM_OUT_MIHEN).Rows.Count = 0 Then
            Return False
        End If

        Dim rtnFlg As Boolean = True
        Dim excelFlg As Boolean = False

        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F005' AND ", _
                                                                                                             "KBN_NM1 = '", inDs.Tables(LMI890C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString, "'"))
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM2").ToString
        Dim xlsFileName As String = String.Concat(xlsPathKbn(0).Item("KBN_NM3").ToString, _
                                                  "_", _
                                                  Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                  Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                  ".xls")

        'ファイルの存在確認
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = True Then
            '存在した場合は、削除して新規作成
            System.IO.File.Delete(String.Concat(xlsPath, xlsFileName))
        End If

        'EXCEL起動
        Dim xlApp As New Excel.Application
        Dim xlBooks As Excel.Workbooks
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing
        Dim startCnt As Integer = 0
        Dim endCnt As Integer = 0

        xlBooks = xlApp.Workbooks
        xlBook = xlBooks.Add()

        Dim sheetCnt As Integer = 0
        Dim sheetCntWk As Integer = 0
        Dim maxSheetCnt As Integer = 0
        Dim shou As Integer = 0
        Dim amari As Decimal = 0

        '全シート数を求め、シートを足す
        Dim rowMax As Integer = 0
        For i As Integer = 0 To 2
            If i = 0 Then
                rowMax = ds.Tables(LMI890C.TABLE_NM_OUT_SHUKKA).Rows.Count
            ElseIf i = 1 Then
                rowMax = ds.Tables(LMI890C.TABLE_NM_OUT_HENSO).Rows.Count
            ElseIf i = 2 Then
                rowMax = ds.Tables(LMI890C.TABLE_NM_OUT_MIHEN).Rows.Count
            End If

            sheetCntWk = Convert.ToInt32(Math.Floor(CalcData(rowMax, LMI890C.EXCEL_MAX_ROW)))
            amari = CalcDataMod(rowMax, LMI890C.EXCEL_MAX_ROW)
            If amari > 0 Then
                sheetCntWk = sheetCntWk + 1
            End If
            If sheetCntWk = 0 Then
                'タイトル行だけ出力したいので、+1する
                sheetCnt = sheetCnt + 1
            Else
                sheetCnt = sheetCnt + sheetCntWk
            End If
        Next
        sheetCnt = sheetCnt - 1
        max = xlBook.Worksheets.Count
        For i As Integer = max To sheetCnt
            xlSheet = DirectCast(xlBook.Worksheets.Add, Excel.Worksheet)
        Next

        sheetCnt = 0
        'DataSetの値を二次元配列に格納し、出力する
        '■出荷実績
        Dim rowMaxShukka As Integer = 0
        Dim excelShukkaData(0, 0) As String
        '出力シート数を求める
        rowMaxShukka = ds.Tables(LMI890C.TABLE_NM_OUT_SHUKKA).Rows.Count
        shou = Convert.ToInt32(Math.Floor(CalcData(rowMaxShukka, LMI890C.EXCEL_MAX_ROW)))
        amari = CalcDataMod(rowMaxShukka, LMI890C.EXCEL_MAX_ROW)
        If amari > 0 Then
            shou = shou + 1
        End If
        max = shou - 1
        If max = -1 Then
            'タイトル行だけ出力したいため、maxに0を設定
            max = 0
        End If
        For i As Integer = 0 To max

            'Max行数での改ページのために、StartとEndを求める
            If rowMaxShukka >= LMI890C.EXCEL_MAX_ROW * (i + 1) Then
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = LMI890C.EXCEL_MAX_ROW * (i + 1)
            Else
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = rowMaxShukka
            End If
            ReDim excelShukkaData(endCnt - startCnt, LMI890C.EXCEL_COL_SHUKKA)
            excelShukkaData = Me.SetExcelShukkaData(ds, startCnt, endCnt, LMI890C.EXCEL_COL_SHUKKA)

            'Excelにデータを出力
            sheetCnt = sheetCnt + 1
            xlSheet = DirectCast(xlBook.Worksheets(sheetCnt), Excel.Worksheet)
            If i = 0 Then
                xlSheet.Name = LMI890C.EXCEL_SHUKKA
            Else
                xlSheet.Name = String.Concat(LMI890C.EXCEL_SHUKKA, "-", i)
            End If
            startCell = DirectCast(xlSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(endCnt - startCnt + 1, LMI890C.EXCEL_COL_SHUKKA + 1), Excel.Range)

            range = xlSheet.Range(startCell, endCell)
            range.Value = excelShukkaData

        Next

        '■返送実績
        Dim rowMaxHenso As Integer = 0
        Dim excelHensoData(0, 0) As String
        '出力シート数を求める
        rowMaxHenso = ds.Tables(LMI890C.TABLE_NM_OUT_HENSO).Rows.Count
        shou = Convert.ToInt32(Math.Floor(CalcData(rowMaxHenso, LMI890C.EXCEL_MAX_ROW)))
        amari = CalcDataMod(rowMaxHenso, LMI890C.EXCEL_MAX_ROW)
        If amari > 0 Then
            shou = shou + 1
        End If
        max = shou - 1
        If max = -1 Then
            'タイトル行だけ出力したいため、maxに0を設定
            max = 0
        End If
        For i As Integer = 0 To max

            'Max行数での改ページのために、StartとEndを求める
            If rowMaxHenso >= LMI890C.EXCEL_MAX_ROW * (i + 1) Then
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = LMI890C.EXCEL_MAX_ROW * (i + 1)
            Else
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = rowMaxHenso
            End If
            ReDim excelHensoData(endCnt - startCnt, LMI890C.EXCEL_COL_HENSO)
            excelHensoData = Me.SetExcelHensoData(ds, startCnt, endCnt, LMI890C.EXCEL_COL_HENSO)

            'Excelにデータを出力
            sheetCnt = sheetCnt + 1
            xlSheet = DirectCast(xlBook.Worksheets(sheetCnt), Excel.Worksheet)
            If i = 0 Then
                xlSheet.Name = LMI890C.EXCEL_HENSO
            Else
                xlSheet.Name = String.Concat(LMI890C.EXCEL_HENSO, "-", i)
            End If
            startCell = DirectCast(xlSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(endCnt - startCnt + 1, LMI890C.EXCEL_COL_HENSO + 1), Excel.Range)

            range = xlSheet.Range(startCell, endCell)
            range.Value = excelHensoData

        Next

        '■長期未返却
        Dim rowMaxMihen As Integer = 0
        Dim excelMihenData(0, 0) As String
        '出力シート数を求める
        rowMaxMihen = ds.Tables(LMI890C.TABLE_NM_OUT_MIHEN).Rows.Count
        shou = Convert.ToInt32(Math.Floor(CalcData(rowMaxMihen, LMI890C.EXCEL_MAX_ROW)))
        amari = CalcDataMod(rowMaxMihen, LMI890C.EXCEL_MAX_ROW)
        If amari > 0 Then
            shou = shou + 1
        End If
        max = shou - 1
        If max = -1 Then
            'タイトル行だけ出力したいため、maxに0を設定
            max = 0
        End If
        For i As Integer = 0 To max

            'Max行数での改ページのために、StartとEndを求める
            If rowMaxMihen >= LMI890C.EXCEL_MAX_ROW * (i + 1) Then
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = LMI890C.EXCEL_MAX_ROW * (i + 1)
            Else
                startCnt = LMI890C.EXCEL_MAX_ROW * i
                endCnt = rowMaxMihen
            End If
            ReDim excelMihenData(endCnt - startCnt, LMI890C.EXCEL_COL_MIHEN)
            excelMihenData = Me.SetExcelMihenData(ds, startCnt, endCnt, LMI890C.EXCEL_COL_MIHEN)

            'Excelにデータを出力
            sheetCnt = sheetCnt + 1
            xlSheet = DirectCast(xlBook.Worksheets(sheetCnt), Excel.Worksheet)
            If i = 0 Then
                xlSheet.Name = LMI890C.EXCEL_MIHEN
            Else
                xlSheet.Name = String.Concat(LMI890C.EXCEL_MIHEN, "-", i)
            End If
            startCell = DirectCast(xlSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(endCnt - startCnt + 1, LMI890C.EXCEL_COL_MIHEN + 1), Excel.Range)

            range = xlSheet.Range(startCell, endCell)
            range.Value = excelMihenData

        Next

        xlApp.DisplayAlerts = False '保存時の問合せのダイアログを非表示に設定

        Try
            'ファイル存在確認からここまでの間に、同名ファイルが作成されてるとシステムエラーになってしまうためTry Catch
            '新規保存の場合
            System.IO.Directory.CreateDirectory(xlsPath)
            xlBook.SaveAs(String.Concat(xlsPath, xlsFileName))
        Catch ex As Exception
            rtnFlg = False
        End Try
        xlApp.DisplayAlerts = True      '元に戻す

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing
        xlBook.Close(False) 'Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

        Return rtnFlg

    End Function

    ''' <summary>
    ''' Excel出力値の設定(出荷実績)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetExcelShukkaData(ByVal ds As DataSet, ByVal startCnt As Integer, ByVal endCnt As Integer, ByVal colMax As Integer) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(endCnt - startCnt, colMax + 1) As String
        Dim titleRow As Integer = 0
        Dim rowCnt As Integer = 0

        'タイトル列を設定
        excelData(titleRow, LMI890C.ExcelShukkaCol.CUSTNML) = LMI890C.EXCEL_CUSTNML  '出荷場所
        excelData(titleRow, LMI890C.ExcelShukkaCol.ARRPLANDATE) = LMI890C.EXCEL_ARRPLANDATE  '納入予定日
        excelData(titleRow, LMI890C.ExcelShukkaCol.CUSTORDNO) = LMI890C.EXCEL_CUSTORDNO  'オーダー番号
        excelData(titleRow, LMI890C.ExcelShukkaCol.BUYERORDNO) = LMI890C.EXCEL_BUYERORDNO  '注文番号
        excelData(titleRow, LMI890C.ExcelShukkaCol.GOODSNM) = LMI890C.EXCEL_GOODSNM  '商品
        excelData(titleRow, LMI890C.ExcelShukkaCol.SERIALNO) = LMI890C.EXCEL_SERIALNO  'シリアル№
        excelData(titleRow, LMI890C.ExcelShukkaCol.DESTNM) = LMI890C.EXCEL_DESTNM  '届先名
        excelData(titleRow, LMI890C.ExcelShukkaCol.SHIPNM) = LMI890C.EXCEL_SHIPNM  '販売先
        excelData(titleRow, LMI890C.ExcelShukkaCol.DESTAD) = LMI890C.EXCEL_DESTAD  '届先住所
        excelData(titleRow, LMI890C.ExcelShukkaCol.CUSTCDL) = LMI890C.EXCEL_CUSTCDL  '日陸荷主コード
        excelData(titleRow, LMI890C.ExcelShukkaCol.DESTCD) = LMI890C.EXCEL_DESTCD  '届け先コード
        excelData(titleRow, LMI890C.ExcelShukkaCol.SHIPCDL) = LMI890C.EXCEL_SHIPCDL  '販売先

        If ds.Tables(LMI890C.TABLE_NM_OUT_SHUKKA).Rows.Count = 0 Then
            Return excelData
        End If

        titleRow = 1
        rowCnt = titleRow - 1

        '値を設定
        With ds.Tables(LMI890C.TABLE_NM_OUT_SHUKKA)
            For i As Integer = startCnt To endCnt - 1
                rowCnt = rowCnt + 1
                excelData(rowCnt, LMI890C.ExcelShukkaCol.CUSTNML) = .Rows(i).Item("CUST_NM_L").ToString '出荷場所
                excelData(rowCnt, LMI890C.ExcelShukkaCol.ARRPLANDATE) = .Rows(i).Item("ARR_PLAN_DATE").ToString '納入予定日
                excelData(rowCnt, LMI890C.ExcelShukkaCol.CUSTORDNO) = .Rows(i).Item("CUST_ORD_NO").ToString 'オーダー番号
                excelData(rowCnt, LMI890C.ExcelShukkaCol.BUYERORDNO) = .Rows(i).Item("BUYER_ORD_NO").ToString '注文番号
                excelData(rowCnt, LMI890C.ExcelShukkaCol.GOODSNM) = .Rows(i).Item("GOODS_NM").ToString '商品
                excelData(rowCnt, LMI890C.ExcelShukkaCol.SERIALNO) = .Rows(i).Item("SERIAL_NO").ToString 'シリアル№
                excelData(rowCnt, LMI890C.ExcelShukkaCol.DESTNM) = .Rows(i).Item("DEST_NM").ToString '届先名
                excelData(rowCnt, LMI890C.ExcelShukkaCol.SHIPNM) = .Rows(i).Item("SHIP_NM").ToString '販売先
                excelData(rowCnt, LMI890C.ExcelShukkaCol.DESTAD) = .Rows(i).Item("DEST_AD").ToString '届先住所
                excelData(rowCnt, LMI890C.ExcelShukkaCol.CUSTCDL) = .Rows(i).Item("CUST_CD_L").ToString '日陸荷主コード
                excelData(rowCnt, LMI890C.ExcelShukkaCol.DESTCD) = .Rows(i).Item("DEST_CD").ToString '届け先コード
                excelData(rowCnt, LMI890C.ExcelShukkaCol.SHIPCDL) = .Rows(i).Item("SHIP_CD_L").ToString '販売先
            Next
        End With

        Return excelData

    End Function

    ''' <summary>
    ''' Excel出力値の設定(返送実績)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetExcelHensoData(ByVal ds As DataSet, ByVal startCnt As Integer, ByVal endCnt As Integer, ByVal colMax As Integer) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(endCnt - startCnt, colMax + 1) As String
        Dim titleRow As Integer = 0
        Dim rowCnt As Integer = 0

        'タイトル列を設定
        excelData(titleRow, LMI890C.ExcelHensoCol.CUSTNML) = LMI890C.EXCEL_CUSTNML  '出荷場所
        excelData(titleRow, LMI890C.ExcelHensoCol.ARRPLANDATE) = LMI890C.EXCEL_ARRPLANDATE  '納入予定日
        excelData(titleRow, LMI890C.ExcelHensoCol.CUSTORDNO) = LMI890C.EXCEL_CUSTORDNO  'オーダー番号
        excelData(titleRow, LMI890C.ExcelHensoCol.BUYERORDNO) = LMI890C.EXCEL_BUYERORDNO  '注文番号
        excelData(titleRow, LMI890C.ExcelHensoCol.GOODSNM) = LMI890C.EXCEL_GOODSNM  '商品
        excelData(titleRow, LMI890C.ExcelHensoCol.SERIALNO) = LMI890C.EXCEL_SERIALNO  'シリアル№
        excelData(titleRow, LMI890C.ExcelHensoCol.HOKOKUDATE) = LMI890C.EXCEL_HOKOKUDATE  '返却日
        excelData(titleRow, LMI890C.ExcelHensoCol.TAIRYU) = LMI890C.EXCEL_TAIRYU  '滞留日数
        excelData(titleRow, LMI890C.ExcelHensoCol.DESTNM) = LMI890C.EXCEL_DESTNM  '届先名
        excelData(titleRow, LMI890C.ExcelHensoCol.SHIPNM) = LMI890C.EXCEL_SHIPNM  '販売先
        excelData(titleRow, LMI890C.ExcelHensoCol.DESTAD) = LMI890C.EXCEL_DESTAD  '届先住所
        excelData(titleRow, LMI890C.ExcelHensoCol.CUSTCDL) = LMI890C.EXCEL_CUSTCDL  '日陸荷主コード
        excelData(titleRow, LMI890C.ExcelHensoCol.DESTCD) = LMI890C.EXCEL_DESTCD  '届け先コード
        excelData(titleRow, LMI890C.ExcelHensoCol.SHIPCDL) = LMI890C.EXCEL_SHIPCDL  '販売先

        If ds.Tables(LMI890C.TABLE_NM_OUT_HENSO).Rows.Count = 0 Then
            Return excelData
        End If

        titleRow = 1
        rowCnt = titleRow - 1

        '値を設定
        With ds.Tables(LMI890C.TABLE_NM_OUT_HENSO)
            For i As Integer = startCnt To endCnt - 1
                rowCnt = rowCnt + 1
                excelData(rowCnt, LMI890C.ExcelHensoCol.CUSTNML) = .Rows(i).Item("CUST_NM_L").ToString '出荷場所
                excelData(rowCnt, LMI890C.ExcelHensoCol.ARRPLANDATE) = .Rows(i).Item("ARR_PLAN_DATE").ToString '納入予定日
                excelData(rowCnt, LMI890C.ExcelHensoCol.CUSTORDNO) = .Rows(i).Item("CUST_ORD_NO").ToString 'オーダー番号
                excelData(rowCnt, LMI890C.ExcelHensoCol.BUYERORDNO) = .Rows(i).Item("BUYER_ORD_NO").ToString '注文番号
                excelData(rowCnt, LMI890C.ExcelHensoCol.GOODSNM) = .Rows(i).Item("GOODS_NM").ToString '商品
                excelData(rowCnt, LMI890C.ExcelHensoCol.SERIALNO) = .Rows(i).Item("SERIAL_NO").ToString 'シリアル№
                excelData(rowCnt, LMI890C.ExcelHensoCol.HOKOKUDATE) = .Rows(i).Item("HOKOKU_DATE").ToString '返却日
                excelData(rowCnt, LMI890C.ExcelHensoCol.TAIRYU) = .Rows(i).Item("TAIRYU").ToString '滞留日数
                excelData(rowCnt, LMI890C.ExcelHensoCol.DESTNM) = .Rows(i).Item("DEST_NM").ToString '届先名
                excelData(rowCnt, LMI890C.ExcelHensoCol.SHIPNM) = .Rows(i).Item("SHIP_NM").ToString '販売先
                excelData(rowCnt, LMI890C.ExcelHensoCol.DESTAD) = .Rows(i).Item("DEST_AD").ToString '届先住所
                excelData(rowCnt, LMI890C.ExcelHensoCol.CUSTCDL) = .Rows(i).Item("CUST_CD_L").ToString '日陸荷主コード
                excelData(rowCnt, LMI890C.ExcelHensoCol.DESTCD) = .Rows(i).Item("DEST_CD").ToString '届け先コード
                excelData(rowCnt, LMI890C.ExcelHensoCol.SHIPCDL) = .Rows(i).Item("SHIP_CD_L").ToString '販売先
            Next
        End With

        Return excelData

    End Function

    ''' <summary>
    ''' Excel出力値の設定(長期未返却)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetExcelMihenData(ByVal ds As DataSet, ByVal startCnt As Integer, ByVal endCnt As Integer, ByVal colMax As Integer) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(endCnt - startCnt, colMax + 1) As String
        Dim titleRow As Integer = 0
        Dim rowCnt As Integer = 0

        'タイトル列を設定
        excelData(titleRow, LMI890C.ExcelMihenCol.CUSTNML) = LMI890C.EXCEL_CUSTNML  '出荷場所
        excelData(titleRow, LMI890C.ExcelMihenCol.ARRPLANDATE) = LMI890C.EXCEL_ARRPLANDATE  '納入予定日
        excelData(titleRow, LMI890C.ExcelMihenCol.TAIRYU) = LMI890C.EXCEL_TAIRYU  '滞留日数
        excelData(titleRow, LMI890C.ExcelMihenCol.CUSTORDNO) = LMI890C.EXCEL_CUSTORDNO  'オーダー番号
        excelData(titleRow, LMI890C.ExcelMihenCol.BUYERORDNO) = LMI890C.EXCEL_BUYERORDNO  '注文番号
        excelData(titleRow, LMI890C.ExcelMihenCol.GOODSNM) = LMI890C.EXCEL_GOODSNM  '商品
        excelData(titleRow, LMI890C.ExcelMihenCol.SERIALNO) = LMI890C.EXCEL_SERIALNO  'シリアル№
        excelData(titleRow, LMI890C.ExcelMihenCol.DESTNM) = LMI890C.EXCEL_DESTNM  '届先名
        excelData(titleRow, LMI890C.ExcelMihenCol.SHIPNM) = LMI890C.EXCEL_SHIPNM  '販売先
        excelData(titleRow, LMI890C.ExcelMihenCol.DESTAD) = LMI890C.EXCEL_DESTAD  '届先住所
        excelData(titleRow, LMI890C.ExcelMihenCol.CUSTCDL) = LMI890C.EXCEL_CUSTCDL  '日陸荷主コード
        excelData(titleRow, LMI890C.ExcelMihenCol.DESTCD) = LMI890C.EXCEL_DESTCD  '届け先コード
        excelData(titleRow, LMI890C.ExcelMihenCol.SHIPCDL) = LMI890C.EXCEL_SHIPCDL  '販売先

        If ds.Tables(LMI890C.TABLE_NM_OUT_MIHEN).Rows.Count = 0 Then
            Return excelData
        End If

        titleRow = 1
        rowCnt = titleRow - 1

        '値を設定
        With ds.Tables(LMI890C.TABLE_NM_OUT_MIHEN)
            For i As Integer = startCnt To endCnt - 1
                rowCnt = rowCnt + 1
                excelData(rowCnt, LMI890C.ExcelMihenCol.CUSTNML) = .Rows(i).Item("CUST_NM_L").ToString '出荷場所
                excelData(rowCnt, LMI890C.ExcelMihenCol.ARRPLANDATE) = .Rows(i).Item("ARR_PLAN_DATE").ToString '納入予定日
                excelData(rowCnt, LMI890C.ExcelMihenCol.TAIRYU) = .Rows(i).Item("TAIRYU").ToString '滞留日数
                excelData(rowCnt, LMI890C.ExcelMihenCol.CUSTORDNO) = .Rows(i).Item("CUST_ORD_NO").ToString 'オーダー番号
                excelData(rowCnt, LMI890C.ExcelMihenCol.BUYERORDNO) = .Rows(i).Item("BUYER_ORD_NO").ToString '注文番号
                excelData(rowCnt, LMI890C.ExcelMihenCol.GOODSNM) = .Rows(i).Item("GOODS_NM").ToString '商品
                excelData(rowCnt, LMI890C.ExcelMihenCol.SERIALNO) = .Rows(i).Item("SERIAL_NO").ToString 'シリアル№
                excelData(rowCnt, LMI890C.ExcelMihenCol.DESTNM) = .Rows(i).Item("DEST_NM").ToString '届先名
                excelData(rowCnt, LMI890C.ExcelMihenCol.SHIPNM) = .Rows(i).Item("SHIP_NM").ToString '販売先
                excelData(rowCnt, LMI890C.ExcelMihenCol.DESTAD) = .Rows(i).Item("DEST_AD").ToString '届先住所
                excelData(rowCnt, LMI890C.ExcelMihenCol.CUSTCDL) = .Rows(i).Item("CUST_CD_L").ToString '日陸荷主コード
                excelData(rowCnt, LMI890C.ExcelMihenCol.DESTCD) = .Rows(i).Item("DEST_CD").ToString '届け先コード
                excelData(rowCnt, LMI890C.ExcelMihenCol.SHIPCDL) = .Rows(i).Item("SHIP_CD_L").ToString '販売先
            Next
        End With

        Return excelData

    End Function

#End Region

#Region "DataSet設定"

#End Region

    ''' <summary>
    ''' ゼロ割回避処理
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcData(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 / value2

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(あまり値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcDataMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "後埋め設定"

    ''' <summary>
    ''' 後埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">後埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>後埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function AtoCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value, value2)
        Next

        Return value

    End Function

#End Region

#Region "文字分割"

    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function stringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

        Dim newCnt As Integer = inCnt - 1
        Dim newByte As Integer = inByte - 1
        Dim oldStr(newCnt) As String
        Dim newStr(newCnt) As String
        Dim byteCnt As Integer = 1

        For i As Integer = 0 To newCnt
            For j As Integer = 0 To newByte
                oldStr(i) = String.Concat(oldStr(i), Mid(inStr, byteCnt, 1))
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(oldStr(i)) <= newByte + 1 Then
                    newStr(i) = oldStr(i)
                    byteCnt = byteCnt + 1
                Else
                    Exit For
                End If
            Next
        Next

        Return newStr

    End Function

#End Region

#End Region 'Method

End Class
