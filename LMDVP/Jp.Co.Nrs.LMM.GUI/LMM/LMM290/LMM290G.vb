' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM290G : 振替対象商品マスタメンテ
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
''' LMM290Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM290G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM290F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM290F, ByVal g As LMMControlG)

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
            .F2ButtonName = String.Empty
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
            .F2ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

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
            .sprDetail.TabIndex = LMM290C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM290C.CtlTabIndex.NRSBRCD
            .txtCustCdL.TabIndex = LMM290C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMM290C.CtlTabIndex.CUSTCDM
            .txtCdCust.TabIndex = LMM290C.CtlTabIndex.CDCUST
            .txtCustCdLTo.TabIndex = LMM290C.CtlTabIndex.CUSTCDLTO
            .txtCustCdMTo.TabIndex = LMM290C.CtlTabIndex.CUSTCDMTO
            .txtCdCustTo.TabIndex = LMM290C.CtlTabIndex.CDCUSTTO
            .lblSituation.TabIndex = LMM290C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM290C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM290C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM290C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM290C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM290C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM290C.CtlTabIndex.SYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM290C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbBox()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks>ナンバー型・コンボボックスの設定など</remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me._LMMConG.ClearControl(Me._Frm)

    End Sub


    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm


            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)

                Case DispMode.EDIT

                    Select Case .lblSituation.RecordStatus

                        '新規
                        Case RecordStatus.NEW_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._LMMConG.LockText(.txtCustCdL, lock)
                            Call Me._LMMConG.LockText(.txtCustCdM, lock)
                            Call Me._LMMConG.LockText(.txtCdCust, lock)
                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

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
    Friend Sub SetFoucus(ByVal eventType As LMM290C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM290C.EventShubetsu.MAIN, LMM290C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM290C.EventShubetsu.SHINKI
                    .txtCustCdL.Focus()
                Case LMM290C.EventShubetsu.HUKUSHA
                    .txtCustCdLTo.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        Call Me._LMMConG.ClearControl(ctl)

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)




        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.NRS_BR_CD.ColNo).Text

            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_L.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_L.ColNo).Text
            .txtCustCdM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_M.ColNo).Text
            .lblCustNmM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_M.ColNo).Text
            .lblCustCdS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_S.ColNo).Text
            .lblCustNmS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_S.ColNo).Text
            .lblCustCdSS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_SS.ColNo).Text
            .lblCustNmSS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_SS.ColNo).Text
            .txtCdCust.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo).Text
            .lblNm1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.GOODS_NM_1.ColNo).Text
            .lblCdNrs.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CD_NRS.ColNo).Text
            .lblPkg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.PKG_UT_NM.ColNo).Text
            .lblIrime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.STD_IRIME.ColNo).Text
            .lblGoodsCustL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_L.ColNo).Text

            .txtCustCdLTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo).Text
            .lblCustNmLTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo).Text
            .txtCustCdMTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_M_TO.ColNo).Text
            .lblCustNmMTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_M_TO.ColNo).Text
            .lblCustCdSTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_S_TO.ColNo).Text
            .lblCustNmSTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_S_TO.ColNo).Text
            .lblCustCdSSTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_SS_TO.ColNo).Text
            .lblCustNmSSTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_NM_SS_TO.ColNo).Text
            .txtCdCustTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo).Text
            .lblNm1To.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo).Text
            .lblCdNrsTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CD_NRS_TO.ColNo).Text
            .lblPkgTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.PKG_UT_NM_TO.ColNo).Text
            .lblIrimeTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.STD_IRIME_TO.ColNo).Text
            .lblGoodsCustLTo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM290G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 150, True)
        Public Shared GOODS_NM_1 As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.GOODS_NM_1, "商品名", 150, True)
        Public Shared CUST_CD_L_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_L_TO, "振替先荷主コード" & vbCrLf & "(大)", 150, True)
        Public Shared CUST_NM_L_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_L_TO, "振替先荷主名(大)", 200, True)
        Public Shared GOODS_CD_CUST_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.GOODS_CD_CUST_TO, "振替先商品コード", 200, True)
        Public Shared GOODS_NM_1_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.GOODS_NM_1_TO, "振替先商品名", 200, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 50, False)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_M, "荷主名（中）", 50, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_S, "荷主コード（小）", 50, False)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_S, "荷主名（小）", 250, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_SS, "荷主コード（極小）", 50, False)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_SS, "荷主名（極小）", 50, False)
        Public Shared CD_NRS As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CD_NRS, "商品KEY", 50, False)
        Public Shared PKG_UT_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.PKG_UT_NM, "荷姿", 50, False)
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME_NB, "標準入目", 50, False)
        Public Shared STD_IRIME_UT_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME_UT_NM, "標準入目単位", 50, False)
        Public Shared STD_IRIME As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME, "入目", 50, False)
        Public Shared CUST_CD_M_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_M_TO, "振替先荷主コード（中）", 50, False)
        Public Shared CUST_NM_M_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_M_TO, "振替先荷主名（中）", 50, False)
        Public Shared CUST_CD_S_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_S_TO, "振替先荷主コード（小）", 50, False)
        Public Shared CUST_NM_S_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_S_TO, "振替先荷主名（小）", 50, False)
        Public Shared CUST_CD_SS_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_CD_SS_TO, "振替先荷主コード（極小）", 50, False)
        Public Shared CUST_NM_SS_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CUST_NM_SS_TO, "振替先荷主名（極小）", 50, False)
        Public Shared CD_NRS_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.CD_NRS_TO, "振替先商品KEY", 50, False)
        Public Shared PKG_UT_NM_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.PKG_UT_NM_TO, "振替先荷姿", 50, False)
        Public Shared STD_IRIME_NB_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME_NB_TO, "振替先標準入目", 50, False)
        Public Shared STD_IRIME_UT_NM_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME_UT_NM_TO, "振替先標準入目単位", 50, False)
        Public Shared STD_IRIME_TO As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.STD_IRIME_TO, "振替先入目", 50, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM290C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


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
            .sprDetail.ActiveSheet.ColumnCount = 40

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.CUST_NM_L.ColNo + 1
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.GOODS_NM_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_M.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_M.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_S.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_S.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_SS.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_SS.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CD_NRS.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.PKG_UT_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME_NB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME_UT_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_M_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_M_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_S_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_S_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_CD_SS_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CUST_NM_SS_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.CD_NRS_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.PKG_UT_NM_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME_NB_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME_UT_NM_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.STD_IRIME_TO.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM290G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)



        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM290F)

        With frm.sprDetail

            .SetCellValue(0, LMM290G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_L.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.GOODS_NM_1.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_M.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_M.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_S.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_S.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_SS.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_SS.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CD_NRS.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.PKG_UT_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME_NB.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME_UT_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_M_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_M_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_S_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_S_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_CD_SS_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CUST_NM_SS_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.CD_NRS_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.PKG_UT_NM_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME_NB_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME_UT_NM_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.STD_IRIME_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM290G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)


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
            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM290G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.GOODS_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_S.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_S.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_SS.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_SS.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.PKG_UT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME_NB.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME_UT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_M_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_M_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_S_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_S_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_CD_SS_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CUST_NM_SS_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.CD_NRS_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.PKG_UT_NM_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME_NB_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME_UT_NM_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.STD_IRIME_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM290G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM290G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.GOODS_NM_1.ColNo, dr.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo, dr.Item("CUST_CD_L_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo, dr.Item("CUST_NM_L_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo, dr.Item("GOODS_CD_CUST_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo, dr.Item("GOODS_NM_1_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_S.ColNo, dr.Item("CUST_NM_S").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_SS.ColNo, dr.Item("CUST_NM_SS").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CD_NRS.ColNo, dr.Item("CD_NRS").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.PKG_UT_NM.ColNo, dr.Item("PKG_UT_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME_NB.ColNo, dr.Item("STD_IRIME_NB").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME_UT_NM.ColNo, dr.Item("STD_IRIME_UT_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME.ColNo, dr.Item("STD_IRIME").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_M_TO.ColNo, dr.Item("CUST_CD_M_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_M_TO.ColNo, dr.Item("CUST_NM_M_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_S_TO.ColNo, dr.Item("CUST_CD_S_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_S_TO.ColNo, dr.Item("CUST_NM_S_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_CD_SS_TO.ColNo, dr.Item("CUST_CD_SS_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CUST_NM_SS_TO.ColNo, dr.Item("CUST_NM_SS_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.CD_NRS_TO.ColNo, dr.Item("CD_NRS_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.PKG_UT_NM_TO.ColNo, dr.Item("PKG_UT_NM_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME_NB_TO.ColNo, dr.Item("STD_IRIME_NB_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME_UT_NM_TO.ColNo, dr.Item("STD_IRIME_UT_NM_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.STD_IRIME_TO.ColNo, dr.Item("STD_IRIME_TO").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM290G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
