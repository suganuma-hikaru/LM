<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMB040F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopLL

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
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField4 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField4 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblSAGYO_USER_NM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNmLM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSAGYO_USER_CD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdSysEntDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.chkMishori = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.imdSysEntDateTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblFromTo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblFromTo)
        Me.pnlViewAria.Controls.Add(Me.imdSysEntDateTo)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.chkMishori)
        Me.pnlViewAria.Controls.Add(Me.imdSysEntDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.lblSAGYO_USER_NM)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmLM)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdM)
        Me.pnlViewAria.Controls.Add(Me.txtSAGYO_USER_CD)
        Me.pnlViewAria.Controls.Add(Me.lblCustCdL)
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Size = New System.Drawing.Size(1018, 626)
        '
        'FunctionKey
        '
        Me.FunctionKey.Location = New System.Drawing.Point(657, 0)
        Me.FunctionKey.Size = New System.Drawing.Size(361, 40)
        Me.FunctionKey.WidthDef = 361
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(25, 13)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel2.TabIndex = 259
        Me.LmTitleLabel2.Text = "営業所"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "営業所"
        Me.LmTitleLabel2.WidthDef = 49
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
        Me.sprDetail.HeightDef = 508
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(12, 112)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(994, 508)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 263
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.WidthDef = 994
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenFocusedNormal)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenAncestorOfFocusedNormal)
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        'cmbNrsBrCd
        '
        Me.cmbNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNrsBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNrsBrCd.DataSource = Nothing
        Me.cmbNrsBrCd.DisplayMember = Nothing
        Me.cmbNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNrsBrCd.HeightDef = 18
        Me.cmbNrsBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNrsBrCd.HissuLabelVisible = False
        Me.cmbNrsBrCd.InsertWildCard = True
        Me.cmbNrsBrCd.IsForbiddenWordsCheck = False
        Me.cmbNrsBrCd.IsHissuCheck = False
        Me.cmbNrsBrCd.ItemName = ""
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(76, 11)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(309, 18)
        Me.cmbNrsBrCd.TabIndex = 598
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 309
        '
        'lblSAGYO_USER_NM
        '
        Me.lblSAGYO_USER_NM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSAGYO_USER_NM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSAGYO_USER_NM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSAGYO_USER_NM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSAGYO_USER_NM.CountWrappedLine = False
        Me.lblSAGYO_USER_NM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSAGYO_USER_NM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSAGYO_USER_NM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSAGYO_USER_NM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSAGYO_USER_NM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSAGYO_USER_NM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSAGYO_USER_NM.HeightDef = 18
        Me.lblSAGYO_USER_NM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSAGYO_USER_NM.HissuLabelVisible = False
        Me.lblSAGYO_USER_NM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSAGYO_USER_NM.IsByteCheck = 60
        Me.lblSAGYO_USER_NM.IsCalendarCheck = False
        Me.lblSAGYO_USER_NM.IsDakutenCheck = False
        Me.lblSAGYO_USER_NM.IsEisuCheck = False
        Me.lblSAGYO_USER_NM.IsForbiddenWordsCheck = False
        Me.lblSAGYO_USER_NM.IsFullByteCheck = 0
        Me.lblSAGYO_USER_NM.IsHankakuCheck = False
        Me.lblSAGYO_USER_NM.IsHissuCheck = False
        Me.lblSAGYO_USER_NM.IsKanaCheck = False
        Me.lblSAGYO_USER_NM.IsMiddleSpace = False
        Me.lblSAGYO_USER_NM.IsNumericCheck = False
        Me.lblSAGYO_USER_NM.IsSujiCheck = False
        Me.lblSAGYO_USER_NM.IsZenkakuCheck = False
        Me.lblSAGYO_USER_NM.ItemName = ""
        Me.lblSAGYO_USER_NM.LineSpace = 0
        Me.lblSAGYO_USER_NM.Location = New System.Drawing.Point(157, 53)
        Me.lblSAGYO_USER_NM.MaxLength = 60
        Me.lblSAGYO_USER_NM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSAGYO_USER_NM.MaxLineCount = 0
        Me.lblSAGYO_USER_NM.Multiline = False
        Me.lblSAGYO_USER_NM.Name = "lblSAGYO_USER_NM"
        Me.lblSAGYO_USER_NM.ReadOnly = True
        Me.lblSAGYO_USER_NM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSAGYO_USER_NM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSAGYO_USER_NM.Size = New System.Drawing.Size(473, 18)
        Me.lblSAGYO_USER_NM.TabIndex = 603
        Me.lblSAGYO_USER_NM.TabStop = False
        Me.lblSAGYO_USER_NM.TabStopSetting = False
        Me.lblSAGYO_USER_NM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblSAGYO_USER_NM.UseSystemPasswordChar = False
        Me.lblSAGYO_USER_NM.WidthDef = 473
        Me.lblSAGYO_USER_NM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNmLM
        '
        Me.lblCustNmLM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmLM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmLM.CountWrappedLine = False
        Me.lblCustNmLM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmLM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmLM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmLM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmLM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmLM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmLM.HeightDef = 18
        Me.lblCustNmLM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmLM.HissuLabelVisible = False
        Me.lblCustNmLM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmLM.IsByteCheck = 60
        Me.lblCustNmLM.IsCalendarCheck = False
        Me.lblCustNmLM.IsDakutenCheck = False
        Me.lblCustNmLM.IsEisuCheck = False
        Me.lblCustNmLM.IsForbiddenWordsCheck = False
        Me.lblCustNmLM.IsFullByteCheck = 0
        Me.lblCustNmLM.IsHankakuCheck = False
        Me.lblCustNmLM.IsHissuCheck = False
        Me.lblCustNmLM.IsKanaCheck = False
        Me.lblCustNmLM.IsMiddleSpace = False
        Me.lblCustNmLM.IsNumericCheck = False
        Me.lblCustNmLM.IsSujiCheck = False
        Me.lblCustNmLM.IsZenkakuCheck = False
        Me.lblCustNmLM.ItemName = ""
        Me.lblCustNmLM.LineSpace = 0
        Me.lblCustNmLM.Location = New System.Drawing.Point(157, 32)
        Me.lblCustNmLM.MaxLength = 60
        Me.lblCustNmLM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmLM.MaxLineCount = 0
        Me.lblCustNmLM.Multiline = False
        Me.lblCustNmLM.Name = "lblCustNmLM"
        Me.lblCustNmLM.ReadOnly = True
        Me.lblCustNmLM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmLM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmLM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmLM.TabIndex = 602
        Me.lblCustNmLM.TabStop = False
        Me.lblCustNmLM.TabStopSetting = False
        Me.lblCustNmLM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmLM.UseSystemPasswordChar = False
        Me.lblCustNmLM.WidthDef = 473
        Me.lblCustNmLM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(35, 35)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel1.TabIndex = 601
        Me.LmTitleLabel1.Text = "荷主"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "荷主"
        Me.LmTitleLabel1.WidthDef = 35
        '
        'lblCustCdM
        '
        Me.lblCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdM.CountWrappedLine = False
        Me.lblCustCdM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdM.HeightDef = 18
        Me.lblCustCdM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdM.HissuLabelVisible = False
        Me.lblCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustCdM.IsByteCheck = 0
        Me.lblCustCdM.IsCalendarCheck = False
        Me.lblCustCdM.IsDakutenCheck = False
        Me.lblCustCdM.IsEisuCheck = False
        Me.lblCustCdM.IsForbiddenWordsCheck = False
        Me.lblCustCdM.IsFullByteCheck = 0
        Me.lblCustCdM.IsHankakuCheck = False
        Me.lblCustCdM.IsHissuCheck = False
        Me.lblCustCdM.IsKanaCheck = False
        Me.lblCustCdM.IsMiddleSpace = False
        Me.lblCustCdM.IsNumericCheck = False
        Me.lblCustCdM.IsSujiCheck = False
        Me.lblCustCdM.IsZenkakuCheck = False
        Me.lblCustCdM.ItemName = ""
        Me.lblCustCdM.LineSpace = 0
        Me.lblCustCdM.Location = New System.Drawing.Point(119, 32)
        Me.lblCustCdM.MaxLength = 0
        Me.lblCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdM.MaxLineCount = 0
        Me.lblCustCdM.Multiline = False
        Me.lblCustCdM.Name = "lblCustCdM"
        Me.lblCustCdM.ReadOnly = True
        Me.lblCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdM.Size = New System.Drawing.Size(39, 18)
        Me.lblCustCdM.TabIndex = 600
        Me.lblCustCdM.TabStop = False
        Me.lblCustCdM.TabStopSetting = False
        Me.lblCustCdM.TextValue = ""
        Me.lblCustCdM.UseSystemPasswordChar = False
        Me.lblCustCdM.WidthDef = 39
        Me.lblCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCdL
        '
        Me.lblCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCdL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCdL.CountWrappedLine = False
        Me.lblCustCdL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCdL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCdL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCdL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCdL.HeightDef = 18
        Me.lblCustCdL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCdL.HissuLabelVisible = False
        Me.lblCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustCdL.IsByteCheck = 0
        Me.lblCustCdL.IsCalendarCheck = False
        Me.lblCustCdL.IsDakutenCheck = False
        Me.lblCustCdL.IsEisuCheck = False
        Me.lblCustCdL.IsForbiddenWordsCheck = False
        Me.lblCustCdL.IsFullByteCheck = 0
        Me.lblCustCdL.IsHankakuCheck = False
        Me.lblCustCdL.IsHissuCheck = False
        Me.lblCustCdL.IsKanaCheck = False
        Me.lblCustCdL.IsMiddleSpace = False
        Me.lblCustCdL.IsNumericCheck = False
        Me.lblCustCdL.IsSujiCheck = False
        Me.lblCustCdL.IsZenkakuCheck = False
        Me.lblCustCdL.ItemName = ""
        Me.lblCustCdL.LineSpace = 0
        Me.lblCustCdL.Location = New System.Drawing.Point(76, 32)
        Me.lblCustCdL.MaxLength = 0
        Me.lblCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCdL.MaxLineCount = 0
        Me.lblCustCdL.Multiline = False
        Me.lblCustCdL.Name = "lblCustCdL"
        Me.lblCustCdL.ReadOnly = True
        Me.lblCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCdL.Size = New System.Drawing.Size(59, 18)
        Me.lblCustCdL.TabIndex = 599
        Me.lblCustCdL.TabStop = False
        Me.lblCustCdL.TabStopSetting = False
        Me.lblCustCdL.TextValue = "WWWWW"
        Me.lblCustCdL.UseSystemPasswordChar = False
        Me.lblCustCdL.WidthDef = 59
        Me.lblCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSAGYO_USER_CD
        '
        Me.txtSAGYO_USER_CD.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSAGYO_USER_CD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSAGYO_USER_CD.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAGYO_USER_CD.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSAGYO_USER_CD.CountWrappedLine = False
        Me.txtSAGYO_USER_CD.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSAGYO_USER_CD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSAGYO_USER_CD.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSAGYO_USER_CD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSAGYO_USER_CD.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSAGYO_USER_CD.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSAGYO_USER_CD.HeightDef = 18
        Me.txtSAGYO_USER_CD.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSAGYO_USER_CD.HissuLabelVisible = False
        Me.txtSAGYO_USER_CD.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSAGYO_USER_CD.IsByteCheck = 5
        Me.txtSAGYO_USER_CD.IsCalendarCheck = False
        Me.txtSAGYO_USER_CD.IsDakutenCheck = False
        Me.txtSAGYO_USER_CD.IsEisuCheck = False
        Me.txtSAGYO_USER_CD.IsForbiddenWordsCheck = False
        Me.txtSAGYO_USER_CD.IsFullByteCheck = 0
        Me.txtSAGYO_USER_CD.IsHankakuCheck = False
        Me.txtSAGYO_USER_CD.IsHissuCheck = False
        Me.txtSAGYO_USER_CD.IsKanaCheck = False
        Me.txtSAGYO_USER_CD.IsMiddleSpace = False
        Me.txtSAGYO_USER_CD.IsNumericCheck = False
        Me.txtSAGYO_USER_CD.IsSujiCheck = False
        Me.txtSAGYO_USER_CD.IsZenkakuCheck = False
        Me.txtSAGYO_USER_CD.ItemName = ""
        Me.txtSAGYO_USER_CD.LineSpace = 0
        Me.txtSAGYO_USER_CD.Location = New System.Drawing.Point(76, 53)
        Me.txtSAGYO_USER_CD.MaxLength = 5
        Me.txtSAGYO_USER_CD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSAGYO_USER_CD.MaxLineCount = 0
        Me.txtSAGYO_USER_CD.Multiline = False
        Me.txtSAGYO_USER_CD.Name = "txtSAGYO_USER_CD"
        Me.txtSAGYO_USER_CD.ReadOnly = False
        Me.txtSAGYO_USER_CD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSAGYO_USER_CD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSAGYO_USER_CD.Size = New System.Drawing.Size(59, 18)
        Me.txtSAGYO_USER_CD.TabIndex = 599
        Me.txtSAGYO_USER_CD.TabStopSetting = True
        Me.txtSAGYO_USER_CD.TextValue = "WWWWW"
        Me.txtSAGYO_USER_CD.UseSystemPasswordChar = False
        Me.txtSAGYO_USER_CD.WidthDef = 59
        Me.txtSAGYO_USER_CD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(21, 53)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel3.TabIndex = 601
        Me.LmTitleLabel3.Text = "作業者"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "作業者"
        Me.LmTitleLabel3.WidthDef = 49
        '
        'imdSysEntDate
        '
        Me.imdSysEntDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSysEntDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSysEntDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSysEntDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField7.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = "/"
        DateDayDisplayField4.ShowLeadingZero = True
        Me.imdSysEntDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField7, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField8, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSysEntDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSysEntDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSysEntDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSysEntDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSysEntDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField4, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSysEntDate.HeightDef = 18
        Me.imdSysEntDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSysEntDate.HissuLabelVisible = False
        Me.imdSysEntDate.Holiday = True
        Me.imdSysEntDate.IsAfterDateCheck = False
        Me.imdSysEntDate.IsBeforeDateCheck = False
        Me.imdSysEntDate.IsHissuCheck = False
        Me.imdSysEntDate.IsMinDateCheck = "1900/01/01"
        Me.imdSysEntDate.ItemName = ""
        Me.imdSysEntDate.Location = New System.Drawing.Point(708, 53)
        Me.imdSysEntDate.Name = "imdSysEntDate"
        Me.imdSysEntDate.Number = CType(0, Long)
        Me.imdSysEntDate.ReadOnly = False
        Me.imdSysEntDate.Size = New System.Drawing.Size(118, 18)
        Me.imdSysEntDate.TabIndex = 605
        Me.imdSysEntDate.TabStopSetting = True
        Me.imdSysEntDate.TextValue = ""
        Me.imdSysEntDate.Value = New Date(CType(0, Long))
        Me.imdSysEntDate.WidthDef = 118
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
        Me.LmTitleLabel4.Location = New System.Drawing.Point(653, 52)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel4.TabIndex = 604
        Me.LmTitleLabel4.Text = "入荷日"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "入荷日"
        Me.LmTitleLabel4.WidthDef = 49
        '
        'chkMishori
        '
        Me.chkMishori.AutoSize = True
        Me.chkMishori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMishori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMishori.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMishori.EnableStatus = True
        Me.chkMishori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMishori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMishori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMishori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMishori.HeightDef = 17
        Me.chkMishori.Location = New System.Drawing.Point(846, 76)
        Me.chkMishori.Name = "chkMishori"
        Me.chkMishori.Size = New System.Drawing.Size(124, 17)
        Me.chkMishori.TabIndex = 606
        Me.chkMishori.TabStopSetting = True
        Me.chkMishori.Text = "未処理のみ表示"
        Me.chkMishori.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMishori.TextValue = "未処理のみ表示"
        Me.chkMishori.UseVisualStyleBackColor = True
        Me.chkMishori.WidthDef = 124
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
        Me.btnRowDel.Location = New System.Drawing.Point(24, 84)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 117
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
        '
        'imdSysEntDateTo
        '
        Me.imdSysEntDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSysEntDateTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSysEntDateTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSysEntDateTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdSysEntDateTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSysEntDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSysEntDateTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSysEntDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSysEntDateTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSysEntDateTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSysEntDateTo.HeightDef = 18
        Me.imdSysEntDateTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSysEntDateTo.HissuLabelVisible = False
        Me.imdSysEntDateTo.Holiday = True
        Me.imdSysEntDateTo.IsAfterDateCheck = False
        Me.imdSysEntDateTo.IsBeforeDateCheck = False
        Me.imdSysEntDateTo.IsHissuCheck = False
        Me.imdSysEntDateTo.IsMinDateCheck = "1900/01/01"
        Me.imdSysEntDateTo.ItemName = ""
        Me.imdSysEntDateTo.Location = New System.Drawing.Point(846, 52)
        Me.imdSysEntDateTo.Name = "imdSysEntDateTo"
        Me.imdSysEntDateTo.Number = CType(0, Long)
        Me.imdSysEntDateTo.ReadOnly = False
        Me.imdSysEntDateTo.Size = New System.Drawing.Size(118, 18)
        Me.imdSysEntDateTo.TabIndex = 607
        Me.imdSysEntDateTo.TabStopSetting = True
        Me.imdSysEntDateTo.TextValue = ""
        Me.imdSysEntDateTo.Value = New Date(CType(0, Long))
        Me.imdSysEntDateTo.WidthDef = 118
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
        Me.lblFromTo.HeightDef = 13
        Me.lblFromTo.Location = New System.Drawing.Point(819, 55)
        Me.lblFromTo.Name = "lblFromTo"
        Me.lblFromTo.Size = New System.Drawing.Size(21, 13)
        Me.lblFromTo.TabIndex = 608
        Me.lblFromTo.Text = "～"
        Me.lblFromTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFromTo.TextValue = "～"
        Me.lblFromTo.WidthDef = 21
        '
        'LMB040F
        '
        Me.ClientSize = New System.Drawing.Size(1018, 706)
        Me.Name = "LMB040F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblSAGYO_USER_NM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmLM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSAGYO_USER_CD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdSysEntDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkMishori As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents imdSysEntDateTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblFromTo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
