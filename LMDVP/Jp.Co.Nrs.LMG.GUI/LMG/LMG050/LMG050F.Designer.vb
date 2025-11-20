<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LMG050F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormSxga

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprSeikyuM_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprSeikyuM_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.lblTitleSeikyuMeigi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlSeikyu = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.cmbSeiqCurrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.TitleSeiqtCurrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSeikyuTantoNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeikyuTantoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeikyuNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSeikyuNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeikyuCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSeikyuCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.imdInvDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblSikyuMeigi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeikyuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCreateNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCreateNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkHokan = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkYokomochi = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkNiyaku = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkSagyou = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblTitleSincyoku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSeikyuNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSeikyuNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTorikomi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprSeikyuM = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprSeikyuM_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSeiqShubetsu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSeiqtShubetu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.btnAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.numCalAllK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numCalAllU = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numCalAllH = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numCalAllM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNebikiRateM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNebikiRateK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numRateNebikigakuM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numRateNebikigakuK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNebikiGakuM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNebikiGakuK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numZeigakuK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numZeigakuU = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSeikyuGakuU = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSeikyuGakuH = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSeikyuGakuM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSeikyuGakuK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numZeiHasuK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSeikyuAll = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleCalAllK = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSeikyuGaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleZeiHasu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleZeigaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleRateNebikigaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleNebikiGaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSeikyuAll = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKazei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUchizei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleHikazei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleMenzei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZeigakuM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZeiHasuU = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblNebikiGakuU = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblRateNebikigakuU = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblNebikiRateU = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZeiHasuH = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZeigakuH = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblNebikiGakuH = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblRateNebikigakuH = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblNebikiRateH = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblZeiHasuM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlSeikyuK = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.chkHikaeAri = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkKeiHikaeAri = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkMainAri = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkSubAri = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblTitleBiko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.chkAkaden = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkTemplate = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblYokomochiImpDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyoImpDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblUnchinImpDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblMaxEdaban = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSeikyuNoRelated = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleExRate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblRate1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCurrencyConversion = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numExRate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.cmbStateKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbCurrencyConversion1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbCurrencyConversion2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblUnsoUT = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblUnsoWT = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnsoWT = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.btnSapOut = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnSapCancel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblSapNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSapNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkDepotHokan = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkDepotLift = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkContainerUnso = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        sprSeikyuM_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlSeikyu.SuspendLayout()
        CType(Me.sprSeikyuM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprSeikyuM_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSeikyuK.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlViewAria.Controls.Add(Me.chkContainerUnso)
        Me.pnlViewAria.Controls.Add(Me.chkDepotLift)
        Me.pnlViewAria.Controls.Add(Me.chkDepotHokan)
        Me.pnlViewAria.Controls.Add(Me.lblSapNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSapNo)
        Me.pnlViewAria.Controls.Add(Me.btnSapCancel)
        Me.pnlViewAria.Controls.Add(Me.btnSapOut)
        Me.pnlViewAria.Controls.Add(Me.lblUnsoUT)
        Me.pnlViewAria.Controls.Add(Me.lblUnsoWT)
        Me.pnlViewAria.Controls.Add(Me.numUnsoWT)
        Me.pnlViewAria.Controls.Add(Me.cmbCurrencyConversion2)
        Me.pnlViewAria.Controls.Add(Me.cmbCurrencyConversion1)
        Me.pnlViewAria.Controls.Add(Me.numExRate)
        Me.pnlViewAria.Controls.Add(Me.lblCurrencyConversion)
        Me.pnlViewAria.Controls.Add(Me.lblRate1)
        Me.pnlViewAria.Controls.Add(Me.lblTitleExRate)
        Me.pnlViewAria.Controls.Add(Me.lblSeikyuNoRelated)
        Me.pnlViewAria.Controls.Add(Me.lblMaxEdaban)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblUnchinImpDate)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoImpDate)
        Me.pnlViewAria.Controls.Add(Me.lblYokomochiImpDate)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.chkTemplate)
        Me.pnlViewAria.Controls.Add(Me.cmbStateKbn)
        Me.pnlViewAria.Controls.Add(Me.chkAkaden)
        Me.pnlViewAria.Controls.Add(Me.lblTitleBiko)
        Me.pnlViewAria.Controls.Add(Me.txtRemark)
        Me.pnlViewAria.Controls.Add(Me.imdInvDate)
        Me.pnlViewAria.Controls.Add(Me.lblSikyuMeigi)
        Me.pnlViewAria.Controls.Add(Me.lblCreateNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeikyuDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeikyuMeigi)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCreateNm)
        Me.pnlViewAria.Controls.Add(Me.pnlSeikyuK)
        Me.pnlViewAria.Controls.Add(Me.lblZeiHasuM)
        Me.pnlViewAria.Controls.Add(Me.lblNebikiRateH)
        Me.pnlViewAria.Controls.Add(Me.lblRateNebikigakuH)
        Me.pnlViewAria.Controls.Add(Me.lblNebikiGakuH)
        Me.pnlViewAria.Controls.Add(Me.lblZeigakuH)
        Me.pnlViewAria.Controls.Add(Me.lblZeiHasuH)
        Me.pnlViewAria.Controls.Add(Me.lblNebikiRateU)
        Me.pnlViewAria.Controls.Add(Me.lblRateNebikigakuU)
        Me.pnlViewAria.Controls.Add(Me.lblNebikiGakuU)
        Me.pnlViewAria.Controls.Add(Me.lblZeiHasuU)
        Me.pnlViewAria.Controls.Add(Me.lblZeigakuM)
        Me.pnlViewAria.Controls.Add(Me.lblTitleMenzei)
        Me.pnlViewAria.Controls.Add(Me.lblTitleHikazei)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUchizei)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKazei)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeikyuAll)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNebikiGaku)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRateNebikigaku)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.lblTitleZeigaku)
        Me.pnlViewAria.Controls.Add(Me.lblTitleZeiHasu)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeikyuGaku)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCalAllK)
        Me.pnlViewAria.Controls.Add(Me.numSeikyuAll)
        Me.pnlViewAria.Controls.Add(Me.numZeiHasuK)
        Me.pnlViewAria.Controls.Add(Me.numSeikyuGakuU)
        Me.pnlViewAria.Controls.Add(Me.numSeikyuGakuH)
        Me.pnlViewAria.Controls.Add(Me.numSeikyuGakuM)
        Me.pnlViewAria.Controls.Add(Me.numSeikyuGakuK)
        Me.pnlViewAria.Controls.Add(Me.numZeigakuU)
        Me.pnlViewAria.Controls.Add(Me.numZeigakuK)
        Me.pnlViewAria.Controls.Add(Me.numNebikiGakuM)
        Me.pnlViewAria.Controls.Add(Me.numNebikiGakuK)
        Me.pnlViewAria.Controls.Add(Me.numRateNebikigakuM)
        Me.pnlViewAria.Controls.Add(Me.numRateNebikigakuK)
        Me.pnlViewAria.Controls.Add(Me.numNebikiRateM)
        Me.pnlViewAria.Controls.Add(Me.numNebikiRateK)
        Me.pnlViewAria.Controls.Add(Me.numCalAllU)
        Me.pnlViewAria.Controls.Add(Me.numCalAllH)
        Me.pnlViewAria.Controls.Add(Me.numCalAllM)
        Me.pnlViewAria.Controls.Add(Me.numCalAllK)
        Me.pnlViewAria.Controls.Add(Me.btnDel)
        Me.pnlViewAria.Controls.Add(Me.btnAdd)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.cmbSeiqtShubetu)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqShubetsu)
        Me.pnlViewAria.Controls.Add(Me.cmbBr)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.sprSeikyuM)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTorikomi)
        Me.pnlViewAria.Controls.Add(Me.chkHokan)
        Me.pnlViewAria.Controls.Add(Me.chkYokomochi)
        Me.pnlViewAria.Controls.Add(Me.chkNiyaku)
        Me.pnlViewAria.Controls.Add(Me.chkSagyou)
        Me.pnlViewAria.Controls.Add(Me.chkUnchin)
        Me.pnlViewAria.Controls.Add(Me.lblSeikyuNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeikyuNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSincyoku)
        Me.pnlViewAria.Controls.Add(Me.pnlSeikyu)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 812)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'lblTitleSeikyuMeigi
        '
        Me.lblTitleSeikyuMeigi.AutoSize = True
        Me.lblTitleSeikyuMeigi.AutoSizeDef = True
        Me.lblTitleSeikyuMeigi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuMeigi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuMeigi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuMeigi.EnableStatus = False
        Me.lblTitleSeikyuMeigi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuMeigi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuMeigi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuMeigi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuMeigi.HeightDef = 13
        Me.lblTitleSeikyuMeigi.Location = New System.Drawing.Point(36, 213)
        Me.lblTitleSeikyuMeigi.Name = "lblTitleSeikyuMeigi"
        Me.lblTitleSeikyuMeigi.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSeikyuMeigi.TabIndex = 1
        Me.lblTitleSeikyuMeigi.Text = "請求書名義"
        Me.lblTitleSeikyuMeigi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuMeigi.TextValue = "請求書名義"
        Me.lblTitleSeikyuMeigi.WidthDef = 77
        '
        'pnlSeikyu
        '
        Me.pnlSeikyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSeikyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSeikyu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSeikyu.Controls.Add(Me.cmbSeiqCurrCd)
        Me.pnlSeikyu.Controls.Add(Me.TitleSeiqtCurrCd)
        Me.pnlSeikyu.Controls.Add(Me.lblTitleSeikyuTantoNm)
        Me.pnlSeikyu.Controls.Add(Me.txtSeikyuTantoNm)
        Me.pnlSeikyu.Controls.Add(Me.lblTitleSeikyuNm)
        Me.pnlSeikyu.Controls.Add(Me.lblSeikyuNm)
        Me.pnlSeikyu.Controls.Add(Me.lblTitleSeikyuCd)
        Me.pnlSeikyu.Controls.Add(Me.txtSeikyuCd)
        Me.pnlSeikyu.EnableStatus = False
        Me.pnlSeikyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSeikyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSeikyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSeikyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSeikyu.HeightDef = 83
        Me.pnlSeikyu.Location = New System.Drawing.Point(13, 99)
        Me.pnlSeikyu.Name = "pnlSeikyu"
        Me.pnlSeikyu.Size = New System.Drawing.Size(505, 83)
        Me.pnlSeikyu.TabIndex = 2
        Me.pnlSeikyu.TabStop = False
        Me.pnlSeikyu.Text = "請求先情報"
        Me.pnlSeikyu.TextValue = "請求先情報"
        Me.pnlSeikyu.WidthDef = 505
        '
        'cmbSeiqCurrCd
        '
        Me.cmbSeiqCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSeiqCurrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSeiqCurrCd.DataCode = ""
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
        Me.cmbSeiqCurrCd.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSeiqCurrCd.Location = New System.Drawing.Point(341, 16)
        Me.cmbSeiqCurrCd.Name = "cmbSeiqCurrCd"
        Me.cmbSeiqCurrCd.ReadOnly = True
        Me.cmbSeiqCurrCd.SelectedIndex = -1
        Me.cmbSeiqCurrCd.SelectedItem = Nothing
        Me.cmbSeiqCurrCd.SelectedText = ""
        Me.cmbSeiqCurrCd.SelectedValue = ""
        Me.cmbSeiqCurrCd.Size = New System.Drawing.Size(157, 18)
        Me.cmbSeiqCurrCd.TabIndex = 579
        Me.cmbSeiqCurrCd.TabStop = False
        Me.cmbSeiqCurrCd.TabStopSetting = False
        Me.cmbSeiqCurrCd.TextValue = ""
        Me.cmbSeiqCurrCd.Value1 = Nothing
        Me.cmbSeiqCurrCd.Value2 = Nothing
        Me.cmbSeiqCurrCd.Value3 = Nothing
        Me.cmbSeiqCurrCd.ValueMember = Nothing
        Me.cmbSeiqCurrCd.WidthDef = 157
        '
        'TitleSeiqtCurrCd
        '
        Me.TitleSeiqtCurrCd.AutoSize = True
        Me.TitleSeiqtCurrCd.AutoSizeDef = True
        Me.TitleSeiqtCurrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleSeiqtCurrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleSeiqtCurrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitleSeiqtCurrCd.EnableStatus = False
        Me.TitleSeiqtCurrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleSeiqtCurrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleSeiqtCurrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleSeiqtCurrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleSeiqtCurrCd.HeightDef = 13
        Me.TitleSeiqtCurrCd.Location = New System.Drawing.Point(275, 18)
        Me.TitleSeiqtCurrCd.Name = "TitleSeiqtCurrCd"
        Me.TitleSeiqtCurrCd.Size = New System.Drawing.Size(63, 13)
        Me.TitleSeiqtCurrCd.TabIndex = 214
        Me.TitleSeiqtCurrCd.Text = "請求通貨"
        Me.TitleSeiqtCurrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitleSeiqtCurrCd.TextValue = "請求通貨"
        Me.TitleSeiqtCurrCd.WidthDef = 63
        '
        'lblTitleSeikyuTantoNm
        '
        Me.lblTitleSeikyuTantoNm.AutoSize = True
        Me.lblTitleSeikyuTantoNm.AutoSizeDef = True
        Me.lblTitleSeikyuTantoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuTantoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuTantoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuTantoNm.EnableStatus = False
        Me.lblTitleSeikyuTantoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuTantoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuTantoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuTantoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuTantoNm.HeightDef = 13
        Me.lblTitleSeikyuTantoNm.Location = New System.Drawing.Point(37, 61)
        Me.lblTitleSeikyuTantoNm.Name = "lblTitleSeikyuTantoNm"
        Me.lblTitleSeikyuTantoNm.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSeikyuTantoNm.TabIndex = 40
        Me.lblTitleSeikyuTantoNm.Text = "担当者名"
        Me.lblTitleSeikyuTantoNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuTantoNm.TextValue = "担当者名"
        Me.lblTitleSeikyuTantoNm.WidthDef = 63
        '
        'txtSeikyuTantoNm
        '
        Me.txtSeikyuTantoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeikyuTantoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeikyuTantoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeikyuTantoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeikyuTantoNm.CountWrappedLine = False
        Me.txtSeikyuTantoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeikyuTantoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuTantoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuTantoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeikyuTantoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeikyuTantoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeikyuTantoNm.HeightDef = 18
        Me.txtSeikyuTantoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeikyuTantoNm.HissuLabelVisible = False
        Me.txtSeikyuTantoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSeikyuTantoNm.IsByteCheck = 20
        Me.txtSeikyuTantoNm.IsCalendarCheck = False
        Me.txtSeikyuTantoNm.IsDakutenCheck = False
        Me.txtSeikyuTantoNm.IsEisuCheck = False
        Me.txtSeikyuTantoNm.IsForbiddenWordsCheck = False
        Me.txtSeikyuTantoNm.IsFullByteCheck = 0
        Me.txtSeikyuTantoNm.IsHankakuCheck = False
        Me.txtSeikyuTantoNm.IsHissuCheck = False
        Me.txtSeikyuTantoNm.IsKanaCheck = False
        Me.txtSeikyuTantoNm.IsMiddleSpace = False
        Me.txtSeikyuTantoNm.IsNumericCheck = False
        Me.txtSeikyuTantoNm.IsSujiCheck = False
        Me.txtSeikyuTantoNm.IsZenkakuCheck = False
        Me.txtSeikyuTantoNm.ItemName = ""
        Me.txtSeikyuTantoNm.LineSpace = 0
        Me.txtSeikyuTantoNm.Location = New System.Drawing.Point(109, 56)
        Me.txtSeikyuTantoNm.MaxLength = 20
        Me.txtSeikyuTantoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeikyuTantoNm.MaxLineCount = 0
        Me.txtSeikyuTantoNm.Multiline = False
        Me.txtSeikyuTantoNm.Name = "txtSeikyuTantoNm"
        Me.txtSeikyuTantoNm.ReadOnly = False
        Me.txtSeikyuTantoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeikyuTantoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeikyuTantoNm.Size = New System.Drawing.Size(165, 18)
        Me.txtSeikyuTantoNm.TabIndex = 39
        Me.txtSeikyuTantoNm.TabStopSetting = True
        Me.txtSeikyuTantoNm.TextValue = ""
        Me.txtSeikyuTantoNm.UseSystemPasswordChar = False
        Me.txtSeikyuTantoNm.WidthDef = 165
        Me.txtSeikyuTantoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeikyuNm
        '
        Me.lblTitleSeikyuNm.AutoSize = True
        Me.lblTitleSeikyuNm.AutoSizeDef = True
        Me.lblTitleSeikyuNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuNm.EnableStatus = False
        Me.lblTitleSeikyuNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuNm.HeightDef = 13
        Me.lblTitleSeikyuNm.Location = New System.Drawing.Point(39, 41)
        Me.lblTitleSeikyuNm.Name = "lblTitleSeikyuNm"
        Me.lblTitleSeikyuNm.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSeikyuNm.TabIndex = 27
        Me.lblTitleSeikyuNm.Text = "請求先名"
        Me.lblTitleSeikyuNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuNm.TextValue = "請求先名"
        Me.lblTitleSeikyuNm.WidthDef = 63
        '
        'lblSeikyuNm
        '
        Me.lblSeikyuNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeikyuNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeikyuNm.CountWrappedLine = False
        Me.lblSeikyuNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeikyuNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeikyuNm.HeightDef = 18
        Me.lblSeikyuNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNm.HissuLabelVisible = False
        Me.lblSeikyuNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeikyuNm.IsByteCheck = 0
        Me.lblSeikyuNm.IsCalendarCheck = False
        Me.lblSeikyuNm.IsDakutenCheck = False
        Me.lblSeikyuNm.IsEisuCheck = False
        Me.lblSeikyuNm.IsForbiddenWordsCheck = False
        Me.lblSeikyuNm.IsFullByteCheck = 0
        Me.lblSeikyuNm.IsHankakuCheck = False
        Me.lblSeikyuNm.IsHissuCheck = False
        Me.lblSeikyuNm.IsKanaCheck = False
        Me.lblSeikyuNm.IsMiddleSpace = False
        Me.lblSeikyuNm.IsNumericCheck = False
        Me.lblSeikyuNm.IsSujiCheck = False
        Me.lblSeikyuNm.IsZenkakuCheck = False
        Me.lblSeikyuNm.ItemName = ""
        Me.lblSeikyuNm.LineSpace = 0
        Me.lblSeikyuNm.Location = New System.Drawing.Point(109, 36)
        Me.lblSeikyuNm.MaxLength = 0
        Me.lblSeikyuNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeikyuNm.MaxLineCount = 0
        Me.lblSeikyuNm.Multiline = False
        Me.lblSeikyuNm.Name = "lblSeikyuNm"
        Me.lblSeikyuNm.ReadOnly = True
        Me.lblSeikyuNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeikyuNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeikyuNm.Size = New System.Drawing.Size(389, 18)
        Me.lblSeikyuNm.TabIndex = 7
        Me.lblSeikyuNm.TabStop = False
        Me.lblSeikyuNm.TabStopSetting = False
        Me.lblSeikyuNm.TextValue = ""
        Me.lblSeikyuNm.UseSystemPasswordChar = False
        Me.lblSeikyuNm.WidthDef = 389
        Me.lblSeikyuNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeikyuCd
        '
        Me.lblTitleSeikyuCd.AutoSize = True
        Me.lblTitleSeikyuCd.AutoSizeDef = True
        Me.lblTitleSeikyuCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuCd.EnableStatus = False
        Me.lblTitleSeikyuCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuCd.HeightDef = 13
        Me.lblTitleSeikyuCd.Location = New System.Drawing.Point(11, 21)
        Me.lblTitleSeikyuCd.Name = "lblTitleSeikyuCd"
        Me.lblTitleSeikyuCd.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleSeikyuCd.TabIndex = 5
        Me.lblTitleSeikyuCd.Text = "請求先コード"
        Me.lblTitleSeikyuCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuCd.TextValue = "請求先コード"
        Me.lblTitleSeikyuCd.WidthDef = 91
        '
        'txtSeikyuCd
        '
        Me.txtSeikyuCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeikyuCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeikyuCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeikyuCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeikyuCd.CountWrappedLine = False
        Me.txtSeikyuCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeikyuCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeikyuCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeikyuCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeikyuCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeikyuCd.HeightDef = 18
        Me.txtSeikyuCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeikyuCd.HissuLabelVisible = True
        Me.txtSeikyuCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSeikyuCd.IsByteCheck = 7
        Me.txtSeikyuCd.IsCalendarCheck = False
        Me.txtSeikyuCd.IsDakutenCheck = False
        Me.txtSeikyuCd.IsEisuCheck = False
        Me.txtSeikyuCd.IsForbiddenWordsCheck = False
        Me.txtSeikyuCd.IsFullByteCheck = 0
        Me.txtSeikyuCd.IsHankakuCheck = False
        Me.txtSeikyuCd.IsHissuCheck = True
        Me.txtSeikyuCd.IsKanaCheck = False
        Me.txtSeikyuCd.IsMiddleSpace = False
        Me.txtSeikyuCd.IsNumericCheck = False
        Me.txtSeikyuCd.IsSujiCheck = False
        Me.txtSeikyuCd.IsZenkakuCheck = False
        Me.txtSeikyuCd.ItemName = ""
        Me.txtSeikyuCd.LineSpace = 0
        Me.txtSeikyuCd.Location = New System.Drawing.Point(109, 16)
        Me.txtSeikyuCd.MaxLength = 7
        Me.txtSeikyuCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeikyuCd.MaxLineCount = 0
        Me.txtSeikyuCd.Multiline = False
        Me.txtSeikyuCd.Name = "txtSeikyuCd"
        Me.txtSeikyuCd.ReadOnly = False
        Me.txtSeikyuCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeikyuCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeikyuCd.Size = New System.Drawing.Size(82, 18)
        Me.txtSeikyuCd.TabIndex = 6
        Me.txtSeikyuCd.TabStopSetting = True
        Me.txtSeikyuCd.TextValue = ""
        Me.txtSeikyuCd.UseSystemPasswordChar = False
        Me.txtSeikyuCd.WidthDef = 82
        Me.txtSeikyuCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdInvDate
        '
        Me.imdInvDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInvDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdInvDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInvDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInvDate.HeightDef = 18
        Me.imdInvDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInvDate.HissuLabelVisible = True
        Me.imdInvDate.Holiday = True
        Me.imdInvDate.IsAfterDateCheck = False
        Me.imdInvDate.IsBeforeDateCheck = False
        Me.imdInvDate.IsHissuCheck = True
        Me.imdInvDate.IsMinDateCheck = "1900/01/01"
        Me.imdInvDate.ItemName = ""
        Me.imdInvDate.Location = New System.Drawing.Point(122, 191)
        Me.imdInvDate.Name = "imdInvDate"
        Me.imdInvDate.Number = CType(10101000000, Long)
        Me.imdInvDate.ReadOnly = False
        Me.imdInvDate.Size = New System.Drawing.Size(115, 18)
        Me.imdInvDate.TabIndex = 36
        Me.imdInvDate.TabStopSetting = True
        Me.imdInvDate.TextValue = ""
        Me.imdInvDate.Value = New Date(CType(0, Long))
        Me.imdInvDate.WidthDef = 115
        '
        'lblSikyuMeigi
        '
        Me.lblSikyuMeigi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSikyuMeigi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSikyuMeigi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSikyuMeigi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSikyuMeigi.CountWrappedLine = False
        Me.lblSikyuMeigi.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSikyuMeigi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSikyuMeigi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSikyuMeigi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSikyuMeigi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSikyuMeigi.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSikyuMeigi.HeightDef = 18
        Me.lblSikyuMeigi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSikyuMeigi.HissuLabelVisible = False
        Me.lblSikyuMeigi.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSikyuMeigi.IsByteCheck = 0
        Me.lblSikyuMeigi.IsCalendarCheck = False
        Me.lblSikyuMeigi.IsDakutenCheck = False
        Me.lblSikyuMeigi.IsEisuCheck = False
        Me.lblSikyuMeigi.IsForbiddenWordsCheck = False
        Me.lblSikyuMeigi.IsFullByteCheck = 0
        Me.lblSikyuMeigi.IsHankakuCheck = False
        Me.lblSikyuMeigi.IsHissuCheck = False
        Me.lblSikyuMeigi.IsKanaCheck = False
        Me.lblSikyuMeigi.IsMiddleSpace = False
        Me.lblSikyuMeigi.IsNumericCheck = False
        Me.lblSikyuMeigi.IsSujiCheck = False
        Me.lblSikyuMeigi.IsZenkakuCheck = False
        Me.lblSikyuMeigi.ItemName = ""
        Me.lblSikyuMeigi.LineSpace = 0
        Me.lblSikyuMeigi.Location = New System.Drawing.Point(122, 211)
        Me.lblSikyuMeigi.MaxLength = 0
        Me.lblSikyuMeigi.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSikyuMeigi.MaxLineCount = 0
        Me.lblSikyuMeigi.Multiline = False
        Me.lblSikyuMeigi.Name = "lblSikyuMeigi"
        Me.lblSikyuMeigi.ReadOnly = True
        Me.lblSikyuMeigi.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSikyuMeigi.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSikyuMeigi.Size = New System.Drawing.Size(389, 18)
        Me.lblSikyuMeigi.TabIndex = 27
        Me.lblSikyuMeigi.TabStop = False
        Me.lblSikyuMeigi.TabStopSetting = False
        Me.lblSikyuMeigi.TextValue = ""
        Me.lblSikyuMeigi.UseSystemPasswordChar = False
        Me.lblSikyuMeigi.WidthDef = 389
        Me.lblSikyuMeigi.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeikyuDate
        '
        Me.lblTitleSeikyuDate.AutoSize = True
        Me.lblTitleSeikyuDate.AutoSizeDef = True
        Me.lblTitleSeikyuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuDate.EnableStatus = False
        Me.lblTitleSeikyuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuDate.HeightDef = 13
        Me.lblTitleSeikyuDate.Location = New System.Drawing.Point(64, 194)
        Me.lblTitleSeikyuDate.Name = "lblTitleSeikyuDate"
        Me.lblTitleSeikyuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeikyuDate.TabIndex = 19
        Me.lblTitleSeikyuDate.Text = "請求日"
        Me.lblTitleSeikyuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuDate.TextValue = "請求日"
        Me.lblTitleSeikyuDate.WidthDef = 49
        '
        'lblCreateNm
        '
        Me.lblCreateNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCreateNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCreateNm.CountWrappedLine = False
        Me.lblCreateNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCreateNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCreateNm.HeightDef = 18
        Me.lblCreateNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateNm.HissuLabelVisible = False
        Me.lblCreateNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCreateNm.IsByteCheck = 20
        Me.lblCreateNm.IsCalendarCheck = False
        Me.lblCreateNm.IsDakutenCheck = False
        Me.lblCreateNm.IsEisuCheck = False
        Me.lblCreateNm.IsForbiddenWordsCheck = False
        Me.lblCreateNm.IsFullByteCheck = 0
        Me.lblCreateNm.IsHankakuCheck = False
        Me.lblCreateNm.IsHissuCheck = False
        Me.lblCreateNm.IsKanaCheck = False
        Me.lblCreateNm.IsMiddleSpace = False
        Me.lblCreateNm.IsNumericCheck = False
        Me.lblCreateNm.IsSujiCheck = False
        Me.lblCreateNm.IsZenkakuCheck = False
        Me.lblCreateNm.ItemName = ""
        Me.lblCreateNm.LineSpace = 0
        Me.lblCreateNm.Location = New System.Drawing.Point(354, 75)
        Me.lblCreateNm.MaxLength = 20
        Me.lblCreateNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCreateNm.MaxLineCount = 0
        Me.lblCreateNm.Multiline = False
        Me.lblCreateNm.Name = "lblCreateNm"
        Me.lblCreateNm.ReadOnly = True
        Me.lblCreateNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCreateNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCreateNm.Size = New System.Drawing.Size(165, 18)
        Me.lblCreateNm.TabIndex = 38
        Me.lblCreateNm.TabStop = False
        Me.lblCreateNm.TabStopSetting = False
        Me.lblCreateNm.TextValue = "Ｎ－－－２０－－－Ｎ"
        Me.lblCreateNm.UseSystemPasswordChar = False
        Me.lblCreateNm.WidthDef = 165
        Me.lblCreateNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCreateNm
        '
        Me.lblTitleCreateNm.AutoSize = True
        Me.lblTitleCreateNm.AutoSizeDef = True
        Me.lblTitleCreateNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCreateNm.EnableStatus = False
        Me.lblTitleCreateNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateNm.HeightDef = 13
        Me.lblTitleCreateNm.Location = New System.Drawing.Point(287, 77)
        Me.lblTitleCreateNm.Name = "lblTitleCreateNm"
        Me.lblTitleCreateNm.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleCreateNm.TabIndex = 37
        Me.lblTitleCreateNm.Text = "作成者名"
        Me.lblTitleCreateNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCreateNm.TextValue = "作成者名"
        Me.lblTitleCreateNm.WidthDef = 63
        '
        'chkHokan
        '
        Me.chkHokan.AutoSize = True
        Me.chkHokan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHokan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHokan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkHokan.EnableStatus = True
        Me.chkHokan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHokan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHokan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHokan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHokan.HeightDef = 17
        Me.chkHokan.Location = New System.Drawing.Point(121, 255)
        Me.chkHokan.Name = "chkHokan"
        Me.chkHokan.Size = New System.Drawing.Size(68, 17)
        Me.chkHokan.TabIndex = 3
        Me.chkHokan.TabStopSetting = True
        Me.chkHokan.Text = "保管料"
        Me.chkHokan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkHokan.TextValue = "保管料"
        Me.chkHokan.UseVisualStyleBackColor = True
        Me.chkHokan.WidthDef = 68
        '
        'chkYokomochi
        '
        Me.chkYokomochi.AutoSize = True
        Me.chkYokomochi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYokomochi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkYokomochi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkYokomochi.EnableStatus = True
        Me.chkYokomochi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYokomochi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkYokomochi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYokomochi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkYokomochi.HeightDef = 17
        Me.chkYokomochi.Location = New System.Drawing.Point(406, 255)
        Me.chkYokomochi.Name = "chkYokomochi"
        Me.chkYokomochi.Size = New System.Drawing.Size(68, 17)
        Me.chkYokomochi.TabIndex = 33
        Me.chkYokomochi.TabStopSetting = True
        Me.chkYokomochi.Text = "横持料"
        Me.chkYokomochi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkYokomochi.TextValue = "横持料"
        Me.chkYokomochi.UseVisualStyleBackColor = True
        Me.chkYokomochi.WidthDef = 68
        '
        'chkNiyaku
        '
        Me.chkNiyaku.AutoSize = True
        Me.chkNiyaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkNiyaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkNiyaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkNiyaku.EnableStatus = True
        Me.chkNiyaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkNiyaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkNiyaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkNiyaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkNiyaku.HeightDef = 17
        Me.chkNiyaku.Location = New System.Drawing.Point(195, 255)
        Me.chkNiyaku.Name = "chkNiyaku"
        Me.chkNiyaku.Size = New System.Drawing.Size(68, 17)
        Me.chkNiyaku.TabIndex = 30
        Me.chkNiyaku.TabStopSetting = True
        Me.chkNiyaku.Text = "荷役料"
        Me.chkNiyaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkNiyaku.TextValue = "荷役料"
        Me.chkNiyaku.UseVisualStyleBackColor = True
        Me.chkNiyaku.WidthDef = 68
        '
        'chkSagyou
        '
        Me.chkSagyou.AutoSize = True
        Me.chkSagyou.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSagyou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSagyou.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSagyou.EnableStatus = True
        Me.chkSagyou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSagyou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSagyou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSagyou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSagyou.HeightDef = 17
        Me.chkSagyou.Location = New System.Drawing.Point(332, 255)
        Me.chkSagyou.Name = "chkSagyou"
        Me.chkSagyou.Size = New System.Drawing.Size(68, 17)
        Me.chkSagyou.TabIndex = 32
        Me.chkSagyou.TabStopSetting = True
        Me.chkSagyou.Text = "作業料"
        Me.chkSagyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSagyou.TextValue = "作業料"
        Me.chkSagyou.UseVisualStyleBackColor = True
        Me.chkSagyou.WidthDef = 68
        '
        'chkUnchin
        '
        Me.chkUnchin.AutoSize = True
        Me.chkUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkUnchin.EnableStatus = True
        Me.chkUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkUnchin.HeightDef = 17
        Me.chkUnchin.Location = New System.Drawing.Point(269, 255)
        Me.chkUnchin.Name = "chkUnchin"
        Me.chkUnchin.Size = New System.Drawing.Size(54, 17)
        Me.chkUnchin.TabIndex = 31
        Me.chkUnchin.TabStopSetting = True
        Me.chkUnchin.Text = "運賃"
        Me.chkUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkUnchin.TextValue = "運賃"
        Me.chkUnchin.UseVisualStyleBackColor = True
        Me.chkUnchin.WidthDef = 54
        '
        'lblTitleSincyoku
        '
        Me.lblTitleSincyoku.AutoSize = True
        Me.lblTitleSincyoku.AutoSizeDef = True
        Me.lblTitleSincyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSincyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSincyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSincyoku.EnableStatus = False
        Me.lblTitleSincyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSincyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSincyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSincyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSincyoku.HeightDef = 13
        Me.lblTitleSincyoku.Location = New System.Drawing.Point(287, 57)
        Me.lblTitleSincyoku.Name = "lblTitleSincyoku"
        Me.lblTitleSincyoku.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSincyoku.TabIndex = 28
        Me.lblTitleSincyoku.Text = "進捗区分"
        Me.lblTitleSincyoku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSincyoku.TextValue = "進捗区分"
        Me.lblTitleSincyoku.WidthDef = 63
        '
        'lblTitleSeikyuNo
        '
        Me.lblTitleSeikyuNo.AutoSize = True
        Me.lblTitleSeikyuNo.AutoSizeDef = True
        Me.lblTitleSeikyuNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuNo.EnableStatus = False
        Me.lblTitleSeikyuNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuNo.HeightDef = 13
        Me.lblTitleSeikyuNo.Location = New System.Drawing.Point(38, 57)
        Me.lblTitleSeikyuNo.Name = "lblTitleSeikyuNo"
        Me.lblTitleSeikyuNo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSeikyuNo.TabIndex = 29
        Me.lblTitleSeikyuNo.Text = "請求書番号"
        Me.lblTitleSeikyuNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuNo.TextValue = "請求書番号"
        Me.lblTitleSeikyuNo.WidthDef = 77
        '
        'lblSeikyuNo
        '
        Me.lblSeikyuNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeikyuNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeikyuNo.CountWrappedLine = False
        Me.lblSeikyuNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeikyuNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeikyuNo.HeightDef = 18
        Me.lblSeikyuNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNo.HissuLabelVisible = False
        Me.lblSeikyuNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeikyuNo.IsByteCheck = 0
        Me.lblSeikyuNo.IsCalendarCheck = False
        Me.lblSeikyuNo.IsDakutenCheck = False
        Me.lblSeikyuNo.IsEisuCheck = False
        Me.lblSeikyuNo.IsForbiddenWordsCheck = False
        Me.lblSeikyuNo.IsFullByteCheck = 0
        Me.lblSeikyuNo.IsHankakuCheck = False
        Me.lblSeikyuNo.IsHissuCheck = False
        Me.lblSeikyuNo.IsKanaCheck = False
        Me.lblSeikyuNo.IsMiddleSpace = False
        Me.lblSeikyuNo.IsNumericCheck = False
        Me.lblSeikyuNo.IsSujiCheck = False
        Me.lblSeikyuNo.IsZenkakuCheck = False
        Me.lblSeikyuNo.ItemName = ""
        Me.lblSeikyuNo.LineSpace = 0
        Me.lblSeikyuNo.Location = New System.Drawing.Point(121, 54)
        Me.lblSeikyuNo.MaxLength = 0
        Me.lblSeikyuNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeikyuNo.MaxLineCount = 0
        Me.lblSeikyuNo.Multiline = False
        Me.lblSeikyuNo.Name = "lblSeikyuNo"
        Me.lblSeikyuNo.ReadOnly = True
        Me.lblSeikyuNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeikyuNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeikyuNo.Size = New System.Drawing.Size(165, 18)
        Me.lblSeikyuNo.TabIndex = 31
        Me.lblSeikyuNo.TabStop = False
        Me.lblSeikyuNo.TabStopSetting = False
        Me.lblSeikyuNo.TextValue = ""
        Me.lblSeikyuNo.UseSystemPasswordChar = False
        Me.lblSeikyuNo.WidthDef = 165
        Me.lblSeikyuNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTorikomi
        '
        Me.lblTitleTorikomi.AutoSize = True
        Me.lblTitleTorikomi.AutoSizeDef = True
        Me.lblTitleTorikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTorikomi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTorikomi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTorikomi.EnableStatus = False
        Me.lblTitleTorikomi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTorikomi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTorikomi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTorikomi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTorikomi.HeightDef = 13
        Me.lblTitleTorikomi.Location = New System.Drawing.Point(52, 256)
        Me.lblTitleTorikomi.Name = "lblTitleTorikomi"
        Me.lblTitleTorikomi.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTorikomi.TabIndex = 74
        Me.lblTitleTorikomi.Text = "取込項目"
        Me.lblTitleTorikomi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTorikomi.TextValue = "取込項目"
        Me.lblTitleTorikomi.WidthDef = 63
        '
        'sprSeikyuM
        '
        Me.sprSeikyuM.AccessibleDescription = ""
        Me.sprSeikyuM.AllowUserZoom = False
        Me.sprSeikyuM.AutoImeMode = False
        Me.sprSeikyuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprSeikyuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprSeikyuM.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprSeikyuM.CellClickEventArgs = Nothing
        Me.sprSeikyuM.CheckToCheckBox = True
        Me.sprSeikyuM.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprSeikyuM.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprSeikyuM.EditModeReplace = True
        Me.sprSeikyuM.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprSeikyuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM.ForeColorDef = System.Drawing.Color.Empty
        Me.sprSeikyuM.HeightDef = 500
        Me.sprSeikyuM.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprSeikyuM.HorizontalScrollBar.Name = ""
        Me.sprSeikyuM.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprSeikyuM.KeyboardCheckBoxOn = False
        Me.sprSeikyuM.Location = New System.Drawing.Point(12, 300)
        Me.sprSeikyuM.Name = "sprSeikyuM"
        Me.sprSeikyuM.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprSeikyuM.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprSeikyuM_Sheet1})
        Me.sprSeikyuM.Size = New System.Drawing.Size(1255, 500)
        Me.sprSeikyuM.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprSeikyuM.SortColumn = True
        Me.sprSeikyuM.SpanColumnLock = True
        Me.sprSeikyuM.SpreadDoubleClicked = False
        Me.sprSeikyuM.TabIndex = 75
        Me.sprSeikyuM.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprSeikyuM.TextValue = Nothing
        Me.sprSeikyuM.UseGrouping = False
        Me.sprSeikyuM.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprSeikyuM.VerticalScrollBar.Name = ""
        Me.sprSeikyuM.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprSeikyuM.WidthDef = 1255
        sprSeikyuM_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprSeikyuM_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprSeikyuM.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprSeikyuM_InputMapWhenFocusedNormal)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprSeikyuM_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprSeikyuM.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprSeikyuM_InputMapWhenAncestorOfFocusedNormal)
        '
        'sprSeikyuM_Sheet1
        '
        Me.sprSeikyuM_Sheet1.Reset()
        Me.sprSeikyuM_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprSeikyuM_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprSeikyuM_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprSeikyuM_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprSeikyuM_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprSeikyuM_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprSeikyuM_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprSeikyuM_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSeikyuM_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprSeikyuM_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprSeikyuM_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprSeikyuM_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.sprSeikyuM_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.DefaultStyle.Locked = False
        Me.sprSeikyuM_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprSeikyuM_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprSeikyuM_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprSeikyuM_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprSeikyuM_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprSeikyuM_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprSeikyuM_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprSeikyuM_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprSeikyuM_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprSeikyuM_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprSeikyuM_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprSeikyuM_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprSeikyuM_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.Rows.Default.Height = 18.0!
        Me.sprSeikyuM_Sheet1.Rows.Default.Resizable = False
        Me.sprSeikyuM_Sheet1.Rows.Default.Visible = True
        Me.sprSeikyuM_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprSeikyuM_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprSeikyuM_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.Locked = True
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprSeikyuM_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprSeikyuM_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprSeikyuM_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnPrint.EnableStatus = True
        Me.btnPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.HeightDef = 22
        Me.btnPrint.Location = New System.Drawing.Point(987, 28)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 106
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBr.DataSource = Nothing
        Me.cmbBr.DisplayMember = Nothing
        Me.cmbBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.HeightDef = 18
        Me.cmbBr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.HissuLabelVisible = True
        Me.cmbBr.InsertWildCard = True
        Me.cmbBr.IsForbiddenWordsCheck = False
        Me.cmbBr.IsHissuCheck = True
        Me.cmbBr.ItemName = ""
        Me.cmbBr.Location = New System.Drawing.Point(121, 14)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = True
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 130
        Me.cmbBr.TabStop = False
        Me.cmbBr.TabStopSetting = False
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
        '
        'lblTitleEigyo
        '
        Me.lblTitleEigyo.AutoSize = True
        Me.lblTitleEigyo.AutoSizeDef = True
        Me.lblTitleEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo.EnableStatus = False
        Me.lblTitleEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.HeightDef = 13
        Me.lblTitleEigyo.Location = New System.Drawing.Point(66, 16)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 129
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblTitleSeiqShubetsu
        '
        Me.lblTitleSeiqShubetsu.AutoSize = True
        Me.lblTitleSeiqShubetsu.AutoSizeDef = True
        Me.lblTitleSeiqShubetsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqShubetsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqShubetsu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqShubetsu.EnableStatus = False
        Me.lblTitleSeiqShubetsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqShubetsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqShubetsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqShubetsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqShubetsu.HeightDef = 13
        Me.lblTitleSeiqShubetsu.Location = New System.Drawing.Point(52, 36)
        Me.lblTitleSeiqShubetsu.Name = "lblTitleSeiqShubetsu"
        Me.lblTitleSeiqShubetsu.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSeiqShubetsu.TabIndex = 131
        Me.lblTitleSeiqShubetsu.Text = "請求種別"
        Me.lblTitleSeiqShubetsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqShubetsu.TextValue = "請求種別"
        Me.lblTitleSeiqShubetsu.WidthDef = 63
        '
        'cmbSeiqtShubetu
        '
        Me.cmbSeiqtShubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqtShubetu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqtShubetu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSeiqtShubetu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSeiqtShubetu.DataCode = "K019"
        Me.cmbSeiqtShubetu.DataSource = Nothing
        Me.cmbSeiqtShubetu.DisplayMember = Nothing
        Me.cmbSeiqtShubetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSeiqtShubetu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSeiqtShubetu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSeiqtShubetu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSeiqtShubetu.HeightDef = 18
        Me.cmbSeiqtShubetu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSeiqtShubetu.HissuLabelVisible = False
        Me.cmbSeiqtShubetu.InsertWildCard = True
        Me.cmbSeiqtShubetu.IsForbiddenWordsCheck = False
        Me.cmbSeiqtShubetu.IsHissuCheck = False
        Me.cmbSeiqtShubetu.ItemName = ""
        Me.cmbSeiqtShubetu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSeiqtShubetu.Location = New System.Drawing.Point(121, 34)
        Me.cmbSeiqtShubetu.Name = "cmbSeiqtShubetu"
        Me.cmbSeiqtShubetu.ReadOnly = True
        Me.cmbSeiqtShubetu.SelectedIndex = -1
        Me.cmbSeiqtShubetu.SelectedItem = Nothing
        Me.cmbSeiqtShubetu.SelectedText = ""
        Me.cmbSeiqtShubetu.SelectedValue = ""
        Me.cmbSeiqtShubetu.Size = New System.Drawing.Size(165, 18)
        Me.cmbSeiqtShubetu.TabIndex = 132
        Me.cmbSeiqtShubetu.TabStop = False
        Me.cmbSeiqtShubetu.TabStopSetting = False
        Me.cmbSeiqtShubetu.TextValue = ""
        Me.cmbSeiqtShubetu.Value1 = Nothing
        Me.cmbSeiqtShubetu.Value2 = Nothing
        Me.cmbSeiqtShubetu.Value3 = Nothing
        Me.cmbSeiqtShubetu.ValueMember = Nothing
        Me.cmbSeiqtShubetu.WidthDef = 165
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1143, 17)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(115, 18)
        Me.lblSituation.TabIndex = 133
        Me.lblSituation.TabStop = False
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnAdd.EnableStatus = True
        Me.btnAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.HeightDef = 22
        Me.btnAdd.Location = New System.Drawing.Point(13, 273)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnAdd.TabIndex = 134
        Me.btnAdd.TabStopSetting = True
        Me.btnAdd.Text = "行追加"
        Me.btnAdd.TextValue = "行追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.WidthDef = 70
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnDel.EnableStatus = True
        Me.btnDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.HeightDef = 22
        Me.btnDel.Location = New System.Drawing.Point(89, 273)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(70, 22)
        Me.btnDel.TabIndex = 135
        Me.btnDel.TabStopSetting = True
        Me.btnDel.Text = "行削除"
        Me.btnDel.TextValue = "行削除"
        Me.btnDel.UseVisualStyleBackColor = True
        Me.btnDel.WidthDef = 70
        '
        'numCalAllK
        '
        Me.numCalAllK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCalAllK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCalAllK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllK.HeightDef = 18
        Me.numCalAllK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllK.HissuLabelVisible = False
        Me.numCalAllK.IsHissuCheck = False
        Me.numCalAllK.IsRangeCheck = False
        Me.numCalAllK.ItemName = ""
        Me.numCalAllK.Location = New System.Drawing.Point(773, 82)
        Me.numCalAllK.Name = "numCalAllK"
        Me.numCalAllK.ReadOnly = True
        Me.numCalAllK.Size = New System.Drawing.Size(137, 18)
        Me.numCalAllK.TabIndex = 147
        Me.numCalAllK.TabStop = False
        Me.numCalAllK.TabStopSetting = False
        Me.numCalAllK.TextValue = "0"
        Me.numCalAllK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCalAllK.WidthDef = 137
        '
        'numCalAllU
        '
        Me.numCalAllU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCalAllU.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCalAllU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllU.HeightDef = 18
        Me.numCalAllU.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllU.HissuLabelVisible = False
        Me.numCalAllU.IsHissuCheck = False
        Me.numCalAllU.IsRangeCheck = False
        Me.numCalAllU.ItemName = ""
        Me.numCalAllU.Location = New System.Drawing.Point(1136, 82)
        Me.numCalAllU.Name = "numCalAllU"
        Me.numCalAllU.ReadOnly = True
        Me.numCalAllU.Size = New System.Drawing.Size(137, 18)
        Me.numCalAllU.TabIndex = 148
        Me.numCalAllU.TabStop = False
        Me.numCalAllU.TabStopSetting = False
        Me.numCalAllU.TextValue = "0"
        Me.numCalAllU.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCalAllU.WidthDef = 137
        '
        'numCalAllH
        '
        Me.numCalAllH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCalAllH.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCalAllH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllH.HeightDef = 18
        Me.numCalAllH.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllH.HissuLabelVisible = False
        Me.numCalAllH.IsHissuCheck = False
        Me.numCalAllH.IsRangeCheck = False
        Me.numCalAllH.ItemName = ""
        Me.numCalAllH.Location = New System.Drawing.Point(1015, 82)
        Me.numCalAllH.Name = "numCalAllH"
        Me.numCalAllH.ReadOnly = True
        Me.numCalAllH.Size = New System.Drawing.Size(137, 18)
        Me.numCalAllH.TabIndex = 149
        Me.numCalAllH.TabStop = False
        Me.numCalAllH.TabStopSetting = False
        Me.numCalAllH.TextValue = "0"
        Me.numCalAllH.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCalAllH.WidthDef = 137
        '
        'numCalAllM
        '
        Me.numCalAllM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numCalAllM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numCalAllM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numCalAllM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numCalAllM.HeightDef = 18
        Me.numCalAllM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numCalAllM.HissuLabelVisible = False
        Me.numCalAllM.IsHissuCheck = False
        Me.numCalAllM.IsRangeCheck = False
        Me.numCalAllM.ItemName = ""
        Me.numCalAllM.Location = New System.Drawing.Point(894, 82)
        Me.numCalAllM.Name = "numCalAllM"
        Me.numCalAllM.ReadOnly = True
        Me.numCalAllM.Size = New System.Drawing.Size(137, 18)
        Me.numCalAllM.TabIndex = 150
        Me.numCalAllM.TabStop = False
        Me.numCalAllM.TabStopSetting = False
        Me.numCalAllM.TextValue = "0"
        Me.numCalAllM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numCalAllM.WidthDef = 137
        '
        'numNebikiRateM
        '
        Me.numNebikiRateM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRateM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRateM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiRateM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiRateM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRateM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRateM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRateM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRateM.HeightDef = 18
        Me.numNebikiRateM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiRateM.HissuLabelVisible = False
        Me.numNebikiRateM.IsHissuCheck = False
        Me.numNebikiRateM.IsRangeCheck = False
        Me.numNebikiRateM.ItemName = ""
        Me.numNebikiRateM.Location = New System.Drawing.Point(894, 99)
        Me.numNebikiRateM.Name = "numNebikiRateM"
        Me.numNebikiRateM.ReadOnly = False
        Me.numNebikiRateM.Size = New System.Drawing.Size(137, 18)
        Me.numNebikiRateM.TabIndex = 151
        Me.numNebikiRateM.TabStopSetting = True
        Me.numNebikiRateM.TextValue = "0"
        Me.numNebikiRateM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiRateM.WidthDef = 137
        '
        'numNebikiRateK
        '
        Me.numNebikiRateK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRateK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiRateK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiRateK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiRateK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRateK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiRateK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRateK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiRateK.HeightDef = 18
        Me.numNebikiRateK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiRateK.HissuLabelVisible = False
        Me.numNebikiRateK.IsHissuCheck = False
        Me.numNebikiRateK.IsRangeCheck = False
        Me.numNebikiRateK.ItemName = ""
        Me.numNebikiRateK.Location = New System.Drawing.Point(773, 99)
        Me.numNebikiRateK.Name = "numNebikiRateK"
        Me.numNebikiRateK.ReadOnly = False
        Me.numNebikiRateK.Size = New System.Drawing.Size(137, 18)
        Me.numNebikiRateK.TabIndex = 152
        Me.numNebikiRateK.TabStopSetting = True
        Me.numNebikiRateK.TextValue = "0"
        Me.numNebikiRateK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiRateK.WidthDef = 137
        '
        'numRateNebikigakuM
        '
        Me.numRateNebikigakuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numRateNebikigakuM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numRateNebikigakuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRateNebikigakuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRateNebikigakuM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRateNebikigakuM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRateNebikigakuM.HeightDef = 18
        Me.numRateNebikigakuM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuM.HissuLabelVisible = False
        Me.numRateNebikigakuM.IsHissuCheck = False
        Me.numRateNebikigakuM.IsRangeCheck = False
        Me.numRateNebikigakuM.ItemName = ""
        Me.numRateNebikigakuM.Location = New System.Drawing.Point(894, 116)
        Me.numRateNebikigakuM.Name = "numRateNebikigakuM"
        Me.numRateNebikigakuM.ReadOnly = True
        Me.numRateNebikigakuM.Size = New System.Drawing.Size(137, 18)
        Me.numRateNebikigakuM.TabIndex = 153
        Me.numRateNebikigakuM.TabStop = False
        Me.numRateNebikigakuM.TabStopSetting = False
        Me.numRateNebikigakuM.TextValue = "0"
        Me.numRateNebikigakuM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numRateNebikigakuM.WidthDef = 137
        '
        'numRateNebikigakuK
        '
        Me.numRateNebikigakuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numRateNebikigakuK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numRateNebikigakuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRateNebikigakuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRateNebikigakuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRateNebikigakuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRateNebikigakuK.HeightDef = 18
        Me.numRateNebikigakuK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRateNebikigakuK.HissuLabelVisible = False
        Me.numRateNebikigakuK.IsHissuCheck = False
        Me.numRateNebikigakuK.IsRangeCheck = False
        Me.numRateNebikigakuK.ItemName = ""
        Me.numRateNebikigakuK.Location = New System.Drawing.Point(773, 116)
        Me.numRateNebikigakuK.Name = "numRateNebikigakuK"
        Me.numRateNebikigakuK.ReadOnly = True
        Me.numRateNebikigakuK.Size = New System.Drawing.Size(137, 18)
        Me.numRateNebikigakuK.TabIndex = 154
        Me.numRateNebikigakuK.TabStop = False
        Me.numRateNebikigakuK.TabStopSetting = False
        Me.numRateNebikigakuK.TextValue = "0"
        Me.numRateNebikigakuK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numRateNebikigakuK.WidthDef = 137
        '
        'numNebikiGakuM
        '
        Me.numNebikiGakuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGakuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGakuM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiGakuM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiGakuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGakuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGakuM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGakuM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGakuM.HeightDef = 18
        Me.numNebikiGakuM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiGakuM.HissuLabelVisible = False
        Me.numNebikiGakuM.IsHissuCheck = False
        Me.numNebikiGakuM.IsRangeCheck = False
        Me.numNebikiGakuM.ItemName = ""
        Me.numNebikiGakuM.Location = New System.Drawing.Point(894, 133)
        Me.numNebikiGakuM.Name = "numNebikiGakuM"
        Me.numNebikiGakuM.ReadOnly = False
        Me.numNebikiGakuM.Size = New System.Drawing.Size(137, 18)
        Me.numNebikiGakuM.TabIndex = 155
        Me.numNebikiGakuM.TabStopSetting = True
        Me.numNebikiGakuM.TextValue = "0"
        Me.numNebikiGakuM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiGakuM.WidthDef = 137
        '
        'numNebikiGakuK
        '
        Me.numNebikiGakuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGakuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNebikiGakuK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNebikiGakuK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNebikiGakuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGakuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNebikiGakuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGakuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNebikiGakuK.HeightDef = 18
        Me.numNebikiGakuK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNebikiGakuK.HissuLabelVisible = False
        Me.numNebikiGakuK.IsHissuCheck = False
        Me.numNebikiGakuK.IsRangeCheck = False
        Me.numNebikiGakuK.ItemName = ""
        Me.numNebikiGakuK.Location = New System.Drawing.Point(773, 133)
        Me.numNebikiGakuK.Name = "numNebikiGakuK"
        Me.numNebikiGakuK.ReadOnly = False
        Me.numNebikiGakuK.Size = New System.Drawing.Size(137, 18)
        Me.numNebikiGakuK.TabIndex = 156
        Me.numNebikiGakuK.TabStopSetting = True
        Me.numNebikiGakuK.TextValue = "0"
        Me.numNebikiGakuK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNebikiGakuK.WidthDef = 137
        '
        'numZeigakuK
        '
        Me.numZeigakuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numZeigakuK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numZeigakuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeigakuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeigakuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeigakuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeigakuK.HeightDef = 18
        Me.numZeigakuK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuK.HissuLabelVisible = False
        Me.numZeigakuK.IsHissuCheck = False
        Me.numZeigakuK.IsRangeCheck = False
        Me.numZeigakuK.ItemName = ""
        Me.numZeigakuK.Location = New System.Drawing.Point(773, 150)
        Me.numZeigakuK.Name = "numZeigakuK"
        Me.numZeigakuK.ReadOnly = True
        Me.numZeigakuK.Size = New System.Drawing.Size(137, 18)
        Me.numZeigakuK.TabIndex = 157
        Me.numZeigakuK.TabStop = False
        Me.numZeigakuK.TabStopSetting = False
        Me.numZeigakuK.TextValue = "0"
        Me.numZeigakuK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numZeigakuK.WidthDef = 137
        '
        'numZeigakuU
        '
        Me.numZeigakuU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numZeigakuU.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numZeigakuU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeigakuU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeigakuU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeigakuU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeigakuU.HeightDef = 18
        Me.numZeigakuU.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeigakuU.HissuLabelVisible = False
        Me.numZeigakuU.IsHissuCheck = False
        Me.numZeigakuU.IsRangeCheck = False
        Me.numZeigakuU.ItemName = ""
        Me.numZeigakuU.Location = New System.Drawing.Point(1136, 150)
        Me.numZeigakuU.Name = "numZeigakuU"
        Me.numZeigakuU.ReadOnly = True
        Me.numZeigakuU.Size = New System.Drawing.Size(137, 18)
        Me.numZeigakuU.TabIndex = 158
        Me.numZeigakuU.TabStop = False
        Me.numZeigakuU.TabStopSetting = False
        Me.numZeigakuU.TextValue = "0"
        Me.numZeigakuU.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numZeigakuU.WidthDef = 137
        '
        'numSeikyuGakuU
        '
        Me.numSeikyuGakuU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeikyuGakuU.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeikyuGakuU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuU.HeightDef = 18
        Me.numSeikyuGakuU.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuU.HissuLabelVisible = False
        Me.numSeikyuGakuU.IsHissuCheck = False
        Me.numSeikyuGakuU.IsRangeCheck = False
        Me.numSeikyuGakuU.ItemName = ""
        Me.numSeikyuGakuU.Location = New System.Drawing.Point(1136, 184)
        Me.numSeikyuGakuU.Name = "numSeikyuGakuU"
        Me.numSeikyuGakuU.ReadOnly = True
        Me.numSeikyuGakuU.Size = New System.Drawing.Size(137, 18)
        Me.numSeikyuGakuU.TabIndex = 159
        Me.numSeikyuGakuU.TabStop = False
        Me.numSeikyuGakuU.TabStopSetting = False
        Me.numSeikyuGakuU.TextValue = "0"
        Me.numSeikyuGakuU.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeikyuGakuU.WidthDef = 137
        '
        'numSeikyuGakuH
        '
        Me.numSeikyuGakuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeikyuGakuH.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeikyuGakuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuH.HeightDef = 18
        Me.numSeikyuGakuH.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuH.HissuLabelVisible = False
        Me.numSeikyuGakuH.IsHissuCheck = False
        Me.numSeikyuGakuH.IsRangeCheck = False
        Me.numSeikyuGakuH.ItemName = ""
        Me.numSeikyuGakuH.Location = New System.Drawing.Point(1015, 184)
        Me.numSeikyuGakuH.Name = "numSeikyuGakuH"
        Me.numSeikyuGakuH.ReadOnly = True
        Me.numSeikyuGakuH.Size = New System.Drawing.Size(137, 18)
        Me.numSeikyuGakuH.TabIndex = 160
        Me.numSeikyuGakuH.TabStop = False
        Me.numSeikyuGakuH.TabStopSetting = False
        Me.numSeikyuGakuH.TextValue = "0"
        Me.numSeikyuGakuH.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeikyuGakuH.WidthDef = 137
        '
        'numSeikyuGakuM
        '
        Me.numSeikyuGakuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeikyuGakuM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeikyuGakuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuM.HeightDef = 18
        Me.numSeikyuGakuM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuM.HissuLabelVisible = False
        Me.numSeikyuGakuM.IsHissuCheck = False
        Me.numSeikyuGakuM.IsRangeCheck = False
        Me.numSeikyuGakuM.ItemName = ""
        Me.numSeikyuGakuM.Location = New System.Drawing.Point(894, 184)
        Me.numSeikyuGakuM.Name = "numSeikyuGakuM"
        Me.numSeikyuGakuM.ReadOnly = True
        Me.numSeikyuGakuM.Size = New System.Drawing.Size(137, 18)
        Me.numSeikyuGakuM.TabIndex = 161
        Me.numSeikyuGakuM.TabStop = False
        Me.numSeikyuGakuM.TabStopSetting = False
        Me.numSeikyuGakuM.TextValue = "0"
        Me.numSeikyuGakuM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeikyuGakuM.WidthDef = 137
        '
        'numSeikyuGakuK
        '
        Me.numSeikyuGakuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeikyuGakuK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeikyuGakuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuGakuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuGakuK.HeightDef = 18
        Me.numSeikyuGakuK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuGakuK.HissuLabelVisible = False
        Me.numSeikyuGakuK.IsHissuCheck = False
        Me.numSeikyuGakuK.IsRangeCheck = False
        Me.numSeikyuGakuK.ItemName = ""
        Me.numSeikyuGakuK.Location = New System.Drawing.Point(773, 184)
        Me.numSeikyuGakuK.Name = "numSeikyuGakuK"
        Me.numSeikyuGakuK.ReadOnly = True
        Me.numSeikyuGakuK.Size = New System.Drawing.Size(137, 18)
        Me.numSeikyuGakuK.TabIndex = 162
        Me.numSeikyuGakuK.TabStop = False
        Me.numSeikyuGakuK.TabStopSetting = False
        Me.numSeikyuGakuK.TextValue = "0"
        Me.numSeikyuGakuK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeikyuGakuK.WidthDef = 137
        '
        'numZeiHasuK
        '
        Me.numZeiHasuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numZeiHasuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numZeiHasuK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numZeiHasuK.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numZeiHasuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeiHasuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numZeiHasuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeiHasuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numZeiHasuK.HeightDef = 18
        Me.numZeiHasuK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numZeiHasuK.HissuLabelVisible = False
        Me.numZeiHasuK.IsHissuCheck = False
        Me.numZeiHasuK.IsRangeCheck = False
        Me.numZeiHasuK.ItemName = ""
        Me.numZeiHasuK.Location = New System.Drawing.Point(773, 167)
        Me.numZeiHasuK.Name = "numZeiHasuK"
        Me.numZeiHasuK.ReadOnly = False
        Me.numZeiHasuK.Size = New System.Drawing.Size(137, 18)
        Me.numZeiHasuK.TabIndex = 163
        Me.numZeiHasuK.TabStopSetting = True
        Me.numZeiHasuK.TextValue = "0"
        Me.numZeiHasuK.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numZeiHasuK.WidthDef = 137
        '
        'numSeikyuAll
        '
        Me.numSeikyuAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuAll.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuAll.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSeikyuAll.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSeikyuAll.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuAll.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSeikyuAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuAll.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSeikyuAll.HeightDef = 28
        Me.numSeikyuAll.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSeikyuAll.HissuLabelVisible = False
        Me.numSeikyuAll.IsHissuCheck = False
        Me.numSeikyuAll.IsRangeCheck = False
        Me.numSeikyuAll.ItemName = ""
        Me.numSeikyuAll.Location = New System.Drawing.Point(1136, 201)
        Me.numSeikyuAll.Name = "numSeikyuAll"
        Me.numSeikyuAll.ReadOnly = True
        Me.numSeikyuAll.Size = New System.Drawing.Size(137, 28)
        Me.numSeikyuAll.TabIndex = 164
        Me.numSeikyuAll.TabStop = False
        Me.numSeikyuAll.TabStopSetting = False
        Me.numSeikyuAll.TextValue = "0"
        Me.numSeikyuAll.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSeikyuAll.WidthDef = 137
        '
        'lblTitleCalAllK
        '
        Me.lblTitleCalAllK.AutoSizeDef = False
        Me.lblTitleCalAllK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCalAllK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCalAllK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleCalAllK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleCalAllK.EnableStatus = False
        Me.lblTitleCalAllK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCalAllK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCalAllK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCalAllK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCalAllK.HeightDef = 18
        Me.lblTitleCalAllK.Location = New System.Drawing.Point(533, 82)
        Me.lblTitleCalAllK.Name = "lblTitleCalAllK"
        Me.lblTitleCalAllK.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleCalAllK.TabIndex = 165
        Me.lblTitleCalAllK.Text = "計算総額 ： a"
        Me.lblTitleCalAllK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleCalAllK.TextValue = "計算総額 ： a"
        Me.lblTitleCalAllK.WidthDef = 241
        '
        'lblTitleSeikyuGaku
        '
        Me.lblTitleSeikyuGaku.AutoSizeDef = False
        Me.lblTitleSeikyuGaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuGaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleSeikyuGaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleSeikyuGaku.EnableStatus = False
        Me.lblTitleSeikyuGaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuGaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuGaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuGaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuGaku.HeightDef = 18
        Me.lblTitleSeikyuGaku.Location = New System.Drawing.Point(533, 184)
        Me.lblTitleSeikyuGaku.Name = "lblTitleSeikyuGaku"
        Me.lblTitleSeikyuGaku.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleSeikyuGaku.TabIndex = 167
        Me.lblTitleSeikyuGaku.Text = "請求額(a - c - d + e + f) ： g"
        Me.lblTitleSeikyuGaku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleSeikyuGaku.TextValue = "請求額(a - c - d + e + f) ： g"
        Me.lblTitleSeikyuGaku.WidthDef = 241
        '
        'lblTitleZeiHasu
        '
        Me.lblTitleZeiHasu.AutoSizeDef = False
        Me.lblTitleZeiHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZeiHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZeiHasu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleZeiHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleZeiHasu.EnableStatus = False
        Me.lblTitleZeiHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZeiHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZeiHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZeiHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZeiHasu.HeightDef = 18
        Me.lblTitleZeiHasu.Location = New System.Drawing.Point(533, 167)
        Me.lblTitleZeiHasu.Name = "lblTitleZeiHasu"
        Me.lblTitleZeiHasu.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleZeiHasu.TabIndex = 169
        Me.lblTitleZeiHasu.Text = "税端数処理 ： f"
        Me.lblTitleZeiHasu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleZeiHasu.TextValue = "税端数処理 ： f"
        Me.lblTitleZeiHasu.WidthDef = 241
        '
        'lblTitleZeigaku
        '
        Me.lblTitleZeigaku.AutoSizeDef = False
        Me.lblTitleZeigaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZeigaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZeigaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleZeigaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleZeigaku.EnableStatus = False
        Me.lblTitleZeigaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZeigaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZeigaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZeigaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZeigaku.HeightDef = 18
        Me.lblTitleZeigaku.Location = New System.Drawing.Point(533, 150)
        Me.lblTitleZeigaku.Name = "lblTitleZeigaku"
        Me.lblTitleZeigaku.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleZeigaku.TabIndex = 171
        Me.lblTitleZeigaku.Text = "税額 ： e"
        Me.lblTitleZeigaku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleZeigaku.TextValue = "税額 ： e"
        Me.lblTitleZeigaku.WidthDef = 241
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSizeDef = False
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 18
        Me.LmTitleLabel4.Location = New System.Drawing.Point(533, 133)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(241, 18)
        Me.LmTitleLabel4.TabIndex = 173
        Me.LmTitleLabel4.Text = "全体値引額 ： d"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel4.TextValue = "全体値引額 ： d"
        Me.LmTitleLabel4.WidthDef = 241
        '
        'lblTitleRateNebikigaku
        '
        Me.lblTitleRateNebikigaku.AutoSizeDef = False
        Me.lblTitleRateNebikigaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRateNebikigaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRateNebikigaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleRateNebikigaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleRateNebikigaku.EnableStatus = False
        Me.lblTitleRateNebikigaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRateNebikigaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRateNebikigaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRateNebikigaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRateNebikigaku.HeightDef = 18
        Me.lblTitleRateNebikigaku.Location = New System.Drawing.Point(533, 116)
        Me.lblTitleRateNebikigaku.Name = "lblTitleRateNebikigaku"
        Me.lblTitleRateNebikigaku.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleRateNebikigaku.TabIndex = 175
        Me.lblTitleRateNebikigaku.Text = "全体値引率による値引額(a * b) ：c"
        Me.lblTitleRateNebikigaku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleRateNebikigaku.TextValue = "全体値引率による値引額(a * b) ：c"
        Me.lblTitleRateNebikigaku.WidthDef = 241
        '
        'lblTitleNebikiGaku
        '
        Me.lblTitleNebikiGaku.AutoSizeDef = False
        Me.lblTitleNebikiGaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiGaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNebikiGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleNebikiGaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleNebikiGaku.EnableStatus = False
        Me.lblTitleNebikiGaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiGaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNebikiGaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiGaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNebikiGaku.HeightDef = 18
        Me.lblTitleNebikiGaku.Location = New System.Drawing.Point(533, 99)
        Me.lblTitleNebikiGaku.Name = "lblTitleNebikiGaku"
        Me.lblTitleNebikiGaku.Size = New System.Drawing.Size(241, 18)
        Me.lblTitleNebikiGaku.TabIndex = 177
        Me.lblTitleNebikiGaku.Text = "全体値引率(%) ： b"
        Me.lblTitleNebikiGaku.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTitleNebikiGaku.TextValue = "全体値引率(%) ： b"
        Me.lblTitleNebikiGaku.WidthDef = 241
        '
        'lblTitleSeikyuAll
        '
        Me.lblTitleSeikyuAll.AutoSizeDef = False
        Me.lblTitleSeikyuAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuAll.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleSeikyuAll.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleSeikyuAll.EnableStatus = False
        Me.lblTitleSeikyuAll.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuAll.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuAll.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuAll.HeightDef = 28
        Me.lblTitleSeikyuAll.Location = New System.Drawing.Point(533, 201)
        Me.lblTitleSeikyuAll.Name = "lblTitleSeikyuAll"
        Me.lblTitleSeikyuAll.Size = New System.Drawing.Size(604, 28)
        Me.lblTitleSeikyuAll.TabIndex = 178
        Me.lblTitleSeikyuAll.Text = "請求書総額　　"
        Me.lblTitleSeikyuAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuAll.TextValue = "請求書総額　　"
        Me.lblTitleSeikyuAll.WidthDef = 604
        '
        'LmTitleLabel1
        '
        Me.LmTitleLabel1.AutoSizeDef = False
        Me.LmTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmTitleLabel1.EnableStatus = False
        Me.LmTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.HeightDef = 18
        Me.LmTitleLabel1.Location = New System.Drawing.Point(533, 65)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(241, 18)
        Me.LmTitleLabel1.TabIndex = 179
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel1.TextValue = ""
        Me.LmTitleLabel1.WidthDef = 241
        '
        'lblTitleKazei
        '
        Me.lblTitleKazei.AutoSizeDef = False
        Me.lblTitleKazei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKazei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKazei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleKazei.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleKazei.EnableStatus = False
        Me.lblTitleKazei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKazei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKazei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKazei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKazei.HeightDef = 18
        Me.lblTitleKazei.Location = New System.Drawing.Point(773, 65)
        Me.lblTitleKazei.Name = "lblTitleKazei"
        Me.lblTitleKazei.Size = New System.Drawing.Size(122, 18)
        Me.lblTitleKazei.TabIndex = 180
        Me.lblTitleKazei.Text = "課税分"
        Me.lblTitleKazei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitleKazei.TextValue = "課税分"
        Me.lblTitleKazei.WidthDef = 122
        '
        'lblTitleUchizei
        '
        Me.lblTitleUchizei.AutoSizeDef = False
        Me.lblTitleUchizei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUchizei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUchizei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleUchizei.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleUchizei.EnableStatus = False
        Me.lblTitleUchizei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUchizei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUchizei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUchizei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUchizei.HeightDef = 18
        Me.lblTitleUchizei.Location = New System.Drawing.Point(1136, 65)
        Me.lblTitleUchizei.Name = "lblTitleUchizei"
        Me.lblTitleUchizei.Size = New System.Drawing.Size(122, 18)
        Me.lblTitleUchizei.TabIndex = 181
        Me.lblTitleUchizei.Text = "内税分"
        Me.lblTitleUchizei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitleUchizei.TextValue = "内税分"
        Me.lblTitleUchizei.WidthDef = 122
        '
        'lblTitleHikazei
        '
        Me.lblTitleHikazei.AutoSizeDef = False
        Me.lblTitleHikazei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikazei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikazei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleHikazei.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleHikazei.EnableStatus = False
        Me.lblTitleHikazei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikazei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikazei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikazei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikazei.HeightDef = 18
        Me.lblTitleHikazei.Location = New System.Drawing.Point(1015, 65)
        Me.lblTitleHikazei.Name = "lblTitleHikazei"
        Me.lblTitleHikazei.Size = New System.Drawing.Size(122, 18)
        Me.lblTitleHikazei.TabIndex = 182
        Me.lblTitleHikazei.Text = "非･不課税分"
        Me.lblTitleHikazei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitleHikazei.TextValue = "非･不課税分"
        Me.lblTitleHikazei.WidthDef = 122
        '
        'lblTitleMenzei
        '
        Me.lblTitleMenzei.AutoSizeDef = False
        Me.lblTitleMenzei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMenzei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMenzei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleMenzei.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitleMenzei.EnableStatus = False
        Me.lblTitleMenzei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMenzei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMenzei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMenzei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMenzei.HeightDef = 18
        Me.lblTitleMenzei.Location = New System.Drawing.Point(894, 65)
        Me.lblTitleMenzei.Name = "lblTitleMenzei"
        Me.lblTitleMenzei.Size = New System.Drawing.Size(122, 18)
        Me.lblTitleMenzei.TabIndex = 183
        Me.lblTitleMenzei.Text = "免税分"
        Me.lblTitleMenzei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitleMenzei.TextValue = "免税分"
        Me.lblTitleMenzei.WidthDef = 122
        '
        'lblZeigakuM
        '
        Me.lblZeigakuM.AutoSizeDef = False
        Me.lblZeigakuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeigakuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeigakuM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeigakuM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeigakuM.EnableStatus = False
        Me.lblZeigakuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeigakuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeigakuM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeigakuM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeigakuM.HeightDef = 18
        Me.lblZeigakuM.Location = New System.Drawing.Point(894, 150)
        Me.lblZeigakuM.Name = "lblZeigakuM"
        Me.lblZeigakuM.Size = New System.Drawing.Size(122, 18)
        Me.lblZeigakuM.TabIndex = 184
        Me.lblZeigakuM.Text = "*"
        Me.lblZeigakuM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblZeigakuM.TextValue = "*"
        Me.lblZeigakuM.WidthDef = 122
        '
        'lblZeiHasuU
        '
        Me.lblZeiHasuU.AutoSizeDef = False
        Me.lblZeiHasuU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuU.EnableStatus = False
        Me.lblZeiHasuU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuU.HeightDef = 18
        Me.lblZeiHasuU.Location = New System.Drawing.Point(1136, 167)
        Me.lblZeiHasuU.Name = "lblZeiHasuU"
        Me.lblZeiHasuU.Size = New System.Drawing.Size(122, 18)
        Me.lblZeiHasuU.TabIndex = 185
        Me.lblZeiHasuU.Text = "*"
        Me.lblZeiHasuU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblZeiHasuU.TextValue = "*"
        Me.lblZeiHasuU.WidthDef = 122
        '
        'lblNebikiGakuU
        '
        Me.lblNebikiGakuU.AutoSizeDef = False
        Me.lblNebikiGakuU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiGakuU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiGakuU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiGakuU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiGakuU.EnableStatus = False
        Me.lblNebikiGakuU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiGakuU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiGakuU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiGakuU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiGakuU.HeightDef = 18
        Me.lblNebikiGakuU.Location = New System.Drawing.Point(1136, 133)
        Me.lblNebikiGakuU.Name = "lblNebikiGakuU"
        Me.lblNebikiGakuU.Size = New System.Drawing.Size(122, 18)
        Me.lblNebikiGakuU.TabIndex = 186
        Me.lblNebikiGakuU.Text = "*"
        Me.lblNebikiGakuU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNebikiGakuU.TextValue = "*"
        Me.lblNebikiGakuU.WidthDef = 122
        '
        'lblRateNebikigakuU
        '
        Me.lblRateNebikigakuU.AutoSizeDef = False
        Me.lblRateNebikigakuU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRateNebikigakuU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRateNebikigakuU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRateNebikigakuU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRateNebikigakuU.EnableStatus = False
        Me.lblRateNebikigakuU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRateNebikigakuU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRateNebikigakuU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRateNebikigakuU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRateNebikigakuU.HeightDef = 18
        Me.lblRateNebikigakuU.Location = New System.Drawing.Point(1136, 116)
        Me.lblRateNebikigakuU.Name = "lblRateNebikigakuU"
        Me.lblRateNebikigakuU.Size = New System.Drawing.Size(122, 18)
        Me.lblRateNebikigakuU.TabIndex = 187
        Me.lblRateNebikigakuU.Text = "*"
        Me.lblRateNebikigakuU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblRateNebikigakuU.TextValue = "*"
        Me.lblRateNebikigakuU.WidthDef = 122
        '
        'lblNebikiRateU
        '
        Me.lblNebikiRateU.AutoSizeDef = False
        Me.lblNebikiRateU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiRateU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiRateU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiRateU.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiRateU.EnableStatus = False
        Me.lblNebikiRateU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiRateU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiRateU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiRateU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiRateU.HeightDef = 18
        Me.lblNebikiRateU.Location = New System.Drawing.Point(1136, 99)
        Me.lblNebikiRateU.Name = "lblNebikiRateU"
        Me.lblNebikiRateU.Size = New System.Drawing.Size(122, 18)
        Me.lblNebikiRateU.TabIndex = 188
        Me.lblNebikiRateU.Text = "*"
        Me.lblNebikiRateU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNebikiRateU.TextValue = "*"
        Me.lblNebikiRateU.WidthDef = 122
        '
        'lblZeiHasuH
        '
        Me.lblZeiHasuH.AutoSizeDef = False
        Me.lblZeiHasuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuH.EnableStatus = False
        Me.lblZeiHasuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuH.HeightDef = 18
        Me.lblZeiHasuH.Location = New System.Drawing.Point(1015, 167)
        Me.lblZeiHasuH.Name = "lblZeiHasuH"
        Me.lblZeiHasuH.Size = New System.Drawing.Size(122, 18)
        Me.lblZeiHasuH.TabIndex = 189
        Me.lblZeiHasuH.Text = "*"
        Me.lblZeiHasuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblZeiHasuH.TextValue = "*"
        Me.lblZeiHasuH.WidthDef = 122
        '
        'lblZeigakuH
        '
        Me.lblZeigakuH.AutoSizeDef = False
        Me.lblZeigakuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeigakuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeigakuH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeigakuH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeigakuH.EnableStatus = False
        Me.lblZeigakuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeigakuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeigakuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeigakuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeigakuH.HeightDef = 18
        Me.lblZeigakuH.Location = New System.Drawing.Point(1015, 150)
        Me.lblZeigakuH.Name = "lblZeigakuH"
        Me.lblZeigakuH.Size = New System.Drawing.Size(122, 18)
        Me.lblZeigakuH.TabIndex = 190
        Me.lblZeigakuH.Text = "*"
        Me.lblZeigakuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblZeigakuH.TextValue = "*"
        Me.lblZeigakuH.WidthDef = 122
        '
        'lblNebikiGakuH
        '
        Me.lblNebikiGakuH.AutoSizeDef = False
        Me.lblNebikiGakuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiGakuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiGakuH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiGakuH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiGakuH.EnableStatus = False
        Me.lblNebikiGakuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiGakuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiGakuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiGakuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiGakuH.HeightDef = 18
        Me.lblNebikiGakuH.Location = New System.Drawing.Point(1015, 133)
        Me.lblNebikiGakuH.Name = "lblNebikiGakuH"
        Me.lblNebikiGakuH.Size = New System.Drawing.Size(122, 18)
        Me.lblNebikiGakuH.TabIndex = 191
        Me.lblNebikiGakuH.Text = "*"
        Me.lblNebikiGakuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNebikiGakuH.TextValue = "*"
        Me.lblNebikiGakuH.WidthDef = 122
        '
        'lblRateNebikigakuH
        '
        Me.lblRateNebikigakuH.AutoSizeDef = False
        Me.lblRateNebikigakuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRateNebikigakuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRateNebikigakuH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRateNebikigakuH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRateNebikigakuH.EnableStatus = False
        Me.lblRateNebikigakuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRateNebikigakuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRateNebikigakuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRateNebikigakuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRateNebikigakuH.HeightDef = 18
        Me.lblRateNebikigakuH.Location = New System.Drawing.Point(1015, 116)
        Me.lblRateNebikigakuH.Name = "lblRateNebikigakuH"
        Me.lblRateNebikigakuH.Size = New System.Drawing.Size(122, 18)
        Me.lblRateNebikigakuH.TabIndex = 192
        Me.lblRateNebikigakuH.Text = "*"
        Me.lblRateNebikigakuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblRateNebikigakuH.TextValue = "*"
        Me.lblRateNebikigakuH.WidthDef = 122
        '
        'lblNebikiRateH
        '
        Me.lblNebikiRateH.AutoSizeDef = False
        Me.lblNebikiRateH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiRateH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNebikiRateH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiRateH.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNebikiRateH.EnableStatus = False
        Me.lblNebikiRateH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiRateH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNebikiRateH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiRateH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNebikiRateH.HeightDef = 18
        Me.lblNebikiRateH.Location = New System.Drawing.Point(1015, 99)
        Me.lblNebikiRateH.Name = "lblNebikiRateH"
        Me.lblNebikiRateH.Size = New System.Drawing.Size(122, 18)
        Me.lblNebikiRateH.TabIndex = 193
        Me.lblNebikiRateH.Text = "*"
        Me.lblNebikiRateH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNebikiRateH.TextValue = "*"
        Me.lblNebikiRateH.WidthDef = 122
        '
        'lblZeiHasuM
        '
        Me.lblZeiHasuM.AutoSizeDef = False
        Me.lblZeiHasuM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZeiHasuM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZeiHasuM.EnableStatus = False
        Me.lblZeiHasuM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZeiHasuM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZeiHasuM.HeightDef = 18
        Me.lblZeiHasuM.Location = New System.Drawing.Point(894, 167)
        Me.lblZeiHasuM.Name = "lblZeiHasuM"
        Me.lblZeiHasuM.Size = New System.Drawing.Size(122, 18)
        Me.lblZeiHasuM.TabIndex = 194
        Me.lblZeiHasuM.Text = "*"
        Me.lblZeiHasuM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblZeiHasuM.TextValue = "*"
        Me.lblZeiHasuM.WidthDef = 122
        '
        'pnlSeikyuK
        '
        Me.pnlSeikyuK.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSeikyuK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSeikyuK.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSeikyuK.Controls.Add(Me.chkHikaeAri)
        Me.pnlSeikyuK.Controls.Add(Me.chkKeiHikaeAri)
        Me.pnlSeikyuK.Controls.Add(Me.chkMainAri)
        Me.pnlSeikyuK.Controls.Add(Me.chkSubAri)
        Me.pnlSeikyuK.EnableStatus = False
        Me.pnlSeikyuK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSeikyuK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSeikyuK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSeikyuK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSeikyuK.HeightDef = 39
        Me.pnlSeikyuK.Location = New System.Drawing.Point(533, 12)
        Me.pnlSeikyuK.Name = "pnlSeikyuK"
        Me.pnlSeikyuK.Size = New System.Drawing.Size(264, 39)
        Me.pnlSeikyuK.TabIndex = 195
        Me.pnlSeikyuK.TabStop = False
        Me.pnlSeikyuK.Text = "請求書種別"
        Me.pnlSeikyuK.TextValue = "請求書種別"
        Me.pnlSeikyuK.WidthDef = 264
        '
        'chkHikaeAri
        '
        Me.chkHikaeAri.AutoSize = True
        Me.chkHikaeAri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHikaeAri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkHikaeAri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkHikaeAri.EnableStatus = True
        Me.chkHikaeAri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHikaeAri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkHikaeAri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHikaeAri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkHikaeAri.HeightDef = 17
        Me.chkHikaeAri.Location = New System.Drawing.Point(131, 19)
        Me.chkHikaeAri.Name = "chkHikaeAri"
        Me.chkHikaeAri.Size = New System.Drawing.Size(40, 17)
        Me.chkHikaeAri.TabIndex = 200
        Me.chkHikaeAri.TabStopSetting = True
        Me.chkHikaeAri.Text = "控"
        Me.chkHikaeAri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkHikaeAri.TextValue = "控"
        Me.chkHikaeAri.UseVisualStyleBackColor = True
        Me.chkHikaeAri.WidthDef = 40
        '
        'chkKeiHikaeAri
        '
        Me.chkKeiHikaeAri.AutoSize = True
        Me.chkKeiHikaeAri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeiHikaeAri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeiHikaeAri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkKeiHikaeAri.EnableStatus = True
        Me.chkKeiHikaeAri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeiHikaeAri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeiHikaeAri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeiHikaeAri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeiHikaeAri.HeightDef = 17
        Me.chkKeiHikaeAri.Location = New System.Drawing.Point(184, 19)
        Me.chkKeiHikaeAri.Name = "chkKeiHikaeAri"
        Me.chkKeiHikaeAri.Size = New System.Drawing.Size(68, 17)
        Me.chkKeiHikaeAri.TabIndex = 199
        Me.chkKeiHikaeAri.TabStopSetting = True
        Me.chkKeiHikaeAri.Text = "経理控"
        Me.chkKeiHikaeAri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkKeiHikaeAri.TextValue = "経理控"
        Me.chkKeiHikaeAri.UseVisualStyleBackColor = True
        Me.chkKeiHikaeAri.WidthDef = 68
        '
        'chkMainAri
        '
        Me.chkMainAri.AutoSize = True
        Me.chkMainAri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMainAri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMainAri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMainAri.EnableStatus = True
        Me.chkMainAri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMainAri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMainAri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMainAri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMainAri.HeightDef = 17
        Me.chkMainAri.Location = New System.Drawing.Point(24, 19)
        Me.chkMainAri.Name = "chkMainAri"
        Me.chkMainAri.Size = New System.Drawing.Size(40, 17)
        Me.chkMainAri.TabIndex = 197
        Me.chkMainAri.TabStopSetting = True
        Me.chkMainAri.Text = "正"
        Me.chkMainAri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMainAri.TextValue = "正"
        Me.chkMainAri.UseVisualStyleBackColor = True
        Me.chkMainAri.WidthDef = 40
        '
        'chkSubAri
        '
        Me.chkSubAri.AutoSize = True
        Me.chkSubAri.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSubAri.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSubAri.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSubAri.EnableStatus = True
        Me.chkSubAri.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSubAri.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSubAri.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSubAri.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSubAri.HeightDef = 17
        Me.chkSubAri.Location = New System.Drawing.Point(80, 19)
        Me.chkSubAri.Name = "chkSubAri"
        Me.chkSubAri.Size = New System.Drawing.Size(40, 17)
        Me.chkSubAri.TabIndex = 198
        Me.chkSubAri.TabStopSetting = True
        Me.chkSubAri.Text = "副"
        Me.chkSubAri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSubAri.TextValue = "副"
        Me.chkSubAri.UseVisualStyleBackColor = True
        Me.chkSubAri.WidthDef = 40
        '
        'lblTitleBiko
        '
        Me.lblTitleBiko.AutoSize = True
        Me.lblTitleBiko.AutoSizeDef = True
        Me.lblTitleBiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBiko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBiko.EnableStatus = False
        Me.lblTitleBiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBiko.HeightDef = 13
        Me.lblTitleBiko.Location = New System.Drawing.Point(80, 234)
        Me.lblTitleBiko.Name = "lblTitleBiko"
        Me.lblTitleBiko.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleBiko.TabIndex = 39
        Me.lblTitleBiko.Text = "備考"
        Me.lblTitleBiko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBiko.TextValue = "備考"
        Me.lblTitleBiko.WidthDef = 35
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemark.CountWrappedLine = False
        Me.txtRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark.HeightDef = 18
        Me.txtRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.HissuLabelVisible = False
        Me.txtRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark.IsByteCheck = 100
        Me.txtRemark.IsCalendarCheck = False
        Me.txtRemark.IsDakutenCheck = False
        Me.txtRemark.IsEisuCheck = False
        Me.txtRemark.IsForbiddenWordsCheck = False
        Me.txtRemark.IsFullByteCheck = 0
        Me.txtRemark.IsHankakuCheck = False
        Me.txtRemark.IsHissuCheck = False
        Me.txtRemark.IsKanaCheck = False
        Me.txtRemark.IsMiddleSpace = False
        Me.txtRemark.IsNumericCheck = False
        Me.txtRemark.IsSujiCheck = False
        Me.txtRemark.IsZenkakuCheck = False
        Me.txtRemark.ItemName = ""
        Me.txtRemark.LineSpace = 0
        Me.txtRemark.Location = New System.Drawing.Point(122, 231)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(787, 18)
        Me.txtRemark.TabIndex = 40
        Me.txtRemark.TabStopSetting = True
        Me.txtRemark.TextValue = "Ｎ－－－－－－－－ＮＮ－－－－－－－－ＮＮ－－－－－－－－ＮＮ－－－－－－－－ＮＮ－－－－－－－－Ｎ"
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 787
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'chkAkaden
        '
        Me.chkAkaden.AutoSize = True
        Me.chkAkaden.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkAkaden.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkAkaden.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkAkaden.EnableStatus = True
        Me.chkAkaden.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkAkaden.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkAkaden.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAkaden.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAkaden.HeightDef = 17
        Me.chkAkaden.Location = New System.Drawing.Point(290, 36)
        Me.chkAkaden.Name = "chkAkaden"
        Me.chkAkaden.Size = New System.Drawing.Size(68, 17)
        Me.chkAkaden.TabIndex = 201
        Me.chkAkaden.TabStopSetting = True
        Me.chkAkaden.Text = "赤伝票"
        Me.chkAkaden.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAkaden.TextValue = "赤伝票"
        Me.chkAkaden.UseVisualStyleBackColor = True
        Me.chkAkaden.WidthDef = 68
        '
        'chkTemplate
        '
        Me.chkTemplate.AutoSize = True
        Me.chkTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTemplate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTemplate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkTemplate.EnableStatus = True
        Me.chkTemplate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTemplate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTemplate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTemplate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTemplate.HeightDef = 17
        Me.chkTemplate.Location = New System.Drawing.Point(480, 256)
        Me.chkTemplate.Name = "chkTemplate"
        Me.chkTemplate.Size = New System.Drawing.Size(152, 17)
        Me.chkTemplate.TabIndex = 203
        Me.chkTemplate.TabStopSetting = True
        Me.chkTemplate.Text = "テンプレートマスタ"
        Me.chkTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTemplate.TextValue = "テンプレートマスタ"
        Me.chkTemplate.UseVisualStyleBackColor = True
        Me.chkTemplate.WidthDef = 152
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "S063"
        Me.cmbPrint.DataSource = Nothing
        Me.cmbPrint.DisplayMember = Nothing
        Me.cmbPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.HeightDef = 18
        Me.cmbPrint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrint.HissuLabelVisible = False
        Me.cmbPrint.InsertWildCard = True
        Me.cmbPrint.IsForbiddenWordsCheck = False
        Me.cmbPrint.IsHissuCheck = False
        Me.cmbPrint.ItemName = ""
        Me.cmbPrint.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrint.Location = New System.Drawing.Point(809, 29)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(175, 18)
        Me.cmbPrint.TabIndex = 205
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 175
        '
        'lblYokomochiImpDate
        '
        Me.lblYokomochiImpDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokomochiImpDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokomochiImpDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYokomochiImpDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblYokomochiImpDate.CountWrappedLine = False
        Me.lblYokomochiImpDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblYokomochiImpDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokomochiImpDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYokomochiImpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokomochiImpDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblYokomochiImpDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblYokomochiImpDate.HeightDef = 18
        Me.lblYokomochiImpDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblYokomochiImpDate.HissuLabelVisible = False
        Me.lblYokomochiImpDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblYokomochiImpDate.IsByteCheck = 0
        Me.lblYokomochiImpDate.IsCalendarCheck = False
        Me.lblYokomochiImpDate.IsDakutenCheck = False
        Me.lblYokomochiImpDate.IsEisuCheck = False
        Me.lblYokomochiImpDate.IsForbiddenWordsCheck = False
        Me.lblYokomochiImpDate.IsFullByteCheck = 0
        Me.lblYokomochiImpDate.IsHankakuCheck = False
        Me.lblYokomochiImpDate.IsHissuCheck = False
        Me.lblYokomochiImpDate.IsKanaCheck = False
        Me.lblYokomochiImpDate.IsMiddleSpace = False
        Me.lblYokomochiImpDate.IsNumericCheck = False
        Me.lblYokomochiImpDate.IsSujiCheck = False
        Me.lblYokomochiImpDate.IsZenkakuCheck = False
        Me.lblYokomochiImpDate.ItemName = ""
        Me.lblYokomochiImpDate.LineSpace = 0
        Me.lblYokomochiImpDate.Location = New System.Drawing.Point(940, 280)
        Me.lblYokomochiImpDate.MaxLength = 0
        Me.lblYokomochiImpDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblYokomochiImpDate.MaxLineCount = 0
        Me.lblYokomochiImpDate.Multiline = False
        Me.lblYokomochiImpDate.Name = "lblYokomochiImpDate"
        Me.lblYokomochiImpDate.ReadOnly = True
        Me.lblYokomochiImpDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblYokomochiImpDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblYokomochiImpDate.Size = New System.Drawing.Size(100, 18)
        Me.lblYokomochiImpDate.TabIndex = 206
        Me.lblYokomochiImpDate.TabStop = False
        Me.lblYokomochiImpDate.TabStopSetting = False
        Me.lblYokomochiImpDate.TextValue = ""
        Me.lblYokomochiImpDate.UseSystemPasswordChar = False
        Me.lblYokomochiImpDate.Visible = False
        Me.lblYokomochiImpDate.WidthDef = 100
        Me.lblYokomochiImpDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyoImpDate
        '
        Me.lblSagyoImpDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoImpDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoImpDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyoImpDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyoImpDate.CountWrappedLine = False
        Me.lblSagyoImpDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyoImpDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoImpDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoImpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoImpDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoImpDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyoImpDate.HeightDef = 18
        Me.lblSagyoImpDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoImpDate.HissuLabelVisible = False
        Me.lblSagyoImpDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyoImpDate.IsByteCheck = 0
        Me.lblSagyoImpDate.IsCalendarCheck = False
        Me.lblSagyoImpDate.IsDakutenCheck = False
        Me.lblSagyoImpDate.IsEisuCheck = False
        Me.lblSagyoImpDate.IsForbiddenWordsCheck = False
        Me.lblSagyoImpDate.IsFullByteCheck = 0
        Me.lblSagyoImpDate.IsHankakuCheck = False
        Me.lblSagyoImpDate.IsHissuCheck = False
        Me.lblSagyoImpDate.IsKanaCheck = False
        Me.lblSagyoImpDate.IsMiddleSpace = False
        Me.lblSagyoImpDate.IsNumericCheck = False
        Me.lblSagyoImpDate.IsSujiCheck = False
        Me.lblSagyoImpDate.IsZenkakuCheck = False
        Me.lblSagyoImpDate.ItemName = ""
        Me.lblSagyoImpDate.LineSpace = 0
        Me.lblSagyoImpDate.Location = New System.Drawing.Point(759, 280)
        Me.lblSagyoImpDate.MaxLength = 0
        Me.lblSagyoImpDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyoImpDate.MaxLineCount = 0
        Me.lblSagyoImpDate.Multiline = False
        Me.lblSagyoImpDate.Name = "lblSagyoImpDate"
        Me.lblSagyoImpDate.ReadOnly = True
        Me.lblSagyoImpDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyoImpDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyoImpDate.Size = New System.Drawing.Size(100, 18)
        Me.lblSagyoImpDate.TabIndex = 207
        Me.lblSagyoImpDate.TabStop = False
        Me.lblSagyoImpDate.TabStopSetting = False
        Me.lblSagyoImpDate.TextValue = ""
        Me.lblSagyoImpDate.UseSystemPasswordChar = False
        Me.lblSagyoImpDate.Visible = False
        Me.lblSagyoImpDate.WidthDef = 100
        Me.lblSagyoImpDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnchinImpDate
        '
        Me.lblUnchinImpDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinImpDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinImpDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnchinImpDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnchinImpDate.CountWrappedLine = False
        Me.lblUnchinImpDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnchinImpDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinImpDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinImpDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinImpDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinImpDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnchinImpDate.HeightDef = 18
        Me.lblUnchinImpDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinImpDate.HissuLabelVisible = False
        Me.lblUnchinImpDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnchinImpDate.IsByteCheck = 0
        Me.lblUnchinImpDate.IsCalendarCheck = False
        Me.lblUnchinImpDate.IsDakutenCheck = False
        Me.lblUnchinImpDate.IsEisuCheck = False
        Me.lblUnchinImpDate.IsForbiddenWordsCheck = False
        Me.lblUnchinImpDate.IsFullByteCheck = 0
        Me.lblUnchinImpDate.IsHankakuCheck = False
        Me.lblUnchinImpDate.IsHissuCheck = False
        Me.lblUnchinImpDate.IsKanaCheck = False
        Me.lblUnchinImpDate.IsMiddleSpace = False
        Me.lblUnchinImpDate.IsNumericCheck = False
        Me.lblUnchinImpDate.IsSujiCheck = False
        Me.lblUnchinImpDate.IsZenkakuCheck = False
        Me.lblUnchinImpDate.ItemName = ""
        Me.lblUnchinImpDate.LineSpace = 0
        Me.lblUnchinImpDate.Location = New System.Drawing.Point(584, 280)
        Me.lblUnchinImpDate.MaxLength = 0
        Me.lblUnchinImpDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnchinImpDate.MaxLineCount = 0
        Me.lblUnchinImpDate.Multiline = False
        Me.lblUnchinImpDate.Name = "lblUnchinImpDate"
        Me.lblUnchinImpDate.ReadOnly = True
        Me.lblUnchinImpDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnchinImpDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnchinImpDate.Size = New System.Drawing.Size(100, 18)
        Me.lblUnchinImpDate.TabIndex = 208
        Me.lblUnchinImpDate.TabStop = False
        Me.lblUnchinImpDate.TabStopSetting = False
        Me.lblUnchinImpDate.TextValue = ""
        Me.lblUnchinImpDate.UseSystemPasswordChar = False
        Me.lblUnchinImpDate.Visible = False
        Me.lblUnchinImpDate.WidthDef = 100
        Me.lblUnchinImpDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdTime
        '
        Me.lblSysUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdTime.CountWrappedLine = False
        Me.lblSysUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdTime.HeightDef = 18
        Me.lblSysUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.HissuLabelVisible = False
        Me.lblSysUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdTime.IsByteCheck = 0
        Me.lblSysUpdTime.IsCalendarCheck = False
        Me.lblSysUpdTime.IsDakutenCheck = False
        Me.lblSysUpdTime.IsEisuCheck = False
        Me.lblSysUpdTime.IsForbiddenWordsCheck = False
        Me.lblSysUpdTime.IsFullByteCheck = 0
        Me.lblSysUpdTime.IsHankakuCheck = False
        Me.lblSysUpdTime.IsHissuCheck = False
        Me.lblSysUpdTime.IsKanaCheck = False
        Me.lblSysUpdTime.IsMiddleSpace = False
        Me.lblSysUpdTime.IsNumericCheck = False
        Me.lblSysUpdTime.IsSujiCheck = False
        Me.lblSysUpdTime.IsZenkakuCheck = False
        Me.lblSysUpdTime.ItemName = ""
        Me.lblSysUpdTime.LineSpace = 0
        Me.lblSysUpdTime.Location = New System.Drawing.Point(846, 280)
        Me.lblSysUpdTime.MaxLength = 0
        Me.lblSysUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdTime.MaxLineCount = 0
        Me.lblSysUpdTime.Multiline = False
        Me.lblSysUpdTime.Name = "lblSysUpdTime"
        Me.lblSysUpdTime.ReadOnly = True
        Me.lblSysUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdTime.Size = New System.Drawing.Size(100, 18)
        Me.lblSysUpdTime.TabIndex = 209
        Me.lblSysUpdTime.TabStop = False
        Me.lblSysUpdTime.TabStopSetting = False
        Me.lblSysUpdTime.TextValue = ""
        Me.lblSysUpdTime.UseSystemPasswordChar = False
        Me.lblSysUpdTime.Visible = False
        Me.lblSysUpdTime.WidthDef = 100
        Me.lblSysUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdDate
        '
        Me.lblSysUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdDate.CountWrappedLine = False
        Me.lblSysUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdDate.HeightDef = 18
        Me.lblSysUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.HissuLabelVisible = False
        Me.lblSysUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdDate.IsByteCheck = 0
        Me.lblSysUpdDate.IsCalendarCheck = False
        Me.lblSysUpdDate.IsDakutenCheck = False
        Me.lblSysUpdDate.IsEisuCheck = False
        Me.lblSysUpdDate.IsForbiddenWordsCheck = False
        Me.lblSysUpdDate.IsFullByteCheck = 0
        Me.lblSysUpdDate.IsHankakuCheck = False
        Me.lblSysUpdDate.IsHissuCheck = False
        Me.lblSysUpdDate.IsKanaCheck = False
        Me.lblSysUpdDate.IsMiddleSpace = False
        Me.lblSysUpdDate.IsNumericCheck = False
        Me.lblSysUpdDate.IsSujiCheck = False
        Me.lblSysUpdDate.IsZenkakuCheck = False
        Me.lblSysUpdDate.ItemName = ""
        Me.lblSysUpdDate.LineSpace = 0
        Me.lblSysUpdDate.Location = New System.Drawing.Point(671, 280)
        Me.lblSysUpdDate.MaxLength = 0
        Me.lblSysUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdDate.MaxLineCount = 0
        Me.lblSysUpdDate.Multiline = False
        Me.lblSysUpdDate.Name = "lblSysUpdDate"
        Me.lblSysUpdDate.ReadOnly = True
        Me.lblSysUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdDate.Size = New System.Drawing.Size(100, 18)
        Me.lblSysUpdDate.TabIndex = 210
        Me.lblSysUpdDate.TabStop = False
        Me.lblSysUpdDate.TabStopSetting = False
        Me.lblSysUpdDate.TextValue = ""
        Me.lblSysUpdDate.UseSystemPasswordChar = False
        Me.lblSysUpdDate.Visible = False
        Me.lblSysUpdDate.WidthDef = 100
        Me.lblSysUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblMaxEdaban
        '
        Me.lblMaxEdaban.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEdaban.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEdaban.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMaxEdaban.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMaxEdaban.CountWrappedLine = False
        Me.lblMaxEdaban.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMaxEdaban.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxEdaban.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaxEdaban.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMaxEdaban.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMaxEdaban.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMaxEdaban.HeightDef = 18
        Me.lblMaxEdaban.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMaxEdaban.HissuLabelVisible = False
        Me.lblMaxEdaban.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMaxEdaban.IsByteCheck = 0
        Me.lblMaxEdaban.IsCalendarCheck = False
        Me.lblMaxEdaban.IsDakutenCheck = False
        Me.lblMaxEdaban.IsEisuCheck = False
        Me.lblMaxEdaban.IsForbiddenWordsCheck = False
        Me.lblMaxEdaban.IsFullByteCheck = 0
        Me.lblMaxEdaban.IsHankakuCheck = False
        Me.lblMaxEdaban.IsHissuCheck = False
        Me.lblMaxEdaban.IsKanaCheck = False
        Me.lblMaxEdaban.IsMiddleSpace = False
        Me.lblMaxEdaban.IsNumericCheck = False
        Me.lblMaxEdaban.IsSujiCheck = False
        Me.lblMaxEdaban.IsZenkakuCheck = False
        Me.lblMaxEdaban.ItemName = ""
        Me.lblMaxEdaban.LineSpace = 0
        Me.lblMaxEdaban.Location = New System.Drawing.Point(1030, 280)
        Me.lblMaxEdaban.MaxLength = 0
        Me.lblMaxEdaban.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMaxEdaban.MaxLineCount = 0
        Me.lblMaxEdaban.Multiline = False
        Me.lblMaxEdaban.Name = "lblMaxEdaban"
        Me.lblMaxEdaban.ReadOnly = True
        Me.lblMaxEdaban.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMaxEdaban.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMaxEdaban.Size = New System.Drawing.Size(100, 18)
        Me.lblMaxEdaban.TabIndex = 211
        Me.lblMaxEdaban.TabStop = False
        Me.lblMaxEdaban.TabStopSetting = False
        Me.lblMaxEdaban.TextValue = ""
        Me.lblMaxEdaban.UseSystemPasswordChar = False
        Me.lblMaxEdaban.Visible = False
        Me.lblMaxEdaban.WidthDef = 100
        Me.lblMaxEdaban.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSeikyuNoRelated
        '
        Me.lblSeikyuNoRelated.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNoRelated.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNoRelated.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeikyuNoRelated.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeikyuNoRelated.CountWrappedLine = False
        Me.lblSeikyuNoRelated.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeikyuNoRelated.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNoRelated.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeikyuNoRelated.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNoRelated.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeikyuNoRelated.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeikyuNoRelated.HeightDef = 18
        Me.lblSeikyuNoRelated.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeikyuNoRelated.HissuLabelVisible = False
        Me.lblSeikyuNoRelated.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeikyuNoRelated.IsByteCheck = 0
        Me.lblSeikyuNoRelated.IsCalendarCheck = False
        Me.lblSeikyuNoRelated.IsDakutenCheck = False
        Me.lblSeikyuNoRelated.IsEisuCheck = False
        Me.lblSeikyuNoRelated.IsForbiddenWordsCheck = False
        Me.lblSeikyuNoRelated.IsFullByteCheck = 0
        Me.lblSeikyuNoRelated.IsHankakuCheck = False
        Me.lblSeikyuNoRelated.IsHissuCheck = False
        Me.lblSeikyuNoRelated.IsKanaCheck = False
        Me.lblSeikyuNoRelated.IsMiddleSpace = False
        Me.lblSeikyuNoRelated.IsNumericCheck = False
        Me.lblSeikyuNoRelated.IsSujiCheck = False
        Me.lblSeikyuNoRelated.IsZenkakuCheck = False
        Me.lblSeikyuNoRelated.ItemName = ""
        Me.lblSeikyuNoRelated.LineSpace = 0
        Me.lblSeikyuNoRelated.Location = New System.Drawing.Point(1136, 280)
        Me.lblSeikyuNoRelated.MaxLength = 0
        Me.lblSeikyuNoRelated.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeikyuNoRelated.MaxLineCount = 0
        Me.lblSeikyuNoRelated.Multiline = False
        Me.lblSeikyuNoRelated.Name = "lblSeikyuNoRelated"
        Me.lblSeikyuNoRelated.ReadOnly = True
        Me.lblSeikyuNoRelated.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeikyuNoRelated.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeikyuNoRelated.Size = New System.Drawing.Size(100, 18)
        Me.lblSeikyuNoRelated.TabIndex = 212
        Me.lblSeikyuNoRelated.TabStop = False
        Me.lblSeikyuNoRelated.TabStopSetting = False
        Me.lblSeikyuNoRelated.TextValue = ""
        Me.lblSeikyuNoRelated.UseSystemPasswordChar = False
        Me.lblSeikyuNoRelated.Visible = False
        Me.lblSeikyuNoRelated.WidthDef = 100
        Me.lblSeikyuNoRelated.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleExRate
        '
        Me.lblTitleExRate.AutoSize = True
        Me.lblTitleExRate.AutoSizeDef = True
        Me.lblTitleExRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExRate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExRate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleExRate.EnableStatus = False
        Me.lblTitleExRate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExRate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExRate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExRate.HeightDef = 13
        Me.lblTitleExRate.Location = New System.Drawing.Point(661, 259)
        Me.lblTitleExRate.Name = "lblTitleExRate"
        Me.lblTitleExRate.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleExRate.TabIndex = 215
        Me.lblTitleExRate.Text = "Ex. Rate"
        Me.lblTitleExRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleExRate.TextValue = "Ex. Rate"
        Me.lblTitleExRate.WidthDef = 63
        '
        'lblRate1
        '
        Me.lblRate1.AutoSize = True
        Me.lblRate1.AutoSizeDef = True
        Me.lblRate1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRate1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRate1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblRate1.EnableStatus = False
        Me.lblRate1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRate1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRate1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRate1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRate1.HeightDef = 13
        Me.lblRate1.Location = New System.Drawing.Point(741, 259)
        Me.lblRate1.Name = "lblRate1"
        Me.lblRate1.Size = New System.Drawing.Size(14, 13)
        Me.lblRate1.TabIndex = 216
        Me.lblRate1.Text = "1"
        Me.lblRate1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRate1.TextValue = "1"
        Me.lblRate1.WidthDef = 14
        '
        'lblCurrencyConversion
        '
        Me.lblCurrencyConversion.AutoSize = True
        Me.lblCurrencyConversion.AutoSizeDef = True
        Me.lblCurrencyConversion.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCurrencyConversion.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCurrencyConversion.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCurrencyConversion.EnableStatus = False
        Me.lblCurrencyConversion.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCurrencyConversion.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCurrencyConversion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCurrencyConversion.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCurrencyConversion.HeightDef = 13
        Me.lblCurrencyConversion.Location = New System.Drawing.Point(861, 259)
        Me.lblCurrencyConversion.Name = "lblCurrencyConversion"
        Me.lblCurrencyConversion.Size = New System.Drawing.Size(28, 13)
        Me.lblCurrencyConversion.TabIndex = 217
        Me.lblCurrencyConversion.Text = ">>>"
        Me.lblCurrencyConversion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCurrencyConversion.TextValue = ">>>"
        Me.lblCurrencyConversion.WidthDef = 28
        '
        'numExRate
        '
        Me.numExRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numExRate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numExRate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numExRate.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numExRate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numExRate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numExRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numExRate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numExRate.HeightDef = 18
        Me.numExRate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numExRate.HissuLabelVisible = False
        Me.numExRate.IsHissuCheck = False
        Me.numExRate.IsRangeCheck = False
        Me.numExRate.ItemName = ""
        Me.numExRate.Location = New System.Drawing.Point(897, 257)
        Me.numExRate.Name = "numExRate"
        Me.numExRate.ReadOnly = True
        Me.numExRate.Size = New System.Drawing.Size(137, 18)
        Me.numExRate.TabIndex = 218
        Me.numExRate.TabStop = False
        Me.numExRate.TabStopSetting = False
        Me.numExRate.TextValue = "0"
        Me.numExRate.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numExRate.WidthDef = 137
        '
        'cmbStateKbn
        '
        Me.cmbStateKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbStateKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbStateKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbStateKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbStateKbn.DataCode = "S025"
        Me.cmbStateKbn.DataSource = Nothing
        Me.cmbStateKbn.DisplayMember = Nothing
        Me.cmbStateKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStateKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbStateKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStateKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbStateKbn.HeightDef = 18
        Me.cmbStateKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbStateKbn.HissuLabelVisible = False
        Me.cmbStateKbn.InsertWildCard = True
        Me.cmbStateKbn.IsForbiddenWordsCheck = False
        Me.cmbStateKbn.IsHissuCheck = False
        Me.cmbStateKbn.ItemName = ""
        Me.cmbStateKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbStateKbn.Location = New System.Drawing.Point(354, 54)
        Me.cmbStateKbn.Name = "cmbStateKbn"
        Me.cmbStateKbn.ReadOnly = True
        Me.cmbStateKbn.SelectedIndex = -1
        Me.cmbStateKbn.SelectedItem = Nothing
        Me.cmbStateKbn.SelectedText = ""
        Me.cmbStateKbn.SelectedValue = ""
        Me.cmbStateKbn.Size = New System.Drawing.Size(157, 18)
        Me.cmbStateKbn.TabIndex = 202
        Me.cmbStateKbn.TabStop = False
        Me.cmbStateKbn.TabStopSetting = False
        Me.cmbStateKbn.TextValue = ""
        Me.cmbStateKbn.Value1 = Nothing
        Me.cmbStateKbn.Value2 = Nothing
        Me.cmbStateKbn.Value3 = Nothing
        Me.cmbStateKbn.ValueMember = Nothing
        Me.cmbStateKbn.WidthDef = 157
        '
        'cmbCurrencyConversion1
        '
        Me.cmbCurrencyConversion1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCurrencyConversion1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCurrencyConversion1.DataCode = ""
        Me.cmbCurrencyConversion1.DataSource = Nothing
        Me.cmbCurrencyConversion1.DisplayMember = Nothing
        Me.cmbCurrencyConversion1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCurrencyConversion1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCurrencyConversion1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCurrencyConversion1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCurrencyConversion1.HeightDef = 18
        Me.cmbCurrencyConversion1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion1.HissuLabelVisible = True
        Me.cmbCurrencyConversion1.InsertWildCard = True
        Me.cmbCurrencyConversion1.IsForbiddenWordsCheck = False
        Me.cmbCurrencyConversion1.IsHissuCheck = True
        Me.cmbCurrencyConversion1.ItemName = ""
        Me.cmbCurrencyConversion1.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCurrencyConversion1.Location = New System.Drawing.Point(761, 257)
        Me.cmbCurrencyConversion1.Name = "cmbCurrencyConversion1"
        Me.cmbCurrencyConversion1.ReadOnly = True
        Me.cmbCurrencyConversion1.SelectedIndex = -1
        Me.cmbCurrencyConversion1.SelectedItem = Nothing
        Me.cmbCurrencyConversion1.SelectedText = ""
        Me.cmbCurrencyConversion1.SelectedValue = ""
        Me.cmbCurrencyConversion1.Size = New System.Drawing.Size(110, 18)
        Me.cmbCurrencyConversion1.TabIndex = 580
        Me.cmbCurrencyConversion1.TabStop = False
        Me.cmbCurrencyConversion1.TabStopSetting = False
        Me.cmbCurrencyConversion1.TextValue = ""
        Me.cmbCurrencyConversion1.Value1 = Nothing
        Me.cmbCurrencyConversion1.Value2 = Nothing
        Me.cmbCurrencyConversion1.Value3 = Nothing
        Me.cmbCurrencyConversion1.ValueMember = Nothing
        Me.cmbCurrencyConversion1.WidthDef = 110
        '
        'cmbCurrencyConversion2
        '
        Me.cmbCurrencyConversion2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCurrencyConversion2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCurrencyConversion2.DataCode = ""
        Me.cmbCurrencyConversion2.DataSource = Nothing
        Me.cmbCurrencyConversion2.DisplayMember = Nothing
        Me.cmbCurrencyConversion2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCurrencyConversion2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCurrencyConversion2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCurrencyConversion2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCurrencyConversion2.HeightDef = 18
        Me.cmbCurrencyConversion2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCurrencyConversion2.HissuLabelVisible = True
        Me.cmbCurrencyConversion2.InsertWildCard = True
        Me.cmbCurrencyConversion2.IsForbiddenWordsCheck = False
        Me.cmbCurrencyConversion2.IsHissuCheck = True
        Me.cmbCurrencyConversion2.ItemName = ""
        Me.cmbCurrencyConversion2.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCurrencyConversion2.Location = New System.Drawing.Point(1030, 257)
        Me.cmbCurrencyConversion2.Name = "cmbCurrencyConversion2"
        Me.cmbCurrencyConversion2.ReadOnly = True
        Me.cmbCurrencyConversion2.SelectedIndex = -1
        Me.cmbCurrencyConversion2.SelectedItem = Nothing
        Me.cmbCurrencyConversion2.SelectedText = ""
        Me.cmbCurrencyConversion2.SelectedValue = ""
        Me.cmbCurrencyConversion2.Size = New System.Drawing.Size(110, 18)
        Me.cmbCurrencyConversion2.TabIndex = 581
        Me.cmbCurrencyConversion2.TabStop = False
        Me.cmbCurrencyConversion2.TabStopSetting = False
        Me.cmbCurrencyConversion2.TextValue = ""
        Me.cmbCurrencyConversion2.Value1 = Nothing
        Me.cmbCurrencyConversion2.Value2 = Nothing
        Me.cmbCurrencyConversion2.Value3 = Nothing
        Me.cmbCurrencyConversion2.ValueMember = Nothing
        Me.cmbCurrencyConversion2.WidthDef = 110
        '
        'lblUnsoUT
        '
        Me.lblUnsoUT.AutoSize = True
        Me.lblUnsoUT.AutoSizeDef = True
        Me.lblUnsoUT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoUT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoUT.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnsoUT.EnableStatus = False
        Me.lblUnsoUT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoUT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoUT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoUT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoUT.HeightDef = 13
        Me.lblUnsoUT.Location = New System.Drawing.Point(1084, 234)
        Me.lblUnsoUT.Name = "lblUnsoUT"
        Me.lblUnsoUT.Size = New System.Drawing.Size(21, 13)
        Me.lblUnsoUT.TabIndex = 583
        Me.lblUnsoUT.Text = "KG"
        Me.lblUnsoUT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnsoUT.TextValue = "KG"
        Me.lblUnsoUT.WidthDef = 21
        '
        'lblUnsoWT
        '
        Me.lblUnsoWT.AutoSize = True
        Me.lblUnsoWT.AutoSizeDef = True
        Me.lblUnsoWT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoWT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoWT.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblUnsoWT.EnableStatus = False
        Me.lblUnsoWT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoWT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoWT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoWT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoWT.HeightDef = 13
        Me.lblUnsoWT.Location = New System.Drawing.Point(903, 234)
        Me.lblUnsoWT.Name = "lblUnsoWT"
        Me.lblUnsoWT.Size = New System.Drawing.Size(63, 13)
        Me.lblUnsoWT.TabIndex = 582
        Me.lblUnsoWT.Text = "運送重量"
        Me.lblUnsoWT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnsoWT.TextValue = "運送重量"
        Me.lblUnsoWT.WidthDef = 63
        '
        'numUnsoWT
        '
        Me.numUnsoWT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoWT.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoWT.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoWT.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoWT.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWT.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoWT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWT.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoWT.HeightDef = 18
        Me.numUnsoWT.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoWT.HissuLabelVisible = False
        Me.numUnsoWT.IsHissuCheck = False
        Me.numUnsoWT.IsRangeCheck = False
        Me.numUnsoWT.ItemName = ""
        Me.numUnsoWT.Location = New System.Drawing.Point(966, 231)
        Me.numUnsoWT.Name = "numUnsoWT"
        Me.numUnsoWT.ReadOnly = False
        Me.numUnsoWT.Size = New System.Drawing.Size(130, 18)
        Me.numUnsoWT.TabIndex = 584
        Me.numUnsoWT.TabStopSetting = True
        Me.numUnsoWT.TextValue = "0"
        Me.numUnsoWT.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnsoWT.WidthDef = 130
        '
        'btnSapOut
        '
        Me.btnSapOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSapOut.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSapOut.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSapOut.EnableStatus = True
        Me.btnSapOut.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSapOut.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSapOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSapOut.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSapOut.HeightDef = 22
        Me.btnSapOut.Location = New System.Drawing.Point(967, 3)
        Me.btnSapOut.Name = "btnSapOut"
        Me.btnSapOut.Size = New System.Drawing.Size(70, 22)
        Me.btnSapOut.TabIndex = 585
        Me.btnSapOut.TabStopSetting = True
        Me.btnSapOut.Text = "SAP出力"
        Me.btnSapOut.TextValue = "SAP出力"
        Me.btnSapOut.UseVisualStyleBackColor = True
        Me.btnSapOut.Visible = False
        Me.btnSapOut.WidthDef = 70
        '
        'btnSapCancel
        '
        Me.btnSapCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSapCancel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSapCancel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSapCancel.EnableStatus = True
        Me.btnSapCancel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSapCancel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSapCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSapCancel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSapCancel.HeightDef = 22
        Me.btnSapCancel.Location = New System.Drawing.Point(1042, 3)
        Me.btnSapCancel.Name = "btnSapCancel"
        Me.btnSapCancel.Size = New System.Drawing.Size(70, 22)
        Me.btnSapCancel.TabIndex = 586
        Me.btnSapCancel.TabStopSetting = True
        Me.btnSapCancel.Text = "SAP取消"
        Me.btnSapCancel.TextValue = "SAP取消"
        Me.btnSapCancel.UseVisualStyleBackColor = True
        Me.btnSapCancel.Visible = False
        Me.btnSapCancel.WidthDef = 70
        '
        'lblSapNo
        '
        Me.lblSapNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSapNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSapNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSapNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSapNo.CountWrappedLine = False
        Me.lblSapNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSapNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSapNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSapNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSapNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSapNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSapNo.HeightDef = 18
        Me.lblSapNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSapNo.HissuLabelVisible = False
        Me.lblSapNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSapNo.IsByteCheck = 0
        Me.lblSapNo.IsCalendarCheck = False
        Me.lblSapNo.IsDakutenCheck = False
        Me.lblSapNo.IsEisuCheck = False
        Me.lblSapNo.IsForbiddenWordsCheck = False
        Me.lblSapNo.IsFullByteCheck = 0
        Me.lblSapNo.IsHankakuCheck = False
        Me.lblSapNo.IsHissuCheck = False
        Me.lblSapNo.IsKanaCheck = False
        Me.lblSapNo.IsMiddleSpace = False
        Me.lblSapNo.IsNumericCheck = False
        Me.lblSapNo.IsSujiCheck = False
        Me.lblSapNo.IsZenkakuCheck = False
        Me.lblSapNo.ItemName = ""
        Me.lblSapNo.LineSpace = 0
        Me.lblSapNo.Location = New System.Drawing.Point(121, 75)
        Me.lblSapNo.MaxLength = 0
        Me.lblSapNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSapNo.MaxLineCount = 0
        Me.lblSapNo.Multiline = False
        Me.lblSapNo.Name = "lblSapNo"
        Me.lblSapNo.ReadOnly = True
        Me.lblSapNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSapNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSapNo.Size = New System.Drawing.Size(165, 18)
        Me.lblSapNo.TabIndex = 588
        Me.lblSapNo.TabStop = False
        Me.lblSapNo.TabStopSetting = False
        Me.lblSapNo.TextValue = ""
        Me.lblSapNo.UseSystemPasswordChar = False
        Me.lblSapNo.WidthDef = 165
        Me.lblSapNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSapNo
        '
        Me.lblTitleSapNo.AutoSize = True
        Me.lblTitleSapNo.AutoSizeDef = True
        Me.lblTitleSapNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSapNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSapNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSapNo.EnableStatus = False
        Me.lblTitleSapNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSapNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSapNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSapNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSapNo.HeightDef = 13
        Me.lblTitleSapNo.Location = New System.Drawing.Point(31, 78)
        Me.lblTitleSapNo.Name = "lblTitleSapNo"
        Me.lblTitleSapNo.Size = New System.Drawing.Size(84, 13)
        Me.lblTitleSapNo.TabIndex = 587
        Me.lblTitleSapNo.Text = "SAP伝票番号"
        Me.lblTitleSapNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSapNo.TextValue = "SAP伝票番号"
        Me.lblTitleSapNo.WidthDef = 84
        '
        'chkDepotHokan
        '
        Me.chkDepotHokan.AutoSize = True
        Me.chkDepotHokan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDepotHokan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDepotHokan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkDepotHokan.EnableStatus = True
        Me.chkDepotHokan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDepotHokan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDepotHokan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDepotHokan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDepotHokan.HeightDef = 17
        Me.chkDepotHokan.Location = New System.Drawing.Point(194, 273)
        Me.chkDepotHokan.Name = "chkDepotHokan"
        Me.chkDepotHokan.Size = New System.Drawing.Size(82, 17)
        Me.chkDepotHokan.TabIndex = 589
        Me.chkDepotHokan.TabStopSetting = True
        Me.chkDepotHokan.Text = "デポ保管"
        Me.chkDepotHokan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDepotHokan.TextValue = "デポ保管"
        Me.chkDepotHokan.UseVisualStyleBackColor = True
        Me.chkDepotHokan.WidthDef = 82
        '
        'chkDepotLift
        '
        Me.chkDepotLift.AutoSize = True
        Me.chkDepotLift.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDepotLift.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDepotLift.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkDepotLift.EnableStatus = True
        Me.chkDepotLift.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDepotLift.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDepotLift.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDepotLift.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDepotLift.HeightDef = 17
        Me.chkDepotLift.Location = New System.Drawing.Point(284, 273)
        Me.chkDepotLift.Name = "chkDepotLift"
        Me.chkDepotLift.Size = New System.Drawing.Size(96, 17)
        Me.chkDepotLift.TabIndex = 590
        Me.chkDepotLift.TabStopSetting = True
        Me.chkDepotLift.Text = "デポリフト"
        Me.chkDepotLift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDepotLift.TextValue = "デポリフト"
        Me.chkDepotLift.UseVisualStyleBackColor = True
        Me.chkDepotLift.WidthDef = 96
        '
        'chkContainerUnso
        '
        Me.chkContainerUnso.AutoSize = True
        Me.chkContainerUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkContainerUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkContainerUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkContainerUnso.EnableStatus = True
        Me.chkContainerUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkContainerUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkContainerUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkContainerUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkContainerUnso.HeightDef = 17
        Me.chkContainerUnso.Location = New System.Drawing.Point(383, 273)
        Me.chkContainerUnso.Name = "chkContainerUnso"
        Me.chkContainerUnso.Size = New System.Drawing.Size(159, 17)
        Me.chkContainerUnso.TabIndex = 591
        Me.chkContainerUnso.TabStopSetting = True
        Me.chkContainerUnso.Text = "運送収入（ISO自車）"
        Me.chkContainerUnso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkContainerUnso.TextValue = "運送収入（ISO自車）"
        Me.chkContainerUnso.UseVisualStyleBackColor = True
        Me.chkContainerUnso.WidthDef = 159
        '
        'LMG050F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 892)
        Me.Name = "LMG050F"
        Me.Text = "【LMG050】  請求処理 請求書作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlSeikyu.ResumeLayout(False)
        Me.pnlSeikyu.PerformLayout()
        CType(Me.sprSeikyuM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprSeikyuM_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSeikyuK.ResumeLayout(False)
        Me.pnlSeikyuK.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSeikyuNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeikyuCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeikyuCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlSeikyu As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleSeikyuMeigi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkHokan As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleSeikyuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSeikyuNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSincyoku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSikyuMeigi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeikyuNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkYokomochi As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkSagyou As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkUnchin As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkNiyaku As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblSeikyuNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdInvDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblCreateNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCreateNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTorikomi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprSeikyuM As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleSeiqShubetsu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSeiqtShubetu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents btnDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents numCalAllM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numCalAllH As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numCalAllU As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numCalAllK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numRateNebikigakuK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numRateNebikigakuM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNebikiRateM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNebikiRateK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numZeigakuU As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numZeigakuK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNebikiGakuM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNebikiGakuK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSeikyuGakuU As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSeikyuGakuH As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSeikyuGakuM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSeikyuGakuK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numZeiHasuK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSeikyuAll As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleCalAllK As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNebikiGaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleRateNebikigaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleZeigaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleZeiHasu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSeikyuGaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSeikyuAll As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeigakuM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleMenzei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleHikazei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUchizei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKazei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeiHasuM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblNebikiRateH As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblRateNebikigakuH As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblNebikiGakuH As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeigakuH As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeiHasuH As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblNebikiRateU As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblRateNebikigakuU As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblNebikiGakuU As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblZeiHasuU As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlSeikyuK As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkHikaeAri As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkKeiHikaeAri As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkSubAri As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkMainAri As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleBiko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeikyuTantoNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSeikyuTantoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents chkAkaden As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkTemplate As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblUnchinImpDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyoImpDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblYokomochiImpDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblMaxEdaban As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSeikyuNoRelated As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numExRate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblCurrencyConversion As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblRate1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleExRate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitleSeiqtCurrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbStateKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbSeiqCurrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbCurrencyConversion2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbCurrencyConversion1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblUnsoUT As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnsoWT As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoWT As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents btnSapCancel As Win.LMButton
    Friend WithEvents btnSapOut As Win.LMButton
    Friend WithEvents lblSapNo As Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSapNo As Win.LMTitleLabel
    Friend WithEvents sprSeikyuM_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents chkDepotLift As Win.LMCheckBox
    Friend WithEvents chkDepotHokan As Win.LMCheckBox
    Friend WithEvents chkContainerUnso As Win.LMCheckBox
End Class
