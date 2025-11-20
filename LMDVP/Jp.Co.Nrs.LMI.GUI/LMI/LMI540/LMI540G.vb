' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI540G : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH511Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI540G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI540F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI540H

    ''' <summary>
    ''' スプレッド列定義体を格納するフィールド(列移動許可のため)
    ''' </summary>
    ''' <remarks></remarks>
    Friend objSprDef As Object = Nothing
    Friend sprDetailDef As sprDetailDefault


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI540F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Form"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            ' ヘッダ部
            .cmbEigyo.TabIndex = LMI540C.CtlTabIndex_MAIN.CMBEIGYO
            .cmbWare.TabIndex = LMI540C.CtlTabIndex_MAIN.CMBWARE
            .txtCustCD_L.TabIndex = LMI540C.CtlTabIndex_MAIN.TXTCUSTCD_L
            .txtCustCD_M.TabIndex = LMI540C.CtlTabIndex_MAIN.TXTCUSTCD_M
            .imdOutkaDateFrom.TabIndex = LMI540C.CtlTabIndex_MAIN.IMEOUTKADATEFROM
            .imdOutkaDateTo.TabIndex = LMI540C.CtlTabIndex_MAIN.IMEOUTKADATETO
            .cmbPrint.TabIndex = LMI540C.CtlTabIndex_MAIN.CMBPRINT
            .btnPrint.TabIndex = LMI540C.CtlTabIndex_MAIN.BTNPRINT

            ' 明細部
            .sprDetail.TabIndex = LMI540C.CtlTabIndex_MAIN.SPRDETAIL

            '' フォーカスを得ない
            '.btnPrint.TabStop = False
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMI540F, ByVal sysdate As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm, sysdate)

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .txtCustCD_L.TextValue = String.Empty
            .txtCustCD_M.TextValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="frm"></param>
    ''' <param name="sysdate"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMI540F, ByVal sysdate As String)

        'コントロールに初期値を設定
        With Me._Frm
            '　営業所
            .cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()
            frm.cmbEigyo.ReadOnly = True

            ' 倉庫
            .cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()

            ' 出荷日FROM
            .imdOutkaDateFrom.TextValue = sysdate

            ' 出荷日TO
            .imdOutkaDateTo.TextValue = sysdate

            ' 荷主コード(大)
            .txtCustCD_L.TextValue = String.Empty
            ' 荷主コード(中)
            .txtCustCD_M.TextValue = String.Empty

            ' 印刷コンボボックス
            .cmbPrint.SelectedValue = String.Empty

        End With

        SetInitControlCust(frm)

    End Sub

    ''' <summary>
    ''' 荷主コード/荷主名 (大/中) 初期値設定
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub SetInitControlCust(ByRef frm As LMI540F)

        SetInitControlCust(frm, "", "")

    End Sub

    ''' <summary>
    ''' 荷主コード/荷主名 (大/中) 初期値設定
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Function SetInitControlCust(ByRef frm As LMI540F, ByVal custCdL As String, ByVal custCdM As String) As Boolean

        Dim ret As Boolean = False

        Dim drCust As DataRow()
        Dim where As New Text.StringBuilder()
        If True Then
            where.Append(String.Concat("    NRS_BR_CD = '" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() & "' "))
        End If
        If custCdL.Trim().Length > 0 Then
            where.Append(String.Concat("AND CUST_CD_L = '" & custCdL & "' "))
        End If
        If custCdM.Trim().Length > 0 Then
            where.Append(String.Concat("AND CUST_CD_M = '" & custCdM & "' "))
        End If

        drCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(where.ToString())
        With Me._Frm
            If drCust.Count > 0 Then
                .txtCustCD_L.TextValue = drCust(0).Item("CUST_CD_L").ToString()
                .lblCustNM_L.TextValue = drCust(0).Item("CUST_NM_L").ToString()
                .txtCustCD_L.BackColorDef() = LMGUIUtility.GetSystemInputBackColor()
                .txtCustCD_M.TextValue = drCust(0).Item("CUST_CD_M").ToString()
                .lblCustNM_M.TextValue = drCust(0).Item("CUST_NM_M").ToString()
                .txtCustCD_M.BackColorDef() = LMGUIUtility.GetSystemInputBackColor()
                ret = True
            Else
                .lblCustNM_L.TextValue = ""
                .lblCustNM_M.TextValue = ""
            End If
        End With

        Return ret

    End Function

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(mode As Integer)

        Call Me.SetFunctionKey(mode)
        Call Me.SetControlsStatus(mode)

    End Sub

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal mode As Integer)

        With Me._Frm.FunctionKey
            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            '表示名を設定
            .F1ButtonName = ""
            .F2ButtonName = ""
            .F3ButtonName = ""
            .F4ButtonName = ""
            .F5ButtonName = "取　込"
            .F6ButtonName = ""
            .F7ButtonName = ""
            .F8ButtonName = ""
            .F9ButtonName = "検索"
            .F10ButtonName = ""
            .F11ButtonName = ""
            .F12ButtonName = "閉じる"

            ' 使用状態の設定
            .Enabled = True
            Select Case mode
                Case LMI540C.Mode.INT
                    ' 初期モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = True
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True

                Case LMI540C.Mode.REF
                    ' 参照モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = True
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True
            End Select
        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal mode As Integer)

        With Me._Frm
            Select Case mode
                Case LMI540C.Mode.INT
                    ' 初期モード
                    '.cmbPrint.Enabled = True
                    .cmbPrint.Visible = False
                    .btnPrint.Enabled = True

                Case LMI540C.Mode.REF
                    ' 参照モード
                    '.cmbPrint.Enabled = True
                    .cmbPrint.Visible = False
                    .btnPrint.Enabled = True

            End Select
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(ヘッダー部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusHeader()

        With Me._Frm
            .cmbEigyo.Focus()
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(明細部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusDetail()

        With Me._Frm
            .sprDetail.Focus()
        End With

    End Sub

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefault

        ' 見出し/幅/表示状態
        Public DEF As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.DEF, " ", 20, True)
        Public KEY_NO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.KEY_NO, "KEY NO.", 60, True)
        Public OFFLINE_NO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.OFFLINE_NO, "オフラインNo.", 100, True)
        Public SHIZI_KB_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SHIZI_KB_NM, "依頼書", 80, True)
        Public NOHIN_KB_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.NOHIN_KB_NM, "納品書", 60, False)
        Public IRAI_DATE As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.IRAI_DATE, "依頼日", 80, True)
        Public IRAI_SYA As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.IRAI_SYA, "依頼者", 70, True)
        Public MOTO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.MOTO, "出荷/回収元", 120, True)
        Public SHUBETSU As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SHUBETSU, "種別", 70, True)
        Public OUTKA_DATE As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.OUTKA_DATE, "出荷日", 80, True)
        Public ARR_DATE As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.ARR_DATE, "納品日", 80, True)
        Public ZIP As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.ZIP, "郵便番号", 70, True)
        Public DEST_AD As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.DEST_AD, "住所", 200, True)
        Public COMP_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.COMP_NM, "会社名", 200, True)
        Public BUSYO_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.BUSYO_NM, "部署名", 120, True)
        Public TANTO_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.TANTO_NM, "担当者名", 120, True)
        Public TEL As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.TEL, "電話番号", 100, True)
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.GOODS_NM, "品名", 160, True)
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.LOT_NO, "製造ロット", 100, True)
        Public INOUTKA_NB As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.INOUTKA_NB, "本数", 80, True)
        Public ONDO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.ONDO, "温度条件", 70, True)
        Public DOKUGEKI As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.DOKUGEKI, "毒劇物", 70, True)
        Public HAISO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.HAISO, "配送便", 120, True)
        Public SAP_NO As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SAP_NO, "SAP受注登録番号", 100, False)
        Public REMARK As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.REMARK, "備考欄", 200, True)

        Public SHIZI_KB As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SHIZI_KB, "(出荷指示書→依頼書)", 20, False)
        Public NOHIN_KB As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.NOHIN_KB, "(納品書→未使用)", 20, False)
        Public PLANT_CD As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.PLANT_CD, "(出荷/回収元  取込元の値中の プラントコード)", 20, False)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.NRS_BR_CD, "(出荷/回収元 コード の営業所コード)", 20, False)
        Public WH_CD As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.WH_CD, "(倉庫コード)", 20, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.CUST_CD_L, "(荷主CD(大))", 20, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.CUST_CD_M, "(荷主CD(小))", 20, False)
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SYS_UPD_DATE, "(更新日)", 40, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI540C.SprColumnIndex.SYS_UPD_TIME, "(更新時刻)", 40, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            ' スプレッドをクリア
            .CrearSpread()

            ' 列数設定
            .ActiveSheet.ColumnCount = LMI540C.SprColumnIndex.LAST

            ' スプレッドの列設定（見出し／幅／表示状態／列移動許可）
            objSprDef = New sprDetailDefault
            .SetColProperty(objSprDef, True)
            sprDetailDef = DirectCast(objSprDef, sprDetailDefault)

            ' 列固定位置を設定(ex.荷主名で固定)
            .ActiveSheet.FrozenColumnCount = 1

            ' 先頭検索行のセルスタイル設定
            .SetCellStyle(0, sprDetailDef.KEY_NO.ColNo, LMSpreadUtility.GetNumberCell(spr, 1, 999999, False, 0, False))
            .SetCellStyle(0, sprDetailDef.OFFLINE_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprDetailDef.SHIZI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H010", False))
            .SetCellStyle(0, sprDetailDef.NOHIN_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H010", False))
            .SetCellStyle(0, sprDetailDef.IRAI_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.IRAI_SYA.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 20, False))
            .SetCellStyle(0, sprDetailDef.MOTO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.SHUBETSU.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 20, False))
            .SetCellStyle(0, sprDetailDef.OUTKA_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.ARR_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.ZIP.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 9, False))
            .SetCellStyle(0, sprDetailDef.DEST_AD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.COMP_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.BUSYO_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.TANTO_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.TEL.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 200, False))
            .SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 200, False))
            .SetCellStyle(0, sprDetailDef.INOUTKA_NB.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.ONDO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 10, False))
            .SetCellStyle(0, sprDetailDef.DOKUGEKI.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 10, False))
            .SetCellStyle(0, sprDetailDef.HAISO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.SAP_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False))
            .SetCellStyle(0, sprDetailDef.SHIZI_KB.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.NOHIN_KB.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.PLANT_CD.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.WH_CD.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
        End With

    End Sub

    ''' <summary>
    ''' スプレッドに取得データをセット
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        '取得データ件数
        Dim dataCnt As Integer = dt.Rows.Count()

        '取得データが0件なら抜ける
        If dataCnt = 0 Then
            Exit Sub
        End If

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim sCheck As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sLabelR As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            '描画中断
            .SuspendLayout()

            'スプレッドのカレント行
            Dim spIdx As Integer = 0

            '集計用変数
            Dim tumiSu As Decimal = 0

            ' 取得データのループ
            For dtIdx As Integer = 1 To dataCnt
                Dim dr As DataRow = dt.Rows(dtIdx - 1)

                .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)
                spIdx += 1

                ' 追加行のセルスタイル設定
                .SetCellStyle(spIdx, sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(spIdx, sprDetailDef.KEY_NO.ColNo, sNum)
                .SetCellStyle(spIdx, sprDetailDef.OFFLINE_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SHIZI_KB_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.NOHIN_KB_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.IRAI_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.IRAI_SYA.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.MOTO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SHUBETSU.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.OUTKA_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.ARR_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.ZIP.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.DEST_AD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.COMP_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.BUSYO_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.TANTO_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.TEL.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.INOUTKA_NB.ColNo, sNum)
                .SetCellStyle(spIdx, sprDetailDef.ONDO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.DOKUGEKI.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.HAISO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SAP_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.REMARK.ColNo, sLabel)

                .SetCellStyle(spIdx, sprDetailDef.SHIZI_KB.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.NOHIN_KB.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.PLANT_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)

                ' セル値の設定
                .SetCellValue(spIdx, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(spIdx, sprDetailDef.KEY_NO.ColNo, dr.Item("KEY_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.OFFLINE_NO.ColNo, dr.Item("OFFLINE_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.SHIZI_KB_NM.ColNo, dr.Item("SHIZI_KB_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.NOHIN_KB_NM.ColNo, dr.Item("NOHIN_KB_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.IRAI_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("IRAI_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.IRAI_SYA.ColNo, dr.Item("IRAI_SYA").ToString())
                .SetCellValue(spIdx, sprDetailDef.MOTO.ColNo, dr.Item("MOTO").ToString())
                .SetCellValue(spIdx, sprDetailDef.SHUBETSU.ColNo, dr.Item("SHUBETSU").ToString())
                .SetCellValue(spIdx, sprDetailDef.OUTKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.ARR_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(spIdx, sprDetailDef.DEST_AD.ColNo, dr.Item("DEST_AD").ToString())
                .SetCellValue(spIdx, sprDetailDef.COMP_NM.ColNo, dr.Item("COMP_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.BUSYO_NM.ColNo, dr.Item("BUSYO_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.TANTO_NM.ColNo, dr.Item("TANTO_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(spIdx, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.INOUTKA_NB.ColNo, dr.Item("INOUTKA_NB").ToString())
                .SetCellValue(spIdx, sprDetailDef.ONDO.ColNo, dr.Item("ONDO").ToString())
                .SetCellValue(spIdx, sprDetailDef.DOKUGEKI.ColNo, dr.Item("DOKUGEKI").ToString())
                .SetCellValue(spIdx, sprDetailDef.HAISO.ColNo, dr.Item("HAISO").ToString())
                .SetCellValue(spIdx, sprDetailDef.SAP_NO.ColNo, dr.Item("SAP_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())

                .SetCellValue(spIdx, sprDetailDef.SHIZI_KB.ColNo, dr.Item("SHIZI_KB").ToString())
                .SetCellValue(spIdx, sprDetailDef.NOHIN_KB.ColNo, dr.Item("NOHIN_KB").ToString())
                .SetCellValue(spIdx, sprDetailDef.PLANT_CD.ColNo, dr.Item("PLANT_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(spIdx, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            Next

            '描画再開
            .ResumeLayout(True)
        End With

    End Sub

    ''' <summary>
    ''' スプレッドの編集状態を制御
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <param name="spIdx">対象行番号</param>
    ''' <param name="endAfter">True:終了アクション後,False:通常時</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadEdit(ByVal mode As Integer, ByVal spIdx As Integer, Optional ByVal endAfter As Boolean = False)

        With Me._Frm.sprDetail
            ' 描画中断
            .SuspendLayout()

            Dim readOnlyBackColor As System.Drawing.Color = Utility.LMGUIUtility.GetReadOnlyBackColor
            Dim inputBackColor As System.Drawing.Color = Color.White

            Select Case mode
                Case LMI540C.Mode.REF
                    ' 参照モード
                    .ActiveSheet.Cells(spIdx, sprDetailDef.KEY_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.KEY_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OFFLINE_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OFFLINE_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SHIZI_KB_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SHIZI_KB_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.NOHIN_KB_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.NOHIN_KB_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.IRAI_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.IRAI_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.IRAI_SYA.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.IRAI_SYA.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.MOTO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.MOTO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SHUBETSU.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SHUBETSU.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OUTKA_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OUTKA_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ARR_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ARR_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZIP.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZIP.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DEST_AD.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DEST_AD.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.COMP_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.COMP_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.BUSYO_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.BUSYO_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TANTO_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TANTO_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TEL.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TEL.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GOODS_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GOODS_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOT_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOT_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.INOUTKA_NB.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.INOUTKA_NB.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ONDO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ONDO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DOKUGEKI.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DOKUGEKI.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.HAISO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.HAISO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SAP_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SAP_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.REMARK.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.REMARK.ColNo).BackColor = readOnlyBackColor

            End Select

            '描画再開
            .ResumeLayout(True)
        End With

    End Sub

#End Region 'Form

End Class
