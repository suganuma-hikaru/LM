' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM160G : 届先商品マスタメンテナンス
'  作  成  者     : [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMM160Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM160G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM160F
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM160F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
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
            .sprDetail.TabIndex = LMM160C.CtlTabIndex.DETAIL
            .txtCustCdL.TabIndex = LMM160C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMM160C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LMM160C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LMM160C.CtlTabIndex.CUSTNMM
            .txtDestCd.TabIndex = LMM160C.CtlTabIndex.DESTCD
            .lblDestNm.TabIndex = LMM160C.CtlTabIndex.DESTNM
            .txtGoodsCd.TabIndex = LMM160C.CtlTabIndex.GOODSCD
            .lblGoodsNm.TabIndex = LMM160C.CtlTabIndex.GOODSNM
            .lblGoodsNrs.TabIndex = LMM160C.CtlTabIndex.GOODSNRS
            .txtDelverGoodsNm.TabIndex = LMM160C.CtlTabIndex.DELVERGOODSNM
            .txtWorkSeiqCd.TabIndex = LMM160C.CtlTabIndex.WORKDEMANDCD
            .lblWorkSeiqNm.TabIndex = LMM160C.CtlTabIndex.WORKDEMANDNM
            .txtWork1Kb.TabIndex = LMM160C.CtlTabIndex.WORK1KB
            .lblWork1Nm.TabIndex = LMM160C.CtlTabIndex.WORK1NM
            .txtWork2Kb.TabIndex = LMM160C.CtlTabIndex.WORK2KB
            .lblWork2Nm.TabIndex = LMM160C.CtlTabIndex.WORK2NM
            .lblSituation.TabIndex = LMM160C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM160C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM160C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM160C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM160C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM160C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM160C.CtlTabIndex.SYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()


        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM160C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub
    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbNrsBrCd()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()

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
                            Me.SetLockControl(.txtCustCdL, True)
                            Me.SetLockControl(.txtCustCdM, True)
                            Me.SetLockControl(.txtDestCd, True)
                            Me.SetLockControl(.txtGoodsCd, True)
                            Me.SetLockControl(.cmbNrsBrCd, True)

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
    Friend Sub SetFoucus(ByVal eventType As LMM160C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM160C.EventShubetsu.MAIN, LMM160C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM160C.EventShubetsu.SHINKI, LMM160C.EventShubetsu.HUKUSHA
                    .txtCustCdL.Focus()
                Case LMM160C.EventShubetsu.HENSHU
                    .txtDelverGoodsNm.Focus()

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
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .txtGoodsCd.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .lblGoodsNrs.TextValue = String.Empty
            .txtDelverGoodsNm.TextValue = String.Empty
            .txtWorkSeiqCd.TextValue = String.Empty
            .lblWorkSeiqNm.TextValue = String.Empty
            .txtWork1Kb.TextValue = String.Empty
            .txtWork2Kb.TextValue = String.Empty
            .lblWork1Nm.TextValue = String.Empty
            .lblWork2Nm.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
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
            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.CUST_CD_L.ColNo).Text
            .txtCustCdM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.CUST_CD_M.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.CUST_NM_L.ColNo).Text
            .lblCustNmM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.CUST_NM_M.ColNo).Text
            .txtDestCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.CD.ColNo).Text
            .lblDestNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.DEST_NM.ColNo).Text
            .txtGoodsCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.GOODS_CD.ColNo).Text
            .lblGoodsNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.GOODS_NM_1.ColNo).Text
            .lblGoodsNrs.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.GOODS_CD_NRS.ColNo).Text
            .txtDelverGoodsNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.GOODS_NM.ColNo).Text
            .txtWorkSeiqCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo).Text
            .lblWorkSeiqNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_SEIQTO_NM.ColNo).Text
            .txtWork1Kb.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo).Text
            .lblWork1Nm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_NM_1.ColNo).Text
            .txtWork2Kb.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo).Text
            .lblWork2Nm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SAGYO_NM_2.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_ENT_TIME.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_UPD_USER_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM160G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 100, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.CUST_NM_L, "荷主名（大）", 150, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.CUST_NM_M, "荷主名（中）", 150, True)
        Public Shared CD As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.CD, "届先コード", 100, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.DEST_NM, "届先名", 80, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.GOODS_CD, "商品コード", 120, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.GOODS_NM, "納品書表示商品名", 150, True)
        Public Shared SAGYO_KB_1 As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_KB_1, "出荷時作業1", 100, True)
        Public Shared SAGYO_KB_2 As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_KB_2, "出荷時作業2", 100, True)
        Public Shared SAGYO_SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_SEIQTO_CD, "作業料請求先" & vbCrLf & "コード", 100, True)
        Public Shared SAGYO_SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_SEIQTO_NM, "作業料請求先名", 60, False)
        Public Shared SAGYO_NM_1 As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_NM_1, "作業名1", 60, False)
        Public Shared SAGYO_NM_2 As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SAGYO_NM_2, "作業名2", 60, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.GOODS_CD_NRS, "商品KEY", 60, False)
        Public Shared GOODS_NM_1 As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.GOODS_NM_1, "商品名", 60, False)
        Public Shared SYS_ENT_TIME As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_USER_TIME As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_UPD_USER_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM160C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


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
            .sprDetail.Sheets(0).ColumnCount = 26

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.CUST_NM_M.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 15, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 80, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_KB_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_KB_2.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_SEIQTO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))

            '隠し項目
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_SEIQTO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_NM_1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SAGYO_NM_2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_NM_1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_USER_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))



        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM160F)

        With frm.sprDetail
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.NRS_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.CUST_NM_M.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.GOODS_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.GOODS_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_SEIQTO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_NM_1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SAGYO_NM_2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.GOODS_CD_NRS.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.GOODS_NM_1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_ENT_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_UPD_USER_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM160G.sprDetailDef.SYS_DEL_FLG.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet

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


            Dim dr As DataRow

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                ''セルスタイル設定
                .SetCellStyle(i, LMM160G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.CD.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SAGYO_NM_2.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.GOODS_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_ENT_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_UPD_USER_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM160G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)


                ''セルに値を設定
                .SetCellValue(i, LMM160G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.CD.ColNo, dr.Item("CD").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.GOODS_CD.ColNo, dr.Item("GOODS_CD").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo, dr.Item("SAGYO_KB_1").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo, dr.Item("SAGYO_KB_2").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_SEIQTO_NM.ColNo, dr.Item("SAGYO_SEIQTO_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo, dr.Item("SAGYO_SEIQTO_CD").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_NM_1.ColNo, dr.Item("SAGYO_NM_1").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SAGYO_NM_2.ColNo, dr.Item("SAGYO_NM_2").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.GOODS_NM_1.ColNo, dr.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_ENT_TIME.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_UPD_USER_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM160G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

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

        Me.SetLockControl(Me._Frm.cmbNrsBrCd, lock)
        Me.SetLockControl(Me._Frm.txtCustCdL, lock)
        Me.SetLockControl(Me._Frm.txtCustCdM, lock)
        Me.SetLockControl(Me._Frm.txtDestCd, lock)
        Me.SetLockControl(Me._Frm.txtGoodsCd, lock)
        Me.SetLockControl(Me._Frm.txtDelverGoodsNm, lock)
        Me.SetLockControl(Me._Frm.txtWorkSeiqCd, lock)
        Me.SetLockControl(Me._Frm.txtWork1Kb, lock)
        Me.SetLockControl(Me._Frm.txtWork2Kb, lock)

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
