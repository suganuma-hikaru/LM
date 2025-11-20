' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMB     : 入荷
'  プログラムID   : LMB010G : 入荷データ一覧
'  作  成  者     : 大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMB010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

    Friend objSprDef As Object = Nothing
    Friend sprDetailDef As sprDetailDefault

    '20160615 tsunehira 修正完了のためコメントアウト
    '#Region "進捗区分英語化暫定対応" 'INOUE 20151105

    '    'Private staitc INKA＿STATE＿KB＿NM As Dictionary
    '    Private ReadOnly _inka_state_kb_nm_en As System.Collections.Generic.Dictionary(Of String, String) _
    '        = New System.Collections.Generic.Dictionary(Of String, String)() From {
    '      {"10", "Already scheduled input"},
    '      {"20", "Acceptance vote Print"},
    '      {"30", "Acceptance settled"},
    '      {"40", "Inspection completed"},
    '      {"50", "Already in stock"},
    '      {"90", "Reported"}
    '    }

    '#End Region


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        'Me.MyForm = frm
        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMBconG = New LMBControlG(DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal mode As String)

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = "完　了"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "初期荷主変更"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = always
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = True
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .grpSTATUS.TabIndex = LMB010C.CtlTabIndex_MAIN.GRPSTATUS
            .cmbEigyo.TabIndex = LMB010C.CtlTabIndex_MAIN.CMBEIGYO
            .cmbSoko.TabIndex = LMB010C.CtlTabIndex_MAIN.CMBSOKO
            .txtCustCD.TabIndex = LMB010C.CtlTabIndex_MAIN.TXTCUSTCD
            .imdNyukaDate_From.TabIndex = LMB010C.CtlTabIndex_MAIN.IMDNYUKADATE_FROM
            .imdNyukaDate_To.TabIndex = LMB010C.CtlTabIndex_MAIN.IMDNYUKADATE_TO
            .grpUNSO.TabIndex = LMB010C.CtlTabIndex_MAIN.GRPUNSO
            .grpInkaNoL.TabIndex = LMB010C.CtlTabIndex_MAIN.GRPINKANOL
            .sprDetail.TabIndex = LMB010C.CtlTabIndex_MAIN.SPRDETAIL

            'GroupBox chkSTA
            .chkStaYotei.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTAYOTEI
            .chkStaPrint.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTAPRINT
            .chkStaUketsuke.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTAUKETSUKE
            .chkStaKenpin.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTAKENPIN
            .chkStaNyuka.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTANYUKA
            .chkStaHoukoku.TabIndex = LMB010C.CtlTabIndex_chkSTA.CHKSTAHOUKOKU

            'GroupBox chkUNSO
            .chkTranUnso.TabIndex = LMB010C.CtlTabIndex_chkUNSO.CHKTRANUNSO
            .chkTranYoko.TabIndex = LMB010C.CtlTabIndex_chkUNSO.CHKTRANYOKO
            .chkTranALL.TabIndex = LMB010C.CtlTabIndex_chkUNSO.CHKTRANALL

            'GroupBox grpINKANOL
            .txtInkaNoL.TabIndex = LMB010C.CtlTabIndex_grpINKANOL.TXTINKANOL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMB010F)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm)

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMB010F)

        '=== TODO : 初期荷主取得仕様決定後　修正になる可能性あり ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.chkStaYotei.Checked() = True                                                     '進捗区分（予定入力済）
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbSoko.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()         '（自）倉庫

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            frm.cmbEigyo.ReadOnly = True
        Else
            frm.cmbEigyo.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        If getDr.Length() > 0 Then
            frm.txtCustCD.TextValue = getDr(0).Item("CUST_CD_L").ToString()                  '（初期）荷主コード（大）
            frm.lblCustNM.TextValue = getDr(0).Item("CUST_NM_L").ToString()                  '（初期）荷主名（大）
            'デフォルト倉庫コード設定 yamanaka 2013.02.26 Start
            Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST). _
            Select("SYS_DEL_FLG = '0' AND CUST_CD_L = '" & getDr(0).Item("CUST_CD_L").ToString() & _
                "' AND CUST_CD_M = '00' AND CUST_CD_S = '00' AND CUST_CD_SS = '00'")

            If getCustDr.Length() > 0 Then
                frm.cmbSoko.SelectedValue() = getCustDr(0).Item("DEFAULT_SOKO_CD").ToString()  '（初期）デフォルト倉庫
            End If
            'デフォルト倉庫コード設定 yamanaka 2013.02.26 End

        End If

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMB010C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .grpSTATUS.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .chkStaYotei.Checked = False
            .chkStaPrint.Checked = False
            .chkStaUketsuke.Checked = False
            .chkStaKenpin.Checked = False
            .chkStaNyuka.Checked = False
            .chkStaHoukoku.Checked = False
            .txtCustCD.TextValue = String.Empty
            .imdNyukaDate_From.TextValue = String.Empty
            .imdNyukaDate_To.TextValue = String.Empty
            .chkTranUnso.Checked = False
            .chkTranYoko.Checked = False
            .chkTranALL.Checked = False
            .cmbEigyo.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .txtInkaNoL.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 検索条件の荷主名のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearCustNM()

        With Me._Frm

            If String.IsNullOrEmpty(.txtCustCD.TextValue) = True Then
                .lblCustNM.TextValue = String.Empty
            End If

        End With

    End Sub

