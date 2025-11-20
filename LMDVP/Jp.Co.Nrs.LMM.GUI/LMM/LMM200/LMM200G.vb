' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM200G : 車輌マスタメンテ
'  作  成  者       :  平山
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
''' LMM200Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM200G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM200F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM200F, ByVal g As LMMControlG)

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
            .F5ButtonEnabled = lock
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

        With Me._Frm
            .lblSituation.DispMode = dispMd
            .lblSituation.RecordStatus = recSts
        End With

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
            .sprDetail.TabIndex = LMM200C.CtlTabIndex.DETAIL

            .cmbNrsBrCd.TabIndex = LMM200C.CtlTabIndex.NRSBRCD
            .lblCarKey.TabIndex = LMM200C.CtlTabIndex.CARKEY
            .cmbCarTpKb.TabIndex = LMM200C.CtlTabIndex.CARTPKB
            .grpFront.TabIndex = LMM200C.CtlTabIndex.GRPFRONT
            .txtCarNo.TabIndex = LMM200C.CtlTabIndex.CARNO
            .imdInspcDateTruck.TabIndex = LMM200C.CtlTabIndex.INSPCDATETRUCK
            .grpBack.TabIndex = LMM200C.CtlTabIndex.GRPBACK
            .txtTrailerNo.TabIndex = LMM200C.CtlTabIndex.TRAILERNO
            .imdInspcDateTrailer.TabIndex = LMM200C.CtlTabIndex.INSPCDATETRAILER
            .cmbAvalYn.TabIndex = LMM200C.CtlTabIndex.AVALYN
            .txtUnsocoCd.TabIndex = LMM200C.CtlTabIndex.UNSOCOCD
            .txtUnsocoBrCd.TabIndex = LMM200C.CtlTabIndex.UNSOCOBRCD
            .lblUnsocoNm.TabIndex = LMM200C.CtlTabIndex.UNSOCONM
            .cmbJshaKb.TabIndex = LMM200C.CtlTabIndex.JSHAKB
            .cmbVcleKb.TabIndex = LMM200C.CtlTabIndex.VCLEKB
            .numLoadWt.TabIndex = LMM200C.CtlTabIndex.LOADWT
            .cmbTempYn.TabIndex = LMM200C.CtlTabIndex.TEMPYN
            .numOndoMm.TabIndex = LMM200C.CtlTabIndex.ONDOMM
            .numOndoMx.TabIndex = LMM200C.CtlTabIndex.ONDOMX
            .cmbFukusuOndoYn.TabIndex = LMM200C.CtlTabIndex.FUKUSUONDOYN
            '2022.09.06 追加START
            .numDayUnchin.TabIndex = LMM200C.CtlTabIndex.DAYUNCHIN
            '2022.09.06 追加END
            .lblSituation.TabIndex = LMM200C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM200C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM200C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM200C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM200C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM200C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM200C.CtlTabIndex.SYSDELFLG






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

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Me._ControlG.SetDateFormat(.imdInspcDateTruck, LMMControlC.DATE_FORMAT.YYYY_MM_DD)
            Me._ControlG.SetDateFormat(.imdInspcDateTrailer, LMMControlC.DATE_FORMAT.YYYY_MM_DD)

        End With

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm


            'numberCellの桁数を設定する
            .numLoadWt.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , 999999999, 0)
            .numOndoMm.SetInputFields("#0", , 2, 1, , 0, 0, , 99, -99)
            .numOndoMx.SetInputFields("#0", , 2, 1, , 0, 0, , 99, -99)
            '2022.09.06 追加START
            .numDayUnchin.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , 9999999999, 0)
            '2022.09.06 追加END

        End With

    End Sub

    ''' <summary>
    ''' 新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetValue()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbAvalYn.SelectedValue = "01"
        Me._Frm.cmbTempYn.SelectedValue = "00"
        Me._Frm.cmbFukusuOndoYn.SelectedValue = "00"




    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, lock)

                Case DispMode.VIEW
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, lock)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblCarKey, lock)
                            Call Me._ControlG.LockComb(.cmbCarTpKb, lock)
                            Call Me._ControlG.LockText(.lblUnsocoNm, lock)


                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblCarKey, lock)
                            Call Me._ControlG.LockText(.lblUnsocoNm, lock)


                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblCarKey, lock)
                            Call Me._ControlG.LockComb(.cmbCarTpKb, lock)
                            Call Me._ControlG.LockText(.lblUnsocoNm, lock)

                    End Select

               
            End Select

        End With

    End Sub


    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM200C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM200C.EventShubetsu.MAIN, LMM200C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM200C.EventShubetsu.SHINKI
                    .cmbCarTpKb.Focus()
                Case LMM200C.EventShubetsu.HUKUSHA, LMM200C.EventShubetsu.HENSHU
                    .txtCarNo.Focus()
            End Select
        End With

    End Sub


    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        With Me._Frm

            '数値項目に初期値0を設定する
            .numLoadWt.Value = 0
            .numOndoMm.Value = 0
            .numOndoMx.Value = 0
            '2022.09.06 追加START
            .numDayUnchin.Value = 0
            '2022.09.06 追加END

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .lblCarKey.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.CAR_KEY.ColNo).Text
            .cmbCarTpKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.CAR_TP_KB.ColNo).Text
            .txtCarNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.CAR_NO.ColNo).Text
            .imdInspcDateTruck.TextValue = DateFormatUtility.DeleteSlash(.sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.INSPC_DATE_TRUCK.ColNo).Text)
            .txtTrailerNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.TRAILER_NO.ColNo).Text
            .imdInspcDateTrailer.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.INSPC_DATE_TRAILER.ColNo).Text
            .cmbAvalYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.AVAL_YN.ColNo).Text
            .txtUnsocoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.UNSOCO_CD.ColNo).Text
            .txtUnsocoBrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo).Text
            .lblUnsocoNm.TextValue = String.Concat(.sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.UNSOCO_NM.ColNo).Text, " ", .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo).Text)
            .cmbJshaKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.JSHA_KB.ColNo).Text
            .cmbVcleKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.VCLE_KB.ColNo).Text
            .numLoadWt.Value = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.LOAD_WT.ColNo).Text
            .cmbTempYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.TEMP_YN.ColNo).Text
            .numOndoMm.Value = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.ONDO_MM.ColNo).Text
            .numOndoMx.Value = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.ONDO_MX.ColNo).Text
            .cmbFukusuOndoYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.FUKUSU_ONDO_YN.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
            '2022.09.06 追加START
            .numDayUnchin.Value = .sprDetail.ActiveSheet.Cells(row, LMM200G.sprDetailDef.DAY_UNCHIN.ColNo).Text
            '2022.09.06 追加END


        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    ''' 
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)           '営業所コード（隠し項目）        
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.UNSOCO_CD, "運送会社" & vbCrLf & "コード", 78, True)
        Public Shared UNSOCO_BR_CD As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.UNSOCO_BR_CD, "支店" & vbCrLf & "コード", 68, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.UNSOCO_NM, "運送会社名", 200, True)
        Public Shared UNSOCO_BR_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.UNSOCO_BR_NM, "支店名", 150, True)
        Public Shared CAR_NO As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.CAR_NO, "車輌番号(前)", 100, True)
        Public Shared INSPC_DATE_TRUCK As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.INSPC_DATE_TRUCK, "車検期限" & vbCrLf & "日付(前)", 80, True)
        Public Shared AVAL_YN As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.AVAL_YN, "使用可能フラグ", 70, False)
        Public Shared AVAL_YN_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.AVAL_YN_NM, "使用可能", 70, True)
        Public Shared VCLE_KB As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.VCLE_KB, "車輌種別", 80, False)
        Public Shared VCLE_KB_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.VCLE_KB_NM, "車輌区分", 80, True)
        Public Shared CAR_KEY As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.CAR_KEY, "車輌キー", 80, False)
        Public Shared TRAILER_NO As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.TRAILER_NO, "トレーラー番号", 80, False)
        Public Shared JSHA_KB As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.JSHA_KB, "自車・傭車の区分", 80, False)
        Public Shared CAR_TP_KB As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.CAR_TP_KB, "車輌区分", 80, False)
        Public Shared LOAD_WT As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.LOAD_WT, "最大積載重量", 70, False)
        Public Shared TEMP_YN As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.TEMP_YN, "温度管理可否区分", 150, False)
        Public Shared ONDO_MM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.ONDO_MM, "設定可能温度上限", 200, False)
        Public Shared ONDO_MX As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.ONDO_MX, "設定可能温度下限", 80, False)
        Public Shared FUKUSU_ONDO_YN As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.FUKUSU_ONDO_YN, "複数温度車室の有無", 200, False)
        Public Shared INSPC_DATE_TRAILER As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.INSPC_DATE_TRAILER, "車検期限日(トレーラー)", 120, False)

        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        '2022.09.06 追加START
        Public Shared DAY_UNCHIN As SpreadColProperty = New SpreadColProperty(LMM200C.SprColumnIndex.DAY_UNCHIN, "1日料金", 60, False)
        '2022.09.06 追加END

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
            '2022.09.06 修正START
            '.sprDetail.Sheets(0).ColumnCount = 30
            .sprDetail.Sheets(0).ColumnCount = 31
            '2022.09.06 修正END

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM200G.sprDetailDef)
            .sprDetail.SetColProperty(New LMM200G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM200G.sprDetailDef.UNSOCO_NM.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.UNSOCO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.CAR_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 20, False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.INSPC_DATE_TRUCK.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.AVAL_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "K017", False))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.VCLE_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S012", False))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.AVAL_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.VCLE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.CAR_KEY.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.TRAILER_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.JSHA_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.CAR_TP_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.LOAD_WT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.TEMP_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.ONDO_MM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.ONDO_MX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.FUKUSU_ONDO_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.INSPC_DATE_TRAILER.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2022.09.06 追加START
            .sprDetail.SetCellStyle(0, LMM200G.sprDetailDef.DAY_UNCHIN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2022.09.06 追加END





        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr


            .SetCellValue(0, LMM200G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM200G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM200G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM200G.sprDetailDef.UNSOCO_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.UNSOCO_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.CAR_NO.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.INSPC_DATE_TRUCK.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.AVAL_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.AVAL_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.VCLE_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.VCLE_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.CAR_KEY.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.TRAILER_NO.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.JSHA_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.CAR_TP_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.LOAD_WT.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.TEMP_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.ONDO_MM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.ONDO_MX.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.FUKUSU_ONDO_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.INSPC_DATE_TRAILER.ColNo, String.Empty)

            .SetCellValue(0, LMM200G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM200G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)
            '2022.09.06 追加START
            .SetCellValue(0, LMM200G.sprDetailDef.DAY_UNCHIN.ColNo, String.Empty)
            '2022.09.06 追加END



            .ResumeLayout(True)

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()
        Dim lock As Boolean = True
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
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

            Dim dr As DataRow = Nothing

            Dim carDate As String = String.Empty


            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)


                'セルスタイル設定
                .SetCellStyle(i, LMM200G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.UNSOCO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.CAR_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.INSPC_DATE_TRUCK.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.AVAL_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.AVAL_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.VCLE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.VCLE_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.CAR_KEY.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.TRAILER_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.JSHA_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.CAR_TP_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.LOAD_WT.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.TEMP_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.ONDO_MM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.ONDO_MX.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.FUKUSU_ONDO_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.INSPC_DATE_TRAILER.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM200G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                '2022.09.06 追加START
                .SetCellStyle(i, LMM200G.sprDetailDef.DAY_UNCHIN.ColNo, sLabel)
                '2022.09.06 追加END


                'セルに値を設定
                .SetCellValue(i, LMM200G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.UNSOCO_CD.ColNo, dr.Item("UNSOCO_CD").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.UNSOCO_BR_CD.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.UNSOCO_BR_NM.ColNo, dr.Item("UNSOCO_BR_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.CAR_NO.ColNo, dr.Item("CAR_NO").ToString())
                carDate = DateFormatUtility.EditSlash(dr.Item("INSPC_DATE_TRUCK").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.INSPC_DATE_TRUCK.ColNo, carDate)
                .SetCellValue(i, LMM200G.sprDetailDef.AVAL_YN.ColNo, dr.Item("AVAL_YN").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.AVAL_YN_NM.ColNo, dr.Item("AVAL_YN_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.VCLE_KB.ColNo, dr.Item("VCLE_KB").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.VCLE_KB_NM.ColNo, dr.Item("VCLE_KB_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.CAR_KEY.ColNo, dr.Item("CAR_KEY").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.TRAILER_NO.ColNo, dr.Item("TRAILER_NO").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.JSHA_KB.ColNo, dr.Item("JSHA_KB").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.CAR_TP_KB.ColNo, dr.Item("CAR_TP_KB").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.LOAD_WT.ColNo, dr.Item("LOAD_WT").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.TEMP_YN.ColNo, dr.Item("TEMP_YN").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.ONDO_MM.ColNo, dr.Item("ONDO_MM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.ONDO_MX.ColNo, dr.Item("ONDO_MX").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.FUKUSU_ONDO_YN.ColNo, dr.Item("FUKUSU_ONDO_YN").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.INSPC_DATE_TRAILER.ColNo, dr.Item("INSPC_DATE_TRAILER").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM200G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                '2022.09.06 追加START
                .SetCellValue(i, LMM200G.sprDetailDef.DAY_UNCHIN.ColNo, dr.Item("DAY_UNCHIN").ToString())
                '2022.09.06 追加END


            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

#End Region

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

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
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl1(ByVal actionType As LMM200C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '車輌区分がトラックの場合はトレーラーの項目をロック
            If .cmbCarTpKb.SelectedValue.ToString() = "01" Then

                'ロック
                Call Me._ControlG.LockText(.txtTrailerNo, lock)
                Call Me._ControlG.LockDate(.imdInspcDateTrailer, lock)

                'クリア処理
                .txtTrailerNo.TextValue = String.Empty
                .imdInspcDateTrailer.TextValue = String.Empty
                Exit Sub

            End If

            '車輌区分がトレーラーの場合はロックを解除
            Call Me._ControlG.LockText(.txtTrailerNo, unLock)
            Call Me._ControlG.LockDate(.imdInspcDateTrailer, unLock)

        End With

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl2(ByVal actionType As LMM200C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '温度管理可否区分が有の場合はロック解除
            If .cmbTempYn.SelectedValue.ToString() = "01" Then
                Call Me._ControlG.LockNumber(.numOndoMm, unLock)
                Call Me._ControlG.LockNumber(.numOndoMx, unLock)
                Call Me._ControlG.LockComb(.cmbFukusuOndoYn, unLock)
                Exit Sub

                '温度管理区分が無の場合はロック
            End If

            'ロック
            Call Me._ControlG.LockNumber(.numOndoMm, lock)
            Call Me._ControlG.LockNumber(.numOndoMx, lock)
            Call Me._ControlG.LockComb(.cmbFukusuOndoYn, lock)

            'クリア処理
            .numOndoMm.Value = 0
            .numOndoMx.Value = 0
            .cmbFukusuOndoYn.SelectedValue = LMMControlC.FLG_OFF

        End With

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub



#End Region

End Class
