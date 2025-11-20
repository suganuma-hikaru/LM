' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMH     : EDIサブシステム
'  プログラムID   : LMH030G : EDI出荷データ検索
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMH030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH030F

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
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region ""



#End Region

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
            .F1ButtonName = "出荷登録"
            .F2ButtonName = "実績作成"
            .F3ButtonName = "紐付け"
            .F4ButtonName = "EDI取消"
            .F5ButtonName = "取　込"
            .F6ButtonName = "運送登録"
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
            .F5ButtonEnabled = True 'TODO:二次対応でTrue
            .F6ButtonEnabled = True 'TODO:二次対応でTrue        '2012.03.26 大阪対応START
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
            .grpSTATUS.TabIndex = LMH030C.CtlTabIndex_MAIN.GRPSTATUS
            .cmbEigyo.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBEIGYO
            .cmbWare.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBWARE
            .txtCustCD_L.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTCUSTCD_L
            .txtCustCD_M.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTCUSTCD_M
            .txtTantouCd.TabIndex = LMH030C.CtlTabIndex_MAIN.TANTOUCD
            .txtTodokesakiCd.TabIndex = LMH030C.CtlTabIndex_MAIN.TODOKESAKICD
            .cmbSelectDate.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBSELECTDATE
            '.imdOutkaDateFrom.TabIndex = LMH030C.CtlTabIndex_MAIN.IMDEDIDATE
            .imdEdiDateFrom.TabIndex = LMH030C.CtlTabIndex_MAIN.EDIDATEFROM
            .imdEdiDateTo.TabIndex = LMH030C.CtlTabIndex_MAIN.EDIDATETO
            .imdSearchDateFrom.TabIndex = LMH030C.CtlTabIndex_MAIN.SEARCHDATEFROM
            .imdSearchDateTo.TabIndex = LMH030C.CtlTabIndex_MAIN.SEARCHDATETO
            .pnlEdit.TabIndex = LMH030C.CtlTabIndex_MAIN.PNLEDIT
            .cmbIkkatuChangeKbn.TabIndex = LMH030C.CtlTabIndex_MAIN.IKKATUCHANGEKBN
            .txtEditMain.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTEDITMAIN
            .txtEditSub.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTEDITSUB
            .cmbEditKbn.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBEDIT
            .cmbEditKbn2.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBEDIT2
            .txtEditDestCD.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTEDITDESTCD        'ADD 2018/02/22
            .btnIkkatuChange.TabIndex = LMH030C.CtlTabIndex_MAIN.BTNIKKATUCHANGE
            .txtBarCD.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTBARCD        'ADD 2017/06/20
            .cmbOutput.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBOUTPUT
            .btnOutput.TabIndex = LMH030C.CtlTabIndex_MAIN.BTNOUTPUT
            .cmbExe.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBEXE
            .btnExe.TabIndex = LMH030C.CtlTabIndex_MAIN.BTNEXE
            .cmbPrint.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBPRINT
            .btnPrint.TabIndex = LMH030C.CtlTabIndex_MAIN.BTNPRINT

            .sprEdiList.TabIndex = LMH030C.CtlTabIndex_MAIN.SPREDILIST
            '▼▼▼要望番号:467
            .pnlOutput.TabIndex = LMH030C.CtlTabIndex_MAIN.PNLOUTPUT
            .cmbOutPutCustKb.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBOUTPUTCUST
            .txtPrt_CustCD_L.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTPRTCUSTCD_L
            .txtPrt_CustCD_M.TabIndex = LMH030C.CtlTabIndex_MAIN.TXTPRTCUSTCD_M
            .cmbOutputKb.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBOUTPUTKB
            .cmbAkakuroKb.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBAKAKUROKB
            .imdOutputDateFrom.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBOUTPUTDATEFROM
            .imdOutputDateTo.TabIndex = LMH030C.CtlTabIndex_MAIN.CMBOUTPUTDATETO
            '▲▲▲要望番号:467

            'GroupBox chkSTA
            .chkStaMitouroku.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKMITOUROKU
            .chkStaTourokuzumi.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTATOUROKUZUMI
            .chkStaJissekimi.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTAJISSEKIMI
            .chkStaJissekiSakusei.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTAJISSEKISAKUSEI
            .chkStaJissekiSousin.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTAJISSEKISOUSIN
            .chkstaRedData.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTAREDDATA
            '.chkStaAll.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTAALL
            .chkStaTorikesi.TabIndex = LMH030C.CtlTabIndex_chkSTA.CHKSTATORIKESI

            'TabStop
            .btnPrint.TabStop = False         '印刷ボタン

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMH030F, ByVal sysdate As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm, sysdate)

    End Sub

#Region "コントロール初期化(共通)"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtCustCD_M.TextValue = String.Empty
            .txtTantouCd.TextValue = String.Empty
            .txtTodokesakiCd.TextValue = String.Empty
            .cmbIkkatuChangeKbn.SelectedValue = String.Empty
            .txtEditMain.TextValue = String.Empty
            .cmbEditKbn.SelectedValue = String.Empty
            .cmbEditKbn2.SelectedValue = String.Empty
            .cmbOutput.SelectedValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
            .cmbExe.SelectedValue = String.Empty
            .lblEditNm.TextValue = String.Empty
            .txtEditDestCD.TextValue = String.Empty     'ADD 2018/02/22 一括変更　届先コード追加

        End With

    End Sub

#End Region

#Region "コントロール初期化(一括変更コンボ選択時)"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub IkkatuClearControl(ByVal frm As LMH030F)

        With frm
            .txtEditMain.TextValue = String.Empty
            .txtEditSub.TextValue = String.Empty
            .cmbEditDate.TextValue = String.Empty
            .cmbEditKbn.TextValue = String.Empty
            .cmbEditKbn2.TextValue = String.Empty
            .lblEditNm.TextValue = String.Empty
            .txtEditDestCD.TextValue = String.Empty     'ADD 2018/02/22 届先コード追加
        End With

    End Sub
#End Region

#Region "コントロール設定(一括変更コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub IkkatuSetControl(ByVal frm As LMH030F, ByVal sysdate As String) 'BP・カストロール対応 terakawa 2012.12.26

        Dim selectCmbValue As String = frm.cmbIkkatuChangeKbn.SelectedValue.ToString

        frm.txtEditMain.Visible = False
        frm.txtEditSub.Visible = False
        frm.cmbEditDate.Visible = False
        frm.cmbEditKbn.Visible = False
        frm.cmbEditKbn2.Visible = False
        frm.lblEditNm.Visible = False
        frm.txtEditDestCD.Visible = False       'ADD 2018/02/22


        Select Case selectCmbValue
            Case "01" '便区分
                With frm.cmbEditKbn
                    .Visible = True
                    .DataCode = LMKbnConst.KBN_U001
                End With

            Case "02" '運送会社コード,支店コード
                With frm.txtEditMain
                    .Visible = True
                    .MaxLength = 5
                    .InputType = Com.Const.InputControl.HAN_NUM_ALPHA
                End With

                With frm.txtEditSub
                    .Visible = True
                    .BringToFront()
                    .MaxLength = 3
                    .InputType = Com.Const.InputControl.HAN_NUM_ALPHA
                End With

                With frm.lblEditNm
                    .Visible = True
                End With

                'BP・カストロール対応 terakawa 2012.12.26 Start
            Case "03", "04" '出庫日,出荷予定日
                With frm.cmbEditDate
                    .Visible = True
                    .TextValue = sysdate
                End With

            Case "05" '納入予定日
                With frm.cmbEditDate
                    .Visible = True
                End With
                'BP・カストロール対応 terakawa 2012.12.26 End

            Case "06" '届先コード　　　'ADD 2018/02/22
                With frm.txtEditDestCD
                    .Visible = True
                    .MaxLength = 15
                    .Width = 133
                    .InputType = Com.Const.InputControl.ALL_HANKAKU
                End With

                With frm.lblEditNm
                    .Visible = True
                End With

            Case "07" 'ピック区分
                With frm.cmbEditKbn2
                    .Visible = True
                    .DataCode = LMKbnConst.KBN_P001
                End With

        End Select

    End Sub

