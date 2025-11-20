<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI020F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormPopL8B

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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.pnlSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblLoadFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbZaiRirekiDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.lblCustNmS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleZaiRirekiDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleHokokuDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdHokokuDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitlePrint = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPlantCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.lblPlantNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlCust = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblCustDtl = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.pnlViewAria.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlCust.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.pnlCust)
        Me.pnlViewAria.Controls.Add(Me.lblPlantNm)
        Me.pnlViewAria.Controls.Add(Me.cmbPlantCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePrint)
        Me.pnlViewAria.Controls.Add(Me.pnlSearch)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "マスタ参照"
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F7ButtonName = "作　成"
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSearch.Controls.Add(Me.lblLoadFlg)
        Me.pnlSearch.Controls.Add(Me.cmbZaiRirekiDate)
        Me.pnlSearch.Controls.Add(Me.lblCustNmS)
        Me.pnlSearch.Controls.Add(Me.txtCustCdS)
        Me.pnlSearch.Controls.Add(Me.lblTitleZaiRirekiDate)
        Me.pnlSearch.Controls.Add(Me.lblTitleHokokuDate)
        Me.pnlSearch.Controls.Add(Me.imdHokokuDate)
        Me.pnlSearch.Controls.Add(Me.lblCustNmM)
        Me.pnlSearch.Controls.Add(Me.lblCustNmL)
        Me.pnlSearch.Controls.Add(Me.lblTitleCust)
        Me.pnlSearch.Controls.Add(Me.txtCustCdM)
        Me.pnlSearch.Controls.Add(Me.txtCustCdL)
        Me.pnlSearch.Controls.Add(Me.lblTitleEigyo)
        Me.pnlSearch.Controls.Add(Me.cmbEigyo)
        Me.pnlSearch.EnableStatus = False
        Me.pnlSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSearch.HeightDef = 166
        Me.pnlSearch.Location = New System.Drawing.Point(27, 51)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(738, 166)
        Me.pnlSearch.TabIndex = 238
        Me.pnlSearch.TabStop = False
        Me.pnlSearch.Text = "出力条件"
        Me.pnlSearch.TextValue = "出力条件"
        Me.pnlSearch.WidthDef = 738
        '
        'lblLoadFlg
        '
        Me.lblLoadFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoadFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoadFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLoadFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLoadFlg.CountWrappedLine = False
        Me.lblLoadFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblLoadFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLoadFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLoadFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoadFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoadFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblLoadFlg.HeightDef = 18
        Me.lblLoadFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoadFlg.HissuLabelVisible = False
        Me.lblLoadFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblLoadFlg.IsByteCheck = 0
        Me.lblLoadFlg.IsCalendarCheck = False
        Me.lblLoadFlg.IsDakutenCheck = False
        Me.lblLoadFlg.IsEisuCheck = False
        Me.lblLoadFlg.IsForbiddenWordsCheck = False
        Me.lblLoadFlg.IsFullByteCheck = 0
        Me.lblLoadFlg.IsHankakuCheck = False
        Me.lblLoadFlg.IsHissuCheck = False
        Me.lblLoadFlg.IsKanaCheck = False
        Me.lblLoadFlg.IsMiddleSpace = False
        Me.lblLoadFlg.IsNumericCheck = False
        Me.lblLoadFlg.IsSujiCheck = False
        Me.lblLoadFlg.IsZenkakuCheck = False
        Me.lblLoadFlg.ItemName = ""
        Me.lblLoadFlg.LineSpace = 0
        Me.lblLoadFlg.Location = New System.Drawing.Point(592, 134)
        Me.lblLoadFlg.MaxLength = 0
        Me.lblLoadFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblLoadFlg.MaxLineCount = 0
        Me.lblLoadFlg.Multiline = False
        Me.lblLoadFlg.Name = "lblLoadFlg"
        Me.lblLoadFlg.ReadOnly = True
        Me.lblLoadFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblLoadFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblLoadFlg.Size = New System.Drawing.Size(125, 18)
        Me.lblLoadFlg.TabIndex = 248
        Me.lblLoadFlg.TabStop = False
        Me.lblLoadFlg.TabStopSetting = False
        Me.lblLoadFlg.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblLoadFlg.UseSystemPasswordChar = False
        Me.lblLoadFlg.Visible = False
        Me.lblLoadFlg.WidthDef = 125
        Me.lblLoadFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbZaiRirekiDate
        '
        Me.cmbZaiRirekiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZaiRirekiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbZaiRirekiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbZaiRirekiDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbZaiRirekiDate.DataSource = Nothing
        Me.cmbZaiRirekiDate.DisplayMember = Nothing
        Me.cmbZaiRirekiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZaiRirekiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbZaiRirekiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZaiRirekiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbZaiRirekiDate.HeightDef = 18
        Me.cmbZaiRirekiDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbZaiRirekiDate.HissuLabelVisible = True
        Me.cmbZaiRirekiDate.InsertWildCard = True
        Me.cmbZaiRirekiDate.IsForbiddenWordsCheck = False
        Me.cmbZaiRirekiDate.IsHissuCheck = True
        Me.cmbZaiRirekiDate.ItemName = ""
        Me.cmbZaiRirekiDate.Location = New System.Drawing.Point(382, 115)
        Me.cmbZaiRirekiDate.Name = "cmbZaiRirekiDate"
        Me.cmbZaiRirekiDate.ReadOnly = False
        Me.cmbZaiRirekiDate.SelectedIndex = -1
        Me.cmbZaiRirekiDate.SelectedItem = Nothing
        Me.cmbZaiRirekiDate.SelectedText = ""
        Me.cmbZaiRirekiDate.SelectedValue = ""
        Me.cmbZaiRirekiDate.Size = New System.Drawing.Size(117, 18)
        Me.cmbZaiRirekiDate.TabIndex = 245
        Me.cmbZaiRirekiDate.TabStopSetting = True
        Me.cmbZaiRirekiDate.TextValue = ""
        Me.cmbZaiRirekiDate.ValueMember = Nothing
        Me.cmbZaiRirekiDate.WidthDef = 117
        '
        'lblCustNmS
        '
        Me.lblCustNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNmS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNmS.CountWrappedLine = False
        Me.lblCustNmS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNmS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNmS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNmS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNmS.HeightDef = 18
        Me.lblCustNmS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNmS.HissuLabelVisible = False
        Me.lblCustNmS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmS.IsByteCheck = 0
        Me.lblCustNmS.IsCalendarCheck = False
        Me.lblCustNmS.IsDakutenCheck = False
        Me.lblCustNmS.IsEisuCheck = False
        Me.lblCustNmS.IsForbiddenWordsCheck = False
        Me.lblCustNmS.IsFullByteCheck = 0
        Me.lblCustNmS.IsHankakuCheck = False
        Me.lblCustNmS.IsHissuCheck = False
        Me.lblCustNmS.IsKanaCheck = False
        Me.lblCustNmS.IsMiddleSpace = False
        Me.lblCustNmS.IsNumericCheck = False
        Me.lblCustNmS.IsSujiCheck = False
        Me.lblCustNmS.IsZenkakuCheck = False
        Me.lblCustNmS.ItemName = ""
        Me.lblCustNmS.LineSpace = 0
        Me.lblCustNmS.Location = New System.Drawing.Point(180, 88)
        Me.lblCustNmS.MaxLength = 0
        Me.lblCustNmS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmS.MaxLineCount = 0
        Me.lblCustNmS.Multiline = False
        Me.lblCustNmS.Name = "lblCustNmS"
        Me.lblCustNmS.ReadOnly = True
        Me.lblCustNmS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmS.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmS.TabIndex = 231
        Me.lblCustNmS.TabStop = False
        Me.lblCustNmS.TabStopSetting = False
        Me.lblCustNmS.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmS.UseSystemPasswordChar = False
        Me.lblCustNmS.WidthDef = 473
        Me.lblCustNmS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdS
        '
        Me.txtCustCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdS.CountWrappedLine = False
        Me.txtCustCdS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdS.HeightDef = 18
        Me.txtCustCdS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdS.HissuLabelVisible = False
        Me.txtCustCdS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdS.IsByteCheck = 2
        Me.txtCustCdS.IsCalendarCheck = False
        Me.txtCustCdS.IsDakutenCheck = False
        Me.txtCustCdS.IsEisuCheck = False
        Me.txtCustCdS.IsForbiddenWordsCheck = False
        Me.txtCustCdS.IsFullByteCheck = 0
        Me.txtCustCdS.IsHankakuCheck = False
        Me.txtCustCdS.IsHissuCheck = False
        Me.txtCustCdS.IsKanaCheck = False
        Me.txtCustCdS.IsMiddleSpace = False
        Me.txtCustCdS.IsNumericCheck = False
        Me.txtCustCdS.IsSujiCheck = False
        Me.txtCustCdS.IsZenkakuCheck = False
        Me.txtCustCdS.ItemName = ""
        Me.txtCustCdS.LineSpace = 0
        Me.txtCustCdS.Location = New System.Drawing.Point(144, 88)
        Me.txtCustCdS.MaxLength = 2
        Me.txtCustCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdS.MaxLineCount = 0
        Me.txtCustCdS.Multiline = False
        Me.txtCustCdS.Name = "txtCustCdS"
        Me.txtCustCdS.ReadOnly = False
        Me.txtCustCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdS.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdS.TabIndex = 230
        Me.txtCustCdS.TabStopSetting = True
        Me.txtCustCdS.TextValue = ""
        Me.txtCustCdS.UseSystemPasswordChar = False
        Me.txtCustCdS.WidthDef = 52
        Me.txtCustCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleZaiRirekiDate
        '
        Me.lblTitleZaiRirekiDate.AutoSize = True
        Me.lblTitleZaiRirekiDate.AutoSizeDef = True
        Me.lblTitleZaiRirekiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZaiRirekiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZaiRirekiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZaiRirekiDate.EnableStatus = False
        Me.lblTitleZaiRirekiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZaiRirekiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZaiRirekiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZaiRirekiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZaiRirekiDate.HeightDef = 13
        Me.lblTitleZaiRirekiDate.Location = New System.Drawing.Point(313, 118)
        Me.lblTitleZaiRirekiDate.Name = "lblTitleZaiRirekiDate"
        Me.lblTitleZaiRirekiDate.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleZaiRirekiDate.TabIndex = 229
        Me.lblTitleZaiRirekiDate.Text = "月末在庫"
        Me.lblTitleZaiRirekiDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZaiRirekiDate.TextValue = "月末在庫"
        Me.lblTitleZaiRirekiDate.WidthDef = 63
        '
        'lblTitleHokokuDate
        '
        Me.lblTitleHokokuDate.AutoSize = True
        Me.lblTitleHokokuDate.AutoSizeDef = True
        Me.lblTitleHokokuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokokuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHokokuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHokokuDate.EnableStatus = False
        Me.lblTitleHokokuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokokuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHokokuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokokuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHokokuDate.HeightDef = 13
        Me.lblTitleHokokuDate.Location = New System.Drawing.Point(45, 118)
        Me.lblTitleHokokuDate.Name = "lblTitleHokokuDate"
        Me.lblTitleHokokuDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleHokokuDate.TabIndex = 226
        Me.lblTitleHokokuDate.Text = "報告日"
        Me.lblTitleHokokuDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHokokuDate.TextValue = "報告日"
        Me.lblTitleHokokuDate.WidthDef = 49
        '
        'imdHokokuDate
        '
        Me.imdHokokuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdHokokuDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdHokokuDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdHokokuDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdHokokuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdHokokuDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdHokokuDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdHokokuDate.HeightDef = 18
        Me.imdHokokuDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdHokokuDate.HissuLabelVisible = True
        Me.imdHokokuDate.Holiday = False
        Me.imdHokokuDate.IsAfterDateCheck = False
        Me.imdHokokuDate.IsBeforeDateCheck = False
        Me.imdHokokuDate.IsHissuCheck = True
        Me.imdHokokuDate.IsMinDateCheck = "1900/01/01"
        Me.imdHokokuDate.ItemName = ""
        Me.imdHokokuDate.Location = New System.Drawing.Point(99, 115)
        Me.imdHokokuDate.Name = "imdHokokuDate"
        Me.imdHokokuDate.Number = CType(10101000000, Long)
        Me.imdHokokuDate.ReadOnly = False
        Me.imdHokokuDate.Size = New System.Drawing.Size(118, 18)
        Me.imdHokokuDate.TabIndex = 225
        Me.imdHokokuDate.TabStopSetting = True
        Me.imdHokokuDate.TextValue = ""
        Me.imdHokokuDate.Value = New Date(CType(0, Long))
        Me.imdHokokuDate.WidthDef = 118
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
        Me.lblCustNmM.HissuLabelVisible = False
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = False
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(180, 67)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmM.TabIndex = 224
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 473
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCustNmL.HissuLabelVisible = False
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = False
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(180, 46)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 223
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 473
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCust.Location = New System.Drawing.Point(58, 49)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 222
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
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
        Me.txtCustCdM.Location = New System.Drawing.Point(144, 67)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 221
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdL.Location = New System.Drawing.Point(99, 46)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 220
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(44, 28)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.cmbEigyo.Location = New System.Drawing.Point(99, 25)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 247
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'lblTitlePrint
        '
        Me.lblTitlePrint.AutoSize = True
        Me.lblTitlePrint.AutoSizeDef = True
        Me.lblTitlePrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePrint.EnableStatus = False
        Me.lblTitlePrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePrint.HeightDef = 13
        Me.lblTitlePrint.Location = New System.Drawing.Point(40, 19)
        Me.lblTitlePrint.Name = "lblTitlePrint"
        Me.lblTitlePrint.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePrint.TabIndex = 242
        Me.lblTitlePrint.Text = "作成種別"
        Me.lblTitlePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrint.TextValue = "作成種別"
        Me.lblTitlePrint.WidthDef = 63
        '
        'cmbPlantCd
        '
        Me.cmbPlantCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPlantCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPlantCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPlantCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPlantCd.DataSource = Nothing
        Me.cmbPlantCd.DisplayMember = Nothing
        Me.cmbPlantCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPlantCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPlantCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPlantCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPlantCd.HeightDef = 18
        Me.cmbPlantCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPlantCd.HissuLabelVisible = False
        Me.cmbPlantCd.InsertWildCard = True
        Me.cmbPlantCd.IsForbiddenWordsCheck = False
        Me.cmbPlantCd.IsHissuCheck = False
        Me.cmbPlantCd.ItemName = ""
        Me.cmbPlantCd.Location = New System.Drawing.Point(440, 16)
        Me.cmbPlantCd.Name = "cmbPlantCd"
        Me.cmbPlantCd.ReadOnly = False
        Me.cmbPlantCd.SelectedIndex = -1
        Me.cmbPlantCd.SelectedItem = Nothing
        Me.cmbPlantCd.SelectedText = ""
        Me.cmbPlantCd.SelectedValue = ""
        Me.cmbPlantCd.Size = New System.Drawing.Size(86, 18)
        Me.cmbPlantCd.TabIndex = 243
        Me.cmbPlantCd.TabStopSetting = True
        Me.cmbPlantCd.TextValue = ""
        Me.cmbPlantCd.ValueMember = Nothing
        Me.cmbPlantCd.WidthDef = 86
        '
        'lblPlantNm
        '
        Me.lblPlantNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPlantNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPlantNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPlantNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPlantNm.CountWrappedLine = False
        Me.lblPlantNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblPlantNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPlantNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPlantNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPlantNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPlantNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblPlantNm.HeightDef = 18
        Me.lblPlantNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblPlantNm.HissuLabelVisible = False
        Me.lblPlantNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblPlantNm.IsByteCheck = 0
        Me.lblPlantNm.IsCalendarCheck = False
        Me.lblPlantNm.IsDakutenCheck = False
        Me.lblPlantNm.IsEisuCheck = False
        Me.lblPlantNm.IsForbiddenWordsCheck = False
        Me.lblPlantNm.IsFullByteCheck = 0
        Me.lblPlantNm.IsHankakuCheck = False
        Me.lblPlantNm.IsHissuCheck = False
        Me.lblPlantNm.IsKanaCheck = False
        Me.lblPlantNm.IsMiddleSpace = False
        Me.lblPlantNm.IsNumericCheck = False
        Me.lblPlantNm.IsSujiCheck = False
        Me.lblPlantNm.IsZenkakuCheck = False
        Me.lblPlantNm.ItemName = ""
        Me.lblPlantNm.LineSpace = 0
        Me.lblPlantNm.Location = New System.Drawing.Point(510, 16)
        Me.lblPlantNm.MaxLength = 0
        Me.lblPlantNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblPlantNm.MaxLineCount = 0
        Me.lblPlantNm.Multiline = False
        Me.lblPlantNm.Name = "lblPlantNm"
        Me.lblPlantNm.ReadOnly = True
        Me.lblPlantNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblPlantNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblPlantNm.Size = New System.Drawing.Size(255, 18)
        Me.lblPlantNm.TabIndex = 244
        Me.lblPlantNm.TabStop = False
        Me.lblPlantNm.TabStopSetting = False
        Me.lblPlantNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblPlantNm.UseSystemPasswordChar = False
        Me.lblPlantNm.WidthDef = 255
        Me.lblPlantNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlCust
        '
        Me.pnlCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCust.Controls.Add(Me.lblCustDtl)
        Me.pnlCust.EnableStatus = False
        Me.pnlCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCust.HeightDef = 166
        Me.pnlCust.Location = New System.Drawing.Point(28, 225)
        Me.pnlCust.Name = "pnlCust"
        Me.pnlCust.Size = New System.Drawing.Size(738, 166)
        Me.pnlCust.TabIndex = 245
        Me.pnlCust.TabStop = False
        Me.pnlCust.Text = "荷主(小)一覧"
        Me.pnlCust.TextValue = "荷主(小)一覧"
        Me.pnlCust.WidthDef = 738
        '
        'lblCustDtl
        '
        Me.lblCustDtl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustDtl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustDtl.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustDtl.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustDtl.CountWrappedLine = False
        Me.lblCustDtl.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustDtl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustDtl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustDtl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustDtl.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustDtl.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustDtl.HeightDef = 112
        Me.lblCustDtl.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustDtl.HissuLabelVisible = False
        Me.lblCustDtl.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustDtl.IsByteCheck = 0
        Me.lblCustDtl.IsCalendarCheck = False
        Me.lblCustDtl.IsDakutenCheck = False
        Me.lblCustDtl.IsEisuCheck = False
        Me.lblCustDtl.IsForbiddenWordsCheck = False
        Me.lblCustDtl.IsFullByteCheck = 0
        Me.lblCustDtl.IsHankakuCheck = False
        Me.lblCustDtl.IsHissuCheck = False
        Me.lblCustDtl.IsKanaCheck = False
        Me.lblCustDtl.IsMiddleSpace = False
        Me.lblCustDtl.IsNumericCheck = False
        Me.lblCustDtl.IsSujiCheck = False
        Me.lblCustDtl.IsZenkakuCheck = False
        Me.lblCustDtl.ItemName = ""
        Me.lblCustDtl.LineSpace = 0
        Me.lblCustDtl.Location = New System.Drawing.Point(47, 30)
        Me.lblCustDtl.MaxLength = 0
        Me.lblCustDtl.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustDtl.MaxLineCount = 0
        Me.lblCustDtl.Multiline = True
        Me.lblCustDtl.Name = "lblCustDtl"
        Me.lblCustDtl.ReadOnly = True
        Me.lblCustDtl.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustDtl.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustDtl.Size = New System.Drawing.Size(473, 112)
        Me.lblCustDtl.TabIndex = 223
        Me.lblCustDtl.TabStop = False
        Me.lblCustDtl.TabStopSetting = False
        Me.lblCustDtl.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustDtl.UseSystemPasswordChar = False
        Me.lblCustDtl.WidthDef = 473
        Me.lblCustDtl.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "D004"
        Me.cmbPrint.DataSource = Nothing
        Me.cmbPrint.DisplayMember = Nothing
        Me.cmbPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.HeightDef = 18
        Me.cmbPrint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrint.HissuLabelVisible = True
        Me.cmbPrint.InsertWildCard = True
        Me.cmbPrint.IsForbiddenWordsCheck = False
        Me.cmbPrint.IsHissuCheck = True
        Me.cmbPrint.ItemName = ""
        Me.cmbPrint.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrint.Location = New System.Drawing.Point(109, 16)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(320, 18)
        Me.cmbPrint.TabIndex = 246
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 320
        '
        'LMI020F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI020F"
        Me.Text = "【LMI020F】  デュポン在庫報告"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlCust.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleHokokuDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdHokokuDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleZaiRirekiDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitlePrint As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblPlantNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbPlantCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents cmbZaiRirekiDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents pnlCust As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblCustDtl As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblLoadFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
