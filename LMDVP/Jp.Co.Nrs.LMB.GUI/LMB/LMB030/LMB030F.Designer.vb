<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMB030F
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
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField4 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField4 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.grpSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleDataInsDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdDataInsDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.chkFurikae = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.lblTitleNyukaDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdNyukaDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblCustNM_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNM_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.pnlViewAria.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.grpSearch)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "マスタ参照"
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F7ButtonName = "印　刷"
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSearch.Controls.Add(Me.cmbEigyo)
        Me.grpSearch.Controls.Add(Me.lblTitleDataInsDate)
        Me.grpSearch.Controls.Add(Me.imdDataInsDate)
        Me.grpSearch.Controls.Add(Me.chkFurikae)
        Me.grpSearch.Controls.Add(Me.lblTitleNyukaDate)
        Me.grpSearch.Controls.Add(Me.imdNyukaDate)
        Me.grpSearch.Controls.Add(Me.lblCustNM_M)
        Me.grpSearch.Controls.Add(Me.lblCustNM_L)
        Me.grpSearch.Controls.Add(Me.LmTitleLabel6)
        Me.grpSearch.Controls.Add(Me.txtCustCD_M)
        Me.grpSearch.Controls.Add(Me.txtCustCD_L)
        Me.grpSearch.Controls.Add(Me.LmTitleLabel2)
        Me.grpSearch.EnableStatus = False
        Me.grpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.HeightDef = 118
        Me.grpSearch.Location = New System.Drawing.Point(27, 51)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(738, 118)
        Me.grpSearch.TabIndex = 238
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "出力条件"
        Me.grpSearch.TextValue = "出力条件"
        Me.grpSearch.WidthDef = 738
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
        Me.cmbEigyo.Location = New System.Drawing.Point(99, 25)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 230
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'lblTitleDataInsDate
        '
        Me.lblTitleDataInsDate.AutoSize = True
        Me.lblTitleDataInsDate.AutoSizeDef = True
        Me.lblTitleDataInsDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDataInsDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDataInsDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDataInsDate.EnableStatus = False
        Me.lblTitleDataInsDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDataInsDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDataInsDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDataInsDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDataInsDate.HeightDef = 13
        Me.lblTitleDataInsDate.Location = New System.Drawing.Point(355, 91)
        Me.lblTitleDataInsDate.Name = "lblTitleDataInsDate"
        Me.lblTitleDataInsDate.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleDataInsDate.TabIndex = 229
        Me.lblTitleDataInsDate.Text = "データ登録日"
        Me.lblTitleDataInsDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDataInsDate.TextValue = "データ登録日"
        Me.lblTitleDataInsDate.WidthDef = 91
        '
        'imdDataInsDate
        '
        Me.imdDataInsDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDataInsDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdDataInsDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdDataInsDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdDataInsDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdDataInsDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDataInsDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdDataInsDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDataInsDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdDataInsDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdDataInsDate.HeightDef = 18
        Me.imdDataInsDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdDataInsDate.HissuLabelVisible = False
        Me.imdDataInsDate.Holiday = True
        Me.imdDataInsDate.IsAfterDateCheck = False
        Me.imdDataInsDate.IsBeforeDateCheck = False
        Me.imdDataInsDate.IsHissuCheck = False
        Me.imdDataInsDate.IsMinDateCheck = "1900/01/01"
        Me.imdDataInsDate.ItemName = ""
        Me.imdDataInsDate.Location = New System.Drawing.Point(452, 88)
        Me.imdDataInsDate.Name = "imdDataInsDate"
        Me.imdDataInsDate.Number = CType(0, Long)
        Me.imdDataInsDate.ReadOnly = False
        Me.imdDataInsDate.Size = New System.Drawing.Size(118, 18)
        Me.imdDataInsDate.TabIndex = 228
        Me.imdDataInsDate.TabStopSetting = True
        Me.imdDataInsDate.TextValue = ""
        Me.imdDataInsDate.Value = New Date(CType(0, Long))
        Me.imdDataInsDate.WidthDef = 118
        '
        'chkFurikae
        '
        Me.chkFurikae.AutoSize = True
        Me.chkFurikae.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkFurikae.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkFurikae.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkFurikae.EnableStatus = True
        Me.chkFurikae.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFurikae.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFurikae.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFurikae.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFurikae.HeightDef = 17
        Me.chkFurikae.Location = New System.Drawing.Point(246, 90)
        Me.chkFurikae.Name = "chkFurikae"
        Me.chkFurikae.Size = New System.Drawing.Size(82, 17)
        Me.chkFurikae.TabIndex = 227
        Me.chkFurikae.TabStopSetting = True
        Me.chkFurikae.Text = "振替含む"
        Me.chkFurikae.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkFurikae.TextValue = "振替含む"
        Me.chkFurikae.UseVisualStyleBackColor = True
        Me.chkFurikae.WidthDef = 82
        '
        'lblTitleNyukaDate
        '
        Me.lblTitleNyukaDate.AutoSize = True
        Me.lblTitleNyukaDate.AutoSizeDef = True
        Me.lblTitleNyukaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyukaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyukaDate.EnableStatus = False
        Me.lblTitleNyukaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyukaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyukaDate.HeightDef = 13
        Me.lblTitleNyukaDate.Location = New System.Drawing.Point(44, 91)
        Me.lblTitleNyukaDate.Name = "lblTitleNyukaDate"
        Me.lblTitleNyukaDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNyukaDate.TabIndex = 226
        Me.lblTitleNyukaDate.Text = "入荷日"
        Me.lblTitleNyukaDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyukaDate.TextValue = "入荷日"
        Me.lblTitleNyukaDate.WidthDef = 49
        '
        'imdNyukaDate
        '
        Me.imdNyukaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField7.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = "/"
        DateDayDisplayField4.ShowLeadingZero = True
        Me.imdNyukaDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField7, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField8, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField4, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate.HeightDef = 18
        Me.imdNyukaDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate.HissuLabelVisible = True
        Me.imdNyukaDate.Holiday = True
        Me.imdNyukaDate.IsAfterDateCheck = False
        Me.imdNyukaDate.IsBeforeDateCheck = False
        Me.imdNyukaDate.IsHissuCheck = True
        Me.imdNyukaDate.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate.ItemName = ""
        Me.imdNyukaDate.Location = New System.Drawing.Point(99, 88)
        Me.imdNyukaDate.Name = "imdNyukaDate"
        Me.imdNyukaDate.Number = CType(0, Long)
        Me.imdNyukaDate.ReadOnly = False
        Me.imdNyukaDate.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate.TabIndex = 225
        Me.imdNyukaDate.TabStopSetting = True
        Me.imdNyukaDate.TextValue = ""
        Me.imdNyukaDate.Value = New Date(CType(0, Long))
        Me.imdNyukaDate.WidthDef = 118
        '
        'lblCustNM_M
        '
        Me.lblCustNM_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_M.CountWrappedLine = False
        Me.lblCustNM_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_M.HeightDef = 18
        Me.lblCustNM_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.HissuLabelVisible = True
        Me.lblCustNM_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_M.IsByteCheck = 0
        Me.lblCustNM_M.IsCalendarCheck = False
        Me.lblCustNM_M.IsDakutenCheck = False
        Me.lblCustNM_M.IsEisuCheck = False
        Me.lblCustNM_M.IsForbiddenWordsCheck = False
        Me.lblCustNM_M.IsFullByteCheck = 0
        Me.lblCustNM_M.IsHankakuCheck = False
        Me.lblCustNM_M.IsHissuCheck = True
        Me.lblCustNM_M.IsKanaCheck = False
        Me.lblCustNM_M.IsMiddleSpace = False
        Me.lblCustNM_M.IsNumericCheck = False
        Me.lblCustNM_M.IsSujiCheck = False
        Me.lblCustNM_M.IsZenkakuCheck = False
        Me.lblCustNM_M.ItemName = ""
        Me.lblCustNM_M.LineSpace = 0
        Me.lblCustNM_M.Location = New System.Drawing.Point(180, 67)
        Me.lblCustNM_M.MaxLength = 0
        Me.lblCustNM_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_M.MaxLineCount = 0
        Me.lblCustNM_M.Multiline = False
        Me.lblCustNM_M.Name = "lblCustNM_M"
        Me.lblCustNM_M.ReadOnly = True
        Me.lblCustNM_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_M.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNM_M.TabIndex = 224
        Me.lblCustNM_M.TabStop = False
        Me.lblCustNM_M.TabStopSetting = False
        Me.lblCustNM_M.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_M.UseSystemPasswordChar = False
        Me.lblCustNM_M.WidthDef = 473
        Me.lblCustNM_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNM_L
        '
        Me.lblCustNM_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_L.CountWrappedLine = False
        Me.lblCustNM_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_L.HeightDef = 18
        Me.lblCustNM_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.HissuLabelVisible = True
        Me.lblCustNM_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_L.IsByteCheck = 0
        Me.lblCustNM_L.IsCalendarCheck = False
        Me.lblCustNM_L.IsDakutenCheck = False
        Me.lblCustNM_L.IsEisuCheck = False
        Me.lblCustNM_L.IsForbiddenWordsCheck = False
        Me.lblCustNM_L.IsFullByteCheck = 0
        Me.lblCustNM_L.IsHankakuCheck = False
        Me.lblCustNM_L.IsHissuCheck = True
        Me.lblCustNM_L.IsKanaCheck = False
        Me.lblCustNM_L.IsMiddleSpace = False
        Me.lblCustNM_L.IsNumericCheck = False
        Me.lblCustNM_L.IsSujiCheck = False
        Me.lblCustNM_L.IsZenkakuCheck = False
        Me.lblCustNM_L.ItemName = ""
        Me.lblCustNM_L.LineSpace = 0
        Me.lblCustNM_L.Location = New System.Drawing.Point(180, 46)
        Me.lblCustNM_L.MaxLength = 0
        Me.lblCustNM_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_L.MaxLineCount = 0
        Me.lblCustNM_L.Multiline = False
        Me.lblCustNM_L.Name = "lblCustNM_L"
        Me.lblCustNM_L.ReadOnly = True
        Me.lblCustNM_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_L.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNM_L.TabIndex = 223
        Me.lblCustNM_L.TabStop = False
        Me.lblCustNM_L.TabStopSetting = False
        Me.lblCustNM_L.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_L.UseSystemPasswordChar = False
        Me.lblCustNM_L.WidthDef = 473
        Me.lblCustNM_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel6
        '
        Me.LmTitleLabel6.AutoSize = True
        Me.LmTitleLabel6.AutoSizeDef = True
        Me.LmTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel6.EnableStatus = False
        Me.LmTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.HeightDef = 13
        Me.LmTitleLabel6.Location = New System.Drawing.Point(58, 49)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel6.TabIndex = 222
        Me.LmTitleLabel6.Text = "荷主"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "荷主"
        Me.LmTitleLabel6.WidthDef = 35
        '
        'txtCustCD_M
        '
        Me.txtCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_M.CountWrappedLine = False
        Me.txtCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_M.HeightDef = 18
        Me.txtCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_M.HissuLabelVisible = False
        Me.txtCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD_M.IsByteCheck = 2
        Me.txtCustCD_M.IsCalendarCheck = False
        Me.txtCustCD_M.IsDakutenCheck = False
        Me.txtCustCD_M.IsEisuCheck = False
        Me.txtCustCD_M.IsForbiddenWordsCheck = False
        Me.txtCustCD_M.IsFullByteCheck = 0
        Me.txtCustCD_M.IsHankakuCheck = False
        Me.txtCustCD_M.IsHissuCheck = False
        Me.txtCustCD_M.IsKanaCheck = False
        Me.txtCustCD_M.IsMiddleSpace = False
        Me.txtCustCD_M.IsNumericCheck = False
        Me.txtCustCD_M.IsSujiCheck = False
        Me.txtCustCD_M.IsZenkakuCheck = False
        Me.txtCustCD_M.ItemName = ""
        Me.txtCustCD_M.LineSpace = 0
        Me.txtCustCD_M.Location = New System.Drawing.Point(144, 67)
        Me.txtCustCD_M.MaxLength = 2
        Me.txtCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_M.MaxLineCount = 0
        Me.txtCustCD_M.Multiline = False
        Me.txtCustCD_M.Name = "txtCustCD_M"
        Me.txtCustCD_M.ReadOnly = False
        Me.txtCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_M.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCD_M.TabIndex = 221
        Me.txtCustCD_M.TabStopSetting = True
        Me.txtCustCD_M.TextValue = ""
        Me.txtCustCD_M.UseSystemPasswordChar = False
        Me.txtCustCD_M.WidthDef = 52
        Me.txtCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCD_L
        '
        Me.txtCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD_L.CountWrappedLine = False
        Me.txtCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD_L.HeightDef = 18
        Me.txtCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD_L.HissuLabelVisible = False
        Me.txtCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD_L.IsByteCheck = 5
        Me.txtCustCD_L.IsCalendarCheck = False
        Me.txtCustCD_L.IsDakutenCheck = False
        Me.txtCustCD_L.IsEisuCheck = False
        Me.txtCustCD_L.IsForbiddenWordsCheck = False
        Me.txtCustCD_L.IsFullByteCheck = 0
        Me.txtCustCD_L.IsHankakuCheck = False
        Me.txtCustCD_L.IsHissuCheck = False
        Me.txtCustCD_L.IsKanaCheck = False
        Me.txtCustCD_L.IsMiddleSpace = False
        Me.txtCustCD_L.IsNumericCheck = False
        Me.txtCustCD_L.IsSujiCheck = False
        Me.txtCustCD_L.IsZenkakuCheck = False
        Me.txtCustCD_L.ItemName = ""
        Me.txtCustCD_L.LineSpace = 0
        Me.txtCustCD_L.Location = New System.Drawing.Point(99, 46)
        Me.txtCustCD_L.MaxLength = 5
        Me.txtCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD_L.MaxLineCount = 0
        Me.txtCustCD_L.Multiline = False
        Me.txtCustCD_L.Name = "txtCustCD_L"
        Me.txtCustCD_L.ReadOnly = False
        Me.txtCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD_L.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCD_L.TabIndex = 220
        Me.txtCustCD_L.TabStopSetting = True
        Me.txtCustCD_L.TextValue = ""
        Me.txtCustCD_L.UseSystemPasswordChar = False
        Me.txtCustCD_L.WidthDef = 97
        Me.txtCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(44, 28)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel2.TabIndex = 219
        Me.LmTitleLabel2.Text = "営業所"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "営業所"
        Me.LmTitleLabel2.WidthDef = 49
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(57, 19)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel3.TabIndex = 242
        Me.LmTitleLabel3.Text = "印刷種別"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "印刷種別"
        Me.LmTitleLabel3.WidthDef = 63
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "N016"
        Me.cmbPrint.DataSource = Nothing
        Me.cmbPrint.DisplayMember = Nothing
        Me.cmbPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.HeightDef = 18
        Me.cmbPrint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrint.HissuLabelVisible = True
        Me.cmbPrint.InsertWildCard = True
        Me.cmbPrint.IsForbiddenWordsCheck = False
        Me.cmbPrint.IsHissuCheck = True
        Me.cmbPrint.ItemName = ""
        Me.cmbPrint.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrint.Location = New System.Drawing.Point(126, 16)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(320, 18)
        Me.cmbPrint.TabIndex = 406
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 320
        '
        'LMB030F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMB030F"
        Me.Text = "【LMB030F】  入荷印刷指示"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNM_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNM_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNyukaDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdNyukaDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleDataInsDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdDataInsDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents chkFurikae As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr

End Class
