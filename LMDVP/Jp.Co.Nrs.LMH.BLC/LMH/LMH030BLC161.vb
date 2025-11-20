' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID        :  161       : アフトンケミカル(大阪)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC161
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC161 = New LMH030DAC161()

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

#Region "アフトンケミカル CONST"

    ''' <summary>
    ''' その他
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_CTL_NO As String = "T00000000"             '管理番号初期値
    Public Const DEF_UNSO_NO_L As String = "01-T00000000"       '運送番号初期値
    Public Const DEF_UNSO_NO_M As String = "01-T00000000000"    '運送番号初期値

#End Region ' "アフトンケミカル CONST"

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

        ' Loading Date
        ' （【出荷日】→画面.出荷予定日）
        sDate = dr.Item("COLUMN_1").ToString().Trim()
        ' 年月日チェック
        If IsDate(sDate) = True Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("Loading Date(カラム1番目)[", sDate, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' Ship To
        ' （【届先コード】→画面.届先コード）
        targetStr = dr.Item("COLUMN_3").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("Ship To(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("Ship To(カラム3番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' Delivery
        ' （【荷主注文番号（全体）】→画面.オーダー番号）
        targetStr = dr.Item("COLUMN_5").ToString().Trim()
        ' 文字列長チェック
        If LenB(targetStr) > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("Delivery(カラム5番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' Mat Number
        ' （【商品コード】→画面.商品コード）
        targetStr = dr.Item("COLUMN_7").ToString.Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("Mat Number(カラム7番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            ' 半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("Mat Number(カラム7番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' Batch
        ' （【ロット№】→画面.ロット№）
        targetStr = dr.Item("COLUMN_10").ToString.Trim()
        If LenB(targetStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("Batch(カラム10番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        ' Order Quantity
        targetStr = dr.Item("COLUMN_11").ToString().Trim()
        ' 必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(targetStr)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(targetStr) = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                ElseIf Convert.ToDouble(targetStr) < 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                ElseIf dNum > 9999999999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    ' （→画面.個数 (UOM(カラム12番目) が "KG" の場合は重量であるため小数点チェック不要)）
                    If dr.Item("COLUMN_12").ToString().Trim() <> "KG" Then
                        If targetStr.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(targetStr.Substring(targetStr.IndexOf(".") + 1)) > 0 Then
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("Order Quantity(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                            bRet = False
                        End If
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

        Dim gyo As Integer = 0

        Dim destDict As Dictionary(Of String, DataRow) = New Dictionary(Of String, DataRow)
        Dim notFoundDestCdSet As HashSet(Of String) = New HashSet(Of String)
        Dim goodsDict As Dictionary(Of String, DataTable) = New Dictionary(Of String, DataTable)
        Dim notFoundGoodsCdSet As HashSet(Of String) = New HashSet(Of String)
        Dim goodsDetailsDict As Dictionary(Of String, DataRow) = New Dictionary(Of String, DataRow)
        Dim notFoundGoodsDetailsCdSet As HashSet(Of String) = New HashSet(Of String)

        ' 取込Dtlの並べ替え（5列目: Delivery 昇順）
        Dim tmpDt As DataTable = dtSetDtl.Clone()
        Dim tmpDr() As DataRow = Nothing
        tmpDr = dtSetDtl.Select(String.Empty, "COLUMN_5 ASC")
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
            Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_AFT").NewRow()
            gyo += 1
            ds.Tables("LMH030_H_OUTKAEDI_DTL_AFT").Clear() ' 受信DTLのクリア
            ds = Me.SetSemiOutkaEdiRcv(ds, i, gyo, drEdiRcvDtl)

            '---------------------------------------------------------------------------
            ' キー項目設定
            '---------------------------------------------------------------------------
            sNewKey = drEdiRcvDtl.Item("CUST_ORD_NO").ToString()

            If i = 0 Then
                '1番目は必ずbSameKeyFlgはFalse
                bSameKeyFlg = False
            Else
                '2番目以降はキーを比較
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                End If
            End If

            Dim setDs As DataSet = New Jp.Co.Nrs.LM.DSL.LMH030DS()
            Dim ediRecCnt As Integer = 0

            If drEdiRcvDtl.Item("CUST_ORD_NO").ToString().Trim().Equals("") = False Then
                '---------------------------------------------------------------------------
                ' EDI出荷データ件数および出荷データL 出荷管理番号L 等 取得処理
                '---------------------------------------------------------------------------
                setDs.Tables("LMH030_H_OUTKAEDI_DTL_AFT").ImportRow(drEdiRcvDtl)
                setDs = MyBase.CallDAC(Me._Dac, "GetOutkaCntAndNoL", setDs)

                ediRecCnt = MyBase.GetResultCount()
                Dim outkaNoL As String = setDs.Tables("LMH030_H_OUTKAEDI_DTL_AFT").Rows(0).Item("OUTKA_CTL_NO").ToString()
                If ediRecCnt > 0 AndAlso (outkaNoL.Equals("") = False) Then
                    ' 出荷EDI存在かつ出荷存在（「出荷管理番号L IS NULL → ""」でない）の場合
                    ' 当該行はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E847", New String() {outkaNoL},
                                       dtSetDtl.Rows(i).Item("REC_NO").ToString().Trim(),
                                       LMH030BLC.EXCEL_COLTITLE_SEMIEDI, dtSetDtl.Rows(i).Item("FILE_NAME_RCV").ToString().Trim())
                    bNoErr = False
                    Continue For
                End If
            End If

            '---------------------------------------------------------------------------
            ' EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            '---------------------------------------------------------------------------
            ds = Me.GetEdiCtlNo(ds, iDeleteFlg, iSkipFlg, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu, drEdiRcvDtl)

            If bSameKeyFlg = False Then
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
                        Dim sDenNo As String = drEdiRcvDtl.Item("CUST_ORD_NO").ToString().Trim()

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
            setDs.Tables("LMH030_H_OUTKAEDI_DTL_AFT").Clear()
            setDs.Tables("LMH030_H_OUTKAEDI_DTL_AFT").ImportRow(drEdiRcvDtl)
            setDs.Tables("LMH030_SEMIEDI_INFO").ImportRow(ds.Tables("LMH030_SEMIEDI_INFO").Rows(0))

            ' 新規追加 EDI受信データ(DTL) と同一キーレコードの件数取得
            setDs = MyBase.CallDAC(Me._Dac, "GetOutkaEdiDtlCnt", setDs)
            If MyBase.GetResultCount() > 0 Then
                ' 同一キーレコード存在時の物理削除
                setDs = MyBase.CallDAC(Me._Dac, "DeleteOutkaEdiDtl", setDs)
                iRcvDtlCanCnt = iRcvDtlCanCnt + 1
            End If

            ' EDI受信データ(DTL) の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

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

                setDs.Tables("LMH030_M_GOODS_DETAILS").Clear()
                If goodsDetailsDict.ContainsKey(custGoodsCd) Then
                    setDs.Tables("LMH030_M_GOODS_DETAILS").ImportRow(goodsDetailsDict(custGoodsCd))
                ElseIf Not notFoundGoodsDetailsCdSet.Contains(custGoodsCd) Then
                    ' 商品明細マスタ読込処理
                    setDs = MyBase.CallDAC(Me._Dac, "SelectGoodsDetails", setDs)
                    If MyBase.GetResultCount = 0 Then
                        notFoundGoodsDetailsCdSet.Add(custGoodsCd)
                    Else
                        goodsDetailsDict(custGoodsCd) = setDs.Tables("LMH030_M_GOODS_DETAILS").Rows(0)
                    End If
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

#Region "データセット設定 EDI出荷受信テーブル(明細)"

    ''' <summary>
    ''' データセット設定 EDI出荷受信テーブル(明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="i"></param>
    ''' <param name="drEdiRcvDtl"></param>
    ''' <returns></returns>
    Private Function SetSemiOutkaEdiRcv(ByVal ds As DataSet, ByVal i As Integer, ByVal gyo As Integer, ByVal drEdiRcvDtl As DataRow) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)


        ' EDI受信DTL設定
        drEdiRcvDtl("DEL_KB") = "0"
        drEdiRcvDtl("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("FILE_NAME") = drSetDtl("FILE_NAME_RCV")
        drEdiRcvDtl("REC_NO") = Convert.ToInt32(drSetDtl("REC_NO")).ToString("00000")
        drEdiRcvDtl("GYO") = Right(String.Concat("000", gyo.ToString()), 3)
        drEdiRcvDtl("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl("EDI_CTL_NO") = String.Empty                                                            ' 後でセットする。
        drEdiRcvDtl("EDI_CTL_NO_CHU") = String.Empty                                                        ' 後でセットする。
        drEdiRcvDtl("OUTKA_CTL_NO") = DEF_CTL_NO
        drEdiRcvDtl("OUTKA_CTL_NO_CHU") = "000"
        drEdiRcvDtl("CUST_CD_L") = drSemiEdiInfo("CUST_CD_L")                                               ' 荷主コード（大）
        drEdiRcvDtl("CUST_CD_M") = drSemiEdiInfo("CUST_CD_M")                                               ' 荷主コード（中）
        drEdiRcvDtl("PRTFLG") = "0"                                                                         ' プリントフラグ

        drEdiRcvDtl("OUTKA_PLAN_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_1").ToString().Trim(), 10)          ' 出荷日　　　　　　　　　←受信データ(Excel) Loading Date
        drEdiRcvDtl("P_DELIVERY_DATE") = Me._Blc.LeftB(drSetDtl("COLUMN_2").ToString().Trim(), 100)         ' P-Delivery Date 　　　　←受信データ(Excel) P-Delivery Date
        drEdiRcvDtl("DEST_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_3").ToString().Trim(), 15)                  ' 届先コード　　　　　　　←受信データ(Excel) Ship To
        drEdiRcvDtl("SHIPMENT_NUMBER") = Me._Blc.LeftB(drSetDtl("COLUMN_4").ToString().Trim(), 100)         ' Shipment Number 　　　　←受信データ(Excel) Shipment Number
        drEdiRcvDtl("CUST_ORD_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_5").ToString().Trim(), 30)              ' 荷主注文番号（全体）　　←受信データ(Excel) Delivery
        drEdiRcvDtl("ORDER_SEQ") = Me._Blc.LeftB(drSetDtl("COLUMN_6").ToString().Trim(), 100)               ' Order 　　　　　　　　　←受信データ(Excel) Order
        drEdiRcvDtl("CUST_GOODS_CD") = Me._Blc.LeftB(drSetDtl("COLUMN_7").ToString().Trim(), 20)            ' 商品コード　　　　　　　←受信データ(Excel) Mat Number
        drEdiRcvDtl("MATERIAL_DESCRIPTION") = Me._Blc.LeftB(drSetDtl("COLUMN_8").ToString().Trim(), 100)    ' Material Description　　←受信データ(Excel) Material Description
        drEdiRcvDtl("CUSTOMER_MATERIAL_DESC") = Me._Blc.LeftB(drSetDtl("COLUMN_9").ToString().Trim(), 100)  ' Customer Material Desc　←受信データ(Excel) Customer Material Desc
        drEdiRcvDtl("LOT_NO") = Me._Blc.LeftB(drSetDtl("COLUMN_10").ToString().Trim(), 40)                  ' ロット№　　　　　　　　←受信データ(Excel) Batch
        drEdiRcvDtl("ORDER_QUANTITY") = Me._Blc.LeftB(drSetDtl("COLUMN_11").ToString().Trim(), 100)         ' Order Quantity　　　　　←受信データ(Excel) Order Quantity
        drEdiRcvDtl("UOM") = Me._Blc.LeftB(drSetDtl("COLUMN_12").ToString().Trim(), 100)                    ' UOM 　　　　　　　　　　←受信データ(Excel) UOM
        drEdiRcvDtl("CARRIER_NAME") = Me._Blc.LeftB(drSetDtl("COLUMN_13").ToString().Trim(), 100)           ' Carrier Name　　　　　　←受信データ(Excel) Carrier Name
        drEdiRcvDtl("CUSTOMER_NAME") = Me._Blc.LeftB(drSetDtl("COLUMN_14").ToString().Trim(), 100)          ' Customer Name 　　　　　←受信データ(Excel) Customer Name
        drEdiRcvDtl("LOAD_POINT") = Me._Blc.LeftB(drSetDtl("COLUMN_15").ToString().Trim(), 100)             ' Load Point　　　　　　　←受信データ(Excel) Load Point
        drEdiRcvDtl("PLANT") = Me._Blc.LeftB(drSetDtl("COLUMN_16").ToString().Trim(), 100)                  ' Plant 　　　　　　　　　←受信データ(Excel) Plant
        drEdiRcvDtl("SHIP_TO_CITY") = Me._Blc.LeftB(drSetDtl("COLUMN_17").ToString().Trim(), 100)           ' Ship-to City　　　　　　←受信データ(Excel) Ship-to City

        'データセットに設定
        ds.Tables("LMH030_H_OUTKAEDI_DTL_AFT").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region ' "データセット設定 EDI出荷受信テーブル(明細)"

#Region "データセット設定 EDI出荷データ 用 EDI管理番号(大・中)"

    ''' <summary>
    ''' データセット設定 EDI出荷データ 用 EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNo(ByVal ds As DataSet _
                               , ByVal iDeleteFlg As Integer, ByVal iSkipFlg As Integer, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer _
                                , ByRef drEdiRcvDtl As DataRow) As DataSet

        Dim sNrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_AFT").Rows(0).Item("NRS_BR_CD").ToString()


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
            drEdiRcvDtl.Item("EDI_CTL_NO") = DEF_CTL_NO             ' DTLに固定値をセット
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
        drOutkaEdiM("CUST_ORD_NO_DTL") = drEdiRcvDtl("CUST_ORD_NO")                 ' 荷主注文番号（明細単位）←EDI出荷受信テーブル.荷主注文番号（全体）　　←受信データ(Excel) Delivery
        drOutkaEdiM("BUYER_ORD_NO_DTL") = String.Empty
        drOutkaEdiM("CUST_GOODS_CD") = drGoods("GOODS_CD_CUST")                     ' 商品コード　　　　　　　←商品マスタ.商品コード
        drOutkaEdiM("NRS_GOODS_CD") = String.Empty
        drOutkaEdiM("GOODS_NM") = drGoods("GOODS_NM_1")                             ' 商品名　　　　　　　　　←商品マスタ.商品名1

        drOutkaEdiM("RSV_NO") = String.Empty
        drOutkaEdiM("LOT_NO") = drEdiRcvDtl("LOT_NO")                               ' ロット№　　　　　　　　←EDI出荷受信テーブル.ロット№　　　　　　　　←受信データ(Excel) Batch
        drOutkaEdiM("SERIAL_NO") = String.Empty
        drOutkaEdiM("ALCTD_KB") = "01"

        ' 出荷総個数←EDI出荷受信テーブル.Order Quantity←受信データ(Excel) Order Quantity
        ' ただし、商品明細の設定値が設定されている場合は Order Quantity / 設定値 で算出する。
        Dim outkaTtlNb As String = String.Empty
        If setDs.Tables("LMH030_M_GOODS_DETAILS").Rows.Count > 0 Then
            Dim setNaiyo As String = setDs.Tables("LMH030_M_GOODS_DETAILS").Rows(0).Item("SET_NAIYO").ToString()
            Dim dSetNaiyo As Double
            If Double.TryParse(setNaiyo, dSetNaiyo) Then
                Dim dOrderQuantity As Double = Convert.ToDouble(drEdiRcvDtl("ORDER_QUANTITY"))
                Dim dNum As Double = Math.Floor(dOrderQuantity / dSetNaiyo) + If(dOrderQuantity Mod dSetNaiyo > 0, 1, 0)
                outkaTtlNb = System.Math.Ceiling(dNum).ToString()
            Else
                outkaTtlNb = drEdiRcvDtl("ORDER_QUANTITY").ToString()
            End If
        Else
            outkaTtlNb = drEdiRcvDtl("ORDER_QUANTITY").ToString()
        End If

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

        drOutkaEdiM("KB_UT") = String.Empty
        drOutkaEdiM("QT_UT") = String.Empty
        drOutkaEdiM("PKG_NB") = drGoods("PKG_NB")
        drOutkaEdiM("PKG_UT") = String.Empty
        drOutkaEdiM("ONDO_KB") = String.Empty
        drOutkaEdiM("UNSO_ONDO_KB") = String.Empty

        drOutkaEdiM("IRIME") = drGoods("STD_IRIME_NB")
        drOutkaEdiM("IRIME_UT") = String.Empty
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
        drOutkaEdiM("FREE_C01") = drEdiRcvDtl("MATERIAL_DESCRIPTION").ToString()     ' 文字列01　　　　　　　　←EDI出荷受信テーブル.Material Description　　←受信データ(Excel) Material Description
        drOutkaEdiM("FREE_C02") = drEdiRcvDtl("CUSTOMER_MATERIAL_DESC").ToString()   ' 文字列02　　　　　　　　←EDI出荷受信テーブル.Customer Material Desc　←受信データ(Excel) Customer Material Desc
        drOutkaEdiM("FREE_C03") = drEdiRcvDtl("ORDER_QUANTITY").ToString()           ' 文字列03　　　　　　　　←EDI出荷受信テーブル.Order Quantity　　　　　←受信データ(Excel) Order Quantity
        drOutkaEdiM("FREE_C04") = drEdiRcvDtl("UOM").ToString()                      ' 文字列04　　　　　　　　←EDI出荷受信テーブル.UOM 　　　　　　　　　　←受信データ(Excel) UOM
        drOutkaEdiM("FREE_C05") = drEdiRcvDtl("CARRIER_NAME").ToString()             ' 文字列05　　　　　　　　←EDI出荷受信テーブル.Carrier Name　　　　　　←受信データ(Excel) Carrier Name
        drOutkaEdiM("FREE_C06") = drEdiRcvDtl("CUSTOMER_NAME").ToString()            ' 文字列06　　　　　　　　←EDI出荷受信テーブル.Customer Name 　　　　　←受信データ(Excel) Customer Name
        drOutkaEdiM("FREE_C07") = drEdiRcvDtl("LOAD_POINT").ToString()               ' 文字列07　　　　　　　　←EDI出荷受信テーブル.Load Point　　　　　　　←受信データ(Excel) Load Point
        drOutkaEdiM("FREE_C08") = drEdiRcvDtl("PLANT").ToString()                    ' 文字列08　　　　　　　　←EDI出荷受信テーブル.Plant 　　　　　　　　　←受信データ(Excel) Plant
        drOutkaEdiM("FREE_C09") = drEdiRcvDtl("SHIP_TO_CITY").ToString()             ' 文字列09　　　　　　　　←EDI出荷受信テーブル.Ship-to City　　　　　　←受信データ(Excel) Ship-to City
        drOutkaEdiM("FREE_C10") = String.Empty
        drOutkaEdiM("FREE_C11") = String.Empty
        drOutkaEdiM("FREE_C12") = String.Empty
        drOutkaEdiM("FREE_C13") = String.Empty
        drOutkaEdiM("FREE_C14") = String.Empty
        drOutkaEdiM("FREE_C15") = String.Empty
        drOutkaEdiM("FREE_C16") = String.Empty
        drOutkaEdiM("FREE_C17") = String.Empty
        drOutkaEdiM("FREE_C18") = String.Empty
        drOutkaEdiM("FREE_C19") = String.Empty
        drOutkaEdiM("FREE_C20") = String.Empty
        drOutkaEdiM("FREE_C21") = String.Empty
        drOutkaEdiM("FREE_C22") = String.Empty
        drOutkaEdiM("FREE_C23") = String.Empty
        drOutkaEdiM("FREE_C24") = String.Empty
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

        ' 出荷日←EDI出荷受信テーブル.出荷日←受信データ(Excel) Loading Date (YYYYMMDD 形式変換)
        Dim outkaPlanDate As String = Date.Parse(drEdiRcvDtl("OUTKA_PLAN_DATE").ToString().Trim()).ToString("yyyyMMdd")
        drOutkaEdiL("OUTKA_PLAN_DATE") = outkaPlanDate                              ' 出荷日　　　　　　　←上記 出荷日
        drOutkaEdiL("OUTKO_DATE") = outkaPlanDate                                   ' 出庫日　　　　　　　←上記 出荷日

        ' 出荷日の 翌営業日の取得
        Dim oneBussinessDay1After As String = Left(Convert.ToString(Me.GetBussinessDayKrt(outkaPlanDate, 1, setDs)).Replace("/", String.Empty), 8)
        drOutkaEdiL("ARR_PLAN_DATE") = oneBussinessDay1After                        ' 納入予定日　　　　　←出荷日の 翌営業日
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

        ' 届先コード←EDI出荷受信テーブル.届先コード←受信データ(Excel) Ship To
        Dim destCd As String = drEdiRcvDtl("DEST_CD").ToString()
        drOutkaEdiL("EDI_DEST_CD") = destCd                                         ' EDI届先コード 　　　←上記 届先コード
        drOutkaEdiL("DEST_CD") = destCd                                             ' 届先コード　　　　　←上記 届先コード
        drOutkaEdiL("DEST_NM") = drDest("DEST_NM")                                  ' 届先名　　　　　　　←届先マスタ.届先名称
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
        drOutkaEdiL("CUST_ORD_NO") = drEdiRcvDtl("CUST_ORD_NO")                     ' 荷主注文番号（全体）←EDI出荷受信テーブル.荷主注文番号（全体）　　←受信データ(Excel) Delivery
        drOutkaEdiL("BUYER_ORD_NO") = String.Empty
        drOutkaEdiL("UNSO_MOTO_KB") = String.Empty
        drOutkaEdiL("UNSO_TEHAI_KB") = String.Empty
        drOutkaEdiL("SYARYO_KB") = String.Empty
        drOutkaEdiL("BIN_KB") = "01"
        drOutkaEdiL("UNSO_CD") = String.Empty
        drOutkaEdiL("UNSO_NM") = String.Empty
        drOutkaEdiL("UNSO_BR_CD") = String.Empty
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

        drOutkaEdiL("FREE_C01") = drEdiRcvDtl("P_DELIVERY_DATE")                    ' 文字列01　　　　　　←EDI出荷受信テーブル.P-Delivery Date 　　　←受信データ(Excel) P-Delivery Date
        drOutkaEdiL("FREE_C02") = drEdiRcvDtl("SHIPMENT_NUMBER")                    ' 文字列02　　　　　　←EDI出荷受信テーブル.Shipment Number 　　　←受信データ(Excel) Shipment Number
        drOutkaEdiL("FREE_C03") = drEdiRcvDtl("CUST_ORD_NO")                        ' 文字列03　　　　　　←EDI出荷受信テーブル.荷主注文番号（全体）　←受信データ(Excel) Delivery
        drOutkaEdiL("FREE_C04") = drEdiRcvDtl("ORDER_SEQ")                          ' 文字列04　　　　　　←EDI出荷受信テーブル.Order 　　　　　　　　←受信データ(Excel) Order
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
        drOutkaEdiL("FREE_C23") = String.Empty
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

#Region "営業日取得"

    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetBussinessDayKrt(ByVal sStartDay As String, ByVal iBussinessDays As Integer, ByVal setDs As DataSet) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        Dim drHOL As DataRow

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(sStartDay))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合
                    setDs.Tables("LMH030_M_HOL").Clear()

                    '休日マスタ参照
                    drHOL = setDs.Tables("LMH030_M_HOL").NewRow()
                    drHOL("HOL") = Format(dBussinessDate, "yyyyMMdd")
                    'データセットに設定
                    setDs.Tables("LMH030_M_HOL").Rows.Add(drHOL)

                    '休日マスタの値を取得
                    setDs = MyBase.CallDAC(Me._DacCom, "SelectMHolList", setDs)

                    If MyBase.GetResultCount = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If
                End If
            Loop
        Next

        Return dBussinessDate

    End Function

#End Region ' "営業日取得"

#End Region ' "画面取込(セミEDI) 関連処理"

#Region "出荷登録処理 および関連処理"

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

        ' EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        ' EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        ' 届先自動追加チェック
        Dim sFlag17 As String = ds.Tables("LMH030INOUT").Rows(0).Item("FLAG_17").ToString()
        If (sFlag17.Equals(LMConst.FLG.OFF)) AndAlso (ds.Tables("LMH030_M_DEST").Rows.Count = 0) Then
            ' エラーメッセージ
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        ' EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        ' EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ' EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ' 届先コードの初期値設定
        ds = Me.SetDestCd(ds)

        ' EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ' EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False

        ' 自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            ' まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                ' まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L001, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    ' まとめ対象だったデータを出したい場合はコメントをはずす
                    ' Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.STD_WID_L001, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    ' ワーニングで"はい"を選択時
                    ' 自動まとめ処理を行う
                    matomeFlg = True
                End If

            ElseIf autoMatomeF.Equals("0") = True Then

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L002, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.STD_WID_L002, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    ' ワーニングで"はい"を選択時
                    ' 自動まとめ処理を行う
                    matomeFlg = True

                ElseIf choiceKb.Equals("03") = True Then
                    ' ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds

                End If

            ElseIf autoMatomeF.Equals("1") = True Then
                Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

                Dim matomeStatus As String = dtMatome.Rows(0).Item("OUTKA_STATE_KB").ToString()

                If matomeStatus.Equals("10") = False Then

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.STD_WID_L003, 0)

                    ' 進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.STD_WID_L003, ds, msgArray, matomeNo, String.Empty)
                        Return ds

                    ElseIf choiceKb.Equals("01") = True Then
                        ' ワーニングで"はい"を選択時
                        ' 自動まとめ処理を行う
                        matomeFlg = True

                    End If

                Else
                    ' まとめ処理を行う
                    matomeFlg = True
                End If

            End If
        End If
        ' 追加箇所 20110824 end

        ' 出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds, matomeFlg)

        ' '出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds, matomeFlg)

        ' 紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            ' 紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        ' 出荷(大)データセット設定
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        ' 出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds, matomeFlg)

        ' 作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)

        ' 運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds, matomeFlg)
        ds = Me.SetDatasetUnsoM(ds)

        ' 運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds, matomeFlg)

        ' タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        ' 出荷登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        ' EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        If matomeFlg = False Then
            ' 出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        Else
            ' 出荷(大)のまとめ更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If

        ' 出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        If sFlag17.Equals(LMConst.FLG.ON) = True Then

            ' 届先マスタの自動追加
            If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
                   AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
                ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
                ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
                ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
            End If

            ' 届先マスタの自動更新
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

        End If

        ' 作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        End If

        ' 運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            If matomeFlg = False Then
                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
            Else
                ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
            End If
        End If

        ' 運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        If matomeFlg = True Then
            ' まとめ先EDI出荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        Return ds

    End Function

#End Region ' "出荷登録処理"

#Region "初期値設定"

#Region "EDI出荷(大)の初期値設定(出荷登録処理)"

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
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse
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
            Else
                ediDr("COA_YN") = "0"
            End If
        End If

        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True OrElse
           ediDr("UNSO_MOTO_KB").ToString().Trim().Equals("90") = True Then
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
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso
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

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        'タリフセットマスタの取得(運賃タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフセットマスタの取得(割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)


        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False Then
                    If String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then
                        ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '運送会社荷主別送り状マスタの存在チェック
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
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
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse
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

#End Region ' "EDI出荷(大)の初期値設定(出荷登録処理)"

#Region "届先コードの初期値設定"

    ''' <summary>
    ''' 届先コードの初期値設定
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

#End Region ' "届先コードの初期値設定"

#Region "EDI出荷(中)の初期値設定(出荷登録処理)"

    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet,
                                    ByVal count As Integer, ByVal unsodata As String,
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)

        '商品M不確定チェック
        If ("1").Equals(ediMDr.Item("GOODSM_FUKAKUTEI_FLG").ToString) = True Then

            'OUTKA_PKG_NB計算し設定
            ediMDr("OUTKA_PKG_NB") = Convert.ToInt32(ediMDr.Item("OUTKA_TTL_NB")) \ Convert.ToInt32(mGoodsDr.Item("PKG_NB"))
            '端数も再設定
            ediMDr("OUTKA_HASU") = Convert.ToInt32(ediMDr.Item("OUTKA_TTL_NB")) Mod Convert.ToInt32(mGoodsDr.Item("PKG_NB"))
        End If

        ''-------------------------------------------------------------------------------------
        ''●チェック
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '商品名
        Dim mGoodsNm As String = mGoodsDr("GOODS_NM_1").ToString().Trim()
        Dim ediGoodsNm As String = ediMDr("GOODS_NM").ToString().Trim()
        Dim mDestGoodsNm As String = String.Empty
        If String.IsNullOrEmpty(ediMDr("DEST_GOODS_NM").ToString()) = False Then
            mDestGoodsNm = ediMDr("DEST_GOODS_NM").ToString().Trim()
        End If

        If mGoodsNm.Equals(ediGoodsNm) = True Then
            'チェック、値入替えなし

        ElseIf String.IsNullOrEmpty(mGoodsNm) = False AndAlso String.IsNullOrEmpty(mDestGoodsNm) = False AndAlso
               mDestGoodsNm.Equals(ediGoodsNm) = False Then
            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.GODO_WID_M001, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "商品名"
                msgArray(2) = "届先商品マスタ"
                msgArray(3) = "商品名"
                msgArray(4) = "注意) 受信した品名は「出荷時注意事項」に記載します。"
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W194", LMH030BLC.GODO_WID_M001, ds, setDs, msgArray,
                    ediMDr("DEST_GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                If String.IsNullOrEmpty(ediMDr("REMARK").ToString()) = True Then
                    ediMDr("REMARK") = String.Concat("受信品名＝", ediMDr("GOODS_NM"))
                Else
                    ediMDr("REMARK") = Me._Blc.LeftB(String.Concat(ediMDr("REMARK"), Space(2), "受信品名＝", ediMDr("GOODS_NM")), 100)
                End If

                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            End If

        ElseIf mGoodsNm.Equals(ediGoodsNm) = False Then

            choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.GODO_WID_M001, count)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "商品名"
                msgArray(2) = "商品マスタ"
                msgArray(3) = "商品名"
                msgArray(4) = String.Empty
                msgArray(5) = String.Empty

                ds = Me._Blc.SetComWarningM("W194", LMH030BLC.GODO_WID_M001, ds, setDs, msgArray,
                            ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                If String.IsNullOrEmpty(ediMDr("REMARK").ToString()) = True Then
                    ediMDr("REMARK") = String.Concat("受信品名＝", ediMDr("GOODS_NM"))
                Else
                    ediMDr("REMARK") = Me._Blc.LeftB(String.Concat(ediMDr("REMARK"), Space(2), "受信品名＝", ediMDr("GOODS_NM")), 100)
                End If

                ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            End If

        End If

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = Left(mGoodsDr("COA_YN").ToString, 1)
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
        If unsodata.Equals("01") = False Then
            ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")
        End If

        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        '個数単位
        ediMDr("KB_UT") = mGoodsDr("NB_UT")

        '数量単位
        ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")

        '包装個数
        ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

        '包装単位
        ediMDr("PKG_UT") = mGoodsDr("PKG_UT")

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

        '入目
        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '出荷包装個数
        '出荷端数
        Dim pkgNb As Decimal = Convert.ToDecimal(ediMDr("PKG_NB"))
        Dim outkaPkgNb As Decimal = Convert.ToDecimal(ediMDr("OUTKA_PKG_NB"))
        Dim outkaHasu As Decimal = Convert.ToDecimal(ediMDr("OUTKA_HASU"))
        Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
        Dim irime As Decimal = Convert.ToDecimal(ediMDr("IRIME"))
        Dim outkaTtlQt As Decimal = Convert.ToDecimal(ediMDr("OUTKA_TTL_QT"))

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
                ediMDr("OUTKA_TTL_NB") = pkgNb * outkaPkgNb + outkaHasu

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

        '個別重量(KGS)
        If unsodata.Equals("01") = False Then
            ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
        End If

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

#End Region ' "EDI出荷(中)の初期値設定(出荷登録処理)"

#End Region ' "初期値設定"

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

#End Region ' "EDI出荷(大)のBLC側でのチェック"

#Region "EDI出荷(大)のDAC側でのチェック"
    ''' <summary>
    ''' EDI出荷(大)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        'オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then
            Dim actionNm As String = String.Empty

            Select Case drIn("ORDER_CHECK_FLG").ToString()
                Case "1"
                    actionNm = "SelectOrderCheckData"
                Case "2"
                    actionNm = "SelectOrderCheckDataInSum"

            End Select

            If String.IsNullOrEmpty(actionNm) = False Then
                Call MyBase.CallDAC(Me._DacCom, actionNm, ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
            End If
        End If

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
        '●荷主固有チェック(標準化荷主用)
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
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
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェック
            'セミEDI時点での届先Ｍ情報と出荷登録時の届先Ｍ情報がズレがないかのチェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                End If
            End If

        ElseIf mDestCount = 0 Then
            '0件の場合、ZIPコードのマスタ存在チェックを行い、届先マスタの更新をする
            'JISマスタに存在しない場合、エラー
            'JISマスタに存在するが、JISが空の場合、ワーニング
            If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) = False Then
                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    'チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                    flgWarning = True
                End If
            End If

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

#End Region ' "EDI出荷(大)のDAC側でのチェック"

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

#End Region ' "EDI出荷(中)のBLC側でのチェック"

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

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.NKS_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                If MyBase.GetResultCount = 0 Then
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
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

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.NKS_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'エラー/ワーニング設定
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

#End Region ' "EDI出荷(中)のDAC側でのチェック + 初期値設定"

#Region "届先マスタ追加時チェック"

    ''' <summary>
    ''' 届先マスタ追加時チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = String.Empty
        If String.IsNullOrEmpty(dtEdi.Rows(0)("DEST_ZIP").ToString()) = False Then
            dtEdi.Rows(0)("DEST_ZIP") = Replace(dtEdi.Rows(0)("DEST_ZIP").ToString(), "-", String.Empty)
            ediZip = dtEdi.Rows(0)("DEST_ZIP").ToString()
        End If

        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

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
                'drEdiL("DEST_JIS_CD") = mZipJis
                warningString = "郵便番号マスタ"
            End If
        End If

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L004, 0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If


            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.STD_WID_L004, ds, msgArray, workDestCd, String.Empty)

            compareWarningFlg = True

        ElseIf choiceKb.Equals("01") = True Then

            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMdest.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            drMD("EDI_CD") = workDestCd

            Dim sDEST_NM As String = String.Empty
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            drMD("ZIP") = Replace(drEdiL("DEST_ZIP").ToString(), "-", String.Empty)
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
            drMD("FAX") = drEdiL("DEST_FAX").ToString()
            drMD("JIS") = mZipJis
            'EDIデータにも値をセットする
            drEdiL("DEST_JIS_CD") = mZipJis
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            drMD("LARGE_CAR_YN") = "01"
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

#End Region ' "届先マスタ追加時チェック"

#Region "届先マスタチェック"

    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()

        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

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

        '現LMSでチェックコメントアウトの為コメント化
        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestNm) OrElse mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L005, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.STD_WID_L005, ds, msgArray, ediDestNm, mDestNm)

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        '届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestAdAll) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediDestAdAll = SpaceCutChk(ediDestAdAll)
            If mAdAll.Equals(ediDestAdAll) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L006, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.STD_WID_L006, ds, msgArray, ediDestAdAll, mAdAll)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                    dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                    dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If
            End If

        End If

        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If Replace(mZip, "-", String.Empty).Equals(Replace(ediZip, "-", String.Empty)) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L007, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先郵便番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "郵便番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.STD_WID_L007, ds, msgArray, ediZip, mZip)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If


        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L008, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.STD_WID_L008, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '届先JISコード(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestJisCd) = True Then
            'チェックなしだが届先MのDEST_JISをセット
            dtEdi.Rows(0)("DEST_JIS_CD") = mJis
        Else
            If mJis.Equals(ediDestJisCd) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.STD_WID_L009, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先JISコード"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "JISコード"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.STD_WID_L009, ds, msgArray, ediDestJisCd, mJis)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        Return True

    End Function

