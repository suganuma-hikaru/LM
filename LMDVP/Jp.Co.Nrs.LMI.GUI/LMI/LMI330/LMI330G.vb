' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330G : 納品データ選択&編集
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
''' LMI330Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI330G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI330F

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
    Private _V As LMI330V

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI330F, ByVal g As LMIControlG, ByVal v As LMI330V)

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
            .F1ButtonName = "セット品"
            .F2ButtonName = "編　集"
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
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
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock

            '常に使用可能キー
            .F1ButtonEnabled = unLock
            .F9ButtonEnabled = unLock
            .F10ButtonEnabled = unLock
            .F12ButtonEnabled = unLock

            '画面入力モードによるロック制御
            .F2ButtonEnabled = view
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
            .cmbNrsBr.TabIndex = LMI330C.CtlTabIndex.CMB_NRS_BR
            .cmbSoko.TabIndex = LMI330C.CtlTabIndex.CMB_SOKO
            .txtCustCdL.TabIndex = LMI330C.CtlTabIndex.TXT_CUST_CD_L
            .lblCustNmL.TabIndex = LMI330C.CtlTabIndex.LBL_CUST_NM_L
            .imdOutkaPlanDate_From.TabIndex = LMI330C.CtlTabIndex.IMD_OUTKA_PLAN_DATE_FROM
            .imdOutkaPlanDate_To.TabIndex = LMI330C.CtlTabIndex.IMD_OUTKA_PLAN_DATE_TO
            .cmbPrintShubetu.TabIndex = LMI330C.CtlTabIndex.CMB_PRINT_SHUBETU
            .btnPrint.TabIndex = LMI330C.CtlTabIndex.BTN_PRINT

            '要望対応:1853 yamanaka 2013.02.14 Start
            .grpExcel.TabIndex = LMI330C.CtlTabIndex.GRP_EXCEL
            .txtGoodsCdPosition.TabIndex = LMI330C.CtlTabIndex.TXT_GOODS_CD_POSITION
            .cmbExcel.TabIndex = LMI330C.CtlTabIndex.CMB_EXCEL
            .btnExcel.TabIndex = LMI330C.CtlTabIndex.BTN_EXCEL
            '要望対応:1853 yamanaka 2013.02.14 End

            .sprSearch.TabIndex = LMI330C.CtlTabIndex.SPR_SEARCH
            '******************* 編集部 *****************************
            .txtDeliveryNo.TabIndex = LMI330C.CtlTabIndex.TXT_DELIVERY
            .imdArrPlanDate.TabIndex = LMI330C.CtlTabIndex.IMD_ARR_PLAN_DATE
            .txtDestCd.TabIndex = LMI330C.CtlTabIndex.TXT_DEST_CD
            .lblDestNm.TabIndex = LMI330C.CtlTabIndex.LBL_DEST_NM
            .btnDel.TabIndex = LMI330C.CtlTabIndex.BTN_DEL
            .sprDetail.TabIndex = LMI330C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '初期設定
        Call Me.SetDataControl(sysDate)

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.INIT, DispMode.VIEW
                    .txtCustCdL.Focus()

                Case DispMode.EDIT
                    .txtDeliveryNo.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.EDIT

                    '編集モード
                    .cmbNrsBr.ReadOnly = True
                    .cmbSoko.ReadOnly = False
                    .txtCustCdL.ReadOnly = False
                    .lblCustNmL.ReadOnly = False
                    .imdOutkaPlanDate_From.ReadOnly = False
                    .imdOutkaPlanDate_To.ReadOnly = False
                    .cmbPrintShubetu.ReadOnly = True
                    .btnPrint.Enabled = True
                    .lblEdiCtlNo.ReadOnly = True
                    .txtDeliveryNo.ReadOnly = False
                    .imdArrPlanDate.ReadOnly = False
                    .txtDestCd.ReadOnly = False
                    .lblDestNm.ReadOnly = True
                    .btnDel.Enabled = True
                    .lblCrtUser.ReadOnly = True
                    .lblCrtDate.ReadOnly = True
                    .lblUpdUser.ReadOnly = True
                    .lblUpdDate.ReadOnly = True
                    .lblUpdateTime.ReadOnly = True


                Case DispMode.INIT, DispMode.VIEW

                    '参照モード時
                    .sprDetail.CrearSpread()
                    .cmbNrsBr.ReadOnly = True
                    .cmbSoko.ReadOnly = False
                    .txtCustCdL.ReadOnly = False
                    .lblCustNmL.ReadOnly = True
                    .imdOutkaPlanDate_From.ReadOnly = False
                    .imdOutkaPlanDate_To.ReadOnly = False
                    .cmbPrintShubetu.ReadOnly = False
                    .btnPrint.Enabled = True
                    .lblEdiCtlNo.ReadOnly = True
                    .txtDeliveryNo.ReadOnly = True
                    .imdArrPlanDate.ReadOnly = True
                    .txtDestCd.ReadOnly = True
                    .lblDestNm.ReadOnly = True
                    .btnDel.Enabled = False
                    .lblCrtUser.ReadOnly = True
                    .lblCrtDate.ReadOnly = True
                    .lblUpdUser.ReadOnly = True
                    .lblUpdDate.ReadOnly = True
                    .lblUpdateTime.ReadOnly = True

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 画面項目クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .sprDetail.CrearSpread()
            .lblEdiCtlNo.TextValue = String.Empty
            .txtDeliveryNo.TextValue = String.Empty
            .imdArrPlanDate.TextValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty

        End With
    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataControl(ByVal sysDate As String)

        With Me._Frm

            .cmbNrsBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbSoko.SelectedValue = LMUserInfoManager.GetWhCd()
            .txtCustCdL.TextValue = "00182"
            .imdOutkaPlanDate_From.TextValue = sysDate
            .imdOutkaPlanDate_To.TextValue = sysDate
            .txtGoodsCdPosition.TextValue = "I"

            '名称を取得し、ラベルに表示を行う
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue _
                                                                                                        , "' AND CUST_CD_M = '00'"))
            If 0 < dr.Length Then
                .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
            End If
        End With

    End Sub

    ''' <summary>
    ''' 明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprDetail.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprSearchDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.DEF, " ", 20, True)
        Public Shared DELIVERY_NO As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.DELIVERY_NO, "デリバリー№", 200, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.DEST_CD, "届先コード", 150, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.DEST_NM, "届先名称", 300, True)
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.OUTKA_PLAN_DATE, "出荷日", 100, True)
        Public Shared ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.ARR_PLAN_DATE, "納入日", 100, True)
        Public Shared EDI_CTL_NO As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.EDI_CTL_NO, "EDI管理番号", 200, True)
        Public Shared FREE_C06 As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.FREE_C06, "フリー項目", 200, True)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.CREATE_USER, "作成者", 100, True)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.CREATE_DATE, "作成日", 100, True)
        Public Shared CREATE_TIME As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.CREATE_TIME, "作成時間", 100, True)

        '**** 隠し列 ****
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.NRS_BR_CD, "営業所コード", 50, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.CUST_CD_L, "荷主コード(大)", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.UPDATE_USER, "更新者", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.UPDATE_DATE, "更新日", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.UPDATE_TIME, "更新時間", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(明細)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI330C.SprSearchColumnIndex.DEF, " ", 20, True)
        Public Shared EDI_CTL_NO_CHU As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.EDI_CTL_NO_CHU, "EDI管理番号(中)", 150, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.GOODS_CD_CUST, "品番", 100, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.LOT_NO, "ロット№", 150, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.GOODS_NM, "品名", 300, True)
        Public Shared BUYER_ORD_NO As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.BUYER_ORD_NO, "オーダー番号", 150, True)
        Public Shared OUTKA_TTL_NB As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.OUTKA_TTL_NB, "個数", 100, True)

        '**** 隠し列 ****
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.NRS_BR_CD, "営業所コード", 50, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI330C.SprDetailColumnIndex.CUST_CD_L, "荷主コード(大)", 50, False)

    End Class

    ''' <summary>
    ''' SPREADの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitDetailSpread()

        '検索スプレッド初期化
        Call SetSprSearch()

        '明細スプレッド初期化
        Call SetSprDetail()

    End Sub

    ''' <summary>
    ''' SPREADのコントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch()

        'Spreadの初期値設定
        Dim sprSearch As LMSpread = Me._Frm.sprSearch

        With sprSearch

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI330C.SprSearchColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprSearchDef)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprSearchDef.DEST_NM.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprSearch)
            Dim delNo As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 30, False)
            Dim destCd As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_HANKAKU, 15, False)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 60, False)
            '**** 表示列 ****
            .SetCellStyle(0, sprSearchDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.DELIVERY_NO.ColNo, delNo)
            .SetCellStyle(0, sprSearchDef.DEST_CD.ColNo, destCd)
            .SetCellStyle(0, sprSearchDef.DEST_NM.ColNo, text)
            .SetCellStyle(0, sprSearchDef.OUTKA_PLAN_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.ARR_PLAN_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.EDI_CTL_NO.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.FREE_C06.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.CREATE_TIME.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprSearchDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.SYS_DEL_FLG.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' SPREADのコントロール設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSprDetail()

        'Spreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail

        With sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI330C.SprDetailColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDetailDef)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprSearch

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

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
                .SetCellStyle(i, sprSearchDef.DEF.ColNo, def)
                .SetCellStyle(i, sprSearchDef.DELIVERY_NO.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.DEST_CD.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.DEST_NM.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.OUTKA_PLAN_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.ARR_PLAN_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.EDI_CTL_NO.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.FREE_C06.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.CREATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.CREATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.CREATE_TIME.ColNo, lblL)

                ''**** 隠し列 ****
                .SetCellStyle(i, sprSearchDef.UPDATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UPDATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UPDATE_TIME.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.SYS_DEL_FLG.ColNo, lblL)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprSearchDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprSearchDef.DELIVERY_NO.ColNo, dr.Item("DELIVERY_NO").ToString())
                .SetCellValue(i, sprSearchDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, sprSearchDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprSearchDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.ARR_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.EDI_CTL_NO.ColNo, dr.Item("EDI_CTL_NO").ToString())
                .SetCellValue(i, sprSearchDef.FREE_C06.ColNo, dr.Item("FREE_C06").ToString())
                .SetCellValue(i, sprSearchDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, sprSearchDef.CREATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.CREATE_TIME.ColNo, dr.Item("SYS_ENT_TIME").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprSearchDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprSearchDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprSearchDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())
                .SetCellValue(i, sprSearchDef.UPDATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprSearchDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

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

            Dim spr As FarPoint.Win.Spread.SheetView = .sprSearch.ActiveSheet

            '明細部
            .lblEdiCtlNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.EDI_CTL_NO.ColNo))
            .txtDeliveryNo.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.DELIVERY_NO.ColNo))
            .imdArrPlanDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.ARR_PLAN_DATE.ColNo)).Replace("/", "")
            .txtDestCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.DEST_CD.ColNo))
            .lblDestNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.DEST_NM.ColNo))

            ''共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.CREATE_USER.ColNo))
            .lblCrtDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.CREATE_DATE.ColNo))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.UPDATE_USER.ColNo))
            .lblUpdDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.UPDATE_DATE.ColNo))
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI330G.sprSearchDef.UPDATE_TIME.ColNo))
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSprDetail(ByVal dt As DataTable)

        Dim sprDetail As LMSpread = Me._Frm.sprDetail

        With sprDetail

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            Dim edit As Boolean = False

            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
                edit = True
            End If


            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprDetail, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail)
            Dim numNb As StyleInfo = LMSpreadUtility.GetNumberCell(sprDetail, 0, 999999999, edit, 0, , ",")
            Dim goodsCd As StyleInfo = LMSpreadUtility.GetTextCell(sprDetail, InputControl.HAN_NUM_ALPHA, 20, edit)
            Dim lotNo As StyleInfo = LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 40, edit)
            Dim goodsNm As StyleInfo = LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 60, edit)
            Dim ordNo As StyleInfo = LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_MIX, 30, edit)
            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.EDI_CTL_NO_CHU.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_CUST.ColNo, goodsCd)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, lotNo)
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, goodsNm)
                .SetCellStyle(i, sprDetailDef.BUYER_ORD_NO.ColNo, ordNo)
                .SetCellStyle(i, sprDetailDef.OUTKA_TTL_NB.ColNo, numNb)

                '**** 隠し列 ****
                .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, lbl)
                .SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, lbl)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.EDI_CTL_NO_CHU.ColNo, dr.Item("EDI_CTL_NO_CHU").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.BUYER_ORD_NO.ColNo, dr.Item("BUYER_ORD_NO").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_TTL_NB.ColNo, dr.Item("OUTKA_TTL_NB").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
