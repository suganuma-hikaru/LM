' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD070G : 在庫帳票印刷
'  作  成  者       :  
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports GrapeCity.Win.Editors

''' <summary>
''' LMD070Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD070G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD070F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD070F, ByVal g As LMDControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMDconG = g

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

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = "印　刷"
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = always   '印刷
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = always  'マスタ参照
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always  '閉じる

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
            .cmbPrint.TabIndex = LMD070C.CtlTabIndex.Print
            '追加開始 2014.11.26            
            .cmbPrintSub.TabIndex = LMD070C.CtlTabIndex.PrintSub
            '追加終了 2014.11.26
            .cmbEigyo.TabIndex = LMD070C.CtlTabIndex.Eigyo
            .txtCustCdL.TabIndex = LMD070C.CtlTabIndex.CustCD_L
            .txtCustCdM.TabIndex = LMD070C.CtlTabIndex.CustCD_M
            .txtCustCdS.TabIndex = LMD070C.CtlTabIndex.CustCD_S
            .txtCustCdSs.TabIndex = LMD070C.CtlTabIndex.CustCD_SS
            .imdSyukkaDate.TabIndex = LMD070C.CtlTabIndex.SyukkaDate
            .chkNyuko.TabIndex = LMD070C.CtlTabIndex.Nyuko
            .chkSyukka.TabIndex = LMD070C.CtlTabIndex.Syukka
            .cmbNiugoki.TabIndex = LMD070C.CtlTabIndex.Niugoki
            .imdPrintDateS.TabIndex = LMD070C.CtlTabIndex.PrintDate_S
            .imdPrintDateE.TabIndex = LMD070C.CtlTabIndex.PrintDate_E
            .cmbDataInsDate.TabIndex = LMD070C.CtlTabIndex.DataInsDate
            'START YANAI 要望番号1057 在庫証明書出力順変更
            .cmbSort.TabIndex = LMD070C.CtlTabIndex.CMBSORT
            'END YANAI 要望番号1057 在庫証明書出力順変更

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal frm As LMD070F)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '日付コントロールの書式設定
        Call Me.SetDateFormat()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm)

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateFormat()

        With Me._Frm

            Me.SetDateFormat(.imdPrintDateE)
            Me.SetDateFormat(.imdPrintDateS)
            Me.SetDateFormat(.imdSyukkaDate)

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="holidayFlg">休日マスタ反映フラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate, Optional ByVal holidayFlg As Boolean = True)

        ctl.Format = DateFieldsBuilder.BuildFields(LMDControlC.DATE_YYYYMMDD)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(LMDControlC.DATE_SLASH_YYYYMMDD)
        ctl.Holiday = holidayFlg

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMD070F)

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所


        If getDr.Length() > 0 Then
            frm.txtCustCdL.TextValue = getDr(0).Item("CUST_CD_L").ToString()                   '（初期）荷主コード（大）")
            frm.lblCustNmL.TextValue = getDr(0).Item("CUST_NM_L").ToString()                   '（初期）荷主名（大）
            frm.txtCustCdM.TextValue = getDr(0).Item("CUST_CD_M").ToString()                   '（初期）荷主コード（中）
            frm.lblCustNmM.TextValue = getDr(0).Item("CUST_NM_M").ToString()                   '（初期）荷主名（中）

        End If

    End Sub

    ''' <summary>
    ''' 初期在庫の設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        '初期在庫の設定
        Call Me.CreateKBNComboBox(Me._Frm.cmbDataInsDate, LMKbnConst.KBN_G003)

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm

            .cmbPrint.Focus()
        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .cmbEigyo.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .txtCustCdS.TextValue = String.Empty
            .txtCustCdSs.TextValue = String.Empty
            .imdSyukkaDate.TextValue = String.Empty
            .cmbDataInsDate.TextValue = String.Empty
            .imdPrintDateS.TextValue = String.Empty
            .imdPrintDateE.TextValue = String.Empty
            .chkNyuko.Checked = True
            .chkSyukka.Checked = False
            .cmbNiugoki.SelectedValue = Nothing
            'START YANAI 要望番号1057 在庫証明書出力順変更
            .cmbSort.SelectedValue = Nothing
            'END YANAI 要望番号1057 在庫証明書出力順変更

        End With

    End Sub

    ''' <summary>
    ''' 月末在庫コンボ生成
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Friend Sub SetZaikoDateControl(ByVal dt As DataTable)

        Dim cmb As LMImCombo = Me._Frm.cmbDataInsDate
        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        'リストのクリア
        cmb.Items.Clear()

        '空行の追加
        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Dim value As String = String.Empty

        '値設定処理
        If dt Is Nothing = False Then
            'START YANAI No.106
            'Dim max As Integer = dt.Rows.Count - 1
            'For i As Integer = 0 To max

            '    value = dt.Rows(i).Item("RIREKI_DATE").ToString()

            '    'アイテム追加
            '    cmb.Items.Add(New ListItem(New SubItem() {New SubItem(DateFormatUtility.EditSlash(value)), New SubItem(value)}))

            'Next
            If 0 < dt.Rows.Count Then
                value = dt.Rows(0).Item("RIREKI_DATE").ToString()
            End If
            If String.IsNullOrEmpty(value) = False Then
                'アイテム追加
                cmb.Items.Add(New ListItem(New SubItem() {New SubItem(DateFormatUtility.EditSlash(value)), New SubItem(value)}))
            Else
                '初期在庫の設定
                Call Me.CreateComboBox()
            End If
        Else
            '初期在庫の設定
            Call Me.CreateComboBox()
            'END YANAI No.106
        End If

        'START YANAI No.106
        ''初期在庫の設定
        'Call Me.CreateComboBox()
        'END YANAI No.106

        '1行目を初期表示
        cmb.SelectedIndex = 1

    End Sub