#End Region

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMH030F, ByVal sysdate As String)

        '=== TODO : 初期荷主取得仕様決定後　修正になる可能性あり ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)
        '2014.08.04 FFEM高取対応 END

        With Me._Frm
            '初期値が存在するコントロール
            .chkStaMitouroku.Checked() = True                                             '進捗区分(未登録）
            .chkStaTourokuzumi.Checked() = False                                          '進捗区分(出荷登録済）
            .chkStaJissekimi.Checked() = False                                            '進捗区分(実績未）
            .chkStaJissekiSakusei.Checked() = False                                       '進捗区分(実績作成）
            .chkStaJissekiSousin.Checked() = False                                        '進捗区分(実績送信）
            .chkstaRedData.Checked() = False                                              '進捗区分(赤データ）
            '.chkStaAll.Checked() = False                                                  '進捗区分(全て）
            .chkStaTorikesi.Checked() = False                                             '進捗区分(取消）
            .cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
            .cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()      '（自）倉庫
            '2014.08.04 FFEM高取対応 START
            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                frm.cmbEigyo.ReadOnly = True
            Else
                frm.cmbEigyo.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END
            .imdEdiDateFrom.TextValue = sysdate
            .imdEdiDateTo.TextValue = sysdate
            .cmbSelectDate.SelectedValue = "01"
            '.imdEdiDate.TextValue = Now.ToString("yyyyMMdd")
            .imdSearchDateFrom.TextValue = sysdate
            .imdSearchDateTo.TextValue = sysdate
            .cmbEditKbn.SelectedValue = String.Empty
            .cmbEditKbn2.SelectedValue = String.Empty
            .cmbOutput.SelectedValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
            .cmbExe.SelectedValue = String.Empty


            If getDr.Length() > 0 Then
                .txtCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                   '（初期）荷主コード（大）")
                .lblCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()                   '（初期）荷主名（大）
                .txtCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                   '（初期）荷主コード（大）")
                .lblCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()                   '（初期）荷主名（大）
            End If

        End With

    End Sub

    '2013.10.08 追加START DIC SAP対応
    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHitachiControl(ByVal id As String)

        With Me._Frm
            If id.Equals("LMH030") = True Then
                .lblTitleSap.Visible = False
                .chkHitachiSap.Visible = False

            ElseIf id.Equals("LMH031") = True Then
                .lblTitleSap.Visible = True
                .chkHitachiSap.Checked = True
                .chkHitachiSap.EnableStatus = False

            End If
        End With

    End Sub
    '2013.10.08 追加END DIC SAP対応

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue

    ''' <summary>
    ''' EDI納品書抽出コンボボックス設定
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <remarks></remarks>
    Friend Sub SetCmbNohinPrt(ByVal rows As DataRow())

        Dim selectedItem As ListItem = Me._Frm.cmbNohinPRT.SelectedItem
        Dim selectedIndex As Integer = Me._Frm.cmbNohinPRT.SelectedIndex
        Dim selectedValue As Object = Me._Frm.cmbNohinPRT.SelectedValue


        Me._Frm.cmbNohinPRT.Items.Clear()
        Dim subItems As List(Of ListItem) = New List(Of ListItem)

        subItems.Add(New ListItem(New SubItem() {New SubItem("") _
                                               , New SubItem("")}))
        For Each r As DataRow In rows
            subItems.Add(New ListItem(New SubItem() {New SubItem(r.Item("KBN_NM1")) _
                                                   , New SubItem(r.Item("KBN_CD"))}))
        Next
        Me._Frm.cmbNohinPRT.Items.AddRange(subItems.ToArray())

        If (selectedItem IsNot Nothing AndAlso _
            selectedIndex < Me._Frm.cmbNohinPRT.Items.Count AndAlso _
            Me._Frm.cmbNohinPRT.Items(selectedIndex).Text.Equals(selectedItem.Text) AndAlso _
            Me._Frm.cmbNohinPRT.Items(selectedIndex).SubItems(1).Value.Equals(selectedValue)) Then

            Me._Frm.cmbNohinPRT.SelectedIndex = selectedIndex
        End If

        Me._Frm.cmbNohinPRT.Refresh()


    End Sub


#End If

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey(LMH030C.MODE_DEFAULT)
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMH030C.MODE_DEFAULT)

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
            .grpSTATUS.Focus()
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

    '▼▼▼要望番号:467
#Region "コントロール設定(CSV作成・印刷コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutputControl(ByVal frm As LMH030F, ByVal sysdate As String)

        Dim selectCmbValue As String = frm.cmbOutput.SelectedValue.ToString

        frm.cmbOutPutCustKb.Visible = False
        frm.imdOutputDateFrom.Visible = False
        frm.imdOutputDateTo.Visible = False
        frm.lblTitlePrintDate.Visible = False
        frm.lblTitlePrintDate.Text = "出荷予定日"
        frm.lblTitleFromTo.Visible = False
        '2012.03.03 大阪対応START
        frm.txtPrt_CustCD_L.Visible = False            '荷主コード（大）
        frm.txtPrt_CustCD_M.Visible = False            '荷主コード（中）
        frm.lblPrtTitleCust.Visible = False
        frm.cmbOutputKb.Visible = False
        frm.cmbAkakuroKb.Visible = False
        '2012.03.03 大阪対応END
        '2012.04.18 要望番号1005 追加START
        frm.cmbRcvSendCustkbn.Visible = False
        '2012.04.18 要望番号1005 追加END

        '要望番号1007 2012.05.08 追加START                
        frm.cmbOutputKb.SelectedValue = "00"
        '要望番号1007 2012.05.08 追加END

        frm.cmbAkakuroKb.SelectedValue = "00"

        Select Case selectCmbValue

            Case LMH030C.PRINT_CSV '出荷依頼送信データ作成
                frm.cmbOutPutCustKb.Visible = True
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.lblTitlePrintDate.Visible = True
                frm.lblTitleFromTo.Visible = True

                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate

                '2012.03.18 大阪対応START
            Case LMH030C.JYUSIN_PRT _
                , LMH030C.JYUSIN_ICHIRAN _
                , LMH030C.OUTKA_PRT _
                , LMH030C.IKKATU_PRT
                '受信帳票,受信一覧表,出荷伝票 '要望番号1007 2012.05.08 追加START

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

                '2012.04.18 要望番号1005 追加START
            Case LMH030C.RCVCONF_SEND '受信確認送信
                frm.cmbRcvSendCustkbn.Visible = True
                frm.cmbRcvSendCustkbn.BringToFront()
                '2012.04.18 要望番号1005 追加END

                '2012.09.12 要望番号1429 追加START
            Case LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST 'EDI出荷取消チェックリスト
                frm.txtPrt_CustCD_L.Visible = True            '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True            '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.lblPrtTitleCust.Visible = True
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.SelectedValue = "01"      '未出力をデフォルト値に設定 要望番号:1444 terakawa 2012.09.18
                '2012.09.12 要望番号1429 追加END

                '2012.12.17 BP納品書関係　追加　━━━START━━━
            Case LMH030C.NOHIN_OKURIJO _
                , LMH030C.NOHINSYO_AUTO_BAKKUSU _
                , LMH030C.NOHIN_TACTI _
                , LMH030C.NOHIN_YELLOW_HAT _
                , LMH030C.NOHIN_OKURIJO_AUTO _
                , LMH030C.INVOICE_NIPPON_EXPRESS_BP  ' BP納品送状,BP納品書　オートバックス(埼玉),BP納品書　タクティー(埼玉) ,BP納品書　イエローハット(埼玉)  
                '(2013.02.05)要望番号1822 -- START --
                'frm.txtPrt_CustCD_L.Visible = True            '荷主コード（大）
                'frm.txtPrt_CustCD_M.Visible = True            '荷主コード（中）
                frm.txtPrt_CustCD_L.Visible = False
                frm.txtPrt_CustCD_M.Visible = False
                '(2013.02.05)要望番号1822 --  END  --

                frm.txtPrt_CustCD_M.BringToFront()

                '(2013.02.05)要望番号1822 -- START --
                'frm.lblPrtTitleCust.Visible = True
                'frm.imdOutputDateFrom.Visible = True
                'frm.imdOutputDateTo.Visible = True
                'frm.imdOutputDateFrom.TextValue = sysdate
                'frm.imdOutputDateTo.TextValue = sysdate
                frm.lblPrtTitleCust.Visible = False
                frm.imdOutputDateFrom.Visible = False
                frm.imdOutputDateTo.Visible = False
                frm.imdOutputDateFrom.TextValue = String.Empty
                frm.imdOutputDateTo.TextValue = String.Empty
                '(2013.02.05)要望番号1822 --  END  --

                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue

                '(2013.02.05)要望番号1822 -- START --
                'frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.Visible = False
                '(2013.02.05)要望番号1822 -- END  --

                frm.cmbOutputKb.SelectedValue = "01"      '未出力をデフォルト値に設定
                '2012.12.17 BP納品書関係　追加　━━━　END　━━━

                '(2012.12.17)大阪 日興産業対応 -- START --
            Case LMH030C.NOHIN_NIKKO, LMH030C.NIHUDA_TOR
                frm.txtPrt_CustCD_L.Visible = True          '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True          '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.lblPrtTitleCust.Visible = True
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.SelectedValue = "01"        '未出力をデフォルト値に設定
                '(2012.12.17)大阪 日興産業対応 --  END  --

                '(2012.12.17)千葉 ロンザ対応 -- START --
            Case LMH030C.NOHIN_RONZA
                frm.txtPrt_CustCD_L.Visible = True          '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True          '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.lblPrtTitleCust.Visible = True
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.SelectedValue = "01"        '未出力をデフォルト値に設定
                '(2012.12.17)千葉 ロンザ対応 --  END  --

            Case LMH030C.SHIKIRI_TERUMO
                frm.txtPrt_CustCD_L.Visible = True          '荷主コード（大）
                frm.txtPrt_CustCD_M.Visible = True          '荷主コード（中）
                frm.txtPrt_CustCD_M.BringToFront()
                frm.lblPrtTitleCust.Visible = True
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.imdOutputDateFrom.TextValue = sysdate
                frm.imdOutputDateTo.TextValue = sysdate
                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.SelectedValue = "01"        '未出力をデフォルト値に設定
                frm.cmbAkakuroKb.SelectedValue = "02"

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
            Case LMH030C.NOHIN_NICHIGO

                frm.txtPrt_CustCD_L.Visible = False
                frm.txtPrt_CustCD_M.Visible = False
                frm.txtPrt_CustCD_M.BringToFront()
                frm.lblPrtTitleCust.Visible = False
                frm.imdOutputDateFrom.Visible = False
                frm.imdOutputDateTo.Visible = False
                frm.imdOutputDateFrom.TextValue = String.Empty
                frm.imdOutputDateTo.TextValue = String.Empty

                frm.txtPrt_CustCD_L.TextValue = frm.txtCustCD_L.TextValue
                frm.txtPrt_CustCD_M.TextValue = frm.txtCustCD_M.TextValue
                frm.cmbOutputKb.Visible = True
                frm.cmbOutputKb.SelectedValue = "01"