#End Region ' "届先マスタチェック"

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

#End Region ' "SPACE除去 + 文字変換"

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

#End Region ' "左埋処理"

#Region "ワーニング処理(EDI(大)届先)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet,
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

#End Region ' "ワーニング処理(EDI(大)届先)選択区分の取得"

#Region "ワーニング処理(EDI(中)商品)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet,
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

#End Region ' "ワーニング処理(EDI(中)商品)選択区分の取得"

#End Region ' "入力チェック(出荷登録処理)"

#Region "データセット設定"

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

#End Region ' "データセット設定(出荷管理番号L)"

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
            Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            For i As Integer = 0 To max
                outkaKanriNo = (maxOutkaKanriNo + i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region ' "データセット設定(出荷管理番号M)"

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

        If matomeFlg = False Then
            '通常登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
            outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
            outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
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

            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
                outkaDr("DEST_CD") = destMDr("DEST_CD")
                outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_TEL") = destMDr("TEL")
                outkaDr("DEST_KB") = "00"
                outkaDr("DEST_NM") = destMDr("DEST_NM")
                outkaDr("DEST_AD_1") = destMDr("AD_1")
                outkaDr("DEST_AD_2") = destMDr("AD_2")
            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
                outkaDr("DEST_KB") = "02"
                outkaDr("DEST_NM") = ediDr("DEST_NM")
                outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
                outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
            End If

            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
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
            'outkaDr("DEST_KB") = "02"
            'outkaDr("DEST_NM") = ediDr("DEST_NM")
            'outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            'outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")

            '先方区切りバイト数(区分マスタ)
            Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0).Item("EVENT_SHUBETSU").ToString()
            ds.Tables("LMH030_JUDGE").Clear()
            Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").NewRow()
            Dim delimiterByte As Integer = 30
            drJudge("EVENT_SHUBETSU") = eventShubetsu
            drJudge("KBN_GROUP_CD") = "D027"
            drJudge("KBN_CD") = String.Concat(ediDr("NRS_BR_CD"), ediDr("CUST_CD_L"), ediDr("CUST_CD_M"))
            ds.Tables("LMH030_JUDGE").Rows.Add(drJudge)

            ds = MyBase.CallDAC(Me._Dac, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 1 Then
                delimiterByte = Convert.ToInt32(ds.Tables("LMH030_Z_KBN").Rows(0).Item("NISUGATA"))
            End If

            ' まとめ処理時のオーダー番号まとめ --ST--
            If String.IsNullOrEmpty(matomesakiDt.Rows(0)("CUST_ORD_NO").ToString()) = False Then
                outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)("CUST_ORD_NO"), ",", Right(ediDr("CUST_ORD_NO").ToString(), delimiterByte)), 30)
            ElseIf String.IsNullOrEmpty(ediDr("CUST_ORD_NO").ToString()) = False Then
                outkaDr("CUST_ORD_NO") = Me._Blc.LeftB(ediDr("CUST_ORD_NO").ToString(), 30)
            End If
            ' まとめ処理時のオーダー番号まとめ --ED--

        End If
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region ' "データセット設定(出荷L)"

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

