' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM100G : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMM100Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM100G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM100F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM100F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = LMMControlC.FUNCTION_F1_SHINKI
            .F2ButtonName = LMMControlC.FUNCTION_F2_HENSHU
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = LMMControlC.FUNCTION_F5_TANKA_IKKATU
            'START YANAI 要望番号372
            '.F6ButtonName = String.Empty
            .F6ButtonName = LMMControlC.FUNCTION_F6_NINUSHI_IKKATU
            'END YANAI 要望番号372
            '2015.10.02 他荷主対応START
            .F7ButtonName = LMMControlC.FUNCTION_F7_TANINUSI
            '2015.10.02 他荷主対応END
            .F8ButtonName = LMMControlC.FUNCTION_F8_KIKEN
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            'START YANAI 要望番号372
            '.F6ButtonEnabled = lock
            'END YANAI 要望番号372
            .F8ButtonEnabled = view OrElse init
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F5ButtonEnabled = view OrElse init
            '2015.10.02 他荷主対応START
            .F7ButtonEnabled = view OrElse init
            '2015.10.02 他荷主対応END
            'START YANAI 要望番号372
            .F6ButtonEnabled = view OrElse init
            'END YANAI 要望番号372
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal dispMode As String, ByVal recStatus As String)

        With Me._Frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recStatus
        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            .numWidthBulk.TabIndex = LMM100C.CtlTabIndex.NUM_WIDTH_BULK
            .numDepthBulk.TabIndex = LMM100C.CtlTabIndex.NUM_DEPTH_BULK
            .numHeightBulk.TabIndex = LMM100C.CtlTabIndex.NUM_HEIGHT_BULK
            .numActualVolumeBulk.TabIndex = LMM100C.CtlTabIndex.NUM_ACTUAL_VOLUME_BULK
            .numOccupyVolumeBulk.TabIndex = LMM100C.CtlTabIndex.NUM_OCCUPY_VOLUME_BULK
            .cmbPrint.TabIndex = LMM100C.CtlTabIndex.CMB_PRINT
            .btnPrint.TabIndex = LMM100C.CtlTabIndex.BTN_PRINT
            .sprGoods.TabIndex = LMM100C.CtlTabIndex.SPR_GOODS
            .tabGoodsMst.TabIndex = LMM100C.CtlTabIndex.TAB_GOODS_M
            .tpgGoods.TabIndex = LMM100C.CtlTabIndex.TPG_GOODS
            '******************* 商品タブ *****************************
            .cmbBr.TabIndex = LMM100C.CtlTabIndex.CMB_BR
            .txtCustCdL.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CD_L
            .lblCustNmL.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_NM_L
            .txtCustCdM.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CD_M
            .lblCustNmM.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_NM_M
            .txtCustCdS.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CD_S
            .lblCustNmS.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_NM_S
            .txtCustCdSS.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CD_SS
            .lblCustNmSS.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_NM_SS
            .txtNisonin.TabIndex = LMM100C.CtlTabIndex.TXT_NISOUNIN
            .lblNisonin.TabIndex = LMM100C.CtlTabIndex.LBL_NISOUNIN
            .txtGoodsNm1.TabIndex = LMM100C.CtlTabIndex.TXT_GOODS_NM_1
            .txtGoodsNm2.TabIndex = LMM100C.CtlTabIndex.TXT_GOODS_NM_2
            .txtGoodsNm3.TabIndex = LMM100C.CtlTabIndex.TXT_GOODS_NM_3
            .lblGoodsKey.TabIndex = LMM100C.CtlTabIndex.LBL_GOODS_KEY
            .txtGoodsCd.TabIndex = LMM100C.CtlTabIndex.TXT_GOODS_CD
            .optKosu.TabIndex = LMM100C.CtlTabIndex.OPT_KOSU
            .optSuryo.TabIndex = LMM100C.CtlTabIndex.OPT_SURYO
            .cmbKosuTani.TabIndex = LMM100C.CtlTabIndex.CMB_KOSUTANI
            .numHutaiJyuryo.TabIndex = LMM100C.CtlTabIndex.NUM_HUTAI_JURYO
            .cmbHutaiJyuryo.TabIndex = LMM100C.CtlTabIndex.CMB_HUTAI_JURYO_KASAN
            .numIrisu.TabIndex = LMM100C.CtlTabIndex.NUM_IRISU
            .lblNisugata.TabIndex = LMM100C.CtlTabIndex.LBL_NISUGATA
            .cmbHosotani.TabIndex = LMM100C.CtlTabIndex.CMB_HOSOTANI
            .txtKonpoSagyoCd.TabIndex = LMM100C.CtlTabIndex.TXT_KONPOSAGYO_CD
            .lblKonpoSagyoCd.TabIndex = LMM100C.CtlTabIndex.LBL_KONPOSAGYO_CD
            .txtShukkaChuiJiko.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKACYUI_JIKO
            .cmbHokanKbnHokan.TabIndex = LMM100C.CtlTabIndex.CMB_HOKANKBN_HOKAN
            .cmbHokanKbnUnso.TabIndex = LMM100C.CtlTabIndex.CMB_HOKANKBN_UNSO
            .imdOndoKanriStartHokan.TabIndex = LMM100C.CtlTabIndex.IMD_ONDOKANRI_START_HOKAN
            .imdOndoKanriStartUnso.TabIndex = LMM100C.CtlTabIndex.IMD_ONDOKANRI_START_UNSO
            .imdOndoKanriEndHokan.TabIndex = LMM100C.CtlTabIndex.IMD_ONDOKANRI_END_HOKAN
            .imdOndoKanriEndUnso.TabIndex = LMM100C.CtlTabIndex.IMD_ONDOKANRI_END_UNSO
            .numOndoKanriMax.TabIndex = LMM100C.CtlTabIndex.NUM_ONDOKANRI_MAX
            .numOndoKanriMin.TabIndex = LMM100C.CtlTabIndex.NUM_ONDOKANRI_MIN
            .numHyojyunIrime.TabIndex = LMM100C.CtlTabIndex.NUM_HYOJYUN_IRIME
            .cmbHyojyunIrimeTani.TabIndex = LMM100C.CtlTabIndex.CMB_HYOJYUN_IRIME_TANI
            .numHyojyunJyuryo.TabIndex = LMM100C.CtlTabIndex.NUM_HYOJYUN_JURYO
            .numHizyu.TabIndex = LMM100C.CtlTabIndex.NUM_HIZYU
            .numHyojyunYoseki.TabIndex = LMM100C.CtlTabIndex.NUM_HYOJYUN_YOSEKI
            .txtCustKanjokamoku1.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_KANJOCD_1
            .txtCustKanjokamoku2.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_KANJOCD_2
            .txtCustCategory1.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CATEGORY_1
            .txtCustCategory2.TabIndex = LMM100C.CtlTabIndex.TXT_CUST_CATEGORY_2
            .numPalettoSu.TabIndex = LMM100C.CtlTabIndex.NUM_PALETTO_SU
            .cmbLotKanriLevel.TabIndex = LMM100C.CtlTabIndex.CMB_LOT_KANRI_LEVEL
            .cmbKiteiHoryuhinKbn.TabIndex = LMM100C.CtlTabIndex.CMB_KITEI_HORYUHIN_KBN
            .cmbKitakuKakaku.TabIndex = LMM100C.CtlTabIndex.CMB_KITAKU_KAKAKU_TANI_KBN
            .numKitakuShohinTanka.TabIndex = LMM100C.CtlTabIndex.NUM_KITAKU_GOODS_TANKA
            .numNihudaInsatu.TabIndex = LMM100C.CtlTabIndex.NUM_NIHUDA_INSTU_MAISU
            .numInnerPkgNb.TabIndex = LMM100C.CtlTabIndex.NUM_INNER_PKG_NB
            .txtTankaGroup.TabIndex = LMM100C.CtlTabIndex.TXT_GROUP_CD
            .numShohikigenKinshi.TabIndex = LMM100C.CtlTabIndex.NUM_SHOHIKIGEN_KINSHIBI
            .cmbSiteiNohinSho.TabIndex = LMM100C.CtlTabIndex.CMB_NOHINSHO
            .cmbBunsekiHyo.TabIndex = LMM100C.CtlTabIndex.CMB_BUNSEKIHYO
            .cmbShomiKigenKanri.TabIndex = LMM100C.CtlTabIndex.CMB_SHOMIKEGENKANRI
            .cmbSeizobiKanri.TabIndex = LMM100C.CtlTabIndex.CMB_SEIZOBIKANRI
            .cmbHikiateChuiHin.TabIndex = LMM100C.CtlTabIndex.CMB_HIKIATE_CHUIHIN
            .cmbSeikyuMeisaisho.TabIndex = LMM100C.CtlTabIndex.CMB_SEIKYUMEISAI_SHUTURYOKU
            .cmbUnsoHoken.TabIndex = LMM100C.CtlTabIndex.CMB_UNSO_HOKEN             'ADD 2018/07/17 依頼番号 001540 
            .numWidth.TabIndex = LMM100C.CtlTabIndex.NUM_WIDTH
            .numDepth.TabIndex = LMM100C.CtlTabIndex.NUM_DEPTH
            .numHeight.TabIndex = LMM100C.CtlTabIndex.NUM_HEIGHT
            .numActualVolume.TabIndex = LMM100C.CtlTabIndex.NUM_ACTUAL_VOLUME
            .numOccupyVolume.TabIndex = LMM100C.CtlTabIndex.NUM_OCCUPY_VOLUME

            ' 入荷時加工作業区分パネル/出荷時加工作業区分パネル
            .pnlNyukaSagyoKbn.TabIndex = LMM100C.CtlTabIndex.PNL_NYUKASAGYO_KBN
            .txtNyukaSagyoKbn1.TabIndex = LMM100C.CtlTabIndex.TXT_NYUKASAGYO_KBN_1
            .txtNyukaSagyoKbn2.TabIndex = LMM100C.CtlTabIndex.TXT_NYUKASAGYO_KBN_2
            .txtNyukaSagyoKbn3.TabIndex = LMM100C.CtlTabIndex.TXT_NYUKASAGYO_KBN_3
            .txtNyukaSagyoKbn4.TabIndex = LMM100C.CtlTabIndex.TXT_NYUKASAGYO_KBN_4
            .txtNyukaSagyoKbn5.TabIndex = LMM100C.CtlTabIndex.TXT_NYUKASAGYO_KBN_5
            .pnlShukkaSagyoKbn.TabIndex = LMM100C.CtlTabIndex.PNL_SHUKKASAGYO_KBN
            .txtShukkaSagyoKbn1.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKASAGYO_KBN_1
            .txtShukkaSagyoKbn2.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKASAGYO_KBN_2
            .txtShukkaSagyoKbn3.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKASAGYO_KBN_3
            .txtShukkaSagyoKbn4.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKASAGYO_KBN_4
            .txtShukkaSagyoKbn5.TabIndex = LMM100C.CtlTabIndex.TXT_SHUKKASAGYO_KBN_5
            ' 単価情報パネル
            .pnlTankaJoho.TabIndex = LMM100C.CtlTabIndex.PNL_TANKA_JOHO
            .lblTekiyoStartDate.TabIndex = LMM100C.CtlTabIndex.LBL_TEKIYOSTART
            .numHokanTujo.TabIndex = LMM100C.CtlTabIndex.NUM_HOKANRYO_TUJO
            .cmbHokanTujo.TabIndex = LMM100C.CtlTabIndex.CMB_HOKANRYO_TUJO
            .numHokanTeion.TabIndex = LMM100C.CtlTabIndex.NUM_HOKANRYO_TEION
            .cmbHokanTeion.TabIndex = LMM100C.CtlTabIndex.CMB_HOKANRYO_TEION
            .numNiyakuNyuko.TabIndex = LMM100C.CtlTabIndex.NUM_NIYAKURYO_NYUKO
            .cmbNiyakuNyuko.TabIndex = LMM100C.CtlTabIndex.CMB_NIYAKURYO_NYUKO
            .numNiyakuShukko.TabIndex = LMM100C.CtlTabIndex.NUM_NIYAKURYO_SHUKKO
            .cmbNiyakuShukko.TabIndex = LMM100C.CtlTabIndex.CMB_NIYAKURYO_SHUKKO
            .numNiyakuMinHosho.TabIndex = LMM100C.CtlTabIndex.NUM_NIYAKU_MIN_NYUKO
            '隠し項目
            .lblCustCdS.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_CD_S
            .lblCustCdSS.TabIndex = LMM100C.CtlTabIndex.LBL_CUST_CD_SS
            .lblCylFlg.TabIndex = LMM100C.CtlTabIndex.LBL_CYL_FLG
            '******************* 商品明細タブ *****************************
            .tpgGoodsDetail.TabIndex = LMM100C.CtlTabIndex.TPG_GOODS_DETAIL
            .cmbDokugeki.TabIndex = LMM100C.CtlTabIndex.CMB_DOKUGEKI
            .cmbKouathugas.TabIndex = LMM100C.CtlTabIndex.CMB_KOUATHUGAS
            .cmbYakuziho.TabIndex = LMM100C.CtlTabIndex.CMB_YAKUZIHO
            .cmbShobokiken.TabIndex = LMM100C.CtlTabIndex.CMB_SHOBOKIKEN
            .txtShobo.TabIndex = LMM100C.CtlTabIndex.TXT_SHOBOCD
            .lblShobo.TabIndex = LMM100C.CtlTabIndex.LBL_SHOBOCD
            .txtUn.TabIndex = LMM100C.CtlTabIndex.TXT_UN
            '.cmbPg.TabIndex = LMM100C.CtlTabIndex.TXT_PG
            .txtPg.TabIndex = LMM100C.CtlTabIndex.TXT_PG
            .lblClass1.TabIndex = LMM100C.CtlTabIndex.TXT_CLASS1
            .lblClass2.TabIndex = LMM100C.CtlTabIndex.TXT_CLASS2
            .lblClass3.TabIndex = LMM100C.CtlTabIndex.TXT_CLASS3
            .lblKaiyouosen.TabIndex = LMM100C.CtlTabIndex.TXT_KAIYOUOSEN
            .txtBarkodo.TabIndex = LMM100C.CtlTabIndex.TXT_BARCODE
            .cmbKagakuBusitu.TabIndex = LMM100C.CtlTabIndex.CMB_KAGAKU_BUSITU
            .cmbSokoHinmoku.TabIndex = LMM100C.CtlTabIndex.CMB_SOKOHINMOKU
            .cmbHacchuten.TabIndex = LMM100C.CtlTabIndex.CMB_HACCHUTEN
            .numHaccyuSuryo.TabIndex = LMM100C.CtlTabIndex.NUM_HACCHUTEN
            .cmbSizeKbn.TabIndex = LMM100C.CtlTabIndex.CMB_SIZEKBN
            .cmbOuterPackage.TabIndex = LMM100C.CtlTabIndex.CMB_OUTER_PACKAGE
            .cmbAVAL_YN.TabIndex = LMM100C.CtlTabIndex.AVAL_YN
            .btnRowAdd.TabIndex = LMM100C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM100C.CtlTabIndex.BTN_DETAIL
            .sprGoodsDetail.TabIndex = LMM100C.CtlTabIndex.SPR_GOODS_DETAIL
            .lblCreateUser.TabIndex = LMM100C.CtlTabIndex.LBL_CREATE_USER
            .lblCreateDate.TabIndex = LMM100C.CtlTabIndex.LBL_CREATE_DATE
            .lblUpdateUser.TabIndex = LMM100C.CtlTabIndex.LBL_UPDATE_USER
            .lblUpdateDate.TabIndex = LMM100C.CtlTabIndex.LBL_UPDATE_DATE
            '隠し項目
            .lblUpdateTime.TabIndex = LMM100C.CtlTabIndex.LBL_UPDATE_TIME

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        'コントロールの日付書式設定
        Call Me.SetDateControl()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()


    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal existZaikoFlg As Boolean)

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.INIT _
                , DispMode.VIEW
                    .sprGoods.Focus()

                Case DispMode.EDIT
                    '新規、複写時⇒荷主コード(大)
                    '編集時　　　⇒荷主コード(小)
                    .tabGoodsMst.SelectedTab = .tpgGoods
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC _
                           , RecordStatus.COPY_REC
                            .txtCustCdL.Focus()
                        Case RecordStatus.NOMAL_REC
                            If .txtCustCdS.ReadOnly() Then
                                .txtNisonin.Focus()
                            Else
                                .txtCustCdS.Focus()
                            End If
                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <param name="existZaikoFlg">在庫存在フラグ</param>
    ''' <param name="existCustZaikoFlg">在庫存在フラグ(荷主コードS・SS 編集可否判定用)</param>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal existZaikoFlg As Boolean, Optional ByVal existCustZaikoFlg As Boolean = False)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus(existZaikoFlg, existCustZaikoFlg)

    End Sub

    ''' <summary>
    ''' 項目のクリア処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        With Me._Frm

            '数値項目に初期値0を設定する
            '商品タブ
            .numHutaiJyuryo.Value = 0
            .numIrisu.Value = 0
            .numOndoKanriMax.Value = 0
            .numOndoKanriMin.Value = 0
            .numHyojyunIrime.Value = 0.0
            .numHyojyunJyuryo.Value = 0.0
            .numHizyu.Value = 0.7
            .numHyojyunYoseki.Value = 0
            .numPalettoSu.Value = 0
            .numInnerPkgNb.Value = 0
            .numKitakuShohinTanka.Value = 0
            .numNihudaInsatu.Value = 0
            .numShohikigenKinshi.Value = 0
            '単価情報パネル
            .numHokanTujo.Value = 0
            .numHokanTeion.Value = 0
            .numNiyakuNyuko.Value = 0
            .numNiyakuShukko.Value = 0
            .numNiyakuMinHosho.Value = 0
            '通貨コード
            .lblHokanTudoCurrCd.TextValue = ""
            .lblHokanTeionCurrCd.TextValue = ""
            .lblNiyakuNyukoCurrCd.TextValue = ""
            .lblNiyakuSyukkoCurrCd.TextValue = ""
            .lblNiyakuMinHoshoCurrCd.TextValue = ""
            '商品明細タブ
            .numHaccyuSuryo.Value = 0

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
            .cmbOuterPackage.Items.Clear()
#End If

            '2016/05/24 Shinoda Add START
            .cmbSiteiNohinSho.SelectedValue = "00"
            .cmbBunsekiHyo.SelectedValue = "00"
            .cmbShomiKigenKanri.SelectedValue = "00"
            .cmbSeizobiKanri.SelectedValue = "00"
            .cmbHikiateChuiHin.SelectedValue = "00"
            .cmbSeikyuMeisaisho.SelectedValue = "00"
            '2016/05/24 Shinoda Add END

            .cmbUnsoHoken.SelectedValue = "00"          'ADD 2018/07/17 依頼番号 001540 

            .numWidth.Value = 0
            .numDepth.Value = 0
            .numHeight.Value = 0
            .numActualVolume.Value = 0
            .numOccupyVolume.Value = 0
            .numWidthBulk.Value = 0
            .numDepthBulk.Value = 0
            .numHeightBulk.Value = 0
            .numActualVolumeBulk.Value = 0
            .numOccupyVolumeBulk.Value = 0

            '使用状態を可にする
            .cmbAVAL_YN.SelectedValue = "01"    'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprGoods.ActiveSheet

            '******************* 商品タブ *****************************
            .cmbBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
            .txtCustCdL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            .lblCustNmL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_NM_L.ColNo))
            .txtCustCdM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))
            .lblCustNmM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_NM_M.ColNo))
            .txtCustCdS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_S.ColNo))
            .lblCustNmS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_NM_S.ColNo))
            .txtCustCdSS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_SS.ColNo))
            .lblCustNmSS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_NM_SS.ColNo))
            .txtNisonin.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NISONIN_CD_L.ColNo))
            .lblNisonin.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NISONIN_NM_L.ColNo))
            .txtGoodsNm1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo))
            .txtGoodsNm2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_2.ColNo))
            .txtGoodsNm3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_3.ColNo))
            .lblGoodsKey.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
            .txtGoodsCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GOODS_CD.ColNo))
            If Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HIKIATE_TANI_KBN.ColNo)).Equals(LMM100C.HIKIATE_TANI_KOSU) Then
                .optKosu.Checked = True
                .optSuryo.Checked = False
            ElseIf Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HIKIATE_TANI_KBN.ColNo)).Equals(LMM100C.HIKIATE_TANI_SURYO) Then
                .optKosu.Checked = False
                .optSuryo.Checked = True
            End If
            .cmbKosuTani.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KOSU_TANI_KBN.ColNo))
            .numHutaiJyuryo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HUTAI_JURYO.ColNo))
            .cmbHutaiJyuryo.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HUTAI_KASAN_FLG.ColNo))
            .numIrisu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.IRI_SU.ColNo))
            .lblNisugata.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KOSU_TANI_NM.ColNo))
            .cmbHosotani.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOSO_TANI.ColNo))
            .txtKonpoSagyoCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KONPO_SAGYO_CD.ColNo))
            .lblKonpoSagyoCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KONPO_SAGYO_NM.ColNo))
            .txtShukkaChuiJiko.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_CHUIJIKO.ColNo))
            .cmbHokanKbnHokan.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_KBN.ColNo))
            .cmbHokanKbnUnso.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KBN_UNSO.ColNo))
            .imdOndoKanriStartHokan.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_START_HOKAN.ColNo))
            .imdOndoKanriStartUnso.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_START_UNSO.ColNo))
            .imdOndoKanriEndHokan.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_END_HOKAN.ColNo))
            .imdOndoKanriEndUnso.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_END_UNSO.ColNo))
            .numOndoKanriMax.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_MAX.ColNo))
            .numOndoKanriMin.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ONDO_MIN.ColNo))
            .numHyojyunIrime.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HYOJUN_IRIME.ColNo))
            .cmbHyojyunIrimeTani.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HYOJUN_IRIME_TANI.ColNo))
            .numHyojyunJyuryo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HYOJUN_JURYO.ColNo))
            .numHizyu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HIZYU.ColNo))
            .numHyojyunYoseki.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HYOJYUN_YOSEKI.ColNo))
            .txtCustKanjokamoku1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_KANJO_KMK_CD_1.ColNo))
            .txtCustKanjokamoku2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_KANJO_KMK_CD_2.ColNo))
            .txtCustCategory1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CATEGORY_1.ColNo))
            .txtCustCategory2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CATEGORY_2.ColNo))
            .numPalettoSu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.PALETTO_HOSOKOSU.ColNo))
            .numInnerPkgNb.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.INNER_PKG_NB.ColNo))
            .cmbLotKanriLevel.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.LOT_KANRI_LEVEL_CD.ColNo))
            .cmbKiteiHoryuhinKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KITEI_HORYUHIN_KBN.ColNo))
            .cmbKitakuKakaku.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KITAKU_KAKAKU_TANI_KBN.ColNo))
            .numKitakuShohinTanka.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KITAKU_SHOHIN_TANKA.ColNo))
            .numNihudaInsatu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIHUDA_INSATU_SU.ColNo))
            .txtTankaGroup.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColNo))
            .numShohikigenKinshi.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHOHIKIGEN_KINSHIBI.ColNo))
            .cmbSiteiNohinSho.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SITEI_NOHINSHO_KBN.ColNo))
            .cmbBunsekiHyo.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.BUNSEKI_HYO_KBN.ColNo))
            .cmbShomiKigenKanri.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHOMIKIGEN_KANRI_CD.ColNo))
            .cmbSeizobiKanri.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SEIZOBI_KANRI_CD.ColNo))
            .cmbHikiateChuiHin.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo))
            .cmbSeikyuMeisaisho.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SEIQT_DTL_SHUTURYOKU_FLG.ColNo))
            .cmbUnsoHoken.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.UNSO_HOKEN_FLG.ColNo))     'ADD 2017/07/17 依頼番号 001540 
            .numWidth.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.WIDTH.ColNo))
            .numDepth.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.DEPTH.ColNo))
            .numHeight.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HEIGHT.ColNo))
            .numActualVolume.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.ACTUAL_VOLUME.ColNo))
            .numOccupyVolume.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OCCUPY_VOLUME.ColNo))
            ' 入荷時加工作業区分パネル/出荷時加工作業区分パネル
            .txtNyukaSagyoKbn1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_1.ColNo))
            .txtNyukaSagyoKbn2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_2.ColNo))
            .txtNyukaSagyoKbn3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_3.ColNo))
            .txtNyukaSagyoKbn4.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_4.ColNo))
            .txtNyukaSagyoKbn5.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_5.ColNo))
            .txtShukkaSagyoKbn1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_1.ColNo))
            .txtShukkaSagyoKbn2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_2.ColNo))
            .txtShukkaSagyoKbn3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_3.ColNo))
            .txtShukkaSagyoKbn4.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_4.ColNo))
            .txtShukkaSagyoKbn5.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_5.ColNo))
            ' 単価情報パネル                   
            .lblTekiyoStartDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.TEKIYO_START_DATE.ColNo))
            .numHokanTujo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TUJO.ColNo))
            .cmbHokanTujo.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TATE_KBN_NASHI.ColNo))
            .numHokanTeion.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TEION.ColNo))
            .cmbHokanTeion.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TATE_KBN_ARI.ColNo))
            .numNiyakuNyuko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_NYUKO.ColNo))
            .cmbNiyakuNyuko.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_NYUKO_TATE_KBN.ColNo))
            .numNiyakuShukko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_SHUKKO.ColNo))
            .cmbNiyakuShukko.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_SHUKKO_TATE_KBN.ColNo))
            .numNiyakuMinHosho.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKU_MIN_NYUKO.ColNo))

            .cmbAVAL_YN.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.AVAL_YN.ColNo))        'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

            'フォーマット登録　20150729 常平Add
            .TxtBoxGoodsCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_CUST.ColNo))
            .TxtBoxGoodsNM1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_NM1.ColNo))
            .TxtBoxGoodsNM2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_NM2.ColNo))
            .TxtBoxIrime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_STD_IRIME.ColNo))
            '隠し項目     
            .lblCustCdS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_S.ColNo))
            .lblCustCdSS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CUST_CD_SS.ColNo))
            .lblCylFlg.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CYL_FLG.ColNo))

            '******************* 商品明細タブ *****************************
            .cmbDokugeki.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.DOKUGEKI_KBN.ColNo))
            .cmbKouathugas.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KOUATHUGAS_KB.ColNo))
            .cmbYakuziho.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.YAKUZIHO_KB.ColNo))
            .cmbShobokiken.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHOBOKIKEN_KB.ColNo))
            .txtShobo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHOBO_CD.ColNo))
            .lblShobo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHOBO_JOHO_NM.ColNo))
            .txtUn.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.UN.ColNo))
            '.cmbPg.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.PG.ColNo))
            .txtPg.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.PG.ColNo))
            '非該当の場合は固定文字列
            If .txtUn.TextValue <> "-" Then
                .lblClass1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CLASS_1.ColNo))
                .lblClass2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CLASS_2.ColNo))
                .lblClass3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CLASS_3.ColNo))
            Else
                .lblClass1.TextValue = "非該当"
                .lblClass2.TextValue = String.Empty
                .lblClass3.TextValue = String.Empty
            End If
            .lblKaiyouosen.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KAIYOUOSEN_KB_NM.ColNo))
            .txtKaiyouosen.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KAIYOUOSEN_KB.ColNo))
            .txtBarkodo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.BAR_CD.ColNo))
            .cmbKagakuBusitu.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KAGAKUBUSITU_KBN.ColNo))
            .cmbSokoHinmoku.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SOKO_KYOKAI_HIN_KBN.ColNo))
            .cmbHacchuten.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HACCHUTEN_KBN.ColNo))
            .numHaccyuSuryo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HACCHU_SURYO.ColNo))
            .cmbSizeKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SIZE_KBN.ColNo))  '検証結果№70対応(2011.09.08)
            Me.CreateOuterPackageComboBox()
            .cmbOuterPackage.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.OUTER_PACKEGE.ColNo))
            .cmbGusKanri.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.GUS_KANRI_KBN.ColNo))
            .cmbKikenhin.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.KIKENHIN_KBN.ColNo))
            .lblHokanTudoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TUJO_CURR_CD.ColNo))
            .lblHokanTeionCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.HOKANRYO_TEION_CURR_CD.ColNo))
            .lblNiyakuNyukoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_NYUKO_CURR_CD.ColNo))
            .lblNiyakuSyukkoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo))
            .lblNiyakuMinHoshoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.NIYAKU_MIN_NYUKO_CURR_CD.ColNo))
            .lblCreateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CREATE_USER.ColNo))
            .lblCreateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.CREATE_DATE.ColNo)))
            .lblUpdateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.UPDATE_USER.ColNo))
            .lblUpdateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.UPDATE_DATE.ColNo)))
            '隠し項目                           
            .lblUpdateTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.UPDATE_TIME.ColNo))

            '要望対応1995 端数出荷時作業区分対応
            .txtHasuSagyoKbn1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_1.ColNo))
            .txtHasuSagyoKbn2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_2.ColNo))
            .txtHasuSagyoKbn3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_3.ColNo))

        End With

        ' 寄託商品単価 表示制御
        Call SetKitakuShohinTankaVisible(row)

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Me._ControlG.SetDateFormat(.imdOndoKanriStartHokan, LMMControlC.DATE_FORMAT.MM_DD)
            Me._ControlG.SetDateFormat(.imdOndoKanriStartUnso, LMMControlC.DATE_FORMAT.MM_DD)
            Me._ControlG.SetDateFormat(.imdOndoKanriEndHokan, LMMControlC.DATE_FORMAT.MM_DD)
            Me._ControlG.SetDateFormat(.imdOndoKanriEndUnso, LMMControlC.DATE_FORMAT.MM_DD)

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            '商品タブ
            .numHutaiJyuryo.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(-999999.999))
            .numIrisu.SetInputFields("##,###,##0", , 8, 1, , 0, 0, , Convert.ToDecimal(99999999), Convert.ToDecimal(0))
            .numOndoKanriMax.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(-99))
            .numOndoKanriMin.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(-99))
            .numHyojyunIrime.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0.0))
            .numHyojyunJyuryo.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0.0))
            .numHizyu.SetInputFields("##0.000", , 3, 1, , 3, 3, , Convert.ToDecimal(999.999), Convert.ToDecimal(0.0))
            .numHyojyunYoseki.SetInputFields("#0.00000", , 2, 1, , 5, 5, , Convert.ToDecimal(99.99999), Convert.ToDecimal(0))
            '(2012.12.05)要望番号1667 3桁→6桁 --- START ---
            '.numPalettoSu.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(0))
            .numPalettoSu.SetInputFields("###,##0", , 6, 1, , 0, 0, , Convert.ToDecimal(999999), Convert.ToDecimal(0))
            '(2012.12.05)要望番号1667 3桁→6桁 ---  END  ---
            .numKitakuShohinTanka.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNihudaInsatu.SetInputFields("#,##0", , 4, 1, , 0, 0, , Convert.ToDecimal(9999), Convert.ToDecimal(0))
            .numInnerPkgNb.SetInputFields("##,###,##0", , 8, 1, , 0, 0, , Convert.ToDecimal(99999999), Convert.ToDecimal(0))
            .numShohikigenKinshi.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(0))
            '単価マスタ情報パネル
            .numHokanTujo.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numHokanTeion.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNiyakuNyuko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNiyakuShukko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNiyakuMinHosho.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            '商品明細タブ
            .numHaccyuSuryo.SetInputFields("###,##0", , 6, 1, , 0, 0, , Convert.ToDecimal(999999), Convert.ToDecimal(0))

            .numWidth.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numDepth.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numHeight.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numActualVolume.SetInputFields("###,##0.000000", , 6, 1, , 6, 6, , Convert.ToDecimal(999999.999999), Convert.ToDecimal(0))
            .numOccupyVolume.SetInputFields("###,##0.000000", , 6, 1, , 6, 6, , Convert.ToDecimal(999999.999999), Convert.ToDecimal(0))
            .numWidthBulk.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numDepthBulk.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numHeightBulk.SetInputFields("#0.00", , 2, 1, , 2, 2, , Convert.ToDecimal(99.99), Convert.ToDecimal(0))
            .numActualVolumeBulk.SetInputFields("###,##0.000000", , 6, 1, , 6, 6, , Convert.ToDecimal(999999.999999), Convert.ToDecimal(0))
            .numOccupyVolumeBulk.SetInputFields("###,##0.000000", , 6, 1, , 6, 6, , Convert.ToDecimal(999999.999999), Convert.ToDecimal(0))
        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus(ByVal existZaikoFlg As Boolean, ByVal existCustZaikoFlg As Boolean)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    'タブ内の表示内容を初期化する
                    Call Me.ClearControl(Me._Frm.tabGoodsMst)
                    Me._Frm.sprGoodsDetail.CrearSpread()

                Case DispMode.VIEW

                    '印刷関連活性化
                    Call Me._ControlG.LockButton(.btnPrint, unLock)
                    Call Me._ControlG.LockCombKbn(.cmbPrint, unLock)

                    '容積一括更新活性化
                    Call Me._ControlG.LockButton(.btnUpdate, unLock)
                    Call Me._ControlG.LockNumber(.numWidthBulk, unLock)
                    Call Me._ControlG.LockNumber(.numDepthBulk, unLock)
                    Call Me._ControlG.LockNumber(.numHeightBulk, unLock)
                    Call Me._ControlG.LockNumber(.numActualVolumeBulk, unLock)
                    Call Me._ControlG.LockNumber(.numOccupyVolumeBulk, unLock)

                Case DispMode.EDIT

                    '行追加/削除ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel, unLock)

                    'タブ活性化
                    Call Me._ControlG.SetLockControl(.tabGoodsMst, unLock)
                    '常にロック項目ロック制御
                    Call Me._ControlG.SetLockControl(.pnlTankaJoho, lock)
                    Call Me._ControlG.LockComb(.cmbBr, lock)
                    Call Me._ControlG.LockNumber(.numHutaiJyuryo, lock)

                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit(existZaikoFlg, existCustZaikoFlg)

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha()

                    End Select
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 寄託商品単価 表示制御
    ''' </summary>
    Friend Sub SetKitakuShohinTankaVisible(Optional ByVal row As Integer = -1)

        Dim isVisible As Boolean = True

        If LMUserInfoManager.GetNrsBrCd = "75" Then
            ' 熊本

            Dim custCdL As String = ""
            If 1 <= row AndAlso row <= (Me._Frm.sprGoods.ActiveSheet.RowCount() - 1) Then
                custCdL = Me._ControlG.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.CUST_CD.ColNo)).Substring(0, "00000".Length())
            End If
            If custCdL = "00001" Then
                ' TSMC

                ' ユーザー権限別
                Select Case LMUserInfoManager.GetAuthoLv
                    Case LMConst.AuthoKBN.VIEW      ' 10:閲覧者
                        isVisible = False
                    Case LMConst.AuthoKBN.EDIT      ' 20:入力者（一般）
                        isVisible = False
                    Case LMConst.AuthoKBN.EDIT_UP   ' 25:入力者（上級）
                        isVisible = True
                    Case LMConst.AuthoKBN.LEADER    ' 30:センター長
                        isVisible = True
                    Case LMConst.AuthoKBN.MANAGER   ' 40:システム管理者
                        isVisible = True
                    Case LMConst.AuthoKBN.AGENT     ' 50:外部
                        isVisible = False
                End Select
            End If
        End If

        Me._Frm.lblTitleKitakuShohinTanka.Visible = isVisible
        Me._Frm.numKitakuShohinTanka.Visible = isVisible
        Me._Frm.lblTitleKitakuShohinTankaTani.Visible = isVisible

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        With Me._Frm

            'タブ内の表示内容を初期化する
            Call Me.ClearControl(.tabGoodsMst)
            .sprGoodsDetail.CrearSpread()

            '初期値を設定
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd
            .optKosu.Checked = True

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit(ByVal existZaikoFlg As Boolean, ByVal existCustZaikoFlg As Boolean)

        Dim lock As Boolean = True

        With Me._Frm

            Call Me._ControlG.LockText(.txtCustCdL, lock)          '荷主コード(大)
            Call Me._ControlG.LockText(.txtCustCdM, lock)          '荷主コード(中)

            ' existCustZaikoFlg = True の場合
            ' (営業所コード、荷主コードL・M、商品キーが一致で
            '  (実予在庫個数、引当中個数、引当可能個数) のいずれかがゼロでないレコードありの場合)
            ' 荷主コードS・SS は編集不可とする
            Call Me._ControlG.LockText(.txtCustCdS, existCustZaikoFlg)  '荷主コード(小)
            Call Me._ControlG.LockText(.txtCustCdSS, existCustZaikoFlg) '荷主コード(極小)

            If existZaikoFlg = True Then

                '在庫テーブルに存在する場合
                Call Me._ControlG.LockComb(.cmbKosuTani, lock)         '個数単位
                Call Me._ControlG.LockNumber(.numIrisu, lock)          '入数
                Call Me._ControlG.LockNumber(.numHyojyunIrime, lock)   '標準入目
                Call Me._ControlG.LockComb(.cmbHyojyunIrimeTani, lock) '標準入目単位

            End If
        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .lblGoodsKey.TextValue = String.Empty
            .lblCreateDate.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty

            '依頼番号:012720 変更項目は複製対象外 依頼番号:013744【LMS】商品マスタ複製時の制御
            .numHizyu.Value = 0.7   '比重は初期値
            .cmbDokugeki.SelectedValue = String.Empty
            .cmbKouathugas.SelectedValue = String.Empty
            .cmbYakuziho.SelectedValue = String.Empty
            .cmbShobokiken.SelectedValue = String.Empty
            .txtShobo.TextValue = String.Empty
            .lblShobo.TextValue = String.Empty
            .txtUn.TextValue = String.Empty
            .txtPg.TextValue = String.Empty
            .lblClass1.TextValue = String.Empty
            .lblClass2.TextValue = String.Empty
            .lblClass3.TextValue = String.Empty
            .lblKaiyouosen.TextValue = String.Empty
            .txtKaiyouosen.TextValue = String.Empty

            '使用状態を可にする
            .cmbAVAL_YN.SelectedValue = "01"    'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

        End With

    End Sub

    ''' <summary>
    ''' 個数単位をLabelに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetKosuTani()

        Me._Frm.lblNisugata.TextValue = Me._Frm.cmbKosuTani.SelectedText

    End Sub

    ''' <summary>
    ''' 風袋重量設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHutaiJyuryo()

        '区分マスタから風袋重量を取得
        Dim selectStr As String = "KBN_GROUP_CD = 'N001' AND KBN_CD = '"
        selectStr = String.Concat(selectStr, Me._Frm.cmbHosotani.SelectedValue.ToString(), "' ")
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(selectStr)

        Dim HutaiJyuryo As Decimal = 0
        If 0 < drs.Length Then
            HutaiJyuryo = Convert.ToDecimal(drs(0).Item("VALUE1"))
            Me._Frm.lblCylFlg.TextValue = drs(0).Item("KBN_NM6").ToString
        End If

        Me._Frm.numHutaiJyuryo.Value = HutaiJyuryo

    End Sub

    ''' <summary>
    ''' ガス管理区分設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetGusKanri()

        '旧項目：ガス管理区分を高圧ガス区分の設定値に従い設定する。
        Dim GusKanriKbn As String = String.Empty
        '全て空白で設定
        Me._Frm.cmbGusKanri.SelectedValue = GusKanriKbn

    End Sub

    ''' <summary>
    ''' 危険品区分設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetKikenhin()

        '旧項目：危険品区分を消防危険品区分の設定値に従い設定する。
        Dim KikenhinKbn As String = String.Empty

        Select Case Me._Frm.cmbShobokiken.SelectedValue.ToString
            Case LMM100C.SHOBO_KIKEN_KIKEN

                KikenhinKbn = LMM100C.KIKEN_SHOBO

            Case LMM100C.SHOBO_KIKEN_SHITEIKANEN,
                 LMM100C.SHOBO_KIKEN_HIGAITO

                KikenhinKbn = LMM100C.KIKEN_IPPAN

        End Select

        Me._Frm.cmbKikenhin.SelectedValue = KikenhinKbn

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(商品Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.STATUS, "状態", 60, True)
        Public Shared AVAL_YN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.AVAL_YN, "使用状態", 60, False)            'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
        Public Shared AVAL_YN_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.AVAL_YN_NM, "使用状態", 60, True)       'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
        Public Shared BR_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.BR_NM, "営業所", 275, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CD, "荷主コード", 105, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_NM_L, "荷主名(大)", 200, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GOODS_CD, "商品コード", 145, True)
        Public Shared GOODS_NM_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GOODS_NM_1, "商品名", 200, True)
        Public Shared HYOJUN_IRIME As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HYOJUN_IRIME, "標準入目", 90, True)
        Public Shared IRIME_TANI_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.IRIME_TANI_CD, "入目単位", 115, True)
        Public Shared NISUGATA_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NISUGATA_CD, "荷姿", 115, True)
        Public Shared IRI_SU As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.IRI_SU, "入数", 80, True)
        Public Shared HYOJUN_JURYO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HYOJUN_JURYO, "標準重量", 100, True)
        Public Shared ACTUAL_VOLUME As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ACTUAL_VOLUME, "実容積", 100, True)
        Public Shared OCCUPY_VOLUME As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OCCUPY_VOLUME, "占有容積", 100, True)
        Public Shared ONDO_KANRI_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_NM, "温度管理", 70, True)
        Public Shared TANKA_GROUP_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.TANKA_GROUP_CD, "単価グループ" & vbCrLf & "コード", 100, True)
        Public Shared DOKUGEKI_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.DOKUGEKI_NM, "毒劇", 80, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHOBO_CD, "消防" & vbCrLf & "コード", 70, True)
        Public Shared SEIQT_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SEIQT_CD, "請求先" & vbCrLf & "コード", 70, True)
        Public Shared SEIQT_COMP_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SEIQT_COMP_NM, "請求先会社名", 160, True)
        Public Shared SEIQT_BUSHO_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SEIQT_BUSHO_NM, "請求先部署名", 160, True)
        Public Shared KIKEN_DATE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KIKEN_DATE, "危険品情報" & vbCrLf & "最終確認日", 80, True)
        Public Shared KIKEN_USER_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KIKEN_USER_NM, "危険品情報" & vbCrLf & "最終確認者", 160, True)
        Public Shared CUST_CATEGORY_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CATEGORY_2, "荷主カテゴリ2", 200, True)          'UPD 2019/06/21 006318

        '**** 隠し列 ****
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.BR_CD, "", 50, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CD_L, "", 50, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CD_M, "", 50, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CD_S, "", 50, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CD_SS, "", 50, False)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_NM_M, "", 50, False)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_NM_S, "", 50, False)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_NM_SS, "", 50, False)
        Public Shared GOODS_KEY As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GOODS_KEY, "", 50, False)
        Public Shared GOODS_NM_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GOODS_NM_2, "", 50, False)
        Public Shared GOODS_NM_3 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GOODS_NM_3, "", 50, False)
        Public Shared HYOJUN_IRIME_TANI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HYOJUN_IRIME_TANI, "", 50, False)
        Public Shared HOSO_TANI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOSO_TANI, "", 50, False)
        Public Shared NISUGATA_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NISUGATA_NM, "", 50, False)
        Public Shared ONDO_KANRI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_KBN, "", 50, False)
        Public Shared DOKUGEKI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.DOKUGEKI_KBN, "", 50, False)
        Public Shared KOUATHUGAS_KB As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KOUATHUGAS_KB, "", 50, False)
        Public Shared YAKUZIHO_KB As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.YAKUZIHO_KB, "", 50, False)
        Public Shared SHOBOKIKEN_KB As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHOBOKIKEN_KB, "", 50, False)
        Public Shared SHOBO_JOHO_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHOBO_JOHO_NM, "", 50, False)
        Public Shared CUST_CATEGORY_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CATEGORY_1, "", 50, False)
        'Public Shared CUST_CATEGORY_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_CATEGORY_2, "", 50, False)      'UPD 2019/06/21 006318
        Public Shared CUST_KANJO_KMK_CD_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_KANJO_KMK_CD_1, "", 50, False)
        Public Shared CUST_KANJO_KMK_CD_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CUST_KANJO_KMK_CD_2, "", 50, False)
        Public Shared BAR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.BAR_CD, "", 50, False)
        Public Shared KIKENHIN_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KIKENHIN_KBN, "", 50, False)
        Public Shared UN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.UN, "", 50, False)
        Public Shared PG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.PG, "", 50, False)
        Public Shared CLASS_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CLASS_1, "", 50, False)
        Public Shared CLASS_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CLASS_2, "", 50, False)
        Public Shared CLASS_3 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CLASS_3, "", 50, False)
        Public Shared KAIYOUOSEN_KB As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KAIYOUOSEN_KB, "", 50, False)
        Public Shared KAIYOUOSEN_KB_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KAIYOUOSEN_KB_NM, "", 50, False)
        Public Shared KAGAKUBUSITU_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KAGAKUBUSITU_KBN, "", 50, False)
        Public Shared GUS_KANRI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.GUS_KANRI_KBN, "", 50, False)
        Public Shared ONDO_KBN_UNSO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KBN_UNSO, "", 50, False)
        Public Shared ONDO_NM_UNSO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_NM_UNSO, "", 50, False)
        Public Shared ONDO_MAX As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_MAX, "", 50, False)
        Public Shared ONDO_MIN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_MIN, "", 50, False)
        Public Shared ONDO_KANRI_START_HOKAN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_START_HOKAN, "", 50, False)
        Public Shared ONDO_KANRI_END_HOKAN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_END_HOKAN, "", 50, False)
        Public Shared ONDO_KANRI_START_UNSO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_START_UNSO, "", 50, False)
        Public Shared ONDO_KANRI_END_UNSO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.ONDO_KANRI_END_UNSO, "", 50, False)
        Public Shared SOKO_KYOKAI_HIN_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SOKO_KYOKAI_HIN_KBN, "", 50, False)
        Public Shared HIKIATE_TANI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HIKIATE_TANI_KBN, "", 50, False)
        Public Shared KOSU_TANI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KOSU_TANI_KBN, "", 50, False)
        Public Shared KOSU_TANI_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KOSU_TANI_NM, "", 50, False)
        Public Shared HUTAI_JURYO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HUTAI_JURYO, "", 50, False)
        Public Shared PALETTO_HOSOKOSU As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.PALETTO_HOSOKOSU, "", 50, False)
        Public Shared INNER_PKG_NB As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.INNER_PKG_NB, "", 50, False)
        Public Shared HIZYU As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HIZYU, "", 50, False)
        Public Shared HYOJYUN_YOSEKI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HYOJYUN_YOSEKI, "", 50, False)
        Public Shared NYUKAJI_SAGYO_KBN_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NYUKAJI_SAGYO_KBN_1, "", 50, False)
        Public Shared NYUKAJI_SAGYO_KBN_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NYUKAJI_SAGYO_KBN_2, "", 50, False)
        Public Shared NYUKAJI_SAGYO_KBN_3 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NYUKAJI_SAGYO_KBN_3, "", 50, False)
        Public Shared NYUKAJI_SAGYO_KBN_4 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NYUKAJI_SAGYO_KBN_4, "", 50, False)
        Public Shared NYUKAJI_SAGYO_KBN_5 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NYUKAJI_SAGYO_KBN_5, "", 50, False)
        Public Shared SHUKKAJI_SAGYO_KBN_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_SAGYO_KBN_1, "", 50, False)
        Public Shared SHUKKAJI_SAGYO_KBN_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_SAGYO_KBN_2, "", 50, False)
        Public Shared SHUKKAJI_SAGYO_KBN_3 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_SAGYO_KBN_3, "", 50, False)
        Public Shared SHUKKAJI_SAGYO_KBN_4 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_SAGYO_KBN_4, "", 50, False)
        Public Shared SHUKKAJI_SAGYO_KBN_5 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_SAGYO_KBN_5, "", 50, False)
        Public Shared KONPO_SAGYO_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KONPO_SAGYO_CD, "", 50, False)
        Public Shared KONPO_SAGYO_NM As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KONPO_SAGYO_NM, "", 50, False)
        Public Shared HUTAI_KASAN_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HUTAI_KASAN_FLG, "", 50, False)
        Public Shared SITEI_NOHINSHO_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SITEI_NOHINSHO_KBN, "", 50, False)
        Public Shared BUNSEKI_HYO_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.BUNSEKI_HYO_KBN, "", 50, False)
        Public Shared LOT_KANRI_LEVEL_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.LOT_KANRI_LEVEL_CD, "", 50, False)
        Public Shared SHOMIKIGEN_KANRI_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHOMIKIGEN_KANRI_CD, "", 50, False)
        Public Shared SEIZOBI_KANRI_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SEIZOBI_KANRI_CD, "", 50, False)
        Public Shared KITEI_HORYUHIN_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KITEI_HORYUHIN_KBN, "", 50, False)
        Public Shared KITAKU_KAKAKU_TANI_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KITAKU_KAKAKU_TANI_KBN, "", 50, False)
        Public Shared KITAKU_SHOHIN_TANKA As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.KITAKU_SHOHIN_TANKA, "", 50, False)
        Public Shared HACCHUTEN_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HACCHUTEN_KBN, "", 50, False)
        Public Shared HACCHU_SURYO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HACCHU_SURYO, "", 50, False)
        Public Shared NISONIN_CD_L As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NISONIN_CD_L, "", 50, False)
        Public Shared NISONIN_NM_L As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NISONIN_NM_L, "", 50, False)
        Public Shared SEIQT_DTL_SHUTURYOKU_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SEIQT_DTL_SHUTURYOKU_FLG, "", 50, False)
        Public Shared UNSO_HOKEN_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.UNSO_HOKEN_FLG, "", 50, False)   'ADD 2017/07/17 依頼番号 001540 
        Public Shared HIKIATE_CHUIHIN_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HIKIATE_CHUIHIN_FLG, "", 50, False)
        Public Shared SHUKKAJI_CHUIJIKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_CHUIJIKO, "", 50, False)
        Public Shared NIHUDA_INSATU_SU As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIHUDA_INSATU_SU, "", 50, False)
        Public Shared SHOHIKIGEN_KINSHIBI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHOHIKIGEN_KINSHIBI, "", 50, False)
        Public Shared TEKIYO_START_DATE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.TEKIYO_START_DATE, "", 50, False)
        Public Shared HOKANRYO_TUJO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TUJO, "", 50, False)
        Public Shared HOKANRYO_TATE_KBN_NASHI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TATE_KBN_NASHI, "", 50, False)
        Public Shared HOKANRYO_TEION As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TEION, "", 50, False)
        Public Shared HOKANRYO_TATE_KBN_ARI As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TATE_KBN_ARI, "", 50, False)
        Public Shared NIYAKURYO_NYUKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_NYUKO, "", 50, False)
        Public Shared NIYAKURYO_NYUKO_TATE_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_NYUKO_TATE_KBN, "", 50, False)
        Public Shared NIYAKURYO_SHUKKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_SHUKKO, "", 50, False)
        Public Shared NIYAKURYO_SHUKKO_TATE_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_SHUKKO_TATE_KBN, "", 50, False)
        Public Shared NIYAKU_MIN_NYUKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKU_MIN_NYUKO, "", 50, False)
        Public Shared NIYAKU_MIN_SHUKKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKU_MIN_SHUKKO, "", 50, False)
        Public Shared HOKANRYO_TUJO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TUJO_CURR_CD, "", 50, False)
        Public Shared HOKANRYO_TEION_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HOKANRYO_TEION_CURR_CD, "", 50, False)
        Public Shared NIYAKURYO_NYUKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_NYUKO_CURR_CD, "", 50, False)
        Public Shared NIYAKURYO_SHUKKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKURYO_SHUKKO_CURR_CD, "", 50, False)
        Public Shared NIYAKU_MIN_NYUKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.NIYAKU_MIN_NYUKO_CURR_CD, "", 50, False)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CREATE_DATE, "", 50, False)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CREATE_USER, "", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.UPDATE_DATE, "", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.UPDATE_USER, "", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.UPDATE_TIME, "", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SYS_DEL_FLG, "", 50, False)
        Public Shared SIZE_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SIZE_KBN, "", 50, False) '検証結果№70対応(2011.09.08)
        '20150729 常平add
        Public Shared OCR_GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OCR_GOODS_CD_CUST, "", 30, False)
        Public Shared OCR_GOODS_CD_NM1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OCR_GOODS_CD_NM1, "", 30, False)
        Public Shared OCR_GOODS_CD_NM2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OCR_GOODS_CD_NM2, "", 30, False)
        Public Shared OCR_GOODS_CD_STD_IRIME As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OCR_GOODS_CD_STD_IRIME, "", 30, False)

