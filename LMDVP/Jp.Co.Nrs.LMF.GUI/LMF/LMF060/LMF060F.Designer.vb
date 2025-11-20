<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF060F
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
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMF060F))
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.btnKyoriSel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.pnlSisan = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleKm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKg = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblWarimashiNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtWarimashiCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleWarimashi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numJyuryo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTitleJyuryo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtKyoriteiCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numKyori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.txtTodokedeJisNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleKyoritei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKyori = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdUnsoDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleUnsoDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbSyasyu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.txtTodokedeJisCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblOrigJisNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleTodokedeJis = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleSyasyu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbUnso = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleUnso = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtOrigJis = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleOrigJis = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.pnlUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleKako = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleWarimashiBun = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numWarimashi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.btnGet = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.btnSet = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.btnDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblWint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblRely = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblInsu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblCity = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblFrry = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblUnchinMeisai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.pnlViewAria.SuspendLayout()
        Me.pnlSisan.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUnchin.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblUnchinMeisai)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.btnDel)
        Me.pnlViewAria.Controls.Add(Me.btnSet)
        Me.pnlViewAria.Controls.Add(Me.pnlUnchin)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.pnlSisan)
        Me.pnlViewAria.Controls.Add(Me.lblFrry)
        Me.pnlViewAria.Controls.Add(Me.lblCity)
        Me.pnlViewAria.Controls.Add(Me.lblInsu)
        Me.pnlViewAria.Controls.Add(Me.lblRely)
        Me.pnlViewAria.Controls.Add(Me.lblWint)
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
        Me.lblCustNm.Location = New System.Drawing.Point(184, 37)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(337, 18)
        Me.lblCustNm.TabIndex = 7
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 337
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
        Me.txtCustCdM.Location = New System.Drawing.Point(155, 37)
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
        Me.lblTitleCustCd.Location = New System.Drawing.Point(68, 41)
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
        Me.txtCustCdL.Location = New System.Drawing.Point(103, 37)
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
        'btnKyoriSel
        '
        Me.btnKyoriSel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKyoriSel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKyoriSel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnKyoriSel.EnableStatus = True
        Me.btnKyoriSel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKyoriSel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKyoriSel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKyoriSel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKyoriSel.HeightDef = 22
        Me.btnKyoriSel.Location = New System.Drawing.Point(559, 81)
        Me.btnKyoriSel.Name = "btnKyoriSel"
        Me.btnKyoriSel.Size = New System.Drawing.Size(91, 22)
        Me.btnKyoriSel.TabIndex = 14
        Me.btnKyoriSel.TabStopSetting = True
        Me.btnKyoriSel.Text = "距離取得"
        Me.btnKyoriSel.TextValue = "距離取得"
        Me.btnKyoriSel.UseVisualStyleBackColor = True
        Me.btnKyoriSel.WidthDef = 91
        '
        'pnlSisan
        '
        Me.pnlSisan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSisan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlSisan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlSisan.Controls.Add(Me.lblTitleKm)
        Me.pnlSisan.Controls.Add(Me.lblTitleKg)
        Me.pnlSisan.Controls.Add(Me.lblWarimashiNm)
        Me.pnlSisan.Controls.Add(Me.lblTariffNm)
        Me.pnlSisan.Controls.Add(Me.txtWarimashiCd)
        Me.pnlSisan.Controls.Add(Me.lblTitleWarimashi)
        Me.pnlSisan.Controls.Add(Me.txtTariffCd)
        Me.pnlSisan.Controls.Add(Me.lblTitleTariff)
        Me.pnlSisan.Controls.Add(Me.numJyuryo)
        Me.pnlSisan.Controls.Add(Me.lblTitleJyuryo)
        Me.pnlSisan.Controls.Add(Me.txtKyoriteiCd)
        Me.pnlSisan.Controls.Add(Me.numKyori)
        Me.pnlSisan.Controls.Add(Me.txtTodokedeJisNm)
        Me.pnlSisan.Controls.Add(Me.lblTitleKyoritei)
        Me.pnlSisan.Controls.Add(Me.lblTitleKyori)
        Me.pnlSisan.Controls.Add(Me.imdUnsoDate)
        Me.pnlSisan.Controls.Add(Me.lblTitleUnsoDate)
        Me.pnlSisan.Controls.Add(Me.lblCustNm)
        Me.pnlSisan.Controls.Add(Me.cmbSyasyu)
        Me.pnlSisan.Controls.Add(Me.txtTodokedeJisCd)
        Me.pnlSisan.Controls.Add(Me.lblOrigJisNm)
        Me.pnlSisan.Controls.Add(Me.lblTitleTodokedeJis)
        Me.pnlSisan.Controls.Add(Me.txtCustCdM)
        Me.pnlSisan.Controls.Add(Me.lblTitleSyasyu)
        Me.pnlSisan.Controls.Add(Me.cmbUnso)
        Me.pnlSisan.Controls.Add(Me.lblTitleUnso)
        Me.pnlSisan.Controls.Add(Me.txtOrigJis)
        Me.pnlSisan.Controls.Add(Me.btnKyoriSel)
        Me.pnlSisan.Controls.Add(Me.lblTitleOrigJis)
        Me.pnlSisan.Controls.Add(Me.lblTitleCustCd)
        Me.pnlSisan.Controls.Add(Me.txtCustCdL)
        Me.pnlSisan.EnableStatus = False
        Me.pnlSisan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSisan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlSisan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSisan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlSisan.HeightDef = 162
        Me.pnlSisan.Location = New System.Drawing.Point(12, 36)
        Me.pnlSisan.Name = "pnlSisan"
        Me.pnlSisan.Size = New System.Drawing.Size(1248, 162)
        Me.pnlSisan.TabIndex = 36
        Me.pnlSisan.TabStop = False
        Me.pnlSisan.Text = "試算情報"
        Me.pnlSisan.TextValue = "試算情報"
        Me.pnlSisan.WidthDef = 1248
        '
        'lblTitleKm
        '
        Me.lblTitleKm.AutoSize = True
        Me.lblTitleKm.AutoSizeDef = True
        Me.lblTitleKm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKm.EnableStatus = False
        Me.lblTitleKm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKm.HeightDef = 13
        Me.lblTitleKm.Location = New System.Drawing.Point(791, 84)
        Me.lblTitleKm.Name = "lblTitleKm"
        Me.lblTitleKm.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKm.TabIndex = 56
        Me.lblTitleKm.Text = "KM"
        Me.lblTitleKm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKm.TextValue = "KM"
        Me.lblTitleKm.WidthDef = 21
        '
        'lblTitleKg
        '
        Me.lblTitleKg.AutoSize = True
        Me.lblTitleKg.AutoSizeDef = True
        Me.lblTitleKg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg.EnableStatus = False
        Me.lblTitleKg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg.HeightDef = 13
        Me.lblTitleKg.Location = New System.Drawing.Point(791, 111)
        Me.lblTitleKg.Name = "lblTitleKg"
        Me.lblTitleKg.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg.TabIndex = 55
        Me.lblTitleKg.Text = "KG"
        Me.lblTitleKg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg.TextValue = "KG"
        Me.lblTitleKg.WidthDef = 21
        '
        'lblWarimashiNm
        '
        Me.lblWarimashiNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashiNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashiNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWarimashiNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWarimashiNm.CountWrappedLine = False
        Me.lblWarimashiNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWarimashiNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWarimashiNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWarimashiNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWarimashiNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWarimashiNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWarimashiNm.HeightDef = 18
        Me.lblWarimashiNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashiNm.HissuLabelVisible = False
        Me.lblWarimashiNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWarimashiNm.IsByteCheck = 0
        Me.lblWarimashiNm.IsCalendarCheck = False
        Me.lblWarimashiNm.IsDakutenCheck = False
        Me.lblWarimashiNm.IsEisuCheck = False
        Me.lblWarimashiNm.IsForbiddenWordsCheck = False
        Me.lblWarimashiNm.IsFullByteCheck = 0
        Me.lblWarimashiNm.IsHankakuCheck = False
        Me.lblWarimashiNm.IsHissuCheck = False
        Me.lblWarimashiNm.IsKanaCheck = False
        Me.lblWarimashiNm.IsMiddleSpace = False
        Me.lblWarimashiNm.IsNumericCheck = False
        Me.lblWarimashiNm.IsSujiCheck = False
        Me.lblWarimashiNm.IsZenkakuCheck = False
        Me.lblWarimashiNm.ItemName = ""
        Me.lblWarimashiNm.LineSpace = 0
        Me.lblWarimashiNm.Location = New System.Drawing.Point(184, 129)
        Me.lblWarimashiNm.MaxLength = 0
        Me.lblWarimashiNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWarimashiNm.MaxLineCount = 0
        Me.lblWarimashiNm.Multiline = False
        Me.lblWarimashiNm.Name = "lblWarimashiNm"
        Me.lblWarimashiNm.ReadOnly = True
        Me.lblWarimashiNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWarimashiNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWarimashiNm.Size = New System.Drawing.Size(337, 18)
        Me.lblWarimashiNm.TabIndex = 54
        Me.lblWarimashiNm.TabStop = False
        Me.lblWarimashiNm.TabStopSetting = False
        Me.lblWarimashiNm.TextValue = "ＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮ"
        Me.lblWarimashiNm.UseSystemPasswordChar = False
        Me.lblWarimashiNm.WidthDef = 337
        Me.lblWarimashiNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTariffNm
        '
        Me.lblTariffNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTariffNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTariffNm.CountWrappedLine = False
        Me.lblTariffNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTariffNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTariffNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTariffNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTariffNm.HeightDef = 18
        Me.lblTariffNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTariffNm.HissuLabelVisible = False
        Me.lblTariffNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTariffNm.IsByteCheck = 0
        Me.lblTariffNm.IsCalendarCheck = False
        Me.lblTariffNm.IsDakutenCheck = False
        Me.lblTariffNm.IsEisuCheck = False
        Me.lblTariffNm.IsForbiddenWordsCheck = False
        Me.lblTariffNm.IsFullByteCheck = 0
        Me.lblTariffNm.IsHankakuCheck = False
        Me.lblTariffNm.IsHissuCheck = False
        Me.lblTariffNm.IsKanaCheck = False
        Me.lblTariffNm.IsMiddleSpace = False
        Me.lblTariffNm.IsNumericCheck = False
        Me.lblTariffNm.IsSujiCheck = False
        Me.lblTariffNm.IsZenkakuCheck = False
        Me.lblTariffNm.ItemName = ""
        Me.lblTariffNm.LineSpace = 0
        Me.lblTariffNm.Location = New System.Drawing.Point(184, 108)
        Me.lblTariffNm.MaxLength = 0
        Me.lblTariffNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffNm.MaxLineCount = 0
        Me.lblTariffNm.Multiline = False
        Me.lblTariffNm.Name = "lblTariffNm"
        Me.lblTariffNm.ReadOnly = True
        Me.lblTariffNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffNm.Size = New System.Drawing.Size(337, 18)
        Me.lblTariffNm.TabIndex = 53
        Me.lblTariffNm.TabStop = False
        Me.lblTariffNm.TabStopSetting = False
        Me.lblTariffNm.TextValue = "ＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮ"
        Me.lblTariffNm.UseSystemPasswordChar = False
        Me.lblTariffNm.WidthDef = 337
        Me.lblTariffNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtWarimashiCd
        '
        Me.txtWarimashiCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWarimashiCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWarimashiCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWarimashiCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtWarimashiCd.CountWrappedLine = False
        Me.txtWarimashiCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWarimashiCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarimashiCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarimashiCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarimashiCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarimashiCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWarimashiCd.HeightDef = 18
        Me.txtWarimashiCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWarimashiCd.HissuLabelVisible = False
        Me.txtWarimashiCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtWarimashiCd.IsByteCheck = 10
        Me.txtWarimashiCd.IsCalendarCheck = False
        Me.txtWarimashiCd.IsDakutenCheck = False
        Me.txtWarimashiCd.IsEisuCheck = False
        Me.txtWarimashiCd.IsForbiddenWordsCheck = False
        Me.txtWarimashiCd.IsFullByteCheck = 0
        Me.txtWarimashiCd.IsHankakuCheck = False
        Me.txtWarimashiCd.IsHissuCheck = False
        Me.txtWarimashiCd.IsKanaCheck = False
        Me.txtWarimashiCd.IsMiddleSpace = False
        Me.txtWarimashiCd.IsNumericCheck = False
        Me.txtWarimashiCd.IsSujiCheck = False
        Me.txtWarimashiCd.IsZenkakuCheck = False
        Me.txtWarimashiCd.ItemName = ""
        Me.txtWarimashiCd.LineSpace = 0
        Me.txtWarimashiCd.Location = New System.Drawing.Point(103, 129)
        Me.txtWarimashiCd.MaxLength = 10
        Me.txtWarimashiCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWarimashiCd.MaxLineCount = 0
        Me.txtWarimashiCd.Multiline = False
        Me.txtWarimashiCd.Name = "txtWarimashiCd"
        Me.txtWarimashiCd.ReadOnly = False
        Me.txtWarimashiCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWarimashiCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWarimashiCd.Size = New System.Drawing.Size(97, 18)
        Me.txtWarimashiCd.TabIndex = 52
        Me.txtWarimashiCd.TabStopSetting = True
        Me.txtWarimashiCd.TextValue = "XXXXXXXXXX"
        Me.txtWarimashiCd.UseSystemPasswordChar = False
        Me.txtWarimashiCd.WidthDef = 97
        Me.txtWarimashiCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleWarimashi
        '
        Me.lblTitleWarimashi.AutoSize = True
        Me.lblTitleWarimashi.AutoSizeDef = True
        Me.lblTitleWarimashi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWarimashi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWarimashi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWarimashi.EnableStatus = False
        Me.lblTitleWarimashi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWarimashi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWarimashi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWarimashi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWarimashi.HeightDef = 13
        Me.lblTitleWarimashi.Location = New System.Drawing.Point(26, 132)
        Me.lblTitleWarimashi.Name = "lblTitleWarimashi"
        Me.lblTitleWarimashi.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleWarimashi.TabIndex = 51
        Me.lblTitleWarimashi.Text = "割増タリフ"
        Me.lblTitleWarimashi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWarimashi.TextValue = "割増タリフ"
        Me.lblTitleWarimashi.WidthDef = 77
        '
        'txtTariffCd
        '
        Me.txtTariffCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTariffCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTariffCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariffCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTariffCd.CountWrappedLine = False
        Me.txtTariffCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTariffCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTariffCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTariffCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTariffCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTariffCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTariffCd.HeightDef = 18
        Me.txtTariffCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTariffCd.HissuLabelVisible = False
        Me.txtTariffCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtTariffCd.IsByteCheck = 10
        Me.txtTariffCd.IsCalendarCheck = False
        Me.txtTariffCd.IsDakutenCheck = False
        Me.txtTariffCd.IsEisuCheck = False
        Me.txtTariffCd.IsForbiddenWordsCheck = False
        Me.txtTariffCd.IsFullByteCheck = 0
        Me.txtTariffCd.IsHankakuCheck = False
        Me.txtTariffCd.IsHissuCheck = False
        Me.txtTariffCd.IsKanaCheck = False
        Me.txtTariffCd.IsMiddleSpace = False
        Me.txtTariffCd.IsNumericCheck = False
        Me.txtTariffCd.IsSujiCheck = False
        Me.txtTariffCd.IsZenkakuCheck = False
        Me.txtTariffCd.ItemName = ""
        Me.txtTariffCd.LineSpace = 0
        Me.txtTariffCd.Location = New System.Drawing.Point(103, 108)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(97, 18)
        Me.txtTariffCd.TabIndex = 50
        Me.txtTariffCd.TabStopSetting = True
        Me.txtTariffCd.TextValue = "XXXXXXXXXX"
        Me.txtTariffCd.UseSystemPasswordChar = False
        Me.txtTariffCd.WidthDef = 97
        Me.txtTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTariff
        '
        Me.lblTitleTariff.AutoSize = True
        Me.lblTitleTariff.AutoSizeDef = True
        Me.lblTitleTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariff.EnableStatus = False
        Me.lblTitleTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.HeightDef = 13
        Me.lblTitleTariff.Location = New System.Drawing.Point(54, 111)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleTariff.TabIndex = 49
        Me.lblTitleTariff.Text = "タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "タリフ"
        Me.lblTitleTariff.WidthDef = 49
        '
        'numJyuryo
        '
        Me.numJyuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numJyuryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numJyuryo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numJyuryo.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numJyuryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numJyuryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numJyuryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numJyuryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numJyuryo.HeightDef = 18
        Me.numJyuryo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numJyuryo.HissuLabelVisible = False
        Me.numJyuryo.IsHissuCheck = False
        Me.numJyuryo.IsRangeCheck = False
        Me.numJyuryo.ItemName = ""
        Me.numJyuryo.Location = New System.Drawing.Point(691, 108)
        Me.numJyuryo.Name = "numJyuryo"
        Me.numJyuryo.ReadOnly = False
        Me.numJyuryo.Size = New System.Drawing.Size(115, 18)
        Me.numJyuryo.TabIndex = 48
        Me.numJyuryo.TabStopSetting = True
        Me.numJyuryo.TextValue = "0"
        Me.numJyuryo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numJyuryo.WidthDef = 115
        '
        'lblTitleJyuryo
        '
        Me.lblTitleJyuryo.AutoSize = True
        Me.lblTitleJyuryo.AutoSizeDef = True
        Me.lblTitleJyuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJyuryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJyuryo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJyuryo.EnableStatus = False
        Me.lblTitleJyuryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJyuryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJyuryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJyuryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJyuryo.HeightDef = 13
        Me.lblTitleJyuryo.Location = New System.Drawing.Point(656, 112)
        Me.lblTitleJyuryo.Name = "lblTitleJyuryo"
        Me.lblTitleJyuryo.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleJyuryo.TabIndex = 47
        Me.lblTitleJyuryo.Text = "重量"
        Me.lblTitleJyuryo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJyuryo.TextValue = "重量"
        Me.lblTitleJyuryo.WidthDef = 35
        '
        'txtKyoriteiCd
        '
        Me.txtKyoriteiCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKyoriteiCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKyoriteiCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKyoriteiCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKyoriteiCd.CountWrappedLine = False
        Me.txtKyoriteiCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKyoriteiCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKyoriteiCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKyoriteiCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKyoriteiCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKyoriteiCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKyoriteiCd.HeightDef = 18
        Me.txtKyoriteiCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKyoriteiCd.HissuLabelVisible = False
        Me.txtKyoriteiCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtKyoriteiCd.IsByteCheck = 3
        Me.txtKyoriteiCd.IsCalendarCheck = False
        Me.txtKyoriteiCd.IsDakutenCheck = False
        Me.txtKyoriteiCd.IsEisuCheck = False
        Me.txtKyoriteiCd.IsForbiddenWordsCheck = False
        Me.txtKyoriteiCd.IsFullByteCheck = 0
        Me.txtKyoriteiCd.IsHankakuCheck = False
        Me.txtKyoriteiCd.IsHissuCheck = False
        Me.txtKyoriteiCd.IsKanaCheck = False
        Me.txtKyoriteiCd.IsMiddleSpace = False
        Me.txtKyoriteiCd.IsNumericCheck = False
        Me.txtKyoriteiCd.IsSujiCheck = False
        Me.txtKyoriteiCd.IsZenkakuCheck = False
        Me.txtKyoriteiCd.ItemName = ""
        Me.txtKyoriteiCd.LineSpace = 0
        Me.txtKyoriteiCd.Location = New System.Drawing.Point(103, 83)
        Me.txtKyoriteiCd.MaxLength = 3
        Me.txtKyoriteiCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKyoriteiCd.MaxLineCount = 0
        Me.txtKyoriteiCd.Multiline = False
        Me.txtKyoriteiCd.Name = "txtKyoriteiCd"
        Me.txtKyoriteiCd.ReadOnly = False
        Me.txtKyoriteiCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKyoriteiCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKyoriteiCd.Size = New System.Drawing.Size(74, 18)
        Me.txtKyoriteiCd.TabIndex = 46
        Me.txtKyoriteiCd.TabStopSetting = True
        Me.txtKyoriteiCd.TextValue = "XXX"
        Me.txtKyoriteiCd.UseSystemPasswordChar = False
        Me.txtKyoriteiCd.WidthDef = 74
        Me.txtKyoriteiCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numKyori
        '
        Me.numKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKyori.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKyori.HeightDef = 18
        Me.numKyori.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKyori.HissuLabelVisible = False
        Me.numKyori.IsHissuCheck = False
        Me.numKyori.IsRangeCheck = False
        Me.numKyori.ItemName = ""
        Me.numKyori.Location = New System.Drawing.Point(691, 82)
        Me.numKyori.Name = "numKyori"
        Me.numKyori.ReadOnly = False
        Me.numKyori.Size = New System.Drawing.Size(115, 18)
        Me.numKyori.TabIndex = 45
        Me.numKyori.TabStopSetting = True
        Me.numKyori.TextValue = "0"
        Me.numKyori.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKyori.WidthDef = 115
        '
        'txtTodokedeJisNm
        '
        Me.txtTodokedeJisNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTodokedeJisNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTodokedeJisNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTodokedeJisNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTodokedeJisNm.CountWrappedLine = False
        Me.txtTodokedeJisNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTodokedeJisNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTodokedeJisNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTodokedeJisNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTodokedeJisNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTodokedeJisNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTodokedeJisNm.HeightDef = 18
        Me.txtTodokedeJisNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTodokedeJisNm.HissuLabelVisible = False
        Me.txtTodokedeJisNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtTodokedeJisNm.IsByteCheck = 0
        Me.txtTodokedeJisNm.IsCalendarCheck = False
        Me.txtTodokedeJisNm.IsDakutenCheck = False
        Me.txtTodokedeJisNm.IsEisuCheck = False
        Me.txtTodokedeJisNm.IsForbiddenWordsCheck = False
        Me.txtTodokedeJisNm.IsFullByteCheck = 0
        Me.txtTodokedeJisNm.IsHankakuCheck = False
        Me.txtTodokedeJisNm.IsHissuCheck = False
        Me.txtTodokedeJisNm.IsKanaCheck = False
        Me.txtTodokedeJisNm.IsMiddleSpace = False
        Me.txtTodokedeJisNm.IsNumericCheck = False
        Me.txtTodokedeJisNm.IsSujiCheck = False
        Me.txtTodokedeJisNm.IsZenkakuCheck = False
        Me.txtTodokedeJisNm.ItemName = ""
        Me.txtTodokedeJisNm.LineSpace = 0
        Me.txtTodokedeJisNm.Location = New System.Drawing.Point(749, 58)
        Me.txtTodokedeJisNm.MaxLength = 0
        Me.txtTodokedeJisNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTodokedeJisNm.MaxLineCount = 0
        Me.txtTodokedeJisNm.Multiline = False
        Me.txtTodokedeJisNm.Name = "txtTodokedeJisNm"
        Me.txtTodokedeJisNm.ReadOnly = True
        Me.txtTodokedeJisNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTodokedeJisNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTodokedeJisNm.Size = New System.Drawing.Size(337, 18)
        Me.txtTodokedeJisNm.TabIndex = 41
        Me.txtTodokedeJisNm.TabStop = False
        Me.txtTodokedeJisNm.TabStopSetting = False
        Me.txtTodokedeJisNm.TextValue = "ＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮ"
        Me.txtTodokedeJisNm.UseSystemPasswordChar = False
        Me.txtTodokedeJisNm.WidthDef = 337
        Me.txtTodokedeJisNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleKyoritei
        '
        Me.lblTitleKyoritei.AutoSize = True
        Me.lblTitleKyoritei.AutoSizeDef = True
        Me.lblTitleKyoritei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKyoritei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKyoritei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKyoritei.EnableStatus = False
        Me.lblTitleKyoritei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKyoritei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKyoritei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKyoritei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKyoritei.HeightDef = 13
        Me.lblTitleKyoritei.Location = New System.Drawing.Point(12, 86)
        Me.lblTitleKyoritei.Name = "lblTitleKyoritei"
        Me.lblTitleKyoritei.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleKyoritei.TabIndex = 44
        Me.lblTitleKyoritei.Text = "距離程コード"
        Me.lblTitleKyoritei.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKyoritei.TextValue = "距離程コード"
        Me.lblTitleKyoritei.WidthDef = 91
        '
        'lblTitleKyori
        '
        Me.lblTitleKyori.AutoSize = True
        Me.lblTitleKyori.AutoSizeDef = True
        Me.lblTitleKyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKyori.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKyori.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKyori.EnableStatus = False
        Me.lblTitleKyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKyori.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKyori.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKyori.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKyori.HeightDef = 13
        Me.lblTitleKyori.Location = New System.Drawing.Point(656, 85)
        Me.lblTitleKyori.Name = "lblTitleKyori"
        Me.lblTitleKyori.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKyori.TabIndex = 43
        Me.lblTitleKyori.Text = "距離"
        Me.lblTitleKyori.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKyori.TextValue = "距離"
        Me.lblTitleKyori.WidthDef = 35
        '
        'imdUnsoDate
        '
        Me.imdUnsoDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdUnsoDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdUnsoDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdUnsoDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdUnsoDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdUnsoDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdUnsoDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdUnsoDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdUnsoDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdUnsoDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdUnsoDate.HeightDef = 18
        Me.imdUnsoDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdUnsoDate.HissuLabelVisible = False
        Me.imdUnsoDate.Holiday = True
        Me.imdUnsoDate.IsAfterDateCheck = False
        Me.imdUnsoDate.IsBeforeDateCheck = False
        Me.imdUnsoDate.IsHissuCheck = False
        Me.imdUnsoDate.IsMinDateCheck = "1900/01/01"
        Me.imdUnsoDate.ItemName = ""
        Me.imdUnsoDate.Location = New System.Drawing.Point(691, 37)
        Me.imdUnsoDate.Name = "imdUnsoDate"
        Me.imdUnsoDate.Number = CType(20101214000000, Long)
        Me.imdUnsoDate.ReadOnly = False
        Me.imdUnsoDate.Size = New System.Drawing.Size(118, 18)
        Me.imdUnsoDate.TabIndex = 42
        Me.imdUnsoDate.TabStopSetting = True
        Me.imdUnsoDate.TextValue = "20101214"
        Me.imdUnsoDate.Value = New Date(2010, 12, 14, 0, 0, 0, 0)
        Me.imdUnsoDate.WidthDef = 118
        '
        'lblTitleUnsoDate
        '
        Me.lblTitleUnsoDate.AutoSize = True
        Me.lblTitleUnsoDate.AutoSizeDef = True
        Me.lblTitleUnsoDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoDate.EnableStatus = False
        Me.lblTitleUnsoDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoDate.HeightDef = 13
        Me.lblTitleUnsoDate.Location = New System.Drawing.Point(642, 40)
        Me.lblTitleUnsoDate.Name = "lblTitleUnsoDate"
        Me.lblTitleUnsoDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleUnsoDate.TabIndex = 41
        Me.lblTitleUnsoDate.Text = "運送日"
        Me.lblTitleUnsoDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoDate.TextValue = "運送日"
        Me.lblTitleUnsoDate.WidthDef = 49
        '
        'cmbSyasyu
        '
        Me.cmbSyasyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSyasyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSyasyu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSyasyu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSyasyu.DataCode = "S012"
        Me.cmbSyasyu.DataSource = Nothing
        Me.cmbSyasyu.DisplayMember = Nothing
        Me.cmbSyasyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSyasyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSyasyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSyasyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSyasyu.HeightDef = 18
        Me.cmbSyasyu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSyasyu.HissuLabelVisible = False
        Me.cmbSyasyu.InsertWildCard = True
        Me.cmbSyasyu.IsForbiddenWordsCheck = False
        Me.cmbSyasyu.IsHissuCheck = False
        Me.cmbSyasyu.ItemName = ""
        Me.cmbSyasyu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSyasyu.Location = New System.Drawing.Point(691, 16)
        Me.cmbSyasyu.Name = "cmbSyasyu"
        Me.cmbSyasyu.ReadOnly = False
        Me.cmbSyasyu.SelectedIndex = -1
        Me.cmbSyasyu.SelectedItem = Nothing
        Me.cmbSyasyu.SelectedText = ""
        Me.cmbSyasyu.SelectedValue = ""
        Me.cmbSyasyu.Size = New System.Drawing.Size(118, 18)
        Me.cmbSyasyu.TabIndex = 38
        Me.cmbSyasyu.TabStopSetting = True
        Me.cmbSyasyu.TextValue = ""
        Me.cmbSyasyu.Value1 = Nothing
        Me.cmbSyasyu.Value2 = Nothing
        Me.cmbSyasyu.Value3 = Nothing
        Me.cmbSyasyu.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSyasyu.ValueMember = Nothing
        Me.cmbSyasyu.WidthDef = 118
        '
        'txtTodokedeJisCd
        '
        Me.txtTodokedeJisCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTodokedeJisCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTodokedeJisCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTodokedeJisCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtTodokedeJisCd.CountWrappedLine = False
        Me.txtTodokedeJisCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtTodokedeJisCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTodokedeJisCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTodokedeJisCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTodokedeJisCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTodokedeJisCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtTodokedeJisCd.HeightDef = 18
        Me.txtTodokedeJisCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtTodokedeJisCd.HissuLabelVisible = False
        Me.txtTodokedeJisCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtTodokedeJisCd.IsByteCheck = 7
        Me.txtTodokedeJisCd.IsCalendarCheck = False
        Me.txtTodokedeJisCd.IsDakutenCheck = False
        Me.txtTodokedeJisCd.IsEisuCheck = False
        Me.txtTodokedeJisCd.IsForbiddenWordsCheck = False
        Me.txtTodokedeJisCd.IsFullByteCheck = 0
        Me.txtTodokedeJisCd.IsHankakuCheck = False
        Me.txtTodokedeJisCd.IsHissuCheck = False
        Me.txtTodokedeJisCd.IsKanaCheck = False
        Me.txtTodokedeJisCd.IsMiddleSpace = False
        Me.txtTodokedeJisCd.IsNumericCheck = False
        Me.txtTodokedeJisCd.IsSujiCheck = False
        Me.txtTodokedeJisCd.IsZenkakuCheck = False
        Me.txtTodokedeJisCd.ItemName = ""
        Me.txtTodokedeJisCd.LineSpace = 0
        Me.txtTodokedeJisCd.Location = New System.Drawing.Point(691, 58)
        Me.txtTodokedeJisCd.MaxLength = 7
        Me.txtTodokedeJisCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTodokedeJisCd.MaxLineCount = 0
        Me.txtTodokedeJisCd.Multiline = False
        Me.txtTodokedeJisCd.Name = "txtTodokedeJisCd"
        Me.txtTodokedeJisCd.ReadOnly = False
        Me.txtTodokedeJisCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTodokedeJisCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTodokedeJisCd.Size = New System.Drawing.Size(74, 18)
        Me.txtTodokedeJisCd.TabIndex = 40
        Me.txtTodokedeJisCd.TabStopSetting = True
        Me.txtTodokedeJisCd.TextValue = "XXXXXXX"
        Me.txtTodokedeJisCd.UseSystemPasswordChar = False
        Me.txtTodokedeJisCd.WidthDef = 74
        Me.txtTodokedeJisCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblOrigJisNm
        '
        Me.lblOrigJisNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrigJisNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrigJisNm.CountWrappedLine = False
        Me.lblOrigJisNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblOrigJisNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigJisNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrigJisNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigJisNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrigJisNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblOrigJisNm.HeightDef = 18
        Me.lblOrigJisNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrigJisNm.HissuLabelVisible = False
        Me.lblOrigJisNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblOrigJisNm.IsByteCheck = 0
        Me.lblOrigJisNm.IsCalendarCheck = False
        Me.lblOrigJisNm.IsDakutenCheck = False
        Me.lblOrigJisNm.IsEisuCheck = False
        Me.lblOrigJisNm.IsForbiddenWordsCheck = False
        Me.lblOrigJisNm.IsFullByteCheck = 0
        Me.lblOrigJisNm.IsHankakuCheck = False
        Me.lblOrigJisNm.IsHissuCheck = False
        Me.lblOrigJisNm.IsKanaCheck = False
        Me.lblOrigJisNm.IsMiddleSpace = False
        Me.lblOrigJisNm.IsNumericCheck = False
        Me.lblOrigJisNm.IsSujiCheck = False
        Me.lblOrigJisNm.IsZenkakuCheck = False
        Me.lblOrigJisNm.ItemName = ""
        Me.lblOrigJisNm.LineSpace = 0
        Me.lblOrigJisNm.Location = New System.Drawing.Point(184, 58)
        Me.lblOrigJisNm.MaxLength = 0
        Me.lblOrigJisNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblOrigJisNm.MaxLineCount = 0
        Me.lblOrigJisNm.Multiline = False
        Me.lblOrigJisNm.Name = "lblOrigJisNm"
        Me.lblOrigJisNm.ReadOnly = True
        Me.lblOrigJisNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblOrigJisNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblOrigJisNm.Size = New System.Drawing.Size(337, 18)
        Me.lblOrigJisNm.TabIndex = 38
        Me.lblOrigJisNm.TabStop = False
        Me.lblOrigJisNm.TabStopSetting = False
        Me.lblOrigJisNm.TextValue = "ＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮＮ"
        Me.lblOrigJisNm.UseSystemPasswordChar = False
        Me.lblOrigJisNm.WidthDef = 337
        Me.lblOrigJisNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTodokedeJis
        '
        Me.lblTitleTodokedeJis.AutoSize = True
        Me.lblTitleTodokedeJis.AutoSizeDef = True
        Me.lblTitleTodokedeJis.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTodokedeJis.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTodokedeJis.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTodokedeJis.EnableStatus = False
        Me.lblTitleTodokedeJis.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTodokedeJis.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTodokedeJis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTodokedeJis.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTodokedeJis.HeightDef = 13
        Me.lblTitleTodokedeJis.Location = New System.Drawing.Point(635, 61)
        Me.lblTitleTodokedeJis.Name = "lblTitleTodokedeJis"
        Me.lblTitleTodokedeJis.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleTodokedeJis.TabIndex = 39
        Me.lblTitleTodokedeJis.Text = "届先JIS"
        Me.lblTitleTodokedeJis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTodokedeJis.TextValue = "届先JIS"
        Me.lblTitleTodokedeJis.WidthDef = 56
        '
        'lblTitleSyasyu
        '
        Me.lblTitleSyasyu.AutoSize = True
        Me.lblTitleSyasyu.AutoSizeDef = True
        Me.lblTitleSyasyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSyasyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSyasyu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSyasyu.EnableStatus = False
        Me.lblTitleSyasyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSyasyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSyasyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSyasyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSyasyu.HeightDef = 13
        Me.lblTitleSyasyu.Location = New System.Drawing.Point(628, 20)
        Me.lblTitleSyasyu.Name = "lblTitleSyasyu"
        Me.lblTitleSyasyu.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSyasyu.TabIndex = 38
        Me.lblTitleSyasyu.Text = "車輌区分"
        Me.lblTitleSyasyu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSyasyu.TextValue = "車輌区分"
        Me.lblTitleSyasyu.WidthDef = 63
        '
        'cmbUnso
        '
        Me.cmbUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbUnso.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbUnso.DataCode = "T015"
        Me.cmbUnso.DataSource = Nothing
        Me.cmbUnso.DisplayMember = Nothing
        Me.cmbUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbUnso.HeightDef = 18
        Me.cmbUnso.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbUnso.HissuLabelVisible = False
        Me.cmbUnso.InsertWildCard = True
        Me.cmbUnso.IsForbiddenWordsCheck = False
        Me.cmbUnso.IsHissuCheck = False
        Me.cmbUnso.ItemName = ""
        Me.cmbUnso.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbUnso.Location = New System.Drawing.Point(103, 16)
        Me.cmbUnso.Name = "cmbUnso"
        Me.cmbUnso.ReadOnly = False
        Me.cmbUnso.SelectedIndex = -1
        Me.cmbUnso.SelectedItem = Nothing
        Me.cmbUnso.SelectedText = ""
        Me.cmbUnso.SelectedValue = ""
        Me.cmbUnso.Size = New System.Drawing.Size(100, 18)
        Me.cmbUnso.TabIndex = 37
        Me.cmbUnso.TabStopSetting = True
        Me.cmbUnso.TextValue = ""
        Me.cmbUnso.Value1 = "='1.000'"
        Me.cmbUnso.Value2 = Nothing
        Me.cmbUnso.Value3 = Nothing
        Me.cmbUnso.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V1
        Me.cmbUnso.ValueMember = Nothing
        Me.cmbUnso.WidthDef = 100
        '
        'lblTitleUnso
        '
        Me.lblTitleUnso.AutoSize = True
        Me.lblTitleUnso.AutoSizeDef = True
        Me.lblTitleUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnso.EnableStatus = False
        Me.lblTitleUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnso.HeightDef = 13
        Me.lblTitleUnso.Location = New System.Drawing.Point(26, 20)
        Me.lblTitleUnso.Name = "lblTitleUnso"
        Me.lblTitleUnso.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleUnso.TabIndex = 31
        Me.lblTitleUnso.Text = "タリフ分類"
        Me.lblTitleUnso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnso.TextValue = "タリフ分類"
        Me.lblTitleUnso.WidthDef = 77
        '
        'txtOrigJis
        '
        Me.txtOrigJis.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigJis.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOrigJis.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrigJis.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOrigJis.CountWrappedLine = False
        Me.txtOrigJis.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOrigJis.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigJis.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOrigJis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigJis.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOrigJis.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOrigJis.HeightDef = 18
        Me.txtOrigJis.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOrigJis.HissuLabelVisible = False
        Me.txtOrigJis.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtOrigJis.IsByteCheck = 7
        Me.txtOrigJis.IsCalendarCheck = False
        Me.txtOrigJis.IsDakutenCheck = False
        Me.txtOrigJis.IsEisuCheck = False
        Me.txtOrigJis.IsForbiddenWordsCheck = False
        Me.txtOrigJis.IsFullByteCheck = 0
        Me.txtOrigJis.IsHankakuCheck = False
        Me.txtOrigJis.IsHissuCheck = False
        Me.txtOrigJis.IsKanaCheck = False
        Me.txtOrigJis.IsMiddleSpace = False
        Me.txtOrigJis.IsNumericCheck = False
        Me.txtOrigJis.IsSujiCheck = False
        Me.txtOrigJis.IsZenkakuCheck = False
        Me.txtOrigJis.ItemName = ""
        Me.txtOrigJis.LineSpace = 0
        Me.txtOrigJis.Location = New System.Drawing.Point(103, 58)
        Me.txtOrigJis.MaxLength = 7
        Me.txtOrigJis.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOrigJis.MaxLineCount = 0
        Me.txtOrigJis.Multiline = False
        Me.txtOrigJis.Name = "txtOrigJis"
        Me.txtOrigJis.ReadOnly = False
        Me.txtOrigJis.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOrigJis.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOrigJis.Size = New System.Drawing.Size(97, 18)
        Me.txtOrigJis.TabIndex = 38
        Me.txtOrigJis.TabStopSetting = True
        Me.txtOrigJis.TextValue = "XXXXXXX"
        Me.txtOrigJis.UseSystemPasswordChar = False
        Me.txtOrigJis.WidthDef = 97
        Me.txtOrigJis.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleOrigJis
        '
        Me.lblTitleOrigJis.AutoSize = True
        Me.lblTitleOrigJis.AutoSizeDef = True
        Me.lblTitleOrigJis.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigJis.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOrigJis.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOrigJis.EnableStatus = False
        Me.lblTitleOrigJis.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigJis.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOrigJis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigJis.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOrigJis.HeightDef = 13
        Me.lblTitleOrigJis.Location = New System.Drawing.Point(47, 62)
        Me.lblTitleOrigJis.Name = "lblTitleOrigJis"
        Me.lblTitleOrigJis.Size = New System.Drawing.Size(56, 13)
        Me.lblTitleOrigJis.TabIndex = 38
        Me.lblTitleOrigJis.Text = "発地JIS"
        Me.lblTitleOrigJis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOrigJis.TextValue = "発地JIS"
        Me.lblTitleOrigJis.WidthDef = 56
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
        Me.sprDetail.HeightDef = 612
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(12, 264)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        Me.sprDetail.Size = New System.Drawing.Size(1229, 612)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 0
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1229
        '
        'pnlUnchin
        '
        Me.pnlUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlUnchin.Controls.Add(Me.lblTitleKako)
        Me.pnlUnchin.Controls.Add(Me.lblTitleWarimashiBun)
        Me.pnlUnchin.Controls.Add(Me.numWarimashi)
        Me.pnlUnchin.Controls.Add(Me.numUnchin)
        Me.pnlUnchin.Controls.Add(Me.btnGet)
        Me.pnlUnchin.EnableStatus = False
        Me.pnlUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnchin.HeightDef = 55
        Me.pnlUnchin.Location = New System.Drawing.Point(12, 204)
        Me.pnlUnchin.Name = "pnlUnchin"
        Me.pnlUnchin.Size = New System.Drawing.Size(455, 55)
        Me.pnlUnchin.TabIndex = 37
        Me.pnlUnchin.TabStop = False
        Me.pnlUnchin.TextValue = ""
        Me.pnlUnchin.WidthDef = 455
        '
        'lblTitleKako
        '
        Me.lblTitleKako.AutoSize = True
        Me.lblTitleKako.AutoSizeDef = True
        Me.lblTitleKako.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKako.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKako.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKako.EnableStatus = False
        Me.lblTitleKako.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKako.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKako.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKako.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKako.HeightDef = 13
        Me.lblTitleKako.Location = New System.Drawing.Point(406, 21)
        Me.lblTitleKako.Name = "lblTitleKako"
        Me.lblTitleKako.Size = New System.Drawing.Size(14, 13)
        Me.lblTitleKako.TabIndex = 53
        Me.lblTitleKako.Text = ")"
        Me.lblTitleKako.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKako.TextValue = ")"
        Me.lblTitleKako.WidthDef = 14
        '
        'lblTitleWarimashiBun
        '
        Me.lblTitleWarimashiBun.AutoSize = True
        Me.lblTitleWarimashiBun.AutoSizeDef = True
        Me.lblTitleWarimashiBun.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWarimashiBun.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleWarimashiBun.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleWarimashiBun.EnableStatus = False
        Me.lblTitleWarimashiBun.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWarimashiBun.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleWarimashiBun.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWarimashiBun.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleWarimashiBun.HeightDef = 13
        Me.lblTitleWarimashiBun.Location = New System.Drawing.Point(236, 21)
        Me.lblTitleWarimashiBun.Name = "lblTitleWarimashiBun"
        Me.lblTitleWarimashiBun.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleWarimashiBun.TabIndex = 52
        Me.lblTitleWarimashiBun.Text = "(内割増分"
        Me.lblTitleWarimashiBun.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWarimashiBun.TextValue = "(内割増分"
        Me.lblTitleWarimashiBun.WidthDef = 70
        '
        'numWarimashi
        '
        Me.numWarimashi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWarimashi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWarimashi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numWarimashi.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numWarimashi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numWarimashi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numWarimashi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numWarimashi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numWarimashi.HeightDef = 18
        Me.numWarimashi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numWarimashi.HissuLabelVisible = False
        Me.numWarimashi.IsHissuCheck = False
        Me.numWarimashi.IsRangeCheck = False
        Me.numWarimashi.ItemName = ""
        Me.numWarimashi.Location = New System.Drawing.Point(306, 18)
        Me.numWarimashi.Name = "numWarimashi"
        Me.numWarimashi.ReadOnly = True
        Me.numWarimashi.Size = New System.Drawing.Size(115, 18)
        Me.numWarimashi.TabIndex = 50
        Me.numWarimashi.TabStop = False
        Me.numWarimashi.TabStopSetting = False
        Me.numWarimashi.TextValue = "0"
        Me.numWarimashi.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numWarimashi.WidthDef = 115
        '
        'numUnchin
        '
        Me.numUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnchin.HeightDef = 18
        Me.numUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnchin.HissuLabelVisible = False
        Me.numUnchin.IsHissuCheck = False
        Me.numUnchin.IsRangeCheck = False
        Me.numUnchin.ItemName = ""
        Me.numUnchin.Location = New System.Drawing.Point(123, 18)
        Me.numUnchin.Name = "numUnchin"
        Me.numUnchin.ReadOnly = True
        Me.numUnchin.Size = New System.Drawing.Size(115, 18)
        Me.numUnchin.TabIndex = 49
        Me.numUnchin.TabStop = False
        Me.numUnchin.TabStopSetting = False
        Me.numUnchin.TextValue = "0"
        Me.numUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnchin.WidthDef = 115
        '
        'btnGet
        '
        Me.btnGet.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnGet.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnGet.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnGet.EnableStatus = True
        Me.btnGet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnGet.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnGet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGet.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGet.HeightDef = 22
        Me.btnGet.Location = New System.Drawing.Point(12, 16)
        Me.btnGet.Name = "btnGet"
        Me.btnGet.Size = New System.Drawing.Size(91, 22)
        Me.btnGet.TabIndex = 15
        Me.btnGet.TabStopSetting = True
        Me.btnGet.Text = "運賃取得"
        Me.btnGet.TextValue = "運賃取得"
        Me.btnGet.UseVisualStyleBackColor = True
        Me.btnGet.WidthDef = 91
        '
        'btnSet
        '
        Me.btnSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSet.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSet.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSet.EnableStatus = True
        Me.btnSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSet.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSet.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSet.HeightDef = 22
        Me.btnSet.Location = New System.Drawing.Point(475, 220)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(131, 22)
        Me.btnSet.TabIndex = 38
        Me.btnSet.TabStopSetting = True
        Me.btnSet.Text = "試算結果退避"
        Me.btnSet.TextValue = "試算結果退避"
        Me.btnSet.UseVisualStyleBackColor = True
        Me.btnSet.WidthDef = 131
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnDel.EnableStatus = True
        Me.btnDel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDel.HeightDef = 22
        Me.btnDel.Location = New System.Drawing.Point(612, 220)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(68, 22)
        Me.btnDel.TabIndex = 39
        Me.btnDel.TabStopSetting = True
        Me.btnDel.Text = "行削除"
        Me.btnDel.TextValue = "行削除"
        Me.btnDel.UseVisualStyleBackColor = True
        Me.btnDel.WidthDef = 68
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(66, 16)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 335
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
        Me.cmbEigyo.HissuLabelVisible = False
        Me.cmbEigyo.InsertWildCard = True
        Me.cmbEigyo.IsForbiddenWordsCheck = False
        Me.cmbEigyo.IsHissuCheck = False
        Me.cmbEigyo.ItemName = ""
        Me.cmbEigyo.Location = New System.Drawing.Point(115, 12)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(297, 18)
        Me.cmbEigyo.TabIndex = 334
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 297
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnPrint.EnableStatus = True
        Me.btnPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnPrint.HeightDef = 22
        Me.btnPrint.Location = New System.Drawing.Point(853, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 336
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "P004"
        Me.cmbPrint.DataSource = Nothing
        Me.cmbPrint.DisplayMember = Nothing
        Me.cmbPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbPrint.HeightDef = 18
        Me.cmbPrint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbPrint.HissuLabelVisible = False
        Me.cmbPrint.InsertWildCard = True
        Me.cmbPrint.IsForbiddenWordsCheck = False
        Me.cmbPrint.IsHissuCheck = False
        Me.cmbPrint.ItemName = ""
        Me.cmbPrint.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbPrint.Location = New System.Drawing.Point(703, 12)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(150, 18)
        Me.cmbPrint.TabIndex = 405
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 150
        '
        'lblWint
        '
        Me.lblWint.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWint.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblWint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWint.HeightDef = 18
        Me.lblWint.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWint.HissuLabelVisible = False
        Me.lblWint.IsHissuCheck = False
        Me.lblWint.IsRangeCheck = False
        Me.lblWint.ItemName = ""
        Me.lblWint.Location = New System.Drawing.Point(12, 797)
        Me.lblWint.Name = "lblWint"
        Me.lblWint.ReadOnly = True
        Me.lblWint.Size = New System.Drawing.Size(115, 18)
        Me.lblWint.TabIndex = 406
        Me.lblWint.TabStop = False
        Me.lblWint.TabStopSetting = False
        Me.lblWint.TextValue = "0"
        Me.lblWint.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblWint.Visible = False
        Me.lblWint.WidthDef = 115
        '
        'lblRely
        '
        Me.lblRely.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRely.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRely.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRely.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRely.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRely.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblRely.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRely.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRely.HeightDef = 18
        Me.lblRely.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblRely.HissuLabelVisible = False
        Me.lblRely.IsHissuCheck = False
        Me.lblRely.IsRangeCheck = False
        Me.lblRely.ItemName = ""
        Me.lblRely.Location = New System.Drawing.Point(133, 797)
        Me.lblRely.Name = "lblRely"
        Me.lblRely.ReadOnly = True
        Me.lblRely.Size = New System.Drawing.Size(115, 18)
        Me.lblRely.TabIndex = 407
        Me.lblRely.TabStop = False
        Me.lblRely.TabStopSetting = False
        Me.lblRely.TextValue = "0"
        Me.lblRely.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblRely.Visible = False
        Me.lblRely.WidthDef = 115
        '
        'lblInsu
        '
        Me.lblInsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInsu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInsu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInsu.HeightDef = 18
        Me.lblInsu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInsu.HissuLabelVisible = False
        Me.lblInsu.IsHissuCheck = False
        Me.lblInsu.IsRangeCheck = False
        Me.lblInsu.ItemName = ""
        Me.lblInsu.Location = New System.Drawing.Point(254, 797)
        Me.lblInsu.Name = "lblInsu"
        Me.lblInsu.ReadOnly = True
        Me.lblInsu.Size = New System.Drawing.Size(115, 18)
        Me.lblInsu.TabIndex = 408
        Me.lblInsu.TabStop = False
        Me.lblInsu.TabStopSetting = False
        Me.lblInsu.TextValue = "0"
        Me.lblInsu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblInsu.Visible = False
        Me.lblInsu.WidthDef = 115
        '
        'lblCity
        '
        Me.lblCity.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCity.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCity.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCity.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCity.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCity.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCity.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCity.HeightDef = 18
        Me.lblCity.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCity.HissuLabelVisible = False
        Me.lblCity.IsHissuCheck = False
        Me.lblCity.IsRangeCheck = False
        Me.lblCity.ItemName = ""
        Me.lblCity.Location = New System.Drawing.Point(12, 838)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.ReadOnly = True
        Me.lblCity.Size = New System.Drawing.Size(115, 18)
        Me.lblCity.TabIndex = 409
        Me.lblCity.TabStop = False
        Me.lblCity.TabStopSetting = False
        Me.lblCity.TextValue = "0"
        Me.lblCity.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblCity.Visible = False
        Me.lblCity.WidthDef = 115
        '
        'lblFrry
        '
        Me.lblFrry.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFrry.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFrry.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFrry.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFrry.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFrry.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFrry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFrry.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFrry.HeightDef = 18
        Me.lblFrry.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFrry.HissuLabelVisible = False
        Me.lblFrry.IsHissuCheck = False
        Me.lblFrry.IsRangeCheck = False
        Me.lblFrry.ItemName = ""
        Me.lblFrry.Location = New System.Drawing.Point(135, 838)
        Me.lblFrry.Name = "lblFrry"
        Me.lblFrry.ReadOnly = True
        Me.lblFrry.Size = New System.Drawing.Size(115, 18)
        Me.lblFrry.TabIndex = 410
        Me.lblFrry.TabStop = False
        Me.lblFrry.TabStopSetting = False
        Me.lblFrry.TextValue = "0"
        Me.lblFrry.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblFrry.Visible = False
        Me.lblFrry.WidthDef = 115
        '
        'lblUnchinMeisai
        '
        Me.lblUnchinMeisai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinMeisai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinMeisai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnchinMeisai.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.lblUnchinMeisai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinMeisai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnchinMeisai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinMeisai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnchinMeisai.HeightDef = 18
        Me.lblUnchinMeisai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnchinMeisai.HissuLabelVisible = False
        Me.lblUnchinMeisai.IsHissuCheck = False
        Me.lblUnchinMeisai.IsRangeCheck = False
        Me.lblUnchinMeisai.ItemName = ""
        Me.lblUnchinMeisai.Location = New System.Drawing.Point(709, 220)
        Me.lblUnchinMeisai.Name = "lblUnchinMeisai"
        Me.lblUnchinMeisai.ReadOnly = True
        Me.lblUnchinMeisai.Size = New System.Drawing.Size(115, 18)
        Me.lblUnchinMeisai.TabIndex = 411
        Me.lblUnchinMeisai.TabStop = False
        Me.lblUnchinMeisai.TabStopSetting = False
        Me.lblUnchinMeisai.TextValue = "0"
        Me.lblUnchinMeisai.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.lblUnchinMeisai.Visible = False
        Me.lblUnchinMeisai.WidthDef = 115
        '
        'LMF060F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMF060F"
        Me.Text = "【LMF060】 運賃試算"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlSisan.ResumeLayout(False)
        Me.pnlSisan.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUnchin.ResumeLayout(False)
        Me.pnlUnchin.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnKyoriSel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlSisan As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtKyoriteiCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numKyori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtTodokedeJisNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleKyoritei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKyori As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdUnsoDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleUnsoDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSyasyu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtTodokedeJisCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblOrigJisNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTodokedeJis As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSyasyu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbUnso As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleUnso As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOrigJis As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOrigJis As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblWarimashiNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtWarimashiCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleWarimashi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numJyuryo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleJyuryo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents pnlUnchin As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnGet As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnSet As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleKako As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleWarimashiBun As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numWarimashi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents btnDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblFrry As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblCity As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblInsu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblRely As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblWint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblUnchinMeisai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber

End Class
