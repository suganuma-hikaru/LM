' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM130G : 棟室マスタメンテナンス
'  作  成  者     : [kishi]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMM130Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM130G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM130F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM130F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        'Gamen共通クラスの設定
        _ControlG = New LMMControlG(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region

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
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start 
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            '.F10ButtonName = String.Empty
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start 
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
            .F10ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            .F10ButtonEnabled = edit
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

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
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            .imdSearchDate_From.TabIndex = LMM130C.CtlTabIndex.APL_DATE_FROM
            .imdSearchDate_To.TabIndex = LMM130C.CtlTabIndex.APL_DATE_TO
            .txtCustCD.TabIndex = LMM130C.CtlTabIndex.CUST_CD
            .btnIkkatuTouroku.TabIndex = LMM130C.CtlTabIndex.IKKATU_TOUROKU
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
            .sprDetail.TabIndex = LMM130C.CtlTabIndex.TOU_SITU
            '編集部
            .tabTouSitu.TabIndex = LMM130C.CtlTabIndex.TAB_TOU_SITU
            .cmbNrsBrCd.TabIndex = LMM130C.CtlTabIndex.NRS_BR_CD
            .cmbWare.TabIndex = LMM130C.CtlTabIndex.WH_CD
            .txtTouNo.TabIndex = LMM130C.CtlTabIndex.TOU_NO
            .txtSituNo.TabIndex = LMM130C.CtlTabIndex.SITU_NO
            .txtTouSituNm.TabIndex = LMM130C.CtlTabIndex.TOU_SITU_NM
            .cmbSokoKbn.TabIndex = LMM130C.CtlTabIndex.SOKO_KB
            .cmbHozeiKbn.TabIndex = LMM130C.CtlTabIndex.HOZEI_KB
            .numChozoMaxQty.TabIndex = LMM130C.CtlTabIndex.CHOZO_MAX_QTY
            .numChozoMaxBaisu.TabIndex = LMM130C.CtlTabIndex.CHOZO_MAX_BAISU
            .cmbOndoCtlKbn.TabIndex = LMM130C.CtlTabIndex.ONDO_CTL_KB
            .numMaxOndoUp.TabIndex = LMM130C.CtlTabIndex.MAX_ONDO_UP
            .numMiniOndoDown.TabIndex = LMM130C.CtlTabIndex.MINI_ONDO_DOWN
            .numOndo.TabIndex = LMM130C.CtlTabIndex.ONDO
            .cmbOndoCtlFlg.TabIndex = LMM130C.CtlTabIndex.ONDO_CTL_FLG
            .numCbm.TabIndex = LMM130C.CtlTabIndex.CBM
            .numArea.TabIndex = LMM130C.CtlTabIndex.AREA
            .txtHan.TabIndex = LMM130C.CtlTabIndex.HAN
            .numMxPltQt.TabIndex = LMM130C.CtlTabIndex.MX_PLT_QT
            .cmbRackYn.TabIndex = LMM130C.CtlTabIndex.RACK_YN
            .cmbShokaKbn.TabIndex = LMM130C.CtlTabIndex.SHOKA_KB
            .txtFctMgr.TabIndex = LMM130C.CtlTabIndex.FCT_MGR
            .cmbJisyatasyaKbn.TabIndex = LMM130C.CtlTabIndex.JISYA_TASYA_KB
            .numMxWt.TabIndex = LMM130C.CtlTabIndex.MAX_WT
            .txtTasyaWhNm.TabIndex = LMM130C.CtlTabIndex.TASYA_WH_NM
            .txtTasyaZip.TabIndex = LMM130C.CtlTabIndex.TASYA_ZIP
            .txtTasyaAd1.TabIndex = LMM130C.CtlTabIndex.TASYA_AD_1
            .txtTasyaAd2.TabIndex = LMM130C.CtlTabIndex.TASYA_AD_2
            .txtTasyaAd3.TabIndex = LMM130C.CtlTabIndex.TASYA_AD_3
            .numAreaRentHokanAmo.TabIndex = LMM130C.CtlTabIndex.AREA_RENT_HOKAN_AMO
            .txtUserCd.TabIndex = LMM130C.CtlTabIndex.USER_CD
            .txtUserCdSub.TabIndex = LMM130C.CtlTabIndex.USER_CD_SUB
            .cmbDokuKbn.TabIndex = LMM130C.CtlTabIndex.DOKU_KB
            .cmbGasKanriKbn.TabIndex = LMM130C.CtlTabIndex.GAS_KANRI_KB
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            .grpShinseigai.TabIndex = LMM130C.CtlTabIndex.EXP_JOHO
            .btnRowAdd_Shinseigai.TabIndex = LMM130C.CtlTabIndex.BTN_EXP_ADD
            .btnRowDel_Shinseigai.TabIndex = LMM130C.CtlTabIndex.BTN_EXP_DLL
            .sprDetail3.TabIndex = LMM130C.CtlTabIndex.SPR_TOU_SITU_EXP
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
            '棟室消防スプレッド
            .grpShoboJoho.TabIndex = LMM130C.CtlTabIndex.SHOBO_JOHO
            .btnRowAdd.TabIndex = LMM130C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM130C.CtlTabIndex.BTN_DETAIL
            .sprDetail2.TabIndex = LMM130C.CtlTabIndex.SPR_TOU_SITU_SHOBO
            .grpDoku.TabIndex = LMM130C.CtlTabIndex.DOKU_JOHO
            .btnRowAdd_Doku.TabIndex = LMM130C.CtlTabIndex.BTN_DOKU_ADD
            .btnRowDel_Doku.TabIndex = LMM130C.CtlTabIndex.BTN_DOKU_DEL
            .sprDetail4.TabIndex = LMM130C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_DOKU
            .GrpKouathuGas.TabIndex = LMM130C.CtlTabIndex.KOUATHUGAS_JOHO
            .btnRowAdd_KouathuGas.TabIndex = LMM130C.CtlTabIndex.BTN_KOUATHUGAS_ADD
            .btnRowDel_KouathuGas.TabIndex = LMM130C.CtlTabIndex.BTN_KOUATHUGAS_DEL
            .sprDetail5.TabIndex = LMM130C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_KOUATHUGAS
            .grpYakuzihoJoho.TabIndex = LMM130C.CtlTabIndex.YAKUZIHO_JOHO
            .btnRowAdd_Yakuziho.TabIndex = LMM130C.CtlTabIndex.BTN_YAKUZIHO_ADD
            .btnRowDel_Yakuziho.TabIndex = LMM130C.CtlTabIndex.BTN_YAKUZIHO_DEL
            .sprDetail6.TabIndex = LMM130C.CtlTabIndex.SPR_TOU_SITU_ZONE_CHK_YAKUZIHO
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.INIT, DispMode.VIEW
                    .sprDetail.Focus()

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                            '新規、複写時
                            .tabTouSitu.SelectedTab = .tpgTouSitu
                            .cmbWare.Focus()
                        Case RecordStatus.NOMAL_REC
                            '編集時
                            If .tabTouSitu.SelectedTab.Equals(.tpgTouSitu) Then
                                .txtTouSituNm.Focus()
                            Else
                                .sprDetail3.Focus()
                            End If
                    End Select
            End Select

        End With

    End Sub
    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

        Call Me.ChangeJisyaTasya()

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
            .numChozoMaxQty.Value = 0
            .numChozoMaxBaisu.Value = 0
            .numOndo.Value = 0
            .numMaxOndoUp.Value = 0
            .numMiniOndoDown.Value = 0
            .numCbm.Value = 0
            .numArea.Value = 0
            .numMxPltQt.Value = 0
            .numMxWt.Value = 0
            .numAreaRentHokanAmo.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

            .cmbNrsBrCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.NRS_BR_CD.ColNo))
            .cmbWare.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.WH_CD.ColNo))
            .txtTouNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TOU_NO.ColNo))
            .txtSituNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SITU_NO.ColNo))
            .txtTouSituNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TOU_SITU_NM.ColNo))
            .cmbSokoKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SOKO_KB.ColNo))
            .cmbHozeiKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.HOZEI_KB.ColNo))
            .numChozoMaxQty.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.CHOZO_MAX_QTY.ColNo))
            .numChozoMaxBaisu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.CHOZO_MAX_BAISU.ColNo))
            .cmbOndoCtlKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.ONDO_CTL_KB.ColNo))
            .numMaxOndoUp.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.MAX_ONDO_UP.ColNo))
            .numMiniOndoDown.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.MINI_ONDO_DOWN.ColNo))
            .numOndo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.ONDO_JITU.ColNo))
            .cmbOndoCtlFlg.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.ONDO_CTL_FLG.ColNo))
            .numCbm.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.CBM.ColNo))
            .numArea.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.AREA.ColNo))
            .txtHan.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.HAN.ColNo))
            .numMxPltQt.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.MX_PLT_QT.ColNo))
            .cmbRackYn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.RACK_YN.ColNo))
            .cmbShokaKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SHOKA_KB.ColNo))
            '要望番号：674 yamanaka 2012.7.5 Start
            .cmbJisyatasyaKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.JISYATASYA_KB.ColNo))
            '要望番号：674 yamanaka 2012.7.5 End
            .txtTasyaWhNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TASYA_WH_NM.ColNo))
            .txtTasyaZip.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TASYA_ZIP.ColNo))
            .txtTasyaAd1.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TASYA_AD_1.ColNo))
            .txtTasyaAd2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TASYA_AD_2.ColNo))
            .txtTasyaAd3.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.TASYA_AD_3.ColNo))
            .cmbDokuKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.DOKU_KB.ColNo))
            .cmbGasKanriKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.GAS_KANRI_KB.ColNo))
            .txtFctMgr.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.FCT_MGR.ColNo))
            .lblFctMgrNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.FCT_MGR_NM.ColNo))
            .numMxWt.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.MAX_WT.ColNo))
            .numAreaRentHokanAmo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.AREA_RENT_HOKAN_AMO.ColNo))
            '共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_ENT_USER_NM.ColNo))
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_ENT_DATE.ColNo)))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_UPD_USER_NM.ColNo))
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_UPD_DATE.ColNo)))
            '隠し項目                           
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_UPD_TIME.ColNo))
            .lblSysDelFlg.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.SYS_DEL_FLG.ColNo))
            .txtUserCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.USER_CD.ColNo))
            .lblUserNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.USER_nm.ColNo))
            .txtUserCdSub.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.USER_CD_SUB.ColNo))
            .lblUserNmSub.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.USER_NM_SUB.ColNo))
            .lblWhKb.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM130G.sprDetailDef.WH_KB.ColNo))

        End With

    End Sub

    ''' <summary>
    ''' 他社倉庫情報の入力可/不可判定
    ''' </summary>
    ''' <returns>True:入力可、False:入力不可</returns>
    Friend Function IsTasyaInfo() As Boolean

        With Me._Frm

            Select Case .lblWhKb.TextValue
                Case "01"
                    '倉庫が自社倉庫の場合
                    Select Case .cmbJisyatasyaKbn.SelectedValue.ToString()
                        Case "02"
                            '自社他社区分が他社倉庫の場合：他社倉庫情報の入力可
                            Return True

                        Case Else
                            '自社他社区分が自社倉庫または判別不能の場合：他社倉庫情報の入力不可
                            Return False
                    End Select

                Case Else
                    '倉庫が他社倉庫または判別不能の場合：他社倉庫情報の入力不可
                    Return False
            End Select

        End With

    End Function

    ''' <summary>
    ''' 倉庫の選択肢が変更された際の制御
    ''' </summary>
    ''' <param name="dsSokoJT">倉庫自社他社判定用データセット</param>
    Friend Sub ChangeWare(ByVal dsSokoJT As DataSet)

        With Me._Frm

            If Not DispMode.EDIT.Equals(.lblSituation.DispMode) Then
                '編集モード以外は抜ける
                Exit Sub
            End If

            '倉庫自社他社判定用データから該当倉庫レコードを取得
            Dim dr As DataRow() = dsSokoJT.Tables("LMM130_SOKO_JT").Select(String.Concat("WH_CD = '" & .cmbWare.SelectedValue.ToString) & "'")

            '倉庫区分を画面の隠し項目にセット
            If dr.Length = 0 Then
                .lblWhKb.TextValue = String.Empty
            Else
                .lblWhKb.TextValue = dr(0).Item("WH_KB").ToString()
            End If

        End With

        '自社他社区分の選択肢が変更された際の制御を呼ぶ
        Me.ChangeJisyaTasya()

    End Sub

    ''' <summary>
    ''' 自社他社区分の選択肢が変更された際の制御
    ''' </summary>
    Friend Sub ChangeJisyaTasya()

        With Me._Frm

            If Not DispMode.EDIT.Equals(.lblSituation.DispMode) Then
                '編集モード以外は抜ける
                Exit Sub
            End If

            '他社倉庫情報の入力可/不可判定
            Dim lock As Boolean = Not IsTasyaInfo()

            '入力有無判定によるコントロールの制御
            .txtTasyaWhNm.ReadOnly = lock
            .txtTasyaZip.ReadOnly = lock
            .txtTasyaAd1.ReadOnly = lock
            .txtTasyaAd2.ReadOnly = lock
            .txtTasyaAd3.ReadOnly = lock

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            '編集部
            .numChozoMaxQty.SetInputFields("###,###,##0.000", , 12, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numChozoMaxBaisu.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0))
            .numMaxOndoUp.SetInputFields("#0", , 2, 1, , 0, 0, , Convert.ToDecimal(99), Convert.ToDecimal(-99))
            .numMiniOndoDown.SetInputFields("#0", , 2, 1, , 0, 0, , Convert.ToDecimal(99), Convert.ToDecimal(-99))
            .numOndo.SetInputFields("#0", , 2, 1, , 0, 0, , Convert.ToDecimal(99), Convert.ToDecimal(-99))
            .numCbm.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0))
            .numArea.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0))
            .numMxPltQt.SetInputFields("#,##0", , 4, 1, , 0, 0, , Convert.ToDecimal(9999), Convert.ToDecimal(0))
            .numMxWt.SetInputFields("###,###,###,###,###,##0", , 18, 1, , 0, 0, , Convert.ToDecimal(999999999999999999), Convert.ToDecimal(0))
            .numAreaRentHokanAmo.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(0))
            '棟室消防スプレッド

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    '編集部の項目をクリア
                    Call Me.ClearControl(Me._Frm)
                    Me._Frm.sprDetail2.CrearSpread()
                    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
                    Me._Frm.sprDetail3.CrearSpread()
                    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
                    Me._Frm.sprDetail4.CrearSpread()
                    Me._Frm.sprDetail5.CrearSpread()
                    Me._Frm.sprDetail6.CrearSpread()

                Case DispMode.VIEW
                    'スプレッド(下部)をロックする
                    Me.SetLockBottomSpreadControl(True)
                    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
                    Me.SetLockLightSpreadControl(True)
                    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
                    Me.SetLockTouSituZoneChkSpreadControl(True)

                Case DispMode.EDIT

                    '行追加/削除ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel, unLock)

                    '編集部の項目のロック解除
                    Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)

                    '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
                    '申請外の商品保管許可ルールの行追加、行削除、一括登録ボタン、および適用日FROM、適用日TO、荷主は
                    '権限が管理者、システム管理者の場合は活性、そうでない場合は非活性を設定する
                    Dim kengen As String = LMUserInfoManager.GetAuthoLv
                    Dim kengenFlg As Boolean = Not (kengen.Equals(LMConst.AuthoKBN.LEADER) OrElse kengen.Equals(LMConst.AuthoKBN.MANAGER))
                    Call Me._ControlG.LockButton(.btnRowAdd_Shinseigai, kengenFlg)
                    Call Me._ControlG.LockButton(.btnRowDel_Shinseigai, kengenFlg)
                    Call Me._ControlG.LockButton(.btnIkkatuTouroku, kengenFlg)
                    Call Me._ControlG.LockDate(.imdSearchDate_From, kengenFlg)
                    Call Me._ControlG.LockDate(.imdSearchDate_To, kengenFlg)
                    Call Me._ControlG.LockText(.txtCustCD, kengenFlg)
                    '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
                    Call Me._ControlG.LockNumber(.numAreaRentHokanAmo, kengenFlg)

                    '棟室ゾーンチェックマスタスプレッド(3種) 行追加/削除ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd_Doku, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_Doku, unLock)

                    Call Me._ControlG.LockButton(.btnRowAdd_KouathuGas, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_KouathuGas, unLock)

                    Call Me._ControlG.LockButton(.btnRowAdd_Yakuziho, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_Yakuziho, unLock)

                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit()

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha()

                            '棟室消防情報Spreadの隠し項目である"更新区分"の設定
                            Dim RowCnt As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                '更新区分："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")
                            Next

                            '毒劇情報
                            RowCnt = .sprDetail4.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                '更新区分："0"を設定
                                .sprDetail4.SetCellValue(i, sprDetailDef4.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail4.SetCellValue(i, sprDetailDef4.SYS_DEL_FLG_T.ColNo, "0")
                            Next

                            '高圧ガス情報
                            RowCnt = .sprDetail5.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                '更新区分："0"を設定
                                .sprDetail5.SetCellValue(i, sprDetailDef5.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail5.SetCellValue(i, sprDetailDef5.SYS_DEL_FLG_T.ColNo, "0")
                            Next

                            '薬事法情報
                            RowCnt = .sprDetail6.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                '更新区分："0"を設定
                                .sprDetail6.SetCellValue(i, sprDetailDef6.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail6.SetCellValue(i, sprDetailDef6.SYS_DEL_FLG_T.ColNo, "0")
                            Next
                    End Select

                    '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
                    '申請外の商品保管許可ルール一覧に対し、権限が管理者、システム管理者の場合はロック解除を、そうでない場合はロックを行う
                    Me.SetLockLightSpreadControl(kengenFlg)
                    '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        With Me._Frm

            '編集部の項目をクリア
            Call Me.ClearControl(Me._Frm)
            .sprDetail2.CrearSpread()

            .sprDetail4.CrearSpread()
            .sprDetail5.CrearSpread()
            .sprDetail6.CrearSpread()

            '初期値を設定
            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd
            .cmbWare.SelectedValue = LMUserInfoManager.GetWhCd
            .cmbSokoKbn.SelectedValue = LMM130C.SOKO_KB      '11(普通倉庫)
            .cmbHozeiKbn.SelectedValue = LMM130C.ONDO_J      '01(普通倉庫)
            .cmbOndoCtlKbn.SelectedValue = LMM130C.ONDO_T    '02(定温)
            .cmbOndoCtlFlg.SelectedValue = LMM130C.ONDO_J    '01(温度管理中)
            .cmbRackYn.SelectedValue = LMM130C.ONDO_J        '01(有り)

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True

        With Me._Frm
            Call Me._ControlG.LockComb(.cmbWare, lock)         '倉庫
            Call Me._ControlG.LockText(.txtTouNo, lock)        '棟NO
            Call Me._ControlG.LockText(.txtSituNo, lock)       '室NO
        End With

        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        'スプレッド(右下部)をロック解除する
        Me.SetLockLightSpreadControl(False)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        Me.SetLockTouSituZoneChkSpreadControl(False)

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm
            '複写しない項目は空を設定
            .txtTouNo.TextValue = String.Empty
            .txtSituNo.TextValue = String.Empty
            .txtTouSituNm.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
        End With

        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)

        Me.SetLockTouSituZoneChkSpreadControl(False)

    End Sub

    ''' <summary>
    ''' スプレッド(下部)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockBottomSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2)

            Dim max As Integer = .sprDetail2.ActiveSheet.Rows.Count
            For i As Integer = 1 To max

                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.SHOBO_CD.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.HINMEI.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, lbl)
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 999999.99, lockFlg, 2, , ","))
                '.sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 9999.99, lockFlg, 2, , ","))
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
                .sprDetail2.SetCellStyle((i - 1), LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail2, lockFlg))
            Next

        End With

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' スプレッド(右下部)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockLightSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail3)

            Dim max As Integer = .sprDetail3.ActiveSheet.Rows.Count
            For i As Integer = 1 To max
                .sprDetail3.SetCellStyle((i - 1), LMM130G.sprDetailDef3.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg))
                .sprDetail3.SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail3, lockFlg))
                .sprDetail3.SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail3, lockFlg))
                .sprDetail3.SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_CODE.ColNo, lbl)
                .sprDetail3.SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_NM.ColNo, lbl)
            Next

        End With

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 棟室ゾーンチェックマスタスプレッド(3種)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockTouSituZoneChkSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            '毒劇情報
            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail4)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail4, LMM130C.M_Z_KBN_DOKUGEKI, lockFlg)

            Dim max As Integer = .sprDetail4.ActiveSheet.Rows.Count
            For i As Integer = 1 To max
                .sprDetail4.SetCellStyle((i - 1), LMM130G.sprDetailDef4.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail4, lockFlg))
                .sprDetail4.SetCellStyle((i - 1), LMM130G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
            Next

            '高圧ガス情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetail5)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetail5, LMM130C.M_Z_KBN_KOUATHUGAS, lockFlg)

            max = .sprDetail5.ActiveSheet.Rows.Count
            For i As Integer = 1 To max

                .sprDetail5.SetCellStyle((i - 1), LMM130G.sprDetailDef5.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail5, lockFlg))
                .sprDetail5.SetCellStyle((i - 1), LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
            Next

            '薬事法情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetail6)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetail6, LMM130C.M_Z_KBN_YAKUZIHO, lockFlg)

            max = .sprDetail6.ActiveSheet.Rows.Count
            For i As Integer = 1 To max

                .sprDetail6.SetCellStyle((i - 1), LMM130G.sprDetailDef6.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail6, lockFlg))
                .sprDetail6.SetCellStyle((i - 1), LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
            Next

        End With

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)     '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)           '営業所名
        Public Shared SOKO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SOKO, "倉庫", 275, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SITU_NO, "室", 30, True)
        Public Shared TOU_SITU_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TOU_SITU_NM, "棟室名", 200, True)
        Public Shared HOZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.HOZEI_KB_NM, "保税区分", 80, True)
        Public Shared ONDO_CTL_KB_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO_CTL_KB_NM, "温度管理区分", 100, True)
        Public Shared ONDO_CTL_FLG_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO_CTL_FLG_NM, "温度管理中", 90, True)
        Public Shared ONDO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO, "設定" & vbCrLf & "温度(℃)", 80, True)

        '隠し項目
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.WH_CD, "倉庫コード", 60, False)
        Public Shared SOKO_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SOKO_KB, "倉庫区分", 60, False)
        Public Shared HOZEI_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.HOZEI_KB, "保税区分", 60, False)
        Public Shared CHOZO_MAX_QTY As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.CHOZO_MAX_QTY, "貯蔵最大数量", 60, False)
        Public Shared CHOZO_MAX_BAISU As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.CHOZO_MAX_BAISU, "貯蔵最大倍数", 60, False)
        Public Shared ONDO_CTL_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO_CTL_KB, "温度管理区分", 60, False)
        Public Shared MAX_ONDO_UP As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.MAX_ONDO_UP, "最高設定温度上限", 60, False)
        Public Shared MINI_ONDO_DOWN As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.MINI_ONDO_DOWN, "最低設定温度下限", 60, False)
        Public Shared ONDO_CTL_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO_CTL_FLG, "温度管理中フラグ", 60, False)
        Public Shared HAN As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.HAN, "担当作業班", 60, False)
        Public Shared CBM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.CBM, "棟室容積", 60, False)
        Public Shared AREA As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.AREA, "床面積（ｍ２）", 60, False)
        Public Shared MX_PLT_QT As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.MX_PLT_QT, "最大収納パレット数", 60, False)
        Public Shared RACK_YN As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.RACK_YN, "ラック設備有無", 60, False)
        Public Shared FCT_MGR As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.FCT_MGR, "保安監督者名", 60, False)
        Public Shared FCT_MGR_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.FCT_MGR_NM, "保安監督者名", 60, False)
        Public Shared SHOKA_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SHOKA_KB, "消化設備区分", 60, False)
        '要望番号：674 yamanaka 2012.7.5 Start
        Public Shared JISYATASYA_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.JISYATASYA_KB, "自社他社区分", 60, False)
        '要望番号：674 yamanaka 2012.7.5 End
        Public Shared DOKU_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.DOKU_KB, "毒劇区分", 60, False)
        Public Shared GAS_KANRI_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.GAS_KANRI_KB, "ガス管理区分", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared ONDO_JITU As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.ONDO_JITU, "設定" & vbCrLf & "温度(℃)", 60, False)
        Public Shared MAX_WT As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.MAX_WT, "最大重量", 60, False)
        Public Shared USER_CD As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.USER_CD, "主担当作業者CD", 60, False)
        Public Shared USER_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.USER_NM, "主担当作業者名", 60, False)
        Public Shared USER_CD_SUB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.USER_CD_SUB, "副担当作業者CD", 60, False)
        Public Shared USER_NM_SUB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.USER_NM_SUB, "副担当作業者名", 60, False)
        Public Shared WH_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.WH_KB, "倉庫区分(倉庫)", 60, False)
        Public Shared TASYA_WH_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TASYA_WH_NM, "他社倉庫名称", 60, False)
        Public Shared TASYA_ZIP As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TASYA_ZIP, "他社倉庫郵便番号", 60, False)
        Public Shared TASYA_AD_1 As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TASYA_AD_1, "他社倉庫住所1", 60, False)
        Public Shared TASYA_AD_2 As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TASYA_AD_2, "他社倉庫住所2", 60, False)
        Public Shared TASYA_AD_3 As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.TASYA_AD_3, "他社倉庫住所3", 60, False)
        Public Shared AREA_RENT_HOKAN_AMO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex.AREA_RENT_HOKAN_AMO, "坪貸し保管料(統計用)", 60, False)
    End Class


    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.SHOBO_CD, "消防" & vbCrLf & "コード", 80, True)
        Public Shared HINMEI As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.HINMEI, "品名", 150, True)
        Public Shared KIKEN_TOKYU As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.KIKEN_TOKYU, "危険等級", 80, True)
        Public Shared KIKEN_SYU As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.KIKEN_SYU, "危険種別", 80, True)
        Public Shared BAISU As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.BAISU, "倍数", 80, True)
        Public Shared WH_KYOKA_DATE As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.WH_KYOKA_DATE, "許可日", 90, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex2.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' スプレッド列定義体(右下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef3

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.DEF, " ", 20, True)
        Public Shared APPLICATION_DATE_FROM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM, "適用日FROM", 90, True)
        Public Shared APPLICATION_DATE_TO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.APPLICATION_DATE_TO, "適用日TO", 90, True)
        Public Shared CUST_CODE As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.CUST_CD, "荷主CD", 80, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.CUST_NM, "荷主名", 200, True)
        '隠し項目
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.SERIAL_NO, "シリアルNo", 80, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex3.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class
    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' スプレッド列定義体(毒劇情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef4

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex4.DEF, " ", 20, True)
        Public Shared DOKU_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex4.DOKU_KB, "毒劇区分", 114, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex4.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex4.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(高圧ガス情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef5

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex5.DEF, " ", 20, True)
        Public Shared KOUATHUGAS_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex5.KOUATHUGAS_KB, "高圧ガス区分", 304, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex5.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex5.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(薬事法情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef6

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex6.DEF, " ", 20, True)
        Public Shared YAKUZIHO_KB As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex6.YAKUZIHO_KB, "薬機法区分", 184, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex6.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM130C.SprColumnIndex6.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '棟室Spreadの初期化処理
        Call Me.InitTouSituSpread()

        '消防Spreadの初期化処理
        Call Me.InitShoboSpread()

        '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        Call Me.InitHokanSpread()
        '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add END 

        '毒劇情報Spreadの初期化処理
        Call Me.InitDokuSpread()

        '高圧ガス情報Spreadの初期化処理
        Call Me.InitKouathuGasSpread()

        '薬事法情報Spreadの初期化処理
        Call Me.InitYakuzihoSpread()

    End Sub

    ''' <summary>
    ''' 棟室スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitTouSituSpread()

        '商品Spreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail
        Dim dr As DataRow
        With sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM130C.SprColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMM130G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprDetailDef.DEF.ColNo + 1

            '温度管理中の区分値(○・×)を設定
            Dim umuStyle As StyleInfo = Me.StyleInfoCustCond(sprDetail)

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail)

            '**** 表示列 ****
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, "S051", False))
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, lbl)

            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)
            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If

            .SetCellStyle(0, sprDetailDef.SOKO.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            .SetCellStyle(0, sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'START YANAI 要望番号705
            '.SetCellStyle(0, sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.HAN_NUM_ALPHA, 1, False))
            .SetCellStyle(0, sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'END YANAI 要望番号705
            .SetCellStyle(0, sprDetailDef.TOU_SITU_NM.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.HOZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_H001, False))
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_O002, False))
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_FLG_NM.ColNo, umuStyle)
            .SetCellStyle(0, sprDetailDef.ONDO.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprDetailDef.WH_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SOKO_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.HOZEI_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CHOZO_MAX_QTY.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CHOZO_MAX_BAISU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MAX_ONDO_UP.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MINI_ONDO_DOWN.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.HAN.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CBM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.AREA.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MX_PLT_QT.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.RACK_YN.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FCT_MGR.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FCT_MGR_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SHOKA_KB.ColNo, lbl)
            '要望番号：674 yamanaka 2012.7.5 Start
            .SetCellStyle(0, sprDetailDef.JISYATASYA_KB.ColNo, lbl)
            '要望番号：674 yamanaka 2012.7.5 End
            .SetCellStyle(0, sprDetailDef.DOKU_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.GAS_KANRI_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ONDO_JITU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MAX_WT.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.USER_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.USER_CD_SUB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.USER_NM_SUB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.WH_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TASYA_WH_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TASYA_ZIP.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TASYA_AD_1.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TASYA_AD_2.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TASYA_AD_3.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.AREA_RENT_HOKAN_AMO.ColNo, lbl)
            '初期値設定
            Call Me._ControlG.ClearControl(sprDetail)
            .SetCellValue(0, sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())

        End With

    End Sub

    ''' <summary>
    ''' 消防スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitShoboSpread()

        '消防Spreadの初期値設定
        Dim sprDetail2 As LMSpread = Me._Frm.sprDetail2

        With sprDetail2

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 9

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef2)
            .SetColProperty(New LMM130G.sprDetailDef2(), False)
            '2015.10.15 英語化対応END

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail2)

            '**** 表示列 ****
            .SetCellStyle(-1, sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail2, False))
            .SetCellStyle(-1, sprDetailDef2.SHOBO_CD.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef2.HINMEI.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef2.KIKEN_TOKYU.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef2.KIKEN_SYU.ColNo, lbl)
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
            .SetCellStyle(-1, sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(sprDetail2, 0, 999999.99, False, 2, , ","))
            '.SetCellStyle(-1, sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(sprDetail2, 0, 9999.99, False, 2, , ","))
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
            .SetCellStyle(-1, sprDetailDef2.WH_KYOKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(sprDetail2, False))
            .SetCellStyle(-1, sprDetailDef2.UPD_FLG.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef2.SYS_DEL_FLG_T.ColNo, lbl)


        End With

    End Sub

    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 商品保管許可ルールスプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitHokanSpread()

        'スプレッドの初期値設定
        Dim sprDetail3 As LMSpread = Me._Frm.sprDetail3

        With sprDetail3

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 8

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM130G.sprDetailDef3(), False)
            '2015.10.15 英語化対応END

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail3)

            '**** 表示列 ****
            .SetCellStyle(-1, sprDetailDef3.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail3, False))
            .SetCellStyle(-1, sprDetailDef3.APPLICATION_DATE_FROM.ColNo, LMSpreadUtility.GetDateTimeCell(sprDetail3, False))
            .SetCellStyle(-1, sprDetailDef3.APPLICATION_DATE_TO.ColNo, LMSpreadUtility.GetDateTimeCell(sprDetail3, False))
            .SetCellStyle(-1, sprDetailDef3.CUST_CODE.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef3.CUST_NM.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef3.SERIAL_NO.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(-1, sprDetailDef3.UPD_FLG.ColNo, lbl)
            .SetCellStyle(-1, sprDetailDef3.SYS_DEL_FLG_T.ColNo, lbl)

            'セル移動方法設定
            '編集中でないとき
            Dim im As New FarPoint.Win.Spread.InputMap
            im = .GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            'タブ
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
            '方向キー
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRowWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRowWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

            '編集中のとき
            im = .GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
            'タブ
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Tab, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
            '方向キー
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRowWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRowWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        End With

    End Sub

    ''' <summary>
    ''' 毒劇情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitDokuSpread()

        '毒劇情報Spreadの初期値設定
        Dim sprDetail4 As LMSpread = Me._Frm.sprDetail4

        With sprDetail4

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM130C.SprColumnIndex4.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM130G.sprDetailDef4(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef4.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail4, False))
            .SetCellStyle(-1, sprDetailDef4.DOKU_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail4, LMM130C.M_Z_KBN_DOKUGEKI, False))

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitKouathuGasSpread()

        '高圧ガス情報Spreadの初期値設定
        Dim sprDetail5 As LMSpread = Me._Frm.sprDetail5

        With sprDetail5

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM130C.SprColumnIndex5.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM130G.sprDetailDef5(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef5.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail5, False))
            .SetCellStyle(-1, sprDetailDef5.KOUATHUGAS_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail5, LMM130C.M_Z_KBN_KOUATHUGAS, False))

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitYakuzihoSpread()

        '薬事法情報Spreadの初期値設定
        Dim sprDetail6 As LMSpread = Me._Frm.sprDetail6

        With sprDetail6

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM130C.SprColumnIndex6.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM130G.sprDetailDef6(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef6.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail6, False))
            .SetCellStyle(-1, sprDetailDef6.YAKUZIHO_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail6, LMM130C.M_Z_KBN_YAKUZIHO, False))

        End With

    End Sub

    Friend Sub SetItemIniValue(ByVal frm As LMM130F)
        '適用日Fromの設定
        frm.imdSearchDate_From.TextValue = Now().ToString("yyyyMMdd")

        '適用期間の設定
        Dim period As Integer = Convert.ToInt32(Convert.ToDouble(
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'B032' AND KBN_CD = '1'")(0).Item("VALUE1")))

        '適用日Toの設定
        frm.imdSearchDate_To.TextValue = Convert.ToDateTime(Now()).AddDays(period).ToString("yyyyMMdd")


    End Sub

    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM130F)

        With frm.sprDetail

            '.Sheets(0).Cells(0, sprDetailDef.DEF.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.JYOTAI.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.CUST_NM_M.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.CUST_GOODS_CD.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.GOODS_NM.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.LOT_NO.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.DEST_CD.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            '.Sheets(0).Cells(0, sprDetailDef.COA_LINK.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        '値設定用の変数
        Dim ondo As String = String.Empty

        With spr

            .SuspendLayout()
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
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -99, 99, True, 2, True, ",")

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)
                '値設定用変数の初期化
                ondo = String.Empty

                '温度管理フラグによる現在設定温度の表示制御                
                If (LMM130C.ONDO_J).Equals(dr.Item("ONDO_CTL_FLG").ToString()) = True Then
                    ondo = dr.Item("ONDO").ToString()
                End If

                'セルスタイル設定
                .SetCellStyle(i, LMM130G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SOKO.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TOU_SITU_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.HOZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.ONDO.ColNo, sNumber)
                .SetCellStyle(i, LMM130G.sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SOKO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.HOZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.CHOZO_MAX_QTY.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.CHOZO_MAX_BAISU.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.ONDO_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.MAX_ONDO_UP.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.MINI_ONDO_DOWN.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.ONDO_CTL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.HAN.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.CBM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.AREA.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.MX_PLT_QT.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.RACK_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.FCT_MGR.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.FCT_MGR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SHOKA_KB.ColNo, sLabel)
                '要望番号：674 yamanaka 2012.7.5 Start
                .SetCellStyle(i, LMM130G.sprDetailDef.JISYATASYA_KB.ColNo, sLabel)
                '要望番号：674 yamanaka 2012.7.5 End
                .SetCellStyle(i, LMM130G.sprDetailDef.DOKU_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.GAS_KANRI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.MAX_WT.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.USER_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.USER_CD_SUB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.USER_NM_SUB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.WH_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TASYA_WH_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TASYA_ZIP.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TASYA_AD_1.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TASYA_AD_2.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.TASYA_AD_3.ColNo, sLabel)
                .SetCellStyle(i, LMM130G.sprDetailDef.AREA_RENT_HOKAN_AMO.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM130G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SOKO.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TOU_SITU_NM.ColNo, dr.Item("TOU_SITU_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.HOZEI_KB_NM.ColNo, dr.Item("HOZEI_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, dr.Item("ONDO_CTL_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo, dr.Item("ONDO_CTL_FLG_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO.ColNo, ondo)
                .SetCellValue(i, LMM130G.sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SOKO_KB.ColNo, dr.Item("SOKO_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.HOZEI_KB.ColNo, dr.Item("HOZEI_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.CHOZO_MAX_QTY.ColNo, dr.Item("CHOZO_MAX_QTY").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.CHOZO_MAX_BAISU.ColNo, dr.Item("CHOZO_MAX_BAISU").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO_CTL_KB.ColNo, dr.Item("ONDO_CTL_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.MAX_ONDO_UP.ColNo, dr.Item("MAX_ONDO_UP").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.MINI_ONDO_DOWN.ColNo, dr.Item("MINI_ONDO_DOWN").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO_CTL_FLG.ColNo, dr.Item("ONDO_CTL_FLG").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.HAN.ColNo, dr.Item("HAN").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.CBM.ColNo, dr.Item("CBM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.AREA.ColNo, dr.Item("AREA").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.MX_PLT_QT.ColNo, dr.Item("MX_PLT_QT").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.RACK_YN.ColNo, dr.Item("RACK_YN").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.FCT_MGR.ColNo, dr.Item("FCT_MGR").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.FCT_MGR_NM.ColNo, dr.Item("FCT_MGR_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SHOKA_KB.ColNo, dr.Item("SHOKA_KB").ToString())
                '要望番号：674 yamanaka 2012.7.5 Start
                .SetCellValue(i, LMM130G.sprDetailDef.JISYATASYA_KB.ColNo, dr.Item("JISYATASYA_KB").ToString())
                '要望番号：674 yamanaka 2012.7.5 End
                .SetCellValue(i, LMM130G.sprDetailDef.DOKU_KB.ColNo, dr.Item("DOKU_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.GAS_KANRI_KB.ColNo, dr.Item("GAS_KANRI_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.MAX_WT.ColNo, dr.Item("MAX_CAPA_KG_QTY").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.ONDO_JITU.ColNo, dr.Item("ONDO").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.USER_CD.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.USER_NM.ColNo, dr.Item("USER_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.USER_CD_SUB.ColNo, dr.Item("USER_CD_SUB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.USER_NM_SUB.ColNo, dr.Item("USER_NM_SUB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.WH_KB.ColNo, dr.Item("WH_KB").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TASYA_WH_NM.ColNo, dr.Item("TASYA_WH_NM").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TASYA_ZIP.ColNo, dr.Item("TASYA_ZIP").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TASYA_AD_1.ColNo, dr.Item("TASYA_AD_1").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TASYA_AD_2.ColNo, dr.Item("TASYA_AD_2").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.TASYA_AD_3.ColNo, dr.Item("TASYA_AD_3").ToString())
                .SetCellValue(i, LMM130G.sprDetailDef.AREA_RENT_HOKAN_AMO.ColNo, dr.Item("AREA_RENT_HOKAN_AMO").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,SHOBO_CD ASC"

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND  ", "SITU_NO = '", situno, "'"), sort)

        With spr2

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0, 999999.99, False, 2, , ",")
            'Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0, 9999.99, False, 2, , ",")
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr2, False)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.HINMEI.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, sNumber)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, sDate)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.HINMEI.ColNo, dr.Item("HINMEI").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, dr.Item("TOKYU").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, dr.Item("SHUBETU").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, dr.Item("BAISU").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("WH_KYOKA_DATE").ToString()))
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread3(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr3 As LMSpread = Me._Frm.sprDetail3
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,CUST_CD_L ASC,APL_DATE_FROM DESC"

        Dim tmpDatarow3() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND  ", "SITU_NO = '", situno, "'"), sort)

        With spr3

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow3.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr3, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr3, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr3, 0, 9999.99, False, 2, , ",")
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr3, False)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow3(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, sDate)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, sDate)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_CODE.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_NM.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.SERIAL_NO.ColNo, sNumber)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, DateFormatUtility.EditSlash(dr.Item("APL_DATE_FROM").ToString()))
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, DateFormatUtility.EditSlash(dr.Item("APL_DATE_TO").ToString()))
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.CUST_CODE.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread4(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr4 As LMSpread = Me._Frm.sprDetail4
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow4() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND  ", "SITU_NO = '", situno, "'", "AND ZONE_CD = '' AND KBN_GROUP_CD = '", LMM130C.M_Z_KBN_DOKUGEKI, "'"), sort)

        With spr4

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow4.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr4, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr4, LMM130C.M_Z_KBN_DOKUGEKI, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr4, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow4(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM130G.sprDetailDef4.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef4.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM130G.sprDetailDef4.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM130G.sprDetailDef4.DOKU_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef4.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread5(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr5 As LMSpread = Me._Frm.sprDetail5
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow5() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND  ", "SITU_NO = '", situno, "'", " AND ZONE_CD = '' AND KBN_GROUP_CD = '", LMM130C.M_Z_KBN_KOUATHUGAS, "'"), sort)

        With spr5

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow5.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr5, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr5, LMM130C.M_Z_KBN_KOUATHUGAS, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr5, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow5(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM130G.sprDetailDef5.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef5.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM130G.sprDetailDef5.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef5.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread6(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr6 As LMSpread = Me._Frm.sprDetail6
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SITU_NO ASC,ZONE_CD ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow6() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND  ", "SITU_NO = '", situno, "'", " AND ZONE_CD = '' AND KBN_GROUP_CD = '", LMM130C.M_Z_KBN_YAKUZIHO, "'"), sort)

        With spr6

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow6.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr6, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr6, LMM130C.M_Z_KBN_YAKUZIHO, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr6, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow6(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM130G.sprDetailDef6.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef6.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM130G.sprDetailDef6.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef6.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread2(ByVal dt As DataTable)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        With spr2

            .SuspendLayout()

            For i As Integer = 0 To dt.Rows.Count - 1

                .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

                'セルに設定するスタイルの取得
                Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
                Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
                Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0, 999999.99, False, 2, , ",")
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
                Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr2, False)

                sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

                Dim dr As DataRow = Nothing

                '値設定(消防マスタ照会POP)
                dr = dt.Rows(i)

                Dim row As Integer = .Sheets(0).Rows.Count - 1

                'セルスタイル設定
                .SetCellStyle(row, LMM130G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle(row, LMM130G.sprDetailDef2.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle(row, LMM130G.sprDetailDef2.HINMEI.ColNo, sLabel)
                .SetCellStyle(row, LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, sLabel)
                .SetCellStyle(row, LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, sLabel)
                .SetCellStyle(row, LMM130G.sprDetailDef2.BAISU.ColNo, sNumber)
                .SetCellStyle(row, LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, sDate)
                .SetCellStyle(row, LMM130G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                .SetCellStyle(row, LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(row, LMM130G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(row, LMM130G.sprDetailDef2.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue(row, LMM130G.sprDetailDef2.HINMEI.ColNo, dr.Item("HINMEI").ToString())
                .SetCellValue(row, LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, dr.Item("KIKEN_TOKYU_NM").ToString())
                .SetCellValue(row, LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, dr.Item("SYU_NM").ToString())
                .SetCellValue(row, LMM130G.sprDetailDef2.BAISU.ColNo, String.Empty)
                .SetCellValue(row, LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, String.Empty)
                .SetCellValue(row, LMM130G.sprDetailDef2.UPD_FLG.ColNo, "0")
                .SetCellValue(row, LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")
            Next

            .ResumeLayout(True)

        End With

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread3(ByVal dt As DataTable)

        Dim spr3 As LMSpread = Me._Frm.sprDetail3
        Dim dtOut As New DataSet

        With spr3

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr3, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr3, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr3, 0, 9999.99, False, 2, , ",")
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr3, False)
            '適用期間の設定
            Dim period As Integer = Convert.ToInt32(Convert.ToDouble(
                                    MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                    .Select("KBN_GROUP_CD = 'B032' AND KBN_CD = '1'")(0).Item("VALUE1")))

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定(申請外の商品保管ルール POP)
            dr = dt.Rows(0)

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM130G.sprDetailDef3.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, sDate)
            .SetCellStyle(row, LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, sDate)
            .SetCellStyle(row, LMM130G.sprDetailDef3.CUST_CODE.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef3.CUST_NM.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef3.SERIAL_NO.ColNo, sNumber)
            .SetCellStyle(row, LMM130G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM130G.sprDetailDef3.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, Now().ToString("yyyy/MM/dd"))
            .SetCellValue(row, LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, Convert.ToDateTime(Now()).AddDays(period).ToString("yyyy/MM/dd"))
            .SetCellValue(row, LMM130G.sprDetailDef3.CUST_CODE.ColNo, dr.Item("CUST_CD_L").ToString())
            .SetCellValue(row, LMM130G.sprDetailDef3.CUST_NM.ColNo, dr.Item("CUST_NM_L").ToString())
            .SetCellValue(row, LMM130G.sprDetailDef3.SERIAL_NO.ColNo, String.Empty)
            .SetCellValue(row, LMM130G.sprDetailDef3.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread4()

        Dim spr4 As LMSpread = Me._Frm.sprDetail4

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With spr4

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr4, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr4, LMM130C.M_Z_KBN_DOKUGEKI, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr4, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM130G.sprDetailDef4.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM130G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM130G.sprDetailDef4.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM130G.sprDetailDef4.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM130G.sprDetailDef4.DOKU_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM130G.sprDetailDef4.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread5()

        Dim spr5 As LMSpread = Me._Frm.sprDetail5

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With spr5

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr5, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr5, LMM130C.M_Z_KBN_KOUATHUGAS, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr5, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM130G.sprDetailDef5.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM130G.sprDetailDef5.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM130G.sprDetailDef5.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM130G.sprDetailDef5.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread6()

        Dim spr6 As LMSpread = Me._Frm.sprDetail6

        Dim unlock As Boolean = False
        Dim lock As Boolean = True

        With spr6

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr6, False)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr6, LMM130C.M_Z_KBN_YAKUZIHO, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr6, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM130G.sprDetailDef6.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM130G.sprDetailDef6.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM130G.sprDetailDef6.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM130G.sprDetailDef6.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行削除時(明細)
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
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.99, False, 2, , ",")
            'Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999.99, False, 2, , ",")
            '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
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
                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo))) = False Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.DEF.ColNo, sDEF)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.SHOBO_CD.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.HINMEI.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, sNumber)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, sDate)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.SHOBO_CD.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SHOBO_CD.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.HINMEI.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.HINMEI.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.KIKEN_TOKYU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.KIKEN_TOKYU.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.KIKEN_SYU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.KIKEN_SYU.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.BAISU.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.BAISU.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo, DateFormatUtility.EditSlash(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.WH_KYOKA_DATE.ColNo)).ToString()))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.UPD_FLG.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.UPD_FLG.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    Friend Sub ReSetExpSpread(ByVal spr As LMSpread)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999.99, False, 2, , ",")
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
                If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.SYS_DEL_FLG_T.ColNo))) = False Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.DEF.ColNo, sDEF)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, sDate)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, sDate)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_CODE.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.CUST_NM.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.SERIAL_NO.ColNo, sNumber)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo, DateFormatUtility.EditSlash(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.APPLICATION_DATE_FROM.ColNo))))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo, DateFormatUtility.EditSlash(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.APPLICATION_DATE_TO.ColNo))))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.CUST_CODE.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.CUST_CODE.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.CUST_NM.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.CUST_NM.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.SERIAL_NO.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.SERIAL_NO.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.UPD_FLG.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.UPD_FLG.ColNo)))
                    .SetCellValue((i - 1), LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef3.SYS_DEL_FLG_T.ColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    ''' <summary>
    ''' 棟室ゾーンチェックマスタスプレッドスプレッドにデータを設定_行削除時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetTouSituZoneChkSpread(ByVal spr As LMSpread)

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sComboKbn As StyleInfo
            Dim sKbnGroupCode As String = ""
            Dim iColNoDef As Integer = 0
            Dim iColNoCombo As Integer = 0
            Dim iColNoUpdFlg As Integer = 0
            Dim iColNoSysDelFlg As Integer = 0
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Select Case spr.Name
                Case "sprDetail4"
                    sKbnGroupCode = LMM130C.M_Z_KBN_DOKUGEKI
                    iColNoDef = LMM130G.sprDetailDef4.DEF.ColNo
                    iColNoCombo = LMM130G.sprDetailDef4.DOKU_KB.ColNo
                    iColNoUpdFlg = LMM130G.sprDetailDef4.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo

                Case "sprDetail5"
                    sKbnGroupCode = LMM130C.M_Z_KBN_KOUATHUGAS
                    iColNoDef = LMM130G.sprDetailDef5.DEF.ColNo
                    iColNoCombo = LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo
                    iColNoUpdFlg = LMM130G.sprDetailDef5.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo

                Case "sprDetail6"
                    sKbnGroupCode = LMM130C.M_Z_KBN_YAKUZIHO
                    iColNoDef = LMM130G.sprDetailDef6.DEF.ColNo
                    iColNoCombo = LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo
                    iColNoUpdFlg = LMM130G.sprDetailDef6.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo

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
                If (LMConst.FLG.OFF).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), iColNoSysDelFlg))) = True Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), iColNoDef, sDEF)
                    .SetCellStyle((i - 1), iColNoCombo, sComboKbn)
                    .SetCellStyle((i - 1), iColNoUpdFlg, sLabel)
                    .SetCellStyle((i - 1), iColNoSysDelFlg, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), iColNoDef, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), iColNoCombo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), iColNoCombo)))
                    .SetCellValue((i - 1), iColNoUpdFlg, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), iColNoUpdFlg)))
                    .SetCellValue((i - 1), iColNoSysDelFlg, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), iColNoSysDelFlg)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定
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
                                                  , New String() {LMKbnConst.KBN_O004}
                                                  )

    End Function

#End Region 'Spread

#Region "Spread(ADD/DEL)"

    ''' <summary>
    ''' 棟室マスタ消防Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTouSituShobo(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            If ("sprDetail2").Equals(spr.Name) = True Then
                For i As Integer = 0 To max

                    If i > max Then
                        Exit For
                    End If

                    If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.DEF.ColNo))) = True Then

                        If ("1").Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.UPD_FLG.ColNo))) = True Then
                            '既に登録済みのデータの場合は、削除フラグを"1"に変更
                            .SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMConst.FLG.ON)
                        Else
                            '上記以外(新規追加データ)の場合は、スプレッドから行削除
                            .ActiveSheet.RemoveRows(i, 1)

                            '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                            i = i - 1
                            max = max - 1
                        End If

                    End If
                Next

            End If

        End With

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 申請外の商品保管ルールSpreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTouSituExp(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            If ("sprDetail3").Equals(spr.Name) = True Then
                For i As Integer = 0 To max

                    If i > max Then
                        Exit For
                    End If

                    If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef3.DEF.ColNo))) = True Then

                        If ("1").Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef3.UPD_FLG.ColNo))) = True Then
                            '既に登録済みのデータの場合は、削除フラグを"1"に変更
                            .SetCellValue(i, sprDetailDef3.SYS_DEL_FLG_T.ColNo, LMConst.FLG.ON)
                        Else
                            '上記以外(新規追加データ)の場合は、スプレッドから行削除
                            .ActiveSheet.RemoveRows(i, 1)

                            '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                            i = i - 1
                            max = max - 1
                        End If

                    End If
                Next

            End If

        End With

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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

#End Region

#End Region

End Class
