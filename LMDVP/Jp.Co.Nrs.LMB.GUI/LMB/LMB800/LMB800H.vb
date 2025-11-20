' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 出荷
'  プログラムID     :  LMB800  : GHSラベル CSV出力
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
''' LMB800ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB800H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconV As LMBControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconH As LMBControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

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
        Dim setOutDt As DataTable = setDs.Tables("LMB800CSV")

        'LABE_TYPEを取得
        Dim getLABE_TYPE As String = setDs.Tables("LMB800IN").Rows(0).Item("LABEL_TYPE").ToString.Trim

        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMB800OUT").Select("PDF_NO <> '' AND PDF_NM <> '' AND FOLDER <> ''", "INKA_NO,PDF_NO")

        '編集処理
        Dim max As Integer = outDr.Length - 1
        Dim strWk As String = String.Empty

        Dim dr As DataRow = Nothing

        'システム日時取得
        Dim sysDtTm As String() = MyBase.GetSystemDateTime()

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim csvPath As String = String.Empty

        Dim setData As StringBuilder = Nothing

        Dim sr As StreamWriter = Nothing

        '一時退避用
        Dim nyuko_no As String = String.Empty
        Dim lot_no As String = String.Empty
        Dim pdf_nm As String = String.Empty
        Dim pdf_no As String = String.Empty
        Dim folder As String = String.Empty
        Dim pkg_nb As String = String.Empty
        Dim kosu As String = String.Empty
        Dim inka_no As String = String.Empty
        Dim goods_cd_cust As String = String.Empty
        Dim konsu As String = String.Empty
        Dim btw_nm As String = String.Empty

        For i As Integer = 0 To max

            If getLABE_TYPE.Equals(outDr(i).Item("PDF_NO").ToString) = False Then
                '指定のラベル種別以外はCSV作成対象外
                Continue For
            End If

            '入庫番号
            nyuko_no = outDr(i).Item("NYUKO_NO").ToString
            'ロット
            lot_no = outDr(i).Item("LOT_NO").ToString
            'PDF名
            pdf_nm = outDr(i).Item("PDF_NM").ToString
            'ファイルパスから拡張子を取得します
            Dim sExt As String = System.IO.Path.GetExtension(pdf_nm)
            '拡張子ばない場合は、".pdf"を付加する
            If String.IsNullOrEmpty(sExt) Then
                pdf_nm = String.Concat(pdf_nm, ".pdf")
            End If

            'PDF_NO
            pdf_no = outDr(i).Item("PDF_NO").ToString
            'フォルダー
            folder = outDr(i).Item("FOLDER").ToString
            '包装個数 (=入数)
            pkg_nb = outDr(i).Item("PKG_NB").ToString
            '個数
            kosu = outDr(i).Item("KOSU").ToString
            '入荷番号
            inka_no = Mid(outDr(i).Item("INKA_NO").ToString, 1, 9)
            '荷主商品コード
            goods_cd_cust = outDr(i).Item("GOODS_CD_CUST").ToString
            '梱数
            konsu = outDr(i).Item("KONSU").ToString
            'BarTender　btwファイル名称
            btw_nm = outDr(i).Item("BTW_NM").ToString

            '*** 梱包数分データ作成
            If ("0").Equals(outDr(i).Item("KONSU").ToString) = True Then
                '箱個数クリア
                pkg_nb = String.Empty

            Else
                '梱包数データ作成

                'CSV出力処理
                setData = New StringBuilder()

                '１行目に各項目名設定
                setData.Append("入庫番号")
                setData.Append(",")
                setData.Append("ロット")
                setData.Append(",")
                setData.Append("PDF")
                setData.Append(",")
                setData.Append("発行枚数")
                setData.Append(",")
                setData.Append("包装個数")
                setData.Append(",")
                setData.Append("ラベルプリンタ")

                setData.Append(vbNewLine)

                'データ設定
                setData.Append(nyuko_no)
                setData.Append(",")
                setData.Append(lot_no)
                setData.Append(",")
                setData.Append(pdf_nm)
                setData.Append(",")
                setData.Append(konsu)
                setData.Append(",")
                setData.Append("x" & pkg_nb.ToString.Trim)
                setData.Append(",")
                setData.Append(btw_nm)

                'setData.Append(vbNewLine)

                '保存先のCSVファイルのパス
                csvPath = String.Concat(folder.ToString, _
                                        inka_no.ToString, "-", _
                                        goods_cd_cust.ToString, "-", _
                                        pkg_nb, "-", _
                                        i.ToString, "-", _
                                        sysDtTm(0), "-", _
                                        Mid(sysDtTm(1), 1, 6), _
                                        ".csv")

                'ファイルを開く
                System.IO.Directory.CreateDirectory(folder.ToString)
                sr = New StreamWriter(csvPath, False, enc)

                '値の設定
                sr.Write(setData.ToString())

                'ファイルを閉じる
                sr.Close()

            End If

            '*** 個数分データ作成
            '包装個数 (クリア)	 
            pkg_nb = String.Empty

            'CSV出力処理
            setData = New StringBuilder()

            '１行目に各項目名設定
            setData.Append("入庫番号")
            setData.Append(",")
            setData.Append("ロット")
            setData.Append(",")
            setData.Append("PDF")
            setData.Append(",")
            setData.Append("発行枚数")
            setData.Append(",")
            setData.Append("包装個数")
            setData.Append(",")
            setData.Append("ラベルプリンタ")

            setData.Append(vbNewLine)

            'データ設定
            setData.Append(nyuko_no)
            setData.Append(",")
            setData.Append(lot_no)
            setData.Append(",")
            setData.Append(pdf_nm)
            setData.Append(",")
            setData.Append(kosu)
            setData.Append(",")
            setData.Append("")
            setData.Append(",")
            setData.Append(btw_nm)

            'setData.Append(vbNewLine)

            '保存先のCSVファイルのパス
            csvPath = String.Concat(folder.ToString, _
                                    inka_no.ToString, "-", _
                                    goods_cd_cust.ToString, "-", _
                                    i.ToString, "-", _
                                    sysDtTm(0), "-", _
                                    Mid(sysDtTm(1), 1, 6), _
                                    ".csv")

            System.IO.Directory.CreateDirectory(folder.ToString)
            sr = New StreamWriter(csvPath, False, enc)

            '値の設定
            sr.Write(setData.ToString())

            'ファイルを閉じる
            sr.Close()
        Next

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