#Region "削除予定"

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMB010C.MODE_DEFAULT)

        '**********TODO:削除予定　画面モードによるロック等がある場合は、画面モードを引数でもらい処理を行う。*********************

        '画面モードはJp.Co.Nrs.Com.Constクラスで持っている。
        'EX)
        'If mode.Equals(DispMode.INIT) = True Then
        '    'ロック処理
        'ElseIf mode.Equals(DispMode.EDIT) = True Then[
        '    'ロック解除処理
        'End If

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '**********TODO:削除予定　スプレッドのデータをコントロールに反映するロック等がある場合は、ここに記入。*********************

        With Me._Frm

        End With

    End Sub

#End Region

#End Region

#Region "検索結果表示"

    '''' <summary>
    '''' 検索結果表示
    '''' </summary>
    '''' <param name="ds">検索結果格納データセット</param>
    '''' <remarks></remarks>
    'Public Sub SetSelectListDataa(ByVal ds As DataTable)

    '    'スプレッドに明細データ設定
    '    Call Me.SetSpread(ds)

    'End Sub

#End Region

#End Region 'Form

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefault

        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.DEF, " ", 20, True)
        Public OUTKA_FROM_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.OUTKA_FROM_ORD_NO_L, "オーダー番号", 100, True)
        Public STATUS_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.STATUS_NM, "進捗区分", 80, True)
        Public INKA_DATE As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_DATE, "入荷日", 80, True)
        Public CUST_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.CUST_NM, "荷主名", 150, True)
        'START YANAI 要望番号748
        Public CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.CUST_CD_S, "小ＣＤ", 60, True)
        'END YANAI 要望番号748
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.GOODS_NM, "商品名", 150, True)
        Public INKA_TTL_NB As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_TTL_NB, "個数", 100, True)
        Public WT As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WT, "重量", 100, True)
        Public REMARK As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.REMARK, "備考大(社内)", 100, True)
        Public REC_CNT As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.REC_CNT, "中レコ" & vbCrLf & "ード数", 60, True)
        Public DEST_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.DEST_NM, "出荷元", 80, True)
        Public UNCHIN_KB As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.UNCHIN_KB, "タリフ" & vbCrLf & "分類区分", 95, True)
        Public UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.UNSOCO_NM, "運送会社名", 150, True)
        Public WEB_INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WEB_INKA_NO_L, "顧客入荷管理番号", 120, True)
        Public INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_NO_L, "入荷管理番号" & vbCrLf & "（大）", 100, True)
        Public BUYER_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.BUYER_ORD_NO_L, "注文番号", 70, True)
        Public INKA_TP As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_TP, "入荷種別", 95, True)
        Public INKA_KB As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_KB, "入荷区分", 120, True)
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.LOT_NO, "ロット№", 100, True)
        Public SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.SERIAL_NO, "シリアル№", 100, True)
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Public WH_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_NM, "倉庫名", 150, True)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.NRS_BR_CD, "営業所", 0, False)
        Public NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.NRS_BR_NM, "営業所名", 150, True)
        Public TANTO_USER As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.TANTO_USER, "担当者", 150, True)
        Public SYS_ENT_USER As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.SYS_ENT_USER, "作成者", 150, True)
        Public SYS_UPD_USER As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.SYS_UPD_USER, "更新者", 150, True)

        'invisible
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.SYS_UPD_DATE, "最終更新日", 86, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.SYS_UPD_TIME, "最終更新時間", 86, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.CUST_CD_L, "荷主コード（大）", 86, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 86, False)
        Public INKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_STATE_KB, "進捗区分", 1, False)
        'START YANAI メモ②No.28
        Public WH_CD As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_CD, "倉庫コード", 1, False)
        Public OUTKA_FROM_ORD_NO_M As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.OUTKA_FROM_ORD_NO_M, "オーダー番号(中)", 1, False)
        Public INKA_TP_CD As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.INKA_TP_CD, "入荷種別", 1, False)
        'END YANAI メモ②No.28
        'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
        Public RECCNTS As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.REC_S_CNT, "入荷(小)件数", 1, False)
        Public PIC As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.PIC, "荷主主担当者", 1, False)
        'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

        Public WH_WORK_STATUS_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_WORK_STATUS_NM, "庫内作業" & vbCrLf & "ステータス", 100, True)
        Public WH_TAB_STATUS_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_TAB_STATUS_NM, "現場作業指示", 100, True)
        Public WH_TAB_WORK_STATUS_KB As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_TAB_WORK_STATUS_KB, "現場進捗区分", 0, False)
        Public WH_TAB_WORK_STATUS_NM As SpreadColProperty = New SpreadColProperty(LMB010C.SprColumnIndex.WH_TAB_WORK_STATUS_NM, "現場進捗", 100, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
            Dim spr As LMSpread = Me._Frm.sprDetail
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            'START YANAI メモ②No.28
            '.sprDetail.Sheets(0).ColumnCount() = 28
            'START YANAI 要望番号748
            '.sprDetail.Sheets(0).ColumnCount() = 31
            'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
            '.sprDetail.Sheets(0).ColumnCount() = 32
            .sprDetail.Sheets(0).ColumnCount() = LMB010C.SprColumnIndex.LAST
            'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
            'END YANAI 要望番号748
            'END YANAI メモ②No.28

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef())
            objSprDef = New sprDetailDefault
            .sprDetail.SetColProperty(objSprDef, True)
            sprDetailDef = DirectCast(objSprDef, LMB010G.sprDetailDefault)


            '列固定位置を設定します。(ex.荷主名で固定)
            '.sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.CUST_NM.ColNo + 1
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.CUST_NM.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.STATUS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True, CellType.DateTimeFormat.ShortDate))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 122, False))
            'START YANAI 要望番号748
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 2, False))
            'END YANAI 要望番号748
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False))   '検証結果_導入時要望№62対応(2011.09.13)
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999, True, 3))
            .sprDetail.SetCellStyle(0, sprDetailDef.WT.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 9999999999, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.REC_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 99, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.UNCHIN_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "T015", False))
            '.sprDetail.SetCellStyle(0, sprDetailDef.UNSO_KB.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_NM1", False, "KBN_GROUP_CD", LMKbnConst.KBN_N012))
            .sprDetail.SetCellStyle(0, sprDetailDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 122, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.WEB_INKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.BUYER_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 30, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_TP.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "N007", False))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "N006", False))
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            .sprDetail.SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            '.sprDetail.SetCellStyle(0, sprDetailDef.INKA_TP.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_NM1", False, "KBN_GROUP_CD", LMKbnConst.KBN_N007))
            '.sprDetail.SetCellStyle(0, sprDetailDef.INKA_KB.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_NM1", False, "KBN_GROUP_CD", LMKbnConst.KBN_N006))
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.TANTO_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_STATE_KB.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'START YANAI メモ②No.28
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 3, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_FROM_ORD_NO_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.INKA_TP_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 2, False))
            'END YANAI メモ②No.28
            'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
            .sprDetail.SetCellStyle(0, sprDetailDef.RECCNTS.ColNo, sLabel)  '入荷(小)件数
            .sprDetail.SetCellStyle(0, sprDetailDef.PIC.ColNo, sLabel)  '荷主主担当者
            'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする


            .sprDetail.SetCellStyle(0, sprDetailDef.WH_WORK_STATUS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "W008", False))
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_TAB_STATUS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S118", False))
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_TAB_WORK_STATUS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S124", False))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMB010F)

        With frm.sprDetail

            .Sheets(0).Cells(0, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.INKA_TP.ColNo).Value = LMConst.FLG.OFF
            .Sheets(0).Cells(0, sprDetailDef.INKA_KB.ColNo).Value = LMConst.FLG.OFF
            .Sheets(0).Cells(0, sprDetailDef.CUST_NM.ColNo).Value = String.Empty
            'START YANAI 要望番号748
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_S.ColNo).Value = String.Empty
            'END YANAI 要望番号748
            .Sheets(0).Cells(0, sprDetailDef.GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.REC_CNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNCHIN_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSOCO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.WEB_INKA_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.INKA_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BUYER_ORD_NO_L.ColNo).Value = String.Empty
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            .Sheets(0).Cells(0, sprDetailDef.LOT_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SERIAL_NO.ColNo).Value = String.Empty
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            .Sheets(0).Cells(0, sprDetailDef.WH_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRS_BR_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.TANTO_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_ENT_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty

            .Sheets(0).Cells(0, sprDetailDef.WH_WORK_STATUS_NM.ColNo).Value = String.Empty

            .Sheets(0).Cells(0, sprDetailDef.WH_TAB_STATUS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.WH_TAB_WORK_STATUS_NM.ColNo).Value = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count(), lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim cLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                '*****表示列*****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STATUS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM.ColNo, sLabel)
                'START YANAI 要望番号748
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)
                'END YANAI 要望番号748
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.WT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, True, ","))
                .SetCellStyle(i, sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REC_CNT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999, True))
                .SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WEB_INKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BUYER_ORD_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_TP.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_KB.ColNo, sLabel)
                'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SERIAL_NO.ColNo, sLabel)
                'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
                .SetCellStyle(i, sprDetailDef.WH_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TANTO_USER.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_ENT_USER.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_USER.ColNo, sLabel)

                '*****隠し列*****
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_STATE_KB.ColNo, sLabel)
                'START YANAI メモ②No.28
                .SetCellStyle(i, sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_FROM_ORD_NO_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INKA_TP_CD.ColNo, sLabel)
                'END YANAI メモ②No.28
                'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
                .SetCellStyle(i, sprDetailDef.RECCNTS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PIC.ColNo, sLabel)
                'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする


                .SetCellStyle(i, sprDetailDef.WH_WORK_STATUS_NM.ColNo, sLabel)

                .SetCellStyle(i, sprDetailDef.WH_TAB_STATUS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WH_TAB_WORK_STATUS_NM.ColNo, sLabel)

                '.SetCellStyle(i, sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                '.SetCellStyle(i, sprDetailDef.SYS_ENT_USER.ColNo, sLabel)
                '.SetCellStyle(i, sprDetailDef.SYS_UPD_USER.ColNo, sLabel)

                'セル値設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo, dr.Item("OUTKA_FROM_ORD_NO_L").ToString())

                '20160605 tsunehira 修正完了のためコメントアウト
                '#If False Then  'START INOUE 進捗区分英語化 20151105
                '                .SetCellValue(i, sprDetailDef.STATUS_NM.ColNo, dr.Item("INKA_STATE_KB_NM").ToString())
                '#Else
                '                If (Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage = LMB010C.MESSEGE_LANGUAGE_ENGLISH AndAlso _
                '                    Me._inka_state_kb_nm_en.ContainsKey(dr.Item("INKA_STATE_KB").ToString())) Then
                '                    '区分名設定
                '                    .SetCellValue(i, sprDetailDef.STATUS_NM.ColNo, Me._inka_state_kb_nm_en(dr.Item("INKA_STATE_KB").ToString()))
                '                Else
                '                    .SetCellValue(i, sprDetailDef.STATUS_NM.ColNo, dr.Item("INKA_STATE_KB_NM").ToString())
                '                End If
                '#End If 'END INOUE 進捗区分英語化 20151105        
                '20160615 tsunehira add
                .SetCellValue(i, sprDetailDef.STATUS_NM.ColNo, dr.Item("INKA_STATE_KB_NM").ToString())

                .SetCellValue(i, sprDetailDef.INKA_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString()))     '入荷日
                .SetCellValue(i, sprDetailDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                'START YANAI 要望番号748
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                'END YANAI 要望番号748
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.INKA_TTL_NB.ColNo, dr.Item("INKA_TTL_NB").ToString())
                .SetCellValue(i, sprDetailDef.WT.ColNo, dr.Item("WT").ToString())
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprDetailDef.REC_CNT.ColNo, dr.Item("REC_CNT").ToString())
                .SetCellValue(i, sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprDetailDef.UNCHIN_KB.ColNo, dr.Item("UNCHIN_NM").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, sprDetailDef.WEB_INKA_NO_L.ColNo, dr.Item("WEB_INKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.INKA_NO_L.ColNo, dr.Item("INKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.BUYER_ORD_NO_L.ColNo, dr.Item("BUYER_ORD_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.INKA_TP.ColNo, dr.Item("INKA_TP_NM").ToString())
                .SetCellValue(i, sprDetailDef.INKA_KB.ColNo, dr.Item("INKA_KB_NM").ToString())
                'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, sprDetailDef.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
                .SetCellValue(i, sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprDetailDef.TANTO_USER.ColNo, dr.Item("TANTO_USER").ToString())
                .SetCellValue(i, sprDetailDef.SYS_ENT_USER.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())

                '.SetCellValue(i, sprDetailDef.SHUKKAMOTO_CD.ColNo, dr.Item("OUTKA_FROM_ORD_NO_L").ToString())
                '.SetCellValue(i, sprDetailDef.STATUS_CD.ColNo, dr.Item("OUTKA_FROM_ORD_NO_L").ToString())
                '.SetCellValue(i, sprDetailDef.SYS_ENT_DATE.ColNo, "test")
                '.SetCellValue(i, sprDetailDef.SYS_ENT_USER.ColNo, "test")
                '.SetCellValue(i, sprDetailDef.SYS_UPD_USER.ColNo, "test")

                .SetCellValue(i, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.INKA_STATE_KB.ColNo, dr.Item("INKA_STATE_KB").ToString())
                'START YANAI メモ②No.28
                .SetCellValue(i, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_FROM_ORD_NO_M.ColNo, dr.Item("OUTKA_FROM_ORD_NO_M").ToString())
                .SetCellValue(i, sprDetailDef.INKA_TP_CD.ColNo, dr.Item("INKA_TP_CD").ToString())
                'END YANAI メモ②No.28
                'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
                .SetCellValue(i, sprDetailDef.RECCNTS.ColNo, dr.Item("REC_CNT_S").ToString())
                .SetCellValue(i, sprDetailDef.PIC.ColNo, dr.Item("PIC").ToString())
                'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする


                .SetCellValue(i, sprDetailDef.WH_WORK_STATUS_NM.ColNo, dr.Item("WH_WORK_STATUS_NM").ToString())

                .SetCellValue(i, sprDetailDef.WH_TAB_STATUS_NM.ColNo, dr.Item("WH_TAB_STATUS_NM").ToString())
                .SetCellValue(i, sprDetailDef.WH_TAB_WORK_STATUS_KB.ColNo, dr.Item("WH_TAB_WORK_STATUS_KB").ToString())
                .SetCellValue(i, sprDetailDef.WH_TAB_WORK_STATUS_NM.ColNo, dr.Item("WH_TAB_WORK_STATUS_NM").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
