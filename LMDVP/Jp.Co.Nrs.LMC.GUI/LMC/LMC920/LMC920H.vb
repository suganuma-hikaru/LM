' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC920  : カンガルーマジック CSV出力
'  作  成  者       :  daikoku
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
''' LMC920ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC920H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

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

        '出荷(大)の更新
        Me.UpdateKangarooCsv(Me._PrmDs)

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
        Dim setOutDt As DataTable = setDs.Tables("LMC920OUT")
        setOutDt.Clear()
        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMC920OUT").Select(Nothing, "OUTKA_PLAN_DATE")

        Dim max As Integer = outDr.Length - 1
        Dim strWk As String = String.Empty

        For i As Integer = 0 To max

            '編集がある場合は下記で処理する
            ''出荷予定日編集(YYYY/MM/DD)
            'If String.IsNullOrEmpty(outDr(i).Item("OUTKA_PLAN_DATE").ToString) = False Then

            '    outDr(i).Item("OUTKA_PLAN_DATE") = Left(CStr(Convert.ToDateTime((Convert.ToInt32(outDr(i).Item("OUTKA_PLAN_DATE").ToString)).ToString("0000/00/00"))), 10)
            'End If


            setOutDt.ImportRow(outDr(i))
        Next

        'CSV出力処理
        Dim sWk(0) As String

        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("OUTKA_NO_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PLAN_DATE").ToString()))
                setData.Append(",")

                '-（ハイフン）を取る
                setData.Append(SetDblQuotation(.Item("AD_TEL").ToString().Replace("-", "")))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ZIP").ToString()))
                setData.Append(",")

                sWk(0) = Mid(.Item("AD_1").ToString(), 1, 30)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = Mid(.Item("AD_2").ToString(), 1, 30)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = Mid(.Item("AD_3").ToString(), 1, 30)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = Mid(.Item("DEST_NM").ToString(), 1, 30)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PKG_NB").ToString()))
                setData.Append(",")

                sWk = Me.stringCut(.Item("OUTKAL_REMARK").ToString(), 30, 1)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk = Me.stringCut(.Item("UNSOL_REMARK").ToString(), 30, 1)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk = Me.stringCut(.Item("NHS_REMARK").ToString(), 30, 1)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ARR_PLAN_DATE").ToString()))
                setData.Append(",")

                '-（ハイフン）を取る
                setData.Append(SetDblQuotation(.Item("CODE_TEL").ToString().Replace("-", "")))
                setData.Append(",")

                '-（ハイフン）を取る
                setData.Append(SetDblQuotation(.Item("TEL").ToString().Replace("-", "")))
                setData.Append(",")

                sWk(0) = Mid(.Item("AD1_AD2").ToString(), 1, 20)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = Mid(.Item("SOKO_WH_NM").ToString(), 1, 20)
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = .Item("JYURYO").ToString()
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = .Item("CUST_ORD_NO").ToString()
                setData.Append(SetDblQuotation(sWk(0).ToString))
                setData.Append(",")

                sWk(0) = .Item("BUYER_ORD_NO").ToString()
                setData.Append(SetDblQuotation(sWk(0).ToString))

                If "30".Equals(.Item("NRS_BR_CD").ToString) AndAlso "00360".Equals(.Item("CUST_CD_L").ToString) Then
                    'アルマークのみ
                    sWk(0) = Me.LeftB(.Item("GOODS_NM").ToString(), 60)
                    setData.Append(",")
                    setData.Append(SetDblQuotation(sWk(0).ToString))
                End If

                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC920OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC920OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC920OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC920OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC920OUT").Rows(0).Item("FILEPATH").ToString)
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

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateKangarooCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC920BLF", "UpdateKangarooCsv", ds)

        Return rtnDs

    End Function

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

#End Region 'Method

End Class