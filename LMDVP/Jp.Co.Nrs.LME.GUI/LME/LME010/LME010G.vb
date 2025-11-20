' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME010F : 作業料明細書作成
'  作  成  者       :  Nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LME010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LME010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME010F)

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
    Friend Sub SetFunctionKey(ByVal mode As String)
        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            'START YANAI 20120319　作業画面改造
            '.F1ButtonName = String.Empty
            .F1ButtonName = "新　規"
            'END YANAI 20120319　作業画面改造
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            'START YANAI 20120319　作業画面改造
            '.F4ButtonName = String.Empty
            .F4ButtonName = "連続入力"
            'END YANAI 20120319　作業画面改造
            .F5ButtonName = "確　定"
            .F6ButtonName = "確定解除"
            .F7ButtonName = "初期荷主変更"
            'START YANAI 20120319　作業画面改造
            '.F8ButtonName = String.Empty
            .F8ButtonName = "完　了"
            'END YANAI 20120319　作業画面改造
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            'START YANAI 要望番号811
            '.F11ButtonName = "保　存"
            .F11ButtonName = String.Empty
            'END YANAI 要望番号811
            .F12ButtonName = "閉じる"
            'ファンクションキーの制御
            'START YANAI 20120319　作業画面改造
            '.F1ButtonEnabled = False
            .F1ButtonEnabled = always
            'END YANAI 20120319　作業画面改造
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            'START YANAI 20120319　作業画面改造
            '.F4ButtonEnabled = False
            .F4ButtonEnabled = always
            'END YANAI 20120319　作業画面改造
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = always
            'START YANAI 20120319　作業画面改造
            '.F8ButtonEnabled = False
            .F8ButtonEnabled = always
            'END YANAI 20120319　作業画面改造
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            'START YANAI 20120319　作業画面改造
            '.F11ButtonEnabled = always
            .F11ButtonEnabled = False
            'END YANAI 20120319　作業画面改造
            .F12ButtonEnabled = always

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
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            'Main

            .cmbPrint.TabIndex = LME010C.CtlTabIndex_MAIN.CMB_PRINT
            .btnPrint.TabIndex = LME010C.CtlTabIndex_MAIN.BTN_PRINT
            .grpSeikyuSearch.TabIndex = LME010C.CtlTabIndex_MAIN.GRP_SEARCH
            .txtCustCD_L.TabIndex = LME010C.CtlTabIndex_MAIN.CUST_CD_L
            .txtCustCD_M.TabIndex = LME010C.CtlTabIndex_MAIN.CUST_CD_M
            .txtSeikyuCD.TabIndex = LME010C.CtlTabIndex_MAIN.SEIQ_CD
            .txtSagyoCD.TabIndex = LME010C.CtlTabIndex_MAIN.SAGYO_CD
            .cmbEigyo.TabIndex = LME010C.CtlTabIndex_MAIN.NRS_BR_CD
            .cmbWare.TabIndex = LME010C.CtlTabIndex_MAIN.WH_CD
            .txtSagyoSijiNO.TabIndex = LME010C.CtlTabIndex_MAIN.SAGYO_SIJI_NO
            .imdSagyoDate_S.TabIndex = LME010C.CtlTabIndex_MAIN.SAGYO_DATE_FROM
            .imdSagyoDate_E.TabIndex = LME010C.CtlTabIndex_MAIN.SAGYO_DATE_TO
            .grpCheckSearch.TabIndex = LME010C.CtlTabIndex_MAIN.GRP_CHK
            .optNotKakutei.TabIndex = LME010C.CtlTabIndex_MAIN.NOT_KAKUTEI
            .optKakuteiZumi.TabIndex = LME010C.CtlTabIndex_MAIN.KAKUTEI
            'START YANAI 20120319　作業画面改造
            .grpKanryoSearch.TabIndex = LME010C.CtlTabIndex_MAIN.GRP_KANRYO
            .optNotKanryo.TabIndex = LME010C.CtlTabIndex_MAIN.NOT_KANRYO
            .optKanryo.TabIndex = LME010C.CtlTabIndex_MAIN.KANRYO
            'END YANAI 20120319　作業画面改造
            .grpSeikyuAllChange.TabIndex = LME010C.CtlTabIndex_MAIN.GRP_CHANGE
            .cmbEditList.TabIndex = LME010C.CtlTabIndex_MAIN.SYUSEI_KOUMOKU
            .txtEditTxt.TabIndex = LME010C.CtlTabIndex_MAIN.TXT_EDIT
            .txtEditNum.TabIndex = LME010C.CtlTabIndex_MAIN.NUM_EDIT
            .cmbEditKbSkyu.TabIndex = LME010C.CtlTabIndex_MAIN.CMB_TANI
            .cmbEditDate.TabIndex = LME010C.CtlTabIndex_MAIN.CMB_DATE
            .cmbEditKbTax.TabIndex = LME010C.CtlTabIndex_MAIN.CMB_TAX
            .btnAllChange.TabIndex = LME010C.CtlTabIndex_MAIN.BTN_CHANGE
            .btnRowCopy.TabIndex = LME010C.CtlTabIndex_MAIN.BTN_COPY
            .btnRowDel.TabIndex = LME010C.CtlTabIndex_MAIN.BTN_DEL
            .sprSagyo.TabIndex = LME010C.CtlTabIndex_MAIN.SPR

        End With

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LME010F)

        '=== TODO : 初期荷主取得仕様決定後　修正になる可能性あり ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()         '（自）倉庫

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbEigyo.ReadOnly = True
        Else
            Me._Frm.cmbEigyo.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        If getDr.Length() > 0 Then
            frm.txtCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                   '（初期）荷主コード（大）")
            frm.lblCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()                   '（初期）荷主名（大）
            frm.txtCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                   '（初期）荷主コード（中）")
            frm.lblCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()                   '（初期）荷主名（中）
        End If

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LME010C.MODE_DEFAULT)

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LME010C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

    Friend Sub SetTxtEditNum(ByVal frm As LME010F, ByVal selectCmbValue As String)
        With Me._Frm

            Dim sharp10 As String = "#########0"
            Dim sagyoNb As Decimal = Convert.ToDecimal(LME010C.SAGYO_NB)
            Dim sharp9_3 As String = "###,###,##0.000"
            Dim sagyoUp As Decimal = Convert.ToDecimal(LME010C.SAGYO_UP)
            Dim sharp9 As String = "###,###,##0"
            Dim sagyoGk As Decimal = Convert.ToDecimal(LME010C.SAGYO_GK)

            Select Case selectCmbValue
                Case "04"
                    .txtEditNum.SetInputFields(sharp10, , 10, 1, , 0, 0, , sagyoNb, 0)
                Case "05"
                    .txtEditNum.SetInputFields(sharp9_3, , 9, 1, , 3, 3, , sagyoUp, 0)
                Case "07"
                    .txtEditNum.SetInputFields(sharp9, , 9, 1, , 0, 0, , sagyoGk, 0)
            End Select

        End With
    End Sub

