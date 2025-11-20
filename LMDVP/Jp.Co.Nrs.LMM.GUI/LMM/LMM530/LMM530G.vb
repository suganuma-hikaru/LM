' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM530G : イエローカード管理マスタメンテ
'  作  成  者       :  hori
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
''' LMM530Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM530G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM530F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM530F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
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
            .F8ButtonName = LMMControlC.FUNCTION_F8_HIRAKU
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F8ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit
            '追加ボタン/クリアボタン
            Me._Frm.btnRowAdd_M.Enabled = edit
            Me._Frm.btnClear.Enabled = edit

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
            .sprDetail.TabIndex = LMM530C.CtlTabIndex.DETAIL
            .txtCustCdL.TabIndex = LMM530C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMM530C.CtlTabIndex.CUSTCDM
            .txtGoodsCd.TabIndex = LMM530C.CtlTabIndex.GOODSCD
            .txtLotNo.TabIndex = LMM530C.CtlTabIndex.LOTNO
            .txtShoboCd.TabIndex = LMM530C.CtlTabIndex.SHOBOCD
            .btnRowAdd_M.TabIndex = LMM530C.CtlTabIndex.BTNADD
            .btnClear.TabIndex = LMM530C.CtlTabIndex.BTNCLEAR
            .lblSituation.TabIndex = LMM530C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM530C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM530C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM530C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM530C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM530C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM530C.CtlTabIndex.SYSDELFLG

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
    Friend Sub UnLockedForm(ByVal eventType As LMM530C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
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
                    Call Me.ClearControl()
                    Call Me.LockControl(True)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me.LockControl(False)
                            Call Me.SetLockControl(.cmbNrsBrCd, True)
                            Call Me.SetLockControl(.txtCustCdL, True)
                            Call Me.SetLockControl(.txtCustCdM, True)
                            Call Me.SetLockControl(.txtGoodsCd, True)
                            Call Me.SetLockControl(.txtShoboCd, True)
                            Call Me.SetLockControl(.txtLotNo, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me.LockControl(False)
                            Call Me.SetLockControl(.cmbNrsBrCd, True)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me.LockControl(False)
                            Call Me.SetLockControl(.cmbNrsBrCd, True)
                            Call Me.SetLockControl(.txtCustCdL, True)
                            Call Me.SetLockControl(.txtCustCdM, True)
                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Call Me.ClearControl()
                    Call Me.LockControl(True)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM530C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM530C.EventShubetsu.MAIN, LMM530C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()

                Case LMM530C.EventShubetsu.SHINKI
                    .txtCustCdL.Focus()

                Case LMM530C.EventShubetsu.HUKUSHA
                    .txtGoodsCd.Focus()

                Case LMM530C.EventShubetsu.HENSHU
                    .lblYCardLink.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            'ファイルパス名のクリア
            .lblYCardLink.TextValue = String.Empty
            .lblYCardName.TextValue = String.Empty
            .lblSvPath.TextValue = String.Empty
            .lblSvFileNm.TextValue = String.Empty

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
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtGoodsCd.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .lblGoodsKey.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtShoboCd.TextValue = String.Empty
            .lblShoboNm.TextValue = String.Empty
            .lblYCardLink.TextValue = String.Empty
            .lblYCardName.TextValue = String.Empty
            .lblSvPath.TextValue = String.Empty
            .lblSvFileNm.TextValue = String.Empty
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

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.CUST_CD_L.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.CUST_NM_L.ColNo).Text
            .txtCustCdM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.CUST_CD_M.ColNo).Text
            .lblCustNmM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.CUST_NM_M.ColNo).Text
            .txtGoodsCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.GOODS_CD_CUST.ColNo).Text
            .lblGoodsNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.GOODS_NM_1.ColNo).Text
            .lblGoodsKey.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.GOODS_CD_NRS.ColNo).Text
            .txtLotNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.LOT_NO.ColNo).Text
            .txtShoboCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SHOBO_CD.ColNo).Text
            .lblShoboNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SHOBO_NM.ColNo).Text
            .lblYCardLink.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.YCARD_LINK.ColNo).Text
            .lblYCardName.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.YCARD_NAME.ColNo).Text
            .lblSvPath.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.YCARD_LINK.ColNo).Text
            .lblSvFileNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.YCARD_NAME.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM530G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

        End With

    End Sub

    ''' <summary>
    ''' 入荷データ編集画面からのデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInkaSData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables("LMM530IN").Rows(0)

        With Me._Frm

            .sprDetail.ActiveSheet.Cells(0, LMM530G.sprDetailDef.NRS_BR_NM.ColNo).Value = dr.Item("NRS_BR_CD").ToString()

            .cmbNrsBrCd.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
            .txtGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
            .lblGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
            .lblGoodsKey.TextValue = dr.Item("GOODS_CD_NRS").ToString()
            .txtLotNo.TextValue = dr.Item("LOT_NO").ToString()

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
        Call Me.SetSpread(ds.Tables(LMM530C.TABLE_NM_OUT))

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 80, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 150, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 80, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 140, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 100, True)
        Public Shared GOODS_NM_1 As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.GOODS_NM_1, "商品名", 150, True)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.GOODS_CD_NRS, "商品KEY", 50, False)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.LOT_NO, "ロット№", 120, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SHOBO_CD, "消防コード", 100, True)
        Public Shared SHOBO_NM As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SHOBO_NM, "消防名", 50, False)
        Public Shared ZAIKO As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.ZAIKO, "在庫数", 100, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_ENT_DATE, "作成日", 80, True)
        Public Shared YCARD_LINK As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.YCARD_LINK, "イエローカード" & vbCrLf & "ファイルパス", 300, True)
        Public Shared YCARD_NAME As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.YCARD_NAME, "イエローカード" & vbCrLf & "ファイル名", 300, True)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM530C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 23

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM530G.sprDetailDef())

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM530G.sprDetailDef.DEF.ColNo + 1

            '列設定用変数
            'Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.GOODS_NM_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SHOBO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SHOBO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.ZAIKO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.YCARD_LINK.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.YCARD_NAME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM530G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM530F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.NRS_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.CUST_NM_M.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.GOODS_CD_CUST.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.GOODS_NM_1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.GOODS_CD_NRS.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.LOT_NO.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SHOBO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SHOBO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.ZAIKO.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.YCARD_LINK.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.YCARD_NAME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM530G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty

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
            Dim rlabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim ediDate As String = String.Empty

            '残数セルのセルスタイル設定
            Dim zaikoLabel As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999999, True, 0, , ",")

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM530G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.GOODS_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SHOBO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.ZAIKO.ColNo, zaikoLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.YCARD_LINK.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.YCARD_NAME.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM530G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM530G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.GOODS_NM_1.ColNo, dr.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SHOBO_NM.ColNo, dr.Item("SHOBO_NM").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.ZAIKO.ColNo, dr.Item("ZAI_NB").ToString())

                ediDate = DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_ENT_DATE.ColNo, ediDate)

                .SetCellValue(i, LMM530G.sprDetailDef.YCARD_LINK.ColNo, dr.Item("YCARD_LINK").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.YCARD_NAME.ColNo, dr.Item("YCARD_NAME").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM530G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

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

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtCustCdL, lock)
            Me.SetLockControl(.txtCustCdM, lock)
            Me.SetLockControl(.txtGoodsCd, lock)
            Me.SetLockControl(.lblGoodsKey, lock)
            Me.SetLockControl(.txtLotNo, lock)
            Me.SetLockControl(.txtShoboCd, lock)

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
        Call Me._LMMConG.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
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
        Call Me._LMMConG.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me._LMMConG.GetTarget(Of Win.LMCheckBox)(arr, ctl)
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

        ctl.Enabled = Not lockFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        ctl.EnableStatus = Not lockFlg

    End Sub

#End Region

#End Region

End Class