#End If

            Case Else


        End Select

    End Sub

#End Region
    '▲▲▲要望番号:467

    '要望番号1061 2012.05.15 追加START
#Region "コントロール設定(出力コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetOutputkbControl(ByVal frm As LMH030F, ByVal sysdate As String)

        'Me.SetOutputControl(frm, sysdate)

        Dim selectOutputValue As String = frm.cmbOutputKb.SelectedValue.ToString
        Dim selectCmbValue As String = frm.cmbOutput.SelectedValue.ToString

        Select Case selectOutputValue

            Case LMH030C.OUTPUT_SUMI '出力済
                frm.imdOutputDateFrom.Visible = False
                frm.imdOutputDateTo.Visible = False
                frm.lblTitlePrintDate.Visible = False
                frm.lblTitleFromTo.Visible = False
                frm.cmbAkakuroKb.Visible = False

            Case Else
                frm.imdOutputDateFrom.Visible = True
                frm.imdOutputDateTo.Visible = True
                frm.lblTitlePrintDate.Visible = True
                frm.lblTitleFromTo.Visible = True

                If selectCmbValue = LMH030C.SHIKIRI_TERUMO Then
                    'テルモのみ赤黒区分を表示
                    frm.cmbAkakuroKb.Visible = True
                End If

                Select Case selectCmbValue

                    Case String.Empty
                        frm.imdOutputDateFrom.Visible = False
                        frm.imdOutputDateTo.Visible = False
                        frm.lblTitlePrintDate.Visible = False
                        frm.lblTitleFromTo.Visible = False
                        frm.cmbAkakuroKb.Visible = False

                End Select

        End Select

        '(2013.02.05)要望番号1822 BP納品系は条件不要 -- START --
        Select Case selectCmbValue
            Case LMH030C.NOHIN_OKURIJO _
               , LMH030C.NOHINSYO_AUTO_BAKKUSU _
               , LMH030C.NOHIN_TACTI _
               , LMH030C.NOHIN_YELLOW_HAT _
               , LMH030C.NOHIN_OKURIJO_AUTO _
               , LMH030C.INVOICE_NIPPON_EXPRESS_BP _
               , LMH030C.NOHIN_NICHIGO

                frm.imdOutputDateFrom.Visible = False
                frm.imdOutputDateTo.Visible = False
                frm.lblTitlePrintDate.Visible = False
                frm.lblTitleFromTo.Visible = False
                frm.txtPrt_CustCD_L.Visible = False
                frm.txtPrt_CustCD_M.Visible = False
                frm.lblPrtTitleCust.Visible = False

            Case Else

        End Select
        '(2013.02.05)要望番号1822 BP納品系は条件不要 --  END  --

    End Sub

