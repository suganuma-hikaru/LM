' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI511G : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL
Imports GrapeCity.Win.Editors

''' <summary>
''' LMH511Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI511G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI511F

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
    Private _H As LMI511H

    ''' <summary>
    ''' スプレッド列定義体を格納するフィールド(列移動許可のため)
    ''' </summary>
    ''' <remarks></remarks>
    Friend objSprDef As Object = Nothing
    Friend sprEdiListDef As sprEdiListDefault
    Friend objSprExcel As Object = Nothing
    Friend sprEdiListExcel As sprEdiListDefault


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI511F)

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
            'ヘッダ部
            .cmbEigyo.TabIndex = LMI511C.CtlTabIndex_MAIN.CMBEIGYO
            '.txtCustCD_L.TabIndex = LMI511C.CtlTabIndex_MAIN.TXTCUSTCD_L
            '.txtCustCD_M.TabIndex = LMI511C.CtlTabIndex_MAIN.TXTCUSTCD_M
            .grpSearchKbn.TabIndex = LMI511C.CtlTabIndex_MAIN.GRPSEARCHKBN
            .cmbOutkaPosi.TabIndex = LMI511C.CtlTabIndex_MAIN.CMBOUTKAPOSI
            .imdEdiDateFrom.TabIndex = LMI511C.CtlTabIndex_MAIN.IMDEDIDATEFROM
            .imdEdiDateTo.TabIndex = LMI511C.CtlTabIndex_MAIN.IMDEDIDATETO
            .cmbSelectDate.TabIndex = LMI511C.CtlTabIndex_MAIN.CMBSELECTDATE
            .imdSearchDateFrom.TabIndex = LMI511C.CtlTabIndex_MAIN.IMDSEARCHDATEFROM
            .imdSearchDateTo.TabIndex = LMI511C.CtlTabIndex_MAIN.IMDSEARCHDATETO
            .grpHide.TabIndex = LMI511C.CtlTabIndex_MAIN.GRPHIDE
            .chkHide1.TabIndex = LMI511C.CtlTabIndex_MAIN.CHKHIDE1
            .chkHide2.TabIndex = LMI511C.CtlTabIndex_MAIN.CHKHIDE2
            .grpExclusion.TabIndex = LMI511C.CtlTabIndex_MAIN.GRPEXCLUSION
            .txtExclusionA.TabIndex = LMI511C.CtlTabIndex_MAIN.TXTEXCLUSIONA
            .txtExclusionB.TabIndex = LMI511C.CtlTabIndex_MAIN.TXTEXCLUSIONB
            .txtExclusionC.TabIndex = LMI511C.CtlTabIndex_MAIN.TXTEXCLUSIONC
            .cmbPrint.TabIndex = LMI511C.CtlTabIndex_MAIN.CMBPRINT
            .btnPrint.TabIndex = LMI511C.CtlTabIndex_MAIN.BTNPRINT
            .btnAllCheck.TabIndex = LMI511C.CtlTabIndex_MAIN.BTNALLCHECK
            .btnAllClear.TabIndex = LMI511C.CtlTabIndex_MAIN.BTNALLCLEAR
            .btnExcel.TabIndex = LMI511C.CtlTabIndex_MAIN.BTNEXCEL


            '明細部
            .sprEdiList.TabIndex = LMI511C.CtlTabIndex_MAIN.SPREDILIST

            'フォーカスを得ない
            .btnPrint.TabStop = False
            .btnAllCheck.TabStop = False
            .btnAllClear.TabStop = False
            .btnExcel.TabStop = False
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMI511F, ByVal sysdate As String)

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
            '.txtCustCD_M.TextValue = String.Empty
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
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMI511F, ByVal sysdate As String)

        Dim sql As String = String.Empty

        '営業マスタ情報取得
        sql = "NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() & "'"
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(sql)(0)

        ''初期荷主情報取得
        'sql = "SYS_DEL_FLG = '0' AND USER_CD = '" & LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'"
        'Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST).Select(sql)

        'ＪＮＣ営業所マスタ情報取得
        Dim rtDs As DataSet = DirectCast(Me.MyHandler, LMI511H).BoMst(frm, LM.Base.LMUserInfoManager.GetNrsBrCd().ToString())
        Dim dtBoMst As DataTable = rtDs.Tables(LMI511C.TABLE_NM.OUT_BO_MST)

        'コントロールに初期値を設定
        With Me._Frm
            '営業所
            .cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()
            'If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            '    frm.cmbEigyo.ReadOnly = True
            'Else
            '    frm.cmbEigyo.ReadOnly = False
            'End If
            frm.cmbEigyo.ReadOnly = False

            ''荷主(大)／荷主(中)
            'If getDr.Length() > 0 Then
            '    .txtCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()
            '    .lblCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()
            '    .txtCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()
            '    .lblCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()
            'End If

            '出荷先
            .cmbOutkaPosi.Items.Clear()
            Dim subItems As List(Of ListItem) = New List(Of ListItem)
            subItems.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))
            For Each r As DataRow In dtBoMst.Rows
                subItems.Add(New ListItem(New SubItem() {New SubItem(r.Item("OUTKA_POSI_BU_NM_RYAK")), New SubItem(r.Item("OUTKA_POSI_BU_CD"))}))
            Next
            .cmbOutkaPosi.Items.AddRange(subItems.ToArray())
            .cmbOutkaPosi.Refresh()

            'EDI取込日FROM
            .imdEdiDateFrom.TextValue = sysdate

            'EDI取込日TO
            .imdEdiDateTo.TextValue = sysdate

            '検索日区分
            .cmbSelectDate.SelectedValue = "01"

            '検索日FROM
            .imdSearchDateFrom.TextValue = sysdate

            '検索日TO
            .imdSearchDateTo.TextValue = sysdate

            '印刷
            .cmbPrint.SelectedValue = String.Empty

            '表示区分
            .chkHide1.Checked() = True
            .chkHide2.Checked() = True

            '除外商品名ABC
            .txtExclusionA.TextValue = String.Empty
            .txtExclusionB.TextValue = String.Empty
            .txtExclusionC.TextValue = String.Empty

        End With

    End Sub

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
            .F1ButtonName = "出荷登録"
            .F2ButtonName = "編集"
            .F3ButtonName = "まとめ指示"
            .F4ButtonName = "まとめ解除"
            .F5ButtonName = "送信要求"
            .F6ButtonName = "報告訂正"
            .F7ButtonName = "送信取消"
            .F8ButtonName = "まとめ候補検索"
            .F9ButtonName = "検索"
            '.F10ButtonName = "マスタ参照"
            .F10ButtonName = String.Empty
            .F11ButtonName = "保存"
            .F12ButtonName = "閉じる"

            '使用状態の設定
            .Enabled = True
            Select Case mode
                Case LMI511C.Mode.INT
                    '初期モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = True
                    .F9ButtonEnabled = True
                    '.F10ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True

                Case LMI511C.Mode.REF
                    '参照モード
                    If Me._Frm.optSyuko.Checked Then
                        .F1ButtonEnabled = True
                    Else
                        .F1ButtonEnabled = False
                    End If
                    .F2ButtonEnabled = True
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = True
                    .F5ButtonEnabled = True
                    .F6ButtonEnabled = True
                    .F7ButtonEnabled = True
                    .F8ButtonEnabled = True
                    .F9ButtonEnabled = True
                    '.F10ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True

                Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                    '編集モード／訂正モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = True
                    .F9ButtonEnabled = True
                    '.F10ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = True
                    .F12ButtonEnabled = True

                Case LMI511C.Mode.MTM
                    'まとめモード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = True
                    .F4ButtonEnabled = True
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = True
                    .F9ButtonEnabled = True
                    '.F10ButtonEnabled = True
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
                Case LMI511C.Mode.INT
                    '初期モード
                    .cmbPrint.Enabled = False
                    .btnPrint.Enabled = False
                    .btnAllCheck.Enabled = False
                    .btnAllClear.Enabled = False
                    .btnExcel.Enabled = False

                Case LMI511C.Mode.REF
                    '参照モード
                    If Me._Frm.optSyuko.Checked Then
                        .cmbPrint.Enabled = True
                        .btnPrint.Enabled = True
                        .btnExcel.Enabled = True
                    Else
                        .cmbPrint.Enabled = False
                        .btnPrint.Enabled = False
                        .btnExcel.Enabled = True
                    End If
                    .btnAllCheck.Enabled = True
                    .btnAllClear.Enabled = True

                Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                    '編集モード／訂正モード
                    .cmbPrint.Enabled = False
                    .btnPrint.Enabled = False
                    .btnAllCheck.Enabled = False
                    .btnAllClear.Enabled = False
                    .btnExcel.Enabled = False

                Case LMI511C.Mode.MTM
                    'まとめモード
                    .cmbPrint.Enabled = False
                    .btnPrint.Enabled = False
                    .btnAllCheck.Enabled = True
                    .btnAllClear.Enabled = True
                    .btnExcel.Enabled = False
            End Select
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(ヘッダー部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusHeader()

        With Me._Frm
            '.txtCustCD_L.Focus()
            .cmbEigyo.Focus()
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(明細部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusDetail()

        With Me._Frm
            .sprEdiList.Focus()
        End With

    End Sub

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprEdiListDefault
        '見出し／幅／表示状態
        Public DEF As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DEF, " ", 20, True)
        Public RB_KBN As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.RB_KBN, "(赤黒区分)", 40, False)
        Public RB_KBN_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.RB_KBN_NM, "赤黒", 40, True)
        Public MOD_KBN As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.MOD_KBN, "(変更区分)", 40, False)
        Public MOD_KBN_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.MOD_KBN_NM, "変更", 40, True)
        Public RTN_FLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.RTN_FLG, "(報告フラグ)", 40, False)
        Public RTN_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.RTN_FLG_NM, "報告", 50, True)
        Public SND_CANCEL_FLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SND_CANCEL_FLG, "(送信訂正区分)", 40, False)
        Public SND_CANCEL_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SND_CANCEL_FLG_NM, "送信訂正", 60, True)
        Public PRTFLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.PRTFLG, "(プリントフラグ)", 40, False)
        Public PRTFLG_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.PRTFLG_NM, "印刷", 40, True)
        Public PRTFLG_SUB As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.PRTFLG_SUB, "(プリントフラグサブ)", 40, False)
        Public PRTFLG_SUB_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.PRTFLG_SUB_NM, "専門印刷", 60, True)
        Public NRS_SYS_FLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.NRS_SYS_FLG, "(取り込みフラグ)", 40, False)
        Public NRS_SYS_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.NRS_SYS_FLG_NM, "取込", 40, True)
        Public COMBI As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.COMBI, "積合", 40, True)
        Public UNSO_REQ_YN As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_REQ_YN, "(運送依頼有無区分)", 40, False)
        Public UNSO_REQ_YN_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_REQ_YN_NM, "運送", 40, True)
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
        Public UNSO_EDI_CTL_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_EDI_CTL_NO, "(運送EDI_CTL_NO)", 40, False)
        Public UNSO_RTN_FLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_RTN_FLG, "(運送報告区分)", 40, False)
        Public UNSO_RTN_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_RTN_FLG_NM, "運送報告", 60, True)
