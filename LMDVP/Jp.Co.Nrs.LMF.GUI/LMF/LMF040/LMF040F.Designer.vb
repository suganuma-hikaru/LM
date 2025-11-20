<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF040F
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
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField4 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField4 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.pnlCondition = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleDest = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlConditionKbn = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.pnlMoto = New System.Windows.Forms.Panel()
        Me.optAll = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optUnso = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optOut = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optIn = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleMoto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlRev = New System.Windows.Forms.Panel()
        Me.optRevRyoho = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optRevKaku = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optRevMi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleRev = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlGroupNo = New System.Windows.Forms.Panel()
        Me.optGroupRyoho = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optGroupSumi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optGroupMi = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleGroupNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlUnchin = New System.Windows.Forms.Panel()
        Me.optUnchinRyoho = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optTonKiro = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optShaDate = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblTitleUnchinKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblExtcNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtExtcCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTehaiKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleTyuki = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKey = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlKey = New System.Windows.Forms.Panel()
        Me.optGroup = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.optNomal = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton()
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKara = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblDriverNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDriver = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDriverCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbGroup = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleGroup = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
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
        Me.cmbVisibleKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleVisibleKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSysUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSysUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlCondition.SuspendLayout()
        Me.pnlConditionKbn.SuspendLayout()
        Me.pnlMoto.SuspendLayout()
        Me.pnlRev.SuspendLayout()
        Me.pnlGroupNo.SuspendLayout()
        Me.pnlUnchin.SuspendLayout()
        Me.pnlKey.SuspendLayout()
        Me.pnlHenko.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdTime)
        Me.pnlViewAria.Controls.Add(Me.lblSysUpdDate)
        Me.pnlViewAria.Controls.Add(Me.cmbVisibleKb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleVisibleKb)
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
        Me.pnlCondition.Controls.Add(Me.lblExtcNm)
        Me.pnlCondition.Controls.Add(Me.txtExtcCd)
        Me.pnlCondition.Controls.Add(Me.lblTitleExtc)
        Me.pnlCondition.Controls.Add(Me.cmbTariffKbn)
        Me.pnlCondition.Controls.Add(Me.lblTitleTehaiKbn)
        Me.pnlCondition.Controls.Add(Me.lblTitleTyuki)
        Me.pnlCondition.Controls.Add(Me.lblTitleKey)
        Me.pnlCondition.Controls.Add(Me.pnlKey)
        Me.pnlCondition.Controls.Add(Me.lblCustNm)
        Me.pnlCondition.Controls.Add(Me.txtCustCdM)
        Me.pnlCondition.Controls.Add(Me.txtCustCdL)
        Me.pnlCondition.Controls.Add(Me.lblTitleCust)
        Me.pnlCondition.Controls.Add(Me.lblTitleKara)
        Me.pnlCondition.Controls.Add(Me.lblDriverNm)
        Me.pnlCondition.Controls.Add(Me.lblTitleDriver)
        Me.pnlCondition.Controls.Add(Me.txtDriverCd)
        Me.pnlCondition.Controls.Add(Me.lblTariffNm)
        Me.pnlCondition.Controls.Add(Me.txtTariffCd)
        Me.pnlCondition.Controls.Add(Me.lblTitleTariff)
        Me.pnlCondition.Controls.Add(Me.cmbGroup)
        Me.pnlCondition.Controls.Add(Me.lblTitleGroup)
        Me.pnlCondition.Controls.Add(Me.imdTo)
        Me.pnlCondition.Controls.Add(Me.imdFrom)
        Me.pnlCondition.Controls.Add(Me.cmbDateKb)
        Me.pnlCondition.EnableStatus = False
        Me.pnlCondition.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlCondition.HeightDef = 222
        Me.pnlCondition.Location = New System.Drawing.Point(16, 45)
        Me.pnlCondition.Name = "pnlCondition"
        Me.pnlCondition.Size = New System.Drawing.Size(1243, 222)
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
        Me.lblTitleDest.Location = New System.Drawing.Point(43, 152)
        Me.lblTitleDest.Name = "lblTitleDest"
        Me.lblTitleDest.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleDest.TabIndex = 346
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
        Me.lblDestNm.Location = New System.Drawing.Point(220, 149)
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
        Me.txtDestCd.Location = New System.Drawing.Point(113, 149)
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
        Me.pnlConditionKbn.Controls.Add(Me.pnlMoto)
        Me.pnlConditionKbn.Controls.Add(Me.pnlRev)
        Me.pnlConditionKbn.Controls.Add(Me.pnlGroupNo)
        Me.pnlConditionKbn.Controls.Add(Me.pnlUnchin)
        Me.pnlConditionKbn.EnableStatus = False
        Me.pnlConditionKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlConditionKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlConditionKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlConditionKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlConditionKbn.HeightDef = 167
        Me.pnlConditionKbn.Location = New System.Drawing.Point(763, 25)
        Me.pnlConditionKbn.Name = "pnlConditionKbn"
        Me.pnlConditionKbn.Size = New System.Drawing.Size(471, 167)
        Me.pnlConditionKbn.TabIndex = 335
        Me.pnlConditionKbn.TabStop = False
        Me.pnlConditionKbn.Text = "検索区分"
        Me.pnlConditionKbn.TextValue = "検索区分"
        Me.pnlConditionKbn.WidthDef = 471
        '
        'pnlMoto
        '
        Me.pnlMoto.Controls.Add(Me.optAll)
        Me.pnlMoto.Controls.Add(Me.optUnso)
        Me.pnlMoto.Controls.Add(Me.optOut)
        Me.pnlMoto.Controls.Add(Me.optIn)
        Me.pnlMoto.Controls.Add(Me.lblTitleMoto)
        Me.pnlMoto.Location = New System.Drawing.Point(11, 120)
        Me.pnlMoto.Name = "pnlMoto"
        Me.pnlMoto.Size = New System.Drawing.Size(442, 27)
        Me.pnlMoto.TabIndex = 3
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optAll.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optAll.EnableStatus = True
        Me.optAll.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optAll.HeightDef = 17
        Me.optAll.Location = New System.Drawing.Point(365, 5)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(53, 17)
        Me.optAll.TabIndex = 315
        Me.optAll.TabStop = True
        Me.optAll.TabStopSetting = True
        Me.optAll.Text = "全て"
        Me.optAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optAll.TextValue = "全て"
        Me.optAll.UseVisualStyleBackColor = True
        Me.optAll.WidthDef = 53
        '
        'optUnso
        '
        Me.optUnso.AutoSize = True
        Me.optUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optUnso.EnableStatus = True
        Me.optUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optUnso.HeightDef = 17
        Me.optUnso.Location = New System.Drawing.Point(285, 5)
        Me.optUnso.Name = "optUnso"
        Me.optUnso.Size = New System.Drawing.Size(53, 17)
        Me.optUnso.TabIndex = 314
        Me.optUnso.TabStop = True
        Me.optUnso.TabStopSetting = True
        Me.optUnso.Text = "運送"
        Me.optUnso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optUnso.TextValue = "運送"
        Me.optUnso.UseVisualStyleBackColor = True
        Me.optUnso.WidthDef = 53
        '
        'optOut
        '
        Me.optOut.AutoSize = True
        Me.optOut.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optOut.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optOut.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optOut.EnableStatus = True
        Me.optOut.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optOut.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optOut.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optOut.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optOut.HeightDef = 17
        Me.optOut.Location = New System.Drawing.Point(198, 5)
        Me.optOut.Name = "optOut"
        Me.optOut.Size = New System.Drawing.Size(53, 17)
        Me.optOut.TabIndex = 313
        Me.optOut.TabStop = True
        Me.optOut.TabStopSetting = True
        Me.optOut.Text = "出荷"
        Me.optOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optOut.TextValue = "出荷"
        Me.optOut.UseVisualStyleBackColor = True
        Me.optOut.WidthDef = 53
        '
        'optIn
        '
        Me.optIn.AutoSize = True
        Me.optIn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optIn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optIn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optIn.EnableStatus = True
        Me.optIn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optIn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optIn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optIn.HeightDef = 17
        Me.optIn.Location = New System.Drawing.Point(110, 5)
        Me.optIn.Name = "optIn"
        Me.optIn.Size = New System.Drawing.Size(53, 17)
        Me.optIn.TabIndex = 312
        Me.optIn.TabStop = True
        Me.optIn.TabStopSetting = True
        Me.optIn.Text = "入荷"
        Me.optIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optIn.TextValue = "入荷"
        Me.optIn.UseVisualStyleBackColor = True
        Me.optIn.WidthDef = 53
        '
        'lblTitleMoto
        '
        Me.lblTitleMoto.AutoSize = True
        Me.lblTitleMoto.AutoSizeDef = True
        Me.lblTitleMoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMoto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMoto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMoto.EnableStatus = False
        Me.lblTitleMoto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMoto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMoto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMoto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMoto.HeightDef = 13
        Me.lblTitleMoto.Location = New System.Drawing.Point(6, 7)
        Me.lblTitleMoto.Name = "lblTitleMoto"
        Me.lblTitleMoto.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleMoto.TabIndex = 311
        Me.lblTitleMoto.Text = "元データ区分"
        Me.lblTitleMoto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMoto.TextValue = "元データ区分"
        Me.lblTitleMoto.WidthDef = 91
        '
        'pnlRev
        '
        Me.pnlRev.Controls.Add(Me.optRevRyoho)
        Me.pnlRev.Controls.Add(Me.optRevKaku)
        Me.pnlRev.Controls.Add(Me.optRevMi)
        Me.pnlRev.Controls.Add(Me.lblTitleRev)
        Me.pnlRev.Location = New System.Drawing.Point(11, 87)
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
        'pnlGroupNo
        '
        Me.pnlGroupNo.Controls.Add(Me.optGroupRyoho)
        Me.pnlGroupNo.Controls.Add(Me.optGroupSumi)
        Me.pnlGroupNo.Controls.Add(Me.optGroupMi)
        Me.pnlGroupNo.Controls.Add(Me.lblTitleGroupNo)
        Me.pnlGroupNo.Location = New System.Drawing.Point(11, 53)
        Me.pnlGroupNo.Name = "pnlGroupNo"
        Me.pnlGroupNo.Size = New System.Drawing.Size(442, 27)
        Me.pnlGroupNo.TabIndex = 1
        '
        'optGroupRyoho
        '
        Me.optGroupRyoho.AutoSize = True
        Me.optGroupRyoho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupRyoho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupRyoho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optGroupRyoho.EnableStatus = True
        Me.optGroupRyoho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupRyoho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupRyoho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupRyoho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupRyoho.HeightDef = 17
        Me.optGroupRyoho.Location = New System.Drawing.Point(285, 5)
        Me.optGroupRyoho.Name = "optGroupRyoho"
        Me.optGroupRyoho.Size = New System.Drawing.Size(53, 17)
        Me.optGroupRyoho.TabIndex = 314
        Me.optGroupRyoho.TabStop = True
        Me.optGroupRyoho.TabStopSetting = True
        Me.optGroupRyoho.Text = "両方"
        Me.optGroupRyoho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optGroupRyoho.TextValue = "両方"
        Me.optGroupRyoho.UseVisualStyleBackColor = True
        Me.optGroupRyoho.WidthDef = 53
        '
        'optGroupSumi
        '
        Me.optGroupSumi.AutoSize = True
        Me.optGroupSumi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupSumi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupSumi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optGroupSumi.EnableStatus = True
        Me.optGroupSumi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupSumi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupSumi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupSumi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupSumi.HeightDef = 17
        Me.optGroupSumi.Location = New System.Drawing.Point(198, 5)
        Me.optGroupSumi.Name = "optGroupSumi"
        Me.optGroupSumi.Size = New System.Drawing.Size(39, 17)
        Me.optGroupSumi.TabIndex = 313
        Me.optGroupSumi.TabStop = True
        Me.optGroupSumi.TabStopSetting = True
        Me.optGroupSumi.Text = "済"
        Me.optGroupSumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optGroupSumi.TextValue = "済"
        Me.optGroupSumi.UseVisualStyleBackColor = True
        Me.optGroupSumi.WidthDef = 39
        '
        'optGroupMi
        '
        Me.optGroupMi.AutoSize = True
        Me.optGroupMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupMi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroupMi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optGroupMi.EnableStatus = True
        Me.optGroupMi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupMi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroupMi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupMi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroupMi.HeightDef = 17
        Me.optGroupMi.Location = New System.Drawing.Point(110, 5)
        Me.optGroupMi.Name = "optGroupMi"
        Me.optGroupMi.Size = New System.Drawing.Size(39, 17)
        Me.optGroupMi.TabIndex = 312
        Me.optGroupMi.TabStop = True
        Me.optGroupMi.TabStopSetting = True
        Me.optGroupMi.Text = "未"
        Me.optGroupMi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optGroupMi.TextValue = "未"
        Me.optGroupMi.UseVisualStyleBackColor = True
        Me.optGroupMi.WidthDef = 39
        '
        'lblTitleGroupNo
        '
        Me.lblTitleGroupNo.AutoSize = True
        Me.lblTitleGroupNo.AutoSizeDef = True
        Me.lblTitleGroupNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroupNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroupNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGroupNo.EnableStatus = False
        Me.lblTitleGroupNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroupNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroupNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroupNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroupNo.HeightDef = 13
        Me.lblTitleGroupNo.Location = New System.Drawing.Point(7, 7)
        Me.lblTitleGroupNo.Name = "lblTitleGroupNo"
        Me.lblTitleGroupNo.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleGroupNo.TabIndex = 311
        Me.lblTitleGroupNo.Text = "まとめ番号"
        Me.lblTitleGroupNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGroupNo.TextValue = "まとめ番号"
        Me.lblTitleGroupNo.WidthDef = 77
        '
        'pnlUnchin
        '
        Me.pnlUnchin.Controls.Add(Me.optUnchinRyoho)
        Me.pnlUnchin.Controls.Add(Me.optTonKiro)
        Me.pnlUnchin.Controls.Add(Me.optShaDate)
        Me.pnlUnchin.Controls.Add(Me.lblTitleUnchinKbn)
        Me.pnlUnchin.Location = New System.Drawing.Point(11, 20)
        Me.pnlUnchin.Name = "pnlUnchin"
        Me.pnlUnchin.Size = New System.Drawing.Size(443, 27)
        Me.pnlUnchin.TabIndex = 0
        '
        'optUnchinRyoho
        '
        Me.optUnchinRyoho.AutoSize = True
        Me.optUnchinRyoho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optUnchinRyoho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optUnchinRyoho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optUnchinRyoho.EnableStatus = True
        Me.optUnchinRyoho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optUnchinRyoho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optUnchinRyoho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optUnchinRyoho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optUnchinRyoho.HeightDef = 17
        Me.optUnchinRyoho.Location = New System.Drawing.Point(284, 5)
        Me.optUnchinRyoho.Name = "optUnchinRyoho"
        Me.optUnchinRyoho.Size = New System.Drawing.Size(53, 17)
        Me.optUnchinRyoho.TabIndex = 314
        Me.optUnchinRyoho.TabStop = True
        Me.optUnchinRyoho.TabStopSetting = True
        Me.optUnchinRyoho.Text = "両方"
        Me.optUnchinRyoho.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optUnchinRyoho.TextValue = "両方"
        Me.optUnchinRyoho.UseVisualStyleBackColor = True
        Me.optUnchinRyoho.WidthDef = 53
        '
        'optTonKiro
        '
        Me.optTonKiro.AutoSize = True
        Me.optTonKiro.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optTonKiro.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optTonKiro.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optTonKiro.EnableStatus = True
        Me.optTonKiro.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optTonKiro.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optTonKiro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTonKiro.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optTonKiro.HeightDef = 17
        Me.optTonKiro.Location = New System.Drawing.Point(197, 5)
        Me.optTonKiro.Name = "optTonKiro"
        Me.optTonKiro.Size = New System.Drawing.Size(81, 17)
        Me.optTonKiro.TabIndex = 313
        Me.optTonKiro.TabStop = True
        Me.optTonKiro.TabStopSetting = True
        Me.optTonKiro.Text = "トンキロ"
        Me.optTonKiro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optTonKiro.TextValue = "トンキロ"
        Me.optTonKiro.UseVisualStyleBackColor = True
        Me.optTonKiro.WidthDef = 81
        '
        'optShaDate
        '
        Me.optShaDate.AutoSize = True
        Me.optShaDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optShaDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optShaDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optShaDate.EnableStatus = True
        Me.optShaDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optShaDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optShaDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optShaDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optShaDate.HeightDef = 17
        Me.optShaDate.Location = New System.Drawing.Point(109, 5)
        Me.optShaDate.Name = "optShaDate"
        Me.optShaDate.Size = New System.Drawing.Size(67, 17)
        Me.optShaDate.TabIndex = 312
        Me.optShaDate.TabStop = True
        Me.optShaDate.TabStopSetting = True
        Me.optShaDate.Text = "車建て"
        Me.optShaDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optShaDate.TextValue = "車建て"
        Me.optShaDate.UseVisualStyleBackColor = True
        Me.optShaDate.WidthDef = 67
        '
        'lblTitleUnchinKbn
        '
        Me.lblTitleUnchinKbn.AutoSize = True
        Me.lblTitleUnchinKbn.AutoSizeDef = True
        Me.lblTitleUnchinKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchinKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnchinKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnchinKbn.EnableStatus = False
        Me.lblTitleUnchinKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchinKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnchinKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchinKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnchinKbn.HeightDef = 13
        Me.lblTitleUnchinKbn.Location = New System.Drawing.Point(6, 7)
        Me.lblTitleUnchinKbn.Name = "lblTitleUnchinKbn"
        Me.lblTitleUnchinKbn.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUnchinKbn.TabIndex = 311
        Me.lblTitleUnchinKbn.Text = "運賃区分"
        Me.lblTitleUnchinKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnchinKbn.TextValue = "運賃区分"
        Me.lblTitleUnchinKbn.WidthDef = 63
        '
        'lblExtcNm
        '
        Me.lblExtcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExtcNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExtcNm.CountWrappedLine = False
        Me.lblExtcNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblExtcNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblExtcNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblExtcNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblExtcNm.HeightDef = 18
        Me.lblExtcNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblExtcNm.HissuLabelVisible = False
        Me.lblExtcNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblExtcNm.IsByteCheck = 0
        Me.lblExtcNm.IsCalendarCheck = False
        Me.lblExtcNm.IsDakutenCheck = False
        Me.lblExtcNm.IsEisuCheck = False
        Me.lblExtcNm.IsForbiddenWordsCheck = False
        Me.lblExtcNm.IsFullByteCheck = 0
        Me.lblExtcNm.IsHankakuCheck = False
        Me.lblExtcNm.IsHissuCheck = False
        Me.lblExtcNm.IsKanaCheck = False
        Me.lblExtcNm.IsMiddleSpace = False
        Me.lblExtcNm.IsNumericCheck = False
        Me.lblExtcNm.IsSujiCheck = False
        Me.lblExtcNm.IsZenkakuCheck = False
        Me.lblExtcNm.ItemName = ""
        Me.lblExtcNm.LineSpace = 0
        Me.lblExtcNm.Location = New System.Drawing.Point(220, 85)
        Me.lblExtcNm.MaxLength = 0
        Me.lblExtcNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblExtcNm.MaxLineCount = 0
        Me.lblExtcNm.Multiline = False
        Me.lblExtcNm.Name = "lblExtcNm"
        Me.lblExtcNm.ReadOnly = True
        Me.lblExtcNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblExtcNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblExtcNm.Size = New System.Drawing.Size(473, 18)
        Me.lblExtcNm.TabIndex = 343
        Me.lblExtcNm.TabStop = False
        Me.lblExtcNm.TabStopSetting = False
        Me.lblExtcNm.TextValue = "XXXXXXXXX"
        Me.lblExtcNm.UseSystemPasswordChar = False
        Me.lblExtcNm.WidthDef = 473
        Me.lblExtcNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtExtcCd
        '
        Me.txtExtcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtExtcCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtExtcCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExtcCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtExtcCd.CountWrappedLine = False
        Me.txtExtcCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtExtcCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtExtcCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtExtcCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtExtcCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtExtcCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtExtcCd.HeightDef = 18
        Me.txtExtcCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtExtcCd.HissuLabelVisible = False
        Me.txtExtcCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtExtcCd.IsByteCheck = 10
        Me.txtExtcCd.IsCalendarCheck = False
        Me.txtExtcCd.IsDakutenCheck = False
        Me.txtExtcCd.IsEisuCheck = False
        Me.txtExtcCd.IsForbiddenWordsCheck = False
        Me.txtExtcCd.IsFullByteCheck = 0
        Me.txtExtcCd.IsHankakuCheck = False
        Me.txtExtcCd.IsHissuCheck = False
        Me.txtExtcCd.IsKanaCheck = False
        Me.txtExtcCd.IsMiddleSpace = False
        Me.txtExtcCd.IsNumericCheck = False
        Me.txtExtcCd.IsSujiCheck = False
        Me.txtExtcCd.IsZenkakuCheck = False
        Me.txtExtcCd.ItemName = ""
        Me.txtExtcCd.LineSpace = 0
        Me.txtExtcCd.Location = New System.Drawing.Point(113, 85)
        Me.txtExtcCd.MaxLength = 10
        Me.txtExtcCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtExtcCd.MaxLineCount = 0
        Me.txtExtcCd.Multiline = False
        Me.txtExtcCd.Name = "txtExtcCd"
        Me.txtExtcCd.ReadOnly = False
        Me.txtExtcCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtExtcCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtExtcCd.Size = New System.Drawing.Size(125, 18)
        Me.txtExtcCd.TabIndex = 342
        Me.txtExtcCd.TabStopSetting = True
        Me.txtExtcCd.TextValue = "XXXXXXXXX"
        Me.txtExtcCd.UseSystemPasswordChar = False
        Me.txtExtcCd.WidthDef = 125
        Me.txtExtcCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleExtc
        '
        Me.lblTitleExtc.AutoSizeDef = False
        Me.lblTitleExtc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExtc.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleExtc.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleExtc.EnableStatus = False
        Me.lblTitleExtc.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExtc.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleExtc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExtc.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleExtc.HeightDef = 13
        Me.lblTitleExtc.Location = New System.Drawing.Point(36, 88)
        Me.lblTitleExtc.Name = "lblTitleExtc"
        Me.lblTitleExtc.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleExtc.TabIndex = 341
        Me.lblTitleExtc.Text = "割増タリフ"
        Me.lblTitleExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleExtc.TextValue = "割増タリフ"
        Me.lblTitleExtc.WidthDef = 77
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
        Me.cmbTariffKbn.Location = New System.Drawing.Point(113, 43)
        Me.cmbTariffKbn.Name = "cmbTariffKbn"
        Me.cmbTariffKbn.ReadOnly = False
        Me.cmbTariffKbn.SelectedIndex = -1
        Me.cmbTariffKbn.SelectedItem = Nothing
        Me.cmbTariffKbn.SelectedText = ""
        Me.cmbTariffKbn.SelectedValue = ""
        Me.cmbTariffKbn.Size = New System.Drawing.Size(125, 18)
        Me.cmbTariffKbn.TabIndex = 340
        Me.cmbTariffKbn.TabStopSetting = True
        Me.cmbTariffKbn.TextValue = ""
        Me.cmbTariffKbn.Value1 = Nothing
        Me.cmbTariffKbn.Value2 = Nothing
        Me.cmbTariffKbn.Value3 = Nothing
        Me.cmbTariffKbn.ValueMember = Nothing
        Me.cmbTariffKbn.WidthDef = 125
        '
        'lblTitleTehaiKbn
        '
        Me.lblTitleTehaiKbn.AutoSizeDef = False
        Me.lblTitleTehaiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehaiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTehaiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTehaiKbn.EnableStatus = False
        Me.lblTitleTehaiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehaiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTehaiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehaiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTehaiKbn.HeightDef = 13
        Me.lblTitleTehaiKbn.Location = New System.Drawing.Point(37, 46)
        Me.lblTitleTehaiKbn.Name = "lblTitleTehaiKbn"
        Me.lblTitleTehaiKbn.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTehaiKbn.TabIndex = 339
        Me.lblTitleTehaiKbn.Text = "タリフ分類"
        Me.lblTitleTehaiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTehaiKbn.TextValue = "タリフ分類"
        Me.lblTitleTehaiKbn.WidthDef = 77
        '
        'lblTitleTyuki
        '
        Me.lblTitleTyuki.AutoSize = True
        Me.lblTitleTyuki.AutoSizeDef = True
        Me.lblTitleTyuki.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTyuki.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTyuki.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTyuki.EnableStatus = False
        Me.lblTitleTyuki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTyuki.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTyuki.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTyuki.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTyuki.HeightDef = 13
        Me.lblTitleTyuki.Location = New System.Drawing.Point(272, 177)
        Me.lblTitleTyuki.Name = "lblTitleTyuki"
        Me.lblTitleTyuki.Size = New System.Drawing.Size(497, 13)
        Me.lblTitleTyuki.TabIndex = 338
        Me.lblTitleTyuki.Text = "※ 出荷日/納入日/請求先/タリフ/割増タリフ/税区分は常に候補となります。"
        Me.lblTitleTyuki.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTyuki.TextValue = "※ 出荷日/納入日/請求先/タリフ/割増タリフ/税区分は常に候補となります。"
        Me.lblTitleTyuki.WidthDef = 497
        '
        'lblTitleKey
        '
        Me.lblTitleKey.AutoSizeDef = False
        Me.lblTitleKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKey.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKey.EnableStatus = False
        Me.lblTitleKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKey.HeightDef = 13
        Me.lblTitleKey.Location = New System.Drawing.Point(36, 202)
        Me.lblTitleKey.Name = "lblTitleKey"
        Me.lblTitleKey.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleKey.TabIndex = 337
        Me.lblTitleKey.Text = "(検索キー)"
        Me.lblTitleKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKey.TextValue = "(検索キー)"
        Me.lblTitleKey.WidthDef = 77
        '
        'pnlKey
        '
        Me.pnlKey.Controls.Add(Me.optGroup)
        Me.pnlKey.Controls.Add(Me.optNomal)
        Me.pnlKey.Enabled = False
        Me.pnlKey.Location = New System.Drawing.Point(113, 195)
        Me.pnlKey.Name = "pnlKey"
        Me.pnlKey.Size = New System.Drawing.Size(157, 24)
        Me.pnlKey.TabIndex = 336
        '
        'optGroup
        '
        Me.optGroup.AutoSize = True
        Me.optGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroup.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optGroup.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optGroup.Enabled = False
        Me.optGroup.EnableStatus = True
        Me.optGroup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroup.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroup.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optGroup.HeightDef = 17
        Me.optGroup.Location = New System.Drawing.Point(74, 5)
        Me.optGroup.Name = "optGroup"
        Me.optGroup.Size = New System.Drawing.Size(67, 17)
        Me.optGroup.TabIndex = 313
        Me.optGroup.TabStop = True
        Me.optGroup.TabStopSetting = True
        Me.optGroup.Text = "まとめ"
        Me.optGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optGroup.TextValue = "まとめ"
        Me.optGroup.UseVisualStyleBackColor = True
        Me.optGroup.WidthDef = 67
        '
        'optNomal
        '
        Me.optNomal.AutoSize = True
        Me.optNomal.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optNomal.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optNomal.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optNomal.Enabled = False
        Me.optNomal.EnableStatus = True
        Me.optNomal.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optNomal.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optNomal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optNomal.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optNomal.HeightDef = 17
        Me.optNomal.Location = New System.Drawing.Point(7, 5)
        Me.optNomal.Name = "optNomal"
        Me.optNomal.Size = New System.Drawing.Size(53, 17)
        Me.optNomal.TabIndex = 312
        Me.optNomal.TabStop = True
        Me.optNomal.TabStopSetting = True
        Me.optNomal.Text = "通常"
        Me.optNomal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optNomal.TextValue = "通常"
        Me.optNomal.UseVisualStyleBackColor = True
        Me.optNomal.WidthDef = 53
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
        Me.lblCustNm.Location = New System.Drawing.Point(220, 127)
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
        Me.txtCustCdM.Location = New System.Drawing.Point(182, 127)
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
        Me.txtCustCdL.Location = New System.Drawing.Point(113, 127)
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
        Me.lblTitleCust.Location = New System.Drawing.Point(40, 130)
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
        'lblDriverNm
        '
        Me.lblDriverNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDriverNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDriverNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDriverNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDriverNm.CountWrappedLine = False
        Me.lblDriverNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblDriverNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDriverNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDriverNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDriverNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDriverNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblDriverNm.HeightDef = 18
        Me.lblDriverNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblDriverNm.HissuLabelVisible = False
        Me.lblDriverNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblDriverNm.IsByteCheck = 0
        Me.lblDriverNm.IsCalendarCheck = False
        Me.lblDriverNm.IsDakutenCheck = False
        Me.lblDriverNm.IsEisuCheck = False
        Me.lblDriverNm.IsForbiddenWordsCheck = False
        Me.lblDriverNm.IsFullByteCheck = 0
        Me.lblDriverNm.IsHankakuCheck = False
        Me.lblDriverNm.IsHissuCheck = False
        Me.lblDriverNm.IsKanaCheck = False
        Me.lblDriverNm.IsMiddleSpace = False
        Me.lblDriverNm.IsNumericCheck = False
        Me.lblDriverNm.IsSujiCheck = False
        Me.lblDriverNm.IsZenkakuCheck = False
        Me.lblDriverNm.ItemName = ""
        Me.lblDriverNm.LineSpace = 0
        Me.lblDriverNm.Location = New System.Drawing.Point(220, 106)
        Me.lblDriverNm.MaxLength = 0
        Me.lblDriverNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDriverNm.MaxLineCount = 0
        Me.lblDriverNm.Multiline = False
        Me.lblDriverNm.Name = "lblDriverNm"
        Me.lblDriverNm.ReadOnly = True
        Me.lblDriverNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDriverNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDriverNm.Size = New System.Drawing.Size(473, 18)
        Me.lblDriverNm.TabIndex = 327
        Me.lblDriverNm.TabStop = False
        Me.lblDriverNm.TabStopSetting = False
        Me.lblDriverNm.TextValue = "XXXXXXXXX"
        Me.lblDriverNm.UseSystemPasswordChar = False
        Me.lblDriverNm.WidthDef = 473
        Me.lblDriverNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDriver
        '
        Me.lblTitleDriver.AutoSizeDef = False
        Me.lblTitleDriver.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDriver.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDriver.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDriver.EnableStatus = False
        Me.lblTitleDriver.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDriver.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDriver.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDriver.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDriver.HeightDef = 13
        Me.lblTitleDriver.Location = New System.Drawing.Point(64, 109)
        Me.lblTitleDriver.Name = "lblTitleDriver"
        Me.lblTitleDriver.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleDriver.TabIndex = 326
        Me.lblTitleDriver.Text = "乗務員"
        Me.lblTitleDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDriver.TextValue = "乗務員"
        Me.lblTitleDriver.WidthDef = 49
        '
        'txtDriverCd
        '
        Me.txtDriverCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDriverCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDriverCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDriverCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDriverCd.CountWrappedLine = False
        Me.txtDriverCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDriverCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDriverCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDriverCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDriverCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDriverCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDriverCd.HeightDef = 18
        Me.txtDriverCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDriverCd.HissuLabelVisible = False
        Me.txtDriverCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtDriverCd.IsByteCheck = 5
        Me.txtDriverCd.IsCalendarCheck = False
        Me.txtDriverCd.IsDakutenCheck = False
        Me.txtDriverCd.IsEisuCheck = False
        Me.txtDriverCd.IsForbiddenWordsCheck = False
        Me.txtDriverCd.IsFullByteCheck = 0
        Me.txtDriverCd.IsHankakuCheck = False
        Me.txtDriverCd.IsHissuCheck = False
        Me.txtDriverCd.IsKanaCheck = False
        Me.txtDriverCd.IsMiddleSpace = False
        Me.txtDriverCd.IsNumericCheck = False
        Me.txtDriverCd.IsSujiCheck = False
        Me.txtDriverCd.IsZenkakuCheck = False
        Me.txtDriverCd.ItemName = ""
        Me.txtDriverCd.LineSpace = 0
        Me.txtDriverCd.Location = New System.Drawing.Point(113, 106)
        Me.txtDriverCd.MaxLength = 5
        Me.txtDriverCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDriverCd.MaxLineCount = 0
        Me.txtDriverCd.Multiline = False
        Me.txtDriverCd.Name = "txtDriverCd"
        Me.txtDriverCd.ReadOnly = False
        Me.txtDriverCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDriverCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDriverCd.Size = New System.Drawing.Size(125, 18)
        Me.txtDriverCd.TabIndex = 325
        Me.txtDriverCd.TabStopSetting = True
        Me.txtDriverCd.TextValue = "XXXXX"
        Me.txtDriverCd.UseSystemPasswordChar = False
        Me.txtDriverCd.WidthDef = 125
        Me.txtDriverCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTariffNm.Location = New System.Drawing.Point(220, 64)
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
        Me.txtTariffCd.Location = New System.Drawing.Point(113, 64)
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
        Me.lblTitleTariff.Location = New System.Drawing.Point(36, 67)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariff.TabIndex = 321
        Me.lblTitleTariff.Text = "運賃タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "運賃タリフ"
        Me.lblTitleTariff.WidthDef = 77
        '
        'cmbGroup
        '
        Me.cmbGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbGroup.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbGroup.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbGroup.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbGroup.DataCode = "M009"
        Me.cmbGroup.DataSource = Nothing
        Me.cmbGroup.DisplayMember = Nothing
        Me.cmbGroup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbGroup.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbGroup.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbGroup.HeightDef = 18
        Me.cmbGroup.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbGroup.HissuLabelVisible = False
        Me.cmbGroup.InsertWildCard = True
        Me.cmbGroup.IsForbiddenWordsCheck = False
        Me.cmbGroup.IsHissuCheck = False
        Me.cmbGroup.ItemName = ""
        Me.cmbGroup.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbGroup.Location = New System.Drawing.Point(113, 174)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.ReadOnly = False
        Me.cmbGroup.SelectedIndex = -1
        Me.cmbGroup.SelectedItem = Nothing
        Me.cmbGroup.SelectedText = ""
        Me.cmbGroup.SelectedValue = ""
        Me.cmbGroup.Size = New System.Drawing.Size(172, 18)
        Me.cmbGroup.TabIndex = 320
        Me.cmbGroup.TabStopSetting = True
        Me.cmbGroup.TextValue = ""
        Me.cmbGroup.Value1 = Nothing
        Me.cmbGroup.Value2 = Nothing
        Me.cmbGroup.Value3 = Nothing
        Me.cmbGroup.ValueMember = Nothing
        Me.cmbGroup.WidthDef = 172
        '
        'lblTitleGroup
        '
        Me.lblTitleGroup.AutoSizeDef = False
        Me.lblTitleGroup.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroup.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGroup.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGroup.EnableStatus = False
        Me.lblTitleGroup.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroup.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroup.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGroup.HeightDef = 13
        Me.lblTitleGroup.Location = New System.Drawing.Point(8, 177)
        Me.lblTitleGroup.Name = "lblTitleGroup"
        Me.lblTitleGroup.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleGroup.TabIndex = 319
        Me.lblTitleGroup.Text = "まとめ候補条件"
        Me.lblTitleGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGroup.TextValue = "まとめ候補条件"
        Me.lblTitleGroup.WidthDef = 105
        '
        'imdTo
        '
        Me.imdTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdTo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        Me.imdTo.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField5, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField6, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdTo.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField3, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField3, GrapeCity.Win.Editors.Fields.DateField)}
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
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField7.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = "/"
        DateDayDisplayField4.ShowLeadingZero = True
        Me.imdFrom.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField7, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField8, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdFrom.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField4, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField4, GrapeCity.Win.Editors.Fields.DateField)}
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
        Me.pnlHenko.Location = New System.Drawing.Point(17, 273)
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
        Me.txtShuseiSS.Location = New System.Drawing.Point(421, 19)
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
        Me.txtShuseiS.Location = New System.Drawing.Point(391, 19)
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
        Me.btnHenko.Location = New System.Drawing.Point(469, 17)
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
        Me.cmbShusei.DataCode = "S057"
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
        Me.txtShuseiM.Location = New System.Drawing.Point(361, 19)
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
        Me.txtShuseiL.Location = New System.Drawing.Point(287, 19)
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
        Me.lblTitleHenko.Location = New System.Drawing.Point(21, 22)
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
        Me.lblTitleSokei.Location = New System.Drawing.Point(943, 308)
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
        Me.lblTitleYen.Location = New System.Drawing.Point(1218, 308)
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
        Me.numSokeithi.Location = New System.Drawing.Point(1076, 306)
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
        Me.sprDetail.HeightDef = 531
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 336)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1244, 531)
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
        Me.lblCalcKbn.Location = New System.Drawing.Point(946, 282)
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
        'cmbVisibleKb
        '
        Me.cmbVisibleKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVisibleKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbVisibleKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbVisibleKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbVisibleKb.DataCode = "K023"
        Me.cmbVisibleKb.DataSource = Nothing
        Me.cmbVisibleKb.DisplayMember = Nothing
        Me.cmbVisibleKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVisibleKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbVisibleKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVisibleKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbVisibleKb.HeightDef = 18
        Me.cmbVisibleKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbVisibleKb.HissuLabelVisible = False
        Me.cmbVisibleKb.InsertWildCard = True
        Me.cmbVisibleKb.IsForbiddenWordsCheck = False
        Me.cmbVisibleKb.IsHissuCheck = False
        Me.cmbVisibleKb.ItemName = ""
        Me.cmbVisibleKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbVisibleKb.Location = New System.Drawing.Point(685, 292)
        Me.cmbVisibleKb.Name = "cmbVisibleKb"
        Me.cmbVisibleKb.ReadOnly = False
        Me.cmbVisibleKb.SelectedIndex = -1
        Me.cmbVisibleKb.SelectedItem = Nothing
        Me.cmbVisibleKb.SelectedText = ""
        Me.cmbVisibleKb.SelectedValue = ""
        Me.cmbVisibleKb.Size = New System.Drawing.Size(113, 18)
        Me.cmbVisibleKb.TabIndex = 348
        Me.cmbVisibleKb.TabStopSetting = True
        Me.cmbVisibleKb.TextValue = ""
        Me.cmbVisibleKb.Value1 = Nothing
        Me.cmbVisibleKb.Value2 = Nothing
        Me.cmbVisibleKb.Value3 = Nothing
        Me.cmbVisibleKb.ValueMember = Nothing
        Me.cmbVisibleKb.WidthDef = 113
        '
        'lblTitleVisibleKb
        '
        Me.lblTitleVisibleKb.AutoSizeDef = False
        Me.lblTitleVisibleKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVisibleKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleVisibleKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleVisibleKb.EnableStatus = False
        Me.lblTitleVisibleKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVisibleKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleVisibleKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVisibleKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleVisibleKb.HeightDef = 18
        Me.lblTitleVisibleKb.Location = New System.Drawing.Point(598, 291)
        Me.lblTitleVisibleKb.Name = "lblTitleVisibleKb"
        Me.lblTitleVisibleKb.Size = New System.Drawing.Size(87, 18)
        Me.lblTitleVisibleKb.TabIndex = 347
        Me.lblTitleVisibleKb.Text = "項目表示"
        Me.lblTitleVisibleKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleVisibleKb.TextValue = "項目表示"
        Me.lblTitleVisibleKb.WidthDef = 87
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
        Me.lblSysUpdDate.Location = New System.Drawing.Point(790, 282)
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
        Me.lblSysUpdTime.Location = New System.Drawing.Point(813, 282)
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
        'LMF040F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMF040F"
        Me.Text = "【LMF040】 運賃検索"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.pnlCondition.ResumeLayout(False)
        Me.pnlCondition.PerformLayout()
        Me.pnlConditionKbn.ResumeLayout(False)
        Me.pnlMoto.ResumeLayout(False)
        Me.pnlMoto.PerformLayout()
        Me.pnlRev.ResumeLayout(False)
        Me.pnlRev.PerformLayout()
        Me.pnlGroupNo.ResumeLayout(False)
        Me.pnlGroupNo.PerformLayout()
        Me.pnlUnchin.ResumeLayout(False)
        Me.pnlUnchin.PerformLayout()
        Me.pnlKey.ResumeLayout(False)
        Me.pnlKey.PerformLayout()
        Me.pnlHenko.ResumeLayout(False)
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlCondition As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlHenko As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtShuseiL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtShuseiM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleHenko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTyuki As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKey As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlKey As System.Windows.Forms.Panel
    Friend WithEvents optGroup As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optNomal As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents pnlConditionKbn As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents pnlMoto As System.Windows.Forms.Panel
    Friend WithEvents optAll As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optUnso As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optOut As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optIn As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleMoto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlRev As System.Windows.Forms.Panel
    Friend WithEvents optRevRyoho As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optRevKaku As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optRevMi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleRev As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlGroupNo As System.Windows.Forms.Panel
    Friend WithEvents optGroupRyoho As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optGroupSumi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optGroupMi As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleGroupNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlUnchin As System.Windows.Forms.Panel
    Friend WithEvents optUnchinRyoho As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optTonKiro As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optShaDate As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents lblTitleUnchinKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKara As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblDriverNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDriver As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDriverCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbGroup As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleGroup As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
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
    Friend WithEvents cmbTariffKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTehaiKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblOrderBy As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblExtcNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtExtcCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCalcKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDest As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbVisibleKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleVisibleKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSysUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSysUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
End Class
