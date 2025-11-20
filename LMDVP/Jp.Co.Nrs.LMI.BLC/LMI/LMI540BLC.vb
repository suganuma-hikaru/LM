' ' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI540BLC : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI540BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI540BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

#End Region ' "Const"

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI540DAC = New LMI540DAC()

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = ""
    Public Const EXCEL_COLTITLE_FILENAME As String = "取込ファイル名"

#End Region

#Region "Method"

#Region "取込処理前チェック/取込処理"

#Region "取込処理前チェックおよび関連処理"

#Region "取込処理前チェック"

    ''' <summary>
    ''' 取込前チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function TorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtHed As DataTable = ds.Tables("LMI540_TORIKOMI_HED")
        Dim dtDtl As DataTable = ds.Tables("LMI540_TORIKOMI_DTL")

        Dim dr As DataRow

        Dim max As Integer = dtDtl.Rows.Count - 1

        Dim iRowCnt As Integer = 0

        If dtHed.Rows(0).Item("ERR_FLG").ToString.Equals("1") Then
            ' 最初からエラーフラグが立っている場合（明細件数０件の場合）
            Dim sFileNm As String = dtHed.Rows(0).Item("FILE_NAME_RCV").ToString()
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E460", , , LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)

        Else

            For j As Integer = iRowCnt To max

                dr = dtDtl.Rows(j)

                If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtHed.Rows(0).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                    ' ヘッダと明細のファイル名称が等しい場合

                    ' 入力チェック(数値,日付チェック)
                    ' (チェックは省略列値を前行以前から補う前の取込値にて行う)
                    If Me.TorikomiValChk(dr) = False Then

                        ' 異常の場合

                        ' 詳細のエラーフラグに"1"をセットする
                        dr.Item("ERR_FLG") = "1"

                        ' ヘッダのエラーフラグに"1"をセットする
                        dtHed.Rows(0).Item("ERR_FLG") = "1"
                    Else
                        ' 正常の場合は処理無し（未処理（:9）の状態を保持するため）
                    End If
                Else
                    ' ヘッダと明細のファイル名称が等しくない場合
                    ' 現在行を保持してループを抜ける()
                    iRowCnt = j
                    Exit For
                End If

            Next

        End If

        Return ds

    End Function

