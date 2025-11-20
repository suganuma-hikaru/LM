<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI990F
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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.grpSakusei = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleSeikyuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleFilePath = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtFilePath = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.imdSeikyuDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlViewAria.SuspendLayout()
        Me.grpSakusei.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpSakusei)
        Me.pnlViewAria.Size = New System.Drawing.Size(794, 488)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'grpSakusei
        '
        Me.grpSakusei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSakusei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSakusei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSakusei.Controls.Add(Me.lblTitleSeikyuDate)
        Me.grpSakusei.Controls.Add(Me.lblTitleFilePath)
        Me.grpSakusei.Controls.Add(Me.txtFilePath)
        Me.grpSakusei.Controls.Add(Me.imdSeikyuDate)
        Me.grpSakusei.Controls.Add(Me.cmbEigyo)
        Me.grpSakusei.Controls.Add(Me.lblTitleEigyo)
        Me.grpSakusei.EnableStatus = False
        Me.grpSakusei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSakusei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSakusei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSakusei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSakusei.HeightDef = 110
        Me.grpSakusei.Location = New System.Drawing.Point(27, 19)
        Me.grpSakusei.Name = "grpSakusei"
        Me.grpSakusei.Size = New System.Drawing.Size(738, 110)
        Me.grpSakusei.TabIndex = 247
        Me.grpSakusei.TabStop = False
        Me.grpSakusei.Text = "作成条件"
        Me.grpSakusei.TextValue = "作成条件"
        Me.grpSakusei.WidthDef = 738
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
        Me.lblTitleSeikyuDate.Location = New System.Drawing.Point(62, 75)
        Me.lblTitleSeikyuDate.Name = "lblTitleSeikyuDate"
        Me.lblTitleSeikyuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeikyuDate.TabIndex = 650
        Me.lblTitleSeikyuDate.Text = "請求日"
        Me.lblTitleSeikyuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuDate.TextValue = "請求日"
        Me.lblTitleSeikyuDate.WidthDef = 49
        '
        'lblTitleFilePath
        '
        Me.lblTitleFilePath.AutoSize = True
        Me.lblTitleFilePath.AutoSizeDef = True
        Me.lblTitleFilePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFilePath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFilePath.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFilePath.EnableStatus = False
        Me.lblTitleFilePath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFilePath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFilePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFilePath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFilePath.HeightDef = 13
        Me.lblTitleFilePath.Location = New System.Drawing.Point(20, 51)
        Me.lblTitleFilePath.Name = "lblTitleFilePath"
        Me.lblTitleFilePath.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleFilePath.TabIndex = 649
        Me.lblTitleFilePath.Text = "取込ファイル"
        Me.lblTitleFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFilePath.TextValue = "取込ファイル"
        Me.lblTitleFilePath.WidthDef = 91
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFilePath.CountWrappedLine = False
        Me.txtFilePath.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFilePath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFilePath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFilePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFilePath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFilePath.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFilePath.HeightDef = 18
        Me.txtFilePath.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFilePath.HissuLabelVisible = True
        Me.txtFilePath.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtFilePath.IsByteCheck = 0
        Me.txtFilePath.IsCalendarCheck = False
        Me.txtFilePath.IsDakutenCheck = False
        Me.txtFilePath.IsEisuCheck = False
        Me.txtFilePath.IsForbiddenWordsCheck = False
        Me.txtFilePath.IsFullByteCheck = 0
        Me.txtFilePath.IsHankakuCheck = False
        Me.txtFilePath.IsHissuCheck = True
        Me.txtFilePath.IsKanaCheck = False
        Me.txtFilePath.IsMiddleSpace = False
        Me.txtFilePath.IsNumericCheck = False
        Me.txtFilePath.IsSujiCheck = False
        Me.txtFilePath.IsZenkakuCheck = False
        Me.txtFilePath.ItemName = ""
        Me.txtFilePath.LineSpace = 0
        Me.txtFilePath.Location = New System.Drawing.Point(117, 49)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFilePath.MaxLineCount = 0
        Me.txtFilePath.Multiline = False
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFilePath.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFilePath.Size = New System.Drawing.Size(595, 18)
        Me.txtFilePath.TabIndex = 648
        Me.txtFilePath.TabStop = False
        Me.txtFilePath.TabStopSetting = False
        Me.txtFilePath.TextValue = ""
        Me.txtFilePath.UseSystemPasswordChar = False
        Me.txtFilePath.WidthDef = 595
        Me.txtFilePath.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdSeikyuDate
        '
        Me.imdSeikyuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeikyuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeikyuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSeikyuDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSeikyuDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSeikyuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeikyuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeikyuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeikyuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeikyuDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSeikyuDate.HeightDef = 18
        Me.imdSeikyuDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSeikyuDate.HissuLabelVisible = True
        Me.imdSeikyuDate.Holiday = True
        Me.imdSeikyuDate.IsAfterDateCheck = False
        Me.imdSeikyuDate.IsBeforeDateCheck = False
        Me.imdSeikyuDate.IsHissuCheck = True
        Me.imdSeikyuDate.IsMinDateCheck = "1900/01/01"
        Me.imdSeikyuDate.ItemName = ""
        Me.imdSeikyuDate.Location = New System.Drawing.Point(117, 73)
        Me.imdSeikyuDate.Name = "imdSeikyuDate"
        Me.imdSeikyuDate.Number = CType(10101000000, Long)
        Me.imdSeikyuDate.ReadOnly = False
        Me.imdSeikyuDate.Size = New System.Drawing.Size(125, 18)
        Me.imdSeikyuDate.TabIndex = 646
        Me.imdSeikyuDate.TabStopSetting = True
        Me.imdSeikyuDate.TextValue = ""
        Me.imdSeikyuDate.Value = New Date(CType(0, Long))
        Me.imdSeikyuDate.WidthDef = 125
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
        Me.cmbEigyo.HissuLabelVisible = True
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = True
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(117, 25)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 416
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(58, 27)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 417
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'LMI990F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI990F"
        Me.Text = "【LMI990】 サーテック運賃明細作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpSakusei.ResumeLayout(False)
        Me.grpSakusei.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSakusei As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSeikyuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleFilePath As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtFilePath As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdSeikyuDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate

End Class
