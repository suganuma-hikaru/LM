<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI110F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMI110F))
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
        Me.sprNichikoGoods = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleFromTo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdEdiDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleEdiDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdEdiDateFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.pnlNichikoGoods = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleImportKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkImport = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkNotImport = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.txtSerchGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtSerchGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmGroupBox1 = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.txtGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsKey = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoodsKey = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsNm1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoodsNm1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsNm2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoodsNm2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtUpGroupCd1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtGoodsCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtGoodsCustNmSS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtGoodsCustNmS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmGroupBox2 = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbSkyuMeiYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtGoodsCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblSkyuMeiYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbHikiateAlertYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtGoodsCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleHikiateAlertYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbCrtDateCtlKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtGoodsCustCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCrtDateCtlKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtGoodsCustCdSS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbCoaYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleCoaYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbSpNhsYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleSpNhsYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleUpGroupCd1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleLotCtlKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbLotCtlKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleTareYn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbTareYn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprNichikoGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNichikoGoods.SuspendLayout()
        Me.LmGroupBox1.SuspendLayout()
        Me.LmGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.AutoSize = True
        Me.pnlViewAria.Controls.Add(Me.txtCustNm)
        Me.pnlViewAria.Controls.Add(Me.LmGroupBox1)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCust)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.pnlNichikoGoods)
        '
        'sprNichikoGoods
        '
        Me.sprNichikoGoods.AccessibleDescription = ""
        Me.sprNichikoGoods.AllowUserZoom = False
        Me.sprNichikoGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprNichikoGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprNichikoGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprNichikoGoods.CellClickEventArgs = Nothing
        Me.sprNichikoGoods.CheckToCheckBox = True
        Me.sprNichikoGoods.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprNichikoGoods.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprNichikoGoods.EditModeReplace = True
        Me.sprNichikoGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprNichikoGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprNichikoGoods.ForeColorDef = System.Drawing.Color.Empty
        Me.sprNichikoGoods.HeightDef = 454
        Me.sprNichikoGoods.KeyboardCheckBoxOn = False
        Me.sprNichikoGoods.Location = New System.Drawing.Point(15, 44)
        Me.sprNichikoGoods.Name = "sprNichikoGoods"
        Me.sprNichikoGoods.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprNichikoGoods.SetViewportTopRow(0, 0, 1)
        Me.sprNichikoGoods.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        'RowCount = 1
        Me.sprNichikoGoods.SetViewportTopRow(0, 0, 1)
        Me.sprNichikoGoods.SetActiveViewport(0, -1, 0)
        Me.sprNichikoGoods.Size = New System.Drawing.Size(1218, 454)
        Me.sprNichikoGoods.SortColumn = True
        Me.sprNichikoGoods.SpanColumnLock = True
        Me.sprNichikoGoods.SpreadDoubleClicked = False
        Me.sprNichikoGoods.TabIndex = 15
        Me.sprNichikoGoods.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprNichikoGoods.TextValue = Nothing
        Me.sprNichikoGoods.WidthDef = 1218
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(58, 17)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 535
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdL.Location = New System.Drawing.Point(479, 15)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = True
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(66, 18)
        Me.txtCustCdL.TabIndex = 538
        Me.txtCustCdL.TabStop = False
        Me.txtCustCdL.TabStopSetting = False
        Me.txtCustCdL.Tag = ""
        Me.txtCustCdL.TextValue = "X10XX"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 66
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(441, 17)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 537
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdM.Location = New System.Drawing.Point(529, 15)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = True
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(43, 18)
        Me.txtCustCdM.TabIndex = 539
        Me.txtCustCdM.TabStop = False
        Me.txtCustCdM.TabStopSetting = False
        Me.txtCustCdM.Tag = ""
        Me.txtCustCdM.TextValue = "X1"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 43
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleFromTo
        '
        Me.lblTitleFromTo.AutoSize = True
        Me.lblTitleFromTo.AutoSizeDef = True
        Me.lblTitleFromTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFromTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFromTo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFromTo.EnableStatus = False
        Me.lblTitleFromTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFromTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFromTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFromTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFromTo.HeightDef = 13
        Me.lblTitleFromTo.Location = New System.Drawing.Point(255, 22)
        Me.lblTitleFromTo.Name = "lblTitleFromTo"
        Me.lblTitleFromTo.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleFromTo.TabIndex = 544
        Me.lblTitleFromTo.Text = "～"
        Me.lblTitleFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFromTo.TextValue = "～"
        Me.lblTitleFromTo.WidthDef = 21
        '
        'imdEdiDateTo
        '
        Me.imdEdiDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdEdiDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdEdiDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdEdiDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdEdiDateTo.HeightDef = 18
        Me.imdEdiDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdEdiDateTo.HissuLabelVisible = False
        Me.imdEdiDateTo.Holiday = True
        Me.imdEdiDateTo.IsAfterDateCheck = False
        Me.imdEdiDateTo.IsBeforeDateCheck = False
        Me.imdEdiDateTo.IsHissuCheck = False
        Me.imdEdiDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdEdiDateTo.ItemName = ""
        Me.imdEdiDateTo.Location = New System.Drawing.Point(282, 19)
        Me.imdEdiDateTo.Name = "imdEdiDateTo"
        Me.imdEdiDateTo.Number = CType(10101000000, Long)
        Me.imdEdiDateTo.ReadOnly = False
        Me.imdEdiDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdEdiDateTo.TabIndex = 543
        Me.imdEdiDateTo.TabStopSetting = True
        Me.imdEdiDateTo.TextValue = ""
        Me.imdEdiDateTo.Value = New Date(CType(0, Long))
        Me.imdEdiDateTo.WidthDef = 118
        '
        'lblTitleEdiDate
        '
        Me.lblTitleEdiDate.AutoSize = True
        Me.lblTitleEdiDate.AutoSizeDef = True
        Me.lblTitleEdiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEdiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEdiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEdiDate.EnableStatus = False
        Me.lblTitleEdiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEdiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEdiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEdiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEdiDate.HeightDef = 13
        Me.lblTitleEdiDate.Location = New System.Drawing.Point(63, 22)
        Me.lblTitleEdiDate.Name = "lblTitleEdiDate"
        Me.lblTitleEdiDate.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleEdiDate.TabIndex = 542
        Me.lblTitleEdiDate.Text = "EDI取込日"
        Me.lblTitleEdiDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEdiDate.TextValue = "EDI取込日"
        Me.lblTitleEdiDate.WidthDef = 70
        '
        'imdEdiDateFrom
        '
        Me.imdEdiDateFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdEdiDateFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdEdiDateFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdEdiDateFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdEdiDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdEdiDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdEdiDateFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdEdiDateFrom.HeightDef = 18
        Me.imdEdiDateFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdEdiDateFrom.HissuLabelVisible = False
        Me.imdEdiDateFrom.Holiday = True
        Me.imdEdiDateFrom.IsAfterDateCheck = False
        Me.imdEdiDateFrom.IsBeforeDateCheck = False
        Me.imdEdiDateFrom.IsHissuCheck = False
        Me.imdEdiDateFrom.IsMinDateCheck = "1900/01/01"
        Me.imdEdiDateFrom.ItemName = ""
        Me.imdEdiDateFrom.Location = New System.Drawing.Point(142, 19)
        Me.imdEdiDateFrom.Name = "imdEdiDateFrom"
        Me.imdEdiDateFrom.Number = CType(10101000000, Long)
        Me.imdEdiDateFrom.ReadOnly = False
        Me.imdEdiDateFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdEdiDateFrom.TabIndex = 541
        Me.imdEdiDateFrom.TabStopSetting = True
        Me.imdEdiDateFrom.TextValue = ""
        Me.imdEdiDateFrom.Value = New Date(CType(0, Long))
        Me.imdEdiDateFrom.WidthDef = 118
        '
        'pnlNichikoGoods
        '
        Me.pnlNichikoGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNichikoGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlNichikoGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlNichikoGoods.Controls.Add(Me.lblTitleImportKb)
        Me.pnlNichikoGoods.Controls.Add(Me.chkImport)
        Me.pnlNichikoGoods.Controls.Add(Me.chkNotImport)
        Me.pnlNichikoGoods.Controls.Add(Me.lblTitleFromTo)
        Me.pnlNichikoGoods.Controls.Add(Me.imdEdiDateFrom)
        Me.pnlNichikoGoods.Controls.Add(Me.imdEdiDateTo)
        Me.pnlNichikoGoods.Controls.Add(Me.lblTitleEdiDate)
        Me.pnlNichikoGoods.Controls.Add(Me.sprNichikoGoods)
        Me.pnlNichikoGoods.EnableStatus = False
        Me.pnlNichikoGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNichikoGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlNichikoGoods.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNichikoGoods.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlNichikoGoods.HeightDef = 515
        Me.pnlNichikoGoods.Location = New System.Drawing.Point(12, 44)
        Me.pnlNichikoGoods.Name = "pnlNichikoGoods"
        Me.pnlNichikoGoods.Size = New System.Drawing.Size(1250, 515)
        Me.pnlNichikoGoods.TabIndex = 546
        Me.pnlNichikoGoods.TabStop = False
        Me.pnlNichikoGoods.Text = "日医工　製品マスタ　一覧表示"
        Me.pnlNichikoGoods.TextValue = "日医工　製品マスタ　一覧表示"
        Me.pnlNichikoGoods.WidthDef = 1250
        '
        'lblTitleImportKb
        '
        Me.lblTitleImportKb.AutoSize = True
        Me.lblTitleImportKb.AutoSizeDef = True
        Me.lblTitleImportKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleImportKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleImportKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleImportKb.EnableStatus = False
        Me.lblTitleImportKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleImportKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleImportKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleImportKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleImportKb.HeightDef = 13
        Me.lblTitleImportKb.Location = New System.Drawing.Point(429, 22)
        Me.lblTitleImportKb.Name = "lblTitleImportKb"
        Me.lblTitleImportKb.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleImportKb.TabIndex = 547
        Me.lblTitleImportKb.Text = "取込区分"
        Me.lblTitleImportKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleImportKb.TextValue = "取込区分"
        Me.lblTitleImportKb.WidthDef = 63
        '
        'chkImport
        '
        Me.chkImport.AutoSize = True
        Me.chkImport.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkImport.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkImport.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkImport.EnableStatus = True
        Me.chkImport.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkImport.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkImport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkImport.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkImport.HeightDef = 17
        Me.chkImport.Location = New System.Drawing.Point(589, 21)
        Me.chkImport.Name = "chkImport"
        Me.chkImport.Size = New System.Drawing.Size(68, 17)
        Me.chkImport.TabIndex = 546
        Me.chkImport.TabStopSetting = True
        Me.chkImport.Text = "反映済"
        Me.chkImport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkImport.TextValue = "反映済"
        Me.chkImport.UseVisualStyleBackColor = True
        Me.chkImport.WidthDef = 68
        '
        'chkNotImport
        '
        Me.chkNotImport.AutoSize = True
        Me.chkNotImport.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkNotImport.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkNotImport.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkNotImport.EnableStatus = True
        Me.chkNotImport.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkNotImport.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkNotImport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkNotImport.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkNotImport.HeightDef = 17
        Me.chkNotImport.Location = New System.Drawing.Point(505, 21)
        Me.chkNotImport.Name = "chkNotImport"
        Me.chkNotImport.Size = New System.Drawing.Size(68, 17)
        Me.chkNotImport.TabIndex = 545
        Me.chkNotImport.TabStopSetting = True
        Me.chkNotImport.Text = "未反映"
        Me.chkNotImport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkNotImport.TextValue = "未反映"
        Me.chkNotImport.UseVisualStyleBackColor = True
        Me.chkNotImport.WidthDef = 68
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
        Me.cmbEigyo.HissuLabelVisible = False
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = False
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(112, 15)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 549
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'txtSerchGoodsNm
        '
        Me.txtSerchGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerchGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerchGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerchGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerchGoodsNm.CountWrappedLine = False
        Me.txtSerchGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerchGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerchGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerchGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerchGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerchGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerchGoodsNm.HeightDef = 18
        Me.txtSerchGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerchGoodsNm.HissuLabelVisible = False
        Me.txtSerchGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtSerchGoodsNm.IsByteCheck = 60
        Me.txtSerchGoodsNm.IsCalendarCheck = False
        Me.txtSerchGoodsNm.IsDakutenCheck = False
        Me.txtSerchGoodsNm.IsEisuCheck = False
        Me.txtSerchGoodsNm.IsForbiddenWordsCheck = False
        Me.txtSerchGoodsNm.IsFullByteCheck = 0
        Me.txtSerchGoodsNm.IsHankakuCheck = False
        Me.txtSerchGoodsNm.IsHissuCheck = False
        Me.txtSerchGoodsNm.IsKanaCheck = False
        Me.txtSerchGoodsNm.IsMiddleSpace = False
        Me.txtSerchGoodsNm.IsNumericCheck = False
        Me.txtSerchGoodsNm.IsSujiCheck = False
        Me.txtSerchGoodsNm.IsZenkakuCheck = False
        Me.txtSerchGoodsNm.ItemName = ""
        Me.txtSerchGoodsNm.LineSpace = 0
        Me.txtSerchGoodsNm.Location = New System.Drawing.Point(233, 19)
        Me.txtSerchGoodsNm.MaxLength = 60
        Me.txtSerchGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerchGoodsNm.MaxLineCount = 0
        Me.txtSerchGoodsNm.Multiline = False
        Me.txtSerchGoodsNm.Name = "txtSerchGoodsNm"
        Me.txtSerchGoodsNm.ReadOnly = False
        Me.txtSerchGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerchGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerchGoodsNm.Size = New System.Drawing.Size(333, 18)
        Me.txtSerchGoodsNm.TabIndex = 269
        Me.txtSerchGoodsNm.TabStopSetting = True
        Me.txtSerchGoodsNm.TextValue = ""
        Me.txtSerchGoodsNm.UseSystemPasswordChar = False
        Me.txtSerchGoodsNm.WidthDef = 333
        Me.txtSerchGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoods
        '
        Me.lblTitleGoods.AutoSize = True
        Me.lblTitleGoods.AutoSizeDef = True
        Me.lblTitleGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoods.EnableStatus = False
        Me.lblTitleGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoods.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoods.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoods.HeightDef = 13
        Me.lblTitleGoods.Location = New System.Drawing.Point(52, 21)
        Me.lblTitleGoods.Name = "lblTitleGoods"
        Me.lblTitleGoods.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoods.TabIndex = 268
        Me.lblTitleGoods.Text = "商品"
        Me.lblTitleGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoods.TextValue = "商品"
        Me.lblTitleGoods.WidthDef = 35
        '
        'txtSerchGoodsCd
        '
        Me.txtSerchGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerchGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerchGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerchGoodsCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerchGoodsCd.CountWrappedLine = False
        Me.txtSerchGoodsCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerchGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerchGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerchGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerchGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerchGoodsCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerchGoodsCd.HeightDef = 18
        Me.txtSerchGoodsCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerchGoodsCd.HissuLabelVisible = False
        Me.txtSerchGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtSerchGoodsCd.IsByteCheck = 20
        Me.txtSerchGoodsCd.IsCalendarCheck = False
        Me.txtSerchGoodsCd.IsDakutenCheck = False
        Me.txtSerchGoodsCd.IsEisuCheck = False
        Me.txtSerchGoodsCd.IsForbiddenWordsCheck = False
        Me.txtSerchGoodsCd.IsFullByteCheck = 0
        Me.txtSerchGoodsCd.IsHankakuCheck = False
        Me.txtSerchGoodsCd.IsHissuCheck = False
        Me.txtSerchGoodsCd.IsKanaCheck = False
        Me.txtSerchGoodsCd.IsMiddleSpace = False
        Me.txtSerchGoodsCd.IsNumericCheck = False
        Me.txtSerchGoodsCd.IsSujiCheck = False
        Me.txtSerchGoodsCd.IsZenkakuCheck = False
        Me.txtSerchGoodsCd.ItemName = ""
        Me.txtSerchGoodsCd.LineSpace = 0
        Me.txtSerchGoodsCd.Location = New System.Drawing.Point(91, 19)
        Me.txtSerchGoodsCd.MaxLength = 20
        Me.txtSerchGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerchGoodsCd.MaxLineCount = 0
        Me.txtSerchGoodsCd.Multiline = False
        Me.txtSerchGoodsCd.Name = "txtSerchGoodsCd"
        Me.txtSerchGoodsCd.ReadOnly = False
        Me.txtSerchGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerchGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerchGoodsCd.Size = New System.Drawing.Size(159, 18)
        Me.txtSerchGoodsCd.TabIndex = 267
        Me.txtSerchGoodsCd.TabStopSetting = True
        Me.txtSerchGoodsCd.TextValue = "XXXXXXXXXXXXXXXXXXXZ"
        Me.txtSerchGoodsCd.UseSystemPasswordChar = False
        Me.txtSerchGoodsCd.WidthDef = 159
        Me.txtSerchGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmGroupBox1
        '
        Me.LmGroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCd)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleGoodsCd)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsKey)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleGoodsKey)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsNm1)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleGoodsNm1)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsNm2)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleGoodsNm2)
        Me.LmGroupBox1.Controls.Add(Me.txtUpGroupCd1)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleCustL)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustNmM)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustNmL)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustNmSS)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustNmS)
        Me.LmGroupBox1.Controls.Add(Me.LmGroupBox2)
        Me.LmGroupBox1.Controls.Add(Me.cmbSkyuMeiYn)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustCdM)
        Me.LmGroupBox1.Controls.Add(Me.lblSkyuMeiYn)
        Me.LmGroupBox1.Controls.Add(Me.cmbHikiateAlertYn)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustCdL)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleHikiateAlertYn)
        Me.LmGroupBox1.Controls.Add(Me.cmbCrtDateCtlKb)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustCdS)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleCrtDateCtlKb)
        Me.LmGroupBox1.Controls.Add(Me.txtGoodsCustCdSS)
        Me.LmGroupBox1.Controls.Add(Me.cmbCoaYn)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleCoaYn)
        Me.LmGroupBox1.Controls.Add(Me.cmbSpNhsYn)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleSpNhsYn)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleUpGroupCd1)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleLotCtlKb)
        Me.LmGroupBox1.Controls.Add(Me.cmbLotCtlKb)
        Me.LmGroupBox1.Controls.Add(Me.lblTitleTareYn)
        Me.LmGroupBox1.Controls.Add(Me.cmbTareYn)
        Me.LmGroupBox1.EnableStatus = False
        Me.LmGroupBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox1.HeightDef = 285
        Me.LmGroupBox1.Location = New System.Drawing.Point(12, 565)
        Me.LmGroupBox1.Name = "LmGroupBox1"
        Me.LmGroupBox1.Size = New System.Drawing.Size(1250, 285)
        Me.LmGroupBox1.TabIndex = 550
        Me.LmGroupBox1.TabStop = False
        Me.LmGroupBox1.Text = "商品マスタ　反映項目表示（必須項目のみ）"
        Me.LmGroupBox1.TextValue = "商品マスタ　反映項目表示（必須項目のみ）"
        Me.LmGroupBox1.WidthDef = 1250
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCd.CountWrappedLine = False
        Me.txtGoodsCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCd.HeightDef = 18
        Me.txtGoodsCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.HissuLabelVisible = False
        Me.txtGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtGoodsCd.IsByteCheck = 20
        Me.txtGoodsCd.IsCalendarCheck = False
        Me.txtGoodsCd.IsDakutenCheck = False
        Me.txtGoodsCd.IsEisuCheck = False
        Me.txtGoodsCd.IsForbiddenWordsCheck = False
        Me.txtGoodsCd.IsFullByteCheck = 0
        Me.txtGoodsCd.IsHankakuCheck = False
        Me.txtGoodsCd.IsHissuCheck = False
        Me.txtGoodsCd.IsKanaCheck = False
        Me.txtGoodsCd.IsMiddleSpace = False
        Me.txtGoodsCd.IsNumericCheck = False
        Me.txtGoodsCd.IsSujiCheck = False
        Me.txtGoodsCd.IsZenkakuCheck = False
        Me.txtGoodsCd.ItemName = ""
        Me.txtGoodsCd.LineSpace = 0
        Me.txtGoodsCd.Location = New System.Drawing.Point(143, 244)
        Me.txtGoodsCd.MaxLength = 20
        Me.txtGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCd.MaxLineCount = 0
        Me.txtGoodsCd.Multiline = False
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.ReadOnly = True
        Me.txtGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCd.Size = New System.Drawing.Size(167, 18)
        Me.txtGoodsCd.TabIndex = 589
        Me.txtGoodsCd.TabStop = False
        Me.txtGoodsCd.TabStopSetting = False
        Me.txtGoodsCd.Tag = ""
        Me.txtGoodsCd.TextValue = "X10XX10X"
        Me.txtGoodsCd.UseSystemPasswordChar = False
        Me.txtGoodsCd.WidthDef = 167
        Me.txtGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsCd
        '
        Me.lblTitleGoodsCd.AutoSize = True
        Me.lblTitleGoodsCd.AutoSizeDef = True
        Me.lblTitleGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsCd.EnableStatus = False
        Me.lblTitleGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCd.HeightDef = 13
        Me.lblTitleGoodsCd.Location = New System.Drawing.Point(63, 246)
        Me.lblTitleGoodsCd.Name = "lblTitleGoodsCd"
        Me.lblTitleGoodsCd.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleGoodsCd.TabIndex = 588
        Me.lblTitleGoodsCd.Text = "商品コード"
        Me.lblTitleGoodsCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsCd.TextValue = "商品コード"
        Me.lblTitleGoodsCd.WidthDef = 77
        '
        'txtGoodsKey
        '
        Me.txtGoodsKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsKey.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsKey.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsKey.CountWrappedLine = False
        Me.txtGoodsKey.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsKey.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsKey.HeightDef = 18
        Me.txtGoodsKey.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsKey.HissuLabelVisible = False
        Me.txtGoodsKey.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtGoodsKey.IsByteCheck = 20
        Me.txtGoodsKey.IsCalendarCheck = False
        Me.txtGoodsKey.IsDakutenCheck = False
        Me.txtGoodsKey.IsEisuCheck = False
        Me.txtGoodsKey.IsForbiddenWordsCheck = False
        Me.txtGoodsKey.IsFullByteCheck = 0
        Me.txtGoodsKey.IsHankakuCheck = False
        Me.txtGoodsKey.IsHissuCheck = False
        Me.txtGoodsKey.IsKanaCheck = False
        Me.txtGoodsKey.IsMiddleSpace = False
        Me.txtGoodsKey.IsNumericCheck = False
        Me.txtGoodsKey.IsSujiCheck = False
        Me.txtGoodsKey.IsZenkakuCheck = False
        Me.txtGoodsKey.ItemName = ""
        Me.txtGoodsKey.LineSpace = 0
        Me.txtGoodsKey.Location = New System.Drawing.Point(143, 223)
        Me.txtGoodsKey.MaxLength = 20
        Me.txtGoodsKey.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsKey.MaxLineCount = 0
        Me.txtGoodsKey.Multiline = False
        Me.txtGoodsKey.Name = "txtGoodsKey"
        Me.txtGoodsKey.ReadOnly = True
        Me.txtGoodsKey.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsKey.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsKey.Size = New System.Drawing.Size(167, 18)
        Me.txtGoodsKey.TabIndex = 586
        Me.txtGoodsKey.TabStop = False
        Me.txtGoodsKey.TabStopSetting = False
        Me.txtGoodsKey.Tag = ""
        Me.txtGoodsKey.TextValue = "X10XX10X"
        Me.txtGoodsKey.UseSystemPasswordChar = False
        Me.txtGoodsKey.WidthDef = 167
        Me.txtGoodsKey.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsKey
        '
        Me.lblTitleGoodsKey.AutoSize = True
        Me.lblTitleGoodsKey.AutoSizeDef = True
        Me.lblTitleGoodsKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsKey.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsKey.EnableStatus = False
        Me.lblTitleGoodsKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsKey.HeightDef = 13
        Me.lblTitleGoodsKey.Location = New System.Drawing.Point(84, 225)
        Me.lblTitleGoodsKey.Name = "lblTitleGoodsKey"
        Me.lblTitleGoodsKey.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleGoodsKey.TabIndex = 587
        Me.lblTitleGoodsKey.Text = "商品KEY"
        Me.lblTitleGoodsKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsKey.TextValue = "商品KEY"
        Me.lblTitleGoodsKey.WidthDef = 56
        '
        'txtGoodsNm1
        '
        Me.txtGoodsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsNm1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsNm1.CountWrappedLine = False
        Me.txtGoodsNm1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsNm1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsNm1.HeightDef = 18
        Me.txtGoodsNm1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm1.HissuLabelVisible = False
        Me.txtGoodsNm1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsNm1.IsByteCheck = 60
        Me.txtGoodsNm1.IsCalendarCheck = False
        Me.txtGoodsNm1.IsDakutenCheck = False
        Me.txtGoodsNm1.IsEisuCheck = False
        Me.txtGoodsNm1.IsForbiddenWordsCheck = False
        Me.txtGoodsNm1.IsFullByteCheck = 0
        Me.txtGoodsNm1.IsHankakuCheck = False
        Me.txtGoodsNm1.IsHissuCheck = False
        Me.txtGoodsNm1.IsKanaCheck = False
        Me.txtGoodsNm1.IsMiddleSpace = False
        Me.txtGoodsNm1.IsNumericCheck = False
        Me.txtGoodsNm1.IsSujiCheck = False
        Me.txtGoodsNm1.IsZenkakuCheck = False
        Me.txtGoodsNm1.ItemName = ""
        Me.txtGoodsNm1.LineSpace = 0
        Me.txtGoodsNm1.Location = New System.Drawing.Point(143, 180)
        Me.txtGoodsNm1.MaxLength = 60
        Me.txtGoodsNm1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNm1.MaxLineCount = 0
        Me.txtGoodsNm1.Multiline = False
        Me.txtGoodsNm1.Name = "txtGoodsNm1"
        Me.txtGoodsNm1.ReadOnly = True
        Me.txtGoodsNm1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNm1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNm1.Size = New System.Drawing.Size(448, 18)
        Me.txtGoodsNm1.TabIndex = 582
        Me.txtGoodsNm1.TabStop = False
        Me.txtGoodsNm1.TabStopSetting = False
        Me.txtGoodsNm1.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsNm1.UseSystemPasswordChar = False
        Me.txtGoodsNm1.WidthDef = 448
        Me.txtGoodsNm1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsNm1
        '
        Me.lblTitleGoodsNm1.AutoSize = True
        Me.lblTitleGoodsNm1.AutoSizeDef = True
        Me.lblTitleGoodsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsNm1.EnableStatus = False
        Me.lblTitleGoodsNm1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm1.HeightDef = 13
        Me.lblTitleGoodsNm1.Location = New System.Drawing.Point(84, 182)
        Me.lblTitleGoodsNm1.Name = "lblTitleGoodsNm1"
        Me.lblTitleGoodsNm1.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleGoodsNm1.TabIndex = 583
        Me.lblTitleGoodsNm1.Text = "商品名1"
        Me.lblTitleGoodsNm1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsNm1.TextValue = "商品名1"
        Me.lblTitleGoodsNm1.WidthDef = 56
        '
        'txtGoodsNm2
        '
        Me.txtGoodsNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsNm2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsNm2.CountWrappedLine = False
        Me.txtGoodsNm2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsNm2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsNm2.HeightDef = 18
        Me.txtGoodsNm2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm2.HissuLabelVisible = False
        Me.txtGoodsNm2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtGoodsNm2.IsByteCheck = 60
        Me.txtGoodsNm2.IsCalendarCheck = False
        Me.txtGoodsNm2.IsDakutenCheck = False
        Me.txtGoodsNm2.IsEisuCheck = False
        Me.txtGoodsNm2.IsForbiddenWordsCheck = False
        Me.txtGoodsNm2.IsFullByteCheck = 0
        Me.txtGoodsNm2.IsHankakuCheck = False
        Me.txtGoodsNm2.IsHissuCheck = False
        Me.txtGoodsNm2.IsKanaCheck = False
        Me.txtGoodsNm2.IsMiddleSpace = False
        Me.txtGoodsNm2.IsNumericCheck = False
        Me.txtGoodsNm2.IsSujiCheck = False
        Me.txtGoodsNm2.IsZenkakuCheck = False
        Me.txtGoodsNm2.ItemName = ""
        Me.txtGoodsNm2.LineSpace = 0
        Me.txtGoodsNm2.Location = New System.Drawing.Point(143, 202)
        Me.txtGoodsNm2.MaxLength = 60
        Me.txtGoodsNm2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNm2.MaxLineCount = 0
        Me.txtGoodsNm2.Multiline = False
        Me.txtGoodsNm2.Name = "txtGoodsNm2"
        Me.txtGoodsNm2.ReadOnly = True
        Me.txtGoodsNm2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNm2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNm2.Size = New System.Drawing.Size(448, 18)
        Me.txtGoodsNm2.TabIndex = 584
        Me.txtGoodsNm2.TabStop = False
        Me.txtGoodsNm2.TabStopSetting = False
        Me.txtGoodsNm2.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsNm2.UseSystemPasswordChar = False
        Me.txtGoodsNm2.WidthDef = 448
        Me.txtGoodsNm2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsNm2
        '
        Me.lblTitleGoodsNm2.AutoSize = True
        Me.lblTitleGoodsNm2.AutoSizeDef = True
        Me.lblTitleGoodsNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsNm2.EnableStatus = False
        Me.lblTitleGoodsNm2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm2.HeightDef = 13
        Me.lblTitleGoodsNm2.Location = New System.Drawing.Point(84, 204)
        Me.lblTitleGoodsNm2.Name = "lblTitleGoodsNm2"
        Me.lblTitleGoodsNm2.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleGoodsNm2.TabIndex = 585
        Me.lblTitleGoodsNm2.Text = "商品名2"
        Me.lblTitleGoodsNm2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsNm2.TextValue = "商品名2"
        Me.lblTitleGoodsNm2.WidthDef = 56
        '
        'txtUpGroupCd1
        '
        Me.txtUpGroupCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpGroupCd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpGroupCd1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUpGroupCd1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUpGroupCd1.CountWrappedLine = False
        Me.txtUpGroupCd1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUpGroupCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpGroupCd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpGroupCd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpGroupCd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpGroupCd1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUpGroupCd1.HeightDef = 18
        Me.txtUpGroupCd1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpGroupCd1.HissuLabelVisible = False
        Me.txtUpGroupCd1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUpGroupCd1.IsByteCheck = 5
        Me.txtUpGroupCd1.IsCalendarCheck = False
        Me.txtUpGroupCd1.IsDakutenCheck = False
        Me.txtUpGroupCd1.IsEisuCheck = False
        Me.txtUpGroupCd1.IsForbiddenWordsCheck = False
        Me.txtUpGroupCd1.IsFullByteCheck = 0
        Me.txtUpGroupCd1.IsHankakuCheck = False
        Me.txtUpGroupCd1.IsHissuCheck = False
        Me.txtUpGroupCd1.IsKanaCheck = False
        Me.txtUpGroupCd1.IsMiddleSpace = False
        Me.txtUpGroupCd1.IsNumericCheck = False
        Me.txtUpGroupCd1.IsSujiCheck = False
        Me.txtUpGroupCd1.IsZenkakuCheck = False
        Me.txtUpGroupCd1.ItemName = ""
        Me.txtUpGroupCd1.LineSpace = 0
        Me.txtUpGroupCd1.Location = New System.Drawing.Point(749, 116)
        Me.txtUpGroupCd1.MaxLength = 5
        Me.txtUpGroupCd1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUpGroupCd1.MaxLineCount = 0
        Me.txtUpGroupCd1.Multiline = False
        Me.txtUpGroupCd1.Name = "txtUpGroupCd1"
        Me.txtUpGroupCd1.ReadOnly = True
        Me.txtUpGroupCd1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUpGroupCd1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUpGroupCd1.Size = New System.Drawing.Size(63, 18)
        Me.txtUpGroupCd1.TabIndex = 581
        Me.txtUpGroupCd1.TabStop = False
        Me.txtUpGroupCd1.TabStopSetting = False
        Me.txtUpGroupCd1.Tag = ""
        Me.txtUpGroupCd1.TextValue = "X10XX"
        Me.txtUpGroupCd1.UseSystemPasswordChar = False
        Me.txtUpGroupCd1.WidthDef = 63
        Me.txtUpGroupCd1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCustL
        '
        Me.lblTitleCustL.AutoSize = True
        Me.lblTitleCustL.AutoSizeDef = True
        Me.lblTitleCustL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCustL.EnableStatus = False
        Me.lblTitleCustL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustL.HeightDef = 13
        Me.lblTitleCustL.Location = New System.Drawing.Point(105, 96)
        Me.lblTitleCustL.Name = "lblTitleCustL"
        Me.lblTitleCustL.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustL.TabIndex = 580
        Me.lblTitleCustL.Text = "荷主"
        Me.lblTitleCustL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustL.TextValue = "荷主"
        Me.lblTitleCustL.WidthDef = 35
        '
        'txtGoodsCustNmM
        '
        Me.txtGoodsCustNmM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustNmM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustNmM.CountWrappedLine = False
        Me.txtGoodsCustNmM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustNmM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustNmM.HeightDef = 18
        Me.txtGoodsCustNmM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmM.HissuLabelVisible = False
        Me.txtGoodsCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCustNmM.IsByteCheck = 0
        Me.txtGoodsCustNmM.IsCalendarCheck = False
        Me.txtGoodsCustNmM.IsDakutenCheck = False
        Me.txtGoodsCustNmM.IsEisuCheck = False
        Me.txtGoodsCustNmM.IsForbiddenWordsCheck = False
        Me.txtGoodsCustNmM.IsFullByteCheck = 0
        Me.txtGoodsCustNmM.IsHankakuCheck = False
        Me.txtGoodsCustNmM.IsHissuCheck = False
        Me.txtGoodsCustNmM.IsKanaCheck = False
        Me.txtGoodsCustNmM.IsMiddleSpace = False
        Me.txtGoodsCustNmM.IsNumericCheck = False
        Me.txtGoodsCustNmM.IsSujiCheck = False
        Me.txtGoodsCustNmM.IsZenkakuCheck = False
        Me.txtGoodsCustNmM.ItemName = ""
        Me.txtGoodsCustNmM.LineSpace = 0
        Me.txtGoodsCustNmM.Location = New System.Drawing.Point(193, 115)
        Me.txtGoodsCustNmM.MaxLength = 0
        Me.txtGoodsCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustNmM.MaxLineCount = 0
        Me.txtGoodsCustNmM.Multiline = False
        Me.txtGoodsCustNmM.Name = "txtGoodsCustNmM"
        Me.txtGoodsCustNmM.ReadOnly = True
        Me.txtGoodsCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustNmM.Size = New System.Drawing.Size(398, 18)
        Me.txtGoodsCustNmM.TabIndex = 576
        Me.txtGoodsCustNmM.TabStop = False
        Me.txtGoodsCustNmM.TabStopSetting = False
        Me.txtGoodsCustNmM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsCustNmM.UseSystemPasswordChar = False
        Me.txtGoodsCustNmM.WidthDef = 398
        Me.txtGoodsCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCustNmL
        '
        Me.txtGoodsCustNmL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustNmL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustNmL.CountWrappedLine = False
        Me.txtGoodsCustNmL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustNmL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustNmL.HeightDef = 18
        Me.txtGoodsCustNmL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmL.HissuLabelVisible = False
        Me.txtGoodsCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCustNmL.IsByteCheck = 0
        Me.txtGoodsCustNmL.IsCalendarCheck = False
        Me.txtGoodsCustNmL.IsDakutenCheck = False
        Me.txtGoodsCustNmL.IsEisuCheck = False
        Me.txtGoodsCustNmL.IsForbiddenWordsCheck = False
        Me.txtGoodsCustNmL.IsFullByteCheck = 0
        Me.txtGoodsCustNmL.IsHankakuCheck = False
        Me.txtGoodsCustNmL.IsHissuCheck = False
        Me.txtGoodsCustNmL.IsKanaCheck = False
        Me.txtGoodsCustNmL.IsMiddleSpace = False
        Me.txtGoodsCustNmL.IsNumericCheck = False
        Me.txtGoodsCustNmL.IsSujiCheck = False
        Me.txtGoodsCustNmL.IsZenkakuCheck = False
        Me.txtGoodsCustNmL.ItemName = ""
        Me.txtGoodsCustNmL.LineSpace = 0
        Me.txtGoodsCustNmL.Location = New System.Drawing.Point(193, 94)
        Me.txtGoodsCustNmL.MaxLength = 0
        Me.txtGoodsCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustNmL.MaxLineCount = 0
        Me.txtGoodsCustNmL.Multiline = False
        Me.txtGoodsCustNmL.Name = "txtGoodsCustNmL"
        Me.txtGoodsCustNmL.ReadOnly = True
        Me.txtGoodsCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustNmL.Size = New System.Drawing.Size(398, 18)
        Me.txtGoodsCustNmL.TabIndex = 575
        Me.txtGoodsCustNmL.TabStop = False
        Me.txtGoodsCustNmL.TabStopSetting = False
        Me.txtGoodsCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsCustNmL.UseSystemPasswordChar = False
        Me.txtGoodsCustNmL.WidthDef = 398
        Me.txtGoodsCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCustNmSS
        '
        Me.txtGoodsCustNmSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmSS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmSS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustNmSS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustNmSS.CountWrappedLine = False
        Me.txtGoodsCustNmSS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustNmSS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmSS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmSS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmSS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmSS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustNmSS.HeightDef = 18
        Me.txtGoodsCustNmSS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmSS.HissuLabelVisible = False
        Me.txtGoodsCustNmSS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCustNmSS.IsByteCheck = 0
        Me.txtGoodsCustNmSS.IsCalendarCheck = False
        Me.txtGoodsCustNmSS.IsDakutenCheck = False
        Me.txtGoodsCustNmSS.IsEisuCheck = False
        Me.txtGoodsCustNmSS.IsForbiddenWordsCheck = False
        Me.txtGoodsCustNmSS.IsFullByteCheck = 0
        Me.txtGoodsCustNmSS.IsHankakuCheck = False
        Me.txtGoodsCustNmSS.IsHissuCheck = False
        Me.txtGoodsCustNmSS.IsKanaCheck = False
        Me.txtGoodsCustNmSS.IsMiddleSpace = False
        Me.txtGoodsCustNmSS.IsNumericCheck = False
        Me.txtGoodsCustNmSS.IsSujiCheck = False
        Me.txtGoodsCustNmSS.IsZenkakuCheck = False
        Me.txtGoodsCustNmSS.ItemName = ""
        Me.txtGoodsCustNmSS.LineSpace = 0
        Me.txtGoodsCustNmSS.Location = New System.Drawing.Point(193, 158)
        Me.txtGoodsCustNmSS.MaxLength = 0
        Me.txtGoodsCustNmSS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustNmSS.MaxLineCount = 0
        Me.txtGoodsCustNmSS.Multiline = False
        Me.txtGoodsCustNmSS.Name = "txtGoodsCustNmSS"
        Me.txtGoodsCustNmSS.ReadOnly = True
        Me.txtGoodsCustNmSS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustNmSS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustNmSS.Size = New System.Drawing.Size(398, 18)
        Me.txtGoodsCustNmSS.TabIndex = 579
        Me.txtGoodsCustNmSS.TabStop = False
        Me.txtGoodsCustNmSS.TabStopSetting = False
        Me.txtGoodsCustNmSS.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsCustNmSS.UseSystemPasswordChar = False
        Me.txtGoodsCustNmSS.WidthDef = 398
        Me.txtGoodsCustNmSS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCustNmS
        '
        Me.txtGoodsCustNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustNmS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustNmS.CountWrappedLine = False
        Me.txtGoodsCustNmS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustNmS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustNmS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustNmS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustNmS.HeightDef = 18
        Me.txtGoodsCustNmS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustNmS.HissuLabelVisible = False
        Me.txtGoodsCustNmS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCustNmS.IsByteCheck = 0
        Me.txtGoodsCustNmS.IsCalendarCheck = False
        Me.txtGoodsCustNmS.IsDakutenCheck = False
        Me.txtGoodsCustNmS.IsEisuCheck = False
        Me.txtGoodsCustNmS.IsForbiddenWordsCheck = False
        Me.txtGoodsCustNmS.IsFullByteCheck = 0
        Me.txtGoodsCustNmS.IsHankakuCheck = False
        Me.txtGoodsCustNmS.IsHissuCheck = False
        Me.txtGoodsCustNmS.IsKanaCheck = False
        Me.txtGoodsCustNmS.IsMiddleSpace = False
        Me.txtGoodsCustNmS.IsNumericCheck = False
        Me.txtGoodsCustNmS.IsSujiCheck = False
        Me.txtGoodsCustNmS.IsZenkakuCheck = False
        Me.txtGoodsCustNmS.ItemName = ""
        Me.txtGoodsCustNmS.LineSpace = 0
        Me.txtGoodsCustNmS.Location = New System.Drawing.Point(193, 137)
        Me.txtGoodsCustNmS.MaxLength = 0
        Me.txtGoodsCustNmS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustNmS.MaxLineCount = 0
        Me.txtGoodsCustNmS.Multiline = False
        Me.txtGoodsCustNmS.Name = "txtGoodsCustNmS"
        Me.txtGoodsCustNmS.ReadOnly = True
        Me.txtGoodsCustNmS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustNmS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustNmS.Size = New System.Drawing.Size(398, 18)
        Me.txtGoodsCustNmS.TabIndex = 572
        Me.txtGoodsCustNmS.TabStop = False
        Me.txtGoodsCustNmS.TabStopSetting = False
        Me.txtGoodsCustNmS.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.txtGoodsCustNmS.UseSystemPasswordChar = False
        Me.txtGoodsCustNmS.WidthDef = 398
        Me.txtGoodsCustNmS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmGroupBox2
        '
        Me.LmGroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmGroupBox2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmGroupBox2.Controls.Add(Me.lblTitleGoods)
        Me.LmGroupBox2.Controls.Add(Me.txtSerchGoodsNm)
        Me.LmGroupBox2.Controls.Add(Me.txtSerchGoodsCd)
        Me.LmGroupBox2.EnableStatus = False
        Me.LmGroupBox2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmGroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmGroupBox2.HeightDef = 50
        Me.LmGroupBox2.Location = New System.Drawing.Point(52, 29)
        Me.LmGroupBox2.Name = "LmGroupBox2"
        Me.LmGroupBox2.Size = New System.Drawing.Size(585, 50)
        Me.LmGroupBox2.TabIndex = 567
        Me.LmGroupBox2.TabStop = False
        Me.LmGroupBox2.Text = "検索条件"
        Me.LmGroupBox2.TextValue = "検索条件"
        Me.LmGroupBox2.WidthDef = 585
        '
        'cmbSkyuMeiYn
        '
        Me.cmbSkyuMeiYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuMeiYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuMeiYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSkyuMeiYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSkyuMeiYn.DataCode = "U009"
        Me.cmbSkyuMeiYn.DataSource = Nothing
        Me.cmbSkyuMeiYn.DisplayMember = Nothing
        Me.cmbSkyuMeiYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSkyuMeiYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSkyuMeiYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSkyuMeiYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSkyuMeiYn.HeightDef = 18
        Me.cmbSkyuMeiYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuMeiYn.HissuLabelVisible = False
        Me.cmbSkyuMeiYn.InsertWildCard = True
        Me.cmbSkyuMeiYn.IsForbiddenWordsCheck = False
        Me.cmbSkyuMeiYn.IsHissuCheck = False
        Me.cmbSkyuMeiYn.ItemName = ""
        Me.cmbSkyuMeiYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSkyuMeiYn.Location = New System.Drawing.Point(1024, 181)
        Me.cmbSkyuMeiYn.Name = "cmbSkyuMeiYn"
        Me.cmbSkyuMeiYn.ReadOnly = True
        Me.cmbSkyuMeiYn.SelectedIndex = -1
        Me.cmbSkyuMeiYn.SelectedItem = Nothing
        Me.cmbSkyuMeiYn.SelectedText = ""
        Me.cmbSkyuMeiYn.SelectedValue = ""
        Me.cmbSkyuMeiYn.Size = New System.Drawing.Size(133, 18)
        Me.cmbSkyuMeiYn.TabIndex = 566
        Me.cmbSkyuMeiYn.TabStop = False
        Me.cmbSkyuMeiYn.TabStopSetting = False
        Me.cmbSkyuMeiYn.TextValue = ""
        Me.cmbSkyuMeiYn.Value1 = Nothing
        Me.cmbSkyuMeiYn.Value2 = Nothing
        Me.cmbSkyuMeiYn.Value3 = Nothing
        Me.cmbSkyuMeiYn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSkyuMeiYn.ValueMember = Nothing
        Me.cmbSkyuMeiYn.WidthDef = 133
        '
        'txtGoodsCustCdM
        '
        Me.txtGoodsCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustCdM.CountWrappedLine = False
        Me.txtGoodsCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustCdM.HeightDef = 18
        Me.txtGoodsCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdM.HissuLabelVisible = False
        Me.txtGoodsCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtGoodsCustCdM.IsByteCheck = 2
        Me.txtGoodsCustCdM.IsCalendarCheck = False
        Me.txtGoodsCustCdM.IsDakutenCheck = False
        Me.txtGoodsCustCdM.IsEisuCheck = False
        Me.txtGoodsCustCdM.IsForbiddenWordsCheck = False
        Me.txtGoodsCustCdM.IsFullByteCheck = 0
        Me.txtGoodsCustCdM.IsHankakuCheck = False
        Me.txtGoodsCustCdM.IsHissuCheck = False
        Me.txtGoodsCustCdM.IsKanaCheck = False
        Me.txtGoodsCustCdM.IsMiddleSpace = False
        Me.txtGoodsCustCdM.IsNumericCheck = False
        Me.txtGoodsCustCdM.IsSujiCheck = False
        Me.txtGoodsCustCdM.IsZenkakuCheck = False
        Me.txtGoodsCustCdM.ItemName = ""
        Me.txtGoodsCustCdM.LineSpace = 0
        Me.txtGoodsCustCdM.Location = New System.Drawing.Point(166, 115)
        Me.txtGoodsCustCdM.MaxLength = 2
        Me.txtGoodsCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustCdM.MaxLineCount = 0
        Me.txtGoodsCustCdM.Multiline = False
        Me.txtGoodsCustCdM.Name = "txtGoodsCustCdM"
        Me.txtGoodsCustCdM.ReadOnly = True
        Me.txtGoodsCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustCdM.Size = New System.Drawing.Size(43, 18)
        Me.txtGoodsCustCdM.TabIndex = 574
        Me.txtGoodsCustCdM.TabStop = False
        Me.txtGoodsCustCdM.TabStopSetting = False
        Me.txtGoodsCustCdM.TextValue = "XX"
        Me.txtGoodsCustCdM.UseSystemPasswordChar = False
        Me.txtGoodsCustCdM.WidthDef = 43
        Me.txtGoodsCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSkyuMeiYn
        '
        Me.lblSkyuMeiYn.AutoSize = True
        Me.lblSkyuMeiYn.AutoSizeDef = True
        Me.lblSkyuMeiYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSkyuMeiYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSkyuMeiYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSkyuMeiYn.EnableStatus = False
        Me.lblSkyuMeiYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSkyuMeiYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSkyuMeiYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSkyuMeiYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSkyuMeiYn.HeightDef = 13
        Me.lblSkyuMeiYn.Location = New System.Drawing.Point(917, 183)
        Me.lblSkyuMeiYn.Name = "lblSkyuMeiYn"
        Me.lblSkyuMeiYn.Size = New System.Drawing.Size(105, 13)
        Me.lblSkyuMeiYn.TabIndex = 565
        Me.lblSkyuMeiYn.Text = "請求明細書出力"
        Me.lblSkyuMeiYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSkyuMeiYn.TextValue = "請求明細書出力"
        Me.lblSkyuMeiYn.WidthDef = 105
        '
        'cmbHikiateAlertYn
        '
        Me.cmbHikiateAlertYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHikiateAlertYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHikiateAlertYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbHikiateAlertYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbHikiateAlertYn.DataCode = "U009"
        Me.cmbHikiateAlertYn.DataSource = Nothing
        Me.cmbHikiateAlertYn.DisplayMember = Nothing
        Me.cmbHikiateAlertYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHikiateAlertYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHikiateAlertYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHikiateAlertYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHikiateAlertYn.HeightDef = 18
        Me.cmbHikiateAlertYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHikiateAlertYn.HissuLabelVisible = False
        Me.cmbHikiateAlertYn.InsertWildCard = True
        Me.cmbHikiateAlertYn.IsForbiddenWordsCheck = False
        Me.cmbHikiateAlertYn.IsHissuCheck = False
        Me.cmbHikiateAlertYn.ItemName = ""
        Me.cmbHikiateAlertYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbHikiateAlertYn.Location = New System.Drawing.Point(1024, 159)
        Me.cmbHikiateAlertYn.Name = "cmbHikiateAlertYn"
        Me.cmbHikiateAlertYn.ReadOnly = True
        Me.cmbHikiateAlertYn.SelectedIndex = -1
        Me.cmbHikiateAlertYn.SelectedItem = Nothing
        Me.cmbHikiateAlertYn.SelectedText = ""
        Me.cmbHikiateAlertYn.SelectedValue = ""
        Me.cmbHikiateAlertYn.Size = New System.Drawing.Size(133, 18)
        Me.cmbHikiateAlertYn.TabIndex = 564
        Me.cmbHikiateAlertYn.TabStop = False
        Me.cmbHikiateAlertYn.TabStopSetting = False
        Me.cmbHikiateAlertYn.TextValue = ""
        Me.cmbHikiateAlertYn.Value1 = Nothing
        Me.cmbHikiateAlertYn.Value2 = Nothing
        Me.cmbHikiateAlertYn.Value3 = Nothing
        Me.cmbHikiateAlertYn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbHikiateAlertYn.ValueMember = Nothing
        Me.cmbHikiateAlertYn.WidthDef = 133
        '
        'txtGoodsCustCdL
        '
        Me.txtGoodsCustCdL.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.txtGoodsCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustCdL.CountWrappedLine = False
        Me.txtGoodsCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustCdL.HeightDef = 18
        Me.txtGoodsCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdL.HissuLabelVisible = False
        Me.txtGoodsCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtGoodsCustCdL.IsByteCheck = 5
        Me.txtGoodsCustCdL.IsCalendarCheck = False
        Me.txtGoodsCustCdL.IsDakutenCheck = False
        Me.txtGoodsCustCdL.IsEisuCheck = False
        Me.txtGoodsCustCdL.IsForbiddenWordsCheck = False
        Me.txtGoodsCustCdL.IsFullByteCheck = 0
        Me.txtGoodsCustCdL.IsHankakuCheck = False
        Me.txtGoodsCustCdL.IsHissuCheck = False
        Me.txtGoodsCustCdL.IsKanaCheck = False
        Me.txtGoodsCustCdL.IsMiddleSpace = False
        Me.txtGoodsCustCdL.IsNumericCheck = False
        Me.txtGoodsCustCdL.IsSujiCheck = False
        Me.txtGoodsCustCdL.IsZenkakuCheck = False
        Me.txtGoodsCustCdL.ItemName = ""
        Me.txtGoodsCustCdL.LineSpace = 0
        Me.txtGoodsCustCdL.Location = New System.Drawing.Point(143, 94)
        Me.txtGoodsCustCdL.MaxLength = 5
        Me.txtGoodsCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustCdL.MaxLineCount = 0
        Me.txtGoodsCustCdL.Multiline = False
        Me.txtGoodsCustCdL.Name = "txtGoodsCustCdL"
        Me.txtGoodsCustCdL.ReadOnly = True
        Me.txtGoodsCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustCdL.Size = New System.Drawing.Size(66, 18)
        Me.txtGoodsCustCdL.TabIndex = 573
        Me.txtGoodsCustCdL.TabStop = False
        Me.txtGoodsCustCdL.TabStopSetting = False
        Me.txtGoodsCustCdL.TextValue = "X1"
        Me.txtGoodsCustCdL.UseSystemPasswordChar = False
        Me.txtGoodsCustCdL.WidthDef = 66
        Me.txtGoodsCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleHikiateAlertYn
        '
        Me.lblTitleHikiateAlertYn.AutoSize = True
        Me.lblTitleHikiateAlertYn.AutoSizeDef = True
        Me.lblTitleHikiateAlertYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiateAlertYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHikiateAlertYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHikiateAlertYn.EnableStatus = False
        Me.lblTitleHikiateAlertYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiateAlertYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHikiateAlertYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiateAlertYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHikiateAlertYn.HeightDef = 13
        Me.lblTitleHikiateAlertYn.Location = New System.Drawing.Point(945, 161)
        Me.lblTitleHikiateAlertYn.Name = "lblTitleHikiateAlertYn"
        Me.lblTitleHikiateAlertYn.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleHikiateAlertYn.TabIndex = 563
        Me.lblTitleHikiateAlertYn.Text = "引当注意品"
        Me.lblTitleHikiateAlertYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHikiateAlertYn.TextValue = "引当注意品"
        Me.lblTitleHikiateAlertYn.WidthDef = 77
        '
        'cmbCrtDateCtlKb
        '
        Me.cmbCrtDateCtlKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCrtDateCtlKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCrtDateCtlKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCrtDateCtlKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCrtDateCtlKb.DataCode = "U009"
        Me.cmbCrtDateCtlKb.DataSource = Nothing
        Me.cmbCrtDateCtlKb.DisplayMember = Nothing
        Me.cmbCrtDateCtlKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCrtDateCtlKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCrtDateCtlKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCrtDateCtlKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCrtDateCtlKb.HeightDef = 18
        Me.cmbCrtDateCtlKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCrtDateCtlKb.HissuLabelVisible = False
        Me.cmbCrtDateCtlKb.InsertWildCard = True
        Me.cmbCrtDateCtlKb.IsForbiddenWordsCheck = False
        Me.cmbCrtDateCtlKb.IsHissuCheck = False
        Me.cmbCrtDateCtlKb.ItemName = ""
        Me.cmbCrtDateCtlKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCrtDateCtlKb.Location = New System.Drawing.Point(1024, 137)
        Me.cmbCrtDateCtlKb.Name = "cmbCrtDateCtlKb"
        Me.cmbCrtDateCtlKb.ReadOnly = True
        Me.cmbCrtDateCtlKb.SelectedIndex = -1
        Me.cmbCrtDateCtlKb.SelectedItem = Nothing
        Me.cmbCrtDateCtlKb.SelectedText = ""
        Me.cmbCrtDateCtlKb.SelectedValue = ""
        Me.cmbCrtDateCtlKb.Size = New System.Drawing.Size(133, 18)
        Me.cmbCrtDateCtlKb.TabIndex = 562
        Me.cmbCrtDateCtlKb.TabStop = False
        Me.cmbCrtDateCtlKb.TabStopSetting = False
        Me.cmbCrtDateCtlKb.TextValue = ""
        Me.cmbCrtDateCtlKb.Value1 = Nothing
        Me.cmbCrtDateCtlKb.Value2 = Nothing
        Me.cmbCrtDateCtlKb.Value3 = Nothing
        Me.cmbCrtDateCtlKb.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbCrtDateCtlKb.ValueMember = Nothing
        Me.cmbCrtDateCtlKb.WidthDef = 133
        '
        'txtGoodsCustCdS
        '
        Me.txtGoodsCustCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustCdS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustCdS.CountWrappedLine = False
        Me.txtGoodsCustCdS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustCdS.HeightDef = 18
        Me.txtGoodsCustCdS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdS.HissuLabelVisible = False
        Me.txtGoodsCustCdS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtGoodsCustCdS.IsByteCheck = 2
        Me.txtGoodsCustCdS.IsCalendarCheck = False
        Me.txtGoodsCustCdS.IsDakutenCheck = False
        Me.txtGoodsCustCdS.IsEisuCheck = False
        Me.txtGoodsCustCdS.IsForbiddenWordsCheck = False
        Me.txtGoodsCustCdS.IsFullByteCheck = 0
        Me.txtGoodsCustCdS.IsHankakuCheck = False
        Me.txtGoodsCustCdS.IsHissuCheck = False
        Me.txtGoodsCustCdS.IsKanaCheck = False
        Me.txtGoodsCustCdS.IsMiddleSpace = False
        Me.txtGoodsCustCdS.IsNumericCheck = False
        Me.txtGoodsCustCdS.IsSujiCheck = False
        Me.txtGoodsCustCdS.IsZenkakuCheck = False
        Me.txtGoodsCustCdS.ItemName = ""
        Me.txtGoodsCustCdS.LineSpace = 0
        Me.txtGoodsCustCdS.Location = New System.Drawing.Point(166, 137)
        Me.txtGoodsCustCdS.MaxLength = 2
        Me.txtGoodsCustCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustCdS.MaxLineCount = 0
        Me.txtGoodsCustCdS.Multiline = False
        Me.txtGoodsCustCdS.Name = "txtGoodsCustCdS"
        Me.txtGoodsCustCdS.ReadOnly = True
        Me.txtGoodsCustCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustCdS.Size = New System.Drawing.Size(43, 18)
        Me.txtGoodsCustCdS.TabIndex = 577
        Me.txtGoodsCustCdS.TabStop = False
        Me.txtGoodsCustCdS.TabStopSetting = False
        Me.txtGoodsCustCdS.TextValue = "XX"
        Me.txtGoodsCustCdS.UseSystemPasswordChar = False
        Me.txtGoodsCustCdS.WidthDef = 43
        Me.txtGoodsCustCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCrtDateCtlKb
        '
        Me.lblTitleCrtDateCtlKb.AutoSize = True
        Me.lblTitleCrtDateCtlKb.AutoSizeDef = True
        Me.lblTitleCrtDateCtlKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtDateCtlKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtDateCtlKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCrtDateCtlKb.EnableStatus = False
        Me.lblTitleCrtDateCtlKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtDateCtlKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtDateCtlKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtDateCtlKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtDateCtlKb.HeightDef = 13
        Me.lblTitleCrtDateCtlKb.Location = New System.Drawing.Point(945, 139)
        Me.lblTitleCrtDateCtlKb.Name = "lblTitleCrtDateCtlKb"
        Me.lblTitleCrtDateCtlKb.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleCrtDateCtlKb.TabIndex = 561
        Me.lblTitleCrtDateCtlKb.Text = "製造日管理"
        Me.lblTitleCrtDateCtlKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCrtDateCtlKb.TextValue = "製造日管理"
        Me.lblTitleCrtDateCtlKb.WidthDef = 77
        '
        'txtGoodsCustCdSS
        '
        Me.txtGoodsCustCdSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdSS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdSS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCustCdSS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCustCdSS.CountWrappedLine = False
        Me.txtGoodsCustCdSS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCustCdSS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdSS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCustCdSS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdSS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCustCdSS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCustCdSS.HeightDef = 18
        Me.txtGoodsCustCdSS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCustCdSS.HissuLabelVisible = False
        Me.txtGoodsCustCdSS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtGoodsCustCdSS.IsByteCheck = 2
        Me.txtGoodsCustCdSS.IsCalendarCheck = False
        Me.txtGoodsCustCdSS.IsDakutenCheck = False
        Me.txtGoodsCustCdSS.IsEisuCheck = False
        Me.txtGoodsCustCdSS.IsForbiddenWordsCheck = False
        Me.txtGoodsCustCdSS.IsFullByteCheck = 0
        Me.txtGoodsCustCdSS.IsHankakuCheck = False
        Me.txtGoodsCustCdSS.IsHissuCheck = False
        Me.txtGoodsCustCdSS.IsKanaCheck = False
        Me.txtGoodsCustCdSS.IsMiddleSpace = False
        Me.txtGoodsCustCdSS.IsNumericCheck = False
        Me.txtGoodsCustCdSS.IsSujiCheck = False
        Me.txtGoodsCustCdSS.IsZenkakuCheck = False
        Me.txtGoodsCustCdSS.ItemName = ""
        Me.txtGoodsCustCdSS.LineSpace = 0
        Me.txtGoodsCustCdSS.Location = New System.Drawing.Point(166, 158)
        Me.txtGoodsCustCdSS.MaxLength = 2
        Me.txtGoodsCustCdSS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCustCdSS.MaxLineCount = 0
        Me.txtGoodsCustCdSS.Multiline = False
        Me.txtGoodsCustCdSS.Name = "txtGoodsCustCdSS"
        Me.txtGoodsCustCdSS.ReadOnly = True
        Me.txtGoodsCustCdSS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCustCdSS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCustCdSS.Size = New System.Drawing.Size(43, 18)
        Me.txtGoodsCustCdSS.TabIndex = 578
        Me.txtGoodsCustCdSS.TabStop = False
        Me.txtGoodsCustCdSS.TabStopSetting = False
        Me.txtGoodsCustCdSS.TextValue = "XX"
        Me.txtGoodsCustCdSS.UseSystemPasswordChar = False
        Me.txtGoodsCustCdSS.WidthDef = 43
        Me.txtGoodsCustCdSS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbCoaYn
        '
        Me.cmbCoaYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCoaYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCoaYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCoaYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCoaYn.DataCode = "U009"
        Me.cmbCoaYn.DataSource = Nothing
        Me.cmbCoaYn.DisplayMember = Nothing
        Me.cmbCoaYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCoaYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCoaYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCoaYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCoaYn.HeightDef = 18
        Me.cmbCoaYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCoaYn.HissuLabelVisible = False
        Me.cmbCoaYn.InsertWildCard = True
        Me.cmbCoaYn.IsForbiddenWordsCheck = False
        Me.cmbCoaYn.IsHissuCheck = False
        Me.cmbCoaYn.ItemName = ""
        Me.cmbCoaYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbCoaYn.Location = New System.Drawing.Point(1024, 115)
        Me.cmbCoaYn.Name = "cmbCoaYn"
        Me.cmbCoaYn.ReadOnly = True
        Me.cmbCoaYn.SelectedIndex = -1
        Me.cmbCoaYn.SelectedItem = Nothing
        Me.cmbCoaYn.SelectedText = ""
        Me.cmbCoaYn.SelectedValue = ""
        Me.cmbCoaYn.Size = New System.Drawing.Size(133, 18)
        Me.cmbCoaYn.TabIndex = 558
        Me.cmbCoaYn.TabStop = False
        Me.cmbCoaYn.TabStopSetting = False
        Me.cmbCoaYn.TextValue = ""
        Me.cmbCoaYn.Value1 = Nothing
        Me.cmbCoaYn.Value2 = Nothing
        Me.cmbCoaYn.Value3 = Nothing
        Me.cmbCoaYn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbCoaYn.ValueMember = Nothing
        Me.cmbCoaYn.WidthDef = 133
        '
        'lblTitleCoaYn
        '
        Me.lblTitleCoaYn.AutoSize = True
        Me.lblTitleCoaYn.AutoSizeDef = True
        Me.lblTitleCoaYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCoaYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCoaYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCoaYn.EnableStatus = False
        Me.lblTitleCoaYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCoaYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCoaYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCoaYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCoaYn.HeightDef = 13
        Me.lblTitleCoaYn.Location = New System.Drawing.Point(973, 119)
        Me.lblTitleCoaYn.Name = "lblTitleCoaYn"
        Me.lblTitleCoaYn.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleCoaYn.TabIndex = 557
        Me.lblTitleCoaYn.Text = "分析表"
        Me.lblTitleCoaYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCoaYn.TextValue = "分析表"
        Me.lblTitleCoaYn.WidthDef = 49
        '
        'cmbSpNhsYn
        '
        Me.cmbSpNhsYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSpNhsYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSpNhsYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSpNhsYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSpNhsYn.DataCode = "U009"
        Me.cmbSpNhsYn.DataSource = Nothing
        Me.cmbSpNhsYn.DisplayMember = Nothing
        Me.cmbSpNhsYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSpNhsYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSpNhsYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSpNhsYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSpNhsYn.HeightDef = 18
        Me.cmbSpNhsYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSpNhsYn.HissuLabelVisible = False
        Me.cmbSpNhsYn.InsertWildCard = True
        Me.cmbSpNhsYn.IsForbiddenWordsCheck = False
        Me.cmbSpNhsYn.IsHissuCheck = False
        Me.cmbSpNhsYn.ItemName = ""
        Me.cmbSpNhsYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSpNhsYn.Location = New System.Drawing.Point(1024, 94)
        Me.cmbSpNhsYn.Name = "cmbSpNhsYn"
        Me.cmbSpNhsYn.ReadOnly = True
        Me.cmbSpNhsYn.SelectedIndex = -1
        Me.cmbSpNhsYn.SelectedItem = Nothing
        Me.cmbSpNhsYn.SelectedText = ""
        Me.cmbSpNhsYn.SelectedValue = ""
        Me.cmbSpNhsYn.Size = New System.Drawing.Size(133, 18)
        Me.cmbSpNhsYn.TabIndex = 556
        Me.cmbSpNhsYn.TabStop = False
        Me.cmbSpNhsYn.TabStopSetting = False
        Me.cmbSpNhsYn.TextValue = ""
        Me.cmbSpNhsYn.Value1 = Nothing
        Me.cmbSpNhsYn.Value2 = Nothing
        Me.cmbSpNhsYn.Value3 = Nothing
        Me.cmbSpNhsYn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSpNhsYn.ValueMember = Nothing
        Me.cmbSpNhsYn.WidthDef = 133
        '
        'lblTitleSpNhsYn
        '
        Me.lblTitleSpNhsYn.AutoSize = True
        Me.lblTitleSpNhsYn.AutoSizeDef = True
        Me.lblTitleSpNhsYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSpNhsYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSpNhsYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSpNhsYn.EnableStatus = False
        Me.lblTitleSpNhsYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSpNhsYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSpNhsYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSpNhsYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSpNhsYn.HeightDef = 13
        Me.lblTitleSpNhsYn.Location = New System.Drawing.Point(945, 96)
        Me.lblTitleSpNhsYn.Name = "lblTitleSpNhsYn"
        Me.lblTitleSpNhsYn.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSpNhsYn.TabIndex = 555
        Me.lblTitleSpNhsYn.Text = "指定納品書"
        Me.lblTitleSpNhsYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSpNhsYn.TextValue = "指定納品書"
        Me.lblTitleSpNhsYn.WidthDef = 77
        '
        'lblTitleUpGroupCd1
        '
        Me.lblTitleUpGroupCd1.AutoSize = True
        Me.lblTitleUpGroupCd1.AutoSizeDef = True
        Me.lblTitleUpGroupCd1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpGroupCd1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpGroupCd1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUpGroupCd1.EnableStatus = False
        Me.lblTitleUpGroupCd1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpGroupCd1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpGroupCd1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpGroupCd1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpGroupCd1.HeightDef = 13
        Me.lblTitleUpGroupCd1.Location = New System.Drawing.Point(614, 119)
        Me.lblTitleUpGroupCd1.Name = "lblTitleUpGroupCd1"
        Me.lblTitleUpGroupCd1.Size = New System.Drawing.Size(133, 13)
        Me.lblTitleUpGroupCd1.TabIndex = 552
        Me.lblTitleUpGroupCd1.Text = "単価グループコード"
        Me.lblTitleUpGroupCd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUpGroupCd1.TextValue = "単価グループコード"
        Me.lblTitleUpGroupCd1.WidthDef = 133
        '
        'lblTitleLotCtlKb
        '
        Me.lblTitleLotCtlKb.AutoSize = True
        Me.lblTitleLotCtlKb.AutoSizeDef = True
        Me.lblTitleLotCtlKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotCtlKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotCtlKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLotCtlKb.EnableStatus = False
        Me.lblTitleLotCtlKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotCtlKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotCtlKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotCtlKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotCtlKb.HeightDef = 13
        Me.lblTitleLotCtlKb.Location = New System.Drawing.Point(628, 160)
        Me.lblTitleLotCtlKb.Name = "lblTitleLotCtlKb"
        Me.lblTitleLotCtlKb.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleLotCtlKb.TabIndex = 83
        Me.lblTitleLotCtlKb.Text = "ロット管理レベル"
        Me.lblTitleLotCtlKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLotCtlKb.TextValue = "ロット管理レベル"
        Me.lblTitleLotCtlKb.WidthDef = 119
        '
        'cmbLotCtlKb
        '
        Me.cmbLotCtlKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLotCtlKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLotCtlKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbLotCtlKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbLotCtlKb.DataCode = "R002"
        Me.cmbLotCtlKb.DataSource = Nothing
        Me.cmbLotCtlKb.DisplayMember = Nothing
        Me.cmbLotCtlKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLotCtlKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLotCtlKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLotCtlKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLotCtlKb.HeightDef = 18
        Me.cmbLotCtlKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLotCtlKb.HissuLabelVisible = False
        Me.cmbLotCtlKb.InsertWildCard = True
        Me.cmbLotCtlKb.IsForbiddenWordsCheck = False
        Me.cmbLotCtlKb.IsHissuCheck = False
        Me.cmbLotCtlKb.ItemName = ""
        Me.cmbLotCtlKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbLotCtlKb.Location = New System.Drawing.Point(748, 158)
        Me.cmbLotCtlKb.Name = "cmbLotCtlKb"
        Me.cmbLotCtlKb.ReadOnly = True
        Me.cmbLotCtlKb.SelectedIndex = -1
        Me.cmbLotCtlKb.SelectedItem = Nothing
        Me.cmbLotCtlKb.SelectedText = ""
        Me.cmbLotCtlKb.SelectedValue = ""
        Me.cmbLotCtlKb.Size = New System.Drawing.Size(153, 18)
        Me.cmbLotCtlKb.TabIndex = 82
        Me.cmbLotCtlKb.TabStop = False
        Me.cmbLotCtlKb.TabStopSetting = False
        Me.cmbLotCtlKb.TextValue = ""
        Me.cmbLotCtlKb.Value1 = Nothing
        Me.cmbLotCtlKb.Value2 = Nothing
        Me.cmbLotCtlKb.Value3 = Nothing
        Me.cmbLotCtlKb.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbLotCtlKb.ValueMember = Nothing
        Me.cmbLotCtlKb.WidthDef = 153
        '
        'lblTitleTareYn
        '
        Me.lblTitleTareYn.AutoSize = True
        Me.lblTitleTareYn.AutoSizeDef = True
        Me.lblTitleTareYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTareYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTareYn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTareYn.EnableStatus = False
        Me.lblTitleTareYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTareYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTareYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTareYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTareYn.HeightDef = 13
        Me.lblTitleTareYn.Location = New System.Drawing.Point(656, 96)
        Me.lblTitleTareYn.Name = "lblTitleTareYn"
        Me.lblTitleTareYn.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleTareYn.TabIndex = 81
        Me.lblTitleTareYn.Text = "風袋重量加算"
        Me.lblTitleTareYn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTareYn.TextValue = "風袋重量加算"
        Me.lblTitleTareYn.WidthDef = 91
        '
        'cmbTareYn
        '
        Me.cmbTareYn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTareYn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTareYn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTareYn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTareYn.DataCode = "U009"
        Me.cmbTareYn.DataSource = Nothing
        Me.cmbTareYn.DisplayMember = Nothing
        Me.cmbTareYn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTareYn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTareYn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTareYn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTareYn.HeightDef = 18
        Me.cmbTareYn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTareYn.HissuLabelVisible = False
        Me.cmbTareYn.InsertWildCard = True
        Me.cmbTareYn.IsForbiddenWordsCheck = False
        Me.cmbTareYn.IsHissuCheck = False
        Me.cmbTareYn.ItemName = ""
        Me.cmbTareYn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTareYn.Location = New System.Drawing.Point(749, 94)
        Me.cmbTareYn.Name = "cmbTareYn"
        Me.cmbTareYn.ReadOnly = True
        Me.cmbTareYn.SelectedIndex = -1
        Me.cmbTareYn.SelectedItem = Nothing
        Me.cmbTareYn.SelectedText = ""
        Me.cmbTareYn.SelectedValue = ""
        Me.cmbTareYn.Size = New System.Drawing.Size(133, 18)
        Me.cmbTareYn.TabIndex = 80
        Me.cmbTareYn.TabStop = False
        Me.cmbTareYn.TabStopSetting = False
        Me.cmbTareYn.TextValue = ""
        Me.cmbTareYn.Value1 = Nothing
        Me.cmbTareYn.Value2 = Nothing
        Me.cmbTareYn.Value3 = Nothing
        Me.cmbTareYn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbTareYn.ValueMember = Nothing
        Me.cmbTareYn.WidthDef = 133
        '
        'txtCustNm
        '
        Me.txtCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustNm.CountWrappedLine = False
        Me.txtCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustNm.HeightDef = 18
        Me.txtCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.HissuLabelVisible = False
        Me.txtCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustNm.IsByteCheck = 0
        Me.txtCustNm.IsCalendarCheck = False
        Me.txtCustNm.IsDakutenCheck = False
        Me.txtCustNm.IsEisuCheck = False
        Me.txtCustNm.IsForbiddenWordsCheck = False
        Me.txtCustNm.IsFullByteCheck = 0
        Me.txtCustNm.IsHankakuCheck = False
        Me.txtCustNm.IsHissuCheck = False
        Me.txtCustNm.IsKanaCheck = False
        Me.txtCustNm.IsMiddleSpace = False
        Me.txtCustNm.IsNumericCheck = False
        Me.txtCustNm.IsSujiCheck = False
        Me.txtCustNm.IsZenkakuCheck = False
        Me.txtCustNm.ItemName = ""
        Me.txtCustNm.LineSpace = 0
        Me.txtCustNm.Location = New System.Drawing.Point(556, 15)
        Me.txtCustNm.MaxLength = 0
        Me.txtCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustNm.MaxLineCount = 30
        Me.txtCustNm.Multiline = False
        Me.txtCustNm.Name = "txtCustNm"
        Me.txtCustNm.ReadOnly = True
        Me.txtCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustNm.Size = New System.Drawing.Size(398, 18)
        Me.txtCustNm.TabIndex = 551
        Me.txtCustNm.TabStop = False
        Me.txtCustNm.TabStopSetting = False
        Me.txtCustNm.TextValue = ""
        Me.txtCustNm.UseSystemPasswordChar = False
        Me.txtCustNm.WidthDef = 398
        Me.txtCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMI110F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.FocusedControlName = "sprGoodsDetail"
        Me.Name = "LMI110F"
        Me.Text = "【LMI110】日医工製品マスタメンテ"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprNichikoGoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNichikoGoods.ResumeLayout(False)
        Me.pnlNichikoGoods.PerformLayout()
        Me.LmGroupBox1.ResumeLayout(False)
        Me.LmGroupBox1.PerformLayout()
        Me.LmGroupBox2.ResumeLayout(False)
        Me.LmGroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprNichikoGoods As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleFromTo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdEdiDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleEdiDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdEdiDateFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents pnlNichikoGoods As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkNotImport As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleImportKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkImport As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents txtSerchGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSerchGoodsCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmGroupBox1 As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleLotCtlKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbLotCtlKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTareYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTareYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleUpGroupCd1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSpNhsYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSpNhsYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbCrtDateCtlKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleCrtDateCtlKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbCoaYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleCoaYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSkyuMeiYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSkyuMeiYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbHikiateAlertYn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleHikiateAlertYn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmGroupBox2 As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustNmSS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustNmS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustCdS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCustCdSS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUpGroupCd1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsNm1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsNm1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsNm2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsNm2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsKey As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsKey As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
