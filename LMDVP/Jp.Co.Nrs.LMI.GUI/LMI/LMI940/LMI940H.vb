' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI940  : ロンザExcel作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop

''' <summary>
''' LMI940ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI940H
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

        Dim excelFlg As Boolean = False
        Dim cnt As Integer = 0
        Dim rowNo As Integer = 2
        Dim goodsCdPosition As String = ds.Tables(LMI940C.TABLE_NM_IN).Rows(0).Item("GOODS_CD_POSITION").ToString()
        Dim goodsCd As String = String.Empty
        Dim searchKey2 As String = String.Empty

        'ファイルパス取得
        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'E035' AND KBN_CD = '01' ")
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM1").ToString

        '要望対応:1853 yamanaka 2013.02.14 Start
        'フォルダの存在確認
        If System.IO.Directory.Exists(xlsPath) = False Then
            ds.Tables(LMI940C.TABLE_NM_IN).Rows(0).Item("ERR_FLG") = "1"
            Return False
        End If
        '要望対応:1853 yamanaka 2013.02.14 End

        'ファイル名取得
        Dim xlsDirectory As String() = Directory.GetFiles(xlsPath)

        '要望対応:1853 yamanaka 2013.02.14 Start
        'ファイルの存在確認
        If xlsDirectory.Length = 0 Then
            ds.Tables(LMI940C.TABLE_NM_IN).Rows(0).Item("ERR_FLG") = "2"
            Return False
        ElseIf xlsDirectory.Length > 1 Then
            ds.Tables(LMI940C.TABLE_NM_IN).Rows(0).Item("ERR_FLG") = "3"
            Return False
        End If

        'If xlsDirectory.Length = 0 OrElse xlsDirectory.Length > 1 Then
        '    Return False
        'End If
        '要望対応:1853 yamanaka 2013.02.14 Start

        Dim xlsFileName As String = Path.GetFileName(xlsDirectory(0))

        '要望対応:1853 yamanaka 2013.02.14 Start
        '読み取り専用ファイルか判断
        If (File.GetAttributes(String.Concat(xlsPath, xlsFileName)) And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
            ds.Tables(LMI940C.TABLE_NM_IN).Rows(0).Item("ERR_FLG") = "4"
            Return False
        End If
        '要望対応:1853 yamanaka 2013.02.14 End

        'EXCEL起動
        Dim xlApp As New Excel.Application
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        'エクセルを開く
        xlBook = xlApp.Workbooks.Open(String.Concat(xlsPath, xlsFileName), UpdateLinks:=0)

        '作業シート設定
        xlSheet = DirectCast(xlBook.Sheets(1), Excel.Worksheet)

        Dim colMax As Integer = xlSheet.UsedRange.Columns.Count
        Dim rowMax As Integer = xlSheet.UsedRange.Rows.Count
        Dim colNo As Integer = 0

        For cnt = colMax + 1 To 40
            If String.IsNullOrEmpty(DirectCast(xlSheet.Cells(1, cnt), Excel.Range).Text.ToString()) = True Then
                colNo = cnt
                Exit For
            End If
        Next

        xlSheet.Cells(1, colNo + 1) = "荷主ｶﾃｺﾞﾘ2"

        For rowCnt As Integer = rowNo To rowMax

            cnt = 0

            For wkCnt As Integer = rowCnt To rowMax

                If String.IsNullOrEmpty(xlSheet.Range(goodsCdPosition & wkCnt).Text.ToString()) = False Then
                    rowCnt = wkCnt
                    Exit For
                Else
                    cnt = cnt + 1
                End If

                If cnt >= 50 Then
                    Exit For
                End If
            Next

            If cnt >= 50 Then
                Exit For
            End If

            goodsCd = xlSheet.Range(goodsCdPosition & rowCnt).Text.ToString()
            searchKey2 = Me.SetExcelData(ds, goodsCd)

            Select Case String.IsNullOrEmpty(searchKey2)
                Case False
                    xlSheet.Cells(rowCnt, colNo + 1) = searchKey2
                Case True
                    xlSheet.Cells(rowCnt, colNo + 1) = "商品マスタに存在しません。"
            End Select

        Next

        '保存時の問合せのダイアログを非表示に設定
        xlApp.DisplayAlerts = False

        'エクセルの保存
        xlBook.Save()

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
    ''' Excel出力値の設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetExcelData(ByVal ds As DataSet, ByVal goodsCd As String) As String

        With ds.Tables(LMI940C.TABLE_NM_IN)

            Dim searchKey2 As String = String.Empty

            '商品マスタをキャッシュから取得
            Dim drGoods As DataRow() = Nothing
            Dim goodsDs As MGoodsDS = New MGoodsDS
            Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("NRS_BR_CD") = .Rows(0).Item("NRS_BR_CD").ToString()
            goodsDr.Item("CUST_CD_L") = .Rows(0).Item("CUST_CD_L").ToString()
            goodsDr.Item("CUST_CD_M") = .Rows(0).Item("CUST_CD_M").ToString()
            goodsDr.Item("SYS_DEL_FLG") = "0"
            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
            drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_CUST = '", goodsCd, "'"))

            If drGoods.Count = 0 Then
                Return searchKey2
            End If

            '商品コード
            searchKey2 = drGoods(0).Item("SEARCH_KEY_2").ToString()

            Return searchKey2

        End With

    End Function

#End Region

#Region "DataSet設定"

#End Region

#End Region 'Method

End Class
