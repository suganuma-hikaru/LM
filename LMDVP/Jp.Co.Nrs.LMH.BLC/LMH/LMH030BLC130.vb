' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  456　　　 : DSP五協フード＆ケミカル株式会社（横浜）
'  作  成  者       :  Annen
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC130
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC130 = New LMH030DAC130()

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

#Region "Const"

#End Region

#Region "Method"

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

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)

        'EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
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

        Dim matomeFlg As Boolean = False

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

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

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
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

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

        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"
            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの更新 2012.03.20 追加START
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
        '届先マスタの更新 2012.03.20 追加END

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

#Region "EDI_Lの初期値設定(出荷登録処理)"
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

        '不具合暫定対応 START
        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If
        '不具合暫定対応 END

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""
        Else
            'DACで値セットを行う
            '2012.02.25 大阪対応 START
            'ds = MyBase.CallDAC(Me._DacCom, "SetShipNmL", ds)
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
            '2012.02.25 大阪対応 END
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '追加箇所 20120222
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
        '追加箇所 20120222

        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True OrElse _
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
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False AndAlso _
                   String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                    ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                Else
                    If InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        '2012.03.02 大阪対応START
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                        '2012.03.02 大阪対応END
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                ''運送会社マスタの存在チェック
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

        '追加箇所 20120222
        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If
        '追加箇所 20120222

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
                '2012.04.04 要望番号:943 修正 Start
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim()
                '2012.04.04 要望番号:943 修正 End
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
        Dim compareWarningFlg As Boolean = False

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
        End If

        '荷主注文番号(明細単位)
        'DEL Start 2018.10.22 要望管理002632 オーダー番号Mが空の場合でもオーダー番号Lで置換しないよう修正
        'If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
        '    ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
        'End If
        'DEL End   2018.10.22 要望管理002632 オーダー番号Mが空の場合でもオーダー番号Lで置換しないよう修正

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


        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー(サクラ以外)
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

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
        If String.IsNullOrEmpty(mGoodsDr("OUTKA_ATT").ToString()) = False Then
            ediMDr("REMARK") = mGoodsDr("OUTKA_ATT").ToString()
            If String.IsNullOrEmpty(ediMDr("FREE_C02").ToString()) = False Then
                ediMDr("REMARK") = ediMDr("REMARK").ToString() & " " & ediMDr("FREE_C02").ToString()
            End If
        Else
            If String.IsNullOrEmpty(ediMDr("FREE_C02").ToString()) = False Then
                ediMDr("REMARK") = ediMDr("FREE_C02").ToString()
            End If
        End If

        '2012.03.01 大阪対応START
        'ワーニングが存在する場合はここでの判定はFalseで返す
        '(第日本住友製薬は、現状ワーニング設定なし)
        If compareWarningFlg = True Then
            Return False
        End If
        '2012.03.01 大阪対応END

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
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                '要望番号:1159　対応　2012.06.28 Start
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                'Call MyBase.CallDAC(Me._Dac, "SelectOrderCheckData", ds)
                '要望番号:1159　対応　2012.06.28 End
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


        'Todo)
        'Annen まだタリフ等の情報はどうするかの確認が取れていないので
        '取り合えずチェックは外す。（EDI出荷マスタ（大）で運送手配には「未設定」を設定している）
        ''運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        'drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        'drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        'ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return False
        'End If

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
        '●荷主固有チェック
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
            '2012.04.04 要望番号:943 修正 Start
            'DEST_CDとEDI_DEST_CDが両方空の場合、エラーとする。
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"届先(EDI)コードが空", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
            '2012.04.04 要望番号:943 修正 End
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェックとZIPコードのマスタ存在チェック
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
        Dim dtInOut As DataTable = ds.Tables("LMH030INOUT")
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

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.DSPGKY_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                If LMConst.FLG.ON.Equals(dtInOut.Rows(0).Item("FLAG_19").ToString) Then
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsDetailsPrefixOutka", setDs))
                Else
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsPrefixOutka", setDs))
                End If

                If MyBase.GetResultCount = 0 Then
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                ElseIf GetResultCount() > 1 Then

                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    '注意!!! セットメッセージは消してよいのか判断がつかないので調査する
                    'MyBase.SetMessage("W162")
                    msgArray(1) = String.Empty
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty

                    ds = Me._Blc.SetComWarningM("W281", LMH030BLC.DSPGKY_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                    flgWarning = True 'ワーニングフラグをたてて処理続行

                    Continue For

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    '2012.03.01 大阪対応START
                    '大日本住友製薬は現段階ではワーニングはないが、共通のロジックを組み込む為入れておく
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、flgWarning=True
                        flgWarning = True
                    End If
                    '2012.03.01 大阪対応END

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

#Region "届先マスタチェック"
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
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        mDestNm = Me.SpaceCutChk(mDestNm)
        ediDestNm = Me.SpaceCutChk(ediDestNm)

        '届先名称(マスタ値が完全一致でなければワーニング)
        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L006, 0)

            If String.IsNullOrEmpty(choiceKb) = True Then
                msgArray(1) = "届先名称"
                msgArray(2) = "届先マスタ"
                msgArray(3) = "届先名称"
                msgArray(4) = "EDIデータ"
                msgArray(5) = String.Empty
                '2012.03.22 修正START
                ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DSP_WID_L006, ds, msgArray, _
                                            dtEdi.Rows(0)("DEST_NM").ToString(), dtMdest.Rows(0).Item("DEST_NM").ToString())
                'ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DSP_WID_L006, ds, msgArray, ediDestNm, mDestNm)
                '2012.03.22 修正END

                compareWarningFlg = True

            ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            End If

        End If

        'FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L007, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先住所"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "住所"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DSP_WID_L007, ds, msgArray, ediFreeC21, mAdAll)

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

        '届先電話番号(マスタ値が完全一致でなければワーニング)
        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L008, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then

                    msgArray(1) = "届先電話番号"
                    msgArray(2) = "届先マスタ"
                    msgArray(3) = "電話番号"
                    msgArray(4) = "EDIデータ"
                    msgArray(5) = String.Empty
                    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.DSP_WID_L008, ds, msgArray, ediTel, mTel)

                    compareWarningFlg = True

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                    'マスタ更新対象フラグ
                    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                End If

            End If

        End If

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先JISコード", "JISマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "郵便番号マスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END

        End If

        'Else
        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END

        If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then

            '要望番号954 修正START ワーニング表示させずに、自動更新を行う
            'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
            'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L009, 0)

            'If String.IsNullOrEmpty(choiceKb) = True Then

            '    msgArray(1) = "JISコード"
            '    msgArray(2) = "届先マスタ"
            '    msgArray(3) = "JISコード"
            '    msgArray(4) = "EDIデータ"
            '    msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            '    ds = Me._Blc.SetComWarningL("W197", LMH030BLC.DSP_WID_L009, ds, msgArray, ediDestJisCd, mJis)

            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then
            '    'ワーニングで"はい"を選択時
            '    dtMdest.Rows(0).Item("JIS") = ediDestJisCd
            '    'マスタ更新対象フラグ
            '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            'End If

            dtMdest.Rows(0).Item("JIS") = ediDestJisCd
            'マスタ更新対象フラグ
            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            '要望番号954 修正END ワーニング表示させずに、自動更新を行う


            '要望番号954 修正START ワーニング表示させない
            'ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
            '    'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
            '    choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L010, 0)

            '    If String.IsNullOrEmpty(choiceKb) = True Then

            '        ds = Me._Blc.SetComWarningL("W188", LMH030BLC.DSP_WID_L010, ds, msgArray, ediDestJisCd, mJis)

            '        compareWarningFlg = True

            '    ElseIf choiceKb.Equals("01") = True Then
            '        'ワーニングで"はい"を選択時

            '    End If
            '要望番号954 修正END ワーニング表示させない

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then

            '要望番号954 修正START ワーニング表示させずに、自動更新を行う
            ''EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
            'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L011, 0)

            'If String.IsNullOrEmpty(choiceKb) = True Then

            '    msgArray(1) = "JISコード"
            '    msgArray(2) = "届先マスタ"
            '    msgArray(3) = "JISコード"
            '    msgArray(4) = "EDIデータ"
            '    msgArray(5) = String.Empty
            '    ds = Me._Blc.SetComWarningL("W187", LMH030BLC.DSP_WID_L011, ds, msgArray, ediDestJisCd, mJis)

            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then
            '    'ワーニングで"はい"を選択時
            '    dtMdest.Rows(0).Item("JIS") = ediDestJisCd
            '    'マスタ更新対象フラグ
            '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            'ElseIf choiceKb.Equals("02") = True Then
            '    'ワーニングで"いいえ"を選択時
            'End If

            dtMdest.Rows(0).Item("JIS") = ediDestJisCd
            'マスタ更新対象フラグ
            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            '要望番号954 修正END ワーニング表示させずに、自動更新を行う

        ElseIf String.IsNullOrEmpty(ediDestJisCd) = True AndAlso mZipJis.Equals(mJis) = False Then

            '要望番号954 修正START ワーニング表示させずに、自動更新を行う
            ''EDIのJISコードが空でJISマスタ(郵便番号マスタ)のJISコードと届先マスタのJISコードに差異がある場合
            'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L011, 0)

            'If String.IsNullOrEmpty(choiceKb) = True Then

            '    msgArray(1) = String.Concat(warningString, "から取得したJISコード")
            '    msgArray(2) = "届先マスタ"
            '    msgArray(3) = "JISコード"
            '    msgArray(4) = warningString
            '    msgArray(5) = String.Empty
            '    ds = Me._Blc.SetComWarningL("W187", LMH030BLC.DSP_WID_L011, ds, msgArray, mZipJis, mJis)

            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then
            '    'ワーニングで"はい"を選択時
            '    dtMdest.Rows(0).Item("JIS") = mZipJis
            '    dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis '追加箇所 20110222
            '    'マスタ更新対象フラグ
            '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            'ElseIf choiceKb.Equals("02") = True Then
            '    'ワーニングで"いいえ"を選択時
            'End If

            dtMdest.Rows(0).Item("JIS") = mZipJis
            dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis '追加箇所 20110222
            'マスタ更新対象フラグ
            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            '要望番号954 修正END ワーニング表示させずに、自動更新を行う

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
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)

        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        Dim compareWarningFlg As Boolean = False

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If
            '2012.03.01 大阪対応END

        End If

        If String.IsNullOrEmpty(mZipJis) = True Then

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            '2012.03.01 大阪対応START
            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If
            '2012.03.01 大阪対応END

        End If
        '2012.03.28 要望番号948 修正END

        choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.DSP_WID_L012, 0)

        If String.IsNullOrEmpty(choiceKb) = True Then

            'ワーニング設定する
            msgArray(1) = workDestString
            msgArray(2) = "届先マスタ"
            msgArray(3) = "EDIデータ"
            If String.IsNullOrEmpty(mZipJis) = False Then
                msgArray(4) = String.Empty
            Else
                msgArray(4) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
            End If

            '2012.04.04 要望番号:943 修正 Start
            ds = Me._Blc.SetComWarningL("W186", LMH030BLC.DSP_WID_L012, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222
            '2012.04.04 要望番号:943 修正 End

            compareWarningFlg = True

            'Return True

        ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            Dim drMD As DataRow = dtMdest.NewRow()
            drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
            drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
            drMD("DEST_CD") = workDestCd
            drMD("EDI_CD") = workDestCd
            If String.IsNullOrEmpty(drEdiL("DEST_NM").ToString()) = False Then
                drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
            End If
            '2012.03.22 修正START
            drMD("ZIP") = Replace(drEdiL("DEST_ZIP").ToString(), "-", String.Empty)
            '2012.03.22 修正START
            drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
            drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
            drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
            drMD("COA_YN") = "00"
            drMD("TEL") = drEdiL("DEST_TEL").ToString()

            '2012.03.01 大阪対応START
            drMD("JIS") = mZipJis
            '2012.03.01 大阪対応END
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
            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
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
            outkaDr("DEST_KB") = "02"
            outkaDr("DEST_NM") = ediDr("DEST_NM")
            outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")

            ''現場要望(要望番号922 2012.03.29 追加START(まとめ処理：荷主注文番号連結)

            Dim strCustOrder As String = "CUST_ORD_NO"
            Dim byteCnt As Integer = 30

            '現場要望(要望番号851) 荷主注文番号もまとめの場合は足し込む
            '②今からまとめるEDI出荷(大)の荷主注文番号とまとめられる出荷(大)の荷主注文番号のチェック

            '②-1 EDI出荷(大)の荷主注文番号が空の場合は、まとめられる出荷(大)の荷主注文番号をそのまま使用
            If String.IsNullOrEmpty(ediDr(strCustOrder).ToString()) = True Then
                outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)

                '②-2 出荷(大)の荷主注文番号が空の場合は、今からまとめるEDI出荷(大)の荷主注文番号を付与
            ElseIf String.IsNullOrEmpty(matomesakiDt.Rows(0)(strCustOrder).ToString()) = True Then
                outkaDr(strCustOrder) = ediDr(strCustOrder)

                '②-3 出荷(大)の荷主注文番号が空でない場合で、今からまとめるEDI出荷(大)の荷主注文番号が同じ場合は、
                '出荷(大)の荷主注文番号をそのまま使用
            ElseIf String.IsNullOrEmpty(matomesakiDt.Rows(0)(strCustOrder).ToString()) = False AndAlso _
                   (matomesakiDt.Rows(0)(strCustOrder).ToString()).Equals(ediDr(strCustOrder).ToString().Trim()) = True Then
                outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)

                '②-4 出荷(大)の荷主注文番号にまとめるEDI出荷(大)の荷主注文番号が含まれていない場合は、
                '出荷(大)の荷主注文番号に、まとめるEDI出荷(大)の荷主注文番号を付与
            ElseIf InStr(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), ediDr(strCustOrder).ToString().Trim()) = 0 Then
                outkaDr(strCustOrder) = Me._Blc.LeftB(String.Concat(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), Space(1), ediDr(strCustOrder).ToString().Trim()), byteCnt)

                '②-5 出荷(大)の荷主注文番号にまとめるEDI出荷(大)の荷主注文番号が含まれている場合は、
                '出荷(大)の荷主注文番号をそのまま使用
            ElseIf InStr(matomesakiDt.Rows(0)(strCustOrder).ToString().Trim(), ediDr(strCustOrder).ToString().Trim()) > 0 Then
                outkaDr(strCustOrder) = matomesakiDt.Rows(0)(strCustOrder)
            End If

            ''要望番号922 2012.03.29 追加END

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