#If True Then   ' 要望番号2471対応 added 2015.12.14 inoue
        Public Shared OUTER_PACKEGE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.OUTER_PACKEGE, "", 0, False)
#End If
        Public Shared WIDTH As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.WIDTH, "", 50, False)
        Public Shared DEPTH As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.DEPTH, "", 50, False)
        Public Shared HEIGHT As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.HEIGHT, "", 50, False)
        Public Shared CYL_FLG As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.CYL_FLG, "", 50, False)

        '要望対応001995 端数出荷時作業区分対応
        Public Shared SHUKKAJI_HASU_SAGYO_KBN_1 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_HASU_SAGYO_KBN_1, "", 50, False)
        Public Shared SHUKKAJI_HASU_SAGYO_KBN_2 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_HASU_SAGYO_KBN_2, "", 50, False)
        Public Shared SHUKKAJI_HASU_SAGYO_KBN_3 As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsColumnIndex.SHUKKAJI_HASU_SAGYO_KBN_3, "", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(商品明細Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsDtlDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsDetailColumnIndex.DEF, " ", 20, True)
        Public Shared EDA_NO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsDetailColumnIndex.EDA_NO, "枝番", 60, True)
        Public Shared YOTO_KBN As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN, "用途区分", 120, True)
        Public Shared SETTEI_VALUE As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE, "設定値", 430, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM100C.SprGoodsDetailColumnIndex.BIKO, "備考", 430, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '商品Spreadの初期化処理
        Call Me.InitGoodsSpread()

        '商品明細Spreadの初期化処理
        Call Me.InitGoodsDtlSpread()

    End Sub

    ''' <summary>
    ''' 検索結果を商品Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprGoods

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numIrime As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim numIrSu As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, True, 0, , ",")
            Dim numKg As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim numVolume As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999999, True, 6, , ",")  'MOD 2018/11/09 要望番号002599 maxをDBに合わせて変更

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprGoodsDef.DEF.ColNo, def)
                .SetCellStyle(i, sprGoodsDef.STATUS.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.AVAL_YN.ColNo, lblL)           'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
                .SetCellStyle(i, sprGoodsDef.AVAL_YN_NM.ColNo, lblL)        'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
                .SetCellStyle(i, sprGoodsDef.BR_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_NM_L.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GOODS_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GOODS_NM_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HYOJUN_IRIME.ColNo, numIrime)
                .SetCellStyle(i, sprGoodsDef.IRIME_TANI_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NISUGATA_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.IRI_SU.ColNo, numIrSu)
                .SetCellStyle(i, sprGoodsDef.HYOJUN_JURYO.ColNo, numKg)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.TANKA_GROUP_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.DOKUGEKI_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHOBO_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SEIQT_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SEIQT_COMP_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SEIQT_BUSHO_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KIKEN_DATE.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KIKEN_USER_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ACTUAL_VOLUME.ColNo, numVolume)
                .SetCellStyle(i, sprGoodsDef.OCCUPY_VOLUME.ColNo, numVolume)

                '**** 隠し列 ****
                .SetCellStyle(i, sprGoodsDef.BR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CD_L.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CD_M.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CD_S.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CD_SS.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_NM_M.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_NM_S.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_NM_SS.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GOODS_KEY.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GOODS_NM_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GOODS_NM_3.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HYOJUN_IRIME_TANI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOSO_TANI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NISUGATA_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.DOKUGEKI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHOBO_JOHO_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CATEGORY_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_CATEGORY_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_KANJO_KMK_CD_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CUST_KANJO_KMK_CD_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KOUATHUGAS_KB.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.YAKUZIHO_KB.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHOBOKIKEN_KB.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.BAR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KIKENHIN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.UN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.PG.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CLASS_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CLASS_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CLASS_3.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KAIYOUOSEN_KB.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KAIYOUOSEN_KB_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KAGAKUBUSITU_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.GUS_KANRI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KBN_UNSO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_NM_UNSO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_MAX.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_MIN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_START_HOKAN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_END_HOKAN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_START_UNSO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.ONDO_KANRI_END_UNSO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SOKO_KYOKAI_HIN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HIKIATE_TANI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KOSU_TANI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KOSU_TANI_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HUTAI_JURYO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.PALETTO_HOSOKOSU.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.INNER_PKG_NB.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HIZYU.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HYOJYUN_YOSEKI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_3.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_4.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_5.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_3.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_4.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_5.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KONPO_SAGYO_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KONPO_SAGYO_NM.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HUTAI_KASAN_FLG.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SITEI_NOHINSHO_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.BUNSEKI_HYO_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.LOT_KANRI_LEVEL_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHOMIKIGEN_KANRI_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SEIZOBI_KANRI_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KITEI_HORYUHIN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KITAKU_KAKAKU_TANI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.KITAKU_SHOHIN_TANKA.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HACCHUTEN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HACCHU_SURYO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NISONIN_CD_L.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NISONIN_NM_L.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SEIQT_DTL_SHUTURYOKU_FLG.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_CHUIJIKO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIHUDA_INSATU_SU.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHOHIKIGEN_KINSHIBI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.TEKIYO_START_DATE.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TUJO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TATE_KBN_NASHI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TEION.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TATE_KBN_ARI.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_NYUKO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_NYUKO_TATE_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_SHUKKO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_SHUKKO_TATE_KBN.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKU_MIN_NYUKO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKU_MIN_SHUKKO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TUJO_CURR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HOKANRYO_TEION_CURR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.NIYAKU_MIN_NYUKO_CURR_CD.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CREATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CREATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.UPDATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.UPDATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.UPDATE_TIME.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SYS_DEL_FLG.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SIZE_KBN.ColNo, lblL) '検証結果№70対応(2011.09.08)
                '20150729 常平add
                .SetCellStyle(i, sprGoodsDef.OCR_GOODS_CD_CUST.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.OCR_GOODS_CD_NM1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.OCR_GOODS_CD_NM2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.OCR_GOODS_CD_STD_IRIME.ColNo, lblL)
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
                .SetCellStyle(i, sprGoodsDef.OUTER_PACKEGE.ColNo, lblL)