#End Region
    '要望番号1061 2012.05.15 追加END

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataTable)

        'スプレッドに明細データ設定
        Call Me.SetSpread(ds)

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprEdiListDef
        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS_KBN As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.STATUS_KBN, "状態", 40, True) 'SIZE 80⇒40
        Public Shared HORYU_KBN As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.HORYU_KBN, "保留", 40, True)    'SIZE 80⇒40
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.ORDER_NO, "オーダー番号", 100, True) 'SIZE 100⇒80
        Public Shared STATUS_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.STATUS_NM, "進捗区分", 70, True) 'SIZE 80⇒70
        Public Shared OUTKO_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKO_DATE, "出庫日", 70, True) 'SIZE 100⇒70
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷予定日", 72, True) 'SIZE 100⇒72
        Public Shared ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.ARR_PLAN_DATE, "納入予定日", 72, True) 'SIZE 100⇒72
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.CUST_NM, "荷主名", 150, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.DEST_NM, "届先名", 150, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.REMARK, "出荷時注意", 135, True) 'SIZE 150⇒135
        Public Shared UNSO_ATT As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_ATT, "配送時注意", 135, True) 'SIZE 150⇒135
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.GOODS_NM, "商品(中1)", 145, True) 'SIZE 150⇒145
        Public Shared OUTKA_CNT As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_CNT, "出荷数", 45, True) 'SIZE 80⇒45
        Public Shared DEST_AD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.DEST_AD, "届先住所", 150, True)
        Public Shared UNSO_CORP As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_CORP, "運送会社名", 120, True) 'SIZE 150⇒120
        Public Shared BIN As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.BIN, "便区分", 80, True) 'SIZE 100⇒80
        Public Shared MDL_REC_CNT As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.MDL_REC_CNT, "中レコ" & vbCrLf & "ード数", 60, True)
        Public Shared EDI_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_NO, "EDI" & vbCrLf & "管理番号(大)", 100, True)
        Public Shared MATOME_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.MATOME_NO, "まとめ番号", 100, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.KANRI_NO, "出荷" & vbCrLf & "管理番号(大)", 100, True)
        Public Shared BUYER_ORDER_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.BUYER_ORDER_NO, "注文番号", 100, True)
        Public Shared OUTKA_SHUBETSU As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_SHUBETSU, "出荷" & vbCrLf & "種別", 80, True)
        Public Shared OUTKA_SU As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_SU, "出荷" & vbCrLf & "個数", 100, True)
        Public Shared PICK_KB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.PICK_KB, "ピック区分", 135, True)
        Public Shared UNSOMOTO_KBN As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSOMOTO_KBN, "タリフ" & vbCrLf & "分類区分", 80, True)
        '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
        Public Shared PRTFLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.PRTFLG, "受信伝票" & vbCrLf & "印刷", 80, True)
        '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
        Public Shared EDI_IMP_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_IMP_DATE, "EDI取込日", 100, True)
        Public Shared EDI_IMP_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_IMP_TIME, "取込時刻", 100, True)
        '2013.03.01 / Notes1909 受信ファイル名追加 開始
        Public Shared EDI_FILE_NAME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_FILE_NAME, "受信ファイル名", 250, True)
        '2013.03.01 / Notes1909 受信ファイル名追加 終了
        Public Shared EDI_SEND_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_SEND_DATE, "送信日", 100, True)
        Public Shared EDI_SEND_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_SEND_TIME, "送信時刻", 100, True)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.NRS_BR_NM, "営業所名", 150, True)
        Public Shared NRS_WH_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.NRS_WH_NM, "倉庫名", 150, True)
        Public Shared TANTO_USER_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.TANTO_USER_NM, "担当者", 150, True)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 150, True)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 150, True)

        'invisible
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SYS_UPD_DATE, "更新日", 86, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 86, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.NRS_BR_CD, "営業所コード", 86, False)
        Public Shared NRS_WH_CD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.NRS_WH_CD, "倉庫コード", 86, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.CUST_CD_L, "荷主コード（大）", 86, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 86, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.GOODS_CD_NRS, "商品コード", 86, False)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.DEST_CD, "届先コード", 86, False)
        Public Shared OUTKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_STATE_KB, "進捗区分", 86, False)
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_CD, "運送会社コード", 86, False)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_BR_CD, "運送会社支店コード", 86, False)
        '2012.03.25 修正START 運送番号の表示
        Public Shared UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_NO_L, "運送番号", 86, True)
        '2012.03.25 修正END 運送番号の表示
        '2012.11.11 修正START 運行番号の表示
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.TRIP_NO, "運行番号", 86, True)
        '2012.11.11 修正END 運行番号の表示
        Public Shared UNSO_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_SYS_UPD_DATE, "更新日(運送L)", 86, False)
        Public Shared UNSO_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_SYS_UPD_TIME, "更新時刻(運送L)", 86, False)
        Public Shared MIN_NB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.MIN_NB, "最小個数", 86, False)
        Public Shared EDI_DEL_KB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_DEL_KB, "EDI削除区分", 86, False)
        Public Shared OUTKA_DEL_KB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_DEL_KB, "出荷削除区分", 86, False)
        Public Shared UNSO_DEL_KB As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSO_DEL_KB, "運送削除区分", 86, False)
        Public Shared FREE_C01 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C01, "文字列01", 86, False)
        Public Shared FREE_C02 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C02, "文字列02", 86, False)
        Public Shared FREE_C03 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C03, "文字列03", 86, False)
        Public Shared FREE_C04 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C04, "文字列04", 86, False)
        Public Shared FREE_C30 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C30, "文字列30", 86, False)
        Public Shared AKAKURO_FLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.AKAKURO_FLG, "赤黒フラグ", 86, False)
        Public Shared EDI_CUST_JISSEKI As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_JISSEKI, "実績出力対象フラグ", 86, False)
        Public Shared EDI_CUST_MATOMEF As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_MATOMEF, "まとめ登録フラグ", 86, False)
        Public Shared EDI_CUST_DELDISP As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_DELDISP, "EDI取消データ表示フラグ", 86, False)
        Public Shared EDI_CUST_SPECIAL As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_SPECIAL, "EDI特殊処理フラグ", 86, False)
        Public Shared EDI_CUST_HOLDOUT As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_HOLDOUT, "保留データ出力フラグ", 86, False)
        Public Shared EDI_CUST_UNSOFLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_UNSOFLG, "EDIデータ出力先フラグ", 86, False)
        Public Shared EDI_CUST_INDEX As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_INDEX, "EDI対象荷主INDEX", 86, False)
        Public Shared SND_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SND_SYS_UPD_DATE, "更新日(EDI送信TBL)", 86, False)
        Public Shared SND_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SND_SYS_UPD_TIME, "更新時刻(EDI送信TBL)", 86, False)
        Public Shared RCV_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.RCV_SYS_UPD_DATE, "更新日(EDI受信TBL)", 86, False)
        Public Shared RCV_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.RCV_SYS_UPD_TIME, "更新時刻(EDI受信TBL)", 86, False)
        Public Shared OUTKA_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_SYS_UPD_DATE, "更新日(出荷(大))", 86, False)
        Public Shared OUTKA_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUTKA_SYS_UPD_TIME, "更新時刻(出荷(大))", 86, False)
        Public Shared JISSEKI_FLAG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.JISSEKI_FLAG, "実績書込フラグ", 86, False)
        Public Shared OUT_FLAG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.OUT_FLAG, "出荷データ書込フラグ", 86, False)
        Public Shared AUTO_MATOME_FLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.AUTO_MATOME_FLG, "自動まとめフラグ", 86, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SYS_DEL_FLG, "EDI削除フラグ", 86, False)
        Public Shared ORDER_CHECK_FLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.ORDER_CHECK_FLG, "オーダー番号重複チェックフラグ", 20, False)
        '▼▼▼二次
        Public Shared RCV_NM_HED As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.RCV_NM_HED, "", 20, False)
        Public Shared RCV_NM_DTL As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.RCV_NM_DTL, "", 20, False)
        Public Shared RCV_NM_EXT As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.RCV_NM_EXT, "", 20, False)
        Public Shared SND_NM As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.SND_NM, "", 20, False)
        '2012.02.25 大阪対応 START
        Public Shared EDI_CUST_INOUTFLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.EDI_CUST_INOUTFLG, "EDI荷主テーブル入出荷フラグ", 20, False)
        '2012.02.25 大阪対応 END
        '▲▲▲二次

        '▼▼▼ セミ標準対応 --ST--
        Public Shared FLAG_17 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FLAG_17, "フラグ17", 20, False)
        '▲▲▲ セミ標準対応 --ED--

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
        Public Shared FREE_C05 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C05, "文字列05", 86, False)
#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
        Public Shared FREE_C07 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C07, "文字列07", 86, False)
        Public Shared FREE_C08 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FREE_C08, "文字列08", 86, False)
