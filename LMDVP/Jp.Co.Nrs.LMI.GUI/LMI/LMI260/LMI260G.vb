' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260G : 引取運賃明細入力
'  作  成  者       :  yamanaka
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

''' <summary>
''' LMI260Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI260G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI260F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 初期処理か判断するフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean = True

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI260V

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI260F, ByVal g As LMIControlG, ByVal v As LMI260V)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        Me._V = v

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
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = "複　写"
            .F4ButtonName = "削除・復活"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"


            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock

            '常に使用可能キー
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock

            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

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

            '******************* ヘッダー部 *****************************
            .imdHikiDate_From.TabIndex = LMI260C.CtlTabIndex.IMD_HIKI_DATE_FROM
            .imdHikiDate_To.TabIndex = LMI260C.CtlTabIndex.IMD_HIKI_DATE_TO
            .cmbPrintShubetu.TabIndex = LMI260C.CtlTabIndex.CMB_PRINT_SHUBETU
            .cmbPrintHinNm.TabIndex = LMI260C.CtlTabIndex.CMB_PRINT_HIN_NM
            .sprDetail.TabIndex = LMI260C.CtlTabIndex.SPR_DETAIL
            '******************* 編集部 *****************************
            .cmbNrsBr.TabIndex = LMI260C.CtlTabIndex.CMB_NRS_BR
            .txtCustCdL.TabIndex = LMI260C.CtlTabIndex.TXT_CUST_CD_L
            .lblCustNmL.TabIndex = LMI260C.CtlTabIndex.LBL_CUST_NM_L
            .txtCustCdM.TabIndex = LMI260C.CtlTabIndex.TXT_CUST_CD_M
            .lblCustNmM.TabIndex = LMI260C.CtlTabIndex.LBL_CUST_NM_M
            .imdHikiDate.TabIndex = LMI260C.CtlTabIndex.IMD_HIKI_DATE
            .numMeisaiNo.TabIndex = LMI260C.CtlTabIndex.NUM_MEISAI_NO
            .cmbHinNm.TabIndex = LMI260C.CtlTabIndex.CMB_HIN_NM
            .txtHikitoriCd.TabIndex = LMI260C.CtlTabIndex.TXT_HIKITORI_CD
            .lblHikitoriNm.TabIndex = LMI260C.CtlTabIndex.LBL_HIKITORI_NM
            .grpFc.TabIndex = LMI260C.CtlTabIndex.GRP_FC
            .numFcNb.TabIndex = LMI260C.CtlTabIndex.NUM_FC_NB
            .numFcTanka.TabIndex = LMI260C.CtlTabIndex.NUM_FC_TANKA
            .numFcTotal.TabIndex = LMI260C.CtlTabIndex.NUM_FC_TOTAL
            .grpDm.TabIndex = LMI260C.CtlTabIndex.GRP_DM
            .numDmNb.TabIndex = LMI260C.CtlTabIndex.NUM_DM_NB
            .numDmTanka.TabIndex = LMI260C.CtlTabIndex.NUM_DM_TANKA
            .numDmTotal.TabIndex = LMI260C.CtlTabIndex.NUM_DM_TOTAL
            .numKisu.TabIndex = LMI260C.CtlTabIndex.NUM_KISU
            .numSeihin.TabIndex = LMI260C.CtlTabIndex.NUM_SEIHIN
            .numSukurap.TabIndex = LMI260C.CtlTabIndex.NUM_SUKURAP
            .numWarimasi.TabIndex = LMI260C.CtlTabIndex.NUM_WARIMASI
            .numSeikei.TabIndex = LMI260C.CtlTabIndex.NUM_SEIKEI
            .numRosen.TabIndex = LMI260C.CtlTabIndex.NUM_ROSEN
            .numKousoku.TabIndex = LMI260C.CtlTabIndex.NUM_KOUSOKU
            .numSonota.TabIndex = LMI260C.CtlTabIndex.NUM_SONOTA
            .numAllTotal.TabIndex = LMI260C.CtlTabIndex.NUM_ALL_TOTAL
            .txtRemark.TabIndex = LMI260C.CtlTabIndex.TXT_REMARK
            .lblCrtUser.TabIndex = LMI260C.CtlTabIndex.LBL_CRT_USER
            .lblCrtDate.TabIndex = LMI260C.CtlTabIndex.LBL_CRT_DATE
            .lblUpdUser.TabIndex = LMI260C.CtlTabIndex.LBL_UPD_USER
            .lblUpdDate.TabIndex = LMI260C.CtlTabIndex.LBL_UPD_DATE
            '隠し項目
            .lblUpdTime.TabIndex = LMI260C.CtlTabIndex.LBL_UPD_TIME

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '初期日付設定
        Call Me.SetDateControl(sysDate)

        '編集部の数値項目設定
        Call Me.SetDefaultNumber()

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
                    .imdHikiDate_From.Focus()

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                            .txtCustCdL.Focus()
                        Case RecordStatus.NOMAL_REC
                            .numFcNb.Focus()
                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '初期化処理
        Call Me.ClearControlSinki()

        'ナンバー型初期値
        Call Me.SetDefaultNumber()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.EDIT

                    Select Case Me._Frm.lblSituation.RecordStatus

                        Case RecordStatus.NEW_REC
                            '新規モード
                            .imdHikiDate_From.ReadOnly = False
                            .imdHikiDate_To.ReadOnly = False
                            .cmbPrintShubetu.ReadOnly = True
                            .cmbPrintHinNm.ReadOnly = True
                            .btnPrint.Enabled = False

                            .cmbNrsBr.ReadOnly = True
                            .txtCustCdL.ReadOnly = False
                            .lblCustNmL.ReadOnly = True
                            .txtCustCdM.ReadOnly = False
                            .lblCustNmM.ReadOnly = True
                            .imdHikiDate.ReadOnly = False
                            .numMeisaiNo.ReadOnly = False
                            .cmbHinNm.ReadOnly = False
                            .txtHikitoriCd.ReadOnly = False
                            .lblHikitoriNm.ReadOnly = True
                            .numFcNb.ReadOnly = False
                            .numFcTanka.ReadOnly = False
                            .numFcTotal.ReadOnly = True
                            .numDmNb.ReadOnly = False
                            .numDmTanka.ReadOnly = False
                            .numDmTotal.ReadOnly = True
                            .numKisu.ReadOnly = False
                            .numSeihin.ReadOnly = False
                            .numSukurap.ReadOnly = False
                            .numWarimasi.ReadOnly = False
                            .numSeikei.ReadOnly = False
                            .numRosen.ReadOnly = False
                            .numKousoku.ReadOnly = False
                            .numSonota.ReadOnly = False
                            .numAllTotal.ReadOnly = True
                            .txtRemark.ReadOnly = False
                            .lblCrtUser.ReadOnly = True
                            .lblCrtDate.ReadOnly = True
                            .lblUpdUser.ReadOnly = True
                            .lblUpdDate.ReadOnly = True
                            .lblUpdateTime.ReadOnly = True

                        Case RecordStatus.NOMAL_REC

                            '編集モード
                            .imdHikiDate_From.ReadOnly = False
                            .imdHikiDate_To.ReadOnly = False
                            .cmbPrintShubetu.ReadOnly = True
                            .cmbPrintHinNm.ReadOnly = True
                            .btnPrint.Enabled = False

                            .cmbNrsBr.ReadOnly = True
                            .txtCustCdL.ReadOnly = True
                            .lblCustNmL.ReadOnly = True
                            .txtCustCdM.ReadOnly = True
                            .lblCustNmM.ReadOnly = True
                            .imdHikiDate.ReadOnly = True
                            .numMeisaiNo.ReadOnly = True
                            .cmbHinNm.ReadOnly = True
                            .txtHikitoriCd.ReadOnly = True
                            .lblHikitoriNm.ReadOnly = True
                            .numFcNb.ReadOnly = False
                            .numFcTanka.ReadOnly = False
                            .numFcTotal.ReadOnly = True
                            .numDmNb.ReadOnly = False
                            .numDmTanka.ReadOnly = False
                            .numDmTotal.ReadOnly = True
                            .numKisu.ReadOnly = False
                            .numSeihin.ReadOnly = False
                            .numSukurap.ReadOnly = False
                            .numWarimasi.ReadOnly = False
                            .numSeikei.ReadOnly = False
                            .numRosen.ReadOnly = False
                            .numKousoku.ReadOnly = False
                            .numSonota.ReadOnly = False
                            .numAllTotal.ReadOnly = True
                            .txtRemark.ReadOnly = False
                            .lblCrtUser.ReadOnly = True
                            .lblCrtDate.ReadOnly = True
                            .lblUpdUser.ReadOnly = True
                            .lblUpdDate.ReadOnly = True
                            .lblUpdateTime.ReadOnly = True

                        Case RecordStatus.COPY_REC

                            'コピーモード
                            .imdHikiDate_From.ReadOnly = False
                            .imdHikiDate_To.ReadOnly = False
                            .cmbPrintShubetu.ReadOnly = True
                            .cmbPrintHinNm.ReadOnly = True
                            .btnPrint.Enabled = False

                            .cmbNrsBr.ReadOnly = True
                            .txtCustCdL.ReadOnly = False
                            .lblCustNmL.ReadOnly = True
                            .txtCustCdM.ReadOnly = False
                            .lblCustNmM.ReadOnly = True
                            .imdHikiDate.ReadOnly = False
                            .numMeisaiNo.ReadOnly = False
                            .cmbHinNm.ReadOnly = False
                            .txtHikitoriCd.ReadOnly = False
                            .lblHikitoriNm.ReadOnly = True
                            .numFcNb.ReadOnly = False
                            .numFcTanka.ReadOnly = False
                            .numFcTotal.ReadOnly = True
                            .numDmNb.ReadOnly = False
                            .numDmTanka.ReadOnly = False
                            .numDmTotal.ReadOnly = True
                            .numKisu.ReadOnly = False
                            .numSeihin.ReadOnly = False
                            .numSukurap.ReadOnly = False
                            .numWarimasi.ReadOnly = False
                            .numSeikei.ReadOnly = False
                            .numRosen.ReadOnly = False
                            .numKousoku.ReadOnly = False
                            .numSonota.ReadOnly = False
                            .numAllTotal.ReadOnly = True
                            .txtRemark.ReadOnly = False
                            .lblCrtUser.ReadOnly = True
                            .lblCrtDate.ReadOnly = True
                            .lblUpdUser.ReadOnly = True
                            .lblUpdDate.ReadOnly = True
                            .lblUpdateTime.ReadOnly = True

                    End Select

                Case DispMode.INIT, DispMode.VIEW

                    '参照モード時
                    .imdHikiDate_From.ReadOnly = False
                    .imdHikiDate_To.ReadOnly = False
                    .cmbPrintShubetu.ReadOnly = False
                    .cmbPrintHinNm.ReadOnly = False
                    .btnPrint.Enabled = True

                    .cmbNrsBr.ReadOnly = True
                    .txtCustCdL.ReadOnly = True
                    .lblCustNmL.ReadOnly = True
                    .txtCustCdM.ReadOnly = True
                    .lblCustNmM.ReadOnly = True
                    .imdHikiDate.ReadOnly = True
                    .numMeisaiNo.ReadOnly = True
                    .cmbHinNm.ReadOnly = True
                    .txtHikitoriCd.ReadOnly = True
                    .lblHikitoriNm.ReadOnly = True
                    .numFcNb.ReadOnly = True
                    .numFcTanka.ReadOnly = True
                    .numFcTotal.ReadOnly = True
                    .numDmNb.ReadOnly = True
                    .numDmTanka.ReadOnly = True
                    .numDmTotal.ReadOnly = True
                    .numKisu.ReadOnly = True
                    .numSeihin.ReadOnly = True
                    .numSukurap.ReadOnly = True
                    .numWarimasi.ReadOnly = True
                    .numSeikei.ReadOnly = True
                    .numRosen.ReadOnly = True
                    .numKousoku.ReadOnly = True
                    .numSonota.ReadOnly = True
                    .numAllTotal.ReadOnly = True
                    .txtRemark.ReadOnly = True
                    .lblCrtUser.ReadOnly = True
                    .lblCrtDate.ReadOnly = True
                    .lblUpdUser.ReadOnly = True
                    .lblUpdDate.ReadOnly = True
                    .lblUpdateTime.ReadOnly = True

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロール初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub DefaultSetControl(ByVal sysDate As String)

        '編集部初期設定
        Call Me.SetDefault(sysDate)

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 初期日付設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl(ByVal sysDate As String)

        With Me._Frm

            Dim day As Integer = Convert.ToInt32(Mid(sysDate, 7, 2))
            Dim hikiDate As DateTime = Convert.ToDateTime(DateFormatUtility.EditSlash(String.Concat(Mid(sysDate, 1, 6), 21)))
            Dim recStatus As String = .lblSituation.RecordStatus

            If day < 21 Then
                .imdHikiDate_From.TextValue = hikiDate.AddMonths(-1).ToString("yyyyMMdd")
                .imdHikiDate_To.TextValue = hikiDate.AddDays(-1).ToString("yyyyMMdd")
            Else
                .imdHikiDate_From.TextValue = hikiDate.ToString("yyyyMMdd")
                .imdHikiDate_To.TextValue = hikiDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
            End If

        End With

    End Sub

    ''' <summary>
    ''' ナンバー型初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultNumber()

        With Me._Frm

            '数値項目に初期値0を設定する
            .numMeisaiNo.Value = 0
            .numFcNb.Value = 0
            .numFcTanka.Value = 0
            .numFcTotal.Value = 0
            .numDmNb.Value = 0
            .numDmTanka.Value = 0
            .numDmTotal.Value = 0
            .numKisu.Value = 0
            .numSeihin.Value = 0
            .numSukurap.Value = 0
            .numWarimasi.Value = 0
            .numSeikei.Value = 0
            .numRosen.Value = 0
            .numKousoku.Value = 0
            .numSonota.Value = 0
            .numAllTotal.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            .numMeisaiNo.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(0))
            .numFcNb.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numFcTanka.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numFcTotal.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(0))
            .numDmNb.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numDmTanka.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numDmTotal.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(0))
            .numKisu.SetInputFields("##0", , 3, 1, , 0, 0, , Convert.ToDecimal(999), Convert.ToDecimal(0))
            .numSeihin.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numSukurap.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numWarimasi.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numSeikei.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numRosen.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numKousoku.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numSonota.SetInputFields("#,###,##0", , 7, 1, , 0, 0, , Convert.ToDecimal(9999999), Convert.ToDecimal(0))
            .numAllTotal.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , Convert.ToDecimal(999999999), Convert.ToDecimal(0))

        End With

    End Sub

    ''' <summary>
    ''' 複写時の明細No初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMeisaiNo()

        With Me._Frm

            .numMeisaiNo.Value = 0

        End With
    End Sub

    ''' <summary>
    '''新規押下時の初期設定 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefault(ByVal sysDate As String)

        With Me._Frm

            .cmbNrsBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .imdHikiDate.TextValue = sysDate
            .txtCustCdL.TextValue = "00107"
            .txtCustCdM.TextValue = "00"

            '名称を取得し、ラベルに表示を行う
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue _
                                                                                                        , "' AND CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
            If 0 < dr.Length Then
                .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr(0).Item("CUST_NM_M").ToString()
            End If


        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlSinki()

        With Me._Frm

            .cmbNrsBr.SelectedValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .imdHikiDate.TextValue = String.Empty
            .numMeisaiNo.TextValue = String.Empty
            .cmbHinNm.TextValue = String.Empty
            .txtHikitoriCd.TextValue = String.Empty
            .lblHikitoriNm.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 合計を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetSumTotal(Optional ByVal eventShubetsu As LMI260C.EventShubetsu = Nothing) As Boolean

        With Me._Frm

            Dim fcTotal As Decimal = Convert.ToDecimal(.numFcTotal.Value)
            Dim dmTotal As Decimal = Convert.ToDecimal(.numDmTotal.Value)
            Dim allTotal As Decimal = Convert.ToDecimal(.numAllTotal.Value)
            Dim errFlg As Boolean = True

            Select Case eventShubetsu

                Case LMI260C.EventShubetsu.FC_TOTAL

                    'フレコン合計
                    fcTotal = Convert.ToDecimal( _
                             (Convert.ToDecimal(.numFcNb.Value) _
                            * Convert.ToDecimal(.numFcTanka.Value)))

                Case LMI260C.EventShubetsu.DM_TOTAL

                    'ドラム合計
                    dmTotal = Convert.ToDecimal( _
                             (Convert.ToDecimal(.numDmNb.Value) _
                            * Convert.ToDecimal(.numDmTanka.Value)))

                Case Else

                    'フレコン合計
                    fcTotal = Convert.ToDecimal( _
                             (Convert.ToDecimal(.numFcNb.Value) _
                            * Convert.ToDecimal(.numFcTanka.Value)))

                    'ドラム合計
                    dmTotal = Convert.ToDecimal( _
                             (Convert.ToDecimal(.numDmNb.Value) _
                            * Convert.ToDecimal(.numDmTanka.Value)))

            End Select

            '総合計
            allTotal = Convert.ToDecimal( _
                      (fcTotal _
                     + dmTotal _
                     + Convert.ToDecimal(.numSeihin.Value) _
                     + Convert.ToDecimal(.numSukurap.Value) _
                     + Convert.ToDecimal(.numWarimasi.Value) _
                     + Convert.ToDecimal(.numSeikei.Value) _
                     + Convert.ToDecimal(.numRosen.Value) _
                     + Convert.ToDecimal(.numKousoku.Value) _
                     + Convert.ToDecimal(.numSonota.Value)))

            '値をチェック
            If _V.TotalSumCheck(fcTotal, dmTotal, allTotal) = False Then
                Return False
            End If

            '値設定
            .numFcTotal.Value = fcTotal
            .numDmTotal.Value = dmTotal
            .numAllTotal.Value = allTotal

            Return True

        End With

    End Function

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.STATUS, "状態", 60, True)
        Public Shared HIKI_DATE As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.HIKI_DATE, "引取日", 100, True)
        Public Shared MEISAI_NO As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.MEISAI_NO, "明細NO", 60, True)
        Public Shared HIKITORI_CD As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.HIKI_CD, "取引先コード", 100, True)
        Public Shared HIKITORI_NM As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.HIKI_NM, "取引先名称", 200, True)
        Public Shared HIN_NM As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.HIN_NM, "品名", 80, True)
        Public Shared FC_NB As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.FC_NB, "F/B個数", 80, True)
        Public Shared FC_TANKA As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.FC_TANKA, "F/B単価", 80, True)
        Public Shared FC_TOTAL As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.FC_TOTAL, "F/B合計", 100, True)
        Public Shared DM_NB As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.DM_NB, "D/M個数", 80, True)
        Public Shared DM_TANKA As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.DM_TANKA, "D/M単価", 80, True)
        Public Shared DM_TOTAL As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.DM_TOTAL, "D/M合計", 100, True)
        Public Shared KISU As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.KISU, "基数", 60, True)
        Public Shared SEIHIN As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.SEIHIN, "製品引取", 80, True)
        Public Shared SUKURAP As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.SUKURAP, "スクラップ回収", 80, True)
        Public Shared WARIMASI As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.WARIMASI, "割増運賃", 80, True)
        Public Shared SEIKEI As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.SEIKEI, "成形品引取", 80, True)
        Public Shared ROSEN As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.ROSEN, "コンテナ", 80, True)
        Public Shared KOUSOKU As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.KOUSOKU, "高速料金", 80, True)
        Public Shared SONOTA As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.SONOTA, "その他", 80, True)
        Public Shared ALL_TOTAL As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.ALL_TOTAL, "総合計", 100, True)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CREATE_USER, "作成者", 100, True)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CREATE_DATE, "作成日", 100, True)
        Public Shared CREATE_TIME As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CREATE_TIME, "作成時間", 100, True)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.UPDATE_USER, "更新者", 100, True)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.UPDATE_DATE, "更新日", 100, True)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.UPDATE_TIME, "更新時間", 100, True)

        '**** 隠し列 ****
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.SYS_DEL_FLG, "", 50, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.NRS_BR_CD, "", 50, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CUST_CD_L, "", 50, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CUST_CD_M, "", 50, False)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CUST_NM_L, "", 50, False)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.CUST_NM_M, "", 50, False)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.REMARK, "", 50, False)
        Public Shared HIN_CD As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.HIN_CD, "", 50, False)
        Public Shared EXIST_UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMI260C.SprDetailColumnIndex.EXIST_UPDATE_TIME, "", 50, False)

    End Class

    ''' <summary>
    ''' SPREADの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitDetailSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMI260C.SprDetailColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.HIN_NM.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータのコントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        'Spreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail

        '検索行の設定を行う
        Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail)

        With sprDetail

            '**** 表示列 ****
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_S051, False))
            .SetCellStyle(0, sprDetailDef.HIKI_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.MEISAI_NO.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.HIKITORI_CD.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_HANKAKU, 15, False))
            .SetCellStyle(0, sprDetailDef.HIKITORI_NM.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.HIN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_H019, False))
            .SetCellStyle(0, sprDetailDef.FC_NB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FC_TANKA.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.FC_TOTAL.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.DM_NB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.DM_TANKA.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.DM_TOTAL.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KISU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIHIN.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SUKURAP.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.WARIMASI.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIKEI.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ROSEN.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KOUSOKU.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SONOTA.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.ALL_TOTAL.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CREATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPDATE_TIME.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            .SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_NM_L.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CUST_NM_M.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.REMARK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.HIN_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.EXIST_UPDATE_TIME.ColNo, lbl)

            If Me._ShokiFlg = True Then

                '初期値設定
                .SetCellValue(0, sprDetailDef.STATUS.ColNo, LMConst.FLG.OFF)
                .SetCellValue(0, sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
                Me._ShokiFlg = False

            End If

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr

            .SuspendLayout()

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
            Dim numNb As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.STATUS.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.HIKI_DATE.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.MEISAI_NO.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.HIKITORI_CD.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.HIKITORI_NM.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.HIN_NM.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.FC_NB.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.FC_TANKA.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.FC_TOTAL.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.DM_NB.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.DM_TANKA.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.DM_TOTAL.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.KISU.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.SEIHIN.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.SUKURAP.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.WARIMASI.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.SEIKEI.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.ROSEN.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.KOUSOKU.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.SONOTA.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.ALL_TOTAL.ColNo, numNb)
                .SetCellStyle(i, sprDetailDef.CREATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CREATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CREATE_TIME.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.UPDATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.UPDATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.UPDATE_TIME.ColNo, lblL)

                '**** 隠し列 ****
                .SetCellStyle(i, sprDetailDef.SYS_DEL_FLG.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.CUST_NM_M.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.REMARK.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.HIN_CD.ColNo, lblL)
                .SetCellStyle(i, sprDetailDef.EXIST_UPDATE_TIME.ColNo, lblL)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprDetailDef.HIKI_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("HIKI_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.MEISAI_NO.ColNo, dr.Item("MEISAI_NO").ToString())
                .SetCellValue(i, sprDetailDef.HIKITORI_CD.ColNo, dr.Item("HIKITORI_CD").ToString())
                .SetCellValue(i, sprDetailDef.HIKITORI_NM.ColNo, dr.Item("HIKITORI_NM").ToString())
                .SetCellValue(i, sprDetailDef.HIN_NM.ColNo, dr.Item("HIN_NM").ToString())
                .SetCellValue(i, sprDetailDef.FC_NB.ColNo, dr.Item("FC_NB").ToString())
                .SetCellValue(i, sprDetailDef.FC_TANKA.ColNo, dr.Item("FC_TANKA").ToString())
                .SetCellValue(i, sprDetailDef.FC_TOTAL.ColNo, dr.Item("FC_TOTAL").ToString())
                .SetCellValue(i, sprDetailDef.DM_NB.ColNo, dr.Item("DM_NB").ToString())
                .SetCellValue(i, sprDetailDef.DM_TANKA.ColNo, dr.Item("DM_TANKA").ToString())
                .SetCellValue(i, sprDetailDef.DM_TOTAL.ColNo, dr.Item("DM_TOTAL").ToString())
                .SetCellValue(i, sprDetailDef.KISU.ColNo, dr.Item("KISU").ToString())
                .SetCellValue(i, sprDetailDef.SEIHIN.ColNo, dr.Item("SEIHIN").ToString())
                .SetCellValue(i, sprDetailDef.SUKURAP.ColNo, dr.Item("SUKURAP").ToString())
                .SetCellValue(i, sprDetailDef.WARIMASI.ColNo, dr.Item("WARIMASI").ToString())
                .SetCellValue(i, sprDetailDef.SEIKEI.ColNo, dr.Item("SEIKEI").ToString())
                .SetCellValue(i, sprDetailDef.ROSEN.ColNo, dr.Item("ROSEN").ToString())
                .SetCellValue(i, sprDetailDef.KOUSOKU.ColNo, dr.Item("KOUSOKU").ToString())
                .SetCellValue(i, sprDetailDef.SONOTA.ColNo, dr.Item("SONOTA").ToString())
                .SetCellValue(i, sprDetailDef.ALL_TOTAL.ColNo, dr.Item("ALL_TOTAL").ToString())
                .SetCellValue(i, sprDetailDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, sprDetailDef.CREATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.CREATE_TIME.ColNo, Me.TimeFormatData(dr.Item("SYS_ENT_TIME").ToString()))
                .SetCellValue(i, sprDetailDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())
                .SetCellValue(i, sprDetailDef.UPDATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.UPDATE_TIME.ColNo, Me.TimeFormatData(dr.Item("SYS_UPD_TIME").ToString()))

                '**** 隠し列 ****
                .SetCellValue(i, sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprDetailDef.HIN_CD.ColNo, dr.Item("HIN_CD").ToString())
                .SetCellValue(i, sprDetailDef.EXIST_UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

            .cmbNrsBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.NRS_BR_CD.ColNo))
            .txtCustCdL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CUST_CD_L.ColNo))
            .lblCustNmL.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CUST_NM_L.ColNo))
            .txtCustCdM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CUST_CD_M.ColNo))
            .lblCustNmM.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CUST_NM_M.ColNo))
            .imdHikiDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.HIKI_DATE.ColNo)).Replace("/", "")
            .numMeisaiNo.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.MEISAI_NO.ColNo))
            .cmbHinNm.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.HIN_CD.ColNo))
            .txtHikitoriCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.HIKITORI_CD.ColNo))
            .lblHikitoriNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.HIKITORI_NM.ColNo))
            .numFcNb.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.FC_NB.ColNo))
            .numFcTanka.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.FC_TANKA.ColNo))
            .numFcTotal.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.FC_TOTAL.ColNo))
            .numDmNb.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.DM_NB.ColNo))
            .numDmTanka.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.DM_TANKA.ColNo))
            .numDmTotal.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.DM_TOTAL.ColNo))
            .numKisu.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.KISU.ColNo))
            .numSeihin.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.SEIHIN.ColNo))
            .numSukurap.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.SUKURAP.ColNo))
            .numWarimasi.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.WARIMASI.ColNo))
            .numSeikei.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.SEIKEI.ColNo))
            .numRosen.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.ROSEN.ColNo))
            .numKousoku.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.KOUSOKU.ColNo))
            .numSonota.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.SONOTA.ColNo))
            .numAllTotal.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.ALL_TOTAL.ColNo))
            .txtRemark.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.REMARK.ColNo))

            '共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CREATE_USER.ColNo))
            .lblCrtDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.CREATE_DATE.ColNo))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.UPDATE_USER.ColNo))
            .lblUpdDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.UPDATE_DATE.ColNo))

            '隠し項目
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI260G.sprDetailDef.EXIST_UPDATE_TIME.ColNo))


        End With

    End Sub

    ''' <summary>
    ''' 時間(コロン)編集
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>コロンを含む8桁を返却　空の場合、そのまま返却</returns>
    ''' <remarks></remarks>
    Private Function TimeFormatData(ByVal value As String) As String

        '空の場合、そのまま返却
        If String.IsNullOrEmpty(value) = True Then
            Return value
        End If

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

#End Region

#End Region

End Class
