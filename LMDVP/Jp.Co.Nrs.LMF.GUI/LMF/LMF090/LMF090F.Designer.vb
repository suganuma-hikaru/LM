<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF090F
    Inherits Jp.Co.Nrs.LM.GUI.Win.LMFormSxga

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMF090F))
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.imdShukka = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleShukka = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblUnsoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUnsoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblUnsoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleUnso = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleTehai = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKiken = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleShashu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleNisugata = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKyori = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleJuryo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKosu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleWarimashi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbKiken = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbShashu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbNisugata = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.numKyori = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numJuryo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numKosu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblWarimashi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtWarimashi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.btnKeisan = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
        Me.lblTitleKg1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.imdArr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.lblTitleArr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleKanriNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleUnsoNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtUnsoNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtKanriNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtKanriNo)
        Me.pnlViewAria.Controls.Add(Me.txtUnsoNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKanriNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUnsoNo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleArr)
        Me.pnlViewAria.Controls.Add(Me.imdArr)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKg1)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.btnKeisan)
        Me.pnlViewAria.Controls.Add(Me.lblWarimashi)
        Me.pnlViewAria.Controls.Add(Me.txtWarimashi)
        Me.pnlViewAria.Controls.Add(Me.lblTariffNm)
        Me.pnlViewAria.Controls.Add(Me.txtTariffCd)
        Me.pnlViewAria.Controls.Add(Me.numKosu)
        Me.pnlViewAria.Controls.Add(Me.numJuryo)
        Me.pnlViewAria.Controls.Add(Me.numKyori)
        Me.pnlViewAria.Controls.Add(Me.cmbNisugata)
        Me.pnlViewAria.Controls.Add(Me.cmbShashu)
        Me.pnlViewAria.Controls.Add(Me.cmbKiken)
        Me.pnlViewAria.Controls.Add(Me.cmbTariffKbn)
        Me.pnlViewAria.Controls.Add(Me.lblTitleWarimashi)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTariff)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKosu)
        Me.pnlViewAria.Controls.Add(Me.lblTitleJuryo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKyori)
        Me.pnlViewAria.Controls.Add(Me.lblTitleNisugata)
        Me.pnlViewAria.Controls.Add(Me.lblTitleShashu)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKiken)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTehai)
        Me.pnlViewAria.Controls.Add(Me.lblUnsoNm)
        Me.pnlViewAria.Controls.Add(Me.lblUnsoBrCd)
        Me.pnlViewAria.Controls.Add(Me.lblUnsoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleUnso)
        Me.pnlViewAria.Controls.Add(Me.lblTitleShukka)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.imdShukka)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
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
        Me.cmbEigyo.Location = New System.Drawing.Point(98, 20)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(297, 18)
        Me.cmbEigyo.TabIndex = 110
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 297
        '
        'imdShukka
        '
        Me.imdShukka.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdShukka.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdShukka.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdShukka.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdShukka.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdShukka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdShukka.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdShukka.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdShukka.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdShukka.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdShukka.HeightDef = 18
        Me.imdShukka.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdShukka.HissuLabelVisible = False
        Me.imdShukka.Holiday = False
        Me.imdShukka.IsAfterDateCheck = False
        Me.imdShukka.IsBeforeDateCheck = False
        Me.imdShukka.IsHissuCheck = False
        Me.imdShukka.IsMinDateCheck = "1900/01/01"
        Me.imdShukka.ItemName = ""
        Me.imdShukka.Location = New System.Drawing.Point(98, 41)
        Me.imdShukka.Name = "imdShukka"
        Me.imdShukka.Number = CType(10101000000, Long)
        Me.imdShukka.ReadOnly = True
        Me.imdShukka.Size = New System.Drawing.Size(118, 18)
        Me.imdShukka.TabIndex = 329
        Me.imdShukka.TabStop = False
        Me.imdShukka.TabStopSetting = False
        Me.imdShukka.TextValue = ""
        Me.imdShukka.Value = New Date(CType(0, Long))
        Me.imdShukka.WidthDef = 118
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(49, 24)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 333
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblTitleShukka
        '
        Me.lblTitleShukka.AutoSize = True
        Me.lblTitleShukka.AutoSizeDef = True
        Me.lblTitleShukka.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShukka.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShukka.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShukka.EnableStatus = False
        Me.lblTitleShukka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShukka.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShukka.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShukka.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShukka.HeightDef = 13
        Me.lblTitleShukka.Location = New System.Drawing.Point(49, 44)
        Me.lblTitleShukka.Name = "lblTitleShukka"
        Me.lblTitleShukka.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleShukka.TabIndex = 334
        Me.lblTitleShukka.Text = "出荷日"
        Me.lblTitleShukka.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShukka.TextValue = "出荷日"
        Me.lblTitleShukka.WidthDef = 49
        '
        'lblUnsoNm
        '
        Me.lblUnsoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoNm.CountWrappedLine = False
        Me.lblUnsoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoNm.HeightDef = 18
        Me.lblUnsoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoNm.HissuLabelVisible = False
        Me.lblUnsoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsoNm.IsByteCheck = 0
        Me.lblUnsoNm.IsCalendarCheck = False
        Me.lblUnsoNm.IsDakutenCheck = False
        Me.lblUnsoNm.IsEisuCheck = False
        Me.lblUnsoNm.IsForbiddenWordsCheck = False
        Me.lblUnsoNm.IsFullByteCheck = 0
        Me.lblUnsoNm.IsHankakuCheck = False
        Me.lblUnsoNm.IsHissuCheck = False
        Me.lblUnsoNm.IsKanaCheck = False
        Me.lblUnsoNm.IsMiddleSpace = False
        Me.lblUnsoNm.IsNumericCheck = False
        Me.lblUnsoNm.IsSujiCheck = False
        Me.lblUnsoNm.IsZenkakuCheck = False
        Me.lblUnsoNm.ItemName = ""
        Me.lblUnsoNm.LineSpace = 0
        Me.lblUnsoNm.Location = New System.Drawing.Point(172, 83)
        Me.lblUnsoNm.MaxLength = 0
        Me.lblUnsoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoNm.MaxLineCount = 0
        Me.lblUnsoNm.Multiline = False
        Me.lblUnsoNm.Name = "lblUnsoNm"
        Me.lblUnsoNm.ReadOnly = True
        Me.lblUnsoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoNm.Size = New System.Drawing.Size(473, 18)
        Me.lblUnsoNm.TabIndex = 337
        Me.lblUnsoNm.TabStop = False
        Me.lblUnsoNm.TabStopSetting = False
        Me.lblUnsoNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblUnsoNm.UseSystemPasswordChar = False
        Me.lblUnsoNm.WidthDef = 473
        Me.lblUnsoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnsoBrCd
        '
        Me.lblUnsoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoBrCd.CountWrappedLine = False
        Me.lblUnsoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoBrCd.HeightDef = 18
        Me.lblUnsoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoBrCd.HissuLabelVisible = False
        Me.lblUnsoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.lblUnsoBrCd.IsByteCheck = 3
        Me.lblUnsoBrCd.IsCalendarCheck = False
        Me.lblUnsoBrCd.IsDakutenCheck = False
        Me.lblUnsoBrCd.IsEisuCheck = False
        Me.lblUnsoBrCd.IsForbiddenWordsCheck = False
        Me.lblUnsoBrCd.IsFullByteCheck = 0
        Me.lblUnsoBrCd.IsHankakuCheck = False
        Me.lblUnsoBrCd.IsHissuCheck = False
        Me.lblUnsoBrCd.IsKanaCheck = False
        Me.lblUnsoBrCd.IsMiddleSpace = False
        Me.lblUnsoBrCd.IsNumericCheck = False
        Me.lblUnsoBrCd.IsSujiCheck = False
        Me.lblUnsoBrCd.IsZenkakuCheck = False
        Me.lblUnsoBrCd.ItemName = ""
        Me.lblUnsoBrCd.LineSpace = 0
        Me.lblUnsoBrCd.Location = New System.Drawing.Point(142, 83)
        Me.lblUnsoBrCd.MaxLength = 3
        Me.lblUnsoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoBrCd.MaxLineCount = 0
        Me.lblUnsoBrCd.Multiline = False
        Me.lblUnsoBrCd.Name = "lblUnsoBrCd"
        Me.lblUnsoBrCd.ReadOnly = True
        Me.lblUnsoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoBrCd.Size = New System.Drawing.Size(46, 18)
        Me.lblUnsoBrCd.TabIndex = 336
        Me.lblUnsoBrCd.TabStop = False
        Me.lblUnsoBrCd.TabStopSetting = False
        Me.lblUnsoBrCd.TextValue = "XX"
        Me.lblUnsoBrCd.UseSystemPasswordChar = False
        Me.lblUnsoBrCd.WidthDef = 46
        Me.lblUnsoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblUnsoCd
        '
        Me.lblUnsoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsoCd.CountWrappedLine = False
        Me.lblUnsoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsoCd.HeightDef = 18
        Me.lblUnsoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsoCd.HissuLabelVisible = False
        Me.lblUnsoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.lblUnsoCd.IsByteCheck = 5
        Me.lblUnsoCd.IsCalendarCheck = False
        Me.lblUnsoCd.IsDakutenCheck = False
        Me.lblUnsoCd.IsEisuCheck = False
        Me.lblUnsoCd.IsForbiddenWordsCheck = False
        Me.lblUnsoCd.IsFullByteCheck = 0
        Me.lblUnsoCd.IsHankakuCheck = False
        Me.lblUnsoCd.IsHissuCheck = False
        Me.lblUnsoCd.IsKanaCheck = False
        Me.lblUnsoCd.IsMiddleSpace = False
        Me.lblUnsoCd.IsNumericCheck = False
        Me.lblUnsoCd.IsSujiCheck = False
        Me.lblUnsoCd.IsZenkakuCheck = False
        Me.lblUnsoCd.ItemName = ""
        Me.lblUnsoCd.LineSpace = 0
        Me.lblUnsoCd.Location = New System.Drawing.Point(98, 83)
        Me.lblUnsoCd.MaxLength = 5
        Me.lblUnsoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsoCd.MaxLineCount = 0
        Me.lblUnsoCd.Multiline = False
        Me.lblUnsoCd.Name = "lblUnsoCd"
        Me.lblUnsoCd.ReadOnly = True
        Me.lblUnsoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsoCd.Size = New System.Drawing.Size(60, 18)
        Me.lblUnsoCd.TabIndex = 338
        Me.lblUnsoCd.TabStop = False
        Me.lblUnsoCd.TabStopSetting = False
        Me.lblUnsoCd.TextValue = "XXXXX"
        Me.lblUnsoCd.UseSystemPasswordChar = False
        Me.lblUnsoCd.WidthDef = 60
        Me.lblUnsoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleUnso.Location = New System.Drawing.Point(35, 86)
        Me.lblTitleUnso.Name = "lblTitleUnso"
        Me.lblTitleUnso.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnso.TabIndex = 335
        Me.lblTitleUnso.Text = "運送会社"
        Me.lblTitleUnso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnso.TextValue = "運送会社"
        Me.lblTitleUnso.WidthDef = 63
        '
        'lblTitleTehai
        '
        Me.lblTitleTehai.AutoSize = True
        Me.lblTitleTehai.AutoSizeDef = True
        Me.lblTitleTehai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTehai.EnableStatus = False
        Me.lblTitleTehai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehai.HeightDef = 13
        Me.lblTitleTehai.Location = New System.Drawing.Point(21, 127)
        Me.lblTitleTehai.Name = "lblTitleTehai"
        Me.lblTitleTehai.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTehai.TabIndex = 340
        Me.lblTitleTehai.Text = "タリフ分類"
        Me.lblTitleTehai.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTehai.TextValue = "タリフ分類"
        Me.lblTitleTehai.WidthDef = 77
        '
        'lblTitleKiken
        '
        Me.lblTitleKiken.AutoSize = True
        Me.lblTitleKiken.AutoSizeDef = True
        Me.lblTitleKiken.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKiken.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKiken.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKiken.EnableStatus = False
        Me.lblTitleKiken.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKiken.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKiken.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKiken.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKiken.HeightDef = 13
        Me.lblTitleKiken.Location = New System.Drawing.Point(35, 148)
        Me.lblTitleKiken.Name = "lblTitleKiken"
        Me.lblTitleKiken.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleKiken.TabIndex = 341
        Me.lblTitleKiken.Text = "危険区分"
        Me.lblTitleKiken.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKiken.TextValue = "危険区分"
        Me.lblTitleKiken.WidthDef = 63
        '
        'lblTitleShashu
        '
        Me.lblTitleShashu.AutoSize = True
        Me.lblTitleShashu.AutoSizeDef = True
        Me.lblTitleShashu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShashu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShashu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShashu.EnableStatus = False
        Me.lblTitleShashu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShashu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShashu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShashu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShashu.HeightDef = 13
        Me.lblTitleShashu.Location = New System.Drawing.Point(35, 169)
        Me.lblTitleShashu.Name = "lblTitleShashu"
        Me.lblTitleShashu.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleShashu.TabIndex = 342
        Me.lblTitleShashu.Text = "車輌区分"
        Me.lblTitleShashu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShashu.TextValue = "車輌区分"
        Me.lblTitleShashu.WidthDef = 63
        '
        'lblTitleNisugata
        '
        Me.lblTitleNisugata.AutoSize = True
        Me.lblTitleNisugata.AutoSizeDef = True
        Me.lblTitleNisugata.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNisugata.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleNisugata.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleNisugata.EnableStatus = False
        Me.lblTitleNisugata.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNisugata.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleNisugata.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNisugata.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleNisugata.HeightDef = 13
        Me.lblTitleNisugata.Location = New System.Drawing.Point(63, 190)
        Me.lblTitleNisugata.Name = "lblTitleNisugata"
        Me.lblTitleNisugata.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleNisugata.TabIndex = 343
        Me.lblTitleNisugata.Text = "荷姿"
        Me.lblTitleNisugata.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleNisugata.TextValue = "荷姿"
        Me.lblTitleNisugata.WidthDef = 35
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
        Me.lblTitleKyori.Location = New System.Drawing.Point(333, 127)
        Me.lblTitleKyori.Name = "lblTitleKyori"
        Me.lblTitleKyori.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKyori.TabIndex = 344
        Me.lblTitleKyori.Text = "距離"
        Me.lblTitleKyori.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKyori.TextValue = "距離"
        Me.lblTitleKyori.WidthDef = 35
        '
        'lblTitleJuryo
        '
        Me.lblTitleJuryo.AutoSize = True
        Me.lblTitleJuryo.AutoSizeDef = True
        Me.lblTitleJuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJuryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJuryo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJuryo.EnableStatus = False
        Me.lblTitleJuryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJuryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJuryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJuryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJuryo.HeightDef = 13
        Me.lblTitleJuryo.Location = New System.Drawing.Point(305, 148)
        Me.lblTitleJuryo.Name = "lblTitleJuryo"
        Me.lblTitleJuryo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleJuryo.TabIndex = 345
        Me.lblTitleJuryo.Text = "運送重量"
        Me.lblTitleJuryo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJuryo.TextValue = "運送重量"
        Me.lblTitleJuryo.WidthDef = 63
        '
        'lblTitleKosu
        '
        Me.lblTitleKosu.AutoSize = True
        Me.lblTitleKosu.AutoSizeDef = True
        Me.lblTitleKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKosu.EnableStatus = False
        Me.lblTitleKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKosu.HeightDef = 13
        Me.lblTitleKosu.Location = New System.Drawing.Point(333, 169)
        Me.lblTitleKosu.Name = "lblTitleKosu"
        Me.lblTitleKosu.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleKosu.TabIndex = 346
        Me.lblTitleKosu.Text = "個数"
        Me.lblTitleKosu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKosu.TextValue = "個数"
        Me.lblTitleKosu.WidthDef = 35
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
        Me.lblTitleTariff.Location = New System.Drawing.Point(576, 127)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariff.TabIndex = 347
        Me.lblTitleTariff.Text = "支払タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "支払タリフ"
        Me.lblTitleTariff.WidthDef = 77
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
        Me.lblTitleWarimashi.Location = New System.Drawing.Point(548, 148)
        Me.lblTitleWarimashi.Name = "lblTitleWarimashi"
        Me.lblTitleWarimashi.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleWarimashi.TabIndex = 348
        Me.lblTitleWarimashi.Text = "支払割増タリフ"
        Me.lblTitleWarimashi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleWarimashi.TextValue = "支払割増タリフ"
        Me.lblTitleWarimashi.WidthDef = 105
        '
        'cmbTariffKbn
        '
        Me.cmbTariffKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTariffKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTariffKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTariffKbn.DataCode = "T015"
        Me.cmbTariffKbn.DataSource = Nothing
        Me.cmbTariffKbn.DisplayMember = Nothing
        Me.cmbTariffKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTariffKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTariffKbn.HeightDef = 18
        Me.cmbTariffKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTariffKbn.HissuLabelVisible = False
        Me.cmbTariffKbn.InsertWildCard = True
        Me.cmbTariffKbn.IsForbiddenWordsCheck = False
        Me.cmbTariffKbn.IsHissuCheck = False
        Me.cmbTariffKbn.ItemName = ""
        Me.cmbTariffKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTariffKbn.Location = New System.Drawing.Point(98, 124)
        Me.cmbTariffKbn.Name = "cmbTariffKbn"
        Me.cmbTariffKbn.ReadOnly = False
        Me.cmbTariffKbn.SelectedIndex = -1
        Me.cmbTariffKbn.SelectedItem = Nothing
        Me.cmbTariffKbn.SelectedText = ""
        Me.cmbTariffKbn.SelectedValue = ""
        Me.cmbTariffKbn.Size = New System.Drawing.Size(118, 18)
        Me.cmbTariffKbn.TabIndex = 349
        Me.cmbTariffKbn.TabStopSetting = True
        Me.cmbTariffKbn.TextValue = ""
        Me.cmbTariffKbn.Value1 = ""
        Me.cmbTariffKbn.Value2 = "='1.000'"
        Me.cmbTariffKbn.Value3 = ""
        Me.cmbTariffKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V2
        Me.cmbTariffKbn.ValueMember = "1"
        Me.cmbTariffKbn.WidthDef = 118
        '
        'cmbKiken
        '
        Me.cmbKiken.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKiken.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbKiken.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbKiken.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbKiken.DataCode = "K008"
        Me.cmbKiken.DataSource = Nothing
        Me.cmbKiken.DisplayMember = Nothing
        Me.cmbKiken.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKiken.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbKiken.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKiken.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbKiken.HeightDef = 18
        Me.cmbKiken.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbKiken.HissuLabelVisible = False
        Me.cmbKiken.InsertWildCard = True
        Me.cmbKiken.IsForbiddenWordsCheck = False
        Me.cmbKiken.IsHissuCheck = False
        Me.cmbKiken.ItemName = ""
        Me.cmbKiken.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbKiken.Location = New System.Drawing.Point(98, 145)
        Me.cmbKiken.Name = "cmbKiken"
        Me.cmbKiken.ReadOnly = False
        Me.cmbKiken.SelectedIndex = -1
        Me.cmbKiken.SelectedItem = Nothing
        Me.cmbKiken.SelectedText = ""
        Me.cmbKiken.SelectedValue = ""
        Me.cmbKiken.Size = New System.Drawing.Size(118, 18)
        Me.cmbKiken.TabIndex = 350
        Me.cmbKiken.TabStopSetting = True
        Me.cmbKiken.TextValue = ""
        Me.cmbKiken.Value1 = Nothing
        Me.cmbKiken.Value2 = Nothing
        Me.cmbKiken.Value3 = Nothing
        Me.cmbKiken.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbKiken.ValueMember = Nothing
        Me.cmbKiken.WidthDef = 118
        '
        'cmbShashu
        '
        Me.cmbShashu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShashu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShashu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShashu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShashu.DataCode = "S012"
        Me.cmbShashu.DataSource = Nothing
        Me.cmbShashu.DisplayMember = Nothing
        Me.cmbShashu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShashu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShashu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShashu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShashu.HeightDef = 18
        Me.cmbShashu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShashu.HissuLabelVisible = False
        Me.cmbShashu.InsertWildCard = True
        Me.cmbShashu.IsForbiddenWordsCheck = False
        Me.cmbShashu.IsHissuCheck = False
        Me.cmbShashu.ItemName = ""
        Me.cmbShashu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShashu.Location = New System.Drawing.Point(98, 166)
        Me.cmbShashu.Name = "cmbShashu"
        Me.cmbShashu.ReadOnly = False
        Me.cmbShashu.SelectedIndex = -1
        Me.cmbShashu.SelectedItem = Nothing
        Me.cmbShashu.SelectedText = ""
        Me.cmbShashu.SelectedValue = ""
        Me.cmbShashu.Size = New System.Drawing.Size(118, 18)
        Me.cmbShashu.TabIndex = 351
        Me.cmbShashu.TabStopSetting = True
        Me.cmbShashu.TextValue = ""
        Me.cmbShashu.Value1 = Nothing
        Me.cmbShashu.Value2 = Nothing
        Me.cmbShashu.Value3 = Nothing
        Me.cmbShashu.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbShashu.ValueMember = Nothing
        Me.cmbShashu.WidthDef = 118
        '
        'cmbNisugata
        '
        Me.cmbNisugata.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNisugata.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbNisugata.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbNisugata.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbNisugata.DataCode = "N001"
        Me.cmbNisugata.DataSource = Nothing
        Me.cmbNisugata.DisplayMember = Nothing
        Me.cmbNisugata.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNisugata.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbNisugata.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNisugata.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbNisugata.HeightDef = 18
        Me.cmbNisugata.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbNisugata.HissuLabelVisible = False
        Me.cmbNisugata.InsertWildCard = True
        Me.cmbNisugata.IsForbiddenWordsCheck = False
        Me.cmbNisugata.IsHissuCheck = False
        Me.cmbNisugata.ItemName = ""
        Me.cmbNisugata.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbNisugata.Location = New System.Drawing.Point(98, 187)
        Me.cmbNisugata.Name = "cmbNisugata"
        Me.cmbNisugata.ReadOnly = False
        Me.cmbNisugata.SelectedIndex = -1
        Me.cmbNisugata.SelectedItem = Nothing
        Me.cmbNisugata.SelectedText = ""
        Me.cmbNisugata.SelectedValue = ""
        Me.cmbNisugata.Size = New System.Drawing.Size(118, 18)
        Me.cmbNisugata.TabIndex = 352
        Me.cmbNisugata.TabStopSetting = True
        Me.cmbNisugata.TextValue = ""
        Me.cmbNisugata.Value1 = Nothing
        Me.cmbNisugata.Value2 = Nothing
        Me.cmbNisugata.Value3 = Nothing
        Me.cmbNisugata.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbNisugata.ValueMember = Nothing
        Me.cmbNisugata.WidthDef = 118
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
        Me.numKyori.Location = New System.Drawing.Point(368, 124)
        Me.numKyori.Name = "numKyori"
        Me.numKyori.ReadOnly = False
        Me.numKyori.Size = New System.Drawing.Size(138, 18)
        Me.numKyori.TabIndex = 353
        Me.numKyori.TabStopSetting = True
        Me.numKyori.TextValue = "0"
        Me.numKyori.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKyori.WidthDef = 138
        '
        'numJuryo
        '
        Me.numJuryo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numJuryo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numJuryo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numJuryo.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numJuryo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numJuryo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numJuryo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numJuryo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numJuryo.HeightDef = 18
        Me.numJuryo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numJuryo.HissuLabelVisible = False
        Me.numJuryo.IsHissuCheck = False
        Me.numJuryo.IsRangeCheck = False
        Me.numJuryo.ItemName = ""
        Me.numJuryo.Location = New System.Drawing.Point(368, 145)
        Me.numJuryo.Name = "numJuryo"
        Me.numJuryo.ReadOnly = False
        Me.numJuryo.Size = New System.Drawing.Size(138, 18)
        Me.numJuryo.TabIndex = 354
        Me.numJuryo.TabStopSetting = True
        Me.numJuryo.TextValue = "0"
        Me.numJuryo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numJuryo.WidthDef = 138
        '
        'numKosu
        '
        Me.numKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKosu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKosu.HeightDef = 18
        Me.numKosu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKosu.HissuLabelVisible = False
        Me.numKosu.IsHissuCheck = False
        Me.numKosu.IsRangeCheck = False
        Me.numKosu.ItemName = ""
        Me.numKosu.Location = New System.Drawing.Point(368, 166)
        Me.numKosu.Name = "numKosu"
        Me.numKosu.ReadOnly = False
        Me.numKosu.Size = New System.Drawing.Size(138, 18)
        Me.numKosu.TabIndex = 355
        Me.numKosu.TabStopSetting = True
        Me.numKosu.TextValue = "0"
        Me.numKosu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKosu.WidthDef = 138
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
        Me.lblTariffNm.Location = New System.Drawing.Point(727, 124)
        Me.lblTariffNm.MaxLength = 0
        Me.lblTariffNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffNm.MaxLineCount = 0
        Me.lblTariffNm.Multiline = False
        Me.lblTariffNm.Name = "lblTariffNm"
        Me.lblTariffNm.ReadOnly = True
        Me.lblTariffNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffNm.Size = New System.Drawing.Size(535, 18)
        Me.lblTariffNm.TabIndex = 357
        Me.lblTariffNm.TabStop = False
        Me.lblTariffNm.TabStopSetting = False
        Me.lblTariffNm.TextValue = "XXXXXXXXX"
        Me.lblTariffNm.UseSystemPasswordChar = False
        Me.lblTariffNm.WidthDef = 535
        Me.lblTariffNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtTariffCd.Location = New System.Drawing.Point(653, 124)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(90, 18)
        Me.txtTariffCd.TabIndex = 356
        Me.txtTariffCd.TabStopSetting = True
        Me.txtTariffCd.TextValue = "XXXXXXXXX"
        Me.txtTariffCd.UseSystemPasswordChar = False
        Me.txtTariffCd.WidthDef = 90
        Me.txtTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblWarimashi
        '
        Me.lblWarimashi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWarimashi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWarimashi.CountWrappedLine = False
        Me.lblWarimashi.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblWarimashi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWarimashi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWarimashi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWarimashi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWarimashi.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblWarimashi.HeightDef = 18
        Me.lblWarimashi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWarimashi.HissuLabelVisible = False
        Me.lblWarimashi.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblWarimashi.IsByteCheck = 0
        Me.lblWarimashi.IsCalendarCheck = False
        Me.lblWarimashi.IsDakutenCheck = False
        Me.lblWarimashi.IsEisuCheck = False
        Me.lblWarimashi.IsForbiddenWordsCheck = False
        Me.lblWarimashi.IsFullByteCheck = 0
        Me.lblWarimashi.IsHankakuCheck = False
        Me.lblWarimashi.IsHissuCheck = False
        Me.lblWarimashi.IsKanaCheck = False
        Me.lblWarimashi.IsMiddleSpace = False
        Me.lblWarimashi.IsNumericCheck = False
        Me.lblWarimashi.IsSujiCheck = False
        Me.lblWarimashi.IsZenkakuCheck = False
        Me.lblWarimashi.ItemName = ""
        Me.lblWarimashi.LineSpace = 0
        Me.lblWarimashi.Location = New System.Drawing.Point(727, 145)
        Me.lblWarimashi.MaxLength = 0
        Me.lblWarimashi.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblWarimashi.MaxLineCount = 0
        Me.lblWarimashi.Multiline = False
        Me.lblWarimashi.Name = "lblWarimashi"
        Me.lblWarimashi.ReadOnly = True
        Me.lblWarimashi.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblWarimashi.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblWarimashi.Size = New System.Drawing.Size(535, 18)
        Me.lblWarimashi.TabIndex = 359
        Me.lblWarimashi.TabStop = False
        Me.lblWarimashi.TabStopSetting = False
        Me.lblWarimashi.TextValue = "XXXXXXXXX"
        Me.lblWarimashi.UseSystemPasswordChar = False
        Me.lblWarimashi.WidthDef = 535
        Me.lblWarimashi.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtWarimashi
        '
        Me.txtWarimashi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWarimashi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWarimashi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWarimashi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtWarimashi.CountWrappedLine = False
        Me.txtWarimashi.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtWarimashi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarimashi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtWarimashi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarimashi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarimashi.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtWarimashi.HeightDef = 18
        Me.txtWarimashi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtWarimashi.HissuLabelVisible = False
        Me.txtWarimashi.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtWarimashi.IsByteCheck = 10
        Me.txtWarimashi.IsCalendarCheck = False
        Me.txtWarimashi.IsDakutenCheck = False
        Me.txtWarimashi.IsEisuCheck = False
        Me.txtWarimashi.IsForbiddenWordsCheck = False
        Me.txtWarimashi.IsFullByteCheck = 0
        Me.txtWarimashi.IsHankakuCheck = False
        Me.txtWarimashi.IsHissuCheck = False
        Me.txtWarimashi.IsKanaCheck = False
        Me.txtWarimashi.IsMiddleSpace = False
        Me.txtWarimashi.IsNumericCheck = False
        Me.txtWarimashi.IsSujiCheck = False
        Me.txtWarimashi.IsZenkakuCheck = False
        Me.txtWarimashi.ItemName = ""
        Me.txtWarimashi.LineSpace = 0
        Me.txtWarimashi.Location = New System.Drawing.Point(653, 145)
        Me.txtWarimashi.MaxLength = 10
        Me.txtWarimashi.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtWarimashi.MaxLineCount = 0
        Me.txtWarimashi.Multiline = False
        Me.txtWarimashi.Name = "txtWarimashi"
        Me.txtWarimashi.ReadOnly = False
        Me.txtWarimashi.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtWarimashi.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtWarimashi.Size = New System.Drawing.Size(90, 18)
        Me.txtWarimashi.TabIndex = 358
        Me.txtWarimashi.TabStopSetting = True
        Me.txtWarimashi.TextValue = "XXXXXXXXX"
        Me.txtWarimashi.UseSystemPasswordChar = False
        Me.txtWarimashi.WidthDef = 90
        Me.txtWarimashi.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnKeisan
        '
        Me.btnKeisan.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKeisan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnKeisan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnKeisan.EnableStatus = True
        Me.btnKeisan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKeisan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKeisan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKeisan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnKeisan.HeightDef = 22
        Me.btnKeisan.Location = New System.Drawing.Point(24, 234)
        Me.btnKeisan.Name = "btnKeisan"
        Me.btnKeisan.Size = New System.Drawing.Size(91, 22)
        Me.btnKeisan.TabIndex = 360
        Me.btnKeisan.TabStopSetting = True
        Me.btnKeisan.Text = "再計算"
        Me.btnKeisan.TextValue = "再計算"
        Me.btnKeisan.UseVisualStyleBackColor = True
        Me.btnKeisan.WidthDef = 91
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1119, 10)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 362
        Me.lblSituation.TabStop = False
        '
        'lblTitleKg1
        '
        Me.lblTitleKg1.AutoSize = True
        Me.lblTitleKg1.AutoSizeDef = True
        Me.lblTitleKg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg1.EnableStatus = False
        Me.lblTitleKg1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg1.HeightDef = 13
        Me.lblTitleKg1.Location = New System.Drawing.Point(491, 148)
        Me.lblTitleKg1.Name = "lblTitleKg1"
        Me.lblTitleKg1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg1.TabIndex = 363
        Me.lblTitleKg1.Text = "KG"
        Me.lblTitleKg1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg1.TextValue = "KG"
        Me.lblTitleKg1.WidthDef = 21
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
        Me.lblTitleKm.Location = New System.Drawing.Point(491, 127)
        Me.lblTitleKm.Name = "lblTitleKm"
        Me.lblTitleKm.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKm.TabIndex = 364
        Me.lblTitleKm.Text = "KM"
        Me.lblTitleKm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKm.TextValue = "KM"
        Me.lblTitleKm.WidthDef = 21
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
        Me.sprDetail.HeightDef = 604
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(25, 262)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        Me.sprDetail.Size = New System.Drawing.Size(1229, 604)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 365
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1229
        '
        'imdArr
        '
        Me.imdArr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdArr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdArr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdArr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdArr.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdArr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdArr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdArr.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdArr.HeightDef = 18
        Me.imdArr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdArr.HissuLabelVisible = False
        Me.imdArr.Holiday = False
        Me.imdArr.IsAfterDateCheck = False
        Me.imdArr.IsBeforeDateCheck = False
        Me.imdArr.IsHissuCheck = False
        Me.imdArr.IsMinDateCheck = "1900/01/01"
        Me.imdArr.ItemName = ""
        Me.imdArr.Location = New System.Drawing.Point(98, 62)
        Me.imdArr.Name = "imdArr"
        Me.imdArr.Number = CType(10101000000, Long)
        Me.imdArr.ReadOnly = True
        Me.imdArr.Size = New System.Drawing.Size(118, 18)
        Me.imdArr.TabIndex = 366
        Me.imdArr.TabStop = False
        Me.imdArr.TabStopSetting = False
        Me.imdArr.TextValue = ""
        Me.imdArr.Value = New Date(CType(0, Long))
        Me.imdArr.WidthDef = 118
        '
        'lblTitleArr
        '
        Me.lblTitleArr.AutoSize = True
        Me.lblTitleArr.AutoSizeDef = True
        Me.lblTitleArr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleArr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleArr.EnableStatus = False
        Me.lblTitleArr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleArr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleArr.HeightDef = 13
        Me.lblTitleArr.Location = New System.Drawing.Point(49, 65)
        Me.lblTitleArr.Name = "lblTitleArr"
        Me.lblTitleArr.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleArr.TabIndex = 367
        Me.lblTitleArr.Text = "納入日"
        Me.lblTitleArr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleArr.TextValue = "納入日"
        Me.lblTitleArr.WidthDef = 49
        '
        'lblTitleKanriNo
        '
        Me.lblTitleKanriNo.AutoSize = True
        Me.lblTitleKanriNo.AutoSizeDef = True
        Me.lblTitleKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKanriNo.EnableStatus = False
        Me.lblTitleKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKanriNo.HeightDef = 13
        Me.lblTitleKanriNo.Location = New System.Drawing.Point(590, 192)
        Me.lblTitleKanriNo.Name = "lblTitleKanriNo"
        Me.lblTitleKanriNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleKanriNo.TabIndex = 369
        Me.lblTitleKanriNo.Text = "管理番号"
        Me.lblTitleKanriNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKanriNo.TextValue = "管理番号"
        Me.lblTitleKanriNo.WidthDef = 63
        '
        'lblTitleUnsoNo
        '
        Me.lblTitleUnsoNo.AutoSize = True
        Me.lblTitleUnsoNo.AutoSizeDef = True
        Me.lblTitleUnsoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoNo.EnableStatus = False
        Me.lblTitleUnsoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNo.HeightDef = 13
        Me.lblTitleUnsoNo.Location = New System.Drawing.Point(590, 170)
        Me.lblTitleUnsoNo.Name = "lblTitleUnsoNo"
        Me.lblTitleUnsoNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnsoNo.TabIndex = 368
        Me.lblTitleUnsoNo.Text = "運送番号"
        Me.lblTitleUnsoNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoNo.TextValue = "運送番号"
        Me.lblTitleUnsoNo.WidthDef = 63
        '
        'txtUnsoNo
        '
        Me.txtUnsoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsoNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsoNo.CountWrappedLine = False
        Me.txtUnsoNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsoNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsoNo.HeightDef = 18
        Me.txtUnsoNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsoNo.HissuLabelVisible = False
        Me.txtUnsoNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtUnsoNo.IsByteCheck = 9
        Me.txtUnsoNo.IsCalendarCheck = False
        Me.txtUnsoNo.IsDakutenCheck = False
        Me.txtUnsoNo.IsEisuCheck = False
        Me.txtUnsoNo.IsForbiddenWordsCheck = False
        Me.txtUnsoNo.IsFullByteCheck = 0
        Me.txtUnsoNo.IsHankakuCheck = False
        Me.txtUnsoNo.IsHissuCheck = False
        Me.txtUnsoNo.IsKanaCheck = False
        Me.txtUnsoNo.IsMiddleSpace = False
        Me.txtUnsoNo.IsNumericCheck = False
        Me.txtUnsoNo.IsSujiCheck = False
        Me.txtUnsoNo.IsZenkakuCheck = False
        Me.txtUnsoNo.ItemName = ""
        Me.txtUnsoNo.LineSpace = 0
        Me.txtUnsoNo.Location = New System.Drawing.Point(653, 167)
        Me.txtUnsoNo.MaxLength = 9
        Me.txtUnsoNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsoNo.MaxLineCount = 0
        Me.txtUnsoNo.Multiline = False
        Me.txtUnsoNo.Name = "txtUnsoNo"
        Me.txtUnsoNo.ReadOnly = True
        Me.txtUnsoNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsoNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsoNo.Size = New System.Drawing.Size(103, 18)
        Me.txtUnsoNo.TabIndex = 374
        Me.txtUnsoNo.TabStop = False
        Me.txtUnsoNo.TabStopSetting = False
        Me.txtUnsoNo.TextValue = "XXXXXXXXX"
        Me.txtUnsoNo.UseSystemPasswordChar = False
        Me.txtUnsoNo.WidthDef = 103
        Me.txtUnsoNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtKanriNo
        '
        Me.txtKanriNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKanriNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKanriNo.CountWrappedLine = False
        Me.txtKanriNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKanriNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKanriNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKanriNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKanriNo.HeightDef = 18
        Me.txtKanriNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKanriNo.HissuLabelVisible = False
        Me.txtKanriNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtKanriNo.IsByteCheck = 9
        Me.txtKanriNo.IsCalendarCheck = False
        Me.txtKanriNo.IsDakutenCheck = False
        Me.txtKanriNo.IsEisuCheck = False
        Me.txtKanriNo.IsForbiddenWordsCheck = False
        Me.txtKanriNo.IsFullByteCheck = 0
        Me.txtKanriNo.IsHankakuCheck = False
        Me.txtKanriNo.IsHissuCheck = False
        Me.txtKanriNo.IsKanaCheck = False
        Me.txtKanriNo.IsMiddleSpace = False
        Me.txtKanriNo.IsNumericCheck = False
        Me.txtKanriNo.IsSujiCheck = False
        Me.txtKanriNo.IsZenkakuCheck = False
        Me.txtKanriNo.ItemName = ""
        Me.txtKanriNo.LineSpace = 0
        Me.txtKanriNo.Location = New System.Drawing.Point(653, 189)
        Me.txtKanriNo.MaxLength = 9
        Me.txtKanriNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKanriNo.MaxLineCount = 0
        Me.txtKanriNo.Multiline = False
        Me.txtKanriNo.Name = "txtKanriNo"
        Me.txtKanriNo.ReadOnly = True
        Me.txtKanriNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKanriNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKanriNo.Size = New System.Drawing.Size(103, 18)
        Me.txtKanriNo.TabIndex = 375
        Me.txtKanriNo.TabStop = False
        Me.txtKanriNo.TabStopSetting = False
        Me.txtKanriNo.TextValue = "XXXXXXXXX"
        Me.txtKanriNo.UseSystemPasswordChar = False
        Me.txtKanriNo.WidthDef = 103
        Me.txtKanriNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMF090F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMF090F"
        Me.Text = "【LMF090】運賃編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents imdShukka As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleShukka As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblUnsoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblUnsoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnso As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleJuryo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKyori As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleNisugata As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleShashu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKiken As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTehai As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKosu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleWarimashi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbNisugata As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbShashu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbKiken As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbTariffKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents numKosu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numJuryo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents numKyori As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblWarimashi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtWarimashi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnKeisan As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblTitleKm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents imdArr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblTitleArr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKanriNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnsoNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKanriNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsoNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
End Class
