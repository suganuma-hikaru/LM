' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM030G : 作業項目マスタメンテ
'  作  成  者     : [金へスル]
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
''' LMM030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM030F
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

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
            .sprDetail.TabIndex = LMM030C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM030C.CtlTabIndex.NRSBRCD
            .txtCustCdL.TabIndex = LMM030C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMM030C.CtlTabIndex.CUSTNML
            .txtSagyoCd.TabIndex = LMM030C.CtlTabIndex.SAGYOCD
            .txtSagyoNm.TabIndex = LMM030C.CtlTabIndex.SAGYONM
            .txtSagyoRyak.TabIndex = LMM030C.CtlTabIndex.SAGYORYAK
            .cmbInvYn.TabIndex = LMM030C.CtlTabIndex.INVYN
            .cmbInvTani.TabIndex = LMM030C.CtlTabIndex.INVTANI
            .cmbKosuBai.TabIndex = LMM030C.CtlTabIndex.KOSUBAI
            .numSagyoUp.TabIndex = LMM030C.CtlTabIndex.SAGYOUP
            .cmbZeiKbn.TabIndex = LMM030C.CtlTabIndex.ZEIKBN
            .cmbSplRpt.TabIndex = LMM030C.CtlTabIndex.SPLRPT
            .cmbFlwpYn.TabIndex = LMM030C.CtlTabIndex.FLWPYN
            .txtRemark.TabIndex = LMM030C.CtlTabIndex.SAGYOREMARK
            .cmbWHSagyoYn.TabIndex = LMM030C.CtlTabIndex.TABYN
            .txtWhSagyoNm.TabIndex = LMM030C.CtlTabIndex.WHSAGYONM
            .txtWhSagyoRemark.TabIndex = LMM030C.CtlTabIndex.WHSAGYOREMARK
            .txtSagyoSubCd.TabIndex = LMM030C.CtlTabIndex.SAGYOSUBCD
            .lblSagyoSubNm.TabIndex = LMM030C.CtlTabIndex.SAGYOSUBNM
            .lblCustSubCdL.TabIndex = LMM030C.CtlTabIndex.CUSTSUBCDL
            .lblCustSubNmL.TabIndex = LMM030C.CtlTabIndex.CUSTSUBNML
            .lblSituation.TabIndex = LMM030C.CtlTabIndex.SITUATION
            .lblCrtUser.TabIndex = LMM030C.CtlTabIndex.CRTUSER
            .lblCrtDate.TabIndex = LMM030C.CtlTabIndex.CRTDATE
            .lblUpdUser.TabIndex = LMM030C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM030C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM030C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM030C.CtlTabIndex.SYSDELFLG


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
    Friend Sub UnLockedForm(ByVal eventType As LMM030C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()

        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d93 As Decimal = Convert.ToDecimal(999999999.999)

            'numberの桁数を設定する
            .numSagyoUp.SetInputFields("###,###,##0", , 9, 1, , 3, 3, , d93, , , , )

        End With

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCmbBox()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbInvYn.SelectedValue = LMM030C.YN
        Me._Frm.cmbKosuBai.SelectedValue = LMM030C.ZEI
        Me._Frm.cmbZeiKbn.SelectedValue = LMM030C.ZEI
        Me._Frm.cmbSplRpt.SelectedValue = LMM030C.YN
        Me._Frm.cmbFlwpYn.SelectedValue = LMM030C.YN

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
                            Me.SetLockControl(.txtSagyoCd, True)
                            Me.SetLockControl(.txtCustCdL, True)
                            Me.SetLockControl(.lblCustNmL, True)


                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me.SetLockControl(.txtSagyoCd, True)
                            Me.SetLockControl(.lblCustNmL, True)

                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me.SetLockControl(.txtSagyoCd, True)
                            Me.SetLockControl(.lblCustNmL, True)
                            Call Me.ClearControlFukusha()


                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM030C.EventShubetsu)
        With Me._Frm
            Select Case eventType
                Case LMM030C.EventShubetsu.MAIN, LMM030C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM030C.EventShubetsu.SHINKI
                    .txtCustCdL.Focus()
                Case LMM030C.EventShubetsu.HUKUSHA _
                     , LMM030C.EventShubetsu.HENSHU
                    .txtSagyoNm.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtSagyoCd.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = String.Empty
            .txtSagyoCd.TextValue = String.Empty
            .txtSagyoNm.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtSagyoRyak.TextValue = String.Empty
            .cmbInvYn.SelectedValue = String.Empty
            .cmbInvTani.SelectedValue = String.Empty
            .cmbKosuBai.SelectedValue = String.Empty
            .numSagyoUp.Value = 0
            .lblSagyoUpCurrCd.TextValue = String.Empty
            .cmbZeiKbn.SelectedValue = String.Empty
            .cmbSplRpt.SelectedValue = String.Empty
            .cmbFlwpYn.SelectedValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .cmbWHSagyoYn.SelectedValue = String.Empty
            .txtWhSagyoNm.TextValue = String.Empty
            .txtWhSagyoRemark.TextValue = String.Empty
            .txtSagyoSubCd.TextValue = String.Empty
            .lblSagyoSubNm.TextValue = String.Empty
            .lblCustSubCdL.TextValue = String.Empty
            .lblCustSubNmL.TextValue = String.Empty
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
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtSagyoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_CD.ColNo).Text
            .txtSagyoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_NM.ColNo).Text
            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.CUST_CD_L.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.CUST_NM_L.ColNo).Text
            .txtSagyoRyak.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo).Text
            .cmbInvYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.INV_YN.ColNo).Text
            .cmbInvTani.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.INV_TANI.ColNo).Text
            .cmbKosuBai.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.KOSU_BAI.ColNo).Text
            .numSagyoUp.Value = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_UP.ColNo).Text
            .lblSagyoUpCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_UP_CURR_CD.ColNo).Text
            .cmbZeiKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.ZEI_KBN.ColNo).Text
            .cmbSplRpt.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SPL_RPT.ColNo).Text
            .cmbFlwpYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.FLWP_YN.ColNo).Text
            .txtRemark.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_REMARK.ColNo).Text
            .cmbWHSagyoYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.WH_SAGYO_YN.ColNo).Text
            .txtWhSagyoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.WH_SAGYO_NM.ColNo).Text
            .txtWhSagyoRemark.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.WH_SAGYO_REMARK.ColNo).Text
            .txtSagyoSubCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo).Text
            .lblSagyoSubNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo).Text
            .lblCustSubCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.CUST_SUB_CD_L.ColNo).Text
            .lblCustSubNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.CUST_SUB_NM_L.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM030G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
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
        Call Me.SetSpread(ds.Tables(LMM030C.TABLE_NM_OUT))

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared SAGYO_CD As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_CD, "作業コード", 100, True)
        Public Shared SAGYO_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_NM, "作業項目名", 230, True)
        Public Shared SAGYO_RYAK As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_RYAK, "作業項目略称", 120, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 250, True)
        Public Shared INV_YN_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.INV_YN_NM, "請求", 60, True)
        Public Shared INV_YN As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.INV_YN, "請求有無区分", 60, False)
        Public Shared FLWP_YN_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.FLWP_YN_NM, "進捗管理", 70, True)
        Public Shared FLWP_YN As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.FLWP_YN, "進捗管理有無区分", 60, False)
        Public Shared SPL_RPT As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SPL_RPT, " 作業料明細纏め区分", 60, False)
        Public Shared INV_TANI As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.INV_TANI, "請求単位", 60, False)
        Public Shared KOSU_BAI As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.KOSU_BAI, "請求金額計算区分", 60, False)
        Public Shared SAGYO_UP As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_UP, "単価", 60, False)
        Public Shared SAGYO_UP_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_UP_CURR_CD, "単価通貨", 60, False)
        Public Shared ZEI_KBN As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.ZEI_KBN, "税区分", 60, False)
        Public Shared SAGYO_REMARK As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_REMARK, "備考", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared WH_SAGYO_YN_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.WH_SAGYO_YN_NM, "現場作業", 70, True)
        Public Shared WH_SAGYO_YN As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.WH_SAGYO_YN, "現場作業有無区分", 60, False)
        Public Shared WH_SAGYO_REMARK As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.WH_SIJI_REMARK, "現場作業備考", 0, False)
        Public Shared WH_SAGYO_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.WH_SAGYO_NM, "現場作業名", 0, False)
        Public Shared SAGYO_SUB_CD As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_SUB_CD, "協力会社作業コード", 100, True)
        Public Shared SAGYO_SUB_NM As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.SAGYO_SUB_NM, "協力会社作業項目名", 230, True)
        Public Shared CUST_SUB_CD_L As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.CUST_SUB_CD_L, "協力会社荷主コード", 100, False)
        Public Shared CUST_SUB_NM_L As SpreadColProperty = New SpreadColProperty(LMM030C.SprColumnIndex.CUST_SUB_NM_L, "協力会社荷主名(大)", 250, False)
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
            .sprDetail.ActiveSheet.ColumnCount = 34

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM030G.sprDetailDef())
            .sprDetail.SetColProperty(New LMM030G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM030G.sprDetailDef.SAGYO_NM.ColNo + 1

            '列設定用変数
            Dim umuStyle As StyleInfo = Me.StyleInfoUmukbn(.sprDetail)
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 6, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.INV_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.INV_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.FLWP_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.FLWP_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.WH_SAGYO_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SPL_RPT.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.INV_TANI.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.KOSU_BAI.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_UP.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, InputControl.HAN_NUMBER, 2, True))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_UP_CURR_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.ZEI_KBN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_REMARK.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.WH_SAGYO_REMARK.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.WH_SAGYO_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.CUST_SUB_NM_L.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM030G.sprDetailDef.CUST_SUB_CD_L.ColNo, lblStyle)

        End With

    End Sub


    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm.sprDetail

            .SetCellValue(0, LMM030G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM030G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM030G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.CUST_NM_L.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.INV_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.INV_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.FLWP_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.FLWP_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.WH_SAGYO_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SPL_RPT.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.INV_TANI.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.KOSU_BAI.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_UP.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_UP_CURR_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.ZEI_KBN.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_REMARK.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.WH_SAGYO_REMARK.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.WH_SAGYO_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.CUST_SUB_CD_L.ColNo, String.Empty)
            .SetCellValue(0, LMM030G.sprDetailDef.CUST_SUB_NM_L.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet()
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

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM030G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.INV_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.INV_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.FLWP_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.FLWP_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.WH_SAGYO_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SPL_RPT.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.INV_TANI.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.KOSU_BAI.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_UP.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_UP_CURR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.ZEI_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.WH_SAGYO_REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.WH_SAGYO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.CUST_SUB_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM030G.sprDetailDef.CUST_SUB_NM_L.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM030G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_CD.ColNo, dr.Item("SAGYO_CD").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_NM.ColNo, dr.Item("SAGYO_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_RYAK.ColNo, dr.Item("SAGYO_RYAK").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.INV_YN_NM.ColNo, dr.Item("INV_YN_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.INV_YN.ColNo, dr.Item("INV_YN").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.FLWP_YN_NM.ColNo, dr.Item("FLWP_YN_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.FLWP_YN.ColNo, dr.Item("FLWP_YN").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, dr.Item("WH_SAGYO_YN_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.WH_SAGYO_YN.ColNo, dr.Item("WH_SAGYO_YN").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SPL_RPT.ColNo, dr.Item("SPL_RPT").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.INV_TANI.ColNo, dr.Item("INV_TANI").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.KOSU_BAI.ColNo, dr.Item("KOSU_BAI").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_UP.ColNo, dr.Item("SAGYO_UP").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_UP_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.ZEI_KBN.ColNo, dr.Item("ZEI_KBN").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_REMARK.ColNo, dr.Item("SAGYO_REMARK").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.WH_SAGYO_REMARK.ColNo, dr.Item("WH_SAGYO_REMARK").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.WH_SAGYO_NM.ColNo, dr.Item("WH_SAGYO_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_SUB_CD.ColNo, dr.Item("SAGYO_CD_SUB").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.SAGYO_SUB_NM.ColNo, dr.Item("SAGYO_NM_SUB").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.CUST_SUB_CD_L.ColNo, dr.Item("CUST_CD_L_SUB").ToString())
                .SetCellValue(i, LMM030G.sprDetailDef.CUST_SUB_NM_L.ColNo, dr.Item("CUST_NM_L_SUB").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "部品化検討中"

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoUmukbn(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM2" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_U009} _
                                                  )

    End Function

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtSagyoCd, lock)
            Me.SetLockControl(.txtSagyoNm, lock)
            Me.SetLockControl(.txtCustCdL, lock)
            Me.SetLockControl(.lblCustNmL, lock)
            Me.SetLockControl(.txtSagyoRyak, lock)
            Me.SetLockControl(.cmbInvYn, lock)
            Me.SetLockControl(.cmbInvTani, lock)
            Me.SetLockControl(.cmbKosuBai, lock)
            Me.SetLockControl(.numSagyoUp, lock)
            Me.SetLockControl(.cmbZeiKbn, lock)
            Me.SetLockControl(.cmbSplRpt, lock)
            Me.SetLockControl(.cmbFlwpYn, lock)
            Me.SetLockControl(.txtRemark, lock)
            Me.SetLockControl(.cmbWHSagyoYn, lock)
            Me.SetLockControl(.txtWhSagyoNm, lock)
            Me.SetLockControl(.txtWhSagyoRemark, lock)
            Me.SetLockControl(.txtSagyoSubCd, lock)
            Me.SetLockControl(.lblSagyoSubNm, lock)
            Me.SetLockControl(.lblCustSubCdL, lock)
            Me.SetLockControl(.lblCustSubNmL, lock)
        End With

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

#End Region

End Class
