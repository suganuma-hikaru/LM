<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LMB070F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

    'Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.LmTitleLabelSatsueiDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdSatsueiDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.btnDownLoad = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.imdSatsueiDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblFromTo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.pnlDetailIn = New System.Windows.Forms.Panel()
        Me.txtKeyword = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtNo)
        Me.pnlViewAria.Controls.Add(Me.txtKeyword)
        Me.pnlViewAria.Controls.Add(Me.pnlDetail)
        Me.pnlViewAria.Controls.Add(Me.lblFromTo)
        Me.pnlViewAria.Controls.Add(Me.imdSatsueiDateTo)
        Me.pnlViewAria.Controls.Add(Me.btnDownLoad)
        Me.pnlViewAria.Controls.Add(Me.imdSatsueiDateFrom)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabelSatsueiDate)
        Me.pnlViewAria.Size = New System.Drawing.Size(878, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.AutoSize = False
        Me.FunctionKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.FunctionKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.FunctionKey.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.FunctionKey.CombinationKeyDisplayMode = GrapeCity.Win.Bars.CombinationKeyDisplayMode.Always
        Me.FunctionKey.Dock = System.Windows.Forms.DockStyle.None
        Me.FunctionKey.F10ButtonEnabled = True
        Me.FunctionKey.F10ButtonName = "条件クリア"
        Me.FunctionKey.F11ButtonEnabled = True
        Me.FunctionKey.F11ButtonName = "Ｏ　Ｋ"
        Me.FunctionKey.F12ButtonEnabled = True
        Me.FunctionKey.F12ButtonName = "キャンセル"
        Me.FunctionKey.F1ButtonEnabled = False
        Me.FunctionKey.F1ButtonName = ""
        Me.FunctionKey.F2ButtonEnabled = False
        Me.FunctionKey.F2ButtonName = ""
        Me.FunctionKey.F3ButtonEnabled = False
        Me.FunctionKey.F3ButtonName = ""
        Me.FunctionKey.F4ButtonEnabled = False
        Me.FunctionKey.F4ButtonName = ""
        Me.FunctionKey.F5ButtonEnabled = False
        Me.FunctionKey.F5ButtonName = ""
        Me.FunctionKey.F6ButtonEnabled = False
        Me.FunctionKey.F6ButtonName = ""
        Me.FunctionKey.F7ButtonEnabled = False
        Me.FunctionKey.F7ButtonName = ""
        Me.FunctionKey.F8ButtonEnabled = False
        Me.FunctionKey.F8ButtonName = ""
        Me.FunctionKey.F9ButtonEnabled = True
        Me.FunctionKey.F9ButtonName = "検　索"
        Me.FunctionKey.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FunctionKey.FontDef = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FunctionKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FunctionKey.ForeColorDef = System.Drawing.SystemColors.ControlText
        Me.FunctionKey.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.FunctionKey.HeightDef = 40
        Me.FunctionKey.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.FunctionKey.Location = New System.Drawing.Point(517, 0)
        Me.FunctionKey.Margin = New System.Windows.Forms.Padding(4)
        Me.FunctionKey.Name = "FunctionKey"
        Me.FunctionKey.Size = New System.Drawing.Size(361, 40)
        Me.FunctionKey.TabIndex = 0
        Me.FunctionKey.TextValue = ""
        Me.FunctionKey.Visible = True
        Me.FunctionKey.WidthDef = 361
        '
        'LmTitleLabelSatsueiDate
        '
        Me.LmTitleLabelSatsueiDate.AutoSize = True
        Me.LmTitleLabelSatsueiDate.AutoSizeDef = True
        Me.LmTitleLabelSatsueiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabelSatsueiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabelSatsueiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabelSatsueiDate.EnableStatus = False
        Me.LmTitleLabelSatsueiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabelSatsueiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabelSatsueiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabelSatsueiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabelSatsueiDate.HeightDef = 20
        Me.LmTitleLabelSatsueiDate.Location = New System.Drawing.Point(12, 10)
        Me.LmTitleLabelSatsueiDate.Name = "LmTitleLabelSatsueiDate"
        Me.LmTitleLabelSatsueiDate.Size = New System.Drawing.Size(69, 20)
        Me.LmTitleLabelSatsueiDate.TabIndex = 259
        Me.LmTitleLabelSatsueiDate.Text = "撮影日"
        Me.LmTitleLabelSatsueiDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabelSatsueiDate.TextValue = "撮影日"
        Me.LmTitleLabelSatsueiDate.WidthDef = 69
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
        Me.LmTitleLabel2.HeightDef = 20
        Me.LmTitleLabel2.Location = New System.Drawing.Point(12, 34)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(149, 20)
        Me.LmTitleLabel2.TabIndex = 601
        Me.LmTitleLabel2.Text = "キーワード検索"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "キーワード検索"
        Me.LmTitleLabel2.WidthDef = 149
        '
        'imdSatsueiDateFrom
        '
        Me.imdSatsueiDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSatsueiDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSatsueiDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSatsueiDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdSatsueiDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSatsueiDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSatsueiDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSatsueiDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSatsueiDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSatsueiDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSatsueiDateFrom.HeightDef = 18
        Me.imdSatsueiDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSatsueiDateFrom.HissuLabelVisible = False
        Me.imdSatsueiDateFrom.Holiday = True
        Me.imdSatsueiDateFrom.IsAfterDateCheck = False
        Me.imdSatsueiDateFrom.IsBeforeDateCheck = False
        Me.imdSatsueiDateFrom.IsHissuCheck = False
        Me.imdSatsueiDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdSatsueiDateFrom.ItemName = ""
        Me.imdSatsueiDateFrom.Location = New System.Drawing.Point(139, 12)
        Me.imdSatsueiDateFrom.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.imdSatsueiDateFrom.Name = "imdSatsueiDateFrom"
        Me.imdSatsueiDateFrom.Number = CType(10101000000, Long)
        Me.imdSatsueiDateFrom.ReadOnly = False
        Me.imdSatsueiDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdSatsueiDateFrom.TabIndex = 605
        Me.imdSatsueiDateFrom.TabStopSetting = True
        Me.imdSatsueiDateFrom.TextValue = ""
        Me.imdSatsueiDateFrom.Value = New Date(CType(0, Long))
        Me.imdSatsueiDateFrom.WidthDef = 118
        '
        'btnDownLoad
        '
        Me.btnDownLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDownLoad.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDownLoad.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnDownLoad.EnableStatus = True
        Me.btnDownLoad.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDownLoad.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDownLoad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDownLoad.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDownLoad.HeightDef = 22
        Me.btnDownLoad.Location = New System.Drawing.Point(754, 3)
        Me.btnDownLoad.Name = "btnDownLoad"
        Me.btnDownLoad.Size = New System.Drawing.Size(90, 22)
        Me.btnDownLoad.TabIndex = 117
        Me.btnDownLoad.TabStopSetting = True
        Me.btnDownLoad.Text = "Download"
        Me.btnDownLoad.TextValue = "Download"
        Me.btnDownLoad.UseVisualStyleBackColor = True
        Me.btnDownLoad.WidthDef = 90
        '
        'imdSatsueiDateTo
        '
        Me.imdSatsueiDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSatsueiDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSatsueiDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSatsueiDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSatsueiDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSatsueiDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSatsueiDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSatsueiDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSatsueiDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSatsueiDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSatsueiDateTo.HeightDef = 18
        Me.imdSatsueiDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSatsueiDateTo.HissuLabelVisible = False
        Me.imdSatsueiDateTo.Holiday = True
        Me.imdSatsueiDateTo.IsAfterDateCheck = False
        Me.imdSatsueiDateTo.IsBeforeDateCheck = False
        Me.imdSatsueiDateTo.IsHissuCheck = False
        Me.imdSatsueiDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdSatsueiDateTo.ItemName = ""
        Me.imdSatsueiDateTo.Location = New System.Drawing.Point(277, 12)
        Me.imdSatsueiDateTo.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.imdSatsueiDateTo.Name = "imdSatsueiDateTo"
        Me.imdSatsueiDateTo.Number = CType(10101000000, Long)
        Me.imdSatsueiDateTo.ReadOnly = False
        Me.imdSatsueiDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdSatsueiDateTo.TabIndex = 607
        Me.imdSatsueiDateTo.TabStopSetting = True
        Me.imdSatsueiDateTo.TextValue = ""
        Me.imdSatsueiDateTo.Value = New Date(CType(0, Long))
        Me.imdSatsueiDateTo.WidthDef = 118
        '
        'lblFromTo
        '
        Me.lblFromTo.AutoSize = True
        Me.lblFromTo.AutoSizeDef = True
        Me.lblFromTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFromTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFromTo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblFromTo.EnableStatus = False
        Me.lblFromTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFromTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFromTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFromTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFromTo.HeightDef = 20
        Me.lblFromTo.Location = New System.Drawing.Point(250, 13)
        Me.lblFromTo.Name = "lblFromTo"
        Me.lblFromTo.Size = New System.Drawing.Size(29, 20)
        Me.lblFromTo.TabIndex = 608
        Me.lblFromTo.Text = "～"
        Me.lblFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFromTo.TextValue = "～"
        Me.lblFromTo.WidthDef = 29
        '
        'pnlDetail
        '
        Me.pnlDetail.AutoScroll = True
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetail.Controls.Add(Me.pnlDetailIn)
        Me.pnlDetail.Location = New System.Drawing.Point(12, 70)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(854, 806)
        Me.pnlDetail.TabIndex = 609
        '
        'pnlDetailIn
        '
        Me.pnlDetailIn.Location = New System.Drawing.Point(0, 0)
        Me.pnlDetailIn.Name = "pnlDetailIn"
        Me.pnlDetailIn.Size = New System.Drawing.Size(828, 880)
        Me.pnlDetailIn.TabIndex = 0
        '
        'txtKeyword
        '
        Me.txtKeyword.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKeyword.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKeyword.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKeyword.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKeyword.CountWrappedLine = False
        Me.txtKeyword.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKeyword.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyword.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyword.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyword.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKeyword.HeightDef = 18
        Me.txtKeyword.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKeyword.HissuLabelVisible = False
        Me.txtKeyword.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtKeyword.IsByteCheck = 50
        Me.txtKeyword.IsCalendarCheck = False
        Me.txtKeyword.IsDakutenCheck = False
        Me.txtKeyword.IsEisuCheck = False
        Me.txtKeyword.IsForbiddenWordsCheck = False
        Me.txtKeyword.IsFullByteCheck = 0
        Me.txtKeyword.IsHankakuCheck = False
        Me.txtKeyword.IsHissuCheck = False
        Me.txtKeyword.IsKanaCheck = False
        Me.txtKeyword.IsMiddleSpace = False
        Me.txtKeyword.IsNumericCheck = False
        Me.txtKeyword.IsSujiCheck = False
        Me.txtKeyword.IsZenkakuCheck = False
        Me.txtKeyword.ItemName = ""
        Me.txtKeyword.LineSpace = 0
        Me.txtKeyword.Location = New System.Drawing.Point(139, 34)
        Me.txtKeyword.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.txtKeyword.MaxLength = 50
        Me.txtKeyword.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKeyword.MaxLineCount = 0
        Me.txtKeyword.Multiline = False
        Me.txtKeyword.Name = "txtKeyword"
        Me.txtKeyword.ReadOnly = False
        Me.txtKeyword.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKeyword.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKeyword.Size = New System.Drawing.Size(724, 18)
        Me.txtKeyword.TabIndex = 610
        Me.txtKeyword.TabStopSetting = True
        Me.txtKeyword.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtKeyword.UseSystemPasswordChar = False
        Me.txtKeyword.WidthDef = 724
        Me.txtKeyword.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtNo
        '
        Me.txtNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNo.CountWrappedLine = False
        Me.txtNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNo.HeightDef = 18
        Me.txtNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNo.HissuLabelVisible = False
        Me.txtNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtNo.IsByteCheck = 50
        Me.txtNo.IsCalendarCheck = False
        Me.txtNo.IsDakutenCheck = False
        Me.txtNo.IsEisuCheck = False
        Me.txtNo.IsForbiddenWordsCheck = False
        Me.txtNo.IsFullByteCheck = 0
        Me.txtNo.IsHankakuCheck = False
        Me.txtNo.IsHissuCheck = False
        Me.txtNo.IsKanaCheck = False
        Me.txtNo.IsMiddleSpace = False
        Me.txtNo.IsNumericCheck = False
        Me.txtNo.IsSujiCheck = False
        Me.txtNo.IsZenkakuCheck = False
        Me.txtNo.ItemName = ""
        Me.txtNo.LineSpace = 0
        Me.txtNo.Location = New System.Drawing.Point(139, 34)
        Me.txtNo.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.txtNo.MaxLength = 50
        Me.txtNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNo.MaxLineCount = 0
        Me.txtNo.Multiline = False
        Me.txtNo.Name = "txtNo"
        Me.txtNo.ReadOnly = False
        Me.txtNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNo.Size = New System.Drawing.Size(724, 18)
        Me.txtNo.TabIndex = 610
        Me.txtNo.TabStopSetting = True
        Me.txtNo.TextValue = ""
        Me.txtNo.Visible = False
        Me.txtNo.UseSystemPasswordChar = False
        Me.txtNo.WidthDef = 724
        Me.txtNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMB070F
        '
        Me.ClientSize = New System.Drawing.Size(878, 962)
        Me.Name = "LMB070F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LmTitleLabelSatsueiDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSatsueiDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents btnDownLoad As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents imdSatsueiDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblFromTo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlDetail As Panel
    Friend WithEvents pnlDetailIn As Panel
    Friend WithEvents txtKeyword As Win.InputMan.LMImTextBox
    Friend WithEvents txtNo As Win.InputMan.LMImTextBox
End Class
