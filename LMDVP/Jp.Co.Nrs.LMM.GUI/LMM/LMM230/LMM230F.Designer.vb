<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM230F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM230F))
        Me.lblTitleArea = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.lblTitleInfo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtAreaNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtAreaCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtAreaInfo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.sprDetail2 = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.LmTitleLabel19 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.cmbBin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel12 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblShi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblKen = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtJis = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleJis = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlEdit = New System.Windows.Forms.Panel
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblShi)
        Me.pnlViewAria.Controls.Add(Me.lblKen)
        Me.pnlViewAria.Controls.Add(Me.txtJis)
        Me.pnlViewAria.Controls.Add(Me.lblTitleJis)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.pnlEdit)
        '
        'lblTitleArea
        '
        Me.lblTitleArea.AutoSize = True
        Me.lblTitleArea.AutoSizeDef = True
        Me.lblTitleArea.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArea.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArea.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleArea.EnableStatus = False
        Me.lblTitleArea.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArea.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArea.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArea.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArea.HeightDef = 13
        Me.lblTitleArea.Location = New System.Drawing.Point(40, 59)
        Me.lblTitleArea.Name = "lblTitleArea"
        Me.lblTitleArea.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleArea.TabIndex = 78
        Me.lblTitleArea.Text = "エリア"
        Me.lblTitleArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleArea.TextValue = "エリア"
        Me.lblTitleArea.WidthDef = 49
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
        Me.sprDetail.HeightDef = 328
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(16, 48)
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
        Me.sprDetail.Size = New System.Drawing.Size(1237, 328)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 110
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1237
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1105, 35)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 117
        Me.lblSituation.TabStop = False
        '
        'lblTitleInfo
        '
        Me.lblTitleInfo.AutoSize = True
        Me.lblTitleInfo.AutoSizeDef = True
        Me.lblTitleInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInfo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInfo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInfo.EnableStatus = False
        Me.lblTitleInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInfo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInfo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInfo.HeightDef = 13
        Me.lblTitleInfo.Location = New System.Drawing.Point(54, 101)
        Me.lblTitleInfo.Name = "lblTitleInfo"
        Me.lblTitleInfo.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleInfo.TabIndex = 269
        Me.lblTitleInfo.Text = "備考"
        Me.lblTitleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInfo.TextValue = "備考"
        Me.lblTitleInfo.WidthDef = 35
        '
        'txtAreaNm
        '
        Me.txtAreaNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAreaNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAreaNm.CountWrappedLine = False
        Me.txtAreaNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAreaNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAreaNm.HeightDef = 18
        Me.txtAreaNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAreaNm.HissuLabelVisible = True
        Me.txtAreaNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAreaNm.IsByteCheck = 20
        Me.txtAreaNm.IsCalendarCheck = False
        Me.txtAreaNm.IsDakutenCheck = False
        Me.txtAreaNm.IsEisuCheck = False
        Me.txtAreaNm.IsForbiddenWordsCheck = False
        Me.txtAreaNm.IsFullByteCheck = 0
        Me.txtAreaNm.IsHankakuCheck = False
        Me.txtAreaNm.IsHissuCheck = True
        Me.txtAreaNm.IsKanaCheck = False
        Me.txtAreaNm.IsMiddleSpace = False
        Me.txtAreaNm.IsNumericCheck = False
        Me.txtAreaNm.IsSujiCheck = False
        Me.txtAreaNm.IsZenkakuCheck = False
        Me.txtAreaNm.ItemName = ""
        Me.txtAreaNm.LineSpace = 0
        Me.txtAreaNm.Location = New System.Drawing.Point(152, 56)
        Me.txtAreaNm.MaxLength = 20
        Me.txtAreaNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAreaNm.MaxLineCount = 0
        Me.txtAreaNm.Multiline = False
        Me.txtAreaNm.Name = "txtAreaNm"
        Me.txtAreaNm.ReadOnly = False
        Me.txtAreaNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAreaNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAreaNm.Size = New System.Drawing.Size(179, 18)
        Me.txtAreaNm.TabIndex = 398
        Me.txtAreaNm.TabStopSetting = True
        Me.txtAreaNm.TextValue = "12345678901234567890"
        Me.txtAreaNm.UseSystemPasswordChar = False
        Me.txtAreaNm.WidthDef = 179
        Me.txtAreaNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleNrsBrCd
        '
        Me.lblTitleNrsBrCd.AutoSize = True
        Me.lblTitleNrsBrCd.AutoSizeDef = True
        Me.lblTitleNrsBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNrsBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNrsBrCd.EnableStatus = False
        Me.lblTitleNrsBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNrsBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNrsBrCd.HeightDef = 13
        Me.lblTitleNrsBrCd.Location = New System.Drawing.Point(40, 37)
        Me.lblTitleNrsBrCd.Name = "lblTitleNrsBrCd"
        Me.lblTitleNrsBrCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNrsBrCd.TabIndex = 402
        Me.lblTitleNrsBrCd.Text = "営業所"
        Me.lblTitleNrsBrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNrsBrCd.TextValue = "営業所"
        Me.lblTitleNrsBrCd.WidthDef = 49
        '
        'txtAreaCd
        '
        Me.txtAreaCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAreaCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAreaCd.CountWrappedLine = False
        Me.txtAreaCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAreaCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAreaCd.HeightDef = 18
        Me.txtAreaCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAreaCd.HissuLabelVisible = False
        Me.txtAreaCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtAreaCd.IsByteCheck = 7
        Me.txtAreaCd.IsCalendarCheck = False
        Me.txtAreaCd.IsDakutenCheck = False
        Me.txtAreaCd.IsEisuCheck = False
        Me.txtAreaCd.IsForbiddenWordsCheck = False
        Me.txtAreaCd.IsFullByteCheck = 0
        Me.txtAreaCd.IsHankakuCheck = False
        Me.txtAreaCd.IsHissuCheck = False
        Me.txtAreaCd.IsKanaCheck = False
        Me.txtAreaCd.IsMiddleSpace = False
        Me.txtAreaCd.IsNumericCheck = False
        Me.txtAreaCd.IsSujiCheck = False
        Me.txtAreaCd.IsZenkakuCheck = False
        Me.txtAreaCd.ItemName = ""
        Me.txtAreaCd.LineSpace = 0
        Me.txtAreaCd.Location = New System.Drawing.Point(91, 56)
        Me.txtAreaCd.MaxLength = 7
        Me.txtAreaCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAreaCd.MaxLineCount = 0
        Me.txtAreaCd.Multiline = False
        Me.txtAreaCd.Name = "txtAreaCd"
        Me.txtAreaCd.ReadOnly = False
        Me.txtAreaCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAreaCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAreaCd.Size = New System.Drawing.Size(77, 18)
        Me.txtAreaCd.TabIndex = 404
        Me.txtAreaCd.TabStopSetting = True
        Me.txtAreaCd.TextValue = "1234567"
        Me.txtAreaCd.UseSystemPasswordChar = False
        Me.txtAreaCd.WidthDef = 77
        Me.txtAreaCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtAreaInfo
        '
        Me.txtAreaInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaInfo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAreaInfo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAreaInfo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtAreaInfo.CountWrappedLine = False
        Me.txtAreaInfo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtAreaInfo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaInfo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtAreaInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaInfo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAreaInfo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtAreaInfo.HeightDef = 18
        Me.txtAreaInfo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtAreaInfo.HissuLabelVisible = False
        Me.txtAreaInfo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtAreaInfo.IsByteCheck = 100
        Me.txtAreaInfo.IsCalendarCheck = False
        Me.txtAreaInfo.IsDakutenCheck = False
        Me.txtAreaInfo.IsEisuCheck = False
        Me.txtAreaInfo.IsForbiddenWordsCheck = False
        Me.txtAreaInfo.IsFullByteCheck = 0
        Me.txtAreaInfo.IsHankakuCheck = False
        Me.txtAreaInfo.IsHissuCheck = False
        Me.txtAreaInfo.IsKanaCheck = False
        Me.txtAreaInfo.IsMiddleSpace = False
        Me.txtAreaInfo.IsNumericCheck = False
        Me.txtAreaInfo.IsSujiCheck = False
        Me.txtAreaInfo.IsZenkakuCheck = False
        Me.txtAreaInfo.ItemName = ""
        Me.txtAreaInfo.LineSpace = 0
        Me.txtAreaInfo.Location = New System.Drawing.Point(91, 98)
        Me.txtAreaInfo.MaxLength = 100
        Me.txtAreaInfo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtAreaInfo.MaxLineCount = 0
        Me.txtAreaInfo.Multiline = False
        Me.txtAreaInfo.Name = "txtAreaInfo"
        Me.txtAreaInfo.ReadOnly = False
        Me.txtAreaInfo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtAreaInfo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAreaInfo.Size = New System.Drawing.Size(730, 18)
        Me.txtAreaInfo.TabIndex = 405
        Me.txtAreaInfo.TabStopSetting = True
        Me.txtAreaInfo.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
            "---10---XX---10---X"
        Me.txtAreaInfo.UseSystemPasswordChar = False
        Me.txtAreaInfo.WidthDef = 730
        Me.txtAreaInfo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.btnRowDel.Location = New System.Drawing.Point(139, 135)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 410
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
        Me.btnRowAdd.Location = New System.Drawing.Point(62, 135)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 411
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 70
        '
        'sprDetail2
        '
        Me.sprDetail2.AccessibleDescription = ""
        Me.sprDetail2.AllowUserZoom = False
        Me.sprDetail2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetail2.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetail2.CellClickEventArgs = Nothing
        Me.sprDetail2.CheckToCheckBox = True
        Me.sprDetail2.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetail2.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail2.EditModeReplace = True
        Me.sprDetail2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetail2.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetail2.HeightDef = 324
        Me.sprDetail2.KeyboardCheckBoxOn = False
        Me.sprDetail2.Location = New System.Drawing.Point(29, 168)
        Me.sprDetail2.Name = "sprDetail2"
        Me.sprDetail2.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail2.Size = New System.Drawing.Size(657, 324)
        Me.sprDetail2.SortColumn = True
        Me.sprDetail2.SpanColumnLock = True
        Me.sprDetail2.SpreadDoubleClicked = False
        Me.sprDetail2.TabIndex = 409
        Me.sprDetail2.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail2.TextValue = Nothing
        Me.sprDetail2.WidthDef = 657
        '
        'LmTitleLabel19
        '
        Me.LmTitleLabel19.AutoSize = True
        Me.LmTitleLabel19.AutoSizeDef = True
        Me.LmTitleLabel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel19.EnableStatus = False
        Me.LmTitleLabel19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.HeightDef = 13
        Me.LmTitleLabel19.Location = New System.Drawing.Point(39, 80)
        Me.LmTitleLabel19.Name = "LmTitleLabel19"
        Me.LmTitleLabel19.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel19.TabIndex = 425
        Me.LmTitleLabel19.Text = "便区分"
        Me.LmTitleLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel19.TextValue = "便区分"
        Me.LmTitleLabel19.WidthDef = 49
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
        Me.cmbNrsBrCd.HissuLabelVisible = True
        Me.cmbNrsBrCd.InsertWildCard = True
        Me.cmbNrsBrCd.IsForbiddenWordsCheck = False
        Me.cmbNrsBrCd.IsHissuCheck = True
        Me.cmbNrsBrCd.ItemName = ""
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(91, 35)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 607
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'cmbBin
        '
        Me.cmbBin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBin.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBin.DataCode = "U001"
        Me.cmbBin.DataSource = Nothing
        Me.cmbBin.DisplayMember = Nothing
        Me.cmbBin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBin.HeightDef = 18
        Me.cmbBin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBin.HissuLabelVisible = True
        Me.cmbBin.InsertWildCard = True
        Me.cmbBin.IsForbiddenWordsCheck = False
        Me.cmbBin.IsHissuCheck = True
        Me.cmbBin.ItemName = ""
        Me.cmbBin.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBin.Location = New System.Drawing.Point(91, 77)
        Me.cmbBin.Name = "cmbBin"
        Me.cmbBin.ReadOnly = False
        Me.cmbBin.SelectedIndex = -1
        Me.cmbBin.SelectedItem = Nothing
        Me.cmbBin.SelectedText = ""
        Me.cmbBin.SelectedValue = ""
        Me.cmbBin.Size = New System.Drawing.Size(119, 18)
        Me.cmbBin.TabIndex = 628
        Me.cmbBin.TabStopSetting = True
        Me.cmbBin.TextValue = ""
        Me.cmbBin.Value1 = Nothing
        Me.cmbBin.Value2 = Nothing
        Me.cmbBin.Value3 = Nothing
        Me.cmbBin.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbBin.ValueMember = Nothing
        Me.cmbBin.WidthDef = 119
        '
        'lblSysDelFlg
        '
        Me.lblSysDelFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysDelFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysDelFlg.CountWrappedLine = False
        Me.lblSysDelFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysDelFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDelFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysDelFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysDelFlg.HeightDef = 18
        Me.lblSysDelFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysDelFlg.HissuLabelVisible = False
        Me.lblSysDelFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysDelFlg.IsByteCheck = 0
        Me.lblSysDelFlg.IsCalendarCheck = False
        Me.lblSysDelFlg.IsDakutenCheck = False
        Me.lblSysDelFlg.IsEisuCheck = False
        Me.lblSysDelFlg.IsForbiddenWordsCheck = False
        Me.lblSysDelFlg.IsFullByteCheck = 0
        Me.lblSysDelFlg.IsHankakuCheck = False
        Me.lblSysDelFlg.IsHissuCheck = False
        Me.lblSysDelFlg.IsKanaCheck = False
        Me.lblSysDelFlg.IsMiddleSpace = False
        Me.lblSysDelFlg.IsNumericCheck = False
        Me.lblSysDelFlg.IsSujiCheck = False
        Me.lblSysDelFlg.IsZenkakuCheck = False
        Me.lblSysDelFlg.ItemName = ""
        Me.lblSysDelFlg.LineSpace = 0
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1101, 180)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 638
        Me.lblSysDelFlg.TabStop = False
        Me.lblSysDelFlg.TabStopSetting = False
        Me.lblSysDelFlg.TextValue = ""
        Me.lblSysDelFlg.UseSystemPasswordChar = False
        Me.lblSysDelFlg.Visible = False
        Me.lblSysDelFlg.WidthDef = 157
        Me.lblSysDelFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdTime
        '
        Me.lblUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdTime.CountWrappedLine = False
        Me.lblUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdTime.HeightDef = 18
        Me.lblUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdTime.HissuLabelVisible = False
        Me.lblUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdTime.IsByteCheck = 0
        Me.lblUpdTime.IsCalendarCheck = False
        Me.lblUpdTime.IsDakutenCheck = False
        Me.lblUpdTime.IsEisuCheck = False
        Me.lblUpdTime.IsForbiddenWordsCheck = False
        Me.lblUpdTime.IsFullByteCheck = 0
        Me.lblUpdTime.IsHankakuCheck = False
        Me.lblUpdTime.IsHissuCheck = False
        Me.lblUpdTime.IsKanaCheck = False
        Me.lblUpdTime.IsMiddleSpace = False
        Me.lblUpdTime.IsNumericCheck = False
        Me.lblUpdTime.IsSujiCheck = False
        Me.lblUpdTime.IsZenkakuCheck = False
        Me.lblUpdTime.ItemName = ""
        Me.lblUpdTime.LineSpace = 0
        Me.lblUpdTime.Location = New System.Drawing.Point(1101, 159)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 637
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdDate
        '
        Me.lblUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdDate.CountWrappedLine = False
        Me.lblUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdDate.HeightDef = 18
        Me.lblUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdDate.HissuLabelVisible = False
        Me.lblUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdDate.IsByteCheck = 10
        Me.lblUpdDate.IsCalendarCheck = False
        Me.lblUpdDate.IsDakutenCheck = False
        Me.lblUpdDate.IsEisuCheck = False
        Me.lblUpdDate.IsForbiddenWordsCheck = False
        Me.lblUpdDate.IsFullByteCheck = 0
        Me.lblUpdDate.IsHankakuCheck = False
        Me.lblUpdDate.IsHissuCheck = False
        Me.lblUpdDate.IsKanaCheck = False
        Me.lblUpdDate.IsMiddleSpace = False
        Me.lblUpdDate.IsNumericCheck = False
        Me.lblUpdDate.IsSujiCheck = False
        Me.lblUpdDate.IsZenkakuCheck = False
        Me.lblUpdDate.ItemName = ""
        Me.lblUpdDate.LineSpace = 0
        Me.lblUpdDate.Location = New System.Drawing.Point(1101, 138)
        Me.lblUpdDate.MaxLength = 10
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 636
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = "2011/01/10"
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCrtUser
        '
        Me.lblCrtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtUser.CountWrappedLine = False
        Me.lblCrtUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtUser.HeightDef = 18
        Me.lblCrtUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtUser.HissuLabelVisible = False
        Me.lblCrtUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtUser.IsByteCheck = 20
        Me.lblCrtUser.IsCalendarCheck = False
        Me.lblCrtUser.IsDakutenCheck = False
        Me.lblCrtUser.IsEisuCheck = False
        Me.lblCrtUser.IsForbiddenWordsCheck = False
        Me.lblCrtUser.IsFullByteCheck = 0
        Me.lblCrtUser.IsHankakuCheck = False
        Me.lblCrtUser.IsHissuCheck = False
        Me.lblCrtUser.IsKanaCheck = False
        Me.lblCrtUser.IsMiddleSpace = False
        Me.lblCrtUser.IsNumericCheck = False
        Me.lblCrtUser.IsSujiCheck = False
        Me.lblCrtUser.IsZenkakuCheck = False
        Me.lblCrtUser.ItemName = ""
        Me.lblCrtUser.LineSpace = 0
        Me.lblCrtUser.Location = New System.Drawing.Point(1101, 75)
        Me.lblCrtUser.MaxLength = 20
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 634
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = "Admin"
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdUser
        '
        Me.lblUpdUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdUser.CountWrappedLine = False
        Me.lblUpdUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdUser.HeightDef = 18
        Me.lblUpdUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdUser.HissuLabelVisible = False
        Me.lblUpdUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdUser.IsByteCheck = 20
        Me.lblUpdUser.IsCalendarCheck = False
        Me.lblUpdUser.IsDakutenCheck = False
        Me.lblUpdUser.IsEisuCheck = False
        Me.lblUpdUser.IsForbiddenWordsCheck = False
        Me.lblUpdUser.IsFullByteCheck = 0
        Me.lblUpdUser.IsHankakuCheck = False
        Me.lblUpdUser.IsHissuCheck = False
        Me.lblUpdUser.IsKanaCheck = False
        Me.lblUpdUser.IsMiddleSpace = False
        Me.lblUpdUser.IsNumericCheck = False
        Me.lblUpdUser.IsSujiCheck = False
        Me.lblUpdUser.IsZenkakuCheck = False
        Me.lblUpdUser.ItemName = ""
        Me.lblUpdUser.LineSpace = 0
        Me.lblUpdUser.Location = New System.Drawing.Point(1102, 117)
        Me.lblUpdUser.MaxLength = 20
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(156, 18)
        Me.lblUpdUser.TabIndex = 635
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = "Admin"
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 156
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSize = True
        Me.LmTitleLabel9.AutoSizeDef = True
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 13
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1050, 141)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel9.TabIndex = 632
        Me.LmTitleLabel9.Text = "更新日"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新日"
        Me.LmTitleLabel9.WidthDef = 49
        '
        'lblCrtDate
        '
        Me.lblCrtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrtDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCrtDate.CountWrappedLine = False
        Me.lblCrtDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCrtDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCrtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCrtDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCrtDate.HeightDef = 18
        Me.lblCrtDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCrtDate.HissuLabelVisible = False
        Me.lblCrtDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCrtDate.IsByteCheck = 10
        Me.lblCrtDate.IsCalendarCheck = False
        Me.lblCrtDate.IsDakutenCheck = False
        Me.lblCrtDate.IsEisuCheck = False
        Me.lblCrtDate.IsForbiddenWordsCheck = False
        Me.lblCrtDate.IsFullByteCheck = 0
        Me.lblCrtDate.IsHankakuCheck = False
        Me.lblCrtDate.IsHissuCheck = False
        Me.lblCrtDate.IsKanaCheck = False
        Me.lblCrtDate.IsMiddleSpace = False
        Me.lblCrtDate.IsNumericCheck = False
        Me.lblCrtDate.IsSujiCheck = False
        Me.lblCrtDate.IsZenkakuCheck = False
        Me.lblCrtDate.ItemName = ""
        Me.lblCrtDate.LineSpace = 0
        Me.lblCrtDate.Location = New System.Drawing.Point(1101, 96)
        Me.lblCrtDate.MaxLength = 10
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 633
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = "2001/01/01"
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSize = True
        Me.LmTitleLabel10.AutoSizeDef = True
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 13
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1051, 120)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel10.TabIndex = 631
        Me.LmTitleLabel10.Text = "更新者"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新者"
        Me.LmTitleLabel10.WidthDef = 49
        '
        'LmTitleLabel12
        '
        Me.LmTitleLabel12.AutoSize = True
        Me.LmTitleLabel12.AutoSizeDef = True
        Me.LmTitleLabel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel12.EnableStatus = False
        Me.LmTitleLabel12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.HeightDef = 13
        Me.LmTitleLabel12.Location = New System.Drawing.Point(1050, 100)
        Me.LmTitleLabel12.Name = "LmTitleLabel12"
        Me.LmTitleLabel12.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel12.TabIndex = 630
        Me.LmTitleLabel12.Text = "作成日"
        Me.LmTitleLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel12.TextValue = "作成日"
        Me.LmTitleLabel12.WidthDef = 49
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(1050, 78)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 629
        Me.LmTitleLabel1.Text = "作成者"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "作成者"
        Me.LmTitleLabel1.WidthDef = 49
        '
        'lblShi
        '
        Me.lblShi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblShi.CountWrappedLine = False
        Me.lblShi.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblShi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblShi.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblShi.HeightDef = 18
        Me.lblShi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblShi.HissuLabelVisible = False
        Me.lblShi.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblShi.IsByteCheck = 20
        Me.lblShi.IsCalendarCheck = False
        Me.lblShi.IsDakutenCheck = False
        Me.lblShi.IsEisuCheck = False
        Me.lblShi.IsForbiddenWordsCheck = False
        Me.lblShi.IsFullByteCheck = 0
        Me.lblShi.IsHankakuCheck = False
        Me.lblShi.IsHissuCheck = False
        Me.lblShi.IsKanaCheck = False
        Me.lblShi.IsMiddleSpace = False
        Me.lblShi.IsNumericCheck = False
        Me.lblShi.IsSujiCheck = False
        Me.lblShi.IsZenkakuCheck = False
        Me.lblShi.ItemName = ""
        Me.lblShi.LineSpace = 0
        Me.lblShi.Location = New System.Drawing.Point(223, 15)
        Me.lblShi.MaxLength = 20
        Me.lblShi.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblShi.MaxLineCount = 0
        Me.lblShi.Multiline = False
        Me.lblShi.Name = "lblShi"
        Me.lblShi.ReadOnly = True
        Me.lblShi.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblShi.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblShi.Size = New System.Drawing.Size(159, 18)
        Me.lblShi.TabIndex = 642
        Me.lblShi.TabStop = False
        Me.lblShi.TabStopSetting = False
        Me.lblShi.Tag = ""
        Me.lblShi.TextValue = ""
        Me.lblShi.UseSystemPasswordChar = False
        Me.lblShi.WidthDef = 159
        Me.lblShi.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblKen
        '
        Me.lblKen.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKen.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKen.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKen.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKen.CountWrappedLine = False
        Me.lblKen.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblKen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKen.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKen.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblKen.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblKen.HeightDef = 18
        Me.lblKen.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblKen.HissuLabelVisible = False
        Me.lblKen.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblKen.IsByteCheck = 10
        Me.lblKen.IsCalendarCheck = False
        Me.lblKen.IsDakutenCheck = False
        Me.lblKen.IsEisuCheck = False
        Me.lblKen.IsForbiddenWordsCheck = False
        Me.lblKen.IsFullByteCheck = 0
        Me.lblKen.IsHankakuCheck = False
        Me.lblKen.IsHissuCheck = False
        Me.lblKen.IsKanaCheck = False
        Me.lblKen.IsMiddleSpace = False
        Me.lblKen.IsNumericCheck = False
        Me.lblKen.IsSujiCheck = False
        Me.lblKen.IsZenkakuCheck = False
        Me.lblKen.ItemName = ""
        Me.lblKen.LineSpace = 0
        Me.lblKen.Location = New System.Drawing.Point(146, 15)
        Me.lblKen.MaxLength = 10
        Me.lblKen.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblKen.MaxLineCount = 0
        Me.lblKen.Multiline = False
        Me.lblKen.Name = "lblKen"
        Me.lblKen.ReadOnly = True
        Me.lblKen.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblKen.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblKen.Size = New System.Drawing.Size(93, 18)
        Me.lblKen.TabIndex = 641
        Me.lblKen.TabStop = False
        Me.lblKen.TabStopSetting = False
        Me.lblKen.Tag = ""
        Me.lblKen.TextValue = ""
        Me.lblKen.UseSystemPasswordChar = False
        Me.lblKen.WidthDef = 93
        Me.lblKen.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtJis
        '
        Me.txtJis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJis.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJis.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJis.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtJis.CountWrappedLine = False
        Me.txtJis.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtJis.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJis.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJis.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJis.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtJis.HeightDef = 18
        Me.txtJis.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtJis.HissuLabelVisible = False
        Me.txtJis.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtJis.IsByteCheck = 7
        Me.txtJis.IsCalendarCheck = False
        Me.txtJis.IsDakutenCheck = False
        Me.txtJis.IsEisuCheck = False
        Me.txtJis.IsForbiddenWordsCheck = False
        Me.txtJis.IsFullByteCheck = 0
        Me.txtJis.IsHankakuCheck = False
        Me.txtJis.IsHissuCheck = False
        Me.txtJis.IsKanaCheck = False
        Me.txtJis.IsMiddleSpace = False
        Me.txtJis.IsNumericCheck = False
        Me.txtJis.IsSujiCheck = False
        Me.txtJis.IsZenkakuCheck = False
        Me.txtJis.ItemName = ""
        Me.txtJis.LineSpace = 0
        Me.txtJis.Location = New System.Drawing.Point(87, 15)
        Me.txtJis.MaxLength = 7
        Me.txtJis.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtJis.MaxLineCount = 0
        Me.txtJis.Multiline = False
        Me.txtJis.Name = "txtJis"
        Me.txtJis.ReadOnly = False
        Me.txtJis.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtJis.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtJis.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtJis.Size = New System.Drawing.Size(75, 18)
        Me.txtJis.TabIndex = 640
        Me.txtJis.TabStopSetting = True
        Me.txtJis.Tag = ""
        Me.txtJis.TextValue = ""
        Me.txtJis.UseSystemPasswordChar = False
        Me.txtJis.WidthDef = 75
        Me.txtJis.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleJis
        '
        Me.lblTitleJis.AutoSize = True
        Me.lblTitleJis.AutoSizeDef = True
        Me.lblTitleJis.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJis.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJis.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJis.EnableStatus = False
        Me.lblTitleJis.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJis.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJis.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJis.HeightDef = 13
        Me.lblTitleJis.Location = New System.Drawing.Point(15, 16)
        Me.lblTitleJis.Name = "lblTitleJis"
        Me.lblTitleJis.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleJis.TabIndex = 639
        Me.lblTitleJis.Text = "JISコード"
        Me.lblTitleJis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJis.TextValue = "JISコード"
        Me.lblTitleJis.WidthDef = 70
        '
        'pnlEdit
        '
        Me.pnlEdit.Controls.Add(Me.txtAreaNm)
        Me.pnlEdit.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlEdit.Controls.Add(Me.txtAreaCd)
        Me.pnlEdit.Controls.Add(Me.lblTitleArea)
        Me.pnlEdit.Controls.Add(Me.lblSituation)
        Me.pnlEdit.Controls.Add(Me.lblSysDelFlg)
        Me.pnlEdit.Controls.Add(Me.lblTitleInfo)
        Me.pnlEdit.Controls.Add(Me.lblUpdTime)
        Me.pnlEdit.Controls.Add(Me.lblUpdDate)
        Me.pnlEdit.Controls.Add(Me.lblTitleNrsBrCd)
        Me.pnlEdit.Controls.Add(Me.lblCrtUser)
        Me.pnlEdit.Controls.Add(Me.txtAreaInfo)
        Me.pnlEdit.Controls.Add(Me.lblUpdUser)
        Me.pnlEdit.Controls.Add(Me.sprDetail2)
        Me.pnlEdit.Controls.Add(Me.LmTitleLabel9)
        Me.pnlEdit.Controls.Add(Me.btnRowAdd)
        Me.pnlEdit.Controls.Add(Me.lblCrtDate)
        Me.pnlEdit.Controls.Add(Me.btnRowDel)
        Me.pnlEdit.Controls.Add(Me.LmTitleLabel10)
        Me.pnlEdit.Controls.Add(Me.LmTitleLabel19)
        Me.pnlEdit.Controls.Add(Me.LmTitleLabel12)
        Me.pnlEdit.Controls.Add(Me.cmbBin)
        Me.pnlEdit.Controls.Add(Me.LmTitleLabel1)
        Me.pnlEdit.Location = New System.Drawing.Point(12, 382)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(1259, 497)
        Me.pnlEdit.TabIndex = 643
        '
        'LMM230F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM230F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEdit.ResumeLayout(False)
        Me.pnlEdit.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleArea As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblTitleInfo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAreaNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtAreaInfo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtAreaCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents sprDetail2 As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents LmTitleLabel19 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbBin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel12 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblShi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblKen As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtJis As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleJis As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlEdit As System.Windows.Forms.Panel

End Class
