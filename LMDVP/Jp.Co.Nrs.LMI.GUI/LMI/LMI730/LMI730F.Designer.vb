<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI730F
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
        Dim DateYearDisplayField9 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField17 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField9 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField18 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField9 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField9 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField9 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField9 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField10 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField19 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField10 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField20 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField10 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField10 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField10 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField10 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.pnlCondition = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleDest = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlConditionKbn = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.pnlRev = New System.Windows.Forms.Panel()
        Me.optRevRyoho = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optRevKaku = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optRevMi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleRev = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKara = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdTo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.imdFrom = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.cmbDateKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.pnlHenko = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtShuseiSS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtShuseiS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnHenko = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbShusei = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtShuseiM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtShuseiL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleHenko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSokei = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleYen = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSokeithi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.lblOrderBy = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCalcKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlCondition.SuspendLayout()
        Me.pnlConditionKbn.SuspendLayout()
        Me.pnlRev.SuspendLayout()
        Me.pnlHenko.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblCalcKbn)
        Me.pnlViewAria.Controls.Add(Me.lblOrderBy)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleYen)
        Me.pnlViewAria.Controls.Add(Me.numSokeithi)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSokei)
        Me.pnlViewAria.Controls.Add(Me.pnlHenko)
        Me.pnlViewAria.Controls.Add(Me.pnlCondition)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'pnlCondition
        '
        Me.pnlCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlCondition.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlCondition.Controls.Add(Me.lblTitleDest)
        Me.pnlCondition.Controls.Add(Me.lblDestNm)
        Me.pnlCondition.Controls.Add(Me.txtDestCd)
        Me.pnlCondition.Controls.Add(Me.pnlConditionKbn)
        Me.pnlCondition.Controls.Add(Me.lblCustNm)
        Me.pnlCondition.Controls.Add(Me.txtCustCdM)
        Me.pnlCondition.Controls.Add(Me.txtCustCdL)
        Me.pnlCondition.Controls.Add(Me.lblTitleCust)
        Me.pnlCondition.Controls.Add(Me.lblTitleKara)
        Me.pnlCondition.Controls.Add(Me.lblTariffNm)
        Me.pnlCondition.Controls.Add(Me.txtTariffCd)
        Me.pnlCondition.Controls.Add(Me.lblTitleTariff)
        Me.pnlCondition.Controls.Add(Me.imdTo)
        Me.pnlCondition.Controls.Add(Me.imdFrom)
        Me.pnlCondition.Controls.Add(Me.cmbDateKb)
        Me.pnlCondition.EnableStatus = False
        Me.pnlCondition.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.HeightDef = 116
        Me.pnlCondition.Location = New System.Drawing.Point(16, 45)
        Me.pnlCondition.Name = "pnlCondition"
        Me.pnlCondition.Size = New System.Drawing.Size(1243, 116)
        Me.pnlCondition.TabIndex = 112
        Me.pnlCondition.TabStop = False
        Me.pnlCondition.Text = "抽出条件"
        Me.pnlCondition.TextValue = "抽出条件"
        Me.pnlCondition.WidthDef = 1243
        '
        'lblTitleDest
        '
        Me.lblTitleDest.AutoSizeDef = False
        Me.lblTitleDest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDest.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDest.EnableStatus = False
        Me.lblTitleDest.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDest.HeightDef = 13
        Me.lblTitleDest.Location = New System.Drawing.Point(43, 88)
        Me.lblTitleDest.Name = "lblTitleDest"
        Me.lblTitleDest.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleDest.TabIndex = 347
        Me.lblTitleDest.Text = "届先"
        Me.lblTitleDest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDest.TextValue = "届先"
        Me.lblTitleDest.WidthDef = 70
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
        Me.lblDestNm.HissuLabelVisible = False
        Me.lblDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDestNm.IsByteCheck = 0
        Me.lblDestNm.IsCalendarCheck = False
        Me.lblDestNm.IsDakutenCheck = False
        Me.lblDestNm.IsEisuCheck = False
        Me.lblDestNm.IsForbiddenWordsCheck = False
        Me.lblDestNm.IsFullByteCheck = 0
        Me.lblDestNm.IsHankakuCheck = False
        Me.lblDestNm.IsHissuCheck = False
        Me.lblDestNm.IsKanaCheck = False
        Me.lblDestNm.IsMiddleSpace = False
        Me.lblDestNm.IsNumericCheck = False
        Me.lblDestNm.IsSujiCheck = False
        Me.lblDestNm.IsZenkakuCheck = False
        Me.lblDestNm.ItemName = ""
        Me.lblDestNm.LineSpace = 0
        Me.lblDestNm.Location = New System.Drawing.Point(220, 88)
        Me.lblDestNm.MaxLength = 0
        Me.lblDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDestNm.MaxLineCount = 0
        Me.lblDestNm.Multiline = False
        Me.lblDestNm.Name = "lblDestNm"
        Me.lblDestNm.ReadOnly = True
        Me.lblDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDestNm.Size = New System.Drawing.Size(473, 18)
        Me.lblDestNm.TabIndex = 345
        Me.lblDestNm.TabStop = False
        Me.lblDestNm.TabStopSetting = False
        Me.lblDestNm.TextValue = "XXXXXXXXX"
        Me.lblDestNm.UseSystemPasswordChar = False
        Me.lblDestNm.WidthDef = 473
        Me.lblDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtDestCd.HissuLabelVisible = False
        Me.txtDestCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtDestCd.IsByteCheck = 15
        Me.txtDestCd.IsCalendarCheck = False
        Me.txtDestCd.IsDakutenCheck = False
        Me.txtDestCd.IsEisuCheck = False
        Me.txtDestCd.IsForbiddenWordsCheck = False
        Me.txtDestCd.IsFullByteCheck = 0
        Me.txtDestCd.IsHankakuCheck = False
        Me.txtDestCd.IsHissuCheck = False
        Me.txtDestCd.IsKanaCheck = False
        Me.txtDestCd.IsMiddleSpace = False
        Me.txtDestCd.IsNumericCheck = False
        Me.txtDestCd.IsSujiCheck = False
        Me.txtDestCd.IsZenkakuCheck = False
        Me.txtDestCd.ItemName = ""
        Me.txtDestCd.LineSpace = 0
        Me.txtDestCd.Location = New System.Drawing.Point(113, 88)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(125, 18)
        Me.txtDestCd.TabIndex = 344
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = "XXXXXXXXXXXXXXX"
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 125
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlConditionKbn
        '
        Me.pnlConditionKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlConditionKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlConditionKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlConditionKbn.Controls.Add(Me.pnlRev)
        Me.pnlConditionKbn.EnableStatus = False
        Me.pnlConditionKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlConditionKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlConditionKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlConditionKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlConditionKbn.HeightDef = 81
        Me.pnlConditionKbn.Location = New System.Drawing.Point(763, 25)
        Me.pnlConditionKbn.Name = "pnlConditionKbn"
        Me.pnlConditionKbn.Size = New System.Drawing.Size(471, 81)
        Me.pnlConditionKbn.TabIndex = 335
        Me.pnlConditionKbn.TabStop = False
        Me.pnlConditionKbn.Text = "検索区分"
        Me.pnlConditionKbn.TextValue = "検索区分"
        Me.pnlConditionKbn.WidthDef = 471
        '
        'pnlRev
        '
        Me.pnlRev.Controls.Add(Me.optRevRyoho)
        Me.pnlRev.Controls.Add(Me.optRevKaku)
        Me.pnlRev.Controls.Add(Me.optRevMi)
        Me.pnlRev.Controls.Add(Me.lblTitleRev)
        Me.pnlRev.Location = New System.Drawing.Point(6, 30)
        Me.pnlRev.Name = "pnlRev"
        Me.pnlRev.Size = New System.Drawing.Size(442, 27)
        Me.pnlRev.TabIndex = 2
        '
        'optRevRyoho
        '
        Me.optRevRyoho.AutoSize = True
        Me.optRevRyoho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevRyoho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevRyoho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optRevRyoho.EnableStatus = True
        Me.optRevRyoho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevRyoho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevRyoho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevRyoho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevRyoho.HeightDef = 17
        Me.optRevRyoho.Location = New System.Drawing.Point(285, 5)
        Me.optRevRyoho.Name = "optRevRyoho"
        Me.optRevRyoho.Size = New System.Drawing.Size(53, 17)
        Me.optRevRyoho.TabIndex = 314
        Me.optRevRyoho.TabStop = True
        Me.optRevRyoho.TabStopSetting = True
        Me.optRevRyoho.Text = "両方"
        Me.optRevRyoho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optRevRyoho.TextValue = "両方"
        Me.optRevRyoho.UseVisualStyleBackColor = True
        Me.optRevRyoho.WidthDef = 53
        '
        'optRevKaku
        '
        Me.optRevKaku.AutoSize = True
        Me.optRevKaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevKaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevKaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optRevKaku.EnableStatus = True
        Me.optRevKaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevKaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevKaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevKaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevKaku.HeightDef = 17
        Me.optRevKaku.Location = New System.Drawing.Point(198, 5)
        Me.optRevKaku.Name = "optRevKaku"
        Me.optRevKaku.Size = New System.Drawing.Size(53, 17)
        Me.optRevKaku.TabIndex = 313
        Me.optRevKaku.TabStop = True
        Me.optRevKaku.TabStopSetting = True
        Me.optRevKaku.Text = "確定"
        Me.optRevKaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optRevKaku.TextValue = "確定"
        Me.optRevKaku.UseVisualStyleBackColor = True
        Me.optRevKaku.WidthDef = 53
        '
        'optRevMi
        '
        Me.optRevMi.AutoSize = True
        Me.optRevMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevMi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optRevMi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optRevMi.EnableStatus = True
        Me.optRevMi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevMi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optRevMi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevMi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optRevMi.HeightDef = 17
        Me.optRevMi.Location = New System.Drawing.Point(110, 5)
        Me.optRevMi.Name = "optRevMi"
        Me.optRevMi.Size = New System.Drawing.Size(39, 17)
        Me.optRevMi.TabIndex = 312
        Me.optRevMi.TabStop = True
        Me.optRevMi.TabStopSetting = True
        Me.optRevMi.Text = "未"
        Me.optRevMi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optRevMi.TextValue = "未"
        Me.optRevMi.UseVisualStyleBackColor = True
        Me.optRevMi.WidthDef = 39
        '
        'lblTitleRev
        '
        Me.lblTitleRev.AutoSizeDef = False
        Me.lblTitleRev.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRev.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRev.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRev.EnableStatus = False
        Me.lblTitleRev.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRev.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRev.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRev.HeightDef = 13
        Me.lblTitleRev.Location = New System.Drawing.Point(5, 7)
        Me.lblTitleRev.Name = "lblTitleRev"
        Me.lblTitleRev.Size = New System.Drawing.Size(92, 13)
        Me.lblTitleRev.TabIndex = 311
        Me.lblTitleRev.Text = "請求運賃確定"
        Me.lblTitleRev.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRev.TextValue = "請求運賃確定"
        Me.lblTitleRev.WidthDef = 92
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
        Me.lblCustNm.Location = New System.Drawing.Point(220, 67)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNm.TabIndex = 333
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 473
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
        Me.txtCustCdM.Location = New System.Drawing.Point(182, 67)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(56, 18)
        Me.txtCustCdM.TabIndex = 332
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = "XX"
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 56
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
        Me.txtCustCdL.Location = New System.Drawing.Point(113, 67)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(85, 18)
        Me.txtCustCdL.TabIndex = 334
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "XXXXX"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 85
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleCust
        '
        Me.lblTitleCust.AutoSizeDef = False
        Me.lblTitleCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCust.EnableStatus = False
        Me.lblTitleCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCust.HeightDef = 13
        Me.lblTitleCust.Location = New System.Drawing.Point(40, 67)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(73, 13)
        Me.lblTitleCust.TabIndex = 331
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 73
        '
        'lblTitleKara
        '
        Me.lblTitleKara.AutoSize = True
        Me.lblTitleKara.AutoSizeDef = True
        Me.lblTitleKara.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKara.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKara.EnableStatus = False
        Me.lblTitleKara.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKara.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKara.HeightDef = 13
        Me.lblTitleKara.Location = New System.Drawing.Point(233, 25)
        Me.lblTitleKara.Name = "lblTitleKara"
        Me.lblTitleKara.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKara.TabIndex = 329
        Me.lblTitleKara.Text = "～"
        Me.lblTitleKara.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKara.TextValue = "～"
        Me.lblTitleKara.WidthDef = 21
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
        Me.lblTariffNm.Location = New System.Drawing.Point(220, 46)
        Me.lblTariffNm.MaxLength = 0
        Me.lblTariffNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffNm.MaxLineCount = 0
        Me.lblTariffNm.Multiline = False
        Me.lblTariffNm.Name = "lblTariffNm"
        Me.lblTariffNm.ReadOnly = True
        Me.lblTariffNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffNm.Size = New System.Drawing.Size(473, 18)
        Me.lblTariffNm.TabIndex = 323
        Me.lblTariffNm.TabStop = False
        Me.lblTariffNm.TabStopSetting = False
        Me.lblTariffNm.TextValue = "XXXXXXXXX"
        Me.lblTariffNm.UseSystemPasswordChar = False
        Me.lblTariffNm.WidthDef = 473
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
        Me.txtTariffCd.Location = New System.Drawing.Point(113, 46)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(125, 18)
        Me.txtTariffCd.TabIndex = 322
        Me.txtTariffCd.TabStopSetting = True
        Me.txtTariffCd.TextValue = "XXXXXXXXX"
        Me.txtTariffCd.UseSystemPasswordChar = False
        Me.txtTariffCd.WidthDef = 125
        Me.txtTariffCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTariff
        '
        Me.lblTitleTariff.AutoSizeDef = False
        Me.lblTitleTariff.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariff.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariff.EnableStatus = False
        Me.lblTitleTariff.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariff.HeightDef = 13
        Me.lblTitleTariff.Location = New System.Drawing.Point(36, 47)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariff.TabIndex = 321
        Me.lblTitleTariff.Text = "運賃タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "運賃タリフ"
        Me.lblTitleTariff.WidthDef = 77
        '
        'imdTo
        '
        Me.imdTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField9.ShowLeadingZero = True
        DateLiteralDisplayField17.Text = "/"
        DateMonthDisplayField9.ShowLeadingZero = True
        DateLiteralDisplayField18.Text = "/"
        DateDayDisplayField9.ShowLeadingZero = True
        Me.imdTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField9, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField17, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField9, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField18, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField9, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField9, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField9, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField9, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdTo.HeightDef = 18
        Me.imdTo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdTo.HissuLabelVisible = False
        Me.imdTo.Holiday = True
        Me.imdTo.IsAfterDateCheck = False
        Me.imdTo.IsBeforeDateCheck = False
        Me.imdTo.IsHissuCheck = False
        Me.imdTo.IsMinDateCheck = "1900/01/01"
        Me.imdTo.ItemName = ""
        Me.imdTo.Location = New System.Drawing.Point(260, 22)
        Me.imdTo.Name = "imdTo"
        Me.imdTo.Number = CType(0, Long)
        Me.imdTo.ReadOnly = False
        Me.imdTo.Size = New System.Drawing.Size(125, 18)
        Me.imdTo.TabIndex = 330
        Me.imdTo.TabStopSetting = True
        Me.imdTo.TextValue = ""
        Me.imdTo.Value = New Date(CType(0, Long))
        Me.imdTo.WidthDef = 125
        '
        'imdFrom
        '
        Me.imdFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFrom.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdFrom.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField10.ShowLeadingZero = True
        DateLiteralDisplayField19.Text = "/"
        DateMonthDisplayField10.ShowLeadingZero = True
        DateLiteralDisplayField20.Text = "/"
        DateDayDisplayField10.ShowLeadingZero = True
        Me.imdFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField10, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField19, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField10, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField20, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField10, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField10, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField10, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField10, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdFrom.HeightDef = 18
        Me.imdFrom.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdFrom.HissuLabelVisible = False
        Me.imdFrom.Holiday = True
        Me.imdFrom.IsAfterDateCheck = False
        Me.imdFrom.IsBeforeDateCheck = False
        Me.imdFrom.IsHissuCheck = False
        Me.imdFrom.IsMinDateCheck = "1900/01/01"
        Me.imdFrom.ItemName = ""
        Me.imdFrom.Location = New System.Drawing.Point(113, 22)
        Me.imdFrom.Name = "imdFrom"
        Me.imdFrom.Number = CType(0, Long)
        Me.imdFrom.ReadOnly = False
        Me.imdFrom.Size = New System.Drawing.Size(125, 18)
        Me.imdFrom.TabIndex = 328
        Me.imdFrom.TabStopSetting = True
        Me.imdFrom.TextValue = ""
        Me.imdFrom.Value = New Date(CType(0, Long))
        Me.imdFrom.WidthDef = 125
        '
        'cmbDateKb
        '
        Me.cmbDateKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDateKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbDateKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbDateKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbDateKb.DataCode = "H014"
        Me.cmbDateKb.DataSource = Nothing
        Me.cmbDateKb.DisplayMember = Nothing
        Me.cmbDateKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDateKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbDateKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDateKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbDateKb.HeightDef = 18
        Me.cmbDateKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbDateKb.HissuLabelVisible = False
        Me.cmbDateKb.InsertWildCard = True
        Me.cmbDateKb.IsForbiddenWordsCheck = False
        Me.cmbDateKb.IsHissuCheck = False
        Me.cmbDateKb.ItemName = ""
        Me.cmbDateKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbDateKb.Location = New System.Drawing.Point(39, 22)
        Me.cmbDateKb.Name = "cmbDateKb"
        Me.cmbDateKb.ReadOnly = False
        Me.cmbDateKb.SelectedIndex = -1
        Me.cmbDateKb.SelectedItem = Nothing
        Me.cmbDateKb.SelectedText = ""
        Me.cmbDateKb.SelectedValue = ""
        Me.cmbDateKb.Size = New System.Drawing.Size(90, 18)
        Me.cmbDateKb.TabIndex = 324
        Me.cmbDateKb.TabStopSetting = True
        Me.cmbDateKb.TextValue = ""
        Me.cmbDateKb.Value1 = "= '1.000'"
        Me.cmbDateKb.Value2 = Nothing
        Me.cmbDateKb.Value3 = Nothing
        Me.cmbDateKb.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V1
        Me.cmbDateKb.ValueMember = Nothing
        Me.cmbDateKb.WidthDef = 90
        '
        'pnlHenko
        '
        Me.pnlHenko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHenko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlHenko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlHenko.Controls.Add(Me.txtShuseiSS)
        Me.pnlHenko.Controls.Add(Me.txtShuseiS)
        Me.pnlHenko.Controls.Add(Me.btnHenko)
        Me.pnlHenko.Controls.Add(Me.cmbShusei)
        Me.pnlHenko.Controls.Add(Me.txtShuseiM)
        Me.pnlHenko.Controls.Add(Me.txtShuseiL)
        Me.pnlHenko.Controls.Add(Me.lblTitleHenko)
        Me.pnlHenko.EnableStatus = False
        Me.pnlHenko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHenko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlHenko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHenko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlHenko.HeightDef = 50
        Me.pnlHenko.Location = New System.Drawing.Point(16, 171)
        Me.pnlHenko.Name = "pnlHenko"
        Me.pnlHenko.Size = New System.Drawing.Size(575, 50)
        Me.pnlHenko.TabIndex = 299
        Me.pnlHenko.TabStop = False
        Me.pnlHenko.Text = "一覧変更"
        Me.pnlHenko.TextValue = "一覧変更"
        Me.pnlHenko.WidthDef = 575
        '
        'txtShuseiSS
        '
        Me.txtShuseiSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiSS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiSS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShuseiSS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShuseiSS.CountWrappedLine = False
        Me.txtShuseiSS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShuseiSS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiSS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiSS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiSS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiSS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShuseiSS.HeightDef = 18
        Me.txtShuseiSS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShuseiSS.HissuLabelVisible = False
        Me.txtShuseiSS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtShuseiSS.IsByteCheck = 2
        Me.txtShuseiSS.IsCalendarCheck = False
        Me.txtShuseiSS.IsDakutenCheck = False
        Me.txtShuseiSS.IsEisuCheck = False
        Me.txtShuseiSS.IsForbiddenWordsCheck = False
        Me.txtShuseiSS.IsFullByteCheck = 0
        Me.txtShuseiSS.IsHankakuCheck = False
        Me.txtShuseiSS.IsHissuCheck = False
        Me.txtShuseiSS.IsKanaCheck = False
        Me.txtShuseiSS.IsMiddleSpace = False
        Me.txtShuseiSS.IsNumericCheck = False
        Me.txtShuseiSS.IsSujiCheck = False
        Me.txtShuseiSS.IsZenkakuCheck = False
        Me.txtShuseiSS.ItemName = ""
        Me.txtShuseiSS.LineSpace = 0
        Me.txtShuseiSS.Location = New System.Drawing.Point(421, 20)
        Me.txtShuseiSS.MaxLength = 2
        Me.txtShuseiSS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShuseiSS.MaxLineCount = 0
        Me.txtShuseiSS.Multiline = False
        Me.txtShuseiSS.Name = "txtShuseiSS"
        Me.txtShuseiSS.ReadOnly = False
        Me.txtShuseiSS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShuseiSS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShuseiSS.Size = New System.Drawing.Size(46, 18)
        Me.txtShuseiSS.TabIndex = 324
        Me.txtShuseiSS.TabStopSetting = True
        Me.txtShuseiSS.TextValue = "XX"
        Me.txtShuseiSS.UseSystemPasswordChar = False
        Me.txtShuseiSS.WidthDef = 46
        Me.txtShuseiSS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtShuseiS
        '
        Me.txtShuseiS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShuseiS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShuseiS.CountWrappedLine = False
        Me.txtShuseiS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShuseiS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShuseiS.HeightDef = 18
        Me.txtShuseiS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShuseiS.HissuLabelVisible = False
        Me.txtShuseiS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtShuseiS.IsByteCheck = 2
        Me.txtShuseiS.IsCalendarCheck = False
        Me.txtShuseiS.IsDakutenCheck = False
        Me.txtShuseiS.IsEisuCheck = False
        Me.txtShuseiS.IsForbiddenWordsCheck = False
        Me.txtShuseiS.IsFullByteCheck = 0
        Me.txtShuseiS.IsHankakuCheck = False
        Me.txtShuseiS.IsHissuCheck = False
        Me.txtShuseiS.IsKanaCheck = False
        Me.txtShuseiS.IsMiddleSpace = False
        Me.txtShuseiS.IsNumericCheck = False
        Me.txtShuseiS.IsSujiCheck = False
        Me.txtShuseiS.IsZenkakuCheck = False
        Me.txtShuseiS.ItemName = ""
        Me.txtShuseiS.LineSpace = 0
        Me.txtShuseiS.Location = New System.Drawing.Point(391, 20)
        Me.txtShuseiS.MaxLength = 2
        Me.txtShuseiS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShuseiS.MaxLineCount = 0
        Me.txtShuseiS.Multiline = False
        Me.txtShuseiS.Name = "txtShuseiS"
        Me.txtShuseiS.ReadOnly = False
        Me.txtShuseiS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShuseiS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShuseiS.Size = New System.Drawing.Size(46, 18)
        Me.txtShuseiS.TabIndex = 323
        Me.txtShuseiS.TabStopSetting = True
        Me.txtShuseiS.TextValue = "XX"
        Me.txtShuseiS.UseSystemPasswordChar = False
        Me.txtShuseiS.WidthDef = 46
        Me.txtShuseiS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'btnHenko
        '
        Me.btnHenko.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnHenko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnHenko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnHenko.EnableStatus = True
        Me.btnHenko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHenko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHenko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnHenko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnHenko.HeightDef = 22
        Me.btnHenko.Location = New System.Drawing.Point(469, 18)
        Me.btnHenko.Name = "btnHenko"
        Me.btnHenko.Size = New System.Drawing.Size(91, 22)
        Me.btnHenko.TabIndex = 322
        Me.btnHenko.TabStopSetting = True
        Me.btnHenko.Text = "一括変更"
        Me.btnHenko.TextValue = "一括変更"
        Me.btnHenko.UseVisualStyleBackColor = True
        Me.btnHenko.WidthDef = 91
        '
        'cmbShusei
        '
        Me.cmbShusei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShusei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShusei.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShusei.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShusei.DataCode = "S114"
        Me.cmbShusei.DataSource = Nothing
        Me.cmbShusei.DisplayMember = Nothing
        Me.cmbShusei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShusei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShusei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShusei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShusei.HeightDef = 18
        Me.cmbShusei.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShusei.HissuLabelVisible = False
        Me.cmbShusei.InsertWildCard = True
        Me.cmbShusei.IsForbiddenWordsCheck = False
        Me.cmbShusei.IsHissuCheck = False
        Me.cmbShusei.ItemName = ""
        Me.cmbShusei.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShusei.Location = New System.Drawing.Point(112, 19)
        Me.cmbShusei.Name = "cmbShusei"
        Me.cmbShusei.ReadOnly = False
        Me.cmbShusei.SelectedIndex = -1
        Me.cmbShusei.SelectedItem = Nothing
        Me.cmbShusei.SelectedText = ""
        Me.cmbShusei.SelectedValue = ""
        Me.cmbShusei.Size = New System.Drawing.Size(172, 18)
        Me.cmbShusei.TabIndex = 321
        Me.cmbShusei.TabStopSetting = True
        Me.cmbShusei.TextValue = ""
        Me.cmbShusei.Value1 = Nothing
        Me.cmbShusei.Value2 = "= '1.000'"
        Me.cmbShusei.Value3 = Nothing
        Me.cmbShusei.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V2
        Me.cmbShusei.ValueMember = Nothing
        Me.cmbShusei.WidthDef = 172
        '
        'txtShuseiM
        '
        Me.txtShuseiM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShuseiM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShuseiM.CountWrappedLine = False
        Me.txtShuseiM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShuseiM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShuseiM.HeightDef = 18
        Me.txtShuseiM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShuseiM.HissuLabelVisible = False
        Me.txtShuseiM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtShuseiM.IsByteCheck = 2
        Me.txtShuseiM.IsCalendarCheck = False
        Me.txtShuseiM.IsDakutenCheck = False
        Me.txtShuseiM.IsEisuCheck = False
        Me.txtShuseiM.IsForbiddenWordsCheck = False
        Me.txtShuseiM.IsFullByteCheck = 0
        Me.txtShuseiM.IsHankakuCheck = False
        Me.txtShuseiM.IsHissuCheck = False
        Me.txtShuseiM.IsKanaCheck = False
        Me.txtShuseiM.IsMiddleSpace = False
        Me.txtShuseiM.IsNumericCheck = False
        Me.txtShuseiM.IsSujiCheck = False
        Me.txtShuseiM.IsZenkakuCheck = False
        Me.txtShuseiM.ItemName = ""
        Me.txtShuseiM.LineSpace = 0
        Me.txtShuseiM.Location = New System.Drawing.Point(361, 20)
        Me.txtShuseiM.MaxLength = 2
        Me.txtShuseiM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShuseiM.MaxLineCount = 0
        Me.txtShuseiM.Multiline = False
        Me.txtShuseiM.Name = "txtShuseiM"
        Me.txtShuseiM.ReadOnly = False
        Me.txtShuseiM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShuseiM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShuseiM.Size = New System.Drawing.Size(46, 18)
        Me.txtShuseiM.TabIndex = 315
        Me.txtShuseiM.TabStopSetting = True
        Me.txtShuseiM.TextValue = "XX"
        Me.txtShuseiM.UseSystemPasswordChar = False
        Me.txtShuseiM.WidthDef = 46
        Me.txtShuseiM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtShuseiL
        '
        Me.txtShuseiL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShuseiL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShuseiL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtShuseiL.CountWrappedLine = False
        Me.txtShuseiL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtShuseiL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShuseiL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtShuseiL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtShuseiL.HeightDef = 18
        Me.txtShuseiL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtShuseiL.HissuLabelVisible = False
        Me.txtShuseiL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtShuseiL.IsByteCheck = 10
        Me.txtShuseiL.IsCalendarCheck = False
        Me.txtShuseiL.IsDakutenCheck = False
        Me.txtShuseiL.IsEisuCheck = False
        Me.txtShuseiL.IsForbiddenWordsCheck = False
        Me.txtShuseiL.IsFullByteCheck = 0
        Me.txtShuseiL.IsHankakuCheck = False
        Me.txtShuseiL.IsHissuCheck = False
        Me.txtShuseiL.IsKanaCheck = False
        Me.txtShuseiL.IsMiddleSpace = False
        Me.txtShuseiL.IsNumericCheck = False
        Me.txtShuseiL.IsSujiCheck = False
        Me.txtShuseiL.IsZenkakuCheck = False
        Me.txtShuseiL.ItemName = ""
        Me.txtShuseiL.LineSpace = 0
        Me.txtShuseiL.Location = New System.Drawing.Point(287, 20)
        Me.txtShuseiL.MaxLength = 10
        Me.txtShuseiL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtShuseiL.MaxLineCount = 0
        Me.txtShuseiL.Multiline = False
        Me.txtShuseiL.Name = "txtShuseiL"
        Me.txtShuseiL.ReadOnly = False
        Me.txtShuseiL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtShuseiL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtShuseiL.Size = New System.Drawing.Size(90, 18)
        Me.txtShuseiL.TabIndex = 317
        Me.txtShuseiL.TabStopSetting = True
        Me.txtShuseiL.TextValue = "XXXXXXXXX"
        Me.txtShuseiL.UseSystemPasswordChar = False
        Me.txtShuseiL.WidthDef = 90
        Me.txtShuseiL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleHenko
        '
        Me.lblTitleHenko.AutoSizeDef = False
        Me.lblTitleHenko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHenko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHenko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHenko.EnableStatus = False
        Me.lblTitleHenko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHenko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHenko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHenko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHenko.HeightDef = 13
        Me.lblTitleHenko.Location = New System.Drawing.Point(21, 23)
        Me.lblTitleHenko.Name = "lblTitleHenko"
        Me.lblTitleHenko.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleHenko.TabIndex = 314
        Me.lblTitleHenko.Text = "変更対象項目"
        Me.lblTitleHenko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHenko.TextValue = "変更対象項目"
        Me.lblTitleHenko.WidthDef = 91
        '
        'lblTitleSokei
        '
        Me.lblTitleSokei.AutoSizeDef = False
        Me.lblTitleSokei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSokei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSokei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSokei.EnableStatus = False
        Me.lblTitleSokei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSokei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSokei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSokei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSokei.HeightDef = 13
        Me.lblTitleSokei.Location = New System.Drawing.Point(938, 204)
        Me.lblTitleSokei.Name = "lblTitleSokei"
        Me.lblTitleSokei.Size = New System.Drawing.Size(133, 13)
        Me.lblTitleSokei.TabIndex = 339
        Me.lblTitleSokei.Text = "表示データ運賃総計"
        Me.lblTitleSokei.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSokei.TextValue = "表示データ運賃総計"
        Me.lblTitleSokei.WidthDef = 133
        '
        'lblTitleYen
        '
        Me.lblTitleYen.AutoSize = True
        Me.lblTitleYen.AutoSizeDef = True
        Me.lblTitleYen.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen.EnableStatus = False
        Me.lblTitleYen.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen.HeightDef = 13
        Me.lblTitleYen.Location = New System.Drawing.Point(1216, 205)
        Me.lblTitleYen.Name = "lblTitleYen"
        Me.lblTitleYen.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen.TabIndex = 340
        Me.lblTitleYen.Text = "円"
        Me.lblTitleYen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen.TextValue = "円"
        Me.lblTitleYen.WidthDef = 21
        '
        'numSokeithi
        '
        Me.numSokeithi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSokeithi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSokeithi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSokeithi.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSokeithi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSokeithi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSokeithi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSokeithi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSokeithi.HeightDef = 18
        Me.numSokeithi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSokeithi.HissuLabelVisible = False
        Me.numSokeithi.IsHissuCheck = False
        Me.numSokeithi.IsRangeCheck = False
        Me.numSokeithi.ItemName = ""
        Me.numSokeithi.Location = New System.Drawing.Point(1071, 202)
        Me.numSokeithi.Name = "numSokeithi"
        Me.numSokeithi.ReadOnly = True
        Me.numSokeithi.Size = New System.Drawing.Size(157, 18)
        Me.numSokeithi.TabIndex = 341
        Me.numSokeithi.TabStop = False
        Me.numSokeithi.TabStopSetting = False
        Me.numSokeithi.TextValue = "0"
        Me.numSokeithi.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSokeithi.WidthDef = 157
        '
        'cmbEigyo
        '
        Me.cmbEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbEigyo.Location = New System.Drawing.Point(129, 20)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = False
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 343
        Me.cmbEigyo.TabStopSetting = True
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'lblTitleEigyo
        '
        Me.lblTitleEigyo.AutoSizeDef = False
        Me.lblTitleEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo.EnableStatus = False
        Me.lblTitleEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo.HeightDef = 18
        Me.lblTitleEigyo.Location = New System.Drawing.Point(45, 20)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(85, 18)
        Me.lblTitleEigyo.TabIndex = 342
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 85
        '
        'sprDetail
        '
        Me.sprDetail.AccessibleDescription = ""
        Me.sprDetail.AllowUserZoom = False
        Me.sprDetail.AutoImeMode = False
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
        Me.sprDetail.HeightDef = 640
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 227)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1244, 640)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 344
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.WidthDef = 1244
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDetail_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenFocusedNormal)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDetail.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetail_InputMapWhenAncestorOfFocusedNormal)
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        'lblOrderBy
        '
        Me.lblOrderBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrderBy.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrderBy.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrderBy.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOrderBy.CountWrappedLine = False
        Me.lblOrderBy.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblOrderBy.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderBy.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrderBy.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOrderBy.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblOrderBy.HeightDef = 18
        Me.lblOrderBy.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOrderBy.HissuLabelVisible = False
        Me.lblOrderBy.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblOrderBy.IsByteCheck = 0
        Me.lblOrderBy.IsCalendarCheck = False
        Me.lblOrderBy.IsDakutenCheck = False
        Me.lblOrderBy.IsEisuCheck = False
        Me.lblOrderBy.IsForbiddenWordsCheck = False
        Me.lblOrderBy.IsFullByteCheck = 0
        Me.lblOrderBy.IsHankakuCheck = False
        Me.lblOrderBy.IsHissuCheck = False
        Me.lblOrderBy.IsKanaCheck = False
        Me.lblOrderBy.IsMiddleSpace = False
        Me.lblOrderBy.IsNumericCheck = False
        Me.lblOrderBy.IsSujiCheck = False
        Me.lblOrderBy.IsZenkakuCheck = False
        Me.lblOrderBy.ItemName = ""
        Me.lblOrderBy.LineSpace = 0
        Me.lblOrderBy.Location = New System.Drawing.Point(497, 21)
        Me.lblOrderBy.MaxLength = 0
        Me.lblOrderBy.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblOrderBy.MaxLineCount = 0
        Me.lblOrderBy.Multiline = False
        Me.lblOrderBy.Name = "lblOrderBy"
        Me.lblOrderBy.ReadOnly = True
        Me.lblOrderBy.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblOrderBy.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblOrderBy.Size = New System.Drawing.Size(250, 18)
        Me.lblOrderBy.TabIndex = 345
        Me.lblOrderBy.TabStop = False
        Me.lblOrderBy.TabStopSetting = False
        Me.lblOrderBy.TextValue = "XXXXXXXXX"
        Me.lblOrderBy.UseSystemPasswordChar = False
        Me.lblOrderBy.Visible = False
        Me.lblOrderBy.WidthDef = 250
        Me.lblOrderBy.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCalcKbn
        '
        Me.lblCalcKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalcKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCalcKbn.CountWrappedLine = False
        Me.lblCalcKbn.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCalcKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCalcKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCalcKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCalcKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCalcKbn.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCalcKbn.HeightDef = 18
        Me.lblCalcKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCalcKbn.HissuLabelVisible = False
        Me.lblCalcKbn.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCalcKbn.IsByteCheck = 0
        Me.lblCalcKbn.IsCalendarCheck = False
        Me.lblCalcKbn.IsDakutenCheck = False
        Me.lblCalcKbn.IsEisuCheck = False
        Me.lblCalcKbn.IsForbiddenWordsCheck = False
        Me.lblCalcKbn.IsFullByteCheck = 0
        Me.lblCalcKbn.IsHankakuCheck = False
        Me.lblCalcKbn.IsHissuCheck = False
        Me.lblCalcKbn.IsKanaCheck = False
        Me.lblCalcKbn.IsMiddleSpace = False
        Me.lblCalcKbn.IsNumericCheck = False
        Me.lblCalcKbn.IsSujiCheck = False
        Me.lblCalcKbn.IsZenkakuCheck = False
        Me.lblCalcKbn.ItemName = ""
        Me.lblCalcKbn.LineSpace = 0
        Me.lblCalcKbn.Location = New System.Drawing.Point(941, 178)
        Me.lblCalcKbn.MaxLength = 0
        Me.lblCalcKbn.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCalcKbn.MaxLineCount = 0
        Me.lblCalcKbn.Multiline = False
        Me.lblCalcKbn.Name = "lblCalcKbn"
        Me.lblCalcKbn.ReadOnly = True
        Me.lblCalcKbn.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCalcKbn.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCalcKbn.Size = New System.Drawing.Size(250, 18)
        Me.lblCalcKbn.TabIndex = 346
        Me.lblCalcKbn.TabStop = False
        Me.lblCalcKbn.TabStopSetting = False
        Me.lblCalcKbn.TextValue = "XXXXXXXXX"
        Me.lblCalcKbn.UseSystemPasswordChar = False
        Me.lblCalcKbn.Visible = False
        Me.lblCalcKbn.WidthDef = 250
        Me.lblCalcKbn.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdDate
        '
        Me.lblSysUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdDate.CountWrappedLine = False
        Me.lblSysUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdDate.HeightDef = 18
        Me.lblSysUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdDate.HissuLabelVisible = False
        Me.lblSysUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdDate.IsByteCheck = 0
        Me.lblSysUpdDate.IsCalendarCheck = False
        Me.lblSysUpdDate.IsDakutenCheck = False
        Me.lblSysUpdDate.IsEisuCheck = False
        Me.lblSysUpdDate.IsForbiddenWordsCheck = False
        Me.lblSysUpdDate.IsFullByteCheck = 0
        Me.lblSysUpdDate.IsHankakuCheck = False
        Me.lblSysUpdDate.IsHissuCheck = False
        Me.lblSysUpdDate.IsKanaCheck = False
        Me.lblSysUpdDate.IsMiddleSpace = False
        Me.lblSysUpdDate.IsNumericCheck = False
        Me.lblSysUpdDate.IsSujiCheck = False
        Me.lblSysUpdDate.IsZenkakuCheck = False
        Me.lblSysUpdDate.ItemName = ""
        Me.lblSysUpdDate.LineSpace = 0
        Me.lblSysUpdDate.Location = New System.Drawing.Point(785, 178)
        Me.lblSysUpdDate.MaxLength = 0
        Me.lblSysUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdDate.MaxLineCount = 0
        Me.lblSysUpdDate.Multiline = False
        Me.lblSysUpdDate.Name = "lblSysUpdDate"
        Me.lblSysUpdDate.ReadOnly = True
        Me.lblSysUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdDate.Size = New System.Drawing.Size(37, 18)
        Me.lblSysUpdDate.TabIndex = 349
        Me.lblSysUpdDate.TabStop = False
        Me.lblSysUpdDate.TabStopSetting = False
        Me.lblSysUpdDate.TextValue = "XXXXXXXXX"
        Me.lblSysUpdDate.UseSystemPasswordChar = False
        Me.lblSysUpdDate.Visible = False
        Me.lblSysUpdDate.WidthDef = 37
        Me.lblSysUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSysUpdTime
        '
        Me.lblSysUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSysUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSysUpdTime.CountWrappedLine = False
        Me.lblSysUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSysUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSysUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSysUpdTime.HeightDef = 18
        Me.lblSysUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSysUpdTime.HissuLabelVisible = False
        Me.lblSysUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSysUpdTime.IsByteCheck = 0
        Me.lblSysUpdTime.IsCalendarCheck = False
        Me.lblSysUpdTime.IsDakutenCheck = False
        Me.lblSysUpdTime.IsEisuCheck = False
        Me.lblSysUpdTime.IsForbiddenWordsCheck = False
        Me.lblSysUpdTime.IsFullByteCheck = 0
        Me.lblSysUpdTime.IsHankakuCheck = False
        Me.lblSysUpdTime.IsHissuCheck = False
        Me.lblSysUpdTime.IsKanaCheck = False
        Me.lblSysUpdTime.IsMiddleSpace = False
        Me.lblSysUpdTime.IsNumericCheck = False
        Me.lblSysUpdTime.IsSujiCheck = False
        Me.lblSysUpdTime.IsZenkakuCheck = False
        Me.lblSysUpdTime.ItemName = ""
        Me.lblSysUpdTime.LineSpace = 0
        Me.lblSysUpdTime.Location = New System.Drawing.Point(808, 178)
        Me.lblSysUpdTime.MaxLength = 0
        Me.lblSysUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSysUpdTime.MaxLineCount = 0
        Me.lblSysUpdTime.Multiline = False
        Me.lblSysUpdTime.Name = "lblSysUpdTime"
        Me.lblSysUpdTime.ReadOnly = True
        Me.lblSysUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSysUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSysUpdTime.Size = New System.Drawing.Size(39, 18)
        Me.lblSysUpdTime.TabIndex = 350
        Me.lblSysUpdTime.TabStop = False
        Me.lblSysUpdTime.TabStopSetting = False
        Me.lblSysUpdTime.TextValue = "XXXXXXXXX"
        Me.lblSysUpdTime.UseSystemPasswordChar = False
        Me.lblSysUpdTime.Visible = False
        Me.lblSysUpdTime.WidthDef = 39
        Me.lblSysUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMI730F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI730F"
        Me.Text = "【LMI730】 運賃差分抽出"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlCondition.ResumeLayout(False)
        Me.pnlCondition.PerformLayout()
        Me.pnlConditionKbn.ResumeLayout(False)
        Me.pnlRev.ResumeLayout(False)
        Me.pnlRev.PerformLayout()
        Me.pnlHenko.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCondition As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlHenko As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtShuseiL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtShuseiM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleHenko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlConditionKbn As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlRev As System.Windows.Forms.Panel
    Friend WithEvents optRevRyoho As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optRevKaku As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optRevMi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleRev As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKara As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdTo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents imdFrom As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents cmbDateKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbShusei As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents btnHenko As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleYen As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSokei As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSokeithi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents txtShuseiSS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtShuseiS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblOrderBy As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCalcKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDest As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
End Class
