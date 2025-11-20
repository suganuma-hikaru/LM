<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI180F
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
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMI180F))
        Me.grpShukkaKaishu = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.txtOutkaNoLOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtFileTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.grpTorikomi = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnSelect = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.txtPath = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleFile = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpExcel = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnExcel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleHokokuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblKara1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdHokokuDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.imdHokokuDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.txtSerialNoTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblKara2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtSerialNoFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleSerialNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleDestNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtOutkaNoL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleOutokaNoL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpMode = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.optTorikeshi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optKaishu = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optShukka = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.lblTitleKaishuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdKaishuDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetails = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.pnlViewAria.SuspendLayout()
        Me.grpShukkaKaishu.SuspendLayout()
        Me.grpTorikomi.SuspendLayout()
        Me.grpExcel.SuspendLayout()
        Me.grpMode.SuspendLayout()
        CType(Me.sprDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.sprDetails)
        Me.pnlViewAria.Controls.Add(Me.grpShukkaKaishu)
        '
        'grpShukkaKaishu
        '
        Me.grpShukkaKaishu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShukkaKaishu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShukkaKaishu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpShukkaKaishu.Controls.Add(Me.txtOutkaNoLOld)
        Me.grpShukkaKaishu.Controls.Add(Me.txtFileTextBox)
        Me.grpShukkaKaishu.Controls.Add(Me.grpTorikomi)
        Me.grpShukkaKaishu.Controls.Add(Me.grpExcel)
        Me.grpShukkaKaishu.Controls.Add(Me.txtSerialNoTo)
        Me.grpShukkaKaishu.Controls.Add(Me.lblKara2)
        Me.grpShukkaKaishu.Controls.Add(Me.txtSerialNoFrom)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleSerialNo)
        Me.grpShukkaKaishu.Controls.Add(Me.lblDestNm)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleDestNm)
        Me.grpShukkaKaishu.Controls.Add(Me.txtOutkaNoL)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleOutokaNoL)
        Me.grpShukkaKaishu.Controls.Add(Me.lblCustNM)
        Me.grpShukkaKaishu.Controls.Add(Me.txtCustCD)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleCust)
        Me.grpShukkaKaishu.Controls.Add(Me.grpMode)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleKaishuDate)
        Me.grpShukkaKaishu.Controls.Add(Me.imdKaishuDate)
        Me.grpShukkaKaishu.Controls.Add(Me.cmbEigyo)
        Me.grpShukkaKaishu.Controls.Add(Me.lblTitleEigyo)
        Me.grpShukkaKaishu.EnableStatus = False
        Me.grpShukkaKaishu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShukkaKaishu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShukkaKaishu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShukkaKaishu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShukkaKaishu.HeightDef = 276
        Me.grpShukkaKaishu.Location = New System.Drawing.Point(3, 19)
        Me.grpShukkaKaishu.Name = "grpShukkaKaishu"
        Me.grpShukkaKaishu.Size = New System.Drawing.Size(1259, 276)
        Me.grpShukkaKaishu.TabIndex = 238
        Me.grpShukkaKaishu.TabStop = False
        Me.grpShukkaKaishu.Text = "出荷／回収"
        Me.grpShukkaKaishu.TextValue = "出荷／回収"
        Me.grpShukkaKaishu.WidthDef = 1259
        '
        'txtOutkaNoLOld
        '
        Me.txtOutkaNoLOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNoLOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNoLOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaNoLOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaNoLOld.CountWrappedLine = False
        Me.txtOutkaNoLOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaNoLOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNoLOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNoLOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNoLOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNoLOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaNoLOld.HeightDef = 18
        Me.txtOutkaNoLOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNoLOld.HissuLabelVisible = False
        Me.txtOutkaNoLOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtOutkaNoLOld.IsByteCheck = 9
        Me.txtOutkaNoLOld.IsCalendarCheck = False
        Me.txtOutkaNoLOld.IsDakutenCheck = False
        Me.txtOutkaNoLOld.IsEisuCheck = False
        Me.txtOutkaNoLOld.IsForbiddenWordsCheck = False
        Me.txtOutkaNoLOld.IsFullByteCheck = 0
        Me.txtOutkaNoLOld.IsHankakuCheck = False
        Me.txtOutkaNoLOld.IsHissuCheck = False
        Me.txtOutkaNoLOld.IsKanaCheck = False
        Me.txtOutkaNoLOld.IsMiddleSpace = False
        Me.txtOutkaNoLOld.IsNumericCheck = False
        Me.txtOutkaNoLOld.IsSujiCheck = False
        Me.txtOutkaNoLOld.IsZenkakuCheck = False
        Me.txtOutkaNoLOld.ItemName = ""
        Me.txtOutkaNoLOld.LineSpace = 0
        Me.txtOutkaNoLOld.Location = New System.Drawing.Point(715, 114)
        Me.txtOutkaNoLOld.MaxLength = 9
        Me.txtOutkaNoLOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaNoLOld.MaxLineCount = 0
        Me.txtOutkaNoLOld.Multiline = False
        Me.txtOutkaNoLOld.Name = "txtOutkaNoLOld"
        Me.txtOutkaNoLOld.ReadOnly = True
        Me.txtOutkaNoLOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaNoLOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaNoLOld.Size = New System.Drawing.Size(93, 18)
        Me.txtOutkaNoLOld.TabIndex = 282
        Me.txtOutkaNoLOld.TabStop = False
        Me.txtOutkaNoLOld.TabStopSetting = False
        Me.txtOutkaNoLOld.TextValue = ""
        Me.txtOutkaNoLOld.UseSystemPasswordChar = False
        Me.txtOutkaNoLOld.Visible = False
        Me.txtOutkaNoLOld.WidthDef = 93
        Me.txtOutkaNoLOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtFileTextBox
        '
        Me.txtFileTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileTextBox.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileTextBox.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileTextBox.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFileTextBox.CountWrappedLine = False
        Me.txtFileTextBox.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFileTextBox.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileTextBox.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileTextBox.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileTextBox.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFileTextBox.HeightDef = 18
        Me.txtFileTextBox.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileTextBox.HissuLabelVisible = False
        Me.txtFileTextBox.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtFileTextBox.IsByteCheck = 0
        Me.txtFileTextBox.IsCalendarCheck = False
        Me.txtFileTextBox.IsDakutenCheck = False
        Me.txtFileTextBox.IsEisuCheck = False
        Me.txtFileTextBox.IsForbiddenWordsCheck = False
        Me.txtFileTextBox.IsFullByteCheck = 0
        Me.txtFileTextBox.IsHankakuCheck = False
        Me.txtFileTextBox.IsHissuCheck = False
        Me.txtFileTextBox.IsKanaCheck = False
        Me.txtFileTextBox.IsMiddleSpace = False
        Me.txtFileTextBox.IsNumericCheck = False
        Me.txtFileTextBox.IsSujiCheck = False
        Me.txtFileTextBox.IsZenkakuCheck = False
        Me.txtFileTextBox.ItemName = ""
        Me.txtFileTextBox.LineSpace = 0
        Me.txtFileTextBox.Location = New System.Drawing.Point(620, 207)
        Me.txtFileTextBox.MaxLength = 0
        Me.txtFileTextBox.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFileTextBox.MaxLineCount = 0
        Me.txtFileTextBox.Multiline = False
        Me.txtFileTextBox.Name = "txtFileTextBox"
        Me.txtFileTextBox.ReadOnly = True
        Me.txtFileTextBox.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFileTextBox.Size = New System.Drawing.Size(93, 18)
        Me.txtFileTextBox.TabIndex = 281
        Me.txtFileTextBox.TabStop = False
        Me.txtFileTextBox.TabStopSetting = False
        Me.txtFileTextBox.TextValue = ""
        Me.txtFileTextBox.UseSystemPasswordChar = False
        Me.txtFileTextBox.Visible = False
        Me.txtFileTextBox.WidthDef = 93
        Me.txtFileTextBox.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpTorikomi
        '
        Me.grpTorikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpTorikomi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpTorikomi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpTorikomi.Controls.Add(Me.btnSelect)
        Me.grpTorikomi.Controls.Add(Me.txtPath)
        Me.grpTorikomi.Controls.Add(Me.lblTitleFile)
        Me.grpTorikomi.EnableStatus = False
        Me.grpTorikomi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpTorikomi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpTorikomi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpTorikomi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpTorikomi.HeightDef = 66
        Me.grpTorikomi.Location = New System.Drawing.Point(13, 184)
        Me.grpTorikomi.Name = "grpTorikomi"
        Me.grpTorikomi.Size = New System.Drawing.Size(601, 66)
        Me.grpTorikomi.TabIndex = 280
        Me.grpTorikomi.TabStop = False
        Me.grpTorikomi.Text = "荷主在庫取込"
        Me.grpTorikomi.TextValue = "荷主在庫取込"
        Me.grpTorikomi.WidthDef = 601
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSelect.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSelect.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSelect.EnableStatus = True
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSelect.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSelect.HeightDef = 22
        Me.btnSelect.Location = New System.Drawing.Point(519, 21)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(70, 22)
        Me.btnSelect.TabIndex = 458
        Me.btnSelect.TabStopSetting = True
        Me.btnSelect.Text = "選択"
        Me.btnSelect.TextValue = "選択"
        Me.btnSelect.UseVisualStyleBackColor = True
        Me.btnSelect.WidthDef = 70
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPath.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPath.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPath.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPath.CountWrappedLine = False
        Me.txtPath.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtPath.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPath.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPath.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPath.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtPath.HeightDef = 18
        Me.txtPath.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtPath.HissuLabelVisible = False
        Me.txtPath.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtPath.IsByteCheck = 0
        Me.txtPath.IsCalendarCheck = False
        Me.txtPath.IsDakutenCheck = False
        Me.txtPath.IsEisuCheck = False
        Me.txtPath.IsForbiddenWordsCheck = False
        Me.txtPath.IsFullByteCheck = 0
        Me.txtPath.IsHankakuCheck = False
        Me.txtPath.IsHissuCheck = False
        Me.txtPath.IsKanaCheck = False
        Me.txtPath.IsMiddleSpace = False
        Me.txtPath.IsNumericCheck = False
        Me.txtPath.IsSujiCheck = False
        Me.txtPath.IsZenkakuCheck = False
        Me.txtPath.ItemName = ""
        Me.txtPath.LineSpace = 0
        Me.txtPath.Location = New System.Drawing.Point(117, 23)
        Me.txtPath.MaxLength = 0
        Me.txtPath.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtPath.MaxLineCount = 0
        Me.txtPath.Multiline = False
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = False
        Me.txtPath.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtPath.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPath.Size = New System.Drawing.Size(396, 18)
        Me.txtPath.TabIndex = 261
        Me.txtPath.TabStopSetting = True
        Me.txtPath.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtPath.UseSystemPasswordChar = False
        Me.txtPath.WidthDef = 396
        Me.txtPath.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleFile
        '
        Me.lblTitleFile.AutoSize = True
        Me.lblTitleFile.AutoSizeDef = True
        Me.lblTitleFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFile.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFile.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFile.EnableStatus = False
        Me.lblTitleFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFile.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFile.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFile.HeightDef = 13
        Me.lblTitleFile.Location = New System.Drawing.Point(6, 26)
        Me.lblTitleFile.Name = "lblTitleFile"
        Me.lblTitleFile.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleFile.TabIndex = 260
        Me.lblTitleFile.Text = "取込ファイル名"
        Me.lblTitleFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFile.TextValue = "取込ファイル名"
        Me.lblTitleFile.WidthDef = 105
        '
        'grpExcel
        '
        Me.grpExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpExcel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpExcel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpExcel.Controls.Add(Me.btnExcel)
        Me.grpExcel.Controls.Add(Me.lblTitleHokokuDate)
        Me.grpExcel.Controls.Add(Me.lblKara1)
        Me.grpExcel.Controls.Add(Me.imdHokokuDateFrom)
        Me.grpExcel.Controls.Add(Me.imdHokokuDateTo)
        Me.grpExcel.EnableStatus = False
        Me.grpExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpExcel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpExcel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpExcel.HeightDef = 45
        Me.grpExcel.Location = New System.Drawing.Point(285, 19)
        Me.grpExcel.Name = "grpExcel"
        Me.grpExcel.Size = New System.Drawing.Size(447, 45)
        Me.grpExcel.TabIndex = 279
        Me.grpExcel.TabStop = False
        Me.grpExcel.Text = "Excel出力"
        Me.grpExcel.TextValue = "Excel出力"
        Me.grpExcel.WidthDef = 447
        '
        'btnExcel
        '
        Me.btnExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnExcel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnExcel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnExcel.EnableStatus = True
        Me.btnExcel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExcel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExcel.HeightDef = 24
        Me.btnExcel.Location = New System.Drawing.Point(335, 15)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(100, 24)
        Me.btnExcel.TabIndex = 255
        Me.btnExcel.TabStopSetting = True
        Me.btnExcel.Text = "Excel出力"
        Me.btnExcel.TextValue = "Excel出力"
        Me.btnExcel.UseVisualStyleBackColor = True
        Me.btnExcel.WidthDef = 100
        '
        'lblTitleHokokuDate
        '
        Me.lblTitleHokokuDate.AutoSize = True
        Me.lblTitleHokokuDate.AutoSizeDef = True
        Me.lblTitleHokokuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokokuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokokuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokokuDate.EnableStatus = False
        Me.lblTitleHokokuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokokuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokokuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokokuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokokuDate.HeightDef = 13
        Me.lblTitleHokokuDate.Location = New System.Drawing.Point(22, 21)
        Me.lblTitleHokokuDate.Name = "lblTitleHokokuDate"
        Me.lblTitleHokokuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleHokokuDate.TabIndex = 254
        Me.lblTitleHokokuDate.Text = "報告日"
        Me.lblTitleHokokuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokokuDate.TextValue = "報告日"
        Me.lblTitleHokokuDate.WidthDef = 49
        '
        'lblKara1
        '
        Me.lblKara1.AutoSize = True
        Me.lblKara1.AutoSizeDef = True
        Me.lblKara1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKara1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKara1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKara1.EnableStatus = False
        Me.lblKara1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKara1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKara1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKara1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKara1.HeightDef = 13
        Me.lblKara1.Location = New System.Drawing.Point(184, 22)
        Me.lblKara1.Name = "lblKara1"
        Me.lblKara1.Size = New System.Drawing.Size(21, 13)
        Me.lblKara1.TabIndex = 13
        Me.lblKara1.Text = "～"
        Me.lblKara1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKara1.TextValue = "～"
        Me.lblKara1.WidthDef = 21
        '
        'imdHokokuDateFrom
        '
        Me.imdHokokuDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdHokokuDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdHokokuDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdHokokuDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdHokokuDateFrom.HeightDef = 18
        Me.imdHokokuDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdHokokuDateFrom.HissuLabelVisible = False
        Me.imdHokokuDateFrom.Holiday = True
        Me.imdHokokuDateFrom.IsAfterDateCheck = False
        Me.imdHokokuDateFrom.IsBeforeDateCheck = False
        Me.imdHokokuDateFrom.IsHissuCheck = False
        Me.imdHokokuDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdHokokuDateFrom.ItemName = ""
        Me.imdHokokuDateFrom.Location = New System.Drawing.Point(74, 19)
        Me.imdHokokuDateFrom.Name = "imdHokokuDateFrom"
        Me.imdHokokuDateFrom.Number = CType(0, Long)
        Me.imdHokokuDateFrom.ReadOnly = False
        Me.imdHokokuDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdHokokuDateFrom.TabIndex = 12
        Me.imdHokokuDateFrom.TabStopSetting = True
        Me.imdHokokuDateFrom.TextValue = ""
        Me.imdHokokuDateFrom.Value = New Date(CType(0, Long))
        Me.imdHokokuDateFrom.WidthDef = 118
        '
        'imdHokokuDateTo
        '
        Me.imdHokokuDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdHokokuDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdHokokuDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdHokokuDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdHokokuDateTo.HeightDef = 18
        Me.imdHokokuDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdHokokuDateTo.HissuLabelVisible = True
        Me.imdHokokuDateTo.Holiday = True
        Me.imdHokokuDateTo.IsAfterDateCheck = False
        Me.imdHokokuDateTo.IsBeforeDateCheck = False
        Me.imdHokokuDateTo.IsHissuCheck = True
        Me.imdHokokuDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdHokokuDateTo.ItemName = ""
        Me.imdHokokuDateTo.Location = New System.Drawing.Point(211, 19)
        Me.imdHokokuDateTo.Name = "imdHokokuDateTo"
        Me.imdHokokuDateTo.Number = CType(0, Long)
        Me.imdHokokuDateTo.ReadOnly = False
        Me.imdHokokuDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdHokokuDateTo.TabIndex = 14
        Me.imdHokokuDateTo.TabStopSetting = True
        Me.imdHokokuDateTo.TextValue = ""
        Me.imdHokokuDateTo.Value = New Date(CType(0, Long))
        Me.imdHokokuDateTo.WidthDef = 118
        '
        'txtSerialNoTo
        '
        Me.txtSerialNoTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNoTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNoTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerialNoTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerialNoTo.CountWrappedLine = False
        Me.txtSerialNoTo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerialNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNoTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNoTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNoTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNoTo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerialNoTo.HeightDef = 18
        Me.txtSerialNoTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerialNoTo.HissuLabelVisible = False
        Me.txtSerialNoTo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtSerialNoTo.IsByteCheck = 7
        Me.txtSerialNoTo.IsCalendarCheck = False
        Me.txtSerialNoTo.IsDakutenCheck = False
        Me.txtSerialNoTo.IsEisuCheck = False
        Me.txtSerialNoTo.IsForbiddenWordsCheck = False
        Me.txtSerialNoTo.IsFullByteCheck = 0
        Me.txtSerialNoTo.IsHankakuCheck = False
        Me.txtSerialNoTo.IsHissuCheck = False
        Me.txtSerialNoTo.IsKanaCheck = False
        Me.txtSerialNoTo.IsMiddleSpace = False
        Me.txtSerialNoTo.IsNumericCheck = False
        Me.txtSerialNoTo.IsSujiCheck = False
        Me.txtSerialNoTo.IsZenkakuCheck = False
        Me.txtSerialNoTo.ItemName = ""
        Me.txtSerialNoTo.LineSpace = 0
        Me.txtSerialNoTo.Location = New System.Drawing.Point(212, 136)
        Me.txtSerialNoTo.MaxLength = 7
        Me.txtSerialNoTo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerialNoTo.MaxLineCount = 0
        Me.txtSerialNoTo.Multiline = False
        Me.txtSerialNoTo.Name = "txtSerialNoTo"
        Me.txtSerialNoTo.ReadOnly = False
        Me.txtSerialNoTo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerialNoTo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerialNoTo.Size = New System.Drawing.Size(93, 18)
        Me.txtSerialNoTo.TabIndex = 278
        Me.txtSerialNoTo.TabStopSetting = True
        Me.txtSerialNoTo.TextValue = ""
        Me.txtSerialNoTo.UseSystemPasswordChar = False
        Me.txtSerialNoTo.WidthDef = 93
        Me.txtSerialNoTo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKara2
        '
        Me.lblKara2.AutoSize = True
        Me.lblKara2.AutoSizeDef = True
        Me.lblKara2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKara2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKara2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKara2.EnableStatus = False
        Me.lblKara2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKara2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKara2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKara2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKara2.HeightDef = 13
        Me.lblKara2.Location = New System.Drawing.Point(189, 139)
        Me.lblKara2.Name = "lblKara2"
        Me.lblKara2.Size = New System.Drawing.Size(21, 13)
        Me.lblKara2.TabIndex = 277
        Me.lblKara2.Text = "～"
        Me.lblKara2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKara2.TextValue = "～"
        Me.lblKara2.WidthDef = 21
        '
        'txtSerialNoFrom
        '
        Me.txtSerialNoFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNoFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNoFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerialNoFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerialNoFrom.CountWrappedLine = False
        Me.txtSerialNoFrom.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerialNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNoFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNoFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNoFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNoFrom.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerialNoFrom.HeightDef = 18
        Me.txtSerialNoFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerialNoFrom.HissuLabelVisible = False
        Me.txtSerialNoFrom.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtSerialNoFrom.IsByteCheck = 7
        Me.txtSerialNoFrom.IsCalendarCheck = False
        Me.txtSerialNoFrom.IsDakutenCheck = False
        Me.txtSerialNoFrom.IsEisuCheck = False
        Me.txtSerialNoFrom.IsForbiddenWordsCheck = False
        Me.txtSerialNoFrom.IsFullByteCheck = 0
        Me.txtSerialNoFrom.IsHankakuCheck = False
        Me.txtSerialNoFrom.IsHissuCheck = False
        Me.txtSerialNoFrom.IsKanaCheck = False
        Me.txtSerialNoFrom.IsMiddleSpace = False
        Me.txtSerialNoFrom.IsNumericCheck = False
        Me.txtSerialNoFrom.IsSujiCheck = False
        Me.txtSerialNoFrom.IsZenkakuCheck = False
        Me.txtSerialNoFrom.ItemName = ""
        Me.txtSerialNoFrom.LineSpace = 0
        Me.txtSerialNoFrom.Location = New System.Drawing.Point(104, 136)
        Me.txtSerialNoFrom.MaxLength = 7
        Me.txtSerialNoFrom.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerialNoFrom.MaxLineCount = 0
        Me.txtSerialNoFrom.Multiline = False
        Me.txtSerialNoFrom.Name = "txtSerialNoFrom"
        Me.txtSerialNoFrom.ReadOnly = False
        Me.txtSerialNoFrom.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerialNoFrom.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerialNoFrom.Size = New System.Drawing.Size(93, 18)
        Me.txtSerialNoFrom.TabIndex = 276
        Me.txtSerialNoFrom.TabStopSetting = True
        Me.txtSerialNoFrom.TextValue = ""
        Me.txtSerialNoFrom.UseSystemPasswordChar = False
        Me.txtSerialNoFrom.WidthDef = 93
        Me.txtSerialNoFrom.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSerialNo
        '
        Me.lblTitleSerialNo.AutoSize = True
        Me.lblTitleSerialNo.AutoSizeDef = True
        Me.lblTitleSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSerialNo.EnableStatus = False
        Me.lblTitleSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSerialNo.HeightDef = 13
        Me.lblTitleSerialNo.Location = New System.Drawing.Point(24, 139)
        Me.lblTitleSerialNo.Name = "lblTitleSerialNo"
        Me.lblTitleSerialNo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSerialNo.TabIndex = 275
        Me.lblTitleSerialNo.Text = "シリアル№"
        Me.lblTitleSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSerialNo.TextValue = "シリアル№"
        Me.lblTitleSerialNo.WidthDef = 77
        '
        'lblDestNm
        '
        Me.lblDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDestNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDestNm.CountWrappedLine = False
        Me.lblDestNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDestNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDestNm.HeightDef = 18
        Me.lblDestNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDestNm.HissuLabelVisible = False
        Me.lblDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestNm.IsByteCheck = 0
        Me.lblDestNm.IsCalendarCheck = False
        Me.lblDestNm.IsDakutenCheck = False
        Me.lblDestNm.IsEisuCheck = False
        Me.lblDestNm.IsForbiddenWordsCheck = False
        Me.lblDestNm.IsFullByteCheck = 0
        Me.lblDestNm.IsHankakuCheck = False
        Me.lblDestNm.IsHissuCheck = False
        Me.lblDestNm.IsKanaCheck = False
        Me.lblDestNm.IsMiddleSpace = False
        Me.lblDestNm.IsNumericCheck = False
        Me.lblDestNm.IsSujiCheck = False
        Me.lblDestNm.IsZenkakuCheck = False
        Me.lblDestNm.ItemName = ""
        Me.lblDestNm.LineSpace = 0
        Me.lblDestNm.Location = New System.Drawing.Point(255, 114)
        Me.lblDestNm.MaxLength = 0
        Me.lblDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestNm.MaxLineCount = 0
        Me.lblDestNm.Multiline = False
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.ReadOnly = True
        Me.lblDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestNm.Size = New System.Drawing.Size(465, 18)
        Me.lblDestNm.TabIndex = 274
        Me.lblDestNm.TabStop = False
        Me.lblDestNm.TabStopSetting = False
        Me.lblDestNm.TextValue = ""
        Me.lblDestNm.UseSystemPasswordChar = False
        Me.lblDestNm.WidthDef = 465
        Me.lblDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDestNm
        '
        Me.lblTitleDestNm.AutoSize = True
        Me.lblTitleDestNm.AutoSizeDef = True
        Me.lblTitleDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDestNm.EnableStatus = False
        Me.lblTitleDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestNm.HeightDef = 13
        Me.lblTitleDestNm.Location = New System.Drawing.Point(203, 118)
        Me.lblTitleDestNm.Name = "lblTitleDestNm"
        Me.lblTitleDestNm.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleDestNm.TabIndex = 273
        Me.lblTitleDestNm.Text = "届先名"
        Me.lblTitleDestNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDestNm.TextValue = "届先名"
        Me.lblTitleDestNm.WidthDef = 49
        '
        'txtOutkaNoL
        '
        Me.txtOutkaNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOutkaNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOutkaNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaNoL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaNoL.CountWrappedLine = False
        Me.txtOutkaNoL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaNoL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaNoL.HeightDef = 18
        Me.txtOutkaNoL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaNoL.HissuLabelVisible = True
        Me.txtOutkaNoL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtOutkaNoL.IsByteCheck = 9
        Me.txtOutkaNoL.IsCalendarCheck = False
        Me.txtOutkaNoL.IsDakutenCheck = False
        Me.txtOutkaNoL.IsEisuCheck = False
        Me.txtOutkaNoL.IsForbiddenWordsCheck = False
        Me.txtOutkaNoL.IsFullByteCheck = 0
        Me.txtOutkaNoL.IsHankakuCheck = False
        Me.txtOutkaNoL.IsHissuCheck = True
        Me.txtOutkaNoL.IsKanaCheck = False
        Me.txtOutkaNoL.IsMiddleSpace = False
        Me.txtOutkaNoL.IsNumericCheck = False
        Me.txtOutkaNoL.IsSujiCheck = False
        Me.txtOutkaNoL.IsZenkakuCheck = False
        Me.txtOutkaNoL.ItemName = ""
        Me.txtOutkaNoL.LineSpace = 0
        Me.txtOutkaNoL.Location = New System.Drawing.Point(104, 114)
        Me.txtOutkaNoL.MaxLength = 9
        Me.txtOutkaNoL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaNoL.MaxLineCount = 0
        Me.txtOutkaNoL.Multiline = False
        Me.txtOutkaNoL.Name = "txtOutkaNoL"
        Me.txtOutkaNoL.ReadOnly = False
        Me.txtOutkaNoL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaNoL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaNoL.Size = New System.Drawing.Size(93, 18)
        Me.txtOutkaNoL.TabIndex = 272
        Me.txtOutkaNoL.TabStopSetting = True
        Me.txtOutkaNoL.TextValue = ""
        Me.txtOutkaNoL.UseSystemPasswordChar = False
        Me.txtOutkaNoL.WidthDef = 93
        Me.txtOutkaNoL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOutokaNoL
        '
        Me.lblTitleOutokaNoL.AutoSize = True
        Me.lblTitleOutokaNoL.AutoSizeDef = True
        Me.lblTitleOutokaNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutokaNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOutokaNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOutokaNoL.EnableStatus = False
        Me.lblTitleOutokaNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutokaNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOutokaNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutokaNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOutokaNoL.HeightDef = 13
        Me.lblTitleOutokaNoL.Location = New System.Drawing.Point(10, 118)
        Me.lblTitleOutokaNoL.Name = "lblTitleOutokaNoL"
        Me.lblTitleOutokaNoL.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleOutokaNoL.TabIndex = 271
        Me.lblTitleOutokaNoL.Text = "出荷管理番号"
        Me.lblTitleOutokaNoL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOutokaNoL.TextValue = "出荷管理番号"
        Me.lblTitleOutokaNoL.WidthDef = 91
        '
        'lblCustNM
        '
        Me.lblCustNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM.CountWrappedLine = False
        Me.lblCustNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM.HeightDef = 18
        Me.lblCustNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.HissuLabelVisible = False
        Me.lblCustNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM.IsByteCheck = 0
        Me.lblCustNM.IsCalendarCheck = False
        Me.lblCustNM.IsDakutenCheck = False
        Me.lblCustNM.IsEisuCheck = False
        Me.lblCustNM.IsForbiddenWordsCheck = False
        Me.lblCustNM.IsFullByteCheck = 0
        Me.lblCustNM.IsHankakuCheck = False
        Me.lblCustNM.IsHissuCheck = False
        Me.lblCustNM.IsKanaCheck = False
        Me.lblCustNM.IsMiddleSpace = False
        Me.lblCustNM.IsNumericCheck = False
        Me.lblCustNM.IsSujiCheck = False
        Me.lblCustNM.IsZenkakuCheck = False
        Me.lblCustNM.ItemName = ""
        Me.lblCustNM.LineSpace = 0
        Me.lblCustNM.Location = New System.Drawing.Point(181, 92)
        Me.lblCustNM.MaxLength = 0
        Me.lblCustNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM.MaxLineCount = 0
        Me.lblCustNM.Multiline = False
        Me.lblCustNM.Name = "lblCustNM"
        Me.lblCustNM.ReadOnly = True
        Me.lblCustNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM.Size = New System.Drawing.Size(465, 18)
        Me.lblCustNM.TabIndex = 270
        Me.lblCustNM.TabStop = False
        Me.lblCustNM.TabStopSetting = False
        Me.lblCustNM.TextValue = ""
        Me.lblCustNM.UseSystemPasswordChar = False
        Me.lblCustNM.WidthDef = 465
        Me.lblCustNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCD
        '
        Me.txtCustCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD.CountWrappedLine = False
        Me.txtCustCD.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD.HeightDef = 18
        Me.txtCustCD.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD.HissuLabelVisible = False
        Me.txtCustCD.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD.IsByteCheck = 5
        Me.txtCustCD.IsCalendarCheck = False
        Me.txtCustCD.IsDakutenCheck = False
        Me.txtCustCD.IsEisuCheck = False
        Me.txtCustCD.IsForbiddenWordsCheck = False
        Me.txtCustCD.IsFullByteCheck = 0
        Me.txtCustCD.IsHankakuCheck = False
        Me.txtCustCD.IsHissuCheck = False
        Me.txtCustCD.IsKanaCheck = False
        Me.txtCustCD.IsMiddleSpace = False
        Me.txtCustCD.IsNumericCheck = False
        Me.txtCustCD.IsSujiCheck = False
        Me.txtCustCD.IsZenkakuCheck = False
        Me.txtCustCD.ItemName = ""
        Me.txtCustCD.LineSpace = 0
        Me.txtCustCD.Location = New System.Drawing.Point(104, 92)
        Me.txtCustCD.MaxLength = 5
        Me.txtCustCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD.MaxLineCount = 0
        Me.txtCustCD.Multiline = False
        Me.txtCustCD.Name = "txtCustCD"
        Me.txtCustCD.ReadOnly = True
        Me.txtCustCD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD.Size = New System.Drawing.Size(93, 18)
        Me.txtCustCD.TabIndex = 269
        Me.txtCustCD.TabStop = False
        Me.txtCustCD.TabStopSetting = False
        Me.txtCustCD.TextValue = ""
        Me.txtCustCD.UseSystemPasswordChar = False
        Me.txtCustCD.WidthDef = 93
        Me.txtCustCD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(66, 95)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 268
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
        '
        'grpMode
        '
        Me.grpMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMode.Controls.Add(Me.optTorikeshi)
        Me.grpMode.Controls.Add(Me.optKaishu)
        Me.grpMode.Controls.Add(Me.optShukka)
        Me.grpMode.EnableStatus = False
        Me.grpMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.HeightDef = 45
        Me.grpMode.Location = New System.Drawing.Point(33, 19)
        Me.grpMode.Name = "grpMode"
        Me.grpMode.Size = New System.Drawing.Size(227, 45)
        Me.grpMode.TabIndex = 262
        Me.grpMode.TabStop = False
        Me.grpMode.Text = "出荷／回収"
        Me.grpMode.TextValue = "出荷／回収"
        Me.grpMode.WidthDef = 227
        '
        'optTorikeshi
        '
        Me.optTorikeshi.AutoSize = True
        Me.optTorikeshi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optTorikeshi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optTorikeshi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optTorikeshi.Enabled = False
        Me.optTorikeshi.EnableStatus = True
        Me.optTorikeshi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optTorikeshi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optTorikeshi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTorikeshi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTorikeshi.HeightDef = 17
        Me.optTorikeshi.Location = New System.Drawing.Point(159, 20)
        Me.optTorikeshi.Name = "optTorikeshi"
        Me.optTorikeshi.Size = New System.Drawing.Size(53, 17)
        Me.optTorikeshi.TabIndex = 316
        Me.optTorikeshi.TabStopSetting = False
        Me.optTorikeshi.Text = "取消"
        Me.optTorikeshi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optTorikeshi.TextValue = "取消"
        Me.optTorikeshi.UseVisualStyleBackColor = True
        Me.optTorikeshi.WidthDef = 53
        '
        'optKaishu
        '
        Me.optKaishu.AutoSize = True
        Me.optKaishu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKaishu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optKaishu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optKaishu.Enabled = False
        Me.optKaishu.EnableStatus = True
        Me.optKaishu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKaishu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optKaishu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKaishu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optKaishu.HeightDef = 17
        Me.optKaishu.Location = New System.Drawing.Point(91, 20)
        Me.optKaishu.Name = "optKaishu"
        Me.optKaishu.Size = New System.Drawing.Size(53, 17)
        Me.optKaishu.TabIndex = 315
        Me.optKaishu.TabStopSetting = False
        Me.optKaishu.Text = "回収"
        Me.optKaishu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optKaishu.TextValue = "回収"
        Me.optKaishu.UseVisualStyleBackColor = True
        Me.optKaishu.WidthDef = 53
        '
        'optShukka
        '
        Me.optShukka.AutoSize = True
        Me.optShukka.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optShukka.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optShukka.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optShukka.Enabled = False
        Me.optShukka.EnableStatus = True
        Me.optShukka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optShukka.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optShukka.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optShukka.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optShukka.HeightDef = 17
        Me.optShukka.Location = New System.Drawing.Point(22, 20)
        Me.optShukka.Name = "optShukka"
        Me.optShukka.Size = New System.Drawing.Size(53, 17)
        Me.optShukka.TabIndex = 314
        Me.optShukka.TabStopSetting = False
        Me.optShukka.Text = "出荷"
        Me.optShukka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optShukka.TextValue = "出荷"
        Me.optShukka.UseVisualStyleBackColor = True
        Me.optShukka.WidthDef = 53
        '
        'lblTitleKaishuDate
        '
        Me.lblTitleKaishuDate.AutoSize = True
        Me.lblTitleKaishuDate.AutoSizeDef = True
        Me.lblTitleKaishuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKaishuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKaishuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKaishuDate.EnableStatus = False
        Me.lblTitleKaishuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKaishuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKaishuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKaishuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKaishuDate.HeightDef = 13
        Me.lblTitleKaishuDate.Location = New System.Drawing.Point(52, 161)
        Me.lblTitleKaishuDate.Name = "lblTitleKaishuDate"
        Me.lblTitleKaishuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleKaishuDate.TabIndex = 253
        Me.lblTitleKaishuDate.Text = "回収日"
        Me.lblTitleKaishuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKaishuDate.TextValue = "回収日"
        Me.lblTitleKaishuDate.WidthDef = 49
        '
        'imdKaishuDate
        '
        Me.imdKaishuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdKaishuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdKaishuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdKaishuDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdKaishuDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdKaishuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdKaishuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdKaishuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdKaishuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdKaishuDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdKaishuDate.HeightDef = 18
        Me.imdKaishuDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdKaishuDate.HissuLabelVisible = True
        Me.imdKaishuDate.Holiday = True
        Me.imdKaishuDate.IsAfterDateCheck = False
        Me.imdKaishuDate.IsBeforeDateCheck = False
        Me.imdKaishuDate.IsHissuCheck = True
        Me.imdKaishuDate.IsMinDateCheck = "1900/01/01"
        Me.imdKaishuDate.ItemName = ""
        Me.imdKaishuDate.Location = New System.Drawing.Point(104, 158)
        Me.imdKaishuDate.Name = "imdKaishuDate"
        Me.imdKaishuDate.Number = CType(0, Long)
        Me.imdKaishuDate.ReadOnly = False
        Me.imdKaishuDate.Size = New System.Drawing.Size(118, 18)
        Me.imdKaishuDate.TabIndex = 252
        Me.imdKaishuDate.TabStopSetting = True
        Me.imdKaishuDate.TextValue = ""
        Me.imdKaishuDate.Value = New Date(CType(0, Long))
        Me.imdKaishuDate.WidthDef = 118
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
        Me.cmbEigyo.Location = New System.Drawing.Point(104, 70)
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(52, 72)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'sprDetails
        '
        Me.sprDetails.AccessibleDescription = ""
        Me.sprDetails.AllowUserZoom = False
        Me.sprDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetails.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetails.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetails.CellClickEventArgs = Nothing
        Me.sprDetails.CheckToCheckBox = True
        Me.sprDetails.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetails.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetails.EditModeReplace = True
        Me.sprDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetails.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetails.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetails.HeightDef = 528
        Me.sprDetails.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetails.KeyboardCheckBoxOn = False
        Me.sprDetails.Location = New System.Drawing.Point(12, 348)
        Me.sprDetails.Name = "sprDetails"
        Me.sprDetails.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        Me.sprDetails.Size = New System.Drawing.Size(1250, 528)
        Me.sprDetails.SortColumn = True
        Me.sprDetails.SpanColumnLock = True
        Me.sprDetails.SpreadDoubleClicked = False
        Me.sprDetails.TabIndex = 263
        Me.sprDetails.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetails.TextValue = Nothing
        Me.sprDetails.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetails.WidthDef = 1250
        '
        'btnRowDel
        '
        Me.btnRowDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowDel.EnableStatus = True
        Me.btnRowDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowDel.HeightDef = 22
        Me.btnRowDel.Location = New System.Drawing.Point(89, 320)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 457
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
        '
        'btnRowAdd
        '
        Me.btnRowAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRowAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnRowAdd.EnableStatus = True
        Me.btnRowAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnRowAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRowAdd.HeightDef = 22
        Me.btnRowAdd.Location = New System.Drawing.Point(13, 320)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 456
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 70
        '
        'LMI180F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI180F"
        Me.Text = "【LMI180】   NRC出荷／回収情報入力"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpShukkaKaishu.ResumeLayout(False)
        Me.grpShukkaKaishu.PerformLayout()
        Me.grpTorikomi.ResumeLayout(False)
        Me.grpTorikomi.PerformLayout()
        Me.grpExcel.ResumeLayout(False)
        Me.grpExcel.PerformLayout()
        Me.grpMode.ResumeLayout(False)
        Me.grpMode.PerformLayout()
        CType(Me.sprDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpShukkaKaishu As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleKaishuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdKaishuDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents grpMode As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents optKaishu As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optShukka As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optTorikeshi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblCustNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOutkaNoL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOutokaNoL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDestNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSerialNoFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSerialNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKara2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSerialNoTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents grpExcel As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleHokokuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKara1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdHokokuDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdHokokuDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents btnExcel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents grpTorikomi As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtPath As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleFile As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetails As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents txtFileTextBox As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtOutkaNoLOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnSelect As Jp.Co.Nrs.LM.GUI.Win.LMButton

End Class
