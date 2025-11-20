<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LMI963F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL

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
        Dim EnhancedFocusIndicatorRenderer1 As FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer = New FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer()
        Dim EnhancedScrollBarRenderer1 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim EnhancedScrollBarRenderer2 As FarPoint.Win.Spread.EnhancedScrollBarRenderer = New FarPoint.Win.Spread.EnhancedScrollBarRenderer()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim TextCellType1 As FarPoint.Win.Spread.CellType.TextCellType = New FarPoint.Win.Spread.CellType.TextCellType()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.txtCmdGyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel102 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtLoadNumber = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtNonyuNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtOutkaNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtNonyuDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtOutkaDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.sprDetail_Sheet1 = New FarPoint.Win.Spread.SheetView()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtOutkaDate)
        Me.pnlViewAria.Controls.Add(Me.txtNonyuDate)
        Me.pnlViewAria.Controls.Add(Me.txtOutkaNm)
        Me.pnlViewAria.Controls.Add(Me.txtNonyuNm)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel5)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.txtCmdGyo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel102)
        Me.pnlViewAria.Controls.Add(Me.txtLoadNumber)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Size = New System.Drawing.Size(794, 488)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonEnabled = False
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F9ButtonEnabled = False
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(445, 1)
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(15, 13)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel2.TabIndex = 240
        Me.LmTitleLabel2.Text = "営業所"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "営業所"
        Me.LmTitleLabel2.WidthDef = 49
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(68, 10)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 597
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'txtCmdGyo
        '
        Me.txtCmdGyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCmdGyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCmdGyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmdGyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCmdGyo.CountWrappedLine = False
        Me.txtCmdGyo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCmdGyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCmdGyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCmdGyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCmdGyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCmdGyo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCmdGyo.HeightDef = 18
        Me.txtCmdGyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCmdGyo.HissuLabelVisible = False
        Me.txtCmdGyo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCmdGyo.IsByteCheck = 0
        Me.txtCmdGyo.IsCalendarCheck = False
        Me.txtCmdGyo.IsDakutenCheck = False
        Me.txtCmdGyo.IsEisuCheck = False
        Me.txtCmdGyo.IsForbiddenWordsCheck = False
        Me.txtCmdGyo.IsFullByteCheck = 0
        Me.txtCmdGyo.IsHankakuCheck = False
        Me.txtCmdGyo.IsHissuCheck = False
        Me.txtCmdGyo.IsKanaCheck = False
        Me.txtCmdGyo.IsMiddleSpace = False
        Me.txtCmdGyo.IsNumericCheck = False
        Me.txtCmdGyo.IsSujiCheck = False
        Me.txtCmdGyo.IsZenkakuCheck = False
        Me.txtCmdGyo.ItemName = ""
        Me.txtCmdGyo.LineSpace = 0
        Me.txtCmdGyo.Location = New System.Drawing.Point(110, 41)
        Me.txtCmdGyo.MaxLength = 0
        Me.txtCmdGyo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCmdGyo.MaxLineCount = 0
        Me.txtCmdGyo.Multiline = False
        Me.txtCmdGyo.Name = "txtCmdGyo"
        Me.txtCmdGyo.ReadOnly = True
        Me.txtCmdGyo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCmdGyo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCmdGyo.Size = New System.Drawing.Size(55, 18)
        Me.txtCmdGyo.TabIndex = 601
        Me.txtCmdGyo.TabStop = False
        Me.txtCmdGyo.TabStopSetting = False
        Me.txtCmdGyo.Tag = ""
        Me.txtCmdGyo.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtCmdGyo.UseSystemPasswordChar = False
        Me.txtCmdGyo.WidthDef = 55
        Me.txtCmdGyo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel102
        '
        Me.LmTitleLabel102.AutoSize = True
        Me.LmTitleLabel102.AutoSizeDef = True
        Me.LmTitleLabel102.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel102.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel102.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel102.EnableStatus = False
        Me.LmTitleLabel102.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel102.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel102.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel102.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel102.HeightDef = 13
        Me.LmTitleLabel102.Location = New System.Drawing.Point(27, 44)
        Me.LmTitleLabel102.Name = "LmTitleLabel102"
        Me.LmTitleLabel102.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel102.TabIndex = 600
        Me.LmTitleLabel102.Text = "商品行番号"
        Me.LmTitleLabel102.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel102.TextValue = "商品行番号"
        Me.LmTitleLabel102.WidthDef = 77
        '
        'txtLoadNumber
        '
        Me.txtLoadNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLoadNumber.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLoadNumber.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoadNumber.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLoadNumber.CountWrappedLine = False
        Me.txtLoadNumber.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLoadNumber.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLoadNumber.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLoadNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLoadNumber.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLoadNumber.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLoadNumber.HeightDef = 18
        Me.txtLoadNumber.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLoadNumber.HissuLabelVisible = False
        Me.txtLoadNumber.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtLoadNumber.IsByteCheck = 0
        Me.txtLoadNumber.IsCalendarCheck = False
        Me.txtLoadNumber.IsDakutenCheck = False
        Me.txtLoadNumber.IsEisuCheck = False
        Me.txtLoadNumber.IsForbiddenWordsCheck = False
        Me.txtLoadNumber.IsFullByteCheck = 0
        Me.txtLoadNumber.IsHankakuCheck = False
        Me.txtLoadNumber.IsHissuCheck = False
        Me.txtLoadNumber.IsKanaCheck = False
        Me.txtLoadNumber.IsMiddleSpace = False
        Me.txtLoadNumber.IsNumericCheck = False
        Me.txtLoadNumber.IsSujiCheck = False
        Me.txtLoadNumber.IsZenkakuCheck = False
        Me.txtLoadNumber.ItemName = ""
        Me.txtLoadNumber.LineSpace = 0
        Me.txtLoadNumber.Location = New System.Drawing.Point(110, 65)
        Me.txtLoadNumber.MaxLength = 0
        Me.txtLoadNumber.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLoadNumber.MaxLineCount = 0
        Me.txtLoadNumber.Multiline = False
        Me.txtLoadNumber.Name = "txtLoadNumber"
        Me.txtLoadNumber.ReadOnly = True
        Me.txtLoadNumber.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLoadNumber.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLoadNumber.Size = New System.Drawing.Size(125, 18)
        Me.txtLoadNumber.TabIndex = 599
        Me.txtLoadNumber.TabStop = False
        Me.txtLoadNumber.TabStopSetting = False
        Me.txtLoadNumber.Tag = ""
        Me.txtLoadNumber.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtLoadNumber.UseSystemPasswordChar = False
        Me.txtLoadNumber.WidthDef = 125
        Me.txtLoadNumber.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel6.Location = New System.Drawing.Point(20, 68)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(84, 13)
        Me.LmTitleLabel6.TabIndex = 598
        Me.LmTitleLabel6.Text = "Load Number"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "Load Number"
        Me.LmTitleLabel6.WidthDef = 84
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(495, 68)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 603
        Me.LmTitleLabel1.Text = "納入先"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "納入先"
        Me.LmTitleLabel1.WidthDef = 49
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(495, 44)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 604
        Me.LmTitleLabel3.Text = "出荷元"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "出荷元"
        Me.LmTitleLabel3.WidthDef = 49
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSize = True
        Me.LmTitleLabel4.AutoSizeDef = True
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 13
        Me.LmTitleLabel4.Location = New System.Drawing.Point(343, 68)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel4.TabIndex = 605
        Me.LmTitleLabel4.Text = "納入日"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "納入日"
        Me.LmTitleLabel4.WidthDef = 49
        '
        'LmTitleLabel5
        '
        Me.LmTitleLabel5.AutoSize = True
        Me.LmTitleLabel5.AutoSizeDef = True
        Me.LmTitleLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel5.EnableStatus = False
        Me.LmTitleLabel5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.HeightDef = 13
        Me.LmTitleLabel5.Location = New System.Drawing.Point(343, 44)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel5.TabIndex = 606
        Me.LmTitleLabel5.Text = "出荷日"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "出荷日"
        Me.LmTitleLabel5.WidthDef = 49
        '
        'txtNonyuNm
        '
        Me.txtNonyuNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNonyuNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNonyuNm.CountWrappedLine = False
        Me.txtNonyuNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNonyuNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNonyuNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNonyuNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNonyuNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNonyuNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNonyuNm.HeightDef = 18
        Me.txtNonyuNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuNm.HissuLabelVisible = False
        Me.txtNonyuNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtNonyuNm.IsByteCheck = 0
        Me.txtNonyuNm.IsCalendarCheck = False
        Me.txtNonyuNm.IsDakutenCheck = False
        Me.txtNonyuNm.IsEisuCheck = False
        Me.txtNonyuNm.IsForbiddenWordsCheck = False
        Me.txtNonyuNm.IsFullByteCheck = 0
        Me.txtNonyuNm.IsHankakuCheck = False
        Me.txtNonyuNm.IsHissuCheck = False
        Me.txtNonyuNm.IsKanaCheck = False
        Me.txtNonyuNm.IsMiddleSpace = False
        Me.txtNonyuNm.IsNumericCheck = False
        Me.txtNonyuNm.IsSujiCheck = False
        Me.txtNonyuNm.IsZenkakuCheck = False
        Me.txtNonyuNm.ItemName = ""
        Me.txtNonyuNm.LineSpace = 0
        Me.txtNonyuNm.Location = New System.Drawing.Point(548, 65)
        Me.txtNonyuNm.MaxLength = 0
        Me.txtNonyuNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNonyuNm.MaxLineCount = 0
        Me.txtNonyuNm.Multiline = False
        Me.txtNonyuNm.Name = "txtNonyuNm"
        Me.txtNonyuNm.ReadOnly = True
        Me.txtNonyuNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNonyuNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNonyuNm.Size = New System.Drawing.Size(217, 18)
        Me.txtNonyuNm.TabIndex = 607
        Me.txtNonyuNm.TabStop = False
        Me.txtNonyuNm.TabStopSetting = False
        Me.txtNonyuNm.Tag = ""
        Me.txtNonyuNm.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtNonyuNm.UseSystemPasswordChar = False
        Me.txtNonyuNm.WidthDef = 217
        Me.txtNonyuNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtOutkaNm
        '
        Me.txtOutkaNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaNm.CountWrappedLine = False
        Me.txtOutkaNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaNm.HeightDef = 18
        Me.txtOutkaNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNm.HissuLabelVisible = False
        Me.txtOutkaNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOutkaNm.IsByteCheck = 0
        Me.txtOutkaNm.IsCalendarCheck = False
        Me.txtOutkaNm.IsDakutenCheck = False
        Me.txtOutkaNm.IsEisuCheck = False
        Me.txtOutkaNm.IsForbiddenWordsCheck = False
        Me.txtOutkaNm.IsFullByteCheck = 0
        Me.txtOutkaNm.IsHankakuCheck = False
        Me.txtOutkaNm.IsHissuCheck = False
        Me.txtOutkaNm.IsKanaCheck = False
        Me.txtOutkaNm.IsMiddleSpace = False
        Me.txtOutkaNm.IsNumericCheck = False
        Me.txtOutkaNm.IsSujiCheck = False
        Me.txtOutkaNm.IsZenkakuCheck = False
        Me.txtOutkaNm.ItemName = ""
        Me.txtOutkaNm.LineSpace = 0
        Me.txtOutkaNm.Location = New System.Drawing.Point(548, 41)
        Me.txtOutkaNm.MaxLength = 0
        Me.txtOutkaNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaNm.MaxLineCount = 0
        Me.txtOutkaNm.Multiline = False
        Me.txtOutkaNm.Name = "txtOutkaNm"
        Me.txtOutkaNm.ReadOnly = True
        Me.txtOutkaNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaNm.Size = New System.Drawing.Size(217, 18)
        Me.txtOutkaNm.TabIndex = 608
        Me.txtOutkaNm.TabStop = False
        Me.txtOutkaNm.TabStopSetting = False
        Me.txtOutkaNm.Tag = ""
        Me.txtOutkaNm.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtOutkaNm.UseSystemPasswordChar = False
        Me.txtOutkaNm.WidthDef = 217
        Me.txtOutkaNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtNonyuDate
        '
        Me.txtNonyuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNonyuDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNonyuDate.CountWrappedLine = False
        Me.txtNonyuDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNonyuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNonyuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNonyuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNonyuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNonyuDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNonyuDate.HeightDef = 18
        Me.txtNonyuDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNonyuDate.HissuLabelVisible = False
        Me.txtNonyuDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtNonyuDate.IsByteCheck = 0
        Me.txtNonyuDate.IsCalendarCheck = False
        Me.txtNonyuDate.IsDakutenCheck = False
        Me.txtNonyuDate.IsEisuCheck = False
        Me.txtNonyuDate.IsForbiddenWordsCheck = False
        Me.txtNonyuDate.IsFullByteCheck = 0
        Me.txtNonyuDate.IsHankakuCheck = False
        Me.txtNonyuDate.IsHissuCheck = False
        Me.txtNonyuDate.IsKanaCheck = False
        Me.txtNonyuDate.IsMiddleSpace = False
        Me.txtNonyuDate.IsNumericCheck = False
        Me.txtNonyuDate.IsSujiCheck = False
        Me.txtNonyuDate.IsZenkakuCheck = False
        Me.txtNonyuDate.ItemName = ""
        Me.txtNonyuDate.LineSpace = 0
        Me.txtNonyuDate.Location = New System.Drawing.Point(393, 65)
        Me.txtNonyuDate.MaxLength = 0
        Me.txtNonyuDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNonyuDate.MaxLineCount = 0
        Me.txtNonyuDate.Multiline = False
        Me.txtNonyuDate.Name = "txtNonyuDate"
        Me.txtNonyuDate.ReadOnly = True
        Me.txtNonyuDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNonyuDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNonyuDate.Size = New System.Drawing.Size(96, 18)
        Me.txtNonyuDate.TabIndex = 609
        Me.txtNonyuDate.TabStop = False
        Me.txtNonyuDate.TabStopSetting = False
        Me.txtNonyuDate.Tag = ""
        Me.txtNonyuDate.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtNonyuDate.UseSystemPasswordChar = False
        Me.txtNonyuDate.WidthDef = 96
        Me.txtNonyuDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtOutkaDate
        '
        Me.txtOutkaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaDate.CountWrappedLine = False
        Me.txtOutkaDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaDate.HeightDef = 18
        Me.txtOutkaDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaDate.HissuLabelVisible = False
        Me.txtOutkaDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOutkaDate.IsByteCheck = 0
        Me.txtOutkaDate.IsCalendarCheck = False
        Me.txtOutkaDate.IsDakutenCheck = False
        Me.txtOutkaDate.IsEisuCheck = False
        Me.txtOutkaDate.IsForbiddenWordsCheck = False
        Me.txtOutkaDate.IsFullByteCheck = 0
        Me.txtOutkaDate.IsHankakuCheck = False
        Me.txtOutkaDate.IsHissuCheck = False
        Me.txtOutkaDate.IsKanaCheck = False
        Me.txtOutkaDate.IsMiddleSpace = False
        Me.txtOutkaDate.IsNumericCheck = False
        Me.txtOutkaDate.IsSujiCheck = False
        Me.txtOutkaDate.IsZenkakuCheck = False
        Me.txtOutkaDate.ItemName = ""
        Me.txtOutkaDate.LineSpace = 0
        Me.txtOutkaDate.Location = New System.Drawing.Point(393, 41)
        Me.txtOutkaDate.MaxLength = 0
        Me.txtOutkaDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaDate.MaxLineCount = 0
        Me.txtOutkaDate.Multiline = False
        Me.txtOutkaDate.Name = "txtOutkaDate"
        Me.txtOutkaDate.ReadOnly = True
        Me.txtOutkaDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaDate.Size = New System.Drawing.Size(96, 18)
        Me.txtOutkaDate.TabIndex = 610
        Me.txtOutkaDate.TabStop = False
        Me.txtOutkaDate.TabStopSetting = False
        Me.txtOutkaDate.Tag = ""
        Me.txtOutkaDate.TextValue = "X---10---XX---10---XX---10---X"
        Me.txtOutkaDate.UseSystemPasswordChar = False
        Me.txtOutkaDate.WidthDef = 96
        Me.txtOutkaDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.sprDetail.FocusRenderer = EnhancedFocusIndicatorRenderer1
        Me.sprDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail.HeightDef = 383
        Me.sprDetail.HorizontalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.HorizontalScrollBar.Name = ""
        Me.sprDetail.HorizontalScrollBar.Renderer = EnhancedScrollBarRenderer1
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(12, 93)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprDetail_Sheet1})
        Me.sprDetail.Size = New System.Drawing.Size(770, 383)
        Me.sprDetail.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 4
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton")
        Me.sprDetail.VerticalScrollBar.Name = ""
        Me.sprDetail.VerticalScrollBar.Renderer = EnhancedScrollBarRenderer2
        Me.sprDetail.WidthDef = 770

        '
        'LMI963F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI963F"
        Me.Text = "【LMI963】 荷主マスタ照会"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents txtCmdGyo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel102 As Win.LMTitleLabel
    Friend WithEvents txtLoadNumber As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel6 As Win.LMTitleLabel
    Friend WithEvents txtOutkaDate As Win.InputMan.LMImTextBox
    Friend WithEvents txtNonyuDate As Win.InputMan.LMImTextBox
    Friend WithEvents txtOutkaNm As Win.InputMan.LMImTextBox
    Friend WithEvents txtNonyuNm As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel5 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel3 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Win.LMTitleLabel
    Friend WithEvents sprDetail As Win.Spread.LMSpread
    Friend WithEvents sprDetail_Sheet1 As FarPoint.Win.Spread.SheetView
End Class
