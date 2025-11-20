' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC020C : 出荷データ編集
'  作  成  者       :  矢内
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Com.Utility   'ADD 2018/11/14 要望管理001939

''' <summary>
''' LMC020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _frm As LMC020F

    ''' <summary>
    ''' Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMC020H

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC020V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._frm = frm

        'Handlerクラスの設定
        Me._H = New LMC020H()

        'Validateクラスの設定
        Me._V = New LMC020V(handlerClass, frm)

        'Gamen共通クラスの設定
        _LMCconG = New LMCControlG(handlerClass, DirectCast(frm, Form))

        'Validate共通クラスの設定
        _LMCconV = New LMCControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal mode As String, Optional ByVal ds As DataSet = Nothing)
        Dim always As Boolean = True

        With Me._frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = "複　写"
            .F4ButtonName = "削　除"
            .F5ButtonName = "引　当"
            .F6ButtonName = "完了取消"
            .F7ButtonName = "運送修正"
            .F8ButtonName = "終算日修正"
            .F9ButtonName = "追加(中)"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            Select Case mode
                Case LMC020C.MODE_READONLY
                    '参照モード
                    .F1ButtonEnabled = True
                    .F2ButtonEnabled = True
                    .F3ButtonEnabled = True
                    .F4ButtonEnabled = True
                    .F5ButtonEnabled = False
                    '要望番号:0919（報告済みの場合も、完了取消が出来るように変更) 2013/01/31 本明 Start
                    'If ("60").Equals(Me._frm.cmbSagyoSintyoku.SelectedValue) = True Then
                    If ("60").Equals(Me._frm.cmbSagyoSintyoku.SelectedValue) = True OrElse
                        ("90").Equals(Me._frm.cmbSagyoSintyoku.SelectedValue) = True Then
                        '要望番号:0919（報告済みの場合も、完了取消が出来るように変更) 2013/01/31 本明 End
                        .F6ButtonEnabled = True
                    Else
                        .F6ButtonEnabled = False
                    End If
                    .F7ButtonEnabled = True
                    If ("60").Equals(Me._frm.cmbSagyoSintyoku.SelectedValue) = True OrElse
                        ("90").Equals(Me._frm.cmbSagyoSintyoku.SelectedValue) = True Then
                        .F8ButtonEnabled = True
                    Else
                        .F8ButtonEnabled = False
                    End If
                    .F9ButtonEnabled = False
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = always

                Case LMC020C.MODE_EDIT
                    '編集モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = True
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = True
                    If Me.IsFFEM_MaterialPlantTransfer(ds) Then
                        ' FFEM原料プラント間転送の場合
                        ' 保存ボタンの無効化
                        .F11ButtonEnabled = False
                    Else
                        .F11ButtonEnabled = True
                    End If
                    .F12ButtonEnabled = True

                Case LMC020C.MODE_UNSO
                    '運送修正モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = True
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = False
                    .F10ButtonEnabled = True
                    If Me.IsFFEM_MaterialPlantTransfer(ds) Then
                        ' FFEM原料プラント間転送の場合
                        ' 保存ボタンの無効化
                        .F11ButtonEnabled = False
                    Else
                        .F11ButtonEnabled = True
                    End If
                    .F12ButtonEnabled = always

                Case LMC020C.MODE_SHUSAN
                    '終算日修正モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = True
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = False
                    .F10ButtonEnabled = True
                    If Me.IsFFEM_MaterialPlantTransfer(ds) Then
                        ' FFEM原料プラント間転送の場合
                        ' 保存ボタンの無効化
                        .F11ButtonEnabled = False
                    Else
                        .F11ButtonEnabled = True
                    End If
                    .F12ButtonEnabled = always

            End Select

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーの設定（編集不可能）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKeyCanNotEdit()

        With Me._Frm.FunctionKey

            'ファンクションキーの制御（(F12)閉じる以外利用不可）
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

        End With
    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._frm
            '出荷大
            .cmbJikkou.TabIndex = LMC020C.CtlTabIndex.JIKKOU
            .btnJikkou.TabIndex = LMC020C.CtlTabIndex.BTN_JIKKOU
            .cmbPRINT.TabIndex = LMC020C.CtlTabIndex.PRINT
            .numPrtCnt.TabIndex = LMC020C.CtlTabIndex.PRINT_CNT
            .numPrtCnt_From.TabIndex = LMC020C.CtlTabIndex.PRINT_CNT_FROM
            .numPrtCnt_To.TabIndex = LMC020C.CtlTabIndex.PRINT_CNT_TO
            .btnPRINT.TabIndex = LMC020C.CtlTabIndex.BTN_PRINT
            .grpSyukka.TabIndex = LMC020C.CtlTabIndex.GRPSYUKKA
            .lblSyukkaLNo.TabIndex = LMC020C.CtlTabIndex.KANRINOL
            .cmbEigyosyo.TabIndex = LMC020C.CtlTabIndex.EIGYO
            .cmbSoko.TabIndex = LMC020C.CtlTabIndex.SOKO
            .lblFurikaeNo.TabIndex = LMC020C.CtlTabIndex.FURINO
            .imdSyukkaDate.TabIndex = LMC020C.CtlTabIndex.SYUKKADATE
            .imdSyukkaYoteiDate.TabIndex = LMC020C.CtlTabIndex.SYUKKAYOTEIDATE
            .imdNounyuYoteiDate.TabIndex = LMC020C.CtlTabIndex.NONYUDATE
            .cmbNounyuKbn.TabIndex = LMC020C.CtlTabIndex.NONYUKBN
            .imdSyukkaHoukoku.TabIndex = LMC020C.CtlTabIndex.HOUKOKUDATE
            .imdHokanEndDate.TabIndex = LMC020C.CtlTabIndex.SYUSANDATE
            .cmbSyukkaKbn.TabIndex = LMC020C.CtlTabIndex.OUTKAKBN
            .cmbSyukkaSyubetu.TabIndex = LMC020C.CtlTabIndex.OUTKASYUBETU
            .cmbSagyoSintyoku.TabIndex = LMC020C.CtlTabIndex.OUTKASTATEKBN
            .cmbWHSagyoSintyoku.TabIndex = LMC020C.CtlTabIndex.WHSTATEKBN
            .chkNifuda.TabIndex = LMC020C.CtlTabIndex.NIFUDA
            .chkNHS.TabIndex = LMC020C.CtlTabIndex.NOUHIN
            .chkDenp.TabIndex = LMC020C.CtlTabIndex.OKURI
            .chkCoa.TabIndex = LMC020C.CtlTabIndex.COA
            .chkHokoku.TabIndex = LMC020C.CtlTabIndex.HOUKOKU
            .txtNisyuTyumonNo.TabIndex = LMC020C.CtlTabIndex.ORDERNO
            .txtKainusiTyumonNo.TabIndex = LMC020C.CtlTabIndex.BUYERORDNO
            .cmbPick.TabIndex = LMC020C.CtlTabIndex.PICK
            .cmbOutkaHokoku_Yn.TabIndex = LMC020C.CtlTabIndex.SYUKKAHOUKOKU
            .cmbToukiYn.TabIndex = LMC020C.CtlTabIndex.HOKANRYO
            .txtCust_Cd_L.TabIndex = LMC020C.CtlTabIndex.CUSTCDL
            .txtCust_Cd_M.TabIndex = LMC020C.CtlTabIndex.CUSTCDM
            .txtCust_Nm.TabIndex = LMC020C.CtlTabIndex.CUSTNM
            .cmbNiyaku.TabIndex = LMC020C.CtlTabIndex.NIYAKURYO
            .numKonpoKosu.TabIndex = LMC020C.CtlTabIndex.KONPOSU
            .numSyukkaSouKosu.TabIndex = LMC020C.CtlTabIndex.SOUKOSU
            .txtUriCd.TabIndex = LMC020C.CtlTabIndex.URICD
            .lblUriNm.TabIndex = LMC020C.CtlTabIndex.URINM
            .cmbOkurijo.TabIndex = LMC020C.CtlTabIndex.YOUOKURI
            .txtOkuriNo.TabIndex = LMC020C.CtlTabIndex.OKURINO
            .btnNew.TabIndex = LMC020C.CtlTabIndex.BTNNEW
            .cmbTodokesaki.TabIndex = LMC020C.CtlTabIndex.DESTKBN
            .txtTodokesakiCd.TabIndex = LMC020C.CtlTabIndex.DESTCD
            .txtTodokesakiNm.TabIndex = LMC020C.CtlTabIndex.DESTNM
            .cmbSiteinouhin.TabIndex = LMC020C.CtlTabIndex.SITEINOUHIN
            .cmbBunsakiTmp.TabIndex = LMC020C.CtlTabIndex.BUNSEKIHYO
            .txtTodokeAdderss1.TabIndex = LMC020C.CtlTabIndex.DESTADRESS1
            .txtTodokeAdderss2.TabIndex = LMC020C.CtlTabIndex.DESTADRESS2
            .txtTodokeAdderss3.TabIndex = LMC020C.CtlTabIndex.DESTADRESS3
            .txtTodokeTel.TabIndex = LMC020C.CtlTabIndex.DESTTEL
            .txtNouhinTeki.TabIndex = LMC020C.CtlTabIndex.NOUHINTEKIYO
            .txtSyukkaRemark.TabIndex = LMC020C.CtlTabIndex.SYUKKATYUI
            .txtHaisoRemark.TabIndex = LMC020C.CtlTabIndex.HAISOUTYUI
            .txtOrderType.TabIndex = LMC020C.CtlTabIndex.ORDTYPE
            .chkUrgent.TabIndex = LMC020C.CtlTabIndex.URGENT_YN
            .chkTablet.TabIndex = LMC020C.CtlTabIndex.WH_TAB_YN
            .txtSijiRemark.TabIndex = LMC020C.CtlTabIndex.WH_TAB_RMK
            .chkTabHokoku.TabIndex = LMC020C.CtlTabIndex.WH_TAB_HOKOKU_YN
            .chkNoSiji.TabIndex = LMC020C.CtlTabIndex.WH_TAB_NO_SIJI
            .tabTop.TabIndex = LMC020C.CtlTabIndex.TAB_TOP '2014/01/22 追加(運送情報・輸出情報タブ)

            '出荷中
            .tabMiddle.TabIndex = LMC020C.CtlTabIndex.TAB
            .btnROW_INS_M.TabIndex = LMC020C.CtlTabIndex.BTN_INM
            .btnROW_COPY_M.TabIndex = LMC020C.CtlTabIndex.BTN_COPYM
            .btnROW_DEL_M.TabIndex = LMC020C.CtlTabIndex.BTN_DELM
            .btnRireki.TabIndex = LMC020C.CtlTabIndex.BTN_RIREKI
            .pnlKensaku.TabIndex = LMC020C.CtlTabIndex.PNL_KENSAKU
            .txtSerchGoodsCd.TabIndex = LMC020C.CtlTabIndex.SER_SYOHINCD
            .txtSerchGoodsNm.TabIndex = LMC020C.CtlTabIndex.SER_SYOHINNM
            .txtSerchLot.TabIndex = LMC020C.CtlTabIndex.SER_LOT
            .pnlHenko.TabIndex = LMC020C.CtlTabIndex.PNL_HENKO
            .numPrintSortHenko.TabIndex = LMC020C.CtlTabIndex.PRINTSORTHENKO
            .sprSyukkaM.TabIndex = LMC020C.CtlTabIndex.SPR_OUTKAM
            .lblSyukkaMNo.TabIndex = LMC020C.CtlTabIndex.KANRINOM
            .numPrintSort.TabIndex = LMC020C.CtlTabIndex.PRINTSORT
            .lblHikiate.TabIndex = LMC020C.CtlTabIndex.JOKYO
            .txtGoodsCdCust.TabIndex = LMC020C.CtlTabIndex.GOODSCD
            .lblGoodsNm.TabIndex = LMC020C.CtlTabIndex.GOODSNM
            .txtLotNo.TabIndex = LMC020C.CtlTabIndex.LOT
            .txtOrderNo.TabIndex = LMC020C.CtlTabIndex.ORDERNOM
            .txtRsvNo.TabIndex = LMC020C.CtlTabIndex.RSVNO
            .txtSerialNo.TabIndex = LMC020C.CtlTabIndex.SERIALNO
            .txtCyumonNo.TabIndex = LMC020C.CtlTabIndex.BUYERORDNOM
            .optTempY.TabIndex = LMC020C.CtlTabIndex.BUNSEKIHYOM
            .optTempN.TabIndex = LMC020C.CtlTabIndex.OUTKATANI
            .grpShukaTani.TabIndex = LMC020C.CtlTabIndex.GRPSHUKATANI
            .optCnt.TabIndex = LMC020C.CtlTabIndex.KOSU_TANI
            .optAmt.TabIndex = LMC020C.CtlTabIndex.SURYO_TANI
            .optKowake.TabIndex = LMC020C.CtlTabIndex.KOWAKE_TANI
            .optSample.TabIndex = LMC020C.CtlTabIndex.SAMPLE_TANI
            .numIrime.TabIndex = LMC020C.CtlTabIndex.IRIME
            .numPkgCnt.TabIndex = LMC020C.CtlTabIndex.KONPOKOSU
            .cmbUnsoOndo.TabIndex = LMC020C.CtlTabIndex.ONDO
            .cmbTakkyuSize.TabIndex = LMC020C.CtlTabIndex.TAKYUBIN
            .grpKosu.TabIndex = LMC020C.CtlTabIndex.GRPKOSU
            .numKonsu.TabIndex = LMC020C.CtlTabIndex.KONSU
            .numIrisu.TabIndex = LMC020C.CtlTabIndex.IRISU
            .numSouKosu.TabIndex = LMC020C.CtlTabIndex.KOSU
            .numHasu.TabIndex = LMC020C.CtlTabIndex.HASU
            .numHikiateKosuSumi.TabIndex = LMC020C.CtlTabIndex.HIKI_ZUMIKOSU
            .numHikiateKosuZan.TabIndex = LMC020C.CtlTabIndex.HIKI_ZANKOSU
            .grpSuryo.TabIndex = LMC020C.CtlTabIndex.GRPSURYO
            .numSouSuryo.TabIndex = LMC020C.CtlTabIndex.SURYO
            .numHikiateSuryoSumi.TabIndex = LMC020C.CtlTabIndex.HIKI_ZUMISURYO
            .numHikiateSuryoZan.TabIndex = LMC020C.CtlTabIndex.HIKI_ZANSURYO
            .txtGoodsRemark.TabIndex = LMC020C.CtlTabIndex.GOODSREMARK

            'START 要望番号1959
            .lblEdiOutkaTtlNb.TabIndex = LMC020C.CtlTabIndex.EDI_OUTKATTL_NB
            .lblEdiOutkaTtlQt.TabIndex = LMC020C.CtlTabIndex.EDI_OUTKATTL_QT
            'END 要望番号1959

            .txtSagyoM1.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_M1
            .lblSagyoM1.TabIndex = LMC020C.CtlTabIndex.SAGYONM_M1
            .txtSagyoRemarkM1.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_M1
            .txtSagyoM2.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_M2
            .lblSagyoM2.TabIndex = LMC020C.CtlTabIndex.SAGYONM_M2
            .txtSagyoRemarkM2.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_M2
            .txtSagyoM3.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_M3
            .lblSagyoM3.TabIndex = LMC020C.CtlTabIndex.SAGYONM_M3
            .txtSagyoRemarkM3.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_M3
            .txtSagyoM4.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_M4
            .lblSagyoM4.TabIndex = LMC020C.CtlTabIndex.SAGYONM_M4
            .txtSagyoRemarkM4.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_M4
            .txtSagyoM5.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_M5
            .lblSagyoM5.TabIndex = LMC020C.CtlTabIndex.SAGYONM_M5
            .txtSagyoRemarkM5.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_M5
            .txtDestSagyoM1.TabIndex = LMC020C.CtlTabIndex.DESTSAGYOCD_M1
            .lblDestSagyoM1.TabIndex = LMC020C.CtlTabIndex.DESTSAGYONM_M1
            .txtDestSagyoRemarkM1.TabIndex = LMC020C.CtlTabIndex.DESTSAGYORMK_M1
            .txtDestSagyoM2.TabIndex = LMC020C.CtlTabIndex.DESTSAGYOCD_M2
            .lblDestSagyoM2.TabIndex = LMC020C.CtlTabIndex.DESTSAGYONM_M2
            .txtDestSagyoRemarkM2.TabIndex = LMC020C.CtlTabIndex.DESTSAGYORMK_M2
            .btnROW_DEL_S.TabIndex = LMC020C.CtlTabIndex.BTN_DELS
            .sprDtl.TabIndex = LMC020C.CtlTabIndex.SPR_OUTKAS
            '運送
            .grpUnso.TabIndex = LMC020C.CtlTabIndex.GRPUNSO
            .txtUnsoNo.TabIndex = LMC020C.CtlTabIndex.UNSONO
            .cmbTehaiKbn.TabIndex = LMC020C.CtlTabIndex.UNSOKBN
            .cmbTariffKbun.TabIndex = LMC020C.CtlTabIndex.TEHAI
            .cmbSyaryoKbn.TabIndex = LMC020C.CtlTabIndex.SYARYO
            .cmbBinKbn.TabIndex = LMC020C.CtlTabIndex.BIN
            .cmbMotoCyakuKbn.TabIndex = LMC020C.CtlTabIndex.MOTOTYAKU
            .numJuryo.TabIndex = LMC020C.CtlTabIndex.JURYO
            .numKyori.TabIndex = LMC020C.CtlTabIndex.KYORI
            .txtUnsoCompanyCd.TabIndex = LMC020C.CtlTabIndex.UNSOCOCD_L
            .txtUnsoSitenCd.TabIndex = LMC020C.CtlTabIndex.UNSOCOCD_M
            .lblUnsoCompanyNm.TabIndex = LMC020C.CtlTabIndex.UNSOCONM_L
            .lblUnsoSitenNm.TabIndex = LMC020C.CtlTabIndex.UNSOCONM_M
            .cmbUnsoKazeiKbn.TabIndex = LMC020C.CtlTabIndex.UNSOKAZEIKBN
            .txtUnthinTariffCd.TabIndex = LMC020C.CtlTabIndex.TARIFFCD
            .lblUnthinTariffNm.TabIndex = LMC020C.CtlTabIndex.TARIFFNM
            .txtExtcTariffCd.TabIndex = LMC020C.CtlTabIndex.WARITARIFF
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayUnthinTariffCd.TabIndex = LMC020C.CtlTabIndex.SIHARAI_TARIFFCD
            .lblPayUnthinTariffNm.TabIndex = LMC020C.CtlTabIndex.SIHARAI_TARIFFNM
            .txtPayExtcTariffCd.TabIndex = LMC020C.CtlTabIndex.SIHARAI_WARITARIFF
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .cmbYusoBrCd.TabIndex = LMC020C.CtlTabIndex.YUSOEIGYO
            '要望番号:2408 2015.09.17 追加START
            .cmbAutoDenpKbn.TabIndex = LMC020C.CtlTabIndex.AUTO_DENP_KBN
            .lblAutoDenpNo.TabIndex = LMC020C.CtlTabIndex.AUTO_DENP_NO
            '要望番号:2408 2015.09.17 追加END


            .txtSagyoL1.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_L1
            .lblSagyoL1.TabIndex = LMC020C.CtlTabIndex.SAGYONM_L1
            .txtSagyoRemarkL1.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_L1
            .txtSagyoL2.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_L2
            .lblSagyoL2.TabIndex = LMC020C.CtlTabIndex.SAGYONM_L2
            .txtSagyoRemarkL2.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_L2
            .txtSagyoL3.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_L3
            .lblSagyoL3.TabIndex = LMC020C.CtlTabIndex.SAGYONM_L3
            .txtSagyoRemarkL3.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_L3
            .txtSagyoL4.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_L4
            .lblSagyoL4.TabIndex = LMC020C.CtlTabIndex.SAGYONM_L4
            .txtSagyoRemarkL4.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_L4
            .txtSagyoL5.TabIndex = LMC020C.CtlTabIndex.SAGYOCD_L5
            .lblSagyoL5.TabIndex = LMC020C.CtlTabIndex.SAGYONM_L5
            .txtSagyoRemarkL5.TabIndex = LMC020C.CtlTabIndex.SAGYORMK_L5

            '追加 START 2014/01/22 金
            '運送情報・輸出情報タブ
            .txtShipNm.TabIndex = LMC020C.CtlTabIndex.SHIP_NM
            .txtDestination.TabIndex = LMC020C.CtlTabIndex.DESTINATION
            .txtBookingNo.TabIndex = LMC020C.CtlTabIndex.BOOKING_NO
            .txtVoyageNo.TabIndex = LMC020C.CtlTabIndex.VOYAGE_NO
            .txtShipperCd.TabIndex = LMC020C.CtlTabIndex.SHIPPER_CD
            .imdContLoadingDate.TabIndex = LMC020C.CtlTabIndex.CONT_LOADING_DATE
            .imdStorageTestDate.TabIndex = LMC020C.CtlTabIndex.STORAGE_TEST_DATE
            .txtStorageTestTime.TabIndex = LMC020C.CtlTabIndex.STORAGE_TEST_TIME
            .imdDepartureDate.TabIndex = LMC020C.CtlTabIndex.DEPARTURE_DATE
            .txtContainerNo.TabIndex = LMC020C.CtlTabIndex.CONTAINER_NO
            .txtContainerNm.TabIndex = LMC020C.CtlTabIndex.CONTAINER_NM
            .cmbContainerSize.TabIndex = LMC020C.CtlTabIndex.CONTAINER_SIZE
            '追加 END   2014/01/22 金

            '2015.07.08 協立化学　シッピングマーク対応 追加START
            .numCaseNoFrom.TabIndex = LMC020C.CtlTabIndex.CASE_NO_FROM
            .numCaseNoTo.TabIndex = LMC020C.CtlTabIndex.CASE_NO_TO
            .txtMarkInfo1.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_1
            .txtMarkInfo2.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_2
            .txtMarkInfo3.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_3
            .txtMarkInfo4.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_4
            .txtMarkInfo5.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_5
            .txtMarkInfo6.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_6
            .txtMarkInfo7.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_7
            .txtMarkInfo8.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_8
            .txtMarkInfo9.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_9
            .txtMarkInfo10.TabIndex = LMC020C.CtlTabIndex.MARK_INFO_10
            '2015.07.08 協立化学　シッピングマーク対応 追加END

        End With

    End Sub

    ''' <summary>
    ''' ボタンの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBtn(ByVal mode As String, ByVal noChangeOutkaM As Boolean)
        Dim always As Boolean = True

        With Me._frm

            'ボタンの制御
            Select Case mode
                Case LMC020C.MODE_READONLY
                    '参照モード
                    .btnJikkou.Enabled = True
                    .btnPRINT.Enabled = True
                    .btnNew.Enabled = False
                    .btnHenko.Enabled = False
                    .btnROW_INS_M.Enabled = False
                    .btnROW_DEL_M.Enabled = False
                    .btnROW_COPY_M.Enabled = False
                    .btnRireki.Enabled = True
                    .btnROW_DEL_S.Enabled = False
                    .btnChangeGoods.Enabled = False

                Case LMC020C.MODE_EDIT
                    '編集モード
                    .btnJikkou.Enabled = False
                    .btnPRINT.Enabled = False
                    .btnNew.Enabled = True
                    .btnHenko.Enabled = True
                    .btnROW_INS_M.Enabled = True
                    .btnROW_DEL_M.Enabled = True
                    .btnROW_COPY_M.Enabled = True
                    .btnRireki.Enabled = True
                    .btnROW_DEL_S.Enabled = True
                    .btnChangeGoods.Enabled = True

                Case LMC020C.MODE_UNSO
                    '運送修正モード
                    .btnJikkou.Enabled = False
                    .btnPRINT.Enabled = False
                    .btnNew.Enabled = False
                    .btnHenko.Enabled = False
                    .btnROW_INS_M.Enabled = False
                    .btnROW_DEL_M.Enabled = False
                    .btnROW_COPY_M.Enabled = False
                    .btnRireki.Enabled = True
                    .btnROW_DEL_S.Enabled = False
                    .btnChangeGoods.Enabled = False

                Case LMC020C.MODE_SHUSAN
                    '終算日修正モード
                    .btnJikkou.Enabled = False
                    .btnPRINT.Enabled = False
                    .btnNew.Enabled = False
                    .btnHenko.Enabled = False
                    .btnROW_INS_M.Enabled = False
                    .btnROW_DEL_M.Enabled = False
                    .btnROW_COPY_M.Enabled = False
                    .btnRireki.Enabled = True
                    .btnROW_DEL_S.Enabled = False
                    .btnChangeGoods.Enabled = False

            End Select

            If ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                .btnNew.Enabled = False
            End If

            '商品変更ボタンは横浜デュポン(CD:00588)のみ可視化する。
            If ("40").Equals(.cmbEigyosyo.SelectedValue.ToString) = True _
                AndAlso ("00588").Equals(.lblCustCdL.TextValue.ToString) = True Then
                .btnChangeGoods.Visible = True
            Else
                .btnChangeGoods.Visible = False
            End If

            '顧客指示変更不可フラグがTrueならば出荷(中)に関する一部機能を使用不可とする
            If noChangeOutkaM Then
                .btnROW_INS_M.Enabled = False
                .btnROW_COPY_M.Enabled = False
                .btnROW_DEL_M.Enabled = False
                .btnHenko.Enabled = False
            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="mode">モードによるロック制御を行う。</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal mode As String, ByVal hikiCnt As Integer, Optional ByVal ds As DataSet = Nothing)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        'Friend Sub SetControlsStatus(ByVal mode As String, ByVal hikiCnt As Integer)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

        With Me._frm

            '画面項目の制御
            Select Case mode
                Case LMC020C.MODE_READONLY
                    '参照モード
                    .cmbJikkou.ReadOnly = False
                    .cmbSoko.ReadOnly = True
                    .imdSyukkaDate.ReadOnly = True
                    .imdSyukkaYoteiDate.ReadOnly = True
                    .imdNounyuYoteiDate.ReadOnly = True
                    .cmbNounyuKbn.ReadOnly = True
                    .imdHokanEndDate.ReadOnly = True
                    .cmbSyukkaSyubetu.ReadOnly = True
                    .chkNifuda.Enabled = False
                    .chkNHS.Enabled = False
                    .chkDenp.Enabled = False
                    .chkCoa.Enabled = False
                    .chkHokoku.Enabled = False
                    .txtNisyuTyumonNo.ReadOnly = True
                    .txtKainusiTyumonNo.ReadOnly = True
                    .cmbPick.ReadOnly = True
                    .cmbOutkaHokoku_Yn.ReadOnly = True
                    .txtCust_Cd_L.ReadOnly = True
                    .txtCust_Cd_M.ReadOnly = True
                    .cmbNiyaku.ReadOnly = True
                    .numKonpoKosu.ReadOnly = True
                    .txtUriCd.ReadOnly = True
                    .cmbOkurijo.ReadOnly = True
                    'START YANAI 要望番号982
                    .txtOkuriNo.ReadOnly = True
                    'END YANAI 要望番号982
                    .cmbTodokesaki.ReadOnly = True
                    .txtTodokesakiCd.ReadOnly = True
                    .txtTodokesakiNm.ReadOnly = True
                    .txtTodokeTel.ReadOnly = True
                    .txtNouhinTeki.ReadOnly = True
                    .txtSyukkaRemark.ReadOnly = True
                    .txtOrderType.ReadOnly = True
                    .txtTodokeAdderss1.ReadOnly = True
                    .txtTodokeAdderss2.ReadOnly = True
                    .txtTodokeAdderss3.ReadOnly = True
                    .txtHaisoRemark.ReadOnly = True
                    .txtSerchGoodsCd.ReadOnly = True
                    .txtSerchGoodsNm.ReadOnly = True
                    .txtSerchLot.ReadOnly = True
                    .numPrintSortHenko.ReadOnly = True
                    '要望番号:1595 yamanaka 2012.11.09 Start
                    .txtGoodsCdCust.ReadOnly = True
                    .lblGoodsNm.ReadOnly = True
                    '要望番号:1595 yamanaka 2012.11.09 End
                    .numPrintSort.ReadOnly = True
                    .txtOrderNo.ReadOnly = True
                    .txtRsvNo.ReadOnly = True
                    .txtSerialNo.ReadOnly = True
                    .txtCyumonNo.ReadOnly = True
                    .optTempY.Enabled = False
                    .optTempN.Enabled = False
                    .optCnt.Enabled = False
                    .optAmt.Enabled = False
                    .optKowake.Enabled = False
                    .optSample.Enabled = False
                    .numIrime.ReadOnly = True
                    .numPkgCnt.ReadOnly = True
                    .cmbUnsoOndo.ReadOnly = True
                    .cmbTakkyuSize.ReadOnly = True
                    .numKonsu.ReadOnly = True
                    .numHasu.ReadOnly = True
                    .numSouSuryo.ReadOnly = True
                    .txtGoodsRemark.ReadOnly = True
                    .txtSagyoM1.ReadOnly = True
                    .txtSagyoM2.ReadOnly = True
                    .txtSagyoM3.ReadOnly = True
                    .txtSagyoM4.ReadOnly = True
                    .txtSagyoM5.ReadOnly = True
                    .txtDestSagyoM1.ReadOnly = True
                    .txtDestSagyoM2.ReadOnly = True
                    .txtSagyoRemarkM1.ReadOnly = True
                    .txtSagyoRemarkM2.ReadOnly = True
                    .txtSagyoRemarkM3.ReadOnly = True
                    .txtSagyoRemarkM4.ReadOnly = True
                    .txtSagyoRemarkM5.ReadOnly = True
                    .txtDestSagyoRemarkM1.ReadOnly = True
                    .txtDestSagyoRemarkM2.ReadOnly = True
                    .cmbTehaiKbn.ReadOnly = True
                    .cmbTariffKbun.ReadOnly = True
                    .cmbSyaryoKbn.ReadOnly = True
                    .cmbBinKbn.ReadOnly = True
                    .cmbMotoCyakuKbn.ReadOnly = True
                    .numJuryo.ReadOnly = True
                    .numKyori.ReadOnly = True
                    .txtUnsoCompanyCd.ReadOnly = True
                    .txtUnsoSitenCd.ReadOnly = True
                    .txtUnthinTariffCd.ReadOnly = True
                    .txtExtcTariffCd.ReadOnly = True
                    'START UMANO 要望番号1302 支払運賃に伴う修正。
                    .txtPayUnthinTariffCd.ReadOnly = True
                    .txtPayExtcTariffCd.ReadOnly = True
                    'END UMANO 要望番号1302 支払運賃に伴う修正。
                    .cmbYusoBrCd.ReadOnly = True
                    '要望番号:2408 2015.09.17 追加START
                    .cmbAutoDenpKbn.ReadOnly = True
                    .lblAutoDenpNo.ReadOnly = True
                    '要望番号:2408 2015.09.17 追加END
                    .cmbUnsoKazeiKbn.ReadOnly = True
                    .txtSagyoL1.ReadOnly = True
                    .txtSagyoL2.ReadOnly = True
                    .txtSagyoL3.ReadOnly = True
                    .txtSagyoL4.ReadOnly = True
                    .txtSagyoL5.ReadOnly = True
                    .txtSagyoRemarkL1.ReadOnly = True
                    .txtSagyoRemarkL2.ReadOnly = True
                    .txtSagyoRemarkL3.ReadOnly = True
                    .txtSagyoRemarkL4.ReadOnly = True
                    .txtSagyoRemarkL5.ReadOnly = True

                    '2013.03.25  要望番号1959 START
                    .numHikiateKosuZan.ReadOnly = True
                    .numHikiateSuryoZan.ReadOnly = True
                    .numSouKosu.ReadOnly = True
                    .numSouSuryo.ReadOnly = True
                    '2013.03.25  要望番号1959 END

                    '2014/01/22 輸出情報追加 START
                    .txtShipNm.ReadOnly = True
                    .txtDestination.ReadOnly = True
                    .txtBookingNo.ReadOnly = True
                    .txtVoyageNo.ReadOnly = True
                    .txtShipperCd.ReadOnly = True
                    .imdContLoadingDate.ReadOnly = True
                    .imdStorageTestDate.ReadOnly = True
                    .txtStorageTestTime.ReadOnly = True
                    .imdDepartureDate.ReadOnly = True
                    .txtContainerNo.ReadOnly = True
                    .txtContainerNm.ReadOnly = True
                    .cmbContainerSize.ReadOnly = True
                    '2014/01/22 輸出情報追加 START

                    '追加開始 --- 2014.07.24 kikuchi
                    .cmbSiteinouhin.ReadOnly = True
                    .cmbBunsakiTmp.ReadOnly = True
                    '追加終了 ---

                    '2015.07.08 協立化学　シッピング対応 追加START
                    .txtOutkaNoM.ReadOnly = True
                    .numCaseNoFrom.ReadOnly = True
                    .numCaseNoTo.ReadOnly = True
                    .txtMarkInfo1.ReadOnly = True
                    .txtMarkInfo2.ReadOnly = True
                    .txtMarkInfo3.ReadOnly = True
                    .txtMarkInfo4.ReadOnly = True
                    .txtMarkInfo5.ReadOnly = True
                    .txtMarkInfo6.ReadOnly = True
                    .txtMarkInfo7.ReadOnly = True
                    .txtMarkInfo8.ReadOnly = True
                    .txtMarkInfo9.ReadOnly = True
                    .txtMarkInfo10.ReadOnly = True
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '倉庫タブレット用項目
                    .cmbWHSagyoSintyoku.ReadOnly = True
                    .chkUrgent.Enabled = False
                    .chkTablet.Enabled = False
                    .txtSijiRemark.ReadOnly = True
                    .chkNoSiji.Enabled = False
                    .chkTabHokoku.Enabled = False
                    .txtHokoku.ReadOnly = True
                Case LMC020C.MODE_EDIT
                    '【実行種別】選択肢固定化
                    '「分納出荷」選択→実行処理 よりの「編集」処理の場合「分納出荷」
                    '上記以外の「編集」処理の場合、未選択状態
                    .cmbJikkou.ReadOnly = True
                    '編集モード
                    If 0 < hikiCnt Then
                        .cmbSoko.ReadOnly = True
                    Else
                        .cmbSoko.ReadOnly = False
                    End If
                    .imdSyukkaDate.ReadOnly = False
                    .imdSyukkaYoteiDate.ReadOnly = False
                    .imdNounyuYoteiDate.ReadOnly = False
                    .cmbNounyuKbn.ReadOnly = False
                    .imdHokanEndDate.ReadOnly = False
                    If .cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                        ' 「分納出荷」の場合の【出荷種別】選択肢固定化
                        .cmbSyukkaSyubetu.ReadOnly = True
                    Else
                        .cmbSyukkaSyubetu.ReadOnly = False
                    End If
                    .chkNifuda.Enabled = True
                    .chkNHS.Enabled = True
                    .chkDenp.Enabled = True
                    .chkCoa.Enabled = True
                    .chkHokoku.Enabled = True
                    .txtNisyuTyumonNo.ReadOnly = False
                    .txtKainusiTyumonNo.ReadOnly = False
                    .cmbPick.ReadOnly = False
                    .cmbOutkaHokoku_Yn.ReadOnly = False
                    .txtCust_Cd_L.ReadOnly = True
                    .txtCust_Cd_M.ReadOnly = True
                    .cmbNiyaku.ReadOnly = False
                    .numKonpoKosu.ReadOnly = False
                    .txtUriCd.ReadOnly = False
                    .cmbOkurijo.ReadOnly = False
                    'START YANAI 要望番号982
                    .txtOkuriNo.ReadOnly = False
                    'END YANAI 要望番号982
                    .cmbTodokesaki.ReadOnly = False

                    '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                                        String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                      "' AND CUST_CD = '", .txtCust_Cd_L.TextValue & .txtCust_Cd_M.TextValue, _
                                                                      "' AND SUB_KB = '53'"))
                    Dim bYusyutu As Boolean = False '輸出情報にするフラグをセット
                    If custDetailsDr.Length > 0 Then
                        bYusyutu = True
                    End If
                    '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End

                    If ("00").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        .btnNew.Enabled = True
                        .txtTodokesakiCd.ReadOnly = False
                        .txtTodokesakiNm.ReadOnly = True
                        .txtTodokeAdderss1.ReadOnly = True
                        .txtTodokeAdderss2.ReadOnly = True
                    ElseIf ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        .btnNew.Enabled = False
                        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
                        '.txtTodokesakiCd.ReadOnly = True
                        If bYusyutu Then
                            .txtTodokesakiCd.ReadOnly = False
                        Else
                            .txtTodokesakiCd.ReadOnly = True
                        End If
                        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End
                        .txtTodokesakiNm.ReadOnly = False
                        .txtTodokeAdderss1.ReadOnly = False
                        .txtTodokeAdderss2.ReadOnly = False
                    ElseIf ("02").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        'EDIの場合
                        .btnNew.Enabled = False
                        .txtTodokesakiCd.ReadOnly = True
                        .txtTodokesakiNm.ReadOnly = True
                        .txtTodokeAdderss1.ReadOnly = True
                        .txtTodokeAdderss2.ReadOnly = True
                    End If
                    .txtTodokeTel.ReadOnly = False
                    .txtNouhinTeki.ReadOnly = False
                    .txtSyukkaRemark.ReadOnly = False
                    .txtOrderType.ReadOnly = True
                    .txtTodokeAdderss3.ReadOnly = False
                    .txtHaisoRemark.ReadOnly = False
                    .txtSerchGoodsCd.ReadOnly = False
                    .txtSerchGoodsNm.ReadOnly = False
                    .txtSerchLot.ReadOnly = False
                    .numPrintSortHenko.ReadOnly = False
                    .numPrintSort.ReadOnly = False
                    .txtOrderNo.ReadOnly = False
                    .txtRsvNo.ReadOnly = False
                    .txtSerialNo.ReadOnly = False
                    .txtCyumonNo.ReadOnly = False
                    .optTempY.Enabled = True
                    .optTempN.Enabled = True
                    .optCnt.Enabled = True
                    .optAmt.Enabled = True

                    .optKowake.Enabled = True
                    .optSample.Enabled = True

                    .numIrime.ReadOnly = False
                    .numPkgCnt.ReadOnly = False
                    .cmbUnsoOndo.ReadOnly = False
                    .cmbTakkyuSize.ReadOnly = False
                    .numKonsu.ReadOnly = False
                    .numHasu.ReadOnly = False
                    .numSouSuryo.ReadOnly = False
                    .txtGoodsRemark.ReadOnly = False
                    .txtSagyoM1.ReadOnly = False
                    .txtSagyoM2.ReadOnly = False
                    .txtSagyoM3.ReadOnly = False
                    .txtSagyoM4.ReadOnly = False
                    .txtSagyoM5.ReadOnly = False
                    .txtDestSagyoM1.ReadOnly = False
                    .txtDestSagyoM2.ReadOnly = False
                    .txtSagyoRemarkM1.ReadOnly = False
                    .txtSagyoRemarkM2.ReadOnly = False
                    .txtSagyoRemarkM3.ReadOnly = False
                    .txtSagyoRemarkM4.ReadOnly = False
                    .txtSagyoRemarkM5.ReadOnly = False
                    .txtDestSagyoRemarkM1.ReadOnly = False
                    .txtDestSagyoRemarkM2.ReadOnly = False
                    .cmbTehaiKbn.ReadOnly = False
                    .cmbTariffKbun.ReadOnly = False
                    .cmbSyaryoKbn.ReadOnly = False
                    .cmbBinKbn.ReadOnly = False
                    .cmbMotoCyakuKbn.ReadOnly = False
                    .numJuryo.ReadOnly = True
                    .numKyori.ReadOnly = True
                    .txtUnsoCompanyCd.ReadOnly = False
                    .txtUnsoSitenCd.ReadOnly = False
                    .txtUnthinTariffCd.ReadOnly = False
                    .txtExtcTariffCd.ReadOnly = False
                    'START UMANO 要望番号1302 支払運賃に伴う修正。
                    .txtPayUnthinTariffCd.ReadOnly = False
                    .txtPayExtcTariffCd.ReadOnly = False
                    'END UMANO 要望番号1302 支払運賃に伴う修正。
                    .cmbYusoBrCd.ReadOnly = False
                    '要望番号:2408 2015.09.17 追加START
                    .cmbAutoDenpKbn.ReadOnly = True
                    .lblAutoDenpNo.ReadOnly = True
                    '要望番号:2408 2015.09.17 追加END
                    .cmbUnsoKazeiKbn.ReadOnly = False
                    .txtSagyoL1.ReadOnly = False
                    .txtSagyoL2.ReadOnly = False
                    .txtSagyoL3.ReadOnly = False
                    .txtSagyoL4.ReadOnly = False
                    .txtSagyoL5.ReadOnly = False
                    .txtSagyoRemarkL1.ReadOnly = False
                    .txtSagyoRemarkL2.ReadOnly = False
                    .txtSagyoRemarkL3.ReadOnly = False
                    .txtSagyoRemarkL4.ReadOnly = False
                    .txtSagyoRemarkL5.ReadOnly = False

                    '梱数・端数・個数のロック制御
                    .numKonsu.ReadOnly = False
                    .numHasu.ReadOnly = False
                    .numSouKosu.ReadOnly = True

                    '運送情報のロック制御a
                    If String.IsNullOrEmpty(Convert.ToString(.cmbTehaiKbn.SelectedValue)) = True Then
                        '選ばれていない場合
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = True
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = True
                        .txtUnsoSitenCd.ReadOnly = True
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    ElseIf ("10").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                        '先方手配・未定の場合
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = False
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = False
                        .txtUnsoSitenCd.ReadOnly = False
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    Else
                        '日陸手配の場合
                        .cmbTariffKbun.ReadOnly = False
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        If ("10").Equals(.cmbTariffKbun.SelectedValue) = True OrElse _
                            ("30").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：混載、特便の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        ElseIf ("20").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：車扱いの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送元区分：横持ちの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If String.IsNullOrEmpty(Convert.ToString(.cmbTariffKbun.SelectedValue)) = True Then
                            '運送元区分：未選択の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                    End If

                    'オーダータイプのロック制御（区分マスタに登録されている荷主の時のみロック解除）
                    Dim odrType As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'O006' AND KBN_NM1 ='", .cmbEigyosyo.SelectedValue, "'"))
                    Dim odrTypeMax As Integer = odrType.Count - 1
                    For i As Integer = 0 To odrTypeMax
                        If (odrType(i).Item("KBN_NM2").ToString).Equals(.txtCust_Cd_L.TextValue) = True Then
                            .txtOrderType.ReadOnly = False
                            Exit For
                        End If
                    Next

                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                    '運行に紐づいている場合
                    If ds IsNot Nothing Then
                        If ds.Tables("LMC020_UNSO_L").Rows.Count > 0 Then 'ADD 2020/02/27 010901
                            If String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_SYUKA").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_HAIKA").ToString()) = False Then
                                '以下の項目をロックする
                                '.cmbTehaiKbn.ReadOnly = True
                                '.cmbBinKbn.ReadOnly = True
                                '.txtUnsoCompanyCd.ReadOnly = True
                                '.txtUnsoSitenCd.ReadOnly = True
                                .txtPayUnthinTariffCd.ReadOnly = True
                                .txtPayExtcTariffCd.ReadOnly = True
                            End If
                        End If 'ADD 2020/02/27 010901
                    End If
                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

                    '2014/01/30 輸出情報追加 START
                    If ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        '届け先区分が輸出情報の場合
                        .txtShipNm.ReadOnly = False
                        .txtDestination.ReadOnly = False
                        .txtBookingNo.ReadOnly = False
                        .txtVoyageNo.ReadOnly = False
                        .txtShipperCd.ReadOnly = False
                        .imdContLoadingDate.ReadOnly = False
                        .imdStorageTestDate.ReadOnly = False
                        .txtStorageTestTime.ReadOnly = False
                        .imdDepartureDate.ReadOnly = False
                        .txtContainerNo.ReadOnly = False
                        .txtContainerNm.ReadOnly = False
                        .cmbContainerSize.ReadOnly = False
                    Else
                        '上記以外の場合
                        .txtShipNm.ReadOnly = True
                        .txtDestination.ReadOnly = True
                        .txtBookingNo.ReadOnly = True
                        .txtVoyageNo.ReadOnly = True
                        .txtShipperCd.ReadOnly = True
                        .imdContLoadingDate.ReadOnly = True
                        .imdStorageTestDate.ReadOnly = True
                        .txtStorageTestTime.ReadOnly = True
                        .imdDepartureDate.ReadOnly = True
                        .txtContainerNo.ReadOnly = True
                        .txtContainerNm.ReadOnly = True
                        .cmbContainerSize.ReadOnly = True
                    End If
                    '2014/01/30 輸出情報追加 START

                    '追加開始 --- 2014.07.24 kikuchi
                    .cmbSiteinouhin.ReadOnly = False
                    .cmbBunsakiTmp.ReadOnly = False
                    '追加終了 ---

                    '2015.07.08 協立化学　シッピング対応 追加START
                    .txtOutkaNoM.ReadOnly = True
                    .numCaseNoFrom.ReadOnly = False
                    .numCaseNoTo.ReadOnly = False
                    .txtMarkInfo1.ReadOnly = False
                    .txtMarkInfo2.ReadOnly = False
                    .txtMarkInfo3.ReadOnly = False
                    .txtMarkInfo4.ReadOnly = False
                    .txtMarkInfo5.ReadOnly = False
                    .txtMarkInfo6.ReadOnly = False
                    .txtMarkInfo7.ReadOnly = False
                    .txtMarkInfo8.ReadOnly = False
                    .txtMarkInfo9.ReadOnly = False
                    .txtMarkInfo10.ReadOnly = False
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '倉庫タブレット用項目
                    .cmbWHSagyoSintyoku.ReadOnly = True
                    .chkUrgent.Enabled = True
                    If (LMC020C.WH_TAB_STATUS_01.Equals(.cmbWHSagyoSintyoku.SelectedValue) = False) AndAlso _
                         (LMC020C.WH_TAB_STATUS_02.Equals(.cmbWHSagyoSintyoku.SelectedValue) = False) Then
                        .chkTablet.Enabled = True
                    End If
                    .txtSijiRemark.ReadOnly = False
                    .chkNoSiji.Enabled = True
                    .chkTabHokoku.Enabled = True
                    .txtHokoku.ReadOnly = True

                Case LMC020C.MODE_UNSO
                    '運送修正モード
                    '運送情報のロック制御
                    If String.IsNullOrEmpty(Convert.ToString(.cmbTehaiKbn.SelectedValue)) = True Then
                        '選ばれていない場合
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = True
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = True
                        .txtUnsoSitenCd.ReadOnly = True
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    ElseIf ("10").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                        '先方手配・未定の場合
                        .cmbTehaiKbn.ReadOnly = False
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = False
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = False
                        .txtUnsoSitenCd.ReadOnly = False
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    Else
                        '日陸手配の場合
                        .cmbTehaiKbn.ReadOnly = False
                        .cmbTariffKbun.ReadOnly = False
                        .cmbBinKbn.ReadOnly = False
                        .cmbMotoCyakuKbn.ReadOnly = False
                        .numJuryo.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = False
                        .txtUnsoSitenCd.ReadOnly = False
                        .txtUnthinTariffCd.ReadOnly = False
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = False
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = False
                        '要望番号:2408 2015.09.17 追加START
                        .cmbAutoDenpKbn.ReadOnly = True
                        .lblAutoDenpNo.ReadOnly = True
                        '要望番号:2408 2015.09.17 追加END
                        .cmbUnsoKazeiKbn.ReadOnly = False
                        If ("10").Equals(.cmbTariffKbun.SelectedValue) = True OrElse _
                            ("30").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：混載、特便の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        ElseIf ("20").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：車扱いの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送元区分：横持ちの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If String.IsNullOrEmpty(Convert.ToString(.cmbTariffKbun.SelectedValue)) = True Then
                            '運送元区分：未選択の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                    End If

                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                    '運行に紐づいている場合
                    If ds IsNot Nothing Then
                        If ds.Tables("LMC020_UNSO_L").Rows.Count > 0 Then 'ADD 2020/02/27 010901
                            If String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_SYUKA").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = False OrElse _
                               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_HAIKA").ToString()) = False Then
                                '以下の項目をロックする
                                '.cmbTehaiKbn.ReadOnly = True
                                '.cmbBinKbn.ReadOnly = True
                                '.txtUnsoCompanyCd.ReadOnly = True
                                '.txtUnsoSitenCd.ReadOnly = True
                                .txtPayUnthinTariffCd.ReadOnly = True
                                .txtPayExtcTariffCd.ReadOnly = True
                            End If
                        End If 'ADD 2020/02/27 010901
                    End If
                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

                    '追加開始 --- 2014.07.24 kikuchi
                    .cmbSiteinouhin.ReadOnly = False
                    .cmbBunsakiTmp.ReadOnly = False
                    '追加終了 ---

                Case LMC020C.MODE_SHUSAN
                    '終算日修正モード
                    .imdHokanEndDate.ReadOnly = False
                    '(2013.03.11)要望番号1912 荷役料有無も修正可能にする -- START --
                    .cmbNiyaku.ReadOnly = False
                    '(2013.03.11)要望番号1912 荷役料有無も修正可能にする --  END  --

                Case LMC020C.MODE_UNSO_CHANGE

                    '運送情報のロック制御
                    If String.IsNullOrEmpty(Convert.ToString(.cmbTehaiKbn.SelectedValue)) = True Then
                        '選ばれていない場合
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = True
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = True
                        .txtUnsoSitenCd.ReadOnly = True
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    ElseIf ("10").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                        '先方手配・未定の場合
                        .cmbTariffKbun.ReadOnly = True
                        .cmbSyaryoKbn.ReadOnly = True
                        .cmbBinKbn.ReadOnly = True
                        .cmbMotoCyakuKbn.ReadOnly = False
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = False
                        .txtUnsoSitenCd.ReadOnly = False
                        .txtUnthinTariffCd.ReadOnly = True
                        .txtExtcTariffCd.ReadOnly = True
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = True
                        .txtPayExtcTariffCd.ReadOnly = True
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = True
                        .cmbUnsoKazeiKbn.ReadOnly = True

                    Else
                        '日陸手配
                        .cmbTariffKbun.ReadOnly = False
                        .cmbBinKbn.ReadOnly = False
                        .cmbMotoCyakuKbn.ReadOnly = False
                        .numJuryo.ReadOnly = True
                        .numKyori.ReadOnly = True
                        .txtUnsoCompanyCd.ReadOnly = False
                        .txtUnsoSitenCd.ReadOnly = False
                        .txtUnthinTariffCd.ReadOnly = False
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        .txtPayUnthinTariffCd.ReadOnly = False
                        'END UMANO 要望番号1302 支払運賃に伴う修正。
                        .cmbYusoBrCd.ReadOnly = False
                        .cmbUnsoKazeiKbn.ReadOnly = False

                        If ("10").Equals(.cmbTariffKbun.SelectedValue) = True OrElse _
                            ("30").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：混載、特便の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        ElseIf ("20").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送手配：車扱いの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                            '運送元区分：横持ちの場合
                            .cmbSyaryoKbn.ReadOnly = False
                            .txtExtcTariffCd.ReadOnly = True
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = True
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                        If String.IsNullOrEmpty(Convert.ToString(.cmbTariffKbun.SelectedValue)) = True Then
                            '運送元区分：未選択の場合
                            .cmbSyaryoKbn.ReadOnly = True
                            .txtExtcTariffCd.ReadOnly = False
                            'START UMANO 要望番号1302 支払運賃に伴う修正。
                            .txtPayExtcTariffCd.ReadOnly = False
                            'END UMANO 要望番号1302 支払運賃に伴う修正。
                        End If

                    End If

                Case LMC020C.MODE_TODOKESAKI_CHANGE '届先区分の変更時


                    '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                                        String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                      "' AND CUST_CD = '", .txtCust_Cd_L.TextValue & .txtCust_Cd_M.TextValue, _
                                                                      "' AND SUB_KB = '53'"))
                    Dim bYusyutu As Boolean = False '輸出情報にするフラグをセット
                    If custDetailsDr.Length > 0 Then
                        bYusyutu = True
                    End If
                    '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End


                    If ("00").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        '届先の場合
                        .btnNew.Enabled = True
                        .txtTodokesakiCd.ReadOnly = False
                        .txtTodokesakiNm.ReadOnly = True
                        .txtTodokeAdderss1.ReadOnly = True
                        .txtTodokeAdderss2.ReadOnly = True
                    ElseIf ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        '輸出情報の場合
                        .btnNew.Enabled = False

                        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
                        '.txtTodokesakiCd.ReadOnly = True
                        If bYusyutu Then
                            .txtTodokesakiCd.ReadOnly = False
                        Else
                            .txtTodokesakiCd.ReadOnly = True
                        End If
                        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End

                        .txtTodokesakiNm.ReadOnly = False
                        .txtTodokeAdderss1.ReadOnly = False
                        .txtTodokeAdderss2.ReadOnly = False
                    ElseIf ("02").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        'EDIの場合
                        .btnNew.Enabled = False
                        .txtTodokesakiCd.ReadOnly = True
                        .txtTodokesakiNm.ReadOnly = True
                        .txtTodokeAdderss1.ReadOnly = True
                        .txtTodokeAdderss2.ReadOnly = True
                    End If

                    '2014/01/30 輸出情報追加 START
                    If ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                        '届け先区分が輸出情報の場合
                        .txtShipNm.ReadOnly = False
                        .txtDestination.ReadOnly = False
                        .txtBookingNo.ReadOnly = False
                        .txtVoyageNo.ReadOnly = False
                        .txtShipperCd.ReadOnly = False
                        .imdContLoadingDate.ReadOnly = False
                        .imdStorageTestDate.ReadOnly = False
                        .txtStorageTestTime.ReadOnly = False
                        .imdDepartureDate.ReadOnly = False
                        .txtContainerNo.ReadOnly = False
                        .txtContainerNm.ReadOnly = False
                        .cmbContainerSize.ReadOnly = False
                    Else
                        '上記以外の場合
                        .txtShipNm.ReadOnly = True
                        .txtDestination.ReadOnly = True
                        .txtBookingNo.ReadOnly = True
                        .txtVoyageNo.ReadOnly = True
                        .txtShipperCd.ReadOnly = True
                        .imdContLoadingDate.ReadOnly = True
                        .imdStorageTestDate.ReadOnly = True
                        .txtStorageTestTime.ReadOnly = True
                        .imdDepartureDate.ReadOnly = True
                        .txtContainerNo.ReadOnly = True
                        .txtContainerNm.ReadOnly = True
                        .cmbContainerSize.ReadOnly = True
                    End If
                    '2014/01/30 輸出情報追加 START

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat()

        With Me._frm

            Me._LMCconG.SetDateFormat(.imdSyukkaDate)
            Me._LMCconG.SetDateFormat(.imdSyukkaYoteiDate)
            Me._LMCconG.SetDateFormat(.imdNounyuYoteiDate)
            Me._LMCconG.SetDateFormat(.imdSyukkaHoukoku)
            Me._LMCconG.SetDateFormat(.imdHokanEndDate)

            '2014/01/22 輸出情報追加 START
            Me._LMCconG.SetDateFormat(.imdContLoadingDate)
            Me._LMCconG.SetDateFormat(.imdStorageTestDate)
            Me._LMCconG.SetDateFormat(.imdDepartureDate)
            '2014/01/22 輸出情報追加 END

        End With
    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._frm

            Dim d2 As Decimal = Convert.ToDecimal(LMC020C.SORT_MAX)
            Dim sharp2 As String = "#0"
            Dim d4 As Decimal = Convert.ToDecimal(LMC020C.NB_MAX_4)
            Dim sharp4 As String = "#,##0"
            Dim d5 As Decimal = Convert.ToDecimal(LMC020C.NB_MAX_5)
            Dim sharp5 As String = "##,##0"
            Dim d6_3 As Decimal = Convert.ToDecimal(LMC020C.IRIME_MAX)
            Dim sharp6_3 As String = "###,##0.000"
            Dim d9_3 As Decimal = Convert.ToDecimal(LMC020C.QT_MAX)
            Dim sharp9_3 As String = "###,###,##0.000"
            Dim d10 As Decimal = Convert.ToDecimal(LMC020C.NB_MAX_10)
            Dim sharp10 As String = "#,###,###,##0"
            Dim d20 As Decimal = Convert.ToDecimal("99999999999999999999")
            Dim sharp20 As String = "##,###,###,###,###,###,##0"
            Dim d12_3 As Decimal = Convert.ToDecimal(LMC020C.SURYO_MAX)
            Dim sharp12_3 As String = "###,###,###,##0.000"

            .numPrtCnt.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numPrtCnt_From.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numPrtCnt_To.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numKonpoKosu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numSyukkaSouKosu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numPrintSortHenko.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numPrintSort.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numIrime.SetInputFields(sharp6_3, , 6, 1, , 3, 3, , d6_3, 0)
            .numPkgCnt.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numKonsu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numHasu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numIrisu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numSouKosu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numHikiateKosuSumi.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numHikiateKosuZan.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numSouSuryo.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numHikiateSuryoSumi.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numHikiateSuryoZan.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numJuryo.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numKyori.SetInputFields(sharp5, , 5, 1, , 0, 0, , d5, 0)

            '2015.07.08 協立化学　シッピングマーク対応 追加START
            Dim d3 As Decimal = Convert.ToDecimal(LMC020C.NB_MAX_3)
            Dim sharp3 As String = "#,##0"
            .numCaseNoFrom.SetInputFields(sharp3, , 3, 1, , 0, 0, , d3, 0)
            .numCaseNoTo.SetInputFields(sharp3, , 3, 1, , 0, 0, , d3, 0)
            '2015.07.08 協立化学　シッピングマーク対応 追加END

            .lblEdiOutkaTtlNb.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .lblEdiOutkaTtlQt.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)

            .numKitakuKakaku.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._frm

            '初期 
            .imdSyukkaYoteiDate.Focus()

        End With

    End Sub

    '''' <summary>
    '''' タブのフォーカスの設定(項目チェック・保存エラー時を除く)
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetTabFoucus()

    '    With Me._frm

    '        .tabMiddle.SelectTab(.tabUnso)

    '    End With

    'End Sub

#If True Then       'ADD 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定

    ''' <summary>
    ''' コントロール値のクリア（大項目）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlL2(ByVal nowDate As String, ByVal ds As DataSet)

#If True Then       'ADD 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定
        Dim prmDr As DataRow = ds.Tables("LMC020IN").Rows(0)
        Dim custLCd As String = prmDr.Item("CUST_CD_L").ToString()
        Dim custMCd As String = prmDr.Item("CUST_CD_M").ToString()
        Dim nrsBrCd As String = prmDr.Item("NRS_BR_CD").ToString()

#End If

        With Me._frm

            '出荷大
            .cmbPRINT.SelectedValue = String.Empty
            .numPrtCnt.Value = 1
            .numPrtCnt_From.Value = 1
            .lblSyukkaLNo.TextValue = String.Empty
            .cmbEigyosyo.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .lblFurikaeNo.TextValue = String.Empty
            .imdSyukkaDate.TextValue = String.Empty

            .imdSyukkaYoteiDate.TextValue = nowDate

#If True Then       'UPd 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定
            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))

            Dim custSetFLG As String = LMConst.FLG.OFF
            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                      "CUST_CD_L = '", custLCd, "' AND ", _
                                                                                                      "CUST_CD_M = '", custMCd, "'"))

            If 0 < custDr.Length Then
                custSetFLG = LMConst.FLG.ON

                Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                    Case Else

                        custSetFLG = LMConst.FLG.OFF
                End Select
            End If

            If (custSetFLG).Equals(LMConst.FLG.OFF) = True Then
                Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))

                If 0 < mUser.Length Then
                    Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_04
                            '前営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_05
                            '翌営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                    End Select

                End If
            End If
#Else
            Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
            If 0 < mUser.Length Then
                Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))
                Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                End Select

            End If
#End If

            .imdNounyuYoteiDate.TextValue = String.Empty
            .cmbNounyuKbn.SelectedValue = String.Empty
            .imdSyukkaHoukoku.TextValue = String.Empty
            .imdHokanEndDate.TextValue = String.Empty
            .cmbSyukkaKbn.SelectedValue = "10"
            .cmbSyukkaSyubetu.SelectedValue = "10"
            .cmbSagyoSintyoku.SelectedValue = String.Empty
            .chkNifuda.Checked = False
            .chkNHS.Checked = False
            .chkDenp.Checked = False
            .chkCoa.Checked = False
            .chkHokoku.Checked = False
            .txtNisyuTyumonNo.TextValue = String.Empty
            .txtKainusiTyumonNo.TextValue = String.Empty
            .cmbPick.SelectedValue = "01"
            .cmbOutkaHokoku_Yn.SelectedValue = String.Empty
            .cmbToukiYn.SelectedValue = "01"
            .txtCust_Cd_L.TextValue = String.Empty
            .txtCust_Cd_M.TextValue = String.Empty
            .txtCust_Nm.TextValue = String.Empty
            .cmbNiyaku.SelectedValue = "01"
            .numKonpoKosu.Value = 0
            .numSyukkaSouKosu.Value = 0
            .txtUriCd.TextValue = String.Empty
            .lblUriNm.TextValue = String.Empty
            .cmbOkurijo.SelectedValue = "00"
            .txtOkuriNo.TextValue = String.Empty
            .cmbTodokesaki.SelectedValue = "00"
            .txtTodokesakiCd.TextValue = String.Empty
            'START YANAI 要望番号909
            .txtTodokesakiCdOld.TextValue = String.Empty
            'END YANAI 要望番号909
            .txtTodokesakiNm.TextValue = String.Empty
            .cmbSiteinouhin.SelectedValue = String.Empty
            .cmbBunsakiTmp.SelectedValue = String.Empty
            .txtTodokeAdderss1.TextValue = String.Empty
            .txtTodokeTel.TextValue = String.Empty
            .txtNouhinTeki.TextValue = String.Empty
            .txtTodokeAdderss2.TextValue = String.Empty
            .txtSyukkaRemark.TextValue = String.Empty
            .txtOrderType.TextValue = String.Empty
            .txtTodokeAdderss3.TextValue = String.Empty
            .txtHaisoRemark.TextValue = String.Empty
            'START YANAI 要望番号909
            .txtHaisoRemarkNew.TextValue = String.Empty
            'END YANAI 要望番号909
            '運送
            .txtUnsoNo.TextValue = String.Empty
            .cmbTehaiKbn.SelectedValue = String.Empty
            .cmbTariffKbun.SelectedValue = String.Empty
            .cmbSyaryoKbn.SelectedValue = String.Empty
            .cmbBinKbn.SelectedValue = String.Empty
            .cmbMotoCyakuKbn.SelectedValue = String.Empty
            .numJuryo.Value = 0
            .numKyori.Value = 0
            .txtUnsoCompanyCd.TextValue = String.Empty
            .txtUnsoSitenCd.TextValue = String.Empty
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsoCompanyCdOld.TextValue = String.Empty
            .txtUnsoSitenCdOld.TextValue = String.Empty
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .lblUnsoCompanyNm.TextValue = String.Empty
            .lblUnsoSitenNm.TextValue = String.Empty
            .txtUnthinTariffCd.TextValue = String.Empty
            .lblUnthinTariffNm.TextValue = String.Empty
            .lblUnsoTareYn.TextValue = String.Empty
            .txtExtcTariffCd.TextValue = String.Empty
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayUnthinTariffCd.TextValue = String.Empty
            .lblPayUnthinTariffNm.TextValue = String.Empty
            .txtPayExtcTariffCd.TextValue = String.Empty
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .cmbYusoBrCd.SelectedValue = String.Empty
            '要望番号:2408 2015.09.17 追加START
            .cmbAutoDenpKbn.SelectedValue = String.Empty
            .lblAutoDenpNo.TextValue = String.Empty
            '要望番号:2408 2015.09.17 追加END
            .txtSagyoL1.TextValue = String.Empty
            .lblSagyoL1.TextValue = String.Empty
            .txtSagyoRemarkL1.TextValue = String.Empty
            .txtSagyoL2.TextValue = String.Empty
            .lblSagyoL2.TextValue = String.Empty
            .txtSagyoRemarkL2.TextValue = String.Empty
            .txtSagyoL3.TextValue = String.Empty
            .lblSagyoL3.TextValue = String.Empty
            .txtSagyoRemarkL3.TextValue = String.Empty
            .txtSagyoL4.TextValue = String.Empty
            .lblSagyoL4.TextValue = String.Empty
            .txtSagyoRemarkL4.TextValue = String.Empty
            .txtSagyoL5.TextValue = String.Empty
            .lblSagyoL5.TextValue = String.Empty
            .txtSagyoRemarkL5.TextValue = String.Empty
            '要望番号:1683 yamanaka 2013.03.04 START
            .lblShiharaiGroupNo.TextValue = String.Empty
            .lblSeiqGroupNo.TextValue = String.Empty
            '要望番号:1683 yamanaka 2013.03.04 END

            '2014/01/22 輸出情報追加 START
            .txtShipNm.TextValue = String.Empty
            .txtDestination.TextValue = String.Empty
            .txtBookingNo.TextValue = String.Empty
            .txtVoyageNo.TextValue = String.Empty
            .txtShipperCd.TextValue = String.Empty
            .lblShipperNm.TextValue = String.Empty
            .imdContLoadingDate.TextValue = String.Empty
            .imdStorageTestDate.TextValue = String.Empty
            .txtStorageTestTime.TextValue = String.Empty
            .imdDepartureDate.TextValue = String.Empty
            .txtContainerNo.TextValue = String.Empty
            .txtContainerNm.TextValue = String.Empty
            .cmbContainerSize.TextValue = String.Empty
            '2014/01/22 輸出情報追加 END

            .txtSyukkaRemark.TextValue = String.Empty

        End With

    End Sub
#End If

    ''' <summary>
    ''' コントロール値のクリア（大項目）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlL(ByVal nowDate As String)

        With Me._frm

            '出荷大
            .cmbPRINT.SelectedValue = String.Empty
            .numPrtCnt.Value = 1
            .numPrtCnt_From.Value = 1
            .lblSyukkaLNo.TextValue = String.Empty
            .cmbEigyosyo.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .lblFurikaeNo.TextValue = String.Empty
            .imdSyukkaDate.TextValue = String.Empty

            .imdSyukkaYoteiDate.TextValue = nowDate

#If True Then      'ADD 2018/11/02  依頼番号 : 002621   【LMS】日立FN_納品書未印刷表示時、まとめﾋﾟｯｷﾝｸﾞ非表示
            'ここでは処理しない

            'Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
            'If 0 < mUser.Length Then
            '    Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))
            '    Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
            '        Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
            '            .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
            '        Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

            '        Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
            '            .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

            '        Case LMC020C.OUTKA_DATE_INIT_04
            '            '前営業日
            '            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

            '        Case LMC020C.OUTKA_DATE_INIT_05
            '            '翌営業日
            '            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

            '    End Select

            'End If
#Else
            'Dim prmDr As DataRow = prmDs.Tables(tblNm).Rows(0)
            'Dim custLCd As String = prmDr.Item("CUST_CD_L").ToString()
            'Dim custMCd As String = prmDr.Item("CUST_CD_M").ToString()
            'Dim nrsBrCd As String = prmDr.Item("NRS_BR_CD").ToString()

            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))

            Dim custSetFLG As String = LMConst.FLG.OFF
            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                      "CUST_CD_L = '", custLCd, "' AND ", _
                                                                                                      "CUST_CD_M = '", custMCd, "'"))

            If 0 < custDr.Length Then
                custSetFLG = LMConst.FLG.ON

                Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")
                    Case Else
                        custSetFLG = LMConst.FLG.OFF
                End Select
            End If

            If (custSetFLG).Equals(LMConst.FLG.OFF) = True Then
                '荷主より設定できないとき
                '現行　ユーザーマスタ設定より
                Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
                If 0 < mUser.Length Then
                    Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_04
                            '前営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_05
                            '翌営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                    End Select

                End If
            End If
#End If

            .imdNounyuYoteiDate.TextValue = String.Empty
            .cmbNounyuKbn.SelectedValue = String.Empty
            .imdSyukkaHoukoku.TextValue = String.Empty
            .imdHokanEndDate.TextValue = String.Empty
            .cmbSyukkaKbn.SelectedValue = "10"
            .cmbSyukkaSyubetu.SelectedValue = "10"
            .cmbSagyoSintyoku.SelectedValue = String.Empty
            .chkNifuda.Checked = False
            .chkNHS.Checked = False
            .chkDenp.Checked = False
            .chkCoa.Checked = False
            .chkHokoku.Checked = False
            .txtNisyuTyumonNo.TextValue = String.Empty
            .txtKainusiTyumonNo.TextValue = String.Empty
            .cmbPick.SelectedValue = "01"
            .cmbOutkaHokoku_Yn.SelectedValue = String.Empty
            .cmbToukiYn.SelectedValue = "01"
            .txtCust_Cd_L.TextValue = String.Empty
            .txtCust_Cd_M.TextValue = String.Empty
            .txtCust_Nm.TextValue = String.Empty
            .cmbNiyaku.SelectedValue = "01"
            .numKonpoKosu.Value = 0
            .numSyukkaSouKosu.Value = 0
            .txtUriCd.TextValue = String.Empty
            .lblUriNm.TextValue = String.Empty
            .cmbOkurijo.SelectedValue = "00"
            .txtOkuriNo.TextValue = String.Empty
            .cmbTodokesaki.SelectedValue = "00"
            .txtTodokesakiCd.TextValue = String.Empty
            'START YANAI 要望番号909
            .txtTodokesakiCdOld.TextValue = String.Empty
            'END YANAI 要望番号909
            .txtTodokesakiNm.TextValue = String.Empty
            .cmbSiteinouhin.SelectedValue = String.Empty
            .cmbBunsakiTmp.SelectedValue = String.Empty
            .txtTodokeAdderss1.TextValue = String.Empty
            .txtTodokeTel.TextValue = String.Empty
            .txtNouhinTeki.TextValue = String.Empty
            .txtTodokeAdderss2.TextValue = String.Empty
            .txtSyukkaRemark.TextValue = String.Empty
            .txtOrderType.TextValue = String.Empty
            .txtTodokeAdderss3.TextValue = String.Empty
            .txtHaisoRemark.TextValue = String.Empty
            'START YANAI 要望番号909
            .txtHaisoRemarkNew.TextValue = String.Empty
            'END YANAI 要望番号909
            '運送
            .txtUnsoNo.TextValue = String.Empty
            .cmbTehaiKbn.SelectedValue = String.Empty
            .cmbTariffKbun.SelectedValue = String.Empty
            .cmbSyaryoKbn.SelectedValue = String.Empty
            .cmbBinKbn.SelectedValue = String.Empty
            .cmbMotoCyakuKbn.SelectedValue = String.Empty
            .numJuryo.Value = 0
            .numKyori.Value = 0
            .txtUnsoCompanyCd.TextValue = String.Empty
            .txtUnsoSitenCd.TextValue = String.Empty
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsoCompanyCdOld.TextValue = String.Empty
            .txtUnsoSitenCdOld.TextValue = String.Empty
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .lblUnsoCompanyNm.TextValue = String.Empty
            .lblUnsoSitenNm.TextValue = String.Empty
            .txtUnthinTariffCd.TextValue = String.Empty
            .lblUnthinTariffNm.TextValue = String.Empty
            .lblUnsoTareYn.TextValue = String.Empty
            .txtExtcTariffCd.TextValue = String.Empty
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayUnthinTariffCd.TextValue = String.Empty
            .lblPayUnthinTariffNm.TextValue = String.Empty
            .txtPayExtcTariffCd.TextValue = String.Empty
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .cmbYusoBrCd.SelectedValue = String.Empty
            '要望番号:2408 2015.09.17 追加START
            .cmbAutoDenpKbn.SelectedValue = String.Empty
            .lblAutoDenpNo.TextValue = String.Empty
            '要望番号:2408 2015.09.17 追加END
            .txtSagyoL1.TextValue = String.Empty
            .lblSagyoL1.TextValue = String.Empty
            .txtSagyoRemarkL1.TextValue = String.Empty
            .txtSagyoL2.TextValue = String.Empty
            .lblSagyoL2.TextValue = String.Empty
            .txtSagyoRemarkL2.TextValue = String.Empty
            .txtSagyoL3.TextValue = String.Empty
            .lblSagyoL3.TextValue = String.Empty
            .txtSagyoRemarkL3.TextValue = String.Empty
            .txtSagyoL4.TextValue = String.Empty
            .lblSagyoL4.TextValue = String.Empty
            .txtSagyoRemarkL4.TextValue = String.Empty
            .txtSagyoL5.TextValue = String.Empty
            .lblSagyoL5.TextValue = String.Empty
            .txtSagyoRemarkL5.TextValue = String.Empty
            '要望番号:1683 yamanaka 2013.03.04 START
            .lblShiharaiGroupNo.TextValue = String.Empty
            .lblSeiqGroupNo.TextValue = String.Empty
            '要望番号:1683 yamanaka 2013.03.04 END

            '2014/01/22 輸出情報追加 START
            .txtShipNm.TextValue = String.Empty
            .txtDestination.TextValue = String.Empty
            .txtBookingNo.TextValue = String.Empty
            .txtVoyageNo.TextValue = String.Empty
            .txtShipperCd.TextValue = String.Empty
            .lblShipperNm.TextValue = String.Empty
            .imdContLoadingDate.TextValue = String.Empty
            .imdStorageTestDate.TextValue = String.Empty
            .txtStorageTestTime.TextValue = String.Empty
            .imdDepartureDate.TextValue = String.Empty
            .txtContainerNo.TextValue = String.Empty
            .txtContainerNm.TextValue = String.Empty
            .cmbContainerSize.TextValue = String.Empty
            '2014/01/22 輸出情報追加 END
            .txtSijiRemark.TextValue = String.Empty
            .chkNoSiji.Checked = False
            .chkTabHokoku.Checked = False

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(中項目)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlM()

        With Me._frm

            '出荷中
            .lblSyukkaMNo.TextValue = String.Empty
            .numPrintSort.TextValue = String.Empty
            .lblHikiate.TextValue = String.Empty
            .txtGoodsCdCust.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .txtRsvNo.TextValue = String.Empty
            .txtSerialNo.TextValue = String.Empty
            .txtCyumonNo.TextValue = String.Empty
            .optTempY.Checked = False
            .optTempN.Checked = True
            .optCnt.Checked = True
            .optAmt.Checked = False
            .optKowake.Checked = False
            .optSample.Checked = False
            .numIrime.Value = 0
            .numPkgCnt.Value = 0
            .cmbUnsoOndo.SelectedValue = String.Empty
            .cmbTakkyuSize.SelectedValue = String.Empty
            .numKonsu.Value = 0
            .numIrisu.Value = 0
            .numSouKosu.Value = 0
            .numHasu.Value = 0
            .numHikiateKosuSumi.Value = 0
            .numHikiateKosuZan.Value = 0
            .numSouSuryo.Value = 0
            .numHikiateSuryoSumi.Value = 0
            .numHikiateSuryoZan.Value = 0
            .txtGoodsRemark.TextValue = String.Empty
            .txtSagyoM1.TextValue = String.Empty
            .lblSagyoM1.TextValue = String.Empty
            .txtSagyoRemarkM1.TextValue = String.Empty
            .txtSagyoM2.TextValue = String.Empty
            .lblSagyoM2.TextValue = String.Empty
            .txtSagyoRemarkM2.TextValue = String.Empty
            .txtSagyoM3.TextValue = String.Empty
            .lblSagyoM3.TextValue = String.Empty
            .txtSagyoRemarkM3.TextValue = String.Empty
            .txtSagyoM4.TextValue = String.Empty
            .lblSagyoM4.TextValue = String.Empty
            .txtSagyoRemarkM4.TextValue = String.Empty
            .txtSagyoM5.TextValue = String.Empty
            .lblSagyoM5.TextValue = String.Empty
            .txtSagyoRemarkM5.TextValue = String.Empty
            .txtDestSagyoM1.TextValue = String.Empty
            .lblDestSagyoM1.TextValue = String.Empty
            .txtDestSagyoRemarkM1.TextValue = String.Empty
            .txtDestSagyoM2.TextValue = String.Empty
            .lblDestSagyoM2.TextValue = String.Empty
            .txtDestSagyoRemarkM2.TextValue = String.Empty

#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
            .lblIrimeUT.TextValue = String.Empty
            .lblIrimeUThide.TextValue = String.Empty
            .lblKonsuUT.TextValue = String.Empty
            .lblHasuUT.TextValue = String.Empty
            .lblSuryouUT.TextValue = String.Empty
#Else
            .lblIrimeUT.KbnValue = ""
            .lblKonsuUT.KbnValue = ""
            .lblHasuUT.KbnValue = ""
            .lblSuryouUT.KbnValue = ""
#End If



            .lblGoodsCdNrs.TextValue = String.Empty
            .lblGoodsCdNrsFrom.TextValue = String.Empty
            .lblRecNo.TextValue = String.Empty
            .lblCustCdS.TextValue = String.Empty
            .lblCustCdSS.TextValue = String.Empty

            'START YANAI メモ②No.26
            .lblRemark.TextValue = String.Empty
            .lblRemarkOut.TextValue = String.Empty
            'END YANAI メモ②No.26
            'START YANAI 要望番号681
            .lblTaniKowake.TextValue = String.Empty
            'END YANAI 要望番号681

            '要望番号1959 START
            .lblEdiOutkaTtlNb.Value = 0
            .lblEdiOutkaTtlQt.Value = 0
            '要望番号1959 END

        End With

    End Sub

    '2015.07.08 協立化学　シッピングマーク対応 追加START
    ''' <summary>
    ''' コントロール値のクリア(マーク(HED))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlMarkHed()

        With Me._frm

            'マーク(HED)
            .txtOutkaNoM.TextValue = String.Empty
            .numCaseNoFrom.Value = 0
            .numCaseNoTo.Value = 0
            .txtMarkInfo1.TextValue = String.Empty
            .txtMarkInfo2.TextValue = String.Empty
            .txtMarkInfo3.TextValue = String.Empty
            .txtMarkInfo4.TextValue = String.Empty
            .txtMarkInfo5.TextValue = String.Empty
            .txtMarkInfo6.TextValue = String.Empty
            .txtMarkInfo7.TextValue = String.Empty
            .txtMarkInfo8.TextValue = String.Empty
            .txtMarkInfo9.TextValue = String.Empty
            .txtMarkInfo10.TextValue = String.Empty

        End With

    End Sub
    '2015.07.08 協立化学　シッピングマーク対応 追加END

    ''' <summary>
    ''' 複写時のコントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyControlL()

        With Me._frm

            '出荷大
            .lblSyukkaLNo.TextValue = String.Empty
            .lblFurikaeNo.TextValue = String.Empty
            .cmbSagyoSintyoku.SelectedValue = String.Empty
            .txtNisyuTyumonNo.TextValue = String.Empty
            .txtKainusiTyumonNo.TextValue = String.Empty
            'START YANAI 要望番号606
            .numKonpoKosu.Value = 0
            'END YANAI 要望番号606
            '(2013.01.18)要望番号1784 保管料終算日は複写しない -- START --
            .imdHokanEndDate.TextValue = String.Empty
            '(2013.01.18)要望番号1784 保管料終算日は複写しない --  END  --
            .chkNifuda.Checked = False
            .chkNHS.Checked = False
            .chkDenp.Checked = False
            .chkCoa.Checked = False
            .chkHokoku.Checked = False
            'タブレット項目
            .chkUrgent.Checked = False
            .cmbWHSagyoSintyoku.SelectedValue = String.Empty
            .chkNoSiji.Checked = False
            .chkTabHokoku.Checked = False
            .txtHokoku.TextValue = String.Empty

            '運送
            .txtUnsoNo.TextValue = String.Empty
            '作業大
            .txtSagyoL1.TextValue = String.Empty
            .lblSagyoL1.TextValue = String.Empty
            .txtSagyoRemarkL1.TextValue = String.Empty
            .txtSagyoL2.TextValue = String.Empty
            .lblSagyoL2.TextValue = String.Empty
            .txtSagyoRemarkL2.TextValue = String.Empty
            .txtSagyoL3.TextValue = String.Empty
            .lblSagyoL3.TextValue = String.Empty
            .txtSagyoRemarkL3.TextValue = String.Empty
            .txtSagyoL4.TextValue = String.Empty
            .lblSagyoL4.TextValue = String.Empty
            .txtSagyoRemarkL4.TextValue = String.Empty
            .txtSagyoL5.TextValue = String.Empty
            .lblSagyoL5.TextValue = String.Empty
            .txtSagyoRemarkL5.TextValue = String.Empty
            '出荷中
            .lblSyukkaMNo.TextValue = String.Empty
            .numPrintSort.TextValue = String.Empty
            .lblHikiate.TextValue = String.Empty
            .txtGoodsCdCust.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .txtRsvNo.TextValue = String.Empty
            .txtSerialNo.TextValue = String.Empty
            .txtCyumonNo.TextValue = String.Empty
            .optTempY.Checked = False
            .optTempN.Checked = True
            .optCnt.Checked = True
            .optAmt.Checked = False
            .optKowake.Checked = False
            .optSample.Checked = False
            .numIrime.Value = 0
            .numPkgCnt.Value = 0
            .cmbUnsoOndo.SelectedValue = String.Empty
            .cmbTakkyuSize.SelectedValue = String.Empty
            .numKonsu.Value = 0
            .numIrisu.Value = 0
            .numSouKosu.Value = 0
            .numHasu.Value = 0
            .numHikiateKosuSumi.Value = 0
            .numHikiateKosuZan.Value = 0
            .numSouSuryo.Value = 0
            .numHikiateSuryoSumi.Value = 0
            .numHikiateSuryoZan.Value = 0
            .txtGoodsRemark.TextValue = String.Empty


#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
            .lblIrimeUT.TextValue = String.Empty
            .lblIrimeUThide.TextValue = String.Empty
            .lblKonsuUT.TextValue = String.Empty
            .lblHasuUT.TextValue = String.Empty
            .lblSuryouUT.TextValue = String.Empty
#Else
            .lblIrimeUT.KbnValue = ""
            .lblKonsuUT.KbnValue = ""
            .lblHasuUT.KbnValue = ""
            .lblSuryouUT.KbnValue = ""
#End If




            '作業中
            .txtSagyoM1.TextValue = String.Empty
            .lblSagyoM1.TextValue = String.Empty
            .txtSagyoM2.TextValue = String.Empty
            .lblSagyoM2.TextValue = String.Empty
            .txtSagyoM3.TextValue = String.Empty
            .lblSagyoM3.TextValue = String.Empty
            .txtSagyoM4.TextValue = String.Empty
            .lblSagyoM4.TextValue = String.Empty
            .txtSagyoM5.TextValue = String.Empty
            .lblSagyoM5.TextValue = String.Empty
            .txtDestSagyoM1.TextValue = String.Empty
            .lblDestSagyoM1.TextValue = String.Empty
            .txtDestSagyoM2.TextValue = String.Empty
            .lblDestSagyoM2.TextValue = String.Empty
            .txtSagyoRemarkM1.TextValue = String.Empty
            .txtSagyoRemarkM2.TextValue = String.Empty
            .txtSagyoRemarkM3.TextValue = String.Empty
            .txtSagyoRemarkM4.TextValue = String.Empty
            .txtSagyoRemarkM5.TextValue = String.Empty
            .txtDestSagyoRemarkM1.TextValue = String.Empty
            .txtDestSagyoRemarkM2.TextValue = String.Empty

            '運送
            .numJuryo.Value = 0

            .lblGoodsCdNrs.TextValue = String.Empty
            .lblGoodsCdNrsFrom.TextValue = String.Empty
            .lblRecNo.TextValue = String.Empty
            .lblCustCdS.TextValue = String.Empty
            .lblCustCdSS.TextValue = String.Empty
            'START YANAI メモ②No.26
            .lblRemark.TextValue = String.Empty
            .lblRemarkOut.TextValue = String.Empty
            'END YANAI メモ②No.26
            'START YANAI 要望番号681
            .lblTaniKowake.TextValue = String.Empty
            'END YANAI 要望番号681

            '2014/01/22 輸出情報追加 START
            .txtShipNm.TextValue = String.Empty
            .txtDestination.TextValue = String.Empty
            .txtBookingNo.TextValue = String.Empty
            .txtVoyageNo.TextValue = String.Empty
            .txtShipperCd.TextValue = String.Empty
            .lblShipperNm.TextValue = String.Empty
            .imdContLoadingDate.TextValue = String.Empty
            .imdStorageTestDate.TextValue = String.Empty
            .txtStorageTestTime.TextValue = String.Empty
            .imdDepartureDate.TextValue = String.Empty
            .txtContainerNo.TextValue = String.Empty
            .txtContainerNm.TextValue = String.Empty
            .cmbContainerSize.TextValue = String.Empty
            '2014/01/22 輸出情報追加 END

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
            .lblAutoDenpNo.TextValue = String.Empty
#End If

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア（運送手配・タリフ分類変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlUnso()

        With Me._frm

            '運送のクリア制御
            If ("10").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '日陸手配以外の場合
                .cmbTariffKbun.SelectedValue = Nothing
                .cmbUnsoKazeiKbn.SelectedValue = Nothing

                '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 Start
                .cmbYusoBrCd.SelectedValue = Nothing
                '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 End

                '要望番号:2408 2015.09.17 追加START
                .cmbAutoDenpKbn.SelectedValue = Nothing
                .lblAutoDenpNo.TextValue = String.Empty
                '要望番号:2408 2015.09.17 追加END

            End If

            '元着払い
            If .cmbMotoCyakuKbn.ReadOnly = True Then
                .cmbMotoCyakuKbn.SelectedValue = Nothing
            ElseIf String.IsNullOrEmpty(.cmbMotoCyakuKbn.TextValue) = True Then
                .cmbMotoCyakuKbn.SelectedValue = "01"
            End If

            '便区分
            If .cmbBinKbn.ReadOnly = True Then
                .cmbBinKbn.SelectedValue = Nothing
            ElseIf String.IsNullOrEmpty(.cmbBinKbn.TextValue) = True Then
                .cmbBinKbn.SelectedValue = "01"
            End If

            '車輌区分
            If .cmbSyaryoKbn.ReadOnly = True Then
                .cmbSyaryoKbn.SelectedValue = Nothing
            End If

            '距離
            .numKyori.Value = 0

            '運送会社コード
            If .txtUnsoCompanyCd.ReadOnly = True Then
                .txtUnsoCompanyCd.TextValue = String.Empty
                .lblUnsoCompanyNm.TextValue = String.Empty
                'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                .txtUnsoCompanyCdOld.TextValue = String.Empty
                'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            End If

            '運送会社支店コード
            If .txtUnsoSitenCd.ReadOnly = True Then
                .txtUnsoSitenCd.TextValue = String.Empty
                .lblUnsoSitenNm.TextValue = String.Empty
                'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                .txtUnsoSitenCdOld.TextValue = String.Empty
                'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            End If

            '運賃タリフ
            If .txtUnthinTariffCd.ReadOnly = True Then
                .txtUnthinTariffCd.TextValue = String.Empty
                .lblUnthinTariffNm.TextValue = String.Empty
            End If

            '割増タリフ
            If .txtExtcTariffCd.ReadOnly = True Then
                .txtExtcTariffCd.TextValue = String.Empty
            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            '(支払)運賃タリフ
            If .txtPayUnthinTariffCd.ReadOnly = True Then
                .txtPayUnthinTariffCd.TextValue = String.Empty
                .lblPayUnthinTariffNm.TextValue = String.Empty
            End If

            '(支払)割増タリフ
            If .txtPayExtcTariffCd.ReadOnly = True Then
                .txtPayExtcTariffCd.TextValue = String.Empty
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            '輸送営業所
            If .cmbYusoBrCd.Enabled = False Then
                .cmbYusoBrCd.SelectedValue = Nothing
            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア（運送のその他いろいろ設定する項目）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDefaultUnso()

        With Me._frm

            '運送のデフォルト値設定
            '元着払い
            If .cmbMotoCyakuKbn.ReadOnly = True Then
                .cmbMotoCyakuKbn.SelectedValue = Nothing
            Else
                .cmbMotoCyakuKbn.SelectedValue = "01"
            End If

            '便区分
            If .cmbBinKbn.ReadOnly = True AndAlso ("10").Equals(.cmbTehaiKbn.SelectedValue) = True Then
                .cmbBinKbn.SelectedValue = Nothing
            Else
                .cmbBinKbn.SelectedValue = "01"
            End If


            '運送のロック制御設定
            If ("10").Equals(.cmbTariffKbun.SelectedValue) = True OrElse _
                ("30").Equals(.cmbTariffKbun.SelectedValue) = True Then
                '運送手配：混載、特便の場合
                .cmbSyaryoKbn.ReadOnly = True
                .txtExtcTariffCd.ReadOnly = False
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .txtPayExtcTariffCd.ReadOnly = False
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            ElseIf ("20").Equals(.cmbTariffKbun.SelectedValue) = True Then
                '運送手配：車扱いの場合
                .cmbSyaryoKbn.ReadOnly = False
                .txtExtcTariffCd.ReadOnly = True
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .txtPayExtcTariffCd.ReadOnly = True
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            End If

            If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                '運送元区分：横持ちの場合
                .cmbSyaryoKbn.ReadOnly = False
                .txtExtcTariffCd.ReadOnly = True
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .txtPayExtcTariffCd.ReadOnly = True
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            End If

            If String.IsNullOrEmpty(Convert.ToString(.cmbTariffKbun.SelectedValue)) = True Then
                '運送元区分：未選択の場合
                .cmbSyaryoKbn.ReadOnly = True
                .txtExtcTariffCd.ReadOnly = False
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                .txtPayExtcTariffCd.ReadOnly = False
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            End If

            '輸送営業所
            If .cmbYusoBrCd.ReadOnly = True Then
                .cmbYusoBrCd.SelectedValue = Nothing
            Else
                .cmbYusoBrCd.SelectedValue = Base.LMUserInfoManager.GetNrsBrCd
            End If

            '要望番号:2408 2015.09.17 追加START
            If String.IsNullOrEmpty(.txtUnsoCompanyCd.TextValue) = False Then

            End If
            '要望番号:2408 2015.09.17 追加END

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア（検索条件）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlSearch()

        With Me._frm

            '検索条件のクリア制御
            .txtSerchGoodsCd.TextValue = String.Empty
            .txtSerchGoodsNm.TextValue = String.Empty
            .txtSerchLot.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア（届先区分変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlTodokesaki(ByVal ds As DataSet)

        With Me._frm

            If ("00").Equals(.cmbTodokesaki.SelectedValue) = True Then
                '届先の場合
                .txtTodokesakiNm.TextValue = String.Empty
                .txtTodokeAdderss1.TextValue = String.Empty
                .txtTodokeAdderss2.TextValue = String.Empty
            ElseIf ("01").Equals(.cmbTodokesaki.SelectedValue) = True Then
                '輸出情報の場合
                .txtTodokesakiCd.TextValue = String.Empty
                'START YANAI 要望番号909
                .txtTodokesakiCdOld.TextValue = String.Empty
                'END YANAI 要望番号909
            ElseIf ("02").Equals(.cmbTodokesaki.SelectedValue) = True Then
                'EDIの場合
                If 0 < ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows.Count Then
                    .txtTodokesakiCd.TextValue = ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows(0).Item("DEST_CD").ToString()
                    'START YANAI 要望番号909
                    .txtTodokesakiCdOld.TextValue = ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows(0).Item("DEST_CD").ToString()
                    'END YANAI 要望番号909
                    .txtTodokesakiNm.TextValue = ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows(0).Item("DEST_NM").ToString()
                    .txtTodokeAdderss1.TextValue = ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows(0).Item("DEST_AD_1").ToString()
                    .txtTodokeAdderss2.TextValue = ds.Tables(LMC020C.TABLE_NM_EDI_L).Rows(0).Item("DEST_AD_2").ToString()
                Else
                    .txtTodokesakiCd.TextValue = String.Empty
                    'START YANAI 要望番号909
                    .txtTodokesakiCdOld.TextValue = String.Empty
                    'END YANAI 要望番号909
                    .txtTodokesakiNm.TextValue = String.Empty
                    .txtTodokeAdderss1.TextValue = String.Empty
                    .txtTodokeAdderss2.TextValue = String.Empty
                End If

            End If

            '2014/01/30 輸出情報追加 START
            .txtShipNm.TextValue = String.Empty
            .txtDestination.TextValue = String.Empty
            .txtBookingNo.TextValue = String.Empty
            .txtVoyageNo.TextValue = String.Empty
            .txtShipperCd.TextValue = String.Empty
            .lblShipperNm.TextValue = String.Empty
            .imdContLoadingDate.TextValue = String.Empty
            .imdStorageTestDate.TextValue = String.Empty
            .txtStorageTestTime.TextValue = String.Empty
            .imdDepartureDate.TextValue = String.Empty
            .txtContainerNo.TextValue = String.Empty
            .txtContainerNm.TextValue = String.Empty
            .cmbContainerSize.TextValue = String.Empty
            '2014/01/30 輸出情報追加 END

        End With

    End Sub

    ''' <summary>
    ''' 部数の設定（印刷種別変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlPrintBusu()

        With Me._frm

            If ("01").Equals(.cmbPRINT.SelectedValue) = True Then
                If 99 < Convert.ToDecimal(.numKonpoKosu.Value) Then
                    .numPrtCnt.TextValue = "99"
                    .numPrtCnt_To.TextValue = "99"
                Else
                    .numPrtCnt_To.TextValue = .numKonpoKosu.TextValue
                End If
            Else
                .numPrtCnt.TextValue = "1"
                .numPrtCnt_From.TextValue = "1"
            End If

        End With

    End Sub

    ''' <summary>
    ''' 部数範囲の設定（印刷種別変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub PrintCntSetControl()

        With Me._frm

            .numPrtCnt.Visible = False
            .numPrtCnt_From.Visible = False
            .numPrtCnt_To.Visible = False
            .lblTitleBu.Visible = False
            .lblTitlePrtCntFromTo.Visible = False

            If ("01").Equals(.cmbPRINT.SelectedValue) = True Then
                .numPrtCnt_From.Visible = True
                .numPrtCnt_To.Visible = True
                .lblTitlePrtCntFromTo.Visible = True
            Else
                .numPrtCnt.Visible = True
                .lblTitleBu.Visible = True
            End If

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        With Me._frm

        End With

    End Sub

    ''' <summary>
    ''' シチュエーションラベルの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSituation(ByVal dispMode As String, ByVal recordStatus As String)

        '編集部の項目をクリア
        With Me._frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recordStatus
        End With
    End Sub

    ''' <summary>
    ''' 一覧検索画面で設定された荷主を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCustInit(ByVal dRow As DataRow)

        '荷主設定
        '荷主マスタよりデフォルト倉庫コードを取得
        Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                   "NRS_BR_CD = '", dRow.Item("NRS_BR_CD").ToString, "' AND " _
                                                                                 , "CUST_CD_L = '", dRow.Item("CUST_CD_L").ToString, "' AND " _
                                                                                 , "CUST_CD_M = '", dRow.Item("CUST_CD_M").ToString, "' AND " _
                                                                                 , "CUST_CD_S = '00' AND " _
                                                                                 , "CUST_CD_SS = '00'"))

        Me._frm.cmbEigyosyo.SelectedValue = dRow.Item("NRS_BR_CD").ToString
        'Me._frm.cmbSoko.SelectedValue = dRow.Item("WH_CD").ToString
        Me._frm.cmbSoko.SelectedValue = custDr(0).Item("DEFAULT_SOKO_CD").ToString
        Me._frm.txtCust_Cd_L.TextValue = dRow.Item("CUST_CD_L").ToString
        Me._frm.txtCust_Cd_M.TextValue = dRow.Item("CUST_CD_M").ToString
        Me._frm.txtCust_Nm.TextValue = dRow.Item("CUST_NM").ToString
        Me._frm.cmbTehaiKbn.SelectedValue = custDr(0).Item("UNSO_TEHAI_KB").ToString
        Me._frm.lblCust_Nm_L.TextValue = custDr(0).Item("CUST_NM_L").ToString()
        Me._frm.lblCust_Nm_M.TextValue = custDr(0).Item("CUST_NM_M").ToString()

        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
        With Me._frm
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                                String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                              "' AND CUST_CD = '", .txtCust_Cd_L.TextValue & .txtCust_Cd_M.TextValue, _
                                                              "' AND SUB_KB = '53'"))
            If custDetailsDr.Length > 0 Then
                .cmbTodokesaki.SelectedValue = "01"
            Else
                .cmbTodokesaki.SelectedValue = "00"
            End If
        End With
        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End

#If True Then       'ADD 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定

        Dim custLCd As String = dRow.Item("CUST_CD_L").ToString
        Dim custMCd As String = dRow.Item("CUST_CD_M").ToString
        Dim nrsBrCd As String = dRow.Item("NRS_BR_CD").ToString

        With Me._frm

            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))

            Dim custSetFLG As String = LMConst.FLG.OFF
            'Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
            '                                                                                          "CUST_CD_L = '", custLCd, "' AND ", _
            '                                                                                          "CUST_CD_M = '", custMCd, "'"))
            If 0 < custDr.Length Then
                custSetFLG = LMConst.FLG.ON

                Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                    Case Else

                        custSetFLG = LMConst.FLG.OFF
                End Select
            End If

            If (custSetFLG).Equals(LMConst.FLG.OFF) = True Then
                Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))

                If 0 < mUser.Length Then
                    Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                            .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_04
                            '前営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_05
                            '翌営業日
                            .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                    End Select

                End If
            End If
        End With
#End If

    End Sub

    ''' <summary>
    ''' 一覧検索画面で設定された出荷予定日を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetOutkaPlanDate(ByVal dRow As DataRow)

        '出荷予定日設定
        Dim outkaPlanDate As String = dRow.Item("OUTKA_PLAN_DATE").ToString

        If String.IsNullOrEmpty(outkaPlanDate) = False Then
            Me._frm.imdSyukkaYoteiDate.TextValue = outkaPlanDate
        End If

    End Sub

#If False Then       'ADD 2018/10/31 'ADD 2018/10/31 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定
    ''' <summary>
    ''' 荷主マスタより出荷予定日初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetOutkaPlanDate2(ByVal frm As LMC020F)

        Dim nowDate As String = Convert.ToString(Me.SysData(LMC020C.SysData.YYYYMMDD))

        With frm

            Dim nrsBrCd As String
            Dim custLCd As String
            Dim custMCd As String

            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                          "CUST_CD_L = '", custLCd, "' AND ", _
                                                                                          "CUST_CD_M = '", custMCd, "'"))

            If 0 < custDr.Length Then
                Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(.imdSyukkaYoteiDate.TextValue), "0000/00/00"))

                Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02
                        .imdSyukkaYoteiDate.TextValue = tmpdate

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdSyukkaYoteiDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdSyukkaYoteiDate.TextValue = Format(GetBussinessDay(.imdSyukkaYoteiDate.TextValue, +1), "yyyyMMdd")

                End Select

            End If
        End With

    End Sub

#End If

    '要望番号:1793 terakawa 2013.01.21 Start
    ''' <summary>
    ''' 一覧検索画面で設定されたまとめピック区分を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetPickKb(ByVal dRow As DataRow)

        'まとめピック区分設定
        Dim pickKb As String = dRow.Item("PICK_KB").ToString

        If String.IsNullOrEmpty(pickKb) = False Then
            Me._frm.cmbPick.SelectedValue = pickKb
        End If

    End Sub
    '要望番号:1793 terakawa 2013.01.21 End


    ''' <summary>
    ''' POPから取得した荷主を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCust(ByVal dRow As DataRow)

        '荷主設定
        Me._frm.cmbEigyosyo.SelectedValue = dRow.Item("NRS_BR_CD").ToString
        Me._frm.cmbSoko.SelectedValue = dRow.Item("DEFAULT_SOKO_CD").ToString
        If String.IsNullOrEmpty(dRow.Item("DEFAULT_SOKO_CD").ToString) = True Then
            Me._frm.cmbSoko.SelectedValue = LM.Base.LMUserInfoManager.GetWhCd().ToString()
        End If
        Me._frm.txtCust_Cd_L.TextValue = dRow.Item("CUST_CD_L").ToString
        Me._frm.txtCust_Cd_M.TextValue = dRow.Item("CUST_CD_M").ToString
        Me._frm.txtCust_Nm.TextValue = String.Concat(dRow.Item("CUST_NM_L").ToString, dRow.Item("CUST_NM_M").ToString)
        Me._frm.cmbTehaiKbn.SelectedValue = dRow.Item("UNSO_TEHAI_KB").ToString

        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 Start
        With Me._frm
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                                String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                              "' AND CUST_CD = '", .txtCust_Cd_L.TextValue & .txtCust_Cd_M.TextValue, _
                                                              "' AND SUB_KB = '53'"))
            If custDetailsDr.Length > 0 Then
                .cmbTodokesaki.SelectedValue = "01"
            Else
                .cmbTodokesaki.SelectedValue = "00"
            End If
        End With
        '要望番号:1820（千葉日立物流　届先区分を輸出情報とし、届先テキストコードを使用可とする）対応　 2013/02/04 本明 End

    End Sub

    ''' <summary>
    ''' 運送会社をキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUnsoCompany(ByVal frm As LMC020F, Optional ByVal dr As DataRow = Nothing)

        With frm

            If .txtUnsoCompanyCd.ReadOnly = True Then
                Exit Sub
            End If

            '要望番号:1568 terakawa 2013.01.17 Start
            If dr IsNot Nothing AndAlso _
               String.IsNullOrEmpty(dr.Item("UNSOCO_CD").ToString()) = False AndAlso _
               String.IsNullOrEmpty(dr.Item("UNSOCO_BR_CD").ToString()) = False Then
                '遷移元で運送会社が設定されていた場合、そちらを優先する
                .txtUnsoCompanyCd.TextValue = dr.Item("UNSOCO_CD").ToString
                .txtUnsoSitenCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString

            Else
                '荷主マスタより運賃請求先マスタコードを取得
                Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                         "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                         , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                                         , "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND " _
                                                                                         , "CUST_CD_S = '00' AND " _
                                                                                         , "CUST_CD_SS = '00'"))
                If custDr.Length = 0 Then
                    '荷主がヒットしない場合はこれ以上やりようがないので、処理を終わる
                    Exit Sub
                End If

                '運賃請求先マスタコードを元に、届先マスタより指定運送会社コードを取得
                '---↓
                'Dim destDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
                '                                                                         "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                         , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
                '                                                                         , "DEST_CD = '", custDr(0).Item("UNCHIN_SEIQTO_CD").ToString, "'"))

                Dim destMstDs As MDestDS = New MDestDS
                Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                destMstDr.Item("DEST_CD") = custDr(0).Item("UNCHIN_SEIQTO_CD").ToString
                destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
                destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                '---↑

                If destDr.Length > 0 Then
                    '届先マスタの運送会社コードを画面に設定
                    .txtUnsoCompanyCd.TextValue = destDr(0).Item("SP_UNSO_CD").ToString
                    .txtUnsoSitenCd.TextValue = destDr(0).Item("SP_UNSO_BR_CD").ToString
                End If

                If String.IsNullOrEmpty(.txtUnsoCompanyCd.TextValue) = True AndAlso _
                    String.IsNullOrEmpty(.txtUnsoSitenCd.TextValue) = True Then
                    '届先マスタの運送会社コードが未設定の場合は、荷主マスタの運送会社コードを設定する。
                    .txtUnsoCompanyCd.TextValue = custDr(0).Item("SP_UNSO_CD").ToString
                    .txtUnsoSitenCd.TextValue = custDr(0).Item("SP_UNSO_BR_CD").ToString
                End If
            End If
            '要望番号:1568 terakawa 2013.01.17 End

            If String.IsNullOrEmpty(.txtUnsoCompanyCd.TextValue) = True AndAlso _
                String.IsNullOrEmpty(.txtUnsoSitenCd.TextValue) = True Then
                '運送会社コード・支店コードの両方が空の場合は、この先の検索処理でヒットしないのがわかっているので、処理を終わる
                Exit Sub
            End If

            '指定運送会社コードを元に、運送会社マスタより運送会社名を取得
            Dim unsoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat( _
                                                                                      "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                    , "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND " _
                                                                                    , "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "'"))
            If unsoDr.Length = 0 Then
                Exit Sub
            End If
            '運送会社名を画面に設定
            .lblUnsoCompanyNm.TextValue = unsoDr(0).Item("UNSOCO_NM").ToString
            .lblUnsoSitenNm.TextValue = unsoDr(0).Item("UNSOCO_BR_NM").ToString
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            'If String.IsNullOrEmpty(.txtPayUnthinTariffCd.TextValue) = True Then
            .txtPayUnthinTariffCd.TextValue = unsoDr(0).Item("UNCHIN_TARIFF_CD").ToString()
            .lblPayUnthinTariffNm.TextValue = unsoDr(0).Item("SHIHARAI_TARIFF_REM").ToString()
            'End If
            'If String.IsNullOrEmpty(.txtPayExtcTariffCd.TextValue) = True Then
            .txtPayExtcTariffCd.TextValue = unsoDr(0).Item("EXTC_TARIFF_CD").ToString()
            'End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            '要望番号:2408 2015.09.17 追加START
            If unsoDr(0).Item("AUTO_DENP_NO_FLG").ToString.Equals(LMConst.FLG.ON) = True Then
                .cmbAutoDenpKbn.SelectedValue = unsoDr(0).Item("AUTO_DENP_NO_KBN").ToString
                If .cmbAutoDenpKbn.SelectedValue.Equals(.beforeAutoDenpKbn.SelectedValue) = False Then
                    .lblAutoDenpNo.TextValue = String.Empty
                End If
            End If
            '要望番号:2408 2015.09.17 追加END

        End With

    End Sub

    ''' <summary>
    ''' 運送会社をキャッシュより取得（届先コード変更時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUnsoCompanyChangeDest(ByVal frm As LMC020F)

        With frm

            If .txtUnsoCompanyCd.ReadOnly = True Then
                Exit Sub
            End If

            '運賃請求先マスタコードを元に、届先マスタより指定運送会社コードを取得
            '---↓
            'Dim destDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
            '                                                                         "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
            '                                                                         , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
            '                                                                         , "DEST_CD = '", .txtTodokesakiCd.TextValue, "'"))
            If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = True Then
                Exit Sub
            End If

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            destMstDr.Item("DEST_CD") = .txtTodokesakiCd.TextValue
            destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If destDr.Length > 0 Then
                '届先マスタの運送会社コードを画面に設定
                If String.IsNullOrEmpty(destDr(0).Item("SP_UNSO_CD").ToString) = True Then
                    '届先マスタの運送会社コードが
                    Exit Sub
                End If
                .txtUnsoCompanyCd.TextValue = destDr(0).Item("SP_UNSO_CD").ToString
                .txtUnsoSitenCd.TextValue = destDr(0).Item("SP_UNSO_BR_CD").ToString
            Else
                'この先の検索処理でヒットしないのがわかっているので、処理を終わる
                Exit Sub
            End If

            '指定運送会社コードを元に、運送会社マスタより運送会社名を取得
            Dim unsoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat( _
                                                                                      "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                    , "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND " _
                                                                                    , "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "'"))
            If unsoDr.Length = 0 Then
                Exit Sub
            End If
            '運送会社名を画面に設定
            .lblUnsoCompanyNm.TextValue = unsoDr(0).Item("UNSOCO_NM").ToString
            .lblUnsoSitenNm.TextValue = unsoDr(0).Item("UNSOCO_BR_NM").ToString

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            'If String.IsNullOrEmpty(.txtPayUnthinTariffCd.TextValue) = True Then
            .txtPayUnthinTariffCd.TextValue = unsoDr(0).Item("UNCHIN_TARIFF_CD").ToString()
            .lblPayUnthinTariffNm.TextValue = unsoDr(0).Item("SHIHARAI_TARIFF_REM").ToString()
            'End If
            'If String.IsNullOrEmpty(.txtPayExtcTariffCd.TextValue) = True Then
            .txtPayExtcTariffCd.TextValue = unsoDr(0).Item("EXTC_TARIFF_CD").ToString()
            'End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

    'START YANAI 要望番号745
    ''' <summary>
    ''' 売上先をキャッシュより取得（届先コード変更時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUriageChangeDest(ByVal frm As LMC020F)

        With frm

            '要望番号1943 修正START
            '修正　yamanaka 2012.11.21 Start
            'If .txtUriCd.ReadOnly = True OrElse String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = True then
            If .txtUriCd.ReadOnly = True OrElse String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = True _
                OrElse (String.IsNullOrEmpty(.txtUriCd.TextValue) = False AndAlso .txtUriCd.ReadOnly = False) Then
                Exit Sub
            End If
            '修正　yamanaka 2012.11.21 End
            '要望番号1943 修正END

            .txtUriCd.TextValue = String.Empty
            .lblUriNm.TextValue = String.Empty

            '届先マスタコードを元に、届先マスタより売上先コードを取得
            '---↓
            'Dim destDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
            '                                                                         "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
            '                                                                         , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
            '                                                                         , "DEST_CD = '", .txtTodokesakiCd.TextValue, "'"))

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            destMstDr.Item("DEST_CD") = .txtTodokesakiCd.TextValue
            destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If destDr.Length > 0 Then
                '届先マスタの売上先コードを画面に設定
                If String.IsNullOrEmpty(destDr(0).Item("URIAGE_CD").ToString) = True Then
                    Exit Sub
                End If
                .txtUriCd.TextValue = destDr(0).Item("URIAGE_CD").ToString
            Else
                'この先の検索処理でヒットしないのがわかっているので、処理を終わる
                Exit Sub
            End If

            '届先マスタコードを元に、届先マスタより売上先コードを取得
            '---↓
            'Dim destDr2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
            '                                                                         "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
            '                                                                         , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
            '                                                                         , "DEST_CD = '", .txtUriCd.TextValue, "'"))

            destMstDs = New MDestDS
            destMstDr = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            destMstDr.Item("DEST_CD") = .txtUriCd.TextValue
            destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            rtnDs = MyBase.GetDestMasterData(destMstDs)
            Dim destDr2 As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If destDr2.Length = 0 Then
                Exit Sub
            End If
            '運送会社名を画面に設定
            .lblUriNm.TextValue = destDr2(0).Item("DEST_NM").ToString

        End With

    End Sub
    'END YANAI 要望番号745

    'START YANAI 要望番号880
    ''' <summary>
    ''' 運賃タリフを運賃タリフセットマスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetTariffSet(ByVal frm As LMC020F)

        'START YANAI 要望番号909
        Dim updateFlg As Boolean = False
        'END YANAI 要望番号909

        With frm

            'START YANAI 要望番号909
            '届先情報上書き確認チェック
            updateFlg = Me._V.IsReturnDestChk()
            If updateFlg = False Then
                .txtHaisoRemarkNew.TextValue = String.Empty
                Exit Sub
            End If
            'END YANAI 要望番号909

            Dim tariffSetDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(String.Concat("CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                                "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                                                "DEST_CD = '", .txtTodokesakiCd.TextValue, "' AND ", _
                                                                                                                                "SET_KB = '01'"))

            If 0 < tariffSetDr.Length Then
                .txtUnthinTariffCd.TextValue = String.Empty
                .lblUnthinTariffNm.TextValue = String.Empty
                .txtExtcTariffCd.TextValue = String.Empty
                .cmbTariffKbun.SelectedValue = tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString

                If ("10").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '混載の場合
                    .txtUnthinTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD1").ToString
                    .lblUnthinTariffNm.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_REM1").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("20").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '車扱いの場合
                    .txtUnthinTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD2").ToString
                    .lblUnthinTariffNm.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_REM2").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("30").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '特便の場合
                ElseIf ("40").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '横持ちの場合
                    .txtUnthinTariffCd.TextValue = tariffSetDr(0).Item("YOKO_TARIFF_CD").ToString
                    .lblUnthinTariffNm.TextValue = tariffSetDr(0).Item("YOKO_TARIFF_REM").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                End If

            End If
            'START YANAI 要望番号909
            .txtHaisoRemark.TextValue = .txtHaisoRemarkNew.TextValue
            .txtHaisoRemarkNew.TextValue = String.Empty
            'END YANAI 要望番号909

        End With

    End Sub
    'END YANAI 要望番号880

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運賃タリフを運賃タリフセットマスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUnchinTariffSet(ByVal frm As LMC020F, ByVal tariffBunruiFlg As Boolean)

        Dim updateFlg As Boolean = False

        With frm
            Dim tariffSetDr() As DataRow = Nothing

            If tariffBunruiFlg = False Then
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbTehaiKbn.SelectedValue, "'"))
            Else
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbTehaiKbn.SelectedValue, "' AND ", _
                                                                                                                          "TARIFF_BUNRUI_KB = '", .cmbTariffKbun.SelectedValue, "'"))
            End If

            If 0 < tariffSetDr.Length Then
                .txtUnthinTariffCd.TextValue = String.Empty
                .lblUnthinTariffNm.TextValue = String.Empty
                .txtExtcTariffCd.TextValue = String.Empty
                If tariffBunruiFlg = False Then
                    .cmbTariffKbun.SelectedValue = tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString
                End If

                If ("10").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '混載の場合
                    .txtUnthinTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblUnthinTariffNm.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("20").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '車扱いの場合
                    .txtUnthinTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblUnthinTariffNm.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("30").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '特便の場合
                ElseIf ("40").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '横持ちの場合
                End If

            End If

        End With

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    '要望番号:2408 2015.09.17 追加START
    ''' <summary>
    ''' 自動送状区分を運送会社マスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetAutoDenpSet(ByVal frm As LMC020F)

        Dim updateFlg As Boolean = False

        With frm
            Dim unsoSetDr() As DataRow = Nothing

            unsoSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "'"))
            If 0 < unsoSetDr.Length Then
                If unsoSetDr(0).Item("AUTO_DENP_NO_FLG").ToString.Equals(LMConst.FLG.ON) = True Then
                    .cmbAutoDenpKbn.SelectedValue = unsoSetDr(0).Item("AUTO_DENP_NO_KBN").ToString
                Else
                    .cmbAutoDenpKbn.SelectedValue = String.Empty
                End If
                If .cmbAutoDenpKbn.SelectedValue.Equals(.beforeAutoDenpKbn.SelectedValue) = False Then
                    .lblAutoDenpNo.TextValue = String.Empty
                End If
            End If

        End With

    End Sub
    '要望番号:2408 2015.09.17 追加END

    'START YANAI 要望番号909
    ''' <summary>
    ''' 届先コードOLD設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetDestCdOld(ByVal frm As LMC020F)

        frm.txtTodokesakiCdOld.TextValue = frm.txtTodokesakiCd.TextValue

    End Sub
    'END YANAI 要望番号909

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードOLD設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoCdOld(ByVal frm As LMC020F)

        frm.txtUnsoCompanyCdOld.TextValue = frm.txtUnsoCompanyCd.TextValue
        frm.txtUnsoSitenCdOld.TextValue = frm.txtUnsoSitenCd.TextValue

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetKosuSuryo()

        With Me._frm

            Dim kLock As Boolean = True '個数のロック制御
            Dim sLock As Boolean = True '数量のロック制御
            Dim hLock As Boolean = True '端数のロック制御

            If .optCnt.Checked = True Then
                kLock = False
                sLock = True

                If .numIrisu.TextValue.Equals("1") = True Then
                    hLock = True
                Else
                    hLock = False
                End If
            Else
                kLock = True
                sLock = False
                hLock = True
            End If


            '梱数・端数・数量のロック制御
            If .numPrintSort.ReadOnly = False Then
                '参照モード以外の時にここでロック制御したいので、印順のReadOnlyで判定
                .numKonsu.ReadOnly = kLock
                .numHasu.ReadOnly = hLock
                .numSouSuryo.ReadOnly = sLock
            End If

        End With

    End Sub

    ''' <summary>
    ''' 倉庫の入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSoko(ByVal cnt As Integer)

        With Me._frm

            '出荷(中)が0件だったら倉庫をロック
            If cnt = 0 Then
                .cmbSoko.ReadOnly = False
            Else
                .cmbSoko.ReadOnly = True
            End If

        End With

    End Sub

    ''' <summary>
    ''' 運送のデフォルト値を、運賃タリフセットから取得し、設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetUnsoTariffSet(ByVal insFlg As Boolean)

        With Me._frm

            '運賃タリフセットマスタからタリフコードを取得し、設定

            Dim tariffSet As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                              "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                              "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                                              "SET_KB = '00' AND ", _
                                                                                                                              "DEST_CD = ''"))

            If 0 < tariffSet.Length = True Then
                .cmbTariffKbun.SelectedValue = tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()

                If insFlg = True AndAlso _
                    ("90").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso _
                    String.IsNullOrEmpty(.cmbTariffKbun.SelectedValue.ToString) = True Then
                    '手配区分が"未定"以外で、タリフ分類区分が未設定の時は、手配区分を"未定"にする
                    .cmbTehaiKbn.SelectedValue = "90"
                End If

                If .txtUnthinTariffCd.ReadOnly = False AndAlso ("10").Equals(.cmbTehaiKbn.SelectedValue) = True Then
                    If ("40").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '横持ちの場合は横持ちタリフコードをセット
                        .txtUnthinTariffCd.TextValue = tariffSet(0).Item("YOKO_TARIFF_CD").ToString()
                        .lblUnthinTariffNm.TextValue = tariffSet(0).Item("YOKO_TARIFF_REM").ToString()
                    ElseIf ("20").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '車扱いの場合は運賃タリフコード２をセット
                        .txtUnthinTariffCd.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_CD2").ToString()
                        .lblUnthinTariffNm.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_REM2").ToString()
                    Else
                        '横持ち以外の場合は運賃タリフコードをセット
                        .txtUnthinTariffCd.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_CD1").ToString()
                        .lblUnthinTariffNm.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_REM1").ToString()
                    End If

                End If

                If .txtExtcTariffCd.ReadOnly = False AndAlso ("10").Equals(.cmbTehaiKbn.SelectedValue) = True Then
                    .txtExtcTariffCd.TextValue = tariffSet(0).Item("EXTC_TARIFF_CD").ToString()
                End If
            Else
                If insFlg = True Then
                    '手配区分が"未定"以外で、タリフ分類区分が未設定の時は、手配区分を"未定"にするのだが、
                    'タリフセットからデータが取得できない時点でタリフ分類は空白確定なので、"未定"にする。
                    .cmbTehaiKbn.SelectedValue = "90"
                End If
            End If

        End With

    End Sub

    '要望対応:1595 yamanaka 2012.11.15 End
    ''' <summary>
    ''' 商品情報のロック設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ChangeGoodsLock(ByVal cnt As Integer)

        With Me._frm

            If .sprSyukkaM.ActiveSheet.RowCount = 0 Then
                .txtGoodsCdCust.ReadOnly = True
                .lblGoodsNm.ReadOnly = True

            ElseIf .cmbEigyosyo.SelectedValue.Equals("40") AndAlso .txtCust_Cd_L.TextValue.Equals("00588") Then
                '横浜デュポン(00588)は別処理があるため処理を行わない

            Else
                If cnt = 0 Then
                    .txtGoodsCdCust.ReadOnly = False
                    .lblGoodsNm.ReadOnly = False
                Else
                    .txtGoodsCdCust.ReadOnly = True
                    .lblGoodsNm.ReadOnly = True
                End If
            End If

        End With

    End Sub
    '要望対応:1595 yamanaka 2012.11.15 End

    '追加開始 --- 2014.07.24 kikuchi
    ''' <summary>
    ''' 商品情報のロック設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub setBunsekiTemp(ByVal boo As Boolean)

        With Me._frm

            If boo = True Then
                .optTempY.Checked = True
                .optTempN.Checked = False
            Else
                .optTempY.Checked = False
                .optTempN.Checked = True
            End If

        End With

    End Sub
    '追加終了 ---

    ''' <summary>
    ''' タブレット項目の初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetWHTablet()
        With Me._frm

            .chkTablet.Checked = False
            .chkUrgent.Checked = False

            Dim nrsBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd
            Dim kbnDr() As DataRow = Nothing
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'B007' AND ", _
                                                                                           "KBN_CD = '", nrsBrCd, "' AND ", _
                                                                                           "VALUE1 = '1.000'"))
            If kbnDr.Length > 0 Then
                .chkTablet.Checked = True
            End If

            '荷主明細 アラートメール対象外チェック
            Dim custDtlDr() As DataRow = Nothing
            custDtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                                        "CUST_CD = '", .txtCust_Cd_L.TextValue, .txtCust_Cd_M.TextValue, "' AND ",
                                                                                                        "SUB_KB = '1O' AND ",
                                                                                                        "SET_NAIYO = '1' "))
            If custDtlDr.Length > 0 Then
                .chkTablet.Checked = False
            End If

        End With
    End Sub

    ''' <summary>
    ''' 現場作業指示チェックの付与
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetChkTabletOnChangeOutS(ByRef ds As DataSet, ByVal dt As DataTable)

        With Me._frm

            'タブレット使用の前提条件確認
            Dim nrsBrCd As String = .cmbEigyosyo.SelectedValue.ToString
            Dim whCd As String = .cmbSoko.SelectedValue.ToString

            'タブレット対応営業所
            Dim kbnDr() As DataRow = Nothing
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'B007' AND ",
                                                                                           "KBN_CD = '", nrsBrCd, "' AND ",
                                                                                           "VALUE1 = '1.000'"))
            If kbnDr.Length = 0 Then
                Exit Sub
            End If

            'ロケ管理対象
            Dim sokoDr() As DataRow = Nothing
            sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                                 "WH_CD = '", whCd, "' "))

            If sokoDr.Length > 0 Then
                If "00".Equals(sokoDr(0).Item("LOC_MANAGER_YN").ToString) Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            'アラートメール
            Dim custDtlDr() As DataRow = Nothing
            custDtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                                        "CUST_CD = '", .txtCust_Cd_L.TextValue, .txtCust_Cd_M.TextValue, "' AND ",
                                                                                                        "SUB_KB = '1O' AND ",
                                                                                                        "SET_NAIYO = '1' "))
            If custDtlDr.Length > 0 Then
                Exit Sub
            End If

            '既存出荷Sの自社他社確認
            Dim osTbl As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_S)
            Dim oldJisya As Boolean = False
            Dim oldTasya As Boolean = False
            Dim tabYnChgFlg As Boolean = False

            For i As Integer = 0 To osTbl.Rows.Count - 1
                Dim tsRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select(
                                                     String.Concat("NRS_BR_CD = '", nrsBrCd, "'",
                                                              " AND TOU_NO    = '", osTbl.Rows(i).Item("TOU_NO").ToString, "'",
                                                              " AND SITU_NO   = '", osTbl.Rows(i).Item("SITU_NO").ToString, "'",
                                                              " AND WH_CD     = '", whCd, "'"))
                If tsRow.Length = 0 Then
                    Continue For
                End If
                If "02".Equals(tsRow(0).Item("JISYATASYA_KB").ToString) Then
                    oldTasya = True
                Else
                    oldJisya = True
                End If
            Next

            '追加した出荷Sの自社他社確認
            Dim newJisya As Boolean = False
            Dim newTasya As Boolean = False
            For j As Integer = 0 To dt.Rows.Count - 1
                Dim tsRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select(
                                                     String.Concat("NRS_BR_CD = '", nrsBrCd, "'",
                                                              " AND TOU_NO    = '", dt.Rows(j).Item("TOU_NO").ToString, "'",
                                                              " AND SITU_NO   = '", dt.Rows(j).Item("SITU_NO").ToString, "'",
                                                              " AND WH_CD     = '", whCd, "'"))
                If tsRow.Length = 0 Then
                    Continue For
                End If
                If "02".Equals(tsRow(0).Item("JISYATASYA_KB").ToString) Then
                    newJisya = True
                Else
                    newJisya = True
                End If
            Next
            '変更前データに自社がない且つ追加データに自社がある
            If oldJisya = False AndAlso newJisya = True Then
                .chkTablet.Checked = True
            End If

        End With
    End Sub
#End Region

#Region "検索結果表示"


#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprSyukkaM
        'スプレッド(タイトル列)の設定
        Public Shared DEFM As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.DEFM, " ", 20, True)
        Public Shared PRT_ORDER As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.PRT_ORDER, "印順", 40, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SHOBO_CD, "消防" & vbCrLf & "CD", 35, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.KANRI_NO, "出荷管理" & vbCrLf & "番号(中)", 80, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.GOODS_CD, "商品コード", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.GOODS_NM, "商品名", 150, True)
        Public Shared SYUKKA_TANI As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SYUKKA_TANI, "出荷単位", 65, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.IRIME, "入目", 90, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.NB, "個数", 100, True)
        Public Shared ALL_SURYO As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.ALL_SURYO, "数量", 120, True)
        'START YANAI 要望番号651
        'Public Shared ZANSU As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.ZANSU, "残個数", 100, True)
        Public Shared ZANSU As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.ZANSU, "引当残", 100, True)
        'END YANAI 要望番号651
        Public Shared HIKIATE_JK As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.HIKIATE_JK, "引当" & vbCrLf & "状況", 35, True)
        Public Shared GOODS_COMMENT As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.GOODS_COMMENT, "商品別注意事項", 140, True)
        '(2012.12.21)要望番号1710 ロット№追加 -- START --
        Public Shared M_LOT_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.M_LOT_NO, "ロット№", 140, True)
        '(2012.12.21)要望番号1710 ロット№追加 --  END  --
        Public Shared SHOBO_NM As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SHOBO_NM, "消防情報名", 120, True)
        'invisible
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SYS_DEL_FLG, "削除フラグ", 1, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.REC_NO, "レコード番号", 1, False)
        Public Shared SEARCH_KEY_1 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SEARCH_KEY_1, "荷主カテゴリ1", 1, False)
        Public Shared UNSO_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.UNSO_ONDO_KB, "運送温度区分", 1, False)
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.PKG_UT, "包装単位", 1, False)
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.STD_IRIME_NB, "標準入目", 1, False)
        Public Shared STD_WT_KGS As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.STD_WT_KGS, "標準重量KGS", 1, False)
        Public Shared TARE_YN As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.TARE_YN, "風袋加算フラグ", 1, False)
        Public Shared OUTKA_ATT As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.OUTKA_ATT, "出荷時注意事項", 1, False)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.REMARK, "備考小(社内)", 1, False)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.REMARK_OUT, "備考小(社外)", 1, False)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.TAX_KB, "課税区分", 1, False)
        Public Shared HIKIATE_ALERT_YN As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.HIKIATE_ALERT_YN, "引当注意品", 1, False)
        Public Shared GOODS_CD_NRS_FROM As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.GOODS_CD_NRS_FROM, "振替元商品キー", 1, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.CUST_CD_S, "荷主コード小", 1, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.CUST_CD_SS, "荷主コード極小", 1, False)
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.NB_UT, "NB_UT", 1, False)
        'START YANAI メモ②No.20
        Public Shared EDI_FLG As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.EDI_FLG, "EDI出荷管理番号L", 1, False)
        'END YANAI メモ②No.20
        'START YANAI 要望番号681
        Public Shared SYUKKA_TANI_KOWAKE As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.SYUKKA_TANI_KOWAKE, "出荷単位(小分け→個数)", 80, False)
        'END YANAI 要望番号681
        '2015.07.08 協立化学　シッピング対応　追加START
        Public Shared CASE_NO_FROM As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.CASE_NO_FROM, "ケース№(開始)", 100, False)
        Public Shared CASE_NO_TO As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.CASE_NO_TO, "(終了)", 100, False)
        'Public Shared MARK_INFO_1 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_1, "１行目", 65, False)
        'Public Shared MARK_INFO_2 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_2, "２行目", 65, False)
        'Public Shared MARK_INFO_3 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_3, "３行目", 65, False)
        'Public Shared MARK_INFO_4 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_4, "４行目", 65, False)
        'Public Shared MARK_INFO_5 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_5, "５行目", 65, False)
        'Public Shared MARK_INFO_6 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_6, "６行目", 65, False)
        'Public Shared MARK_INFO_7 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_7, "７行目", 65, False)
        'Public Shared MARK_INFO_8 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_8, "８行目", 65, False)
        'Public Shared MARK_INFO_9 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_9, "９行目", 65, False)
        'Public Shared MARK_INFO_10 As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_INFO_10, "１０行目", 65, False)
        Public Shared MARK_SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_SYS_DEL_FLG, "削除F", 65, False)
        Public Shared MARK_UP_KBN As SpreadColProperty = New SpreadColProperty(LMC020C.sprSyukkaMColumnIndex.MARK_UP_KBN, "更新区分", 65, False)
        '2015.07.08 協立化学　シッピング対応　追加START

    End Class

    Public Class sprDtl
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.DEF, " ", 20, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.LOT_NO, "ロット№", 140, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.IRIME, "入目", 70, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared SHITSU_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SHITSU_NO, "室", 30, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ZONE_CD, "ZONE", 60, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.LOCA, "ロケーション", 100, True)
        Public Shared ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_NB, "引当個数", 80, True)
        Public Shared ALCTD_QT As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_QT, "引当数量", 80, True)
        Public Shared ALCTD_CAN_NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_CAN_NB, "引当可能個数", 100, True)
        Public Shared ALCTD_CAN_QT As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_CAN_QT, "引当可能数量", 100, True)
        Public Shared NAKAMI As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.NAKAMI, "状態 中身", 100, True)
        Public Shared GAIKAN As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.GAIKAN, "状態 外装", 100, True)
        Public Shared JOTAI_NM As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.JOTAI_NM, "状態 荷主", 100, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.REMARK, "備考小(社内)", 100, True)
        Public Shared INKO_DATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.INKO_DATE, "入荷日", 80, True)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.LT_DATE, "有効期限", 80, True)
        Public Shared HORYUHIN As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.HORYUHIN, "保留品", 80, True)
        Public Shared BOGAIHIN As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.BOGAIHIN, "簿外品", 80, True)
        Public Shared RSV_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.RSV_NO, "予約番号", 90, True)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SERIAL_NO, "シリアル№", 90, True)
        Public Shared GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.GOODS_CRT_DATE, "製造日", 80, True)
        Public Shared WARIATE_NM As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.WARIATE_NM, "割当優先", 100, True)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.NYUBAN_S, "備考小(社外)", 100, True)
        Public Shared SHO_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SHO_NO, "出荷管理" & vbCrLf & "番号(小)", 80, True)

        'invisible
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ZAI_REC_NO, "在庫レコード番号", 1, False)
        Public Shared INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.INKA_NO_L, "入荷管理番号(大)", 1, False)
        Public Shared INKA_NO_M As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.INKA_NO_M, "入荷管理番号(中)", 1, False)
        Public Shared INKA_NO_S As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.INKA_NO_S, "入荷管理番号(小)", 1, False)
        Public Shared SMPL_FLAG As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SMPL_FLAG, "小分けフラグ", 1, False)
        Public Shared GOODS_COND_KB_1 As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.GOODS_COND_KB_1, "状態 中身", 1, False)
        Public Shared GOODS_COND_KB_2 As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.GOODS_COND_KB_2, "状態 外装", 1, False)
        Public Shared GOODS_COND_KB_3 As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.GOODS_COND_KB_3, "状態 荷主", 1, False)
        Public Shared OFB_KB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.OFB_KB, "保留品", 1, False)
        Public Shared SPD_KB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SPD_KB, "状簿外品", 1, False)
        Public Shared PORA_ZAI_NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.PORA_ZAI_NB, "実予在庫個数", 1, False)
        Public Shared PORA_ZAI_QT As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.PORA_ZAI_QT, "実予在庫数量", 1, False)
        Public Shared ALLOC_CAN_NB_HOZON As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALLOC_CAN_NB_HOZON, "引当可能個数（更新前保持）", 1, False)
        Public Shared ALLOC_CAN_QT_HOZON As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALLOC_CAN_QT_HOZON, "引当可能数量（更新前保持）", 1, False)
        Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALLOC_CAN_NB, "引当可能個数", 1, False)
        Public Shared ALLOC_CAN_QT As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALLOC_CAN_QT, "引当可能数量", 1, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SYS_DEL_FLG, "削除フラグ", 1, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.REC_NO, "レコード番号", 1, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SYS_UPD_DATE, "更新日付", 1, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.SYS_UPD_TIME, "更新時間", 1, False)
        Public Shared HOKAN_YN As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.HOKAN_YN, "保管料有無", 1, False)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.TAX_KB, "課税区分", 1, False)
        Public Shared INKO_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.INKO_PLAN_DATE, "入荷予定日", 1, False)
        Public Shared WARIATE As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.WARIATE, "割当優先", 1, False)
        Public Shared ALCTD_NB_HOZON As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_NB_HOZON, "引当中個数（更新前保持）", 1, False)
        Public Shared ALCTD_QT_HOZON As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_QT_HOZON, "引当中数量（更新前保持）", 1, False)
        Public Shared DEST_CD_P As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.DEST_CD_P, "届先コード", 1, False)
        Public Shared UPDATE_FLG As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.UPDATE_FLG, "", 1, False)
        Public Shared ALCTD_CAN_NB_MATOME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_CAN_NB_MATOME, "引当可能個数（まとめ前）", 1, False)
        Public Shared ALCTD_CAN_QT_MATOME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_CAN_QT_MATOME, "引当可能数量（まとめ前）", 1, False)
        Public Shared ALCTD_NB_MATOME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_NB_MATOME, "引当個数（まとめ前）", 1, False)
        Public Shared ALCTD_QT_MATOME As SpreadColProperty = New SpreadColProperty(LMC020C.sprDtlMColumnIndex.ALCTD_QT_MATOME, "引当数量（まとめ前）", 1, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定（出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpreadM()

        With Me._frm

            'スプレッドの行をクリア
            .sprSyukkaM.CrearSpread()

            '列数設定
            .sprSyukkaM.Sheets(0).ColumnCount = LMC020C.sprSyukkaMColCount

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSyukkaM.SetColProperty(New sprSyukkaM)
            .sprSyukkaM.SetColProperty(New sprSyukkaM, False)
            '2015.10.15 英語化対応END

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの初期設定（出荷(小))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpreadS()

        With Me._frm

            'スプレッドの行をクリア
            .sprDtl.CrearSpread()

            '列数設定
            .sprDtl.Sheets(0).ColumnCount = LMC020C.sprDtlMColCount

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDtl.SetColProperty(New sprDtl)
            .sprDtl.SetColProperty(New sprDtl, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ロケーションで固定)
            .sprDtl.Sheets(0).FrozenColumnCount = sprDtl.LOCA.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)
        'Friend Sub SetInitValue(ByVal ds As DataSet, ByVal activeCol As Integer)

        '出荷(大)の情報を設定
        Call Me.SetOutLControl(ds)

        '運送(大)の情報を設定
        Call Me.SetUnsoLControl(ds)

        '作業(大)の情報を設定
        Call Me.SetSagyoLControl(ds)

        '出荷（中）の情報を設定
        Call Me.SetSpread(_frm.sprSyukkaM, ds, LMConst.FLG.OFF, -1)

        '2014/01/22 輸出情報追加 START
        Call Me.SetExportLControl(ds)
        '2014/01/22 輸出情報追加 END

        '2015.07.08 協立化学　シッピングマーク対応　追加START
        'シッピングマーク(HED)の情報を設定
        Call Me.SetMarkHedControl(ds, -1)
        '2015.07.08 協立化学　シッピングマーク対応　追加END

    End Sub

    ''' <summary>
    ''' 出荷（大）に値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetOutLControl(ByVal ds As DataSet)

        With Me._frm

            Dim dr As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)

            .lblSyukkaLNo.TextValue = dr.Item("OUTKA_NO_L").ToString()
            .cmbEigyosyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbSoko.SelectedValue = dr.Item("WH_CD").ToString()
            .lblFurikaeNo.TextValue = dr.Item("FURI_NO").ToString()
            .imdSyukkaDate.TextValue = dr.Item("OUTKO_DATE").ToString()
            .imdSyukkaYoteiDate.TextValue = dr.Item("OUTKA_PLAN_DATE").ToString()
            .imdNounyuYoteiDate.TextValue = dr.Item("ARR_PLAN_DATE").ToString()
            .cmbNounyuKbn.SelectedValue = dr.Item("ARR_PLAN_TIME").ToString()
            .imdSyukkaHoukoku.TextValue = dr.Item("HOKOKU_DATE").ToString()
            .imdHokanEndDate.TextValue = dr.Item("END_DATE").ToString()
            .cmbSyukkaKbn.SelectedValue = dr.Item("OUTKA_KB").ToString()
            .cmbSyukkaSyubetu.SelectedValue = dr.Item("SYUBETU_KB").ToString()
            .cmbSagyoSintyoku.SelectedValue = dr.Item("OUTKA_STATE_KB").ToString()
            If ("01").Equals(dr.Item("NIHUDA_FLAG").ToString()) = True Then
                .chkNifuda.Checked = True
            Else
                .chkNifuda.Checked = False
            End If
            If ("01").Equals(dr.Item("NHS_FLAG").ToString()) = True Then
                .chkNHS.Checked = True
            Else
                .chkNHS.Checked = False
            End If
            If ("01").Equals(dr.Item("DENP_FLAG").ToString()) = True Then
                .chkDenp.Checked = True
            Else
                .chkDenp.Checked = False
            End If
            If ("01").Equals(dr.Item("COA_FLAG").ToString()) = True Then
                .chkCoa.Checked = True
            Else
                .chkCoa.Checked = False
            End If
            If ("01").Equals(dr.Item("HOKOKU_FLAG").ToString()) = True Then
                .chkHokoku.Checked = True
            Else
                .chkHokoku.Checked = False
            End If
            .txtNisyuTyumonNo.TextValue = dr.Item("CUST_ORD_NO").ToString()
            .txtKainusiTyumonNo.TextValue = dr.Item("BUYER_ORD_NO").ToString()
            .cmbPick.SelectedValue = dr.Item("PICK_KB").ToString()
            .cmbOutkaHokoku_Yn.SelectedValue = dr.Item("OUTKAHOKOKU_YN").ToString()
            .cmbToukiYn.SelectedValue = dr.Item("TOUKI_HOKAN_YN").ToString()
            .txtCust_Cd_L.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCust_Cd_M.TextValue = dr.Item("CUST_CD_M").ToString()
            .txtCust_Nm.TextValue = dr.Item("CUST_NM").ToString()
            .cmbNiyaku.SelectedValue = dr.Item("NIYAKU_YN").ToString()
            .numKonpoKosu.Value = Me._LMCconG.FormatNumValue(dr.Item("OUTKA_PKG_NB").ToString())
            .txtUriCd.TextValue = dr.Item("SHIP_CD_L").ToString()
            .lblUriNm.TextValue = dr.Item("SHIP_NM").ToString()
            .cmbOkurijo.SelectedValue = dr.Item("DENP_YN").ToString()
            .txtOkuriNo.TextValue = dr.Item("DENP_NO").ToString()
            .cmbSiteinouhin.SelectedValue = dr.Item("SP_NHS_KB").ToString()
            .cmbBunsakiTmp.SelectedValue = dr.Item("COA_YN").ToString()
            .txtNouhinTeki.TextValue = dr.Item("NHS_REMARK").ToString()
            .txtSyukkaRemark.TextValue = dr.Item("REMARK").ToString()
            .txtOrderType.TextValue = dr.Item("ORDER_TYPE").ToString()

            .lblCust_Nm_L.TextValue = dr.Item("CUST_NM_L").ToString()
            .lblCust_Nm_M.TextValue = dr.Item("CUST_NM_M").ToString()

            .cmbTodokesaki.SelectedValue = dr.Item("DEST_KB").ToString()
            .txtTodokesakiCd.TextValue = dr.Item("DEST_CD").ToString()
            'START YANAI 要望番号909
            .txtTodokesakiCdOld.TextValue = dr.Item("DEST_CD").ToString()
            'END YANAI 要望番号909
            If ("00").Equals(.cmbTodokesaki.SelectedValue) = True Then
                '届先の場合
                .txtTodokesakiNm.TextValue = dr.Item("DEST_NM").ToString()
                .txtTodokeAdderss1.TextValue = dr.Item("AD_1").ToString()
                .txtTodokeAdderss2.TextValue = dr.Item("AD_2").ToString()
            Else
                '輸出情報の場合
                .txtTodokesakiNm.TextValue = dr.Item("DEST_NM2").ToString()
                .txtTodokeAdderss1.TextValue = dr.Item("DEST_AD_1").ToString()
                .txtTodokeAdderss2.TextValue = dr.Item("DEST_AD_2").ToString()
            End If
            .txtTodokeAdderss3.TextValue = dr.Item("DEST_AD_3").ToString()
            .txtTodokeTel.TextValue = dr.Item("DEST_TEL").ToString()

            '出荷総個数を求める
            Dim outMdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
            Dim max As Integer = outMdr.Length - 1
            Dim sumOutkaTtlNb As Decimal = 0
            For i As Integer = 0 To max
                sumOutkaTtlNb = sumOutkaTtlNb + Convert.ToDecimal(outMdr(i).Item("OUTKA_TTL_NB").ToString)
            Next
            outMdr(0).Item("SUM_OUTKA_TTL_NB") = Convert.ToString(sumOutkaTtlNb)

            'タブレット項目
            .cmbWHSagyoSintyoku.SelectedValue = dr.Item("WH_TAB_STATUS").ToString()
            If ("01").Equals(dr.Item("URGENT_YN").ToString()) = True Then
                .chkUrgent.Checked = True
            Else
                .chkUrgent.Checked = False
            End If
            If ("01").Equals(dr.Item("WH_TAB_YN").ToString()) = True Then
                .chkTablet.Checked = True
            Else
                .chkTablet.Checked = False
            End If
            .txtSijiRemark.TextValue = dr.Item("WH_SIJI_REMARK").ToString()
            '報告
            If ("01").Equals(dr.Item("WH_TAB_HOKOKU_YN").ToString()) = True Then
                .chkTabHokoku.Checked = True
            Else
                .chkTabHokoku.Checked = False
            End If
            '再指示不要
            If ("01").Equals(dr.Item("WH_TAB_NO_SIJI_FLG").ToString()) = True Then
                .chkNoSiji.Checked = True
            Else
                .chkNoSiji.Checked = False
            End If
            .txtHokoku.TextValue = dr.Item("WH_TAB_HOKOKU").ToString()

            'ADD Start 2019/06/18 004870【LMS】IntegWeb入力のCOA情報をLMS出荷検索画面_COA添付の有無の追加
            Dim outmDt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)

            If outmDt.Select(" COA_YN = '01' ").Length < 1 Then


            End If
            'ADD End   2019/06/18 004870【LMS】IntegWeb入力のCOA情報をLMS出荷検索画面_COA添付の有無の追加

        End With

    End Sub

    ''' <summary>
    ''' 運送（大）に値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoLControl(ByVal ds As DataSet)

        With Me._frm
            If (0).Equals(ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Count) = True Then
                Exit Sub
            End If

            Dim dr As DataRow = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(0)

            .txtHaisoRemark.TextValue = dr.Item("REMARK").ToString()
            .txtUnsoNo.TextValue = dr.Item("UNSO_NO_L").ToString()
            .cmbTehaiKbn.SelectedValue = dr.Item("UNSO_TEHAI_KB").ToString()
            .cmbTariffKbun.SelectedValue = dr.Item("TARIFF_BUNRUI_KB").ToString()
            .cmbSyaryoKbn.SelectedValue = dr.Item("VCLE_KB").ToString()
            .cmbBinKbn.SelectedValue = dr.Item("BIN_KB").ToString()
            .cmbMotoCyakuKbn.SelectedValue = dr.Item("PC_KB").ToString()
            .numJuryo.Value = Me._LMCconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
            .numKyori.Value = Me._LMCconG.FormatNumValue(dr.Item("KYORI").ToString())
            .txtUnsoCompanyCd.TextValue = dr.Item("UNSO_CD").ToString()
            .txtUnsoSitenCd.TextValue = dr.Item("UNSO_BR_CD").ToString()
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsoCompanyCdOld.TextValue = dr.Item("UNSO_CD").ToString()
            .txtUnsoSitenCdOld.TextValue = dr.Item("UNSO_BR_CD").ToString()
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .lblUnsoCompanyNm.TextValue = dr.Item("UNSOCO_NM").ToString()
            .lblUnsoSitenNm.TextValue = dr.Item("UNSOCO_BR_NM").ToString()
            .txtUnthinTariffCd.TextValue = dr.Item("SEIQ_TARIFF_CD").ToString()
            .lblUnthinTariffNm.TextValue = dr.Item("SEIQ_TARIFF_NM").ToString()
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayUnthinTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
            .lblPayUnthinTariffNm.TextValue = dr.Item("SHIHARAI_TARIFF_NM").ToString()
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .lblUnsoTareYn.TextValue = dr.Item("TARE_YN").ToString()
            .cmbUnsoKazeiKbn.SelectedValue = dr.Item("TAX_KB").ToString()
            .txtExtcTariffCd.TextValue = dr.Item("SEIQ_ETARIFF_CD").ToString()
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayExtcTariffCd.TextValue = dr.Item("SHIHARAI_ETARIFF_CD").ToString()
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .cmbYusoBrCd.SelectedValue = dr.Item("YUSO_BR_CD").ToString()
            '要望番号:1683 yamanaka 2013.03.04 START
            .lblShiharaiGroupNo.TextValue = dr.Item("SHIHARAI_GROUP_NO").ToString()
            .lblSeiqGroupNo.TextValue = dr.Item("SEIQ_GROUP_NO").ToString()
            '要望番号:1683 yamanaka 2013.03.04 END
            '要望番号:2408 2015.09.17 追加START
            .beforeAutoDenpKbn.SelectedValue = dr.Item("AUTO_DENP_KBN").ToString()
            .cmbAutoDenpKbn.SelectedValue = dr.Item("AUTO_DENP_KBN").ToString()
            .lblAutoDenpNo.TextValue = dr.Item("AUTO_DENP_NO").ToString()
            '要望番号:2408 2015.09.17 追加END
#If True Then       'ADD 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 
            Call SetKitakugaku(ds)
#End If
        End With

    End Sub

    ''' <summary>
    ''' 作業（大）に値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetSagyoLControl(ByVal ds As DataSet)

        With Me._frm
            If (0).Equals(ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Count) = True Then
                Exit Sub
            End If

            Dim tbl As DataTable = ds.Tables(LMC020C.TABLE_NM_SAGYO)
            Dim lngcnt As Integer = tbl.Rows.Count
            Dim dRow As DataRow = Nothing
            Dim outkaNo As String = ""
            Dim lCnt As Integer = 0

            For i As Integer = 1 To lngcnt
                dRow = tbl.Rows(i - 1)

                outkaNo = dRow.Item("INOUTKA_NO_LM").ToString()
                If ("000").Equals(outkaNo.Substring(outkaNo.Length - 3)) = True Then
                    lCnt = lCnt + 1
                    Select Case lCnt
                        Case 1
                            .txtSagyoL1.TextValue = dRow.Item("SAGYO_CD").ToString()
                            .lblSagyoL1.TextValue = dRow.Item("SAGYO_RYAK").ToString()
                            .txtSagyoRemarkL1.TextValue = dRow.Item("REMARK_SIJI").ToString()
                        Case 2
                            .txtSagyoL2.TextValue = dRow.Item("SAGYO_CD").ToString()
                            .lblSagyoL2.TextValue = dRow.Item("SAGYO_RYAK").ToString()
                            .txtSagyoRemarkL2.TextValue = dRow.Item("REMARK_SIJI").ToString()
                        Case 3
                            .txtSagyoL3.TextValue = dRow.Item("SAGYO_CD").ToString()
                            .lblSagyoL3.TextValue = dRow.Item("SAGYO_RYAK").ToString()
                            .txtSagyoRemarkL3.TextValue = dRow.Item("REMARK_SIJI").ToString()
                        Case 4
                            .txtSagyoL4.TextValue = dRow.Item("SAGYO_CD").ToString()
                            .lblSagyoL4.TextValue = dRow.Item("SAGYO_RYAK").ToString()
                            .txtSagyoRemarkL4.TextValue = dRow.Item("REMARK_SIJI").ToString()
                        Case 5
                            .txtSagyoL5.TextValue = dRow.Item("SAGYO_CD").ToString()
                            .lblSagyoL5.TextValue = dRow.Item("SAGYO_RYAK").ToString()
                            .txtSagyoRemarkL5.TextValue = dRow.Item("REMARK_SIJI").ToString()
                    End Select
                End If

            Next

        End With

    End Sub


#Region "2014/01/22 輸出情報追加"


    ''' <summary>
    ''' 輸出情報に値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetExportLControl(ByVal ds As DataSet)

        With Me._frm
            If (0).Equals(ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows.Count) = True Then
                Exit Sub
            End If

            Dim dRow As DataRow = ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows(0)

            .txtShipNm.TextValue = dRow.Item("SHIP_NM").ToString()
            .txtDestination.TextValue = dRow.Item("DESTINATION").ToString()
            .txtBookingNo.TextValue = dRow.Item("BOOKING_NO").ToString()
            .txtVoyageNo.TextValue = dRow.Item("VOYAGE_NO").ToString()
            .txtShipperCd.TextValue = dRow.Item("SHIPPER_CD").ToString()
            .lblShipperNm.TextValue = dRow.Item("SHIPPER_NM").ToString()
            .imdContLoadingDate.TextValue = dRow.Item("CONT_LOADING_DATE").ToString()
            .imdStorageTestDate.TextValue = dRow.Item("STORAGE_TEST_DATE").ToString()
            .txtStorageTestTime.TextValue = dRow.Item("STORAGE_TEST_TIME").ToString()
            .imdDepartureDate.TextValue = dRow.Item("DEPARTURE_DATE").ToString()
            .txtContainerNo.TextValue = dRow.Item("CONTAINER_NO").ToString()
            .txtContainerNm.TextValue = dRow.Item("CONTAINER_NM").ToString()
            .cmbContainerSize.SelectedValue = dRow.Item("CONTAINER_SIZE").ToString()

        End With

    End Sub


#End Region

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' シッピングマーク(HED)の情報を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetMarkHedControl(ByVal ds As DataSet, ByVal activeRow As Integer, Optional ByVal eventShubetsu As LMC020C.EventShubetsu = LMC020C.EventShubetsu.OPTIONAL_MODE)

        With Me._frm

            Dim drM As DataRow() = Nothing
            Dim max As Integer = 8
            '荷主明細
            drM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                              "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                             , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                             , "SUB_KB = '00'"))
            '列数設定
            If drM.Length > 0 Then
                max = Convert.ToInt32(drM(0).Item("SET_NAIYO").ToString())
            End If

            If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) > max Then
                .lblMarkInfo1.Visible = False
                .txtMarkInfo1.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) > max Then
                .lblMarkInfo2.Visible = False
                .txtMarkInfo2.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) > max Then
                .lblMarkInfo3.Visible = False
                .txtMarkInfo3.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) > max Then
                .lblMarkInfo4.Visible = False
                .txtMarkInfo4.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) > max Then
                .lblMarkInfo5.Visible = False
                .txtMarkInfo5.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) > max Then
                .lblMarkInfo6.Visible = False
                .txtMarkInfo6.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) > max Then
                .lblMarkInfo7.Visible = False
                .txtMarkInfo7.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) > max Then
                .lblMarkInfo8.Visible = False
                .txtMarkInfo8.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) > max Then
                .lblMarkInfo9.Visible = False
                .txtMarkInfo9.Visible = False
            End If
            If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) > max Then
                .lblMarkInfo10.Visible = False
                .txtMarkInfo10.Visible = False
            End If

            Dim sprCnt As Integer = .sprSyukkaM.ActiveSheet.RowCount - 1

            If ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Count = 0 OrElse (eventShubetsu = LMC020C.EventShubetsu.INS_M) OrElse (eventShubetsu = LMC020C.EventShubetsu.DOUBLE_CLICK) Then
                Me.ClearControlMarkHed()

                '2015.10.07 シッピングマーク対応START
                If eventShubetsu = LMC020C.EventShubetsu.INS_M Then
                    Dim drDefShip As DataRow() = Nothing
                    '荷主明細
                    drDefShip = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                      "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                     , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                                     , "SUB_KB = '0E'"))

                    'マークDTL 荷主情報の初期値設定(荷主明細Ｍ SUB_KB = '55')
                    If drDefShip.Length > 0 Then
                        Dim shipFormatkey As String() = drDefShip(0).Item("SET_NAIYO").ToString().Split(","c)
                        If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo1.TextValue() = shipFormatkey(0)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo2.TextValue() = shipFormatkey(1)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo3.TextValue() = shipFormatkey(2)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo4.TextValue() = shipFormatkey(3)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo5.TextValue() = shipFormatkey(4)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo6.TextValue() = shipFormatkey(5)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo7.TextValue() = shipFormatkey(6)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo8.TextValue() = shipFormatkey(7)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= shipFormatkey.Length Then
                            .txtMarkInfo9.TextValue() = shipFormatkey(8)
                        End If
                        If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max AndAlso Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= shipFormatkey.Length Then
                            .txtMarkInfo10.TextValue() = shipFormatkey(9)
                        End If
                    End If

                    'マークHED 出荷管理番号(中番)のセット
                    .txtOutkaNoM.TextValue() = .lblSyukkaMNo.TextValue


                    Exit Sub
                    '2015.10.07 シッピングマーク対応END

                End If

                If eventShubetsu = LMC020C.EventShubetsu.LEAVE_CELL OrElse eventShubetsu = LMC020C.EventShubetsu.DOUBLE_CLICK Then
                    .txtOutkaNoM.TextValue = _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo))
                ElseIf eventShubetsu = LMC020C.EventShubetsu.SINKI Then
                    Exit Sub
                    '20150825 引当→在庫なし→閉じる→エラー発生の対応 adachi
                ElseIf sprCnt = -1 Then
                    Exit Sub

                Else
                    .txtOutkaNoM.TextValue = _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(sprCnt, sprSyukkaM.KANRI_NO.ColNo))
                    Exit Sub
                End If
            End If

            Dim dr As DataRow = Nothing
            Dim drS As DataRow() = Nothing
            Dim drDtl As DataRow() = Nothing
            Dim seqNo As String = String.Empty

            If activeRow = -1 Then

                activeRow = _frm.sprSyukkaM.Sheets(0).ActiveRow.Index

                dr = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows(0)
                .txtOutkaNoM.TextValue = dr.Item("OUTKA_NO_M").ToString()
                .numCaseNoFrom.Value = Convert.ToInt32(dr.Item("CASE_NO_FROM").ToString())
                .numCaseNoTo.Value = Convert.ToInt32(dr.Item("CASE_NO_TO").ToString())

                If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Count = 0 Then
                    Exit Sub
                End If

                If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo1.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo2.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo3.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo4.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo5.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo6.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo7.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo8.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                    seqNo = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo9.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
                If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                    seqNo = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'")).Length > 0 Then
                        .txtMarkInfo10.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                      , "MARK_EDA = '", seqNo, "' AND " _
                                                                                      , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                    End If
                End If
            Else
                drS = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                , "SYS_DEL_FLG = '0'"))

                If drS.Length = 0 Then
                    .txtOutkaNoM.TextValue = _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)).ToString()
                    .numCaseNoFrom.Value = 0
                    .numCaseNoTo.Value = 0
                    .txtMarkInfo1.TextValue = String.Empty
                    .txtMarkInfo2.TextValue = String.Empty
                    .txtMarkInfo3.TextValue = String.Empty
                    .txtMarkInfo4.TextValue = String.Empty
                    .txtMarkInfo5.TextValue = String.Empty
                    .txtMarkInfo6.TextValue = String.Empty
                    .txtMarkInfo7.TextValue = String.Empty
                    .txtMarkInfo8.TextValue = String.Empty
                    .txtMarkInfo9.TextValue = String.Empty
                    .txtMarkInfo10.TextValue = String.Empty
                Else

                    .txtOutkaNoM.TextValue = drS(0).Item("OUTKA_NO_M").ToString()
                    .numCaseNoFrom.Value = Convert.ToInt32(drS(0).Item("CASE_NO_FROM").ToString())
                    .numCaseNoTo.Value = Convert.ToInt32(drS(0).Item("CASE_NO_TO").ToString())

                    drS = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                        , "SYS_DEL_FLG = '0'"))

                    If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Count = 0 Then
                        Exit Sub
                    End If

                    If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo1.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo2.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo3.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo4.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo5.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo6.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo7.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo8.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                        seqNo = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo9.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If
                    If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                        seqNo = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                        If ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'")).Length > 0 Then
                            .txtMarkInfo10.TextValue = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                          , "MARK_EDA = '", seqNo, "' AND " _
                                                                                          , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO").ToString()
                        End If
                    End If

                End If

            End If

        End With

    End Sub
    '2015.07.08 協立化学　シッピング対応 追加END

    ''' <summary>
    ''' 詳細にデータを追加(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddOutkaM(ByVal dt As DataTable)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dtOut As New DataSet
        Dim dRow As DataRow

        '値設定
        dRow = dt.Rows(0)

        '区分マスタキャッシュ取得（入目の単位名称を取得）
        Dim irimeKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'I001' AND ", _
                                                                                  "KBN_CD ='", dRow.Item("STD_IRIME_UT").ToString, "'"))

        With Me._frm

            '出荷(中)の各項目に設定
            .lblSyukkaMNo.TextValue = String.Empty
            .numPrintSort.TextValue = "99"

            '2017/09/25 修正 李↓
            .lblHikiate.TextValue() = lgm.Selector({"未", "Yet", "미(未)", "中国語"})
            '2017/09/25 修正 李↑

            .txtGoodsCdCust.TextValue = dRow.Item("GOODS_CD_CUST").ToString
            .lblGoodsNm.TextValue = dRow.Item("NM_1").ToString
            .txtLotNo.TextValue = dRow.Item("LOT_NO").ToString()
            .txtOrderNo.TextValue = String.Empty
            .txtRsvNo.TextValue = String.Empty
            .txtSerialNo.TextValue = String.Empty
            .txtCyumonNo.TextValue = String.Empty
            .optTempN.Checked = True
            .optCnt.Checked = True
            .numIrime.Value = Me._LMCconG.FormatNumValue(dRow.Item("IRIME").ToString)

#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
            .lblIrimeUT.TextValue = irimeKbn(0).Item("KBN_NM1").ToString
            .lblIrimeUThide.TextValue = dRow.Item("STD_IRIME_UT").ToString
#Else
            .lblIrimeUT.KbnValue = dRow.Item("STD_IRIME_UT").ToString
#End If
            .numPkgCnt.TextValue = String.Empty
            .cmbUnsoOndo.SelectedValue = dRow.Item("UNSO_ONDO_KB").ToString
            .cmbTakkyuSize.SelectedValue = dRow.Item("SIZE_KB").ToString
            .numKonsu.Value = 0

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblKonsuUT.TextValue = dRow.Item("NB_UT_NM").ToString
#Else
            .lblKonsuUT.KbnValue = dRow.Item("NB_UT").ToString
#End If
            .numIrisu.Value = Me._LMCconG.FormatNumValue(dRow.Item("PKG_NB").ToString)
            .numSouKosu.Value = 0
            .numHasu.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblHasuUT.TextValue = dRow.Item("NB_UT_NM").ToString
#Else
            .lblHasuUT.KbnValue = dRow.Item("NB_UT").ToString
#End If
            .numHikiateKosuSumi.Value = 0
            .numHikiateKosuZan.Value = 0
            .numSouSuryo.Value = 0

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblSuryouUT.TextValue = dRow.Item("IRIME_UT_NM").ToString
#Else
            .lblSuryouUT.KbnValue = dRow.Item("IRIME_UT").ToString
#End If
            .numHikiateSuryoSumi.Value = 0
            .numHikiateSuryoZan.Value = 0
            .txtGoodsRemark.TextValue = dRow.Item("OUTKA_ATT").ToString

            .lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS").ToString
            .lblGoodsCdNrsFrom.TextValue = dRow.Item("GOODS_CD_NRS_FROM").ToString
            .lblTareYn.TextValue = dRow.Item("TARE_YN").ToString
            .lblStdIrimeNb.TextValue = dRow.Item("STD_IRIME_NB").ToString
            .lblStdWtKgs.TextValue = dRow.Item("STD_WT_KGS").ToString
            .lblRemark.TextValue = dRow.Item("REMARK").ToString
            .lblRemarkOut.TextValue = dRow.Item("REMARK_OUT").ToString
            .lblTaxKb.TextValue = dRow.Item("TAX_KB").ToString
            .lblHikiateAlertYn.TextValue = dRow.Item("HIKIATE_ALERT_YN").ToString
            .lblRecNo.TextValue = .numRecMCnt.TextValue
            .lblCustCdS.TextValue = dRow.Item("CUST_CD_S").ToString
            .lblCustCdSS.TextValue = dRow.Item("CUST_CD_SS").ToString
            'START YANAI 要望番号499
            .lblCustCdL.TextValue = dRow.Item("CUST_CD_L_GOODS").ToString
            .lblCustCdM.TextValue = dRow.Item("CUST_CD_M_GOODS").ToString
            'END YANAI 要望番号499

            If ("00").Equals(dRow.Item("COA_YN").ToString) = True Then
                .optTempN.Checked = True
            ElseIf ("01").Equals(dRow.Item("COA_YN").ToString) = True Then
                .optTempY.Checked = True
            End If

            Dim sagyoDr As DataRow() = Nothing

            '作業1
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString) = False Then
                'START YANAI 要望番号376
                'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString, "' AND " _
                '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                'END YANAI 要望番号376
                If 0 < sagyoDr.Length Then
                    .txtSagyoM1.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString
                    .lblSagyoM1.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM1.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業2
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString) = False Then
                'START YANAI 要望番号376
                'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString, "' AND " _
                '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                'END YANAI 要望番号376
                If 0 < sagyoDr.Length Then
                    .txtSagyoM2.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString
                    .lblSagyoM2.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM2.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業3
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString) = False Then
                'START YANAI 要望番号376
                'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString, "' AND " _
                '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                'END YANAI 要望番号376
                If 0 < sagyoDr.Length Then
                    .txtSagyoM3.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString
                    .lblSagyoM3.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM3.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業4
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString) = False Then
                'START YANAI 要望番号376
                'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString, "' AND " _
                '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                'END YANAI 要望番号376
                If 0 < sagyoDr.Length Then
                    .txtSagyoM4.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString
                    .lblSagyoM4.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM4.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業5
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString) = False Then
                'START YANAI 要望番号376
                'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                '                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString, "' AND " _
                '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                'END YANAI 要望番号376
                If 0 < sagyoDr.Length Then
                    .txtSagyoM5.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString
                    .lblSagyoM5.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM5.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            Dim destGoodsDr As DataRow() = Nothing
            If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = False Then
                destGoodsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DESTGOODS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                   , "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND " _
                                                                   , "CD = '", .txtTodokesakiCd.TextValue, "' AND " _
                                                                   , "GOODS_CD_NRS = '", dRow.Item("GOODS_CD_NRS").ToString, "'"))
                If 0 < destGoodsDr.Length Then
                    '届先作業1
                    .txtDestSagyoM1.TextValue = destGoodsDr(0).Item("SAGYO_KB_1").ToString
                    'START YANAI 要望番号376
                    'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                    '                                                                   , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_1").ToString, "' AND " _
                    '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                    sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_1").ToString, "' AND " _
                                                                                       , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                    'END YANAI 要望番号376
                    If 0 < sagyoDr.Length Then
                        .lblDestSagyoM1.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                        .txtDestSagyoRemarkM1.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                    End If

                    '届先作業2
                    .txtDestSagyoM2.TextValue = destGoodsDr(0).Item("SAGYO_KB_2").ToString
                    'START YANAI 要望番号376
                    'sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                    '                                                                   , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_2").ToString, "' AND " _
                    '                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "'"))
                    sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_2").ToString, "' AND " _
                                                                                       , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                    'END YANAI 要望番号376
                    If 0 < sagyoDr.Length Then
                        .lblDestSagyoM2.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                        .txtDestSagyoRemarkM2.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                    End If

                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' 詳細のロット№設定(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetLotNo(ByVal dt As DataTable)

        Dim dtOut As New DataSet
        Dim dRow As DataRow

        '値設定
        dRow = dt.Rows(0)

        With Me._frm

            'ロット№設定
            .txtLotNo.TextValue = dRow.Item("LOT_NO").ToString()

        End With

    End Sub


#If True Then   'add 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間)
    ''' <summary>
    ''' 詳細のロット№設定(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetKitakugaku(ByVal ds As DataSet)

        Dim ditakugaku As Decimal = 0

        For i As Integer = 0 To ds.Tables(LMC020C.UNSO_KITAKUGAKU).Rows.Count - 1
            ditakugaku = ditakugaku + (CDec(ds.Tables(LMC020C.UNSO_KITAKUGAKU).Rows(i).Item("UNSO_WT").ToString) * CDec(ds.Tables(LMC020C.UNSO_KITAKUGAKU).Rows(i).Item("KITAKU_GOODS_UP").ToString))
        Next

        With Me._frm

            '寄託価格合計
            .numKitakuKakaku.Value = ditakugaku

        End With

    End Sub

#End If

    'START YANAI 要望番号1019
    '''' <summary>
    '''' 引当後、詳細にデータを表示(出荷(中))
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetOutkaMhikiate(ByVal dt As DataTable)
    ''' <summary>
    ''' 引当後、詳細にデータを表示(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetOutkaMhikiate(ByVal dt As DataTable, ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal taninusiFlg As Boolean)
        'END YANAI 要望番号1019

        Dim dtOut As New DataSet
        Dim dRow As DataRow

        '値設定
        dRow = dt.Rows(0)

        With Me._frm

            '出荷(中)の各項目に設定
            If ("-1").Equals(dRow.Item("KONSU").ToString) = False Then

                .txtGoodsCdCust.TextValue = dRow.Item("GOODS_CD_CUST").ToString

                .txtLotNo.TextValue = dRow.Item("LOT_NO").ToString
                .txtRsvNo.TextValue = dRow.Item("RSV_NO_L").ToString
                .txtSerialNo.TextValue = dRow.Item("SERIAL_NO_L").ToString
                'START YANAI 20111027 入り目対応
                '.numIrime.Value = Me._LMCconG.FormatNumValue(dRow.Item("IRIME_L").ToString)
                .numIrime.Value = Me._LMCconG.FormatNumValue(dRow.Item("IRIME").ToString)
                'END YANAI 20111027 入り目対応
                .numKonsu.Value = Me._LMCconG.FormatNumValue(dRow.Item("KONSU").ToString)
                .numSouKosu.Value = Me._LMCconG.FormatNumValue(dRow.Item("KOSU").ToString)
                .numHasu.Value = Me._LMCconG.FormatNumValue(dRow.Item("HASU").ToString)
                .numSouSuryo.Value = Me._LMCconG.FormatNumValue(dRow.Item("SURYO").ToString)

                .numIrisu.Value = Me._LMCconG.FormatNumValue(dRow.Item("PKG_NB").ToString)


                If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                    .optCnt.Checked = True
                ElseIf ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                    .optAmt.Checked = True
                ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                    .optKowake.Checked = True
                ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                    .optSample.Checked = True
                End If

                If ("00").Equals(dRow.Item("COA_YN").ToString) = True Then
                    .optTempN.Checked = True
                ElseIf ("01").Equals(dRow.Item("COA_YN").ToString) = True Then
                    .optTempY.Checked = True
                End If

                '追加開始 --- 2014.07.24 kikuchi
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                                     "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, _
                                                                                                     "' AND SUB_KB = '78'"))
                '荷主がロームの場合、下記処理を実行する
                If 0 < custDetailsDr.Length Then

                    If .cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_ARI) Then
                        .optTempY.Checked = True
                        .optTempN.Checked = False

                    ElseIf .cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_NASI) Then
                        .optTempY.Checked = False
                        .optTempN.Checked = True
                    End If

                End If
                ''追加終了 ---

                'START YANAI 20120321 緊急
                'START YANAI 要望番号1019
                '.lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS").ToString
                '.lblGoodsCdNrsFrom.TextValue = dRow.Item("GOODS_CD_NRS_FROM").ToString
                If (eventShubetsu).Equals(LMC020C.EventShubetsu.HIKIATE) = True AndAlso taninusiFlg = True Then
                    'この条件の時は、最初は他荷主ではない状態で引当したが、その後、他荷主引当をした場合。
                    .lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS_FROM").ToString
                    .lblGoodsCdNrsFrom.TextValue = dRow.Item("GOODS_CD_NRS").ToString
                Else
                    .lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS").ToString
                    .lblGoodsCdNrsFrom.TextValue = dRow.Item("GOODS_CD_NRS_FROM").ToString
                End If
                'END YANAI 要望番号1019
                'END YANAI 20120321 緊急

            End If

            '.cmbSoko.ReadOnly = True
        End With

    End Sub

    ''' <summary>
    ''' 在庫引当後の端数作業を設定する
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Friend Sub SetHasuSagyoCd(ByVal dt As DataTable, ByVal eventShubetsu As LMC020C.EventShubetsu)

        Dim dr As DataRow = dt.Rows(0)

        '端数を取得
        Dim hasu As Integer = Integer.Parse(dr.Item("HASU").ToString)
        '端数が1以下であれば処理終了
        If hasu = 0 Then Exit Sub

        '端数出荷時作業区分を取得
        Dim hasuSagyoDr() As DataRow = dt.Select("OUTKA_HASU_SAGYO_KB_1 <> '' or OUTKA_HASU_SAGYO_KB_2 <> '' or OUTKA_HASU_SAGYO_KB_3 <> ''")

        '荷主マスタの用途区分「端数出荷時 付帯作業自動設定 出荷単位」を取得
        Dim custSagyoDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS)
        Dim custSagyoDr As DataRow() = custSagyoDt.Select(
                String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' AND " _
                , "CUST_CD = '", dr.Item("CUST_CD_L").ToString(), "' AND " _
                , "SUB_KB = 'B3' AND " _
                , "SET_NAIYO = '1'"))

        If custSagyoDr.Length = 0 OrElse custSagyoDr Is Nothing Then

            '端数出荷作業区分が設定されていなければ処理終了
            If hasuSagyoDr.Length = 0 OrElse hasuSagyoDr Is Nothing Then Exit Sub

        Else

            '端数出荷時作業区分を設定
            hasuSagyoDr = dt.Select()
            hasuSagyoDr(0).Item("OUTKA_HASU_SAGYO_KB_1") = custSagyoDr(0).Item("SET_NAIYO_2")
            hasuSagyoDr(0).Item("OUTKA_HASU_SAGYO_KB_2") = String.Empty
            hasuSagyoDr(0).Item("OUTKA_HASU_SAGYO_KB_3") = String.Empty

        End If

        '作業テキストボックスに設定する端数作業を取得
        Dim hasuSagyoList As List(Of String) = New List(Of String)
        Dim hasuSagyoCd As String = Nothing
        For i As Integer = 0 To 2
            hasuSagyoCd = hasuSagyoDr(0).Item("OUTKA_HASU_SAGYO_KB_" + (i + 1).ToString()).ToString()
            If String.IsNullOrEmpty(hasuSagyoCd) Then
                Continue For
            End If

            hasuSagyoList.Add(hasuSagyoCd)

        Next

        '作業用テキストボックスを取得
        Dim txtSagyoCd() As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox() _
                                                                        {Me._frm.txtSagyoM1, Me._frm.txtSagyoM2, Me._frm.txtSagyoM3, Me._frm.txtSagyoM4, Me._frm.txtSagyoM5}

        '作業コード用テキストボックスで値が設定されていないものを取得
        Dim txtEmptySagyoCd() As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = txtSagyoCd.Where(Function(x) x.TextValue = "").ToArray()

        '作業名用ラベルを取得
        Dim lblSagyoNm() As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox() _
                                                                        {Me._frm.lblSagyoM1, Me._frm.lblSagyoM2, Me._frm.lblSagyoM3, Me._frm.lblSagyoM4, Me._frm.lblSagyoM5}

        '作業名用ラベルで値が設定されていないものを取得
        Dim lblEmptySagyoNm() As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox = lblSagyoNm.Where(Function(x) x.TextValue = "").ToArray()

        '端数出荷時作業区分を空の作業用テキストボックスに設定
        For i As Integer = 0 To hasuSagyoList.Count - 1
            '作業コードを取得
            Dim sagyoCd As String = hasuSagyoList(i)
            If sagyoCd.Length = 0 Then Continue For

            '同一作業コードが既に付帯作業（中）に登録されている場合は、２重登録しない。
            Dim existSagyoCdCnt As Integer = txtSagyoCd.Where(Function(x) x.TextValue = sagyoCd).Count()
            If existSagyoCdCnt > 0 Then
                Continue For
            End If

            '作業コードをテキストボックスに設定
            txtEmptySagyoCd(i).TextValue = sagyoCd

            Dim nrsBrCd As String = hasuSagyoDr(0)("NRS_BR_CD").ToString()
            Dim custCdL As String = hasuSagyoDr(0)("CUST_CD_L").ToString()

            '作業コードに紐づく作業名を取得
            Dim sagyoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                   , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                   , "(CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')"))

            If sagyoDr.Length > 0 Then

                '作業名をラベルに設定
                lblEmptySagyoNm(i).TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString

            End If

        Next

    End Sub

    ''' <summary>
    ''' 出荷(中)SPREADダブルクリック時の出荷(中)詳細表示処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetOutkaM(ByVal activeRow As Integer, ByVal ds As DataSet) As Boolean

        With Me._frm

            Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select _
                (String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                , "SYS_DEL_FLG = '0'"))
            If dr.Length = 0 Then
                Return True
            End If

            '出荷(中)の各項目に設定
            .lblSyukkaMNo.TextValue = dr(0)("OUTKA_NO_M").ToString
            .numPrintSort.TextValue = dr(0)("PRINT_SORT").ToString
            .lblHikiate.TextValue = dr(0)("HIKIATE").ToString
            .txtGoodsCdCust.TextValue = dr(0)("GOODS_CD_CUST").ToString
            .lblGoodsNm.TextValue = dr(0)("GOODS_NM").ToString
            .txtLotNo.TextValue = dr(0)("LOT_NO").ToString
            .txtOrderNo.TextValue = dr(0)("CUST_ORD_NO_DTL").ToString
            .txtRsvNo.TextValue = dr(0)("RSV_NO").ToString
            .txtSerialNo.TextValue = dr(0)("SERIAL_NO").ToString
            .txtCyumonNo.TextValue = dr(0)("BUYER_ORD_NO_DTL").ToString

            If ("01").Equals(dr(0)("COA_YN").ToString) = True Then
                .optTempY.Checked = True
            Else
                .optTempN.Checked = True
            End If

            If ("01").Equals(dr(0)("ALCTD_KB").ToString) = True Then
                .optCnt.Checked = True
            ElseIf ("02").Equals(dr(0)("ALCTD_KB").ToString) = True Then
                .optAmt.Checked = True
            ElseIf ("03").Equals(dr(0)("ALCTD_KB").ToString) = True Then
                .optKowake.Checked = True
            ElseIf ("04").Equals(dr(0)("ALCTD_KB").ToString) = True Then
                .optSample.Checked = True
            End If

            .numIrime.Value = Me._LMCconG.FormatNumValue(dr(0)("IRIME").ToString)

#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
            .lblIrimeUT.TextValue = dr(0).Item("QT_UT_NM").ToString
            .lblIrimeUThide.TextValue = dr(0)("IRIME_UT").ToString
#Else
            .lblIrimeUT.KbnValue = dr(0).Item("IRIME_UT").ToString
#End If
            .numPkgCnt.Value = Me._LMCconG.FormatNumValue(dr(0)("OUTKA_M_PKG_NB").ToString)
            .cmbUnsoOndo.SelectedValue = dr(0)("UNSO_ONDO_KB").ToString
            .cmbTakkyuSize.SelectedValue = dr(0)("SIZE_KB").ToString
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblKonsuUT.TextValue = dr(0).Item("NB_UT_NM").ToString
#Else
            .lblKonsuUT.KbnValue = dr(0).Item("NB_UT").ToString
#End If
            .numIrisu.Value = Me._LMCconG.FormatNumValue(dr(0)("PKG_NB").ToString)
            .numSouKosu.Value = Me._LMCconG.FormatNumValue(dr(0)("OUTKA_TTL_NB").ToString)
            .numHasu.Value = Me._LMCconG.FormatNumValue(dr(0)("OUTKA_HASU").ToString)

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblHasuUT.TextValue = dr(0).Item("NB_UT_NM").ToString
#Else
            .lblHasuUT.KbnValue = dr(0).Item("NB_UT").ToString
#End If



            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                     "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, _
                                                                                     "' AND SUB_KB = '80'"))

            If custDetailsDr.Length > 0 AndAlso Convert.ToDecimal(Me._LMCconG.FormatNumValue(dr(0).Item("EDI_OUTKA_TTL_NB").ToString())) > 0 Then
                .numHikiateSuryoZan.Value = Convert.ToDecimal(Me._LMCconG.FormatNumValue(dr(0).Item("BACKLOG_NB").ToString)) * Convert.ToDecimal(dr(0).Item("STD_IRIME_NB").ToString)
                .numSouSuryo.Value = Convert.ToDecimal(Me._LMCconG.FormatNumValue(dr(0)("OUTKA_TTL_NB").ToString)) * Convert.ToDecimal(dr(0).Item("STD_IRIME_NB").ToString)
                .lblEdiOutkaTtlQt.Value = Convert.ToDecimal(Me._LMCconG.FormatNumValue(dr(0)("OUTKA_TTL_NB").ToString)) * Convert.ToDecimal(dr(0).Item("STD_IRIME_NB").ToString)
                If Convert.ToDecimal(.numSouSuryo.Value) > 0 Then
                    .numKonsu.Value = Convert.ToDecimal( _
                            ((Convert.ToDecimal(.numSouKosu.Value) - _
                            Convert.ToDecimal(.numHasu.Value)) / _
                            Convert.ToDecimal(.numIrisu.Value)))
                Else
                    .numKonsu.Value = 0
                End If
            Else

                .numKonsu.Value = Convert.ToDecimal( _
                        ((Convert.ToDecimal(.numSouKosu.Value) - _
                          Convert.ToDecimal(.numHasu.Value)) / _
                         Convert.ToDecimal(.numIrisu.Value)))
                .numHikiateSuryoZan.Value = Me._LMCconG.FormatNumValue(dr(0).Item("BACKLOG_QT").ToString)
                .numSouSuryo.Value = Me._LMCconG.FormatNumValue(dr(0)("OUTKA_TTL_QT").ToString)
                .lblEdiOutkaTtlQt.Value = Me._LMCconG.FormatNumValue(dr(0).Item("EDI_OUTKA_TTL_QT").ToString)
            End If
            .numHikiateKosuSumi.Value = Me._LMCconG.FormatNumValue(dr(0)("ALCTD_NB").ToString)
            .numHikiateKosuZan.Value = Me._LMCconG.FormatNumValue(dr(0).Item("BACKLOG_NB").ToString)

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblSuryouUT.TextValue = dr(0).Item("QT_UT_NM").ToString
#Else
            .lblSuryouUT.KbnValue = dr(0).Item("IRIME_UT").ToString
#End If
            .numHikiateSuryoSumi.Value = Me._LMCconG.FormatNumValue(dr(0)("ALCTD_QT").ToString)

            .txtGoodsRemark.TextValue = dr(0)("REMARK").ToString

            .lblGoodsCdNrs.TextValue = dr(0)("GOODS_CD_NRS").ToString
            .lblGoodsCdNrsFrom.TextValue = dr(0)("GOODS_CD_NRS_FROM").ToString
            .lblTareYn.TextValue = dr(0).Item("TARE_YN").ToString
            .lblStdIrimeNb.TextValue = dr(0).Item("STD_IRIME_NB").ToString
            .lblStdWtKgs.TextValue = dr(0).Item("STD_WT_KGS").ToString
            .lblTaxKb.TextValue = dr(0).Item("TAX_KB").ToString
            .lblHikiateAlertYn.TextValue = dr(0).Item("HIKIATE_ALERT_YN").ToString
            .lblRecNo.TextValue = _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.REC_NO.ColNo))
            .lblCustCdS.TextValue = dr(0)("CUST_CD_S").ToString
            .lblCustCdSS.TextValue = dr(0)("CUST_CD_SS").ToString
            'START YANAI 要望番号499
            .lblCustCdL.TextValue = dr(0)("CUST_CD_L_GOODS").ToString
            .lblCustCdM.TextValue = dr(0)("CUST_CD_M_GOODS").ToString
            'END YANAI 要望番号499

            'START YANAI 要望番号1959
            .lblEdiOutkaTtlNb.Value = Me._LMCconG.FormatNumValue(dr(0).Item("EDI_OUTKA_TTL_NB").ToString)

            'END YANAI 要望番号1959

            'START YANAI 要望番号681
            .lblTaniKowake.TextValue = _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.SYUKKA_TANI_KOWAKE.ColNo))
            'END YANAI 要望番号681

            '要望番号:1869（小分けデータが編集出来なくなる）対応　 2013/02/26 本明Start
            If .lblTaniKowake.TextValue = String.Empty Then
                '引当単位が個数 かつ 入目＞出荷総数量の場合、lblTaniKowakeに"01"を設定(小分けの全量引当とする)

                '引当単位が個数の場合個数が0なら小分け全量ではない(分譲物がない) 引当単位が数量の場合総数量が0の場合も小分け全量ではない
                If .optCnt.Checked = True AndAlso Convert.ToDouble(.numIrime.Value) > Convert.ToDouble(.numSouSuryo.Value) Then

                    If (.optCnt.Checked = True AndAlso Convert.ToDouble(.numSouSuryo.Value) <> 0) _
                             OrElse _
                           (.optAmt.Checked = True AndAlso Convert.ToDouble(.numSouSuryo.Value) <> 0) Then
                        .lblTaniKowake.TextValue = "01"

                    End If
                End If

            End If
            '要望番号:1869（小分けデータが編集出来なくなる）対応　 2013/02/26 本明End


        End With

        Return True

    End Function

    ''' <summary>
    ''' 引当済個数・引当残個数・引当済数量・引当残数量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetHikiKosuSuryo(ByVal ds As DataSet) As Boolean

        With Me._frm.sprDtl.ActiveSheet

            Dim kosu As Decimal = 0
            Dim suryo As Decimal = 0
            Dim kanoKosu As Decimal = 0
            Dim kanoSuryo As Decimal = 0
            Dim kosuG As Decimal = 0
            Dim suryoG As Decimal = 0
            Dim kanoKosuG As Decimal = 0
            Dim kanoSuryoG As Decimal = 0

            Dim max As Integer = .Rows.Count - 1

            '合計を求める
            For i As Integer = 0 To max
                kosuG = kosuG + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                suryoG = suryoG + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_QT.ColNo)))
                kanoKosu = kanoKosu + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo)))
                kanoSuryo = kanoSuryo + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo)))
                If ("01").Equals(_LMCconG.GetCellValue(.Cells(i, sprDtl.SMPL_FLAG.ColNo))) = True Then
                    '小分けの場合
                    kosu = kosu
                    suryo = suryo + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.IRIME.ColNo)))

                Else
                    kosu = kosu + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                    suryo = suryo + Convert.ToDecimal(_LMCconG.GetCellValue(.Cells(i, sprDtl.ALCTD_QT.ColNo)))

                End If

            Next

            '合計値のチェック
            If Me._V.IsHaniCheck(kosu, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {String.Concat(_frm.lblTitleHikiateKosuSumi.TextValue, _frm.lblKosu.TextValue), Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                Return False
            End If

            If Me._V.IsHaniCheck(Convert.ToDecimal(Convert.ToDecimal(Me._frm.numSouKosu.Value) - kosuG), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {String.Concat(_frm.lblTitleHikiateKosuZan.TextValue, _frm.lblKosu.TextValue), Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                Return False
            End If

            If Me._V.IsHaniCheck(suryo, LMC020C.SURYO_MIN_NUM, Convert.ToDecimal(LMC020C.SURYO_MAX_NUM)) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {String.Concat(_frm.lblTitleHikiateKosuSumi.TextValue, _frm.lblTitleSouSuryo.TextValue), Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                Return False
            End If

            If Me._V.IsHaniCheck(Convert.ToDecimal(Convert.ToDecimal(Me._frm.numSouSuryo.Value) - suryoG), LMC020C.SURYO_MIN_NUM, Convert.ToDecimal(LMC020C.SURYO_MAX_NUM)) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {String.Concat(_frm.lblTitleHikiateKosuZan.TextValue, _frm.lblTitleSouSuryo.TextValue), Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                Return False
            End If

            Me._frm.numHikiateKosuSumi.Value = kosuG
            Me._frm.numHikiateSuryoSumi.Value = suryoG
            If Me._frm.optSample.Checked = True Then
                Me._frm.numHikiateKosuZan.Value = 0
                'START YANAI 20110906 サンプル対応
                'Me._frm.numHikiateSuryoZan.Value = 0
                Me._frm.numHikiateSuryoZan.Value = Convert.ToDecimal(Me._frm.numSouSuryo.Value) - suryo
                'END YANAI 20110906 サンプル対応
            Else
                Me._frm.numHikiateKosuZan.Value = Convert.ToDecimal(Me._frm.numSouKosu.Value) - kosu
                Me._frm.numHikiateSuryoZan.Value = Convert.ToDecimal(Me._frm.numSouSuryo.Value) - suryo
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 個数・数量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetKosuSuryo(ByVal mode As String) As Boolean

        Dim kosu As Decimal = 0
        Dim suryo As Decimal = 0
        Dim konsu As Decimal = 0
        Dim hasu As Decimal = 0
        Dim amari As Decimal = 0

        With Me._frm
            'START YANAI 要望番号711
            If ("01").Equals(.lblTaniKowake.TextValue) = True Then
                '小分け全量出荷の場合は、再計算すると違う値になる
                Return True
            End If
            'END YANAI 要望番号711

            If LMConst.FLG.ON.Equals(mode) = True Then
                '梱数・端数変更時
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                 "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, _
                                                 "' AND SUB_KB = '80'"))
                If custDetailsDr.Length > 0 AndAlso .lblEdiOutkaTtlNb.TextValue <> "0" Then
                    'konsu = Math.Floor(Convert.ToDecimal(.numSouKosu.Value) / Convert.ToDecimal(.numIrisu.Value))
                    'hasu = Convert.ToDecimal(.numSouKosu.Value) Mod Convert.ToDecimal(.numIrisu.Value)
                    konsu = Math.Floor(CalcData(Convert.ToDecimal(.numSouKosu.Value), Convert.ToDecimal(.numIrisu.Value)))
                    hasu = CalcMod(Convert.ToDecimal(.numSouKosu.Value), Convert.ToDecimal(.numIrisu.Value))
                    suryo = (konsu * Convert.ToDecimal(.numIrisu.Value) + hasu) * Convert.ToDecimal(.numIrime.Value)
                    kosu = konsu * Convert.ToDecimal(.numIrisu.Value) + hasu
                Else
                    kosu = Convert.ToDecimal(.numKonsu.Value) * Convert.ToDecimal(.numIrisu.Value) + Convert.ToDecimal(.numHasu.Value)
                    suryo = kosu * Convert.ToDecimal(.numIrime.Value)
                    konsu = Convert.ToDecimal(.numKonsu.Value)
                    hasu = Convert.ToDecimal(.numHasu.Value)
                End If
            Else
                '数量変更時
                If (LMC020C.PLUS_ZERO).Equals(.numSouSuryo.TextValue) = True OrElse _
                    (LMC020C.MINUS_ZERO).Equals(.numSouSuryo.TextValue) = True OrElse _
                    (LMC020C.PLUS_ZERO).Equals(.numIrime.TextValue) = True OrElse _
                    (LMC020C.MINUS_ZERO).Equals(.numIrime.TextValue) = True Then
                    kosu = 0
                    hasu = 0
                Else
                    kosu = System.Math.Floor(Convert.ToDecimal(.numSouSuryo.Value) / Convert.ToDecimal(.numIrime.Value))
                    konsu = System.Math.Floor(Convert.ToDecimal(.numSouSuryo.Value) / Convert.ToDecimal(.numIrime.Value) / Convert.ToDecimal(.numIrisu.Value))
                    hasu = kosu - konsu * Convert.ToDecimal(.numIrisu.Value)
                End If
                suryo = Convert.ToDecimal(.numSouSuryo.Value)
            End If
            If .optKowake.Checked = True OrElse .optSample.Checked = True Then
                '小分けまたはサンプルの場合
                If (LMC020C.PLUS_ZERO).Equals(.numSouSuryo.TextValue) = True OrElse _
                    (LMC020C.MINUS_ZERO).Equals(.numSouSuryo.TextValue) = True OrElse _
                    (LMC020C.PLUS_ZERO).Equals(.numIrime.TextValue) = True OrElse _
                    (LMC020C.MINUS_ZERO).Equals(.numIrime.TextValue) = True OrElse _
                    .optSample.Checked = True Then
                    kosu = 0
                    hasu = 0
                Else
                    kosu = System.Math.Floor(Convert.ToDecimal(.numSouSuryo.Value) / Convert.ToDecimal(.numIrime.Value))
                    konsu = System.Math.Floor(Convert.ToDecimal(.numSouSuryo.Value) / Convert.ToDecimal(.numIrime.Value) / Convert.ToDecimal(.numIrisu.Value))
                    hasu = kosu - konsu * Convert.ToDecimal(.numIrisu.Value)
                    If 0 = kosu Then
                        kosu = 0
                        konsu = 0
                        hasu = 0
                    End If
                End If
                suryo = Convert.ToDecimal(.numSouSuryo.Value)
            End If

            '梱数値のチェック
            If Me._V.IsHaniCheck(konsu, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {_frm.lblKonsu.TextValue, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                Return False
            End If

            '端数値のチェック
            If Me._V.IsHaniCheck(hasu, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {_frm.lblHasu.TextValue, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                Return False
            End If

            '個数
            If Me._V.IsHaniCheck(kosu, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {_frm.lblKosu.TextValue, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                Return False
            End If

            If Me._V.IsHaniCheck(suryo, LMC020C.SURYO_MIN_NUM, Convert.ToDecimal(LMC020C.SURYO_MAX_NUM)) = False Then
                'メッセージの表示
                Me._LMCconV.SetErrMessage("E117", New String() {_frm.lblTitleSouSuryo.TextValue, Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                Return False
            End If

            .numKonsu.Value = konsu
            .numHasu.Value = hasu
            .numSouKosu.Value = kosu
            .numSouSuryo.Value = suryo
            If .optSample.Checked = True Then
                .numHikiateKosuZan.Value = 0
                'START YANAI 20110906 サンプル対応
                '.numHikiateSuryoZan.Value = 0
                .numHikiateSuryoZan.Value = Convert.ToString(Convert.ToDecimal(.numSouSuryo.Value) - _
                                                             Convert.ToDecimal(.numHikiateSuryoSumi.Value))
                'END YANAI 20110906 サンプル対応
            Else
                .numHikiateKosuZan.Value = Convert.ToString(Convert.ToDecimal(.numSouKosu.Value) - _
                                Convert.ToDecimal(.numHikiateKosuSumi.Value))
                .numHikiateSuryoZan.Value = Convert.ToString(Convert.ToDecimal(.numSouSuryo.Value) - _
                                                             Convert.ToDecimal(.numHikiateSuryoSumi.Value))
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷(中)SPREADダブルクリック時の作業(中)詳細表示処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSagyoMControl(ByVal activeRow As Integer, ByVal ds As DataSet) As Boolean

        With Me._frm

            Dim dr As DataRow() = Nothing
            Dim gMax As Integer = 0
            Dim sagyoCd As String = String.Empty
            Dim sagyoNm As String = String.Empty
            Dim sagyoRmk As String = String.Empty

            '作業コード取得（届先付帯作業ではない方）
            dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                        , "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, .lblSyukkaMNo.TextValue, "' AND " _
                                                                        , "DEST_SAGYO_FLG = '00' AND " _
                                                                        , "SYS_DEL_FLG = '0'"))
            gMax = dr.Length - 1

            For i As Integer = 0 To gMax
                sagyoCd = dr(i).Item("SAGYO_CD").ToString
                sagyoNm = dr(i).Item("SAGYO_RYAK").ToString
                sagyoRmk = dr(i).Item("REMARK_SIJI").ToString

                Select Case i
                    Case 0
                        '1レコード目
                        .txtSagyoM1.TextValue = sagyoCd
                        .lblSagyoM1.TextValue = sagyoNm
                        .txtSagyoRemarkM1.TextValue = sagyoRmk
                    Case 1
                        '2レコード目
                        .txtSagyoM2.TextValue = sagyoCd
                        .lblSagyoM2.TextValue = sagyoNm
                        .txtSagyoRemarkM2.TextValue = sagyoRmk
                    Case 2
                        '3レコード目
                        .txtSagyoM3.TextValue = sagyoCd
                        .lblSagyoM3.TextValue = sagyoNm
                        .txtSagyoRemarkM3.TextValue = sagyoRmk
                    Case 3
                        '4レコード目
                        .txtSagyoM4.TextValue = sagyoCd
                        .lblSagyoM4.TextValue = sagyoNm
                        .txtSagyoRemarkM4.TextValue = sagyoRmk
                    Case 4
                        '5レコード目
                        .txtSagyoM5.TextValue = sagyoCd
                        .lblSagyoM5.TextValue = sagyoNm
                        .txtSagyoRemarkM5.TextValue = sagyoRmk
                End Select

            Next

            '作業コード取得（届先付帯作業）
            dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                        "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, .lblSyukkaMNo.TextValue, "' AND " _
                                                                        , "DEST_SAGYO_FLG = '01'"))
            gMax = dr.Length - 1

            For i As Integer = 0 To gMax
                sagyoCd = dr(i).Item("SAGYO_CD").ToString
                sagyoNm = dr(i).Item("SAGYO_RYAK").ToString
                sagyoRmk = dr(i).Item("REMARK_SIJI").ToString

                Select Case i
                    Case 0
                        '1レコード目
                        .txtDestSagyoM1.TextValue = sagyoCd
                        .lblDestSagyoM1.TextValue = sagyoNm
                        .txtDestSagyoRemarkM1.TextValue = sagyoRmk
                    Case 1
                        '2レコード目
                        .txtDestSagyoM2.TextValue = sagyoCd
                        .lblDestSagyoM2.TextValue = sagyoNm
                        .txtDestSagyoRemarkM2.TextValue = sagyoRmk
                End Select

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 採番した出荷管理番号(大)を表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetOutkaLkanriNo(ByVal dt As DataTable)

        '値設定
        Dim dRow As DataRow = dt.Rows(0)
        Me._frm.lblSyukkaLNo.TextValue = dRow.Item("OUTKA_NO_L").ToString()

    End Sub

    ''' <summary>
    ''' 採番した運送番号を表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetUnsoLkanriNo(ByVal dt As DataTable)

        If dt.Rows.Count = 0 Then
            Exit Sub
        End If
        '値設定
        Dim dRow As DataRow = dt.Rows(0)
        Me._frm.txtUnsoNo.TextValue = dRow.Item("UNSO_NO_L").ToString()

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal hikiateFlg As String, ByVal activeRow As Integer)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            '.Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = New DataTable
            Dim tbl2 As DataTable = New DataTable

            If (spr.Name).Equals(_frm.sprSyukkaM.Name) = True Then
                tbl = ds.Tables(LMC020C.TABLE_NM_OUT_M)
            ElseIf (spr.Name).Equals(_frm.sprDtl.Name) = True AndAlso hikiateFlg = LMConst.FLG.OFF Then
                tbl = ds.Tables(LMC020C.TABLE_NM_OUT_S)
                tbl2 = ds.Tables(LMC020C.TABLE_NM_ZAI)
            ElseIf (spr.Name).Equals(_frm.sprDtl.Name) = True AndAlso hikiateFlg = LMConst.FLG.ON Then
                tbl = ds.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
            End If

            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow = Nothing
            'START YANAI 要望番号681
            Dim outSdr() As DataRow = Nothing
            'END YANAI 要望番号681

            '値設定
            If (spr.Name).Equals(_frm.sprSyukkaM.Name) = True Then

                Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                           , "SYS_DEL_FLG = '0'"))

                lngcnt = dr.Length

                If lngcnt = 0 AndAlso .Sheets(0).Rows.Count = 0 Then
                    .ResumeLayout(True)
                    Exit Sub
                End If

                .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

                lngcnt = lngcnt - 1

                For i As Integer = 0 To lngcnt

                    dRow = dr(i)

                    If i = 0 Then
                        Me._frm.numSyukkaSouKosu.Value = Me._LMCconG.FormatNumValue(dRow.Item("SUM_OUTKA_TTL_NB").ToString())
                    End If

                    'セルスタイル設定
                    .SetCellStyle(i, sprSyukkaM.DEFM.ColNo, sDEF)
                    .SetCellStyle(i, sprSyukkaM.PRT_ORDER.ColNo, nLabel)
                    .SetCellStyle(i, sprSyukkaM.SHOBO_CD.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.KANRI_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.GOODS_CD.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.GOODS_NM.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.SYUKKA_TANI.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
                    .SetCellStyle(i, sprSyukkaM.NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(i, sprSyukkaM.ALL_SURYO.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(i, sprSyukkaM.ZANSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(i, sprSyukkaM.HIKIATE_JK.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.GOODS_COMMENT.ColNo, sLabel)
                    '(2012.12.21)要望番号1710 ロット№追加 -- START --
                    .SetCellStyle(i, sprSyukkaM.M_LOT_NO.ColNo, sLabel)
                    '(2012.12.21)要望番号1710 ロット№追加 --  END  --
                    .SetCellStyle(i, sprSyukkaM.SHOBO_NM.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.SYS_DEL_FLG.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.REC_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.SEARCH_KEY_1.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.UNSO_ONDO_KB.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.PKG_UT.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.STD_IRIME_NB.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.STD_WT_KGS.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.TARE_YN.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.REMARK.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.REMARK_OUT.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.TAX_KB.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.OUTKA_ATT.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.NB_UT.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(i, sprSyukkaM.DEFM.ColNo, LMC020C.FLG_FALSE)
                    .SetCellValue(i, sprSyukkaM.PRT_ORDER.ColNo, dRow.Item("PRINT_SORT").ToString())
                    .SetCellValue(i, sprSyukkaM.SHOBO_CD.ColNo, dRow.Item("SHOBO_CD").ToString())
                    .SetCellValue(i, sprSyukkaM.KANRI_NO.ColNo, dRow.Item("OUTKA_NO_M").ToString())
                    .SetCellValue(i, sprSyukkaM.GOODS_CD.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
                    .SetCellValue(i, sprSyukkaM.GOODS_NM.ColNo, dRow.Item("GOODS_NM").ToString())
                    .SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, dRow.Item("ALCTD_KB_NM").ToString())
                    .SetCellValue(i, sprSyukkaM.IRIME.ColNo, dRow.Item("IRIME").ToString())
                    .SetCellValue(i, sprSyukkaM.NB.ColNo, dRow.Item("OUTKA_TTL_NB").ToString())
                    .SetCellValue(i, sprSyukkaM.ALL_SURYO.ColNo, dRow.Item("OUTKA_TTL_QT").ToString())
                    .SetCellValue(i, sprSyukkaM.ZANSU.ColNo, dRow.Item("BACKLOG_NB").ToString())
                    .SetCellValue(i, sprSyukkaM.HIKIATE_JK.ColNo, dRow.Item("HIKIATE").ToString())
                    .SetCellValue(i, sprSyukkaM.GOODS_COMMENT.ColNo, dRow.Item("REMARK").ToString())
                    '(2012.12.21)要望番号1710 ロット№追加 -- START --
                    .SetCellValue(i, sprSyukkaM.M_LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                    '(2012.12.21)要望番号1710 ロット№追加 --  END  --
                    .SetCellValue(i, sprSyukkaM.SHOBO_NM.ColNo, dRow.Item("SHOBO_NM").ToString())
                    .SetCellValue(i, sprSyukkaM.SYS_DEL_FLG.ColNo, dRow.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(i, sprSyukkaM.REC_NO.ColNo, Convert.ToString(i))
                    .SetCellValue(i, sprSyukkaM.SEARCH_KEY_1.ColNo, dRow.Item("SEARCH_KEY_1").ToString())
                    .SetCellValue(i, sprSyukkaM.UNSO_ONDO_KB.ColNo, dRow.Item("GOODS_UNSO_ONDO_KB").ToString())
                    .SetCellValue(i, sprSyukkaM.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString())
                    .SetCellValue(i, sprSyukkaM.STD_IRIME_NB.ColNo, dRow.Item("STD_IRIME_NB").ToString())
                    .SetCellValue(i, sprSyukkaM.STD_WT_KGS.ColNo, dRow.Item("STD_WT_KGS").ToString())
                    .SetCellValue(i, sprSyukkaM.TARE_YN.ColNo, dRow.Item("TARE_YN").ToString())
                    .SetCellValue(i, sprSyukkaM.REMARK.ColNo, dRow.Item("REMARK").ToString())
                    .SetCellValue(i, sprSyukkaM.REMARK_OUT.ColNo, dRow.Item("REMARK_OUT").ToString())
                    .SetCellValue(i, sprSyukkaM.TAX_KB.ColNo, dRow.Item("TAX_KB").ToString())
                    .SetCellValue(i, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, dRow.Item("HIKIATE_ALERT_YN").ToString())
                    .SetCellValue(i, sprSyukkaM.OUTKA_ATT.ColNo, dRow.Item("OUTKA_ATT").ToString())
                    .SetCellValue(i, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, dRow.Item("GOODS_CD_NRS_FROM").ToString())
                    .SetCellValue(i, sprSyukkaM.NB_UT.ColNo, dRow.Item("NB_UT").ToString())

                    'START YANAI 要望番号681
                    outSdr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                  , "OUTKA_NO_M = '", dRow.Item("OUTKA_NO_M").ToString(), "' AND " _
                                                                                  , "SMPL_FLAG = '01' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))
                    If 0 < outSdr.Length AndAlso ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                        .SetCellValue(i, sprSyukkaM.SYUKKA_TANI_KOWAKE.ColNo, "01")
                    End If
                    'END YANAI 要望番号681

                    Me._frm.numRecMCnt.Value = i

                    '値設定

                    Dim dMHRow As DataRow() = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select((String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                     , "SYS_DEL_FLG = '0'")))

                    If dMHRow.Length = 0 Then
                        Continue For
                    End If

                    'セルスタイル設定
                    .SetCellStyle(i, sprSyukkaM.CASE_NO_FROM.ColNo, nLabel)
                    .SetCellStyle(i, sprSyukkaM.CASE_NO_TO.ColNo, nLabel)
                    .SetCellStyle(i, sprSyukkaM.MARK_SYS_DEL_FLG.ColNo, sLabel)
                    .SetCellStyle(i, sprSyukkaM.MARK_UP_KBN.ColNo, sLabel)

                    'セルに値を設定
                    If String.IsNullOrEmpty(dMHRow(0).Item("CASE_NO_FROM").ToString()) = True Then
                        .SetCellValue(i, sprSyukkaM.CASE_NO_FROM.ColNo, 0)
                    Else
                        .SetCellValue(i, sprSyukkaM.CASE_NO_FROM.ColNo, dMHRow(0).Item("CASE_NO_FROM").ToString())
                    End If
                    If String.IsNullOrEmpty(dMHRow(0).Item("CASE_NO_FROM").ToString()) = True Then
                        .SetCellValue(i, sprSyukkaM.CASE_NO_TO.ColNo, 0)
                    Else
                        .SetCellValue(i, sprSyukkaM.CASE_NO_TO.ColNo, dMHRow(0).Item("CASE_NO_TO").ToString())
                    End If
                    .SetCellValue(i, sprSyukkaM.MARK_SYS_DEL_FLG.ColNo, dMHRow(0).Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(i, sprSyukkaM.MARK_UP_KBN.ColNo, dMHRow(0).Item("UP_KBN").ToString())

                Next

            ElseIf (spr.Name).Equals(_frm.sprDtl.Name) = True Then

                'START YANAI メモ②No.26
                Me._frm.lblRemark.TextValue = String.Empty
                Me._frm.lblRemarkOut.TextValue = String.Empty
                'END YANAI メモ②No.26

                Dim activeRowindex As Integer = 0
                If activeRow <> -1 Then
                    activeRowindex = activeRow
                Else
                    activeRowindex = _frm.sprSyukkaM.Sheets(0).ActiveRow.Index
                End If

                Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                    (String.Concat("OUTKA_NO_L = '", Me._frm.lblSyukkaLNo.TextValue, "' AND " _
                                     , "OUTKA_NO_M = '", _LMCconG.GetCellValue(_frm.sprSyukkaM.ActiveSheet.Cells(activeRowindex, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                     , "SYS_DEL_FLG = '0'"))
                lngcnt = dr.Length - 1

                If (-1) = lngcnt Then
                    Me._frm.numRecSCnt.Value = 0
                    .ResumeLayout(True)
                    Exit Sub
                End If

                .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt + 1)

                'START YANAI メモ②No.26
                'Me._frm.lblRemark.TextValue = String.Empty
                'Me._frm.lblRemarkOut.TextValue = String.Empty
                'END YANAI メモ②No.26

                For i As Integer = 0 To lngcnt

                    dRow = dr(i)

                    'セルスタイル設定
                    .SetCellStyle(i, sprDtl.DEF.ColNo, sDEF)
                    .SetCellStyle(i, sprDtl.LOT_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
                    .SetCellStyle(i, sprDtl.TOU_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SHITSU_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ZONE_CD.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.LOCA.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(i, sprDtl.ALCTD_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(i, sprDtl.ALCTD_CAN_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(i, sprDtl.ALCTD_CAN_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(i, sprDtl.NAKAMI.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.GAIKAN.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.JOTAI_NM.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.REMARK.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.INKO_DATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.LT_DATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.HORYUHIN.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.BOGAIHIN.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.RSV_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SERIAL_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.GOODS_CRT_DATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.WARIATE_NM.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.REMARK_OUT.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SHO_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ZAI_REC_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.INKA_NO_L.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.INKA_NO_M.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.INKA_NO_S.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SMPL_FLAG.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.GOODS_COND_KB_1.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.GOODS_COND_KB_2.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.GOODS_COND_KB_3.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.OFB_KB.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SPD_KB.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.PORA_ZAI_NB.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.PORA_ZAI_QT.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALLOC_CAN_NB_HOZON.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALLOC_CAN_NB.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALLOC_CAN_QT.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SYS_DEL_FLG.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.REC_NO.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.HOKAN_YN.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.TAX_KB.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.INKO_PLAN_DATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.WARIATE.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_NB_HOZON.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_QT_HOZON.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.DEST_CD_P.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.UPDATE_FLG.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_NB_MATOME.ColNo, sLabel)
                    .SetCellStyle(i, sprDtl.ALCTD_QT_MATOME.ColNo, sLabel)

                    'セルに値を設定
                    Dim zaiDr As DataRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                            "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                            "SYS_DEL_FLG = '0'"))(0)

                    .SetCellValue(i, sprDtl.DEF.ColNo, LMC020C.FLG_FALSE)
                    .SetCellValue(i, sprDtl.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                    .SetCellValue(i, sprDtl.IRIME.ColNo, dRow.Item("IRIME").ToString())
                    .SetCellValue(i, sprDtl.TOU_NO.ColNo, dRow.Item("TOU_NO").ToString())
                    .SetCellValue(i, sprDtl.SHITSU_NO.ColNo, dRow.Item("SITU_NO").ToString())
                    .SetCellValue(i, sprDtl.ZONE_CD.ColNo, dRow.Item("ZONE_CD").ToString())
                    .SetCellValue(i, sprDtl.LOCA.ColNo, dRow.Item("LOCA").ToString())
                    .SetCellValue(i, sprDtl.ALCTD_NB.ColNo, dRow.Item("ALCTD_NB_GAMEN").ToString())
                    .SetCellValue(i, sprDtl.ALCTD_QT.ColNo, dRow.Item("ALCTD_QT_GAMEN").ToString())
                    'If dRow.Item("ALCTD_CAN_NB_GAMEN").ToString().Equals(dRow.Item("ALCTD_CAN_NB_MATOME").ToString()) = True Then
                    If String.IsNullOrEmpty(dRow.Item("ALCTD_CAN_NB_MATOME").ToString()) = True OrElse _
                        dRow.Item("ALCTD_CAN_NB_GAMEN").ToString().Equals(dRow.Item("ALCTD_CAN_NB_MATOME").ToString()) = True Then
                        .SetCellValue(i, sprDtl.ALCTD_QT.ColNo, dRow.Item("ALCTD_QT_GAMEN").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_CAN_NB.ColNo, dRow.Item("ALCTD_CAN_NB_GAMEN").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_CAN_QT.ColNo, dRow.Item("ALCTD_CAN_QT_GAMEN").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_NB.ColNo, dRow.Item("ALCTD_NB_GAMEN").ToString())
                    Else
                        .SetCellValue(i, sprDtl.ALCTD_CAN_NB.ColNo, dRow.Item("ALCTD_CAN_NB_MATOME").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_CAN_QT.ColNo, dRow.Item("ALCTD_CAN_QT_MATOME").ToString())
                        'START YANAI 要望番号853 まとめ処理対応
                        '.SetCellValue(i, sprDtl.ALCTD_NB.ColNo, dRow.Item("ALCTD_NB_MATOME").ToString())
                        '.SetCellValue(i, sprDtl.ALCTD_QT.ColNo, dRow.Item("ALCTD_QT_MATOME").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_NB.ColNo, dRow.Item("ALCTD_NB_GAMEN").ToString())
                        .SetCellValue(i, sprDtl.ALCTD_QT.ColNo, dRow.Item("ALCTD_QT_GAMEN").ToString())
                        'END YANAI 要望番号853 まとめ処理対応
                    End If
                    'START YANAI 要望番号776
                    .SetCellValue(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo, dRow.Item("ALCTD_CAN_NB_MATOME").ToString())
                    .SetCellValue(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo, dRow.Item("ALCTD_CAN_QT_MATOME").ToString())
                    'END YANAI 要望番号776
                    .SetCellValue(i, sprDtl.NAKAMI.ColNo, zaiDr.Item("GOODS_COND_NM_1").ToString())
                    .SetCellValue(i, sprDtl.GAIKAN.ColNo, zaiDr.Item("GOODS_COND_NM_2").ToString())
                    .SetCellValue(i, sprDtl.JOTAI_NM.ColNo, zaiDr.Item("GOODS_COND_NM_3").ToString())
                    .SetCellValue(i, sprDtl.REMARK.ColNo, dRow.Item("REMARK").ToString())
                    .SetCellValue(i, sprDtl.INKO_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiDr.Item("INKO_DATE").ToString()))
                    .SetCellValue(i, sprDtl.LT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiDr.Item("LT_DATE").ToString()))
                    .SetCellValue(i, sprDtl.HORYUHIN.ColNo, zaiDr.Item("SPD_KB_NM").ToString())
                    .SetCellValue(i, sprDtl.BOGAIHIN.ColNo, zaiDr.Item("OFB_KB_NM").ToString())
                    .SetCellValue(i, sprDtl.RSV_NO.ColNo, zaiDr.Item("RSV_NO").ToString())
                    .SetCellValue(i, sprDtl.SERIAL_NO.ColNo, dRow.Item("SERIAL_NO").ToString())
                    .SetCellValue(i, sprDtl.GOODS_CRT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiDr.Item("GOODS_CRT_DATE").ToString()))
                    .SetCellValue(i, sprDtl.WARIATE_NM.ColNo, zaiDr.Item("ALLOC_PRIORITY_NM").ToString())
                    .SetCellValue(i, sprDtl.REMARK_OUT.ColNo, zaiDr.Item("REMARK_OUT").ToString())
                    .SetCellValue(i, sprDtl.SHO_NO.ColNo, dRow.Item("OUTKA_NO_S").ToString())
                    .SetCellValue(i, sprDtl.ZAI_REC_NO.ColNo, zaiDr.Item("ZAI_REC_NO").ToString())
                    .SetCellValue(i, sprDtl.INKA_NO_L.ColNo, dRow.Item("INKA_NO_L").ToString())
                    .SetCellValue(i, sprDtl.INKA_NO_M.ColNo, dRow.Item("INKA_NO_M").ToString())
                    .SetCellValue(i, sprDtl.INKA_NO_S.ColNo, dRow.Item("INKA_NO_S").ToString())
                    .SetCellValue(i, sprDtl.SMPL_FLAG.ColNo, dRow.Item("SMPL_FLAG").ToString())
                    .SetCellValue(i, sprDtl.GOODS_COND_KB_1.ColNo, zaiDr.Item("GOODS_COND_KB_1").ToString())
                    .SetCellValue(i, sprDtl.GOODS_COND_KB_2.ColNo, zaiDr.Item("GOODS_COND_KB_2").ToString())
                    .SetCellValue(i, sprDtl.GOODS_COND_KB_3.ColNo, zaiDr.Item("GOODS_COND_KB_3").ToString())
                    .SetCellValue(i, sprDtl.OFB_KB.ColNo, zaiDr.Item("OFB_KB").ToString())
                    .SetCellValue(i, sprDtl.SPD_KB.ColNo, zaiDr.Item("SPD_KB").ToString())
                    .SetCellValue(i, sprDtl.PORA_ZAI_NB.ColNo, zaiDr.Item("PORA_ZAI_NB").ToString())
                    .SetCellValue(i, sprDtl.PORA_ZAI_QT.ColNo, zaiDr.Item("PORA_ZAI_QT").ToString())
                    .SetCellValue(i, sprDtl.ALLOC_CAN_NB_HOZON.ColNo, zaiDr.Item("ALLOC_CAN_NB_HOZON").ToString())
                    .SetCellValue(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo, zaiDr.Item("ALLOC_CAN_QT_HOZON").ToString())
                    .SetCellValue(i, sprDtl.ALLOC_CAN_NB.ColNo, zaiDr.Item("ALLOC_CAN_NB").ToString())
                    .SetCellValue(i, sprDtl.ALLOC_CAN_QT.ColNo, zaiDr.Item("ALLOC_CAN_QT").ToString())
                    .SetCellValue(i, sprDtl.SYS_DEL_FLG.ColNo, dRow.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(i, sprDtl.REC_NO.ColNo, Convert.ToString(i))
                    .SetCellValue(i, sprDtl.SYS_UPD_DATE.ColNo, dRow.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(i, sprDtl.SYS_UPD_TIME.ColNo, dRow.Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(i, sprDtl.HOKAN_YN.ColNo, zaiDr.Item("HOKAN_YN").ToString())
                    .SetCellValue(i, sprDtl.TAX_KB.ColNo, zaiDr.Item("TAX_KB").ToString())
                    .SetCellValue(i, sprDtl.INKO_PLAN_DATE.ColNo, zaiDr.Item("INKO_PLAN_DATE").ToString())
                    .SetCellValue(i, sprDtl.WARIATE.ColNo, zaiDr.Item("ALLOC_PRIORITY").ToString())
                    .SetCellValue(i, sprDtl.ALCTD_NB_HOZON.ColNo, zaiDr.Item("ALCTD_NB_HOZON").ToString())
                    .SetCellValue(i, sprDtl.ALCTD_QT_HOZON.ColNo, zaiDr.Item("ALCTD_QT_HOZON").ToString())
                    .SetCellValue(i, sprDtl.DEST_CD_P.ColNo, zaiDr.Item("DEST_CD_P").ToString())
                    .SetCellValue(i, sprDtl.UPDATE_FLG.ColNo, LMConst.FLG.OFF)

                    '引当画面初期検索条件用の値を設定
                    If String.IsNullOrEmpty(Me._frm.lblRemark.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(dRow.Item("REMARK").ToString()) = False Then
                        Me._frm.lblRemark.TextValue = dRow.Item("REMARK").ToString
                    End If
                    If String.IsNullOrEmpty(Me._frm.lblRemarkOut.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(zaiDr.Item("REMARK_OUT").ToString()) = False Then
                        Me._frm.lblRemarkOut.TextValue = zaiDr.Item("REMARK_OUT").ToString
                    End If

                    Me._frm.numRecSCnt.Value = i
                Next

            End If

            .ResumeLayout(True)

        End With

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' スプレッドにデータを設定(シッピングマーク(HED))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMarkHed(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal activeRow As Integer)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            'データ挿入
            '行数設定
            Dim tbl As DataTable = New DataTable

            tbl = ds.Tables(LMC020C.TABLE_NM_MARK_HED)

            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow = Nothing

            '値設定

            'Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select((String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
            '                                                                 , "SYS_DEL_FLG = '0'")))
            Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select()

            lngcnt = dr.Length

            If lngcnt = 0 AndAlso .Sheets(0).Rows.Count = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            lngcnt = lngcnt - 1

            For i As Integer = 0 To lngcnt

                dRow = dr(i)

                'セルスタイル設定
                .SetCellStyle(i, sprSyukkaM.CASE_NO_FROM.ColNo, nLabel)
                .SetCellStyle(i, sprSyukkaM.CASE_NO_TO.ColNo, nLabel)
                .SetCellStyle(i, sprSyukkaM.MARK_SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprSyukkaM.MARK_UP_KBN.ColNo, sLabel)

                'セルに値を設定
                If String.IsNullOrEmpty(dRow.Item("CASE_NO_FROM").ToString()) = True Then
                    .SetCellValue(i, sprSyukkaM.CASE_NO_FROM.ColNo, 0)
                Else
                    .SetCellValue(i, sprSyukkaM.CASE_NO_FROM.ColNo, dRow.Item("CASE_NO_FROM").ToString())
                End If
                If String.IsNullOrEmpty(dRow.Item("CASE_NO_FROM").ToString()) = True Then
                    .SetCellValue(i, sprSyukkaM.CASE_NO_TO.ColNo, 0)
                Else
                    .SetCellValue(i, sprSyukkaM.CASE_NO_TO.ColNo, dRow.Item("CASE_NO_TO").ToString())
                End If
                .SetCellValue(i, sprSyukkaM.MARK_SYS_DEL_FLG.ColNo, dRow.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, sprSyukkaM.MARK_UP_KBN.ColNo, dRow.Item("UP_KBN").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub
    '2015.07.08 協立化学　シッピング対応 追加END

    'START YANAI 要望番号853 まとめ処理対応
    '''' <summary>
    '''' スプレッド(小)の同じキーのデータをまとめる
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SpreadGroup(ByVal ds As DataSet)

    '    With Me._frm

    '        Dim max As Integer = .sprDtl.ActiveSheet.Rows.Count - 1
    '        Dim groupFlg As Boolean = False
    '        Dim row As Integer = -1
    '        Dim canNb As Decimal = 0
    '        Dim canQt As Decimal = 0
    '        Dim alctdNb As Decimal = 0
    '        Dim alctdQt As Decimal = 0

    '        max = max - 1   '1行しかない時に、1行目と2行目を比較しないように-1する
    '        For i As Integer = 0 To max
    '            If Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOT_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.LOT_NO.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.IRIME.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.TOU_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.TOU_NO.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHITSU_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.SHITSU_NO.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZONE_CD.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.ZONE_CD.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOCA.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.LOCA.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.NAKAMI.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.NAKAMI.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.GAIKAN.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.GAIKAN.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.JOTAI_NM.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.JOTAI_NM.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.REMARK.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.REMARK.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKO_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.INKO_DATE.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LT_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.LT_DATE.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.HORYUHIN.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.HORYUHIN.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.BOGAIHIN.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.BOGAIHIN.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.RSV_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.RSV_NO.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SERIAL_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.SERIAL_NO.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.GOODS_CRT_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.GOODS_CRT_DATE.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.WARIATE_NM.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.WARIATE_NM.ColNo))) = True AndAlso _
    '                Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.REMARK_OUT.ColNo)).Equals(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.REMARK_OUT.ColNo))) = True Then
    '                '一致するデータがある場合
    '                If row = -1 Then
    '                    '一致データの1件目の時
    '                    row = i
    '                    groupFlg = True
    '                    canNb = canNb + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo)))
    '                    canQt = canQt + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo)))
    '                    alctdNb = alctdNb + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
    '                    alctdQt = alctdQt + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
    '                End If
    '                canNb = canNb + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.ALCTD_CAN_NB.ColNo)))
    '                canQt = canQt + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.ALCTD_CAN_QT.ColNo)))
    '                alctdNb = alctdNb + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.ALCTD_NB.ColNo)))
    '                alctdQt = alctdQt + Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i + 1, sprDtl.ALCTD_QT.ColNo)))

    '            ElseIf groupFlg = True And (-1).Equals(row) = False Then
    '                '一致しなかった場合で、まとめる必要がある場合
    '                Call Me.SetSpreadGroup(ds, row, i, canNb, canQt, alctdNb, alctdQt)

    '                '値のリセット
    '                row = -1
    '                canNb = 0
    '                canQt = 0
    '                alctdNb = 0
    '                alctdQt = 0

    '            End If
    '        Next

    '        If groupFlg = True And (-1).Equals(row) = False Then
    '            '一致しなかった場合で、まとめる必要がある場合
    '            Call Me.SetSpreadGroup(ds, row, max + 1, canNb, canQt, alctdNb, alctdQt)
    '        End If

    '    End With

    'End Sub
    'END YANAI 要望番号853 まとめ処理対応

    'START YANAI 要望番号853 まとめ処理対応
    '''' <summary>
    '''' スプレッド(小)の同じキーのデータをまとめる
    '''' </summary>
    '''' <param name="canNb">引当可能個数(今回引当分が引かれている)</param>
    '''' <param name="canQt">引当可能数量(今回引当分が引かれている)</param>
    '''' <param name="alctdNb">引当個数</param>
    '''' <param name="alctdQt">引当数量</param>
    '''' <remarks></remarks>
    'Friend Sub SetSpreadGroup(ByVal ds As DataSet, _
    '                          ByVal startRow As Integer, _
    '                          ByVal endRow As Integer, _
    '                          ByVal canNb As Decimal, _
    '                          ByVal canQt As Decimal, _
    '                          ByVal alctdNb As Decimal, _
    '                          ByVal alctdQt As Decimal)

    '    'Dim outRow() As DataRow = Nothing
    '    Dim zaiRow() As DataRow = Nothing

    '    With Me._frm

    '        .SuspendLayout()

    '        canNb = canNb + alctdNb
    '        canQt = canQt + alctdQt

    '        Dim matomeFlg As String = String.Empty
    '        Dim zaiRecNo As String = String.Empty

    '        For i As Integer = startRow To endRow
    '            zaiRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
    '                                                            "OUTKA_NO_M = '", Me._frm.lblSyukkaMNo.TextValue, "' AND ", _
    '                                                            "OUTKA_NO_S = '", Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo)), "'"))

    '            If zaiRow.Length = 0 Then
    '                '新規追加の1回目の時
    '                canNb = canNb - Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
    '                canQt = canQt - Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
    '                .sprDtl.SetCellValue(i, sprDtl.ALCTD_CAN_NB.ColNo, Convert.ToString(canNb))
    '                .sprDtl.SetCellValue(i, sprDtl.ALCTD_CAN_QT.ColNo, Convert.ToString(canQt))

    '            ElseIf zaiRow.Length > 0 Then
    '                If ("1").Equals(zaiRow(0).Item("MATOME_FLG")) = False Then
    '                    '編集の1回目の時
    '                    canNb = canNb - Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
    '                    canQt = canQt - Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
    '                    .sprDtl.SetCellValue(i, sprDtl.ALCTD_CAN_NB.ColNo, Convert.ToString(canNb))
    '                    .sprDtl.SetCellValue(i, sprDtl.ALCTD_CAN_QT.ColNo, Convert.ToString(canQt))

    '                    zaiRow(0).Item("MATOME_FLG") = "1"
    '                End If
    '            End If

    '        Next

    '        .ResumeLayout(True)

    '    End With

    'End Sub
    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' スプレッドにデータを追加(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddOutkaMspread(ByVal spr As LMSpread, ByVal dt As DataTable)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow

            '値設定
            dRow = dt.Rows(0)

            Dim row As Integer = .Sheets(0).Rows.Count - 1
            'セルスタイル設定
            .SetCellStyle(row, sprSyukkaM.DEFM.ColNo, sDEF)
            .SetCellStyle(row, sprSyukkaM.PRT_ORDER.ColNo, nLabel)
            .SetCellStyle(row, sprSyukkaM.SHOBO_CD.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.KANRI_NO.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_CD.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_NM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SYUKKA_TANI.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
            .SetCellStyle(row, sprSyukkaM.NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
            .SetCellStyle(row, sprSyukkaM.ALL_SURYO.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
            .SetCellStyle(row, sprSyukkaM.ZANSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
            .SetCellStyle(row, sprSyukkaM.HIKIATE_JK.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_COMMENT.ColNo, sLabel)
            '(2012.12.21)要望番号1710 ロット№追加 -- START --
            .SetCellStyle(row, sprSyukkaM.M_LOT_NO.ColNo, sLabel)
            '(2012.12.21)要望番号1710 ロット№追加 --  END  --
            .SetCellStyle(row, sprSyukkaM.SHOBO_NM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SYS_DEL_FLG.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REC_NO.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SEARCH_KEY_1.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.UNSO_ONDO_KB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.PKG_UT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.STD_IRIME_NB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.STD_WT_KGS.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.TARE_YN.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REMARK.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REMARK_OUT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.TAX_KB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.OUTKA_ATT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.CUST_CD_S.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.CUST_CD_SS.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.NB_UT.ColNo, sLabel)
            'START YANAI メモ②No.20
            .SetCellStyle(row, sprSyukkaM.EDI_FLG.ColNo, sLabel)
            'END YANAI メモ②No.20

            'セルに値を設定
            .SetCellValue(row, sprSyukkaM.DEFM.ColNo, LMC020C.FLG_FALSE)
            .SetCellValue(row, sprSyukkaM.PRT_ORDER.ColNo, "99")
            .SetCellValue(row, sprSyukkaM.SHOBO_CD.ColNo, dRow.Item("SHOBO_CD").ToString())
            .SetCellValue(row, sprSyukkaM.KANRI_NO.ColNo, String.Empty)
            .SetCellValue(row, sprSyukkaM.GOODS_CD.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
            .SetCellValue(row, sprSyukkaM.GOODS_NM.ColNo, dRow.Item("NM_1").ToString())
            .SetCellValue(row, sprSyukkaM.SYUKKA_TANI.ColNo, String.Empty)
            .SetCellValue(row, sprSyukkaM.IRIME.ColNo, dRow.Item("IRIME").ToString())
            .SetCellValue(row, sprSyukkaM.NB.ColNo, String.Empty)
            .SetCellValue(row, sprSyukkaM.ALL_SURYO.ColNo, String.Empty)
            .SetCellValue(row, sprSyukkaM.ZANSU.ColNo, String.Empty)
            .SetCellValue(row, sprSyukkaM.HIKIATE_JK.ColNo, Me._frm.lblHikiate.TextValue)
            If String.IsNullOrEmpty(Me._frm.txtGoodsRemark.TextValue) = True Then
                .SetCellValue(row, sprSyukkaM.GOODS_COMMENT.ColNo, dRow.Item("OUTKA_ATT").ToString)
            End If
            '(2012.12.21)要望番号1710 ロット№追加 -- START --
            .SetCellValue(row, sprSyukkaM.M_LOT_NO.ColNo, dRow.Item("LOT_NO").ToString)
            '(2012.12.21)要望番号1710 ロット№追加 --  END  --
            .SetCellValue(row, sprSyukkaM.SHOBO_NM.ColNo, dRow.Item("SHOBO_NM").ToString())
            .SetCellValue(row, sprSyukkaM.SYS_DEL_FLG.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, sprSyukkaM.REC_NO.ColNo, Convert.ToString(Convert.ToDecimal(_frm.numRecMCnt.Value) + 1))
            .SetCellValue(row, sprSyukkaM.SEARCH_KEY_1.ColNo, dRow.Item("SEARCH_KEY_1").ToString())
            .SetCellValue(row, sprSyukkaM.UNSO_ONDO_KB.ColNo, dRow.Item("UNSO_ONDO_KB").ToString())
            .SetCellValue(row, sprSyukkaM.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString())
            .SetCellValue(row, sprSyukkaM.STD_IRIME_NB.ColNo, dRow.Item("STD_IRIME_NB").ToString())
            .SetCellValue(row, sprSyukkaM.STD_WT_KGS.ColNo, dRow.Item("STD_WT_KGS").ToString())
            .SetCellValue(row, sprSyukkaM.TARE_YN.ColNo, dRow.Item("TARE_YN").ToString())
            .SetCellValue(row, sprSyukkaM.REMARK.ColNo, dRow.Item("REMARK").ToString())
            .SetCellValue(row, sprSyukkaM.REMARK_OUT.ColNo, dRow.Item("REMARK_OUT").ToString())
            .SetCellValue(row, sprSyukkaM.TAX_KB.ColNo, dRow.Item("TAX_KB").ToString())
            .SetCellValue(row, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, dRow.Item("HIKIATE_ALERT_YN").ToString())
            .SetCellValue(row, sprSyukkaM.OUTKA_ATT.ColNo, dRow.Item("OUTKA_ATT").ToString())
            .SetCellValue(row, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, dRow.Item("GOODS_CD_NRS_FROM").ToString())
            .SetCellValue(row, sprSyukkaM.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString())
            .SetCellValue(row, sprSyukkaM.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString())
            .SetCellValue(row, sprSyukkaM.NB_UT.ColNo, dRow.Item("NB_UT").ToString())
            'START YANAI メモ②No.20
            .SetCellValue(row, sprSyukkaM.EDI_FLG.ColNo, String.Empty)
            'END YANAI メモ②No.20

            Me._frm.numRecMCnt.Value = Convert.ToDecimal(_frm.numRecMCnt.Value) + 1

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドのデータを複写(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyOutkaM(ByVal spr As LMSpread, ByVal ds As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim msg As String = String.Empty

            .SuspendLayout()

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then

                    '出荷管理番号(中)の採番をする
                    Dim maxCnt As Integer = ds.Tables(LMC020C.TABLE_NM_MAX).Rows.Count
                    Dim insMaxRows As DataRow = ds.Tables(LMC020C.TABLE_NM_MAX).NewRow

                    '出荷管理番号(中)の最大値を求める
                    Dim oldMaxOutM As String = ds.Tables(LMC020C.TABLE_NM_MAX).Rows(maxCnt - 1).Item("OUTKA_NO_M").ToString()
                    Dim newMaxOutM As String = Me.SetZeroData(Convert.ToString(Convert.ToDecimal(oldMaxOutM) + 1), "000")

                    '2017/09/25 修正 李↓
                    msg = lgm.Selector({"出荷管理番号(中)", "Shipment control number (M)", "출하관리번호(中)", "中国語"})
                    '2017/09/25 修正 李↑

                    'SEQの限界値、チェック
                    If Me._LMCconV.IsMaxChk(Convert.ToInt32(newMaxOutM), 999, msg) = False Then
                        '処理終了アクション
                        Exit Sub
                    End If

                    '出荷管理番号(中)の更新
                    insMaxRows("OUTKA_NO_M") = newMaxOutM
                    insMaxRows("MAX_OUTKA_NO_S") = "000"

                    'データセットに追加
                    ds.Tables(LMC020C.TABLE_NM_MAX).Rows.Add(insMaxRows)


                    'スプレッド追加処理
                    .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)
                    max = max + 1

                    'セルスタイル設定
                    .SetCellStyle(max, sprSyukkaM.DEFM.ColNo, sDEF)
                    .SetCellStyle(max, sprSyukkaM.PRT_ORDER.ColNo, nLabel)
                    .SetCellStyle(max, sprSyukkaM.SHOBO_CD.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.KANRI_NO.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.GOODS_CD.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.GOODS_NM.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.SYUKKA_TANI.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
                    .SetCellStyle(max, sprSyukkaM.NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(max, sprSyukkaM.ALL_SURYO.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(max, sprSyukkaM.ZANSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(max, sprSyukkaM.HIKIATE_JK.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.GOODS_COMMENT.ColNo, sLabel)
                    '(2012.12.21)要望番号1710 ロット№追加 -- START --
                    .SetCellStyle(max, sprSyukkaM.M_LOT_NO.ColNo, sLabel)
                    '(2012.12.21)要望番号1710 ロット№追加 --  END  --
                    .SetCellStyle(max, sprSyukkaM.SHOBO_NM.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.SYS_DEL_FLG.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.REC_NO.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.SEARCH_KEY_1.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.UNSO_ONDO_KB.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.PKG_UT.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.STD_IRIME_NB.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.STD_WT_KGS.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.TARE_YN.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.REMARK.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.REMARK_OUT.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.TAX_KB.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.OUTKA_ATT.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.CUST_CD_S.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.CUST_CD_SS.ColNo, sLabel)
                    .SetCellStyle(max, sprSyukkaM.NB_UT.ColNo, sLabel)
                    'START YANAI メモ②No.20
                    .SetCellStyle(max, sprSyukkaM.EDI_FLG.ColNo, sLabel)
                    'END YANAI メモ②No.20

                    'セルに値を設定
                    .SetCellValue(max, sprSyukkaM.DEFM.ColNo, LMC020C.FLG_FALSE)
                    .SetCellValue(max, sprSyukkaM.PRT_ORDER.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.PRT_ORDER.ColNo)))
                    .SetCellValue(max, sprSyukkaM.SHOBO_CD.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.SHOBO_CD.ColNo)))
                    .SetCellValue(max, sprSyukkaM.KANRI_NO.ColNo, newMaxOutM)
                    .SetCellValue(max, sprSyukkaM.GOODS_CD.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.GOODS_CD.ColNo)))
                    .SetCellValue(max, sprSyukkaM.GOODS_NM.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.GOODS_NM.ColNo)))
                    .SetCellValue(max, sprSyukkaM.SYUKKA_TANI.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.SYUKKA_TANI.ColNo)))
                    .SetCellValue(max, sprSyukkaM.IRIME.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.IRIME.ColNo)))
                    .SetCellValue(max, sprSyukkaM.NB.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.NB.ColNo)))
                    .SetCellValue(max, sprSyukkaM.ALL_SURYO.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.ALL_SURYO.ColNo)))
                    .SetCellValue(max, sprSyukkaM.ZANSU.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.ZANSU.ColNo)))
                    .SetCellValue(max, sprSyukkaM.HIKIATE_JK.ColNo, "未")
                    .SetCellValue(max, sprSyukkaM.GOODS_COMMENT.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.GOODS_COMMENT.ColNo)))
                    '(2012.12.21)要望番号1710 ロット№追加 -- START --
                    .SetCellValue(max, sprSyukkaM.M_LOT_NO.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.M_LOT_NO.ColNo)))
                    '(2012.12.21)要望番号1710 ロット№追加 --  END  --
                    .SetCellValue(max, sprSyukkaM.SHOBO_NM.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.SHOBO_NM.ColNo)))
                    .SetCellValue(max, sprSyukkaM.SYS_DEL_FLG.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.SYS_DEL_FLG.ColNo)))
                    .SetCellValue(max, sprSyukkaM.REC_NO.ColNo, Convert.ToString(Convert.ToDecimal(_frm.numRecMCnt.Value) + 1))
                    .SetCellValue(max, sprSyukkaM.SEARCH_KEY_1.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.SEARCH_KEY_1.ColNo)))
                    .SetCellValue(max, sprSyukkaM.UNSO_ONDO_KB.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.UNSO_ONDO_KB.ColNo)))
                    .SetCellValue(max, sprSyukkaM.PKG_UT.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.PKG_UT.ColNo)))
                    .SetCellValue(max, sprSyukkaM.STD_IRIME_NB.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.STD_IRIME_NB.ColNo)))
                    .SetCellValue(max, sprSyukkaM.STD_WT_KGS.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.STD_WT_KGS.ColNo)))
                    .SetCellValue(max, sprSyukkaM.TARE_YN.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.TARE_YN.ColNo)))
                    .SetCellValue(max, sprSyukkaM.REMARK.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.REMARK.ColNo)))
                    .SetCellValue(max, sprSyukkaM.REMARK_OUT.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.REMARK_OUT.ColNo)))
                    .SetCellValue(max, sprSyukkaM.TAX_KB.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.TAX_KB.ColNo)))
                    .SetCellValue(max, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.HIKIATE_ALERT_YN.ColNo)))
                    .SetCellValue(max, sprSyukkaM.OUTKA_ATT.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.OUTKA_ATT.ColNo)))
                    .SetCellValue(max, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo)))
                    .SetCellValue(max, sprSyukkaM.CUST_CD_S.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.CUST_CD_S.ColNo)))
                    .SetCellValue(max, sprSyukkaM.CUST_CD_SS.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.CUST_CD_SS.ColNo)))
                    .SetCellValue(max, sprSyukkaM.NB_UT.ColNo, _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.NB_UT.ColNo)))
                    'START YANAI メモ②No.20
                    .SetCellValue(max, sprSyukkaM.EDI_FLG.ColNo, String.Empty)
                    'END YANAI メモ②No.20

                    Me._frm.numRecMCnt.Value = Convert.ToDecimal(_frm.numRecMCnt.Value) + 1


                    '複写行をデータセットに追加
                    Dim insMRows As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).NewRow

                    Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select _
                                        (String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                    insMRows.Item("NRS_BR_CD") = dr(0)("NRS_BR_CD").ToString
                    insMRows.Item("OUTKA_NO_L") = dr(0)("OUTKA_NO_L").ToString
                    insMRows.Item("OUTKA_NO_M") = newMaxOutM
                    insMRows.Item("EDI_SET_NO") = dr(0)("EDI_SET_NO").ToString
                    insMRows.Item("COA_YN") = dr(0)("COA_YN").ToString
                    insMRows.Item("CUST_ORD_NO_DTL") = dr(0)("CUST_ORD_NO_DTL").ToString
                    insMRows.Item("BUYER_ORD_NO_DTL") = dr(0)("BUYER_ORD_NO_DTL").ToString
                    insMRows.Item("GOODS_CD_NRS") = dr(0)("GOODS_CD_NRS").ToString
                    insMRows.Item("RSV_NO") = dr(0)("RSV_NO").ToString
                    insMRows.Item("LOT_NO") = dr(0)("LOT_NO").ToString
                    insMRows.Item("SERIAL_NO") = dr(0)("SERIAL_NO").ToString
                    insMRows.Item("ALCTD_KB") = dr(0)("ALCTD_KB").ToString
                    insMRows.Item("OUTKA_PKG_NB") = dr(0)("OUTKA_PKG_NB").ToString
                    insMRows.Item("OUTKA_HASU") = dr(0)("OUTKA_HASU").ToString
                    insMRows.Item("OUTKA_QT") = dr(0)("OUTKA_QT").ToString
                    insMRows.Item("OUTKA_TTL_NB") = dr(0)("OUTKA_TTL_NB").ToString
                    insMRows.Item("OUTKA_TTL_QT") = dr(0)("OUTKA_TTL_QT").ToString
                    insMRows.Item("ALCTD_NB") = "0"
                    insMRows.Item("ALCTD_QT") = "0"
                    insMRows.Item("BACKLOG_NB") = dr(0)("BACKLOG_NB").ToString
                    insMRows.Item("BACKLOG_QT") = dr(0)("BACKLOG_QT").ToString
                    insMRows.Item("UNSO_ONDO_KB") = dr(0)("UNSO_ONDO_KB").ToString
                    insMRows.Item("IRIME") = dr(0)("IRIME").ToString
                    insMRows.Item("IRIME_UT") = dr(0)("IRIME_UT").ToString
                    insMRows.Item("OUTKA_M_PKG_NB") = dr(0)("OUTKA_M_PKG_NB").ToString
                    insMRows.Item("REMARK") = dr(0)("REMARK").ToString
                    insMRows.Item("SIZE_KB") = dr(0)("SIZE_KB").ToString
                    insMRows.Item("ZAIKO_KB") = dr(0)("ZAIKO_KB").ToString
                    insMRows.Item("SOURCE_CD") = dr(0)("SOURCE_CD").ToString
                    insMRows.Item("YELLOW_CARD") = dr(0)("YELLOW_CARD").ToString
                    insMRows.Item("PRINT_SORT") = dr(0)("PRINT_SORT").ToString
                    insMRows.Item("SUM_OUTKA_TTL_NB") = dr(0)("SUM_OUTKA_TTL_NB").ToString
                    insMRows.Item("GOODS_CD_CUST") = dr(0)("GOODS_CD_CUST").ToString
                    insMRows.Item("GOODS_NM") = dr(0)("GOODS_NM").ToString
                    insMRows.Item("HIKIATE") = dr(0)("HIKIATE").ToString
                    insMRows.Item("ALCTD_KB_NM") = dr(0)("ALCTD_KB_NM").ToString
                    insMRows.Item("NB_UT_NM") = dr(0)("NB_UT_NM").ToString
                    insMRows.Item("QT_UT_NM") = dr(0)("QT_UT_NM").ToString
                    insMRows.Item("PKG_NB") = dr(0)("PKG_NB").ToString
                    insMRows.Item("SEARCH_KEY_1") = dr(0)("SEARCH_KEY_1").ToString
                    insMRows.Item("UNSO_ONDO_KB") = dr(0)("UNSO_ONDO_KB").ToString
                    insMRows.Item("PKG_UT") = dr(0)("PKG_UT").ToString
                    insMRows.Item("STD_IRIME_NB") = dr(0)("STD_IRIME_NB").ToString
                    insMRows.Item("STD_WT_KGS") = dr(0)("STD_WT_KGS").ToString
                    insMRows.Item("TARE_YN") = dr(0)("TARE_YN").ToString
                    insMRows.Item("REMARK") = dr(0)("REMARK").ToString
                    insMRows.Item("REMARK_OUT") = dr(0)("REMARK_OUT").ToString
                    insMRows.Item("TAX_KB") = dr(0)("TAX_KB").ToString
                    insMRows.Item("HIKIATE_ALERT_YN") = dr(0)("HIKIATE_ALERT_YN").ToString
                    insMRows.Item("OUTKA_ATT") = dr(0)("OUTKA_ATT").ToString
                    insMRows.Item("UNSO_WT_M") = dr(0)("UNSO_WT_M").ToString
                    insMRows.Item("GOODS_CD_NRS_FROM") = dr(0)("GOODS_CD_NRS_FROM").ToString
                    insMRows.Item("NB_UT") = dr(0)("NB_UT").ToString
                    insMRows.Item("CUST_CD_S") = dr(0)("CUST_CD_S").ToString
                    insMRows.Item("CUST_CD_SS") = dr(0)("CUST_CD_SS").ToString
                    'START YANAI メモ②No.20
                    insMRows.Item("EDI_FLG") = String.Empty
                    'END YANAI メモ②No.20
                    'START YANAI 要望番号499
                    insMRows.Item("CUST_CD_L_GOODS") = dr(0)("CUST_CD_L_GOODS").ToString
                    insMRows.Item("CUST_CD_M_GOODS") = dr(0)("CUST_CD_M_GOODS").ToString
                    'END YANAI 要望番号499
                    insMRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    insMRows.Item("UP_KBN") = "0"
                    insMRows.Item("SHOBO_CD") = dr(0)("SHOBO_CD").ToString
                    insMRows.Item("SHOBO_NM") = dr(0)("SHOBO_NM").ToString
                    'データセットに追加
                    ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Add(insMRows)

                    Exit For
                End If
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドのデータを複写(作業(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopySagyoM(ByVal spr As LMSpread, ByVal ds As DataSet)

        Dim dr As DataRow() = Nothing
        Dim gMax As Integer = 0
        Dim sagyoCd As String = String.Empty
        Dim sagyoNm As DataRow() = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim custCd As String = String.Empty

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            nrsBrCd = Convert.ToString(Me._frm.cmbEigyosyo.SelectedValue)
            custCd = Me._frm.txtCust_Cd_L.TextValue

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then

                    '作業コード取得（届先付帯作業ではない方）
                    dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "INOUTKA_NO_LM = '", Me._frm.lblSyukkaLNo.TextValue, _
                                                                                _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                , "DEST_SAGYO_FLG = '00'"))
                    gMax = dr.Length - 1

                    For j As Integer = 0 To gMax
                        sagyoCd = dr(j).Item("SAGYO_CD").ToString
                        'START YANAI 要望番号376
                        'sagyoNm = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                        '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                        '                                                                               , "CUST_CD_L = '", custCd, "'"))
                        sagyoNm = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                       , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                       , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                        'END YANAI 要望番号376

                        Select Case j
                            Case 0
                                '1レコード目
                                Me._frm.txtSagyoM1.TextValue = sagyoCd
                                Me._frm.lblSagyoM1.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtSagyoRemarkM1.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                            Case 1
                                '2レコード目
                                Me._frm.txtSagyoM2.TextValue = sagyoCd
                                Me._frm.lblSagyoM2.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtSagyoRemarkM2.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                            Case 2
                                '3レコード目
                                Me._frm.txtSagyoM3.TextValue = sagyoCd
                                Me._frm.lblSagyoM3.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtSagyoRemarkM3.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                            Case 3
                                '4レコード目
                                Me._frm.txtSagyoM4.TextValue = sagyoCd
                                Me._frm.lblSagyoM4.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtSagyoRemarkM4.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                            Case 4
                                '5レコード目
                                Me._frm.txtSagyoM5.TextValue = sagyoCd
                                Me._frm.lblSagyoM5.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtSagyoRemarkM5.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                        End Select

                    Next


                    '作業コード取得（届先付帯作業）
                    dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "INOUTKA_NO_LM = '", Me._frm.lblSyukkaLNo.TextValue, _
                                                                                _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                , "DEST_SAGYO_FLG = '01'"))
                    gMax = dr.Length - 1

                    For j As Integer = 0 To gMax
                        sagyoCd = dr(j).Item("SAGYO_CD").ToString
                        'START YANAI 要望番号376
                        'sagyoNm = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                        '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                        '                                                                               , "CUST_CD_L = '", custCd, "'"))
                        sagyoNm = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                       , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                       , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                        'END YANAI 要望番号376

                        Select Case j
                            Case 0
                                '1レコード目
                                Me._frm.txtDestSagyoM1.TextValue = sagyoCd
                                Me._frm.lblDestSagyoM1.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtDestSagyoRemarkM1.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                            Case 1
                                '2レコード目
                                Me._frm.txtDestSagyoM2.TextValue = sagyoCd
                                Me._frm.lblDestSagyoM2.TextValue = sagyoNm(0).Item("SAGYO_RYAK").ToString()
                                Me._frm.txtDestSagyoRemarkM2.TextValue = sagyoNm(0).Item("WH_SAGYO_REMARK").ToString()
                        End Select

                    Next

                    Exit For

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを追加(出荷(小))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddOutkaS(ByVal spr As LMSpread, ByVal dt As DataTable, ByVal ds As DataSet)

        Dim dtOut As New DataSet
        With spr

            .SuspendLayout()

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow
            Dim max As Integer = dt.Rows.Count - 1
            Dim row As Integer = 0
            Dim updFlg As Boolean = False

            Dim zaiRow As DataRow() = Nothing
            Dim outSRow As DataRow() = Nothing
            Dim outSmax As Integer = 0

            If -1 < max Then
                row = .Sheets(0).Rows.Count - 1
                For J As Integer = 0 To row
                    'フラグの初期化
                    .SetCellValue(J, sprDtl.UPDATE_FLG.ColNo, LMConst.FLG.OFF)   '更新判定用にオフにする
                Next
                For i As Integer = 0 To max
                    dRow = dt.Rows(i)
                    zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                  "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO"), "'"))
                    If zaiRow.Length = 0 Then
                        Exit For
                    End If
                    zaiRow(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    For J As Integer = 0 To row
                        If dRow.Item("ZAI_REC_NO").ToString().Equals(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(J, sprDtl.ZAI_REC_NO.ColNo))) = True Then
                            '同じレコード有の場合
                            .SetCellValue(J, sprDtl.UPDATE_FLG.ColNo, LMConst.FLG.ON)   '更新判定用にオンにする

                            Exit For
                        End If
                    Next
                Next

                row = .Sheets(0).Rows.Count - 1
                For i As Integer = 0 To row
                    If (LMConst.FLG.OFF).Equals(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.UPDATE_FLG.ColNo))) = True Then
                        '出荷(小)データセットから削除
                        outSRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "OUTKA_NO_S = '", _LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo)), "'"))
                        If ("0").Equals(outSRow(0).Item("UP_KBN")) = True Then
                            ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Remove(outSRow(0))
                        Else
                            outSRow(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                        End If

                        '在庫データセットの個数・数量戻し
                        zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                      "ZAI_REC_NO = '", _LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "'"))

                        zaiRow(0).Item("ALCTD_NB") = Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                        zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                        zaiRow(0).Item("ALCTD_QT") = Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
                        zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))

                        '出荷小データセットの個数・数量戻し
                        outSRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "ZAI_REC_NO = '", _LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND " _
                                                                                       , "UP_KBN = '0'"))
                        outSmax = outSRow.Length - 1
                        For j As Integer = 0 To outSmax
                            outSRow(j).Item("ALCTD_CAN_NB") = Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_NB")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                            outSRow(j).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_NB_GAMEN")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                            outSRow(j).Item("ALLOC_CAN_QT") = Convert.ToDecimal(outSRow(j).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
                            outSRow(j).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToDecimal(outSRow(j).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
                        Next

                        'スプレッドからクリア
                        .ActiveSheet.RemoveRows(i, 1)

                        'START YANAI 要望番号461
                        'i = i - 1
                        'END YANAI 要望番号461

                        row = .Sheets(0).Rows.Count - 1
                        If i >= row Then
                            Exit For
                        End If

                        'START YANAI 要望番号461
                        i = i - 1
                        'END YANAI 要望番号461

                    End If
                Next

            End If

            For i As Integer = 0 To max
                '値設定
                dRow = dt.Rows(i)
                row = .Sheets(0).Rows.Count

                updFlg = False
                For J As Integer = 0 To row - 1
                    If dRow.Item("ZAI_REC_NO").ToString().Equals(_LMCconG.GetCellValue(_frm.sprDtl.ActiveSheet.Cells(J, sprDtl.ZAI_REC_NO.ColNo))) = True Then
                        '同じ在庫レコード番号の行があったら、上書きする
                        row = J
                        updFlg = True

                        Exit For
                    End If
                Next

                zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                              "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "'"))

                If updFlg = False Then
                    .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

                    'セルスタイル設定
                    .SetCellStyle(row, sprDtl.DEF.ColNo, sDEF)
                    .SetCellStyle(row, sprDtl.LOT_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
                    .SetCellStyle(row, sprDtl.TOU_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SHITSU_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ZONE_CD.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.LOCA.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(row, sprDtl.ALCTD_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(row, sprDtl.ALCTD_CAN_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(row, sprDtl.ALCTD_CAN_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
                    .SetCellStyle(row, sprDtl.NAKAMI.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.GAIKAN.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.JOTAI_NM.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.REMARK.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.INKO_DATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.LT_DATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.HORYUHIN.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.BOGAIHIN.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.RSV_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SERIAL_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.GOODS_CRT_DATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.WARIATE_NM.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.REMARK_OUT.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SHO_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ZAI_REC_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.INKA_NO_L.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.INKA_NO_M.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.INKA_NO_S.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SMPL_FLAG.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.GOODS_COND_KB_1.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.GOODS_COND_KB_2.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.GOODS_COND_KB_3.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.OFB_KB.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SPD_KB.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.PORA_ZAI_NB.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.PORA_ZAI_QT.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALLOC_CAN_NB_HOZON.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALLOC_CAN_QT_HOZON.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALLOC_CAN_NB.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALLOC_CAN_QT.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SYS_DEL_FLG.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.REC_NO.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.HOKAN_YN.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.TAX_KB.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.INKO_PLAN_DATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.WARIATE.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_NB_HOZON.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_QT_HOZON.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.DEST_CD_P.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.UPDATE_FLG.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_CAN_NB_MATOME.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_CAN_QT_MATOME.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_NB_MATOME.ColNo, sLabel)
                    .SetCellStyle(row, sprDtl.ALCTD_QT_MATOME.ColNo, sLabel)

                End If

                'セルに値を設定
                .SetCellValue(row, sprDtl.DEF.ColNo, LMC020C.FLG_FALSE)
                .SetCellValue(row, sprDtl.LOT_NO.ColNo, zaiRow(0).Item("LOT_NO").ToString())
                .SetCellValue(row, sprDtl.IRIME.ColNo, zaiRow(0).Item("IRIME").ToString())
                .SetCellValue(row, sprDtl.TOU_NO.ColNo, zaiRow(0).Item("TOU_NO").ToString())
                .SetCellValue(row, sprDtl.SHITSU_NO.ColNo, zaiRow(0).Item("SITU_NO").ToString())
                .SetCellValue(row, sprDtl.ZONE_CD.ColNo, zaiRow(0).Item("ZONE_CD").ToString())
                .SetCellValue(row, sprDtl.LOCA.ColNo, zaiRow(0).Item("LOCA").ToString())
                If ("03").Equals(dRow.Item("ALCTD_KB")) = True Then
                    '小分けの場合
                    .SetCellValue(row, sprDtl.ALCTD_NB.ColNo, "0")
                Else
                    '小分け以外の場合
                    .SetCellValue(row, sprDtl.ALCTD_NB.ColNo, Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_NB").ToString()) - Convert.ToDecimal(dRow.Item("ALCTD_NB_HOZON").ToString())))
                End If
                .SetCellValue(row, sprDtl.ALCTD_QT.ColNo, Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_QT").ToString()) - Convert.ToDecimal(dRow.Item("ALCTD_QT_HOZON").ToString())))
                .SetCellValue(row, sprDtl.ALCTD_CAN_NB.ColNo, zaiRow(0).Item("ALLOC_CAN_NB_GAMEN").ToString())
                .SetCellValue(row, sprDtl.ALCTD_CAN_QT.ColNo, zaiRow(0).Item("ALLOC_CAN_QT_GAMEN").ToString())
                .SetCellValue(row, sprDtl.NAKAMI.ColNo, zaiRow(0).Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(row, sprDtl.GAIKAN.ColNo, zaiRow(0).Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(row, sprDtl.JOTAI_NM.ColNo, zaiRow(0).Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(row, sprDtl.REMARK.ColNo, zaiRow(0).Item("REMARK").ToString())
                .SetCellValue(row, sprDtl.INKO_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiRow(0).Item("INKO_DATE").ToString()))
                .SetCellValue(row, sprDtl.LT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiRow(0).Item("LT_DATE").ToString()))
                .SetCellValue(row, sprDtl.HORYUHIN.ColNo, zaiRow(0).Item("SPD_KB_NM").ToString())
                .SetCellValue(row, sprDtl.BOGAIHIN.ColNo, zaiRow(0).Item("OFB_KB_NM").ToString())
                .SetCellValue(row, sprDtl.RSV_NO.ColNo, zaiRow(0).Item("RSV_NO").ToString())
                .SetCellValue(row, sprDtl.SERIAL_NO.ColNo, zaiRow(0).Item("SERIAL_NO").ToString())
                .SetCellValue(row, sprDtl.GOODS_CRT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(zaiRow(0).Item("GOODS_CRT_DATE").ToString()))
                .SetCellValue(row, sprDtl.WARIATE_NM.ColNo, zaiRow(0).Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(row, sprDtl.REMARK_OUT.ColNo, zaiRow(0).Item("REMARK_OUT").ToString())
                If updFlg = False Then
                    .SetCellValue(row, sprDtl.SHO_NO.ColNo, String.Empty)
                End If
                .SetCellValue(row, sprDtl.ZAI_REC_NO.ColNo, zaiRow(0).Item("ZAI_REC_NO").ToString())
                .SetCellValue(row, sprDtl.INKA_NO_L.ColNo, zaiRow(0).Item("INKA_NO_L").ToString())
                .SetCellValue(row, sprDtl.INKA_NO_M.ColNo, zaiRow(0).Item("INKA_NO_M").ToString())
                .SetCellValue(row, sprDtl.INKA_NO_S.ColNo, zaiRow(0).Item("INKA_NO_S").ToString())
                If ("03").Equals(dRow.Item("ALCTD_KB")) = True Then
                    '在庫の場合
                    .SetCellValue(row, sprDtl.SMPL_FLAG.ColNo, "01")
                Else
                    '在庫以外の場合
                    .SetCellValue(row, sprDtl.SMPL_FLAG.ColNo, "00")
                End If
                .SetCellValue(row, sprDtl.GOODS_COND_KB_1.ColNo, zaiRow(0).Item("GOODS_COND_KB_1").ToString())
                .SetCellValue(row, sprDtl.GOODS_COND_KB_2.ColNo, zaiRow(0).Item("GOODS_COND_KB_2").ToString())
                .SetCellValue(row, sprDtl.GOODS_COND_KB_3.ColNo, zaiRow(0).Item("GOODS_COND_KB_3").ToString())
                .SetCellValue(row, sprDtl.NAKAMI.ColNo, zaiRow(0).Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(row, sprDtl.GAIKAN.ColNo, zaiRow(0).Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(row, sprDtl.JOTAI_NM.ColNo, zaiRow(0).Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(row, sprDtl.OFB_KB.ColNo, zaiRow(0).Item("OFB_KB").ToString())
                .SetCellValue(row, sprDtl.SPD_KB.ColNo, zaiRow(0).Item("SPD_KB").ToString())
                .SetCellValue(row, sprDtl.PORA_ZAI_NB.ColNo, zaiRow(0).Item("PORA_ZAI_NB").ToString())
                .SetCellValue(row, sprDtl.PORA_ZAI_QT.ColNo, zaiRow(0).Item("PORA_ZAI_QT").ToString())
                .SetCellValue(row, sprDtl.ALLOC_CAN_NB_HOZON.ColNo, zaiRow(0).Item("ALLOC_CAN_NB_HOZON").ToString())
                .SetCellValue(row, sprDtl.ALLOC_CAN_QT_HOZON.ColNo, zaiRow(0).Item("ALLOC_CAN_QT_HOZON").ToString())

                'START YANAI 要望番号853 まとめ処理対応
                '.SetCellValue(row, sprDtl.ALLOC_CAN_NB.ColNo, zaiRow(0).Item("ALLOC_CAN_NB").ToString())
                '.SetCellValue(row, sprDtl.ALLOC_CAN_QT.ColNo, zaiRow(0).Item("ALLOC_CAN_QT").ToString())
                If ("01").Equals(dRow.Item("ALCTD_KB")) = True OrElse _
                    ("02").Equals(dRow.Item("ALCTD_KB")) = True Then
                    .SetCellValue(row, sprDtl.ALLOC_CAN_NB.ColNo, zaiRow(0).Item("ALLOC_CAN_NB_GAMEN").ToString())
                    .SetCellValue(row, sprDtl.ALLOC_CAN_QT.ColNo, zaiRow(0).Item("ALLOC_CAN_QT_GAMEN").ToString())
                Else
                    .SetCellValue(row, sprDtl.ALLOC_CAN_NB.ColNo, zaiRow(0).Item("ALLOC_CAN_NB").ToString())
                    .SetCellValue(row, sprDtl.ALLOC_CAN_QT.ColNo, zaiRow(0).Item("ALLOC_CAN_QT").ToString())
                End If
                'END YANAI 要望番号853 まとめ処理対応

                .SetCellValue(row, sprDtl.SYS_DEL_FLG.ColNo, LMConst.FLG.OFF)
                .SetCellValue(row, sprDtl.SYS_UPD_DATE.ColNo, zaiRow(0).Item("SYS_UPD_DATE").ToString())
                .SetCellValue(row, sprDtl.SYS_UPD_TIME.ColNo, zaiRow(0).Item("SYS_UPD_TIME").ToString())
                .SetCellValue(row, sprDtl.HOKAN_YN.ColNo, zaiRow(0).Item("HOKAN_YN").ToString())
                .SetCellValue(row, sprDtl.TAX_KB.ColNo, zaiRow(0).Item("TAX_KB").ToString())
                .SetCellValue(row, sprDtl.INKO_PLAN_DATE.ColNo, zaiRow(0).Item("INKO_PLAN_DATE").ToString())
                .SetCellValue(row, sprDtl.WARIATE.ColNo, zaiRow(0).Item("ALLOC_PRIORITY").ToString())
                .SetCellValue(row, sprDtl.ALCTD_NB_HOZON.ColNo, zaiRow(0).Item("ALCTD_NB_HOZON").ToString())
                .SetCellValue(row, sprDtl.ALCTD_QT_HOZON.ColNo, zaiRow(0).Item("ALCTD_QT_HOZON").ToString())
                .SetCellValue(row, sprDtl.DEST_CD_P.ColNo, zaiRow(0).Item("DEST_CD_P").ToString())
                .SetCellValue(row, sprDtl.UPDATE_FLG.ColNo, LMConst.FLG.ON)
                If updFlg = False Then
                    .SetCellValue(row, sprDtl.REC_NO.ColNo, Convert.ToString(i))
                    Me._frm.numRecSCnt.Value = Convert.ToDecimal(_frm.numRecSCnt.Value) + 1
                End If
                .SetCellValue(row, sprDtl.ALCTD_CAN_NB_MATOME.ColNo, zaiRow(0).Item("ALLOC_CAN_NB_GAMEN").ToString())
                .SetCellValue(row, sprDtl.ALCTD_CAN_QT_MATOME.ColNo, zaiRow(0).Item("ALLOC_CAN_QT_GAMEN").ToString())
                .SetCellValue(row, sprDtl.ALCTD_NB_MATOME.ColNo, Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_NB").ToString()) - Convert.ToDecimal(dRow.Item("ALCTD_NB_HOZON").ToString())))
                .SetCellValue(row, sprDtl.ALCTD_QT_MATOME.ColNo, Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_QT").ToString()) - Convert.ToDecimal(dRow.Item("ALCTD_QT_HOZON").ToString())))

            Next

            .ResumeLayout(True)

        End With

        '現場作業チェックの要否確認
        SetChkTabletOnChangeOutS(ds, dt)

    End Sub

    ''' <summary>
    ''' 出荷(中)スプレッドの中の管理番号のRowIndexを返却
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetOutkaNoM(ByVal outkaNoM As String) As Integer

        Dim max As Integer = Me._frm.sprSyukkaM.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            If outkaNoM.Equals(Me._LMCconG.GetCellValue(Me._frm.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo))) = True Then
                Return i
            End If
        Next

        Return -1

    End Function

    ''' <summary>
    ''' 引当状況の更新
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ChangeHikiate(ByVal cnt As Integer)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '2017/09/25 修正 李↓
        If 0 < cnt Then
            Me._frm.lblHikiate.TextValue = lgm.Selector({"済", "Fin", "끝남", "中国語"})
        Else
            Me._frm.lblHikiate.TextValue = lgm.Selector({"未", "Yet", "미(未)", "中国語"})
        End If
        '2017/09/25 修正 李↑

    End Sub

    '要望対応:1595 yamanaka 2012.11.15 Start
    ''' <summary>
    ''' スプレッドのデータを変更(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpOutkaMspread(ByVal spr As LMSpread, ByVal dt As DataTable, Optional ByVal likeFlg As Boolean = False)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dRow As DataRow

            '値設定
            dRow = dt.Rows(0)

            Dim row As Integer = .Sheets(0).ActiveRow.Index
            'セルスタイル設定
            .SetCellStyle(row, sprSyukkaM.DEFM.ColNo, sDEF)
            .SetCellStyle(row, sprSyukkaM.PRT_ORDER.ColNo, nLabel)
            .SetCellStyle(row, sprSyukkaM.SHOBO_CD.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.KANRI_NO.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_CD.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_NM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SYUKKA_TANI.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ","))
            .SetCellStyle(row, sprSyukkaM.NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
            .SetCellStyle(row, sprSyukkaM.ALL_SURYO.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ","))
            .SetCellStyle(row, sprSyukkaM.ZANSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ","))
            .SetCellStyle(row, sprSyukkaM.HIKIATE_JK.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_COMMENT.ColNo, sLabel)
            '(2012.12.21)要望番号1710 ロット№追加 -- START --
            .SetCellStyle(row, sprSyukkaM.M_LOT_NO.ColNo, sLabel)
            '(2012.12.21)要望番号1710 ロット№追加 --  END  --
            .SetCellStyle(row, sprSyukkaM.SHOBO_NM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SYS_DEL_FLG.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REC_NO.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.SEARCH_KEY_1.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.UNSO_ONDO_KB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.PKG_UT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.STD_IRIME_NB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.STD_WT_KGS.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.TARE_YN.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REMARK.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.REMARK_OUT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.TAX_KB.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.OUTKA_ATT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.CUST_CD_S.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.CUST_CD_SS.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.NB_UT.ColNo, sLabel)
            .SetCellStyle(row, sprSyukkaM.EDI_FLG.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, sprSyukkaM.DEFM.ColNo, LMC020C.FLG_FALSE)
            .SetCellValue(row, sprSyukkaM.PRT_ORDER.ColNo, Me._frm.numPrintSort.TextValue)
            .SetCellValue(row, sprSyukkaM.SHOBO_CD.ColNo, dRow.Item("SHOBO_CD").ToString())
            .SetCellValue(row, sprSyukkaM.KANRI_NO.ColNo, Me._frm.lblSyukkaMNo.TextValue)
            .SetCellValue(row, sprSyukkaM.GOODS_CD.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
            .SetCellValue(row, sprSyukkaM.GOODS_NM.ColNo, dRow.Item("NM_1").ToString())
            If Me._frm.optCnt.Checked = True Then
                .SetCellValue(row, sprSyukkaM.SYUKKA_TANI.ColNo, Me._frm.optCnt.Text)
            ElseIf Me._frm.optAmt.Checked = True Then
                .SetCellValue(row, sprSyukkaM.SYUKKA_TANI.ColNo, Me._frm.optAmt.Text)
            ElseIf Me._frm.optKowake.Checked = True Then
                .SetCellValue(row, sprSyukkaM.SYUKKA_TANI.ColNo, Me._frm.optKowake.Text)
            ElseIf Me._frm.optSample.Checked = True Then
                .SetCellValue(row, sprSyukkaM.SYUKKA_TANI.ColNo, Me._frm.optSample.Text)
            End If
            .SetCellValue(row, sprSyukkaM.IRIME.ColNo, dRow.Item("IRIME").ToString())
            If likeFlg = False Then
                .SetCellValue(row, sprSyukkaM.NB.ColNo, String.Empty)
                .SetCellValue(row, sprSyukkaM.ALL_SURYO.ColNo, String.Empty)
                .SetCellValue(row, sprSyukkaM.ZANSU.ColNo, String.Empty)
            Else
                .SetCellValue(row, sprSyukkaM.NB.ColNo, dRow.Item("KOSU").ToString())
                .SetCellValue(row, sprSyukkaM.ALL_SURYO.ColNo, dRow.Item("SURYO").ToString())
                .SetCellValue(row, sprSyukkaM.ZANSU.ColNo, Convert.ToDecimal(Me._frm.numSouKosu.TextValue) - Convert.ToDecimal(dRow.Item("KOSU").ToString()))
            End If
            .SetCellValue(row, sprSyukkaM.HIKIATE_JK.ColNo, Me._frm.lblHikiate.TextValue)
            If String.IsNullOrEmpty(Me._frm.txtGoodsRemark.TextValue) = True Then
                .SetCellValue(row, sprSyukkaM.GOODS_COMMENT.ColNo, dRow.Item("OUTKA_ATT").ToString)
            End If
            '(2012.12.21)要望番号1710 ロット№追加 -- START --
            .SetCellValue(row, sprSyukkaM.M_LOT_NO.ColNo, Me._frm.txtLotNo.TextValue)
            '(2012.12.21)要望番号1710 ロット№追加 --  END --
            .SetCellValue(row, sprSyukkaM.SHOBO_NM.ColNo, dRow.Item("SHOBO_NM").ToString())
            .SetCellValue(row, sprSyukkaM.SYS_DEL_FLG.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, sprSyukkaM.REC_NO.ColNo, _LMCconG.GetCellValue(.ActiveSheet.Cells(row, sprSyukkaM.REC_NO.ColNo)))
            .SetCellValue(row, sprSyukkaM.SEARCH_KEY_1.ColNo, dRow.Item("SEARCH_KEY_1").ToString())
            .SetCellValue(row, sprSyukkaM.UNSO_ONDO_KB.ColNo, dRow.Item("UNSO_ONDO_KB").ToString())
            .SetCellValue(row, sprSyukkaM.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString())
            .SetCellValue(row, sprSyukkaM.STD_IRIME_NB.ColNo, dRow.Item("STD_IRIME_NB").ToString())
            .SetCellValue(row, sprSyukkaM.STD_WT_KGS.ColNo, dRow.Item("STD_WT_KGS").ToString())
            .SetCellValue(row, sprSyukkaM.TARE_YN.ColNo, dRow.Item("TARE_YN").ToString())
            .SetCellValue(row, sprSyukkaM.REMARK.ColNo, dRow.Item("REMARK").ToString())
            .SetCellValue(row, sprSyukkaM.REMARK_OUT.ColNo, dRow.Item("REMARK_OUT").ToString())
            .SetCellValue(row, sprSyukkaM.TAX_KB.ColNo, dRow.Item("TAX_KB").ToString())
            .SetCellValue(row, sprSyukkaM.HIKIATE_ALERT_YN.ColNo, dRow.Item("HIKIATE_ALERT_YN").ToString())
            .SetCellValue(row, sprSyukkaM.OUTKA_ATT.ColNo, dRow.Item("OUTKA_ATT").ToString())
            .SetCellValue(row, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo, dRow.Item("GOODS_CD_NRS_FROM").ToString())
            .SetCellValue(row, sprSyukkaM.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString())
            .SetCellValue(row, sprSyukkaM.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString())
            .SetCellValue(row, sprSyukkaM.NB_UT.ColNo, dRow.Item("NB_UT").ToString())
            .SetCellValue(row, sprSyukkaM.EDI_FLG.ColNo, String.Empty)

            .ResumeLayout(True)

        End With



    End Sub

    ''' <summary>
    ''' 詳細のデータを変更(出荷(中))
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpOutkaM(ByVal dt As DataTable, ByVal ds As DataSet)

        '値設定
        Dim dRow As DataRow = dt.Rows(0)

        '区分マスタキャッシュ取得（入目の単位名称を取得）
        Dim irimeKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'I001' AND ", _
                                                                                                           "KBN_CD ='", dRow.Item("STD_IRIME_UT").ToString, "'"))

        With Me._frm

            '出荷(中)の各項目に設定
            .lblHikiate.TextValue = "未"
            .txtGoodsCdCust.TextValue = dRow.Item("GOODS_CD_CUST").ToString
            .lblGoodsNm.TextValue = dRow.Item("NM_1").ToString
            .txtLotNo.TextValue = dRow.Item("LOT_NO").ToString()
            .numIrime.Value = Me._LMCconG.FormatNumValue(dRow.Item("IRIME").ToString)

#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
            .lblIrimeUT.TextValue = irimeKbn(0).Item("KBN_NM1").ToString
            .lblIrimeUThide.TextValue = dRow.Item("STD_IRIME_UT").ToString
#Else
            .lblIrimeUT.KbnValue = irimeKbn(0).Item("KBN_CD").ToString
#End If
            .numPkgCnt.TextValue = String.Empty
            .cmbUnsoOndo.SelectedValue = dRow.Item("UNSO_ONDO_KB").ToString
            .cmbTakkyuSize.SelectedValue = dRow.Item("SIZE_KB").ToString
            .numKonsu.Value = 0

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblKonsuUT.TextValue = dRow.Item("NB_UT_NM").ToString
#Else
            .lblKonsuUT.KbnValue = dRow.Item("NB_UT").ToString
#End If
            .numIrisu.Value = Me._LMCconG.FormatNumValue(dRow.Item("PKG_NB").ToString)
            .numSouKosu.Value = 0
            .numHasu.Value = 0

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblHasuUT.TextValue = dRow.Item("NB_UT_NM").ToString
#Else
            .lblHasuUT.KbnValue = dRow.Item("NB_UT").ToString
#End If
            .numHikiateKosuSumi.Value = 0
            .numHikiateKosuZan.Value = 0
            .numSouSuryo.Value = 0

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .lblSuryouUT.TextValue = dRow.Item("IRIME_UT_NM").ToString
#Else
            .lblSuryouUT.KbnValue = dRow.Item("IRIME_UT").ToString
#End If
            .numHikiateSuryoSumi.Value = 0
            .numHikiateSuryoZan.Value = 0
            .txtGoodsRemark.TextValue = dRow.Item("OUTKA_ATT").ToString

            .lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS").ToString
            .lblGoodsCdNrsFrom.TextValue = dRow.Item("GOODS_CD_NRS_FROM").ToString
            .lblTareYn.TextValue = dRow.Item("TARE_YN").ToString
            .lblStdIrimeNb.TextValue = dRow.Item("STD_IRIME_NB").ToString
            .lblStdWtKgs.TextValue = dRow.Item("STD_WT_KGS").ToString
            .lblRemark.TextValue = dRow.Item("REMARK").ToString
            .lblRemarkOut.TextValue = dRow.Item("REMARK_OUT").ToString
            .lblTaxKb.TextValue = dRow.Item("TAX_KB").ToString
            .lblHikiateAlertYn.TextValue = dRow.Item("HIKIATE_ALERT_YN").ToString
            .lblCustCdS.TextValue = dRow.Item("CUST_CD_S").ToString
            .lblCustCdSS.TextValue = dRow.Item("CUST_CD_SS").ToString
            .lblCustCdL.TextValue = dRow.Item("CUST_CD_L_GOODS").ToString
            .lblCustCdM.TextValue = dRow.Item("CUST_CD_M_GOODS").ToString

            '要望番号1988 START 削除
            '要望番号1959 START
            '.lblEdiOutkaTtlNb.Value = Me._LMCconG.FormatNumValue(dRow.Item("EDI_OUTKA_TTL_NB").ToString)
            '.lblEdiOutkaTtlQt.Value = Me._LMCconG.FormatNumValue(dRow.Item("EDI_OUTKA_TTL_QT").ToString)
            '要望番号1959 END
            '要望番号1988 END

            Dim sagyoDr As DataRow() = Nothing

            '作業1
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString) = False Then
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                If 0 < sagyoDr.Length Then
                    .txtSagyoM1.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_1").ToString
                    .lblSagyoM1.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM1.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業2
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString) = False Then
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                If 0 < sagyoDr.Length Then
                    .txtSagyoM2.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_2").ToString
                    .lblSagyoM2.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM2.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業3
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString) = False Then
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                If 0 < sagyoDr.Length Then
                    .txtSagyoM3.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_3").ToString
                    .lblSagyoM3.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM3.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業4
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString) = False Then
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                If 0 < sagyoDr.Length Then
                    .txtSagyoM4.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_4").ToString
                    .lblSagyoM4.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM4.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            '作業5
            If String.IsNullOrEmpty(dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString) = False Then
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString, "' AND " _
                                                                                   , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                If 0 < sagyoDr.Length Then
                    .txtSagyoM5.TextValue = dRow.Item("OUTKA_KAKO_SAGYO_KB_5").ToString
                    .lblSagyoM5.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    .txtSagyoRemarkM5.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                End If
            End If

            Dim destGoodsDr As DataRow() = Nothing
            If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = False Then
                destGoodsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DESTGOODS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                   , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                   , "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND " _
                                                                   , "CD = '", .txtTodokesakiCd.TextValue, "' AND " _
                                                                   , "GOODS_CD_NRS = '", dRow.Item("GOODS_CD_NRS").ToString, "'"))
                If 0 < destGoodsDr.Length Then
                    '届先作業1
                    .txtDestSagyoM1.TextValue = destGoodsDr(0).Item("SAGYO_KB_1").ToString
                    sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_1").ToString, "' AND " _
                                                                                       , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                    If 0 < sagyoDr.Length Then
                        .lblDestSagyoM1.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                        .txtDestSagyoRemarkM1.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                    End If

                    '届先作業2
                    .txtDestSagyoM2.TextValue = destGoodsDr(0).Item("SAGYO_KB_2").ToString
                    sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                       , "SAGYO_CD = '", destGoodsDr(0).Item("SAGYO_KB_2").ToString, "' AND " _
                                                                                       , "(CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))
                    If 0 < sagyoDr.Length Then
                        .lblDestSagyoM2.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString
                        .txtDestSagyoRemarkM2.TextValue = sagyoDr(0).Item("WH_SAGYO_REMARK").ToString
                    End If

                End If

            End If

        End With

    End Sub
    '要望対応:1595 yamanaka 2012.11.15 End

    ''' <summary>
    ''' 印順の一括変更
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetPrintSort(ByVal ds As DataSet) As Boolean

        With Me._frm

            Dim chkList As ArrayList = Me._V.getCheckList(.sprSyukkaM)
            Dim max As Integer = chkList.Count() - 1
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
            Dim userNrCd As String = .cmbEigyosyo.SelectedValue.ToString()
            Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)
            Dim dr As DataRow() = Nothing

            For i As Integer = 0 To max
                .sprSyukkaM.SetCellValue(Convert.ToInt32(chkList(i)), sprSyukkaM.PRT_ORDER.ColNo, Convert.ToString(.numPrintSortHenko.Value))

                dr = dt.Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                             "OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(Convert.ToInt32(chkList(i)), sprSyukkaM.KANRI_NO.ColNo)), "' AND ", _
                                             "SYS_DEL_FLG = '0'"))
                If 0 < dr.Length Then
                    dr(0).Item("PRINT_SORT") = Convert.ToString(.numPrintSortHenko.Value)
                    If (dr(0).Item("OUTKA_NO_M").ToString).Equals(.lblSyukkaMNo.TextValue) = True Then
                        .numPrintSort.Value = .numPrintSortHenko.Value
                    End If
                End If
            Next

        End With

        Return True

    End Function

#End Region 'Spread

#Region "DataSet"

    ''' <summary>
    ''' 出荷管理番号の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetOutNoDataSet(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._frm

            If String.IsNullOrEmpty(.lblSyukkaMNo.TextValue) = True AndAlso _
                String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = True Then
                '編集ボタン(F2)押下後、すぐに保存ボタンを押した時の対応用
                Exit Function
            End If

            '出荷(中)
            If String.IsNullOrEmpty(.lblSyukkaMNo.TextValue) = True Then
                '新規の場合
                Dim max As Integer = ds.Tables(LMC020C.TABLE_NM_MAX).Rows.Count
                Dim insMRows As DataRow = ds.Tables(LMC020C.TABLE_NM_MAX).NewRow

                '出荷管理番号(中)の最大値を求める
                Dim oldMaxOutM As String = String.Empty
                If (0).Equals(max) = True Then
                    oldMaxOutM = "0"
                Else
                    oldMaxOutM = ds.Tables(LMC020C.TABLE_NM_MAX).Rows(max - 1).Item("OUTKA_NO_M").ToString()
                End If
                Dim newMaxOutM As String = Me.SetZeroData(Convert.ToString(Convert.ToDecimal(oldMaxOutM) + 1), "000")

                'SEQの限界値、チェック
                If Me._LMCconV.IsMaxChk(Convert.ToInt32(newMaxOutM), 999, _frm.lblSyukkaMNoL.TextValue) = False Then
                    '処理終了アクション
                    Return False
                End If

                '出荷管理番号(中)の更新
                insMRows("OUTKA_NO_M") = newMaxOutM
                insMRows("MAX_OUTKA_NO_S") = "000"

                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_MAX).Rows.Add(insMRows)

                '画面の出荷管理番号(中)に設定
                .lblSyukkaMNo.TextValue = newMaxOutM

                max = .sprSyukkaM.ActiveSheet.Rows.Count - 1
                For i As Integer = 0 To max
                    If (.lblRecNo.TextValue).Equals(_LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.REC_NO.ColNo))) = True Then
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.KANRI_NO.ColNo, newMaxOutM)
                        Exit For
                    End If
                Next

            End If

            '出荷(小)
            Dim sprMax As Integer = .sprDtl.ActiveSheet.Rows.Count - 1
            Dim insSRows As DataRow() = ds.Tables(LMC020C.TABLE_NM_MAX).Select(String.Concat("OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))

            If 0 < insSRows.Length Then

                Dim outSNo As String = Convert.ToString(insSRows(0).Item("MAX_OUTKA_NO_S"))

                For i As Integer = 0 To sprMax
                    If String.IsNullOrEmpty(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo))) = True Then
                        '出荷管理番号(小)が未設定の場合

                        '出荷管理番号(小)の最大値を求める
                        outSNo = Me.SetZeroData(Convert.ToString(Convert.ToDecimal(outSNo) + 1), "000")

                        '2017/09/25 修正 李↓
                        'SEQの限界値、チェック
                        If Me._LMCconV.IsMaxChk(Convert.ToInt32(outSNo), 999, lgm.Selector({"出荷管理番号(小)", "Shipment control number (S)", "출하관리번호(小)", "中国語"})) = False Then
                            '処理終了アクション
                            Return False
                        End If
                        '2017/09/25 修正 李↑

                        '出荷管理番号(小)の設定
                        insSRows(0).Item("MAX_OUTKA_NO_S") = outSNo

                        '画面の出荷管理番号(小)に設定
                        .sprDtl.SetCellValue(i, sprDtl.SHO_NO.ColNo, outSNo)
                    End If
                Next

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷(大)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetOutLDataSet(ByVal ds As DataSet) As Boolean

        With Me._frm
            Dim dr As DataRow = Nothing

            '採番されていない時は常にinsertするようにデータセットをクリアする。
            If String.IsNullOrEmpty(.lblSyukkaLNo.TextValue) = True Then
                ds.Tables(LMC020C.TABLE_NM_OUT_L).Clear()
            Else
                dr = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)
            End If

            If String.IsNullOrEmpty(.lblSyukkaLNo.TextValue) = True Then
                '新規の場合
                Dim insRows As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_L).NewRow

                insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                insRows.Item("OUTKA_NO_L") = String.Empty
                insRows.Item("FURI_NO") = .lblFurikaeNo.TextValue
                insRows.Item("OUTKA_KB") = .cmbSyukkaKbn.SelectedValue
                insRows.Item("SYUBETU_KB") = .cmbSyukkaSyubetu.SelectedValue

                insRows.Item("OUTKA_STATE_KB_OLD") = LMC020C.SINTYOKU10
                'START YANAI 要望番号394
                'Dim newStateKb As String = String.Empty
                'END YANAI 要望番号394
                Dim zanFlg As Boolean = True
                Dim outM() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMdr As DataRow = Nothing
                Dim outSdr() As DataRow = Nothing
                Dim max As Integer = outM.Length - 1

                '進捗区分判定処理
                '出荷中のデータセットすべて対象に残個数が0以外のデータがあるか検索
                For i As Integer = 0 To max
                    outMdr = outM(i)
                    If .lblSyukkaMNo.TextValue = outMdr.Item("OUTKA_NO_M").ToString Then
                        If ("0").Equals(.numHikiateKosuZan.TextValue) = False OrElse _
                            (LMC020C.PLUS_ZERO).Equals(.numHikiateSuryoZan.TextValue) = False Then
                            zanFlg = False
                            Exit For
                        End If

                    Else
                        If ("0").Equals(outMdr.Item("BACKLOG_NB").ToString) = False OrElse _
                            (LMC020C.PLUS_ZERO).Equals(outMdr.Item("BACKLOG_QT").ToString) = False Then
                            zanFlg = False
                            Exit For
                        End If
                    End If

                Next

                '画面上の値で判定
                If zanFlg = True Then
                    If ("0").Equals(.numHikiateKosuZan.TextValue) = False OrElse _
                        (LMC020C.PLUS_ZERO).Equals(.numHikiateSuryoZan.TextValue) = False Then
                        zanFlg = False
                    End If
                End If

                '出荷Sが存在するかチェック
                If zanFlg = True Then
                    For i As Integer = 0 To max
                        outMdr = outM(i)
                        If .lblSyukkaMNo.TextValue = outMdr.Item("OUTKA_NO_M").ToString Then
                            If .sprDtl.ActiveSheet.Rows.Count = 0 Then
                                zanFlg = False
                                Exit For
                            End If
                        Else
                            outSdr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat( _
                                                                                           "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                           "OUTKA_NO_M = '", outMdr.Item("OUTKA_NO_M").ToString(), "' AND ", _
                                                                                           "SYS_DEL_FLG = '0'"))
                            If outSdr.Length = 0 Then
                                zanFlg = False
                                Exit For
                            End If
                        End If
                    Next

                    If max = -1 Then
                        'maxが-1になる条件は、追加(中)⇒商品選ぶが、引当はしない⇒ダブルクリックとか何もせずに保存押下の時
                        If .sprDtl.ActiveSheet.Rows.Count = 0 Then
                            zanFlg = False
                        End If
                    End If
                End If

                'START YANAI 要望番号394
                'If zanFlg = True Then
                '    newStateKb = LMC020C.SINTYOKU50
                'Else
                '    newStateKb = LMC020C.SINTYOKU10
                'End If
                'insRows.Item("OUTKA_STATE_KB") = newStateKb
                'START YANAI 要望番号565
                'insRows.Item("OUTKA_STATE_KB") = Me.SetStateKb()
                insRows.Item("OUTKA_STATE_KB") = Me.SetStateKb("00", zanFlg)
                'END YANAI 要望番号565
                'END YANAI 要望番号394

                insRows.Item("OUTKAHOKOKU_YN") = .cmbOutkaHokoku_Yn.SelectedValue
                insRows.Item("PICK_KB") = .cmbPick.SelectedValue
                insRows.Item("DENP_NO") = .txtOkuriNo.TextValue
                insRows.Item("ARR_KANRYO_INFO") = String.Empty
                insRows.Item("WH_CD") = .cmbSoko.SelectedValue
                insRows.Item("OUTKA_PLAN_DATE") = .imdSyukkaYoteiDate.TextValue
                insRows.Item("OUTKO_DATE") = .imdSyukkaDate.TextValue
                insRows.Item("ARR_PLAN_DATE") = .imdNounyuYoteiDate.TextValue
                insRows.Item("ARR_PLAN_TIME") = .cmbNounyuKbn.SelectedValue
                insRows.Item("HOKOKU_DATE") = .imdSyukkaHoukoku.TextValue
                insRows.Item("TOUKI_HOKAN_YN") = .cmbToukiYn.SelectedValue
                insRows.Item("END_DATE") = .imdHokanEndDate.TextValue
                insRows.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                insRows.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
                insRows.Item("SHIP_CD_L") = .txtUriCd.TextValue
                insRows.Item("SHIP_CD_M") = String.Empty
                insRows.Item("DEST_CD") = .txtTodokesakiCd.TextValue
                insRows.Item("DEST_AD_3") = .txtTodokeAdderss3.TextValue
                insRows.Item("DEST_TEL") = .txtTodokeTel.TextValue
                insRows.Item("NHS_REMARK") = .txtNouhinTeki.TextValue
                insRows.Item("SP_NHS_KB") = .cmbSiteinouhin.SelectedValue
                insRows.Item("COA_YN") = .cmbBunsakiTmp.SelectedValue
                insRows.Item("CUST_ORD_NO") = .txtNisyuTyumonNo.TextValue
                insRows.Item("BUYER_ORD_NO") = .txtKainusiTyumonNo.TextValue
                insRows.Item("REMARK") = .txtSyukkaRemark.TextValue
                insRows.Item("OUTKA_PKG_NB") = .numKonpoKosu.Value
                insRows.Item("DENP_YN") = .cmbOkurijo.SelectedValue
                insRows.Item("PC_KB") = .cmbMotoCyakuKbn.SelectedValue
                insRows.Item("NIYAKU_YN") = .cmbNiyaku.SelectedValue
                insRows.Item("ALL_PRINT_FLAG") = "00"
                If .chkNifuda.Checked = True Then
                    insRows.Item("NIHUDA_FLAG") = "01"
                Else
                    insRows.Item("NIHUDA_FLAG") = "00"
                End If
                If .chkNHS.Checked = True Then
                    insRows.Item("NHS_FLAG") = "01"
                Else
                    insRows.Item("NHS_FLAG") = "00"
                End If
                If .chkDenp.Checked = True Then
                    insRows.Item("DENP_FLAG") = "01"
                Else
                    insRows.Item("DENP_FLAG") = "00"
                End If
                If .chkCoa.Checked = True Then
                    insRows.Item("COA_FLAG") = "01"
                Else
                    insRows.Item("COA_FLAG") = "00"
                End If
                If .chkHokoku.Checked = True Then
                    insRows.Item("HOKOKU_FLAG") = "01"
                Else
                    insRows.Item("HOKOKU_FLAG") = "00"
                End If
                insRows.Item("MATOME_PICK_FLAG") = "00"
                insRows.Item("LAST_PRINT_DATE") = String.Empty
                insRows.Item("LAST_PRINT_TIME") = String.Empty
                insRows.Item("SASZ_USER") = String.Empty
                insRows.Item("OUTKO_USER") = String.Empty
                insRows.Item("KEN_USER") = String.Empty
                insRows.Item("OUTKA_USER") = String.Empty
                insRows.Item("HOU_USER") = String.Empty
                insRows.Item("ORDER_TYPE") = .txtOrderType.TextValue
                insRows.Item("DEST_KB") = .cmbTodokesaki.SelectedValue
                insRows.Item("DEST_NM2") = .txtTodokesakiNm.TextValue
                insRows.Item("DEST_AD_1") = .txtTodokeAdderss1.TextValue
                insRows.Item("DEST_AD_2") = .txtTodokeAdderss2.TextValue

                insRows.Item("CUST_NM") = .txtCust_Nm.TextValue
                insRows.Item("CUST_NM_L") = .lblCust_Nm_L.TextValue
                insRows.Item("CUST_NM_M") = .lblCust_Nm_M.TextValue
                insRows.Item("SHIP_NM") = .lblUriNm.TextValue
                insRows.Item("DEST_NM") = .txtTodokesakiNm.TextValue
                insRows.Item("AD_1") = .txtTodokeAdderss1.TextValue
                insRows.Item("AD_2") = .txtTodokeAdderss2.TextValue

                insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                insRows.Item("UP_KBN") = "0"

                Dim mSoko As DataRow() = Nothing
                mSoko = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                               , "WH_CD = '", .cmbSoko.SelectedValue, "'"))
                If mSoko.Count - 1 < 0 Then
                    insRows.Item("OUTKA_SASHIZU_PRT_YN") = String.Empty
                Else
                    insRows.Item("OUTKA_SASHIZU_PRT_YN") = mSoko(0).Item("OUTKA_SASHIZU_PRT_YN").ToString
                End If

                '新規保存からすぐに編集した時と、検索画面からきて編集した時の違いがわかるようにINSUPD_FLGを設定
                'INSUPD_FLG="01"なら、新規保存からすぐに編集した時
                insRows.Item("INSUPD_FLG") = "01"

                'タブレット項目
                'insRows.Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_00
                If .chkTablet.Checked = True Then
                    insRows.Item("WH_TAB_YN") = "01"
                    insRows.Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_00
                Else
                    insRows.Item("WH_TAB_YN") = "00"
                    insRows.Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_99
                End If
                If .chkUrgent.Checked = True Then
                    insRows.Item("URGENT_YN") = "01"
                Else
                    insRows.Item("URGENT_YN") = "00"
                End If
                insRows.Item("WH_SIJI_REMARK") = .txtSijiRemark.TextValue
                If .chkNoSiji.Checked = True Then
                    insRows.Item("WH_TAB_NO_SIJI_FLG") = "01"
                Else
                    insRows.Item("WH_TAB_NO_SIJI_FLG") = "00"
                End If
                If .chkTabHokoku.Checked = True Then
                    insRows.Item("WH_TAB_HOKOKU_YN") = "01"
                Else
                    insRows.Item("WH_TAB_HOKOKU_YN") = "00"
                End If
                insRows.Item("WH_TAB_HOKOKU") = .txtHokoku.TextValue
                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows.Add(insRows)

            Else
                '更新の場合
                dr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                dr.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                dr.Item("FURI_NO") = .lblFurikaeNo.TextValue
                dr.Item("OUTKA_KB") = .cmbSyukkaKbn.SelectedValue
                dr.Item("SYUBETU_KB") = .cmbSyukkaSyubetu.SelectedValue

                Dim stateKb As String = dr.Item("OUTKA_STATE_KB").ToString
                dr.Item("OUTKA_STATE_KB_OLD") = stateKb

                'START YANAI 要望番号394
                'Dim newStateKb As String = stateKb
                'Dim outSdr2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0'")
                'If 0 = outSdr2.Length AndAlso _
                '    String.IsNullOrEmpty(newStateKb) = True Then
                '    If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU10) Then
                '        newStateKb = LMC020C.SINTYOKU10
                '    Else
                '        newStateKb = stateKb
                '    End If
                'End If
                'END YANAI 要望番号394

                Dim zanFlg As Boolean = True
                Dim outM() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMdr As DataRow = Nothing
                Dim outSdr() As DataRow = Nothing
                Dim max As Integer = outM.Length - 1

                '出荷中のデータセットすべて対象に残個数が0以外のデータがあるか検索
                For i As Integer = 0 To max
                    outMdr = outM(i)
                    If .lblSyukkaMNo.TextValue = outMdr.Item("OUTKA_NO_M").ToString Then
                        If ("0").Equals(.numHikiateKosuZan.TextValue) = False OrElse _
                            (LMC020C.PLUS_ZERO).Equals(.numHikiateSuryoZan.TextValue) = False Then
                            zanFlg = False
                            Exit For
                        End If

                    Else
                        If ("0").Equals(outMdr.Item("BACKLOG_NB").ToString) = False OrElse _
                            (LMC020C.PLUS_ZERO).Equals(outMdr.Item("BACKLOG_QT").ToString) = False Then
                            zanFlg = False
                            Exit For
                        End If
                    End If

                Next

                '画面上の値で判定
                If zanFlg = True Then
                    If ("0").Equals(.numHikiateKosuZan.TextValue) = False OrElse _
                        (LMC020C.PLUS_ZERO).Equals(.numHikiateSuryoZan.TextValue) = False Then
                        zanFlg = False
                    End If
                End If

                '出荷Sが存在するかチェック
                If zanFlg = True Then
                    For i As Integer = 0 To max
                        outMdr = outM(i)
                        If .lblSyukkaMNo.TextValue = outMdr.Item("OUTKA_NO_M").ToString Then
                            If .sprDtl.ActiveSheet.Rows.Count = 0 Then
                                zanFlg = False
                                Exit For
                            End If
                        Else
                            outSdr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat( _
                                                                                           "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                           "OUTKA_NO_M = '", outMdr.Item("OUTKA_NO_M").ToString(), "' AND ", _
                                                                                           "SYS_DEL_FLG = '0'"))
                            If outSdr.Length = 0 Then
                                zanFlg = False
                                Exit For
                            End If
                        End If
                    Next

                    If max = -1 Then
                        'maxが-1になる条件は、追加(中)⇒商品選ぶが、引当はしない⇒ダブルクリックとか何もせずに保存押下の時
                        If .sprDtl.ActiveSheet.Rows.Count = 0 Then
                            zanFlg = False
                        End If
                    End If

                End If

                'START YANAI 要望番号394
                'If zanFlg = True Then
                '    If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU50) Then
                '        newStateKb = LMC020C.SINTYOKU50
                '    Else
                '        newStateKb = stateKb
                '    End If
                'End If

                ''START YANAI No.29
                'Dim sokodr() As DataRow = Nothing
                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    sokodr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                '                                                                                     "WH_CD = '", .cmbSoko.SelectedValue, "'"))
                '    If ("00").Equals(sokodr(0).Item("OUTKA_SASHIZU_PRT_YN").ToString) = False AndAlso _
                '        String.IsNullOrEmpty(newStateKb) = True Then
                '        If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU10) Then
                '            newStateKb = LMC020C.SINTYOKU10
                '        Else
                '            newStateKb = stateKb
                '        End If
                '    End If
                'End If
                ''END YANAI No.29

                'If ("01").Equals(.cmbPick.SelectedValue) = False AndAlso _
                '    String.IsNullOrEmpty(newStateKb) = True Then
                '    If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU30) Then
                '        newStateKb = LMC020C.SINTYOKU30
                '    Else
                '        newStateKb = stateKb
                '    End If
                'End If
                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    'START YANAI No.29
                '    'Dim sokodr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                '    '                                                                                                  "WH_CD = '", .cmbSoko.SelectedValue, "'"))
                '    'END YANAI No.29
                '    If ("00").Equals(sokodr(0).Item("OUTOKA_KANRYO_YN").ToString) = False AndAlso _
                '    String.IsNullOrEmpty(newStateKb) = True Then
                '        If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU30) Then
                '            newStateKb = LMC020C.SINTYOKU30
                '        Else
                '            newStateKb = stateKb
                '        End If
                '    End If
                '    If ("00").Equals(sokodr(0).Item("OUTKA_KENPIN_YN").ToString) = False AndAlso _
                '    String.IsNullOrEmpty(newStateKb) = True Then
                '        If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU40) Then
                '            newStateKb = LMC020C.SINTYOKU40
                '        Else
                '            newStateKb = stateKb
                '        End If
                '    End If
                '    If ("01").Equals(sokodr(0).Item("OUTKA_KENPIN_YN").ToString) = False AndAlso _
                '    String.IsNullOrEmpty(newStateKb) = True Then
                '        If Convert.ToDecimal(stateKb) < Convert.ToDecimal(LMC020C.SINTYOKU50) Then
                '            newStateKb = LMC020C.SINTYOKU50
                '        Else
                '            newStateKb = stateKb
                '        End If
                '    End If
                '    If String.IsNullOrEmpty(newStateKb) = True Then
                '        newStateKb = stateKb
                '    End If
                'End If
                'dr.Item("OUTKA_STATE_KB") = newStateKb
                'START YANAI 要望番号565
                'dr.Item("OUTKA_STATE_KB") = Me.SetStateKb(stateKb)
                dr.Item("OUTKA_STATE_KB") = Me.SetStateKb(stateKb, zanFlg)
                'END YANAI 要望番号565
                'END YANAI 要望番号394

                dr.Item("OUTKAHOKOKU_YN") = .cmbOutkaHokoku_Yn.SelectedValue
                dr.Item("PICK_KB") = .cmbPick.SelectedValue
                dr.Item("DENP_NO") = .txtOkuriNo.TextValue
                dr.Item("WH_CD") = .cmbSoko.SelectedValue
                dr.Item("OUTKA_PLAN_DATE") = .imdSyukkaYoteiDate.TextValue
                dr.Item("OUTKO_DATE") = .imdSyukkaDate.TextValue
                dr.Item("ARR_PLAN_DATE") = .imdNounyuYoteiDate.TextValue
                dr.Item("ARR_PLAN_TIME") = .cmbNounyuKbn.SelectedValue
                dr.Item("HOKOKU_DATE") = .imdSyukkaHoukoku.TextValue
                dr.Item("TOUKI_HOKAN_YN") = .cmbToukiYn.SelectedValue
                dr.Item("END_DATE") = .imdHokanEndDate.TextValue
                dr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                dr.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
                dr.Item("SHIP_CD_L") = .txtUriCd.TextValue
                dr.Item("DEST_CD") = .txtTodokesakiCd.TextValue
                dr.Item("DEST_AD_3") = .txtTodokeAdderss3.TextValue
                dr.Item("DEST_TEL") = .txtTodokeTel.TextValue
                dr.Item("NHS_REMARK") = .txtNouhinTeki.TextValue
                dr.Item("SP_NHS_KB") = .cmbSiteinouhin.SelectedValue
                dr.Item("COA_YN") = .cmbBunsakiTmp.SelectedValue
                dr.Item("CUST_ORD_NO") = .txtNisyuTyumonNo.TextValue
                dr.Item("BUYER_ORD_NO") = .txtKainusiTyumonNo.TextValue
                dr.Item("REMARK") = .txtSyukkaRemark.TextValue
                '2018/12/07 ADD START 要望管理002171
                If Convert.ToDecimal(dr.Item("OUTKA_PKG_NB")) <> Convert.ToDecimal(.numKonpoKosu.Value) Then
                    'DataTableと画面の値が異なる⇒ユーザが入力⇒画面の値を使用
                    ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Rows(0).Item("USE_GAMEN_VALUE_FLG") = LMC020C.USE_GAMEN_VALUE_TRUE
                End If
                '2018/12/07 ADD END   要望管理002171
                dr.Item("OUTKA_PKG_NB") = .numKonpoKosu.Value
                dr.Item("DENP_YN") = .cmbOkurijo.SelectedValue
                dr.Item("PC_KB") = .cmbMotoCyakuKbn.SelectedValue
                dr.Item("NIYAKU_YN") = .cmbNiyaku.SelectedValue
                If .chkNifuda.Checked = True Then
                    dr.Item("NIHUDA_FLAG") = "01"
                Else
                    dr.Item("NIHUDA_FLAG") = "00"
                End If
                If .chkNHS.Checked = True Then
                    dr.Item("NHS_FLAG") = "01"
                Else
                    dr.Item("NHS_FLAG") = "00"
                End If
                If .chkDenp.Checked = True Then
                    dr.Item("DENP_FLAG") = "01"
                Else
                    dr.Item("DENP_FLAG") = "00"
                End If
                If .chkCoa.Checked = True Then
                    dr.Item("COA_FLAG") = "01"
                Else
                    dr.Item("COA_FLAG") = "00"
                End If
                If .chkHokoku.Checked = True Then
                    dr.Item("HOKOKU_FLAG") = "01"
                Else
                    dr.Item("HOKOKU_FLAG") = "00"
                End If
                dr.Item("ORDER_TYPE") = .txtOrderType.TextValue
                dr.Item("DEST_KB") = .cmbTodokesaki.SelectedValue
                dr.Item("DEST_NM2") = .txtTodokesakiNm.TextValue
                dr.Item("DEST_AD_1") = .txtTodokeAdderss1.TextValue
                dr.Item("DEST_AD_2") = .txtTodokeAdderss2.TextValue

                dr.Item("CUST_NM") = .txtCust_Nm.TextValue
                dr.Item("CUST_NM_L") = .lblCust_Nm_L.TextValue
                dr.Item("CUST_NM_M") = .lblCust_Nm_M.TextValue
                dr.Item("SHIP_NM") = .lblUriNm.TextValue
                dr.Item("DEST_NM") = .txtTodokesakiNm.TextValue
                dr.Item("AD_1") = .txtTodokeAdderss1.TextValue
                dr.Item("AD_2") = .txtTodokeAdderss2.TextValue

                dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                If ("1").Equals(dr.Item("UP_KBN").ToString) = False Then
                    dr.Item("UP_KBN") = "1"
                End If

                'タブレット項目
                '作業指示ステータス
                'Select Case .cmbWHSagyoSintyoku.SelectedValue.ToString
                '    Case LMC020C.WH_TAB_STATUS_00, LMC020C.WH_TAB_STATUS_02
                '        dr.Item("WH_TAB_STATUS") = .cmbWHSagyoSintyoku.SelectedValue
                '    Case LMC020C.WH_TAB_STATUS_01
                '        dr.Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_02
                '    Case Else
                '        dr.Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_00
                'End Select
                '現場作業指示有無
                If .chkTablet.Checked = True Then
                    dr.Item("WH_TAB_YN") = "01"
                Else
                    dr.Item("WH_TAB_YN") = "00"
                End If
                '急ぎ有無
                If .chkUrgent.Checked = True Then
                    dr.Item("URGENT_YN") = "01"
                Else
                    dr.Item("URGENT_YN") = "00"
                End If
                dr.Item("WH_SIJI_REMARK") = .txtSijiRemark.TextValue

                If .chkNoSiji.Checked = True Then
                    dr.Item("WH_TAB_NO_SIJI_FLG") = "01"
                Else
                    dr.Item("WH_TAB_NO_SIJI_FLG") = "00"
                End If
                If .chkTabHokoku.Checked = True Then
                    dr.Item("WH_TAB_HOKOKU_YN") = "01"
                Else
                    dr.Item("WH_TAB_HOKOKU_YN") = "00"
                End If
                dr.Item("WH_TAB_HOKOKU") = .txtHokoku.TextValue
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷(中)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetOutMDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As Boolean

        With Me._frm

            If String.IsNullOrEmpty(.lblRecNo.TextValue) = True Then
                Exit Function
            End If


            Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                         "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))
            Dim mdr As DataRow = Nothing

            Dim recNo As Integer = Convert.ToInt32(.lblRecNo.TextValue)

            If dr.Length = 0 Then
                '新規の場合
                Dim insRows As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).NewRow

                insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                insRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                insRows.Item("OUTKA_NO_M") = .lblSyukkaMNo.TextValue
                insRows.Item("EDI_SET_NO") = String.Empty
                If .optTempY.Checked = True Then
                    insRows.Item("COA_YN") = "01"
                Else
                    insRows.Item("COA_YN") = "00"
                End If
                insRows.Item("CUST_ORD_NO_DTL") = .txtOrderNo.TextValue
                insRows.Item("BUYER_ORD_NO_DTL") = .txtCyumonNo.TextValue
                insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                insRows.Item("GOODS_CD_NRS_FROM") = .lblGoodsCdNrsFrom.TextValue
                insRows.Item("RSV_NO") = .txtRsvNo.TextValue
                insRows.Item("LOT_NO") = .txtLotNo.TextValue.ToUpper()
                insRows.Item("SERIAL_NO") = .txtSerialNo.TextValue
                If .optCnt.Checked = True Then
                    insRows.Item("ALCTD_KB") = "01"
                    insRows.Item("ALCTD_KB_NM") = .optCnt.Text
                ElseIf .optAmt.Checked = True Then
                    insRows.Item("ALCTD_KB") = "02"
                    insRows.Item("ALCTD_KB_NM") = .optAmt.Text
                ElseIf .optKowake.Checked = True Then
                    insRows.Item("ALCTD_KB") = "03"
                    insRows.Item("ALCTD_KB_NM") = .optKowake.Text
                ElseIf .optSample.Checked = True Then
                    insRows.Item("ALCTD_KB") = "04"
                    insRows.Item("ALCTD_KB_NM") = .optSample.Text
                End If
                'START YANAI 要望番号681
                'insRows.Item("OUTKA_PKG_NB") = .numIrisu.Value
                insRows.Item("OUTKA_PKG_NB") = .numIrisu.Value
                'END YANAI 要望番号681
                insRows.Item("OUTKA_HASU") = .numHasu.Value
                insRows.Item("OUTKA_QT") = "0"
                insRows.Item("OUTKA_TTL_NB") = .numSouKosu.Value
                insRows.Item("OUTKA_TTL_QT") = .numSouSuryo.Value
                insRows.Item("ALCTD_NB") = .numHikiateKosuSumi.Value
                insRows.Item("ALCTD_QT") = .numHikiateSuryoSumi.Value
                insRows.Item("BACKLOG_NB") = .numHikiateKosuZan.Value
                insRows.Item("BACKLOG_QT") = .numHikiateSuryoZan.Value
                insRows.Item("UNSO_ONDO_KB") = .cmbUnsoOndo.SelectedValue
                insRows.Item("IRIME") = .numIrime.Value
#If False Then '区分タイトルラベル対応 Changed 20151117 INOUE
                insRows.Item("IRIME_UT") = .lblIrimeUThide.TextValue
#Else
                insRows.Item("IRIME_UT") = .lblIrimeUT.KbnValue
#End If
                insRows.Item("OUTKA_M_PKG_NB") = .numPkgCnt.Value
                insRows.Item("REMARK") = .txtGoodsRemark.TextValue
                insRows.Item("SIZE_KB") = .cmbTakkyuSize.SelectedValue
                insRows.Item("ZAIKO_KB") = String.Empty
                insRows.Item("SOURCE_CD") = String.Empty
                insRows.Item("YELLOW_CARD") = String.Empty
                insRows.Item("PRINT_SORT") = .numPrintSort.TextValue
                insRows.Item("HIKIATE") = .lblHikiate.TextValue
                insRows.Item("UP_KBN") = "0"
                insRows.Item("GOODS_CD_CUST") = .txtGoodsCdCust.TextValue
                insRows.Item("GOODS_NM") = .lblGoodsNm.TextValue


#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
                insRows.Item("QT_UT_NM") = .lblIrimeUT.TextValue
                insRows.Item("NB_UT_NM") = .lblHasuUT.TextValue
#Else
                insRows.Item("QT_UT_NM") = .lblIrimeUT.TextValue
                insRows.Item("NB_UT_NM") = .lblHasuUT.TextValue
#End If
                insRows.Item("PKG_NB") = .numIrisu.Value
                insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                insRows.Item("CUST_CD_S") = .lblCustCdS.TextValue
                insRows.Item("CUST_CD_SS") = .lblCustCdSS.TextValue
                'START YANAI メモ②No.20
                insRows.Item("EDI_FLG") = String.Empty
                'END YANAI メモ②No.20
                'START YANAI 要望番号499
                insRows.Item("CUST_CD_L_GOODS") = .lblCustCdL.TextValue
                insRows.Item("CUST_CD_M_GOODS") = .lblCustCdM.TextValue
                'END YANAI 要望番号499

                Dim row As Integer = .sprSyukkaM.ActiveSheet.Rows.Count - 1
                insRows.Item("SEARCH_KEY_1") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.SEARCH_KEY_1.ColNo))
                insRows.Item("UNSO_ONDO_KB") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.UNSO_ONDO_KB.ColNo))
                insRows.Item("PKG_UT") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.PKG_UT.ColNo))
                insRows.Item("STD_IRIME_NB") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.STD_IRIME_NB.ColNo))
                insRows.Item("STD_WT_KGS") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.STD_WT_KGS.ColNo))
                insRows.Item("TARE_YN") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.TARE_YN.ColNo))
                insRows.Item("REMARK_OUT") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.REMARK_OUT.ColNo))
                insRows.Item("TAX_KB") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.TAX_KB.ColNo))
                insRows.Item("HIKIATE_ALERT_YN") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.HIKIATE_ALERT_YN.ColNo))
                insRows.Item("OUTKA_ATT") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.OUTKA_ATT.ColNo))
                'insRows.Item("GOODS_CD_NRS_FROM") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.GOODS_CD_NRS_FROM.ColNo))

                '出荷(中)単位の運送重量を求める
                insRows.Item("UNSO_WT_M") = Me._H.GetUnsoWTM(insRows, Me._frm)

                insRows.Item("NB_UT") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.NB_UT.ColNo))

                insRows.Item("SHOBO_CD") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.SHOBO_CD.ColNo))
                insRows.Item("SHOBO_NM") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(row, sprSyukkaM.SHOBO_NM.ColNo))

                'START YANAI 要望番号998
                '新規保存からすぐに編集した時と、検索画面からきて編集した時の違いがわかるようにINSUPD_FLGを設定
                'INSUPD_FLG="01"なら、新規保存からすぐに編集した時
                insRows.Item("INSUPD_FLG") = "01"
                'END YANAI 要望番号998

                '追加開始 --- 2014.07.28 kikuchi
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                                 "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, _
                                                                                                 "' AND SUB_KB = '78'"))

                If 0 < custDetailsDr.Length Then

                    If .cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_ARI) Then
                        insRows.Item("COA_YN") = "01"

                    ElseIf .cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_NASI) Then
                        insRows.Item("COA_YN") = "00"

                    End If
                End If
                '追加終了 ---

                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Add(insRows)

                mdr = insRows

                '2015.07.08 協立化学　シッピング対応 追加START
                'マーク(HED)
                Dim insHedRows As DataRow = Nothing
                Dim insDtlRows As DataRow = Nothing
                If (eventShubetsu = LMC020C.EventShubetsu.INS_M) OrElse (Convert.ToInt32(.numCaseNoFrom.Value()) = 0 AndAlso Convert.ToInt32(.numCaseNoTo.Value()) = 0 AndAlso String.IsNullOrEmpty(.txtMarkInfo1.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtMarkInfo2.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo3.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo4.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtMarkInfo5.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo6.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo7.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtMarkInfo8.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo9.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo10.TextValue) = True) Then

                Else

                    insHedRows = ds.Tables(LMC020C.TABLE_NM_MARK_HED).NewRow
                    insHedRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                    insHedRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                    insHedRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()
                    insHedRows.Item("CASE_NO_FROM") = .numCaseNoFrom.Value()
                    insHedRows.Item("CASE_NO_TO") = .numCaseNoTo.Value()
                    insHedRows.Item("UP_KBN") = "0"
                    insHedRows.Item("SYS_DEL_FLG") = "0" '新規
                    'データセットに追加
                    ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Add(insHedRows)

                    Dim drM As DataRow() = Nothing
                    Dim max As Integer = 8
                    '荷主明細
                    drM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                      "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                     , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                                     , "SUB_KB = '00'"))
                    '列数設定
                    If drM.Length > 0 Then
                        max = Convert.ToInt32(drM(0).Item("SET_NAIYO").ToString())
                    End If


                    For i As Integer = 1 To max
                        insDtlRows = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).NewRow
                        insDtlRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                        insDtlRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                        insDtlRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()

                        Select Case i

                            Case Convert.ToInt32(Right(.txtMarkInfo1.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo1.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo2.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo2.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo3.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo3.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo4.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo4.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo5.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo5.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo6.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo6.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo7.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo7.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo8.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo8.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo9.Name, 1))
                                If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo9.TextValue()
                                End If

                            Case Convert.ToInt32(Right(.txtMarkInfo10.Name, 2))
                                If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                                    insDtlRows.Item("MARK_EDA") = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                                    insDtlRows.Item("REMARK_INFO") = .txtMarkInfo10.TextValue()
                                End If

                        End Select
                        insDtlRows.Item("UP_KBN") = "0"
                        insDtlRows.Item("SYS_DEL_FLG") = "0" '新規
                        'insDtlRows追加
                        ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Add(insDtlRows)
                    Next

                End If
                '2015.07.08 協立化学　シッピング対応 追加END

            Else

                '更新の場合
                dr(0).Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                dr(0).Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                dr(0).Item("OUTKA_NO_M") = .lblSyukkaMNo.TextValue
                If .optTempY.Checked = True Then
                    dr(0).Item("COA_YN") = "01"
                Else
                    dr(0).Item("COA_YN") = "00"
                End If
                dr(0).Item("CUST_ORD_NO_DTL") = .txtOrderNo.TextValue
                dr(0).Item("BUYER_ORD_NO_DTL") = .txtCyumonNo.TextValue
                dr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                dr(0).Item("GOODS_CD_NRS_FROM") = .lblGoodsCdNrsFrom.TextValue
                dr(0).Item("RSV_NO") = .txtRsvNo.TextValue
                dr(0).Item("LOT_NO") = .txtLotNo.TextValue.ToUpper()
                dr(0).Item("SERIAL_NO") = .txtSerialNo.TextValue
                If .optCnt.Checked = True Then
                    dr(0).Item("ALCTD_KB") = "01"
                    dr(0).Item("ALCTD_KB_NM") = .optCnt.Text
                ElseIf .optAmt.Checked = True Then
                    dr(0).Item("ALCTD_KB") = "02"
                    dr(0).Item("ALCTD_KB_NM") = .optAmt.Text
                ElseIf .optKowake.Checked = True Then
                    dr(0).Item("ALCTD_KB") = "03"
                    dr(0).Item("ALCTD_KB_NM") = .optKowake.Text
                ElseIf .optSample.Checked = True Then
                    dr(0).Item("ALCTD_KB") = "04"
                    dr(0).Item("ALCTD_KB_NM") = .optSample.Text
                End If
                dr(0).Item("OUTKA_PKG_NB") = .numIrisu.Value
                dr(0).Item("OUTKA_HASU") = .numHasu.Value
                dr(0).Item("OUTKA_TTL_NB") = .numSouKosu.Value
                dr(0).Item("OUTKA_TTL_QT") = .numSouSuryo.Value
                dr(0).Item("ALCTD_NB") = .numHikiateKosuSumi.Value
                dr(0).Item("ALCTD_QT") = .numHikiateSuryoSumi.Value
                dr(0).Item("BACKLOG_NB") = .numHikiateKosuZan.Value
                dr(0).Item("BACKLOG_QT") = .numHikiateSuryoZan.Value
                dr(0).Item("UNSO_ONDO_KB") = .cmbUnsoOndo.SelectedValue
                dr(0).Item("IRIME") = .numIrime.Value
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
                dr(0).Item("IRIME_UT") = .lblIrimeUThide.TextValue
#Else
                dr(0).Item("IRIME_UT") = .lblIrimeUT.KbnValue
#End If
                dr(0).Item("OUTKA_M_PKG_NB") = .numPkgCnt.Value
                dr(0).Item("REMARK") = .txtGoodsRemark.TextValue
                dr(0).Item("SIZE_KB") = .cmbTakkyuSize.SelectedValue
                dr(0).Item("PRINT_SORT") = .numPrintSort.Value
                dr(0).Item("GOODS_CD_CUST") = .txtGoodsCdCust.TextValue
                dr(0).Item("GOODS_NM") = .lblGoodsNm.TextValue


#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
                dr(0).Item("QT_UT_NM") = .lblIrimeUT.TextValue
                dr(0).Item("NB_UT_NM") = .lblHasuUT.TextValue
#Else
                dr(0).Item("QT_UT_NM") = .lblIrimeUT.TextValue
                dr(0).Item("NB_UT_NM") = .lblHasuUT.TextValue
#End If
                dr(0).Item("PKG_NB") = .numIrisu.Value

                '出荷(中)単位の運送重量を求める
                dr(0).Item("UNSO_WT_M") = Me._H.GetUnsoWTM(dr(0), Me._frm)

                'dr(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                dr(0).Item("CUST_CD_S") = .lblCustCdS.TextValue
                dr(0).Item("CUST_CD_SS") = .lblCustCdSS.TextValue
                'START YANAI メモ②No.20
                dr(0).Item("EDI_FLG") = dr(0).Item("EDI_FLG")
                'END YANAI メモ②No.20
                'START YANAI 要望番号499
                dr(0).Item("CUST_CD_L_GOODS") = .lblCustCdL.TextValue
                dr(0).Item("CUST_CD_M_GOODS") = .lblCustCdM.TextValue
                'END YANAI 要望番号499

                If ("0").Equals(dr(0).Item("UP_KBN").ToString) = False Then
                    dr(0).Item("UP_KBN") = "1"
                End If
                dr(0).Item("HIKIATE") = .lblHikiate.TextValue

                mdr = dr(0)

                '2015.07.08 協立化学　シッピング対応 追加START
                Dim insHedRows As DataRow = Nothing
                Dim insDtlRows As DataRow = Nothing

                Dim Drs As DataRow() = Nothing
                Dim DrsDtl As DataRow() = Nothing
                If .TabPage2.CanFocus = True Then

                    'マーク(HED)
                    If Convert.ToInt32(.numCaseNoFrom.Value()) = 0 AndAlso Convert.ToInt32(.numCaseNoTo.Value()) = 0 AndAlso String.IsNullOrEmpty(.txtMarkInfo1.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo2.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo3.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo4.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo5.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo6.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo7.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo8.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo9.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo10.TextValue) = True Then

                    Else

                        If ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Count > 0 AndAlso _
                            ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", .txtOutkaNoM.TextValue(), "' AND " _
                                                                                         , "SYS_DEL_FLG = '0'")).Length > 0 Then

                            Drs = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", .txtOutkaNoM.TextValue(), "' AND " _
                                                             , "SYS_DEL_FLG = '0'"))
                            Drs(0).Item("CASE_NO_FROM") = .numCaseNoFrom.Value()
                            Drs(0).Item("CASE_NO_TO") = .numCaseNoTo.Value()
                            If ("0").Equals(Drs(0).Item("UP_KBN").ToString) = False Then
                                Drs(0).Item("UP_KBN") = "1"
                            End If
                            Drs(0).Item("SYS_DEL_FLG") = "0" '新規


                            Dim drM As DataRow() = Nothing
                            Dim max As Integer = 8
                            '荷主明細
                            drM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                              "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                             , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                                             , "SUB_KB = '00'"))
                            '列数設定
                            If drM.Length > 0 Then
                                max = Convert.ToInt32(drM(0).Item("SET_NAIYO").ToString())
                            End If


                            For i As Integer = 1 To max
                                DrsDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", .txtOutkaNoM.TextValue(), "' AND " _
                                                             , "SYS_DEL_FLG = '0'"))
                                DrsDtl(i - 1).Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                                DrsDtl(i - 1).Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                                DrsDtl(i - 1).Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()

                                Select Case i

                                    Case Convert.ToInt32(Right(.txtMarkInfo1.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo1.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo2.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo2.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo3.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo3.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo4.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo4.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo5.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo5.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo6.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo6.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo7.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo7.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo8.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo8.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo9.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo9.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo10.Name, 2))
                                        If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                                            DrsDtl(i - 1).Item("MARK_EDA") = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                                            DrsDtl(i - 1).Item("REMARK_INFO") = .txtMarkInfo10.TextValue()
                                        End If

                                End Select
                                If DrsDtl(i - 1).Item("UP_KBN").ToString().Equals(LMConst.FLG.ON) = False Then
                                    DrsDtl(i - 1).Item("UP_KBN") = "0"
                                    DrsDtl(i - 1).Item("SYS_DEL_FLG") = "0" '新規
                                End If
                            Next

                        Else
                            insHedRows = ds.Tables(LMC020C.TABLE_NM_MARK_HED).NewRow
                            insHedRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                            insHedRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                            insHedRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()
                            insHedRows.Item("CASE_NO_FROM") = .numCaseNoFrom.Value()
                            insHedRows.Item("CASE_NO_TO") = .numCaseNoTo.Value()
                            insHedRows.Item("UP_KBN") = "0"
                            insHedRows.Item("SYS_DEL_FLG") = "0" '新規
                            'データセットに追加
                            ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Add(insHedRows)


                            Dim drM As DataRow() = Nothing
                            Dim max As Integer = 8
                            '荷主明細
                            drM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                              "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                             , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                                             , "SUB_KB = '00'"))
                            '列数設定
                            If drM.Length > 0 Then
                                max = Convert.ToInt32(drM(0).Item("SET_NAIYO").ToString())
                            End If


                            For i As Integer = 1 To max
                                insDtlRows = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).NewRow
                                insDtlRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                                insDtlRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                                insDtlRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()
                                insDtlRows.Item("MARK_EDA") = .txtMarkInfo1.TextValue()

                                Select Case i

                                    Case Convert.ToInt32(Right(.txtMarkInfo1.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo1.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo2.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo2.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo3.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo3.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo4.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo4.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo5.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo5.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo6.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo6.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo7.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo7.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo8.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo8.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo9.Name, 1))
                                        If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo9.TextValue()
                                        End If

                                    Case Convert.ToInt32(Right(.txtMarkInfo10.Name, 2))
                                        If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                                            insDtlRows.Item("MARK_EDA") = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                                            insDtlRows.Item("REMARK_INFO") = .txtMarkInfo10.TextValue()
                                        End If

                                End Select
                                If insDtlRows.Item("UP_KBN").ToString().Equals(LMConst.FLG.ON) = False Then
                                    insDtlRows.Item("UP_KBN") = "0"
                                    insDtlRows.Item("SYS_DEL_FLG") = "0" '新規
                                End If
                                'insDtlRows追加
                                ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Add(insDtlRows)
                            Next

                        End If

                    End If
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '2015.07.08 協立化学　シッピング対応 追加START
                    '.sprSyukkaM.SetCellValue(recNo - 1, sprSyukkaM.CASE_NO_FROM.ColNo, .numCaseNoFrom.Value)
                    '.sprSyukkaM.SetCellValue(recNo - 1, sprSyukkaM.CASE_NO_TO.ColNo, .numCaseNoTo.Value)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_1.ColNo, .txtMarkInfo1.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_2.ColNo, .txtMarkInfo2.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_3.ColNo, .txtMarkInfo3.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_4.ColNo, .txtMarkInfo4.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_5.ColNo, .txtMarkInfo5.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_6.ColNo, .txtMarkInfo6.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_7.ColNo, .txtMarkInfo7.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_8.ColNo, .txtMarkInfo8.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_9.ColNo, .txtMarkInfo9.TextValue)
                    '.sprSyukkaM.SetCellValue(recNo, sprSyukkaM.MARK_INFO_10.ColNo, .txtMarkInfo10.TextValue)
                    '2015.07.08 協立化学　シッピング対応 追加END

                End If

                '2015.07.08 協立化学　シッピング対応 追加START
                'マーク(HED)
                Dim updHedRows As DataRow = Nothing
                For i As Integer = 0 To ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Count - 1
                    updHedRows = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows(i)
                    updHedRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                    updHedRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                    If updHedRows.Item("SYS_DEL_FLG").ToString().Equals(LMConst.FLG.ON) = False Then
                        'updHedRows.Item("OUTKA_NO_M") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo))
                        'updHedRows.Item("CASE_NO_FROM") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.CASE_NO_FROM.ColNo))
                        'updHedRows.Item("CASE_NO_TO") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.CASE_NO_TO.ColNo))
                        'updHedRows.Item("MARK_INFO_1") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_1.ColNo))
                        'updHedRows.Item("MARK_INFO_2") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_2.ColNo))
                        'updHedRows.Item("MARK_INFO_3") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_3.ColNo))
                        'updHedRows.Item("MARK_INFO_4") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_4.ColNo))
                        'updHedRows.Item("MARK_INFO_5") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_5.ColNo))
                        'updHedRows.Item("MARK_INFO_6") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_6.ColNo))
                        'updHedRows.Item("MARK_INFO_7") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_7.ColNo))
                        'updHedRows.Item("MARK_INFO_8") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_8.ColNo))
                        'updHedRows.Item("MARK_INFO_9") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_9.ColNo))
                        'updHedRows.Item("MARK_INFO_10") = Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.MARK_INFO_10.ColNo))
                        updHedRows.Item("SYS_DEL_FLG") = "0" '更新
                    End If
                    If updHedRows.Item("UP_KBN").ToString().Equals(LMConst.FLG.OFF) = False Then
                        updHedRows.Item("UP_KBN") = "1"
                    End If
                Next

                '2015.07.08 協立化学　シッピング対応 追加END

            End If

            If (eventShubetsu).Equals("LMC020C.EventShubetsu.HOZON") = False Then

                'スプレッドに反映させる
                Dim max As Integer = .sprSyukkaM.ActiveSheet.Rows.Count - 1

                For i As Integer = 0 To max
                    If Convert.ToString(recNo).Equals(_LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.REC_NO.ColNo))) = True Then

                        '.sprSyukkaM.SetCellValue(i, sprSyukkaM.DEFM.ColNo, LMC020C.FLG_FALSE)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.PRT_ORDER.ColNo, .numPrintSort.TextValue)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.KANRI_NO.ColNo, .lblSyukkaMNo.TextValue)
                        If .optCnt.Checked = True Then
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, .optCnt.Text)
                        ElseIf .optAmt.Checked = True Then
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, .optAmt.Text)
                        ElseIf .optKowake.Checked = True Then
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, .optKowake.Text)
                        ElseIf .optSample.Checked = True Then
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, .optSample.Text)
                        End If
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.IRIME.ColNo, .numIrime.TextValue)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.NB.ColNo, .numSouKosu.TextValue)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.ALL_SURYO.ColNo, .numSouSuryo.TextValue)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.ZANSU.ColNo, .numHikiateKosuZan.TextValue)
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.GOODS_COMMENT.ColNo, .txtGoodsRemark.TextValue)
                        '(2012.12.21)要望番号1710 ロット№追加 -- START --
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.M_LOT_NO.ColNo, .txtLotNo.TextValue)
                        '(2012.12.21)要望番号1710 ロット№追加 --  END  --
                        .sprSyukkaM.SetCellValue(i, sprSyukkaM.HIKIATE_JK.ColNo, .lblHikiate.TextValue)

                        Exit For

                    End If
                Next

            End If

        End With

        Return True

    End Function

    'START YANAI 20110913 小分け対応
    '''' <summary>
    '''' 出荷(小)の値を設定
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks></remarks>
    'Friend Function SetOutSDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As Boolean
    ''' <summary>
    ''' 出荷(小)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetOutSDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal newFlg As Boolean) As Boolean
        'END YANAI 20110913 小分け対応

        With Me._frm

            Dim sprMax As Integer = .sprDtl.ActiveSheet.Rows.Count - 1
            Dim dr As DataRow() = Nothing
            Dim drOutM As DataRow() = Nothing
            Dim drZai As DataRow() = Nothing
            Dim poraZaiQt As Decimal = 0
            Dim insRows As DataRow = Nothing
            Dim kosuFlg As Boolean = True

            'START YANAI 20110913 小分け対応
            Dim drOutS As DataRow() = Nothing
            Dim drOutS2 As DataRow() = Nothing
            Dim sumAlctdQt As Decimal = 0
            Dim max As Integer = 0
            'END YANAI 20110913 小分け対応

            If .optKowake.Checked = False OrElse _
                -1 = sprMax Then
                kosuFlg = False
            End If

            For i As Integer = 0 To sprMax
                dr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select( _
                         String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                       "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'", _
                                       "AND OUTKA_NO_S = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo)), "'"))
                If (0).Equals(dr.Length) = True Then
                    '新規の場合
                    insRows = ds.Tables(LMC020C.TABLE_NM_OUT_S).NewRow

                    insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                    insRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                    insRows.Item("OUTKA_NO_M") = .lblSyukkaMNo.TextValue
                    insRows.Item("OUTKA_NO_S") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo))
                    insRows.Item("TOU_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.TOU_NO.ColNo))
                    insRows.Item("SITU_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHITSU_NO.ColNo))
                    insRows.Item("ZONE_CD") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZONE_CD.ColNo))
                    insRows.Item("LOCA") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOCA.ColNo))
                    insRows.Item("LOT_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOT_NO.ColNo)).ToUpper()
                    insRows.Item("SERIAL_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SERIAL_NO.ColNo))
                    insRows.Item("OUTKA_TTL_NB") = .numSouKosu.Value
                    insRows.Item("OUTKA_TTL_QT") = .numSouSuryo.Value
                    insRows.Item("ZAI_REC_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo))
                    insRows.Item("INKA_NO_L") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_L.ColNo))
                    insRows.Item("INKA_NO_M") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_M.ColNo))
                    insRows.Item("INKA_NO_S") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_S.ColNo))
                    insRows.Item("ZAI_UPD_FLAG") = "00"

                    'START YANAI 要望番号681
                    drZai = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(
                             String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                           "ZAI_REC_NO = '", insRows.Item("ZAI_REC_NO").ToString, "' AND ",
                                           "SMPL_FLAG_ZAI = '01' AND ",
                                           "SYS_DEL_FLG = '0' AND ",
                                           "UP_KBN = '0'"))
                    If 0 < drZai.Length Then
                        insRows.Item("SMPL_FLG_ZAI") = "01"
                    Else
                        insRows.Item("SMPL_FLG_ZAI") = "00"
                    End If
                    'END YANAI 要望番号681

                    If .optKowake.Checked = True AndAlso
                        (_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo))).Equals(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))) = True Then
                        '在庫が1の時対応
                        insRows.Item("ALCTD_CAN_NB") = "0"
                        insRows.Item("ALCTD_NB") = "1"
                        insRows.Item("ALCTD_CAN_QT") = "0"
                        insRows.Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_GAMEN") = "0"
                        insRows.Item("ALCTD_NB_GAMEN") = "1"
                        insRows.Item("ALCTD_CAN_QT_GAMEN") = "0"
                        insRows.Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_MATOME") = "0"
                        insRows.Item("ALCTD_NB_MATOME") = "1"
                        insRows.Item("ALCTD_CAN_QT_MATOME") = "0"
                        insRows.Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                        insRows.Item("O_ZAI_REC_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo))
                        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

                        'START YANAI 要望番号681
                    ElseIf .optKowake.Checked = True AndAlso
                            0 < drZai.Length Then
                        '小分け出荷選択時、在庫が小分けで作成されたデータの場合
                        drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                             String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                           "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                           "SYS_DEL_FLG = '0'")
                                                 )
                        If newFlg = True Then
                            '新規作成からの流れの時
                            drOutS2 = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                                 String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                               "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                               "SYS_DEL_FLG = '0' AND ",
                                                               "SMPL_FLAG = '01'")
                                                     )

                        Else
                            '編集モードからの流れの時
                            drOutS2 = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                                 String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                               "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                               "SYS_DEL_FLG = '0' AND ",
                                                               "SMPL_FLAG = '01' AND ",
                                                               "UP_KBN = '0'")
                                                     )
                        End If

                        'START YANAI 要望番号772
                        'insRows.Item("ALCTD_CAN_NB") = "1"
                        'START YANAI 要望番号811
                        'insRows.Item("ALCTD_CAN_NB") = "0"
                        If Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))) = 0 Then
                            '全量出荷の場合は"0"
                            insRows.Item("ALCTD_CAN_NB") = "0"
                        Else
                            '全量出荷でない場合は"1"
                            insRows.Item("ALCTD_CAN_NB") = "1"
                        End If
                        'END YANAI 要望番号811
                        'END YANAI 要望番号772
                        insRows.Item("ALCTD_NB") = "0"
                        insRows.Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        insRows.Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                        insRows.Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        insRows.Item("ALCTD_NB_GAMEN") = "0"
                        insRows.Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        insRows.Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                        insRows.Item("ALCTD_CAN_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        insRows.Item("ALCTD_NB_MATOME") = "0"
                        insRows.Item("ALCTD_CAN_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo))
                        insRows.Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                        insRows.Item("O_ZAI_REC_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo))
                        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

                        kosuFlg = False
                        'END YANAI 要望番号681
                    ElseIf .optKowake.Checked = True Then
                        'START YANAI 20110913 小分け対応
                        drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                                                     String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                                                   "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                                                   "SYS_DEL_FLG = '0'")
                                                                         )
                        If newFlg = True Then
                            '新規作成からの流れの時
                            drOutS2 = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                                 String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                               "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                               "SYS_DEL_FLG = '0' AND ",
                                                               "SMPL_FLAG = '01'")
                                                     )

                        Else
                            '編集モードからの流れの時
                            drOutS2 = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(
                                                 String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                                               "ZAI_REC_NO = '", _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo)), "' AND ",
                                                               "SYS_DEL_FLG = '0' AND ",
                                                               "SMPL_FLAG = '01' AND ",
                                                               "UP_KBN = '0'")
                                                     )
                        End If

                        'START YANAI 要望番号681
                        'If 0 < drOutS2.Length Then
                        '    max = drOutS2.Length - 1
                        '    For j As Integer = 0 To max
                        '        sumAlctdQt = sumAlctdQt + Convert.ToDecimal(drOutS2(j).Item("ALCTD_QT"))
                        '    Next
                        'End If
                        'END YANAI 要望番号681

                        'START YANAI 要望番号681
                        'insRows.Item("ALCTD_CAN_NB") = Convert.ToString( _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))) - _
                        '                                1 - _
                        '                                drOutS.Length _
                        '                               )
                        insRows.Item("ALCTD_CAN_NB") = Convert.ToString(
                                                        Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))) -
                                                        drOutS.Length
                                                       )
                        'END YANAI 要望番号681
                        'START YANAI 要望番号681
                        'insRows.Item("ALCTD_NB") = "1"
                        insRows.Item("ALCTD_NB") = "0"
                        'END YANAI 要望番号681
                        'START YANAI 要望番号681
                        'insRows.Item("ALCTD_CAN_QT") = Convert.ToString( _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))) + _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))) - _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) - _
                        '                                (Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) * drOutS.Length) + _
                        '                                sumAlctdQt _
                        '                               )

                        'START YANAI 要望番号772
                        'insRows.Item("ALCTD_CAN_QT") = Convert.ToString( _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))) + _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) - _
                        '                                Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))) - _
                        '                                (Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) * drOutS.Length) + _
                        '                                sumAlctdQt _
                        '                               )
                        insRows.Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        'END YANAI 要望番号772
                        'END YANAI 要望番号681
                        insRows.Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        insRows.Item("ALCTD_NB_GAMEN") = "0"
                        insRows.Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        insRows.Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        'START YANAI 要望番号681
                        'insRows.Item("ALCTD_NB_MATOME") = "1"
                        insRows.Item("ALCTD_NB_MATOME") = "0"
                        'END YANAI 要望番号681
                        insRows.Item("ALCTD_CAN_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo))
                        insRows.Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        kosuFlg = False
                        'END YANAI 20110913 小分け対応

                        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                        insRows.Item("O_ZAI_REC_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo))
                        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

                    Else
                        'START YANAI 要望番号500
                        'insRows.Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        drZai = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(
                                 String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ",
                                               "ZAI_REC_NO = '", insRows.Item("ZAI_REC_NO").ToString, "' AND ",
                                               "SYS_DEL_FLG = '0' AND ",
                                               "MATOME_FLG = '1' AND ",
                                               "UP_KBN = '0'"))

                        If drZai.Length > 0 Then
                            'まとめ処理時
                            insRows.Item("ALCTD_CAN_NB") = Convert.ToString(
                                                                            Convert.ToDecimal(_LMCconG.GetCellValue(Me._frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_NB_HOZON.ColNo))) -
                                                                            Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo)))
                                                                           )
                        Else
                            'まとめ処理以外
                            insRows.Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        End If
                        'END YANAI 要望番号500
                        insRows.Item("ALCTD_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                        'START YANAI 要望番号500
                        'insRows.Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        If drZai.Length > 0 Then
                            'まとめ処理時
                            insRows.Item("ALCTD_CAN_QT") = Convert.ToString(
                                                                            Convert.ToDecimal(_LMCconG.GetCellValue(Me._frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo))) -
                                                                            Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo)))
                                                                           )
                        Else
                            'まとめ処理以外
                            insRows.Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        End If
                        'END YANAI 要望番号500
                        insRows.Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        insRows.Item("ALCTD_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                        insRows.Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        insRows.Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        insRows.Item("ALCTD_CAN_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        insRows.Item("ALCTD_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                        insRows.Item("ALCTD_CAN_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo))
                        insRows.Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                    End If

                    insRows.Item("IRIME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))

                    drOutM = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                    , "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))

                    '重量 = 【商品M】標準重量 × ( 【出荷ﾃﾞｰﾀS】入目 ÷ 【商品M】入目 )
                    insRows.Item("BETU_WT") = Convert.ToDecimal(drOutM(0).Item("STD_WT_KGS").ToString) *
                                               (Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) /
                                               Convert.ToDecimal(drOutM(0).Item("STD_IRIME_NB").ToString))

                    insRows.Item("COA_FLAG") = "00"
                    insRows.Item("REMARK") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.REMARK.ColNo))
                    If .optKowake.Checked = True Then
                        insRows.Item("SMPL_FLAG") = "01"
                    Else
                        insRows.Item("SMPL_FLAG") = "00"
                    End If
                    insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    insRows.Item("UP_KBN") = "0"

                    insRows.Item("MATOME_FLG") = "0"

                    If String.IsNullOrEmpty(drOutM(0).Item("UP_KBN").ToString) = True Then
                        '出荷(中)のUP_KBNが空ということは、既に一度保存したデータに対して、小を追加したということなので、
                        '出荷(中)の更新時間等を更新するために、ここでUP_KBNを"1"にしておく。
                        drOutM(0).Item("UP_KBN") = "1"
                    End If

                    insRows.Item("INKA_DATE") = DateFormatUtility.DeleteSlash(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKO_DATE.ColNo)))  'ADD 2018/11/14 要望管理001939

                    'データセットに追加
                    ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Add(insRows)

                Else
                    '更新の場合
                    dr(0).Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                    dr(0).Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
                    dr(0).Item("OUTKA_NO_M") = .lblSyukkaMNo.TextValue
                    dr(0).Item("OUTKA_NO_S") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo))
                    dr(0).Item("TOU_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.TOU_NO.ColNo))
                    dr(0).Item("SITU_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SHITSU_NO.ColNo))
                    dr(0).Item("ZONE_CD") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZONE_CD.ColNo))
                    dr(0).Item("LOCA") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOCA.ColNo))
                    dr(0).Item("LOT_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.LOT_NO.ColNo)).ToUpper()
                    dr(0).Item("SERIAL_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.SERIAL_NO.ColNo))
                    dr(0).Item("OUTKA_TTL_NB") = .numSouKosu.Value
                    dr(0).Item("OUTKA_TTL_QT") = .numSouSuryo.Value
                    dr(0).Item("ZAI_REC_NO") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ZAI_REC_NO.ColNo))
                    dr(0).Item("INKA_NO_L") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_L.ColNo))
                    dr(0).Item("INKA_NO_M") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_M.ColNo))
                    dr(0).Item("INKA_NO_S") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKA_NO_S.ColNo))
                    'START YANAI 20110913 小分け対応
                    'dr(0).Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                    'dr(0).Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                    'If ("00").Equals(dr(0).Item("SMPL_FLAG").ToString) = True Then
                    '    '変更前が小分け以外の場合
                    '    dr(0).Item("ALCTD_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                    '    dr(0).Item("ALCTD_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                    'Else
                    '    '変更前が小分けの場合
                    '    dr(0).Item("ALCTD_NB") = "1"
                    '    dr(0).Item("ALCTD_NB_GAMEN") = "0"
                    'End If
                    'dr(0).Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                    'dr(0).Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                    'dr(0).Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                    'dr(0).Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                    If .optKowake.Checked = True AndAlso _
                        (_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo))).Equals(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))) = True AndAlso _
                        Convert.ToDouble(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))) <> 0 Then
                        dr(0).Item("ALCTD_CAN_NB") = "0"
                        dr(0).Item("ALCTD_NB") = "1"
                        dr(0).Item("ALCTD_CAN_QT") = "0"
                        dr(0).Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_GAMEN") = "0"
                        dr(0).Item("ALCTD_NB_GAMEN") = "1"
                        dr(0).Item("ALCTD_CAN_QT_GAMEN") = "0"
                        dr(0).Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_MATOME") = "0"
                        dr(0).Item("ALCTD_NB_MATOME") = "1"
                        dr(0).Item("ALCTD_CAN_QT_MATOME") = "0"
                        dr(0).Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                    ElseIf .optKowake.Checked = True Then
                        'START YANAI 20110913 小分け対応
                        dr(0).Item("ALCTD_CAN_NB") = dr(0).Item("ALCTD_CAN_NB") '変わらない
                        'START YANAI 要望番号681
                        'dr(0).Item("ALCTD_NB") = "1"
                        dr(0).Item("ALCTD_NB") = "0"
                        'END YANAI 要望番号681
                        dr(0).Item("ALCTD_CAN_QT") = dr(0).Item("ALCTD_CAN_QT") '変わらない
                        dr(0).Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        dr(0).Item("ALCTD_NB_GAMEN") = "0"
                        dr(0).Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        dr(0).Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        'START YANAI 要望番号681
                        'dr(0).Item("ALCTD_NB_MATOME") = "1"
                        dr(0).Item("ALCTD_NB_MATOME") = "0"
                        'END YANAI 要望番号681
                        dr(0).Item("ALCTD_CAN_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo))
                        dr(0).Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))
                        'END YANAI 20110913 小分け対応

                        kosuFlg = False

                    Else
                        'START YANAI 要望番号500
                        'dr(0).Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        If ("0").Equals(dr(0).Item("UP_KBN")) = True Then
                            drZai = ds.Tables(LMC020C.TABLE_NM_ZAI).Select( _
                                     String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                   "ZAI_REC_NO = '", dr(0).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                   "SYS_DEL_FLG = '0' AND ", _
                                                   "MATOME_FLG = '1' AND ", _
                                                   "UP_KBN = '0'"))

                            If drZai.Length > 0 Then
                                'まとめ処理時
                                dr(0).Item("ALCTD_CAN_NB") = Convert.ToString( _
                                                      Convert.ToDecimal(_LMCconG.GetCellValue(Me._frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_NB_HOZON.ColNo))) - _
                                                      Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))) _
                                                     )
                            Else
                                'まとめ処理以外
                                dr(0).Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                            End If
                        Else
                            dr(0).Item("ALCTD_CAN_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        End If
                        'END YANAI 要望番号500
                        dr(0).Item("ALCTD_NB") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                        'START YANAI 要望番号500
                        'dr(0).Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        If ("0").Equals(dr(0).Item("UP_KBN")) = True Then
                            If drZai.Length > 0 Then
                                'まとめ処理時
                                dr(0).Item("ALCTD_CAN_QT") = Convert.ToString( _
                                                                              Convert.ToDecimal(_LMCconG.GetCellValue(Me._frm.sprDtl.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT_HOZON.ColNo))) - _
                                                                              Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))) _
                                                                             )
                            Else
                                'まとめ処理以外
                                dr(0).Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                            End If
                        Else
                            dr(0).Item("ALCTD_CAN_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        End If
                        'END YANAI 要望番号500
                        dr(0).Item("ALCTD_QT") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB.ColNo))
                        dr(0).Item("ALCTD_NB_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB.ColNo))
                        dr(0).Item("ALCTD_CAN_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT.ColNo))
                        dr(0).Item("ALCTD_QT_GAMEN") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT.ColNo))

                        dr(0).Item("ALCTD_CAN_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        'START YANAI 要望番号500
                        'dr(0).Item("ALCTD_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_NB_MATOME.ColNo))
                        dr(0).Item("ALCTD_NB_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_NB_MATOME.ColNo))
                        'END YANAI 要望番号500
                        dr(0).Item("ALCTD_CAN_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_CAN_QT_MATOME.ColNo))
                        'START YANAI 要望番号459
                        dr(0).Item("ALCTD_QT_MATOME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.ALCTD_QT_MATOME.ColNo))
                        'END YANAI 要望番号459
                    End If
                    'END YANAI 20110913 小分け対応

                    dr(0).Item("IRIME") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))

                    drOutM = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                    "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))

                    '重量 = 【商品M】標準重量 × ( 【出荷ﾃﾞｰﾀS】入目 ÷ 【商品M】入目 )
                    dr(0).Item("BETU_WT") = Convert.ToDecimal(drOutM(0).Item("STD_WT_KGS").ToString) * _
                                               (Convert.ToDecimal(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo))) / _
                                               Convert.ToDecimal(drOutM(0).Item("STD_IRIME_NB").ToString))

                    dr(0).Item("REMARK") = _LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.REMARK.ColNo))
                    If .optKowake.Checked = True Then
                        dr(0).Item("SMPL_FLAG") = "01"
                    Else
                        dr(0).Item("SMPL_FLAG") = "00"
                    End If
                    If ("0").Equals(dr(0).Item("UP_KBN").ToString) = False Then
                        dr(0).Item("ZAI_UPD_FLAG") = "00"
                        dr(0).Item("UP_KBN") = "1"
                        drOutM(0).Item("UP_KBN") = "1"
                    End If

                    If .optKowake.Checked = True AndAlso _
                        (dr(0).Item("ALCTD_CAN_QT")).Equals(dr(0).Item("ALCTD_QT")) = False Then
                        kosuFlg = False
                    End If

                    'START YANAI 要望番号681
                    drZai = ds.Tables(LMC020C.TABLE_NM_ZAI).Select( _
                             String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                           "ZAI_REC_NO = '", dr(0).Item("ZAI_REC_NO").ToString, "' AND ", _
                                           "SMPL_FLAG_ZAI = '01' AND ", _
                                           "SYS_DEL_FLG = '0' AND ", _
                                           "UP_KBN = '0'"))
                    If 0 < drZai.Length Then
                        dr(0).Item("SMPL_FLG_ZAI") = "01"
                    Else
                        dr(0).Item("SMPL_FLG_ZAI") = "00"
                    End If
                    'END YANAI 要望番号681

                    dr(0).Item("INKA_DATE") = DateFormatUtility.DeleteSlash(_LMCconG.GetCellValue(.sprDtl.ActiveSheet.Cells(i, sprDtl.INKO_DATE.ColNo)))    'ADD 2018/11/14 要望管理001939

                End If

            Next

            Dim outMdr As DataRow() = Nothing
            Dim outSdr As DataRow() = Nothing
            Dim outZaidr As DataRow() = Nothing
            If kosuFlg = True Then
                '実予在庫個数が全て"1"の時、引当単位を「個数」に変更する
                outMdr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                              , "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))
                outMdr(0).Item("ALCTD_KB") = "01"
                .optCnt.Checked = True

                If (eventShubetsu).Equals(LMC020C.EventShubetsu.HOZON) = False Then
                    'スプレッドに反映させる
                    max = .sprSyukkaM.ActiveSheet.Rows.Count - 1
                    Dim recNo As Integer = Convert.ToInt32(.lblRecNo.TextValue)

                    For i As Integer = 0 To max
                        If Convert.ToString(recNo).Equals(_LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.REC_NO.ColNo))) = True Then
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI.ColNo, .optCnt.Text)
                            'START YANAI 要望番号681
                            .sprSyukkaM.SetCellValue(i, sprSyukkaM.SYUKKA_TANI_KOWAKE.ColNo, "01")
                            .lblTaniKowake.TextValue = "01"
                            'END YANAI 要望番号681
                            Exit For
                        End If
                    Next
                End If

                'START YANAI 要望番号711
                '小分けの全量出荷で区分が「小分け」から「個数」になる場合は、引当個数に"1"を設定する
                outSdr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                              , "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))
                max = outSdr.Length - 1
                For i As Integer = 0 To max
                    outSdr(i).Item("ALCTD_NB") = "1"
                    outSdr(i).Item("ALCTD_NB_GAMEN") = "1"
                    outZaidr = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                                    , "ZAI_REC_NO = '", outSdr(i).Item("ZAI_REC_NO").ToString(), "'"))
                    If 0 < outZaidr.Length Then
                        outZaidr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(outZaidr(0).Item("ALCTD_NB_HOZON").ToString) + 1)
                        outZaidr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(outZaidr(0).Item("ALLOC_CAN_NB_HOZON").ToString) - 1)
                    End If

                Next
                max = .sprDtl.ActiveSheet.Rows.Count - 1
                For i As Integer = 0 To max
                    .sprDtl.SetCellValue(i, sprDtl.ALCTD_NB.ColNo, "1")
                Next
                'END YANAI 要望番号711

            End If

            'START YANAI 要望番号681
            If .optKowake.Checked = True Then
                '小分けチェック時は出荷(中)の出荷包装個数に0を設定する
                outMdr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                              , "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))
                outMdr(0).Item("OUTKA_PKG_NB") = "0"
            End If
            'END YANAI 要望番号681

        End With

        Return True

    End Function

    '2015.07.21 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マークテーブルの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetMarkDataSet(ByVal ds As DataSet, ByVal activeRow As Integer, ByVal eventShubetsu As LMC020C.EventShubetsu)

        Dim drDtl As DataRow() = Nothing
        Dim seqNo As String = String.Empty
        Dim max As Integer = 8
        Dim drM As DataRow() = Nothing

        With Me._frm

            '荷主明細
            drM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                              "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                                                             , "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND " _
                                                                             , "SUB_KB = '00'"))
            '列数設定
            If drM.Length > 0 Then
                max = Convert.ToInt32(drM(0).Item("SET_NAIYO").ToString())
            End If

            If String.IsNullOrEmpty(.txtMarkInfo1.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo2.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo3.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo4.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo5.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo6.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo7.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtMarkInfo8.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo9.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo10.TextValue) = True Then
                Exit Sub
            End If

            If Convert.ToInt32(Right(.txtMarkInfo1.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo1.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo1.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo1.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo2.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo2.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo2.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo2.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo3.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo3.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo3.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo3.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo4.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo4.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo4.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo4.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo5.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo5.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo5.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo5.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo6.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo6.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo6.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo6.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo7.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo7.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo7.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo7.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo8.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo8.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo8.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo8.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo9.Name, 1)) <= max Then
                seqNo = String.Concat("00", Right(.txtMarkInfo9.Name, 1))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo9.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo9.TextValue(), seqNo, _frm)
                End If
            End If
            If Convert.ToInt32(Right(.txtMarkInfo10.Name, 2)) <= max Then
                seqNo = String.Concat("0", Right(.txtMarkInfo10.Name, 2))
                drDtl = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "MARK_EDA = '", seqNo, "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))

                If drDtl.Length > 0 Then
                    ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(activeRow, sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                              , "MARK_EDA = '", seqNo, "' AND " _
                                                                                              , "SYS_DEL_FLG = '0'"))(0).Item("REMARK_INFO") = .txtMarkInfo10.TextValue()
                Else
                    Me.SetMarkDtlDataSet(ds, .txtMarkInfo10.TextValue(), seqNo, _frm)
                End If
            End If

        End With

    End Sub
    '2015.07.21 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(DTL)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetMarkHedDataSet(ByVal ds As DataSet, ByVal activeRow As Integer, ByVal frm As LMC020F)

        Dim hedRows As DataRow = Nothing

        With frm

            hedRows = ds.Tables(LMC020C.TABLE_NM_MARK_HED).NewRow
            hedRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            hedRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
            hedRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()
            hedRows.Item("CASE_NO_FROM") = .numCaseNoFrom.Value()
            hedRows.Item("CASE_NO_TO") = .numCaseNoTo.Value()
            hedRows.Item("UP_KBN") = "0"
            hedRows.Item("SYS_DEL_FLG") = "0" '新規
            'データセットに追加
            ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Add(hedRows)

            .sprSyukkaM.SetCellValue(activeRow, LMC020G.sprSyukkaM.CASE_NO_FROM.ColNo, .numCaseNoFrom.Value)
            .sprSyukkaM.SetCellValue(activeRow, LMC020G.sprSyukkaM.CASE_NO_TO.ColNo, .numCaseNoTo.Value)

        End With

    End Sub

    '2015.07.21 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(DTL)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetMarkDtlDataSet(ByVal ds As DataSet, ByVal markInfoVal As String, ByVal seqNo As String, ByVal frm As LMC020F)

        Dim dtlRows As DataRow = Nothing

        With frm

            dtlRows = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).NewRow
            dtlRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            dtlRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
            dtlRows.Item("OUTKA_NO_M") = .txtOutkaNoM.TextValue()
            dtlRows.Item("MARK_EDA") = seqNo
            dtlRows.Item("REMARK_INFO") = markInfoVal
            dtlRows.Item("UP_KBN") = "0"
            dtlRows.Item("SYS_DEL_FLG") = "0" '新規
            'データセットに追加
            ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Add(dtlRows)

        End With

    End Sub
    '2015.07.21 協立化学　シッピング対応 追加END

    ''' <summary>
    ''' 作業(大レコード)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSagyoLDataSet(ByVal ds As DataSet) As Boolean

        With Me._frm

            Dim dr As DataRow() = Nothing
            dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                        "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, "000'"))

            Dim gMax As Integer = dr.Length - 1

            If -1 < gMax Then
                For i As Integer = 0 To gMax
                    dr(i).Item("UP_KBN2") = "0"
                Next
            End If

            Dim sagyoCd As String = String.Empty
            Dim sagyoNm As String = String.Empty
            Dim sagyoRmk As String = String.Empty
            Dim nrsBrCd As String = Convert.ToString(.cmbEigyosyo.SelectedValue)
            Dim custCd As String = .txtCust_Cd_L.TextValue
            Dim destFlg As String = String.Empty
            Dim sagyoDr As DataRow() = Nothing
            Dim insRows As DataRow = Nothing
            Dim outMdt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)

            Dim mCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", outMdt.Rows(0).Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                                             "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                             "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                             "CUST_CD_S = '00' AND ", _
                                                                                                             "CUST_CD_SS = '00'"))
            Dim mSagyo As DataRow() = Nothing
            Dim maxS As Integer = 0
            Dim lotCnt As Integer = 0

            For i As Integer = 1 To LMC020C.SAGYO_L_REC_CNT
                Select Case i
                    Case 1
                        '1レコード目
                        sagyoCd = .txtSagyoL1.TextValue
                        sagyoNm = .lblSagyoL1.TextValue
                        sagyoRmk = .txtSagyoRemarkL1.TextValue
                        destFlg = "00"
                    Case 2
                        '2レコード目
                        sagyoCd = .txtSagyoL2.TextValue
                        sagyoNm = .lblSagyoL2.TextValue
                        sagyoRmk = .txtSagyoRemarkL2.TextValue
                        destFlg = "00"
                    Case 3
                        '3レコード目
                        sagyoCd = .txtSagyoL3.TextValue
                        sagyoNm = .lblSagyoL3.TextValue
                        sagyoRmk = .txtSagyoRemarkL3.TextValue
                        destFlg = "00"
                    Case 4
                        '4レコード目
                        sagyoCd = .txtSagyoL4.TextValue
                        sagyoNm = .lblSagyoL4.TextValue
                        sagyoRmk = .txtSagyoRemarkL4.TextValue
                        destFlg = "00"
                    Case 5
                        '5レコード目
                        sagyoCd = .txtSagyoL5.TextValue
                        sagyoNm = .lblSagyoL5.TextValue
                        sagyoRmk = .txtSagyoRemarkL5.TextValue
                        destFlg = "00"
                End Select

                If String.IsNullOrEmpty(sagyoCd.ToString) = False Then
                    sagyoDr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select( _
                                          String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                        "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, "000' AND ", _
                                                        "SAGYO_CD = '", sagyoCd, "'"))
                    If (0).Equals(sagyoDr.Length) = True Then
                        '0件の場合は追加
                        insRows = ds.Tables(LMC020C.TABLE_NM_SAGYO).NewRow
                        'START YANAI 要望番号376
                        'mSagyo = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                        '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                        '                                                                               , "CUST_CD_L = '", custCd, "'"))
                        mSagyo = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                       , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                       , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                        'END YANAI 要望番号376

                        If (LMC020C.SINTYOKU60.Equals(.cmbSagyoSintyoku.SelectedValue) = True OrElse _
                                LMC020C.SINTYOKU90.Equals(.cmbSagyoSintyoku.SelectedValue) = True) Then
                            insRows.Item("SAGYO_COMP") = "01"
                        Else
                            insRows.Item("SAGYO_COMP") = "00"
                        End If
                        insRows.Item("SKYU_CHK") = "00"
                        insRows.Item("SAGYO_REC_NO") = String.Empty
                        insRows.Item("SAGYO_SIJI_NO") = String.Empty
                        insRows.Item("INOUTKA_NO_LM") = String.Concat(.lblSyukkaLNo.TextValue, "000")
                        insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                        insRows.Item("WH_CD") = .cmbSoko.SelectedValue
                        insRows.Item("IOZS_KB") = "20"
                        insRows.Item("SAGYO_CD") = sagyoCd.ToString
                        insRows.Item("SAGYO_NM") = mSagyo(0).Item("SAGYO_NM").ToString
                        insRows.Item("REMARK_SIJI") = sagyoRmk.ToString
                        insRows.Item("DEST_SAGYO_FLG") = destFlg
                        insRows.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                        insRows.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
                        insRows.Item("DEST_CD") = .txtTodokesakiCd.TextValue
                        insRows.Item("DEST_NM") = .txtTodokesakiNm.TextValue
                        insRows.Item("GOODS_CD_NRS") = String.Empty
                        insRows.Item("GOODS_NM_NRS") = String.Empty
                        insRows.Item("LOT_NO") = String.Empty
                        insRows.Item("INV_TANI") = mSagyo(0).Item("INV_TANI").ToString
                        If ("01").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            insRows.Item("SAGYO_NB") = "1"
                        ElseIf ("02").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            insRows.Item("SAGYO_NB") = outMdt.Rows(0).Item("OUTKA_TTL_NB").ToString
                        ElseIf ("03").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            maxS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Count - 1
                            lotCnt = 0
                            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
                            '出荷（小）に同じロットナンバーのものがあった場合、その個数はカウントしない処理追加
                            Dim stockLotNo As New Hashtable
                            For j As Integer = 0 To maxS
                                Dim targetLotNo As String = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString
                                If String.IsNullOrEmpty(targetLotNo) = False Then
                                    If stockLotNo.ContainsKey(targetLotNo) = False Then
                                        lotCnt = lotCnt + 1
                                        stockLotNo.Add(targetLotNo, String.Empty)
                                    End If
                                End If
                            Next
                            'For j As Integer = 0 To maxS
                            '    If String.IsNullOrEmpty(ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString) = False Then
                            '        lotCnt = lotCnt + 1
                            '    End If
                            'Next
                            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd end 
                            insRows.Item("SAGYO_NB") = lotCnt
                        ElseIf ("04").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            insRows.Item("SAGYO_NB") = .numKonpoKosu.Value
                        End If
                        If String.IsNullOrEmpty(insRows.Item("SAGYO_NB").ToString) = True OrElse _
                            ("0").Equals(insRows.Item("SAGYO_NB").ToString) = True Then
                            insRows.Item("SAGYO_NB") = outMdt.Rows(0).Item("OUTKA_M_PKG_NB").ToString
                        End If
                        If String.IsNullOrEmpty(insRows.Item("SAGYO_NB").ToString) = True OrElse _
                            ("0").Equals(insRows.Item("SAGYO_NB").ToString) = True Then
                            insRows.Item("SAGYO_NB") = "1"
                        End If
                        insRows.Item("SAGYO_UP") = mSagyo(0).Item("SAGYO_UP").ToString
                        'START YANAI メモ②No.16
                        'insRows.Item("SAGYO_GK") = Convert.ToDecimal(insRows.Item("SAGYO_NB").ToString) * Convert.ToDecimal(insRows.Item("SAGYO_UP").ToString)
                        insRows.Item("SAGYO_GK") = Convert.ToString(Math.Round(Convert.ToDecimal(insRows.Item("SAGYO_NB").ToString) * Convert.ToDecimal(insRows.Item("SAGYO_UP").ToString), MidpointRounding.AwayFromZero))
                        'END YANAI メモ②No.16
                        insRows.Item("TAX_KB") = mSagyo(0).Item("ZEI_KBN").ToString
                        insRows.Item("SEIQTO_CD") = mCust(0).Item("SAGYO_SEIQTO_CD").ToString
                        insRows.Item("REMARK_ZAI") = String.Empty
                        insRows.Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                        insRows.Item("SAGYO_COMP_CD") = String.Empty
                        insRows.Item("SAGYO_COMP_DATE") = String.Empty
                        insRows.Item("SAGYO_RYAK") = sagyoNm.ToString
                        insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                        insRows.Item("UP_KBN") = "0"
                        insRows.Item("UP_KBN2") = "1"

                        '追加
                        ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Add(insRows)
                    Else
                        '存在する場合
                        'START YANAI 要望番号376
                        'mSagyo = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                        '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                        '                                                                               , "CUST_CD_L = '", custCd, "'"))
                        mSagyo = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                       , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                       , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                        'END YANAI 要望番号376

                        If (LMC020C.SINTYOKU60.Equals(.cmbSagyoSintyoku.SelectedValue) = True OrElse _
                            LMC020C.SINTYOKU90.Equals(.cmbSagyoSintyoku.SelectedValue) = True) Then
                            sagyoDr(0).Item("SAGYO_COMP") = "01"
                        Else
                            sagyoDr(0).Item("SAGYO_COMP") = "00"
                        End If
                        sagyoDr(0).Item("SAGYO_NM") = mSagyo(0).Item("SAGYO_NM").ToString
                        sagyoDr(0).Item("REMARK_SIJI") = sagyoRmk.ToString
                        sagyoDr(0).Item("DEST_CD") = .txtTodokesakiCd.TextValue
                        sagyoDr(0).Item("DEST_NM") = .txtTodokesakiNm.TextValue
                        sagyoDr(0).Item("LOT_NO") = String.Empty
                        sagyoDr(0).Item("INV_TANI") = mSagyo(0).Item("INV_TANI").ToString
                        If ("01").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            sagyoDr(0).Item("SAGYO_NB") = "1"
                        ElseIf ("02").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            sagyoDr(0).Item("SAGYO_NB") = outMdt.Rows(0).Item("OUTKA_TTL_NB").ToString
                        ElseIf ("03").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            maxS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Count - 1
                            lotCnt = 0
                            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
                            '出荷（小）に同じロットナンバーのものがあった場合、その個数はカウントしない処理追加
                            Dim stockLotNo As New Hashtable
                            For j As Integer = 0 To maxS
                                Dim targetLotNo As String = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString
                                If String.IsNullOrEmpty(targetLotNo) = False Then
                                    If stockLotNo.ContainsKey(targetLotNo) = False Then
                                        lotCnt = lotCnt + 1
                                        stockLotNo.Add(targetLotNo, String.Empty)
                                    End If
                                End If
                            Next
                            'For j As Integer = 0 To maxS
                            '    If String.IsNullOrEmpty(ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString) = False Then
                            '        lotCnt = lotCnt + 1
                            '    End If
                            'Next
                            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd end 
                            sagyoDr(0).Item("SAGYO_NB") = lotCnt
                        ElseIf ("04").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                            sagyoDr(0).Item("SAGYO_NB") = .numKonpoKosu.Value
                        End If
                        If String.IsNullOrEmpty(sagyoDr(0).Item("SAGYO_NB").ToString) = True OrElse _
                            ("0").Equals(sagyoDr(0).Item("SAGYO_NB").ToString) = True Then
                            sagyoDr(0).Item("SAGYO_NB") = outMdt.Rows(0).Item("OUTKA_M_PKG_NB").ToString
                        End If
                        If String.IsNullOrEmpty(sagyoDr(0).Item("SAGYO_NB").ToString) = True OrElse _
                            ("0").Equals(sagyoDr(0).Item("SAGYO_NB").ToString) = True Then
                            sagyoDr(0).Item("SAGYO_NB") = "1"
                        End If
                        sagyoDr(0).Item("SAGYO_UP") = mSagyo(0).Item("SAGYO_UP").ToString
                        'START YANAI メモ②No.16
                        'sagyoDr(0).Item("SAGYO_GK") = Convert.ToDecimal(sagyoDr(0).Item("SAGYO_NB").ToString) * Convert.ToDecimal(sagyoDr(0).Item("SAGYO_UP").ToString)
                        sagyoDr(0).Item("SAGYO_GK") = Convert.ToString(Math.Round(Convert.ToDecimal(sagyoDr(0).Item("SAGYO_NB").ToString) * Convert.ToDecimal(sagyoDr(0).Item("SAGYO_UP").ToString), MidpointRounding.AwayFromZero))
                        'END YANAI メモ②No.16
                        sagyoDr(0).Item("TAX_KB") = mSagyo(0).Item("ZEI_KBN").ToString
                        sagyoDr(0).Item("SEIQTO_CD") = mCust(0).Item("SAGYO_SEIQTO_CD").ToString
                        sagyoDr(0).Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                        sagyoDr(0).Item("SAGYO_RYAK") = sagyoNm.ToString

                        'sagyoDr(0).Item("UP_KBN") = "1"
                        sagyoDr(0).Item("UP_KBN2") = "1"
                        sagyoDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                    End If

                End If
            Next i

            If -1 < gMax Then
                For i As Integer = 0 To gMax
                    If ("0").Equals(dr(i).Item("UP_KBN2")) = True Then
                        '更新されていないレコードがあったら、画面にないということなので、削除
                        If ("0").Equals(dr(i).Item("UP_KBN")) = True Then
                            ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Remove(dr(i))
                            'i = i - 1
                            'gMax = gMax - 1

                        Else
                            dr(i).Item("UP_KBN") = "2"
                            dr(i).Item("UP_KBN2") = "1"
                            dr(i).Item("SYS_DEL_FLG") = LMConst.FLG.ON

                        End If
                    End If
                Next
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 作業(中レコード)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSagyoMDataSet(ByVal ds As DataSet) As Boolean

        With Me._frm

#If True Then   'ADD 2020/0218 010582【LMS】出荷編集時の梱包数変更を作業数に変更してほしい
            Dim CovanceFLG As Boolean = False   '千葉コーヴァンス限定処理
            If ("00787").Equals(.txtCust_Cd_L.TextValue.Trim) _
                AndAlso ("10").Equals(.cmbEigyosyo.SelectedValue) Then
                CovanceFLG = True
            End If

#End If

            Dim remarkDr As DataRow = Nothing
            Dim gMax As Integer = ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Count - 1
            'START YANAI 要望番号1019
            Dim outkaMdr As DataRow() = Nothing
            'END YANAI 要望番号1019

            If String.IsNullOrEmpty(.lblSyukkaMNo.TextValue) = True AndAlso _
                String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = True Then
                '編集ボタン(F2)押下後、すぐに保存ボタンを押した時の対応用

                '出荷(中)の注文番号が入力されていなかったら、出荷(大)の注文番号を設定するという処理の過程で、
                '最後に出荷(大)の注文番号を入力されると、更新されない作業のデータが出てきてしまうので、ここで改めて設定をする。
                If String.IsNullOrEmpty(.txtNisyuTyumonNo.TextValue) = False Then
                    gMax = ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Count - 1
                    For i As Integer = 0 To gMax
                        remarkDr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows(i)
                        If String.IsNullOrEmpty(remarkDr.Item("REMARK_SKYU").ToString) = True AndAlso _
                            ("2").Equals(remarkDr.Item("UP_KBN")) = False Then
                            '大の注文番号が入力されている且つ削除データは対象外
                            remarkDr.Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                            'remarkDr.Item("UP_KBN") = "1"
                            remarkDr.Item("UP_KBN2") = "1"
                        End If
                    Next
                End If

                Exit Function
            End If

            Dim dr As DataRow() = Nothing
            dr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                        "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, .lblSyukkaMNo.TextValue, "'"))
            gMax = dr.Length - 1

            If String.IsNullOrEmpty(.lblGoodsCdNrs.TextValue) = False Then
                '商品空でない場合。（空の場合は、編集ボタン押して、すぐに保存ボタン押した時）
                Dim mGoods As DataRow() = Nothing
                Dim mCust As DataRow() = Nothing
                mCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                "NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                "CUST_CD_L = '", Me._frm.txtCust_Cd_L.TextValue, "' AND ", _
                                                "CUST_CD_M = '", Me._frm.txtCust_Cd_M.TextValue, "' AND ", _
                                                "CUST_CD_S = '", Me._frm.lblCustCdS.TextValue, "' AND ", _
                                                "CUST_CD_SS = '", Me._frm.lblCustCdSS.TextValue, "'"))

                If -1 < gMax Then
                    For i As Integer = 0 To gMax
                        dr(i).Item("UP_KBN2") = "0"
                    Next
                End If

                Dim sagyoCd As String = String.Empty
                Dim sagyoNm As String = String.Empty
                Dim sagyoRmk As String = String.Empty
                Dim nrsBrCd As String = Convert.ToString(.cmbEigyosyo.SelectedValue)
                Dim custCd As String = .txtCust_Cd_L.TextValue
                Dim destFlg As String = String.Empty
                Dim sagyoDr As DataRow() = Nothing
                Dim insRows As DataRow = Nothing
                Dim maxS As Integer = 0
                Dim lotCnt As Integer = 0

                For i As Integer = 1 To LMC020C.SAGYO_M_REC_CNT
                    Select Case i
                        Case 1
                            '1レコード目
                            sagyoCd = .txtSagyoM1.TextValue
                            sagyoNm = .lblSagyoM1.TextValue
                            sagyoRmk = .txtSagyoRemarkM1.TextValue
                            destFlg = "00"
                        Case 2
                            '2レコード目
                            sagyoCd = .txtSagyoM2.TextValue
                            sagyoNm = .lblSagyoM2.TextValue
                            sagyoRmk = .txtSagyoRemarkM2.TextValue
                            destFlg = "00"
                        Case 3
                            '3レコード目
                            sagyoCd = .txtSagyoM3.TextValue
                            sagyoNm = .lblSagyoM3.TextValue
                            sagyoRmk = .txtSagyoRemarkM3.TextValue
                            destFlg = "00"
                        Case 4
                            '4レコード目
                            sagyoCd = .txtSagyoM4.TextValue
                            sagyoNm = .lblSagyoM4.TextValue
                            sagyoRmk = .txtSagyoRemarkM4.TextValue
                            destFlg = "00"
                        Case 5
                            '5レコード目
                            sagyoCd = .txtSagyoM5.TextValue
                            sagyoNm = .lblSagyoM5.TextValue
                            sagyoRmk = .txtSagyoRemarkM5.TextValue
                            destFlg = "00"
                        Case 6
                            '6レコード目
                            sagyoCd = .txtDestSagyoM1.TextValue
                            sagyoNm = .lblDestSagyoM1.TextValue
                            sagyoRmk = .txtDestSagyoRemarkM1.TextValue
                            destFlg = "01"
                        Case 7
                            '7レコード目
                            sagyoCd = .txtDestSagyoM2.TextValue
                            sagyoNm = .lblDestSagyoM2.TextValue
                            sagyoRmk = .txtDestSagyoRemarkM2.TextValue
                            destFlg = "01"
                    End Select

                    If String.IsNullOrEmpty(sagyoCd.ToString) = False Then
                        sagyoDr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select( _
                                              String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                            "INOUTKA_NO_LM = '", .lblSyukkaLNo.TextValue, .lblSyukkaMNo.TextValue, "' AND ", _
                                                            "SAGYO_CD = '", sagyoCd, "' AND ", _
                                                            "DEST_SAGYO_FLG = '", destFlg, "'"))

                        If (0).Equals(sagyoDr.Length) = True Then
                            '0件の場合は追加
                            insRows = ds.Tables(LMC020C.TABLE_NM_SAGYO).NewRow
                            'START YANAI 要望番号376
                            'Dim mSagyo As DataRow()  = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                            '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                            '                                                                               , "CUST_CD_L = '", custCd, "'"))
                            Dim mSagyo As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                           , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                           , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                            If mSagyo.Length = 0 Then
                                Return False
                            End If
                            'END YANAI 要望番号376

                            If (LMC020C.SINTYOKU60.Equals(.cmbSagyoSintyoku.SelectedValue) = True OrElse _
                                    LMC020C.SINTYOKU90.Equals(.cmbSagyoSintyoku.SelectedValue) = True) Then
                                insRows.Item("SAGYO_COMP") = "01"
                            Else
                                insRows.Item("SAGYO_COMP") = "00"
                            End If
                            insRows.Item("SKYU_CHK") = "00"
                            insRows.Item("SAGYO_REC_NO") = String.Empty
                            insRows.Item("SAGYO_SIJI_NO") = String.Empty
                            insRows.Item("INOUTKA_NO_LM") = String.Concat(.lblSyukkaLNo.TextValue, .lblSyukkaMNo.TextValue)
                            insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                            insRows.Item("WH_CD") = .cmbSoko.SelectedValue
                            insRows.Item("IOZS_KB") = "21"
                            insRows.Item("SAGYO_CD") = sagyoCd.ToString
                            insRows.Item("SAGYO_NM") = mSagyo(0).Item("SAGYO_NM").ToString
                            insRows.Item("REMARK_SIJI") = sagyoRmk.ToString
                            insRows.Item("DEST_SAGYO_FLG") = destFlg
                            insRows.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                            insRows.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
                            insRows.Item("DEST_CD") = .txtTodokesakiCd.TextValue
                            insRows.Item("DEST_NM") = .txtTodokesakiNm.TextValue

                            'START YANAI 要望番号1019
                            'If String.IsNullOrEmpty(.lblGoodsCdNrsFrom.TextValue) = False Then
                            '    insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrsFrom.TextValue
                            'Else
                            '    insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                            'End If
                            If String.IsNullOrEmpty(.lblGoodsCdNrsFrom.TextValue) = False Then
                                outkaMdr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                  "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "' "))

                                If String.IsNullOrEmpty(outkaMdr(0).Item("INSUPD_FLG").ToString) = True Then
                                    '他荷主で、編集モードの場合
                                    insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                                Else
                                    '他荷主で、新規作成モードの場合
                                    insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrsFrom.TextValue
                                End If
                            Else
                                '他荷主以外の場合
                                insRows.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                            End If
                            'END YANAI 要望番号1019

                            insRows.Item("GOODS_NM_NRS") = .lblGoodsNm.TextValue
                            insRows.Item("LOT_NO") = .txtLotNo.TextValue.ToUpper()
                            insRows.Item("INV_TANI") = mSagyo(0).Item("INV_TANI").ToString
                            'insRows.Item("SAGYO_NB") = .sprDtl.ActiveSheet.Rows.Count
                            If ("01").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                insRows.Item("SAGYO_NB") = "1"
                            ElseIf ("02").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                insRows.Item("SAGYO_NB") = .numSouKosu.Value
                            ElseIf ("03").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                maxS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Count - 1
                                lotCnt = 0
                                '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
                                '出荷（小）に同じロットナンバーのものがあった場合、その個数はカウントしない処理追加
                                Dim stockLotNo As New Hashtable
                                For j As Integer = 0 To maxS
                                    Dim targetLotNo As String = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString
                                    If String.IsNullOrEmpty(targetLotNo) = False Then
                                        If stockLotNo.ContainsKey(targetLotNo) = False Then
                                            lotCnt = lotCnt + 1
                                            stockLotNo.Add(targetLotNo, String.Empty)
                                        End If
                                    End If
                                Next
                                'For j As Integer = 0 To maxS
                                '    If String.IsNullOrEmpty(ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString) = False Then
                                '        lotCnt = lotCnt + 1
                                '    End If
                                'Next
                                '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd end 
                                insRows.Item("SAGYO_NB") = lotCnt
                            ElseIf ("04").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                insRows.Item("SAGYO_NB") = .numPkgCnt.Value
                            End If
                            If String.IsNullOrEmpty(insRows.Item("SAGYO_NB").ToString) = True OrElse _
                                ("0").Equals(insRows.Item("SAGYO_NB").ToString) = True Then
                                insRows.Item("SAGYO_NB") = .numPkgCnt.Value
                            End If
                            If String.IsNullOrEmpty(insRows.Item("SAGYO_NB").ToString) = True OrElse _
                                ("0").Equals(insRows.Item("SAGYO_NB").ToString) = True Then
                                insRows.Item("SAGYO_NB") = "1"
                            End If
                            insRows.Item("SAGYO_UP") = mSagyo(0).Item("SAGYO_UP").ToString

#If True Then   'ADD 2020/0218 010582【LMS】出荷編集時の梱包数変更を作業数に変更してほしい
                            If CovanceFLG = True Then
                                If ("01").Equals(insRows.Item("INV_TANI").ToString) Then
                                    '請求単位が"01"(個)の時
                                    insRows.Item("SAGYO_NB") = .numKonpoKosu.Value
                                Else
                                    '請求単位が"01"(個)以外
                                    insRows.Item("SAGYO_NB") = "1"
                                End If
                            End If
#End If
                            'START YANAI メモ②No.16
                            'insRows.Item("SAGYO_GK") = Convert.ToDecimal(insRows.Item("SAGYO_NB").ToString) * Convert.ToDecimal(insRows.Item("SAGYO_UP").ToString)
                            insRows.Item("SAGYO_GK") = Convert.ToString(Math.Round(Convert.ToDecimal(insRows.Item("SAGYO_NB").ToString) * Convert.ToDecimal(insRows.Item("SAGYO_UP").ToString), MidpointRounding.AwayFromZero))
                            'END YANAI メモ②No.16
                            insRows.Item("TAX_KB") = mSagyo(0).Item("ZEI_KBN").ToString
                            insRows.Item("SEIQTO_CD") = mCust(0).Item("SAGYO_SEIQTO_CD").ToString
                            insRows.Item("REMARK_ZAI") = String.Empty
                            If String.IsNullOrEmpty(.txtOrderNo.TextValue) = False Then
                                insRows.Item("REMARK_SKYU") = .txtOrderNo.TextValue
                            Else
                                insRows.Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                            End If
                            insRows.Item("SAGYO_COMP_CD") = String.Empty
                            insRows.Item("SAGYO_COMP_DATE") = String.Empty
                            insRows.Item("SAGYO_RYAK") = sagyoNm.ToString
                            insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                            insRows.Item("UP_KBN") = "0"
                            insRows.Item("UP_KBN2") = "1"

                            '追加
                            ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Add(insRows)
                        Else
                            '存在する場合
                            'START YANAI 要望番号376
                            'Dim mSagyo As DataRow()  = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                            '                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
                            '                                                                               , "CUST_CD_L = '", custCd, "'"))
                            Dim mSagyo As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                           , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                           , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
                            'END YANAI 要望番号376

                            If (LMC020C.SINTYOKU60.Equals(.cmbSagyoSintyoku.SelectedValue) = True OrElse _
                                LMC020C.SINTYOKU90.Equals(.cmbSagyoSintyoku.SelectedValue) = True) Then
                                sagyoDr(0).Item("SAGYO_COMP") = "01"
                            Else
                                sagyoDr(0).Item("SAGYO_COMP") = "00"
                            End If
                            sagyoDr(0).Item("SAGYO_NM") = mSagyo(0).Item("SAGYO_NM").ToString
                            sagyoDr(0).Item("REMARK_SIJI") = sagyoRmk.ToString
                            sagyoDr(0).Item("DEST_CD") = .txtTodokesakiCd.TextValue
                            sagyoDr(0).Item("DEST_NM") = .txtTodokesakiNm.TextValue

                            'START YANAI 要望番号1019
                            'If String.IsNullOrEmpty(.lblGoodsCdNrsFrom.TextValue) = False Then
                            '    sagyoDr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrsFrom.TextValue
                            'Else
                            '    sagyoDr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                            'End If
                            If String.IsNullOrEmpty(.lblGoodsCdNrsFrom.TextValue) = False Then
                                outkaMdr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                  "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "' "))

                                If String.IsNullOrEmpty(outkaMdr(0).Item("INSUPD_FLG").ToString) = True Then
                                    '他荷主で、編集モードの場合
                                    sagyoDr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                                Else
                                    '他荷主で、新規作成モードの場合
                                    sagyoDr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrsFrom.TextValue
                                End If
                            Else
                                '他荷主以外の場合
                                sagyoDr(0).Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                            End If
                            'END YANAI 要望番号1019

                            sagyoDr(0).Item("GOODS_NM_NRS") = .lblGoodsNm.TextValue
                            sagyoDr(0).Item("LOT_NO") = .txtLotNo.TextValue.ToUpper()
                            sagyoDr(0).Item("INV_TANI") = mSagyo(0).Item("INV_TANI").ToString
                            'sagyoDr(0).Item("SAGYO_NB") = .sprDtl.ActiveSheet.Rows.Count
                            If ("01").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                sagyoDr(0).Item("SAGYO_NB") = "1"
                            ElseIf ("02").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                sagyoDr(0).Item("SAGYO_NB") = .numSouKosu.Value
                            ElseIf ("03").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                maxS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Count - 1
                                lotCnt = 0
                                '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
                                Dim stockLotNo As New Hashtable
                                For j As Integer = 0 To maxS
                                    Dim targetLotNo As String = ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString
                                    If String.IsNullOrEmpty(targetLotNo) = False Then
                                        If stockLotNo.ContainsKey(targetLotNo) = False Then
                                            lotCnt = lotCnt + 1
                                            stockLotNo.Add(targetLotNo, String.Empty)
                                        End If
                                    End If
                                Next
                                'For j As Integer = 0 To maxS
                                '    If String.IsNullOrEmpty(ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows(j).Item("LOT_NO").ToString) = False Then
                                '        lotCnt = lotCnt + 1
                                '    End If
                                'Next
                                '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
                                sagyoDr(0).Item("SAGYO_NB") = lotCnt
                            ElseIf ("04").Equals(mSagyo(0).Item("KOSU_BAI").ToString) = True Then
                                sagyoDr(0).Item("SAGYO_NB") = .numPkgCnt.Value
                            End If
                            If String.IsNullOrEmpty(sagyoDr(0).Item("SAGYO_NB").ToString) = True OrElse _
                                ("0").Equals(sagyoDr(0).Item("SAGYO_NB").ToString) = True Then
                                sagyoDr(0).Item("SAGYO_NB") = .numPkgCnt.Value
                            End If
                            If String.IsNullOrEmpty(sagyoDr(0).Item("SAGYO_NB").ToString) = True OrElse _
                                ("0").Equals(sagyoDr(0).Item("SAGYO_NB").ToString) = True Then
                                sagyoDr(0).Item("SAGYO_NB") = "1"
                            End If

#If True Then   'ADD 2020/0218 010582【LMS】出荷編集時の梱包数変更を作業数に変更してほしい
                            If CovanceFLG = True Then
                                If ("01").Equals(mSagyo(0).Item("INV_TANI").ToString) Then
                                    '請求単位が"01"(個)の時
                                    sagyoDr(0).Item("SAGYO_NB") = .numKonpoKosu.Value
                                Else
                                    '請求単位が"01"(個)以外
                                    sagyoDr(0).Item("SAGYO_NB") = "1"
                                End If
                            End If
#End If
                            sagyoDr(0).Item("SAGYO_UP") = mSagyo(0).Item("SAGYO_UP").ToString
                            'START YANAI メモ②No.16
                            'sagyoDr(0).Item("SAGYO_GK") = Convert.ToDecimal(sagyoDr(0).Item("SAGYO_NB").ToString) * Convert.ToDecimal(sagyoDr(0).Item("SAGYO_UP").ToString)
                            sagyoDr(0).Item("SAGYO_GK") = Convert.ToString(Math.Round(Convert.ToDecimal(sagyoDr(0).Item("SAGYO_NB").ToString) * Convert.ToDecimal(sagyoDr(0).Item("SAGYO_UP").ToString), MidpointRounding.AwayFromZero))
                            'END YANAI メモ②No.16
                            sagyoDr(0).Item("TAX_KB") = mSagyo(0).Item("ZEI_KBN").ToString
                            sagyoDr(0).Item("SEIQTO_CD") = mCust(0).Item("SAGYO_SEIQTO_CD").ToString
                            If String.IsNullOrEmpty(.txtOrderNo.TextValue) = False Then
                                sagyoDr(0).Item("REMARK_SKYU") = .txtOrderNo.TextValue
                            Else
                                sagyoDr(0).Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                            End If
                            sagyoDr(0).Item("SAGYO_RYAK") = sagyoNm.ToString

                            'sagyoDr(0).Item("UP_KBN") = "1"
                            sagyoDr(0).Item("UP_KBN2") = "1"
                            sagyoDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                        End If

                    End If
                Next i

                If -1 < gMax Then
                    For i As Integer = 0 To gMax
                        If ("0").Equals(dr(i).Item("UP_KBN2")) = True Then
                            '更新されていないレコードがあったら、画面にないということなので、削除
                            If ("0").Equals(dr(i).Item("UP_KBN")) = True Then
                                ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Remove(dr(i))
                                'i = i - 1
                                'gMax = gMax - 1

                            Else
                                dr(i).Item("UP_KBN") = "2"
                                dr(i).Item("UP_KBN2") = "1"
                                dr(i).Item("SYS_DEL_FLG") = LMConst.FLG.ON

                            End If
                        End If
                    Next
                End If

            End If

            '出荷(中)の注文番号が入力されていなかったら、出荷(大)の注文番号を設定するという処理の過程で、
            '最後に出荷(大)の注文番号を入力されると、更新されない作業のデータが出てきてしまうので、ここで改めて設定をする。
            remarkDr = Nothing
            If String.IsNullOrEmpty(.txtNisyuTyumonNo.TextValue) = False Then
                gMax = ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Count - 1
                For i As Integer = 0 To gMax
                    remarkDr = ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows(i)
                    If String.IsNullOrEmpty(remarkDr.Item("REMARK_SKYU").ToString) = True AndAlso _
                        ("2").Equals(remarkDr.Item("UP_KBN")) = False Then
                        '大の注文番号が入力されている且つ削除データは対象外
                        remarkDr.Item("REMARK_SKYU") = .txtNisyuTyumonNo.TextValue
                        'remarkDr.Item("UP_KBN") = "1"
                        remarkDr.Item("UP_KBN2") = "1"
                    End If
                Next
            End If

        End With

        Return True

    End Function

    '要望番号:1731 terakawa 2013.01.15 Start
    ''' <summary>
    ''' サンプル作業コードの値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetSagyoMSample() As Boolean

        With Me._frm

            Dim sagyoCd As String = String.Empty
            Dim sagyoNm As String = String.Empty
            Dim sampleSagyo As String = String.Empty
            Dim exitFlg As Boolean = False

            'サンプル作業コードを取得
            Dim mCust As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                      "NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                      "CUST_CD_L = '", Me._frm.txtCust_Cd_L.TextValue, "' AND ", _
                                      "CUST_CD_M = '", Me._frm.txtCust_Cd_M.TextValue, "' AND ", _
                                      "CUST_CD_S = '", Me._frm.lblCustCdS.TextValue, "' AND ", _
                                      "CUST_CD_SS = '", Me._frm.lblCustCdSS.TextValue, "'"))
            sampleSagyo = mCust(0).Item("SMPL_SAGYO").ToString()

            'サンプル作業コードが取得できない場合、設定は行わない
            If String.IsNullOrEmpty(sampleSagyo) = False Then

                'サンプル作業名を取得
                Dim mSagyo As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat( _
                                          "SAGYO_CD = '", sampleSagyo, "'"))
                Dim sampleSagyoNm As String = mSagyo(0).Item("SAGYO_RYAK").ToString()

                For i As Integer = 1 To LMC020C.SAGYO_M_REC_CNT
                    Select Case i
                        Case 1
                            '1レコード目
                            sagyoCd = .txtSagyoM1.TextValue
                        Case 2
                            '2レコード目
                            sagyoCd = .txtSagyoM2.TextValue
                        Case 3
                            '3レコード目
                            sagyoCd = .txtSagyoM3.TextValue
                        Case 4
                            '4レコード目
                            sagyoCd = .txtSagyoM4.TextValue
                        Case 5
                            '5レコード目
                            sagyoCd = .txtSagyoM5.TextValue
                        Case 6
                            '6レコード目
                            sagyoCd = .txtDestSagyoM1.TextValue
                        Case 7
                            '7レコード目
                            sagyoCd = .txtDestSagyoM2.TextValue
                    End Select

                    If String.IsNullOrEmpty(sagyoCd.ToString) = True Then

                        Select Case i
                            Case 1
                                '1レコード目
                                .txtSagyoM1.TextValue = sampleSagyo
                                .lblSagyoM1.TextValue = sampleSagyoNm
                            Case 2
                                '2レコード目
                                .txtSagyoM2.TextValue = sampleSagyo
                                .lblSagyoM2.TextValue = sampleSagyoNm
                            Case 3
                                '3レコード目
                                .txtSagyoM3.TextValue = sampleSagyo
                                .lblSagyoM3.TextValue = sampleSagyoNm
                            Case 4
                                '4レコード目
                                .txtSagyoM4.TextValue = sampleSagyo
                                .lblSagyoM4.TextValue = sampleSagyoNm
                            Case 5
                                '5レコード目
                                .txtSagyoM5.TextValue = sampleSagyo
                                .lblSagyoM5.TextValue = sampleSagyoNm
                            Case 6
                                '6レコード目
                                .txtDestSagyoM1.TextValue = sampleSagyo
                                .lblDestSagyoM1.TextValue = sampleSagyoNm
                            Case 7
                                '7レコード目
                                .txtDestSagyoM2.TextValue = sampleSagyo
                                .lblDestSagyoM2.TextValue = sampleSagyoNm
                        End Select

                        exitFlg = True
                    Else
                        If sagyoCd.Equals(sampleSagyo) = True Then
                            'サンプル作業コードがすでに設定されていた場合、設定を行わない。
                            exitFlg = True
                        End If
                    End If

                    If exitFlg = True Then
                        Exit For
                    End If
                Next
            End If
        End With

        Return True

    End Function
    '要望番号:1731 terakawa 2013.01.15 End

    ''' <summary>
    ''' 運送(大)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnsoLDataSet(ByVal ds As DataSet) As Boolean

        Dim rtnResutl As Boolean = True

        With Me._frm

            '採番されていない時は常にinsertするようにデータセットをクリアする。
            If String.IsNullOrEmpty(.txtUnsoNo.TextValue) = True Then
                ds.Tables(LMC020C.TABLE_NM_UNSO_L).Clear()
            End If

            '運送サブの計算処理を呼び出す
            'Dim unsoH As LMFControlH = New LMFControlH(Me._frm, LMC020C.PGID_LMC020)

            Dim outdr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_L).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                            "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "'"))
            Dim sokodr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                              "WH_CD = '", .cmbSoko.SelectedValue, "'"))
            Dim mUnsodr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
                                               , "UNSOCO_CD = '", .txtUnsoCompanyCd.TextValue, "' AND " _
                                               , "UNSOCO_BR_CD = '", .txtUnsoSitenCd.TextValue, "'"))

            Dim dr As DataRow = Nothing
            If (0).Equals(ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Count) = True Then
                '新規の場合

                Dim insRows As DataRow = ds.Tables(LMC020C.TABLE_NM_UNSO_L).NewRow

                insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                insRows.Item("UNSO_NO_L") = String.Empty
                insRows.Item("YUSO_BR_CD") = .cmbYusoBrCd.SelectedValue
                insRows.Item("INOUTKA_NO_L") = outdr(0).Item("OUTKA_NO_L").ToString
                insRows.Item("TRIP_NO") = String.Empty
                insRows.Item("UNSO_CD") = .txtUnsoCompanyCd.TextValue
                insRows.Item("UNSO_BR_CD") = .txtUnsoSitenCd.TextValue
                insRows.Item("TARE_YN") = .lblUnsoTareYn.TextValue
                insRows.Item("BIN_KB") = .cmbBinKbn.SelectedValue
                insRows.Item("JIYU_KB") = String.Empty
                insRows.Item("DENP_NO") = outdr(0).Item("DENP_NO").ToString
                '要望番号:2408 2015.09.17 追加START
                Me.GetAutoDenpSet(Me._frm)
                insRows.Item("AUTO_DENP_NO") = .lblAutoDenpNo.TextValue
                insRows.Item("AUTO_DENP_KBN") = .cmbAutoDenpKbn.SelectedValue
                '要望番号:2408 2015.09.17 追加END
                insRows.Item("OUTKA_PLAN_DATE") = outdr(0).Item("OUTKA_PLAN_DATE").ToString
                insRows.Item("OUTKA_PLAN_TIME") = String.Empty
                insRows.Item("ARR_PLAN_DATE") = outdr(0).Item("ARR_PLAN_DATE").ToString
                insRows.Item("ARR_PLAN_TIME") = outdr(0).Item("ARR_PLAN_TIME").ToString
                insRows.Item("ARR_ACT_TIME") = String.Empty
                insRows.Item("CUST_CD_L") = outdr(0).Item("CUST_CD_L").ToString
                insRows.Item("CUST_CD_M") = outdr(0).Item("CUST_CD_M").ToString
                insRows.Item("CUST_REF_NO") = outdr(0).Item("CUST_ORD_NO").ToString
                insRows.Item("SHIP_CD") = outdr(0).Item("SHIP_CD_L").ToString
                insRows.Item("ORIG_CD") = sokodr(0).Item("UNSO_HATTI_CD").ToString
                insRows.Item("DEST_CD") = outdr(0).Item("DEST_CD").ToString
                'START YANAI 要望番号473
                'insRows.Item("UNSO_PKG_NB") = unsoH.SetNbData(ds.Tables(LMC020C.TABLE_NM_UNSO_M)).ToString()
                insRows.Item("UNSO_PKG_NB") = outdr(0).Item("OUTKA_PKG_NB").ToString
                'END YANAI 要望番号473
                insRows.Item("NB_UT") = String.Empty
                insRows.Item("UNSO_ONDO_KB") = .cmbUnsoOndo.SelectedValue
                insRows.Item("PC_KB") = .cmbMotoCyakuKbn.SelectedValue
                insRows.Item("TARIFF_BUNRUI_KB") = .cmbTariffKbun.SelectedValue
                insRows.Item("VCLE_KB") = .cmbSyaryoKbn.SelectedValue
                insRows.Item("MOTO_DATA_KB") = "20"
                insRows.Item("TAX_KB") = .cmbUnsoKazeiKbn.SelectedValue
                insRows.Item("REMARK") = .txtHaisoRemark.TextValue
                insRows.Item("SEIQ_TARIFF_CD") = .txtUnthinTariffCd.TextValue
                insRows.Item("SEIQ_ETARIFF_CD") = .txtExtcTariffCd.TextValue
                insRows.Item("AD_3") = outdr(0).Item("DEST_AD_3").ToString
                insRows.Item("UNSO_TEHAI_KB") = .cmbTehaiKbn.SelectedValue
                insRows.Item("BUY_CHU_NO") = outdr(0).Item("BUYER_ORD_NO").ToString
                insRows.Item("AREA_CD") = String.Empty
                insRows.Item("TYUKEI_HAISO_FLG") = "00"
                insRows.Item("SYUKA_TYUKEI_CD") = String.Empty
                insRows.Item("HAIKA_TYUKEI_CD") = String.Empty
                insRows.Item("TRIP_NO_SYUKA") = String.Empty
                insRows.Item("TRIP_NO_TYUKEI") = String.Empty
                insRows.Item("TRIP_NO_HAIKA") = String.Empty

                insRows.Item("UNSOCO_NM") = .lblUnsoCompanyNm.TextValue
                insRows.Item("UNSOCO_BR_NM") = .lblUnsoSitenNm.TextValue
                insRows.Item("SEIQ_TARIFF_NM") = .lblUnthinTariffNm.TextValue
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                insRows.Item("SHIHARAI_TARIFF_CD") = .txtPayUnthinTariffCd.TextValue
                insRows.Item("SHIHARAI_ETARIFF_CD") = .txtPayExtcTariffCd.TextValue
                insRows.Item("SHIHARAI_TARIFF_NM") = .lblPayUnthinTariffNm.TextValue
                'END UMANO 要望番号1302 支払運賃に伴う修正。

                insRows.Item("UP_KBN") = "0"
                insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                insRows.Item("TORIKESI_FLG") = LMConst.FLG.OFF

                If mUnsodr.Count - 1 < 0 Then
                    insRows.Item("NIHUDA_YN") = String.Empty
                Else
                    insRows.Item("NIHUDA_YN") = mUnsodr(0).Item("NIHUDA_YN").ToString
                End If
                insRows.Item("TARE_YN") = .lblUnsoTareYn.TextValue

                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Add(insRows)
                dr = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(0)
            Else
                '更新の場合
                Dim drs As DataRow() = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                              "UNSO_NO_L = '", .txtUnsoNo.TextValue, "'"))

                drs(0).Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                drs(0).Item("UNSO_NO_L") = .txtUnsoNo.TextValue
                drs(0).Item("YUSO_BR_CD") = .cmbYusoBrCd.SelectedValue
                drs(0).Item("INOUTKA_NO_L") = outdr(0).Item("OUTKA_NO_L").ToString
                drs(0).Item("UNSO_CD") = .txtUnsoCompanyCd.TextValue
                drs(0).Item("UNSO_BR_CD") = .txtUnsoSitenCd.TextValue
                drs(0).Item("BIN_KB") = .cmbBinKbn.SelectedValue
                drs(0).Item("DENP_NO") = outdr(0).Item("DENP_NO").ToString
                '要望番号:2408 2015.09.17 追加START
                Me.GetAutoDenpSet(Me._frm)
                drs(0).Item("AUTO_DENP_NO") = .lblAutoDenpNo.TextValue
                drs(0).Item("AUTO_DENP_KBN") = .cmbAutoDenpKbn.SelectedValue
                '要望番号:2408 2015.09.17 追加END
                drs(0).Item("OUTKA_PLAN_DATE") = outdr(0).Item("OUTKA_PLAN_DATE").ToString
                drs(0).Item("ARR_PLAN_DATE") = outdr(0).Item("ARR_PLAN_DATE").ToString
                drs(0).Item("ARR_PLAN_TIME") = outdr(0).Item("ARR_PLAN_TIME").ToString
                drs(0).Item("CUST_CD_L") = outdr(0).Item("CUST_CD_L").ToString
                drs(0).Item("CUST_CD_M") = outdr(0).Item("CUST_CD_M").ToString
                drs(0).Item("CUST_REF_NO") = outdr(0).Item("CUST_ORD_NO").ToString
                drs(0).Item("SHIP_CD") = outdr(0).Item("SHIP_CD_L").ToString
                drs(0).Item("ORIG_CD") = sokodr(0).Item("UNSO_HATTI_CD").ToString
                drs(0).Item("DEST_CD") = outdr(0).Item("DEST_CD").ToString
                'START YANAI 要望番号473
                'drs(0).Item("UNSO_PKG_NB") = unsoH.SetNbData(ds.Tables(LMC020C.TABLE_NM_UNSO_M)).ToString()
                drs(0).Item("UNSO_PKG_NB") = outdr(0).Item("OUTKA_PKG_NB").ToString
                'END YANAI 要望番号473
                drs(0).Item("UNSO_ONDO_KB") = .cmbUnsoOndo.SelectedValue
                drs(0).Item("PC_KB") = .cmbMotoCyakuKbn.SelectedValue
                drs(0).Item("TARIFF_BUNRUI_KB") = .cmbTariffKbun.SelectedValue
                drs(0).Item("VCLE_KB") = .cmbSyaryoKbn.SelectedValue
                drs(0).Item("MOTO_DATA_KB") = "20"
                drs(0).Item("TAX_KB") = .cmbUnsoKazeiKbn.SelectedValue
                drs(0).Item("REMARK") = .txtHaisoRemark.TextValue
                drs(0).Item("SEIQ_TARIFF_CD") = .txtUnthinTariffCd.TextValue
                drs(0).Item("SEIQ_ETARIFF_CD") = .txtExtcTariffCd.TextValue
                drs(0).Item("AD_3") = outdr(0).Item("DEST_AD_3").ToString
                drs(0).Item("UNSO_TEHAI_KB") = .cmbTehaiKbn.SelectedValue
                drs(0).Item("BUY_CHU_NO") = outdr(0).Item("BUYER_ORD_NO").ToString

                drs(0).Item("UNSOCO_NM") = .lblUnsoCompanyNm.TextValue
                drs(0).Item("UNSOCO_BR_NM") = .lblUnsoSitenNm.TextValue
                drs(0).Item("SEIQ_TARIFF_NM") = .lblUnthinTariffNm.TextValue

                'START UMANO 要望番号1302 支払運賃に伴う修正。
                drs(0).Item("SHIHARAI_TARIFF_CD") = .txtPayUnthinTariffCd.TextValue
                drs(0).Item("SHIHARAI_ETARIFF_CD") = .txtPayExtcTariffCd.TextValue
                drs(0).Item("SHIHARAI_TARIFF_NM") = .lblPayUnthinTariffNm.TextValue
                'END UMANO 要望番号1302 支払運賃に伴う修正。

                drs(0).Item("UP_KBN") = "1"
                drs(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                drs(0).Item("TORIKESI_FLG") = LMConst.FLG.OFF
                drs(0).Item("TARE_YN") = .lblUnsoTareYn.TextValue

                If mUnsodr.Count - 1 < 0 Then
                    drs(0).Item("NIHUDA_YN") = String.Empty
                Else
                    drs(0).Item("NIHUDA_YN") = mUnsodr(0).Item("NIHUDA_YN").ToString
                End If
                dr = drs(0)
            End If

            '運送重量を求める
#If True Then   'UPD 2018/04/24 '群馬で引当前は、画面表示の運送重量をそのままで更新する 依頼番号 : 001204  
            Dim outsCnt As Integer = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0'").Count    'MOD 2018/11/12 要望番号001730 "SYS_DEL_FLG = '0'"での絞り込みを追加

            If .cmbEigyosyo.SelectedValue.Equals(LMC020C.BR_CD_GUNMA) _
                AndAlso (0).Equals(outsCnt) Then

                '運送重量の設定
                dr.Item("UNSO_WT") = .numJuryo.Value
            Else
                '運送重量を求める(numJuryoの書換え)
                rtnResutl = Me.SetWt(ds)

                '運送重量の設定
                dr.Item("UNSO_WT") = .numJuryo.Value

            End If

#Else
             rtnResutl = Me.SetWt(ds)

            '運送重量の設定
            dr.Item("UNSO_WT") = .numJuryo.Value

#End If

        End With

        Return rtnResutl

    End Function

    ''' <summary>
    ''' 運送重量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetWt(ByVal ds As DataSet) As Boolean

        With Me._frm

            '運送サブの計算処理を呼び出す
            Dim unsoH As LMFControlH = New LMFControlH(Me._frm, LMC020C.PGID_LMC020)

            Dim wt As Decimal = unsoH.GetJuryoOutkaData(ds _
                                                        , LMC020C.TABLE_NM_OUT_M _
                                                        , LMC020C.TABLE_NM_UNSO_L _
                                                        , LMC020C.TABLE_NM_OUT_S _
                                                        , "TARE_YN" _
                                                        , "PKG_UT" _
                                                        , "PKG_NB" _
                                                        , "STD_IRIME_NB" _
                                                        , "STD_WT_KGS" _
                                                        )

            '切り上げ
            wt = Math.Ceiling(wt)

            '運送重量の上限チェック
            If Me._V.IsHaniCheck(wt, Convert.ToDecimal(LMC020C.UNSO_JURYO_MIN), Convert.ToDecimal(LMC020C.UNSO_JURYO_MAX)) = False Then
                Me._LMCconV.SetErrMessage("E117", New String() {_frm.lblJuryo.TextValue, Convert.ToDecimal(LMC020C.UNSO_JURYO_MAX).ToString("#,##0")})
                Return False
            End If

            .numJuryo.Value = wt

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送(中)の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnsoMDataSet(ByVal ds As DataSet) As Boolean

        With Me._frm

            Dim unsoMDt As DataTable = ds.Tables(LMC020C.TABLE_NM_UNSO_M)
            Dim dr As DataRow = Nothing
            unsoMDt.Clear()

            'START YANAI 要望番号706
            'Dim outkaMDt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)
            'Dim outkaSDt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_S)
            'Dim outkaSDr As DataRow() = Nothing
            Dim outkaMDr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
            'END YANAI 要望番号706
            'START YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
            'Dim zbukaKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'Z019' AND ", _
            '                                                                                  "KBN_NM1 ='", .cmbEigyosyo.SelectedValue, "' AND ", _
            '                                                                                  "KBN_NM2 ='", .cmbSoko.SelectedValue, "'"))
            Dim zbukaKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'Z019' AND ", _
                                                                                              "KBN_NM1 ='", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                              "KBN_NM2 ='", .txtCust_Cd_L.TextValue, "'"))
            'END YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応


            'START YANAI 要望番号706
            'Dim max As Integer = outkaMDt.Rows.Count - 1
            Dim max As Integer = outkaMDr.Length - 1
            'END YANAI 要望番号706
            Dim smax As Integer = 0
            Dim juryo As Decimal = 0
            Dim insRows As DataRow = Nothing

            For i As Integer = 0 To max

                insRows = ds.Tables(LMC020C.TABLE_NM_UNSO_M).NewRow

                insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                insRows.Item("UNSO_NO_L") = .txtUnsoNo.TextValue
                'START YANAI 要望番号706
                'insRows.Item("UNSO_NO_M") = outkaMDt.Rows(i).Item("OUTKA_NO_M").ToString()
                insRows.Item("UNSO_NO_M") = outkaMDr(i).Item("OUTKA_NO_M").ToString()
                'END YANAI 要望番号706
                'START YANAI 要望番号422
                'insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                'START YANAI 要望番号510
                'If String.IsNullOrEmpty(outkaMDt.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()) = False Then
                '    insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()
                'Else
                '    insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                'End If
                'START YANAI 要望番号654
                'insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                'START YANAI 要望番号673
                'If String.IsNullOrEmpty(outkaMDt.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()) = False Then
                '    insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()
                'Else
                '    insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                'End If
                'START YANAI 要望番号706
                'insRows.Item("GOODS_CD_NRS") = outkaMDt.Rows(i).Item("GOODS_CD_NRS").ToString()
                'insRows.Item("GOODS_CD_NRS_FROM") = outkaMDt.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()
                insRows.Item("GOODS_CD_NRS") = outkaMDr(i).Item("GOODS_CD_NRS").ToString()
                insRows.Item("GOODS_CD_NRS_FROM") = outkaMDr(i).Item("GOODS_CD_NRS_FROM").ToString()
                'END YANAI 要望番号706
                'END YANAI 要望番号673
                'END YANAI 要望番号654
                'END YANAI 要望番号510
                'END YANAI 要望番号422
                'START YANAI 要望番号706
                'insRows.Item("GOODS_NM") = outkaMDt.Rows(i).Item("GOODS_NM").ToString()
                insRows.Item("GOODS_NM") = outkaMDr(i).Item("GOODS_NM").ToString()
                'END YANAI 要望番号706
                '( 出荷総個数 - 端数 ) / 入数
                'START YANAI 要望番号706
                'insRows.Item("UNSO_TTL_NB") = (Convert.ToDecimal(outkaMDt.Rows(i).Item("OUTKA_TTL_NB").ToString()) _
                '                              - Convert.ToDecimal(outkaMDt.Rows(i).Item("OUTKA_HASU").ToString())) _
                '                              / Convert.ToDecimal(outkaMDt.Rows(i).Item("PKG_NB").ToString())
                'insRows.Item("NB_UT") = outkaMDt.Rows(i).Item("NB_UT").ToString()
                'insRows.Item("UNSO_TTL_QT") = outkaMDt.Rows(i).Item("OUTKA_TTL_QT").ToString()
                'insRows.Item("QT_UT") = outkaMDt.Rows(i).Item("PKG_UT").ToString()
                'insRows.Item("HASU") = outkaMDt.Rows(i).Item("OUTKA_HASU").ToString()
                'insRows.Item("ZAI_REC_NO") = String.Empty
                'insRows.Item("UNSO_ONDO_KB") = outkaMDt.Rows(i).Item("UNSO_ONDO_KB").ToString()
                'insRows.Item("IRIME") = outkaMDt.Rows(i).Item("IRIME").ToString()
                'insRows.Item("IRIME_UT") = outkaMDt.Rows(i).Item("IRIME_UT").ToString()
                'insRows.Item("BETU_WT") = outkaMDt.Rows(i).Item("STD_WT_KGS").ToString()
                'insRows.Item("SIZE_KB") = outkaMDt.Rows(i).Item("SIZE_KB").ToString()

                'ADD START 2019/11/13  07667【LMS】エクシング運送EDIデータ出荷本数0 
                If Convert.ToDecimal(outkaMDr(i).Item("OUTKA_TTL_NB").ToString()) = Convert.ToDecimal(outkaMDr(i).Item("OUTKA_HASU").ToString()) Then
                    '「OUTKA_TTL_NB」と「OUTKA_HASU」が一致する場合
                    insRows.Item("UNSO_TTL_NB") = outkaMDr(i).Item("OUTKA_TTL_NB").ToString()
                Else
                    'ADD END 2019/11/13  07667【LMS】エクシング運送EDIデータ出荷本数0 
                    insRows.Item("UNSO_TTL_NB") = (Convert.ToDecimal(outkaMDr(i).Item("OUTKA_TTL_NB").ToString()) _
                                                  - Convert.ToDecimal(outkaMDr(i).Item("OUTKA_HASU").ToString())) _
                                                  / Convert.ToDecimal(outkaMDr(i).Item("PKG_NB").ToString())
                End If
                insRows.Item("NB_UT") = outkaMDr(i).Item("NB_UT").ToString()
                insRows.Item("UNSO_TTL_QT") = outkaMDr(i).Item("OUTKA_TTL_QT").ToString()
                insRows.Item("QT_UT") = outkaMDr(i).Item("PKG_UT").ToString()
                insRows.Item("HASU") = outkaMDr(i).Item("OUTKA_HASU").ToString()
                insRows.Item("ZAI_REC_NO") = String.Empty
                insRows.Item("UNSO_ONDO_KB") = outkaMDr(i).Item("UNSO_ONDO_KB").ToString()
                insRows.Item("IRIME") = outkaMDr(i).Item("IRIME").ToString()
                insRows.Item("IRIME_UT") = outkaMDr(i).Item("IRIME_UT").ToString()
                insRows.Item("BETU_WT") = outkaMDr(i).Item("STD_WT_KGS").ToString()
                insRows.Item("SIZE_KB") = outkaMDr(i).Item("SIZE_KB").ToString()
                'END YANAI 要望番号706

                'START YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応
                'If (0).Equals(zbukaKbn.Length) = False Then
                '    'START YANAI 要望番号706
                '    'insRows.Item("ZBUKA_CD") = Mid(outkaMDt.Rows(i).Item("SEARCH_KEY_1").ToString, 1, 8)
                '    insRows.Item("ZBUKA_CD") = Mid(outkaMDr(i).Item("SEARCH_KEY_1").ToString, 1, 8)
                '    'END YANAI 要望番号706
                'Else
                '    insRows.Item("ZBUKA_CD") = String.Empty
                'End If
                'insRows.Item("ABUKA_CD") = String.Empty
                If String.IsNullOrEmpty(outkaMDr(i).Item("ZBUKA_CD").ToString()) = True Then
                    If (0).Equals(zbukaKbn.Length) = False Then
                        'START YANAI 要望番号1308 出荷登録時、在庫部課コードは7文字で切り取る
                        'insRows.Item("ZBUKA_CD") = Mid(outkaMDr(i).Item("SEARCH_KEY_1").ToString, 1, 8)
                        insRows.Item("ZBUKA_CD") = Mid(outkaMDr(i).Item("SEARCH_KEY_1").ToString, 1, 7)
                        'END YANAI 要望番号1308 出荷登録時、在庫部課コードは7文字で切り取る
                    Else
                        insRows.Item("ZBUKA_CD") = String.Empty
                    End If
                Else
                    'outkaMDrの"ZBUKA_CD"には初期検索時、F_UNSO_Mに設定されているZBUKA_CDが設定されている。
                    'F_UNSO_Mに値が設定されていたら、値をそのまま設定する
                    insRows.Item("ZBUKA_CD") = outkaMDr(i).Item("ZBUKA_CD").ToString()
                End If
                'outkaMDrの"ABUKA_CD"には初期検索時、F_UNSO_Mに設定されているABUKA_CDが設定されている。
                'F_UNSO_Mに値が設定されていたら、値をそのまま設定する
                insRows.Item("ABUKA_CD") = outkaMDr(i).Item("ABUKA_CD").ToString()
                'END YANAI 要望番号1299 運送(中)の在庫部課・扱い部課保持対応

                'START YANAI 要望番号706
                'insRows.Item("PKG_NB") = outkaMDt.Rows(i).Item("PKG_NB").ToString()
                'insRows.Item("LOT_NO") = outkaMDt.Rows(i).Item("LOT_NO")
                insRows.Item("PKG_NB") = outkaMDr(i).Item("PKG_NB").ToString()
                insRows.Item("LOT_NO") = outkaMDr(i).Item("LOT_NO").ToString()
                'END YANAI 要望番号706
                insRows.Item("REMARK") = String.Empty
                insRows.Item("UP_KBN") = "0"
                insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_UNSO_M).Rows.Add(insRows)

            Next

        End With

        Return True

    End Function

    'START YANAI 要望番号888
    '''' <summary>
    '''' 在庫の値を設定（引当をしたタイミング）
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks></remarks>
    'Friend Sub SetZaiDataSet_hikiate(ByVal spr As LMSpread, ByVal dt As DataTable, ByVal ds As DataSet)
    ''' <summary>
    ''' 在庫の値を設定（引当をしたタイミング）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetZaiDataSet_hikiate(ByVal spr As LMSpread, ByVal dt As DataTable, ByVal ds As DataSet, ByVal taninusiFlg As Boolean)
        'END YANAI 要望番号888

        With Me._frm

            'START YANAI 要望番号853 まとめ処理対応
            Dim zaiMATOMEds As DataSet = Nothing
            Dim zaiRow2 As DataRow() = Nothing
            'END YANAI 要望番号853 まとめ処理対応
            Dim zaiRow As DataRow() = Nothing
            Dim insRows As DataRow = Nothing
            'START YANAI 要望番号853 まとめ処理対応
            Dim insMatomeRows As DataRow = Nothing
            'START YANAI 要望番号1064 出荷引当時の不具合
            'Dim poraZaiNbMATOME As Decimal = 0
            'Dim poraZaiQtMATOME As Decimal = 0
            'END YANAI 要望番号1064 出荷引当時の不具合
            Dim allocCanNbMATOME As Decimal = 0
            Dim allocCanQtMATOME As Decimal = 0
            Dim matomeMax As Integer = 0
            Dim drOutS As DataRow() = Nothing
            Dim outSMax As Integer = 0
            'END YANAI 要望番号853 まとめ処理対応

            Dim dRow As DataRow = Nothing
            Dim max As Integer = dt.Rows.Count - 1

            Dim drOutM As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                 "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "'"))

            If -1 < max Then
                For i As Integer = 0 To max
                    dRow = dt.Rows(i)
                    zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                  "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO"), "'"))
                    If zaiRow.Length = 0 Then
                        '同じ在庫レコード番号のデータなしの場合は、新規追加
                        insRows = ds.Tables(LMC020C.TABLE_NM_ZAI).NewRow

                        insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                        insRows.Item("ZAI_REC_NO") = dRow.Item("ZAI_REC_NO").ToString()
                        insRows.Item("WH_CD") = .cmbSoko.SelectedValue
                        insRows.Item("TOU_NO") = dRow.Item("TOU_NO").ToString()
                        insRows.Item("SITU_NO") = dRow.Item("SITU_NO").ToString()
                        insRows.Item("ZONE_CD") = dRow.Item("ZONE_CD").ToString()
                        insRows.Item("LOCA") = dRow.Item("LOCA").ToString()
                        insRows.Item("LOT_NO") = dRow.Item("LOT_NO").ToString()
                        insRows.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                        insRows.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
                        insRows.Item("GOODS_CD_NRS") = dRow.Item("GOODS_CD_NRS").ToString()
                        'START ADD 2013/09/10 KOBAYASHI WIT対応
                        insRows.Item("GOODS_KANRI_NO") = dRow.Item("GOODS_KANRI_NO").ToString()
                        'END   ADD 2013/09/10 KOBAYASHI WIT対応
                        insRows.Item("INKA_NO_L") = dRow.Item("INKA_NO_L").ToString()
                        insRows.Item("INKA_NO_M") = dRow.Item("INKA_NO_M").ToString()
                        insRows.Item("INKA_NO_S") = dRow.Item("INKA_NO_S").ToString()
                        insRows.Item("ALLOC_PRIORITY") = dRow.Item("ALLOC_PRIORITY").ToString()
                        insRows.Item("RSV_NO") = dRow.Item("RSV_NO").ToString()
                        insRows.Item("SERIAL_NO") = dRow.Item("SERIAL_NO").ToString()
                        insRows.Item("HOKAN_YN") = dRow.Item("HOKAN_YN").ToString()
                        insRows.Item("TAX_KB") = dRow.Item("TAX_KB").ToString()
                        insRows.Item("GOODS_COND_KB_1") = dRow.Item("GOODS_COND_KB_1").ToString()
                        insRows.Item("GOODS_COND_KB_2") = dRow.Item("GOODS_COND_KB_2").ToString()
                        insRows.Item("GOODS_COND_KB_3") = dRow.Item("GOODS_COND_KB_3").ToString()
                        insRows.Item("GOODS_COND_NM_1") = dRow.Item("GOODS_COND_NM_1").ToString()
                        insRows.Item("GOODS_COND_NM_2") = dRow.Item("GOODS_COND_NM_2").ToString()
                        insRows.Item("GOODS_COND_NM_3") = dRow.Item("GOODS_COND_NM_3").ToString()
                        insRows.Item("OFB_KB") = dRow.Item("OFB_KB").ToString()
                        insRows.Item("SPD_KB") = dRow.Item("SPD_KB").ToString()
                        insRows.Item("REMARK_OUT") = dRow.Item("REMARK_OUT").ToString()

                        If ("-1").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            If .optCnt.Checked = True Then
                                dRow.Item("ALCTD_KB") = "01"
                            ElseIf .optAmt.Checked = True Then
                                dRow.Item("ALCTD_KB") = "02"
                            ElseIf .optKowake.Checked = True Then
                                dRow.Item("ALCTD_KB") = "03"
                            ElseIf .optSample.Checked = True Then
                                dRow.Item("ALCTD_KB") = "04"
                            End If
                        End If


                        insRows.Item("INKO_DATE") = dRow.Item("INKO_DATE").ToString()
                        insRows.Item("INKO_PLAN_DATE") = dRow.Item("INKO_PLAN_DATE").ToString()
                        insRows.Item("ZERO_FLAG") = String.Empty
                        insRows.Item("LT_DATE") = dRow.Item("LT_DATE").ToString()
                        insRows.Item("GOODS_CRT_DATE") = dRow.Item("GOODS_CRT_DATE").ToString()
                        insRows.Item("DEST_CD_P") = dRow.Item("DEST_CD_P").ToString()
                        insRows.Item("REMARK") = dRow.Item("REMARK").ToString()
                        If ("03").Equals(dRow.Item("ALCTD_KB").ToString) = False Then
                            insRows.Item("SMPL_FLAG") = "00"
                        Else
                            insRows.Item("SMPL_FLAG") = "01"
                        End If
                        insRows.Item("SMPL_FLAG_ZAI") = dRow.Item("SMPL_FLAG").ToString.ToString()
                        insRows.Item("ALLOC_CAN_NB_HOZON") = dRow.Item("ALLOC_CAN_NB_HOZON").ToString()
                        insRows.Item("ALLOC_CAN_QT_HOZON") = dRow.Item("ALLOC_CAN_QT_HOZON").ToString()
                        insRows.Item("ALCTD_NB_HOZON") = dRow.Item("ALCTD_NB_HOZON").ToString()
                        insRows.Item("ALCTD_QT_HOZON") = dRow.Item("ALCTD_QT_HOZON").ToString()
                        insRows.Item("SYS_UPD_DATE") = dRow.Item("SYS_UPD_DATE").ToString()
                        insRows.Item("SYS_UPD_TIME") = dRow.Item("SYS_UPD_TIME").ToString()
                        insRows.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                        insRows.Item("UP_KBN") = "0"

                        '小分け先在庫判断用
                        If ("03").Equals(dRow.Item("ALCTD_KB")) = True Then
                            insRows.Item("ALCTD_KB_FLG") = "01"
                        Else
                            insRows.Item("ALCTD_KB_FLG") = "00"
                        End If

                        insRows.Item("INKA_DATE") = dRow.Item("INKA_DATE").ToString()
                        insRows.Item("IDO_DATE") = dRow.Item("IDO_DATE").ToString()
                        insRows.Item("HOKAN_STR_DATE") = dRow.Item("HOKAN_STR_DATE").ToString()
                        insRows.Item("IRIME") = dRow.Item("IRIME").ToString()
                        'START YANAI 要望番号780
                        insRows.Item("INKA_DATE_KANRI_KB") = dRow.Item("INKA_DATE_KANRI_KB").ToString()
                        'END YANAI 要望番号780

                        'START YANAI 要望番号853 まとめ処理対応
                        'まとめ処理用
                        ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).Clear()
                        insMatomeRows = ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).NewRow

                        insMatomeRows.Item("NRS_BR_CD") = insRows.Item("NRS_BR_CD")
                        insMatomeRows.Item("TOU_NO") = insRows.Item("TOU_NO").ToString()
                        insMatomeRows.Item("SITU_NO") = insRows.Item("SITU_NO").ToString()
                        insMatomeRows.Item("ZONE_CD") = insRows.Item("ZONE_CD").ToString()
                        insMatomeRows.Item("LOCA") = insRows.Item("LOCA").ToString()
                        insMatomeRows.Item("LOT_NO") = insRows.Item("LOT_NO").ToString()
                        insMatomeRows.Item("GOODS_CD_NRS") = insRows.Item("GOODS_CD_NRS")
                        insMatomeRows.Item("ALLOC_PRIORITY") = insRows.Item("ALLOC_PRIORITY")
                        insMatomeRows.Item("RSV_NO") = insRows.Item("RSV_NO").ToString()
                        insMatomeRows.Item("SERIAL_NO") = insRows.Item("SERIAL_NO").ToString()
                        insMatomeRows.Item("GOODS_COND_KB_1") = insRows.Item("GOODS_COND_KB_1").ToString()
                        insMatomeRows.Item("GOODS_COND_KB_2") = insRows.Item("GOODS_COND_KB_2").ToString()
                        insMatomeRows.Item("OFB_KB") = insRows.Item("OFB_KB").ToString()
                        insMatomeRows.Item("SPD_KB") = insRows.Item("SPD_KB").ToString()
                        insMatomeRows.Item("REMARK_OUT") = insRows.Item("REMARK_OUT").ToString()
                        insMatomeRows.Item("INKO_DATE") = insRows.Item("INKO_DATE").ToString()
                        insMatomeRows.Item("LT_DATE") = insRows.Item("LT_DATE").ToString()
                        insMatomeRows.Item("GOODS_CRT_DATE") = insRows.Item("GOODS_CRT_DATE").ToString()
                        insMatomeRows.Item("REMARK") = insRows.Item("REMARK").ToString()
                        insMatomeRows.Item("IRIME") = insRows.Item("IRIME").ToString()
                        insMatomeRows.Item("INKO_PLAN_DATE") = insRows.Item("INKO_PLAN_DATE").ToString()
                        'START YANAI 要望番号780
                        insMatomeRows.Item("INKA_DATE_KANRI_KB") = insRows.Item("INKA_DATE_KANRI_KB").ToString()
                        'END YANAI 要望番号780

                        'データセットに追加
                        ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).Rows.Add(insMatomeRows)

                        '取得した値から各個数・数量のまとめた値を求める
                        zaiMATOMEds = Me._H.SelectZaiDataMATOME(Me._frm, ds)
                        matomeMax = zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows.Count - 1
                        'START YANAI 要望番号1064 出荷引当時の不具合
                        'poraZaiNbMATOME = 0
                        'poraZaiQtMATOME = 0
                        'For j As Integer = 0 To matomeMax
                        '    poraZaiNbMATOME = poraZaiNbMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_NB").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_NB_HOZON").ToString)
                        '    poraZaiQtMATOME = poraZaiQtMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_QT").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_QT_HOZON").ToString)
                        'Next
                        'allocCanNbMATOME = poraZaiNbMATOME
                        'allocCanQtMATOME = poraZaiQtMATOME
                        allocCanNbMATOME = 0
                        allocCanQtMATOME = 0
                        For j As Integer = 0 To matomeMax
                            allocCanNbMATOME = allocCanNbMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_NB").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_NB_HOZON").ToString)
                            allocCanQtMATOME = allocCanQtMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_QT").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_QT_HOZON").ToString)
                        Next
                        'END YANAI 要望番号1064 出荷引当時の不具合

                        'START YANAI 要望番号780
                        ''求めたまとめの値から、この出荷データ内で既に引当ててる分を減算する
                        'If String.IsNullOrEmpty(insMatomeRows.Item("INKO_DATE").ToString) = False Then
                        '    zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                        '                                                                  "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                        '                                                                  "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                        '                                                                  "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                        '                                                                  "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                        '                                                                  "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                        '                                                                  "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                        '                                                                  "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                        '                                                                  "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                        '                                                                  "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                        '                                                                  "INKO_DATE = '", insMatomeRows.Item("INKO_DATE"), "' AND ", _
                        '                                                                  "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                        '                                                                  "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                        '                                                                  "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                        '                                                                  "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                        '                                                                  "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                        '                                                                  "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                        '                                                                  "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        'Else
                        '    zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                        '                                                                  "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                        '                                                                  "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                        '                                                                  "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                        '                                                                  "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                        '                                                                  "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                        '                                                                  "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                        '                                                                  "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                        '                                                                  "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                        '                                                                  "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                        '                                                                  "INKO_PLAN_DATE = '", insMatomeRows.Item("INKO_PLAN_DATE"), "' AND ", _
                        '                                                                  "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                        '                                                                  "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                        '                                                                  "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                        '                                                                  "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                        '                                                                  "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                        '                                                                  "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                        '                                                                  "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        'End If
                        '求めたまとめの値から、この出荷データ内で既に引当ててる分を減算する
                        'upd s.kobayashi NotesNo.1572 "01"-->"1"
                        If ("1").Equals(insMatomeRows.Item("INKA_DATE_KANRI_KB").ToString) = True Then
                            zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        ElseIf String.IsNullOrEmpty(insMatomeRows.Item("INKO_DATE").ToString) = False Then
                            zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "INKO_DATE = '", insMatomeRows.Item("INKO_DATE"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        Else
                            zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "INKO_PLAN_DATE = '", insMatomeRows.Item("INKO_PLAN_DATE"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        End If
                        'END YANAI 要望番号780

                        matomeMax = zaiRow.Length - 1
                        For j As Integer = 0 To matomeMax
                            allocCanNbMATOME = allocCanNbMATOME - Convert.ToDecimal(zaiRow(j).Item("ALCTD_NB").ToString) + Convert.ToDecimal(zaiRow(j).Item("ALCTD_NB_HOZON").ToString)
                            allocCanQtMATOME = allocCanQtMATOME - Convert.ToDecimal(zaiRow(j).Item("ALCTD_QT").ToString) + Convert.ToDecimal(zaiRow(j).Item("ALCTD_QT_HOZON").ToString)
                        Next
                        'END YANAI 要望番号853 まとめ処理対応

                        If ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(dRow.Item("PORA_ZAI_NB").ToString()) - 1)
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("PORA_ZAI_NB") = dRow.Item("PORA_ZAI_NB").ToString()
                        Else
                            insRows.Item("PORA_ZAI_NB") = dRow.Item("PORA_ZAI_NB").ToString()
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()) + Convert.ToDecimal(dRow.Item("ALCTD_NB_HOZON").ToString()))
                            insRows.Item("ALCTD_NB_GAMEN") = dRow.Item("HIKI_KOSU").ToString()
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            'START YANAI 要望番号903
                            'insRows.Item("ALCTD_NB") = "0"
                            insRows.Item("ALCTD_NB") = dRow.Item("ALCTD_NB_HOZON").ToString()
                            'END YANAI 要望番号903
                            insRows.Item("ALCTD_NB_GAMEN") = "0"
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) AndAlso _
                               dRow.Item("ALLOC_CAN_QT_HOZON").ToString() <> dRow.Item("HIKI_SURYO").ToString() Then
                            'START YANAI 要望番号903
                            'insRows.Item("ALCTD_NB") = "0"
                            insRows.Item("ALCTD_NB") = dRow.Item("ALCTD_NB_HOZON").ToString()
                            'END YANAI 要望番号903
                            insRows.Item("ALCTD_NB_GAMEN") = "0"
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) AndAlso _
                               dRow.Item("ALLOC_CAN_QT_HOZON").ToString() = dRow.Item("HIKI_SURYO").ToString() Then
                            'START YANAI 要望番号903
                            'insRows.Item("ALCTD_NB") = "1"
                            insRows.Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_NB_HOZON").ToString()) + 1)
                            'END YANAI 要望番号903
                            insRows.Item("ALCTD_NB_GAMEN") = "1"
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()) + Convert.ToDecimal(dRow.Item("ALCTD_NB_HOZON").ToString()))
                            insRows.Item("ALCTD_NB_GAMEN") = "0"
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            'START YANAI 要望番号853 まとめ処理対応
                            'insRows.Item("ALLOC_CAN_NB") = dRow.Item("ALLOC_CAN_NB")
                            'insRows.Item("ALLOC_CAN_NB_GAMEN") = dRow.Item("ALLOC_CAN_NB")
                            insRows.Item("ALLOC_CAN_NB") = dRow.Item("ALLOC_CAN_NB")
                            insRows.Item("ALLOC_CAN_NB_GAMEN") = allocCanNbMATOME - Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString())
                            'END YANAI 要望番号853 まとめ処理対応
                            'START YANAI 要望番号888
                            If taninusiFlg = True Then
                                insRows.Item("ALLOC_CAN_NB_GAMEN") = dRow.Item("ALLOC_CAN_NB")
                            End If
                            If Convert.ToDecimal(insRows.Item("ALLOC_CAN_NB_GAMEN")) < 0 Then
                                insRows.Item("ALLOC_CAN_NB_GAMEN") = "0"
                            End If
                            If Convert.ToDecimal(insRows.Item("ALLOC_CAN_NB")) < 0 Then
                                insRows.Item("ALLOC_CAN_NB") = "0"
                            End If
                            'END YANAI 要望番号888
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("ALLOC_CAN_NB") = Convert.ToDecimal(Convert.ToDecimal(dRow.Item("ALLOC_CAN_NB_HOZON")) - 1)
                            insRows.Item("ALLOC_CAN_NB_GAMEN") = dRow.Item("ALLOC_CAN_NB_HOZON")
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) AndAlso _
                               Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) <> Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()) Then
                            insRows.Item("ALLOC_CAN_NB") = dRow.Item("ALLOC_CAN_NB_HOZON").ToString
                            insRows.Item("ALLOC_CAN_NB_GAMEN") = dRow.Item("ALLOC_CAN_NB_HOZON").ToString
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                               Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) AndAlso _
                               Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) = Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()) Then
                            insRows.Item("ALLOC_CAN_NB") = "0"
                            insRows.Item("ALLOC_CAN_NB_GAMEN") = "0"
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("ALLOC_CAN_NB") = dRow.Item("ALLOC_CAN_NB")
                            insRows.Item("ALLOC_CAN_NB_GAMEN") = dRow.Item("ALLOC_CAN_NB")
                        End If

                        If ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) - Convert.ToDecimal(dRow.Item("IRIME").ToString()))
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("PORA_ZAI_QT") = dRow.Item("PORA_ZAI_QT").ToString()
                        Else
                            insRows.Item("PORA_ZAI_QT") = dRow.Item("PORA_ZAI_QT").ToString()
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()) + Convert.ToDecimal(dRow.Item("ALCTD_QT_HOZON").ToString()))
                            insRows.Item("ALCTD_QT_GAMEN") = dRow.Item("HIKI_SURYO").ToString()
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("ALCTD_QT") = dRow.Item("ALCTD_QT_HOZON").ToString()
                            insRows.Item("ALCTD_QT_GAMEN") = dRow.Item("ALCTD_QT_HOZON").ToString()
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_QT_HOZON").ToString()) + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            insRows.Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALCTD_QT_HOZON").ToString()) + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            'START YANAI 要望番号897
                            'insRows.Item("ALCTD_QT") = "0"
                            insRows.Item("ALCTD_QT") = dRow.Item("ALCTD_QT_HOZON").ToString()
                            'END YANAI 要望番号897
                            insRows.Item("ALCTD_QT_GAMEN") = dRow.Item("HIKI_SURYO").ToString()
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            'START YANAI 要望番号780
                            'If Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT").ToString) > 0 Then
                            If Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT").ToString) > 0 OrElse _
                                allocCanQtMATOME > 0 Then
                                'END YANAI 要望番号780
                                'START YANAI 要望番号853 まとめ処理対応
                                'insRows.Item("ALLOC_CAN_QT") = dRow.Item("ALLOC_CAN_QT")
                                'insRows.Item("ALLOC_CAN_QT_GAMEN") = dRow.Item("ALLOC_CAN_QT")
                                insRows.Item("ALLOC_CAN_QT") = dRow.Item("ALLOC_CAN_QT")
                                insRows.Item("ALLOC_CAN_QT_GAMEN") = allocCanQtMATOME - Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString())
                                'END YANAI 要望番号853 まとめ処理対応
                                'START YANAI 要望番号888
                                If taninusiFlg = True Then
                                    insRows.Item("ALLOC_CAN_QT_GAMEN") = dRow.Item("ALLOC_CAN_QT")
                                End If
                                'END YANAI 要望番号888
                            Else
                                insRows.Item("ALLOC_CAN_QT") = "0"
                                insRows.Item("ALLOC_CAN_QT_GAMEN") = "0"
                            End If
                            If Convert.ToDecimal(insRows.Item("ALLOC_CAN_QT_GAMEN")) < 0 Then
                                insRows.Item("ALLOC_CAN_QT_GAMEN") = "0"
                            End If
                            If Convert.ToDecimal(insRows.Item("ALLOC_CAN_QT")) < 0 Then
                                insRows.Item("ALLOC_CAN_QT") = "0"
                            End If
                            'END YANAI 要望番号888

                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) < Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON")) - Convert.ToDecimal(dRow.Item("IRIME")))
                            insRows.Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON")) - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            Convert.ToDecimal(dRow.Item("IRIME").ToString()) >= Convert.ToDecimal(dRow.Item("PORA_ZAI_QT").ToString()) Then
                            insRows.Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON")) - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            insRows.Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT_HOZON")) - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            insRows.Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            insRows.Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(dRow.Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                        End If

                        'データセットに追加
                        ds.Tables(LMC020C.TABLE_NM_ZAI).Rows.Add(insRows)

                    Else
                        '同じ在庫レコード番号のデータありの場合は、更新

                        'START YANAI 要望番号853 まとめ処理対応
                        'まとめ処理用
                        ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).Clear()
                        insMatomeRows = ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).NewRow

                        insMatomeRows.Item("NRS_BR_CD") = zaiRow(0).Item("NRS_BR_CD")
                        insMatomeRows.Item("TOU_NO") = zaiRow(0).Item("TOU_NO").ToString()
                        insMatomeRows.Item("SITU_NO") = zaiRow(0).Item("SITU_NO").ToString()
                        insMatomeRows.Item("ZONE_CD") = zaiRow(0).Item("ZONE_CD").ToString()
                        insMatomeRows.Item("LOCA") = zaiRow(0).Item("LOCA").ToString()
                        insMatomeRows.Item("LOT_NO") = zaiRow(0).Item("LOT_NO").ToString()
                        insMatomeRows.Item("GOODS_CD_NRS") = zaiRow(0).Item("GOODS_CD_NRS")
                        insMatomeRows.Item("ALLOC_PRIORITY") = zaiRow(0).Item("ALLOC_PRIORITY")
                        insMatomeRows.Item("RSV_NO") = zaiRow(0).Item("RSV_NO").ToString()
                        insMatomeRows.Item("SERIAL_NO") = zaiRow(0).Item("SERIAL_NO").ToString()
                        insMatomeRows.Item("GOODS_COND_KB_1") = zaiRow(0).Item("GOODS_COND_KB_1").ToString()
                        insMatomeRows.Item("GOODS_COND_KB_2") = zaiRow(0).Item("GOODS_COND_KB_2").ToString()
                        insMatomeRows.Item("OFB_KB") = zaiRow(0).Item("OFB_KB").ToString()
                        insMatomeRows.Item("SPD_KB") = zaiRow(0).Item("SPD_KB").ToString()
                        insMatomeRows.Item("REMARK_OUT") = zaiRow(0).Item("REMARK_OUT").ToString()
                        insMatomeRows.Item("INKO_DATE") = zaiRow(0).Item("INKO_DATE").ToString()
                        insMatomeRows.Item("LT_DATE") = zaiRow(0).Item("LT_DATE").ToString()
                        insMatomeRows.Item("GOODS_CRT_DATE") = zaiRow(0).Item("GOODS_CRT_DATE").ToString()
                        insMatomeRows.Item("REMARK") = zaiRow(0).Item("REMARK").ToString()
                        insMatomeRows.Item("IRIME") = zaiRow(0).Item("IRIME").ToString()
                        insMatomeRows.Item("INKO_PLAN_DATE") = zaiRow(0).Item("INKO_PLAN_DATE").ToString()
                        'START YANAI 要望番号780
                        insMatomeRows.Item("INKA_DATE_KANRI_KB") = zaiRow(0).Item("INKA_DATE_KANRI_KB").ToString()
                        'END YANAI 要望番号780

                        'データセットに追加
                        ds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_IN).Rows.Add(insMatomeRows)

                        '取得した値から各個数・数量のまとめた値を求める
                        zaiMATOMEds = Me._H.SelectZaiDataMATOME(Me._frm, ds)
                        matomeMax = zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows.Count - 1
                        'START YANAI 要望番号1064 出荷引当時の不具合
                        'poraZaiNbMATOME = 0
                        'poraZaiQtMATOME = 0
                        'For j As Integer = 0 To matomeMax
                        '    poraZaiNbMATOME = poraZaiNbMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_NB").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_NB_HOZON").ToString)
                        '    poraZaiQtMATOME = poraZaiQtMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_QT").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_QT_HOZON").ToString)
                        'Next
                        'allocCanNbMATOME = poraZaiNbMATOME
                        'allocCanQtMATOME = poraZaiQtMATOME
                        allocCanNbMATOME = 0
                        allocCanQtMATOME = 0
                        For j As Integer = 0 To matomeMax
                            allocCanNbMATOME = allocCanNbMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_NB").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_NB_HOZON").ToString)
                            allocCanQtMATOME = allocCanQtMATOME + Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("PORA_ZAI_QT").ToString) - Convert.ToDecimal(zaiMATOMEds.Tables(LMC020C.TABLE_NM_ZAI_MATOME_OUT).Rows(j).Item("ALCTD_QT_HOZON").ToString)
                        Next
                        'END YANAI 要望番号1064 出荷引当時の不具合

                        'START YANAI 要望番号780
                        ''求めたまとめの値から、この出荷データ内で既に引当ててる分を減算する
                        'If String.IsNullOrEmpty(insMatomeRows.Item("INKO_DATE").ToString) = False Then
                        '    zaiRow2 = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                        '                                                                  "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                        '                                                                  "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                        '                                                                  "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                        '                                                                  "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                        '                                                                  "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                        '                                                                  "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                        '                                                                  "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                        '                                                                  "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                        '                                                                  "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                        '                                                                  "INKO_DATE = '", insMatomeRows.Item("INKO_DATE"), "' AND ", _
                        '                                                                  "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                        '                                                                  "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                        '                                                                  "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                        '                                                                  "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                        '                                                                  "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                        '                                                                  "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                        '                                                                  "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        'Else
                        '    zaiRow2 = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                        '                                                                  "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                        '                                                                  "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                        '                                                                  "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                        '                                                                  "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                        '                                                                  "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                        '                                                                  "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                        '                                                                  "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                        '                                                                  "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                        '                                                                  "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                        '                                                                  "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                        '                                                                  "INKO_PLAN_DATE = '", insMatomeRows.Item("INKO_PLAN_DATE"), "' AND ", _
                        '                                                                  "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                        '                                                                  "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                        '                                                                  "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                        '                                                                  "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                        '                                                                  "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                        '                                                                  "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                        '                                                                  "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        'End If
                        '求めたまとめの値から、この出荷データ内で既に引当ててる分を減算する
                        'upd s.kobayashi NotesNo.1572 "01"-->"1"
                        If ("1").Equals(insMatomeRows.Item("INKA_DATE_KANRI_KB").ToString) = True Then
                            zaiRow2 = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))

                        ElseIf String.IsNullOrEmpty(insMatomeRows.Item("INKO_DATE").ToString) = False Then
                            zaiRow2 = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "INKO_DATE = '", insMatomeRows.Item("INKO_DATE"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        Else
                            zaiRow2 = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", insMatomeRows.Item("NRS_BR_CD"), "' AND ", _
                                                                                          "GOODS_CD_NRS = '", insMatomeRows.Item("GOODS_CD_NRS"), "' AND ", _
                                                                                          "LOT_NO = '", insMatomeRows.Item("LOT_NO"), "' AND ", _
                                                                                          "IRIME = '", insMatomeRows.Item("IRIME"), "' AND ", _
                                                                                          "TOU_NO = '", insMatomeRows.Item("TOU_NO"), "' AND ", _
                                                                                          "SITU_NO = '", insMatomeRows.Item("SITU_NO"), "' AND ", _
                                                                                          "ZONE_CD = '", insMatomeRows.Item("ZONE_CD"), "' AND ", _
                                                                                          "LOCA = '", insMatomeRows.Item("LOCA"), "' AND ", _
                                                                                          "GOODS_COND_KB_1 = '", insMatomeRows.Item("GOODS_COND_KB_1"), "' AND ", _
                                                                                          "GOODS_COND_KB_2 = '", insMatomeRows.Item("GOODS_COND_KB_2"), "' AND ", _
                                                                                          "REMARK_OUT = '", insMatomeRows.Item("REMARK_OUT"), "' AND ", _
                                                                                          "REMARK = '", insMatomeRows.Item("REMARK"), "' AND ", _
                                                                                          "INKO_PLAN_DATE = '", insMatomeRows.Item("INKO_PLAN_DATE"), "' AND ", _
                                                                                          "LT_DATE = '", insMatomeRows.Item("LT_DATE"), "' AND ", _
                                                                                          "OFB_KB = '", insMatomeRows.Item("OFB_KB"), "' AND ", _
                                                                                          "SPD_KB = '", insMatomeRows.Item("SPD_KB"), "' AND ", _
                                                                                          "RSV_NO = '", insMatomeRows.Item("RSV_NO"), "' AND ", _
                                                                                          "SERIAL_NO = '", insMatomeRows.Item("SERIAL_NO"), "' AND ", _
                                                                                          "GOODS_CRT_DATE = '", insMatomeRows.Item("GOODS_CRT_DATE"), "' AND ", _
                                                                                          "ALLOC_PRIORITY = '", insMatomeRows.Item("ALLOC_PRIORITY"), "'"))
                        End If
                        'END YANAI 要望番号780

                        matomeMax = zaiRow2.Length - 1
                        For j As Integer = 0 To matomeMax
                            'START YANAI 要望番号1064 出荷引当時の不具合
                            allocCanNbMATOME = allocCanNbMATOME - Convert.ToDecimal(zaiRow2(j).Item("ALCTD_NB").ToString) + Convert.ToDecimal(zaiRow2(j).Item("ALCTD_NB_HOZON").ToString)
                            allocCanQtMATOME = allocCanQtMATOME - Convert.ToDecimal(zaiRow2(j).Item("ALCTD_QT").ToString) + Convert.ToDecimal(zaiRow2(j).Item("ALCTD_QT_HOZON").ToString)
                            'allocCanNbMATOME = allocCanNbMATOME - Convert.ToDecimal(zaiRow2(j).Item("ALCTD_NB_GAMEN").ToString)
                            'allocCanQtMATOME = allocCanQtMATOME - Convert.ToDecimal(zaiRow2(j).Item("ALCTD_QT_GAMEN").ToString)
                            'END YANAI 要望番号1064 出荷引当時の不具合
                        Next
                        'END YANAI 要望番号853 まとめ処理対応

                        Dim alctdNb As Decimal = 0
                        Dim alctdNbG As Decimal = 0
                        Dim alctdCanNb As Decimal = 0
                        Dim alctdQt As Decimal = 0
                        Dim alctdQtG As Decimal = 0
                        Dim alctdCanQt As Decimal = 0
                        Dim irime As Decimal = 0
                        Dim kowakeIrime As Decimal = 0
                        Dim outSRow() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                                       , "OUTKA_NO_M = '", Me._frm.lblSyukkaMNo.TextValue, "' AND " _
                                       , "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "' AND " _
                                       , "SYS_DEL_FLG = '0'"))

                        If 0 < outSRow.Length = True Then
                            '変更前の該当データの引当個数・数量を取得
                            alctdNb = Convert.ToDecimal(outSRow(0).Item("ALCTD_NB"))
                            alctdNbG = Convert.ToDecimal(outSRow(0).Item("ALCTD_NB"))
                            alctdCanNb = Convert.ToDecimal(outSRow(0).Item("ALCTD_CAN_NB"))
                            alctdQt = Convert.ToDecimal(outSRow(0).Item("ALCTD_QT"))
                            alctdQtG = Convert.ToDecimal(outSRow(0).Item("ALCTD_QT"))
                            alctdCanQt = Convert.ToDecimal(outSRow(0).Item("ALCTD_CAN_QT"))
                            irime = Convert.ToDecimal(outSRow(0).Item("IRIME"))
                            If String.IsNullOrEmpty(outSRow(0).Item("KOWAKE_IRIME").ToString) = False Then
                                kowakeIrime = Convert.ToDecimal(outSRow(0).Item("KOWAKE_IRIME"))
                            End If
                        End If

                        If ("-1").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            If .optCnt.Checked = True Then
                                dRow.Item("ALCTD_KB") = "01"
                            ElseIf .optAmt.Checked = True Then
                                dRow.Item("ALCTD_KB") = "02"
                            ElseIf .optKowake.Checked = True Then
                                dRow.Item("ALCTD_KB") = "03"
                            ElseIf .optSample.Checked = True Then
                                dRow.Item("ALCTD_KB") = "04"
                            End If
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            '既存データ - 変更前のデータ(引当前に画面に表示している値)  + 引当した値
                            If alctdNb = 0 Then
                                zaiRow(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB_GAMEN")) - alctdNbG + Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()))
                                zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb + Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()))
                            Else
                                zaiRow(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB_GAMEN")) - alctdNbG + Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()))
                                zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb + Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()))
                            End If
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            (dRow.Item("HIKI_SURYO").ToString()).Equals(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) = True Then
                            zaiRow(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB_GAMEN")) - alctdNb + 1)
                            zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb + 1)
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB_GAMEN")) - alctdNbG)
                            'START YANAI 20120717 小分け在庫
                            'zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb + 1)
                            zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb)
                            'END YANAI 20120717 小分け在庫
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB_GAMEN")) - alctdNbG)
                            zaiRow(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_NB")) - alctdNb + Convert.ToDecimal(dRow.Item("HIKI_KOSU").ToString()))
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            '既存データ + 変更前のデータ(引当前に画面に表示している値)  - 引当した値
                            'START YANAI 要望番号853 まとめ処理対応
                            'START YANAI 要望番号1004
                            ''zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")) + alctdNbG - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            ''zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + alctdNb - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            'zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(allocCanNbMATOME + alctdNbG - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            'zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + alctdNb - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            ''END YANAI 要望番号853 まとめ処理対応
                            ''START YANAI 要望番号888
                            'If taninusiFlg = True Then
                            '    zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")) + alctdNbG - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            'End If
                            ''END YANAI 要望番号888
                            If taninusiFlg = True Then
                                zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")) + alctdNbG - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            Else
                                zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(allocCanNbMATOME + alctdNbG - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            End If
                            If Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")) < 0 Then
                                zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = "0"
                            End If
                            zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + alctdNb - Convert.ToDecimal(dRow.Item("HIKI_KOSU")))
                            If Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) < 0 Then
                                zaiRow(0).Item("ALLOC_CAN_NB") = "0"
                            End If
                            'END YANAI 要望番号1004

                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            (dRow.Item("HIKI_SURYO").ToString()).Equals(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) = True Then
                            zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = "0"
                            zaiRow(0).Item("ALLOC_CAN_NB") = "0"
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")
                            'START YANAI 20120717 小分け在庫
                            'zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + alctdNb - 1)
                            zaiRow(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_NB")) + alctdNb)
                            'END YANAI 20120717 小分け在庫
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALLOC_CAN_NB_GAMEN") = zaiRow(0).Item("ALLOC_CAN_NB_GAMEN")
                            zaiRow(0).Item("ALLOC_CAN_NB") = zaiRow(0).Item("ALLOC_CAN_NB")
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            '既存データ - 変更前のデータ(引当前に画面に表示している値)  + 引当した値
                            If alctdNb = 0 Then
                                zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - alctdQtG + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                                zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - alctdQt + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            Else
                                zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - alctdQtG + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                                zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - alctdQt + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            End If
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            (dRow.Item("HIKI_SURYO").ToString()).Equals(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) = True Then
                            zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - alctdQtG + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            'START YANAI 20120717 小分け在庫
                            'zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - alctdQt + Convert.ToDecimal(dRow.Item("IRIME").ToString()))
                            zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - alctdQt + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            'END YANAI 20120717 小分け在庫
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            If kowakeIrime = 0 Then
                                zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - alctdQtG + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            Else
                                zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - kowakeIrime + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            End If
                            'START YANAI 20120717 小分け在庫
                            'zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - irime + Convert.ToDecimal(dRow.Item("IRIME").ToString()))
                            zaiRow(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT")) - irime + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            'END YANAI 20120717 小分け在庫
                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALCTD_QT_GAMEN")) - alctdQtG + Convert.ToDecimal(dRow.Item("HIKI_SURYO").ToString()))
                            zaiRow(0).Item("ALCTD_QT") = zaiRow(0).Item("ALCTD_QT")
                        End If

                        If ("01").Equals(dRow.Item("ALCTD_KB").ToString) = True OrElse ("02").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            'START YANAI 要望番号853 まとめ処理対応
                            'zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            'zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + alctdQt - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            'START YANAI 要望番号1004
                            'zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(allocCanQtMATOME + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            'zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + alctdQt - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            ''END YANAI 要望番号853 まとめ処理対応
                            ' ''START YANAI 要望番号888
                            'If taninusiFlg = True Then
                            '    zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            'End If
                            ''END YANAI 要望番号888
                            If taninusiFlg = True Then
                                zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            Else
                                zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(allocCanQtMATOME + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            End If
                            zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + alctdQt - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            If Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) < 0 Then
                                zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = "0"
                            End If
                            If Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) < 0 Then
                                zaiRow(0).Item("ALLOC_CAN_QT") = "0"
                            End If
                            'END YANAI 要望番号1004
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True AndAlso _
                            (dRow.Item("HIKI_SURYO").ToString()).Equals(dRow.Item("ALLOC_CAN_QT_HOZON").ToString()) = True Then
                            zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = "0"
                            zaiRow(0).Item("ALLOC_CAN_QT") = "0"
                        ElseIf ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            If ("00").Equals(dRow.Item("SMPL_FLAG").ToString) = True Then
                                '出荷元が小分け以外の場合
                                If kowakeIrime = 0 Then
                                    zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                                Else
                                    zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + kowakeIrime - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                                End If
                                'START 2023/06/05 出荷（小）の行を削除した後、在庫引当を行うと ALLOC_CAN_QT が0となる現象の対応
                                'zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + irime - Convert.ToDecimal(zaiRow(0).Item("IRIME")))
                                If 0 < outSRow.Length Then
                                    zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + irime - Convert.ToDecimal(zaiRow(0).Item("IRIME")))
                                Else
                                    '出荷（小）の行を削除した場合
                                    zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + irime - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                                End If
                                'END 2023/06/05 出荷（小）の行を削除した後、在庫引当を行うと ALLOC_CAN_QT が0となる現象の対応
                            Else
                                '出荷元が小分けの場合
                                If kowakeIrime = 0 Then
                                    zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + alctdQtG - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                                Else
                                    zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")) + kowakeIrime - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                                End If
                                zaiRow(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiRow(0).Item("ALLOC_CAN_QT")) + irime - Convert.ToDecimal(dRow.Item("HIKI_SURYO")))
                            End If

                        ElseIf ("04").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("ALLOC_CAN_QT_GAMEN") = zaiRow(0).Item("ALLOC_CAN_QT_GAMEN")
                            zaiRow(0).Item("ALLOC_CAN_QT") = zaiRow(0).Item("ALLOC_CAN_QT")
                        End If

                        If ("03").Equals(dRow.Item("ALCTD_KB").ToString) = True Then
                            zaiRow(0).Item("SMPL_FLAG") = "01"
                        End If
                        zaiRow(0).Item("SMPL_FLAG_ZAI") = dRow.Item("SMPL_FLAG").ToString()

                        zaiRow(0).Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                        '小分け先在庫判断用
                        If ("03").Equals(dRow.Item("ALCTD_KB")) = True Then
                            ''現在小分けが選ばれていて、検索時は小分けではなかった場合
                            zaiRow(0).Item("ALCTD_KB_FLG") = "01"
                        End If

                        zaiRow(0).Item("INKA_DATE") = dRow.Item("INKA_DATE").ToString()
                        zaiRow(0).Item("IDO_DATE") = dRow.Item("IDO_DATE").ToString()
                        zaiRow(0).Item("HOKAN_STR_DATE") = dRow.Item("HOKAN_STR_DATE").ToString()

                        '該当の在庫レコードを同じ出荷小のデータの引当個数・数量を更新する
                        outSRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND " _
                               , "ZAI_REC_NO = '", dRow.Item("ZAI_REC_NO").ToString(), "' AND " _
                               , "UP_KBN = '0'"))
                        outSMax = outSRow.Length - 1
                        For j As Integer = 0 To outSMax
                            If alctdCanQt > Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_QT")) Then
                                '更新される引当可能数量と比較して、小さいデータは引当個数・数量分、可能個数・数量を加算する
                                outSRow(j).Item("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_NB")) + alctdNb)
                                outSRow(j).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_NB_GAMEN")) + alctdNb)
                                outSRow(j).Item("ALCTD_CAN_NB_MATOME") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_NB_MATOME")) + alctdNb)
                                outSRow(j).Item("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_QT")) + alctdQt)
                                outSRow(j).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_QT_GAMEN")) + alctdQt)
                                outSRow(j).Item("ALCTD_CAN_QT_MATOME") = Convert.ToString(Convert.ToDecimal(outSRow(j).Item("ALCTD_CAN_QT_MATOME")) + alctdQt)
                            End If
                        Next

                    End If

                Next

            End If

        End With

    End Sub

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
    ''' 引き当てた在庫を戻す
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function SetZaiRtn(ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As DataSet

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_S)
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

            outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                            "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))
            If ("01").Equals(outMDr(0).Item("ALCTD_KB")) = True OrElse _
                ("02").Equals(outMDr(0).Item("ALCTD_KB")) = True Then
                '出荷単位が個数・数量の場合のみ

                zaiDr = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                             "ZAI_REC_NO = '", dr.Item("ZAI_REC_NO"), "'"))
                If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
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
                ElseIf LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True Then
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


                outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))

                zaiDr(0).Item("UP_KBN") = "1"
            ElseIf ("03").Equals(outMDr(0).Item("ALCTD_KB")) = True Then
                '小分けの場合のみ
                zaiDr = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                             "ZAI_REC_NO = '", dr.Item("ZAI_REC_NO"), "'"))
                If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
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
                ElseIf LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True Then
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

                outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "OUTKA_NO_M = '", dr.Item("OUTKA_NO_M"), "'"))

                If ("99").Equals(zaiDr(0).Item("ALCTD_KB_FLG")) = False Then
                    zaiDr(0).Item("ALCTD_KB_FLG") = "01"
                End If

                zaiDr(0).Item("UP_KBN") = "1"

            End If
        Next

        Return ds

    End Function


#Region "'2014/01/22 輸出情報追加"

    ''' <summary>
    ''' 輸出情報の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetExportLSet(ByRef ds As DataSet) As Boolean

        Dim rtnResutl As Boolean = True

        With Me._frm

            'データセットをクリアする
            Dim insRows As DataRow = Nothing
            If ds.Tables(LMC020C.TABLE_NM_EXPORT_L) Is Nothing OrElse
               ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows.Count = 0 OrElse
               Me._frm.lblSituation.RecordStatus.Equals(RecordStatus.NEW_REC) = True OrElse
               Me._frm.lblSituation.RecordStatus.Equals(RecordStatus.COPY_REC) = True Then
                ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Clear()
                insRows = ds.Tables(LMC020C.TABLE_NM_EXPORT_L).NewRow
                insRows.Item("UP_KBN") = "0"
                insRows.Item("SYS_DEL_FLG") = "0" '新規
            Else
                insRows = ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows(0)
                insRows.Item("UP_KBN") = "1"
                insRows.Item("SYS_DEL_FLG") = "0" '更新
            End If

            insRows.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            insRows.Item("OUTKA_NO_L") = .lblSyukkaLNo.TextValue
            insRows.Item("SHIP_NM") = .txtShipNm.TextValue
            insRows.Item("DESTINATION") = .txtDestination.TextValue
            insRows.Item("BOOKING_NO") = .txtBookingNo.TextValue
            insRows.Item("VOYAGE_NO") = .txtVoyageNo.TextValue
            insRows.Item("SHIPPER_CD") = .txtShipperCd.TextValue
            insRows("SHIPPER_NM") = Me.GetDestNm(.cmbEigyosyo.SelectedValue.ToString(), .txtCust_Cd_L.TextValue, .txtShipperCd.TextValue)
            insRows.Item("CONT_LOADING_DATE") = .imdContLoadingDate.TextValue
            insRows.Item("STORAGE_TEST_DATE") = .imdStorageTestDate.TextValue
            insRows.Item("STORAGE_TEST_TIME") = .txtStorageTestTime.TextValue
            insRows.Item("DEPARTURE_DATE") = .imdDepartureDate.TextValue
            insRows.Item("CONTAINER_NO") = .txtContainerNo.TextValue
            insRows.Item("CONTAINER_NM") = .txtContainerNm.TextValue
            insRows.Item("CONTAINER_SIZE") = .cmbContainerSize.SelectedValue

            If ds.Tables(LMC020C.TABLE_NM_EXPORT_L) Is Nothing OrElse
               ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows.Count = 0 Then
                'データセットに追加
                ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows.Add(insRows)
            End If

        End With

        Return rtnResutl

    End Function

#End Region

#Region "輸出情報 DataTable 初期化"

    ''' <summary>
    ''' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
    ''' SetExportLSet() にて UP_KBN = "0"(新規) として設定後にエラーチェック等による中断があった場合(☆)の処理
    ''' ☆の状態からのエラー解消による再実行で再度 SetExportLSet() を実行の際、 UP_KBN = "1"(更新) となってしまい
    ''' LMC020DAC#ComExportLData で更新処理と判定され、対象データなしによる排他エラーが発生する事象の対応策
    ''' </summary>
    ''' <param name="ds"></param>
    Friend Sub ClearExportLSet(ByRef ds As DataSet)

        If Not (ds.Tables(LMC020C.TABLE_NM_EXPORT_L) Is Nothing) AndAlso
            ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows.Count() > 0 AndAlso
            ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Rows(0).Item("UP_KBN").ToString() = "0" Then
            ds.Tables(LMC020C.TABLE_NM_EXPORT_L).Clear()
        End If

    End Sub

#End Region ' "輸出情報 DataTable 初期化"

    ''' <summary>
    ''' 出荷(中)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelOutMDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow() = Nothing

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    dr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "OUTKA_NO_M = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                    If 0 < dr.Length Then

                        If ("0").Equals(dr(0).Item("UP_KBN")) = True Then
                            '新規追加の値の場合は、データセットから削除
                            ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Remove(dr(0))
                        Else
                            '更新の値の場合は、システムフラグを"1"に変更
                            dr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                        End If

                    End If

                End If

            Next

        End With

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(HED)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelMarkHedDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow() = Nothing

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    dr = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "OUTKA_NO_M = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                    If 0 < dr.Length Then

                        If ("0").Equals(dr(0).Item("UP_KBN")) = True Then
                            '新規追加の値の場合は、データセットから削除
                            ds.Tables(LMC020C.TABLE_NM_MARK_HED).Rows.Remove(dr(0))
                        Else
                            '更新の値の場合は、システムフラグを"1"に変更
                            dr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                        End If

                    End If

                End If

            Next

        End With

    End Sub

    ''' <summary>
    ''' マーク(DTL)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelMarkDtlDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow() = Nothing

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    dr = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                "OUTKA_NO_M = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                    If 0 < dr.Length Then

                        For j As Integer = 0 To dr.Length - 1

                            If ("0").Equals(dr(j).Item("UP_KBN")) = True Then
                                '新規追加の値の場合は、データセットから削除
                                ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Rows.Remove(dr(j))
                            Else
                                '更新の値の場合は、システムフラグを"1"に変更
                                dr(j).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                            End If

                        Next

                    End If

                End If

            Next

        End With

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加END

    ''' <summary>
    ''' 作業(中)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelSagyoMDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim gMax As Integer = 0

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                 "INOUTKA_NO_LM = '", Me._frm.lblSyukkaLNo.TextValue, _
                                                                                                 _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                    gMax = dr.Length - 1

                    For J As Integer = 0 To gMax

                        If ("0").Equals(dr(J).Item("UP_KBN")) = True Then
                            '新規追加の値の場合は、データセットから削除
                            ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Remove(dr(J))
                        Else
                            '更新の値の場合は、システムフラグを"1"に変更
                            dr(J).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                            dr(J).Item("UP_KBN") = "2"
                        End If

                    Next

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 出荷(小)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelOutSDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim gMax As Integer = 0
            Dim dr As DataRow() = Nothing
            'Dim outSDr As DataRow() = Nothing
            Dim zaiDr As DataRow() = Nothing
            Dim drOutM As DataRow() = Nothing
            Dim drOutS As DataRow() = Nothing
            Dim OutSmax As Integer = 0
            'START YANAI 要望番号809
            Dim drOutS2 As DataRow() = Nothing
            Dim OutSmax2 As Integer = 0
            'END YANAI 要望番号809

            If ("sprSyukkaM").Equals(spr.Name) = True Then
                For i As Integer = 0 To max
                    If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                        drOutM = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                 "OUTKA_NO_M = '", _LMCconG.GetCellValue(_frm.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))

                        dr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                 (String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(_frm.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "' AND ", _
                                                "SYS_DEL_FLG = '0'"))

                        gMax = dr.Length - 1

                        If -1 < gMax Then
                            For J As Integer = 0 To gMax
                                '在庫データの引当可能個数・数量に出荷(小)が引当てていた数を戻す。
                                zaiDr = ds.Tables(LMC020C.TABLE_NM_ZAI).Select _
                                         (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "'"))
                                drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                         (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "'"))
                                OutSmax = drOutS.Length - 1
                                'START YANAI 要望番号809
                                drOutS2 = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                         (String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(_frm.sprSyukkaM.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "' AND ", _
                                                        "ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "' AND SYS_DEL_FLG = '0'"))
                                OutSmax2 = drOutS2.Length - 1
                                'END YANAI 要望番号809

                                If ("0").Equals(dr(J).Item("UP_KBN")) = True Then
                                    If 1 = drOutS.Length Then
                                        '削除行以外に同じ在庫レコードの出荷(小)が無い場合は、在庫レコードをデータセットから削除
                                        ds.Tables(LMC020C.TABLE_NM_ZAI).Rows.Remove(zaiDr(0))
                                        'START YANAI 20110906 サンプル対応
                                        'Else
                                        'START YANAI 要望番号809
                                        'ElseIf ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'START YANAI 要望番号1064 出荷引当時の不具合
                                        'ElseIf ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False And 0 < OutSmax2 Then
                                    ElseIf ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False And 0 <= OutSmax2 Then
                                        'END YANAI 要望番号1064 出荷引当時の不具合
                                        '出荷(小)を削除した後、出荷(中)を削除した時、2回目の在庫戻しを行わないようにOutSmax2件数にて判定()
                                        'END YANAI 要望番号809
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応
                                        '在庫データの引当可能個数・数量に出荷(小)が引当てていた数を戻す。
                                        '引当中個数・数量は減算、引当可能個数・数量は加算
                                        'START YANAI 要望番号809
                                        'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                        'END YANAI 要望番号809
                                        If ("03").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                            '小分け以外の場合
                                            'START YANAI 要望番号809
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            'END YANAI 要望番号809
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT_GAMEN")))
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                        Else
                                            'START YANAI 要望番号809
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - 1)
                                            'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                            'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            ''END YANAI 要望番号809
                                            ''START YANAI 要望番号780
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - 1)
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            ''END YANAI 要望番号780
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - 1)
                                            zaiDr(0).Item("ALCTD_NB") = "0"
                                            'END YANAI 20120717 小分け在庫
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString) = True Then
                                                zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                                zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - 1)
                                            End If
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(J).Item("IRIME")))
                                            'zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(J).Item("IRIME")))
                                            'zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 要望番号780
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(J).Item("IRIME")))
                                            zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            End If
                                            'END YANAI 要望番号780
                                            'START YANAI 要望番号809
                                            'zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            'END YANAI 要望番号809
                                        End If
                                    End If

                                    'START YANAI 20110906 サンプル対応
                                    If ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応

                                        '新規で追加している出荷小のみ値を戻す
                                        drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                                          (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "' AND ", _
                                                                         "UP_KBN = '0'"))
                                        OutSmax = drOutS.Length - 1
                                        For k As Integer = 0 To OutSmax
                                            If Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) < Convert.ToDecimal(dr(J).Item("ALCTD_CAN_QT")) Then
                                                If drOutS(k).Item("ALCTD_CAN_NB").ToString().Equals(drOutS(k).Item("ALCTD_CAN_NB_MATOME").ToString()) = True Then
                                                    drOutS(k).Item("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                                    drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                                    'START YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_NB_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                                    'END YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                                    drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                                    'START YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_QT_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                                    'END YANAI 要望番号459
                                                Else
                                                    drOutS(k).Item("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_MATOME")))
                                                    drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_MATOME")))
                                                    'START YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_NB_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_MATOME")))
                                                    'END YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT_MATOME")))
                                                    drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT_MATOME")))
                                                    'START YANAI 要望番号459
                                                    drOutS(k).Item("ALCTD_CAN_QT_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_MATOME")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT_MATOME")))
                                                    'END YANAI 要望番号459

                                                End If

                                            End If
                                        Next

                                        'START YANAI 20110906 サンプル対応
                                    End If
                                    'END YANAI 20110906 サンプル対応

                                    '新規追加の値の場合は、データセットから削除
                                    ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Remove(dr(J))
                                Else

                                    'START YANAI 20110906 サンプル対応
                                    If ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応

                                        '在庫データの引当可能個数・数量に出荷(小)が引当てていた数を戻す。
                                        '引当中個数・数量は減算、引当可能個数・数量は加算
                                        'START YANAI 要望番号571
                                        'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        ''zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        ''zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'If ("03").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        '    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                        '    'zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                        '    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                        '    'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                        'Else
                                        '    zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("IRIME")))
                                        '    'zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(0).Item("IRIME")))
                                        '    zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("IRIME")))
                                        '    'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("IRIME")))
                                        '    zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                        'End If
                                        'START YANAI 要望番号809
                                        'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                        'END YANAI 要望番号809
                                        If ("03").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                            'START YANAI 要望番号809
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            'END YANAI 要望番号809
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT_GAMEN")))
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                        Else
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                            'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            ''END YANAI 要望番号809
                                            ''START YANAI 要望番号780
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            ''END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                            If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString) = True Then
                                                zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                                zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB_GAMEN")))
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                            End If
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_QT") = zaiDr(0).Item("ALCTD_QT").ToString
                                            'zaiDr(0).Item("ALCTD_QT_GAMEN") = zaiDr(0).Item("ALCTD_QT_GAMEN").ToString
                                            'zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            End If
                                            'END YANAI 要望番号780
                                            'START YANAI 要望番号809
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + 1)
                                            'zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            If ("0").Equals(zaiDr(0).Item("PORA_ZAI_NB").ToString) = True Then
                                                zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + 1)
                                                zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'END YANAI 要望番号809

                                            'START YANAI 20110913 小分け対応
                                            'zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            If 1 = drOutS.Length Then
                                                '削除行以外に同じ在庫レコードの出荷(小)が無い場合は、在庫レコードのシステムフラグをオン
                                                zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            End If
                                            'END YANAI 20110913 小分け対応
                                        End If
                                        'END YANAI 要望番号571

                                        ''新規で追加している出荷小のみ値を戻す
                                        'drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                        '                  (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "' AND ", _
                                        '                                 "UP_KBN = '0'"))
                                        'OutSmax = drOutS.Length - 1
                                        'For k As Integer = 0 To OutSmax
                                        '    If Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) < Convert.ToDecimal(dr(J).Item("ALCTD_CAN_QT")) Then
                                        '        drOutS(k).Item("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        '        drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                        '        drOutS(k).Item("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                        '        drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                        '    End If
                                        'Next

                                        'START YANAI 20110906 サンプル対応
                                    End If
                                    'END YANAI 20110906 サンプル対応

                                    '更新の値の場合は、システムフラグを"1"に変更
                                    dr(J).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                End If
                            Next
                        End If

                    End If
                Next

            ElseIf ("sprDtl").Equals(spr.Name) = True Then
                drOutM = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                 "OUTKA_NO_M = '", Me._frm.lblSyukkaMNo.TextValue, "'"))
                For i As Integer = 0 To max
                    If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.DEF.ColNo))) = True Then
                        dr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                 (String.Concat("OUTKA_NO_M = '", Me._frm.lblSyukkaMNo.TextValue, "' AND ", _
                                                "OUTKA_NO_S = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo)).ToString, "' AND ", _
                                                "SYS_DEL_FLG = '0'"))
                        gMax = dr.Length - 1

                        If -1 < gMax Then
                            For J As Integer = 0 To gMax
                                zaiDr = ds.Tables(LMC020C.TABLE_NM_ZAI).Select _
                                         (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "'"))
                                drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                         (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "'"))
                                OutSmax = drOutS.Length - 1

                                If ("0").Equals(dr(J).Item("UP_KBN")) = True Then
                                    If 1 = drOutS.Length Then
                                        '削除行以外に同じ在庫レコードの出荷(小)が無い場合は、在庫レコードをデータセットから削除
                                        ds.Tables(LMC020C.TABLE_NM_ZAI).Rows.Remove(zaiDr(0))
                                        'START YANAI 20110906 サンプル対応
                                        'Else
                                    ElseIf ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応
                                        '在庫データの引当可能個数・数量に出荷(小)が引当てていた数を戻す。
                                        '引当中個数・数量は減算、引当可能個数・数量は加算
                                        'START YANAI 要望番号809
                                        'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'END YANAI 要望番号809
                                        If ("03").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                            'START YANAI 要望番号809
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            'END YANAI 要望番号809
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                        Else
                                            'START YANAI 20120717 小分け在庫
                                            ''START YANAI 要望番号809
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - 1)
                                            'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                            'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            ''END YANAI 要望番号809
                                            ''START YANAI 要望番号780
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            ''END YANAI 要望番号780
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - 1)
                                            zaiDr(0).Item("ALCTD_NB") = "0"
                                            'END YANAI 20120717 小分け在庫
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString) = True Then
                                                zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                                zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            End If
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("IRIME")))
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")))
                                            'zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")))
                                            'zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("IRIME")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            End If
                                            'END YANAI 要望番号780
                                            'START YANAI 要望番号809
                                            'zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            'END YANAI 要望番号809
                                        End If

                                    End If

                                    'START YANAI 20110906 サンプル対応
                                    If ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応

                                        '新規で追加している出荷小のみ値を戻す
                                        drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                                          (String.Concat("ZAI_REC_NO = '", dr(J).Item("ZAI_REC_NO"), "' AND ", _
                                                                         "UP_KBN = '0'"))
                                        OutSmax = drOutS.Length - 1
                                        For k As Integer = 0 To OutSmax
                                            If Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) < Convert.ToDecimal(dr(0).Item("ALCTD_CAN_QT")) Then
                                                drOutS(k).Item("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                                drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                                'START YANAI 要望番号459
                                                drOutS(k).Item("ALCTD_CAN_NB_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_MATOME")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                                'END YANAI 要望番号459
                                                drOutS(k).Item("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                                drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                                'START YANAI 要望番号459
                                                drOutS(k).Item("ALCTD_CAN_QT_MATOME") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_MATOME")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                                'END YANAI 要望番号459
                                            End If
                                        Next


                                        'START YANAI 20110906 サンプル対応
                                    End If
                                    'END YANAI 20110906 サンプル対応

                                    '新規追加の値の場合は、データセットから削除
                                    ds.Tables(LMC020C.TABLE_NM_OUT_S).Rows.Remove(dr(J))
                                Else

                                    'START YANAI 20110906 サンプル対応
                                    If ("04").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                        'サンプル以外の場合
                                        'END YANAI 20110906 サンプル対応

                                        '在庫データの引当可能個数・数量に出荷(小)が引当てていた数を戻す。
                                        '引当中個数・数量は減算、引当可能個数・数量は加算
                                        'START YANAI 要望番号809
                                        'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                        'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                        'END YANAI 要望番号809
                                        If ("03").Equals(drOutM(0).Item("ALCTD_KB").ToString) = False Then
                                            'START YANAI 要望番号809
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            'END YANAI 要望番号809
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT_GAMEN")))
                                            'START YANAI 要望番号780
                                            'START YANAI 要望番号1064 出荷引当時の不具合
                                            'zaiDr(0).Item("ALCTD_QT_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            'End If
                                            'END YANAI 要望番号1064 出荷引当時の不具合
                                            'END YANAI 要望番号780
                                        Else
                                            'START YANAI 20120717 小分け在庫
                                            ''START YANAI 要望番号809
                                            'zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            'zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            'zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                            'zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            ''END YANAI 要望番号809
                                            ''START YANAI 要望番号780
                                            'zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            'If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                            '    zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            'End If
                                            ''END YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            zaiDr(0).Item("ALCTD_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                            If ("0").Equals(zaiDr(0).Item("ALLOC_CAN_NB").ToString) = True Then
                                                zaiDr(0).Item("ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB")) + 1)
                                                zaiDr(0).Item("ALLOC_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_NB_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_NB_GAMEN")))
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) - Convert.ToDecimal(dr(0).Item("ALCTD_NB")))
                                            End If
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_NB_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_NB_HOZON") = "0"
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("ALCTD_QT") = zaiDr(0).Item("ALCTD_QT").ToString
                                            'zaiDr(0).Item("ALCTD_QT_GAMEN") = zaiDr(0).Item("ALCTD_QT_GAMEN").ToString
                                            'zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("IRIME")))
                                            'zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("IRIME")))
                                            zaiDr(0).Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALCTD_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_GAMEN")) - Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            zaiDr(0).Item("ALLOC_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("ALLOC_CAN_QT_GAMEN")) + Convert.ToDecimal(dr(0).Item("ALCTD_QT")))
                                            'END YANAI 20120717 小分け在庫
                                            'START YANAI 要望番号780
                                            zaiDr(0).Item("ALCTD_QT_HOZON") = zaiDr(0).Item("ALCTD_QT_HOZON").ToString
                                            If Convert.ToDecimal(zaiDr(0).Item("ALCTD_QT_HOZON")) < 0 Then
                                                zaiDr(0).Item("ALCTD_QT_HOZON") = "0"
                                            End If
                                            'END YANAI 要望番号780
                                            'START YANAI 要望番号809
                                            'START YANAI 20120717 小分け在庫
                                            'zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + 1)
                                            'zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr(J).Item("IRIME")))
                                            If ("0").Equals(zaiDr(0).Item("PORA_ZAI_NB").ToString) = True Then
                                                zaiDr(0).Item("PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_NB")) + 1)
                                                zaiDr(0).Item("PORA_ZAI_QT") = Convert.ToString(Convert.ToDecimal(zaiDr(0).Item("PORA_ZAI_QT")) + Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                            End If
                                            'END YANAI 20120717 小分け在庫
                                            'END YANAI 要望番号809
                                            'START YANAI 20110913 小分け対応
                                            'zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            If 1 = drOutS.Length Then
                                                '削除行以外に同じ在庫レコードの出荷(小)が無い場合は、在庫レコードのシステムフラグをオン
                                                zaiDr(0).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                            End If
                                            'END YANAI 20110913 小分け対応

                                        End If

                                        'START YANAI 20110906 サンプル対応
                                    End If
                                    'END YANAI 20110906 サンプル対応

                                    'START YANAI 要望番号500
                                    'まとめ処理されているレコードの場合、画面上の引当可能個数・数量を削除したレコード分だけ減算する
                                    For m As Integer = 0 To max
                                        If i <> m AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.LOT_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.LOT_NO.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.IRIME.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.IRIME.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.TOU_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.TOU_NO.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.SHITSU_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.SHITSU_NO.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.ZONE_CD.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.ZONE_CD.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.LOCA.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.LOCA.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.NAKAMI.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.NAKAMI.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.GAIKAN.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.GAIKAN.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.JOTAI_NM.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.JOTAI_NM.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.REMARK.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.REMARK.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.INKO_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.INKO_DATE.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.LT_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.LT_DATE.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.HORYUHIN.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.HORYUHIN.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.BOGAIHIN.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.BOGAIHIN.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.RSV_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.RSV_NO.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.SERIAL_NO.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.SERIAL_NO.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.GOODS_CRT_DATE.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.GOODS_CRT_DATE.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.WARIATE_NM.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.WARIATE_NM.ColNo))) = True AndAlso _
                                            Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.REMARK_OUT.ColNo)).Equals(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.REMARK_OUT.ColNo))) = True Then

                                            drOutS = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                                              (String.Concat("OUTKA_NO_M = '", dr(J).Item("OUTKA_NO_M").ToString, "' AND ", _
                                                                             "OUTKA_NO_S = '", Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(m, sprDtl.SHO_NO.ColNo)), "' AND ", _
                                                                             "SYS_DEL_FLG = '0'"))
                                            OutSmax = drOutS.Length - 1
                                            For k As Integer = 0 To OutSmax
                                                If m < i Then
                                                    drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_GAMEN")) - Convert.ToDecimal(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_NB.ColNo))) - Convert.ToDecimal(dr(J).Item("ALCTD_NB")))
                                                    drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_GAMEN")) - Convert.ToDecimal(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT.ColNo))) - Convert.ToDecimal(dr(J).Item("ALCTD_QT")))
                                                Else
                                                    drOutS(k).Item("ALCTD_CAN_NB_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_NB_GAMEN")) - Convert.ToDecimal(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_NB.ColNo))))
                                                    drOutS(k).Item("ALCTD_CAN_QT_GAMEN") = Convert.ToString(Convert.ToDecimal(drOutS(k).Item("ALCTD_CAN_QT_GAMEN")) - Convert.ToDecimal(Me._LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.ALLOC_CAN_QT.ColNo))))
                                                End If
                                            Next

                                        End If
                                    Next
                                    'END YANAI 要望番号500

                                    '更新の値の場合は、システムフラグを"1"に変更
                                    dr(J).Item("SYS_DEL_FLG") = LMConst.FLG.ON
                                End If
                            Next
                        End If

                    End If
                Next

            End If

        End With

    End Sub

    ''' <summary>
    ''' ピッキングWK(中レベル相当)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelPickWkMDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow = Nothing

            Dim onDelFlg As String = String.Empty

            '荷主マスタより運賃請求先マスタコードを取得
            Dim custDDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                       "NRS_BR_CD = '", _frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                     , "CUST_CD = '", _frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                                     , "SUB_KB = '69'"))

            If custDDr.Length > 0 Then
                onDelFlg = custDDr(0).Item("SET_NAIYO").ToString()
            End If


            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    dr = ds.Tables("LMC020_OUTKA_PICK_WK").NewRow

                    '削除該当フラグ
                    dr.Item("ON_DELETE_FLG") = onDelFlg

                    '営業所コード
                    dr.Item("NRS_BR_CD") = _frm.cmbEigyosyo.SelectedValue.ToString()
                    '出荷管理番号(大)
                    dr.Item("OUTKA_NO_L") = _frm.lblSyukkaLNo.TextValue
                    '出荷管理番号(中)
                    dr.Item("OUTKA_NO_M") = _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo))
                    '削除フラグ
                    dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON

                    'Add処理
                    ds.Tables("LMC020_OUTKA_PICK_WK").Rows.Add(dr)

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' ピッキングWK(小レベル相当)の値を削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub DelPickWkSDataSet(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow = Nothing
            Dim sDr() As DataRow = Nothing
            Dim sMax As Integer = 0

            Dim onDelFlg As String = String.Empty

            '荷主マスタより運賃請求先マスタコードを取得
            Dim custDDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                                       "NRS_BR_CD = '", _frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                     , "CUST_CD = '", _frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                                     , "SUB_KB = '69'"))

            If custDDr.Length > 0 Then
                onDelFlg = custDDr(0).Item("SET_NAIYO").ToString()
            End If

            For i As Integer = 0 To max
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.DEF.ColNo))) = True Then

                    sDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select _
                                   (String.Concat("OUTKA_NO_M = '", Me._frm.lblSyukkaMNo.TextValue, "' AND ", _
                                                  "OUTKA_NO_S = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprDtl.SHO_NO.ColNo)).ToString, "' AND ", _
                                                  "SYS_DEL_FLG = '0'"))
                    sMax = sDr.Length - 1

                    For j As Integer = 0 To sMax

                        dr = ds.Tables("LMC020_OUTKA_PICK_WK").NewRow

                        '削除該当フラグ
                        dr.Item("ON_DELETE_FLG") = onDelFlg

                        '営業所コード
                        dr.Item("NRS_BR_CD") = Me._frm.cmbEigyosyo.SelectedValue.ToString()
                        '出荷管理番号(大)
                        dr.Item("OUTKA_NO_L") = Me._frm.lblSyukkaLNo.TextValue
                        '出荷管理番号(中)
                        dr.Item("OUTKA_NO_M") = Me._frm.lblSyukkaMNo.TextValue
                        'シリアル番号
                        dr.Item("SERIAL_NO") = sDr(j).Item("SERIAL_NO")
                        '削除フラグ
                        dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON

                        'Add処理
                        ds.Tables("LMC020_OUTKA_PICK_WK").Rows.Add(dr)

                    Next

                End If
            Next

        End With

    End Sub

    'START YANAI 要望番号780
    ''' <summary>
    ''' 出荷(小)のチェックボックスをオンにする
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCheckBox(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            If ("sprDtl").Equals(spr.Name) = True Then
                For i As Integer = 0 To max
                    .SetCellValue(i, sprDtl.DEF.ColNo, LMC020C.FLG_TRUE)
                Next
            End If

        End With

    End Sub
    'END YANAI 要望番号780

    ''' <summary>
    ''' 出荷(大)の完了取消設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetTK_OUT_L(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_L)
        Dim dr As DataRow = Nothing

        dr = dt.Rows(0)
        dr.Item("OUTKA_STATE_KB") = "50"
        'START YANAI 要望番号342
        dr.Item("END_DATE") = String.Empty
        'END YANAI 要望番号342
        'START YANAI 20110913 小分け対応
        dr.Item("TORIKESHI_FLG") = "01"
        'END YANAI 20110913 小分け対応
        'START SHINODA 要望管理2165
        If String.IsNullOrEmpty(_frm.imdHokanEndDate.TextValue) = True Then
            dr.Item("END_DATE2") = ""
        Else
            dr.Item("END_DATE2") = _frm.imdHokanEndDate.TextValue
        End If
        'END SHINODA 要望管理2165

    End Sub

    ''' <summary>
    ''' 作業の完了取消設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetTK_SAGYO(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_SAGYO)
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

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_UNSO_L)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            'START YANAI 要望番号635
            'dr.Item("OUTKA_PLAN_DATE") = String.Empty
            'END YANAI 要望番号635
            dr.Item("TORIKESI_FLG") = LMConst.FLG.ON
        Next

    End Sub

    ''' <summary>
    ''' 出荷大のデータセットクリア設定（複写時）
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub ClearOutL(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_L)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            dr.Item("OUTKA_NO_L") = String.Empty
            dr.Item("FURI_NO") = String.Empty
            dr.Item("OUTKA_STATE_KB") = String.Empty
            dr.Item("CUST_ORD_NO") = String.Empty
            dr.Item("BUYER_ORD_NO") = String.Empty
        Next

    End Sub

    '要望番号:997 terakawa 2012.10.22 Start
    ''' <summary>
    ''' EDI更新テーブル用の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetEdiUpdTblDataSet(ByRef ds As DataSet) As Boolean

        With Me._frm

            'DataSet初期化
            ds.Tables(LMC020C.TABLE_NM_EDI_UPD_TBL).Clear()

            '荷主明細マスタ取得
            Dim custDetailDr() As DataRow = Nothing
            custDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                               "SUB_KB = '48' AND ", _
                                                                                               "CUST_CD = '", String.Concat(.txtCust_Cd_L.TextValue, .txtCust_Cd_M.TextValue), "'"))

            If custDetailDr.Count > 0 AndAlso _
               custDetailDr(0).Item("SET_NAIYO").ToString = LMConst.FLG.ON Then
                '削除された出荷(中)が存在した場合、確認用ワーニングを出力
                If Me._V.IsOutkaMDeleteChk(ds) = False Then
                    Return False
                End If

                Dim row As DataRow = ds.Tables(LMC020C.TABLE_NM_EDI_UPD_TBL).NewRow
                row("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                row("EDI_UPD_FLG") = custDetailDr(0).Item("SET_NAIYO")
                row("EDI_UPD_RCV_TBL") = custDetailDr(0).Item("SET_NAIYO_2")

                ds.Tables(LMC020C.TABLE_NM_EDI_UPD_TBL).Rows.Add(row)
            End If

            Return True
        End With
    End Function
    '要望番号:997 terakawa 2012.10.22 End

    'ADD 2017/08/29 トールのとき、送状先頭５桁を取得
    ''' <summary>
    ''' トールの送状番号先頭の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetTollOkurijyoTblDataSet(ByRef ds As DataSet) As Boolean

        With Me._frm

            'DataSet初期化
            ds.Tables(LMC020C.TABLE_NM_OKURIJYO_WK).Clear()

            '区分マスタより取得
            Dim kbnDetailDr() As DataRow = Nothing
            kbnDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T029' AND ", _
                                                                                               "KBN_NM1 = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                               "KBN_NM2 = '", .txtUnsoCompanyCd.TextValue, "' AND ", _
                                                                                               "KBN_NM3 = '", .txtUnsoSitenCd.TextValue, "'"))

            If kbnDetailDr.Count > 0 Then
                Dim row As DataRow = ds.Tables(LMC020C.TABLE_NM_OKURIJYO_WK).NewRow
                Row("OKURIJYO_HEAD") = kbnDetailDr(0).Item("KBN_NM4")

                ds.Tables(LMC020C.TABLE_NM_OKURIJYO_WK).Rows.Add(row)
            End If

            Return True
        End With
    End Function

    ''' <summary>
    ''' 請求鑑データチェック用の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetKagamiDataSet(ByRef ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As Boolean

        With Me._frm

            'DataSet初期化
            ds.Tables(LMC020C.TABLE_NM_KAGAMI_IN).Clear()

            '荷主マスタ取得
            Dim dr As DataRow = ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).NewRow()
            dr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue                '営業所コード
            dr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue                   '荷主コード（大）
            dr.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue                '荷主コード（中）
            dr.Item("CUST_CD_S") = "00"
            dr.Item("CUST_CD_SS") = "00"
            ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows.Add(dr)
            Dim custDs As DataSet = Me._H.SelectCustData(Me._frm, ds, eventShubetsu)
            Dim custRow As DataRow = custDs.Tables(LMC020C.TABLE_NM_CUST).Rows(0)

            '運賃計算締め基準の値によって、チェック対象の日付を変更
            Dim checkDate As String = String.Empty
            If ("01").Equals(custRow.Item("UNTIN_CALCULATION_KB")) = True Then
                checkDate = .imdSyukkaYoteiDate.TextValue
            Else
                checkDate = .imdNounyuYoteiDate.TextValue
            End If
            'チェック対象の日付が未入力の場合は、処理を抜ける
            If String.IsNullOrEmpty(checkDate) = True Then
                Return True
            End If

            Dim row As DataRow = ds.Tables(LMC020C.TABLE_NM_KAGAMI_IN).NewRow
            row("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            row("STORAGE_SEIQTO_CD") = String.Empty
            row("HANDLING_SEIQTO_CD") = String.Empty
            row("UNCHIN_SEIQTO_CD") = String.Empty
            row("SAGYO_SEIQTO_CD") = String.Empty
            row("YOKOMOCHI_SEIQTO_CD") = String.Empty

            row("STORAGE_SEIQTO_CD") = custRow.Item("HOKAN_SEIQTO_CD")
            row("HANDLING_SEIQTO_CD") = custRow.Item("NIYAKU_SEIQTO_CD")
            If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = False Then
                '完了取消はチェック対象外
                row("UNCHIN_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
            End If
            row("SAGYO_SEIQTO_CD") = custRow.Item("SAGYO_SEIQTO_CD")
            If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = False Then
                '完了取消はチェック対象外
                row("YOKOMOCHI_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
            End If
            row("CHECK_DATE") = checkDate

            ds.Tables(LMC020C.TABLE_NM_KAGAMI_IN).Rows.Add(row)

            Return True

        End With

    End Function

    ''' <summary>
    ''' 出荷中の梱包個数の設定処理コントロール
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function OutMkonpoCtl(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._frm

            Dim value As Decimal = 0

            '出荷中の梱包個数
            value = Me.OutMkonpo(ds)

            '範囲チェック（NUMERIC 10桁）
            If Me._V.IsHaniCheck(value, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示

                '2017/09/25 修正 李↓
                Me._LMCconV.SetErrMessage("E014", New String() {lgm.Selector({"出荷(中)の梱包個数", "Packing the number of shipments (M)", "출하(中)의 포장개수", "中国語"}), LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                '2017/09/25 修正 李↑

                Return False
            End If
            .numPkgCnt.Value = value

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷中の梱包個数の設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function OutMkonpo(ByVal ds As DataSet) As Decimal

        Dim konpoMatome As Decimal = 0

        With Me._frm

            Dim irisu As Decimal = Convert.ToDecimal(.numIrisu.Value)
            Dim hasu As Decimal = 0
            Dim amari As Decimal = 0

            Dim max As Integer = .sprDtl.ActiveSheet.Rows.Count - 1

            '梱数を設定
            konpoMatome = Convert.ToDecimal(.numKonsu.Value)

            If 0 < Convert.ToDecimal(.numHasu.Value) Then
                '割った時の整数部分を求める
                hasu = Convert.ToDecimal(.numHasu.Value)
                konpoMatome = konpoMatome + Convert.ToInt32(Math.Floor(CalcData(hasu, irisu)))

                '割った時の余り部分を求める
                amari = CalcDataMod(hasu, irisu)
                If 0 < amari Then
                    konpoMatome = konpoMatome + 1
                End If

            End If

        End With

        Return konpoMatome

    End Function

    ''' <summary>
    ''' 出荷大の梱包個数の設定処理コントロール
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function OutLkonpoCtl(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._frm

            Dim value As Decimal = 0

            '出荷大の出荷梱包個数
            value = Me.OutLkonpo(ds)
            '範囲チェック（NUMERIC 10桁）
            If Me._V.IsHaniCheck(value, LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                'メッセージの表示

                '2017/09/25 修正 李↓
                Me._LMCconV.SetErrMessage("E014", New String() {lgm.Selector({"出荷(大)の出荷梱包個数", "Shipping packing number of shipment (L)", "출하(大)의 출하포장개수", "中国語"}), LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                '2017/09/25 修正 李↑

                Return False
            End If

            .numKonpoKosu.Value = value

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷大の梱包個数の設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function OutLkonpo(ByVal ds As DataSet) As Decimal

        Dim konpoMatome As Decimal = 0

        With Me._frm

            Dim outMdr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                             "SYS_DEL_FLG = '0'"))
            Dim max As Integer = outMdr.Length - 1

            If ("0").Equals(.numKonpoKosu.TextValue) = True Then

                '値が0の時のみ自動計算
                For i As Integer = 0 To max
                    '出荷小の引当個数をすべて加算
                    If (.lblSyukkaMNo.TextValue).Equals(outMdr(i).Item("OUTKA_NO_M").ToString()) = False Then
                        konpoMatome = konpoMatome + Convert.ToDecimal(outMdr(i).Item("OUTKA_M_PKG_NB").ToString())
                    End If
                Next
                '出荷管理番号中が空の場合は、データセットに含まれていないということなので、現在表示中のデータも設定
                konpoMatome = konpoMatome + Convert.ToDecimal(.numPkgCnt.Value)

            Else
                konpoMatome = Convert.ToDecimal(.numKonpoKosu.Value)

                '2018/12/07 ADD START 要望管理002171
                If String.IsNullOrEmpty(.lblSyukkaLNo.TextValue) = True Then
                    '新規で値が0でない⇒ユーザが入力⇒画面の値を使用
                    ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Rows(0).Item("USE_GAMEN_VALUE_FLG") = LMC020C.USE_GAMEN_VALUE_TRUE
                End If
                '2018/12/07 ADD END   要望管理002171
            End If

        End With

        Return konpoMatome

    End Function

    '要望番号:1731 terakawa 2013.01.15 Start
    ''' <summary>
    ''' 出荷大の梱包個数の設定処理コントロール(サンプル・小分けの場合は1を設定）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetOutLKonpo() As Boolean

        With Me._frm

            '出荷梱包個数が0の場合は出荷梱包個数に'1'を設定
            If ("0").Equals(.numKonpoKosu.TextValue) = True Then
                .numKonpoKosu.Value = 1
            End If

        End With

        Return True

    End Function
    '要望番号:1731 terakawa 2013.01.15 End

    'START YANAI 要望番号394
    'START YANAI 要望番号565
    '''' <summary>
    '''' 進捗区分設定
    '''' </summary>
    '''' <param name="oldStateKb">変更前の進捗区分</param>
    '''' <returns>新しい進捗区分を返却</returns>
    '''' <remarks></remarks>
    'Private Function SetStateKb(Optional ByVal oldStateKb As String = "00") As String
    ''' <summary>
    ''' 進捗区分設定
    ''' </summary>
    ''' <param name="oldStateKb">変更前の進捗区分</param>
    ''' <returns>新しい進捗区分を返却</returns>
    ''' <remarks></remarks>
    Private Function SetStateKb(Optional ByVal oldStateKb As String = "00", _
                                Optional ByVal zanFlg As Boolean = False) As String
        'END YANAI 要望番号565

        Dim sokodr() As DataRow = Nothing
        Dim newStateKb As String = String.Empty

        sokodr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                         "WH_CD = '", Me._frm.cmbSoko.SelectedValue, "'"))

        'START YANAI 要望番号565
        If zanFlg = False AndAlso _
            String.IsNullOrEmpty(newStateKb) = True AndAlso _
            Convert.ToDecimal(oldStateKb) <= Convert.ToDecimal(LMC020C.SINTYOKU10) Then
            newStateKb = LMC020C.SINTYOKU10
        End If
        'END YANAI 要望番号565

        '出荷予定制御有無
        If ("01").Equals(sokodr(0).Item("OUTKA_YOTEI_YN").ToString) = True AndAlso _
             String.IsNullOrEmpty(newStateKb) = True Then
            If Convert.ToDecimal(oldStateKb) < Convert.ToDecimal(LMC020C.SINTYOKU10) Then
                newStateKb = LMC020C.SINTYOKU10
            Else
                newStateKb = oldStateKb
            End If
        End If

        '出荷指図書印刷制御有無
        If ("01").Equals(sokodr(0).Item("OUTKA_SASHIZU_PRT_YN").ToString) = True AndAlso _
             String.IsNullOrEmpty(newStateKb) = True Then
            If Convert.ToDecimal(oldStateKb) < Convert.ToDecimal(LMC020C.SINTYOKU30) Then
                newStateKb = LMC020C.SINTYOKU30
            Else
                newStateKb = oldStateKb
            End If
        End If

        '出庫完了制御有無
        If ("01").Equals(sokodr(0).Item("OUTOKA_KANRYO_YN").ToString) = True AndAlso _
             String.IsNullOrEmpty(newStateKb) = True Then
            If Convert.ToDecimal(oldStateKb) < Convert.ToDecimal(LMC020C.SINTYOKU40) Then
                newStateKb = LMC020C.SINTYOKU40
            Else
                newStateKb = oldStateKb
            End If
        End If

        '出荷検品制御有無
        If ("01").Equals(sokodr(0).Item("OUTKA_KENPIN_YN").ToString) = True AndAlso _
             String.IsNullOrEmpty(newStateKb) = True Then
            If Convert.ToDecimal(oldStateKb) < Convert.ToDecimal(LMC020C.SINTYOKU50) Then
                newStateKb = LMC020C.SINTYOKU50
            Else
                newStateKb = oldStateKb
            End If
        End If

        '上記以外の場合
        If String.IsNullOrEmpty(newStateKb) = True Then
            newStateKb = LMC020C.SINTYOKU10
        End If

        Return newStateKb

    End Function
    'END YANAI 要望番号394

    ''' <summary>
    ''' ゼロ割回避処理
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcData(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 / value2

    End Function

    ''' <summary>
    ''' ゼロ剰余回避処理
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(あまり値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcDataMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function

    ''' <summary>
    ''' 前ゼロ設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="keta">前につけるゼロ</param>
    ''' <returns>設定値</returns>
    ''' <remarks></remarks>
    Friend Function SetZeroData(ByVal value As String, ByVal keta As String) As String

        SetZeroData = String.Concat(keta, value)

        Dim ketasu As Integer = keta.Length

        Return SetZeroData.Substring(SetZeroData.Length - ketasu, ketasu)

    End Function
    '2012.11.22 nakamura 要望番号612:在庫振替削除 Start
    ''' <summary>
    ''' 在庫振替削除の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetFuriDelDataSet(ByRef ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As DataSet

        With Me._frm

            'DataSet初期化
            ds.Tables(LMC020C.TABLE_NM_FURIDEL).Clear()

            Dim row As DataRow = ds.Tables(LMC020C.TABLE_NM_FURIDEL).NewRow
            row("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            row("FURI_NO") = .lblFurikaeNo.TextValue

            row("INKA_NO_L") = String.Empty
            row("INKA_NO_M") = String.Empty
            row("INKA_NO_S") = String.Empty
            row("ZAI_REC_NO") = String.Empty
            row("SYS_UPD_DATE_ZAI") = String.Empty
            row("SYS_UPD_TIME_ZAI") = String.Empty
            row("HIKIATE") = String.Empty
            row("ZAI_REC_CNT") = String.Empty
            ds.Tables(LMC020C.TABLE_NM_FURIDEL).Rows.Add(row)

            Dim InkaDs As DataSet = Me._H.SelectInkaData(Me._frm, ds, eventShubetsu)

            Return InkaDs

        End With

    End Function
    '2012.11.22 nakamura 要望番号612:在庫振替削除 End

    ''' <summary>
    ''' 出荷(中)の印刷対象を更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetOutMDataSetPrint(ByVal spr As LMSpread, ByVal ds As DataSet)

        With spr
            Dim max As Integer = .ActiveSheet.Rows.Count - 1
            Dim dr As DataRow() = Nothing

            For i As Integer = 0 To max
                dr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                            "OUTKA_NO_M = '", _LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.KANRI_NO.ColNo)), "'"))
                If (LMConst.FLG.ON).Equals(_LMCconG.GetCellValue(spr.ActiveSheet.Cells(i, sprSyukkaM.DEFM.ColNo))) = True Then
                    If 0 < dr.Length Then
                        dr(0).Item("PRINT_FLG") = LMConst.FLG.ON
                    End If
                Else
                    If 0 < dr.Length Then
                        dr(0).Item("PRINT_FLG") = LMConst.FLG.OFF
                    End If
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' タブレット項目の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetTabletItemData(ByRef ds As DataSet) As Boolean

        With Me._frm

            Dim olTbl As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_L)
            Dim osTbl As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_S)

            'タブレット使用営業所の場合は現場作業あり
            Dim nrsBrCd As String = olTbl.Rows(0).Item("NRS_BR_CD").ToString
            Dim whCd As String = olTbl.Rows(0).Item("WH_CD").ToString
            Dim sokoDr() As DataRow = Nothing
            sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                             "WH_CD = '", whCd, "' "))
            'ロケ管理対象外の倉庫の場合は現場作業なし
            If sokoDr.Length > 0 Then
                If "00".Equals(sokoDr(0).Item("LOC_MANAGER_YN").ToString) Then
                    olTbl.Rows(0).Item("WH_TAB_YN") = "00"
                End If
            End If

            '出荷Sすべてが他社倉庫保管の商品の場合は現場作業なし
            Dim jisya As Boolean = False
            Dim tasya As Boolean = False
            For Each osRow As DataRow In osTbl.Rows
                If LMConst.FLG.OFF.Equals(osRow.Item("SYS_DEL_FLG").ToString) Then
                    Dim tsRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU).Select( _
                                                 String.Concat("NRS_BR_CD = '", osRow.Item("NRS_BR_CD").ToString, "'", _
                                                          " AND TOU_NO    = '", osRow.Item("TOU_NO").ToString, "'", _
                                                          " AND SITU_NO   = '", osRow.Item("SITU_NO").ToString, "'", _
                                                          " AND WH_CD     = '", olTbl.Rows(0).Item("WH_CD").ToString, "'"))
                    If tsRow.Length = 0 Then
                        Continue For
                    End If

                    If "02".Equals(tsRow(0).Item("JISYATASYA_KB").ToString) Then
                        tasya = True
                    Else
                        jisya = True
                    End If
                End If
            Next
            If tasya = True AndAlso jisya = False Then
                olTbl.Rows(0).Item("WH_TAB_YN") = "00"
            End If

            If LMC020C.WH_TAB_YN_NO.Equals(olTbl.Rows(0).Item("WH_TAB_YN")) Then
                olTbl.Rows(0).Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_99
            End If

        End With

        Return True

    End Function

#End Region 'DataSet

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#Region "2014/01/31 輸出情報追加関連"

    ''' <summary>
    ''' 届先マスタから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetDestNm(ByVal nrsBrCd As String, _
                                  ByVal custCdL As String, _
                                  ByVal destCd As String) As String

        If String.IsNullOrEmpty(destCd) = True Then
            Return String.Empty
        End If

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("NRS_BR_CD") = nrsBrCd
        destMstDr.Item("CUST_CD_L") = custCdL
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select

        If 0 < destDr.Length Then
            Return destDr(0).Item("DEST_NM").ToString
        End If

        Return String.Empty

    End Function

#End Region

#Region "営業日取得"
    '要望番号2690 前営業日・翌営業日対応
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

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

                    '該当する日付が休日マスタに存在するか？
                    Dim sBussinessDate As String = Format(dBussinessDate, "yyyyMMdd")
                    Dim holDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
                    If holDr.Count = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function
#End Region

#Region "荷主固有の処理"

    ''' <summary>
    ''' FFEM原料プラント間転送か否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Friend Function IsFFEM_MaterialPlantTransfer(ByVal ds As DataSet) As Boolean

        If (Not (ds Is Nothing)) AndAlso
            ds.Tables.Contains(LMC020C.TABLE_INOUTKAEDI_HED_FJF) AndAlso ds.Tables(LMC020C.TABLE_INOUTKAEDI_HED_FJF).Rows.Count() > 0 AndAlso
            ds.Tables.Contains(LMC020C.TABLE_NM_OUT_M) AndAlso ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count() > 0 AndAlso
            ds.Tables(LMC020C.TABLE_INOUTKAEDI_HED_FJF).Rows(0).Item("ZFVYHKKBN").ToString() = "2" AndAlso
            ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(0).Item("GOODS_CD_CUST").ToString().StartsWith("243") AndAlso
            ds.Tables(LMC020C.TABLE_INOUTKAEDI_HED_FJF).Rows(0).Item("ZFVYDENTYP").ToString() = "ZUB1" Then
            ' 引当計上予実区分(ZFVYHKKBN) = '2'(出荷予定) かつ
            ' 品目コード(GOODS_CD_CUST[設定元元:MATNR]) の左 3桁が '243'(原料) かつ
            ' 伝票タイプ区分(ZFVYDENTYP) = 'ZUB1'(在庫転送オーダー) の場合
            ' FFEM原料プラント間転送である
            Return True
        End If

        Return False

    End Function

#End Region

#End Region

End Class