#End If

        Public Shared HAISO_SIJI_NO As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.HAISO_SIJI_NO, "配送指示No.", 80, False)     'ADD 2017/06/16
        Public Shared FLAG_19 As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.FLAG_19, "フラグ19", 20, False)
        Public Shared UNSOEDI_EXISTS_FLAG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.UNSOEDI_EXISTS_FLAG, "運送データフラグ", 20, False)
        Public Shared NCGO_OPEOUT_ONLY_FLG As SpreadColProperty = New SpreadColProperty(LMH030C.SprColumnIndex.NCGO_OPEOUT_ONLY_FLG, "日合出荷データのみフラグ", 20, False)     'Add 2018/10/31 要望番号002808
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
            '▼▼▼二次
            '.sprEdiList.Sheets(0).ColumnCount = 76
            '2012.02.25 大阪対応 START
            '.sprEdiList.Sheets(0).ColumnCount = 81
            '2012.02.25 大阪対応 END
            '2012.11.11 センコー対応 START
            '.sprEdiList.Sheets(0).ColumnCount = 82
            '2012.11.11 センコー対応 END
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
            .sprEdiList.Sheets(0).ColumnCount = LMH030C.SprColumnIndex.LAST
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
            '▲▲▲二次

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprEdiList.SetColProperty(New sprEdiListDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            '2012.04.09 要望番号923 固定列の変更 START
            '.sprEdiList.Sheets(0).FrozenColumnCount = sprEdiListDef.CUST_NM.ColNo + 1
            .sprEdiList.Sheets(0).FrozenColumnCount = sprEdiListDef.ARR_PLAN_DATE.ColNo + 1
            '2012.04.09 要望番号923 固定列の変更 END

            '列設定
            .sprEdiList.SetCellStyle(0, sprEdiListDef.HORYU_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_E011, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.STATUS_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S051, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 30, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.STATUS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKO_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_PLAN_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.ARR_PLAN_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 122, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 80, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_ATT.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 60, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DEST_AD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 124, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_CORP.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 122, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.BIN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_U001, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.MDL_REC_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.MATOME_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.KANRI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.BUYER_ORDER_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 30, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_SHUBETSU.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S020, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_SU.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.PICK_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_P001, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSOMOTO_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_T015, False))
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
            .sprEdiList.SetCellStyle(0, sprEdiListDef.PRTFLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 1, True))
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_IMP_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_IMP_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            '2013.03.01 / Notes1909 受信ファイル名追加 開始
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_FILE_NAME.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 300, False))
            '2013.03.01 / Notes1909 受信ファイル名追加 終了
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_SEND_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_SEND_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 120, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NRS_WH_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 120, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.TANTO_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 20, False))
            'invisible
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NRS_WH_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 3, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 2, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 20, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.DEST_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX_IME_OFF, 15, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_STATE_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S010, False))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 5, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 3, True))
            '2012.03.25 修正START 運送番号の表示
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 9, False))
            '2012.03.25 修正END 運送番号の表示
            '2012.11.11 修正START 運行番号の表示
            .sprEdiList.SetCellStyle(0, sprEdiListDef.TRIP_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 10, False))
            '2012.11.11 修正END 運行番号の表示
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.MIN_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_DEL_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S011, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_DEL_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S012, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSO_DEL_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprEdiList, LMKbnConst.KBN_S012, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C01.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C02.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C03.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C04.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C30.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.AKAKURO_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_JISSEKI.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_MATOMEF.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_DELDISP.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_SPECIAL.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_HOLDOUT.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_UNSOFLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.EDI_CUST_INDEX.ColNo, LMSpreadUtility.GetNumberCell(.sprEdiList, 0, 999999, True, 3))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SND_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SND_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.RCV_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.RCV_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetDateTimeCell(.sprEdiList, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.JISSEKI_FLAG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.OUT_FLAG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.AUTO_MATOME_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.ORDER_CHECK_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C05.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C07.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FREE_C08.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.ALL_MIX, 100, True))
#End If

            'ADD 2017/06/16
            .sprEdiList.SetCellStyle(0, sprEdiListDef.HAISO_SIJI_NO.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 20, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.FLAG_19.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.UNSOEDI_EXISTS_FLAG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))
            .sprEdiList.SetCellStyle(0, sprEdiListDef.NCGO_OPEOUT_ONLY_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprEdiList, InputControl.HAN_NUM_ALPHA, 1, True))    'Add 2018/10/31 要望番号002808
        End With

    End Sub

    '要望番号1991 2013.04.02 追加START
    ''' <summary>
    ''' スプレッドの列の表示・非表示を設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadVisible()

        Dim visibleFlg As Boolean = True
        Dim bp_muke As String = "01"        'BP向け     '埼玉BP用対応

        With Me._Frm.sprEdiList

            .SuspendLayout()

            .ActiveSheet.Columns(LMH030G.sprEdiListDef.DEF.ColNo).Visible = visibleFlg               'DEF
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.STATUS_KBN.ColNo).Visible = visibleFlg        '状態
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.BIN.ColNo).Visible = visibleFlg               '便区分
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.ORDER_NO.ColNo).Visible = visibleFlg          'オーダー番号
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.OUTKA_PLAN_DATE.ColNo).Visible = visibleFlg   '出荷予定日
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.ARR_PLAN_DATE.ColNo).Visible = visibleFlg     '納入予定日
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.DEST_NM.ColNo).Visible = visibleFlg           '届先名
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.DEST_AD.ColNo).Visible = visibleFlg           '届先住所
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.UNSO_ATT.ColNo).Visible = visibleFlg          '出荷時注意
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.UNSO_CORP.ColNo).Visible = visibleFlg         '運送会社名
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.GOODS_NM.ColNo).Visible = visibleFlg          '商品（中1）
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.OUTKA_CNT.ColNo).Visible = visibleFlg         '出荷数
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.STATUS_NM.ColNo).Visible = visibleFlg         '進捗区分

            '項目表示が"BP用"の場合、表示しない。
            If (bp_muke).Equals(Me._Frm.cmbVisibleKb.SelectedValue) = True Then
                'BP用
                visibleFlg = False
            End If

            .ActiveSheet.Columns(LMH030G.sprEdiListDef.HORYU_KBN.ColNo).Visible = visibleFlg         '保留
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.OUTKO_DATE.ColNo).Visible = visibleFlg        '出庫日
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.CUST_NM.ColNo).Visible = visibleFlg           '荷主名
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.UNSO_ATT.ColNo).Visible = visibleFlg          '配送時注意
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.MDL_REC_CNT.ColNo).Visible = visibleFlg       '中レコード数
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.MATOME_NO.ColNo).Visible = visibleFlg         'まとめ番号
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.BUYER_ORDER_NO.ColNo).Visible = visibleFlg    '注文番号
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.OUTKA_SHUBETSU.ColNo).Visible = visibleFlg    '出荷種別
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.OUTKA_SU.ColNo).Visible = visibleFlg          '出荷個数
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.PICK_KB.ColNo).Visible = visibleFlg           'ピック区分
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.UNSOMOTO_KBN.ColNo).Visible = visibleFlg      'タリフ分類区分
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.NRS_BR_NM.ColNo).Visible = visibleFlg         '営業所名
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.NRS_WH_NM.ColNo).Visible = visibleFlg         '倉庫名
            .ActiveSheet.Columns(LMH030G.sprEdiListDef.TANTO_USER_NM.ColNo).Visible = visibleFlg     '担当者

            .ResumeLayout(True)

        End With

    End Sub
    '要望番号1991 2013.04.02 追加END

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMH030F)

        With frm.sprEdiList

            .Sheets(0).Cells(0, sprEdiListDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.ORDER_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.REMARK.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.UNSO_ATT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.DEST_AD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.UNSO_CORP.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.BIN.ColNo).Value = LMConst.FLG.OFF
            .Sheets(0).Cells(0, sprEdiListDef.EDI_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.MATOME_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.KANRI_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.BUYER_ORDER_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.OUTKA_SHUBETSU.ColNo).Value = LMConst.FLG.OFF
            .Sheets(0).Cells(0, sprEdiListDef.PICK_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.UNSOMOTO_KBN.ColNo).Value = String.Empty
            '2013.03.01 / Notes1909 受信ファイル追加 開始
            .Sheets(0).Cells(0, sprEdiListDef.EDI_FILE_NAME.ColNo).Value = String.Empty
            '2013.03.01 / Notes1909 受信ファイル追加 終了
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
            .Sheets(0).Cells(0, sprEdiListDef.PRTFLG.ColNo).Value = String.Empty
            '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
            .Sheets(0).Cells(0, sprEdiListDef.TANTO_USER_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprEdiListDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty

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

            '----データ挿入----'

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
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                ''*****表示列*****
                .SetCellStyle(i, sprEdiListDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprEdiListDef.STATUS_KBN.ColNo, sLabel)                        '状態
                .SetCellStyle(i, sprEdiListDef.HORYU_KBN.ColNo, sLabel)                         '保留
                .SetCellStyle(i, sprEdiListDef.ORDER_NO.ColNo, sLabel)                          'オーダー番号
                .SetCellStyle(i, sprEdiListDef.STATUS_NM.ColNo, sLabel)                         '進捗区分
                .SetCellStyle(i, sprEdiListDef.OUTKO_DATE.ColNo, sLabel)                        '出庫日
                .SetCellStyle(i, sprEdiListDef.OUTKA_PLAN_DATE.ColNo, sLabel)                   '出荷予定日
                .SetCellStyle(i, sprEdiListDef.ARR_PLAN_DATE.ColNo, sLabel)                     '納入予定日
                .SetCellStyle(i, sprEdiListDef.CUST_NM.ColNo, sLabel)                           '荷主名
                .SetCellStyle(i, sprEdiListDef.DEST_NM.ColNo, sLabel)                           '届先名
                .SetCellStyle(i, sprEdiListDef.REMARK.ColNo, sLabel)                            '出荷時注意
                .SetCellStyle(i, sprEdiListDef.UNSO_ATT.ColNo, sLabel)                          '配送時注意
                .SetCellStyle(i, sprEdiListDef.GOODS_NM.ColNo, sLabel)                          '商品(中1)
                .SetCellStyle(i, sprEdiListDef.OUTKA_CNT.ColNo, sNum)                           '出荷数
                .SetCellStyle(i, sprEdiListDef.DEST_AD.ColNo, sLabel)                           '届先住所
                .SetCellStyle(i, sprEdiListDef.UNSO_CORP.ColNo, sLabel)                         '運送会社名
                .SetCellStyle(i, sprEdiListDef.BIN.ColNo, sLabel)                               '便区分
                .SetCellStyle(i, sprEdiListDef.MDL_REC_CNT.ColNo, sNum)                         '中レコード数
                .SetCellStyle(i, sprEdiListDef.EDI_NO.ColNo, sLabel)                            'EDI管理番号(大)
                .SetCellStyle(i, sprEdiListDef.MATOME_NO.ColNo, sLabel)                         'まとめ番号
                .SetCellStyle(i, sprEdiListDef.KANRI_NO.ColNo, sLabel)                          '出荷管理番号(大)
                .SetCellStyle(i, sprEdiListDef.BUYER_ORDER_NO.ColNo, sLabel)                    '注文番号
                .SetCellStyle(i, sprEdiListDef.OUTKA_SHUBETSU.ColNo, sLabel)                    '出荷種別
                .SetCellStyle(i, sprEdiListDef.OUTKA_SU.ColNo, sNum)                            '出荷個数
                .SetCellStyle(i, sprEdiListDef.PICK_KB.ColNo, sLabel)                           'ピック区分
                .SetCellStyle(i, sprEdiListDef.UNSOMOTO_KBN.ColNo, sLabel)                      '運送手配区分
                '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
                .SetCellStyle(i, sprEdiListDef.PRTFLG.ColNo, sLabel)                            '受信伝票印刷
                '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
                .SetCellStyle(i, sprEdiListDef.EDI_IMP_DATE.ColNo, sLabel)                      'EDI取込日
                .SetCellStyle(i, sprEdiListDef.EDI_IMP_TIME.ColNo, sLabel)                      '取込時刻
                '2013.03.01 / Notes1909 受信ファイル追加 開始
                .SetCellStyle(i, sprEdiListDef.EDI_FILE_NAME.ColNo, sLabel)                     '受信ファイル名
                '2013.03.01 / Notes1909 受信ファイル追加 終了
                .SetCellStyle(i, sprEdiListDef.EDI_SEND_DATE.ColNo, sLabel)                     '送信日
                .SetCellStyle(i, sprEdiListDef.EDI_SEND_TIME.ColNo, sLabel)                     '送信時刻

                ''*****隠し列*****
                .SetCellStyle(i, sprEdiListDef.NRS_BR_NM.ColNo, sLabel)                         '営業所名
                .SetCellStyle(i, sprEdiListDef.NRS_WH_NM.ColNo, sLabel)                         '倉庫名
                .SetCellStyle(i, sprEdiListDef.TANTO_USER_NM.ColNo, sLabel)                     '担当者名
                .SetCellStyle(i, sprEdiListDef.SYS_ENT_USER_NM.ColNo, sLabel)                   '作成者
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_USER_NM.ColNo, sLabel)                   '更新者
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_DATE.ColNo, sLabel)                      '更新日
                .SetCellStyle(i, sprEdiListDef.SYS_UPD_TIME.ColNo, sLabel)                      '更新時刻
                .SetCellStyle(i, sprEdiListDef.NRS_BR_CD.ColNo, sLabel)                         '営業所コード
                .SetCellStyle(i, sprEdiListDef.NRS_WH_CD.ColNo, sLabel)                         '倉庫コード
                .SetCellStyle(i, sprEdiListDef.CUST_CD_L.ColNo, sLabel)                         '荷主コード（大）
                .SetCellStyle(i, sprEdiListDef.CUST_CD_M.ColNo, sLabel)                         '荷主コード（中）
                .SetCellStyle(i, sprEdiListDef.GOODS_CD_NRS.ColNo, sLabel)                      '商品コード
                .SetCellStyle(i, sprEdiListDef.DEST_CD.ColNo, sLabel)                           '届先コード
                .SetCellStyle(i, sprEdiListDef.OUTKA_STATE_KB.ColNo, sLabel)                    '進捗区分
                .SetCellStyle(i, sprEdiListDef.UNSO_CD.ColNo, sLabel)                           '運送会社コード
                .SetCellStyle(i, sprEdiListDef.UNSO_BR_CD.ColNo, sLabel)                        '運送会社支店コード
                .SetCellStyle(i, sprEdiListDef.UNSO_NO_L.ColNo, sLabel)                         '運送番号
                '2012.11.11 センコー対応 追加START
                .SetCellStyle(i, sprEdiListDef.TRIP_NO.ColNo, sLabel)                           '運行番号
                '2012.11.11 センコー対応 追加END
                .SetCellStyle(i, sprEdiListDef.UNSO_SYS_UPD_DATE.ColNo, sLabel)                 '更新日(運送L)
                .SetCellStyle(i, sprEdiListDef.UNSO_SYS_UPD_TIME.ColNo, sLabel)                 '更新時刻(運送L)
                .SetCellStyle(i, sprEdiListDef.MIN_NB.ColNo, sNum)                              '最小個数
                .SetCellStyle(i, sprEdiListDef.EDI_DEL_KB.ColNo, sLabel)                        'EDI削除区分
                .SetCellStyle(i, sprEdiListDef.OUTKA_DEL_KB.ColNo, sLabel)                      '出荷削除区分
                .SetCellStyle(i, sprEdiListDef.UNSO_DEL_KB.ColNo, sLabel)                       '運送削除区分
                .SetCellStyle(i, sprEdiListDef.FREE_C01.ColNo, sLabel)                          '文字列01
                .SetCellStyle(i, sprEdiListDef.FREE_C02.ColNo, sLabel)                          '文字列02
                .SetCellStyle(i, sprEdiListDef.FREE_C03.ColNo, sLabel)                          '文字列03
                .SetCellStyle(i, sprEdiListDef.FREE_C04.ColNo, sLabel)                          '文字列04
                .SetCellStyle(i, sprEdiListDef.FREE_C30.ColNo, sLabel)                          '文字列30
                .SetCellStyle(i, sprEdiListDef.AKAKURO_FLG.ColNo, sLabel)                       '赤黒フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_JISSEKI.ColNo, sLabel)                  '実績出力対象フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_MATOMEF.ColNo, sLabel)                  'まとめ登録フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_DELDISP.ColNo, sLabel)                  'EDI取消データ表示フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_SPECIAL.ColNo, sLabel)                  'EDI特殊処理フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_HOLDOUT.ColNo, sLabel)                  '保留データ出力フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_UNSOFLG.ColNo, sLabel)                  'EDIデータ出力先フラグ
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_INDEX.ColNo, sLabel)                    'EDI対象荷主INDEX
                .SetCellStyle(i, sprEdiListDef.SND_SYS_UPD_DATE.ColNo, sLabel)                  '更新日(EDI送信TBL)
                .SetCellStyle(i, sprEdiListDef.SND_SYS_UPD_TIME.ColNo, sLabel)                  '更新時刻(EDI送信TBL)
                .SetCellStyle(i, sprEdiListDef.RCV_SYS_UPD_DATE.ColNo, sLabel)                  '更新日(EDI受信TBL)
                .SetCellStyle(i, sprEdiListDef.RCV_SYS_UPD_TIME.ColNo, sLabel)                  '更新時刻(EDI受信TBL)
                .SetCellStyle(i, sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo, sLabel)                '更新日(出荷(大))
                .SetCellStyle(i, sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo, sLabel)                '更新時刻(出荷(大))
                .SetCellStyle(i, sprEdiListDef.JISSEKI_FLAG.ColNo, sLabel)                      '実績書込フラグ
                .SetCellStyle(i, sprEdiListDef.OUT_FLAG.ColNo, sLabel)                          '出荷データ書込フラグ
                .SetCellStyle(i, sprEdiListDef.AUTO_MATOME_FLG.ColNo, sLabel)                   '自動まとめフラグ
                .SetCellStyle(i, sprEdiListDef.SYS_DEL_FLG.ColNo, sLabel)                       'EDI削除フラグ
                .SetCellStyle(i, sprEdiListDef.ORDER_CHECK_FLG.ColNo, sLabel)                   'オーダー番号重複チェックフラグ
                '▼▼▼二次
                .SetCellStyle(i, sprEdiListDef.RCV_NM_HED.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_NM_DTL.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.RCV_NM_EXT.ColNo, sLabel)
                .SetCellStyle(i, sprEdiListDef.SND_NM.ColNo, sLabel)
                '2012.02.25 大阪対応 START
                .SetCellStyle(i, sprEdiListDef.EDI_CUST_INOUTFLG.ColNo, sLabel)
                '2012.02.25 大阪対応 END
                '▲▲▲二次
                '2014.03.31 セミ標準対応 --ST--
                .SetCellStyle(i, sprEdiListDef.FLAG_17.ColNo, sLabel)
                '2014.03.31 セミ標準対応 --ED--

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
                .SetCellStyle(i, sprEdiListDef.FREE_C05.ColNo, sLabel)                          '文字列05
