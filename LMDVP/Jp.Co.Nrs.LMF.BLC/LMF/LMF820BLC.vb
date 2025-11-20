' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF      : 運送管理
'  プログラムID     :  LMF820   : 車載受注渡し処理
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.Text
Imports System.IO

''' <summary>
''' LMF820BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF820BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF820DAC = New LMF820DAC()


#End Region

#Region "Method"

#Region "メイン処理"

    ''' <summary>
    ''' 車載受注渡しデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BLCの車載受注渡し処理コントロール</remarks>
    Private Function SyasaiWatashi(ByVal ds As DataSet) As DataSet

        '①車載受注渡し対象データ取得取得 -------------------
        Dim rtnDs As DataSet = Me.SelectSyasaiWatashi(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("LMF820OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        '②車載受注渡し対象データ編集処理 -------------------
        rtnDs = Me.EditSelectDataSet(rtnDs)

        '③車載受注渡し対象データ更新処理 -------------------
        rtnDs = Me.SyasaiJutyuWatashi(rtnDs)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 車載受注渡しデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectSyasaiWatashiメソッド呼出</remarks>
    Private Function SelectSyasaiWatashi(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMF820IN")
        Dim outDt As DataTable = ds.Tables("LMF820OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMF820IN")
        Dim setDt As DataTable = setDs.Tables("LMF820OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '車載受注渡しデータの取得(DAC呼び出し)
            rtnResult = Me.ServerChkJudge(setDs, "SelectSyasaiWatashi")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                MyBase.SetMessage("E430")
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 車載受注渡しデータ処理(追加処理 DAC呼び出し)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SyasaiJutyuWatashi(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "SyasaiJutyuWatashi", ds)

    End Function

    ''' <summary>
    ''' DataSet編集処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditSelectDataSet(ByVal ds As DataSet) As DataSet

        Dim outDt As DataTable = ds.Tables("LMF820OUT") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得

        '編集処理
        For i As Integer = 0 To max

            '[1]受注番号
            '10byte → 9byteに変更
            outDt.Rows(i).Item("JYT_NO") = Right(outDt.Rows(i).Item("JYT_NO").ToString, 9)

            '[2]作業NO1
            If outDt.Rows(i).Item("SGY_NO1").ToString.Trim.Length = 8 Then
                '作業NO1(出荷日)が8桁ならば、3桁目～6桁目をセット
                outDt.Rows(i).Item("SGY_NO1") = Mid(outDt.Rows(i).Item("SGY_NO1").ToString, 3, 4)
            Else
                '作業NO1(出荷日)が8桁以外ならば、""設定
                outDt.Rows(i).Item("SGY_NO1") = ""
            End If

            '[3]作業NO2
            '9byte → 7byteに変更
            outDt.Rows(i).Item("SGY_NO2") = Right(outDt.Rows(i).Item("SGY_NO2").ToString, 7)

            '[4]荷主名
            If SetByteCount(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString.Trim) > 16 Then
                '16byteより大きかったならば、下記の文字を削除、17byte以降切り捨て
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "株式会社", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "有限会社", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "㈱", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "㈲", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "（株）", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "（有）", "")
                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), "・", "")

                outDt.Rows(i).Item("NNU_TRSRYK_MEI") = LeftB(outDt.Rows(i).Item("NNU_TRSRYK_MEI").ToString(), 16)

            End If

            '[5]届先名
            If SetByteCount(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString.Trim) > 16 Then
                '16byteより大きかったならば、下記の文字を削除、17byte以降切り捨て
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "株式会社", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "有限会社", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "㈱", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "㈲", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "（株）", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "（有）", "")
                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = Replace(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), "・", "")

                outDt.Rows(i).Item("OROSI_TRSRYK_MEI") = LeftB(outDt.Rows(i).Item("OROSI_TRSRYK_MEI").ToString(), 16)

            End If

            '[6]積降区域
            If SetByteCount(outDt.Rows(i).Item("OROSI_KUIKI_MEI").ToString.Trim) > 10 Then
                '10byteより大きかったならば、11byte以降切り捨て
                outDt.Rows(i).Item("OROSI_KUIKI_MEI") = LeftB(outDt.Rows(i).Item("OROSI_KUIKI_MEI").ToString(), 10)
            End If

            '[7]商品名
            If SetByteCount(outDt.Rows(i).Item("HINMEIRYK").ToString.Trim) > 20 Then
                '20byteより大きかったならば、21byte以降切り捨て
                outDt.Rows(i).Item("HINMEIRYK") = LeftB(outDt.Rows(i).Item("HINMEIRYK").ToString(), 20)
            End If

        Next

        Return ds

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

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

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetByteCount(ByVal str As String) As Integer

        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)
        Return RestLength

    End Function


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
#End Region

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

#End Region

#End Region

#End Region

End Class
