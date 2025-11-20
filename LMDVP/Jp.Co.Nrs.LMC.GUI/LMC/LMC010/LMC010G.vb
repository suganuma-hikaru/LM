' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMC     : 出荷
'  プログラムID   : LMC010G : 出荷データ一覧
'  作  成  者     : 大貫和正
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMC010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    '2017/09/25 修正 李↓
    ''20160614 tsunehira add start
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    ''20160614 tsunehira add end
    '2017/09/25 修正 李↑


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        'Me.MyForm = frm
        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMCconG = New LMCControlG(MyBase.MyHandler, DirectCast(frm, LMFormSxga))

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
            .F1ButtonEnabled = always   '新規
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = always   '完了
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always   '検索
            .F10ButtonEnabled = always  'マスタ参照
            .F11ButtonEnabled = True    '初期荷主変更
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
            .grpSTATUS.TabIndex = LMC010C.CtlTabIndex_MAIN.GRPSTATUS
            .txtCustCD.TabIndex = LMC010C.CtlTabIndex_MAIN.TXTCUSTCD
            .txtPicCD.TabIndex = LMC010C.CtlTabIndex_MAIN.TXTPICCD
            .cmbEigyo.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBEIGYO
            .cmbSoko.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBSOKO
            'START YANAI 要望番号917
            '.cmbPrintStatus.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBPRINTSTATUS
            'END YANAI 要望番号917
            .cmbPrintSyubetu.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBPRINTSYUBETU
            .cmbPickSyubetu.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBPICKSYUBETU
            .chkSelectByNrsB.TabIndex = LMC010C.CtlTabIndex_MAIN.CHKSELECTBYNRSB
            .imdPrintDate_From.TabIndex = LMC010C.CtlTabIndex_MAIN.IMDPRINTDATEF
            .imdPrintDate_To.TabIndex = LMC010C.CtlTabIndex_MAIN.IMDPRINTDATET
            .cmbSearchDate.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBSEARCHDATE
            .imdSearchDate_From.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBSEARCHDATE_FROM
            .imdSearchDate_To.TabIndex = LMC010C.CtlTabIndex_MAIN.CMBSEARCHDATE_To
            .grpUNSO.TabIndex = LMC010C.CtlTabIndex_MAIN.GRPUNSO
            .grpOutkaNoL.TabIndex = LMC010C.CtlTabIndex_MAIN.GRPOUTKANOL
            .sprDetail.TabIndex = LMC010C.CtlTabIndex_MAIN.SPRDETAIL
            .grpTrapoPrint.TabIndex = LMC010C.CtlTabIndex_MAIN.GRPTRAPOPRINT        ' 名鉄対応(2499) 20160323 added inoue

            'GroupBox chkSTA
            .chkStaYotei.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTAYOTEI
            .chkStaPrint.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTAPRINT
            .chkStaShukko.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTASHUKKO
            .chkStaKenpin.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTAKENPIN
            .chkStaShukka.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTASHUKKA
            .chkStaHoukoku.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTAHOUKOKU
            .chkStaTorikeshi.TabIndex = LMC010C.CtlTabIndex_chkSTA.CHKSTATORIKESHI

            'GroupBox grpUNSO
            .txtTrnCD.TabIndex = LMC010C.CtlTabIndex_grpUNSO.TXTTRNCD
            .txtTrnBrCD.TabIndex = LMC010C.CtlTabIndex_grpUNSO.TXTTRNBRCD
            .btnUnso.TabIndex = LMC010C.CtlTabIndex_grpUNSO.BTNUNSO
            .cmbPrint.TabIndex = LMC010C.CtlTabIndex_grpUNSO.CMBPRINT
            .btnPrint.TabIndex = LMC010C.CtlTabIndex_grpUNSO.BTNPRINT
            .cmbJikkou.TabIndex = LMC010C.CtlTabIndex_grpUNSO.CMBJIKKOU
            .btnJikkou.TabIndex = LMC010C.CtlTabIndex_grpUNSO.BTNJIKKOU

            'GroupBox grpOutkaNoL
            .txtOutkaNoL.TabIndex = LMC010C.CtlTabIndex_grpOUTKANOL.TXTOUTKANOL

            'TabStop

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMC010F, ByVal sysDate As String)  '要望番号:1568 terakawa 2013.01.17 sysDate追加

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コンボボックスの設定

        '===================== 部品化により削除 ====================='

        'Call Me._LMCconG.CreateComboBox(frm.cmbEigyo, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", String.Empty)   '営業所コンボ
        'Call Me._LMCconG.CreateComboBox(frm.cmbSoko, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", String.Empty)              '倉庫コンボ
        'Call Me._LMCconG.CreateKBNComboBox(frm.cmbPrintStatus, LMKbnConst.KBN_K013)                                      '印刷状況
        'Call Me._LMCconG.CreateKBNComboBox(frm.cmbPrintSyubetu, LMKbnConst.KBN_S034)                                     '印刷種別
        'Call Me._LMCconG.CreateKBNComboBox(frm.cmbSearchDate, LMKbnConst.KBN_S037)                                       '出荷検索日区分
        'Call Me._LMCconG.CreateKBNComboBox(frm.cmbPrint, LMKbnConst.KBN_S034)                                            '印刷種別
        'Call Me._LMCconG.CreateKBNComboBox(frm.cmbJikkou, LMKbnConst.KBN_S038)                                           '実行日区分

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm, sysDate)  '要望番号:1568 terakawa 2013.01.17 sysDate追加

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMC010F, ByVal sysDate As String)

        '=== TODO : 初期荷主取得仕様決定後　修正 ==='

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.chkStaYotei.Checked() = True                                                     ' 進捗区分（予定入力済）
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbSoko.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()      '（自）倉庫

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            frm.cmbEigyo.ReadOnly = True
        Else
            frm.cmbEigyo.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        '要望番号:1793 terakawa 2013.01.21 Start
        '要望番号:1839 terakawa 2013.02.08 Start
        'frm.cmbPick.SelectedValue() = LMC010C.PICK_KB_MATOMENASHI
        '要望番号:1839 terakawa 2013.02.08 End
        '要望番号:1793 terakawa 2013.01.21 End

        If getDr.Length() > 0 Then
            frm.txtCustCD.TextValue = getDr(0).Item("CUST_CD_L").ToString()                  '（初期）荷主コード（大）
            frm.lblCustNM.TextValue = getDr(0).Item("CUST_NM_L").ToString()                  '（初期）荷主名（大）
            '要望番号:1568 terakawa 2013.01.17 Start
            frm.txtShinkiCustCdL.TextValue = getDr(0).Item("CUST_CD_L").ToString()           '（初期）荷主コード（大）
            '要望番号:1839 terakawa 2013.02.08 Start
            Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST). _
                     Select("SYS_DEL_FLG = '0' AND CUST_CD_L = '" & getDr(0).Item("CUST_CD_L").ToString() & _
                            "' AND CUST_CD_M = '00' AND CUST_CD_S = '00' AND CUST_CD_SS = '00'")
            If getCustDr.Length() > 0 Then
                frm.cmbPick.SelectedValue() = getCustDr(0).Item("PICK_LIST_KB").ToString()   '（初期）ピッキングリスト区分
                'デフォルト倉庫コード設定 yamanaka 2013.02.26 Start
                frm.cmbSoko.SelectedValue = getCustDr(0).Item("DEFAULT_SOKO_CD").ToString    '（初期）デフォルト倉庫
                'デフォルト倉庫コード設定 yamanaka 2013.02.26 End
            End If
            '要望番号:1839 terakawa 2013.02.08 End
        End If

        frm.imdShinkiOutkaDate.TextValue = sysDate                                           '（初期）出荷予定日
        Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
        If 0 < mUser.Length Then
            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(frm.imdShinkiOutkaDate.TextValue), "0000/00/00"))
            Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                    frm.imdShinkiOutkaDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                    frm.imdShinkiOutkaDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")
            End Select

        End If
        '要望番号:1568 terakawa 2013.01.17 End

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        'ファンクションキーのみ解除
        Me._Frm.FunctionKey.Enabled = True
        'Call Me.SetFunctionKey()

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

            'ここにコントロールのクリアを書く
            .txtCustCD.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty
            .txtPicCD.TextValue = String.Empty
            .lblPicNM.TextValue = String.Empty
            .cmbEigyo.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            'START YANAI 要望番号917
            '.cmbPrintStatus.SelectedValue = String.Empty
            'END YANAI 要望番号917
            .cmbPrintSyubetu.SelectedValue = String.Empty
            .imdPrintDate_From.TextValue = String.Empty
            .imdPrintDate_To.TextValue = String.Empty
            .cmbSearchDate.SelectedValue = String.Empty
            .imdSearchDate_From.TextValue = String.Empty
            .imdSearchDate_To.TextValue = String.Empty

            'GroupBox chkSTA
            .chkStaYotei.Checked = False
            .chkStaPrint.Checked = False
            .chkStaShukko.Checked = False
            .chkStaKenpin.Checked = False
            .chkStaShukka.Checked = False
            .chkStaHoukoku.Checked = False
            .chkStaTorikeshi.Checked = False

            'GroupBox grpUNSO
            .txtTrnCD.TextValue = String.Empty
            .txtTrnBrCD.TextValue = String.Empty
            .lblStatus.TextValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
            .cmbJikkou.SelectedValue = String.Empty

            'GroupBox grpOUTKANOL
            .txtOutkaNoL.TextValue = String.Empty

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
    ''' 検索条件の荷主名のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearCustNM()

        With Me._Frm

            If String.IsNullOrEmpty(.txtCustCD.TextValue) = True Then
                .lblCustNM.TextValue = String.Empty
            End If

            If String.IsNullOrEmpty(.txtPicCD.TextValue) = True Then
                .lblPicNM.TextValue = String.Empty
            End If

        End With

    End Sub


    '社内入荷データ作成 terakawa Start
#Region "コンボボックス設定"
    ''' <summary>
    ''' コンボボックスの設定 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetCmbIntEdi(ByVal ds As DataSet)

        With Me._Frm.cmbIntEdi

            'リストのクリア 
            .Items.Clear()

            Dim INT_EDI_NM As String = String.Empty
            Dim INT_EDI_CD As String = String.Empty

            '空行の設定
            .Items.Add(New ListItem(New SubItem() {New SubItem(INT_EDI_NM), New SubItem(INT_EDI_CD)}))

            'マスタ検索処理
            Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_INT_EDI)
            Dim max As Integer = dt.Rows.Count - 1
            For i As Integer = 0 To max

                INT_EDI_NM = dt.Rows(i).Item("INT_EDI_NM").ToString()
                INT_EDI_CD = dt.Rows(i).Item("INT_EDI_CD").ToString() '★
                .Items.Add(New ListItem(New SubItem() {New SubItem(INT_EDI_NM), New SubItem(INT_EDI_CD)})) '★
            Next

        End With

    End Sub
#End Region

#Region "コントロール設定(実行コンボ選択時)"
    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetIntEdiControl(ByVal frm As LMC010F)

        Dim selectCmbValue As String = frm.cmbJikkou.SelectedValue.ToString

        frm.grpIntEdi.Visible = False
        frm.cmbIntEdi.Visible = False

        Select Case selectCmbValue

            Case LMC010C.SHANAI_INKA_MAKE '社内入荷データ作成
                frm.grpIntEdi.Visible = True
                frm.cmbIntEdi.Visible = True

            Case Else

        End Select

    End Sub

#End Region
    '社内入荷データ作成 terakawa End

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

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

            'Select Case strKBN
            '    Case "1"
            '        .txtType.TextValue = Get_Shubetu(Mid(strKNAPTA, 2, 2))      
            '        .txtHurikomi.TextValue = Trim(Mid(strKNAPTA, 2, 10))        
            '        .txtHurikomiNm.TextValue = Trim(Mid(strKNAPTA, 15, 40))     
            '        .txtTorikumi.TextValue = Trim(Mid(strKNAPTA, 55, 4))        
            '        .txtBankno.TextValue = Trim(Mid(strKNAPTA, 59, 4))          
            '        .txtBankNm.TextValue = Trim(Mid(strKNAPTA, 63, 15))         
            '        .txtShitenno.TextValue = Trim(Mid(strKNAPTA, 78, 3))        
            '        .txtShitenNm.TextValue = Trim(Mid(strKNAPTA, 81, 15))       
            '        .txtYokinsyu.TextValue = Get_Yokin(Mid(strKNAPTA, 96, 1))   
            '        .txtKozabango.TextValue = Trim(Mid(strKNAPTA, 97, 7))       
            '    Case "8"
            '        .txtTotalcnt.Value = CInt(Trim(Mid(strKNAPTA, 2, 6)))       
            '        .txtTotalKin.Value = CDec(Trim(Mid(strKNAPTA, 8, 12)))      
            'End Select

        End With

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_ORD_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_ORD_NO, "オーダー番号", 100, True)
        Public Shared OUTKA_STATE_KB_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_STATE_KB_NM, "進捗区分", 70, True)
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷予定日", 80, True)
        Public Shared ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.ARR_PLAN_DATE, "納入予定日", 80, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_NM, "荷主名", 150, True)
        'START YANAI 要望番号748
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_CD_S, "小CD", 30, True)
        'END YANAI 要望番号748
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.DEST_NM, "届先名", 130, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.GOODS_NM, "商品(中1)", 130, True)
        Public Shared OUTKA_PKG_NB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_PKG_NB, "梱包数", 40, True)
        Public Shared OUTKA_TTL_NB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_TTL_NB, "総個数", 40, True)
        Public Shared DEST_AD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.DEST_AD, "届先住所", 130, True)
        Public Shared BIN_KB_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.BIN_KB_NM, "便区分", 130, True)             '2013.02.27 / Notes1897
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSOCO_NM, "運送会社名", 130, True)
        Public Shared DENP_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.DENP_NO, "送り状番号", 100, True)
        Public Shared M_COUNT As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.M_COUNT, "中レコ" & vbCrLf & "ード数", 60, True)
        '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
        Public Shared BUYER_ORD_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.BUYER_ORD_NO, "注文番号", 100, True)
        '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end
        Public Shared WEB_OUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.WEB_OUTKA_NO_L, "顧客出荷管理番号", 120, True)
        Public Shared OUTKA_NO_L As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_NO_L, "出荷管理番号" & vbCrLf & "（大）", 100, True)
        Public Shared SYUBETU_KB_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYUBETU_KB_NM, "出荷種別", 70, True)
        Public Shared REMARK_UNSO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.REMARK_UNSO, "配送時注意事項", 100, True) '要望番号1856 2013/02/21
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
        Public Shared LOT_NO_S As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.LOT_NO_S, "ロット№", 100, True)
        Public Shared SERIAL_NO_S As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SERIAL_NO_S, "シリアル№", 100, True)
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
        Public Shared COA_UMU As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.COA_UMU, "分析表添付", 80, True)             'ADD 2019/06/18 004870
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.WH_NM, "倉庫名", 150, True)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.NRS_BR_NM, "営業所名", 150, True)
        Public Shared TANTO_USER As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TANTO_USER, "担当者", 150, True)
        Public Shared SYS_ENT_USER As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYS_ENT_USER, "作成者", 150, True)
        Public Shared SYS_UPD_USER As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYS_UPD_USER, "更新者", 150, True)
        Public Shared LAST_PRINT_DATE As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.LAST_PRINT_DATE, "最終" & vbCrLf & "印刷日", 100, True)
        Public Shared LAST_PRINT_TIME As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.LAST_PRINT_TIME, "最終" & vbCrLf & "印刷時間", 100, True)

#If True Then ' 出荷作業ステータス対応 20160822 added inoue
        Public Shared WH_WORK_STATUS_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.WH_WORK_STATUS_NM, "庫内作業" & vbCrLf & "ステータス", 90, True)
#End If
        Public Shared WH_TAB_STATUS_NM As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.WH_TAB_STATUS_NM, "現場作業指示", 90, True)

        'invisible
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYS_UPD_DATE, "更新日", 86, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 86, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.NRS_BR_CD, "営業所コード", 86, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.WH_CD, "倉庫コード", 86, False)
        Public Shared PICK_KB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.PICK_KB, "ピッキングリスト区分", 86, False)
        Public Shared OUTKA_SASHIZU_PRT_YN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_SASHIZU_PRT_YN, "出荷指図書印刷", 86, False)
        Public Shared OUTOKA_KANRYO_YN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTOKA_KANRYO_YN, "出庫完了", 86, False)
        Public Shared OUTKA_KENPIN_YN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_KENPIN_YN, "出荷検品", 86, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_CD_L, "荷主コード（大）", 86, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 86, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.GOODS_CD_NRS, "商品コード", 86, False)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.DEST_CD, "届先コード", 86, False)
        Public Shared OUTKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.OUTKA_STATE_KB, "進捗区分", 86, False)
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSO_CD, "運送会社コード", 86, False)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSO_BR_CD, "運送会社支店コード", 86, False)
        Public Shared UNSO_NO_L As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSO_NO_L, "運送番号", 86, False)
        Public Shared UNSO_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSO_SYS_UPD_DATE, "更新日", 86, False)
        Public Shared UNSO_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.UNSO_SYS_UPD_TIME, "更新時刻", 86, False)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.LOT_NO, "ロット№", 86, False)
        Public Shared SEIQ_FIXED_FLAG As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SEIQ_FIXED_FLAG, "支払料金確定フラッグ", 86, False)
        Public Shared S_COUNT As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.S_COUNT, "小レコード数", 86, False)
        Public Shared BACKLOG_NB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.BACKLOG_NB, "引当残個数", 86, False)
        Public Shared BACKLOG_QT As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.BACKLOG_QT, "引当残数量", 86, False)
        Public Shared FURI_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.FURI_NO, "振替管理番号", 0, False)
        Public Shared NIHUDA_YN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.NIHUDA_YN, "荷札有無フラグ", 0, False)
        'START YANAI メモ②No.2
        Public Shared SASZ_USER As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SASZ_USER, "指図書印刷者", 0, False)
        'END YANAI メモ②No.2
        'START YANAI 20120122 立会書印刷対応
        Public Shared TACHIAI_FLG As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TACHIAI_FLG, "立会書フラグ", 0, False)
        'END YANAI 20120122 立会書印刷対応
        '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 -- START --
        Public Shared NHS_FLAG As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.NHS_FLAG, "納品書発行フラグ", 0, False)
        '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 --  END  --
        'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
        Public Shared MIN_ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.MIN_ALCTD_NB, "引当済個数(最小)", 0, False)
        Public Shared MIN_ALCTD_QT As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.MIN_ALCTD_QT, "引当済数量(最小)", 0, False)
        'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TRIP_NO, "運行番号", 86, False)
        Public Shared TRIP_NO_SYUKA As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TRIP_NO_SYUKA, "運行番号(集荷)", 86, False)
        Public Shared TRIP_NO_TYUKEI As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TRIP_NO_TYUKEI, "運行番号(中継)", 86, False)
        Public Shared TRIP_NO_HAIKA As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.TRIP_NO_HAIKA, "運行番号(配荷)", 86, False)
        Public Shared SHIHARAI_FIXED_FLAG As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SHIHARAI_FIXED_FLAG, "支払料金確定フラグ", 86, False)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End

        '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加START
        Public Shared CUST_DEST_CD As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.CUST_DEST_CD, "顧客運賃纏めコード", 86, False)
        '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加END

#If True Then ' 西濃自動送り状番号対応 20160704 added inoue
        Public Shared AUTO_DENP_KBN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.AUTO_DENP_KBN, "自動送り状番号区分", 0, False)
        Public Shared AUTO_DENP_NO As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.AUTO_DENP_NO, "自動送り状番号", 0, False)
#End If
        Public Shared SYUBETU_KB As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SYUBETU_KB, "出荷種別区分", 0, False)

        Public Shared SHIKAKARI_HIN_FLG As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.SHIKAKARI_HIN_FLG, "仕掛品フラグ(FFEM固有)", 0, False)
        Public Shared ZFVYHKKBN As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.ZFVYHKKBN, "引当計上予実区分(FFEM固有)", 0, False)
        Public Shared MATNR As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.MATNR, "品目コード(LMS M_GOODS.GOODS_CD_CUST が FFEM では MATNR)", 0, False)
        Public Shared ZFVYDENTYP As SpreadColProperty = New SpreadColProperty(LMC010C.SprColumnIndex.ZFVYDENTYP, "伝票タイプ区分(FFEM固有)", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.Sheets(0).ColumnCount = LMC010C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMC010G.sprDetailDef())
            .sprDetail.SetColProperty(New LMC010G.sprDetailDef(), True)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.CUST_NM.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_ORD_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))       'オーダー番号
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, True))         '進捗
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_PLAN_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))                                  '出荷予定日
            .sprDetail.SetCellStyle(0, sprDetailDef.ARR_PLAN_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))                                    '納入予定日
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 122, False))                  '荷主名
            'START YANAI 要望番号748
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 2, False))          '荷主ＣＤ小
            'END YANAI 要望番号748
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))                   '届先
            .sprDetail.SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False))          '商品(中1) '検証結果_導入時要望№62対応(2011.09.13)
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_PKG_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999, True))                            '梱包数
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_TTL_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999, True))                            '総個数
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_AD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))                   '届先住所
            .sprDetail.SetCellStyle(0, sprDetailDef.DENP_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 20, False))             '送り状番号
            .sprDetail.SetCellStyle(0, sprDetailDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 122, False))                '運送会社名
            '2013.02.27 / Notes1897開始
            .sprDetail.SetCellStyle(0, sprDetailDef.BIN_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_U001, False))                  '便区分
            '2013.02.27 / Notes1897終了
            .sprDetail.SetCellStyle(0, sprDetailDef.M_COUNT.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 99, True))                                     '中レコード数
            .sprDetail.SetCellStyle(0, sprDetailDef.WEB_OUTKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 9, False))       '顧客管理番号
            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 9, False))           '管理番号
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
            .sprDetail.SetCellStyle(0, sprDetailDef.BUYER_ORD_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 30, False))      'オーダー番号
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end
            .sprDetail.SetCellStyle(0, sprDetailDef.SYUBETU_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_S020, False))              '出荷種別
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            .sprDetail.SetCellStyle(0, sprDetailDef.LOT_NO_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))          'ロット№
            .sprDetail.SetCellStyle(0, sprDetailDef.SERIAL_NO_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 40, False))       'シリアル№
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            .sprDetail.SetCellStyle(0, sprDetailDef.COA_UMU.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_U009, False))                     '分析表有無 ADD 2019*06/18 004870
            .sprDetail.SetCellStyle(0, sprDetailDef.REMARK_UNSO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))              '配送時注意事項　要望番号1856 2013/02/13 本明
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, True))              '倉庫名
            .sprDetail.SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, True))          '営業所名
            .sprDetail.SetCellStyle(0, sprDetailDef.TANTO_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, True))                 '担当者名
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))              '作成者
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_USER.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))              '最終更新者
            .sprDetail.SetCellStyle(0, sprDetailDef.LAST_PRINT_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, True))            '最終印刷日
            .sprDetail.SetCellStyle(0, sprDetailDef.LAST_PRINT_TIME.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, True))            '最終印刷時間

#If True Then ' 出荷作業ステータス対応 20160824 added inoue
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_WORK_STATUS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S107", False))                       '作業ステータス
#End If
            .sprDetail.SetCellStyle(0, sprDetailDef.WH_TAB_STATUS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S118", False))                       '現場作業指示ステータス

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMC010F)

        With frm.sprDetail

            'visible
            .Sheets(0).Cells(0, sprDetailDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_ORD_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_STATE_KB_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_PLAN_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.ARR_PLAN_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_NM.ColNo).Value = String.Empty
            'START YANAI 要望番号748
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_S.ColNo).Value = String.Empty
            'END YANAI 要望番号748
            .Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_PKG_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_TTL_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_AD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DENP_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSOCO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BIN_KB_NM.ColNo).Value = String.Empty       '2013.02.27 / Notes1897
            .Sheets(0).Cells(0, sprDetailDef.M_COUNT.ColNo).Value = String.Empty
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
            .Sheets(0).Cells(0, sprDetailDef.BUYER_ORD_NO.ColNo).Value = String.Empty
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
            .Sheets(0).Cells(0, sprDetailDef.WEB_OUTKA_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYUBETU_KB_NM.ColNo).Value = String.Empty
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            .Sheets(0).Cells(0, sprDetailDef.LOT_NO_S.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SERIAL_NO_S.ColNo).Value = String.Empty
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            .Sheets(0).Cells(0, sprDetailDef.COA_UMU.ColNo).Value = String.Empty         'ADD 2019/06/18 004870
            .Sheets(0).Cells(0, sprDetailDef.REMARK_UNSO.ColNo).Value = String.Empty    '要望番号1856対応　2013/02/21　本明
            .Sheets(0).Cells(0, sprDetailDef.WH_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRS_BR_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.TANTO_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_ENT_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LAST_PRINT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LAST_PRINT_TIME.ColNo).Value = String.Empty

#If True Then   ' 出荷作業ステータス対応 20160824 added inoue
            .Sheets(0).Cells(0, sprDetailDef.WH_WORK_STATUS_NM.ColNo).Value = String.Empty
#End If
            .Sheets(0).Cells(0, sprDetailDef.WH_TAB_STATUS_NM.ColNo).Value = String.Empty

            'invisible
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRS_BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.WH_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.PICK_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTOKA_KANRYO_YN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_KENPIN_YN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.GOODS_CD_NRS.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_STATE_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSO_BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSO_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSO_SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.UNSO_SYS_UPD_TIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FURI_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NIHUDA_YN.ColNo).Value = String.Empty
            'START YANAI メモ②No.2
            .Sheets(0).Cells(0, sprDetailDef.SASZ_USER.ColNo).Value = String.Empty
            'END YANAI メモ②No.2
            '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 -- START --
            .Sheets(0).Cells(0, sprDetailDef.NHS_FLAG.ColNo).Value = String.Empty
            '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 --  END  --
            'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
            .Sheets(0).Cells(0, sprDetailDef.MIN_ALCTD_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.MIN_ALCTD_QT.ColNo).Value = String.Empty
            'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt"></param>
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

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ",")
            Dim sLabelC As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added

            Dim rowColorType As String = ""
            Dim isCutomizeRowColor As Boolean = _
                Me.IsCustomizeRowColorCustWithType(_Frm.cmbEigyo.SelectedValue.ToString() _
                                                 , _Frm.txtCustCD.TextValue _
                                                 , rowColorType)
#End If

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added
                If (isCutomizeRowColor) Then
                    Dim rowColor As Color = Me.GetRowColor(rowColorType _
                                                         , dr)
                    If (rowColor <> Color.Black) Then
                        ' 行カラー設定
                        .ActiveSheet.Rows(i).ForeColor = rowColor
                    End If
                End If
#End If

                'セルスタイル設定

                '*****表示列*****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.CUST_ORD_NO.ColNo, sLabel)           'オーダー番号
                .SetCellStyle(i, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, sLabel)     '進捗区分
                .SetCellStyle(i, sprDetailDef.OUTKA_PLAN_DATE.ColNo, sLabel)       '出荷予定日
                .SetCellStyle(i, sprDetailDef.ARR_PLAN_DATE.ColNo, sLabel)         '納入予定日
                .SetCellStyle(i, sprDetailDef.CUST_NM.ColNo, sLabel)               '荷主名
                'START YANAI 要望番号748
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)             '荷主コード小
                'END YANAI 要望番号748
                .SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, sLabel)               '届先
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)              '商品(中1)
                .SetCellStyle(i, sprDetailDef.OUTKA_PKG_NB.ColNo, sNum)            '梱包数
                .SetCellStyle(i, sprDetailDef.OUTKA_TTL_NB.ColNo, sNum)            '総個数
                .SetCellStyle(i, sprDetailDef.DEST_AD.ColNo, sLabel)               '届先住所
                .SetCellStyle(i, sprDetailDef.DENP_NO.ColNo, sLabel)               '送り状番号
                .SetCellStyle(i, sprDetailDef.UNSOCO_NM.ColNo, sLabel)             '運送会社名
                .SetCellStyle(i, sprDetailDef.BIN_KB_NM.ColNo, sLabel)             '便区分         '2013.02.27 / Notes1897
                .SetCellStyle(i, sprDetailDef.M_COUNT.ColNo, sNum)                 '中数
                '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
                .SetCellStyle(i, sprDetailDef.BUYER_ORD_NO.ColNo, sLabel)          '注文番号
                '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
                .SetCellStyle(i, sprDetailDef.WEB_OUTKA_NO_L.ColNo, sLabel)        '顧客管理番号
                .SetCellStyle(i, sprDetailDef.OUTKA_NO_L.ColNo, sLabel)            '管理番号
                .SetCellStyle(i, sprDetailDef.SYUBETU_KB_NM.ColNo, sLabel)         '出荷種別
                .SetCellStyle(i, sprDetailDef.REMARK_UNSO.ColNo, sLabel)           '配送時注意事項 '要望番号1856対応　2013/02/21　本明
                'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
                .SetCellStyle(i, sprDetailDef.LOT_NO_S.ColNo, sLabel)              'ロット№
                .SetCellStyle(i, sprDetailDef.SERIAL_NO_S.ColNo, sLabel)           'シリアル№
                'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
                .SetCellStyle(i, sprDetailDef.COA_UMU.ColNo, sLabelC)               '分析表有無 ADD 2019/06/18 004870
                .SetCellStyle(i, sprDetailDef.WH_NM.ColNo, sLabel)                 '倉庫名
                .SetCellStyle(i, sprDetailDef.NRS_BR_NM.ColNo, sLabel)             '営業所名
                .SetCellStyle(i, sprDetailDef.TANTO_USER.ColNo, sLabel)            '担当者名
                .SetCellStyle(i, sprDetailDef.SYS_ENT_USER.ColNo, sLabel)          '作成者
                .SetCellStyle(i, sprDetailDef.SYS_UPD_USER.ColNo, sLabel)          '最終更新者
                .SetCellStyle(i, sprDetailDef.LAST_PRINT_DATE.ColNo, sLabel) '最終印刷日
                .SetCellStyle(i, sprDetailDef.LAST_PRINT_TIME.ColNo, sLabel) '最終印刷時間

#If True Then ' 出荷作業ステータス対応 20160704 added inoue
                .SetCellStyle(i, sprDetailDef.WH_WORK_STATUS_NM.ColNo, sLabel)
#End If
                .SetCellStyle(i, sprDetailDef.WH_TAB_STATUS_NM.ColNo, sLabel)
                '*****隠し列*****
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PICK_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTOKA_KANRYO_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_KENPIN_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OUTKA_STATE_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSO_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSO_SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSO_SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEIQ_FIXED_FLAG.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.S_COUNT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BACKLOG_NB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BACKLOG_QT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FURI_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NIHUDA_YN.ColNo, sLabel)
                'START YANAI メモ②No.2
                .SetCellStyle(i, sprDetailDef.SASZ_USER.ColNo, sLabel)
                'END YANAI メモ②No.2
                'START YANAI 20120122 立会書印刷対応
                .SetCellStyle(i, sprDetailDef.TACHIAI_FLG.ColNo, sLabel)
                'END YANAI 20120122 立会書印刷対応
                '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 -- START --
                .SetCellStyle(i, sprDetailDef.NHS_FLAG.ColNo, sLabel)
                '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 --  END  --
                'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
                .SetCellStyle(i, sprDetailDef.MIN_ALCTD_NB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MIN_ALCTD_QT.ColNo, sLabel)
                'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                .SetCellStyle(i, sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TRIP_NO_SYUKA.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TRIP_NO_TYUKEI.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TRIP_NO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SHIHARAI_FIXED_FLAG.ColNo, sLabel)
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加START
                .SetCellStyle(i, sprDetailDef.CUST_DEST_CD.ColNo, sLabel)
                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加END

                .SetCellStyle(i, sprDetailDef.SYUBETU_KB.ColNo, sLabel)

                'セルに値を設定

                '*****表示列*****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.CUST_ORD_NO.ColNo, dr.Item("CUST_ORD_NO").ToString())

                '20160615 tsunehira comment out
                '#If False Then 'START INOUE 進捗区分英語化 20151105
                '                .SetCellValue(i, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, dr.Item("OUTKA_STATE_KB_NM").ToString())
                '#Else
                '                If (Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage = LMB010C.MESSEGE_LANGUAGE_ENGLISH AndAlso _
                '                    Me._outka_state_kb_nm_en.ContainsKey(dr.Item("OUTKA_STATE_KB").ToString())) Then
                '                    '区分名設定
                '                    .SetCellValue(i, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, Me._outka_state_kb_nm_en(dr.Item("OUTKA_STATE_KB").ToString()))
                '                Else
                '                    .SetCellValue(i, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, dr.Item("OUTKA_STATE_KB_NM").ToString())
                '                End If
                '#End If 'END INOUE 進捗区分英語化 20151105  

                '20160615 tsunehira add
                .SetCellValue(i, sprDetailDef.OUTKA_STATE_KB_NM.ColNo, dr.Item("OUTKA_STATE_KB_NM").ToString())

                .SetCellValue(i, sprDetailDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.ARR_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                'START YANAI 要望番号748
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                'END YANAI 要望番号748
                .SetCellValue(i, sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_PKG_NB.ColNo, dr.Item("OUTKA_PKG_NB").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_TTL_NB.ColNo, dr.Item("OUTKA_TTL_NB").ToString())
                .SetCellValue(i, sprDetailDef.DEST_AD.ColNo, dr.Item("DEST_AD").ToString())
                .SetCellValue(i, sprDetailDef.DENP_NO.ColNo, dr.Item("DENP_NO").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, sprDetailDef.BIN_KB_NM.ColNo, dr.Item("BIN_KB_NM").ToString())            '2013.02.27 / Notes1897
                .SetCellValue(i, sprDetailDef.M_COUNT.ColNo, dr.Item("M_COUNT").ToString())
                '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
                .SetCellValue(i, sprDetailDef.BUYER_ORD_NO.ColNo, dr.Item("BUYER_ORD_NO").ToString())
                '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end
                .SetCellValue(i, sprDetailDef.WEB_OUTKA_NO_L.ColNo, dr.Item("WEB_OUTKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_NO_L.ColNo, dr.Item("OUTKA_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.SYUBETU_KB_NM.ColNo, dr.Item("SYUBETU_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.REMARK_UNSO.ColNo, dr.Item("REMARK_UNSO").ToString())     '要望番号1856対応　2013/02/21　本明
                'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
                .SetCellValue(i, sprDetailDef.LOT_NO_S.ColNo, dr.Item("LOT_NO_S").ToString())
                .SetCellValue(i, sprDetailDef.SERIAL_NO_S.ColNo, dr.Item("SERIAL_NO_S").ToString())
                'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
#If True Then   'ADD 2019/06/18 004870【LMS】IntegWeb入力のCOA情報をLMS出荷検索画面_COA添付の有無の追加
                .SetCellValue(i, sprDetailDef.COA_UMU.ColNo, dr.Item("COA_UMU").ToString())
#End If
                .SetCellValue(i, sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprDetailDef.TANTO_USER.ColNo, dr.Item("TANTO_USER").ToString())
                .SetCellValue(i, sprDetailDef.SYS_ENT_USER.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())
                .SetCellValue(i, sprDetailDef.LAST_PRINT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LAST_PRINT_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.LAST_PRINT_TIME.ColNo, Me.TimeFormatData(dr.Item("LAST_PRINT_TIME").ToString()))

#If True Then ' 出荷作業ステータス対応 20160704 added inoue
                .SetCellValue(i, sprDetailDef.WH_WORK_STATUS_NM.ColNo, dr.Item("WH_WORK_STATUS_NM").ToString())
#End If
                .SetCellValue(i, sprDetailDef.WH_TAB_STATUS_NM.ColNo, dr.Item("WH_TAB_STATUS_NM").ToString())
                '*****隠し列*****
                .SetCellValue(i, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, sprDetailDef.PICK_KB.ColNo, dr.Item("PICK_KB").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo, dr.Item("OUTKA_SASHIZU_PRT_YN").ToString())
                .SetCellValue(i, sprDetailDef.OUTOKA_KANRYO_YN.ColNo, dr.Item("OUTOKA_KANRYO_YN").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_KENPIN_YN.ColNo, dr.Item("OUTKA_KENPIN_YN").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprDetailDef.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, sprDetailDef.OUTKA_STATE_KB.ColNo, dr.Item("OUTKA_STATE_KB").ToString())
                .SetCellValue(i, sprDetailDef.UNSO_CD.ColNo, dr.Item("UNSO_CD").ToString())
                .SetCellValue(i, sprDetailDef.UNSO_BR_CD.ColNo, dr.Item("UNSO_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.UNSO_NO_L.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, sprDetailDef.UNSO_SYS_UPD_DATE.ColNo, dr.Item("UNSO_SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDetailDef.UNSO_SYS_UPD_TIME.ColNo, dr.Item("UNSO_SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, sprDetailDef.SEIQ_FIXED_FLAG.ColNo, dr.Item("SEIQ_FIXED_FLAG").ToString())
                .SetCellValue(i, sprDetailDef.S_COUNT.ColNo, dr.Item("S_COUNT").ToString())
                .SetCellValue(i, sprDetailDef.BACKLOG_NB.ColNo, dr.Item("BACKLOG_NB").ToString())
                .SetCellValue(i, sprDetailDef.BACKLOG_QT.ColNo, dr.Item("BACKLOG_QT").ToString())
                .SetCellValue(i, sprDetailDef.FURI_NO.ColNo, dr.Item("FURI_NO").ToString())
                .SetCellValue(i, sprDetailDef.NIHUDA_YN.ColNo, dr.Item("NIHUDA_YN").ToString())
                'START YANAI メモ②No.2
                .SetCellValue(i, sprDetailDef.SASZ_USER.ColNo, dr.Item("SASZ_USER").ToString())
                'END YANAI メモ②No.2
                'START YANAI 20120122 立会書印刷対応
                .SetCellValue(i, sprDetailDef.TACHIAI_FLG.ColNo, String.Empty)
                'END YANAI 20120122 立会書印刷対応
                '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 -- START --
                .SetCellValue(i, sprDetailDef.NHS_FLAG.ColNo, dr.Item("NHS_FLAG").ToString())
                '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 --  END  --
                'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
                .SetCellValue(i, sprDetailDef.MIN_ALCTD_NB.ColNo, dr.Item("MIN_ALCTD_NB").ToString())
                .SetCellValue(i, sprDetailDef.MIN_ALCTD_QT.ColNo, dr.Item("MIN_ALCTD_QT").ToString())
                'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                .SetCellValue(i, sprDetailDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
                .SetCellValue(i, sprDetailDef.TRIP_NO_SYUKA.ColNo, dr.Item("TRIP_NO_SYUKA").ToString())
                .SetCellValue(i, sprDetailDef.TRIP_NO_TYUKEI.ColNo, dr.Item("TRIP_NO_TYUKEI").ToString())
                .SetCellValue(i, sprDetailDef.TRIP_NO_HAIKA.ColNo, dr.Item("TRIP_NO_HAIKA").ToString())
                .SetCellValue(i, sprDetailDef.SHIHARAI_FIXED_FLAG.ColNo, dr.Item("SHIHARAI_FIXED_FLAG").ToString())
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  
                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加START
                .SetCellValue(i, sprDetailDef.CUST_DEST_CD.ColNo, dr.Item("CUST_DEST_CD").ToString())
                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加END

#If True Then ' 西濃自動送り状番号対応 added inoue
                .SetCellValue(i, sprDetailDef.AUTO_DENP_KBN.ColNo, dr.Item("AUTO_DENP_KBN").ToString())
                .SetCellValue(i, sprDetailDef.AUTO_DENP_NO.ColNo, dr.Item("AUTO_DENP_NO").ToString())
#End If
                .SetCellValue(i, sprDetailDef.SYUBETU_KB.ColNo, dr.Item("SYUBETU_KB").ToString())

                .SetCellValue(i, sprDetailDef.SHIKAKARI_HIN_FLG.ColNo, dr.Item("SHIKAKARI_HIN_FLG").ToString())
                .SetCellValue(i, sprDetailDef.ZFVYHKKBN.ColNo, dr.Item("ZFVYHKKBN").ToString())
                .SetCellValue(i, sprDetailDef.MATNR.ColNo, dr.Item("MATNR").ToString())
                .SetCellValue(i, sprDetailDef.ZFVYDENTYP.ColNo, dr.Item("ZFVYDENTYP").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added

    ''' <summary>
    ''' 行カラー取得
    ''' </summary>
    ''' <param name="colorType"></param>
    ''' <param name="showRow"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRowColor(ByVal colorType As String _
                               , ByVal showRow As DataRow) As Color

        Select Case colorType
            Case LMC010C.CustomizeRowColorType.FFEM

                Return Me.GetRowColorFFEM(showRow)
        End Select


        Return Color.Black

    End Function


    ''' <summary>
    ''' 表示する行カラーのカスタマイズ対象荷主か判定する。
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="custCdL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCustomizeRowColorCust(ByVal nrsBrCd As String _
                                          , ByVal custCdL As String) As Boolean

        Dim colorType As String = ""
        Return Me.IsCustomizeRowColorCustWithType(nrsBrCd, custCdL, colorType)


    End Function

    ''' <summary>
    ''' 表示する行カラーのカスタマイズ対象荷主か判定する。(設定タイプ取得付)
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="custCdL"></param>
    ''' <param name="colorType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsCustomizeRowColorCustWithType(ByVal nrsBrCd As String _
                                                  , ByVal custCdL As String _
                                                  , ByRef colorType As String) As Boolean

        colorType = ""
        If (String.IsNullOrEmpty(nrsBrCd) OrElse _
            String.IsNullOrEmpty(custCdL)) Then

            Return False
        End If
        Dim row As DataRow = _
            MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable() _
                .Where(Function(r) LMC010C.ROW_COLOR_TYPE_Z_KBN_GRP_CD.Equals(r.Item("KBN_GROUP_CD")) AndAlso _
                                   nrsBrCd.Equals(r.Item("KBN_NM1")) AndAlso _
                                   custCdL.Equals(r.Item("KBN_NM2"))).FirstOrDefault()

        If (row IsNot Nothing) Then

            colorType = TryCast(row.Item("KBN_NM3"), String)

            Return True

        End If

        Return False

    End Function


    ''' <summary>
    ''' 行カラー取得(FFEM)
    ''' </summary>
    ''' <param name="showRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRowColorFFEM(ByVal showRow As DataRow) As Color

        If (showRow IsNot Nothing) Then

            If (LMConst.FLG.OFF.Equals(showRow.Item("EXISTS_COA_FILE")) AndAlso _
                LMConst.FLG.OFF.Equals(showRow.Item("EXISTS_DLV_FILE"))) Then

                ' 納品書とCOAが共に添付されていない場合は[紫]

                Return Color.Purple

            ElseIf (LMConst.FLG.OFF.Equals(showRow.Item("EXISTS_COA_FILE"))) Then

                ' COAが添付されていない場合は[青]

                Return Color.Blue
            ElseIf (LMConst.FLG.OFF.Equals(showRow.Item("EXISTS_DLV_FILE"))) Then

                ' 納品書が添付されていない場合は[赤]
                Return Color.Red

            End If

        End If

        Return Color.Black

    End Function
#End If


    ''' <summary>
    ''' 時間(コロン)編集
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>コロンを含む8桁を返却　空の場合、そのまま返却</returns>
    ''' <remarks></remarks>
    Private Function TimeFormatData(ByVal value As String) As String

        '空の場合、そのまま返却
        If String.IsNullOrEmpty(value) = True Then
            Return value
        End If

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region 'Spread

#End Region

End Class
