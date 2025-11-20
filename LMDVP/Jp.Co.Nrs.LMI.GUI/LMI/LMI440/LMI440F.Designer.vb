<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI440F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopM

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
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
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.grpAction = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblActionName = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbAction = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.imdArrPlanDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.imdArrPlanDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.TitleLabelKara = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblArrPlanDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlViewAria.SuspendLayout()
        Me.grpAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpAction)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F9ButtonName = "実 行"
        '
        'grpAction
        '
        Me.grpAction.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpAction.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpAction.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpAction.Controls.Add(Me.lblActionName)
        Me.grpAction.Controls.Add(Me.cmbAction)
        Me.grpAction.Controls.Add(Me.imdArrPlanDateTo)
        Me.grpAction.Controls.Add(Me.imdArrPlanDateFrom)
        Me.grpAction.Controls.Add(Me.TitleLabelKara)
        Me.grpAction.Controls.Add(Me.lblArrPlanDate)
        Me.grpAction.EnableStatus = False
        Me.grpAction.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpAction.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpAction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpAction.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpAction.HeightDef = 120
        Me.grpAction.Location = New System.Drawing.Point(20, 28)
        Me.grpAction.Name = "grpAction"
        Me.grpAction.Size = New System.Drawing.Size(554, 120)
        Me.grpAction.TabIndex = 413
        Me.grpAction.TabStop = False
        Me.grpAction.Text = "処理条件"
        Me.grpAction.TextValue = "処理条件"
        Me.grpAction.WidthDef = 554
        '
        'lblActionName
        '
        Me.lblActionName.AutoSize = True
        Me.lblActionName.AutoSizeDef = True
        Me.lblActionName.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblActionName.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblActionName.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblActionName.EnableStatus = False
        Me.lblActionName.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblActionName.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblActionName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblActionName.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblActionName.HeightDef = 13
        Me.lblActionName.Location = New System.Drawing.Point(57, 36)
        Me.lblActionName.Name = "lblActionName"
        Me.lblActionName.Size = New System.Drawing.Size(35, 13)
        Me.lblActionName.TabIndex = 413
        Me.lblActionName.Text = "処理"
        Me.lblActionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblActionName.TextValue = "処理"
        Me.lblActionName.WidthDef = 35
        '
        'cmbAction
        '
        Me.cmbAction.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAction.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAction.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbAction.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbAction.DataCode = "D031"
        Me.cmbAction.DataSource = Nothing
        Me.cmbAction.DisplayMember = Nothing
        Me.cmbAction.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAction.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAction.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAction.HeightDef = 18
        Me.cmbAction.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbAction.HissuLabelVisible = True
        Me.cmbAction.InsertWildCard = False
        Me.cmbAction.IsForbiddenWordsCheck = False
        Me.cmbAction.IsHissuCheck = True
        Me.cmbAction.ItemName = ""
        Me.cmbAction.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbAction.Location = New System.Drawing.Point(99, 33)
        Me.cmbAction.Name = "cmbAction"
        Me.cmbAction.ReadOnly = False
        Me.cmbAction.SelectedIndex = -1
        Me.cmbAction.SelectedItem = Nothing
        Me.cmbAction.SelectedText = ""
        Me.cmbAction.SelectedValue = ""
        Me.cmbAction.Size = New System.Drawing.Size(376, 18)
        Me.cmbAction.TabIndex = 412
        Me.cmbAction.TabStopSetting = True
        Me.cmbAction.TextValue = ""
        Me.cmbAction.Value1 = Nothing
        Me.cmbAction.Value2 = Nothing
        Me.cmbAction.Value3 = Nothing
        Me.cmbAction.ValueMember = Nothing
        Me.cmbAction.WidthDef = 376
        '
        'imdArrPlanDateTo
        '
        Me.imdArrPlanDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdArrPlanDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdArrPlanDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdArrPlanDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdArrPlanDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdArrPlanDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArrPlanDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArrPlanDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArrPlanDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArrPlanDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdArrPlanDateTo.HeightDef = 18
        Me.imdArrPlanDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdArrPlanDateTo.HissuLabelVisible = False
        Me.imdArrPlanDateTo.Holiday = True
        Me.imdArrPlanDateTo.IsAfterDateCheck = False
        Me.imdArrPlanDateTo.IsBeforeDateCheck = False
        Me.imdArrPlanDateTo.IsHissuCheck = False
        Me.imdArrPlanDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdArrPlanDateTo.ItemName = ""
        Me.imdArrPlanDateTo.Location = New System.Drawing.Point(245, 65)
        Me.imdArrPlanDateTo.Name = "imdArrPlanDateTo"
        Me.imdArrPlanDateTo.Number = CType(0, Long)
        Me.imdArrPlanDateTo.ReadOnly = False
        Me.imdArrPlanDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdArrPlanDateTo.TabIndex = 411
        Me.imdArrPlanDateTo.TabStopSetting = True
        Me.imdArrPlanDateTo.TextValue = ""
        Me.imdArrPlanDateTo.Value = New Date(CType(0, Long))
        Me.imdArrPlanDateTo.WidthDef = 118
        '
        'imdArrPlanDateFrom
        '
        Me.imdArrPlanDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdArrPlanDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdArrPlanDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdArrPlanDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdArrPlanDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdArrPlanDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArrPlanDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArrPlanDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArrPlanDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArrPlanDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdArrPlanDateFrom.HeightDef = 18
        Me.imdArrPlanDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdArrPlanDateFrom.HissuLabelVisible = False
        Me.imdArrPlanDateFrom.Holiday = True
        Me.imdArrPlanDateFrom.IsAfterDateCheck = False
        Me.imdArrPlanDateFrom.IsBeforeDateCheck = False
        Me.imdArrPlanDateFrom.IsHissuCheck = False
        Me.imdArrPlanDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdArrPlanDateFrom.ItemName = ""
        Me.imdArrPlanDateFrom.Location = New System.Drawing.Point(99, 65)
        Me.imdArrPlanDateFrom.Name = "imdArrPlanDateFrom"
        Me.imdArrPlanDateFrom.Number = CType(0, Long)
        Me.imdArrPlanDateFrom.ReadOnly = False
        Me.imdArrPlanDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdArrPlanDateFrom.TabIndex = 410
        Me.imdArrPlanDateFrom.TabStopSetting = True
        Me.imdArrPlanDateFrom.TextValue = ""
        Me.imdArrPlanDateFrom.Value = New Date(CType(0, Long))
        Me.imdArrPlanDateFrom.WidthDef = 118
        '
        'TitleLabelKara
        '
        Me.TitleLabelKara.AutoSize = True
        Me.TitleLabelKara.AutoSizeDef = True
        Me.TitleLabelKara.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleLabelKara.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitleLabelKara.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitleLabelKara.EnableStatus = False
        Me.TitleLabelKara.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleLabelKara.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleLabelKara.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleLabelKara.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitleLabelKara.HeightDef = 13
        Me.TitleLabelKara.Location = New System.Drawing.Point(218, 70)
        Me.TitleLabelKara.Name = "TitleLabelKara"
        Me.TitleLabelKara.Size = New System.Drawing.Size(21, 13)
        Me.TitleLabelKara.TabIndex = 129
        Me.TitleLabelKara.Text = "～"
        Me.TitleLabelKara.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitleLabelKara.TextValue = "～"
        Me.TitleLabelKara.WidthDef = 21
        '
        'lblArrPlanDate
        '
        Me.lblArrPlanDate.AutoSize = True
        Me.lblArrPlanDate.AutoSizeDef = True
        Me.lblArrPlanDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblArrPlanDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblArrPlanDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblArrPlanDate.EnableStatus = False
        Me.lblArrPlanDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblArrPlanDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblArrPlanDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblArrPlanDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblArrPlanDate.HeightDef = 13
        Me.lblArrPlanDate.Location = New System.Drawing.Point(17, 67)
        Me.lblArrPlanDate.Name = "lblArrPlanDate"
        Me.lblArrPlanDate.Size = New System.Drawing.Size(77, 13)
        Me.lblArrPlanDate.TabIndex = 126
        Me.lblArrPlanDate.Text = "納入予定日"
        Me.lblArrPlanDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblArrPlanDate.TextValue = "納入予定日"
        Me.lblArrPlanDate.WidthDef = 77
        '
        'LMI440F
        '
        Me.ClientSize = New System.Drawing.Size(594, 425)
        Me.Name = "LMI440F"
        Me.Text = "【LMI440】 輸送ファイル編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpAction.ResumeLayout(False)
        Me.grpAction.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpAction As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbAction As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents imdArrPlanDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdArrPlanDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents TitleLabelKara As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblArrPlanDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblActionName As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
