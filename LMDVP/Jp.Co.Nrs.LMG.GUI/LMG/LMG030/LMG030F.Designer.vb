<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMG030F
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
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprMeisaiPrt_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblJobNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleJobNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.grpShuturyoku = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblInvDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoodsCdCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblGoodsCdNrs = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblGoodsCdCust = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblGoodsNm1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleLotNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbNbUt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleNbUt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlIrimeInfo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleIrimeUt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleIrimeNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbIrimeUt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblIrimeNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleTaxKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTaxKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.pnlHokanInfo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.numHokanAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHokanAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numHokanTnk3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHokanTnk3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numHokanTnk2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHokanTnk2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numHokanTnk1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleHokanTnk1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleHokanTnk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSekiNb3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSekiNb3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSekiNb2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSekiNb2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSekiNb1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSekiNb1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSekiNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlNiyakuInfo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.numNiyakuOutTnk3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuOutTnk3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNiyakuOutTnk2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuOutTnk2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNiyakuOutTnk1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuOutTnk1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleNiyakuOutTnk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numOutNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleOutNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleNiyakuAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNiyakuInTnk3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numNiyakuAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuInTnk3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNiyakuInTnk2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuInTnk2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numNiyakuInTnk1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleNiyakuInTnk1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleNiyakuInTnk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numInNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleInNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprMeisaiPrt = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.sprMeisaiPrt_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.chkMeisaiPrev = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.lblSerialNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSerialNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNm2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCdM2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCdL2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCdSS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblInkaNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSysUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCtlNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.grpIKKATU = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.btnIkkatu = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.numIkkatu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.cmbIkkatu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblIkkatu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numVarHokanAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprMeisaiPrt_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.grpShuturyoku.SuspendLayout()
        Me.pnlIrimeInfo.SuspendLayout()
        Me.pnlHokanInfo.SuspendLayout()
        Me.pnlNiyakuInfo.SuspendLayout()
        CType(Me.sprMeisaiPrt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprMeisaiPrt_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpIKKATU.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpIKKATU)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.chkMeisaiPrev)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.lblCtlNo)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblInkaNo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.lblCustNm2)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdSS)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdS)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdM2)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdL2)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.lblSerialNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSerialNo)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsCdNrs)
        Me.pnlViewAria.Controls.Add(Me.sprMeisaiPrt)
        Me.pnlViewAria.Controls.Add(Me.pnlNiyakuInfo)
        Me.pnlViewAria.Controls.Add(Me.pnlHokanInfo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTaxKb)
        Me.pnlViewAria.Controls.Add(Me.cmbTaxKb)
        Me.pnlViewAria.Controls.Add(Me.pnlIrimeInfo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNbUt)
        Me.pnlViewAria.Controls.Add(Me.cmbNbUt)
        Me.pnlViewAria.Controls.Add(Me.lblLotNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleLotNo)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNm1)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsCdCust)
        Me.pnlViewAria.Controls.Add(Me.lblTitleGoodsCdCust)
        Me.pnlViewAria.Controls.Add(Me.grpShuturyoku)
        Me.pnlViewAria.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'lblCustNm
        '
        Me.lblCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNm.CountWrappedLine = False
        Me.lblCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNm.HeightDef = 18
        Me.lblCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.HissuLabelVisible = False
        Me.lblCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNm.IsByteCheck = 0
        Me.lblCustNm.IsCalendarCheck = False
        Me.lblCustNm.IsDakutenCheck = False
        Me.lblCustNm.IsEisuCheck = False
        Me.lblCustNm.IsForbiddenWordsCheck = False
        Me.lblCustNm.IsFullByteCheck = 0
        Me.lblCustNm.IsHankakuCheck = False
        Me.lblCustNm.IsHissuCheck = False
        Me.lblCustNm.IsKanaCheck = False
        Me.lblCustNm.IsMiddleSpace = False
        Me.lblCustNm.IsNumericCheck = False
        Me.lblCustNm.IsSujiCheck = False
        Me.lblCustNm.IsZenkakuCheck = False
        Me.lblCustNm.ItemName = ""
        Me.lblCustNm.LineSpace = 0
        Me.lblCustNm.Location = New System.Drawing.Point(223, 45)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNm.TabIndex = 126
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 513
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblJobNo
        '
        Me.lblJobNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJobNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJobNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblJobNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblJobNo.CountWrappedLine = False
        Me.lblJobNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblJobNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJobNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJobNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJobNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblJobNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblJobNo.HeightDef = 18
        Me.lblJobNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblJobNo.HissuLabelVisible = False
        Me.lblJobNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblJobNo.IsByteCheck = 10
        Me.lblJobNo.IsCalendarCheck = False
        Me.lblJobNo.IsDakutenCheck = False
        Me.lblJobNo.IsEisuCheck = False
        Me.lblJobNo.IsForbiddenWordsCheck = False
        Me.lblJobNo.IsFullByteCheck = 0
        Me.lblJobNo.IsHankakuCheck = False
        Me.lblJobNo.IsHissuCheck = False
        Me.lblJobNo.IsKanaCheck = False
        Me.lblJobNo.IsMiddleSpace = False
        Me.lblJobNo.IsNumericCheck = False
        Me.lblJobNo.IsSujiCheck = False
        Me.lblJobNo.IsZenkakuCheck = False
        Me.lblJobNo.ItemName = ""
        Me.lblJobNo.LineSpace = 0
        Me.lblJobNo.Location = New System.Drawing.Point(125, 18)
        Me.lblJobNo.MaxLength = 10
        Me.lblJobNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblJobNo.MaxLineCount = 0
        Me.lblJobNo.Multiline = False
        Me.lblJobNo.Name = "lblJobNo"
        Me.lblJobNo.ReadOnly = True
        Me.lblJobNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblJobNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblJobNo.Size = New System.Drawing.Size(92, 18)
        Me.lblJobNo.TabIndex = 129
        Me.lblJobNo.TabStop = False
        Me.lblJobNo.TabStopSetting = False
        Me.lblJobNo.TextValue = ""
        Me.lblJobNo.UseSystemPasswordChar = False
        Me.lblJobNo.WidthDef = 92
        Me.lblJobNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleJobNo
        '
        Me.lblTitleJobNo.AutoSize = True
        Me.lblTitleJobNo.AutoSizeDef = True
        Me.lblTitleJobNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJobNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJobNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJobNo.EnableStatus = False
        Me.lblTitleJobNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJobNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJobNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJobNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJobNo.HeightDef = 13
        Me.lblTitleJobNo.Location = New System.Drawing.Point(64, 21)
        Me.lblTitleJobNo.Name = "lblTitleJobNo"
        Me.lblTitleJobNo.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleJobNo.TabIndex = 128
        Me.lblTitleJobNo.Text = "JOB番号"
        Me.lblTitleJobNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJobNo.TextValue = "JOB番号"
        Me.lblTitleJobNo.WidthDef = 56
        '
        'lblCustCdM
        '
        Me.lblCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdM.CountWrappedLine = False
        Me.lblCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdM.HeightDef = 18
        Me.lblCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.HissuLabelVisible = False
        Me.lblCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdM.IsByteCheck = 7
        Me.lblCustCdM.IsCalendarCheck = False
        Me.lblCustCdM.IsDakutenCheck = False
        Me.lblCustCdM.IsEisuCheck = False
        Me.lblCustCdM.IsForbiddenWordsCheck = False
        Me.lblCustCdM.IsFullByteCheck = 0
        Me.lblCustCdM.IsHankakuCheck = False
        Me.lblCustCdM.IsHissuCheck = False
        Me.lblCustCdM.IsKanaCheck = False
        Me.lblCustCdM.IsMiddleSpace = False
        Me.lblCustCdM.IsNumericCheck = False
        Me.lblCustCdM.IsSujiCheck = False
        Me.lblCustCdM.IsZenkakuCheck = False
        Me.lblCustCdM.ItemName = ""
        Me.lblCustCdM.LineSpace = 0
        Me.lblCustCdM.Location = New System.Drawing.Point(191, 45)
        Me.lblCustCdM.MaxLength = 7
        Me.lblCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdM.MaxLineCount = 0
        Me.lblCustCdM.Multiline = False
        Me.lblCustCdM.Name = "lblCustCdM"
        Me.lblCustCdM.ReadOnly = True
        Me.lblCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdM.Size = New System.Drawing.Size(51, 18)
        Me.lblCustCdM.TabIndex = 127
        Me.lblCustCdM.TabStop = False
        Me.lblCustCdM.TabStopSetting = False
        Me.lblCustCdM.TextValue = ""
        Me.lblCustCdM.UseSystemPasswordChar = False
        Me.lblCustCdM.WidthDef = 51
        Me.lblCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCustCd
        '
        Me.lblTitleCustCd.AutoSize = True
        Me.lblTitleCustCd.AutoSizeDef = True
        Me.lblTitleCustCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCustCd.EnableStatus = False
        Me.lblTitleCustCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.HeightDef = 13
        Me.lblTitleCustCd.Location = New System.Drawing.Point(85, 48)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustCd.TabIndex = 124
        Me.lblTitleCustCd.Text = "荷主"
        Me.lblTitleCustCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustCd.TextValue = "荷主"
        Me.lblTitleCustCd.WidthDef = 35
        '
        'lblCustCdL
        '
        Me.lblCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdL.CountWrappedLine = False
        Me.lblCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdL.HeightDef = 18
        Me.lblCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.HissuLabelVisible = False
        Me.lblCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdL.IsByteCheck = 7
        Me.lblCustCdL.IsCalendarCheck = False
        Me.lblCustCdL.IsDakutenCheck = False
        Me.lblCustCdL.IsEisuCheck = False
        Me.lblCustCdL.IsForbiddenWordsCheck = False
        Me.lblCustCdL.IsFullByteCheck = 0
        Me.lblCustCdL.IsHankakuCheck = False
        Me.lblCustCdL.IsHissuCheck = False
        Me.lblCustCdL.IsKanaCheck = False
        Me.lblCustCdL.IsMiddleSpace = False
        Me.lblCustCdL.IsNumericCheck = False
        Me.lblCustCdL.IsSujiCheck = False
        Me.lblCustCdL.IsZenkakuCheck = False
        Me.lblCustCdL.ItemName = ""
        Me.lblCustCdL.LineSpace = 0
        Me.lblCustCdL.Location = New System.Drawing.Point(125, 45)
        Me.lblCustCdL.MaxLength = 7
        Me.lblCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdL.MaxLineCount = 0
        Me.lblCustCdL.Multiline = False
        Me.lblCustCdL.Name = "lblCustCdL"
        Me.lblCustCdL.ReadOnly = True
        Me.lblCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdL.Size = New System.Drawing.Size(82, 18)
        Me.lblCustCdL.TabIndex = 125
        Me.lblCustCdL.TabStop = False
        Me.lblCustCdL.TabStopSetting = False
        Me.lblCustCdL.TextValue = ""
        Me.lblCustCdL.UseSystemPasswordChar = False
        Me.lblCustCdL.WidthDef = 82
        Me.lblCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpShuturyoku
        '
        Me.grpShuturyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpShuturyoku.Controls.Add(Me.lblCustNm)
        Me.grpShuturyoku.Controls.Add(Me.lblCustCdM)
        Me.grpShuturyoku.Controls.Add(Me.lblCustCdL)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleCustCd)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleJobNo)
        Me.grpShuturyoku.Controls.Add(Me.lblInvDateTo)
        Me.grpShuturyoku.Controls.Add(Me.lblNrsBrCd)
        Me.grpShuturyoku.Controls.Add(Me.lblJobNo)
        Me.grpShuturyoku.EnableStatus = False
        Me.grpShuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.HeightDef = 75
        Me.grpShuturyoku.Location = New System.Drawing.Point(12, 10)
        Me.grpShuturyoku.Name = "grpShuturyoku"
        Me.grpShuturyoku.Size = New System.Drawing.Size(847, 75)
        Me.grpShuturyoku.TabIndex = 138
        Me.grpShuturyoku.TabStop = False
        Me.grpShuturyoku.Text = "抽出条件"
        Me.grpShuturyoku.TextValue = "抽出条件"
        Me.grpShuturyoku.WidthDef = 847
        '
        'lblInvDateTo
        '
        Me.lblInvDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInvDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInvDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInvDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInvDateTo.CountWrappedLine = False
        Me.lblInvDateTo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblInvDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInvDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInvDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInvDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInvDateTo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblInvDateTo.HeightDef = 18
        Me.lblInvDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInvDateTo.HissuLabelVisible = False
        Me.lblInvDateTo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblInvDateTo.IsByteCheck = 8
        Me.lblInvDateTo.IsCalendarCheck = False
        Me.lblInvDateTo.IsDakutenCheck = False
        Me.lblInvDateTo.IsEisuCheck = False
        Me.lblInvDateTo.IsForbiddenWordsCheck = False
        Me.lblInvDateTo.IsFullByteCheck = 0
        Me.lblInvDateTo.IsHankakuCheck = False
        Me.lblInvDateTo.IsHissuCheck = False
        Me.lblInvDateTo.IsKanaCheck = False
        Me.lblInvDateTo.IsMiddleSpace = False
        Me.lblInvDateTo.IsNumericCheck = False
        Me.lblInvDateTo.IsSujiCheck = False
        Me.lblInvDateTo.IsZenkakuCheck = False
        Me.lblInvDateTo.ItemName = ""
        Me.lblInvDateTo.LineSpace = 0
        Me.lblInvDateTo.Location = New System.Drawing.Point(448, 18)
        Me.lblInvDateTo.MaxLength = 8
        Me.lblInvDateTo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblInvDateTo.MaxLineCount = 0
        Me.lblInvDateTo.Multiline = False
        Me.lblInvDateTo.Name = "lblInvDateTo"
        Me.lblInvDateTo.ReadOnly = True
        Me.lblInvDateTo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblInvDateTo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblInvDateTo.Size = New System.Drawing.Size(82, 18)
        Me.lblInvDateTo.TabIndex = 129
        Me.lblInvDateTo.TabStop = False
        Me.lblInvDateTo.TabStopSetting = False
        Me.lblInvDateTo.TextValue = ""
        Me.lblInvDateTo.UseSystemPasswordChar = False
        Me.lblInvDateTo.Visible = False
        Me.lblInvDateTo.WidthDef = 82
        Me.lblInvDateTo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblNrsBrCd
        '
        Me.lblNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNrsBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNrsBrCd.CountWrappedLine = False
        Me.lblNrsBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNrsBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblNrsBrCd.HeightDef = 18
        Me.lblNrsBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNrsBrCd.HissuLabelVisible = False
        Me.lblNrsBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblNrsBrCd.IsByteCheck = 7
        Me.lblNrsBrCd.IsCalendarCheck = False
        Me.lblNrsBrCd.IsDakutenCheck = False
        Me.lblNrsBrCd.IsEisuCheck = False
        Me.lblNrsBrCd.IsForbiddenWordsCheck = False
        Me.lblNrsBrCd.IsFullByteCheck = 0
        Me.lblNrsBrCd.IsHankakuCheck = False
        Me.lblNrsBrCd.IsHissuCheck = False
        Me.lblNrsBrCd.IsKanaCheck = False
        Me.lblNrsBrCd.IsMiddleSpace = False
        Me.lblNrsBrCd.IsNumericCheck = False
        Me.lblNrsBrCd.IsSujiCheck = False
        Me.lblNrsBrCd.IsZenkakuCheck = False
        Me.lblNrsBrCd.ItemName = ""
        Me.lblNrsBrCd.LineSpace = 0
        Me.lblNrsBrCd.Location = New System.Drawing.Point(364, 18)
        Me.lblNrsBrCd.MaxLength = 7
        Me.lblNrsBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblNrsBrCd.MaxLineCount = 0
        Me.lblNrsBrCd.Multiline = False
        Me.lblNrsBrCd.Name = "lblNrsBrCd"
        Me.lblNrsBrCd.ReadOnly = True
        Me.lblNrsBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblNrsBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblNrsBrCd.Size = New System.Drawing.Size(82, 18)
        Me.lblNrsBrCd.TabIndex = 129
        Me.lblNrsBrCd.TabStop = False
        Me.lblNrsBrCd.TabStopSetting = False
        Me.lblNrsBrCd.TextValue = ""
        Me.lblNrsBrCd.UseSystemPasswordChar = False
        Me.lblNrsBrCd.Visible = False
        Me.lblNrsBrCd.WidthDef = 82
        Me.lblNrsBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsCdCust
        '
        Me.lblTitleGoodsCdCust.AutoSize = True
        Me.lblTitleGoodsCdCust.AutoSizeDef = True
        Me.lblTitleGoodsCdCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCdCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCdCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsCdCust.EnableStatus = False
        Me.lblTitleGoodsCdCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCdCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCdCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCdCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCdCust.HeightDef = 13
        Me.lblTitleGoodsCdCust.Location = New System.Drawing.Point(160, 625)
        Me.lblTitleGoodsCdCust.Name = "lblTitleGoodsCdCust"
        Me.lblTitleGoodsCdCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoodsCdCust.TabIndex = 260
        Me.lblTitleGoodsCdCust.Text = "商品"
        Me.lblTitleGoodsCdCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsCdCust.TextValue = "商品"
        Me.lblTitleGoodsCdCust.WidthDef = 35
        '
        'lblGoodsCdNrs
        '
        Me.lblGoodsCdNrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsCdNrs.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCdNrs.CountWrappedLine = False
        Me.lblGoodsCdNrs.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsCdNrs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrs.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdNrs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrs.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdNrs.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsCdNrs.HeightDef = 18
        Me.lblGoodsCdNrs.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdNrs.HissuLabelVisible = False
        Me.lblGoodsCdNrs.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsCdNrs.IsByteCheck = 20
        Me.lblGoodsCdNrs.IsCalendarCheck = False
        Me.lblGoodsCdNrs.IsDakutenCheck = False
        Me.lblGoodsCdNrs.IsEisuCheck = False
        Me.lblGoodsCdNrs.IsForbiddenWordsCheck = False
        Me.lblGoodsCdNrs.IsFullByteCheck = 0
        Me.lblGoodsCdNrs.IsHankakuCheck = False
        Me.lblGoodsCdNrs.IsHissuCheck = False
        Me.lblGoodsCdNrs.IsKanaCheck = False
        Me.lblGoodsCdNrs.IsMiddleSpace = False
        Me.lblGoodsCdNrs.IsNumericCheck = False
        Me.lblGoodsCdNrs.IsSujiCheck = False
        Me.lblGoodsCdNrs.IsZenkakuCheck = False
        Me.lblGoodsCdNrs.ItemName = ""
        Me.lblGoodsCdNrs.LineSpace = 0
        Me.lblGoodsCdNrs.Location = New System.Drawing.Point(876, 620)
        Me.lblGoodsCdNrs.MaxLength = 20
        Me.lblGoodsCdNrs.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsCdNrs.MaxLineCount = 0
        Me.lblGoodsCdNrs.Multiline = False
        Me.lblGoodsCdNrs.Name = "lblGoodsCdNrs"
        Me.lblGoodsCdNrs.ReadOnly = True
        Me.lblGoodsCdNrs.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsCdNrs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsCdNrs.Size = New System.Drawing.Size(167, 18)
        Me.lblGoodsCdNrs.TabIndex = 132
        Me.lblGoodsCdNrs.TabStop = False
        Me.lblGoodsCdNrs.TabStopSetting = False
        Me.lblGoodsCdNrs.TextValue = "XXXXXXXXXXXXXXXXXXXX"
        Me.lblGoodsCdNrs.UseSystemPasswordChar = False
        Me.lblGoodsCdNrs.WidthDef = 167
        Me.lblGoodsCdNrs.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsCdCust
        '
        Me.lblGoodsCdCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdCust.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsCdCust.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCdCust.CountWrappedLine = False
        Me.lblGoodsCdCust.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsCdCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCdCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCdCust.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsCdCust.HeightDef = 18
        Me.lblGoodsCdCust.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCdCust.HissuLabelVisible = False
        Me.lblGoodsCdCust.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lblGoodsCdCust.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsCdCust.IsByteCheck = 20
        Me.lblGoodsCdCust.IsCalendarCheck = False
        Me.lblGoodsCdCust.IsDakutenCheck = False
        Me.lblGoodsCdCust.IsEisuCheck = False
        Me.lblGoodsCdCust.IsForbiddenWordsCheck = False
        Me.lblGoodsCdCust.IsFullByteCheck = 0
        Me.lblGoodsCdCust.IsHankakuCheck = False
        Me.lblGoodsCdCust.IsHissuCheck = False
        Me.lblGoodsCdCust.IsKanaCheck = False
        Me.lblGoodsCdCust.IsMiddleSpace = False
        Me.lblGoodsCdCust.IsNumericCheck = False
        Me.lblGoodsCdCust.IsSujiCheck = False
        Me.lblGoodsCdCust.IsZenkakuCheck = False
        Me.lblGoodsCdCust.ItemName = ""
        Me.lblGoodsCdCust.LineSpace = 0
        Me.lblGoodsCdCust.Location = New System.Drawing.Point(210, 620)
        Me.lblGoodsCdCust.MaxLength = 20
        Me.lblGoodsCdCust.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsCdCust.MaxLineCount = 0
        Me.lblGoodsCdCust.Multiline = False
        Me.lblGoodsCdCust.Name = "lblGoodsCdCust"
        Me.lblGoodsCdCust.ReadOnly = True
        Me.lblGoodsCdCust.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsCdCust.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsCdCust.Size = New System.Drawing.Size(185, 18)
        Me.lblGoodsCdCust.TabIndex = 264
        Me.lblGoodsCdCust.TabStop = False
        Me.lblGoodsCdCust.TabStopSetting = False
        Me.lblGoodsCdCust.TextValue = "XXXXXXXXXXXXXXXXXXXX"
        Me.lblGoodsCdCust.UseSystemPasswordChar = False
        Me.lblGoodsCdCust.WidthDef = 185
        Me.lblGoodsCdCust.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsNm1
        '
        Me.lblGoodsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNm1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNm1.CountWrappedLine = False
        Me.lblGoodsNm1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNm1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNm1.HeightDef = 18
        Me.lblGoodsNm1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm1.HissuLabelVisible = False
        Me.lblGoodsNm1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsNm1.IsByteCheck = 0
        Me.lblGoodsNm1.IsCalendarCheck = False
        Me.lblGoodsNm1.IsDakutenCheck = False
        Me.lblGoodsNm1.IsEisuCheck = False
        Me.lblGoodsNm1.IsForbiddenWordsCheck = False
        Me.lblGoodsNm1.IsFullByteCheck = 0
        Me.lblGoodsNm1.IsHankakuCheck = False
        Me.lblGoodsNm1.IsHissuCheck = False
        Me.lblGoodsNm1.IsKanaCheck = False
        Me.lblGoodsNm1.IsMiddleSpace = False
        Me.lblGoodsNm1.IsNumericCheck = False
        Me.lblGoodsNm1.IsSujiCheck = False
        Me.lblGoodsNm1.IsZenkakuCheck = False
        Me.lblGoodsNm1.ItemName = ""
        Me.lblGoodsNm1.LineSpace = 0
        Me.lblGoodsNm1.Location = New System.Drawing.Point(379, 620)
        Me.lblGoodsNm1.MaxLength = 0
        Me.lblGoodsNm1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNm1.MaxLineCount = 0
        Me.lblGoodsNm1.Multiline = False
        Me.lblGoodsNm1.Name = "lblGoodsNm1"
        Me.lblGoodsNm1.ReadOnly = True
        Me.lblGoodsNm1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNm1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNm1.Size = New System.Drawing.Size(513, 18)
        Me.lblGoodsNm1.TabIndex = 132
        Me.lblGoodsNm1.TabStop = False
        Me.lblGoodsNm1.TabStopSetting = False
        Me.lblGoodsNm1.TextValue = ""
        Me.lblGoodsNm1.UseSystemPasswordChar = False
        Me.lblGoodsNm1.WidthDef = 513
        Me.lblGoodsNm1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblLotNo
        '
        Me.lblLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLotNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLotNo.CountWrappedLine = False
        Me.lblLotNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblLotNo.HeightDef = 18
        Me.lblLotNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.HissuLabelVisible = False
        Me.lblLotNo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.lblLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblLotNo.IsByteCheck = 0
        Me.lblLotNo.IsCalendarCheck = False
        Me.lblLotNo.IsDakutenCheck = False
        Me.lblLotNo.IsEisuCheck = False
        Me.lblLotNo.IsForbiddenWordsCheck = False
        Me.lblLotNo.IsFullByteCheck = 0
        Me.lblLotNo.IsHankakuCheck = False
        Me.lblLotNo.IsHissuCheck = False
        Me.lblLotNo.IsKanaCheck = False
        Me.lblLotNo.IsMiddleSpace = False
        Me.lblLotNo.IsNumericCheck = False
        Me.lblLotNo.IsSujiCheck = False
        Me.lblLotNo.IsZenkakuCheck = False
        Me.lblLotNo.ItemName = ""
        Me.lblLotNo.LineSpace = 0
        Me.lblLotNo.Location = New System.Drawing.Point(210, 643)
        Me.lblLotNo.MaxLength = 0
        Me.lblLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblLotNo.MaxLineCount = 0
        Me.lblLotNo.Multiline = False
        Me.lblLotNo.Name = "lblLotNo"
        Me.lblLotNo.ReadOnly = True
        Me.lblLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblLotNo.Size = New System.Drawing.Size(332, 18)
        Me.lblLotNo.TabIndex = 266
        Me.lblLotNo.TabStop = False
        Me.lblLotNo.TabStopSetting = False
        Me.lblLotNo.TextValue = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
        Me.lblLotNo.UseSystemPasswordChar = False
        Me.lblLotNo.WidthDef = 332
        Me.lblLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleLotNo
        '
        Me.lblTitleLotNo.AutoSize = True
        Me.lblTitleLotNo.AutoSizeDef = True
        Me.lblTitleLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLotNo.EnableStatus = False
        Me.lblTitleLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.HeightDef = 13
        Me.lblTitleLotNo.Location = New System.Drawing.Point(132, 646)
        Me.lblTitleLotNo.Name = "lblTitleLotNo"
        Me.lblTitleLotNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleLotNo.TabIndex = 265
        Me.lblTitleLotNo.Text = "ロット№"
        Me.lblTitleLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLotNo.TextValue = "ロット№"
        Me.lblTitleLotNo.WidthDef = 63
        '
        'cmbNbUt
        '
        Me.cmbNbUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNbUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNbUt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNbUt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNbUt.DataCode = "K002"
        Me.cmbNbUt.DataSource = Nothing
        Me.cmbNbUt.DisplayMember = Nothing
        Me.cmbNbUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNbUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNbUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNbUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNbUt.HeightDef = 20
        Me.cmbNbUt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNbUt.HissuLabelVisible = False
        Me.cmbNbUt.InsertWildCard = True
        Me.cmbNbUt.IsForbiddenWordsCheck = False
        Me.cmbNbUt.IsHissuCheck = False
        Me.cmbNbUt.ItemName = ""
        Me.cmbNbUt.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNbUt.Location = New System.Drawing.Point(210, 667)
        Me.cmbNbUt.Name = "cmbNbUt"
        Me.cmbNbUt.ReadOnly = True
        Me.cmbNbUt.SelectedIndex = -1
        Me.cmbNbUt.SelectedItem = Nothing
        Me.cmbNbUt.SelectedText = ""
        Me.cmbNbUt.SelectedValue = ""
        Me.cmbNbUt.Size = New System.Drawing.Size(160, 20)
        Me.cmbNbUt.TabIndex = 268
        Me.cmbNbUt.TabStop = False
        Me.cmbNbUt.TabStopSetting = False
        Me.cmbNbUt.TextValue = ""
        Me.cmbNbUt.Value1 = Nothing
        Me.cmbNbUt.Value2 = Nothing
        Me.cmbNbUt.Value3 = Nothing
        Me.cmbNbUt.ValueMember = Nothing
        Me.cmbNbUt.WidthDef = 160
        '
        'lblTitleNbUt
        '
        Me.lblTitleNbUt.AutoSize = True
        Me.lblTitleNbUt.AutoSizeDef = True
        Me.lblTitleNbUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNbUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNbUt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNbUt.EnableStatus = False
        Me.lblTitleNbUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNbUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNbUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNbUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNbUt.HeightDef = 13
        Me.lblTitleNbUt.Location = New System.Drawing.Point(160, 671)
        Me.lblTitleNbUt.Name = "lblTitleNbUt"
        Me.lblTitleNbUt.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleNbUt.TabIndex = 269
        Me.lblTitleNbUt.Text = "単位"
        Me.lblTitleNbUt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNbUt.TextValue = "単位"
        Me.lblTitleNbUt.WidthDef = 35
        '
        'pnlIrimeInfo
        '
        Me.pnlIrimeInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlIrimeInfo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlIrimeInfo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlIrimeInfo.Controls.Add(Me.lblTitleIrimeUt)
        Me.pnlIrimeInfo.Controls.Add(Me.lblTitleIrimeNb)
        Me.pnlIrimeInfo.Controls.Add(Me.cmbIrimeUt)
        Me.pnlIrimeInfo.Controls.Add(Me.lblIrimeNb)
        Me.pnlIrimeInfo.EnableStatus = False
        Me.pnlIrimeInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlIrimeInfo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlIrimeInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlIrimeInfo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlIrimeInfo.HeightDef = 38
        Me.pnlIrimeInfo.Location = New System.Drawing.Point(364, 664)
        Me.pnlIrimeInfo.Name = "pnlIrimeInfo"
        Me.pnlIrimeInfo.Size = New System.Drawing.Size(455, 38)
        Me.pnlIrimeInfo.TabIndex = 372
        Me.pnlIrimeInfo.TabStop = False
        Me.pnlIrimeInfo.Text = "入目"
        Me.pnlIrimeInfo.TextValue = "入目"
        Me.pnlIrimeInfo.WidthDef = 455
        '
        'lblTitleIrimeUt
        '
        Me.lblTitleIrimeUt.AutoSize = True
        Me.lblTitleIrimeUt.AutoSizeDef = True
        Me.lblTitleIrimeUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeUt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrimeUt.EnableStatus = False
        Me.lblTitleIrimeUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeUt.HeightDef = 13
        Me.lblTitleIrimeUt.Location = New System.Drawing.Point(254, 16)
        Me.lblTitleIrimeUt.Name = "lblTitleIrimeUt"
        Me.lblTitleIrimeUt.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleIrimeUt.TabIndex = 374
        Me.lblTitleIrimeUt.Text = "単位"
        Me.lblTitleIrimeUt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrimeUt.TextValue = "単位"
        Me.lblTitleIrimeUt.WidthDef = 35
        '
        'lblTitleIrimeNb
        '
        Me.lblTitleIrimeNb.AutoSize = True
        Me.lblTitleIrimeNb.AutoSizeDef = True
        Me.lblTitleIrimeNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIrimeNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIrimeNb.EnableStatus = False
        Me.lblTitleIrimeNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIrimeNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIrimeNb.HeightDef = 13
        Me.lblTitleIrimeNb.Location = New System.Drawing.Point(42, 16)
        Me.lblTitleIrimeNb.Name = "lblTitleIrimeNb"
        Me.lblTitleIrimeNb.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleIrimeNb.TabIndex = 373
        Me.lblTitleIrimeNb.Text = "数量"
        Me.lblTitleIrimeNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIrimeNb.TextValue = "数量"
        Me.lblTitleIrimeNb.WidthDef = 35
        '
        'cmbIrimeUt
        '
        Me.cmbIrimeUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIrimeUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIrimeUt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbIrimeUt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbIrimeUt.DataCode = "I001"
        Me.cmbIrimeUt.DataSource = Nothing
        Me.cmbIrimeUt.DisplayMember = Nothing
        Me.cmbIrimeUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIrimeUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIrimeUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIrimeUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIrimeUt.HeightDef = 18
        Me.cmbIrimeUt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIrimeUt.HissuLabelVisible = False
        Me.cmbIrimeUt.InsertWildCard = True
        Me.cmbIrimeUt.IsForbiddenWordsCheck = False
        Me.cmbIrimeUt.IsHissuCheck = False
        Me.cmbIrimeUt.ItemName = ""
        Me.cmbIrimeUt.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbIrimeUt.Location = New System.Drawing.Point(299, 13)
        Me.cmbIrimeUt.Name = "cmbIrimeUt"
        Me.cmbIrimeUt.ReadOnly = True
        Me.cmbIrimeUt.SelectedIndex = -1
        Me.cmbIrimeUt.SelectedItem = Nothing
        Me.cmbIrimeUt.SelectedText = ""
        Me.cmbIrimeUt.SelectedValue = ""
        Me.cmbIrimeUt.Size = New System.Drawing.Size(135, 18)
        Me.cmbIrimeUt.TabIndex = 373
        Me.cmbIrimeUt.TabStop = False
        Me.cmbIrimeUt.TabStopSetting = False
        Me.cmbIrimeUt.TextValue = ""
        Me.cmbIrimeUt.Value1 = Nothing
        Me.cmbIrimeUt.Value2 = Nothing
        Me.cmbIrimeUt.Value3 = Nothing
        Me.cmbIrimeUt.ValueMember = Nothing
        Me.cmbIrimeUt.WidthDef = 135
        '
        'lblIrimeNb
        '
        Me.lblIrimeNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIrimeNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrimeNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrimeNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrimeNb.HeightDef = 18
        Me.lblIrimeNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeNb.HissuLabelVisible = False
        Me.lblIrimeNb.IsHissuCheck = False
        Me.lblIrimeNb.IsRangeCheck = False
        Me.lblIrimeNb.ItemName = ""
        Me.lblIrimeNb.Location = New System.Drawing.Point(87, 14)
        Me.lblIrimeNb.Name = "lblIrimeNb"
        Me.lblIrimeNb.ReadOnly = True
        Me.lblIrimeNb.Size = New System.Drawing.Size(136, 18)
        Me.lblIrimeNb.TabIndex = 372
        Me.lblIrimeNb.TabStop = False
        Me.lblIrimeNb.TabStopSetting = False
        Me.lblIrimeNb.TextValue = "0"
        Me.lblIrimeNb.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblIrimeNb.WidthDef = 136
        '
        'lblTitleTaxKb
        '
        Me.lblTitleTaxKb.AutoSizeDef = False
        Me.lblTitleTaxKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTaxKb.EnableStatus = False
        Me.lblTitleTaxKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKb.HeightDef = 18
        Me.lblTitleTaxKb.Location = New System.Drawing.Point(105, 687)
        Me.lblTitleTaxKb.Name = "lblTitleTaxKb"
        Me.lblTitleTaxKb.Size = New System.Drawing.Size(90, 18)
        Me.lblTitleTaxKb.TabIndex = 374
        Me.lblTitleTaxKb.Text = "課税区分"
        Me.lblTitleTaxKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTaxKb.TextValue = "課税区分"
        Me.lblTitleTaxKb.WidthDef = 90
        '
        'cmbTaxKb
        '
        Me.cmbTaxKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTaxKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTaxKb.DataCode = "Z001"
        Me.cmbTaxKb.DataSource = Nothing
        Me.cmbTaxKb.DisplayMember = Nothing
        Me.cmbTaxKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKb.HeightDef = 18
        Me.cmbTaxKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKb.HissuLabelVisible = False
        Me.cmbTaxKb.InsertWildCard = True
        Me.cmbTaxKb.IsForbiddenWordsCheck = False
        Me.cmbTaxKb.IsHissuCheck = False
        Me.cmbTaxKb.ItemName = ""
        Me.cmbTaxKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTaxKb.Location = New System.Drawing.Point(210, 690)
        Me.cmbTaxKb.Name = "cmbTaxKb"
        Me.cmbTaxKb.ReadOnly = True
        Me.cmbTaxKb.SelectedIndex = -1
        Me.cmbTaxKb.SelectedItem = Nothing
        Me.cmbTaxKb.SelectedText = ""
        Me.cmbTaxKb.SelectedValue = ""
        Me.cmbTaxKb.Size = New System.Drawing.Size(121, 18)
        Me.cmbTaxKb.TabIndex = 373
        Me.cmbTaxKb.TabStop = False
        Me.cmbTaxKb.TabStopSetting = False
        Me.cmbTaxKb.TextValue = ""
        Me.cmbTaxKb.Value1 = Nothing
        Me.cmbTaxKb.Value2 = Nothing
        Me.cmbTaxKb.Value3 = Nothing
        Me.cmbTaxKb.ValueMember = Nothing
        Me.cmbTaxKb.WidthDef = 121
        '
        'pnlHokanInfo
        '
        Me.pnlHokanInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHokanInfo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHokanInfo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlHokanInfo.Controls.Add(Me.LmTitleLabel3)
        Me.pnlHokanInfo.Controls.Add(Me.numVarHokanAmt)
        Me.pnlHokanInfo.Controls.Add(Me.numHokanAmt)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleHokanAmt)
        Me.pnlHokanInfo.Controls.Add(Me.numHokanTnk3)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleHokanTnk3)
        Me.pnlHokanInfo.Controls.Add(Me.numHokanTnk2)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleHokanTnk2)
        Me.pnlHokanInfo.Controls.Add(Me.numHokanTnk1)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleHokanTnk1)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleHokanTnk)
        Me.pnlHokanInfo.Controls.Add(Me.numSekiNb3)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleSekiNb3)
        Me.pnlHokanInfo.Controls.Add(Me.numSekiNb2)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleSekiNb2)
        Me.pnlHokanInfo.Controls.Add(Me.numSekiNb1)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleSekiNb1)
        Me.pnlHokanInfo.Controls.Add(Me.lblTitleSekiNb)
        Me.pnlHokanInfo.EnableStatus = False
        Me.pnlHokanInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHokanInfo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHokanInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHokanInfo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHokanInfo.HeightDef = 54
        Me.pnlHokanInfo.Location = New System.Drawing.Point(40, 708)
        Me.pnlHokanInfo.Name = "pnlHokanInfo"
        Me.pnlHokanInfo.Size = New System.Drawing.Size(1201, 54)
        Me.pnlHokanInfo.TabIndex = 375
        Me.pnlHokanInfo.TabStop = False
        Me.pnlHokanInfo.Text = "保管料"
        Me.pnlHokanInfo.TextValue = "保管料"
        Me.pnlHokanInfo.WidthDef = 1201
        '
        'numHokanAmt
        '
        Me.numHokanAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHokanAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHokanAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanAmt.HeightDef = 18
        Me.numHokanAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHokanAmt.HissuLabelVisible = False
        Me.numHokanAmt.IsHissuCheck = False
        Me.numHokanAmt.IsRangeCheck = False
        Me.numHokanAmt.ItemName = ""
        Me.numHokanAmt.Location = New System.Drawing.Point(920, 11)
        Me.numHokanAmt.Name = "numHokanAmt"
        Me.numHokanAmt.ReadOnly = False
        Me.numHokanAmt.Size = New System.Drawing.Size(228, 18)
        Me.numHokanAmt.TabIndex = 403
        Me.numHokanAmt.TabStopSetting = True
        Me.numHokanAmt.TextValue = "0"
        Me.numHokanAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHokanAmt.WidthDef = 228
        '
        'lblTitleHokanAmt
        '
        Me.lblTitleHokanAmt.AutoSize = True
        Me.lblTitleHokanAmt.AutoSizeDef = True
        Me.lblTitleHokanAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanAmt.EnableStatus = False
        Me.lblTitleHokanAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanAmt.HeightDef = 13
        Me.lblTitleHokanAmt.Location = New System.Drawing.Point(824, 31)
        Me.lblTitleHokanAmt.Name = "lblTitleHokanAmt"
        Me.lblTitleHokanAmt.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleHokanAmt.TabIndex = 376
        Me.lblTitleHokanAmt.Text = "変動保管料"
        Me.lblTitleHokanAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanAmt.TextValue = "変動保管料"
        Me.lblTitleHokanAmt.WidthDef = 77
        '
        'numHokanTnk3
        '
        Me.numHokanTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHokanTnk3.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHokanTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk3.HeightDef = 18
        Me.numHokanTnk3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHokanTnk3.HissuLabelVisible = False
        Me.numHokanTnk3.IsHissuCheck = False
        Me.numHokanTnk3.IsRangeCheck = False
        Me.numHokanTnk3.ItemName = ""
        Me.numHokanTnk3.Location = New System.Drawing.Point(589, 31)
        Me.numHokanTnk3.Name = "numHokanTnk3"
        Me.numHokanTnk3.ReadOnly = False
        Me.numHokanTnk3.Size = New System.Drawing.Size(135, 18)
        Me.numHokanTnk3.TabIndex = 387
        Me.numHokanTnk3.TabStopSetting = True
        Me.numHokanTnk3.TextValue = "0"
        Me.numHokanTnk3.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHokanTnk3.WidthDef = 135
        '
        'lblTitleHokanTnk3
        '
        Me.lblTitleHokanTnk3.AutoSize = True
        Me.lblTitleHokanTnk3.AutoSizeDef = True
        Me.lblTitleHokanTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanTnk3.EnableStatus = False
        Me.lblTitleHokanTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk3.HeightDef = 13
        Me.lblTitleHokanTnk3.Location = New System.Drawing.Point(548, 34)
        Me.lblTitleHokanTnk3.Name = "lblTitleHokanTnk3"
        Me.lblTitleHokanTnk3.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleHokanTnk3.TabIndex = 386
        Me.lblTitleHokanTnk3.Text = "3期"
        Me.lblTitleHokanTnk3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanTnk3.TextValue = "3期"
        Me.lblTitleHokanTnk3.WidthDef = 28
        '
        'numHokanTnk2
        '
        Me.numHokanTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHokanTnk2.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHokanTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk2.HeightDef = 18
        Me.numHokanTnk2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHokanTnk2.HissuLabelVisible = False
        Me.numHokanTnk2.IsHissuCheck = False
        Me.numHokanTnk2.IsRangeCheck = False
        Me.numHokanTnk2.ItemName = ""
        Me.numHokanTnk2.Location = New System.Drawing.Point(377, 31)
        Me.numHokanTnk2.Name = "numHokanTnk2"
        Me.numHokanTnk2.ReadOnly = False
        Me.numHokanTnk2.Size = New System.Drawing.Size(135, 18)
        Me.numHokanTnk2.TabIndex = 385
        Me.numHokanTnk2.TabStopSetting = True
        Me.numHokanTnk2.TextValue = "0"
        Me.numHokanTnk2.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHokanTnk2.WidthDef = 135
        '
        'lblTitleHokanTnk2
        '
        Me.lblTitleHokanTnk2.AutoSize = True
        Me.lblTitleHokanTnk2.AutoSizeDef = True
        Me.lblTitleHokanTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanTnk2.EnableStatus = False
        Me.lblTitleHokanTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk2.HeightDef = 13
        Me.lblTitleHokanTnk2.Location = New System.Drawing.Point(336, 34)
        Me.lblTitleHokanTnk2.Name = "lblTitleHokanTnk2"
        Me.lblTitleHokanTnk2.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleHokanTnk2.TabIndex = 384
        Me.lblTitleHokanTnk2.Text = "2期"
        Me.lblTitleHokanTnk2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanTnk2.TextValue = "2期"
        Me.lblTitleHokanTnk2.WidthDef = 28
        '
        'numHokanTnk1
        '
        Me.numHokanTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numHokanTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHokanTnk1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHokanTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHokanTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHokanTnk1.HeightDef = 18
        Me.numHokanTnk1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHokanTnk1.HissuLabelVisible = False
        Me.numHokanTnk1.IsHissuCheck = False
        Me.numHokanTnk1.IsRangeCheck = False
        Me.numHokanTnk1.ItemName = ""
        Me.numHokanTnk1.Location = New System.Drawing.Point(170, 31)
        Me.numHokanTnk1.Name = "numHokanTnk1"
        Me.numHokanTnk1.ReadOnly = False
        Me.numHokanTnk1.Size = New System.Drawing.Size(135, 18)
        Me.numHokanTnk1.TabIndex = 383
        Me.numHokanTnk1.TabStopSetting = True
        Me.numHokanTnk1.TextValue = "0"
        Me.numHokanTnk1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHokanTnk1.WidthDef = 135
        '
        'lblTitleHokanTnk1
        '
        Me.lblTitleHokanTnk1.AutoSize = True
        Me.lblTitleHokanTnk1.AutoSizeDef = True
        Me.lblTitleHokanTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanTnk1.EnableStatus = False
        Me.lblTitleHokanTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk1.HeightDef = 13
        Me.lblTitleHokanTnk1.Location = New System.Drawing.Point(127, 34)
        Me.lblTitleHokanTnk1.Name = "lblTitleHokanTnk1"
        Me.lblTitleHokanTnk1.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleHokanTnk1.TabIndex = 381
        Me.lblTitleHokanTnk1.Text = "1期"
        Me.lblTitleHokanTnk1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanTnk1.TextValue = "1期"
        Me.lblTitleHokanTnk1.WidthDef = 28
        '
        'lblTitleHokanTnk
        '
        Me.lblTitleHokanTnk.AutoSize = True
        Me.lblTitleHokanTnk.AutoSizeDef = True
        Me.lblTitleHokanTnk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokanTnk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokanTnk.EnableStatus = False
        Me.lblTitleHokanTnk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokanTnk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokanTnk.HeightDef = 13
        Me.lblTitleHokanTnk.Location = New System.Drawing.Point(64, 34)
        Me.lblTitleHokanTnk.Name = "lblTitleHokanTnk"
        Me.lblTitleHokanTnk.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleHokanTnk.TabIndex = 382
        Me.lblTitleHokanTnk.Text = "単価"
        Me.lblTitleHokanTnk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokanTnk.TextValue = "単価"
        Me.lblTitleHokanTnk.WidthDef = 35
        '
        'numSekiNb3
        '
        Me.numSekiNb3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSekiNb3.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSekiNb3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb3.HeightDef = 18
        Me.numSekiNb3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSekiNb3.HissuLabelVisible = False
        Me.numSekiNb3.IsHissuCheck = False
        Me.numSekiNb3.IsRangeCheck = False
        Me.numSekiNb3.ItemName = ""
        Me.numSekiNb3.Location = New System.Drawing.Point(589, 11)
        Me.numSekiNb3.Name = "numSekiNb3"
        Me.numSekiNb3.ReadOnly = False
        Me.numSekiNb3.Size = New System.Drawing.Size(135, 18)
        Me.numSekiNb3.TabIndex = 380
        Me.numSekiNb3.TabStopSetting = True
        Me.numSekiNb3.TextValue = "0"
        Me.numSekiNb3.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSekiNb3.WidthDef = 135
        '
        'lblTitleSekiNb3
        '
        Me.lblTitleSekiNb3.AutoSize = True
        Me.lblTitleSekiNb3.AutoSizeDef = True
        Me.lblTitleSekiNb3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSekiNb3.EnableStatus = False
        Me.lblTitleSekiNb3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb3.HeightDef = 13
        Me.lblTitleSekiNb3.Location = New System.Drawing.Point(548, 14)
        Me.lblTitleSekiNb3.Name = "lblTitleSekiNb3"
        Me.lblTitleSekiNb3.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleSekiNb3.TabIndex = 379
        Me.lblTitleSekiNb3.Text = "3期"
        Me.lblTitleSekiNb3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSekiNb3.TextValue = "3期"
        Me.lblTitleSekiNb3.WidthDef = 28
        '
        'numSekiNb2
        '
        Me.numSekiNb2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSekiNb2.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSekiNb2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb2.HeightDef = 18
        Me.numSekiNb2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSekiNb2.HissuLabelVisible = False
        Me.numSekiNb2.IsHissuCheck = False
        Me.numSekiNb2.IsRangeCheck = False
        Me.numSekiNb2.ItemName = ""
        Me.numSekiNb2.Location = New System.Drawing.Point(377, 11)
        Me.numSekiNb2.Name = "numSekiNb2"
        Me.numSekiNb2.ReadOnly = False
        Me.numSekiNb2.Size = New System.Drawing.Size(135, 18)
        Me.numSekiNb2.TabIndex = 378
        Me.numSekiNb2.TabStopSetting = True
        Me.numSekiNb2.TextValue = "0"
        Me.numSekiNb2.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSekiNb2.WidthDef = 135
        '
        'lblTitleSekiNb2
        '
        Me.lblTitleSekiNb2.AutoSize = True
        Me.lblTitleSekiNb2.AutoSizeDef = True
        Me.lblTitleSekiNb2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSekiNb2.EnableStatus = False
        Me.lblTitleSekiNb2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb2.HeightDef = 13
        Me.lblTitleSekiNb2.Location = New System.Drawing.Point(336, 14)
        Me.lblTitleSekiNb2.Name = "lblTitleSekiNb2"
        Me.lblTitleSekiNb2.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleSekiNb2.TabIndex = 377
        Me.lblTitleSekiNb2.Text = "2期"
        Me.lblTitleSekiNb2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSekiNb2.TextValue = "2期"
        Me.lblTitleSekiNb2.WidthDef = 28
        '
        'numSekiNb1
        '
        Me.numSekiNb1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSekiNb1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSekiNb1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSekiNb1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSekiNb1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSekiNb1.HeightDef = 18
        Me.numSekiNb1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSekiNb1.HissuLabelVisible = False
        Me.numSekiNb1.IsHissuCheck = False
        Me.numSekiNb1.IsRangeCheck = False
        Me.numSekiNb1.ItemName = ""
        Me.numSekiNb1.Location = New System.Drawing.Point(170, 11)
        Me.numSekiNb1.Name = "numSekiNb1"
        Me.numSekiNb1.ReadOnly = False
        Me.numSekiNb1.Size = New System.Drawing.Size(135, 18)
        Me.numSekiNb1.TabIndex = 376
        Me.numSekiNb1.TabStopSetting = True
        Me.numSekiNb1.TextValue = "0"
        Me.numSekiNb1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSekiNb1.WidthDef = 135
        '
        'lblTitleSekiNb1
        '
        Me.lblTitleSekiNb1.AutoSize = True
        Me.lblTitleSekiNb1.AutoSizeDef = True
        Me.lblTitleSekiNb1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSekiNb1.EnableStatus = False
        Me.lblTitleSekiNb1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb1.HeightDef = 13
        Me.lblTitleSekiNb1.Location = New System.Drawing.Point(127, 14)
        Me.lblTitleSekiNb1.Name = "lblTitleSekiNb1"
        Me.lblTitleSekiNb1.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleSekiNb1.TabIndex = 373
        Me.lblTitleSekiNb1.Text = "1期"
        Me.lblTitleSekiNb1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSekiNb1.TextValue = "1期"
        Me.lblTitleSekiNb1.WidthDef = 28
        '
        'lblTitleSekiNb
        '
        Me.lblTitleSekiNb.AutoSize = True
        Me.lblTitleSekiNb.AutoSizeDef = True
        Me.lblTitleSekiNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSekiNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSekiNb.EnableStatus = False
        Me.lblTitleSekiNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSekiNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSekiNb.HeightDef = 13
        Me.lblTitleSekiNb.Location = New System.Drawing.Point(64, 14)
        Me.lblTitleSekiNb.Name = "lblTitleSekiNb"
        Me.lblTitleSekiNb.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSekiNb.TabIndex = 373
        Me.lblTitleSekiNb.Text = "積数"
        Me.lblTitleSekiNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSekiNb.TextValue = "積数"
        Me.lblTitleSekiNb.WidthDef = 35
        '
        'pnlNiyakuInfo
        '
        Me.pnlNiyakuInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNiyakuInfo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNiyakuInfo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuOutTnk3)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuOutTnk3)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuOutTnk2)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuOutTnk2)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuOutTnk1)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuOutTnk1)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuOutTnk)
        Me.pnlNiyakuInfo.Controls.Add(Me.numOutNb)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleOutNb)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuAmt)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuInTnk3)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuAmt)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuInTnk3)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuInTnk2)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuInTnk2)
        Me.pnlNiyakuInfo.Controls.Add(Me.numNiyakuInTnk1)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuInTnk1)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleNiyakuInTnk)
        Me.pnlNiyakuInfo.Controls.Add(Me.numInNb)
        Me.pnlNiyakuInfo.Controls.Add(Me.lblTitleInNb)
        Me.pnlNiyakuInfo.EnableStatus = False
        Me.pnlNiyakuInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNiyakuInfo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNiyakuInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNiyakuInfo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNiyakuInfo.HeightDef = 94
        Me.pnlNiyakuInfo.Location = New System.Drawing.Point(40, 771)
        Me.pnlNiyakuInfo.Name = "pnlNiyakuInfo"
        Me.pnlNiyakuInfo.Size = New System.Drawing.Size(1201, 94)
        Me.pnlNiyakuInfo.TabIndex = 388
        Me.pnlNiyakuInfo.TabStop = False
        Me.pnlNiyakuInfo.Text = "荷役料"
        Me.pnlNiyakuInfo.TextValue = "荷役料"
        Me.pnlNiyakuInfo.WidthDef = 1201
        '
        'numNiyakuOutTnk3
        '
        Me.numNiyakuOutTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuOutTnk3.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuOutTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk3.HeightDef = 18
        Me.numNiyakuOutTnk3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuOutTnk3.HissuLabelVisible = False
        Me.numNiyakuOutTnk3.IsHissuCheck = False
        Me.numNiyakuOutTnk3.IsRangeCheck = False
        Me.numNiyakuOutTnk3.ItemName = ""
        Me.numNiyakuOutTnk3.Location = New System.Drawing.Point(589, 71)
        Me.numNiyakuOutTnk3.Name = "numNiyakuOutTnk3"
        Me.numNiyakuOutTnk3.ReadOnly = False
        Me.numNiyakuOutTnk3.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuOutTnk3.TabIndex = 397
        Me.numNiyakuOutTnk3.TabStopSetting = True
        Me.numNiyakuOutTnk3.TextValue = "0"
        Me.numNiyakuOutTnk3.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuOutTnk3.WidthDef = 135
        '
        'lblTitleNiyakuOutTnk3
        '
        Me.lblTitleNiyakuOutTnk3.AutoSize = True
        Me.lblTitleNiyakuOutTnk3.AutoSizeDef = True
        Me.lblTitleNiyakuOutTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuOutTnk3.EnableStatus = False
        Me.lblTitleNiyakuOutTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk3.HeightDef = 13
        Me.lblTitleNiyakuOutTnk3.Location = New System.Drawing.Point(548, 74)
        Me.lblTitleNiyakuOutTnk3.Name = "lblTitleNiyakuOutTnk3"
        Me.lblTitleNiyakuOutTnk3.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuOutTnk3.TabIndex = 396
        Me.lblTitleNiyakuOutTnk3.Text = "3期"
        Me.lblTitleNiyakuOutTnk3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuOutTnk3.TextValue = "3期"
        Me.lblTitleNiyakuOutTnk3.WidthDef = 28
        '
        'numNiyakuOutTnk2
        '
        Me.numNiyakuOutTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuOutTnk2.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuOutTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk2.HeightDef = 18
        Me.numNiyakuOutTnk2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuOutTnk2.HissuLabelVisible = False
        Me.numNiyakuOutTnk2.IsHissuCheck = False
        Me.numNiyakuOutTnk2.IsRangeCheck = False
        Me.numNiyakuOutTnk2.ItemName = ""
        Me.numNiyakuOutTnk2.Location = New System.Drawing.Point(377, 71)
        Me.numNiyakuOutTnk2.Name = "numNiyakuOutTnk2"
        Me.numNiyakuOutTnk2.ReadOnly = False
        Me.numNiyakuOutTnk2.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuOutTnk2.TabIndex = 395
        Me.numNiyakuOutTnk2.TabStopSetting = True
        Me.numNiyakuOutTnk2.TextValue = "0"
        Me.numNiyakuOutTnk2.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuOutTnk2.WidthDef = 135
        '
        'lblTitleNiyakuOutTnk2
        '
        Me.lblTitleNiyakuOutTnk2.AutoSize = True
        Me.lblTitleNiyakuOutTnk2.AutoSizeDef = True
        Me.lblTitleNiyakuOutTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuOutTnk2.EnableStatus = False
        Me.lblTitleNiyakuOutTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk2.HeightDef = 13
        Me.lblTitleNiyakuOutTnk2.Location = New System.Drawing.Point(336, 74)
        Me.lblTitleNiyakuOutTnk2.Name = "lblTitleNiyakuOutTnk2"
        Me.lblTitleNiyakuOutTnk2.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuOutTnk2.TabIndex = 394
        Me.lblTitleNiyakuOutTnk2.Text = "2期"
        Me.lblTitleNiyakuOutTnk2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuOutTnk2.TextValue = "2期"
        Me.lblTitleNiyakuOutTnk2.WidthDef = 28
        '
        'numNiyakuOutTnk1
        '
        Me.numNiyakuOutTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuOutTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuOutTnk1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuOutTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuOutTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuOutTnk1.HeightDef = 18
        Me.numNiyakuOutTnk1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuOutTnk1.HissuLabelVisible = False
        Me.numNiyakuOutTnk1.IsHissuCheck = False
        Me.numNiyakuOutTnk1.IsRangeCheck = False
        Me.numNiyakuOutTnk1.ItemName = ""
        Me.numNiyakuOutTnk1.Location = New System.Drawing.Point(170, 71)
        Me.numNiyakuOutTnk1.Name = "numNiyakuOutTnk1"
        Me.numNiyakuOutTnk1.ReadOnly = False
        Me.numNiyakuOutTnk1.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuOutTnk1.TabIndex = 393
        Me.numNiyakuOutTnk1.TabStopSetting = True
        Me.numNiyakuOutTnk1.TextValue = "0"
        Me.numNiyakuOutTnk1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuOutTnk1.WidthDef = 135
        '
        'lblTitleNiyakuOutTnk1
        '
        Me.lblTitleNiyakuOutTnk1.AutoSize = True
        Me.lblTitleNiyakuOutTnk1.AutoSizeDef = True
        Me.lblTitleNiyakuOutTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuOutTnk1.EnableStatus = False
        Me.lblTitleNiyakuOutTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk1.HeightDef = 13
        Me.lblTitleNiyakuOutTnk1.Location = New System.Drawing.Point(127, 74)
        Me.lblTitleNiyakuOutTnk1.Name = "lblTitleNiyakuOutTnk1"
        Me.lblTitleNiyakuOutTnk1.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuOutTnk1.TabIndex = 391
        Me.lblTitleNiyakuOutTnk1.Text = "1期"
        Me.lblTitleNiyakuOutTnk1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuOutTnk1.TextValue = "1期"
        Me.lblTitleNiyakuOutTnk1.WidthDef = 28
        '
        'lblTitleNiyakuOutTnk
        '
        Me.lblTitleNiyakuOutTnk.AutoSize = True
        Me.lblTitleNiyakuOutTnk.AutoSizeDef = True
        Me.lblTitleNiyakuOutTnk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuOutTnk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuOutTnk.EnableStatus = False
        Me.lblTitleNiyakuOutTnk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuOutTnk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuOutTnk.HeightDef = 13
        Me.lblTitleNiyakuOutTnk.Location = New System.Drawing.Point(22, 73)
        Me.lblTitleNiyakuOutTnk.Name = "lblTitleNiyakuOutTnk"
        Me.lblTitleNiyakuOutTnk.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleNiyakuOutTnk.TabIndex = 392
        Me.lblTitleNiyakuOutTnk.Text = "(出庫)単価"
        Me.lblTitleNiyakuOutTnk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuOutTnk.TextValue = "(出庫)単価"
        Me.lblTitleNiyakuOutTnk.WidthDef = 77
        '
        'numOutNb
        '
        Me.numOutNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOutNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOutNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numOutNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numOutNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOutNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOutNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOutNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOutNb.HeightDef = 18
        Me.numOutNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOutNb.HissuLabelVisible = False
        Me.numOutNb.IsHissuCheck = False
        Me.numOutNb.IsRangeCheck = False
        Me.numOutNb.ItemName = ""
        Me.numOutNb.Location = New System.Drawing.Point(170, 51)
        Me.numOutNb.Name = "numOutNb"
        Me.numOutNb.ReadOnly = False
        Me.numOutNb.Size = New System.Drawing.Size(135, 18)
        Me.numOutNb.TabIndex = 390
        Me.numOutNb.TabStopSetting = True
        Me.numOutNb.TextValue = "0"
        Me.numOutNb.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numOutNb.WidthDef = 135
        '
        'lblTitleOutNb
        '
        Me.lblTitleOutNb.AutoSize = True
        Me.lblTitleOutNb.AutoSizeDef = True
        Me.lblTitleOutNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOutNb.EnableStatus = False
        Me.lblTitleOutNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutNb.HeightDef = 13
        Me.lblTitleOutNb.Location = New System.Drawing.Point(106, 54)
        Me.lblTitleOutNb.Name = "lblTitleOutNb"
        Me.lblTitleOutNb.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleOutNb.TabIndex = 389
        Me.lblTitleOutNb.Text = "出庫高"
        Me.lblTitleOutNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOutNb.TextValue = "出庫高"
        Me.lblTitleOutNb.WidthDef = 49
        '
        'lblTitleNiyakuAmt
        '
        Me.lblTitleNiyakuAmt.AutoSize = True
        Me.lblTitleNiyakuAmt.AutoSizeDef = True
        Me.lblTitleNiyakuAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuAmt.EnableStatus = False
        Me.lblTitleNiyakuAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuAmt.HeightDef = 13
        Me.lblTitleNiyakuAmt.Location = New System.Drawing.Point(852, 73)
        Me.lblTitleNiyakuAmt.Name = "lblTitleNiyakuAmt"
        Me.lblTitleNiyakuAmt.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNiyakuAmt.TabIndex = 376
        Me.lblTitleNiyakuAmt.Text = "荷役料"
        Me.lblTitleNiyakuAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuAmt.TextValue = "荷役料"
        Me.lblTitleNiyakuAmt.WidthDef = 49
        '
        'numNiyakuInTnk3
        '
        Me.numNiyakuInTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuInTnk3.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuInTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk3.HeightDef = 18
        Me.numNiyakuInTnk3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuInTnk3.HissuLabelVisible = False
        Me.numNiyakuInTnk3.IsHissuCheck = False
        Me.numNiyakuInTnk3.IsRangeCheck = False
        Me.numNiyakuInTnk3.ItemName = ""
        Me.numNiyakuInTnk3.Location = New System.Drawing.Point(589, 31)
        Me.numNiyakuInTnk3.Name = "numNiyakuInTnk3"
        Me.numNiyakuInTnk3.ReadOnly = False
        Me.numNiyakuInTnk3.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuInTnk3.TabIndex = 387
        Me.numNiyakuInTnk3.TabStopSetting = True
        Me.numNiyakuInTnk3.TextValue = "0"
        Me.numNiyakuInTnk3.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuInTnk3.WidthDef = 135
        '
        'numNiyakuAmt
        '
        Me.numNiyakuAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuAmt.HeightDef = 18
        Me.numNiyakuAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuAmt.HissuLabelVisible = False
        Me.numNiyakuAmt.IsHissuCheck = False
        Me.numNiyakuAmt.IsRangeCheck = False
        Me.numNiyakuAmt.ItemName = ""
        Me.numNiyakuAmt.Location = New System.Drawing.Point(920, 70)
        Me.numNiyakuAmt.Name = "numNiyakuAmt"
        Me.numNiyakuAmt.ReadOnly = False
        Me.numNiyakuAmt.Size = New System.Drawing.Size(228, 18)
        Me.numNiyakuAmt.TabIndex = 375
        Me.numNiyakuAmt.TabStopSetting = True
        Me.numNiyakuAmt.TextValue = "0"
        Me.numNiyakuAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuAmt.WidthDef = 228
        '
        'lblTitleNiyakuInTnk3
        '
        Me.lblTitleNiyakuInTnk3.AutoSize = True
        Me.lblTitleNiyakuInTnk3.AutoSizeDef = True
        Me.lblTitleNiyakuInTnk3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuInTnk3.EnableStatus = False
        Me.lblTitleNiyakuInTnk3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk3.HeightDef = 13
        Me.lblTitleNiyakuInTnk3.Location = New System.Drawing.Point(548, 34)
        Me.lblTitleNiyakuInTnk3.Name = "lblTitleNiyakuInTnk3"
        Me.lblTitleNiyakuInTnk3.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuInTnk3.TabIndex = 386
        Me.lblTitleNiyakuInTnk3.Text = "3期"
        Me.lblTitleNiyakuInTnk3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuInTnk3.TextValue = "3期"
        Me.lblTitleNiyakuInTnk3.WidthDef = 28
        '
        'numNiyakuInTnk2
        '
        Me.numNiyakuInTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuInTnk2.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuInTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk2.HeightDef = 18
        Me.numNiyakuInTnk2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuInTnk2.HissuLabelVisible = False
        Me.numNiyakuInTnk2.IsHissuCheck = False
        Me.numNiyakuInTnk2.IsRangeCheck = False
        Me.numNiyakuInTnk2.ItemName = ""
        Me.numNiyakuInTnk2.Location = New System.Drawing.Point(377, 31)
        Me.numNiyakuInTnk2.Name = "numNiyakuInTnk2"
        Me.numNiyakuInTnk2.ReadOnly = False
        Me.numNiyakuInTnk2.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuInTnk2.TabIndex = 385
        Me.numNiyakuInTnk2.TabStopSetting = True
        Me.numNiyakuInTnk2.TextValue = "0"
        Me.numNiyakuInTnk2.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuInTnk2.WidthDef = 135
        '
        'lblTitleNiyakuInTnk2
        '
        Me.lblTitleNiyakuInTnk2.AutoSize = True
        Me.lblTitleNiyakuInTnk2.AutoSizeDef = True
        Me.lblTitleNiyakuInTnk2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuInTnk2.EnableStatus = False
        Me.lblTitleNiyakuInTnk2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk2.HeightDef = 13
        Me.lblTitleNiyakuInTnk2.Location = New System.Drawing.Point(336, 34)
        Me.lblTitleNiyakuInTnk2.Name = "lblTitleNiyakuInTnk2"
        Me.lblTitleNiyakuInTnk2.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuInTnk2.TabIndex = 384
        Me.lblTitleNiyakuInTnk2.Text = "2期"
        Me.lblTitleNiyakuInTnk2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuInTnk2.TextValue = "2期"
        Me.lblTitleNiyakuInTnk2.WidthDef = 28
        '
        'numNiyakuInTnk1
        '
        Me.numNiyakuInTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numNiyakuInTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNiyakuInTnk1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNiyakuInTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNiyakuInTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNiyakuInTnk1.HeightDef = 18
        Me.numNiyakuInTnk1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNiyakuInTnk1.HissuLabelVisible = False
        Me.numNiyakuInTnk1.IsHissuCheck = False
        Me.numNiyakuInTnk1.IsRangeCheck = False
        Me.numNiyakuInTnk1.ItemName = ""
        Me.numNiyakuInTnk1.Location = New System.Drawing.Point(170, 31)
        Me.numNiyakuInTnk1.Name = "numNiyakuInTnk1"
        Me.numNiyakuInTnk1.ReadOnly = False
        Me.numNiyakuInTnk1.Size = New System.Drawing.Size(135, 18)
        Me.numNiyakuInTnk1.TabIndex = 383
        Me.numNiyakuInTnk1.TabStopSetting = True
        Me.numNiyakuInTnk1.TextValue = "0"
        Me.numNiyakuInTnk1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNiyakuInTnk1.WidthDef = 135
        '
        'lblTitleNiyakuInTnk1
        '
        Me.lblTitleNiyakuInTnk1.AutoSize = True
        Me.lblTitleNiyakuInTnk1.AutoSizeDef = True
        Me.lblTitleNiyakuInTnk1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuInTnk1.EnableStatus = False
        Me.lblTitleNiyakuInTnk1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk1.HeightDef = 13
        Me.lblTitleNiyakuInTnk1.Location = New System.Drawing.Point(127, 34)
        Me.lblTitleNiyakuInTnk1.Name = "lblTitleNiyakuInTnk1"
        Me.lblTitleNiyakuInTnk1.Size = New System.Drawing.Size(28, 13)
        Me.lblTitleNiyakuInTnk1.TabIndex = 381
        Me.lblTitleNiyakuInTnk1.Text = "1期"
        Me.lblTitleNiyakuInTnk1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuInTnk1.TextValue = "1期"
        Me.lblTitleNiyakuInTnk1.WidthDef = 28
        '
        'lblTitleNiyakuInTnk
        '
        Me.lblTitleNiyakuInTnk.AutoSize = True
        Me.lblTitleNiyakuInTnk.AutoSizeDef = True
        Me.lblTitleNiyakuInTnk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNiyakuInTnk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNiyakuInTnk.EnableStatus = False
        Me.lblTitleNiyakuInTnk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNiyakuInTnk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNiyakuInTnk.HeightDef = 13
        Me.lblTitleNiyakuInTnk.Location = New System.Drawing.Point(22, 33)
        Me.lblTitleNiyakuInTnk.Name = "lblTitleNiyakuInTnk"
        Me.lblTitleNiyakuInTnk.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleNiyakuInTnk.TabIndex = 382
        Me.lblTitleNiyakuInTnk.Text = "(入庫)単価"
        Me.lblTitleNiyakuInTnk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNiyakuInTnk.TextValue = "(入庫)単価"
        Me.lblTitleNiyakuInTnk.WidthDef = 77
        '
        'numInNb
        '
        Me.numInNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numInNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numInNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numInNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numInNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numInNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numInNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numInNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numInNb.HeightDef = 18
        Me.numInNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numInNb.HissuLabelVisible = False
        Me.numInNb.IsHissuCheck = False
        Me.numInNb.IsRangeCheck = False
        Me.numInNb.ItemName = ""
        Me.numInNb.Location = New System.Drawing.Point(170, 11)
        Me.numInNb.Name = "numInNb"
        Me.numInNb.ReadOnly = False
        Me.numInNb.Size = New System.Drawing.Size(135, 18)
        Me.numInNb.TabIndex = 376
        Me.numInNb.TabStopSetting = True
        Me.numInNb.TextValue = "0"
        Me.numInNb.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numInNb.WidthDef = 135
        '
        'lblTitleInNb
        '
        Me.lblTitleInNb.AutoSize = True
        Me.lblTitleInNb.AutoSizeDef = True
        Me.lblTitleInNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInNb.EnableStatus = False
        Me.lblTitleInNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInNb.HeightDef = 13
        Me.lblTitleInNb.Location = New System.Drawing.Point(106, 14)
        Me.lblTitleInNb.Name = "lblTitleInNb"
        Me.lblTitleInNb.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleInNb.TabIndex = 373
        Me.lblTitleInNb.Text = "入庫高"
        Me.lblTitleInNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInNb.TextValue = "入庫高"
        Me.lblTitleInNb.WidthDef = 49
        '
        'sprMeisaiPrt
        '
        Me.sprMeisaiPrt.AccessibleDescription = ""
        Me.sprMeisaiPrt.AllowUserZoom = False
        Me.sprMeisaiPrt.AutoImeMode = False
        Me.sprMeisaiPrt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprMeisaiPrt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprMeisaiPrt.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprMeisaiPrt.CellClickEventArgs = Nothing
        Me.sprMeisaiPrt.CheckToCheckBox = True
        Me.sprMeisaiPrt.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprMeisaiPrt.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisaiPrt.EditModeReplace = True
        Me.sprMeisaiPrt.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprMeisaiPrt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt.ForeColorDef = System.Drawing.Color.Empty
        Me.sprMeisaiPrt.HeightDef = 431
        Me.sprMeisaiPrt.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprMeisaiPrt.HorizontalScrollBar.Name = ""
        Me.sprMeisaiPrt.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprMeisaiPrt.KeyboardCheckBoxOn = False
        Me.sprMeisaiPrt.Location = New System.Drawing.Point(12, 156)
        Me.sprMeisaiPrt.Name = "sprMeisaiPrt"
        Me.sprMeisaiPrt.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisaiPrt.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprMeisaiPrt_Sheet1})
        Me.sprMeisaiPrt.Size = New System.Drawing.Size(1245, 431)
        Me.sprMeisaiPrt.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprMeisaiPrt.SortColumn = True
        Me.sprMeisaiPrt.SpanColumnLock = True
        Me.sprMeisaiPrt.SpreadDoubleClicked = False
        Me.sprMeisaiPrt.TabIndex = 389
        Me.sprMeisaiPrt.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprMeisaiPrt.TextValue = Nothing
        Me.sprMeisaiPrt.UseGrouping = False
        Me.sprMeisaiPrt.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprMeisaiPrt.VerticalScrollBar.Name = ""
        Me.sprMeisaiPrt.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprMeisaiPrt.WidthDef = 1245
        sprMeisaiPrt_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprMeisaiPrt_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprMeisaiPrt.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprMeisaiPrt_InputMapWhenFocusedNormal)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.CSEStopEditing)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.OemMinus, CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.AutoSum)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprMeisaiPrt.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprMeisaiPrt_InputMapWhenAncestorOfFocusedNormal)
        Me.sprMeisaiPrt.SetViewportTopRow(0, 0, 1)
        Me.sprMeisaiPrt.SetActiveViewport(0, -1, 0)
        '
        'sprMeisaiPrt_Sheet1
        '
        Me.sprMeisaiPrt_Sheet1.Reset()
        Me.sprMeisaiPrt_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprMeisaiPrt_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprMeisaiPrt_Sheet1.RowCount = 1
        Me.sprMeisaiPrt_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprMeisaiPrt_Sheet1.Cells.Get(0, 0).BackColor = System.Drawing.SystemColors.Control
        Me.sprMeisaiPrt_Sheet1.Cells.Get(0, 0).Locked = True
        Me.sprMeisaiPrt_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprMeisaiPrt_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = " "
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(0).Label = " "
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprMeisaiPrt_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        Me.sprMeisaiPrt_Sheet1.Columns.Get(0).Label = " "
        Me.sprMeisaiPrt_Sheet1.Columns.Get(0).Width = 20.0!
        TextCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprMeisaiPrt_Sheet1.Columns.Get(1).CellType = TextCellType1
        Me.sprMeisaiPrt_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.DefaultStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprMeisaiPrt_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprMeisaiPrt_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprMeisaiPrt_Sheet1.FrozenRowCount = 1
        Me.sprMeisaiPrt_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprMeisaiPrt_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprMeisaiPrt_Sheet1.RowHeader.Cells.Get(0, 0).Value = "ｸﾘｱ"
        Me.sprMeisaiPrt_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprMeisaiPrt_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprMeisaiPrt_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprMeisaiPrt_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprMeisaiPrt_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprMeisaiPrt_Sheet1.RowHeader.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprMeisaiPrt_Sheet1.Rows.Default.Height = 18.0!
        Me.sprMeisaiPrt_Sheet1.Rows.Default.Resizable = False
        Me.sprMeisaiPrt_Sheet1.Rows.Default.Visible = True
        Me.sprMeisaiPrt_Sheet1.Rows.Get(0).BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprMeisaiPrt_Sheet1.Rows.Get(0).Label = "ｸﾘｱ"
        Me.sprMeisaiPrt_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprMeisaiPrt_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprMeisaiPrt_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprMeisaiPrt_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprMeisaiPrt_Sheet1.SheetCornerStyle.Locked = True
        Me.sprMeisaiPrt_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprMeisaiPrt_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprMeisaiPrt_Sheet1.StartingRowNumber = 0
        Me.sprMeisaiPrt_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprMeisaiPrt_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'chkMeisaiPrev
        '
        Me.chkMeisaiPrev.AutoSize = True
        Me.chkMeisaiPrev.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMeisaiPrev.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMeisaiPrev.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMeisaiPrev.EnableStatus = True
        Me.chkMeisaiPrev.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMeisaiPrev.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMeisaiPrev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMeisaiPrev.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMeisaiPrev.HeightDef = 17
        Me.chkMeisaiPrev.Location = New System.Drawing.Point(1047, 56)
        Me.chkMeisaiPrev.Name = "chkMeisaiPrev"
        Me.chkMeisaiPrev.Size = New System.Drawing.Size(138, 17)
        Me.chkMeisaiPrev.TabIndex = 390
        Me.chkMeisaiPrev.TabStopSetting = True
        Me.chkMeisaiPrev.Text = "プレビューを表示"
        Me.chkMeisaiPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMeisaiPrev.TextValue = "プレビューを表示"
        Me.chkMeisaiPrev.UseVisualStyleBackColor = True
        Me.chkMeisaiPrev.WidthDef = 138
        '
        'lblSerialNo
        '
        Me.lblSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSerialNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSerialNo.CountWrappedLine = False
        Me.lblSerialNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSerialNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSerialNo.HeightDef = 18
        Me.lblSerialNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSerialNo.HissuLabelVisible = False
        Me.lblSerialNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSerialNo.IsByteCheck = 0
        Me.lblSerialNo.IsCalendarCheck = False
        Me.lblSerialNo.IsDakutenCheck = False
        Me.lblSerialNo.IsEisuCheck = False
        Me.lblSerialNo.IsForbiddenWordsCheck = False
        Me.lblSerialNo.IsFullByteCheck = 0
        Me.lblSerialNo.IsHankakuCheck = False
        Me.lblSerialNo.IsHissuCheck = False
        Me.lblSerialNo.IsKanaCheck = False
        Me.lblSerialNo.IsMiddleSpace = False
        Me.lblSerialNo.IsNumericCheck = False
        Me.lblSerialNo.IsSujiCheck = False
        Me.lblSerialNo.IsZenkakuCheck = False
        Me.lblSerialNo.ItemName = ""
        Me.lblSerialNo.LineSpace = 0
        Me.lblSerialNo.Location = New System.Drawing.Point(629, 643)
        Me.lblSerialNo.MaxLength = 0
        Me.lblSerialNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSerialNo.MaxLineCount = 0
        Me.lblSerialNo.Multiline = False
        Me.lblSerialNo.Name = "lblSerialNo"
        Me.lblSerialNo.ReadOnly = True
        Me.lblSerialNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSerialNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSerialNo.Size = New System.Drawing.Size(332, 18)
        Me.lblSerialNo.TabIndex = 392
        Me.lblSerialNo.TabStop = False
        Me.lblSerialNo.TabStopSetting = False
        Me.lblSerialNo.TextValue = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
        Me.lblSerialNo.UseSystemPasswordChar = False
        Me.lblSerialNo.WidthDef = 332
        Me.lblSerialNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSerialNo
        '
        Me.lblTitleSerialNo.AutoSize = True
        Me.lblTitleSerialNo.AutoSizeDef = True
        Me.lblTitleSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSerialNo.EnableStatus = False
        Me.lblTitleSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.HeightDef = 13
        Me.lblTitleSerialNo.Location = New System.Drawing.Point(539, 646)
        Me.lblTitleSerialNo.Name = "lblTitleSerialNo"
        Me.lblTitleSerialNo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSerialNo.TabIndex = 391
        Me.lblTitleSerialNo.Text = "シリアル№"
        Me.lblTitleSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSerialNo.TextValue = "シリアル№"
        Me.lblTitleSerialNo.WidthDef = 77
        '
        'lblCustNm2
        '
        Me.lblCustNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNm2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNm2.CountWrappedLine = False
        Me.lblCustNm2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNm2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNm2.HeightDef = 18
        Me.lblCustNm2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm2.HissuLabelVisible = False
        Me.lblCustNm2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNm2.IsByteCheck = 0
        Me.lblCustNm2.IsCalendarCheck = False
        Me.lblCustNm2.IsDakutenCheck = False
        Me.lblCustNm2.IsEisuCheck = False
        Me.lblCustNm2.IsForbiddenWordsCheck = False
        Me.lblCustNm2.IsFullByteCheck = 0
        Me.lblCustNm2.IsHankakuCheck = False
        Me.lblCustNm2.IsHissuCheck = False
        Me.lblCustNm2.IsKanaCheck = False
        Me.lblCustNm2.IsMiddleSpace = False
        Me.lblCustNm2.IsNumericCheck = False
        Me.lblCustNm2.IsSujiCheck = False
        Me.lblCustNm2.IsZenkakuCheck = False
        Me.lblCustNm2.ItemName = ""
        Me.lblCustNm2.LineSpace = 0
        Me.lblCustNm2.Location = New System.Drawing.Point(379, 597)
        Me.lblCustNm2.MaxLength = 0
        Me.lblCustNm2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm2.MaxLineCount = 0
        Me.lblCustNm2.Multiline = False
        Me.lblCustNm2.Name = "lblCustNm2"
        Me.lblCustNm2.ReadOnly = True
        Me.lblCustNm2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm2.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNm2.TabIndex = 395
        Me.lblCustNm2.TabStop = False
        Me.lblCustNm2.TabStopSetting = False
        Me.lblCustNm2.TextValue = ""
        Me.lblCustNm2.UseSystemPasswordChar = False
        Me.lblCustNm2.WidthDef = 513
        Me.lblCustNm2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCdM2
        '
        Me.lblCustCdM2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdM2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdM2.CountWrappedLine = False
        Me.lblCustCdM2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdM2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdM2.HeightDef = 18
        Me.lblCustCdM2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM2.HissuLabelVisible = False
        Me.lblCustCdM2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdM2.IsByteCheck = 7
        Me.lblCustCdM2.IsCalendarCheck = False
        Me.lblCustCdM2.IsDakutenCheck = False
        Me.lblCustCdM2.IsEisuCheck = False
        Me.lblCustCdM2.IsForbiddenWordsCheck = False
        Me.lblCustCdM2.IsFullByteCheck = 0
        Me.lblCustCdM2.IsHankakuCheck = False
        Me.lblCustCdM2.IsHissuCheck = False
        Me.lblCustCdM2.IsKanaCheck = False
        Me.lblCustCdM2.IsMiddleSpace = False
        Me.lblCustCdM2.IsNumericCheck = False
        Me.lblCustCdM2.IsSujiCheck = False
        Me.lblCustCdM2.IsZenkakuCheck = False
        Me.lblCustCdM2.ItemName = ""
        Me.lblCustCdM2.LineSpace = 0
        Me.lblCustCdM2.Location = New System.Drawing.Point(276, 597)
        Me.lblCustCdM2.MaxLength = 7
        Me.lblCustCdM2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdM2.MaxLineCount = 0
        Me.lblCustCdM2.Multiline = False
        Me.lblCustCdM2.Name = "lblCustCdM2"
        Me.lblCustCdM2.ReadOnly = True
        Me.lblCustCdM2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdM2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdM2.Size = New System.Drawing.Size(51, 18)
        Me.lblCustCdM2.TabIndex = 396
        Me.lblCustCdM2.TabStop = False
        Me.lblCustCdM2.TabStopSetting = False
        Me.lblCustCdM2.TextValue = ""
        Me.lblCustCdM2.UseSystemPasswordChar = False
        Me.lblCustCdM2.WidthDef = 51
        Me.lblCustCdM2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCdL2
        '
        Me.lblCustCdL2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdL2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdL2.CountWrappedLine = False
        Me.lblCustCdL2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdL2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdL2.HeightDef = 18
        Me.lblCustCdL2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL2.HissuLabelVisible = False
        Me.lblCustCdL2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdL2.IsByteCheck = 7
        Me.lblCustCdL2.IsCalendarCheck = False
        Me.lblCustCdL2.IsDakutenCheck = False
        Me.lblCustCdL2.IsEisuCheck = False
        Me.lblCustCdL2.IsForbiddenWordsCheck = False
        Me.lblCustCdL2.IsFullByteCheck = 0
        Me.lblCustCdL2.IsHankakuCheck = False
        Me.lblCustCdL2.IsHissuCheck = False
        Me.lblCustCdL2.IsKanaCheck = False
        Me.lblCustCdL2.IsMiddleSpace = False
        Me.lblCustCdL2.IsNumericCheck = False
        Me.lblCustCdL2.IsSujiCheck = False
        Me.lblCustCdL2.IsZenkakuCheck = False
        Me.lblCustCdL2.ItemName = ""
        Me.lblCustCdL2.LineSpace = 0
        Me.lblCustCdL2.Location = New System.Drawing.Point(210, 597)
        Me.lblCustCdL2.MaxLength = 7
        Me.lblCustCdL2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdL2.MaxLineCount = 0
        Me.lblCustCdL2.Multiline = False
        Me.lblCustCdL2.Name = "lblCustCdL2"
        Me.lblCustCdL2.ReadOnly = True
        Me.lblCustCdL2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdL2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdL2.Size = New System.Drawing.Size(82, 18)
        Me.lblCustCdL2.TabIndex = 394
        Me.lblCustCdL2.TabStop = False
        Me.lblCustCdL2.TabStopSetting = False
        Me.lblCustCdL2.TextValue = ""
        Me.lblCustCdL2.UseSystemPasswordChar = False
        Me.lblCustCdL2.WidthDef = 82
        Me.lblCustCdL2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel1
        '
        Me.LmTitleLabel1.AutoSize = True
        Me.LmTitleLabel1.AutoSizeDef = True
        Me.LmTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel1.EnableStatus = False
        Me.LmTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.HeightDef = 13
        Me.LmTitleLabel1.Location = New System.Drawing.Point(160, 597)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel1.TabIndex = 393
        Me.LmTitleLabel1.Text = "荷主"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "荷主"
        Me.LmTitleLabel1.WidthDef = 35
        '
        'lblCustCdS
        '
        Me.lblCustCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdS.CountWrappedLine = False
        Me.lblCustCdS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdS.HeightDef = 18
        Me.lblCustCdS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdS.HissuLabelVisible = False
        Me.lblCustCdS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdS.IsByteCheck = 7
        Me.lblCustCdS.IsCalendarCheck = False
        Me.lblCustCdS.IsDakutenCheck = False
        Me.lblCustCdS.IsEisuCheck = False
        Me.lblCustCdS.IsForbiddenWordsCheck = False
        Me.lblCustCdS.IsFullByteCheck = 0
        Me.lblCustCdS.IsHankakuCheck = False
        Me.lblCustCdS.IsHissuCheck = False
        Me.lblCustCdS.IsKanaCheck = False
        Me.lblCustCdS.IsMiddleSpace = False
        Me.lblCustCdS.IsNumericCheck = False
        Me.lblCustCdS.IsSujiCheck = False
        Me.lblCustCdS.IsZenkakuCheck = False
        Me.lblCustCdS.ItemName = ""
        Me.lblCustCdS.LineSpace = 0
        Me.lblCustCdS.Location = New System.Drawing.Point(309, 597)
        Me.lblCustCdS.MaxLength = 7
        Me.lblCustCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdS.MaxLineCount = 0
        Me.lblCustCdS.Multiline = False
        Me.lblCustCdS.Name = "lblCustCdS"
        Me.lblCustCdS.ReadOnly = True
        Me.lblCustCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdS.Size = New System.Drawing.Size(51, 18)
        Me.lblCustCdS.TabIndex = 397
        Me.lblCustCdS.TabStop = False
        Me.lblCustCdS.TabStopSetting = False
        Me.lblCustCdS.TextValue = ""
        Me.lblCustCdS.UseSystemPasswordChar = False
        Me.lblCustCdS.WidthDef = 51
        Me.lblCustCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCdSS
        '
        Me.lblCustCdSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdSS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdSS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdSS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdSS.CountWrappedLine = False
        Me.lblCustCdSS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdSS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdSS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdSS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdSS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdSS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdSS.HeightDef = 18
        Me.lblCustCdSS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdSS.HissuLabelVisible = False
        Me.lblCustCdSS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCustCdSS.IsByteCheck = 7
        Me.lblCustCdSS.IsCalendarCheck = False
        Me.lblCustCdSS.IsDakutenCheck = False
        Me.lblCustCdSS.IsEisuCheck = False
        Me.lblCustCdSS.IsForbiddenWordsCheck = False
        Me.lblCustCdSS.IsFullByteCheck = 0
        Me.lblCustCdSS.IsHankakuCheck = False
        Me.lblCustCdSS.IsHissuCheck = False
        Me.lblCustCdSS.IsKanaCheck = False
        Me.lblCustCdSS.IsMiddleSpace = False
        Me.lblCustCdSS.IsNumericCheck = False
        Me.lblCustCdSS.IsSujiCheck = False
        Me.lblCustCdSS.IsZenkakuCheck = False
        Me.lblCustCdSS.ItemName = ""
        Me.lblCustCdSS.LineSpace = 0
        Me.lblCustCdSS.Location = New System.Drawing.Point(344, 597)
        Me.lblCustCdSS.MaxLength = 7
        Me.lblCustCdSS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdSS.MaxLineCount = 0
        Me.lblCustCdSS.Multiline = False
        Me.lblCustCdSS.Name = "lblCustCdSS"
        Me.lblCustCdSS.ReadOnly = True
        Me.lblCustCdSS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdSS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdSS.Size = New System.Drawing.Size(51, 18)
        Me.lblCustCdSS.TabIndex = 398
        Me.lblCustCdSS.TabStop = False
        Me.lblCustCdSS.TabStopSetting = False
        Me.lblCustCdSS.TextValue = ""
        Me.lblCustCdSS.UseSystemPasswordChar = False
        Me.lblCustCdSS.WidthDef = 51
        Me.lblCustCdSS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel2
        '
        Me.LmTitleLabel2.AutoSize = True
        Me.LmTitleLabel2.AutoSizeDef = True
        Me.LmTitleLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel2.EnableStatus = False
        Me.LmTitleLabel2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.HeightDef = 13
        Me.LmTitleLabel2.Location = New System.Drawing.Point(825, 685)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(98, 13)
        Me.LmTitleLabel2.TabIndex = 399
        Me.LmTitleLabel2.Text = "入荷管理番号L"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "入荷管理番号L"
        Me.LmTitleLabel2.WidthDef = 98
        '
        'lblInkaNo
        '
        Me.lblInkaNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInkaNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblInkaNo.CountWrappedLine = False
        Me.lblInkaNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblInkaNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblInkaNo.HeightDef = 18
        Me.lblInkaNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaNo.HissuLabelVisible = False
        Me.lblInkaNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblInkaNo.IsByteCheck = 20
        Me.lblInkaNo.IsCalendarCheck = False
        Me.lblInkaNo.IsDakutenCheck = False
        Me.lblInkaNo.IsEisuCheck = False
        Me.lblInkaNo.IsForbiddenWordsCheck = False
        Me.lblInkaNo.IsFullByteCheck = 0
        Me.lblInkaNo.IsHankakuCheck = False
        Me.lblInkaNo.IsHissuCheck = False
        Me.lblInkaNo.IsKanaCheck = False
        Me.lblInkaNo.IsMiddleSpace = False
        Me.lblInkaNo.IsNumericCheck = False
        Me.lblInkaNo.IsSujiCheck = False
        Me.lblInkaNo.IsZenkakuCheck = False
        Me.lblInkaNo.ItemName = ""
        Me.lblInkaNo.LineSpace = 0
        Me.lblInkaNo.Location = New System.Drawing.Point(929, 680)
        Me.lblInkaNo.MaxLength = 20
        Me.lblInkaNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblInkaNo.MaxLineCount = 0
        Me.lblInkaNo.Multiline = False
        Me.lblInkaNo.Name = "lblInkaNo"
        Me.lblInkaNo.ReadOnly = True
        Me.lblInkaNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblInkaNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblInkaNo.Size = New System.Drawing.Size(114, 18)
        Me.lblInkaNo.TabIndex = 400
        Me.lblInkaNo.TabStop = False
        Me.lblInkaNo.TabStopSetting = False
        Me.lblInkaNo.TextValue = "XXXXXXXXX"
        Me.lblInkaNo.UseSystemPasswordChar = False
        Me.lblInkaNo.WidthDef = 114
        Me.lblInkaNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1127, 597)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 219
        Me.lblSituation.TabStop = False
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.White
        Me.cmbPrint.BackColorDef = System.Drawing.Color.White
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "S067"
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
        Me.cmbPrint.Location = New System.Drawing.Point(892, 55)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(157, 18)
        Me.cmbPrint.TabIndex = 401
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 157
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
        Me.lblSysUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblSysUpdDate.IsByteCheck = 20
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
        Me.lblSysUpdDate.Location = New System.Drawing.Point(1127, 642)
        Me.lblSysUpdDate.MaxLength = 20
        Me.lblSysUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdDate.MaxLineCount = 0
        Me.lblSysUpdDate.Multiline = False
        Me.lblSysUpdDate.Name = "lblSysUpdDate"
        Me.lblSysUpdDate.ReadOnly = True
        Me.lblSysUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdDate.Size = New System.Drawing.Size(114, 18)
        Me.lblSysUpdDate.TabIndex = 400
        Me.lblSysUpdDate.TabStop = False
        Me.lblSysUpdDate.TabStopSetting = False
        Me.lblSysUpdDate.TextValue = ""
        Me.lblSysUpdDate.UseSystemPasswordChar = False
        Me.lblSysUpdDate.Visible = False
        Me.lblSysUpdDate.WidthDef = 114
        Me.lblSysUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblSysUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblSysUpdTime.IsByteCheck = 20
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
        Me.lblSysUpdTime.Location = New System.Drawing.Point(1127, 663)
        Me.lblSysUpdTime.MaxLength = 20
        Me.lblSysUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdTime.MaxLineCount = 0
        Me.lblSysUpdTime.Multiline = False
        Me.lblSysUpdTime.Name = "lblSysUpdTime"
        Me.lblSysUpdTime.ReadOnly = True
        Me.lblSysUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdTime.Size = New System.Drawing.Size(114, 18)
        Me.lblSysUpdTime.TabIndex = 400
        Me.lblSysUpdTime.TabStop = False
        Me.lblSysUpdTime.TabStopSetting = False
        Me.lblSysUpdTime.TextValue = ""
        Me.lblSysUpdTime.UseSystemPasswordChar = False
        Me.lblSysUpdTime.Visible = False
        Me.lblSysUpdTime.WidthDef = 114
        Me.lblSysUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCtlNo
        '
        Me.lblCtlNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCtlNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCtlNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCtlNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCtlNo.CountWrappedLine = False
        Me.lblCtlNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCtlNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCtlNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCtlNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCtlNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCtlNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCtlNo.HeightDef = 18
        Me.lblCtlNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCtlNo.HissuLabelVisible = False
        Me.lblCtlNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.lblCtlNo.IsByteCheck = 20
        Me.lblCtlNo.IsCalendarCheck = False
        Me.lblCtlNo.IsDakutenCheck = False
        Me.lblCtlNo.IsEisuCheck = False
        Me.lblCtlNo.IsForbiddenWordsCheck = False
        Me.lblCtlNo.IsFullByteCheck = 0
        Me.lblCtlNo.IsHankakuCheck = False
        Me.lblCtlNo.IsHissuCheck = False
        Me.lblCtlNo.IsKanaCheck = False
        Me.lblCtlNo.IsMiddleSpace = False
        Me.lblCtlNo.IsNumericCheck = False
        Me.lblCtlNo.IsSujiCheck = False
        Me.lblCtlNo.IsZenkakuCheck = False
        Me.lblCtlNo.ItemName = ""
        Me.lblCtlNo.LineSpace = 0
        Me.lblCtlNo.Location = New System.Drawing.Point(1127, 687)
        Me.lblCtlNo.MaxLength = 20
        Me.lblCtlNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCtlNo.MaxLineCount = 0
        Me.lblCtlNo.Multiline = False
        Me.lblCtlNo.Name = "lblCtlNo"
        Me.lblCtlNo.ReadOnly = True
        Me.lblCtlNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCtlNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCtlNo.Size = New System.Drawing.Size(114, 18)
        Me.lblCtlNo.TabIndex = 400
        Me.lblCtlNo.TabStop = False
        Me.lblCtlNo.TabStopSetting = False
        Me.lblCtlNo.TextValue = ""
        Me.lblCtlNo.UseSystemPasswordChar = False
        Me.lblCtlNo.Visible = False
        Me.lblCtlNo.WidthDef = 114
        Me.lblCtlNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.btnPrint.Location = New System.Drawing.Point(1187, 53)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 402
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
        '
        'grpIKKATU
        '
        Me.grpIKKATU.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpIKKATU.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpIKKATU.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpIKKATU.Controls.Add(Me.btnIkkatu)
        Me.grpIKKATU.Controls.Add(Me.numIkkatu)
        Me.grpIKKATU.Controls.Add(Me.cmbIkkatu)
        Me.grpIKKATU.Controls.Add(Me.lblIkkatu)
        Me.grpIKKATU.EnableStatus = False
        Me.grpIKKATU.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpIKKATU.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpIKKATU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpIKKATU.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpIKKATU.HeightDef = 46
        Me.grpIKKATU.Location = New System.Drawing.Point(12, 95)
        Me.grpIKKATU.Name = "grpIKKATU"
        Me.grpIKKATU.Size = New System.Drawing.Size(620, 46)
        Me.grpIKKATU.TabIndex = 403
        Me.grpIKKATU.TabStop = False
        Me.grpIKKATU.Text = "一括変更"
        Me.grpIKKATU.TextValue = "一括変更"
        Me.grpIKKATU.WidthDef = 620
        '
        'btnIkkatu
        '
        Me.btnIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnIkkatu.EnableStatus = True
        Me.btnIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatu.HeightDef = 22
        Me.btnIkkatu.Location = New System.Drawing.Point(486, 16)
        Me.btnIkkatu.Name = "btnIkkatu"
        Me.btnIkkatu.Size = New System.Drawing.Size(89, 22)
        Me.btnIkkatu.TabIndex = 404
        Me.btnIkkatu.TabStopSetting = True
        Me.btnIkkatu.Text = "一括変更"
        Me.btnIkkatu.TextValue = "一括変更"
        Me.btnIkkatu.UseVisualStyleBackColor = True
        Me.btnIkkatu.WidthDef = 89
        '
        'numIkkatu
        '
        Me.numIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIkkatu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIkkatu.HeightDef = 18
        Me.numIkkatu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIkkatu.HissuLabelVisible = False
        Me.numIkkatu.IsHissuCheck = False
        Me.numIkkatu.IsRangeCheck = False
        Me.numIkkatu.ItemName = ""
        Me.numIkkatu.Location = New System.Drawing.Point(252, 19)
        Me.numIkkatu.Name = "numIkkatu"
        Me.numIkkatu.ReadOnly = False
        Me.numIkkatu.Size = New System.Drawing.Size(228, 18)
        Me.numIkkatu.TabIndex = 404
        Me.numIkkatu.TabStopSetting = True
        Me.numIkkatu.TextValue = "0"
        Me.numIkkatu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIkkatu.WidthDef = 228
        '
        'cmbIkkatu
        '
        Me.cmbIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbIkkatu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbIkkatu.DataCode = "G007"
        Me.cmbIkkatu.DataSource = Nothing
        Me.cmbIkkatu.DisplayMember = Nothing
        Me.cmbIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIkkatu.HeightDef = 18
        Me.cmbIkkatu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIkkatu.HissuLabelVisible = False
        Me.cmbIkkatu.InsertWildCard = True
        Me.cmbIkkatu.IsForbiddenWordsCheck = False
        Me.cmbIkkatu.IsHissuCheck = False
        Me.cmbIkkatu.ItemName = ""
        Me.cmbIkkatu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbIkkatu.Location = New System.Drawing.Point(125, 19)
        Me.cmbIkkatu.Name = "cmbIkkatu"
        Me.cmbIkkatu.ReadOnly = False
        Me.cmbIkkatu.SelectedIndex = -1
        Me.cmbIkkatu.SelectedItem = Nothing
        Me.cmbIkkatu.SelectedText = ""
        Me.cmbIkkatu.SelectedValue = ""
        Me.cmbIkkatu.Size = New System.Drawing.Size(135, 18)
        Me.cmbIkkatu.TabIndex = 375
        Me.cmbIkkatu.TabStopSetting = True
        Me.cmbIkkatu.TextValue = ""
        Me.cmbIkkatu.Value1 = Nothing
        Me.cmbIkkatu.Value2 = Nothing
        Me.cmbIkkatu.Value3 = Nothing
        Me.cmbIkkatu.ValueMember = Nothing
        Me.cmbIkkatu.WidthDef = 135
        '
        'lblIkkatu
        '
        Me.lblIkkatu.AutoSize = True
        Me.lblIkkatu.AutoSizeDef = True
        Me.lblIkkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIkkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIkkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIkkatu.EnableStatus = False
        Me.lblIkkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIkkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIkkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIkkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIkkatu.HeightDef = 13
        Me.lblIkkatu.Location = New System.Drawing.Point(55, 21)
        Me.lblIkkatu.Name = "lblIkkatu"
        Me.lblIkkatu.Size = New System.Drawing.Size(63, 13)
        Me.lblIkkatu.TabIndex = 61
        Me.lblIkkatu.Text = "修正項目"
        Me.lblIkkatu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIkkatu.TextValue = "修正項目"
        Me.lblIkkatu.WidthDef = 63
        '
        'numVarHokanAmt
        '
        Me.numVarHokanAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numVarHokanAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numVarHokanAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numVarHokanAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numVarHokanAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numVarHokanAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numVarHokanAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numVarHokanAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numVarHokanAmt.HeightDef = 18
        Me.numVarHokanAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numVarHokanAmt.HissuLabelVisible = False
        Me.numVarHokanAmt.IsHissuCheck = False
        Me.numVarHokanAmt.IsRangeCheck = False
        Me.numVarHokanAmt.ItemName = ""
        Me.numVarHokanAmt.Location = New System.Drawing.Point(920, 31)
        Me.numVarHokanAmt.Name = "numVarHokanAmt"
        Me.numVarHokanAmt.ReadOnly = False
        Me.numVarHokanAmt.Size = New System.Drawing.Size(228, 18)
        Me.numVarHokanAmt.TabIndex = 404
        Me.numVarHokanAmt.TabStopSetting = True
        Me.numVarHokanAmt.TextValue = "0"
        Me.numVarHokanAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numVarHokanAmt.WidthDef = 228
        '
        'LmTitleLabel3
        '
        Me.LmTitleLabel3.AutoSize = True
        Me.LmTitleLabel3.AutoSizeDef = True
        Me.LmTitleLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel3.EnableStatus = False
        Me.LmTitleLabel3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel3.HeightDef = 13
        Me.LmTitleLabel3.Location = New System.Drawing.Point(852, 14)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 405
        Me.LmTitleLabel3.Text = "保管料"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "保管料"
        Me.LmTitleLabel3.WidthDef = 49
        '
        'LMG030F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMG030F"
        Me.Text = "【LMG030】 保管料荷役料明細編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpShuturyoku.ResumeLayout(False)
        Me.grpShuturyoku.PerformLayout()
        Me.pnlIrimeInfo.ResumeLayout(False)
        Me.pnlIrimeInfo.PerformLayout()
        Me.pnlHokanInfo.ResumeLayout(False)
        Me.pnlHokanInfo.PerformLayout()
        Me.pnlNiyakuInfo.ResumeLayout(False)
        Me.pnlNiyakuInfo.PerformLayout()
        CType(Me.sprMeisaiPrt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprMeisaiPrt_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpIKKATU.ResumeLayout(False)
        Me.grpIKKATU.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblJobNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleJobNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents grpShuturyoku As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleGoodsCdCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblGoodsCdNrs As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsCdCust As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsNm1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblLotNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleLotNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNbUt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleNbUt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlIrimeInfo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleIrimeNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrimeNb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleIrimeUt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbIrimeUt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTaxKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTaxKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents pnlHokanInfo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleSekiNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSekiNb1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSekiNb1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHokanTnk3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHokanTnk3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHokanTnk2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHokanTnk2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHokanTnk1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleHokanTnk1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleHokanTnk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSekiNb3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSekiNb3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSekiNb2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSekiNb2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleHokanAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlNiyakuInfo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleNiyakuAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuInTnk3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numNiyakuAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuInTnk3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuInTnk2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuInTnk2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuInTnk1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuInTnk1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNiyakuInTnk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numInNb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleInNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuOutTnk3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuOutTnk3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuOutTnk2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuOutTnk2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numNiyakuOutTnk1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleNiyakuOutTnk1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNiyakuOutTnk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numOutNb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleOutNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprMeisaiPrt As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents chkMeisaiPrev As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblSerialNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSerialNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNm2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdSS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdM2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdL2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblInkaNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSysUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCtlNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblInvDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numHokanAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents grpIKKATU As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents numIkkatu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblIkkatu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbIkkatu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents btnIkkatu As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents sprMeisaiPrt_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents LmTitleLabel3 As Win.LMTitleLabel
    Friend WithEvents numVarHokanAmt As Win.InputMan.LMImNumber
End Class