#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
                .SetCellStyle(i, sprEdiListDef.FREE_C07.ColNo, sLabel)                          '文字列07
                .SetCellStyle(i, sprEdiListDef.FREE_C08.ColNo, sLabel)                          '文字列08
#End If

                .SetCellStyle(i, sprEdiListDef.HAISO_SIJI_NO.ColNo, sLabel)                     '配送指示No.    ADD 2017/06/16
                .SetCellStyle(i, sprEdiListDef.NCGO_OPEOUT_ONLY_FLG.ColNo, sLabel)              '日合出荷データのみフラグ   Add 2018/10/31 要望番号002808

                ''セルに値を設定

                ''*****表示列*****
                .SetCellValue(i, sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprEdiListDef.STATUS_KBN.ColNo, dr.Item("JYOTAI").ToString())                                                                          '状態
                .SetCellValue(i, sprEdiListDef.HORYU_KBN.ColNo, dr.Item("HORYU").ToString())                                                                            '保留
                .SetCellValue(i, sprEdiListDef.ORDER_NO.ColNo, dr.Item("CUST_ORD_NO").ToString())                                                                       'オーダー番号
                .SetCellValue(i, sprEdiListDef.STATUS_NM.ColNo, dr.Item("OUTKA_STATE_KB_NM").ToString())                                                                '進捗区分
                .SetCellValue(i, sprEdiListDef.OUTKO_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("OUTKO_DATE").ToString()))                   '出庫日
                .SetCellValue(i, sprEdiListDef.OUTKA_PLAN_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))         '出荷予定日
                .SetCellValue(i, sprEdiListDef.ARR_PLAN_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))             '納入予定日
                .SetCellValue(i, sprEdiListDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())                                                                            '荷主名
                .SetCellValue(i, sprEdiListDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())                                                                            '届先名
                .SetCellValue(i, sprEdiListDef.REMARK.ColNo, dr.Item("REMARK").ToString())                                                                              '出荷時注意
                .SetCellValue(i, sprEdiListDef.UNSO_ATT.ColNo, dr.Item("UNSO_ATT").ToString())                                                                          '配送時注意
                .SetCellValue(i, sprEdiListDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())                                                                          '商品(中1)
                .SetCellValue(i, sprEdiListDef.OUTKA_CNT.ColNo, dr.Item("OUTKA_TTL_NB").ToString())                                                                     '出荷数
                .SetCellValue(i, sprEdiListDef.DEST_AD.ColNo, dr.Item("DEST_AD").ToString())                                                                            '届先住所
                .SetCellValue(i, sprEdiListDef.UNSO_CORP.ColNo, dr.Item("UNSO_NM").ToString())                                                                          '運送会社名
                .SetCellValue(i, sprEdiListDef.BIN.ColNo, dr.Item("BIN_KB_NM").ToString())                                                                              '便区分
                .SetCellValue(i, sprEdiListDef.MDL_REC_CNT.ColNo, dr.Item("M_COUNT").ToString())                                                                        '中レコード数
                .SetCellValue(i, sprEdiListDef.EDI_NO.ColNo, dr.Item("EDI_CTL_NO").ToString())                                                                        'EDI管理番号(大)
                .SetCellValue(i, sprEdiListDef.MATOME_NO.ColNo, dr.Item("MATOME_NO").ToString())                                                                        'まとめ番号
                .SetCellValue(i, sprEdiListDef.KANRI_NO.ColNo, dr.Item("OUTKA_CTL_NO").ToString())                                                                    '出荷管理番号(大)
                .SetCellValue(i, sprEdiListDef.BUYER_ORDER_NO.ColNo, dr.Item("BUYER_ORD_NO").ToString())                                                                '注文番号
                .SetCellValue(i, sprEdiListDef.OUTKA_SHUBETSU.ColNo, dr.Item("SYUBETU_KB_NM").ToString())                                                               '出荷種別
                .SetCellValue(i, sprEdiListDef.OUTKA_SU.ColNo, dr.Item("OUTKA_PKG_NB").ToString())                                                                      '出荷個数
                .SetCellValue(i, sprEdiListDef.PICK_KB.ColNo, dr.Item("PICK_KB_NM").ToString())                                                                         'ピック区分
                .SetCellValue(i, sprEdiListDef.UNSOMOTO_KBN.ColNo, dr.Item("UNSO_MOTO_KB_NM").ToString())                                                               '運送手配区分
                '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
                If dr.Item("PRTFLG").ToString() = LMConst.FLG.ON Then
                    .SetCellValue(i, sprEdiListDef.PRTFLG.ColNo, LMH030C.PRT_SUMI)
                Else
                    .SetCellValue(i, sprEdiListDef.PRTFLG.ColNo, String.Empty)
                End If                                                                                                                                                  ' 受信伝票印刷
                '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
                .SetCellValue(i, sprEdiListDef.EDI_IMP_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("CRT_DATE").ToString()))                   'EDI取込日
                .SetCellValue(i, sprEdiListDef.EDI_IMP_TIME.ColNo, dr.Item("CRT_TIME").ToString())                                                                      '取込時刻
                '2013.03.01 / Notes1909 受信ファイル追加 開始
                .SetCellValue(i, sprEdiListDef.EDI_FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                '2013.03.01 / notes1909 受信ファイル追加 終了
                .SetCellValue(i, sprEdiListDef.EDI_SEND_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("SEND_DATE").ToString()))                 '送信日
                .SetCellValue(i, sprEdiListDef.EDI_SEND_TIME.ColNo, dr.Item("SEND_TIME").ToString())                                                                    '送信時刻
                .SetCellValue(i, sprEdiListDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())                                                                        '営業所名
                .SetCellValue(i, sprEdiListDef.NRS_WH_NM.ColNo, dr.Item("WH_NM").ToString())                                                                            '倉庫名
                .SetCellValue(i, sprEdiListDef.TANTO_USER_NM.ColNo, dr.Item("TANTO_USER").ToString())                                                                   '担当者名
                .SetCellValue(i, sprEdiListDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER").ToString())                                                               '作成者
                .SetCellValue(i, sprEdiListDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER").ToString())                                                               '更新者
                .SetCellValue(i, sprEdiListDef.SYS_UPD_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))               '更新日
                .SetCellValue(i, sprEdiListDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())                                                                  '更新時刻

                ''*****隠し列*****
                .SetCellValue(i, sprEdiListDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())                                                                        '営業所コード
                .SetCellValue(i, sprEdiListDef.NRS_WH_CD.ColNo, dr.Item("WH_CD").ToString())                                                                            '倉庫コード
                .SetCellValue(i, sprEdiListDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())                                                                        '荷主コード（大）
                .SetCellValue(i, sprEdiListDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())                                                                        '荷主コード（中）
                .SetCellValue(i, sprEdiListDef.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())                                                                  '商品コード
                .SetCellValue(i, sprEdiListDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())                                                                            '届先コード
                .SetCellValue(i, sprEdiListDef.OUTKA_STATE_KB.ColNo, dr.Item("OUTKA_STATE_KB").ToString())                                                              '進捗区分
                .SetCellValue(i, sprEdiListDef.UNSO_CD.ColNo, dr.Item("UNSO_CD").ToString())                                                                            '運送会社コード
                .SetCellValue(i, sprEdiListDef.UNSO_BR_CD.ColNo, dr.Item("UNSO_BR_CD").ToString())                                                                      '運送会社支店コード
                .SetCellValue(i, sprEdiListDef.UNSO_NO_L.ColNo, dr.Item("UNSO_NO_L").ToString())                                                                        '運送番号
                '2012.11.11 センコー対応追加START
                .SetCellValue(i, sprEdiListDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())                                                                            '運行番号
                '2012.11.11 センコー対応追加END
                .SetCellValue(i, sprEdiListDef.UNSO_SYS_UPD_DATE.ColNo, dr.Item("UNSO_SYS_UPD_DATE").ToString())                                                        '更新日(運送L)
                .SetCellValue(i, sprEdiListDef.UNSO_SYS_UPD_TIME.ColNo, dr.Item("UNSO_SYS_UPD_TIME").ToString())                                                        '更新時刻(運送L)
                .SetCellValue(i, sprEdiListDef.MIN_NB.ColNo, dr.Item("MIN_NB").ToString())                                                                              '最小個数
                .SetCellValue(i, sprEdiListDef.EDI_DEL_KB.ColNo, dr.Item("EDI_DEL_KB").ToString())                                                                      'EDI削除区分
                .SetCellValue(i, sprEdiListDef.OUTKA_DEL_KB.ColNo, dr.Item("OUTKA_DEL_KB").ToString())                                                                  '出荷削除区分
                .SetCellValue(i, sprEdiListDef.UNSO_DEL_KB.ColNo, dr.Item("UNSO_DEL_KB").ToString())                                                                    '運送削除区分
                .SetCellValue(i, sprEdiListDef.FREE_C01.ColNo, dr.Item("FREE_C01").ToString())                                                                          '文字列01
                .SetCellValue(i, sprEdiListDef.FREE_C02.ColNo, dr.Item("FREE_C02").ToString())                                                                          '文字列02
                .SetCellValue(i, sprEdiListDef.FREE_C03.ColNo, dr.Item("FREE_C03").ToString())                                                                          '文字列03
                .SetCellValue(i, sprEdiListDef.FREE_C04.ColNo, dr.Item("FREE_C04").ToString())                                                                          '文字列04
                .SetCellValue(i, sprEdiListDef.FREE_C30.ColNo, dr.Item("FREE_C30").ToString())                                                                          '文字列30
                .SetCellValue(i, sprEdiListDef.AKAKURO_FLG.ColNo, dr.Item("AKAKURO_FLG").ToString())                                                                    '赤黒フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_JISSEKI.ColNo, dr.Item("EDI_CUST_JISSEKI").ToString())                                                          '実績出力対象フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_MATOMEF.ColNo, dr.Item("EDI_CUST_MATOMEF").ToString())                                                          'まとめ登録フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_DELDISP.ColNo, dr.Item("EDI_CUST_DELDISP").ToString())                                                          'EDI取消データ表示フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_SPECIAL.ColNo, dr.Item("EDI_CUST_SPECIAL").ToString())                                                          'EDI特殊処理フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_HOLDOUT.ColNo, dr.Item("EDI_CUST_HOLDOUT").ToString())                                                          '保留データ出力フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_UNSOFLG.ColNo, dr.Item("EDI_CUST_UNSOFLG").ToString())                                                          'EDIデータ出力先フラグ
                .SetCellValue(i, sprEdiListDef.EDI_CUST_INDEX.ColNo, dr.Item("EDI_CUST_INDEX").ToString())                                                              'EDI対象荷主INDEX
                .SetCellValue(i, sprEdiListDef.SND_SYS_UPD_DATE.ColNo, dr.Item("SND_SYS_UPD_DATE").ToString())                                                          '更新日(EDI送信TBL)
                .SetCellValue(i, sprEdiListDef.SND_SYS_UPD_TIME.ColNo, dr.Item("SND_SYS_UPD_TIME").ToString())                                                          '更新時刻(EDI送信TBL)
                .SetCellValue(i, sprEdiListDef.RCV_SYS_UPD_DATE.ColNo, dr.Item("RCV_SYS_UPD_DATE").ToString())                                                          '更新日(EDI受信TBL)
                .SetCellValue(i, sprEdiListDef.RCV_SYS_UPD_TIME.ColNo, dr.Item("RCV_SYS_UPD_TIME").ToString())                                                          '更新時刻(EDI受信TBL)
                .SetCellValue(i, sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo, dr.Item("OUTKA_SYS_UPD_DATE").ToString())                                                      '更新日(出荷(大))
                .SetCellValue(i, sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo, dr.Item("OUTKA_SYS_UPD_TIME").ToString())                                                      '更新時刻(出荷(大))
                .SetCellValue(i, sprEdiListDef.JISSEKI_FLAG.ColNo, dr.Item("JISSEKI_FLAG").ToString())                                                                  '実績書込フラグ
                .SetCellValue(i, sprEdiListDef.OUT_FLAG.ColNo, dr.Item("OUT_FLAG").ToString())                                                                          '出荷データ書込フラグ
                .SetCellValue(i, sprEdiListDef.AUTO_MATOME_FLG.ColNo, dr.Item("AUTO_MATOME_FLG").ToString())                                                            '自動まとめフラグ
                .SetCellValue(i, sprEdiListDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())                                                                    'EDI削除フラグ
                .SetCellValue(i, sprEdiListDef.ORDER_CHECK_FLG.ColNo, dr.Item("ORDER_CHECK_FLG").ToString())                                                            'オーダー番号重複チェックフラグ
                '▼▼▼二次
                .SetCellValue(i, sprEdiListDef.RCV_NM_HED.ColNo, dr.Item("RCV_NM_HED").ToString())
                .SetCellValue(i, sprEdiListDef.RCV_NM_DTL.ColNo, dr.Item("RCV_NM_DTL").ToString())
                .SetCellValue(i, sprEdiListDef.RCV_NM_EXT.ColNo, dr.Item("RCV_NM_EXT").ToString())
                .SetCellValue(i, sprEdiListDef.SND_NM.ColNo, dr.Item("SND_NM").ToString())
                '2012.02.25 大阪対応 START
                .SetCellValue(i, sprEdiListDef.EDI_CUST_INOUTFLG.ColNo, dr.Item("EDI_CUST_INOUTFLG").ToString())
                '2012.02.25 大阪対応 END
                '▲▲▲二次
                '2014.03.31 セミ標準対応 --ST--
                .SetCellValue(i, sprEdiListDef.FLAG_17.ColNo, dr.Item("FLAG_17").ToString())
                '2014.03.31 セミ標準対応 --ED--

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
                .SetCellValue(i, sprEdiListDef.FREE_C05.ColNo, dr.Item("FREE_C05").ToString())
