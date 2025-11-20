' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD810  : 在庫照合結果EXCEL作成
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
''' LMD810ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD810H
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

        'EXCEL出力データ作成処理
        Dim rtnFlg As Boolean = Me.MakeEXCEL(prmDs)

        prm.ReturnFlg = rtnFlg

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
    ''' EXCEL出力データ出力処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function MakeExcel(ByVal ds As DataSet) As Boolean

        If ds.Tables(LMD810C.TABLE_NM_IN).Rows.Count = 0 Then
            Return False
        End If

        Dim rtnFlg As Boolean = True
        Dim excelFlg As Boolean = False

        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E028' AND ", _
                                                                                                             "KBN_NM1 = '", ds.Tables(LMD810C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString, "'"))
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM2").ToString
        Dim xlsFileName As String = String.Concat(xlsPathKbn(0).Item("KBN_NM3").ToString, _
                                                  Mid(MyBase.GetSystemDateTime(0), 3, 6), _
                                                  Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                  ".xls")

        'ファイルの存在確認
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = True Then
            '存在した場合は、削除して新規作成
            System.IO.File.Delete(String.Concat(xlsPath, xlsFileName))
        End If

        '出力する列数を求める
        Dim colMax As Integer = LMD810C.EXCEL_COL
        With ds.Tables(LMD810C.TABLE_NM_IN_FLG).Rows(0)
            'ロット№
            If ("01").Equals(.Item("LOT_NO").ToString) = True Then
                colMax = colMax + 1
            End If
            'シリアル№
            If ("01").Equals(.Item("SERIAL_NO").ToString) = True Then
                colMax = colMax + 1
            End If
            '入目
            If ("01").Equals(.Item("IRIME").ToString) = True Then
                colMax = colMax + 1
            End If
            '入目単位
            If ("01").Equals(.Item("IRIME_UT").ToString) = True Then
                colMax = colMax + 1
            End If
        End With

        With ds.Tables(LMD810C.TABLE_NM_IN)

            'DataSetの値を二次元配列に格納する
            Dim rowMax As Integer = .Rows.Count - 1
            Dim excelData(0, 0) As String
            ReDim excelData(rowMax, colMax)
            excelData = Me.SetExcelData(ds, rowMax, colMax)

            'EXCEL起動
            Dim xlApp As New Excel.Application
            Dim xlBooks As Excel.Workbooks
            Dim xlBook As Excel.Workbook = Nothing
            Dim xlSheet As Excel.Worksheet = Nothing

            Dim startCell As Excel.Range = Nothing
            Dim endCell As Excel.Range = Nothing
            Dim range As Excel.Range = Nothing

            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)
            startCell = DirectCast(xlSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(rowMax + 2, colMax + 1), Excel.Range)

            range = xlSheet.Range(startCell, endCell)
            range.Value = excelData

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

        End With

        Return rtnFlg

    End Function

    ''' <summary>
    ''' Excel出力値の設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetExcelData(ByVal ds As DataSet, ByVal rowMax As Integer, ByVal colMax As Integer) As String(,)

        'DataSetの値を二次元配列に格納する
        Dim excelData(rowMax + 1, colMax + 1) As String
        Dim titleRow As Integer = 0

        Dim colCount As Integer = -1
        Dim goodsNmCol As Integer = -1
        Dim goodsCdCol As Integer = -1
        Dim lotNoCol As Integer = -1
        Dim serialNoCol As Integer = -1
        Dim irimeCol As Integer = -1
        Dim irimeUtCol As Integer = -1
        Dim nrsNbCol As Integer = -1
        Dim custNbCol As Integer = -1
        Dim kbnCol As Integer = -1
        With ds.Tables(LMD810C.TABLE_NM_IN_FLG).Rows(0)
            '品名
            colCount = colCount + 1
            goodsNmCol = colCount
            '品目コード
            colCount = colCount + 1
            goodsCdCol = colCount
            'ロット№
            If ("01").Equals(.Item("LOT_NO").ToString) = True Then
                colCount = colCount + 1
                lotNoCol = colCount
            End If
            'シリアル№
            If ("01").Equals(.Item("SERIAL_NO").ToString) = True Then
                colCount = colCount + 1
                serialNoCol = colCount
            End If
            '入目
            If ("01").Equals(.Item("IRIME").ToString) = True Then
                colCount = colCount + 1
                irimeCol = colCount
            End If
            '入目単位
            If ("01").Equals(.Item("IRIME_UT").ToString) = True Then
                colCount = colCount + 1
                irimeUtCol = colCount
            End If
            'NRS個数
            colCount = colCount + 1
            nrsNbCol = colCount
            '荷主個数
            colCount = colCount + 1
            custNbCol = colCount
            '区分
            colCount = colCount + 1
            kbnCol = colCount
        End With

        'タイトル列を設定
        excelData(titleRow, goodsNmCol) = LMD810C.EXCEL_GOODSNM '品名
        excelData(titleRow, goodsCdCol) = LMD810C.EXCEL_GOODSCD '品目コード
        If lotNoCol <> -1 Then
            excelData(titleRow, lotNoCol) = LMD810C.EXCEL_LOTNO 'ロット№
        End If
        If serialNoCol <> -1 Then
            excelData(titleRow, serialNoCol) = LMD810C.EXCEL_SERIALNO 'シリアル№
        End If
        If irimeCol <> -1 Then
            excelData(titleRow, irimeCol) = LMD810C.EXCEL_IRIME '入目
        End If
        If irimeUtCol <> -1 Then
            excelData(titleRow, irimeUtCol) = LMD810C.EXCEL_IRIMEUT '入目単位
        End If
        excelData(titleRow, nrsNbCol) = LMD810C.EXCEL_NRSNB 'NRS個数
        excelData(titleRow, custNbCol) = LMD810C.EXCEL_CUSTNB '荷主個数
        excelData(titleRow, kbnCol) = LMD810C.EXCEL_KBN '区分

        titleRow = 1

        '値を設定
        With ds.Tables(LMD810C.TABLE_NM_IN)
            For i As Integer = 0 To rowMax
                '品名
                excelData(i + titleRow, goodsNmCol) = .Rows(i).Item("GOODS_NM").ToString
                '品目コード
                excelData(i + titleRow, goodsCdCol) = .Rows(i).Item("CUST_GOODS_CD").ToString
                'ロット№
                If lotNoCol <> -1 Then
                    excelData(i + titleRow, lotNoCol) = .Rows(i).Item("LOT_NO").ToString
                End If
                'シリアル№
                If serialNoCol <> -1 Then
                    excelData(i + titleRow, serialNoCol) = .Rows(i).Item("SERIAL_NO").ToString
                End If
                '入目
                If irimeCol <> -1 Then
                    excelData(i + titleRow, irimeCol) = .Rows(i).Item("IRIME").ToString
                End If
                '入目単位
                If irimeUtCol <> -1 Then
                    excelData(i + titleRow, irimeUtCol) = .Rows(i).Item("IRIME_UT").ToString
                End If
                'NRS個数
                excelData(i + titleRow, nrsNbCol) = .Rows(i).Item("NRS_NB").ToString
                '荷主個数
                excelData(i + titleRow, custNbCol) = .Rows(i).Item("CUST_NB").ToString
                '区分
                If ("01").Equals(.Rows(i).Item("KBN").ToString) = True Then
                    excelData(i + titleRow, kbnCol) = LMD810C.EXCEL_NRSKBN
                Else
                    excelData(i + titleRow, kbnCol) = LMD810C.EXCEL_CUSTKBN
                End If
            Next
        End With

        Return excelData

    End Function

#End Region

#Region "DataSet設定"

#End Region

#End Region 'Method

End Class
