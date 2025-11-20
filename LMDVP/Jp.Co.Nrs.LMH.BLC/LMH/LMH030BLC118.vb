' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  118　　　 : サクラファインテック(千葉⇒土気)
'  作  成  者       :  daikoku
' 備考             :　401(横浜)を丸ごとコピー
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC118
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC118 = New LMH030DAC118()

    ''' <summary>
    ''' 使用するDACクラスの生成(共通DAC)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacCom As LMH030DAC = New LMH030DAC()

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

        Dim sakuraMatomeF As String = String.Empty
        Dim autoMatomeF As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim matomeNo As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI出荷(大)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            Return ds
        End If

        ds = Me.SetEdiLShoki(ds)

        'EDI出荷(中)の初期値設定
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E011")
            Return ds
        End If

        'EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then

            Return ds
        End If

        '自動まとめチェック
        autoMatomeF = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()

        If autoMatomeF.Equals("9") = False AndAlso String.IsNullOrEmpty(autoMatomeF) = False Then

            'サクラ専用 まとめデータチェック
            ds = MyBase.CallDAC(Me._Dac, "SelectMatomeCheck", ds)

            'まとめ対象データチェック
            If MyBase.GetResultCount = 0 Then

            Else
                'まとめ先データチェック
                ds = MyBase.CallDAC(Me._Dac, "SelectMatomeTarget", ds)

                If MyBase.GetResultCount = 0 Then

                ElseIf MyBase.GetResultCount > 1 Then
                    matomeNo = Me.matomesakiOutkaNo(ds)

                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E427", New String() {matomeNo}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds
                ElseIf autoMatomeF.Equals("0") = True Then

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.SAKURA_WID_L001, 0)

                    If String.IsNullOrEmpty(choiceKb) = True Then
                        'ワーニング画面(LMH070)で処理
                        '!!!TO-DO 後にEXCEL出力に変更!!!
                        'MyBase.SetMessage("W168")
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W168", LMH030BLC.SAKURA_WID_L001, ds, msgArray, matomeNo, String.Empty)
                        Return ds
                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時
                        '自動まとめ処理を行う
                        sakuraMatomeF = "1"
                    ElseIf choiceKb.Equals("03") = True Then
                        'ワーニングで"キャンセル"を選択時
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        'MyBase.SetMessage("G042")
                        Return ds
                    End If

                ElseIf autoMatomeF.Equals("1") = True Then
                    '自動まとめ処理を行う
                    sakuraMatomeF = "1"

                End If

            End If

        End If

        '出荷管理番号(大)の採番(サクラはまとめ用にパラメータが必要)
        ds = Me.GetOutkaNoL(ds, sakuraMatomeF)

        ''出荷管理番号(中)の採番(サクラはまとめ用にパラメータが必要)
        ds = Me.GetOutkaNoM(ds, sakuraMatomeF)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds, sakuraMatomeF)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

        'EDI受信テーブル(HED)データセット設定
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds, sakuraMatomeF)

        '2011.09.12 サクラまとめ時作業ﾚｺｰﾄﾞ不具合対応
        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds, sakuraMatomeF)
        'ds = Me.SetDatasetSagyo(ds)

        '運送(大,中)データセット設定
        'If ds.Tables("LMH030_OUTKAEDI_L").Rows(0)("UNSO_MOTO_KB").ToString() = "10" _
        ' AndAlso String.IsNullOrEmpty(sakuraMatomeF) = True Then

        'ds = Me.SetDatasetUnsoL(ds)
        'ds = Me.SetDatasetUnsoM(ds)

        '2011.08.19 運送のサクラまとめ対応 START
        ds = Me.SetDatasetUnsoL(ds, sakuraMatomeF)
        ds = Me.SetDatasetUnsoM(ds, sakuraMatomeF)
        '2011.08.19 運送のサクラまとめ対応 END

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds, sakuraMatomeF)

        'End If

        'タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        'EDI出荷(大)の更新
        Dim matomeCnt As Integer = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count

        '出荷登録(通常処理)
        '出荷登録(サクラまとめ処理)今からまとめるデータの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        If sakuraMatomeF.Equals("1") = True AndAlso 0 < matomeCnt Then

            '出荷登録(サクラまとめ処理)
            'まとめ先データの更新
            'EDI出荷(大)の更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If

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

        '出荷(大)の新規登録 OR 更新
        If sakuraMatomeF.Equals("1") = True Then

            '出荷(大)の更新(サクラまとめの場合)
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If

        Else
            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)

        End If

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
                AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの自動更新
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
            '2011.08.19 運送のサクラまとめ対応 START
            If sakuraMatomeF.Equals("1") = True Then

                '運送(大)の更新(サクラまとめの場合)

                ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
                If MyBase.GetResultCount = 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds
                End If
            Else

                '運送(大)の新規登録
                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)

            End If
            '2011.08.19 運送のサクラまとめ対応 END

        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)
        End If

        Return ds

    End Function

