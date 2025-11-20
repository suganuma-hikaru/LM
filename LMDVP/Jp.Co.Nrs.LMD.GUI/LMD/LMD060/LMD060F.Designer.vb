<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMD060F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMD060F))
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.sprCreate = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblTitleSimebi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbSimebi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.grpSelect = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTantouNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleTantou = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtTantouCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.grpCreDelCheck = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleZaiRirekiCreate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdZaiRirekiDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprCreate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSelect.SuspendLayout()
        Me.grpCreDelCheck.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpCreDelCheck)
        Me.pnlViewAria.Controls.Add(Me.sprCreate)
        Me.pnlViewAria.Controls.Add(Me.grpSelect)
        '
        'lblTitleEigyo
        '
        Me.lblTitleEigyo.AutoSizeDef = False
        Me.lblTitleEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo.EnableStatus = False
        Me.lblTitleEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.HeightDef = 18
        Me.lblTitleEigyo.Location = New System.Drawing.Point(31, 14)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleEigyo.TabIndex = 1
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.lblCustNm.Location = New System.Drawing.Point(172, 35)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(608, 18)
        Me.lblCustNm.TabIndex = 7
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 608
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdM.CountWrappedLine = False
        Me.txtCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdM.HeightDef = 18
        Me.txtCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.HissuLabelVisible = False
        Me.txtCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdM.IsByteCheck = 2
        Me.txtCustCdM.IsCalendarCheck = False
        Me.txtCustCdM.IsDakutenCheck = False
        Me.txtCustCdM.IsEisuCheck = False
        Me.txtCustCdM.IsForbiddenWordsCheck = False
        Me.txtCustCdM.IsFullByteCheck = 0
        Me.txtCustCdM.IsHankakuCheck = False
        Me.txtCustCdM.IsHissuCheck = False
        Me.txtCustCdM.IsKanaCheck = False
        Me.txtCustCdM.IsMiddleSpace = False
        Me.txtCustCdM.IsNumericCheck = False
        Me.txtCustCdM.IsSujiCheck = False
        Me.txtCustCdM.IsZenkakuCheck = False
        Me.txtCustCdM.ItemName = ""
        Me.txtCustCdM.LineSpace = 0
        Me.txtCustCdM.Location = New System.Drawing.Point(143, 35)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdM.TabIndex = 18
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 45
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCustCd
        '
        Me.lblTitleCustCd.AutoSizeDef = False
        Me.lblTitleCustCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCustCd.EnableStatus = False
        Me.lblTitleCustCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.HeightDef = 18
        Me.lblTitleCustCd.Location = New System.Drawing.Point(45, 34)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 18)
        Me.lblTitleCustCd.TabIndex = 5
        Me.lblTitleCustCd.Text = "荷主"
        Me.lblTitleCustCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustCd.TextValue = "荷主"
        Me.lblTitleCustCd.WidthDef = 35
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdL.CountWrappedLine = False
        Me.txtCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdL.HeightDef = 18
        Me.txtCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.HissuLabelVisible = False
        Me.txtCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdL.IsByteCheck = 5
        Me.txtCustCdL.IsCalendarCheck = False
        Me.txtCustCdL.IsDakutenCheck = False
        Me.txtCustCdL.IsEisuCheck = False
        Me.txtCustCdL.IsForbiddenWordsCheck = False
        Me.txtCustCdL.IsFullByteCheck = 0
        Me.txtCustCdL.IsHankakuCheck = False
        Me.txtCustCdL.IsHissuCheck = False
        Me.txtCustCdL.IsKanaCheck = False
        Me.txtCustCdL.IsMiddleSpace = False
        Me.txtCustCdL.IsNumericCheck = False
        Me.txtCustCdL.IsSujiCheck = False
        Me.txtCustCdL.IsZenkakuCheck = False
        Me.txtCustCdL.ItemName = ""
        Me.txtCustCdL.LineSpace = 0
        Me.txtCustCdL.Location = New System.Drawing.Point(91, 35)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(82, 18)
        Me.txtCustCdL.TabIndex = 6
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 82
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'sprCreate
        '
        Me.sprCreate.AccessibleDescription = ""
        Me.sprCreate.AllowUserZoom = False
        Me.sprCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprCreate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprCreate.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprCreate.CellClickEventArgs = Nothing
        Me.sprCreate.CheckToCheckBox = True
        Me.sprCreate.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprCreate.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprCreate.EditModeReplace = True
        Me.sprCreate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprCreate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprCreate.ForeColorDef = System.Drawing.Color.Empty
        Me.sprCreate.HeightDef = 763
        Me.sprCreate.KeyboardCheckBoxOn = False
        Me.sprCreate.Location = New System.Drawing.Point(12, 100)
        Me.sprCreate.Name = "sprCreate"
        Me.sprCreate.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprCreate.SetViewportTopRow(0, 0, 1)
        Me.sprCreate.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        'RowCount = 1
        Me.sprCreate.SetViewportTopRow(0, 0, 1)
        Me.sprCreate.SetActiveViewport(0, -1, 0)
        Me.sprCreate.Size = New System.Drawing.Size(1248, 763)
        Me.sprCreate.SortColumn = True
        Me.sprCreate.SpanColumnLock = True
        Me.sprCreate.SpreadDoubleClicked = False
        Me.sprCreate.TabIndex = 15
        Me.sprCreate.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Bottom
        Me.sprCreate.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprCreate.TextValue = Nothing
        Me.sprCreate.WidthDef = 1248
        '
        'lblTitleSimebi
        '
        Me.lblTitleSimebi.AutoSizeDef = False
        Me.lblTitleSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSimebi.EnableStatus = False
        Me.lblTitleSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.HeightDef = 18
        Me.lblTitleSimebi.Location = New System.Drawing.Point(6, 55)
        Me.lblTitleSimebi.Name = "lblTitleSimebi"
        Me.lblTitleSimebi.Size = New System.Drawing.Size(74, 18)
        Me.lblTitleSimebi.TabIndex = 21
        Me.lblTitleSimebi.Text = "締日区分"
        Me.lblTitleSimebi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSimebi.TextValue = "締日区分"
        Me.lblTitleSimebi.WidthDef = 74
        '
        'cmbSimebi
        '
        Me.cmbSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSimebi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSimebi.DataCode = "S008"
        Me.cmbSimebi.DataSource = Nothing
        Me.cmbSimebi.DisplayMember = Nothing
        Me.cmbSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.HeightDef = 18
        Me.cmbSimebi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSimebi.HissuLabelVisible = False
        Me.cmbSimebi.InsertWildCard = True
        Me.cmbSimebi.IsForbiddenWordsCheck = False
        Me.cmbSimebi.IsHissuCheck = False
        Me.cmbSimebi.ItemName = ""
        Me.cmbSimebi.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSimebi.Location = New System.Drawing.Point(91, 56)
        Me.cmbSimebi.Name = "cmbSimebi"
        Me.cmbSimebi.ReadOnly = False
        Me.cmbSimebi.SelectedIndex = -1
        Me.cmbSimebi.SelectedItem = Nothing
        Me.cmbSimebi.SelectedText = ""
        Me.cmbSimebi.SelectedValue = ""
        Me.cmbSimebi.Size = New System.Drawing.Size(127, 18)
        Me.cmbSimebi.TabIndex = 27
        Me.cmbSimebi.TabStopSetting = True
        Me.cmbSimebi.TextValue = ""
        Me.cmbSimebi.Value1 = ""
        Me.cmbSimebi.Value2 = Nothing
        Me.cmbSimebi.Value3 = Nothing
        Me.cmbSimebi.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSimebi.ValueMember = Nothing
        Me.cmbSimebi.WidthDef = 127
        '
        'grpSelect
        '
        Me.grpSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSelect.Controls.Add(Me.cmbEigyo)
        Me.grpSelect.Controls.Add(Me.lblTantouNM)
        Me.grpSelect.Controls.Add(Me.lblCustNm)
        Me.grpSelect.Controls.Add(Me.txtCustCdM)
        Me.grpSelect.Controls.Add(Me.lblTitleEigyo)
        Me.grpSelect.Controls.Add(Me.txtCustCdL)
        Me.grpSelect.Controls.Add(Me.lblTitleCustCd)
        Me.grpSelect.Controls.Add(Me.lblTitleSimebi)
        Me.grpSelect.Controls.Add(Me.cmbSimebi)
        Me.grpSelect.Controls.Add(Me.lblTitleTantou)
        Me.grpSelect.Controls.Add(Me.txtTantouCd)
        Me.grpSelect.EnableStatus = False
        Me.grpSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.HeightDef = 81
        Me.grpSelect.Location = New System.Drawing.Point(12, 13)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(792, 81)
        Me.grpSelect.TabIndex = 33
        Me.grpSelect.TabStop = False
        Me.grpSelect.Text = "検索条件"
        Me.grpSelect.TextValue = "検索条件"
        Me.grpSelect.WidthDef = 792
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbEigyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbEigyo.DataSource = Nothing
        Me.cmbEigyo.DisplayMember = Nothing
        Me.cmbEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbEigyo.HeightDef = 18
        Me.cmbEigyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbEigyo.HissuLabelVisible = False
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = False
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(91, 14)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 41
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'lblTantouNM
        '
        Me.lblTantouNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantouNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantouNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTantouNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTantouNM.CountWrappedLine = False
        Me.lblTantouNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTantouNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantouNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantouNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTantouNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTantouNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTantouNM.HeightDef = 18
        Me.lblTantouNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantouNM.HissuLabelVisible = False
        Me.lblTantouNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTantouNM.IsByteCheck = 0
        Me.lblTantouNM.IsCalendarCheck = False
        Me.lblTantouNM.IsDakutenCheck = False
        Me.lblTantouNM.IsEisuCheck = False
        Me.lblTantouNM.IsForbiddenWordsCheck = False
        Me.lblTantouNM.IsFullByteCheck = 0
        Me.lblTantouNM.IsHankakuCheck = False
        Me.lblTantouNM.IsHissuCheck = False
        Me.lblTantouNM.IsKanaCheck = False
        Me.lblTantouNM.IsMiddleSpace = False
        Me.lblTantouNM.IsNumericCheck = False
        Me.lblTantouNM.IsSujiCheck = False
        Me.lblTantouNM.IsZenkakuCheck = False
        Me.lblTantouNM.ItemName = ""
        Me.lblTantouNM.LineSpace = 0
        Me.lblTantouNM.Location = New System.Drawing.Point(512, 14)
        Me.lblTantouNM.MaxLength = 0
        Me.lblTantouNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTantouNM.MaxLineCount = 0
        Me.lblTantouNM.Multiline = False
        Me.lblTantouNM.Name = "lblTantouNM"
        Me.lblTantouNM.ReadOnly = True
        Me.lblTantouNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTantouNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTantouNM.Size = New System.Drawing.Size(268, 18)
        Me.lblTantouNM.TabIndex = 40
        Me.lblTantouNM.TabStop = False
        Me.lblTantouNM.TabStopSetting = False
        Me.lblTantouNM.TextValue = ""
        Me.lblTantouNM.UseSystemPasswordChar = False
        Me.lblTantouNM.WidthDef = 268
        Me.lblTantouNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTantou
        '
        Me.lblTitleTantou.AutoSizeDef = False
        Me.lblTitleTantou.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTantou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTantou.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTantou.EnableStatus = False
        Me.lblTitleTantou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTantou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTantou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTantou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTantou.HeightDef = 18
        Me.lblTitleTantou.Location = New System.Drawing.Point(393, 14)
        Me.lblTitleTantou.Name = "lblTitleTantou"
        Me.lblTitleTantou.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleTantou.TabIndex = 38
        Me.lblTitleTantou.Text = "担当者"
        Me.lblTitleTantou.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTantou.TextValue = "担当者"
        Me.lblTitleTantou.WidthDef = 49
        '
        'txtTantouCd
        '
        Me.txtTantouCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTantouCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTantouCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTantouCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTantouCd.CountWrappedLine = False
        Me.txtTantouCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTantouCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantouCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantouCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTantouCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTantouCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTantouCd.HeightDef = 18
        Me.txtTantouCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTantouCd.HissuLabelVisible = False
        Me.txtTantouCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtTantouCd.IsByteCheck = 5
        Me.txtTantouCd.IsCalendarCheck = False
        Me.txtTantouCd.IsDakutenCheck = False
        Me.txtTantouCd.IsEisuCheck = False
        Me.txtTantouCd.IsForbiddenWordsCheck = False
        Me.txtTantouCd.IsFullByteCheck = 0
        Me.txtTantouCd.IsHankakuCheck = False
        Me.txtTantouCd.IsHissuCheck = False
        Me.txtTantouCd.IsKanaCheck = False
        Me.txtTantouCd.IsMiddleSpace = False
        Me.txtTantouCd.IsNumericCheck = False
        Me.txtTantouCd.IsSujiCheck = False
        Me.txtTantouCd.IsZenkakuCheck = False
        Me.txtTantouCd.ItemName = ""
        Me.txtTantouCd.LineSpace = 0
        Me.txtTantouCd.Location = New System.Drawing.Point(446, 14)
        Me.txtTantouCd.MaxLength = 5
        Me.txtTantouCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTantouCd.MaxLineCount = 0
        Me.txtTantouCd.Multiline = False
        Me.txtTantouCd.Name = "txtTantouCd"
        Me.txtTantouCd.ReadOnly = False
        Me.txtTantouCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTantouCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTantouCd.Size = New System.Drawing.Size(82, 18)
        Me.txtTantouCd.TabIndex = 39
        Me.txtTantouCd.TabStopSetting = True
        Me.txtTantouCd.TextValue = ""
        Me.txtTantouCd.UseSystemPasswordChar = False
        Me.txtTantouCd.WidthDef = 82
        Me.txtTantouCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpCreDelCheck
        '
        Me.grpCreDelCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpCreDelCheck.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpCreDelCheck.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpCreDelCheck.Controls.Add(Me.lblTitleZaiRirekiCreate)
        Me.grpCreDelCheck.Controls.Add(Me.imdZaiRirekiDate)
        Me.grpCreDelCheck.EnableStatus = False
        Me.grpCreDelCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpCreDelCheck.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpCreDelCheck.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpCreDelCheck.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpCreDelCheck.HeightDef = 81
        Me.grpCreDelCheck.Location = New System.Drawing.Point(810, 13)
        Me.grpCreDelCheck.Name = "grpCreDelCheck"
        Me.grpCreDelCheck.Size = New System.Drawing.Size(299, 81)
        Me.grpCreDelCheck.TabIndex = 34
        Me.grpCreDelCheck.TabStop = False
        Me.grpCreDelCheck.Text = "作成処理"
        Me.grpCreDelCheck.TextValue = "作成処理"
        Me.grpCreDelCheck.WidthDef = 299
        '
        'lblTitleZaiRirekiCreate
        '
        Me.lblTitleZaiRirekiCreate.AutoSize = True
        Me.lblTitleZaiRirekiCreate.AutoSizeDef = True
        Me.lblTitleZaiRirekiCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZaiRirekiCreate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZaiRirekiCreate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZaiRirekiCreate.EnableStatus = False
        Me.lblTitleZaiRirekiCreate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZaiRirekiCreate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZaiRirekiCreate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZaiRirekiCreate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZaiRirekiCreate.HeightDef = 13
        Me.lblTitleZaiRirekiCreate.Location = New System.Drawing.Point(15, 16)
        Me.lblTitleZaiRirekiCreate.Name = "lblTitleZaiRirekiCreate"
        Me.lblTitleZaiRirekiCreate.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleZaiRirekiCreate.TabIndex = 41
        Me.lblTitleZaiRirekiCreate.Text = "在庫履歴作成日"
        Me.lblTitleZaiRirekiCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZaiRirekiCreate.TextValue = "在庫履歴作成日"
        Me.lblTitleZaiRirekiCreate.WidthDef = 105
        '
        'imdZaiRirekiDate
        '
        Me.imdZaiRirekiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdZaiRirekiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdZaiRirekiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdZaiRirekiDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdZaiRirekiDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdZaiRirekiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdZaiRirekiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdZaiRirekiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdZaiRirekiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdZaiRirekiDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdZaiRirekiDate.HeightDef = 18
        Me.imdZaiRirekiDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdZaiRirekiDate.HissuLabelVisible = True
        Me.imdZaiRirekiDate.Holiday = True
        Me.imdZaiRirekiDate.IsAfterDateCheck = False
        Me.imdZaiRirekiDate.IsBeforeDateCheck = False
        Me.imdZaiRirekiDate.IsHissuCheck = True
        Me.imdZaiRirekiDate.IsMinDateCheck = "1900/01/01"
        Me.imdZaiRirekiDate.ItemName = ""
        Me.imdZaiRirekiDate.Location = New System.Drawing.Point(126, 14)
        Me.imdZaiRirekiDate.Name = "imdZaiRirekiDate"
        Me.imdZaiRirekiDate.Number = CType(0, Long)
        Me.imdZaiRirekiDate.ReadOnly = False
        Me.imdZaiRirekiDate.Size = New System.Drawing.Size(115, 18)
        Me.imdZaiRirekiDate.TabIndex = 42
        Me.imdZaiRirekiDate.TabStopSetting = True
        Me.imdZaiRirekiDate.TextValue = ""
        Me.imdZaiRirekiDate.Value = New Date(CType(0, Long))
        Me.imdZaiRirekiDate.WidthDef = 115
        '
        'LMD060F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMD060F"
        Me.RightToLeftLayout = True
        Me.Text = "【LMD060】 月末在庫履歴作成"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprCreate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSelect.ResumeLayout(False)
        Me.grpCreDelCheck.ResumeLayout(False)
        Me.grpCreDelCheck.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprCreate As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbSimebi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSimebi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSelect As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents grpCreDelCheck As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleTantou As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdZaiRirekiDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleZaiRirekiCreate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTantouNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTantouCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr

End Class
