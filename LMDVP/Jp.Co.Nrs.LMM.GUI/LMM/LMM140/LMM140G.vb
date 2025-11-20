' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM140G : ZONEマスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM140Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM140G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM140F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM140F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

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
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        Me._Frm.lblSituation.DispMode = dispMd
        Me._Frm.lblSituation.RecordStatus = recSts

    End Sub

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM140C.CtlTabIndex.DETAIL
            .cmbSokoKb.TabIndex = LMM140C.CtlTabIndex.SOKO
            .txtTouNo.TabIndex = LMM140C.CtlTabIndex.TOUNO
            .txtSituNo.TabIndex = LMM140C.CtlTabIndex.SITUNO
            .lblTouSituNm.TabIndex = LMM140C.CtlTabIndex.TOUSITUNM
            .txtZoneCd.TabIndex = LMM140C.CtlTabIndex.ZONECD
            .txtZoneNm.TabIndex = LMM140C.CtlTabIndex.ZONENM
            .cmbZoneKb.TabIndex = LMM140C.CtlTabIndex.ZONEKB
            .cmbHeatCtlKb.TabIndex = LMM140C.CtlTabIndex.HEATCTLKB
            .numSetHeat.TabIndex = LMM140C.CtlTabIndex.NOWSETHEAT
            .numMaxHeatLim.TabIndex = LMM140C.CtlTabIndex.MAXHEATLIM
            .numMinHeatLow.TabIndex = LMM140C.CtlTabIndex.MINHEATLOW
            .cmbHeatCtl.TabIndex = LMM140C.CtlTabIndex.HEATCTL
            .cmbBondCtlKb.TabIndex = LMM140C.CtlTabIndex.BONDCTLKB
            .cmbPharKb.TabIndex = LMM140C.CtlTabIndex.PHARKB
            .cmbPflKb.TabIndex = LMM140C.CtlTabIndex.PFLKB
            .cmbGasCtlKb.TabIndex = LMM140C.CtlTabIndex.GASCTLKB
            .txtSeiqCd.TabIndex = LMM140C.CtlTabIndex.SEIQCD
            .numRentMonthly.TabIndex = LMM140C.CtlTabIndex.RENTMONTHLY
            .grpShoboJoho.TabIndex = LMM140C.CtlTabIndex.SHOBO_JOHO
            .btnRowAdd.TabIndex = LMM140C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM140C.CtlTabIndex.BTN_DEL
            .sprDetailShobo.TabIndex = LMM140C.CtlTabIndex.SPR_ZONE_SHOBO
            .grpDoku.TabIndex = LMM140C.CtlTabIndex.DOKU_JOHO
            .btnRowAdd_Doku.TabIndex = LMM140C.CtlTabIndex.BTN_DOKU_ADD
            .btnRowDel_Doku.TabIndex = LMM140C.CtlTabIndex.BTN_DOKU_DEL
            .sprDetailDoku.TabIndex = LMM140C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_DOKU
            .GrpKouathuGas.TabIndex = LMM140C.CtlTabIndex.KOUATHUGAS_JOHO
            .btnRowAdd_KouathuGas.TabIndex = LMM140C.CtlTabIndex.BTN_KOUATHUGAS_DEL
            .btnRowDel_KouathuGas.TabIndex = LMM140C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_KOUATHUGAS
            .sprDetailKouathuGas.TabIndex = LMM140C.CtlTabIndex.BTN_KOUATHUGAS_ADD
            .grpYakuzihoJoho.TabIndex = LMM140C.CtlTabIndex.YAKUZIHO_JOHO
            .btnRowAdd_Yakuziho.TabIndex = LMM140C.CtlTabIndex.BTN_YAKUZIHO_DEL
            .btnRowDel_Yakuziho.TabIndex = LMM140C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_YAKUZIHO
            .sprDetailYakuzihoJoho.TabIndex = LMM140C.CtlTabIndex.BTN_YAKUZIHO_ADD
            .lblSituation.TabIndex = LMM140C.CtlTabIndex.SITUATION
            .lblCrtUser.TabIndex = LMM140C.CtlTabIndex.CRTDATE
            .lblTitleCrtDate.TabIndex = LMM140C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM140C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM140C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM140C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM140C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM140C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''各コンボボックスの初期値設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm
            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbSokoKb.SelectedValue = Nothing
            .cmbZoneKb.SelectedValue = LMM140C.TUBO
            .cmbHeatCtlKb.SelectedValue = LMM140C.JOUON
            .cmbHeatCtl.SelectedValue = LMM140C.ONDOMIKANRI
            .cmbBondCtlKb.SelectedValue = LMM140C.FUTUUKANRI
            .cmbPharKb.SelectedValue = LMM140C.NONE
            .cmbPflKb.SelectedValue = LMM140C.NONE
            .cmbGasCtlKb.SelectedValue = LMM140C.NONE
        End With

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d2 As Decimal = Convert.ToDecimal("99")
            Dim dm2 As Decimal = Convert.ToDecimal("-99")
            Dim d9 As Decimal = Convert.ToDecimal("999999999")

            'numberCellの桁数を設定する
            .numSetHeat.SetInputFields("#0", , 2, 1, , 0, 0, , d2, dm2)
            .numMaxHeatLim.SetInputFields("#0", , 2, 1, , 0, 0, , d2, dm2)
            .numMinHeatLow.SetInputFields("#0", , 2, 1, , 0, 0, , d2, dm2)

            .numRentMonthly.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)


        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me.ClearControl()
                    Me.LockControl(True)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Me.LockControl(False)
                            Me._ControlG.SetLockControl(.cmbNrsBrCd, True)
                            Me._ControlG.SetLockControl(.cmbSokoKb, True)
                            Me._ControlG.SetLockControl(.txtSituNo, True)
                            Me._ControlG.SetLockControl(.txtTouNo, True)
                            Me._ControlG.SetLockControl(.lblTouSituNm, True)
                            Me._ControlG.SetLockControl(.txtZoneCd, True)

                            Me.SetLockZoneShoboSpreadControl(False)
                            Me.SetLockTouSituZoneChkSpreadControl(False)

                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me._ControlG.SetLockControl(.cmbNrsBrCd, True)
                            Me._ControlG.SetLockControl(.lblTouSituNm, True)

                            Me.SetLockZoneShoboSpreadControl(False)
                            Me.SetLockTouSituZoneChkSpreadControl(False)

                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me._ControlG.SetLockControl(.cmbNrsBrCd, True)
                            Me._ControlG.SetLockControl(.cmbSokoKb, True)
                            Me._ControlG.SetLockControl(.txtSituNo, True)
                            Me._ControlG.SetLockControl(.txtTouNo, True)
                            Me._ControlG.SetLockControl(.lblTouSituNm, True)
                            Call Me.ClearControlFukusha()

                            Me.SetLockZoneShoboSpreadControl(False)
                            Me.SetLockTouSituZoneChkSpreadControl(False)

                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)

                    Me.SetLockZoneShoboSpreadControl(True)
                    Me.SetLockTouSituZoneChkSpreadControl(True)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtZoneCd.TextValue = String.Empty
            .txtZoneNm.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub
    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM140C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM140C.EventShubetsu.MAIN, LMM140C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM140C.EventShubetsu.SHINKI
                    .cmbSokoKb.Focus()
                Case LMM140C.EventShubetsu.HENSHU
                    .txtZoneNm.Focus()
                Case LMM140C.EventShubetsu.HUKUSHA
                    .txtZoneCd.Focus()
                Case LMM140C.EventShubetsu.INS_T
                    .sprDetailShobo.Focus()
                Case LMM140C.EventShubetsu.INS_DOKU
                    .sprDetailDoku.Focus()
                Case LMM140C.EventShubetsu.INS_KOUATHUGAS
                    .sprDetailKouathuGas.Focus()
                Case LMM140C.EventShubetsu.INS_YAKUZIHO
                    .sprDetailYakuzihoJoho.Focus()
                Case LMM140C.EventShubetsu.DEL_T
                    .sprDetailShobo.Focus()
                Case LMM140C.EventShubetsu.DEL_DOKU
                    .sprDetailDoku.Focus()
                Case LMM140C.EventShubetsu.DEL_KOUATHUGAS
                    .sprDetailKouathuGas.Focus()
                Case LMM140C.EventShubetsu.DEL_YAKUZIHO
                    .sprDetailYakuzihoJoho.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbSokoKb.SelectedValue = Nothing
            .cmbNrsBrCd.SelectedValue = Nothing
            .txtTouNo.TextValue = String.Empty
            .txtSituNo.TextValue = String.Empty
            .lblTouSituNm.TextValue = String.Empty
            .txtZoneCd.TextValue = String.Empty
            .txtZoneNm.TextValue = String.Empty
            .cmbZoneKb.SelectedValue = Nothing
            .cmbHeatCtlKb.SelectedValue = Nothing
            .numSetHeat.Value = 0
            .numMaxHeatLim.Value = 0
            .numMinHeatLow.Value = 0
            .cmbHeatCtl.SelectedValue = Nothing
            .cmbBondCtlKb.SelectedValue = Nothing
            .cmbPharKb.SelectedValue = Nothing
            .cmbPflKb.SelectedValue = Nothing
            .cmbGasCtlKb.SelectedValue = Nothing
            .txtSeiqCd.TextValue = String.Empty
            .lblSeiqNm.TextValue = String.Empty
            .numRentMonthly.Value = 0
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
            .lblSysDelFlg.TextValue = String.Empty

            'ゾーンマスタ消防情報スプレッド
            .sprDetailShobo.CrearSpread()

            '棟室ゾーンチェックマスタスプレッド
            .sprDetailDoku.CrearSpread()
            .sprDetailKouathuGas.CrearSpread()
            .sprDetailYakuzihoJoho.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .cmbSokoKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.WH_CD.ColNo).Text
            .txtTouNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.TOU_NO.ColNo).Text
            .txtSituNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SITU_NO.ColNo).Text
            .lblTouSituNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.TOU_SITU_NM.ColNo).Text
            .txtZoneCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ZONE_CD.ColNo).Text
            .txtZoneNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ZONE_NM.ColNo).Text
            .cmbZoneKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ZONE_KB.ColNo).Text
            .cmbHeatCtlKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ONDO_CTL_KB.ColNo).Text
            .numSetHeat.Value = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ONDO.ColNo).Text
            .numMaxHeatLim.Value = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.MAX_ONDO_UP.ColNo).Text
            .numMinHeatLow.Value = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.MINI_ONDO_DOWN.ColNo).Text
            .cmbHeatCtl.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.ONDO_CTL_FLG.ColNo).Text
            .cmbBondCtlKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.HOZEI_KB.ColNo).Text
            .cmbPharKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.YAKUJI_YN.ColNo).Text
            .cmbPflKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.DOKU_YN.ColNo).Text
            .cmbGasCtlKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.GASS_YN.ColNo).Text
            .txtSeiqCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SEIQTO_CD.ColNo).Text
            .lblSeiqNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SEIQTO_NM.ColNo).Text
            .numRentMonthly.Value = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.TSUBO_AM.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(ゾーンマスタ消防情報)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockZoneShoboSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetailShobo)
            Dim max As Integer = .sprDetailShobo.ActiveSheet.Rows.Count

            For i As Integer = 1 To max

                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetailShobo, lockFlg))
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, lbl)
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.HINMEI.ColNo, lbl)
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, lbl)
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, lbl)
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.BAISU.ColNo, LMSpreadUtility.GetNumberCell(.sprDetailShobo, 0, 999999.99, lockFlg, 2, , ","))
                .sprDetailShobo.SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetailShobo, lockFlg))
            Next

        End With

    End Sub

    ''' <summary>
    ''' 棟室ゾーンチェックマスタスプレッド(3種)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockTouSituZoneChkSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            '毒劇情報
            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetailDoku)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetailDoku, LMM140C.M_Z_KBN_DOKUGEKI, lockFlg)

            Dim max As Integer = .sprDetailDoku.ActiveSheet.Rows.Count

            For i As Integer = 1 To max
                .sprDetailDoku.SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetailDoku, lockFlg))
                .sprDetailDoku.SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.DOKU_KB.ColNo, sComboKbn)
            Next

            '高圧ガス情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetailKouathuGas)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetailKouathuGas, LMM140C.M_Z_KBN_KOUATHUGAS, lockFlg)

            max = .sprDetailKouathuGas.ActiveSheet.Rows.Count

            For i As Integer = 1 To max

                .sprDetailKouathuGas.SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetailKouathuGas, lockFlg))
                .sprDetailKouathuGas.SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, sComboKbn)
            Next

            '薬事法情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetailYakuzihoJoho)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetailYakuzihoJoho, LMM140C.M_Z_KBN_YAKUZIHO, lockFlg)

            max = .sprDetailYakuzihoJoho.ActiveSheet.Rows.Count

            For i As Integer = 1 To max

                .sprDetailYakuzihoJoho.SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetailYakuzihoJoho, lockFlg))
                .sprDetailYakuzihoJoho.SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, sComboKbn)
            Next

        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds.Tables(LMM140C.TABLE_NM_OUT))

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.DEF, "  ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.NRS_BR_CD, "営業所コード", 50, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.WH_CD, "倉庫コード", 50, False)
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.WH_NM, "倉庫", 200, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SITU_NO, "室", 30, True)
        Public Shared TOU_SITU_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.TOU_SITU_NM, "棟室名", 50, False)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ZONE_CD, "ZONE", 60, True)
        Public Shared ZONE_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ZONE_NM, "ZONE名", 200, True)
        Public Shared HOZEI_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.HOZEI_KB, "保税管理区分", 50, False)
        Public Shared HOZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.HOZEI_KB_NM, "保税区分", 80, True)
        Public Shared ONDO_CTL_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ONDO_CTL_KB, "温度管理区分", 50, False)
        Public Shared ONDO_CTL_KB_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ONDO_CTL_KB_NM, "温度管理区分", 110, True)
        Public Shared ONDO_CTL_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ONDO_CTL_FLG, "温度管理中フラグ", 50, False)
        Public Shared ONDO_CTL_FLG_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ONDO_CTL_FLG_NM, "温度管理中", 90, True)
        Public Shared ONDO As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ONDO, "設定" & vbCrLf & "温度(℃)", 80, True)
        Public Shared YAKUJI_YN As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.YAKUJI_YN, "薬機法有無区分", 50, False)
        Public Shared YAKUJI_YN_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.YAKUJI_YN_NM, "薬事", 45, True)
        Public Shared DOKU_YN As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.DOKU_YN, "毒劇物取締法有無区分", 50, False)
        Public Shared DOKU_YN_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.DOKU_YN_NM, "毒劇", 45, True)
        Public Shared GASS_YN As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.GASS_YN, "ガス管理有無区分", 50, False)
        Public Shared GASS_YN_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.GASS_YN_NM, "ガス", 45, True)
        Public Shared ZONE_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.ZONE_KB, "ZONE区分", 50, False)
        Public Shared MAX_ONDO_UP As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.MAX_ONDO_UP, "最高設定温度上限", 50, False)
        Public Shared MINI_ONDO_DOWN As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.MINI_ONDO_DOWN, "最低設定温度下限", 50, False)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SEIQTO_CD, "請求先コード", 50, False)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SEIQTO_NM, "請求先名", 50, False)
        Public Shared TSUBO_AM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.TSUBO_AM, "貸料月額", 50, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_ENT_DATE, "作成日", 50, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 50, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_UPD_DATE, "更新日", 50, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 50, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)


    End Class

    ''' <summary>
    ''' スプレッド列定義体(ゾーンマスタ消防情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefShobo

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.DEF, " ", 20, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.SHOBO_CD, "消防" & vbCrLf & "コード", 80, True)
        Public Shared HINMEI As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.HINMEI, "品名", 150, True)
        Public Shared KIKEN_TOKYU As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.KIKEN_TOKYU, "危険等級", 80, True)
        Public Shared KIKEN_SYU As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.KIKEN_SYU, "危険種別", 80, True)
        Public Shared BAISU As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.BAISU, "倍数", 80, True)
        Public Shared WH_KYOKA_DATE As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.WH_KYOKA_DATE, "許可日", 90, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexShobo.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(毒劇情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefDoku

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexDoku.DEF, " ", 20, True)
        Public Shared DOKU_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexDoku.DOKU_KB, "毒劇区分", 114, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexDoku.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexDoku.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(高圧ガス情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefKouathuGas

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexKouathuGas.DEF, " ", 20, True)
        Public Shared KOUATHUGAS_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexKouathuGas.KOUATHUGAS_KB, "高圧ガス区分", 304, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexKouathuGas.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexKouathuGas.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(薬事法情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefYakuziho

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexYakuziho.DEF, " ", 20, True)
        Public Shared YAKUZIHO_KB As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexYakuziho.YAKUZIHO_KB, "薬機法区分", 184, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexYakuziho.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM140C.SprColumnIndexYakuziho.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()
        Dim dr As DataRow
        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 36

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM140G.sprDetailDef())
            .sprDetail.SetColProperty(New LMM140G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM140G.sprDetailDef.DEF.ColNo + 1

            Dim umuStyle As StyleInfo = Me.StyleInfoCustCond(.sprDetail)
            Dim rowCount As Integer = 0

            '列設定

            '表示項目
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.WH_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'START YANAI 要望番号705
            '.sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 1, False))
            '.sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 1, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 2, False))
            'END YANAI 要望番号705
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ZONE_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.HOZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_H001, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_O002, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_O004, False))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ONDO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.YAKUJI_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.DOKU_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.GASS_YN_NM.ColNo, umuStyle)
            '非表示項目
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.TOU_SITU_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.HOZEI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ONDO_CTL_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ONDO_CTL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.YAKUJI_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.DOKU_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.GASS_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.ZONE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.MAX_ONDO_UP.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.MINI_ONDO_DOWN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SEIQTO_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.TSUBO_AM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))


        End With

        'ゾーンマスタ消防Spreadの初期化処理
        Call Me.InitShoboSpread()

        '毒劇情報Spreadの初期化処理
        Call Me.InitDokuSpread()

        '高圧ガス情報Spreadの初期化処理
        Call Me.InitKouathuGasSpread()

        '薬事法情報Spreadの初期化処理
        Call Me.InitYakuzihoSpread()

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM140F)

        With frm.sprDetail

            Dim rowCount As Integer = 0

            .SetCellValue(rowCount, LMM140G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(rowCount, LMM140G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(rowCount, LMM140G.sprDetailDef.WH_CD.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.WH_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.TOU_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SITU_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.TOU_SITU_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ZONE_CD.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ZONE_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.HOZEI_KB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.HOZEI_KB_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ONDO_CTL_KB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ONDO_CTL_FLG.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ONDO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.YAKUJI_YN.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.YAKUJI_YN_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.DOKU_YN.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.DOKU_YN_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.GASS_YN.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.GASS_YN_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.ZONE_KB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.MAX_ONDO_UP.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.MINI_ONDO_DOWN.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SEIQTO_CD.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SEIQTO_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.TSUBO_AM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(rowCount, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)



        End With

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitShoboSpread()

        'ゾーンマスタ消防Spreadの初期値設定
        Dim sprDetailShobo As LMSpread = Me._Frm.sprDetailShobo

        With sprDetailShobo

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 9

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM140G.sprDetailDefShobo(), False)

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetailShobo)

            '**** 表示列 ****
            .SetCellStyle(-1, sprDetailDefShobo.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetailShobo, False))
            .SetCellStyle(-1, sprDetailDefShobo.SHOBO_CD.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDefShobo.HINMEI.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDefShobo.KIKEN_TOKYU.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDefShobo.KIKEN_SYU.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDefShobo.BAISU.ColNo, LMSpreadUtility.GetNumberCell(sprDetailShobo, 0, 999999.99, False, 2, , ","))
            .SetCellStyle(-1, sprDetailDefShobo.WH_KYOKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(sprDetailShobo, False))
            .SetCellStyle(-1, sprDetailDefShobo.UPD_FLG.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' 毒劇情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitDokuSpread()

        '毒劇情報Spreadの初期値設定
        Dim sprDetailDoku As LMSpread = Me._Frm.sprDetailDoku

        With sprDetailDoku

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM140C.SprColumnIndexDoku.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM140G.sprDetailDefDoku(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDefDoku.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetailDoku, False))
            .SetCellStyle(-1, sprDetailDefDoku.DOKU_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetailDoku, LMM140C.M_Z_KBN_DOKUGEKI, False))

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitKouathuGasSpread()

        '高圧ガス情報Spreadの初期値設定
        Dim sprDetailKouathuGas As LMSpread = Me._Frm.sprDetailKouathuGas

        With sprDetailKouathuGas

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM140C.SprColumnIndexKouathuGas.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM140G.sprDetailDefKouathuGas(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDefKouathuGas.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetailKouathuGas, False))
            .SetCellStyle(-1, sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetailKouathuGas, LMM140C.M_Z_KBN_KOUATHUGAS, False))

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitYakuzihoSpread()

        '薬事法情報Spreadの初期値設定
        Dim sprDetailYakuziho As LMSpread = Me._Frm.sprDetailYakuzihoJoho

        With sprDetailYakuziho

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM140C.SprColumnIndexYakuziho.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM140G.sprDetailDefYakuziho(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDefYakuziho.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetailYakuziho, False))
            .SetCellStyle(-1, sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetailYakuziho, LMM140C.M_Z_KBN_YAKUZIHO, False))

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

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

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -99, 99, True, 0, , ",")

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM140G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.WH_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.TOU_SITU_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ZONE_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.HOZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.HOZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ONDO_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ONDO_CTL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ONDO.ColNo, sNumber)
                .SetCellStyle(i, LMM140G.sprDetailDef.YAKUJI_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.YAKUJI_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.DOKU_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.DOKU_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.GASS_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.GASS_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.ZONE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.MAX_ONDO_UP.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.MINI_ONDO_DOWN.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.TSUBO_AM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM140G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.TOU_SITU_NM.ColNo, dr.Item("TOU_SITU_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ZONE_NM.ColNo, dr.Item("ZONE_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.HOZEI_KB.ColNo, dr.Item("HOZEI_KB").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.HOZEI_KB_NM.ColNo, dr.Item("HOZEI_KB_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ONDO_CTL_KB.ColNo, dr.Item("ONDO_CTL_KB").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, dr.Item("ONDO_CTL_KB_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ONDO_CTL_FLG.ColNo, dr.Item("ONDO_CTL_FLG").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, dr.Item("ONDO_CTL_FLG_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ONDO.ColNo, dr.Item("ONDO").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.YAKUJI_YN.ColNo, dr.Item("YAKUJI_YN").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.YAKUJI_YN_NM.ColNo, dr.Item("YAKUJI_YN_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.DOKU_YN.ColNo, dr.Item("DOKU_YN").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.DOKU_YN_NM.ColNo, dr.Item("DOKU_YN_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.GASS_YN.ColNo, dr.Item("GASS_YN").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.GASS_YN_NM.ColNo, dr.Item("GASS_YN_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.ZONE_KB.ColNo, dr.Item("ZONE_KB").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.MAX_ONDO_UP.ColNo, dr.Item("MAX_ONDO_UP").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.MINI_ONDO_DOWN.ColNo, dr.Item("MINI_ONDO_DOWN").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.TSUBO_AM.ColNo, dr.Item("TSUBO_AM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM140G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防スプレッドにデータを設定_SPREADダブルクリック時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadShobo(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String, ByRef zonecd As String)

        Dim sprShobo As LMSpread = Me._Frm.sprDetailShobo
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,SHOBO_CD ASC,ZONE_CD ASC"

        Dim tmpDatarowShobo() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND TOU_NO = '", touno, "' AND SITU_NO = '", situno, "' AND ZONE_CD = '", zonecd, "'"), sort)

        With sprShobo

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarowShobo.Length

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprShobo, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprShobo, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(sprShobo, 0, 999999.99, False, 2, , ",")
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(sprShobo, False)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarowShobo(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.HINMEI.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.BAISU.ColNo, sNumber)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, sDate)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.HINMEI.ColNo, dr.Item("HINMEI").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, dr.Item("TOKYU").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, dr.Item("SHUBETU").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.BAISU.ColNo, dr.Item("BAISU").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("WH_KYOKA_DATE").ToString()))
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 毒劇情報スプレッドにデータを設定_SPREADダブルクリック時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadDoku(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String, ByRef zonecd As String)

        Dim sprDoku As LMSpread = Me._Frm.sprDetailDoku
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarowDoku() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND TOU_NO = '", touno, "' AND SITU_NO = '", situno, "' AND ZONE_CD = '", zonecd, "' AND KBN_GROUP_CD = '", LMM140C.M_Z_KBN_DOKUGEKI, "'"), sort)

        With sprDoku

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarowDoku.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprDoku, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprDoku, LMM140C.M_Z_KBN_DOKUGEKI, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprDoku, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarowDoku(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.DOKU_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM140G.sprDetailDefDoku.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM140G.sprDetailDefDoku.DOKU_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefDoku.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッドにデータを設定_SPREADダブルクリック時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadKouathuGas(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String, ByRef zonecd As String)

        Dim sprKouathuGas As LMSpread = Me._Frm.sprDetailKouathuGas
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarowKouathuGas() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND TOU_NO = '", touno, "' AND SITU_NO = '", situno, "' AND ZONE_CD = '", zonecd, "' AND KBN_GROUP_CD = '", LMM140C.M_Z_KBN_KOUATHUGAS, "'"), sort)

        With sprKouathuGas

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarowKouathuGas.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprKouathuGas, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprKouathuGas, LMM140C.M_Z_KBN_KOUATHUGAS, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprKouathuGas, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarowKouathuGas(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM140G.sprDetailDefKouathuGas.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッドにデータを設定_SPREADダブルクリック時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadYakuziho(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String, ByRef zonecd As String)

        Dim sprYakuziho As LMSpread = Me._Frm.sprDetailYakuzihoJoho
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarowYakuziho() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND TOU_NO = '", touno, "' AND SITU_NO = '", situno, "' AND ZONE_CD = '", zonecd, "' AND KBN_GROUP_CD = '", LMM140C.M_Z_KBN_YAKUZIHO, "'"), sort)

        With sprYakuziho

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarowYakuziho.Length

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprYakuziho, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprYakuziho, LMM140C.M_Z_KBN_YAKUZIHO, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprYakuziho, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarowYakuziho(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM140G.sprDetailDefYakuziho.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM2" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_U009}
                                                  )

    End Function

    ''' <summary>
    ''' ゾーンマスタ消防スプレッドにデータを設定_行追加時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpreadShobo(ByVal dt As DataTable)

        Dim sprShobo As LMSpread = Me._Frm.sprDetailShobo
        Dim dtOut As New DataSet

        With sprShobo

            .SuspendLayout()

            For i As Integer = 0 To dt.Rows.Count - 1

                .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

                'セルに設定するスタイルの取得
                Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprShobo, False)
                Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprShobo, CellHorizontalAlignment.Left)
                Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(sprShobo, 0, 999999.99, False, 2, , ",")
                Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(sprShobo, False)

                sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

                Dim dr As DataRow = Nothing

                '値設定(消防マスタ照会POP)
                dr = dt.Rows(i)

                Dim row As Integer = .Sheets(0).Rows.Count - 1

                'セルスタイル設定
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.DEF.ColNo, sDEF)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.HINMEI.ColNo, sLabel)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, sLabel)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, sLabel)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.BAISU.ColNo, sNumber)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, sDate)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, sLabel)
                .SetCellStyle(row, LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(row, LMM140G.sprDetailDefShobo.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(row, LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue(row, LMM140G.sprDetailDefShobo.HINMEI.ColNo, dr.Item("HINMEI").ToString())
                .SetCellValue(row, LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, dr.Item("KIKEN_TOKYU_NM").ToString())
                .SetCellValue(row, LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, dr.Item("SYU_NM").ToString())
                .SetCellValue(row, LMM140G.sprDetailDefShobo.BAISU.ColNo, String.Empty)
                .SetCellValue(row, LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, String.Empty)
                .SetCellValue(row, LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, "0")
                .SetCellValue(row, LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, "0")
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 毒劇情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpreadDoku()

        Dim sprDoku As LMSpread = Me._Frm.sprDetailDoku

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With sprDoku

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprDoku, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprDoku, LMM140C.M_Z_KBN_DOKUGEKI, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprDoku, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM140G.sprDetailDefDoku.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM140G.sprDetailDefDoku.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM140G.sprDetailDefDoku.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM140G.sprDetailDefDoku.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM140G.sprDetailDefDoku.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpreadKouathuGas()

        Dim sprKouathuGas As LMSpread = Me._Frm.sprDetailKouathuGas

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With sprKouathuGas

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprKouathuGas, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprKouathuGas, LMM140C.M_Z_KBN_KOUATHUGAS, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprKouathuGas, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM140G.sprDetailDefKouathuGas.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM140G.sprDetailDefKouathuGas.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM140G.sprDetailDefKouathuGas.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM140G.sprDetailDefKouathuGas.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpreadYakuziho()

        Dim sprYakuziho As LMSpread = Me._Frm.sprDetailYakuzihoJoho

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With sprYakuziho

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprYakuziho, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(sprYakuziho, LMM140C.M_Z_KBN_YAKUZIHO, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprYakuziho, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM140G.sprDetailDefYakuziho.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM140G.sprDetailDefYakuziho.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM140G.sprDetailDefYakuziho.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM140G.sprDetailDefYakuziho.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防スプレッドにデータを設定_行削除時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetSpread(ByVal spr As LMSpread)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.99, False, 2, , ",")
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, False, CellType.DateTimeFormat.ShortDate)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            For i As Integer = 1 To max

                If i > max Then
                    Exit For
                End If

                '行削除処理で最初のスプレッドの行数が減った場合は、行削除後のスプレッドの行数を設定
                If max > .ActiveSheet.Rows.Count Then
                    i = i - 1
                    max = max - 1
                End If

                '行削除されていない行は再描画
                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.SYS_DEL_FLG_T.ColNo))) = False Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.DEF.ColNo, sDEF)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.HINMEI.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.BAISU.ColNo, sNumber)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, sDate)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.SHOBO_CD.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.SHOBO_CD.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.HINMEI.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.HINMEI.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.KIKEN_TOKYU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.KIKEN_TOKYU.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.KIKEN_SYU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.KIKEN_SYU.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.BAISU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.BAISU.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo, DateFormatUtility.EditSlash(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.WH_KYOKA_DATE.ColNo)).ToString()))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.UPD_FLG.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.UPD_FLG.ColNo)))
                    .SetCellValue((i - 1), LMM140G.sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDefShobo.SYS_DEL_FLG_T.ColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' ゾーンマスタ消防Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelZoneShobo(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To max

                If i > max Then
                    Exit For
                End If

                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDefShobo.DEF.ColNo))) = True Then

                    If ("1").Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDefShobo.UPD_FLG.ColNo))) = True Then
                        '既に登録済みのデータの場合は、削除フラグを"1"に変更
                        .SetCellValue(i, sprDetailDefShobo.SYS_DEL_FLG_T.ColNo, LMConst.FLG.ON)
                    Else
                        '上記以外(新規追加データ)の場合は、スプレッドから行削除
                        .ActiveSheet.RemoveRows(i, 1)

                        '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                        i = i - 1
                        max = max - 1
                    End If

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 棟室ゾーンチェックマスタスプレッドの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTouSituZoneChk(ByVal spr As LMSpread, ByVal defColNo As Integer, ByVal updFlgColNo As Integer, ByVal sysDelFlgColNo As Integer)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To max

                If i > max Then
                    Exit For
                End If

                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, defColNo))) = True Then

                    If ("1").Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, updFlgColNo))) = True Then
                        '既に登録済みのデータの場合は、削除フラグを"1"に変更
                        .SetCellValue(i, sysDelFlgColNo, LMConst.FLG.ON)
                    Else
                        '上記以外(新規追加データ)の場合は、スプレッドから行削除
                        .ActiveSheet.RemoveRows(i, 1)

                        '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                        i = i - 1
                        max = max - 1
                    End If

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 棟室ゾーンチェックマスタスプレッドスプレッドにデータを設定_行削除時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetTouSituZoneChkSpread(ByVal spr As LMSpread, ByVal defColNo As Integer, ByVal comboColNo As Integer, ByVal updFlgColNo As Integer, ByVal sysDelFlgColNo As Integer)

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sComboKbn As StyleInfo
            Dim sKbnGroupCode As String = ""
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Select Case spr.Name
                Case Me._Frm.sprDetailDoku.Name
                    sKbnGroupCode = LMM140C.M_Z_KBN_DOKUGEKI

                Case Me._Frm.sprDetailKouathuGas.Name
                    sKbnGroupCode = LMM140C.M_Z_KBN_KOUATHUGAS

                Case Me._Frm.sprDetailYakuzihoJoho.Name
                    sKbnGroupCode = LMM140C.M_Z_KBN_YAKUZIHO

            End Select

            sComboKbn = LMSpreadUtility.GetComboCellKbn(spr, sKbnGroupCode, False)

            For i As Integer = 1 To max

                If i > max Then
                    Exit For
                End If

                '行削除処理で最初のスプレッドの行数が減った場合は、行削除後のスプレッドの行数を設定
                If max > .ActiveSheet.Rows.Count Then
                    i = i - 1
                    max = max - 1
                End If

                '行削除されていない行は再描画
                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sysDelFlgColNo))) = False Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), defColNo, sDEF)
                    .SetCellStyle((i - 1), comboColNo, sComboKbn)
                    .SetCellStyle((i - 1), updFlgColNo, sLabel)
                    .SetCellStyle((i - 1), sysDelFlgColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), defColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), comboColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), comboColNo)))
                    .SetCellValue((i - 1), updFlgColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), updFlgColNo)))
                    .SetCellValue((i - 1), sysDelFlgColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sysDelFlgColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub
#End Region 'Spread

#Region "部品化検討中"

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me._ControlG.SetLockControl(.cmbNrsBrCd, lock)
            Me._ControlG.SetLockControl(.cmbSokoKb, lock)
            Me._ControlG.SetLockControl(.txtTouNo, lock)
            Me._ControlG.SetLockControl(.txtSituNo, lock)
            Me._ControlG.SetLockControl(.lblTouSituNm, lock)
            Me._ControlG.SetLockControl(.txtZoneCd, lock)
            Me._ControlG.SetLockControl(.txtZoneNm, lock)
            Me._ControlG.SetLockControl(.cmbZoneKb, lock)
            Me._ControlG.SetLockControl(.cmbHeatCtlKb, lock)
            Me._ControlG.SetLockControl(.numSetHeat, lock)
            Me._ControlG.SetLockControl(.numMaxHeatLim, lock)
            Me._ControlG.SetLockControl(.numMinHeatLow, lock)
            Me._ControlG.SetLockControl(.cmbHeatCtl, lock)
            Me._ControlG.SetLockControl(.cmbBondCtlKb, lock)
            Me._ControlG.SetLockControl(.cmbPharKb, lock)
            Me._ControlG.SetLockControl(.cmbPflKb, lock)
            Me._ControlG.SetLockControl(.cmbGasCtlKb, lock)
            Me._ControlG.SetLockControl(.txtSeiqCd, lock)
            Me._ControlG.SetLockControl(.numRentMonthly, lock)
            Me._ControlG.SetLockControl(.lblCrtUser, lock)
            Me._ControlG.SetLockControl(.lblTitleCrtDate, lock)
            Me._ControlG.SetLockControl(.lblUpdUser, lock)
            Me._ControlG.SetLockControl(.lblUpdDate, lock)
            Me._ControlG.SetLockControl(.lblUpdTime, lock)
            Me._ControlG.SetLockControl(.lblSysDelFlg, lock)

            'ゾーンマスタ消防情報 行追加削除ボタン
            Me._ControlG.SetLockControl(.btnRowAdd, lock)
            Me._ControlG.SetLockControl(.btnRowDel, lock)

            '棟室ゾーンチェックマスタスプレッド(3種) 行追加削除ボタン
            Me._ControlG.SetLockControl(.btnRowAdd_Doku, lock)
            Me._ControlG.SetLockControl(.btnRowDel_Doku, lock)
            Me._ControlG.SetLockControl(.btnRowAdd_KouathuGas, lock)
            Me._ControlG.SetLockControl(.btnRowDel_KouathuGas, lock)
            Me._ControlG.SetLockControl(.btnRowAdd_Yakuziho, lock)
            Me._ControlG.SetLockControl(.btnRowDel_Yakuziho, lock)

        End With

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl(ByVal actionType As LMM140C.EventShubetsu)

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim ptn1 As Boolean = True

            Dim heatCtl As String = .cmbHeatCtlKb.SelectedValue.ToString()

            '空でない 且つ 定温の場合、活性化
            If String.IsNullOrEmpty(heatCtl) = False _
                AndAlso LMM140C.TEION.Equals(heatCtl) = True Then

                ptn1 = False

            End If

            Call Me.ChnageLockControl(.numSetHeat, ptn1)
            Call Me.ChnageLockControl(.numMaxHeatLim, ptn1)
            Call Me.ChnageLockControl(.numMinHeatLow, ptn1)

            'ロックする場合は値をクリア
            If ptn1 = True Then
                .numSetHeat.Value = 0
                .numMaxHeatLim.Value = 0
                .numMinHeatLow.Value = 0
            End If

        End With

    End Sub

    ''' <summary>
    ''' ロック切り替え処理
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="lock">ロックフラグ</param>
    ''' <remarks></remarks>
    Private Sub ChnageLockControl(ByVal ctl As Win.Interface.ILMEditableControl, ByVal lock As Boolean)

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, Win.InputMan.LMComboNrsBr).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            DirectCast(ctl, Win.InputMan.LMComboSoko).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock

        End If

    End Sub

#End Region

#End Region

End Class
