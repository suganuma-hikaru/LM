' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  403　　　 : デュポン(横浜)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC403
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC403 = New LMH030DAC403()

    ''' <summary>
    ''' 使用するDACクラスの生成(マスタチェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacMst As LMH030DAC = New LMH030DAC()


    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH030BLC = New LMH030BLC()

#End Region

#Region "Method"

#Region "検索処理"

    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

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
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "出荷登録処理"
    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        '★★★2011.08.30 追加START
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.08.30 追加END

        'EDI出荷(大)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            '★★★2011.08.30 修正END
            Return ds
        End If

        'EDI出荷(中)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            '★★★2011.08.30 修正END
            Return ds
        End If

        'EDI出荷(大)の初期値設定後の関連チェック
        '★★★2011.08.30 修正START
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.08.30 修正END

        'EDI出荷(大)の初期値設定後のDB存在チェック
        '★★★2011.08.30 修正START
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.08.30 修正END

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        '★★★2011.08.30 修正START
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.08.30 修正END

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする add start
        '荷主明細マスタより該当する荷主の納入時刻全件設定情報を取得する。
        ds = MyBase.CallDAC(Me._Dac, "SelectNounyuuJikokuZenken", ds)
        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする add end

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

        'EDI受信テーブル(HED)データセット設定
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)

        '運送(大,中)データセット設定
        'If ds.Tables("LMH030_OUTKAEDI_L").Rows(0)("UNSO_MOTO_KB").ToString() = "10" _
        ' AndAlso String.IsNullOrEmpty(sakuraMatomeF) = True Then
        ds = Me.SetDatasetUnsoL(ds)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds)

        'タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        'End If

        'EDI出荷(大)の更新
        Dim matomeCnt As Integer = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count

        '出荷登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)


        '出荷(大)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        '▼▼▼後に追加
        '★★★2011.11.08 要望番号439 届先マスタ自動追加START
        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
        End If
        '★★★2011.11.08 END
        '▲▲▲後に追加

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)
        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        Return ds

    End Function

#Region "データセット設定"

#Region "データセット設定(出荷管理番号L)"

    Private Function GetOutkaNoL(ByVal ds As DataSet) As DataSet

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
        Else

            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)
            dr("OUTKA_CTL_NO") = outkaKanriNo

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
    Private Function GetOutkaNoM(ByVal ds As DataSet) As DataSet

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
        Else
            '通常出荷登録処理の場合
            For i As Integer = 0 To max

                outkaKanriNo = (i + 1).ToString("000")
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
    Private Function SetDatasetOutkaL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする add start
        Dim custDtlDr As DataRow = Nothing
        If ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count = 1 Then
            custDtlDr = ds.Tables("LMH030_M_CUST_DETAILS").Rows(0)
        End If
        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする add end
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty

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
        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする upd start
        If custDtlDr Is Nothing Then
            '荷主明細マスタに納入時刻設定情報が設定されていなければEDI出荷情報の納入時刻を設定する
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        Else
            '荷主明細マスタに納入時刻設定情報が設定されていれば荷主明細マスタに納入時刻設定情報の納入時刻を設定する
            outkaDr("ARR_PLAN_TIME") = custDtlDr("SET_NAIYO")
        End If
        'outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        '2018/02/05 Annen 000589 出荷 _横浜アクサルタ-納入日の時間帯指定を全てAM必着にする upd end
        outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
        outkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
        outkaDr("END_DATE") = String.Empty
        outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
        outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
        outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
        outkaDr("SHIP_CD_M") = String.Empty
        '2011.11.07  要望番号483 修正START
        '荷主によって取得先が切り替わるので、2次以降反映する際には気をつける事!!!
        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
            outkaDr("DEST_CD") = destMDr("DEST_CD")
            outkaDr("DEST_AD_3") = destMDr("AD_3")
            outkaDr("DEST_TEL") = destMDr("TEL")
        Else
            outkaDr("DEST_CD") = ediDr("DEST_CD")
            outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
            outkaDr("DEST_TEL") = ediDr("DEST_TEL")
        End If
        '2011.11.07  要望番号483 修正END
        outkaDr("NHS_REMARK") = String.Empty
        outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
        outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
        outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
        outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
        outkaDr("REMARK") = ediDr("REMARK")
        '★★★2011.10.20 修正START 出荷包装個数対応(全荷主ではなく、サクラのみ)
        '出荷梱包個数は1固定(全荷主)
        'outkaDr("OUTKA_PKG_NB") = 1
        outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
        '★★★2011.10.20 修正END
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
        '2011.09.16 追加START
        outkaDr("DEST_KB") = "02"
        outkaDr("DEST_NM") = ediDr("DEST_NM")
        outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
        outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        '2011.09.16 追加END


        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Private Function SumPkgNb(ByVal dt As DataTable) As Double
        '----入数で集計する　開始　　ADD 2017/04/27
        Dim viw As New DataView(dt)
        Dim isDistinct As Boolean = True

        'PKG_NBの重複を削除
        Dim cols As String = "PKG_NB"
        Dim dtFilter As DataTable = viw.ToTable(isDistinct, cols)
        dtFilter.Columns.Add("expr", GetType(Integer))

        For Each row As DataRow In dtFilter.Rows

            Dim sSql As String = "PKG_NB = '" & CStr(row("PKG_NB")) & "'"
            '同じPKG_NB(包装個数)をSELECT
            Dim outkaDr() As DataRow = dt.Select(sSql)
            Dim drMax As Integer = outkaDr.Length - 1
            Dim gOUTKA_TTL_NB As Long = 0

            For i As Integer = 0 To drMax
                '同じPKG_NBを集計
                gOUTKA_TTL_NB += Convert.ToInt64(outkaDr(i).Item("OUTKA_TTL_NB"))
            Next
            row("expr") = gOUTKA_TTL_NB
        Next

        Dim sumNb As Double = 0
        Dim calcPKG_NB As Long = 0
        Dim calcexpr As Long = 0

        '同じPKG_NBでの梱包数計算
        For Each row As DataRow In dtFilter.Rows

            calcPKG_NB = Convert.ToInt64(row("PKG_NB"))
            calcexpr = Convert.ToInt64(row("expr"))

            If calcexpr <= calcPKG_NB Then
                sumNb += 1

            Else
                sumNb += Math.Ceiling(calcexpr / calcPKG_NB)

            End If
        Next

        Return sumNb

        '---入数で集計する END

