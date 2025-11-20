' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF    : 運送
'  プログラムID     :  LMF850 : 名鉄CSV出力
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO

''' <summary>
''' LMF850ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF850H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ' ''' <summary>
    ' ''' Validate共通クラスを格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LMCconV As LMCControlV

    ' ''' <summary>
    ' ''' Handler共通クラスを格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LMCconH As LMCControlH

    ' ''' <summary>
    ' ''' G共通クラスを格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region 'Field

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'CSV出力処理
        Me.MakeCSV(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim setDs As DataSet = prmDs.Copy()
        Dim setOutDt As DataTable = setDs.Tables("LMF850OUT")
        setOutDt.Clear()

        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMF850OUT").Select(Nothing, "UNSO_NO_L,UNSO_NO_M")

        'まとめ処理、編集処理
        Dim kosu As Decimal = 0
        Dim cutStr() As String
        Dim max As Integer = outDr.Length - 1
        For i As Integer = 0 To max
            '別インスタンスのデータロウを空にする
            kosu = Convert.ToDecimal(outDr(i).Item("KOSU").ToString)

            '発荷主名2から発荷主名1と同じ部分を取り除く
            If String.IsNullOrEmpty(outDr(i).Item("NIOKURININ_MEI1").ToString) = False Then
                '置換対象文字(NIOKURININ_MEI1)が空の場合エラーになるため
                outDr(i).Item("NIOKURININ_MEI2") = outDr(i).Item("NIOKURININ_MEI2").ToString().Replace(outDr(i).Item("NIOKURININ_MEI1").ToString(), "").Trim()
            End If

            '発荷主名1の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_MEI1").ToString, 30, 1)
            outDr(i).Item("NIOKURININ_MEI1") = cutStr(0).Trim

            '発荷主名2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_MEI2").ToString, 30, 1)
            outDr(i).Item("NIOKURININ_MEI2") = cutStr(0).Trim

            '発荷主住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD1").ToString, 30, 1)
            outDr(i).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '発荷主住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD2").ToString, 30, 1)
            outDr(i).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '発荷主住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD3").ToString, 30, 1)
            outDr(i).Item("NIOKURININ_ADD3") = cutStr(0).Trim

            '発荷主郵便番号からハイフンを取り除く(念のため、半角と全角の両方を考慮)
            outDr(i).Item("NIOKURININ_ZIP") = outDr(i).Item("NIOKURININ_ZIP").ToString().Replace("-", "")
            outDr(i).Item("NIOKURININ_ZIP") = outDr(i).Item("NIOKURININ_ZIP").ToString().Replace("‐", "")

            '出荷先郵便番号からハイフンを取り除く(念のため、半角と全角の両方を考慮)
            outDr(i).Item("NIUKENIN_ZIP") = outDr(i).Item("NIUKENIN_ZIP").ToString().Replace("-", "")
            outDr(i).Item("NIUKENIN_ZIP") = outDr(i).Item("NIUKENIN_ZIP").ToString().Replace("‐", "")

            '出荷先名1、2の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_NM1").ToString, 30, 2)
            outDr(i).Item("NIUKENIN_NM1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_NM2") = cutStr(1).Trim

            '出荷先住所1、2、3の分割処理
            ReDim cutStr(2)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_ADD1").ToString, 30, 3)
            outDr(i).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            outDr(i).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の再設定
            If String.IsNullOrEmpty(outDr(i).Item("SHIP_NM_L").ToString) = False Then
                outDr(i).Item("KIJI_1") = String.Concat(outDr(i).Item("SHIP_NM_L").ToString, "様扱い")
            ElseIf String.IsNullOrEmpty(outDr(i).Item("DENPYO_NM").ToString) = False Then
                outDr(i).Item("KIJI_1") = String.Concat(outDr(i).Item("DENPYO_NM").ToString, "様扱い")
            Else
                outDr(i).Item("KIJI_1") = String.Concat(outDr(i).Item("CUST_NM_L").ToString, "様扱い")
            End If
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_1").ToString, 40, 1)
            outDr(i).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_2").ToString, 40, 1)
            outDr(i).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_3").ToString, 40, 1)
            outDr(i).Item("KIJI_3") = cutStr(0).Trim

            '記事4、5、6の分割処理
            ReDim cutStr(2)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_4").ToString, 30, 3)
            outDr(i).Item("KIJI_4") = cutStr(0).Trim
            outDr(i).Item("KIJI_5") = cutStr(1).Trim
            outDr(i).Item("KIJI_6") = cutStr(2).Trim

            '納期詳細の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("REMARK_NOUKI").ToString, 20, 1)
            outDr(i).Item("REMARK_NOUKI") = cutStr(0).Trim

            If i = 0 Then
                '1件目
                setOutDt.ImportRow(outDr(i))
            ElseIf String.IsNullOrEmpty(outDr(i).Item("UNSO_NO_L").ToString) = False AndAlso _
                   (outDr(i).Item("UNSO_NO_L").ToString).Equals(outDr(i - 1).Item("UNSO_NO_L").ToString) = True Then
                '2012/2/14 DEL 個数はヘッダ部個数としたため、加算はしない。
                ''まとめ対象データの場合
                ''個数まとめ
                'setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KOSU") = Convert.ToString( _
                '                                                                       Convert.ToDecimal(setOutDt.Rows(setOutDt.Rows.Count - 1).Item("KOSU").ToString()) + _
                '                                                                       kosu _
                '                                                                      )
            Else
                setOutDt.ImportRow(outDr(i))
            End If

        Next

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("UNSO_NO_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUKKABI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KEN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_KANA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDA_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_KEN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_KANA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KOSU").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("JYURYO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("JYURYO_SAI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_6").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAISOBI_MM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAISOBI_DD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("REMARK_NOUKI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKURI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KAMOTSU_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("MOTOTYAKU_KB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KASHIKIRI_KB").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMF850OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMF850OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMF850OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMF850OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMF850OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

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

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

#End Region 'Method

End Class