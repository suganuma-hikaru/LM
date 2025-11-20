' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMH     : EDIサブシステム
'  プログラムID   : LMH010G : EDI入荷データ検索
'  作  成  者     : 
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
''' LMH010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH010F

    Friend objSprDef As Object = Nothing
    Friend sprEdiListDef As sprEdiListDefault


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH010F)

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
            .F1ButtonName = "入荷登録"
            .F2ButtonName = "実績作成"
            .F3ButtonName = "紐付け"
            .F4ButtonName = "EDI取消"
            .F5ButtonName = "取　込"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = "実績取消"
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "初期荷主変更"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = True
            .F2ButtonEnabled = True
            .F3ButtonEnabled = True
            .F4ButtonEnabled = True
            .F5ButtonEnabled = True
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = True
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = True
            .F12ButtonEnabled = always

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
            .grpSTATUS.TabIndex = LMH010C.CtlTabIndex_MAIN.GRP_SATUS
            .cmbEigyo.TabIndex = LMH010C.CtlTabIndex_MAIN.NRS_BR_CD
            .cmbWare.TabIndex = LMH010C.CtlTabIndex_MAIN.WH_CD
            .txtCustCD_L.TabIndex = LMH010C.CtlTabIndex_MAIN.CUST_CD_L
            .txtCustCD_M.TabIndex = LMH010C.CtlTabIndex_MAIN.CUST_CD_M
            .txtTantouCd.TabIndex = LMH010C.CtlTabIndex_MAIN.TANTO
            .imdEdiDateFrom.TabIndex = LMH010C.CtlTabIndex_MAIN.EDI_DATE_FROM
            .imdEdiDateTo.TabIndex = LMH010C.CtlTabIndex_MAIN.EDI_DATE_TO
            .imdInkaDateFrom.TabIndex = LMH010C.CtlTabIndex_MAIN.INKA_DATE_FROM
            .imdInkaDateTo.TabIndex = LMH010C.CtlTabIndex_MAIN.INKA_DATE_TO
            '.cmbSelectDate.TabIndex = LMH010C.CtlTabIndex_MAIN.EDI_DATE_KB
            '.imdEdiDateFrom.TabIndex = LMH010C.CtlTabIndex_MAIN.EDI_DATE
            .cmbJikkou.TabIndex = LMH010C.CtlTabIndex_MAIN.CMB_JIKKOU
            .btnJikkou.TabIndex = LMH010C.CtlTabIndex_MAIN.BTN_JIKKOU
            .cmbPrint.TabIndex = LMH010C.CtlTabIndex_MAIN.CMB_PRINT
            .btnPrint.TabIndex = LMH010C.CtlTabIndex_MAIN.BTN_PRINT
            '2012.03.13 大阪対応START
            .cmbOutput.TabIndex = LMH010C.CtlTabIndex_MAIN.CMBOUTPUT
            .txtPrt_CustCD_L.TabIndex = LMH010C.CtlTabIndex_MAIN.TXTPRTCUSTCD_L
            .txtPrt_CustCD_M.TabIndex = LMH010C.CtlTabIndex_MAIN.TXTPRTCUSTCD_M
            .cmbOutputKb.TabIndex = LMH010C.CtlTabIndex_MAIN.CMBOUTPUTKB
            .imdOutputDateFrom.TabIndex = LMH010C.CtlTabIndex_MAIN.CMBOUTPUTDATEFROM
            .imdOutputDateTo.TabIndex = LMH010C.CtlTabIndex_MAIN.CMBOUTPUTDATETO
            '2012.03.13 大阪対応END
            '.cmbRePrint.TabIndex = LMH010C.CtlTabIndex_MAIN.CMB_REPRINT
            '.btnRePrint.TabIndex = LMH010C.CtlTabIndex_MAIN.BTN_REPRINT
            .sprEdiList.TabIndex = LMH010C.CtlTabIndex_MAIN.SPR_MAIN


            'GroupBox chkSTA
            .chkStaMitouroku.TabIndex = LMH010C.CtlTabIndex_KBN.MITOUROKU
            .chkStaTourokuzumi.TabIndex = LMH010C.CtlTabIndex_KBN.INKAZUMI
            .chkStaJissekimi.TabIndex = LMH010C.CtlTabIndex_KBN.JISSEKIMI
            .chkStaJissekizumi1.TabIndex = LMH010C.CtlTabIndex_KBN.JISSEKIZUMI
            .chkStaJissekizumi2.TabIndex = LMH010C.CtlTabIndex_KBN.SOUSINZUMI
            .chkstaRedData.TabIndex = LMH010C.CtlTabIndex_KBN.AKA
            '.chkStaAll.TabIndex = LMH010C.CtlTabIndex_KBN.ALL
            .chkStaTorikesi.TabIndex = LMH010C.CtlTabIndex_KBN.DEL

            'TabStop
            .cmbRePrint.TabStop = False
            .btnRePrint.TabStop = False         '再印刷ボタン

            '.cmbChg.TabIndex = LMH010C.Chg_KBN.BULK_CUST_CHANGE
            '.btnChg.TabStop = False '変更ボタン

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMH010C.MODE_DEFAULT)

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
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMH010C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub


    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMH010F, ByVal sysDate As String)

        '=== TODO : 初期荷主取得仕様決定後　修正になる可能性あり ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()      '（自）倉庫

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
            frm.txtCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                   '（初期）荷主コード（大）")
            frm.lblCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()                   '（初期）荷主名（大）
            frm.txtCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                   '（初期）荷主コード（中）")
            frm.lblCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()                   '（初期）荷主名（中）
        End If

        frm.chkStaMitouroku.Checked = True
        'frm.cmbSelectDate.SelectedValue = "01"

        frm.imdEdiDateFrom.TextValue = sysDate
        frm.imdEdiDateTo.TextValue = sysDate
        frm.imdInkaDateFrom.TextValue = sysDate
        frm.imdInkaDateTo.TextValue = sysDate


    End Sub


    '2012.03.13 大阪対応START
#Region "コントロール設定(CSV作成・印刷コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutputControl(ByVal frm As LMH010F, ByVal sysdate As String)

        Dim selectCmbValue As String = frm.cmbOutput.SelectedValue.ToString()

        frm.imdOutputDateFrom.Visible = False
        frm.imdOutputDateTo.Visible = False
        frm.lblTitlePrintDate.Visible = False
        frm.lblTitlePrintDate.Text = "EDI取込日"
        frm.lblTitleFromTo.Visible = False
        frm.txtPrt_CustCD_L.Visible = False            '荷主コード（大）
        frm.txtPrt_CustCD_M.Visible = False            '荷主コード（中）
        frm.lblPrtTitleCust.Visible = False
        frm.cmbOutputKb.Visible = False

        '要望番号1007 2012.05.08 追加START                
        frm.cmbOutputKb.SelectedValue = "00"
        '要望番号1007 2012.05.08 追加END

        Select Case selectCmbValue

            '2012.03.18 大阪対応START
            Case LMH010C.JYUSIN_PRT _
                , LMH010C.JYUSIN_ICHIRAN            '受信帳票,受信一覧表

                frm.txtPrt_CustCD_L.Visible = True            '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True            '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.lblTitlePrintDate.Visible = True
                frm.lblTitleFromTo.Visible = True
                frm.lblPrtTitleCust.Visible = True
                frm.cmbOutputKb.Visible = True

                frm.lblTitlePrintDate.Text = "EDI取込日"
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue

                '2012.03.18 大阪対応END

                '要望番号1007 2012.05.08 追加START                
                frm.cmbOutputKb.SelectedValue = "01"
                '要望番号1007 2012.05.08 追加END

                '未着・早着ファイル作成対応 Start
            Case LMH010C.MISOUTYAKU_FILE_MAKE       '未着・早着ファイル作成

                frm.txtPrt_CustCD_L.Visible = True            '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True            '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateFrom.Enabled = False
                frm.imdOutputDateTo.Visible = False
                frm.lblTitlePrintDate.Visible = True
                frm.lblTitleFromTo.Visible = False
                frm.lblPrtTitleCust.Visible = True
                frm.cmbOutputKb.Visible = False

                frm.lblTitlePrintDate.Text = "処理日"
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                '未着・早着ファイル作成対応 End

            Case Else

        End Select

    End Sub

#End Region
    '2012.03.13 大阪対応END

    '要望番号1061 2012.05.15 追加START
#Region "コントロール設定(出力コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutputkbControl(ByVal frm As LMH010F, ByVal sysdate As String)

        'Me.SetOutputControl(frm, sysdate)

        Dim selectOutputValue As String = frm.cmbOutputKb.SelectedValue.ToString
        Dim selectCmbValue As String = frm.cmbOutput.SelectedValue.ToString

        Select Case selectOutputValue

            Case LMH010C.OUTPUT_SUMI '出力済
                frm.imdOutputDateFrom.Visible = False
                frm.imdOutputDateTo.Visible = False
                frm.lblTitlePrintDate.Visible = False
                frm.lblTitleFromTo.Visible = False

            Case Else
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.lblTitlePrintDate.Visible = True
                frm.lblTitleFromTo.Visible = True

                Select Case selectCmbValue

                    Case String.Empty
                        frm.imdOutputDateFrom.Visible = False
                        frm.imdOutputDateTo.Visible = False
                        frm.lblTitlePrintDate.Visible = False
                        frm.lblTitleFromTo.Visible = False

                End Select

        End Select

    End Sub

#End Region
    '要望番号1061 2012.05.15 追加END

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="dt">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal dt As DataTable)

        'スプレッド設定
        Call Me.SetSpread(dt)

    End Sub

#End Region


    ''' <summary>
    ''' M品出荷管理番号L列表示有無判定
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="custCdL"></param>
    ''' <param name="custCdM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsShowOutkaCtlNoLCondM(ByVal nrsBrCd As String _
                                          , ByVal custCdL As String _
                                          , ByVal custCdM As String) As Boolean

        Const KBN_GROUP_CD As String = "M027"
        Return (MyBase.GetLMCachedDataTable("Z_KBN").AsEnumerable() _
                .Where(Function(r) KBN_GROUP_CD.Equals(r.Item("KBN_GROUP_CD")) AndAlso _
                                   nrsBrCd.Equals(r.Item("KBN_NM1")) AndAlso _
                                   custCdL.Equals(r.Item("KBN_NM2")) AndAlso _
                                   custCdM.Equals(r.Item("KBN_NM3"))).Count > 0)

    End Function

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprEdiListDefault
        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.DEF, " ", 20, True)
        Public JOTAI As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.JOTAI, "状態", 40, True) 'SIZE 80⇒40
        Public HORYU As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.HORYU_KBN, "保留", 40, True) 'SIZE 80⇒40
        Public ORDER_NO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.ORDER_NO, "オーダー番号", 85, True) 'SIZE 100⇒85
        Public STATUS_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.STATUS_NM, "進捗区分", 70, True) 'SIZE 80⇒70
        Public INKA_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_DATE, "入荷日", 72, True) 'SIZE 100⇒72
        Public CUST_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.CUST_NM, "荷主名", 150, True) 'SIZE 150⇒100
        Public ITEM_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.ITEM_NM, "商品(中1)", 150, True)
        Public INKA_NB As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_NB, "入荷数", 60, True) 'SIZE 80⇒60
        Public INKA_TTL_NB As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_TTL_NB, "総個数", 75, True) 'SIZE 100⇒75
        Public MDL_REC_CNT As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.MDL_REC_CNT, "中レコ" & vbCrLf & "ード数", 50, True) 'SIZE 60⇒50
        Public UNSOMOTO_KBN As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.UNSOMOTO_KBN, "タリフ" & vbCrLf & "分類区分", 80, True)
        Public UNSO_CORP As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.UNSO_CORP, "運送会社名", 130, True) 'SIZE 150⇒130
        '2013.04.03 Notes1995 START
        Public OUTKA_MOTO_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.OUTKA_MOTO_NM, "出荷元", 130, True)
        '2013.04.03 Notes1995 END
        Public EDI_NO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_NO, "EDI" & vbCrLf & "管理番号(大)", 100, True)
        '2012.02.25 大阪対応START
        Public MATOME_NO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.MATOME_NO, "まとめ番号", 100, True)
        '2012.02.25 大阪対応END
        Public KANRI_NO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.KANRI_NO, "入荷" & vbCrLf & "管理番号(大)", 100, True)
        Public BUYER_ORDER_NO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.BUYER_ORDER_NO, "注文番号", 100, True)
        Public INKA_SHUBETSU As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_SHUBETSU, "入荷" & vbCrLf & "種別", 80, True)
        Public EDI_IMP_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_IMP_DATE, "EDI取込日", 100, True)
        Public EDI_IMP_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_IMP_TIME, "取込時刻", 100, True)
        Public EDI_SEND_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_SEND_DATE, "送信日", 100, True)
        Public EDI_SEND_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_SEND_TIME, "送信時刻", 100, True)
        Public TANTO_USER_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.TANTO_USER_NM, "担当者", 150, True)
        Public SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 150, True)
        Public SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 150, True)

        ' 依頼番号000209対応(日本合成化学用デフォルト非表示)
        Public OUTKA_CTL_NO_L_COND_M As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.OUTKA_CTL_NO_L_COND_M _
                                                                                , String.Concat("M品出荷", vbCrLf, "管理番号") _
                                                                                , 60 _
                                                                                , False)


        'invisible
        Public AKAKURO_FLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.AKAKURO_FLG, "", 20, False)
        Public DEL_KB As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.DEL_KB, "", 20, False)
        Public OUT_FLAG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.OUT_FLAG, "", 20, False)
        Public EDI_CUST_JISSEKI As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_JISSEKI, "", 20, False)
        Public EDI_CUST_MATOME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_MATOME, "", 20, False)
        Public EDI_CUST_SPECIAL As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_SPECIAL, "", 20, False)
        Public EDI_CUST_HOLDOUT As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_HOLD, "", 20, False)
        Public EDI_CUST_INDEX As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_INDEX, "", 20, False)
        Public EDI_CUST_UNSO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_UNSO, "", 20, False)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.NRS_BR_CD, "", 20, False)
        Public WH_CD As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.WH_CD, "", 20, False)
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SYS_UPD_DATE, "", 20, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SYS_UPD_TIME, "", 20, False)
        Public SND_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SND_UPD_DATE, "", 20, False)
        Public SND_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SND_UPD_TIME, "", 20, False)
        Public RCV_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_UPD_DATE, "", 20, False)
        Public RCV_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_UPD_TIME, "", 20, False)
        Public INKA_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_UPD_DATE, "", 20, False)
        Public INKA_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_UPD_TIME, "", 20, False)
        Public INKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_STATE_KB, "", 20, False)
        Public SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SYS_DEL_FLG, "", 20, False)
        Public INKA_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.INKA_DEL_FLG, "", 20, False)
        Public JISSEKI_FLAG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.JISSEKI_FLAG, "", 20, False)
        Public FREE_C30 As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.FREE_C30, "", 20, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.CUST_CD_L, "", 20, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.CUST_CD_M, "", 20, False)
        Public ORDER_CHECK_FLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.ORDER_CHECK_FLG, "", 20, False)
        '▼▼▼二次
        Public AUTO_MATOME_FLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.AUTO_MATOME_FLG, "", 20, False)
        Public RCV_NM_HED As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_NM_HED, "", 20, False)
        Public RCV_NM_DTL As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_NM_DTL, "", 20, False)
        Public RCV_NM_EXT As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_NM_EXT, "", 20, False)
        Public SND_NM As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.SND_NM, "", 20, False)
        '2012.02.25 大阪対応 START
        Public EDI_CUST_INOUTFLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.EDI_CUST_INOUTFLG, "", 20, False)
        '2012.02.25 大阪対応 END
        '2013.04.03 Notes1995 START
        Public OUTKA_MOTO As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.OUTKA_MOTO, "", 20, False)
        '2013.04.03 Notes1995 END
        '受信DTL排他用コメントアウト
        'Public Shared RCV_DTL_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_DTL_UPD_DATE, "", 20, False)
        'Public Shared RCV_DTL_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.RCV_DTL_UPD_TIME, "", 20, False)
        '▲▲▲二次
        '2015.09.03 tsunehira add
        Public CHG_CUST_CD As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.CHG_CUST_CD, "", 20, False)

        Public GENPINHYO_CHKFLG As SpreadColProperty = New SpreadColProperty(LMH010C.SprColumnIndex.GENPINHYO_CHKFLG, "", 20, False)






    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprEdiList.CrearSpread()

            '列数設定
            '受信DTL排他用コメントアウト
            '.sprEdiList.Sheets(0).ColumnCount = 60
            '.sprEdiList.Sheets(0).ColumnCount = 58                  '2012.02.25 大阪対応ADD

            .sprEdiList.Sheets(0).ColumnCount = LMH010C.SprColumnIndex.LAST                  '2013.04.03 Notes1995

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprEdiList.SetColProperty(New sprEdiListDef)
            objSprDef = New sprEdiListDefault
            .sprEdiList.SetColProperty(objSprDef, True)
            sprEdiListDef = DirectCast(objSprDef, LMH010G.sprEdiListDefault)

            '列固定位置を設定します。(ex.荷主名で固定)
            '.sprEdiList.Sheets(0).FrozenColumnCount = sprEdiListDef.CUST_NM.ColNo + 1
            .sprEdiList.Sheets(0).FrozenColumnCount = sprEdiListDef.CUST_NM.ColNo + 1

            '列設定
            .sprEdiList.SetCellStyle(0, sprEdiListDef.JOTAI.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, "S051", False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.HORYU.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, "E011", False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.STATUS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 122, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.ITEM_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.MDL_REC_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 99, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSOMOTO_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, "T015", False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_CORP.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 122, False))
            '2013.04.03 Notes1995 START
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_MOTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 80, False))
            '2013.04.03 Notes1995 END
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            '2012.02.25 大阪対応 START
            .sprEdiList.SetCellStyle(0, sprEdiListDef.MATOME_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            '2012.02.25 大阪対応 END
            .sprEdiList.SetCellStyle(0, sprEdiListDef.KANRI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.BUYER_ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.INKA_SHUBETSU.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, "N007", False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_IMP_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_IMP_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_SEND_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_SEND_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.TANTO_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))

            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_CTL_NO_L_COND_M.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMH010F)

        With frm.sprEdiList

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList
        Dim dr As DataRow
        Dim lngcnt As Integer = dt.Rows.Count()

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                If dr("INKA_DEL_FLG").ToString().Equals("1") Then
                    '入荷取消：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("SYS_DEL_FLG").ToString().Equals("1") Then
                    '削除データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf String.IsNullOrEmpty(dr("MIN_NB").ToString()) = False AndAlso Convert.ToInt64(dr("MIN_NB")) <= 0 Then
                    '中データの個数がマイナス：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("MAX_AKAKURO_KB").ToString().Equals("1") Then
                    '赤黒区分が赤データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("DEL_KB").ToString().Equals("3") Then
                    '保留データ：
                    .ActiveSheet.Rows(i).ForeColor = Color.Blue
                End If

            Next

        End With


    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprEdiList

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim dr As DataRow




            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設

                .SetCellStyle(i, sprEdiListDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprEdiListDef.JOTAI.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.HORYU.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.ORDER_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.STATUS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.ITEM_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ","))
                .SetCellStyle(i, sprEdiListDef.INKA_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ","))
                .SetCellStyle(i, sprEdiListDef.MDL_REC_CNT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999, True))
                .SetCellStyle(i, sprEdiListDef.UNSOMOTO_KBN.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.UNSO_CORP.ColNo, sLabel)
                '2013.04.03 Notes1995 START
                .SetCellStyle(i, sprEdiListDef.OUTKA_MOTO_NM.ColNo, sLabel)
                '2013.04.03 Notes1995 END
                .SetCellStyle(i, sprEdiListDef.EDI_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.MATOME_NO.ColNo, sLabel)                         '2012.02.25 大阪対応ADDまとめ番号
                .SetCellStyle(i, sprEdiListDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.BUYER_ORDER_NO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_SHUBETSU.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_IMP_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_IMP_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_SEND_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_SEND_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.TANTO_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_USER_NM.ColNo, sLabel)
                'invisible
                .SetCellStyle(i, sprEdiListDef.AKAKURO_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.DEL_KB.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_JISSEKI.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_MATOME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_SPECIAL.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_HOLDOUT.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_UNSO.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_INDEX.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SND_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SND_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_STATE_KB.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.INKA_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.JISSEKI_FLAG.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.FREE_C30.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.ORDER_CHECK_FLG.ColNo, sLabel)
                '▼▼▼二次
                .SetCellStyle(i, sprEdiListDef.AUTO_MATOME_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_NM_HED.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_NM_DTL.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_NM_EXT.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SND_NM.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_INOUTFLG.ColNo, sLabel)                     '2012.02.25 大阪対応ADD
                '2013.04.03 Notes1995 START
                .SetCellStyle(i, sprEdiListDef.OUTKA_MOTO.ColNo, sLabel)
                '2013.04.03 Notes1995 END
                '受信DTL排他用コメントアウト
                '.SetCellStyle(i, sprEdiListDef.RCV_DTL_UPD_DATE.ColNo, sLabel)
                '.SetCellStyle(i, sprEdiListDef.RCV_DTL_UPD_TIME.ColNo, sLabel)
                '▲▲▲二次
                '2015.09.03 tsunehira add
                .SetCellStyle(i, sprEdiListDef.CHG_CUST_CD.ColNo, sLabel)

                .SetCellStyle(i, sprEdiListDef.OUTKA_CTL_NO_L_COND_M.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.GENPINHYO_CHKFLG.ColNo, sLabel)      'ADD2019/12/18 009991
                'セルに値を設定
                .SetCellValue(i, sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprEdiListDef.JOTAI.ColNo, dr.Item("JYOTAI").ToString)
                .SetCellValue(i, sprEdiListDef.HORYU.ColNo, dr.Item("HORYU").ToString)
                .SetCellValue(i, sprEdiListDef.ORDER_NO.ColNo, dr.Item("OUTKA_FROM_ORD_NO").ToString)
                .SetCellValue(i, sprEdiListDef.STATUS_NM.ColNo, dr.Item("INKA_STATE_NM").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString))     '入荷日
                .SetCellValue(i, sprEdiListDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString)
                'UPD 2017/05/09 アクサルタの場合 Start
                '.SetCellValue(i, sprEdiListDef.ITEM_NM.ColNo, dr.Item("GOODS_NM").ToString)
                If dr("EDI_CUST_INDEX").ToString().Equals("36") And dr("KOSU_FLG").ToString().Equals("1") Then
                    '個数取得エラー
                    .SetCellValue(i, sprEdiListDef.ITEM_NM.ColNo, "?? " & dr.Item("GOODS_NM").ToString)

                Else
                    .SetCellValue(i, sprEdiListDef.ITEM_NM.ColNo, dr.Item("GOODS_NM").ToString)

                End If
                'UPD 2017/05/09 アクサルタの場合 End

                .SetCellValue(i, sprEdiListDef.INKA_NB.ColNo, dr.Item("NB").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_TTL_NB.ColNo, dr.Item("INKA_TTL_NB").ToString)
                .SetCellValue(i, sprEdiListDef.MDL_REC_CNT.ColNo, dr.Item("RECCNT").ToString)
                .SetCellValue(i, sprEdiListDef.UNSOMOTO_KBN.ColNo, dr.Item("UNSO_KB").ToString)
                .SetCellValue(i, sprEdiListDef.UNSO_CORP.ColNo, dr.Item("UNSOCO_NM").ToString)
                '2013.04.03 Notes1995 START
                .SetCellValue(i, sprEdiListDef.OUTKA_MOTO_NM.ColNo, dr.Item("OUTKA_MOTO_NM").ToString)
                '2013.04.03 Notes1995 END
                .SetCellValue(i, sprEdiListDef.EDI_NO.ColNo, dr.Item("EDI_CTL_NO").ToString)
                .SetCellValue(i, sprEdiListDef.MATOME_NO.ColNo, dr.Item("MATOME_NO").ToString())                                    '2012.02.25 大阪対応 ADD まとめ番号
                .SetCellValue(i, sprEdiListDef.KANRI_NO.ColNo, dr.Item("INKA_CTL_NO_L").ToString)
                .SetCellValue(i, sprEdiListDef.BUYER_ORDER_NO.ColNo, dr.Item("BUYER_ORD_NO_L").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_SHUBETSU.ColNo, dr.Item("INKA_TP_NM").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_IMP_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("CRT_DATE").ToString))       '入荷日
                .SetCellValue(i, sprEdiListDef.EDI_IMP_TIME.ColNo, dr.Item("CRT_TIME").ToString)                                    '取込時刻
                .SetCellValue(i, sprEdiListDef.EDI_SEND_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SEND_DATE").ToString))     '送信日
                .SetCellValue(i, sprEdiListDef.EDI_SEND_TIME.ColNo, dr.Item("SEND_TIME").ToString)                                  '送信時刻
                .SetCellValue(i, sprEdiListDef.TANTO_USER_NM.ColNo, dr.Item("TANTO_USER").ToString)
                .SetCellValue(i, sprEdiListDef.SYS_ENT_USER_NM.ColNo, dr.Item("CRT_USER").ToString)
                .SetCellValue(i, sprEdiListDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER").ToString)

                'invisible
                .SetCellValue(i, sprEdiListDef.AKAKURO_FLG.ColNo, dr.Item("MAX_AKAKURO_KB").ToString)
                .SetCellValue(i, sprEdiListDef.DEL_KB.ColNo, dr.Item("DEL_KB").ToString)
                .SetCellValue(i, sprEdiListDef.OUT_FLAG.ColNo, dr.Item("OUT_FLAG").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_JISSEKI.ColNo, dr.Item("EDI_CUST_JISSEKI").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_MATOME.ColNo, dr.Item("EDI_CUST_MATOME").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_SPECIAL.ColNo, dr.Item("EDI_CUST_SPECIAL").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_HOLDOUT.ColNo, dr.Item("EDI_CUST_HOLDOUT").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_UNSO.ColNo, dr.Item("EDI_CUST_UNSO").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_INDEX.ColNo, dr.Item("EDI_CUST_INDEX").ToString)
                .SetCellValue(i, sprEdiListDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString)
                .SetCellValue(i, sprEdiListDef.WH_CD.ColNo, dr.Item("NRS_WH_CD").ToString)
                .SetCellValue(i, sprEdiListDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.SND_UPD_DATE.ColNo, dr.Item("SND_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.SND_UPD_TIME.ColNo, dr.Item("SND_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.RCV_UPD_DATE.ColNo, dr.Item("RCV_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.RCV_UPD_TIME.ColNo, dr.Item("RCV_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_UPD_DATE.ColNo, dr.Item("INKA_UPD_DATE").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_UPD_TIME.ColNo, dr.Item("INKA_UPD_TIME").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_STATE_KB.ColNo, dr.Item("INKA_STATE_KB").ToString)
                .SetCellValue(i, sprEdiListDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString)
                .SetCellValue(i, sprEdiListDef.INKA_DEL_FLG.ColNo, dr.Item("INKA_DEL_FLG").ToString)
                .SetCellValue(i, sprEdiListDef.JISSEKI_FLAG.ColNo, dr.Item("JISSEKI_FLAG").ToString)
                .SetCellValue(i, sprEdiListDef.FREE_C30.ColNo, dr.Item("FREE_C30").ToString)
                .SetCellValue(i, sprEdiListDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprEdiListDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString)
                .SetCellValue(i, sprEdiListDef.ORDER_CHECK_FLG.ColNo, dr.Item("ORDER_CHECK_FLG").ToString)
                '▼▼▼二次
                .SetCellValue(i, sprEdiListDef.AUTO_MATOME_FLG.ColNo, dr.Item("AUTO_MATOME_FLG").ToString)
                .SetCellValue(i, sprEdiListDef.RCV_NM_HED.ColNo, dr.Item("RCV_NM_HED").ToString)
                .SetCellValue(i, sprEdiListDef.RCV_NM_DTL.ColNo, dr.Item("RCV_NM_DTL").ToString)
                .SetCellValue(i, sprEdiListDef.RCV_NM_EXT.ColNo, dr.Item("RCV_NM_EXT").ToString)
                .SetCellValue(i, sprEdiListDef.SND_NM.ColNo, dr.Item("SND_NM").ToString)
                .SetCellValue(i, sprEdiListDef.EDI_CUST_INOUTFLG.ColNo, dr.Item("EDI_CUST_INOUTFLG").ToString)
                '2013.04.03 Notes1995 START
                .SetCellValue(i, sprEdiListDef.OUTKA_MOTO.ColNo, dr.Item("OUTKA_MOTO").ToString)
                '2013.04.03 Notes1995 END
                '2012.02.25 大阪対応 ADD
                '受信DTL排他用コメントアウト
                '.SetCellValue(i, sprEdiListDef.RCV_DTL_UPD_DATE.ColNo, dr.Item("RCV_DTL_UPD_DATE").ToString)
                '.SetCellValue(i, sprEdiListDef.RCV_DTL_UPD_TIME.ColNo, dr.Item("RCV_DTL_UPD_TIME").ToString)
                '▲▲▲二次
                '2015.09.03 tsunehira add
                .SetCellValue(i, sprEdiListDef.CHG_CUST_CD.ColNo, dr.Item("CHG_CUST_CD").ToString)

                .SetCellValue(i _
                            , sprEdiListDef.OUTKA_CTL_NO_L_COND_M.ColNo _
                            , dr.Item("OUTKA_CTL_NO_COND_M").ToString)

                .SetCellValue(i, sprEdiListDef.GENPINHYO_CHKFLG.ColNo, dr.Item("GENPINHYO_CHKFLG").ToString)    'ADD 2019/12/18 009991
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの列の表示・非表示を設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadVisible()

        With Me._Frm

            Try
                .sprEdiList.SuspendLayout()


                ' M品振替出荷管理番号
                .sprEdiList.ActiveSheet.Columns(sprEdiListDef.OUTKA_CTL_NO_L_COND_M.ColNo).Visible _
                    = Me.IsShowOutkaCtlNoLCondM(.cmbEigyo.SelectedValue.ToString() _
                                              , .txtCustCD_L.TextValue _
                                              , .txtCustCD_M.TextValue)


            Finally
                .sprEdiList.ResumeLayout(True)
            End Try

        End With


    End Sub

#End Region 'Spread

#End Region

#End Region

End Class