#End Region ' "取込処理前チェック"

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

        ' オフラインNo.
        targetStr = dr.Item("COLUMN_1").ToString.Trim()
        ' 半角文字列長チェック
        If Not IsHalfSize(targetStr, 20, False) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("オフラインNo.(カラム1番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 依頼日
        sDate = dr.Item("COLUMN_2").ToString().Trim()
        ' 年月日チェック
        If IsDate(sDate) = True Then
        Else
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("依頼日(カラム2番目)[", sDate, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 依頼者
        targetStr = dr.Item("COLUMN_3").ToString.Trim()
        ' 字列長チェック
        If targetStr.Length > 20 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("依頼者(カラム3番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 出荷/回収元
        targetStr = dr.Item("COLUMN_4").ToString.Trim()
        Dim plantCd As String
        If targetStr.Length >= 4 Then
            plantCd = targetStr.Substring(0, 4)
        Else
            plantCd = targetStr
        End If
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷/回収元(カラム4番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
            ' 半角文字列長チェック
        ElseIf Not IsHalfSize(plantCd, 4, False) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷/回収元(カラム4番目)の左側のプラントコード部分[", plantCd, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        ElseIf lenb(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷/回収元(カラム4番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 回収/出荷/仕掛出荷
        targetStr = dr.Item("COLUMN_5").ToString.Trim()
        ' 字列長チェック
        If targetStr.Length > 20 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("回収/出荷/仕掛出荷(カラム5番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 出荷日
        sDate = dr.Item("COLUMN_6").ToString().Trim()
        ' 年月日チェック
        If IsDate(sDate) = True Then
        Else
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日(カラム6番目)[", sDate, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 納品日
        sDate = dr.Item("COLUMN_7").ToString().Trim()
        ' 年月日チェック
        If IsDate(sDate) = True Then
        Else
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("納品日(カラム7番目)[", sDate, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 郵便番号
        targetStr = dr.Item("COLUMN_8").ToString.Trim()
        ' 半角文字列長チェック
        If Not IsHalfSize(targetStr.Replace("-", ""), 9, False) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("郵便番号(カラム8番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 住所
        targetStr = dr.Item("COLUMN_9").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("住所(カラム9番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 会社名
        targetStr = dr.Item("COLUMN_10").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("会社名(カラム10番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 部署名
        targetStr = dr.Item("COLUMN_11").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("部署名(カラム11番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 担当者名
        targetStr = dr.Item("COLUMN_12").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("担当者名(カラム12番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 電話番号
        targetStr = dr.Item("COLUMN_13").ToString.Trim()
        ' 半角文字列長チェック
        If Not IsHalfSize(targetStr, 20, False) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("電話番号(カラム13番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 品名
        targetStr = dr.Item("COLUMN_14").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("品名(カラム14番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 製造ロット
        targetStr = dr.Item("COLUMN_15").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("製造ロット(カラム15番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 本数
        targetStr = dr.Item("COLUMN_16").ToString().Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(targetStr)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(targetStr) = 0 Then
                    MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
                    bRet = False
                ElseIf Convert.ToDouble(targetStr) < 0 Then
                    MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E185", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
                    bRet = False
                ElseIf dNum > 9999999999 Then
                    MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
                    bRet = False
                Else
                    If targetStr.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(targetStr.Substring(targetStr.IndexOf(".") + 1)) > 0 Then
                        MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("本数(カラム16番目)[", targetStr, "]")}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        ' 温度条件
        targetStr = dr.Item("COLUMN_17").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 10 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("温度条件(カラム17番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 毒劇物
        targetStr = dr.Item("COLUMN_18").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 10 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("毒劇物(カラム18番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 備考欄
        targetStr = dr.Item("COLUMN_19").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("備考欄(カラム19番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
        End If

        ' 配送便
        targetStr = dr.Item("COLUMN_20").ToString.Trim()
        ' 字列長チェック
        If LenB(targetStr) > 200 Then
            MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("配送便(カラム20番目)[", targetStr, "]"), ""}, sRecNo, LMI540BLC.EXCEL_COLTITLE_FILENAME, sFileNm)
            bRet = False
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

#End Region ' "取込処理前チェックおよび関連処理"

#Region "取込処理"

    Private Function Torikomi(ByVal ds As DataSet) As DataSet

        Dim dtHed As DataTable = ds.Tables("LMI540_TORIKOMI_HED")    ' 取込Hed
        Dim dtDtl As DataTable = ds.Tables("LMI540_TORIKOMI_DTL")    ' 取込Dtl

        ' 営業所コード
        Dim nrsBrCd As String = dtHed.Rows(0).Item("NRS_BR_CD").ToString()

        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        For i As Integer = 0 To dtDtl.Rows.Count - 1
            Dim drDtl As DataRow = dtDtl.Rows(i)

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒FFEM オフライン入出荷受信データ
            '---------------------------------------------------------------------------
            Dim drImport As DataRow = ds.Tables("LMI540IN_IMPORT").NewRow()
            ds.Tables("LMI540IN_IMPORT").Clear()
            '--
            drImport.Item("KEY_NO") = "0"
            drImport.Item("OFFLINE_NO") = drDtl.Item("COLUMN_1").ToString().Trim()
            drImport.Item("IRAI_DATE") = Date.Parse(drDtl.Item("COLUMN_2").ToString().Trim()).ToString("yyyyMMdd")
            drImport.Item("IRAI_SYA") = drDtl.Item("COLUMN_3").ToString().Trim()
            drImport.Item("MOTO") = drDtl.Item("COLUMN_4").ToString().Trim()
            drImport.Item("PLANT_CD") = drDtl.Item("COLUMN_4").ToString().Substring(0, 4)
            drImport.Item("NRS_BR_CD") = ""
            drImport.Item("SHUBETSU") = drDtl.Item("COLUMN_5").ToString().Trim()
            drImport.Item("SAP_NO") = ""
            drImport.Item("OUTKA_DATE") = Date.Parse(drDtl.Item("COLUMN_6").ToString().Trim()).ToString("yyyyMMdd")
            drImport.Item("ARR_DATE") = Date.Parse(drDtl.Item("COLUMN_7").ToString().Trim()).ToString("yyyyMMdd")
            drImport.Item("ZIP") = drDtl.Item("COLUMN_8").ToString().Trim().Replace("-", "")
            drImport.Item("DEST_AD") = drDtl.Item("COLUMN_9").ToString()
            drImport.Item("COMP_NM") = drDtl.Item("COLUMN_10").ToString().Trim()
            drImport.Item("BUSYO_NM") = drDtl.Item("COLUMN_11").ToString().Trim()
            drImport.Item("TANTO_NM") = drDtl.Item("COLUMN_12").ToString().Trim()
            drImport.Item("TEL") = drDtl.Item("COLUMN_13").ToString().Trim()
            drImport.Item("GOODS_NM") = drDtl.Item("COLUMN_14").ToString().Trim()
            drImport.Item("LOT_NO") = drDtl.Item("COLUMN_15").ToString().Trim()
            drImport.Item("INOUTKA_NB") = drDtl.Item("COLUMN_16").ToString().Trim()
            drImport.Item("ONDO") = drDtl.Item("COLUMN_17").ToString().Trim()
            drImport.Item("DOKUGEKI") = drDtl.Item("COLUMN_18").ToString().Trim()
            drImport.Item("REMARK") = drDtl.Item("COLUMN_19").ToString().Trim()
            drImport.Item("HAISO") = drDtl.Item("COLUMN_20").ToString().Trim()
            drImport.Item("SHIZI_KB") = "00"
            drImport.Item("NOHIN_KB") = "00"
            '--
            ds.Tables("LMI540IN_IMPORT").Rows.Add(drImport)

            '---------------------------------------------------------------------------
            ' 区分マスタ('F017') 存在チェック/取得
            '---------------------------------------------------------------------------
            ' 届先マスタ読込
            ds = MyBase.CallDAC(Me._Dac, "SelectKbnF017", ds)
            ' 抽出件数判定
            If MyBase.GetResultCount = 0 Then
                Dim plantCd As String = drImport.Item("PLANT_CD").ToString().Trim()
                ' エラー返却
                MyBase.SetMessageStore(LMI540BLC.GUIDANCE_KBN, "E493",
                                               New String() {String.Concat("出荷/回収元のの左側のプラントコード部分:", plantCd), "区分マスタ(F017)", ""},
                                               dtDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                               LMI540BLC.EXCEL_COLTITLE_FILENAME, dtDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                bNoErr = False
                Continue For
            End If

            ' 区分マスタより取得の営業所コード(KBN_NM3)の、FFEM オフライン入出荷受信データ.同項目への設定
            ds.Tables("LMI540IN_IMPORT").Rows(0).Item("NRS_BR_CD") = ds.Tables("LMI540OUT_KBN_F017").Rows(0).Item("NRS_BR_CD")

            '---------------------------------------------------------------------------
            ' FFEM オフライン入出荷受信データの新規追加
            '---------------------------------------------------------------------------
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaediDtlFjfOff", ds)

        Next

        If bNoErr Then
            'エラー無し
            dtHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        Return ds

    End Function

#End Region ' "取込処理"

#End Region ' "取込処理前チェック/取込処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SearchCount", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SearchSelect", ds)
        Return rtnDs

    End Function

#End Region ' "検索処理"

#Region "印刷処理"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrtFlg(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdatePrtFlg", ds)

    End Function

#End Region

#End Region ' "Method"

End Class