#If False Then      '旧内容 2017/04/27
        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0

        '★★★2011.10.20 START 出荷包装個数対応
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0
        '★★★2011.10.20 END

        For i As Integer = 0 To max

            '★★★2011.10.20 START 出荷包装個数対応
            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            '▼▼▼(EDI_M梱数不具合)
            'If 0 = calcPkgModNb Then
            '    dt.Rows(i)("OUTKA_PKG_NB") = calcPkgQuoNb
            'Else
            '    dt.Rows(i)("OUTKA_PKG_NB") = calcPkgQuoNb + 1
            'End If

            ''★★★2011.10.20 END

            'sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            If 0 = calcPkgModNb Then
            Else
                sumNb = sumNb + 1
            End If

            '▲▲▲(EDI_M梱数不具合)

        Next


        Return sumNb
#End If

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
        '2011.10.14 START 個数MAX桁数対応
        'Dim calcPkgModNb As Integer = 0
        Dim calcPkgModNb As Long = 0
        '2011.10.14 END 
        Dim calcPkgQuoNb As Double = 0


        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").NewRow()

            'If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            'AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            'AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            'AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

            '    calcPkgModNb = Convert.ToInt32(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt32(dt.Rows(i)("PKG_NB"))
            '    calcPkgQuoNb = Convert.ToDouble(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt32(dt.Rows(i)("PKG_NB"))
            'End If

            '2011.07.29 不具合対応 START
            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                '2011.10.14 START 個数MAX桁数対応
                'calcPkgModNb = Convert.ToInt32(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt32(dt.Rows(i)("PKG_NB"))
                'calcPkgQuoNb = Convert.ToInt32(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt32(dt.Rows(i)("PKG_NB"))
                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                '2011.10.14 END 
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
            End If
            '2011.07.29 不具合対応 END


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

            '2012.03.15 要望番号895 大阪対応START
            'EDI出荷(大)の買主注文番号にEDI出荷(中)の買主注文番号が含まれていない場合のみ、
            '出荷(中)の買主注文番号にEDI出荷(中)の買主注文番号を付与
            If InStr(ediLDr("BUYER_ORD_NO").ToString().Trim(), ediDr("BUYER_ORD_NO_DTL").ToString().Trim()) = 0 Then
                outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            End If
            'outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            '2012.03.15 要望番号895 大阪対応END

            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            '★★★2011.11.08 要望番号472 修正START 
            'outkaDr("OUTKA_PKG_NB") = ediDr("OUTKA_PKG_NB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            '★★★2011.11.08 要望番号472 修正END
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

            'If Convert.ToInt32(dt.Rows(i)("PKG_NB")) = 0 Then

            '    outkaDr("OUTKA_M_PKG_NB") = ediDr("OUTKA_PKG_NB")
            'ElseIf 999 > Convert.ToInt32(dt.Rows(i)("PKG_NB")) AndAlso 0 = calcPkgModNb Then

            '    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb

            'ElseIf 999 > Convert.ToInt32(dt.Rows(i)("PKG_NB")) AndAlso 0 <> calcPkgModNb Then

            '    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1

            'ElseIf Convert.ToInt32(dt.Rows(i)("PKG_NB")) > 999 Then

            '    outkaDr("OUTKA_M_PKG_NB") = 1

            'End If

            '2011.07.29 不具合対応 START
            '2011.10.14 START 個数MAX桁数対応
            'If Convert.ToInt32(dt.Rows(i)("PKG_NB")) = 0 Then
            If Convert.ToInt64(dt.Rows(i)("PKG_NB")) = 0 Then
                '2011.10.14 END
                outkaDr("OUTKA_M_PKG_NB") = 0
            Else
                If 0 = calcPkgModNb Then
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb
                Else
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                End If
            End If

            '2011.10.14 START 個数MAX桁数対応
            'If Convert.ToInt32(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
            If Convert.ToInt64(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
                '2011.10.14 END
                outkaDr("OUTKA_M_PKG_NB") = 1
            End If
            '2011.07.29 不具合対応 END

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

#Region "データセット設定(EDI受信HED)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(HED))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvHed(ByVal ds As DataSet) As DataSet

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

        'データセットに設定
        ds.Tables("LMH030_EDI_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function
#End Region

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

            If i <> 0 Then
                '2011.09.11 出荷管理番号中番不具合対応 START
                outkaedimDr("OUTKA_CTL_NO_CHU") = (Convert.ToInt32(outkaCtlNoChu) + 1).ToString("000")
                'outkaedimDr("OUTKA_CTL_NO_CHU") = Convert.ToInt32(outkaCtlNoChu) + 1
                '2011.09.11 出荷管理番号中番不具合対応 END

            End If

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            'rcvDr("CUST_CD_L") = outkaedilDr("CUST_CD_L")
            'rcvDr("CUST_CD_M") = outkaedilDr("CUST_CD_M")
            rcvDr("SYS_DEL_FLG") = "0"
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            'rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU")

            outkaCtlNoChu = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("OUTKA_CTL_NO_CHU") = outkaCtlNoChu

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
    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        '新規採番
        unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        '倉庫マスタより取得用
        unsoDr("WH_CD") = ediDr("WH_CD")
        unsoDr("INOUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
        unsoDr("TRIP_NO") = String.Empty
        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
        unsoDr("BIN_KB") = ediDr("BIN_KB")
        unsoDr("JIYU_KB") = String.Empty
        unsoDr("DENP_NO") = String.Empty
        unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
        '項目がないから空↓
        unsoDr("OUTKA_PLAN_TIME") = String.Empty
        unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
        unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        unsoDr("ARR_ACT_TIME") = String.Empty
        '荷主マスタより課税区分取得用にも使用
        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
        unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
        unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
        'unsoDr("SHIP_CD_M") = ediDr("SHIP_CD_M") '項目削除の為
        'unsoDr("ORIG_CD") = ediDr("DEST_TEL") '倉庫Mより取得の為
        unsoDr("DEST_CD") = ediDr("DEST_CD")
        unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
        'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
        unsoDr("UNSO_WT") = 0 '運送Mの集計値の為要修正
        unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
        'unsoDr("PC_KB") = ediDr("PICK_KB")
        unsoDr("PC_KB") = ediDr("PC_KB")
        '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
        unsoDr("MOTO_DATA_KB") = "20"
        'unsoDr("TAX_KB") = ediDr("TAX_KB") '荷主マスタの課税区分をセットする為
        '要望番号602 2011.12.09 追加START
        unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
        '要望番号602 2011.12.09 追加END
        unsoDr("REMARK") = ediDr("UNSO_ATT")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
        unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
        '2011.11.07  要望番号483 修正START
        '荷主によって取得先が切り替わるので、2次以降反映する際には気をつける事!!!
        'unsoDr("AD_3") = ediDr("DEST_AD_3")
        unsoDr("AD_3") = outkaLDr("DEST_AD_3")
        '2011.11.07  要望番号483 修正END
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
            ds = MyBase.CallDAC(Me._DacMst, "SelectListDataShiharaiTariff", ds)
            Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

            If MyBase.GetResultCount > 0 Then
                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
            End If

        End If
        'END UMANO 要望番号1302 支払運賃に伴う修正。

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
            '▼▼▼(運送M運送個数不具合）
            'unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_TTL_NB")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            '▲▲▲(運送M運送個数不具合）
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

            '★★★
            'nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            'outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))
            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))
            '★★★

            '2011.10.26 荷姿不具合START
            If ediDr("TARE_YN").Equals("01") = False Then
                'If ediDr("TARE_YN").Equals("1") = False Then
                '2011.10.26 荷姿不具合END
                '▼▼▼(運送M個別重量不具合）
                'unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb) * outkaTtlNb
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)
                '▲▲▲(運送M個別重量不具合）
            Else
                '▼▼▼(運送M個別重量不具合）
                'unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata) * outkaTtlNb
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)
                '▲▲▲(運送M個別重量不具合）
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
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        'Dim unsoMDr As DataRow
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        'Dim max As Integer = ds.Tables("LMH030_UNSO_M").Rows.Count - 1

        '★★★2011.08.30 修正START
        unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
        'For i As Integer = 0 To max

        '    unsoMDr = ds.Tables("LMH030_UNSO_M").Rows(i)

        '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
        '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

        '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

        'Next
        '★★★2011.08.30 修正END

        unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
        unsoLDr("NB_UT") = ediMDr("KB_UT")

        Return ds

    End Function

