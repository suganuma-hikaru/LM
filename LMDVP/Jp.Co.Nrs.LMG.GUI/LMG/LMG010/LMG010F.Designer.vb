<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMG010F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMG010F))
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.chkSelectByNrsB = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblTitleSeikyuKikan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.optSeikyuH = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optSeikyuC = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.chkMikan = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.lblTitleSimebi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbSimebi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleMode = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNyusyukkaMikan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpMode = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbBatch = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.optSeikyuGC = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.grpSelect = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.imdInvDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMode.SuspendLayout()
        Me.grpSelect.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpSelect)
        Me.pnlViewAria.Controls.Add(Me.grpMode)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(58, 18)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 1
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblCustNm
        '
        Me.lblCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNm.CountWrappedLine = False
        Me.lblCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNm.HeightDef = 18
        Me.lblCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNm.HissuLabelVisible = False
        Me.lblCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNm.IsByteCheck = 0
        Me.lblCustNm.IsCalendarCheck = False
        Me.lblCustNm.IsDakutenCheck = False
        Me.lblCustNm.IsEisuCheck = False
        Me.lblCustNm.IsForbiddenWordsCheck = False
        Me.lblCustNm.IsFullByteCheck = 0
        Me.lblCustNm.IsHankakuCheck = False
        Me.lblCustNm.IsHissuCheck = False
        Me.lblCustNm.IsKanaCheck = False
        Me.lblCustNm.IsMiddleSpace = False
        Me.lblCustNm.IsNumericCheck = False
        Me.lblCustNm.IsSujiCheck = False
        Me.lblCustNm.IsZenkakuCheck = False
        Me.lblCustNm.ItemName = ""
        Me.lblCustNm.LineSpace = 0
        Me.lblCustNm.Location = New System.Drawing.Point(200, 38)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(565, 18)
        Me.lblCustNm.TabIndex = 7
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 565
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdM.Location = New System.Drawing.Point(171, 38)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdM.TabIndex = 18
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 45
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCustCd
        '
        Me.lblTitleCustCd.AutoSize = True
        Me.lblTitleCustCd.AutoSizeDef = True
        Me.lblTitleCustCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCustCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCustCd.EnableStatus = False
        Me.lblTitleCustCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCustCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCustCd.HeightDef = 13
        Me.lblTitleCustCd.Location = New System.Drawing.Point(72, 41)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustCd.TabIndex = 5
        Me.lblTitleCustCd.Text = "荷主"
        Me.lblTitleCustCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustCd.TextValue = "荷主"
        Me.lblTitleCustCd.WidthDef = 35
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
        Me.txtCustCdL.Location = New System.Drawing.Point(119, 38)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(82, 18)
        Me.txtCustCdL.TabIndex = 6
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 82
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'chkSelectByNrsB
        '
        Me.chkSelectByNrsB.AutoSize = True
        Me.chkSelectByNrsB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSelectByNrsB.EnableStatus = True
        Me.chkSelectByNrsB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.HeightDef = 17
        Me.chkSelectByNrsB.Location = New System.Drawing.Point(526, 62)
        Me.chkSelectByNrsB.Name = "chkSelectByNrsB"
        Me.chkSelectByNrsB.Size = New System.Drawing.Size(124, 17)
        Me.chkSelectByNrsB.TabIndex = 3
        Me.chkSelectByNrsB.TabStopSetting = True
        Me.chkSelectByNrsB.Text = "担当分のみ表示"
        Me.chkSelectByNrsB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSelectByNrsB.TextValue = "担当分のみ表示"
        Me.chkSelectByNrsB.UseVisualStyleBackColor = True
        Me.chkSelectByNrsB.WidthDef = 124
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
        Me.sprDetail.HeightDef = 680
        Me.sprDetail.Location = New System.Drawing.Point(12, 180)
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
        Me.sprDetail.Size = New System.Drawing.Size(1257, 680)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Bottom
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1257
        '
        'lblTitleSeikyuKikan
        '
        Me.lblTitleSeikyuKikan.AutoSize = True
        Me.lblTitleSeikyuKikan.AutoSizeDef = True
        Me.lblTitleSeikyuKikan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuKikan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuKikan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuKikan.EnableStatus = False
        Me.lblTitleSeikyuKikan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuKikan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuKikan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuKikan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuKikan.HeightDef = 13
        Me.lblTitleSeikyuKikan.Location = New System.Drawing.Point(272, 64)
        Me.lblTitleSeikyuKikan.Name = "lblTitleSeikyuKikan"
        Me.lblTitleSeikyuKikan.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeikyuKikan.TabIndex = 19
        Me.lblTitleSeikyuKikan.Text = "請求月"
        Me.lblTitleSeikyuKikan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuKikan.TextValue = "請求月"
        Me.lblTitleSeikyuKikan.WidthDef = 49
        '
        'optSeikyuH
        '
        Me.optSeikyuH.AutoSize = True
        Me.optSeikyuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuH.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSeikyuH.EnableStatus = True
        Me.optSeikyuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuH.HeightDef = 17
        Me.optSeikyuH.Location = New System.Drawing.Point(120, 20)
        Me.optSeikyuH.Name = "optSeikyuH"
        Me.optSeikyuH.Size = New System.Drawing.Size(53, 17)
        Me.optSeikyuH.TabIndex = 2
        Me.optSeikyuH.TabStop = True
        Me.optSeikyuH.TabStopSetting = True
        Me.optSeikyuH.Text = "本番"
        Me.optSeikyuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSeikyuH.TextValue = "本番"
        Me.optSeikyuH.UseVisualStyleBackColor = True
        Me.optSeikyuH.WidthDef = 53
        '
        'optSeikyuC
        '
        Me.optSeikyuC.AutoSize = True
        Me.optSeikyuC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuC.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuC.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSeikyuC.EnableStatus = True
        Me.optSeikyuC.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuC.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuC.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuC.HeightDef = 17
        Me.optSeikyuC.Location = New System.Drawing.Point(190, 20)
        Me.optSeikyuC.Name = "optSeikyuC"
        Me.optSeikyuC.Size = New System.Drawing.Size(81, 17)
        Me.optSeikyuC.TabIndex = 1
        Me.optSeikyuC.TabStop = True
        Me.optSeikyuC.TabStopSetting = True
        Me.optSeikyuC.Text = "チェック"
        Me.optSeikyuC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSeikyuC.TextValue = "チェック"
        Me.optSeikyuC.UseVisualStyleBackColor = True
        Me.optSeikyuC.WidthDef = 81
        '
        'chkMikan
        '
        Me.chkMikan.AutoSize = True
        Me.chkMikan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMikan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMikan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMikan.EnableStatus = True
        Me.chkMikan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMikan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMikan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMikan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMikan.HeightDef = 17
        Me.chkMikan.Location = New System.Drawing.Point(120, 45)
        Me.chkMikan.Name = "chkMikan"
        Me.chkMikan.Size = New System.Drawing.Size(306, 17)
        Me.chkMikan.TabIndex = 3
        Me.chkMikan.TabStopSetting = True
        Me.chkMikan.Text = "未完了があっても計算する（チェックのみ）"
        Me.chkMikan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMikan.TextValue = "未完了があっても計算する（チェックのみ）"
        Me.chkMikan.UseVisualStyleBackColor = True
        Me.chkMikan.WidthDef = 306
        '
        'lblTitleSimebi
        '
        Me.lblTitleSimebi.AutoSize = True
        Me.lblTitleSimebi.AutoSizeDef = True
        Me.lblTitleSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSimebi.EnableStatus = False
        Me.lblTitleSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.HeightDef = 13
        Me.lblTitleSimebi.Location = New System.Drawing.Point(72, 64)
        Me.lblTitleSimebi.Name = "lblTitleSimebi"
        Me.lblTitleSimebi.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSimebi.TabIndex = 21
        Me.lblTitleSimebi.Text = "締日"
        Me.lblTitleSimebi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSimebi.TextValue = "締日"
        Me.lblTitleSimebi.WidthDef = 35
        '
        'cmbSimebi
        '
        Me.cmbSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSimebi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSimebi.DataCode = "S008"
        Me.cmbSimebi.DataSource = Nothing
        Me.cmbSimebi.DisplayMember = Nothing
        Me.cmbSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.HeightDef = 18
        Me.cmbSimebi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSimebi.HissuLabelVisible = True
        Me.cmbSimebi.InsertWildCard = True
        Me.cmbSimebi.IsForbiddenWordsCheck = False
        Me.cmbSimebi.IsHissuCheck = True
        Me.cmbSimebi.ItemName = ""
        Me.cmbSimebi.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSimebi.Location = New System.Drawing.Point(119, 61)
        Me.cmbSimebi.Name = "cmbSimebi"
        Me.cmbSimebi.ReadOnly = False
        Me.cmbSimebi.SelectedIndex = -1
        Me.cmbSimebi.SelectedItem = Nothing
        Me.cmbSimebi.SelectedText = ""
        Me.cmbSimebi.SelectedValue = ""
        Me.cmbSimebi.Size = New System.Drawing.Size(127, 18)
        Me.cmbSimebi.TabIndex = 27
        Me.cmbSimebi.TabStopSetting = True
        Me.cmbSimebi.TextValue = ""
        Me.cmbSimebi.Value1 = Nothing
        Me.cmbSimebi.Value2 = Nothing
        Me.cmbSimebi.Value3 = Nothing
        Me.cmbSimebi.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSimebi.ValueMember = Nothing
        Me.cmbSimebi.WidthDef = 127
        '
        'lblTitleMode
        '
        Me.lblTitleMode.AutoSize = True
        Me.lblTitleMode.AutoSizeDef = True
        Me.lblTitleMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMode.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMode.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMode.EnableStatus = False
        Me.lblTitleMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMode.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMode.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMode.HeightDef = 13
        Me.lblTitleMode.Location = New System.Drawing.Point(28, 22)
        Me.lblTitleMode.Name = "lblTitleMode"
        Me.lblTitleMode.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleMode.TabIndex = 28
        Me.lblTitleMode.Text = "実行モード"
        Me.lblTitleMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMode.TextValue = "実行モード"
        Me.lblTitleMode.WidthDef = 77
        '
        'lblTitleNyusyukkaMikan
        '
        Me.lblTitleNyusyukkaMikan.AutoSize = True
        Me.lblTitleNyusyukkaMikan.AutoSizeDef = True
        Me.lblTitleNyusyukkaMikan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyusyukkaMikan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNyusyukkaMikan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNyusyukkaMikan.EnableStatus = False
        Me.lblTitleNyusyukkaMikan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyusyukkaMikan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNyusyukkaMikan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyusyukkaMikan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNyusyukkaMikan.HeightDef = 13
        Me.lblTitleNyusyukkaMikan.Location = New System.Drawing.Point(14, 46)
        Me.lblTitleNyusyukkaMikan.Name = "lblTitleNyusyukkaMikan"
        Me.lblTitleNyusyukkaMikan.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleNyusyukkaMikan.TabIndex = 31
        Me.lblTitleNyusyukkaMikan.Text = "入出荷未完了"
        Me.lblTitleNyusyukkaMikan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNyusyukkaMikan.TextValue = "入出荷未完了"
        Me.lblTitleNyusyukkaMikan.WidthDef = 91
        '
        'grpMode
        '
        Me.grpMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMode.Controls.Add(Me.cmbBatch)
        Me.grpMode.Controls.Add(Me.optSeikyuGC)
        Me.grpMode.Controls.Add(Me.LmTitleLabel1)
        Me.grpMode.Controls.Add(Me.lblTitleMode)
        Me.grpMode.Controls.Add(Me.optSeikyuC)
        Me.grpMode.Controls.Add(Me.optSeikyuH)
        Me.grpMode.Controls.Add(Me.chkMikan)
        Me.grpMode.Controls.Add(Me.lblTitleNyusyukkaMikan)
        Me.grpMode.EnableStatus = False
        Me.grpMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.HeightDef = 71
        Me.grpMode.Location = New System.Drawing.Point(12, 103)
        Me.grpMode.Name = "grpMode"
        Me.grpMode.Size = New System.Drawing.Size(675, 71)
        Me.grpMode.TabIndex = 33
        Me.grpMode.TabStop = False
        Me.grpMode.Text = "モード条件"
        Me.grpMode.TextValue = "モード条件"
        Me.grpMode.WidthDef = 675
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
        Me.cmbBatch.HissuLabelVisible = False
        Me.cmbBatch.InsertWildCard = True
        Me.cmbBatch.IsForbiddenWordsCheck = False
        Me.cmbBatch.IsHissuCheck = False
        Me.cmbBatch.ItemName = ""
        Me.cmbBatch.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBatch.Location = New System.Drawing.Point(503, 19)
        Me.cmbBatch.Name = "cmbBatch"
        Me.cmbBatch.ReadOnly = False
        Me.cmbBatch.SelectedIndex = -1
        Me.cmbBatch.SelectedItem = Nothing
        Me.cmbBatch.SelectedText = ""
        Me.cmbBatch.SelectedValue = ""
        Me.cmbBatch.Size = New System.Drawing.Size(157, 18)
        Me.cmbBatch.TabIndex = 32
        Me.cmbBatch.TabStopSetting = True
        Me.cmbBatch.TextValue = ""
        Me.cmbBatch.Value1 = Nothing
        Me.cmbBatch.Value2 = Nothing
        Me.cmbBatch.Value3 = Nothing
        Me.cmbBatch.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbBatch.ValueMember = Nothing
        Me.cmbBatch.WidthDef = 157
        '
        'optSeikyuGC
        '
        Me.optSeikyuGC.AutoSize = True
        Me.optSeikyuGC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuGC.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuGC.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSeikyuGC.EnableStatus = True
        Me.optSeikyuGC.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuGC.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuGC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuGC.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuGC.HeightDef = 17
        Me.optSeikyuGC.Location = New System.Drawing.Point(289, 20)
        Me.optSeikyuGC.Name = "optSeikyuGC"
        Me.optSeikyuGC.Size = New System.Drawing.Size(109, 17)
        Me.optSeikyuGC.TabIndex = 3
        Me.optSeikyuGC.TabStop = True
        Me.optSeikyuGC.TabStopSetting = True
        Me.optSeikyuGC.Text = "業務チェック"
        Me.optSeikyuGC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSeikyuGC.TextValue = "業務チェック"
        Me.optSeikyuGC.UseVisualStyleBackColor = True
        Me.optSeikyuGC.WidthDef = 109
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(420, 22)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel1.TabIndex = 28
        Me.LmTitleLabel1.Text = "バッチ条件"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "バッチ条件"
        Me.LmTitleLabel1.WidthDef = 77
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbBr.Location = New System.Drawing.Point(119, 15)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = False
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 128
        Me.cmbBr.TabStopSetting = True
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
        '
        'grpSelect
        '
        Me.grpSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSelect.Controls.Add(Me.imdInvDate)
        Me.grpSelect.Controls.Add(Me.lblCustNm)
        Me.grpSelect.Controls.Add(Me.txtCustCdM)
        Me.grpSelect.Controls.Add(Me.cmbBr)
        Me.grpSelect.Controls.Add(Me.lblTitleEigyo)
        Me.grpSelect.Controls.Add(Me.txtCustCdL)
        Me.grpSelect.Controls.Add(Me.lblTitleCustCd)
        Me.grpSelect.Controls.Add(Me.chkSelectByNrsB)
        Me.grpSelect.Controls.Add(Me.lblTitleSimebi)
        Me.grpSelect.Controls.Add(Me.cmbSimebi)
        Me.grpSelect.Controls.Add(Me.lblTitleSeikyuKikan)
        Me.grpSelect.EnableStatus = False
        Me.grpSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.HeightDef = 90
        Me.grpSelect.Location = New System.Drawing.Point(12, 10)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(866, 90)
        Me.grpSelect.TabIndex = 129
        Me.grpSelect.TabStop = False
        Me.grpSelect.Text = "検索条件"
        Me.grpSelect.TextValue = "検索条件"
        Me.grpSelect.WidthDef = 866
        '
        'imdInvDate
        '
        Me.imdInvDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInvDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdInvDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInvDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInvDate.HeightDef = 18
        Me.imdInvDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInvDate.HissuLabelVisible = True
        Me.imdInvDate.Holiday = True
        Me.imdInvDate.IsAfterDateCheck = False
        Me.imdInvDate.IsBeforeDateCheck = False
        Me.imdInvDate.IsHissuCheck = True
        Me.imdInvDate.IsMinDateCheck = "1900/01/01"
        Me.imdInvDate.ItemName = ""
        Me.imdInvDate.Location = New System.Drawing.Point(333, 61)
        Me.imdInvDate.Name = "imdInvDate"
        Me.imdInvDate.Number = CType(0, Long)
        Me.imdInvDate.ReadOnly = False
        Me.imdInvDate.Size = New System.Drawing.Size(115, 18)
        Me.imdInvDate.TabIndex = 130
        Me.imdInvDate.TabStopSetting = True
        Me.imdInvDate.TextValue = ""
        Me.imdInvDate.Value = New Date(CType(0, Long))
        Me.imdInvDate.WidthDef = 115
        '
        'LMG010F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMG010F"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "【LMG010】 保管料・荷役料計算"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMode.ResumeLayout(False)
        Me.grpMode.PerformLayout()
        Me.grpSelect.ResumeLayout(False)
        Me.grpSelect.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkSelectByNrsB As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents chkMikan As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents optSeikyuH As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optSeikyuC As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleSeikyuKikan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNyusyukkaMikan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleMode As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSimebi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSimebi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpMode As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents grpSelect As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents imdInvDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents optSeikyuGC As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents cmbBatch As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
