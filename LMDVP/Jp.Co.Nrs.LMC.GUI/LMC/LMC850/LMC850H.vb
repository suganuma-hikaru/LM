' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC850C : 名鉄CSV出力(埼玉)
'  作  成  者       :  大貫和正
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
''' LMC850ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC850H
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
        Me.UpdateMeitetuCsv(Me._PrmDs)

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
        Dim setOutDt As DataTable = setDs.Tables("LMC850OUT")
        setOutDt.Clear()

        '並び替えをする
        '20130215 upd s.kobayashi 要望番号:1859 Start 
        'Dim outDr As DataRow() = prmDs.Tables("LMC850OUT").Select(Nothing, "CUST_CD_L,JIS,NIUKENIN_CD,SYUKKABI,HAISOBI,DENPYO_NO")
        Dim outDr As DataRow() = prmDs.Tables("LMC850OUT").Select(Nothing, "CUST_CD_L,NIUKENIN_ADD1,NIUKENIN_ADD2,NIUKENIN_ADD3,NIUKENIN_CD,SYUKKABI,HAISOBI,DENPYO_NO")
        '20130215 upd s.kobayashi 要望番号:1859 End

        'まとめ処理、編集処理
        Dim kiji7 As String = String.Empty
        Dim kosu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim cutStr() As String
        Dim matomeCnt As Integer = 0
        Dim max As Integer = outDr.Length - 1

        For i As Integer = 0 To max
            '別インスタンスのデータロウを空にする
            kiji7 = outDr(i).Item("KIJI_7").ToString
            kosu = Convert.ToDecimal(outDr(i).Item("KOSU").ToString)
            juryo = Convert.ToDecimal(outDr(i).Item("JYURYO").ToString)

            '荷送人(営業所)名の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_MEI1").ToString, 34, 2)
            outDr(i).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            outDr(i).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人(営業所)住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD1").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人(営業所)住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD2").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人(営業所)住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("NIOKURININ_ADD3").ToString, 34, 1)
            outDr(i).Item("NIOKURININ_ADD3") = cutStr(0).Trim

            '荷受人(届先)名の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_NM1").ToString, 34, 2)
            outDr(i).Item("NIUKENIN_NM1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_NM2") = cutStr(1).Trim

            '荷送人(営業所)住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("NIUKENIN_ADD1").ToString, 34, 3)
            outDr(i).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            outDr(i).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            outDr(i).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '荷送人(営業所)郵便番号ハイフン入り制御 
            If outDr(i).Item("NIOKURININ_ZIP").ToString.Trim.Equals("") = False Then
                outDr(i).Item("NIOKURININ_ZIP") = Left(outDr(i).Item("NIOKURININ_ZIP").ToString, 3) & "-" & _
                                                  Right(outDr(i).Item("NIOKURININ_ZIP").ToString, 4)

            End If

            '荷受人(届先)郵便番号ハイフン入り制御 
            If outDr(i).Item("NIUKENIN_ZIP").ToString.Trim.Equals("") = False Then
                outDr(i).Item("NIUKENIN_ZIP") = Left(outDr(i).Item("NIUKENIN_ZIP").ToString, 3) & "-" & _
                                                Right(outDr(i).Item("NIUKENIN_ZIP").ToString, 4)

            End If

            '記事1の分割処理
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

            '記事4の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_4").ToString, 40, 1)
            outDr(i).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.stringCut(outDr(i).Item("KIJI_5").ToString, 40, 2)
            outDr(i).Item("KIJI_5") = cutStr(0).Trim
            outDr(i).Item("KIJI_6") = cutStr(1).Trim

            setOutDt.ImportRow(outDr(i))
        Next

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("DENPYO_NO").ToString()))          '伝票番号
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUKKABI").ToString()))           '出荷日
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_CD").ToString()))      '荷送人コード(送り状Ｍ FREE_C01)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI1").ToString()))    '荷送人名1(営業所名)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_MEI2").ToString()))    '荷送人名2(営業所名)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ZIP").ToString()))     '荷送人郵便番号(営業所郵便番号)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD1").ToString()))    '荷送人住所1(営業所住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD2").ToString()))    '荷送人住所2(営業所住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_ADD3").ToString()))    '荷送人住所3(営業所住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_TEL").ToString()))     '荷送人住所1(営業所電話番号)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SHIHARAININ_CD").ToString()))     '支払人コード(送り状Ｍ FREE_C02)　…？
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_CD").ToString()))        '荷受人コード(届先コード)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM1").ToString()))       '荷受人名1(届先名)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_NM2").ToString()))       '荷受人名2(届先名)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ZIP").ToString()))       '荷受人郵便番号(届先郵便番号)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD1").ToString()))      '荷受人住所1(届先住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD2").ToString()))      '荷受人住所2(届先住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_ADD3").ToString()))      '荷受人住所3(届先住所)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIUKENIN_TEL").ToString()))       '荷受人郵便番号(届先郵便番号)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAISOBI").ToString()))            '配送日(納入日)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_1").ToString()))             '記事1(送り状Ｍ FREE_C03)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_2").ToString()))             '記事2(送り状Ｍ FREE_C04)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_3").ToString()))             '記事3(送り状Ｍ FREE_C05)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_4").ToString()))             '記事4(送り状Ｍ FREE_C06)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_5").ToString()))             '記事5(運送備考)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_6").ToString()))             '記事6(運送備考)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_7").ToString()))             '記事7(オーダー番号)
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_8").ToString()))             '記事8(オーダー番号)
                setData.Append(",")
                setData.Append(.Item("KOSU").ToString())                                '個数(SUM(OUTKA_PKG_NB))
                setData.Append(",")
                setData.Append(.Item("PARETTOSU").ToString())                           'パレット数(0固定)
                setData.Append(",")
                setData.Append(.Item("JYURYO").ToString())                              '重量(運送重量)
                setData.Append(",")
                setData.Append(.Item("YOSEKI").ToString())                              '容積(0固定)
                setData.Append(",")
                setData.Append(.Item("HOKENRYOU").ToString())                           '保険料(0固定)
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC850OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC850OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC850OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC850OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC850OUT").Rows(0).Item("FILEPATH").ToString)
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
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC850BLF", "UpdateMeitetuCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class