#End Region

#Region "運送L：運送重量計算処理"

    ''' <summary>
    ''' 運送重量再計算処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''         '★★★2011.08.30 追加START
    Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim unsoMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1
        '▼▼▼(運送M個別重量不具合）
        Dim ediMDr As DataRow
        '▲▲▲(運送M個別重量不具合）
        For i As Integer = 0 To max

            unsoMDr = ds.Tables(tblNm).Rows(i)

            '▼▼▼運送対応
            ediMDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            '▲▲▲運送対応

            '▼▼▼運送対応
            '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
            'unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo
            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * Convert.ToDecimal(ediMDr("OUTKA_TTL_NB")) + unsoJyuryo
            '▲▲▲運送対応

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
        '2011.10.26 荷姿不具合START
        'drJudge = ds.Tables("LMH030_JUDGE").NewRow()
        drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
        drJudge("KBN_CD") = ediMDr("PKG_UT")
        'ds.Tables("LMH030_JUDGE").Rows.Add(drJudge)
        '2011.10.26 荷姿不具合END

        ds = MyBase.CallDAC(Me._DacMst, "SelectDataPkgUtZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"包装単位", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        '★★★2011.08.30 修正START
        Return True
        '★★★2011.08.30 修正END

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定(デュポン横浜専用)"

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

        '2011.09.11 温度区分対応 START
        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        '2011.09.11 温度区分対応 END

        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

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
        If unsodata.Equals("01") = False Then
            ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")
        End If

        '商品名
        If unsodata.Equals("01") = False Then
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1")
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
        If unsodata.Equals("01") = False Then
            ediMDr("KB_UT") = mGoodsDr("NB_UT")
        End If

        '数量単位
        If unsodata.Equals("01") = False Then
            ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")
        End If

        '包装個数
        If unsodata.Equals("01") = False Then
            ediMDr("PKG_NB") = mGoodsDr("PKG_NB")
        End If

        '包装単位
        If unsodata.Equals("01") = False Then
            ediMDr("PKG_UT") = mGoodsDr("PKG_UT")
        End If

        '2011.09.11 温度区分対応 START
        '温度区分
        ediMDr("ONDO_KB") = mGoodsDr("ONDO_KB")
        '2011.09.11 温度区分対応 END

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

            '2011.09.11 運送温度区分対応 START
            '運送温度区分(区分マスタ)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"出荷区分", "区分マスタ"})
                Return False
            End If
            '2011.09.11 運送温度区分対応 END

        End If

        '▼▼▼
        '入目
        'If (ediMDr("IRIME").ToString()).Equals("0") = True _
        'AndAlso (mGoodsDr("STD_IRIME_NB").ToString()).Equals("0") = False Then

        '    ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")

        'End If

        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then

            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")

        End If
        '▲▲▲

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then

            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
            '▼▼▼
        Else

            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー(サクラ以外)
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '▲▲▲
        End If

        '出荷端数(デュポン専用)
        Dim freeN02 As Double = 0
        Dim freeN03 As Double = 0
        Dim freeC03 As String = String.Empty
        Dim freeC12 As String = String.Empty
        Dim irime As Double = 0
        Dim hasu As Double = 0
        Dim ediValue As String = String.Empty
        freeN02 = Convert.ToDouble(ediMDr("FREE_N02"))
        freeN03 = Convert.ToDouble(ediMDr("FREE_N03"))
        freeC03 = ediMDr("FREE_C03").ToString()
        freeC12 = ediMDr("FREE_C12").ToString()
        irime = Convert.ToDouble(ediMDr("IRIME"))
        hasu = freeN02 Mod irime

        If freeN03 = 0 AndAlso freeC03.Equals("KG") = True AndAlso String.IsNullOrEmpty(freeC12) = True Then

            If hasu <= 0 Then

                ediMDr("OUTKA_HASU") = Math.Floor((freeN02) / irime)
                ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")
                ediMDr("FREE_C12") = "個数再計算済"

            Else
                'ワーニング
                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdiM, ds, LMH030BLC.DUPONT_WID_M002, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("W167", New String() {"端数"})

                    msgArray(1) = "端数"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    ediValue = Convert.ToString(hasu)
                    ds = Me._Blc.SetComWarningM("W167", LMH030BLC.DUPONT_WID_M002, ds, setDs, msgArray, ediValue, String.Empty)

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    ediMDr("FREE_C12") = "端数許可（出荷処理で修正）"
                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    '★★★2011.08.30 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("G042")
                    '★★★2011.08.30 修正END
                    Return False

                End If


            End If

        End If

        '出荷包装個数
        Dim pkgNb As Double = 0
        Dim outkaPkgNb As Double = 0
        Dim outkaHasu As Double = 0
        pkgNb = Convert.ToDouble(ediMDr("PKG_NB"))
        outkaPkgNb = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        outkaHasu = Convert.ToDouble(ediMDr("OUTKA_HASU"))

        If 1 < pkgNb Then

            '★★★2011.10.20 小数点以下切捨て 修正START
            ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
            'ediMDr("OUTKA_PKG_NB") = (pkgNb * outkaPkgNb + outkaHasu) / pkgNb
            '★★★2011.10.20 修正END
            ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
        Else
            '2011.09.27 修正 START出荷画面対応による個数,端数の値入替
            'ediMDr("OUTKA_PKG_NB") = 0
            'ediMDr("OUTKA_HASU") = pkgNb * outkaPkgNb + outkaHasu
            ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
            ediMDr("OUTKA_HASU") = 0
            '2011.09.27 修正 END

        End If
        ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

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

        '★★★2012.01.12 要望番号597 START
        '注意事項
        If String.IsNullOrEmpty(mGoodsDr("OUTKA_ATT").ToString) = False Then
            'ediMDr("REMARK") = String.Concat(ediMDr("REMARK").ToString, mGoodsDr("OUTKA_ATT").ToString) 　'再修正20120119
            If "00688".Equals(ediLDr("CUST_CD_L").ToString) Then
                'セラニーズの場合、商品マスタの出荷時注意事項で置き換える
                ediMDr("REMARK") = Me.LeftB(mGoodsDr("OUTKA_ATT").ToString, 100)
            Else
                ediMDr("REMARK") = Me.LeftB(String.Concat(ediMDr("REMARK").ToString, mGoodsDr("OUTKA_ATT").ToString), 100)
            End If

        End If
        '★★★2012.01.12 要望番号597 END

        Return True

    End Function

#End Region

#End Region

#Region "入力チェック"

#Region "EDI出荷(大)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function EdiLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        '出荷管理番号
        If Me._Blc.OutkaCtlNoCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E349", New String() {"EDIデータ", "出荷管理番号"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"出荷報告有無"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷予定日"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出庫日"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E166", New String() {"出荷予定日", "出庫日"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"納入予定日"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷報告日"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(大)"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(中)"})
            '★★★2011.08.30 修正END
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"送り状作成有無"})
            '★★★2011.08.30 修正END
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
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        'オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._DacMst, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    '★★★2011.08.30 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E377", New String() {"出荷データ"})
                    Return False
                    '★★★2011.08.30 修正END
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷種別区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"作業進捗区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"ピッキングリスト区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"倉庫コード", "倉庫マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.08.30 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"納入予定時刻", "区分マスタ"})
                Return False
                '★★★2011.08.30 修正END
            End If

        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"荷主コード", "荷主マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"運送手配区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()      '荷送人コード

        '★★★2012.01.12 要望番号596 START
        '荷送人コードのマスタ存在チェック
        'SHIP_CD_Lが空でなく、SHIP_CD_L = DEST_CD <> EDI_DEST_CD の場合、もしくはSHIP_CD_L <> DEST_CD の場合
        If (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = True AndAlso shipCdL.Equals(ediDestCd) = False) _
            OrElse (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = False) Then

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdestShip", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷送人コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If
        '★★★2012.01.12 要望番号596 END

        '届先コード(届先マスタ)
        Dim workDestCd As String = String.Empty                     '検索する届先コード格納変数
        Dim workDestString As String = String.Empty                 '"届先コード"or"EDI届先コード"
        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
        Dim flgWarning As Boolean = False

        'DEST_CDが空の場合、EDI_DEST_CDを使う
        If String.IsNullOrEmpty(destCd) = False Then
            workDestCd = destCd
            workDestString = "届先コード"
        ElseIf String.IsNullOrEmpty(ediDestCd) = False Then
            workDestCd = ediDestCd
            workDestString = "EDI届先コード"
        Else
        End If

        '届先コード(EDI届先コード)のマスタ存在チェック
        If String.IsNullOrEmpty(workDestCd) = False Then

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdest", ds)

            If MyBase.GetResultCount = 0 Then
                '▼▼▼要望番号439　後に追加するロジック
                'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                If SetInsMDestFromDest(ds, workDestCd, workDestString) = True Then
                    flgWarning = True
                End If
                '▲▲▲要望番号439　後に追加するロジック
                ''▼▼▼後に削除するロジック
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {workDestString, "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'Return False
                ''▲▲▲後に削除するロジック

            ElseIf 1 < MyBase.GetResultCount Then
                '複数件の場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {workDestString, "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            Else
                '1件に特定できた場合、デュポン専用 マスタ値とEDI出荷(大)の整合性チェック
                If Me.DupontDestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                    '★★★2012.01.12 要望番号596 START
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、EDI出荷(大)のチェックまで行いワーニング画面に遷移
                        flgWarning = True
                    End If
                    '★★★2012.01.12 要望番号596 END
                End If
            End If
        End If

        '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"タリフ分類区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.08.30 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"車輌区分", "区分マスタ"})
                Return False
                '★★★2011.08.30 修正END
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"便区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '2011.09.25 追加START
        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._DacMst, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運送会社コード", "運送会社マスタ"})
                Return False
            End If
        End If
        '2011.09.25 追加END

        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        Dim unchinTariffCd As String = String.Empty
        unchinTariffCd = drEdiL("UNCHIN_TARIFF_CD").ToString()
        Dim unsoTehaiKb As String = String.Empty
        unsoTehaiKb = drEdiL("UNSO_TEHAI_KB").ToString()

        If String.IsNullOrEmpty(unchinTariffCd) = True Then

        Else

            If unsoTehaiKb.Equals("40") = True Then
                ds = MyBase.CallDAC(Me._DacMst, "SelectDataMyokoTariffHd", ds)
            Else
                ds = MyBase.CallDAC(Me._DacMst, "SelectDataMunchinTariff", ds)
            End If

            If MyBase.GetResultCount = 0 Then
                '★★★2011.08.30 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"})
                Return False
                '★★★2011.08.30 修正END
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.08.30 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"})
                Return False
                '★★★2011.08.30 修正END
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"元着払い区分", "区分マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If

        '★★★要望番号439 2011.11.08 届先自動追加START
        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合は届先マスタの自動追加または自動更新を行う為、EDI出荷(大)のチェック終了後に
        'ワーニングがある（flgWarning=True）場合は処理を終了させる。
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If
        '★★★要望番号439 2011.11.08 届先自動追加END

        Return True

    End Function

    '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

    ''' <summary>
    ''' 届先コード(EDI届先コード)から届先マスタInsertデータを作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ワーニング設定をする
    ''' ワーニング画面の戻り値がある場合、届先コード(EDI届先コード)から届先マスタUpdateデータを作成する
    ''' </remarks>
    Private Function SetInsMDestFromDest(ByVal ds As DataSet, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMD As DataTable = ds.Tables("LMH030_M_DEST")
        Dim msgArray(5) As String
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.DUPONT_WID_L005, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.DUPONT_WID_L005, ds, msgArray, workDestCd, String.Empty)

            Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMD.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            drMD("EDI_CD") = workDestCd
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            drMD("ZIP") = drEdiL("DEST_ZIP").ToString()
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()
            drMD("JIS") = drEdiL("DEST_JIS_CD").ToString()
            drMD("PICK_KB") = "01"
            drMD("BIN_KB") = "01"
            'マスタ自動追加対象フラグ
            drMD("MST_INSERT_FLG") = "1"
            dtMD.Rows.Add(drMD)

        End If

    End Function
    '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

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
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"引当単位区分"})
            Return False
            '★★★2011.08.30 修正END
        End If
        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E352")
            Return False
            '★★★2011.08.30 修正END
        End If
        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
            Return False
            '★★★2011.08.30 修正END
        End If
        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"入目と出荷総数量"})
            Return False
            '★★★2011.08.30 修正END
        End If
        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
            Return False
            '★★★2011.08.30 修正END
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
        '2011.10.06 START EDI(メモ)№79
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        '▼▼▼
        Dim flgWarning As Boolean = False
        '▲▲▲

        For i As Integer = 0 To max

            unsoData = dtL.Rows(0)("FREE_C30").ToString()

            If String.IsNullOrEmpty(unsoData) = False Then
                unsoData = dtL.Rows(0)("FREE_C30").ToString().Substring(0, 2)
            End If

            custGoodsCd = dtM.Rows(i)("CUST_GOODS_CD").ToString()

            If unsoData.Equals("01") = True Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                '2011.10.06 START EDI(メモ)№79
                setDtM.ImportRow(dtM.Rows(i))
                '2011.10.06 END

                '商品コード,商品キー(商品マスタ)
                setDs = MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsUnso", setDs)

                If MyBase.GetResultCount = 0 Then
                    '★★★2011.08.30 修正START
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E326", New String() {"商品コード", "商品マスタ"})
                    Return False
                    '★★★2011.08.30 修正END
                End If

                If 1 < MyBase.GetResultCount Then
                    '★★★2011.08.30 修正START
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E494", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E330", New String() {"商品コード", "商品マスタ"})
                    Return False
                    '★★★2011.08.30 修正END
                End If

                'EDI出荷(中)の初期値設定処理
                '★★★2011.08.30 修正START
                'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    Return False
                End If
                '★★★2011.08.30 修正END

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                '★★★2011.08.30 修正START
                'ds = Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i)
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                    Return False
                End If
                '★★★2011.08.30 修正END

            Else
                If String.IsNullOrEmpty(custGoodsCd) = False Then

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    '2011.10.06 START EDI(メモ)№79
                    setDtL.ImportRow(dtL.Rows(0))
                    setDtM.ImportRow(dtM.Rows(i))
                    '2011.10.06 END

                    '★★★
                    'choiceKb = Me.SetGoodsWarningChoiceKb(setDt, ds, LMH030BLC.DUPONT_WID_M001, i)
                    '2011.10.06 START EDI(メモ)№79 対応で修正
                    choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.DUPONT_WID_M001, 0)
                    '2011.10.06 END
                    '★★★

                    If choiceKb.Equals("03") = True Then
                        'ワーニングで"キャンセル"を選択時
                        '★★★2011.08.30 修正START
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                        'MyBase.SetMessage("G042")
                        '★★★2011.08.30 修正END
                    End If

                    '商品マスタ検索（NRS商品コード or 荷主商品コード）
                    setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsOutka", setDs))

                    If MyBase.GetResultCount = 0 Then
                        '★★★2011.08.30 修正START
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                        'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                        'MyBase.SetMessage("E326", New String() {"商品", "商品マスタ"})
                        Return False
                        '★★★2011.08.30 修正END
                    ElseIf GetResultCount() > 1 Then

                        '入目 + 荷主商品コードで再検索
                        setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsIrimeOutka", setDs))

                        If MyBase.GetResultCount = 1 Then
                        Else
                            '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                            ''20120216 DEL S_KOBAYASHI
                            'MyBase.SetMessage("W162")
                            msgArray(1) = String.Empty
                            msgArray(2) = String.Empty
                            msgArray(3) = String.Empty
                            msgArray(4) = String.Empty

                            ds = Me._Blc.SetComWarningM("W162", LMH030BLC.DUPONT_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)
                            '▼▼▼
                            flgWarning = True 'ワーニングフラグをたてて処理続行
                            '▲▲▲
                            Continue For
                        End If

                    End If

                    'EDI出荷(中)の初期値設定処理
                    '★★★2011.08.30 修正START
                    'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                    If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                        Return False
                    End If
                    '★★★2011.08.30 修正END

                    '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                    '★★★2011.08.30 修正START
                    'ds = Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i)
                    If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                        Return False
                    End If
                    '★★★2011.08.30 修正END

                Else
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E019", New String() {"商品コード"})
                    Return False

                End If

            End If

        Next

        '▼▼▼　
        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If
        '▲▲▲

        Return True

    End Function

