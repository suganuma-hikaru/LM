' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI900  : ハネウェル管理Excel作成
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
''' LMI900ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI900H
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
        Dim rtnFlg As Boolean = Me.MakeExcel(prmDs)

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

        If ds.Tables(LMI900C.TABLE_NM_IN).Rows.Count = 0 Then
            Return False
        End If

        Dim rtnFlg As Boolean = True
        Dim excelFlg As Boolean = False

        'ファイルパス・ファイル名取得
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'E033' AND KBN_CD = '01' ")
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM1").ToString
        Dim xlsFileName As String = String.Concat(xlsPathKbn(0).Item("KBN_NM2").ToString, _
                                                  MyBase.GetSystemDateTime(0), "_", _
                                                  MyBase.GetSystemDateTime(1).Substring(0, 6), ".xls")

        'ファイルの存在確認
        If System.IO.File.Exists(String.Concat(xlsPath, xlsFileName)) = True Then
            '存在した場合は、削除して新規作成
            System.IO.File.Delete(String.Concat(xlsPath, xlsFileName))
        End If

        '出力する列数を求める
        Dim colMax As Integer = LMI900C.EXCEL_COL

        With ds.Tables(LMI900C.TABLE_NM_IN)

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

            '作業シート設定
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

            '出力セル範囲設定
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

            xlApp.DisplayAlerts = True   'ダイアログの表示設定を元に戻す

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

        '値を設定
        With ds.Tables(LMI900C.TABLE_NM_IN)
            For i As Integer = 0 To rowMax

                '営業所コード
                excelData(i, LMI900C.ExcelCol.NRS_BR_CD) = .Rows(i).Item("NRS_BR_CD").ToString()
                '空・実入り
                excelData(i, LMI900C.ExcelCol.EMPTY_KB) = .Rows(i).Item("EMPTY_KB").ToString()
                'シリンダタイプ
                excelData(i, LMI900C.ExcelCol.CYLINDER_TYPE) = .Rows(i).Item("CYLINDER_TYPE").ToString()
                '出荷元・届先名
                excelData(i, LMI900C.ExcelCol.TOFROM_NM) = .Rows(i).Item("TOFROM_NM").ToString()
                'シリアル№
                excelData(i, LMI900C.ExcelCol.SERIAL_NO) = .Rows(i).Item("SERIAL_NO").ToString()
                '変換後容器
                excelData(i, LMI900C.ExcelCol.ALBAS_CHG) = .Rows(i).Item("ALBAS_CHG").ToString()
                '入出荷日
                excelData(i, LMI900C.ExcelCol.INOUT_DATE) = .Rows(i).Item("INOUT_DATE").ToString()
                '次回定検日
                excelData(i, LMI900C.ExcelCol.NEXT_TEST_DATE) = .Rows(i).Item("NEXT_TEST_DATE").ToString()
                '製造日（点検開始日）
                excelData(i, LMI900C.ExcelCol.PROD_DATE) = .Rows(i).Item("PROD_DATE").ToString()   'ADD 2019/10/29 006786
                '経過日数
                excelData(i, LMI900C.ExcelCol.KEIKA_DATE) = .Rows(i).Item("KEIKA_DATE").ToString()
                '入出在その他区分
                excelData(i, LMI900C.ExcelCol.IOZS_KB) = .Rows(i).Item("IOZS_KB").ToString()
                '荷送人コード
                excelData(i, LMI900C.ExcelCol.SHIP_CD_L) = .Rows(i).Item("SHIP_CD_L").ToString()
                '荷送人名
                excelData(i, LMI900C.ExcelCol.SHIP_NM_L) = .Rows(i).Item("SHIP_NM_L").ToString()
                '買主注文番号(明細単位)
                excelData(i, LMI900C.ExcelCol.BUYER_ORD_NO_DTL) = .Rows(i).Item("BUYER_ORD_NO_DTL").ToString()
#If True Then   'ADD 2019/10/31 008262【LMS】データ抽出_ハネウェル管理_商品コード・商品名を追加出力
                '商品コード
                excelData(i, LMI900C.ExcelCol.GOODS_CD_CUST) = .Rows(i).Item("GOODS_CD_CUST").ToString()
                '商品名
                excelData(i, LMI900C.ExcelCol.GOODS_NM) = .Rows(i).Item("GOODS_NM").ToString()
#End If
                '荷主カテゴリ2
                excelData(i, LMI900C.ExcelCol.SEARCH_KEY_2) = .Rows(i).Item("SEARCH_KEY_2").ToString()  'ADD 2019/12/10 009849

                '備考小(荷主)
                excelData(i, LMI900C.ExcelCol.REMARK_IN) = .Rows(i).Item("REMARK_IN").ToString()

            Next

        End With

        Return excelData

    End Function

#End Region

#Region "DataSet設定"

#End Region

#End Region 'Method

End Class
