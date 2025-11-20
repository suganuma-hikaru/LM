<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMR010F
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
        Dim sprKanryo_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprKanryo_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.lblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblNyukaDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdNyukaDate_From = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdNyukaDate_To = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblKanryo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtTantoCD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTanto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTantoNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.sprKanryo = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.CmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbKanryo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        sprKanryo_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprKanryo_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprKanryo_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprKanryo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbKanryo)
        Me.pnlViewAria.Controls.Add(Me.CmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.sprKanryo)
        Me.pnlViewAria.Controls.Add(Me.lblTantoNM)
        Me.pnlViewAria.Controls.Add(Me.txtTantoCD)
        Me.pnlViewAria.Controls.Add(Me.lblTanto)
        Me.pnlViewAria.Controls.Add(Me.lblKanryo)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM)
        Me.pnlViewAria.Controls.Add(Me.imdNyukaDate_To)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel5)
        Me.pnlViewAria.Controls.Add(Me.imdNyukaDate_From)
        Me.pnlViewAria.Controls.Add(Me.lblNyukaDate)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD)
        Me.pnlViewAria.Controls.Add(Me.lblCust)
        Me.pnlViewAria.Controls.Add(Me.lblEigyo)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'lblEigyo
        '
        Me.lblEigyo.AutoSizeDef = False
        Me.lblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblEigyo.EnableStatus = False
        Me.lblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.HeightDef = 13
        Me.lblEigyo.Location = New System.Drawing.Point(19, 24)
        Me.lblEigyo.Name = "lblEigyo"
        Me.lblEigyo.Size = New System.Drawing.Size(67, 13)
        Me.lblEigyo.TabIndex = 3
        Me.lblEigyo.Text = "営業所"
        Me.lblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEigyo.TextValue = "営業所"
        Me.lblEigyo.WidthDef = 67
        '
        'lblCust
        '
        Me.lblCust.AutoSizeDef = False
        Me.lblCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCust.EnableStatus = False
        Me.lblCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.HeightDef = 13
        Me.lblCust.Location = New System.Drawing.Point(16, 46)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.Size = New System.Drawing.Size(70, 13)
        Me.lblCust.TabIndex = 5
        Me.lblCust.Text = "荷主"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCust.TextValue = "荷主"
        Me.lblCust.WidthDef = 70
        '
        'txtCustCD
        '
        Me.txtCustCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.txtCustCD.Location = New System.Drawing.Point(92, 43)
        Me.txtCustCD.MaxLength = 5
        Me.txtCustCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD.MaxLineCount = 0
        Me.txtCustCD.Multiline = False
        Me.txtCustCD.Name = "txtCustCD"
        Me.txtCustCD.ReadOnly = False
        Me.txtCustCD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD.Size = New System.Drawing.Size(93, 18)
        Me.txtCustCD.TabIndex = 6
        Me.txtCustCD.TabStopSetting = True
        Me.txtCustCD.TextValue = ""
        Me.txtCustCD.UseSystemPasswordChar = False
        Me.txtCustCD.WidthDef = 93
        Me.txtCustCD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCustNM.Location = New System.Drawing.Point(169, 43)
        Me.lblCustNM.MaxLength = 0
        Me.lblCustNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM.MaxLineCount = 0
        Me.lblCustNM.Multiline = False
        Me.lblCustNM.Name = "lblCustNM"
        Me.lblCustNM.ReadOnly = True
        Me.lblCustNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM.Size = New System.Drawing.Size(543, 18)
        Me.lblCustNM.TabIndex = 7
        Me.lblCustNM.TabStop = False
        Me.lblCustNM.TabStopSetting = False
        Me.lblCustNM.TextValue = ""
        Me.lblCustNM.UseSystemPasswordChar = False
        Me.lblCustNM.WidthDef = 543
        Me.lblCustNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblNyukaDate
        '
        Me.lblNyukaDate.AutoSizeDef = False
        Me.lblNyukaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNyukaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNyukaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblNyukaDate.EnableStatus = False
        Me.lblNyukaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNyukaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNyukaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNyukaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNyukaDate.HeightDef = 13
        Me.lblNyukaDate.Location = New System.Drawing.Point(362, 67)
        Me.lblNyukaDate.Name = "lblNyukaDate"
        Me.lblNyukaDate.Size = New System.Drawing.Size(102, 13)
        Me.lblNyukaDate.TabIndex = 8
        Me.lblNyukaDate.Text = "入出荷/作業日"
        Me.lblNyukaDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNyukaDate.TextValue = "入出荷/作業日"
        Me.lblNyukaDate.WidthDef = 102
        '
        'imdNyukaDate_From
        '
        Me.imdNyukaDate_From.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_From.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_From.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate_From.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdNyukaDate_From.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate_From.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_From.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_From.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_From.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_From.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate_From.HeightDef = 18
        Me.imdNyukaDate_From.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate_From.HissuLabelVisible = False
        Me.imdNyukaDate_From.Holiday = True
        Me.imdNyukaDate_From.IsAfterDateCheck = False
        Me.imdNyukaDate_From.IsBeforeDateCheck = False
        Me.imdNyukaDate_From.IsHissuCheck = False
        Me.imdNyukaDate_From.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate_From.ItemName = ""
        Me.imdNyukaDate_From.Location = New System.Drawing.Point(467, 64)
        Me.imdNyukaDate_From.Name = "imdNyukaDate_From"
        Me.imdNyukaDate_From.Number = CType(10101000000, Long)
        Me.imdNyukaDate_From.ReadOnly = False
        Me.imdNyukaDate_From.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate_From.TabIndex = 9
        Me.imdNyukaDate_From.TabStopSetting = True
        Me.imdNyukaDate_From.TextValue = ""
        Me.imdNyukaDate_From.Value = New Date(CType(0, Long))
        Me.imdNyukaDate_From.WidthDef = 118
        '
        'LmTitleLabel5
        '
        Me.LmTitleLabel5.AutoSize = True
        Me.LmTitleLabel5.AutoSizeDef = True
        Me.LmTitleLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel5.EnableStatus = False
        Me.LmTitleLabel5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.HeightDef = 13
        Me.LmTitleLabel5.Location = New System.Drawing.Point(573, 67)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel5.TabIndex = 10
        Me.LmTitleLabel5.Text = "～"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "～"
        Me.LmTitleLabel5.WidthDef = 21
        '
        'imdNyukaDate_To
        '
        Me.imdNyukaDate_To.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_To.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_To.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate_To.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdNyukaDate_To.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate_To.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_To.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_To.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_To.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_To.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate_To.HeightDef = 18
        Me.imdNyukaDate_To.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate_To.HissuLabelVisible = False
        Me.imdNyukaDate_To.Holiday = True
        Me.imdNyukaDate_To.IsAfterDateCheck = False
        Me.imdNyukaDate_To.IsBeforeDateCheck = False
        Me.imdNyukaDate_To.IsHissuCheck = False
        Me.imdNyukaDate_To.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate_To.ItemName = ""
        Me.imdNyukaDate_To.Location = New System.Drawing.Point(594, 64)
        Me.imdNyukaDate_To.Name = "imdNyukaDate_To"
        Me.imdNyukaDate_To.Number = CType(10101000000, Long)
        Me.imdNyukaDate_To.ReadOnly = False
        Me.imdNyukaDate_To.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate_To.TabIndex = 11
        Me.imdNyukaDate_To.TabStopSetting = True
        Me.imdNyukaDate_To.TextValue = ""
        Me.imdNyukaDate_To.Value = New Date(CType(0, Long))
        Me.imdNyukaDate_To.WidthDef = 118
        '
        'lblKanryo
        '
        Me.lblKanryo.AutoSizeDef = False
        Me.lblKanryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKanryo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKanryo.EnableStatus = False
        Me.lblKanryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKanryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKanryo.HeightDef = 13
        Me.lblKanryo.Location = New System.Drawing.Point(3, 67)
        Me.lblKanryo.Name = "lblKanryo"
        Me.lblKanryo.Size = New System.Drawing.Size(83, 13)
        Me.lblKanryo.TabIndex = 246
        Me.lblKanryo.Text = "完了種別"
        Me.lblKanryo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKanryo.TextValue = "完了種別"
        Me.lblKanryo.WidthDef = 83
        '
        'txtTantoCD
        '
        Me.txtTantoCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTantoCD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTantoCD.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTantoCD.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTantoCD.CountWrappedLine = False
        Me.txtTantoCD.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTantoCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoCD.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTantoCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTantoCD.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTantoCD.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTantoCD.HeightDef = 18
        Me.txtTantoCD.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTantoCD.HissuLabelVisible = False
        Me.txtTantoCD.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtTantoCD.IsByteCheck = 5
        Me.txtTantoCD.IsCalendarCheck = False
        Me.txtTantoCD.IsDakutenCheck = False
        Me.txtTantoCD.IsEisuCheck = False
        Me.txtTantoCD.IsForbiddenWordsCheck = False
        Me.txtTantoCD.IsFullByteCheck = 0
        Me.txtTantoCD.IsHankakuCheck = False
        Me.txtTantoCD.IsHissuCheck = False
        Me.txtTantoCD.IsKanaCheck = False
        Me.txtTantoCD.IsMiddleSpace = False
        Me.txtTantoCD.IsNumericCheck = False
        Me.txtTantoCD.IsSujiCheck = False
        Me.txtTantoCD.IsZenkakuCheck = False
        Me.txtTantoCD.ItemName = ""
        Me.txtTantoCD.LineSpace = 0
        Me.txtTantoCD.Location = New System.Drawing.Point(467, 22)
        Me.txtTantoCD.MaxLength = 5
        Me.txtTantoCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTantoCD.MaxLineCount = 0
        Me.txtTantoCD.Multiline = False
        Me.txtTantoCD.Name = "txtTantoCD"
        Me.txtTantoCD.ReadOnly = False
        Me.txtTantoCD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTantoCD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTantoCD.Size = New System.Drawing.Size(93, 18)
        Me.txtTantoCD.TabIndex = 248
        Me.txtTantoCD.TabStopSetting = True
        Me.txtTantoCD.TextValue = ""
        Me.txtTantoCD.UseSystemPasswordChar = False
        Me.txtTantoCD.WidthDef = 93
        Me.txtTantoCD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTanto
        '
        Me.lblTanto.AutoSizeDef = False
        Me.lblTanto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTanto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTanto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTanto.EnableStatus = False
        Me.lblTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTanto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTanto.HeightDef = 13
        Me.lblTanto.Location = New System.Drawing.Point(401, 24)
        Me.lblTanto.Name = "lblTanto"
        Me.lblTanto.Size = New System.Drawing.Size(49, 13)
        Me.lblTanto.TabIndex = 247
        Me.lblTanto.Text = "担当者"
        Me.lblTanto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTanto.TextValue = "担当者"
        Me.lblTanto.WidthDef = 49
        '
        'lblTantoNM
        '
        Me.lblTantoNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantoNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantoNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTantoNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTantoNM.CountWrappedLine = False
        Me.lblTantoNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTantoNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantoNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTantoNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTantoNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTantoNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTantoNM.HeightDef = 18
        Me.lblTantoNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTantoNM.HissuLabelVisible = False
        Me.lblTantoNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTantoNM.IsByteCheck = 0
        Me.lblTantoNM.IsCalendarCheck = False
        Me.lblTantoNM.IsDakutenCheck = False
        Me.lblTantoNM.IsEisuCheck = False
        Me.lblTantoNM.IsForbiddenWordsCheck = False
        Me.lblTantoNM.IsFullByteCheck = 0
        Me.lblTantoNM.IsHankakuCheck = False
        Me.lblTantoNM.IsHissuCheck = False
        Me.lblTantoNM.IsKanaCheck = False
        Me.lblTantoNM.IsMiddleSpace = False
        Me.lblTantoNM.IsNumericCheck = False
        Me.lblTantoNM.IsSujiCheck = False
        Me.lblTantoNM.IsZenkakuCheck = False
        Me.lblTantoNM.ItemName = ""
        Me.lblTantoNM.LineSpace = 0
        Me.lblTantoNM.Location = New System.Drawing.Point(544, 22)
        Me.lblTantoNM.MaxLength = 0
        Me.lblTantoNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTantoNM.MaxLineCount = 0
        Me.lblTantoNM.Multiline = False
        Me.lblTantoNM.Name = "lblTantoNM"
        Me.lblTantoNM.ReadOnly = True
        Me.lblTantoNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTantoNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTantoNM.Size = New System.Drawing.Size(250, 18)
        Me.lblTantoNM.TabIndex = 251
        Me.lblTantoNM.TabStop = False
        Me.lblTantoNM.TabStopSetting = False
        Me.lblTantoNM.TextValue = ""
        Me.lblTantoNM.UseSystemPasswordChar = False
        Me.lblTantoNM.WidthDef = 250
        Me.lblTantoNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'sprKanryo
        '
        Me.sprKanryo.AccessibleDescription = ""
        Me.sprKanryo.AllowUserZoom = False
        Me.sprKanryo.AutoImeMode = False
        Me.sprKanryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprKanryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprKanryo.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprKanryo.CellClickEventArgs = Nothing
        Me.sprKanryo.CheckToCheckBox = True
        Me.sprKanryo.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprKanryo.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprKanryo.EditModeReplace = True
        Me.sprKanryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprKanryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprKanryo.ForeColorDef = System.Drawing.Color.Empty
        Me.sprKanryo.HeightDef = 762
        Me.sprKanryo.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprKanryo.KeyboardCheckBoxOn = False
        Me.sprKanryo.Location = New System.Drawing.Point(16, 103)
        Me.sprKanryo.Name = "sprKanryo"
        Me.sprKanryo.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprKanryo.Size = New System.Drawing.Size(1239, 762)
        Me.sprKanryo.SortColumn = True
        Me.sprKanryo.SpanColumnLock = True
        Me.sprKanryo.SpreadDoubleClicked = False
        Me.sprKanryo.TabIndex = 252
        Me.sprKanryo.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprKanryo.TextValue = Nothing
        Me.sprKanryo.UseGrouping = False
        Me.sprKanryo.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprKanryo.WidthDef = 1239
        sprKanryo_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprKanryo_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprKanryo.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprKanryo_InputMapWhenFocusedNormal)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprKanryo_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprKanryo.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprKanryo_InputMapWhenAncestorOfFocusedNormal)
        Me.sprKanryo.SetViewportTopRow(0, 0, 1)
        Me.sprKanryo.SetActiveViewport(0, -1, 0)
        '
        'CmbEigyo
        '
        Me.CmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CmbEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CmbEigyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmbEigyo.DataSource = Nothing
        Me.CmbEigyo.DisplayMember = Nothing
        Me.CmbEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CmbEigyo.HeightDef = 18
        Me.CmbEigyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.CmbEigyo.HissuLabelVisible = True
        Me.CmbEigyo.InsertWildCard = True
        Me.CmbEigyo.IsForbiddenWordsCheck = False
        Me.CmbEigyo.IsHissuCheck = True
        Me.CmbEigyo.ItemName = ""
        Me.CmbEigyo.Location = New System.Drawing.Point(92, 22)
        Me.CmbEigyo.Name = "CmbEigyo"
        Me.CmbEigyo.ReadOnly = False
        Me.CmbEigyo.SelectedIndex = -1
        Me.CmbEigyo.SelectedItem = Nothing
        Me.CmbEigyo.SelectedText = ""
        Me.CmbEigyo.SelectedValue = ""
        Me.CmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.CmbEigyo.TabIndex = 17
        Me.CmbEigyo.TabStopSetting = True
        Me.CmbEigyo.TextValue = ""
        Me.CmbEigyo.ValueMember = Nothing
        Me.CmbEigyo.WidthDef = 300
        '
        'cmbKanryo
        '
        Me.cmbKanryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKanryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKanryo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKanryo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKanryo.DataCode = "K014"
        Me.cmbKanryo.DataSource = Nothing
        Me.cmbKanryo.DisplayMember = Nothing
        Me.cmbKanryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKanryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKanryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKanryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKanryo.HeightDef = 18
        Me.cmbKanryo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKanryo.HissuLabelVisible = True
        Me.cmbKanryo.InsertWildCard = True
        Me.cmbKanryo.IsForbiddenWordsCheck = False
        Me.cmbKanryo.IsHissuCheck = True
        Me.cmbKanryo.ItemName = ""
        Me.cmbKanryo.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbKanryo.Location = New System.Drawing.Point(92, 64)
        Me.cmbKanryo.Name = "cmbKanryo"
        Me.cmbKanryo.ReadOnly = False
        Me.cmbKanryo.SelectedIndex = -1
        Me.cmbKanryo.SelectedItem = Nothing
        Me.cmbKanryo.SelectedText = ""
        Me.cmbKanryo.SelectedValue = ""
        Me.cmbKanryo.Size = New System.Drawing.Size(164, 18)
        Me.cmbKanryo.TabIndex = 253
        Me.cmbKanryo.TabStopSetting = True
        Me.cmbKanryo.TextValue = ""
        Me.cmbKanryo.Value1 = Nothing
        Me.cmbKanryo.Value2 = Nothing
        Me.cmbKanryo.Value3 = Nothing
        Me.cmbKanryo.ValueMember = Nothing
        Me.cmbKanryo.WidthDef = 164
        '
        'LMR010F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMR010F"
        Me.Text = "【LMR010】 完了取込"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprKanryo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imdNyukaDate_To As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdNyukaDate_From As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblNyukaDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKanryo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtTantoCD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTanto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTantoNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents sprKanryo As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents CmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbKanryo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun

End Class