#End Region

#Region "届先マスタチェック(デュポン用)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function DupontDestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Dim mSysDelF As String = String.Empty
        Dim mDestNm As String = String.Empty
        Dim mAd1 As String = String.Empty
        Dim mAd2 As String = String.Empty
        Dim mAd3 As String = String.Empty
        Dim mZip As String = String.Empty
        Dim mAdAll As String = String.Empty

        Dim ediDestCd As String = String.Empty
        Dim ediDestNm As String = String.Empty
        Dim ediZip As String = String.Empty
        Dim ediFreeC21 As String = String.Empty
        Dim ediFreeC23 As String = String.Empty
        Dim ediRemark As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        mSysDelF = dtM.Rows(0).Item("SYS_DEL_FLG").ToString()
        mDestNm = dtM.Rows(0).Item("DEST_NM").ToString()
        mAd1 = dtM.Rows(0).Item("AD_1").ToString()
        mAd2 = dtM.Rows(0).Item("AD_2").ToString()
        mAd3 = dtM.Rows(0).Item("AD_3").ToString()
        mZip = dtM.Rows(0).Item("ZIP").ToString()
        mAdAll = String.Concat(mAd1, mAd2, mAd3)

        ediDestCd = dtEdi.Rows(0)("DEST_CD").ToString()
        ediDestNm = dtEdi.Rows(0)("DEST_NM").ToString()
        ediZip = dtEdi.Rows(0)("DEST_ZIP").ToString()
        ediFreeC21 = dtEdi.Rows(0)("FREE_C21").ToString()
        ediFreeC23 = dtEdi.Rows(0)("FREE_C23").ToString()
        ediRemark = dtEdi.Rows(0)("REMARK").ToString()

        '★★★2012.01.12 START
        '整合性チェック時のワーニング有無フラグ
        Dim compareWarningFlg As Boolean = False
        '★★★2012.01.12 START

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            '★★★2011.08.30 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E331", New String() {"届先コード", "届先マスタ"})
            Return False
            '★★★2011.08.30 修正END
        End If


        mDestNm = SpaceCutChk(mDestNm)
        ediDestNm = SpaceCutChk(ediDestNm)
        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else

            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DUPONT_WID_L001, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ''!!!TO-DO 後にEXCEL出力に変更!!!
                'MyBase.SetMessage("W159", New String() {"届先名称", "届先マスタ", "届先名称", "EDIデータ"})
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                ds = Me._Blc.SetComWarningL("W159", LMH030BLC.DUPONT_WID_L001, ds, msgArray, ediDestNm, mDestNm)

                '★★★2012.01.12 要望番号596 START
                compareWarningFlg = True
                '★★★2012.01.12 要望番号596 END

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtEdi.Rows(0)("DEST_NM") = dtM.Rows(0).Item("DEST_NM").ToString()
            ElseIf choiceKb.Equals("03") = True Then
                'ワーニングで"キャンセル"を選択時
                '★★★2011.08.30 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                'MyBase.SetMessage("G042")
                '★★★2011.08.30 修正END

            End If

        End If

        '▼▼▼要望番号:562
        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(ediZip) = True Then
        '    'チェックなし
        'Else
        '    If mZip.Equals(ediZip) = False Then

        '        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DUPONT_WID_L002, 0)

        '        If String.IsNullOrEmpty(choiceKb) = True Then

        '            ''!!!TO-DO 後にEXCEL出力に変更!!!
        '            'MyBase.SetMessage("W159", New String() {"届先郵便番号", "届先マスタ", "郵便番号", "EDIデータ"})
        '            msgArray(1) = "届先郵便番号"
        '            msgArray(2) = "届先マスタ"
        '            msgArray(3) = "郵便番号"
        '            msgArray(4) = "EDIデータ"
        '            ds = Me._Blc.SetComWarningL("W159", LMH030BLC.DUPONT_WID_L002, ds, msgArray, ediZip, mZip)

        '★★★2012.01.12 要望番号596 START
        'compareWarningFlg = True
        ''★★★2012.01.12 要望番号596 END

        '        ElseIf choiceKb.Equals("01") = True Then
        '            'ワーニングで"はい"を選択時
        '            dtEdi.Rows(0)("DEST_ZIP") = dtM.Rows(0).Item("ZIP").ToString()
        '        ElseIf choiceKb.Equals("03") = True Then
        '            'ワーニングで"キャンセル"を選択時
        '            '★★★2011.08.30 修正START
        '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
        '            'MyBase.SetMessage("G042")
        '            '★★★2011.08.30 修正END

        '        End If
        '    End If

        'End If
        '▲▲▲要望番号:562

        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DUPONT_WID_L003, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("W159", New String() {"届先住所", "届先マスタ", "住所", "EDIデータ"})
                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningL("W159", LMH030BLC.DUPONT_WID_L003, ds, msgArray, ediFreeC21, mAdAll)

                    '★★★2012.01.12 要望番号596 START
                    compareWarningFlg = True
                    '★★★2012.01.12 要望番号596 END

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtEdi.Rows(0)("DEST_AD_1") = mAd1
                    dtEdi.Rows(0)("DEST_AD_2") = mAd2
                    dtEdi.Rows(0)("DEST_AD_3") = mAd3
                    dtEdi.Rows(0)("FREE_C21") = mAdAll
                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    '★★★2011.08.30 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("G042")
                    '★★★2011.08.30 修正END

                End If
            End If

        End If

        'FREE_C23:注意事項(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC23) = True Then
            'チェックなし
        Else

            ediRemark = SpaceCutChk(ediRemark)
            ediFreeC23 = SpaceCutChk(ediFreeC23)
            If ediRemark.Equals(ediFreeC23) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DUPONT_WID_L004, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("W165")
                    msgArray(1) = String.Empty
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    ds = Me._Blc.SetComWarningL("W165", LMH030BLC.DUPONT_WID_L004, ds, msgArray, ediRemark, ediFreeC23)

                    '★★★2012.01.12 要望番号596 START
                    compareWarningFlg = True
                    '★★★2012.01.12 要望番号596 END

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    '★★★2011.08.30 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("G042")
                    '★★★2011.08.30 修正END

                End If
            End If

        End If

        '★★★2012.01.12 要望番号596 START
        'ワーニングが存在する場合はここでの判定はFalseで返す
        If compareWarningFlg = True Then
            Return False
        End If
        '★★★2012.01.12 要望番号596 END

        Return True

    End Function