#End Region ' "データセット設定(出荷包装個数)"

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaM(ByVal ds As DataSet, ByVal matomeflg As Boolean) As DataSet

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")


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
            'EDI出荷(大)のオーダ番号とEDI出荷(中)のオーダ番号が同一である場合、出荷(中)のオーダ番号を空で登録する。
            'ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            Dim sCustOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sCustOrdNo = matomesakiDt.Rows(0)("CUST_ORD_NO").ToString   'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                sCustOrdNo = ediLDr("CUST_ORD_NO").ToString                 '自分のEDI出荷(大)を参照
            End If

            If ediDr("CUST_ORD_NO_DTL").ToString = sCustOrdNo Then
                outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            Else
                outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            End If

            ' EDI出荷(大)の注文番号とEDI出荷(中)の注文番号が同一である場合、出荷(中)の注文番号を空で登録する。
            ' ただしまとめた場合はまとめ先のEDI出荷(大)を参照する
            Dim sBuyerOrdNo As String = vbNullString
            If matomeflg Then   'まとめの場合
                sBuyerOrdNo = matomesakiDt.Rows(0)("BUYER_ORD_NO").ToString 'まとめ先のEDI出荷(大)を参照
            Else                'まとめでない場合
                sBuyerOrdNo = ediLDr("BUYER_ORD_NO").ToString               '自分のEDI出荷(大)を参照
            End If

            If ediDr("BUYER_ORD_NO_DTL").ToString = sBuyerOrdNo Then
                outkaDr("BUYER_ORD_NO_DTL") = vbNullString
            Else
                outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            End If
            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            outkaDr("OUTKA_HASU") = ediDr("OUTKA_HASU")
            outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
            outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("ALCTD_NB") = 0
            outkaDr("ALCTD_QT") = 0
            outkaDr("BACKLOG_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("BACKLOG_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            outkaDr("IRIME") = ediDr("IRIME")
            outkaDr("IRIME_UT") = ediDr("IRIME_UT")

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

#End Region ' "データセット設定(出荷M)"

#Region "データセット設定(EDI受信DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet) As DataSet

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

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region ' "データセット設定(EDI受信DTL)"

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
                    sagyoDr("DEST_SAGYO_FLG") = "01"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
        Next

        Return ds

    End Function

#End Region ' "データセット設定(作業)"

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
            unsoDr("PC_KB") = ediDr("PC_KB")
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
            unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
            unsoDr("SYUKA_TYUKEI_CD") = String.Empty
            unsoDr("HAIKA_TYUKEI_CD") = String.Empty
            unsoDr("TRIP_NO_SYUKA") = String.Empty
            unsoDr("TRIP_NO_TYUKEI") = String.Empty
            unsoDr("TRIP_NO_HAIKA") = String.Empty

            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso
               String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

                '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
                ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
                Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

                If MyBase.GetResultCount > 0 Then
                    unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                    unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
                End If

            End If
        Else
            'まとめ処理
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            unsoDr("NRS_BR_CD") = matomeDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = matomeDr("UNSO_NO_L")
            unsoDr("SYS_UPD_DATE") = matomeDr("SYS_UNSO_UPD_DATE")
            unsoDr("SYS_UPD_TIME") = matomeDr("SYS_UNSO_UPD_TIME")

            '運送梱包個数の計算
            Dim unsoPkgNb As Long = 0
            Dim matomesakiUnsoPkgNb As Long = Convert.ToInt64(matomeDr("UNSO_PKG_NB"))
            Dim matomesakiOutkaPkgNb As Long = Convert.ToInt64(matomeDr("OUTKA_PKG_NB"))

            unsoDr("UNSO_PKG_NB") = Convert.ToInt64(outkaLDr("OUTKA_PKG_NB")) + matomesakiUnsoPkgNb - matomesakiOutkaPkgNb

        End If
        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function

#End Region ' "データセット設定(運送L)"

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
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_TTL_NB")
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

#End Region ' "データセット設定(運送M)"

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

#End Region ' "データセット設定(運送L：運送重量)"

#Region "データセット設定(EDI出荷M：運送重量必要項目)"

    ''' <summary>
    ''' データセット設定(EDI出荷M：運送重量必要項目)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer,
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

#End Region ' "データセット設定(EDI出荷M：運送重量必要項目)"

#End Region ' "データセット設定"

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

#End Region ' "出荷登録処理(運賃作成)"

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

#End Region ' "出荷登録処理 および関連処理"

#End Region ' "Method"

End Class