#End If
        Public COMBI_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.COMBI_NO, "まとめ番号", 80, True)
        Public SR_DEN_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SR_DEN_NO, "伝票番号", 80, True)
        Public OUTKA_DATE As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.OUTKA_DATE, "出荷予定日", 80, True)
        Public ARRIVAL_DATE As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.ARRIVAL_DATE, "納入予定日", 80, True)
        Public INKO_DATE As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.INKO_DATE, "入荷予定日", 80, True)
        Public DEST_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DEST_CD, "(届先コード)", 40, False)
        Public DEST_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DEST_NM, "届先名", 140, True)
        Public DEST_AD_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DEST_AD_NM, "届先住所", 140, True)
        Public OUTKA_POSI_BU_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.OUTKA_POSI_BU_CD, "(出荷場所部門コード)", 40, False)
        Public OUTKA_POSI_BU_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.OUTKA_POSI_BU_NM, "出荷先", 80, True)
        Public ACT_UNSO_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.ACT_UNSO_CD, "(運送会社コード)", 40, False)
        Public ACT_UNSO_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.ACT_UNSO_NM, "運送会社名", 100, True)
        Public MEMO_UNSO_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.MEMO_UNSO_CD, "(運送会社コード(控))", 40, False)
        Public MEMO_UNSO_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.MEMO_UNSO_NM, "運送会社名(控)", 100, True)
        Public UNSO_ROUTE_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_ROUTE_CD, "(運送手段コード)", 40, False)
        Public UNSO_ROUTE_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.UNSO_ROUTE_NM, "運送手段名", 160, True)
        Public GOODS_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.GOODS_CD, "(商品コード)", 40, False)
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.GOODS_NM, "商品名", 100, True)
        Public SURY_REQ As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_REQ, "出荷数量", 60, True)
        Public SURY_TANI_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_TANI_CD, "単位", 40, True)
        Public SURY_RPT As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_RPT, "実出荷数量(積み)", 90, True)
        Public SURY_RPT_UNSO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_RPT_UNSO, "実出荷数量(卸し)", 90, True)
        Public CAR_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.CAR_NO, "車両番号", 60, True)
        Public TUMI_SU As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.TUMI_SU, "出荷個数", 60, True)
        Public PRINT_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.PRINT_NO, "印刷番号", 80, True)
        Public JYUCHU_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.JYUCHU_NO, "受注番号", 80, True)
        Public ORDER_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.ORDER_NO, "注文番号", 80, True)
        Public DELIVERY_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DELIVERY_NM, "外部記事1", 160, True)
        Public INV_REM_NM As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.INV_REM_NM, "外部記事2", 160, True)
        Public GOODS_CD2 As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.GOODS_CD2, "(商品コード2)", 40, False)
        Public GOODS_NM2 As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.GOODS_NM2, "商品名2", 100, False)
        Public SURY_REQ2 As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_REQ2, "出荷数量2", 70, False)
        Public SURY_TANI_CD2 As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_TANI_CD2, "単位2", 40, False)
        Public SURY_RPT2 As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SURY_RPT2, "実出荷数量2", 90, False)
        Public HIS_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.HIS_NO, "(履歴番号)", 60, False)
        Public OLD_DATA_FLG As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.OLD_DATA_FLG, "旧データ", 60, False)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.NRS_BR_CD, "(営業所コード)", 40, False)
        Public INOUT_KB As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.INOUT_KB, "(入出荷区分)", 40, False)
        Public DATA_KIND As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.DATA_KIND, "(データ種別)", 40, False)
        Public EDI_CTL_NO As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.EDI_CTL_NO, "(ＥＤＩ管理番号)", 40, False)
        Public EDI_CTL_NO_CHU As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.EDI_CTL_NO_CHU, "(ＥＤＩ管理番号中番)", 40, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.CUST_CD_L, "(荷主コード（大）)", 40, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.CUST_CD_M, "(荷主コード（中）)", 40, False)
        Public SYS_UPD_DATE_HED As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_DATE_HED, "(更新日(HED))", 40, False)
        Public SYS_UPD_TIME_HED As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_TIME_HED, "(更新時刻(HED))", 40, False)
        Public SYS_UPD_DATE_DTL As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_DATE_DTL, "(更新日(DTL))", 40, False)
        Public SYS_UPD_TIME_DTL As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_TIME_DTL, "(更新時刻(DTL))", 40, False)
        Public EDI_CTL_NO_UHD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.EDI_CTL_NO_UHD, "(ＥＤＩ管理番号(UHD))", 40, False)
        Public SYS_UPD_DATE_UHD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_DATE_UHD, "(更新日(UHD))", 40, False)
        Public SYS_UPD_TIME_UHD As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_TIME_UHD, "(更新時刻(UHD))", 40, False)
        Public EDI_CTL_NO_CHU_UDL As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.EDI_CTL_NO_CHU_UDL, "(ＥＤＩ管理番号中番(UDL))", 40, False)
        Public SYS_UPD_DATE_UDL As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_DATE_UDL, "(更新日(UDL))", 40, False)
        Public SYS_UPD_TIME_UDL As SpreadColProperty = New SpreadColProperty(LMI511C.SprColumnIndex.SYS_UPD_TIME_UDL, "(更新時刻(UDL))", 40, False)
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            'スプレッドをクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI511C.SprColumnIndex.LAST

            'スプレッドの列設定（見出し／幅／表示状態／列移動許可）
            objSprDef = New sprEdiListDefault
            .SetColProperty(objSprDef, True)
            sprEdiListDef = DirectCast(objSprDef, sprEdiListDefault)

            '列固定位置を設定(ex.荷主名で固定)
            .ActiveSheet.FrozenColumnCount = 1

            '先頭検索行のセルスタイル設定
            .SetCellStyle(0, sprEdiListDef.RB_KBN.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.RB_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J022", False))
            .SetCellStyle(0, sprEdiListDef.MOD_KBN.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.MOD_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J018", False))
            .SetCellStyle(0, sprEdiListDef.RTN_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.RTN_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J017", False))
            .SetCellStyle(0, sprEdiListDef.SND_CANCEL_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SND_CANCEL_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J021", False))
            .SetCellStyle(0, sprEdiListDef.PRTFLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.PRTFLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
            .SetCellStyle(0, sprEdiListDef.PRTFLG_SUB.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.PRTFLG_SUB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
            .SetCellStyle(0, sprEdiListDef.NRS_SYS_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.NRS_SYS_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
            .SetCellStyle(0, sprEdiListDef.UNSO_RTN_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J017", False))
#End If
            .SetCellStyle(0, sprEdiListDef.COMBI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J020", False))
            .SetCellStyle(0, sprEdiListDef.UNSO_REQ_YN.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.UNSO_REQ_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J020", False))
            .SetCellStyle(0, sprEdiListDef.COMBI_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SR_DEN_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprEdiListDef.OUTKA_DATE.ColNo, sDate)
            .SetCellStyle(0, sprEdiListDef.ARRIVAL_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.INKO_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprEdiListDef.DEST_AD_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 180, False))
            .SetCellStyle(0, sprEdiListDef.OUTKA_POSI_BU_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.OUTKA_POSI_BU_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.ACT_UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.ACT_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", False))
            .SetCellStyle(0, sprEdiListDef.MEMO_UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.MEMO_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", False))
            .SetCellStyle(0, sprEdiListDef.UNSO_ROUTE_CD.ColNo, sLabel)
#If False Then  'UPD 2020/09/10
            .SetCellStyle(0, sprEdiListDef.UNSO_ROUTE_NM.ColNo, sLabel)
#Else
            .SetCellStyle(0, sprEdiListDef.UNSO_ROUTE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J015", False))
#End If
            .SetCellStyle(0, sprEdiListDef.GOODS_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprEdiListDef.SURY_REQ.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.SURY_TANI_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SURY_RPT.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.SURY_RPT_UNSO.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.CAR_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprEdiListDef.TUMI_SU.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.PRINT_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.JYUCHU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 23, False))
            .SetCellStyle(0, sprEdiListDef.ORDER_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.DELIVERY_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.INV_REM_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.GOODS_CD2.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.GOODS_NM2.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SURY_REQ2.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.SURY_TANI_CD2.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SURY_RPT2.ColNo, sNum)
            .SetCellStyle(0, sprEdiListDef.HIS_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.OLD_DATA_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.NRS_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.INOUT_KB.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.DATA_KIND.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.EDI_CTL_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.EDI_CTL_NO_CHU.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SYS_UPD_DATE_HED.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SYS_UPD_TIME_HED.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SYS_UPD_DATE_DTL.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListDef.SYS_UPD_TIME_DTL.ColNo, sLabel)
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

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList
        Dim sCheck As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            '描画中断
            .SuspendLayout()

            'スプレッドのカレント行
            Dim spIdx As Integer = 0

            '集計用変数
            Dim tumiSu As Decimal = 0

            '取得データのループ
            For dtIdx As Integer = 1 To dataCnt
                Dim dr As DataRow = dt.Rows(dtIdx - 1)

                'データ判定用項目を取得
                Dim ediCtlNoChu As String = dr.Item("EDI_CTL_NO_CHU").ToString()

                'データ判定による分岐
                If Val(ediCtlNoChu) = 1 Then
                    '**明細1行目**

                    '行を追加
                    .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)
                    spIdx += 1

                    '追加行のセルスタイル設定
                    .SetCellStyle(spIdx, sprEdiListDef.DEF.ColNo, sCheck)
                    .SetCellStyle(spIdx, sprEdiListDef.RB_KBN.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.RB_KBN_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.MOD_KBN.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.MOD_KBN_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.RTN_FLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.RTN_FLG_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SND_CANCEL_FLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SND_CANCEL_FLG_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.PRTFLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.PRTFLG_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.PRTFLG_SUB.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.PRTFLG_SUB_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.NRS_SYS_FLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.NRS_SYS_FLG_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.COMBI.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_REQ_YN.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_REQ_YN_NM.ColNo, sLabel)
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_EDI_CTL_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_RTN_FLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_RTN_FLG_NM.ColNo, sLabel)
#End If
                    .SetCellStyle(spIdx, sprEdiListDef.COMBI_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SR_DEN_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.OUTKA_DATE.ColNo, sDate)
                    .SetCellStyle(spIdx, sprEdiListDef.ARRIVAL_DATE.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.INKO_DATE.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.DEST_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.DEST_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.DEST_AD_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.OUTKA_POSI_BU_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.OUTKA_POSI_BU_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.ACT_UNSO_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", True))
                    .SetCellStyle(spIdx, sprEdiListDef.MEMO_UNSO_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", True))
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_ROUTE_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J015", True))
                    .SetCellStyle(spIdx, sprEdiListDef.GOODS_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.GOODS_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_REQ.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_TANI_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_RPT.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.CAR_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, True))
                    .SetCellStyle(spIdx, sprEdiListDef.TUMI_SU.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.PRINT_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.JYUCHU_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.ORDER_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.DELIVERY_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.INV_REM_NM.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.GOODS_CD2.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.GOODS_NM2.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_REQ2.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_TANI_CD2.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SURY_RPT2.ColNo, sNum)
                    .SetCellStyle(spIdx, sprEdiListDef.HIS_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.OLD_DATA_FLG.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.NRS_BR_CD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.INOUT_KB.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.DATA_KIND.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.EDI_CTL_NO.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.EDI_CTL_NO_CHU.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.CUST_CD_L.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.CUST_CD_M.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_DATE_HED.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_TIME_HED.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_DATE_DTL.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_TIME_DTL.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_DATE_UHD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_TIME_UHD.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_DATE_UDL.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.EDI_CTL_NO_CHU_UDL.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.SYS_UPD_TIME_UDL.ColNo, sLabel)

                    '取得データをセット
                    .SetCellValue(spIdx, sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue(spIdx, sprEdiListDef.RB_KBN.ColNo, dr.Item("RB_KBN").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.RB_KBN_NM.ColNo, dr.Item("RB_KBN_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.MOD_KBN.ColNo, dr.Item("MOD_KBN").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.MOD_KBN_NM.ColNo, dr.Item("MOD_KBN_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.RTN_FLG.ColNo, dr.Item("RTN_FLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.RTN_FLG_NM.ColNo, dr.Item("RTN_FLG_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SND_CANCEL_FLG.ColNo, dr.Item("SND_CANCEL_FLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SND_CANCEL_FLG_NM.ColNo, dr.Item("SND_CANCEL_FLG_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.PRTFLG.ColNo, dr.Item("PRTFLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.PRTFLG_NM.ColNo, dr.Item("PRTFLG_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.PRTFLG_SUB.ColNo, dr.Item("PRTFLG_SUB").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.PRTFLG_SUB_NM.ColNo, dr.Item("PRTFLG_SUB_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.NRS_SYS_FLG.ColNo, dr.Item("NRS_SYS_FLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.NRS_SYS_FLG_NM.ColNo, dr.Item("NRS_SYS_FLG_NM").ToString())
                    .SetCellStyle(spIdx, sprEdiListDef.COMBI.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_REQ_YN.ColNo, sLabel)
                    .SetCellStyle(spIdx, sprEdiListDef.UNSO_REQ_YN_NM.ColNo, sLabel)
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_EDI_CTL_NO.ColNo, dr.Item("UNSO_EDI_CTL_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_RTN_FLG.ColNo, dr.Item("UNSO_RTN_FLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_RTN_FLG_NM.ColNo, dr.Item("UNSO_RTN_FLG_NM").ToString())
#End If
                    .SetCellValue(spIdx, sprEdiListDef.COMBI.ColNo, dr.Item("COMBI").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_REQ_YN.ColNo, dr.Item("UNSO_REQ_YN").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_REQ_YN_NM.ColNo, dr.Item("UNSO_REQ_YN_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.COMBI_NO.ColNo, dr.Item("COMBI_NO_A").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SR_DEN_NO.ColNo, dr.Item("SR_DEN_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.OUTKA_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("OUTKA_DATE_A").ToString()))
                    .SetCellValue(spIdx, sprEdiListDef.ARRIVAL_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("ARRIVAL_DATE_A").ToString()))
                    .SetCellValue(spIdx, sprEdiListDef.INKO_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("INKO_DATE_A").ToString()))
                    .SetCellValue(spIdx, sprEdiListDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.DEST_AD_NM.ColNo, dr.Item("DEST_AD_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.OUTKA_POSI_BU_CD.ColNo, dr.Item("OUTKA_POSI_BU_CD_PA").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.OUTKA_POSI_BU_NM.ColNo, dr.Item("OUTKA_POSI_BU_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.ACT_UNSO_CD.ColNo, dr.Item("ACT_UNSO_CD").ToString())

                    Dim actUnsoCd As String = dr.Item("ACT_UNSO_CD").ToString()
                    If Not String.IsNullOrEmpty(actUnsoCd) Then
                        actUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_NM2 = '", actUnsoCd, "' "))(0).Item("KBN_CD").ToString()
                    End If
                    .SetCellValue(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo, actUnsoCd)

                    .SetCellValue(spIdx, sprEdiListDef.MEMO_UNSO_CD.ColNo, dr.Item("UNSO_CD_MEMO").ToString())

                    Dim memoUnsoCd As String = dr.Item("UNSO_CD_MEMO").ToString()
                    If Not String.IsNullOrEmpty(memoUnsoCd) Then
                        memoUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_NM2 = '", memoUnsoCd, "' "))(0).Item("KBN_CD").ToString()
                    End If
                    .SetCellValue(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo, memoUnsoCd)

                    .SetCellValue(spIdx, sprEdiListDef.UNSO_ROUTE_CD.ColNo, dr.Item("UNSO_ROUTE_A").ToString())

                    Dim unsoRoute As String = dr.Item("UNSO_ROUTE_A").ToString()
                    If Not String.IsNullOrEmpty(unsoRoute) Then
                        unsoRoute = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_NM2 = '", unsoRoute, "' "))(0).Item("KBN_CD").ToString()
                    End If
                    .SetCellValue(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo, unsoRoute)

                    .SetCellValue(spIdx, sprEdiListDef.GOODS_CD.ColNo, dr.Item("JYUCHU_GOODS_CD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.GOODS_NM.ColNo, dr.Item("GOODS_KANA2").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SURY_REQ.ColNo, dr.Item("SURY_REQ").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SURY_TANI_CD.ColNo, dr.Item("SURY_TANI_CD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SURY_RPT.ColNo, dr.Item("SURY_RPT").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo, dr.Item("SURY_RPT_UDL").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.CAR_NO.ColNo, dr.Item("CAR_NO_A").ToString())

                    If IsNumeric(dr.Item("TUMI_SU").ToString()) Then
                        tumiSu = Convert.ToDecimal(dr.Item("TUMI_SU").ToString())
                    End If
                    .SetCellValue(spIdx, sprEdiListDef.TUMI_SU.ColNo, tumiSu.ToString())

                    .SetCellValue(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo, unsoRoute)
                    .SetCellValue(spIdx, sprEdiListDef.PRINT_NO.ColNo, dr.Item("PRINT_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.JYUCHU_NO.ColNo, dr.Item("JYUCHU_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.ORDER_NO.ColNo, dr.Item("ORDER_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.DELIVERY_NM.ColNo, dr.Item("DELIVERY_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.INV_REM_NM.ColNo, dr.Item("INV_REM_NM").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.HIS_NO.ColNo, dr.Item("HIS_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.OLD_DATA_FLG.ColNo, dr.Item("OLD_DATA_FLG").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.INOUT_KB.ColNo, dr.Item("INOUT_KB").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.DATA_KIND.ColNo, dr.Item("DATA_KIND").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.EDI_CTL_NO.ColNo, dr.Item("EDI_CTL_NO").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.EDI_CTL_NO_CHU.ColNo, dr.Item("EDI_CTL_NO_CHU").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_DATE_HED.ColNo, dr.Item("SYS_UPD_DATE_HED").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_TIME_HED.ColNo, dr.Item("SYS_UPD_TIME_HED").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_DATE_DTL.ColNo, dr.Item("SYS_UPD_DATE_DTL").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_TIME_DTL.ColNo, dr.Item("SYS_UPD_TIME_DTL").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.EDI_CTL_NO_UHD.ColNo, dr.Item("EDI_CTL_NO_UHD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_DATE_UHD.ColNo, dr.Item("SYS_UPD_DATE_UHD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_TIME_UHD.ColNo, dr.Item("SYS_UPD_TIME_UHD").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.EDI_CTL_NO_CHU_UDL.ColNo, dr.Item("EDI_CTL_NO_CHU_UDL").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_DATE_UDL.ColNo, dr.Item("SYS_UPD_DATE_UDL").ToString())
                    .SetCellValue(spIdx, sprEdiListDef.SYS_UPD_TIME_UDL.ColNo, dr.Item("SYS_UPD_TIME_UDL").ToString())

                    '行の色設定
                    If dr.Item("RB_KBN").ToString() = "1" Then
                        '赤黒区分が"赤"：文字色を赤色
                        .ActiveSheet.Rows(spIdx).ForeColor = Color.Red
                    Else
                        If dr.Item("OUTKA_POSI_BU_CD_PA").ToString() = "AW39" Then
                            '出荷場所部門コードが"日陸／千葉"
                            If dr.Item("UNSO_ROUTE_A").ToString() = "K2" Then
                                '運送手段が"トラック路線"
                                Select Case dr.Item("DEST_CD").ToString()
                                    Case "12222002", "20174002", "20175007", "20175001", "17680005"
                                        '荷届先コードが以下に該当：文字色を緑色
                                        '【12222002】東レ・ダウコーニング・シリコーン株式会社　千葉工場
                                        '【20174002】アヅマックス株式会社
                                        '【20175007】アヅマ株式会社　南工場
                                        '【20175001】アヅマ株式会社　本社
                                        '【17680005】旭電化工業株式会社　千葉工場
                                        .ActiveSheet.Rows(spIdx).ForeColor = Color.Green
                                End Select
                            End If
                        End If
                    End If

                Else
                    '**明細2行目以降**

                    '集計しつつセットし直す
                    If IsNumeric(dr.Item("TUMI_SU").ToString()) Then
                        tumiSu += Convert.ToDecimal(dr.Item("TUMI_SU").ToString())
                        .SetCellValue(spIdx, sprEdiListDef.TUMI_SU.ColNo, tumiSu.ToString())
                    End If

                    '明細番号に応じた使用項目を決定する
                    Dim goodsCdIdx As Integer = 0
                    Select Case Val(ediCtlNoChu)
                        Case 2 : goodsCdIdx = sprEdiListDef.GOODS_CD2.ColNo
                        Case 3 : Continue For
                        Case 4 : Continue For
                        Case 5 : Continue For
                        Case 6 : Continue For
                        Case 7 : Continue For
                        Case 8 : Continue For
                        Case 9 : Continue For
                    End Select

                    '取得データをセット
                    .SetCellValue(spIdx, goodsCdIdx + 0, dr.Item("JYUCHU_GOODS_CD").ToString())
                    .SetCellValue(spIdx, goodsCdIdx + 1, dr.Item("GOODS_KANA2").ToString())
                    .SetCellValue(spIdx, goodsCdIdx + 2, dr.Item("SURY_REQ").ToString())
                    .SetCellValue(spIdx, goodsCdIdx + 3, dr.Item("SURY_TANI_CD").ToString())
                    .SetCellValue(spIdx, goodsCdIdx + 4, dr.Item("SURY_RPT").ToString())

                End If
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

        With Me._Frm.sprEdiList
            '描画中断
            .SuspendLayout()

            Dim readOnlyBackColor As System.Drawing.Color = Utility.LMGUIUtility.GetReadOnlyBackColor
            Dim inputBackColor As System.Drawing.Color = Color.White

            Select Case mode
                Case LMI511C.Mode.REF
                    '参照モード
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.OUTKA_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.OUTKA_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.CAR_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.CAR_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo).BackColor = readOnlyBackColor

                Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                    '編集モード／訂正モード
                    If endAfter Then
                        For i As Integer = 1 To .ActiveSheet.Rows.Count - 1
                            .ActiveSheet.Cells(i, sprEdiListDef.DEF.ColNo).Locked = True
                        Next
                    Else
#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
                        '出庫・輸送RTN_FLG設定
                        '出庫RTN_FLG（入庫）
                        Dim OUT_RTN_FLG As String = .ActiveSheet.Cells(spIdx, sprEdiListDef.RTN_FLG.ColNo).Value.ToString()
                        '輸送RTN_FLG
                        Dim UNSO_RTN_FLG As String = .ActiveSheet.Cells(spIdx, sprEdiListDef.UNSO_RTN_FLG.ColNo).Value.ToString()
                        '入庫時編集不可INKA_FLG
                        Dim INKA_FLG As String = .ActiveSheet.Cells(spIdx, sprEdiListDef.INOUT_KB.ColNo).Value.ToString()

#End If
                        '出庫未完了時　ADD ADD 2020/08/31 013087 
                        If OUT_RTN_FLG <> LMI511C.RTN_FLG.COMP Then

                            If INKA_FLG <> LMI511C.INOUT_KB.INKA Then
                                .ActiveSheet.Cells(spIdx, sprEdiListDef.OUTKA_DATE.ColNo).Locked = False
                                .ActiveSheet.Cells(spIdx, sprEdiListDef.OUTKA_DATE.ColNo).BackColor = inputBackColor
                            End If

                            .ActiveSheet.Cells(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo).Locked = False
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.ACT_UNSO_NM.ColNo).BackColor = inputBackColor
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo).Locked = False
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.MEMO_UNSO_NM.ColNo).BackColor = inputBackColor
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT.ColNo).Locked = False
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT.ColNo).BackColor = inputBackColor
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.CAR_NO.ColNo).Locked = False
                            .ActiveSheet.Cells(spIdx, sprEdiListDef.CAR_NO.ColNo).BackColor = inputBackColor

                        End If
                        If Me._Frm.optSyuko.Checked Then
                            '出庫未完了時　ADD ADD 2020/08/31 013087 
                            If OUT_RTN_FLG <> LMI511C.RTN_FLG.COMP Then
                                .ActiveSheet.Cells(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo).Locked = False
                                .ActiveSheet.Cells(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo).BackColor = inputBackColor
                            End If
                            If String.IsNullOrEmpty(.ActiveSheet.Cells(spIdx, sprEdiListDef.EDI_CTL_NO_UHD.ColNo).Value.ToString()) = False Then
                                '運送未完了時　ADD ADD 2020/08/31 013087 
                                If UNSO_RTN_FLG <> LMI511C.RTN_FLG.COMP Then
                                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo).Locked = False
                                    .ActiveSheet.Cells(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo).BackColor = inputBackColor
                                End If
                            End If
                        End If
                    End If
            End Select

            '描画再開
            .ResumeLayout(True)
        End With

    End Sub

    ''' <summary>
    ''' Excel受け渡し時
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub OutputSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            'スプレッドをクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI511C.SprColumnIndex.LAST

            'スプレッドの列設定（見出し／幅／表示状態／列移動許可）
            objSprDef = New sprEdiListDefault
            .SetColProperty(objSprDef, True)
            sprEdiListExcel = DirectCast(objSprDef, sprEdiListDefault)

            '列固定位置を設定(ex.荷主名で固定)
            .ActiveSheet.FrozenColumnCount = 1

            '先頭検索行のセルスタイル設定
            .SetCellStyle(0, sprEdiListExcel.RB_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J022", False))
            .SetCellStyle(0, sprEdiListExcel.MOD_KBN.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.MOD_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J018", False))
            .SetCellStyle(0, sprEdiListExcel.RTN_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.RTN_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J017", False))
            .SetCellStyle(0, sprEdiListExcel.SND_CANCEL_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.SND_CANCEL_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J021", False))
            .SetCellStyle(0, sprEdiListExcel.PRTFLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.PRTFLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
            .SetCellStyle(0, sprEdiListExcel.PRTFLG_SUB.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.PRTFLG_SUB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
            .SetCellStyle(0, sprEdiListExcel.NRS_SYS_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.NRS_SYS_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J019", False))
            .SetCellStyle(0, sprEdiListExcel.UNSO_RTN_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J017", False))
            .SetCellStyle(0, sprEdiListExcel.COMBI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J020", False))
            .SetCellStyle(0, sprEdiListExcel.UNSO_REQ_YN.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.UNSO_REQ_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J020", False))
            .SetCellStyle(0, sprEdiListExcel.COMBI_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.SR_DEN_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprEdiListExcel.OUTKA_DATE.ColNo, sDate)
            .SetCellStyle(0, sprEdiListExcel.ARRIVAL_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.INKO_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprEdiListExcel.DEST_AD_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 180, False))
            .SetCellStyle(0, sprEdiListExcel.OUTKA_POSI_BU_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.OUTKA_POSI_BU_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.ACT_UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.ACT_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", False))
            .SetCellStyle(0, sprEdiListExcel.MEMO_UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.MEMO_UNSO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J016", False))
            .SetCellStyle(0, sprEdiListExcel.UNSO_ROUTE_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.UNSO_ROUTE_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.UNSO_ROUTE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "J015", False))
            .SetCellStyle(0, sprEdiListExcel.GOODS_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprEdiListExcel.SURY_REQ.ColNo, sNum)
            .SetCellStyle(0, sprEdiListExcel.SURY_TANI_CD.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.SURY_RPT.ColNo, sNum)
            .SetCellStyle(0, sprEdiListExcel.SURY_RPT_UNSO.ColNo, sNum)
            .SetCellStyle(0, sprEdiListExcel.CAR_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprEdiListExcel.TUMI_SU.ColNo, sNum)
            .SetCellStyle(0, sprEdiListExcel.PRINT_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.JYUCHU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 23, False))
            .SetCellStyle(0, sprEdiListExcel.ORDER_NO.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.DELIVERY_NM.ColNo, sLabel)
            .SetCellStyle(0, sprEdiListExcel.INV_REM_NM.ColNo, sLabel)
        End With

    End Sub
#End Region 'Form

End Class
