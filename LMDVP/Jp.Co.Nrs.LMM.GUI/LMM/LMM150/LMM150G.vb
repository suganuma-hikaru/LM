' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM150G : 請求テンプレートマスタメンテ
'  作  成  者     : [kishi]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM150Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM150G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM150F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM150F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMMConG = g

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

#Region "Form"

#Region "設定・制御"

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

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM150C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM150C.CtlTabIndex.NRSBRCD
            .txtWhCd.TabIndex = LMM150C.CtlTabIndex.WHCD
            .txtWhNm.TabIndex = LMM150C.CtlTabIndex.WHNM
            .txtZip.TabIndex = LMM150C.CtlTabIndex.ZIP
            .txtAd1.TabIndex = LMM150C.CtlTabIndex.AD1
            .txtAd2.TabIndex = LMM150C.CtlTabIndex.AD2
            .txtAd3.TabIndex = LMM150C.CtlTabIndex.AD3
            .cmbJtsFlg.TabIndex = LMM150C.CtlTabIndex.JITASHAFLG
            .txtTel.TabIndex = LMM150C.CtlTabIndex.TEL
            .txtFax.TabIndex = LMM150C.CtlTabIndex.FAX
            .txtJis.TabIndex = LMM150C.CtlTabIndex.JIS
            .lblKen.TabIndex = LMM150C.CtlTabIndex.KEN
            .lblShi.TabIndex = LMM150C.CtlTabIndex.SHI
            .numNihudaMxCnt.TabIndex = LMM150C.CtlTabIndex.MXCNT
            .grpStage.TabIndex = LMM150C.CtlTabIndex.STAGE
            .cmbInkaYotei.TabIndex = LMM150C.CtlTabIndex.INKAYOTEI
            .cmbInkaUkePrt.TabIndex = LMM150C.CtlTabIndex.INKAUKEPRT
            .cmbInkaKenpin.TabIndex = LMM150C.CtlTabIndex.INKAKENPIN
            .cmbInkaKakunin.TabIndex = LMM150C.CtlTabIndex.INKAKAKUNIN
            .cmbInkaInfo.TabIndex = LMM150C.CtlTabIndex.INKAINFO
            'START YANAI 要望番号394
            .cmbOutkaYotei.TabIndex = LMM150C.CtlTabIndex.OUTKAYOTEI
            'END YANAI 要望番号394
            .cmbOutkaSashizuPrt.TabIndex = LMM150C.CtlTabIndex.OUTKASASHIZUPRT
            .cmbOutkaKanryo.TabIndex = LMM150C.CtlTabIndex.OUTKAKANRYO
            .cmbOutkaKenpin.TabIndex = LMM150C.CtlTabIndex.OUTKAKENPIN
            .cmbOutkaInfo.TabIndex = LMM150C.CtlTabIndex.OUTKAINFO
            .grpChk.TabIndex = LMM150C.CtlTabIndex.CHK
            .cmbLocManager.TabIndex = LMM150C.CtlTabIndex.LOCMANAGER
            .cmbTouKanri.TabIndex = LMM150C.CtlTabIndex.TOUKANRI
            .cmbTouhanSashizu.TabIndex = LMM150C.CtlTabIndex.TOUHANSASHIZU
            'START KIM 2012/09/12 要望番号1404 
            .cmbGoodslotCheckYN.TabIndex = LMM150C.CtlTabIndex.GOODSLOTCHECKYN
            'END KIM 2012/09/12 要望番号1404 

            .lblSituation.TabIndex = LMM150C.CtlTabIndex.SITUATION
            .lblCrtUser.TabIndex = LMM150C.CtlTabIndex.CRTUSER
            .lblCrtDate.TabIndex = LMM150C.CtlTabIndex.CRTDATE
            .lblUpdUser.TabIndex = LMM150C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM150C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM150C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM150C.CtlTabIndex.SYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM150C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbJtsFlg.SelectedValue = LMM150C.JITASHAFLG
            .cmbInkaYotei.SelectedValue = LMM150C.UMUFLG
            .cmbInkaUkePrt.SelectedValue = LMM150C.UMUFLG
            .cmbInkaKenpin.SelectedValue = LMM150C.UMUFLG
            .cmbInkaKakunin.SelectedValue = LMM150C.UMUFLG
            .cmbInkaInfo.SelectedValue = LMM150C.UMUFLG
            'START YANAI 要望番号394
            .cmbOutkaYotei.SelectedValue = LMM150C.UMUFLG
            'END YANAI 要望番号394
            .cmbOutkaSashizuPrt.SelectedValue = LMM150C.UMUFLG
            .cmbOutkaKanryo.SelectedValue = LMM150C.UMUFLG
            .cmbOutkaKenpin.SelectedValue = LMM150C.UMUFLG
            .cmbOutkaInfo.SelectedValue = LMM150C.UMUFLG
            .cmbLocManager.SelectedValue = LMM150C.UMUFLG
            .cmbTouKanri.SelectedValue = LMM150C.UMUFLG
            .cmbTouhanSashizu.SelectedValue = LMM150C.UMUFLG
            'START KIM 2012/09/12 要望番号1404 
            .cmbGoodslotCheckYN.SelectedValue = LMM150C.UMUFLG
            'END KIM 2012/09/12 要望番号1404 

            .numNihudaMxCnt.Value = 99

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
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d999 As Decimal = Convert.ToDecimal(999)
            
            ''numberの桁数を設定する
            .numNihudaMxCnt.SetInputFields("##0", , 3, 1, , 0, 0, , d999, 0, , , , )

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
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me.SetLockControl(.txtWhCd, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)

                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtWhCd.TextValue = String.Empty
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
    Friend Sub SetFoucus(ByVal eventType As LMM150C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM150C.EventShubetsu.MAIN, LMM150C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM150C.EventShubetsu.SHINKI, LMM150C.EventShubetsu.HUKUSHA
                    .txtWhCd.Focus()
                Case LMM150C.EventShubetsu.HENSHU
                    .txtWhNm.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = String.Empty
            .txtWhCd.TextValue = String.Empty
            .txtWhNm.TextValue = String.Empty
            .txtZip.TextValue = String.Empty
            .txtAd1.TextValue = String.Empty
            .txtAd2.TextValue = String.Empty
            .txtAd3.TextValue = String.Empty
            .cmbJtsFlg.SelectedValue = String.Empty
            .txtTel.TextValue = String.Empty
            .txtFax.TextValue = String.Empty
            .txtJis.TextValue = String.Empty
            .lblKen.TextValue = String.Empty
            .lblShi.TextValue = String.Empty
            .numNihudaMxCnt.Value = 0
            .cmbInkaYotei.SelectedValue = String.Empty
            .cmbInkaUkePrt.SelectedValue = String.Empty
            .cmbInkaKenpin.SelectedValue = String.Empty
            .cmbInkaKakunin.SelectedValue = String.Empty
            .cmbInkaInfo.SelectedValue = String.Empty
            'START YANAI 要望番号394
            .cmbOutkaYotei.SelectedValue = String.Empty
            'END YANAI 要望番号394
            .cmbOutkaSashizuPrt.SelectedValue = String.Empty
            .cmbOutkaKanryo.SelectedValue = String.Empty
            .cmbOutkaKenpin.SelectedValue = String.Empty
            .cmbOutkaInfo.SelectedValue = String.Empty
            .cmbLocManager.SelectedValue = String.Empty
            .cmbTouKanri.SelectedValue = String.Empty
            .cmbTouhanSashizu.SelectedValue = String.Empty
            'START KIM 2012/09/12 要望番号1404 
            .cmbGoodslotCheckYN.SelectedValue = String.Empty
            'END KIM 2012/09/12 要望番号1404 
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
            .lblSysDelFlg.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtWhCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.WH_CD.ColNo).Text
            .txtWhNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.WH_NM.ColNo).Text
            .txtZip.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.ZIP.ColNo).Text
            .txtAd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.AD_1.ColNo).Text
            .txtAd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.AD_2.ColNo).Text
            .txtAd3.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.AD_3.ColNo).Text
            .cmbJtsFlg.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.WH_KB.ColNo).Text
            .txtTel.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.TEL.ColNo).Text
            .txtFax.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.FAX.ColNo).Text
            .txtJis.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.JIS_CD.ColNo).Text
            .lblKen.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.KEN.ColNo).Text
            .lblShi.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SHI.ColNo).Text
            .numNihudaMxCnt.Value = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.NIHUDA_MX_CNT.ColNo).Text
            .cmbInkaYotei.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.INKA_YOTEI_YN.ColNo).Text
            .cmbInkaUkePrt.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.INKA_UKE_PRT_YN.ColNo).Text
            .cmbInkaKenpin.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.INKA_KENPIN_YN.ColNo).Text
            .cmbInkaKakunin.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.INKA_KAKUNIN_YN.ColNo).Text
            .cmbInkaInfo.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.INKA_INFO_YN.ColNo).Text
            'START YANAI 要望番号394
            .cmbOutkaYotei.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.OUTKA_YOTEI_YN.ColNo).Text
            'END YANAI 要望番号394
            .cmbOutkaSashizuPrt.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo).Text
            .cmbOutkaKanryo.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo).Text
            .cmbOutkaKenpin.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.OUTKA_KENPIN_YN.ColNo).Text
            .cmbOutkaInfo.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.OUTKA_INFO_YN.ColNo).Text
            .cmbLocManager.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.LOC_MANAGER_YN.ColNo).Text
            .cmbTouKanri.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.TOU_KANRI_YN.ColNo).Text
            .cmbTouhanSashizu.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.TOUHAN_SASHIZU_YN.ColNo).Text
            'START KIM 2012/09/12 要望番号1404 
            .cmbGoodslotCheckYN.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.GOODSLOT_CHECK_YN.ColNo).Text
            'END KIM 2012/09/12 要望番号1404 
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM150G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

        End With

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.WH_CD, "倉庫コード", 50, False)
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.WH_NM, "倉庫", 250, True)
        Public Shared TEL As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.TEL, "電話番号", 100, True)
        Public Shared FAX As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.FAX, "FAX番号", 100, True)
        Public Shared ZIP As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.ZIP, "郵便番号", 50, False)
        Public Shared AD_1 As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.AD_1, "住所1", 50, False)
        Public Shared AD_2 As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.AD_2, "住所2", 50, False)
        Public Shared AD_3 As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.AD_3, "住所3", 50, False)
        Public Shared WH_KB As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.WH_KB, "倉庫区分", 50, False)
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.JIS_CD, "JISコード", 50, False)
        Public Shared KEN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.KEN, "都道府県名", 50, False)
        Public Shared SHI As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SHI, "市区町村名", 50, False)
        Public Shared NIHUDA_MX_CNT As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.NIHUDA_MX_CNT, "荷札最大枚数", 50, False)
        Public Shared INKA_YOTEI_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.INKA_YOTEI_YN, "入荷予定制御有無", 50, False)
        Public Shared INKA_UKE_PRT_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.INKA_UKE_PRT_YN, "入荷受付票印刷制御有無", 50, False)
        Public Shared INKA_KENPIN_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.INKA_KENPIN_YN, "入荷検品制御有無", 50, False)
        Public Shared INKA_KAKUNIN_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.INKA_KAKUNIN_YN, "入荷確認制御有無", 50, False)
        Public Shared INKA_INFO_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.INKA_INFO_YN, "入荷報告制御有無", 50, False)
        'START YANAI 要望番号394
        Public Shared OUTKA_YOTEI_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.OUTKA_YOTEI_YN, "出荷予定制御有無", 50, False)
        'END YANAI 要望番号394
        Public Shared OUTKA_SASHIZU_PRT_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.OUTKA_SASHIZU_PRT_YN, "出荷指図書印刷制御有無", 50, False)
        Public Shared OUTOKA_KANRYO_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.OUTOKA_KANRYO_YN, "出庫完了制御有無", 50, False)
        Public Shared OUTKA_KENPIN_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.OUTKA_KENPIN_YN, "出荷検品制御有無", 50, False)
        Public Shared OUTKA_INFO_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.OUTKA_INFO_YN, "出荷報告制御有無", 50, False)
        Public Shared LOC_MANAGER_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.LOC_MANAGER_YN, "ロケーション管理有無", 50, False)
        Public Shared TOU_KANRI_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.TOU_KANRI_YN, "棟管理有無", 50, False)
        Public Shared TOUHAN_SASHIZU_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.TOUHAN_SASHIZU_YN, "棟班別出荷指図有無", 50, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        'START KIM 2012/09/12 要望番号1404 
        Public Shared GOODSLOT_CHECK_YN As SpreadColProperty = New SpreadColProperty(LMM150C.SprColumnIndex.GOODSLOT_CHECK_YN, "商品・ロット違い管理有無", 50, False)
        'END KIM 2012/09/12 要望番号1404 

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
            'START KIM 2012/09/12 要望番号1404 

            'START YANAI 要望番号394
            '.sprDetail.ActiveSheet.ColumnCount = 35
            '.sprDetail.ActiveSheet.ColumnCount = 36
            'END YANAI 要望番号394
            .sprDetail.ActiveSheet.ColumnCount = 37
            'END KIM 2012/09/12 要望番号1404 

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New LMM150G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.DEF.ColNo + 1
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)

            '2017/09/25 修正 李↓
            '.sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.WH_NM.ColNo, Me.StyleInfoCombSoko(.sprDetail, String.Empty))
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.WH_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            '2017/09/25 修正 李↑

            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.WH_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.TEL.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 20, False))
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.FAX.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 20, False))
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.ZIP.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.AD_1.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.AD_2.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.AD_3.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.WH_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.JIS_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.KEN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SHI.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.NIHUDA_MX_CNT.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.INKA_YOTEI_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.INKA_UKE_PRT_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.INKA_KENPIN_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.INKA_KAKUNIN_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.INKA_INFO_YN.ColNo, lblStyle)
            'START YANAI 要望番号394
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.OUTKA_YOTEI_YN.ColNo, lblStyle)
            'END YANAI 要望番号394
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.OUTKA_KENPIN_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.OUTKA_INFO_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.LOC_MANAGER_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.TOU_KANRI_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.TOUHAN_SASHIZU_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)
            'START KIM 2012/09/12 要望番号1404 
            .sprDetail.SetCellStyle(0, LMM150G.sprDetailDef.GOODSLOT_CHECK_YN.ColNo, lblStyle)
            'END KIM 2012/09/12 要望番号1404 

            'セルにイベント追加
            Dim aCell As Cell = .sprDetail.ActiveSheet.Cells(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo)
            Dim combCell As FarPoint.Win.Spread.CellType.ComboBoxCellType = DirectCast(aCell.Editor, FarPoint.Win.Spread.CellType.ComboBoxCellType)
            AddHandler combCell.EditingStopped, AddressOf Me.NrsBrCellEditingStopped

        End With

    End Sub
    
    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm.sprDetail

            .SetCellValue(0, LMM150G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM150G.sprDetailDef.WH_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.WH_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.TEL.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.FAX.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.ZIP.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.AD_1.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.AD_2.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.AD_3.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.WH_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.JIS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.KEN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SHI.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.NIHUDA_MX_CNT.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.INKA_YOTEI_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.INKA_UKE_PRT_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.INKA_KENPIN_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.INKA_KAKUNIN_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.INKA_INFO_YN.ColNo, String.Empty)
            'START YANAI 要望番号394
            .SetCellValue(0, LMM150G.sprDetailDef.OUTKA_YOTEI_YN.ColNo, String.Empty)
            'END YANAI 要望番号394
            .SetCellValue(0, LMM150G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.OUTKA_KENPIN_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.OUTKA_INFO_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.LOC_MANAGER_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.TOU_KANRI_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.TOUHAN_SASHIZU_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM150G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            'START KIM 2012/09/12 要望番号1404 
            .SetCellValue(0, LMM150G.sprDetailDef.GOODSLOT_CHECK_YN.ColNo, String.Empty)
            'END KIM 2012/09/12 要望番号1404 

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet
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
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sMxCnt As StyleInfo = Me.StyleInfoNum3(spr, lock)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM150G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.WH_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.TEL.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.FAX.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.ZIP.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.AD_1.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.AD_2.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.AD_3.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.WH_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.KEN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SHI.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.NIHUDA_MX_CNT.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.INKA_YOTEI_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.INKA_UKE_PRT_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.INKA_KENPIN_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.INKA_KAKUNIN_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.INKA_INFO_YN.ColNo, sLabel)
                'START YANAI 要望番号394
                .SetCellStyle(i, LMM150G.sprDetailDef.OUTKA_YOTEI_YN.ColNo, sLabel)
                'END YANAI 要望番号394
                .SetCellStyle(i, LMM150G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.OUTKA_KENPIN_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.OUTKA_INFO_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.LOC_MANAGER_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.TOU_KANRI_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.TOUHAN_SASHIZU_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM150G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                'START KIM 2012/09/12 要望番号1404 
                .SetCellStyle(i, LMM150G.sprDetailDef.GOODSLOT_CHECK_YN.ColNo, sLabel)
                'END KIM 2012/09/12 要望番号1404 


                'セルに値を設定
                .SetCellValue(i, LMM150G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.FAX.ColNo, dr.Item("FAX").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.AD_1.ColNo, dr.Item("AD_1").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.AD_2.ColNo, dr.Item("AD_2").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.AD_3.ColNo, dr.Item("AD_3").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.WH_KB.ColNo, dr.Item("WH_KB").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.JIS_CD.ColNo, dr.Item("JIS_CD").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.KEN.ColNo, dr.Item("KEN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SHI.ColNo, dr.Item("SHI").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.NIHUDA_MX_CNT.ColNo, dr.Item("NIHUDA_MX_CNT").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.INKA_YOTEI_YN.ColNo, dr.Item("INKA_YOTEI_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.INKA_UKE_PRT_YN.ColNo, dr.Item("INKA_UKE_PRT_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.INKA_KENPIN_YN.ColNo, dr.Item("INKA_KENPIN_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.INKA_KAKUNIN_YN.ColNo, dr.Item("INKA_KAKUNIN_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.INKA_INFO_YN.ColNo, dr.Item("INKA_INFO_YN").ToString())
                'START YANAI 要望番号394
                .SetCellValue(i, LMM150G.sprDetailDef.OUTKA_YOTEI_YN.ColNo, dr.Item("OUTKA_YOTEI_YN").ToString())
                'END YANAI 要望番号394
                .SetCellValue(i, LMM150G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, dr.Item("OUTKA_SASHIZU_PRT_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo, dr.Item("OUTOKA_KANRYO_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.OUTKA_KENPIN_YN.ColNo, dr.Item("OUTKA_KENPIN_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.OUTKA_INFO_YN.ColNo, dr.Item("OUTKA_INFO_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.LOC_MANAGER_YN.ColNo, dr.Item("LOC_MANAGER_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.TOU_KANRI_YN.ColNo, dr.Item("TOU_KANRI_YN").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.TOUHAN_SASHIZU_YN.ColNo, dr.Item("TOUHAN_SASHIZU_YN").ToString())

                .SetCellValue(i, LMM150G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM150G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                'START KIM 2012/09/12 要望番号1404 
                .SetCellValue(i, LMM150G.sprDetailDef.GOODSLOT_CHECK_YN.ColNo, dr.Item("GOODSLOT_CHECK_YN").ToString())
                'END KIM 2012/09/12 要望番号1404 

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 営業所に紐付く倉庫コンボを設定
    ''' </summary>
    ''' <param name="sender">イベント発生オブジェクト</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Private Sub NrsBrCellEditingStopped(ByVal sender As Object, ByVal e As System.EventArgs)

        '選択した営業所情報を反映
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim brCd As String = Me._LMMConG.GetCellValue(spr.ActiveSheet.Cells(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo))
        spr.SetCellStyle(0, LMM150G.sprDetailDef.WH_NM.ColNo, Me.StyleInfoCombSoko(spr, brCd))

        '設定したリストがある場合、始めのリストを設定
        Dim aCell As Cell = spr.ActiveSheet.Cells(0, LMM150G.sprDetailDef.WH_NM.ColNo)
        Dim combCell As FarPoint.Win.Spread.CellType.ComboBoxCellType = DirectCast(aCell.Editor, FarPoint.Win.Spread.CellType.ComboBoxCellType)
        If 1 < combCell.Items.Count Then
            spr.SetCellValue(0, LMM150G.sprDetailDef.WH_NM.ColNo, combCell.ItemData(1))
        End If

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(倉庫コンボ)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoCombSoko(ByVal spr As LMSpread, ByVal brCd As String) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False, "NRS_BR_CD", brCd)

    End Function

#End Region 'Spread

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtWhCd, lock)
            Me.SetLockControl(.txtWhNm, lock)
            Me.SetLockControl(.txtZip, lock)
            Me.SetLockControl(.txtAd1, lock)
            Me.SetLockControl(.txtAd2, lock)
            Me.SetLockControl(.txtAd3, lock)
            Me.SetLockControl(.cmbJtsFlg, lock)
            Me.SetLockControl(.txtTel, lock)
            Me.SetLockControl(.txtFax, lock)
            Me.SetLockControl(.txtJis, lock)
            Me.SetLockControl(.lblKen, lock)
            Me.SetLockControl(.lblShi, lock)
            Me.SetLockControl(.numNihudaMxCnt, lock)
            Me.SetLockControl(.cmbInkaYotei, lock)
            Me.SetLockControl(.cmbInkaUkePrt, lock)
            Me.SetLockControl(.cmbInkaKenpin, lock)
            Me.SetLockControl(.cmbInkaKakunin, lock)
            Me.SetLockControl(.cmbInkaInfo, lock)
            'START YANAI 要望番号394
            Me.SetLockControl(.cmbOutkaYotei, lock)
            'END YANAI 要望番号394
            Me.SetLockControl(.cmbOutkaSashizuPrt, lock)
            Me.SetLockControl(.cmbOutkaKanryo, lock)
            Me.SetLockControl(.cmbOutkaKenpin, lock)
            Me.SetLockControl(.cmbOutkaInfo, lock)
            Me.SetLockControl(.cmbLocManager, lock)
            Me.SetLockControl(.cmbTouKanri, lock)
            Me.SetLockControl(.cmbTouhanSashizu, lock)
            'START KIM 2012/09/12 要望番号1404 
            Me.SetLockControl(.cmbGoodslotCheckYN, lock)
            'END KIM 2012/09/12 要望番号1404 

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

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