#End Region

#Region "印刷区分変更時"

    ''' <summary>
    ''' 印刷区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockPrint(ByVal frm As LMD070F)

        With Me._Frm

            Dim lockflgCust As Boolean = False
            Dim lockflgShuka As Boolean = False
            Dim lockflgZaiko As Boolean = False
            Dim lockflgFrom As Boolean = False
            Dim lockflgTo As Boolean = False
            Dim lockflgFudo As Boolean = False
            'START YANAI 要望番号1057 在庫証明書出力順変更
            Dim lockflgSort As Boolean = False
            'END YANAI 要望番号1057 在庫証明書出力順変更
            '追加開始 2014.11.19 kikuchi
            Dim lockflgPrintSub As Boolean = False 'cmbPrintSubの変更で使用
            Dim lockflgCustLM As Boolean = False   'cmbPrintSubの変更で使用
            Dim lockflgEigyo As Boolean = False    'cmbPrintSubの変更で使用
            '追加終了 2014.11.19 kikuchi


            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString
            '追加開始 2014.11.19 kikuchi
            Dim PrintSubKb As String = .cmbPrintSub.SelectedValue.ToString
            '追加終了 2014.11.19 kikuchi

            Select Case PrintKb

                Case LMD070C.PRINT_NITI
                    '日次出荷別在庫リスト
                    '出荷日以外ロック
                    lockflgCust = True
                    lockflgShuka = False
                    lockflgZaiko = True
                    lockflgFrom = True
                    lockflgTo = True
                    lockflgFudo = True
                    'START YANAI 要望番号1057 在庫証明書出力順変更
                    lockflgSort = True
                    'END YANAI 要望番号1057 在庫証明書出力順変更
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .cmbDataInsDate.Items.Clear()
                    .imdPrintDateS.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                Case LMD070C.PRINT_ZAIKO, LMD070C.PRINT_ZAIKO_SHOUMEI

                    '在庫表
                    '在庫証明書
                    '月末在庫、印刷範囲FROM以外ロック
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = False
                    lockflgFrom = False
                    lockflgTo = True
                    lockflgFudo = True
                    'START YANAI 要望番号1057 在庫証明書出力順変更
                    lockflgSort = False
                    'END YANAI 要望番号1057 在庫証明書出力順変更
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                Case LMD070C.PRINT_NYUSHUKA

                    '入出荷履歴表(標準)
                    '印刷範囲FROM、印刷範囲TO以外ロック
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = True
                    lockflgFrom = False
                    lockflgTo = False
                    lockflgFudo = True
                    'START YANAI 要望番号1057 在庫証明書出力順変更
                    lockflgSort = True
                    'END YANAI 要望番号1057 在庫証明書出力順変更
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .cmbDataInsDate.Items.Clear()
                    .imdSyukkaDate.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                Case LMD070C.PRINT_ZAIKO_SEIGOUSEI_JITU, LMD070C.PRINT_ZAIKO_SEIGOUSEI_SHUKA, LMD070C.PRINT_ZAIKO_SEIGOUSEI_HIKI
                    '在庫整合性リスト(実在庫、出荷予定日、引当数)
                    '月末在庫以外ロック
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = False
                    lockflgFrom = True
                    lockflgTo = True
                    lockflgFudo = True
                    'START YANAI 要望番号1057 在庫証明書出力順変更
                    lockflgSort = True
                    'END YANAI 要望番号1057 在庫証明書出力順変更
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateS.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                Case LMD070C.PRINT_FUDOU
                    '不動在庫リスト
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = True
                    lockflgFrom = False
                    lockflgTo = False
                    lockflgFudo = False
                    'START YANAI 要望番号1057 在庫証明書出力順変更
                    lockflgSort = True
                    'END YANAI 要望番号1057 在庫証明書出力順変更
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .cmbDataInsDate.Items.Clear()
                    .imdSyukkaDate.TextValue = String.Empty
                    .cmbDataInsDate.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi


                    '(2012.12.13)要望番号1671 在庫証明書 条件追加 --- START ---
                Case LMD070C.PRINT_ZAIKO_SHOUMEI_S_SS
                    '在庫証明書(小・極小毎)
                    lockflgCust = False
                    lockflgShuka = True
                    lockflgZaiko = False
                    lockflgFrom = False
                    lockflgTo = True
                    lockflgFudo = True
                    lockflgSort = False
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    '.txtCustCdS.TextValue = String.Empty
                    '.txtCustCdSs.TextValue = String.Empty
                    '.lblCustNmS.TextValue = String.Empty
                    '.lblCustNmSs.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                    '(2012.12.13)要望番号1671 在庫証明書 条件追加 ---  END  ---

                    '(2013.01.10)要望番号1752 消防分類別在庫重量表 条件追加 --- START ---
                Case LMD070C.PRINT_SYOUBOU_BUNRUI
                    '在消防分類別在庫重量表
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = True
                    lockflgFrom = False
                    lockflgTo = True
                    lockflgFudo = True
                    lockflgSort = True
                    '追加開始 2014.11.19 kikuchi
                    lockflgPrintSub = True
                    '追加開始 2014.11.19 kikuchi

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    '追加開始 2014.11.19 kikuchi
                    .cmbPrintSub.TextValue = String.Empty
                    '追加開始 2014.11.19 kikuchi

                    '(2013.01.10)要望番号1752 消防分類別在庫重量表 条件追加 ---  END  ---

                    '追加開始 2014.11.19 kikuchi
                Case LMD070C.PRINT_ZAIKO_UKEHARAI
                    '在庫受払表
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = True
                    lockflgFrom = True
                    lockflgTo = True
                    lockflgFudo = True
                    lockflgSort = True
                    lockflgPrintSub = False

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .txtCustCdL.TextValue = String.Empty
                    .txtCustCdM.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .lblCustNmL.TextValue = String.Empty
                    .lblCustNmM.TextValue = String.Empty
                    .imdPrintDateS.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty

                    .cmbDataInsDate.Items.Clear()
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False

                    '在庫受払表(サブ)
                    Select Case PrintSubKb

                        Case LMD070C.PRINTSUB_ZAIKO_NORMAL _
                           , LMD070C.PRINTSUB_ZAIKO_SOKO _
                           , LMD070C.PRINTSUB_ZAIKO_OKIBA_NORMAL _
                           , LMD070C.PRINTSUB_ZAIKO_OKIBA_KIKEN _
                           , LMD070C.PRINTSUB_ZAIKO_GOODS         'ADD 2020/10/29
                            '在庫受払表　通常
                            '在庫受払表　倉庫指定
                            '在庫受払表（置場別）
                            '在庫受払表（置場別）危険品区分順
                            '在庫受払表（商品ごと）

                            lockflgCust = True
                            lockflgShuka = True
                            lockflgZaiko = True
                            lockflgFrom = False
                            lockflgTo = False
                            lockflgFudo = True
                            lockflgSort = True
                            lockflgCustLM = False
                            lockflgEigyo = False

                            'クリアするもの
                            .txtCustCdS.TextValue = String.Empty
                            .txtCustCdSs.TextValue = String.Empty
                            .lblCustNmS.TextValue = String.Empty
                            .lblCustNmSs.TextValue = String.Empty
                            .imdSyukkaDate.TextValue = String.Empty
                            .cmbNiugoki.Items.Clear()
                            .cmbDataInsDate.Items.Clear()
                            .chkNyuko.Checked = False
                            .chkSyukka.Checked = False

                        Case LMD070C.PRINTSUB_ZAIKO_NONINUSHI _
                           , LMD070C.PRINTSUB_ZAIKO_OKIBA_NONINUSHI
                            '在庫受払表　荷主指定なし
                            '在庫受払表（置場別）荷主指定なし

                            lockflgCust = True
                            lockflgShuka = True
                            lockflgZaiko = True
                            lockflgFrom = False
                            lockflgTo = False
                            lockflgFudo = True
                            lockflgSort = True
                            lockflgCustLM = True
                            lockflgEigyo = False

                            'クリアするもの
                            .txtCustCdS.TextValue = String.Empty
                            .txtCustCdSs.TextValue = String.Empty
                            .txtCustCdL.TextValue = String.Empty
                            .txtCustCdM.TextValue = String.Empty
                            .lblCustNmS.TextValue = String.Empty
                            .lblCustNmSs.TextValue = String.Empty
                            .lblCustNmL.TextValue = String.Empty
                            .lblCustNmM.TextValue = String.Empty
                            .imdPrintDateS.TextValue = String.Empty
                            .imdPrintDateE.TextValue = String.Empty
                            .imdSyukkaDate.TextValue = String.Empty
                            .cmbNiugoki.Items.Clear()
                            .cmbDataInsDate.Items.Clear()
                            .chkNyuko.Checked = False
                            .chkSyukka.Checked = False

                        Case LMD070C.PRINTSUB_GEKKAN_GOTO _
                           , LMD070C.PRINTSUB_GEKKAN_NINUSHI _
                           , LMD070C.PRINTSUB_GEKKAN_GOTO_NINUSHI _
                            '月間入出庫重量集計表（号棟別）
                            '月間入出庫重量集計表（荷主別）
                            '月間入出庫重量集計表(号棟別・荷主別)

                            lockflgCust = True
                            lockflgShuka = True
                            lockflgZaiko = True
                            lockflgFrom = False
                            lockflgTo = True
                            lockflgFudo = True
                            lockflgSort = True
                            lockflgCustLM = False
                            lockflgEigyo = False

                            'クリアするもの
                            .txtCustCdS.TextValue = String.Empty
                            .txtCustCdSs.TextValue = String.Empty
                            .txtCustCdL.TextValue = String.Empty
                            .txtCustCdM.TextValue = String.Empty
                            .lblCustNmS.TextValue = String.Empty
                            .lblCustNmSs.TextValue = String.Empty
                            .lblCustNmL.TextValue = String.Empty
                            .lblCustNmM.TextValue = String.Empty
                            .imdPrintDateS.TextValue = String.Empty
                            .imdPrintDateE.TextValue = String.Empty
                            .imdSyukkaDate.TextValue = String.Empty
                            .cmbNiugoki.Items.Clear()
                            .cmbDataInsDate.Items.Clear()
                            .chkNyuko.Checked = False
                            .chkSyukka.Checked = False

                    End Select

                Case LMD070C.PRINT_SYOUBOU_BUNRUI_ALL
                    '在消防分類別在庫重量表(全荷主・現在庫版)
                    lockflgCust = True
                    lockflgShuka = True
                    lockflgZaiko = True
                    lockflgFrom = True
                    lockflgTo = True
                    lockflgFudo = True
                    lockflgSort = True
                    lockflgPrintSub = True
                    lockflgCustLM = True

                    'クリアするもの
                    .txtCustCdS.TextValue = String.Empty
                    .txtCustCdSs.TextValue = String.Empty
                    .lblCustNmS.TextValue = String.Empty
                    .lblCustNmSs.TextValue = String.Empty
                    .lblCustNmL.TextValue = String.Empty
                    .lblCustNmM.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty
                    .imdPrintDateE.TextValue = String.Empty
                    .cmbNiugoki.Items.Clear()
                    .chkNyuko.Checked = False
                    .chkSyukka.Checked = False
                    .cmbPrintSub.TextValue = String.Empty

            End Select
            '追加終了 2014.11.19 kikuchi

            Me.SetLockControl(.txtCustCdS, lockflgCust)
            Me.SetLockControl(.txtCustCdSs, lockflgCust)
            Me.SetLockControl(.imdSyukkaDate, lockflgShuka)
            Me.SetLockControl(.cmbDataInsDate, lockflgZaiko)
            Me.SetLockControl(.imdPrintDateS, lockflgFrom)
            Me.SetLockControl(.imdPrintDateE, lockflgTo)
            Me.SetLockControl(.chkNyuko, lockflgFudo)
            Me.SetLockControl(.chkSyukka, lockflgFudo)
            Me.SetLockControl(.cmbNiugoki, lockflgFudo)
            'START YANAI 要望番号1057 在庫証明書出力順変更
            Me.SetLockControl(.cmbSort, lockflgSort)
            'END YANAI 要望番号1057 在庫証明書出力順変更
            '追加開始 2014.11.19 kikuchi
            Me.SetLockControl(.cmbPrintSub, lockflgPrintSub)
            Me.SetLockControl(.txtCustCdL, lockflgCustLM)
            Me.SetLockControl(.txtCustCdM, lockflgCustLM)
            Me.SetLockControl(.cmbEigyo, lockflgEigyo)
            '追加終了 2014.11.19 kikuchi

        End With

    End Sub
