' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  103　　　 : 富士フイルム（千葉）
'  作  成  者       :  terakawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Const
'↓FFEM特殊処理↓
'2014.06.09 使用START
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
'↑FFEM特殊処理↑
'2014.06.09 使用END
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC103
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    '↓FFEM特殊処理↓
    '2014.06.09 使用START
    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMC020DS
    '↑FFEM特殊処理↑
    '2014.06.09 使用END

    '↓FFEM特殊処理↓
    '2014.06.09 使用START
    Private _MotoDelDac As LMC020DAC = New LMC020DAC()
    '↑FFEM特殊処理↑
    '2014.06.09 使用END

    Public Const SIKAKARI_SYUKKA As String = "Z3"
    Public Const SYUKKA_REMARK As String = "！！！荷動きなし！！！"

    Public Const DENPYO_TYPE_Z03 As String = "Z03"
    Public Const DENPYO_TYPE_551 As String = "551"
    Public Const DENPYO_TYPE_301 As String = "301"

    Public Const SYANAISIYOU_REMARK As String = "社内使用"
    Public Const HAIKI_REMARK As String = "廃棄"
    Public Const TUMEKAE_REMARK As String = "詰め替え"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC103 = New LMH030DAC103()

    ''' <summary>
    ''' 使用するDACクラスの生成(共通DAC)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacCom As LMH030DAC = New LMH030DAC()

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH030BLC = New LMH030BLC()

#End Region

#Region "Method"

#Region "画面取込(セミEDI) 関連処理"

#Region "画面取込(セミEDI)チェック および関連処理"

#Region "画面取込(セミEDI)チェック"

    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        Dim dr As DataRow

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim iRowCnt As Integer = 0

        For i As Integer = 0 To hedmax

            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）
                Dim sFileNm As String = dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E460", , , LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

                For j As Integer = iRowCnt To max

                    dr = dtSemiDtl.Rows(j)

                    If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                        'ヘッダと明細のファイル名称が等しい場合

                        '入力チェック(数値,日付チェック)
                        '(チェックは省略列値を前行以前から補う前の取込値にて行う)
                        If Me.TorikomiValChk(dr) = False Then

                            '異常の場合

                            '詳細のエラーフラグに"1"をセットする
                            dr.Item("ERR_FLG") = "1"

                            'ヘッダのエラーフラグに"1"をセットする
                            dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
                        Else
                            '正常の場合は処理無し（未処理（:9）の状態を保持するため）
                        End If
                    Else
                        'ヘッダと明細のファイル名称が等しくない場合
                        '現在行を保持してループを抜ける()
                        iRowCnt = j
                        Exit For
                    End If

                Next


            End If
        Next

        Return ds

    End Function

#End Region ' "画面取込(セミEDI)チェック"

