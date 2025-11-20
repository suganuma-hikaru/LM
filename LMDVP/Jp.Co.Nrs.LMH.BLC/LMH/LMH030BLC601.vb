' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  601　　　 : 日本合成(名古屋)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC601
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC601 = New LMH030DAC601()

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


    ''' <summary>
    ''' 名古屋　日本合成化学　出荷か運送判定用  ADD 2017/05/26
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutEdiL_FREE_C30 As String = String.Empty

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

        '★★★2011.10.04 追加START
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.10.04 追加END

        'EDI出荷(大)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            '★★★2011.10.04 追加START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            '★★★2011.10.04 追加END
            Return ds
        End If

        'ADD 2017/05/26 名古屋　日本合成化学用　出荷か運送か判断用
        _OutEdiL_FREE_C30 = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString

        'EDI出荷(中)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            '★★★2011.10.04 追加START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            '★★★2011.10.04 追加END
            Return ds
        End If

        'EDI出荷(大)の初期値設定後の関連チェック
        '★★★2011.10.04 追加START
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(大)の初期値設定後のDB存在チェック
        '★★★2011.10.04 追加START
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        '★★★2011.10.04 追加START
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        'ADD 2017/05/26
        If _OutEdiL_FREE_C30.Equals("01-000000000") _
            Or _OutEdiL_FREE_C30.Equals("01-N00000000") Then
            '運送

            '出荷管理番号(大)の採番
            '運送登録なので、出荷は作成しない
            ds = Me.GetOutkaNoLunso(ds)

            ''出荷管理番号(中)の採番
            '運送登録なので、出荷は作成しない
            ds = Me.GetOutkaNoMunso(ds)
        Else
            '出荷
            '出荷管理番号(大)の採番
            ds = Me.GetOutkaNoL(ds)

            ''出荷管理番号(中)の採番
            ds = Me.GetOutkaNoM(ds)

        End If

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

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
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'DEL 2017/01/07
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If
        ''★★★2011.10.04 追加END

        'EDI受信(DTL)の更新(出荷)
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)


        'ADD 2017/05/26
        If _OutEdiL_FREE_C30.Equals("01-000000000") _
            Or _OutEdiL_FREE_C30.Equals("01-N00000000") Then
            '運送EDIワークにセット（物流費用）
            ds.Tables("LMH030_EDI_RCV_DTL").Rows(0).Item("OUTKA_CTL_NO") = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString.Substring(3, 9)
        End If
        'EDI受信(DTL)の更新(輸送) ADD 2017/01/07 輸送を更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)


        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

        'ADD 2017/05/26
        If _OutEdiL_FREE_C30.Equals("01-000000000") = False _
            And _OutEdiL_FREE_C30.Equals("01-N00000000") = False Then
            '運送（出荷登録）は対象外

            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)

            '出荷(中)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)
        End If

        '★★★2011.10.17 届先マスタ自動追加START
        '届先マスタの自動追加(日本合成化学専用)
        If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If
        '★★★2011.10.17 END

        '★★★2011.10.17 届先マスタ自動更新START
        '届先マスタの自動追加(日本合成化学専用)
        If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("UPDATE_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
        End If
        '★★★2011.10.17 END

        '★★★2011.11.08 要望番号439 届先マスタ自動追加START
        '届先マスタの自動追加(日本合成化学専用)
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If
        '★★★2011.11.08 END

        '届先マスタの更新(日本合成化学専用)
        '★★★2011.10.17 届先マスタ自動更新 修正START
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

#If True Then   'ADD 2017/05/26

#Region "データセット設定(運送番号)"

    ''' <summary>
    ''' データセット設定(運送番号)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoLunso(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim max As Integer = dt.Rows.Count - 1

        '運送登録の場合
        Dim num As New NumberMasterUtility
        dr("FREE_C30") = String.Concat("01-", num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd))

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
    Private Function GetOutkaNoMunso(ByVal ds As DataSet) As DataSet

        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1

        Dim unsoNoL As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C30").ToString()
        Dim unsoNoM As String = String.Empty

        '運送登録の場合
        For i As Integer = 0 To max
            unsoNoM = (i + 1).ToString("000")
            dtEdiM.Rows(i)("FREE_C30") = String.Concat(unsoNoL, unsoNoM)
        Next

        Return ds

    End Function

#End Region


#End If

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
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty

        outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")

        'ADD 2017/05/26 Start
        If ediDr("FREE_C30").ToString.Substring(0, 2).Equals("01")  Then
            '運送（出荷登録）

            outkaDr("OUTKA_NO_L") = ediDr("FREE_C30").ToString.Substring(3, 9)
        Else
            '通常出荷登録
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
        End If
        'ADD 2017/05/26 End

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
        '納品書摘要:日本合成化学の場合のみ設定
        outkaDr("NHS_REMARK") = ediDr("DEST_MAIL")
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

            '2011.10.04 不具合対応 START
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
            '2011.10.04 不具合対応 END

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
            '№028対応 2011.08.27
            If i <> 0 Then
                '2011.09.11 出荷管理番号中番不具合対応 START
                outkaedimDr("OUTKA_CTL_NO_CHU") = (Convert.ToInt32(outkaCtlNoChu) + 1).ToString("000")
                'outkaedimDr("OUTKA_CTL_NO_CHU") = Convert.ToInt32(outkaCtlNoChu) + 1
                '2011.09.11 出荷管理番号中番不具合対応 END
            End If
            '№028対応 2011.08.27

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            'rcvDr("CUST_CD_L") = outkaedilDr("CUST_CD_L")
            'rcvDr("CUST_CD_M") = outkaedilDr("CUST_CD_M")
            rcvDr("SYS_DEL_FLG") = "0"
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            'rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU")

            '№028対応 2011.08.27
            outkaCtlNoChu = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("OUTKA_CTL_NO_CHU") = outkaCtlNoChu
            '№028対応 2011.08.27

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


        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")

        'UPD 2017/05/26
        If _OutEdiL_FREE_C30.Equals("01-000000000") _
            Or _OutEdiL_FREE_C30.Equals("01-N00000000") Then
            '運送からの出荷登録なので
            unsoDr("UNSO_NO_L") = ediDr("FREE_C30").ToString().Substring(3, 9)
        Else
            '通常の出荷登録時
            '新規採番
            Dim num As New NumberMasterUtility
            unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
        End If

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

        'UPD 2017/05/26
        If _OutEdiL_FREE_C30.Equals("01-000000000") _
             Or _OutEdiL_FREE_C30.Equals("01-N00000000") Then
            '運送からの出荷登録なので
            unsoDr("MOTO_DATA_KB") = "40"
        Else
            '通常の出荷登録時
            unsoDr("MOTO_DATA_KB") = "20"
        End If

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

            'UPD 2017/05/26
            If _OutEdiL_FREE_C30.Equals("01-000000000") _
                Or _OutEdiL_FREE_C30.Equals("01-N00000000") Then
                '運送からの出荷登録なので
                unsoMDr("UNSO_NO_M") = (i + 1).ToString("000")
            Else
                unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")

            End If

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

        '★★★2011.10.04 修正START
        unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
        'For i As Integer = 0 To max

        '    unsoMDr = ds.Tables("LMH030_UNSO_M").Rows(i)

        '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
        '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

        '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

        'Next
        '★★★2011.10.04 修正END

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
    '''         '★★★2011.10.04 追加START
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
            '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)

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
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"包装単位", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        '★★★2011.10.04 修正START
        Return True
        '★★★2011.10.04 修正END

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定(日本合成(名古屋)専用)"

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

        '2011.10.04 温度区分対応 START
        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        '2011.10.04 温度区分対応 END

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
            '2011.10.04 運送温度区分対応 START
            '運送温度区分(区分マスタ)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"出荷区分", "区分マスタ"})
                Return False
            End If
            '2011.10.04 運送温度区分対応 END

        End If

        '入目
        'If (ediMDr("IRIME").ToString()).Equals("0") = True _
        'AndAlso (mGoodsDr("STD_IRIME_NB").ToString()).Equals("0") = False Then

        '    ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")

        'End If

        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then

            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")

        End If

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then

            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
            '▼▼▼2011.10.04 追加START
        Else

            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー(サクラ以外)
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
            '▲▲▲2011.10.04 追加END
        End If

        '出荷包装個数
        '出荷端数
        Dim pkgNb As Double = 0
        Dim outkaPkgNb As Double = 0
        Dim outkaHasu As Double = 0
        Dim alctdKb As String = String.Empty
        Dim irime As Double = 0
        Dim outkaTtlQt As Double = 0
        pkgNb = Convert.ToDouble(ediMDr("PKG_NB"))
        outkaPkgNb = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        outkaHasu = Convert.ToDouble(ediMDr("OUTKA_HASU"))
        outkaTtlQt = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))
        alctdKb = ediMDr("ALCTD_KB").ToString
        irime = Convert.ToDouble(ediMDr("IRIME"))

        'If String.IsNullOrEmpty(alctdKb) = False AndAlso alctdKb.Equals("01") = True Then

        Select Case alctdKb

            Case "01"
                If 1 < pkgNb Then

                    '★★★2011.10.20 小数点以下切捨て 修正START
                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    'ediMDr("OUTKA_PKG_NB") = (pkgNb * outkaPkgNb + outkaHasu) / pkgNb
                    '★★★2011.10.20 修正END
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb

                Else
                    '2011.09.27 修正 START 出荷画面対応による個数,端数の値入替
                    'ediMDr("OUTKA_PKG_NB") = 0
                    'ediMDr("OUTKA_HASU") = pkgNb * outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                    '2011.09.27 修正 END

                End If
                ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

            Case "02"
                ediMDr("OUTKA_PKG_NB") = 0
                If outkaTtlQt Mod irime = 0 Then
                    ediMDr("OUTKA_HASU") = outkaTtlQt / irime

                Else
                    '★★★2011.10.20 小数点以下切捨て 修正START
                    ediMDr("OUTKA_HASU") = Math.Floor(outkaTtlQt / irime) + 1
                    'ediMDr("OUTKA_HASU") = outkaTtlQt / irime + 1
                    '★★★2011.10.20 修正END

                End If
                ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")

            Case "03"
                ediMDr("OUTKA_PKG_NB") = 0
                ediMDr("OUTKA_HASU") = 0
                ediMDr("OUTKA_TTL_NB") = 0

            Case Else

        End Select

        'End If

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
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E349", New String() {"EDIデータ", "出荷管理番号"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"出荷報告有無"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷予定日"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出庫日"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E166", New String() {"出荷予定日", "出庫日"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"納入予定日"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷報告日"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(大)"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(中)"})
            '★★★2011.10.04 修正END
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"送り状作成有無"})
            '★★★2011.10.04 修正END
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
                    '★★★2011.10.04 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E377", New String() {"出荷データ"})
                    Return False
                    '★★★2011.10.04 修正END
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷種別区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"作業進捗区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"ピッキングリスト区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"倉庫コード", "倉庫マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"納入予定時刻", "区分マスタ"})
                Return False
                '★★★2011.10.04 修正END
            End If

        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"荷主コード", "荷主マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"運送手配区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()      '荷送人コード
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

        '荷送人コードのマスタ存在チェック
        'SHIP_CD_Lが空でなく、SHIP_CD_L = DEST_CD <> EDI_DEST_CD の場合、もしくはSHIP_CD_L <> DEST_CD の場合
        If (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = True AndAlso shipCdL.Equals(ediDestCd) = False) _
            OrElse (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = False) Then

            ds = MyBase.CallDAC(Me._Dac, "SelectDataNcgoMdest", ds)

            If MyBase.GetResultCount = 0 Then
                'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                If SetInsMDestFromShip(ds) = True Then
                    flgWarning = True
                End If
            Else
                If drEdiL("SHIP_NM_L").ToString().Equals(dtMS.Rows(0).Item("DEST_NM").ToString()) = False Then
                    'ワーニング⇒マスタ更新(ワーニング設定した場合はflgWarning=True)
                    If SetUpdMDestFromShip(ds) = True Then
                        flgWarning = True
                    End If
                End If
            End If
        End If

        '届先コード(EDI届先コード)のマスタ存在チェック
        If String.IsNullOrEmpty(workDestCd) = False Then

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdest", ds)

            If MyBase.GetResultCount = 0 Then
                'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                '要望番号611 2011.12.09 修正START
                If SetInsMDestFromDest(ds, workDestCd, workDestString, rowNo, ediCtlNo) = True Then
                    flgWarning = True
                Else
                    If ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
                        Return False
                        '要望番号611 2011.12.09 修正END
                    End If
                End If
            ElseIf 1 < MyBase.GetResultCount Then
                '複数件の場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            Else
                '1件に特定できた場合、日本合成(名古屋)専用 マスタ値とEDI出荷(大)の整合性チェック
                If Me.NcgoDestCompareCheck(ds, rowNo, ediCtlNo) = False Then

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
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"タリフ分類区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"車輌区分", "区分マスタ"})
                Return False
                '★★★2011.10.04 修正END
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"便区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
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
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"})
                Return False
                '★★★2011.10.04 修正END
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"})
                Return False
                '★★★2011.10.04 修正END
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"元着払い区分", "区分マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If

        '★★★2011.10.17 追加START
        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合は届先マスタの自動追加または自動更新を行う為、EDI出荷(大)のチェック終了後に
        'ワーニングがある（flgWarning=True）場合は処理を終了させる。
        '-----------------------------------------------------------------------------------------------------------

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If
        '★★★2011.10.17 追加END

        Return True

    End Function

    '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

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
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.NCGO_WID_L005, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = "荷送人コード"
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.NCGO_WID_L005, ds, msgArray, shipCdL, String.Empty)

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
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.NCGO_WID_L006, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = "荷送人名（大）"
            msgArray(2) = "届先マスタ"
            msgArray(3) = "届先名称"
            msgArray(4) = "EDIデータ"

            ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L006, ds, msgArray, drEdiL("SHIP_NM_L").ToString(), dtMS.Rows(0).Item("DEST_NM").ToString())

            Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            dtMS.Rows(0).Item("DEST_CD") = shipCdL
            dtMS.Rows(0).Item("DEST_NM") = drEdiL("SHIP_NM_L").ToString()
            'マスタ更新対象フラグ
            dtMS.Rows(0).Item("MST_UPDATE_FLG") = "1"

        End If

    End Function

    ''' <summary>
    ''' 届先コード(EDI届先コード)から届先マスタInsertデータを作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ワーニング設定をする
    ''' ワーニング画面の戻り値がある場合、届先コード(EDI届先コード)から届先マスタUpdateデータを作成する
    ''' </remarks>
    Private Function SetInsMDestFromDest(ByVal ds As DataSet, ByVal workDestCd As String, ByVal workDestString As String, _
                                          ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMD As DataTable = ds.Tables("LMH030_M_DEST")
        Dim msgArray(5) As String
        Dim choiceKb As String = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.NCGO_WID_L008, 0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        '要望番号611 2011.12.09 追加START
        Dim dtMZ As DataTable = ds.Tables("LMH030_M_ZIP")
        '要望番号611 2011.12.09 追加END

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            msgArray(4) = String.Empty

            ds = Me._Blc.SetComWarningL("W182", LMH030BLC.NCGO_WID_L008, ds, msgArray, workDestCd, String.Empty)

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

            '要望番号611 2011.12.08 修正START
            If String.IsNullOrEmpty(drEdiL("DEST_ZIP").ToString) = False _
               AndAlso String.IsNullOrEmpty(drEdiL("DEST_JIS_CD").ToString) = True Then

                '郵便番号(郵便番号マスタ)
                ds = MyBase.CallDAC(Me._DacMst, "SelectDataMzip", ds)
                If MyBase.GetResultCount = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先郵便番号", "郵便番号マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                Else
                    drMD("JIS") = dtMZ.Rows(0).Item("JIS_CD").ToString()
                End If

            Else
                drMD("JIS") = drEdiL("DEST_JIS_CD").ToString()
            End If
            '要望番号611 2011.12.08 修正END
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
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"引当単位区分"})
            Return False
            '★★★2011.10.04 修正END
        End If
        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E352")
            Return False
            '★★★2011.10.04 修正END
        End If
        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
            Return False
            '★★★2011.10.04 修正END
        End If
        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"入目と出荷総数量"})
            Return False
            '★★★2011.10.04 修正END
        End If
        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
            Return False
            '★★★2011.10.04 修正END
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
        Dim wkDs As DataSet = ds.Copy()     'ADD 2017/05/30

        '2011.10.06 START EDI(メモ)№79
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        '▼▼▼2011.10.04 追加START
        Dim flgWarning As Boolean = False
        '▲▲▲2011.10.04 追加END

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
                    '★★★2011.10.04 修正START
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E326", New String() {"商品コード", "商品マスタ"})
                    Return False
                    '★★★2011.10.04 修正END
                End If

                If 1 < MyBase.GetResultCount Then
                    '★★★2011.10.04 修正START
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E494", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E330", New String() {"商品コード", "商品マスタ"})
                    Return False
                    '★★★2011.10.04 修正END
                End If

                'EDI出荷(中)の初期値設定処理
                '★★★2011.10.04 修正START
                'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    Return False
                End If
                '★★★2011.10.04 修正END


                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                '★★★2011.10.04 修正START
                'ds = Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i)

                '通常出荷登録時
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                    Return False
                End If
                '★★★2011.10.04 修正END


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
                    'choiceKb = Me.SetGoodsWarningChoiceKb(setDt, ds, LMH030BLC.NCGO_WID_M001, i)
                    choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.NCGO_WID_M001, 0)
                    '★★★

                    If choiceKb.Equals("03") = True Then
                        'ワーニングで"キャンセル"を選択時
                        '★★★2011.10.04 修正START
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                        'MyBase.SetMessage("G042")
                        '★★★2011.10.04 修正END
                    End If

                    '商品マスタ検索（NRS商品コード or 荷主商品コード）
                    setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsOutka", setDs))

                    If MyBase.GetResultCount = 0 Then
                        '★★★2011.10.04 修正START
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                        'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                        'MyBase.SetMessage("E326", New String() {"商品", "商品マスタ"})
                        Return False
                        '★★★2011.10.04 修正END
                    ElseIf GetResultCount() > 1 Then

                        '入目 + 荷主商品コードで再検索
                        setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsIrimeOutka", setDs))

                        If MyBase.GetResultCount = 1 Then
                        Else
                            '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                            '注意!!! セットメッセージは消してよいのか判断がつかないので調査する
                            'MyBase.SetMessage("W162")
                            msgArray(1) = String.Empty
                            msgArray(2) = String.Empty
                            msgArray(3) = String.Empty
                            msgArray(4) = String.Empty

                            ds = Me._Blc.SetComWarningM("W162", LMH030BLC.NCGO_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                            '▼▼▼2011.10.04 追加START
                            flgWarning = True 'ワーニングフラグをたてて処理続行
                            '▲▲▲2011.10.04 追加END

                            Continue For
                        End If

                    End If

                    'EDI出荷(中)の初期値設定処理
                    '★★★2011.10.04 修正START
                    'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                    If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                        Return False
                    End If
                    '★★★2011.10.04 修正END

                    '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                    '★★★2011.10.04 修正START
                    'ds = Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i)
                    If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                        Return False
                    End If
                    '★★★2011.10.04 修正END

                Else
                    '★★★2011.10.04 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E019", New String() {"商品コード"})
                    Return False
                    '★★★2011.10.04 修正END
                End If

            End If

        Next

        '▼▼▼2011.10.04 追加START
        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If
        '▲▲▲2011.10.04 追加END

        Return True

    End Function

#End Region

#Region "届先マスタチェック(日本合成(名古屋)用)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function NcgoDestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

        Dim mSysDelF As String = String.Empty
        Dim mDestNm As String = String.Empty
        Dim mAd1 As String = String.Empty
        Dim mAd2 As String = String.Empty
        Dim mAd3 As String = String.Empty
        Dim mZip As String = String.Empty
        Dim mTel As String = String.Empty
        Dim mJis As String = String.Empty
        Dim mAdAll As String = String.Empty
        Dim mZipJisCd As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = String.Empty
        Dim ediDestNm As String = String.Empty
        Dim ediZip As String = String.Empty
        Dim ediTel As String = String.Empty
        Dim ediFreeC21 As String = String.Empty
        Dim ediFreeC22 As String = String.Empty     'ADD 2017/02/20
        Dim ediFreeC06 As String = String.Empty     'ADD 2017/02/20
        Dim ediDestJisCd As String = String.Empty

        mSysDelF = dtM.Rows(0).Item("SYS_DEL_FLG").ToString()
        mDestNm = dtM.Rows(0).Item("DEST_NM").ToString()
        mAd1 = dtM.Rows(0).Item("AD_1").ToString()
        mAd2 = dtM.Rows(0).Item("AD_2").ToString()
        mAd3 = dtM.Rows(0).Item("AD_3").ToString()
        mZip = dtM.Rows(0).Item("ZIP").ToString()
        mTel = dtM.Rows(0).Item("TEL").ToString()
        mJis = dtM.Rows(0).Item("JIS").ToString()
        mAdAll = String.Concat(mAd1, mAd2, mAd3)

        ediDestCd = dtEdi.Rows(0)("DEST_CD").ToString()
        ediDestNm = dtEdi.Rows(0)("DEST_NM").ToString()
        ediZip = dtEdi.Rows(0)("DEST_ZIP").ToString()
        ediTel = dtEdi.Rows(0)("DEST_TEL").ToString()
        ediFreeC21 = dtEdi.Rows(0)("FREE_C21").ToString()
        ediFreeC22 = dtEdi.Rows(0)("FREE_C22").ToString()   'ADD 2017/02/20
        ediFreeC06 = dtEdi.Rows(0)("FREE_C06").ToString()   'ADD 2017/02/20
        ediDestJisCd = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        '★★★2012.01.12 START
        '整合性チェック時のワーニング有無フラグ
        Dim compareWarningFlg As Boolean = False
        '★★★2012.01.12 START

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            '★★★2011.10.04 修正START
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E331", New String() {"届先コード", "届先マスタ"})
            Return False
            '★★★2011.10.04 修正END
        End If


        mDestNm = SpaceCutChk(mDestNm)
        ediDestNm = SpaceCutChk(ediDestNm)
        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else

            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.NCGO_WID_L001, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ''!!!TO-DO 後にEXCEL出力に変更!!!
                'MyBase.SetMessage("W166", New String() {"届先名称", "届先マスタ", "届先名称", "EDIデータ"})
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L001, ds, msgArray, ediDestNm, mDestNm)

                '★★★2012.01.12 要望番号596 START
                compareWarningFlg = True
                '★★★2012.01.12 要望番号596 END

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtM.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtM.Rows(0).Item("MST_UPDATE_FLG") = "1"
            ElseIf choiceKb.Equals("03") = True Then
                'ワーニングで"キャンセル"を選択時
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                'MyBase.SetMessage("G042")
                '★★★2011.10.04 修正END
            End If

        End If

        '★★★2011.11.17 要望番号439 届先マスタ自動更新 修正START(ZIPとJISのチェック分割)
        '届先郵便番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediZip) = True Then
            '届先郵便番号が空の場合は、届先マスタの郵便番号をセットする
            dtEdi.Rows(0)("DEST_ZIP") = dtM.Rows(0).Item("ZIP").ToString()
        Else

            If mZip.Equals(ediZip) = False Then

                '郵便番号(郵便番号マスタ)
                ds = MyBase.CallDAC(Me._DacMst, "SelectDataMzip", ds)
                If MyBase.GetResultCount = 0 Then
                    '★★★2011.10.04 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先郵便番号", "郵便番号マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E326", New String() {"届先郵便番号", "郵便番号マスタ"})
                    Return False
                    '★★★2011.10.04 修正END
                Else
                    '▼▼▼要望番号:562
                    'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.NCGO_WID_L002, 0)

                    'If String.IsNullOrEmpty(choiceKb) = True Then

                    '    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    '    'MyBase.SetMessage("W166", New String() {"届先郵便番号", "届先マスタ", "郵便番号", "EDIデータ"})
                    '    msgArray(1) = "届先郵便番号"
                    '    msgArray(2) = "届先マスタ"
                    '    msgArray(3) = "郵便番号"
                    '    msgArray(4) = "EDIデータ"
                    '    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L002, ds, msgArray, ediZip, mZip)

                    ''★★★2012.01.12 要望番号596 START
                    'compareWarningFlg = True
                    ''★★★2012.01.12 要望番号596 END

                    'ElseIf choiceKb.Equals("01") = True Then
                    '    'ワーニングで"はい"を選択時
                    '    dtM.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                    '    '★★★2011.10.04 追加START
                    '    'マスタ更新対象フラグ
                    '    dtM.Rows(0).Item("MST_UPDATE_FLG") = "1"
                    '    '★★★2011.10.04 追加END
                    'ElseIf choiceKb.Equals("03") = True Then
                    '    'ワーニングで"キャンセル"を選択時
                    '    '★★★2011.10.04 修正START
                    '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '    'MyBase.SetMessage("G042")
                    '    '★★★2011.10.04 修正END
                    'End If
                    '▲▲▲要望番号:562
                End If
            Else

            End If

        End If

        '届先JISコード(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediDestJisCd) = True Then

            '要望番号611 2011.12.09 追加START
            '郵便番号(郵便番号マスタ)
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMzip", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先郵便番号", "郵便番号マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            Else
                '要望番号611 2011.12.09 追加END
                mZipJisCd = dtMZip.Rows(0).Item("JIS_CD").ToString()
                ediDestJisCd = mZipJisCd
            End If
        End If

        If ediDestJisCd.Equals(mJis) = False Then

            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.NCGO_WID_L009, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                ''!!!TO-DO 後にEXCEL出力に変更!!!
                'MyBase.SetMessage("W166", New String() {"届先郵便番号", "届先マスタ", "郵便番号", "EDIデータ"})
                msgArray(1) = "届先JISコード"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "JISコード"
                msgArray(4) = "EDIデータ"
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L009, ds, msgArray, ediDestJisCd, mJis)

                '★★★2012.01.12 要望番号596 START
                compareWarningFlg = True
                '★★★2012.01.12 要望番号596 END

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

                '要望番号611 2011.12.09 修正START
                'dtM.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                dtM.Rows(0).Item("JIS") = ediDestJisCd
                '要望番号611 2011.12.09 修正END

                '★★★2011.10.04 追加START
                'マスタ更新対象フラグ
                dtM.Rows(0).Item("MST_UPDATE_FLG") = "1"
                '★★★2011.10.04 追加END
            ElseIf choiceKb.Equals("03") = True Then
                'ワーニングで"キャンセル"を選択時
                '★★★2011.10.04 修正START
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("G042")
                '★★★2011.10.04 修正END
            End If

        End If
        '★★★2011.11.17 要望番号439 届先マスタ自動更新 修正END(ZIPとJISのチェック分割)

        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            ediFreeC21 = ediFreeC21 + SpaceCutChk(String.Concat(ediFreeC22, ediFreeC06))   'ADD 2017/02/20

            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.NCGO_WID_L003, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("W166", New String() {"届先住所", "届先マスタ", "住所", "EDIデータ"})
                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L003, ds, msgArray, ediFreeC21, mAdAll)

                    '★★★2012.01.12 要望番号596 START
                    compareWarningFlg = True
                    '★★★2012.01.12 要望番号596 END

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtM.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                    dtM.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                    dtM.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
                    'マスタ更新対象フラグ
                    dtM.Rows(0).Item("MST_UPDATE_FLG") = "1"

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    '★★★2011.10.04 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("G042")
                    '★★★2011.10.04 修正END
                End If
            End If

        End If

        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else

            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.NCGO_WID_L004, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    ''!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("W166", New String() {"届先電話番号", "届先マスタ", "電話番号", "EDIデータ"})
                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.NCGO_WID_L004, ds, msgArray, ediTel, mTel)

                    '★★★2012.01.12 要望番号596 START
                    compareWarningFlg = True
                    '★★★2012.01.12 要望番号596 END

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtM.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtM.Rows(0).Item("MST_UPDATE_FLG") = "1"

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    '★★★2011.10.04 修正START
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                    'MyBase.SetMessage("G042")
                    '★★★2011.10.04 修正END
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

        '★★★2011.10.05 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim updFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_L_UPD_FLG").ToString()
        '★★★2011.10.05 追加END

        '★★★2011.09.14 追加START
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '出荷(中)オーダー番号チェック
        '▼▼▼(日本合成専用オーダー番号チェック)

        '日本合成EDI受信HEDから受付№、受付№枝番を取得
        ds = MyBase.CallDAC(Me._Dac, "SelectNcgoUketsukeNo", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '同一オーダー番号（枝番含まず）での変更データの有無確認
        ds = MyBase.CallDAC(Me._Dac, "SelectNcgoOrderChk", ds)
        If MyBase.GetResultCount <> 0 Then

            choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030INOUT"), ds, LMH030BLC.NCGO_WID_L007, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "同一オーダー番号での変更"
                msgArray(2) = "出荷"
                msgArray(3) = String.Empty
                msgArray(4) = String.Empty
                ds = Me._Blc.SetComWarningL("W181", LMH030BLC.NCGO_WID_L007, ds, msgArray, String.Empty, String.Empty)
                Return ds
            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            ElseIf choiceKb.Equals("03") = True Then
                'ワーニングで"キャンセル"を選択時
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                Return ds
            End If

        End If

        '同一オーダー番号での取消データの有無確認
        ds = MyBase.CallDAC(Me._Dac, "SelectNcgoOrderChkTorikeshi", ds)
        If MyBase.GetResultCount <> 0 Then

            choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030INOUT"), ds, LMH030BLC.NCGO_WID_L007, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then

                msgArray(1) = "同一オーダー番号での取消"
                msgArray(2) = "出荷"
                msgArray(3) = String.Empty
                msgArray(4) = String.Empty
                ds = Me._Blc.SetComWarningL("W181", LMH030BLC.NCGO_WID_L007, ds, msgArray, String.Empty, String.Empty)
                Return ds
            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時

            ElseIf choiceKb.Equals("03") = True Then
                'ワーニングで"キャンセル"を選択時
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)                        '!!!TO-DO 後にEXCEL出力に変更!!!
                Return ds
            End If

        End If
        '▲▲▲(日本合成専用オーダー番号チェック)

        '日本合成EDI実績の値設定(EDI受信TBLより)
        ds = MyBase.CallDAC(Me._Dac, "SelectNcgoEdiSend", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI受信テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.10.04 追加END

        '日本合成(名古屋)EDI実績の値設定(EDI受信TBLより)
        If ds.Tables("LMH030_H_SENDOUTEDI_NCGO").Rows.Count <> 0 Then

            '日本合成(名古屋)送信データ編集
            ds = Me.SetDatasetEdiNcgoSend(ds)
        End If

        '日本合成(名古屋)EDI実績データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertNcgoEdiSendData", ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'DEL 2017/01/07
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

        '出荷(大)の更新
        '★★★2011.10.04 追加START
        If updFlg.Equals("1") = True Then
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If
        '★★★2011.10.04 追加END

        Return ds

    End Function

#Region "データセット再設定(EDI日本合成(名古屋)実績送信TBL)"

    ''' <summary>
    ''' EDI送信データ編集
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiNcgoSend(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_H_SENDOUTEDI_NCGO").Rows(0)

        Dim strTemp As String = dr("UKETSUKE_NO_EDA").ToString().Trim()
        Dim upFlg As String = dr("RCV_EDA_UP_FLG").ToString().Trim()

        Dim strTempSimo As String = strTemp.Substring(strTemp.Length - 1, 1)
        Dim strTempKami As String = strTemp.Substring(0, 1)

        '受付№枝番
        Select Case strTempSimo
            Case "0" To "8", "A" To "Y", "ｱ" To "ﾜ"
                strTempSimo = Chr(Asc(strTempSimo) + 1)         '.net要変換
            Case "9"
                strTempSimo = "A"
            Case "Z"
                strTempSimo = "ｱ"
            Case Else

        End Select

        If upFlg = "1" Then
            Select Case strTempSimo
                Case "0" To "8", "A" To "Y", "ｱ" To "ﾜ"
                    strTempSimo = Chr(Asc(strTempSimo) + 1)
                Case "9"
                    strTempSimo = "A"
                Case "Z"
                    strTempSimo = "ｱ"
                Case Else

            End Select

        End If

        '受付№枝番
        dr("UKETSUKE_NO_EDA") = String.Concat(strTempKami, strTempSimo)

        '入力年月日
        '2011.10.27 日付取得　修正START
        'dr("INPUT_YMD") = Date.Now.ToString("yyyyMMdd")
        dr("INPUT_YMD") = MyBase.GetSystemDate()
        '2011.10.27 日付取得　修正END

        '容量
        dr("YORYO") = Me.FormatSpace(dr("YORYO").ToString(), 8)

        Return ds
    End Function

#End Region

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
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'del 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

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

        ' ''EDI受信(HED)の更新
        ''ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''If MyBase.GetResultCount = 0 Then
        ''    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        ''    Return ds
        ''End If

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

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

        '★★★2011.10.04 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.10.04 追加END

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.10.04 追加END

        'del 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

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

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'del 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "DeleteEdiSendLData", ds)

        '出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

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
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'DEL 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiSendLData", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

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
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'DEL 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

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
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If
        '★★★2011.10.04 追加END

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)
        'DEL 2017/01/17
        'EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        ''★★★2011.10.04 追加START
        'If MyBase.GetResultCount = 0 Then
        '    Return ds
        'End If
        '★★★2011.10.04 追加END

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'ADD 2017/02/02 輸送ワークも更新する　Start
        'EDI受信(DTL)の更新(輸送) 
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlDataUuso", ds)
        'ADD 2017/02/02 輸送ワークも更新する　End

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

        '★★★2011.10.04 追加START
        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        '★★★2011.10.04 追加END

        Dim rtnResult As Boolean = False

        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "02" Then

            '運送会社マスタの存在チェック＋運送名称の設定
            ds = MyBase.CallDAC(Me._Dac, "SelectUnsoNM", ds)

            '★★★2011.10.04 追加START
            If MyBase.IsMessageExist = True Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E079", New String() {"運送会社マスタ", "運送会社コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            '★★★2011.10.04 追加END
        End If

        ds = MyBase.CallDAC(Me._Dac, "UpdateHenko", ds)
        '★★★2011.10.04 追加START
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '★★★2011.10.04 追加END

        Return ds

    End Function

#End Region

    '▼▼▼要望番号:467
#Region "三徳出荷依頼送信データ作成"

    ''' <summary>
    ''' 三徳出荷依頼送信データ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCsv(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectCsv", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

#End Region
    '▲▲▲要望番号:467

#Region "セミEDI処理"

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

        Dim ediCustIndex As String = dtSemiInfo.Rows(0).Item("EDI_CUST_INDEX").ToString()

        Dim iRowCnt As Integer = 0
        Dim cnt As Integer = 0


        For i As Integer = 0 To hedmax

            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）
                Dim sFileNm As String = dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E460", , , LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

                '対象データのみソートして抜き出す
                Dim strSort As String = String.Empty
                Dim filter As String = "COLUMN_11 = 'SY'"
                Dim drSelect As DataRow() = dtSemiDtl.Select(filter.ToString(), strSort)
                If drSelect.Count = 0 Then
                    '抜き出したデータRowが０件の場合
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "1" '０件エラーフラグを立てる

                Else

                    'SelectしたデータをdtSemiDtlに再セットする
                    Dim dtSelect As DataTable = dtSemiDtl.Clone          'Select前テーブルの情報をクローン化
                    For Each row As DataRow In drSelect
                        dtSelect.ImportRow(row)         'SelectしたデータRowをクローンにセットする
                    Next

                    'dtSemiDtlに再セット（以降の処理はdtSemiDtlで処理されるため）
                    dtSemiDtl.Clear()
                    For k As Integer = 0 To dtSelect.Rows.Count - 1
                        dtSemiDtl.ImportRow(dtSelect.Rows(k))
                    Next
                End If

                max = dtSemiDtl.Rows.Count - 1

                For j As Integer = iRowCnt To max

                    dr = dtSemiDtl.Rows(j)

                    If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                        'ヘッダと明細のファイル名称が等しい場合

                        '入力チェック(数値,日付チェック)
                        If Me.TorikomiValChk(dr, dtSemiHed.Rows(i), cnt) = False Then

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
    ''' <param name="drSemiHed"></param>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiValChk(ByVal dr As DataRow, ByVal drSemiHed As DataRow, ByRef cnt As Integer) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sMsg As String = String.Empty
        Dim sStr As String = String.Empty
        Dim sNum As String = String.Empty
        Dim dNum As Double = 0

        sMsg = "受注ヘッダ訂正No.(カラム4番目)["
        sNum = dr.Item("COLUMN_4").ToString()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If
        End If

        sMsg = "赤黒区分(カラム5番目)["
        sStr = dr.Item("COLUMN_5").ToString()
        If Not (sStr = "1" OrElse sStr = "2") Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "細目区分(カラム190番目)["
        Dim detailKbn As String = dr.Item("COLUMN_190").ToString()
        If String.IsNullOrEmpty(detailKbn) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        ElseIf Not (detailKbn = "A" OrElse detailKbn = "F" OrElse detailKbn = "H" OrElse detailKbn = "I") Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, detailKbn, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷主オーダーNo.(カラム6番目)["
        sStr = dr.Item("COLUMN_6").ToString()
        If LenB(sStr) <> 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）０５ (MCC元項目: 荷主依頼明細№)(カラム280番目)["
        sStr = dr.Item("COLUMN_280").ToString()
        If _
            ((detailKbn = "A" OrElse detailKbn = "H") AndAlso LenB(sStr) > 6) OrElse
            ((detailKbn = "F" OrElse detailKbn = "I") AndAlso LenB(sStr) > 5) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）０３ (MCC元項目: 荷主伝票明細№)(カラム278番目)["
        sStr = dr.Item("COLUMN_278").ToString()
        If LenB(sStr) > 6 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "指定作業日(カラム14番目)["
        sStr = dr.Item("COLUMN_14").ToString()
        If IsDate(Jp.Co.Nrs.Win.Utility.DateFormatUtility.EditSlash(sStr)) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納期日付（作業期限）(カラム15番目)["
        sStr = dr.Item("COLUMN_15").ToString()
        If IsDate(Jp.Co.Nrs.Win.Utility.DateFormatUtility.EditSlash(sStr)) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "品名コード(カラム210番目)["
        sStr = dr.Item("COLUMN_210").ToString()
        If LenB(sStr) > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "品名愛称(カラム209番目)["
        sStr = dr.Item("COLUMN_209").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "品目グループ(カラム206番目)["
        sStr = dr.Item("COLUMN_206").ToString()
        If LenB(sStr) > 2 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "グレード1(カラム212番目)["
        sStr = dr.Item("COLUMN_212").ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "グレード2(カラム213番目)["
        sStr = dr.Item("COLUMN_213").ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷姿コード(カラム215番目)["
        sStr = dr.Item("COLUMN_215").ToString()
        If LenB(sStr) > 2 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "容量(カラム214番目)["
        sStr = dr.Item("COLUMN_214").ToString()
        If LenB(sStr) > 8 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "ロットNo.(カラム226番目)["
        sStr = dr.Item("COLUMN_226").ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "個数(カラム228番目)["
        sNum = dr.Item("COLUMN_228").ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(sNum)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(sNum) = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                    'ElseIf Convert.ToDouble(sNum) < 0 Then
                    '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    '    bRet = False
                ElseIf dNum > 9999999999 OrElse dNum < -9999999999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 0 Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        sMsg = "数量(カラム230番目)["
        sNum = dr.Item("COLUMN_230").ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(sNum)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(sNum) = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                    'ElseIf Convert.ToDouble(sNum) < 0 Then
                    '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E185", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    '    bRet = False
                ElseIf dNum > 999999999.999 OrElse dNum < -999999999.999 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 999D Then
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        sMsg = "先方SPコード(カラム201番目)["
        sStr = dr.Item("COLUMN_201").ToString()
        If LenB(sStr) > 4 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先コード(カラム19番目)["
        sStr = dr.Item("COLUMN_19").ToString()
        If LenB(sStr) > 12 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先名称(カラム20番目)["
        sStr = dr.Item("COLUMN_20").ToString()
        If LenB(sStr) > 120 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先情報(カラム21番目)["
        sStr = dr.Item("COLUMN_21").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先住所(カラム24番目)["
        sStr = dr.Item("COLUMN_24").ToString()
        If LenB(sStr) > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先電話番号(カラム25番目)["
        sStr = dr.Item("COLUMN_25").ToString()
        If LenB(sStr) > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入先郵便番号(カラム23番目)["
        sStr = dr.Item("COLUMN_23").ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "便区分コード(カラム42番目)["
        sStr = dr.Item("COLUMN_42").ToString()
        If LenB(sStr) > 2 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "便区分名称(カラム43番目)["
        sStr = dr.Item("COLUMN_43").ToString()
        If LenB(sStr) > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷主輸送会社(カラム44番目)["
        sStr = dr.Item("COLUMN_44").ToString()
        If LenB(sStr) > 2 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'sMsg = "荷主輸送会社名(カラム?番目)["
        'sStr = dr.Item("COLUMN_?").ToString()
        'If LenB(sStr) > 20 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
        '    bRet = False
        'End If

        sMsg = "契約先コード(カラム118番目)["
        sStr = dr.Item("COLUMN_118").ToString()
        If LenB(sStr) > 12 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "契約先名称(カラム119番目)["
        sStr = dr.Item("COLUMN_119").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（中）０１ (MCC元項目: 支払人名称２)(カラム311番目)["
        sStr = dr.Item("COLUMN_311").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "需要家名称(カラム222番目)["
        sStr = dr.Item("COLUMN_222").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "注文No.(カラム101番目)["
        sStr = dr.Item("COLUMN_101").ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時刻名称(カラム17番目)["
        sStr = dr.Item("COLUMN_17").ToString()
        If LenB(sStr) > 24 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "指定伝票名称(カラム103番目)["
        sStr = dr.Item("COLUMN_103").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称１(カラム67番目)["
        sStr = dr.Item("COLUMN_67").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称２(カラム69番目)["
        sStr = dr.Item("COLUMN_69").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称３(カラム71番目)["
        sStr = dr.Item("COLUMN_71").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称４(カラム73番目)["
        sStr = dr.Item("COLUMN_73").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称５(カラム75番目)["
        sStr = dr.Item("COLUMN_75").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称６(カラム77番目)["
        sStr = dr.Item("COLUMN_77").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称７(カラム79番目)["
        sStr = dr.Item("COLUMN_79").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称８(カラム81番目)["
        sStr = dr.Item("COLUMN_81").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称９(カラム83番目)["
        sStr = dr.Item("COLUMN_83").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入時条件名称１０(カラム85番目)["
        sStr = dr.Item("COLUMN_85").ToString()
        If LenB(sStr) > 20 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "納入条件備考(カラム62番目)["
        sStr = dr.Item("COLUMN_62").ToString()
        If LenB(sStr) > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "成績書要否名称(カラム53番目)["
        sStr = dr.Item("COLUMN_53").ToString()
        If LenB(sStr) > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷主備考（内）(カラム55番目)["
        sStr = dr.Item("COLUMN_55").ToString()
        If LenB(sStr) > 200 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷主備考（外）(カラム56番目)["
        sStr = dr.Item("COLUMN_56").ToString()
        If LenB(sStr) > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "荷姿名称(カラム216番目)["
        sStr = dr.Item("COLUMN_216").ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）１９ ～ 文字項目（短）２６ (MCC元項目: 送状非表示区分)(カラム294～301番目)["
        sStr = String.Concat(
            dr.Item("COLUMN_294").ToString(), dr.Item("COLUMN_295").ToString(),
            dr.Item("COLUMN_296").ToString(), dr.Item("COLUMN_297").ToString(),
            dr.Item("COLUMN_298").ToString(), dr.Item("COLUMN_299").ToString(),
            dr.Item("COLUMN_300").ToString(), dr.Item("COLUMN_301").ToString())
        If LenB(sStr) > 8 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）３２(MCC元項目: 基本数量単位)(カラム307番目)["
        sStr = dr.Item("COLUMN_307").ToString()
        If LenB(sStr) > 3 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）１５ (MCC元項目: データ作成日)(カラム290番目)["
        sStr = dr.Item("COLUMN_290").ToString()
        If LenB(sStr) > 8 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）１６ (MCC元項目: データ作成時刻)(カラム291番目)["
        sStr = dr.Item("COLUMN_291").ToString()
        If LenB(sStr) > 6 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

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

        Dim dtEdiOutkaRcvDtl As DataTable = ds.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW")   'EDI受信Dtl(出荷)
        dtEdiOutkaRcvDtl.Clear()
        Dim drEdiOutkaRcvDtl As DataRow = Nothing

        Dim dtEdiUnsoRcvDtl As DataTable = ds.Tables("LMH030_UNSOEDI_DTL_NCGO")         'EDI受信Dtl(輸送)
        dtEdiUnsoRcvDtl.Clear()
        Dim drEdiUnsoRcvDtl As DataRow = Nothing

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim nrsBrCd As String = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0).Item("NRS_BR_CD").ToString()   '営業所コード     

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

        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iOutHedInsCnt As Integer = 0            '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0            '書込件数（出荷EDI(中)）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0            '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0            '取消件数（出荷EDI(中)）


        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        Dim dsKbnD003 As DataSet = ds.Clone()
        Dim defCtlNo As String = GetDefCtlNo(dsKbnD003, nrsBrCd)

        Dim dsKbnM036 As DataSet = ds.Clone()
        Call GetYusoCompNm(dsKbnM036, nrsBrCd)

        Dim tmpDt As DataTable
        Dim tmpDr() As DataRow

        Dim num As New NumberMasterUtility
        Dim sOutkaEDINoL As String
        Dim iOutkaEDINoM As Integer
        Dim iRecNo As Integer

        Dim sOUTKA_DENP_NO As String
        Dim sOUTKA_DENP_DTL_NO As String
        Dim sINPUT_KBN As String
        Dim sAKADEN_KBN As String

        Dim recNoArr(dtSetDtl.Rows.Count) As String

        For i As Integer = 0 To iSetDtlMax
            recNoArr(i) = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item("REC_NO").ToString()

            Dim iraiSyubetsuCd As String = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item("COLUMN_11").ToString()
            If iraiSyubetsuCd <> "SY" Then
                ' 依頼種別コードが“出荷輸送”でない行は
                ' 入荷の対象外につきEDI受信データ設定対象外
                Continue For
            End If

            Dim detailKbn As String = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i).Item("COLUMN_190").ToString()
            If detailKbn <> "H" Then
                ' データID細目区分が“返品”でない行のみ設定する。

                '---------------------------------------------------------------------------
                ' セミEDI取込(共通)⇒EDI受信データ(出荷)セット
                '---------------------------------------------------------------------------
                ds = Me.SetSemiOutkaEdiRcv(ds, defCtlNo, dsKbnM036, i)
            End If

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒EDI受信データ(輸送)セット
            '---------------------------------------------------------------------------
            ds = Me.SetSemiUnsoEdiRcv(ds, defCtlNo, dsKbnM036, i)
        Next

        ' EDI受信データセット(出荷) の並べ替え
        tmpDt = dtEdiOutkaRcvDtl.Clone()
        tmpDr = dtEdiOutkaRcvDtl.Select("", "DATA_CRE_DATE, DATA_CRE_TIME, OUTKA_DENP_NO, OUTKA_DENP_DTL_NO")
        For Each row As DataRow In tmpDr
            tmpDt.ImportRow(row)
        Next
        Call dtEdiOutkaRcvDtl.Clear()
        Call dtEdiOutkaRcvDtl.Merge(tmpDt)

        ' EDI受信データセット(出荷)
        ' EDI_CTL_NO, EDI_CTL_NO_CHU 設定

        sOutkaEDINoL = ""
        iOutkaEDINoM = 0
        iRecNo = 0

        sOUTKA_DENP_NO = ""
        sOUTKA_DENP_DTL_NO = ""
        sINPUT_KBN = ""
        sAKADEN_KBN = ""

        For i = 0 To dtEdiOutkaRcvDtl.Rows.Count - 1
            drEdiOutkaRcvDtl = dtEdiOutkaRcvDtl.Rows(i)

            If Not sOUTKA_DENP_NO.Equals(drEdiOutkaRcvDtl.Item("OUTKA_DENP_NO").ToString.Trim) _
                Or Not sINPUT_KBN.Equals(drEdiOutkaRcvDtl.Item("INPUT_KBN").ToString.Trim) _
                Or Not sAKADEN_KBN.Equals(drEdiOutkaRcvDtl.Item("AKADEN_KBN").ToString.Trim) Then
                '出荷伝票No.ブレーク時
                '採番
                sOutkaEDINoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, drEdiOutkaRcvDtl.Item("NRS_BR_CD").ToString())

                iOutkaEDINoM = 0

                sOUTKA_DENP_NO = drEdiOutkaRcvDtl.Item("OUTKA_DENP_NO").ToString.Trim
                sINPUT_KBN = drEdiOutkaRcvDtl.Item("INPUT_KBN").ToString.Trim
                sAKADEN_KBN = drEdiOutkaRcvDtl.Item("AKADEN_KBN").ToString.Trim
            End If

            iOutkaEDINoM = iOutkaEDINoM + 1

            '管理番号のセット
            drEdiOutkaRcvDtl.Item("EDI_CTL_NO") = sOutkaEDINoL
            drEdiOutkaRcvDtl.Item("EDI_CTL_NO_CHU") = iOutkaEDINoM.ToString().PadLeft(3, CChar("0"))

            'ソート後のREC_NOを再設定
            iRecNo = iRecNo + 1
            drEdiOutkaRcvDtl.Item("REC_NO") = iRecNo.ToString().PadLeft(5, CChar("0"))
        Next

        ' EDI受信データセット(輸送) の並べ替え
        tmpDt = dtEdiUnsoRcvDtl.Clone()
        tmpDr = dtEdiUnsoRcvDtl.Select("", "DATA_CRE_DATE, DATA_CRE_TIME, OUTKA_DENP_NO, OUTKA_DENP_DTL_NO")
        For Each row As DataRow In tmpDr
            tmpDt.ImportRow(row)
        Next
        Call dtEdiUnsoRcvDtl.Clear()
        Call dtEdiUnsoRcvDtl.Merge(tmpDt)

        ' EDI受信データセット(輸送)
        ' EDI_CTL_NO, EDI_CTL_NO_CHU 設定

        sOutkaEDINoL = ""
        iOutkaEDINoM = 0
        iRecNo = 0

        sOUTKA_DENP_NO = ""
        sOUTKA_DENP_DTL_NO = ""
        sINPUT_KBN = ""
        sAKADEN_KBN = ""

        For i = 0 To dtEdiUnsoRcvDtl.Rows.Count - 1
            drEdiUnsoRcvDtl = dtEdiUnsoRcvDtl.Rows(i)

            If Not sOUTKA_DENP_NO.Equals(drEdiUnsoRcvDtl.Item("OUTKA_DENP_NO").ToString.Trim) _
                Or Not sINPUT_KBN.Equals(drEdiUnsoRcvDtl.Item("INPUT_KBN").ToString.Trim) _
                Or Not sAKADEN_KBN.Equals(drEdiUnsoRcvDtl.Item("AKADEN_KBN").ToString.Trim) Then
                '出荷伝票No.ブレーク時
                '採番
                sOutkaEDINoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, drEdiUnsoRcvDtl.Item("NRS_BR_CD").ToString())

                iOutkaEDINoM = 0

                sOUTKA_DENP_NO = drEdiUnsoRcvDtl.Item("OUTKA_DENP_NO").ToString.Trim
                sINPUT_KBN = drEdiUnsoRcvDtl.Item("INPUT_KBN").ToString.Trim
                sAKADEN_KBN = drEdiUnsoRcvDtl.Item("AKADEN_KBN").ToString.Trim
            End If

            iOutkaEDINoM = iOutkaEDINoM + 1

            '管理番号のセット
            drEdiUnsoRcvDtl.Item("EDI_CTL_NO") = sOutkaEDINoL
            drEdiUnsoRcvDtl.Item("EDI_CTL_NO_CHU") = iOutkaEDINoM.ToString().PadLeft(3, CChar("0"))

            'ソート後のREC_NOを再設定
            iRecNo = iRecNo + 1
            drEdiUnsoRcvDtl.Item("REC_NO") = iRecNo.ToString().PadLeft(5, CChar("0"))
        Next


        Dim setDs As DataSet
        Dim setDs2 As DataSet

        ' 商品マスタ存在チェック処理

        ' EDI受信データセット(出荷) は返品レコードを設定していないので、
        ' EDI受信データセット(輸送) のみでチェックを行う。
        setDs = New Jp.Co.Nrs.LM.DSL.LMH030DS()
        For i = 0 To dtEdiUnsoRcvDtl.Rows.Count - 1
            drEdiUnsoRcvDtl = dtEdiUnsoRcvDtl.Rows(i)
            setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Clear()
            setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").ImportRow(drEdiUnsoRcvDtl)

            setDs = MyBase.CallDAC(Me._Dac, "SelectMstGoods", setDs)
            If MyBase.GetResultCount = 0 Then
                ' 商品マスタに荷主商品コードが存在しない場合はエラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493",
                                           New String() {String.Concat("商品コード:", drEdiUnsoRcvDtl.Item("ITEM_RYAKUGO").ToString()), "商品マスタ", ""},
                                           recNoArr(Convert.ToInt32(drEdiUnsoRcvDtl.Item("GYO").ToString()) - 1),
                                           LMH030BLC.EXCEL_COLTITLE_SEMIEDI, drEdiUnsoRcvDtl.Item("FILE_NAME").ToString().Trim())
                bNoErr = False
                'ElseIf MyBase.GetResultCount > 1 Then
                '    ' 商品マスタに荷主商品コードが複数存在する場合はエラー
                '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E975",
                '                               New String() {String.Concat("商品コード:", drEdiUnsoRcvDtl.Item("ITEM_RYAKUGO").ToString())},
                '                               recNoDict(drEdiUnsoRcvDtl.Item("GYO").ToString()),
                '                               LMH030BLC.EXCEL_COLTITLE_SEMIEDI, drEdiUnsoRcvDtl.Item("FILE_NAME").ToString().Trim())
                '    bNoErr = False
            End If
        Next

        ' EDI出荷/輸送データ件数および出荷/輸送データL 出荷管理番号L 等 取得(チェック)処理
        Dim isFileNameError As Boolean = False
        Const LOOP_OUTKA As Integer = 1
        Const LOOP_UNSO As Integer = 2
        Dim rcvDtlMax As Integer = 0
        For loopCount = LOOP_OUTKA To LOOP_UNSO
            setDs = New Jp.Co.Nrs.LM.DSL.LMH030DS()
            If loopCount = LOOP_OUTKA Then
                rcvDtlMax = dtEdiOutkaRcvDtl.Rows.Count - 1
            Else
                rcvDtlMax = dtEdiUnsoRcvDtl.Rows.Count - 1
            End If
            For i = 0 To rcvDtlMax
                Dim recNo As String
                Dim fileName As String
                If loopCount = LOOP_OUTKA Then
                    setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Clear()
                    setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").ImportRow(dtEdiOutkaRcvDtl.Rows(i))
                    recNo = recNoArr(Convert.ToInt32(setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("GYO").ToString()) - 1)
                    fileName = setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("FILE_NAME").ToString()
                Else
                    setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Clear()
                    setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").ImportRow(dtEdiUnsoRcvDtl.Rows(i))
                    recNo = recNoArr(Convert.ToInt32(setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("GYO").ToString()) - 1)
                    fileName = setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("FILE_NAME").ToString()
                End If

                setDs = MyBase.CallDAC(Me._Dac, "SelectOutkaCntAndNoL", setDs)

                Dim ediRecCnt As Integer = MyBase.GetResultCount()
                Dim outkaNoL As String
                If loopCount = LOOP_OUTKA Then
                    outkaNoL = setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("OUTKA_CTL_NO").ToString()
                Else
                    outkaNoL = setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("OUTKA_CTL_NO").ToString()
                End If
                If ediRecCnt > 0 AndAlso (outkaNoL.Equals("") = False) Then
                    ' 出荷EDI存在かつ出荷存在（「出荷管理番号L IS NULL → ""」でない）の場合
                    ' 当該行はエラー
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E847", New String() {outkaNoL},
                                       recNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, fileName)
                    bNoErr = False
                End If

                Dim outkaEdiCnt As Integer = 0
                If isFileNameError = False Then
                    ' 新規追加 EDI受信データ(DTL) と同一キーレコードの出荷伝票No. と同明細No. 取得
                    ' 出荷伝票No. と同明細No. …… EDI出荷/輸送データ件数 等 取得時の突合条件項目
                    setDs2 = setDs.Copy()
                    setDs2 = MyBase.CallDAC(Me._Dac, "SelectOutkaEdiOutkaDenpNoAndDtlNo", setDs2)
                    outkaEdiCnt = MyBase.GetResultCount()
                    If outkaEdiCnt > 0 Then
                        Dim outkaDenpNoFile As String
                        Dim outkaDenpNoDb As String
                        Dim outkaDenpDtlNoFile As String
                        Dim outkaDenpDtlNoDb As String
                        If loopCount = LOOP_OUTKA Then
                            outkaDenpNoFile = setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("OUTKA_DENP_NO").ToString()
                            outkaDenpNoDb = setDs2.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("OUTKA_DENP_NO").ToString()
                            outkaDenpDtlNoFile = setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("OUTKA_DENP_DTL_NO").ToString()
                            outkaDenpDtlNoDb = setDs2.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows(0).Item("OUTKA_DENP_DTL_NO").ToString()
                        Else
                            outkaDenpNoFile = setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("OUTKA_DENP_NO").ToString()
                            outkaDenpNoDb = setDs2.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("OUTKA_DENP_NO").ToString()
                            outkaDenpDtlNoFile = setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("OUTKA_DENP_DTL_NO").ToString()
                            outkaDenpDtlNoDb = setDs2.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows(0).Item("OUTKA_DENP_DTL_NO").ToString()
                        End If
                        If Not (outkaDenpNoFile = outkaDenpNoDb AndAlso outkaDenpDtlNoFile = outkaDenpDtlNoDb) Then
                            ' EDI受信データ(DTL) と同一キーレコード存在、かつ
                            ' 出荷伝票No. 、同明細No. が不一致の場合
                            ' ファイル名はエラー
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E454",
                            New String() {"既に別のオーダーが登録されているファイル名", "処理", "ファイル名を変更して再度取込を行ってください。"},
                            recNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, fileName)
                            bNoErr = False
                            ' ファイル名エラーメッセージは検出初回のみの出力とする
                            isFileNameError = True
                        End If
                    End If
                End If

                If bNoErr Then
                    ' EDI出荷データ件数 > 0 の場合、EDI出荷データの削除更新を行う
                    If ediRecCnt > 0 Then
                        ' EDI出荷(大)の削除(論理削除)
                        setDs = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiL", setDs)
                        iOutHedCanCnt += MyBase.GetResultCount()

                        ' EDI出荷(中)の削除(論理削除)
                        setDs = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiM", setDs)
                        iOutDtlCanCnt += MyBase.GetResultCount()

                        ' EDI 出荷/輸送 受信テーブル(明細) の削除(論理削除)
                        ' 一度処理したファイル(サフィクスが付いて別ファイル名となる)の再取込みで、
                        ' 後続の物理削除処理の対象とならないことによる、中間テーブルのレコード消え残り回避処理
                        setDs = MyBase.CallDAC(Me._Dac, "UpdateDelOutkaEdiDtl", setDs)
                        iRcvDtlCanCnt += MyBase.GetResultCount()
                    End If

                    If outkaEdiCnt > 0 Then
                        ' 新規追加 EDI受信データ(DTL) と同一キーのレコード件数 > 0 の場合
                        ' 同一キーレコード存在時の物理削除
                        setDs = MyBase.CallDAC(Me._Dac, "DeleteOutkaEdi", setDs)
                        iRcvDtlCanCnt = iRcvDtlCanCnt + 1
                    End If
                End If
            Next
        Next

        If bNoErr Then
            'エラー無し

            setDs = New Jp.Co.Nrs.LM.DSL.LMH030DS()
            setDs.Tables("LMH030_SEMIEDI_INFO").Merge(ds.Tables("LMH030_SEMIEDI_INFO"))
            setDs.Tables("LMH030_Z_KBN_OUT").Merge(dsKbnD003.Tables("LMH030_Z_KBN_OUT"))
            setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Merge(dtEdiOutkaRcvDtl)

            ' EDI受信データ(出荷)(DTL) の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt += setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows.Count()

            ' 取消対象のデータの H_OUTKAEDI_DTL_NCGO_NEW からの読み込み
            setDs = MyBase.CallDAC(Me._Dac, "SelectOutkaediDtlNcgoNewCancel", setDs)
            If MyBase.GetResultCount > 0 Then
                ' H_OUTKAEDI_DTL_NCGO_NEW 更新 (出荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediDtlNcgoNewCancel", setDs)
                iRcvDtlCanCnt += MyBase.GetResultCount()

                ' H_OUTKAEDI_L 更新 (出荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediL_Cancel", setDs)
                iOutHedCanCnt += MyBase.GetResultCount()

                ' H_OUTKAEDI_M 更新 (出荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediM_Cancel", setDs)
                iOutDtlCanCnt += MyBase.GetResultCount()
            End If

            setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Clear()
            setDs.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Merge(dtEdiOutkaRcvDtl)

            ' H_UNSOEDI_DTL_NCGO の EDI_CTR_NO 更新 (今回取り込んだ H_OUTKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)
            ' 対象: 今回取り込んだ H_OUTKAEDI_DTL_NCGO_NEW の OUTKA_DENP_NO, OUTKA_DENP_DTL_NO と一致する H_UNSOEDI_DTL_NCGO
            setDs = MyBase.CallDAC(Me._Dac, "UpdateUnsoediDtlNcgo_EdiCtlNo_FromOutkaEdidtlNcgoNew1", setDs)

            ' H_OUTKAEDI_DTL_NCGO_NEW 取得 (EDI入荷(大) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForOutkaediL_FromOutkaediDtlNcgoNew", setDs)

            ' H_OUTKAEDI_DTL_NCGO_NEW 取得 (EDI入荷(中) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForOutkaediM_FromOutkaediDtlNcgoNew", setDs)

            If setDs.Tables("LMH030_OUTKAEDI_L").Rows.Count() > 0 Then
                ' EDI出荷データ(大)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                iOutHedInsCnt += setDs.Tables("LMH030_OUTKAEDI_L").Rows.Count()
            End If

            If setDs.Tables("LMH030_OUTKAEDI_M").Rows.Count() > 0 Then
                ' EDI出荷データ(中)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt += setDs.Tables("LMH030_OUTKAEDI_M").Rows.Count()
            End If


            setDs = New Jp.Co.Nrs.LM.DSL.LMH030DS()
            setDs.Tables("LMH030_SEMIEDI_INFO").Merge(ds.Tables("LMH030_SEMIEDI_INFO"))
            setDs.Tables("LMH030_Z_KBN_OUT").Merge(dsKbnD003.Tables("LMH030_Z_KBN_OUT"))
            setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Merge(dtEdiUnsoRcvDtl)

            ' EDI受信データ(輸送)(DTL) の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertUnsoEdiRcvDtl", setDs)
            iRcvDtlInsCnt += setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows.Count()

            ' 取消対象のデータの H_UNSOEDI_DTL_NCGO からの読み込み
            setDs = MyBase.CallDAC(Me._Dac, "SelectUnsoediDtlNcgoCancel", setDs)
            If MyBase.GetResultCount > 0 Then
                ' H_UNSOEDI_DTL_NCGO 更新 (輸送赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateUnsoediDtlNcgoCancel", setDs)
                iRcvDtlCanCnt += MyBase.GetResultCount()

                ' H_OUTKAEDI_L 更新 (輸送赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediL_UnsoCancel", setDs)
                iOutHedCanCnt += MyBase.GetResultCount()

                ' H_OUTKAEDI_M 更新 (輸送赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediM_UnsoCancel", setDs)
                iOutDtlCanCnt += MyBase.GetResultCount()
            End If

            setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Clear()
            setDs.Tables("LMH030_UNSOEDI_DTL_NCGO").Merge(dtEdiUnsoRcvDtl)

            '下記処理(EDI出荷データ(大)(中)登録)は輸送のみ依頼（輸送会社＝日陸 & 保管場所≠日陸）データのみ処理する(輸送のみ依頼時)

            ' H_UNSOEDI_DTL_NCGO 取得 (EDI入荷(大) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForOutkaediL_FromUnsoediDtlNcgo", setDs)

            ' H_UNSOEDI_DTL_NCGO 取得 (EDI入荷(中) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForOutkaediM_FromUnsoediDtlNcgo", setDs)

            If setDs.Tables("LMH030_OUTKAEDI_L").Rows.Count() > 0 Then
                ' EDI出荷データ(大)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiL", setDs)
                iOutHedInsCnt += setDs.Tables("LMH030_OUTKAEDI_L").Rows.Count()
            End If

            If setDs.Tables("LMH030_OUTKAEDI_M").Rows.Count() > 0 Then
                ' EDI出荷データ(中)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiM", setDs)
                iOutDtlInsCnt += setDs.Tables("LMH030_OUTKAEDI_M").Rows.Count()
            End If

            ' 今回取り込んだ H_UNSOEDI_DTL_NCGO の EDI_CTR_NO 更新 (過去に取り込んだ H_OUTKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)
            ' 対象: 今回取り込んだ H_UNSOEDI_DTL_NCGO
            ' 設定値: 今回取り込んだ H_UNSOEDI_DTL_NCGO の OUTKA_DENP_NO, OUTKA_DENP_DTL_NO と一致する H_OUTKAEDI_DTL_NCGO_NEW の EDI_CTR_NO
            setDs = MyBase.CallDAC(Me._Dac, "UpdateUnsoediDtlNcgo_EdiCtlNo_FromOutkaEdidtlNcgoNew2", setDs)

            ' 今回取り込んだ H_UNSOEDI_DTL_NCGO の EDI_CTR_NO 更新 (過去に取り込んだ H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)
            ' 対象: 今回取り込んだ H_UNSOEDI_DTL_NCGO(細目区分 = 'H'[返品] のみ)
            ' 設定値: 今回取り込んだ H_UNSOEDI_DTL_NCGO の OUTKA_DENP_NO, OUTKA_DENP_DTL_NO と一致する H_INKAEDI_DTL_NCGO_NEW の EDI_CTR_NO
            setDs = MyBase.CallDAC(Me._Dac, "UpdateUnsoediDtlNcgo_EdiCtlNo_FromInkaEdidtlNcgoNew", setDs)

            ' 今回取り込んだ H_UNSOEDI_DTL_NCGO の EDI_CTR_NO に一致する H_OUTKAEDI_L の更新
            ' SHIP_CD_L, SHIP_NM_L, DEST_MAIL, UNSO_ATT を H_OUTKAEDI_DTL_NCGO_NEW よりの新規登録時と同様の編集条件で設定する。
            setDs = MyBase.CallDAC(Me._Dac, "UpdateOutkaediL_Unso", setDs)

        End If

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function

#End Region ' "画面取込(セミEDI)データセット＋更新処理"

#Region "管理番号採番前初期値取得"

    ''' <summary>
    ''' 管理番号採番前初期値取得
    ''' </summary>
    ''' <param name="dsWork"></param>
    ''' <param name="nrsBrCd"></param>
    ''' <returns></returns>
    Private Function GetDefCtlNo(ByVal dsWork As DataSet, ByVal nrsBrCd As String) As String

        Call GetKbn(dsWork, nrsBrCd, "D003")

        Dim drKbnD003 As DataRow() = dsWork.Tables("LMH030_Z_KBN_OUT").Select("KBN_NM1 = '" & nrsBrCd & "'")

        Dim kigo As String = " "
        If drKbnD003.Length > 0 Then
            kigo = drKbnD003(0).Item("KBN_NM6").ToString()
        Else
            Dim drZ_KbnOut As DataRow = dsWork.Tables("LMH030_Z_KBN_OUT").NewRow()
            drZ_KbnOut.Item("KBN_GROUP_CD") = "D003"
            drZ_KbnOut.Item("KBN_CD") = ""
            drZ_KbnOut.Item("KBN_NM1") = nrsBrCd
            drZ_KbnOut.Item("KBN_NM6") = ""
            dsWork.Tables("LMH030_Z_KBN_OUT").Rows.Add(drZ_KbnOut)
        End If

        Dim ret As String = String.Concat(kigo, New String("0"c, 8))

        Return ret

    End Function

#End Region ' "管理番号採番前初期値取得"

#Region "MCLC輸送会社名取得"

    ''' <summary>
    ''' MCLC輸送会社名取得
    ''' </summary>
    ''' <param name="dsWork"></param>
    ''' <param name="nrsBrCd"></param>
    Private Sub GetYusoCompNm(ByVal dsWork As DataSet, ByVal nrsBrCd As String)

        Call GetKbn(dsWork, nrsBrCd, "M036")

    End Sub

#End Region ' "MCLC輸送会社名取得"

#Region "MCLC輸送会社名取得"

    ''' <summary>
    ''' MCLC輸送会社名取得
    ''' </summary>
    ''' <param name="dsWork"></param>
    ''' <param name="nrsBrCd"></param>
    Private Sub GetKbn(ByVal dsWork As DataSet, ByVal nrsBrCd As String, ByVal kbnGroupCd As String)

        If dsWork.Tables("LMH030_Z_KBN_OUT").Rows.Count = 0 Then
            dsWork.Tables("LMH030_OUTKAEDI_L").Clear()
            Dim drOutEdiL As DataRow = dsWork.Tables("LMH030_OUTKAEDI_L").NewRow()
            drOutEdiL.Item("NRS_BR_CD") = nrsBrCd
            dsWork.Tables("LMH030_OUTKAEDI_L").Rows.Add(drOutEdiL)

            dsWork.Tables("LMH030_Z_KBN_IN").Clear()
            Dim drZKbnIn As DataRow = dsWork.Tables("LMH030_Z_KBN_IN").NewRow()
            drZKbnIn.Item("KBN_GROUP_CD") = kbnGroupCd
            dsWork.Tables("LMH030_Z_KBN_IN").Rows.Add(drZKbnIn)

            Call MyBase.CallDAC(Me._DacMst, "SelectZKbnHanyo", dsWork)
        End If

    End Sub

#End Region ' "MCLC輸送会社名取得"

#Region "データセット設定 EDI出荷受信テーブル(出荷)(明細)"

    ''' <summary>
    ''' データセット設定 EDI出荷受信テーブル(出荷)(明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="defCtlNo"></param>
    ''' <param name="dsKbnM036"></param>
    ''' <param name="i"></param>
    ''' <returns></returns>
    Private Function SetSemiOutkaEdiRcv(ByVal ds As DataSet, ByVal defCtlNo As String, ByVal dsKbnM036 As DataSet, ByVal i As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drSetHead As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_HED").Rows(0)
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").NewRow()

        ' EDI受信DTL設定
        If drSetDtl.Item("COLUMN_5").ToString().Equals("2") Then
            '赤
            drEdiRcvDtl.Item("DEL_KB") = "3"
        Else
            drEdiRcvDtl.Item("DEL_KB") = "0"
        End If
        drEdiRcvDtl.Item("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl.Item("FILE_NAME") = drSetDtl.Item("FILE_NAME_RCV")
        drEdiRcvDtl.Item("REC_NO") = "00000"                                                                ' ソート後に設定する
        drEdiRcvDtl.Item("GYO") = (i + 1).ToString().PadLeft(5, "0"c)
        drEdiRcvDtl.Item("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl.Item("EDI_CTL_NO") = String.Empty                                                       ' ソート後に設定する
        drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = String.Empty                                                   ' ソート後に設定する
        drEdiRcvDtl.Item("OUTKA_CTL_NO") = defCtlNo
        drEdiRcvDtl.Item("OUTKA_CTL_NO_CHU") = "000"
        drEdiRcvDtl.Item("CUST_CD_L") = drSemiEdiInfo("CUST_CD_L")                                          ' 荷主コード（大）
        drEdiRcvDtl.Item("CUST_CD_M") = drSemiEdiInfo("CUST_CD_M")                                          ' 荷主コード（中）
        drEdiRcvDtl.Item("PRTFLG") = "0"                                                                    ' プリントフラグ

        ' データIDエリア
        drEdiRcvDtl.Item("DATA_ID_AREA") = "201"
        ' データID細目区分
        Dim detailKbn As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_190").ToString().Trim(), 1)
        drEdiRcvDtl.Item("DATA_ID_DETAIL") = detailKbn
        If drSetDtl.Item("COLUMN_4").ToString().Equals("1") AndAlso drSetDtl.Item("COLUMN_5").ToString().Equals("1") Then
            ' 受注ヘッダ訂正No. = 1 かつ赤黒区分“黒”の場合
            ' 訂正区分“新規”
            drEdiRcvDtl.Item("INPUT_KBN") = "1"
        Else
            ' 訂正区分“訂正”
            drEdiRcvDtl.Item("INPUT_KBN") = "2"
        End If
        If drSetDtl.Item("COLUMN_5").ToString().Equals("1") Then
            ' 赤伝区分“黒”
            drEdiRcvDtl.Item("AKADEN_KBN") = ""
        Else
            ' 赤伝区分“赤”
            drEdiRcvDtl.Item("AKADEN_KBN") = "1"
        End If
        ' データ作成日
        drEdiRcvDtl.Item("DATA_CRE_DATE") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_290").ToString(), 8)
        ' データ作成時刻
        drEdiRcvDtl.Item("DATA_CRE_TIME") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_291").ToString(), 6)
        ' 伝票タイプ
        drEdiRcvDtl.Item("DENPYO_TYPE") = ""

        Dim custOrderNo As String = drSetDtl.Item("COLUMN_6").ToString()
        If detailKbn = "A" OrElse detailKbn = "H" Then
            ' 受注伝票NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 受注伝票明細NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_280").ToString(), 6)
            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = New String("0"c, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = New String("0"c, 5)
        ElseIf detailKbn = "F" OrElse detailKbn = "I" Then
            ' 受注伝票NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_NO") = New String("0"c, 10)
            ' 受注伝票明細NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_DTL_NO") = New String("0"c, 6)
            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_280").ToString(), 5)
        End If
        ' 出荷伝票No.
        drEdiRcvDtl.Item("OUTKA_DENP_NO") = custOrderNo.Substring(10, 10)
        ' 出荷伝票明細No.
        drEdiRcvDtl.Item("OUTKA_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_278").ToString(), 6)
        ' 出荷年月日
        drEdiRcvDtl.Item("SYUKKA_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Me._Blc.LeftB(drSetDtl.Item("COLUMN_14").ToString().Trim(), 10))
        ' 納期年月日
        drEdiRcvDtl.Item("NOUKI_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Me._Blc.LeftB(drSetDtl.Item("COLUMN_15").ToString().Trim(), 10))
        ' 品目コード
        drEdiRcvDtl.Item("ITEM_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_210").ToString(), 30)
        ' 品目略号
        drEdiRcvDtl.Item("ITEM_RYAKUGO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_211").ToString(), 20)
        ' 品目愛称
        drEdiRcvDtl.Item("ITEM_AISYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_209").ToString(), 40)
        ' 品目グループ
        drEdiRcvDtl.Item("ITEM_GROUP") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_206").ToString(), 2)
        ' グレード1
        drEdiRcvDtl.Item("GRADE1") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_212").ToString(), 10)
        ' グレード2
        drEdiRcvDtl.Item("GRADE2") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_213").ToString(), 10)
        ' 個別荷姿コード
        drEdiRcvDtl.Item("KOBETSU_NISUGATA_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_215").ToString(), 2)
        ' 容量
        drEdiRcvDtl.Item("YOURYOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_214").ToString(), 8)
        ' 製造ロット
        drEdiRcvDtl.Item("SEIZO_LOT") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_226").ToString(), 10)
        ' 個数
        drEdiRcvDtl.Item("KOSU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_228").ToString(), 16)
        If drEdiRcvDtl.Item("KOSU").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl.Item("KOSU") = drEdiRcvDtl.Item("KOSU").ToString().Substring(1)
            drEdiRcvDtl.Item("KOSU_FUGO") = "-"
        Else
            drEdiRcvDtl.Item("KOSU_FUGO") = "+"
        End If
        ' 数量
        drEdiRcvDtl.Item("SUURYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_230").ToString(), 16)
        If drEdiRcvDtl.Item("SUURYO").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl.Item("SUURYO") = drEdiRcvDtl.Item("SUURYO").ToString().Substring(1)
            drEdiRcvDtl.Item("SUURYO_FUGO") = "-"
        Else
            drEdiRcvDtl.Item("SUURYO_FUGO") = "+"
        End If
        ' 保管場所
        drEdiRcvDtl.Item("HOKAN_BASYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_201").ToString(), 4)
        ' 出荷先コード
        drEdiRcvDtl.Item("SYUKKASAKI_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_19").ToString(), 12)
        Dim syukkasakiNm As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_20").ToString(), 120)
        ' 出荷先名称１
        drEdiRcvDtl.Item("SYUKKASAKI_NM1") = Me._Blc.LeftB(syukkasakiNm, 40)
        ' 出荷先名称２
        drEdiRcvDtl.Item("SYUKKASAKI_NM2") = Me._Blc.MidB(syukkasakiNm, 41, 40)
        ' 出荷先名称３
        drEdiRcvDtl.Item("SYUKKASAKI_NM3") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_21").ToString(), 40)
        ' 出荷先名称４
        drEdiRcvDtl.Item("SYUKKASAKI_NM4") = ""
        Dim syukkasakiAdd As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_24").ToString(), 80)
        ' 出荷先住所１
        drEdiRcvDtl.Item("SYUKKASAKI_ADD1") = Me._Blc.LeftB(syukkasakiAdd, 40)
        ' 出荷先住所２
        drEdiRcvDtl.Item("SYUKKASAKI_ADD2") = Me._Blc.MidB(syukkasakiAdd, 41, 40)
        ' 出荷先住所３
        drEdiRcvDtl.Item("SYUKKASAKI_ADD3") = ""
        ' 出荷先ＴＥＬ
        drEdiRcvDtl.Item("SYUKKASAKI_TEL") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_25").ToString(), 30)
        ' 郵便番号
        drEdiRcvDtl.Item("ZIP") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_23").ToString(), 10)
        ' 便区分
        drEdiRcvDtl.Item("BIN_KBN") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_42").ToString(), 2)
        ' 便区分名称
        drEdiRcvDtl.Item("BIN_KBN_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_43").ToString(), 30)
        ' 輸送会社コード
        drEdiRcvDtl.Item("YUSO_COMP_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_44").ToString(), 2)
        ' 輸送会社名称
        Dim drKbnM036 As DataRow() = dsKbnM036.Tables("LMH030_Z_KBN_OUT").Select("KBN_CD = '" & drEdiRcvDtl.Item("YUSO_COMP_CD").ToString() & "'")
        If drKbnM036.Length() = 0 Then
            drEdiRcvDtl.Item("YUSO_COMP_NM") = ""
        Else
            drEdiRcvDtl.Item("YUSO_COMP_NM") = drKbnM036(0).Item("KBN_NM1")
        End If
        ' 先方注文No.
        drEdiRcvDtl.Item("SENPO_ORDER_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_101").ToString(), 40)
        ' 納入条件備考
        drEdiRcvDtl.Item("NOUNYU_JYOUKEN_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_62").ToString(), 100)
        ' 成績書発行名称
        drEdiRcvDtl.Item("SEISEKISYO_HAKKOU_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_53").ToString(), 30)
        ' 内備考
        drEdiRcvDtl.Item("UCHI_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_55").ToString(), 200)
        ' 外備考
        drEdiRcvDtl.Item("SOTO_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_56").ToString(), 100)
        ' 荷姿名称
        drEdiRcvDtl.Item("NISUGATA_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_216").ToString(), 10)
        ' 基本数量単位
        drEdiRcvDtl.Item("KIHON_SURYO_TANI") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_307").ToString(), 3)

        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                  ' 実績処理フラグ

        drEdiRcvDtl("SYS_ENT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_ENT_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_ENT_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_ENT_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_UPD_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_UPD_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_UPD_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_UPD_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH030_OUTKAEDI_DTL_NCGO_NEW").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region ' "データセット設定 EDI出荷受信テーブル(出荷)(明細)"

#Region "データセット設定 EDI出荷受信テーブル(輸送)(明細)"

    ''' <summary>
    '''  データセット設定 EDI出荷受信テーブル(輸送)(明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="defCtlNo"></param>
    ''' <param name="dsKbnM036"></param>
    ''' <param name="i"></param>
    ''' <returns></returns>
    Private Function SetSemiUnsoEdiRcv(ByVal ds As DataSet, ByVal defCtlNo As String, ByVal dsKbnM036 As DataSet, ByVal i As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)
        Dim drSetHead As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_HED").Rows(0)
        Dim drSetDtl As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH030_UNSOEDI_DTL_NCGO").NewRow()

        ' EDI受信DTL設定
        If drSetDtl.Item("COLUMN_5").ToString().Equals("2") Then
            '赤
            drEdiRcvDtl.Item("DEL_KB") = "3"
        Else
            drEdiRcvDtl.Item("DEL_KB") = "0"
        End If
        drEdiRcvDtl.Item("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl.Item("FILE_NAME") = drSetDtl.Item("FILE_NAME_RCV")
        drEdiRcvDtl.Item("REC_NO") = "00000"                                                                ' ソート後に設定する
        drEdiRcvDtl.Item("GYO") = (i + 1).ToString().PadLeft(5, "0"c)
        drEdiRcvDtl.Item("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl.Item("EDI_CTL_NO") = String.Empty                                                       ' ソート後に設定する
        drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = String.Empty                                                   ' ソート後に設定する
        drEdiRcvDtl.Item("OUTKA_CTL_NO") = defCtlNo
        drEdiRcvDtl.Item("OUTKA_CTL_NO_CHU") = "000"
        drEdiRcvDtl.Item("CUST_CD_L") = drSemiEdiInfo("CUST_CD_L")                                          ' 荷主コード（大）
        drEdiRcvDtl.Item("CUST_CD_M") = drSemiEdiInfo("CUST_CD_M")                                          ' 荷主コード（中）

        ' データIDエリア
        drEdiRcvDtl.Item("DATA_ID_AREA") = "101"
        ' データID細目区分
        Dim detailKbn As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_190").ToString().Trim(), 1)
        drEdiRcvDtl.Item("DATA_ID_DETAIL") = detailKbn
        If drSetDtl.Item("COLUMN_4").ToString().Equals("1") AndAlso drSetDtl.Item("COLUMN_5").ToString().Equals("1") Then
            ' 受注ヘッダ訂正No. = 1 かつ赤黒区分“黒”の場合
            ' 訂正区分“新規”
            drEdiRcvDtl.Item("INPUT_KBN") = "1"
        Else
            ' 訂正区分“訂正”
            drEdiRcvDtl.Item("INPUT_KBN") = "2"
        End If
        If drSetDtl.Item("COLUMN_5").ToString().Equals("1") Then
            ' 赤伝区分“黒”
            drEdiRcvDtl.Item("AKADEN_KBN") = ""
        Else
            ' 赤伝区分“赤”
            drEdiRcvDtl.Item("AKADEN_KBN") = "1"
        End If
        ' データ作成日
        drEdiRcvDtl.Item("DATA_CRE_DATE") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_290").ToString(), 8)
        ' データ作成時刻
        drEdiRcvDtl.Item("DATA_CRE_TIME") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_291").ToString(), 6)
        ' 伝票タイプ
        drEdiRcvDtl.Item("DENPYO_TYPE") = ""

        Dim custOrderNo As String = drSetDtl.Item("COLUMN_6").ToString()
        If detailKbn = "A" OrElse detailKbn = "H" Then
            ' 受注伝票NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 受注伝票明細NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_280").ToString(), 6)
            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = New String("0"c, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = New String("0"c, 5)
        ElseIf detailKbn = "F" OrElse detailKbn = "I" Then
            ' 受注伝票NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_NO") = New String("0"c, 10)
            ' 受注伝票明細NO.
            drEdiRcvDtl.Item("JYUCHU_DENP_DTL_NO") = New String("0"c, 6)
            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_280").ToString(), 5)
        End If
        ' 出荷伝票No.
        drEdiRcvDtl.Item("OUTKA_DENP_NO") = custOrderNo.Substring(10, 10)
        ' 出荷伝票明細No.
        drEdiRcvDtl.Item("OUTKA_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_278").ToString(), 6)
        ' 納期年月日
        drEdiRcvDtl.Item("NOUKI_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Me._Blc.LeftB(drSetDtl.Item("COLUMN_15").ToString().Trim(), 10))
        ' 品目コード
        drEdiRcvDtl.Item("ITEM_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_210").ToString(), 30)
        ' 品目略号
        drEdiRcvDtl.Item("ITEM_RYAKUGO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_211").ToString(), 20)
        ' 品目愛称
        drEdiRcvDtl.Item("ITEM_AISYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_209").ToString(), 40)
        ' 品目グループ
        drEdiRcvDtl.Item("ITEM_GROUP") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_206").ToString(), 2)
        ' グレード1
        drEdiRcvDtl.Item("GRADE1") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_212").ToString(), 10)
        ' グレード2
        drEdiRcvDtl.Item("GRADE2") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_213").ToString(), 10)
        ' 個別荷姿コード
        drEdiRcvDtl.Item("KOBETSU_NISUGATA_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_215").ToString(), 2)
        ' 容量
        drEdiRcvDtl.Item("YOURYOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_214").ToString(), 8)
        ' 製造ロット
        drEdiRcvDtl.Item("SEIZO_LOT") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_226").ToString(), 10)
        ' 個数
        drEdiRcvDtl.Item("KOSU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_228").ToString(), 16)
        If drEdiRcvDtl.Item("KOSU").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl.Item("KOSU") = drEdiRcvDtl.Item("KOSU").ToString().Substring(1)
            drEdiRcvDtl.Item("KOSU_FUGO") = "-"
        Else
            drEdiRcvDtl.Item("KOSU_FUGO") = "+"
        End If
        ' 数量
        drEdiRcvDtl.Item("SUURYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_230").ToString(), 16)
        If drEdiRcvDtl.Item("SUURYO").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl.Item("SUURYO") = drEdiRcvDtl.Item("SUURYO").ToString().Substring(1)
            drEdiRcvDtl.Item("SUURYO_FUGO") = "-"
        Else
            drEdiRcvDtl.Item("SUURYO_FUGO") = "+"
        End If
        ' 保管場所
        drEdiRcvDtl.Item("HOKAN_BASYO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_201").ToString(), 4)
        ' 出荷先コード
        drEdiRcvDtl.Item("SYUKKASAKI_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_19").ToString(), 12)
        Dim syukkasakiNm As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_20").ToString(), 120)
        ' 出荷先名称１
        drEdiRcvDtl.Item("SYUKKASAKI_NM1") = Me._Blc.LeftB(syukkasakiNm, 40)
        ' 出荷先名称２
        drEdiRcvDtl.Item("SYUKKASAKI_NM2") = Me._Blc.MidB(syukkasakiNm, 41, 40)
        ' 出荷先名称３
        drEdiRcvDtl.Item("SYUKKASAKI_NM3") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_21").ToString(), 40)
        ' 出荷先名称４
        drEdiRcvDtl.Item("SYUKKASAKI_NM4") = ""
        Dim syukkasakiAdd As String = Me._Blc.LeftB(drSetDtl.Item("COLUMN_24").ToString(), 80)
        ' 出荷先住所１
        drEdiRcvDtl.Item("SYUKKASAKI_ADD1") = Me._Blc.LeftB(syukkasakiAdd, 40)
        ' 出荷先住所２
        drEdiRcvDtl.Item("SYUKKASAKI_ADD2") = Me._Blc.MidB(syukkasakiAdd, 41, 40)
        ' 出荷先住所３
        drEdiRcvDtl.Item("SYUKKASAKI_ADD3") = ""
        ' 出荷先ＴＥＬ
        drEdiRcvDtl.Item("SYUKKASAKI_TEL") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_25").ToString(), 30)
        ' 郵便番号
        drEdiRcvDtl.Item("ZIP") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_23").ToString(), 10)
        ' 便区分名称
        drEdiRcvDtl.Item("BIN_KBN_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_43").ToString(), 30)
        ' 輸送会社コード
        drEdiRcvDtl.Item("YUSO_COMP_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_44").ToString(), 2)
        ' 輸送会社名称
        Dim drKbnM036 As DataRow() = dsKbnM036.Tables("LMH030_Z_KBN_OUT").Select("KBN_CD = '" & drEdiRcvDtl.Item("YUSO_COMP_CD").ToString() & "'")
        If drKbnM036.Length() = 0 Then
            drEdiRcvDtl.Item("YUSO_COMP_NM") = ""
        Else
            drEdiRcvDtl.Item("YUSO_COMP_NM") = drKbnM036(0).Item("KBN_NM1")
        End If
        ' 支払人コード
        drEdiRcvDtl.Item("SHIHARAININ_CD") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_118").ToString(), 12)
        ' 支払人名称1
        drEdiRcvDtl.Item("SHIHARAININ_NM1") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_119").ToString(), 40)
        ' 支払人名称2
        drEdiRcvDtl.Item("SHIHARAININ_NM2") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_311").ToString(), 40)
        ' 受注先名称1
        drEdiRcvDtl.Item("JYUCHUSAKI_NM1") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_222").ToString(), 40)
        ' 先方注文No.
        drEdiRcvDtl.Item("SENPO_ORDER_NO") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_101").ToString(), 40)
        ' 納入時刻名称
        drEdiRcvDtl.Item("NOUNYU_JIKOKU_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_17").ToString(), 24)
        ' 指定伝票名称
        drEdiRcvDtl.Item("SHITEI_DENP_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_103").ToString(), 20)
        ' 成績書発行名称
        drEdiRcvDtl.Item("SEISEKISYO_HAKKOU_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_53").ToString(), 30)
        ' 納入時条件コード名称1
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM1") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_67").ToString(), 20)
        ' 納入時条件コード名称2
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM2") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_69").ToString(), 20)
        ' 納入時条件コード名称3
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM3") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_71").ToString(), 20)
        ' 納入時条件コード名称4
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM4") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_73").ToString(), 20)
        ' 納入時条件コード名称5
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM5") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_75").ToString(), 20)
        ' 納入時条件コード名称6
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM6") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_77").ToString(), 20)
        ' 納入時条件コード名称7
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM7") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_79").ToString(), 20)
        ' 納入時条件コード名称8
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM8") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_81").ToString(), 20)
        ' 納入時条件コード名称9
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM9") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_83").ToString(), 20)
        ' 納入時条件コード名称10
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_NM10") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_85").ToString(), 20)
        ' 納入条件備考
        drEdiRcvDtl.Item("NOUNYUJI_JYOUKEN_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_62").ToString(), 100)
        ' 成績書発行名称
        drEdiRcvDtl.Item("SEISEKISYO_HAKKOU_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_53").ToString(), 30)
        ' 内備考
        drEdiRcvDtl.Item("UCHI_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_55").ToString(), 200)
        ' 外備考
        drEdiRcvDtl.Item("SOTO_BIKOU") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_56").ToString(), 100)
        ' 荷姿名称
        drEdiRcvDtl.Item("NISUGATA_NM") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_216").ToString(), 10)
        ' 送状非表示区分
        drEdiRcvDtl.Item("OKURIJYO_HIHYOJI_KBN") = Me._Blc.LeftB(String.Concat(
            drSetDtl.Item("COLUMN_294").ToString(), drSetDtl.Item("COLUMN_295").ToString(),
            drSetDtl.Item("COLUMN_296").ToString(), drSetDtl.Item("COLUMN_297").ToString(),
            drSetDtl.Item("COLUMN_298").ToString(), drSetDtl.Item("COLUMN_299").ToString(),
            drSetDtl.Item("COLUMN_300").ToString(), drSetDtl.Item("COLUMN_301").ToString()), 10)
        ' 基本数量単位
        drEdiRcvDtl.Item("KIHON_SURYO_TANI") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_307").ToString(), 3)
        ' 支払人名称1（長）
        drEdiRcvDtl.Item("SHIHARAININ_NM1L") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_313").ToString(), 80)
        ' 受注先名称1（長）
        drEdiRcvDtl.Item("JYUCHUSAKI_NM1L") = Me._Blc.LeftB(drSetDtl.Item("COLUMN_314").ToString(), 80)

        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                  ' 実績処理フラグ

        drEdiRcvDtl("SYS_ENT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_ENT_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_ENT_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_ENT_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_UPD_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_UPD_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_UPD_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_UPD_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH030_UNSOEDI_DTL_NCGO").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region ' "データセット設定 EDI出荷受信テーブル(輸送)(明細)"

#End Region ' "画面取込(セミEDI)データセット＋更新処理 および関連処理"

#End Region ' "セミEDI処理"

#End Region

End Class
