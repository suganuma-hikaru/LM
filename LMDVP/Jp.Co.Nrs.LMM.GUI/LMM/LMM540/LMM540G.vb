' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM540G : 棟マスタメンテナンス
'  作  成  者     : [narita]
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
''' LMM540Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM540G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM540F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM540F, ByVal g As LMMControlG)

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
            .F10ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

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
            .sprDetail.TabIndex = LMM540C.CtlTabIndex.TOU
            '編集部
            .cmbNrsBrCd.TabIndex = LMM540C.CtlTabIndex.NRS_BR_CD
            .cmbWare.TabIndex = LMM540C.CtlTabIndex.WH_CD
            .txtTouNo.TabIndex = LMM540C.CtlTabIndex.TOU_NO
            .txtTouNm.TabIndex = LMM540C.CtlTabIndex.TOU_NM
            .cmbSokoKbn.TabIndex = LMM540C.CtlTabIndex.SOKO_KB
            .cmbHozeiKbn.TabIndex = LMM540C.CtlTabIndex.HOZEI_KB
            .numChozoMaxQty.TabIndex = LMM540C.CtlTabIndex.CHOZO_MAX_QTY
            .numChozoMaxBaisu.TabIndex = LMM540C.CtlTabIndex.CHOZO_MAX_BAISU
            .cmbOndoKbn.TabIndex = LMM540C.CtlTabIndex.ONDO_CTL_KB
            .numArea.TabIndex = LMM540C.CtlTabIndex.AREA
            .txtFctMgr.TabIndex = LMM540C.CtlTabIndex.FCT_MGR
            .lblFctMgrNm.TabIndex = LMM540C.CtlTabIndex.FCT_MGR_NM
            '棟消防スプレッド
            .grpShoboJoho.TabIndex = LMM540C.CtlTabIndex.SHOBO_JOHO
            .btnRowAdd.TabIndex = LMM540C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM540C.CtlTabIndex.BTN_DETAIL
            .chkShobo.TabIndex = LMM540C.CtlTabIndex.SHOBO_HAIKA_CHK
            .sprDetail2.TabIndex = LMM540C.CtlTabIndex.SPR_TOU_SHOBO
            '棟毒劇スプレッド
            .grpDoku.TabIndex = LMM540C.CtlTabIndex.DOKU_JOHO
            .btnRowAdd_Doku.TabIndex = LMM540C.CtlTabIndex.BTN_DOKU_ADD
            .btnRowDel_Doku.TabIndex = LMM540C.CtlTabIndex.BTN_DOKU_DEL
            .chkDoku.TabIndex = LMM540C.CtlTabIndex.DOKU_HAIKA_CHK
            .sprDetail4.TabIndex = LMM540C.CtlTabIndex.SPR_TOU_CHK_DOKU
            '棟高圧ガススプレッド
            .GrpKouathuGas.TabIndex = LMM540C.CtlTabIndex.KOUATHUGAS_JOHO
            .btnRowAdd_KouathuGas.TabIndex = LMM540C.CtlTabIndex.BTN_KOUATHUGAS_ADD
            .btnRowDel_KouathuGas.TabIndex = LMM540C.CtlTabIndex.BTN_KOUATHUGAS_DEL
            .chkKouathugas.TabIndex = LMM540C.CtlTabIndex.KOUATHUGAS_HAIKA_CHK
            .sprDetail5.TabIndex = LMM540C.CtlTabIndex.SPR_TOU_CHK_KOUATHUGAS
            '棟薬機法スプレッド
            .grpYakuzihoJoho.TabIndex = LMM540C.CtlTabIndex.YAKUZIHO_JOHO
            .btnRowAdd_Yakuziho.TabIndex = LMM540C.CtlTabIndex.BTN_YAKUZIHO_ADD
            .btnRowDel_Yakuziho.TabIndex = LMM540C.CtlTabIndex.BTN_YAKUZIHO_DEL
            .chkYakkiho.TabIndex = LMM540C.CtlTabIndex.YAKKIHO_HAIKA_CHK
            .sprDetail6.TabIndex = LMM540C.CtlTabIndex.SPR_TOU_CHK_YAKUZIHO
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
                Case DispMode.INIT _
                , DispMode.VIEW
                    .sprDetail.Focus()

                Case DispMode.EDIT
                    '新規、複写時⇒倉庫コード
                    '編集時　　　⇒棟名
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC _
                           , RecordStatus.COPY_REC
                            .cmbWare.Focus()
                        Case RecordStatus.NOMAL_REC
                            .txtTouNm.Focus()
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
            .numHokanKanoM3.Value = 0
            .numHokanKanoKg.Value = 0
            .numArea.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

            .cmbNrsBrCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.NRS_BR_CD.ColNo))
            .cmbWare.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.WH_CD.ColNo))
            .txtTouNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.TOU_NO.ColNo))
            .txtTouNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.TOU_NM.ColNo))
            .cmbSokoKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SOKO_KB.ColNo))
            .cmbHozeiKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.HOZEI_KB.ColNo))
            .numHokanKanoM3.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.HOKAN_KANO_M3.ColNo))
            .numHokanKanoKg.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.HOKAN_KANO_KG.ColNo))
            .numChozoMaxQty.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.CHOZO_MAX_QTY.ColNo))
            .numChozoMaxBaisu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.CHOZO_MAX_BAISU.ColNo))
            .numArea.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.AREA.ColNo))
            .txtFctMgr.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.FCT_MGR.ColNo))
            .lblFctMgrNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.FCT_MGR_NM.ColNo))
            .cmbOndoKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.ONDO_CTL_KB.ColNo))
            '共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_ENT_USER_NM.ColNo))
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_ENT_DATE.ColNo)))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_UPD_USER_NM.ColNo))
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_UPD_DATE.ColNo)))
            '隠し項目                           
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_UPD_TIME.ColNo))
            .lblSysDelFlg.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM540G.sprDetailDef.SYS_DEL_FLG.ColNo))

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
            .numChozoMaxQty.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numChozoMaxBaisu.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , Convert.ToDecimal(999999.999), Convert.ToDecimal(0))
            .numHokanKanoM3.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , Convert.ToDecimal(999999999999.999), Convert.ToDecimal(0))
            .numHokanKanoKg.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , Convert.ToDecimal(999999999999.999), Convert.ToDecimal(0))
            .numArea.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , Convert.ToDecimal(999999999999.999), Convert.ToDecimal(0))


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
                    Me._Frm.sprDetail4.CrearSpread()
                    Me._Frm.sprDetail5.CrearSpread()
                    Me._Frm.sprDetail6.CrearSpread()

                Case DispMode.VIEW
                    'スプレッド(下部)をロックする
                    Me.SetLockBottomSpreadControl(True)
                    Me.SetLockTouChkSpreadControl(True)

                Case DispMode.EDIT

                    '行追加/削除ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel, unLock)

                    '編集部の項目のロック解除
                    Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                    Call Me._ControlG.LockNumber(.numChozoMaxBaisu, lock)
                    Call Me._ControlG.LockNumber(.numHokanKanoM3, lock)
                    Call Me._ControlG.LockNumber(.numHokanKanoKg, lock)
                    '申請外の商品保管許可ルールの行追加、行削除、一括登録ボタン、および適用日FROM、適用日TO、荷主は
                    '権限が管理者、システム管理者の場合は活性、そうでない場合は非活性を設定する
                    Dim kengen As String = LMUserInfoManager.GetAuthoLv
                    Dim kengenFlg As Boolean = Not (kengen.Equals(LMConst.AuthoKBN.LEADER) OrElse kengen.Equals(LMConst.AuthoKBN.MANAGER))

                    '棟チェックマスタスプレッド(3種) 行追加/削除ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd_Doku, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_Doku, unLock)

                    Call Me._ControlG.LockButton(.btnRowAdd_KouathuGas, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_KouathuGas, unLock)

                    Call Me._ControlG.LockButton(.btnRowAdd_Yakuziho, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel_Yakuziho, unLock)

                    '消防/毒劇/高圧ガス/薬機法 配下に反映CheckBox活性化
                    Call Me._ControlG.LockCheckBox(.chkShobo, unLock)
                    Call Me._ControlG.LockCheckBox(.chkDoku, unLock)
                    Call Me._ControlG.LockCheckBox(.chkKouathugas, unLock)
                    Call Me._ControlG.LockCheckBox(.chkYakkiho, unLock)

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

                            '棟消防情報Spreadの隠し項目である"更新区分"の設定
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

                            '薬機法情報
                            RowCnt = .sprDetail6.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                '更新区分："0"を設定
                                .sprDetail6.SetCellValue(i, sprDetailDef6.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail6.SetCellValue(i, sprDetailDef6.SYS_DEL_FLG_T.ColNo, "0")
                            Next
                    End Select


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
            .cmbSokoKbn.SelectedValue = LMM540C.SOKO_KB      '11(普通倉庫)

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
        End With

        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)
        Me.SetLockTouChkSpreadControl(False)

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm
            '複写しない項目は空を設定
            .txtTouNo.TextValue = String.Empty
            .txtTouNm.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
        End With

        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)

        Me.SetLockTouChkSpreadControl(False)

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

                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.SHOBO_CD.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.HINMEI.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.KIKEN_TOKYU.ColNo, lbl)
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.KIKEN_SYU.ColNo, lbl)
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 999999.99, lockFlg, 2, , ","))
                '.sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.BAISU.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 9999.99, lockFlg, 2, , ","))
                '2018/04/13 001134 【LMS】棟室マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
                .sprDetail2.SetCellStyle((i - 1), LMM540G.sprDetailDef2.WH_KYOKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail2, lockFlg))
            Next

        End With

    End Sub


    ''' <summary>
    ''' 棟チェックマスタスプレッド(3種)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True・ロック解除処理を行う場合False</param>
    ''' <remarks></remarks>
    Friend Sub SetLockTouChkSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            '毒劇情報
            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail4)
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail4, LMM540C.M_Z_KBN_DOKUGEKI, lockFlg)

            Dim max As Integer = .sprDetail4.ActiveSheet.Rows.Count
            For i As Integer = 1 To max
                .sprDetail4.SetCellStyle((i - 1), LMM540G.sprDetailDef4.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail4, lockFlg))
                .sprDetail4.SetCellStyle((i - 1), LMM540G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
            Next

            '高圧ガス情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetail5)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetail5, LMM540C.M_Z_KBN_KOUATHUGAS, lockFlg)

            max = .sprDetail5.ActiveSheet.Rows.Count
            For i As Integer = 1 To max

                .sprDetail5.SetCellStyle((i - 1), LMM540G.sprDetailDef5.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail5, lockFlg))
                .sprDetail5.SetCellStyle((i - 1), LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
            Next

            '薬事法情報
            'ラベルスタイルの設定
            lbl = LMSpreadUtility.GetLabelCell(.sprDetail6)
            sComboKbn = LMSpreadUtility.GetComboCellKbn(.sprDetail6, LMM540C.M_Z_KBN_YAKUZIHO, lockFlg)

            max = .sprDetail6.ActiveSheet.Rows.Count
            For i As Integer = 1 To max

                .sprDetail6.SetCellStyle((i - 1), LMM540G.sprDetailDef6.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail6, lockFlg))
                .sprDetail6.SetCellStyle((i - 1), LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)     '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)           '営業所名
        Public Shared SOKO As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SOKO, "倉庫", 275, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared TOU_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.TOU_NM, "棟名", 200, True)
        Public Shared SOKO_KB_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SOKO_KB_NM, "倉庫区分", 100, True)
        Public Shared HOZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.HOZEI_KB_NM, "保税区分", 80, True)
        Public Shared CHOZO_MAX_QTY As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.CHOZO_MAX_QTY, "貯蔵最大数量", 120, True)
        Public Shared CHOZO_MAX_BAISU As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.CHOZO_MAX_BAISU, "貯蔵最大倍数", 100, True)

        '隠し項目
        Public Shared SOKO_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SOKO_KB, "倉庫区分", 60, False)
        Public Shared HOZEI_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.HOZEI_KB, "保税区分", 60, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.WH_CD, "倉庫コード", 60, False)
        Public Shared HOKAN_KANO_M3 As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.HOKAN_KANO_M3, "保管可能M3", 80, False)
        Public Shared HOKAN_KANO_KG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.HOKAN_KANO_KG, "保管可能重量", 80, False)
        Public Shared ONDO_CTL_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.ONDO_CTL_KB, "温度管理区分", 60, False)
        Public Shared AREA As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.AREA, "床面積（ｍ２）", 60, False)
        Public Shared FCT_MGR As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.FCT_MGR, "保安監督者名", 60, False)
        Public Shared FCT_MGR_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.FCT_MGR_NM, "保安監督者名", 60, False)
        Public Shared ONDO_CTL_KB_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.ONDO_CTL_KB_NM, "温度管理区分", 100, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared ONDO_JITU As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.ONDO_JITU, "設定" & vbCrLf & "温度(℃)", 60, False)
        Public Shared MAX_WT As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex.MAX_WT, "最大重量", 60, False)
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
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex2.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex2.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    '2017/10/20 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' スプレッド列定義体(右下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef3

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.DEF, " ", 20, True)
        Public Shared APPLICATION_DATE_FROM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.APPLICATION_DATE_FROM, "適用日FROM", 90, True)
        Public Shared APPLICATION_DATE_TO As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.APPLICATION_DATE_TO, "適用日TO", 90, True)
        Public Shared CUST_CODE As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.CUST_CD, "荷主CD", 80, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.CUST_NM, "荷主名", 200, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex3.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class
    '2017/10/20 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' スプレッド列定義体(毒劇情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef4

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex4.DEF, " ", 20, True)
        Public Shared DOKU_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex4.DOKU_KB, "毒劇区分", 114, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex4.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex4.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(高圧ガス情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef5

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex5.DEF, " ", 20, True)
        Public Shared KOUATHUGAS_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex5.KOUATHUGAS_KB, "高圧ガス区分", 304, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex5.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex5.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(薬事法情報)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef6

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex6.DEF, " ", 20, True)
        Public Shared YAKUZIHO_KB As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex6.YAKUZIHO_KB, "薬事法区分", 184, True)
        '隠し項目
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex6.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM540C.SprColumnIndex6.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '棟Spreadの初期化処理
        Call Me.InitTouSpread()

        '消防Spreadの初期化処理
        Call Me.InitShoboSpread()

        '毒劇情報Spreadの初期化処理
        Call Me.InitDokuSpread()

        '高圧ガス情報Spreadの初期化処理
        Call Me.InitKouathuGasSpread()

        '薬事法情報Spreadの初期化処理
        Call Me.InitYakuzihoSpread()

    End Sub

    ''' <summary>
    ''' 棟スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitTouSpread()

        '商品Spreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail
        Dim dr As DataRow
        With sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM540C.SprColumnIndex.ClmNm

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMM540G.sprDetailDef(), False)
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
            .SetCellStyle(0, sprDetailDef.TOU_NM.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.SOKO_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_S015, False))
            .SetCellStyle(0, sprDetailDef.HOZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_H001, False))
            .SetCellStyle(0, sprDetailDef.HOKAN_KANO_M3.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprDetailDef.HOKAN_KANO_KG.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_O002, False))

            '**** 隠し列 ****
            .SetCellStyle(0, sprDetailDef.WH_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SOKO_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.HOZEI_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CHOZO_MAX_QTY.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CHOZO_MAX_BAISU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ONDO_CTL_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.AREA.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FCT_MGR.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FCT_MGR_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ONDO_JITU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MAX_WT.ColNo, lbl)

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
            .SetColProperty(New LMM540G.sprDetailDef2(), False)
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
            .ActiveSheet.ColumnCount = LMM540C.SprColumnIndex4.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM540G.sprDetailDef4(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef4.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail4, False))
            .SetCellStyle(-1, sprDetailDef4.DOKU_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail4, LMM540C.M_Z_KBN_DOKUGEKI, False))

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
            .ActiveSheet.ColumnCount = LMM540C.SprColumnIndex5.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM540G.sprDetailDef5(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef5.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail5, False))
            .SetCellStyle(-1, sprDetailDef5.KOUATHUGAS_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail5, LMM540C.M_Z_KBN_KOUATHUGAS, False))

        End With

    End Sub

    ''' <summary>
    ''' 薬機法情報スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitYakuzihoSpread()

        '薬事法情報Spreadの初期値設定
        Dim sprDetail6 As LMSpread = Me._Frm.sprDetail6

        With sprDetail6

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMM540C.SprColumnIndex6.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM540G.sprDetailDef6(), False)

            'ラベルスタイルの設定
            .SetCellStyle(-1, sprDetailDef6.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(sprDetail6, False))
            .SetCellStyle(-1, sprDetailDef6.YAKUZIHO_KB.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail6, LMM540C.M_Z_KBN_YAKUZIHO, False))

        End With

    End Sub

    Friend Sub SetItemIniValue(ByVal frm As LMM540F)

        '適用期間の設定
        Dim period As Integer = Convert.ToInt32(Convert.ToDouble(
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'B032' AND KBN_CD = '1'")(0).Item("VALUE1")))


    End Sub

    '2017/10/20 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM540F)

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
            Dim sLabelR As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -99, 99, True, 2, True, ",")

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)
                '値設定用変数の初期化
                ondo = String.Empty


                'セルスタイル設定
                .SetCellStyle(i, LMM540G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SOKO.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.TOU_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SOKO_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.HOZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.HOKAN_KANO_M3.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.HOKAN_KANO_KG.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SOKO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.HOZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.CHOZO_MAX_QTY.ColNo, sLabelR)
                .SetCellStyle(i, LMM540G.sprDetailDef.CHOZO_MAX_BAISU.ColNo, sLabelR)
                .SetCellStyle(i, LMM540G.sprDetailDef.ONDO_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.AREA.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.FCT_MGR.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.FCT_MGR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM540G.sprDetailDef.MAX_WT.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM540G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SOKO.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.TOU_NM.ColNo, dr.Item("TOU_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SOKO_KB_NM.ColNo, dr.Item("SOKO_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.HOZEI_KB_NM.ColNo, dr.Item("HOZEI_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.HOKAN_KANO_M3.ColNo, dr.Item("HOKAN_KANO_M3").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.HOKAN_KANO_KG.ColNo, dr.Item("HOKAN_KANO_KG").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.ONDO_CTL_KB_NM.ColNo, dr.Item("ONDO_CTL_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SOKO_KB.ColNo, dr.Item("SOKO_KB").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.HOZEI_KB.ColNo, dr.Item("HOZEI_KB").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.CHOZO_MAX_QTY.ColNo, dr.Item("CHOZO_MAX_QTY").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.CHOZO_MAX_BAISU.ColNo, dr.Item("CHOZO_MAX_BAISU").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.ONDO_CTL_KB.ColNo, dr.Item("ONDO_CTL_KB").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.AREA.ColNo, dr.Item("AREA").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.FCT_MGR.ColNo, dr.Item("FCT_MGR").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.FCT_MGR_NM.ColNo, dr.Item("FCT_MGR_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM540G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド2にデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,SHOBO_CD ASC"

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "'"), sort)

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
    ''' スプレッド4にデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread4(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr4 As LMSpread = Me._Frm.sprDetail4
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow4() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND ", "KBN_GROUP_CD = '", LMM540C.M_Z_KBN_DOKUGEKI, "'"), sort)

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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr4, LMM540C.M_Z_KBN_DOKUGEKI, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr4, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow4(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM540G.sprDetailDef4.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef4.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM540G.sprDetailDef4.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM540G.sprDetailDef4.DOKU_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef4.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド5にデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread5(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr5 As LMSpread = Me._Frm.sprDetail5
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow5() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "' AND ", "KBN_GROUP_CD = '", LMM540C.M_Z_KBN_KOUATHUGAS, "'"), sort)

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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr5, LMM540C.M_Z_KBN_KOUATHUGAS, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr5, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow5(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM540G.sprDetailDef5.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef5.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM540G.sprDetailDef5.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef5.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド6にデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread6(ByVal dt As DataTable, ByRef sokocd As String, ByRef touno As String, ByRef situno As String)

        Dim spr6 As LMSpread = Me._Frm.sprDetail6
        Dim dtOut As New DataSet
        Dim sort As String = "NRS_BR_CD ASC,WH_CD ASC,TOU_NO ASC,KBN_GROUP_CD ASC,KBN_CD ASC"

        Dim tmpDatarow6() As DataRow = dt.Select(String.Concat("WH_CD = '", sokocd, "' AND  ", "TOU_NO = '", touno, "'", " AND KBN_GROUP_CD = '", LMM540C.M_Z_KBN_YAKUZIHO, "'"), sort)

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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr6, LMM540C.M_Z_KBN_YAKUZIHO, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr6, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow6(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM540G.sprDetailDef6.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef6.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM540G.sprDetailDef6.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo, dr.Item("KBN_CD").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef6.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド2にデータを設定_行追加時(明細)
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
                '2018/04/13 001134 【LMS】棟マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
                Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0, 999999.99, False, 2, , ",")
                '2018/04/13 001134 【LMS】棟マスタ_倍数4桁→6桁対応(PS和地) Annen upd end
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

    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報スプレッド4にデータを設定_行追加時(明細)
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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr4, LMM540C.M_Z_KBN_DOKUGEKI, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr4, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM540G.sprDetailDef4.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM540G.sprDetailDef4.DOKU_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM540G.sprDetailDef4.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM540G.sprDetailDef4.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM540G.sprDetailDef4.DOKU_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM540G.sprDetailDef4.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 高圧ガス情報スプレッド5にデータを設定_行追加時(明細)
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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr5, LMM540C.M_Z_KBN_KOUATHUGAS, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr5, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM540G.sprDetailDef5.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM540G.sprDetailDef5.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM540G.sprDetailDef5.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM540G.sprDetailDef5.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 薬事法情報スプレッド6にデータを設定_行追加時(明細)
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
            Dim sComboKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr6, LMM540C.M_Z_KBN_YAKUZIHO, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr6, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM540G.sprDetailDef6.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo, sComboKbn)
            .SetCellStyle(row, LMM540G.sprDetailDef6.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM540G.sprDetailDef6.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM540G.sprDetailDef6.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo, "0")

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
            '2018/04/13 001134 【LMS】棟マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.99, False, 2, , ",")
            'Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999.99, False, 2, , ",")
            '2018/04/13 001134 【LMS】棟マスタ_倍数4桁→6桁対応(PS和地) Annen upd start
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

    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    ''' <summary>
    ''' 棟チェックマスタスプレッドスプレッドにデータを設定_行削除時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetTouChkSpread(ByVal spr As LMSpread)

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
                    sKbnGroupCode = LMM540C.M_Z_KBN_DOKUGEKI
                    iColNoDef = LMM540G.sprDetailDef4.DEF.ColNo
                    iColNoCombo = LMM540G.sprDetailDef4.DOKU_KB.ColNo
                    iColNoUpdFlg = LMM540G.sprDetailDef4.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo

                Case "sprDetail5"
                    sKbnGroupCode = LMM540C.M_Z_KBN_KOUATHUGAS
                    iColNoDef = LMM540G.sprDetailDef5.DEF.ColNo
                    iColNoCombo = LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo
                    iColNoUpdFlg = LMM540G.sprDetailDef5.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo

                Case "sprDetail6"
                    sKbnGroupCode = LMM540C.M_Z_KBN_YAKUZIHO
                    iColNoDef = LMM540G.sprDetailDef6.DEF.ColNo
                    iColNoCombo = LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo
                    iColNoUpdFlg = LMM540G.sprDetailDef6.UPD_FLG.ColNo
                    iColNoSysDelFlg = LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo

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
    ''' 棟マスタ消防Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTouShobo(ByVal spr As LMSpread)

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


    ''' <summary>
    ''' 棟チェックマスタスプレッドの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTouChk(ByVal spr As LMSpread, ByVal defColNo As Integer, ByVal updFlgColNo As Integer, ByVal sysDelFlgColNo As Integer)

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