#End Region

#Region "部品化検討中"

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

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
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

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

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

#Region "初期在庫"
    ''' <summary>
    ''' 区分マスタコンボボックス作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="KbnGCD">区分グループコード</param>
    ''' <remarks></remarks>
    Friend Sub CreateKBNComboBox(ByRef cmb As LMImCombo, ByVal KbnGCD As String)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim hantei As String = "01"

        'マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_CD='" + hantei + "'AND KBN_GROUP_CD = '" + KbnGCD + "'")

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            cd = getDr(i).Item("KBN_CD").ToString()

            '2017/09/25 修正 李↓
            item = getDr(i).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub
    ''' <summary>
    ''' 区分マスタコンボボックス(荷動き・単位)作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="KbnGCD">区分グループコード</param>
    ''' <remarks></remarks>
    Friend Sub CreateNiugokiComboBox(ByRef cmb As LMImCombo, ByVal KbnGCD As String)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim hantei As Integer = -1
        'リストのクリア
        cmb.Items.Clear()

        'マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(" KBN_GROUP_CD = '" + KbnGCD + "'")

        Dim max As Integer = getDr.Count - 1

        'MAX件数が-1件の場合処理を終了する
        If max = hantei Then

            Exit Sub

        End If

        For i As Integer = 0 To max

            cd = getDr(i).Item("KBN_CD").ToString()

            '2017/09/25 修正 李↓
            item = getDr(i).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

            '荷動き・単位のコンボボックスに値の設定
            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

        '荷動き・単位のコンボボックスの初期設定
        Me._Frm.cmbNiugoki.SelectedValue = "01"

    End Sub

#End Region


#End Region

#End Region

#End Region

End Class