#End Region

#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' 
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

#Region "ワーニング処理(EDI(中)(商品))選択区分の取得"

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
        '★★★
        'Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        '★★★
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

#Region "実績作成処理"

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        '★★★2011.08.30 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim updFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_L_UPD_FLG").ToString()
        '★★★2011.08.30 追加END

        '2011.10.07 START デュポンEDIデータ即実績作成対応
        Dim outkaDelFlg As String = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
        '2011.10.07 END

        '★★★2011.09.14 追加START
        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CUST_INDEX").ToString())
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        '出荷(中)オーダー番号チェック(荷主コード:00588-00,00689-00のみ)


        Dim flgWarning As Boolean = False

        '要望番号1041 修正START
        If custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00588_00) = True OrElse _
           custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00689_00) = True OrElse _
           custIndex.Equals(LMH030BLC.EdiCustIndex.DupontChb00689_00) = True Then
            '要望番号1041 修正END

            ds = MyBase.CallDAC(Me._Dac, "SelectDupontOrderCheck", ds)
            '実績作成のワーニングは検討が必要!!!
            'If MyBase.GetResultCount > 0 Then
            Dim max As Integer = MyBase.GetResultCount - 1
            For i As Integer = 0 To max


                Dim chkDt As DataTable = ds.Tables("LMH030_H_SEND_CHECK_DPN")
                Dim custOrderDtl As String = chkDt.Rows(i).Item("CUST_ORD_NO_DTL").ToString()
                Dim ediOrderNo As String = chkDt.Rows(i).Item("EDI_ORDER_NO").ToString()
                Dim custGoodsCd As String = chkDt.Rows(i).Item("GOODS_CD_CUST").ToString()
                Dim ediGoodsCd As String = chkDt.Rows(i).Item("EDI_GOODS").ToString()
                Dim lotNo As String = chkDt.Rows(i).Item("LOT_NO").ToString()
                Dim ediLot As String = chkDt.Rows(i).Item("EDI_LOT").ToString()
                Dim alctdNb As String = chkDt.Rows(i).Item("ALCTD_NB").ToString()
                Dim ediNb As String = chkDt.Rows(i).Item("EDI_NB").ToString()

                If String.IsNullOrEmpty(ediGoodsCd) = True Then

                    'ワーニング
                    choiceKb = Me.SetGoodsWarningChoiceKb(chkDt, ds, LMH030BLC.DUPONT_WID_M004, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then
                        If custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00588_00) = True Then
                            '出荷手入力追加データ有ワーニング
                            msgArray(1) = "オーダー番号,商品コード,LOT,個数"
                            msgArray(2) = String.Concat(":", custOrderDtl)
                            msgArray(3) = String.Concat(":", custGoodsCd)
                            msgArray(4) = String.Concat(":", lotNo)
                            msgArray(5) = String.Concat(":", alctdNb)
                            'ds = Me._Blc.SetComWarningJissekiM("W177", LMH030BLC.DUPONT_WID_M003, ds, msgArray)
                            ds = Me._Blc.SetComWarningJissekiM("W189", LMH030BLC.DUPONT_WID_M004, ds, msgArray, i)
                            'Return ds
                        ElseIf custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00689_00) = True OrElse _
                               custIndex.Equals(LMH030BLC.EdiCustIndex.DupontChb00689_00) = True Then
                            '出荷手入力追加データ有ワーニング
                            msgArray(1) = "商品コード,LOT,数量"
                            msgArray(2) = String.Concat(":", custGoodsCd)
                            msgArray(3) = String.Concat(":", lotNo)
                            msgArray(4) = String.Concat(":", alctdNb)
                            msgArray(5) = String.Empty
                            ds = Me._Blc.SetComWarningJissekiM("W205", LMH030BLC.DUPONT_WID_M004, ds, msgArray, i)
                        End If

                        flgWarning = True
                        Continue For

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時

                    ElseIf choiceKb.Equals("03") = True Then
                        'ワーニングで"キャンセル"を選択時
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        Return ds

                    End If

                Else

                    'ワーニング
                    choiceKb = Me.SetGoodsWarningChoiceKb(chkDt, ds, LMH030BLC.DUPONT_WID_M003, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then
                        If custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00588_00) = True Then
                            'EDIと出荷データの差異ワーニング
                            msgArray(1) = "オーダー番号,商品コード,LOT,個数"
                            msgArray(2) = String.Concat(custOrderDtl, ":", ediOrderNo)
                            msgArray(3) = String.Concat(custGoodsCd, ":", ediGoodsCd)
                            msgArray(4) = String.Concat(lotNo, ":", ediLot)
                            msgArray(5) = String.Concat(alctdNb, ":", ediNb)
                            'ds = Me._Blc.SetComWarningJissekiM("W177", LMH030BLC.DUPONT_WID_M003, ds, msgArray)
                            ds = Me._Blc.SetComWarningJissekiM("W177", LMH030BLC.DUPONT_WID_M003, ds, msgArray, i)
                            'Return ds
                        ElseIf custIndex.Equals(LMH030BLC.EdiCustIndex.Dupont00689_00) = True OrElse _
                               custIndex.Equals(LMH030BLC.EdiCustIndex.DupontChb00689_00) = True Then
                            'EDIと出荷データの差異ワーニング
                            msgArray(1) = "商品コード,LOT,数量"
                            msgArray(2) = String.Concat(custGoodsCd, ":", ediGoodsCd)
                            msgArray(3) = String.Concat(lotNo, ":", ediLot)
                            msgArray(4) = String.Concat(alctdNb, ":", ediNb)
                            msgArray(5) = String.Empty
                            'ds = Me._Blc.SetComWarningJissekiM("W177", LMH030BLC.DUPONT_WID_M003, ds, msgArray)
                            ds = Me._Blc.SetComWarningJissekiM("W204", LMH030BLC.DUPONT_WID_M003, ds, msgArray, i)
                            'Return ds
                        End If

                        flgWarning = True
                        Continue For

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時

                    ElseIf choiceKb.Equals("03") = True Then
                        'ワーニングで"キャンセル"を選択時
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        Return ds

                    End If

                End If
            Next
            'Else

            'End If

            If flgWarning = True Then
                Return ds
            End If

        End If
        '★★★2011.09.14 追加END

        'デュポン専用EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        '★★★2011.08.30 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI受信テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.08.30 追加END

        'デュポンEDI実績の値設定(EDI受信TBLより)

        ds = MyBase.CallDAC(Me._Dac, "SelectDupontEdiSend", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI受信テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"EDI受信テーブル", "該当レコード"})
            Return ds
            Exit Function
        End If

        'デュポンEDI実績データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertDupontEdiSendData", ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        '★★★2011.08.30 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.08.30 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        '★★★2011.08.30 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.08.30 追加END

        '出荷(大)の更新
        '★★★2011.08.30 追加START
        '2011.10.07 START デュポンEDIデータ即実績作成対応
        If updFlg.Equals("1") = True AndAlso (String.IsNullOrEmpty(outkaDelFlg)) = False Then
            '2011.10.07 END
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If
        '★★★2011.08.30 追加END

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

        '受信HEDデータセット
        ds = Me.SetDatasetEdiRcvHed(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetEdiRcvDtl(ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

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

#Region "EDI取消"
    ''' <summary>
    ''' EDI取消
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大),EDI出荷(中),EDI受信(HED),EDI受信(DTL)の削除フラグ変更</remarks>
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds

    End Function
#End Region

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        '★★★2011.08.30 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.08.30 追加END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds

    End Function

#End Region

#Region "実績作成済⇒実績未,実績送信済⇒実績未(実行処理)"
    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JikkouSyori(ByVal ds As DataSet) As DataSet

        '2011.10.07 START デュポンEDIデータ即実績作成,実績送信済⇒実績未の対応
        Dim outkaDelFlg As String = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
        '2011.10.07 END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "DeleteEdiSendLData", ds)

        '2011.10.07 START デュポンEDIデータ即実績作成,実績送信済⇒実績未の対応
        '出荷(大)の更新
        If String.IsNullOrEmpty(outkaDelFlg) = False Then
            '出荷(大)の更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If
        '2011.10.07 END

        Return ds

    End Function

#End Region

#Region "実績送信済⇒送信待"
    ''' <summary>
    ''' 実績送信済⇒送信待
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SousinSousinmi(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiSendLData", ds)

        Return ds

    End Function

#End Region

#Region "EDI取消⇒未登録"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大),EDI出荷(中),EDI受信(HED),EDI受信(DTL)の削除フラグ変更</remarks>
    Private Function EdiMitouroku(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds

    End Function
#End Region

#Region "出荷取消⇒未登録"
    ''' <summary>
    ''' 出荷取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds

    End Function

#End Region

#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        '★★★2011.08.30 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.08.30 追加END

        Dim rtnResult As Boolean = False

        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "02" Then

            '運送会社マスタの存在チェック＋運送名称の設定
            ds = MyBase.CallDAC(Me._Dac, "SelectUnsoNM", ds)

            '▼▼▼
            If MyBase.IsMessageExist = True Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E079", New String() {"運送会社マスタ", "運送会社コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            '▲▲▲
        End If

        ds = MyBase.CallDAC(Me._Dac, "UpdateHenko", ds)
        '★★★2011.08.30 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.08.30 追加END

        Return ds

    End Function

#End Region

    '★★★2012.01.12 要望番号597 START(nishikawa)
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
    '★★★2012.01.12 要望番号597 END(nishikawa)

#End Region

End Class
