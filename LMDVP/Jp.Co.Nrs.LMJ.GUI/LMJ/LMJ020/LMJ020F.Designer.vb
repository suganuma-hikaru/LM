<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMJ020F
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
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.lblTitleShubetsu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbShori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.imdLastUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.imdLastUpdDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.lblTitleShubetsu)
        Me.pnlViewAria.Controls.Add(Me.cmbShori)
        Me.pnlViewAria.Size = New System.Drawing.Size(794, 488)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "　"
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F7ButtonName = "実　行"
        Me.FunctionKey.F8ButtonName = "　"
        Me.FunctionKey.F9ButtonName = " 検索"
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'lblTitleShubetsu
        '
        Me.lblTitleShubetsu.AutoSize = True
        Me.lblTitleShubetsu.AutoSizeDef = True
        Me.lblTitleShubetsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShubetsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShubetsu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShubetsu.EnableStatus = False
        Me.lblTitleShubetsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShubetsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShubetsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShubetsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShubetsu.HeightDef = 13
        Me.lblTitleShubetsu.Location = New System.Drawing.Point(50, 21)
        Me.lblTitleShubetsu.Name = "lblTitleShubetsu"
        Me.lblTitleShubetsu.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleShubetsu.TabIndex = 241
        Me.lblTitleShubetsu.Text = "処理内容"
        Me.lblTitleShubetsu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShubetsu.TextValue = "処理内容"
        Me.lblTitleShubetsu.WidthDef = 63
        '
        'cmbShori
        '
        Me.cmbShori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShori.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShori.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShori.DataCode = "J013"
        Me.cmbShori.DataSource = Nothing
        Me.cmbShori.DisplayMember = Nothing
        Me.cmbShori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShori.HeightDef = 18
        Me.cmbShori.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShori.HissuLabelVisible = True
        Me.cmbShori.InsertWildCard = True
        Me.cmbShori.IsForbiddenWordsCheck = False
        Me.cmbShori.IsHissuCheck = True
        Me.cmbShori.ItemName = ""
        Me.cmbShori.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShori.Location = New System.Drawing.Point(119, 16)
        Me.cmbShori.Name = "cmbShori"
        Me.cmbShori.ReadOnly = False
        Me.cmbShori.SelectedIndex = -1
        Me.cmbShori.SelectedItem = Nothing
        Me.cmbShori.SelectedText = ""
        Me.cmbShori.SelectedValue = ""
        Me.cmbShori.Size = New System.Drawing.Size(182, 18)
        Me.cmbShori.TabIndex = 242
        Me.cmbShori.TabStopSetting = True
        Me.cmbShori.TextValue = ""
        Me.cmbShori.Value1 = Nothing
        Me.cmbShori.Value2 = Nothing
        Me.cmbShori.Value3 = Nothing
        Me.cmbShori.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbShori.ValueMember = Nothing
        Me.cmbShori.WidthDef = 182
        '
        'imdLastUpdDate
        '
        Me.imdLastUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdLastUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdLastUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdLastUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdLastUpdDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdLastUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdLastUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdLastUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdLastUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdLastUpdDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdLastUpdDate.HeightDef = 18
        Me.imdLastUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdLastUpdDate.HissuLabelVisible = False
        Me.imdLastUpdDate.Holiday = True
        Me.imdLastUpdDate.IsAfterDateCheck = False
        Me.imdLastUpdDate.IsBeforeDateCheck = False
        Me.imdLastUpdDate.IsHissuCheck = False
        Me.imdLastUpdDate.IsMinDateCheck = "1900/01/01"
        Me.imdLastUpdDate.ItemName = ""
        Me.imdLastUpdDate.Location = New System.Drawing.Point(119, 66)
        Me.imdLastUpdDate.Name = "imdLastUpdDate"
        Me.imdLastUpdDate.Number = CType(0, Long)
        Me.imdLastUpdDate.ReadOnly = False
        Me.imdLastUpdDate.Size = New System.Drawing.Size(118, 18)
        Me.imdLastUpdDate.TabIndex = 246
        Me.imdLastUpdDate.TabStopSetting = True
        Me.imdLastUpdDate.TextValue = ""
        Me.imdLastUpdDate.Value = New Date(CType(0, Long))
        Me.imdLastUpdDate.WidthDef = 118
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSizeDef = False
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 18
        Me.LmTitleLabel4.Location = New System.Drawing.Point(12, 66)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(101, 18)
        Me.LmTitleLabel4.TabIndex = 245
        Me.LmTitleLabel4.Text = "最終更新日"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "最終更新日"
        Me.LmTitleLabel4.WidthDef = 101
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
        Me.sprDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail.HeightDef = 392
        Me.sprDetail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(12, 90)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(770, 392)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 248
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 770
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(64, 45)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 249
        Me.LmTitleLabel1.Text = "営業所"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "営業所"
        Me.LmTitleLabel1.WidthDef = 49
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
        Me.cmbEigyo.Location = New System.Drawing.Point(119, 40)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(294, 18)
        Me.cmbEigyo.TabIndex = 250
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 294
        '
        'LMJ020F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMJ020F"
        Me.Text = "【LMJ020F】   請求在庫・実在庫差異分リスト作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleShubetsu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbShori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents imdLastUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr

End Class
