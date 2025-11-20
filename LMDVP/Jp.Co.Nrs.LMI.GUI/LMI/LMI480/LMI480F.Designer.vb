<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI480F
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
        Me.btnMake = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbSelectKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.imdKikanYM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblTitleKikanYM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSelectKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
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
        Me.grpSakusei.Controls.Add(Me.btnMake)
        Me.grpSakusei.Controls.Add(Me.cmbSelectKb)
        Me.grpSakusei.Controls.Add(Me.imdKikanYM)
        Me.grpSakusei.Controls.Add(Me.lblTitleKikanYM)
        Me.grpSakusei.Controls.Add(Me.lblTitleSelectKb)
        Me.grpSakusei.EnableStatus = False
        Me.grpSakusei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSakusei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSakusei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSakusei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSakusei.HeightDef = 106
        Me.grpSakusei.Location = New System.Drawing.Point(27, 19)
        Me.grpSakusei.Name = "grpSakusei"
        Me.grpSakusei.Size = New System.Drawing.Size(738, 106)
        Me.grpSakusei.TabIndex = 247
        Me.grpSakusei.TabStop = False
        Me.grpSakusei.Text = "作成条件"
        Me.grpSakusei.TextValue = "作成条件"
        Me.grpSakusei.WidthDef = 738
        '
        'btnMake
        '
        Me.btnMake.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnMake.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnMake.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnMake.EnableStatus = True
        Me.btnMake.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMake.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMake.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMake.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnMake.HeightDef = 22
        Me.btnMake.Location = New System.Drawing.Point(255, 57)
        Me.btnMake.Name = "btnMake"
        Me.btnMake.Size = New System.Drawing.Size(70, 22)
        Me.btnMake.TabIndex = 415
        Me.btnMake.TabStopSetting = True
        Me.btnMake.Text = "作成"
        Me.btnMake.TextValue = "作成"
        Me.btnMake.UseVisualStyleBackColor = True
        Me.btnMake.WidthDef = 70
        '
        'cmbSelectKb
        '
        Me.cmbSelectKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSelectKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSelectKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSelectKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSelectKb.DataCode = "S113"
        Me.cmbSelectKb.DataSource = Nothing
        Me.cmbSelectKb.DisplayMember = Nothing
        Me.cmbSelectKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSelectKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSelectKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSelectKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSelectKb.HeightDef = 18
        Me.cmbSelectKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSelectKb.HissuLabelVisible = True
        Me.cmbSelectKb.InsertWildCard = True
        Me.cmbSelectKb.IsForbiddenWordsCheck = False
        Me.cmbSelectKb.IsHissuCheck = True
        Me.cmbSelectKb.ItemName = ""
        Me.cmbSelectKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSelectKb.Location = New System.Drawing.Point(113, 25)
        Me.cmbSelectKb.Name = "cmbSelectKb"
        Me.cmbSelectKb.ReadOnly = False
        Me.cmbSelectKb.SelectedIndex = -1
        Me.cmbSelectKb.SelectedItem = Nothing
        Me.cmbSelectKb.SelectedText = ""
        Me.cmbSelectKb.SelectedValue = ""
        Me.cmbSelectKb.Size = New System.Drawing.Size(226, 18)
        Me.cmbSelectKb.TabIndex = 409
        Me.cmbSelectKb.TabStopSetting = True
        Me.cmbSelectKb.TextValue = ""
        Me.cmbSelectKb.Value1 = Nothing
        Me.cmbSelectKb.Value2 = Nothing
        Me.cmbSelectKb.Value3 = Nothing
        Me.cmbSelectKb.ValueMember = Nothing
        Me.cmbSelectKb.WidthDef = 226
        '
        'imdKikanYM
        '
        Me.imdKikanYM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdKikanYM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdKikanYM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdKikanYM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdKikanYM.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdKikanYM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdKikanYM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdKikanYM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdKikanYM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdKikanYM.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdKikanYM.HeightDef = 18
        Me.imdKikanYM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdKikanYM.HissuLabelVisible = True
        Me.imdKikanYM.Holiday = True
        Me.imdKikanYM.IsAfterDateCheck = False
        Me.imdKikanYM.IsBeforeDateCheck = False
        Me.imdKikanYM.IsHissuCheck = True
        Me.imdKikanYM.IsMinDateCheck = "1900/01/01"
        Me.imdKikanYM.ItemName = ""
        Me.imdKikanYM.Location = New System.Drawing.Point(113, 59)
        Me.imdKikanYM.Name = "imdKikanYM"
        Me.imdKikanYM.Number = CType(10101000000, Long)
        Me.imdKikanYM.ReadOnly = False
        Me.imdKikanYM.Size = New System.Drawing.Size(118, 18)
        Me.imdKikanYM.TabIndex = 414
        Me.imdKikanYM.TabStopSetting = True
        Me.imdKikanYM.TextValue = ""
        Me.imdKikanYM.Value = New Date(CType(0, Long))
        Me.imdKikanYM.WidthDef = 118
        '
        'lblTitleKikanYM
        '
        Me.lblTitleKikanYM.AutoSize = True
        Me.lblTitleKikanYM.AutoSizeDef = True
        Me.lblTitleKikanYM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKikanYM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKikanYM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKikanYM.EnableStatus = False
        Me.lblTitleKikanYM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKikanYM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKikanYM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKikanYM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKikanYM.HeightDef = 13
        Me.lblTitleKikanYM.Location = New System.Drawing.Point(72, 62)
        Me.lblTitleKikanYM.Name = "lblTitleKikanYM"
        Me.lblTitleKikanYM.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKikanYM.TabIndex = 413
        Me.lblTitleKikanYM.Text = "期間"
        Me.lblTitleKikanYM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKikanYM.TextValue = "期間"
        Me.lblTitleKikanYM.WidthDef = 35
        '
        'lblTitleSelectKb
        '
        Me.lblTitleSelectKb.AutoSize = True
        Me.lblTitleSelectKb.AutoSizeDef = True
        Me.lblTitleSelectKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSelectKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSelectKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSelectKb.EnableStatus = False
        Me.lblTitleSelectKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSelectKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSelectKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSelectKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSelectKb.HeightDef = 13
        Me.lblTitleSelectKb.Location = New System.Drawing.Point(44, 28)
        Me.lblTitleSelectKb.Name = "lblTitleSelectKb"
        Me.lblTitleSelectKb.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSelectKb.TabIndex = 17
        Me.lblTitleSelectKb.Text = "抽出区分"
        Me.lblTitleSelectKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSelectKb.TextValue = "抽出区分"
        Me.lblTitleSelectKb.WidthDef = 63
        '
        'LMI480F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI480F"
        Me.Text = "【LMI480F】  古河請求"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpSakusei.ResumeLayout(False)
        Me.grpSakusei.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSakusei As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbSelectKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents imdKikanYM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleKikanYM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSelectKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnMake As Jp.Co.Nrs.LM.GUI.Win.LMButton

End Class
