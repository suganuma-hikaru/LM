<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMZ300F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopM

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMZ300F))
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblUnsocoBrNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUnsocoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUnsoco = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtUnsocoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtUnsocoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.lblUnsocoBrNm)
        Me.pnlViewAria.Controls.Add(Me.lblUnsocoNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUnsoco)
        Me.pnlViewAria.Controls.Add(Me.txtUnsocoBrCd)
        Me.pnlViewAria.Controls.Add(Me.txtUnsocoCd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(258, 1)
        '
        'sprDetail
        '
        Me.sprDetail.AccessibleDescription = ""
        Me.sprDetail.AllowUserZoom = False
        Me.sprDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetail.CellClickEventArgs = Nothing
        Me.sprDetail.CheckToCheckBox = True
        Me.sprDetail.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetail.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.EditModeReplace = True
        Me.sprDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail.HeightDef = 255
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(13, 77)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        'RowCount = 1
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        Me.sprDetail.Size = New System.Drawing.Size(570, 255)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 5
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 570
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(66, 11)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(309, 18)
        Me.cmbNrsBrCd.TabIndex = 605
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 309
        '
        'lblUnsocoBrNm
        '
        Me.lblUnsocoBrNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoBrNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoBrNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsocoBrNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsocoBrNm.CountWrappedLine = False
        Me.lblUnsocoBrNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsocoBrNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoBrNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoBrNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoBrNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoBrNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsocoBrNm.HeightDef = 18
        Me.lblUnsocoBrNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoBrNm.HissuLabelVisible = False
        Me.lblUnsocoBrNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsocoBrNm.IsByteCheck = 0
        Me.lblUnsocoBrNm.IsCalendarCheck = False
        Me.lblUnsocoBrNm.IsDakutenCheck = False
        Me.lblUnsocoBrNm.IsEisuCheck = False
        Me.lblUnsocoBrNm.IsForbiddenWordsCheck = False
        Me.lblUnsocoBrNm.IsFullByteCheck = 0
        Me.lblUnsocoBrNm.IsHankakuCheck = False
        Me.lblUnsocoBrNm.IsHissuCheck = False
        Me.lblUnsocoBrNm.IsKanaCheck = False
        Me.lblUnsocoBrNm.IsMiddleSpace = False
        Me.lblUnsocoBrNm.IsNumericCheck = False
        Me.lblUnsocoBrNm.IsSujiCheck = False
        Me.lblUnsocoBrNm.IsZenkakuCheck = False
        Me.lblUnsocoBrNm.ItemName = ""
        Me.lblUnsocoBrNm.LineSpace = 0
        Me.lblUnsocoBrNm.Location = New System.Drawing.Point(147, 53)
        Me.lblUnsocoBrNm.MaxLength = 0
        Me.lblUnsocoBrNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsocoBrNm.MaxLineCount = 0
        Me.lblUnsocoBrNm.Multiline = False
        Me.lblUnsocoBrNm.Name = "lblUnsocoBrNm"
        Me.lblUnsocoBrNm.ReadOnly = True
        Me.lblUnsocoBrNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsocoBrNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsocoBrNm.Size = New System.Drawing.Size(435, 18)
        Me.lblUnsocoBrNm.TabIndex = 604
        Me.lblUnsocoBrNm.TabStop = False
        Me.lblUnsocoBrNm.TabStopSetting = False
        Me.lblUnsocoBrNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblUnsocoBrNm.UseSystemPasswordChar = False
        Me.lblUnsocoBrNm.WidthDef = 435
        Me.lblUnsocoBrNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnsocoNm
        '
        Me.lblUnsocoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsocoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsocoNm.CountWrappedLine = False
        Me.lblUnsocoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsocoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsocoNm.HeightDef = 18
        Me.lblUnsocoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.HissuLabelVisible = False
        Me.lblUnsocoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsocoNm.IsByteCheck = 0
        Me.lblUnsocoNm.IsCalendarCheck = False
        Me.lblUnsocoNm.IsDakutenCheck = False
        Me.lblUnsocoNm.IsEisuCheck = False
        Me.lblUnsocoNm.IsForbiddenWordsCheck = False
        Me.lblUnsocoNm.IsFullByteCheck = 0
        Me.lblUnsocoNm.IsHankakuCheck = False
        Me.lblUnsocoNm.IsHissuCheck = False
        Me.lblUnsocoNm.IsKanaCheck = False
        Me.lblUnsocoNm.IsMiddleSpace = False
        Me.lblUnsocoNm.IsNumericCheck = False
        Me.lblUnsocoNm.IsSujiCheck = False
        Me.lblUnsocoNm.IsZenkakuCheck = False
        Me.lblUnsocoNm.ItemName = ""
        Me.lblUnsocoNm.LineSpace = 0
        Me.lblUnsocoNm.Location = New System.Drawing.Point(147, 32)
        Me.lblUnsocoNm.MaxLength = 0
        Me.lblUnsocoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsocoNm.MaxLineCount = 0
        Me.lblUnsocoNm.Multiline = False
        Me.lblUnsocoNm.Name = "lblUnsocoNm"
        Me.lblUnsocoNm.ReadOnly = True
        Me.lblUnsocoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsocoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsocoNm.Size = New System.Drawing.Size(435, 18)
        Me.lblUnsocoNm.TabIndex = 603
        Me.lblUnsocoNm.TabStop = False
        Me.lblUnsocoNm.TabStopSetting = False
        Me.lblUnsocoNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblUnsocoNm.UseSystemPasswordChar = False
        Me.lblUnsocoNm.WidthDef = 435
        Me.lblUnsocoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoco
        '
        Me.lblTitleUnsoco.AutoSize = True
        Me.lblTitleUnsoco.AutoSizeDef = True
        Me.lblTitleUnsoco.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoco.EnableStatus = False
        Me.lblTitleUnsoco.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.HeightDef = 13
        Me.lblTitleUnsoco.Location = New System.Drawing.Point(3, 37)
        Me.lblTitleUnsoco.Name = "lblTitleUnsoco"
        Me.lblTitleUnsoco.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnsoco.TabIndex = 602
        Me.lblTitleUnsoco.Text = "運送会社"
        Me.lblTitleUnsoco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoco.TextValue = "運送会社"
        Me.lblTitleUnsoco.WidthDef = 63
        '
        'txtUnsocoBrCd
        '
        Me.txtUnsocoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoBrCd.CountWrappedLine = False
        Me.txtUnsocoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoBrCd.HeightDef = 18
        Me.txtUnsocoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCd.HissuLabelVisible = False
        Me.txtUnsocoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoBrCd.IsByteCheck = 3
        Me.txtUnsocoBrCd.IsCalendarCheck = False
        Me.txtUnsocoBrCd.IsDakutenCheck = False
        Me.txtUnsocoBrCd.IsEisuCheck = False
        Me.txtUnsocoBrCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoBrCd.IsFullByteCheck = 0
        Me.txtUnsocoBrCd.IsHankakuCheck = False
        Me.txtUnsocoBrCd.IsHissuCheck = False
        Me.txtUnsocoBrCd.IsKanaCheck = False
        Me.txtUnsocoBrCd.IsMiddleSpace = False
        Me.txtUnsocoBrCd.IsNumericCheck = False
        Me.txtUnsocoBrCd.IsSujiCheck = False
        Me.txtUnsocoBrCd.IsZenkakuCheck = False
        Me.txtUnsocoBrCd.ItemName = ""
        Me.txtUnsocoBrCd.LineSpace = 0
        Me.txtUnsocoBrCd.Location = New System.Drawing.Point(111, 53)
        Me.txtUnsocoBrCd.MaxLength = 3
        Me.txtUnsocoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoBrCd.MaxLineCount = 0
        Me.txtUnsocoBrCd.Multiline = False
        Me.txtUnsocoBrCd.Name = "txtUnsocoBrCd"
        Me.txtUnsocoBrCd.ReadOnly = False
        Me.txtUnsocoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoBrCd.Size = New System.Drawing.Size(52, 18)
        Me.txtUnsocoBrCd.TabIndex = 601
        Me.txtUnsocoBrCd.TabStopSetting = True
        Me.txtUnsocoBrCd.TextValue = "123"
        Me.txtUnsocoBrCd.UseSystemPasswordChar = False
        Me.txtUnsocoBrCd.WidthDef = 52
        Me.txtUnsocoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoCd
        '
        Me.txtUnsocoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoCd.CountWrappedLine = False
        Me.txtUnsocoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoCd.HeightDef = 18
        Me.txtUnsocoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCd.HissuLabelVisible = False
        Me.txtUnsocoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoCd.IsByteCheck = 5
        Me.txtUnsocoCd.IsCalendarCheck = False
        Me.txtUnsocoCd.IsDakutenCheck = False
        Me.txtUnsocoCd.IsEisuCheck = False
        Me.txtUnsocoCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoCd.IsFullByteCheck = 0
        Me.txtUnsocoCd.IsHankakuCheck = False
        Me.txtUnsocoCd.IsHissuCheck = False
        Me.txtUnsocoCd.IsKanaCheck = False
        Me.txtUnsocoCd.IsMiddleSpace = False
        Me.txtUnsocoCd.IsNumericCheck = False
        Me.txtUnsocoCd.IsSujiCheck = False
        Me.txtUnsocoCd.IsZenkakuCheck = False
        Me.txtUnsocoCd.ItemName = ""
        Me.txtUnsocoCd.LineSpace = 0
        Me.txtUnsocoCd.Location = New System.Drawing.Point(66, 32)
        Me.txtUnsocoCd.MaxLength = 5
        Me.txtUnsocoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoCd.MaxLineCount = 0
        Me.txtUnsocoCd.Multiline = False
        Me.txtUnsocoCd.Name = "txtUnsocoCd"
        Me.txtUnsocoCd.ReadOnly = False
        Me.txtUnsocoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoCd.Size = New System.Drawing.Size(97, 18)
        Me.txtUnsocoCd.TabIndex = 600
        Me.txtUnsocoCd.TabStopSetting = True
        Me.txtUnsocoCd.TextValue = "12345"
        Me.txtUnsocoCd.UseSystemPasswordChar = False
        Me.txtUnsocoCd.WidthDef = 97
        Me.txtUnsocoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(12, 16)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 599
        Me.LmTitleLabel3.Text = "営業所"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "営業所"
        Me.LmTitleLabel3.WidthDef = 49
        '
        'LMZ300F
        '
        Me.ClientSize = New System.Drawing.Size(594, 418)
        Me.Name = "LMZ300F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblUnsocoBrNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsocoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnsoco As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsocoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsocoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
