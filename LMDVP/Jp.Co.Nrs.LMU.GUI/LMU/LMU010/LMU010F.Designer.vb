<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMU010F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMForm

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
        Me.lblKeyType = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblKeyNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbKeyType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtKeyNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.sprData = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.btnAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnDelete = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.txtChkControl = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.lblSystemID = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSystemID = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblCTRNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCTRtypekbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.NfTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.NfTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.NfTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.NfTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.NfTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.lblCTRNo)
        Me.pnlViewAria.Controls.Add(Me.lblCTRtypekbn)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.txtChkControl)
        Me.pnlViewAria.Controls.Add(Me.btnDelete)
        Me.pnlViewAria.Controls.Add(Me.btnAdd)
        Me.pnlViewAria.Controls.Add(Me.sprData)
        Me.pnlViewAria.Controls.Add(Me.txtKeyNo)
        Me.pnlViewAria.Controls.Add(Me.cmbSystemID)
        Me.pnlViewAria.Controls.Add(Me.cmbKeyType)
        Me.pnlViewAria.Controls.Add(Me.lblSystemID)
        Me.pnlViewAria.Controls.Add(Me.lblKeyNo)
        Me.pnlViewAria.Controls.Add(Me.lblKeyType)
        Me.pnlViewAria.Controls.Add(Me.NfTitleLabel2)
        Me.pnlViewAria.Size = New System.Drawing.Size(1016, 624)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1016, 40)
        Me.FunctionKey.WidthDef = 1016
        '
        'lblKeyType
        '
        Me.lblKeyType.AutoSizeDef = False
        Me.lblKeyType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKeyType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKeyType.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKeyType.EnableStatus = False
        Me.lblKeyType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeyType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeyType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKeyType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKeyType.HeightDef = 18
        Me.lblKeyType.Location = New System.Drawing.Point(621, 46)
        Me.lblKeyType.Name = "lblKeyType"
        Me.lblKeyType.Size = New System.Drawing.Size(120, 18)
        Me.lblKeyType.TabIndex = 478
        Me.lblKeyType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKeyType.TextValue = ""
        Me.lblKeyType.WidthDef = 120
        '
        'lblKeyNo
        '
        Me.lblKeyNo.AutoSizeDef = False
        Me.lblKeyNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKeyNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKeyNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblKeyNo.EnableStatus = False
        Me.lblKeyNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeyNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeyNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKeyNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKeyNo.HeightDef = 18
        Me.lblKeyNo.Location = New System.Drawing.Point(658, 46)
        Me.lblKeyNo.Name = "lblKeyNo"
        Me.lblKeyNo.Size = New System.Drawing.Size(120, 18)
        Me.lblKeyNo.TabIndex = 477
        Me.lblKeyNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblKeyNo.TextValue = ""
        Me.lblKeyNo.WidthDef = 120
        '
        'cmbKeyType
        '
        Me.cmbKeyType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKeyType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKeyType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKeyType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKeyType.DataCode = ""
        Me.cmbKeyType.DataSource = Nothing
        Me.cmbKeyType.DisplayMember = Nothing
        Me.cmbKeyType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKeyType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKeyType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKeyType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKeyType.HeightDef = 18
        Me.cmbKeyType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKeyType.HissuLabelVisible = False
        Me.cmbKeyType.InsertWildCard = True
        Me.cmbKeyType.IsForbiddenWordsCheck = False
        Me.cmbKeyType.IsHissuCheck = False
        Me.cmbKeyType.ItemName = ""
        Me.cmbKeyType.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbKeyType.Location = New System.Drawing.Point(227, 12)
        Me.cmbKeyType.Name = "cmbKeyType"
        Me.cmbKeyType.ReadOnly = True
        Me.cmbKeyType.SelectedIndex = -1
        Me.cmbKeyType.SelectedItem = Nothing
        Me.cmbKeyType.SelectedText = ""
        Me.cmbKeyType.SelectedValue = ""
        Me.cmbKeyType.Size = New System.Drawing.Size(122, 18)
        Me.cmbKeyType.TabIndex = 66
        Me.cmbKeyType.TabStop = False
        Me.cmbKeyType.TabStopSetting = False
        Me.cmbKeyType.TextValue = ""
        Me.cmbKeyType.Value1 = Nothing
        Me.cmbKeyType.Value2 = Nothing
        Me.cmbKeyType.Value3 = Nothing
        Me.cmbKeyType.ValueMember = Nothing
        Me.cmbKeyType.WidthDef = 122
        '
        'txtKeyNo
        '
        Me.txtKeyNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKeyNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKeyNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKeyNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKeyNo.CountWrappedLine = False
        Me.txtKeyNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKeyNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKeyNo.HeightDef = 18
        Me.txtKeyNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKeyNo.HissuLabelVisible = False
        Me.txtKeyNo.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtKeyNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtKeyNo.IsByteCheck = 0
        Me.txtKeyNo.IsCalendarCheck = False
        Me.txtKeyNo.IsDakutenCheck = False
        Me.txtKeyNo.IsEisuCheck = False
        Me.txtKeyNo.IsForbiddenWordsCheck = False
        Me.txtKeyNo.IsFullByteCheck = 0
        Me.txtKeyNo.IsHankakuCheck = False
        Me.txtKeyNo.IsHissuCheck = False
        Me.txtKeyNo.IsKanaCheck = False
        Me.txtKeyNo.IsMiddleSpace = False
        Me.txtKeyNo.IsNumericCheck = False
        Me.txtKeyNo.IsSujiCheck = False
        Me.txtKeyNo.IsZenkakuCheck = False
        Me.txtKeyNo.ItemName = ""
        Me.txtKeyNo.LineSpace = 0
        Me.txtKeyNo.Location = New System.Drawing.Point(422, 12)
        Me.txtKeyNo.MaxLength = 0
        Me.txtKeyNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKeyNo.MaxLineCount = 0
        Me.txtKeyNo.Multiline = False
        Me.txtKeyNo.Name = "txtKeyNo"
        Me.txtKeyNo.ReadOnly = True
        Me.txtKeyNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKeyNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKeyNo.Size = New System.Drawing.Size(106, 18)
        Me.txtKeyNo.TabIndex = 67
        Me.txtKeyNo.TabStop = False
        Me.txtKeyNo.TabStopSetting = False
        Me.txtKeyNo.TextValue = ""
        Me.txtKeyNo.UseSystemPasswordChar = False
        Me.txtKeyNo.WidthDef = 106
        Me.txtKeyNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'sprData
        '
        Me.sprData.AccessibleDescription = ""
        Me.sprData.AllowUserZoom = False
        Me.sprData.AutoImeMode = False
        Me.sprData.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprData.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprData.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprData.CellClickEventArgs = Nothing
        Me.sprData.CheckToCheckBox = True
        Me.sprData.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprData.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprData.EditModeReplace = True
        Me.sprData.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprData.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprData.ForeColorDef = System.Drawing.Color.Empty
        Me.sprData.HeightDef = 534
        Me.sprData.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sprData.KeyboardCheckBoxOn = False
        Me.sprData.Location = New System.Drawing.Point(10, 74)
        Me.sprData.Name = "sprData"
        Me.sprData.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprData.Size = New System.Drawing.Size(994, 534)
        Me.sprData.SortColumn = True
        Me.sprData.SpanColumnLock = True
        Me.sprData.SpreadDoubleClicked = False
        Me.sprData.TabIndex = 70
        Me.sprData.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprData.TextValue = Nothing
        Me.sprData.UseGrouping = False
        Me.sprData.WidthDef = 994
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAdd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnAdd.EnableStatus = True
        Me.btnAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAdd.HeightDef = 22
        Me.btnAdd.Location = New System.Drawing.Point(23, 46)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(80, 22)
        Me.btnAdd.TabIndex = 71
        Me.btnAdd.TabStopSetting = True
        Me.btnAdd.Text = "行追加"
        Me.btnAdd.TextValue = "行追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.WidthDef = 80
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDelete.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDelete.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnDelete.EnableStatus = True
        Me.btnDelete.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelete.HeightDef = 22
        Me.btnDelete.Location = New System.Drawing.Point(109, 46)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 22)
        Me.btnDelete.TabIndex = 72
        Me.btnDelete.TabStopSetting = True
        Me.btnDelete.Text = "行削除"
        Me.btnDelete.TextValue = "行削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        Me.btnDelete.WidthDef = 80
        '
        'txtChkControl
        '
        Me.txtChkControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtChkControl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtChkControl.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChkControl.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtChkControl.CountWrappedLine = False
        Me.txtChkControl.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtChkControl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtChkControl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtChkControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtChkControl.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtChkControl.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtChkControl.HeightDef = 18
        Me.txtChkControl.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtChkControl.HissuLabelVisible = False
        Me.txtChkControl.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtChkControl.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtChkControl.IsByteCheck = 0
        Me.txtChkControl.IsCalendarCheck = False
        Me.txtChkControl.IsDakutenCheck = False
        Me.txtChkControl.IsEisuCheck = False
        Me.txtChkControl.IsForbiddenWordsCheck = False
        Me.txtChkControl.IsFullByteCheck = 0
        Me.txtChkControl.IsHankakuCheck = False
        Me.txtChkControl.IsHissuCheck = False
        Me.txtChkControl.IsKanaCheck = False
        Me.txtChkControl.IsMiddleSpace = False
        Me.txtChkControl.IsNumericCheck = False
        Me.txtChkControl.IsSujiCheck = False
        Me.txtChkControl.IsZenkakuCheck = False
        Me.txtChkControl.ItemName = ""
        Me.txtChkControl.LineSpace = 0
        Me.txtChkControl.Location = New System.Drawing.Point(843, 50)
        Me.txtChkControl.MaxLength = 0
        Me.txtChkControl.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtChkControl.MaxLineCount = 0
        Me.txtChkControl.Multiline = False
        Me.txtChkControl.Name = "txtChkControl"
        Me.txtChkControl.ReadOnly = True
        Me.txtChkControl.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtChkControl.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtChkControl.Size = New System.Drawing.Size(170, 18)
        Me.txtChkControl.TabIndex = 74
        Me.txtChkControl.TabStop = False
        Me.txtChkControl.TabStopSetting = False
        Me.txtChkControl.TextValue = ""
        Me.txtChkControl.UseSystemPasswordChar = False
        Me.txtChkControl.Visible = False
        Me.txtChkControl.WidthDef = 170
        Me.txtChkControl.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(889, 12)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(115, 18)
        Me.lblSituation.TabIndex = 475
        Me.lblSituation.TabStop = False
        '
        'lblSystemID
        '
        Me.lblSystemID.AutoSizeDef = False
        Me.lblSystemID.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSystemID.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSystemID.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSystemID.EnableStatus = False
        Me.lblSystemID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSystemID.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSystemID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSystemID.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSystemID.HeightDef = 18
        Me.lblSystemID.Location = New System.Drawing.Point(495, 46)
        Me.lblSystemID.Name = "lblSystemID"
        Me.lblSystemID.Size = New System.Drawing.Size(120, 18)
        Me.lblSystemID.TabIndex = 476
        Me.lblSystemID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSystemID.TextValue = ""
        Me.lblSystemID.WidthDef = 120
        '
        'cmbSystemID
        '
        Me.cmbSystemID.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSystemID.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSystemID.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSystemID.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSystemID.DataCode = ""
        Me.cmbSystemID.DataSource = Nothing
        Me.cmbSystemID.DisplayMember = Nothing
        Me.cmbSystemID.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSystemID.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSystemID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSystemID.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSystemID.HeightDef = 18
        Me.cmbSystemID.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSystemID.HissuLabelVisible = False
        Me.cmbSystemID.InsertWildCard = True
        Me.cmbSystemID.IsForbiddenWordsCheck = False
        Me.cmbSystemID.IsHissuCheck = False
        Me.cmbSystemID.ItemName = ""
        Me.cmbSystemID.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM3
        Me.cmbSystemID.Location = New System.Drawing.Point(87, 12)
        Me.cmbSystemID.Name = "cmbSystemID"
        Me.cmbSystemID.ReadOnly = True
        Me.cmbSystemID.SelectedIndex = -1
        Me.cmbSystemID.SelectedItem = Nothing
        Me.cmbSystemID.SelectedText = ""
        Me.cmbSystemID.SelectedValue = ""
        Me.cmbSystemID.Size = New System.Drawing.Size(65, 18)
        Me.cmbSystemID.TabIndex = 66
        Me.cmbSystemID.TabStop = False
        Me.cmbSystemID.TabStopSetting = False
        Me.cmbSystemID.TextValue = ""
        Me.cmbSystemID.Value1 = Nothing
        Me.cmbSystemID.Value2 = Nothing
        Me.cmbSystemID.Value3 = Nothing
        Me.cmbSystemID.ValueMember = Nothing
        Me.cmbSystemID.WidthDef = 65
        '
        'lblCTRNo
        '
        Me.lblCTRNo.AutoSizeDef = False
        Me.lblCTRNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCTRNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCTRNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCTRNo.EnableStatus = False
        Me.lblCTRNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCTRNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCTRNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCTRNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCTRNo.HeightDef = 18
        Me.lblCTRNo.Location = New System.Drawing.Point(532, 46)
        Me.lblCTRNo.Name = "lblCTRNo"
        Me.lblCTRNo.Size = New System.Drawing.Size(120, 18)
        Me.lblCTRNo.TabIndex = 0
        Me.lblCTRNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCTRNo.TextValue = ""
        Me.lblCTRNo.WidthDef = 120
        '
        'lblCTRtypekbn
        '
        Me.lblCTRtypekbn.AutoSizeDef = False
        Me.lblCTRtypekbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCTRtypekbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCTRtypekbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCTRtypekbn.EnableStatus = False
        Me.lblCTRtypekbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCTRtypekbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCTRtypekbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCTRtypekbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCTRtypekbn.HeightDef = 18
        Me.lblCTRtypekbn.Location = New System.Drawing.Point(532, 46)
        Me.lblCTRtypekbn.Name = "lblCTRtypekbn"
        Me.lblCTRtypekbn.Size = New System.Drawing.Size(120, 18)
        Me.lblCTRtypekbn.TabIndex = 474
        Me.lblCTRtypekbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCTRtypekbn.TextValue = ""
        Me.lblCTRtypekbn.WidthDef = 120
        '
        'NfTitleLabel6
        '
        Me.NfTitleLabel6.AutoSizeDef = False
        Me.NfTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.NfTitleLabel6.EnableStatus = False
        Me.NfTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel6.HeightDef = 18
        Me.NfTitleLabel6.Location = New System.Drawing.Point(2, 12)
        Me.NfTitleLabel6.Name = "NfTitleLabel6"
        Me.NfTitleLabel6.Size = New System.Drawing.Size(81, 18)
        Me.NfTitleLabel6.TabIndex = 303
        Me.NfTitleLabel6.Text = "システムID"
        Me.NfTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NfTitleLabel6.TextValue = "システムID"
        Me.NfTitleLabel6.WidthDef = 81
        '
        'NfTitleLabel1
        '
        Me.NfTitleLabel1.AutoSizeDef = False
        Me.NfTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.NfTitleLabel1.EnableStatus = False
        Me.NfTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel1.HeightDef = 18
        Me.NfTitleLabel1.Location = New System.Drawing.Point(143, 12)
        Me.NfTitleLabel1.Name = "NfTitleLabel1"
        Me.NfTitleLabel1.Size = New System.Drawing.Size(81, 18)
        Me.NfTitleLabel1.TabIndex = 304
        Me.NfTitleLabel1.Text = "管理タイプ"
        Me.NfTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NfTitleLabel1.TextValue = "管理タイプ"
        Me.NfTitleLabel1.WidthDef = 81
        '
        'NfTitleLabel2
        '
        Me.NfTitleLabel2.AutoSizeDef = False
        Me.NfTitleLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.NfTitleLabel2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.NfTitleLabel2.EnableStatus = False
        Me.NfTitleLabel2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NfTitleLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NfTitleLabel2.HeightDef = 18
        Me.NfTitleLabel2.Location = New System.Drawing.Point(337, 12)
        Me.NfTitleLabel2.Name = "NfTitleLabel2"
        Me.NfTitleLabel2.Size = New System.Drawing.Size(81, 18)
        Me.NfTitleLabel2.TabIndex = 305
        Me.NfTitleLabel2.Text = "受注番号"
        Me.NfTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NfTitleLabel2.TextValue = "受注番号"
        Me.NfTitleLabel2.WidthDef = 81
        '
        'LMU010F
        '
        Me.ClientSize = New System.Drawing.Size(1016, 704)
        Me.Name = "LMU010F"
        Me.Text = "【LMU010】 文書管理画面"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblKeyType As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKeyNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbKeyType As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblKeyNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnDelete As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents sprData As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents txtChkControl As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents cmbSystemID As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSystemID As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCTRNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCTRtypekbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents NfTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents NfTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents NfTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