#Region "セミEDI時　データセット設定 CSV"

#Region "セミEDI時　データセット設定(EDI受信DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiEdiRcvDtlDspComCsv(ByVal ds As DataSet, ByVal i As Integer) As DataSet


        Dim semiInfoDr As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        Dim rcvDtlDr As DataRow
        Dim toriDtlDr As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        rcvDtlDr = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").NewRow()

        rcvDtlDr("DEL_KB") = "0"
        rcvDtlDr("CRT_DATE") = MyBase.GetSystemDate()
        rcvDtlDr("FILE_NAME") = toriDtlDr("FILE_NAME_OPE")
        rcvDtlDr("REC_NO") = Convert.ToInt32(toriDtlDr("REC_NO")).ToString("00000")
        rcvDtlDr("NRS_BR_CD") = semiInfoDr("NRS_BR_CD")
        rcvDtlDr("EDI_CTL_NO") = String.Empty
        rcvDtlDr("EDI_CTL_NO_CHU") = String.Empty

        '営業所コードにより「OUTKA_CTL_NO」を振り分ける
        If rcvDtlDr("NRS_BR_CD").ToString = "10" Then
            '千葉
            rcvDtlDr("OUTKA_CTL_NO") = "C00000000"
        ElseIf rcvDtlDr("NRS_BR_CD").ToString = "20" Then
            '大阪
            rcvDtlDr("OUTKA_CTL_NO") = "T00000000"
        Else
            '千葉、大阪以外（横浜）
            rcvDtlDr("OUTKA_CTL_NO") = "C00000000"
        End If

        rcvDtlDr("OUTKA_CTL_NO_CHU") = "000"
        rcvDtlDr("PRTFLG") = "0"

        rcvDtlDr("IDO_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_1").ToString().Trim(), 30)                 '移動番号
        rcvDtlDr("IDO_DATE") = Me._Blc.LeftB(toriDtlDr("COLUMN_2").ToString().Trim(), 10)               '移動日
        rcvDtlDr("IDO_KB") = Me._Blc.LeftB(toriDtlDr("COLUMN_3").ToString().Trim(), 3)                  '移動区分
        rcvDtlDr("IDO_KB_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_4").ToString().Trim(), 30)              '移動区分名
        rcvDtlDr("ARR_PLAN_DATE") = Me._Blc.LeftB(toriDtlDr("COLUMN_5").ToString().Trim(), 10)          '入荷予定日
        rcvDtlDr("REMARK") = Me._Blc.LeftB(toriDtlDr("COLUMN_6").ToString().Trim(), 100)                '伝票適用
        rcvDtlDr("DEST_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_7").ToString().Trim(), 15)                '移動先コード
        rcvDtlDr("DEST_ZIP") = Me._Blc.LeftB(toriDtlDr("COLUMN_8").ToString().Trim(), 10)               '移動先郵便番号
        rcvDtlDr("DEST_AD_1") = Me._Blc.LeftB(toriDtlDr("COLUMN_9").ToString().Trim(), 40)              '移動先住所１
        rcvDtlDr("DEST_AD_2") = Me._Blc.LeftB(toriDtlDr("COLUMN_10").ToString().Trim(), 40)             '移動先住所２
        rcvDtlDr("DEST_TEL") = Me._Blc.LeftB(toriDtlDr("COLUMN_11").ToString().Trim(), 20)              '移動先電話番号
        rcvDtlDr("DEST_FAX") = Me._Blc.LeftB(toriDtlDr("COLUMN_12").ToString().Trim(), 15)              '移動先FAX番号
        rcvDtlDr("DEST_NM1") = Me._Blc.LeftB(toriDtlDr("COLUMN_13").ToString().Trim(), 80)              '移動先名１
        rcvDtlDr("DEST_NM2") = Me._Blc.LeftB(toriDtlDr("COLUMN_14").ToString().Trim(), 80)              '移動先名２
        rcvDtlDr("DEST_NM3") = Me._Blc.LeftB(toriDtlDr("COLUMN_15").ToString().Trim(), 80)              '移動先名３
        rcvDtlDr("IDO_MOTO_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_16").ToString().Trim(), 10)           '移動元コード
        rcvDtlDr("IDO_MOTO_ZIP") = Me._Blc.LeftB(toriDtlDr("COLUMN_17").ToString().Trim(), 10)          '移動元郵便番号
        rcvDtlDr("IDO_MOTO_AD_1") = Me._Blc.LeftB(toriDtlDr("COLUMN_18").ToString().Trim(), 40)         '移動元住所１
        rcvDtlDr("IDO_MOTO_AD_2") = Me._Blc.LeftB(toriDtlDr("COLUMN_19").ToString().Trim(), 40)         '移動元住所２
        rcvDtlDr("IDO_MOTO_TEL") = Me._Blc.LeftB(toriDtlDr("COLUMN_20").ToString().Trim(), 20)          '移動元電話番号
        rcvDtlDr("IDO_MOTO_FAX") = Me._Blc.LeftB(toriDtlDr("COLUMN_21").ToString().Trim(), 15)          '移動元FAX番号
        rcvDtlDr("IDO_MOTO_NM1") = Me._Blc.LeftB(toriDtlDr("COLUMN_22").ToString().Trim(), 80)          '移動元名１
        rcvDtlDr("IDO_MOTO_NM2") = Me._Blc.LeftB(toriDtlDr("COLUMN_23").ToString().Trim(), 80)          '移動元名２
        rcvDtlDr("IDO_MOTO_NM3") = Me._Blc.LeftB(toriDtlDr("COLUMN_24").ToString().Trim(), 80)          '移動元名３
        rcvDtlDr("BLANK1") = Me._Blc.LeftB(toriDtlDr("COLUMN_25").ToString().Trim(), 100)               'ブランク１
        rcvDtlDr("BLANK2") = Me._Blc.LeftB(toriDtlDr("COLUMN_26").ToString().Trim(), 100)               'ブランク２
        rcvDtlDr("BLANK3") = Me._Blc.LeftB(toriDtlDr("COLUMN_27").ToString().Trim(), 100)               'ブランク３
        rcvDtlDr("CUST_GOODS_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_28").ToString().Trim(), 20)         '商品コード
        rcvDtlDr("GOODS_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_29").ToString().Trim(), 60)              '商品名
        rcvDtlDr("STANDARD") = Me._Blc.LeftB(toriDtlDr("COLUMN_30").ToString().Trim(), 60)              '規格
        rcvDtlDr("PROPERTY_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_31").ToString().Trim(), 60)           '商品変動特性数値
        rcvDtlDr("PROPERTY_NO_UNIT") = Me._Blc.LeftB(toriDtlDr("COLUMN_32").ToString().Trim(), 60)      '商品変動特性数値単位
        rcvDtlDr("GOODS_RANK") = Me._Blc.LeftB(toriDtlDr("COLUMN_33").ToString().Trim(), 60)            '商品ランク
        rcvDtlDr("CTR_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_34").ToString().Trim(), 30)                '管理NO
        rcvDtlDr("ASTERRISK") = Me._Blc.LeftB(toriDtlDr("COLUMN_35").ToString().Trim(), 60)             '*
        rcvDtlDr("IDO_MOTO_TANABAN") = Me._Blc.LeftB(toriDtlDr("COLUMN_36").ToString().Trim(), 60)      '移動元棚番コード
        rcvDtlDr("IDO_SAKI_TANABAN") = Me._Blc.LeftB(toriDtlDr("COLUMN_37").ToString().Trim(), 60)      '移動先棚番コード
        rcvDtlDr("IRIME") = Me._Blc.LeftB(toriDtlDr("COLUMN_38").ToString().Trim(), 9)                  '入数
        rcvDtlDr("LOAD_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_39").ToString().Trim(), 10)               '荷数
        rcvDtlDr("BARA_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_40").ToString().Trim(), 10)               'バラ数
        rcvDtlDr("PACK_UT_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_41").ToString().Trim(), 10)            '荷姿単位名
        rcvDtlDr("BARA_UT_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_42").ToString().Trim(), 10)            'バラ単位名
        rcvDtlDr("OUTKA_QT") = Me._Blc.LeftB(toriDtlDr("COLUMN_43").ToString().Trim(), 12)              '数量
        rcvDtlDr("QT_UT") = Me._Blc.LeftB(toriDtlDr("COLUMN_44").ToString().Trim(), 2)                  '数量単位
        rcvDtlDr("ASSET_QT") = Me._Blc.LeftB(toriDtlDr("COLUMN_45").ToString().Trim(), 30)              '資産数量
        rcvDtlDr("ASSET_QT_UT") = Me._Blc.LeftB(toriDtlDr("COLUMN_46").ToString().Trim(), 30)           '資産数量単位
        rcvDtlDr("IDO_TANKA") = Me._Blc.LeftB(toriDtlDr("COLUMN_47").ToString().Trim(), 30)             '移動単価
        rcvDtlDr("IDO_KINGAKU") = Me._Blc.LeftB(toriDtlDr("COLUMN_48").ToString().Trim(), 30)           '移動金額
        rcvDtlDr("DTL_APL") = Me._Blc.LeftB(toriDtlDr("COLUMN_49").ToString().Trim(), 30)               '明細摘要
        rcvDtlDr("TANZAI_FLG") = Me._Blc.LeftB(toriDtlDr("COLUMN_50").ToString().Trim(), 1)             '端材フラグ
        rcvDtlDr("IDO_TOTAL") = Me._Blc.LeftB(toriDtlDr("COLUMN_51").ToString().Trim(), 30)             '移動金額計
        rcvDtlDr("ASSET_OWNER_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_52").ToString().Trim(), 100)       '資産所有者コード
        rcvDtlDr("ASSET_OWNER_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_53").ToString().Trim(), 100)       '資産所有者名
        rcvDtlDr("LOT_BIKOU1") = Me._Blc.LeftB(toriDtlDr("COLUMN_54").ToString().Trim(), 100)           'LotBikou1
        rcvDtlDr("LOT_BIKOU2") = Me._Blc.LeftB(toriDtlDr("COLUMN_55").ToString().Trim(), 100)           'LotBikou2
        rcvDtlDr("DENPYOU_GENKA_KINGAKU") = Me._Blc.LeftB(toriDtlDr("COLUMN_56").ToString().Trim(), 100)           'DenpyouGenkaKingaku
        rcvDtlDr("ZAIKO_BASHO_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_57").ToString().Trim(), 100)       'ZaikoBashoCD
        rcvDtlDr("ZAIKO_BASHO_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_58").ToString().Trim(), 100)       'ZaikoBashoName

        rcvDtlDr("JISSEKI_SHORI_FLG") = "1"                                                             '実績処理フラグ

        'データセットに設定
        ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows.Add(rcvDtlDr)

        Return ds

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(中))"

    ''' <summary>
    ''' データセット設定(EDI出荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiMCsv(ByVal setDs As DataSet) As DataSet

        Dim outkaediMDr As DataRow

        Dim rcvDtlDr As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows(0)
        Dim semiEdiDr As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        outkaediMDr = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()

        outkaediMDr("DEL_KB") = "0"
        outkaediMDr("NRS_BR_CD") = semiEdiDr("NRS_BR_CD")
        outkaediMDr("EDI_CTL_NO") = rcvDtlDr("EDI_CTL_NO")
        outkaediMDr("EDI_CTL_NO_CHU") = rcvDtlDr("EDI_CTL_NO_CHU")
        outkaediMDr("OUTKA_CTL_NO") = String.Empty
        outkaediMDr("OUTKA_CTL_NO_CHU") = String.Empty
        outkaediMDr("COA_YN") = String.Empty

        outkaediMDr("CUST_ORD_NO_DTL") = String.Empty
        outkaediMDr("BUYER_ORD_NO_DTL") = String.Empty
        outkaediMDr("CUST_GOODS_CD") = rcvDtlDr("CUST_GOODS_CD")
        outkaediMDr("NRS_GOODS_CD") = String.Empty
        outkaediMDr("GOODS_NM") = rcvDtlDr("GOODS_NM")

        outkaediMDr("RSV_NO") = String.Empty
        outkaediMDr("LOT_NO") = String.Empty
        outkaediMDr("SERIAL_NO") = String.Empty
        outkaediMDr("ALCTD_KB") = "01"

        outkaediMDr("OUTKA_PKG_NB") = String.Empty
        outkaediMDr("OUTKA_HASU") = String.Empty
        outkaediMDr("OUTKA_QT") = rcvDtlDr("OUTKA_QT")
        outkaediMDr("OUTKA_TTL_NB") = Convert.ToDouble(rcvDtlDr("LOAD_NO"))
        outkaediMDr("OUTKA_TTL_QT") = Convert.ToDecimal(rcvDtlDr("IRIME").ToString) * Convert.ToDecimal(rcvDtlDr("LOAD_NO").ToString)
        outkaediMDr("KB_UT") = String.Empty
        outkaediMDr("QT_UT") = String.Empty
        outkaediMDr("PKG_NB") = String.Empty
        outkaediMDr("PKG_UT") = String.Empty
        outkaediMDr("ONDO_KB") = String.Empty
        outkaediMDr("UNSO_ONDO_KB") = String.Empty
        outkaediMDr("IRIME") = rcvDtlDr("IRIME")
        outkaediMDr("IRIME_UT") = String.Empty
        outkaediMDr("BETU_WT") = 0
        outkaediMDr("REMARK") = rcvDtlDr("REMARK")
        outkaediMDr("OUT_KB") = "0"
        outkaediMDr("AKAKURO_KB") = "0"
        outkaediMDr("JISSEKI_FLAG") = "0"
        outkaediMDr("JISSEKI_USER") = String.Empty
        outkaediMDr("JISSEKI_DATE") = String.Empty
        outkaediMDr("JISSEKI_TIME") = String.Empty
        outkaediMDr("SET_KB") = "0"
        outkaediMDr("FREE_N01") = 0
        outkaediMDr("FREE_N02") = 0
        outkaediMDr("FREE_N03") = 0
        outkaediMDr("FREE_N04") = 0
        outkaediMDr("FREE_N05") = 0
        outkaediMDr("FREE_N06") = 0
        outkaediMDr("FREE_N07") = 0
        outkaediMDr("FREE_N08") = 0
        outkaediMDr("FREE_N09") = 0
        outkaediMDr("FREE_N10") = 0
        outkaediMDr("FREE_C01") = rcvDtlDr("GOODS_RANK")
        outkaediMDr("FREE_C02") = rcvDtlDr("ASSET_OWNER_CD")
        outkaediMDr("FREE_C03") = String.Empty
        outkaediMDr("FREE_C04") = String.Empty
        outkaediMDr("FREE_C05") = String.Empty
        outkaediMDr("FREE_C06") = String.Empty
        outkaediMDr("FREE_C07") = String.Empty
        outkaediMDr("FREE_C08") = String.Empty
        outkaediMDr("FREE_C09") = String.Empty
        outkaediMDr("FREE_C10") = String.Empty
        outkaediMDr("FREE_C11") = String.Empty
        outkaediMDr("FREE_C12") = String.Empty
        outkaediMDr("FREE_C13") = String.Empty
        outkaediMDr("FREE_C14") = String.Empty
        outkaediMDr("FREE_C15") = String.Empty
        outkaediMDr("FREE_C16") = String.Empty
        outkaediMDr("FREE_C17") = String.Empty
        outkaediMDr("FREE_C18") = String.Empty
        outkaediMDr("FREE_C19") = String.Empty
        outkaediMDr("FREE_C20") = String.Empty
        outkaediMDr("FREE_C21") = String.Empty
        outkaediMDr("FREE_C22") = String.Empty
        outkaediMDr("FREE_C23") = String.Empty
        outkaediMDr("FREE_C24") = String.Empty
        outkaediMDr("FREE_C25") = String.Empty
        outkaediMDr("FREE_C26") = String.Empty
        outkaediMDr("FREE_C27") = String.Empty
        outkaediMDr("FREE_C28") = String.Empty
        outkaediMDr("FREE_C29") = String.Empty
        outkaediMDr("FREE_C30") = String.Empty

        'DAC側で設定
        'outkaediMDr("CRT_USER") = String.Empty
        'outkaediMDr("CRT_DATE") = String.Empty
        'outkaediMDr("CRT_TIME") = String.Empty
        'outkaediMDr("UPD_USER") = String.Empty
        'outkaediMDr("UPD_DATE") = String.Empty
        'outkaediMDr("UPD_TIME") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(outkaediMDr)

        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(大))"

    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiLCsv(ByVal setDs As DataSet) As DataSet

        Dim outkaediLDr As DataRow
        Dim rcvDtlDr As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows(0)
        Dim semiEdiDr As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        outkaediLDr = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        '荷主Index
        Dim ediCustIndex As String = semiEdiDr.Item("EDI_CUST_INDEX").ToString()

        outkaediLDr("DEL_KB") = "0"
        outkaediLDr("NRS_BR_CD") = semiEdiDr("NRS_BR_CD")
        outkaediLDr("EDI_CTL_NO") = rcvDtlDr("EDI_CTL_NO")
        outkaediLDr("OUTKA_CTL_NO") = String.Empty
        outkaediLDr("OUTKA_KB") = "10"
        outkaediLDr("SYUBETU_KB") = "10"
        outkaediLDr("NAIGAI_KB") = String.Empty
        outkaediLDr("OUTKA_STATE_KB") = "10"
        outkaediLDr("OUTKAHOKOKU_YN") = String.Empty
        outkaediLDr("PICK_KB") = "01"
        outkaediLDr("NRS_BR_NM") = String.Empty
        outkaediLDr("WH_CD") = semiEdiDr("WH_CD")
        outkaediLDr("WH_NM") = String.Empty
        outkaediLDr("OUTKA_PLAN_DATE") = CDate(rcvDtlDr("IDO_DATE").ToString).ToString("yyyyMMdd")
        outkaediLDr("OUTKO_DATE") = CDate(rcvDtlDr("IDO_DATE").ToString).ToString("yyyyMMdd")
        outkaediLDr("ARR_PLAN_DATE") = CDate(rcvDtlDr("ARR_PLAN_DATE").ToString).ToString("yyyyMMdd")
        outkaediLDr("ARR_PLAN_TIME") = String.Empty
        outkaediLDr("HOKOKU_DATE") = String.Empty
        outkaediLDr("TOUKI_HOKAN_YN") = String.Empty
        outkaediLDr("CUST_CD_L") = semiEdiDr("CUST_CD_L")
        outkaediLDr("CUST_CD_M") = semiEdiDr("CUST_CD_M")
        outkaediLDr("CUST_NM_L") = String.Empty
        outkaediLDr("CUST_NM_M") = String.Empty
        outkaediLDr("SHIP_CD_L") = String.Empty
        outkaediLDr("SHIP_CD_M") = String.Empty
        outkaediLDr("SHIP_NM_L") = String.Empty
        outkaediLDr("SHIP_NM_M") = String.Empty
        outkaediLDr("EDI_DEST_CD") = rcvDtlDr("DEST_CD")
        outkaediLDr("DEST_CD") = rcvDtlDr("DEST_CD")
        outkaediLDr("DEST_NM") = rcvDtlDr("DEST_NM2")
        outkaediLDr("DEST_ZIP") = rcvDtlDr("DEST_ZIP")
        outkaediLDr("DEST_AD_1") = rcvDtlDr("DEST_AD_1")
        outkaediLDr("DEST_AD_2") = rcvDtlDr("DEST_AD_2")
        outkaediLDr("DEST_AD_3") = String.Empty
        outkaediLDr("DEST_AD_4") = String.Empty
        outkaediLDr("DEST_AD_5") = String.Empty
        outkaediLDr("DEST_TEL") = rcvDtlDr("DEST_TEL")
        outkaediLDr("DEST_FAX") = rcvDtlDr("DEST_FAX")
        outkaediLDr("DEST_MAIL") = String.Empty
        outkaediLDr("DEST_JIS_CD") = String.Empty
        outkaediLDr("SP_NHS_KB") = String.Empty
        outkaediLDr("COA_YN") = String.Empty
        outkaediLDr("CUST_ORD_NO") = rcvDtlDr("IDO_NO")
        outkaediLDr("BUYER_ORD_NO") = String.Empty

        '運送元区分は出荷時に判断されるので必要なし
        'Todo)
        'Annen - まだタリフ等の情報設定が未確定なので、取り合えず運送手配区分には"未設定"を設定する
        outkaediLDr("UNSO_MOTO_KB") = "90"
        'outkaediLDr("UNSO_MOTO_KB") = String.Empty
        '--------
        outkaediLDr("UNSO_TEHAI_KB") = String.Empty
        outkaediLDr("SYARYO_KB") = String.Empty
        outkaediLDr("BIN_KB") = String.Empty
        outkaediLDr("UNSO_CD") = String.Empty
        outkaediLDr("UNSO_NM") = String.Empty
        outkaediLDr("UNSO_BR_CD") = String.Empty
        outkaediLDr("UNSO_BR_NM") = String.Empty
        outkaediLDr("UNCHIN_TARIFF_CD") = String.Empty
        outkaediLDr("EXTC_TARIFF_CD") = String.Empty

        ''注意事項
        outkaediLDr("REMARK") = rcvDtlDr("REMARK")

        outkaediLDr("UNSO_ATT") = String.Empty
        outkaediLDr("DENP_YN") = "1"
        outkaediLDr("PC_KB") = String.Empty
        outkaediLDr("UNCHIN_YN") = String.Empty
        outkaediLDr("NIYAKU_YN") = String.Empty
        outkaediLDr("OUT_FLAG") = "0"
        outkaediLDr("AKAKURO_KB") = "0"
        outkaediLDr("JISSEKI_FLAG") = "0"
        outkaediLDr("JISSEKI_USER") = String.Empty
        outkaediLDr("JISSEKI_DATE") = String.Empty
        outkaediLDr("JISSEKI_TIME") = String.Empty
        outkaediLDr("FREE_N01") = 0
        outkaediLDr("FREE_N02") = 0
        outkaediLDr("FREE_N03") = 0
        outkaediLDr("FREE_N04") = 0
        outkaediLDr("FREE_N05") = 0
        outkaediLDr("FREE_N06") = 0
        outkaediLDr("FREE_N07") = 0
        outkaediLDr("FREE_N08") = 0
        outkaediLDr("FREE_N09") = 0
        outkaediLDr("FREE_N10") = 0
        outkaediLDr("FREE_C01") = "CSV"
        outkaediLDr("FREE_C02") = String.Empty
        outkaediLDr("FREE_C03") = String.Empty
        outkaediLDr("FREE_C04") = String.Empty
        outkaediLDr("FREE_C05") = String.Empty
        outkaediLDr("FREE_C06") = String.Empty
        outkaediLDr("FREE_C07") = String.Empty
        outkaediLDr("FREE_C08") = String.Empty
        outkaediLDr("FREE_C09") = String.Empty
        outkaediLDr("FREE_C10") = String.Empty
        outkaediLDr("FREE_C11") = String.Empty
        outkaediLDr("FREE_C12") = String.Empty
        outkaediLDr("FREE_C13") = String.Empty
        outkaediLDr("FREE_C14") = String.Empty
        outkaediLDr("FREE_C15") = String.Empty
        outkaediLDr("FREE_C16") = String.Empty
        outkaediLDr("FREE_C17") = String.Empty
        outkaediLDr("FREE_C18") = String.Empty
        outkaediLDr("FREE_C19") = String.Empty
        outkaediLDr("FREE_C20") = String.Empty
        outkaediLDr("FREE_C21") = String.Empty
        outkaediLDr("FREE_C22") = String.Empty
        outkaediLDr("FREE_C23") = String.Empty
        outkaediLDr("FREE_C24") = String.Empty
        outkaediLDr("FREE_C25") = String.Empty
        outkaediLDr("FREE_C26") = String.Empty
        outkaediLDr("FREE_C27") = String.Empty
        outkaediLDr("FREE_C28") = String.Empty
        outkaediLDr("FREE_C29") = String.Empty
        outkaediLDr("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(outkaediLDr)
        Return setDs

    End Function

#End Region

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNoCsv(ByVal ds As DataSet, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        Dim rcvDtldt As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC")
        Dim rcvDtldr As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows(0).Item("NRS_BR_CD").ToString()

        'レコードが１行目または送られてきたCSVの移動番号(カラム１列目)が前行と差異がある場合
        If bSameKeyFlg = False Then

            'EDI管理番号(大)を新規採番＋EDI管理番号、(中)を"001"採番
            Dim num As New NumberMasterUtility
            sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, nrsBrCd)

            iEdiCtlNoChu = 1    '1から開始
        Else

            'EDI管理番号(大)は前行と同じ番号をセット
            '処理なし

            'EDI管理番号(中)をカウントアップ
            iEdiCtlNoChu = iEdiCtlNoChu + 1
        End If

        '登録用EDI管理番号
        rcvDtldt.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo
        rcvDtldt.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")

        Return ds

    End Function

#End Region

#End Region

#Region "セミEDI時　データセット設定 EXCEL"

#Region "セミEDI時　データセット設定(EDI受信DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiEdiRcvDtlDspComExcel(ByVal ds As DataSet, ByVal i As Integer) As DataSet


        Dim semiInfoDr As DataRow = ds.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        Dim rcvDtlDr As DataRow
        Dim toriDtlDr As DataRow = ds.Tables("LMH030_EDI_TORIKOMI_DTL").Rows(i)

        rcvDtlDr = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").NewRow()

        rcvDtlDr("DEL_KB") = "0"
        rcvDtlDr("CRT_DATE") = MyBase.GetSystemDate()
        rcvDtlDr("FILE_NAME") = toriDtlDr("FILE_NAME_OPE")
        rcvDtlDr("REC_NO") = Convert.ToInt32(toriDtlDr("REC_NO")).ToString("00000")
        rcvDtlDr("NRS_BR_CD") = semiInfoDr("NRS_BR_CD")
        rcvDtlDr("EDI_CTL_NO") = String.Empty
        rcvDtlDr("EDI_CTL_NO_CHU") = String.Empty
        rcvDtlDr("OUTKA_CTL_NO") = "C00000000"
        rcvDtlDr("OUTKA_CTL_NO_CHU") = "000"
        rcvDtlDr("PRTFLG") = "0"

        rcvDtlDr("OUTKA_PLAN_DATE") = Me._Blc.LeftB(toriDtlDr("COLUMN_1").ToString().Trim(), 10)                '出荷指示日
        rcvDtlDr("OUTKA_DTE") = Me._Blc.LeftB(toriDtlDr("COLUMN_2").ToString().Trim(), 10)                      '出荷日
        rcvDtlDr("CUST_ORD_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_3").ToString().Trim(), 30)                    '出荷指示番号
        rcvDtlDr("OFFICE_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_4").ToString().Trim(), 80)                      '事業所名
        rcvDtlDr("PERSON_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_5").ToString().Trim(), 80)                      '担当者名
        rcvDtlDr("GROUP_TEL") = Me._Blc.LeftB(toriDtlDr("COLUMN_6").ToString().Trim(), 20)                      '部門電話番号
        rcvDtlDr("GROUP_FAX") = Me._Blc.LeftB(toriDtlDr("COLUMN_7").ToString().Trim(), 15)                      '部門FAX番号
        rcvDtlDr("CUSOMER_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_8").ToString().Trim(), 30)                     '得意先コード
        rcvDtlDr("CUSOMER_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_9").ToString().Trim(), 80)                     '得意先名
        rcvDtlDr("DEST_ZIP") = Me._Blc.LeftB(toriDtlDr("COLUMN_10").ToString().Trim(), 10)                      '納入先郵便番号
        rcvDtlDr("DEST_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_11").ToString().Trim(), 15)                       '納入先コード
        rcvDtlDr("DEST_NM1") = Me._Blc.LeftB(toriDtlDr("COLUMN_12").ToString().Trim(), 80)                      '納入先名１
        rcvDtlDr("DEST_NM2") = Me._Blc.LeftB(toriDtlDr("COLUMN_13").ToString().Trim(), 60)                      '納入先名２
        rcvDtlDr("DEST_TEL") = Me._Blc.LeftB(toriDtlDr("COLUMN_14").ToString().Trim(), 20)                      '納入先電話番号
        rcvDtlDr("DEST_FAX") = Me._Blc.LeftB(toriDtlDr("COLUMN_15").ToString().Trim(), 15)                      '納入先FAX番号
        rcvDtlDr("DEST_AD_1") = Me._Blc.LeftB(toriDtlDr("COLUMN_16").ToString().Trim(), 40)                     '納入先住所１
        rcvDtlDr("DEST_AD_2") = Me._Blc.LeftB(toriDtlDr("COLUMN_17").ToString().Trim(), 40)                     '納入先住所２
        rcvDtlDr("DEST_AD_3") = String.Empty                                                                    '納入先住所３/納入先住所３は納入先名１と同じため、出力する帳票の見た目がおかしいために設定しないこととなった
        'rcvDtlDr("DEST_AD_3") = Me._Blc.LeftB(toriDtlDr("COLUMN_18").ToString().Trim(), 60)                     '納入先住所３
        rcvDtlDr("OFFICIAL_COMPANY_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_19").ToString().Trim(), 80)           '自社正式名称
        rcvDtlDr("COMPANY_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_20").ToString().Trim(), 80)                    '自社名称
        rcvDtlDr("WH_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_21").ToString().Trim(), 3)                          '倉庫コード
        rcvDtlDr("WH_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_22").ToString().Trim(), 50)                         '倉庫名
        rcvDtlDr("CUST_GOODS_CD") = Me._Blc.LeftB(toriDtlDr("COLUMN_23").ToString().Trim(), 20)                 '商品コード
        rcvDtlDr("GOODS_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_24").ToString().Trim(), 60)                      '商品名
        rcvDtlDr("STANDARD") = Me._Blc.LeftB(toriDtlDr("COLUMN_25").ToString().Trim(), 60)                      '規格
        rcvDtlDr("PROPERTY_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_26").ToString().Trim(), 60)                   '商品変動特性数値
        rcvDtlDr("PROPERTY_NO_UNIT") = Me._Blc.LeftB(toriDtlDr("COLUMN_27").ToString().Trim(), 60)              '商品変動特性数値単位
        rcvDtlDr("ORDER_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_28").ToString().Trim(), 30)                      '受注番号
        rcvDtlDr("ORDER_ROW_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_29").ToString().Trim(), 30)                  '受注行番号
        rcvDtlDr("TRADE_KB") = Me._Blc.LeftB(toriDtlDr("COLUMN_30").ToString().Trim(), 30)                      '取引区分名
        rcvDtlDr("GOODS_RANK_NM") = Me._Blc.LeftB(toriDtlDr("COLUMN_31").ToString().Trim(), 60)                 '商品ランク名
        rcvDtlDr("CTR_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_32").ToString().Trim(), 30)                        '管理NO
        rcvDtlDr("LOT_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_33").ToString().Trim(), 40)                        'ロットNO
        rcvDtlDr("IRIME") = Me._Blc.LeftB(toriDtlDr("COLUMN_34").ToString().Trim(), 9)                          '入数
        rcvDtlDr("LOAD_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_35").ToString().Trim(), 10)                       '荷数
        rcvDtlDr("KB_UT") = Me._Blc.LeftB(toriDtlDr("COLUMN_36").ToString().Trim(), 10)                         '荷姿単位
        rcvDtlDr("BARA_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_37").ToString().Trim(), 10)                       'バラ数
        rcvDtlDr("QUANTITY_UT") = Me._Blc.LeftB(toriDtlDr("COLUMN_38").ToString().Trim(), 10)                   '数量単位
        rcvDtlDr("TOTAL_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_39").ToString().Trim(), 10)                      '総数
        rcvDtlDr("REMARK") = Me._Blc.LeftB(toriDtlDr("COLUMN_40").ToString().Trim(), 100)                       '現品添付書類
        rcvDtlDr("UNSO_ATT") = Me._Blc.LeftB(toriDtlDr("COLUMN_41").ToString().Trim(), 100)                     '物流指定
        rcvDtlDr("HIKIATE_BIKOU") = Me._Blc.LeftB(toriDtlDr("COLUMN_42").ToString().Trim(), 100)                '在庫引当備考
        rcvDtlDr("ARR_PLAN_DATE") = Me._Blc.LeftB(toriDtlDr("COLUMN_43").ToString().Trim(), 10)                 '納品日
        rcvDtlDr("REMARK_M") = Me._Blc.LeftB(toriDtlDr("COLUMN_44").ToString().Trim(), 100)                     '備考
        rcvDtlDr("CUSTOMER_ORD_NO") = Me._Blc.LeftB(toriDtlDr("COLUMN_45").ToString().Trim(), 100)              '得意先注文番号

        rcvDtlDr("JISSEKI_SHORI_FLG") = "1"                                                             '実績処理フラグ

        'データセットに設定
        ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows.Add(rcvDtlDr)

        Return ds

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(中))"

    ''' <summary>
    ''' データセット設定(EDI出荷(中)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiMExcel(ByVal setDs As DataSet) As DataSet

        Dim outkaediMDr As DataRow
        Dim rcvDtlDr As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows(0)
        Dim semiEdiDr As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        outkaediMDr = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()

        outkaediMDr("DEL_KB") = "0"
        outkaediMDr("NRS_BR_CD") = semiEdiDr("NRS_BR_CD")
        outkaediMDr("EDI_CTL_NO") = rcvDtlDr("EDI_CTL_NO")
        outkaediMDr("EDI_CTL_NO_CHU") = rcvDtlDr("EDI_CTL_NO_CHU")
        outkaediMDr("OUTKA_CTL_NO") = String.Empty
        outkaediMDr("OUTKA_CTL_NO_CHU") = String.Empty
        outkaediMDr("COA_YN") = String.Empty

        outkaediMDr("CUST_ORD_NO_DTL") = String.Empty
        outkaediMDr("BUYER_ORD_NO_DTL") = rcvDtlDr("CUSTOMER_ORD_NO")
        outkaediMDr("CUST_GOODS_CD") = rcvDtlDr("CUST_GOODS_CD")
        outkaediMDr("NRS_GOODS_CD") = String.Empty
        outkaediMDr("GOODS_NM") = rcvDtlDr("GOODS_NM")
        outkaediMDr("RSV_NO") = String.Empty
        outkaediMDr("LOT_NO") = rcvDtlDr("LOT_NO")
        outkaediMDr("SERIAL_NO") = String.Empty
        outkaediMDr("ALCTD_KB") = "01"

        outkaediMDr("OUTKA_PKG_NB") = 0D
        outkaediMDr("OUTKA_HASU") = Convert.ToDouble(rcvDtlDr("LOAD_NO"))
        outkaediMDr("OUTKA_QT") = rcvDtlDr("TOTAL_NO")
        outkaediMDr("OUTKA_TTL_NB") = Convert.ToDouble(rcvDtlDr("LOAD_NO"))
        outkaediMDr("OUTKA_TTL_QT") = Convert.ToDecimal(rcvDtlDr("IRIME")) * Convert.ToDecimal(rcvDtlDr("LOAD_NO"))
        outkaediMDr("KB_UT") = String.Empty
        outkaediMDr("QT_UT") = String.Empty
        outkaediMDr("PKG_NB") = 1D
        outkaediMDr("PKG_UT") = String.Empty
        outkaediMDr("ONDO_KB") = String.Empty
        outkaediMDr("UNSO_ONDO_KB") = String.Empty
        outkaediMDr("IRIME") = rcvDtlDr("IRIME")
        outkaediMDr("IRIME_UT") = String.Empty
        outkaediMDr("BETU_WT") = 0
        outkaediMDr("REMARK") = String.Empty
        outkaediMDr("OUT_KB") = "0"
        outkaediMDr("AKAKURO_KB") = "0"
        outkaediMDr("JISSEKI_FLAG") = "0"
        outkaediMDr("JISSEKI_USER") = String.Empty
        outkaediMDr("JISSEKI_DATE") = String.Empty
        outkaediMDr("JISSEKI_TIME") = String.Empty
        outkaediMDr("SET_KB") = "0"
        outkaediMDr("FREE_N01") = 0
        outkaediMDr("FREE_N02") = 0
        outkaediMDr("FREE_N03") = 0
        outkaediMDr("FREE_N04") = 0
        outkaediMDr("FREE_N05") = 0
        outkaediMDr("FREE_N06") = 0
        outkaediMDr("FREE_N07") = 0
        outkaediMDr("FREE_N08") = 0
        outkaediMDr("FREE_N09") = 0
        outkaediMDr("FREE_N10") = 0
        outkaediMDr("FREE_C01") = rcvDtlDr("GOODS_RANK_NM")
        outkaediMDr("FREE_C02") = rcvDtlDr("REMARK_M")
        outkaediMDr("FREE_C03") = rcvDtlDr("CUSTOMER_ORD_NO")
        outkaediMDr("FREE_C04") = rcvDtlDr("UNSO_ATT")
        outkaediMDr("FREE_C05") = String.Empty
        outkaediMDr("FREE_C06") = String.Empty
        outkaediMDr("FREE_C07") = String.Empty
        outkaediMDr("FREE_C08") = String.Empty
        outkaediMDr("FREE_C09") = String.Empty
        outkaediMDr("FREE_C10") = String.Empty
        outkaediMDr("FREE_C11") = String.Empty
        outkaediMDr("FREE_C12") = String.Empty
        outkaediMDr("FREE_C13") = String.Empty
        outkaediMDr("FREE_C14") = String.Empty
        outkaediMDr("FREE_C15") = String.Empty
        outkaediMDr("FREE_C16") = String.Empty
        outkaediMDr("FREE_C17") = String.Empty
        outkaediMDr("FREE_C18") = String.Empty
        outkaediMDr("FREE_C19") = String.Empty
        outkaediMDr("FREE_C20") = String.Empty
        outkaediMDr("FREE_C21") = String.Empty
        outkaediMDr("FREE_C22") = String.Empty
        outkaediMDr("FREE_C23") = String.Empty
        outkaediMDr("FREE_C24") = String.Empty
        outkaediMDr("FREE_C25") = String.Empty
        outkaediMDr("FREE_C26") = String.Empty
        outkaediMDr("FREE_C27") = String.Empty
        outkaediMDr("FREE_C28") = String.Empty
        outkaediMDr("FREE_C29") = String.Empty
        outkaediMDr("FREE_C30") = String.Empty

        'DAC側で設定
        'outkaediMDr("CRT_USER") = String.Empty
        'outkaediMDr("CRT_DATE") = String.Empty
        'outkaediMDr("CRT_TIME") = String.Empty
        'outkaediMDr("UPD_USER") = String.Empty
        'outkaediMDr("UPD_DATE") = String.Empty
        'outkaediMDr("UPD_TIME") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(outkaediMDr)

        Return setDs

    End Function

#End Region

#Region "セミEDI時　データセット設定(EDI出荷(大))"

    ''' <summary>
    ''' データセット設定(EDI出荷(大)：セミEDI
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSemiOutkaEdiLExcel(ByVal setDs As DataSet) As DataSet

        Dim outkaediLDr As DataRow
        Dim rcvDtlDr As DataRow = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows(0)
        Dim semiEdiDr As DataRow = setDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)

        outkaediLDr = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()
        '荷主Index
        Dim ediCustIndex As String = semiEdiDr.Item("EDI_CUST_INDEX").ToString()

        outkaediLDr("DEL_KB") = "0"
        outkaediLDr("NRS_BR_CD") = semiEdiDr("NRS_BR_CD")
        outkaediLDr("EDI_CTL_NO") = rcvDtlDr("EDI_CTL_NO")
        outkaediLDr("OUTKA_CTL_NO") = String.Empty
        outkaediLDr("OUTKA_KB") = "10"
        outkaediLDr("SYUBETU_KB") = "10"
        outkaediLDr("NAIGAI_KB") = String.Empty
        outkaediLDr("OUTKA_STATE_KB") = "10"
        outkaediLDr("OUTKAHOKOKU_YN") = String.Empty
        outkaediLDr("PICK_KB") = "01"
        outkaediLDr("NRS_BR_NM") = String.Empty
        outkaediLDr("WH_CD") = semiEdiDr("WH_CD")
        outkaediLDr("WH_NM") = String.Empty
        outkaediLDr("OUTKA_PLAN_DATE") = CDate(rcvDtlDr("OUTKA_DTE").ToString).ToString("yyyyMMdd")
        outkaediLDr("OUTKO_DATE") = CDate(rcvDtlDr("OUTKA_DTE").ToString).ToString("yyyyMMdd")
        outkaediLDr("ARR_PLAN_DATE") = CDate(rcvDtlDr("ARR_PLAN_DATE").ToString).ToString("yyyyMMdd")
        outkaediLDr("ARR_PLAN_TIME") = String.Empty
        outkaediLDr("HOKOKU_DATE") = String.Empty
        outkaediLDr("TOUKI_HOKAN_YN") = String.Empty
        outkaediLDr("CUST_CD_L") = semiEdiDr("CUST_CD_L")
        outkaediLDr("CUST_CD_M") = semiEdiDr("CUST_CD_M")
        outkaediLDr("CUST_NM_L") = String.Empty
        outkaediLDr("CUST_NM_M") = String.Empty
        outkaediLDr("SHIP_CD_L") = String.Empty
        outkaediLDr("SHIP_CD_M") = String.Empty
        outkaediLDr("SHIP_NM_L") = String.Empty
        outkaediLDr("SHIP_NM_M") = String.Empty
        outkaediLDr("EDI_DEST_CD") = rcvDtlDr("DEST_CD")
        outkaediLDr("DEST_CD") = rcvDtlDr("DEST_CD")
        If String.IsNullOrEmpty(rcvDtlDr("DEST_NM2").ToString) = True Then
            outkaediLDr("DEST_NM") = rcvDtlDr("DEST_NM1")
        Else
            outkaediLDr("DEST_NM") = Left(rcvDtlDr("DEST_NM1").ToString & "　" & rcvDtlDr("DEST_NM2").ToString, 80)
        End If
        outkaediLDr("DEST_ZIP") = rcvDtlDr("DEST_ZIP")
        outkaediLDr("DEST_AD_1") = rcvDtlDr("DEST_AD_1")
        outkaediLDr("DEST_AD_2") = rcvDtlDr("DEST_AD_2")
        outkaediLDr("DEST_AD_3") = rcvDtlDr("DEST_AD_3")
        outkaediLDr("DEST_AD_4") = String.Empty
        outkaediLDr("DEST_AD_5") = String.Empty
        outkaediLDr("DEST_TEL") = rcvDtlDr("DEST_TEL")
        outkaediLDr("DEST_FAX") = rcvDtlDr("DEST_FAX")
        outkaediLDr("DEST_MAIL") = String.Empty
        outkaediLDr("DEST_JIS_CD") = String.Empty
        outkaediLDr("SP_NHS_KB") = String.Empty
        outkaediLDr("COA_YN") = String.Empty
        outkaediLDr("CUST_ORD_NO") = rcvDtlDr("CUST_ORD_NO")
        outkaediLDr("BUYER_ORD_NO") = rcvDtlDr("CUSTOMER_ORD_NO")

        '運送元区分は出荷時に判断されるので必要なし
        'Todo)
        'Annen - まだタリフ等の情報設定が未確定なので、取り合えず運送手配区分には"未設定"を設定する
        outkaediLDr("UNSO_MOTO_KB") = "90"
        'outkaediLDr("UNSO_MOTO_KB") = String.Empty
        '--------
        outkaediLDr("UNSO_TEHAI_KB") = String.Empty
        outkaediLDr("SYARYO_KB") = String.Empty
        outkaediLDr("BIN_KB") = String.Empty
        outkaediLDr("UNSO_CD") = String.Empty
        outkaediLDr("UNSO_NM") = String.Empty
        outkaediLDr("UNSO_BR_CD") = String.Empty
        outkaediLDr("UNSO_BR_NM") = String.Empty
        outkaediLDr("UNCHIN_TARIFF_CD") = String.Empty
        outkaediLDr("EXTC_TARIFF_CD") = String.Empty

        ''注意事項
        outkaediLDr("REMARK") = rcvDtlDr("REMARK")

        outkaediLDr("UNSO_ATT") = rcvDtlDr("UNSO_ATT")
        outkaediLDr("DENP_YN") = "1"
        outkaediLDr("PC_KB") = String.Empty
        outkaediLDr("UNCHIN_YN") = String.Empty
        outkaediLDr("NIYAKU_YN") = String.Empty
        outkaediLDr("OUT_FLAG") = "0"
        outkaediLDr("AKAKURO_KB") = "0"
        outkaediLDr("JISSEKI_FLAG") = "0"
        outkaediLDr("JISSEKI_USER") = String.Empty
        outkaediLDr("JISSEKI_DATE") = String.Empty
        outkaediLDr("JISSEKI_TIME") = String.Empty
        outkaediLDr("FREE_N01") = 0
        outkaediLDr("FREE_N02") = 0
        outkaediLDr("FREE_N03") = 0
        outkaediLDr("FREE_N04") = 0
        outkaediLDr("FREE_N05") = 0
        outkaediLDr("FREE_N06") = 0
        outkaediLDr("FREE_N07") = 0
        outkaediLDr("FREE_N08") = 0
        outkaediLDr("FREE_N09") = 0
        outkaediLDr("FREE_N10") = 0
        outkaediLDr("FREE_C01") = "EXCEL"
        outkaediLDr("FREE_C02") = String.Empty
        outkaediLDr("FREE_C03") = String.Empty
        outkaediLDr("FREE_C04") = String.Empty
        outkaediLDr("FREE_C05") = String.Empty
        outkaediLDr("FREE_C06") = String.Empty
        outkaediLDr("FREE_C07") = String.Empty
        outkaediLDr("FREE_C08") = String.Empty
        outkaediLDr("FREE_C09") = String.Empty
        outkaediLDr("FREE_C10") = String.Empty
        outkaediLDr("FREE_C11") = String.Empty
        outkaediLDr("FREE_C12") = String.Empty
        outkaediLDr("FREE_C13") = String.Empty
        outkaediLDr("FREE_C14") = String.Empty
        outkaediLDr("FREE_C15") = String.Empty
        outkaediLDr("FREE_C16") = String.Empty
        outkaediLDr("FREE_C17") = String.Empty
        outkaediLDr("FREE_C18") = String.Empty
        outkaediLDr("FREE_C19") = String.Empty
        outkaediLDr("FREE_C20") = String.Empty
        outkaediLDr("FREE_C21") = String.Empty
        outkaediLDr("FREE_C22") = String.Empty
        outkaediLDr("FREE_C23") = String.Empty
        outkaediLDr("FREE_C24") = String.Empty
        outkaediLDr("FREE_C25") = String.Empty
        outkaediLDr("FREE_C26") = String.Empty
        outkaediLDr("FREE_C27") = String.Empty
        outkaediLDr("FREE_C28") = String.Empty
        outkaediLDr("FREE_C29") = String.Empty
        outkaediLDr("FREE_C30") = String.Empty

        'データセットに設定
        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(outkaediLDr)
        Return setDs

    End Function

#End Region

#Region "セミEDI データセット設定(EDI管理番号(大・中))"

    ''' <summary>
    ''' データセット設定(EDI管理番号(大・中)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCtlNoExcel(ByVal ds As DataSet, ByVal bSameKeyFlg As Boolean _
                                , ByRef sEdiCtlNo As String, ByRef iEdiCtlNoChu As Integer) As DataSet

        Dim rcvDtldt As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE")
        Dim rcvDtldr As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows(0).Item("NRS_BR_CD").ToString()

        'レコードが１行目または送られてきたCSVの移動番号(カラム１列目)が前行と差異がある場合
        If bSameKeyFlg = False Then

            'EDI管理番号(大)を新規採番＋EDI管理番号、(中)を"001"採番
            Dim num As New NumberMasterUtility
            sEdiCtlNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, nrsBrCd)

            iEdiCtlNoChu = 1    '1から開始
        Else

            'EDI管理番号(大)は前行と同じ番号をセット
            '処理なし

            'EDI管理番号(中)をカウントアップ
            iEdiCtlNoChu = iEdiCtlNoChu + 1
        End If

        '登録用EDI管理番号
        rcvDtldt.Rows(0).Item("EDI_CTL_NO") = sEdiCtlNo
        rcvDtldt.Rows(0).Item("EDI_CTL_NO_CHU") = iEdiCtlNoChu.ToString("000")

        Return ds

    End Function

#End Region

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

#Region "画面取込(セミEDI)チェック処理メイン"
    Private Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        '取込ファイルの拡張子で取込処理を分岐する
        Dim toriDtlDt As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
        Dim filename() As String = toriDtlDt.Rows(0).Item("FILE_NAME_RCV").ToString.Split("."c)
        Dim extension As String = String.Empty
        If filename.Length > 1 Then
            extension = filename(filename.Length - 1)
        End If

        If "CSV".Equals(extension.ToUpper) Then
            'CSVファイル取込
            Return SemiEdiTorikomiChkCsv(ds)
        Else
            'エクセルファイル取込
            Return SemiEdiTorikomiChkExcel(ds)
        End If

    End Function

#End Region
    '2012.03.05 大阪対応START
#Region "画面取込(セミEDI)チェック処理CSV用"
    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChkCsv(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim ediCustIndex As String = dtSemiInfo.Rows(0).Item("EDI_CUST_INDEX").ToString()

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
                        If Me.TorikomiValChkCsv(dr, ediCustIndex) = False Then

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

#End Region
    '2012.03.05 大阪対応END
#Region "画面取込(セミEDI)チェック処理"
    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChkExcel(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim ediCustIndex As String = dtSemiInfo.Rows(0).Item("EDI_CUST_INDEX").ToString()

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
                        If Me.TorikomiValChkExcel(dr, ediCustIndex) = False Then

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

#End Region


    '2012.03.06 大阪対応START
#Region "画面取込(セミEDI)データセット＋更新処理"

#Region "取込メイン"
    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        '取込ファイルの拡張子で取込処理を分岐する
        Dim toriDtlDt As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
        Dim filename() As String = toriDtlDt.Rows(0).Item("FILE_NAME_RCV").ToString.Split("."c)
        Dim extension As String = String.Empty
        If filename.Length > 1 Then
            extension = filename(filename.Length - 1)
        End If

        If "CSV".Equals(extension.ToUpper) Then
            'CSVファイル取込
            Return SemiEdiTorikomiCsv(ds)
        Else
            'エクセルファイル取込
            '中間テーブル名称を変更する
            Return SemiEdiTorikomiExcel(ds)
        End If

    End Function

#End Region

#Region "CSV取込"
    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiCsv(ByVal ds As DataSet) As DataSet

        Dim setDtHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim toriDtlDt As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
        Dim rcvDtlDt As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC")
        Dim setSemiRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数

        Dim rowNo As String = String.Empty
        Dim cancelCnt As Integer = 0
        Dim mGoodsCnt As Integer = 0
        Dim max As Integer = toriDtlDt.Rows.Count - 1
        Dim sOldKey As String = String.Empty    'キー項目（前行）
        Dim sNewKey As String = String.Empty    'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False      '前行とキーが同じ場合True、異なる場合False
        Dim sEdiCtlNo As String = String.Empty  'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0         'EDI管理番号（中）
        Dim iRcvHedInsCnt As Integer = 0        '書込件数（受信Hed）
        Dim iRcvDtlInsCnt As Integer = 0        '書込件数（受信Dtl）
        Dim iOutHedInsCnt As Integer = 0        '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0        '書込件数（出荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0        '取消件数（受信Hed）
        Dim iRcvDtlCanCnt As Integer = 0        '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0        '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0        '取消件数（出荷EDI(中)）

        For i As Integer = 0 To max

            'セミEDI取込(共通)⇒受信DTLへのデータセット
            ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Clear() '受信DTLをクリア
            ds = Me.SetSemiEdiRcvDtlDspComCsv(ds, i)

            Dim rcvDtlDr As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKC").Rows(0)

            'キー項目設定
            If i = 0 Then
                '0番目は必ずbSameKeyFlgはFlase
                bSameKeyFlg = False
                sOldKey = rcvDtlDr.Item("IDO_NO").ToString()
            Else
                '1番目以降はキーを比較
                sNewKey = rcvDtlDr.Item("IDO_NO").ToString()
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                    sOldKey = sNewKey   'OldキーにNewキーをセット
                End If

            End If

            'EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            ds = Me.GetEdiCtlNoCsv(ds, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDt As DataTable = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKC")
            setDt.Clear()
            setDt.ImportRow(rcvDtlDt.Rows(0))

            'EDI受信(DTL)の新規追加
            '削除EDI番号をクリアする
            setDs = MyBase.CallDAC(Me._Dac, "InsertEdiRcvDtlDataCsv", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            '受信DTL⇒EDI出荷(中)へのデータセット
            '注)EDI出荷(中)は１レコード単位でセット
            setDs = Me.SetSemiOutkaEdiMCsv(setDs)

            'EDI出荷(中)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiMData", setDs)
            iOutDtlInsCnt = iOutDtlInsCnt + 1

            'EDI出荷(大) txtファイルの整理№(カラム８行目)が全行と差異がある場合は、新規追加
            If bSameKeyFlg = False Then
                '受信DTL⇒EDI出荷(大)へのデータセット
                setDs = Me.SetSemiOutkaEdiLCsv(setDs)

                '該当データに対するEDI出荷大テーブルの件数取得
                setDs = MyBase.CallDAC(Me._Dac, "SelectEdiLCount", setDs)

                '取得した件数が0件より多い場合、エラーメッセージ出力
                If Convert.ToInt32(setDs.Tables("DATA_COUNT").Rows(0).Item("DATA_COUNT").ToString) > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, _
                                           "E269", _
                                           New String() {String.Concat("移動番号[", setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_ORD_NO").ToString, "]")}, _
                                           toriDtlDt.Rows(i).Item("REC_NO").ToString(), _
                                           LMH030BLC.EXCEL_COLTITLE, _
                                           String.Empty)
                    Return ds
                End If

                'EDI出荷(大)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiLData", setDs)
                iOutHedInsCnt = iOutHedInsCnt + 1
            End If

            'End If

        Next

        'エラー無し
        setDtHed.Rows(0).Item("ERR_FLG") = "0"

        '処理件数
        setSemiRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function
#End Region

#Region "エクセル取込"
    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiExcel(ByVal ds As DataSet) As DataSet

        Dim setDtHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim toriDtlDt As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
        Dim rcvDtlDt As DataTable = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE")
        Dim setSemiRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数

        Dim rowNo As String = String.Empty
        Dim cancelCnt As Integer = 0
        Dim max As Integer = toriDtlDt.Rows.Count - 1
        Dim sOldKey As String = String.Empty    'キー項目（前行）
        Dim sNewKey As String = String.Empty    'キー項目（現在行）
        Dim bSameKeyFlg As Boolean = False      '前行とキーが同じ場合True、異なる場合False
        Dim sEdiCtlNo As String = String.Empty  'EDI管理番号
        Dim iEdiCtlNoChu As Integer = 0         'EDI管理番号（中）
        Dim iRcvHedInsCnt As Integer = 0        '書込件数（受信Hed）
        Dim iRcvDtlInsCnt As Integer = 0        '書込件数（受信Dtl）
        Dim iOutHedInsCnt As Integer = 0        '書込件数（出荷EDI(大)）
        Dim iOutDtlInsCnt As Integer = 0        '書込件数（出荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0        '取消件数（受信Hed）
        Dim iRcvDtlCanCnt As Integer = 0        '取消件数（受信Dtl）
        Dim iOutHedCanCnt As Integer = 0        '取消件数（出荷EDI(大)）
        Dim iOutDtlCanCnt As Integer = 0        '取消件数（出荷EDI(中)）


        For i As Integer = 0 To max

            'セミEDI取込(共通)⇒受信DTLへのデータセット
            ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Clear() '受信DTLをクリア
            ds = Me.SetSemiEdiRcvDtlDspComExcel(ds, i)

            Dim rcvDtlDr As DataRow = ds.Tables("LMH030_H_OUTKAEDI_DTL_GKE").Rows(0)

            'キー項目設定
            If i = 0 Then
                '0番目は必ずbSameKeyFlgはFlase
                bSameKeyFlg = False
                sOldKey = rcvDtlDr.Item("CUST_ORD_NO").ToString()
            Else
                '1番目以降はキーを比較
                sNewKey = rcvDtlDr.Item("CUST_ORD_NO").ToString()
                If sNewKey.Equals(sOldKey) = True Then
                    'キーが同一の場合
                    bSameKeyFlg = True
                Else
                    'キーが異なる場合
                    bSameKeyFlg = False
                    sOldKey = sNewKey   'OldキーにNewキーをセット
                End If

            End If

            'EDI管理番号(大,中)の採番(キャンセルフラグが０の場合も関数内で判断
            ds = Me.GetEdiCtlNoExcel(ds, bSameKeyFlg, sEdiCtlNo, iEdiCtlNoChu)

            '別インスタンス
            Dim setDs As DataSet = ds.Copy()
            Dim setDt As DataTable = setDs.Tables("LMH030_H_OUTKAEDI_DTL_GKE")
            setDt.Clear()
            setDt.ImportRow(rcvDtlDt.Rows(0))

            'EDI受信(DTL)の新規追加
            '削除EDI番号をクリアする
            setDs = MyBase.CallDAC(Me._Dac, "InsertEdiRcvDtlDataExcel", setDs)
            iRcvDtlInsCnt = iRcvDtlInsCnt + 1

            '受信DTL⇒EDI出荷(中)へのデータセット(上記で取得した商品情報も含む)
            '注)EDI出荷(中)は１レコード単位でセット
            setDs = Me.SetSemiOutkaEdiMExcel(setDs)

            'EDI出荷(中)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiMData", setDs)
            iOutDtlInsCnt = iOutDtlInsCnt + 1

            'EDI出荷(大) txtファイルの整理№(カラム８行目)が全行と差異がある場合は、新規追加
            If bSameKeyFlg = False Then
                '受信DTL⇒EDI出荷(大)へのデータセット
                setDs = Me.SetSemiOutkaEdiLExcel(setDs)

                '該当データに対するEDI出荷大テーブルの件数取得
                setDs = MyBase.CallDAC(Me._Dac, "SelectEdiLCount", setDs)

                '取得した件数が0件より多い場合、エラーメッセージ出力
                If Convert.ToInt32(setDs.Tables("DATA_COUNT").Rows(0).Item("DATA_COUNT").ToString) > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, _
                                           "E269", _
                                           New String() {String.Concat("出荷指示番号[", setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("CUST_ORD_NO").ToString, "]")}, _
                                           toriDtlDt.Rows(i).Item("REC_NO").ToString(), _
                                           LMH030BLC.EXCEL_COLTITLE, _
                                           String.Empty)
                    Return ds
                End If

                'EDI出荷(大)の新規追加
                setDs = MyBase.CallDAC(Me._Dac, "InsertOutkaEdiLData", setDs)
                iOutHedInsCnt = iOutHedInsCnt + 1
            End If

            'End If

        Next

        'エラー無し
        setDtHed.Rows(0).Item("ERR_FLG") = "0"

        '処理件数
        setSemiRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_HED_INS_CNT") = iOutHedInsCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_DTL_INS_CNT") = iOutDtlInsCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        setSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_HED_CAN_CNT") = iOutHedCanCnt.ToString()
        setSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT") = iOutDtlCanCnt.ToString()

        Return ds

    End Function
#End Region


#End Region
    '2012.03.06 大阪対応END

#Region "カラム項目の値・日付チェック"

#Region "CSVファイル用チェック"
    ''' <summary>
    ''' 項目チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ediCustIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiValChkCsv(ByVal dr As DataRow, ByVal ediCustIndex As String) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        '移動番号
        Dim targetStr As String = dr.Item("COLUMN_1").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("移動番号(カラム1番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動番号(カラム1番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動日
        targetStr = dr.Item("COLUMN_2").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("移動日(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '日付チェック
        ElseIf IsConvertDate(targetStr) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("移動日(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動区分
        '文字列長チェック
        targetStr = dr.Item("COLUMN_3").ToString
        If targetStr.Length > 3 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動区分(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動区分名
        targetStr = dr.Item("COLUMN_4").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動区分名(カラム4番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '入庫予定日
        targetStr = dr.Item("COLUMN_5").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("入庫予定日(カラム5番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '日付チェック
        ElseIf IsConvertDate(targetStr) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("入庫予定日(カラム5番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '伝票適用
        targetStr = dr.Item("COLUMN_6").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("伝票適用(カラム6番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先コード
        targetStr = dr.Item("COLUMN_7").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("移動先コード(カラム7番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先コード(カラム7番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先郵便番号
        targetStr = dr.Item("COLUMN_8").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先郵便番号(カラム8番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先住所１
        targetStr = dr.Item("COLUMN_9").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先住所１(カラム9番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先住所２
        targetStr = dr.Item("COLUMN_10").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先住所２(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先電話番号
        targetStr = dr.Item("COLUMN_11").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先電話番号(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先FAX番号
        targetStr = dr.Item("COLUMN_12").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先FAX番号(カラム12番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先名１
        targetStr = dr.Item("COLUMN_13").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先名１(カラム13番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先名２
        targetStr = dr.Item("COLUMN_14").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先名２(カラム14番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先名３
        targetStr = dr.Item("COLUMN_15").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先名３(カラム15番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元コード
        targetStr = dr.Item("COLUMN_16").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元コード(カラム16番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元郵便番号
        targetStr = dr.Item("COLUMN_17").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元郵便番号(カラム17番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元住所１
        targetStr = dr.Item("COLUMN_18").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元住所１(カラム18番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元住所２
        targetStr = dr.Item("COLUMN_19").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元住所２(カラム19番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元電話番号
        targetStr = dr.Item("COLUMN_20").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元電話番号(カラム20番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元FAX番号
        targetStr = dr.Item("COLUMN_21").ToString
        '半角文字列長チェック
        If IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元FAX番号(カラム21番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元名１
        targetStr = dr.Item("COLUMN_22").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元名１(カラム22番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元名２
        targetStr = dr.Item("COLUMN_23").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元名２(カラム23番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元名３
        targetStr = dr.Item("COLUMN_24").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元名３(カラム24番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ブランク１
        targetStr = dr.Item("COLUMN_25").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ブランク１(カラム25番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ブランク２
        targetStr = dr.Item("COLUMN_26").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ブランク２(カラム26番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ブランク３
        targetStr = dr.Item("COLUMN_27").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ブランク３(カラム27番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品コード
        targetStr = dr.Item("COLUMN_28").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("商品コード(カラム28番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品コード(カラム28番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品名
        targetStr = dr.Item("COLUMN_29").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品名(カラム29番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '規格
        targetStr = dr.Item("COLUMN_30").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("規格(カラム30番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品変動特性数値
        targetStr = dr.Item("COLUMN_31").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 60 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品変動特性数値(カラム31番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("商品変動特性数値(カラム31番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '商品変動特性数値単位
        targetStr = dr.Item("COLUMN_32").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品変動特性数値単位(カラム32番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品ランク
        targetStr = dr.Item("COLUMN_33").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品ランク(カラム33番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '管理NO
        targetStr = dr.Item("COLUMN_34").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("管理NO(カラム34番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'アスタリスク
        targetStr = dr.Item("COLUMN_35").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("*(カラム35番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動元棚番コード
        targetStr = dr.Item("COLUMN_36").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動元棚番コード(カラム36番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動先棚番コード
        targetStr = dr.Item("COLUMN_37").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動先棚番コード(カラム37番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '入数
        targetStr = dr.Item("COLUMN_38").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 9 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("入数(カラム38番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("入数(カラム38番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷数
        targetStr = dr.Item("COLUMN_39").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("荷数(カラム39番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("荷数(カラム39番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        'バラ数
        targetStr = dr.Item("COLUMN_40").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("バラ数(カラム40番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("バラ数(カラム40番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷姿単位名
        targetStr = dr.Item("COLUMN_41").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("荷姿単位名(カラム41番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'バラ単位名
        targetStr = dr.Item("COLUMN_42").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("バラ単位名(カラム42番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '数量
        targetStr = dr.Item("COLUMN_43").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("数量(カラム43番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("数量(カラム43番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷数と数量の関連チェック
        If dr.Item("COLUMN_39").ToString.Length.Equals(0) AndAlso
           dr.Item("COLUMN_43").ToString.Length.Equals(0) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E270", New String() {String.Concat("荷姿(カラム39番目)[", dr.Item("COLUMN_39").ToString, "]"), _
                                                                                 String.Concat("数量(カラム43番目)[", dr.Item("COLUMN_43").ToString, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '数量単位
        targetStr = dr.Item("COLUMN_44").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("数量単位(カラム44番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '資産数量
        targetStr = dr.Item("COLUMN_45").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("資産数量(カラム45番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("資産数量(カラム45番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '資産数量単位
        targetStr = dr.Item("COLUMN_46").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("資産数量単位(カラム46番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動単価
        targetStr = dr.Item("COLUMN_47").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動単価(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("移動単価(カラム47番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '移動金額
        targetStr = dr.Item("COLUMN_48").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動金額(カラム48番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("移動金額(カラム48番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '明細適用
        targetStr = dr.Item("COLUMN_49").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("明細適用(カラム49番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '端材フラグ
        targetStr = dr.Item("COLUMN_50").ToString
        If targetStr.Length > 1 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("端材フラグ(カラム50番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '移動金額計
        targetStr = dr.Item("COLUMN_51").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 30 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("移動金額計(カラム51番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("移動金額計(カラム51番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '資産所有者コード
        targetStr = dr.Item("COLUMN_52").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("資産所有者コード(カラム52番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '資産所有者名
        targetStr = dr.Item("COLUMN_53").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("資産所有者名(カラム53番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'LotBikou1
        targetStr = dr.Item("COLUMN_54").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("LotBikou1(カラム54番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'LotBikou2
        targetStr = dr.Item("COLUMN_55").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("LotBikou2(カラム55番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'DenpyouGenkaKingaku
        targetStr = dr.Item("COLUMN_56").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("DenpyouGenkaKingaku(カラム56番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ZaikoBashoCD
        targetStr = dr.Item("COLUMN_57").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ZaikoBashoCD(カラム57番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ZaikoBashoName
        targetStr = dr.Item("COLUMN_58").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ZaikoBashoName(カラム58番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region

#Region "エクセルファイル用チェック"
    ''' <summary>
    ''' 項目チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ediCustIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TorikomiValChkExcel(ByVal dr As DataRow, ByVal ediCustIndex As String) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        '出荷指示日
        Dim targetStr As String = dr.Item("COLUMN_1").ToString
        targetStr = dr.Item("COLUMN_1").ToString
        '空白の場合、チェックは行わない
        If String.IsNullOrEmpty(targetStr) Then
            '日付チェック
        ElseIf IsConvertDate(targetStr) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷指示日(カラム1番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷日
        targetStr = dr.Item("COLUMN_2").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷日(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '日付チェック
        ElseIf IsConvertDate(targetStr) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("出荷日(カラム2番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '出荷指示番号
        targetStr = dr.Item("COLUMN_3").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("出荷指示番号(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '半角文字列長チェック
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("出荷指示番号(カラム3番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '事務所名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_4").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("事務所名(カラム4番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '担当者名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_5").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("担当者名(カラム5番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '部門電話番号
        '半空白以外時の角文字列長チェック
        targetStr = dr.Item("COLUMN_6").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("部門電話番号(カラム6番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '部門FAX番号
        '半空白以外時の角文字列長チェック
        targetStr = dr.Item("COLUMN_7").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("部門FAX番号(カラム7番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '得意先コード
        '半空白以外時の角文字列長チェック
        targetStr = dr.Item("COLUMN_8").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先コード(カラム8番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '得意先名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_9").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("得意先名(カラム9番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先郵便番号
        targetStr = dr.Item("COLUMN_10").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 10, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先郵便番号(カラム10番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先コード
        targetStr = dr.Item("COLUMN_11").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 30, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先コード(カラム11番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先先名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_12").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先名(カラム12番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先名２
        '文字列長チェック
        targetStr = dr.Item("COLUMN_13").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先名２(カラム13番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先電話番号
        '半空白以外時の角文字列長チェック
        targetStr = dr.Item("COLUMN_14").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先電話番号(カラム14番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先FAX番号
        '空白以外時の半角文字列長チェック
        targetStr = dr.Item("COLUMN_15").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 15, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("部門FAX番号(カラム15番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先住所１
        '文字列長チェック
        targetStr = dr.Item("COLUMN_16").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先住所１(カラム16番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先住所２
        '文字列長チェック
        targetStr = dr.Item("COLUMN_17").ToString
        If targetStr.Length > 40 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先住所２(カラム17番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納入先住所３
        '文字列長チェック
        targetStr = dr.Item("COLUMN_18").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("納入先住所３(カラム18番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '自社正式名称
        '文字列長チェック
        targetStr = dr.Item("COLUMN_19").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("自社正式名称(カラム19番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '自社名称
        '文字列長チェック
        targetStr = dr.Item("COLUMN_20").ToString
        If targetStr.Length > 80 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("自社名称(カラム20番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '倉庫コード
        '空白以外時の半角文字列長チェック
        targetStr = dr.Item("COLUMN_21").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 3, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("倉庫コード(カラム21番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '倉庫名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_22").ToString
        If targetStr.Length > 50 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("倉庫名(カラム22番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品コード
        '文字列長チェック
        targetStr = dr.Item("COLUMN_23").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("商品コード(カラム23番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        ElseIf IsHalfSize(targetStr, 20, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品コード(カラム23番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品名
        '文字列長チェック
        targetStr = dr.Item("COLUMN_24").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品名(カラム24番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '規格
        '文字列長チェック
        targetStr = dr.Item("COLUMN_25").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("規格(カラム25番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品変動特性数値
        targetStr = dr.Item("COLUMN_26").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 60 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品変動特性数値(カラム26番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("商品変動特性数値(カラム26番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '商品変動特性数値単位
        targetStr = dr.Item("COLUMN_27").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品変動特性数値単位(カラム27番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '受注番号
        targetStr = dr.Item("COLUMN_28").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注番号(カラム28番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '受注行番号
        targetStr = dr.Item("COLUMN_29").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注番号(カラム29番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '取引区分名
        targetStr = dr.Item("COLUMN_30").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("受注番号(カラム30番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '商品ランク
        targetStr = dr.Item("COLUMN_31").ToString
        If targetStr.Length > 60 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("商品ランク(カラム31番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '管理NO
        targetStr = dr.Item("COLUMN_32").ToString
        If targetStr.Length > 30 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("管理NO(カラム32番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'ロットNO
        '空白以外時の半角文字列長チェック
        targetStr = dr.Item("COLUMN_33").ToString
        If String.IsNullOrEmpty(targetStr).Equals(True) Then
        ElseIf IsHalfSize(targetStr, 40, False).Equals(False) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("ロットNO(カラム33番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '入数
        targetStr = dr.Item("COLUMN_34").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 9 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("入数(カラム34番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("入数(カラム34番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷数
        targetStr = dr.Item("COLUMN_35").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("荷数(カラム35番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("荷数(カラム35番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷姿単位名
        targetStr = dr.Item("COLUMN_36").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("荷姿単位名(カラム36番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        'バラ数
        targetStr = dr.Item("COLUMN_37").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("バラ数(カラム37番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertInt32(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E025", New String() {String.Concat("バラ数(カラム37番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '数量単位
        targetStr = dr.Item("COLUMN_38").ToString
        If targetStr.Length > 10 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("数量単位(カラム38番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '総数
        targetStr = dr.Item("COLUMN_39").ToString
        If targetStr.Length > 0 Then
            If targetStr.Length > 10 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("総数(カラム39番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            ElseIf IsConvertDbl(targetStr).Equals(False) Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E005", New String() {String.Concat("総数(カラム39番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        '荷数と総数の関連チェック
        If dr.Item("COLUMN_35").ToString.Length.Equals(0) AndAlso
           dr.Item("COLUMN_39").ToString.Length.Equals(0) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E270", New String() {String.Concat("荷姿(カラム35番目)[", dr.Item("COLUMN_35").ToString, "]"), _
                                                                                 String.Concat("総数(カラム39番目)[", dr.Item("COLUMN_39").ToString, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '現品添付書類
        targetStr = dr.Item("COLUMN_40").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("現品添付書類(カラム40番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '物流指定
        targetStr = dr.Item("COLUMN_41").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("物流指定(カラム41番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '引当備考
        targetStr = dr.Item("COLUMN_42").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("引当備考(カラム42番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '納品日
        targetStr = dr.Item("COLUMN_43").ToString
        '必須チェック
        If String.IsNullOrEmpty(targetStr) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E001", New String() {String.Concat("納品日(カラム43番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            '日付チェック
        ElseIf IsConvertDate(targetStr) Then
        Else
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E445", New String() {String.Concat("納品日(カラム43番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '備考
        targetStr = dr.Item("COLUMN_44").ToString
        If targetStr.Length > 100 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E518", New String() {String.Concat("備考(カラム44番目)[", targetStr, "]")}, sRecNo, LMH030BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function






#End Region

#Region "共通処理"
    ''' <summary>
    ''' 年月日チェック
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=年月日として扱える False=年月日として扱えない</returns>
    ''' <remarks>引数にyyyy/MM/dd形式の文字列を設定し、それが年月日として扱えるかの判別を行う</remarks>
    Private Function IsConvertDate(ByVal targetString As String) As Boolean
        Dim d As DateTime
        Return DateTime.TryParseExact(targetString, "yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None, d)
    End Function

    ''' <summary>
    ''' 文字列長（バイト）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)
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
    ''' <returns>True=条件を満たしている False=条件を満たしていない</returns>
    ''' <remarks>文字列長はイコール比較を行う</remarks>
    Private Overloads Function IsHalfSize(ByVal targetString As String, ByVal length As Integer) As Boolean
        IsHalfSize(targetString, length, True)
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
    ''' 文字列が数値（Int32型）に変換出来るかチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=変換できる False=変換できない</returns>
    ''' <remarks></remarks>
    Private Function IsConvertInt32(ByVal targetString As String) As Boolean
        Dim i As Int32
        Return Int32.TryParse(targetString, i)
    End Function

#End Region

#End Region

#Region "土・日・休日チェック"
    ''' <summary>
    ''' 土・日・休日チェック(営業日の抽出)
    ''' </summary>
    ''' <param name="sWorkDay"></param>
    ''' <param name="max"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sWorkDay As String, ByVal max As Integer, ByVal setDs As DataSet) As DateTime

        '2012.03.23
        Dim holDr As DataRow
        Dim sWorkDayDate As DateTime = Convert.ToDateTime(Me._Blc.GetSlashEditDate(sWorkDay))

        '土・日は営業日として認めない
        For i As Integer = 1 To max

            Do
                sWorkDayDate = sWorkDayDate.AddDays(1)

                If Weekday(sWorkDayDate) = 1 OrElse Weekday(sWorkDayDate) = 7 Then

                Else

                    setDs.Tables("LMH030_M_HOL").Clear()

                    '休日マスタ参照
                    holDr = setDs.Tables("LMH030_M_HOL").NewRow()
                    holDr("HOL") = Format(sWorkDayDate, "yyyyMMdd")
                    'データセットに設定
                    setDs.Tables("LMH030_M_HOL").Rows.Add(holDr)

                    '休日マスタの値を取得
                    '休日マスタに存在する場合は、翌日で抽出(抽出できなくなるまで回す)
                    setDs = MyBase.CallDAC(Me._DacCom, "SelectMHolList", setDs)

                    If MyBase.GetResultCount = 0 Then '休日マスタに存在しない
                        'sWorkDayが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return sWorkDayDate

    End Function

#End Region

#End Region

End Class
