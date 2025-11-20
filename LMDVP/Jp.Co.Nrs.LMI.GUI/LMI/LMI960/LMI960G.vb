' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960G : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI960Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI960G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI960F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI960F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "実績作成"
            .F2ButtonName = "受注送信"
            .F3ButtonName = "遅延送信"
            .F4ButtonName = "出荷荷主自動振分"
            .F5ButtonName = "荷主手動振分"
            .F6ButtonName = Me._Frm.WordsShukkaTouroku
            .F7ButtonName = "受注削除"
            .F8ButtonName = "StopNote表示"
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = "シリンダー取込"
            .F11ButtonName = "入荷登録"
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = always
            .F3ButtonEnabled = always
            .F4ButtonEnabled = always
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbEigyo.TabIndex = LMI960C.CtlTabIndex.EIGYO
            .btnUnsoTouroku.TabIndex = LMI960C.CtlTabIndex.BTN_UNSO_TOUROKU

            .pnlCondition.TabIndex = LMI960C.CtlTabIndex.PNL_CONDITION
            'ADD S 2020/02/07 010901
            .chkMishori.TabIndex = LMI960C.CtlTabIndex.CHK_MISHORI
            .chkJuchuOK.TabIndex = LMI960C.CtlTabIndex.CHK_JUCHU_OK
            .chkJuchuNG.TabIndex = LMI960C.CtlTabIndex.CHK_JUCHU_NG
            .chkShukkaTourokuZumi.TabIndex = LMI960C.CtlTabIndex.CHK_SHUKKA_TOUROKU_ZUMI
            .chkJissekiSakuseiZumi.TabIndex = LMI960C.CtlTabIndex.CHK_JISSEKI_SAKUSEI_ZUMI
            'ADD E 2020/02/07 010901
            .chkTorikeshi.TabIndex = LMI960C.CtlTabIndex.CHK_TORIKESHI
            .cmbBumon.TabIndex = LMI960C.CtlTabIndex.BUMON
            .imdOutkaDateFrom.TabIndex = LMI960C.CtlTabIndex.OUTKA_DATE_FROM
            .imdOutkaDateTo.TabIndex = LMI960C.CtlTabIndex.OUTKA_DATE_TO
            'ADD START 2019/03/27
            .chkMitei.TabIndex = LMI960C.CtlTabIndex.CHK_MITEI
            .chkInka.TabIndex = LMI960C.CtlTabIndex.CHK_INKA
            .chkOutka.TabIndex = LMI960C.CtlTabIndex.CHK_OUTKA
            .chkUnso.TabIndex = LMI960C.CtlTabIndex.CHK_UNSO

            .pnlIkkatsuChange.TabIndex = LMI960C.CtlTabIndex.PNL_IKKATSU_CHANGE
            .cmbChangeItem.TabIndex = LMI960C.CtlTabIndex.CMB_CHANGE_ITEM
            .imdChangeDate.TabIndex = LMI960C.CtlTabIndex.IMD_CHANGE_DATE
            .btnIkkatsuChange.TabIndex = LMI960C.CtlTabIndex.BTN_IKKATSU_CHANGE
            'ADD END   2019/03/27

            'ADD S 2019/12/12 009741
            .pnlJuchu.TabIndex = LMI960C.CtlTabIndex.PNL_JUCHU
            .optJuchuYes.TabIndex = LMI960C.CtlTabIndex.OPT_JUCHU_OK
            .optJuchuNo.TabIndex = LMI960C.CtlTabIndex.OPT_JUCHU_NG
            'ADD E 2019/12/12 009741
            .cmbDeclineReason.TabIndex = LMI960C.CtlTabIndex.CMB_DECLINE_REASON  'ADD 2020/03/06 011377

            .pnlHaisoChien.TabIndex = LMI960C.CtlTabIndex.PNL_DELAY
            .cmbDelayShubetsu.TabIndex = LMI960C.CtlTabIndex.CMB_DELAY_SHUBETSU
            .cmbDelayReason.TabIndex = LMI960C.CtlTabIndex.CMB_DELAY_REASON
            .cmbDelayHosoku.TabIndex = LMI960C.CtlTabIndex.CMB_DELAY_HOSOKU

            .pnlSakuseiNaiyo.TabIndex = LMI960C.CtlTabIndex.PNL_SAKUSEI_NAIYO
            .cmbBashoKb.TabIndex = LMI960C.CtlTabIndex.BASHO_KB
            .dtpArrivalTime.TabIndex = LMI960C.CtlTabIndex.ARRIVAL_TIME
            .dtpDepartureTime.TabIndex = LMI960C.CtlTabIndex.DEPARTURE_TIME

            .sprDetail.TabIndex = LMI960C.CtlTabIndex.DETAIL

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
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()
            .chkMishori.Focus()  'MOD 2020/03/06 011377

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.TextValue = String.Empty
            'ADD S 2020/02/07 010901
            .chkMishori.Checked = False
            .chkJuchuOK.Checked = False
            .chkJuchuNG.Checked = False
            .chkShukkaTourokuZumi.Checked = False
            .chkJissekiSakuseiZumi.Checked = False
            'ADD E 2020/02/07 010901
            .cmbBumon.TextValue = String.Empty
            .imdOutkaDateFrom.TextValue = String.Empty
            .imdOutkaDateTo.TextValue = String.Empty
            .cmbBashoKb.TextValue = String.Empty
            .dtpArrivalTime.TextValue = String.Empty
            .dtpDepartureTime.TextValue = String.Empty
            .cmbDeclineReason.TextValue = String.Empty  'ADD 2020/03/06 011377

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '営業所の値を設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd
            Me._Frm.cmbEigyo.ReadOnly = True

            'cmbBumonの値を設定
            .cmbBumon.Items.Add(LMI960C.CmbBumonItems.Soko)
            .cmbBumon.Items.Add(LMI960C.CmbBumonItems.ISO)
            .cmbBumon.Items.Add(LMI960C.CmbBumonItems.ChilledLorry)
            .cmbBumon.SelectedIndex = 0

            'cmbBashoKbの値を設定
            .cmbBashoKb.Items.Add(LMI960C.CmbBashoKbItems.Tsumikomi)
            .cmbBashoKb.Items.Add(LMI960C.CmbBashoKbItems.NonyuYotei)
            .cmbBashoKb.Items.Add(LMI960C.CmbBashoKbItems.Nioroshi)
            .cmbBashoKb.SelectedIndex = 0

            'DEL S 202/07/17  LMComboKubunのプロパティDataCode、Value1、ValueExtractPatternで設定するよう修正
            ''ADD S 2020/03/06 011377
            ''cmbDeclineReasonのDataSourceを設定
            'Dim dtH033 As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'H033' AND VALUE1 = 1.0", "KBN_CD ASC").CopyToDataTable
            'Dim dtDataSrc As DataTable = New DataTable("COMBO_TBL")
            'dtDataSrc.Columns.Add("KBN_NM1")
            'dtDataSrc.Columns.Add("KBN_CD")
            'Dim drDataSrc As DataRow = dtDataSrc.NewRow
            'drDataSrc("KBN_NM1") = ""
            'drDataSrc("KBN_CD") = ""
            'dtDataSrc.Rows.Add(drDataSrc)
            'For Each drH033 As DataRow In dtH033.Rows
            '    drDataSrc = dtDataSrc.NewRow
            '    drDataSrc("KBN_NM1") = drH033("KBN_NM1")
            '    drDataSrc("KBN_CD") = drH033("KBN_CD")
            '    dtDataSrc.Rows.Add(drDataSrc)
            'Next
            '.cmbDeclineReason.DataSource = dtDataSrc
            ''ADD E 2020/03/06 011377
            '.cmbDelayReason.DataSource = dtDataSrc.Copy
            'DEL E 202/07/17

            .imdOutkaDateFrom.TextValue = Now.ToString("yyyyMMdd")

        End With

    End Sub

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 部門(cmbBumonの選択値)に応じてキャプション、活性・非活性などを変更する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ChangeCaption()

        With Me._Frm

            Select Case .ProcessingBumon
                Case LMI960C.CmbBumonItems.Soko, LMI960C.CmbBumonItems.ChilledLorry
                    .FunctionKey.F4ButtonEnabled = True
                    .FunctionKey.F5ButtonEnabled = True
                    .FunctionKey.F6ButtonName = .WordsShukkaTouroku
                    .FunctionKey.F6ButtonEnabled = True
                    .FunctionKey.F7ButtonEnabled = False
                    .FunctionKey.F10ButtonEnabled = True
                    .FunctionKey.F11ButtonEnabled = True
                    .btnUnsoTouroku.Enabled = True

                    .chkShukkaTourokuZumi.Text = LMI960C.JuchuStatusName.NyuShukkaTourokuZumi

                    .sprDetail.ActiveSheet.ColumnHeader.Cells(0, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo).Text = LMI960C.ColTitleInoutkaCtlNo.ForSoko

                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.GOODS_CD.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.GOODS_NM.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.SAP_ORD_NO.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.SHUKKA_MOTO_CD.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.NONYU_SAKI_CD.ColNo, False)

                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CYLINDER_SERIAL_NO.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.INOUT_KB.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_CD_L.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_CD_M.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_ORD_NO.ColNo, True)

                Case LMI960C.CmbBumonItems.ISO
                    .FunctionKey.F4ButtonEnabled = False
                    .FunctionKey.F5ButtonEnabled = False
                    .FunctionKey.F6ButtonName = .WordsShukkaTouroku
                    .FunctionKey.F6ButtonEnabled = True
                    .FunctionKey.F7ButtonEnabled = True
                    .FunctionKey.F10ButtonEnabled = False
                    .FunctionKey.F11ButtonEnabled = False
                    .btnUnsoTouroku.Enabled = False

                    .chkShukkaTourokuZumi.Text = LMI960C.JuchuStatusName.JuchuTourokuZumi

                    .sprDetail.ActiveSheet.ColumnHeader.Cells(0, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo).Text = LMI960C.ColTitleInoutkaCtlNo.ForISO

                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.GOODS_CD.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.GOODS_NM.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.SAP_ORD_NO.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.SHUKKA_MOTO_CD.ColNo, True)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.NONYU_SAKI_CD.ColNo, True)

                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CYLINDER_SERIAL_NO.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.INOUT_KB.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_CD_L.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_CD_M.ColNo, False)
                    .sprDetail.ActiveSheet.SetColumnVisible(sprDetailDef.CUST_ORD_NO.ColNo, False)

            End Select

        End With

    End Sub
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' 部門(倉庫/ISO)選択値変更時のコントールプロパティの変更
    ''' </summary>
    Friend Sub ChangeControlsPropertyWhenBumonChanged()

        With Me._Frm

            Select Case .cmbBumon.TextValue
                Case LMI960C.CmbBumonItems.Soko, LMI960C.CmbBumonItems.ChilledLorry
                    .pnlInOutKb.Visible = True

                Case LMI960C.CmbBumonItems.ISO
                    .pnlInOutKb.Visible = False

            End Select

            Me._Frm.sprDetail.CrearSpread()

            '検索データの部門(倉庫/ISO)を保存
            .ProcessingBumon = .cmbBumon.TextValue
            Select Case .ProcessingBumon
                Case LMI960C.CmbBumonItems.Soko, LMI960C.CmbBumonItems.ChilledLorry
                    .WordsNyuShukka = LMI960C.WordsNyuShukka.ForSoko
                    .WordsShukkaTouroku = LMI960C.WordsShukkaTouroku.ForSoko
                    .WordsNyuShukkaTourokuZumi = LMI960C.JuchuStatusName.NyuShukkaTourokuZumi
                Case LMI960C.CmbBumonItems.ISO
                    .WordsNyuShukka = LMI960C.WordsNyuShukka.ForISO
                    .WordsShukkaTouroku = LMI960C.WordsShukkaTouroku.ForISO
                    .WordsNyuShukkaTourokuZumi = LMI960C.JuchuStatusName.JuchuTourokuZumi
            End Select

            Call Me.ChangeCaption()

        End With

    End Sub

    ''' <summary>
    ''' 一括変更項目変更時のコントールプロパティの変更
    ''' </summary>
    Friend Sub ChangeControlsPropertyWhenChangeItemChanged()

        With Me._Frm

            Select Case .cmbChangeItem.SelectedValue.ToString
                Case LMI960C.CmbIkkatsuChangeItems.ShukkaDate, LMI960C.CmbIkkatsuChangeItems.NonyuDate '出荷日、納入日
                    .txtJobNo.Visible = False
                    .imdChangeDate.Visible = True

                Case LMI960C.CmbIkkatsuChangeItems.JobNo
                    .txtJobNo.Visible = True
                    .imdChangeDate.Visible = False

                Case Else
                    .txtJobNo.Visible = False
                    .imdChangeDate.Visible = False

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 場所区分変更時のコントールプロパティの変更
    ''' </summary>
    Friend Sub ChangeControlsPropertyWhenBashoKbChanged()

        With Me._Frm

            Select Case .cmbBashoKb.TextValue
                Case LMI960C.CmbBashoKbItems.NonyuYotei
                    '納入予定の場合、出発時刻は使用不可
                    .dtpDepartureTime.TextValue = ""
                    .dtpDepartureTime.Enabled = False

                Case Else
                    .dtpDepartureTime.Enabled = True

            End Select

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.DEF, " ", 20, True)
        'ADD S 2019/12/12 009741
        Public Shared REC_STATUS As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.REC_STATUS, "TMC" & vbCrLf & "取消", 60, True)
        Public Shared HORYU_KB As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.HORYU_KB, "保留", 60, True)
        Public Shared JUCHU_STATUS As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.JUCHU_STATUS, "受注" & vbCrLf & "ステータス", 80, True)
        'ADD E 2019/12/12 009741
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STATUS, "配送" & vbCrLf & "ステータス", 80, True)
        Public Shared DELAY_STATUS As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.DELAY_STATUS, "遅延" & vbCrLf & "ステータス", 80, True)
        Public Shared CYLINDER_SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.CYLINDER_SERIAL_NO, "シリンダー番号", 160, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.GOODS_CD, "商品コード", 80, False)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.GOODS_NM, "商品", 150, False)
        Public Shared LOAD_NUMBER As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.LOAD_NUMBER, "Load Number", 100, True)
        Public Shared SAP_ORD_NO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SAP_ORD_NO, "HNW SAP Order No", 120, False)    'ISO用
        Public Shared CUST_ORD_NO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.CUST_ORD_NO, "HNW SAP Order No", 120, True)   '倉庫用
        Public Shared INOUT_KB As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.INOUT_KB, "入出荷区分", 50, True)
        Public Shared INOUTKA_CTL_NO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.INOUTKA_CTL_NO, LMI960C.ColTitleInoutkaCtlNo.ForSoko, 100, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.CUST_CD_L, "荷主CD(大)", 80, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.CUST_CD_M, "荷主CD(中)", 50, True)
        Public Shared SHUKKA_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SHUKKA_DATE, "出荷日", 80, True)
        Public Shared NONYU_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.NONYU_DATE, "納入日", 136, True)
        Public Shared SHUKKA_MOTO_CD As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SHUKKA_MOTO_CD, "出荷元コード", 100, False)
        Public Shared SHUKKA_MOTO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SHUKKA_MOTO, "出荷元", 300, True)
        Public Shared NONYU_SAKI_CD As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.NONYU_SAKI_CD, "納入先コード", 100, False)
        Public Shared NONYU_SAKI As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.NONYU_SAKI, "納入先", 300, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.JURYO, "重量", 100, True)
        Public Shared HED_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.HED_CRT_DATE, "受信日", 80, True)
        Public Shared HED_FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.HED_FILE_NAME, "受信ファイル名", 160, True)
        Public Shared BUYID As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.BUYID, "売上先コード", 90, True)

        '非表示項目
        Public Shared HED_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.HED_UPD_DATE, "HED_UPD_DATE", 0, False)
        Public Shared HED_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.HED_UPD_TIME, "HED_UPD_TIME", 0, False)
        'ADD START 2019/03/27
        Public Shared STP1_GYO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP1_GYO, "STP1_GYO", 0, False)
        Public Shared STP1_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP1_UPD_DATE, "STP1_UPD_DATE", 0, False)
        Public Shared STP1_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP1_UPD_TIME, "STP1_UPD_TIME", 0, False)
        Public Shared STP2_GYO As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP2_GYO, "STP2_GYO", 0, False)
        Public Shared STP2_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP2_UPD_DATE, "STP2_UPD_DATE", 0, False)
        Public Shared STP2_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.STP2_UPD_TIME, "STP2_UPD_TIME", 0, False)
        'ADD END   2019/03/27
        Public Shared P_STOP_NOTE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.P_STOP_NOTE, "P_STOP_NOTE", 0, False)
        Public Shared D_STOP_NOTE As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.D_STOP_NOTE, "D_STOP_NOTE", 0, False)
        Public Shared SKU_NUMBER As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SKU_NUMBER, "SKU_NUMBER", 0, False)
        Public Shared NUMBER_PIECES As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.NUMBER_PIECES, "NUMBER_PIECES", 0, False)
        Public Shared INOUTKA_CTL_NO_DELETED As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.INOUTKA_CTL_NO_DELETED, "INOUTKA_CTL_NO_DELETED", 0, False)
        Public Shared SEQ_DESC As SpreadColProperty = New SpreadColProperty(LMI960C.SprColumnIndex.SEQ_DESC, "SEQ_DESC", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .ActiveSheet.Rows.Count = 1    'MOD 2020/02/07 010901

            '列数設定
            .ActiveSheet.ColumnCount = LMI960C.SprColumnIndex.LAST

            .SetColProperty(New LMI960G.sprDetailDef(), True)

            '列固定位置を設定
            .ActiveSheet.FrozenColumnCount = LMI960G.sprDetailDef.LOAD_NUMBER.ColNo + 1

            'ADD S 2020/02/07 010901
            'セルに設定するスタイルの取得
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

            'セルスタイル設定
            .SetCellStyle(0, LMI960G.sprDetailDef.REC_STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H030", False))
            .SetCellStyle(0, LMI960G.sprDetailDef.HORYU_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "E011", False))
            .SetCellStyle(0, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H032", False))
            .SetCellStyle(0, LMI960G.sprDetailDef.DELAY_STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "H035", False))
            .SetCellStyle(0, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 3000, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 8, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 100, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 256, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.SAP_ORD_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 256, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.CUST_ORD_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 256, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.INOUT_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 10, False))
            'ADD S 2020/02/27 010901
            .SetCellStyle(0, LMI960G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 5, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 2, False))
            'ADD E 2020/02/27 010901
            .SetCellStyle(0, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.NONYU_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.SHUKKA_MOTO_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.SHUKKA_MOTO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 100, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.NONYU_SAKI_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.NONYU_SAKI.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 100, False))
            .SetCellStyle(0, LMI960G.sprDetailDef.JURYO.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo, sLabel)
            'ADD E 2020/02/07 010901
            .SetCellStyle(0, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.BUYID.ColNo, sLabel)

            '非表示列
            .SetCellStyle(0, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP1_GYO.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP1_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP1_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP2_GYO.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP2_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.STP2_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.P_STOP_NOTE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.D_STOP_NOTE.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.SKU_NUMBER.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.NUMBER_PIECES.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.INOUTKA_CTL_NO_DELETED.ColNo, sLabel)
            .SetCellStyle(0, LMI960G.sprDetailDef.SEQ_DESC.ColNo, sLabel)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMI960C.TABLE_NM_OUT)

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()
            .ActiveSheet.Rows.Count = 1    'MOD 2020/02/07 010901

            '列固定位置を設定
            .ActiveSheet.FrozenColumnCount = LMI960G.sprDetailDef.LOAD_NUMBER.ColNo + 1

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lDataCnt As Integer = dt.Rows.Count
            If lDataCnt = 0 Then
                .ResumeLayout(True)
                Return True
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lDataCnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lDataCnt    'MOD 2020/02/07 010901

                dr = dt.Rows(i - 1)             'MOD 2020/02/07 010901

                'セルスタイル設定
                .SetCellStyle(i, LMI960G.sprDetailDef.DEF.ColNo, sDEF)
                'ADD S 2019/12/12 009741
                .SetCellStyle(i, LMI960G.sprDetailDef.REC_STATUS.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.HORYU_KB.ColNo, sLabel)       'ADD 2020/02/07 010901
                .SetCellStyle(i, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, sLabel)
                'ADD E 2019/12/12 009741
                .SetCellStyle(i, LMI960G.sprDetailDef.STATUS.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.DELAY_STATUS.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.SAP_ORD_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.CUST_ORD_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.INOUT_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo, sLabel)   'ADD 2020/02/07 010901
                .SetCellStyle(i, LMI960G.sprDetailDef.CUST_CD_L.ColNo, sLabel)   'ADD 2020/02/27 010901
                .SetCellStyle(i, LMI960G.sprDetailDef.CUST_CD_M.ColNo, sLabel)   'ADD 2020/02/27 010901
                .SetCellStyle(i, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.NONYU_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.SHUKKA_MOTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.SHUKKA_MOTO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.NONYU_SAKI_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.NONYU_SAKI.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.JURYO.ColNo, sLabelRight)
                .SetCellStyle(i, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.BUYID.ColNo, sLabel)
                '非表示列
                .SetCellStyle(i, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP1_GYO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP1_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP1_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP2_GYO.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP2_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.STP2_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.P_STOP_NOTE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.D_STOP_NOTE.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.SKU_NUMBER.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.NUMBER_PIECES.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.INOUTKA_CTL_NO_DELETED.ColNo, sLabel)
                .SetCellStyle(i, LMI960G.sprDetailDef.SEQ_DESC.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMI960G.sprDetailDef.DEF.ColNo, False.ToString())
                'ADD S 2019/12/12 009741
                .SetCellValue(i, LMI960G.sprDetailDef.REC_STATUS.ColNo, dr.Item("STATUS_KB").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.HORYU_KB.ColNo, dr.Item("DEL_KB").ToString())             'ADD 2020/02/07 010901
                .SetCellValue(i, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, dr.Item("SHINCHOKU_KB_JUCHU").ToString())
                'ADD E 2019/12/12 009741
                .SetCellValue(i, LMI960G.sprDetailDef.STATUS.ColNo, dr.Item("SHINCHOKU_KB").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.DELAY_STATUS.ColNo, dr.Item("DELAY_STATUS").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo, dr.Item("CYLINDER_SERIAL_NO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.GOODS_CD.ColNo, dr.Item("GOODS_CD").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo, dr.Item("SHIPMENT_ID").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.SAP_ORD_NO.ColNo, dr.Item("SAP_ORD_NO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.CUST_ORD_NO.ColNo, dr.Item("CUST_ORD_NO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.INOUT_KB.ColNo, dr.Item("INOUT_KB").ToString())   'ADD 2020/02/07 010901
                .SetCellValue(i, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo, dr.Item("OUTKA_CTL_NO").ToString())   'ADD 2020/02/07 010901
                .SetCellValue(i, LMI960G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())   'ADD 2020/02/27 010901
                .SetCellValue(i, LMI960G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())   'ADD 2020/02/27 010901
                .SetCellValue(i, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo, DateFormatUtility.EditSlash(Left(dr.Item("SHUKKA_DATE").ToString(), 8)))
                .SetCellValue(i, LMI960G.sprDetailDef.NONYU_DATE.ColNo, String.Concat(DateFormatUtility.EditSlash(Left(dr.Item("NONYU_DATE").ToString(), 8)), " ", TimeFormatData(dr.Item("NONYU_DATE").ToString())))
                .SetCellValue(i, LMI960G.sprDetailDef.SHUKKA_MOTO_CD.ColNo, dr.Item("SHUKKA_MOTO_CD").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.SHUKKA_MOTO.ColNo, dr.Item("SHUKKA_MOTO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.NONYU_SAKI_CD.ColNo, dr.Item("NONYU_SAKI_CD").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.NONYU_SAKI.ColNo, dr.Item("NONYU_SAKI").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.JURYO.ColNo, Format(CDec(dr.Item("MAXIMUM_WEIGHT")), "#,0.###") & "kg")
                .SetCellValue(i, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("HED_CRT_DATE").ToString()))    'MOD 2019/12/12 009741
                .SetCellValue(i, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo, dr.Item("HED_FILE_NAME").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.BUYID.ColNo, dr.Item("BUYID").ToString())

                .SetCellValue(i, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo, dr.Item("HED_UPD_DATE").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo, dr.Item("HED_UPD_TIME").ToString())
                'ADD START 2019/03/27
                .SetCellValue(i, LMI960G.sprDetailDef.STP1_GYO.ColNo, dr.Item("STP1_GYO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.STP1_UPD_DATE.ColNo, dr.Item("STP1_UPD_DATE").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.STP1_UPD_TIME.ColNo, dr.Item("STP1_UPD_TIME").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.STP2_GYO.ColNo, dr.Item("STP2_GYO").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.STP2_UPD_DATE.ColNo, dr.Item("STP2_UPD_DATE").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.STP2_UPD_TIME.ColNo, dr.Item("STP2_UPD_TIME").ToString())
                'ADD END   2019/03/27
                .SetCellValue(i, LMI960G.sprDetailDef.P_STOP_NOTE.ColNo, dr.Item("P_STOP_NOTE").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.D_STOP_NOTE.ColNo, dr.Item("D_STOP_NOTE").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.SKU_NUMBER.ColNo, dr.Item("SKU_NUMBER").ToString().Substring(1))
                .SetCellValue(i, LMI960G.sprDetailDef.NUMBER_PIECES.ColNo, dr.Item("NUMBER_PIECES").ToString().Substring(1))
                .SetCellValue(i, LMI960G.sprDetailDef.INOUTKA_CTL_NO_DELETED.ColNo, dr.Item("OUTKA_CTL_NO_DELETED").ToString())
                .SetCellValue(i, LMI960G.sprDetailDef.SEQ_DESC.ColNo, dr.Item("SEQ_DESC").ToString())

                'ADD S 2019/12/12 009741
                '文字色の設定

                '通常は黒
                .ActiveSheet.Rows(i).ForeColor = Color.Black

                '条件に応じて色を変える
                Select Case Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.REC_STATUS.ColNo))
                    Case LMI960C.RecStatusName.Cancelled    'DEL 2020/02/07 010901 , LMI960C.RecStatusName.Updated
                        'DEL 2020/02/07 010901 Select Case Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo))
                        'DEL 2020/02/07 010901     Case LMI960C.JuchuStatusName.JuchuOK, LMI960C.JuchuStatusName.JuchuNG
                        'TMC取消が「取消」の場合
                        .ActiveSheet.Rows(i).ForeColor = Color.Red   '赤 'MOD 2020/02/07 010901
                        'DEL 2020/02/07 010901 End Select
                End Select
                'ADD E 2019/12/12 009741
                'ADD S 2020/02/07 010901
                Select Case Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.HORYU_KB.ColNo))
                    Case LMI960C.DelKbName.Horyu
                        '保留区分が「保留」の場合
                        .ActiveSheet.Rows(i).ForeColor = Color.Blue   '青
                End Select
                Select Case Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo))
                    Case LMI960C.DELETED
                        '入出荷管理番号/JOB NOが「削除済」の場合
                        If .ActiveSheet.Rows(i).ForeColor <> Color.Red Then
                            .ActiveSheet.Rows(i).ForeColor = Color.DarkViolet   '紫
                        End If
                End Select
                'ADD E 2020/02/07 010901
                If Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.SEQ_DESC.ColNo)) <> "1" OrElse
                   Me._LMFconG.GetCellValue(.ActiveSheet.Cells(i, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) = LMI960C.JuchuStatusName.EdiTorikeshi Then
                    '同一LoadNumberの最新データでない、または、受注ステータスが「EDI取消」の場合
                    .ActiveSheet.Rows(i).LockBackColor = Color.LightGray   '灰色
                End If

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    'DEL S 2020/04/22 012106
    '''' <summary>
    '''' スプレッドのデータを更新
    '''' </summary>
    '''' <param name="frm">frm</param>
    '''' <param name="arr">arr</param>
    '''' <remarks></remarks>
    'Friend Function SetUpdSpread(ByVal frm As LMI960F, ByVal arr As ArrayList, ByVal eventShubetsu As LMI960C.EventShubetsu, Optional ByVal ds As DataSet = Nothing) As Boolean    'MOD 2020/03/06 011377
    '
    '    Dim spr As LMSpread = Me._Frm.sprDetail
    '    Dim max As Integer = arr.Count - 1
    '    Dim rowNo As Integer = 0
    '
    '    '実行後の配送ステータス(実績作成用)
    '    Dim doneStatus As String = String.Empty
    '    Select Case frm.cmbBashoKb.TextValue
    '
    '        Case LMI960C.CmbBashoKbItems.Tsumikomi
    '            '場所区分が「積込場」の場合、「ピック済」
    '            doneStatus = LMI960C.StatusName.PickZumi
    '
    '        Case LMI960C.CmbBashoKbItems.Nioroshi
    '            '場所区分が「荷下場」の場合、「荷下ろし済」
    '            doneStatus = LMI960C.StatusName.NioroshiZumi
    '
    '    End Select
    '
    '    '実行後の受注ステータス(受注作成用)
    '    Dim juchuStatus As String
    '    If frm.optJuchuYes.Checked Then
    '        juchuStatus = LMI960C.JuchuStatusName.JuchuOK
    '    Else
    '        juchuStatus = LMI960C.JuchuStatusName.JuchuNG
    '    End If
    '
    '    'ADD S 2020/03/06 011377
    '    Dim custCdL As String = String.Empty
    '    Dim custCdM As String = String.Empty
    '    If eventShubetsu = LMI960C.EventShubetsu.UPDATE_CUST_MANUAL Then
    '        If ds IsNot Nothing Then
    '            custCdL = ds.Tables("LMI960M_CUST").Rows(0).Item("CUST_CD_L").ToString()
    '            custCdM = ds.Tables("LMI960M_CUST").Rows(0).Item("CUST_CD_M").ToString()
    '        End If
    '    End If
    '    'ADD E 2020/03/06 011377
    '
    '    With spr
    '
    '        .SuspendLayout()
    '
    '        For i As Integer = 0 To max
    '
    '            rowNo = Convert.ToInt32(arr(i))
    '
    '            'ADD START 2019/03/27
    '            Select Case eventShubetsu
    '                Case LMI960C.EventShubetsu.JISSEKI_SAKUSEI
    '                    '実績作成
    '                    'ADD END  2019/03/27
    '
    '                    'セルに値を設定(配送ステータス)
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.STATUS.ColNo, doneStatus)
    '
    '                    'ADD S 2020/02/07 010901
    '                    Select Case frm.cmbBashoKb.TextValue
    '                        Case LMI960C.CmbBashoKbItems.Nioroshi
    '                            'セルに値を設定(受注ステータス)
    '                            .SetCellValue(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, LMI960C.JuchuStatusName.JissekiSakuseiZumi)
    '                    End Select
    '                    'ADD S 2020/02/07 010901
    '
    '                    'ADD S 2019/12/12 009741
    '                Case LMI960C.EventShubetsu.JUCHU_SAKUSEI
    '                    '受注作成
    '
    '                    'セルに値を設定(受注ステータス)
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, juchuStatus)
    '                    'ADD E 2019/12/12 009741
    '
    '                    'ADD START 2019/03/27
    '                Case LMI960C.EventShubetsu.IKKATSU_CHANGE
    '                    '一括変更
    '
    '                    Dim strDate As String = DateFormatUtility.EditSlash(frm.imdChangeDate.TextValue)
    '
    '                    Select Case frm.cmbChangeItem.SelectedValue.ToString()
    '
    '                        Case LMI960C.CmbIkkatsuChangeItems.ShukkaDate
    '                            'セルに値を設定(出荷日)
    '                            .SetCellValue(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo, strDate)
    '
    '                        Case LMI960C.CmbIkkatsuChangeItems.NonyuDate
    '                            'セルに値を設定(納入日)
    '                            .SetCellValue(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo, strDate)
    '
    '                    End Select
    '
    '                    'ADD S 2020/02/07 010901
    '                Case LMI960C.EventShubetsu.SHUKKA_TOUROKU
    '                    '出荷登録
    '
    '                    'セルに値を設定(受注ステータス)
    '                    Select Case frm.ProcessingBumon
    '                        Case LMI960C.CmbBumonItems.ISO
    '                            .SetCellValue(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo, LMI960C.JuchuStatusName.JuchuTourokuZumi)
    '                    End Select
    '                    'セルに値を設定(保留)
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.HORYU_KB.ColNo, LMI960C.DelKbName.Seijou)
    '                    .ActiveSheet.Rows(rowNo).ForeColor = Color.Black   '黒
    '
    '                Case LMI960C.EventShubetsu.DELETE_GLIS_JUCHU
    '                    '受注削除
    '
    '                    'セルに値を設定(JOB NO)
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.OUTKA_CTL_NO.ColNo, LMI960C.DELETED)
    '                    .ActiveSheet.Rows(rowNo).ForeColor = Color.DarkViolet   '紫
    '                    'ADD E 2020/02/07 010901
    '
    '                    'ADD S 2020/03/06 011377
    '                Case LMI960C.EventShubetsu.UPDATE_CUST_MANUAL
    '                    '荷主手動振分
    '
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo, custCdL)
    '                    .SetCellValue(rowNo, LMI960G.sprDetailDef.CUST_CD_M.ColNo, custCdM)
    '                    'ADD E 2020/03/06 011377
    '
    '            End Select
    '            'ADD END  2019/03/27
    '        Next
    '
    '        .ResumeLayout(True)
    '
    '        Return True
    '
    '    End With
    '
    'End Function
    'DEL E 2020/04/22 012106

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left, Integer.MaxValue)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabelRight(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数14桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum14(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function

#End Region

    ''' <summary>
    ''' 時間(コロン)編集
    ''' </summary>
    ''' <param name="dateTimeValue">値(yyyyMMddHHmmss)</param>
    ''' <returns>コロンを含む8桁を返却　渡された値の時分秒部が空または6桁未満の場合、そのまま返却</returns>
    ''' <remarks></remarks>
    Private Function TimeFormatData(ByVal dateTimeValue As String) As String

        Dim value As String

        ' 空の場合、そのまま返却
        If String.IsNullOrEmpty(dateTimeValue) = True _
            OrElse dateTimeValue.Length <= "yyyyMMdd".Length Then
            Return ""
        End If

        value = dateTimeValue.Substring("yyyyMMdd".Length)
        If value.Length < "HHmmss".Length Then
            Return ""
        End If

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

#End Region

End Class