#End If
                .SetCellStyle(i, sprGoodsDef.WIDTH.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.HEIGHT.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.DEPTH.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.CYL_FLG.ColNo, lblL)

                '要望対応1995 端数出荷時作業区分対応
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_1.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_2.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_3.ColNo, lblL)
                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprGoodsDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprGoodsDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprGoodsDef.BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprGoodsDef.AVAL_YN.ColNo, dr.Item("AVAL_YN").ToString())          'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
                .SetCellValue(i, sprGoodsDef.AVAL_YN_NM.ColNo, dr.Item("AVAL_YN_NM").ToString())    'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
                .SetCellValue(i, sprGoodsDef.CUST_CD.ColNo, dr.Item("CUST_CD").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_CD.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_NM_1.ColNo, dr.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, sprGoodsDef.HYOJUN_IRIME.ColNo, dr.Item("STD_IRIME_NB").ToString())
                .SetCellValue(i, sprGoodsDef.IRIME_TANI_CD.ColNo, dr.Item("STD_IRIME_NM").ToString())
                .SetCellValue(i, sprGoodsDef.NISUGATA_CD.ColNo, dr.Item("PKG_UT_NM").ToString())
                .SetCellValue(i, sprGoodsDef.IRI_SU.ColNo, dr.Item("PKG_NB").ToString())
                .SetCellValue(i, sprGoodsDef.HYOJUN_JURYO.ColNo, dr.Item("STD_WT_KGS").ToString())
                .SetCellValue(i, sprGoodsDef.ACTUAL_VOLUME.ColNo, dr.Item("ACTUAL_VOLUME").ToString())
                .SetCellValue(i, sprGoodsDef.OCCUPY_VOLUME.ColNo, dr.Item("OCCUPY_VOLUME").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_NM.ColNo, dr.Item("ONDO_KB_NM").ToString())
                .SetCellValue(i, sprGoodsDef.TANKA_GROUP_CD.ColNo, dr.Item("UP_GP_CD_1").ToString())
                .SetCellValue(i, sprGoodsDef.DOKUGEKI_NM.ColNo, dr.Item("DOKU_KB_NM").ToString())
                .SetCellValue(i, sprGoodsDef.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue(i, sprGoodsDef.SEIQT_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, sprGoodsDef.SEIQT_COMP_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, sprGoodsDef.SEIQT_BUSHO_NM.ColNo, dr.Item("SEIQTO_BUSYO_NM").ToString())
                .SetCellValue(i, sprGoodsDef.KIKEN_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("KIKEN_DATE").ToString()))
                .SetCellValue(i, sprGoodsDef.KIKEN_USER_NM.ColNo, dr.Item("KIKEN_USER_NM").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CATEGORY_2.ColNo, dr.Item("SEARCH_KEY_2").ToString())    'UPD 2019/06/21 006318

                '**** 隠し列 ****
                .SetCellValue(i, sprGoodsDef.BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_NM_S.ColNo, dr.Item("CUST_NM_S").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_NM_SS.ColNo, dr.Item("CUST_NM_SS").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_KEY.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_NM_2.ColNo, dr.Item("GOODS_NM_2").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_NM_3.ColNo, dr.Item("GOODS_NM_3").ToString())
                .SetCellValue(i, sprGoodsDef.HYOJUN_IRIME_TANI.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprGoodsDef.HOSO_TANI.ColNo, dr.Item("PKG_UT").ToString())
                .SetCellValue(i, sprGoodsDef.NISUGATA_NM.ColNo, dr.Item("PKG_UT_NM").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_KBN.ColNo, dr.Item("ONDO_KB").ToString())
                .SetCellValue(i, sprGoodsDef.DOKUGEKI_KBN.ColNo, dr.Item("DOKU_KB").ToString())
                .SetCellValue(i, sprGoodsDef.SHOBO_JOHO_NM.ColNo, dr.Item("SHOBO_INFO").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_CATEGORY_1.ColNo, dr.Item("SEARCH_KEY_1").ToString())
                '.SetCellValue(i, sprGoodsDef.CUST_CATEGORY_2.ColNo, dr.Item("SEARCH_KEY_2").ToString())     'UPD 2019/06/21 006318
                .SetCellValue(i, sprGoodsDef.CUST_KANJO_KMK_CD_1.ColNo, dr.Item("CUST_COST_CD1").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_KANJO_KMK_CD_2.ColNo, dr.Item("CUST_COST_CD2").ToString())
                .SetCellValue(i, sprGoodsDef.KOUATHUGAS_KB.ColNo, dr.Item("KOUATHUGAS_KB").ToString())
                .SetCellValue(i, sprGoodsDef.YAKUZIHO_KB.ColNo, dr.Item("YAKUZIHO_KB").ToString())
                .SetCellValue(i, sprGoodsDef.SHOBOKIKEN_KB.ColNo, dr.Item("SHOBOKIKEN_KB").ToString())
                .SetCellValue(i, sprGoodsDef.BAR_CD.ColNo, dr.Item("JAN_CD").ToString())
                .SetCellValue(i, sprGoodsDef.KIKENHIN_KBN.ColNo, dr.Item("KIKEN_KB").ToString())
                .SetCellValue(i, sprGoodsDef.UN.ColNo, dr.Item("UN").ToString())
                .SetCellValue(i, sprGoodsDef.PG.ColNo, dr.Item("PG_KB").ToString())
                .SetCellValue(i, sprGoodsDef.CLASS_1.ColNo, dr.Item("CLASS_1").ToString())
                .SetCellValue(i, sprGoodsDef.CLASS_2.ColNo, dr.Item("CLASS_2").ToString())
                .SetCellValue(i, sprGoodsDef.CLASS_3.ColNo, dr.Item("CLASS_3").ToString())
                .SetCellValue(i, sprGoodsDef.KAIYOUOSEN_KB.ColNo, dr.Item("KAIYOUOSEN_KB").ToString())
                .SetCellValue(i, sprGoodsDef.KAIYOUOSEN_KB_NM.ColNo, dr.Item("KAIYOUOSEN_KB_NM").ToString())
                .SetCellValue(i, sprGoodsDef.KAGAKUBUSITU_KBN.ColNo, dr.Item("CHEM_MTRL_KB").ToString())
                .SetCellValue(i, sprGoodsDef.GUS_KANRI_KBN.ColNo, dr.Item("GAS_KANRI_KB").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KBN_UNSO.ColNo, dr.Item("UNSO_ONDO_KB").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_MAX.ColNo, dr.Item("ONDO_MX").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_MIN.ColNo, dr.Item("ONDO_MM").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_START_HOKAN.ColNo, dr.Item("ONDO_STR_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_END_HOKAN.ColNo, dr.Item("ONDO_END_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_START_UNSO.ColNo, dr.Item("ONDO_UNSO_STR_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.ONDO_KANRI_END_UNSO.ColNo, dr.Item("ONDO_UNSO_END_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.SOKO_KYOKAI_HIN_KBN.ColNo, dr.Item("KYOKAI_GOODS_KB").ToString())
                .SetCellValue(i, sprGoodsDef.HIKIATE_TANI_KBN.ColNo, dr.Item("ALCTD_KB").ToString())
                .SetCellValue(i, sprGoodsDef.KOSU_TANI_KBN.ColNo, dr.Item("NB_UT").ToString())
                .SetCellValue(i, sprGoodsDef.KOSU_TANI_NM.ColNo, dr.Item("NB_UT_NM").ToString())
                .SetCellValue(i, sprGoodsDef.HUTAI_JURYO.ColNo, dr.Item("NT_GR_CONV_RATE").ToString())
                .SetCellValue(i, sprGoodsDef.PALETTO_HOSOKOSU.ColNo, dr.Item("PLT_PER_PKG_UT").ToString())
                .SetCellValue(i, sprGoodsDef.INNER_PKG_NB.ColNo, dr.Item("INNER_PKG_NB").ToString())
                .SetCellValue(i, sprGoodsDef.HIZYU.ColNo, dr.Item("HIZYU").ToString())
                .SetCellValue(i, sprGoodsDef.HYOJYUN_YOSEKI.ColNo, dr.Item("STD_CBM").ToString())
                .SetCellValue(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_1.ColNo, dr.Item("INKA_KAKO_SAGYO_KB_1").ToString())
                .SetCellValue(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_2.ColNo, dr.Item("INKA_KAKO_SAGYO_KB_2").ToString())
                .SetCellValue(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_3.ColNo, dr.Item("INKA_KAKO_SAGYO_KB_3").ToString())
                .SetCellValue(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_4.ColNo, dr.Item("INKA_KAKO_SAGYO_KB_4").ToString())
                .SetCellValue(i, sprGoodsDef.NYUKAJI_SAGYO_KBN_5.ColNo, dr.Item("INKA_KAKO_SAGYO_KB_5").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_1.ColNo, dr.Item("OUTKA_KAKO_SAGYO_KB_1").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_2.ColNo, dr.Item("OUTKA_KAKO_SAGYO_KB_2").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_3.ColNo, dr.Item("OUTKA_KAKO_SAGYO_KB_3").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_4.ColNo, dr.Item("OUTKA_KAKO_SAGYO_KB_4").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_SAGYO_KBN_5.ColNo, dr.Item("OUTKA_KAKO_SAGYO_KB_5").ToString())
                .SetCellValue(i, sprGoodsDef.KONPO_SAGYO_CD.ColNo, dr.Item("PKG_SAGYO").ToString())
                .SetCellValue(i, sprGoodsDef.KONPO_SAGYO_NM.ColNo, dr.Item("PKG_SAGYO_NM").ToString())
                .SetCellValue(i, sprGoodsDef.HUTAI_KASAN_FLG.ColNo, dr.Item("TARE_YN").ToString())
                .SetCellValue(i, sprGoodsDef.SITEI_NOHINSHO_KBN.ColNo, dr.Item("SP_NHS_YN").ToString())
                .SetCellValue(i, sprGoodsDef.BUNSEKI_HYO_KBN.ColNo, dr.Item("COA_YN").ToString())
                .SetCellValue(i, sprGoodsDef.LOT_KANRI_LEVEL_CD.ColNo, dr.Item("LOT_CTL_KB").ToString())
                .SetCellValue(i, sprGoodsDef.SHOMIKIGEN_KANRI_CD.ColNo, dr.Item("LT_DATE_CTL_KB").ToString())
                .SetCellValue(i, sprGoodsDef.SEIZOBI_KANRI_CD.ColNo, dr.Item("CRT_DATE_CTL_KB").ToString())
                .SetCellValue(i, sprGoodsDef.KITEI_HORYUHIN_KBN.ColNo, dr.Item("DEF_SPD_KB").ToString())
                .SetCellValue(i, sprGoodsDef.KITAKU_KAKAKU_TANI_KBN.ColNo, dr.Item("KITAKU_AM_UT_KB").ToString())
                .SetCellValue(i, sprGoodsDef.KITAKU_SHOHIN_TANKA.ColNo, dr.Item("KITAKU_GOODS_UP").ToString())
                .SetCellValue(i, sprGoodsDef.HACCHUTEN_KBN.ColNo, dr.Item("ORDER_KB").ToString())
                .SetCellValue(i, sprGoodsDef.HACCHU_SURYO.ColNo, dr.Item("ORDER_NB").ToString())
                .SetCellValue(i, sprGoodsDef.NISONIN_CD_L.ColNo, dr.Item("SHIP_CD_L").ToString())
                .SetCellValue(i, sprGoodsDef.NISONIN_NM_L.ColNo, dr.Item("SHIP_NM_L").ToString())
                .SetCellValue(i, sprGoodsDef.SEIQT_DTL_SHUTURYOKU_FLG.ColNo, dr.Item("SKYU_MEI_YN").ToString())
                .SetCellValue(i, sprGoodsDef.UNSO_HOKEN_FLG.ColNo, dr.Item("UNSO_HOKEN_YN").ToString())            'ADD 2018/07/17
                .SetCellValue(i, sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo, dr.Item("HIKIATE_ALERT_YN").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_CHUIJIKO.ColNo, dr.Item("OUTKA_ATT").ToString())
                .SetCellValue(i, sprGoodsDef.NIHUDA_INSATU_SU.ColNo, dr.Item("PRINT_NB").ToString())
                .SetCellValue(i, sprGoodsDef.SHOHIKIGEN_KINSHIBI.ColNo, dr.Item("CONSUME_PERIOD_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.TEKIYO_START_DATE.ColNo, dr.Item("STR_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TUJO.ColNo, dr.Item("STORAGE_1").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TATE_KBN_NASHI.ColNo, dr.Item("STORAGE_KB1").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TEION.ColNo, dr.Item("STORAGE_2").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TATE_KBN_ARI.ColNo, dr.Item("STORAGE_KB2").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_NYUKO.ColNo, dr.Item("HANDLING_IN").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_NYUKO_TATE_KBN.ColNo, dr.Item("HANDLING_IN_KB").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_SHUKKO.ColNo, dr.Item("HANDLING_OUT").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_SHUKKO_TATE_KBN.ColNo, dr.Item("HANDLING_OUT_KB").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKU_MIN_NYUKO.ColNo, dr.Item("MINI_TEKI_IN_AMO").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKU_MIN_SHUKKO.ColNo, dr.Item("MINI_TEKI_OUT_AMO").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TUJO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.HOKANRYO_TEION_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.NIYAKU_MIN_NYUKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprGoodsDef.CREATE_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, sprGoodsDef.UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprGoodsDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, sprGoodsDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprGoodsDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, sprGoodsDef.SIZE_KBN.ColNo, dr.Item("SIZE_KB").ToString()) '検証結果№70対応(2011.09.08)
                '20150729 常平add
                .SetCellValue(i, sprGoodsDef.OCR_GOODS_CD_CUST.ColNo, dr.Item("OCR_GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprGoodsDef.OCR_GOODS_CD_NM1.ColNo, dr.Item("OCR_GOODS_CD_NM1").ToString())
                .SetCellValue(i, sprGoodsDef.OCR_GOODS_CD_NM2.ColNo, dr.Item("OCR_GOODS_CD_NM2").ToString())
                .SetCellValue(i, sprGoodsDef.OCR_GOODS_CD_STD_IRIME.ColNo, dr.Item("OCR_GOODS_CD_STD_IRIME").ToString())
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
                .SetCellValue(i, sprGoodsDef.OUTER_PACKEGE.ColNo, dr.Item(LMM100C.LMM100OUT_CNAME.OUTER_PKG).ToString())
#End If
                .SetCellValue(i, sprGoodsDef.WIDTH.ColNo, dr.Item("WIDTH").ToString())
                .SetCellValue(i, sprGoodsDef.HEIGHT.ColNo, dr.Item("HEIGHT").ToString())
                .SetCellValue(i, sprGoodsDef.DEPTH.ColNo, dr.Item("DEPTH").ToString())
                .SetCellValue(i, sprGoodsDef.CYL_FLG.ColNo, dr.Item("CYL_FLG").ToString())

                .SetCellValue(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_1.ColNo, dr.Item("OUTKA_HASU_SAGYO_KB_1").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_2.ColNo, dr.Item("OUTKA_HASU_SAGYO_KB_2").ToString())
                .SetCellValue(i, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_3.ColNo, dr.Item("OUTKA_HASU_SAGYO_KB_3").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 表示内容を商品明細Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadDtl(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprGoodsDetail

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True
            Dim edit As Boolean = unlock

            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                edit = unlock
                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.COPY_REC
                        '複写データ時、枝番初期化
                        For i As Integer = 0 To lngcnt - 1
                            With dt.Rows(i)
                                .Item("GOODS_CD_NRS_EDA") = String.Empty
                            End With
                        Next
                End Select
            Else
                edit = True
            End If

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim yotoKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Y007, edit)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, edit)

            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprGoodsDtlDef.DEF.ColNo, def)
                .SetCellStyle(i, sprGoodsDtlDef.EDA_NO.ColNo, lblL)
                .SetCellStyle(i, sprGoodsDtlDef.YOTO_KBN.ColNo, yotoKbn)
                .SetCellStyle(i, sprGoodsDtlDef.SETTEI_VALUE.ColNo, text)
                .SetCellStyle(i, sprGoodsDtlDef.BIKO.ColNo, text)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprGoodsDtlDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprGoodsDtlDef.EDA_NO.ColNo, dr.Item("GOODS_CD_NRS_EDA").ToString())
                .SetCellValue(i, sprGoodsDtlDef.YOTO_KBN.ColNo, dr.Item("SUB_KB").ToString())
                .SetCellValue(i, sprGoodsDtlDef.SETTEI_VALUE.ColNo, dr.Item("SET_NAIYO").ToString())
                .SetCellValue(i, sprGoodsDtlDef.BIKO.ColNo, dr.Item("REMARK").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 商品明細Spreadに一行追加する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRow()

        Dim spr As LMSpread = Me._Frm.sprGoodsDetail

        With spr

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim yotoKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Y007, unlock)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, unlock)

            Dim addRow As Integer = .ActiveSheet.Rows.Count - 1

            'セルスタイル設定
            '**** 表示列 ****
            .SetCellStyle(addRow, sprGoodsDtlDef.DEF.ColNo, def)
            .SetCellStyle(addRow, sprGoodsDtlDef.EDA_NO.ColNo, lblL)
            .SetCellStyle(addRow, sprGoodsDtlDef.YOTO_KBN.ColNo, yotoKbn)
            .SetCellStyle(addRow, sprGoodsDtlDef.SETTEI_VALUE.ColNo, text)
            .SetCellStyle(addRow, sprGoodsDtlDef.BIKO.ColNo, text)

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(addRow, sprGoodsDtlDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(addRow, sprGoodsDtlDef.EDA_NO.ColNo, String.Empty)
            .SetCellValue(addRow, sprGoodsDtlDef.YOTO_KBN.ColNo, String.Empty)
            .SetCellValue(addRow, sprGoodsDtlDef.SETTEI_VALUE.ColNo, String.Empty)
            .SetCellValue(addRow, sprGoodsDtlDef.BIKO.ColNo, String.Empty)

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 商品明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprGoodsDetail.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 商品スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitGoodsSpread()

        '商品Spreadの初期値設定
        Dim sprGoods As LMSpread = Me._Frm.sprGoods
        Dim dr As DataRow
        With sprGoods

            'スプレッドの行をクリア
            .CrearSpread()

#If False Then ' 要望番号2471対応 changed 2015.12.14 inoue
            '列数設定 20150729常平変更
            .ActiveSheet.ColumnCount = 121
#Else
            .ActiveSheet.ColumnCount = LMM100C.SprGoodsColumnIndex.COLUMN_COUNT
#End If

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprGoodsDef)
            .SetColProperty(New sprGoodsDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .ActiveSheet.FrozenColumnCount = sprGoodsDef.GOODS_CD.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprGoods)

            '**** 表示列 ****
            .SetCellStyle(0, sprGoodsDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_S051, False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If dr.Item("LOCK_FLG").ToString.Equals("01") Then
                .SetCellStyle(0, sprGoodsDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprGoods, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprGoodsDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprGoods, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .SetCellStyle(0, sprGoodsDef.AVAL_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, "K017", False))                        'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

            .SetCellStyle(0, sprGoodsDef.CUST_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUM_ALPHA, 11, False))
            .SetCellStyle(0, sprGoodsDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprGoodsDef.GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprGoodsDef.GOODS_NM_1.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .SetCellStyle(0, sprGoodsDef.HYOJUN_IRIME.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.IRIME_TANI_CD.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_I001, False))
            .SetCellStyle(0, sprGoodsDef.NISUGATA_CD.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_N001, False))
            .SetCellStyle(0, sprGoodsDef.IRI_SU.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HYOJUN_JURYO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ACTUAL_VOLUME.ColNo, LMSpreadUtility.GetNumberCell(sprGoods, 0, 999999.999999, False, 6, , ","))   'MOD 2018/11/09 要望番号002599 cellStyleを変更
            .SetCellStyle(0, sprGoodsDef.OCCUPY_VOLUME.ColNo, LMSpreadUtility.GetNumberCell(sprGoods, 0, 999999.999999, False, 6, , ","))   'MOD 2018/11/09 要望番号002599 cellStyleを変更
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_O002, False))
            .SetCellStyle(0, sprGoodsDef.TANKA_GROUP_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUM_ALPHA, 3, False))
            .SetCellStyle(0, sprGoodsDef.DOKUGEKI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprGoods, LMKbnConst.KBN_G001, False))
            .SetCellStyle(0, sprGoodsDef.SHOBO_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUMBER, 3, False))
            .SetCellStyle(0, sprGoodsDef.SEIQT_CD.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.HAN_NUM_ALPHA, 7, False))
            .SetCellStyle(0, sprGoodsDef.SEIQT_COMP_NM.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprGoodsDef.SEIQT_BUSHO_NM.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprGoodsDef.KIKEN_DATE.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KIKEN_USER_NM.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX_IME_OFF, 60, False))
            .SetCellStyle(0, sprGoodsDef.CUST_CATEGORY_2.ColNo, LMSpreadUtility.GetTextCell(sprGoods, InputControl.ALL_MIX_IME_OFF, 25, False)) 'UPD 2019/06/21 006318


            '**** 隠し列 ****
            .SetCellStyle(0, sprGoodsDef.BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_CD_S.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_CD_SS.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_NM_M.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_NM_S.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_NM_SS.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.GOODS_KEY.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.GOODS_NM_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.GOODS_NM_3.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HYOJUN_IRIME_TANI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOSO_TANI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NISUGATA_NM.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.DOKUGEKI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHOBO_JOHO_NM.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_CATEGORY_1.ColNo, lbl)
            '.SetCellStyle(0, sprGoodsDef.CUST_CATEGORY_2.ColNo, lbl)            'UPD 2019/06/21 006318
            .SetCellStyle(0, sprGoodsDef.CUST_KANJO_KMK_CD_1.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CUST_KANJO_KMK_CD_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.BAR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KIKENHIN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.UN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.PG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CLASS_1.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CLASS_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CLASS_3.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KAIYOUOSEN_KB.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KAIYOUOSEN_KB_NM.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SIZE_KBN.ColNo, lbl) '検証結果№70対応(2011.09.08)
