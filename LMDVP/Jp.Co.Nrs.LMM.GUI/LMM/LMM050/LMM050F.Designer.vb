<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM050F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormSxga

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim EnhancedFocusIndicatorRenderer2 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer3 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer4 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.sprDetail_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtFax = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblTelTitlelblFax = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTel = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtAd3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblAd3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtAd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblAd2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtAd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblAd1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblTel = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblDocPtn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblKagamiKB = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblSeiqNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblSeiqCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeiqtoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleNrsKeiriCd1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblNrsCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblSentPeriod = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblCloseKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbCloseKBN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtNrsKeiriCd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtNrsKeiriCd2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeiqSndPeriod = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelbPic = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOyaPic = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleZip = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtZip = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblNr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblNg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numStorageNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.TitlelblMin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numStorageNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numStorageMinBak = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numHandlingNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numHandlingNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel44 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel48 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel52 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel53 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numClearanceNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numClearanceNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numYokomochiNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numYokomochiNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel65 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnchinNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numUnchinNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSagyoNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSagyoNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numTotalNg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numTotalNr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.TitlelblTotalNg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblTotalNr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpSeiq = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.cmbHandlingZeroFlgKBN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbStorageZeroFlgKBN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblZeroMin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSagyoMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnchinMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblHandlingMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblStorageMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numStorageMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numHandlingMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel19 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSagyoMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numUnchinMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblHandlingOtherMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblStorageOtherMinCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numStorageOtherMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numHandlingOtherMin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel12 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkSagyoTotalFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkUnchinTotalFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkHandlingTotalFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblStorageMinCurrCdBak = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTotalMinSeiqCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTotalNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblYokomochiNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblClearanceNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSagyoNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnchinNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblHandlingNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblStorageNgCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTotalMinSeiqAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numTotalMinSeiqAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTotalMinChk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkStorageTotalFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.txtCustKagamiType1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeiqtoBusyoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblSeiqBushoNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitlelblDocKind = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkSei = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkKeiri = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkHikae = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkFuku = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbKouzaKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblKagamiMeigi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbMeigiKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbDocPtn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.cmbDocPtnNomal = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.TitlelblDocPtnNomal = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType6 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType5 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType4 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType9 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType8 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustKagamiType7 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitlelblCustKagami7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSeiqCurrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.lblTitleSeiqCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.TitleEigyoTanto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtEigyoTanto = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTekiyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.TitleltxtTekiyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkdest = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.grpVarStrage = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.optVarStrageFlgN = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optVarStrageFlgY = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleVarStrageFlg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbVarRate6 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.cmbVarRate3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.lblTitleVarRate6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleVarRate3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleVarRate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSeiq.SuspendLayout()
        Me.grpVarStrage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.lblTitleVarStrageFlg)
        Me.pnlViewAria.Controls.Add(Me.cmbVarRate6)
        Me.pnlViewAria.Controls.Add(Me.grpVarStrage)
        Me.pnlViewAria.Controls.Add(Me.cmbVarRate3)
        Me.pnlViewAria.Controls.Add(Me.lblTitleVarRate6)
        Me.pnlViewAria.Controls.Add(Me.chkdest)
        Me.pnlViewAria.Controls.Add(Me.lblTitleVarRate3)
        Me.pnlViewAria.Controls.Add(Me.txtTekiyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleVarRate)
        Me.pnlViewAria.Controls.Add(Me.TitleltxtTekiyo)
        Me.pnlViewAria.Controls.Add(Me.txtEigyoTanto)
        Me.pnlViewAria.Controls.Add(Me.TitleEigyoTanto)
        Me.pnlViewAria.Controls.Add(Me.cmbSeiqCurrCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqCurrCd)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType9)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami9)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType8)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami8)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType7)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami7)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType6)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami6)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType5)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami5)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType4)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami4)
        Me.pnlViewAria.Controls.Add(Me.cmbDocPtnNomal)
        Me.pnlViewAria.Controls.Add(Me.TitlelblDocPtnNomal)
        Me.pnlViewAria.Controls.Add(Me.cmbDocPtn)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.cmbMeigiKbn)
        Me.pnlViewAria.Controls.Add(Me.lblKagamiMeigi)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.cmbKouzaKbn)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.chkFuku)
        Me.pnlViewAria.Controls.Add(Me.chkHikae)
        Me.pnlViewAria.Controls.Add(Me.chkKeiri)
        Me.pnlViewAria.Controls.Add(Me.chkSei)
        Me.pnlViewAria.Controls.Add(Me.TitlelblDocKind)
        Me.pnlViewAria.Controls.Add(Me.TitlelblSeiqBushoNm)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoBusyoNm)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType3)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami3)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType2)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami2)
        Me.pnlViewAria.Controls.Add(Me.txtCustKagamiType1)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCustKagami1)
        Me.pnlViewAria.Controls.Add(Me.TitlelblEigyo)
        Me.pnlViewAria.Controls.Add(Me.txtZip)
        Me.pnlViewAria.Controls.Add(Me.lblTitleZip)
        Me.pnlViewAria.Controls.Add(Me.txtOyaPic)
        Me.pnlViewAria.Controls.Add(Me.TitlelbPic)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqSndPeriod)
        Me.pnlViewAria.Controls.Add(Me.txtNrsKeiriCd2)
        Me.pnlViewAria.Controls.Add(Me.txtNrsKeiriCd1)
        Me.pnlViewAria.Controls.Add(Me.cmbCloseKBN)
        Me.pnlViewAria.Controls.Add(Me.TitlelblCloseKbn)
        Me.pnlViewAria.Controls.Add(Me.TitlelblSentPeriod)
        Me.pnlViewAria.Controls.Add(Me.TitlelblNrsCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNrsKeiriCd1)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoNm)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoCd)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.txtFax)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.TitlelblTelTitlelblFax)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.txtTel)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.txtAd3)
        Me.pnlViewAria.Controls.Add(Me.TitlelblAd3)
        Me.pnlViewAria.Controls.Add(Me.txtAd2)
        Me.pnlViewAria.Controls.Add(Me.TitlelblAd2)
        Me.pnlViewAria.Controls.Add(Me.txtAd1)
        Me.pnlViewAria.Controls.Add(Me.TitlelblAd1)
        Me.pnlViewAria.Controls.Add(Me.TitlelblSeiqNm)
        Me.pnlViewAria.Controls.Add(Me.TitlelblSeiqCd)
        Me.pnlViewAria.Controls.Add(Me.TitlelblTel)
        Me.pnlViewAria.Controls.Add(Me.TitlelblDocPtn)
        Me.pnlViewAria.Controls.Add(Me.TitlelblKagamiKB)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.grpSeiq)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'sprDetail
        '
        Me.sprDetail.AccessibleDescription = ""
        Me.sprDetail.AllowUserZoom = False
        Me.sprDetail.AutoImeMode = False
        Me.sprDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetail.CellClickEventArgs = Nothing
        Me.sprDetail.CheckToCheckBox = True
        Me.sprDetail.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetail.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.EditModeReplace = True
        Me.sprDetail.FocusRenderer = EnhancedFocusIndicatorRenderer2
        Me.sprDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail.HeightDef = 346
        Me.sprDetail.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.HorizontalScrollBar.Name = ""
        Me.sprDetail.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 21)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail_Sheet1})
        Me.sprDetail.Size = New System.Drawing.Size(1245, 346)
        Me.sprDetail.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.VerticalScrollBar.Name = ""
        Me.sprDetail.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.sprDetail.WidthDef = 1245
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenFocusedNormal)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenAncestorOfFocusedNormal)
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        Me.sprDetail.SetViewportTopRow(1, 0, 1)
        '
        'sprDetail_Sheet1
        '
        Me.sprDetail_Sheet1.Reset()
        Me.sprDetail_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprDetail_Sheet1.RowCount = 1
        Me.sprDetail_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprDetail_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.sprDetail_Sheet1.Cells.Get(0, 0).Locked = True
        Me.sprDetail_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprDetail_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " "
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(0).Label = " "
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprDetail_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        Me.sprDetail_Sheet1.Columns.Get(0).Label = " "
        Me.sprDetail_Sheet1.Columns.Get(0).Width = 20.0!
        TextCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprDetail_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.sprDetail_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprDetail_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.FrozenRowCount = 1
        Me.sprDetail_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail_Sheet1.RowHeader.Cells.Get(0, 0).Value = "ｸﾘｱ"
        Me.sprDetail_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprDetail_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprDetail_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprDetail_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.RowHeader.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprDetail_Sheet1.Rows.Default.Height = 18.0!
        Me.sprDetail_Sheet1.Rows.Default.Resizable = False
        Me.sprDetail_Sheet1.Rows.Default.Visible = True
        Me.sprDetail_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail_Sheet1.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprDetail_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprDetail_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprDetail_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprDetail_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprDetail_Sheet1.SheetCornerStyle.Locked = True
        Me.sprDetail_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprDetail_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprDetail_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprDetail_Sheet1.StartingRowNumber = 0
        Me.sprDetail_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprDetail_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1127, 377)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 382
        Me.lblSituation.TabStop = False
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSizeDef = False
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 18
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1039, 409)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(75, 18)
        Me.LmTitleLabel7.TabIndex = 381
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 75
        '
        'lblCrtUser
        '
        Me.lblCrtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtUser.CountWrappedLine = False
        Me.lblCrtUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtUser.HeightDef = 18
        Me.lblCrtUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.HissuLabelVisible = False
        Me.lblCrtUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtUser.IsByteCheck = 0
        Me.lblCrtUser.IsCalendarCheck = False
        Me.lblCrtUser.IsDakutenCheck = False
        Me.lblCrtUser.IsEisuCheck = False
        Me.lblCrtUser.IsForbiddenWordsCheck = False
        Me.lblCrtUser.IsFullByteCheck = 0
        Me.lblCrtUser.IsHankakuCheck = False
        Me.lblCrtUser.IsHissuCheck = False
        Me.lblCrtUser.IsKanaCheck = False
        Me.lblCrtUser.IsMiddleSpace = False
        Me.lblCrtUser.IsNumericCheck = False
        Me.lblCrtUser.IsSujiCheck = False
        Me.lblCrtUser.IsZenkakuCheck = False
        Me.lblCrtUser.ItemName = ""
        Me.lblCrtUser.LineSpace = 0
        Me.lblCrtUser.Location = New System.Drawing.Point(1120, 410)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 383
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtFax
        '
        Me.txtFax.AutoSize = True
        Me.txtFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFax.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFax.CountWrappedLine = False
        Me.txtFax.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFax.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFax.HeightDef = 18
        Me.txtFax.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFax.HissuLabelVisible = False
        Me.txtFax.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtFax.IsByteCheck = 20
        Me.txtFax.IsCalendarCheck = False
        Me.txtFax.IsDakutenCheck = False
        Me.txtFax.IsEisuCheck = False
        Me.txtFax.IsForbiddenWordsCheck = False
        Me.txtFax.IsFullByteCheck = 0
        Me.txtFax.IsHankakuCheck = False
        Me.txtFax.IsHissuCheck = False
        Me.txtFax.IsKanaCheck = False
        Me.txtFax.IsMiddleSpace = False
        Me.txtFax.IsNumericCheck = False
        Me.txtFax.IsSujiCheck = False
        Me.txtFax.IsZenkakuCheck = False
        Me.txtFax.ItemName = ""
        Me.txtFax.LineSpace = 0
        Me.txtFax.Location = New System.Drawing.Point(447, 545)
        Me.txtFax.MaxLength = 20
        Me.txtFax.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFax.MaxLineCount = 0
        Me.txtFax.Multiline = False
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = False
        Me.txtFax.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFax.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFax.Size = New System.Drawing.Size(173, 18)
        Me.txtFax.TabIndex = 417
        Me.txtFax.TabStopSetting = True
        Me.txtFax.TextValue = ""
        Me.txtFax.UseSystemPasswordChar = False
        Me.txtFax.WidthDef = 173
        Me.txtFax.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSizeDef = False
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 18
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1027, 432)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(87, 18)
        Me.LmTitleLabel8.TabIndex = 384
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 87
        '
        'TitlelblTelTitlelblFax
        '
        Me.TitlelblTelTitlelblFax.AutoSizeDef = False
        Me.TitlelblTelTitlelblFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTelTitlelblFax.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTelTitlelblFax.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblTelTitlelblFax.EnableStatus = False
        Me.TitlelblTelTitlelblFax.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTelTitlelblFax.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTelTitlelblFax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTelTitlelblFax.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTelTitlelblFax.HeightDef = 13
        Me.TitlelblTelTitlelblFax.Location = New System.Drawing.Point(389, 548)
        Me.TitlelblTelTitlelblFax.Name = "TitlelblTelTitlelblFax"
        Me.TitlelblTelTitlelblFax.Size = New System.Drawing.Size(56, 13)
        Me.TitlelblTelTitlelblFax.TabIndex = 416
        Me.TitlelblTelTitlelblFax.Text = "FAX番号"
        Me.TitlelblTelTitlelblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblTelTitlelblFax.TextValue = "FAX番号"
        Me.TitlelblTelTitlelblFax.WidthDef = 56
        '
        'lblCrtDate
        '
        Me.lblCrtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtDate.CountWrappedLine = False
        Me.lblCrtDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtDate.HeightDef = 18
        Me.lblCrtDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.HissuLabelVisible = False
        Me.lblCrtDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtDate.IsByteCheck = 0
        Me.lblCrtDate.IsCalendarCheck = False
        Me.lblCrtDate.IsDakutenCheck = False
        Me.lblCrtDate.IsEisuCheck = False
        Me.lblCrtDate.IsForbiddenWordsCheck = False
        Me.lblCrtDate.IsFullByteCheck = 0
        Me.lblCrtDate.IsHankakuCheck = False
        Me.lblCrtDate.IsHissuCheck = False
        Me.lblCrtDate.IsKanaCheck = False
        Me.lblCrtDate.IsMiddleSpace = False
        Me.lblCrtDate.IsNumericCheck = False
        Me.lblCrtDate.IsSujiCheck = False
        Me.lblCrtDate.IsZenkakuCheck = False
        Me.lblCrtDate.ItemName = ""
        Me.lblCrtDate.LineSpace = 0
        Me.lblCrtDate.Location = New System.Drawing.Point(1120, 432)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 385
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtTel
        '
        Me.txtTel.AutoSize = True
        Me.txtTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTel.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTel.CountWrappedLine = False
        Me.txtTel.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTel.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTel.HeightDef = 18
        Me.txtTel.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTel.HissuLabelVisible = False
        Me.txtTel.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtTel.IsByteCheck = 20
        Me.txtTel.IsCalendarCheck = False
        Me.txtTel.IsDakutenCheck = False
        Me.txtTel.IsEisuCheck = False
        Me.txtTel.IsForbiddenWordsCheck = False
        Me.txtTel.IsFullByteCheck = 0
        Me.txtTel.IsHankakuCheck = False
        Me.txtTel.IsHissuCheck = False
        Me.txtTel.IsKanaCheck = False
        Me.txtTel.IsMiddleSpace = False
        Me.txtTel.IsNumericCheck = False
        Me.txtTel.IsSujiCheck = False
        Me.txtTel.IsZenkakuCheck = False
        Me.txtTel.ItemName = ""
        Me.txtTel.LineSpace = 0
        Me.txtTel.Location = New System.Drawing.Point(447, 524)
        Me.txtTel.MaxLength = 20
        Me.txtTel.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTel.MaxLineCount = 0
        Me.txtTel.Multiline = False
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = False
        Me.txtTel.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTel.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTel.Size = New System.Drawing.Size(173, 18)
        Me.txtTel.TabIndex = 415
        Me.txtTel.TabStopSetting = True
        Me.txtTel.TextValue = ""
        Me.txtTel.UseSystemPasswordChar = False
        Me.txtTel.WidthDef = 173
        Me.txtTel.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSizeDef = False
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 18
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1030, 454)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(84, 18)
        Me.LmTitleLabel9.TabIndex = 386
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 84
        '
        'lblUpdUser
        '
        Me.lblUpdUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdUser.CountWrappedLine = False
        Me.lblUpdUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdUser.HeightDef = 18
        Me.lblUpdUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.HissuLabelVisible = False
        Me.lblUpdUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdUser.IsByteCheck = 0
        Me.lblUpdUser.IsCalendarCheck = False
        Me.lblUpdUser.IsDakutenCheck = False
        Me.lblUpdUser.IsEisuCheck = False
        Me.lblUpdUser.IsForbiddenWordsCheck = False
        Me.lblUpdUser.IsFullByteCheck = 0
        Me.lblUpdUser.IsHankakuCheck = False
        Me.lblUpdUser.IsHissuCheck = False
        Me.lblUpdUser.IsKanaCheck = False
        Me.lblUpdUser.IsMiddleSpace = False
        Me.lblUpdUser.IsNumericCheck = False
        Me.lblUpdUser.IsSujiCheck = False
        Me.lblUpdUser.IsZenkakuCheck = False
        Me.lblUpdUser.ItemName = ""
        Me.lblUpdUser.LineSpace = 0
        Me.lblUpdUser.Location = New System.Drawing.Point(1120, 454)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 387
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSizeDef = False
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 18
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1030, 476)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(84, 18)
        Me.LmTitleLabel10.TabIndex = 388
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 84
        '
        'lblUpdDate
        '
        Me.lblUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdDate.CountWrappedLine = False
        Me.lblUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdDate.HeightDef = 18
        Me.lblUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.HissuLabelVisible = False
        Me.lblUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdDate.IsByteCheck = 0
        Me.lblUpdDate.IsCalendarCheck = False
        Me.lblUpdDate.IsDakutenCheck = False
        Me.lblUpdDate.IsEisuCheck = False
        Me.lblUpdDate.IsForbiddenWordsCheck = False
        Me.lblUpdDate.IsFullByteCheck = 0
        Me.lblUpdDate.IsHankakuCheck = False
        Me.lblUpdDate.IsHissuCheck = False
        Me.lblUpdDate.IsKanaCheck = False
        Me.lblUpdDate.IsMiddleSpace = False
        Me.lblUpdDate.IsNumericCheck = False
        Me.lblUpdDate.IsSujiCheck = False
        Me.lblUpdDate.IsZenkakuCheck = False
        Me.lblUpdDate.ItemName = ""
        Me.lblUpdDate.LineSpace = 0
        Me.lblUpdDate.Location = New System.Drawing.Point(1120, 476)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 389
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtAd3
        '
        Me.txtAd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd3.CountWrappedLine = False
        Me.txtAd3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd3.HeightDef = 18
        Me.txtAd3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd3.HissuLabelVisible = False
        Me.txtAd3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd3.IsByteCheck = 40
        Me.txtAd3.IsCalendarCheck = False
        Me.txtAd3.IsDakutenCheck = False
        Me.txtAd3.IsEisuCheck = False
        Me.txtAd3.IsForbiddenWordsCheck = False
        Me.txtAd3.IsFullByteCheck = 0
        Me.txtAd3.IsHankakuCheck = False
        Me.txtAd3.IsHissuCheck = False
        Me.txtAd3.IsKanaCheck = False
        Me.txtAd3.IsMiddleSpace = False
        Me.txtAd3.IsNumericCheck = False
        Me.txtAd3.IsSujiCheck = False
        Me.txtAd3.IsZenkakuCheck = False
        Me.txtAd3.ItemName = ""
        Me.txtAd3.LineSpace = 0
        Me.txtAd3.Location = New System.Drawing.Point(740, 546)
        Me.txtAd3.MaxLength = 40
        Me.txtAd3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd3.MaxLineCount = 0
        Me.txtAd3.Multiline = False
        Me.txtAd3.Name = "txtAd3"
        Me.txtAd3.ReadOnly = False
        Me.txtAd3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd3.Size = New System.Drawing.Size(324, 18)
        Me.txtAd3.TabIndex = 19
        Me.txtAd3.TabStopSetting = True
        Me.txtAd3.TextValue = ""
        Me.txtAd3.UseSystemPasswordChar = False
        Me.txtAd3.WidthDef = 324
        Me.txtAd3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblAd3
        '
        Me.TitlelblAd3.AutoSizeDef = False
        Me.TitlelblAd3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblAd3.EnableStatus = False
        Me.TitlelblAd3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd3.HeightDef = 15
        Me.TitlelblAd3.Location = New System.Drawing.Point(658, 546)
        Me.TitlelblAd3.Name = "TitlelblAd3"
        Me.TitlelblAd3.Size = New System.Drawing.Size(80, 15)
        Me.TitlelblAd3.TabIndex = 410
        Me.TitlelblAd3.Text = "住所3"
        Me.TitlelblAd3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblAd3.TextValue = "住所3"
        Me.TitlelblAd3.WidthDef = 80
        '
        'txtAd2
        '
        Me.txtAd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd2.CountWrappedLine = False
        Me.txtAd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd2.HeightDef = 18
        Me.txtAd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd2.HissuLabelVisible = False
        Me.txtAd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd2.IsByteCheck = 40
        Me.txtAd2.IsCalendarCheck = False
        Me.txtAd2.IsDakutenCheck = False
        Me.txtAd2.IsEisuCheck = False
        Me.txtAd2.IsForbiddenWordsCheck = False
        Me.txtAd2.IsFullByteCheck = 0
        Me.txtAd2.IsHankakuCheck = False
        Me.txtAd2.IsHissuCheck = False
        Me.txtAd2.IsKanaCheck = False
        Me.txtAd2.IsMiddleSpace = False
        Me.txtAd2.IsNumericCheck = False
        Me.txtAd2.IsSujiCheck = False
        Me.txtAd2.IsZenkakuCheck = False
        Me.txtAd2.ItemName = ""
        Me.txtAd2.LineSpace = 0
        Me.txtAd2.Location = New System.Drawing.Point(740, 525)
        Me.txtAd2.MaxLength = 40
        Me.txtAd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd2.MaxLineCount = 0
        Me.txtAd2.Multiline = False
        Me.txtAd2.Name = "txtAd2"
        Me.txtAd2.ReadOnly = False
        Me.txtAd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd2.Size = New System.Drawing.Size(324, 18)
        Me.txtAd2.TabIndex = 18
        Me.txtAd2.TabStopSetting = True
        Me.txtAd2.TextValue = ""
        Me.txtAd2.UseSystemPasswordChar = False
        Me.txtAd2.WidthDef = 324
        Me.txtAd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblAd2
        '
        Me.TitlelblAd2.AutoSizeDef = False
        Me.TitlelblAd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblAd2.EnableStatus = False
        Me.TitlelblAd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd2.HeightDef = 16
        Me.TitlelblAd2.Location = New System.Drawing.Point(667, 525)
        Me.TitlelblAd2.Name = "TitlelblAd2"
        Me.TitlelblAd2.Size = New System.Drawing.Size(71, 16)
        Me.TitlelblAd2.TabIndex = 408
        Me.TitlelblAd2.Text = "住所2"
        Me.TitlelblAd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblAd2.TextValue = "住所2"
        Me.TitlelblAd2.WidthDef = 71
        '
        'txtAd1
        '
        Me.txtAd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAd1.CountWrappedLine = False
        Me.txtAd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAd1.HeightDef = 18
        Me.txtAd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAd1.HissuLabelVisible = False
        Me.txtAd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAd1.IsByteCheck = 40
        Me.txtAd1.IsCalendarCheck = False
        Me.txtAd1.IsDakutenCheck = False
        Me.txtAd1.IsEisuCheck = False
        Me.txtAd1.IsForbiddenWordsCheck = False
        Me.txtAd1.IsFullByteCheck = 0
        Me.txtAd1.IsHankakuCheck = False
        Me.txtAd1.IsHissuCheck = False
        Me.txtAd1.IsKanaCheck = False
        Me.txtAd1.IsMiddleSpace = False
        Me.txtAd1.IsNumericCheck = False
        Me.txtAd1.IsSujiCheck = False
        Me.txtAd1.IsZenkakuCheck = False
        Me.txtAd1.ItemName = ""
        Me.txtAd1.LineSpace = 0
        Me.txtAd1.Location = New System.Drawing.Point(740, 504)
        Me.txtAd1.MaxLength = 40
        Me.txtAd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAd1.MaxLineCount = 0
        Me.txtAd1.Multiline = False
        Me.txtAd1.Name = "txtAd1"
        Me.txtAd1.ReadOnly = False
        Me.txtAd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAd1.Size = New System.Drawing.Size(324, 18)
        Me.txtAd1.TabIndex = 17
        Me.txtAd1.TabStopSetting = True
        Me.txtAd1.TextValue = ""
        Me.txtAd1.UseSystemPasswordChar = False
        Me.txtAd1.WidthDef = 324
        Me.txtAd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblAd1
        '
        Me.TitlelblAd1.AutoSizeDef = False
        Me.TitlelblAd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblAd1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblAd1.EnableStatus = False
        Me.TitlelblAd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblAd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblAd1.HeightDef = 13
        Me.TitlelblAd1.Location = New System.Drawing.Point(677, 507)
        Me.TitlelblAd1.Name = "TitlelblAd1"
        Me.TitlelblAd1.Size = New System.Drawing.Size(61, 13)
        Me.TitlelblAd1.TabIndex = 406
        Me.TitlelblAd1.Text = "住所1"
        Me.TitlelblAd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblAd1.TextValue = "住所1"
        Me.TitlelblAd1.WidthDef = 61
        '
        'TitlelblTel
        '
        Me.TitlelblTel.AutoSizeDef = False
        Me.TitlelblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblTel.EnableStatus = False
        Me.TitlelblTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTel.HeightDef = 13
        Me.TitlelblTel.Location = New System.Drawing.Point(382, 527)
        Me.TitlelblTel.Name = "TitlelblTel"
        Me.TitlelblTel.Size = New System.Drawing.Size(63, 13)
        Me.TitlelblTel.TabIndex = 390
        Me.TitlelblTel.Text = "電話番号"
        Me.TitlelblTel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblTel.TextValue = "電話番号"
        Me.TitlelblTel.WidthDef = 63
        '
        'TitlelblDocPtn
        '
        Me.TitlelblDocPtn.AutoSize = True
        Me.TitlelblDocPtn.AutoSizeDef = True
        Me.TitlelblDocPtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocPtn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocPtn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblDocPtn.EnableStatus = False
        Me.TitlelblDocPtn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocPtn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocPtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocPtn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocPtn.HeightDef = 13
        Me.TitlelblDocPtn.Location = New System.Drawing.Point(49, 835)
        Me.TitlelblDocPtn.Name = "TitlelblDocPtn"
        Me.TitlelblDocPtn.Size = New System.Drawing.Size(147, 13)
        Me.TitlelblDocPtn.TabIndex = 392
        Me.TitlelblDocPtn.Text = "請求書パターン(値引)"
        Me.TitlelblDocPtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblDocPtn.TextValue = "請求書パターン(値引)"
        Me.TitlelblDocPtn.WidthDef = 147
        '
        'TitlelblKagamiKB
        '
        Me.TitlelblKagamiKB.AutoSizeDef = False
        Me.TitlelblKagamiKB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblKagamiKB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblKagamiKB.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblKagamiKB.EnableStatus = False
        Me.TitlelblKagamiKB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblKagamiKB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblKagamiKB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblKagamiKB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblKagamiKB.HeightDef = 13
        Me.TitlelblKagamiKB.Location = New System.Drawing.Point(77, 462)
        Me.TitlelblKagamiKB.Name = "TitlelblKagamiKB"
        Me.TitlelblKagamiKB.Size = New System.Drawing.Size(77, 13)
        Me.TitlelblKagamiKB.TabIndex = 391
        Me.TitlelblKagamiKB.Text = "鑑口座区分"
        Me.TitlelblKagamiKB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblKagamiKB.TextValue = "鑑口座区分"
        Me.TitlelblKagamiKB.WidthDef = 77
        '
        'TitlelblSeiqNm
        '
        Me.TitlelblSeiqNm.AutoSizeDef = False
        Me.TitlelblSeiqNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblSeiqNm.EnableStatus = False
        Me.TitlelblSeiqNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqNm.HeightDef = 15
        Me.TitlelblSeiqNm.Location = New System.Drawing.Point(38, 419)
        Me.TitlelblSeiqNm.Name = "TitlelblSeiqNm"
        Me.TitlelblSeiqNm.Size = New System.Drawing.Size(116, 15)
        Me.TitlelblSeiqNm.TabIndex = 404
        Me.TitlelblSeiqNm.Text = "請求先会社名"
        Me.TitlelblSeiqNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblSeiqNm.TextValue = "請求先会社名"
        Me.TitlelblSeiqNm.WidthDef = 116
        '
        'TitlelblSeiqCd
        '
        Me.TitlelblSeiqCd.AutoSizeDef = False
        Me.TitlelblSeiqCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblSeiqCd.EnableStatus = False
        Me.TitlelblSeiqCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqCd.HeightDef = 15
        Me.TitlelblSeiqCd.Location = New System.Drawing.Point(38, 398)
        Me.TitlelblSeiqCd.Name = "TitlelblSeiqCd"
        Me.TitlelblSeiqCd.Size = New System.Drawing.Size(116, 15)
        Me.TitlelblSeiqCd.TabIndex = 397
        Me.TitlelblSeiqCd.Text = "請求先コード"
        Me.TitlelblSeiqCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblSeiqCd.TextValue = "請求先コード"
        Me.TitlelblSeiqCd.WidthDef = 116
        '
        'txtSeiqtoCd
        '
        Me.txtSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoCd.CountWrappedLine = False
        Me.txtSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoCd.HeightDef = 18
        Me.txtSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoCd.HissuLabelVisible = True
        Me.txtSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSeiqtoCd.IsByteCheck = 7
        Me.txtSeiqtoCd.IsCalendarCheck = False
        Me.txtSeiqtoCd.IsDakutenCheck = False
        Me.txtSeiqtoCd.IsEisuCheck = False
        Me.txtSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtSeiqtoCd.IsFullByteCheck = 0
        Me.txtSeiqtoCd.IsHankakuCheck = False
        Me.txtSeiqtoCd.IsHissuCheck = True
        Me.txtSeiqtoCd.IsKanaCheck = False
        Me.txtSeiqtoCd.IsMiddleSpace = False
        Me.txtSeiqtoCd.IsNumericCheck = False
        Me.txtSeiqtoCd.IsSujiCheck = False
        Me.txtSeiqtoCd.IsZenkakuCheck = False
        Me.txtSeiqtoCd.ItemName = ""
        Me.txtSeiqtoCd.LineSpace = 0
        Me.txtSeiqtoCd.Location = New System.Drawing.Point(156, 398)
        Me.txtSeiqtoCd.MaxLength = 7
        Me.txtSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoCd.MaxLineCount = 0
        Me.txtSeiqtoCd.Multiline = False
        Me.txtSeiqtoCd.Name = "txtSeiqtoCd"
        Me.txtSeiqtoCd.ReadOnly = False
        Me.txtSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoCd.Size = New System.Drawing.Size(82, 18)
        Me.txtSeiqtoCd.TabIndex = 457
        Me.txtSeiqtoCd.TabStopSetting = True
        Me.txtSeiqtoCd.TextValue = "XXXXXXX"
        Me.txtSeiqtoCd.UseSystemPasswordChar = False
        Me.txtSeiqtoCd.WidthDef = 82
        Me.txtSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqtoNm
        '
        Me.txtSeiqtoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoNm.CountWrappedLine = False
        Me.txtSeiqtoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoNm.HeightDef = 18
        Me.txtSeiqtoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoNm.HissuLabelVisible = True
        Me.txtSeiqtoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSeiqtoNm.IsByteCheck = 60
        Me.txtSeiqtoNm.IsCalendarCheck = False
        Me.txtSeiqtoNm.IsDakutenCheck = False
        Me.txtSeiqtoNm.IsEisuCheck = False
        Me.txtSeiqtoNm.IsForbiddenWordsCheck = False
        Me.txtSeiqtoNm.IsFullByteCheck = 0
        Me.txtSeiqtoNm.IsHankakuCheck = False
        Me.txtSeiqtoNm.IsHissuCheck = True
        Me.txtSeiqtoNm.IsKanaCheck = False
        Me.txtSeiqtoNm.IsMiddleSpace = False
        Me.txtSeiqtoNm.IsNumericCheck = False
        Me.txtSeiqtoNm.IsSujiCheck = False
        Me.txtSeiqtoNm.IsZenkakuCheck = False
        Me.txtSeiqtoNm.ItemName = ""
        Me.txtSeiqtoNm.LineSpace = 0
        Me.txtSeiqtoNm.Location = New System.Drawing.Point(156, 419)
        Me.txtSeiqtoNm.MaxLength = 60
        Me.txtSeiqtoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoNm.MaxLineCount = 0
        Me.txtSeiqtoNm.Multiline = False
        Me.txtSeiqtoNm.Name = "txtSeiqtoNm"
        Me.txtSeiqtoNm.ReadOnly = False
        Me.txtSeiqtoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoNm.Size = New System.Drawing.Size(532, 18)
        Me.txtSeiqtoNm.TabIndex = 458
        Me.txtSeiqtoNm.TabStopSetting = True
        Me.txtSeiqtoNm.TextValue = ""
        Me.txtSeiqtoNm.UseSystemPasswordChar = False
        Me.txtSeiqtoNm.WidthDef = 532
        Me.txtSeiqtoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNrsKeiriCd1
        '
        Me.lblTitleNrsKeiriCd1.AutoSizeDef = False
        Me.lblTitleNrsKeiriCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsKeiriCd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsKeiriCd1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNrsKeiriCd1.EnableStatus = False
        Me.lblTitleNrsKeiriCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsKeiriCd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsKeiriCd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsKeiriCd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsKeiriCd1.HeightDef = 13
        Me.lblTitleNrsKeiriCd1.Location = New System.Drawing.Point(17, 506)
        Me.lblTitleNrsKeiriCd1.Name = "lblTitleNrsKeiriCd1"
        Me.lblTitleNrsKeiriCd1.Size = New System.Drawing.Size(137, 13)
        Me.lblTitleNrsKeiriCd1.TabIndex = 460
        Me.lblTitleNrsKeiriCd1.Text = "親請求先コード"
        Me.lblTitleNrsKeiriCd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNrsKeiriCd1.TextValue = "親請求先コード"
        Me.lblTitleNrsKeiriCd1.WidthDef = 137
        '
        'TitlelblNrsCd
        '
        Me.TitlelblNrsCd.AutoSizeDef = False
        Me.TitlelblNrsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNrsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNrsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblNrsCd.EnableStatus = False
        Me.TitlelblNrsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNrsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNrsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNrsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNrsCd.HeightDef = 13
        Me.TitlelblNrsCd.Location = New System.Drawing.Point(14, 526)
        Me.TitlelblNrsCd.Name = "TitlelblNrsCd"
        Me.TitlelblNrsCd.Size = New System.Drawing.Size(140, 13)
        Me.TitlelblNrsCd.TabIndex = 461
        Me.TitlelblNrsCd.Text = "日陸経理コード(JDE)"
        Me.TitlelblNrsCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblNrsCd.TextValue = "日陸経理コード(JDE)"
        Me.TitlelblNrsCd.WidthDef = 140
        '
        'TitlelblSentPeriod
        '
        Me.TitlelblSentPeriod.AutoSizeDef = False
        Me.TitlelblSentPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSentPeriod.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSentPeriod.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblSentPeriod.EnableStatus = False
        Me.TitlelblSentPeriod.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSentPeriod.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSentPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSentPeriod.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSentPeriod.HeightDef = 15
        Me.TitlelblSentPeriod.Location = New System.Drawing.Point(12, 545)
        Me.TitlelblSentPeriod.Name = "TitlelblSentPeriod"
        Me.TitlelblSentPeriod.Size = New System.Drawing.Size(142, 15)
        Me.TitlelblSentPeriod.TabIndex = 462
        Me.TitlelblSentPeriod.Text = "請求書・送付期限"
        Me.TitlelblSentPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblSentPeriod.TextValue = "請求書・送付期限"
        Me.TitlelblSentPeriod.WidthDef = 142
        '
        'TitlelblCloseKbn
        '
        Me.TitlelblCloseKbn.AutoSizeDef = False
        Me.TitlelblCloseKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCloseKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCloseKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCloseKbn.EnableStatus = False
        Me.TitlelblCloseKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCloseKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCloseKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCloseKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCloseKbn.HeightDef = 15
        Me.TitlelblCloseKbn.Location = New System.Drawing.Point(690, 444)
        Me.TitlelblCloseKbn.Name = "TitlelblCloseKbn"
        Me.TitlelblCloseKbn.Size = New System.Drawing.Size(87, 15)
        Me.TitlelblCloseKbn.TabIndex = 464
        Me.TitlelblCloseKbn.Text = "締日区分"
        Me.TitlelblCloseKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCloseKbn.TextValue = "締日区分"
        Me.TitlelblCloseKbn.WidthDef = 87
        '
        'cmbCloseKBN
        '
        Me.cmbCloseKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCloseKBN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCloseKBN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCloseKBN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCloseKBN.DataCode = "S008"
        Me.cmbCloseKBN.DataSource = Nothing
        Me.cmbCloseKBN.DisplayMember = Nothing
        Me.cmbCloseKBN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCloseKBN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCloseKBN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCloseKBN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCloseKBN.HeightDef = 18
        Me.cmbCloseKBN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCloseKBN.HissuLabelVisible = True
        Me.cmbCloseKBN.InsertWildCard = True
        Me.cmbCloseKBN.IsForbiddenWordsCheck = False
        Me.cmbCloseKBN.IsHissuCheck = True
        Me.cmbCloseKBN.ItemName = ""
        Me.cmbCloseKBN.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCloseKBN.Location = New System.Drawing.Point(779, 444)
        Me.cmbCloseKBN.Name = "cmbCloseKBN"
        Me.cmbCloseKBN.ReadOnly = False
        Me.cmbCloseKBN.SelectedIndex = -1
        Me.cmbCloseKBN.SelectedItem = Nothing
        Me.cmbCloseKBN.SelectedText = ""
        Me.cmbCloseKBN.SelectedValue = ""
        Me.cmbCloseKBN.Size = New System.Drawing.Size(173, 18)
        Me.cmbCloseKBN.TabIndex = 15
        Me.cmbCloseKBN.TabStopSetting = True
        Me.cmbCloseKBN.TextValue = ""
        Me.cmbCloseKBN.Value1 = Nothing
        Me.cmbCloseKBN.Value2 = Nothing
        Me.cmbCloseKBN.Value3 = Nothing
        Me.cmbCloseKBN.ValueMember = Nothing
        Me.cmbCloseKBN.WidthDef = 173
        '
        'txtNrsKeiriCd1
        '
        Me.txtNrsKeiriCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNrsKeiriCd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNrsKeiriCd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNrsKeiriCd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNrsKeiriCd1.CountWrappedLine = False
        Me.txtNrsKeiriCd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNrsKeiriCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsKeiriCd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsKeiriCd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsKeiriCd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsKeiriCd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNrsKeiriCd1.HeightDef = 18
        Me.txtNrsKeiriCd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNrsKeiriCd1.HissuLabelVisible = True
        Me.txtNrsKeiriCd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtNrsKeiriCd1.IsByteCheck = 7
        Me.txtNrsKeiriCd1.IsCalendarCheck = False
        Me.txtNrsKeiriCd1.IsDakutenCheck = False
        Me.txtNrsKeiriCd1.IsEisuCheck = False
        Me.txtNrsKeiriCd1.IsForbiddenWordsCheck = False
        Me.txtNrsKeiriCd1.IsFullByteCheck = 0
        Me.txtNrsKeiriCd1.IsHankakuCheck = False
        Me.txtNrsKeiriCd1.IsHissuCheck = True
        Me.txtNrsKeiriCd1.IsKanaCheck = False
        Me.txtNrsKeiriCd1.IsMiddleSpace = False
        Me.txtNrsKeiriCd1.IsNumericCheck = False
        Me.txtNrsKeiriCd1.IsSujiCheck = False
        Me.txtNrsKeiriCd1.IsZenkakuCheck = False
        Me.txtNrsKeiriCd1.ItemName = ""
        Me.txtNrsKeiriCd1.LineSpace = 0
        Me.txtNrsKeiriCd1.Location = New System.Drawing.Point(156, 503)
        Me.txtNrsKeiriCd1.MaxLength = 7
        Me.txtNrsKeiriCd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNrsKeiriCd1.MaxLineCount = 0
        Me.txtNrsKeiriCd1.Multiline = False
        Me.txtNrsKeiriCd1.Name = "txtNrsKeiriCd1"
        Me.txtNrsKeiriCd1.ReadOnly = False
        Me.txtNrsKeiriCd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNrsKeiriCd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNrsKeiriCd1.Size = New System.Drawing.Size(82, 18)
        Me.txtNrsKeiriCd1.TabIndex = 468
        Me.txtNrsKeiriCd1.TabStopSetting = True
        Me.txtNrsKeiriCd1.TextValue = "XXXXXXX"
        Me.txtNrsKeiriCd1.UseSystemPasswordChar = False
        Me.txtNrsKeiriCd1.WidthDef = 82
        Me.txtNrsKeiriCd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtNrsKeiriCd2
        '
        Me.txtNrsKeiriCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNrsKeiriCd2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNrsKeiriCd2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNrsKeiriCd2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNrsKeiriCd2.CountWrappedLine = False
        Me.txtNrsKeiriCd2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNrsKeiriCd2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsKeiriCd2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsKeiriCd2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsKeiriCd2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsKeiriCd2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNrsKeiriCd2.HeightDef = 18
        Me.txtNrsKeiriCd2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNrsKeiriCd2.HissuLabelVisible = True
        Me.txtNrsKeiriCd2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtNrsKeiriCd2.IsByteCheck = 10
        Me.txtNrsKeiriCd2.IsCalendarCheck = False
        Me.txtNrsKeiriCd2.IsDakutenCheck = False
        Me.txtNrsKeiriCd2.IsEisuCheck = False
        Me.txtNrsKeiriCd2.IsForbiddenWordsCheck = False
        Me.txtNrsKeiriCd2.IsFullByteCheck = 0
        Me.txtNrsKeiriCd2.IsHankakuCheck = False
        Me.txtNrsKeiriCd2.IsHissuCheck = True
        Me.txtNrsKeiriCd2.IsKanaCheck = False
        Me.txtNrsKeiriCd2.IsMiddleSpace = False
        Me.txtNrsKeiriCd2.IsNumericCheck = False
        Me.txtNrsKeiriCd2.IsSujiCheck = False
        Me.txtNrsKeiriCd2.IsZenkakuCheck = False
        Me.txtNrsKeiriCd2.ItemName = ""
        Me.txtNrsKeiriCd2.LineSpace = 0
        Me.txtNrsKeiriCd2.Location = New System.Drawing.Point(156, 524)
        Me.txtNrsKeiriCd2.MaxLength = 10
        Me.txtNrsKeiriCd2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNrsKeiriCd2.MaxLineCount = 0
        Me.txtNrsKeiriCd2.Multiline = False
        Me.txtNrsKeiriCd2.Name = "txtNrsKeiriCd2"
        Me.txtNrsKeiriCd2.ReadOnly = False
        Me.txtNrsKeiriCd2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNrsKeiriCd2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNrsKeiriCd2.Size = New System.Drawing.Size(96, 18)
        Me.txtNrsKeiriCd2.TabIndex = 469
        Me.txtNrsKeiriCd2.TabStopSetting = True
        Me.txtNrsKeiriCd2.TextValue = "XXXXXXXXXX"
        Me.txtNrsKeiriCd2.UseSystemPasswordChar = False
        Me.txtNrsKeiriCd2.WidthDef = 96
        Me.txtNrsKeiriCd2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqSndPeriod
        '
        Me.txtSeiqSndPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqSndPeriod.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqSndPeriod.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqSndPeriod.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqSndPeriod.CountWrappedLine = False
        Me.txtSeiqSndPeriod.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqSndPeriod.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqSndPeriod.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqSndPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqSndPeriod.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqSndPeriod.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqSndPeriod.HeightDef = 18
        Me.txtSeiqSndPeriod.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqSndPeriod.HissuLabelVisible = False
        Me.txtSeiqSndPeriod.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSeiqSndPeriod.IsByteCheck = 20
        Me.txtSeiqSndPeriod.IsCalendarCheck = False
        Me.txtSeiqSndPeriod.IsDakutenCheck = False
        Me.txtSeiqSndPeriod.IsEisuCheck = False
        Me.txtSeiqSndPeriod.IsForbiddenWordsCheck = False
        Me.txtSeiqSndPeriod.IsFullByteCheck = 0
        Me.txtSeiqSndPeriod.IsHankakuCheck = False
        Me.txtSeiqSndPeriod.IsHissuCheck = False
        Me.txtSeiqSndPeriod.IsKanaCheck = False
        Me.txtSeiqSndPeriod.IsMiddleSpace = False
        Me.txtSeiqSndPeriod.IsNumericCheck = False
        Me.txtSeiqSndPeriod.IsSujiCheck = False
        Me.txtSeiqSndPeriod.IsZenkakuCheck = False
        Me.txtSeiqSndPeriod.ItemName = ""
        Me.txtSeiqSndPeriod.LineSpace = 0
        Me.txtSeiqSndPeriod.Location = New System.Drawing.Point(156, 545)
        Me.txtSeiqSndPeriod.MaxLength = 20
        Me.txtSeiqSndPeriod.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqSndPeriod.MaxLineCount = 0
        Me.txtSeiqSndPeriod.Multiline = False
        Me.txtSeiqSndPeriod.Name = "txtSeiqSndPeriod"
        Me.txtSeiqSndPeriod.ReadOnly = False
        Me.txtSeiqSndPeriod.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqSndPeriod.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqSndPeriod.Size = New System.Drawing.Size(173, 18)
        Me.txtSeiqSndPeriod.TabIndex = 470
        Me.txtSeiqSndPeriod.TabStopSetting = True
        Me.txtSeiqSndPeriod.TextValue = ""
        Me.txtSeiqSndPeriod.UseSystemPasswordChar = False
        Me.txtSeiqSndPeriod.WidthDef = 173
        Me.txtSeiqSndPeriod.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelbPic
        '
        Me.TitlelbPic.AutoSizeDef = False
        Me.TitlelbPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelbPic.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelbPic.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelbPic.EnableStatus = False
        Me.TitlelbPic.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelbPic.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelbPic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelbPic.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelbPic.HeightDef = 13
        Me.TitlelbPic.Location = New System.Drawing.Point(382, 505)
        Me.TitlelbPic.Name = "TitlelbPic"
        Me.TitlelbPic.Size = New System.Drawing.Size(63, 13)
        Me.TitlelbPic.TabIndex = 471
        Me.TitlelbPic.Text = "担当者名"
        Me.TitlelbPic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelbPic.TextValue = "担当者名"
        Me.TitlelbPic.WidthDef = 63
        '
        'txtOyaPic
        '
        Me.txtOyaPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOyaPic.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOyaPic.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOyaPic.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOyaPic.CountWrappedLine = False
        Me.txtOyaPic.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOyaPic.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOyaPic.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOyaPic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOyaPic.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOyaPic.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOyaPic.HeightDef = 18
        Me.txtOyaPic.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOyaPic.HissuLabelVisible = False
        Me.txtOyaPic.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOyaPic.IsByteCheck = 20
        Me.txtOyaPic.IsCalendarCheck = False
        Me.txtOyaPic.IsDakutenCheck = False
        Me.txtOyaPic.IsEisuCheck = False
        Me.txtOyaPic.IsForbiddenWordsCheck = False
        Me.txtOyaPic.IsFullByteCheck = 0
        Me.txtOyaPic.IsHankakuCheck = False
        Me.txtOyaPic.IsHissuCheck = False
        Me.txtOyaPic.IsKanaCheck = False
        Me.txtOyaPic.IsMiddleSpace = False
        Me.txtOyaPic.IsNumericCheck = False
        Me.txtOyaPic.IsSujiCheck = False
        Me.txtOyaPic.IsZenkakuCheck = False
        Me.txtOyaPic.ItemName = ""
        Me.txtOyaPic.LineSpace = 0
        Me.txtOyaPic.Location = New System.Drawing.Point(447, 503)
        Me.txtOyaPic.MaxLength = 20
        Me.txtOyaPic.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOyaPic.MaxLineCount = 0
        Me.txtOyaPic.Multiline = False
        Me.txtOyaPic.Name = "txtOyaPic"
        Me.txtOyaPic.ReadOnly = False
        Me.txtOyaPic.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOyaPic.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOyaPic.Size = New System.Drawing.Size(173, 18)
        Me.txtOyaPic.TabIndex = 472
        Me.txtOyaPic.TabStopSetting = True
        Me.txtOyaPic.TextValue = ""
        Me.txtOyaPic.UseSystemPasswordChar = False
        Me.txtOyaPic.WidthDef = 173
        Me.txtOyaPic.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleZip
        '
        Me.lblTitleZip.AutoSizeDef = False
        Me.lblTitleZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZip.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZip.EnableStatus = False
        Me.lblTitleZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZip.HeightDef = 14
        Me.lblTitleZip.Location = New System.Drawing.Point(658, 485)
        Me.lblTitleZip.Name = "lblTitleZip"
        Me.lblTitleZip.Size = New System.Drawing.Size(80, 14)
        Me.lblTitleZip.TabIndex = 475
        Me.lblTitleZip.Text = "郵便番号"
        Me.lblTitleZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZip.TextValue = "郵便番号"
        Me.lblTitleZip.WidthDef = 80
        '
        'txtZip
        '
        Me.txtZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZip.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZip.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtZip.CountWrappedLine = False
        Me.txtZip.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZip.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtZip.HeightDef = 18
        Me.txtZip.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtZip.HissuLabelVisible = False
        Me.txtZip.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtZip.IsByteCheck = 7
        Me.txtZip.IsCalendarCheck = False
        Me.txtZip.IsDakutenCheck = False
        Me.txtZip.IsEisuCheck = False
        Me.txtZip.IsForbiddenWordsCheck = False
        Me.txtZip.IsFullByteCheck = 0
        Me.txtZip.IsHankakuCheck = False
        Me.txtZip.IsHissuCheck = False
        Me.txtZip.IsKanaCheck = False
        Me.txtZip.IsMiddleSpace = False
        Me.txtZip.IsNumericCheck = False
        Me.txtZip.IsSujiCheck = False
        Me.txtZip.IsZenkakuCheck = False
        Me.txtZip.ItemName = ""
        Me.txtZip.LineSpace = 0
        Me.txtZip.Location = New System.Drawing.Point(740, 483)
        Me.txtZip.MaxLength = 7
        Me.txtZip.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtZip.MaxLineCount = 0
        Me.txtZip.Multiline = False
        Me.txtZip.Name = "txtZip"
        Me.txtZip.ReadOnly = False
        Me.txtZip.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtZip.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtZip.Size = New System.Drawing.Size(117, 18)
        Me.txtZip.TabIndex = 16
        Me.txtZip.TabStopSetting = True
        Me.txtZip.TextValue = ""
        Me.txtZip.UseSystemPasswordChar = False
        Me.txtZip.WidthDef = 117
        Me.txtZip.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel2
        '
        Me.LmTitleLabel2.AutoSize = True
        Me.LmTitleLabel2.AutoSizeDef = True
        Me.LmTitleLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel2.EnableStatus = False
        Me.LmTitleLabel2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.HeightDef = 13
        Me.LmTitleLabel2.Location = New System.Drawing.Point(163, 14)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(52, 13)
        Me.LmTitleLabel2.TabIndex = 477
        Me.LmTitleLabel2.Text = "保管料"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel2.TextValue = "保管料"
        Me.LmTitleLabel2.WidthDef = 52
        '
        'TitlelblNr
        '
        Me.TitlelblNr.AutoSizeDef = False
        Me.TitlelblNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblNr.EnableStatus = False
        Me.TitlelblNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNr.HeightDef = 13
        Me.TitlelblNr.Location = New System.Drawing.Point(52, 34)
        Me.TitlelblNr.Name = "TitlelblNr"
        Me.TitlelblNr.Size = New System.Drawing.Size(107, 13)
        Me.TitlelblNr.TabIndex = 485
        Me.TitlelblNr.Text = "値引率"
        Me.TitlelblNr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblNr.TextValue = "値引率"
        Me.TitlelblNr.WidthDef = 107
        '
        'TitlelblNg
        '
        Me.TitlelblNg.AutoSizeDef = False
        Me.TitlelblNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblNg.EnableStatus = False
        Me.TitlelblNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNg.HeightDef = 14
        Me.TitlelblNg.Location = New System.Drawing.Point(57, 54)
        Me.TitlelblNg.Name = "TitlelblNg"
        Me.TitlelblNg.Size = New System.Drawing.Size(102, 14)
        Me.TitlelblNg.TabIndex = 487
        Me.TitlelblNg.Text = "値引額"
        Me.TitlelblNg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblNg.TextValue = "値引額"
        Me.TitlelblNg.WidthDef = 102
        '
        'numStorageNr
        '
        Me.numStorageNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStorageNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStorageNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageNr.HeightDef = 18
        Me.numStorageNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStorageNr.HissuLabelVisible = False
        Me.numStorageNr.IsHissuCheck = False
        Me.numStorageNr.IsRangeCheck = False
        Me.numStorageNr.ItemName = ""
        Me.numStorageNr.Location = New System.Drawing.Point(161, 31)
        Me.numStorageNr.Name = "numStorageNr"
        Me.numStorageNr.ReadOnly = False
        Me.numStorageNr.Size = New System.Drawing.Size(80, 18)
        Me.numStorageNr.TabIndex = 488
        Me.numStorageNr.TabStopSetting = True
        Me.numStorageNr.Tag = ""
        Me.numStorageNr.TextValue = "0"
        Me.numStorageNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStorageNr.WidthDef = 80
        '
        'TitlelblMin
        '
        Me.TitlelblMin.AutoSizeDef = False
        Me.TitlelblMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblMin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblMin.EnableStatus = False
        Me.TitlelblMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblMin.HeightDef = 13
        Me.TitlelblMin.Location = New System.Drawing.Point(1007, 79)
        Me.TitlelblMin.Name = "TitlelblMin"
        Me.TitlelblMin.Size = New System.Drawing.Size(107, 13)
        Me.TitlelblMin.TabIndex = 489
        Me.TitlelblMin.Text = "最低保証額"
        Me.TitlelblMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblMin.TextValue = "最低保証額"
        Me.TitlelblMin.Visible = False
        Me.TitlelblMin.WidthDef = 107
        '
        'numStorageNg
        '
        Me.numStorageNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStorageNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStorageNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageNg.HeightDef = 18
        Me.numStorageNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStorageNg.HissuLabelVisible = False
        Me.numStorageNg.IsHissuCheck = False
        Me.numStorageNg.IsRangeCheck = False
        Me.numStorageNg.ItemName = ""
        Me.numStorageNg.Location = New System.Drawing.Point(161, 52)
        Me.numStorageNg.Name = "numStorageNg"
        Me.numStorageNg.ReadOnly = False
        Me.numStorageNg.Size = New System.Drawing.Size(100, 18)
        Me.numStorageNg.TabIndex = 492
        Me.numStorageNg.TabStopSetting = True
        Me.numStorageNg.Tag = ""
        Me.numStorageNg.TextValue = "0"
        Me.numStorageNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStorageNg.WidthDef = 100
        '
        'numStorageMinBak
        '
        Me.numStorageMinBak.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageMinBak.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageMinBak.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStorageMinBak.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStorageMinBak.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageMinBak.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageMinBak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageMinBak.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageMinBak.HeightDef = 18
        Me.numStorageMinBak.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStorageMinBak.HissuLabelVisible = False
        Me.numStorageMinBak.IsHissuCheck = False
        Me.numStorageMinBak.IsRangeCheck = False
        Me.numStorageMinBak.ItemName = ""
        Me.numStorageMinBak.Location = New System.Drawing.Point(1116, 76)
        Me.numStorageMinBak.Name = "numStorageMinBak"
        Me.numStorageMinBak.ReadOnly = False
        Me.numStorageMinBak.Size = New System.Drawing.Size(100, 18)
        Me.numStorageMinBak.TabIndex = 494
        Me.numStorageMinBak.TabStop = False
        Me.numStorageMinBak.TabStopSetting = False
        Me.numStorageMinBak.Tag = ""
        Me.numStorageMinBak.TextValue = "0"
        Me.numStorageMinBak.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStorageMinBak.Visible = False
        Me.numStorageMinBak.WidthDef = 100
        '
        'numHandlingNg
        '
        Me.numHandlingNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHandlingNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHandlingNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingNg.HeightDef = 18
        Me.numHandlingNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHandlingNg.HissuLabelVisible = False
        Me.numHandlingNg.IsHissuCheck = False
        Me.numHandlingNg.IsRangeCheck = False
        Me.numHandlingNg.ItemName = ""
        Me.numHandlingNg.Location = New System.Drawing.Point(297, 52)
        Me.numHandlingNg.Name = "numHandlingNg"
        Me.numHandlingNg.ReadOnly = False
        Me.numHandlingNg.Size = New System.Drawing.Size(100, 18)
        Me.numHandlingNg.TabIndex = 511
        Me.numHandlingNg.TabStopSetting = True
        Me.numHandlingNg.Tag = ""
        Me.numHandlingNg.TextValue = "0"
        Me.numHandlingNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHandlingNg.WidthDef = 100
        '
        'numHandlingNr
        '
        Me.numHandlingNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHandlingNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHandlingNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingNr.HeightDef = 18
        Me.numHandlingNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHandlingNr.HissuLabelVisible = False
        Me.numHandlingNr.IsHissuCheck = False
        Me.numHandlingNr.IsRangeCheck = False
        Me.numHandlingNr.ItemName = ""
        Me.numHandlingNr.Location = New System.Drawing.Point(297, 31)
        Me.numHandlingNr.Name = "numHandlingNr"
        Me.numHandlingNr.ReadOnly = False
        Me.numHandlingNr.Size = New System.Drawing.Size(80, 18)
        Me.numHandlingNr.TabIndex = 507
        Me.numHandlingNr.TabStopSetting = True
        Me.numHandlingNr.Tag = ""
        Me.numHandlingNr.TextValue = "0"
        Me.numHandlingNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHandlingNr.WidthDef = 80
        '
        'LmTitleLabel44
        '
        Me.LmTitleLabel44.AutoSize = True
        Me.LmTitleLabel44.AutoSizeDef = True
        Me.LmTitleLabel44.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel44.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel44.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel44.EnableStatus = False
        Me.LmTitleLabel44.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel44.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel44.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel44.HeightDef = 13
        Me.LmTitleLabel44.Location = New System.Drawing.Point(293, 14)
        Me.LmTitleLabel44.Name = "LmTitleLabel44"
        Me.LmTitleLabel44.Size = New System.Drawing.Size(52, 13)
        Me.LmTitleLabel44.TabIndex = 496
        Me.LmTitleLabel44.Text = "荷役料"
        Me.LmTitleLabel44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel44.TextValue = "荷役料"
        Me.LmTitleLabel44.WidthDef = 52
        '
        'LmTitleLabel48
        '
        Me.LmTitleLabel48.AutoSize = True
        Me.LmTitleLabel48.AutoSizeDef = True
        Me.LmTitleLabel48.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel48.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel48.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel48.EnableStatus = False
        Me.LmTitleLabel48.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel48.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel48.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel48.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel48.HeightDef = 13
        Me.LmTitleLabel48.Location = New System.Drawing.Point(431, 14)
        Me.LmTitleLabel48.Name = "LmTitleLabel48"
        Me.LmTitleLabel48.Size = New System.Drawing.Size(37, 13)
        Me.LmTitleLabel48.TabIndex = 514
        Me.LmTitleLabel48.Text = "運賃"
        Me.LmTitleLabel48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel48.TextValue = "運賃"
        Me.LmTitleLabel48.WidthDef = 37
        '
        'LmTitleLabel52
        '
        Me.LmTitleLabel52.AutoSize = True
        Me.LmTitleLabel52.AutoSizeDef = True
        Me.LmTitleLabel52.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel52.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel52.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel52.EnableStatus = False
        Me.LmTitleLabel52.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel52.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel52.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel52.HeightDef = 13
        Me.LmTitleLabel52.Location = New System.Drawing.Point(569, 14)
        Me.LmTitleLabel52.Name = "LmTitleLabel52"
        Me.LmTitleLabel52.Size = New System.Drawing.Size(52, 13)
        Me.LmTitleLabel52.TabIndex = 520
        Me.LmTitleLabel52.Text = "作業料"
        Me.LmTitleLabel52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel52.TextValue = "作業料"
        Me.LmTitleLabel52.WidthDef = 52
        '
        'LmTitleLabel53
        '
        Me.LmTitleLabel53.AutoSize = True
        Me.LmTitleLabel53.AutoSizeDef = True
        Me.LmTitleLabel53.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel53.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel53.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel53.EnableStatus = False
        Me.LmTitleLabel53.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel53.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel53.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel53.HeightDef = 13
        Me.LmTitleLabel53.Location = New System.Drawing.Point(705, 14)
        Me.LmTitleLabel53.Name = "LmTitleLabel53"
        Me.LmTitleLabel53.Size = New System.Drawing.Size(52, 13)
        Me.LmTitleLabel53.TabIndex = 526
        Me.LmTitleLabel53.Text = "通関料"
        Me.LmTitleLabel53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel53.TextValue = "通関料"
        Me.LmTitleLabel53.WidthDef = 52
        '
        'numClearanceNg
        '
        Me.numClearanceNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numClearanceNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numClearanceNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numClearanceNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numClearanceNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numClearanceNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numClearanceNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numClearanceNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numClearanceNg.HeightDef = 18
        Me.numClearanceNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numClearanceNg.HissuLabelVisible = False
        Me.numClearanceNg.IsHissuCheck = False
        Me.numClearanceNg.IsRangeCheck = False
        Me.numClearanceNg.ItemName = ""
        Me.numClearanceNg.Location = New System.Drawing.Point(708, 52)
        Me.numClearanceNg.Name = "numClearanceNg"
        Me.numClearanceNg.ReadOnly = False
        Me.numClearanceNg.Size = New System.Drawing.Size(100, 18)
        Me.numClearanceNg.TabIndex = 539
        Me.numClearanceNg.TabStopSetting = True
        Me.numClearanceNg.Tag = ""
        Me.numClearanceNg.TextValue = "0"
        Me.numClearanceNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numClearanceNg.WidthDef = 100
        '
        'numClearanceNr
        '
        Me.numClearanceNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numClearanceNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numClearanceNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numClearanceNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numClearanceNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numClearanceNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numClearanceNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numClearanceNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numClearanceNr.HeightDef = 18
        Me.numClearanceNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numClearanceNr.HissuLabelVisible = False
        Me.numClearanceNr.IsHissuCheck = False
        Me.numClearanceNr.IsRangeCheck = False
        Me.numClearanceNr.ItemName = ""
        Me.numClearanceNr.Location = New System.Drawing.Point(708, 31)
        Me.numClearanceNr.Name = "numClearanceNr"
        Me.numClearanceNr.ReadOnly = False
        Me.numClearanceNr.Size = New System.Drawing.Size(80, 18)
        Me.numClearanceNr.TabIndex = 537
        Me.numClearanceNr.TabStopSetting = True
        Me.numClearanceNr.Tag = ""
        Me.numClearanceNr.TextValue = "0"
        Me.numClearanceNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numClearanceNr.WidthDef = 80
        '
        'numYokomochiNg
        '
        Me.numYokomochiNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numYokomochiNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numYokomochiNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numYokomochiNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numYokomochiNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numYokomochiNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numYokomochiNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numYokomochiNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numYokomochiNg.HeightDef = 18
        Me.numYokomochiNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numYokomochiNg.HissuLabelVisible = False
        Me.numYokomochiNg.IsHissuCheck = False
        Me.numYokomochiNg.IsRangeCheck = False
        Me.numYokomochiNg.ItemName = ""
        Me.numYokomochiNg.Location = New System.Drawing.Point(844, 52)
        Me.numYokomochiNg.Name = "numYokomochiNg"
        Me.numYokomochiNg.ReadOnly = False
        Me.numYokomochiNg.Size = New System.Drawing.Size(100, 18)
        Me.numYokomochiNg.TabIndex = 553
        Me.numYokomochiNg.TabStopSetting = True
        Me.numYokomochiNg.Tag = ""
        Me.numYokomochiNg.TextValue = "0"
        Me.numYokomochiNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numYokomochiNg.WidthDef = 100
        '
        'numYokomochiNr
        '
        Me.numYokomochiNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numYokomochiNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numYokomochiNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numYokomochiNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numYokomochiNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numYokomochiNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numYokomochiNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numYokomochiNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numYokomochiNr.HeightDef = 18
        Me.numYokomochiNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numYokomochiNr.HissuLabelVisible = False
        Me.numYokomochiNr.IsHissuCheck = False
        Me.numYokomochiNr.IsRangeCheck = False
        Me.numYokomochiNr.ItemName = ""
        Me.numYokomochiNr.Location = New System.Drawing.Point(844, 31)
        Me.numYokomochiNr.Name = "numYokomochiNr"
        Me.numYokomochiNr.ReadOnly = False
        Me.numYokomochiNr.Size = New System.Drawing.Size(80, 18)
        Me.numYokomochiNr.TabIndex = 551
        Me.numYokomochiNr.TabStopSetting = True
        Me.numYokomochiNr.Tag = ""
        Me.numYokomochiNr.TextValue = "0"
        Me.numYokomochiNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numYokomochiNr.WidthDef = 80
        '
        'LmTitleLabel65
        '
        Me.LmTitleLabel65.AutoSize = True
        Me.LmTitleLabel65.AutoSizeDef = True
        Me.LmTitleLabel65.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel65.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel65.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel65.EnableStatus = False
        Me.LmTitleLabel65.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel65.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel65.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel65.HeightDef = 13
        Me.LmTitleLabel65.Location = New System.Drawing.Point(841, 14)
        Me.LmTitleLabel65.Name = "LmTitleLabel65"
        Me.LmTitleLabel65.Size = New System.Drawing.Size(52, 13)
        Me.LmTitleLabel65.TabIndex = 540
        Me.LmTitleLabel65.Text = "横持料"
        Me.LmTitleLabel65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel65.TextValue = "横持料"
        Me.LmTitleLabel65.WidthDef = 52
        '
        'numUnchinNg
        '
        Me.numUnchinNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnchinNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnchinNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinNg.HeightDef = 18
        Me.numUnchinNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchinNg.HissuLabelVisible = False
        Me.numUnchinNg.IsHissuCheck = False
        Me.numUnchinNg.IsRangeCheck = False
        Me.numUnchinNg.ItemName = ""
        Me.numUnchinNg.Location = New System.Drawing.Point(434, 52)
        Me.numUnchinNg.Name = "numUnchinNg"
        Me.numUnchinNg.ReadOnly = False
        Me.numUnchinNg.Size = New System.Drawing.Size(100, 18)
        Me.numUnchinNg.TabIndex = 561
        Me.numUnchinNg.TabStopSetting = True
        Me.numUnchinNg.Tag = ""
        Me.numUnchinNg.TextValue = "0"
        Me.numUnchinNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnchinNg.WidthDef = 100
        '
        'numUnchinNr
        '
        Me.numUnchinNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnchinNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnchinNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinNr.HeightDef = 18
        Me.numUnchinNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchinNr.HissuLabelVisible = False
        Me.numUnchinNr.IsHissuCheck = False
        Me.numUnchinNr.IsRangeCheck = False
        Me.numUnchinNr.ItemName = ""
        Me.numUnchinNr.Location = New System.Drawing.Point(434, 31)
        Me.numUnchinNr.Name = "numUnchinNr"
        Me.numUnchinNr.ReadOnly = False
        Me.numUnchinNr.Size = New System.Drawing.Size(80, 18)
        Me.numUnchinNr.TabIndex = 559
        Me.numUnchinNr.TabStopSetting = True
        Me.numUnchinNr.Tag = ""
        Me.numUnchinNr.TextValue = "0"
        Me.numUnchinNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnchinNr.WidthDef = 80
        '
        'numSagyoNg
        '
        Me.numSagyoNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNg.HeightDef = 18
        Me.numSagyoNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoNg.HissuLabelVisible = False
        Me.numSagyoNg.IsHissuCheck = False
        Me.numSagyoNg.IsRangeCheck = False
        Me.numSagyoNg.ItemName = ""
        Me.numSagyoNg.Location = New System.Drawing.Point(572, 52)
        Me.numSagyoNg.Name = "numSagyoNg"
        Me.numSagyoNg.ReadOnly = False
        Me.numSagyoNg.Size = New System.Drawing.Size(100, 18)
        Me.numSagyoNg.TabIndex = 569
        Me.numSagyoNg.TabStopSetting = True
        Me.numSagyoNg.Tag = ""
        Me.numSagyoNg.TextValue = "0"
        Me.numSagyoNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoNg.WidthDef = 100
        '
        'numSagyoNr
        '
        Me.numSagyoNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNr.HeightDef = 18
        Me.numSagyoNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoNr.HissuLabelVisible = False
        Me.numSagyoNr.IsHissuCheck = False
        Me.numSagyoNr.IsRangeCheck = False
        Me.numSagyoNr.ItemName = ""
        Me.numSagyoNr.Location = New System.Drawing.Point(572, 31)
        Me.numSagyoNr.Name = "numSagyoNr"
        Me.numSagyoNr.ReadOnly = False
        Me.numSagyoNr.Size = New System.Drawing.Size(80, 18)
        Me.numSagyoNr.TabIndex = 567
        Me.numSagyoNr.TabStopSetting = True
        Me.numSagyoNr.Tag = ""
        Me.numSagyoNr.TextValue = "0"
        Me.numSagyoNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoNr.WidthDef = 80
        '
        'numTotalNg
        '
        Me.numTotalNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalNg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numTotalNg.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numTotalNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalNg.HeightDef = 18
        Me.numTotalNg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTotalNg.HissuLabelVisible = False
        Me.numTotalNg.IsHissuCheck = False
        Me.numTotalNg.IsRangeCheck = False
        Me.numTotalNg.ItemName = ""
        Me.numTotalNg.Location = New System.Drawing.Point(1120, 52)
        Me.numTotalNg.Name = "numTotalNg"
        Me.numTotalNg.ReadOnly = False
        Me.numTotalNg.Size = New System.Drawing.Size(100, 18)
        Me.numTotalNg.TabIndex = 571
        Me.numTotalNg.TabStopSetting = True
        Me.numTotalNg.Tag = ""
        Me.numTotalNg.TextValue = "0"
        Me.numTotalNg.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numTotalNg.WidthDef = 100
        '
        'numTotalNr
        '
        Me.numTotalNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalNr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numTotalNr.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numTotalNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalNr.HeightDef = 18
        Me.numTotalNr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTotalNr.HissuLabelVisible = False
        Me.numTotalNr.IsHissuCheck = False
        Me.numTotalNr.IsRangeCheck = False
        Me.numTotalNr.ItemName = ""
        Me.numTotalNr.Location = New System.Drawing.Point(1120, 31)
        Me.numTotalNr.Name = "numTotalNr"
        Me.numTotalNr.ReadOnly = False
        Me.numTotalNr.Size = New System.Drawing.Size(80, 18)
        Me.numTotalNr.TabIndex = 570
        Me.numTotalNr.TabStopSetting = True
        Me.numTotalNr.Tag = ""
        Me.numTotalNr.TextValue = "0"
        Me.numTotalNr.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numTotalNr.WidthDef = 80
        '
        'TitlelblTotalNg
        '
        Me.TitlelblTotalNg.AutoSize = True
        Me.TitlelblTotalNg.AutoSizeDef = True
        Me.TitlelblTotalNg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTotalNg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTotalNg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblTotalNg.EnableStatus = False
        Me.TitlelblTotalNg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTotalNg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTotalNg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTotalNg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTotalNg.HeightDef = 13
        Me.TitlelblTotalNg.Location = New System.Drawing.Point(976, 54)
        Me.TitlelblTotalNg.Name = "TitlelblTotalNg"
        Me.TitlelblTotalNg.Size = New System.Drawing.Size(142, 13)
        Me.TitlelblTotalNg.TabIndex = 573
        Me.TitlelblTotalNg.Text = "全体値引額（課税）"
        Me.TitlelblTotalNg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblTotalNg.TextValue = "全体値引額（課税）"
        Me.TitlelblTotalNg.WidthDef = 142
        '
        'TitlelblTotalNr
        '
        Me.TitlelblTotalNr.AutoSize = True
        Me.TitlelblTotalNr.AutoSizeDef = True
        Me.TitlelblTotalNr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTotalNr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblTotalNr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblTotalNr.EnableStatus = False
        Me.TitlelblTotalNr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTotalNr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblTotalNr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTotalNr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblTotalNr.HeightDef = 13
        Me.TitlelblTotalNr.Location = New System.Drawing.Point(976, 34)
        Me.TitlelblTotalNr.Name = "TitlelblTotalNr"
        Me.TitlelblTotalNr.Size = New System.Drawing.Size(142, 13)
        Me.TitlelblTotalNr.TabIndex = 572
        Me.TitlelblTotalNr.Text = "全体値引率（課税）"
        Me.TitlelblTotalNr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblTotalNr.TextValue = "全体値引率（課税）"
        Me.TitlelblTotalNr.WidthDef = 142
        '
        'TitlelblEigyo
        '
        Me.TitlelblEigyo.AutoSize = True
        Me.TitlelblEigyo.AutoSizeDef = True
        Me.TitlelblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblEigyo.EnableStatus = False
        Me.TitlelblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblEigyo.HeightDef = 13
        Me.TitlelblEigyo.Location = New System.Drawing.Point(105, 379)
        Me.TitlelblEigyo.Name = "TitlelblEigyo"
        Me.TitlelblEigyo.Size = New System.Drawing.Size(49, 13)
        Me.TitlelblEigyo.TabIndex = 577
        Me.TitlelblEigyo.Text = "営業所"
        Me.TitlelblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblEigyo.TextValue = "営業所"
        Me.TitlelblEigyo.WidthDef = 49
        '
        'grpSeiq
        '
        Me.grpSeiq.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSeiq.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSeiq.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSeiq.Controls.Add(Me.cmbHandlingZeroFlgKBN)
        Me.grpSeiq.Controls.Add(Me.cmbStorageZeroFlgKBN)
        Me.grpSeiq.Controls.Add(Me.lblZeroMin)
        Me.grpSeiq.Controls.Add(Me.lblSagyoMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblUnchinMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblHandlingMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblStorageMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.numStorageMin)
        Me.grpSeiq.Controls.Add(Me.numHandlingMin)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel19)
        Me.grpSeiq.Controls.Add(Me.numSagyoMin)
        Me.grpSeiq.Controls.Add(Me.numUnchinMin)
        Me.grpSeiq.Controls.Add(Me.lblHandlingOtherMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblStorageOtherMinCurrCd)
        Me.grpSeiq.Controls.Add(Me.numStorageOtherMin)
        Me.grpSeiq.Controls.Add(Me.numHandlingOtherMin)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel12)
        Me.grpSeiq.Controls.Add(Me.chkSagyoTotalFlg)
        Me.grpSeiq.Controls.Add(Me.chkUnchinTotalFlg)
        Me.grpSeiq.Controls.Add(Me.chkHandlingTotalFlg)
        Me.grpSeiq.Controls.Add(Me.lblStorageMinCurrCdBak)
        Me.grpSeiq.Controls.Add(Me.lblTotalMinSeiqCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblTotalNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblYokomochiNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblClearanceNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblSagyoNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblUnchinNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblHandlingNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.lblStorageNgCurrCd)
        Me.grpSeiq.Controls.Add(Me.numStorageMinBak)
        Me.grpSeiq.Controls.Add(Me.numStorageNg)
        Me.grpSeiq.Controls.Add(Me.numHandlingNg)
        Me.grpSeiq.Controls.Add(Me.numStorageNr)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel2)
        Me.grpSeiq.Controls.Add(Me.lblTotalMinSeiqAmt)
        Me.grpSeiq.Controls.Add(Me.TitlelblTotalNg)
        Me.grpSeiq.Controls.Add(Me.TitlelblNr)
        Me.grpSeiq.Controls.Add(Me.TitlelblTotalNr)
        Me.grpSeiq.Controls.Add(Me.numTotalMinSeiqAmt)
        Me.grpSeiq.Controls.Add(Me.numTotalNg)
        Me.grpSeiq.Controls.Add(Me.TitlelblNg)
        Me.grpSeiq.Controls.Add(Me.numTotalNr)
        Me.grpSeiq.Controls.Add(Me.lblTotalMinChk)
        Me.grpSeiq.Controls.Add(Me.TitlelblMin)
        Me.grpSeiq.Controls.Add(Me.numSagyoNg)
        Me.grpSeiq.Controls.Add(Me.numSagyoNr)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel44)
        Me.grpSeiq.Controls.Add(Me.numYokomochiNg)
        Me.grpSeiq.Controls.Add(Me.numYokomochiNr)
        Me.grpSeiq.Controls.Add(Me.numHandlingNr)
        Me.grpSeiq.Controls.Add(Me.chkStorageTotalFlg)
        Me.grpSeiq.Controls.Add(Me.numUnchinNg)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel65)
        Me.grpSeiq.Controls.Add(Me.numUnchinNr)
        Me.grpSeiq.Controls.Add(Me.numClearanceNg)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel48)
        Me.grpSeiq.Controls.Add(Me.numClearanceNr)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel52)
        Me.grpSeiq.Controls.Add(Me.LmTitleLabel53)
        Me.grpSeiq.EnableStatus = False
        Me.grpSeiq.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSeiq.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSeiq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSeiq.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSeiq.HeightDef = 162
        Me.grpSeiq.Location = New System.Drawing.Point(20, 665)
        Me.grpSeiq.Name = "grpSeiq"
        Me.grpSeiq.Size = New System.Drawing.Size(1242, 162)
        Me.grpSeiq.TabIndex = 579
        Me.grpSeiq.TabStop = False
        Me.grpSeiq.Text = "請求用"
        Me.grpSeiq.TextValue = "請求用"
        Me.grpSeiq.WidthDef = 1242
        '
        'cmbHandlingZeroFlgKBN
        '
        Me.cmbHandlingZeroFlgKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHandlingZeroFlgKBN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHandlingZeroFlgKBN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbHandlingZeroFlgKBN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbHandlingZeroFlgKBN.DataCode = "T033"
        Me.cmbHandlingZeroFlgKBN.DataSource = Nothing
        Me.cmbHandlingZeroFlgKBN.DisplayMember = Nothing
        Me.cmbHandlingZeroFlgKBN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHandlingZeroFlgKBN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHandlingZeroFlgKBN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHandlingZeroFlgKBN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHandlingZeroFlgKBN.HeightDef = 18
        Me.cmbHandlingZeroFlgKBN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHandlingZeroFlgKBN.HissuLabelVisible = True
        Me.cmbHandlingZeroFlgKBN.InsertWildCard = True
        Me.cmbHandlingZeroFlgKBN.IsForbiddenWordsCheck = False
        Me.cmbHandlingZeroFlgKBN.IsHissuCheck = True
        Me.cmbHandlingZeroFlgKBN.ItemName = ""
        Me.cmbHandlingZeroFlgKBN.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbHandlingZeroFlgKBN.Location = New System.Drawing.Point(297, 116)
        Me.cmbHandlingZeroFlgKBN.Name = "cmbHandlingZeroFlgKBN"
        Me.cmbHandlingZeroFlgKBN.ReadOnly = False
        Me.cmbHandlingZeroFlgKBN.SelectedIndex = -1
        Me.cmbHandlingZeroFlgKBN.SelectedItem = Nothing
        Me.cmbHandlingZeroFlgKBN.SelectedText = ""
        Me.cmbHandlingZeroFlgKBN.SelectedValue = ""
        Me.cmbHandlingZeroFlgKBN.Size = New System.Drawing.Size(115, 18)
        Me.cmbHandlingZeroFlgKBN.TabIndex = 622
        Me.cmbHandlingZeroFlgKBN.TabStopSetting = True
        Me.cmbHandlingZeroFlgKBN.TextValue = ""
        Me.cmbHandlingZeroFlgKBN.Value1 = Nothing
        Me.cmbHandlingZeroFlgKBN.Value2 = Nothing
        Me.cmbHandlingZeroFlgKBN.Value3 = Nothing
        Me.cmbHandlingZeroFlgKBN.ValueMember = Nothing
        Me.cmbHandlingZeroFlgKBN.WidthDef = 115
        '
        'cmbStorageZeroFlgKBN
        '
        Me.cmbStorageZeroFlgKBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStorageZeroFlgKBN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbStorageZeroFlgKBN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbStorageZeroFlgKBN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbStorageZeroFlgKBN.DataCode = "T033"
        Me.cmbStorageZeroFlgKBN.DataSource = Nothing
        Me.cmbStorageZeroFlgKBN.DisplayMember = Nothing
        Me.cmbStorageZeroFlgKBN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStorageZeroFlgKBN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStorageZeroFlgKBN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStorageZeroFlgKBN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStorageZeroFlgKBN.HeightDef = 18
        Me.cmbStorageZeroFlgKBN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbStorageZeroFlgKBN.HissuLabelVisible = True
        Me.cmbStorageZeroFlgKBN.InsertWildCard = True
        Me.cmbStorageZeroFlgKBN.IsForbiddenWordsCheck = False
        Me.cmbStorageZeroFlgKBN.IsHissuCheck = True
        Me.cmbStorageZeroFlgKBN.ItemName = ""
        Me.cmbStorageZeroFlgKBN.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbStorageZeroFlgKBN.Location = New System.Drawing.Point(161, 116)
        Me.cmbStorageZeroFlgKBN.Name = "cmbStorageZeroFlgKBN"
        Me.cmbStorageZeroFlgKBN.ReadOnly = False
        Me.cmbStorageZeroFlgKBN.SelectedIndex = -1
        Me.cmbStorageZeroFlgKBN.SelectedItem = Nothing
        Me.cmbStorageZeroFlgKBN.SelectedText = ""
        Me.cmbStorageZeroFlgKBN.SelectedValue = ""
        Me.cmbStorageZeroFlgKBN.Size = New System.Drawing.Size(115, 18)
        Me.cmbStorageZeroFlgKBN.TabIndex = 621
        Me.cmbStorageZeroFlgKBN.TabStopSetting = True
        Me.cmbStorageZeroFlgKBN.TextValue = ""
        Me.cmbStorageZeroFlgKBN.Value1 = Nothing
        Me.cmbStorageZeroFlgKBN.Value2 = Nothing
        Me.cmbStorageZeroFlgKBN.Value3 = Nothing
        Me.cmbStorageZeroFlgKBN.ValueMember = Nothing
        Me.cmbStorageZeroFlgKBN.WidthDef = 115
        '
        'lblZeroMin
        '
        Me.lblZeroMin.AutoSize = True
        Me.lblZeroMin.AutoSizeDef = True
        Me.lblZeroMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeroMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeroMin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblZeroMin.EnableStatus = False
        Me.lblZeroMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeroMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeroMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeroMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeroMin.HeightDef = 13
        Me.lblZeroMin.Location = New System.Drawing.Point(5, 117)
        Me.lblZeroMin.Name = "lblZeroMin"
        Me.lblZeroMin.Size = New System.Drawing.Size(154, 13)
        Me.lblZeroMin.TabIndex = 619
        Me.lblZeroMin.Text = "計算額0円時の最低保証"
        Me.lblZeroMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblZeroMin.TextValue = "計算額0円時の最低保証"
        Me.lblZeroMin.WidthDef = 154
        '
        'lblSagyoMinCurrCd
        '
        Me.lblSagyoMinCurrCd.AutoSize = True
        Me.lblSagyoMinCurrCd.AutoSizeDef = True
        Me.lblSagyoMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoMinCurrCd.EnableStatus = False
        Me.lblSagyoMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoMinCurrCd.HeightDef = 13
        Me.lblSagyoMinCurrCd.Location = New System.Drawing.Point(660, 75)
        Me.lblSagyoMinCurrCd.Name = "lblSagyoMinCurrCd"
        Me.lblSagyoMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblSagyoMinCurrCd.TabIndex = 616
        Me.lblSagyoMinCurrCd.Text = "XXX"
        Me.lblSagyoMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSagyoMinCurrCd.TextValue = "XXX"
        Me.lblSagyoMinCurrCd.WidthDef = 28
        '
        'lblUnchinMinCurrCd
        '
        Me.lblUnchinMinCurrCd.AutoSize = True
        Me.lblUnchinMinCurrCd.AutoSizeDef = True
        Me.lblUnchinMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnchinMinCurrCd.EnableStatus = False
        Me.lblUnchinMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinMinCurrCd.HeightDef = 13
        Me.lblUnchinMinCurrCd.Location = New System.Drawing.Point(522, 75)
        Me.lblUnchinMinCurrCd.Name = "lblUnchinMinCurrCd"
        Me.lblUnchinMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblUnchinMinCurrCd.TabIndex = 615
        Me.lblUnchinMinCurrCd.Text = "XXX"
        Me.lblUnchinMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinMinCurrCd.TextValue = "XXX"
        Me.lblUnchinMinCurrCd.WidthDef = 28
        '
        'lblHandlingMinCurrCd
        '
        Me.lblHandlingMinCurrCd.AutoSize = True
        Me.lblHandlingMinCurrCd.AutoSizeDef = True
        Me.lblHandlingMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHandlingMinCurrCd.EnableStatus = False
        Me.lblHandlingMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingMinCurrCd.HeightDef = 13
        Me.lblHandlingMinCurrCd.Location = New System.Drawing.Point(384, 75)
        Me.lblHandlingMinCurrCd.Name = "lblHandlingMinCurrCd"
        Me.lblHandlingMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblHandlingMinCurrCd.TabIndex = 614
        Me.lblHandlingMinCurrCd.Text = "XXX"
        Me.lblHandlingMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHandlingMinCurrCd.TextValue = "XXX"
        Me.lblHandlingMinCurrCd.WidthDef = 28
        '
        'lblStorageMinCurrCd
        '
        Me.lblStorageMinCurrCd.AutoSize = True
        Me.lblStorageMinCurrCd.AutoSizeDef = True
        Me.lblStorageMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStorageMinCurrCd.EnableStatus = False
        Me.lblStorageMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageMinCurrCd.HeightDef = 13
        Me.lblStorageMinCurrCd.Location = New System.Drawing.Point(248, 75)
        Me.lblStorageMinCurrCd.Name = "lblStorageMinCurrCd"
        Me.lblStorageMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblStorageMinCurrCd.TabIndex = 613
        Me.lblStorageMinCurrCd.Text = "XXX"
        Me.lblStorageMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStorageMinCurrCd.TextValue = "XXX"
        Me.lblStorageMinCurrCd.WidthDef = 28
        '
        'numStorageMin
        '
        Me.numStorageMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStorageMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStorageMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageMin.HeightDef = 18
        Me.numStorageMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStorageMin.HissuLabelVisible = False
        Me.numStorageMin.IsHissuCheck = False
        Me.numStorageMin.IsRangeCheck = False
        Me.numStorageMin.ItemName = ""
        Me.numStorageMin.Location = New System.Drawing.Point(161, 73)
        Me.numStorageMin.Name = "numStorageMin"
        Me.numStorageMin.ReadOnly = False
        Me.numStorageMin.Size = New System.Drawing.Size(100, 18)
        Me.numStorageMin.TabIndex = 607
        Me.numStorageMin.TabStopSetting = True
        Me.numStorageMin.Tag = ""
        Me.numStorageMin.TextValue = "0"
        Me.numStorageMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStorageMin.WidthDef = 100
        '
        'numHandlingMin
        '
        Me.numHandlingMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHandlingMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHandlingMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingMin.HeightDef = 18
        Me.numHandlingMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHandlingMin.HissuLabelVisible = False
        Me.numHandlingMin.IsHissuCheck = False
        Me.numHandlingMin.IsRangeCheck = False
        Me.numHandlingMin.ItemName = ""
        Me.numHandlingMin.Location = New System.Drawing.Point(297, 73)
        Me.numHandlingMin.Name = "numHandlingMin"
        Me.numHandlingMin.ReadOnly = False
        Me.numHandlingMin.Size = New System.Drawing.Size(100, 18)
        Me.numHandlingMin.TabIndex = 608
        Me.numHandlingMin.TabStopSetting = True
        Me.numHandlingMin.Tag = ""
        Me.numHandlingMin.TextValue = "0"
        Me.numHandlingMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHandlingMin.WidthDef = 100
        '
        'LmTitleLabel19
        '
        Me.LmTitleLabel19.AutoSizeDef = False
        Me.LmTitleLabel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel19.EnableStatus = False
        Me.LmTitleLabel19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.HeightDef = 13
        Me.LmTitleLabel19.Location = New System.Drawing.Point(6, 75)
        Me.LmTitleLabel19.Name = "LmTitleLabel19"
        Me.LmTitleLabel19.Size = New System.Drawing.Size(153, 13)
        Me.LmTitleLabel19.TabIndex = 606
        Me.LmTitleLabel19.Text = "自社最低保証額(単独)"
        Me.LmTitleLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel19.TextValue = "自社最低保証額(単独)"
        Me.LmTitleLabel19.WidthDef = 153
        '
        'numSagyoMin
        '
        Me.numSagyoMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoMin.HeightDef = 18
        Me.numSagyoMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoMin.HissuLabelVisible = False
        Me.numSagyoMin.IsHissuCheck = False
        Me.numSagyoMin.IsRangeCheck = False
        Me.numSagyoMin.ItemName = ""
        Me.numSagyoMin.Location = New System.Drawing.Point(572, 73)
        Me.numSagyoMin.Name = "numSagyoMin"
        Me.numSagyoMin.ReadOnly = False
        Me.numSagyoMin.Size = New System.Drawing.Size(100, 18)
        Me.numSagyoMin.TabIndex = 612
        Me.numSagyoMin.TabStopSetting = True
        Me.numSagyoMin.Tag = ""
        Me.numSagyoMin.TextValue = "0"
        Me.numSagyoMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoMin.WidthDef = 100
        '
        'numUnchinMin
        '
        Me.numUnchinMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnchinMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnchinMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnchinMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchinMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchinMin.HeightDef = 18
        Me.numUnchinMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchinMin.HissuLabelVisible = False
        Me.numUnchinMin.IsHissuCheck = False
        Me.numUnchinMin.IsRangeCheck = False
        Me.numUnchinMin.ItemName = ""
        Me.numUnchinMin.Location = New System.Drawing.Point(434, 73)
        Me.numUnchinMin.Name = "numUnchinMin"
        Me.numUnchinMin.ReadOnly = False
        Me.numUnchinMin.Size = New System.Drawing.Size(100, 18)
        Me.numUnchinMin.TabIndex = 611
        Me.numUnchinMin.TabStopSetting = True
        Me.numUnchinMin.Tag = ""
        Me.numUnchinMin.TextValue = "0"
        Me.numUnchinMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnchinMin.WidthDef = 100
        '
        'lblHandlingOtherMinCurrCd
        '
        Me.lblHandlingOtherMinCurrCd.AutoSize = True
        Me.lblHandlingOtherMinCurrCd.AutoSizeDef = True
        Me.lblHandlingOtherMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingOtherMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingOtherMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHandlingOtherMinCurrCd.EnableStatus = False
        Me.lblHandlingOtherMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingOtherMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingOtherMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingOtherMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingOtherMinCurrCd.HeightDef = 13
        Me.lblHandlingOtherMinCurrCd.Location = New System.Drawing.Point(384, 96)
        Me.lblHandlingOtherMinCurrCd.Name = "lblHandlingOtherMinCurrCd"
        Me.lblHandlingOtherMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblHandlingOtherMinCurrCd.TabIndex = 601
        Me.lblHandlingOtherMinCurrCd.Text = "XXX"
        Me.lblHandlingOtherMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHandlingOtherMinCurrCd.TextValue = "XXX"
        Me.lblHandlingOtherMinCurrCd.WidthDef = 28
        '
        'lblStorageOtherMinCurrCd
        '
        Me.lblStorageOtherMinCurrCd.AutoSize = True
        Me.lblStorageOtherMinCurrCd.AutoSizeDef = True
        Me.lblStorageOtherMinCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageOtherMinCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageOtherMinCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStorageOtherMinCurrCd.EnableStatus = False
        Me.lblStorageOtherMinCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageOtherMinCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageOtherMinCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageOtherMinCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageOtherMinCurrCd.HeightDef = 13
        Me.lblStorageOtherMinCurrCd.Location = New System.Drawing.Point(248, 96)
        Me.lblStorageOtherMinCurrCd.Name = "lblStorageOtherMinCurrCd"
        Me.lblStorageOtherMinCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblStorageOtherMinCurrCd.TabIndex = 600
        Me.lblStorageOtherMinCurrCd.Text = "XXX"
        Me.lblStorageOtherMinCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStorageOtherMinCurrCd.TextValue = "XXX"
        Me.lblStorageOtherMinCurrCd.WidthDef = 28
        '
        'numStorageOtherMin
        '
        Me.numStorageOtherMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageOtherMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numStorageOtherMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStorageOtherMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStorageOtherMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageOtherMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStorageOtherMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageOtherMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStorageOtherMin.HeightDef = 18
        Me.numStorageOtherMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStorageOtherMin.HissuLabelVisible = False
        Me.numStorageOtherMin.IsHissuCheck = False
        Me.numStorageOtherMin.IsRangeCheck = False
        Me.numStorageOtherMin.ItemName = ""
        Me.numStorageOtherMin.Location = New System.Drawing.Point(161, 94)
        Me.numStorageOtherMin.Name = "numStorageOtherMin"
        Me.numStorageOtherMin.ReadOnly = False
        Me.numStorageOtherMin.Size = New System.Drawing.Size(100, 18)
        Me.numStorageOtherMin.TabIndex = 594
        Me.numStorageOtherMin.TabStopSetting = True
        Me.numStorageOtherMin.Tag = ""
        Me.numStorageOtherMin.TextValue = "0"
        Me.numStorageOtherMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStorageOtherMin.WidthDef = 100
        '
        'numHandlingOtherMin
        '
        Me.numHandlingOtherMin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingOtherMin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHandlingOtherMin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHandlingOtherMin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHandlingOtherMin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingOtherMin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHandlingOtherMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingOtherMin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHandlingOtherMin.HeightDef = 18
        Me.numHandlingOtherMin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHandlingOtherMin.HissuLabelVisible = False
        Me.numHandlingOtherMin.IsHissuCheck = False
        Me.numHandlingOtherMin.IsRangeCheck = False
        Me.numHandlingOtherMin.ItemName = ""
        Me.numHandlingOtherMin.Location = New System.Drawing.Point(297, 94)
        Me.numHandlingOtherMin.Name = "numHandlingOtherMin"
        Me.numHandlingOtherMin.ReadOnly = False
        Me.numHandlingOtherMin.Size = New System.Drawing.Size(100, 18)
        Me.numHandlingOtherMin.TabIndex = 595
        Me.numHandlingOtherMin.TabStopSetting = True
        Me.numHandlingOtherMin.Tag = ""
        Me.numHandlingOtherMin.TextValue = "0"
        Me.numHandlingOtherMin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHandlingOtherMin.WidthDef = 100
        '
        'LmTitleLabel12
        '
        Me.LmTitleLabel12.AutoSizeDef = False
        Me.LmTitleLabel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel12.EnableStatus = False
        Me.LmTitleLabel12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.HeightDef = 13
        Me.LmTitleLabel12.Location = New System.Drawing.Point(6, 96)
        Me.LmTitleLabel12.Name = "LmTitleLabel12"
        Me.LmTitleLabel12.Size = New System.Drawing.Size(153, 13)
        Me.LmTitleLabel12.TabIndex = 593
        Me.LmTitleLabel12.Text = "再保最低保証額(単独)"
        Me.LmTitleLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel12.TextValue = "再保最低保証額(単独)"
        Me.LmTitleLabel12.WidthDef = 153
        '
        'chkSagyoTotalFlg
        '
        Me.chkSagyoTotalFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSagyoTotalFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSagyoTotalFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSagyoTotalFlg.EnableStatus = True
        Me.chkSagyoTotalFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSagyoTotalFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSagyoTotalFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSagyoTotalFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSagyoTotalFlg.HeightDef = 18
        Me.chkSagyoTotalFlg.Location = New System.Drawing.Point(572, 137)
        Me.chkSagyoTotalFlg.Name = "chkSagyoTotalFlg"
        Me.chkSagyoTotalFlg.Size = New System.Drawing.Size(54, 18)
        Me.chkSagyoTotalFlg.TabIndex = 592
        Me.chkSagyoTotalFlg.TabStopSetting = True
        Me.chkSagyoTotalFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSagyoTotalFlg.TextValue = ""
        Me.chkSagyoTotalFlg.UseVisualStyleBackColor = True
        Me.chkSagyoTotalFlg.WidthDef = 54
        '
        'chkUnchinTotalFlg
        '
        Me.chkUnchinTotalFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkUnchinTotalFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkUnchinTotalFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkUnchinTotalFlg.EnableStatus = True
        Me.chkUnchinTotalFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkUnchinTotalFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkUnchinTotalFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkUnchinTotalFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkUnchinTotalFlg.HeightDef = 18
        Me.chkUnchinTotalFlg.Location = New System.Drawing.Point(434, 137)
        Me.chkUnchinTotalFlg.Name = "chkUnchinTotalFlg"
        Me.chkUnchinTotalFlg.Size = New System.Drawing.Size(54, 18)
        Me.chkUnchinTotalFlg.TabIndex = 591
        Me.chkUnchinTotalFlg.TabStopSetting = True
        Me.chkUnchinTotalFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkUnchinTotalFlg.TextValue = ""
        Me.chkUnchinTotalFlg.UseVisualStyleBackColor = True
        Me.chkUnchinTotalFlg.WidthDef = 54
        '
        'chkHandlingTotalFlg
        '
        Me.chkHandlingTotalFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHandlingTotalFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHandlingTotalFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkHandlingTotalFlg.EnableStatus = True
        Me.chkHandlingTotalFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHandlingTotalFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHandlingTotalFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHandlingTotalFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHandlingTotalFlg.HeightDef = 18
        Me.chkHandlingTotalFlg.Location = New System.Drawing.Point(297, 137)
        Me.chkHandlingTotalFlg.Name = "chkHandlingTotalFlg"
        Me.chkHandlingTotalFlg.Size = New System.Drawing.Size(54, 18)
        Me.chkHandlingTotalFlg.TabIndex = 590
        Me.chkHandlingTotalFlg.TabStopSetting = True
        Me.chkHandlingTotalFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkHandlingTotalFlg.TextValue = ""
        Me.chkHandlingTotalFlg.UseVisualStyleBackColor = True
        Me.chkHandlingTotalFlg.WidthDef = 54
        '
        'lblStorageMinCurrCdBak
        '
        Me.lblStorageMinCurrCdBak.AutoSize = True
        Me.lblStorageMinCurrCdBak.AutoSizeDef = True
        Me.lblStorageMinCurrCdBak.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageMinCurrCdBak.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageMinCurrCdBak.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStorageMinCurrCdBak.EnableStatus = False
        Me.lblStorageMinCurrCdBak.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageMinCurrCdBak.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageMinCurrCdBak.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageMinCurrCdBak.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageMinCurrCdBak.HeightDef = 13
        Me.lblStorageMinCurrCdBak.Location = New System.Drawing.Point(1203, 79)
        Me.lblStorageMinCurrCdBak.Name = "lblStorageMinCurrCdBak"
        Me.lblStorageMinCurrCdBak.Size = New System.Drawing.Size(28, 13)
        Me.lblStorageMinCurrCdBak.TabIndex = 581
        Me.lblStorageMinCurrCdBak.Text = "XXX"
        Me.lblStorageMinCurrCdBak.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStorageMinCurrCdBak.TextValue = "XXX"
        Me.lblStorageMinCurrCdBak.Visible = False
        Me.lblStorageMinCurrCdBak.WidthDef = 28
        '
        'lblTotalMinSeiqCurrCd
        '
        Me.lblTotalMinSeiqCurrCd.AutoSize = True
        Me.lblTotalMinSeiqCurrCd.AutoSizeDef = True
        Me.lblTotalMinSeiqCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinSeiqCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinSeiqCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTotalMinSeiqCurrCd.EnableStatus = False
        Me.lblTotalMinSeiqCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinSeiqCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinSeiqCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinSeiqCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinSeiqCurrCd.HeightDef = 13
        Me.lblTotalMinSeiqCurrCd.Location = New System.Drawing.Point(1207, 117)
        Me.lblTotalMinSeiqCurrCd.Name = "lblTotalMinSeiqCurrCd"
        Me.lblTotalMinSeiqCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblTotalMinSeiqCurrCd.TabIndex = 580
        Me.lblTotalMinSeiqCurrCd.Text = "XXX"
        Me.lblTotalMinSeiqCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalMinSeiqCurrCd.TextValue = "XXX"
        Me.lblTotalMinSeiqCurrCd.WidthDef = 28
        '
        'lblTotalNgCurrCd
        '
        Me.lblTotalNgCurrCd.AutoSize = True
        Me.lblTotalNgCurrCd.AutoSizeDef = True
        Me.lblTotalNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTotalNgCurrCd.EnableStatus = False
        Me.lblTotalNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalNgCurrCd.HeightDef = 13
        Me.lblTotalNgCurrCd.Location = New System.Drawing.Point(1207, 54)
        Me.lblTotalNgCurrCd.Name = "lblTotalNgCurrCd"
        Me.lblTotalNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblTotalNgCurrCd.TabIndex = 580
        Me.lblTotalNgCurrCd.Text = "XXX"
        Me.lblTotalNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalNgCurrCd.TextValue = "XXX"
        Me.lblTotalNgCurrCd.WidthDef = 28
        '
        'lblYokomochiNgCurrCd
        '
        Me.lblYokomochiNgCurrCd.AutoSize = True
        Me.lblYokomochiNgCurrCd.AutoSizeDef = True
        Me.lblYokomochiNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokomochiNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokomochiNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblYokomochiNgCurrCd.EnableStatus = False
        Me.lblYokomochiNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokomochiNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokomochiNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokomochiNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokomochiNgCurrCd.HeightDef = 13
        Me.lblYokomochiNgCurrCd.Location = New System.Drawing.Point(931, 54)
        Me.lblYokomochiNgCurrCd.Name = "lblYokomochiNgCurrCd"
        Me.lblYokomochiNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblYokomochiNgCurrCd.TabIndex = 579
        Me.lblYokomochiNgCurrCd.Text = "XXX"
        Me.lblYokomochiNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblYokomochiNgCurrCd.TextValue = "XXX"
        Me.lblYokomochiNgCurrCd.WidthDef = 28
        '
        'lblClearanceNgCurrCd
        '
        Me.lblClearanceNgCurrCd.AutoSize = True
        Me.lblClearanceNgCurrCd.AutoSizeDef = True
        Me.lblClearanceNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblClearanceNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblClearanceNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblClearanceNgCurrCd.EnableStatus = False
        Me.lblClearanceNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblClearanceNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblClearanceNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblClearanceNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblClearanceNgCurrCd.HeightDef = 13
        Me.lblClearanceNgCurrCd.Location = New System.Drawing.Point(795, 54)
        Me.lblClearanceNgCurrCd.Name = "lblClearanceNgCurrCd"
        Me.lblClearanceNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblClearanceNgCurrCd.TabIndex = 578
        Me.lblClearanceNgCurrCd.Text = "XXX"
        Me.lblClearanceNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblClearanceNgCurrCd.TextValue = "XXX"
        Me.lblClearanceNgCurrCd.WidthDef = 28
        '
        'lblSagyoNgCurrCd
        '
        Me.lblSagyoNgCurrCd.AutoSize = True
        Me.lblSagyoNgCurrCd.AutoSizeDef = True
        Me.lblSagyoNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoNgCurrCd.EnableStatus = False
        Me.lblSagyoNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoNgCurrCd.HeightDef = 13
        Me.lblSagyoNgCurrCd.Location = New System.Drawing.Point(660, 54)
        Me.lblSagyoNgCurrCd.Name = "lblSagyoNgCurrCd"
        Me.lblSagyoNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblSagyoNgCurrCd.TabIndex = 577
        Me.lblSagyoNgCurrCd.Text = "XXX"
        Me.lblSagyoNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSagyoNgCurrCd.TextValue = "XXX"
        Me.lblSagyoNgCurrCd.WidthDef = 28
        '
        'lblUnchinNgCurrCd
        '
        Me.lblUnchinNgCurrCd.AutoSize = True
        Me.lblUnchinNgCurrCd.AutoSizeDef = True
        Me.lblUnchinNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnchinNgCurrCd.EnableStatus = False
        Me.lblUnchinNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinNgCurrCd.HeightDef = 13
        Me.lblUnchinNgCurrCd.Location = New System.Drawing.Point(522, 54)
        Me.lblUnchinNgCurrCd.Name = "lblUnchinNgCurrCd"
        Me.lblUnchinNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblUnchinNgCurrCd.TabIndex = 576
        Me.lblUnchinNgCurrCd.Text = "XXX"
        Me.lblUnchinNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinNgCurrCd.TextValue = "XXX"
        Me.lblUnchinNgCurrCd.WidthDef = 28
        '
        'lblHandlingNgCurrCd
        '
        Me.lblHandlingNgCurrCd.AutoSize = True
        Me.lblHandlingNgCurrCd.AutoSizeDef = True
        Me.lblHandlingNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHandlingNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHandlingNgCurrCd.EnableStatus = False
        Me.lblHandlingNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHandlingNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHandlingNgCurrCd.HeightDef = 13
        Me.lblHandlingNgCurrCd.Location = New System.Drawing.Point(384, 54)
        Me.lblHandlingNgCurrCd.Name = "lblHandlingNgCurrCd"
        Me.lblHandlingNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblHandlingNgCurrCd.TabIndex = 575
        Me.lblHandlingNgCurrCd.Text = "XXX"
        Me.lblHandlingNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHandlingNgCurrCd.TextValue = "XXX"
        Me.lblHandlingNgCurrCd.WidthDef = 28
        '
        'lblStorageNgCurrCd
        '
        Me.lblStorageNgCurrCd.AutoSize = True
        Me.lblStorageNgCurrCd.AutoSizeDef = True
        Me.lblStorageNgCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageNgCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStorageNgCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStorageNgCurrCd.EnableStatus = False
        Me.lblStorageNgCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageNgCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStorageNgCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageNgCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStorageNgCurrCd.HeightDef = 13
        Me.lblStorageNgCurrCd.Location = New System.Drawing.Point(248, 54)
        Me.lblStorageNgCurrCd.Name = "lblStorageNgCurrCd"
        Me.lblStorageNgCurrCd.Size = New System.Drawing.Size(28, 13)
        Me.lblStorageNgCurrCd.TabIndex = 574
        Me.lblStorageNgCurrCd.Text = "XXX"
        Me.lblStorageNgCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStorageNgCurrCd.TextValue = "XXX"
        Me.lblStorageNgCurrCd.WidthDef = 28
        '
        'lblTotalMinSeiqAmt
        '
        Me.lblTotalMinSeiqAmt.AutoSizeDef = False
        Me.lblTotalMinSeiqAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinSeiqAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinSeiqAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTotalMinSeiqAmt.EnableStatus = False
        Me.lblTotalMinSeiqAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinSeiqAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinSeiqAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinSeiqAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinSeiqAmt.HeightDef = 13
        Me.lblTotalMinSeiqAmt.Location = New System.Drawing.Point(911, 117)
        Me.lblTotalMinSeiqAmt.Name = "lblTotalMinSeiqAmt"
        Me.lblTotalMinSeiqAmt.Size = New System.Drawing.Size(203, 13)
        Me.lblTotalMinSeiqAmt.TabIndex = 573
        Me.lblTotalMinSeiqAmt.Text = "鑑最低保証金額（課税）"
        Me.lblTotalMinSeiqAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalMinSeiqAmt.TextValue = "鑑最低保証金額（課税）"
        Me.lblTotalMinSeiqAmt.WidthDef = 203
        '
        'numTotalMinSeiqAmt
        '
        Me.numTotalMinSeiqAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalMinSeiqAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numTotalMinSeiqAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numTotalMinSeiqAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numTotalMinSeiqAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalMinSeiqAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTotalMinSeiqAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalMinSeiqAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTotalMinSeiqAmt.HeightDef = 18
        Me.numTotalMinSeiqAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTotalMinSeiqAmt.HissuLabelVisible = False
        Me.numTotalMinSeiqAmt.IsHissuCheck = False
        Me.numTotalMinSeiqAmt.IsRangeCheck = False
        Me.numTotalMinSeiqAmt.ItemName = ""
        Me.numTotalMinSeiqAmt.Location = New System.Drawing.Point(1120, 115)
        Me.numTotalMinSeiqAmt.Name = "numTotalMinSeiqAmt"
        Me.numTotalMinSeiqAmt.ReadOnly = False
        Me.numTotalMinSeiqAmt.Size = New System.Drawing.Size(100, 18)
        Me.numTotalMinSeiqAmt.TabIndex = 571
        Me.numTotalMinSeiqAmt.TabStopSetting = True
        Me.numTotalMinSeiqAmt.Tag = ""
        Me.numTotalMinSeiqAmt.TextValue = "0"
        Me.numTotalMinSeiqAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numTotalMinSeiqAmt.WidthDef = 100
        '
        'lblTotalMinChk
        '
        Me.lblTotalMinChk.AutoSizeDef = False
        Me.lblTotalMinChk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinChk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTotalMinChk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTotalMinChk.EnableStatus = False
        Me.lblTotalMinChk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinChk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTotalMinChk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinChk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalMinChk.HeightDef = 13
        Me.lblTotalMinChk.Location = New System.Drawing.Point(36, 139)
        Me.lblTotalMinChk.Name = "lblTotalMinChk"
        Me.lblTotalMinChk.Size = New System.Drawing.Size(123, 13)
        Me.lblTotalMinChk.TabIndex = 489
        Me.lblTotalMinChk.Text = "鑑最低保証対象"
        Me.lblTotalMinChk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalMinChk.TextValue = "鑑最低保証対象"
        Me.lblTotalMinChk.WidthDef = 123
        '
        'chkStorageTotalFlg
        '
        Me.chkStorageTotalFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStorageTotalFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStorageTotalFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStorageTotalFlg.EnableStatus = True
        Me.chkStorageTotalFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStorageTotalFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStorageTotalFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStorageTotalFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStorageTotalFlg.HeightDef = 18
        Me.chkStorageTotalFlg.Location = New System.Drawing.Point(161, 137)
        Me.chkStorageTotalFlg.Name = "chkStorageTotalFlg"
        Me.chkStorageTotalFlg.Size = New System.Drawing.Size(54, 18)
        Me.chkStorageTotalFlg.TabIndex = 589
        Me.chkStorageTotalFlg.TabStopSetting = True
        Me.chkStorageTotalFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStorageTotalFlg.TextValue = ""
        Me.chkStorageTotalFlg.UseVisualStyleBackColor = True
        Me.chkStorageTotalFlg.WidthDef = 54
        '
        'txtCustKagamiType1
        '
        Me.txtCustKagamiType1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType1.CountWrappedLine = False
        Me.txtCustKagamiType1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType1.HeightDef = 18
        Me.txtCustKagamiType1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType1.HissuLabelVisible = False
        Me.txtCustKagamiType1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType1.IsByteCheck = 20
        Me.txtCustKagamiType1.IsCalendarCheck = False
        Me.txtCustKagamiType1.IsDakutenCheck = False
        Me.txtCustKagamiType1.IsEisuCheck = False
        Me.txtCustKagamiType1.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType1.IsFullByteCheck = 0
        Me.txtCustKagamiType1.IsHankakuCheck = False
        Me.txtCustKagamiType1.IsHissuCheck = False
        Me.txtCustKagamiType1.IsKanaCheck = False
        Me.txtCustKagamiType1.IsMiddleSpace = False
        Me.txtCustKagamiType1.IsNumericCheck = False
        Me.txtCustKagamiType1.IsSujiCheck = False
        Me.txtCustKagamiType1.IsZenkakuCheck = False
        Me.txtCustKagamiType1.ItemName = ""
        Me.txtCustKagamiType1.LineSpace = 0
        Me.txtCustKagamiType1.Location = New System.Drawing.Point(156, 598)
        Me.txtCustKagamiType1.MaxLength = 20
        Me.txtCustKagamiType1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType1.MaxLineCount = 0
        Me.txtCustKagamiType1.Multiline = False
        Me.txtCustKagamiType1.Name = "txtCustKagamiType1"
        Me.txtCustKagamiType1.ReadOnly = False
        Me.txtCustKagamiType1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType1.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType1.TabIndex = 581
        Me.txtCustKagamiType1.TabStopSetting = True
        Me.txtCustKagamiType1.TextValue = ""
        Me.txtCustKagamiType1.UseSystemPasswordChar = False
        Me.txtCustKagamiType1.WidthDef = 173
        Me.txtCustKagamiType1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami1
        '
        Me.TitlelblCustKagami1.AutoSizeDef = False
        Me.TitlelblCustKagami1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami1.EnableStatus = False
        Me.TitlelblCustKagami1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami1.HeightDef = 15
        Me.TitlelblCustKagami1.Location = New System.Drawing.Point(15, 598)
        Me.TitlelblCustKagami1.Name = "TitlelblCustKagami1"
        Me.TitlelblCustKagami1.Size = New System.Drawing.Size(139, 15)
        Me.TitlelblCustKagami1.TabIndex = 580
        Me.TitlelblCustKagami1.Text = "荷主鑑分類種別1"
        Me.TitlelblCustKagami1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami1.TextValue = "荷主鑑分類種別1"
        Me.TitlelblCustKagami1.WidthDef = 139
        '
        'txtCustKagamiType2
        '
        Me.txtCustKagamiType2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType2.CountWrappedLine = False
        Me.txtCustKagamiType2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType2.HeightDef = 18
        Me.txtCustKagamiType2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType2.HissuLabelVisible = False
        Me.txtCustKagamiType2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType2.IsByteCheck = 20
        Me.txtCustKagamiType2.IsCalendarCheck = False
        Me.txtCustKagamiType2.IsDakutenCheck = False
        Me.txtCustKagamiType2.IsEisuCheck = False
        Me.txtCustKagamiType2.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType2.IsFullByteCheck = 0
        Me.txtCustKagamiType2.IsHankakuCheck = False
        Me.txtCustKagamiType2.IsHissuCheck = False
        Me.txtCustKagamiType2.IsKanaCheck = False
        Me.txtCustKagamiType2.IsMiddleSpace = False
        Me.txtCustKagamiType2.IsNumericCheck = False
        Me.txtCustKagamiType2.IsSujiCheck = False
        Me.txtCustKagamiType2.IsZenkakuCheck = False
        Me.txtCustKagamiType2.ItemName = ""
        Me.txtCustKagamiType2.LineSpace = 0
        Me.txtCustKagamiType2.Location = New System.Drawing.Point(156, 619)
        Me.txtCustKagamiType2.MaxLength = 20
        Me.txtCustKagamiType2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType2.MaxLineCount = 0
        Me.txtCustKagamiType2.Multiline = False
        Me.txtCustKagamiType2.Name = "txtCustKagamiType2"
        Me.txtCustKagamiType2.ReadOnly = False
        Me.txtCustKagamiType2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType2.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType2.TabIndex = 583
        Me.txtCustKagamiType2.TabStopSetting = True
        Me.txtCustKagamiType2.TextValue = ""
        Me.txtCustKagamiType2.UseSystemPasswordChar = False
        Me.txtCustKagamiType2.WidthDef = 173
        Me.txtCustKagamiType2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami2
        '
        Me.TitlelblCustKagami2.AutoSizeDef = False
        Me.TitlelblCustKagami2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami2.EnableStatus = False
        Me.TitlelblCustKagami2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami2.HeightDef = 13
        Me.TitlelblCustKagami2.Location = New System.Drawing.Point(12, 621)
        Me.TitlelblCustKagami2.Name = "TitlelblCustKagami2"
        Me.TitlelblCustKagami2.Size = New System.Drawing.Size(142, 13)
        Me.TitlelblCustKagami2.TabIndex = 582
        Me.TitlelblCustKagami2.Text = "荷主鑑分類種別2"
        Me.TitlelblCustKagami2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami2.TextValue = "荷主鑑分類種別2"
        Me.TitlelblCustKagami2.WidthDef = 142
        '
        'txtCustKagamiType3
        '
        Me.txtCustKagamiType3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType3.CountWrappedLine = False
        Me.txtCustKagamiType3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType3.HeightDef = 18
        Me.txtCustKagamiType3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType3.HissuLabelVisible = False
        Me.txtCustKagamiType3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType3.IsByteCheck = 20
        Me.txtCustKagamiType3.IsCalendarCheck = False
        Me.txtCustKagamiType3.IsDakutenCheck = False
        Me.txtCustKagamiType3.IsEisuCheck = False
        Me.txtCustKagamiType3.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType3.IsFullByteCheck = 0
        Me.txtCustKagamiType3.IsHankakuCheck = False
        Me.txtCustKagamiType3.IsHissuCheck = False
        Me.txtCustKagamiType3.IsKanaCheck = False
        Me.txtCustKagamiType3.IsMiddleSpace = False
        Me.txtCustKagamiType3.IsNumericCheck = False
        Me.txtCustKagamiType3.IsSujiCheck = False
        Me.txtCustKagamiType3.IsZenkakuCheck = False
        Me.txtCustKagamiType3.ItemName = ""
        Me.txtCustKagamiType3.LineSpace = 0
        Me.txtCustKagamiType3.Location = New System.Drawing.Point(156, 640)
        Me.txtCustKagamiType3.MaxLength = 20
        Me.txtCustKagamiType3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType3.MaxLineCount = 0
        Me.txtCustKagamiType3.Multiline = False
        Me.txtCustKagamiType3.Name = "txtCustKagamiType3"
        Me.txtCustKagamiType3.ReadOnly = False
        Me.txtCustKagamiType3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType3.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType3.TabIndex = 585
        Me.txtCustKagamiType3.TabStopSetting = True
        Me.txtCustKagamiType3.TextValue = ""
        Me.txtCustKagamiType3.UseSystemPasswordChar = False
        Me.txtCustKagamiType3.WidthDef = 173
        Me.txtCustKagamiType3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami3
        '
        Me.TitlelblCustKagami3.AutoSizeDef = False
        Me.TitlelblCustKagami3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami3.EnableStatus = False
        Me.TitlelblCustKagami3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami3.HeightDef = 13
        Me.TitlelblCustKagami3.Location = New System.Drawing.Point(12, 642)
        Me.TitlelblCustKagami3.Name = "TitlelblCustKagami3"
        Me.TitlelblCustKagami3.Size = New System.Drawing.Size(142, 13)
        Me.TitlelblCustKagami3.TabIndex = 584
        Me.TitlelblCustKagami3.Text = "荷主鑑分類種別3"
        Me.TitlelblCustKagami3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami3.TextValue = "荷主鑑分類種別3"
        Me.TitlelblCustKagami3.WidthDef = 142
        '
        'txtSeiqtoBusyoNm
        '
        Me.txtSeiqtoBusyoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoBusyoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoBusyoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoBusyoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoBusyoNm.CountWrappedLine = False
        Me.txtSeiqtoBusyoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoBusyoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoBusyoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoBusyoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoBusyoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoBusyoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoBusyoNm.HeightDef = 18
        Me.txtSeiqtoBusyoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoBusyoNm.HissuLabelVisible = False
        Me.txtSeiqtoBusyoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSeiqtoBusyoNm.IsByteCheck = 60
        Me.txtSeiqtoBusyoNm.IsCalendarCheck = False
        Me.txtSeiqtoBusyoNm.IsDakutenCheck = False
        Me.txtSeiqtoBusyoNm.IsEisuCheck = False
        Me.txtSeiqtoBusyoNm.IsForbiddenWordsCheck = False
        Me.txtSeiqtoBusyoNm.IsFullByteCheck = 0
        Me.txtSeiqtoBusyoNm.IsHankakuCheck = False
        Me.txtSeiqtoBusyoNm.IsHissuCheck = False
        Me.txtSeiqtoBusyoNm.IsKanaCheck = False
        Me.txtSeiqtoBusyoNm.IsMiddleSpace = False
        Me.txtSeiqtoBusyoNm.IsNumericCheck = False
        Me.txtSeiqtoBusyoNm.IsSujiCheck = False
        Me.txtSeiqtoBusyoNm.IsZenkakuCheck = False
        Me.txtSeiqtoBusyoNm.ItemName = ""
        Me.txtSeiqtoBusyoNm.LineSpace = 0
        Me.txtSeiqtoBusyoNm.Location = New System.Drawing.Point(156, 440)
        Me.txtSeiqtoBusyoNm.MaxLength = 60
        Me.txtSeiqtoBusyoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoBusyoNm.MaxLineCount = 0
        Me.txtSeiqtoBusyoNm.Multiline = False
        Me.txtSeiqtoBusyoNm.Name = "txtSeiqtoBusyoNm"
        Me.txtSeiqtoBusyoNm.ReadOnly = False
        Me.txtSeiqtoBusyoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoBusyoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoBusyoNm.Size = New System.Drawing.Size(532, 18)
        Me.txtSeiqtoBusyoNm.TabIndex = 586
        Me.txtSeiqtoBusyoNm.TabStopSetting = True
        Me.txtSeiqtoBusyoNm.TextValue = ""
        Me.txtSeiqtoBusyoNm.UseSystemPasswordChar = False
        Me.txtSeiqtoBusyoNm.WidthDef = 532
        Me.txtSeiqtoBusyoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblSeiqBushoNm
        '
        Me.TitlelblSeiqBushoNm.AutoSizeDef = False
        Me.TitlelblSeiqBushoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqBushoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSeiqBushoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblSeiqBushoNm.EnableStatus = False
        Me.TitlelblSeiqBushoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqBushoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSeiqBushoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqBushoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSeiqBushoNm.HeightDef = 14
        Me.TitlelblSeiqBushoNm.Location = New System.Drawing.Point(29, 440)
        Me.TitlelblSeiqBushoNm.Name = "TitlelblSeiqBushoNm"
        Me.TitlelblSeiqBushoNm.Size = New System.Drawing.Size(125, 14)
        Me.TitlelblSeiqBushoNm.TabIndex = 587
        Me.TitlelblSeiqBushoNm.Text = "請求先部署名"
        Me.TitlelblSeiqBushoNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblSeiqBushoNm.TextValue = "請求先部署名"
        Me.TitlelblSeiqBushoNm.WidthDef = 125
        '
        'TitlelblDocKind
        '
        Me.TitlelblDocKind.AutoSize = True
        Me.TitlelblDocKind.AutoSizeDef = True
        Me.TitlelblDocKind.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocKind.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocKind.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblDocKind.EnableStatus = False
        Me.TitlelblDocKind.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocKind.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocKind.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocKind.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocKind.HeightDef = 13
        Me.TitlelblDocKind.Location = New System.Drawing.Point(451, 837)
        Me.TitlelblDocKind.Name = "TitlelblDocKind"
        Me.TitlelblDocKind.Size = New System.Drawing.Size(77, 13)
        Me.TitlelblDocKind.TabIndex = 588
        Me.TitlelblDocKind.Text = "請求書種別"
        Me.TitlelblDocKind.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblDocKind.TextValue = "請求書種別"
        Me.TitlelblDocKind.WidthDef = 77
        '
        'chkSei
        '
        Me.chkSei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSei.EnableStatus = True
        Me.chkSei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSei.HeightDef = 18
        Me.chkSei.Location = New System.Drawing.Point(537, 835)
        Me.chkSei.Name = "chkSei"
        Me.chkSei.Size = New System.Drawing.Size(54, 18)
        Me.chkSei.TabIndex = 589
        Me.chkSei.TabStopSetting = True
        Me.chkSei.Text = "正"
        Me.chkSei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSei.TextValue = "正"
        Me.chkSei.UseVisualStyleBackColor = True
        Me.chkSei.WidthDef = 54
        '
        'chkKeiri
        '
        Me.chkKeiri.AutoSize = True
        Me.chkKeiri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeiri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeiri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkKeiri.EnableStatus = True
        Me.chkKeiri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeiri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeiri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeiri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeiri.HeightDef = 17
        Me.chkKeiri.Location = New System.Drawing.Point(719, 835)
        Me.chkKeiri.Name = "chkKeiri"
        Me.chkKeiri.Size = New System.Drawing.Size(82, 17)
        Me.chkKeiri.TabIndex = 590
        Me.chkKeiri.TabStopSetting = True
        Me.chkKeiri.Text = "控(経理)"
        Me.chkKeiri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkKeiri.TextValue = "控(経理)"
        Me.chkKeiri.UseVisualStyleBackColor = True
        Me.chkKeiri.WidthDef = 82
        '
        'chkHikae
        '
        Me.chkHikae.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHikae.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHikae.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkHikae.EnableStatus = True
        Me.chkHikae.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHikae.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHikae.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHikae.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHikae.HeightDef = 16
        Me.chkHikae.Location = New System.Drawing.Point(653, 835)
        Me.chkHikae.Name = "chkHikae"
        Me.chkHikae.Size = New System.Drawing.Size(60, 16)
        Me.chkHikae.TabIndex = 591
        Me.chkHikae.TabStopSetting = True
        Me.chkHikae.Text = "控"
        Me.chkHikae.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkHikae.TextValue = "控"
        Me.chkHikae.UseVisualStyleBackColor = True
        Me.chkHikae.WidthDef = 60
        '
        'chkFuku
        '
        Me.chkFuku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkFuku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkFuku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkFuku.EnableStatus = True
        Me.chkFuku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFuku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFuku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFuku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFuku.HeightDef = 17
        Me.chkFuku.Location = New System.Drawing.Point(590, 835)
        Me.chkFuku.Name = "chkFuku"
        Me.chkFuku.Size = New System.Drawing.Size(57, 17)
        Me.chkFuku.TabIndex = 592
        Me.chkFuku.TabStopSetting = True
        Me.chkFuku.Text = "副"
        Me.chkFuku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFuku.TextValue = "副"
        Me.chkFuku.UseVisualStyleBackColor = True
        Me.chkFuku.WidthDef = 57
        '
        'lblUpdTime
        '
        Me.lblUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdTime.CountWrappedLine = False
        Me.lblUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdTime.HeightDef = 18
        Me.lblUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.HissuLabelVisible = False
        Me.lblUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdTime.IsByteCheck = 0
        Me.lblUpdTime.IsCalendarCheck = False
        Me.lblUpdTime.IsDakutenCheck = False
        Me.lblUpdTime.IsEisuCheck = False
        Me.lblUpdTime.IsForbiddenWordsCheck = False
        Me.lblUpdTime.IsFullByteCheck = 0
        Me.lblUpdTime.IsHankakuCheck = False
        Me.lblUpdTime.IsHissuCheck = False
        Me.lblUpdTime.IsKanaCheck = False
        Me.lblUpdTime.IsMiddleSpace = False
        Me.lblUpdTime.IsNumericCheck = False
        Me.lblUpdTime.IsSujiCheck = False
        Me.lblUpdTime.IsZenkakuCheck = False
        Me.lblUpdTime.ItemName = ""
        Me.lblUpdTime.LineSpace = 0
        Me.lblUpdTime.Location = New System.Drawing.Point(1120, 497)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 593
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbKouzaKbn
        '
        Me.cmbKouzaKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKouzaKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKouzaKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKouzaKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKouzaKbn.DataSource = Nothing
        Me.cmbKouzaKbn.DisplayMember = Nothing
        Me.cmbKouzaKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKouzaKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKouzaKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKouzaKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKouzaKbn.HeightDef = 18
        Me.cmbKouzaKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKouzaKbn.HissuLabelVisible = True
        Me.cmbKouzaKbn.InsertWildCard = False
        Me.cmbKouzaKbn.IsForbiddenWordsCheck = False
        Me.cmbKouzaKbn.IsHissuCheck = True
        Me.cmbKouzaKbn.ItemName = ""
        Me.cmbKouzaKbn.Location = New System.Drawing.Point(156, 461)
        Me.cmbKouzaKbn.Name = "cmbKouzaKbn"
        Me.cmbKouzaKbn.ReadOnly = False
        Me.cmbKouzaKbn.SelectedIndex = -1
        Me.cmbKouzaKbn.SelectedItem = Nothing
        Me.cmbKouzaKbn.SelectedText = ""
        Me.cmbKouzaKbn.SelectedValue = ""
        Me.cmbKouzaKbn.Size = New System.Drawing.Size(533, 18)
        Me.cmbKouzaKbn.TabIndex = 5
        Me.cmbKouzaKbn.TabStopSetting = True
        Me.cmbKouzaKbn.TextValue = ""
        Me.cmbKouzaKbn.ValueMember = Nothing
        Me.cmbKouzaKbn.WidthDef = 533
        '
        'lblSysDelFlg
        '
        Me.lblSysDelFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysDelFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysDelFlg.CountWrappedLine = False
        Me.lblSysDelFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysDelFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysDelFlg.HeightDef = 18
        Me.lblSysDelFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.HissuLabelVisible = False
        Me.lblSysDelFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysDelFlg.IsByteCheck = 0
        Me.lblSysDelFlg.IsCalendarCheck = False
        Me.lblSysDelFlg.IsDakutenCheck = False
        Me.lblSysDelFlg.IsEisuCheck = False
        Me.lblSysDelFlg.IsForbiddenWordsCheck = False
        Me.lblSysDelFlg.IsFullByteCheck = 0
        Me.lblSysDelFlg.IsHankakuCheck = False
        Me.lblSysDelFlg.IsHissuCheck = False
        Me.lblSysDelFlg.IsKanaCheck = False
        Me.lblSysDelFlg.IsMiddleSpace = False
        Me.lblSysDelFlg.IsNumericCheck = False
        Me.lblSysDelFlg.IsSujiCheck = False
        Me.lblSysDelFlg.IsZenkakuCheck = False
        Me.lblSysDelFlg.ItemName = ""
        Me.lblSysDelFlg.LineSpace = 0
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1120, 518)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 595
        Me.lblSysDelFlg.TabStop = False
        Me.lblSysDelFlg.TabStopSetting = False
        Me.lblSysDelFlg.TextValue = ""
        Me.lblSysDelFlg.UseSystemPasswordChar = False
        Me.lblSysDelFlg.Visible = False
        Me.lblSysDelFlg.WidthDef = 157
        Me.lblSysDelFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKagamiMeigi
        '
        Me.lblKagamiMeigi.AutoSizeDef = False
        Me.lblKagamiMeigi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKagamiMeigi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKagamiMeigi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKagamiMeigi.EnableStatus = False
        Me.lblKagamiMeigi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKagamiMeigi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKagamiMeigi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKagamiMeigi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKagamiMeigi.HeightDef = 13
        Me.lblKagamiMeigi.Location = New System.Drawing.Point(77, 483)
        Me.lblKagamiMeigi.Name = "lblKagamiMeigi"
        Me.lblKagamiMeigi.Size = New System.Drawing.Size(77, 13)
        Me.lblKagamiMeigi.TabIndex = 598
        Me.lblKagamiMeigi.Text = "鑑名義区分"
        Me.lblKagamiMeigi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKagamiMeigi.TextValue = "鑑名義区分"
        Me.lblKagamiMeigi.WidthDef = 77
        '
        'cmbMeigiKbn
        '
        Me.cmbMeigiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMeigiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMeigiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbMeigiKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbMeigiKbn.DataCode = "K011"
        Me.cmbMeigiKbn.DataSource = Nothing
        Me.cmbMeigiKbn.DisplayMember = Nothing
        Me.cmbMeigiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMeigiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMeigiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMeigiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMeigiKbn.HeightDef = 18
        Me.cmbMeigiKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbMeigiKbn.HissuLabelVisible = True
        Me.cmbMeigiKbn.InsertWildCard = True
        Me.cmbMeigiKbn.IsForbiddenWordsCheck = False
        Me.cmbMeigiKbn.IsHissuCheck = True
        Me.cmbMeigiKbn.ItemName = ""
        Me.cmbMeigiKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbMeigiKbn.Location = New System.Drawing.Point(156, 482)
        Me.cmbMeigiKbn.Name = "cmbMeigiKbn"
        Me.cmbMeigiKbn.ReadOnly = False
        Me.cmbMeigiKbn.SelectedIndex = -1
        Me.cmbMeigiKbn.SelectedItem = Nothing
        Me.cmbMeigiKbn.SelectedText = ""
        Me.cmbMeigiKbn.SelectedValue = ""
        Me.cmbMeigiKbn.Size = New System.Drawing.Size(173, 18)
        Me.cmbMeigiKbn.TabIndex = 599
        Me.cmbMeigiKbn.TabStopSetting = True
        Me.cmbMeigiKbn.TextValue = ""
        Me.cmbMeigiKbn.Value1 = Nothing
        Me.cmbMeigiKbn.Value2 = Nothing
        Me.cmbMeigiKbn.Value3 = Nothing
        Me.cmbMeigiKbn.ValueMember = Nothing
        Me.cmbMeigiKbn.WidthDef = 173
        '
        'cmbNrsBrCd
        '
        Me.cmbNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsBrCd.DataSource = Nothing
        Me.cmbNrsBrCd.DisplayMember = Nothing
        Me.cmbNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.HeightDef = 18
        Me.cmbNrsBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.HissuLabelVisible = True
        Me.cmbNrsBrCd.InsertWildCard = True
        Me.cmbNrsBrCd.IsForbiddenWordsCheck = False
        Me.cmbNrsBrCd.IsHissuCheck = True
        Me.cmbNrsBrCd.ItemName = ""
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(156, 377)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 601
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'cmbDocPtn
        '
        Me.cmbDocPtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDocPtn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDocPtn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbDocPtn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbDocPtn.DataSource = Nothing
        Me.cmbDocPtn.DisplayMember = Nothing
        Me.cmbDocPtn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDocPtn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDocPtn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDocPtn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDocPtn.HeightDef = 18
        Me.cmbDocPtn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbDocPtn.HissuLabelVisible = True
        Me.cmbDocPtn.InsertWildCard = False
        Me.cmbDocPtn.IsForbiddenWordsCheck = False
        Me.cmbDocPtn.IsHissuCheck = True
        Me.cmbDocPtn.ItemName = ""
        Me.cmbDocPtn.Location = New System.Drawing.Point(200, 833)
        Me.cmbDocPtn.Name = "cmbDocPtn"
        Me.cmbDocPtn.ReadOnly = False
        Me.cmbDocPtn.SelectedIndex = -1
        Me.cmbDocPtn.SelectedItem = Nothing
        Me.cmbDocPtn.SelectedText = ""
        Me.cmbDocPtn.SelectedValue = ""
        Me.cmbDocPtn.Size = New System.Drawing.Size(245, 18)
        Me.cmbDocPtn.TabIndex = 602
        Me.cmbDocPtn.TabStopSetting = True
        Me.cmbDocPtn.TextValue = ""
        Me.cmbDocPtn.ValueMember = Nothing
        Me.cmbDocPtn.WidthDef = 245
        '
        'cmbDocPtnNomal
        '
        Me.cmbDocPtnNomal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDocPtnNomal.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDocPtnNomal.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbDocPtnNomal.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbDocPtnNomal.DataSource = Nothing
        Me.cmbDocPtnNomal.DisplayMember = Nothing
        Me.cmbDocPtnNomal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDocPtnNomal.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDocPtnNomal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDocPtnNomal.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDocPtnNomal.HeightDef = 18
        Me.cmbDocPtnNomal.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbDocPtnNomal.HissuLabelVisible = True
        Me.cmbDocPtnNomal.InsertWildCard = False
        Me.cmbDocPtnNomal.IsForbiddenWordsCheck = False
        Me.cmbDocPtnNomal.IsHissuCheck = True
        Me.cmbDocPtnNomal.ItemName = ""
        Me.cmbDocPtnNomal.Location = New System.Drawing.Point(200, 855)
        Me.cmbDocPtnNomal.Name = "cmbDocPtnNomal"
        Me.cmbDocPtnNomal.ReadOnly = False
        Me.cmbDocPtnNomal.SelectedIndex = -1
        Me.cmbDocPtnNomal.SelectedItem = Nothing
        Me.cmbDocPtnNomal.SelectedText = ""
        Me.cmbDocPtnNomal.SelectedValue = ""
        Me.cmbDocPtnNomal.Size = New System.Drawing.Size(245, 18)
        Me.cmbDocPtnNomal.TabIndex = 604
        Me.cmbDocPtnNomal.TabStopSetting = True
        Me.cmbDocPtnNomal.TextValue = ""
        Me.cmbDocPtnNomal.ValueMember = Nothing
        Me.cmbDocPtnNomal.WidthDef = 245
        '
        'TitlelblDocPtnNomal
        '
        Me.TitlelblDocPtnNomal.AutoSize = True
        Me.TitlelblDocPtnNomal.AutoSizeDef = True
        Me.TitlelblDocPtnNomal.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocPtnNomal.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblDocPtnNomal.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblDocPtnNomal.EnableStatus = False
        Me.TitlelblDocPtnNomal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocPtnNomal.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblDocPtnNomal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocPtnNomal.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblDocPtnNomal.HeightDef = 13
        Me.TitlelblDocPtnNomal.Location = New System.Drawing.Point(49, 857)
        Me.TitlelblDocPtnNomal.Name = "TitlelblDocPtnNomal"
        Me.TitlelblDocPtnNomal.Size = New System.Drawing.Size(147, 13)
        Me.TitlelblDocPtnNomal.TabIndex = 603
        Me.TitlelblDocPtnNomal.Text = "請求書パターン(通常)"
        Me.TitlelblDocPtnNomal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblDocPtnNomal.TextValue = "請求書パターン(通常)"
        Me.TitlelblDocPtnNomal.WidthDef = 147
        '
        'txtCustKagamiType6
        '
        Me.txtCustKagamiType6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType6.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType6.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType6.CountWrappedLine = False
        Me.txtCustKagamiType6.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType6.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType6.HeightDef = 18
        Me.txtCustKagamiType6.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType6.HissuLabelVisible = False
        Me.txtCustKagamiType6.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType6.IsByteCheck = 20
        Me.txtCustKagamiType6.IsCalendarCheck = False
        Me.txtCustKagamiType6.IsDakutenCheck = False
        Me.txtCustKagamiType6.IsEisuCheck = False
        Me.txtCustKagamiType6.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType6.IsFullByteCheck = 0
        Me.txtCustKagamiType6.IsHankakuCheck = False
        Me.txtCustKagamiType6.IsHissuCheck = False
        Me.txtCustKagamiType6.IsKanaCheck = False
        Me.txtCustKagamiType6.IsMiddleSpace = False
        Me.txtCustKagamiType6.IsNumericCheck = False
        Me.txtCustKagamiType6.IsSujiCheck = False
        Me.txtCustKagamiType6.IsZenkakuCheck = False
        Me.txtCustKagamiType6.ItemName = ""
        Me.txtCustKagamiType6.LineSpace = 0
        Me.txtCustKagamiType6.Location = New System.Drawing.Point(447, 640)
        Me.txtCustKagamiType6.MaxLength = 20
        Me.txtCustKagamiType6.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType6.MaxLineCount = 0
        Me.txtCustKagamiType6.Multiline = False
        Me.txtCustKagamiType6.Name = "txtCustKagamiType6"
        Me.txtCustKagamiType6.ReadOnly = False
        Me.txtCustKagamiType6.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType6.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType6.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType6.TabIndex = 610
        Me.txtCustKagamiType6.TabStopSetting = True
        Me.txtCustKagamiType6.TextValue = ""
        Me.txtCustKagamiType6.UseSystemPasswordChar = False
        Me.txtCustKagamiType6.WidthDef = 173
        Me.txtCustKagamiType6.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami6
        '
        Me.TitlelblCustKagami6.AutoSizeDef = False
        Me.TitlelblCustKagami6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami6.EnableStatus = False
        Me.TitlelblCustKagami6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami6.HeightDef = 13
        Me.TitlelblCustKagami6.Location = New System.Drawing.Point(322, 642)
        Me.TitlelblCustKagami6.Name = "TitlelblCustKagami6"
        Me.TitlelblCustKagami6.Size = New System.Drawing.Size(123, 13)
        Me.TitlelblCustKagami6.TabIndex = 609
        Me.TitlelblCustKagami6.Text = "荷主鑑分類種別6"
        Me.TitlelblCustKagami6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami6.TextValue = "荷主鑑分類種別6"
        Me.TitlelblCustKagami6.WidthDef = 123
        '
        'txtCustKagamiType5
        '
        Me.txtCustKagamiType5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType5.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType5.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType5.CountWrappedLine = False
        Me.txtCustKagamiType5.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType5.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType5.HeightDef = 18
        Me.txtCustKagamiType5.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType5.HissuLabelVisible = False
        Me.txtCustKagamiType5.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType5.IsByteCheck = 20
        Me.txtCustKagamiType5.IsCalendarCheck = False
        Me.txtCustKagamiType5.IsDakutenCheck = False
        Me.txtCustKagamiType5.IsEisuCheck = False
        Me.txtCustKagamiType5.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType5.IsFullByteCheck = 0
        Me.txtCustKagamiType5.IsHankakuCheck = False
        Me.txtCustKagamiType5.IsHissuCheck = False
        Me.txtCustKagamiType5.IsKanaCheck = False
        Me.txtCustKagamiType5.IsMiddleSpace = False
        Me.txtCustKagamiType5.IsNumericCheck = False
        Me.txtCustKagamiType5.IsSujiCheck = False
        Me.txtCustKagamiType5.IsZenkakuCheck = False
        Me.txtCustKagamiType5.ItemName = ""
        Me.txtCustKagamiType5.LineSpace = 0
        Me.txtCustKagamiType5.Location = New System.Drawing.Point(447, 619)
        Me.txtCustKagamiType5.MaxLength = 20
        Me.txtCustKagamiType5.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType5.MaxLineCount = 0
        Me.txtCustKagamiType5.Multiline = False
        Me.txtCustKagamiType5.Name = "txtCustKagamiType5"
        Me.txtCustKagamiType5.ReadOnly = False
        Me.txtCustKagamiType5.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType5.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType5.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType5.TabIndex = 608
        Me.txtCustKagamiType5.TabStopSetting = True
        Me.txtCustKagamiType5.TextValue = ""
        Me.txtCustKagamiType5.UseSystemPasswordChar = False
        Me.txtCustKagamiType5.WidthDef = 173
        Me.txtCustKagamiType5.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami5
        '
        Me.TitlelblCustKagami5.AutoSizeDef = False
        Me.TitlelblCustKagami5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami5.EnableStatus = False
        Me.TitlelblCustKagami5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami5.HeightDef = 15
        Me.TitlelblCustKagami5.Location = New System.Drawing.Point(322, 619)
        Me.TitlelblCustKagami5.Name = "TitlelblCustKagami5"
        Me.TitlelblCustKagami5.Size = New System.Drawing.Size(123, 15)
        Me.TitlelblCustKagami5.TabIndex = 607
        Me.TitlelblCustKagami5.Text = "荷主鑑分類種別5"
        Me.TitlelblCustKagami5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami5.TextValue = "荷主鑑分類種別5"
        Me.TitlelblCustKagami5.WidthDef = 123
        '
        'txtCustKagamiType4
        '
        Me.txtCustKagamiType4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType4.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType4.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType4.CountWrappedLine = False
        Me.txtCustKagamiType4.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType4.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType4.HeightDef = 18
        Me.txtCustKagamiType4.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType4.HissuLabelVisible = False
        Me.txtCustKagamiType4.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType4.IsByteCheck = 20
        Me.txtCustKagamiType4.IsCalendarCheck = False
        Me.txtCustKagamiType4.IsDakutenCheck = False
        Me.txtCustKagamiType4.IsEisuCheck = False
        Me.txtCustKagamiType4.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType4.IsFullByteCheck = 0
        Me.txtCustKagamiType4.IsHankakuCheck = False
        Me.txtCustKagamiType4.IsHissuCheck = False
        Me.txtCustKagamiType4.IsKanaCheck = False
        Me.txtCustKagamiType4.IsMiddleSpace = False
        Me.txtCustKagamiType4.IsNumericCheck = False
        Me.txtCustKagamiType4.IsSujiCheck = False
        Me.txtCustKagamiType4.IsZenkakuCheck = False
        Me.txtCustKagamiType4.ItemName = ""
        Me.txtCustKagamiType4.LineSpace = 0
        Me.txtCustKagamiType4.Location = New System.Drawing.Point(447, 598)
        Me.txtCustKagamiType4.MaxLength = 20
        Me.txtCustKagamiType4.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType4.MaxLineCount = 0
        Me.txtCustKagamiType4.Multiline = False
        Me.txtCustKagamiType4.Name = "txtCustKagamiType4"
        Me.txtCustKagamiType4.ReadOnly = False
        Me.txtCustKagamiType4.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType4.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType4.TabIndex = 606
        Me.txtCustKagamiType4.TabStopSetting = True
        Me.txtCustKagamiType4.TextValue = ""
        Me.txtCustKagamiType4.UseSystemPasswordChar = False
        Me.txtCustKagamiType4.WidthDef = 173
        Me.txtCustKagamiType4.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami4
        '
        Me.TitlelblCustKagami4.AutoSizeDef = False
        Me.TitlelblCustKagami4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami4.EnableStatus = False
        Me.TitlelblCustKagami4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami4.HeightDef = 15
        Me.TitlelblCustKagami4.Location = New System.Drawing.Point(319, 598)
        Me.TitlelblCustKagami4.Name = "TitlelblCustKagami4"
        Me.TitlelblCustKagami4.Size = New System.Drawing.Size(126, 15)
        Me.TitlelblCustKagami4.TabIndex = 605
        Me.TitlelblCustKagami4.Text = "荷主鑑分類種別4"
        Me.TitlelblCustKagami4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami4.TextValue = "荷主鑑分類種別4"
        Me.TitlelblCustKagami4.WidthDef = 126
        '
        'txtCustKagamiType9
        '
        Me.txtCustKagamiType9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType9.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType9.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType9.CountWrappedLine = False
        Me.txtCustKagamiType9.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType9.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType9.HeightDef = 18
        Me.txtCustKagamiType9.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType9.HissuLabelVisible = False
        Me.txtCustKagamiType9.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType9.IsByteCheck = 20
        Me.txtCustKagamiType9.IsCalendarCheck = False
        Me.txtCustKagamiType9.IsDakutenCheck = False
        Me.txtCustKagamiType9.IsEisuCheck = False
        Me.txtCustKagamiType9.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType9.IsFullByteCheck = 0
        Me.txtCustKagamiType9.IsHankakuCheck = False
        Me.txtCustKagamiType9.IsHissuCheck = False
        Me.txtCustKagamiType9.IsKanaCheck = False
        Me.txtCustKagamiType9.IsMiddleSpace = False
        Me.txtCustKagamiType9.IsNumericCheck = False
        Me.txtCustKagamiType9.IsSujiCheck = False
        Me.txtCustKagamiType9.IsZenkakuCheck = False
        Me.txtCustKagamiType9.ItemName = ""
        Me.txtCustKagamiType9.LineSpace = 0
        Me.txtCustKagamiType9.Location = New System.Drawing.Point(740, 640)
        Me.txtCustKagamiType9.MaxLength = 20
        Me.txtCustKagamiType9.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType9.MaxLineCount = 0
        Me.txtCustKagamiType9.Multiline = False
        Me.txtCustKagamiType9.Name = "txtCustKagamiType9"
        Me.txtCustKagamiType9.ReadOnly = False
        Me.txtCustKagamiType9.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType9.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType9.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType9.TabIndex = 616
        Me.txtCustKagamiType9.TabStopSetting = True
        Me.txtCustKagamiType9.TextValue = ""
        Me.txtCustKagamiType9.UseSystemPasswordChar = False
        Me.txtCustKagamiType9.WidthDef = 173
        Me.txtCustKagamiType9.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami9
        '
        Me.TitlelblCustKagami9.AutoSizeDef = False
        Me.TitlelblCustKagami9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami9.EnableStatus = False
        Me.TitlelblCustKagami9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami9.HeightDef = 13
        Me.TitlelblCustKagami9.Location = New System.Drawing.Point(611, 642)
        Me.TitlelblCustKagami9.Name = "TitlelblCustKagami9"
        Me.TitlelblCustKagami9.Size = New System.Drawing.Size(127, 13)
        Me.TitlelblCustKagami9.TabIndex = 615
        Me.TitlelblCustKagami9.Text = "荷主鑑分類種別9"
        Me.TitlelblCustKagami9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami9.TextValue = "荷主鑑分類種別9"
        Me.TitlelblCustKagami9.WidthDef = 127
        '
        'txtCustKagamiType8
        '
        Me.txtCustKagamiType8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType8.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType8.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType8.CountWrappedLine = False
        Me.txtCustKagamiType8.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType8.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType8.HeightDef = 18
        Me.txtCustKagamiType8.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType8.HissuLabelVisible = False
        Me.txtCustKagamiType8.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType8.IsByteCheck = 20
        Me.txtCustKagamiType8.IsCalendarCheck = False
        Me.txtCustKagamiType8.IsDakutenCheck = False
        Me.txtCustKagamiType8.IsEisuCheck = False
        Me.txtCustKagamiType8.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType8.IsFullByteCheck = 0
        Me.txtCustKagamiType8.IsHankakuCheck = False
        Me.txtCustKagamiType8.IsHissuCheck = False
        Me.txtCustKagamiType8.IsKanaCheck = False
        Me.txtCustKagamiType8.IsMiddleSpace = False
        Me.txtCustKagamiType8.IsNumericCheck = False
        Me.txtCustKagamiType8.IsSujiCheck = False
        Me.txtCustKagamiType8.IsZenkakuCheck = False
        Me.txtCustKagamiType8.ItemName = ""
        Me.txtCustKagamiType8.LineSpace = 0
        Me.txtCustKagamiType8.Location = New System.Drawing.Point(740, 619)
        Me.txtCustKagamiType8.MaxLength = 20
        Me.txtCustKagamiType8.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType8.MaxLineCount = 0
        Me.txtCustKagamiType8.Multiline = False
        Me.txtCustKagamiType8.Name = "txtCustKagamiType8"
        Me.txtCustKagamiType8.ReadOnly = False
        Me.txtCustKagamiType8.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType8.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType8.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType8.TabIndex = 614
        Me.txtCustKagamiType8.TabStopSetting = True
        Me.txtCustKagamiType8.TextValue = ""
        Me.txtCustKagamiType8.UseSystemPasswordChar = False
        Me.txtCustKagamiType8.WidthDef = 173
        Me.txtCustKagamiType8.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami8
        '
        Me.TitlelblCustKagami8.AutoSizeDef = False
        Me.TitlelblCustKagami8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami8.EnableStatus = False
        Me.TitlelblCustKagami8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami8.HeightDef = 13
        Me.TitlelblCustKagami8.Location = New System.Drawing.Point(611, 621)
        Me.TitlelblCustKagami8.Name = "TitlelblCustKagami8"
        Me.TitlelblCustKagami8.Size = New System.Drawing.Size(127, 13)
        Me.TitlelblCustKagami8.TabIndex = 613
        Me.TitlelblCustKagami8.Text = "荷主鑑分類種別8"
        Me.TitlelblCustKagami8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami8.TextValue = "荷主鑑分類種別8"
        Me.TitlelblCustKagami8.WidthDef = 127
        '
        'txtCustKagamiType7
        '
        Me.txtCustKagamiType7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustKagamiType7.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustKagamiType7.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustKagamiType7.CountWrappedLine = False
        Me.txtCustKagamiType7.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustKagamiType7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustKagamiType7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustKagamiType7.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustKagamiType7.HeightDef = 18
        Me.txtCustKagamiType7.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustKagamiType7.HissuLabelVisible = False
        Me.txtCustKagamiType7.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustKagamiType7.IsByteCheck = 20
        Me.txtCustKagamiType7.IsCalendarCheck = False
        Me.txtCustKagamiType7.IsDakutenCheck = False
        Me.txtCustKagamiType7.IsEisuCheck = False
        Me.txtCustKagamiType7.IsForbiddenWordsCheck = False
        Me.txtCustKagamiType7.IsFullByteCheck = 0
        Me.txtCustKagamiType7.IsHankakuCheck = False
        Me.txtCustKagamiType7.IsHissuCheck = False
        Me.txtCustKagamiType7.IsKanaCheck = False
        Me.txtCustKagamiType7.IsMiddleSpace = False
        Me.txtCustKagamiType7.IsNumericCheck = False
        Me.txtCustKagamiType7.IsSujiCheck = False
        Me.txtCustKagamiType7.IsZenkakuCheck = False
        Me.txtCustKagamiType7.ItemName = ""
        Me.txtCustKagamiType7.LineSpace = 0
        Me.txtCustKagamiType7.Location = New System.Drawing.Point(740, 598)
        Me.txtCustKagamiType7.MaxLength = 20
        Me.txtCustKagamiType7.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustKagamiType7.MaxLineCount = 0
        Me.txtCustKagamiType7.Multiline = False
        Me.txtCustKagamiType7.Name = "txtCustKagamiType7"
        Me.txtCustKagamiType7.ReadOnly = False
        Me.txtCustKagamiType7.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustKagamiType7.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustKagamiType7.Size = New System.Drawing.Size(173, 18)
        Me.txtCustKagamiType7.TabIndex = 612
        Me.txtCustKagamiType7.TabStopSetting = True
        Me.txtCustKagamiType7.TextValue = ""
        Me.txtCustKagamiType7.UseSystemPasswordChar = False
        Me.txtCustKagamiType7.WidthDef = 173
        Me.txtCustKagamiType7.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblCustKagami7
        '
        Me.TitlelblCustKagami7.AutoSizeDef = False
        Me.TitlelblCustKagami7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblCustKagami7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblCustKagami7.EnableStatus = False
        Me.TitlelblCustKagami7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblCustKagami7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblCustKagami7.HeightDef = 15
        Me.TitlelblCustKagami7.Location = New System.Drawing.Point(608, 598)
        Me.TitlelblCustKagami7.Name = "TitlelblCustKagami7"
        Me.TitlelblCustKagami7.Size = New System.Drawing.Size(130, 15)
        Me.TitlelblCustKagami7.TabIndex = 611
        Me.TitlelblCustKagami7.Text = "荷主鑑分類種別7"
        Me.TitlelblCustKagami7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblCustKagami7.TextValue = "荷主鑑分類種別7"
        Me.TitlelblCustKagami7.WidthDef = 130
        '
        'cmbSeiqCurrCd
        '
        Me.cmbSeiqCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSeiqCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSeiqCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSeiqCurrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSeiqCurrCd.DataSource = Nothing
        Me.cmbSeiqCurrCd.DisplayMember = Nothing
        Me.cmbSeiqCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSeiqCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSeiqCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSeiqCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSeiqCurrCd.HeightDef = 18
        Me.cmbSeiqCurrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqCurrCd.HissuLabelVisible = True
        Me.cmbSeiqCurrCd.InsertWildCard = True
        Me.cmbSeiqCurrCd.IsForbiddenWordsCheck = False
        Me.cmbSeiqCurrCd.IsHissuCheck = True
        Me.cmbSeiqCurrCd.ItemName = ""
        Me.cmbSeiqCurrCd.Location = New System.Drawing.Point(447, 482)
        Me.cmbSeiqCurrCd.Name = "cmbSeiqCurrCd"
        Me.cmbSeiqCurrCd.ReadOnly = False
        Me.cmbSeiqCurrCd.SelectedIndex = -1
        Me.cmbSeiqCurrCd.SelectedItem = Nothing
        Me.cmbSeiqCurrCd.SelectedText = ""
        Me.cmbSeiqCurrCd.SelectedValue = ""
        Me.cmbSeiqCurrCd.Size = New System.Drawing.Size(62, 18)
        Me.cmbSeiqCurrCd.TabIndex = 618
        Me.cmbSeiqCurrCd.TabStopSetting = True
        Me.cmbSeiqCurrCd.TextValue = ""
        Me.cmbSeiqCurrCd.ValueMember = Nothing
        Me.cmbSeiqCurrCd.WidthDef = 62
        '
        'lblTitleSeiqCurrCd
        '
        Me.lblTitleSeiqCurrCd.AutoSize = True
        Me.lblTitleSeiqCurrCd.AutoSizeDef = True
        Me.lblTitleSeiqCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqCurrCd.EnableStatus = False
        Me.lblTitleSeiqCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqCurrCd.HeightDef = 13
        Me.lblTitleSeiqCurrCd.Location = New System.Drawing.Point(340, 483)
        Me.lblTitleSeiqCurrCd.Name = "lblTitleSeiqCurrCd"
        Me.lblTitleSeiqCurrCd.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSeiqCurrCd.TabIndex = 617
        Me.lblTitleSeiqCurrCd.Text = "請求通貨コード"
        Me.lblTitleSeiqCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqCurrCd.TextValue = "請求通貨コード"
        Me.lblTitleSeiqCurrCd.WidthDef = 105
        '
        'TitleEigyoTanto
        '
        Me.TitleEigyoTanto.AutoSizeDef = False
        Me.TitleEigyoTanto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleEigyoTanto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleEigyoTanto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitleEigyoTanto.EnableStatus = False
        Me.TitleEigyoTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleEigyoTanto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleEigyoTanto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleEigyoTanto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleEigyoTanto.HeightDef = 19
        Me.TitleEigyoTanto.Location = New System.Drawing.Point(683, 419)
        Me.TitleEigyoTanto.Name = "TitleEigyoTanto"
        Me.TitleEigyoTanto.Size = New System.Drawing.Size(93, 19)
        Me.TitleEigyoTanto.TabIndex = 619
        Me.TitleEigyoTanto.Text = "営業担当者名"
        Me.TitleEigyoTanto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitleEigyoTanto.TextValue = "営業担当者名"
        Me.TitleEigyoTanto.WidthDef = 93
        '
        'txtEigyoTanto
        '
        Me.txtEigyoTanto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEigyoTanto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtEigyoTanto.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEigyoTanto.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEigyoTanto.CountWrappedLine = False
        Me.txtEigyoTanto.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEigyoTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEigyoTanto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEigyoTanto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEigyoTanto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEigyoTanto.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEigyoTanto.HeightDef = 18
        Me.txtEigyoTanto.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEigyoTanto.HissuLabelVisible = False
        Me.txtEigyoTanto.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtEigyoTanto.IsByteCheck = 30
        Me.txtEigyoTanto.IsCalendarCheck = False
        Me.txtEigyoTanto.IsDakutenCheck = False
        Me.txtEigyoTanto.IsEisuCheck = False
        Me.txtEigyoTanto.IsForbiddenWordsCheck = False
        Me.txtEigyoTanto.IsFullByteCheck = 0
        Me.txtEigyoTanto.IsHankakuCheck = False
        Me.txtEigyoTanto.IsHissuCheck = False
        Me.txtEigyoTanto.IsKanaCheck = False
        Me.txtEigyoTanto.IsMiddleSpace = False
        Me.txtEigyoTanto.IsNumericCheck = False
        Me.txtEigyoTanto.IsSujiCheck = False
        Me.txtEigyoTanto.IsZenkakuCheck = False
        Me.txtEigyoTanto.ItemName = ""
        Me.txtEigyoTanto.LineSpace = 0
        Me.txtEigyoTanto.Location = New System.Drawing.Point(779, 420)
        Me.txtEigyoTanto.MaxLength = 30
        Me.txtEigyoTanto.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEigyoTanto.MaxLineCount = 0
        Me.txtEigyoTanto.Multiline = False
        Me.txtEigyoTanto.Name = "txtEigyoTanto"
        Me.txtEigyoTanto.ReadOnly = False
        Me.txtEigyoTanto.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEigyoTanto.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEigyoTanto.Size = New System.Drawing.Size(257, 18)
        Me.txtEigyoTanto.TabIndex = 620
        Me.txtEigyoTanto.TabStopSetting = True
        Me.txtEigyoTanto.TextValue = ""
        Me.txtEigyoTanto.UseSystemPasswordChar = False
        Me.txtEigyoTanto.WidthDef = 257
        Me.txtEigyoTanto.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtTekiyo
        '
        Me.txtTekiyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTekiyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTekiyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTekiyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTekiyo.CountWrappedLine = False
        Me.txtTekiyo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTekiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTekiyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTekiyo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTekiyo.HeightDef = 18
        Me.txtTekiyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTekiyo.HissuLabelVisible = False
        Me.txtTekiyo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtTekiyo.IsByteCheck = 100
        Me.txtTekiyo.IsCalendarCheck = False
        Me.txtTekiyo.IsDakutenCheck = False
        Me.txtTekiyo.IsEisuCheck = False
        Me.txtTekiyo.IsForbiddenWordsCheck = False
        Me.txtTekiyo.IsFullByteCheck = 0
        Me.txtTekiyo.IsHankakuCheck = False
        Me.txtTekiyo.IsHissuCheck = False
        Me.txtTekiyo.IsKanaCheck = False
        Me.txtTekiyo.IsMiddleSpace = False
        Me.txtTekiyo.IsNumericCheck = False
        Me.txtTekiyo.IsSujiCheck = False
        Me.txtTekiyo.IsZenkakuCheck = False
        Me.txtTekiyo.ItemName = ""
        Me.txtTekiyo.LineSpace = 0
        Me.txtTekiyo.Location = New System.Drawing.Point(156, 569)
        Me.txtTekiyo.MaxLength = 100
        Me.txtTekiyo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTekiyo.MaxLineCount = 0
        Me.txtTekiyo.Multiline = False
        Me.txtTekiyo.Name = "txtTekiyo"
        Me.txtTekiyo.ReadOnly = False
        Me.txtTekiyo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTekiyo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTekiyo.Size = New System.Drawing.Size(750, 18)
        Me.txtTekiyo.TabIndex = 622
        Me.txtTekiyo.TabStopSetting = True
        Me.txtTekiyo.Tag = ""
        Me.txtTekiyo.TextValue = ""
        Me.txtTekiyo.UseSystemPasswordChar = False
        Me.txtTekiyo.WidthDef = 750
        Me.txtTekiyo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitleltxtTekiyo
        '
        Me.TitleltxtTekiyo.AutoSizeDef = False
        Me.TitleltxtTekiyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleltxtTekiyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleltxtTekiyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitleltxtTekiyo.EnableStatus = False
        Me.TitleltxtTekiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleltxtTekiyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleltxtTekiyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleltxtTekiyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleltxtTekiyo.HeightDef = 13
        Me.TitleltxtTekiyo.Location = New System.Drawing.Point(84, 572)
        Me.TitleltxtTekiyo.Name = "TitleltxtTekiyo"
        Me.TitleltxtTekiyo.Size = New System.Drawing.Size(70, 13)
        Me.TitleltxtTekiyo.TabIndex = 621
        Me.TitleltxtTekiyo.Text = "摘要"
        Me.TitleltxtTekiyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitleltxtTekiyo.TextValue = "摘要"
        Me.TitleltxtTekiyo.WidthDef = 70
        '
        'chkdest
        '
        Me.chkdest.AutoSize = True
        Me.chkdest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkdest.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkdest.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkdest.EnableStatus = True
        Me.chkdest.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkdest.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkdest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkdest.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkdest.HeightDef = 17
        Me.chkdest.Location = New System.Drawing.Point(818, 835)
        Me.chkdest.Name = "chkdest"
        Me.chkdest.Size = New System.Drawing.Size(54, 17)
        Me.chkdest.TabIndex = 623
        Me.chkdest.TabStopSetting = True
        Me.chkdest.Text = "宛先"
        Me.chkdest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkdest.TextValue = "宛先"
        Me.chkdest.UseVisualStyleBackColor = True
        Me.chkdest.WidthDef = 54
        '
        'grpVarStrage
        '
        Me.grpVarStrage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpVarStrage.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpVarStrage.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpVarStrage.Controls.Add(Me.optVarStrageFlgN)
        Me.grpVarStrage.Controls.Add(Me.optVarStrageFlgY)
        Me.grpVarStrage.Enabled = False
        Me.grpVarStrage.EnableStatus = False
        Me.grpVarStrage.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpVarStrage.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpVarStrage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpVarStrage.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpVarStrage.HeightDef = 39
        Me.grpVarStrage.Location = New System.Drawing.Point(1020, 570)
        Me.grpVarStrage.Name = "grpVarStrage"
        Me.grpVarStrage.Size = New System.Drawing.Size(242, 39)
        Me.grpVarStrage.TabIndex = 644
        Me.grpVarStrage.TabStop = False
        Me.grpVarStrage.TextValue = ""
        Me.grpVarStrage.WidthDef = 242
        '
        'optVarStrageFlgN
        '
        Me.optVarStrageFlgN.AutoSize = True
        Me.optVarStrageFlgN.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optVarStrageFlgN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optVarStrageFlgN.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optVarStrageFlgN.Checked = True
        Me.optVarStrageFlgN.EnableStatus = True
        Me.optVarStrageFlgN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optVarStrageFlgN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optVarStrageFlgN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optVarStrageFlgN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optVarStrageFlgN.HeightDef = 17
        Me.optVarStrageFlgN.Location = New System.Drawing.Point(93, 14)
        Me.optVarStrageFlgN.Name = "optVarStrageFlgN"
        Me.optVarStrageFlgN.Size = New System.Drawing.Size(53, 17)
        Me.optVarStrageFlgN.TabIndex = 473
        Me.optVarStrageFlgN.TabStop = True
        Me.optVarStrageFlgN.TabStopSetting = True
        Me.optVarStrageFlgN.Text = "なし"
        Me.optVarStrageFlgN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optVarStrageFlgN.TextValue = "なし"
        Me.optVarStrageFlgN.UseVisualStyleBackColor = True
        Me.optVarStrageFlgN.WidthDef = 53
        '
        'optVarStrageFlgY
        '
        Me.optVarStrageFlgY.AutoSize = True
        Me.optVarStrageFlgY.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optVarStrageFlgY.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optVarStrageFlgY.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optVarStrageFlgY.EnableStatus = True
        Me.optVarStrageFlgY.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optVarStrageFlgY.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optVarStrageFlgY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optVarStrageFlgY.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optVarStrageFlgY.HeightDef = 17
        Me.optVarStrageFlgY.Location = New System.Drawing.Point(160, 14)
        Me.optVarStrageFlgY.Name = "optVarStrageFlgY"
        Me.optVarStrageFlgY.Size = New System.Drawing.Size(53, 17)
        Me.optVarStrageFlgY.TabIndex = 472
        Me.optVarStrageFlgY.TabStopSetting = False
        Me.optVarStrageFlgY.Text = "あり"
        Me.optVarStrageFlgY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optVarStrageFlgY.TextValue = "あり"
        Me.optVarStrageFlgY.UseVisualStyleBackColor = True
        Me.optVarStrageFlgY.WidthDef = 53
        '
        'lblTitleVarStrageFlg
        '
        Me.lblTitleVarStrageFlg.AutoSizeDef = False
        Me.lblTitleVarStrageFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarStrageFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarStrageFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleVarStrageFlg.EnableStatus = False
        Me.lblTitleVarStrageFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarStrageFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarStrageFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarStrageFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarStrageFlg.HeightDef = 13
        Me.lblTitleVarStrageFlg.Location = New System.Drawing.Point(1026, 586)
        Me.lblTitleVarStrageFlg.Name = "lblTitleVarStrageFlg"
        Me.lblTitleVarStrageFlg.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleVarStrageFlg.TabIndex = 273
        Me.lblTitleVarStrageFlg.Text = "変動保管料"
        Me.lblTitleVarStrageFlg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleVarStrageFlg.TextValue = "変動保管料"
        Me.lblTitleVarStrageFlg.WidthDef = 77
        '
        'cmbVarRate6
        '
        Me.cmbVarRate6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVarRate6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVarRate6.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbVarRate6.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbVarRate6.DataSource = Nothing
        Me.cmbVarRate6.DisplayMember = ""
        Me.cmbVarRate6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVarRate6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVarRate6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVarRate6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVarRate6.HeightDef = 18
        Me.cmbVarRate6.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbVarRate6.HissuLabelVisible = False
        Me.cmbVarRate6.InsertWildCard = True
        Me.cmbVarRate6.IsForbiddenWordsCheck = False
        Me.cmbVarRate6.IsHissuCheck = False
        Me.cmbVarRate6.ItemName = ""
        Me.cmbVarRate6.Location = New System.Drawing.Point(1158, 640)
        Me.cmbVarRate6.Name = "cmbVarRate6"
        Me.cmbVarRate6.ReadOnly = False
        Me.cmbVarRate6.SelectedIndex = -1
        Me.cmbVarRate6.SelectedItem = Nothing
        Me.cmbVarRate6.SelectedText = ""
        Me.cmbVarRate6.SelectedValue = ""
        Me.cmbVarRate6.Size = New System.Drawing.Size(90, 18)
        Me.cmbVarRate6.TabIndex = 478
        Me.cmbVarRate6.TabStopSetting = True
        Me.cmbVarRate6.TextValue = ""
        Me.cmbVarRate6.ValueMember = ""
        Me.cmbVarRate6.WidthDef = 90
        '
        'cmbVarRate3
        '
        Me.cmbVarRate3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVarRate3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVarRate3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbVarRate3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbVarRate3.DataSource = Nothing
        Me.cmbVarRate3.DisplayMember = ""
        Me.cmbVarRate3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVarRate3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVarRate3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVarRate3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVarRate3.HeightDef = 18
        Me.cmbVarRate3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbVarRate3.HissuLabelVisible = False
        Me.cmbVarRate3.InsertWildCard = True
        Me.cmbVarRate3.IsForbiddenWordsCheck = False
        Me.cmbVarRate3.IsHissuCheck = False
        Me.cmbVarRate3.ItemName = ""
        Me.cmbVarRate3.Location = New System.Drawing.Point(1158, 616)
        Me.cmbVarRate3.Name = "cmbVarRate3"
        Me.cmbVarRate3.ReadOnly = False
        Me.cmbVarRate3.SelectedIndex = -1
        Me.cmbVarRate3.SelectedItem = Nothing
        Me.cmbVarRate3.SelectedText = ""
        Me.cmbVarRate3.SelectedValue = ""
        Me.cmbVarRate3.Size = New System.Drawing.Size(90, 18)
        Me.cmbVarRate3.TabIndex = 477
        Me.cmbVarRate3.TabStopSetting = True
        Me.cmbVarRate3.TextValue = ""
        Me.cmbVarRate3.ValueMember = ""
        Me.cmbVarRate3.WidthDef = 90
        '
        'lblTitleVarRate6
        '
        Me.lblTitleVarRate6.AutoSizeDef = False
        Me.lblTitleVarRate6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleVarRate6.EnableStatus = False
        Me.lblTitleVarRate6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate6.HeightDef = 13
        Me.lblTitleVarRate6.Location = New System.Drawing.Point(1111, 643)
        Me.lblTitleVarRate6.Name = "lblTitleVarRate6"
        Me.lblTitleVarRate6.Size = New System.Drawing.Size(42, 13)
        Me.lblTitleVarRate6.TabIndex = 476
        Me.lblTitleVarRate6.Text = "6ヶ月"
        Me.lblTitleVarRate6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleVarRate6.TextValue = "6ヶ月"
        Me.lblTitleVarRate6.WidthDef = 42
        '
        'lblTitleVarRate3
        '
        Me.lblTitleVarRate3.AutoSizeDef = False
        Me.lblTitleVarRate3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleVarRate3.EnableStatus = False
        Me.lblTitleVarRate3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate3.HeightDef = 13
        Me.lblTitleVarRate3.Location = New System.Drawing.Point(1111, 619)
        Me.lblTitleVarRate3.Name = "lblTitleVarRate3"
        Me.lblTitleVarRate3.Size = New System.Drawing.Size(42, 13)
        Me.lblTitleVarRate3.TabIndex = 475
        Me.lblTitleVarRate3.Text = "3ヶ月"
        Me.lblTitleVarRate3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleVarRate3.TextValue = "3ヶ月"
        Me.lblTitleVarRate3.WidthDef = 42
        '
        'lblTitleVarRate
        '
        Me.lblTitleVarRate.AutoSizeDef = False
        Me.lblTitleVarRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVarRate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleVarRate.EnableStatus = False
        Me.lblTitleVarRate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVarRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVarRate.HeightDef = 13
        Me.lblTitleVarRate.Location = New System.Drawing.Point(1026, 619)
        Me.lblTitleVarRate.Name = "lblTitleVarRate"
        Me.lblTitleVarRate.Size = New System.Drawing.Size(64, 13)
        Me.lblTitleVarRate.TabIndex = 474
        Me.lblTitleVarRate.Text = "変動倍率"
        Me.lblTitleVarRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleVarRate.TextValue = "変動倍率"
        Me.lblTitleVarRate.WidthDef = 64
        '
        'LMM050F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM050F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSeiq.ResumeLayout(False)
        Me.grpSeiq.PerformLayout()
        Me.grpVarStrage.ResumeLayout(False)
        Me.grpVarStrage.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtFax As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblTelTitlelblFax As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTel As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtAd3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblAd3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblAd2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblAd1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblTel As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblDocPtn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblKagamiKB As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblSeiqNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblSeiqCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeiqtoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblSentPeriod As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblNrsCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNrsKeiriCd1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblCloseKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtNrsKeiriCd2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtNrsKeiriCd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbCloseKBN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtSeiqSndPeriod As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelbPic As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtZip As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleZip As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOyaPic As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblNg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblNr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStorageNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents TitlelblMin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStorageNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numStorageMinBak As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel52 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel48 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHandlingNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numHandlingNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel44 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel53 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnchinNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numUnchinNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numYokomochiNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numYokomochiNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel65 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numClearanceNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numClearanceNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSagyoNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSagyoNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents TitlelblTotalNg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblTotalNr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numTotalNg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numTotalNr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents TitlelblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSeiq As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtCustKagamiType3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblSeiqBushoNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSeiqtoBusyoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblDocKind As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkSei As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkFuku As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkHikae As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkKeiri As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbKouzaKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblKagamiMeigi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbMeigiKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbDocPtn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents cmbDocPtnNomal As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents TitlelblDocPtnNomal As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType6 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType5 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType9 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType8 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustKagamiType7 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitlelblCustKagami7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSeiqCurrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents lblTitleSeiqCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStorageNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStorageMinCurrCdBak As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTotalNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblYokomochiNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblClearanceNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSagyoNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnchinNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHandlingNgCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkSagyoTotalFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkUnchinTotalFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkHandlingTotalFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTotalMinSeiqCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTotalMinSeiqAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numTotalMinSeiqAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTotalMinChk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkStorageTotalFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblSagyoMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnchinMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHandlingMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStorageMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStorageMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numHandlingMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel19 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSagyoMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numUnchinMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblHandlingOtherMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStorageOtherMinCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStorageOtherMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numHandlingOtherMin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel12 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitleEigyoTanto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtEigyoTanto As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTekiyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents TitleltxtTekiyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeroMin As Win.LMTitleLabel
    Friend WithEvents cmbStorageZeroFlgKBN As Win.InputMan.LMComboKubun
    Friend WithEvents cmbHandlingZeroFlgKBN As Win.InputMan.LMComboKubun
    Friend WithEvents chkdest As Win.LMCheckBox
    Friend WithEvents sprDetail_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents grpVarStrage As Win.LMGroupBox
    Friend WithEvents cmbVarRate6 As Win.InputMan.LMImCombo
    Friend WithEvents cmbVarRate3 As Win.InputMan.LMImCombo
    Friend WithEvents lblTitleVarRate6 As Win.LMTitleLabel
    Friend WithEvents lblTitleVarRate3 As Win.LMTitleLabel
    Friend WithEvents lblTitleVarRate As Win.LMTitleLabel
    Friend WithEvents optVarStrageFlgN As Win.LMOptionButton
    Friend WithEvents optVarStrageFlgY As Win.LMOptionButton
    Friend WithEvents lblTitleVarStrageFlg As Win.LMTitleLabel
End Class