#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
                .SetCellValue(i, sprEdiListDef.FREE_C07.ColNo, dr.Item("FREE_C07").ToString())
                .SetCellValue(i, sprEdiListDef.FREE_C08.ColNo, dr.Item("FREE_C08").ToString())
#End If

                'ADD 2017/06/16
                .SetCellValue(i, sprEdiListDef.HAISO_SIJI_NO.ColNo, dr.Item("HAISO_SIJI_NO").ToString())

                .SetCellValue(i, sprEdiListDef.FLAG_19.ColNo, dr.Item("FLAG_19").ToString())
                .SetCellValue(i, sprEdiListDef.UNSOEDI_EXISTS_FLAG.ColNo, dr.Item("UNSOEDI_EXISTS_FLAG").ToString())
                .SetCellValue(i, sprEdiListDef.NCGO_OPEOUT_ONLY_FLG.ColNo, dr.Item("NCGO_OPEOUT_ONLY_FLG").ToString())      '日合出荷データのみフラグ 　 Add 2018/10/31 要望番号002808
            Next

            .ResumeLayout(True)

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

        Dim unsoData As String = String.Empty

        '(2014.01.21)要望番号2145 追加START
        Dim mDelcnt As Integer = 0
        Dim nrsBrCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim meisaiCnacelF As Boolean = False
        '(2014.01.21)要望番号2145 追加END

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)
                '2012.04.04 大阪対応修正START
                If dr("FREE_C30").ToString().Length > 2 Then
                    unsoData = dr("FREE_C30").ToString().Substring(0, 2)
                End If
                '2012.04.04 大阪対応修正END

                '(2014.01.21)要望番号2145 追加START
                nrsBrCd = dr("NRS_BR_CD").ToString()
                custCdL = dr("CUST_CD_L").ToString()
                custCdM = dr("CUST_CD_M").ToString()
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                                                                                                                 "' AND CUST_CD = '", String.Concat(custCdL, custCdM), _
                                                                                                                 "' AND SUB_KB = '63'"))
                If 0 < custDetailsDr.Length Then
                    meisaiCnacelF = True
                End If
                '(2014.01.21)要望番号2145 追加END

                '(2014.01.21)要望番号2145 修正START
                If meisaiCnacelF = True AndAlso Convert.ToInt32(dr("DELCNT").ToString()) > 0 Then
                    .ActiveSheet.Rows(i).ForeColor = Color.Fuchsia

                ElseIf dr("EDI_DEL_KB").ToString().Equals("1") Then
                    'If dr("EDI_DEL_KB").ToString().Equals("1") Then
                    '(2014.01.21)要望番号2145 修正END
                    'EDI取消データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("OUTKA_DEL_KB").ToString().Equals("1") Then
                    '出荷取消データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf String.IsNullOrEmpty(dr("MIN_NB").ToString()) = False AndAlso Convert.ToInt64(dr("MIN_NB")) <= 0 Then
                    '中データの個数がマイナス：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("AKAKURO_FLG").ToString().Equals("1") Then
                    '赤黒区分が赤データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("EDI_DEL_KB").ToString().Equals("3") Then
                    '保留データ：青
                    .ActiveSheet.Rows(i).ForeColor = Color.Blue

                ElseIf unsoData.Equals("01") = True _
                AndAlso dr("UNSO_DEL_KB").ToString().Equals("1") = True Then

                    'EDI出荷(大)データが運送で削除データ：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                ElseIf dr("UNSOEDI_EXISTS_FLAG").ToString().Equals("1") _
                AndAlso dr("NCGO_OPEOUT_ONLY_FLG").ToString().Equals("0") Then  '(この行の条件を)Add 2018/10/31 要望番号002808(先方手配(引き取り)の場合は出荷データのみで輸送データが来ないためエラーにしない)
                    '日本合成 出荷・運送データの整合性対応
                    '運送データが存在しない：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red

                End If

            Next

        End With


    End Sub

#End Region 'Spread

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._Frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function
#End Region

#End Region

End Class
