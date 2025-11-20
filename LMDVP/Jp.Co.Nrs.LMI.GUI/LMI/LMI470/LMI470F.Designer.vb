<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI470F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL8B

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
        Dim DateYearDisplayField5 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField9 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField5 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField10 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField5 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField5 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField5 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField5 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField6 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField11 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField6 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField12 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField6 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField6 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField6 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField6 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.grpOutputTani = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleSeikyuCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSeikyuNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeikyuCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleOutkaDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpOutputTaisyo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblOutkadatekara = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdOutkaDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.imdOutkaDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleKingaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numKingaku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.numKingakuSQL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlViewAria.SuspendLayout()
        Me.grpOutputTani.SuspendLayout()
        Me.grpOutputTaisyo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.numKingakuSQL)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.numKingaku)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKingaku)
        Me.pnlViewAria.Controls.Add(Me.grpOutputTani)
        Me.pnlViewAria.Controls.Add(Me.grpOutputTaisyo)
        Me.pnlViewAria.Size = New System.Drawing.Size(794, 488)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "マスタ参照"
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'grpOutputTani
        '
        Me.grpOutputTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpOutputTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpOutputTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpOutputTani.Controls.Add(Me.lblTitleSeikyuCd)
        Me.grpOutputTani.Controls.Add(Me.lblSeikyuNm)
        Me.grpOutputTani.Controls.Add(Me.txtSeikyuCd)
        Me.grpOutputTani.Controls.Add(Me.cmbEigyo)
        Me.grpOutputTani.Controls.Add(Me.lblTitleEigyo)
        Me.grpOutputTani.EnableStatus = False
        Me.grpOutputTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpOutputTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpOutputTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpOutputTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpOutputTani.HeightDef = 94
        Me.grpOutputTani.Location = New System.Drawing.Point(27, 22)
        Me.grpOutputTani.Name = "grpOutputTani"
        Me.grpOutputTani.Size = New System.Drawing.Size(736, 94)
        Me.grpOutputTani.TabIndex = 266
        Me.grpOutputTani.TabStop = False
        Me.grpOutputTani.TextValue = ""
        Me.grpOutputTani.UseCompatibleTextRendering = True
        Me.grpOutputTani.WidthDef = 736
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
        Me.lblTitleSeikyuCd.Location = New System.Drawing.Point(2, 55)
        Me.lblTitleSeikyuCd.Name = "lblTitleSeikyuCd"
        Me.lblTitleSeikyuCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeikyuCd.TabIndex = 257
        Me.lblTitleSeikyuCd.Text = "請求先"
        Me.lblTitleSeikyuCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuCd.TextValue = "請求先"
        Me.lblTitleSeikyuCd.WidthDef = 49
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
        Me.lblSeikyuNm.Location = New System.Drawing.Point(128, 52)
        Me.lblSeikyuNm.MaxLength = 0
        Me.lblSeikyuNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeikyuNm.MaxLineCount = 0
        Me.lblSeikyuNm.Multiline = False
        Me.lblSeikyuNm.Name = "lblSeikyuNm"
        Me.lblSeikyuNm.ReadOnly = True
        Me.lblSeikyuNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeikyuNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeikyuNm.Size = New System.Drawing.Size(565, 18)
        Me.lblSeikyuNm.TabIndex = 259
        Me.lblSeikyuNm.TabStop = False
        Me.lblSeikyuNm.TabStopSetting = False
        Me.lblSeikyuNm.TextValue = ""
        Me.lblSeikyuNm.UseSystemPasswordChar = False
        Me.lblSeikyuNm.WidthDef = 565
        Me.lblSeikyuNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtSeikyuCd.HissuLabelVisible = False
        Me.txtSeikyuCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSeikyuCd.IsByteCheck = 7
        Me.txtSeikyuCd.IsCalendarCheck = False
        Me.txtSeikyuCd.IsDakutenCheck = False
        Me.txtSeikyuCd.IsEisuCheck = False
        Me.txtSeikyuCd.IsForbiddenWordsCheck = False
        Me.txtSeikyuCd.IsFullByteCheck = 0
        Me.txtSeikyuCd.IsHankakuCheck = False
        Me.txtSeikyuCd.IsHissuCheck = False
        Me.txtSeikyuCd.IsKanaCheck = False
        Me.txtSeikyuCd.IsMiddleSpace = False
        Me.txtSeikyuCd.IsNumericCheck = False
        Me.txtSeikyuCd.IsSujiCheck = False
        Me.txtSeikyuCd.IsZenkakuCheck = False
        Me.txtSeikyuCd.ItemName = ""
        Me.txtSeikyuCd.LineSpace = 0
        Me.txtSeikyuCd.Location = New System.Drawing.Point(62, 52)
        Me.txtSeikyuCd.MaxLength = 7
        Me.txtSeikyuCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeikyuCd.MaxLineCount = 0
        Me.txtSeikyuCd.Multiline = False
        Me.txtSeikyuCd.Name = "txtSeikyuCd"
        Me.txtSeikyuCd.ReadOnly = False
        Me.txtSeikyuCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeikyuCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeikyuCd.Size = New System.Drawing.Size(82, 18)
        Me.txtSeikyuCd.TabIndex = 258
        Me.txtSeikyuCd.TabStopSetting = True
        Me.txtSeikyuCd.TextValue = "XXXXXXX"
        Me.txtSeikyuCd.UseSystemPasswordChar = False
        Me.txtSeikyuCd.WidthDef = 82
        Me.txtSeikyuCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbEigyo.HissuLabelVisible = True
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = True
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(62, 28)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = False
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 255
        Me.cmbEigyo.TabStopSetting = True
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(7, 31)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 256
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblTitleOutkaDate
        '
        Me.lblTitleOutkaDate.AutoSize = True
        Me.lblTitleOutkaDate.AutoSizeDef = True
        Me.lblTitleOutkaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutkaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutkaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOutkaDate.EnableStatus = False
        Me.lblTitleOutkaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutkaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutkaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutkaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutkaDate.HeightDef = 13
        Me.lblTitleOutkaDate.Location = New System.Drawing.Point(64, 33)
        Me.lblTitleOutkaDate.Name = "lblTitleOutkaDate"
        Me.lblTitleOutkaDate.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleOutkaDate.TabIndex = 262
        Me.lblTitleOutkaDate.Text = "請求期間"
        Me.lblTitleOutkaDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOutkaDate.TextValue = "請求期間"
        Me.lblTitleOutkaDate.WidthDef = 63
        '
        'grpOutputTaisyo
        '
        Me.grpOutputTaisyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpOutputTaisyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpOutputTaisyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpOutputTaisyo.Controls.Add(Me.lblOutkadatekara)
        Me.grpOutputTaisyo.Controls.Add(Me.imdOutkaDateTo)
        Me.grpOutputTaisyo.Controls.Add(Me.imdOutkaDateFrom)
        Me.grpOutputTaisyo.Controls.Add(Me.lblTitleOutkaDate)
        Me.grpOutputTaisyo.EnableStatus = False
        Me.grpOutputTaisyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpOutputTaisyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpOutputTaisyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpOutputTaisyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpOutputTaisyo.HeightDef = 73
        Me.grpOutputTaisyo.Location = New System.Drawing.Point(27, 146)
        Me.grpOutputTaisyo.Name = "grpOutputTaisyo"
        Me.grpOutputTaisyo.Size = New System.Drawing.Size(680, 73)
        Me.grpOutputTaisyo.TabIndex = 267
        Me.grpOutputTaisyo.TabStop = False
        Me.grpOutputTaisyo.Text = "処理期間"
        Me.grpOutputTaisyo.TextValue = "処理期間"
        Me.grpOutputTaisyo.WidthDef = 680
        '
        'lblOutkadatekara
        '
        Me.lblOutkadatekara.AutoSize = True
        Me.lblOutkadatekara.AutoSizeDef = True
        Me.lblOutkadatekara.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOutkadatekara.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOutkadatekara.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblOutkadatekara.EnableStatus = False
        Me.lblOutkadatekara.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOutkadatekara.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOutkadatekara.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOutkadatekara.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOutkadatekara.HeightDef = 13
        Me.lblOutkadatekara.Location = New System.Drawing.Point(260, 33)
        Me.lblOutkadatekara.Name = "lblOutkadatekara"
        Me.lblOutkadatekara.Size = New System.Drawing.Size(21, 13)
        Me.lblOutkadatekara.TabIndex = 296
        Me.lblOutkadatekara.Text = "～"
        Me.lblOutkadatekara.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblOutkadatekara.TextValue = "～"
        Me.lblOutkadatekara.WidthDef = 21
        '
        'imdOutkaDateTo
        '
        Me.imdOutkaDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOutkaDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOutkaDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdOutkaDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField5.ShowLeadingZero = True
        DateLiteralDisplayField9.Text = "/"
        DateMonthDisplayField5.ShowLeadingZero = True
        DateLiteralDisplayField10.Text = "/"
        DateDayDisplayField5.ShowLeadingZero = True
        Me.imdOutkaDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField9, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField10, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdOutkaDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOutkaDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOutkaDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOutkaDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOutkaDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField5, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField5, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField5, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdOutkaDateTo.HeightDef = 18
        Me.imdOutkaDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdOutkaDateTo.HissuLabelVisible = False
        Me.imdOutkaDateTo.Holiday = True
        Me.imdOutkaDateTo.IsAfterDateCheck = False
        Me.imdOutkaDateTo.IsBeforeDateCheck = False
        Me.imdOutkaDateTo.IsHissuCheck = False
        Me.imdOutkaDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdOutkaDateTo.ItemName = ""
        Me.imdOutkaDateTo.Location = New System.Drawing.Point(287, 30)
        Me.imdOutkaDateTo.Name = "imdOutkaDateTo"
        Me.imdOutkaDateTo.Number = CType(0, Long)
        Me.imdOutkaDateTo.ReadOnly = False
        Me.imdOutkaDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdOutkaDateTo.TabIndex = 295
        Me.imdOutkaDateTo.TabStopSetting = True
        Me.imdOutkaDateTo.TextValue = ""
        Me.imdOutkaDateTo.Value = New Date(CType(0, Long))
        Me.imdOutkaDateTo.WidthDef = 118
        '
        'imdOutkaDateFrom
        '
        Me.imdOutkaDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOutkaDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdOutkaDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdOutkaDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField6.ShowLeadingZero = True
        DateLiteralDisplayField11.Text = "/"
        DateMonthDisplayField6.ShowLeadingZero = True
        DateLiteralDisplayField12.Text = "/"
        DateDayDisplayField6.ShowLeadingZero = True
        Me.imdOutkaDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField11, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField12, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdOutkaDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOutkaDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdOutkaDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOutkaDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdOutkaDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField6, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField6, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField6, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdOutkaDateFrom.HeightDef = 18
        Me.imdOutkaDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdOutkaDateFrom.HissuLabelVisible = False
        Me.imdOutkaDateFrom.Holiday = True
        Me.imdOutkaDateFrom.IsAfterDateCheck = False
        Me.imdOutkaDateFrom.IsBeforeDateCheck = False
        Me.imdOutkaDateFrom.IsHissuCheck = False
        Me.imdOutkaDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdOutkaDateFrom.ItemName = ""
        Me.imdOutkaDateFrom.Location = New System.Drawing.Point(147, 30)
        Me.imdOutkaDateFrom.Name = "imdOutkaDateFrom"
        Me.imdOutkaDateFrom.Number = CType(0, Long)
        Me.imdOutkaDateFrom.ReadOnly = False
        Me.imdOutkaDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdOutkaDateFrom.TabIndex = 294
        Me.imdOutkaDateFrom.TabStopSetting = True
        Me.imdOutkaDateFrom.TextValue = ""
        Me.imdOutkaDateFrom.Value = New Date(CType(0, Long))
        Me.imdOutkaDateFrom.WidthDef = 118
        '
        'lblTitleKingaku
        '
        Me.lblTitleKingaku.AutoSizeDef = False
        Me.lblTitleKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKingaku.EnableStatus = False
        Me.lblTitleKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKingaku.HeightDef = 13
        Me.lblTitleKingaku.Location = New System.Drawing.Point(48, 230)
        Me.lblTitleKingaku.Name = "lblTitleKingaku"
        Me.lblTitleKingaku.Size = New System.Drawing.Size(106, 13)
        Me.lblTitleKingaku.TabIndex = 287
        Me.lblTitleKingaku.Text = "税抜き請求金額"
        Me.lblTitleKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKingaku.TextValue = "税抜き請求金額"
        Me.lblTitleKingaku.WidthDef = 106
        '
        'numKingaku
        '
        Me.numKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKingaku.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingaku.HeightDef = 18
        Me.numKingaku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingaku.HissuLabelVisible = False
        Me.numKingaku.IsHissuCheck = False
        Me.numKingaku.IsRangeCheck = False
        Me.numKingaku.ItemName = ""
        Me.numKingaku.Location = New System.Drawing.Point(174, 225)
        Me.numKingaku.Name = "numKingaku"
        Me.numKingaku.ReadOnly = False
        Me.numKingaku.Size = New System.Drawing.Size(140, 18)
        Me.numKingaku.TabIndex = 288
        Me.numKingaku.TabStopSetting = True
        Me.numKingaku.TextValue = "0"
        Me.numKingaku.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKingaku.WidthDef = 140
        '
        'numKingakuSQL
        '
        Me.numKingakuSQL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKingakuSQL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingakuSQL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKingakuSQL.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKingakuSQL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingakuSQL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingakuSQL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingakuSQL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingakuSQL.HeightDef = 18
        Me.numKingakuSQL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingakuSQL.HissuLabelVisible = False
        Me.numKingakuSQL.IsHissuCheck = False
        Me.numKingakuSQL.IsRangeCheck = False
        Me.numKingakuSQL.ItemName = ""
        Me.numKingakuSQL.Location = New System.Drawing.Point(174, 253)
        Me.numKingakuSQL.Name = "numKingakuSQL"
        Me.numKingakuSQL.ReadOnly = True
        Me.numKingakuSQL.Size = New System.Drawing.Size(140, 18)
        Me.numKingakuSQL.TabIndex = 290
        Me.numKingakuSQL.TabStop = False
        Me.numKingakuSQL.TabStopSetting = False
        Me.numKingakuSQL.TextValue = "0"
        Me.numKingakuSQL.UseWaitCursor = True
        Me.numKingakuSQL.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKingakuSQL.WidthDef = 140
        '
        'LmTitleLabel1
        '
        Me.LmTitleLabel1.AutoSizeDef = False
        Me.LmTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel1.EnableStatus = False
        Me.LmTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.HeightDef = 13
        Me.LmTitleLabel1.Location = New System.Drawing.Point(48, 258)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(106, 13)
        Me.LmTitleLabel1.TabIndex = 289
        Me.LmTitleLabel1.Text = "データ抽出金額"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "データ抽出金額"
        Me.LmTitleLabel1.WidthDef = 106
        '
        'LMI470F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI470F"
        Me.Text = "【LMI470】 日本合成化学　物流費作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpOutputTani.ResumeLayout(False)
        Me.grpOutputTani.PerformLayout()
        Me.grpOutputTaisyo.ResumeLayout(False)
        Me.grpOutputTaisyo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleOutkaDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpOutputTani As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents grpOutputTaisyo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblOutkadatekara As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdOutkaDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdOutkaDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleSeikyuCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSeikyuNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeikyuCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleKingaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numKingaku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numKingakuSQL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
