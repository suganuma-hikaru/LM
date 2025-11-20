' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC543BLC : 送品案内書(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC543BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC543BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC543DAC = New LMC543DAC()

#End Region ' "Field"

#Region "Method"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet
        Return DoPrintMain(ds, "1")
    End Function

    ''' <summary>
    ''' 印刷処理2
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint2(ByVal ds As DataSet) As DataSet
        Return DoPrintMain(ds, "2")
    End Function

    ''' <summary>
    ''' 印刷処理（メイン）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <param name="mode"></param>
    ''' <remarks></remarks>
    Private Function DoPrintMain(ByVal ds As DataSet, ByVal mode As String) As DataSet

        ' 印刷条件0件チェック
        If ds.Tables("LMC543IN").Rows.Count = 0 Then
            MyBase.SetMessage("G039")
            Return ds
        End If

        ' データ取得はDataSetのコピーを使用する
        Dim setDs As DataSet = ds.Copy()

        ' 印刷条件の繰り返し
        For dtIdx As Integer = 0 To ds.Tables("LMC543IN").Rows.Count - 1
            ' 印刷条件の設定
            setDs.Clear()
            setDs.Tables("LMC543IN").ImportRow(ds.Tables("LMC543IN").Rows(dtIdx))

            ' 帳票パターン取得
            If dtIdx = 0 Then
                ' 繰り返しの1回目のみ
                With Nothing
                    ' 取得
                    setDs = Me.SelectMPrt(setDs)

                    ' メッセージが設定されていればエラーで抜ける
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    ' レポートファイル名が未設定なら抜ける
                    If setDs.Tables("M_RPT").Rows.Count < 1 Then
                        Return ds
                    ElseIf String.IsNullOrEmpty(setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString()) Then
                        Return ds
                    End If

                    ds.Tables("M_RPT").ImportRow(setDs.Tables("M_RPT").Rows(0))
                End With
            End If

            ' 印刷データ取得
            setDs = Me.SelectPrintData(setDs)
            For dtIdx2 As Integer = 0 To setDs.Tables("LMC543OUT").Rows.Count - 1
                ds.Tables("LMC543OUT").ImportRow(setDs.Tables("LMC543OUT").Rows(dtIdx2))
            Next
        Next

        ' 印刷対象0件チェック
        If ds.Tables("LMC543OUT").Rows.Count = 0 Then
            Return ds
        End If

        ' 印刷対象データ取得後の加工
        EditPrintData(ds)

        ' 帳票CSV出力
        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                    dr.Item("PTN_ID").ToString(),
                    dr.Item("PTN_CD").ToString(),
                    dr.Item("RPT_ID").ToString(),
                    ds.Tables("LMC543OUT"),
                    ds.Tables(LMConst.RD))
        Next

        If mode.Equals("2") Then
            Dim outkaNoL As String = String.Empty
            Dim filePath As String = "/rexport [5,C:\LMUSER\LMC543"
            Dim pdf As String = ".pdf] /rop"

            For i As Integer = 0 To ds.Tables(LMConst.RD).Rows.Count - 1
                outkaNoL = ds.Tables("LMC543IN").Rows(0).Item("OUTKA_NO_L").ToString()
                ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH") = String.Concat(ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH"), filePath, outkaNoL, pdf)
            Next
        End If

        Return ds

    End Function

    ''' <summary>
    '''　帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

    ''' <summary>
    ''' 印刷対象データ取得後の加工
    ''' </summary>
    ''' <param name="ds"></param>
    Private Sub EditPrintData(ByVal ds As DataSet)

        ' 件数を保持する
        Dim offlineNoCount As Integer = 0

        ' オフラインNo. 毎の先頭レコードを保持する
        ' Key: オフラインNo., Value: LMC543OUT の DataRow (コピー値)
        Dim offlineNoTopRec As New Dictionary(Of String, DataRow)

        Dim offlineNo As String

        For Each dr As DataRow In ds.Tables("LMC543OUT").Rows
            ' オフラインNo.を編集する。
            offlineNo = dr.Item("OFFLINE_NO").ToString()
            If LenB(offlineNo) = 16 Then
                dr.Item("OFFLINE_NO") = offlineNo.Substring(0, 10).TrimStart("0"c)
                dr.Item("OFFLINE_NO_DTL") = offlineNo.Substring(10).TrimStart("0"c)
            End If

            ' 会社名を必要に応じて分割する。
            Dim compNam As String = dr.Item("COMP_NM_1").ToString()
            If LenB(compNam) > 40 Then
                Dim compNm1 As String = LeftB(compNam, 40)
                Dim compNm2 As String = compNam.Substring(compNm1.Length, compNam.Length - compNm1.Length)
                dr.Item("COMP_NM_1") = compNm1
                dr.Item("COMP_NM_2") = compNm2
            End If

            ' 本数をカンマ編集する。
            Dim inoutkaNbStr As String = dr.Item("INOUTKA_NB").ToString()
            If IsNumeric(inoutkaNbStr) Then
                Dim inoutkaNb As Double = 0D
                If Double.TryParse(inoutkaNbStr, inoutkaNb) Then
                    dr.Item("INOUTKA_NB") = inoutkaNb.ToString("#,0")
                End If
            End If

            ' 件数をカウントする
            offlineNo = dr.Item("OFFLINE_NO").ToString()
            offlineNoCount += 1
            dr.Item("OFFLINE_NO_EDA") = offlineNoCount.ToString().PadLeft(10, "0"c)
        Next

        ' 出力件数が出力帳票の明細数(10件)単位でない場合、不足する件数を追加する。
        Dim addMax As Integer = 10 - (offlineNoCount Mod 10)
        If addMax <> 10 Then
            Dim offlineNoEda As Integer = offlineNoCount
            For addCnt = 1 To addMax
                ' 先頭レコードのヘッダ/フッタ項目を元に、枝番のみカウントアップ値を設定する。
                Dim addDr As DataRow = ds.Tables("LMC543OUT").NewRow()
                addDr.ItemArray = ds.Tables("LMC543OUT").Rows(0).ItemArray
                ' 明細項目は追加レコード設定範囲外
                addDr.Item("GOODS_NM") = ""
                addDr.Item("LOT_NO") = ""
                addDr.Item("INOUTKA_NB") = ""
                addDr.Item("ONDO") = ""
                addDr.Item("DOKUGEKI") = ""
                addDr.Item("OFFLINE_NO_DTL") = ""
                addDr.Item("REMARK_DTL") = ""
                addDr.Item("DOKU_MARK") = ""
                addDr.Item("EXP_MARK") = ""
                ' 枝番へのカウントアップ値の設定
                offlineNoEda += 1
                addDr.Item("OFFLINE_NO_EDA") = offlineNoEda.ToString().ToString().PadLeft(10, "0"c)
                ds.Tables("LMC543OUT").Rows.Add(addDr)
            Next
        End If

        ' 出力するページの種類分、取得したレコードを増やす。
        Dim dt As DataTable() = New DataTable() {
            ds.Tables("LMC543OUT").Copy(),
            ds.Tables("LMC543OUT").Copy(),
            ds.Tables("LMC543OUT").Copy()}
        Dim dtSort As DataTable = ds.Tables("LMC543OUT").Clone()
        For pageKb As Integer = 1 To 3
            ' 出力するページの種類を帳票定義体で判定する区分を設定する。
            ' "1": 送品案内書
            ' "2": 送品案内書(控え)
            ' "3": 受領書
            For Each dr As DataRow In dt(pageKb - 1).Rows
                dr.Item("PAGE_KB") = pageKb.ToString()
            Next
            dtSort.Merge(dt(pageKb - 1))
        Next
        ds.Tables("LMC543OUT").Clear()
        Dim drImportRecs As DataRow() = dtSort.Select("", "OFFLINE_NO, PAGE_KB, OFFLINE_NO_EDA")
        For Each drImport In drImportRecs
            ds.Tables("LMC543OUT").ImportRow(drImport)
        Next

    End Sub

#Region "SJISバイト数取得"
    ''' <summary>
    ''' 文字列長（Shift_JIS 換算のバイト数）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer

        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)

    End Function
#End Region' "SJISバイト数取得"

#Region "バイト切捨て(LeftB)"
    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function
#End Region ' "バイト切捨て(LeftB)"

#Region "バイト切り抜き(MidB)"
    ''' <summary>Mid関数のバイト版。文字数と位置をバイト数で指定して文字列を切り抜く。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Start">切り抜き開始位置。全角文字を分割するよう位置が指定された場合、戻り値の文字列の先頭は意味不明の半角文字となる。</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切り抜かれた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function MidB(ByVal str As String, ByVal Start As Integer, Optional ByVal Length As Integer = 0) As String

        '空文字に対しては常に空文字を返す
        If str.Equals(String.Empty) Then
            Return String.Empty
        End If

        'Startが対象文字列バイト数より大きい場合は空文字を返す
        If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) < Start Then
            Return String.Empty
        End If

        'Lengthが0か、Start以降のバイト数をオーバーする場合はStart以降の全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str) - Start + 1

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切り抜き
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), Start - 1, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - Start + 1

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function
#End Region ' "バイト切り抜き(MidB)"

#End Region ' "Method"

End Class
