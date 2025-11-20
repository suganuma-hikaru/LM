' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text.RegularExpressions
Imports Jp.Co.Nrs.Win.Utility
Imports GrapeCity.Win.Editors

''' <summary>
''' LMI190Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI190G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI190F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Const"

    ''' <summary>
    ''' モード：初期検索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_SHOKI As String = "00"

    ''' <summary>
    ''' モード：在庫検索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_ZAI As String = "01"

    ''' <summary>
    ''' モード：履歴検索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_RIREKI As String = "02"

    ''' <summary>
    ''' モード：廃棄検索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MODE_HAIKI As String = "03"

    '''' <summary>
    '''' モード：編集
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Const MODE_EDIT As String = "04"

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI190F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="modeFlg">00:初期設定、01:在庫検索、02:履歴検索、03:廃棄済検索、04:編集ボタン押下</param>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal modeFlg As String)

        Dim always As Boolean = True
        Dim edit1 As Boolean = False
        Dim edit2 As Boolean = False
        Dim edit3 As Boolean = False
        Dim edit4 As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            '--------猪熊--------------
            'ファンクションキー個別設定
            .F1ButtonName = LMIControlC.FUNCTION_GETDATA
            .F2ButtonName = String.Empty
            .F3ButtonName = "N40コード取込"
            .F4ButtonName = LMIControlC.FUNCTION_HENKHAKU_SHUKKA
            .F5ButtonName = String.Empty
            .F6ButtonName = LMIControlC.FUNCTION_HAIKI
            .F7ButtonName = LMIControlC.FUNCTION_HAIKI_KAIJO
            .F8ButtonName = LMIControlC.FUNCTION_TEIKIKENSA_KANRI
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            '--ポップアップ未使用---------------------------
            .F10ButtonName = String.Empty
            '-----------------------------------------------
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE
            '--------猪熊---------------

            If (MODE_SHOKI).Equals(modeFlg) = True Then
                '初期検索時
                edit1 = False
                edit2 = False
                edit3 = False
                edit4 = False
            ElseIf (MODE_ZAI).Equals(modeFlg) = True Then
                '在庫検索時
                edit1 = False
                edit2 = True
                edit3 = False
                edit4 = False
            ElseIf (MODE_RIREKI).Equals(modeFlg) = True Then
                '履歴検索時
                edit1 = True
                edit2 = False
                edit3 = True
                edit4 = False
            ElseIf (MODE_HAIKI).Equals(modeFlg) = True Then
                '廃棄済検索時
                edit1 = False
                edit2 = False
                edit3 = True
                edit4 = False
                'ElseIf (MODE_EDIT).Equals(modeFlg) = True Then
                '    '編集時
                '    edit1 = False
                '    edit2 = False
                '    edit3 = False
                '    edit4 = True
            End If
            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = always
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = edit3
            .F7ButtonEnabled = edit3
            .F8ButtonEnabled = edit2
            .F9ButtonEnabled = always
            '--ポップアップ未使用---------------------------
            '.F10ButtonEnabled = always
            .F10ButtonEnabled = lock
            '-----------------------------------------------
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "スプレッドの切り替え"

    ''' <summary>
    ''' Dispモードの設定
    ''' </summary>
    ''' <param name="modeFlg">00:初期設定、01:在庫検索、02:履歴検索、03:廃棄済検索、04:編集ボタン押下</param>
    ''' <remarks></remarks>
    Friend Sub SetSprChange(ByVal modeFlg As String)

        If (MODE_ZAI).Equals(modeFlg) = True Then
            'スプレッドを在庫用に切り替える
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.DEF.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.EMPTYKB.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CYLINDERTYPE.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMNM.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SERIALNO.ColNo).Visible = True
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMNMRIREKI.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.YOUKINO.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTDATE.ColNo).Visible = True
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.EMPTYKBRIREKI.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.NEXTTESTDATE.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEIKADATE1.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.IOZSKB.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.HAIKIYN.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SHIPCDL.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SHIPNML.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.BUYERORDNO.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMCD.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SEIQTO.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEISANSTARTDATE.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEIKADATE2.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CHIENDATE.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CHIENMONEY.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOL.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOM.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOS.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SYSUPDDATE.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SYSUPDTIME.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CUSTCDL.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.IOZSKBCD.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.PROD_DATE.ColNo).Visible = True      'ADD 2019/10/28 006786
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.GOODS_CD_CUST.ColNo).Visible = True  'ADD 2019/10/31 008262
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.GOODS_NM.ColNo).Visible = True       'ADD 2019/10/31 008262
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SEARCH_KEY_2.ColNo).Visible = True   'ADD 2019/12/10 009849
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.REMARK_IN.ColNo).Visible = False
        Else
            'スプレッドを履歴・廃棄済用に切り替える
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.DEF.ColNo).Visible = True
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.EMPTYKB.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.EMPTYKB.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CYLINDERTYPE.ColNo).Visible = False
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMNM.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMNM.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SERIALNO.ColNo).Visible = True
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMNMRIREKI.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.YOUKINO.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTDATE.ColNo).Visible = True
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.EMPTYKBRIREKI.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.NEXTTESTDATE.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEIKADATE1.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.IOZSKB.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.HAIKIYN.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SHIPCDL.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SHIPNML.ColNo).Visible = True
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.BUYERORDNO.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.TOFROMCD.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SEIQTO.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEISANSTARTDATE.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.KEIKADATE2.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CHIENDATE.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CHIENMONEY.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOL.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOM.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.INOUTKANOS.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SYSUPDDATE.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SYSUPDTIME.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.CUSTCDL.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.IOZSKBCD.ColNo).Visible = False
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.PROD_DATE.ColNo).Visible = False         'ADD 2019/10/28 006786
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.GOODS_CD_CUST.ColNo).Visible = False     'ADD 2019/10/31 008262 
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.GOODS_NM.ColNo).Visible = False          'ADD 2019/10/31 008262 
            Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.SEARCH_KEY_2.ColNo).Visible = False      'ADD 2019/12/10 009849
            'Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsAll.REMARK_IN.ColNo).Visible = False
        End If

    End Sub

    'Friend Sub SetSprChange()

    '    With Me._Frm

    '        Me._Frm.txtMode.TextValue = mode

    '        Select Case mode

    '            Case MODE_EDIT
    '                'スプレッドを編集モードにする
    '                Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsRireki.SHIPCDL.ColNo).Visible = False
    '                Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo).Visible = True

    '            Case Else
    '                'スプレッドを参照モードにする
    '                Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsRireki.SHIPCDL.ColNo).Visible = True
    '                Me._Frm.sprDetails.Sheets(0).Columns(LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo).Visible = False

    '        End Select

    '    End With

    'End Sub

#End Region 'スプレッドの切り替え

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpMode.TabIndex = LMI190C.CtlTabIndex.GRPMODE
            .optZaiko.TabIndex = LMI190C.CtlTabIndex.OPTZAIKO
            .optRireki.TabIndex = LMI190C.CtlTabIndex.OPTSHUKKA
            .optHaiki.TabIndex = LMI190C.CtlTabIndex.OPTHAIKI
            .cmbEigyo.TabIndex = LMI190C.CtlTabIndex.EIGYO
            .txtSerialNo.TabIndex = LMI190C.CtlTabIndex.SERIALNO
            .cmbCylinderType.TabIndex = LMI190C.CtlTabIndex.CYLINDERTYPE
            .cmbToFromNm.TabIndex = LMI190C.CtlTabIndex.TOFROMNM
            '2013.08.15 要望対応2095 START
            .cmbCoolantGoodsKb.TabIndex = LMI190C.CtlTabIndex.COOLANTGOODSKB
            '2013.08.15 要望対応2095 END
            .numKeikaDate.TabIndex = LMI190C.CtlTabIndex.KEIKADATE
            .imdKijunDate.TabIndex = LMI190C.CtlTabIndex.KIJUNDATE
            .imdIdoDateFrom.TabIndex = LMI190C.CtlTabIndex.IDODATEFROM
            .imdIdoDateTo.TabIndex = LMI190C.CtlTabIndex.IDODATETO
            .btnExcel.TabIndex = LMI190C.CtlTabIndex.BTNEXCEL
            .grpMiiri.TabIndex = LMI190C.CtlTabIndex.GRPMIIRI
            .optKara.TabIndex = LMI190C.CtlTabIndex.OPTKARA
            .optMiiri.TabIndex = LMI190C.CtlTabIndex.OPTMIIRI
            .optKaraMiiri.TabIndex = LMI190C.CtlTabIndex.OPTKARAMIIRI
            .grpInkaOutka.TabIndex = LMI190C.CtlTabIndex.GRPINKAOUTKA
            .optInka.TabIndex = LMI190C.CtlTabIndex.OPTINKA
            .optOutka.TabIndex = LMI190C.CtlTabIndex.OPTOUTKA
            .optInkaOutka.TabIndex = LMI190C.CtlTabIndex.OPTINKAOUTKA
            .imdChienDate.TabIndex = LMI190C.CtlTabIndex.CHIENDATE
            .sprDetails.TabIndex = LMI190C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定、初期値設定
    ''' </summary>
    ''' <param name="sysDate"></param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            'ラジオボタンの設定
            .optZaiko.Checked = True
            .optKaraMiiri.Checked = True
            .optInkaOutka.Checked = True

            '遅延金制度開始日の設定
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C014'"))
            If 0 < kbnDr.Length Then
                .imdChienDate.TextValue = kbnDr(0).Item("KBN_NM1").ToString
            End If

            '最終データ取得ログ
            .lblLogDate.Visible = False

            'ボタン設定
            .btnExcel.Enabled = False

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            If .optZaiko.Checked = True Then
                '在庫の場合
                .numKeikaDate.ReadOnly = False
                .imdKijunDate.ReadOnly = False
                .imdIdoDateFrom.ReadOnly = True
                .imdIdoDateTo.ReadOnly = True
                .grpMiiri.Enabled = True
                .grpInkaOutka.Enabled = True
                .imdChienDate.ReadOnly = False

                .imdIdoDateFrom.TextValue = String.Empty
                .imdIdoDateTo.TextValue = String.Empty
                '遅延金制度開始日の設定
                Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C014'"))
                If 0 < kbnDr.Length Then
                    .imdChienDate.TextValue = kbnDr(0).Item("KBN_NM1").ToString
                End If

                .btnExcel.Enabled = True

            ElseIf .optRireki.Checked = True Then
                '履歴の場合
                .numKeikaDate.ReadOnly = True
                .imdKijunDate.ReadOnly = True
                .imdIdoDateFrom.ReadOnly = False
                .imdIdoDateTo.ReadOnly = False
                .grpMiiri.Enabled = False
                .grpInkaOutka.Enabled = False
                .imdChienDate.ReadOnly = True

                .numKeikaDate.Value = 0
                .imdKijunDate.TextValue = String.Empty
                .imdChienDate.TextValue = String.Empty

                .btnExcel.Enabled = False

            ElseIf .optHaiki.Checked = True Then
                '廃棄済の場合
                .numKeikaDate.ReadOnly = True
                .imdKijunDate.ReadOnly = True
                .imdIdoDateFrom.ReadOnly = False
                .imdIdoDateTo.ReadOnly = False
                .grpMiiri.Enabled = False
                .grpInkaOutka.Enabled = False
                .imdChienDate.ReadOnly = True

                .numKeikaDate.Value = 0
                .imdKijunDate.TextValue = String.Empty
                .imdChienDate.TextValue = String.Empty

                .btnExcel.Enabled = False

            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .txtSerialNo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._Frm

            Dim d4 As Decimal = Convert.ToDecimal(LMI190C.NB_MAX_4)
            Dim sharp4 As String = "#,##0"

            .numKeikaDate.SetInputFields(sharp4, , 4, 1, , 0, 0, , d4, 0)

        End With

    End Sub

    ''' <summary>
    ''' シリンダタイプコンボの作成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateCylinderCombo(ByVal ds As DataSet)

        With Me._Frm

            Dim max As Integer = ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count - 1

            'シリンダタイプコンボの作成
            .cmbCylinderType.SelectedValue() = ""
            .cmbCylinderType.Items.Clear()

            .cmbCylinderType.Items.Add(New ListItem(New SubItem() {New SubItem(String.Empty),
                                                                   New SubItem(String.Empty)}))
            For i As Integer = 0 To max
                .cmbCylinderType.Items.Add(New ListItem(New SubItem() {New SubItem(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString),
                                                                       New SubItem(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString)}))

            Next

        End With

    End Sub

    ''' <summary>
    ''' 在庫場所コンボの作成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateToFromNmCombo(ByVal ds As DataSet)

        With Me._Frm

            Dim max As Integer = ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count - 1

            '在庫場所コンボの作成
            .cmbToFromNm.SelectedValue() = ""
            .cmbToFromNm.Items.Clear()

            .cmbToFromNm.Items.Add(New ListItem(New SubItem() {New SubItem(String.Empty),
                                                               New SubItem(String.Empty)}))
            For i As Integer = 0 To max
                .cmbToFromNm.Items.Add(New ListItem(New SubItem() {New SubItem(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString),
                                                                   New SubItem(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString)}))

            Next

        End With

    End Sub

    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal frm As LMI190F)

        Dim selectCmbValue As String = frm.cmbEditList.SelectedValue.ToString

        frm.txtShipCd.Visible = False
        frm.txtShipNm.Visible = False

        Select Case selectCmbValue
            Case LMI190C.EDIT_SELECT_SHIPCD '荷送人コード
                With frm.txtShipCd
                    .Visible = True
                    .MaxLength = 15
                    .InputType = Com.Const.InputControl.HAN_NUM_ALPHA
                End With

            Case LMI190C.EDIT_SELECT_SHIPNM '荷送人名
                With frm.txtShipNm
                    .Visible = True
                    .MaxLength = 80
                    .InputType = Com.Const.InputControl.ALL_MIX
                End With

        End Select

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailsAll

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.DEF, " ", 20, True)
        Public Shared EMPTYKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.EMPTYKB, "空・実入り", 100, True)
        Public Shared CYLINDERTYPE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.CYLINDERTYPE, "タイプ", 70, True)
        Public Shared TOFROMNM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.TOFROMNM, "在庫場所", 150, True)
        Public Shared SERIALNO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SERIALNO, "シリンダ番号", 100, True)
        'Public Shared TOFROMNMRIREKI As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.TOFROMNMRIREKI, "在庫場所", 150, True)
        Public Shared YOUKINO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.YOUKINO, "変換後", 100, True)
        Public Shared INOUTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.INOUTDATE, "移動日", 80, True)
        'Public Shared EMPTYKBRIREKI As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.EMPTYKBRIREKI, "空・実入り", 100, True)
        Public Shared NEXTTESTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.NEXTTESTDATE, "次回定検日", 80, True)
        Public Shared PROD_DATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.PROD_DATE, "製造日", 80, True)   'ADD 2019/10/29 006786
        Public Shared KEIKADATE1 As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.KEIKADATE1, "経過日数", 80, True)
        Public Shared IOZSKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.IOZSKB, "IN/OUT", 70, True)
        Public Shared HAIKIYN As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.HAIKIYN, "廃棄", 70, True)
        Public Shared SHIPCDL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SHIPCDL, "荷送人コード", 90, True)
        Public Shared SHIPNML As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SHIPNML, "荷送人", 130, True)
        Public Shared BUYERORDNO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.BUYERORDNO, "注文番号", 130, True)
        Public Shared TOFROMCD As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.TOFROMCD, String.Concat("在庫場所", vbCrLf, "コード"), 90, True)
        Public Shared SEIQTO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SEIQTO, "請求先", 130, True)
        Public Shared KEISANSTARTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.KEISANSTARTDATE, "計算開始日", 80, True)
        Public Shared KEIKADATE2 As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.KEIKADATE2, "経過日数", 80, True)
        Public Shared CHIENDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.CHIENDATE, "遅延日数", 80, True)
        Public Shared CHIENMONEY As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.CHIENMONEY, "遅延金", 80, True)
        Public Shared INOUTKANOL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.INOUTKANOL, "出荷管理番号(大)", 0, False)
        Public Shared INOUTKANOM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.INOUTKANOM, "出荷管理番号(中)", 0, False)
        Public Shared INOUTKANOS As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.INOUTKANOS, "出荷管理番号(小)", 0, False)
        Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SYSUPDDATE, "更新日", 0, False)
        Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SYSUPDTIME, "更新時間", 0, False)
        Public Shared CUSTCDL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.CUSTCDL, "荷主コード", 0, False)
        Public Shared IOZSKBCD As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.IOZSKBCD, "IN/OUT", 0, False)
#If True Then   'ADD 2019/10/31 008262【LMS】データ抽出_ハネウェル管理_商品コード・商品名を追加出力
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.GOODS_CD_CUST, "商品コード", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.GOODS_NM, "商品名", 150, True)
#End If
        Public Shared SEARCH_KEY_2 As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.SEARCH_KEY_2, "荷主カテゴリ2", 140, True)    'ADD 2019/12/10 009849
        Public Shared REMARK_IN As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexSPRALL.REMARK_IN, "備考小(荷主)", 0, False)

    End Class

    '''' <summary>
    '''' スプレッド列定義体(在庫検索の場合)
    '''' </summary>
    '''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    'Public Class sprDetailsZaiko

    '    'スプレッド(タイトル列)の設定
    '    Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.DEF, " ", 20, True)
    '    Public Shared EMPTYKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.EMPTYKB, "空・実入り", 100, True)
    '    Public Shared CYLINDERTYPE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.CYLINDERTYPE, "タイプ", 70, True)
    '    Public Shared TOFROMNM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.TOFROMNM, "在庫場所", 150, True)
    '    Public Shared SERIALNO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SERIALNO, "シリンダ番号", 100, True)
    '    Public Shared YOUKINO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.YOUKINO, "変換後", 100, True)
    '    Public Shared INOUTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.INOUTDATE, "移動日", 80, True)
    '    Public Shared NEXTTESTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.NEXTTESTDATE, "次回定検日", 80, True)
    '    Public Shared KEIKADATE1 As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.KEIKADATE1, "経過日数", 80, True)
    '    Public Shared IOZSKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.IOZSKB, "IN/OUT", 70, True)
    '    Public Shared SHIPCDL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SHIPCDL, "荷送人コード", 90, True)
    '    Public Shared SHIPNML As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SHIPNML, "荷送人", 130, True)
    '    Public Shared BUYERORDNO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.BUYERORDNO, "注文番号", 130, True)
    '    Public Shared TOFROMCD As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.TOFROMCD, String.Concat("在庫場所", vbCrLf, "コード"), 90, True)
    '    Public Shared SEIQTO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SEIQTO, "請求先", 130, True)
    '    Public Shared KEISANSTARTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.KEISANSTARTDATE, "計算開始日", 80, True)
    '    Public Shared KEIKADATE2 As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.KEIKADATE2, "経過日数", 80, True)
    '    Public Shared CHIENDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.CHIENDATE, "遅延日数", 80, True)
    '    Public Shared CHIENMONEY As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.CHIENMONEY, "遅延金", 80, True)
    '    Public Shared INOUTKANOL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.INOUTKANOL, "出荷管理番号(大)", 0, False)
    '    Public Shared INOUTKANOM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.INOUTKANOM, "出荷管理番号(中)", 0, False)
    '    Public Shared INOUTKANOS As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.INOUTKANOS, "出荷管理番号(小)", 0, False)
    '    Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SYSUPDDATE, "更新日", 0, False)
    '    Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.SYSUPDTIME, "更新時間", 0, False)
    '    Public Shared IOZSKBCD As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexZAIKO.IOZSKBCD, "IN/OUT", 0, False)

    'End Class

    '''' <summary>
    '''' スプレッド列定義体(履歴検索の場合)
    '''' </summary>
    '''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    'Public Class sprDetailsRireki

    '    'スプレッド(タイトル列)の設定
    '    Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.DEF, " ", 20, True)
    '    Public Shared SERIALNO As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SERIALNO, "シリンダ番号", 100, True)
    '    Public Shared TOFROMNM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.TOFROMNM, "在庫場所", 150, True)
    '    Public Shared INOUTDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.INOUTDATE, "移動日", 80, True)
    '    Public Shared EMPTYKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.EMPTYKB, "空・実入り", 100, True)
    '    Public Shared IOZSKB As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.IOZSKB, "IN/OUT", 70, True)
    '    Public Shared HAIKIYN As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.HAIKIYN, "廃棄", 70, True)
    '    Public Shared SHIPCDL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SHIPCDL, "荷送人コード", 90, True)
    '    Public Shared SHIPCDL_EDIT As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SHIPCDL_EDIT, "荷送人コード", 90, False) '編集モード時のみ表示
    '    Public Shared SHIPNML As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SHIPNML, "荷送人", 130, True)
    '    Public Shared INOUTKANOL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.INOUTKANOL, "出荷管理番号(大)", 0, False)
    '    Public Shared INOUTKANOM As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.INOUTKANOM, "出荷管理番号(中)", 0, False)
    '    Public Shared INOUTKANOS As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.INOUTKANOS, "出荷管理番号(小)", 0, False)
    '    Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SYSUPDDATE, "更新日", 0, False)
    '    Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.SYSUPDTIME, "更新時間", 0, False)
    '    Public Shared CUSTCDL As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.CUSTCDL, "荷主コード", 0, False)
    '    Public Shared IOZSKBCD As SpreadColProperty = New SpreadColProperty(LMI190C.SprColumnIndexRIREKI_HAIKI.IOZSKBCD, "IN/OUT", 0, False)

    'End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

            '列数設定
            .sprDetails.Sheets(0).ColumnCount = LMI190C.SprColumnIndexSPRALL.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetails.SetColProperty(New sprDetailsAll)

            Me.SetSprChange(LMI190G.MODE_ZAI)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadAll(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal sysDate As String, ByVal modeflg As String)

        Dim max As Integer = ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count - 1
        Dim spdstr As String = String.Empty
        'Dim kbnDr() As DataRow = Nothing
        Dim kbnDr2() As DataRow = Nothing
        Dim serialNo As String = String.Empty
        Dim keisanStartDate As String = String.Empty
        Dim keikaDate As String = String.Empty
        Dim chienDate As String = String.Empty
        Dim keikaDate2 As String = String.Empty
        Dim chienDate2 As String = String.Empty
        Dim kbnDr As String = Nothing

        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .SuspendLayout()

            '列数設定
            .Sheets(0).ColumnCount = LMI190C.SprColumnIndexSPRALL.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDetailsAll)

            '列設定
            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)
            Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)

            Dim rowCnt As Integer = 0

            If (MODE_ZAI).Equals(modeflg) = True Then

                For i As Integer = 0 To max

                    If (serialNo).Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()) = True AndAlso
                        i <> 0 Then
                        Continue For
                    End If
                    serialNo = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()

                    '設定する行数を設定
                    rowCnt = .ActiveSheet.Rows.Count

                    '行追加
                    .ActiveSheet.AddRows(rowCnt, 1)

                    'セルスタイル設定
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.DEF.ColNo, sCheck)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.EMPTYKB.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CYLINDERTYPE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMNM.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SERIALNO.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMNMRIREKI.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.YOUKINO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTDATE.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.EMPTYKBRIREKI.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.NEXTTESTDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEIKADATE1.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKB.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.HAIKIYN.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPCDL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPNML.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.BUYERORDNO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMCD.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SEIQTO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEISANSTARTDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEIKADATE2.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CHIENDATE.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CHIENMONEY.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOM.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOS.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CUSTCDL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKBCD.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.PROD_DATE.ColNo, sLabel)        'ADD 2019/10/28 006786
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.GOODS_CD_CUST.ColNo, sLabel)    'ADD 2019/10/31 008262
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.GOODS_NM.ColNo, sLabel)         'ADD 2019/10/31 008262
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SEARCH_KEY_2.ColNo, sLabel)     'ADD 2019/12/10 009849
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.REMARK_IN.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(rowCnt, sprDetailsAll.DEF.ColNo, "False")
                    .SetCellValue(rowCnt, sprDetailsAll.EMPTYKB.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.CYLINDERTYPE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.TOFROMNM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SERIALNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())
                    '.SetCellValue(rowCnt, sprDetailsAll.TOFROMNMRIREKI.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())

                    If _Frm.cmbCoolantGoodsKb.SelectedValue.ToString() = "06" Then
                        '冷媒商品「N40」の場合
                        If String.IsNullOrEmpty(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()) = True Then
                            spdstr = "該当無し"
                        ElseIf String.IsNullOrEmpty(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("N40_CYLINDER_NO").ToString()) = False Then
                            spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("N40_CYLINDER_NO").ToString()
                        Else
                            spdstr = "該当無し"
                        End If
                    Else
                        '2013.09.18 修正START
                        If String.IsNullOrEmpty(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()) = True Then
                            spdstr = "該当無し"
                        ElseIf Me.IsNumericChk(Mid(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString(), 1, 1)) = False Then
                            '2013.09.18 修正END
                            spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()
                        ElseIf String.IsNullOrEmpty(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString()) = False Then
                            spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString() & Mid(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString(), 4, 5)
                        Else
                            spdstr = "該当無し"
                        End If
                    End If

                    .SetCellValue(rowCnt, sprDetailsAll.YOUKINO.ColNo, spdstr)


                    .SetCellValue(rowCnt, sprDetailsAll.INOUTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
                    .SetCellValue(rowCnt, sprDetailsAll.PROD_DATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("PROD_DATE").ToString()))     'ADD 2019/10/28 006786
                    '.SetCellValue(rowCnt, sprDetailsAll.EMPTYKBRIREKI.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.NEXTTESTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("NEXT_TEST_DATE").ToString()))

                    '経過日数を求めて、設定する
                    'spdstr = Convert.ToString(DateDiff(DateInterval.Day, _
                    '                                   Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString())), _
                    '                                   Convert.ToDateTime(DateFormatUtility.EditSlash(sysDate))))
                    '.SetCellValue(rowCnt, sprDetailsAll.KEIKADATE1.ColNo, spdstr)
                    .SetCellValue(rowCnt, sprDetailsAll.KEIKADATE1.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("KEIKA_DATE1").ToString())

                    'IN/OUT
                    'If ("10").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString()) = True Then
                    '    spdstr = "IN"
                    'Else
                    '    spdstr = "OUT"
                    'End If
                    '.SetCellValue(rowCnt, sprDetailsAll.IOZSKB.ColNo, spdstr)
                    .SetCellValue(rowCnt, sprDetailsAll.IOZSKB.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KBNM").ToString())
                    '廃棄
                    'If ("1").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("HAIKI_YN").ToString()) = True Then
                    '    spdstr = "廃棄"
                    'Else
                    '    spdstr = String.Empty
                    'End If
                    '.SetCellValue(rowCnt, sprDetailsAll.HAIKIYN.ColNo, spdstr)
                    .SetCellValue(rowCnt, sprDetailsAll.HAIKIYN.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("HAIKI_YNNM").ToString())

                    .SetCellValue(rowCnt, sprDetailsAll.SHIPCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SHIPNML.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.BUYERORDNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.TOFROMCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.GOODS_CD_CUST.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("GOODS_CD_CUST").ToString())      'ADD 2019/10/31 008362
                    .SetCellValue(rowCnt, sprDetailsAll.GOODS_NM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("GOODS_NM").ToString())                'ADD 2019/10/31 008362
                    .SetCellValue(rowCnt, sprDetailsAll.SEARCH_KEY_2.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SEARCH_KEY_2").ToString())        'ADD 2019/12/10 009849
                    .SetCellValue(rowCnt, sprDetailsAll.REMARK_IN.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("REMARK_IN").ToString())

                    '請求先
                    'spdstr = String.Empty
                    'kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S083' AND ", _
                    '                                                                               "(KBN_NM1 LIKE '%", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString(), "%' OR ", _
                    '                                                                               " KBN_NM1 LIKE '%", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString(), "%')"))
                    'If kbnDr.Length > 0 Then
                    '    If ("00").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True Then
                    '        spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString()
                    '    ElseIf ("01").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True Then
                    '        spdstr = kbnDr(0).Item("KBN_NM1").ToString
                    '    End If
                    'End If
                    '.SetCellValue(rowCnt, sprDetailsAll.SEIQTO.ColNo, spdstr)
                    .SetCellValue(rowCnt, sprDetailsAll.SEIQTO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SEIQTO").ToString())


                    '計算開始日
                    'If ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString() < Me._Frm.imdChienDate.TextValue Then
                    '    keisanStartDate = Me._Frm.imdChienDate.TextValue
                    'Else
                    '    keisanStartDate = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()
                    'End If
                    '.SetCellValue(rowCnt, sprDetailsAll.KEISANSTARTDATE.ColNo, DateFormatUtility.EditSlash(keisanStartDate))
                    .SetCellValue(rowCnt, sprDetailsAll.KEISANSTARTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("KEISANSTART_DATE").ToString()))


                    '経過日数を求めて、設定する
                    'keikaDate = Convert.ToString(DateDiff(DateInterval.Day, _
                    '                                      Convert.ToDateTime(DateFormatUtility.EditSlash(keisanStartDate)), _
                    '                                      Convert.ToDateTime(DateFormatUtility.EditSlash(Me._Frm.imdKijunDate.TextValue))))
                    '.SetCellValue(rowCnt, sprDetailsAll.KEIKADATE2.ColNo, keikaDate)
                    .SetCellValue(rowCnt, sprDetailsAll.KEIKADATE2.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("KEIKA_DATE2").ToString())


                    '遅延日数を求めて、設定する
                    'kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C015' AND ", _
                    '                                                                               "KBN_NM1 = '", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString(), "'"))
                    'kbnDr2 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C016'")
                    'If kbnDr.Length > 0 Then
                    '    '①
                    '    If kbnDr2.Length > 0 Then
                    '        chienDate = Convert.ToString(Convert.ToInt32(keikaDate) - Convert.ToInt32(kbnDr2(0).Item("KBN_NM1").ToString))
                    '    End If
                    'Else
                    '    '②
                    '    If kbnDr2.Length > 0 Then
                    '        chienDate = Convert.ToString(Convert.ToInt32(keikaDate) - Convert.ToInt32(kbnDr2(0).Item("KBN_NM2").ToString))
                    '    End If
                    'End If
                    keikaDate2 = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("KEIKA_DATE2").ToString()
                    chienDate = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CHIEN_DATE").ToString()

                    chienDate2 = Convert.ToString(Convert.ToInt32(keikaDate2) - Convert.ToInt32(chienDate))

                    If Convert.ToInt32(chienDate2) < 0 Then
                        chienDate2 = "0"
                    End If
                    .SetCellValue(rowCnt, sprDetailsAll.CHIENDATE.ColNo, chienDate2)

                    '遅延金を求めて、設定する
                    'If ("0").Equals(chienDate) = True Then
                    '    '①
                    '    spdstr = String.Empty
                    'Else
                    '    kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C017' AND ", _
                    '                                                                                   "KBN_NM1 = '", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString(), "'"))
                    '    kbnDr2 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C018'")

                    '    If kbnDr.Length = 0 Then
                    '        spdstr = "0"
                    '    ElseIf ("01").Equals(kbnDr(0).Item("KBN_CD").ToString) = True OrElse _
                    '        ("02").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
                    '        If Convert.ToInt32(chienDate) <= 365 Then
                    '            '②
                    '            spdstr = kbnDr2(0).Item("KBN_NM1").ToString
                    '        Else
                    '            '③
                    '            spdstr = kbnDr2(0).Item("KBN_NM2").ToString
                    '        End If
                    '    ElseIf ("03").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
                    '        '④
                    '        spdstr = Convert.ToString(Convert.ToInt32(chienDate) * Convert.ToInt32(kbnDr2(0).Item("KBN_NM3").ToString))
                    '    ElseIf ("04").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
                    '        '⑤
                    '        spdstr = Convert.ToString(Convert.ToInt32(chienDate) * Convert.ToInt32(kbnDr2(0).Item("KBN_NM4").ToString))
                    '    End If
                    'End If
                    If ("0").Equals(chienDate2) = True Then
                        '①
                        spdstr = "0"
                    Else
                        kbnDr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CHIEN_KBCD").ToString()

                        If ("01").Equals(kbnDr) = True OrElse
                        ("02").Equals(kbnDr) = True Then

                            If Convert.ToInt32(chienDate2) <= 365 Then
                                '②
                                spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CHIEN_MONEY").ToString()
                            Else
                                '③
                                spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CHIEN_MONEY2").ToString()
                            End If
                        Else
                            '④⑤
                            spdstr = Convert.ToString(Convert.ToInt32(chienDate2) * Convert.ToInt32(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CHIEN_MONEY").ToString()))
                        End If

                    End If
                    .SetCellValue(rowCnt, sprDetailsAll.CHIENMONEY.ColNo, spdstr)

                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_M").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOS.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_S").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SYSUPDDATE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SYSUPDTIME.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.CUSTCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.IOZSKBCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString())

                Next

                .ResumeLayout(True)

            Else
                For i As Integer = 0 To max

                    '設定する行数を設定
                    rowCnt = .ActiveSheet.Rows.Count

                    '行追加
                    .ActiveSheet.AddRows(rowCnt, 1)

                    'セルスタイル設定
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.DEF.ColNo, sCheck)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SERIALNO.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMNM.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTDATE.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.EMPTYKB.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKB.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.HAIKIYN.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPCDL.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPNML.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOM.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOS.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CUSTCDL.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKBCD.ColNo, sLabel)

                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.DEF.ColNo, sCheck)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.EMPTYKB.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CYLINDERTYPE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMNM.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SERIALNO.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMNMRIREKI.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.YOUKINO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTDATE.ColNo, sLabel)
                    '.SetCellStyle(rowCnt, LMI190G.sprDetailsAll.EMPTYKBRIREKI.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.NEXTTESTDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEIKADATE1.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKB.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.HAIKIYN.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPCDL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SHIPNML.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.BUYERORDNO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.TOFROMCD.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SEIQTO.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEISANSTARTDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.KEIKADATE2.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CHIENDATE.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CHIENMONEY.ColNo, sNumMax)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOM.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.INOUTKANOS.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.CUSTCDL.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.IOZSKBCD.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMI190G.sprDetailsAll.PROD_DATE.ColNo, sLabel)        'ADD 2019/10/28 006786

                    'セルに値を設定
                    .SetCellValue(rowCnt, sprDetailsAll.DEF.ColNo, "False")
                    .SetCellValue(rowCnt, sprDetailsAll.EMPTYKB.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.CYLINDERTYPE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.TOFROMNM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SERIALNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())
                    '.SetCellValue(rowCnt, sprDetailsAll.TOFROMNMRIREKI.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.YOUKINO.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
                    '.SetCellValue(rowCnt, sprDetailsAll.EMPTYKBRIREKI.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.NEXTTESTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("NEXT_TEST_DATE").ToString()))
                    .SetCellValue(rowCnt, sprDetailsAll.KEIKADATE1.ColNo, String.Empty)
                    'IN/OUT
                    If ("10").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString()) = True Then
                        spdstr = "IN"
                    Else
                        spdstr = "OUT"
                    End If
                    .SetCellValue(rowCnt, sprDetailsAll.IOZSKB.ColNo, spdstr)
                    '廃棄
                    If ("1").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("HAIKI_YN").ToString()) = True Then
                        spdstr = "廃棄"
                    Else
                        spdstr = String.Empty
                    End If
                    .SetCellValue(rowCnt, sprDetailsAll.HAIKIYN.ColNo, spdstr)
                    .SetCellValue(rowCnt, sprDetailsAll.SHIPCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SHIPNML.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.BUYERORDNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.TOFROMCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SEIQTO.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.KEISANSTARTDATE.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.KEIKADATE2.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.CHIENDATE.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.CHIENMONEY.ColNo, String.Empty)
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_M").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.INOUTKANOS.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_S").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SYSUPDDATE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.SYSUPDTIME.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.CUSTCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_L").ToString())
                    .SetCellValue(rowCnt, sprDetailsAll.IOZSKBCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString())

                Next

                .ResumeLayout(True)

            End If

        End With

    End Sub

    '''' <summary>
    '''' スプレッドにデータを設定（在庫）
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetSpreadZaiko(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal sysDate As String)

    '    Dim max As Integer = ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count - 1
    '    Dim spdstr As String = String.Empty
    '    Dim kbnDr() As DataRow = Nothing
    '    Dim kbnDr2() As DataRow = Nothing
    '    Dim serialNo As String = String.Empty
    '    Dim keisanStartDate As String = String.Empty
    '    Dim keikaDate As String = String.Empty
    '    Dim chienDate As String = String.Empty

    '    With spr

    '        'スプレッドの行をクリア
    '        .CrearSpread()
    '        .SuspendLayout()

    '        '列数設定
    '        .Sheets(0).ColumnCount = LMI190C.SprColumnIndexZAIKO.LAST

    '        'スプレッドの列設定（列名、列幅、列の表示・非表示）
    '        .SetColProperty(New sprDetailsZaiko)

    '        '列設定
    '        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
    '        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
    '        Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)
    '        Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)

    '        Dim rowCnt As Integer = 0

    '        For i As Integer = 0 To max

    '            If (serialNo).Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()) = True AndAlso _
    '                i <> 0 Then
    '                Continue For
    '            End If
    '            serialNo = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()

    '            '設定する行数を設定
    '            rowCnt = .ActiveSheet.Rows.Count

    '            '行追加
    '            .ActiveSheet.AddRows(rowCnt, 1)

    '            'セルスタイル設定
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.DEF.ColNo, sCheck)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.EMPTYKB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.CYLINDERTYPE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.TOFROMNM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SERIALNO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.YOUKINO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.INOUTDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.NEXTTESTDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.KEIKADATE1.ColNo, sNumMax)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.IOZSKB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SHIPCDL.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SHIPNML.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.BUYERORDNO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.TOFROMCD.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SEIQTO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.KEISANSTARTDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.KEIKADATE2.ColNo, sNumMax)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.CHIENDATE.ColNo, sNumMax)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.CHIENMONEY.ColNo, sNumMax)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.INOUTKANOL.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.INOUTKANOM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.INOUTKANOS.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SYSUPDDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.SYSUPDTIME.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsZaiko.IOZSKBCD.ColNo, sLabel)

    '            'セルに値を設定
    '            .SetCellValue(rowCnt, sprDetailsZaiko.DEF.ColNo, "False")
    '            .SetCellValue(rowCnt, sprDetailsZaiko.EMPTYKB.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.CYLINDERTYPE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.TOFROMNM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.SERIALNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())

    '            If Me.IsNumericChk(Mid(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString(), 1, 1)) = False Then
    '                spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()
    '            ElseIf String.IsNullOrEmpty(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString()) = False Then
    '                spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("YOUKI_NO").ToString()
    '            Else
    '                spdstr = "該当無し"
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.YOUKINO.ColNo, spdstr)


    '            .SetCellValue(rowCnt, sprDetailsZaiko.INOUTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
    '            .SetCellValue(rowCnt, sprDetailsZaiko.NEXTTESTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("NEXT_TEST_DATE").ToString()))

    '            '経過日数を求めて、設定する
    '            spdstr = Convert.ToString(DateDiff(DateInterval.Day, _
    '                                               Convert.ToDateTime(DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString())), _
    '                                               Convert.ToDateTime(DateFormatUtility.EditSlash(sysDate))))
    '            .SetCellValue(rowCnt, sprDetailsZaiko.KEIKADATE1.ColNo, spdstr)

    '            'IN/OUT
    '            If ("10").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString()) = True Then
    '                spdstr = "IN"
    '            Else
    '                spdstr = "OUT"
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.IOZSKB.ColNo, spdstr)


    '            .SetCellValue(rowCnt, sprDetailsZaiko.SHIPCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.SHIPNML.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.BUYERORDNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("BUYER_ORD_NO_DTL").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.TOFROMCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_CD").ToString())

    '            '請求先
    '            spdstr = String.Empty
    '            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S083' AND ", _
    '                                                                                           "(KBN_NM1 LIKE '%", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString(), "%' OR ", _
    '                                                                                           " KBN_NM1 LIKE '%", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString(), "%')"))
    '            If kbnDr.Length > 0 Then
    '                If ("00").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True Then
    '                    spdstr = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString()
    '                ElseIf ("01").Equals(kbnDr(0).Item("KBN_NM2").ToString) = True Then
    '                    spdstr = kbnDr(0).Item("KBN_NM1").ToString
    '                End If
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.SEIQTO.ColNo, spdstr)


    '            '計算開始日
    '            If ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString() < Me._Frm.imdChienDate.TextValue Then
    '                keisanStartDate = Me._Frm.imdChienDate.TextValue
    '            Else
    '                keisanStartDate = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.KEISANSTARTDATE.ColNo, DateFormatUtility.EditSlash(keisanStartDate))


    '            '経過日数を求めて、設定する
    '            keikaDate = Convert.ToString(DateDiff(DateInterval.Day, _
    '                                                  Convert.ToDateTime(DateFormatUtility.EditSlash(keisanStartDate)), _
    '                                                  Convert.ToDateTime(DateFormatUtility.EditSlash(Me._Frm.imdKijunDate.TextValue))))
    '            .SetCellValue(rowCnt, sprDetailsZaiko.KEIKADATE2.ColNo, keikaDate)


    '            '遅延日数を求めて、設定する
    '            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C015' AND ", _
    '                                                                                           "KBN_NM1 = '", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString(), "'"))
    '            kbnDr2 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C016'")
    '            If kbnDr.Length > 0 Then
    '                '①
    '                If kbnDr2.Length > 0 Then
    '                    chienDate = Convert.ToString(Convert.ToInt32(keikaDate) - Convert.ToInt32(kbnDr2(0).Item("KBN_NM1").ToString))
    '                End If
    '            Else
    '                '②
    '                If kbnDr2.Length > 0 Then
    '                    chienDate = Convert.ToString(Convert.ToInt32(keikaDate) - Convert.ToInt32(kbnDr2(0).Item("KBN_NM2").ToString))
    '                End If
    '            End If
    '            If Convert.ToInt32(chienDate) < 0 Then
    '                chienDate = "0"
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.CHIENDATE.ColNo, chienDate)

    '            '遅延金を求めて、設定する
    '            If ("0").Equals(chienDate) = True Then
    '                '①
    '                spdstr = String.Empty
    '            Else
    '                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C017' AND ", _
    '                                                                                               "KBN_NM1 = '", ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CYLINDER_TYPE").ToString(), "'"))
    '                kbnDr2 = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'C018'")

    '                If kbnDr.Length = 0 Then
    '                    spdstr = "0"
    '                ElseIf ("01").Equals(kbnDr(0).Item("KBN_CD").ToString) = True OrElse _
    '                    ("02").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
    '                    If Convert.ToInt32(chienDate) <= 365 Then
    '                        '②
    '                        spdstr = kbnDr2(0).Item("KBN_NM1").ToString
    '                    Else
    '                        '③
    '                        spdstr = kbnDr2(0).Item("KBN_NM2").ToString
    '                    End If
    '                ElseIf ("03").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
    '                    '④
    '                    spdstr = Convert.ToString(Convert.ToInt32(chienDate) * Convert.ToInt32(kbnDr2(0).Item("KBN_NM3").ToString))
    '                ElseIf ("04").Equals(kbnDr(0).Item("KBN_CD").ToString) = True Then
    '                    '⑤
    '                    spdstr = Convert.ToString(Convert.ToInt32(chienDate) * Convert.ToInt32(kbnDr2(0).Item("KBN_NM4").ToString))
    '                End If
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsZaiko.CHIENMONEY.ColNo, spdstr)

    '            .SetCellValue(rowCnt, sprDetailsZaiko.INOUTKANOL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.INOUTKANOM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_M").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.INOUTKANOS.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_S").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.SYSUPDDATE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_DATE").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.SYSUPDTIME.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_TIME").ToString())
    '            .SetCellValue(rowCnt, sprDetailsZaiko.IOZSKBCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString())

    '        Next

    '        .ResumeLayout(True)

    '    End With

    'End Sub

    '''' <summary>
    '''' スプレッドにデータを設定（履歴・廃棄済）
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetSpreadRirekiHaiki(ByVal spr As LMSpread, ByVal ds As DataSet)

    '    Dim max As Integer = ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count - 1
    '    Dim spdstr As String = String.Empty
    '    'Dim serialNo As String = String.Empty

    '    With spr

    '        'スプレッドの行をクリア
    '        .CrearSpread()
    '        .SuspendLayout()

    '        '列数設定
    '        .Sheets(0).ColumnCount = LMI190C.SprColumnIndexRIREKI_HAIKI.LAST

    '        'スプレッドの列設定（列名、列幅、列の表示・非表示）
    '        .SetColProperty(New sprDetailsRireki)

    '        '列設定
    '        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
    '        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
    '        Dim sLabelRight As StyleInfo = Me.StyleInfoLabelRight(spr)
    '        Dim sTextBox As StyleInfo = Me.StyleInfoTextHankaku(spr, 15, False)

    '        Dim rowCnt As Integer = 0

    '        For i As Integer = 0 To max

    '            'If (serialNo).Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()) = True AndAlso _
    '            '    i <> 0 Then
    '            '    Continue For
    '            'End If
    '            'serialNo = ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString()

    '            '設定する行数を設定
    '            rowCnt = .ActiveSheet.Rows.Count

    '            '行追加
    '            .ActiveSheet.AddRows(rowCnt, 1)

    '            'セルスタイル設定
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.DEF.ColNo, sCheck)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SERIALNO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.TOFROMNM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.INOUTDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.EMPTYKB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.IOZSKB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.HAIKIYN.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SHIPCDL.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo, sTextBox)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SHIPNML.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.INOUTKANOL.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.INOUTKANOM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.INOUTKANOS.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SYSUPDDATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.SYSUPDTIME.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.CUSTCDL.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMI190G.sprDetailsRireki.IOZSKBCD.ColNo, sLabel)

    '            'セルに値を設定
    '            .SetCellValue(rowCnt, sprDetailsRireki.DEF.ColNo, "False")
    '            .SetCellValue(rowCnt, sprDetailsRireki.SERIALNO.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SERIAL_NO").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.TOFROMNM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("TOFROM_NM").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.INOUTDATE.ColNo, DateFormatUtility.EditSlash(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUT_DATE").ToString()))
    '            .SetCellValue(rowCnt, sprDetailsRireki.EMPTYKB.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("EMPTY_KB").ToString())

    '            'IN/OUT
    '            If ("10").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString()) = True Then
    '                spdstr = "IN"
    '            Else
    '                spdstr = "OUT"
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsRireki.IOZSKB.ColNo, spdstr)

    '            '廃棄
    '            If ("1").Equals(ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("HAIKI_YN").ToString()) = True Then
    '                spdstr = "廃棄"
    '            Else
    '                spdstr = String.Empty
    '            End If
    '            .SetCellValue(rowCnt, sprDetailsRireki.HAIKIYN.ColNo, spdstr)

    '            .SetCellValue(rowCnt, sprDetailsRireki.SHIPCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.SHIPCDL_EDIT.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_CD_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.SHIPNML.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SHIP_NM_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.INOUTKANOL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.INOUTKANOM.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_M").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.INOUTKANOS.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("INOUTKA_NO_S").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.SYSUPDDATE.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_DATE").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.SYSUPDTIME.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("SYS_UPD_TIME").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.CUSTCDL.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_L").ToString())
    '            .SetCellValue(rowCnt, sprDetailsRireki.IOZSKBCD.ColNo, ds.Tables(LMI190C.TABLE_NM_OUT).Rows(i).Item("IOZS_KB").ToString())

    '        Next

    '        .ResumeLayout(True)

    '    End With

    'End Sub

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

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)右寄せ
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabelRight(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankakuNumber)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextNUMBER(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextHankaku(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[14])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNumMax(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function
#End Region

#Region "数字チェック"

    ''' <summary>
    ''' 数字チェック
    ''' </summary>
    ''' <param name="value">判定値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsNumericChk(ByVal value As String) As Boolean

        Dim valLength As Integer = value.Trim.Length

        If Regex.IsMatch(value, "^[0-9]{1," & valLength & "}$") = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
