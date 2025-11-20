<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI320F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMI320F))
        Me.pnlSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbNrsBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdSeiqDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.grpPrint = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitlePrint = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPrintShubetu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.grpMake = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnMake = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleMake = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbMake = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.pnlViewAria.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.grpPrint.SuspendLayout()
        Me.grpMake.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.grpMake)
        Me.pnlViewAria.Controls.Add(Me.grpPrint)
        Me.pnlViewAria.Controls.Add(Me.pnlSearch)
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSearch.Controls.Add(Me.cmbNrsBr)
        Me.pnlSearch.Controls.Add(Me.lblTitleEigyo)
        Me.pnlSearch.Controls.Add(Me.lblTitleDate)
        Me.pnlSearch.Controls.Add(Me.imdSeiqDate)
        Me.pnlSearch.EnableStatus = False
        Me.pnlSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.HeightDef = 74
        Me.pnlSearch.Location = New System.Drawing.Point(3, 19)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(1259, 74)
        Me.pnlSearch.TabIndex = 238
        Me.pnlSearch.TabStop = False
        Me.pnlSearch.Text = "条件"
        Me.pnlSearch.TextValue = "条件"
        Me.pnlSearch.WidthDef = 1259
        '
        'cmbNrsBr
        '
        Me.cmbNrsBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsBr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsBr.DataSource = Nothing
        Me.cmbNrsBr.DisplayMember = Nothing
        Me.cmbNrsBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBr.HeightDef = 18
        Me.cmbNrsBr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBr.HissuLabelVisible = True
        Me.cmbNrsBr.InsertWildCard = True
        Me.cmbNrsBr.IsForbiddenWordsCheck = False
        Me.cmbNrsBr.IsHissuCheck = True
        Me.cmbNrsBr.ItemName = ""
        Me.cmbNrsBr.Location = New System.Drawing.Point(99, 19)
        Me.cmbNrsBr.Name = "cmbNrsBr"
        Me.cmbNrsBr.ReadOnly = True
        Me.cmbNrsBr.SelectedIndex = -1
        Me.cmbNrsBr.SelectedItem = Nothing
        Me.cmbNrsBr.SelectedText = ""
        Me.cmbNrsBr.SelectedValue = ""
        Me.cmbNrsBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBr.TabIndex = 256
        Me.cmbNrsBr.TabStop = False
        Me.cmbNrsBr.TabStopSetting = False
        Me.cmbNrsBr.TextValue = ""
        Me.cmbNrsBr.ValueMember = Nothing
        Me.cmbNrsBr.WidthDef = 300
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(46, 22)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 257
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.lblTitleDate.Location = New System.Drawing.Point(46, 47)
        Me.lblTitleDate.Name = "lblTitleDate"
        Me.lblTitleDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleDate.TabIndex = 253
        Me.lblTitleDate.Text = "請求日"
        Me.lblTitleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDate.TextValue = "請求日"
        Me.lblTitleDate.WidthDef = 49
        '
        'imdSeiqDate
        '
        Me.imdSeiqDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeiqDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeiqDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSeiqDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSeiqDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSeiqDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeiqDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeiqDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeiqDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeiqDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSeiqDate.HeightDef = 18
        Me.imdSeiqDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSeiqDate.HissuLabelVisible = True
        Me.imdSeiqDate.Holiday = True
        Me.imdSeiqDate.IsAfterDateCheck = False
        Me.imdSeiqDate.IsBeforeDateCheck = False
        Me.imdSeiqDate.IsHissuCheck = True
        Me.imdSeiqDate.IsMinDateCheck = "1900/01/01"
        Me.imdSeiqDate.ItemName = ""
        Me.imdSeiqDate.Location = New System.Drawing.Point(99, 44)
        Me.imdSeiqDate.Name = "imdSeiqDate"
        Me.imdSeiqDate.Number = CType(10101000000, Long)
        Me.imdSeiqDate.ReadOnly = False
        Me.imdSeiqDate.Size = New System.Drawing.Size(118, 18)
        Me.imdSeiqDate.TabIndex = 252
        Me.imdSeiqDate.TabStopSetting = True
        Me.imdSeiqDate.TextValue = ""
        Me.imdSeiqDate.Value = New Date(CType(0, Long))
        Me.imdSeiqDate.WidthDef = 118
        '
        'grpPrint
        '
        Me.grpPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpPrint.Controls.Add(Me.btnPrint)
        Me.grpPrint.Controls.Add(Me.lblTitlePrint)
        Me.grpPrint.Controls.Add(Me.cmbPrintShubetu)
        Me.grpPrint.EnableStatus = False
        Me.grpPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpPrint.HeightDef = 49
        Me.grpPrint.Location = New System.Drawing.Point(422, 101)
        Me.grpPrint.Name = "grpPrint"
        Me.grpPrint.Size = New System.Drawing.Size(468, 49)
        Me.grpPrint.TabIndex = 261
        Me.grpPrint.TabStop = False
        Me.grpPrint.Text = "印刷条件"
        Me.grpPrint.TextValue = "印刷条件"
        Me.grpPrint.WidthDef = 468
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnPrint.EnableStatus = True
        Me.btnPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.HeightDef = 22
        Me.btnPrint.Location = New System.Drawing.Point(382, 17)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 258
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
        '
        'lblTitlePrint
        '
        Me.lblTitlePrint.AutoSize = True
        Me.lblTitlePrint.AutoSizeDef = True
        Me.lblTitlePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePrint.EnableStatus = False
        Me.lblTitlePrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrint.HeightDef = 13
        Me.lblTitlePrint.Location = New System.Drawing.Point(17, 24)
        Me.lblTitlePrint.Name = "lblTitlePrint"
        Me.lblTitlePrint.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePrint.TabIndex = 251
        Me.lblTitlePrint.Text = "印刷種別"
        Me.lblTitlePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrint.TextValue = "印刷種別"
        Me.lblTitlePrint.WidthDef = 63
        '
        'cmbPrintShubetu
        '
        Me.cmbPrintShubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrintShubetu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrintShubetu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrintShubetu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrintShubetu.DataCode = "P022"
        Me.cmbPrintShubetu.DataSource = Nothing
        Me.cmbPrintShubetu.DisplayMember = Nothing
        Me.cmbPrintShubetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrintShubetu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrintShubetu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrintShubetu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrintShubetu.HeightDef = 18
        Me.cmbPrintShubetu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrintShubetu.HissuLabelVisible = True
        Me.cmbPrintShubetu.InsertWildCard = True
        Me.cmbPrintShubetu.IsForbiddenWordsCheck = False
        Me.cmbPrintShubetu.IsHissuCheck = True
        Me.cmbPrintShubetu.ItemName = ""
        Me.cmbPrintShubetu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrintShubetu.Location = New System.Drawing.Point(86, 19)
        Me.cmbPrintShubetu.Name = "cmbPrintShubetu"
        Me.cmbPrintShubetu.ReadOnly = False
        Me.cmbPrintShubetu.SelectedIndex = -1
        Me.cmbPrintShubetu.SelectedItem = Nothing
        Me.cmbPrintShubetu.SelectedText = ""
        Me.cmbPrintShubetu.SelectedValue = ""
        Me.cmbPrintShubetu.Size = New System.Drawing.Size(290, 18)
        Me.cmbPrintShubetu.TabIndex = 252
        Me.cmbPrintShubetu.TabStopSetting = True
        Me.cmbPrintShubetu.TextValue = ""
        Me.cmbPrintShubetu.Value1 = Nothing
        Me.cmbPrintShubetu.Value2 = Nothing
        Me.cmbPrintShubetu.Value3 = Nothing
        Me.cmbPrintShubetu.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrintShubetu.ValueMember = Nothing
        Me.cmbPrintShubetu.WidthDef = 290
        '
        'grpMake
        '
        Me.grpMake.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMake.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMake.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMake.Controls.Add(Me.btnMake)
        Me.grpMake.Controls.Add(Me.lblTitleMake)
        Me.grpMake.Controls.Add(Me.cmbMake)
        Me.grpMake.EnableStatus = False
        Me.grpMake.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMake.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMake.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMake.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMake.HeightDef = 49
        Me.grpMake.Location = New System.Drawing.Point(3, 101)
        Me.grpMake.Name = "grpMake"
        Me.grpMake.Size = New System.Drawing.Size(396, 49)
        Me.grpMake.TabIndex = 260
        Me.grpMake.TabStop = False
        Me.grpMake.Text = "作成条件"
        Me.grpMake.TextValue = "作成条件"
        Me.grpMake.WidthDef = 396
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
        Me.btnMake.Location = New System.Drawing.Point(309, 17)
        Me.btnMake.Name = "btnMake"
        Me.btnMake.Size = New System.Drawing.Size(70, 22)
        Me.btnMake.TabIndex = 258
        Me.btnMake.TabStopSetting = True
        Me.btnMake.Text = "作成"
        Me.btnMake.TextValue = "作成"
        Me.btnMake.UseVisualStyleBackColor = True
        Me.btnMake.WidthDef = 70
        '
        'lblTitleMake
        '
        Me.lblTitleMake.AutoSize = True
        Me.lblTitleMake.AutoSizeDef = True
        Me.lblTitleMake.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMake.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMake.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMake.EnableStatus = False
        Me.lblTitleMake.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMake.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMake.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMake.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMake.HeightDef = 13
        Me.lblTitleMake.Location = New System.Drawing.Point(8, 22)
        Me.lblTitleMake.Name = "lblTitleMake"
        Me.lblTitleMake.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleMake.TabIndex = 251
        Me.lblTitleMake.Text = "作成種別"
        Me.lblTitleMake.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMake.TextValue = "作成種別"
        Me.lblTitleMake.WidthDef = 63
        '
        'cmbMake
        '
        Me.cmbMake.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMake.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMake.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbMake.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbMake.DataCode = "D019"
        Me.cmbMake.DataSource = Nothing
        Me.cmbMake.DisplayMember = Nothing
        Me.cmbMake.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMake.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMake.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMake.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMake.HeightDef = 18
        Me.cmbMake.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbMake.HissuLabelVisible = True
        Me.cmbMake.InsertWildCard = True
        Me.cmbMake.IsForbiddenWordsCheck = False
        Me.cmbMake.IsHissuCheck = True
        Me.cmbMake.ItemName = ""
        Me.cmbMake.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbMake.Location = New System.Drawing.Point(77, 19)
        Me.cmbMake.Name = "cmbMake"
        Me.cmbMake.ReadOnly = False
        Me.cmbMake.SelectedIndex = -1
        Me.cmbMake.SelectedItem = Nothing
        Me.cmbMake.SelectedText = ""
        Me.cmbMake.SelectedValue = ""
        Me.cmbMake.Size = New System.Drawing.Size(226, 18)
        Me.cmbMake.TabIndex = 252
        Me.cmbMake.TabStopSetting = True
        Me.cmbMake.TextValue = ""
        Me.cmbMake.Value1 = Nothing
        Me.cmbMake.Value2 = Nothing
        Me.cmbMake.Value3 = Nothing
        Me.cmbMake.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbMake.ValueMember = Nothing
        Me.cmbMake.WidthDef = 226
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
        Me.sprDetail.HeightDef = 684
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(12, 173)
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
        Me.sprDetail.Size = New System.Drawing.Size(1248, 684)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 262
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1248
        '
        'LMI320F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI320F"
        Me.Text = "【LMI320】   請求明細・鑑作成"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.grpPrint.ResumeLayout(False)
        Me.grpPrint.PerformLayout()
        Me.grpMake.ResumeLayout(False)
        Me.grpMake.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents grpPrint As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitlePrint As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPrintShubetu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSeiqDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents grpMake As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnMake As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleMake As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbMake As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbNrsBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch

End Class
