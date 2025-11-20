' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI    : データ管理サブ
'  プログラムID     :  LMI501 : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMI501ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI501
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

        Dim ds As DataSet = Me._LMIconH.SetInData(prm.ParamDataSet, New LMI501DS())

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
        Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(LMI500C.CLASS_NM, LMIControlC.BLF), LMI501C.ACTION_ID_SELECT_DATA, ds)

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        Dim dt As DataTable = rtnDs.Tables(LMI501C.TABLE_NM_LMI501SET)

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim fileFullPath As String = String.Empty

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

            Dim titleValue As String() = New String() {"倉庫コード" _
                                                       , "倉庫名" _
                                                       , "請求先コード" _
                                                       , "請求先名" _
                                                       , "荷主コード（大）" _
                                                       , "荷主コード（中）" _
                                                       , "荷主コード（小）" _
                                                       , "荷主コード（極小）" _
                                                       , "荷主名（大）" _
                                                       , "荷主名（中）" _
                                                       , "荷主名（小）" _
                                                       , "荷主名（極小）" _
                                                       , "荷主カテゴリ１" _
                                                       , "荷主カテゴリ２" _
                                                       , "荷主勘定科目１" _
                                                       , "荷主勘定科目２" _
                                                       , "商品コード" _
                                                       , "商品名" _
                                                       , "ロット" _
                                                       , "シリアル" _
                                                       , "入庫日" _
                                                       , "商品状態１" _
                                                       , "商品状態２" _
                                                       , "商品状態３" _
                                                       , "商品状態３名称" _
                                                       , "薄外品" _
                                                       , "保留品" _
                                                       , "税" _
                                                       , "消防" _
                                                       , "消防種別" _
                                                       , "入番(小)" _
                                                       , "個数単位" _
                                                       , "入目" _
                                                       , "入目単位" _
                                                       , "在庫個数" _
                                                       , "在庫数量" _
                                                       , "包装個数" _
                                                       , "包装単位" _
                                                       , "コメント" _
                                                       }

            Dim xlRange As Excel.Range = Nothing

            'タイトルの設定
            Dim colNo As Integer = dt.Columns.Count
            xlRange = xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(1, colNo))
            xlRange = Me.SetStyleText(xlRange)
            xlRange.Value = titleValue
            Me._LMIconH.MRComObject(xlRange)

            Dim rowNo As Integer = dt.Rows.Count + 1
            Dim rowMax As Integer = rowNo - 2

            'スタイル設定
            For i As Integer = 1 To colNo
                Me.SetStyleData(xlSheet.Range(xlSheet.Cells(2, i), xlSheet.Cells(rowNo, i)), i)
            Next

            '取得したデータの反映
            For i As Integer = 0 To rowMax

                'タイトル分プラス
                rowNo = i + 2
                xlRange = xlSheet.Range(xlSheet.Cells(rowNo, 1), xlSheet.Cells(rowNo, colNo))
                xlRange.Value = dt.Rows(i).ItemArray
                Me._LMIconH.MRComObject(xlRange)

            Next

            '幅調整
            xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNo, colNo)).EntireColumn.AutoFit()

            Dim fileNm As String = "Book"
            'Dim filePath As String = Me._LMIconH.GetFileNmAndAccess(fileNm)
            Dim cnt As Integer = 0
            Dim file As FileInfo = Nothing

            '保存先のファイルのパス
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E021'"))

            '荷主小コード取得
            Dim custCdS As String = String.Empty
            custCdS = dt.Rows(0).Item("CUST_CD_S").ToString

            Dim filePath As String = String.Concat(kbnDr(0).Item("KBN_NM1").ToString, _
                                                   MyBase.GetPGID, "\", _
                                                   custCdS, "\")

            '無限ループ
            While (0 = 0)

                cnt += 1
                fileFullPath = String.Concat(filePath, cnt.ToString(), ".xls")
                file = New FileInfo(fileFullPath)

                '存在しないファイル名の場合、ループを抜ける
                If file.Exists = False Then
                    fileNm = String.Concat(fileNm, cnt.ToString(), ".xls")
                    Exit While
                End If

            End While

            'ファイル作成処理
            'rtnResult = Me._LMIconH.SaveFlieDataAndPrintAction(xlApp, xlSheet, fileNm)

            rtnResult = Me._LMIconH.SaveFlieDataAndPrintAction2(xlApp, xlSheet, filePath, fileNm)

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

        '作成ファイルの出力
        rtnResult = rtnResult AndAlso Me._LMIconH.OutFileOpen(New String() {fileFullPath})

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

                Case LMI501C.SheetColumnIndex.IRIME

                    '入目
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleNumDec(xlRange)

                Case LMI501C.SheetColumnIndex.ZAI_NB

                    '在庫個数
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleNum(xlRange)

                Case LMI501C.SheetColumnIndex.ZAI_QT

                    '在庫数量
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleNumDec(xlRange)

                Case LMI501C.SheetColumnIndex.PKG_NB

                    '梱包個数
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleNum(xlRange)

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
    ''' 数値スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleNum(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormat = "#,##0"
        Return xlRange
    End Function

    ''' <summary>
    ''' 数値スタイル(小数有)設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleNumDec(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormatLocal = "#,##0.000"
        Return xlRange
    End Function

#End Region

#End Region

End Class