#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
            .SetCellStyle(0, sprGoodsDef.OUTER_PACKEGE.ColNo, lbl)
#End If
            .SetCellStyle(0, sprGoodsDef.KOUATHUGAS_KB.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.YAKUZIHO_KB.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHOBOKIKEN_KB.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KAGAKUBUSITU_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.GUS_KANRI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KBN_UNSO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_NM_UNSO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_MAX.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_MIN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_START_HOKAN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_END_HOKAN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_START_UNSO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.ONDO_KANRI_END_UNSO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SOKO_KYOKAI_HIN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HIKIATE_TANI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KOSU_TANI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KOSU_TANI_NM.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HUTAI_JURYO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.PALETTO_HOSOKOSU.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.INNER_PKG_NB.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HIZYU.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HYOJYUN_YOSEKI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NYUKAJI_SAGYO_KBN_1.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NYUKAJI_SAGYO_KBN_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NYUKAJI_SAGYO_KBN_3.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NYUKAJI_SAGYO_KBN_4.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NYUKAJI_SAGYO_KBN_5.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_SAGYO_KBN_1.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_SAGYO_KBN_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_SAGYO_KBN_3.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_SAGYO_KBN_4.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_SAGYO_KBN_5.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KONPO_SAGYO_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KONPO_SAGYO_NM.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HUTAI_KASAN_FLG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SITEI_NOHINSHO_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.BUNSEKI_HYO_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.LOT_KANRI_LEVEL_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHOMIKIGEN_KANRI_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SEIZOBI_KANRI_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KITEI_HORYUHIN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KITAKU_KAKAKU_TANI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.KITAKU_SHOHIN_TANKA.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HACCHUTEN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HACCHU_SURYO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NISONIN_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NISONIN_NM_L.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SEIQT_DTL_SHUTURYOKU_FLG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.UNSO_HOKEN_FLG.ColNo, lbl)                        'ADD 2018/07/17
            .SetCellStyle(0, sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_CHUIJIKO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIHUDA_INSATU_SU.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHOHIKIGEN_KINSHIBI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.TEKIYO_START_DATE.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TUJO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TATE_KBN_NASHI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TEION.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TATE_KBN_ARI.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_NYUKO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_NYUKO_TATE_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_SHUKKO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_SHUKKO_TATE_KBN.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKU_MIN_NYUKO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKU_MIN_SHUKKO.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TUJO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HOKANRYO_TEION_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.NIYAKU_MIN_NYUKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SYS_DEL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.WIDTH.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.HEIGHT.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.DEPTH.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.CYL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_1.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_2.ColNo, lbl)
            .SetCellStyle(0, sprGoodsDef.SHUKKAJI_HASU_SAGYO_KBN_3.ColNo, lbl)

            '初期値設定
            Call Me._ControlG.ClearControl(sprGoods)
            .SetCellValue(0, sprGoodsDef.STATUS.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprGoodsDef.BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())

        End With

    End Sub

    ''' <summary>
    ''' 商品明細スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitGoodsDtlSpread()

        '商品明細Spreadの初期値設定
        Dim sprGoodsDtl As LMSpread = Me._Frm.sprGoodsDetail

        With sprGoodsDtl

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 5

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprGoodsDtlDef)
            '2015.10.26 英語化対応
            .SetColProperty(New sprGoodsDtlDef, False)


        End With

    End Sub


