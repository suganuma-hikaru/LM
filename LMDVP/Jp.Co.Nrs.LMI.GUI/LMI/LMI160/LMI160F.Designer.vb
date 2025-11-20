<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI160F
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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.lblTitlePrint = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpShuturyoku = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.optAll = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optKakuteiZumi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.optNotKakutei = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.imdNonyuDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.imdNonyuDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.TitleLabelKara = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNonyuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNrsBrCdCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleBrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.pnlViewAria.SuspendLayout()
        Me.grpShuturyoku.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.grpShuturyoku)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePrint)
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
        Me.lblTitlePrint.Location = New System.Drawing.Point(56, 19)
        Me.lblTitlePrint.Name = "lblTitlePrint"
        Me.lblTitlePrint.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePrint.TabIndex = 241
        Me.lblTitlePrint.Text = "印刷種別"
        Me.lblTitlePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrint.TextValue = "印刷種別"
        Me.lblTitlePrint.WidthDef = 63
        '
        'grpShuturyoku
        '
        Me.grpShuturyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpShuturyoku.Controls.Add(Me.optAll)
        Me.grpShuturyoku.Controls.Add(Me.optKakuteiZumi)
        Me.grpShuturyoku.Controls.Add(Me.LmTitleLabel1)
        Me.grpShuturyoku.Controls.Add(Me.optNotKakutei)
        Me.grpShuturyoku.Controls.Add(Me.imdNonyuDateTo)
        Me.grpShuturyoku.Controls.Add(Me.imdNonyuDateFrom)
        Me.grpShuturyoku.Controls.Add(Me.cmbBr)
        Me.grpShuturyoku.Controls.Add(Me.TitleLabelKara)
        Me.grpShuturyoku.Controls.Add(Me.lblCustNmM)
        Me.grpShuturyoku.Controls.Add(Me.txtCustCdM)
        Me.grpShuturyoku.Controls.Add(Me.lblCustNmL)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleNonyuDate)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleNrsBrCdCd)
        Me.grpShuturyoku.Controls.Add(Me.txtCustCdL)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleBrCd)
        Me.grpShuturyoku.EnableStatus = False
        Me.grpShuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.HeightDef = 185
        Me.grpShuturyoku.Location = New System.Drawing.Point(27, 51)
        Me.grpShuturyoku.Name = "grpShuturyoku"
        Me.grpShuturyoku.Size = New System.Drawing.Size(738, 185)
        Me.grpShuturyoku.TabIndex = 247
        Me.grpShuturyoku.TabStop = False
        Me.grpShuturyoku.Text = "出力条件"
        Me.grpShuturyoku.TextValue = "出力条件"
        Me.grpShuturyoku.WidthDef = 738
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optAll.EnableStatus = True
        Me.optAll.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.HeightDef = 17
        Me.optAll.Location = New System.Drawing.Point(259, 93)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(53, 17)
        Me.optAll.TabIndex = 9
        Me.optAll.TabStopSetting = False
        Me.optAll.Text = "全て"
        Me.optAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optAll.TextValue = "全て"
        Me.optAll.UseVisualStyleBackColor = True
        Me.optAll.WidthDef = 53
        '
        'optKakuteiZumi
        '
        Me.optKakuteiZumi.AutoSize = True
        Me.optKakuteiZumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKakuteiZumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKakuteiZumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKakuteiZumi.EnableStatus = True
        Me.optKakuteiZumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKakuteiZumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKakuteiZumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKakuteiZumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKakuteiZumi.HeightDef = 17
        Me.optKakuteiZumi.Location = New System.Drawing.Point(179, 93)
        Me.optKakuteiZumi.Name = "optKakuteiZumi"
        Me.optKakuteiZumi.Size = New System.Drawing.Size(67, 17)
        Me.optKakuteiZumi.TabIndex = 8
        Me.optKakuteiZumi.TabStopSetting = False
        Me.optKakuteiZumi.Text = "確定済"
        Me.optKakuteiZumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKakuteiZumi.TextValue = "確定済"
        Me.optKakuteiZumi.UseVisualStyleBackColor = True
        Me.optKakuteiZumi.WidthDef = 67
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(29, 95)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel1.TabIndex = 412
        Me.LmTitleLabel1.Text = "請求確認"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "請求確認"
        Me.LmTitleLabel1.WidthDef = 63
        '
        'optNotKakutei
        '
        Me.optNotKakutei.AutoSize = True
        Me.optNotKakutei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optNotKakutei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optNotKakutei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optNotKakutei.Checked = True
        Me.optNotKakutei.EnableStatus = True
        Me.optNotKakutei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optNotKakutei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optNotKakutei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optNotKakutei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optNotKakutei.HeightDef = 17
        Me.optNotKakutei.Location = New System.Drawing.Point(101, 93)
        Me.optNotKakutei.Name = "optNotKakutei"
        Me.optNotKakutei.Size = New System.Drawing.Size(67, 17)
        Me.optNotKakutei.TabIndex = 7
        Me.optNotKakutei.TabStop = True
        Me.optNotKakutei.TabStopSetting = True
        Me.optNotKakutei.Text = "未確定"
        Me.optNotKakutei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optNotKakutei.TextValue = "未確定"
        Me.optNotKakutei.UseVisualStyleBackColor = True
        Me.optNotKakutei.WidthDef = 67
        '
        'imdNonyuDateTo
        '
        Me.imdNonyuDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNonyuDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNonyuDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNonyuDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdNonyuDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNonyuDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNonyuDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNonyuDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNonyuDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNonyuDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNonyuDateTo.HeightDef = 18
        Me.imdNonyuDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNonyuDateTo.HissuLabelVisible = True
        Me.imdNonyuDateTo.Holiday = True
        Me.imdNonyuDateTo.IsAfterDateCheck = False
        Me.imdNonyuDateTo.IsBeforeDateCheck = False
        Me.imdNonyuDateTo.IsHissuCheck = True
        Me.imdNonyuDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdNonyuDateTo.ItemName = ""
        Me.imdNonyuDateTo.Location = New System.Drawing.Point(245, 121)
        Me.imdNonyuDateTo.Name = "imdNonyuDateTo"
        Me.imdNonyuDateTo.Number = CType(10101000000, Long)
        Me.imdNonyuDateTo.ReadOnly = False
        Me.imdNonyuDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdNonyuDateTo.TabIndex = 411
        Me.imdNonyuDateTo.TabStopSetting = True
        Me.imdNonyuDateTo.TextValue = ""
        Me.imdNonyuDateTo.Value = New Date(CType(0, Long))
        Me.imdNonyuDateTo.WidthDef = 118
        '
        'imdNonyuDateFrom
        '
        Me.imdNonyuDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNonyuDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNonyuDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNonyuDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdNonyuDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNonyuDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNonyuDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNonyuDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNonyuDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNonyuDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNonyuDateFrom.HeightDef = 18
        Me.imdNonyuDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNonyuDateFrom.HissuLabelVisible = True
        Me.imdNonyuDateFrom.Holiday = True
        Me.imdNonyuDateFrom.IsAfterDateCheck = False
        Me.imdNonyuDateFrom.IsBeforeDateCheck = False
        Me.imdNonyuDateFrom.IsHissuCheck = True
        Me.imdNonyuDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdNonyuDateFrom.ItemName = ""
        Me.imdNonyuDateFrom.Location = New System.Drawing.Point(99, 121)
        Me.imdNonyuDateFrom.Name = "imdNonyuDateFrom"
        Me.imdNonyuDateFrom.Number = CType(10101000000, Long)
        Me.imdNonyuDateFrom.ReadOnly = False
        Me.imdNonyuDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdNonyuDateFrom.TabIndex = 410
        Me.imdNonyuDateFrom.TabStopSetting = True
        Me.imdNonyuDateFrom.TextValue = ""
        Me.imdNonyuDateFrom.Value = New Date(CType(0, Long))
        Me.imdNonyuDateFrom.WidthDef = 118
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBr.DataSource = Nothing
        Me.cmbBr.DisplayMember = Nothing
        Me.cmbBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.HeightDef = 18
        Me.cmbBr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.HissuLabelVisible = True
        Me.cmbBr.InsertWildCard = True
        Me.cmbBr.IsForbiddenWordsCheck = False
        Me.cmbBr.IsHissuCheck = True
        Me.cmbBr.ItemName = ""
        Me.cmbBr.Location = New System.Drawing.Point(99, 25)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = True
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 232
        Me.cmbBr.TabStop = False
        Me.cmbBr.TabStopSetting = False
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
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
        Me.TitleLabelKara.Location = New System.Drawing.Point(218, 126)
        Me.TitleLabelKara.Name = "TitleLabelKara"
        Me.TitleLabelKara.Size = New System.Drawing.Size(21, 13)
        Me.TitleLabelKara.TabIndex = 129
        Me.TitleLabelKara.Text = "～"
        Me.TitleLabelKara.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitleLabelKara.TextValue = "～"
        Me.TitleLabelKara.WidthDef = 21
        '
        'lblCustNmM
        '
        Me.lblCustNmM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmM.CountWrappedLine = False
        Me.lblCustNmM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmM.HeightDef = 18
        Me.lblCustNmM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.HissuLabelVisible = False
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = False
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(180, 66)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNmM.TabIndex = 130
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = ""
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 513
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdM.CountWrappedLine = False
        Me.txtCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdM.HeightDef = 18
        Me.txtCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.HissuLabelVisible = False
        Me.txtCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdM.IsByteCheck = 2
        Me.txtCustCdM.IsCalendarCheck = False
        Me.txtCustCdM.IsDakutenCheck = False
        Me.txtCustCdM.IsEisuCheck = False
        Me.txtCustCdM.IsForbiddenWordsCheck = False
        Me.txtCustCdM.IsFullByteCheck = 0
        Me.txtCustCdM.IsHankakuCheck = False
        Me.txtCustCdM.IsHissuCheck = False
        Me.txtCustCdM.IsKanaCheck = False
        Me.txtCustCdM.IsMiddleSpace = False
        Me.txtCustCdM.IsNumericCheck = False
        Me.txtCustCdM.IsSujiCheck = False
        Me.txtCustCdM.IsZenkakuCheck = False
        Me.txtCustCdM.ItemName = ""
        Me.txtCustCdM.LineSpace = 0
        Me.txtCustCdM.Location = New System.Drawing.Point(145, 66)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 129
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNmL
        '
        Me.lblCustNmL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmL.CountWrappedLine = False
        Me.lblCustNmL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmL.HeightDef = 18
        Me.lblCustNmL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.HissuLabelVisible = False
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = False
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(180, 46)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNmL.TabIndex = 127
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = ""
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 513
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNonyuDate
        '
        Me.lblTitleNonyuDate.AutoSize = True
        Me.lblTitleNonyuDate.AutoSizeDef = True
        Me.lblTitleNonyuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNonyuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNonyuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNonyuDate.EnableStatus = False
        Me.lblTitleNonyuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNonyuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNonyuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNonyuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNonyuDate.HeightDef = 13
        Me.lblTitleNonyuDate.Location = New System.Drawing.Point(44, 123)
        Me.lblTitleNonyuDate.Name = "lblTitleNonyuDate"
        Me.lblTitleNonyuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNonyuDate.TabIndex = 126
        Me.lblTitleNonyuDate.Text = "納入日"
        Me.lblTitleNonyuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNonyuDate.TextValue = "納入日"
        Me.lblTitleNonyuDate.WidthDef = 49
        '
        'lblTitleNrsBrCdCd
        '
        Me.lblTitleNrsBrCdCd.AutoSize = True
        Me.lblTitleNrsBrCdCd.AutoSizeDef = True
        Me.lblTitleNrsBrCdCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsBrCdCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsBrCdCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNrsBrCdCd.EnableStatus = False
        Me.lblTitleNrsBrCdCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsBrCdCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsBrCdCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsBrCdCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsBrCdCd.HeightDef = 13
        Me.lblTitleNrsBrCdCd.Location = New System.Drawing.Point(58, 48)
        Me.lblTitleNrsBrCdCd.Name = "lblTitleNrsBrCdCd"
        Me.lblTitleNrsBrCdCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleNrsBrCdCd.TabIndex = 125
        Me.lblTitleNrsBrCdCd.Text = "荷主"
        Me.lblTitleNrsBrCdCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNrsBrCdCd.TextValue = "荷主"
        Me.lblTitleNrsBrCdCd.WidthDef = 35
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdL.CountWrappedLine = False
        Me.txtCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdL.HeightDef = 18
        Me.txtCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.HissuLabelVisible = False
        Me.txtCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdL.IsByteCheck = 5
        Me.txtCustCdL.IsCalendarCheck = False
        Me.txtCustCdL.IsDakutenCheck = False
        Me.txtCustCdL.IsEisuCheck = False
        Me.txtCustCdL.IsForbiddenWordsCheck = False
        Me.txtCustCdL.IsFullByteCheck = 0
        Me.txtCustCdL.IsHankakuCheck = False
        Me.txtCustCdL.IsHissuCheck = False
        Me.txtCustCdL.IsKanaCheck = False
        Me.txtCustCdL.IsMiddleSpace = False
        Me.txtCustCdL.IsNumericCheck = False
        Me.txtCustCdL.IsSujiCheck = False
        Me.txtCustCdL.IsZenkakuCheck = False
        Me.txtCustCdL.ItemName = ""
        Me.txtCustCdL.LineSpace = 0
        Me.txtCustCdL.Location = New System.Drawing.Point(99, 46)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(100, 18)
        Me.txtCustCdL.TabIndex = 74
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 100
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleBrCd
        '
        Me.lblTitleBrCd.AutoSize = True
        Me.lblTitleBrCd.AutoSizeDef = True
        Me.lblTitleBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBrCd.EnableStatus = False
        Me.lblTitleBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBrCd.HeightDef = 13
        Me.lblTitleBrCd.Location = New System.Drawing.Point(44, 28)
        Me.lblTitleBrCd.Name = "lblTitleBrCd"
        Me.lblTitleBrCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleBrCd.TabIndex = 17
        Me.lblTitleBrCd.Text = "営業所"
        Me.lblTitleBrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBrCd.TextValue = "営業所"
        Me.lblTitleBrCd.WidthDef = 49
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "U021"
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
        Me.cmbPrint.Location = New System.Drawing.Point(125, 16)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(320, 18)
        Me.cmbPrint.TabIndex = 409
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 320
        '
        'LMI160F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI160F"
        Me.Text = "【LMI160F】  運賃請求印刷指示"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpShuturyoku.ResumeLayout(False)
        Me.grpShuturyoku.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitlePrint As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpShuturyoku As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents TitleLabelKara As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNonyuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNrsBrCdCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleBrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents imdNonyuDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdNonyuDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents optAll As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optKakuteiZumi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents optNotKakutei As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton

End Class
