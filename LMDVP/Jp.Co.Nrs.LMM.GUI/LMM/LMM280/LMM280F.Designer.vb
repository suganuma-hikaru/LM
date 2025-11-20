<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM280F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM280F))
        Me.sprYokomochiHed = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.sprYokomochiDtl = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleYokomochiTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtYokomochiTariff = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleBiko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtBiko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleKeisanHoho = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleMeisaiBunkatu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleBr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numMinHosho = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleMinHosho = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblUpdateTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUpdateDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUpdateDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUpdateUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUpdateUser = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCreateDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCreateDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCreateUser = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCreateUser = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.cmbMeisaiBunkatu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbKeisanHoho = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprYokomochiHed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprYokomochiDtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbKeisanHoho)
        Me.pnlViewAria.Controls.Add(Me.cmbMeisaiBunkatu)
        Me.pnlViewAria.Controls.Add(Me.lblUpdateTime)
        Me.pnlViewAria.Controls.Add(Me.lblUpdateDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUpdateDate)
        Me.pnlViewAria.Controls.Add(Me.lblUpdateUser)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUpdateUser)
        Me.pnlViewAria.Controls.Add(Me.lblCreateDate)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCreateDate)
        Me.pnlViewAria.Controls.Add(Me.lblCreateUser)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCreateUser)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.cmbBr)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.lblTitleMinHosho)
        Me.pnlViewAria.Controls.Add(Me.numMinHosho)
        Me.pnlViewAria.Controls.Add(Me.lblTitleBr)
        Me.pnlViewAria.Controls.Add(Me.lblTitleMeisaiBunkatu)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.sprYokomochiDtl)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKeisanHoho)
        Me.pnlViewAria.Controls.Add(Me.txtBiko)
        Me.pnlViewAria.Controls.Add(Me.lblTitleBiko)
        Me.pnlViewAria.Controls.Add(Me.txtYokomochiTariff)
        Me.pnlViewAria.Controls.Add(Me.lblTitleYokomochiTariff)
        Me.pnlViewAria.Controls.Add(Me.sprYokomochiHed)
        '
        'sprYokomochiHed
        '
        Me.sprYokomochiHed.AccessibleDescription = ""
        Me.sprYokomochiHed.AllowUserZoom = False
        Me.sprYokomochiHed.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprYokomochiHed.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprYokomochiHed.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprYokomochiHed.CellClickEventArgs = Nothing
        Me.sprYokomochiHed.CheckToCheckBox = True
        Me.sprYokomochiHed.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprYokomochiHed.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprYokomochiHed.EditModeReplace = True
        Me.sprYokomochiHed.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprYokomochiHed.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprYokomochiHed.ForeColorDef = System.Drawing.Color.Empty
        Me.sprYokomochiHed.HeightDef = 230
        Me.sprYokomochiHed.Location = New System.Drawing.Point(16, 20)
        Me.sprYokomochiHed.Name = "sprYokomochiHed"
        Me.sprYokomochiHed.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprYokomochiHed.SetViewportTopRow(0, 0, 1)
        Me.sprYokomochiHed.SetActiveViewport(0, -1, 0)
        Me.sprYokomochiHed.Size = New System.Drawing.Size(1237, 230)
        Me.sprYokomochiHed.SortColumn = True
        Me.sprYokomochiHed.SpanColumnLock = True
        Me.sprYokomochiHed.SpreadDoubleClicked = False
        Me.sprYokomochiHed.TabIndex = 15
        Me.sprYokomochiHed.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprYokomochiHed.TextValue = Nothing
        Me.sprYokomochiHed.WidthDef = 1237
        '
        'sprYokomochiDtl
        '
        Me.sprYokomochiDtl.AccessibleDescription = ""
        Me.sprYokomochiDtl.AllowUserZoom = False
        Me.sprYokomochiDtl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprYokomochiDtl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprYokomochiDtl.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprYokomochiDtl.CellClickEventArgs = Nothing
        Me.sprYokomochiDtl.CheckToCheckBox = True
        Me.sprYokomochiDtl.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprYokomochiDtl.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprYokomochiDtl.EditModeReplace = True
        Me.sprYokomochiDtl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprYokomochiDtl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprYokomochiDtl.ForeColorDef = System.Drawing.Color.Empty
        Me.sprYokomochiDtl.HeightDef = 439
        Me.sprYokomochiDtl.Location = New System.Drawing.Point(17, 427)
        Me.sprYokomochiDtl.Name = "sprYokomochiDtl"
        Me.sprYokomochiDtl.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        Me.sprYokomochiDtl.Size = New System.Drawing.Size(1237, 439)
        Me.sprYokomochiDtl.SortColumn = True
        Me.sprYokomochiDtl.SpanColumnLock = True
        Me.sprYokomochiDtl.SpreadDoubleClicked = False
        Me.sprYokomochiDtl.TabIndex = 230
        Me.sprYokomochiDtl.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprYokomochiDtl.TextValue = Nothing
        Me.sprYokomochiDtl.WidthDef = 1237
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
        Me.btnRowAdd.Location = New System.Drawing.Point(62, 399)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 231
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 70
        '
        'lblTitleYokomochiTariff
        '
        Me.lblTitleYokomochiTariff.AutoSizeDef = False
        Me.lblTitleYokomochiTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYokomochiTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYokomochiTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYokomochiTariff.EnableStatus = False
        Me.lblTitleYokomochiTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYokomochiTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYokomochiTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYokomochiTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYokomochiTariff.HeightDef = 18
        Me.lblTitleYokomochiTariff.Location = New System.Drawing.Point(19, 294)
        Me.lblTitleYokomochiTariff.Name = "lblTitleYokomochiTariff"
        Me.lblTitleYokomochiTariff.Size = New System.Drawing.Size(142, 18)
        Me.lblTitleYokomochiTariff.TabIndex = 259
        Me.lblTitleYokomochiTariff.Text = "横持ちタリフコード"
        Me.lblTitleYokomochiTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYokomochiTariff.TextValue = "横持ちタリフコード"
        Me.lblTitleYokomochiTariff.WidthDef = 142
        '
        'txtYokomochiTariff
        '
        Me.txtYokomochiTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYokomochiTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtYokomochiTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtYokomochiTariff.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtYokomochiTariff.CountWrappedLine = False
        Me.txtYokomochiTariff.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtYokomochiTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYokomochiTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYokomochiTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYokomochiTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtYokomochiTariff.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtYokomochiTariff.HeightDef = 18
        Me.txtYokomochiTariff.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtYokomochiTariff.HissuLabelVisible = True
        Me.txtYokomochiTariff.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtYokomochiTariff.IsByteCheck = 10
        Me.txtYokomochiTariff.IsCalendarCheck = False
        Me.txtYokomochiTariff.IsDakutenCheck = False
        Me.txtYokomochiTariff.IsEisuCheck = False
        Me.txtYokomochiTariff.IsForbiddenWordsCheck = False
        Me.txtYokomochiTariff.IsFullByteCheck = 0
        Me.txtYokomochiTariff.IsHankakuCheck = False
        Me.txtYokomochiTariff.IsHissuCheck = True
        Me.txtYokomochiTariff.IsKanaCheck = False
        Me.txtYokomochiTariff.IsMiddleSpace = False
        Me.txtYokomochiTariff.IsNumericCheck = False
        Me.txtYokomochiTariff.IsSujiCheck = False
        Me.txtYokomochiTariff.IsZenkakuCheck = False
        Me.txtYokomochiTariff.ItemName = ""
        Me.txtYokomochiTariff.LineSpace = 0
        Me.txtYokomochiTariff.Location = New System.Drawing.Point(167, 294)
        Me.txtYokomochiTariff.MaxLength = 10
        Me.txtYokomochiTariff.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtYokomochiTariff.MaxLineCount = 0
        Me.txtYokomochiTariff.Multiline = False
        Me.txtYokomochiTariff.Name = "txtYokomochiTariff"
        Me.txtYokomochiTariff.ReadOnly = False
        Me.txtYokomochiTariff.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtYokomochiTariff.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtYokomochiTariff.Size = New System.Drawing.Size(103, 18)
        Me.txtYokomochiTariff.TabIndex = 260
        Me.txtYokomochiTariff.TabStopSetting = True
        Me.txtYokomochiTariff.Tag = ""
        Me.txtYokomochiTariff.TextValue = "X---10---X"
        Me.txtYokomochiTariff.UseSystemPasswordChar = False
        Me.txtYokomochiTariff.WidthDef = 103
        Me.txtYokomochiTariff.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleBiko
        '
        Me.lblTitleBiko.AutoSizeDef = False
        Me.lblTitleBiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBiko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBiko.EnableStatus = False
        Me.lblTitleBiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBiko.HeightDef = 18
        Me.lblTitleBiko.Location = New System.Drawing.Point(48, 316)
        Me.lblTitleBiko.Name = "lblTitleBiko"
        Me.lblTitleBiko.Size = New System.Drawing.Size(113, 18)
        Me.lblTitleBiko.TabIndex = 261
        Me.lblTitleBiko.Text = "備考"
        Me.lblTitleBiko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBiko.TextValue = "備考"
        Me.lblTitleBiko.WidthDef = 113
        '
        'txtBiko
        '
        Me.txtBiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBiko.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBiko.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBiko.CountWrappedLine = False
        Me.txtBiko.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtBiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtBiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBiko.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtBiko.HeightDef = 18
        Me.txtBiko.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtBiko.HissuLabelVisible = False
        Me.txtBiko.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtBiko.IsByteCheck = 100
        Me.txtBiko.IsCalendarCheck = False
        Me.txtBiko.IsDakutenCheck = False
        Me.txtBiko.IsEisuCheck = False
        Me.txtBiko.IsForbiddenWordsCheck = False
        Me.txtBiko.IsFullByteCheck = 0
        Me.txtBiko.IsHankakuCheck = False
        Me.txtBiko.IsHissuCheck = False
        Me.txtBiko.IsKanaCheck = False
        Me.txtBiko.IsMiddleSpace = False
        Me.txtBiko.IsNumericCheck = False
        Me.txtBiko.IsSujiCheck = False
        Me.txtBiko.IsZenkakuCheck = False
        Me.txtBiko.ItemName = ""
        Me.txtBiko.LineSpace = 0
        Me.txtBiko.Location = New System.Drawing.Point(167, 316)
        Me.txtBiko.MaxLength = 100
        Me.txtBiko.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtBiko.MaxLineCount = 0
        Me.txtBiko.Multiline = False
        Me.txtBiko.Name = "txtBiko"
        Me.txtBiko.ReadOnly = False
        Me.txtBiko.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtBiko.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBiko.Size = New System.Drawing.Size(720, 18)
        Me.txtBiko.TabIndex = 262
        Me.txtBiko.TabStopSetting = True
        Me.txtBiko.Tag = ""
        Me.txtBiko.TextValue = "X---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX---10---XX" & _
            "---10---X"
        Me.txtBiko.UseSystemPasswordChar = False
        Me.txtBiko.WidthDef = 720
        Me.txtBiko.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleKeisanHoho
        '
        Me.lblTitleKeisanHoho.AutoSizeDef = False
        Me.lblTitleKeisanHoho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeisanHoho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeisanHoho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKeisanHoho.EnableStatus = False
        Me.lblTitleKeisanHoho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeisanHoho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeisanHoho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeisanHoho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeisanHoho.HeightDef = 18
        Me.lblTitleKeisanHoho.Location = New System.Drawing.Point(48, 338)
        Me.lblTitleKeisanHoho.Name = "lblTitleKeisanHoho"
        Me.lblTitleKeisanHoho.Size = New System.Drawing.Size(113, 18)
        Me.lblTitleKeisanHoho.TabIndex = 263
        Me.lblTitleKeisanHoho.Text = "計算方法"
        Me.lblTitleKeisanHoho.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKeisanHoho.TextValue = "計算方法"
        Me.lblTitleKeisanHoho.WidthDef = 113
        '
        'lblTitleMeisaiBunkatu
        '
        Me.lblTitleMeisaiBunkatu.AutoSizeDef = False
        Me.lblTitleMeisaiBunkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMeisaiBunkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMeisaiBunkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMeisaiBunkatu.EnableStatus = False
        Me.lblTitleMeisaiBunkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMeisaiBunkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMeisaiBunkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMeisaiBunkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMeisaiBunkatu.HeightDef = 18
        Me.lblTitleMeisaiBunkatu.Location = New System.Drawing.Point(335, 337)
        Me.lblTitleMeisaiBunkatu.Name = "lblTitleMeisaiBunkatu"
        Me.lblTitleMeisaiBunkatu.Size = New System.Drawing.Size(133, 18)
        Me.lblTitleMeisaiBunkatu.TabIndex = 270
        Me.lblTitleMeisaiBunkatu.Text = "明細分割"
        Me.lblTitleMeisaiBunkatu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMeisaiBunkatu.TextValue = "明細分割"
        Me.lblTitleMeisaiBunkatu.WidthDef = 133
        '
        'lblTitleBr
        '
        Me.lblTitleBr.AutoSizeDef = False
        Me.lblTitleBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBr.EnableStatus = False
        Me.lblTitleBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBr.HeightDef = 18
        Me.lblTitleBr.Location = New System.Drawing.Point(41, 272)
        Me.lblTitleBr.Name = "lblTitleBr"
        Me.lblTitleBr.Size = New System.Drawing.Size(120, 18)
        Me.lblTitleBr.TabIndex = 461
        Me.lblTitleBr.Text = "営業所"
        Me.lblTitleBr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBr.TextValue = "営業所"
        Me.lblTitleBr.WidthDef = 120
        '
        'numMinHosho
        '
        Me.numMinHosho.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numMinHosho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numMinHosho.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numMinHosho.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numMinHosho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numMinHosho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numMinHosho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numMinHosho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numMinHosho.HeightDef = 18
        Me.numMinHosho.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numMinHosho.HissuLabelVisible = False
        Me.numMinHosho.IsHissuCheck = False
        Me.numMinHosho.IsRangeCheck = False
        Me.numMinHosho.ItemName = ""
        Me.numMinHosho.Location = New System.Drawing.Point(167, 359)
        Me.numMinHosho.Name = "numMinHosho"
        Me.numMinHosho.ReadOnly = False
        Me.numMinHosho.Size = New System.Drawing.Size(130, 18)
        Me.numMinHosho.TabIndex = 495
        Me.numMinHosho.TabStopSetting = True
        Me.numMinHosho.Tag = ""
        Me.numMinHosho.TextValue = "0"
        Me.numMinHosho.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numMinHosho.WidthDef = 130
        '
        'lblTitleMinHosho
        '
        Me.lblTitleMinHosho.AutoSizeDef = False
        Me.lblTitleMinHosho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMinHosho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMinHosho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMinHosho.EnableStatus = False
        Me.lblTitleMinHosho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMinHosho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMinHosho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMinHosho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMinHosho.HeightDef = 18
        Me.lblTitleMinHosho.Location = New System.Drawing.Point(48, 360)
        Me.lblTitleMinHosho.Name = "lblTitleMinHosho"
        Me.lblTitleMinHosho.Size = New System.Drawing.Size(113, 18)
        Me.lblTitleMinHosho.TabIndex = 496
        Me.lblTitleMinHosho.Text = "最低保証金額"
        Me.lblTitleMinHosho.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMinHosho.TextValue = "最低保証金額"
        Me.lblTitleMinHosho.WidthDef = 113
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
        Me.btnRowDel.Location = New System.Drawing.Point(138, 399)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 497
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.cmbBr.Location = New System.Drawing.Point(167, 272)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = True
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 555
        Me.cmbBr.TabStop = False
        Me.cmbBr.TabStopSetting = False
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
        '
        'lblUpdateTime
        '
        Me.lblUpdateTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdateTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdateTime.CountWrappedLine = False
        Me.lblUpdateTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdateTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdateTime.HeightDef = 18
        Me.lblUpdateTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateTime.HissuLabelVisible = False
        Me.lblUpdateTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdateTime.IsByteCheck = 0
        Me.lblUpdateTime.IsCalendarCheck = False
        Me.lblUpdateTime.IsDakutenCheck = False
        Me.lblUpdateTime.IsEisuCheck = False
        Me.lblUpdateTime.IsForbiddenWordsCheck = False
        Me.lblUpdateTime.IsFullByteCheck = 0
        Me.lblUpdateTime.IsHankakuCheck = False
        Me.lblUpdateTime.IsHissuCheck = False
        Me.lblUpdateTime.IsKanaCheck = False
        Me.lblUpdateTime.IsMiddleSpace = False
        Me.lblUpdateTime.IsNumericCheck = False
        Me.lblUpdateTime.IsSujiCheck = False
        Me.lblUpdateTime.IsZenkakuCheck = False
        Me.lblUpdateTime.ItemName = ""
        Me.lblUpdateTime.LineSpace = 0
        Me.lblUpdateTime.Location = New System.Drawing.Point(1113, 401)
        Me.lblUpdateTime.MaxLength = 0
        Me.lblUpdateTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdateTime.MaxLineCount = 0
        Me.lblUpdateTime.Multiline = False
        Me.lblUpdateTime.Name = "lblUpdateTime"
        Me.lblUpdateTime.ReadOnly = True
        Me.lblUpdateTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdateTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdateTime.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdateTime.TabIndex = 573
        Me.lblUpdateTime.TabStop = False
        Me.lblUpdateTime.TabStopSetting = False
        Me.lblUpdateTime.TextValue = ""
        Me.lblUpdateTime.UseSystemPasswordChar = False
        Me.lblUpdateTime.Visible = False
        Me.lblUpdateTime.WidthDef = 157
        Me.lblUpdateTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUpdateDate
        '
        Me.lblUpdateDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdateDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdateDate.CountWrappedLine = False
        Me.lblUpdateDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdateDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdateDate.HeightDef = 18
        Me.lblUpdateDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateDate.HissuLabelVisible = False
        Me.lblUpdateDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdateDate.IsByteCheck = 0
        Me.lblUpdateDate.IsCalendarCheck = False
        Me.lblUpdateDate.IsDakutenCheck = False
        Me.lblUpdateDate.IsEisuCheck = False
        Me.lblUpdateDate.IsForbiddenWordsCheck = False
        Me.lblUpdateDate.IsFullByteCheck = 0
        Me.lblUpdateDate.IsHankakuCheck = False
        Me.lblUpdateDate.IsHissuCheck = False
        Me.lblUpdateDate.IsKanaCheck = False
        Me.lblUpdateDate.IsMiddleSpace = False
        Me.lblUpdateDate.IsNumericCheck = False
        Me.lblUpdateDate.IsSujiCheck = False
        Me.lblUpdateDate.IsZenkakuCheck = False
        Me.lblUpdateDate.ItemName = ""
        Me.lblUpdateDate.LineSpace = 0
        Me.lblUpdateDate.Location = New System.Drawing.Point(1112, 377)
        Me.lblUpdateDate.MaxLength = 0
        Me.lblUpdateDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdateDate.MaxLineCount = 0
        Me.lblUpdateDate.Multiline = False
        Me.lblUpdateDate.Name = "lblUpdateDate"
        Me.lblUpdateDate.ReadOnly = True
        Me.lblUpdateDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdateDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdateDate.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdateDate.TabIndex = 572
        Me.lblUpdateDate.TabStop = False
        Me.lblUpdateDate.TabStopSetting = False
        Me.lblUpdateDate.TextValue = ""
        Me.lblUpdateDate.UseSystemPasswordChar = False
        Me.lblUpdateDate.WidthDef = 157
        Me.lblUpdateDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUpdateDate
        '
        Me.lblTitleUpdateDate.AutoSizeDef = False
        Me.lblTitleUpdateDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdateDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdateDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUpdateDate.EnableStatus = False
        Me.lblTitleUpdateDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdateDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdateDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdateDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdateDate.HeightDef = 18
        Me.lblTitleUpdateDate.Location = New System.Drawing.Point(1057, 377)
        Me.lblTitleUpdateDate.Name = "lblTitleUpdateDate"
        Me.lblTitleUpdateDate.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleUpdateDate.TabIndex = 571
        Me.lblTitleUpdateDate.Text = "更新日"
        Me.lblTitleUpdateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUpdateDate.TextValue = "更新日"
        Me.lblTitleUpdateDate.WidthDef = 49
        '
        'lblUpdateUser
        '
        Me.lblUpdateUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUpdateUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUpdateUser.CountWrappedLine = False
        Me.lblUpdateUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUpdateUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdateUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUpdateUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUpdateUser.HeightDef = 18
        Me.lblUpdateUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUpdateUser.HissuLabelVisible = False
        Me.lblUpdateUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUpdateUser.IsByteCheck = 0
        Me.lblUpdateUser.IsCalendarCheck = False
        Me.lblUpdateUser.IsDakutenCheck = False
        Me.lblUpdateUser.IsEisuCheck = False
        Me.lblUpdateUser.IsForbiddenWordsCheck = False
        Me.lblUpdateUser.IsFullByteCheck = 0
        Me.lblUpdateUser.IsHankakuCheck = False
        Me.lblUpdateUser.IsHissuCheck = False
        Me.lblUpdateUser.IsKanaCheck = False
        Me.lblUpdateUser.IsMiddleSpace = False
        Me.lblUpdateUser.IsNumericCheck = False
        Me.lblUpdateUser.IsSujiCheck = False
        Me.lblUpdateUser.IsZenkakuCheck = False
        Me.lblUpdateUser.ItemName = ""
        Me.lblUpdateUser.LineSpace = 0
        Me.lblUpdateUser.Location = New System.Drawing.Point(1112, 355)
        Me.lblUpdateUser.MaxLength = 0
        Me.lblUpdateUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUpdateUser.MaxLineCount = 0
        Me.lblUpdateUser.Multiline = False
        Me.lblUpdateUser.Name = "lblUpdateUser"
        Me.lblUpdateUser.ReadOnly = True
        Me.lblUpdateUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUpdateUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUpdateUser.Size = New System.Drawing.Size(157, 18)
        Me.lblUpdateUser.TabIndex = 570
        Me.lblUpdateUser.TabStop = False
        Me.lblUpdateUser.TabStopSetting = False
        Me.lblUpdateUser.TextValue = ""
        Me.lblUpdateUser.UseSystemPasswordChar = False
        Me.lblUpdateUser.WidthDef = 157
        Me.lblUpdateUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUpdateUser
        '
        Me.lblTitleUpdateUser.AutoSizeDef = False
        Me.lblTitleUpdateUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdateUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUpdateUser.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUpdateUser.EnableStatus = False
        Me.lblTitleUpdateUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdateUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUpdateUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdateUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUpdateUser.HeightDef = 18
        Me.lblTitleUpdateUser.Location = New System.Drawing.Point(1057, 355)
        Me.lblTitleUpdateUser.Name = "lblTitleUpdateUser"
        Me.lblTitleUpdateUser.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleUpdateUser.TabIndex = 569
        Me.lblTitleUpdateUser.Text = "更新者"
        Me.lblTitleUpdateUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUpdateUser.TextValue = "更新者"
        Me.lblTitleUpdateUser.WidthDef = 49
        '
        'lblCreateDate
        '
        Me.lblCreateDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCreateDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCreateDate.CountWrappedLine = False
        Me.lblCreateDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCreateDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCreateDate.HeightDef = 18
        Me.lblCreateDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateDate.HissuLabelVisible = False
        Me.lblCreateDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCreateDate.IsByteCheck = 0
        Me.lblCreateDate.IsCalendarCheck = False
        Me.lblCreateDate.IsDakutenCheck = False
        Me.lblCreateDate.IsEisuCheck = False
        Me.lblCreateDate.IsForbiddenWordsCheck = False
        Me.lblCreateDate.IsFullByteCheck = 0
        Me.lblCreateDate.IsHankakuCheck = False
        Me.lblCreateDate.IsHissuCheck = False
        Me.lblCreateDate.IsKanaCheck = False
        Me.lblCreateDate.IsMiddleSpace = False
        Me.lblCreateDate.IsNumericCheck = False
        Me.lblCreateDate.IsSujiCheck = False
        Me.lblCreateDate.IsZenkakuCheck = False
        Me.lblCreateDate.ItemName = ""
        Me.lblCreateDate.LineSpace = 0
        Me.lblCreateDate.Location = New System.Drawing.Point(1112, 333)
        Me.lblCreateDate.MaxLength = 0
        Me.lblCreateDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCreateDate.MaxLineCount = 0
        Me.lblCreateDate.Multiline = False
        Me.lblCreateDate.Name = "lblCreateDate"
        Me.lblCreateDate.ReadOnly = True
        Me.lblCreateDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCreateDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCreateDate.Size = New System.Drawing.Size(157, 18)
        Me.lblCreateDate.TabIndex = 568
        Me.lblCreateDate.TabStop = False
        Me.lblCreateDate.TabStopSetting = False
        Me.lblCreateDate.TextValue = ""
        Me.lblCreateDate.UseSystemPasswordChar = False
        Me.lblCreateDate.WidthDef = 157
        Me.lblCreateDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCreateDate
        '
        Me.lblTitleCreateDate.AutoSizeDef = False
        Me.lblTitleCreateDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCreateDate.EnableStatus = False
        Me.lblTitleCreateDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateDate.HeightDef = 18
        Me.lblTitleCreateDate.Location = New System.Drawing.Point(1057, 333)
        Me.lblTitleCreateDate.Name = "lblTitleCreateDate"
        Me.lblTitleCreateDate.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleCreateDate.TabIndex = 567
        Me.lblTitleCreateDate.Text = "作成日"
        Me.lblTitleCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCreateDate.TextValue = "作成日"
        Me.lblTitleCreateDate.WidthDef = 49
        '
        'lblCreateUser
        '
        Me.lblCreateUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateUser.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCreateUser.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCreateUser.CountWrappedLine = False
        Me.lblCreateUser.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCreateUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCreateUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCreateUser.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCreateUser.HeightDef = 18
        Me.lblCreateUser.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCreateUser.HissuLabelVisible = False
        Me.lblCreateUser.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCreateUser.IsByteCheck = 0
        Me.lblCreateUser.IsCalendarCheck = False
        Me.lblCreateUser.IsDakutenCheck = False
        Me.lblCreateUser.IsEisuCheck = False
        Me.lblCreateUser.IsForbiddenWordsCheck = False
        Me.lblCreateUser.IsFullByteCheck = 0
        Me.lblCreateUser.IsHankakuCheck = False
        Me.lblCreateUser.IsHissuCheck = False
        Me.lblCreateUser.IsKanaCheck = False
        Me.lblCreateUser.IsMiddleSpace = False
        Me.lblCreateUser.IsNumericCheck = False
        Me.lblCreateUser.IsSujiCheck = False
        Me.lblCreateUser.IsZenkakuCheck = False
        Me.lblCreateUser.ItemName = ""
        Me.lblCreateUser.LineSpace = 0
        Me.lblCreateUser.Location = New System.Drawing.Point(1112, 311)
        Me.lblCreateUser.MaxLength = 0
        Me.lblCreateUser.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCreateUser.MaxLineCount = 0
        Me.lblCreateUser.Multiline = False
        Me.lblCreateUser.Name = "lblCreateUser"
        Me.lblCreateUser.ReadOnly = True
        Me.lblCreateUser.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCreateUser.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCreateUser.Size = New System.Drawing.Size(157, 18)
        Me.lblCreateUser.TabIndex = 566
        Me.lblCreateUser.TabStop = False
        Me.lblCreateUser.TabStopSetting = False
        Me.lblCreateUser.TextValue = ""
        Me.lblCreateUser.UseSystemPasswordChar = False
        Me.lblCreateUser.WidthDef = 157
        Me.lblCreateUser.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCreateUser
        '
        Me.lblTitleCreateUser.AutoSizeDef = False
        Me.lblTitleCreateUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateUser.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCreateUser.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCreateUser.EnableStatus = False
        Me.lblTitleCreateUser.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateUser.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCreateUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateUser.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCreateUser.HeightDef = 18
        Me.lblTitleCreateUser.Location = New System.Drawing.Point(1057, 311)
        Me.lblTitleCreateUser.Name = "lblTitleCreateUser"
        Me.lblTitleCreateUser.Size = New System.Drawing.Size(49, 18)
        Me.lblTitleCreateUser.TabIndex = 565
        Me.lblTitleCreateUser.Text = "作成者"
        Me.lblTitleCreateUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCreateUser.TextValue = "作成者"
        Me.lblTitleCreateUser.WidthDef = 49
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1119, 272)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 564
        Me.lblSituation.TabStop = False
        '
        'cmbMeisaiBunkatu
        '
        Me.cmbMeisaiBunkatu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMeisaiBunkatu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbMeisaiBunkatu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbMeisaiBunkatu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbMeisaiBunkatu.DataCode = "U009"
        Me.cmbMeisaiBunkatu.DataSource = Nothing
        Me.cmbMeisaiBunkatu.DisplayMember = Nothing
        Me.cmbMeisaiBunkatu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMeisaiBunkatu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMeisaiBunkatu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMeisaiBunkatu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbMeisaiBunkatu.HeightDef = 18
        Me.cmbMeisaiBunkatu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbMeisaiBunkatu.HissuLabelVisible = True
        Me.cmbMeisaiBunkatu.InsertWildCard = True
        Me.cmbMeisaiBunkatu.IsForbiddenWordsCheck = False
        Me.cmbMeisaiBunkatu.IsHissuCheck = True
        Me.cmbMeisaiBunkatu.ItemName = ""
        Me.cmbMeisaiBunkatu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbMeisaiBunkatu.Location = New System.Drawing.Point(473, 338)
        Me.cmbMeisaiBunkatu.Name = "cmbMeisaiBunkatu"
        Me.cmbMeisaiBunkatu.ReadOnly = False
        Me.cmbMeisaiBunkatu.SelectedIndex = -1
        Me.cmbMeisaiBunkatu.SelectedItem = Nothing
        Me.cmbMeisaiBunkatu.SelectedText = ""
        Me.cmbMeisaiBunkatu.SelectedValue = ""
        Me.cmbMeisaiBunkatu.Size = New System.Drawing.Size(100, 18)
        Me.cmbMeisaiBunkatu.TabIndex = 574
        Me.cmbMeisaiBunkatu.TabStopSetting = True
        Me.cmbMeisaiBunkatu.TextValue = ""
        Me.cmbMeisaiBunkatu.Value1 = Nothing
        Me.cmbMeisaiBunkatu.Value2 = Nothing
        Me.cmbMeisaiBunkatu.Value3 = Nothing
        Me.cmbMeisaiBunkatu.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbMeisaiBunkatu.ValueMember = Nothing
        Me.cmbMeisaiBunkatu.WidthDef = 100
        '
        'cmbKeisanHoho
        '
        Me.cmbKeisanHoho.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKeisanHoho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKeisanHoho.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKeisanHoho.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKeisanHoho.DataCode = "K012"
        Me.cmbKeisanHoho.DataSource = Nothing
        Me.cmbKeisanHoho.DisplayMember = Nothing
        Me.cmbKeisanHoho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKeisanHoho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKeisanHoho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKeisanHoho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKeisanHoho.HeightDef = 18
        Me.cmbKeisanHoho.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKeisanHoho.HissuLabelVisible = True
        Me.cmbKeisanHoho.InsertWildCard = True
        Me.cmbKeisanHoho.IsForbiddenWordsCheck = False
        Me.cmbKeisanHoho.IsHissuCheck = True
        Me.cmbKeisanHoho.ItemName = ""
        Me.cmbKeisanHoho.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbKeisanHoho.Location = New System.Drawing.Point(167, 338)
        Me.cmbKeisanHoho.Name = "cmbKeisanHoho"
        Me.cmbKeisanHoho.ReadOnly = False
        Me.cmbKeisanHoho.SelectedIndex = -1
        Me.cmbKeisanHoho.SelectedItem = Nothing
        Me.cmbKeisanHoho.SelectedText = ""
        Me.cmbKeisanHoho.SelectedValue = ""
        Me.cmbKeisanHoho.Size = New System.Drawing.Size(130, 18)
        Me.cmbKeisanHoho.TabIndex = 575
        Me.cmbKeisanHoho.TabStopSetting = True
        Me.cmbKeisanHoho.TextValue = ""
        Me.cmbKeisanHoho.Value1 = Nothing
        Me.cmbKeisanHoho.Value2 = Nothing
        Me.cmbKeisanHoho.Value3 = Nothing
        Me.cmbKeisanHoho.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbKeisanHoho.ValueMember = Nothing
        Me.cmbKeisanHoho.WidthDef = 130
        '
        'LMM280F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM280F"
        Me.Text = "【LMM280】 横持ちタリフマスタメンテ"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprYokomochiHed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprYokomochiDtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprYokomochiHed As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents sprYokomochiDtl As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleMeisaiBunkatu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKeisanHoho As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtBiko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleBiko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtYokomochiTariff As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleYokomochiTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleBr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleMinHosho As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numMinHosho As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblUpdateTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUpdateDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUpdateDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUpdateUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUpdateUser As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCreateDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCreateDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCreateUser As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCreateUser As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents cmbKeisanHoho As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbMeisaiBunkatu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun

End Class
