<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMH050F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

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
        Dim sprWarning_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprWarning_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType2 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprWarning = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprWarning_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.LmImTextBox1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleOrder_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblHikiate_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel60 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel67 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmImNumber1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTARE_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel68 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel72 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel73 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtHasu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel74 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel78 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKosu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleKonsu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmImTextBox18 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmImTextBox19 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel80 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKanriNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel81 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsComment_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel89 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblIrisuTani1_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblStdIrimeTani_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtBuyerOrdNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel92 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblIrisuTani2_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numStdIrime_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.txtOrderNO_M_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.numIrisu_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel94 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel95 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel96 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel97 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel98 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel99 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel100 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel101 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSumAnt_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numSumCnt_tabFreeM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.txtCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel21 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel56 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKomokuNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel31 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtWarning = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.grpWarning = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.pnlDest = New System.Windows.Forms.Panel()
        Me.txtDestWarning = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel12 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestJisM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestJisE = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestTelM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestTelE = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestZipM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestZipE = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestAdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestAdE = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestNmE = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtMastVal = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel17 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOrderNoM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel16 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKomokuVal = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel13 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtEdiKanriNoM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel11 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtEdiKanriNoL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel14 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOrderNoL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel15 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbNrsBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbNrsWh = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        Me.cmbShubetu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        sprWarning_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprWarning_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprWarning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprWarning_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpWarning.SuspendLayout()
        Me.pnlDest.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbShubetu)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBr)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsWh)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD_M)
        Me.pnlViewAria.Controls.Add(Me.grpWarning)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel21)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD_L)
        Me.pnlViewAria.Size = New System.Drawing.Size(1018, 626)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = " "
        Me.FunctionKey.F9ButtonName = " "
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(649, 13)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel2.TabIndex = 3
        Me.LmTitleLabel2.Text = "倉庫"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "倉庫"
        Me.LmTitleLabel2.WidthDef = 35
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(269, 13)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 3
        Me.LmTitleLabel1.Text = "営業所"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "営業所"
        Me.LmTitleLabel1.WidthDef = 49
        '
        'lblCustNM
        '
        Me.lblCustNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM.CountWrappedLine = False
        Me.lblCustNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM.HeightDef = 18
        Me.lblCustNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.HissuLabelVisible = False
        Me.lblCustNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM.IsByteCheck = 0
        Me.lblCustNM.IsCalendarCheck = False
        Me.lblCustNM.IsDakutenCheck = False
        Me.lblCustNM.IsEisuCheck = False
        Me.lblCustNM.IsForbiddenWordsCheck = False
        Me.lblCustNM.IsFullByteCheck = 0
        Me.lblCustNM.IsHankakuCheck = False
        Me.lblCustNM.IsHissuCheck = False
        Me.lblCustNM.IsKanaCheck = False
        Me.lblCustNM.IsMiddleSpace = False
        Me.lblCustNM.IsNumericCheck = False
        Me.lblCustNM.IsSujiCheck = False
        Me.lblCustNM.IsZenkakuCheck = False
        Me.lblCustNM.ItemName = ""
        Me.lblCustNM.LineSpace = 0
        Me.lblCustNM.Location = New System.Drawing.Point(174, 34)
        Me.lblCustNM.MaxLength = 0
        Me.lblCustNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM.MaxLineCount = 0
        Me.lblCustNM.Multiline = False
        Me.lblCustNM.Name = "lblCustNM"
        Me.lblCustNM.ReadOnly = True
        Me.lblCustNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNM.TabIndex = 28
        Me.lblCustNM.TabStop = False
        Me.lblCustNM.TabStopSetting = False
        Me.lblCustNM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM.UseSystemPasswordChar = False
        Me.lblCustNM.WidthDef = 473
        Me.lblCustNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCD_M
        '
        Me.txtCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_M.CountWrappedLine = False
        Me.txtCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_M.HeightDef = 18
        Me.txtCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.HissuLabelVisible = False
        Me.txtCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustCD_M.IsByteCheck = 0
        Me.txtCustCD_M.IsCalendarCheck = False
        Me.txtCustCD_M.IsDakutenCheck = False
        Me.txtCustCD_M.IsEisuCheck = False
        Me.txtCustCD_M.IsForbiddenWordsCheck = False
        Me.txtCustCD_M.IsFullByteCheck = 0
        Me.txtCustCD_M.IsHankakuCheck = False
        Me.txtCustCD_M.IsHissuCheck = False
        Me.txtCustCD_M.IsKanaCheck = False
        Me.txtCustCD_M.IsMiddleSpace = False
        Me.txtCustCD_M.IsNumericCheck = False
        Me.txtCustCD_M.IsSujiCheck = False
        Me.txtCustCD_M.IsZenkakuCheck = False
        Me.txtCustCD_M.ItemName = ""
        Me.txtCustCD_M.LineSpace = 0
        Me.txtCustCD_M.Location = New System.Drawing.Point(138, 34)
        Me.txtCustCD_M.MaxLength = 0
        Me.txtCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_M.MaxLineCount = 0
        Me.txtCustCD_M.Multiline = False
        Me.txtCustCD_M.Name = "txtCustCD_M"
        Me.txtCustCD_M.ReadOnly = True
        Me.txtCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_M.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCD_M.TabIndex = 27
        Me.txtCustCD_M.TabStop = False
        Me.txtCustCD_M.TabStopSetting = False
        Me.txtCustCD_M.TextValue = "XXX"
        Me.txtCustCD_M.UseSystemPasswordChar = False
        Me.txtCustCD_M.WidthDef = 52
        Me.txtCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSize = True
        Me.LmTitleLabel9.AutoSizeDef = True
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 13
        Me.LmTitleLabel9.Location = New System.Drawing.Point(43, 37)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel9.TabIndex = 25
        Me.LmTitleLabel9.Text = "荷主"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "荷主"
        Me.LmTitleLabel9.WidthDef = 35
        '
        'sprWarning
        '
        Me.sprWarning.AccessibleDescription = ""
        Me.sprWarning.AllowUserZoom = False
        Me.sprWarning.AutoImeMode = False
        Me.sprWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprWarning.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprWarning.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprWarning.CellClickEventArgs = Nothing
        Me.sprWarning.CheckToCheckBox = True
        Me.sprWarning.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprWarning.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprWarning.EditModeReplace = True
        Me.sprWarning.FocusRenderer = EnhancedFocusIndicatorRenderer2
        Me.sprWarning.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning.ForeColorDef = System.Drawing.Color.Empty
        Me.sprWarning.HeightDef = 267
        Me.sprWarning.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprWarning.HorizontalScrollBar.Name = ""
        Me.sprWarning.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer3
        Me.sprWarning.KeyboardCheckBoxOn = False
        Me.sprWarning.Location = New System.Drawing.Point(14, 27)
        Me.sprWarning.Name = "sprWarning"
        Me.sprWarning.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprWarning.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprWarning_Sheet1})
        Me.sprWarning.Size = New System.Drawing.Size(985, 267)
        Me.sprWarning.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprWarning.SortColumn = True
        Me.sprWarning.SpanColumnLock = True
        Me.sprWarning.SpreadDoubleClicked = False
        Me.sprWarning.TabIndex = 115
        Me.sprWarning.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprWarning.TextValue = Nothing
        Me.sprWarning.UseGrouping = False
        Me.sprWarning.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprWarning.VerticalScrollBar.Name = ""
        Me.sprWarning.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer4
        Me.sprWarning.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprWarning.WidthDef = 985
        sprWarning_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprWarning_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprWarning_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprWarning.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprWarning_InputMapWhenFocusedNormal)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.CSEStopEditing)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.OemMinus, CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.AutoSum)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprWarning_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprWarning.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprWarning_InputMapWhenAncestorOfFocusedNormal)
        '
        'sprWarning_Sheet1
        '
        Me.sprWarning_Sheet1.Reset()
        Me.sprWarning_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprWarning_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprWarning_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Default
        Me.sprWarning_Sheet1.ColumnFooter.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnFooter.DefaultStyle.Locked = False
        Me.sprWarning_Sheet1.ColumnFooter.DefaultStyle.Parent = "ColumnFooterEnhanced"
        Me.sprWarning_Sheet1.ColumnFooter.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnFooter.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnFooterSheetCornerStyle.Locked = False
        Me.sprWarning_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprWarning_Sheet1.ColumnFooterSheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(0).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(1).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(2).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(3).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(4).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(5).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(6).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(7).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(8).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(9).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(10).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(11).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(12).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(13).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(14).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(15).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(16).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(17).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(18).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(19).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(20).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(21).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(22).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(23).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(24).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(25).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(26).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(27).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(28).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(29).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(30).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(31).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(32).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(33).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(34).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(35).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(36).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(37).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(38).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(39).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(40).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(41).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(42).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(43).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(44).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(45).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(46).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(47).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(48).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(49).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(50).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(51).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(52).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(53).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(54).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(55).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(56).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(57).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(58).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(59).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(60).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(61).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(62).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(63).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(64).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(65).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(66).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(67).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(68).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(69).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(70).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(71).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(72).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(73).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(74).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(75).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(76).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(77).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(78).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(79).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(80).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(81).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(82).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(83).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(84).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(85).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(86).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(87).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(88).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(89).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(90).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(91).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(92).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(93).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(94).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(95).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(96).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(97).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(98).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(99).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(100).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(101).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(102).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(103).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(104).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(105).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(106).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(107).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(108).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(109).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(110).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(111).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(112).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(113).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(114).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(115).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(116).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(117).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(118).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(119).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(120).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(121).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(122).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(123).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(124).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(125).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(126).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(127).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(128).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(129).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(130).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(131).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(132).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(133).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(134).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(135).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(136).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(137).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(138).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(139).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(140).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(141).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(142).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(143).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(144).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(145).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(146).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(147).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(148).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(149).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(150).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(151).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(152).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(153).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(154).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(155).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(156).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(157).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(158).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(159).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(160).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(161).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(162).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(163).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(164).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(165).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(166).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(167).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(168).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(169).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(170).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(171).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(172).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(173).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(174).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(175).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(176).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(177).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(178).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(179).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(180).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(181).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(182).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(183).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(184).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(185).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(186).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(187).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(188).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(189).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(190).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(191).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(192).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(193).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(194).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(195).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(196).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(197).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(198).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(199).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(200).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(201).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(202).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(203).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(204).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(205).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(206).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(207).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(208).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(209).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(210).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(211).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(212).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(213).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(214).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(215).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(216).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(217).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(218).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(219).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(220).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(221).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(222).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(223).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(224).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(225).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(226).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(227).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(228).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(229).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(230).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(231).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(232).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(233).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(234).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(235).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(236).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(237).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(238).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(239).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(240).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(241).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(242).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(243).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(244).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(245).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(246).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(247).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(248).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(249).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(250).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(251).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(252).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(253).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(254).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(255).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(256).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(257).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(258).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(259).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(260).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(261).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(262).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(263).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(264).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(265).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(266).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(267).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(268).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(269).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(270).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(271).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(272).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(273).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(274).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(275).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(276).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(277).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(278).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(279).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(280).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(281).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(282).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(283).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(284).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(285).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(286).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(287).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(288).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(289).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(290).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(291).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(292).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(293).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(294).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(295).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(296).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(297).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(298).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(299).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(300).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(301).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(302).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(303).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(304).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(305).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(306).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(307).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(308).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(309).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(310).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(311).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(312).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(313).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(314).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(315).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(316).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(317).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(318).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(319).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(320).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(321).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(322).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(323).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(324).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(325).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(326).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(327).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(328).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(329).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(330).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(331).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(332).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(333).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(334).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(335).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(336).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(337).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(338).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(339).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(340).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(341).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(342).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(343).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(344).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(345).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(346).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(347).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(348).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(349).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(350).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(351).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(352).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(353).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(354).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(355).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(356).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(357).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(358).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(359).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(360).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(361).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(362).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(363).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(364).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(365).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(366).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(367).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(368).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(369).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(370).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(371).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(372).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(373).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(374).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(375).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(376).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(377).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(378).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(379).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(380).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(381).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(382).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(383).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(384).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(385).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(386).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(387).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(388).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(389).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(390).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(391).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(392).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(393).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(394).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(395).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(396).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(397).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(398).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(399).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(400).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(401).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(402).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(403).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(404).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(405).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(406).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(407).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(408).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(409).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(410).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(411).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(412).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(413).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(414).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(415).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(416).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(417).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(418).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(419).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(420).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(421).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(422).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(423).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(424).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(425).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(426).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(427).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(428).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(429).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(430).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(431).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(432).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(433).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(434).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(435).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(436).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(437).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(438).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(439).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(440).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(441).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(442).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(443).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(444).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(445).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(446).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(447).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(448).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(449).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(450).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(451).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(452).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(453).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(454).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(455).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(456).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(457).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(458).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(459).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(460).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(461).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(462).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(463).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(464).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(465).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(466).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(467).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(468).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(469).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(470).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(471).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(472).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(473).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(474).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(475).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(476).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(477).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(478).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(479).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(480).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(481).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(482).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(483).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(484).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(485).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(486).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(487).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(488).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(489).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(490).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(491).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(492).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(493).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(494).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(495).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(496).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(497).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(498).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.Columns.Get(499).Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprWarning_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.ColumnHeader.DefaultStyle.Locked = False
        Me.sprWarning_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderEnhanced"
        Me.sprWarning_Sheet1.ColumnHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.ColumnHeader.Rows.Get(0).Height = 30.0!
        TextCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AllIME
        Me.sprWarning_Sheet1.Columns.Get(1).CellType = TextCellType2
        Me.sprWarning_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.DefaultStyle.Locked = False
        Me.sprWarning_Sheet1.DefaultStyle.Parent = "DataAreaDefault"
        Me.sprWarning_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.FilterBar.DefaultStyle.Locked = False
        Me.sprWarning_Sheet1.FilterBar.DefaultStyle.Parent = "FilterBarEnhanced"
        Me.sprWarning_Sheet1.FilterBar.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.FilterBarHeaderStyle.Locked = False
        Me.sprWarning_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderEnhanced"
        Me.sprWarning_Sheet1.FilterBarHeaderStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.GrayAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprWarning_Sheet1.HorizontalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprWarning_Sheet1.RowHeader.Columns.Default.Resizable = True
        Me.sprWarning_Sheet1.RowHeader.Columns.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.RowHeader.DefaultStyle.Locked = False
        Me.sprWarning_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderEnhanced"
        Me.sprWarning_Sheet1.RowHeader.DefaultStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.RowHeader.Rows.Default.Resizable = False
        Me.sprWarning_Sheet1.RowHeader.Rows.Default.Visible = True
        Me.sprWarning_Sheet1.RowHeader.Rows.Default.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.Rows.Default.Height = 18.0!
        Me.sprWarning_Sheet1.Rows.Default.Resizable = False
        Me.sprWarning_Sheet1.Rows.Default.Visible = True
        Me.sprWarning_Sheet1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.sprWarning_Sheet1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sprWarning_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors
        Me.sprWarning_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprWarning_Sheet1.SheetCornerStyle.Locked = True
        Me.sprWarning_Sheet1.SheetCornerStyle.Parent = "CornerEnhanced"
        Me.sprWarning_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center
        Me.sprWarning_Sheet1.SheetCornerStyle.VisualStyles = FarPoint.Win.VisualStyles.[Auto]
        Me.sprWarning_Sheet1.VerticalGridLine = New FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer)))
        Me.sprWarning_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'LmImTextBox1
        '
        Me.LmImTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox1.CountWrappedLine = False
        Me.LmImTextBox1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox1.HeightDef = 18
        Me.LmImTextBox1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox1.HissuLabelVisible = True
        Me.LmImTextBox1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox1.IsByteCheck = 30
        Me.LmImTextBox1.IsCalendarCheck = False
        Me.LmImTextBox1.IsDakutenCheck = False
        Me.LmImTextBox1.IsEisuCheck = False
        Me.LmImTextBox1.IsForbiddenWordsCheck = False
        Me.LmImTextBox1.IsFullByteCheck = 0
        Me.LmImTextBox1.IsHankakuCheck = False
        Me.LmImTextBox1.IsHissuCheck = True
        Me.LmImTextBox1.IsKanaCheck = False
        Me.LmImTextBox1.IsMiddleSpace = False
        Me.LmImTextBox1.IsNumericCheck = False
        Me.LmImTextBox1.IsSujiCheck = False
        Me.LmImTextBox1.IsZenkakuCheck = False
        Me.LmImTextBox1.ItemName = ""
        Me.LmImTextBox1.LineSpace = 0
        Me.LmImTextBox1.Location = New System.Drawing.Point(132, 30)
        Me.LmImTextBox1.MaxLength = 30
        Me.LmImTextBox1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox1.MaxLineCount = 0
        Me.LmImTextBox1.Multiline = False
        Me.LmImTextBox1.Name = "LmImTextBox1"
        Me.LmImTextBox1.ReadOnly = True
        Me.LmImTextBox1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox1.Size = New System.Drawing.Size(45, 18)
        Me.LmImTextBox1.TabIndex = 292
        Me.LmImTextBox1.TabStop = False
        Me.LmImTextBox1.TabStopSetting = False
        Me.LmImTextBox1.TextValue = ""
        Me.LmImTextBox1.UseSystemPasswordChar = False
        Me.LmImTextBox1.WidthDef = 45
        Me.LmImTextBox1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrder_tabFreeM
        '
        Me.lblTitleOrder_tabFreeM.AutoSize = True
        Me.lblTitleOrder_tabFreeM.AutoSizeDef = True
        Me.lblTitleOrder_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrder_tabFreeM.EnableStatus = False
        Me.lblTitleOrder_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrder_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrder_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrder_tabFreeM.HeightDef = 13
        Me.lblTitleOrder_tabFreeM.Location = New System.Drawing.Point(91, 33)
        Me.lblTitleOrder_tabFreeM.Name = "lblTitleOrder_tabFreeM"
        Me.lblTitleOrder_tabFreeM.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleOrder_tabFreeM.TabIndex = 291
        Me.lblTitleOrder_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrder_tabFreeM.TextValue = ""
        Me.lblTitleOrder_tabFreeM.WidthDef = 35
        '
        'lblHikiate_tabFreeM
        '
        Me.lblHikiate_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHikiate_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHikiate_tabFreeM.CountWrappedLine = False
        Me.lblHikiate_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblHikiate_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiate_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiate_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiate_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiate_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblHikiate_tabFreeM.HeightDef = 18
        Me.lblHikiate_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiate_tabFreeM.HissuLabelVisible = False
        Me.lblHikiate_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblHikiate_tabFreeM.IsByteCheck = 0
        Me.lblHikiate_tabFreeM.IsCalendarCheck = False
        Me.lblHikiate_tabFreeM.IsDakutenCheck = False
        Me.lblHikiate_tabFreeM.IsEisuCheck = False
        Me.lblHikiate_tabFreeM.IsForbiddenWordsCheck = False
        Me.lblHikiate_tabFreeM.IsFullByteCheck = 0
        Me.lblHikiate_tabFreeM.IsHankakuCheck = False
        Me.lblHikiate_tabFreeM.IsHissuCheck = False
        Me.lblHikiate_tabFreeM.IsKanaCheck = False
        Me.lblHikiate_tabFreeM.IsMiddleSpace = False
        Me.lblHikiate_tabFreeM.IsNumericCheck = False
        Me.lblHikiate_tabFreeM.IsSujiCheck = False
        Me.lblHikiate_tabFreeM.IsZenkakuCheck = False
        Me.lblHikiate_tabFreeM.ItemName = ""
        Me.lblHikiate_tabFreeM.LineSpace = 0
        Me.lblHikiate_tabFreeM.Location = New System.Drawing.Point(356, 30)
        Me.lblHikiate_tabFreeM.MaxLength = 0
        Me.lblHikiate_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblHikiate_tabFreeM.MaxLineCount = 0
        Me.lblHikiate_tabFreeM.Multiline = False
        Me.lblHikiate_tabFreeM.Name = "lblHikiate_tabFreeM"
        Me.lblHikiate_tabFreeM.ReadOnly = True
        Me.lblHikiate_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblHikiate_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblHikiate_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.lblHikiate_tabFreeM.TabIndex = 290
        Me.lblHikiate_tabFreeM.TabStop = False
        Me.lblHikiate_tabFreeM.TabStopSetting = False
        Me.lblHikiate_tabFreeM.TextValue = ""
        Me.lblHikiate_tabFreeM.UseSystemPasswordChar = False
        Me.lblHikiate_tabFreeM.WidthDef = 100
        Me.lblHikiate_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel60
        '
        Me.LmTitleLabel60.AutoSize = True
        Me.LmTitleLabel60.AutoSizeDef = True
        Me.LmTitleLabel60.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel60.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel60.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel60.EnableStatus = False
        Me.LmTitleLabel60.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel60.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel60.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel60.HeightDef = 13
        Me.LmTitleLabel60.Location = New System.Drawing.Point(259, 11)
        Me.LmTitleLabel60.Name = "LmTitleLabel60"
        Me.LmTitleLabel60.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel60.TabIndex = 289
        Me.LmTitleLabel60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel60.TextValue = ""
        Me.LmTitleLabel60.WidthDef = 91
        '
        'LmTitleLabel67
        '
        Me.LmTitleLabel67.AutoSize = True
        Me.LmTitleLabel67.AutoSizeDef = True
        Me.LmTitleLabel67.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel67.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel67.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel67.EnableStatus = False
        Me.LmTitleLabel67.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel67.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel67.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel67.HeightDef = 13
        Me.LmTitleLabel67.Location = New System.Drawing.Point(675, 75)
        Me.LmTitleLabel67.Name = "LmTitleLabel67"
        Me.LmTitleLabel67.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel67.TabIndex = 287
        Me.LmTitleLabel67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel67.TextValue = ""
        Me.LmTitleLabel67.WidthDef = 21
        '
        'LmImNumber1
        '
        Me.LmImNumber1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImNumber1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LmImNumber1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.HeightDef = 18
        Me.LmImNumber1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.HissuLabelVisible = False
        Me.LmImNumber1.IsHissuCheck = False
        Me.LmImNumber1.IsRangeCheck = False
        Me.LmImNumber1.ItemName = ""
        Me.LmImNumber1.Location = New System.Drawing.Point(570, 30)
        Me.LmImNumber1.Name = "LmImNumber1"
        Me.LmImNumber1.ReadOnly = True
        Me.LmImNumber1.Size = New System.Drawing.Size(79, 18)
        Me.LmImNumber1.TabIndex = 286
        Me.LmImNumber1.TabStop = False
        Me.LmImNumber1.TabStopSetting = False
        Me.LmImNumber1.TextValue = "0"
        Me.LmImNumber1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.LmImNumber1.WidthDef = 79
        '
        'lblTARE_tabFreeM
        '
        Me.lblTARE_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTARE_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTARE_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTARE_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTARE_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTARE_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTARE_tabFreeM.HeightDef = 18
        Me.lblTARE_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTARE_tabFreeM.HissuLabelVisible = False
        Me.lblTARE_tabFreeM.IsHissuCheck = False
        Me.lblTARE_tabFreeM.IsRangeCheck = False
        Me.lblTARE_tabFreeM.ItemName = ""
        Me.lblTARE_tabFreeM.Location = New System.Drawing.Point(570, 72)
        Me.lblTARE_tabFreeM.Name = "lblTARE_tabFreeM"
        Me.lblTARE_tabFreeM.ReadOnly = True
        Me.lblTARE_tabFreeM.Size = New System.Drawing.Size(117, 18)
        Me.lblTARE_tabFreeM.TabIndex = 285
        Me.lblTARE_tabFreeM.TabStop = False
        Me.lblTARE_tabFreeM.TabStopSetting = False
        Me.lblTARE_tabFreeM.TextValue = "0"
        Me.lblTARE_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblTARE_tabFreeM.WidthDef = 117
        '
        'LmTitleLabel68
        '
        Me.LmTitleLabel68.AutoSize = True
        Me.LmTitleLabel68.AutoSizeDef = True
        Me.LmTitleLabel68.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel68.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel68.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel68.EnableStatus = False
        Me.LmTitleLabel68.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel68.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel68.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel68.HeightDef = 13
        Me.LmTitleLabel68.Location = New System.Drawing.Point(529, 75)
        Me.LmTitleLabel68.Name = "LmTitleLabel68"
        Me.LmTitleLabel68.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel68.TabIndex = 284
        Me.LmTitleLabel68.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel68.TextValue = ""
        Me.LmTitleLabel68.WidthDef = 35
        '
        'LmTitleLabel72
        '
        Me.LmTitleLabel72.AutoSize = True
        Me.LmTitleLabel72.AutoSizeDef = True
        Me.LmTitleLabel72.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel72.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel72.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel72.EnableStatus = False
        Me.LmTitleLabel72.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel72.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel72.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel72.HeightDef = 13
        Me.LmTitleLabel72.Location = New System.Drawing.Point(529, 33)
        Me.LmTitleLabel72.Name = "LmTitleLabel72"
        Me.LmTitleLabel72.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel72.TabIndex = 283
        Me.LmTitleLabel72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel72.TextValue = ""
        Me.LmTitleLabel72.WidthDef = 35
        '
        'LmTitleLabel73
        '
        Me.LmTitleLabel73.AutoSize = True
        Me.LmTitleLabel73.AutoSizeDef = True
        Me.LmTitleLabel73.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel73.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel73.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel73.EnableStatus = False
        Me.LmTitleLabel73.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel73.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel73.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel73.HeightDef = 13
        Me.LmTitleLabel73.Location = New System.Drawing.Point(444, 54)
        Me.LmTitleLabel73.Name = "LmTitleLabel73"
        Me.LmTitleLabel73.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel73.TabIndex = 282
        Me.LmTitleLabel73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel73.TextValue = ""
        Me.LmTitleLabel73.WidthDef = 77
        '
        'txtHasu_tabFreeM
        '
        Me.txtHasu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHasu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtHasu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHasu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHasu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtHasu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtHasu_tabFreeM.HeightDef = 18
        Me.txtHasu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtHasu_tabFreeM.HissuLabelVisible = False
        Me.txtHasu_tabFreeM.IsHissuCheck = False
        Me.txtHasu_tabFreeM.IsRangeCheck = False
        Me.txtHasu_tabFreeM.ItemName = ""
        Me.txtHasu_tabFreeM.Location = New System.Drawing.Point(356, 51)
        Me.txtHasu_tabFreeM.Name = "txtHasu_tabFreeM"
        Me.txtHasu_tabFreeM.ReadOnly = True
        Me.txtHasu_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.txtHasu_tabFreeM.TabIndex = 281
        Me.txtHasu_tabFreeM.TabStop = False
        Me.txtHasu_tabFreeM.TabStopSetting = False
        Me.txtHasu_tabFreeM.TextValue = "0"
        Me.txtHasu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtHasu_tabFreeM.WidthDef = 100
        '
        'LmTitleLabel74
        '
        Me.LmTitleLabel74.AutoSize = True
        Me.LmTitleLabel74.AutoSizeDef = True
        Me.LmTitleLabel74.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel74.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel74.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel74.EnableStatus = False
        Me.LmTitleLabel74.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel74.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel74.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel74.HeightDef = 13
        Me.LmTitleLabel74.Location = New System.Drawing.Point(315, 54)
        Me.LmTitleLabel74.Name = "LmTitleLabel74"
        Me.LmTitleLabel74.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel74.TabIndex = 280
        Me.LmTitleLabel74.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel74.TextValue = ""
        Me.LmTitleLabel74.WidthDef = 35
        '
        'LmTitleLabel78
        '
        Me.LmTitleLabel78.AutoSize = True
        Me.LmTitleLabel78.AutoSizeDef = True
        Me.LmTitleLabel78.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel78.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel78.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel78.EnableStatus = False
        Me.LmTitleLabel78.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel78.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel78.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel78.HeightDef = 13
        Me.LmTitleLabel78.Location = New System.Drawing.Point(218, 54)
        Me.LmTitleLabel78.Name = "LmTitleLabel78"
        Me.LmTitleLabel78.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel78.TabIndex = 279
        Me.LmTitleLabel78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel78.TextValue = ""
        Me.LmTitleLabel78.WidthDef = 77
        '
        'txtKosu_tabFreeM
        '
        Me.txtKosu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKosu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.txtKosu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKosu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKosu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKosu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKosu_tabFreeM.HeightDef = 18
        Me.txtKosu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKosu_tabFreeM.HissuLabelVisible = False
        Me.txtKosu_tabFreeM.IsHissuCheck = False
        Me.txtKosu_tabFreeM.IsRangeCheck = False
        Me.txtKosu_tabFreeM.ItemName = ""
        Me.txtKosu_tabFreeM.Location = New System.Drawing.Point(132, 51)
        Me.txtKosu_tabFreeM.Name = "txtKosu_tabFreeM"
        Me.txtKosu_tabFreeM.ReadOnly = True
        Me.txtKosu_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.txtKosu_tabFreeM.TabIndex = 278
        Me.txtKosu_tabFreeM.TabStop = False
        Me.txtKosu_tabFreeM.TabStopSetting = False
        Me.txtKosu_tabFreeM.TextValue = "0"
        Me.txtKosu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtKosu_tabFreeM.WidthDef = 97
        '
        'lblTitleKonsu_tabFreeM
        '
        Me.lblTitleKonsu_tabFreeM.AutoSize = True
        Me.lblTitleKonsu_tabFreeM.AutoSizeDef = True
        Me.lblTitleKonsu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKonsu_tabFreeM.EnableStatus = False
        Me.lblTitleKonsu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKonsu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKonsu_tabFreeM.HeightDef = 13
        Me.lblTitleKonsu_tabFreeM.Location = New System.Drawing.Point(91, 54)
        Me.lblTitleKonsu_tabFreeM.Name = "lblTitleKonsu_tabFreeM"
        Me.lblTitleKonsu_tabFreeM.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKonsu_tabFreeM.TabIndex = 277
        Me.lblTitleKonsu_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKonsu_tabFreeM.TextValue = ""
        Me.lblTitleKonsu_tabFreeM.WidthDef = 35
        '
        'LmImTextBox18
        '
        Me.LmImTextBox18.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox18.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox18.CountWrappedLine = False
        Me.LmImTextBox18.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox18.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox18.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox18.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox18.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox18.HeightDef = 18
        Me.LmImTextBox18.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox18.HissuLabelVisible = False
        Me.LmImTextBox18.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox18.IsByteCheck = 0
        Me.LmImTextBox18.IsCalendarCheck = False
        Me.LmImTextBox18.IsDakutenCheck = False
        Me.LmImTextBox18.IsEisuCheck = False
        Me.LmImTextBox18.IsForbiddenWordsCheck = False
        Me.LmImTextBox18.IsFullByteCheck = 0
        Me.LmImTextBox18.IsHankakuCheck = False
        Me.LmImTextBox18.IsHissuCheck = False
        Me.LmImTextBox18.IsKanaCheck = False
        Me.LmImTextBox18.IsMiddleSpace = False
        Me.LmImTextBox18.IsNumericCheck = False
        Me.LmImTextBox18.IsSujiCheck = False
        Me.LmImTextBox18.IsZenkakuCheck = False
        Me.LmImTextBox18.ItemName = ""
        Me.LmImTextBox18.LineSpace = 0
        Me.LmImTextBox18.Location = New System.Drawing.Point(545, 9)
        Me.LmImTextBox18.MaxLength = 0
        Me.LmImTextBox18.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox18.MaxLineCount = 0
        Me.LmImTextBox18.Multiline = False
        Me.LmImTextBox18.Name = "LmImTextBox18"
        Me.LmImTextBox18.ReadOnly = True
        Me.LmImTextBox18.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox18.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox18.Size = New System.Drawing.Size(443, 18)
        Me.LmImTextBox18.TabIndex = 276
        Me.LmImTextBox18.TabStop = False
        Me.LmImTextBox18.TabStopSetting = False
        Me.LmImTextBox18.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.LmImTextBox18.UseSystemPasswordChar = False
        Me.LmImTextBox18.WidthDef = 443
        Me.LmImTextBox18.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmImTextBox19
        '
        Me.LmImTextBox19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImTextBox19.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmImTextBox19.CountWrappedLine = False
        Me.LmImTextBox19.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.LmImTextBox19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImTextBox19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImTextBox19.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.LmImTextBox19.HeightDef = 18
        Me.LmImTextBox19.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImTextBox19.HissuLabelVisible = False
        Me.LmImTextBox19.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.LmImTextBox19.IsByteCheck = 0
        Me.LmImTextBox19.IsCalendarCheck = False
        Me.LmImTextBox19.IsDakutenCheck = False
        Me.LmImTextBox19.IsEisuCheck = False
        Me.LmImTextBox19.IsForbiddenWordsCheck = False
        Me.LmImTextBox19.IsFullByteCheck = 0
        Me.LmImTextBox19.IsHankakuCheck = False
        Me.LmImTextBox19.IsHissuCheck = False
        Me.LmImTextBox19.IsKanaCheck = False
        Me.LmImTextBox19.IsMiddleSpace = False
        Me.LmImTextBox19.IsNumericCheck = False
        Me.LmImTextBox19.IsSujiCheck = False
        Me.LmImTextBox19.IsZenkakuCheck = False
        Me.LmImTextBox19.ItemName = ""
        Me.LmImTextBox19.LineSpace = 0
        Me.LmImTextBox19.Location = New System.Drawing.Point(356, 9)
        Me.LmImTextBox19.MaxLength = 0
        Me.LmImTextBox19.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.LmImTextBox19.MaxLineCount = 0
        Me.LmImTextBox19.Multiline = False
        Me.LmImTextBox19.Name = "LmImTextBox19"
        Me.LmImTextBox19.ReadOnly = True
        Me.LmImTextBox19.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.LmImTextBox19.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.LmImTextBox19.Size = New System.Drawing.Size(208, 18)
        Me.LmImTextBox19.TabIndex = 275
        Me.LmImTextBox19.TabStop = False
        Me.LmImTextBox19.TabStopSetting = False
        Me.LmImTextBox19.TextValue = ""
        Me.LmImTextBox19.UseSystemPasswordChar = False
        Me.LmImTextBox19.WidthDef = 208
        Me.LmImTextBox19.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel80
        '
        Me.LmTitleLabel80.AutoSize = True
        Me.LmTitleLabel80.AutoSizeDef = True
        Me.LmTitleLabel80.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel80.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel80.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel80.EnableStatus = False
        Me.LmTitleLabel80.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel80.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel80.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel80.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel80.HeightDef = 13
        Me.LmTitleLabel80.Location = New System.Drawing.Point(287, 33)
        Me.LmTitleLabel80.Name = "LmTitleLabel80"
        Me.LmTitleLabel80.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel80.TabIndex = 274
        Me.LmTitleLabel80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel80.TextValue = ""
        Me.LmTitleLabel80.WidthDef = 63
        '
        'txtKanriNO_M_tabFreeM
        '
        Me.txtKanriNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKanriNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKanriNO_M_tabFreeM.CountWrappedLine = False
        Me.txtKanriNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKanriNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKanriNO_M_tabFreeM.HeightDef = 18
        Me.txtKanriNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtKanriNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKanriNO_M_tabFreeM.IsByteCheck = 30
        Me.txtKanriNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtKanriNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtKanriNO_M_tabFreeM.IsEisuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtKanriNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtKanriNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsHissuCheck = False
        Me.txtKanriNO_M_tabFreeM.IsKanaCheck = False
        Me.txtKanriNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtKanriNO_M_tabFreeM.IsNumericCheck = False
        Me.txtKanriNO_M_tabFreeM.IsSujiCheck = False
        Me.txtKanriNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtKanriNO_M_tabFreeM.ItemName = ""
        Me.txtKanriNO_M_tabFreeM.LineSpace = 0
        Me.txtKanriNO_M_tabFreeM.Location = New System.Drawing.Point(132, 9)
        Me.txtKanriNO_M_tabFreeM.MaxLength = 30
        Me.txtKanriNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKanriNO_M_tabFreeM.MaxLineCount = 0
        Me.txtKanriNO_M_tabFreeM.Multiline = False
        Me.txtKanriNO_M_tabFreeM.Name = "txtKanriNO_M_tabFreeM"
        Me.txtKanriNO_M_tabFreeM.ReadOnly = True
        Me.txtKanriNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKanriNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKanriNO_M_tabFreeM.Size = New System.Drawing.Size(45, 18)
        Me.txtKanriNO_M_tabFreeM.TabIndex = 273
        Me.txtKanriNO_M_tabFreeM.TabStop = False
        Me.txtKanriNO_M_tabFreeM.TabStopSetting = False
        Me.txtKanriNO_M_tabFreeM.TextValue = ""
        Me.txtKanriNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtKanriNO_M_tabFreeM.WidthDef = 45
        Me.txtKanriNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel81
        '
        Me.LmTitleLabel81.AutoSize = True
        Me.LmTitleLabel81.AutoSizeDef = True
        Me.LmTitleLabel81.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel81.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel81.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel81.EnableStatus = False
        Me.LmTitleLabel81.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel81.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel81.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel81.HeightDef = 13
        Me.LmTitleLabel81.Location = New System.Drawing.Point(12, 12)
        Me.LmTitleLabel81.Name = "LmTitleLabel81"
        Me.LmTitleLabel81.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel81.TabIndex = 272
        Me.LmTitleLabel81.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel81.TextValue = ""
        Me.LmTitleLabel81.WidthDef = 119
        '
        'txtGoodsComment_tabFreeM
        '
        Me.txtGoodsComment_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsComment_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsComment_tabFreeM.CountWrappedLine = False
        Me.txtGoodsComment_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsComment_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsComment_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsComment_tabFreeM.HeightDef = 18
        Me.txtGoodsComment_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsComment_tabFreeM.HissuLabelVisible = False
        Me.txtGoodsComment_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsComment_tabFreeM.IsByteCheck = 50
        Me.txtGoodsComment_tabFreeM.IsCalendarCheck = False
        Me.txtGoodsComment_tabFreeM.IsDakutenCheck = False
        Me.txtGoodsComment_tabFreeM.IsEisuCheck = False
        Me.txtGoodsComment_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtGoodsComment_tabFreeM.IsFullByteCheck = 0
        Me.txtGoodsComment_tabFreeM.IsHankakuCheck = False
        Me.txtGoodsComment_tabFreeM.IsHissuCheck = False
        Me.txtGoodsComment_tabFreeM.IsKanaCheck = False
        Me.txtGoodsComment_tabFreeM.IsMiddleSpace = False
        Me.txtGoodsComment_tabFreeM.IsNumericCheck = False
        Me.txtGoodsComment_tabFreeM.IsSujiCheck = False
        Me.txtGoodsComment_tabFreeM.IsZenkakuCheck = False
        Me.txtGoodsComment_tabFreeM.ItemName = ""
        Me.txtGoodsComment_tabFreeM.LineSpace = 0
        Me.txtGoodsComment_tabFreeM.Location = New System.Drawing.Point(132, 114)
        Me.txtGoodsComment_tabFreeM.MaxLength = 50
        Me.txtGoodsComment_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsComment_tabFreeM.MaxLineCount = 0
        Me.txtGoodsComment_tabFreeM.Multiline = False
        Me.txtGoodsComment_tabFreeM.Name = "txtGoodsComment_tabFreeM"
        Me.txtGoodsComment_tabFreeM.ReadOnly = True
        Me.txtGoodsComment_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsComment_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsComment_tabFreeM.Size = New System.Drawing.Size(893, 18)
        Me.txtGoodsComment_tabFreeM.TabIndex = 256
        Me.txtGoodsComment_tabFreeM.TabStop = False
        Me.txtGoodsComment_tabFreeM.TabStopSetting = False
        Me.txtGoodsComment_tabFreeM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsComment_tabFreeM.UseSystemPasswordChar = False
        Me.txtGoodsComment_tabFreeM.WidthDef = 893
        Me.txtGoodsComment_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel89
        '
        Me.LmTitleLabel89.AutoSize = True
        Me.LmTitleLabel89.AutoSizeDef = True
        Me.LmTitleLabel89.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel89.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel89.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel89.EnableStatus = False
        Me.LmTitleLabel89.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel89.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel89.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel89.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel89.HeightDef = 13
        Me.LmTitleLabel89.Location = New System.Drawing.Point(21, 117)
        Me.LmTitleLabel89.Name = "LmTitleLabel89"
        Me.LmTitleLabel89.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel89.TabIndex = 255
        Me.LmTitleLabel89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel89.TextValue = ""
        Me.LmTitleLabel89.WidthDef = 105
        '
        'lblIrisuTani1_tabFreeM
        '
        Me.lblIrisuTani1_tabFreeM.AutoSize = True
        Me.lblIrisuTani1_tabFreeM.AutoSizeDef = True
        Me.lblIrisuTani1_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrisuTani1_tabFreeM.EnableStatus = False
        Me.lblIrisuTani1_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani1_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani1_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani1_tabFreeM.HeightDef = 13
        Me.lblIrisuTani1_tabFreeM.Location = New System.Drawing.Point(654, 54)
        Me.lblIrisuTani1_tabFreeM.Name = "lblIrisuTani1_tabFreeM"
        Me.lblIrisuTani1_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblIrisuTani1_tabFreeM.TabIndex = 266
        Me.lblIrisuTani1_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisuTani1_tabFreeM.TextValue = ""
        Me.lblIrisuTani1_tabFreeM.WidthDef = 21
        '
        'lblStdIrimeTani_tabFreeM
        '
        Me.lblStdIrimeTani_tabFreeM.AutoSize = True
        Me.lblStdIrimeTani_tabFreeM.AutoSizeDef = True
        Me.lblStdIrimeTani_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStdIrimeTani_tabFreeM.EnableStatus = False
        Me.lblStdIrimeTani_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStdIrimeTani_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStdIrimeTani_tabFreeM.HeightDef = 13
        Me.lblStdIrimeTani_tabFreeM.Location = New System.Drawing.Point(871, 54)
        Me.lblStdIrimeTani_tabFreeM.Name = "lblStdIrimeTani_tabFreeM"
        Me.lblStdIrimeTani_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblStdIrimeTani_tabFreeM.TabIndex = 267
        Me.lblStdIrimeTani_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStdIrimeTani_tabFreeM.TextValue = ""
        Me.lblStdIrimeTani_tabFreeM.WidthDef = 21
        '
        'txtBuyerOrdNO_M_tabFreeM
        '
        Me.txtBuyerOrdNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerOrdNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBuyerOrdNO_M_tabFreeM.CountWrappedLine = False
        Me.txtBuyerOrdNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtBuyerOrdNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBuyerOrdNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtBuyerOrdNO_M_tabFreeM.HeightDef = 18
        Me.txtBuyerOrdNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBuyerOrdNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtBuyerOrdNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtBuyerOrdNO_M_tabFreeM.IsByteCheck = 30
        Me.txtBuyerOrdNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsEisuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtBuyerOrdNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsHissuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsKanaCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsNumericCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsSujiCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtBuyerOrdNO_M_tabFreeM.ItemName = ""
        Me.txtBuyerOrdNO_M_tabFreeM.LineSpace = 0
        Me.txtBuyerOrdNO_M_tabFreeM.Location = New System.Drawing.Point(570, 93)
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLength = 30
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBuyerOrdNO_M_tabFreeM.MaxLineCount = 0
        Me.txtBuyerOrdNO_M_tabFreeM.Multiline = False
        Me.txtBuyerOrdNO_M_tabFreeM.Name = "txtBuyerOrdNO_M_tabFreeM"
        Me.txtBuyerOrdNO_M_tabFreeM.ReadOnly = True
        Me.txtBuyerOrdNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBuyerOrdNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBuyerOrdNO_M_tabFreeM.Size = New System.Drawing.Size(282, 18)
        Me.txtBuyerOrdNO_M_tabFreeM.TabIndex = 260
        Me.txtBuyerOrdNO_M_tabFreeM.TabStop = False
        Me.txtBuyerOrdNO_M_tabFreeM.TabStopSetting = False
        Me.txtBuyerOrdNO_M_tabFreeM.TextValue = ""
        Me.txtBuyerOrdNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtBuyerOrdNO_M_tabFreeM.WidthDef = 282
        Me.txtBuyerOrdNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel92
        '
        Me.LmTitleLabel92.AutoSize = True
        Me.LmTitleLabel92.AutoSizeDef = True
        Me.LmTitleLabel92.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel92.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel92.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel92.EnableStatus = False
        Me.LmTitleLabel92.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel92.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel92.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel92.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel92.HeightDef = 13
        Me.LmTitleLabel92.Location = New System.Drawing.Point(501, 96)
        Me.LmTitleLabel92.Name = "LmTitleLabel92"
        Me.LmTitleLabel92.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel92.TabIndex = 259
        Me.LmTitleLabel92.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel92.TextValue = ""
        Me.LmTitleLabel92.WidthDef = 63
        '
        'lblIrisuTani2_tabFreeM
        '
        Me.lblIrisuTani2_tabFreeM.AutoSize = True
        Me.lblIrisuTani2_tabFreeM.AutoSizeDef = True
        Me.lblIrisuTani2_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrisuTani2_tabFreeM.EnableStatus = False
        Me.lblIrisuTani2_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani2_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisuTani2_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisuTani2_tabFreeM.HeightDef = 13
        Me.lblIrisuTani2_tabFreeM.Location = New System.Drawing.Point(686, 54)
        Me.lblIrisuTani2_tabFreeM.Name = "lblIrisuTani2_tabFreeM"
        Me.lblIrisuTani2_tabFreeM.Size = New System.Drawing.Size(21, 13)
        Me.lblIrisuTani2_tabFreeM.TabIndex = 263
        Me.lblIrisuTani2_tabFreeM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisuTani2_tabFreeM.TextValue = ""
        Me.lblIrisuTani2_tabFreeM.WidthDef = 21
        '
        'numStdIrime_tabFreeM
        '
        Me.numStdIrime_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numStdIrime_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numStdIrime_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numStdIrime_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numStdIrime_tabFreeM.HeightDef = 18
        Me.numStdIrime_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numStdIrime_tabFreeM.HissuLabelVisible = False
        Me.numStdIrime_tabFreeM.IsHissuCheck = False
        Me.numStdIrime_tabFreeM.IsRangeCheck = False
        Me.numStdIrime_tabFreeM.ItemName = ""
        Me.numStdIrime_tabFreeM.Location = New System.Drawing.Point(783, 51)
        Me.numStdIrime_tabFreeM.Name = "numStdIrime_tabFreeM"
        Me.numStdIrime_tabFreeM.ReadOnly = True
        Me.numStdIrime_tabFreeM.Size = New System.Drawing.Size(100, 18)
        Me.numStdIrime_tabFreeM.TabIndex = 269
        Me.numStdIrime_tabFreeM.TabStop = False
        Me.numStdIrime_tabFreeM.TabStopSetting = False
        Me.numStdIrime_tabFreeM.TextValue = "0"
        Me.numStdIrime_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numStdIrime_tabFreeM.WidthDef = 100
        '
        'txtOrderNO_M_tabFreeM
        '
        Me.txtOrderNO_M_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNO_M_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNO_M_tabFreeM.CountWrappedLine = False
        Me.txtOrderNO_M_tabFreeM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNO_M_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNO_M_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNO_M_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNO_M_tabFreeM.HeightDef = 18
        Me.txtOrderNO_M_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNO_M_tabFreeM.HissuLabelVisible = False
        Me.txtOrderNO_M_tabFreeM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOrderNO_M_tabFreeM.IsByteCheck = 30
        Me.txtOrderNO_M_tabFreeM.IsCalendarCheck = False
        Me.txtOrderNO_M_tabFreeM.IsDakutenCheck = False
        Me.txtOrderNO_M_tabFreeM.IsEisuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsForbiddenWordsCheck = False
        Me.txtOrderNO_M_tabFreeM.IsFullByteCheck = 0
        Me.txtOrderNO_M_tabFreeM.IsHankakuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsHissuCheck = False
        Me.txtOrderNO_M_tabFreeM.IsKanaCheck = False
        Me.txtOrderNO_M_tabFreeM.IsMiddleSpace = False
        Me.txtOrderNO_M_tabFreeM.IsNumericCheck = False
        Me.txtOrderNO_M_tabFreeM.IsSujiCheck = False
        Me.txtOrderNO_M_tabFreeM.IsZenkakuCheck = False
        Me.txtOrderNO_M_tabFreeM.ItemName = ""
        Me.txtOrderNO_M_tabFreeM.LineSpace = 0
        Me.txtOrderNO_M_tabFreeM.Location = New System.Drawing.Point(132, 93)
        Me.txtOrderNO_M_tabFreeM.MaxLength = 30
        Me.txtOrderNO_M_tabFreeM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNO_M_tabFreeM.MaxLineCount = 0
        Me.txtOrderNO_M_tabFreeM.Multiline = False
        Me.txtOrderNO_M_tabFreeM.Name = "txtOrderNO_M_tabFreeM"
        Me.txtOrderNO_M_tabFreeM.ReadOnly = True
        Me.txtOrderNO_M_tabFreeM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNO_M_tabFreeM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNO_M_tabFreeM.Size = New System.Drawing.Size(348, 18)
        Me.txtOrderNO_M_tabFreeM.TabIndex = 254
        Me.txtOrderNO_M_tabFreeM.TabStop = False
        Me.txtOrderNO_M_tabFreeM.TabStopSetting = False
        Me.txtOrderNO_M_tabFreeM.TextValue = ""
        Me.txtOrderNO_M_tabFreeM.UseSystemPasswordChar = False
        Me.txtOrderNO_M_tabFreeM.WidthDef = 348
        Me.txtOrderNO_M_tabFreeM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numIrisu_tabFreeM
        '
        Me.numIrisu_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrisu_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrisu_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu_tabFreeM.HeightDef = 18
        Me.numIrisu_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu_tabFreeM.HissuLabelVisible = False
        Me.numIrisu_tabFreeM.IsHissuCheck = False
        Me.numIrisu_tabFreeM.IsRangeCheck = False
        Me.numIrisu_tabFreeM.ItemName = ""
        Me.numIrisu_tabFreeM.Location = New System.Drawing.Point(570, 51)
        Me.numIrisu_tabFreeM.Name = "numIrisu_tabFreeM"
        Me.numIrisu_tabFreeM.ReadOnly = True
        Me.numIrisu_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numIrisu_tabFreeM.TabIndex = 268
        Me.numIrisu_tabFreeM.TabStop = False
        Me.numIrisu_tabFreeM.TabStopSetting = False
        Me.numIrisu_tabFreeM.TextValue = "0"
        Me.numIrisu_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrisu_tabFreeM.WidthDef = 97
        '
        'LmTitleLabel94
        '
        Me.LmTitleLabel94.AutoSize = True
        Me.LmTitleLabel94.AutoSizeDef = True
        Me.LmTitleLabel94.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel94.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel94.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel94.EnableStatus = False
        Me.LmTitleLabel94.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel94.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel94.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel94.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel94.HeightDef = 13
        Me.LmTitleLabel94.Location = New System.Drawing.Point(35, 96)
        Me.LmTitleLabel94.Name = "LmTitleLabel94"
        Me.LmTitleLabel94.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel94.TabIndex = 253
        Me.LmTitleLabel94.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel94.TextValue = ""
        Me.LmTitleLabel94.WidthDef = 91
        '
        'LmTitleLabel95
        '
        Me.LmTitleLabel95.AutoSize = True
        Me.LmTitleLabel95.AutoSizeDef = True
        Me.LmTitleLabel95.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel95.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel95.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel95.EnableStatus = False
        Me.LmTitleLabel95.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel95.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel95.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel95.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel95.HeightDef = 13
        Me.LmTitleLabel95.Location = New System.Drawing.Point(85, 9)
        Me.LmTitleLabel95.Name = "LmTitleLabel95"
        Me.LmTitleLabel95.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel95.TabIndex = 252
        Me.LmTitleLabel95.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel95.TextValue = ""
        Me.LmTitleLabel95.WidthDef = 35
        '
        'LmTitleLabel96
        '
        Me.LmTitleLabel96.AutoSize = True
        Me.LmTitleLabel96.AutoSizeDef = True
        Me.LmTitleLabel96.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel96.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel96.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel96.EnableStatus = False
        Me.LmTitleLabel96.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel96.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel96.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel96.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel96.HeightDef = 13
        Me.LmTitleLabel96.Location = New System.Drawing.Point(444, 75)
        Me.LmTitleLabel96.Name = "LmTitleLabel96"
        Me.LmTitleLabel96.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel96.TabIndex = 265
        Me.LmTitleLabel96.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel96.TextValue = ""
        Me.LmTitleLabel96.WidthDef = 21
        '
        'LmTitleLabel97
        '
        Me.LmTitleLabel97.AutoSize = True
        Me.LmTitleLabel97.AutoSizeDef = True
        Me.LmTitleLabel97.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel97.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel97.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel97.EnableStatus = False
        Me.LmTitleLabel97.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel97.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel97.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel97.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel97.HeightDef = 13
        Me.LmTitleLabel97.Location = New System.Drawing.Point(714, 54)
        Me.LmTitleLabel97.Name = "LmTitleLabel97"
        Me.LmTitleLabel97.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel97.TabIndex = 257
        Me.LmTitleLabel97.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel97.TextValue = ""
        Me.LmTitleLabel97.WidthDef = 63
        '
        'LmTitleLabel98
        '
        Me.LmTitleLabel98.AutoSize = True
        Me.LmTitleLabel98.AutoSizeDef = True
        Me.LmTitleLabel98.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel98.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel98.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel98.EnableStatus = False
        Me.LmTitleLabel98.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel98.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel98.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel98.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel98.HeightDef = 13
        Me.LmTitleLabel98.Location = New System.Drawing.Point(218, 75)
        Me.LmTitleLabel98.Name = "LmTitleLabel98"
        Me.LmTitleLabel98.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel98.TabIndex = 264
        Me.LmTitleLabel98.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel98.TextValue = ""
        Me.LmTitleLabel98.WidthDef = 21
        '
        'LmTitleLabel99
        '
        Me.LmTitleLabel99.AutoSize = True
        Me.LmTitleLabel99.AutoSizeDef = True
        Me.LmTitleLabel99.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel99.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel99.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel99.EnableStatus = False
        Me.LmTitleLabel99.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel99.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel99.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel99.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel99.HeightDef = 13
        Me.LmTitleLabel99.Location = New System.Drawing.Point(91, 75)
        Me.LmTitleLabel99.Name = "LmTitleLabel99"
        Me.LmTitleLabel99.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel99.TabIndex = 258
        Me.LmTitleLabel99.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel99.TextValue = ""
        Me.LmTitleLabel99.WidthDef = 35
        '
        'LmTitleLabel100
        '
        Me.LmTitleLabel100.AutoSize = True
        Me.LmTitleLabel100.AutoSizeDef = True
        Me.LmTitleLabel100.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel100.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel100.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel100.EnableStatus = False
        Me.LmTitleLabel100.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel100.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel100.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel100.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel100.HeightDef = 13
        Me.LmTitleLabel100.Location = New System.Drawing.Point(315, 75)
        Me.LmTitleLabel100.Name = "LmTitleLabel100"
        Me.LmTitleLabel100.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel100.TabIndex = 261
        Me.LmTitleLabel100.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel100.TextValue = ""
        Me.LmTitleLabel100.WidthDef = 35
        '
        'LmTitleLabel101
        '
        Me.LmTitleLabel101.AutoSize = True
        Me.LmTitleLabel101.AutoSizeDef = True
        Me.LmTitleLabel101.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel101.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel101.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel101.EnableStatus = False
        Me.LmTitleLabel101.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel101.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel101.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel101.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel101.HeightDef = 13
        Me.LmTitleLabel101.Location = New System.Drawing.Point(673, 54)
        Me.LmTitleLabel101.Name = "LmTitleLabel101"
        Me.LmTitleLabel101.Size = New System.Drawing.Size(14, 13)
        Me.LmTitleLabel101.TabIndex = 262
        Me.LmTitleLabel101.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel101.TextValue = ""
        Me.LmTitleLabel101.WidthDef = 14
        '
        'numSumAnt_tabFreeM
        '
        Me.numSumAnt_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumAnt_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumAnt_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumAnt_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumAnt_tabFreeM.HeightDef = 18
        Me.numSumAnt_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumAnt_tabFreeM.HissuLabelVisible = False
        Me.numSumAnt_tabFreeM.IsHissuCheck = False
        Me.numSumAnt_tabFreeM.IsRangeCheck = False
        Me.numSumAnt_tabFreeM.ItemName = ""
        Me.numSumAnt_tabFreeM.Location = New System.Drawing.Point(132, 72)
        Me.numSumAnt_tabFreeM.Name = "numSumAnt_tabFreeM"
        Me.numSumAnt_tabFreeM.ReadOnly = True
        Me.numSumAnt_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numSumAnt_tabFreeM.TabIndex = 270
        Me.numSumAnt_tabFreeM.TabStop = False
        Me.numSumAnt_tabFreeM.TabStopSetting = False
        Me.numSumAnt_tabFreeM.TextValue = "0"
        Me.numSumAnt_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumAnt_tabFreeM.WidthDef = 97
        '
        'numSumCnt_tabFreeM
        '
        Me.numSumCnt_tabFreeM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSumCnt_tabFreeM.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSumCnt_tabFreeM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt_tabFreeM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSumCnt_tabFreeM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt_tabFreeM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSumCnt_tabFreeM.HeightDef = 18
        Me.numSumCnt_tabFreeM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSumCnt_tabFreeM.HissuLabelVisible = False
        Me.numSumCnt_tabFreeM.IsHissuCheck = False
        Me.numSumCnt_tabFreeM.IsRangeCheck = False
        Me.numSumCnt_tabFreeM.ItemName = ""
        Me.numSumCnt_tabFreeM.Location = New System.Drawing.Point(356, 72)
        Me.numSumCnt_tabFreeM.Name = "numSumCnt_tabFreeM"
        Me.numSumCnt_tabFreeM.ReadOnly = True
        Me.numSumCnt_tabFreeM.Size = New System.Drawing.Size(97, 18)
        Me.numSumCnt_tabFreeM.TabIndex = 271
        Me.numSumCnt_tabFreeM.TabStop = False
        Me.numSumCnt_tabFreeM.TabStopSetting = False
        Me.numSumCnt_tabFreeM.TextValue = "0"
        Me.numSumCnt_tabFreeM.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSumCnt_tabFreeM.WidthDef = 97
        '
        'txtCustCD_L
        '
        Me.txtCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_L.CountWrappedLine = False
        Me.txtCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_L.HeightDef = 18
        Me.txtCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.HissuLabelVisible = False
        Me.txtCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustCD_L.IsByteCheck = 0
        Me.txtCustCD_L.IsCalendarCheck = False
        Me.txtCustCD_L.IsDakutenCheck = False
        Me.txtCustCD_L.IsEisuCheck = False
        Me.txtCustCD_L.IsForbiddenWordsCheck = False
        Me.txtCustCD_L.IsFullByteCheck = 0
        Me.txtCustCD_L.IsHankakuCheck = False
        Me.txtCustCD_L.IsHissuCheck = False
        Me.txtCustCD_L.IsKanaCheck = False
        Me.txtCustCD_L.IsMiddleSpace = False
        Me.txtCustCD_L.IsNumericCheck = False
        Me.txtCustCD_L.IsSujiCheck = False
        Me.txtCustCD_L.IsZenkakuCheck = False
        Me.txtCustCD_L.ItemName = ""
        Me.txtCustCD_L.LineSpace = 0
        Me.txtCustCD_L.Location = New System.Drawing.Point(84, 34)
        Me.txtCustCD_L.MaxLength = 0
        Me.txtCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_L.MaxLineCount = 0
        Me.txtCustCD_L.Multiline = False
        Me.txtCustCD_L.Name = "txtCustCD_L"
        Me.txtCustCD_L.ReadOnly = True
        Me.txtCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_L.Size = New System.Drawing.Size(70, 18)
        Me.txtCustCD_L.TabIndex = 95
        Me.txtCustCD_L.TabStop = False
        Me.txtCustCD_L.TabStopSetting = False
        Me.txtCustCD_L.TextValue = "XXXXX"
        Me.txtCustCD_L.UseSystemPasswordChar = False
        Me.txtCustCD_L.WidthDef = 70
        Me.txtCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel21
        '
        Me.LmTitleLabel21.AutoSize = True
        Me.LmTitleLabel21.AutoSizeDef = True
        Me.LmTitleLabel21.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel21.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel21.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel21.EnableStatus = False
        Me.LmTitleLabel21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel21.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel21.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel21.HeightDef = 13
        Me.LmTitleLabel21.Location = New System.Drawing.Point(44, 13)
        Me.LmTitleLabel21.Name = "LmTitleLabel21"
        Me.LmTitleLabel21.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel21.TabIndex = 117
        Me.LmTitleLabel21.Text = "種別"
        Me.LmTitleLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel21.TextValue = "種別"
        Me.LmTitleLabel21.WidthDef = 35
        '
        'LmTitleLabel56
        '
        Me.LmTitleLabel56.AutoSize = True
        Me.LmTitleLabel56.AutoSizeDef = True
        Me.LmTitleLabel56.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel56.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel56.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel56.EnableStatus = False
        Me.LmTitleLabel56.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel56.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel56.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel56.HeightDef = 13
        Me.LmTitleLabel56.Location = New System.Drawing.Point(71, 340)
        Me.LmTitleLabel56.Name = "LmTitleLabel56"
        Me.LmTitleLabel56.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel56.TabIndex = 83
        Me.LmTitleLabel56.Text = "項目名"
        Me.LmTitleLabel56.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel56.TextValue = "項目名"
        Me.LmTitleLabel56.WidthDef = 49
        '
        'txtKomokuNM
        '
        Me.txtKomokuNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKomokuNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKomokuNM.CountWrappedLine = False
        Me.txtKomokuNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKomokuNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKomokuNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKomokuNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKomokuNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKomokuNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKomokuNM.HeightDef = 18
        Me.txtKomokuNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuNM.HissuLabelVisible = False
        Me.txtKomokuNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKomokuNM.IsByteCheck = 0
        Me.txtKomokuNM.IsCalendarCheck = False
        Me.txtKomokuNM.IsDakutenCheck = False
        Me.txtKomokuNM.IsEisuCheck = False
        Me.txtKomokuNM.IsForbiddenWordsCheck = False
        Me.txtKomokuNM.IsFullByteCheck = 0
        Me.txtKomokuNM.IsHankakuCheck = False
        Me.txtKomokuNM.IsHissuCheck = False
        Me.txtKomokuNM.IsKanaCheck = False
        Me.txtKomokuNM.IsMiddleSpace = False
        Me.txtKomokuNM.IsNumericCheck = False
        Me.txtKomokuNM.IsSujiCheck = False
        Me.txtKomokuNM.IsZenkakuCheck = False
        Me.txtKomokuNM.ItemName = ""
        Me.txtKomokuNM.LineSpace = 0
        Me.txtKomokuNM.Location = New System.Drawing.Point(127, 337)
        Me.txtKomokuNM.MaxLength = 0
        Me.txtKomokuNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKomokuNM.MaxLineCount = 0
        Me.txtKomokuNM.Multiline = False
        Me.txtKomokuNM.Name = "txtKomokuNM"
        Me.txtKomokuNM.ReadOnly = True
        Me.txtKomokuNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKomokuNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKomokuNM.Size = New System.Drawing.Size(464, 18)
        Me.txtKomokuNM.TabIndex = 82
        Me.txtKomokuNM.TabStop = False
        Me.txtKomokuNM.TabStopSetting = False
        Me.txtKomokuNM.TextValue = ""
        Me.txtKomokuNM.UseSystemPasswordChar = False
        Me.txtKomokuNM.WidthDef = 464
        Me.txtKomokuNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel31
        '
        Me.LmTitleLabel31.AutoSize = True
        Me.LmTitleLabel31.AutoSizeDef = True
        Me.LmTitleLabel31.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel31.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel31.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel31.EnableStatus = False
        Me.LmTitleLabel31.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel31.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel31.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel31.HeightDef = 13
        Me.LmTitleLabel31.Location = New System.Drawing.Point(15, 410)
        Me.LmTitleLabel31.Name = "LmTitleLabel31"
        Me.LmTitleLabel31.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel31.TabIndex = 111
        Me.LmTitleLabel31.Text = "ワーニング内容"
        Me.LmTitleLabel31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel31.TextValue = "ワーニング内容"
        Me.LmTitleLabel31.WidthDef = 105
        '
        'txtWarning
        '
        Me.txtWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWarning.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWarning.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWarning.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtWarning.CountWrappedLine = False
        Me.txtWarning.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWarning.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarning.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarning.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarning.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWarning.HeightDef = 140
        Me.txtWarning.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWarning.HissuLabelVisible = False
        Me.txtWarning.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtWarning.IsByteCheck = 0
        Me.txtWarning.IsCalendarCheck = False
        Me.txtWarning.IsDakutenCheck = False
        Me.txtWarning.IsEisuCheck = False
        Me.txtWarning.IsForbiddenWordsCheck = False
        Me.txtWarning.IsFullByteCheck = 0
        Me.txtWarning.IsHankakuCheck = False
        Me.txtWarning.IsHissuCheck = False
        Me.txtWarning.IsKanaCheck = False
        Me.txtWarning.IsMiddleSpace = False
        Me.txtWarning.IsNumericCheck = False
        Me.txtWarning.IsSujiCheck = False
        Me.txtWarning.IsZenkakuCheck = False
        Me.txtWarning.ItemName = ""
        Me.txtWarning.LineSpace = 0
        Me.txtWarning.Location = New System.Drawing.Point(127, 406)
        Me.txtWarning.MaxLength = 0
        Me.txtWarning.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWarning.MaxLineCount = 0
        Me.txtWarning.Multiline = True
        Me.txtWarning.Name = "txtWarning"
        Me.txtWarning.ReadOnly = True
        Me.txtWarning.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWarning.Size = New System.Drawing.Size(886, 140)
        Me.txtWarning.TabIndex = 113
        Me.txtWarning.TabStop = False
        Me.txtWarning.TabStopSetting = False
        Me.txtWarning.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtWarning.UseSystemPasswordChar = False
        Me.txtWarning.WidthDef = 886
        Me.txtWarning.WrapMode = GrapeCity.Win.Editors.WrapMode.NoWrap
        '
        'grpWarning
        '
        Me.grpWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpWarning.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpWarning.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpWarning.Controls.Add(Me.pnlDest)
        Me.grpWarning.Controls.Add(Me.txtMastVal)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel17)
        Me.grpWarning.Controls.Add(Me.txtOrderNoM)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel16)
        Me.grpWarning.Controls.Add(Me.txtKomokuVal)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel13)
        Me.grpWarning.Controls.Add(Me.txtEdiKanriNoM)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel11)
        Me.grpWarning.Controls.Add(Me.txtEdiKanriNoL)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel14)
        Me.grpWarning.Controls.Add(Me.txtOrderNoL)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel15)
        Me.grpWarning.Controls.Add(Me.txtWarning)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel31)
        Me.grpWarning.Controls.Add(Me.txtKomokuNM)
        Me.grpWarning.Controls.Add(Me.sprWarning)
        Me.grpWarning.Controls.Add(Me.LmTitleLabel56)
        Me.grpWarning.EnableStatus = False
        Me.grpWarning.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpWarning.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpWarning.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpWarning.HeightDef = 552
        Me.grpWarning.Location = New System.Drawing.Point(5, 68)
        Me.grpWarning.Name = "grpWarning"
        Me.grpWarning.Size = New System.Drawing.Size(1001, 552)
        Me.grpWarning.TabIndex = 119
        Me.grpWarning.TabStop = False
        Me.grpWarning.Text = "ワーニング"
        Me.grpWarning.TextValue = "ワーニング"
        Me.grpWarning.WidthDef = 1001
        '
        'pnlDest
        '
        Me.pnlDest.Controls.Add(Me.txtDestWarning)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel12)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel10)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel6)
        Me.pnlDest.Controls.Add(Me.txtDestJisM)
        Me.pnlDest.Controls.Add(Me.txtDestJisE)
        Me.pnlDest.Controls.Add(Me.txtDestTelM)
        Me.pnlDest.Controls.Add(Me.txtDestTelE)
        Me.pnlDest.Controls.Add(Me.txtDestZipM)
        Me.pnlDest.Controls.Add(Me.txtDestZipE)
        Me.pnlDest.Controls.Add(Me.txtDestAdM)
        Me.pnlDest.Controls.Add(Me.txtDestAdE)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel5)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel4)
        Me.pnlDest.Controls.Add(Me.LmTitleLabel3)
        Me.pnlDest.Controls.Add(Me.txtDestNmM)
        Me.pnlDest.Controls.Add(Me.txtDestNmE)
        Me.pnlDest.Location = New System.Drawing.Point(18, 437)
        Me.pnlDest.Name = "pnlDest"
        Me.pnlDest.Size = New System.Drawing.Size(84, 90)
        Me.pnlDest.TabIndex = 133
        '
        'txtDestWarning
        '
        Me.txtDestWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestWarning.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestWarning.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestWarning.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtDestWarning.CountWrappedLine = False
        Me.txtDestWarning.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestWarning.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestWarning.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestWarning.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestWarning.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestWarning.HeightDef = 73
        Me.txtDestWarning.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestWarning.HissuLabelVisible = False
        Me.txtDestWarning.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestWarning.IsByteCheck = 0
        Me.txtDestWarning.IsCalendarCheck = False
        Me.txtDestWarning.IsDakutenCheck = False
        Me.txtDestWarning.IsEisuCheck = False
        Me.txtDestWarning.IsForbiddenWordsCheck = False
        Me.txtDestWarning.IsFullByteCheck = 0
        Me.txtDestWarning.IsHankakuCheck = False
        Me.txtDestWarning.IsHissuCheck = False
        Me.txtDestWarning.IsKanaCheck = False
        Me.txtDestWarning.IsMiddleSpace = False
        Me.txtDestWarning.IsNumericCheck = False
        Me.txtDestWarning.IsSujiCheck = False
        Me.txtDestWarning.IsZenkakuCheck = False
        Me.txtDestWarning.ItemName = ""
        Me.txtDestWarning.LineSpace = 0
        Me.txtDestWarning.Location = New System.Drawing.Point(119, 134)
        Me.txtDestWarning.MaxLength = 0
        Me.txtDestWarning.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestWarning.MaxLineCount = 0
        Me.txtDestWarning.Multiline = True
        Me.txtDestWarning.Name = "txtDestWarning"
        Me.txtDestWarning.ReadOnly = True
        Me.txtDestWarning.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDestWarning.Size = New System.Drawing.Size(865, 73)
        Me.txtDestWarning.TabIndex = 146
        Me.txtDestWarning.TabStop = False
        Me.txtDestWarning.TabStopSetting = False
        Me.txtDestWarning.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtDestWarning.UseSystemPasswordChar = False
        Me.txtDestWarning.WidthDef = 865
        Me.txtDestWarning.WrapMode = GrapeCity.Win.Editors.WrapMode.NoWrap
        '
        'LmTitleLabel12
        '
        Me.LmTitleLabel12.AutoSize = True
        Me.LmTitleLabel12.AutoSizeDef = True
        Me.LmTitleLabel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel12.EnableStatus = False
        Me.LmTitleLabel12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.HeightDef = 13
        Me.LmTitleLabel12.Location = New System.Drawing.Point(8, 136)
        Me.LmTitleLabel12.Name = "LmTitleLabel12"
        Me.LmTitleLabel12.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel12.TabIndex = 145
        Me.LmTitleLabel12.Text = "ワーニング内容"
        Me.LmTitleLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel12.TextValue = "ワーニング内容"
        Me.LmTitleLabel12.WidthDef = 105
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSize = True
        Me.LmTitleLabel10.AutoSizeDef = True
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 13
        Me.LmTitleLabel10.Location = New System.Drawing.Point(15, 112)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(98, 13)
        Me.LmTitleLabel10.TabIndex = 144
        Me.LmTitleLabel10.Text = "郵便/電話/JIS"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "郵便/電話/JIS"
        Me.LmTitleLabel10.WidthDef = 98
        '
        'LmTitleLabel6
        '
        Me.LmTitleLabel6.AutoSize = True
        Me.LmTitleLabel6.AutoSizeDef = True
        Me.LmTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel6.EnableStatus = False
        Me.LmTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.HeightDef = 13
        Me.LmTitleLabel6.Location = New System.Drawing.Point(50, 53)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel6.TabIndex = 141
        Me.LmTitleLabel6.Text = "届先住所"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "届先住所"
        Me.LmTitleLabel6.WidthDef = 63
        '
        'txtDestJisM
        '
        Me.txtDestJisM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestJisM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestJisM.CountWrappedLine = False
        Me.txtDestJisM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestJisM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestJisM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestJisM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestJisM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestJisM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestJisM.HeightDef = 18
        Me.txtDestJisM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisM.HissuLabelVisible = False
        Me.txtDestJisM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestJisM.IsByteCheck = 0
        Me.txtDestJisM.IsCalendarCheck = False
        Me.txtDestJisM.IsDakutenCheck = False
        Me.txtDestJisM.IsEisuCheck = False
        Me.txtDestJisM.IsForbiddenWordsCheck = False
        Me.txtDestJisM.IsFullByteCheck = 0
        Me.txtDestJisM.IsHankakuCheck = False
        Me.txtDestJisM.IsHissuCheck = False
        Me.txtDestJisM.IsKanaCheck = False
        Me.txtDestJisM.IsMiddleSpace = False
        Me.txtDestJisM.IsNumericCheck = False
        Me.txtDestJisM.IsSujiCheck = False
        Me.txtDestJisM.IsZenkakuCheck = False
        Me.txtDestJisM.ItemName = ""
        Me.txtDestJisM.LineSpace = 0
        Me.txtDestJisM.Location = New System.Drawing.Point(822, 110)
        Me.txtDestJisM.MaxLength = 0
        Me.txtDestJisM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestJisM.MaxLineCount = 0
        Me.txtDestJisM.Multiline = False
        Me.txtDestJisM.Name = "txtDestJisM"
        Me.txtDestJisM.ReadOnly = True
        Me.txtDestJisM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestJisM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestJisM.Size = New System.Drawing.Size(145, 18)
        Me.txtDestJisM.TabIndex = 140
        Me.txtDestJisM.TabStop = False
        Me.txtDestJisM.TabStopSetting = False
        Me.txtDestJisM.TextValue = ""
        Me.txtDestJisM.UseSystemPasswordChar = False
        Me.txtDestJisM.WidthDef = 145
        Me.txtDestJisM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestJisE
        '
        Me.txtDestJisE.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisE.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisE.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestJisE.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestJisE.CountWrappedLine = False
        Me.txtDestJisE.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestJisE.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestJisE.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestJisE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestJisE.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestJisE.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestJisE.HeightDef = 18
        Me.txtDestJisE.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestJisE.HissuLabelVisible = False
        Me.txtDestJisE.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestJisE.IsByteCheck = 0
        Me.txtDestJisE.IsCalendarCheck = False
        Me.txtDestJisE.IsDakutenCheck = False
        Me.txtDestJisE.IsEisuCheck = False
        Me.txtDestJisE.IsForbiddenWordsCheck = False
        Me.txtDestJisE.IsFullByteCheck = 0
        Me.txtDestJisE.IsHankakuCheck = False
        Me.txtDestJisE.IsHissuCheck = False
        Me.txtDestJisE.IsKanaCheck = False
        Me.txtDestJisE.IsMiddleSpace = False
        Me.txtDestJisE.IsNumericCheck = False
        Me.txtDestJisE.IsSujiCheck = False
        Me.txtDestJisE.IsZenkakuCheck = False
        Me.txtDestJisE.ItemName = ""
        Me.txtDestJisE.LineSpace = 0
        Me.txtDestJisE.Location = New System.Drawing.Point(392, 110)
        Me.txtDestJisE.MaxLength = 0
        Me.txtDestJisE.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestJisE.MaxLineCount = 0
        Me.txtDestJisE.Multiline = False
        Me.txtDestJisE.Name = "txtDestJisE"
        Me.txtDestJisE.ReadOnly = True
        Me.txtDestJisE.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestJisE.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestJisE.Size = New System.Drawing.Size(145, 18)
        Me.txtDestJisE.TabIndex = 139
        Me.txtDestJisE.TabStop = False
        Me.txtDestJisE.TabStopSetting = False
        Me.txtDestJisE.TextValue = ""
        Me.txtDestJisE.UseSystemPasswordChar = False
        Me.txtDestJisE.WidthDef = 145
        Me.txtDestJisE.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestTelM
        '
        Me.txtDestTelM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestTelM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestTelM.CountWrappedLine = False
        Me.txtDestTelM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestTelM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTelM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTelM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTelM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTelM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestTelM.HeightDef = 18
        Me.txtDestTelM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelM.HissuLabelVisible = False
        Me.txtDestTelM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestTelM.IsByteCheck = 0
        Me.txtDestTelM.IsCalendarCheck = False
        Me.txtDestTelM.IsDakutenCheck = False
        Me.txtDestTelM.IsEisuCheck = False
        Me.txtDestTelM.IsForbiddenWordsCheck = False
        Me.txtDestTelM.IsFullByteCheck = 0
        Me.txtDestTelM.IsHankakuCheck = False
        Me.txtDestTelM.IsHissuCheck = False
        Me.txtDestTelM.IsKanaCheck = False
        Me.txtDestTelM.IsMiddleSpace = False
        Me.txtDestTelM.IsNumericCheck = False
        Me.txtDestTelM.IsSujiCheck = False
        Me.txtDestTelM.IsZenkakuCheck = False
        Me.txtDestTelM.ItemName = ""
        Me.txtDestTelM.LineSpace = 0
        Me.txtDestTelM.Location = New System.Drawing.Point(685, 110)
        Me.txtDestTelM.MaxLength = 0
        Me.txtDestTelM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestTelM.MaxLineCount = 0
        Me.txtDestTelM.Multiline = False
        Me.txtDestTelM.Name = "txtDestTelM"
        Me.txtDestTelM.ReadOnly = True
        Me.txtDestTelM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestTelM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestTelM.Size = New System.Drawing.Size(146, 18)
        Me.txtDestTelM.TabIndex = 138
        Me.txtDestTelM.TabStop = False
        Me.txtDestTelM.TabStopSetting = False
        Me.txtDestTelM.TextValue = ""
        Me.txtDestTelM.UseSystemPasswordChar = False
        Me.txtDestTelM.WidthDef = 146
        Me.txtDestTelM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestTelE
        '
        Me.txtDestTelE.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelE.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelE.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestTelE.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestTelE.CountWrappedLine = False
        Me.txtDestTelE.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestTelE.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTelE.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestTelE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTelE.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestTelE.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestTelE.HeightDef = 18
        Me.txtDestTelE.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestTelE.HissuLabelVisible = False
        Me.txtDestTelE.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestTelE.IsByteCheck = 0
        Me.txtDestTelE.IsCalendarCheck = False
        Me.txtDestTelE.IsDakutenCheck = False
        Me.txtDestTelE.IsEisuCheck = False
        Me.txtDestTelE.IsForbiddenWordsCheck = False
        Me.txtDestTelE.IsFullByteCheck = 0
        Me.txtDestTelE.IsHankakuCheck = False
        Me.txtDestTelE.IsHissuCheck = False
        Me.txtDestTelE.IsKanaCheck = False
        Me.txtDestTelE.IsMiddleSpace = False
        Me.txtDestTelE.IsNumericCheck = False
        Me.txtDestTelE.IsSujiCheck = False
        Me.txtDestTelE.IsZenkakuCheck = False
        Me.txtDestTelE.ItemName = ""
        Me.txtDestTelE.LineSpace = 0
        Me.txtDestTelE.Location = New System.Drawing.Point(255, 110)
        Me.txtDestTelE.MaxLength = 0
        Me.txtDestTelE.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestTelE.MaxLineCount = 0
        Me.txtDestTelE.Multiline = False
        Me.txtDestTelE.Name = "txtDestTelE"
        Me.txtDestTelE.ReadOnly = True
        Me.txtDestTelE.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestTelE.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestTelE.Size = New System.Drawing.Size(146, 18)
        Me.txtDestTelE.TabIndex = 137
        Me.txtDestTelE.TabStop = False
        Me.txtDestTelE.TabStopSetting = False
        Me.txtDestTelE.TextValue = ""
        Me.txtDestTelE.UseSystemPasswordChar = False
        Me.txtDestTelE.WidthDef = 146
        Me.txtDestTelE.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestZipM
        '
        Me.txtDestZipM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestZipM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestZipM.CountWrappedLine = False
        Me.txtDestZipM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestZipM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZipM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZipM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZipM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZipM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestZipM.HeightDef = 18
        Me.txtDestZipM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipM.HissuLabelVisible = False
        Me.txtDestZipM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestZipM.IsByteCheck = 0
        Me.txtDestZipM.IsCalendarCheck = False
        Me.txtDestZipM.IsDakutenCheck = False
        Me.txtDestZipM.IsEisuCheck = False
        Me.txtDestZipM.IsForbiddenWordsCheck = False
        Me.txtDestZipM.IsFullByteCheck = 0
        Me.txtDestZipM.IsHankakuCheck = False
        Me.txtDestZipM.IsHissuCheck = False
        Me.txtDestZipM.IsKanaCheck = False
        Me.txtDestZipM.IsMiddleSpace = False
        Me.txtDestZipM.IsNumericCheck = False
        Me.txtDestZipM.IsSujiCheck = False
        Me.txtDestZipM.IsZenkakuCheck = False
        Me.txtDestZipM.ItemName = ""
        Me.txtDestZipM.LineSpace = 0
        Me.txtDestZipM.Location = New System.Drawing.Point(549, 110)
        Me.txtDestZipM.MaxLength = 0
        Me.txtDestZipM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestZipM.MaxLineCount = 0
        Me.txtDestZipM.Multiline = False
        Me.txtDestZipM.Name = "txtDestZipM"
        Me.txtDestZipM.ReadOnly = True
        Me.txtDestZipM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestZipM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestZipM.Size = New System.Drawing.Size(145, 18)
        Me.txtDestZipM.TabIndex = 136
        Me.txtDestZipM.TabStop = False
        Me.txtDestZipM.TabStopSetting = False
        Me.txtDestZipM.TextValue = ""
        Me.txtDestZipM.UseSystemPasswordChar = False
        Me.txtDestZipM.WidthDef = 145
        Me.txtDestZipM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestZipE
        '
        Me.txtDestZipE.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipE.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipE.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestZipE.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestZipE.CountWrappedLine = False
        Me.txtDestZipE.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestZipE.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZipE.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZipE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZipE.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZipE.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestZipE.HeightDef = 18
        Me.txtDestZipE.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZipE.HissuLabelVisible = False
        Me.txtDestZipE.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestZipE.IsByteCheck = 0
        Me.txtDestZipE.IsCalendarCheck = False
        Me.txtDestZipE.IsDakutenCheck = False
        Me.txtDestZipE.IsEisuCheck = False
        Me.txtDestZipE.IsForbiddenWordsCheck = False
        Me.txtDestZipE.IsFullByteCheck = 0
        Me.txtDestZipE.IsHankakuCheck = False
        Me.txtDestZipE.IsHissuCheck = False
        Me.txtDestZipE.IsKanaCheck = False
        Me.txtDestZipE.IsMiddleSpace = False
        Me.txtDestZipE.IsNumericCheck = False
        Me.txtDestZipE.IsSujiCheck = False
        Me.txtDestZipE.IsZenkakuCheck = False
        Me.txtDestZipE.ItemName = ""
        Me.txtDestZipE.LineSpace = 0
        Me.txtDestZipE.Location = New System.Drawing.Point(119, 110)
        Me.txtDestZipE.MaxLength = 0
        Me.txtDestZipE.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestZipE.MaxLineCount = 0
        Me.txtDestZipE.Multiline = False
        Me.txtDestZipE.Name = "txtDestZipE"
        Me.txtDestZipE.ReadOnly = True
        Me.txtDestZipE.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestZipE.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestZipE.Size = New System.Drawing.Size(145, 18)
        Me.txtDestZipE.TabIndex = 135
        Me.txtDestZipE.TabStop = False
        Me.txtDestZipE.TabStopSetting = False
        Me.txtDestZipE.TextValue = ""
        Me.txtDestZipE.UseSystemPasswordChar = False
        Me.txtDestZipE.WidthDef = 145
        Me.txtDestZipE.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestAdM
        '
        Me.txtDestAdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestAdM.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtDestAdM.CountWrappedLine = False
        Me.txtDestAdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestAdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestAdM.HeightDef = 53
        Me.txtDestAdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdM.HissuLabelVisible = False
        Me.txtDestAdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestAdM.IsByteCheck = 0
        Me.txtDestAdM.IsCalendarCheck = False
        Me.txtDestAdM.IsDakutenCheck = False
        Me.txtDestAdM.IsEisuCheck = False
        Me.txtDestAdM.IsForbiddenWordsCheck = False
        Me.txtDestAdM.IsFullByteCheck = 0
        Me.txtDestAdM.IsHankakuCheck = False
        Me.txtDestAdM.IsHissuCheck = False
        Me.txtDestAdM.IsKanaCheck = False
        Me.txtDestAdM.IsMiddleSpace = False
        Me.txtDestAdM.IsNumericCheck = False
        Me.txtDestAdM.IsSujiCheck = False
        Me.txtDestAdM.IsZenkakuCheck = False
        Me.txtDestAdM.ItemName = ""
        Me.txtDestAdM.LineSpace = 0
        Me.txtDestAdM.Location = New System.Drawing.Point(549, 51)
        Me.txtDestAdM.MaxLength = 0
        Me.txtDestAdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestAdM.MaxLineCount = 0
        Me.txtDestAdM.Multiline = True
        Me.txtDestAdM.Name = "txtDestAdM"
        Me.txtDestAdM.ReadOnly = True
        Me.txtDestAdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestAdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestAdM.Size = New System.Drawing.Size(418, 53)
        Me.txtDestAdM.TabIndex = 134
        Me.txtDestAdM.TabStop = False
        Me.txtDestAdM.TabStopSetting = False
        Me.txtDestAdM.TextValue = ""
        Me.txtDestAdM.UseSystemPasswordChar = False
        Me.txtDestAdM.WidthDef = 418
        Me.txtDestAdM.WrapMode = GrapeCity.Win.Editors.WrapMode.NoWrap
        '
        'txtDestAdE
        '
        Me.txtDestAdE.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdE.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdE.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestAdE.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtDestAdE.CountWrappedLine = False
        Me.txtDestAdE.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestAdE.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdE.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestAdE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdE.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestAdE.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestAdE.HeightDef = 53
        Me.txtDestAdE.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestAdE.HissuLabelVisible = False
        Me.txtDestAdE.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestAdE.IsByteCheck = 0
        Me.txtDestAdE.IsCalendarCheck = False
        Me.txtDestAdE.IsDakutenCheck = False
        Me.txtDestAdE.IsEisuCheck = False
        Me.txtDestAdE.IsForbiddenWordsCheck = False
        Me.txtDestAdE.IsFullByteCheck = 0
        Me.txtDestAdE.IsHankakuCheck = False
        Me.txtDestAdE.IsHissuCheck = False
        Me.txtDestAdE.IsKanaCheck = False
        Me.txtDestAdE.IsMiddleSpace = False
        Me.txtDestAdE.IsNumericCheck = False
        Me.txtDestAdE.IsSujiCheck = False
        Me.txtDestAdE.IsZenkakuCheck = False
        Me.txtDestAdE.ItemName = ""
        Me.txtDestAdE.LineSpace = 0
        Me.txtDestAdE.Location = New System.Drawing.Point(119, 51)
        Me.txtDestAdE.MaxLength = 0
        Me.txtDestAdE.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestAdE.MaxLineCount = 0
        Me.txtDestAdE.Multiline = True
        Me.txtDestAdE.Name = "txtDestAdE"
        Me.txtDestAdE.ReadOnly = True
        Me.txtDestAdE.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestAdE.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestAdE.Size = New System.Drawing.Size(418, 53)
        Me.txtDestAdE.TabIndex = 133
        Me.txtDestAdE.TabStop = False
        Me.txtDestAdE.TabStopSetting = False
        Me.txtDestAdE.TextValue = ""
        Me.txtDestAdE.UseSystemPasswordChar = False
        Me.txtDestAdE.WidthDef = 418
        Me.txtDestAdE.WrapMode = GrapeCity.Win.Editors.WrapMode.NoWrap
        '
        'LmTitleLabel5
        '
        Me.LmTitleLabel5.AutoSize = True
        Me.LmTitleLabel5.AutoSizeDef = True
        Me.LmTitleLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel5.EnableStatus = False
        Me.LmTitleLabel5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.HeightDef = 13
        Me.LmTitleLabel5.Location = New System.Drawing.Point(546, 8)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(67, 13)
        Me.LmTitleLabel5.TabIndex = 132
        Me.LmTitleLabel5.Text = "マスタ値"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel5.TextValue = "マスタ値"
        Me.LmTitleLabel5.WidthDef = 67
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSize = True
        Me.LmTitleLabel4.AutoSizeDef = True
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 13
        Me.LmTitleLabel4.Location = New System.Drawing.Point(118, 8)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(46, 13)
        Me.LmTitleLabel4.TabIndex = 131
        Me.LmTitleLabel4.Text = "EDI値"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel4.TextValue = "EDI値"
        Me.LmTitleLabel4.WidthDef = 46
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(64, 29)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 130
        Me.LmTitleLabel3.Text = "届先名"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "届先名"
        Me.LmTitleLabel3.WidthDef = 49
        '
        'txtDestNmM
        '
        Me.txtDestNmM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestNmM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestNmM.CountWrappedLine = False
        Me.txtDestNmM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestNmM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNmM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNmM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNmM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNmM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestNmM.HeightDef = 18
        Me.txtDestNmM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmM.HissuLabelVisible = False
        Me.txtDestNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestNmM.IsByteCheck = 0
        Me.txtDestNmM.IsCalendarCheck = False
        Me.txtDestNmM.IsDakutenCheck = False
        Me.txtDestNmM.IsEisuCheck = False
        Me.txtDestNmM.IsForbiddenWordsCheck = False
        Me.txtDestNmM.IsFullByteCheck = 0
        Me.txtDestNmM.IsHankakuCheck = False
        Me.txtDestNmM.IsHissuCheck = False
        Me.txtDestNmM.IsKanaCheck = False
        Me.txtDestNmM.IsMiddleSpace = False
        Me.txtDestNmM.IsNumericCheck = False
        Me.txtDestNmM.IsSujiCheck = False
        Me.txtDestNmM.IsZenkakuCheck = False
        Me.txtDestNmM.ItemName = ""
        Me.txtDestNmM.LineSpace = 0
        Me.txtDestNmM.Location = New System.Drawing.Point(549, 27)
        Me.txtDestNmM.MaxLength = 0
        Me.txtDestNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestNmM.MaxLineCount = 0
        Me.txtDestNmM.Multiline = False
        Me.txtDestNmM.Name = "txtDestNmM"
        Me.txtDestNmM.ReadOnly = True
        Me.txtDestNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestNmM.Size = New System.Drawing.Size(418, 18)
        Me.txtDestNmM.TabIndex = 129
        Me.txtDestNmM.TabStop = False
        Me.txtDestNmM.TabStopSetting = False
        Me.txtDestNmM.TextValue = ""
        Me.txtDestNmM.UseSystemPasswordChar = False
        Me.txtDestNmM.WidthDef = 418
        Me.txtDestNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDestNmE
        '
        Me.txtDestNmE.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmE.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmE.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestNmE.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestNmE.CountWrappedLine = False
        Me.txtDestNmE.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestNmE.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNmE.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNmE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNmE.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNmE.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestNmE.HeightDef = 18
        Me.txtDestNmE.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNmE.HissuLabelVisible = False
        Me.txtDestNmE.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestNmE.IsByteCheck = 0
        Me.txtDestNmE.IsCalendarCheck = False
        Me.txtDestNmE.IsDakutenCheck = False
        Me.txtDestNmE.IsEisuCheck = False
        Me.txtDestNmE.IsForbiddenWordsCheck = False
        Me.txtDestNmE.IsFullByteCheck = 0
        Me.txtDestNmE.IsHankakuCheck = False
        Me.txtDestNmE.IsHissuCheck = False
        Me.txtDestNmE.IsKanaCheck = False
        Me.txtDestNmE.IsMiddleSpace = False
        Me.txtDestNmE.IsNumericCheck = False
        Me.txtDestNmE.IsSujiCheck = False
        Me.txtDestNmE.IsZenkakuCheck = False
        Me.txtDestNmE.ItemName = ""
        Me.txtDestNmE.LineSpace = 0
        Me.txtDestNmE.Location = New System.Drawing.Point(119, 27)
        Me.txtDestNmE.MaxLength = 0
        Me.txtDestNmE.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestNmE.MaxLineCount = 0
        Me.txtDestNmE.Multiline = False
        Me.txtDestNmE.Name = "txtDestNmE"
        Me.txtDestNmE.ReadOnly = True
        Me.txtDestNmE.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestNmE.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestNmE.Size = New System.Drawing.Size(418, 18)
        Me.txtDestNmE.TabIndex = 128
        Me.txtDestNmE.TabStop = False
        Me.txtDestNmE.TabStopSetting = False
        Me.txtDestNmE.TextValue = ""
        Me.txtDestNmE.UseSystemPasswordChar = False
        Me.txtDestNmE.WidthDef = 418
        Me.txtDestNmE.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtMastVal
        '
        Me.txtMastVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtMastVal.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtMastVal.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMastVal.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtMastVal.CountWrappedLine = False
        Me.txtMastVal.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtMastVal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMastVal.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMastVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMastVal.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtMastVal.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtMastVal.HeightDef = 18
        Me.txtMastVal.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtMastVal.HissuLabelVisible = False
        Me.txtMastVal.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtMastVal.IsByteCheck = 0
        Me.txtMastVal.IsCalendarCheck = False
        Me.txtMastVal.IsDakutenCheck = False
        Me.txtMastVal.IsEisuCheck = False
        Me.txtMastVal.IsForbiddenWordsCheck = False
        Me.txtMastVal.IsFullByteCheck = 0
        Me.txtMastVal.IsHankakuCheck = False
        Me.txtMastVal.IsHissuCheck = False
        Me.txtMastVal.IsKanaCheck = False
        Me.txtMastVal.IsMiddleSpace = False
        Me.txtMastVal.IsNumericCheck = False
        Me.txtMastVal.IsSujiCheck = False
        Me.txtMastVal.IsZenkakuCheck = False
        Me.txtMastVal.ItemName = ""
        Me.txtMastVal.LineSpace = 0
        Me.txtMastVal.Location = New System.Drawing.Point(127, 382)
        Me.txtMastVal.MaxLength = 0
        Me.txtMastVal.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtMastVal.MaxLineCount = 0
        Me.txtMastVal.Multiline = False
        Me.txtMastVal.Name = "txtMastVal"
        Me.txtMastVal.ReadOnly = True
        Me.txtMastVal.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtMastVal.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMastVal.Size = New System.Drawing.Size(869, 18)
        Me.txtMastVal.TabIndex = 131
        Me.txtMastVal.TabStop = False
        Me.txtMastVal.TabStopSetting = False
        Me.txtMastVal.TextValue = ""
        Me.txtMastVal.UseSystemPasswordChar = False
        Me.txtMastVal.WidthDef = 869
        Me.txtMastVal.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel17
        '
        Me.LmTitleLabel17.AutoSize = True
        Me.LmTitleLabel17.AutoSizeDef = True
        Me.LmTitleLabel17.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel17.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel17.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel17.EnableStatus = False
        Me.LmTitleLabel17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel17.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel17.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel17.HeightDef = 13
        Me.LmTitleLabel17.Location = New System.Drawing.Point(58, 384)
        Me.LmTitleLabel17.Name = "LmTitleLabel17"
        Me.LmTitleLabel17.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel17.TabIndex = 132
        Me.LmTitleLabel17.Text = "マスタ値"
        Me.LmTitleLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel17.TextValue = "マスタ値"
        Me.LmTitleLabel17.WidthDef = 63
        '
        'txtOrderNoM
        '
        Me.txtOrderNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNoM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNoM.CountWrappedLine = False
        Me.txtOrderNoM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNoM.HeightDef = 18
        Me.txtOrderNoM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoM.HissuLabelVisible = False
        Me.txtOrderNoM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOrderNoM.IsByteCheck = 30
        Me.txtOrderNoM.IsCalendarCheck = False
        Me.txtOrderNoM.IsDakutenCheck = False
        Me.txtOrderNoM.IsEisuCheck = False
        Me.txtOrderNoM.IsForbiddenWordsCheck = False
        Me.txtOrderNoM.IsFullByteCheck = 0
        Me.txtOrderNoM.IsHankakuCheck = False
        Me.txtOrderNoM.IsHissuCheck = False
        Me.txtOrderNoM.IsKanaCheck = False
        Me.txtOrderNoM.IsMiddleSpace = False
        Me.txtOrderNoM.IsNumericCheck = False
        Me.txtOrderNoM.IsSujiCheck = False
        Me.txtOrderNoM.IsZenkakuCheck = False
        Me.txtOrderNoM.ItemName = ""
        Me.txtOrderNoM.LineSpace = 0
        Me.txtOrderNoM.Location = New System.Drawing.Point(419, 315)
        Me.txtOrderNoM.MaxLength = 30
        Me.txtOrderNoM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNoM.MaxLineCount = 0
        Me.txtOrderNoM.Multiline = False
        Me.txtOrderNoM.Name = "txtOrderNoM"
        Me.txtOrderNoM.ReadOnly = True
        Me.txtOrderNoM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNoM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNoM.Size = New System.Drawing.Size(172, 18)
        Me.txtOrderNoM.TabIndex = 130
        Me.txtOrderNoM.TabStop = False
        Me.txtOrderNoM.TabStopSetting = False
        Me.txtOrderNoM.Tag = ""
        Me.txtOrderNoM.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtOrderNoM.UseSystemPasswordChar = False
        Me.txtOrderNoM.WidthDef = 172
        Me.txtOrderNoM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel16
        '
        Me.LmTitleLabel16.AutoSize = True
        Me.LmTitleLabel16.AutoSizeDef = True
        Me.LmTitleLabel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel16.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel16.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel16.EnableStatus = False
        Me.LmTitleLabel16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel16.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel16.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel16.HeightDef = 13
        Me.LmTitleLabel16.Location = New System.Drawing.Point(298, 318)
        Me.LmTitleLabel16.Name = "LmTitleLabel16"
        Me.LmTitleLabel16.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel16.TabIndex = 129
        Me.LmTitleLabel16.Text = "オーダー番号(中)"
        Me.LmTitleLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel16.TextValue = "オーダー番号(中)"
        Me.LmTitleLabel16.WidthDef = 119
        '
        'txtKomokuVal
        '
        Me.txtKomokuVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuVal.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuVal.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKomokuVal.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKomokuVal.CountWrappedLine = False
        Me.txtKomokuVal.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKomokuVal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKomokuVal.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKomokuVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKomokuVal.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKomokuVal.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKomokuVal.HeightDef = 18
        Me.txtKomokuVal.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKomokuVal.HissuLabelVisible = False
        Me.txtKomokuVal.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKomokuVal.IsByteCheck = 0
        Me.txtKomokuVal.IsCalendarCheck = False
        Me.txtKomokuVal.IsDakutenCheck = False
        Me.txtKomokuVal.IsEisuCheck = False
        Me.txtKomokuVal.IsForbiddenWordsCheck = False
        Me.txtKomokuVal.IsFullByteCheck = 0
        Me.txtKomokuVal.IsHankakuCheck = False
        Me.txtKomokuVal.IsHissuCheck = False
        Me.txtKomokuVal.IsKanaCheck = False
        Me.txtKomokuVal.IsMiddleSpace = False
        Me.txtKomokuVal.IsNumericCheck = False
        Me.txtKomokuVal.IsSujiCheck = False
        Me.txtKomokuVal.IsZenkakuCheck = False
        Me.txtKomokuVal.ItemName = ""
        Me.txtKomokuVal.LineSpace = 0
        Me.txtKomokuVal.Location = New System.Drawing.Point(127, 359)
        Me.txtKomokuVal.MaxLength = 0
        Me.txtKomokuVal.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKomokuVal.MaxLineCount = 0
        Me.txtKomokuVal.Multiline = False
        Me.txtKomokuVal.Name = "txtKomokuVal"
        Me.txtKomokuVal.ReadOnly = True
        Me.txtKomokuVal.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKomokuVal.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKomokuVal.Size = New System.Drawing.Size(869, 18)
        Me.txtKomokuVal.TabIndex = 127
        Me.txtKomokuVal.TabStop = False
        Me.txtKomokuVal.TabStopSetting = False
        Me.txtKomokuVal.TextValue = ""
        Me.txtKomokuVal.UseSystemPasswordChar = False
        Me.txtKomokuVal.WidthDef = 869
        Me.txtKomokuVal.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel13
        '
        Me.LmTitleLabel13.AutoSize = True
        Me.LmTitleLabel13.AutoSizeDef = True
        Me.LmTitleLabel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel13.EnableStatus = False
        Me.LmTitleLabel13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.HeightDef = 13
        Me.LmTitleLabel13.Location = New System.Drawing.Point(72, 362)
        Me.LmTitleLabel13.Name = "LmTitleLabel13"
        Me.LmTitleLabel13.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel13.TabIndex = 128
        Me.LmTitleLabel13.Text = "項目値"
        Me.LmTitleLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel13.TextValue = "項目値"
        Me.LmTitleLabel13.WidthDef = 49
        '
        'txtEdiKanriNoM
        '
        Me.txtEdiKanriNoM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEdiKanriNoM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEdiKanriNoM.CountWrappedLine = False
        Me.txtEdiKanriNoM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEdiKanriNoM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEdiKanriNoM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEdiKanriNoM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEdiKanriNoM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEdiKanriNoM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEdiKanriNoM.HeightDef = 18
        Me.txtEdiKanriNoM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoM.HissuLabelVisible = False
        Me.txtEdiKanriNoM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtEdiKanriNoM.IsByteCheck = 0
        Me.txtEdiKanriNoM.IsCalendarCheck = False
        Me.txtEdiKanriNoM.IsDakutenCheck = False
        Me.txtEdiKanriNoM.IsEisuCheck = False
        Me.txtEdiKanriNoM.IsForbiddenWordsCheck = False
        Me.txtEdiKanriNoM.IsFullByteCheck = 0
        Me.txtEdiKanriNoM.IsHankakuCheck = False
        Me.txtEdiKanriNoM.IsHissuCheck = False
        Me.txtEdiKanriNoM.IsKanaCheck = False
        Me.txtEdiKanriNoM.IsMiddleSpace = False
        Me.txtEdiKanriNoM.IsNumericCheck = False
        Me.txtEdiKanriNoM.IsSujiCheck = False
        Me.txtEdiKanriNoM.IsZenkakuCheck = False
        Me.txtEdiKanriNoM.ItemName = ""
        Me.txtEdiKanriNoM.LineSpace = 0
        Me.txtEdiKanriNoM.Location = New System.Drawing.Point(906, 315)
        Me.txtEdiKanriNoM.MaxLength = 0
        Me.txtEdiKanriNoM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEdiKanriNoM.MaxLineCount = 0
        Me.txtEdiKanriNoM.Multiline = False
        Me.txtEdiKanriNoM.Name = "txtEdiKanriNoM"
        Me.txtEdiKanriNoM.ReadOnly = True
        Me.txtEdiKanriNoM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEdiKanriNoM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEdiKanriNoM.Size = New System.Drawing.Size(90, 18)
        Me.txtEdiKanriNoM.TabIndex = 126
        Me.txtEdiKanriNoM.TabStop = False
        Me.txtEdiKanriNoM.TabStopSetting = False
        Me.txtEdiKanriNoM.TextValue = "XXXXXXX"
        Me.txtEdiKanriNoM.UseSystemPasswordChar = False
        Me.txtEdiKanriNoM.WidthDef = 90
        Me.txtEdiKanriNoM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel11
        '
        Me.LmTitleLabel11.AutoSize = True
        Me.LmTitleLabel11.AutoSizeDef = True
        Me.LmTitleLabel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel11.EnableStatus = False
        Me.LmTitleLabel11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.HeightDef = 13
        Me.LmTitleLabel11.Location = New System.Drawing.Point(795, 318)
        Me.LmTitleLabel11.Name = "LmTitleLabel11"
        Me.LmTitleLabel11.Size = New System.Drawing.Size(112, 13)
        Me.LmTitleLabel11.TabIndex = 125
        Me.LmTitleLabel11.Text = "EDI管理番号(中)"
        Me.LmTitleLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel11.TextValue = "EDI管理番号(中)"
        Me.LmTitleLabel11.WidthDef = 112
        '
        'txtEdiKanriNoL
        '
        Me.txtEdiKanriNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEdiKanriNoL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEdiKanriNoL.CountWrappedLine = False
        Me.txtEdiKanriNoL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtEdiKanriNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEdiKanriNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtEdiKanriNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEdiKanriNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtEdiKanriNoL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtEdiKanriNoL.HeightDef = 18
        Me.txtEdiKanriNoL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtEdiKanriNoL.HissuLabelVisible = False
        Me.txtEdiKanriNoL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtEdiKanriNoL.IsByteCheck = 0
        Me.txtEdiKanriNoL.IsCalendarCheck = False
        Me.txtEdiKanriNoL.IsDakutenCheck = False
        Me.txtEdiKanriNoL.IsEisuCheck = False
        Me.txtEdiKanriNoL.IsForbiddenWordsCheck = False
        Me.txtEdiKanriNoL.IsFullByteCheck = 0
        Me.txtEdiKanriNoL.IsHankakuCheck = False
        Me.txtEdiKanriNoL.IsHissuCheck = False
        Me.txtEdiKanriNoL.IsKanaCheck = False
        Me.txtEdiKanriNoL.IsMiddleSpace = False
        Me.txtEdiKanriNoL.IsNumericCheck = False
        Me.txtEdiKanriNoL.IsSujiCheck = False
        Me.txtEdiKanriNoL.IsZenkakuCheck = False
        Me.txtEdiKanriNoL.ItemName = ""
        Me.txtEdiKanriNoL.LineSpace = 0
        Me.txtEdiKanriNoL.Location = New System.Drawing.Point(708, 315)
        Me.txtEdiKanriNoL.MaxLength = 0
        Me.txtEdiKanriNoL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtEdiKanriNoL.MaxLineCount = 0
        Me.txtEdiKanriNoL.Multiline = False
        Me.txtEdiKanriNoL.Name = "txtEdiKanriNoL"
        Me.txtEdiKanriNoL.ReadOnly = True
        Me.txtEdiKanriNoL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtEdiKanriNoL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEdiKanriNoL.Size = New System.Drawing.Size(90, 18)
        Me.txtEdiKanriNoL.TabIndex = 124
        Me.txtEdiKanriNoL.TabStop = False
        Me.txtEdiKanriNoL.TabStopSetting = False
        Me.txtEdiKanriNoL.TextValue = "XXXXXXX"
        Me.txtEdiKanriNoL.UseSystemPasswordChar = False
        Me.txtEdiKanriNoL.WidthDef = 90
        Me.txtEdiKanriNoL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel14
        '
        Me.LmTitleLabel14.AutoSize = True
        Me.LmTitleLabel14.AutoSizeDef = True
        Me.LmTitleLabel14.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel14.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel14.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel14.EnableStatus = False
        Me.LmTitleLabel14.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel14.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel14.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel14.HeightDef = 13
        Me.LmTitleLabel14.Location = New System.Drawing.Point(595, 318)
        Me.LmTitleLabel14.Name = "LmTitleLabel14"
        Me.LmTitleLabel14.Size = New System.Drawing.Size(112, 13)
        Me.LmTitleLabel14.TabIndex = 123
        Me.LmTitleLabel14.Text = "EDI管理番号(大)"
        Me.LmTitleLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel14.TextValue = "EDI管理番号(大)"
        Me.LmTitleLabel14.WidthDef = 112
        '
        'txtOrderNoL
        '
        Me.txtOrderNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNoL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrderNoL.CountWrappedLine = False
        Me.txtOrderNoL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrderNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrderNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrderNoL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrderNoL.HeightDef = 18
        Me.txtOrderNoL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrderNoL.HissuLabelVisible = False
        Me.txtOrderNoL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOrderNoL.IsByteCheck = 30
        Me.txtOrderNoL.IsCalendarCheck = False
        Me.txtOrderNoL.IsDakutenCheck = False
        Me.txtOrderNoL.IsEisuCheck = False
        Me.txtOrderNoL.IsForbiddenWordsCheck = False
        Me.txtOrderNoL.IsFullByteCheck = 0
        Me.txtOrderNoL.IsHankakuCheck = False
        Me.txtOrderNoL.IsHissuCheck = False
        Me.txtOrderNoL.IsKanaCheck = False
        Me.txtOrderNoL.IsMiddleSpace = False
        Me.txtOrderNoL.IsNumericCheck = False
        Me.txtOrderNoL.IsSujiCheck = False
        Me.txtOrderNoL.IsZenkakuCheck = False
        Me.txtOrderNoL.ItemName = ""
        Me.txtOrderNoL.LineSpace = 0
        Me.txtOrderNoL.Location = New System.Drawing.Point(127, 315)
        Me.txtOrderNoL.MaxLength = 30
        Me.txtOrderNoL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrderNoL.MaxLineCount = 0
        Me.txtOrderNoL.Multiline = False
        Me.txtOrderNoL.Name = "txtOrderNoL"
        Me.txtOrderNoL.ReadOnly = True
        Me.txtOrderNoL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrderNoL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrderNoL.Size = New System.Drawing.Size(172, 18)
        Me.txtOrderNoL.TabIndex = 122
        Me.txtOrderNoL.TabStop = False
        Me.txtOrderNoL.TabStopSetting = False
        Me.txtOrderNoL.Tag = ""
        Me.txtOrderNoL.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtOrderNoL.UseSystemPasswordChar = False
        Me.txtOrderNoL.WidthDef = 172
        Me.txtOrderNoL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel15
        '
        Me.LmTitleLabel15.AutoSize = True
        Me.LmTitleLabel15.AutoSizeDef = True
        Me.LmTitleLabel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel15.EnableStatus = False
        Me.LmTitleLabel15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.HeightDef = 13
        Me.LmTitleLabel15.Location = New System.Drawing.Point(7, 318)
        Me.LmTitleLabel15.Name = "LmTitleLabel15"
        Me.LmTitleLabel15.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel15.TabIndex = 121
        Me.LmTitleLabel15.Text = "オーダー番号(大)"
        Me.LmTitleLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel15.TextValue = "オーダー番号(大)"
        Me.LmTitleLabel15.WidthDef = 119
        '
        'cmbNrsBr
        '
        Me.cmbNrsBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsBr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsBr.DataSource = Nothing
        Me.cmbNrsBr.DisplayMember = Nothing
        Me.cmbNrsBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBr.HeightDef = 18
        Me.cmbNrsBr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.HissuLabelVisible = True
        Me.cmbNrsBr.InsertWildCard = True
        Me.cmbNrsBr.IsForbiddenWordsCheck = False
        Me.cmbNrsBr.IsHissuCheck = True
        Me.cmbNrsBr.ItemName = ""
        Me.cmbNrsBr.Location = New System.Drawing.Point(324, 10)
        Me.cmbNrsBr.Name = "cmbNrsBr"
        Me.cmbNrsBr.ReadOnly = True
        Me.cmbNrsBr.SelectedIndex = -1
        Me.cmbNrsBr.SelectedItem = Nothing
        Me.cmbNrsBr.SelectedText = ""
        Me.cmbNrsBr.SelectedValue = ""
        Me.cmbNrsBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBr.TabIndex = 192
        Me.cmbNrsBr.TabStop = False
        Me.cmbNrsBr.TabStopSetting = False
        Me.cmbNrsBr.TextValue = ""
        Me.cmbNrsBr.ValueMember = Nothing
        Me.cmbNrsBr.WidthDef = 300
        '
        'cmbNrsWh
        '
        Me.cmbNrsWh.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsWh.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsWh.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsWh.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsWh.DataSource = Nothing
        Me.cmbNrsWh.DisplayMember = Nothing
        Me.cmbNrsWh.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsWh.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsWh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsWh.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsWh.HeightDef = 18
        Me.cmbNrsWh.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsWh.HissuLabelVisible = True
        Me.cmbNrsWh.InsertWildCard = True
        Me.cmbNrsWh.IsForbiddenWordsCheck = False
        Me.cmbNrsWh.IsHissuCheck = True
        Me.cmbNrsWh.ItemName = ""
        Me.cmbNrsWh.Location = New System.Drawing.Point(690, 10)
        Me.cmbNrsWh.Name = "cmbNrsWh"
        Me.cmbNrsWh.ReadOnly = True
        Me.cmbNrsWh.SelectedIndex = -1
        Me.cmbNrsWh.SelectedItem = Nothing
        Me.cmbNrsWh.SelectedText = ""
        Me.cmbNrsWh.SelectedValue = ""
        Me.cmbNrsWh.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsWh.TabIndex = 193
        Me.cmbNrsWh.TabStop = False
        Me.cmbNrsWh.TabStopSetting = False
        Me.cmbNrsWh.TextValue = ""
        Me.cmbNrsWh.ValueMember = Nothing
        Me.cmbNrsWh.WidthDef = 300
        '
        'cmbShubetu
        '
        Me.cmbShubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShubetu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShubetu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShubetu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShubetu.DataCode = "E014"
        Me.cmbShubetu.DataSource = Nothing
        Me.cmbShubetu.DisplayMember = Nothing
        Me.cmbShubetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShubetu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShubetu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShubetu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShubetu.HeightDef = 19
        Me.cmbShubetu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShubetu.HissuLabelVisible = False
        Me.cmbShubetu.InsertWildCard = True
        Me.cmbShubetu.IsForbiddenWordsCheck = False
        Me.cmbShubetu.IsHissuCheck = False
        Me.cmbShubetu.ItemName = ""
        Me.cmbShubetu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShubetu.Location = New System.Drawing.Point(84, 9)
        Me.cmbShubetu.Name = "cmbShubetu"
        Me.cmbShubetu.ReadOnly = True
        Me.cmbShubetu.SelectedIndex = -1
        Me.cmbShubetu.SelectedItem = Nothing
        Me.cmbShubetu.SelectedText = ""
        Me.cmbShubetu.SelectedValue = ""
        Me.cmbShubetu.Size = New System.Drawing.Size(150, 19)
        Me.cmbShubetu.TabIndex = 205
        Me.cmbShubetu.TabStop = False
        Me.cmbShubetu.TabStopSetting = False
        Me.cmbShubetu.TextValue = ""
        Me.cmbShubetu.Value1 = Nothing
        Me.cmbShubetu.Value2 = Nothing
        Me.cmbShubetu.Value3 = Nothing
        Me.cmbShubetu.ValueMember = Nothing
        Me.cmbShubetu.WidthDef = 150
        '
        'LMH050F
        '
        Me.ClientSize = New System.Drawing.Size(1018, 706)
        Me.FocusedControlName = "LMImTextBox"
        Me.Name = "LMH050F"
        Me.Text = ""
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprWarning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprWarning_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpWarning.ResumeLayout(False)
        Me.grpWarning.PerformLayout()
        Me.pnlDest.ResumeLayout(False)
        Me.pnlDest.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents sprWarning As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents LmTitleLabel21 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImTextBox1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrder_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblHikiate_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel60 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel67 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImNumber1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTARE_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel68 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel72 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel73 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtHasu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel74 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel78 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKosu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKonsu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImTextBox18 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmImTextBox19 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel80 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKanriNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel81 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsComment_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel89 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrisuTani1_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblStdIrimeTani_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtBuyerOrdNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel92 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblIrisuTani2_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numStdIrime_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtOrderNO_M_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numIrisu_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel94 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel95 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel96 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel97 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel98 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel99 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel100 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel101 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSumAnt_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numSumCnt_tabFreeM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents grpWarning As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtWarning As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel31 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKomokuNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel56 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtEdiKanriNoM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel11 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtEdiKanriNoL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel14 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrderNoL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel15 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKomokuVal As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel13 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrderNoM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel16 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtMastVal As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel17 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNrsBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbNrsWh As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents cmbShubetu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents sprWarning_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents pnlDest As Panel
    Friend WithEvents txtDestJisM As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestJisE As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestTelM As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestTelE As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestZipM As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestZipE As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestAdM As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestAdE As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel5 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel3 As Win.LMTitleLabel
    Friend WithEvents txtDestNmM As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestNmE As Win.InputMan.LMImTextBox
    Friend WithEvents txtDestWarning As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel12 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel10 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel6 As Win.LMTitleLabel
End Class
