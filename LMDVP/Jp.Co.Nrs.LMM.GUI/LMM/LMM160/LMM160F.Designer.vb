<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM160F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM160F))
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.lblTitleCrtUser = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCrtDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUpdUser = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUpdDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleDest = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleDelverGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleWorkSeiq = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblWorkSeiqNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtWorkSeiqCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtDelverGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblGoodsNrs = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlShipWork = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblWork2Nm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtWork2Kb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleWork2Kb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblWork1Nm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtWork1Kb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleWork1Kb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlShipWork.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbNrsBrCd)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNrs)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.pnlShipWork)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNm)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCd)
        Me.pnlViewAria.Controls.Add(Me.txtDelverGoodsNm)
        Me.pnlViewAria.Controls.Add(Me.lblWorkSeiqNm)
        Me.pnlViewAria.Controls.Add(Me.txtWorkSeiqCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleWorkSeiq)
        Me.pnlViewAria.Controls.Add(Me.lblTitleDelverGoods)
        Me.pnlViewAria.Controls.Add(Me.lblTitleGoods)
        Me.pnlViewAria.Controls.Add(Me.lblDestNm)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdM)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCrtDate)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUpdUser)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleDest)
        Me.pnlViewAria.Controls.Add(Me.txtDestCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCustCd)
        Me.pnlViewAria.Controls.Add(Me.lblCustNmL)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
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
        Me.sprDetail.HeightDef = 373
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(17, 21)
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
        Me.sprDetail.Size = New System.Drawing.Size(1245, 373)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1245
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1127, 428)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 382
        Me.lblSituation.TabStop = False
        '
        'lblTitleCrtUser
        '
        Me.lblTitleCrtUser.AutoSizeDef = False
        Me.lblTitleCrtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtUser.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCrtUser.EnableStatus = False
        Me.lblTitleCrtUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtUser.HeightDef = 18
        Me.lblTitleCrtUser.Location = New System.Drawing.Point(1065, 460)
        Me.lblTitleCrtUser.Name = "lblTitleCrtUser"
        Me.lblTitleCrtUser.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleCrtUser.TabIndex = 381
        Me.lblTitleCrtUser.Text = "作成者"
        Me.lblTitleCrtUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCrtUser.TextValue = "作成者"
        Me.lblTitleCrtUser.WidthDef = 49
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
        Me.lblCrtUser.IsByteCheck = 0
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1120, 461)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 383
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCrtDate
        '
        Me.lblTitleCrtDate.AutoSizeDef = False
        Me.lblTitleCrtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCrtDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCrtDate.EnableStatus = False
        Me.lblTitleCrtDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCrtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCrtDate.HeightDef = 18
        Me.lblTitleCrtDate.Location = New System.Drawing.Point(1065, 483)
        Me.lblTitleCrtDate.Name = "lblTitleCrtDate"
        Me.lblTitleCrtDate.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleCrtDate.TabIndex = 384
        Me.lblTitleCrtDate.Text = "作成日"
        Me.lblTitleCrtDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCrtDate.TextValue = "作成日"
        Me.lblTitleCrtDate.WidthDef = 49
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
        Me.lblCrtDate.IsByteCheck = 0
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1120, 483)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 385
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUpdUser
        '
        Me.lblTitleUpdUser.AutoSizeDef = False
        Me.lblTitleUpdUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdUser.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUpdUser.EnableStatus = False
        Me.lblTitleUpdUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdUser.HeightDef = 18
        Me.lblTitleUpdUser.Location = New System.Drawing.Point(1065, 505)
        Me.lblTitleUpdUser.Name = "lblTitleUpdUser"
        Me.lblTitleUpdUser.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleUpdUser.TabIndex = 386
        Me.lblTitleUpdUser.Text = "更新者"
        Me.lblTitleUpdUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUpdUser.TextValue = "更新者"
        Me.lblTitleUpdUser.WidthDef = 49
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
        Me.lblUpdUser.IsByteCheck = 0
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1120, 505)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 387
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUpdDate
        '
        Me.lblTitleUpdDate.AutoSizeDef = False
        Me.lblTitleUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUpdDate.EnableStatus = False
        Me.lblTitleUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdDate.HeightDef = 18
        Me.lblTitleUpdDate.Location = New System.Drawing.Point(1065, 527)
        Me.lblTitleUpdDate.Name = "lblTitleUpdDate"
        Me.lblTitleUpdDate.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleUpdDate.TabIndex = 388
        Me.lblTitleUpdDate.Text = "更新日"
        Me.lblTitleUpdDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUpdDate.TextValue = "更新日"
        Me.lblTitleUpdDate.WidthDef = 49
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
        Me.lblUpdDate.IsByteCheck = 0
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1120, 527)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 389
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDest
        '
        Me.lblTitleDest.AutoSize = True
        Me.lblTitleDest.AutoSizeDef = True
        Me.lblTitleDest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDest.EnableStatus = False
        Me.lblTitleDest.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.HeightDef = 13
        Me.lblTitleDest.Location = New System.Drawing.Point(94, 492)
        Me.lblTitleDest.Name = "lblTitleDest"
        Me.lblTitleDest.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleDest.TabIndex = 404
        Me.lblTitleDest.Text = "届先"
        Me.lblTitleDest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDest.TextValue = "届先"
        Me.lblTitleDest.WidthDef = 35
        '
        'txtDestCd
        '
        Me.txtDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestCd.CountWrappedLine = False
        Me.txtDestCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestCd.HeightDef = 18
        Me.txtDestCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestCd.HissuLabelVisible = True
        Me.txtDestCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtDestCd.IsByteCheck = 15
        Me.txtDestCd.IsCalendarCheck = False
        Me.txtDestCd.IsDakutenCheck = False
        Me.txtDestCd.IsEisuCheck = False
        Me.txtDestCd.IsForbiddenWordsCheck = False
        Me.txtDestCd.IsFullByteCheck = 0
        Me.txtDestCd.IsHankakuCheck = False
        Me.txtDestCd.IsHissuCheck = True
        Me.txtDestCd.IsKanaCheck = False
        Me.txtDestCd.IsMiddleSpace = False
        Me.txtDestCd.IsNumericCheck = False
        Me.txtDestCd.IsSujiCheck = False
        Me.txtDestCd.IsZenkakuCheck = False
        Me.txtDestCd.ItemName = ""
        Me.txtDestCd.LineSpace = 0
        Me.txtDestCd.Location = New System.Drawing.Point(131, 491)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(133, 18)
        Me.txtDestCd.TabIndex = 405
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = ""
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 133
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblDestNm.HissuLabelVisible = True
        Me.lblDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestNm.IsByteCheck = 0
        Me.lblDestNm.IsCalendarCheck = False
        Me.lblDestNm.IsDakutenCheck = False
        Me.lblDestNm.IsEisuCheck = False
        Me.lblDestNm.IsForbiddenWordsCheck = False
        Me.lblDestNm.IsFullByteCheck = 0
        Me.lblDestNm.IsHankakuCheck = False
        Me.lblDestNm.IsHissuCheck = True
        Me.lblDestNm.IsKanaCheck = False
        Me.lblDestNm.IsMiddleSpace = False
        Me.lblDestNm.IsNumericCheck = False
        Me.lblDestNm.IsSujiCheck = False
        Me.lblDestNm.IsZenkakuCheck = False
        Me.lblDestNm.ItemName = ""
        Me.lblDestNm.LineSpace = 0
        Me.lblDestNm.Location = New System.Drawing.Point(248, 491)
        Me.lblDestNm.MaxLength = 0
        Me.lblDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestNm.MaxLineCount = 0
        Me.lblDestNm.Multiline = False
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.ReadOnly = True
        Me.lblDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestNm.Size = New System.Drawing.Size(554, 18)
        Me.lblDestNm.TabIndex = 400
        Me.lblDestNm.TabStop = False
        Me.lblDestNm.TabStopSetting = False
        Me.lblDestNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.lblDestNm.UseSystemPasswordChar = False
        Me.lblDestNm.WidthDef = 554
        Me.lblDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCustCd.Location = New System.Drawing.Point(94, 450)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustCd.TabIndex = 397
        Me.lblTitleCustCd.Text = "荷主"
        Me.lblTitleCustCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustCd.TextValue = "荷主"
        Me.lblTitleCustCd.WidthDef = 35
        '
        'lblCustNmL
        '
        Me.lblCustNmL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmL.CountWrappedLine = False
        Me.lblCustNmL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmL.HeightDef = 18
        Me.lblCustNmL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmL.HissuLabelVisible = True
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = True
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(212, 449)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 396
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 473
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdL.Location = New System.Drawing.Point(131, 449)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 395
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "X1"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNmM
        '
        Me.lblCustNmM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmM.CountWrappedLine = False
        Me.lblCustNmM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmM.HeightDef = 18
        Me.lblCustNmM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmM.HissuLabelVisible = True
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = True
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(212, 470)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmM.TabIndex = 460
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 473
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdM.Location = New System.Drawing.Point(176, 470)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 459
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = "xx"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleGoods.Location = New System.Drawing.Point(94, 513)
        Me.lblTitleGoods.Name = "lblTitleGoods"
        Me.lblTitleGoods.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoods.TabIndex = 461
        Me.lblTitleGoods.Text = "商品"
        Me.lblTitleGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoods.TextValue = "商品"
        Me.lblTitleGoods.WidthDef = 35
        '
        'lblTitleDelverGoods
        '
        Me.lblTitleDelverGoods.AutoSize = True
        Me.lblTitleDelverGoods.AutoSizeDef = True
        Me.lblTitleDelverGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDelverGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDelverGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDelverGoods.EnableStatus = False
        Me.lblTitleDelverGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDelverGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDelverGoods.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDelverGoods.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDelverGoods.HeightDef = 13
        Me.lblTitleDelverGoods.Location = New System.Drawing.Point(10, 535)
        Me.lblTitleDelverGoods.Name = "lblTitleDelverGoods"
        Me.lblTitleDelverGoods.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleDelverGoods.TabIndex = 462
        Me.lblTitleDelverGoods.Text = "納品書表示商品名"
        Me.lblTitleDelverGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDelverGoods.TextValue = "納品書表示商品名"
        Me.lblTitleDelverGoods.WidthDef = 119
        '
        'lblTitleWorkSeiq
        '
        Me.lblTitleWorkSeiq.AutoSize = True
        Me.lblTitleWorkSeiq.AutoSizeDef = True
        Me.lblTitleWorkSeiq.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWorkSeiq.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWorkSeiq.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWorkSeiq.EnableStatus = False
        Me.lblTitleWorkSeiq.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWorkSeiq.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWorkSeiq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWorkSeiq.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWorkSeiq.HeightDef = 13
        Me.lblTitleWorkSeiq.Location = New System.Drawing.Point(38, 556)
        Me.lblTitleWorkSeiq.Name = "lblTitleWorkSeiq"
        Me.lblTitleWorkSeiq.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleWorkSeiq.TabIndex = 466
        Me.lblTitleWorkSeiq.Text = "作業料請求先"
        Me.lblTitleWorkSeiq.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWorkSeiq.TextValue = "作業料請求先"
        Me.lblTitleWorkSeiq.WidthDef = 91
        '
        'lblWorkSeiqNm
        '
        Me.lblWorkSeiqNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWorkSeiqNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWorkSeiqNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWorkSeiqNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWorkSeiqNm.CountWrappedLine = False
        Me.lblWorkSeiqNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWorkSeiqNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWorkSeiqNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWorkSeiqNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWorkSeiqNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWorkSeiqNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWorkSeiqNm.HeightDef = 18
        Me.lblWorkSeiqNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWorkSeiqNm.HissuLabelVisible = False
        Me.lblWorkSeiqNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWorkSeiqNm.IsByteCheck = 0
        Me.lblWorkSeiqNm.IsCalendarCheck = False
        Me.lblWorkSeiqNm.IsDakutenCheck = False
        Me.lblWorkSeiqNm.IsEisuCheck = False
        Me.lblWorkSeiqNm.IsForbiddenWordsCheck = False
        Me.lblWorkSeiqNm.IsFullByteCheck = 0
        Me.lblWorkSeiqNm.IsHankakuCheck = False
        Me.lblWorkSeiqNm.IsHissuCheck = False
        Me.lblWorkSeiqNm.IsKanaCheck = False
        Me.lblWorkSeiqNm.IsMiddleSpace = False
        Me.lblWorkSeiqNm.IsNumericCheck = False
        Me.lblWorkSeiqNm.IsSujiCheck = False
        Me.lblWorkSeiqNm.IsZenkakuCheck = False
        Me.lblWorkSeiqNm.ItemName = ""
        Me.lblWorkSeiqNm.LineSpace = 0
        Me.lblWorkSeiqNm.Location = New System.Drawing.Point(226, 554)
        Me.lblWorkSeiqNm.MaxLength = 0
        Me.lblWorkSeiqNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWorkSeiqNm.MaxLineCount = 0
        Me.lblWorkSeiqNm.Multiline = False
        Me.lblWorkSeiqNm.Name = "lblWorkSeiqNm"
        Me.lblWorkSeiqNm.ReadOnly = True
        Me.lblWorkSeiqNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWorkSeiqNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWorkSeiqNm.Size = New System.Drawing.Size(576, 18)
        Me.lblWorkSeiqNm.TabIndex = 470
        Me.lblWorkSeiqNm.TabStop = False
        Me.lblWorkSeiqNm.TabStopSetting = False
        Me.lblWorkSeiqNm.TextValue = ""
        Me.lblWorkSeiqNm.UseSystemPasswordChar = False
        Me.lblWorkSeiqNm.WidthDef = 576
        Me.lblWorkSeiqNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtWorkSeiqCd
        '
        Me.txtWorkSeiqCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWorkSeiqCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWorkSeiqCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWorkSeiqCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtWorkSeiqCd.CountWrappedLine = False
        Me.txtWorkSeiqCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWorkSeiqCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWorkSeiqCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWorkSeiqCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWorkSeiqCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWorkSeiqCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWorkSeiqCd.HeightDef = 18
        Me.txtWorkSeiqCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWorkSeiqCd.HissuLabelVisible = False
        Me.txtWorkSeiqCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtWorkSeiqCd.IsByteCheck = 7
        Me.txtWorkSeiqCd.IsCalendarCheck = False
        Me.txtWorkSeiqCd.IsDakutenCheck = False
        Me.txtWorkSeiqCd.IsEisuCheck = False
        Me.txtWorkSeiqCd.IsForbiddenWordsCheck = False
        Me.txtWorkSeiqCd.IsFullByteCheck = 0
        Me.txtWorkSeiqCd.IsHankakuCheck = False
        Me.txtWorkSeiqCd.IsHissuCheck = False
        Me.txtWorkSeiqCd.IsKanaCheck = False
        Me.txtWorkSeiqCd.IsMiddleSpace = False
        Me.txtWorkSeiqCd.IsNumericCheck = False
        Me.txtWorkSeiqCd.IsSujiCheck = False
        Me.txtWorkSeiqCd.IsZenkakuCheck = False
        Me.txtWorkSeiqCd.ItemName = ""
        Me.txtWorkSeiqCd.LineSpace = 0
        Me.txtWorkSeiqCd.Location = New System.Drawing.Point(131, 554)
        Me.txtWorkSeiqCd.MaxLength = 7
        Me.txtWorkSeiqCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWorkSeiqCd.MaxLineCount = 0
        Me.txtWorkSeiqCd.Multiline = False
        Me.txtWorkSeiqCd.Name = "txtWorkSeiqCd"
        Me.txtWorkSeiqCd.ReadOnly = False
        Me.txtWorkSeiqCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWorkSeiqCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWorkSeiqCd.Size = New System.Drawing.Size(111, 18)
        Me.txtWorkSeiqCd.TabIndex = 469
        Me.txtWorkSeiqCd.TabStopSetting = True
        Me.txtWorkSeiqCd.TextValue = ""
        Me.txtWorkSeiqCd.UseSystemPasswordChar = False
        Me.txtWorkSeiqCd.WidthDef = 111
        Me.txtWorkSeiqCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtDelverGoodsNm
        '
        Me.txtDelverGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDelverGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDelverGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelverGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDelverGoodsNm.CountWrappedLine = False
        Me.txtDelverGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDelverGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDelverGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDelverGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDelverGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDelverGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDelverGoodsNm.HeightDef = 18
        Me.txtDelverGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDelverGoodsNm.HissuLabelVisible = False
        Me.txtDelverGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDelverGoodsNm.IsByteCheck = 60
        Me.txtDelverGoodsNm.IsCalendarCheck = False
        Me.txtDelverGoodsNm.IsDakutenCheck = False
        Me.txtDelverGoodsNm.IsEisuCheck = False
        Me.txtDelverGoodsNm.IsForbiddenWordsCheck = False
        Me.txtDelverGoodsNm.IsFullByteCheck = 0
        Me.txtDelverGoodsNm.IsHankakuCheck = False
        Me.txtDelverGoodsNm.IsHissuCheck = False
        Me.txtDelverGoodsNm.IsKanaCheck = False
        Me.txtDelverGoodsNm.IsMiddleSpace = False
        Me.txtDelverGoodsNm.IsNumericCheck = False
        Me.txtDelverGoodsNm.IsSujiCheck = False
        Me.txtDelverGoodsNm.IsZenkakuCheck = False
        Me.txtDelverGoodsNm.ItemName = ""
        Me.txtDelverGoodsNm.LineSpace = 0
        Me.txtDelverGoodsNm.Location = New System.Drawing.Point(131, 533)
        Me.txtDelverGoodsNm.MaxLength = 60
        Me.txtDelverGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDelverGoodsNm.MaxLineCount = 0
        Me.txtDelverGoodsNm.Multiline = False
        Me.txtDelverGoodsNm.Name = "txtDelverGoodsNm"
        Me.txtDelverGoodsNm.ReadOnly = False
        Me.txtDelverGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDelverGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDelverGoodsNm.Size = New System.Drawing.Size(671, 18)
        Me.txtDelverGoodsNm.TabIndex = 474
        Me.txtDelverGoodsNm.TabStopSetting = True
        Me.txtDelverGoodsNm.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---X"
        Me.txtDelverGoodsNm.UseSystemPasswordChar = False
        Me.txtDelverGoodsNm.WidthDef = 671
        Me.txtDelverGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.txtGoodsCd.HissuLabelVisible = True
        Me.txtGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtGoodsCd.IsByteCheck = 20
        Me.txtGoodsCd.IsCalendarCheck = False
        Me.txtGoodsCd.IsDakutenCheck = False
        Me.txtGoodsCd.IsEisuCheck = False
        Me.txtGoodsCd.IsForbiddenWordsCheck = False
        Me.txtGoodsCd.IsFullByteCheck = 0
        Me.txtGoodsCd.IsHankakuCheck = False
        Me.txtGoodsCd.IsHissuCheck = True
        Me.txtGoodsCd.IsKanaCheck = False
        Me.txtGoodsCd.IsMiddleSpace = False
        Me.txtGoodsCd.IsNumericCheck = False
        Me.txtGoodsCd.IsSujiCheck = False
        Me.txtGoodsCd.IsZenkakuCheck = False
        Me.txtGoodsCd.ItemName = ""
        Me.txtGoodsCd.LineSpace = 0
        Me.txtGoodsCd.Location = New System.Drawing.Point(131, 512)
        Me.txtGoodsCd.MaxLength = 20
        Me.txtGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCd.MaxLineCount = 0
        Me.txtGoodsCd.Multiline = False
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.ReadOnly = False
        Me.txtGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCd.Size = New System.Drawing.Size(167, 18)
        Me.txtGoodsCd.TabIndex = 476
        Me.txtGoodsCd.TabStopSetting = True
        Me.txtGoodsCd.TextValue = "xxxxxxxxxxxxxxx"
        Me.txtGoodsCd.UseSystemPasswordChar = False
        Me.txtGoodsCd.WidthDef = 167
        Me.txtGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsNrs
        '
        Me.lblGoodsNrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNrs.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNrs.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNrs.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNrs.CountWrappedLine = False
        Me.lblGoodsNrs.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNrs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNrs.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNrs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNrs.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNrs.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNrs.HeightDef = 18
        Me.lblGoodsNrs.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNrs.HissuLabelVisible = True
        Me.lblGoodsNrs.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.lblGoodsNrs.IsByteCheck = 0
        Me.lblGoodsNrs.IsCalendarCheck = False
        Me.lblGoodsNrs.IsDakutenCheck = False
        Me.lblGoodsNrs.IsEisuCheck = False
        Me.lblGoodsNrs.IsForbiddenWordsCheck = False
        Me.lblGoodsNrs.IsFullByteCheck = 0
        Me.lblGoodsNrs.IsHankakuCheck = False
        Me.lblGoodsNrs.IsHissuCheck = True
        Me.lblGoodsNrs.IsKanaCheck = False
        Me.lblGoodsNrs.IsMiddleSpace = False
        Me.lblGoodsNrs.IsNumericCheck = False
        Me.lblGoodsNrs.IsSujiCheck = False
        Me.lblGoodsNrs.IsZenkakuCheck = False
        Me.lblGoodsNrs.ItemName = ""
        Me.lblGoodsNrs.LineSpace = 0
        Me.lblGoodsNrs.Location = New System.Drawing.Point(635, 512)
        Me.lblGoodsNrs.MaxLength = 0
        Me.lblGoodsNrs.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNrs.MaxLineCount = 0
        Me.lblGoodsNrs.Multiline = False
        Me.lblGoodsNrs.Name = "lblGoodsNrs"
        Me.lblGoodsNrs.ReadOnly = True
        Me.lblGoodsNrs.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNrs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNrs.Size = New System.Drawing.Size(167, 18)
        Me.lblGoodsNrs.TabIndex = 478
        Me.lblGoodsNrs.TabStop = False
        Me.lblGoodsNrs.TabStopSetting = False
        Me.lblGoodsNrs.TextValue = ""
        Me.lblGoodsNrs.UseSystemPasswordChar = False
        Me.lblGoodsNrs.WidthDef = 167
        Me.lblGoodsNrs.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsNm
        '
        Me.lblGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNm.CountWrappedLine = False
        Me.lblGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNm.HeightDef = 18
        Me.lblGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.HissuLabelVisible = False
        Me.lblGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.lblGoodsNm.IsByteCheck = 0
        Me.lblGoodsNm.IsCalendarCheck = False
        Me.lblGoodsNm.IsDakutenCheck = False
        Me.lblGoodsNm.IsEisuCheck = False
        Me.lblGoodsNm.IsForbiddenWordsCheck = False
        Me.lblGoodsNm.IsFullByteCheck = 0
        Me.lblGoodsNm.IsHankakuCheck = False
        Me.lblGoodsNm.IsHissuCheck = False
        Me.lblGoodsNm.IsKanaCheck = False
        Me.lblGoodsNm.IsMiddleSpace = False
        Me.lblGoodsNm.IsNumericCheck = False
        Me.lblGoodsNm.IsSujiCheck = False
        Me.lblGoodsNm.IsZenkakuCheck = False
        Me.lblGoodsNm.ItemName = ""
        Me.lblGoodsNm.LineSpace = 0
        Me.lblGoodsNm.Location = New System.Drawing.Point(282, 512)
        Me.lblGoodsNm.MaxLength = 0
        Me.lblGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNm.MaxLineCount = 0
        Me.lblGoodsNm.Multiline = False
        Me.lblGoodsNm.Name = "lblGoodsNm"
        Me.lblGoodsNm.ReadOnly = True
        Me.lblGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNm.Size = New System.Drawing.Size(372, 18)
        Me.lblGoodsNm.TabIndex = 480
        Me.lblGoodsNm.TabStop = False
        Me.lblGoodsNm.TabStopSetting = False
        Me.lblGoodsNm.TextValue = ""
        Me.lblGoodsNm.UseSystemPasswordChar = False
        Me.lblGoodsNm.WidthDef = 372
        Me.lblGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlShipWork
        '
        Me.pnlShipWork.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShipWork.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShipWork.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShipWork.Controls.Add(Me.lblWork2Nm)
        Me.pnlShipWork.Controls.Add(Me.txtWork2Kb)
        Me.pnlShipWork.Controls.Add(Me.lblTitleWork2Kb)
        Me.pnlShipWork.Controls.Add(Me.lblWork1Nm)
        Me.pnlShipWork.Controls.Add(Me.txtWork1Kb)
        Me.pnlShipWork.Controls.Add(Me.lblTitleWork1Kb)
        Me.pnlShipWork.EnableStatus = False
        Me.pnlShipWork.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShipWork.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShipWork.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShipWork.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShipWork.HeightDef = 83
        Me.pnlShipWork.Location = New System.Drawing.Point(55, 577)
        Me.pnlShipWork.Name = "pnlShipWork"
        Me.pnlShipWork.Size = New System.Drawing.Size(648, 83)
        Me.pnlShipWork.TabIndex = 481
        Me.pnlShipWork.TabStop = False
        Me.pnlShipWork.Text = "出荷時作業"
        Me.pnlShipWork.TextValue = "出荷時作業"
        Me.pnlShipWork.WidthDef = 648
        '
        'lblWork2Nm
        '
        Me.lblWork2Nm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork2Nm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork2Nm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWork2Nm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWork2Nm.CountWrappedLine = False
        Me.lblWork2Nm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWork2Nm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWork2Nm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWork2Nm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWork2Nm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWork2Nm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWork2Nm.HeightDef = 18
        Me.lblWork2Nm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork2Nm.HissuLabelVisible = False
        Me.lblWork2Nm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWork2Nm.IsByteCheck = 0
        Me.lblWork2Nm.IsCalendarCheck = False
        Me.lblWork2Nm.IsDakutenCheck = False
        Me.lblWork2Nm.IsEisuCheck = False
        Me.lblWork2Nm.IsForbiddenWordsCheck = False
        Me.lblWork2Nm.IsFullByteCheck = 0
        Me.lblWork2Nm.IsHankakuCheck = False
        Me.lblWork2Nm.IsHissuCheck = False
        Me.lblWork2Nm.IsKanaCheck = False
        Me.lblWork2Nm.IsMiddleSpace = False
        Me.lblWork2Nm.IsNumericCheck = False
        Me.lblWork2Nm.IsSujiCheck = False
        Me.lblWork2Nm.IsZenkakuCheck = False
        Me.lblWork2Nm.ItemName = ""
        Me.lblWork2Nm.LineSpace = 0
        Me.lblWork2Nm.Location = New System.Drawing.Point(157, 43)
        Me.lblWork2Nm.MaxLength = 0
        Me.lblWork2Nm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWork2Nm.MaxLineCount = 0
        Me.lblWork2Nm.Multiline = False
        Me.lblWork2Nm.Name = "lblWork2Nm"
        Me.lblWork2Nm.ReadOnly = True
        Me.lblWork2Nm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWork2Nm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWork2Nm.Size = New System.Drawing.Size(473, 18)
        Me.lblWork2Nm.TabIndex = 483
        Me.lblWork2Nm.TabStop = False
        Me.lblWork2Nm.TabStopSetting = False
        Me.lblWork2Nm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblWork2Nm.UseSystemPasswordChar = False
        Me.lblWork2Nm.WidthDef = 473
        Me.lblWork2Nm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtWork2Kb
        '
        Me.txtWork2Kb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWork2Kb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWork2Kb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWork2Kb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtWork2Kb.CountWrappedLine = False
        Me.txtWork2Kb.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWork2Kb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWork2Kb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWork2Kb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWork2Kb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWork2Kb.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWork2Kb.HeightDef = 18
        Me.txtWork2Kb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWork2Kb.HissuLabelVisible = False
        Me.txtWork2Kb.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtWork2Kb.IsByteCheck = 5
        Me.txtWork2Kb.IsCalendarCheck = False
        Me.txtWork2Kb.IsDakutenCheck = False
        Me.txtWork2Kb.IsEisuCheck = False
        Me.txtWork2Kb.IsForbiddenWordsCheck = False
        Me.txtWork2Kb.IsFullByteCheck = 0
        Me.txtWork2Kb.IsHankakuCheck = False
        Me.txtWork2Kb.IsHissuCheck = False
        Me.txtWork2Kb.IsKanaCheck = False
        Me.txtWork2Kb.IsMiddleSpace = False
        Me.txtWork2Kb.IsNumericCheck = False
        Me.txtWork2Kb.IsSujiCheck = False
        Me.txtWork2Kb.IsZenkakuCheck = False
        Me.txtWork2Kb.ItemName = ""
        Me.txtWork2Kb.LineSpace = 0
        Me.txtWork2Kb.Location = New System.Drawing.Point(76, 43)
        Me.txtWork2Kb.MaxLength = 5
        Me.txtWork2Kb.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWork2Kb.MaxLineCount = 0
        Me.txtWork2Kb.Multiline = False
        Me.txtWork2Kb.Name = "txtWork2Kb"
        Me.txtWork2Kb.ReadOnly = False
        Me.txtWork2Kb.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWork2Kb.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWork2Kb.Size = New System.Drawing.Size(97, 18)
        Me.txtWork2Kb.TabIndex = 482
        Me.txtWork2Kb.TabStopSetting = True
        Me.txtWork2Kb.TextValue = "X1"
        Me.txtWork2Kb.UseSystemPasswordChar = False
        Me.txtWork2Kb.WidthDef = 97
        Me.txtWork2Kb.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleWork2Kb
        '
        Me.lblTitleWork2Kb.AutoSizeDef = False
        Me.lblTitleWork2Kb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWork2Kb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWork2Kb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWork2Kb.EnableStatus = False
        Me.lblTitleWork2Kb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWork2Kb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWork2Kb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWork2Kb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWork2Kb.HeightDef = 18
        Me.lblTitleWork2Kb.Location = New System.Drawing.Point(23, 43)
        Me.lblTitleWork2Kb.Name = "lblTitleWork2Kb"
        Me.lblTitleWork2Kb.Size = New System.Drawing.Size(52, 18)
        Me.lblTitleWork2Kb.TabIndex = 481
        Me.lblTitleWork2Kb.Text = "作業2"
        Me.lblTitleWork2Kb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWork2Kb.TextValue = "作業2"
        Me.lblTitleWork2Kb.WidthDef = 52
        '
        'lblWork1Nm
        '
        Me.lblWork1Nm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork1Nm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork1Nm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWork1Nm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWork1Nm.CountWrappedLine = False
        Me.lblWork1Nm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWork1Nm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWork1Nm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWork1Nm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWork1Nm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWork1Nm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWork1Nm.HeightDef = 18
        Me.lblWork1Nm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWork1Nm.HissuLabelVisible = False
        Me.lblWork1Nm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWork1Nm.IsByteCheck = 0
        Me.lblWork1Nm.IsCalendarCheck = False
        Me.lblWork1Nm.IsDakutenCheck = False
        Me.lblWork1Nm.IsEisuCheck = False
        Me.lblWork1Nm.IsForbiddenWordsCheck = False
        Me.lblWork1Nm.IsFullByteCheck = 0
        Me.lblWork1Nm.IsHankakuCheck = False
        Me.lblWork1Nm.IsHissuCheck = False
        Me.lblWork1Nm.IsKanaCheck = False
        Me.lblWork1Nm.IsMiddleSpace = False
        Me.lblWork1Nm.IsNumericCheck = False
        Me.lblWork1Nm.IsSujiCheck = False
        Me.lblWork1Nm.IsZenkakuCheck = False
        Me.lblWork1Nm.ItemName = ""
        Me.lblWork1Nm.LineSpace = 0
        Me.lblWork1Nm.Location = New System.Drawing.Point(157, 22)
        Me.lblWork1Nm.MaxLength = 0
        Me.lblWork1Nm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWork1Nm.MaxLineCount = 0
        Me.lblWork1Nm.Multiline = False
        Me.lblWork1Nm.Name = "lblWork1Nm"
        Me.lblWork1Nm.ReadOnly = True
        Me.lblWork1Nm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWork1Nm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWork1Nm.Size = New System.Drawing.Size(473, 18)
        Me.lblWork1Nm.TabIndex = 480
        Me.lblWork1Nm.TabStop = False
        Me.lblWork1Nm.TabStopSetting = False
        Me.lblWork1Nm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblWork1Nm.UseSystemPasswordChar = False
        Me.lblWork1Nm.WidthDef = 473
        Me.lblWork1Nm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtWork1Kb
        '
        Me.txtWork1Kb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWork1Kb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWork1Kb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWork1Kb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtWork1Kb.CountWrappedLine = False
        Me.txtWork1Kb.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWork1Kb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWork1Kb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWork1Kb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWork1Kb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWork1Kb.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWork1Kb.HeightDef = 18
        Me.txtWork1Kb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWork1Kb.HissuLabelVisible = False
        Me.txtWork1Kb.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtWork1Kb.IsByteCheck = 5
        Me.txtWork1Kb.IsCalendarCheck = False
        Me.txtWork1Kb.IsDakutenCheck = False
        Me.txtWork1Kb.IsEisuCheck = False
        Me.txtWork1Kb.IsForbiddenWordsCheck = False
        Me.txtWork1Kb.IsFullByteCheck = 0
        Me.txtWork1Kb.IsHankakuCheck = False
        Me.txtWork1Kb.IsHissuCheck = False
        Me.txtWork1Kb.IsKanaCheck = False
        Me.txtWork1Kb.IsMiddleSpace = False
        Me.txtWork1Kb.IsNumericCheck = False
        Me.txtWork1Kb.IsSujiCheck = False
        Me.txtWork1Kb.IsZenkakuCheck = False
        Me.txtWork1Kb.ItemName = ""
        Me.txtWork1Kb.LineSpace = 0
        Me.txtWork1Kb.Location = New System.Drawing.Point(76, 22)
        Me.txtWork1Kb.MaxLength = 5
        Me.txtWork1Kb.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWork1Kb.MaxLineCount = 0
        Me.txtWork1Kb.Multiline = False
        Me.txtWork1Kb.Name = "txtWork1Kb"
        Me.txtWork1Kb.ReadOnly = False
        Me.txtWork1Kb.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWork1Kb.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWork1Kb.Size = New System.Drawing.Size(97, 18)
        Me.txtWork1Kb.TabIndex = 479
        Me.txtWork1Kb.TabStopSetting = True
        Me.txtWork1Kb.TextValue = "X1"
        Me.txtWork1Kb.UseSystemPasswordChar = False
        Me.txtWork1Kb.WidthDef = 97
        Me.txtWork1Kb.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleWork1Kb
        '
        Me.lblTitleWork1Kb.AutoSizeDef = False
        Me.lblTitleWork1Kb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWork1Kb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWork1Kb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWork1Kb.EnableStatus = False
        Me.lblTitleWork1Kb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWork1Kb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWork1Kb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWork1Kb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWork1Kb.HeightDef = 18
        Me.lblTitleWork1Kb.Location = New System.Drawing.Point(23, 22)
        Me.lblTitleWork1Kb.Name = "lblTitleWork1Kb"
        Me.lblTitleWork1Kb.Size = New System.Drawing.Size(52, 18)
        Me.lblTitleWork1Kb.TabIndex = 478
        Me.lblTitleWork1Kb.Text = "作業1"
        Me.lblTitleWork1Kb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWork1Kb.TextValue = "作業1"
        Me.lblTitleWork1Kb.WidthDef = 52
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(80, 430)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 482
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1119, 551)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 594
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1120, 575)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 596
        Me.lblSysDelFlg.TabStop = False
        Me.lblSysDelFlg.TabStopSetting = False
        Me.lblSysDelFlg.TextValue = ""
        Me.lblSysDelFlg.UseSystemPasswordChar = False
        Me.lblSysDelFlg.Visible = False
        Me.lblSysDelFlg.WidthDef = 157
        Me.lblSysDelFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.cmbNrsBrCd.Location = New System.Drawing.Point(131, 428)
        Me.cmbNrsBrCd.Name = "cmbNrsBrCd"
        Me.cmbNrsBrCd.ReadOnly = True
        Me.cmbNrsBrCd.SelectedIndex = -1
        Me.cmbNrsBrCd.SelectedItem = Nothing
        Me.cmbNrsBrCd.SelectedText = ""
        Me.cmbNrsBrCd.SelectedValue = ""
        Me.cmbNrsBrCd.Size = New System.Drawing.Size(300, 18)
        Me.cmbNrsBrCd.TabIndex = 603
        Me.cmbNrsBrCd.TabStop = False
        Me.cmbNrsBrCd.TabStopSetting = False
        Me.cmbNrsBrCd.TextValue = ""
        Me.cmbNrsBrCd.ValueMember = Nothing
        Me.cmbNrsBrCd.WidthDef = 300
        '
        'LMM160F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM160F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlShipWork.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblTitleCrtUser As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCrtDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUpdUser As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUpdDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDest As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDelverGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleWorkSeiq As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblWorkSeiqNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtWorkSeiqCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDelverGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsNrs As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlShipWork As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblWork2Nm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtWork2Kb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleWork2Kb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblWork1Nm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtWork1Kb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleWork1Kb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr

End Class