#If True Then ' 要望番号2471対応 added 2015.12.14 inoue

    ''' <summary>
    ''' コンボボックスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateOuterPackageComboBox()

        With Me._Frm

            ' サブ区分
            Const OUTER_PACKAGE_SUB_KB As String = "0F"

            ' 営業所コード
            Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd

            ' 荷主コード(CUST_CLASSは'00'を想定する)
            Dim custCd As String = Me._Frm.txtCustCdL.TextValue

            Me._Frm.cmbOuterPackage.Items.Clear()

            Dim kbnRow As DataRow = _
                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).AsEnumerable() _
                                              .Where(Function(r) OUTER_PACKAGE_SUB_KB.Equals(r.Item(LMM100C.M_CUST_DETAIL_CNAME.SUB_KB)) AndAlso _
                                                                 nrsBrCd.Equals(r.Item(LMM100C.M_CUST_DETAIL_CNAME.NRS_BR_CD)) AndAlso _
                                                                 custCd.Equals(r.Item(LMM100C.M_CUST_DETAIL_CNAME.CUST_CD))).FirstOrDefault
            If (kbnRow IsNot Nothing AndAlso _
                String.IsNullOrEmpty(TryCast(kbnRow.Item(LMM100C.M_CUST_DETAIL_CNAME.SET_NAIYO_3), String)) = False) Then

                Dim kbnGroupCd As String = TryCast(kbnRow.Item(LMM100C.M_CUST_DETAIL_CNAME.SET_NAIYO_3), String)

                For Each row As DataRow In MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable() _
                                                 .Where(Function(r) kbnGroupCd.Equals(r.Item(LMM100C.Z_KBN_CNAME.KBN_GROUP_CD))) _
                                                 .OrderBy(Function(r) r.Item(LMM100C.Z_KBN_CNAME.SORT))

                    Me._Frm.cmbOuterPackage.Items.Add(New ListItem(New SubItem() {New SubItem(row.Item(LMM100C.Z_KBN_CNAME.KBN_NM2).ToString()) _
                                                                                , New SubItem(row.Item(LMM100C.Z_KBN_CNAME.KBN_NM1).ToString())}))
                Next


            End If


        End With

    End Sub

#End If

#End Region

#End Region

#End Region

#End Region

End Class