#Region "カラム項目の値・日付チェック"

    ''' <summary>
    ''' 値・日付チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    Public Function TorikomiValChk(ByVal dr As DataRow) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim sDate As String = String.Empty
        Dim targetStr As String = String.Empty
        Dim dNum As Double = 0
        Dim bRet As Boolean = True

        ' 受注伝票番号
        targetStr = dr.Item("COLUMN_1").ToString().Trim()
        ' 文字列長チェック
        If LenB(targetStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注伝票番号(カラム1番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 受注明細番号
        targetStr = dr.Item("COLUMN_2").ToString().Trim()
        ' 文字列長チェック
        If LenB(targetStr) > 6 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注明細番号(カラム2番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 出荷先
        ' （→画面.届先コード）
        ' ※LMSは NVARCHAR(15) だが、FFEM H_INOUTKAEDI_DTL_FJF.ZFVYSYUKKAC が NVARCHAR(12) なので 12 バイト超はエラーとする。
        targetStr = dr.Item("COLUMN_5").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷先(カラム5番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 12, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先(カラム5番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 出荷先名称
        ' （→画面.届先名称）
        targetStr = dr.Item("COLUMN_6").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷先名称(カラム6番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 文字列長チェック
        ElseIf LenB(targetStr) > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷先名称(カラム6番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 品目コード
        ' （→画面.商品コード）
        ' ※LMS では NVARCHAR(20) だが、FFEM H_INOUTKAEDI_DTL_FJF.MATNR が NVARCHAR(18) なので 18 バイト超はエラーとする。
        targetStr = dr.Item("COLUMN_9").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("品目コード(カラム9番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 18, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("品目コード(カラム9番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 伝票品目テキスト
        ' （→画面.商品名）
        targetStr = dr.Item("COLUMN_10").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("伝票品目テキスト(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 文字列長チェック
        ElseIf LenB(targetStr) > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("伝票品目テキスト(カラム10番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' ロット
        ' （→画面.ロット№）
        targetStr = dr.Item("COLUMN_13").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("ロット(カラム13番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 文字列長チェック
        ElseIf LenB(targetStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ロット(カラム13番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 出荷日付
        ' （→画面.出庫日、同.出荷予定日）
        sDate = dr.Item("COLUMN_43").ToString().Trim()
        ' 年月日チェック
        If IsDate(Me.SerialToDate(sDate)) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日付(カラム43番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 納入予定日
        sDate = dr.Item("COLUMN_44").ToString().Trim()
        ' 年月日チェック
        If IsDate(Me.SerialToDate(sDate)) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("納入予定日(カラム44番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' 受注数量
        ' （→画面.個数）
        targetStr = dr.Item("COLUMN_47").ToString().Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(targetStr)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(targetStr) = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                ElseIf Convert.ToDouble(targetStr) < 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                ElseIf dNum > 9999999999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    If targetStr.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(targetStr.Substring(targetStr.IndexOf(".") + 1)) > 0 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("受注数量(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        ' 戻り値設定
        Return bRet

    End Function

    ''' <summary>
    ''' 文字列が全て半角であるかをチェックし、文字列長をチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <param name="length">比較文字列長</param>
    ''' <returns>True=条件を満たしている False=条件を満たしていない</returns>
    ''' <remarks>文字列長はイコール比較を行う</remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String, ByVal length As Integer) As Boolean
        IsHalfSize(targetString, length, True)
    End Function

    ''' <summary>
    ''' 文字列が全て半角であるかをチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True = 全て半角 False = 全角混在</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String) As Boolean
        Static Encode_JIS As Text.Encoding = Text.Encoding.GetEncoding("Shift_JIS")
        Dim Str_Count As Integer = targetString.Length
        Dim ByteCount As Integer = Encode_JIS.GetByteCount(targetString)
        Return Str_Count.Equals(ByteCount)
    End Function

    ''' <summary>
    ''' 文字列が全て半角であるかをチェックし、文字列長をチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <param name="length">比較文字列長</param>
    ''' <param name="EqualOrMax">True=イコール比較 False=最大値比較 </param>
    ''' <returns>True=条件を満たしている False=条件を満たしていない</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String, ByVal length As Integer, ByVal EqualOrMax As Boolean) As Boolean
        If IsHalfSize(targetString).Equals(False) Then
            Return False
        ElseIf EqualOrMax.Equals(True) Then
            If targetString.Length.Equals(length).Equals(False) Then
                Return False
            End If
        Else
            If targetString.Length > length Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' 文字列が数値（Double型）に変換出来るかチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=変換できる　False=変換できない</returns>
    ''' <remarks></remarks>
    Private Function IsConvertDbl(ByVal targetString As String) As Boolean
        Dim d As Double
        Return Double.TryParse(targetString, d)
    End Function

    ''' <summary>
    ''' 文字列長（Shift_JIS 換算のバイト数）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)
    End Function

#End Region ' "カラム項目の値・日付チェック"

#End Region ' "画面取込(セミEDI)チェック および関連処理"

#Region "画面取込(セミEDI)データセット＋更新処理 および関連処理"

#Region "画面取込(セミEDI)データセット＋更新処理"

    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim dtSetHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")        '処理件数

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        ' 営業所コード
        Dim nrsBrCd As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString()
        ' 荷主コード(大)
        Dim custCdL As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("CUST_CD_L").ToString()

        Dim sWhcd As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("WH_CD").ToString() '倉庫コード     
        ' 届先マスタ自動追加判定
        Dim sFlg17 As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("FLAG_17").ToString()
        If String.IsNullOrEmpty(sFlg17) Then
            sFlg17 = "0"
        End If

        Dim iSkipFlg As Integer = 0                 'スキップフラグ     (0:EDI出荷に登録する、  1:EDI出荷に登録しない)
        Dim iDeleteFlg As Integer = 0               '取消フラグ         (0:EDI出荷を削除しない、1:EDI出荷を削除する)

        Dim iFindRcvEdiFlg As Integer = 0           '削除対象EDI受信データ存在フラグ (0:存在しない、1:存在する)
        Dim iFindOutkaEdiFlg As Integer = 0         '削除対象EDI出荷データ存在フラグ (0:存在しない、1:存在する)

        Dim sNowKey As String = String.Empty        'キー項目（Temp用）
        Dim sOldKey As String = String.Empty        'キー項目（前行）
        Dim sNewKey As String = String.Empty        'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False          '前行とキーが同じ場合True、異なる場合False

        Dim sEdiCtlNo As String = String.Empty      'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0             'EDI管理番号（中）

        Dim iRcvHedInsCnt As Integer = 0            '書込件数（受信HED）
        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iOutHedInsCnt As Integer = 0            '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0            '書込件数（出荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信DTL）
        Dim iOutHedCanCnt As Integer = 0            '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0            '取消件数（出荷EDI(中)）


        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        Dim destDict As Dictionary(Of String, DataRow) = New Dictionary(Of String, DataRow)
        Dim notFoundDestCdSet As HashSet(Of String) = New HashSet(Of String)
        Dim goodsDict As Dictionary(Of String, DataTable) = New Dictionary(Of String, DataTable)
        Dim notFoundGoodsCdSet As HashSet(Of String) = New HashSet(Of String)

        Dim dsWork As DataSet = ds.Clone()

        ' 取込Dtlの並べ替え（255列目: (1列目: 受注伝票番号(前ゼロ付き10桁)) 昇順、256列目(2列目: 明細(前ゼロ付き6桁)) 昇順）
        For Each drSetDtl As DataRow In dtSetDtl.Rows
            drSetDtl.Item("COLUMN_255") = drSetDtl.Item("COLUMN_1").ToString().PadLeft(10, "0"c)
            drSetDtl.Item("COLUMN_256") = drSetDtl.Item("COLUMN_2").ToString().PadLeft(6, "0"c)
        Next
        Dim tmpDt As DataTable = dtSetDtl.Clone()
        Dim tmpDr() As DataRow = Nothing
        tmpDr = dtSetDtl.Select(String.Empty, "COLUMN_255 ASC, COLUMN_256 ASC")
        For Each row As DataRow In tmpDr
            tmpDt.ImportRow(row)
        Next
        Call dtSetDtl.Clear()
        Call dtSetDtl.Merge(tmpDt)

        For i As Integer = 0 To iSetDtlMax

            iSkipFlg = 0
            iDeleteFlg = 0
            iFindRcvEdiFlg = 0
            iFindOutkaEdiFlg = 0

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒EDI受信データセット
            '---------------------------------------------------------------------------
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_FJF").NewRow()
            ds.Tables("LMH030_H_OUTKAEDI_DTL_FJF").Clear() ' 受信DTLのクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, dsWork, i, drEdiRcvDtl)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = drEdiRcvDtl.Item("JUCHU_DENPYO_NO").ToString()

            If i = 0 Then
                '1番目は必ずbSameKeyFlgはFalse
                bSameKeyFlg = False
            Else
                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    If sNewKey = "" Then
                        bSameKeyFlg = False
                    Else
                        bSameKeyFlg = True
                    End If
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                End If
            End If

            Dim setDs As DataSet = New Jp.Co.Nrs.LM.DSL.LMH030DS()
            Dim ediRecCnt As Integer = 0

            If drEdiRcvDtl.Item("JUCHU_DENPYO_NO").ToString().Trim().Equals("") = False AndAlso
                drEdiRcvDtl.Item("MEISAI").ToString().Trim().Equals("") = False Then
                '---------------------------------------------------------------------------
                ' EDI出荷データM件数取得処理
                '---------------------------------------------------------------------------
                setDs.Tables("LMH030_H_OUTKAEDI_DTL_FJF").ImportRow(drEdiRcvDtl)
                setDs = MyBase.CallDAC(Me._Dac, "GetOutkaediMCnt", setDs)

                ediRecCnt = MyBase.GetResultCount()
                If ediRecCnt > 0 Then
                    ' EDI出荷データM 存在の場合
                    ' 当該行はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"すでに取込済み", "取込"},
                                       dtSetDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                       LMH030BLC.EXCEL_COLTITLE_SEMIEDI, dtSetDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                    bNoErr = False
                    Continue For
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, dsWork, iDeleteFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu, drEdiRcvDtl)

            '---------------------------------------------------------------------------
            ' データセット設定 入出荷EDIデータ(明細)
            '---------------------------------------------------------------------------
            Dim drInoutkaEdiDtl As DataRow = ds.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").NewRow()
            ds.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").Clear()
            ds = Me.SetInoutkaEdiDtl(ds, drEdiRcvDtl, drInoutkaEdiDtl)

            If bSameKeyFlg = False Then
                ' 前行と差異がある場合

                '---------------------------------------------------------------------------
                ' データセット設定 入出荷EDIデータ(ヘッダ)
                '---------------------------------------------------------------------------
                Dim drInoutkaEdiHed As DataRow = ds.Tables("LMH030_H_INOUTKAEDI_HED_FJF").NewRow()
                ds.Tables("LMH030_H_INOUTKAEDI_HED_FJF").Clear()
                ds = Me.SetInoutkaEdiHed(ds, drEdiRcvDtl, drInoutkaEdiHed)

                '---------------------------------------------------------------------------
                ' 届先マスタ存在チェック/取得
                '---------------------------------------------------------------------------

                ' 届先マスタ読込用INデータ作成
                Dim setDt As DataTable = setDs.Tables("LMH030_M_DEST")
                Dim setDr As DataRow = setDt.NewRow

                ' INデータ作成前にテーブル内一掃
                setDt.Clear()

                Dim destCnt As Integer = 0
                Dim destCd As String = drEdiRcvDtl.Item("DEST_CD").ToString().Trim()

                If destDict.ContainsKey(destCd) Then
                    setDt.ImportRow(destDict(destCd))
                    destCnt = 1
                ElseIf Not notFoundDestCdSet.Contains(destCd) Then
                    ' INデータ設定
                    setDr.Item("NRS_BR_CD") = nrsBrCd
                    setDr.Item("CUST_CD_L") = custCdL
                    setDr.Item("DEST_CD") = destCd

                    ' Add処理
                    setDt.Rows.Add(setDr)

                    ' 届先マスタ読込
                    setDs = MyBase.CallDAC(Me._Dac, "SelectMstDest", setDs)
                    destCnt = MyBase.GetResultCount
                    If destCnt = 0 Then
                        notFoundDestCdSet.Add(destCd)
                    Else
                        destDict(destCd) = setDs.Tables("LMH030_M_DEST").Rows(0)
                    End If
                End If

                ' 抽出件数判定
                If destCnt = 0 Then
                    If sFlg17.Equals("0") Then
                        ' エラー返却
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493",
                                               New String() {String.Concat("届先コード:", destCd), "届先マスタ", ""},
                                               dtSetDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                               LMH030BLC.EXCEL_COLTITLE_SEMIEDI, dtSetDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                        bNoErr = False
                        Continue For
                    Else
                        ' 自動登録(INSERT)
                        ' 出荷登録時に行うので処理なし

                    End If
                End If

            End If

            '---------------------------------------------------------------------------
            ' EDI出荷データ件数 > 0 の場合、EDI出荷データの削除更新を行う
            '---------------------------------------------------------------------------
            ' ※EDI出荷データM 存在の場合はエラーとするチェックを加えたため、この条件に該当することはなくなったが、
            ' 　後の仕様変更に備えて処理は残す。
            If ediRecCnt > 0 AndAlso (bSameKeyFlg = False) Then

                ' EDI出荷(中)の削除(論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", setDs)
                iOutDtlCanCnt = iOutDtlCanCnt + MyBase.GetResultCount()

                ' EDI出荷(大)の削除(論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", setDs)
                iOutHedCanCnt = iOutHedCanCnt + 1

            End If

            '---------------------------------------------------------------------------
            ' EDI受信データの新規追加
            '---------------------------------------------------------------------------
            setDs.Tables("LMH030_H_OUTKAEDI_DTL_FJF").Clear()
            setDs.Tables("LMH030_H_OUTKAEDI_DTL_FJF").ImportRow(drEdiRcvDtl)
            setDs.Tables("LMH030_SEMIEDI_INFO").ImportRow(ds.Tables("LMH030_SEMIEDI_INFO").Rows(0))

            ' 新規追加 EDI受信データ(DTL) と同一キーレコードの件数取得
            setDs = MyBase.CallDAC(Me._Dac, "GetOutkaEdiDtlCnt", setDs)
            If MyBase.GetResultCount() > 0 Then
                ' 同一キーレコード存在時の物理削除
                setDs = MyBase.CallDAC(Me._Dac, "DeleteOutkaEdiDtl", setDs)
                iRcvDtlCanCnt = iRcvDtlCanCnt + 1
            End If

            ' EDI受信データ(DTL) の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            '---------------------------------------------------------------------------
            ' 入出荷EDIデータ(明細) の新規追加
            '---------------------------------------------------------------------------
            setDs.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").Clear()
            setDs.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").ImportRow(ds.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").Rows(0))

            ' 新規追加 入出荷EDIデータ(明細) と同一キーレコードの件数取得
            setDs = MyBase.CallDAC(Me._Dac, "GetInoutkaEdiDtlCnt", setDs)
            If MyBase.GetResultCount() > 0 Then
                ' 同一キーレコード存在時の物理削除
                setDs = MyBase.CallDAC(Me._Dac, "DeleteInoutkaEdiDtl", setDs)
                iRcvDtlCanCnt = iRcvDtlCanCnt + 1
            End If

            ' 入出荷EDIデータ(明細) の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertInoutkaEdiDtl", setDs)

            If bSameKeyFlg = False Then
                ' 前行と差異がある場合

                '---------------------------------------------------------------------------
                ' 入出荷EDIデータ(ヘッダ) の新規追加
                '---------------------------------------------------------------------------
                setDs.Tables("LMH030_H_INOUTKAEDI_HED_FJF").Clear()
                setDs.Tables("LMH030_H_INOUTKAEDI_HED_FJF").ImportRow(ds.Tables("LMH030_H_INOUTKAEDI_HED_FJF").Rows(0))

                ' 新規追加 入出荷EDIデータ(明細) と同一キーレコードの件数取得
                setDs = MyBase.CallDAC(Me._Dac, "GetInoutkaEdiHedCnt", setDs)
                If MyBase.GetResultCount() > 0 Then
                    ' 同一キーレコード存在時の物理削除
                    setDs = MyBase.CallDAC(Me._Dac, "DeleteInoutkaEdiHed", setDs)
                    iRcvHedCanCnt = iRcvHedCanCnt + 1
                End If

                ' 入出荷EDIデータ(明細) の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertInoutkaEdiHed", setDs)
            End If
            '---------------------------------------------------------------------------
            ' スキップフラグが0の場合、EDI出荷データの追加処理を行う
            '---------------------------------------------------------------------------
            If iSkipFlg = 0 Then
                Dim goodsCnt As Integer = 0
                Dim custGoodsCd As String = drEdiRcvDtl.Item("CUST_GOODS_CD").ToString()
                If goodsDict.ContainsKey(custGoodsCd) Then
                    setDs.Tables("LMH030_M_GOODS").Clear()
                    For Each drGoods As DataRow In goodsDict(custGoodsCd).Rows
                        setDs.Tables("LMH030_M_GOODS").ImportRow(drGoods)
                    Next
                    goodsCnt = goodsDict(custGoodsCd).Rows.Count
                ElseIf Not notFoundGoodsCdSet.Contains(custGoodsCd) Then
                    ' 商品マスタ読込処理(商品コードがマスタに存在しない、複数存在する場合エラー。)
                    setDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", setDs)
                    goodsCnt = MyBase.GetResultCount
                End If

                If goodsCnt = 0 Then
                    If Not notFoundGoodsCdSet.Contains(custGoodsCd) Then
                        notFoundGoodsCdSet.Add(custGoodsCd)
                    End If
                    ' 商品マスタに荷主商品コードが存在しない場合はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493",
                                           New String() {String.Concat("商品コード:", drEdiRcvDtl.Item("CUST_GOODS_CD").ToString()), "商品マスタ", ""},
                                           dtSetDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                           LMH030BLC.EXCEL_COLTITLE_SEMIEDI, dtSetDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                    bNoErr = False
                    Continue For
                ElseIf goodsCnt > 1 Then
                    '    ' 商品マスタに荷主商品コードが複数存在する場合はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E975",
                                           New String() {String.Concat("商品コード:", drEdiRcvDtl.Item("CUST_GOODS_CD").ToString())},
                                           dtSetDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                           LMH030BLC.EXCEL_COLTITLE_SEMIEDI, dtSetDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                    bNoErr = False
                    Continue For
                End If

                ' 受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
                setDs = Me.SetSemiOutkaEdiM(setDs, drEdiRcvDtl)

                ' EDI出荷(中)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt = iOutDtlInsCnt + 1

                ' 前行と差異がある場合は、EDI出荷(大)を新規追加
                If bSameKeyFlg = False Then
                    ' 受信DTL⇒EDI出荷(大)へのデータセット
                    setDs = Me.SetSemiOutkaEdiL(setDs, sWhcd, drEdiRcvDtl)

                    ' EDI出荷(大)の新規追加
                    setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                    iOutHedInsCnt = iOutHedInsCnt + 1
                End If

            End If

            ' キーを入れ替えるのはiSkipFlgの値で判断する
            ' ※iSkipFlg = 1の場合、sOldKeyは前行の値である必要があるため 
            If iSkipFlg = 0 Then
                sOldKey = sNewKey   ' OldキーにNewキーをセット
            End If


        Next

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function

#End Region　' "画面取込(セミEDI)データセット＋更新処理"

#Region "データセット設定"

#Region "データセット設定 EDI出荷受信テーブル"

    ''' <summary>
    ''' データセット設定 EDI出荷受信テーブル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="dsWork"></param>
    ''' <param name="i"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <returns></returns>
    Private Function SetSemiOutkaEdiRcv(ByVal ds As DataSet, ByVal dsWork As DataSet, ByVal i As Integer, ByVal drEdiRcvDtl As DataRow) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)


        ' EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_RCV")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = "001"
        drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty                                                            ' 後でセットする。
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty                                                        ' 後でセットする。
        drEdiRcvDtl("OUTKA_CTL_NO") = GetDefCtlNo(dsWork, drSemiEdiInfo("NRS_BR_CD").ToString())
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "001"
        drEdiRcvDtl("CUST_CD_L") = drSemiEdiInfo("CUST_CD_L")                                               ' 荷主コード（大）
        drEdiRcvDtl("CUST_CD_M") = drSemiEdiInfo("CUST_CD_M")                                               ' 荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                         ' プリントフラグ

        drEdiRcvDtl("JUCHU_DENPYO_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 100)         ' 受注伝票番号　　　　　←受信データ(Excel) 受注伝票番号
        drEdiRcvDtl("MEISAI") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 100)                  ' 明細　　　　　　　　　←受信データ(Excel) 明細
        drEdiRcvDtl("JUCHUSAKI_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 100)            ' 受注先　　　　　　　　←受信データ(Excel) 受注先
        drEdiRcvDtl("JUCHUSAKI_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 100)            ' 受注先名称　　　　　　←受信データ(Excel) 受注先名称
        drEdiRcvDtl("DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 15)                  ' 届先コード　　　　　　←受信データ(Excel) 出荷先
        drEdiRcvDtl("DEST_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 80)                  ' 届先名　　　　　　　　←受信データ(Excel) 出荷先名称
        drEdiRcvDtl("HARAI_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 100)                ' 支払人　　　　　　　　←受信データ(Excel) 支払人
        drEdiRcvDtl("HARAI_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 100)                ' 支払人名称　　　　　　←受信データ(Excel) 支払人名称
        drEdiRcvDtl("CUST_GOODS_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 20)            ' 商品コード　　　　　　←受信データ(Excel) 品目コード
        drEdiRcvDtl("GOODS_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 60)                ' 商品名　　　　　　　　←受信データ(Excel) 伝票品目テキスト
        drEdiRcvDtl("GOODS_MST_NM_TEXT") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 100)      ' マスタ品目テキスト　　←受信データ(Excel) マスタ品目テキスト
        drEdiRcvDtl("MEI_KA") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 100)                 ' 明カ　　　　　　　　　←受信データ(Excel) 明カ
        drEdiRcvDtl("LOT_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 40)                  ' ロット番号　　　　　　←受信データ(Excel) ロット
        drEdiRcvDtl("PLNT") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 100)                   ' Plnt　　　　　　　　　←受信データ(Excel) Plnt
        drEdiRcvDtl("SLOC") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 100)                   ' SLoc　　　　　　　　　←受信データ(Excel) SLoc
        drEdiRcvDtl("NONYU_NITTEI_GYO") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 100)       ' 納入日程行　　　　　　←受信データ(Excel) 納入日程行
        drEdiRcvDtl("TOKUI_HACHU_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 100)         ' 得意先発注番号　　　　←受信データ(Excel) 得意先発注番号
        drEdiRcvDtl("SYUKKA_JOKEN") = Me._Blc.LeftB(drSetDtl("COLUMN_18").ToString().Trim(), 100)           ' 出荷条件　　　　　　　←受信データ(Excel) 出荷条件
        drEdiRcvDtl("HAN_TA") = Me._Blc.LeftB(drSetDtl("COLUMN_19").ToString().Trim(), 100)                 ' 販タ　　　　　　　　　←受信データ(Excel) 販タ
        drEdiRcvDtl("TOKU_G1") = Me._Blc.LeftB(drSetDtl("COLUMN_20").ToString().Trim(), 100)                ' 得G1　　　　　　　　　←受信データ(Excel) 得G1
        drEdiRcvDtl("TOKU_G2") = Me._Blc.LeftB(drSetDtl("COLUMN_21").ToString().Trim(), 100)                ' 得G2　　　　　　　　　←受信データ(Excel) 得G2
        drEdiRcvDtl("HIN_G1") = Me._Blc.LeftB(drSetDtl("COLUMN_22").ToString().Trim(), 100)                 ' 品G1　　　　　　　　　←受信データ(Excel) 品G1
        drEdiRcvDtl("HIN_G2") = Me._Blc.LeftB(drSetDtl("COLUMN_23").ToString().Trim(), 100)                 ' 品G2　　　　　　　　　←受信データ(Excel) 品G2
        drEdiRcvDtl("HAISO_BIN_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_24").ToString().Trim(), 100)           ' 配送便　　　　　　　　←受信データ(Excel) 配送便
        drEdiRcvDtl("HAISO_BIN_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_25").ToString().Trim(), 100)           ' 配送便名　　　　　　　←受信データ(Excel) 配送便名
        drEdiRcvDtl("JUCHU_RIYU") = Me._Blc.LeftB(drSetDtl("COLUMN_26").ToString().Trim(), 100)             ' 受注理由　　　　　　　←受信データ(Excel) 受注理由
        drEdiRcvDtl("EIGYOSYO") = Me._Blc.LeftB(drSetDtl("COLUMN_27").ToString().Trim(), 100)               ' 営業所　　　　　　　　←受信データ(Excel) 営業所
        drEdiRcvDtl("HIN_KAISO") = Me._Blc.LeftB(drSetDtl("COLUMN_28").ToString().Trim(), 100)              ' 品目階層　　　　　　　←受信データ(Excel) 品目階層
        drEdiRcvDtl("JIKOKU") = Me._Blc.LeftB(drSetDtl("COLUMN_29").ToString().Trim(), 100)                 ' 時刻　　　　　　　　　←受信データ(Excel) 時刻
        drEdiRcvDtl("TOUROKU_SYA") = Me._Blc.LeftB(drSetDtl("COLUMN_30").ToString().Trim(), 100)            ' 登録者　　　　　　　　←受信データ(Excel) 登録者
        drEdiRcvDtl("TANTO_EIGYO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_31").ToString().Trim(), 100)         ' 担当営業員　　　　　　←受信データ(Excel) 担当営業員
        drEdiRcvDtl("TANTO_EIGYO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_32").ToString().Trim(), 100)         ' 担当営業員名称　　　　←受信データ(Excel) 担当営業員名称
        drEdiRcvDtl("KYOHI_RIYU") = Me._Blc.LeftB(drSetDtl("COLUMN_33").ToString().Trim(), 100)             ' 拒否理由　　　　　　　←受信データ(Excel) 拒否理由
        drEdiRcvDtl("SGRP") = Me._Blc.LeftB(drSetDtl("COLUMN_34").ToString().Trim(), 100)                   ' SGrp　　　　　　　　　←受信データ(Excel) SGrp
        drEdiRcvDtl("MEISAI_TEXT") = Me._Blc.LeftB(drSetDtl("COLUMN_35").ToString().Trim(), 100)            ' 明細テキスト（非表示）←受信データ(Excel) 明細テキスト（非表示）
        drEdiRcvDtl("NEXT_DENPYO") = Me._Blc.LeftB(drSetDtl("COLUMN_36").ToString().Trim(), 100)            ' 後続伝票　　　　　　　←受信データ(Excel) 後続伝票
        drEdiRcvDtl("SEIQTO_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_37").ToString().Trim(), 100)              ' 請求先　　　　　　　　←受信データ(Excel) 請求先
        drEdiRcvDtl("SEIQTO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_38").ToString().Trim(), 100)              ' 請求先名称　　　　　　←受信データ(Excel) 請求先名称
        drEdiRcvDtl("SEIQTO_BUSYO_NM") = Me._Blc.LeftB(drSetDtl("COLUMN_39").ToString().Trim(), 100)        ' 請求先部署名　　　　　←受信データ(Excel) 請求先部署名
        drEdiRcvDtl("SEIQTO_TANTO") = Me._Blc.LeftB(drSetDtl("COLUMN_40").ToString().Trim(), 100)           ' 請求先担当者　　　　　←受信データ(Excel) 請求先担当者
        drEdiRcvDtl("SEIQTO_TEL") = Me._Blc.LeftB(drSetDtl("COLUMN_41").ToString().Trim(), 100)             ' 請求先連絡先　　　　　←受信データ(Excel) 請求先連絡先
        drEdiRcvDtl("TARGET_COMPONENT") = Me._Blc.LeftB(drSetDtl("COLUMN_42").ToString().Trim(), 400)       ' 対象成分　　　　　　　←受信データ(Excel) 対象成分
        drEdiRcvDtl("OUTKA_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_43").ToString().Trim(), 100)        ' 出荷日　　　　　　　　←受信データ(Excel) 出荷日付
        drEdiRcvDtl("ARR_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_44").ToString().Trim(), 100)          ' 納入予定日　　　　　　←受信データ(Excel) 納入日付
        drEdiRcvDtl("SEIKYU_BI") = Me._Blc.LeftB(drSetDtl("COLUMN_45").ToString().Trim(), 100)              ' 請求日　　　　　　　　←受信データ(Excel) 請求日
        drEdiRcvDtl("TOKUI_HACHU_BI") = Me._Blc.LeftB(drSetDtl("COLUMN_46").ToString().Trim(), 100)         ' 得意先発注日付　　　　←受信データ(Excel) 得意先発注日付
        drEdiRcvDtl("OUTKA_TTL_NB") = Me._Blc.LeftB(drSetDtl("COLUMN_47").ToString().Trim(), 10)            ' 出荷総個数　　　　　　←受信データ(Excel) 受注数量
        drEdiRcvDtl("SU_1") = Me._Blc.LeftB(drSetDtl("COLUMN_48").ToString().Trim(), 100)                   ' SU1 　　　　　　　　　←受信データ(Excel) SU
        drEdiRcvDtl("KAKUNIN_SU") = Me._Blc.LeftB(drSetDtl("COLUMN_49").ToString().Trim(), 100)             ' 確認数量　　　　　　　←受信データ(Excel) 確認数量
        drEdiRcvDtl("SU_2") = Me._Blc.LeftB(drSetDtl("COLUMN_50").ToString().Trim(), 100)                   ' SU2 　　　　　　　　　←受信データ(Excel) SU
        drEdiRcvDtl("FUSOKU_SU") = Me._Blc.LeftB(drSetDtl("COLUMN_51").ToString().Trim(), 100)              ' 不足数量　　　　　　　←受信データ(Excel) 不足数量
        drEdiRcvDtl("SU_3") = Me._Blc.LeftB(drSetDtl("COLUMN_52").ToString().Trim(), 100)                   ' SU3 　　　　　　　　　←受信データ(Excel) SU
        drEdiRcvDtl("SYUKKA_SUMI_SU") = Me._Blc.LeftB(drSetDtl("COLUMN_53").ToString().Trim(), 100)         ' 出荷済数量　　　　　　←受信データ(Excel) 出荷済数量
        drEdiRcvDtl("SU_4") = Me._Blc.LeftB(drSetDtl("COLUMN_54").ToString().Trim(), 100)                   ' SU4 　　　　　　　　　←受信データ(Excel) SU
        drEdiRcvDtl("SYOMI_KAKAKU") = Me._Blc.LeftB(drSetDtl("COLUMN_55").ToString().Trim(), 100)           ' 正味価格　　　　　　　←受信データ(Excel) 正味価格
        drEdiRcvDtl("TSUKA_1") = Me._Blc.LeftB(drSetDtl("COLUMN_56").ToString().Trim(), 100)                ' 通貨　　　　　　　　　←受信データ(Excel) 通貨
        drEdiRcvDtl("SYOMI_GAKU") = Me._Blc.LeftB(drSetDtl("COLUMN_57").ToString().Trim(), 100)             ' 正味額　　　　　　　　←受信データ(Excel) 正味額
        drEdiRcvDtl("TSUKA_2") = Me._Blc.LeftB(drSetDtl("COLUMN_58").ToString().Trim(), 100)                ' 通貨2 　　　　　　　　←受信データ(Excel) 通貨
        drEdiRcvDtl("TOUROKU_BI") = Me._Blc.LeftB(drSetDtl("COLUMN_59").ToString().Trim(), 100)             ' 登録日　　　　　　　　←受信データ(Excel) 登録日

        drEdiRcvDtl("CUST_ORD_NO") = ""                                                                     ' 荷主注文番号（全体）　←仕掛品のセミEDI取込段階では、この項目値は未定

        'データセットに設定
        ds.Tables("LMH030_H_OUTKAEDI_DTL_FJF").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region ' "データセット設定 EDI出荷受信テーブル"

#Region "データセット設定 入出荷EDIデータ(明細)"

    ''' <summary>
    ''' データセット設定 入出荷EDIデータ(明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <param name="drInoutkaEdiDtl"></param>
    ''' <returns></returns>
    Private Function SetInoutkaEdiDtl(ByVal ds As DataSet, ByVal drEdiRcvDtl As DataRow, ByVal drInoutkaEdiDtl As DataRow) As DataSet

        ' 入出荷EDIデータDTL設定
        drInoutkaEdiDtl("DEL_KB") = drEdiRcvDtl("DEL_KB")
        drInoutkaEdiDtl("CRT_DATE") = drEdiRcvDtl("CRT_DATE")
        drInoutkaEdiDtl("FILE_NAME") = drEdiRcvDtl("FILE_NAME")
        drInoutkaEdiDtl("REC_NO") = drEdiRcvDtl("REC_NO")
        drInoutkaEdiDtl("GYO") = drEdiRcvDtl("GYO")
        drInoutkaEdiDtl("NRS_BR_CD") = drEdiRcvDtl("NRS_BR_CD")
        drInoutkaEdiDtl("INOUT_KB") = "0"                                       ' 入出荷区分　　　　←“出荷”
        drInoutkaEdiDtl("EDI_CTL_NO") = drEdiRcvDtl("EDI_CTL_NO")
        drInoutkaEdiDtl("EDI_CTL_NO_CHU") = drEdiRcvDtl("EDI_CTL_NO_CHU")
        drInoutkaEdiDtl("INKA_CTL_NO_L") = ""
        drInoutkaEdiDtl("INKA_CTL_NO_M") = ""
        drInoutkaEdiDtl("OUTKA_CTL_NO") = drEdiRcvDtl("OUTKA_CTL_NO")
        drInoutkaEdiDtl("OUTKA_CTL_NO_CHU") = drEdiRcvDtl("OUTKA_CTL_NO_CHU")
        drInoutkaEdiDtl("CUST_CD_L") = drEdiRcvDtl("CUST_CD_L")
        drInoutkaEdiDtl("CUST_CD_M") = drEdiRcvDtl("CUST_CD_M")
        drInoutkaEdiDtl("PRTFLG") = drEdiRcvDtl("PRTFLG")

        drInoutkaEdiDtl("MATNR") = Me._Blc.LeftB(drEdiRcvDtl("CUST_GOODS_CD").ToString(), 18)   ' 品目コード　　　　←EDI出荷受信テーブル.商品コード　　　　　　←受信データ(Excel) 品目コード
        drInoutkaEdiDtl("ZFVYMAKTX1") = Me._Blc.LeftB(drEdiRcvDtl("GOODS_NM").ToString(), 40)   ' 品目テキスト１　　←EDI出荷受信テーブル.商品名　　　　　　　　←受信データ(Excel) 伝票品目テキスト
        drInoutkaEdiDtl("PRODH") = Me._Blc.LeftB(drEdiRcvDtl("HIN_KAISO").ToString(), 18)       ' 品目階層　　　　　←EDI出荷受信テーブル.品目階層　　　　　　　←受信データ(Excel) 品目階層
        drInoutkaEdiDtl("ZFVYHWERKS") = Me._Blc.LeftB(drEdiRcvDtl("PLNT").ToString(), 4)        ' 出庫プラント　　　←EDI出荷受信テーブル.Plnt　　　　　　　　　←受信データ(Excel) Plnt
        drInoutkaEdiDtl("CHARG") = Me._Blc.LeftB(drEdiRcvDtl("LOT_NO").ToString(), 10)          ' ロット番号　　　　←EDI出荷受信テーブル.ロット番号　　　　　　←受信データ(Excel) ロット
        drInoutkaEdiDtl("ZFVYSURYO") = drEdiRcvDtl("OUTKA_TTL_NB")                              ' 数量　　　　　　　←EDI出荷受信テーブル.出荷総個数　　　　　　←受信データ(Excel) 受注数量

        ' 参照伝票番号←
        '   (EDI出荷受信テーブル.受注伝票番号←受信データ(Excel) 受注伝票番号)(前ゼロ付き10桁) &
        '   (EDI出荷受信テーブル.明細←受信データ(Excel) 明細)(前ゼロ付き6桁)
        drInoutkaEdiDtl("REF_DEN_NO") =
            Right(String.Concat(New String("0"c, 10), Me._Blc.LeftB(drEdiRcvDtl("JUCHU_DENPYO_NO").ToString(), 10)), 10) &
            Right(String.Concat(New String("0"c, 6), Me._Blc.LeftB(drEdiRcvDtl("MEISAI").ToString(), 6)), 6)
        drInoutkaEdiDtl("SHIKAKARI_HIN_FLG") = LMH030BLC.FLG_ON.ToString()                      ' 仕掛品フラグ　　　←ON: 仕掛品である

        drInoutkaEdiDtl("JISSEKI_SHORI_FLG") = "1"                                              ' 実績処理フラグ　　←EDI準拠の初期値

        ' データセットに設定
        ds.Tables("LMH030_H_INOUTKAEDI_DTL_FJF").Rows.Add(drInoutkaEdiDtl)

        Return ds

    End Function

#End Region ' "データセット設定 入出荷EDIデータ(明細)"

#Region "データセット設定 入出荷EDIデータ(ヘッダ)"

    ''' <summary>
    ''' データセット設定 入出荷EDIデータ(ヘッダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <param name="drInoutkaEdiHed"></param>
    ''' <returns></returns>
    Private Function SetInoutkaEdiHed(ByVal ds As DataSet, ByVal drEdiRcvDtl As DataRow, ByVal drInoutkaEdiHed As DataRow) As DataSet

        ' 入出荷EDIデータHED設定
        drInoutkaEdiHed("DEL_KB") = drEdiRcvDtl("DEL_KB")
        drInoutkaEdiHed("CRT_DATE") = drEdiRcvDtl("CRT_DATE")
        drInoutkaEdiHed("FILE_NAME") = drEdiRcvDtl("FILE_NAME")
        drInoutkaEdiHed("REC_NO") = drEdiRcvDtl("REC_NO")
        drInoutkaEdiHed("NRS_BR_CD") = drEdiRcvDtl("NRS_BR_CD")
        drInoutkaEdiHed("INOUT_KB") = "0"                                       ' 入出荷区分　　　　←“出荷”
        drInoutkaEdiHed("EDI_CTL_NO") = drEdiRcvDtl("EDI_CTL_NO")
        drInoutkaEdiHed("INKA_CTL_NO_L") = ""
        drInoutkaEdiHed("OUTKA_CTL_NO") = drEdiRcvDtl("OUTKA_CTL_NO")
        drInoutkaEdiHed("CUST_CD_L") = drEdiRcvDtl("CUST_CD_L")
        drInoutkaEdiHed("CUST_CD_M") = drEdiRcvDtl("CUST_CD_M")
        drInoutkaEdiHed("PRTFLG") = drEdiRcvDtl("PRTFLG")

        ' 出荷日←EDI出荷受信テーブル.出荷日←受信データ(Excel) 出荷日付
        ' (日付がシリアル値の場合は DateTime 型を介して YYYYMMDD 形式とする)
        Dim outkaPlanDate As String = Date.Parse(Me.SerialToDate(drEdiRcvDtl("OUTKA_PLAN_DATE").ToString().Trim())).ToString("yyyyMMdd")
        ' 納入予定日←EDI出荷受信テーブル.納入予定日←受信データ(Excel) 納入日付
        ' (日付がシリアル値の場合は DateTime 型を介して YYYYMMDD 形式とする)
        Dim arrPlanDate As String = Date.Parse(Me.SerialToDate(drEdiRcvDtl("ARR_PLAN_DATE").ToString().Trim())).ToString("yyyyMMdd")

        drInoutkaEdiHed("ZFVYSEQ_D") = Me._Blc.LeftB(drEdiRcvDtl("MEISAI").ToString(), 3)           ' 伝票SEQ-NO　　　　←EDI出荷受信テーブル.明細　　　　　　　　　←受信データ(Excel) 明細
        drInoutkaEdiHed("ZFVYLFDAT") = arrPlanDate                                                  ' 納入期日　　　　　←上記 納入予定日
        drInoutkaEdiHed("ZFVYWADAT") = outkaPlanDate                                                ' 入出庫日付　　　　←上記 出荷日
        drInoutkaEdiHed("ZFVYVSBED") = Me._Blc.LeftB(drEdiRcvDtl("SYUKKA_JOKEN").ToString(), 2)     ' 出荷条件　　　　　←EDI出荷受信テーブル.出荷条件　　　　　　　←受信データ(Excel) 出荷条件
        drInoutkaEdiHed("ZFVYTORIC") = Me._Blc.LeftB(drEdiRcvDtl("JUCHUSAKI_CD").ToString(), 10)    ' 取引先　　　　　　←EDI出荷受信テーブル.受注先　　　　　　　　←受信データ(Excel) 受注先
        drInoutkaEdiHed("ZFVYTORINK") = Me._Blc.LeftB(drEdiRcvDtl("JUCHUSAKI_NM").ToString(), 60)   ' 取引先名称（漢字）←EDI出荷受信テーブル.受注先名称　　　　　　←受信データ(Excel) 受注先名称
        drInoutkaEdiHed("ZFVYSYUKKAC") = Me._Blc.LeftB(drEdiRcvDtl("DEST_CD").ToString(), 12)       ' 出荷先　　　　　　←EDI出荷受信テーブル.出荷先　　　　　　　　←受信データ(Excel) 出荷先
        drInoutkaEdiHed("ZFVYSYUKKANK") = Me._Blc.LeftB(drEdiRcvDtl("DEST_NM").ToString(), 60)      ' 出荷先名称（漢字）←EDI出荷受信テーブル.出荷先名称　　　　　　←受信データ(Excel) 出荷先名称
        drInoutkaEdiHed("ZFVYUCOMPANY") = Me._Blc.LeftB(drEdiRcvDtl("HAISO_BIN_CD").ToString(), 2)  ' 運送会社　　　　　←EDI出荷受信テーブル.配送便　　　　　　　　←受信データ(Excel) 配送便

        drInoutkaEdiHed("JISSEKI_SHORI_FLG") = "1"                                                  ' 実績処理フラグ　　←EDI準拠の初期値

        ' データセットに設定
        ds.Tables("LMH030_H_INOUTKAEDI_HED_FJF").Rows.Add(drInoutkaEdiHed)

        Return ds

    End Function

#End Region ' "データセット設定 入出荷EDIデータ(ヘッダ)"

#Region "データセット設定 EDI出荷データ 用 EDI管理番号(大・中)"

    ''' <summary>
    ''' データセット設定 EDI出荷データ 用 EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet, ByVal dsWork As DataSet _
                                , ByVal iDeleteFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer _
                                , ByRef drEdiRcvDtl As DataRow) As DataSet

        Dim sNrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_FJF").Rows(0).Item("NRS_BR_CD").ToString()


        ' 前行とキーが異なる場合　
        If bSameKeyFlg = False Then
            iEdiCtlNoChu = 0    '０クリア    
        End If

        ' EDI管理番号(中)をカウントアップ
        iEdiCtlNoChu = iEdiCtlNoChu + 1

        If iSkipFlg = 0 Then
            ' スキップフラグが０の場合　
            If bSameKeyFlg = False Then
                ' 前行とキーが異なる場合　
                ' EDI管理番号(大)を新規採番してEDI管理番号(中)を"001"採番
                Dim num As New NumberMasterUtility
                sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, sNrsBrCd)
            End If

            ' 登録用EDI管理番号
            drEdiRcvDtl.Item("EDI_CTL_NO") = sEdiCtlNo              ' DTLにセット
            drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")   ' EDI_CHUにセット
        Else
            drEdiRcvDtl.Item("EDI_CTL_NO") = GetDefCtlNo(dsWork, sNrsBrCd)  ' DTLに固定値をセット
            drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = "000"              ' EDI_CHUに固定値をセット
        End If

        Return ds

    End Function

#End Region ' "データセット設定 EDI出荷データ 用 EDI管理番号(大・中)"

#Region "データセット設定(EDI出荷(中)"

    ''' <summary>
    ''' データセット設定(EDI出荷(中)
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <returns></returns>
    Private Function SetSemiOutkaEdiM(ByVal setDs As DataSet, ByVal drEdiRcvDtl As DataRow) As DataSet

        Dim drOutkaEdiM As DataRow = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()
        Dim drGoods As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        drOutkaEdiM("DEL_KB") = "0"
        drOutkaEdiM("NRS_BR_CD") = drEdiRcvDtl("NRS_BR_CD")
        drOutkaEdiM("EDI_CTL_NO") = drEdiRcvDtl("EDI_CTL_NO")
        drOutkaEdiM("EDI_CTL_NO_CHU") = drEdiRcvDtl("EDI_CTL_NO_CHU")

        drOutkaEdiM("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiM("OUTKA_CTL_NO_CHU") = String.Empty
        drOutkaEdiM("COA_YN") = String.Empty
        drOutkaEdiM("CUST_ORD_NO_DTL") = drEdiRcvDtl("CUST_ORD_NO")                 ' 荷主注文番号（明細単位）←EDI出荷受信テーブル.荷主注文番号（全体）　←設定元項目未定
        ' 参照伝票番号←
        '   (EDI出荷受信テーブル.受注伝票番号←受信データ(Excel) 受注伝票番号)(前ゼロ付き10桁) &
        '   (EDI出荷受信テーブル.明細←受信データ(Excel) 明細)(前ゼロ付き6桁)
        Dim refDenNo As String = String.Concat(
            drEdiRcvDtl("JUCHU_DENPYO_NO").ToString().PadLeft(10, "0"c),
            drEdiRcvDtl("MEISAI").ToString().PadLeft(6, "0"c))
        drOutkaEdiM("BUYER_ORD_NO_DTL") = refDenNo                                  ' 買主注文番号（明細単位）←参照伝票番号
        drOutkaEdiM("CUST_GOODS_CD") = drEdiRcvDtl("CUST_GOODS_CD")                 ' 商品コード　　　　　　　←EDI出荷受信テーブル.商品コード　　　　　　←受信データ(Excel) 品目コード
        drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        drOutkaEdiM("GOODS_NM") = drEdiRcvDtl("GOODS_NM")                           ' 商品名　　　　　　　　　←EDI出荷受信テーブル.商品名　　　　　　　　←受信データ(Excel) 伝票品目テキスト

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = drEdiRcvDtl("LOT_NO")                               ' ロット№　　　　　　　　←EDI出荷受信テーブル.ロット番号　　　　　　←受信データ(Excel) ロット
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = "01"

        ' 出荷総個数←EDI出荷受信テーブル.出荷総個数←受信データ(Excel) 受注数量
        Dim outkaTtlNb As String = drEdiRcvDtl("OUTKA_TTL_NB").ToString()

        drOutkaEdiM("OUTKA_PKG_NB") = 0
        drOutkaEdiM("OUTKA_HASU") = outkaTtlNb                                      ' 出荷端数　　　　　　　　←上記 出荷総個数
        drOutkaEdiM("OUTKA_QT") = 0
        drOutkaEdiM("OUTKA_TTL_NB") = outkaTtlNb                                    ' 出荷総個数　　　　　　　←上記 出荷総個数
        If String.IsNullOrEmpty(drGoods("STD_IRIME_NB").ToString()) Then
            drOutkaEdiM("OUTKA_TTL_QT") = 0
        Else
            ' 出荷総数量 = 出荷総個数 * 入目
            drOutkaEdiM("OUTKA_TTL_QT") = Convert.ToDecimal(outkaTtlNb) * Convert.ToDecimal(drGoods("STD_IRIME_NB"))
        End If

        drOutkaEdiM("KB_UT") = drGoods("NB_UT")
        drOutkaEdiM("QT_UT") = drGoods("STD_IRIME_UT")
        drOutkaEdiM("PKG_NB") = drGoods("PKG_NB")
        drOutkaEdiM("PKG_UT") = drGoods("PKG_UT")
        drOutkaEdiM("ONDO_KB") = drGoods("ONDO_KB")
        drOutkaEdiM("UNSO_ONDO_KB") = drGoods("UNSO_ONDO_KB")

        drOutkaEdiM("IRIME") = drGoods("STD_IRIME_NB")

        ' EDIの処理準拠
        ' 入目単位←商品マスタ.標準入目単位
        Dim stdIrimeUt As String = drGoods("STD_IRIME_UT").ToString()
        If drOutkaEdiM("NRS_BR_CD").ToString() = "96" AndAlso drOutkaEdiM("CUST_GOODS_CD").ToString().Substring(0, 3) = "243" Then
            ' ただし大分工場で原料(商品コード上 3桁が "243") の場合は "KG" 決め打ち
            stdIrimeUt = "KG"
        End If
        drOutkaEdiM("IRIME_UT") = stdIrimeUt

        drOutkaEdiM("BETU_WT") = 0
        drOutkaEdiM("REMARK") = String.Empty
        drOutkaEdiM("OUT_KB") = "0"
        drOutkaEdiM("AKAKURO_KB") = "0"
        drOutkaEdiM("JISSEKI_FLAG") = "0"
        drOutkaEdiM("JISSEKI_USER") = String.Empty
        drOutkaEdiM("JISSEKI_DATE") = String.Empty
        drOutkaEdiM("JISSEKI_TIME") = String.Empty
        drOutkaEdiM("SET_KB") = String.Empty
        drOutkaEdiM("FREE_N01") = 0
        drOutkaEdiM("FREE_N02") = 0
        drOutkaEdiM("FREE_N03") = 0
        drOutkaEdiM("FREE_N04") = 0
        drOutkaEdiM("FREE_N05") = 0
        drOutkaEdiM("FREE_N06") = 0
        drOutkaEdiM("FREE_N07") = 0
        drOutkaEdiM("FREE_N08") = 0
        drOutkaEdiM("FREE_N09") = 0
        drOutkaEdiM("FREE_N10") = 0
        drOutkaEdiM("FREE_C01") = String.Empty
        drOutkaEdiM("FREE_C02") = String.Empty
        drOutkaEdiM("FREE_C03") = String.Empty
        drOutkaEdiM("FREE_C04") = String.Empty
        drOutkaEdiM("FREE_C05") = String.Empty
        drOutkaEdiM("FREE_C06") = String.Empty
        drOutkaEdiM("FREE_C07") = String.Empty
        drOutkaEdiM("FREE_C08") = String.Empty
        drOutkaEdiM("FREE_C09") = String.Empty
        drOutkaEdiM("FREE_C10") = String.Empty
        drOutkaEdiM("FREE_C11") = String.Empty
        drOutkaEdiM("FREE_C12") = String.Empty
        drOutkaEdiM("FREE_C13") = String.Empty
        drOutkaEdiM("FREE_C14") = String.Empty
        drOutkaEdiM("FREE_C15") = drEdiRcvDtl("CRT_DATE")                           ' 文字列15　　　　　　　　←EDI出荷受信テーブル.データ受信日
        drOutkaEdiM("FREE_C16") = drEdiRcvDtl("FILE_NAME")                          ' 文字列16　　　　　　　　←EDI出荷受信テーブル.受信ファイル名
        drOutkaEdiM("FREE_C17") = drEdiRcvDtl("REC_NO")                             ' 文字列17　　　　　　　　←EDI出荷受信テーブル.受信ファイル行数
        drOutkaEdiM("FREE_C18") = drEdiRcvDtl("GYO")                                ' 文字列18　　　　　　　　←EDI出荷受信テーブル.行数
        drOutkaEdiM("FREE_C19") = String.Empty
        drOutkaEdiM("FREE_C20") = String.Empty
        drOutkaEdiM("FREE_C21") = String.Empty
        drOutkaEdiM("FREE_C22") = drEdiRcvDtl("GOODS_NM")                           ' 文字列23　　　　　　　　←EDI出荷受信テーブル.商品名　　　　　　　　←受信データ(Excel) 伝票品目テキスト
        drOutkaEdiM("FREE_C23") = drEdiRcvDtl("JUCHU_DENPYO_NO")                    ' 文字列24　　　　　　　　←EDI出荷受信テーブル.受注伝票番号　　　　　←受信データ(Excel) 受注伝票番号
        drOutkaEdiM("FREE_C24") = drEdiRcvDtl("MEISAI")                             ' 文字列25　　　　　　　　←EDI出荷受信テーブル.明細　　　　　　　　　←受信データ(Excel) 明細
        drOutkaEdiM("FREE_C25") = String.Empty
        drOutkaEdiM("FREE_C26") = String.Empty
        drOutkaEdiM("FREE_C27") = String.Empty
        drOutkaEdiM("FREE_C28") = String.Empty
        drOutkaEdiM("FREE_C29") = String.Empty
        drOutkaEdiM("FREE_C30") = String.Empty

        ' データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(drOutkaEdiM)

        Return setDs

    End Function

#End Region ' "データセット設定(EDI出荷(中)"

#Region "データセット設定(EDI出荷(大))"

    ''' <summary>
    ''' データセット設定(EDI出荷(大))
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <param name="sWhCd"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiL(ByVal setDs As DataSet _
                                    , ByVal sWhCd As String _
                                    , ByVal drEdiRcvDtl As DataRow) As DataSet

        Dim drOutkaEdiL As DataRow = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        Dim drDest As DataRow = setDs.Tables("LMH030_M_DEST").Rows(0)

        drOutkaEdiL("DEL_KB") = "0"
        drOutkaEdiL("NRS_BR_CD") = drEdiRcvDtl("NRS_BR_CD")
        drOutkaEdiL("EDI_CTL_NO") = drEdiRcvDtl("EDI_CTL_NO")
        drOutkaEdiL("OUTKA_CTL_NO") = String.Empty
        drOutkaEdiL("OUTKA_KB") = "10"
        drOutkaEdiL("SYUBETU_KB") = "10"
        drOutkaEdiL("NAIGAI_KB") = "01"
        drOutkaEdiL("OUTKA_STATE_KB") = "10"
        drOutkaEdiL("OUTKAHOKOKU_YN") = "0"
        drOutkaEdiL("PICK_KB") = "01"
        drOutkaEdiL("NRS_BR_NM") = String.Empty
        drOutkaEdiL("WH_CD") = sWhCd
        drOutkaEdiL("WH_NM") = String.Empty

        ' 出荷日←EDI出荷受信テーブル.出荷日←受信データ(Excel) 出荷日付
        ' (日付がシリアル値の場合は DateTime 型を介して YYYYMMDD 形式とする)
        Dim outkaPlanDate As String = Date.Parse(Me.SerialToDate(drEdiRcvDtl("OUTKA_PLAN_DATE").ToString().Trim())).ToString("yyyyMMdd")
        ' 納入予定日←EDI出荷受信テーブル.納入予定日←受信データ(Excel) 納入日付
        ' (日付がシリアル値の場合は DateTime 型を介して YYYYMMDD 形式とする)
        Dim arrPlanDate As String = Date.Parse(Me.SerialToDate(drEdiRcvDtl("ARR_PLAN_DATE").ToString().Trim())).ToString("yyyyMMdd")
        drOutkaEdiL("OUTKA_PLAN_DATE") = outkaPlanDate                              ' 出荷日　　　　　　　←上記 出荷日
        drOutkaEdiL("OUTKO_DATE") = outkaPlanDate                                   ' 出庫日　　　　　　　←上記 出荷日
        drOutkaEdiL("ARR_PLAN_DATE") = arrPlanDate                                  ' 納入予定日　　　　　←上記 納入予定日
        drOutkaEdiL("ARR_PLAN_TIME") = String.Empty

        drOutkaEdiL("HOKOKU_DATE") = String.Empty

        drOutkaEdiL("TOUKI_HOKAN_YN") = "1"
        drOutkaEdiL("CUST_CD_L") = drEdiRcvDtl("CUST_CD_L")
        drOutkaEdiL("CUST_CD_M") = drEdiRcvDtl("CUST_CD_M")
        drOutkaEdiL("CUST_NM_L") = String.Empty
        drOutkaEdiL("CUST_NM_M") = String.Empty

        drOutkaEdiL("SHIP_CD_L") = String.Empty
        drOutkaEdiL("SHIP_CD_M") = String.Empty
        drOutkaEdiL("SHIP_NM_L") = String.Empty
        drOutkaEdiL("SHIP_NM_M") = String.Empty

        ' 届先コード←EDI出荷受信テーブル.届先コード←受信データ(Excel) 出荷先
        Dim destCd As String = drEdiRcvDtl("DEST_CD").ToString()
        drOutkaEdiL("EDI_DEST_CD") = destCd                                         ' EDI届先コード 　　　←上記 届先コード
        drOutkaEdiL("DEST_CD") = destCd                                             ' 届先コード　　　　　←上記 届先コード
        drOutkaEdiL("DEST_NM") = drEdiRcvDtl("DEST_NM")                             ' 届先名　　　　　　　←EDI出荷受信テーブル.届先名　　　　　　　　←受信データ(Excel) 出荷先名称
        drOutkaEdiL("DEST_ZIP") = drDest("ZIP")                                     ' 届先郵便番号　　　　←届先マスタ.郵便番号

        ' 届先住所1～3 ← 届先マスタ.住所1～3
        drOutkaEdiL("DEST_AD_1") = drDest("AD_1")
        drOutkaEdiL("DEST_AD_2") = drDest("AD_2")
        drOutkaEdiL("DEST_AD_3") = drDest("AD_3")
        drOutkaEdiL("DEST_AD_4") = String.Empty
        drOutkaEdiL("DEST_AD_5") = String.Empty

        drOutkaEdiL("DEST_TEL") = drDest("TEL")                                     ' 届先電話番号　　　　←届先マスタ.電話番号
        drOutkaEdiL("DEST_FAX") = drDest("FAX")                                     ' 届先FAX 　　　　　　←届先マスタ.FAX番号
        drOutkaEdiL("DEST_MAIL") = String.Empty
        drOutkaEdiL("DEST_JIS_CD") = drDest("JIS")                                  ' 届先JISコード 　　　←届先マスタ.JISコード
        drOutkaEdiL("SP_NHS_KB") = String.Empty
        drOutkaEdiL("COA_YN") = String.Empty
        drOutkaEdiL("CUST_ORD_NO") = drEdiRcvDtl("CUST_ORD_NO")                     ' 荷主注文番号（全体）←EDI出荷受信テーブル.荷主注文番号（全体）　←設定元項目未定
        Dim juchuDenpyoNo As String = drEdiRcvDtl("JUCHU_DENPYO_NO").ToString().PadLeft(10, "0"c)
        drOutkaEdiL("BUYER_ORD_NO") = juchuDenpyoNo                                 ' 買主注文番号（全体）←EDI出荷受信テーブル.受注伝票番号　　　　　←受信データ(Excel) 受注伝票番号)(前ゼロ付き10桁)
        drOutkaEdiL("UNSO_MOTO_KB") = String.Empty
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty
        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("BIN_KB") = "01"
        drOutkaEdiL("UNSO_CD") = drEdiRcvDtl("HAISO_BIN_CD")                        ' 運送会社コード　　　←EDI出荷受信テーブル.配送便　　　　　　　　←受信データ(Excel) 配送便
        drOutkaEdiL("UNSO_NM") = String.Empty

        ' 運送会社支店コード←固定値 "00"
        ' ただし運送会社コードに値がない場合は "" 
        Dim unsoBrCd As String = "00"
        If drOutkaEdiL("UNSO_CD").ToString().Trim().Length = 0 Then
            unsoBrCd = ""
        End If
        drOutkaEdiL("UNSO_BR_CD") = unsoBrCd                                        ' 運送会社支店コード　←上記 運送会社支店コード

        drOutkaEdiL("UNSO_BR_NM") = String.Empty
        drOutkaEdiL("UNCHIN_TARIFF_CD") = String.Empty
        drOutkaEdiL("EXTC_TARIFF_CD") = String.Empty
        drOutkaEdiL("REMARK") = String.Empty
        drOutkaEdiL("UNSO_ATT") = String.Empty
        drOutkaEdiL("DENP_YN") = String.Empty
        drOutkaEdiL("PC_KB") = String.Empty
        drOutkaEdiL("UNCHIN_YN") = String.Empty
        drOutkaEdiL("NIYAKU_YN") = String.Empty
        drOutkaEdiL("OUT_FLAG") = "0"
        drOutkaEdiL("AKAKURO_KB") = "0"
        drOutkaEdiL("JISSEKI_FLAG") = "0"
        drOutkaEdiL("JISSEKI_USER") = String.Empty
        drOutkaEdiL("JISSEKI_DATE") = String.Empty
        drOutkaEdiL("JISSEKI_TIME") = String.Empty

        drOutkaEdiL("FREE_N01") = 0
        drOutkaEdiL("FREE_N02") = 0
        drOutkaEdiL("FREE_N03") = 0
        drOutkaEdiL("FREE_N04") = 0
        drOutkaEdiL("FREE_N05") = 0
        drOutkaEdiL("FREE_N06") = 0
        drOutkaEdiL("FREE_N07") = 0
        drOutkaEdiL("FREE_N08") = 0
        drOutkaEdiL("FREE_N09") = 0
        drOutkaEdiL("FREE_N10") = 0

        drOutkaEdiL("FREE_C01") = String.Empty
        drOutkaEdiL("FREE_C02") = String.Empty
        drOutkaEdiL("FREE_C03") = String.Empty
        drOutkaEdiL("FREE_C04") = String.Empty
        drOutkaEdiL("FREE_C05") = String.Empty
        drOutkaEdiL("FREE_C06") = String.Empty
        drOutkaEdiL("FREE_C07") = String.Empty
        drOutkaEdiL("FREE_C08") = String.Empty
        drOutkaEdiL("FREE_C09") = String.Empty
        drOutkaEdiL("FREE_C10") = String.Empty
        drOutkaEdiL("FREE_C11") = String.Empty
        drOutkaEdiL("FREE_C12") = String.Empty
        drOutkaEdiL("FREE_C13") = String.Empty
        drOutkaEdiL("FREE_C14") = String.Empty
        drOutkaEdiL("FREE_C15") = String.Empty
        drOutkaEdiL("FREE_C16") = String.Empty
        drOutkaEdiL("FREE_C17") = String.Empty
        drOutkaEdiL("FREE_C18") = String.Empty
        drOutkaEdiL("FREE_C19") = String.Empty
        drOutkaEdiL("FREE_C20") = String.Empty
        drOutkaEdiL("FREE_C21") = String.Empty
        drOutkaEdiL("FREE_C22") = String.Empty
        drOutkaEdiL("FREE_C23") = drEdiRcvDtl("DEST_NM")                            ' 文字列23　　　　　　←EDI出荷受信テーブル.届先名　　　　　　　　←受信データ(Excel) 出荷先名称
        drOutkaEdiL("FREE_C24") = String.Empty
        drOutkaEdiL("FREE_C25") = String.Empty
        drOutkaEdiL("FREE_C26") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C27") = String.Empty
        drOutkaEdiL("FREE_C28") = String.Empty
        drOutkaEdiL("FREE_C29") = String.Empty
        drOutkaEdiL("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutkaEdiL)
        Return setDs


    End Function

#End Region ' "データセット設定(EDI出荷(大))"

#End Region ' "データセット設定"

#End Region　' "画面取込(セミEDI)データセット＋更新処理 および関連処理"

#Region "画面取込(セミEDI) チェック / 更新処理 共通処理"

    ''' <summary>
    ''' シリアル値の可能性のある文字列の日付型文字列への変換
    ''' </summary>
    ''' <param name="sDate"></param>
    ''' <returns>変換できない場合はそのままの値を返す</returns>
    Private Function SerialToDate(ByVal sDate As String) As String

        Dim ret As String = sDate

        If IsNumeric(sDate) AndAlso
            sDate.IndexOf("/") < 0 AndAlso
            sDate.IndexOf(".") < 0 AndAlso
            sDate.IndexOf(",") < 0 AndAlso
            sDate.IndexOf("-") < 0 AndAlso
            sDate.IndexOf("+") < 0 Then
            Dim decDate As Decimal = 0
            If Decimal.TryParse(sDate, Globalization.NumberStyles.Number, Globalization.CultureInfo.InstalledUICulture, decDate) Then
                If 1D <= decDate AndAlso decDate <= 2958465D Then
                    ret = DateTime.FromOADate(decDate).ToString("yyyy/MM/dd")
                End If
            End If
        End If

        Return ret

    End Function

    Private Function GetDefCtlNo(ByVal dsWork As DataSet, ByVal nrsBrCd As String) As String

        If dsWork.Tables("LMH030_Z_KBN_OUT").Rows.Count = 0 Then
            dsWork.Tables("LMH030_OUTKAEDI_L").Clear()
            Dim drOutEdiL As DataRow = dsWork.Tables("LMH030_OUTKAEDI_L").NewRow()
            drOutEdiL.Item("NRS_BR_CD") = nrsBrCd
            dsWork.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutEdiL)

            dsWork.Tables("LMH030_Z_KBN_IN").Clear()
            Dim drZKbnIn As DataRow = dsWork.Tables("LMH030_Z_KBN_IN").NewRow()
            drZKbnIn.Item("KBN_GROUP_CD") = "D003"
            dsWork.Tables("LMH030_Z_KBN_IN").Rows.Add(drZKbnIn)

            Call MyBase.CallDAC(Me._DacCom, "SelectZKbnHanyo", dsWork)
        End If

        Dim drKbnD003 As DataRow() = dsWork.Tables("LMH030_Z_KBN_OUT").Select("KBN_NM1 = '" & nrsBrCd & "'")

        Dim kigo As String = " "
        If drKbnD003.Length > 0 Then
            kigo = drKbnD003(0).Item("KBN_NM6").ToString()
        End If

        Dim ret As String = String.Concat(kigo, New String("0"c, 8))

        Return ret

    End Function

#End Region ' "画面取込(セミEDI) チェック / 更新処理 共通処理"

#End Region ' "画面取込(セミEDI) 関連処理"

#Region "出荷登録処理"
    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        '↓FFEM特殊処理↓
        '実績データのキャンセル報告対応
        '2014.06.09 追加START
        Dim canHokokuF As Boolean = False
        Dim drInOut As DataRow = ds.Tables("LMH030INOUT").Rows(0)
        '2014.06.09 追加END

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
            '↓FFEM特殊処理↓
            '2014.06.09 追加START
        ElseIf ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 1 AndAlso _
            ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("DEL_KB").ToString() = "2" Then


            ''出荷登録時。。キャンセルデータを未登録から移動するため。
            'ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("JISSEKI_FLAG") = "1"
            canHokokuF = True
            '↑FFEM特殊処理↑
            '2014.06.09 追加END
        End If

        '要望番号1282 追加START 2012.08.21
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails2", ds)

        '汎用届先自動追加フラグの取得
        Dim genericInsFlg As Integer = 0
        If ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count > 0 Then
            genericInsFlg = Me.GenericDestCheck(ds)
        End If
        '要望番号1282 追加END 2012.08.21

        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        'EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_ORD_NO").ToString) = False Then

            If drInOut("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds
                End If

            End If

        End If

#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山) 
        If "96".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "98".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) Then
            'FFEM大分工場(営業所コード='96') or 大牟田工場(営業所コード='98')の場合
#Else
        If "96".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "98".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "F1".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) _
            OrElse "F2".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) _
            OrElse "F3".Equals(ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD")) Then   'ADD 2022 / 10 / 19 033380   【LMS】FFEM足柄工場LMS導入 F2追加    'ADD 2023/12/25 039659 F3 追加
            'FFEM大分工場(営業所コード='96') or 大牟田工場(営業所コード='98' or 安田倉庫(営業所コード='F1') or 足柄工場(営業所コード='F2') or 熊本工場(営業所コード='F3')の場合
#End If


            Dim outkaEdiMRow As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)

            '区分マスタF029(保管場所コード(FFEM))より対応する倉庫コードを取得
            Dim zKbnInDt As DataTable = ds.Tables("LMH030_Z_KBN_IN")
            Dim zKbnOutDt As DataTable = ds.Tables("LMH030_Z_KBN_OUT")
            zKbnInDt.Clear()
            zKbnOutDt.Clear()
            Dim zKbnInDr As DataRow = zKbnInDt.NewRow
#If False Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様） 
            zKbnInDr.Item("KBN_GROUP_CD") = "F029"
            zKbnInDr.Item("KBN_NM1") = outkaEdiMRow.Item("FREE_C04").ToString   '保管場所(LGORT)

#Else
            zKbnInDr.Item("KBN_GROUP_CD") = "F030"
            zKbnInDr.Item("KBN_NM1") = outkaEdiMRow.Item("FREE_C02").ToString   'プラントコード
            zKbnInDr.Item("KBN_NM2") = outkaEdiMRow.Item("FREE_C04").ToString   '保管場所(LGORT)
            zKbnInDr.Item("KBN_NM4") = outkaEdiMRow.Item("NRS_BR_CD").ToString   '営業所コード

#End If
            zKbnInDt.Rows.Add(zKbnInDr)

            ds = MyBase.CallDAC(Me._DacCom, "SelectZKbnHanyo", ds)

            If zKbnOutDt.Rows.Count >= 1 Then
                '区分マスタより取得した倉庫コードを設定
#If False Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様）
                ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("WH_CD") = zKbnOutDt.Rows(0).Item("KBN_NM4").ToString
#Else
                ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("WH_CD") = zKbnOutDt.Rows(0).Item("KBN_NM5").ToString
#End If
            Else
                '区分マスタに該当なしの場合、固定値を設定
                ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("WH_CD") = "XXX"
            End If
        End If


        '↓FFEM特殊処理↓
        '2014.06.09 追加START
        If canHokokuF = True Then

            'データセット設定処理(受信ヘッダ)
            ds = Me.SetDatasetEdiRcvHed(ds, canHokokuF)

            'データセット設定処理(受信明細)
            ds = Me.SetDatasetEdiRcvDtl(ds, canHokokuF)

            '①キャンセルデータ実績作成処理
            If Me.JissekiSakusei(ds) = False Then
                Return ds
            End If

            'Dim rtDs As DataSet = New LMC020DS()
            'Dim rtDt As DataTable = rtDs.Tables("LMC020IN")
            'Dim dr As DataRow = rtDt.NewRow()
            'dr.Item("NRS_BR_CD") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()                     '営業所コード
            'dr.Item("OUTKA_NO_L") = ds.Tables("LMH030_H_SENDOUTEDI_FJF").Rows(0).Item("OUTKA_CTL_NO").ToString()           '管理番号
            'dr.Item("WH_CD") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("WH_CD").ToString()                             '倉庫コード
            'dr.Item("CUST_CD_L") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_CD_L").ToString()                     '荷主コード（大）
            'dr.Item("CUST_CD_M") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_CD_M").ToString()                     '荷主コード（中）
            'dr.Item("CUST_NM") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_NM_L").ToString()                       '荷主名
            'rtDt.Rows.Add(dr)

            ''②出荷データ(元黒)の抽出処理
            'rtDs = Me.SelectInitData(rtDs)



            'If rtDs.Tables("LMC020_OUTKA_L").Rows.Count > 0 Then
            '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return ds
            '    Exit Function
            'End If


            ''③出荷データ(元黒)の削除処理
            'If Me.DeleteData(rtDs) = False Then
            '    Return ds
            'End If

            Return ds
        End If
        '↑FFEM特殊処理↑
        '2014.06.09 追加END

        'EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '要望番号1282 追加START 2012.08.21
        'EDI出荷(大)の初期値設定後のDB存在チェック
        'If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo, genericInsFlg) = False Then
            Return ds
        End If
        '要望番号1282 追加End 2012.08.21

        '届先コードの初期値設定
        ds = Me.SetDestCd(ds)

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False


        '富士フイルムはまとめ対象外荷主(共通のまとめSQLを使用)
        '基本、自動まとめフラグは"9"なのでここには入らない
        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            'まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                'まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.FJF_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'まとめ対象だったデータを出したい場合はコメントをはずす
                    'Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.FJF_WID_L001, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True
                End If
                ''まとめ先が複数件の場合、エラー
                'matomeNo = Me.matomesakiOutkaNo(ds)
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E427", New String() {matomeNo}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'Return ds
            ElseIf autoMatomeF.Equals("0") = True Then

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.FJF_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.FJF_WID_L002, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds

                End If

            ElseIf autoMatomeF.Equals("1") = True Then
                Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

                Dim matomeStatus As String = dtMatome.Rows(0).Item("OUTKA_STATE_KB").ToString()

                If matomeStatus.Equals("10") = False Then

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.FJF_WID_L003, 0)

                    '進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.FJF_WID_L003, ds, msgArray, matomeNo, String.Empty)
                        Return ds

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時
                        '自動まとめ処理を行う
                        matomeFlg = True

                    End If

                Else
                    'まとめ処理を行う
                    matomeFlg = True
                End If

            End If
        End If

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds, matomeFlg)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds, matomeFlg)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        'ADD Start 2016/07/26 ediデータの届け先telが未設定時は、M_TOKUI_FJFのtel取得
        ds.Tables("LMH030_M_TOKUI_FJF").Clear()

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        If String.IsNullOrEmpty(ediDr("DEST_TEL").ToString) = True Then
            'M_TOKUI_FJFのtel値取得
            ds = MyBase.CallDAC(Me._Dac, "SelectM_TOKUI_FJF", ds)

        End If
        'ADD End   2016/07/26 ediデータの届け先telが未設定時は、M_TOKUI_FJFのtel取得

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

        If IsMaterialPlantTransfer(ds) Then
            '原料プラント間転送の場合

            '出荷(小)データセット設定
            ds = Me.SetDatasetOutkaS(ds)

            '在庫データ データセット設定
            ds = Me.SetDatasetZaiTrs(ds)
        End If

        '富士フイルム 追加箇所 terakawa 2012.08.06 Start
        'EDI受信テーブル(HED)データセット設定
        ds = Me.SetDatasetEdiRcvHed(ds)
        '富士フイルム 追加箇所 terakawa 2012.08.06 End

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds, matomeFlg)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds, matomeFlg)

        'タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        '出荷登録(通常処理)
        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        '富士フイルム 追加箇所 terakawa 2012.08.06 Start
        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        '富士フイルム 追加箇所 terakawa 2012.08.06 End

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        If matomeFlg = False Then
            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        Else
            '出荷(大)のまとめ更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        If IsMaterialPlantTransfer(ds) Then
            '原料プラント間転送の場合

            '出荷(小)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaSData", ds)

            '在庫データの新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertZarTrsData", ds)
        End If

        '富士フイルムの場合は自動追加・更新を行う予定
        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの更新
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
        End If
        '届先マスタの更新

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            If matomeFlg = False Then
                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
            Else
                ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
            End If
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        If matomeFlg = True Then
            'まとめ先EDI出荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        Return ds

    End Function
#End Region

#Region "実績作成処理"

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As Boolean

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim updFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_L_UPD_FLG").ToString()

        'Dim msgArray(5) As String
        'Dim choiceKb As String = String.Empty

        '富士フイルムEDI実績の値設定(EDI受信TBLより)
        ds = MyBase.CallDAC(Me._Dac, "SelectFjfEdiSend", ds)

        If MyBase.GetResultCount > 0 Then
            '実績作成していなくてもエラーにしない仕様に変更
            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI送信テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'Return False
            '富士フイルムEDI実績データの作成
            ds = MyBase.CallDAC(Me._Dac, "InsertFjfEdiSendData", ds)

        End If

        '処理フラグを強制的に"2"(実績作成)に変換
        ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU") = "2"

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return True

    End Function

#End Region

    '↓FFEM特殊処理↓
    '2014.06.09 使用START
#Region "元黒データ削除処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '出荷(大)のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectOutkaLData", ds)

        '出荷(中)のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectOutkaMData", ds)

        ''出荷(小)、在庫のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectOutkaSData", ds)

        '作業のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectSagyoData", ds)

        '運送(大)のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectUnsoLData", ds)

        '在庫のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectZaiData", ds)

        'Maxシーケンスのデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectMaxNoData", ds)

        'EDI出荷(大)のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectEDILData", ds)

        '2014/01/22 輸出情報追加 START
        '輸出情報のデータ取得
        ds = MyBase.CallDAC(Me._MotoDelDac, "SelectExportLData", ds)
        '2014/01/22 輸出情報追加 END

        Return ds

    End Function

#End Region

#Region "元黒データ削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        If String.IsNullOrEmpty(ds.Tables("LMC020_OUTKA_L").Rows(0).Item("FURI_NO").ToString()) = False Then

            Dim FuriDelrtnDs As DataSet = Me.SetFuriDelDataSet(ds, 3)

            'SetFuriDelDataSetの結果をMe._DsにCOPY
            Me._Ds = FuriDelrtnDs.Copy

            '★★★★★★★★★★★★★★
            Dim rtnResult As Boolean = True

            rtnResult = rtnResult AndAlso Me.IsDeleteChk(Me._Ds)

            'エラーの場合、終了
            If rtnResult = False Then

                '処理終了アクション
                Return False
            End If
            '★★★★★★★★★★★★

            '完了取消データ削除(以下、完了取り消しから流用)
            '出荷データ(大)のDataSet更新
            Call Me.SetTK_OUT_L(Me._Ds)

            '作業のDataSet更新
            Call Me.SetTK_SAGYO(Me._Ds)

            '運送のDataSet更新
            Call Me.SetTK_UNSO(Me._Ds)

            '在庫データのDataSetに削除する出荷データ(小)の引当個数分を戻す(完了取消時の戻し)
            Dim ZairtnDs As DataSet = Me.SetZaiRtn(Me._Ds, 5)
        Else

            Me._Ds = ds.Copy
        End If

        Dim rtnDs As DataSet = Nothing
        Dim copyDs As DataSet = Me._Ds.Copy
        '在庫データのDataSetに削除する出荷データ(小)の引当個数分を戻す
        rtnDs = Me.SetZaiRtn(Me._Ds, 3)

        '出荷データ(大)のDataSetに削除フラグを設定
        Call Me.SetDelFlg("LMC020_OUTKA_L", Me._Ds)

        '出荷データ(中)のDataSetに削除フラグを設定
        Call Me.SetDelFlg("LMC020_OUTKA_M", Me._Ds)

        '出荷データ(小)のDataSetに削除フラグを設定
        Call Me.SetDelFlg("LMC020_OUTKA_S", Me._Ds)

        '作業のDataSetに削除フラグを設定
        Call Me.SetDelFlg("LMC020_SAGYO", Me._Ds)

        '運送(大)のDataSetに削除フラグを設定
        Call Me.SetDelFlg("LMC020_UNSO_L", Me._Ds)
        '※運送(中)と運賃テーブルに関しては削除フラグを設定しなくても削除される

        '請求鑑チェック用データセット設定
        Call Me.SetKagamiDataSet(Me._Ds, 5)

        '更新処理
        Call Me.BlcDeleteData(Me._Ds)

    End Function

    '↓FFEM特殊処理↓
    '2014.06.09 使用END

#Region "実績作成時同一まとめレコード取得処理"
    ''' <summary>
    ''' 実績作成時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>富士フイルムは現状まとめ処理はないが、切り替えた時の為に記載</remarks>
    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatome", ds)

        Return ds

    End Function

#End Region

#Region "EDI_Lの初期値設定(出荷nyuuka処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>各マスタ値を取得しEDI_Lの初期設定をする</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        '荷主M取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectMcustOutkaToroku", ds)

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim mCustDr As DataRow = ds.Tables("LMH030_M_CUST").Rows(0)
        Dim mDestDr As DataRow = Nothing
        Dim mDestFlgYN As Boolean = False      '届先マスタ有無フラグ

        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
            mDestFlgYN = True
        End If

        '出荷区分
        If String.IsNullOrEmpty(ediDr("OUTKA_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_KB") = "10"
        End If

        '出荷種別区分
        If String.IsNullOrEmpty(ediDr("SYUBETU_KB").ToString().Trim()) = True Then
            ediDr("SYUBETU_KB") = "10"
        End If

        '出荷先国内・輸出
        If String.IsNullOrEmpty(ediDr("NAIGAI_KB").ToString().Trim()) = True Then
            ediDr("NAIGAI_KB") = "01"
        End If

        '作業進捗区分
        If String.IsNullOrEmpty(ediDr("OUTKA_STATE_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_STATE_KB") = "10"
        End If

        '出荷報告有無
        If String.IsNullOrEmpty(ediDr("OUTKAHOKOKU_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(mCustDr("OUTKA_RPT_YN").ToString().Trim()) = False Then
                ediDr("OUTKAHOKOKU_YN") = Right(mCustDr("OUTKA_RPT_YN").ToString().Trim(), 1)
            Else
                ediDr("OUTKAHOKOKU_YN") = "0"
            End If
        End If

        'ピッキングリスト区分
        If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("PICK_KB") = mDestDr("PICK_KB").ToString().Trim()
            Else
                ediDr("PICK_KB") = "01"
            End If
        End If

        '出庫日
        If String.IsNullOrEmpty(ediDr("OUTKO_DATE").ToString().Trim()) = True Then
            ediDr("OUTKO_DATE") = ediDr("OUTKA_PLAN_DATE")
        End If

        '当期保管料負担有無
        If String.IsNullOrEmpty(ediDr("TOUKI_HOKAN_YN").ToString().Trim()) = True Then
            ediDr("TOUKI_HOKAN_YN") = "1"
        End If

        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""
        Else
            'DACで値セットを行う
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '分析票添付区分
        If String.IsNullOrEmpty(ediDr("COA_YN").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("COA_YN") = mDestDr("COA_YN").ToString().Trim().Substring(1, 1)
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 Start
            Else
                '届先マスタに存在しない場合、自動追加の値と同値をセットする
                ediDr("COA_YN") = "0"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 End
            End If
        End If

        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True Then
            ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
        End If

        '便区分
        If String.IsNullOrEmpty(ediDr("BIN_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("BIN_KB").ToString().Trim()) = False Then
                    ediDr("BIN_KB") = mDestDr("BIN_KB")
                Else
                    ediDr("BIN_KB") = "01"
                End If
            Else
                ediDr("BIN_KB") = "01"
            End If
        End If

        '運送会社コード
        '運送会社支店コード
        '空の場合は届先マスタの値を設定、届先Mが空の場合は荷主マスタの値を設定
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = True Then

            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("SP_UNSO_CD").ToString().Trim()) = False Then
                    ediDr("UNSO_CD") = mDestDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mDestDr("SP_UNSO_BR_CD").ToString().Trim()
                Else
                    ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
                End If
            Else
                ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
            End If

        End If

        ''タリフ分類区分
        ''運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        ''割増タリフコード(割増運賃タリフマスタ)
        ''DACで値セットを行う
        ''(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        ''①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        ''荷主明細マスタの取得
        'ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        ''タリフセットマスタの取得(運賃タリフ)
        'ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        ''タリフセットマスタの取得(割増タリフ)
        'ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)

        '(2012.09.07)要望番号:1425 UMANO START
        '運賃タリフセットマスタ(荷主・運送会社)の取得(運賃・割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetUnsocoTariffData", ds)
        '(2012.09.07)要望番号:1425 UMANO END

        '(2012.09.07)要望番号:1425 UMANO START
        If MyBase.GetResultCount <> 1 AndAlso _
           MyBase.GetResultCount <> 3 Then
            'タリフセットマスタの取得(運賃タリフ)
            ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)
        End If
        '2012.02.25 大阪対応 END
        '2012.03.05 大阪対応 START
        If MyBase.GetResultCount <> 2 AndAlso _
           MyBase.GetResultCount <> 3 Then
            'タリフセットマスタの取得(割増タリフ)
            ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)
            '2012.03.05 大阪対応 END
        End If
        '(2012.09.07)要望番号:1425 UMANO END

        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False Then

                    If String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                        ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                    ElseIf InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                ''運送会社M取得
                'ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)
                '運送会社荷主別送り状マスタの存在チェック
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                If MyBase.GetResultCount = 0 Then
                    ediDr("DENP_YN") = "0"
                Else
                    ediDr("DENP_YN") = "1"
                End If
            Else
                ediDr("DENP_YN") = "0"
            End If

        End If

        '元着払区分
        If String.IsNullOrEmpty(ediDr("PC_KB").ToString().Trim()) = True Then
            ediDr("PC_KB") = "01"
        End If

        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If

        '荷役料有無
        If String.IsNullOrEmpty(ediDr("NIYAKU_YN").ToString().Trim()) = True Then
            ediDr("NIYAKU_YN") = "1"
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先コード設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDIデータの届先コードが空の場合、届先マスタの値を設定する
    ''' この設定はDB存在チェック後に行う</remarks>
    Private Function SetDestCd(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        '届先コード
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim()
            End If
        End If

        Return ds

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定"

    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet, _
                                    ByVal count As Integer, ByVal unsodata As String, _
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)



        ''-------------------------------------------------------------------------------------
        ''●荷主固有チェック
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        '富士フイルム専用チェック

        '①商品名
        '送られてきた商品名を優先する為、商品マスタの商品名との整合性チェックは行わない
        'Dim mGOODSNm As String = mGoodsDr("GOODS_NM_1").ToString()
        'Dim ediGOODSNm As String = ediMDr("GOODS_NM").ToString()

        ''スペース除去チェックの追加
        'mGOODSNm = Me.SpaceCutChk(mGOODSNm)
        'ediGOODSNm = Me.SpaceCutChk(ediGOODSNm)

        'If mGOODSNm.Equals(ediGOODSNm) = True Then
        '    'チェックなし
        'Else

        '    choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_M002, count)

        '    If String.IsNullOrEmpty(choiceKb) = True Then
        '        msgArray(1) = "商品名"
        '        msgArray(2) = "商品マスタ"
        '        msgArray(3) = "商品名"
        '        msgArray(4) = String.Empty
        '        msgArray(5) = String.Empty

        '        ds = Me._Blc.SetComWarningM("W159", LMH030BLC.FJF_WID_M002, ds, setDs, msgArray, _
        '                                    ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())

        '        compareWarningFlg = True

        '    ElseIf choiceKb.Equals("01") = True Then
        '        '処理区分が"はい"の場合、REMARKを入れ替えて処理続行
        '        If String.IsNullOrEmpty(ediMDr("REMARK").ToString().Trim) Then
        '            ediMDr("REMARK") = mGoodsDr("OUTKA_ATT").ToString
        '        Else
        '            ediMDr("REMARK") = Me.LeftB(String.Concat(ediMDr("REMARK"), Space(2), mGoodsDr("OUTKA_ATT")), 100)
        '        End If

        '        '全角変換したままの場合、桁あふれを起こす事がある
        '        ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        '    End If

        'End If

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
        End If

        '荷主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
            ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
        End If

        '買主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("BUYER_ORD_NO_DTL").ToString()) = True Then
            ediMDr("BUYER_ORD_NO_DTL") = ediLDr("BUYER_ORD_NO")
        End If

        '商品KEY
        'If unsodata.Equals("01") = False Then
        ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")
        'End If

        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        '富士フイルム 追加箇所 terakawa 2012.08.06 Start
        '②個数単位
        If mGoodsDr("NB_UT").ToString().Equals(ediMDr("KB_UT").ToString()) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_M003, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "個数単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "個数単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.FJF_WID_M003, ds, setDs, msgArray, _
                                            ediMDr("KB_UT").ToString(), mGoodsDr("NB_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '個数単位
                ediMDr("KB_UT") = mGoodsDr("NB_UT")
            End If
        End If

        ''個数単位
        'ediMDr("KB_UT") = mGoodsDr("NB_UT")

        '③数量単位
        If mGoodsDr("STD_IRIME_UT").ToString().Equals(ediMDr("QT_UT").ToString()) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_M004, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "数量単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "数量単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.FJF_WID_M004, ds, setDs, msgArray, _
                                            ediMDr("QT_UT").ToString(), mGoodsDr("STD_IRIME_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '数量単位
                ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")
            End If
        End If

        ''数量単位
        'ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")
        '富士フイルム 追加箇所 terakawa 2012.08.06 End

        '④包装個数(入数)
        '送られてくる入数と商品マスタの入数が異なる場合はエラー
        If Convert.ToDecimal(ediMDr("PKG_NB")) <> Convert.ToDecimal(mGoodsDr("PKG_NB")) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"包装個数", "商品マスタ", "包装個数"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'このレコードのワーニングをクリア
            ds.Tables("WARNING_DTL").Rows.Clear()
            Return False
        End If

        ''包装個数
        'ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

        '富士フイルム 追加箇所 terakawa 2012.08.06 Start
        '⑤包装単位
        If mGoodsDr("PKG_UT").ToString().Equals(ediMDr("PKG_UT").ToString()) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_M005, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "包装単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "包装単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.FJF_WID_M005, ds, setDs, msgArray, _
                                            ediMDr("PKG_UT").ToString(), mGoodsDr("PKG_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '包装単位
                ediMDr("PKG_UT") = mGoodsDr("PKG_UT")
            End If
        End If

        ''包装単位
        'ediMDr("PKG_UT") = mGoodsDr("PKG_UT")
        '富士フイルム 追加箇所 terakawa 2012.08.06 End

        '温度区分
        ediMDr("ONDO_KB") = mGoodsDr("ONDO_KB")

        '運送温度区分
        If String.IsNullOrEmpty(ediMDr("UNSO_ONDO_KB").ToString()) = True Then

            If (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If

            If (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If
        Else
            '運送温度区分(区分マスタ)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '⑥入目
        '入目が特定できていない場合は、強制的に商品マスタの値を設定
        If Convert.ToDecimal(ediMDr("IRIME")) = 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '「FFEM大分工場、かつ、原料(品番が243で始まる)」でない場合のみ
#If False Then
        If ((Not "96".Equals(ediMDr("NRS_BR_CD").ToString)) OrElse (Not "98".Equals(ediMDr("NRS_BR_CD").ToString))) OrElse Not ("243".Equals(Left(ediMDr("CUST_GOODS_CD").ToString, 3))) Then    'ADD 2019/10/11 FFEM大分棚番管理

#Else
        '営業所 96,98の時、CUST_GOODS_CDの先頭３文字243以外のとき　←　F2　追加戻し　UPD 2022/12/2 033380 
        ' または営業所 96,98以外の時  ←　F2　追加戻し　UPD 2022/12/2 033380 
        '↓
        '( 営業所が (96, 98, F2, F3) のいずれか、かつ“原料”以外 [CUST_GOODS_CD の先頭3文字が 243 が“原料”] ) または
        '( 営業所が (96, 98, F2, F3) のどれでもない ) 場合
        If ((("96").Equals(ediMDr("NRS_BR_CD").ToString) = True OrElse ("98").Equals(ediMDr("NRS_BR_CD").ToString) = True OrElse ("F2").Equals(ediMDr("NRS_BR_CD").ToString) = True OrElse ("F3").Equals(ediMDr("NRS_BR_CD").ToString) = True) _
            AndAlso (("243".Equals(Left(ediMDr("CUST_GOODS_CD").ToString, 3))) = False)) _
            OrElse (("96").Equals(ediMDr("NRS_BR_CD").ToString) = False AndAlso ("98").Equals(ediMDr("NRS_BR_CD").ToString) = False AndAlso ("F2").Equals(ediMDr("NRS_BR_CD").ToString) = False AndAlso ("F3").Equals(ediMDr("NRS_BR_CD").ToString) = False) Then

#End If
            '受信時にセットした入目と商品マスタの入目が異なる場合はエラー
            If Convert.ToDecimal(ediMDr("IRIME")) <> Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目", "商品マスタ", "標準入目"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False
            End If
        End If    'ADD 2019/10/11 FFEM大分棚番管理

        'If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        'AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
        '    ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        'End If

        '富士フイルム 追加箇所 terakawa 2012.08.06 Start
        '⑦入目単位
        If mGoodsDr("STD_IRIME_UT").ToString().Equals(ediMDr("IRIME_UT").ToString()) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_M006, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "入目単位"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "入目単位"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W159", LMH030BLC.FJF_WID_M006, ds, setDs, msgArray, _
                                            ediMDr("IRIME_UT").ToString(), mGoodsDr("STD_IRIME_UT").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                '入目単位
                ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
            End If
        End If

        '入目単位
        'ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT").ToString()
        '富士フイルム 追加箇所 terakawa 2012.08.06 End

        If Not IsMaterialPlantTransfer(ds) Then
            '「原料プラント間転送」以外の場合

            '出荷包装個数
            '出荷端数
            Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
            Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
            Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
            Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
            Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
            Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

            Select Case alctdKb

                Case "01"
                    If 1 < pkgNb Then

                        ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                        ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                    Else
                        ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                        ediMDr("OUTKA_HASU") = 0
                    End If

                    ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

                Case "02"
                    ediMDr("OUTKA_PKG_NB") = 0
                    If outkaTtlQt Mod irime = 0 Then
                        ediMDr("OUTKA_HASU") = outkaTtlQt / irime
                    Else
                        ediMDr("OUTKA_HASU") = Math.Floor(outkaTtlQt / irime) + 1
                    End If

                    ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")

                Case "03"
                    ediMDr("OUTKA_PKG_NB") = 0
                    ediMDr("OUTKA_HASU") = 0
                    ediMDr("OUTKA_TTL_NB") = 0

                Case Else

            End Select

            '出荷数量
            ediMDr("OUTKA_QT") = ediMDr("OUTKA_TTL_QT")

        End If

        '個別重量(KGS)
        'If unsodata.Equals("01") = False Then
        ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
        'End If

        '出荷時加工作業区分1-5
        ediMDr("OUTKA_KAKO_SAGYO_KB_1") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_1")
        ediMDr("OUTKA_KAKO_SAGYO_KB_2") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_2")
        ediMDr("OUTKA_KAKO_SAGYO_KB_3") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_3")
        ediMDr("OUTKA_KAKO_SAGYO_KB_4") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_4")
        ediMDr("OUTKA_KAKO_SAGYO_KB_5") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_5")

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入力チェック(出荷登録処理)"

#Region "EDI出荷(大)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------

        '出荷管理番号
        If Me._Blc.OutkaCtlNoCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(大)のDAC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, _
                                       ByVal genericInsFlg As Integer) As Boolean '要望番号1282 修正 2012.08.21

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ' ''オーダー番号重複チェック
        'If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

        '    If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
        '        Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
        '        If MyBase.GetResultCount > 0 Then
        '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '            Return False
        '        End If

        '    End If

        'End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        Dim unchinTariffCd As String = String.Empty
        unchinTariffCd = drEdiL("UNCHIN_TARIFF_CD").ToString()
        Dim unsoTehaiKb As String = String.Empty
        unsoTehaiKb = drEdiL("UNSO_TEHAI_KB").ToString()

        If String.IsNullOrEmpty(unchinTariffCd) = True Then

        Else

            If unsoTehaiKb.Equals("40") = True Then
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMyokoTariffHd", ds)
            Else
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMunchinTariff", ds)
            End If

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '-------------------------------------------------------------------------------------
        '●荷主固有チェック(富士フイルム専用)　まだ未着手!!!!!!!!!!!
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()      '荷送人コード
        Dim workDestCd As String = String.Empty                     '検索する届先コード格納変数
        Dim workDestString As String = String.Empty                 '"届先コード"or"EDI届先コード"
        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")


        'DEST_CDが空の場合、EDI_DEST_CDを使う
        If String.IsNullOrEmpty(destCd) = False Then
            workDestCd = destCd
            workDestString = "届先コード"
        ElseIf String.IsNullOrEmpty(ediDestCd) = False Then
            workDestCd = ediDestCd
            workDestString = "EDI届先コード"
        Else
            '富士フイルム 追加箇所 terakawa 2012.08.06 Start
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
            '富士フイルム 追加箇所 terakawa 2012.08.06 End
        End If

        '荷送人コードのマスタ存在チェック
        'SHIP_CD_Lが空でなく、SHIP_CD_L = DEST_CD <> EDI_DEST_CD の場合、もしくはSHIP_CD_L <> DEST_CD の場合
        If (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = True AndAlso shipCdL.Equals(ediDestCd) = False) _
            OrElse (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = False) Then

            '富士フイルム追加箇所 terakawa 2012.08.07 Start
            ds = MyBase.CallDAC(Me._Dac, "SelectDataFjfMdest", ds)

            If MyBase.GetResultCount = 0 Then
                '上記条件に該当する場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E079", New String() {"届先マスタ", "荷送人コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '富士フイルム追加箇所 terakawa 2012.08.07 End

            'ds = MyBase.CallDAC(Me._Dac, "SelectDataFjfMdest", ds)

            'If MyBase.GetResultCount = 0 Then
            '    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
            '    If SetInsMDestFromShip(ds) = True Then
            '        flgWarning = True
            '    End If
            'Else
            '    If drEdiL("SHIP_NM_L").ToString().Equals(dtMS.Rows(0).Item("DEST_NM").ToString()) = False Then
            '        'ワーニング⇒マスタ更新(ワーニング設定した場合はflgWarning=True)
            '        If SetUpdMDestFromShip(ds) = True Then
            '            flgWarning = True
            '        End If
            '    End If
            'End If
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェック
            '富士フイルムの場合、不整合の場合はエラー
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    '整合性チェックでワーニングがあった場合は、flgWarning=True
                    flgWarning = True
                End If
            End If

        ElseIf mDestCount = 0 Then
            '0件の場合、ZIPコードのマスタ存在チェックを行い、届先マスタの更新をする
            'JISマスタに存在しない場合、エラー
            'JISマスタに存在するが、JISが空の場合、ワーニング
            '要望番号1282 修正START 2012.08.21
            If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString, genericInsFlg) = False Then
                'If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) = False Then
                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    'チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                    flgWarning = True
                End If
            End If
            '要望番号1282 修正End 2012.08.21
        Else
            '複数件の場合、エラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(中)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(中)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '引当単位区分
        If Me._Blc.AlctdKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(中)のDAC側でのチェック + 初期値設定"

    ''' <summary>
    ''' EDI出荷(中)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMMasterExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1
        Dim unsoData As String = String.Empty
        Dim custGoodsCd As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        Dim flgWarning As Boolean = False

        For i As Integer = 0 To max

            custGoodsCd = dtM.Rows(i)("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
                '富士フイルム専用チェック
                '富士フイルム品目マスタ検索（荷主商品コード）
                setDs = (MyBase.CallDAC(Me._Dac, "SelectDataHinmokuFjf", setDs))

                'If MyBase.GetResultCount() > 0 Then
                '    '未反映データが１件以上ある場合エラー
                '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E160", New String() {"富士フイルム品目マスタ", "未反映データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                '    'このレコードのワーニングをクリア
                '    ds.Tables("WARNING_DTL").Rows.Clear()
                '    Return False
                'End If

                If MyBase.GetResultCount() > 0 Then
                    'If String.IsNullOrEmpty(setDtM.Rows(0).Item("GOODS_NM").ToString()) = True Then
                    '    '未反映データが１件以上ある場合エラー
                    '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E160", _
                    '                           New String() {String.Concat("荷主商品コード：", _
                    '                                                       setDtM.Rows(0).Item("CUST_GOODS_CD").ToString(), _
                    '                                                       "は、", _
                    '                                                       "富士フイルム品目マスタ"), "未反映データ"}, _
                    '                                                       rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'Else
                    '未反映データが１件以上ある場合エラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E160", _
                                           New String() {String.Concat("荷主商品コード：", _
                                                                       setDtM.Rows(0).Item("CUST_GOODS_CD").ToString(), _
                                                                       " 商品名：", _
                                                                       setDtM.Rows(0).Item("GOODS_NM").ToString(), _
                                                                       "は、", _
                                                                       "富士フイルム品目マスタ"), "未反映データ"}, _
                                                                       rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'End If
                    'このレコードのワーニングをクリア
                    ds.Tables("WARNING_DTL").Rows.Clear()

                    '要望 修正START 商品データがない場合、毎回エラー表示される回避対応
                    '①falseで抜けない(コメント化)
                    '②ワーニングフラグを使用(エラーだが同様に使用する)
                    '③その明細の処理は抜け、次明細の処理を行う
                    'Return False

                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                    '要望 修正END

                End If

                '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.FJF_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                If MyBase.GetResultCount = 0 Then

                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"荷主商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                    Return False
                ElseIf GetResultCount() > 1 Then

                    '入目 + 荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsIrimeOutka", setDs))

                    If MyBase.GetResultCount = 1 Then
                    Else
                        '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.FJF_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    '富士フイルムではワーニング,エラーは両方存在。
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、flgWarning=True
                        flgWarning = True
                    End If

                End If

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then
                    Return False
                End If

            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        Next

        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタチェック(富士フイルム専用)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = dtMdest.Rows(0).Item("DEST_NM").ToString()
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediDestAd1 As String = dtEdi.Rows(0)("DEST_AD_1").ToString()
        Dim ediDestAd2 As String = dtEdi.Rows(0)("DEST_AD_2").ToString()
        Dim ediDestAd3 As String = dtEdi.Rows(0)("DEST_AD_3").ToString()
        Dim ediDestAdAll As String = String.Concat(ediDestAd1, ediDestAd2, ediDestAd3)
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(届先マスタ)
        If String.IsNullOrEmpty(ediDestNm) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先名称が空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L004, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.FJF_WID_L004, ds, msgArray, _
                                            dtEdi.Rows(0)("DEST_NM").ToString(), dtMdest.Rows(0).Item("DEST_NM").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '20160808 ma-takahashi 引当計上データを正として住所データを登録する　住所チェックはなし
        '届先住所(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediDestAdAll) = True Then
        '    チェックなし()
        'Else

        '    mAdAll = SpaceCutChk(mAdAll)
        '    ediDestAdAll = SpaceCutChk(ediDestAdAll)
        '    If mAdAll.Equals(ediDestAdAll) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L005, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            msgArray(1) = "届先住所"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "住所"
        '            msgArray(4) = "EDIデータ"
        '            msgArray(5) = String.Empty
        '            ds = Me._Blc.SetComWarningL("W166", LMH030BLC.FJF_WID_L005, ds, msgArray, ediDestAdAll, mAdAll)

        '            compareWarningFlg = True

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
        '            dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
        '            dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
        '            'マスタ更新対象フラグ
        '            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        '        End If
        '    End If

        'End If

        '郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If mZip.Equals(ediZip) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L014, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先郵便番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "郵便番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.FJF_WID_L014, ds, msgArray, ediZip, mZip)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '20160808 ma-takahashi 引当計上データのみ正として登録するので、電話番号チェックを解除
        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L006, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.FJF_WID_L006, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If

        End If

        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If

        End If

        If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L007, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
                ds = Me._Blc.SetComWarningL("W197", LMH030BLC.FJF_WID_L007, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If


        ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L008, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ds = Me._Blc.SetComWarningL("W188", LMH030BLC.FJF_WID_L008, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then
            'EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L009, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.FJF_WID_L009, ds, msgArray, ediDestJisCd, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = ediDestJisCd
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = True AndAlso mZipJis.Equals(mJis) = False Then
            'EDIのJISコードが空でJISマスタ(郵便番号マスタ)のJISコードと届先マスタのJISコードに差異がある場合
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L010, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = String.Concat(warningString, "から取得したJISコード")
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = warningString
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W187", LMH030BLC.FJF_WID_L010, ds, msgArray, mZipJis, mJis)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("JIS") = mZipJis
                dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf choiceKb.Equals("02") = True Then
                'ワーニングで"いいえ"を選択時
            End If

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタ追加時チェック"
    ''' <summary>
    ''' 届先マスタ追加時チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, _
                                     ByVal workDestCd As String, ByVal workDestString As String, _
                                     ByVal genericInsFlg As Integer) As Boolean '要望番号1282 修正 2012.08.21

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)

        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()

        Dim compareWarningFlg As Boolean = False

        '届先名称(届先マスタ)
        If String.IsNullOrEmpty(ediDestNm) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先名称が空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If

        End If

        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            'Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If

        End If

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.FJF_WID_L011, 0)

        '要望番号1282 修正START 2012.08.21(汎用届先コードの自動追加の場合はワーニングを出力しない)
        If String.IsNullOrEmpty(choiceKb) = True AndAlso genericInsFlg = 0 Then
            'If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If

            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.FJF_WID_L011, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222

            compareWarningFlg = True
            'Return True

            '要望番号1282 修正START 2012.07.21(汎用届先コードの場合はワーニングなしで自動追加)
        ElseIf choiceKb.Equals("01") = True OrElse genericInsFlg = 1 Then
            'ElseIf choiceKb.Equals("01") = True Then

            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMdest.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            '要望番号1282 追加START 2012.08.21
            If genericInsFlg = 0 Then
                drMD("EDI_CD") = workDestCd
            ElseIf genericInsFlg = 1 Then
                drMD("EDI_CD") = drEdiL("EDI_DEST_CD").ToString()
            End If
            '要望番号1282 追加END 2012.08.21
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            drMD("ZIP") = drEdiL("DEST_ZIP").ToString()
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
            drMD("JIS") = mZipJis
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            'マスタ自動追加対象フラグ
            drMD("MST_INSERT_FLG") = "1"
            dtMdest.Rows.Add(drMD)

        End If

        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタ(荷送人コード)追加時チェック"

    ''' <summary>
    ''' 荷送人コードから届先マスタInsertデータを作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ワーニング設定をする
    ''' ワーニング画面の戻り値がある場合、荷送人コードから届先マスタInsertデータを作成する
    ''' </remarks>
    Private Function SetInsMDestFromShip(ByVal ds As DataSet) As Boolean

        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        Dim msgArray(5) As String
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.FJF_WID_L012, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = "荷送人コード"
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.FJF_WID_L012, ds, msgArray, shipCdL, String.Empty)

            Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            Dim drMS As DataRow = dtMS.NewRow()
            drMS("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMS("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMS("DEST_CD") = shipCdL
            drMS("EDI_CD") = shipCdL
            If String.IsNullOrEmpty(drEdiL("SHIP_NM_L").ToString()) = False Then
                drMS("DEST_NM") = drEdiL("SHIP_NM_L").ToString()
            End If
            drMS("ZIP") = String.Empty
            drMS("AD_1") = String.Empty
            drMS("AD_2") = String.Empty
            drMS("AD_3") = String.Empty
            drMS("COA_YN") = "00"
            drMS("TEL") = String.Empty
            drMS("JIS") = String.Empty
            drMS("PICK_KB") = "01"
            drMS("BIN_KB") = "01"
            'マスタ自動追加対象フラグ
            drMS("MST_INSERT_FLG") = "1"
            dtMS.Rows.Add(drMS)

        End If

    End Function

#End Region

#Region "届先マスタ(荷送人コード)更新時チェック"

    ''' <summary>
    ''' 荷送人コードから届先マスタUpdateデータを作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ワーニング設定をする
    ''' ワーニング画面の戻り値がある場合、荷送人コードから届先マスタUpdateデータを作成する
    ''' </remarks>
    Private Function SetUpdMDestFromShip(ByVal ds As DataSet) As Boolean

        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        Dim msgArray(5) As String
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.FJF_WID_L013, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = "荷送人名（大）"
            msgArray(2) = "届先マスタ"
            msgArray(3) = "届先名称"
            msgArray(4) = "EDIデータ"

            ds = Me._Blc.SetComWarningL("W166", LMH030BLC.FJF_WID_L013, ds, msgArray, drEdiL("SHIP_NM_L").ToString(), dtMS.Rows(0).Item("DEST_NM").ToString())

            Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            dtMS.Rows(0).Item("DEST_CD") = shipCdL
            dtMS.Rows(0).Item("DEST_NM") = drEdiL("SHIP_NM_L").ToString()
            'マスタ更新対象フラグ
            dtMS.Rows(0).Item("MST_UPDATE_FLG") = "1"

        End If

    End Function

#End Region

#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function
#End Region

#Region "左埋処理"
    ''' <summary>
    ''' 0埋処理
    ''' </summary>
    ''' <param name="val">対象文字列</param>
    ''' <param name="keta">0埋後の桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatZero(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta, "0"c)

        Return val

    End Function

    ''' <summary>
    ''' スペース埋処理
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="keta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpace(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta)

        Return val

    End Function


#End Region

#Region "ワーニング処理(EDI(大)届先)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#Region "ワーニング処理(EDI(中)商品)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(count)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty
        Dim mstFlg As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング画面の処理区分値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()

                mstFlg = warningId.Substring(7, 1)

                Select Case mstFlg
                    Case "1"
                        'ワーニング処理設定の値を反映
                        setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")
                    Case Else

                End Select

                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#End Region

#Region "データセット設定"

    '要望番号1282 追加START 2012.08.21
#Region "EDI出荷(大)届先コード設定"

    ''' <summary>
    ''' DIC汎用届先コードの採番
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GenericDestCheck(ByVal ds As DataSet) As Integer

        Dim dtCd As DataTable = ds.Tables("LMH030_M_CUST_DETAILS")
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtCd.Rows.Count - 1
        Dim genericCd As String = String.Empty
        Dim whereSql As String = String.Empty
        Dim destCd As String = dtEdiL.Rows(0).Item("DEST_CD").ToString()
        Dim ediDestCd As String = dtEdiL.Rows(0).Item("EDI_DEST_CD").ToString()
        Dim comDestCd As String = String.Empty
        Dim genericlgt As Integer = 0
        Dim genericInsFlg As Integer = LMH030BLC.FLG_OFF
        Dim dicGeneralNo As Integer = 0
        Dim num As New NumberMasterUtility

        If String.IsNullOrEmpty(destCd) = True Then
            comDestCd = ediDestCd
        Else
            comDestCd = destCd
        End If

        For i As Integer = 0 To max

            genericCd = dtCd.Rows(i).Item("SET_NAIYO").ToString()
            whereSql = dtCd.Rows(i).Item("SET_NAIYO_2").ToString()
            genericlgt = genericCd.Length

            Select Case whereSql

                Case "0"

                    If genericCd.Equals(comDestCd) = True Then

                        dicGeneralNo = Convert.ToInt32(num.GetAutoCode(NumberMasterUtility.NumberKbn.GENERAL_NO, Me, String.Empty))

                        dtEdiL.Rows(0).Item("DEST_CD") = String.Concat(genericCd, _
                                                                       Me.FormatZero(Convert.ToString(dicGeneralNo), LMH030BLC.DEST_CD_LENGTH - genericlgt))
                        genericInsFlg = LMH030BLC.FLG_ON
                        Return genericInsFlg
                    End If

                Case "1"

                    If InStr(comDestCd, genericCd) > 0 Then

                        dicGeneralNo = Convert.ToInt32(num.GetAutoCode(NumberMasterUtility.NumberKbn.GENERAL_NO, Me, String.Empty))

                        dtEdiL.Rows(0).Item("DEST_CD") = String.Concat(genericCd, _
                                                                       Me.FormatZero(Convert.ToString(dicGeneralNo), LMH030BLC.DEST_CD_LENGTH - genericlgt))
                        genericInsFlg = LMH030BLC.FLG_ON
                        Return genericInsFlg
                    End If

            End Select

        Next

        Return genericInsFlg

    End Function

#End Region
    '要望番号1282 追加END 2012.08.21

#Region "データセット設定(出荷管理番号L)"

    ''' <summary>
    ''' データセット設定(出荷管理番号L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="matomeFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim outkaKanriNoPrm As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_CTL_NO").ToString()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        Dim max As Integer = dt.Rows.Count - 1

        If eventShubetsu.Equals("3") = True Then

            '紐付け処理の場合
            dr("OUTKA_CTL_NO") = outkaKanriNoPrm
            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNoPrm
            Next

        ElseIf matomeFlg = False Then

            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)

            dr("OUTKA_CTL_NO") = outkaKanriNo

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合
            outkaKanriNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString()
            dr("OUTKA_CTL_NO") = outkaKanriNo
            dr("FREE_C30") = String.Concat("04-", ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("EDI_CTL_NO").ToString())

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷管理番号M)"
    ''' <summary>
    ''' 出荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoM(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtHimoduke As DataTable = ds.Tables("LMH030_HIMODUKE")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") = True Then
            '紐付け処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = dtHimoduke.Rows(i)("HIMODUKE_NO").ToString()
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        ElseIf matomeFlg = False Then
            '通常出荷登録処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = (i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合、まとめ先DataSetから取得
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
            'Dim maxOutkaKanriNo As Integer = Convert.ToInt32(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO_CHU"))
            Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End
            For i As Integer = 0 To max
                outkaKanriNo = (maxOutkaKanriNo + i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷L)"
    ''' <summary>
    ''' データセット設定(出荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        'ADD 2016/07/26 Start 届け先CDが空のときは、LMH030_M_TOKUI_FJFよりセットする
        Dim tokuiTEL As String = String.Empty
        If ds.Tables("LMH030_M_TOKUI_FJF").Rows.Count > 0 Then
            tokuiTEL = ds.Tables("LMH030_M_TOKUI_FJF").Rows(0).Item("TELF1").ToString.Trim
        End If
        'ADD 2016/07/26 End 届け先CDが空のときは、LMH030_M_TOKUI_FJFよりセットする

        If matomeFlg = False Then
            '通常登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
            outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
            If IsMaterialPlantTransfer(ds) Then
                '原料プラント間転送の場合
                outkaDr("OUTKA_STATE_KB") = "50"
            Else
                outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
            End If
            outkaDr("OUTKAHOKOKU_YN") = FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
            outkaDr("PICK_KB") = ediDr("PICK_KB")
            outkaDr("DENP_NO") = String.Empty
            outkaDr("ARR_KANRYO_INFO") = String.Empty
            outkaDr("WH_CD") = ediDr("WH_CD")
            outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
            outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
            outkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            outkaDr("END_DATE") = String.Empty
            outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
            outkaDr("SHIP_CD_M") = String.Empty
            ''富士フイルム専用設定START(届先項目)
            'If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            '    Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
            '    outkaDr("DEST_CD") = destMDr("DEST_CD")
            '    outkaDr("DEST_AD_3") = destMDr("AD_3")
            '    outkaDr("DEST_TEL") = destMDr("TEL")
            'Else
            '    outkaDr("DEST_CD") = ediDr("DEST_CD")
            '    outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
            '    outkaDr("DEST_TEL") = ediDr("DEST_TEL")
            'End If

            'update 引当計上データを優先的に届け先データをセットする改修
            outkaDr("DEST_CD") = ediDr("DEST_CD")
            outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
            outkaDr("DEST_TEL") = ediDr("DEST_TEL")
            'update 引当計上データを優先的に届け先データをセットする改修

            'ADD 2016/07/26 Start
            If String.Empty.Equals(tokuiTEL) = False Then
                outkaDr("DEST_TEL") = tokuiTEL.ToString
            End If
            'ADD 2016/07/26 End

            '富士フイルム専用設定END(届先項目)
            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
            '2014.09.25 高取対応　修正START

            '出荷条件(Z3)の場合は"荷動きなしをセット
            If ediDr("FREE_C18").ToString() = LMH030BLC103.SIKAKARI_SYUKKA Then
                outkaDr("REMARK") = LMH030BLC103.SYUKKA_REMARK
            Else
                outkaDr("REMARK") = String.Empty
            End If

            '特定伝票タイプ(FREE_C16)のセット
            Select Case ediDr("FREE_C16").ToString

                '社内使用の場合("Z03")
                Case LMH030BLC103.DENPYO_TYPE_Z03

                    If String.IsNullOrEmpty(outkaDr("REMARK").ToString()) = False Then
                        outkaDr("REMARK") = String.Concat(outkaDr("REMARK").ToString(), "-", LMH030BLC103.SYANAISIYOU_REMARK)
                    Else
                        outkaDr("REMARK") = LMH030BLC103.SYANAISIYOU_REMARK
                    End If

                    '廃棄の場合("551")
                Case LMH030BLC103.DENPYO_TYPE_551

                    If String.IsNullOrEmpty(outkaDr("REMARK").ToString()) = False Then
                        outkaDr("REMARK") = String.Concat(outkaDr("REMARK").ToString(), "-", LMH030BLC103.HAIKI_REMARK)
                    Else
                        outkaDr("REMARK") = LMH030BLC103.HAIKI_REMARK
                    End If

                    '詰め替えの場合("301")
                Case LMH030BLC103.DENPYO_TYPE_301

                    If String.IsNullOrEmpty(outkaDr("REMARK").ToString()) = False Then
                        outkaDr("REMARK") = String.Concat(outkaDr("REMARK").ToString(), "-", LMH030BLC103.TUMEKAE_REMARK)
                    Else
                        outkaDr("REMARK") = LMH030BLC103.TUMEKAE_REMARK
                    End If

                Case Else

            End Select

            If String.IsNullOrEmpty(outkaDr("REMARK").ToString()) = False AndAlso String.IsNullOrEmpty(ediDr("REMARK").ToString()) = False Then
                outkaDr("REMARK") = String.Concat(outkaDr("REMARK").ToString(), "-", ediDr("REMARK"))
            ElseIf String.IsNullOrEmpty(outkaDr("REMARK").ToString()) = True Then
                outkaDr("REMARK") = ediDr("REMARK")
            Else
                outkaDr("REMARK") = outkaDr("REMARK")
            End If
            '2014.09.25 高取対応　修正END
            If IsMaterialPlantTransfer(ds) Then
                '原料プラント間転送の場合
                outkaDr("OUTKA_PKG_NB") = SumPkgNb2(dt)
            Else
                outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
            End If
            outkaDr("DENP_YN") = FormatZero(ediDr("DENP_YN").ToString(), 2)
            outkaDr("PC_KB") = ediDr("PC_KB")
            outkaDr("UNCHIN_YN") = FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
            outkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            outkaDr("ALL_PRINT_FLAG") = "00"
            outkaDr("NIHUDA_FLAG") = "00"
            outkaDr("NHS_FLAG") = "00"
            outkaDr("DENP_FLAG") = "00"
            outkaDr("COA_FLAG") = "00"
            outkaDr("HOKOKU_FLAG") = "00"
            outkaDr("MATOME_PICK_FLAG") = "00"
            outkaDr("LAST_PRINT_DATE") = String.Empty
            outkaDr("LAST_PRINT_TIME") = String.Empty
            outkaDr("SASZ_USER") = String.Empty
            outkaDr("OUTKO_USER") = String.Empty
            outkaDr("KEN_USER") = String.Empty
            outkaDr("OUTKA_USER") = String.Empty
            outkaDr("HOU_USER") = String.Empty
            outkaDr("ORDER_TYPE") = String.Empty
            outkaDr("SYS_DEL_FLG") = "0"
            outkaDr("DEST_KB") = "02"
            outkaDr("DEST_NM") = ediDr("DEST_NM")
            outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            If IsMaterialPlantTransfer(ds) Then
                '原料プラント間転送の場合
                outkaDr("OUTKA_PKG_NB") = SumPkgNb2(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            Else
                outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            End If
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")
        End If
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Private Function SumPkgNb(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            If 0 = calcPkgModNb Then
            Else
                sumNb = sumNb + 1
            End If

        Next

        Return sumNb

    End Function

    ''' <summary>
    ''' データセット設定(出荷包装個数) 原料プラント間転送の場合
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    Private Function SumPkgNb2(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0

        For i As Integer = 0 To max

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

        Next

        Return sumNb

    End Function
#End Region

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0


        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").NewRow()

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
            End If

            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediLDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            If ediDr("SET_KB").ToString = "2" Then
                outkaDr("EDI_SET_NO") = ediDr("FREE_C10")
            Else
                outkaDr("EDI_SET_NO") = String.Empty
            End If
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            If IsMaterialPlantTransfer(ds) Then
                '原料プラント間転送の場合
                '引当済の状態とする
                outkaDr("OUTKA_PKG_NB") = ediDr("OUTKA_PKG_NB")
                outkaDr("OUTKA_HASU") = 0
                outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
                outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
                outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
                outkaDr("ALCTD_NB") = ediDr("OUTKA_TTL_NB")
                outkaDr("ALCTD_QT") = ediDr("OUTKA_TTL_QT")
                outkaDr("BACKLOG_NB") = 0
                outkaDr("BACKLOG_QT") = 0
            Else
                outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
                outkaDr("OUTKA_HASU") = ediDr("OUTKA_HASU")
                outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
                outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
                outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
                outkaDr("ALCTD_NB") = 0
                outkaDr("ALCTD_QT") = 0
                outkaDr("BACKLOG_NB") = ediDr("OUTKA_TTL_NB")
                outkaDr("BACKLOG_QT") = ediDr("OUTKA_TTL_QT")
            End If
            outkaDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            outkaDr("IRIME") = ediDr("IRIME")
            outkaDr("IRIME_UT") = ediDr("IRIME_UT")

            If IsMaterialPlantTransfer(ds) Then
                '原料プラント間転送の場合
                outkaDr("OUTKA_M_PKG_NB") = ediDr("OUTKA_PKG_NB")
            Else
                If Convert.ToInt64(dt.Rows(i)("PKG_NB")) = 0 Then
                    outkaDr("OUTKA_M_PKG_NB") = 0
                Else
                    If 0 = calcPkgModNb Then
                        outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb
                    Else
                        outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                    End If
                End If

                If Convert.ToInt64(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
                    outkaDr("OUTKA_M_PKG_NB") = 1
                End If
            End If

            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("SIZE_KB") = String.Empty
            outkaDr("ZAIKO_KB") = String.Empty
            outkaDr("SOURCE_CD") = String.Empty
            outkaDr("YELLOW_CARD") = String.Empty
            outkaDr("PRINT_SORT") = "99"
            outkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_C_OUTKA_M").Rows.Add(outkaDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷S)"

    ''' <summary>
    ''' データセット設定(出荷S)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaS(ByVal ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility

        Dim nrsBrCd As String = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("NRS_BR_CD").ToString()
        Dim zaiRecNo As String

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_S").NewRow()

            ' 出庫プラント
            Dim ZFVYHWERKS As String = ediDr.Item("FREE_C02").ToString().Trim().PadRight(2, " "c)
            ' 保管場所
            Dim LGORT As String = ediDr.Item("FREE_C04").ToString().Trim().PadRight(4, " "c)

            ' 在庫レコード番号
            zaiRecNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, nrsBrCd)

            outkaDr.Item("NRS_BR_CD") = ediDr.Item("NRS_BR_CD")
            outkaDr.Item("OUTKA_NO_L") = ediDr.Item("OUTKA_CTL_NO")
            outkaDr.Item("OUTKA_NO_M") = ediDr.Item("OUTKA_CTL_NO_CHU")
            outkaDr.Item("OUTKA_NO_S") = "001"
            outkaDr.Item("TOU_NO") = ZFVYHWERKS.Substring(ZFVYHWERKS.Length() - 2, 2)
            outkaDr.Item("SITU_NO") = LGORT.Substring(0, 2)
            outkaDr.Item("ZONE_CD") = LGORT.Substring(LGORT.Length() - 2, 2)
            outkaDr.Item("LOCA") = ""
            outkaDr.Item("LOT_NO") = ediDr.Item("LOT_NO")
            outkaDr.Item("SERIAL_NO") = ediDr.Item("SERIAL_NO")
            outkaDr.Item("OUTKA_TTL_NB") = ediDr.Item("OUTKA_TTL_NB")
            outkaDr.Item("OUTKA_TTL_QT") = ediDr.Item("OUTKA_TTL_QT")
            outkaDr.Item("ZAI_REC_NO") = zaiRecNo
            outkaDr.Item("INKA_NO_L") = ""
            outkaDr.Item("INKA_NO_M") = ""
            outkaDr.Item("INKA_NO_S") = ""
            outkaDr.Item("ZAI_UPD_FLAG") = "00"
            outkaDr.Item("ALCTD_CAN_NB") = 0
            outkaDr.Item("ALCTD_NB") = ediDr.Item("OUTKA_TTL_NB")
            outkaDr.Item("ALCTD_CAN_QT") = 0.0D
            outkaDr.Item("ALCTD_QT") = ediDr.Item("OUTKA_TTL_QT")
            outkaDr.Item("IRIME") = ediDr.Item("IRIME")
            outkaDr.Item("BETU_WT") = ediDr.Item("BETU_WT")
            outkaDr.Item("COA_FLAG") = "00"
            outkaDr.Item("REMARK") = ediDr.Item("REMARK")
            outkaDr.Item("SMPL_FLAG") = "00"
            outkaDr.Item("REC_NO") = ""

            ' データセットに設定
            ds.Tables("LMH030_C_OUTKA_S").Rows.Add(outkaDr)

        Next

        Return ds

    End Function

#Region "データセット設定(在庫データ)"

    ''' <summary>
    ''' データセット設定(在庫データ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetZaiTrs(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String = ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("NRS_BR_CD").ToString()

        Dim ediL_Dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediM_Dr As DataRow
        Dim outkaS_Dr As DataRow
        Dim zaiDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim max As Integer = dt.Rows.Count - 1


        For i As Integer = 0 To max

            ediM_Dr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaS_Dr = ds.Tables("LMH030_C_OUTKA_S").Rows(i)
            zaiDr = ds.Tables("LMH030_D_ZAI_TRS").NewRow()

            ' 出庫プラント
            Dim ZFVYHWERKS As String = ediM_Dr.Item("FREE_C02").ToString().Trim().PadRight(2, " "c)
            ' 保管場所
            Dim LGORT As String = ediM_Dr.Item("FREE_C04").ToString().Trim().PadRight(4, " "c)

            zaiDr.Item("NRS_BR_CD") = nrsBrCd
            zaiDr.Item("ZAI_REC_NO") = outkaS_Dr.Item("ZAI_REC_NO")
            zaiDr.Item("WH_CD") = ediL_Dr.Item("WH_CD")
            zaiDr.Item("TOU_NO") = ZFVYHWERKS.Substring(ZFVYHWERKS.Length() - 2, 2)
            zaiDr.Item("SITU_NO") = LGORT.Substring(0, 2)
            zaiDr.Item("ZONE_CD") = LGORT.Substring(LGORT.Length() - 2, 2)
            zaiDr.Item("LOCA") = ""
            zaiDr.Item("LOT_NO") = ediM_Dr.Item("LOT_NO")
            zaiDr.Item("CUST_CD_L") = ediL_Dr.Item("CUST_CD_L")
            zaiDr.Item("CUST_CD_M") = ediL_Dr.Item("CUST_CD_M")
            zaiDr.Item("GOODS_CD_NRS") = ediM_Dr.Item("NRS_GOODS_CD")
            zaiDr.Item("GOODS_KANRI_NO") = ""
            zaiDr.Item("INKA_NO_L") = ""
            zaiDr.Item("INKA_NO_M") = ""
            zaiDr.Item("INKA_NO_S") = ""
            zaiDr.Item("ALLOC_PRIORITY") = "10"
            zaiDr.Item("RSV_NO") = ""
            zaiDr.Item("SERIAL_NO") = ediM_Dr.Item("SERIAL_NO")
            zaiDr.Item("HOKAN_YN") = "1"
            zaiDr.Item("TAX_KB") = "01"
            zaiDr.Item("GOODS_COND_KB_1") = ""
            zaiDr.Item("GOODS_COND_KB_2") = ""
            zaiDr.Item("GOODS_COND_KB_3") = "00"
            zaiDr.Item("OFB_KB") = "01"
            zaiDr.Item("SPD_KB") = "01"
            zaiDr.Item("REMARK_OUT") = ""
            zaiDr.Item("PORA_ZAI_NB") = ediM_Dr.Item("OUTKA_TTL_NB")
            zaiDr.Item("ALCTD_NB") = ediM_Dr.Item("OUTKA_TTL_NB")
            zaiDr.Item("ALLOC_CAN_NB") = 0
            zaiDr.Item("IRIME") = ediM_Dr.Item("IRIME")
            zaiDr.Item("PORA_ZAI_QT") = ediM_Dr.Item("OUTKA_TTL_QT")
            zaiDr.Item("ALCTD_QT") = ediM_Dr.Item("OUTKA_TTL_QT")
            zaiDr.Item("ALLOC_CAN_QT") = 0
            zaiDr.Item("INKO_DATE") = ediL_Dr.Item("OUTKA_PLAN_DATE")
            zaiDr.Item("INKO_PLAN_DATE") = ediL_Dr.Item("OUTKA_PLAN_DATE")
            zaiDr.Item("ZERO_FLAG") = ""
            zaiDr.Item("LT_DATE") = ""
            zaiDr.Item("GOODS_CRT_DATE") = ""
            zaiDr.Item("DEST_CD_P") = ""
            zaiDr.Item("REMARK") = ""
            zaiDr.Item("SMPL_FLAG") = "00"

            'データセットに設定
            ds.Tables("LMH030_D_ZAI_TRS").Rows.Add(zaiDr)

        Next

        Return ds

    End Function

#End Region ' "データセット設定(在庫データ)"

#End Region ' "データセット設定(出荷S)"

    '富士フイルム 追加箇所 terakawa 2012.08.06 Start
#Region "データセット設定(EDI受信HED)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(HED))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvHed(ByVal ds As DataSet, Optional canHokokuF As Boolean = False) As DataSet

        Dim rcvDr As DataRow = ds.Tables("LMH030_EDI_RCV_HED").NewRow()
        Dim inDr As DataRow = ds.Tables("LMH030INOUT").Rows(0)
        Dim outkaediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        rcvDr("NRS_BR_CD") = outkaediDr("NRS_BR_CD")
        rcvDr("EDI_CTL_NO") = outkaediDr("EDI_CTL_NO")
        rcvDr("CUST_CD_L") = outkaediDr("CUST_CD_L")
        rcvDr("CUST_CD_M") = outkaediDr("CUST_CD_M")
        rcvDr("SYS_UPD_DATE") = inDr("RCV_SYS_UPD_DATE")
        rcvDr("SYS_UPD_TIME") = inDr("RCV_SYS_UPD_TIME")
        rcvDr("SYS_DEL_FLG") = "0"
        rcvDr("OUTKA_CTL_NO") = outkaediDr("OUTKA_CTL_NO")

        'キャンセルデータの場合のみ
        If canHokokuF = True Then
            'EDI出荷(大)の実績処理フラグを"1"にする
            outkaediDr("JISSEKI_FLAG") = "1"
        End If

        'データセットに設定
        ds.Tables("LMH030_EDI_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function

#End Region
    '富士フイルム 追加箇所 terakawa 2012.08.06 End

#Region "データセット設定(EDI受信DTL)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet, Optional canHokokuF As Boolean = False) As DataSet

        Dim rcvDr As DataRow
        Dim outkaedimDr As DataRow
        Dim outkaedilDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim outkaCtlNoChu As String = String.Empty

        For i As Integer = 0 To max

            outkaedimDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH030_EDI_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("SYS_DEL_FLG") = "0"

            'キャンセルデータの場合のみ
            If canHokokuF = True Then
                'EDI出荷(中)の実績処理フラグを"1"にする
                outkaedimDr("JISSEKI_FLAG") = "1"
            End If

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(作業)"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim outkaNoLM As String = String.Empty
        Dim num As New NumberMasterUtility

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

            For j As Integer = 1 To 5

                sagyoCD = ediDrM("OUTKA_KAKO_SAGYO_KB_" & j).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "00"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next

            For k As Integer = 1 To 2

                sagyoCD = ediDrM("SAGYO_KB_" & k).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    '2012.03.08 要望番号859 START
                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    '2012.03.08 要望番号859 END
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "01"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        If matomeFlg = False Then
            '通常登録
            unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
            unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("WH_CD") = ediDr("WH_CD")
            unsoDr("INOUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            unsoDr("TRIP_NO") = String.Empty
            unsoDr("UNSO_CD") = ediDr("UNSO_CD")
            unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
            unsoDr("BIN_KB") = ediDr("BIN_KB")
            unsoDr("JIYU_KB") = String.Empty
            unsoDr("DENP_NO") = String.Empty
            unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            unsoDr("OUTKA_PLAN_TIME") = String.Empty
            unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            unsoDr("ARR_ACT_TIME") = String.Empty
            unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
            unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
            unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
            unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
            unsoDr("DEST_CD") = ediDr("DEST_CD")
            unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
            'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
            unsoDr("UNSO_WT") = 0             '運送Mの集計値
            unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
            'unsoDr("PC_KB") = ediDr("PICK_KB")
            unsoDr("PC_KB") = ediDr("PC_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
            unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
            unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
            unsoDr("MOTO_DATA_KB") = "20"
            unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
            unsoDr("REMARK") = ediDr("UNSO_ATT")
            unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
            unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
            unsoDr("AD_3") = outkaLDr("DEST_AD_3")
            unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
            unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
            unsoDr("AREA_CD") = String.Empty
            '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
            'unsoDr("TYUKEI_HAISO_FLG") = String.Empty
            unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
            '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End
            unsoDr("SYUKA_TYUKEI_CD") = String.Empty
            unsoDr("HAIKA_TYUKEI_CD") = String.Empty
            unsoDr("TRIP_NO_SYUKA") = String.Empty
            unsoDr("TRIP_NO_TYUKEI") = String.Empty
            unsoDr("TRIP_NO_HAIKA") = String.Empty

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
               String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

                '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
                ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
                Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

                If MyBase.GetResultCount > 0 Then
                    unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                    unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
                End If

            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        Else
            'まとめ処理
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            unsoDr("NRS_BR_CD") = matomeDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = matomeDr("UNSO_NO_L")
            unsoDr("SYS_UPD_DATE") = matomeDr("SYS_UNSO_UPD_DATE")
            unsoDr("SYS_UPD_TIME") = matomeDr("SYS_UNSO_UPD_TIME")

            '2012.03.02 大阪暫定対応START
            '運送梱包個数の計算
            Dim unsoPkgNb As Long = 0
            Dim matomesakiUnsoPkgNb As Long = Convert.ToInt64(matomeDr("UNSO_PKG_NB"))
            Dim matomesakiOutkaPkgNb As Long = Convert.ToInt64(matomeDr("OUTKA_PKG_NB"))

            unsoDr("UNSO_PKG_NB") = Convert.ToInt64(outkaLDr("OUTKA_PKG_NB")) + matomesakiUnsoPkgNb - matomesakiOutkaPkgNb
            '2012.03.02 大阪暫定対応END

        End If
        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(運送M)"
    ''' <summary>
    ''' データセット設定(運送M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
            unsoMDr("ZAI_REC_NO") = String.Empty
            unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            If ediDr("TARE_YN").Equals("01") = False Then
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            Else
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            End If

            unsoMDr("SIZE_KB") = String.Empty
            unsoMDr("ZBUKA_CD") = String.Empty
            unsoMDr("ABUKA_CD") = String.Empty
            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")

            'データセットに設定
            ds.Tables("LMH030_UNSO_M").Rows.Add(unsoMDr)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L：運送重量)"
    ''' <summary>
    ''' データセット設定(運送L：運送重量)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0

        'まとめ(運送Mデータの運送重量合算)
        If matomeFlg = True Then

            'まとめ先の中データ取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectUnsoMatomeTarget", ds)
            If MyBase.GetResultCount = 0 Then
                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
                unsoLDr("NB_UT") = ediMDr("KB_UT")
                Return ds

            Else
                matomeUnsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_MATOME_UNSO_M")
                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoLDr("UNSO_WT") = Math.Ceiling(matomeUnsoJyuryo + unsoJyuryo)

                Return ds

            End If

        Else
            unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
            unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
            unsoLDr("NB_UT") = ediMDr("KB_UT")

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送重量再計算処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim unsoMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim NB As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables(tblNm).Rows(i)

            '運送M個数の算出（梱数 * 入数 + 端数）
            NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * NB + unsoJyuryo

        Next

        Return unsoJyuryo

    End Function
#End Region

#Region "データセット設定(EDI出荷M：運送重量必要項目)"
    ''' <summary>
    ''' データセット設定(EDI出荷M：運送重量必要項目)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer, _
                                              ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim drJudge As DataRow

        '標準重量
        ediMDr("STD_WT_KGS") = mGoodsDr("STD_WT_KGS")
        '標準入目
        ediMDr("STD_IRIME_NB") = mGoodsDr("STD_IRIME_NB")
        '風袋加算フラグ
        ediMDr("TARE_YN") = mGoodsDr("TARE_YN")

        '荷姿(区分マスタ)
        drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
        drJudge("KBN_CD") = ediMDr("PKG_UT")

        ds = MyBase.CallDAC(Me._DacCom, "SelectDataPkgUtZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        Return True

    End Function

#End Region

#End Region

#Region "まとめ先複数件の時出荷管理番号取得"

    ''' <summary>
    ''' まとめ先出荷管理番号の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function matomesakiOutkaNo(ByVal ds As DataSet) As String

        Dim max As Integer = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count - 1
        Dim concatOutkaNo As String = String.Empty
        Dim matomeOutkaNo As String = String.Empty

        For i As Integer = 0 To max

            'まとめ先出荷管理番号の取得
            matomeOutkaNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(i)("OUTKA_CTL_NO").ToString
            If i = 0 Then
                concatOutkaNo = matomeOutkaNo
            ElseIf i > 0 Then
                concatOutkaNo = String.Concat(concatOutkaNo, ",", matomeOutkaNo)
            End If

        Next

        Return concatOutkaNo

    End Function


#End Region

#Region "出荷登録処理(運賃作成)"

    ''' <summary>
    ''' 出荷登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "紐付け処理"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetEdiRcvDtl(ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 紐付けフラグの設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHimodukeFlg(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        dr.Item("MATCHING_FLAG") = "01"

        Return ds

    End Function

#End Region

#Region "LeftB"
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

    '↓FFEM特殊処理↓
    '2014.06.09 使用START

    ''' <summary>
    ''' 出荷(大)の完了取消設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetTK_OUT_L(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables("LMC020_OUTKA_L")
        Dim dr As DataRow = Nothing

        dr = dt.Rows(0)
        dr.Item("OUTKA_STATE_KB") = "50"
        dr.Item("END_DATE") = String.Empty
        dr.Item("TORIKESHI_FLG") = "01"
        'START SHINODA 要望管理2165
        If String.IsNullOrEmpty(dr.Item("END_DATE").ToString()) = True Then
            dr.Item("END_DATE2") = ""
        Else
            dr.Item("END_DATE2") = dr.Item("END_DATE").ToString()
        End If
        'END SHINODA 要望管理2165

    End Sub

    ''' <summary>
    ''' 作業の完了取消設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetTK_SAGYO(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables("LMC020_SAGYO")
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            dr.Item("SAGYO_COMP") = "00"
        Next

    End Sub

    ''' <summary>
    ''' 運送の完了取消設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetTK_UNSO(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables("LMC020_UNSO_L")
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            dr.Item("TORIKESI_FLG") = LMConst.FLG.ON
        Next

    End Sub

    ''' <summary>
    ''' 在庫振替削除の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetFuriDelDataSet(ByRef ds As DataSet, ByVal eventShubetsu As Integer) As DataSet

        'DataSet初期化
        ds.Tables("LMC020_FURI_DEL").Clear()

        Dim row As DataRow = ds.Tables("LMC020_FURI_DEL").NewRow
        row("NRS_BR_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()
        row("FURI_NO") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("FURI_NO").ToString()

        row("INKA_NO_L") = String.Empty
        row("INKA_NO_M") = String.Empty
        row("INKA_NO_S") = String.Empty
        row("ZAI_REC_NO") = String.Empty
        row("SYS_UPD_DATE_ZAI") = String.Empty
        row("SYS_UPD_TIME_ZAI") = String.Empty
        row("HIKIATE") = String.Empty
        row("ZAI_REC_CNT") = String.Empty
        ds.Tables("LMC020_FURI_DEL").Rows.Add(row)

        Dim InkaDs As DataSet = Me.SelectInkaData(ds)

        Return InkaDs

    End Function

    ''' <summary>
    ''' 削除フラグを設定
    ''' </summary>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetDelFlg(ByVal tblNm As String, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim maxCnt As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            If LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG")) = True OrElse _
                ("0").Equals(dr.Item("UP_KBN")) = True OrElse _
                ("2").Equals(dr.Item("UP_KBN")) = True Then
                '新規追加または削除のデータは、データセットからクリア
                ds.Tables(tblNm).Rows.Remove(dr)
                i = i - 1
                maxCnt = maxCnt - 1
            End If

            dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
            dr.Item("UP_KBN") = "2"

            If i = maxCnt = True Then
                Exit For
            End If

        Next

    End Sub

    ''' <summary>
    ''' 請求鑑データチェック用の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetKagamiDataSet(ByRef ds As DataSet, ByVal eventShubetsu As Integer) As Boolean

        'DataSet初期化
        ds.Tables("LMC020_KAGAMI_IN").Clear()

        '荷主マスタ取得
        Dim dr As DataRow = ds.Tables("LMC020IN").Rows(0)
        dr.Item("CUST_CD_S") = "00"
        dr.Item("CUST_CD_SS") = "00"
        Dim custDs As DataSet = Me.SelectCustData(ds)
        Dim custRow As DataRow = custDs.Tables("LMC020_CUST").Rows(0)

        '運賃計算締め基準の値によって、チェック対象の日付を変更
        Dim checkDate As String = String.Empty
        If ("01").Equals(custRow.Item("UNTIN_CALCULATION_KB")) = True Then
            checkDate = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_PLAN_DATE").ToString()
        Else
            checkDate = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("ARR_PLAN_DATE").ToString()
        End If
        'チェック対象の日付が未入力の場合は、処理を抜ける
        If String.IsNullOrEmpty(checkDate) = True Then
            Return True
        End If

        Dim row As DataRow = ds.Tables("LMC020_KAGAMI_IN").NewRow
        row("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        row("STORAGE_SEIQTO_CD") = String.Empty
        row("HANDLING_SEIQTO_CD") = String.Empty
        row("UNCHIN_SEIQTO_CD") = String.Empty
        row("SAGYO_SEIQTO_CD") = String.Empty
        row("YOKOMOCHI_SEIQTO_CD") = String.Empty

        row("STORAGE_SEIQTO_CD") = custRow.Item("HOKAN_SEIQTO_CD")
        row("HANDLING_SEIQTO_CD") = custRow.Item("NIYAKU_SEIQTO_CD")
        If 5.Equals(eventShubetsu) = False Then
            '完了取消はチェック対象外
            row("UNCHIN_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
        End If
        row("SAGYO_SEIQTO_CD") = custRow.Item("SAGYO_SEIQTO_CD")
        If 5.Equals(eventShubetsu) = False Then
            '完了取消はチェック対象外
            row("YOKOMOCHI_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
        End If
        row("CHECK_DATE") = checkDate

        ds.Tables("LMC020_KAGAMI_IN").Rows.Add(row)

        Return True

    End Function

    ''' <summary>
    ''' 引き当てた在庫を戻す
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function SetZaiRtn(ByVal ds As DataSet, ByVal eventShubetsu As Integer) As DataSet

        Dim dt As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim dr As DataRow = Nothing
        Dim max As Integer = 0
        Dim zaiDr As DataRow() = Nothing
        Dim outMDr As DataRow() = Nothing

        If 0 = dt.Rows.Count Then
            Return ds
        End If

        '引当在庫戻し処理
        max = dt.Rows.Count - 1
        For i As Integer = 0 To max
            dr = dt.Rows(i)

            If (LMConst.FLG.ON).Equals(dr.Item("SYS_DEL_FLG")) = True Then
                '削除データはスルー
                Continue For
            End If

            outMDr = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                                                                            "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))
            If ("01").Equals(outMDr(0).Item("ALCTD_KB")) = True OrElse _
                ("02").Equals(outMDr(0).Item("ALCTD_KB")) = True Then
                '出荷単位が個数・数量の場合のみ

                zaiDr = ds.Tables("LMC020_ZAI").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                                                                             "ZAI_REC_NO = '", dr.Item("ZAI_REC_NO"), "'"))
                If 3.Equals(eventShubetsu) = True Then
                    '完了取消時

                    '実予在庫梱数 = 実予在庫梱数(在庫) - 引当済梱数(出荷小)
                    zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '実予在庫数量 = 実予在庫数量(在庫) - 引当済数量(出荷小)
                    zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    '引当中梱数 = 引当中梱数(在庫) + 引当済梱数(出荷小)
                    zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '引当中数量 = 引当中数量(在庫) + 引当済数量(出荷小)
                    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    '引当可能梱数 は変わらず
                    '引当可能数量 は変わらず
                ElseIf 5.Equals(eventShubetsu) = True Then
                    '削除時

                    '実予在庫梱数 は変わらず
                    '実予在庫数量 は変わらず
                    '引当中梱数 = 引当中梱数(在庫) - 引当済梱数(出荷小)
                    zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '引当中数量 = 引当中数量(在庫) - 引当済数量(出荷小)
                    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    '引当可能梱数 = 引当可能梱数(在庫) + 引当済梱数(出荷小)
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '引当可能数量 = 引当可能数量(在庫) + 引当済数量(出荷小)
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                End If


                outMDr = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                                                                                "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))

                zaiDr(0).Item("UP_KBN") = "1"
            ElseIf ("03").Equals(outMDr(0).Item("ALCTD_KB")) = True Then
                '小分けの場合のみ
                zaiDr = ds.Tables("LMC020_ZAI").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                                                             "ZAI_REC_NO = '", dr.Item("ZAI_REC_NO"), "'"))
                If 3.Equals(eventShubetsu) = True Then
                    '完了取消時

                    '実予在庫梱数 = 実予在庫梱数(在庫) - 引当済梱数(出荷小)
                    zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                    ''実予在庫数量 = 実予在庫数量(在庫) - 引当済数量(出荷小)
                    'zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr.Item("IRIME")))
                    '実予在庫数量 = 実予在庫数量(在庫) - 引当済数量(出荷小)
                    zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                    '引当中梱数 = 引当中梱数(在庫) + 引当済梱数(出荷小)
                    zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                    ''引当中数量 = 引当中数量(在庫) + 引当済数量(出荷小)
                    'zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) + Convert.ToDecimal(dr.Item("IRIME")))
                    '引当中数量 = 引当中数量(在庫) + 引当済数量(出荷小)
                    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                    '引当可能梱数 は変わらず
                    '引当可能数量 は変わらず
                ElseIf 5.Equals(eventShubetsu) = True Then
                    '削除時

                    '実予在庫梱数 は変わらず
                    '実予在庫数量 は変わらず
                    '引当中梱数 = 引当中梱数(在庫) - 引当済梱数(出荷小)
                    zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '引当中数量 = 引当中数量(在庫) - 引当済数量(出荷小)
                    'START YANAI 20120717 小分け在庫
                    'zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr.Item("IRIME")))
                    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    'END YANAI 20120717 小分け在庫
                    '引当可能梱数 = 引当可能梱数(在庫) + 引当済梱数(出荷小)
                    zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr.Item("ALCTD_NB")))
                    '引当可能数量 = 引当可能数量(在庫) + 引当済数量(出荷小)
                    'START YANAI 20120717 小分け在庫
                    'zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr.Item("IRIME")))
                    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr.Item("ALCTD_QT")))
                    'END YANAI 20120717 小分け在庫
                End If

                outMDr = ds.Tables("").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                                                                                "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))

                If ("99").Equals(zaiDr(0).Item("ALCTD_KB_FLG")) = False Then
                    zaiDr(0).Item("ALCTD_KB_FLG") = "01"
                End If

                zaiDr(0).Item("UP_KBN") = "1"

            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function BlcDeleteData(ByVal ds As DataSet) As Boolean

        '請求日のチェック
        Call Me.IsSeiqDateChk(ds, "元黒出荷削除", LMConst.FLG.ON)
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        '在庫の論理削除
        Dim rtnResult As Boolean = Me.DeleteZaiData(ds)

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '振替データが存在するときのみ処理する
        If ds.Tables("LMC020_FURI_DEL").Rows.Count > 0 Then
            '2012.11.28 NAKAMURA 振替削除対応 START
            '入荷(大)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaLDelFlg")

            '入荷(中)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaMSysDelFlg")

            '入荷(小)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaSSysDelFlg")

            '振替先の在庫の論理削除（削除フラグを立てる）
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInZaiDelFlg")

        End If
        ' 2012.11.28 要望番号：612 振替一括削除対応 END Nakamura


        '出荷(大)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaLDelFlg")

        '出荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaMSysDelFlg")

        '出荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaSSysDelFlg")

        '2014/01/22 輸出情報追加 START
        '輸出情報のデータの論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateExportLSysDelFlg")
        '2014/01/22 輸出情報追加 END

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '振替の場合は運送、運賃の処理はしない。
        If ds.Tables("LMC020_FURI_DEL").Rows.Count = 0 Then
            '運送(大)の物理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoLData")

            '運送(中)の物理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoMData")

            '運賃の物理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinData")

            '要望番号：1612 terakawa 2012.12.03 Start
            '支払運賃の物理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
            '要望番号：1612 terakawa 2012.12.03 End
        End If

        '作業の物理削除
        rtnResult = rtnResult AndAlso Me.DeleteSagyoData(ds)

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '作業の物理削除 2012.12.13 入荷側の作業　物理削除追加
        If ds.Tables("LMC020_FURI_DEL").Rows.Count > 0 Then
            rtnResult = rtnResult AndAlso Me.DeleteSagyoDataIn(ds)
        End If
        ' 2012.11.28 要望番号：612 振替一括削除対応 END Nakamura

        ''2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ST--
        'rtnResult = rtnResult AndAlso Me.DelOutkaSendData(ds)
        ''2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ED--

        Return rtnResult

    End Function

    ''' <summary>
    ''' 請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="sysDelFlg">削除フラグ　初期値："0"</param>
    ''' <param name="selectFlg">運賃検索フラグ　初期値：False</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqDateChk(ByVal ds As DataSet, ByVal msg As String _
                                   , Optional ByVal sysDelFlg As String = "0" _
                                   , Optional ByVal selectFlg As Boolean = False _
                                   ) As DataSet

        '荷主マスタ取得
        Dim inDt As DataTable = ds.Tables("LMC020IN")
        Dim dr As DataRow = inDt.NewRow()
        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = outkaLDr.Item("NRS_BR_CD").ToString                '営業所コード
        'START YANAI 要望番号499
        'dr.Item("CUST_CD_L") = outkaLDr.Item("CUST_CD_L").ToString                '荷主コード（大）
        'dr.Item("CUST_CD_M") = outkaLDr.Item("CUST_CD_M").ToString                '荷主コード（中）
        'END YANAI 要望番号499
        inDt.Rows.Add(dr)
        Dim custInDr As DataRow = ds.Tables("LMC020IN").Rows(0)

        Dim custDs As DataSet = Nothing
        Dim custRow As DataRow = Nothing

        'START KIM 要望番号1510 2012/10/11
        If ds.Tables("LMC020_KAGAMI_IN").Rows.Count = 0 Then
            ds.Tables("LMC020_KAGAMI_IN").Rows.Add(ds.Tables("LMC020_KAGAMI_IN").NewRow())
            ds.Tables("LMC020_KAGAMI_IN").Rows(0).Item("NRS_BR_CD") = outkaLDr.Item("NRS_BR_CD").ToString
            ds.Tables("LMC020_KAGAMI_IN").Rows(0).Item("MSG") = msg
        End If
        'END KIM 要望番号1510 2012/10/11

        Dim indr As DataRow = ds.Tables("LMC020_KAGAMI_IN").Rows(0)
        Dim sql As String = String.Concat("SYS_DEL_FLG = '", sysDelFlg, "' ")
        Dim outkaMDrs As DataRow() = ds.Tables("LMC020_OUTKA_M").Select(sql)
        Dim max As Integer = outkaMDrs.Length - 1

        Dim chkDate As String = String.Empty
        Dim shukkaDate As String = outkaLDr.Item("OUTKA_PLAN_DATE").ToString()
        Dim nonyuDate As String = outkaLDr.Item("ARR_PLAN_DATE").ToString()

        Dim dt As DataTable = Nothing
        Dim sagyoDt As DataTable = ds.Tables("LMC020_SAGYO")
        Dim sagyoDr() As DataRow = sagyoDt.Select(sql)

        For i As Integer = 0 To max

            'START YANAI 要望番号499
            custInDr.Item("CUST_CD_L") = outkaMDrs(i).Item("CUST_CD_L_GOODS").ToString()
            custInDr.Item("CUST_CD_M") = outkaMDrs(i).Item("CUST_CD_M_GOODS").ToString()
            'END YANAI 要望番号499

            'S,SSを絞って抽出
            custInDr.Item("CUST_CD_S") = outkaMDrs(i).Item("CUST_CD_S").ToString()
            custInDr.Item("CUST_CD_SS") = outkaMDrs(i).Item("CUST_CD_SS").ToString()
            custDs = Me.SelectCustData(ds)
            custRow = custDs.Tables("LMC020_CUST").Rows(0)

            indr("STORAGE_SEIQTO_CD") = custRow.Item("HOKAN_SEIQTO_CD")
            indr("HANDLING_SEIQTO_CD") = custRow.Item("NIYAKU_SEIQTO_CD")
            indr("UNCHIN_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
            indr("SAGYO_SEIQTO_CD") = custRow.Item("SAGYO_SEIQTO_CD")
            indr("YOKOMOCHI_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")

            '入力チェック（請求鑑取得してチェック）
            ds = Me.SelectKagamiData(ds)

            '運賃計算締め基準の値によって、チェック対象の日付を変更
            chkDate = Me.GetChkDate(custRow, shukkaDate, nonyuDate)

            '終算日チェック
            If Me.SelectSubCustDataAtDateChk(custDs, chkDate, msg) = False Then
                Return ds
            End If

            '作業料取込日(始めの行のみチェック)
            dt = ds.Tables("LMC020_SAGYO_SKYU_DATE")
            If i = 0 AndAlso 0 < dt.Rows.Count AndAlso 0 < sagyoDr.Length Then
                If Me.IsSagyoSkyuCheck(dt.Rows(0), chkDate) = False Then
                    Return ds
                End If
            End If

        Next

        '運賃をチェックしない場合、スルー
        If selectFlg = False Then
            Return ds
        End If

        '運賃取込日(始めの行のみチェック)
        Call Me.ChkSeiqDateUnchin(ds, shukkaDate, nonyuDate, msg, selectFlg)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理チェック(LMC020GUI側)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True

        '引当済みチェック
        rtnResult = rtnResult AndAlso Me.IsHikiateChk(ds)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me.IsIdoTrsChk(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 引当済みチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHikiateChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMC020_FURI_DEL")
        Dim dr() As DataRow = dt.Select()
        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            '引当済みの場合、エラー
            If "済".Equals(dr(i).Item("HIKIATE").ToString()) = True Then
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E139", New String() {}, "", LMH010BLC.EXCEL_COLTITLE, "")
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E139", New String() {}, "", "入荷管理番号", String.Concat(dr(i).Item("INKA_NO_L").ToString(), "-", dr(i).Item("INKA_NO_M").ToString()))
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMC020_FURI_DEL")
        Dim dr() As DataRow = dt.Select()
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            '在庫移動がある場合、エラー
            If 0 < Convert.ToInt32(dr(i).Item("ZAI_REC_CNT").ToString()) Then
                'Return Me._Vcon.SetErrMessage("E148")
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E148", New String() {}, "", LMH010BLC.EXCEL_COLTITLE, "")
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E148", New String() {"振替データ", "元黒出荷削除"}, "", "振替管理番号", ds.Tables("LMC020_FURI_DEL").Rows(0).Item("FURI_NO").ToString())
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._MotoDelDac, "SelectSubCustData", ds)

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダテーブル検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKagamiData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._MotoDelDac, "SelectKagamiData", ds)

    End Function

    ''' <summary>
    ''' チェックする日付を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <returns>チェック日付</returns>
    ''' <remarks></remarks>
    Private Function GetChkDate(ByVal dr As DataRow, ByVal shukkaDate As String, ByVal nonyuDate As String) As String

        '運賃計算締め基準の値によって、チェック対象の日付を変更
        If ("01").Equals(dr.Item("UNTIN_CALCULATION_KB").ToString()) = True Then
            GetChkDate = shukkaDate
        Else
            GetChkDate = nonyuDate
        End If

        Return GetChkDate

    End Function

    ''' <summary>
    ''' 商品の荷主を取得し日付をチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="hokanDate">画面 起算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectSubCustDataAtDateChk(ByVal ds As DataSet, ByVal hokanDate As String, ByVal msg As String) As Boolean

        Dim custDt As DataTable = ds.Tables("LMC020_CUST")
        Dim calcDate As String = String.Empty
        If 0 < custDt.Rows.Count Then
            calcDate = custDt.Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString()
        End If

        'START SHINODA 要望管理2165
        '起算日、最終計算日チェック
        'Return Me.IsHokanShimeChk(hokanDate, calcDate, msg)
        If Me.IsHokanShimeChk(hokanDate, calcDate, msg) = False Then
            Return False
        End If

        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        If String.IsNullOrEmpty(outkaLDr.Item("END_DATE2").ToString()) = False AndAlso String.IsNullOrEmpty(calcDate) = False Then
            'すでに終算日が最終計算日より過去の場合、取消不可
            If outkaLDr.Item("END_DATE2").ToString() <= calcDate Then

                'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E375", New String() {"保管料・荷役料が既に計算されている", msg}, "", "", "")
                Return False

            End If
        End If

        Return True
        'END SHINODA 要望管理2165

    End Function

    ''' <summary>
    ''' 起算日、最終計算日チェック
    ''' </summary>
    ''' <param name="value1">画面 起算日</param>
    ''' <param name="value2">荷主M 最終計算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHokanShimeChk(ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '荷主M 最終計算日がない場合、スルー
        If String.IsNullOrEmpty(value2) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E375", New String() {"保管料・荷役料が既に計算されている", msg}, "", "", "")
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求日チェック(運賃料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="selectFlg">検索フラグ　True:検索有　False:検索無</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateUnchin(ByVal ds As DataSet, ByVal shukkaDate As String, ByVal nonyuDate As String, ByVal msg As String, ByVal selectFlg As Boolean) As Boolean

        'レコードがない場合、スルー
        Dim dt As DataTable = ds.Tables("LMC020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '削除レコードの場合、スルー
        If LMConst.FLG.ON.Equals(dt.Rows(0).Item("SYS_DEL_FLG").ToString()) Then
            Return True
        End If

        '運賃情報の取得
        Dim selectDs As DataSet = Nothing
        If selectFlg = True Then
            selectDs = Me.DacAccess(ds, "SelectUnchinData")
        Else
            selectDs = ds.Copy
        End If
        Dim selectDt As DataTable = selectDs.Tables("F_UNCHIN_TRS")
        Dim max As Integer = selectDt.Rows.Count - 1

        Dim chkDs As DataSet = selectDs.Copy
        Dim chkDt As DataTable = chkDs.Tables("F_UNCHIN_TRS")

        For i As Integer = 0 To max

            chkDt.Clear()
            chkDt.ImportRow(selectDt.Rows(i))

            '運賃の請求日チェック
            If Me.ChkDate(Me.GetChkDate(selectDt.Rows(i), shukkaDate, nonyuDate), Me.SelectGheaderData(chkDs, "SelectGheaderData"), msg, dt) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet, ByVal actionId As String) As String

        ds = Me.DacAccess(ds, actionId)

        Dim dt As DataTable = ds.Tables("LMC020_UNCHIN_SKYU_DATE")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String, ByVal dt As DataTable) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '運賃
            If ("40").Equals(dt.Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                '横持ちの場合
                'MyBase.SetMessage("E285", New String() {"横持ち料", msg})
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E285", New String() {"横持ち料", msg}, "", "", "")
            Else
                '運賃の場合
                'MyBase.SetMessage("E285", New String() {"運賃", msg})
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E285", New String() {"運賃", msg}, "", "", "")
            End If

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業料取込チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Private Function IsSagyoSkyuCheck(ByVal row As DataRow, ByVal checkDate As String) As Boolean

        If checkDate <= row.Item("SKYU_DATE").ToString Then
            'MyBase.SetMessage("E285", New String() {"作業料"})
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E285", New String() {"作業料", "元黒出荷削除"}, "", "", "")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMC020_ZAI")

        Return Me.ServerChkJudge(ds, "ComZaiTrsData")

    End Function

    ''' <summary>
    ''' 作業の論理削除(振替？)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoDataIn(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelSagyoDataIn")

    End Function

    ''' <summary>
    ''' 削除フラグを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDeleteData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.Rows(i).Item("SYS_DEL_FLG") = LMConst.FLG.ON
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 作業の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMC020_SAGYO")

        Return Me.ServerChkJudge(ds, "ComSagyoData")

    End Function

    ''' <summary>
    ''' 作業レコードの更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outkaNoL">出荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoData(ByVal ds As DataSet, ByVal outkaNoL As String) As Boolean

        '作業データの値設定
        ds = Me.SetSagyoToOutkaNoData(ds, outkaNoL)

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComSagyoData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 作業Noの設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outkaNoL">出荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoToOutkaNoData(ByVal ds As DataSet, ByVal outkaNoL As String) As DataSet

        Dim dt As DataTable = ds.Tables("LMC020_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            ''PKがない場合、設定
            'If String.IsNullOrEmpty(dt.Rows(i).Item("SAGYO_REC_NO").ToString()) = True Then
            '    dt.Rows(i).Item("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)
            'End If

            '出荷大番号が空の場合スルー(更新処理)
            If String.IsNullOrEmpty(outkaNoL) = True Then
                Continue For
            End If

            '入出荷管理番号L + Mの設定
            dt.Rows(i).Item("INOUTKA_NO_LM") = String.Concat(outkaNoL, dt.Rows(i).Item("INOUTKA_NO_LM").ToString())

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectFuriDelData", ds)

    End Function

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

    ''' <summary>
    ''' 原料プラント間転送か否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function IsMaterialPlantTransfer(ByVal ds As DataSet) As Boolean

        If ds.Tables.Contains("LMH030_OUTKAEDI_L") AndAlso ds.Tables("LMH030_OUTKAEDI_L").Rows.Count() > 0 AndAlso
            ds.Tables.Contains("LMH030_OUTKAEDI_M") AndAlso ds.Tables("LMH030_OUTKAEDI_M").Rows.Count() > 0 AndAlso
            ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("ZFVYHKKBN").ToString() = "2" AndAlso
            ds.Tables("LMH030_OUTKAEDI_M").Rows(0).Item("CUST_GOODS_CD").ToString().StartsWith("243") AndAlso
            ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("ZFVYDENTYP").ToString() = "ZUB1" Then
            ' 引当計上予実区分(ZFVYHKKBN) = '2'(出荷予定) かつ
            ' 品目コード(CUST_GOODS_CD[設定元元:MATNR]) の左 3桁が '243'(原料) かつ
            ' 伝票タイプ区分(ZFVYDENTYP) = 'ZUB1'(在庫転送オーダー) の場合
            ' 原料プラント間転送である
            Return True
        End If

        Return False

    End Function

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._MotoDelDac, actionId, ds)

    End Function

#End Region

    '↓FFEM特殊処理↓
    '2014.06.09 使用END

#End Region

#End Region

End Class
