<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM170F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM170F))
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel12 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel13 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtKenK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel11 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtJisCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtZipNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtTownK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCityK = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtKenN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCityN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtTownN = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.txtTownN)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.txtCityN)
        Me.pnlViewAria.Controls.Add(Me.txtKenN)
        Me.pnlViewAria.Controls.Add(Me.txtCityK)
        Me.pnlViewAria.Controls.Add(Me.txtTownK)
        Me.pnlViewAria.Controls.Add(Me.txtZipNo)
        Me.pnlViewAria.Controls.Add(Me.txtJisCd)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel11)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel5)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.txtKenK)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel12)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel13)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
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
        Me.sprDetail.HeightDef = 342
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(19, 26)
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
        Me.sprDetail.Size = New System.Drawing.Size(1237, 342)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1237
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1121, 394)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 16
        Me.lblSituation.TabStop = False
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1086, 516)
        Me.lblUpdDate.MaxLength = 10
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(182, 18)
        Me.lblUpdDate.TabIndex = 80
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = "2011/01/10"
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 182
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1087, 495)
        Me.lblUpdUser.MaxLength = 20
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(181, 18)
        Me.lblUpdUser.TabIndex = 79
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = "Admin"
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 181
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1086, 474)
        Me.lblCrtDate.MaxLength = 10
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(182, 18)
        Me.lblCrtDate.TabIndex = 77
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = "2001/01/01"
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 182
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1035, 519)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel9.TabIndex = 3
        Me.LmTitleLabel9.Text = "更新日"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新日"
        Me.LmTitleLabel9.WidthDef = 49
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
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1036, 498)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel10.TabIndex = 2
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
        Me.LmTitleLabel12.Location = New System.Drawing.Point(1035, 478)
        Me.LmTitleLabel12.Name = "LmTitleLabel12"
        Me.LmTitleLabel12.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel12.TabIndex = 1
        Me.LmTitleLabel12.Text = "作成日"
        Me.LmTitleLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel12.TextValue = "作成日"
        Me.LmTitleLabel12.WidthDef = 49
        '
        'LmTitleLabel13
        '
        Me.LmTitleLabel13.AutoSize = True
        Me.LmTitleLabel13.AutoSizeDef = True
        Me.LmTitleLabel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel13.EnableStatus = False
        Me.LmTitleLabel13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.HeightDef = 13
        Me.LmTitleLabel13.Location = New System.Drawing.Point(1035, 456)
        Me.LmTitleLabel13.Name = "LmTitleLabel13"
        Me.LmTitleLabel13.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel13.TabIndex = 0
        Me.LmTitleLabel13.Text = "作成者"
        Me.LmTitleLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel13.TextValue = "作成者"
        Me.LmTitleLabel13.WidthDef = 49
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1086, 453)
        Me.lblCrtUser.MaxLength = 20
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(182, 18)
        Me.lblCrtUser.TabIndex = 79
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = "Admin"
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 182
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(21, 500)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel3.TabIndex = 316
        Me.LmTitleLabel3.Text = "都道府県名(漢字)"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "都道府県名(漢字)"
        Me.LmTitleLabel3.WidthDef = 119
        '
        'txtKenK
        '
        Me.txtKenK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKenK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKenK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKenK.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKenK.CountWrappedLine = False
        Me.txtKenK.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKenK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKenK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKenK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKenK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKenK.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKenK.HeightDef = 18
        Me.txtKenK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKenK.HissuLabelVisible = True
        Me.txtKenK.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_KANA
        Me.txtKenK.IsByteCheck = 10
        Me.txtKenK.IsCalendarCheck = False
        Me.txtKenK.IsDakutenCheck = False
        Me.txtKenK.IsEisuCheck = False
        Me.txtKenK.IsForbiddenWordsCheck = False
        Me.txtKenK.IsFullByteCheck = 0
        Me.txtKenK.IsHankakuCheck = False
        Me.txtKenK.IsHissuCheck = True
        Me.txtKenK.IsKanaCheck = False
        Me.txtKenK.IsMiddleSpace = False
        Me.txtKenK.IsNumericCheck = False
        Me.txtKenK.IsSujiCheck = False
        Me.txtKenK.IsZenkakuCheck = False
        Me.txtKenK.ItemName = ""
        Me.txtKenK.LineSpace = 0
        Me.txtKenK.Location = New System.Drawing.Point(142, 436)
        Me.txtKenK.MaxLength = 10
        Me.txtKenK.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKenK.MaxLineCount = 0
        Me.txtKenK.Multiline = False
        Me.txtKenK.Name = "txtKenK"
        Me.txtKenK.ReadOnly = False
        Me.txtKenK.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKenK.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKenK.Size = New System.Drawing.Size(110, 18)
        Me.txtKenK.TabIndex = 319
        Me.txtKenK.TabStopSetting = True
        Me.txtKenK.TextValue = ""
        Me.txtKenK.UseSystemPasswordChar = False
        Me.txtKenK.WidthDef = 110
        Me.txtKenK.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(70, 395)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(70, 13)
        Me.LmTitleLabel2.TabIndex = 320
        Me.LmTitleLabel2.Text = "JISコード"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "JISコード"
        Me.LmTitleLabel2.WidthDef = 70
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSize = True
        Me.LmTitleLabel4.AutoSizeDef = True
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 13
        Me.LmTitleLabel4.Location = New System.Drawing.Point(49, 542)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel4.TabIndex = 321
        Me.LmTitleLabel4.Text = "町域名(漢字)"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "町域名(漢字)"
        Me.LmTitleLabel4.WidthDef = 91
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
        Me.LmTitleLabel5.Location = New System.Drawing.Point(21, 459)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel5.TabIndex = 322
        Me.LmTitleLabel5.Text = "市区町村名(カナ)"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "市区町村名(カナ)"
        Me.LmTitleLabel5.WidthDef = 119
        '
        'LmTitleLabel6
        '
        Me.LmTitleLabel6.AutoSize = True
        Me.LmTitleLabel6.AutoSizeDef = True
        Me.LmTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel6.EnableStatus = False
        Me.LmTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.HeightDef = 13
        Me.LmTitleLabel6.Location = New System.Drawing.Point(77, 416)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel6.TabIndex = 323
        Me.LmTitleLabel6.Text = "郵便番号"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "郵便番号"
        Me.LmTitleLabel6.WidthDef = 63
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSize = True
        Me.LmTitleLabel7.AutoSizeDef = True
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 13
        Me.LmTitleLabel7.Location = New System.Drawing.Point(21, 437)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel7.TabIndex = 324
        Me.LmTitleLabel7.Text = "都道府県名(カナ)"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "都道府県名(カナ)"
        Me.LmTitleLabel7.WidthDef = 119
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSize = True
        Me.LmTitleLabel8.AutoSizeDef = True
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 13
        Me.LmTitleLabel8.Location = New System.Drawing.Point(49, 480)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel8.TabIndex = 325
        Me.LmTitleLabel8.Text = "町域名(カナ)"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "町域名(カナ)"
        Me.LmTitleLabel8.WidthDef = 91
        '
        'LmTitleLabel11
        '
        Me.LmTitleLabel11.AutoSize = True
        Me.LmTitleLabel11.AutoSizeDef = True
        Me.LmTitleLabel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel11.EnableStatus = False
        Me.LmTitleLabel11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.HeightDef = 13
        Me.LmTitleLabel11.Location = New System.Drawing.Point(21, 521)
        Me.LmTitleLabel11.Name = "LmTitleLabel11"
        Me.LmTitleLabel11.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel11.TabIndex = 326
        Me.LmTitleLabel11.Text = "市区町村名(漢字)"
        Me.LmTitleLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel11.TextValue = "市区町村名(漢字)"
        Me.LmTitleLabel11.WidthDef = 119
        '
        'txtJisCd
        '
        Me.txtJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJisCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtJisCd.CountWrappedLine = False
        Me.txtJisCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtJisCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtJisCd.HeightDef = 18
        Me.txtJisCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtJisCd.HissuLabelVisible = True
        Me.txtJisCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtJisCd.IsByteCheck = 7
        Me.txtJisCd.IsCalendarCheck = False
        Me.txtJisCd.IsDakutenCheck = False
        Me.txtJisCd.IsEisuCheck = False
        Me.txtJisCd.IsForbiddenWordsCheck = False
        Me.txtJisCd.IsFullByteCheck = 0
        Me.txtJisCd.IsHankakuCheck = False
        Me.txtJisCd.IsHissuCheck = True
        Me.txtJisCd.IsKanaCheck = False
        Me.txtJisCd.IsMiddleSpace = False
        Me.txtJisCd.IsNumericCheck = False
        Me.txtJisCd.IsSujiCheck = False
        Me.txtJisCd.IsZenkakuCheck = False
        Me.txtJisCd.ItemName = ""
        Me.txtJisCd.LineSpace = 0
        Me.txtJisCd.Location = New System.Drawing.Point(142, 394)
        Me.txtJisCd.MaxLength = 7
        Me.txtJisCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtJisCd.MaxLineCount = 0
        Me.txtJisCd.Multiline = False
        Me.txtJisCd.Name = "txtJisCd"
        Me.txtJisCd.ReadOnly = False
        Me.txtJisCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtJisCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtJisCd.Size = New System.Drawing.Size(110, 18)
        Me.txtJisCd.TabIndex = 327
        Me.txtJisCd.TabStopSetting = True
        Me.txtJisCd.TextValue = ""
        Me.txtJisCd.UseSystemPasswordChar = False
        Me.txtJisCd.WidthDef = 110
        Me.txtJisCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtZipNo
        '
        Me.txtZipNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZipNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtZipNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZipNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtZipNo.CountWrappedLine = False
        Me.txtZipNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtZipNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZipNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtZipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZipNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtZipNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtZipNo.HeightDef = 18
        Me.txtZipNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtZipNo.HissuLabelVisible = True
        Me.txtZipNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtZipNo.IsByteCheck = 7
        Me.txtZipNo.IsCalendarCheck = False
        Me.txtZipNo.IsDakutenCheck = False
        Me.txtZipNo.IsEisuCheck = False
        Me.txtZipNo.IsForbiddenWordsCheck = False
        Me.txtZipNo.IsFullByteCheck = 0
        Me.txtZipNo.IsHankakuCheck = False
        Me.txtZipNo.IsHissuCheck = True
        Me.txtZipNo.IsKanaCheck = False
        Me.txtZipNo.IsMiddleSpace = False
        Me.txtZipNo.IsNumericCheck = False
        Me.txtZipNo.IsSujiCheck = False
        Me.txtZipNo.IsZenkakuCheck = False
        Me.txtZipNo.ItemName = ""
        Me.txtZipNo.LineSpace = 0
        Me.txtZipNo.Location = New System.Drawing.Point(142, 415)
        Me.txtZipNo.MaxLength = 7
        Me.txtZipNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtZipNo.MaxLineCount = 0
        Me.txtZipNo.Multiline = False
        Me.txtZipNo.Name = "txtZipNo"
        Me.txtZipNo.ReadOnly = False
        Me.txtZipNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtZipNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtZipNo.Size = New System.Drawing.Size(110, 18)
        Me.txtZipNo.TabIndex = 328
        Me.txtZipNo.TabStopSetting = True
        Me.txtZipNo.TextValue = ""
        Me.txtZipNo.UseSystemPasswordChar = False
        Me.txtZipNo.WidthDef = 110
        Me.txtZipNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtTownK
        '
        Me.txtTownK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTownK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTownK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTownK.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTownK.CountWrappedLine = False
        Me.txtTownK.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTownK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTownK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTownK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTownK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTownK.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTownK.HeightDef = 18
        Me.txtTownK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTownK.HissuLabelVisible = False
        Me.txtTownK.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_KANA
        Me.txtTownK.IsByteCheck = 100
        Me.txtTownK.IsCalendarCheck = False
        Me.txtTownK.IsDakutenCheck = False
        Me.txtTownK.IsEisuCheck = False
        Me.txtTownK.IsForbiddenWordsCheck = False
        Me.txtTownK.IsFullByteCheck = 0
        Me.txtTownK.IsHankakuCheck = False
        Me.txtTownK.IsHissuCheck = False
        Me.txtTownK.IsKanaCheck = False
        Me.txtTownK.IsMiddleSpace = False
        Me.txtTownK.IsNumericCheck = False
        Me.txtTownK.IsSujiCheck = False
        Me.txtTownK.IsZenkakuCheck = False
        Me.txtTownK.ItemName = ""
        Me.txtTownK.LineSpace = 0
        Me.txtTownK.Location = New System.Drawing.Point(142, 478)
        Me.txtTownK.MaxLength = 100
        Me.txtTownK.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTownK.MaxLineCount = 0
        Me.txtTownK.Multiline = False
        Me.txtTownK.Name = "txtTownK"
        Me.txtTownK.ReadOnly = False
        Me.txtTownK.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTownK.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTownK.Size = New System.Drawing.Size(725, 18)
        Me.txtTownK.TabIndex = 333
        Me.txtTownK.TabStopSetting = True
        Me.txtTownK.TextValue = "ｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱ" & _
            "ｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵ"
        Me.txtTownK.UseSystemPasswordChar = False
        Me.txtTownK.WidthDef = 725
        Me.txtTownK.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCityK
        '
        Me.txtCityK.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCityK.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCityK.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCityK.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCityK.CountWrappedLine = False
        Me.txtCityK.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCityK.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCityK.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCityK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCityK.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCityK.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCityK.HeightDef = 18
        Me.txtCityK.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCityK.HissuLabelVisible = True
        Me.txtCityK.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_KANA
        Me.txtCityK.IsByteCheck = 40
        Me.txtCityK.IsCalendarCheck = False
        Me.txtCityK.IsDakutenCheck = False
        Me.txtCityK.IsEisuCheck = False
        Me.txtCityK.IsForbiddenWordsCheck = False
        Me.txtCityK.IsFullByteCheck = 0
        Me.txtCityK.IsHankakuCheck = False
        Me.txtCityK.IsHissuCheck = True
        Me.txtCityK.IsKanaCheck = False
        Me.txtCityK.IsMiddleSpace = False
        Me.txtCityK.IsNumericCheck = False
        Me.txtCityK.IsSujiCheck = False
        Me.txtCityK.IsZenkakuCheck = False
        Me.txtCityK.ItemName = ""
        Me.txtCityK.LineSpace = 0
        Me.txtCityK.Location = New System.Drawing.Point(142, 457)
        Me.txtCityK.MaxLength = 40
        Me.txtCityK.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCityK.MaxLineCount = 0
        Me.txtCityK.Multiline = False
        Me.txtCityK.Name = "txtCityK"
        Me.txtCityK.ReadOnly = False
        Me.txtCityK.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCityK.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCityK.Size = New System.Drawing.Size(314, 18)
        Me.txtCityK.TabIndex = 334
        Me.txtCityK.TabStopSetting = True
        Me.txtCityK.TextValue = "ｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳｴｵｱｲｳ"
        Me.txtCityK.UseSystemPasswordChar = False
        Me.txtCityK.WidthDef = 314
        Me.txtCityK.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtKenN
        '
        Me.txtKenN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKenN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKenN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKenN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKenN.CountWrappedLine = False
        Me.txtKenN.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKenN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKenN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKenN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKenN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKenN.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKenN.HeightDef = 18
        Me.txtKenN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKenN.HissuLabelVisible = True
        Me.txtKenN.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKenN.IsByteCheck = 10
        Me.txtKenN.IsCalendarCheck = False
        Me.txtKenN.IsDakutenCheck = False
        Me.txtKenN.IsEisuCheck = False
        Me.txtKenN.IsForbiddenWordsCheck = False
        Me.txtKenN.IsFullByteCheck = 0
        Me.txtKenN.IsHankakuCheck = False
        Me.txtKenN.IsHissuCheck = True
        Me.txtKenN.IsKanaCheck = False
        Me.txtKenN.IsMiddleSpace = False
        Me.txtKenN.IsNumericCheck = False
        Me.txtKenN.IsSujiCheck = False
        Me.txtKenN.IsZenkakuCheck = False
        Me.txtKenN.ItemName = ""
        Me.txtKenN.LineSpace = 0
        Me.txtKenN.Location = New System.Drawing.Point(142, 499)
        Me.txtKenN.MaxLength = 10
        Me.txtKenN.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKenN.MaxLineCount = 0
        Me.txtKenN.Multiline = False
        Me.txtKenN.Name = "txtKenN"
        Me.txtKenN.ReadOnly = False
        Me.txtKenN.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKenN.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKenN.Size = New System.Drawing.Size(110, 18)
        Me.txtKenN.TabIndex = 335
        Me.txtKenN.TabStopSetting = True
        Me.txtKenN.TextValue = ""
        Me.txtKenN.UseSystemPasswordChar = False
        Me.txtKenN.WidthDef = 110
        Me.txtKenN.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCityN
        '
        Me.txtCityN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCityN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCityN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCityN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCityN.CountWrappedLine = False
        Me.txtCityN.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCityN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCityN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCityN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCityN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCityN.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCityN.HeightDef = 18
        Me.txtCityN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCityN.HissuLabelVisible = True
        Me.txtCityN.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCityN.IsByteCheck = 40
        Me.txtCityN.IsCalendarCheck = False
        Me.txtCityN.IsDakutenCheck = False
        Me.txtCityN.IsEisuCheck = False
        Me.txtCityN.IsForbiddenWordsCheck = False
        Me.txtCityN.IsFullByteCheck = 0
        Me.txtCityN.IsHankakuCheck = False
        Me.txtCityN.IsHissuCheck = True
        Me.txtCityN.IsKanaCheck = False
        Me.txtCityN.IsMiddleSpace = False
        Me.txtCityN.IsNumericCheck = False
        Me.txtCityN.IsSujiCheck = False
        Me.txtCityN.IsZenkakuCheck = False
        Me.txtCityN.ItemName = ""
        Me.txtCityN.LineSpace = 0
        Me.txtCityN.Location = New System.Drawing.Point(142, 520)
        Me.txtCityN.MaxLength = 40
        Me.txtCityN.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCityN.MaxLineCount = 0
        Me.txtCityN.Multiline = False
        Me.txtCityN.Name = "txtCityN"
        Me.txtCityN.ReadOnly = False
        Me.txtCityN.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCityN.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCityN.Size = New System.Drawing.Size(314, 18)
        Me.txtCityN.TabIndex = 336
        Me.txtCityN.TabStopSetting = True
        Me.txtCityN.TextValue = "Ｎ－－－－－－－－－－－－－－－－－４０"
        Me.txtCityN.UseSystemPasswordChar = False
        Me.txtCityN.WidthDef = 314
        Me.txtCityN.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtTownN
        '
        Me.txtTownN.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTownN.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTownN.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTownN.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTownN.CountWrappedLine = False
        Me.txtTownN.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTownN.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTownN.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTownN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTownN.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTownN.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTownN.HeightDef = 18
        Me.txtTownN.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTownN.HissuLabelVisible = False
        Me.txtTownN.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtTownN.IsByteCheck = 100
        Me.txtTownN.IsCalendarCheck = False
        Me.txtTownN.IsDakutenCheck = False
        Me.txtTownN.IsEisuCheck = False
        Me.txtTownN.IsForbiddenWordsCheck = False
        Me.txtTownN.IsFullByteCheck = 0
        Me.txtTownN.IsHankakuCheck = False
        Me.txtTownN.IsHissuCheck = False
        Me.txtTownN.IsKanaCheck = False
        Me.txtTownN.IsMiddleSpace = False
        Me.txtTownN.IsNumericCheck = False
        Me.txtTownN.IsSujiCheck = False
        Me.txtTownN.IsZenkakuCheck = False
        Me.txtTownN.ItemName = ""
        Me.txtTownN.LineSpace = 0
        Me.txtTownN.Location = New System.Drawing.Point(142, 541)
        Me.txtTownN.MaxLength = 100
        Me.txtTownN.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTownN.MaxLineCount = 0
        Me.txtTownN.Multiline = False
        Me.txtTownN.Name = "txtTownN"
        Me.txtTownN.ReadOnly = False
        Me.txtTownN.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTownN.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTownN.Size = New System.Drawing.Size(725, 18)
        Me.txtTownN.TabIndex = 337
        Me.txtTownN.TabStopSetting = True
        Me.txtTownN.TextValue = ""
        Me.txtTownN.UseSystemPasswordChar = False
        Me.txtTownN.WidthDef = 725
        Me.txtTownN.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblSysDelFlg.IsByteCheck = 10
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1086, 558)
        Me.lblSysDelFlg.MaxLength = 10
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(181, 18)
        Me.lblSysDelFlg.TabIndex = 612
        Me.lblSysDelFlg.TabStop = False
        Me.lblSysDelFlg.TabStopSetting = False
        Me.lblSysDelFlg.TextValue = ""
        Me.lblSysDelFlg.UseSystemPasswordChar = False
        Me.lblSysDelFlg.Visible = False
        Me.lblSysDelFlg.WidthDef = 181
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
        Me.lblUpdTime.IsByteCheck = 10
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1086, 537)
        Me.lblUpdTime.MaxLength = 10
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(181, 18)
        Me.lblUpdTime.TabIndex = 611
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 181
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMM170F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM170F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel12 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel13 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKenK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel11 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCityK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTownK As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtZipNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtJisCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtKenN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTownN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCityN As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
