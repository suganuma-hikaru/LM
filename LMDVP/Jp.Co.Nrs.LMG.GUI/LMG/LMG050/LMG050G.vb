' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG050G : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Win.Utility
Imports GrapeCity.Win.Editors

''' <summary>
''' LMG050Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG050G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG050F

    ''' <summary>
    ''' 画面共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

    ''' <summary>
    ''' ハンドラ共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMGControlH

    ''' <summary>
    ''' 非TSMC請求先 取込項目 直近退避値
    ''' </summary>
    Private _LastTorikomiChkValueNotTsmcDict As New Dictionary(Of String, String)

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <param name="g">請求サブ画面共通クラス</param>
    ''' <param name="h">請求サブハンドラ共通クラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG050F, ByVal g As LMGControlG, ByVal h As LMGControlH)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g
        Me._ControlH = h

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal keiriModoshiFlg As Boolean)

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMGControlC.FUNCTION_HENSHU
            .F3ButtonName = LMGControlC.FUNCTION_FUKKATSU                'UPD 20018/08/20  依頼番号 : 002136  String.Empty 
            .F4ButtonName = LMGControlC.FUNCTION_SAKUJO
            .F5ButtonName = LMGControlC.FUNCTION_KAKUTEI
            .F6ButtonName = LMGControlC.FUNCTION_TORIKOMI
            .F7ButtonName = LMGControlC.FUNCTION_SHOKIKA
            .F8ButtonName = LMGControlC.FUNCTION_KEIRI_TAISHOGAI
            .F9ButtonName = LMGControlC.FUNCTION_KEIRI_MODOSHI
            .F10ButtonName = LMGControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = LMGControlC.FUNCTION_HOZON
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

            '常に使用可能・不可キー
            .F1ButtonEnabled = lock
            '.F3ButtonEnabled = lock        'UPD 2018/08/21 依頼番号 : 002136 
            .F12ButtonEnabled = unLock

            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT)
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW)

            Dim F2lock As Boolean = view
            Dim F3lock As Boolean = view        'ADD 2018/08/21 依頼番号 : 002136 
            Dim F4lock As Boolean = view
            Dim F5lock As Boolean = view
            Dim F6lock As Boolean = edit
            Dim F7lock As Boolean = view
            Dim F8lock As Boolean = view
            Dim F9lock As Boolean = view
            Dim F10lock As Boolean = edit
            Dim F11lock As Boolean = edit

            '手書きデータの場合、ロック
            If Me._Frm.cmbSeiqtShubetu.SelectedValue.ToString.Equals(LMGControlC.CRT_TEGAKI) Then
                F6lock = edit AndAlso lock
            End If

            '赤黒区分による制御と、経理戻し済みデータの制御を行う
            If Me._Frm.chkAkaden.GetBinaryValue() = LMConst.FLG.ON _
            OrElse keiriModoshiFlg = True Then
                F2lock = F2lock AndAlso lock
                F3lock = F3lock AndAlso lock         'ADD 2018/08/21 依頼番号 : 002136 
                F4lock = F4lock AndAlso lock
                F5lock = F5lock AndAlso lock
                F6lock = F6lock AndAlso lock
                F7lock = F7lock AndAlso lock
                F8lock = F8lock AndAlso lock
                F9lock = F9lock AndAlso lock
                F10lock = F10lock AndAlso lock
                F11lock = F11lock AndAlso lock
            ElseIf Me._Frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then         'ADD 2018/08/21 依頼番号 : 002136 
                F2lock = F2lock AndAlso lock
                F3lock = F3lock AndAlso unLock
                F4lock = F4lock AndAlso lock
                F5lock = F5lock AndAlso lock
                F6lock = F6lock AndAlso lock
                F7lock = F7lock AndAlso lock
                F8lock = F8lock AndAlso lock
                F9lock = F9lock AndAlso lock
                F10lock = F10lock AndAlso lock
                F11lock = F11lock AndAlso lock
            Else
                F2lock = F2lock AndAlso unLock
                F3lock = F3lock AndAlso lock         'ADD 2018/08/21 依頼番号 : 002136 
                F4lock = F4lock AndAlso unLock
                F5lock = F5lock AndAlso unLock
                F6lock = F6lock AndAlso unLock
                F7lock = F7lock AndAlso unLock
                F8lock = F8lock AndAlso unLock
                F9lock = F9lock AndAlso unLock
                F10lock = F10lock AndAlso unLock
                F11lock = F11lock AndAlso unLock
            End If

            '進捗区分によるロック
            Select Case Me._Frm.cmbStateKbn.SelectedValue.ToString()
                Case LMG050C.STATE_MIKAKUTEI            '未確定
                    F2lock = F2lock AndAlso unLock
                    F4lock = F4lock AndAlso unLock
                    F5lock = F5lock AndAlso unLock
                    F6lock = F6lock AndAlso unLock
                    F7lock = F7lock AndAlso lock
                    F8lock = F8lock AndAlso lock
                    F9lock = F9lock AndAlso lock
                    F10lock = F10lock AndAlso unLock
                    F11lock = F11lock AndAlso unLock

                Case LMG050C.STATE_KAKUTEI              '確定
                    '2011/08/04 菱刈 検証一覧結果 No2 スタート
                    F2lock = F2lock AndAlso unLock
                    '2011/08/04 菱刈 検証一覧結果 No2 エンド
                    F4lock = F4lock AndAlso lock
                    F5lock = F5lock AndAlso lock
                    F6lock = F6lock AndAlso lock
                    F7lock = F7lock AndAlso unLock
                    F8lock = F8lock AndAlso lock
                    F9lock = F9lock AndAlso lock
                    F10lock = F10lock AndAlso lock
                    F11lock = F11lock AndAlso unLock

                Case LMG050C.STATE_INSATU_ZUMI          '印刷済
                    '2011/08/04 菱刈 検証一覧結果 No2 スタート
                    F2lock = F2lock AndAlso unLock
                    '2011/08/04 菱刈 検証一覧結果 No2 エンド
                    F4lock = F4lock AndAlso lock
                    F5lock = F5lock AndAlso lock
                    F6lock = F6lock AndAlso lock
                    F7lock = F7lock AndAlso unLock
                    F8lock = F8lock AndAlso unLock
                    F9lock = F9lock AndAlso lock
                    F10lock = F10lock AndAlso lock
                    F11lock = F11lock AndAlso unLock

                Case LMG050C.STATE_KEIRI_TORIKOMI_ZUMI  '経理取込済
                    F2lock = F2lock AndAlso lock
                    F4lock = F4lock AndAlso lock
                    F5lock = F5lock AndAlso lock
                    F6lock = F6lock AndAlso lock
                    F7lock = F7lock AndAlso lock
                    F8lock = F8lock AndAlso lock
                    F9lock = F9lock AndAlso unLock
                    F10lock = F10lock AndAlso lock
                    F11lock = F11lock AndAlso lock

                Case LMG050C.STATE_KEIRI_TAISHO_GAI     '経理取込対象外
                    'UPD START 2023/05/02 035323【LMS】経理取込み対象外後の鑑編集
                    'F2lock = F2lock AndAlso lock   
                    F2lock = F2lock AndAlso unLock
                    'UPD END   2023/05/02 035323【LMS】経理取込み対象外後の鑑編集
                    F4lock = F4lock AndAlso lock
                    F5lock = F5lock AndAlso lock
                    F6lock = F6lock AndAlso lock

                    If drABP.Length > 0 Then
                        '外部倉庫用ABP対策
                        'この営業所では(LMG040の)請求データ出力処理により経理取込対象外となるため、この進捗区分でも初期化は可能とする
                        F7lock = F7lock AndAlso unLock
                    Else
                        F7lock = F7lock AndAlso lock
                    End If

                    F8lock = F8lock AndAlso lock
                    F9lock = F9lock AndAlso lock
                    F10lock = F10lock AndAlso lock
                    F11lock = F11lock AndAlso lock
            End Select

            'ファンクションキーのロック制御
            .F2ButtonEnabled = F2lock
            .F3ButtonEnabled = F3lock       'ADD 2018/08/21 依頼番号 : 002136 
            .F4ButtonEnabled = F4lock
            .F5ButtonEnabled = F5lock
            .F6ButtonEnabled = F6lock
            .F7ButtonEnabled = F7lock
            .F8ButtonEnabled = F8lock
            .F9ButtonEnabled = F9lock
            .F10ButtonEnabled = F10lock
            .F11ButtonEnabled = F11lock

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

            .cmbBr.TabIndex = LMG050C.CtlTabIndex.CMB_BR
            .cmbSeiqtShubetu.TabIndex = LMG050C.CtlTabIndex.CMB_SEIQT_SHUBETSU
            .chkAkaden.TabIndex = LMG050C.CtlTabIndex.CHK_AKADEN
            .lblSeikyuNo.TabIndex = LMG050C.CtlTabIndex.LBL_SEIQT_NO
            .cmbStateKbn.TabIndex = LMG050C.CtlTabIndex.CMB_STATE_KBN
            .lblSapNo.TabIndex = LMG050C.CtlTabIndex.LBL_SAP_NO
            .lblCreateNm.TabIndex = LMG050C.CtlTabIndex.LBL_CREATE_USER
            .pnlSeikyu.TabIndex = LMG050C.CtlTabIndex.PNL_SEIQT
            .txtSeikyuCd.TabIndex = LMG050C.CtlTabIndex.TXT_SEIQT_CD
            .lblSeikyuNm.TabIndex = LMG050C.CtlTabIndex.LBL_SEIQT_NM
            .cmbSeiqCurrCd.TabIndex = LMG050C.CtlTabIndex.CMB_SEIQ_CURR_CD
            .txtSeikyuTantoNm.TabIndex = LMG050C.CtlTabIndex.TXT_SEIQT_TANTO_NM
            .imdInvDate.TabIndex = LMG050C.CtlTabIndex.IMD_INV_DATE
            .lblSikyuMeigi.TabIndex = LMG050C.CtlTabIndex.LBL_SEIQT_MEIGI
            .txtRemark.TabIndex = LMG050C.CtlTabIndex.TXT_BIKO
            '2015/04/10 要望番号:2286 対応
            .numUnsoWT.TabIndex = LMG050C.CtlTabIndex.UNSO_WT
            '2015/04/10 要望番号:2286 対応
            .chkHokan.TabIndex = LMG050C.CtlTabIndex.CHK_HOKANRYO
            .chkNiyaku.TabIndex = LMG050C.CtlTabIndex.CHK_NIYAKURYO
            .chkUnchin.TabIndex = LMG050C.CtlTabIndex.CHK_UNCHIN
            .chkSagyou.TabIndex = LMG050C.CtlTabIndex.CHK_SAGYORYO
            .chkYokomochi.TabIndex = LMG050C.CtlTabIndex.CHK_YOKOMOCHI
            .chkDepotHokan.TabIndex = LMG050C.CtlTabIndex.CHK_DEPOT_HOKAN
            .chkDepotLift.TabIndex = LMG050C.CtlTabIndex.CHK_DEPOT_LIFT
            .chkContainerUnso.TabIndex = LMG050C.CtlTabIndex.CHK_SKYU_GROUP_CONTAINER_UNSO
            .chkTemplate.TabIndex = LMG050C.CtlTabIndex.CHK_TEMPLATE

            .cmbCurrencyConversion1.TabIndex = LMG050C.CtlTabIndex.CMB_MOTO_CURR
            .numExRate.TabIndex = LMG050C.CtlTabIndex.NUM_EX_RATE
            .cmbCurrencyConversion2.TabIndex = LMG050C.CtlTabIndex.CMB_SAKI_CURR

            .btnSapOut.TabIndex = LMG050C.CtlTabIndex.BTN_SAP_OUT
            .btnSapCancel.TabIndex = LMG050C.CtlTabIndex.BTN_SAP_CANCEL
            .pnlSeikyuK.TabIndex = LMG050C.CtlTabIndex.PNL_PRT_JUN
            .chkMainAri.TabIndex = LMG050C.CtlTabIndex.CHK_PRT_MAIN
            .chkSubAri.TabIndex = LMG050C.CtlTabIndex.CHK_PRT_SUB
            .chkHikaeAri.TabIndex = LMG050C.CtlTabIndex.CHK_PRT_KEIRI_HIKAE
            .chkKeiHikaeAri.TabIndex = LMG050C.CtlTabIndex.CHK_PRT_HIKAE
            .cmbPrint.TabIndex = LMG050C.CtlTabIndex.CMB_PRINT
            .btnPrint.TabIndex = LMG050C.CtlTabIndex.BTN_PRINT
            .numCalAllK.TabIndex = LMG050C.CtlTabIndex.NUM_CAL_ALL_K
            .numCalAllM.TabIndex = LMG050C.CtlTabIndex.NUM_CAL_ALL_M
            .numCalAllH.TabIndex = LMG050C.CtlTabIndex.NUM_CAL_ALL_H
            .numCalAllU.TabIndex = LMG050C.CtlTabIndex.NUM_CAL_ALL_U
            .numNebikiRateK.TabIndex = LMG050C.CtlTabIndex.NUM_NEBIKI_RATE_K
            .numNebikiRateM.TabIndex = LMG050C.CtlTabIndex.NUM_NEBIKI_RATE_M
            .numRateNebikigakuK.TabIndex = LMG050C.CtlTabIndex.NUM_RATE_NEBIKI_GAKU_K
            .numRateNebikigakuM.TabIndex = LMG050C.CtlTabIndex.NUM_RATE_NEBIKI_GAKU_M
            .numNebikiGakuK.TabIndex = LMG050C.CtlTabIndex.NUM_NEBIKI_GAKU_K
            .numNebikiGakuM.TabIndex = LMG050C.CtlTabIndex.NUM_NEBIKI_GAKU_M
            .numZeigakuK.TabIndex = LMG050C.CtlTabIndex.NUM_ZEI_GAKU_K
            .numZeigakuU.TabIndex = LMG050C.CtlTabIndex.NUM_ZEI_GAKU_U
            .numZeiHasuK.TabIndex = LMG050C.CtlTabIndex.NUM_ZEI_HASU_K
            .numSeikyuGakuK.TabIndex = LMG050C.CtlTabIndex.NUM_SEIQ_GAKU_K
            .numSeikyuGakuM.TabIndex = LMG050C.CtlTabIndex.NUM_SEIQ_GAKU_M
            .numSeikyuGakuH.TabIndex = LMG050C.CtlTabIndex.NUM_SEIQ_GAKU_H
            .numSeikyuGakuU.TabIndex = LMG050C.CtlTabIndex.NUM_SEIQ_GAKU_U
            .numSeikyuAll.TabIndex = LMG050C.CtlTabIndex.NUM_SEIQ_ALL
            .btnAdd.TabIndex = LMG050C.CtlTabIndex.BTN_GYOTUIKA
            .btnDel.TabIndex = LMG050C.CtlTabIndex.BTN_GYOSAKUJO
            .lblUnchinImpDate.TabIndex = LMG050C.CtlTabIndex.UNCHIN_IMP_DATE
            .lblSagyoImpDate.TabIndex = LMG050C.CtlTabIndex.SAGYO_IMP_DATE
            .lblYokomochiImpDate.TabIndex = LMG050C.CtlTabIndex.YOKOMOCHI_IMP_DATE
            .lblSysUpdDate.TabIndex = LMG050C.CtlTabIndex.SYS_UPD_DATE
            .lblSysUpdTime.TabIndex = LMG050C.CtlTabIndex.SYS_UPD_TIME
            .lblMaxEdaban.TabIndex = LMG050C.CtlTabIndex.MAX_EDABAN
            .lblSeikyuNoRelated.TabIndex = LMG050C.CtlTabIndex.LBL_SEIQT_NO_RELATED
            .sprSeikyuM.TabIndex = LMG050C.CtlTabIndex.SPR_MEISAI

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal dr As DataRow)

        '編集部の項目をクリア
        Call Me.ClearControl(dr)

        'コントロールの日付書式設定
        Call Me.SetDateControl()

        Dim kingaku1 As String = "###,###,###,##0"
        Dim kingaku2 As String = "#,###,###,###,##0"
        Dim kingaku3 As String = "##,###,###,##0"

        Dim kingaku As String = "#,###,###,##0"
        Dim nebikiKei As String = "###,###,##0"
        Dim nebikiRitu As String = "##0.00"
        Dim hasu As String = "#,###,##0"
        Dim unsowt As String = "###,###,##0.000" '2015/04/10 要望番号:2286 対応

        '数値項目の設定
        Me._Frm.numCalAllK.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        Me._Frm.numCalAllM.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        Me._Frm.numCalAllH.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        Me._Frm.numCalAllU.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        Me._Frm.numNebikiRateK.SetInputFields(nebikiRitu, , 3, 1, , 2, 2, , Convert.ToDecimal(100), Convert.ToDecimal(0))
        Me._Frm.numNebikiRateM.SetInputFields(nebikiRitu, , 3, 1, , 2, 2, , Convert.ToDecimal(100), Convert.ToDecimal(0))
        Me._Frm.numRateNebikigakuK.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        Me._Frm.numRateNebikigakuM.SetInputFields(kingaku1, , 12, 1, , 0, 0, , Convert.ToDecimal(999999999999), Convert.ToDecimal(-999999999999))
        '2011/08/04 菱刈 全体値引額マイナス値入力可 スタート
        Me._Frm.numNebikiGakuK.SetInputFields(nebikiKei, , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(-999999999))
        Me._Frm.numNebikiGakuM.SetInputFields(nebikiKei, , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(-999999999))
        '2011/08/04 菱刈 全体値引額マイナス値入力可 エンド
        Me._Frm.numZeigakuK.SetInputFields(kingaku3, , 11, 1, , 0, 0, , Convert.ToDecimal(99999999999), Convert.ToDecimal(-99999999999))
        Me._Frm.numZeigakuU.SetInputFields(kingaku3, , 11, 1, , 0, 0, , Convert.ToDecimal(99999999999), Convert.ToDecimal(-99999999999))
        Me._Frm.numZeiHasuK.SetInputFields(hasu, , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(-9999999))
        Me._Frm.numSeikyuGakuK.SetInputFields(kingaku2, , 13, 1, , 0, 0, , Convert.ToDecimal(9999999999999), Convert.ToDecimal(-9999999999999))
        Me._Frm.numSeikyuGakuM.SetInputFields(kingaku2, , 13, 1, , 0, 0, , Convert.ToDecimal(9999999999999), Convert.ToDecimal(-9999999999999))
        Me._Frm.numSeikyuGakuH.SetInputFields(kingaku2, , 13, 1, , 0, 0, , Convert.ToDecimal(9999999999999), Convert.ToDecimal(-9999999999999))
        Me._Frm.numSeikyuGakuU.SetInputFields(kingaku2, , 13, 1, , 0, 0, , Convert.ToDecimal(9999999999999), Convert.ToDecimal(-9999999999999))
        Me._Frm.numSeikyuAll.SetInputFields(kingaku2, , 13, 1, , 0, 0, , Convert.ToDecimal(9999999999999), Convert.ToDecimal(-99999999999999))
        '2015/04/10 要望番号:2286 対応
        Me._Frm.numUnsoWT.SetInputFields(unsowt, , 12, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
        '2015/04/10 要望番号:2286 対応

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocus()

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.EDIT
                    .pnlSeikyu.Focus()
                    .txtSeikyuCd.Focus()
                    If .txtSeikyuCd.ReadOnly = True Then
                        .txtSeikyuTantoNm.Focus()
                    End If

                Case DispMode.VIEW
                    .pnlSeikyuK.Focus()
                    .chkMainAri.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal dr As DataRow)

        With Me._Frm

            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd     '自営業所
            .cmbSeiqtShubetu.SelectedValue = dr.Item("CRT_KB").ToString()
            .chkAkaden.SetBinaryValue(LMConst.FLG.OFF)
            .lblSeikyuNo.TextValue = String.Empty
            .cmbStateKbn.SelectedValue = LMG050C.STATE_MIKAKUTEI    '未確定
            .lblSapNo.TextValue = String.Empty
            .lblCreateNm.TextValue = String.Empty
            .txtSeikyuCd.TextValue = String.Empty
            .lblSeikyuNm.TextValue = String.Empty
            .txtSeikyuTantoNm.TextValue = String.Empty
            .imdInvDate.TextValue = String.Empty
            .lblSikyuMeigi.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .chkHokan.SetBinaryValue(LMConst.FLG.ON)
            .chkNiyaku.SetBinaryValue(LMConst.FLG.ON)
            .chkUnchin.SetBinaryValue(LMConst.FLG.ON)
            .chkSagyou.SetBinaryValue(LMConst.FLG.ON)
            .chkYokomochi.SetBinaryValue(LMConst.FLG.ON)
            .chkDepotHokan.SetBinaryValue(LMConst.FLG.OFF)
            .chkDepotLift.SetBinaryValue(LMConst.FLG.OFF)
            .chkContainerUnso.SetBinaryValue(LMConst.FLG.OFF)
            .chkTemplate.SetBinaryValue(LMConst.FLG.ON)
            '2014.08.25 追加START
            .cmbCurrencyConversion1.SelectedValue = Nothing
            .numExRate.Value = 1
            .cmbCurrencyConversion2.SelectedValue = Nothing
            '2014.08.25 追加END
            .chkMainAri.SetBinaryValue(LMConst.FLG.OFF)
            .chkSubAri.SetBinaryValue(LMConst.FLG.OFF)
            .chkHikaeAri.SetBinaryValue(LMConst.FLG.OFF)
            .chkKeiHikaeAri.SetBinaryValue(LMConst.FLG.OFF)
            .cmbPrint.SelectedValue = String.Empty
            .numCalAllK.Value = 0
            .numCalAllM.Value = 0
            .numCalAllH.Value = 0
            .numCalAllU.Value = 0
            .numNebikiRateK.Value = 0
            .numNebikiRateM.Value = 0
            .numRateNebikigakuK.Value = 0
            .numRateNebikigakuM.Value = 0
            .numNebikiGakuK.Value = 0
            .numNebikiGakuM.Value = 0
            .numZeigakuK.Value = 0
            .numZeigakuU.Value = 0
            .numZeiHasuK.Value = 0
            .numSeikyuGakuK.Value = 0
            .numSeikyuGakuM.Value = 0
            .numSeikyuGakuH.Value = 0
            .numSeikyuGakuU.Value = 0
            .numSeikyuAll.Value = 0
            '2015/04/10 要望番号:2286 対応
            .numUnsoWT.Value = 0.0
            '2015/04/10 要望番号:2286 対応
            .lblUnchinImpDate.TextValue = String.Empty
            .lblSagyoImpDate.TextValue = String.Empty
            .lblYokomochiImpDate.TextValue = String.Empty
            .lblSysUpdDate.TextValue = String.Empty
            .lblSysUpdTime.TextValue = String.Empty
            .lblMaxEdaban.TextValue = "0"
            .lblSeikyuNoRelated.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コンボコントロールの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboControl(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("M_CURR_OUT").Rows.Count - 1
        Dim setDS As DataSet = New LMG050DS()
        Dim setDt As DataTable = setDS.Tables("M_CURR_OUT")
        Dim setdr As DataRow = Nothing

        '空行を1行追加
        setDt.Rows.Add(setDt.NewRow())
        setDt.Rows(0).Item("CURR_CD") = String.Empty

        'コンボ用テーブルに詰め替え
        For i As Integer = 0 To max

            setdr = setDt.NewRow()
            setdr.Item("CURR_CD") = ds.Tables("M_CURR_OUT").Rows(i).Item("CURR_CD")
            setdr.Item("KBN_CD") = ds.Tables("M_CURR_OUT").Rows(i).Item("CURR_CD")
            setDt.Rows.Add(setdr)

        Next

        With Me._Frm

            '通貨マスタのコンボを設定
            .cmbSeiqCurrCd.DataSource = setDt.Copy
            .cmbSeiqCurrCd.DisplayMember = "CURR_CD"
            .cmbSeiqCurrCd.ValueMember = "KBN_CD"
            .cmbCurrencyConversion1.DataSource = setDt.Copy
            .cmbCurrencyConversion1.DisplayMember = "CURR_CD"
            .cmbCurrencyConversion1.ValueMember = "KBN_CD"
            .cmbCurrencyConversion2.DataSource = setDt.Copy
            .cmbCurrencyConversion2.DisplayMember = "CURR_CD"
            .cmbCurrencyConversion2.ValueMember = "KBN_CD"

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal keiriModoshiFlg As Boolean, Optional ByVal eventShubetu As LMG050C.EventShubetsu = Nothing)

        Call Me.SetFunctionKey(keiriModoshiFlg)
        Call Me.SetControlsStatus(eventShubetu)

    End Sub

    ''' <summary>
    ''' 検索結果をヘッダ部に反映
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHed(ByVal ds As DataSet)

        Dim dtHed As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dtDtl As DataTable = ds.Tables(LMG050C.TABLE_NM_DTL)
        Dim dr As DataRow = dtHed.Rows(0)

        With Me._Frm

            .cmbBr.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbSeiqtShubetu.SelectedValue = dr.Item("CRT_KB").ToString()
            If dr.Item("RB_FLG").ToString().Equals(LMG050C.AKA_DEN) Then
                .chkAkaden.SetBinaryValue(LMConst.FLG.ON)
            End If
            .lblSeikyuNo.TextValue = dr.Item("SKYU_NO").ToString()
            .cmbStateKbn.SelectedValue = dr.Item("STATE_KB").ToString()
            .lblSapNo.TextValue = dr.Item("SAP_NO").ToString()
            .lblCreateNm.TextValue = dr.Item("SYS_ENT_USER_NM").ToString()
            '請求先情報フレーム
            .txtSeikyuCd.TextValue = dr.Item("SEIQTO_CD").ToString()
            .lblSeikyuNm.TextValue = dr.Item("SEIQTO_NM").ToString()
            .txtSeikyuTantoNm.TextValue = dr.Item("SEIQTO_PIC").ToString()
            '請求情報
            .imdInvDate.TextValue = dr.Item("SKYU_DATE").ToString()
            .lblSikyuMeigi.TextValue = dr.Item("MEIGI_NM").ToString()
            .txtRemark.TextValue = dr.Item("REMARK").ToString()
            '2015/04/10 要望番号:2286 対応
            .numUnsoWT.Value = Convert.ToDecimal(dr.Item("UNSO_WT").ToString())
            '2015/04/10 要望番号:2286 対応
            If dr.Item("STORAGE_KB").ToString().Equals(LMG050C.MITORIKOMI) Then
                .chkHokan.SetBinaryValue(LMConst.FLG.ON)
            Else
                .chkHokan.SetBinaryValue(LMConst.FLG.OFF)
            End If
            If dr.Item("HANDLING_KB").ToString().Equals(LMG050C.MITORIKOMI) Then
                .chkNiyaku.SetBinaryValue(LMConst.FLG.ON)
            Else
                .chkNiyaku.SetBinaryValue(LMConst.FLG.OFF)
            End If
            If dr.Item("UNCHIN_KB").ToString().Equals(LMG050C.MITORIKOMI) Then
                .chkUnchin.SetBinaryValue(LMConst.FLG.ON)
            Else
                .chkUnchin.SetBinaryValue(LMConst.FLG.OFF)
            End If
            If dr.Item("SAGYO_KB").ToString().Equals(LMG050C.MITORIKOMI) Then
                .chkSagyou.SetBinaryValue(LMConst.FLG.ON)
            Else
                .chkSagyou.SetBinaryValue(LMConst.FLG.OFF)
            End If
            If dr.Item("YOKOMOCHI_KB").ToString().Equals(LMG050C.MITORIKOMI) Then
                .chkYokomochi.SetBinaryValue(LMConst.FLG.ON)
            Else
                .chkYokomochi.SetBinaryValue(LMConst.FLG.OFF)
            End If
            .chkDepotHokan.SetBinaryValue(LMConst.FLG.OFF)
            .chkDepotLift.SetBinaryValue(LMConst.FLG.OFF)
            .chkContainerUnso.SetBinaryValue(LMConst.FLG.OFF)
            Dim max As Integer = dtDtl.Rows.Count - 1
            Dim chkFlg As String = LMConst.FLG.ON
            For i As Integer = 0 To max
                If dtDtl.Rows(i).Item("TEMPLATE_IMP_FLG").Equals(LMG050C.TORIKOMI_ZUMI) Then
                    chkFlg = LMConst.FLG.OFF
                    Exit For
                End If
            Next
            .chkTemplate.SetBinaryValue(chkFlg)
            '2014.08.21 追加START 多通貨対応
            .cmbSeiqCurrCd.SelectedValue = dr.Item("INV_CURR_CD").ToString()
            .cmbCurrencyConversion1.SelectedValue = dr.Item("EX_MOTO_CURR_CD").ToString()
            .numExRate.Value = Convert.ToDecimal(dr.Item("EX_RATE"))
            .cmbCurrencyConversion2.SelectedValue = dr.Item("EX_SAKI_CURR_CD").ToString()

            Dim seiqCurr As String = String.Empty
            Dim unLockFlg As Boolean = False
            Dim sprItemCurr As String = String.Empty
            seiqCurr = dr.Item("INV_CURR_CD").ToString()
            For i As Integer = 0 To dtDtl.Rows.Count - 1

                sprItemCurr = dtDtl.Rows(i).Item("ITEM_CURR_CD").ToString()

                If seiqCurr.Equals(sprItemCurr) = False Then

                    unLockFlg = True
                    Exit For

                End If

            Next

            If unLockFlg = False Then
                .cmbCurrencyConversion1.SelectedValue = seiqCurr
                .cmbCurrencyConversion2.SelectedValue = seiqCurr
                .numExRate.Value = 1
            End If
            '2014.08.21 追加END 多通貨対応

            '印刷情報
            Me._ControlG.SetCheckBox(.chkMainAri, dr.Item("DOC_SEI_YN").ToString())
            Me._ControlG.SetCheckBox(.chkSubAri, dr.Item("DOC_HUKU_YN").ToString())
            Me._ControlG.SetCheckBox(.chkHikaeAri, dr.Item("DOC_HIKAE_YN").ToString())
            Me._ControlG.SetCheckBox(.chkKeiHikaeAri, dr.Item("DOC_KEIRI_YN").ToString())
            'START YANAI 運送・運行・請求メモNo.55
            Dim Shubetu As String = Me._Frm.cmbStateKbn.SelectedValue.ToString
            '種別が未確定の場合
            If Shubetu.Equals(LMG050C.STATE_MIKAKUTEI) = True Then
                .chkMainAri.SetBinaryValue(LMConst.FLG.OFF)
                .chkSubAri.SetBinaryValue(LMConst.FLG.OFF)
                .chkKeiHikaeAri.SetBinaryValue(LMConst.FLG.OFF)
            End If
            'END YANAI 運送・運行・請求メモNo.55

            If .cmbSeiqtShubetu.SelectedValue.Equals(LMGControlC.CRT_TEGAKI) Then
                .cmbPrint.SelectedValue = LMG050C.PRINT_SEIKYU_SHO
            End If
            '集計部
            .numNebikiRateK.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("NEBIKI_RT1")), 2).ToString()
            .numNebikiRateM.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("NEBIKI_RT2")), 2).ToString()
            .numNebikiGakuK.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("NEBIKI_GK1")), 0).ToString()
            .numNebikiGakuM.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("NEBIKI_GK2")), 0).ToString()
            .numZeigakuK.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("TAX_GK1")), 0).ToString()
            .numZeiHasuK.Value = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("TAX_HASU_GK1")), 0).ToString()

            '隠し項目
            .lblSysUpdDate.TextValue = dr.Item("SYS_UPD_DATE").ToString()
            .lblSysUpdTime.TextValue = dr.Item("SYS_UPD_TIME").ToString()
            .lblUnchinImpDate.TextValue = dr.Item("UNCHIN_IMP_FROM_DATE").ToString()
            .lblSagyoImpDate.TextValue = dr.Item("SAGYO_IMP_FROM_DATE").ToString()
            .lblYokomochiImpDate.TextValue = dr.Item("YOKOMOCHI_IMP_FROM_DATE").ToString()
            .lblMaxEdaban.TextValue = dr.Item("MAX_SKYU_SUB_NO").ToString()
            .lblSeikyuNoRelated.TextValue = dr.Item("SKYU_NO_RELATED").ToString()
        End With

    End Sub

    '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add start
    ''' <summary>
    ''' 画面項目「経理控」設定
    ''' </summary>
    ''' <remarks>
    ''' 鑑編集画面の一覧で、項目「作成種別」が全て「自動」の場合、
    ''' 画面項目「経理控」のチェックを外す
    ''' </remarks>
    Friend Sub SetKeiriHikae()
        With Me._Frm
            Dim Shubetu As String = .cmbStateKbn.SelectedValue.ToString
            Dim spr As LMSpread = .sprSeikyuM
            Dim isAuto As Boolean = False

            Select Case Me._Frm.lblSituation.DispMode
                '画面が参照モードの場合
                Case DispMode.VIEW
                    If Shubetu.Equals(LMG050C.STATE_MIKAKUTEI).Equals(False) Then
                        '未確定ではない場合
                        With spr
                            Dim max As Integer = spr.Sheets(0).RowCount - 1
                            If max <> -1 Then
                                '一覧が存在する場合
                                '経理控チェックなしフラグにTrueを設定する。
                                isAuto = True
                                For rowCounter As Integer = 0 To max
                                    If .ActiveSheet.Cells(rowCounter, sprSeikyuMDef.CREATE_SYUBETU_NM.ColNo).Value.ToString <> LMG050C.Auto Then
                                        '一覧の項目「作成種別」に「自動」以外のデータが設定されていた場合
                                        '経理控チェックなしフラグにFalseを設定し、ループを抜ける
                                        isAuto = False
                                        Exit For
                                    End If
                                Next
                            End If
                        End With
                    End If
                Case Else
                    '画面モードが参照以外の場合、経理控チェックなしフラグはFalseのまま
            End Select

            If isAuto.Equals(True) Then
                '経理控チェックなしフラグがTrueの場合、画面項目「経理控」のチェックを外す

                Me._ControlG.SetCheckBox(.chkKeiHikaeAri, LMGControlC.YN_FLG_NO)
            End If

        End With
    End Sub
    '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add end

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            .imdInvDate.Format = Fields.DateFieldsBuilder.BuildFields("yyyyMMdd")
            .imdInvDate.DisplayFormat = Fields.DateDisplayFieldsBuilder.BuildFields("yyyy/MM/dd")

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus(ByVal eventShubetu As LMG050C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False
        Dim unLockFlg As Boolean = False

        Dim seiqCurr As String = String.Empty
        Dim sprItemCurr As String = String.Empty

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.EDIT

                    Dim Shubetu As String = Me._Frm.cmbStateKbn.SelectedValue.ToString

                    .cmbSeiqCurrCd.ReadOnly = lock
                    .cmbCurrencyConversion1.ReadOnly = lock
                    .cmbCurrencyConversion2.ReadOnly = lock

                    '種別が確定、印刷済みの場合
                    If Shubetu.Equals(LMG050C.STATE_KAKUTEI) OrElse
                    Shubetu.Equals(LMG050C.STATE_INSATU_ZUMI) = True Then

                        '担当者名のロック解除
                        Call Me._ControlG.LockText(.txtSeikyuTantoNm, unLock)

                        '備考のロック解除
                        Call Me._ControlG.LockText(.txtRemark, unLock)

                        '2015/04/10 要望番号:2286 対応
                        Call Me._ControlG.LockNumber(.numUnsoWT, unLock)
                        '2015/04/10 要望番号:2286 対応

                    Else

                        '種別が確定、印刷済みでない場合

                        '編集モード時常に使用可能項目
                        Call Me._ControlG.LockText(.txtSeikyuTantoNm, unLock)
                        Call Me._ControlG.LockText(.txtRemark, unLock)
                        Call Me._ControlG.LockNumber(.numNebikiRateK, unLock)
                        Call Me._ControlG.LockNumber(.numNebikiRateM, unLock)
                        Call Me._ControlG.LockNumber(.numNebikiGakuK, unLock)
                        Call Me._ControlG.LockNumber(.numNebikiGakuM, unLock)
                        Call Me._ControlG.LockNumber(.numZeiHasuK, unLock)
                        '2015/04/10 要望番号:2286 対応
                        Call Me._ControlG.LockNumber(.numUnsoWT, unLock)
                        '2015/04/10 要望番号:2286 対応


                        .cmbSeiqCurrCd.ReadOnly = unLock

                        '2014.08.21 追加START 多通貨対応
                        seiqCurr = .cmbSeiqCurrCd.SelectedValue.ToString()

                        For i As Integer = 0 To .sprSeikyuM.ActiveSheet.Rows.Count - 1

                            sprItemCurr = Me._ControlG.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, sprSeikyuMDef.ITEM_CURR_CD.ColNo))

                            If String.IsNullOrEmpty(seiqCurr) = True OrElse
                               String.IsNullOrEmpty(sprItemCurr) = True Then
                                unLockFlg = True
                                Exit For
                            End If

                            If seiqCurr.Equals(sprItemCurr) = False Then

                                unLockFlg = True
                                Exit For

                            End If

                        Next

                        If unLockFlg = False Then
                            .cmbCurrencyConversion1.ReadOnly = lock
                            .numExRate.ReadOnly = lock
                            .cmbCurrencyConversion2.ReadOnly = lock
                        Else
                            .cmbCurrencyConversion1.ReadOnly = unLock
                            .numExRate.ReadOnly = unLock
                            .cmbCurrencyConversion2.ReadOnly = unLock
                        End If

                        If .sprSeikyuM.ActiveSheet.Rows.Count = 0 Then

                            .cmbCurrencyConversion1.ReadOnly = unLock
                            .numExRate.ReadOnly = unLock
                            .cmbCurrencyConversion2.ReadOnly = unLock

                        End If

                        '2014.08.21 追加END 多通貨対応

                        '進捗区分が"00"(未確定)時のみ使用可能
                        If Shubetu.Equals(LMG050C.STATE_MIKAKUTEI) Then
                            Call Me._ControlG.LockButton(.btnAdd, unLock)
                            Call Me._ControlG.LockButton(.btnDel, unLock)
                        End If

                        '請求書番号が空の場合のみ使用可能項目
                        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNo.TextValue) Then
                            If eventShubetu.Equals(LMG050C.EventShubetsu.IMPORT) = False Then
                                Call Me._ControlG.LockText(.txtSeikyuCd, unLock)
                                Call Me._ControlG.LockDate(.imdInvDate, unLock)
                            End If
                        End If

                        '鑑作成区分 = "00"(自動取込請求書)の場合、使用可能項目
                        If Shubetu.Equals(LMGControlC.CRT_TORIKOMI) Then
                            Call Me._ControlG.LockCheckBox(.chkHokan, unLock)
                            Call Me._ControlG.LockCheckBox(.chkNiyaku, unLock)
                            Call Me._ControlG.LockCheckBox(.chkUnchin, unLock)
                            Call Me._ControlG.LockCheckBox(.chkSagyou, unLock)
                            Call Me._ControlG.LockCheckBox(.chkYokomochi, unLock)
                            Call Me._ControlG.LockCheckBox(.chkDepotHokan, unLock)
                            Call Me._ControlG.LockCheckBox(.chkDepotLift, unLock)
                            Call Me._ControlG.LockCheckBox(.chkContainerUnso, unLock)
                            Call Me._ControlG.LockCheckBox(.chkTemplate, unLock)
                        End If

                    End If

                    Exit Sub

                Case DispMode.VIEW

                    'SAP出力、SAP取消 ロック解除
                    Call Me._ControlG.LockButton(.btnSapOut, unLock)
                    Call Me._ControlG.LockButton(.btnSapCancel, unLock)
                    '印刷項目解除
                    Call Me._ControlG.LockButton(.btnPrint, unLock)
                    Call Me._ControlG.SetLockControl(.pnlSeikyuK, unLock)
                    'START YANAI 運送・運行・請求メモNo.55
                    Dim Shubetu As String = Me._Frm.cmbStateKbn.SelectedValue.ToString
                    '種別が未確定の場合
                    If Shubetu.Equals(LMG050C.STATE_MIKAKUTEI) = True Then
                        Call Me._ControlG.SetLockControl(.chkMainAri, lock)
                        Call Me._ControlG.SetLockControl(.chkSubAri, lock)
                        Call Me._ControlG.SetLockControl(.chkKeiHikaeAri, lock)
                    End If
                    'END YANAI 運送・運行・請求メモNo.55
                    '鑑作成区分 = "00"(自動取込請求書)の場合、使用可能項目
                    If Me._Frm.cmbSeiqtShubetu.SelectedValue.Equals(LMGControlC.CRT_TORIKOMI) Then
                        Call Me._ControlG.LockComb(.cmbPrint, unLock)
                    End If
#If True Then       'ADD 2018/08/21 依頼番号 : 002136 削除データ表示時対応
                    If Me._Frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
                        Call Me._ControlG.LockButton(.btnSapOut, lock)
                        Call Me._ControlG.LockButton(.btnSapCancel, lock)
                        Call Me._ControlG.SetLockControl(.pnlSeikyuK, lock)
                        Call Me._ControlG.LockComb(.cmbPrint, lock)
                        Call Me._ControlG.LockButton(.btnPrint, lock)
                    End If
#End If
            End Select

        End With

    End Sub

    ''' <summary>
    ''' TSMCか否かによる 取込項目の表示制御と初期値制御
    ''' </summary>
    ''' <param name="isTsmc"></param>
    Friend Sub SetControlsStatus2(ByVal isTsmc As Boolean, Optional ByVal isInit As Boolean = False)

        If isTsmc Then
            ' TSMC への切り替え(またはその状態の維持)

            ' 非TSMC請求先 取込項目 直近退避値 設定
            If isInit Then
                ' 初期表示時点で請求先が TSMC の場合、SetHed() にて DBよりの画面設定値を退避しているか否かに関わらず、
                ' ClearControl() での設定値と同値を退避する。
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkHokan.Name) = LMConst.FLG.ON
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkNiyaku.Name) = LMConst.FLG.ON
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkUnchin.Name) = LMConst.FLG.ON
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkSagyou.Name) = LMConst.FLG.ON
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkYokomochi.Name) = LMConst.FLG.ON
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotHokan.Name) = LMConst.FLG.OFF
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotLift.Name) = LMConst.FLG.OFF
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkContainerUnso.Name) = LMConst.FLG.OFF
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkTemplate.Name) = LMConst.FLG.ON
            Else
                If Me._Frm.lblTitleTorikomi.Visible Then
                    ' 初期表示以外で取込項目が非表示の場合(非TSMC請求先 (から今回 TSMC に変更) ) の場合、画面の値を退避する。
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkHokan.Name) = Me._Frm.chkHokan.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkNiyaku.Name) = Me._Frm.chkNiyaku.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkUnchin.Name) = Me._Frm.chkUnchin.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkSagyou.Name) = Me._Frm.chkSagyou.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkYokomochi.Name) = Me._Frm.chkYokomochi.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotHokan.Name) = Me._Frm.chkDepotHokan.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotLift.Name) = Me._Frm.chkDepotLift.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkContainerUnso.Name) = Me._Frm.chkContainerUnso.GetBinaryValue()
                    Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkTemplate.Name) = Me._Frm.chkTemplate.GetBinaryValue()
                End If
            End If

            If isInit OrElse Me._Frm.lblTitleTorikomi.Visible Then
                ' 取込項目を表示している場合の非表示化とチェックボックス値への固定値設定(TSMCは取込項目固定)
                Me._Frm.lblTitleTorikomi.Visible = False
                Me._Frm.chkHokan.Visible = False
                Me._Frm.chkNiyaku.Visible = False
                Me._Frm.chkUnchin.Visible = False
                Me._Frm.chkSagyou.Visible = False
                Me._Frm.chkYokomochi.Visible = False
                Me._Frm.chkDepotHokan.Visible = False
                Me._Frm.chkDepotLift.Visible = False
                Me._Frm.chkContainerUnso.Visible = False
                Me._Frm.chkTemplate.Visible = False
                Me._Frm.chkHokan.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkNiyaku.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkUnchin.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkSagyou.SetBinaryValue(LMConst.FLG.OFF)
                Me._Frm.chkYokomochi.SetBinaryValue(LMConst.FLG.OFF)
                Me._Frm.chkDepotHokan.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkDepotLift.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkContainerUnso.SetBinaryValue(LMConst.FLG.ON)
                Me._Frm.chkTemplate.SetBinaryValue(LMConst.FLG.OFF)
            End If
        Else
            ' 非TSMC への切り替え(またはその状態の維持)

            ' 非TSMC請求先 取込項目 直近退避値 設定
            If isInit Then
                ' 初期表示時点で請求先が 非TSMC の場合、
                ' ClearControl() での設定値、または SetHed() にて DBよりの画面設定値を退避する。
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkHokan.Name) = Me._Frm.chkHokan.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkNiyaku.Name) = Me._Frm.chkNiyaku.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkUnchin.Name) = Me._Frm.chkUnchin.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkSagyou.Name) = Me._Frm.chkSagyou.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkYokomochi.Name) = Me._Frm.chkYokomochi.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotHokan.Name) = Me._Frm.chkDepotHokan.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotLift.Name) = Me._Frm.chkDepotLift.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkContainerUnso.Name) = Me._Frm.chkContainerUnso.GetBinaryValue()
                Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkTemplate.Name) = Me._Frm.chkTemplate.GetBinaryValue()
            End If

            If Not Me._Frm.lblTitleTorikomi.Visible Then
                ' 取込項目を表示していない場合の表示とチェックボックス値の復元
                Me._Frm.lblTitleTorikomi.Visible = True
                Me._Frm.chkHokan.Visible = True
                Me._Frm.chkNiyaku.Visible = True
                Me._Frm.chkUnchin.Visible = True
                Me._Frm.chkSagyou.Visible = True
                Me._Frm.chkYokomochi.Visible = True
                Me._Frm.chkDepotHokan.Visible = False
                Me._Frm.chkDepotLift.Visible = False
                Me._Frm.chkContainerUnso.Visible = False
                Me._Frm.chkTemplate.Visible = True
                ' 非表示になる直前の退避値よりのチェックボックス値復元
                Me._Frm.chkHokan.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkHokan.Name))
                Me._Frm.chkNiyaku.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkNiyaku.Name))
                Me._Frm.chkUnchin.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkUnchin.Name))
                Me._Frm.chkSagyou.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkSagyou.Name))
                Me._Frm.chkYokomochi.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkYokomochi.Name))
                Me._Frm.chkDepotHokan.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotHokan.Name))
                Me._Frm.chkDepotLift.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkDepotLift.Name))
                Me._Frm.chkContainerUnso.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkContainerUnso.Name))
                Me._Frm.chkTemplate.SetBinaryValue(Me._LastTorikomiChkValueNotTsmcDict(Me._Frm.chkTemplate.Name))
            End If
        End If

    End Sub

    ''' <summary>
    ''' 計算結果を集計部に設定する
    ''' </summary>
    ''' <param name="tax">税区分</param>
    ''' <param name="total">計算総額</param>
    ''' <param name="rateNebiki">率値引額</param>
    ''' <param name="taxGk">税額</param>
    ''' <param name="skyuGk">請求額</param>
    ''' <remarks></remarks>
    Friend Sub SetHedCalcData(ByVal tax As String _
                              , ByVal total As String _
                              , ByVal rateNebiki As String _
                              , ByVal taxGk As String _
                              , ByVal skyuGk As String)

        With Me._Frm

            Select Case tax
                Case LMGControlC.TAX_KAZEI
                    .numCalAllK.Value = total
                    .numRateNebikigakuK.Value = rateNebiki
                    .numZeigakuK.Value = taxGk
                    .numSeikyuGakuK.Value = skyuGk

                Case LMGControlC.TAX_MENZEI
                    .numCalAllM.Value = total
                    .numRateNebikigakuM.Value = rateNebiki
                    .numSeikyuGakuM.Value = skyuGk

                Case LMGControlC.TAX_HIKAZEI
                    .numCalAllH.Value = total
                    .numSeikyuGakuH.Value = skyuGk

                Case LMGControlC.TAX_UCHIZEI
                    .numCalAllU.Value = total
                    .numZeigakuU.Value = taxGk
                    .numSeikyuGakuU.Value = skyuGk

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 取込開始日の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTorikomiStartDay(ByVal dt As DataTable)

        With Me._Frm

            '経理戻しにより作成されたデータ(新黒)以外の場合、設定を行う
            If String.IsNullOrEmpty(.lblSeikyuNoRelated.TextValue) Then

                If .chkUnchin.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                    .lblUnchinImpDate.TextValue = dt.Rows(0).Item("SKYU_DATE_FROM").ToString()
                End If
                If .chkSagyou.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                    .lblSagyoImpDate.TextValue = dt.Rows(0).Item("SKYU_DATE_FROM").ToString()
                End If
                If .chkYokomochi.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                    .lblYokomochiImpDate.TextValue = dt.Rows(0).Item("SKYU_DATE_FROM").ToString()
                End If

            End If

        End With

    End Sub

    '要望番号:1935 yamanaka 2013.03.08 Start
    ''' <summary>
    ''' コントロール値の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDataControl(ByVal dr As DataRow, ByVal sysData As String)

        With Me._Frm

            Dim closeData As String = String.Empty

            '請求先マスタ取得(キャッシュ)
            Dim seiqDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue.ToString() _
                                                                                                              , "' AND SEIQTO_CD = '", dr.Item("SEIQTO_CD").ToString(), "'"))

            '請求先コード
            .txtSeikyuCd.TextValue = dr.Item("SEIQTO_CD").ToString()

            If seiqDr.Length > 0 Then

                '請求先名
                .lblSeikyuNm.TextValue = seiqDr(0).Item("SEIQTO_NM").ToString()

                '担当者名
                .txtSeikyuTantoNm.TextValue = seiqDr(0).Item("OYA_PIC").ToString()

                '請求日
                If String.IsNullOrEmpty(dr.Item("SKYU_DATE").ToString()) = True Then
                    dr.Item("SKYU_DATE") = sysData
                End If

                Select Case seiqDr(0).Item("CLOSE_KB").ToString()
                    Case LMG050C.CLOSE_10
                        closeData = "10"
                    Case LMG050C.CLOSE_20
                        closeData = "20"
                    Case LMG050C.CLOSE_25
                        closeData = "25"
                    Case LMG050C.CLOSE_MATU
                        closeData = Mid(Convert.ToDateTime(DateFormatUtility.EditSlash(String.Concat(Mid(dr.Item("SKYU_DATE").ToString(), 1, 6), "01"))).AddMonths(1).AddDays(-1).ToString(), 9, 2)
                End Select

                .imdInvDate.TextValue = String.Concat(Mid(dr.Item("SKYU_DATE").ToString(), 1, 6), closeData)

                '請求先口座
                .lblSikyuMeigi.TextValue = GetKouzaMeigiNm(dr.Item("SKYU_DATE").ToString(), seiqDr(0).Item("KOUZA_KB").ToString())

                '請求書種別(正)
                If seiqDr(0).Item("DOC_SEI_YN").ToString.Equals("00") = True Then
                    .chkMainAri.SetBinaryValue(LMConst.FLG.OFF)
                Else
                    .chkMainAri.SetBinaryValue(LMConst.FLG.ON)
                End If

                '請求書種別(副)
                If seiqDr(0).Item("DOC_HUKU_YN").ToString.Equals("00") = True Then
                    .chkSubAri.SetBinaryValue(LMConst.FLG.OFF)
                Else
                    .chkSubAri.SetBinaryValue(LMConst.FLG.ON)
                End If

                '請求書種別(控)
                If seiqDr(0).Item("DOC_HIKAE_YN").ToString.Equals("00") = True Then
                    .chkKeiHikaeAri.SetBinaryValue(LMConst.FLG.OFF)
                Else
                    .chkKeiHikaeAri.SetBinaryValue(LMConst.FLG.ON)
                End If

                '請求書種別(経理)
                If seiqDr(0).Item("DOC_KEIRI_YN").ToString.Equals("00") = True Then
                    .chkHikaeAri.SetBinaryValue(LMConst.FLG.OFF)
                Else
                    .chkHikaeAri.SetBinaryValue(LMConst.FLG.ON)
                End If

                '全体値引率(課税分)
                .numNebikiRateK.Value = Convert.ToDecimal(seiqDr(0).Item("TOTAL_NR").ToString())

                '全体値引額(課税分)
                .numNebikiGakuK.Value = Convert.ToDecimal(seiqDr(0).Item("TOTAL_NG").ToString())

            End If

        End With

    End Sub
    '要望番号:1935 yamanaka 2013.03.08 End

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 画面入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlRate()

        With Me._Frm

            Dim invCurr As String = .cmbSeiqCurrCd.SelectedValue.ToString()
            Dim sprItemCurr As String = String.Empty

            For i As Integer = 0 To .sprSeikyuM.ActiveSheet.Rows.Count - 1

                sprItemCurr = Me._ControlG.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, sprSeikyuMDef.ITEM_CURR_CD.ColNo))

                If invCurr.Equals(sprItemCurr) = False Then

                    .numExRate.ReadOnly = False

                    .numExRate.Value = 0
                    .cmbCurrencyConversion1.ReadOnly = False
                    .cmbCurrencyConversion2.ReadOnly = False

                    Exit Sub

                End If

            Next

            .numExRate.ReadOnly = True
            .numExRate.Value = 1

            .cmbCurrencyConversion1.ReadOnly = True
            .cmbCurrencyConversion2.ReadOnly = True
            .cmbCurrencyConversion1.SelectedValue = invCurr
            .cmbCurrencyConversion2.SelectedValue = invCurr

        End With

    End Sub
    '2014.08.21 追加END 多通貨対応

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprSeikyuMDef

        'スプレッド(タイトル列)の設定
        '*****表示列*****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared IN_JUN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.IN_JUN, "印順", 37, True)
        Public Shared SHUBETU_NM As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SHUBETU_NM, "種別", 80, True)
        Public Shared BUSYO As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.BUSHO, "部署", 80, True)
        Public Shared KANJOKMK_CD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KANJOKMK_CD, "勘定科目" & vbCrLf & "コード", 90, True)
        Public Shared KAZEI_NM As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KAZEI_NM, "課税区分", 70, True)
        Public Shared TCUST_BPCD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.TCUST_BPCD, "真荷主" & vbCrLf & "コード", 80, True)
        Public Shared TCUST_BPNM As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.TCUST_BPNM, "真荷主名", 150, True)
        Public Shared PRODUCT_SEG_CD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.PRODUCT_SEG_CD, "製品セグメント", 240, True)
        Public Shared ORIG_SEG_CD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.ORIG_SEG_CD, "地域セグメント(発地)", 150, True)
        Public Shared DEST_SEG_CD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.DEST_SEG_CD, "地域セグメント(着地)", 150, True)
        Public Shared KEISAN_GAKU As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KEISANGAKU, "計算額", 97, True)
        Public Shared NEBIKI_RATE As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.NEBIKIRITU, "値引率(%)", 76, True)
        Public Shared RATENEBIKI_GAKU As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.RITU_NEBIKIGAKU, "率値引額", 97, True)
        Public Shared KOTEINEBIKI_GAKU As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KOTEI_NEBIKIGAKU, "固定値引額", 88, True)
        Public Shared SEIKYU_GAKU As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SEIQT_GAKU, "請求額", 97, True)
        '2014.08.21 追加START 多通貨対応
        Public Shared ITEM_CURR_CD As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.ITEM_CURR_CD, "契約通貨", 65, True)
        '2014.08.21 追加END 多通貨対応
        Public Shared TEKIYOU As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.TEKIYOU, "摘要", 260, True)
        Public Shared EDABAN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.EDABAN, "枝番", 37, True)
        Public Shared CREATE_SYUBETU_NM As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SAKUSEI_SHUBETU_NM, "作成種別", 70, True)
        '*****隠し列*****
        Public Shared SHUBETU_KBN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SHUBETU_KBN, "種別区分", 80, False)
        Public Shared KAZEI_KBN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KAZEI_KBN, "課税区分", 70, False)
        Public Shared CREATE_SYUBETU_KBN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SAKUSEI_SHUBETU_KBN, "作成種別", 80, False)
        Public Shared GROUP_CD_KBN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.GROUP_CD_KBN, "請求グループコード区分", 80, False)
        Public Shared KEIRI_KBN As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.KEIRI_KB, "経理科目コード区分", 80, False)
        Public Shared TEMPLATE_IMP_FLG As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.TEMPLATE_FLG, "テンプレートマスタ取込フラグ", 80, False)
        Public Shared RECORD_NO As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.RECORD_NO, "レコードNo", 80, False)
        Public Shared JISYATASYA_KB As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.JISYATASYA_KB, "自社他所倉庫", 20, False)     'ADD 2016/09/06
        Public Shared SEIQKMK_CD_S As SpreadColProperty = New SpreadColProperty(LMG050C.SprColumnIndex.SEIQKMK_CD_S, "請求項目コードS", 20, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprSeikyuM

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 29

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprSeikyuMDef)
            .SetColProperty(New LMG050G.sprSeikyuMDef(), False)
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable, ByVal dsCombo As DataSet)
        'Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprSeikyuM

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '削除データを除いたデータを取得
            Dim selectDr As DataRow() = dt.Select("SYS_DEL_FLG = '0'", "RECORD_NO")
            Dim max As Integer = selectDr.Length - 1
            Dim setDS As DataSet = New LMG050DS()
            Dim setDt As DataTable = setDS.Tables(LMG050C.TABLE_NM_DTL)

            For i As Integer = 0 To max
                setDt.ImportRow(selectDr(i))
            Next

            '行数設定
            Dim lngcnt As Integer = setDt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'ロック制御用変数
            Dim lock As Boolean = True
            Dim unLock As Boolean = False

            Dim editAlways As Boolean = unLock
            Dim editMikakutei As Boolean = unLock
            Dim shubetsu As Boolean = unLock
            '2017.09.14 修正START 取込時部署選択可対応
            Dim IsLockBusyoCell As Boolean = unLock
            '2017.09.14 修正END 取込時部署選択可対応

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.VIEW
                    '編集不可
                    editAlways = lock
                    editMikakutei = lock
                Case DispMode.EDIT
                    editAlways = unLock
                    If Me._Frm.cmbStateKbn.SelectedValue.Equals(LMG050C.STATE_MIKAKUTEI) Then
                        editMikakutei = unLock
                    Else
                        editMikakutei = lock
                    End If
            End Select

            'セルに設定するスタイルの取得
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, editAlways)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numInjun As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99, editMikakutei, , , ",")
            Dim num10View As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, lock, 0, , ",")

            Dim dr As DataRow
            Dim kanjoKamokuCd As String = String.Empty

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = setDt.Rows(i)

                If dr.Item("MAKE_SYU_KB").ToString().Equals(LMGControlC.DETAIL_SAKUSEI_AUTO) _
                AndAlso dr.Item("TEMPLATE_IMP_FLG").ToString().Equals(LMGControlC.YN_FLG_NO) Then
                    shubetsu = lock
                Else
                    shubetsu = editMikakutei
                End If

                '2017.09.14 修正START 取込時部署選択可対応
                '種別区分が"00"、グループ区分が"04"（作業料かつ自動）、かつ編集モードの場合は部署項目を活性化する
                If dr.Item("MAKE_SYU_KB").ToString().Equals(LMGControlC.DETAIL_SAKUSEI_AUTO) _
                AndAlso dr.Item("GROUP_KB").ToString().Equals(LMGControlC.WORK_FEE1) Then
                    If dr.Item("NRS_BR_CD").ToString = "40" Then
                        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                            IsLockBusyoCell = unLock
                        Else
                            IsLockBusyoCell = shubetsu
                        End If
                    Else
                        IsLockBusyoCell = shubetsu
                    End If
                Else
                    IsLockBusyoCell = shubetsu
                End If
                '2017.09.14 修正END 取込時部署選択可対応


                If dr.Item("MAKE_SYU_KB").ToString().Equals(LMGControlC.DETAIL_SAKUSEI_AUTO) _
                AndAlso dr.Item("GROUP_KB").ToString().Equals(LMGControlC.STORQAGE_FEE1) _
                AndAlso dr.Item("SEIQKMK_CD").ToString().Equals(LMGControlC.SEIQKMK_GYOMUITAKU_JIMU) Then
                    shubetsu = editMikakutei
                End If

                Dim roundPos As Integer = Convert.ToInt32(dr.Item("ROUND_POS"))
                '2014.08.21 修正START 多通貨対応
                Dim num10ViewPos As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, lock, roundPos, , ",")
                '2014.08.21 修正END 多通貨対応

                'セルスタイル設定
                '*****表示列*****
                .SetCellStyle(i, sprSeikyuMDef.DEF.ColNo, def)
                .SetCellStyle(i, sprSeikyuMDef.IN_JUN.ColNo, numInjun)
                .SetCellStyle(i, sprSeikyuMDef.SHUBETU_NM.ColNo, lbl)
                '2017.09.14 修正START 取込時部署選択可対応
                .SetCellStyle(i, sprSeikyuMDef.BUSYO.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_B007, IsLockBusyoCell))
                '.SetCellStyle(i, sprSeikyuMDef.BUSYO.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_B007, shubetsu))
                '2017.09.14 修正END 取込時部署選択可対応
                .SetCellStyle(i, sprSeikyuMDef.KANJOKMK_CD.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.KAZEI_NM.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.TCUST_BPCD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 10, shubetsu))
                .SetCellStyle(i, sprSeikyuMDef.TCUST_BPNM.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.PRODUCT_SEG_CD.ColNo, LMSpreadUtility.GetComboCell(spr, New DataView(dsCombo.Tables("LMG050COMBO_SEIHINA")), "SEG_CD", "SEG_NM", shubetsu))
                .SetCellStyle(i, sprSeikyuMDef.ORIG_SEG_CD.ColNo, LMSpreadUtility.GetComboCell(spr, New DataView(dsCombo.Tables("LMG050COMBO_CHIIKI")), "SEG_CD", "SEG_NM", shubetsu))
                .SetCellStyle(i, sprSeikyuMDef.DEST_SEG_CD.ColNo, LMSpreadUtility.GetComboCell(spr, New DataView(dsCombo.Tables("LMG050COMBO_CHIIKI")), "SEG_CD", "SEG_NM", shubetsu))
                ''2014.08.21 修正START 多通貨対応
                '.SetCellStyle(i, sprSeikyuMDef.KEISAN_GAKU.ColNo, LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, shubetsu, 0, , ","))
                .SetCellStyle(i, sprSeikyuMDef.KEISAN_GAKU.ColNo, LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, shubetsu, roundPos, , ","))
                ''2014.08.21 修正END 多通貨対応
                .SetCellStyle(i, sprSeikyuMDef.NEBIKI_RATE.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 100, editMikakutei, 2, , ","))
                ''2014.08.21 修正START 多通貨対応
                '.SetCellStyle(i, sprSeikyuMDef.RATENEBIKI_GAKU.ColNo, num10View)
                .SetCellStyle(i, sprSeikyuMDef.RATENEBIKI_GAKU.ColNo, num10ViewPos)
                ''2014.08.21 修正END 多通貨対応
                '2011/08/04 菱刈 固定割引率にマイナス入力可 スタート
                ''2014.08.21 修正START 多通貨対応
                '.SetCellStyle(i, sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, editMikakutei, 0, , ","))
                .SetCellStyle(i, sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, editMikakutei, roundPos, , ","))
                ''2014.08.21 修正END 多通貨対応
                ''2014.08.21 修正START 多通貨対応
                '2011/08/04 菱刈 固定割引率にマイナス入力可 エンド
                '.SetCellStyle(i, sprSeikyuMDef.SEIKYU_GAKU.ColNo, num10View)
                .SetCellStyle(i, sprSeikyuMDef.SEIKYU_GAKU.ColNo, num10ViewPos)
                ''2014.08.21 修正END 多通貨対応
                '2014.08.21 追加START 多通貨対応
                .SetCellStyle(i, sprSeikyuMDef.ITEM_CURR_CD.ColNo, lbl)
                '2014.08.21 追加END 多通貨対応
                .SetCellStyle(i, sprSeikyuMDef.TEKIYOU.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, editAlways))
                .SetCellStyle(i, sprSeikyuMDef.EDABAN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.CREATE_SYUBETU_NM.ColNo, lbl)
                '*****隠し列*****
                .SetCellStyle(i, sprSeikyuMDef.SHUBETU_KBN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.KAZEI_KBN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.GROUP_CD_KBN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.KEIRI_KBN.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.TEMPLATE_IMP_FLG.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.RECORD_NO.ColNo, lbl)
                .SetCellStyle(i, sprSeikyuMDef.JISYATASYA_KB.ColNo, lbl)        'ADD 2016/09/06
                .SetCellStyle(i, sprSeikyuMDef.SEIQKMK_CD_S.ColNo, lbl)

                'セルに値を設定
                '*****表示列*****
                .SetCellValue(i, sprSeikyuMDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprSeikyuMDef.IN_JUN.ColNo, dr.Item("PRINT_SORT").ToString())
                .SetCellValue(i, sprSeikyuMDef.SHUBETU_NM.ColNo, dr.Item("SEIQKMK_NM").ToString())
                .SetCellValue(i, sprSeikyuMDef.BUSYO.ColNo, dr.Item("BUSYO_CD").ToString())
                kanjoKamokuCd = Me._ControlG.EditConcatData(dr.Item("KEIRI_BUMON_CD").ToString(), dr.Item("KANJO_KAMOKU_CD").ToString(), ".")
                .SetCellValue(i, sprSeikyuMDef.KANJOKMK_CD.ColNo, kanjoKamokuCd)
                .SetCellValue(i, sprSeikyuMDef.KAZEI_NM.ColNo, dr.Item("TAX_KB_NM").ToString())
                .SetCellValue(i, sprSeikyuMDef.TCUST_BPCD.ColNo, dr.Item("TCUST_BPCD").ToString())
                .SetCellValue(i, sprSeikyuMDef.TCUST_BPNM.ColNo, dr.Item("TCUST_BPNM").ToString())
                .SetCellValue(i, sprSeikyuMDef.PRODUCT_SEG_CD.ColNo, dr.Item("PRODUCT_SEG_CD").ToString())
                .SetCellValue(i, sprSeikyuMDef.ORIG_SEG_CD.ColNo, dr.Item("ORIG_SEG_CD").ToString())
                .SetCellValue(i, sprSeikyuMDef.DEST_SEG_CD.ColNo, dr.Item("DEST_SEG_CD").ToString())
                .SetCellValue(i, sprSeikyuMDef.KEISAN_GAKU.ColNo, dr.Item("KEISAN_TLGK").ToString())
                .SetCellValue(i, sprSeikyuMDef.NEBIKI_RATE.ColNo, dr.Item("NEBIKI_RT").ToString())
                .SetCellValue(i, sprSeikyuMDef.RATENEBIKI_GAKU.ColNo, dr.Item("NEBIKI_RTGK").ToString())

                .SetCellValue(i, sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo, Me._ControlH.ToHalfAdjust(Convert.ToDecimal(dr.Item("NEBIKI_GK")), 0).ToString())
                .SetCellValue(i, sprSeikyuMDef.SEIKYU_GAKU.ColNo, "0")
                '2014.08.21 追加START 多通貨対応
                .SetCellValue(i, sprSeikyuMDef.ITEM_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                '2014.08.21 追加END 多通貨対応
                .SetCellValue(i, sprSeikyuMDef.TEKIYOU.ColNo, dr.Item("TEKIYO").ToString())
                .SetCellValue(i, sprSeikyuMDef.EDABAN.ColNo, dr.Item("SKYU_SUB_NO").ToString())
                .SetCellValue(i, sprSeikyuMDef.CREATE_SYUBETU_NM.ColNo, dr.Item("MAKE_SYU_KB_NM").ToString())

                '*****隠し列*****
                .SetCellValue(i, sprSeikyuMDef.SHUBETU_KBN.ColNo, dr.Item("SEIQKMK_CD").ToString())
                .SetCellValue(i, sprSeikyuMDef.KAZEI_KBN.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo, dr.Item("MAKE_SYU_KB").ToString())
                .SetCellValue(i, sprSeikyuMDef.GROUP_CD_KBN.ColNo, dr.Item("GROUP_KB").ToString())
                .SetCellValue(i, sprSeikyuMDef.KEIRI_KBN.ColNo, dr.Item("KEIRI_KB").ToString())
                .SetCellValue(i, sprSeikyuMDef.TEMPLATE_IMP_FLG.ColNo, dr.Item("TEMPLATE_IMP_FLG").ToString())
                .SetCellValue(i, sprSeikyuMDef.RECORD_NO.ColNo, dr.Item("RECORD_NO").ToString())
                .SetCellValue(i, sprSeikyuMDef.JISYATASYA_KB.ColNo, dr.Item("JISYATASYA_KB").ToString())        'ADD 2016/09/06
                .SetCellValue(i, sprSeikyuMDef.SEIQKMK_CD_S.ColNo, dr.Item("SEIQKMK_CD_S").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "共通処理"

    ''' <summary>
    ''' 請求名義名(銀行口座名義) 取得
    ''' </summary>
    ''' <param name="skyuDate"></param>
    ''' <param name="kouzaKb"></param>
    ''' <returns></returns>
    Friend Function GetKouzaMeigiNm(ByVal skyuDate As String, ByVal kouzaKb As String) As String

        Dim kouzaMeigiNm As String = ""

        Dim filter As String
        Dim selectDr As DataRow()

        ' 請求書出力内容変更 適用年月 (変更後帳票定義ファイル 使用開始年月) 取得
        Dim rptChgStartYm As String = GetRptChgStartYm()

        If skyuDate.PadRight(6, "9"c).Substring(0, 6) < rptChgStartYm Then
            ' 請求書出力用 変更前請求名義名(銀行口座名義) 取得
            filter = String.Concat("KBN_GROUP_CD = 'B045' AND KBN_CD = '", kouzaKb, "' AND SYS_DEL_FLG = '0'")
            selectDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            If selectDr.Count > 0 Then
                Return selectDr(0).Item("KBN_NM1").ToString()
            End If
        End If

        ' 名義・銀行マスタ よりの取得
        filter = ""
        filter = String.Concat(filter, "MEIGI_CD = '", kouzaKb, "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
        selectDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRSBANK).Select(filter)
        If selectDr.Length <> 0 Then
            kouzaMeigiNm = selectDr(0).Item("MEIGI_NM").ToString()
        End If

        Return kouzaMeigiNm

    End Function

    ''' <summary>
    ''' 請求書出力内容変更 適用年月 (変更後帳票定義ファイル 使用開始年月) 取得
    ''' </summary>
    ''' <returns></returns>
    Friend Function GetRptChgStartYm() As String

        Dim filter As String = String.Concat("KBN_GROUP_CD = 'B043' AND KBN_CD = '01' AND SYS_DEL_FLG = '0'")
        Dim selectDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
        If selectDr.Count = 0 Then
            Return "202210"
        Else
            Return selectDr(0).Item("KBN_NM1").ToString()
        End If

    End Function

#End Region

#End Region

#End Region

End Class
