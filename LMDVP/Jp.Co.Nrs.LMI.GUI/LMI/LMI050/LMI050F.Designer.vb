<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI050F
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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.grpSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbCust = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.lblTitleOutput = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkEDI = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkExcel = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkMail = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.grpZaiko = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.optZaizan = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optZaiko = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.lblTitleDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        Me.grpZaiko.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpSearch)
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSearch.Controls.Add(Me.cmbCust)
        Me.grpSearch.Controls.Add(Me.lblTitleOutput)
        Me.grpSearch.Controls.Add(Me.chkEDI)
        Me.grpSearch.Controls.Add(Me.chkExcel)
        Me.grpSearch.Controls.Add(Me.chkMail)
        Me.grpSearch.Controls.Add(Me.grpZaiko)
        Me.grpSearch.Controls.Add(Me.lblTitleDate)
        Me.grpSearch.Controls.Add(Me.imdDate)
        Me.grpSearch.Controls.Add(Me.cmbEigyo)
        Me.grpSearch.Controls.Add(Me.lblTitleCust)
        Me.grpSearch.Controls.Add(Me.lblTitleEigyo)
        Me.grpSearch.EnableStatus = False
        Me.grpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.HeightDef = 168
        Me.grpSearch.Location = New System.Drawing.Point(3, 19)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(1259, 168)
        Me.grpSearch.TabIndex = 238
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "出力条件"
        Me.grpSearch.TextValue = "出力条件"
        Me.grpSearch.WidthDef = 1259
        '
        'cmbCust
        '
        Me.cmbCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCust.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCust.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCust.DataSource = Nothing
        Me.cmbCust.DisplayMember = Nothing
        Me.cmbCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCust.HeightDef = 18
        Me.cmbCust.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCust.HissuLabelVisible = True
        Me.cmbCust.InsertWildCard = True
        Me.cmbCust.IsForbiddenWordsCheck = False
        Me.cmbCust.IsHissuCheck = True
        Me.cmbCust.ItemName = ""
        Me.cmbCust.Location = New System.Drawing.Point(99, 47)
        Me.cmbCust.Name = "cmbCust"
        Me.cmbCust.ReadOnly = False
        Me.cmbCust.SelectedIndex = -1
        Me.cmbCust.SelectedItem = Nothing
        Me.cmbCust.SelectedText = ""
        Me.cmbCust.SelectedValue = ""
        Me.cmbCust.Size = New System.Drawing.Size(311, 18)
        Me.cmbCust.TabIndex = 239
        Me.cmbCust.TabStopSetting = True
        Me.cmbCust.TextValue = ""
        Me.cmbCust.ValueMember = Nothing
        Me.cmbCust.WidthDef = 311
        '
        'lblTitleOutput
        '
        Me.lblTitleOutput.AutoSize = True
        Me.lblTitleOutput.AutoSizeDef = True
        Me.lblTitleOutput.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutput.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutput.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOutput.EnableStatus = False
        Me.lblTitleOutput.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutput.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutput.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutput.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutput.HeightDef = 13
        Me.lblTitleOutput.Location = New System.Drawing.Point(47, 96)
        Me.lblTitleOutput.Name = "lblTitleOutput"
        Me.lblTitleOutput.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleOutput.TabIndex = 267
        Me.lblTitleOutput.Text = "出力先"
        Me.lblTitleOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOutput.TextValue = "出力先"
        Me.lblTitleOutput.WidthDef = 49
        '
        'chkEDI
        '
        Me.chkEDI.AutoSize = True
        Me.chkEDI.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkEDI.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkEDI.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkEDI.EnableStatus = True
        Me.chkEDI.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkEDI.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkEDI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkEDI.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkEDI.HeightDef = 17
        Me.chkEDI.Location = New System.Drawing.Point(99, 94)
        Me.chkEDI.Name = "chkEDI"
        Me.chkEDI.Size = New System.Drawing.Size(215, 17)
        Me.chkEDI.TabIndex = 266
        Me.chkEDI.TabStopSetting = True
        Me.chkEDI.Text = "EDI送信用にデータを作成する"
        Me.chkEDI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkEDI.TextValue = "EDI送信用にデータを作成する"
        Me.chkEDI.UseVisualStyleBackColor = True
        Me.chkEDI.WidthDef = 215
        '
        'chkExcel
        '
        Me.chkExcel.AutoSize = True
        Me.chkExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkExcel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkExcel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkExcel.EnableStatus = True
        Me.chkExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkExcel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkExcel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkExcel.HeightDef = 17
        Me.chkExcel.Location = New System.Drawing.Point(99, 113)
        Me.chkExcel.Name = "chkExcel"
        Me.chkExcel.Size = New System.Drawing.Size(243, 17)
        Me.chkExcel.TabIndex = 265
        Me.chkExcel.TabStopSetting = True
        Me.chkExcel.Text = "Excelファイルでデータを作成する"
        Me.chkExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkExcel.TextValue = "Excelファイルでデータを作成する"
        Me.chkExcel.UseVisualStyleBackColor = True
        Me.chkExcel.WidthDef = 243
        '
        'chkMail
        '
        Me.chkMail.AutoSize = True
        Me.chkMail.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMail.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMail.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMail.EnableStatus = True
        Me.chkMail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMail.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMail.HeightDef = 17
        Me.chkMail.Location = New System.Drawing.Point(99, 132)
        Me.chkMail.Name = "chkMail"
        Me.chkMail.Size = New System.Drawing.Size(236, 17)
        Me.chkMail.TabIndex = 264
        Me.chkMail.TabStopSetting = True
        Me.chkMail.Text = "メール送信用にデータを作成する"
        Me.chkMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMail.TextValue = "メール送信用にデータを作成する"
        Me.chkMail.UseVisualStyleBackColor = True
        Me.chkMail.WidthDef = 236
        '
        'grpZaiko
        '
        Me.grpZaiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaiko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpZaiko.Controls.Add(Me.optZaizan)
        Me.grpZaiko.Controls.Add(Me.optZaiko)
        Me.grpZaiko.EnableStatus = False
        Me.grpZaiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaiko.HeightDef = 45
        Me.grpZaiko.Location = New System.Drawing.Point(416, 85)
        Me.grpZaiko.Name = "grpZaiko"
        Me.grpZaiko.Size = New System.Drawing.Size(352, 45)
        Me.grpZaiko.TabIndex = 262
        Me.grpZaiko.TabStop = False
        Me.grpZaiko.Text = "作成元在庫"
        Me.grpZaiko.TextValue = "作成元在庫"
        Me.grpZaiko.WidthDef = 352
        '
        'optZaizan
        '
        Me.optZaizan.AutoSize = True
        Me.optZaizan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optZaizan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optZaizan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optZaizan.Enabled = False
        Me.optZaizan.EnableStatus = True
        Me.optZaizan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optZaizan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optZaizan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optZaizan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optZaizan.HeightDef = 17
        Me.optZaizan.Location = New System.Drawing.Point(181, 20)
        Me.optZaizan.Name = "optZaizan"
        Me.optZaizan.Size = New System.Drawing.Size(151, 17)
        Me.optZaizan.TabIndex = 315
        Me.optZaizan.TabStop = True
        Me.optZaizan.TabStopSetting = True
        Me.optZaizan.Text = "月末在庫履歴データ"
        Me.optZaizan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optZaizan.TextValue = "月末在庫履歴データ"
        Me.optZaizan.UseVisualStyleBackColor = True
        Me.optZaizan.WidthDef = 151
        '
        'optZaiko
        '
        Me.optZaiko.AutoSize = True
        Me.optZaiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optZaiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optZaiko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optZaiko.Enabled = False
        Me.optZaiko.EnableStatus = True
        Me.optZaiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optZaiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optZaiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optZaiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optZaiko.HeightDef = 17
        Me.optZaiko.Location = New System.Drawing.Point(22, 20)
        Me.optZaiko.Name = "optZaiko"
        Me.optZaiko.Size = New System.Drawing.Size(137, 17)
        Me.optZaiko.TabIndex = 314
        Me.optZaiko.TabStop = True
        Me.optZaiko.TabStopSetting = True
        Me.optZaiko.Text = "現在の在庫データ"
        Me.optZaiko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optZaiko.TextValue = "現在の在庫データ"
        Me.optZaiko.UseVisualStyleBackColor = True
        Me.optZaiko.WidthDef = 137
        '
        'lblTitleDate
        '
        Me.lblTitleDate.AutoSize = True
        Me.lblTitleDate.AutoSizeDef = True
        Me.lblTitleDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDate.EnableStatus = False
        Me.lblTitleDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDate.HeightDef = 13
        Me.lblTitleDate.Location = New System.Drawing.Point(33, 72)
        Me.lblTitleDate.Name = "lblTitleDate"
        Me.lblTitleDate.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleDate.TabIndex = 253
        Me.lblTitleDate.Text = "実績日付"
        Me.lblTitleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDate.TextValue = "実績日付"
        Me.lblTitleDate.WidthDef = 63
        '
        'imdDate
        '
        Me.imdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdDate.HeightDef = 18
        Me.imdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdDate.HissuLabelVisible = True
        Me.imdDate.Holiday = True
        Me.imdDate.IsAfterDateCheck = False
        Me.imdDate.IsBeforeDateCheck = False
        Me.imdDate.IsHissuCheck = True
        Me.imdDate.IsMinDateCheck = "1900/01/01"
        Me.imdDate.ItemName = ""
        Me.imdDate.Location = New System.Drawing.Point(99, 69)
        Me.imdDate.Name = "imdDate"
        Me.imdDate.Number = CType(10101000000, Long)
        Me.imdDate.ReadOnly = False
        Me.imdDate.Size = New System.Drawing.Size(118, 18)
        Me.imdDate.TabIndex = 252
        Me.imdDate.TabStopSetting = True
        Me.imdDate.TextValue = ""
        Me.imdDate.Value = New Date(CType(0, Long))
        Me.imdDate.WidthDef = 118
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbEigyo.Location = New System.Drawing.Point(99, 25)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(311, 18)
        Me.cmbEigyo.TabIndex = 1
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 311
        '
        'lblTitleCust
        '
        Me.lblTitleCust.AutoSize = True
        Me.lblTitleCust.AutoSizeDef = True
        Me.lblTitleCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCust.EnableStatus = False
        Me.lblTitleCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.HeightDef = 13
        Me.lblTitleCust.Location = New System.Drawing.Point(61, 49)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 222
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(47, 28)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'LMI050F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI050F"
        Me.Text = "【LMI050】   EDI月末在庫実績送信ﾃﾞｰﾀ作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.grpZaiko.ResumeLayout(False)
        Me.grpZaiko.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents grpZaiko As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleOutput As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkEDI As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkExcel As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkMail As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents optZaizan As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optZaiko As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents cmbCust As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo

End Class