#Region "EDI_Lの初期値設定(出荷登録処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>サクラはマスタ取得のみ</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷管理番号L)"

    Private Function GetOutkaNoL(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim dr1 As DataRow
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim outkaKanriNoPrm As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_CTL_NO").ToString()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtM As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        Dim max As Integer = dt.Rows.Count - 1


        If eventShubetsu.Equals("3") = True Then

            '紐付け処理の場合
            dr("OUTKA_CTL_NO") = outkaKanriNoPrm
            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNoPrm
            Next

        ElseIf String.IsNullOrEmpty(sakuraMatomeF) = True Then

            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)
            dr("OUTKA_CTL_NO") = outkaKanriNo

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        Else
            'サクラまとめ処理の場合
            '選択データの更新
            dr("OUTKA_CTL_NO") = dtM.Rows(0)("OUTKA_CTL_NO").ToString()
            dr("FREE_C30") = String.Concat("04-", dtM.Rows(0)("EDI_CTL_NO").ToString())

            'まとめ先のまとめ番号の更新
            dr1 = ds.Tables("LMH030_OUTKAEDI_L").NewRow()
            dr1("NRS_BR_CD") = dtM.Rows(0)("NRS_BR_CD").ToString()
            dr1("OUTKA_CTL_NO") = dtM.Rows(0)("OUTKA_CTL_NO").ToString()
            dr1("EDI_CTL_NO") = dtM.Rows(0)("EDI_CTL_NO").ToString()
            dr1("FREE_C30") = String.Concat("04-", dtM.Rows(0)("EDI_CTL_NO").ToString())
            'データセットに設定
            ds.Tables("LMH030_OUTKAEDI_L").Rows.Add(dr1)

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
    Private Function GetOutkaNoM(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim outkaKanriMaxNo As Integer = 0
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
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


        ElseIf String.IsNullOrEmpty(sakuraMatomeF) = True Then

            '通常出荷登録処理の場合
            For i As Integer = 0 To max

                outkaKanriNo = (i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo

            Next

        Else
            'サクラまとめ処理の場合
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
            'outkaKanriMaxNo = Convert.ToInt32(dtMatome.Rows(0)("OUTKA_CTL_NO_CHU"))
            outkaKanriMaxNo = Me._DacMst.GetMaxOUTKA_NO_CHU(dtMatome.Rows(0)("NRS_BR_CD").ToString, dtMatome.Rows(0)("OUTKA_CTL_NO").ToString)
            '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End
            For i As Integer = 0 To max
                '2011.09.21 追加START
                dtEdiM.Rows(i)("OUTKA_CTL_NO") = dtMatome.Rows(0)("OUTKA_CTL_NO").ToString()
                '2011.09.21 追加END
                outkaKanriNo = (outkaKanriMaxNo + i + 1).ToString("000")
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
    Private Function SetDatasetOutkaL(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
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
        outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
        outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
        outkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
        outkaDr("END_DATE") = String.Empty
        outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
        outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
        outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
        outkaDr("SHIP_CD_M") = String.Empty
        outkaDr("DEST_CD") = ediDr("DEST_CD")
        outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
        outkaDr("DEST_TEL") = ediDr("DEST_TEL")
        outkaDr("NHS_REMARK") = String.Empty
        outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
        outkaDr("COA_YN") = FormatZero(ediDr("COA_YN").ToString(), 2)
        outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
        outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")

        'サクラの場合のみ
        If String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString()) = True Then
            outkaDr("REMARK") = ediDr("REMARK")
        Else
            If String.IsNullOrEmpty(ediDr("REMARK").ToString()) = False Then
                '20150810 100byteバグ対応
                If String.Concat(ediDr("REMARK"), Space(2), ediDr("EDI_DEST_CD")).Length > 100 Then
                    remark = String.Concat(ediDr("REMARK"), Space(2), ediDr("EDI_DEST_CD")).Substring(0, 100)

                Else
                    remark = String.Concat(ediDr("REMARK"), Space(2), ediDr("EDI_DEST_CD"))

                End If

                outkaDr("REMARK") = remark
                '20150810 100byteバグ対応
            End If
        End If

        '出荷梱包個数は1固定(全荷主ではなく１次はサクラのみ)
        outkaDr("OUTKA_PKG_NB") = 1
        'outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
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

        'まとめデータの場合は出荷(大)更新の為、更新日(時間)をセット
        If sakuraMatomeF.Equals("1") = True Then
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            outkaDr("SYS_UPD_DATE") = matomeDr("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomeDr("SYS_UPD_TIME")
        End If

        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)


        Return ds

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

            '2011.07.29 不具合対応(他荷主にも反映) START
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
            '2011.07.29 不具合対応(他荷主にも反映) END

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
            '▼▼▼要望番号:472
            'outkaDr("OUTKA_PKG_NB") = ediDr("OUTKA_PKG_NB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            '▲▲▲要望番号:472
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

            '2011.07.29 不具合対応(他荷主にも反映) START
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
            '2011.07.29 不具合対応(他荷主にも反映) END

            'サクラの場合のみ
            If String.IsNullOrEmpty(ediDr("OUTKA_ATT").ToString()) = True Then
                '2011.11.07  要望番号391 追加START
                'outkaDr("REMARK") = ediDr("REMARK")
                outkaDr("REMARK") = ediDr("CHUUI_NARR")
                '2011.11.07  要望番号391 追加END
            Else
                '2011.11.07  要望番号391 追加START
                'remark = String.Concat(ediDr("OUTKA_ATT"), Space(2), ediDr("REMARK")).Substring(0, 100)
                'outkaDr("REMARK") = remark
                '20150811 100byteない場合に
                If String.Concat(ediDr("OUTKA_ATT"), Space(2), ediDr("CHUUI_NARR")).Length > 100 Then
                    remark = String.Concat(ediDr("OUTKA_ATT"), Space(2), ediDr("CHUUI_NARR")).Substring(0, 100)
                Else
                    remark = String.Concat(ediDr("OUTKA_ATT"), Space(2), ediDr("CHUUI_NARR"))

                End If

                outkaDr("REMARK") = remark
                '2011.11.07  要望番号391 追加END
            End If

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
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

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
            'サクラまとめ処理判定
            If sakuraMatomeF.Equals("1") = True Then
                'サクラまとめ処理時はEDI(大)よりセット
                rcvDr("OUTKA_CTL_NO") = outkaedilDr("OUTKA_CTL_NO")
            Else
                '通常登録時はEDI(中)よりセット
                rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            End If

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
    Private Function SetDatasetSagyo(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

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

                    '2011.09.12 START
                    'サクラまとめ処理の出荷管理番号対応
                    If sakuraMatomeF.Equals("1") = True Then
                        'サクラまとめ処理時はEDI(大)よりセット
                        outkaNoLM = String.Concat(ediDrL("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))
                    Else
                        '通常登録時はEDI(中)よりセット
                        outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))
                    End If
                    '2011.09.12 END

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

                    '2011.09.12 START
                    'サクラまとめ処理の出荷管理番号対応
                    If sakuraMatomeF.Equals("1") = True Then
                        'サクラまとめ処理時はEDI(大)よりセット
                        outkaNoLM = String.Concat(ediDrL("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))
                    Else
                        '通常登録時はEDI(中)よりセット
                        outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))
                    End If
                    '2011.09.12 END

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
    Private Function SetDatasetUnsoL(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        '2011.08.19 運送のサクラまとめ対応 START
        If sakuraMatomeF.Equals("1") = True Then
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            unsoDr("NRS_BR_CD") = matomeDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = matomeDr("UNSO_NO_L")
            unsoDr("SYS_UPD_DATE") = matomeDr("SYS_UPD_DATE")
            unsoDr("SYS_UPD_TIME") = matomeDr("SYS_UPD_TIME")
            '2011.08.19 運送のサクラまとめ対応 END
        Else
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
            unsoDr("AD_3") = ediDr("DEST_AD_3")
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
    Private Function SetDatasetUnsoM(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        'エラー記録票　№42対応 START
        Dim outkaDr As DataRow
        'エラー記録票　№42対応 END

        Dim unsoNo As String = String.Empty
        Dim unsoMaxNo As Integer = 0
        Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")


        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        If sakuraMatomeF.Equals("1") = True Then
            unsoMaxNo = Convert.ToInt32(dtMatome.Rows(0)("UNSO_NO_M"))
        End If

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            'エラー記録票　№42対応 START
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").Rows(i)
            'エラー記録票　№42対応 END
            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            '2011.08.19 運送のサクラまとめ対応 START
            If sakuraMatomeF.Equals("1") = True Then
                unsoNo = (unsoMaxNo + i + 1).ToString("000")
                unsoMDr("UNSO_NO_M") = unsoNo
            Else
                unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")

            End If
            '2011.08.19 運送のサクラまとめ対応 END
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
            'エラー記録票　№42対応 START 一旦対応保留
            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            'unsoMDr("PKG_NB") = outkaDr("OUTKA_M_PKG_NB")
            'エラー記録票　№42対応 END
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
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet, ByVal sakuraMatomeF As String) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        'Dim unsoMDr As DataRow
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0
        'Dim max As Integer = ds.Tables("LMH030_UNSO_M").Rows.Count - 1

        'サクラまとめ(運送Mデータの運送重量合算)
        If sakuraMatomeF.Equals("1") = True Then

            'Dim unsoNo As String = String.Empty
            'Dim unsoMaxNo As Integer = 0
            'Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

            'まとめ先の中データ取得
            ds = MyBase.CallDAC(Me._Dac, "SelectUnsoMatomeTarget", ds)
            If MyBase.GetResultCount = 0 Then

                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                'For i As Integer = 0 To max

                '    unsoMDr = ds.Tables("LMH030_UNSO_M").Rows(i)

                '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

                'Next

                unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
                unsoLDr("NB_UT") = ediMDr("KB_UT")
                Return ds

            Else
                matomeUnsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_MATOME_UNSO_M")
                'Dim maxMoto As Integer = ds.Tables("LMH030_MATOME_UNSO_M").Rows.Count - 1

                'For i As Integer = 0 To maxMoto

                '    unsoMDr = ds.Tables("LMH030_MATOME_UNSO_M").Rows(i)

                '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

                'Next

                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                'For j As Integer = 0 To max

                '    unsoMDr = ds.Tables("LMH030_UNSO_M").Rows(j)

                '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

                '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

                'Next

                unsoLDr("UNSO_WT") = Math.Ceiling(matomeUnsoJyuryo + unsoJyuryo)

                Return ds

            End If

        Else
            unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
            'For i As Integer = 0 To max

            '    unsoMDr = ds.Tables("LMH030_UNSO_M").Rows(i)

            '    '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
            '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo

            '    unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT"))

            'Next

            unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
            unsoLDr("NB_UT") = ediMDr("KB_UT")

        End If

        Return ds

    End Function

#End Region

#Region "運送L：運送重量計算処理"

    '▼▼▼(まとめ運送重量バグ対応)
    '''' <summary>
    '''' 運送重量再計算処理
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

    '    Dim unsoMDr As DataRow
    '    Dim unsoJyuryo As Decimal = 0
    '    Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1
    '    '▼▼▼(運送M個別重量不具合）
    '    Dim ediMDr As DataRow
    '    '▲▲▲(運送M個別重量不具合）

    '    For i As Integer = 0 To max

    '        unsoMDr = ds.Tables(tblNm).Rows(i)

    '        '▼▼▼運送対応
    '        ediMDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
    '        '▲▲▲運送対応

    '        '▼▼▼運送対応
    '        '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
    '        'unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) + unsoJyuryo
    '        unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * Convert.ToDecimal(ediMDr("OUTKA_TTL_NB")) + unsoJyuryo
    '        '▲▲▲運送対応

    '    Next

    '    Return unsoJyuryo

    'End Function


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
    '▲▲▲(まとめ運送重量バグ対応)

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
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"包装単位", "区分マスタ"})
            Return False
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        Return True

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定(サクラ専用)"

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
        If String.IsNullOrEmpty(ediMDr("KB_UT").ToString()) = True Then
            ediMDr("KB_UT") = mGoodsDr("NB_UT")
        End If

        '数量単位
        If String.IsNullOrEmpty(ediMDr("QT_UT").ToString()) = True Then
            ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")
        ElseIf (ediMDr("QT_UT").ToString()).Equals(ediMDr("QT_UT").ToString()) = False Then
            ediMDr("QT_UT") = ediMDr("IRIME_UT")
        End If

        '包装個数
        '2011.09.26 修正START　包装個数違いはマスタ値強制入れ替え
        'If String.IsNullOrEmpty(ediMDr("PKG_NB").ToString()) = True Then
        ediMDr("PKG_NB") = mGoodsDr("PKG_NB")
        'End If

        '包装単位
        If String.IsNullOrEmpty(ediMDr("PKG_UT").ToString()) = True Then
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
                    '2011.09.27 修正 START出荷画面対応による個数,端数の値入替
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

        '出荷時注意事項
        ediMDr("OUTKA_ATT") = mGoodsDr("OUTKA_ATT")

        Return True

    End Function

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
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E349", New String() {"EDIデータ", "出荷管理番号"})
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"出荷報告有無"})
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷予定日"})
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出庫日"})
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E166", New String() {"出荷予定日", "出庫日"})
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"納入予定日"})
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"出荷報告日"})
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(大)"})
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(中)"})
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"送り状作成有無"})
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
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E377", New String() {"出荷データ"})
                    Return False
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷区分", "区分マスタ"})
            Return False
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"出荷種別区分", "区分マスタ"})
            Return False
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"作業進捗区分", "区分マスタ"})
            Return False
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"ピッキングリスト区分", "区分マスタ"})
            Return False
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"倉庫コード", "倉庫マスタ"})
            Return False
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"納入予定時刻", "区分マスタ"})
                Return False
            End If

        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"荷主コード", "荷主マスタ"})
            Return False
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"運送手配区分", "区分マスタ"})
            Return False
        End If

        'Dim destCd As String = String.Empty
        'Dim ediDestCd As String = String.Empty
        'destCd = drEdiL("DEST_CD").ToString()
        'ediDestCd = drEdiL("EDI_DEST_CD").ToString()

        ''★★★2012.01.12 要望番号596 START
        ''荷送人コードのマスタ存在チェック
        'Dim shipCdL As String = drEdiL("SHIP_CD_L").ToString()      '荷送人コード
        ''SHIP_CD_Lが空でなく、SHIP_CD_L = DEST_CD <> EDI_DEST_CD の場合、もしくはSHIP_CD_L <> DEST_CD の場合
        'If (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = True AndAlso shipCdL.Equals(ediDestCd) = False) _
        '    OrElse (String.IsNullOrEmpty(shipCdL) = False AndAlso shipCdL.Equals(destCd) = False) Then

        '    ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdestShip", ds)

        '    If MyBase.GetResultCount = 0 Then
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷送人コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '        Return False
        '    End If
        'End If
        ''★★★2012.01.12 要望番号596 END

        ''届先コード(届先マスタ)
        ''届先コードが"空"の場合は、届先コードを元に届先Mの存在チェックを行う
        'If String.IsNullOrEmpty(destCd) = True Then
        '    '届先コードが"空"の場合は、EDI届先コードを元に届先Mの存在チェックを行う
        '    If String.IsNullOrEmpty(ediDestCd) = True Then
        '        '両方"空"の場合はチェックなし
        '    Else
        '        '0件の場合はエラー(EDI届先コードはユニークな為)
        '        ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdest", ds)
        '        If MyBase.GetResultCount = 0 Then
        '            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '            'MyBase.SetMessage("E326", New String() {"EDI届先コード", "届先マスタ"})
        '            Return False
        '        End If

        '        'サクラ専用 マスタ値とEDI出荷(大)の整合性チェック
        '        If Me.SakuraDestCompareCheck(ds, rowNo, ediCtlNo) = False Then
        '            Return False
        '        End If

        '    End If

        'Else
        '    ds = MyBase.CallDAC(Me._DacMst, "SelectDataMdest", ds)
        '    If MyBase.GetResultCount = 0 Then
        '        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '        'MyBase.SetMessage("E326", New String() {"届先コード", "届先マスタ"})
        '        Return False

        '    End If

        '    'サクラ専用 マスタ値とEDI出荷(大)の整合性チェック
        '    If Me.SakuraDestCompareCheck(ds, rowNo, ediCtlNo) = False Then
        '        Return False
        '    End If

        'End If

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"タリフ分類区分", "区分マスタ"})
            Return False
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"車輌区分", "区分マスタ"})
                Return False
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"便区分", "区分マスタ"})
            Return False
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
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"})
                Return False
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacMst, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"})
                Return False
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacMst, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"元着払い区分", "区分マスタ"})
            Return False
        End If

        '-------------------------------------------------------------------------------------
        '●荷主固有チェック
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim workDestCd As String = String.Empty
        Dim workDestString As String = String.Empty

        '使用する届先コードを決定する
        If Not String.IsNullOrEmpty(drEdiL("DEST_CD").ToString) Then
            '届先コードを使用
            workDestCd = drEdiL("DEST_CD").ToString()
            workDestString = "届先コード"
        ElseIf Not String.IsNullOrEmpty(drEdiL("EDI_DEST_CD").ToString) Then
            'EDI届先コードを使用
            workDestCd = drEdiL("EDI_DEST_CD").ToString()
            workDestString = "EDI届先コード"
        End If

        If String.IsNullOrEmpty(workDestCd) Then
            '届先コードが空でもサクラは許可

        Else
            Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

            If mDestCount = 1 Then
                '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェック
                'EDI時点での届先Ｍ情報と出荷登録時の届先Ｍ情報がズレがないかのチェック
                If Not Me.DestCompareCheck(ds, rowNo, ediCtlNo) Then
                    If MyBase.IsMessageStoreExist(Convert.ToInt32(rowNo)) Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    End If
                End If

            ElseIf mDestCount = 0 Then
                '0件の場合、届先マスタに追加する
                'ただし、JISコードが取得できなければエラー
                If Not Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) Then
                    If MyBase.IsMessageStoreExist(Convert.ToInt32(rowNo)) Then
                        'チェックでエラーがあった場合は処理終了
                        Return False
                    End If
                End If

            Else
                '複数件の場合、エラー
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
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
        'Dim dtIn As DataTable = ds.Tables("LMH030INOUT")

        '引当単位区分
        If Me._Blc.AlctdKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"引当単位区分"})
            Return False
        End If

        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E352")
            Return False
        End If
        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
            Return False
        End If
        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E334", New String() {"入目と出荷総数量"})
            Return False
        End If
        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "出荷登録"})
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

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        '2011.10.06 START EDI(メモ)№79
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END

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
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E326", New String() {"商品コード", "商品マスタ"})
                    Return False
                End If

                If 1 < MyBase.GetResultCount Then
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"商品キー", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E494", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    'MyBase.SetMessage("E330", New String() {"商品コード", "商品マスタ"})
                    Return False
                End If

                'EDI出荷(中)の初期値設定処理
                '2011.09.11 運送温度区分対応START
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then
                    Return False
                End If

                'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                '2011.09.11 運送温度区分対応END

                'サクラ専用 マスタ値とEDI出荷(中)の整合性チェック
                If Me.SakuraGoodsCompareCheck(ds, setDs, i, rowNo, ediCtlNo) = False Then

                    Return False
                End If

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納

                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                    Return False
                End If

            Else
                If String.IsNullOrEmpty(custGoodsCd) = False Then

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    '2011.10.06 START EDI(メモ)№79
                    setDtL.ImportRow(dtL.Rows(0))
                    setDtM.ImportRow(dtM.Rows(i))
                    '2011.10.06 END

                    '商品マスタ検索（NRS商品コード or 荷主商品コード）
                    setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsOutka", setDs))

                    If MyBase.GetResultCount = 0 Then
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                        'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                        '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                        'MyBase.SetMessage("E326", New String() {"荷主商品コード", "商品マスタ"})
                        Return False
                    ElseIf GetResultCount() > 1 Then

                        '入目 + 荷主商品コードで再検索
                        setDs = (MyBase.CallDAC(Me._DacMst, "SelectDataMgoodsIrimeOutka", setDs))

                        If MyBase.GetResultCount = 1 Then
                        Else
                            'サクラの場合は複数件ヒットはエラー
                            '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"荷主商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                            Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E494", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                            '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                            'MyBase.SetMessage("E330", New String() {"商品コード", "商品マスタ"})
                            Return False
                        End If

                    End If

                    'EDI出荷(中)の初期値設定処理
                    '2011.09.11 運送温度区分対応START
                    If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then
                        Return False
                    End If

                    'ds = Me.EdiMDefaultSet(ds, setDs, i, unsoData)
                    '2011.09.11 運送温度区分対応END

                    'サクラ専用 マスタ値とEDI出荷(中)の整合性チェック
                    If Me.SakuraGoodsCompareCheck(ds, setDs, i, rowNo, ediCtlNo) = False Then

                        Return False
                    End If

                    '運送重量取得用項目をデータセット(EDI出荷(中))に格納

                    If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then

                        Return False
                    End If

                Else
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("E019", New String() {"商品コード"})
                    Return False

                End If

            End If

        Next

        Return True

    End Function

