<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM110F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM110F))
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.txtYearMoto = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel43 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtYearSaki = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblMsg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel15 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnCopy = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblSysDelFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCrtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.imdHol = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblMotoHol = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblMotoHol)
        Me.pnlViewAria.Controls.Add(Me.imdHol)
        Me.pnlViewAria.Controls.Add(Me.lblSysDelFlg)
        Me.pnlViewAria.Controls.Add(Me.lblUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.lblCrtUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.lblCrtDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel9)
        Me.pnlViewAria.Controls.Add(Me.lblUpdUser)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel10)
        Me.pnlViewAria.Controls.Add(Me.lblUpdDate)
        Me.pnlViewAria.Controls.Add(Me.btnCopy)
        Me.pnlViewAria.Controls.Add(Me.txtRemark)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel15)
        Me.pnlViewAria.Controls.Add(Me.lblMsg)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.txtYearSaki)
        Me.pnlViewAria.Controls.Add(Me.txtYearMoto)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel43)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        '
        'txtYearMoto
        '
        Me.txtYearMoto.AutoSize = True
        Me.txtYearMoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYearMoto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYearMoto.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYearMoto.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtYearMoto.CountWrappedLine = False
        Me.txtYearMoto.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtYearMoto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYearMoto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYearMoto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYearMoto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYearMoto.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtYearMoto.HeightDef = 18
        Me.txtYearMoto.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtYearMoto.HissuLabelVisible = False
        Me.txtYearMoto.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtYearMoto.IsByteCheck = 4
        Me.txtYearMoto.IsCalendarCheck = False
        Me.txtYearMoto.IsDakutenCheck = False
        Me.txtYearMoto.IsEisuCheck = False
        Me.txtYearMoto.IsForbiddenWordsCheck = False
        Me.txtYearMoto.IsFullByteCheck = 0
        Me.txtYearMoto.IsHankakuCheck = False
        Me.txtYearMoto.IsHissuCheck = False
        Me.txtYearMoto.IsKanaCheck = False
        Me.txtYearMoto.IsMiddleSpace = False
        Me.txtYearMoto.IsNumericCheck = False
        Me.txtYearMoto.IsSujiCheck = False
        Me.txtYearMoto.IsZenkakuCheck = False
        Me.txtYearMoto.ItemName = ""
        Me.txtYearMoto.LineSpace = 0
        Me.txtYearMoto.Location = New System.Drawing.Point(85, 10)
        Me.txtYearMoto.MaxLength = 4
        Me.txtYearMoto.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtYearMoto.MaxLineCount = 0
        Me.txtYearMoto.Multiline = False
        Me.txtYearMoto.Name = "txtYearMoto"
        Me.txtYearMoto.ReadOnly = False
        Me.txtYearMoto.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtYearMoto.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtYearMoto.Size = New System.Drawing.Size(79, 18)
        Me.txtYearMoto.TabIndex = 75
        Me.txtYearMoto.TabStopSetting = True
        Me.txtYearMoto.TextValue = ""
        Me.txtYearMoto.UseSystemPasswordChar = False
        Me.txtYearMoto.WidthDef = 79
        Me.txtYearMoto.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel43
        '
        Me.LmTitleLabel43.AutoSize = True
        Me.LmTitleLabel43.AutoSizeDef = True
        Me.LmTitleLabel43.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel43.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel43.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel43.EnableStatus = False
        Me.LmTitleLabel43.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel43.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel43.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel43.HeightDef = 13
        Me.LmTitleLabel43.Location = New System.Drawing.Point(62, 12)
        Me.LmTitleLabel43.Name = "LmTitleLabel43"
        Me.LmTitleLabel43.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel43.TabIndex = 75
        Me.LmTitleLabel43.Text = "年"
        Me.LmTitleLabel43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel43.TextValue = "年"
        Me.LmTitleLabel43.WidthDef = 21
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
        Me.sprDetail.Location = New System.Drawing.Point(17, 37)
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(20, 426)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel1.TabIndex = 81
        Me.LmTitleLabel1.Text = "業務休日"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "業務休日"
        Me.LmTitleLabel1.WidthDef = 63
        '
        'txtYearSaki
        '
        Me.txtYearSaki.AutoSize = True
        Me.txtYearSaki.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYearSaki.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYearSaki.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYearSaki.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtYearSaki.CountWrappedLine = False
        Me.txtYearSaki.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtYearSaki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYearSaki.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYearSaki.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYearSaki.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYearSaki.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtYearSaki.HeightDef = 18
        Me.txtYearSaki.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtYearSaki.HissuLabelVisible = False
        Me.txtYearSaki.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUMBER
        Me.txtYearSaki.IsByteCheck = 4
        Me.txtYearSaki.IsCalendarCheck = False
        Me.txtYearSaki.IsDakutenCheck = False
        Me.txtYearSaki.IsEisuCheck = False
        Me.txtYearSaki.IsForbiddenWordsCheck = False
        Me.txtYearSaki.IsFullByteCheck = 0
        Me.txtYearSaki.IsHankakuCheck = False
        Me.txtYearSaki.IsHissuCheck = False
        Me.txtYearSaki.IsKanaCheck = False
        Me.txtYearSaki.IsMiddleSpace = False
        Me.txtYearSaki.IsNumericCheck = False
        Me.txtYearSaki.IsSujiCheck = False
        Me.txtYearSaki.IsZenkakuCheck = False
        Me.txtYearSaki.ItemName = ""
        Me.txtYearSaki.LineSpace = 0
        Me.txtYearSaki.Location = New System.Drawing.Point(205, 10)
        Me.txtYearSaki.MaxLength = 4
        Me.txtYearSaki.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtYearSaki.MaxLineCount = 0
        Me.txtYearSaki.Multiline = False
        Me.txtYearSaki.Name = "txtYearSaki"
        Me.txtYearSaki.ReadOnly = False
        Me.txtYearSaki.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtYearSaki.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtYearSaki.Size = New System.Drawing.Size(79, 18)
        Me.txtYearSaki.TabIndex = 83
        Me.txtYearSaki.TabStopSetting = True
        Me.txtYearSaki.TextValue = ""
        Me.txtYearSaki.UseSystemPasswordChar = False
        Me.txtYearSaki.WidthDef = 79
        Me.txtYearSaki.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(182, 12)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel2.TabIndex = 84
        Me.LmTitleLabel2.Text = "年"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "年"
        Me.LmTitleLabel2.WidthDef = 21
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(155, 13)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel3.TabIndex = 85
        Me.LmTitleLabel3.Text = "→"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "→"
        Me.LmTitleLabel3.WidthDef = 21
        '
        'lblMsg
        '
        Me.lblMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMsg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMsg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMsg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMsg.CountWrappedLine = False
        Me.lblMsg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMsg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMsg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMsg.HeightDef = 18
        Me.lblMsg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMsg.HissuLabelVisible = False
        Me.lblMsg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMsg.IsByteCheck = 0
        Me.lblMsg.IsCalendarCheck = False
        Me.lblMsg.IsDakutenCheck = False
        Me.lblMsg.IsEisuCheck = False
        Me.lblMsg.IsForbiddenWordsCheck = False
        Me.lblMsg.IsFullByteCheck = 0
        Me.lblMsg.IsHankakuCheck = False
        Me.lblMsg.IsHissuCheck = False
        Me.lblMsg.IsKanaCheck = False
        Me.lblMsg.IsMiddleSpace = False
        Me.lblMsg.IsNumericCheck = False
        Me.lblMsg.IsSujiCheck = False
        Me.lblMsg.IsZenkakuCheck = False
        Me.lblMsg.ItemName = ""
        Me.lblMsg.LineSpace = 0
        Me.lblMsg.Location = New System.Drawing.Point(390, 10)
        Me.lblMsg.MaxLength = 0
        Me.lblMsg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMsg.MaxLineCount = 0
        Me.lblMsg.Multiline = False
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.ReadOnly = True
        Me.lblMsg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMsg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMsg.Size = New System.Drawing.Size(477, 18)
        Me.lblMsg.TabIndex = 86
        Me.lblMsg.TabStop = False
        Me.lblMsg.TabStopSetting = False
        Me.lblMsg.TextValue = "土日以外を入力して下さい。"
        Me.lblMsg.UseSystemPasswordChar = False
        Me.lblMsg.WidthDef = 477
        Me.lblMsg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.txtRemark.CountWrappedLine = False
        Me.txtRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark.HeightDef = 18
        Me.txtRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.HissuLabelVisible = False
        Me.txtRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark.IsByteCheck = 100
        Me.txtRemark.IsCalendarCheck = False
        Me.txtRemark.IsDakutenCheck = False
        Me.txtRemark.IsEisuCheck = False
        Me.txtRemark.IsForbiddenWordsCheck = False
        Me.txtRemark.IsFullByteCheck = 0
        Me.txtRemark.IsHankakuCheck = False
        Me.txtRemark.IsHissuCheck = False
        Me.txtRemark.IsKanaCheck = False
        Me.txtRemark.IsMiddleSpace = False
        Me.txtRemark.IsNumericCheck = False
        Me.txtRemark.IsSujiCheck = False
        Me.txtRemark.IsZenkakuCheck = False
        Me.txtRemark.ItemName = ""
        Me.txtRemark.LineSpace = 0
        Me.txtRemark.Location = New System.Drawing.Point(85, 444)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = False
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(725, 18)
        Me.txtRemark.TabIndex = 324
        Me.txtRemark.TabStopSetting = True
        Me.txtRemark.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
            "---10---XX---10---X"
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 725
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel15
        '
        Me.LmTitleLabel15.AutoSize = True
        Me.LmTitleLabel15.AutoSizeDef = True
        Me.LmTitleLabel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel15.EnableStatus = False
        Me.LmTitleLabel15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.HeightDef = 13
        Me.LmTitleLabel15.Location = New System.Drawing.Point(48, 446)
        Me.LmTitleLabel15.Name = "LmTitleLabel15"
        Me.LmTitleLabel15.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel15.TabIndex = 323
        Me.LmTitleLabel15.Text = "備考"
        Me.LmTitleLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel15.TextValue = "備考"
        Me.LmTitleLabel15.WidthDef = 35
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCopy.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCopy.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnCopy.EnableStatus = True
        Me.btnCopy.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCopy.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCopy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCopy.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCopy.HeightDef = 22
        Me.btnCopy.Location = New System.Drawing.Point(284, 7)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(80, 22)
        Me.btnCopy.TabIndex = 325
        Me.btnCopy.TabStopSetting = True
        Me.btnCopy.Text = "日付複写"
        Me.btnCopy.TextValue = "日付複写"
        Me.btnCopy.UseVisualStyleBackColor = True
        Me.btnCopy.WidthDef = 80
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
        Me.lblSysDelFlg.Location = New System.Drawing.Point(1112, 561)
        Me.lblSysDelFlg.MaxLength = 0
        Me.lblSysDelFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysDelFlg.MaxLineCount = 0
        Me.lblSysDelFlg.Multiline = False
        Me.lblSysDelFlg.Name = "lblSysDelFlg"
        Me.lblSysDelFlg.ReadOnly = True
        Me.lblSysDelFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysDelFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysDelFlg.Size = New System.Drawing.Size(157, 18)
        Me.lblSysDelFlg.TabIndex = 635
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
        Me.lblUpdTime.Location = New System.Drawing.Point(1112, 540)
        Me.lblUpdTime.MaxLength = 0
        Me.lblUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdTime.MaxLineCount = 0
        Me.lblUpdTime.Multiline = False
        Me.lblUpdTime.Name = "lblUpdTime"
        Me.lblUpdTime.ReadOnly = True
        Me.lblUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdTime.TabIndex = 634
        Me.lblUpdTime.TabStop = False
        Me.lblUpdTime.TabStopSetting = False
        Me.lblUpdTime.TextValue = ""
        Me.lblUpdTime.UseSystemPasswordChar = False
        Me.lblUpdTime.Visible = False
        Me.lblUpdTime.WidthDef = 157
        Me.lblUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1119, 423)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 626
        Me.lblSituation.TabStop = False
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSizeDef = False
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 18
        Me.LmTitleLabel7.Location = New System.Drawing.Point(1062, 456)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel7.TabIndex = 625
        Me.LmTitleLabel7.Text = "作成者"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "作成者"
        Me.LmTitleLabel7.WidthDef = 49
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
        Me.lblCrtUser.Location = New System.Drawing.Point(1112, 456)
        Me.lblCrtUser.MaxLength = 0
        Me.lblCrtUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtUser.MaxLineCount = 0
        Me.lblCrtUser.Multiline = False
        Me.lblCrtUser.Name = "lblCrtUser"
        Me.lblCrtUser.ReadOnly = True
        Me.lblCrtUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtUser.TabIndex = 627
        Me.lblCrtUser.TabStop = False
        Me.lblCrtUser.TabStopSetting = False
        Me.lblCrtUser.TextValue = ""
        Me.lblCrtUser.UseSystemPasswordChar = False
        Me.lblCrtUser.WidthDef = 157
        Me.lblCrtUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSizeDef = False
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 18
        Me.LmTitleLabel8.Location = New System.Drawing.Point(1062, 478)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel8.TabIndex = 628
        Me.LmTitleLabel8.Text = "作成日"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "作成日"
        Me.LmTitleLabel8.WidthDef = 49
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
        Me.lblCrtDate.Location = New System.Drawing.Point(1112, 477)
        Me.lblCrtDate.MaxLength = 0
        Me.lblCrtDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCrtDate.MaxLineCount = 0
        Me.lblCrtDate.Multiline = False
        Me.lblCrtDate.Name = "lblCrtDate"
        Me.lblCrtDate.ReadOnly = True
        Me.lblCrtDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCrtDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCrtDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCrtDate.TabIndex = 629
        Me.lblCrtDate.TabStop = False
        Me.lblCrtDate.TabStopSetting = False
        Me.lblCrtDate.TextValue = ""
        Me.lblCrtDate.UseSystemPasswordChar = False
        Me.lblCrtDate.WidthDef = 157
        Me.lblCrtDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSizeDef = False
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 18
        Me.LmTitleLabel9.Location = New System.Drawing.Point(1062, 499)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel9.TabIndex = 630
        Me.LmTitleLabel9.Text = "更新者"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel9.TextValue = "更新者"
        Me.LmTitleLabel9.WidthDef = 49
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
        Me.lblUpdUser.Location = New System.Drawing.Point(1112, 498)
        Me.lblUpdUser.MaxLength = 0
        Me.lblUpdUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdUser.MaxLineCount = 0
        Me.lblUpdUser.Multiline = False
        Me.lblUpdUser.Name = "lblUpdUser"
        Me.lblUpdUser.ReadOnly = True
        Me.lblUpdUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdUser.TabIndex = 631
        Me.lblUpdUser.TabStop = False
        Me.lblUpdUser.TabStopSetting = False
        Me.lblUpdUser.TextValue = ""
        Me.lblUpdUser.UseSystemPasswordChar = False
        Me.lblUpdUser.WidthDef = 157
        Me.lblUpdUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSizeDef = False
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 18
        Me.LmTitleLabel10.Location = New System.Drawing.Point(1062, 521)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel10.TabIndex = 632
        Me.LmTitleLabel10.Text = "更新日"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel10.TextValue = "更新日"
        Me.LmTitleLabel10.WidthDef = 49
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
        Me.lblUpdDate.Location = New System.Drawing.Point(1112, 519)
        Me.lblUpdDate.MaxLength = 0
        Me.lblUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdDate.MaxLineCount = 0
        Me.lblUpdDate.Multiline = False
        Me.lblUpdDate.Name = "lblUpdDate"
        Me.lblUpdDate.ReadOnly = True
        Me.lblUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdDate.TabIndex = 633
        Me.lblUpdDate.TabStop = False
        Me.lblUpdDate.TabStopSetting = False
        Me.lblUpdDate.TextValue = ""
        Me.lblUpdDate.UseSystemPasswordChar = False
        Me.lblUpdDate.WidthDef = 157
        Me.lblUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdHol
        '
        Me.imdHol.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHol.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHol.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdHol.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdHol.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdHol.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHol.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHol.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHol.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdHol.HeightDef = 18
        Me.imdHol.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdHol.HissuLabelVisible = True
        Me.imdHol.Holiday = False
        Me.imdHol.IsAfterDateCheck = False
        Me.imdHol.IsBeforeDateCheck = False
        Me.imdHol.IsHissuCheck = True
        Me.imdHol.IsMinDateCheck = "1900/01/01"
        Me.imdHol.ItemName = ""
        Me.imdHol.Location = New System.Drawing.Point(85, 423)
        Me.imdHol.Name = "imdHol"
        Me.imdHol.Number = CType(10101000000, Long)
        Me.imdHol.ReadOnly = False
        Me.imdHol.Size = New System.Drawing.Size(118, 18)
        Me.imdHol.TabIndex = 636
        Me.imdHol.TabStopSetting = True
        Me.imdHol.TextValue = ""
        Me.imdHol.Value = New Date(CType(0, Long))
        Me.imdHol.WidthDef = 118
        '
        'lblMotoHol
        '
        Me.lblMotoHol.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoHol.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoHol.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMotoHol.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMotoHol.CountWrappedLine = False
        Me.lblMotoHol.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblMotoHol.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoHol.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMotoHol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoHol.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMotoHol.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblMotoHol.HeightDef = 18
        Me.lblMotoHol.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblMotoHol.HissuLabelVisible = False
        Me.lblMotoHol.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblMotoHol.IsByteCheck = 0
        Me.lblMotoHol.IsCalendarCheck = False
        Me.lblMotoHol.IsDakutenCheck = False
        Me.lblMotoHol.IsEisuCheck = False
        Me.lblMotoHol.IsForbiddenWordsCheck = False
        Me.lblMotoHol.IsFullByteCheck = 0
        Me.lblMotoHol.IsHankakuCheck = False
        Me.lblMotoHol.IsHissuCheck = False
        Me.lblMotoHol.IsKanaCheck = False
        Me.lblMotoHol.IsMiddleSpace = False
        Me.lblMotoHol.IsNumericCheck = False
        Me.lblMotoHol.IsSujiCheck = False
        Me.lblMotoHol.IsZenkakuCheck = False
        Me.lblMotoHol.ItemName = ""
        Me.lblMotoHol.LineSpace = 0
        Me.lblMotoHol.Location = New System.Drawing.Point(218, 423)
        Me.lblMotoHol.MaxLength = 0
        Me.lblMotoHol.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblMotoHol.MaxLineCount = 0
        Me.lblMotoHol.Multiline = False
        Me.lblMotoHol.Name = "lblMotoHol"
        Me.lblMotoHol.ReadOnly = True
        Me.lblMotoHol.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblMotoHol.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblMotoHol.Size = New System.Drawing.Size(123, 18)
        Me.lblMotoHol.TabIndex = 637
        Me.lblMotoHol.TabStop = False
        Me.lblMotoHol.TabStopSetting = False
        Me.lblMotoHol.TextValue = ""
        Me.lblMotoHol.UseSystemPasswordChar = False
        Me.lblMotoHol.Visible = False
        Me.lblMotoHol.WidthDef = 123
        Me.lblMotoHol.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMM110F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM110F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents LmTitleLabel43 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtYearMoto As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtYearSaki As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblMsg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtRemark As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel15 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnCopy As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblSysDelFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents LmTitleLabel7 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel8 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCrtDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents imdHol As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblMotoHol As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