#Region "コントロール設定"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal frm As LME010F)

        Dim selectCmbValue As String = frm.cmbEditList.SelectedValue.ToString

        frm.txtEditTxt.Visible = False
        frm.txtEditNum.Visible = False
        frm.cmbEditKbSkyu.Visible = False
        frm.cmbEditDate.Visible = False
        frm.cmbEditKbTax.Visible = False
        frm.lblEditNM.Visible = False

        Select Case selectCmbValue
            Case LME010C.EDIT_SELECT_GOODS '商品名
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 60
                    .InputType = Com.Const.InputControl.ALL_MIX
                End With

            Case LME010C.EDIT_SELECT_LOT 'ロット№
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 40
                    .InputType = Com.Const.InputControl.ALL_MIX_IME_OFF
                End With

            Case LME010C.EDIT_SELECT_SAGYONM '作業名
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 60
                    .InputType = Com.Const.InputControl.ALL_MIX
                End With

            Case LME010C.EDIT_SELECT_SAGYOSU '作業数
                With frm.txtEditNum
                    .Visible = True
                    Call Me.SetTxtEditNum(frm, selectCmbValue)
                End With

            Case LME010C.EDIT_SELECT_SEIQUP '請求単価
                With frm.txtEditNum
                    .Visible = True
                    Call Me.SetTxtEditNum(frm, selectCmbValue)
                End With

            Case LME010C.EDIT_SELECT_SEIQUT '請求単位
                With frm.cmbEditKbSkyu
                    .Visible = True
                End With

            Case LME010C.EDIT_SELECT_SEIQGK '請求金額
                With frm.txtEditNum
                    .Visible = True
                    Call Me.SetTxtEditNum(frm, selectCmbValue)
                End With

            Case LME010C.EDIT_SELECT_SEIQTO '請求先コード
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 7
                    .InputType = Com.Const.InputControl.HAN_NUM_ALPHA
                End With
                frm.lblEditNM.Visible = True

            Case LME010C.EDIT_SELECT_SAGYODATE '作業完了日
                With frm.cmbEditDate
                    .Visible = True
                End With

            Case LME010C.EDIT_SELECT_DEST '届先名
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 80
                    .InputType = Com.Const.InputControl.ALL_MIX
                End With

            Case LME010C.EDIT_SELECT_REMARK '備考
                With frm.txtEditTxt
                    .Visible = True
                    .MaxLength = 100
                    .InputType = Com.Const.InputControl.ALL_MIX
                End With

            Case LME010C.EDIT_SELECT_TAX '課税分
                With frm.cmbEditKbTax
                    .Visible = True
                    .DataCode = "Z001"
                End With

        End Select

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.DEF, " ", 20, True)                            '選択列
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.GOODS_NM, "商品名", 200, True)                 '商品名
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.LOT_NO, "ロット№", 100, True)                 'ＬＯＴ番号
        Public Shared SAGYO_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_NM, "作業名", 130, True)                 '作業名
        Public Shared SAGYOSU As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYOSU, "作業数", 70, True)                   '作業数
        Public Shared SAGYO_TANKA As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_TANKA, "請求単価", 80, True)               '請求単価
        Public Shared INV_TANI_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.INV_TANI_NM, "請求単位", 70, True)                '請求単位
        Public Shared AMT As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.AMT, "請求金額", 80, True)                     '請求金額
        Public Shared ITEM_CURR_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.ITEM_CURR_CD, "契約通貨コード", 80, True)  '契約通貨コード
        Public Shared SQTO_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SQTO_CD, "請求先コード", 100, True)             '請求先ｺｰﾄﾞ
        Public Shared SQTO_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SQTO_NM, "請求先名", 130, True)               '請求先名称
        Public Shared SAGYO_COMP_DATE As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_COMP_DATE, "作業日", 90, True)      '作業完了日
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.DEST_NM, "届先名", 160, True)                 '届先名
        Public Shared SKYU_REMARK As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SKYU_REMARK, "備考", 130, True)               '備考
        Public Shared TAX_KB_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.TAX_KB_NM, "課税区分", 70, True)                    '課税区分
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.CUST_NM, "荷主名(大)", 130, True)               '荷主名称
        Public Shared IOKA_CTL_NO As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.IOKA_CTL_NO, "管理番号", 110, True)            '管理番号
        Public Shared SAGYO_SIJI_NO As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_SIJI_NO, "作業指示書番号", 110, True)     '作業指示書番号
        Public Shared SAGYO_REC_NO As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_REC_NO, "作業レコード番号", 110, True)     '作業ﾚｺｰﾄﾞ番号
        Public Shared SAGYO_COMP_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_COMP_NM, "確認作業者名", 150, True)     '確認作業者名
        Public Shared SAGYO_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_CD, "作業コード", 100, True)               '作業ｺｰﾄﾞ
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)                '荷主大
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 90, True)                '荷主中
        Public Shared IOZS_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.IOZS_NM, "入出在その他", 105, True)                '入出在その他区分
        Public Shared UPD_DT As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.UPD_DT, "更新日", 80, True)                    '更新日
        Public Shared UPD_TM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.UPD_TM, "更新時間", 80, True)                  '更新時間
        Public Shared UPT_NM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.UPT_NM, "更新者", 100, True)                   '更新者

        'invisible
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.NRS_BR_CD, "営業所コード", 100, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.WH_CD, "倉庫コード", 70, False)
        Public Shared INV_TANI As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.INV_TANI, "請求単位区分", 70, False)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.DEST_CD, "届先コード", 60, False)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.TAX_KB, "課税区分", 60, False)
        Public Shared IOZS_KB As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.IOZS_KB, "入出在その他区分", 60, False)
        Public Shared SAGYO_COMP As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_COMP_KB, "作業完了区分", 60, False)
        Public Shared SKYU_CHK As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SKYU_CHK, "請求確認フラグ", 60, False)
        Public Shared REMARK_ZAI As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.REMARK_ZAI, "在庫用備考", 60, False)
        Public Shared SAGYO_COMP_CD As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAGYO_COMP_CD, "確認作業者コード", 60, False)
        Public Shared DEST_SAGYO_FLG As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.DEST_SAGYO_FLG, "届先作業有無フラグ", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared SYS_COPY_FLG As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.COPY_FLG, "複写フラグ", 60, False)
        Public Shared SYS_SAVE_FLG As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.SAVE_FLG, "登録済フラグ", 60, False)
        Public Shared ROW_NO As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.ROW_NO, "行番号", 60, False)
        Public Shared HAITA_UPD_TM As SpreadColProperty = New SpreadColProperty(LME010C.SprColumnIndex.HAITA_UPD_TM, "更新時間", 80, False)                  '未編集更新時間(排他用)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprSagyo.CrearSpread()

            '列数設定
            .sprSagyo.Sheets(0).ColumnCount = 43

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .sprSagyo.SetColProperty(New LME010G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ロット番号で固定)
            .sprSagyo.Sheets(0).FrozenColumnCount = sprDetailDef.SAGYO_NM.ColNo + 1

            '列設定

            .sprSagyo.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 60, False))  '検証結果_導入時要望 №62対応(2011.09.13)
            .sprSagyo.SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_HANKAKU, 40, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 60, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYOSU.ColNo, LMSpreadUtility.GetNumberCell(.sprSagyo, 0, 9999999999, True, 0, True, ","))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_TANKA.ColNo, LMSpreadUtility.GetNumberCell(.sprSagyo, 0, 9999999999.999, True, 2, True, ","))
            .sprSagyo.SetCellStyle(0, sprDetailDef.INV_TANI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprSagyo, "S027", False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.AMT.ColNo, LMSpreadUtility.GetNumberCell(.sprSagyo, 0, 9999999999, True, 0, True, ","))
            .sprSagyo.SetCellStyle(0, sprDetailDef.ITEM_CURR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 60, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SQTO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 7, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SQTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 60, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_COMP_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprSagyo, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 80, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SKYU_REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 100, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.TAX_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprSagyo, "Z001", False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 60, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.IOKA_CTL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.HAN_NUM_ALPHA, 12, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_SIJI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 10, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_REC_NO.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_COMP_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 20, False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 5, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 5, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX_IME_OFF, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.IOZS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprSagyo, "M010", False))
            .sprSagyo.SetCellStyle(0, sprDetailDef.UPD_DT.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 20, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.UPD_TM.ColNo, LMSpreadUtility.GetDateTimeCell(.sprSagyo, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.UPT_NM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 60, False))
            'invisible
            .sprSagyo.SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 3, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.INV_TANI.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.DEST_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 15, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.TAX_KB.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.IOZS_KB.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_COMP.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SKYU_CHK.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.REMARK_ZAI.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 100, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SAGYO_COMP_CD.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 5, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.DEST_SAGYO_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SYS_COPY_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.SYS_SAVE_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 2, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.ROW_NO.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 20, True))
            .sprSagyo.SetCellStyle(0, sprDetailDef.HAITA_UPD_TM.ColNo, LMSpreadUtility.GetTextCell(.sprSagyo, InputControl.ALL_MIX, 20, True))
        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LME010F)


        With frm.sprSagyo

            .Sheets(0).Cells(0, sprDetailDef.GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LOT_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.INV_TANI_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.AMT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.ITEM_CURR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SQTO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SKYU_REMARK.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.IOKA_CTL_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_SIJI_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_REC_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_COMP_NM.ColNo).Value = String.Empty

            .Sheets(0).Cells(0, sprDetailDef.SAGYO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.IOZS_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYOSU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SAGYO_TANKA.ColNo).Value = String.Empty
     
        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>

    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprSagyo

        With spr

            .SuspendLayout()

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

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                '*****表示列*****
                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYOSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                .SetCellStyle(i, sprDetailDef.INV_TANI_NM.ColNo, sLabel)
                'START WANG 2014/10/23 要望2229対応
                If dr.Item("ROUND_POS").ToString() = "2" Then
                    .SetCellStyle(i, sprDetailDef.SAGYO_TANKA.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.99, True, 2, True, ","))
                    .SetCellStyle(i, sprDetailDef.AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.99, True, 2, True, ","))
                Else
                    .SetCellStyle(i, sprDetailDef.SAGYO_TANKA.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                    .SetCellStyle(i, sprDetailDef.AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))

                End If
                'END WANG 2014/10/23 要望2229対応
                .SetCellStyle(i, sprDetailDef.ITEM_CURR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_COMP_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SKYU_REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TAX_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IOKA_CTL_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_SIJI_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_COMP_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IOZS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UPD_DT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UPD_TM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UPT_NM.ColNo, sLabel)

                'セル値設定
                '*****表示列*****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM_NRS").ToString())
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_NM.ColNo, dr.Item("SAGYO_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAGYOSU.ColNo, dr.Item("SAGYO_NB").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_TANKA.ColNo, dr.Item("SAGYO_UP").ToString())
                .SetCellValue(i, sprDetailDef.INV_TANI_NM.ColNo, dr.Item("INV_TANI_NM").ToString())
                .SetCellValue(i, sprDetailDef.AMT.ColNo, dr.Item("SAGYO_GK").ToString())
                .SetCellValue(i, sprDetailDef.ITEM_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, sprDetailDef.SQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, sprDetailDef.SQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_COMP_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SAGYO_COMP_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprDetailDef.SKYU_REMARK.ColNo, dr.Item("REMARK_SKYU").ToString())
                .SetCellValue(i, sprDetailDef.TAX_KB_NM.ColNo, dr.Item("TAX_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                'START YANAI 20120319　作業画面改造
                '.SetCellValue(i, sprDetailDef.IOKA_CTL_NO.ColNo, String.Concat(dr.Item("INOUTKA_NO_LM").ToString().Substring(0, 9), "-", dr.Item("INOUTKA_NO_LM").ToString().Substring(9, 3)))
                If String.IsNullOrEmpty(dr.Item("INOUTKA_NO_LM").ToString()) = False Then
                    .SetCellValue(i, sprDetailDef.IOKA_CTL_NO.ColNo, String.Concat(dr.Item("INOUTKA_NO_LM").ToString().Substring(0, 9), "-", dr.Item("INOUTKA_NO_LM").ToString().Substring(9, 3)))
                End If
                'END YANAI 20120319　作業画面改造
                .SetCellValue(i, sprDetailDef.SAGYO_SIJI_NO.ColNo, dr.Item("SAGYO_SIJI_NO").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_REC_NO.ColNo, dr.Item("SAGYO_REC_NO").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_COMP_NM.ColNo, dr.Item("SAGYO_COMP_NM").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_CD.ColNo, dr.Item("SAGYO_CD").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.IOZS_NM.ColNo, dr.Item("IOZS_NM").ToString())
                .SetCellValue(i, sprDetailDef.UPD_DT.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.UPD_TM.ColNo, GetColonEditTime(dr.Item("SYS_UPD_TIME").ToString()))
                .SetCellValue(i, sprDetailDef.UPT_NM.ColNo, dr.Item("SYS_UPD_USER").ToString())

                '*****隠し列*****
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, sprDetailDef.INV_TANI.ColNo, dr.Item("INV_TANI").ToString())
                .SetCellValue(i, sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, sprDetailDef.TAX_KB.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, sprDetailDef.IOZS_KB.ColNo, dr.Item("IOZS_KB").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_COMP.ColNo, dr.Item("SAGYO_COMP").ToString())
                .SetCellValue(i, sprDetailDef.SKYU_CHK.ColNo, dr.Item("SKYU_CHK").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_ZAI.ColNo, dr.Item("REMARK_ZAI").ToString())
                .SetCellValue(i, sprDetailDef.SAGYO_COMP_CD.ColNo, dr.Item("SAGYO_COMP_CD").ToString())
                .SetCellValue(i, sprDetailDef.DEST_SAGYO_FLG.ColNo, dr.Item("DEST_SAGYO_FLG").ToString())
                .SetCellValue(i, sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, sprDetailDef.SYS_COPY_FLG.ColNo, dr.Item("COPY_FLG").ToString())
                .SetCellValue(i, sprDetailDef.SYS_SAVE_FLG.ColNo, dr.Item("SAVE_FLG").ToString())
                .SetCellValue(i, sprDetailDef.ROW_NO.ColNo, dr.Item("ROW_NO").ToString())
                .SetCellValue(i, sprDetailDef.HAITA_UPD_TM.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub AddSpread(ByVal frm As LME010F, ByVal dr As DataRow)

        Dim toRow As Integer = 0
        Dim col As Integer = 0
        Dim spr As LMSpread = frm.sprSagyo

        With spr

            toRow = .ActiveSheet.Rows.Count
            col = .ActiveSheet.ColumnCount

            .ActiveSheet.AddRows(toRow, 1)

            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            'セルスタイル設定
            '*****表示列*****
            .SetCellStyle(toRow, sprDetailDef.DEF.ColNo, sDEF)
            .SetCellStyle(toRow, sprDetailDef.GOODS_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.LOT_NO.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYOSU.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
            .SetCellStyle(toRow, sprDetailDef.INV_TANI_NM.ColNo, sLabel)
            'START WANG 2014/10/23 要望2229対応
            If dr.Item("ROUND_POS").ToString() = "2" Then
                .SetCellStyle(toRow, sprDetailDef.SAGYO_TANKA.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.99, True, 2, True, ","))
                .SetCellStyle(toRow, sprDetailDef.AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.99, True, 2, True, ","))
            Else
                .SetCellStyle(toRow, sprDetailDef.SAGYO_TANKA.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))
                .SetCellStyle(toRow, sprDetailDef.AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))

            End If
            'END WANG 2014/10/23 要望2229対応
            .SetCellStyle(toRow, sprDetailDef.ITEM_CURR_CD.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SQTO_CD.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SQTO_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_COMP_DATE.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.DEST_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SKYU_REMARK.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.TAX_KB_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.CUST_NM_L.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.IOKA_CTL_NO.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_SIJI_NO.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_REC_NO.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_COMP_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.SAGYO_CD.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.IOZS_NM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.UPD_DT.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.UPD_TM.ColNo, sLabel)
            .SetCellStyle(toRow, sprDetailDef.UPT_NM.ColNo, sLabel)

            'セル値設定
            '*****表示列*****
            .SetCellValue(toRow, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(toRow, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM_NRS").ToString())
            .SetCellValue(toRow, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_NM.ColNo, dr.Item("SAGYO_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYOSU.ColNo, dr.Item("SAGYO_NB").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_TANKA.ColNo, dr.Item("SAGYO_UP").ToString())
            .SetCellValue(toRow, sprDetailDef.INV_TANI_NM.ColNo, dr.Item("INV_TANI_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.AMT.ColNo, dr.Item("SAGYO_GK").ToString())
            .SetCellValue(toRow, sprDetailDef.ITEM_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.SQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.SQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_COMP_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SAGYO_COMP_DATE").ToString()))
            .SetCellValue(toRow, sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.SKYU_REMARK.ColNo, dr.Item("REMARK_SKYU").ToString())
            .SetCellValue(toRow, sprDetailDef.TAX_KB_NM.ColNo, dr.Item("TAX_KB_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
            .SetCellValue(toRow, sprDetailDef.IOKA_CTL_NO.ColNo, String.Concat(dr.Item("INOUTKA_NO_LM").ToString().Substring(0, 9), "-", dr.Item("INOUTKA_NO_LM").ToString().Substring(9, 3)))
            .SetCellValue(toRow, sprDetailDef.SAGYO_SIJI_NO.ColNo, dr.Item("SAGYO_SIJI_NO").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_REC_NO.ColNo, dr.Item("SAGYO_REC_NO").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_COMP_NM.ColNo, dr.Item("SAGYO_COMP_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_CD.ColNo, dr.Item("SAGYO_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
            .SetCellValue(toRow, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
            .SetCellValue(toRow, sprDetailDef.IOZS_NM.ColNo, dr.Item("IOZS_NM").ToString())
            .SetCellValue(toRow, sprDetailDef.UPD_DT.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))
            .SetCellValue(toRow, sprDetailDef.UPD_TM.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            .SetCellValue(toRow, sprDetailDef.UPT_NM.ColNo, dr.Item("SYS_UPD_USER").ToString())

            '*****隠し列*****
            .SetCellValue(toRow, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.INV_TANI.ColNo, dr.Item("INV_TANI").ToString())
            .SetCellValue(toRow, sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.TAX_KB.ColNo, dr.Item("TAX_KB").ToString())
            .SetCellValue(toRow, sprDetailDef.IOZS_KB.ColNo, dr.Item("IOZS_KB").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_COMP.ColNo, dr.Item("SAGYO_COMP").ToString())
            .SetCellValue(toRow, sprDetailDef.SKYU_CHK.ColNo, dr.Item("SKYU_CHK").ToString())
            .SetCellValue(toRow, sprDetailDef.REMARK_ZAI.ColNo, dr.Item("REMARK_ZAI").ToString())
            .SetCellValue(toRow, sprDetailDef.SAGYO_COMP_CD.ColNo, dr.Item("SAGYO_COMP_CD").ToString())
            .SetCellValue(toRow, sprDetailDef.DEST_SAGYO_FLG.ColNo, dr.Item("DEST_SAGYO_FLG").ToString())
            .SetCellValue(toRow, sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
            .SetCellValue(toRow, sprDetailDef.SYS_COPY_FLG.ColNo, dr.Item("COPY_FLG").ToString())
            .SetCellValue(toRow, sprDetailDef.SYS_SAVE_FLG.ColNo, dr.Item("SAVE_FLG").ToString())
            .SetCellValue(toRow, sprDetailDef.ROW_NO.ColNo, dr.Item("ROW_NO").ToString())
            .SetCellValue(toRow, sprDetailDef.UPD_TM.ColNo, dr.Item("SYS_UPD_TIME").ToString())
        End With

    End Sub

    ''' <summary>
    ''' コロン編集した時刻を取得
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String
        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))
    End Function

#End Region 'Spread

#End Region

End Class
