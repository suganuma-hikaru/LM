<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF210F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMF210F))
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
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.pnlCondition = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleDo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numOnkanTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleKara3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numOnkanFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleOnkan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numLoadWtZanTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleKara2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numLoadWtZanFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleLoadWtZan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleUncoDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKara1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.imdFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCondition.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.pnlCondition)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
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
        Me.sprDetail.HeightDef = 484
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(25, 134)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        'RowCount = 1
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        Me.sprDetail.Size = New System.Drawing.Size(967, 484)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 3
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 967
        '
        'pnlCondition
        '
        Me.pnlCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCondition.Controls.Add(Me.lblTitleDo)
        Me.pnlCondition.Controls.Add(Me.numOnkanTo)
        Me.pnlCondition.Controls.Add(Me.lblTitleKara3)
        Me.pnlCondition.Controls.Add(Me.numOnkanFrom)
        Me.pnlCondition.Controls.Add(Me.lblTitleOnkan)
        Me.pnlCondition.Controls.Add(Me.lblTitleKg)
        Me.pnlCondition.Controls.Add(Me.numLoadWtZanTo)
        Me.pnlCondition.Controls.Add(Me.lblTitleKara2)
        Me.pnlCondition.Controls.Add(Me.numLoadWtZanFrom)
        Me.pnlCondition.Controls.Add(Me.lblTitleLoadWtZan)
        Me.pnlCondition.Controls.Add(Me.lblTitleUncoDate)
        Me.pnlCondition.Controls.Add(Me.lblTitleKara1)
        Me.pnlCondition.Controls.Add(Me.imdTo)
        Me.pnlCondition.Controls.Add(Me.imdFrom)
        Me.pnlCondition.Controls.Add(Me.cmbEigyo)
        Me.pnlCondition.Controls.Add(Me.lblTitleEigyo)
        Me.pnlCondition.EnableStatus = False
        Me.pnlCondition.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.HeightDef = 121
        Me.pnlCondition.Location = New System.Drawing.Point(25, 7)
        Me.pnlCondition.Name = "pnlCondition"
        Me.pnlCondition.Size = New System.Drawing.Size(630, 121)
        Me.pnlCondition.TabIndex = 8
        Me.pnlCondition.TabStop = False
        Me.pnlCondition.Text = "検索条件"
        Me.pnlCondition.TextValue = "検索条件"
        Me.pnlCondition.WidthDef = 630
        '
        'lblTitleDo
        '
        Me.lblTitleDo.AutoSize = True
        Me.lblTitleDo.AutoSizeDef = True
        Me.lblTitleDo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDo.EnableStatus = False
        Me.lblTitleDo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo.HeightDef = 13
        Me.lblTitleDo.Location = New System.Drawing.Point(335, 88)
        Me.lblTitleDo.Name = "lblTitleDo"
        Me.lblTitleDo.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleDo.TabIndex = 323
        Me.lblTitleDo.Text = "℃"
        Me.lblTitleDo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDo.TextValue = "℃"
        Me.lblTitleDo.WidthDef = 21
        '
        'numOnkanTo
        '
        Me.numOnkanTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOnkanTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOnkanTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numOnkanTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numOnkanTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOnkanTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOnkanTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOnkanTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOnkanTo.HeightDef = 18
        Me.numOnkanTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOnkanTo.HissuLabelVisible = False
        Me.numOnkanTo.IsHissuCheck = False
        Me.numOnkanTo.IsRangeCheck = False
        Me.numOnkanTo.ItemName = ""
        Me.numOnkanTo.Location = New System.Drawing.Point(232, 85)
        Me.numOnkanTo.Name = "numOnkanTo"
        Me.numOnkanTo.ReadOnly = False
        Me.numOnkanTo.Size = New System.Drawing.Size(118, 18)
        Me.numOnkanTo.TabIndex = 321
        Me.numOnkanTo.TabStopSetting = True
        Me.numOnkanTo.TextValue = "0"
        Me.numOnkanTo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numOnkanTo.WidthDef = 118
        '
        'lblTitleKara3
        '
        Me.lblTitleKara3.AutoSize = True
        Me.lblTitleKara3.AutoSizeDef = True
        Me.lblTitleKara3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKara3.EnableStatus = False
        Me.lblTitleKara3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara3.HeightDef = 13
        Me.lblTitleKara3.Location = New System.Drawing.Point(211, 88)
        Me.lblTitleKara3.Name = "lblTitleKara3"
        Me.lblTitleKara3.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKara3.TabIndex = 322
        Me.lblTitleKara3.Text = "～"
        Me.lblTitleKara3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKara3.TextValue = "～"
        Me.lblTitleKara3.WidthDef = 21
        '
        'numOnkanFrom
        '
        Me.numOnkanFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOnkanFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numOnkanFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numOnkanFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numOnkanFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOnkanFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOnkanFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOnkanFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOnkanFrom.HeightDef = 18
        Me.numOnkanFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOnkanFrom.HissuLabelVisible = False
        Me.numOnkanFrom.IsHissuCheck = False
        Me.numOnkanFrom.IsRangeCheck = False
        Me.numOnkanFrom.ItemName = ""
        Me.numOnkanFrom.Location = New System.Drawing.Point(107, 85)
        Me.numOnkanFrom.Name = "numOnkanFrom"
        Me.numOnkanFrom.ReadOnly = False
        Me.numOnkanFrom.Size = New System.Drawing.Size(118, 18)
        Me.numOnkanFrom.TabIndex = 320
        Me.numOnkanFrom.TabStopSetting = True
        Me.numOnkanFrom.TextValue = "0"
        Me.numOnkanFrom.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numOnkanFrom.WidthDef = 118
        '
        'lblTitleOnkan
        '
        Me.lblTitleOnkan.AutoSize = True
        Me.lblTitleOnkan.AutoSizeDef = True
        Me.lblTitleOnkan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOnkan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOnkan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOnkan.EnableStatus = False
        Me.lblTitleOnkan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOnkan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOnkan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOnkan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOnkan.HeightDef = 13
        Me.lblTitleOnkan.Location = New System.Drawing.Point(44, 88)
        Me.lblTitleOnkan.Name = "lblTitleOnkan"
        Me.lblTitleOnkan.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleOnkan.TabIndex = 319
        Me.lblTitleOnkan.Text = "管理温度"
        Me.lblTitleOnkan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOnkan.TextValue = "管理温度"
        Me.lblTitleOnkan.WidthDef = 63
        '
        'lblTitleKg
        '
        Me.lblTitleKg.AutoSize = True
        Me.lblTitleKg.AutoSizeDef = True
        Me.lblTitleKg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg.EnableStatus = False
        Me.lblTitleKg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.HeightDef = 13
        Me.lblTitleKg.Location = New System.Drawing.Point(392, 45)
        Me.lblTitleKg.Name = "lblTitleKg"
        Me.lblTitleKg.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg.TabIndex = 318
        Me.lblTitleKg.Text = "KG"
        Me.lblTitleKg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg.TextValue = "KG"
        Me.lblTitleKg.WidthDef = 21
        '
        'numLoadWtZanTo
        '
        Me.numLoadWtZanTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numLoadWtZanTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numLoadWtZanTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numLoadWtZanTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numLoadWtZanTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWtZanTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWtZanTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWtZanTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWtZanTo.HeightDef = 18
        Me.numLoadWtZanTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numLoadWtZanTo.HissuLabelVisible = False
        Me.numLoadWtZanTo.IsHissuCheck = False
        Me.numLoadWtZanTo.IsRangeCheck = False
        Me.numLoadWtZanTo.ItemName = ""
        Me.numLoadWtZanTo.Location = New System.Drawing.Point(267, 43)
        Me.numLoadWtZanTo.Name = "numLoadWtZanTo"
        Me.numLoadWtZanTo.ReadOnly = False
        Me.numLoadWtZanTo.Size = New System.Drawing.Size(136, 18)
        Me.numLoadWtZanTo.TabIndex = 316
        Me.numLoadWtZanTo.TabStopSetting = True
        Me.numLoadWtZanTo.TextValue = "0"
        Me.numLoadWtZanTo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numLoadWtZanTo.WidthDef = 136
        '
        'lblTitleKara2
        '
        Me.lblTitleKara2.AutoSize = True
        Me.lblTitleKara2.AutoSizeDef = True
        Me.lblTitleKara2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKara2.EnableStatus = False
        Me.lblTitleKara2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara2.HeightDef = 13
        Me.lblTitleKara2.Location = New System.Drawing.Point(240, 45)
        Me.lblTitleKara2.Name = "lblTitleKara2"
        Me.lblTitleKara2.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKara2.TabIndex = 317
        Me.lblTitleKara2.Text = "～"
        Me.lblTitleKara2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKara2.TextValue = "～"
        Me.lblTitleKara2.WidthDef = 21
        '
        'numLoadWtZanFrom
        '
        Me.numLoadWtZanFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numLoadWtZanFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numLoadWtZanFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numLoadWtZanFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numLoadWtZanFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWtZanFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWtZanFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWtZanFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWtZanFrom.HeightDef = 18
        Me.numLoadWtZanFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numLoadWtZanFrom.HissuLabelVisible = False
        Me.numLoadWtZanFrom.IsHissuCheck = False
        Me.numLoadWtZanFrom.IsRangeCheck = False
        Me.numLoadWtZanFrom.ItemName = ""
        Me.numLoadWtZanFrom.Location = New System.Drawing.Point(107, 43)
        Me.numLoadWtZanFrom.Name = "numLoadWtZanFrom"
        Me.numLoadWtZanFrom.ReadOnly = False
        Me.numLoadWtZanFrom.Size = New System.Drawing.Size(136, 18)
        Me.numLoadWtZanFrom.TabIndex = 315
        Me.numLoadWtZanFrom.TabStopSetting = True
        Me.numLoadWtZanFrom.TextValue = "0"
        Me.numLoadWtZanFrom.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numLoadWtZanFrom.WidthDef = 136
        '
        'lblTitleLoadWtZan
        '
        Me.lblTitleLoadWtZan.AutoSize = True
        Me.lblTitleLoadWtZan.AutoSizeDef = True
        Me.lblTitleLoadWtZan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoadWtZan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoadWtZan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLoadWtZan.EnableStatus = False
        Me.lblTitleLoadWtZan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoadWtZan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoadWtZan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoadWtZan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoadWtZan.HeightDef = 13
        Me.lblTitleLoadWtZan.Location = New System.Drawing.Point(30, 46)
        Me.lblTitleLoadWtZan.Name = "lblTitleLoadWtZan"
        Me.lblTitleLoadWtZan.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleLoadWtZan.TabIndex = 314
        Me.lblTitleLoadWtZan.Text = "積載重量残"
        Me.lblTitleLoadWtZan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLoadWtZan.TextValue = "積載重量残"
        Me.lblTitleLoadWtZan.WidthDef = 77
        '
        'lblTitleUncoDate
        '
        Me.lblTitleUncoDate.AutoSize = True
        Me.lblTitleUncoDate.AutoSizeDef = True
        Me.lblTitleUncoDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUncoDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUncoDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUncoDate.EnableStatus = False
        Me.lblTitleUncoDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUncoDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUncoDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUncoDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUncoDate.HeightDef = 13
        Me.lblTitleUncoDate.Location = New System.Drawing.Point(30, 67)
        Me.lblTitleUncoDate.Name = "lblTitleUncoDate"
        Me.lblTitleUncoDate.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleUncoDate.TabIndex = 313
        Me.lblTitleUncoDate.Text = "運行予定日"
        Me.lblTitleUncoDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUncoDate.TextValue = "運行予定日"
        Me.lblTitleUncoDate.WidthDef = 77
        '
        'lblTitleKara1
        '
        Me.lblTitleKara1.AutoSize = True
        Me.lblTitleKara1.AutoSizeDef = True
        Me.lblTitleKara1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKara1.EnableStatus = False
        Me.lblTitleKara1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara1.HeightDef = 13
        Me.lblTitleKara1.Location = New System.Drawing.Point(211, 69)
        Me.lblTitleKara1.Name = "lblTitleKara1"
        Me.lblTitleKara1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKara1.TabIndex = 311
        Me.lblTitleKara1.Text = "～"
        Me.lblTitleKara1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKara1.TextValue = "～"
        Me.lblTitleKara1.WidthDef = 21
        '
        'imdTo
        '
        Me.imdTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdTo.HeightDef = 18
        Me.imdTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdTo.HissuLabelVisible = False
        Me.imdTo.Holiday = True
        Me.imdTo.IsAfterDateCheck = False
        Me.imdTo.IsBeforeDateCheck = False
        Me.imdTo.IsHissuCheck = False
        Me.imdTo.IsMinDateCheck = "1900/01/01"
        Me.imdTo.ItemName = ""
        Me.imdTo.Location = New System.Drawing.Point(232, 64)
        Me.imdTo.Name = "imdTo"
        Me.imdTo.Number = CType(10101000000, Long)
        Me.imdTo.ReadOnly = False
        Me.imdTo.Size = New System.Drawing.Size(118, 18)
        Me.imdTo.TabIndex = 312
        Me.imdTo.TabStopSetting = True
        Me.imdTo.TextValue = ""
        Me.imdTo.Value = New Date(CType(0, Long))
        Me.imdTo.WidthDef = 118
        '
        'imdFrom
        '
        Me.imdFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdFrom.HeightDef = 18
        Me.imdFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdFrom.HissuLabelVisible = False
        Me.imdFrom.Holiday = True
        Me.imdFrom.IsAfterDateCheck = False
        Me.imdFrom.IsBeforeDateCheck = False
        Me.imdFrom.IsHissuCheck = False
        Me.imdFrom.IsMinDateCheck = "1900/01/01"
        Me.imdFrom.ItemName = ""
        Me.imdFrom.Location = New System.Drawing.Point(107, 64)
        Me.imdFrom.Name = "imdFrom"
        Me.imdFrom.Number = CType(10101000000, Long)
        Me.imdFrom.ReadOnly = False
        Me.imdFrom.Size = New System.Drawing.Size(118, 18)
        Me.imdFrom.TabIndex = 310
        Me.imdFrom.TabStopSetting = True
        Me.imdFrom.TextValue = ""
        Me.imdFrom.Value = New Date(CType(0, Long))
        Me.imdFrom.WidthDef = 118
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
        Me.cmbEigyo.Location = New System.Drawing.Point(107, 22)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(297, 18)
        Me.cmbEigyo.TabIndex = 122
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 297
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(58, 25)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 121
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'LMF210F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 706)
        Me.Name = "LMF210F"
        Me.Text = "【LMF210】 運行情報一覧検索"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCondition.ResumeLayout(False)
        Me.pnlCondition.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents pnlCondition As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleDo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numOnkanTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKara3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numOnkanFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleOnkan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numLoadWtZanTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKara2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numLoadWtZanFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleLoadWtZan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUncoDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKara1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
End Class
