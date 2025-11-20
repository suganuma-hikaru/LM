<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMA010F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMMenuForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMA010F))
        Me.lblSystemTitle = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUserId = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitlePassword = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtUserId = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtPassword = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnOk = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnCancel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblVersion = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRePassword = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRePassword = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.picNrs = New System.Windows.Forms.PictureBox()
        Me.lblTitleLang = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpMessageType = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.optCn = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optKo = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optJp = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optEn = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.pnlViewAria.SuspendLayout
        Me.pnlTitleAria.SuspendLayout
        CType(Me.picNrs,System.ComponentModel.ISupportInitialize).BeginInit
        Me.grpMessageType.SuspendLayout
        Me.SuspendLayout
        '
        'pnlViewAria
        '
        Me.pnlViewAria.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.pnlViewAria.Controls.Add(Me.grpMessageType)
        Me.pnlViewAria.Controls.Add(Me.lblTitleLang)
        Me.pnlViewAria.Controls.Add(Me.txtRePassword)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRePassword)
        Me.pnlViewAria.Controls.Add(Me.btnCancel)
        Me.pnlViewAria.Controls.Add(Me.btnOk)
        Me.pnlViewAria.Controls.Add(Me.txtPassword)
        Me.pnlViewAria.Controls.Add(Me.txtUserId)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePassword)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUserId)
        Me.pnlViewAria.Controls.Add(Me.lblVersion)
        Me.pnlViewAria.Font = New System.Drawing.Font("HGSënâpÃﬂ⁄æﬁ›ΩEB", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.pnlViewAria.Size = New System.Drawing.Size(392, 212)
        '
        'pnlTitleAria
        '
        Me.pnlTitleAria.BackColor = System.Drawing.Color.White
        Me.pnlTitleAria.Controls.Add(Me.picNrs)
        Me.pnlTitleAria.Controls.Add(Me.lblSystemTitle)
        Me.pnlTitleAria.Size = New System.Drawing.Size(392, 40)
        '
        'lblSystemTitle
        '
        Me.lblSystemTitle.AutoSizeDef = false
        Me.lblSystemTitle.BackColor = System.Drawing.Color.White
        Me.lblSystemTitle.BackColorDef = System.Drawing.Color.White
        Me.lblSystemTitle.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSystemTitle.EnableStatus = false
        Me.lblSystemTitle.Font = New System.Drawing.Font("Century", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSystemTitle.FontDef = New System.Drawing.Font("Century", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSystemTitle.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblSystemTitle.ForeColorDef = System.Drawing.Color.DarkBlue
        Me.lblSystemTitle.HeightDef = 39
        Me.lblSystemTitle.Location = New System.Drawing.Point(61, 0)
        Me.lblSystemTitle.Name = "lblSystemTitle"
        Me.lblSystemTitle.Size = New System.Drawing.Size(328, 39)
        Me.lblSystemTitle.TabIndex = 3
        Me.lblSystemTitle.Text = "Logistics Management System"
        Me.lblSystemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSystemTitle.TextValue = "Logistics Management System"
        Me.lblSystemTitle.WidthDef = 328
        '
        'lblTitleUserId
        '
        Me.lblTitleUserId.AutoSizeDef = false
        Me.lblTitleUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleUserId.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleUserId.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUserId.EnableStatus = false
        Me.lblTitleUserId.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleUserId.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleUserId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleUserId.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleUserId.HeightDef = 13
        Me.lblTitleUserId.Location = New System.Drawing.Point(10, 33)
        Me.lblTitleUserId.Name = "lblTitleUserId"
        Me.lblTitleUserId.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleUserId.TabIndex = 0
        Me.lblTitleUserId.Text = "User ID"
        Me.lblTitleUserId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUserId.TextValue = "User ID"
        Me.lblTitleUserId.WidthDef = 119
        '
        'lblTitlePassword
        '
        Me.lblTitlePassword.AutoSizeDef = false
        Me.lblTitlePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitlePassword.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitlePassword.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePassword.EnableStatus = false
        Me.lblTitlePassword.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitlePassword.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitlePassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitlePassword.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitlePassword.HeightDef = 13
        Me.lblTitlePassword.Location = New System.Drawing.Point(10, 59)
        Me.lblTitlePassword.Name = "lblTitlePassword"
        Me.lblTitlePassword.Size = New System.Drawing.Size(119, 13)
        Me.lblTitlePassword.TabIndex = 1
        Me.lblTitlePassword.Text = "Password"
        Me.lblTitlePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePassword.TextValue = "Password"
        Me.lblTitlePassword.WidthDef = 119
        '
        'txtUserId
        '
        Me.txtUserId.BackColor = System.Drawing.Color.White
        Me.txtUserId.BackColorDef = System.Drawing.Color.White
        Me.txtUserId.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserId.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUserId.CountWrappedLine = false
        Me.txtUserId.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUserId.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtUserId.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtUserId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtUserId.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtUserId.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUserId.HeightDef = 18
        Me.txtUserId.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(192,Byte),Integer))
        Me.txtUserId.HissuLabelVisible = true
        Me.txtUserId.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUserId.IsByteCheck = 5
        Me.txtUserId.IsCalendarCheck = false
        Me.txtUserId.IsDakutenCheck = false
        Me.txtUserId.IsEisuCheck = false
        Me.txtUserId.IsForbiddenWordsCheck = false
        Me.txtUserId.IsFullByteCheck = 0
        Me.txtUserId.IsHankakuCheck = false
        Me.txtUserId.IsHissuCheck = true
        Me.txtUserId.IsKanaCheck = false
        Me.txtUserId.IsMiddleSpace = false
        Me.txtUserId.IsNumericCheck = false
        Me.txtUserId.IsSujiCheck = false
        Me.txtUserId.IsZenkakuCheck = false
        Me.txtUserId.ItemName = ""
        Me.txtUserId.LineSpace = 0
        Me.txtUserId.Location = New System.Drawing.Point(131, 31)
        Me.txtUserId.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUserId.MaxLength = 5
        Me.txtUserId.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUserId.MaxLineCount = 0
        Me.txtUserId.Multiline = false
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.ReadOnly = false
        Me.txtUserId.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUserId.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUserId.Size = New System.Drawing.Size(238, 18)
        Me.txtUserId.TabIndex = 2
        Me.txtUserId.TabStopSetting = true
        Me.txtUserId.TextValue = "X6X"
        Me.txtUserId.UseSystemPasswordChar = false
        Me.txtUserId.WidthDef = 238
        Me.txtUserId.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtPassword
        '
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.txtPassword.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.txtPassword.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPassword.CountWrappedLine = false
        Me.txtPassword.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtPassword.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtPassword.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtPassword.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtPassword.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtPassword.HeightDef = 18
        Me.txtPassword.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.txtPassword.HissuLabelVisible = true
        Me.txtPassword.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtPassword.IsByteCheck = 15
        Me.txtPassword.IsCalendarCheck = false
        Me.txtPassword.IsDakutenCheck = false
        Me.txtPassword.IsEisuCheck = false
        Me.txtPassword.IsForbiddenWordsCheck = false
        Me.txtPassword.IsFullByteCheck = 0
        Me.txtPassword.IsHankakuCheck = false
        Me.txtPassword.IsHissuCheck = true
        Me.txtPassword.IsKanaCheck = false
        Me.txtPassword.IsMiddleSpace = false
        Me.txtPassword.IsNumericCheck = false
        Me.txtPassword.IsSujiCheck = false
        Me.txtPassword.IsZenkakuCheck = false
        Me.txtPassword.ItemName = ""
        Me.txtPassword.LineSpace = 0
        Me.txtPassword.Location = New System.Drawing.Point(131, 57)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPassword.MaxLength = 15
        Me.txtPassword.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtPassword.MaxLineCount = 0
        Me.txtPassword.Multiline = false
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.ReadOnly = false
        Me.txtPassword.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPassword.Size = New System.Drawing.Size(238, 18)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.TabStopSetting = true
        Me.txtPassword.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtPassword.UseSystemPasswordChar = true
        Me.txtPassword.WidthDef = 238
        Me.txtPassword.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.FromArgb(CType(CType(246,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(242,Byte),Integer))
        Me.btnOk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(242,Byte),Integer))
        Me.btnOk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnOk.EnableStatus = true
        Me.btnOk.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnOk.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.btnOk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.btnOk.HeightDef = 25
        Me.btnOk.Location = New System.Drawing.Point(129, 175)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(110, 25)
        Me.btnOk.TabIndex = 4
        Me.btnOk.TabStopSetting = true
        Me.btnOk.Text = "Login"
        Me.btnOk.TextValue = "Login"
        Me.btnOk.UseVisualStyleBackColor = true
        Me.btnOk.WidthDef = 110
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(242,Byte),Integer))
        Me.btnCancel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246,Byte),Integer), CType(CType(246,Byte),Integer), CType(CType(242,Byte),Integer))
        Me.btnCancel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnCancel.EnableStatus = true
        Me.btnCancel.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnCancel.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.btnCancel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.btnCancel.HeightDef = 25
        Me.btnCancel.Location = New System.Drawing.Point(245, 175)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(110, 25)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.TabStopSetting = true
        Me.btnCancel.Text = "Close"
        Me.btnCancel.TextValue = "Close"
        Me.btnCancel.UseVisualStyleBackColor = true
        Me.btnCancel.WidthDef = 110
        '
        'lblVersion
        '
        Me.lblVersion.AutoSizeDef = false
        Me.lblVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblVersion.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblVersion.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblVersion.EnableStatus = false
        Me.lblVersion.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblVersion.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblVersion.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblVersion.HeightDef = 18
        Me.lblVersion.Location = New System.Drawing.Point(213, 1)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(177, 18)
        Me.lblVersion.TabIndex = 6
        Me.lblVersion.Text = "Version"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVersion.TextValue = "Version"
        Me.lblVersion.WidthDef = 177
        '
        'txtRePassword
        '
        Me.txtRePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.txtRePassword.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.txtRePassword.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRePassword.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRePassword.CountWrappedLine = false
        Me.txtRePassword.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRePassword.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtRePassword.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.txtRePassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtRePassword.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.txtRePassword.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRePassword.HeightDef = 18
        Me.txtRePassword.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.txtRePassword.HissuLabelVisible = true
        Me.txtRePassword.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRePassword.IsByteCheck = 15
        Me.txtRePassword.IsCalendarCheck = false
        Me.txtRePassword.IsDakutenCheck = false
        Me.txtRePassword.IsEisuCheck = false
        Me.txtRePassword.IsForbiddenWordsCheck = false
        Me.txtRePassword.IsFullByteCheck = 0
        Me.txtRePassword.IsHankakuCheck = false
        Me.txtRePassword.IsHissuCheck = true
        Me.txtRePassword.IsKanaCheck = false
        Me.txtRePassword.IsMiddleSpace = false
        Me.txtRePassword.IsNumericCheck = false
        Me.txtRePassword.IsSujiCheck = false
        Me.txtRePassword.IsZenkakuCheck = false
        Me.txtRePassword.ItemName = ""
        Me.txtRePassword.LineSpace = 0
        Me.txtRePassword.Location = New System.Drawing.Point(131, 84)
        Me.txtRePassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRePassword.MaxLength = 15
        Me.txtRePassword.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRePassword.MaxLineCount = 0
        Me.txtRePassword.Multiline = false
        Me.txtRePassword.Name = "txtRePassword"
        Me.txtRePassword.ReadOnly = false
        Me.txtRePassword.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRePassword.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRePassword.Size = New System.Drawing.Size(238, 18)
        Me.txtRePassword.TabIndex = 8
        Me.txtRePassword.TabStopSetting = true
        Me.txtRePassword.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtRePassword.UseSystemPasswordChar = true
        Me.txtRePassword.WidthDef = 238
        Me.txtRePassword.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRePassword
        '
        Me.lblTitleRePassword.AutoSizeDef = false
        Me.lblTitleRePassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleRePassword.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleRePassword.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRePassword.EnableStatus = false
        Me.lblTitleRePassword.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleRePassword.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleRePassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleRePassword.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleRePassword.HeightDef = 13
        Me.lblTitleRePassword.Location = New System.Drawing.Point(10, 86)
        Me.lblTitleRePassword.Name = "lblTitleRePassword"
        Me.lblTitleRePassword.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleRePassword.TabIndex = 7
        Me.lblTitleRePassword.Text = "Re-type password"
        Me.lblTitleRePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRePassword.TextValue = "Re-type password"
        Me.lblTitleRePassword.WidthDef = 119
        '
        'picNrs
        '
        Me.picNrs.ErrorImage = CType(resources.GetObject("picNrs.ErrorImage"),System.Drawing.Image)
        Me.picNrs.Image = CType(resources.GetObject("picNrs.Image"),System.Drawing.Image)
        Me.picNrs.Location = New System.Drawing.Point(12, 1)
        Me.picNrs.Name = "picNrs"
        Me.picNrs.Size = New System.Drawing.Size(43, 37)
        Me.picNrs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picNrs.TabIndex = 1
        Me.picNrs.TabStop = false
        '
        'lblTitleLang
        '
        Me.lblTitleLang.AutoSizeDef = false
        Me.lblTitleLang.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleLang.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.lblTitleLang.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLang.EnableStatus = false
        Me.lblTitleLang.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleLang.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.lblTitleLang.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleLang.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.lblTitleLang.HeightDef = 13
        Me.lblTitleLang.Location = New System.Drawing.Point(10, 117)
        Me.lblTitleLang.Name = "lblTitleLang"
        Me.lblTitleLang.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleLang.TabIndex = 9
        Me.lblTitleLang.Text = "Language"
        Me.lblTitleLang.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLang.TextValue = "Language"
        Me.lblTitleLang.WidthDef = 119
        '
        'grpMessageType
        '
        Me.grpMessageType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.grpMessageType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.grpMessageType.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMessageType.Controls.Add(Me.optCn)
        Me.grpMessageType.Controls.Add(Me.optKo)
        Me.grpMessageType.Controls.Add(Me.optJp)
        Me.grpMessageType.Controls.Add(Me.optEn)
        Me.grpMessageType.EnableStatus = false
        Me.grpMessageType.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.grpMessageType.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.grpMessageType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.grpMessageType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.grpMessageType.HeightDef = 65
        Me.grpMessageType.Location = New System.Drawing.Point(130, 104)
        Me.grpMessageType.Name = "grpMessageType"
        Me.grpMessageType.Size = New System.Drawing.Size(225, 65)
        Me.grpMessageType.TabIndex = 10
        Me.grpMessageType.TabStop = false
        Me.grpMessageType.TextValue = ""
        Me.grpMessageType.WidthDef = 225
        '
        'optCn
        '
        Me.optCn.AutoSize = true
        Me.optCn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optCn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optCn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optCn.EnableStatus = True
        Me.optCn.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optCn.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optCn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optCn.HeightDef = 21
        Me.optCn.Location = New System.Drawing.Point(123, 38)
        Me.optCn.Name = "optCn"
        Me.optCn.Size = New System.Drawing.Size(92, 21)
        Me.optCn.TabIndex = 3
        Me.optCn.TabStop = True
        Me.optCn.TabStopSetting = True
        Me.optCn.Text = "Chinese"
        Me.optCn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optCn.TextValue = "Chinese"
        Me.optCn.UseVisualStyleBackColor = False
        Me.optCn.WidthDef = 92
        '
        'optKo
        '
        Me.optKo.AutoSize = True
        Me.optKo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKo.EnableStatus = True
        Me.optKo.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optKo.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optKo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optKo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optKo.HeightDef = 21
        Me.optKo.Location = New System.Drawing.Point(34, 38)
        Me.optKo.Name = "optKo"
        Me.optKo.Size = New System.Drawing.Size(83, 21)
        Me.optKo.TabIndex = 2
        Me.optKo.TabStop = true
        Me.optKo.TabStopSetting = true
        Me.optKo.Text = "Korean"
        Me.optKo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKo.TextValue = "Korean"
        Me.optKo.UseVisualStyleBackColor = false
        Me.optKo.WidthDef = 83
        '
        'optJp
        '
        Me.optJp.AutoSize = true
        Me.optJp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optJp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optJp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optJp.EnableStatus = true
        Me.optJp.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optJp.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optJp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optJp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optJp.HeightDef = 21
        Me.optJp.Location = New System.Drawing.Point(123, 12)
        Me.optJp.Name = "optJp"
        Me.optJp.Size = New System.Drawing.Size(101, 21)
        Me.optJp.TabIndex = 1
        Me.optJp.TabStop = true
        Me.optJp.TabStopSetting = true
        Me.optJp.Text = "Japanese"
        Me.optJp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optJp.TextValue = "Japanese"
        Me.optJp.UseVisualStyleBackColor = false
        Me.optJp.WidthDef = 101
        '
        'optEn
        '
        Me.optEn.AutoSize = true
        Me.optEn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optEn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236,Byte),Integer), CType(CType(233,Byte),Integer), CType(CType(216,Byte),Integer))
        Me.optEn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optEn.EnableStatus = true
        Me.optEn.Font = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optEn.FontDef = New System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
        Me.optEn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optEn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.optEn.HeightDef = 21
        Me.optEn.Location = New System.Drawing.Point(34, 12)
        Me.optEn.Name = "optEn"
        Me.optEn.Size = New System.Drawing.Size(92, 21)
        Me.optEn.TabIndex = 0
        Me.optEn.TabStop = true
        Me.optEn.TabStopSetting = true
        Me.optEn.Text = "English"
        Me.optEn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optEn.TextValue = "English"
        Me.optEn.UseVisualStyleBackColor = false
        Me.optEn.WidthDef = 92
        '
        'LMA010F
        '
        Me.ClientSize = New System.Drawing.Size(392, 292)
        Me.KeyPreview = true
        Me.Name = "LMA010F"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ÅyLMA010Åz ÉçÉOÉCÉì"
        Me.pnlViewAria.ResumeLayout(false)
        Me.pnlTitleAria.ResumeLayout(false)
        CType(Me.picNrs,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpMessageType.ResumeLayout(false)
        Me.grpMessageType.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents picNrs As System.Windows.Forms.PictureBox
    Friend WithEvents lblSystemTitle As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitlePassword As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUserId As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtPassword As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUserId As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnCancel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnOk As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblVersion As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRePassword As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRePassword As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpMessageType As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleLang As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents optJp As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optEn As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optCn As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optKo As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton

End Class
