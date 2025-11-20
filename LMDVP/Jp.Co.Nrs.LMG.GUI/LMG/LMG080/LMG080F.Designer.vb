<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMG080F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMG080F))
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.lblTitleBatch = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.chkShoriMi = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.cmbBatch = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkShoriZumi = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkShoriChu = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.imdInvDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.imdInvDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmGroupBox1 = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbseqflg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkTorikeshi = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LmGroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.LmGroupBox1)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        '
        'lblTitleBatch
        '
        Me.lblTitleBatch.AutoSize = True
        Me.lblTitleBatch.AutoSizeDef = True
        Me.lblTitleBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBatch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBatch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBatch.EnableStatus = False
        Me.lblTitleBatch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBatch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBatch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBatch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBatch.HeightDef = 13
        Me.lblTitleBatch.Location = New System.Drawing.Point(5, 22)
        Me.lblTitleBatch.Name = "lblTitleBatch"
        Me.lblTitleBatch.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleBatch.TabIndex = 1
        Me.lblTitleBatch.Text = "バッチ条件"
        Me.lblTitleBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBatch.TextValue = "バッチ条件"
        Me.lblTitleBatch.WidthDef = 77
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
        Me.sprDetail.HeightDef = 755
        Me.sprDetail.Location = New System.Drawing.Point(12, 108)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        Me.sprDetail.Size = New System.Drawing.Size(1257, 755)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Bottom
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1257
        '
        'chkShoriMi
        '
        Me.chkShoriMi.AutoSize = True
        Me.chkShoriMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriMi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriMi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkShoriMi.EnableStatus = True
        Me.chkShoriMi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriMi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriMi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriMi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriMi.HeightDef = 17
        Me.chkShoriMi.Location = New System.Drawing.Point(314, 22)
        Me.chkShoriMi.Name = "chkShoriMi"
        Me.chkShoriMi.Size = New System.Drawing.Size(40, 17)
        Me.chkShoriMi.TabIndex = 3
        Me.chkShoriMi.TabStopSetting = True
        Me.chkShoriMi.Text = "未"
        Me.chkShoriMi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkShoriMi.TextValue = "未"
        Me.chkShoriMi.UseVisualStyleBackColor = True
        Me.chkShoriMi.WidthDef = 40
        '
        'cmbBatch
        '
        Me.cmbBatch.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBatch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBatch.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBatch.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBatch.DataCode = "S070"
        Me.cmbBatch.DataSource = Nothing
        Me.cmbBatch.DisplayMember = Nothing
        Me.cmbBatch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBatch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBatch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBatch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBatch.HeightDef = 18
        Me.cmbBatch.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBatch.HissuLabelVisible = True
        Me.cmbBatch.InsertWildCard = True
        Me.cmbBatch.IsForbiddenWordsCheck = False
        Me.cmbBatch.IsHissuCheck = True
        Me.cmbBatch.ItemName = ""
        Me.cmbBatch.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBatch.Location = New System.Drawing.Point(89, 20)
        Me.cmbBatch.Name = "cmbBatch"
        Me.cmbBatch.ReadOnly = False
        Me.cmbBatch.SelectedIndex = -1
        Me.cmbBatch.SelectedItem = Nothing
        Me.cmbBatch.SelectedText = ""
        Me.cmbBatch.SelectedValue = ""
        Me.cmbBatch.Size = New System.Drawing.Size(127, 18)
        Me.cmbBatch.TabIndex = 27
        Me.cmbBatch.TabStopSetting = True
        Me.cmbBatch.TextValue = ""
        Me.cmbBatch.Value1 = Nothing
        Me.cmbBatch.Value2 = Nothing
        Me.cmbBatch.Value3 = Nothing
        Me.cmbBatch.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbBatch.ValueMember = Nothing
        Me.cmbBatch.WidthDef = 127
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(232, 22)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel2.TabIndex = 1
        Me.LmTitleLabel2.Text = "処理状況"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "処理状況"
        Me.LmTitleLabel2.WidthDef = 63
        '
        'chkShoriZumi
        '
        Me.chkShoriZumi.AutoSize = True
        Me.chkShoriZumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriZumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriZumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkShoriZumi.EnableStatus = True
        Me.chkShoriZumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriZumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriZumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriZumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriZumi.HeightDef = 17
        Me.chkShoriZumi.Location = New System.Drawing.Point(382, 22)
        Me.chkShoriZumi.Name = "chkShoriZumi"
        Me.chkShoriZumi.Size = New System.Drawing.Size(40, 17)
        Me.chkShoriZumi.TabIndex = 3
        Me.chkShoriZumi.TabStopSetting = True
        Me.chkShoriZumi.Text = "済"
        Me.chkShoriZumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkShoriZumi.TextValue = "済"
        Me.chkShoriZumi.UseVisualStyleBackColor = True
        Me.chkShoriZumi.WidthDef = 40
        '
        'chkShoriChu
        '
        Me.chkShoriChu.AutoSize = True
        Me.chkShoriChu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriChu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkShoriChu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkShoriChu.EnableStatus = True
        Me.chkShoriChu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriChu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkShoriChu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriChu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkShoriChu.HeightDef = 17
        Me.chkShoriChu.Location = New System.Drawing.Point(449, 22)
        Me.chkShoriChu.Name = "chkShoriChu"
        Me.chkShoriChu.Size = New System.Drawing.Size(68, 17)
        Me.chkShoriChu.TabIndex = 3
        Me.chkShoriChu.TabStopSetting = True
        Me.chkShoriChu.Text = "処理中"
        Me.chkShoriChu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkShoriChu.TextValue = "処理中"
        Me.chkShoriChu.UseVisualStyleBackColor = True
        Me.chkShoriChu.WidthDef = 68
        '
        'imdInvDateFrom
        '
        Me.imdInvDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInvDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdInvDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInvDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInvDateFrom.HeightDef = 18
        Me.imdInvDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInvDateFrom.HissuLabelVisible = False
        Me.imdInvDateFrom.Holiday = True
        Me.imdInvDateFrom.IsAfterDateCheck = False
        Me.imdInvDateFrom.IsBeforeDateCheck = False
        Me.imdInvDateFrom.IsHissuCheck = False
        Me.imdInvDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdInvDateFrom.ItemName = ""
        Me.imdInvDateFrom.Location = New System.Drawing.Point(287, 47)
        Me.imdInvDateFrom.Name = "imdInvDateFrom"
        Me.imdInvDateFrom.Number = CType(0, Long)
        Me.imdInvDateFrom.ReadOnly = False
        Me.imdInvDateFrom.Size = New System.Drawing.Size(128, 18)
        Me.imdInvDateFrom.TabIndex = 131
        Me.imdInvDateFrom.TabStopSetting = True
        Me.imdInvDateFrom.TextValue = ""
        Me.imdInvDateFrom.Value = New Date(CType(0, Long))
        Me.imdInvDateFrom.WidthDef = 128
        '
        'imdInvDateTo
        '
        Me.imdInvDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInvDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdInvDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInvDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInvDateTo.HeightDef = 18
        Me.imdInvDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInvDateTo.HissuLabelVisible = False
        Me.imdInvDateTo.Holiday = True
        Me.imdInvDateTo.IsAfterDateCheck = False
        Me.imdInvDateTo.IsBeforeDateCheck = False
        Me.imdInvDateTo.IsHissuCheck = False
        Me.imdInvDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdInvDateTo.ItemName = ""
        Me.imdInvDateTo.Location = New System.Drawing.Point(458, 47)
        Me.imdInvDateTo.Name = "imdInvDateTo"
        Me.imdInvDateTo.Number = CType(0, Long)
        Me.imdInvDateTo.ReadOnly = False
        Me.imdInvDateTo.Size = New System.Drawing.Size(128, 18)
        Me.imdInvDateTo.TabIndex = 131
        Me.imdInvDateTo.TabStopSetting = True
        Me.imdInvDateTo.TextValue = ""
        Me.imdInvDateTo.Value = New Date(CType(0, Long))
        Me.imdInvDateTo.WidthDef = 128
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(232, 49)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 1
        Me.LmTitleLabel3.Text = "実行日"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "実行日"
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
        Me.LmTitleLabel4.Location = New System.Drawing.Point(421, 49)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel4.TabIndex = 133
        Me.LmTitleLabel4.Text = "～"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "～"
        Me.LmTitleLabel4.WidthDef = 21
        '
        'LmGroupBox1
        '
        Me.LmGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmGroupBox1.Controls.Add(Me.cmbseqflg)
        Me.LmGroupBox1.Controls.Add(Me.cmbBatch)
        Me.LmGroupBox1.Controls.Add(Me.LmTitleLabel4)
        Me.LmGroupBox1.Controls.Add(Me.LmTitleLabel1)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleBatch)
        Me.LmGroupBox1.Controls.Add(Me.LmTitleLabel2)
        Me.LmGroupBox1.Controls.Add(Me.imdInvDateTo)
        Me.LmGroupBox1.Controls.Add(Me.imdInvDateFrom)
        Me.LmGroupBox1.Controls.Add(Me.chkShoriMi)
        Me.LmGroupBox1.Controls.Add(Me.chkShoriZumi)
        Me.LmGroupBox1.Controls.Add(Me.LmTitleLabel3)
        Me.LmGroupBox1.Controls.Add(Me.chkTorikeshi)
        Me.LmGroupBox1.Controls.Add(Me.chkShoriChu)
        Me.LmGroupBox1.EnableStatus = False
        Me.LmGroupBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox1.HeightDef = 79
        Me.LmGroupBox1.Location = New System.Drawing.Point(25, 23)
        Me.LmGroupBox1.Name = "LmGroupBox1"
        Me.LmGroupBox1.Size = New System.Drawing.Size(598, 79)
        Me.LmGroupBox1.TabIndex = 134
        Me.LmGroupBox1.TabStop = False
        Me.LmGroupBox1.Text = "検索条件"
        Me.LmGroupBox1.TextValue = "検索条件"
        Me.LmGroupBox1.WidthDef = 598
        '
        'cmbseqflg
        '
        Me.cmbseqflg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbseqflg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbseqflg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbseqflg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbseqflg.DataCode = "H013"
        Me.cmbseqflg.DataSource = Nothing
        Me.cmbseqflg.DisplayMember = Nothing
        Me.cmbseqflg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbseqflg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbseqflg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbseqflg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbseqflg.HeightDef = 18
        Me.cmbseqflg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbseqflg.HissuLabelVisible = False
        Me.cmbseqflg.InsertWildCard = True
        Me.cmbseqflg.IsForbiddenWordsCheck = False
        Me.cmbseqflg.IsHissuCheck = False
        Me.cmbseqflg.ItemName = ""
        Me.cmbseqflg.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbseqflg.Location = New System.Drawing.Point(89, 46)
        Me.cmbseqflg.Name = "cmbseqflg"
        Me.cmbseqflg.ReadOnly = False
        Me.cmbseqflg.SelectedIndex = -1
        Me.cmbseqflg.SelectedItem = Nothing
        Me.cmbseqflg.SelectedText = ""
        Me.cmbseqflg.SelectedValue = ""
        Me.cmbseqflg.Size = New System.Drawing.Size(127, 18)
        Me.cmbseqflg.TabIndex = 27
        Me.cmbseqflg.TabStopSetting = True
        Me.cmbseqflg.TextValue = ""
        Me.cmbseqflg.Value1 = Nothing
        Me.cmbseqflg.Value2 = Nothing
        Me.cmbseqflg.Value3 = Nothing
        Me.cmbseqflg.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbseqflg.ValueMember = Nothing
        Me.cmbseqflg.WidthDef = 127
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(6, 49)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel1.TabIndex = 1
        Me.LmTitleLabel1.Text = "実行モード"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "実行モード"
        Me.LmTitleLabel1.WidthDef = 77
        '
        'chkTorikeshi
        '
        Me.chkTorikeshi.AutoSize = True
        Me.chkTorikeshi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTorikeshi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTorikeshi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkTorikeshi.EnableStatus = True
        Me.chkTorikeshi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTorikeshi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTorikeshi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTorikeshi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTorikeshi.HeightDef = 17
        Me.chkTorikeshi.Location = New System.Drawing.Point(523, 21)
        Me.chkTorikeshi.Name = "chkTorikeshi"
        Me.chkTorikeshi.Size = New System.Drawing.Size(54, 17)
        Me.chkTorikeshi.TabIndex = 3
        Me.chkTorikeshi.TabStopSetting = True
        Me.chkTorikeshi.Text = "取消"
        Me.chkTorikeshi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTorikeshi.TextValue = "取消"
        Me.chkTorikeshi.UseVisualStyleBackColor = True
        Me.chkTorikeshi.WidthDef = 54
        '
        'LMG080F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMG080F"
        Me.Text = "【LMG080】 状況詳細"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LmGroupBox1.ResumeLayout(False)
        Me.LmGroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleBatch As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents chkShoriMi As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents cmbBatch As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkShoriChu As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkShoriZumi As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents imdInvDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdInvDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmGroupBox1 As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkTorikeshi As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents cmbseqflg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
