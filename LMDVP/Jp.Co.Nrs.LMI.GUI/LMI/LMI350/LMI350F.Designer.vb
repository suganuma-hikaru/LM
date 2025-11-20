<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI350F
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
        Me.grpShuturyoku = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblCustNmS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleSeiq = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdSeiqDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblSeiqNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtSeiqCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleSeiqDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleNrsBrCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlViewAria.SuspendLayout()
        Me.grpShuturyoku.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpShuturyoku)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = " "
        Me.FunctionKey.F11ButtonName = " "
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F7ButtonName = "印　刷"
        Me.FunctionKey.F9ButtonName = " "
        Me.FunctionKey.Location = New System.Drawing.Point(119, 1)
        '
        'grpShuturyoku
        '
        Me.grpShuturyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpShuturyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpShuturyoku.Controls.Add(Me.lblCustNmS)
        Me.grpShuturyoku.Controls.Add(Me.txtCustCdS)
        Me.grpShuturyoku.Controls.Add(Me.lblCustNmM)
        Me.grpShuturyoku.Controls.Add(Me.txtCustCdM)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleSeiq)
        Me.grpShuturyoku.Controls.Add(Me.imdSeiqDate)
        Me.grpShuturyoku.Controls.Add(Me.cmbBr)
        Me.grpShuturyoku.Controls.Add(Me.lblSeiqNm)
        Me.grpShuturyoku.Controls.Add(Me.txtSeiqCd)
        Me.grpShuturyoku.Controls.Add(Me.lblCustNmL)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleSeiqDate)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleCust)
        Me.grpShuturyoku.Controls.Add(Me.txtCustCdL)
        Me.grpShuturyoku.Controls.Add(Me.lblTitleNrsBrCd)
        Me.grpShuturyoku.EnableStatus = False
        Me.grpShuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpShuturyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpShuturyoku.HeightDef = 181
        Me.grpShuturyoku.Location = New System.Drawing.Point(27, 13)
        Me.grpShuturyoku.Name = "grpShuturyoku"
        Me.grpShuturyoku.Size = New System.Drawing.Size(738, 181)
        Me.grpShuturyoku.TabIndex = 247
        Me.grpShuturyoku.TabStop = False
        Me.grpShuturyoku.Text = "出力条件"
        Me.grpShuturyoku.TextValue = "出力条件"
        Me.grpShuturyoku.WidthDef = 738
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
        Me.lblCustNmS.Location = New System.Drawing.Point(180, 100)
        Me.lblCustNmS.MaxLength = 0
        Me.lblCustNmS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmS.MaxLineCount = 0
        Me.lblCustNmS.Multiline = False
        Me.lblCustNmS.Name = "lblCustNmS"
        Me.lblCustNmS.ReadOnly = True
        Me.lblCustNmS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmS.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNmS.TabIndex = 415
        Me.lblCustNmS.TabStop = False
        Me.lblCustNmS.TabStopSetting = False
        Me.lblCustNmS.TextValue = ""
        Me.lblCustNmS.UseSystemPasswordChar = False
        Me.lblCustNmS.WidthDef = 513
        Me.lblCustNmS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdS
        '
        Me.txtCustCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdS.IsByteCheck = 5
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
        Me.txtCustCdS.Location = New System.Drawing.Point(149, 100)
        Me.txtCustCdS.MaxLength = 5
        Me.txtCustCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdS.MaxLineCount = 0
        Me.txtCustCdS.Multiline = False
        Me.txtCustCdS.Name = "txtCustCdS"
        Me.txtCustCdS.ReadOnly = True
        Me.txtCustCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdS.Size = New System.Drawing.Size(50, 18)
        Me.txtCustCdS.TabIndex = 414
        Me.txtCustCdS.TabStop = False
        Me.txtCustCdS.TabStopSetting = False
        Me.txtCustCdS.TextValue = ""
        Me.txtCustCdS.UseSystemPasswordChar = False
        Me.txtCustCdS.WidthDef = 50
        Me.txtCustCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCustNmM.Location = New System.Drawing.Point(180, 76)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNmM.TabIndex = 413
        Me.lblCustNmM.TabStop = False
        Me.lblCustNmM.TabStopSetting = False
        Me.lblCustNmM.TextValue = ""
        Me.lblCustNmM.UseSystemPasswordChar = False
        Me.lblCustNmM.WidthDef = 513
        Me.lblCustNmM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdM
        '
        Me.txtCustCdM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdM.IsByteCheck = 5
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
        Me.txtCustCdM.Location = New System.Drawing.Point(149, 76)
        Me.txtCustCdM.MaxLength = 5
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = True
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(50, 18)
        Me.txtCustCdM.TabIndex = 412
        Me.txtCustCdM.TabStop = False
        Me.txtCustCdM.TabStopSetting = False
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 50
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeiq
        '
        Me.lblTitleSeiq.AutoSize = True
        Me.lblTitleSeiq.AutoSizeDef = True
        Me.lblTitleSeiq.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiq.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiq.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiq.EnableStatus = False
        Me.lblTitleSeiq.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiq.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiq.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiq.HeightDef = 13
        Me.lblTitleSeiq.Location = New System.Drawing.Point(44, 127)
        Me.lblTitleSeiq.Name = "lblTitleSeiq"
        Me.lblTitleSeiq.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeiq.TabIndex = 411
        Me.lblTitleSeiq.Text = "請求先"
        Me.lblTitleSeiq.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiq.TextValue = "請求先"
        Me.lblTitleSeiq.WidthDef = 49
        '
        'imdSeiqDate
        '
        Me.imdSeiqDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeiqDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSeiqDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSeiqDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSeiqDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSeiqDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeiqDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSeiqDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeiqDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSeiqDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSeiqDate.HeightDef = 18
        Me.imdSeiqDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSeiqDate.HissuLabelVisible = True
        Me.imdSeiqDate.Holiday = True
        Me.imdSeiqDate.IsAfterDateCheck = False
        Me.imdSeiqDate.IsBeforeDateCheck = False
        Me.imdSeiqDate.IsHissuCheck = True
        Me.imdSeiqDate.IsMinDateCheck = "1900/01/01"
        Me.imdSeiqDate.ItemName = ""
        Me.imdSeiqDate.Location = New System.Drawing.Point(99, 148)
        Me.imdSeiqDate.Name = "imdSeiqDate"
        Me.imdSeiqDate.Number = CType(10101000000, Long)
        Me.imdSeiqDate.ReadOnly = False
        Me.imdSeiqDate.Size = New System.Drawing.Size(118, 18)
        Me.imdSeiqDate.TabIndex = 410
        Me.imdSeiqDate.TabStopSetting = True
        Me.imdSeiqDate.TextValue = ""
        Me.imdSeiqDate.Value = New Date(CType(0, Long))
        Me.imdSeiqDate.WidthDef = 118
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
        Me.cmbBr.Location = New System.Drawing.Point(99, 28)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = True
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 232
        Me.cmbBr.TabStop = False
        Me.cmbBr.TabStopSetting = False
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
        '
        'lblSeiqNm
        '
        Me.lblSeiqNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeiqNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeiqNm.CountWrappedLine = False
        Me.lblSeiqNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeiqNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeiqNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeiqNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeiqNm.HeightDef = 18
        Me.lblSeiqNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeiqNm.HissuLabelVisible = False
        Me.lblSeiqNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeiqNm.IsByteCheck = 0
        Me.lblSeiqNm.IsCalendarCheck = False
        Me.lblSeiqNm.IsDakutenCheck = False
        Me.lblSeiqNm.IsEisuCheck = False
        Me.lblSeiqNm.IsForbiddenWordsCheck = False
        Me.lblSeiqNm.IsFullByteCheck = 0
        Me.lblSeiqNm.IsHankakuCheck = False
        Me.lblSeiqNm.IsHissuCheck = False
        Me.lblSeiqNm.IsKanaCheck = False
        Me.lblSeiqNm.IsMiddleSpace = False
        Me.lblSeiqNm.IsNumericCheck = False
        Me.lblSeiqNm.IsSujiCheck = False
        Me.lblSeiqNm.IsZenkakuCheck = False
        Me.lblSeiqNm.ItemName = ""
        Me.lblSeiqNm.LineSpace = 0
        Me.lblSeiqNm.Location = New System.Drawing.Point(180, 124)
        Me.lblSeiqNm.MaxLength = 0
        Me.lblSeiqNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeiqNm.MaxLineCount = 0
        Me.lblSeiqNm.Multiline = False
        Me.lblSeiqNm.Name = "lblSeiqNm"
        Me.lblSeiqNm.ReadOnly = True
        Me.lblSeiqNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeiqNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeiqNm.Size = New System.Drawing.Size(513, 18)
        Me.lblSeiqNm.TabIndex = 130
        Me.lblSeiqNm.TabStop = False
        Me.lblSeiqNm.TabStopSetting = False
        Me.lblSeiqNm.TextValue = ""
        Me.lblSeiqNm.UseSystemPasswordChar = False
        Me.lblSeiqNm.WidthDef = 513
        Me.lblSeiqNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqCd
        '
        Me.txtSeiqCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqCd.CountWrappedLine = False
        Me.txtSeiqCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqCd.HeightDef = 18
        Me.txtSeiqCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqCd.HissuLabelVisible = False
        Me.txtSeiqCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSeiqCd.IsByteCheck = 7
        Me.txtSeiqCd.IsCalendarCheck = False
        Me.txtSeiqCd.IsDakutenCheck = False
        Me.txtSeiqCd.IsEisuCheck = False
        Me.txtSeiqCd.IsForbiddenWordsCheck = False
        Me.txtSeiqCd.IsFullByteCheck = 0
        Me.txtSeiqCd.IsHankakuCheck = False
        Me.txtSeiqCd.IsHissuCheck = False
        Me.txtSeiqCd.IsKanaCheck = False
        Me.txtSeiqCd.IsMiddleSpace = False
        Me.txtSeiqCd.IsNumericCheck = False
        Me.txtSeiqCd.IsSujiCheck = False
        Me.txtSeiqCd.IsZenkakuCheck = False
        Me.txtSeiqCd.ItemName = ""
        Me.txtSeiqCd.LineSpace = 0
        Me.txtSeiqCd.Location = New System.Drawing.Point(99, 124)
        Me.txtSeiqCd.MaxLength = 7
        Me.txtSeiqCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqCd.MaxLineCount = 0
        Me.txtSeiqCd.Multiline = False
        Me.txtSeiqCd.Name = "txtSeiqCd"
        Me.txtSeiqCd.ReadOnly = True
        Me.txtSeiqCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqCd.Size = New System.Drawing.Size(98, 18)
        Me.txtSeiqCd.TabIndex = 129
        Me.txtSeiqCd.TabStop = False
        Me.txtSeiqCd.TabStopSetting = False
        Me.txtSeiqCd.TextValue = ""
        Me.txtSeiqCd.UseSystemPasswordChar = False
        Me.txtSeiqCd.WidthDef = 98
        Me.txtSeiqCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCustNmL.Location = New System.Drawing.Point(180, 52)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(513, 18)
        Me.lblCustNmL.TabIndex = 127
        Me.lblCustNmL.TabStop = False
        Me.lblCustNmL.TabStopSetting = False
        Me.lblCustNmL.TextValue = ""
        Me.lblCustNmL.UseSystemPasswordChar = False
        Me.lblCustNmL.WidthDef = 513
        Me.lblCustNmL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeiqDate
        '
        Me.lblTitleSeiqDate.AutoSize = True
        Me.lblTitleSeiqDate.AutoSizeDef = True
        Me.lblTitleSeiqDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqDate.EnableStatus = False
        Me.lblTitleSeiqDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqDate.HeightDef = 13
        Me.lblTitleSeiqDate.Location = New System.Drawing.Point(31, 151)
        Me.lblTitleSeiqDate.Name = "lblTitleSeiqDate"
        Me.lblTitleSeiqDate.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSeiqDate.TabIndex = 126
        Me.lblTitleSeiqDate.Text = "請求年月"
        Me.lblTitleSeiqDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqDate.TextValue = "請求年月"
        Me.lblTitleSeiqDate.WidthDef = 63
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
        Me.lblTitleCust.Location = New System.Drawing.Point(58, 55)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 125
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
        '
        'txtCustCdL
        '
        Me.txtCustCdL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtCustCdL.Location = New System.Drawing.Point(99, 52)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = True
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(100, 18)
        Me.txtCustCdL.TabIndex = 74
        Me.txtCustCdL.TabStop = False
        Me.txtCustCdL.TabStopSetting = False
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 100
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleNrsBrCd.Location = New System.Drawing.Point(44, 31)
        Me.lblTitleNrsBrCd.Name = "lblTitleNrsBrCd"
        Me.lblTitleNrsBrCd.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleNrsBrCd.TabIndex = 17
        Me.lblTitleNrsBrCd.Text = "営業所"
        Me.lblTitleNrsBrCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNrsBrCd.TextValue = "営業所"
        Me.lblTitleNrsBrCd.WidthDef = 49
        '
        'LMI350F
        '
        Me.ClientSize = New System.Drawing.Size(794, 568)
        Me.Name = "LMI350F"
        Me.Text = "【LMI350】  保管荷役明細(MT触媒)"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpShuturyoku.ResumeLayout(False)
        Me.grpShuturyoku.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpShuturyoku As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblSeiqNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeiqCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeiqDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleNrsBrCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSeiqDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleSeiq As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNmS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox

End Class
