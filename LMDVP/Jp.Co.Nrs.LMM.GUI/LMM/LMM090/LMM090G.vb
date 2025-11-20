'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM090G : 荷主マスタメンテナンス
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
''' LMM090Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM090G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM090F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM090F, ByVal g As LMMControlG)

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
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F4ButtonEnabled = view
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

            .sprCust.TabIndex = LMM090C.CtlTabIndex.SPR_CUST
            .cmbBr.TabIndex = LMM090C.CtlTabIndex.CMB_BR

            '荷主(大)タブ
            .tabCust.TabIndex = LMM090C.CtlTabIndex.TAB_CUST
            .tpgCustL.TabIndex = LMM090C.CtlTabIndex.TPG_CUST_L
            .txtCustCdL.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_CD_L
            .txtCustNmL.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_NM_L
            .txtMainCustCd.TabIndex = LMM090C.CtlTabIndex.TXT_MAIN_CUST
            .lblMainCustNm.TabIndex = LMM090C.CtlTabIndex.LBL_MAIN_CUST_NM
            .cmbUnsoTehaiKbn.TabIndex = LMM090C.CtlTabIndex.CMB_UNSO_TEHAI
            .txtSampleSagyoCd.TabIndex = LMM090C.CtlTabIndex.TXT_SAMPLE_KBN
            .lblSampleSagyoNm.TabIndex = LMM090C.CtlTabIndex.LBL_SAMPLE_NM
            .cmbProductSegCd.TabIndex = LMM090C.CtlTabIndex.CMB_PRODUCT_SEG_CD
            '要望番号:349 yamanaka 2012.07.24 Start
            .btnDetailRowAddL.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_ADD_L
            .btnDetailRowDelL.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_DEL_L
            .sprCustDetailL.TabIndex = LMM090C.CtlTabIndex.SPR_CUST_DETAIL_L
            '要望番号:349 yamanaka 2012.07.24 End
            .btnEditL.TabIndex = LMM090C.CtlTabIndex.BTN_EDIT_L
            'ADD START 2018/11/14 要望番号001939
            .optCoaInkaDateN.TabIndex = LMM090C.CtlTabIndex.OPT_COA_INKA_DATE_N
            .optCoaInkaDateY.TabIndex = LMM090C.CtlTabIndex.OPT_COA_INKA_DATE_Y
            'ADD END   2018/11/14 要望番号001939

            '荷主(中)タブ
            .tpgCustM.TabIndex = LMM090C.CtlTabIndex.TPG_CUST_M
            .lblCustCdM.TabIndex = LMM090C.CtlTabIndex.LBL_CUST_CD_M
            .txtCustNmM.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_NM_M
            'START YANAI 要望番号824
            .txtTantoCd.TabIndex = LMM090C.CtlTabIndex.TXT_TANTO_CD
            .lblTantoNm.TabIndex = LMM090C.CtlTabIndex.TXT_TANTO_NM
            'END YANAI 要望番号824
            .txtZipNo.TabIndex = LMM090C.CtlTabIndex.TXT_ZIP
            .txtAdd1.TabIndex = LMM090C.CtlTabIndex.TXT_ADD1
            .txtAdd2.TabIndex = LMM090C.CtlTabIndex.TXT_ADD2
            .txtAdd3.TabIndex = LMM090C.CtlTabIndex.TXT_ADD3
            .cmbItemCurrCd.TabIndex = LMM090C.CtlTabIndex.CMB_ITEM_CURR_CD
            .cmbHoshoMinKbn.TabIndex = LMM090C.CtlTabIndex.CMB_HOSHO_MIN_KBN
            .cmbKazeiKbn.TabIndex = LMM090C.CtlTabIndex.CMB_KAZEI
            .txtCustKyoriCd.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_KYORI_CD
            .lblCustKyoriNm.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_KYORI_NM
            .txtTekiyo.TabIndex = LMM090C.CtlTabIndex.REMARK
            .cmbSoko.TabIndex = LMM090C.CtlTabIndex.CMB_SOKO
            .cmbUnsoHokenAutoKbn.TabIndex = LMM090C.CtlTabIndex.UNSO_HOKEN_AUTO_KBN      'ADD 2018/10/10 002400
            .pnlNyuka.TabIndex = LMM090C.CtlTabIndex.PNL_NYUKA
            .cmbTarifBunruiNyuka.TabIndex = LMM090C.CtlTabIndex.CMB_TARIF_KBN_NYUKA
            .txtUnchinTarifTonNyuka.TabIndex = LMM090C.CtlTabIndex.TXT_UNCHIN_TARIF_TON_NYUKA
            .lblUnchinTarifTonNyuka.TabIndex = LMM090C.CtlTabIndex.LBL_UNCHIN_TARIF_TON_NYUKA
            .txtUnchinTarifShadateNyuka.TabIndex = LMM090C.CtlTabIndex.TXT_UNCHIN_TARIF_SHADATE_NYUKA
            .lblUnchinTarifShadateNyuka.TabIndex = LMM090C.CtlTabIndex.LBL_UNCHIN_TARIF_SHADATE_NYUKA
            .txtWarimashiTarifNyuka.TabIndex = LMM090C.CtlTabIndex.TXT_WARIMASHI_TARIF_NYUKA
            .lblWarimashiTarifNyuka.TabIndex = LMM090C.CtlTabIndex.LBL_WARIMASHI_TARIF_NYUKA
            .txtYokomochiTarifNyuka.TabIndex = LMM090C.CtlTabIndex.TXT_YOKOMOCHI_TARIF_NYUKA
            .lblYokomochiTarifNyuka.TabIndex = LMM090C.CtlTabIndex.LBL_YOKOMOCHI_TARIF_NYUKA
            .cmbInitDateNyuka.TabIndex = LMM090C.CtlTabIndex.INIT_INKA_PLAN_DATE_KB      'ADD 2018/10/30 002192
            .pnlShukka.TabIndex = LMM090C.CtlTabIndex.PNL_SHUKKA
            .cmbTarifBunruiShukka.TabIndex = LMM090C.CtlTabIndex.CMB_TARIF_KBN_SHUKKA
            .txtUnchinTarifTonShukka.TabIndex = LMM090C.CtlTabIndex.TXT_UNCHIN_TARIF_TON_SHUKKA
            .lblUnchinTarifTonShukka.TabIndex = LMM090C.CtlTabIndex.LBL_UNCHIN_TARIF_TON_SHUKKA
            .txtUnchinTarifShadateShukka.TabIndex = LMM090C.CtlTabIndex.TXT_UNCHIN_TARIF_SHADATE_SHUKKA
            .lblUnchinTarifShadateShukka.TabIndex = LMM090C.CtlTabIndex.LBL_UNCHIN_TARIF_SHADATE_SHUKKA
            .txtWarimashiTarifShukka.TabIndex = LMM090C.CtlTabIndex.TXT_WARIMASHI_TARIF_SHUKKA
            .lblWarimashiTarifShukka.TabIndex = LMM090C.CtlTabIndex.LBL_WARIMASHI_TARIF_SHUKKA
            .txtYokomochiTarifShukka.TabIndex = LMM090C.CtlTabIndex.TXT_YOKOMOCHI_TARIF_SHUKKA
            .lblYokomochiTarifShukka.TabIndex = LMM090C.CtlTabIndex.LBL_YOKOMOCHI_TARIF_SHUKKA
            .cmbInitDateShukka.TabIndex = LMM090C.CtlTabIndex.INIT_OUTKA_PLAN_DATE_KB        'ADD 2018/10/30 002192
            .txtShiteiUnsoCompCd.TabIndex = LMM090C.CtlTabIndex.TXT_SHITEI_UNSO_COMP
            .txtShiteiUnsoShitenCd.TabIndex = LMM090C.CtlTabIndex.TXT_SHITEI_UNSO_SHISHA
            .lblShiteiUnsoCompNm.TabIndex = LMM090C.CtlTabIndex.LBL_SHITEI_UNSO
            .cmbInitDateNyuka.TabIndex = LMM090C.CtlTabIndex.INKA_ORIG_CD      'ADD 2018/10/25 要望番号001820
            .numHokanFree.TabIndex = LMM090C.CtlTabIndex.NUM_HOKAN_FREE
            .cmbNyukaHokoku.TabIndex = LMM090C.CtlTabIndex.CMB_NYUKA_HOKOKU
            .cmbZaikoHokoku.TabIndex = LMM090C.CtlTabIndex.CMB_ZAIKO_HOKOKU
            .cmbShukkaHokoku.TabIndex = LMM090C.CtlTabIndex.CMB_SHUKKA_HOKOKU
            '要望番号:349 yamanaka 2012.07.24 Start
            .btnDetailRowAddM.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_ADD_M
            .btnDetailRowDelM.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_DEL_M
            .sprCustDetailM.TabIndex = LMM090C.CtlTabIndex.SPR_CUST_DETAIL_M
            '要望番号:349 yamanaka 2012.07.24 End
            .btnFukushaM.TabIndex = LMM090C.CtlTabIndex.BTN_FUKUSHA_M
            .btnEditM.TabIndex = LMM090C.CtlTabIndex.BTN_EDIT_M

            '荷主(小)タブ
            .tpgCustS.TabIndex = LMM090C.CtlTabIndex.TPG_CUST_S
            .lblCustCdS.TabIndex = LMM090C.CtlTabIndex.LBL_CUST_CD_S
            .txtCustNmS.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_NM_S
            .txtCustBetuNm.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_BETU_NM
            .cmbShimeKbn.TabIndex = LMM090C.CtlTabIndex.CMB_SHIME_KBN
            .txtTcustBpCd.TabIndex = LMM090C.CtlTabIndex.TXT_TCUST_BPCD
            .lblTcustBpNm.TabIndex = LMM090C.CtlTabIndex.LBL_TCUST_BPNM
            .txtSeiqCd.TabIndex = LMM090C.CtlTabIndex.TXT_SEIQ_CD
            .lblSeiqNm.TabIndex = LMM090C.CtlTabIndex.LBL_SEIQ_NM
            .txtHokanSeiqCd.TabIndex = LMM090C.CtlTabIndex.TXT_HOKAN_SEIQ_CD
            .lblHokanSeiqNm.TabIndex = LMM090C.CtlTabIndex.LBL_HOKAN_SEIQ_NM
            .txtNiyakuSeiqCd.TabIndex = LMM090C.CtlTabIndex.TXT_NIYAKU_SEIQ_CD
            .lblNiyakuSeiqNm.TabIndex = LMM090C.CtlTabIndex.LBL_NIYAKU_SEIQ_NM
            .txtUnchinSeiqCd.TabIndex = LMM090C.CtlTabIndex.TXT_UNCHIN_SEIQ_CD
            .lblUnchinSeiqNm.TabIndex = LMM090C.CtlTabIndex.LBL_UNCHIN_SEIQ_NM
            .txtSagyoSeiqCd.TabIndex = LMM090C.CtlTabIndex.TXT_SAGYO_SEIQ_CD
            .lblSagyoSeiqNm.TabIndex = LMM090C.CtlTabIndex.LBL_SAGYO_SEIQ_NM
            .cmbPikkingKbn.TabIndex = LMM090C.CtlTabIndex.CMB_PIKKING
            .cmbUnchinCalc.TabIndex = LMM090C.CtlTabIndex.CMB_UNCHIN_CALC
            .btnRowAdd.TabIndex = LMM090C.CtlTabIndex.BTN_ADD_ROW
            .btnRowDel.TabIndex = LMM090C.CtlTabIndex.BTN_DEL_ROW
            .sprCustPrt.TabIndex = LMM090C.CtlTabIndex.SPR_CUST_PRT
            '要望番号:349 yamanaka 2012.07.26 Start
            .btnDetailRowAddS.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_ADD_S
            .btnDetailRowDelS.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_DEL_S
            .sprCustDetailS.TabIndex = LMM090C.CtlTabIndex.SPR_CUST_DETAIL_S
            '要望番号:349 yamanaka 2012.07.26 End
            .btnFukushaS.TabIndex = LMM090C.CtlTabIndex.BTN_FUKUSHA_S
            .btnEditS.TabIndex = LMM090C.CtlTabIndex.BTN_EDIT_S

            '荷主(極小)タブ
            .tpgCustSS.TabIndex = LMM090C.CtlTabIndex.TPG_CUST_SS
            .lblCustCdSS.TabIndex = LMM090C.CtlTabIndex.LBL_CUST_CD_SS
            .txtCustNmSS.TabIndex = LMM090C.CtlTabIndex.TXT_CUST_NM_SS
            .txtMainTantoNm.TabIndex = LMM090C.CtlTabIndex.TXT_MAIN_TANTO
            .txtSubTantoNm.TabIndex = LMM090C.CtlTabIndex.TXT_SUB_TANTO
            .txtTel.TabIndex = LMM090C.CtlTabIndex.TXT_TEL
            .txtFax.TabIndex = LMM090C.CtlTabIndex.TXT_FAX
            .txtEmail.TabIndex = LMM090C.CtlTabIndex.TXT_EMAIL
            .cmbSouKaeSyori.TabIndex = LMM090C.CtlTabIndex.CMB_SOKAE_SHORI
            .imdLastCalc.TabIndex = LMM090C.CtlTabIndex.IMD_LAST_CALK
            .imdBeforeCalc.TabIndex = LMM090C.CtlTabIndex.IMD_BEFORE_CALK
            .lblLastJob.TabIndex = LMM090C.CtlTabIndex.LBL_LAST_JOB
            .lblBeforeJob.TabIndex = LMM090C.CtlTabIndex.LBL_BEFORE_JOB
            .cmbCalc.TabIndex = LMM090C.CtlTabIndex.CMB_CALK
            .cmbSeiqHakugaiHinKbn.TabIndex = LMM090C.CtlTabIndex.CMB_SEIQ_HAKUGAIHIN_KBN
            '要望番号:349 yamanaka 2012.07.26 Start
            .btnDetailRowAddSS.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_ADD_SS
            .btnDetailRowDelSS.TabIndex = LMM090C.CtlTabIndex.BTN_DETAIL_DEL_SS
            .sprCustDetailSS.TabIndex = LMM090C.CtlTabIndex.SPR_CUST_DETAIL_SS
            '要望番号:349 yamanaka 2012.07.26 End
            .btnFukushaSS.TabIndex = LMM090C.CtlTabIndex.BTN_FUKUSHA_SS
            .btnEditSS.TabIndex = LMM090C.CtlTabIndex.BTN_EDIT_SS

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
    ''' コンボボックス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboControl(ByVal ds As DataSet)

        With Me._Frm

            '製品セグメント
            .cmbProductSegCd.Items.Clear()
            Dim subItems As List(Of ListItem) = New List(Of ListItem)
            subItems.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))
            For Each r As DataRow In ds.Tables("LMM090COMBO_SEIHINA").Rows
                subItems.Add(New ListItem(New SubItem() {New SubItem(r.Item("SEG_NM")), New SubItem(r.Item("SEG_CD"))}))
            Next
            .cmbProductSegCd.Items.AddRange(subItems.ToArray())
            .cmbProductSegCd.Refresh()

        End With

    End Sub

    ''' <summary>
    '''  フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <param name="clickObj">選択されたボタン判断する</param>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal clickObj As LMM090C.ClickObject)

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.INIT _
                , DispMode.VIEW   '初期、参照
                    .sprCust.Focus()
                Case DispMode.EDIT '編集                    
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC
                            .tabCust.SelectedTab = .tpgCustL
                            .txtCustCdL.Focus()
                        Case RecordStatus.NOMAL_REC _
                           , RecordStatus.COPY_REC
                            '選択されたボタンによりフォーカス設定を行う
                            Select Case clickObj
                                Case LMM090C.ClickObject.CUST_L
                                    .txtCustNmL.Focus()             '荷主(大)名
                                Case LMM090C.ClickObject.CUST_M
                                    .txtCustNmM.Focus()             '荷主(中)名
                                Case LMM090C.ClickObject.CUST_S
                                    .txtCustNmS.Focus()             '荷主(小)名
                                Case LMM090C.ClickObject.CUST_SS
                                    .txtCustNmSS.Focus()             '荷主(極小)名
                            End Select
                    End Select
            End Select

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal clickObj As LMM090C.ClickObject)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus(clickObj)

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
            .numHokanFree.Value = 0
            '.cmbItemCurrCd.TextValue = ""

            .optCoaInkaDateN.Checked = True     'ADD 2018/11/14 要望番号001939

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row">選択行</param>
    ''' <param name="dt">タリフ情報格納テーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer, ByVal dt As DataTable)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprCust.ActiveSheet

            .cmbBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.BR_CD.ColNo))


            '荷主(大)タブ
            .txtCustCdL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_CD_L.ColNo))
            .txtCustNmL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_NM_L.ColNo))
            .txtMainCustCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.MAIN_CUST_CD.ColNo))
            .lblMainCustNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.MAIN_CUST_NM.ColNo))
            .cmbUnsoTehaiKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UNSO_TEHAI.ColNo))
            .txtSampleSagyoCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SAMPLE_KBN.ColNo))
            .lblSampleSagyoNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SAMPLE_NM.ColNo))
            .cmbProductSegCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.PRODUCT_SEG_CD.ColNo))
            'ADD START 2018/11/14 要望番号001939
            If Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.COA_INKA_DATE_FLG.ColNo)).ToString() = "1" Then
                .optCoaInkaDateY.Checked = True
            Else
                .optCoaInkaDateN.Checked = True
            End If
            'ADD END   2018/11/14 要望番号001939

            '荷主(中)タブ
            .lblCustCdM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_CD_M.ColNo))
            .txtCustNmM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_NM_M.ColNo))
            'START YANAI 要望番号824
            .txtTantoCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.TANTO_CD.ColNo))
            .lblTantoNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.TANTO_NM.ColNo))
            'END YANAI 要望番号824
            .txtZipNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ZIP.ColNo))
            .txtAdd1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ADD1.ColNo))
            .txtAdd2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ADD2.ColNo))
            .txtAdd3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ADD3.ColNo))
            .cmbHoshoMinKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.HOSHO_MIN_KBN.ColNo))
            .cmbUnsoHokenAutoKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UNSO_HOKEN_AUTO_KBN.ColNo))        'ADD 2018/10/10  002400
            .cmbInitDateNyuka.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.INIT_INKA_PLAN_DATE_KB.ColNo))        'ADD 2018/10/31  002192
            .cmbInitDateShukka.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.INIT_OUTKA_PLAN_DATE_KB.ColNo))      'ADD 2018/10/31  002192
            .cmbKazeiKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.KAZEI.ColNo))
            .txtCustKyoriCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_KYORI_CD.ColNo))
            .lblCustKyoriNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_KYORI_NM.ColNo))
            .txtTekiyo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.REMARK.ColNo))                                   'ADD 2019/07/10 002520

            .cmbSoko.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SOKO.ColNo))
            .txtShiteiUnsoCompCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SHITEI_UNSO_COMP.ColNo))
            .txtShiteiUnsoShitenCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SHITEI_UNSO_SHISHA.ColNo))
            .lblShiteiUnsoCompNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SHITEI_UNSO_NM.ColNo))
            'ADD Start 2018/10/25 要望番号001820
            .txtInkaOrigCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.INKA_ORIG_CD.ColNo))
            .lblInkaOrigNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.INKA_ORIG_NM.ColNo))
            'ADD End   2018/10/25 要望番号001820
            .numHokanFree.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.HOKAN_FREE.ColNo))
            .cmbNyukaHokoku.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.NYUKA_HOKOKU.ColNo))
            .cmbZaikoHokoku.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SHUKKA_HOKOKU.ColNo))
            .cmbShukkaHokoku.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ZAIKO_HOKOKU.ColNo))
            '.cmbItemCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ITEM_CURR_CD.ColNo))
            .cmbItemCurrCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.ITEM_CURR_CD.ColNo))

            '荷主(小)タブ
            .lblCustCdS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_CD_S.ColNo))
            .txtCustNmS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_NM_S.ColNo))
            .txtCustBetuNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_BETU_NM.ColNo))
            .cmbShimeKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SHIME_KBN.ColNo))
            .txtTcustBpCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.TCUST_BPCD.ColNo))
            .lblTcustBpNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.TCUST_BPNM.ColNo))
            .txtSeiqCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SEIQ_CD.ColNo))
            .lblSeiqNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SEIQ_NM.ColNo))
            .txtHokanSeiqCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.HOKAN_SEIQ_CD.ColNo))
            .lblHokanSeiqNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.HOKAN_SEIQ_NM.ColNo))
            .txtNiyakuSeiqCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.NIYAKU_SEIQ_CD.ColNo))
            .lblNiyakuSeiqNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.NIYAKU_SEIQ_NM.ColNo))
            .txtUnchinSeiqCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UNCHIN_SEIQ_CD.ColNo))
            .lblUnchinSeiqNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UNCHIN_SEIQ_NM.ColNo))
            .txtSagyoSeiqCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SAGYO_SEIQ_CD.ColNo))
            .lblSagyoSeiqNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SAGYO_SEIQ_NM.ColNo))
            .cmbPikkingKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.PIKKING.ColNo))
            .cmbUnchinCalc.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UNCHIN_CALC.ColNo))

            '荷主(極小)タブ
            .lblCustCdSS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_CD_SS.ColNo))
            .txtCustNmSS.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CUST_NM_SS.ColNo))
            .txtMainTantoNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.MAIN_TANTO.ColNo))
            .txtSubTantoNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SUB_TANTO.ColNo))
            .txtTel.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.TEL.ColNo))
            .txtFax.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.FAX.ColNo))
            .txtEmail.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.EMAIL.ColNo))
            .cmbSouKaeSyori.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SOKAE_SHORI.ColNo))
            .imdLastCalc.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.LAST_CALK.ColNo))
            .imdBeforeCalc.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.BEFORE_CALK.ColNo))
            .lblLastJob.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.LAST_JOB.ColNo))
            .lblBeforeJob.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.BEFORE_JOB.ColNo))
            .cmbCalc.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CALK_UMU.ColNo))
            .cmbSeiqHakugaiHinKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.SEIQ_HAKUGAIHIN_KBN.ColNo))

            '共通項目
            .lblCreateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CREATE_USER.ColNo))
            .lblCreateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.CREATE_DATE.ColNo)))
            .lblUpdateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UPDATE_USER.ColNo))
            .lblUpdateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UPDATE_DATE.ColNo)))
            '隠し項目                           
            .lblUpdateTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM090G.sprCustDef.UPDATE_TIME.ColNo))

            'タリフ情報を設定
            Call Me.SetTriffControl(dt)

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Me._ControlG.SetDateFormat(.imdBeforeCalc, LMMControlC.DATE_FORMAT.YYYY_MM_DD)
            Me._ControlG.SetDateFormat(.imdLastCalc, LMMControlC.DATE_FORMAT.YYYY_MM_DD)

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            '荷主(中)タブ
            .numHokanFree.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(0))

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus(ByVal clickObj As LMM090C.ClickObject)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    '編集領域のクリア処理を行う
                    Call Me.ClearEditField()

                Case DispMode.VIEW

                    '編集/複写ボタン使用可能
                    Call Me._ControlG.LockButton(.btnEditL, unLock)
                    Call Me._ControlG.LockButton(.btnEditM, unLock)
                    Call Me._ControlG.LockButton(.btnEditS, unLock)
                    Call Me._ControlG.LockButton(.btnEditSS, unLock)
                    Call Me._ControlG.LockButton(.btnFukushaM, unLock)
                    Call Me._ControlG.LockButton(.btnFukushaS, unLock)
                    Call Me._ControlG.LockButton(.btnFukushaSS, unLock)

                Case DispMode.EDIT

                    'タブ活性化
                    Call Me._ControlG.SetLockControl(.tabCust, unLock)
                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbShimeKbn, lock)
                    Call Me._ControlG.LockDate(.imdBeforeCalc, lock)
                    Call Me._ControlG.LockButton(.btnEditL, lock)
                    Call Me._ControlG.LockButton(.btnEditM, lock)
                    Call Me._ControlG.LockButton(.btnEditS, lock)
                    Call Me._ControlG.LockButton(.btnEditSS, lock)
                    Call Me._ControlG.LockButton(.btnFukushaM, lock)
                    Call Me._ControlG.LockButton(.btnFukushaS, lock)
                    Call Me._ControlG.LockButton(.btnFukushaSS, lock)

                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit(clickObj)

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha(clickObj)

                    End Select
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 編集領域クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearEditField()

        With Me._Frm

            'タブ内の表示内容を初期化する
            Call Me.ClearControl(.tabCust)

            '荷主別帳票明細Spread初期化
            .sprCustPrt.CrearSpread()

            '要望番号:349 yamanaka 2012.07.11 Start
            '荷主明細Spread初期化
            .sprCustDetailL.CrearSpread()
            .sprCustDetailM.CrearSpread()
            .sprCustDetailS.CrearSpread()
            .sprCustDetailSS.CrearSpread()
            '要望番号:349 yamanaka 2012.07.11 End

            'タブ外表示項目初期化
            Call Me.ClearControl(.cmbBr)
            Call Me.ClearControl(.lblCreateUser)
            Call Me.ClearControl(.lblCreateDate)
            Call Me.ClearControl(.lblUpdateUser)
            Call Me.ClearControl(.lblUpdateDate)
            Call Me.ClearControl(.lblUpdateTime)

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        With Me._Frm

            '編集領域のクリア処理を行う
            Call Me.ClearEditField()

            '初期値を設定
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd                       '営業所
            .cmbHoshoMinKbn.SelectedValue = LMM090C.HOSHO_MIN_KBN_LOT                 '最低保証摘要単位区分
            .txtCustKyoriCd.TextValue = "000"                                         '荷主別距離程マスタコード
            .cmbCalc.SelectedValue = LMMControlC.FLG_ON                               '保管・荷役料計算有無
            .cmbSeiqHakugaiHinKbn.SelectedValue = LMM090C.SEIQ_HAKUGAIHIN_KBN_HUYO    '請求時薄害品取扱区分
            .cmbSoko.SelectedValue = LMUserInfoManager.GetWhCd                        'デフォルト倉庫コンボ
            .cmbItemCurrCd.SelectedValue = String.Empty                               '契約通貨コード
            .lblCustCdM.TextValue = "00"
            .lblCustCdS.TextValue = "00"
            .lblCustCdSS.TextValue = "00"
            .cmbUnsoHokenAutoKbn.SelectedValue = LMMControlC.FLG_OFF                      '運送保険料自動追加  ADD 2018/10/10 002400

            '運賃タリフセットマスタ情報初期設定
            '入荷
            .lblDestCdTariffN.TextValue = String.Empty
            .lblSetMstCdTariffN.TextValue = LMM090C.SET_MST_CD_NYUKA
            .lblSetKbnTariffN.TextValue = LMM090C.SET_KBN_NYUKA
            '出荷
            .lblDestCdTariffS.TextValue = String.Empty
            .lblSetMstCdTariffS.TextValue = LMM090C.SET_MST_CD_SHUKKA
            .lblSetKbnTariffS.TextValue = LMM090C.SET_KBN_SHUKKA

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit(ByVal clickObj As LMM090C.ClickObject)

        Dim lock As Boolean = True

        With Me._Frm

            '編集時常にロック項目
            Call Me._ControlG.LockText(.txtCustCdL, lock)          '荷主コード(大)

            Select Case clickObj
                Case LMM090C.ClickObject.CUST_L
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustM, lock)   '荷主(中)
                    Call Me._ControlG.SetLockControl(.tpgCustS, lock)   '荷主(小)
                    Call Me._ControlG.SetLockControl(.tpgCustSS, lock)  '荷主(極小)

                Case LMM090C.ClickObject.CUST_M
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustL, lock)   '荷主(大)
                    Call Me._ControlG.SetLockControl(.tpgCustS, lock)   '荷主(小)
                    Call Me._ControlG.SetLockControl(.tpgCustSS, lock)  '荷主(極小)


                Case LMM090C.ClickObject.CUST_S
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustL, lock)   '荷主(大)
                    Call Me._ControlG.SetLockControl(.tpgCustM, lock)   '荷主(中)
                    Call Me._ControlG.SetLockControl(.tpgCustSS, lock)  '荷主(極小)

                Case LMM090C.ClickObject.CUST_SS
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustL, lock)   '荷主(大)
                    Call Me._ControlG.SetLockControl(.tpgCustM, lock)   '荷主(中)
                    Call Me._ControlG.SetLockControl(.tpgCustS, lock)   '荷主(小)
#If True Then   'ADD 2019/05/16 依頼番号 : 005712   【LMS】荷主マスタ画面_新規以外は保管荷役料最終計算日・前回計算日を編集不可とする(SYS吉川) 
                    '保管・荷役料最終計算日
                    .imdLastCalc.ReadOnly = True
#End If

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha(ByVal clickObj As LMM090C.ClickObject)

        With Me._Frm

            Dim lock As Boolean = True

            '編集時常にロックタブ
            Call Me._ControlG.SetLockControl(.tpgCustL, lock)   '荷主(大)

            '共通クリア項目
            .imdBeforeCalc.TextValue = String.Empty             '保管・荷役料前回計算日
            .lblBeforeJob.TextValue = String.Empty              '保管・荷役料最終計算JOB番号
            .lblLastJob.TextValue = String.Empty                '保管・荷役料前回計算JOB番号
            .lblCreateDate.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty

            Select Case clickObj
                Case LMM090C.ClickObject.CUST_M
                    'クリア処理
                    .lblCustCdM.TextValue = String.Empty               '荷主コード（中)
                    .lblCustCdS.TextValue = "00"                       '荷主コード（小)
                    .lblCustCdSS.TextValue = "00"                      '荷主コード（極小)

                    '運賃タリフセットマスタ情報初期設定
                    '入荷
                    .lblDestCdTariffN.TextValue = String.Empty
                    .lblSetMstCdTariffN.TextValue = LMM090C.SET_MST_CD_NYUKA
                    .lblSetKbnTariffN.TextValue = LMM090C.SET_KBN_NYUKA
                    '出荷
                    .lblDestCdTariffS.TextValue = String.Empty
                    .lblSetMstCdTariffS.TextValue = LMM090C.SET_MST_CD_SHUKKA
                    .lblSetKbnTariffS.TextValue = LMM090C.SET_KBN_SHUKKA

#If True Then   'ADD 2020/01/07 010270【LMS】荷主追加機能_最終計算日を複写しない(高度化和地) 
                    '複写時は、最終計算日を引き継がず空白固定とする
                    Me._Frm.imdLastCalc.TextValue = String.Empty
#End If

                Case LMM090C.ClickObject.CUST_S
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustM, lock)  '荷主(中)

                    'クリア処理
                    .lblCustCdS.TextValue = String.Empty               '荷主コード（小)
                    .lblCustCdSS.TextValue = "00"                      '荷主コード（極小)

#If True Then   'ADD 2020/01/07 010270【LMS】荷主追加機能_最終計算日を複写しない(高度化和地) 
                    '複写時は、最終計算日を引き継がず空白固定とする
                    Me._Frm.imdLastCalc.TextValue = String.Empty
#End If

                Case LMM090C.ClickObject.CUST_SS
                    '編集対象外タブ頁ロック
                    Call Me._ControlG.SetLockControl(.tpgCustM, lock)  '荷主(中)
                    Call Me._ControlG.SetLockControl(.tpgCustS, lock)  '荷主(小)

                    'クリア処理
                    .lblCustCdSS.TextValue = String.Empty              '荷主コード（極小)

#If True Then   'ADD 2020/01/07 010270【LMS】荷主追加機能_最終計算日を複写しない(高度化和地) 
                    '複写時は、最終計算日を引き継がず空白固定とする
                    Me._Frm.imdLastCalc.TextValue = String.Empty
#End If

            End Select

        End With

    End Sub

    ''' <summary>
    ''' タリフパネルに値を設定する
    ''' </summary>
    ''' <param name="dt">タリフ情報格納DataTable</param>
    ''' <remarks></remarks>
    Private Sub SetTriffControl(ByVal dt As DataTable)

        With Me._Frm

            '表示対象データを取得
            Dim filter As String = String.Empty
            filter = String.Concat(filter, "NRS_BR_CD = '", .cmbBr.SelectedValue, "'")
            filter = String.Concat(filter, " AND CUST_CD_L = '", .txtCustCdL.TextValue, "'")
            filter = String.Concat(filter, " AND CUST_CD_M = '", .lblCustCdM.TextValue, "'")

            Dim nyukaDr As DataRow() = dt.Select(String.Concat(filter, " AND SET_MST_CD = '", LMM090C.SET_MST_CD_NYUKA, "'"))
            Dim shukkaDr As DataRow() = dt.Select(String.Concat(filter, " AND SET_MST_CD = '", LMM090C.SET_MST_CD_SHUKKA, "'"))

            If nyukaDr.Length > 0 Then
                .cmbTarifBunruiNyuka.SelectedValue = nyukaDr(0).Item("TARIFF_BUNRUI_KB").ToString()
                .txtUnchinTarifTonNyuka.TextValue = nyukaDr(0).Item("UNCHIN_TARIFF_CD1").ToString()
                .lblUnchinTarifTonNyuka.TextValue = nyukaDr(0).Item("UNCHIN_TARIFF_NM1").ToString()
                .txtUnchinTarifShadateNyuka.TextValue = nyukaDr(0).Item("UNCHIN_TARIFF_CD2").ToString()
                .lblUnchinTarifShadateNyuka.TextValue = nyukaDr(0).Item("UNCHIN_TARIFF_NM2").ToString()
                .txtWarimashiTarifNyuka.TextValue = nyukaDr(0).Item("EXTC_TARIFF_CD").ToString()
                .lblWarimashiTarifNyuka.TextValue = nyukaDr(0).Item("EXTC_TARIFF_NM").ToString()
                .txtYokomochiTarifNyuka.TextValue = nyukaDr(0).Item("YOKO_TARIFF_CD").ToString()
                .lblYokomochiTarifNyuka.TextValue = nyukaDr(0).Item("YOKO_TARIFF_NM").ToString()
                '隠し項目
                .lblUpdateDateTariffN.TextValue = nyukaDr(0).Item("SYS_UPD_DATE").ToString()
                .lblUpdateTimeTariffN.TextValue = nyukaDr(0).Item("SYS_UPD_TIME").ToString()
                .lblDestCdTariffN.TextValue = nyukaDr(0).Item("DEST_CD").ToString()
                .lblSetMstCdTariffN.TextValue = nyukaDr(0).Item("SET_MST_CD").ToString()
                .lblSetKbnTariffN.TextValue = nyukaDr(0).Item("SET_KB").ToString()
            End If

            If shukkaDr.Length > 0 Then
                .cmbTarifBunruiShukka.SelectedValue = shukkaDr(0).Item("TARIFF_BUNRUI_KB").ToString()
                .txtUnchinTarifTonShukka.TextValue = shukkaDr(0).Item("UNCHIN_TARIFF_CD1").ToString()
                .lblUnchinTarifTonShukka.TextValue = shukkaDr(0).Item("UNCHIN_TARIFF_NM1").ToString()
                .txtUnchinTarifShadateShukka.TextValue = shukkaDr(0).Item("UNCHIN_TARIFF_CD2").ToString()
                .lblUnchinTarifShadateShukka.TextValue = shukkaDr(0).Item("UNCHIN_TARIFF_NM2").ToString()
                .txtWarimashiTarifShukka.TextValue = shukkaDr(0).Item("EXTC_TARIFF_CD").ToString()
                .lblWarimashiTarifShukka.TextValue = shukkaDr(0).Item("EXTC_TARIFF_NM").ToString()
                .txtYokomochiTarifShukka.TextValue = shukkaDr(0).Item("YOKO_TARIFF_CD").ToString()
                .lblYokomochiTarifShukka.TextValue = shukkaDr(0).Item("YOKO_TARIFF_NM").ToString()
                '隠し項目
                .lblUpdateDateTariffS.TextValue = shukkaDr(0).Item("SYS_UPD_DATE").ToString()
                .lblUpdateTimeTariffS.TextValue = shukkaDr(0).Item("SYS_UPD_TIME").ToString()
                .lblDestCdTariffS.TextValue = shukkaDr(0).Item("DEST_CD").ToString()
                .lblSetMstCdTariffS.TextValue = shukkaDr(0).Item("SET_MST_CD").ToString()
                .lblSetKbnTariffS.TextValue = shukkaDr(0).Item("SET_KB").ToString()
            End If

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(荷主Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.STATUS, "状態", 60, True)
        Public Shared BR_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.BR_NM, "営業所", 275, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_CD, "荷主コード", 120, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_NM_L, "荷主名(大)", 180, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_NM_M, "荷主名(中)", 175, True)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_NM_S, "荷主名(小)", 175, True)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_NM_SS, "荷主名(極小)", 175, True)
        Public Shared INTEG_WEB_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.INTEG_WEB_FLG, "IntegWeb", 100, True)   'ADD 2018/11/14 要望番号001939

        '**** 隠し列 ****
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.BR_CD, "", 50, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_CD_L, "", 50, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_CD_M, "", 50, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_CD_S, "", 50, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_CD_SS, "", 50, False)
        Public Shared MAIN_CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.MAIN_CUST_CD, "", 50, False)
        Public Shared MAIN_CUST_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.MAIN_CUST_NM, "", 50, False)
        Public Shared ZIP As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ZIP, "", 50, False)
        Public Shared ADD1 As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ADD1, "", 50, False)
        Public Shared ADD2 As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ADD2, "", 50, False)
        Public Shared ADD3 As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ADD3, "", 50, False)
        Public Shared MAIN_TANTO As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.MAIN_TANTO, "", 50, False)
        Public Shared SUB_TANTO As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SUB_TANTO, "", 50, False)
        Public Shared TEL As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.TEL, "", 50, False)
        Public Shared FAX As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.FAX, "", 50, False)
        Public Shared EMAIL As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.EMAIL, "", 50, False)
        Public Shared HOSHO_MIN_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.HOSHO_MIN_KBN, "", 50, False)
        Public Shared UNSO_HOKEN_AUTO_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UNSO_HOKEN_AUTO_KBN, "", 50, False)
        Public Shared INIT_INKA_PLAN_DATE_KB As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.INIT_INKA_PLAN_DATE_KB, "", 50, False)       'ADD 2018/10/31
        Public Shared INIT_OUTKA_PLAN_DATE_KB As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.INIT_OUTKA_PLAN_DATE_KB, "", 50, False)       'ADD 2018/10/31
        Public Shared SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SEIQ_CD, "", 50, False)
        Public Shared SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SEIQ_NM, "", 50, False)
        Public Shared HOKAN_SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.HOKAN_SEIQ_CD, "", 50, False)
        Public Shared HOKAN_SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.HOKAN_SEIQ_NM, "", 50, False)
        Public Shared NIYAKU_SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.NIYAKU_SEIQ_CD, "", 50, False)
        Public Shared NIYAKU_SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.NIYAKU_SEIQ_NM, "", 50, False)
        Public Shared UNCHIN_SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UNCHIN_SEIQ_CD, "", 50, False)
        Public Shared UNCHIN_SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UNCHIN_SEIQ_NM, "", 50, False)
        Public Shared SAGYO_SEIQ_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SAGYO_SEIQ_CD, "", 50, False)
        Public Shared SAGYO_SEIQ_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SAGYO_SEIQ_NM, "", 50, False)
        Public Shared NYUKA_HOKOKU As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.NYUKA_HOKOKU, "", 50, False)
        Public Shared SHUKKA_HOKOKU As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SHUKKA_HOKOKU, "", 50, False)
        Public Shared ZAIKO_HOKOKU As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ZAIKO_HOKOKU, "", 50, False)
        Public Shared UNSO_TEHAI As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UNSO_TEHAI, "", 50, False)
        Public Shared SHITEI_UNSO_COMP As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SHITEI_UNSO_COMP, "", 50, False)
        Public Shared SHITEI_UNSO_SHISHA As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SHITEI_UNSO_SHISHA, "", 50, False)
        Public Shared SHITEI_UNSO_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SHITEI_UNSO_NM, "", 50, False)
        Public Shared CUST_KYORI_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_KYORI_CD, "", 50, False)
        Public Shared CUST_KYORI_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_KYORI_NM, "", 50, False)
        Public Shared KAZEI As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.KAZEI, "", 50, False)
        Public Shared HOKAN_FREE As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.HOKAN_FREE, "", 50, False)
        Public Shared SAMPLE_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SAMPLE_KBN, "", 50, False)
        Public Shared SAMPLE_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SAMPLE_NM, "", 50, False)
        Public Shared PRODUCT_SEG_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.PRODUCT_SEG_CD, "", 50, False)
        Public Shared PRODUCT_SEG_NM_L As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.PRODUCT_SEG_NM_L, "", 50, False)
        Public Shared PRODUCT_SEG_NM_M As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.PRODUCT_SEG_NM_M, "", 50, False)
        Public Shared TCUST_BPCD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.TCUST_BPCD, "", 50, False)
        Public Shared TCUST_BPNM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.TCUST_BPNM, "", 50, False)
        Public Shared LAST_CALK As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.LAST_CALK, "", 50, False)
        Public Shared BEFORE_CALK As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.BEFORE_CALK, "", 50, False)
        Public Shared LAST_JOB As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.LAST_JOB, "", 50, False)
        Public Shared BEFORE_JOB As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.BEFORE_JOB, "", 50, False)
        Public Shared CALK_UMU As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CALK_UMU, "", 50, False)
        Public Shared UNCHIN_CALC As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UNCHIN_CALC, "", 50, False)
        Public Shared CUST_BETU_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CUST_BETU_NM, "", 50, False)
        Public Shared SOKAE_SHORI As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SOKAE_SHORI, "", 50, False)
        Public Shared SOKO As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SOKO, "", 50, False)
        Public Shared PIKKING As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.PIKKING, "", 50, False)
        Public Shared SEIQ_HAKUGAIHIN_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SEIQ_HAKUGAIHIN_KBN, "", 50, False)
        Public Shared SHIME_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SHIME_KBN, "", 50, False)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CREATE_DATE, "", 50, False)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.CREATE_USER, "", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UPDATE_DATE, "", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UPDATE_USER, "", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.UPDATE_TIME, "", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.SYS_DEL_FLG, "", 50, False)
        'START YANAI 要望番号824
        Public Shared TANTO_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.TANTO_CD, "", 0, False)
        Public Shared TANTO_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.TANTO_NM, "", 0, False)
        'END YANAI 要望番号824
        'START OU 要望番号2229
        Public Shared ITEM_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.ITEM_CURR_CD, "", 0, False)
        'END OU 要望番号2229
        'ADD Start 2018/10/25 要望番号001820
        Public Shared INKA_ORIG_CD As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.INKA_ORIG_CD, "", 50, False)
        Public Shared INKA_ORIG_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.INKA_ORIG_NM, "", 50, False)
        'ADD End   2018/10/25 要望番号001820
        Public Shared COA_INKA_DATE_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.COA_INKA_DATE_FLG, "", 50, False)   'ADD 2018/11/14 要望番号001939
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustColumnIndex.REMARK, "", 50, False)   'ADD 2018/11/14 要望番号001939

    End Class

    ''' <summary>
    ''' スプレッド列定義体(荷主別帳票明細Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustPrtDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustPrtColumnIndex.DEF, " ", 20, True)
        Public Shared PTN_ID As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustPrtColumnIndex.PTN_ID, "帳票種類ＩＤ", 200, True)
        Public Shared PTN_NM As SpreadColProperty = New SpreadColProperty(LMM090C.SprCustPrtColumnIndex.PTN_NM, "帳票パターン名", 250, True)

    End Class

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' スプレッド列定義体(荷主明細Spread大)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustDetailL

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.DEF, " ", 20, True)
        Public Shared EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.EDA_NO, "枝番", 60, True)
        Public Shared YOTO_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.YOTO_KBN, "用途区分", 100, True)
        Public Shared SETTEI_VALUE As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, "設定値", 180, True)
        Public Shared SETTEI_VALUE2 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, "設定値2", 180, True)
        Public Shared SETTEI_VALUE3 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, "設定値3", 180, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.BIKO, "備考", 180, True)

        '**** 隠し列 ****
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CD, "荷主コード", 50, False)
        Public Shared CUST_CLASS As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CLASS, "荷主階層", 50, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.UPD_FLG, "更新区分", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)
        Public Shared MAX_EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, "最大枝番", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(荷主明細Spread中)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustDetailM

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.DEF, " ", 20, True)
        Public Shared EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.EDA_NO, "枝番", 60, True)
        Public Shared YOTO_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.YOTO_KBN, "用途区分", 100, True)
        Public Shared SETTEI_VALUE As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, "設定値", 180, True)
        Public Shared SETTEI_VALUE2 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, "設定値2", 180, True)
        Public Shared SETTEI_VALUE3 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, "設定値3", 180, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.BIKO, "備考", 180, True)

        '**** 隠し列 ****
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CD, "荷主コード", 50, False)
        Public Shared CUST_CLASS As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CLASS, "荷主階層", 50, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.UPD_FLG, "更新区分", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)
        Public Shared MAX_EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, "最大枝番", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(荷主明細Spread小)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustDetailS

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.DEF, " ", 20, True)
        Public Shared EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.EDA_NO, "枝番", 60, True)
        Public Shared YOTO_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.YOTO_KBN, "用途区分", 100, True)
        Public Shared SETTEI_VALUE As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, "設定値", 180, True)
        Public Shared SETTEI_VALUE2 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, "設定値2", 180, True)
        Public Shared SETTEI_VALUE3 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, "設定値3", 180, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.BIKO, "備考", 180, True)

        '**** 隠し列 ****
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CD, "荷主コード", 50, False)
        Public Shared CUST_CLASS As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CLASS, "荷主階層", 50, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.UPD_FLG, "更新区分", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)
        Public Shared MAX_EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, "最大枝番", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(荷主明細Spread極小)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprCustDetailSS

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.DEF, " ", 20, True)
        Public Shared EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.EDA_NO, "枝番", 60, True)
        Public Shared YOTO_KBN As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.YOTO_KBN, "用途区分", 100, True)
        Public Shared SETTEI_VALUE As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, "設定値", 180, True)
        Public Shared SETTEI_VALUE2 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, "設定値2", 180, True)
        Public Shared SETTEI_VALUE3 As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, "設定値3", 180, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.BIKO, "備考", 180, True)

        '**** 隠し列 ****
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CD, "荷主コード", 50, False)
        Public Shared CUST_CLASS As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.CUST_CLASS, "荷主階層", 50, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.UPD_FLG, "更新区分", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)
        Public Shared MAX_EDA_NO As SpreadColProperty = New SpreadColProperty(LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, "最大枝番", 50, False)

    End Class
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '荷主Spreadの初期化処理
        Call Me.InitCustSpread()

        '荷主別帳票明細Spreadの初期化処理
        Call Me.InitCustPrtSpread()

        '要望番号:349 yamanaka 2012.07.10 Start
        '荷主明細Spreadの初期化処理
        Call Me.InitCustDetailSpread()
        '要望番号:349 yamanaka 2012.07.10 End

    End Sub

    ''' <summary>
    ''' 検索結果を荷主Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprCust

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
            Dim lblC As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprCustDef.DEF.ColNo, def)
                .SetCellStyle(i, sprCustDef.STATUS.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.BR_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_NM_L.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_NM_M.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_NM_S.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_NM_SS.ColNo, lblL)

                '**** 隠し列 ****
                .SetCellStyle(i, sprCustDef.BR_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_CD_L.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_CD_M.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_CD_S.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_CD_SS.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.MAIN_CUST_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.MAIN_CUST_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.ZIP.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.ADD1.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.ADD2.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.ADD3.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.MAIN_TANTO.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SUB_TANTO.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.TEL.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.FAX.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.EMAIL.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.HOSHO_MIN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SEIQ_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SEIQ_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.HOKAN_SEIQ_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.HOKAN_SEIQ_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.NIYAKU_SEIQ_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.NIYAKU_SEIQ_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UNCHIN_SEIQ_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UNCHIN_SEIQ_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SAGYO_SEIQ_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SAGYO_SEIQ_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.NYUKA_HOKOKU.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SHUKKA_HOKOKU.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.ZAIKO_HOKOKU.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UNSO_TEHAI.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SHITEI_UNSO_COMP.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SHITEI_UNSO_SHISHA.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SHITEI_UNSO_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_KYORI_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_KYORI_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.KAZEI.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.HOKAN_FREE.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SAMPLE_KBN.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SAMPLE_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.PRODUCT_SEG_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.PRODUCT_SEG_NM_L.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.PRODUCT_SEG_NM_M.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.TCUST_BPCD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.TCUST_BPNM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.LAST_CALK.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.BEFORE_CALK.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.LAST_JOB.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.BEFORE_JOB.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CALK_UMU.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UNCHIN_CALC.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CUST_BETU_NM.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SOKAE_SHORI.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SOKO.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.PIKKING.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SEIQ_HAKUGAIHIN_KBN.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SHIME_KBN.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CREATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.CREATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UPDATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UPDATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.UPDATE_TIME.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.SYS_DEL_FLG.ColNo, lblL)
                'START YANAI 要望番号824
                .SetCellStyle(i, sprCustDef.TANTO_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.TANTO_NM.ColNo, lblL)
                'END YANAI 要望番号824
                'START OU 要望番号2229
                .SetCellStyle(i, sprCustDef.ITEM_CURR_CD.ColNo, lblL)
                'END OU 要望番号2229
                'ADD Start 2018/10/25 要望番号001820
                .SetCellStyle(i, sprCustDef.INKA_ORIG_CD.ColNo, lblL)
                .SetCellStyle(i, sprCustDef.INKA_ORIG_NM.ColNo, lblL)
                'ADD End   2018/10/25 要望番号001820
                .SetCellStyle(i, sprCustDef.COA_INKA_DATE_FLG.ColNo, lblL)  'ADD 2018/11/14 要望番号001939
                .SetCellStyle(i, sprCustDef.INTEG_WEB_FLG.ColNo, lblC)      'ADD 2018/12/28 依頼番号 : 003453

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprCustDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprCustDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprCustDef.BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprCustDef.CUST_CD.ColNo, dr.Item("CUST_CD").ToString())
                .SetCellValue(i, sprCustDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, sprCustDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, sprCustDef.CUST_NM_S.ColNo, dr.Item("CUST_NM_S").ToString())
                .SetCellValue(i, sprCustDef.CUST_NM_SS.ColNo, dr.Item("CUST_NM_SS").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprCustDef.BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprCustDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprCustDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprCustDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprCustDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, sprCustDef.MAIN_CUST_CD.ColNo, dr.Item("CUST_OYA_CD").ToString())
                .SetCellValue(i, sprCustDef.MAIN_CUST_NM.ColNo, dr.Item("CUST_OYA_NM").ToString())
                .SetCellValue(i, sprCustDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(i, sprCustDef.ADD1.ColNo, dr.Item("AD_1").ToString())
                .SetCellValue(i, sprCustDef.ADD2.ColNo, dr.Item("AD_2").ToString())
                .SetCellValue(i, sprCustDef.ADD3.ColNo, dr.Item("AD_3").ToString())
                .SetCellValue(i, sprCustDef.MAIN_TANTO.ColNo, dr.Item("PIC").ToString())
                .SetCellValue(i, sprCustDef.SUB_TANTO.ColNo, dr.Item("FUKU_PIC").ToString())
                .SetCellValue(i, sprCustDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(i, sprCustDef.FAX.ColNo, dr.Item("FAX").ToString())
                .SetCellValue(i, sprCustDef.EMAIL.ColNo, dr.Item("MAIL").ToString())
                .SetCellValue(i, sprCustDef.HOSHO_MIN_KBN.ColNo, dr.Item("SAITEI_HAN_KB").ToString())
                .SetCellValue(i, sprCustDef.UNSO_HOKEN_AUTO_KBN.ColNo, dr.Item("UNSO_HOKEN_AUTO_YN").ToString())       'ADD 2018/10/22 002400
                .SetCellValue(i, sprCustDef.INIT_INKA_PLAN_DATE_KB.ColNo, dr.Item("INIT_INKA_PLAN_DATE_KB").ToString())     'ADD 2018/10/31 002400
                .SetCellValue(i, sprCustDef.INIT_OUTKA_PLAN_DATE_KB.ColNo, dr.Item("INIT_OUTKA_PLAN_DATE_KB").ToString())   'ADD 2018/10/31 002400
                .SetCellValue(i, sprCustDef.SEIQ_CD.ColNo, dr.Item("OYA_SEIQTO_CD").ToString())
                .SetCellValue(i, sprCustDef.SEIQ_NM.ColNo, dr.Item("OYA_SEIQTO_NM").ToString())
                .SetCellValue(i, sprCustDef.HOKAN_SEIQ_CD.ColNo, dr.Item("HOKAN_SEIQTO_CD").ToString())
                .SetCellValue(i, sprCustDef.HOKAN_SEIQ_NM.ColNo, dr.Item("HOKAN_SEIQTO_NM").ToString())
                .SetCellValue(i, sprCustDef.NIYAKU_SEIQ_CD.ColNo, dr.Item("NIYAKU_SEIQTO_CD").ToString())
                .SetCellValue(i, sprCustDef.NIYAKU_SEIQ_NM.ColNo, dr.Item("NIYAKU_SEIQTO_NM").ToString())
                .SetCellValue(i, sprCustDef.UNCHIN_SEIQ_CD.ColNo, dr.Item("UNCHIN_SEIQTO_CD").ToString())
                .SetCellValue(i, sprCustDef.UNCHIN_SEIQ_NM.ColNo, dr.Item("UNCHIN_SEIQTO_NM").ToString())
                .SetCellValue(i, sprCustDef.SAGYO_SEIQ_CD.ColNo, dr.Item("SAGYO_SEIQTO_CD").ToString())
                .SetCellValue(i, sprCustDef.SAGYO_SEIQ_NM.ColNo, dr.Item("SAGYO_SEIQTO_NM").ToString())
                .SetCellValue(i, sprCustDef.NYUKA_HOKOKU.ColNo, dr.Item("INKA_RPT_YN").ToString())
                .SetCellValue(i, sprCustDef.SHUKKA_HOKOKU.ColNo, dr.Item("OUTKA_RPT_YN").ToString())
                .SetCellValue(i, sprCustDef.ZAIKO_HOKOKU.ColNo, dr.Item("ZAI_RPT_YN").ToString())
                .SetCellValue(i, sprCustDef.UNSO_TEHAI.ColNo, dr.Item("UNSO_TEHAI_KB").ToString())
                .SetCellValue(i, sprCustDef.SHITEI_UNSO_COMP.ColNo, dr.Item("SP_UNSO_CD").ToString())
                .SetCellValue(i, sprCustDef.SHITEI_UNSO_SHISHA.ColNo, dr.Item("SP_UNSO_BR_CD").ToString())
                .SetCellValue(i, sprCustDef.SHITEI_UNSO_NM.ColNo, dr.Item("SP_UNSO_NM").ToString())
                .SetCellValue(i, sprCustDef.CUST_KYORI_CD.ColNo, dr.Item("BETU_KYORI_CD").ToString())
                .SetCellValue(i, sprCustDef.CUST_KYORI_NM.ColNo, dr.Item("BETU_KYORI_REM").ToString())
                .SetCellValue(i, sprCustDef.KAZEI.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, sprCustDef.HOKAN_FREE.ColNo, dr.Item("HOKAN_FREE_KIKAN").ToString())
                .SetCellValue(i, sprCustDef.SAMPLE_KBN.ColNo, dr.Item("SMPL_SAGYO").ToString())
                .SetCellValue(i, sprCustDef.SAMPLE_NM.ColNo, dr.Item("SMPL_SAGYO_NM").ToString())
                .SetCellValue(i, sprCustDef.PRODUCT_SEG_CD.ColNo, dr.Item("PRODUCT_SEG_CD").ToString())
                .SetCellValue(i, sprCustDef.PRODUCT_SEG_NM_L.ColNo, dr.Item("PRODUCT_SEG_NM_L").ToString())
                .SetCellValue(i, sprCustDef.PRODUCT_SEG_NM_M.ColNo, dr.Item("PRODUCT_SEG_NM_M").ToString())
                .SetCellValue(i, sprCustDef.TCUST_BPCD.ColNo, dr.Item("TCUST_BPCD").ToString())
                .SetCellValue(i, sprCustDef.TCUST_BPNM.ColNo, dr.Item("TCUST_BPNM").ToString())
                .SetCellValue(i, sprCustDef.LAST_CALK.ColNo, dr.Item("HOKAN_NIYAKU_CALCULATION").ToString())
                .SetCellValue(i, sprCustDef.BEFORE_CALK.ColNo, dr.Item("HOKAN_NIYAKU_CALCULATION_OLD").ToString())
                .SetCellValue(i, sprCustDef.LAST_JOB.ColNo, dr.Item("NEW_JOB_NO").ToString())
                .SetCellValue(i, sprCustDef.BEFORE_JOB.ColNo, dr.Item("OLD_JOB_NO").ToString())
                .SetCellValue(i, sprCustDef.CALK_UMU.ColNo, dr.Item("HOKAN_NIYAKU_KEISAN_YN").ToString())
                .SetCellValue(i, sprCustDef.UNCHIN_CALC.ColNo, dr.Item("UNTIN_CALCULATION_KB").ToString())
                .SetCellValue(i, sprCustDef.CUST_BETU_NM.ColNo, dr.Item("DENPYO_NM").ToString())
                .SetCellValue(i, sprCustDef.SOKAE_SHORI.ColNo, dr.Item("SOKO_CHANGE_KB").ToString())
                .SetCellValue(i, sprCustDef.SOKO.ColNo, dr.Item("DEFAULT_SOKO_CD").ToString())
                .SetCellValue(i, sprCustDef.PIKKING.ColNo, dr.Item("PICK_LIST_KB").ToString())
                .SetCellValue(i, sprCustDef.SEIQ_HAKUGAIHIN_KBN.ColNo, dr.Item("SEKY_OFB_KB").ToString())
                .SetCellValue(i, sprCustDef.SHIME_KBN.ColNo, dr.Item("CLOSE_KB").ToString())
                .SetCellValue(i, sprCustDef.CREATE_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, sprCustDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, sprCustDef.UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprCustDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, sprCustDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprCustDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                'START YANAI 要望番号824
                .SetCellValue(i, sprCustDef.TANTO_CD.ColNo, dr.Item("TANTO_CD").ToString())
                .SetCellValue(i, sprCustDef.TANTO_NM.ColNo, dr.Item("TANTO_NM").ToString())
                'END YANAI 要望番号824
                'START OU 要望番号2229
                .SetCellValue(i, sprCustDef.ITEM_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                'END OU 要望番号2229
                'ADD Start 2018/10/25 要望番号001820
                .SetCellValue(i, sprCustDef.INKA_ORIG_CD.ColNo, dr.Item("INKA_ORIG_CD").ToString())
                .SetCellValue(i, sprCustDef.INKA_ORIG_NM.ColNo, dr.Item("INKA_ORIG_NM").ToString())
                'ADD End   2018/10/25 要望番号001820
                .SetCellValue(i, sprCustDef.COA_INKA_DATE_FLG.ColNo, dr.Item("COA_INKA_DATE_FLG").ToString())    'ADD 2018/11/14 要望番号001939

                .SetCellValue(i, sprCustDef.REMARK.ColNo, dr.Item("REMARK").ToString())         'ADD 2019/07/10 002520
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 表示内容を荷主別帳票明細Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadPrt(ByVal dt As DataTable, ByVal clickObj As LMM090C.ClickObject)

        '極小の場合、スルー
        If LMM090C.ClickObject.CUST_SS = clickObj Then
            Exit Sub
        End If


        Dim spr As LMSpread = Me._Frm.sprCustPrt

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
            Else
                edit = True
            End If

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim ptnId As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_T007, edit)

            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprCustPrtDef.DEF.ColNo, def)
                .SetCellStyle(i, sprCustPrtDef.PTN_ID.ColNo, ptnId)
                .SetCellStyle(i, sprCustPrtDef.PTN_NM.ColNo, Me.StyleInfoPtnNm(dr.Item("PTN_ID").ToString(), edit))

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprCustPrtDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprCustPrtDef.PTN_ID.ColNo, dr.Item("PTN_ID").ToString())
                .SetCellValue(i, sprCustPrtDef.PTN_NM.ColNo, dr.Item("PTN_CD").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 荷主別帳票明細Spreadに一行追加する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRow()

        Dim spr As LMSpread = Me._Frm.sprCustPrt

        With spr

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim ptnId As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_T007, unlock)

            Dim addRow As Integer = .ActiveSheet.Rows.Count - 1

            'セルスタイル設定
            '**** 表示列 ****
            .SetCellStyle(addRow, sprCustPrtDef.DEF.ColNo, def)
            .SetCellStyle(addRow, sprCustPrtDef.PTN_ID.ColNo, ptnId)
            .SetCellStyle(addRow, sprCustPrtDef.PTN_NM.ColNo, Me.StyleInfoPtnNm("", unlock))

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(addRow, sprCustPrtDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(addRow, sprCustPrtDef.PTN_ID.ColNo, "")
            .SetCellValue(addRow, sprCustPrtDef.PTN_NM.ColNo, "")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 荷主別帳票明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprCustPrt.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 表示内容を荷主明細Spreadに表示(共通)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <param name="spr">Spread</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadDtl(ByVal dt As DataTable, ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

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
                                .Item("CUST_CD_EDA") = String.Empty
                            End With
                        Next
                End Select
            Else
                edit = True
            End If

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim yotoKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Y008, edit)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, edit)

            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.DEF, def)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.EDA_NO, lblL)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.YOTO_KBN, yotoKbn)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, text)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, text)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, text)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.BIKO, text)

                '**** 隠し列 ****
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.CUST_CD, lblL)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.CUST_CLASS, lblL)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.UPD_FLG, lblL)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, lblL)
                .SetCellStyle(i, LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, lblL)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.DEF, LMConst.FLG.OFF)
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.EDA_NO, dr.Item("CUST_CD_EDA").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.YOTO_KBN, dr.Item("SUB_KB").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, dr.Item("SET_NAIYO").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, dr.Item("SET_NAIYO_2").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, dr.Item("SET_NAIYO_3").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.BIKO, dr.Item("REMARK").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.CUST_CD, dr.Item("CUST_CD").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.CUST_CLASS, dr.Item("CUST_CLASS").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.UPD_FLG, dr.Item("UPD_FLG").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, LMM090C.MAX_EDA_NO.ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 荷主明細Spreadに一行追加する(共通)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub DetailAddRow(ByVal maxEda As Integer, ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        With spr

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim yotoKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Y008, unlock)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, unlock)
            Dim maxEdaNo As String = String.Empty
            Dim addRow As Integer = spr.ActiveSheet.Rows.Count - 1

            'セルスタイル設定
            '**** 表示列 ****
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.DEF, def)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.EDA_NO, lblL)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.YOTO_KBN, yotoKbn)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, text)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2, text)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3, text)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.BIKO, text)

            '****隠れ列 ****
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.CUST_CD, lblL)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.CUST_CLASS, lblL)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.UPD_FLG, lblL)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, lblL)
            .SetCellStyle(addRow, LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, lblL)

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.DEF, LMConst.FLG.OFF)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.EDA_NO, String.Empty)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.YOTO_KBN, String.Empty)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE, String.Empty)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.BIKO, String.Empty)

            '****隠し列 ****
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.CUST_CD, Me._Frm.txtCustCdL.TextValue)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.CUST_CLASS, LMM090C.CUST_CLASS_L)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.UPD_FLG, String.Empty)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.SYS_DEL_FLG, String.Empty)
            .SetCellValue(addRow, LMM090C.sprCustDetailColumnIndex.MAX_EDA_NO, String.Empty)

            .ResumeLayout(True)
        End With


    End Sub

    ''' <summary>
    ''' 荷主明細Spreadの行削除を行う(共通)
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DetailDelate(ByVal list As ArrayList, ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            spr.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub
    '要望番号:349 yamanaka 2012.07.10 End

#Region "内部メソッド"

    ''' <summary>
    ''' 荷主スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitCustSpread()

        '商品Spreadの初期値設定
        Dim sprCust As LMSpread = Me._Frm.sprCust
        Dim dr As DataRow
        With sprCust

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 82

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustDef)
            .SetColProperty(New LMM090G.sprCustDef(), False)
            '2015.10.15 英語化対応END


            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprCust)

            '**** 表示列 ****
            .SetCellStyle(0, sprCustDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(sprCust, LMKbnConst.KBN_S051, False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprCustDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprCust, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprCustDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprCust, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .SetCellStyle(0, sprCustDef.CUST_CD.ColNo, LMSpreadUtility.GetTextCell(sprCust, InputControl.HAN_NUM_ALPHA, 11, False))
            .SetCellStyle(0, sprCustDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(sprCust, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprCustDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(sprCust, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprCustDef.CUST_NM_S.ColNo, LMSpreadUtility.GetTextCell(sprCust, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprCustDef.CUST_NM_SS.ColNo, LMSpreadUtility.GetTextCell(sprCust, InputControl.ALL_MIX, 60, False))
            '.SetCellStyle(0, sprCustDef.INTEG_WEB_FLG.ColNo, LMSpreadUtility.GetComboCellKbn(.sprCustDef, LMKbnConst.KBN_U001, False))
            .SetCellStyle(0, sprCustDef.INTEG_WEB_FLG.ColNo, LMSpreadUtility.GetComboCellKbn(sprCust, "U036", False))                  '使用フラグ
            '2013.02.27 / Notes1897終了

            '**** 隠し列 ****
            .SetCellStyle(0, sprCustDef.BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_CD_S.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_CD_SS.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.MAIN_CUST_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.MAIN_CUST_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.ZIP.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.ADD1.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.ADD2.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.ADD3.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.MAIN_TANTO.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SUB_TANTO.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.TEL.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.FAX.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.EMAIL.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.HOSHO_MIN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SEIQ_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SEIQ_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.HOKAN_SEIQ_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.HOKAN_SEIQ_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.NIYAKU_SEIQ_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.NIYAKU_SEIQ_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UNCHIN_SEIQ_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UNCHIN_SEIQ_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SAGYO_SEIQ_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SAGYO_SEIQ_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.NYUKA_HOKOKU.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SHUKKA_HOKOKU.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.ZAIKO_HOKOKU.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UNSO_TEHAI.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SHITEI_UNSO_COMP.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SHITEI_UNSO_SHISHA.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SHITEI_UNSO_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_KYORI_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_KYORI_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.KAZEI.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.HOKAN_FREE.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SAMPLE_KBN.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SAMPLE_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.PRODUCT_SEG_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.PRODUCT_SEG_NM_L.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.PRODUCT_SEG_NM_M.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.TCUST_BPCD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.TCUST_BPNM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.LAST_CALK.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.BEFORE_CALK.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.LAST_JOB.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.BEFORE_JOB.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CALK_UMU.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UNCHIN_CALC.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CUST_BETU_NM.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SOKAE_SHORI.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SOKO.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.PIKKING.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SEIQ_HAKUGAIHIN_KBN.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SHIME_KBN.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.SYS_DEL_FLG.ColNo, lbl)
            'START YANAI 要望番号824
            .SetCellStyle(0, sprCustDef.TANTO_CD.ColNo, lbl)
            .SetCellStyle(0, sprCustDef.TANTO_NM.ColNo, lbl)
            'END YANAI 要望番号824
            .SetCellStyle(0, sprCustDef.COA_INKA_DATE_FLG.ColNo, lbl)   'ADD 2018/11/14 要望番号001939
            .SetCellStyle(0, sprCustDef.REMARK.ColNo, lbl)              'ADD 2019/07/10 002520

            '初期値設定
            Call Me._ControlG.ClearControl(sprCust)
            .SetCellValue(0, sprCustDef.STATUS.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprCustDef.BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
        End With

    End Sub

    ''' <summary>
    ''' 荷主別帳票明細Spreadの初期化設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitCustPrtSpread()

        '商品明細Spreadの初期値設定
        Dim sprCustDtl As LMSpread = Me._Frm.sprCustPrt

        With sprCustDtl

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 3

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustPrtDef)
            .SetColProperty(New LMM090G.sprCustPrtDef(), False)
            '2015.10.15 英語化対応END

        End With

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(帳票パターン名)
    ''' </summary>
    ''' <param name="ptnId">帳票種別ID</param>
    ''' <param name="lock">ロック制御</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoPtnNm(ByVal ptnId As String _
                                  , ByVal lock As Boolean _
                                  ) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(Me._Frm.sprCustPrt _
                                                  , LMConst.CacheTBL.RPT _
                                                  , "PTN_CD" _
                                                  , "PTN_NM" _
                                                  , lock _
                                                  , New String() {"NRS_BR_CD", "PTN_ID"} _
                                                  , New String() {Me._Frm.cmbBr.SelectedValue.ToString(), ptnId} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 荷主明細Spreadの初期化設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitCustDetailSpread()

        '荷主明細Spreadの初期値設定
        Dim sprCustDtlL As LMSpread = Me._Frm.sprCustDetailL
        Dim sprCustDtlM As LMSpread = Me._Frm.sprCustDetailM
        Dim sprCustDtlS As LMSpread = Me._Frm.sprCustDetailS
        Dim sprCustDtlSS As LMSpread = Me._Frm.sprCustDetailSS

        '荷主明細スプレッド(大)
        With sprCustDtlL
            'スプレッドの行をクリア
            .CrearSpread()
            '列数設定
            .ActiveSheet.ColumnCount = LMM090C.sprCustDetailColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustDetailL)
            .SetColProperty(New LMM090G.sprCustDetailL(), False)
            '2015.10.15 英語化対応END

        End With

        '荷主明細スプレッド(中)
        With sprCustDtlM
            'スプレッドの行をクリア
            .CrearSpread()
            '列数設定
            .ActiveSheet.ColumnCount = LMM090C.sprCustDetailColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustDetailM)
            .SetColProperty(New LMM090G.sprCustDetailM(), False)
            '2015.10.15 英語化対応END

        End With

        '荷主明細スプレッド(小)
        With sprCustDtlS
            'スプレッドの行をクリア
            .CrearSpread()
            '列数設定
            .ActiveSheet.ColumnCount = LMM090C.sprCustDetailColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustDetailS)
            .SetColProperty(New LMM090G.sprCustDetailS(), False)
            '2015.10.15 英語化対応END

        End With

        '荷主明細スプレッド(小)
        With sprCustDtlSS
            'スプレッドの行をクリア
            .CrearSpread()
            '列数設定
            .ActiveSheet.ColumnCount = LMM090C.sprCustDetailColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprCustDetailSS)
            .SetColProperty(New LMM090G.sprCustDetailSS(), False)
            '2015.10.15 英語化対応END

        End With

    End Sub
    '要望番号:349 yamanaka 2012.07.10 End

#End Region

#End Region

#End Region

#End Region

End Class
