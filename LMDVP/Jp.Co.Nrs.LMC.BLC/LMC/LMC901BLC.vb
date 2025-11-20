' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC901BLC : 毒劇物譲受書(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC901BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC901BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC901DAC = New LMC901DAC()

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
        If ds.Tables("LMC901IN").Rows.Count = 0 Then
            MyBase.SetMessage("G039")
            Return ds
        End If

        ' データ取得はDataSetのコピーを使用する
        Dim setDs As DataSet = ds.Copy()

        ' 印刷条件の繰り返し
        For dtIdx As Integer = 0 To ds.Tables("LMC901IN").Rows.Count - 1
            ' 印刷条件の設定
            setDs.Clear()
            setDs.Tables("LMC901IN").ImportRow(ds.Tables("LMC901IN").Rows(dtIdx))

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
            For dtIdx2 As Integer = 0 To setDs.Tables("LMC901OUT").Rows.Count - 1
                ds.Tables("LMC901OUT").ImportRow(setDs.Tables("LMC901OUT").Rows(dtIdx2))
            Next
        Next

        ' 印刷対象0件チェック
        If ds.Tables("LMC901OUT").Rows.Count = 0 Then
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
                    ds.Tables("LMC901OUT"),
                    ds.Tables(LMConst.RD))
        Next

        If mode.Equals("2") Then
            Dim outkaNoL As String = String.Empty
            Dim filePath As String = "/rexport [5,C:\LMUSER\LMC901"
            Dim pdf As String = ".pdf] /rop"

            For i As Integer = 0 To ds.Tables(LMConst.RD).Rows.Count - 1
                outkaNoL = ds.Tables("LMC901IN").Rows(0).Item("OUTKA_NO_L").ToString()
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

        For Each dr As DataRow In ds.Tables("LMC901OUT").Rows
            '受注伝票番号を前ゼロ埋めで10桁に編集
            Dim editJuchuDenpyoNo As String = Right(String.Concat(StrDup(10, "0"c), dr.Item("JUCHU_DENPYO_NO").ToString()), 10)
            '明細番号を前ゼロ埋めで6桁に編集
            Dim editMeisai As String = Right(String.Concat(StrDup(6, "0"c), dr.Item("MEISAI").ToString()), 6)
            '商品コードの前ゼロを除去
            Dim editGoodsCd As String = dr.Item("CUST_GOODS_CD").ToString().Trim.TrimStart("0"c)
            'ロットNoはそのまま
            Dim editLotNo As String = dr.Item("LOT_NO").ToString()
            '数量を左半角空白埋めで13桁に編集
            Dim editSuryo As String = Right(String.Concat(Space(13), dr.Item("SURYO").ToString()), 13)

            'テキスト①
            dr.Item("TEXT_1") = editJuchuDenpyoNo
            'バーコード①
            dr.Item("BARCODE_1") = String.Concat("N", editJuchuDenpyoNo)
            'テキスト②
            dr.Item("TEXT_2") = String.Concat(editMeisai, Space(1), editGoodsCd, Space(1), editLotNo, Space(1), editSuryo)
            'バーコード②
            dr.Item("BARCODE_2") = String.Concat("N", editJuchuDenpyoNo, editMeisai)
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