#End Region

#Region "届先マスタチェック(サクラ用)"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SakuraDestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Dim mSysDelF As String = String.Empty
        Dim mDestNm As String = String.Empty
        Dim mAd1 As String = String.Empty
        Dim mAd2 As String = String.Empty

        Dim ediDestCd As String = String.Empty
        Dim ediDestNm As String = String.Empty
        Dim ediAd1 As String = String.Empty
        Dim ediAd2 As String = String.Empty


        mSysDelF = dtM.Rows(0).Item("SYS_DEL_FLG").ToString()
        mDestNm = dtM.Rows(0).Item("DEST_NM").ToString()
        mAd1 = dtM.Rows(0).Item("AD_1").ToString()
        mAd2 = dtM.Rows(0).Item("AD_2").ToString()

        ediDestCd = dtEdi.Rows(0)("DEST_CD").ToString()
        ediDestNm = dtEdi.Rows(0)("DEST_NM").ToString()
        ediAd1 = dtEdi.Rows(0)("DEST_AD_1").ToString()
        ediAd2 = dtEdi.Rows(0)("DEST_AD_2").ToString()

        If String.IsNullOrEmpty(ediDestCd) = False Then

            '削除フラグ(届先マスタ)
            If mSysDelF.Equals("1") = True Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E331", New String() {"届先コード", "届先マスタ"})
                Return False
            End If


            mDestNm = SpaceCutChk(mDestNm)
            ediDestNm = SpaceCutChk(ediDestNm)
            '届先名称(マスタ値が完全一致でなければエラー)
            If mDestNm.Equals(ediDestNm) = True Then
                'チェックなし
            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先名称", "届先マスタ", "届先名称"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E332", New String() {"届先名称", "届先マスタ", "届先名称"})
                Return False
            End If

        End If

        '住所1(マスタ値が完全一致でなければエラー)
        If String.IsNullOrEmpty(ediAd1) = True Then
            'チェックなし
        Else

            mAd1 = SpaceCutChk(mAd1)
            ediAd1 = SpaceCutChk(ediAd1)
            If mAd1.Equals(ediAd1) = False Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所1", "届先マスタ", "住所1"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E332", New String() {"届先住所1", "届先マスタ", "住所1"})
                Return False
            End If

        End If

        '住所2(マスタ値が完全一致でなければエラー)
        If String.IsNullOrEmpty(ediAd2) = True Then
            'チェックなし
        Else

            mAd2 = SpaceCutChk(mAd2)
            ediAd2 = SpaceCutChk(ediAd2)
            If mAd2.Equals(ediAd2) = False Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所2", "届先マスタ", "住所2"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E332", New String() {"届先住所2", "届先マスタ", "住所2"})
                Return False
            End If

        End If

        Return True

    End Function

