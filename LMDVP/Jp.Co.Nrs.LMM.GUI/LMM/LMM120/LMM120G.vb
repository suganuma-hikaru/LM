' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM120G : 単価マスタメンテナンス
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
''' LMM120Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM120G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM120F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' 特定荷主フラグ（TSMC）
    ''' </summary>
    Friend _flgTSMC As Boolean = False

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM120F, ByVal g As LMMControlG)

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
            .F5ButtonName = LMMControlC.FUNCTION_F5_REQUEST
            .F6ButtonName = LMMControlC.FUNCTION_F6_APPROVAL
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
            Dim auth30 As Boolean = "30".Equals(LMUserInfoManager.GetAuthoLv)         '権限レベルがセンター長なら使用可能

            '常に使用不可キー
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
            '承認系
            .F5ButtonEnabled = view
            .F6ButtonEnabled = view AndAlso auth30
            '承認系(ボタン)
            Me._Frm.btnRemand.Enabled = view AndAlso auth30
            Me._Frm.btnRemand.Visible = auth30

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

            .chkZenDataHyoji.TabIndex = LMM120C.CtlTabIndex.CHK_ZENDATA_HYOJI
            .sprDetail.TabIndex = LMM120C.CtlTabIndex.SPR_DTL
            .cmbBr.TabIndex = LMM120C.CtlTabIndex.CMB_BR
            .lblRecNo.TabIndex = LMM120C.CtlTabIndex.LBL_REC_NO
            .txtCustCdL.TabIndex = LMM120C.CtlTabIndex.TXT_CUST_CD_L
            .lblCustNmL.TabIndex = LMM120C.CtlTabIndex.LBL_CUST_NM_L
            .txtCustCdM.TabIndex = LMM120C.CtlTabIndex.TXT_CUST_CD_M
            .lblCustNmM.TabIndex = LMM120C.CtlTabIndex.LBL_CUST_NM_M
            .cmbKiwariKbn.TabIndex = LMM120C.CtlTabIndex.CMB_KIWARI_KBN
            .txtTekiyo.TabIndex = LMM120C.CtlTabIndex.TXT_TEKIYO
            .txtTankaMstCd.TabIndex = LMM120C.CtlTabIndex.TXT_TANKA_MST_CD
            .imdTekiyoStart.TabIndex = LMM120C.CtlTabIndex.IMD_TEKIYO_START
            .cmbHokanKbnNashi.TabIndex = LMM120C.CtlTabIndex.CMB_HOKANRYO_KBN_NASHI
            .cmbHokanKbnAri.TabIndex = LMM120C.CtlTabIndex.CMB_HOKANRYO_KBN_ARI
            .numHokanryoNashi.TabIndex = LMM120C.CtlTabIndex.NUM_HOKANRYO_NASHI
            .numHokanryoAri.TabIndex = LMM120C.CtlTabIndex.NUM_HOKANRYO_ARI
            .cmbNiyakuryoKbnNyuko.TabIndex = LMM120C.CtlTabIndex.CMB_NIYAKURYO_KBN_NYUKO
            .cmbNiyakuryoKbnShukko.TabIndex = LMM120C.CtlTabIndex.CMB_NIYAKURYO_KBN_SHUKKO
            .numNiyakuryoNyuko.TabIndex = LMM120C.CtlTabIndex.NUM_NIYAKURYO_NYUKO
            .numNiyakuryoShukko.TabIndex = LMM120C.CtlTabIndex.NUM_NIYAKURYO_SHUKKO
            .numMinHoshoNyuko.TabIndex = LMM120C.CtlTabIndex.NUM_MIN_HOSHO_NIYAKU_NYUKO
            .numMinHoshoShukko.TabIndex = LMM120C.CtlTabIndex.NUM_MIN_HOSHO_NIYAKU_SHUKKO
            .cmbProductSegCd.TabIndex = LMM120C.CtlTabIndex.CMB_PRODUCT_SEG_CD

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

        '特定荷主（TSMC）用コントロール制御
        If Me._flgTSMC Then
            '非表示とする
            Me._Frm.lblTitleOndoKanriNashi.Visible = False
            Me._Frm.lblTitleOndoKanriAri.Visible = False
            Me._Frm.lblTitleHokanKbn.Visible = False
            Me._Frm.cmbHokanKbnNashi.Visible = False
            Me._Frm.cmbHokanKbnAri.Visible = False
            Me._Frm.lblTitleNyuko.Visible = False
            Me._Frm.lblTitleShukko.Visible = False
            Me._Frm.lblTitleNiyakuryoKbn.Visible = False
            Me._Frm.cmbNiyakuryoKbnNyuko.Visible = False
            Me._Frm.cmbNiyakuryoKbnShukko.Visible = False
            Me._Frm.numNiyakuryoShukko.Visible = False
            Me._Frm.lblNiyakuryoShukkoCurrCd.Visible = False
            ''''Me._Frm.lblTitleMinHosho.Visible = False
            ''''Me._Frm.numMinHoshoNyuko.Visible = False
            Me._Frm.lblMinHoshoNyukoCurrCd.Visible = False
            Me._Frm.numMinHoshoShukko.Visible = False
            Me._Frm.lblMinHoshoSyukkoCurrCd.Visible = False

            '見出しを変更する
            Me._Frm.lblTitleHokanryo.Text = "セット料金"
            Me._Frm.lblTitleNiyakuryo.Text = "超過料金"
            Me._Frm.lblTitleMinHosho.Text = "期間"

            '専用ラベルを表示する／位置を調整する
            Me._Frm.lblTitleHokanryoAri.Visible = True
            Me._Frm.lblTitleHokanryoAri.Left = Me._Frm.numHokanryoAri.Left
            Me._Frm.numHokanryoAri.Left = Me._Frm.lblTitleHokanryoAri.Left + Me._Frm.lblTitleHokanryoAri.Width + 2
            Me._Frm.lblHokanryoAriCurrCd.Left = Me._Frm.numHokanryoAri.Left + 87
            Dim minHoshoTopOffset As Integer = Me._Frm.lblTitleMinHosho.Top - Me._Frm.lblTitleNyuko.Top
            Me._Frm.lblTitleMinHosho.Top = Me._Frm.lblTitleNyuko.Top
            Me._Frm.numMinHoshoNyuko.Top -= minHoshoTopOffset
            Me._Frm.lblMinHoshoNyukoCurrCd2.Visible = True
            Me._Frm.lblMinHoshoNyukoCurrCd2.Top -= minHoshoTopOffset
            Me._Frm.lblMinHoshoNyukoCurrCd2.Left = Me._Frm.lblMinHoshoNyukoCurrCd.Left

            '書式設定の変更
            Me._Frm.numMinHoshoNyuko.SetInputFields("##,##0", , 5, 1, , 0, 0, , Convert.ToDecimal(99999), Convert.ToDecimal(0))

            '位置の前後関係を変更した項目のタブインデックスの(再)設定
            Me._Frm.numMinHoshoNyuko.TabIndex = LMM120C.CtlTabIndex.CMB_NIYAKURYO_KBN_NYUKO
        End If

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
            For Each r As DataRow In ds.Tables("LMM120COMBO_SEIHINA").Rows
                subItems.Add(New ListItem(New SubItem() {New SubItem(r.Item("SEG_NM")), New SubItem(r.Item("SEG_CD"))}))
            Next
            .cmbProductSegCd.Items.AddRange(subItems.ToArray())
            .cmbProductSegCd.Refresh()

        End With

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
                    .chkZenDataHyoji.Focus()

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NOMAL_REC
                            .cmbKiwariKbn.Focus()
                        Case Else
                            .txtCustCdL.Focus()
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
            .numHokanryoNashi.Value = 0
            .numHokanryoAri.Value = 0
            .numNiyakuryoNyuko.Value = 0
            .numNiyakuryoShukko.Value = 0
            .numMinHoshoNyuko.Value = 0
            .numMinHoshoShukko.Value = 0
            .lblHokanryoNashiCurrCd.TextValue = ""
            .lblHokanryoAriCurrCd.TextValue = ""
            .lblNiyakuryoNyukoCurrCd.TextValue = ""
            .lblNiyakuryoShukkoCurrCd.TextValue = ""
            .lblMinHoshoNyukoCurrCd.TextValue = ""
            .lblMinHoshoSyukkoCurrCd.TextValue = ""
            .cmbAVAL_YN.SelectedValue = "01"    'ADD 2019/04/18 依頼番号 : 004862
        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

            .cmbBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.BR_CD.ColNo))
            .lblRecNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.REC_NO.ColNo))
            .txtCustCdL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CUST_CD_L.ColNo))
            .lblCustNmL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CUST_NM_L.ColNo))
            .txtCustCdM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CUST_CD_M.ColNo))
            .lblCustNmM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CUST_NM_M.ColNo))
            .cmbKiwariKbn.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.KIWARI_KBN.ColNo))
            .txtTekiyo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.TEKIYO.ColNo))
            .txtTankaMstCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.TANKA_MST_CD.ColNo))
            .imdTekiyoStart.TextValue = DateFormatUtility.DeleteSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.TEKIYO_START_DATE.ColNo)))
            .cmbAVAL_YN.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.AVAL_YN.ColNo))                              'ADD 2019/04/18 依頼番号 : 004862
            .cmbHokanKbnNashi.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_KBN_NASHI.ColNo))
            .cmbHokanKbnAri.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_KBN_ARI.ColNo))
            .numHokanryoNashi.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_NASHI.ColNo))
            .numHokanryoAri.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_ARI.ColNo))
            .cmbNiyakuryoKbnNyuko.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_KBN_NYUKO.ColNo))
            .cmbNiyakuryoKbnShukko.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_KBN_SHUKKO.ColNo))
            .numNiyakuryoNyuko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_NYUKO.ColNo))
            .numNiyakuryoShukko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_SHUKKO.ColNo))
            .numMinHoshoNyuko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO.ColNo))
            .numMinHoshoShukko.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO.ColNo))
            .lblHokanryoNashiCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_NASHI_CURR_CD.ColNo))
            .lblHokanryoAriCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.HOKANRYO_ARI_CURR_CD.ColNo))
            .lblNiyakuryoNyukoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_NYUKO_CURR_CD.ColNo))
            .lblNiyakuryoShukkoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo))
            .lblMinHoshoNyukoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD.ColNo))
            .lblMinHoshoSyukkoCurrCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD.ColNo))
            .cmbProductSegCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.PRODUCT_SEG_CD.ColNo))

            '共通項目
            .lblCreateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CREATE_USER.ColNo))
            .lblCreateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.CREATE_DATE.ColNo)))
            .lblUpdateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.UPDATE_USER.ColNo))
            .lblUpdateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.UPDATE_DATE.ColNo)))
            '承認項目
            .lblApprovalUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.APPROVAL_USER.ColNo))
            .lblApprovalDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.APPROVAL_DATE.ColNo)))
            '隠し項目                           
            .lblUpdateTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.UPDATE_TIME.ColNo))
            .lblApprovalCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM120G.sprDtlDef.APPROVAL_CD.ColNo))

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Me._ControlG.SetDateFormat(.imdTekiyoStart, LMMControlC.DATE_FORMAT.YYYY_MM_DD)

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            .numHokanryoNashi.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numHokanryoAri.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNiyakuryoNyuko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numNiyakuryoShukko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numMinHoshoNyuko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))
            .numMinHoshoShukko.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))

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

            '画面項目を全ロックする（承認の差し戻しボタンは影響を受けないようにする）
            Dim remandEnabled As Boolean = Me._Frm.btnRemand.Enabled

            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Me._Frm.btnRemand.Enabled = remandEnabled


            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    Call Me.ClearControl(Me._Frm)

                Case DispMode.EDIT

                    '画面項目を全ロック解除する
                    Call Me._ControlG.SetLockControl(Me._Frm, unLock)

                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbBr, lock)

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

                    End Select
            End Select

            '常に使用可能項目を設定
            Call Me._ControlG.LockCheckBox(.chkZenDataHyoji, unLock)

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        With Me._Frm

            '表示内容を初期化する
            Call Me.ClearControl(Me._Frm)

            '初期値を設定
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd
            .cmbHokanKbnNashi.SelectedValue = LMM120C.TANKA_KBN_KOSU_DATE
            .cmbHokanKbnAri.SelectedValue = LMM120C.TANKA_KBN_KOSU_DATE
            .cmbNiyakuryoKbnNyuko.SelectedValue = LMM120C.TANKA_KBN_KOSU_DATE
            .cmbNiyakuryoKbnShukko.SelectedValue = LMM120C.TANKA_KBN_KOSU_DATE
            .lblHokanryoNashiCurrCd.TextValue = ""
            .lblHokanryoAriCurrCd.TextValue = ""
            .lblNiyakuryoNyukoCurrCd.TextValue = ""
            .lblNiyakuryoShukkoCurrCd.TextValue = ""
            .lblMinHoshoNyukoCurrCd.TextValue = ""
            .lblMinHoshoSyukkoCurrCd.TextValue = ""

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True

        With Me._Frm

            '主キーロック
            Call Me._ControlG.LockText(.txtCustCdL, lock)         '荷主コード(大)
            Call Me._ControlG.LockText(.txtCustCdM, lock)         '荷主コード(中)
            Call Me._ControlG.LockText(.txtTankaMstCd, lock)      '単価マスタコード
            Call Me._ControlG.LockDate(.imdTekiyoStart, lock)     '適用開始日

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        Dim lock As Boolean = True

        With Me._Frm

            '主キー項目クリア
            .lblRecNo.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtTankaMstCd.TextValue = String.Empty
            '共通項目クリア
            .imdTekiyoStart.TextValue = String.Empty
            .lblCreateDate.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty
            '承認項目クリア
            .lblApprovalDate.TextValue = String.Empty
            .lblApprovalUser.TextValue = String.Empty
            .lblApprovalCd.TextValue = String.Empty

            .cmbAVAL_YN.SelectedValue = "01"    'ADD 2019/04/18 依頼番号 : 004862
        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    ''' 
    Public Class sprDtlDef

        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.STATUS, "状態", 60, True)
        Public Shared AVAL_YN As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.AVAL_YN, "使用状態", 60, False)            'ADD 2019/04/18 依頼番号 : 004862
        Public Shared AVAL_YN_NM As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.AVAL_YN_NM, "使用状態", 60, True)       'ADD 2019/04/18 依頼番号 : 004862
        Public Shared APPROVAL_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.APPROVAL_CD, "承認状況", 60, False)
        Public Shared APPROVAL_NM As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.APPROVAL_NM, "承認状況", 60, True)
        Public Shared APPROVAL_USER As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.APPROVAL_USER, "承認者", 60, False)
        Public Shared APPROVAL_DATE As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.APPROVAL_DATE, "承認日", 60, False)
        Public Shared APPROVAL_TIME As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.APPROVAL_TIME, "承認時間", 60, False)
        Public Shared BR_NM As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.BR_NM, "営業所", 275, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CUST_NM_L, "荷主名(大)", 200, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 100, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CUST_NM_M, "荷主名(中)", 200, True)
        Public Shared TANKA_MST_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.TANKA_MST_CD, "単価マスタ" & vbCrLf & "コード", 90, True)
        Public Shared TEKIYO_START_DATE As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.TEKIYO_START_DATE, "適用開始日", 90, True)

        '******* 隠し列 *******
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.BR_CD, "", 50, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.REC_NO, "", 50, False)
        Public Shared TEKIYO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.TEKIYO, "", 50, False)
        Public Shared HOKANRYO_KBN_NASHI As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_KBN_NASHI, "", 50, False)
        Public Shared HOKANRYO_KBN_ARI As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_KBN_ARI, "", 50, False)
        Public Shared HOKANRYO_NASHI As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_NASHI, "", 50, False)
        Public Shared HOKANRYO_ARI As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_ARI, "", 50, False)
        Public Shared NIYAKURYO_KBN_NYUKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_KBN_NYUKO, "", 50, False)
        Public Shared NIYAKURYO_KBN_SHUKKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_KBN_SHUKKO, "", 50, False)
        Public Shared NIYAKURYO_NYUKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_NYUKO, "", 50, False)
        Public Shared NIYAKURYO_SHUKKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_SHUKKO, "", 50, False)
        Public Shared MIN_HOSHO_NIYAKURYO_NYUKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.MIN_HOSHO_NIYAKURYO_NYUKO, "", 50, False)
        Public Shared MIN_HOSHO_NIYAKURYO_SHUKKO As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.MIN_HOSHO_NIYAKURYO_SHUKKO, "", 50, False)
        Public Shared KIWARI_KBN As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.KIWARI_KBN, "", 50, False)
        Public Shared HOKANRYO_NASHI_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_NASHI_CURR_CD, "", 50, False)
        Public Shared HOKANRYO_ARI_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.HOKANRYO_ARI_CURR_CD, "", 50, False)
        Public Shared NIYAKURYO_NYUKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_NYUKO_CURR_CD, "", 50, False)
        Public Shared NIYAKURYO_SHUKKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.NIYAKURYO_SHUKKO_CURR_CD, "", 50, False)
        Public Shared MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD, "", 50, False)
        Public Shared MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD, "", 50, False)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CREATE_DATE, "", 50, False)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.CREATE_USER, "", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.UPDATE_DATE, "", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.UPDATE_USER, "", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.UPDATE_TIME, "", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.SYS_DEL_FLG, "", 50, False)
        Public Shared PRODUCT_SEG_CD As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.PRODUCT_SEG_CD, "", 50, False)
        Public Shared PRODUCT_SEG_NM_L As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.PRODUCT_SEG_NM_L, "", 50, False)
        Public Shared PRODUCT_SEG_NM_M As SpreadColProperty = New SpreadColProperty(LMM120C.SprDtlColumnIndex.PRODUCT_SEG_NM_M, "", 50, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        'Spreadの初期値設定
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dr As DataRow

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 45

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDtlDef)
            .SetColProperty(New LMM120G.sprDtlDef(), False)
            '2015.10.15 英語化対応END

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '**** 表示列 ****
            .SetCellStyle(0, sprDtlDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_S051, False))
            .SetCellStyle(0, sprDtlDef.AVAL_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "K017", False))          'ADD 2019/04/18 依頼番号 : 004862
            .SetCellStyle(0, sprDtlDef.APPROVAL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "A009", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprDtlDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprDtlDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If

            '.SetCellStyle(0, sprDtlDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            .SetCellStyle(0, sprDtlDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 5, False))
            .SetCellStyle(0, sprDtlDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDtlDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 2, False))
            .SetCellStyle(0, sprDtlDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDtlDef.TANKA_MST_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 3, False))
            .SetCellStyle(0, sprDtlDef.TEKIYO_START_DATE.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprDtlDef.BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.REC_NO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.TEKIYO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_KBN_NASHI.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_KBN_ARI.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_NASHI.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_ARI.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_KBN_NYUKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_KBN_SHUKKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_NYUKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_SHUKKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.KIWARI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_NASHI_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.HOKANRYO_ARI_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.SYS_DEL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.PRODUCT_SEG_CD.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.PRODUCT_SEG_NM_L.ColNo, lbl)
            .SetCellStyle(0, sprDtlDef.PRODUCT_SEG_NM_M.ColNo, lbl)

            '初期値設定
            Call Me._ControlG.ClearControl(spr)
            .SetCellValue(0, sprDtlDef.STATUS.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprDtlDef.BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())

        End With

    End Sub

    ''' <summary>
    ''' 検索結果をSpreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
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

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDtlDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDtlDef.STATUS.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.AVAL_YN.ColNo, lbl)          'ADD 2019/04/18 依頼番号 : 004862
                .SetCellStyle(i, sprDtlDef.AVAL_YN_NM.ColNo, lbl)       'ADD 2019/04/18 依頼番号 : 004862
                .SetCellStyle(i, sprDtlDef.APPROVAL_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.APPROVAL_NM.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.APPROVAL_USER.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.APPROVAL_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.APPROVAL_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.BR_NM.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CUST_CD_L.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CUST_NM_L.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CUST_CD_M.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CUST_NM_M.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.TANKA_MST_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.TEKIYO_START_DATE.ColNo, lbl)
                '**** 隠し列 ****
                .SetCellStyle(i, sprDtlDef.BR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.REC_NO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.TEKIYO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_KBN_NASHI.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_KBN_ARI.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_NASHI.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_ARI.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_KBN_NYUKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_KBN_SHUKKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_NYUKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_SHUKKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.KIWARI_KBN.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CREATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.CREATE_USER.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.UPDATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.UPDATE_USER.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.UPDATE_TIME.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.SYS_DEL_FLG.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_NASHI_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.HOKANRYO_ARI_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.PRODUCT_SEG_CD.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.PRODUCT_SEG_NM_L.ColNo, lbl)
                .SetCellStyle(i, sprDtlDef.PRODUCT_SEG_NM_M.ColNo, lbl)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDtlDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDtlDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprDtlDef.AVAL_YN.ColNo, dr.Item("AVAL_YN").ToString())            'ADD 2019/04/18 依頼番号 : 004862
                .SetCellValue(i, sprDtlDef.AVAL_YN_NM.ColNo, dr.Item("AVAL_YN_NM").ToString())       'ADD 2019/04/18 依頼番号 : 004862
                .SetCellValue(i, sprDtlDef.APPROVAL_CD.ColNo, dr.Item("APPROVAL_CD").ToString())
                .SetCellValue(i, sprDtlDef.APPROVAL_NM.ColNo, dr.Item("APPROVAL_NM").ToString())
                .SetCellValue(i, sprDtlDef.APPROVAL_USER.ColNo, dr.Item("APPROVAL_USER").ToString())
                .SetCellValue(i, sprDtlDef.APPROVAL_DATE.ColNo, dr.Item("APPROVAL_DATE").ToString())
                .SetCellValue(i, sprDtlDef.APPROVAL_TIME.ColNo, dr.Item("APPROVAL_TIME").ToString())
                .SetCellValue(i, sprDtlDef.BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprDtlDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDtlDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, sprDtlDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDtlDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, sprDtlDef.TANKA_MST_CD.ColNo, dr.Item("UP_GP_CD_1").ToString())
                .SetCellValue(i, sprDtlDef.TEKIYO_START_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("STR_DATE").ToString()))
                '**** 隠し列 ****
                .SetCellValue(i, sprDtlDef.BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDtlDef.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, sprDtlDef.TEKIYO.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_KBN_NASHI.ColNo, dr.Item("STORAGE_KB1").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_KBN_ARI.ColNo, dr.Item("STORAGE_KB2").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_NASHI.ColNo, dr.Item("STORAGE_1").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_ARI.ColNo, dr.Item("STORAGE_2").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_KBN_NYUKO.ColNo, dr.Item("HANDLING_IN_KB").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_KBN_SHUKKO.ColNo, dr.Item("HANDLING_OUT_KB").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_NYUKO.ColNo, dr.Item("HANDLING_IN").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_SHUKKO.ColNo, dr.Item("HANDLING_OUT").ToString())
                .SetCellValue(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO.ColNo, dr.Item("MINI_TEKI_IN_AMO").ToString())
                .SetCellValue(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO.ColNo, dr.Item("MINI_TEKI_OUT_AMO").ToString())
                .SetCellValue(i, sprDtlDef.KIWARI_KBN.ColNo, dr.Item("KIWARI_KB").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_NASHI_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.HOKANRYO_ARI_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_NYUKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.NIYAKURYO_SHUKKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDtlDef.CREATE_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, sprDtlDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, sprDtlDef.UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDtlDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, sprDtlDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDtlDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, sprDtlDef.PRODUCT_SEG_CD.ColNo, dr.Item("PRODUCT_SEG_CD").ToString())
                .SetCellValue(i, sprDtlDef.PRODUCT_SEG_NM_L.ColNo, dr.Item("PRODUCT_SEG_NM_L").ToString())
                .SetCellValue(i, sprDtlDef.PRODUCT_SEG_NM_M.ColNo, dr.Item("PRODUCT_SEG_NM_M").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
