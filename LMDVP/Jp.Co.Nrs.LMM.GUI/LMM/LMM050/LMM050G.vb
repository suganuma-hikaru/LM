' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM050G : 請求先マスタメンテ
'  作  成  者     : [金へスル]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.DSL


''' <summary>
''' LMM050Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM050G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM050F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM050F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMMConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = LMMControlC.FUNCTION_F1_SHINKI
            .F2ButtonName = LMMControlC.FUNCTION_F2_HENSHU
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW,
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        With Me._Frm
            .lblSituation.DispMode = dispMd
            .lblSituation.RecordStatus = recSts
        End With

    End Sub

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM050C.CtlTabIndex.DETAIL
            .txtSeiqtoCd.TabIndex = LMM050C.CtlTabIndex.SEIQTOCD
            .txtSeiqtoNm.TabIndex = LMM050C.CtlTabIndex.SEIQTONM
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            .txtEigyoTanto.TabIndex = LMM050C.CtlTabIndex.EIGYOTANTO
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
            .txtSeiqtoBusyoNm.TabIndex = LMM050C.CtlTabIndex.SEIQTOBUSYONM
            .cmbKouzaKbn.TabIndex = LMM050C.CtlTabIndex.KOUZAKBN
            .cmbMeigiKbn.TabIndex = LMM050C.CtlTabIndex.MEIGIKBN
            .txtNrsKeiriCd1.TabIndex = LMM050C.CtlTabIndex.NRSKEIRICD1
            .txtNrsKeiriCd2.TabIndex = LMM050C.CtlTabIndex.NRSKEIRICD2
            .txtSeiqSndPeriod.TabIndex = LMM050C.CtlTabIndex.SEIQSNDPERIOD
            .txtCustKagamiType1.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE1
            .txtCustKagamiType2.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE2
            .txtCustKagamiType3.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE3
            .txtOyaPic.TabIndex = LMM050C.CtlTabIndex.OYAPIC
            .txtTel.TabIndex = LMM050C.CtlTabIndex.TEL
            .txtFax.TabIndex = LMM050C.CtlTabIndex.FAX
            .cmbCloseKBN.TabIndex = LMM050C.CtlTabIndex.CLOSEKBN
            .txtZip.TabIndex = LMM050C.CtlTabIndex.ZIP
            .txtAd1.TabIndex = LMM050C.CtlTabIndex.AD1
            .txtAd2.TabIndex = LMM050C.CtlTabIndex.AD2
            .txtAd3.TabIndex = LMM050C.CtlTabIndex.AD3
            .txtAd3.TabIndex = LMM050C.CtlTabIndex.AD3
            .txtTekiyo.TabIndex = LMM050C.CtlTabIndex.REMARK

            .grpSeiq.TabIndex = LMM050C.CtlTabIndex.SEIQ
            .numStorageNr.TabIndex = LMM050C.CtlTabIndex.STORAGENR
            .numStorageNg.TabIndex = LMM050C.CtlTabIndex.STORAGENG
            .numStorageMin.TabIndex = LMM050C.CtlTabIndex.STORAGEMIN
            .numStorageOtherMin.TabIndex = LMM050C.CtlTabIndex.STORAGEOTHERMIN
            .cmbStorageZeroFlgKBN.TabIndex = LMM050C.CtlTabIndex.STORAGEZEROFLG
            .chkStorageTotalFlg.TabIndex = LMM050C.CtlTabIndex.STORAGETOTALFLG
            '.numStorageMinBak.TabIndex = LMM050C.CtlTabIndex.STORAGEMIN
            .numHandlingNr.TabIndex = LMM050C.CtlTabIndex.HANDLINGNR
            .numHandlingNg.TabIndex = LMM050C.CtlTabIndex.HANDLINGNG
            .numHandlingMin.TabIndex = LMM050C.CtlTabIndex.HANDLINGMIN
            .numHandlingOtherMin.TabIndex = LMM050C.CtlTabIndex.HANDLINGOTHERMIN
            .cmbHandlingZeroFlgKBN.TabIndex = LMM050C.CtlTabIndex.HANDLINGZEROFLG
            .chkHandlingTotalFlg.TabIndex = LMM050C.CtlTabIndex.HANDLINGTOTALFLG
            .numUnchinNr.TabIndex = LMM050C.CtlTabIndex.UNCHINNR
            .numUnchinNg.TabIndex = LMM050C.CtlTabIndex.UNCHINNG
            .numUnchinMin.TabIndex = LMM050C.CtlTabIndex.UNCHINMIN
            .chkUnchinTotalFlg.TabIndex = LMM050C.CtlTabIndex.UNCHIN_TOTAL_FLG
            .numSagyoNr.TabIndex = LMM050C.CtlTabIndex.SAGYONR
            .numSagyoNg.TabIndex = LMM050C.CtlTabIndex.SAGYONG
            .numSagyoMin.TabIndex = LMM050C.CtlTabIndex.SAGYOMIN
            .chkSagyoTotalFlg.TabIndex = LMM050C.CtlTabIndex.SAGYO_TOTAL_FLG
            .numClearanceNr.TabIndex = LMM050C.CtlTabIndex.CLEARANCENR
            .numClearanceNg.TabIndex = LMM050C.CtlTabIndex.CLEARANCENG
            .numYokomochiNr.TabIndex = LMM050C.CtlTabIndex.YOKOMOCHINR
            .numYokomochiNg.TabIndex = LMM050C.CtlTabIndex.YOKOMOCHING
            .numTotalNr.TabIndex = LMM050C.CtlTabIndex.TOTALNR
            .numTotalNg.TabIndex = LMM050C.CtlTabIndex.TOTALNG
            .numTotalMinSeiqAmt.TabIndex = LMM050C.CtlTabIndex.TOTALMIN
            .cmbDocPtn.TabIndex = LMM050C.CtlTabIndex.DOCPTN
            .chkSei.TabIndex = LMM050C.CtlTabIndex.SEI
            .chkFuku.TabIndex = LMM050C.CtlTabIndex.FUKU
            .chkHikae.TabIndex = LMM050C.CtlTabIndex.HIKAE
            .chkKeiri.TabIndex = LMM050C.CtlTabIndex.KEIRI
            .cmbDocPtnNomal.TabIndex = LMM050C.CtlTabIndex.DOCPTNNOMAL
            .lblSituation.TabIndex = LMM050C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM050C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM050C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM050C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM050C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM050C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM050C.CtlTabIndex.SYSDELFLG
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            .txtCustKagamiType4.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE4
            .txtCustKagamiType5.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE5
            .txtCustKagamiType6.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE6
            .txtCustKagamiType7.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE7
            .txtCustKagamiType8.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE8
            .txtCustKagamiType9.TabIndex = LMM050C.CtlTabIndex.CUSTKAGAMITYPE9
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            .grpVarStrage.TabIndex = LMM050C.CtlTabIndex.OPT_VAR_STRAGE
            .optVarStrageFlgN.TabIndex = LMM050C.CtlTabIndex.OPT_VAR_STRAGE_FLG_N
            .optVarStrageFlgY.TabIndex = LMM050C.CtlTabIndex.OPT_VAR_STRAGE_FLG_Y
            .cmbVarRate3.TabIndex = LMM050C.CtlTabIndex.CMB_VAR_RATE_3
            .cmbVarRate6.TabIndex = LMM050C.CtlTabIndex.CMB_VAR_RATE_6
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(Optional ByVal notHissuBusyo As Boolean = False)

        'コンボボックスの設定
        Call Me.CreateComboBox()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        'number型コントロールの桁数を設定する
        Call Me.SetNumberControl()

        'number型コントロールの初期値を設定する
        Call Me.SetDefaultNumber()

        'コントロールの必須項目を変更
        With Me._Frm
            '日陸経理コード(JDE)非必須化
            If notHissuBusyo = True Then
                .txtNrsKeiriCd2.HissuLabelVisible = False
            Else
                .txtNrsKeiriCd2.HissuLabelVisible = True
            End If

        End With
    End Sub

    ''' <summary>
    ''' コンボボックス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboControl()

        '変動倍率（3ヶ月）
        With Me._Frm
            .cmbVarRate3.Items.Clear()
            Dim subItems As List(Of ListItem) = New List(Of ListItem)
            For i As Decimal = 1D To 2D Step 0.1D
                subItems.Add(New ListItem(New SubItem() {New SubItem(i.ToString("0.0")), New SubItem(i.ToString("0.0"))}))
            Next
            .cmbVarRate3.Items.AddRange(subItems.ToArray())
            .cmbVarRate3.Refresh()
        End With

        '変動倍率（6ヶ月）
        With Me._Frm
            .cmbVarRate6.Items.Clear()
            Dim subItems As List(Of ListItem) = New List(Of ListItem)
            For i As Decimal = 1D To 2D Step 0.1D
                subItems.Add(New ListItem(New SubItem() {New SubItem(i.ToString("0.0")), New SubItem(i.ToString("0.0"))}))
            Next
            .cmbVarRate6.Items.AddRange(subItems.ToArray())
            .cmbVarRate6.Refresh()
        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM050C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' コンボボックス作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateComboBox()

        Call Me.NrsBankCmbBoxCrt()

        Call Me.RptCmbBoxCrt()

    End Sub

    ''' <summary>
    ''' 名義・銀行マスタコンボボックス
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NrsBankCmbBoxCrt()

        '区分マスタ（口座区分制御）
        Dim getKb As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M033' AND KBN_NM2 = '" & LMUserInfoManager.GetNrsBrCd() & "'")

        '名義・銀行マスタ
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim sort As String = "MEIGI_CD"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRSBANK).Select("SYS_DEL_FLG = '0'", sort)

        With Me._Frm

            Dim max As Integer = getDr.Count - 1
            .cmbKouzaKbn.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                cd = getDr(i).Item("MEIGI_CD").ToString()

                'コンボボックスにセットしていいかを判定する
                Dim setOk As Boolean = False
                If getKb.Count = 0 Then
                    setOk = True
                Else
                    For j As Integer = 0 To getKb.Count - 1
                        If cd.Equals(getKb(j).Item("KBN_NM1").ToString) Then
                            setOk = True
                            Exit For
                        End If
                    Next
                End If
                If Not setOk Then
                    Continue For
                End If

                item = getDr(i).Item("MEIGI_NM").ToString()
                item = String.Concat(item, Space(1), getDr(i).Item("BANK_NM").ToString())
                item = String.Concat(item, Space(1), getDr(i).Item("YOKIN_SYU").ToString())
                item = String.Concat(item, Space(1), getDr(i).Item("KOZA_NO").ToString())

                .cmbKouzaKbn.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
            Next

        End With

    End Sub

    ''' <summary>
    ''' 帳票パターンマスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RptCmbBoxCrt()

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        '請求書パターン（値引）
        '帳票パターンマスタ
        Dim sort As String = "PTN_CD"
        Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd()
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.RPT).Select(Me.SeiqToPtnString(nrsBrCd), sort)

        With Me._Frm

            Dim max As Integer = getDr.Count - 1
            .cmbDocPtn.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                cd = getDr(i).Item("PTN_CD").ToString()
                item = getDr(i).Item("PTN_NM").ToString()
                .cmbDocPtn.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
            Next

        End With

        'START YANAI 要望番号661
        '請求書パターン（通常）
        '帳票パターンマスタ
        getDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.RPT).Select(Me.SeiqToPtnString77(nrsBrCd), sort)

        With Me._Frm

            Dim max As Integer = getDr.Count - 1
            .cmbDocPtnNomal.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                cd = getDr(i).Item("PTN_CD").ToString()
                item = getDr(i).Item("PTN_NM").ToString()
                .cmbDocPtnNomal.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
            Next

        End With
        'END YANAI 要望番号661

    End Sub

    ''' <summary>
    ''' 帳票パターンマスタSELECT文(キャッシュ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SeiqToPtnString(ByVal brCd As String) As String

        SeiqToPtnString = String.Empty

        SeiqToPtnString = String.Concat(SeiqToPtnString, "SYS_DEL_FLG = '0'")

        SeiqToPtnString = String.Concat(SeiqToPtnString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        SeiqToPtnString = String.Concat(SeiqToPtnString, " AND ", "PTN_ID = ", " '", LMM050C.PTNID53, "' ")

        Return SeiqToPtnString

    End Function

    'START YANAI 要望番号661
    ''' <summary>
    ''' 帳票パターンマスタSELECT文(キャッシュ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SeiqToPtnString77(ByVal brCd As String) As String

        SeiqToPtnString77 = String.Empty

        SeiqToPtnString77 = String.Concat(SeiqToPtnString77, "SYS_DEL_FLG = '0'")

        SeiqToPtnString77 = String.Concat(SeiqToPtnString77, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        SeiqToPtnString77 = String.Concat(SeiqToPtnString77, " AND ", "PTN_ID = ", " '", LMM050C.PTNID77, "' ")

        Return SeiqToPtnString77

    End Function
    'END YANAI 要望番号661

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            'numberCellの桁数を設定する
            Call Me.SetNumberRate(.numStorageNr)
            Call Me.SetNumberGaku(.numStorageNg)
            Call Me.SetNumberGaku(.numStorageMinBak)

            Call Me.SetNumberRate(.numHandlingNr)
            Call Me.SetNumberGaku(.numHandlingNg)

            Call Me.SetNumberRate(.numUnchinNr)
            Call Me.SetNumberGaku(.numUnchinNg)

            Call Me.SetNumberRate(.numSagyoNr)
            Call Me.SetNumberGaku(.numSagyoNg)

            Call Me.SetNumberRate(.numClearanceNr)
            Call Me.SetNumberGaku(.numClearanceNg)

            Call Me.SetNumberRate(.numYokomochiNr)
            Call Me.SetNumberGaku(.numYokomochiNg)

            Call Me.SetNumberRate(.numTotalNr)
            Call Me.SetNumberGaku(.numTotalNg)

            Call Me.SetNumberGaku(.numTotalMinSeiqAmt)

            Call Me.SetNumberGaku(.numStorageMin)
            Call Me.SetNumberGaku(.numStorageOtherMin)
            Call Me.SetNumberGaku(.numHandlingMin)
            Call Me.SetNumberGaku(.numHandlingOtherMin)
            Call Me.SetNumberGaku(.numUnchinMin)
            Call Me.SetNumberGaku(.numSagyoMin)

        End With

    End Sub

    ''' <summary>
    ''' 値引率のナンバー型設定(0.00～100.00)
    ''' </summary>
    ''' <param name="numCtl"></param>
    ''' <remarks></remarks>
    Private Sub SetNumberRate(ByVal numCtl As Win.InputMan.LMImNumber)

        Dim d100 As Decimal = Convert.ToDecimal(100.0)
        numCtl.SetInputFields("##0.00", , 3, 1, , 2, 2, , d100, 0)

    End Sub

    ''' <summary>
    ''' 値引額のナンバー型設定(－999,999,999～999,999,999)
    ''' </summary>
    ''' <param name="numCtl"></param>
    ''' <remarks></remarks>
    Private Sub SetNumberGaku(ByVal numCtl As Win.InputMan.LMImNumber)

        Dim d9 As Decimal = Convert.ToDecimal(999999999)
        Dim dm9 As Decimal = Convert.ToDecimal(-999999999)

        numCtl.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, dm9, , , )

    End Sub

    ''' <summary>
    ''' 画面コントロール初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub DefaultSetControl()

        'コンボボックス初期値
        Call Me.SetDefaultcmbBox()

        'ナンバー型初期値
        Call Me.SetDefaultNumber()

        '通貨コード初期値
        Call Me.SetDefaultCurr()

    End Sub


    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultcmbBox()

        With Me._Frm
            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            '.cmbKouzaKbn.SelectedValue = LMM050C.KOUZA
            .cmbKouzaKbn.SelectedIndex = 1
            .cmbMeigiKbn.SelectedValue = LMM050C.BUTSURYU_CENTER    '2018/11/20 本社→物流センター 要望番号002425
            .cmbCloseKBN.SelectedValue = LMM050C.MATUJIME
            .cmbStorageZeroFlgKBN.SelectedValue = LMM050C.TEKIYOUNO
            .cmbHandlingZeroFlgKBN.SelectedValue = LMM050C.TEKIYOUNO
            .cmbVarRate3.SelectedValue = LMM050C.VAR_RATE_3
            .cmbVarRate6.SelectedValue = LMM050C.VAR_RATE_6
        End With

    End Sub

    ''' <summary>
    ''' ナンバー型初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultNumber()

        With Me._Frm

            .numStorageNr.Value = 0
            .numStorageNg.Value = 0
            .numStorageMinBak.Value = 0

            .numHandlingNr.Value = 0
            .numHandlingNg.Value = 0

            .numUnchinNr.Value = 0
            .numUnchinNg.Value = 0

            .numSagyoNr.Value = 0
            .numSagyoNg.Value = 0

            .numClearanceNr.Value = 0
            .numClearanceNg.Value = 0

            .numYokomochiNr.Value = 0
            .numYokomochiNg.Value = 0

            .numTotalNr.Value = 0
            .numTotalNg.Value = 0

            .numTotalMinSeiqAmt.Value = 0

            .numStorageMin.Value = 0
            .numStorageOtherMin.Value = 0
            .numHandlingMin.Value = 0
            .numHandlingOtherMin.Value = 0
            .numUnchinMin.Value = 0
            .numSagyoMin.Value = 0

        End With

    End Sub

    ''' <summary>
    '''新規押下時通貨コードの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultCurr()

        With Me._Frm
            .lblStorageNgCurrCd.TextValue = ""
            .lblHandlingNgCurrCd.TextValue = ""
            .lblUnchinNgCurrCd.TextValue = ""
            .lblSagyoNgCurrCd.TextValue = ""
            .lblClearanceNgCurrCd.TextValue = ""
            .lblYokomochiNgCurrCd.TextValue = ""
            .lblTotalNgCurrCd.TextValue = ""
            .lblStorageMinCurrCdBak.TextValue = ""
            .cmbSeiqCurrCd.TextValue = ""
            .lblTotalMinSeiqCurrCd.TextValue = ""
            .lblStorageMinCurrCd.TextValue = ""
            .lblStorageOtherMinCurrCd.TextValue = ""
            .lblHandlingMinCurrCd.TextValue = ""
            .lblHandlingOtherMinCurrCd.TextValue = ""
            .lblUnchinMinCurrCd.TextValue = ""
            .lblSagyoMinCurrCd.TextValue = ""

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Call Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)


                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._LMMConG.LockText(.txtSeiqtoCd, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)
                            Call Me.ClearControlFukusha()
                    End Select

                Case DispMode.INIT
                    Call Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)

            End Select

        End With

    End Sub


    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtSeiqtoCd.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

            '変動保管料関係
            .optVarStrageFlgN.Checked = True
            .cmbVarRate3.Enabled = False
            .cmbVarRate6.Enabled = False
            .cmbVarRate3.SelectedValue = LMM050C.VAR_RATE_3
            .cmbVarRate6.SelectedValue = LMM050C.VAR_RATE_6

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM050C.EventShubetsu)
        With Me._Frm
            Select Case eventType
                Case LMM050C.EventShubetsu.MAIN, LMM050C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM050C.EventShubetsu.SHINKI, LMM050C.EventShubetsu.HUKUSHA
                    .txtSeiqtoCd.Focus()
                Case LMM050C.EventShubetsu.HENSHU
                    .txtSeiqtoNm.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        Call Me._LMMConG.ClearControl(ctl)

        'ナンバー型初期値設定
        Call Me.SetDefaultNumber()

        '変動保管料
        Me._Frm.optVarStrageFlgN.Checked = True
        Me._Frm.cmbVarRate3.Enabled = False
        Me._Frm.cmbVarRate6.Enabled = False

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        'データ桁変換用変数(請求書種別)
        Dim sei As String = String.Empty
        Dim huku As String = String.Empty
        Dim hikae As String = String.Empty
        Dim keiri As String = String.Empty
        Dim dest As String = String.Empty    'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

        'データ桁変換用変数(最低保証)
        Dim storageTotalFlg As String = String.Empty
        Dim handlingTotalFlg As String = String.Empty
        Dim unchinTotalFlg As String = String.Empty
        Dim sagyoTotalFlg As String = String.Empty

        'データchar(2)を1桁に変換
        With Me._Frm
            sei = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_SEI_YN.ColNo).Text, 1)
            huku = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_HUKU_YN.ColNo).Text, 1)
            hikae = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_HIKAE_YN.ColNo).Text, 1)
            keiri = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_KEIRI_YN.ColNo).Text, 1)
            dest = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_DEST_YN.ColNo).Text, 1)  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtSeiqtoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQTO_CD.ColNo).Text
            .txtSeiqtoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQTO_NM.ColNo).Text
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            .txtEigyoTanto.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.EIGYO_TANTO.ColNo).Text
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
            .txtSeiqtoBusyoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo).Text
            .cmbKouzaKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.KOUZA_KB.ColNo).Text
            .cmbMeigiKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.MEIGI_KB.ColNo).Text
            .txtNrsKeiriCd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.NRS_KEIRI_CD1.ColNo).Text
            .txtNrsKeiriCd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.NRS_KEIRI_CD2.ColNo).Text
            .txtSeiqSndPeriod.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQ_SND_PERIOD.ColNo).Text
            .txtCustKagamiType1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE1.ColNo).Text
            .txtCustKagamiType2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE2.ColNo).Text
            .txtCustKagamiType3.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE3.ColNo).Text
            .txtOyaPic.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.OYA_PIC.ColNo).Text
            .txtTel.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TEL.ColNo).Text
            .txtFax.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.FAX.ColNo).Text
            .cmbCloseKBN.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CLOSE_KB.ColNo).Text
            .txtZip.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.ZIP.ColNo).Text
            .txtAd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.AD_1.ColNo).Text
            .txtAd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.AD_2.ColNo).Text
            .txtAd3.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.AD_3.ColNo).Text

            .numStorageNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_NR.ColNo).Text
            .numStorageNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_NG.ColNo).Text
            .numStorageMinBak.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN.ColNo).Text
            .numHandlingNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_NR.ColNo).Text
            .numHandlingNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_NG.ColNo).Text
            .numUnchinNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.UNCHIN_NR.ColNo).Text
            .numUnchinNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.UNCHIN_NG.ColNo).Text
            .numSagyoNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SAGYO_NR.ColNo).Text
            .numSagyoNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SAGYO_NG.ColNo).Text
            .numClearanceNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CLEARANCE_NR.ColNo).Text
            .numClearanceNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CLEARANCE_NG.ColNo).Text
            .numYokomochiNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.YOKOMOCHI_NR.ColNo).Text
            .numYokomochiNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.YOKOMOCHI_NG.ColNo).Text
            .numTotalNr.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TOTAL_NR.ColNo).Text
            .numTotalNg.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TOTAL_NG.ColNo).Text

            .cmbDocPtn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_PTN.ColNo).Text
            'START YANAI 要望番号661
            .cmbDocPtnNomal.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.DOC_PTN_NOMAL.ColNo).Text
            'END YANAI 要望番号661

            .chkSei.SetBinaryValue(sei)
            .chkFuku.SetBinaryValue(huku)
            .chkHikae.SetBinaryValue(hikae)
            .chkKeiri.SetBinaryValue(keiri)
            .chkdest.SetBinaryValue(dest)   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            .txtCustKagamiType4.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE4.ColNo).Text
            .txtCustKagamiType5.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE5.ColNo).Text
            .txtCustKagamiType6.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE6.ColNo).Text
            .txtCustKagamiType7.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE7.ColNo).Text
            .txtCustKagamiType8.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE8.ColNo).Text
            .txtCustKagamiType9.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE9.ColNo).Text
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
            '.cmbSeiqCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo).Text
            .cmbSeiqCurrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo).Text
            .lblTotalNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TOTAL_NG_CURR_CD.ColNo).Text
            .lblStorageNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_NG_CURR_CD.ColNo).Text
            .lblHandlingNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_NG_CURR_CD.ColNo).Text
            .lblUnchinNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.UNCHIN_NG_CURR_CD.ColNo).Text
            .lblSagyoNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SAGYO_NG_CURR_CD.ColNo).Text
            .lblClearanceNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.CLEARANCE_NG_CURR_CD.ColNo).Text
            .lblYokomochiNgCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.YOKOMOCHI_NG_CURR_CD.ColNo).Text
            .lblStorageMinCurrCdBak.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --

            .numTotalMinSeiqAmt.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_AMT.ColNo).Text
            .lblTotalMinSeiqCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_CURR_CD.ColNo).Text
            storageTotalFlg = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_TOTAL_FLG.ColNo).Text, 1)
            handlingTotalFlg = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_TOTAL_FLG.ColNo).Text, 1)
            unchinTotalFlg = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.UNCHIN_TOTAL_FLG.ColNo).Text, 1)
            sagyoTotalFlg = Right(.sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SAGYO_TOTAL_FLG.ColNo).Text, 1)
            .chkStorageTotalFlg.SetBinaryValue(storageTotalFlg)
            .chkHandlingTotalFlg.SetBinaryValue(handlingTotalFlg)
            .chkUnchinTotalFlg.SetBinaryValue(unchinTotalFlg)
            .chkSagyoTotalFlg.SetBinaryValue(sagyoTotalFlg)

            .numStorageMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_AMT.ColNo).Text
            .numStorageOtherMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT.ColNo).Text
            .cmbStorageZeroFlgKBN.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_ZERO_FLG.ColNo).Text
            .numHandlingMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_MIN_AMT.ColNo).Text
            .numHandlingOtherMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT.ColNo).Text
            .cmbHandlingZeroFlgKBN.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.HANDLING_ZERO_FLG.ColNo).Text
            .numUnchinMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.UNCHIN_MIN_AMT.ColNo).Text
            .numSagyoMin.Value = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.SAGYO_MIN_AMT.ColNo).Text

            .lblStorageMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            .lblStorageOtherMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            .lblHandlingMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            .lblHandlingOtherMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            .lblUnchinMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text
            .lblSagyoMinCurrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Text

            .txtTekiyo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.REMARK.ColNo).Text    'ADD 2019/07/10 002520

            If .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.VAR_STRAGE_FLG.ColNo).Text = "1" Then
                .optVarStrageFlgY.Checked = True
                .cmbVarRate3.Enabled = True
                .cmbVarRate6.Enabled = True
            Else
                .optVarStrageFlgN.Checked = True
                .cmbVarRate3.Enabled = False
                .cmbVarRate6.Enabled = False
            End If
            .cmbVarRate3.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.VAR_RATE_3.ColNo).Text
            .cmbVarRate6.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM050G.sprDetailDef.VAR_RATE_6.ColNo).Text
        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds.Tables(LMM050C.TABLE_NM_OUT))

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SEIQTO_CD, "請求先" & vbCrLf & "コード", 70, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SEIQTO_NM, "請求先会社名", 180, True)
        Public Shared SEIQTO_BUSYO_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SEIQTO_BUSYO_NM, "請求先部署名", 180, True)
        Public Shared KOUZA_KB As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.KOUZA_KB, "鑑口座区分コード", 60, False)         '区分コード(隠し項目)
        Public Shared KOUZA_KB_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.KOUZA_KB_NM, "鑑口座区分", 350, True)            '区分名称
        Public Shared MEIGI_KB As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.MEIGI_KB, "鑑名義区分", 60, False)         '区分コード(隠し項目)
        Public Shared OYA_PIC As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.OYA_PIC, "担当者名", 120, True)
        Public Shared ZIP As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.ZIP, "郵便番号", 60, False)
        Public Shared AD_1 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.AD_1, "住所1", 60, False)
        Public Shared AD_2 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.AD_2, "住所2", 60, False)
        Public Shared AD_3 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.AD_3, "住所3", 60, False)
        Public Shared TEL As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TEL, "電話番号", 60, False)
        Public Shared FAX As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.FAX, "ファックス番号", 60, False)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CLOSE_KB, "締日区分", 60, False)
        Public Shared CLOSE_KB_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CLOSE_KB_NM, "締日区分名", 60, False)
        Public Shared DOC_PTN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_PTN, "請求書パターン", 60, False)
        'START YANAI 要望番号661
        Public Shared DOC_PTN_NOMAL As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_PTN_NOMAL, "請求書パターン２", 60, False)
        'END YANAI 要望番号661
        Public Shared DOC_SEI_YN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_SEI_YN, "請求書種別正", 60, False)
        Public Shared DOC_HUKU_YN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_HUKU_YN, "請求書種別副", 60, False)
        Public Shared DOC_HIKAE_YN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_HIKAE_YN, "請求書種別控", 60, False)
        Public Shared DOC_KEIRI_YN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_KEIRI_YN, "請求書種別控(経理)", 60, False)
        Public Shared DOC_DEST_YN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.DOC_DEST_YN, "宛名", 60, False)  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        Public Shared NRS_KEIRI_CD1 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.NRS_KEIRI_CD1, "親請求先" & vbCrLf & "コード", 100, False)
        Public Shared NRS_KEIRI_CD2 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.NRS_KEIRI_CD2, "経理" & vbCrLf & "コード(JDE)", 100, False)
        Public Shared SEIQ_SND_PERIOD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SEIQ_SND_PERIOD, "請求書・送付期限", 60, False)
        Public Shared TOTAL_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TOTAL_NR, "全体値引率", 60, False)
        Public Shared STORAGE_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_NR, "保管料値引率", 60, False)
        Public Shared HANDLING_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_NR, "荷役料値引率", 60, False)
        Public Shared UNCHIN_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_NR, "運賃値引率", 60, False)
        Public Shared SAGYO_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_NR, "作業料値引率", 60, False)
        Public Shared CLEARANCE_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CLEARANCE_NR, "通関料値引率", 60, False)
        Public Shared YOKOMOCHI_NR As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.YOKOMOCHI_NR, "横持料値引率", 60, False)
        Public Shared TOTAL_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TOTAL_NG, "全体値引額", 60, False)
        Public Shared STORAGE_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_NG, "保管料値引額", 60, False)
        Public Shared HANDLING_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_NG, "荷役料値引額", 60, False)
        Public Shared UNCHIN_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_NG, "運賃値引額", 60, False)
        Public Shared SAGYO_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_NG, "作業料値引額", 60, False)
        Public Shared CLEARANCE_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CLEARANCE_NG, "通関料値引額", 60, False)
        Public Shared YOKOMOCHI_NG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.YOKOMOCHI_NG, "横持料値引額", 60, False)
        Public Shared STORAGE_MIN As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_MIN, "保管料最低保証額", 60, False)
        Public Shared CUST_KAGAMI_TYPE1 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE1, "荷主鑑分類種別1", 60, False)
        Public Shared CUST_KAGAMI_TYPE2 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE2, "荷主鑑分類種別2", 60, False)
        Public Shared CUST_KAGAMI_TYPE3 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE3, "荷主鑑分類種別3", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
        Public Shared CUST_KAGAMI_TYPE4 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE4, "荷主鑑分類種別4", 60, False)
        Public Shared CUST_KAGAMI_TYPE5 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE5, "荷主鑑分類種別5", 60, False)
        Public Shared CUST_KAGAMI_TYPE6 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE6, "荷主鑑分類種別6", 60, False)
        Public Shared CUST_KAGAMI_TYPE7 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE7, "荷主鑑分類種別7", 60, False)
        Public Shared CUST_KAGAMI_TYPE8 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE8, "荷主鑑分類種別8", 60, False)
        Public Shared CUST_KAGAMI_TYPE9 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CUST_KAGAMI_TYPE9, "荷主鑑分類種別9", 60, False)
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
        '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
        Public Shared SEIQ_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SEIQ_CURR_CD, "請求通貨コード", 60, False)
        Public Shared TOTAL_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TOTAL_NG_CURR_CD, "全体値引額通貨", 60, False)
        Public Shared STORAGE_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_NG_CURR_CD, "保管料値引額通貨", 60, False)
        Public Shared HANDLING_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_NG_CURR_CD, "荷役料値引額通貨", 60, False)
        Public Shared UNCHIN_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_NG_CURR_CD, "運賃値引額通貨", 60, False)
        Public Shared SAGYO_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_NG_CURR_CD, "作業料値引額通貨", 60, False)
        Public Shared CLEARANCE_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.CLEARANCE_NG_CURR_CD, "通関料値引額通貨", 60, False)
        Public Shared YOKOMOCHI_NG_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.YOKOMOCHI_NG_CURR_CD, "横持料値引額通貨", 60, False)
        Public Shared STORAGE_MIN_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_MIN_CURR_CD, "保管料最低保証額通貨", 60, False)
        '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --
        Public Shared TOTAL_MIN_SEIQ_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TOTAL_MIN_SEIQ_AMT, "鑑最低保証請求合計額", 60, False)
        Public Shared TOTAL_MIN_SEIQ_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.TOTAL_MIN_SEIQ_CURR_CD, "鑑最低保証請求合計額", 60, False)
        Public Shared STORAGE_TOTAL_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_TOTAL_FLG, "保管料最低保証設定フラグ", 60, False)
        Public Shared HANDLING_TOTAL_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_TOTAL_FLG, "荷役料最低保証設定フラグ", 60, False)
        Public Shared UNCHIN_TOTAL_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_TOTAL_FLG, "運賃最低保証設定フラグ", 60, False)
        Public Shared SAGYO_TOTAL_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_TOTAL_FLG, "作業料最低保証設定フラグ", 60, False)

        Public Shared STORAGE_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_MIN_AMT, "保管料最低保証金額(自社)", 60, False)
        Public Shared STORAGE_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_MIN_AMT_CURR_CD, "保管料最低保証金額通貨(自社)", 60, False)
        Public Shared STORAGE_OTHER_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_OTHER_MIN_AMT, "保管料最低保証金額(他社)", 60, False)
        Public Shared STORAGE_OTHER_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_OTHER_MIN_AMT_CURR_CD, "保管料最低保証金額通貨(他社)", 60, False)
        Public Shared HANDLING_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_MIN_AMT, "荷役料最低保証金額(自社)", 60, False)
        Public Shared HANDLING_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_MIN_AMT_CURR_CD, "荷役料最低保証金額通貨(自社)", 60, False)
        Public Shared HANDLING_OTHER_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_OTHER_MIN_AMT, "荷役料最低保証金額(他社)", 60, False)
        Public Shared HANDLING_OTHER_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_OTHER_MIN_AMT_CURR_CD, "荷役料最低保証金額通貨(他社)", 60, False)
        Public Shared UNCHIN_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_MIN_AMT, "運賃最低保証金額", 60, False)
        Public Shared UNCHIN_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.UNCHIN_MIN_AMT_CURR_CD, "運賃最低保証金額通貨", 60, False)
        Public Shared SAGYO_MIN_AMT As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_MIN_AMT, "作業料最低保証金額", 60, False)
        Public Shared SAGYO_MIN_AMT_CURR_CD As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.SAGYO_MIN_AMT_CURR_CD, "作業料最低保証金額通貨", 60, False)
        Public Shared STORAGE_ZERO_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.STORAGE_ZERO_FLG, "保管料最低保証設定フラグ", 60, False)
        Public Shared HANDLING_ZERO_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.HANDLING_ZERO_FLG, "荷役料最低保証設定フラグ", 60, False)
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
        Public Shared EIGYO_TANTO As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.EIGYO_TANTO, "営業担当", 100, False)
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.REMARK, "適用", 200, False)      'ADD 2019/07/10 002520
        Public Shared VAR_STRAGE_FLG As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.VAR_STRAGE_FLG, "", 50, False)
        Public Shared VAR_RATE_3 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.VAR_RATE_3, "", 50, False)
        Public Shared VAR_RATE_6 As SpreadColProperty = New SpreadColProperty(LMM050C.SprColumnIndex.VAR_RATE_6, "", 50, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()
        Dim dr As DataRow
        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            'START YANAI 要望番号661
            '.sprDetail.ActiveSheet.ColumnCount = 51
            '.sprDetail.ActiveSheet.ColumnCount = 52
            '.sprDetail.ActiveSheet.ColumnCount = 58
            '.sprDetail.ActiveSheet.ColumnCount = 67
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            '.sprDetail.ActiveSheet.ColumnCount = 87         'UPD 2019/07/10 002520　86⇒87
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
            'END YANAI 要望番号661
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            .sprDetail.ActiveSheet.ColumnCount = LMM050C.SprColumnIndex.LAST

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM050G.sprDetailDef())
            .sprDetail.SetColProperty(New LMM050G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM050G.sprDetailDef.DEF.ColNo + 1

            '列設定用変数

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)
            Dim numLbl As StyleInfo = Me.StyleInfoNum9(.sprDetail, True)
            Dim gakLbl As StyleInfo = Me.StyleInfoNum3dec2(.sprDetail, True)
            '列設定
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.NRS_BR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SEIQTO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.KOUZA_KB.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.KOUZA_KB_NM.ColNo, Me.SetComboKouza())
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.OYA_PIC.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.MEIGI_KB.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.ZIP.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.AD_1.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.AD_2.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.AD_3.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TEL.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.FAX.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CLOSE_KB.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CLOSE_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S008", False))
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_PTN.ColNo, lbl)
            'START YANAI 要望番号661
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_PTN_NOMAL.ColNo, lbl)
            'END YANAI 要望番号661
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_SEI_YN.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_HUKU_YN.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_HIKAE_YN.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_KEIRI_YN.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.DOC_DEST_YN.ColNo, lbl)  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.NRS_KEIRI_CD1.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.NRS_KEIRI_CD2.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SEIQ_SND_PERIOD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CLEARANCE_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.YOKOMOCHI_NR.ColNo, gakLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CLEARANCE_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.YOKOMOCHI_NG.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE1.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE2.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE3.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_ENT_DATE.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_UPD_TIME.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SYS_DEL_FLG.ColNo, lbl)
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE4.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE5.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE6.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE7.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE8.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE9.ColNo, lbl)
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.CLEARANCE_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.YOKOMOCHI_NG_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo, lbl)
            '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_TOTAL_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_TOTAL_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_TOTAL_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_TOTAL_FLG.ColNo, lbl)

            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT.ColNo, numLbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT_CURR_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_ZERO_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_ZERO_FLG.ColNo, lbl)
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.EIGYO_TANTO.ColNo, lbl)
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end

            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.REMARK.ColNo, lbl)      'ADD 2019/07/10 002520
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.VAR_STRAGE_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.VAR_RATE_3.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM050G.sprDetailDef.VAR_RATE_6.ColNo, lbl)
        End With

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM050F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.NRS_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SEIQTO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SEIQTO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.KOUZA_KB_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.OYA_PIC.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.MEIGI_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.ZIP.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.AD_1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.AD_2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.AD_3.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TEL.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.FAX.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CLOSE_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CLOSE_KB_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_PTN.ColNo).Value = String.Empty
            'START YANAI 要望番号661
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_PTN_NOMAL.ColNo).Value = String.Empty
            'END YANAI 要望番号661
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_SEI_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_HUKU_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_HIKAE_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_KEIRI_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.DOC_DEST_YN.ColNo).Value = String.Empty   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.NRS_KEIRI_CD1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.NRS_KEIRI_CD2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SEIQ_SND_PERIOD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TOTAL_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CLEARANCE_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.YOKOMOCHI_NR.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TOTAL_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CLEARANCE_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.YOKOMOCHI_NG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_MIN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE3.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE4.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE5.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE6.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE7.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE8.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE9.ColNo).Value = String.Empty
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TOTAL_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.CLEARANCE_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.YOKOMOCHI_NG_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo).Value = String.Empty
            '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_TOTAL_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_TOTAL_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_TOTAL_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_TOTAL_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT_CURR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.STORAGE_ZERO_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.HANDLING_ZERO_FLG.ColNo).Value = String.Empty
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.EIGYO_TANTO.ColNo).Value = String.Empty
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.REMARK.ColNo).Value = String.Empty           'ADD 2019/07/10 002520
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.VAR_STRAGE_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.VAR_RATE_3.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM050G.sprDetailDef.VAR_RATE_6.ColNo).Value = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet
        Dim lock As Boolean = True
        With spr

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sRt As StyleInfo = Me.StyleInfoNum3dec2(spr, lock)
            Dim sGk As StyleInfo = Me.StyleInfoNum9(spr, lock)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM050G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.KOUZA_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.KOUZA_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.OYA_PIC.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.MEIGI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.ZIP.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.AD_1.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.AD_2.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.AD_3.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.TEL.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.FAX.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CLOSE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CLOSE_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_PTN.ColNo, sLabel)
                'START YANAI 要望番号661
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_PTN_NOMAL.ColNo, sLabel)
                'END YANAI 要望番号661
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_SEI_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_HUKU_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_HIKAE_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_KEIRI_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.DOC_DEST_YN.ColNo, sLabel)     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                .SetCellStyle(i, LMM050G.sprDetailDef.NRS_KEIRI_CD1.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.NRS_KEIRI_CD2.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SEIQ_SND_PERIOD.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.TOTAL_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.STORAGE_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.HANDLING_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.UNCHIN_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.SAGYO_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.CLEARANCE_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.YOKOMOCHI_NR.ColNo, sRt)
                .SetCellStyle(i, LMM050G.sprDetailDef.TOTAL_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.STORAGE_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.HANDLING_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.UNCHIN_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.SAGYO_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.CLEARANCE_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.YOKOMOCHI_NG.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.STORAGE_MIN.ColNo, sGk)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE1.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE2.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE3.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE4.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE5.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE6.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE7.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE8.ColNo, sLabel)
                .SetCellStyle(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE9.ColNo, sLabel)
                '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
                '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
                .SetCellStyle(0, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.CLEARANCE_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.YOKOMOCHI_NG_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo, sLabel)
                '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --
                .SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_TOTAL_FLG.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_TOTAL_FLG.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_TOTAL_FLG.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_TOTAL_FLG.ColNo, sLabel)

                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.UNCHIN_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT.ColNo, sGk)
                .SetCellStyle(0, LMM050G.sprDetailDef.SAGYO_MIN_AMT_CURR_CD.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.STORAGE_ZERO_FLG.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.HANDLING_ZERO_FLG.ColNo, sLabel)
                '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
                .SetCellStyle(0, LMM050G.sprDetailDef.EIGYO_TANTO.ColNo, sLabel)
                '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
                .SetCellStyle(0, LMM050G.sprDetailDef.REMARK.ColNo, sLabel)     'ADD 2019/07/10 002520
                .SetCellStyle(0, LMM050G.sprDetailDef.VAR_STRAGE_FLG.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.VAR_RATE_3.ColNo, sLabel)
                .SetCellStyle(0, LMM050G.sprDetailDef.VAR_RATE_6.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM050G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SEIQTO_BUSYO_NM.ColNo, dr.Item("SEIQTO_BUSYO_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.KOUZA_KB.ColNo, dr.Item("KOUZA_KB").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.KOUZA_KB_NM.ColNo, dr.Item("KOUZA_KB_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.OYA_PIC.ColNo, dr.Item("OYA_PIC").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.MEIGI_KB.ColNo, dr.Item("MEIGI_KB").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.AD_1.ColNo, dr.Item("AD_1").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.AD_2.ColNo, dr.Item("AD_2").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.AD_3.ColNo, dr.Item("AD_3").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.FAX.ColNo, dr.Item("FAX").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CLOSE_KB.ColNo, dr.Item("CLOSE_KB").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CLOSE_KB_NM.ColNo, dr.Item("CLOSE_KB_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_PTN.ColNo, dr.Item("DOC_PTN").ToString())
                'START YANAI 要望番号661
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_PTN_NOMAL.ColNo, dr.Item("DOC_PTN2").ToString())
                'END YANAI 要望番号661
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_SEI_YN.ColNo, dr.Item("DOC_SEI_YN").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_HUKU_YN.ColNo, dr.Item("DOC_HUKU_YN").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_HIKAE_YN.ColNo, dr.Item("DOC_HIKAE_YN").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_KEIRI_YN.ColNo, dr.Item("DOC_KEIRI_YN").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.DOC_DEST_YN.ColNo, dr.Item("DOC_DEST_YN").ToString())  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                .SetCellValue(i, LMM050G.sprDetailDef.NRS_KEIRI_CD1.ColNo, dr.Item("NRS_KEIRI_CD1").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.NRS_KEIRI_CD2.ColNo, dr.Item("NRS_KEIRI_CD2").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SEIQ_SND_PERIOD.ColNo, dr.Item("SEIQ_SND_PERIOD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.TOTAL_NR.ColNo, dr.Item("TOTAL_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_NR.ColNo, dr.Item("STORAGE_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_NR.ColNo, dr.Item("HANDLING_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_NR.ColNo, dr.Item("UNCHIN_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_NR.ColNo, dr.Item("SAGYO_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CLEARANCE_NR.ColNo, dr.Item("CLEARANCE_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.YOKOMOCHI_NR.ColNo, dr.Item("YOKOMOCHI_NR").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.TOTAL_NG.ColNo, dr.Item("TOTAL_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_NG.ColNo, dr.Item("STORAGE_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_NG.ColNo, dr.Item("HANDLING_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_NG.ColNo, dr.Item("UNCHIN_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_NG.ColNo, dr.Item("SAGYO_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CLEARANCE_NG.ColNo, dr.Item("CLEARANCE_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.YOKOMOCHI_NG.ColNo, dr.Item("YOKOMOCHI_NG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_MIN.ColNo, dr.Item("STORAGE_MIN").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE1.ColNo, dr.Item("CUST_KAGAMI_TYPE1").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE2.ColNo, dr.Item("CUST_KAGAMI_TYPE2").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE3.ColNo, dr.Item("CUST_KAGAMI_TYPE3").ToString())

                .SetCellValue(i, LMM050G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE4.ColNo, dr.Item("CUST_KAGAMI_TYPE4").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE5.ColNo, dr.Item("CUST_KAGAMI_TYPE5").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE6.ColNo, dr.Item("CUST_KAGAMI_TYPE6").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE7.ColNo, dr.Item("CUST_KAGAMI_TYPE7").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE8.ColNo, dr.Item("CUST_KAGAMI_TYPE8").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CUST_KAGAMI_TYPE9.ColNo, dr.Item("CUST_KAGAMI_TYPE9").ToString())
                '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
                '(2014.09.17)要望番号2229 請求通貨 追加 -- START --
                .SetCellValue(i, LMM050G.sprDetailDef.SEIQ_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.TOTAL_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.CLEARANCE_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.YOKOMOCHI_NG_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_MIN_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                '(2014.09.17)要望番号2229 請求通貨 追加 --  END  --
                .SetCellValue(i, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_AMT.ColNo, dr.Item("TOTAL_MIN_SEIQ_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.TOTAL_MIN_SEIQ_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_TOTAL_FLG.ColNo, dr.Item("STORAGE_TOTAL_FLG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_TOTAL_FLG.ColNo, dr.Item("HANDLING_TOTAL_FLG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_TOTAL_FLG.ColNo, dr.Item("UNCHIN_TOTAL_FLG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_TOTAL_FLG.ColNo, dr.Item("SAGYO_TOTAL_FLG").ToString())

                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_MIN_AMT.ColNo, dr.Item("STORAGE_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT.ColNo, dr.Item("STORAGE_OTHER_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_MIN_AMT.ColNo, dr.Item("HANDLING_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT.ColNo, dr.Item("HANDLING_OTHER_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_MIN_AMT.ColNo, dr.Item("UNCHIN_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_MIN_AMT.ColNo, dr.Item("SAGYO_MIN_AMT").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_OTHER_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_OTHER_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.UNCHIN_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.SAGYO_MIN_AMT_CURR_CD.ColNo, dr.Item("SEIQ_CURR_CD").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.STORAGE_ZERO_FLG.ColNo, dr.Item("STORAGE_ZERO_FLG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.HANDLING_ZERO_FLG.ColNo, dr.Item("HANDLING_ZERO_FLG").ToString())
                '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
                .SetCellValue(i, LMM050G.sprDetailDef.EIGYO_TANTO.ColNo, dr.Item("EIGYO_TANTO").ToString())
                '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
                .SetCellValue(i, LMM050G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())       'ADD 2019/07/10 002520
                .SetCellValue(i, LMM050G.sprDetailDef.VAR_STRAGE_FLG.ColNo, dr.Item("VAR_STRAGE_FLG").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.VAR_RATE_3.ColNo, dr.Item("VAR_RATE_3").ToString())
                .SetCellValue(i, LMM050G.sprDetailDef.VAR_RATE_6.ColNo, dr.Item("VAR_RATE_6").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド鑑名義区分コンボボックス設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetComboKouza() As StyleInfo

        Dim sort As String = "MEIGI_CD"
        Dim getDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRSBANK)
        getDt.Rows.Clear()
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRSBANK).Select("SYS_DEL_FLG = '0'", sort)
        Dim item As String = String.Empty
        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("MEIGI_NM").ToString()
            item = String.Concat(item, " ", getDr(i).Item("BANK_NM").ToString())
            item = String.Concat(item, " ", getDr(i).Item("YOKIN_SYU").ToString())
            item = String.Concat(item, " ", getDr(i).Item("KOZA_NO").ToString())

            getDr(i).Item("MEIGI_NM") = item
            getDt.ImportRow(getDr(i))
        Next

        Dim cmb As StyleInfo = LMSpreadUtility.GetComboCell(Me._Frm.sprDetail, New DataView(getDt), "MEIGI_CD", "MEIGI_NM", False)

        Return cmb

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, lock, 0, True, ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数3桁　少数2桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3dec2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999.99, lock, 2, , ",")

    End Function

#End Region 'Spread

#Region "部品化検討中"

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

    End Sub


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



#End Region

#End Region

End Class