#End Region

#Region "商品マスタチェック(サクラ用)"

    ''' <summary>
    ''' マスタ値とEDI出荷(中)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SakuraGoodsCompareCheck(ByRef ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer, _
                                             ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = setDs.Tables("LMH030_M_GOODS")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim mNbUt As String = String.Empty
        Dim mPkgNb As String = String.Empty
        Dim mPkgUt As String = String.Empty
        Dim mStdIrimeNb As String = String.Empty
        Dim mStdIrimeUt As String = String.Empty

        Dim ediNbUt As String = String.Empty
        Dim ediPkgNb As String = String.Empty
        Dim ediPkgUt As String = String.Empty
        Dim ediStdIrimeNb As String = String.Empty
        Dim ediStdIrimeUt As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        mNbUt = dtM.Rows(0).Item("NB_UT").ToString()
        mPkgNb = dtM.Rows(0).Item("PKG_NB").ToString()
        mPkgUt = dtM.Rows(0).Item("PKG_UT").ToString()
        mStdIrimeNb = dtM.Rows(0).Item("STD_IRIME_NB").ToString()
        mStdIrimeUt = dtM.Rows(0).Item("STD_IRIME_UT").ToString()

        ediNbUt = dtEdi.Rows(count)("KB_UT").ToString()
        ediPkgNb = dtEdi.Rows(count)("PKG_NB").ToString()
        ediPkgUt = dtEdi.Rows(count)("PKG_UT").ToString()
        ediStdIrimeNb = dtEdi.Rows(count)("IRIME").ToString()
        ediStdIrimeUt = dtEdi.Rows(count)("IRIME_UT").ToString()

        '2011.09.26 包装個数のエラーチェックは不要とする。
        ''包装個数(エラー)
        'If String.IsNullOrEmpty(ediPkgNb) = True Then
        '    'チェックなし
        'Else

        '    If ediPkgNb.Equals(mPkgNb) = False Then
        'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"包装個数", "商品マスタ", "包装個数"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '2011.09.26 修正END
        'MyBase.SetMessage("E332", New String() {"包装個数", "商品マスタ", "包装個数"})
        'Return False
        '    End If

        'End If

        '入目(エラー) 全荷主共通
        'If Convert.ToDouble(ediStdIrimeNb) = 0 AndAlso Convert.ToDouble(mStdIrimeNb) = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E333", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    'MyBase.SetMessage("E333")
        '    Return False
        'End If

        '個数単位(ワーニング)
        If String.IsNullOrEmpty(ediNbUt) = True Then
            'チェックなし
        Else
            If ediNbUt.Equals(mNbUt) = False Then

                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SAKURA_WID_M001, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'MyBase.SetMessage("W159", New String() {"個数単位", "商品マスタ", "個数単位", "EDIデータ"})

                    msgArray(1) = "個数単位"
                    msgArray(2) = "商品マスタ"
                    msgArray(3) = "個数単位"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SAKURA_WID_M001, ds, setDs, msgArray, ediNbUt, mNbUt)

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtEdi.Rows(count)("KB_UT") = dtM.Rows(0).Item("NB_UT").ToString()
                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("G042")

                End If

            End If

        End If

        '包装単位(ワーニング)
        If String.IsNullOrEmpty(ediPkgUt) = True Then
            'チェックなし
        Else

            If ediPkgUt.Equals(mPkgUt) = False Then

                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SAKURA_WID_M002, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'MyBase.SetMessage("W159", New String() {"包装単位", "商品マスタ", "包装単位", "EDIデータ"})

                    msgArray(1) = "包装単位"
                    msgArray(2) = "商品マスタ"
                    msgArray(3) = "包装単位"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SAKURA_WID_M002, ds, setDs, msgArray, ediPkgUt, mPkgUt)
                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtEdi.Rows(count)("PKG_UT") = dtM.Rows(0).Item("PKG_UT").ToString()
                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("G042")

                End If

            End If

        End If

        '入目単位(ワーニング)
        If String.IsNullOrEmpty(ediStdIrimeUt) = True Then
            'チェックなし
        Else

            If ediStdIrimeUt.Equals(mStdIrimeUt) = False Then

                choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.SAKURA_WID_M003, count)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'MyBase.SetMessage("W159", New String() {"入目単位", "商品マスタ", "入目単位", "EDIデータ"})

                    msgArray(1) = "入目単位"
                    msgArray(2) = "商品マスタ"
                    msgArray(3) = "入目単位"
                    msgArray(4) = "EDIデータ"
                    ds = Me._Blc.SetComWarningM("W159", LMH030BLC.SAKURA_WID_M003, ds, setDs, msgArray, ediStdIrimeUt, mStdIrimeUt)

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtEdi.Rows(count)("IRIME_UT") = dtM.Rows(0).Item("STD_IRIME_UT").ToString()
                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    'MyBase.SetMessage("G042")

                End If

            End If

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
    ''' <remarks>サクラではJISが取得できなければエラーとする（ワーニングにはしない）</remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim drEdiL As DataRow = dtEdi.Rows(0)
        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")

        'EDIの郵便番号からハイフンを取り除き取得
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString.Replace("-", String.Empty)
        dtEdi.Rows(0)("DEST_ZIP") = ediZip

        '郵便番号がなければJISコードが取得できないのでエラー
        If String.IsNullOrEmpty(ediZip) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E02G", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '郵便番号を元にJISコードを取得
        Dim mZipJis As String = String.Empty
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)
        If MyBase.GetResultCount = 0 Then
            '取得できなければエラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E02G", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        Else
            mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
        End If

        Dim drMD As DataRow = dtMdest.NewRow()
        drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
        drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
        drMD("DEST_CD") = workDestCd
        drMD("EDI_CD") = workDestCd
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

        Return True

    End Function

#End Region

#Region "届先マスタチェック"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim choiceKb As String = String.Empty
        Dim flgWarning As Boolean = False
        Dim msgArray(5) As String

        '届先マスタの情報を取得
        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = Me.SpaceCutChk(dtMdest.Rows(0).Item("DEST_NM").ToString)
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString.Replace("-", String.Empty)
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()

        'EDIの情報を取得
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim ediDestNm As String = Me.SpaceCutChk(dtEdi.Rows(0)("DEST_NM").ToString)
        Dim ediDestAd1 As String = dtEdi.Rows(0)("DEST_AD_1").ToString()
        Dim ediDestAd2 As String = dtEdi.Rows(0)("DEST_AD_2").ToString()
        Dim ediDestAd3 As String = dtEdi.Rows(0)("DEST_AD_3").ToString()
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString.Replace("-", String.Empty)
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        '届先マスタが削除されていればエラー
        If "1".Equals(mSysDelF) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '届先名称が不一致ならばワーニング対象
        If (Not flgWarning) AndAlso (Not mDestNm.Equals(ediDestNm)) Then
            flgWarning = True
        End If

        '届先住所が不一致ならばワーニング対象（SPACE除去+文字変換はしない）
        If (Not flgWarning) AndAlso (Not mAd1.Equals(ediDestAd1)) Then
            flgWarning = True
        End If
        If (Not flgWarning) AndAlso (Not mAd2.Equals(ediDestAd2)) Then
            flgWarning = True
        End If
        If (Not flgWarning) AndAlso (Not mAd3.Equals(ediDestAd3)) Then
            flgWarning = True
        End If

        '届先郵便番号が不一致ならばワーニング対象
        If (Not flgWarning) AndAlso (Not mZip.Equals(ediZip)) Then
            flgWarning = True
        End If

        '届先電話番号が不一致ならばワーニング対象
        If (Not flgWarning) AndAlso (Not mTel.Equals(ediTel)) Then
            flgWarning = True
        End If

        'EDIの届先JISコードが空ならば設定する
        '設定できなければ空にしておき、後のワーニングで"EDI"が選択された際にエラーとする
        If String.IsNullOrEmpty(ediDestJisCd) Then
            If Not String.IsNullOrEmpty(ediZip) Then
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)
                If MyBase.GetResultCount > 0 Then
                    '郵便番号から取得したJISを設定
                    ediDestJisCd = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                    dtEdi.Rows(0)("DEST_JIS_CD") = ediDestJisCd
                End If
            End If
        End If

        '届先JISコードが不一致ならばワーニング対象
        If (Not flgWarning) AndAlso (Not mJis.Equals(ediDestJisCd)) Then
            flgWarning = True
        End If

        'ワーニング対象となった場合の処理
        If flgWarning Then
            '選択区分の取得
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.SAKURA_WID_L002, 0)

            If String.IsNullOrEmpty(choiceKb) Then
                'ワーニング情報をセット
                Dim mAdAll As String = String.Concat(mAd1, "\n", mAd2, "\n", mAd3)
                Dim ediDestAdAll As String = String.Concat(ediDestAd1, "\n", ediDestAd2, "\n", ediDestAd3)

                Dim mInfo As String = String.Concat(mDestNm, vbTab, mAdAll, vbTab, mZip, vbTab, mTel, vbTab, mJis, vbTab, "@")
                Dim ediInfo As String = String.Concat(ediDestNm, vbTab, ediDestAdAll, vbTab, ediZip, vbTab, ediTel, vbTab, ediDestJisCd, vbTab, "@")

                ds = Me._Blc.SetComWarningL("W307", LMH030BLC.SAKURA_WID_L002, ds, msgArray, ediInfo, mInfo)

            ElseIf "01".Equals(choiceKb) Then
                'ワーニングで"EDI"が選択された

                'JISコードが設定できていなければエラー
                If String.IsNullOrEmpty(dtEdi.Rows(0)("DEST_JIS_CD").ToString) Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E02G", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

                'EDI値で届先マスタを更新する
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
                dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                dtMdest.Rows(0).Item("JIS") = dtEdi.Rows(0)("DEST_JIS_CD").ToString()
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            ElseIf "02".Equals(choiceKb) Then
                'ワーニングで"マスタ"が選択された
                '届先マスタ値をEDIにセットする
                dtEdi.Rows(0)("DEST_NM") = dtMdest.Rows(0).Item("DEST_NM").ToString()
                dtEdi.Rows(0)("DEST_AD_1") = dtMdest.Rows(0).Item("AD_1").ToString()
                dtEdi.Rows(0)("DEST_AD_2") = dtMdest.Rows(0).Item("AD_2").ToString()
                dtEdi.Rows(0)("DEST_AD_3") = dtMdest.Rows(0).Item("AD_3").ToString()
                dtEdi.Rows(0)("DEST_ZIP") = dtMdest.Rows(0).Item("ZIP").ToString()
                dtEdi.Rows(0)("DEST_TEL") = dtMdest.Rows(0).Item("TEL").ToString()
                dtEdi.Rows(0)("DEST_JIS_CD") = dtMdest.Rows(0).Item("JIS").ToString()
            End If
        End If

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

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim updFlg As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_L_UPD_FLG").ToString()

        '2011.09.28 追加START 出荷取消データの実績作成対応
        Dim outkaDelFlg As String = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
        '2011.09.28 追加END

        ''2011.09.28★★ 追加START 出荷取消データの実績作成対応
        'Dim ediDelFlg As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("SYS_DEL_FLG").ToString()

        'サクラEDI実績の値設定(EDI受信TBLより)
        ds = MyBase.CallDAC(Me._Dac, "SelectSakuraEdiSend", ds)
        If MyBase.GetResultCount = 0 Then
            '▼▼▼20011.09.21
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷テーブル", "該当レコード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '▲▲▲20011.09.21
            'MyBase.SetMessage("E326", New String() {"出荷テーブル", "該当レコード"})
            Return ds
            Exit Function
        End If
        'サクラEDI実績データセット再設定処理
        ds = Me.SetDatasetEdiSakuraSend(ds)


        'サクラEDI実績データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertSakuraEdiSendData", ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        'If ediDelFlg.Equals("0") = True Then
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        'End If
        '2011.09.28★★ 追加END

        'EDI受信(DTL)の更新
        '2011.09.28★★ 追加START 出荷取消データの実績作成対応
        'If ediDelFlg.Equals("0") = True Then
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)
        'End If
        '2011.09.28★★ 追加END

        '出荷(大)の更新
        '2011.09.28 修正START★★ 出荷取消データの実績作成対応
        If updFlg.Equals("1") = True AndAlso (String.IsNullOrEmpty(outkaDelFlg)) = False Then
            '2011.09.28 修正END 
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If


        Return ds

    End Function

#Region "データセット再設定(EDIサクラ実績送信TBL)"
    ''' <summary>
    ''' ＥＤＩ出荷管理番号小番の採番(サクラEDI実績データ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiSakuraSend(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMH030_H_SENDOUTEDI_SFJ").Rows.Count - 1
        Dim sakuraDr As DataRow = ds.Tables("LMH030_H_SENDOUTEDI_SFJ").Rows(0)
        Dim BeforeRowEdiNo As String = String.Empty
        Dim BeforeRowEdiNoChu As String = String.Empty
        Dim BeforeRowEdiNoSho As Integer = 0

        For i As Integer = 0 To max

            sakuraDr = ds.Tables("LMH030_H_SENDOUTEDI_SFJ").Rows(i)

            If BeforeRowEdiNo.Equals(sakuraDr("EDI_CTL_NO").ToString()) = True _
            AndAlso BeforeRowEdiNoChu.Equals(sakuraDr("EDI_CTL_NO_CHU").ToString()) = True Then

                sakuraDr("EDI_CTL_NO_SHO") = (BeforeRowEdiNoSho + 1).ToString("000")
                BeforeRowEdiNoSho = Convert.ToInt32(sakuraDr("EDI_CTL_NO_SHO"))

            Else

                sakuraDr("EDI_CTL_NO_SHO") = "001"
                '2011.09.26 修正START 
                BeforeRowEdiNoSho = Convert.ToInt32("001")
                '2011.09.26 修正END

            End If

            BeforeRowEdiNo = sakuraDr("EDI_CTL_NO").ToString()
            BeforeRowEdiNoChu = sakuraDr("EDI_CTL_NO_CHU").ToString()

        Next

        Return ds

    End Function
#End Region

    '▼▼▼20011.09.21
#Region "実績作成時同一まとめレコード取得処理"
    ''' <summary>
    ''' 実績作成時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatome", ds)

        Return ds

    End Function

#End Region
    '▲▲▲20011.09.21

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
        '紐付け処理の場合はサクラまとめフラグは"0"
        ds = Me.SetDatasetEdiRcvDtl(ds, "0")

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

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

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

        '2011.09.30 追加START 出荷取消データの実績作成済⇒実績未の対応
        Dim outkaDelFlg As String = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
        '2011.09.30 追加END

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

        '2011.09.30追加 START 取消データの実績作成,実績送信済⇒実績未の対応
        '出荷(大)の更新
        If String.IsNullOrEmpty(outkaDelFlg) = False Then
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If


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

    '2011.09.27 追加START
#Region "出荷取消⇒未登録時同一まとめレコード取得処理"
    ''' <summary>
    ''' 出荷取消⇒未登録時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeTorikesi", ds)

        Return ds

    End Function

#End Region
    '2011.09.27 追加END

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

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

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

        'EDI出荷(大):一括変更
        ds = MyBase.CallDAC(Me._Dac, "UpdateHenko", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        Return ds

    End Function

#End Region

#End Region

    '#Region "更新エラーチェック"

    '    ''' <summary>
    '    ''' 更新エラーチェック
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' 
    '    Private Function UpdateErrorCheck() As Boolean

    '        If MyBase.IsMessageStoreExist = True Then
    '            Return False
    '        End If

    '        Return True

    '    End Function

    '#End Region

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

#Region "ワーニング処理(EDI(大))選択区分の取得"

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

#Region "ワーニング処理(EDI(中)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
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

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

End Class
