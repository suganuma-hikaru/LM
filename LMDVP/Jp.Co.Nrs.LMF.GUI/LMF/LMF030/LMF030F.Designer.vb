<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMF030F
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
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.pnlUnso = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTehaisyubetsu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbTehaisyubetsu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.btnTehaiCreate = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.txtUnsocoBrCdOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnsocoCdOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSyakenTrailer = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSyakenTruck = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCarKey = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCarType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCarNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleHaiso = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbHaiso = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.numUnsoNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleUnsoNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleYen2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleYen1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUnderUnsoInfor = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUncoInfor = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numRevUnchin = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleRevUnchin = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numPayAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitlePayAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleDo3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleUnsoOndo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numUnsoOndo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleKg2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numTripWt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleTripWt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKg1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numLoadWt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleLoadWt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleRem = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRem = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblDriverNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDriver = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDriverCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleJshaKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbJshaKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblUnsocoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnsocoBrCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUnsocoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleUnsoco = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleDo2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numOndoMm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleDo1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numOndoMx = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleCarOndo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleCarNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbBinKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleBinKbn = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTripNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTripNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleTripDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTripDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.pnlShiharai = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleYen4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numShiharaiKingaku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleShiharaiKingaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTariffKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTariffKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnShiharaiKeisan = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblTitleKensu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numKensu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleShiharaiKensu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleKg3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numShiharaiWt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleShiharaiWt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblExtcNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtExtcCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleExtc = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTariffNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtTariffCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleTariff = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleYen3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numKingaku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleKingaku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleShiharai = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbShiharai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.txtIsEditRowFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.pnlUnso.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlShiharai.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtIsEditRowFlg)
        Me.pnlViewAria.Controls.Add(Me.pnlShiharai)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.pnlUnso)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
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
        Me.lblSituation.TabIndex = 111
        Me.lblSituation.TabStop = False
        '
        'pnlUnso
        '
        Me.pnlUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlUnso.Controls.Add(Me.lblTitleTripWt)
        Me.pnlUnso.Controls.Add(Me.lblTehaisyubetsu)
        Me.pnlUnso.Controls.Add(Me.cmbTehaisyubetsu)
        Me.pnlUnso.Controls.Add(Me.btnTehaiCreate)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoBrCdOld)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoCdOld)
        Me.pnlUnso.Controls.Add(Me.lblSyakenTrailer)
        Me.pnlUnso.Controls.Add(Me.lblSyakenTruck)
        Me.pnlUnso.Controls.Add(Me.lblCarKey)
        Me.pnlUnso.Controls.Add(Me.lblCarType)
        Me.pnlUnso.Controls.Add(Me.txtCarNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleHaiso)
        Me.pnlUnso.Controls.Add(Me.cmbHaiso)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoNb)
        Me.pnlUnso.Controls.Add(Me.lblTitleYen2)
        Me.pnlUnso.Controls.Add(Me.lblTitleYen1)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnderUnsoInfor)
        Me.pnlUnso.Controls.Add(Me.lblTitleUncoInfor)
        Me.pnlUnso.Controls.Add(Me.numRevUnchin)
        Me.pnlUnso.Controls.Add(Me.lblTitleRevUnchin)
        Me.pnlUnso.Controls.Add(Me.numPayAmt)
        Me.pnlUnso.Controls.Add(Me.lblTitlePayAmt)
        Me.pnlUnso.Controls.Add(Me.lblTitleDo3)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoOndo)
        Me.pnlUnso.Controls.Add(Me.numUnsoOndo)
        Me.pnlUnso.Controls.Add(Me.lblTitleKg2)
        Me.pnlUnso.Controls.Add(Me.numTripWt)
        Me.pnlUnso.Controls.Add(Me.lblTitleKg1)
        Me.pnlUnso.Controls.Add(Me.numLoadWt)
        Me.pnlUnso.Controls.Add(Me.lblTitleLoadWt)
        Me.pnlUnso.Controls.Add(Me.lblTitleRem)
        Me.pnlUnso.Controls.Add(Me.txtRem)
        Me.pnlUnso.Controls.Add(Me.lblDriverNm)
        Me.pnlUnso.Controls.Add(Me.lblTitleDriver)
        Me.pnlUnso.Controls.Add(Me.txtDriverCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleJshaKbn)
        Me.pnlUnso.Controls.Add(Me.cmbJshaKbn)
        Me.pnlUnso.Controls.Add(Me.lblUnsocoNm)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoBrCd)
        Me.pnlUnso.Controls.Add(Me.txtUnsocoCd)
        Me.pnlUnso.Controls.Add(Me.lblTitleUnsoco)
        Me.pnlUnso.Controls.Add(Me.lblTitleDo2)
        Me.pnlUnso.Controls.Add(Me.numOndoMm)
        Me.pnlUnso.Controls.Add(Me.lblTitleDo1)
        Me.pnlUnso.Controls.Add(Me.numOndoMx)
        Me.pnlUnso.Controls.Add(Me.lblTitleCarOndo)
        Me.pnlUnso.Controls.Add(Me.lblTitleCarNo)
        Me.pnlUnso.Controls.Add(Me.cmbBinKbn)
        Me.pnlUnso.Controls.Add(Me.lblTitleBinKbn)
        Me.pnlUnso.Controls.Add(Me.lblTripNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleTripNo)
        Me.pnlUnso.Controls.Add(Me.lblTitleTripDate)
        Me.pnlUnso.Controls.Add(Me.cmbEigyo)
        Me.pnlUnso.Controls.Add(Me.lblTitleEigyo)
        Me.pnlUnso.Controls.Add(Me.lblTripDate)
        Me.pnlUnso.Controls.Add(Me.numUnsoNb)
        Me.pnlUnso.EnableStatus = False
        Me.pnlUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlUnso.HeightDef = 195
        Me.pnlUnso.Location = New System.Drawing.Point(16, 32)
        Me.pnlUnso.Name = "pnlUnso"
        Me.pnlUnso.Size = New System.Drawing.Size(1243, 195)
        Me.pnlUnso.TabIndex = 112
        Me.pnlUnso.TabStop = False
        Me.pnlUnso.Text = "運行情報"
        Me.pnlUnso.TextValue = "運行情報"
        Me.pnlUnso.WidthDef = 1243
        '
        'lblTehaisyubetsu
        '
        Me.lblTehaisyubetsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTehaisyubetsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTehaisyubetsu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTehaisyubetsu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTehaisyubetsu.CountWrappedLine = False
        Me.lblTehaisyubetsu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTehaisyubetsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTehaisyubetsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTehaisyubetsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTehaisyubetsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTehaisyubetsu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTehaisyubetsu.HeightDef = 18
        Me.lblTehaisyubetsu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTehaisyubetsu.HissuLabelVisible = False
        Me.lblTehaisyubetsu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTehaisyubetsu.IsByteCheck = 0
        Me.lblTehaisyubetsu.IsCalendarCheck = False
        Me.lblTehaisyubetsu.IsDakutenCheck = False
        Me.lblTehaisyubetsu.IsEisuCheck = False
        Me.lblTehaisyubetsu.IsForbiddenWordsCheck = False
        Me.lblTehaisyubetsu.IsFullByteCheck = 0
        Me.lblTehaisyubetsu.IsHankakuCheck = False
        Me.lblTehaisyubetsu.IsHissuCheck = False
        Me.lblTehaisyubetsu.IsKanaCheck = False
        Me.lblTehaisyubetsu.IsMiddleSpace = False
        Me.lblTehaisyubetsu.IsNumericCheck = False
        Me.lblTehaisyubetsu.IsSujiCheck = False
        Me.lblTehaisyubetsu.IsZenkakuCheck = False
        Me.lblTehaisyubetsu.ItemName = ""
        Me.lblTehaisyubetsu.LineSpace = 0
        Me.lblTehaisyubetsu.Location = New System.Drawing.Point(1124, 71)
        Me.lblTehaisyubetsu.MaxLength = 0
        Me.lblTehaisyubetsu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTehaisyubetsu.MaxLineCount = 0
        Me.lblTehaisyubetsu.Multiline = False
        Me.lblTehaisyubetsu.Name = "lblTehaisyubetsu"
        Me.lblTehaisyubetsu.ReadOnly = True
        Me.lblTehaisyubetsu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTehaisyubetsu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTehaisyubetsu.Size = New System.Drawing.Size(119, 18)
        Me.lblTehaisyubetsu.TabIndex = 356
        Me.lblTehaisyubetsu.TabStop = False
        Me.lblTehaisyubetsu.TabStopSetting = False
        Me.lblTehaisyubetsu.TextValue = "XXXX"
        Me.lblTehaisyubetsu.UseSystemPasswordChar = False
        Me.lblTehaisyubetsu.Visible = False
        Me.lblTehaisyubetsu.WidthDef = 119
        Me.lblTehaisyubetsu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbTehaisyubetsu
        '
        Me.cmbTehaisyubetsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTehaisyubetsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTehaisyubetsu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTehaisyubetsu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTehaisyubetsu.DataCode = "U027"
        Me.cmbTehaisyubetsu.DataSource = Nothing
        Me.cmbTehaisyubetsu.DisplayMember = Nothing
        Me.cmbTehaisyubetsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTehaisyubetsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTehaisyubetsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTehaisyubetsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTehaisyubetsu.HeightDef = 18
        Me.cmbTehaisyubetsu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTehaisyubetsu.HissuLabelVisible = True
        Me.cmbTehaisyubetsu.InsertWildCard = True
        Me.cmbTehaisyubetsu.IsForbiddenWordsCheck = False
        Me.cmbTehaisyubetsu.IsHissuCheck = True
        Me.cmbTehaisyubetsu.ItemName = ""
        Me.cmbTehaisyubetsu.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTehaisyubetsu.Location = New System.Drawing.Point(1006, 42)
        Me.cmbTehaisyubetsu.Name = "cmbTehaisyubetsu"
        Me.cmbTehaisyubetsu.ReadOnly = False
        Me.cmbTehaisyubetsu.SelectedIndex = -1
        Me.cmbTehaisyubetsu.SelectedItem = Nothing
        Me.cmbTehaisyubetsu.SelectedText = ""
        Me.cmbTehaisyubetsu.SelectedValue = ""
        Me.cmbTehaisyubetsu.Size = New System.Drawing.Size(120, 18)
        Me.cmbTehaisyubetsu.TabIndex = 355
        Me.cmbTehaisyubetsu.TabStopSetting = True
        Me.cmbTehaisyubetsu.TextValue = ""
        Me.cmbTehaisyubetsu.Value1 = Nothing
        Me.cmbTehaisyubetsu.Value2 = Nothing
        Me.cmbTehaisyubetsu.Value3 = Nothing
        Me.cmbTehaisyubetsu.ValueMember = Nothing
        Me.cmbTehaisyubetsu.WidthDef = 120
        '
        'btnTehaiCreate
        '
        Me.btnTehaiCreate.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnTehaiCreate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnTehaiCreate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnTehaiCreate.EnableStatus = True
        Me.btnTehaiCreate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTehaiCreate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTehaiCreate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTehaiCreate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTehaiCreate.HeightDef = 22
        Me.btnTehaiCreate.Location = New System.Drawing.Point(1125, 40)
        Me.btnTehaiCreate.Name = "btnTehaiCreate"
        Me.btnTehaiCreate.Size = New System.Drawing.Size(103, 22)
        Me.btnTehaiCreate.TabIndex = 354
        Me.btnTehaiCreate.TabStopSetting = True
        Me.btnTehaiCreate.Text = "手配作成"
        Me.btnTehaiCreate.TextValue = "手配作成"
        Me.btnTehaiCreate.UseVisualStyleBackColor = True
        Me.btnTehaiCreate.WidthDef = 103
        '
        'txtUnsocoBrCdOld
        '
        Me.txtUnsocoBrCdOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCdOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCdOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoBrCdOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoBrCdOld.CountWrappedLine = False
        Me.txtUnsocoBrCdOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoBrCdOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCdOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCdOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCdOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCdOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoBrCdOld.HeightDef = 18
        Me.txtUnsocoBrCdOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCdOld.HissuLabelVisible = False
        Me.txtUnsocoBrCdOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoBrCdOld.IsByteCheck = 3
        Me.txtUnsocoBrCdOld.IsCalendarCheck = False
        Me.txtUnsocoBrCdOld.IsDakutenCheck = False
        Me.txtUnsocoBrCdOld.IsEisuCheck = False
        Me.txtUnsocoBrCdOld.IsForbiddenWordsCheck = False
        Me.txtUnsocoBrCdOld.IsFullByteCheck = 0
        Me.txtUnsocoBrCdOld.IsHankakuCheck = False
        Me.txtUnsocoBrCdOld.IsHissuCheck = False
        Me.txtUnsocoBrCdOld.IsKanaCheck = False
        Me.txtUnsocoBrCdOld.IsMiddleSpace = False
        Me.txtUnsocoBrCdOld.IsNumericCheck = False
        Me.txtUnsocoBrCdOld.IsSujiCheck = False
        Me.txtUnsocoBrCdOld.IsZenkakuCheck = False
        Me.txtUnsocoBrCdOld.ItemName = ""
        Me.txtUnsocoBrCdOld.LineSpace = 0
        Me.txtUnsocoBrCdOld.Location = New System.Drawing.Point(1050, 116)
        Me.txtUnsocoBrCdOld.MaxLength = 3
        Me.txtUnsocoBrCdOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoBrCdOld.MaxLineCount = 0
        Me.txtUnsocoBrCdOld.Multiline = False
        Me.txtUnsocoBrCdOld.Name = "txtUnsocoBrCdOld"
        Me.txtUnsocoBrCdOld.ReadOnly = False
        Me.txtUnsocoBrCdOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoBrCdOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoBrCdOld.Size = New System.Drawing.Size(46, 18)
        Me.txtUnsocoBrCdOld.TabIndex = 298
        Me.txtUnsocoBrCdOld.TabStopSetting = True
        Me.txtUnsocoBrCdOld.TextValue = "XXX"
        Me.txtUnsocoBrCdOld.UseSystemPasswordChar = False
        Me.txtUnsocoBrCdOld.Visible = False
        Me.txtUnsocoBrCdOld.WidthDef = 46
        Me.txtUnsocoBrCdOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoCdOld
        '
        Me.txtUnsocoCdOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCdOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCdOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoCdOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoCdOld.CountWrappedLine = False
        Me.txtUnsocoCdOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoCdOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCdOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCdOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCdOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCdOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoCdOld.HeightDef = 18
        Me.txtUnsocoCdOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCdOld.HissuLabelVisible = False
        Me.txtUnsocoCdOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoCdOld.IsByteCheck = 5
        Me.txtUnsocoCdOld.IsCalendarCheck = False
        Me.txtUnsocoCdOld.IsDakutenCheck = False
        Me.txtUnsocoCdOld.IsEisuCheck = False
        Me.txtUnsocoCdOld.IsForbiddenWordsCheck = False
        Me.txtUnsocoCdOld.IsFullByteCheck = 0
        Me.txtUnsocoCdOld.IsHankakuCheck = False
        Me.txtUnsocoCdOld.IsHissuCheck = False
        Me.txtUnsocoCdOld.IsKanaCheck = False
        Me.txtUnsocoCdOld.IsMiddleSpace = False
        Me.txtUnsocoCdOld.IsNumericCheck = False
        Me.txtUnsocoCdOld.IsSujiCheck = False
        Me.txtUnsocoCdOld.IsZenkakuCheck = False
        Me.txtUnsocoCdOld.ItemName = ""
        Me.txtUnsocoCdOld.LineSpace = 0
        Me.txtUnsocoCdOld.Location = New System.Drawing.Point(1006, 116)
        Me.txtUnsocoCdOld.MaxLength = 5
        Me.txtUnsocoCdOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoCdOld.MaxLineCount = 0
        Me.txtUnsocoCdOld.Multiline = False
        Me.txtUnsocoCdOld.Name = "txtUnsocoCdOld"
        Me.txtUnsocoCdOld.ReadOnly = False
        Me.txtUnsocoCdOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoCdOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoCdOld.Size = New System.Drawing.Size(60, 18)
        Me.txtUnsocoCdOld.TabIndex = 297
        Me.txtUnsocoCdOld.TabStopSetting = True
        Me.txtUnsocoCdOld.TextValue = "XXXXX"
        Me.txtUnsocoCdOld.UseSystemPasswordChar = False
        Me.txtUnsocoCdOld.Visible = False
        Me.txtUnsocoCdOld.WidthDef = 60
        Me.txtUnsocoCdOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSyakenTrailer
        '
        Me.lblSyakenTrailer.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTrailer.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTrailer.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyakenTrailer.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSyakenTrailer.CountWrappedLine = False
        Me.lblSyakenTrailer.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSyakenTrailer.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyakenTrailer.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyakenTrailer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyakenTrailer.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyakenTrailer.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSyakenTrailer.HeightDef = 18
        Me.lblSyakenTrailer.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTrailer.HissuLabelVisible = False
        Me.lblSyakenTrailer.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSyakenTrailer.IsByteCheck = 0
        Me.lblSyakenTrailer.IsCalendarCheck = False
        Me.lblSyakenTrailer.IsDakutenCheck = False
        Me.lblSyakenTrailer.IsEisuCheck = False
        Me.lblSyakenTrailer.IsForbiddenWordsCheck = False
        Me.lblSyakenTrailer.IsFullByteCheck = 0
        Me.lblSyakenTrailer.IsHankakuCheck = False
        Me.lblSyakenTrailer.IsHissuCheck = False
        Me.lblSyakenTrailer.IsKanaCheck = False
        Me.lblSyakenTrailer.IsMiddleSpace = False
        Me.lblSyakenTrailer.IsNumericCheck = False
        Me.lblSyakenTrailer.IsSujiCheck = False
        Me.lblSyakenTrailer.IsZenkakuCheck = False
        Me.lblSyakenTrailer.ItemName = ""
        Me.lblSyakenTrailer.LineSpace = 0
        Me.lblSyakenTrailer.Location = New System.Drawing.Point(1007, 92)
        Me.lblSyakenTrailer.MaxLength = 0
        Me.lblSyakenTrailer.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSyakenTrailer.MaxLineCount = 0
        Me.lblSyakenTrailer.Multiline = False
        Me.lblSyakenTrailer.Name = "lblSyakenTrailer"
        Me.lblSyakenTrailer.ReadOnly = True
        Me.lblSyakenTrailer.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSyakenTrailer.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSyakenTrailer.Size = New System.Drawing.Size(119, 18)
        Me.lblSyakenTrailer.TabIndex = 296
        Me.lblSyakenTrailer.TabStop = False
        Me.lblSyakenTrailer.TabStopSetting = False
        Me.lblSyakenTrailer.TextValue = "XXXX"
        Me.lblSyakenTrailer.UseSystemPasswordChar = False
        Me.lblSyakenTrailer.Visible = False
        Me.lblSyakenTrailer.WidthDef = 119
        Me.lblSyakenTrailer.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSyakenTruck
        '
        Me.lblSyakenTruck.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTruck.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTruck.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyakenTruck.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSyakenTruck.CountWrappedLine = False
        Me.lblSyakenTruck.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSyakenTruck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyakenTruck.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyakenTruck.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyakenTruck.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyakenTruck.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSyakenTruck.HeightDef = 18
        Me.lblSyakenTruck.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyakenTruck.HissuLabelVisible = False
        Me.lblSyakenTruck.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSyakenTruck.IsByteCheck = 0
        Me.lblSyakenTruck.IsCalendarCheck = False
        Me.lblSyakenTruck.IsDakutenCheck = False
        Me.lblSyakenTruck.IsEisuCheck = False
        Me.lblSyakenTruck.IsForbiddenWordsCheck = False
        Me.lblSyakenTruck.IsFullByteCheck = 0
        Me.lblSyakenTruck.IsHankakuCheck = False
        Me.lblSyakenTruck.IsHissuCheck = False
        Me.lblSyakenTruck.IsKanaCheck = False
        Me.lblSyakenTruck.IsMiddleSpace = False
        Me.lblSyakenTruck.IsNumericCheck = False
        Me.lblSyakenTruck.IsSujiCheck = False
        Me.lblSyakenTruck.IsZenkakuCheck = False
        Me.lblSyakenTruck.ItemName = ""
        Me.lblSyakenTruck.LineSpace = 0
        Me.lblSyakenTruck.Location = New System.Drawing.Point(1007, 71)
        Me.lblSyakenTruck.MaxLength = 0
        Me.lblSyakenTruck.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSyakenTruck.MaxLineCount = 0
        Me.lblSyakenTruck.Multiline = False
        Me.lblSyakenTruck.Name = "lblSyakenTruck"
        Me.lblSyakenTruck.ReadOnly = True
        Me.lblSyakenTruck.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSyakenTruck.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSyakenTruck.Size = New System.Drawing.Size(119, 18)
        Me.lblSyakenTruck.TabIndex = 295
        Me.lblSyakenTruck.TabStop = False
        Me.lblSyakenTruck.TabStopSetting = False
        Me.lblSyakenTruck.TextValue = "XXXX"
        Me.lblSyakenTruck.UseSystemPasswordChar = False
        Me.lblSyakenTruck.Visible = False
        Me.lblSyakenTruck.WidthDef = 119
        Me.lblSyakenTruck.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCarKey
        '
        Me.lblCarKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarKey.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCarKey.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCarKey.CountWrappedLine = False
        Me.lblCarKey.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCarKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCarKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCarKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCarKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCarKey.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCarKey.HeightDef = 18
        Me.lblCarKey.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarKey.HissuLabelVisible = False
        Me.lblCarKey.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCarKey.IsByteCheck = 0
        Me.lblCarKey.IsCalendarCheck = False
        Me.lblCarKey.IsDakutenCheck = False
        Me.lblCarKey.IsEisuCheck = False
        Me.lblCarKey.IsForbiddenWordsCheck = False
        Me.lblCarKey.IsFullByteCheck = 0
        Me.lblCarKey.IsHankakuCheck = False
        Me.lblCarKey.IsHissuCheck = False
        Me.lblCarKey.IsKanaCheck = False
        Me.lblCarKey.IsMiddleSpace = False
        Me.lblCarKey.IsNumericCheck = False
        Me.lblCarKey.IsSujiCheck = False
        Me.lblCarKey.IsZenkakuCheck = False
        Me.lblCarKey.ItemName = ""
        Me.lblCarKey.LineSpace = 0
        Me.lblCarKey.Location = New System.Drawing.Point(425, 61)
        Me.lblCarKey.MaxLength = 0
        Me.lblCarKey.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCarKey.MaxLineCount = 0
        Me.lblCarKey.Multiline = False
        Me.lblCarKey.Name = "lblCarKey"
        Me.lblCarKey.ReadOnly = True
        Me.lblCarKey.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCarKey.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCarKey.Size = New System.Drawing.Size(119, 18)
        Me.lblCarKey.TabIndex = 129
        Me.lblCarKey.TabStop = False
        Me.lblCarKey.TabStopSetting = False
        Me.lblCarKey.TextValue = "XXXX"
        Me.lblCarKey.UseSystemPasswordChar = False
        Me.lblCarKey.WidthDef = 119
        Me.lblCarKey.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCarType
        '
        Me.lblCarType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCarType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCarType.CountWrappedLine = False
        Me.lblCarType.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCarType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCarType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCarType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCarType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCarType.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCarType.HeightDef = 18
        Me.lblCarType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCarType.HissuLabelVisible = False
        Me.lblCarType.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCarType.IsByteCheck = 0
        Me.lblCarType.IsCalendarCheck = False
        Me.lblCarType.IsDakutenCheck = False
        Me.lblCarType.IsEisuCheck = False
        Me.lblCarType.IsForbiddenWordsCheck = False
        Me.lblCarType.IsFullByteCheck = 0
        Me.lblCarType.IsHankakuCheck = False
        Me.lblCarType.IsHissuCheck = False
        Me.lblCarType.IsKanaCheck = False
        Me.lblCarType.IsMiddleSpace = False
        Me.lblCarType.IsNumericCheck = False
        Me.lblCarType.IsSujiCheck = False
        Me.lblCarType.IsZenkakuCheck = False
        Me.lblCarType.ItemName = ""
        Me.lblCarType.LineSpace = 0
        Me.lblCarType.Location = New System.Drawing.Point(311, 61)
        Me.lblCarType.MaxLength = 0
        Me.lblCarType.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCarType.MaxLineCount = 0
        Me.lblCarType.Multiline = False
        Me.lblCarType.Name = "lblCarType"
        Me.lblCarType.ReadOnly = True
        Me.lblCarType.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCarType.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCarType.Size = New System.Drawing.Size(130, 18)
        Me.lblCarType.TabIndex = 294
        Me.lblCarType.TabStop = False
        Me.lblCarType.TabStopSetting = False
        Me.lblCarType.TextValue = "トレーラ"
        Me.lblCarType.UseSystemPasswordChar = False
        Me.lblCarType.WidthDef = 130
        Me.lblCarType.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCarNo
        '
        Me.txtCarNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCarNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCarNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCarNo.CountWrappedLine = False
        Me.txtCarNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCarNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCarNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCarNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCarNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCarNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCarNo.HeightDef = 18
        Me.txtCarNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCarNo.HissuLabelVisible = False
        Me.txtCarNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCarNo.IsByteCheck = 20
        Me.txtCarNo.IsCalendarCheck = False
        Me.txtCarNo.IsDakutenCheck = False
        Me.txtCarNo.IsEisuCheck = False
        Me.txtCarNo.IsForbiddenWordsCheck = False
        Me.txtCarNo.IsFullByteCheck = 0
        Me.txtCarNo.IsHankakuCheck = False
        Me.txtCarNo.IsHissuCheck = False
        Me.txtCarNo.IsKanaCheck = False
        Me.txtCarNo.IsMiddleSpace = False
        Me.txtCarNo.IsNumericCheck = False
        Me.txtCarNo.IsSujiCheck = False
        Me.txtCarNo.IsZenkakuCheck = False
        Me.txtCarNo.ItemName = ""
        Me.txtCarNo.LineSpace = 0
        Me.txtCarNo.Location = New System.Drawing.Point(163, 61)
        Me.txtCarNo.MaxLength = 20
        Me.txtCarNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCarNo.MaxLineCount = 0
        Me.txtCarNo.Multiline = False
        Me.txtCarNo.Name = "txtCarNo"
        Me.txtCarNo.ReadOnly = False
        Me.txtCarNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCarNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCarNo.Size = New System.Drawing.Size(164, 18)
        Me.txtCarNo.TabIndex = 128
        Me.txtCarNo.TabStopSetting = True
        Me.txtCarNo.TextValue = "XXXXXXXXXXXXXXXXXXXX"
        Me.txtCarNo.UseSystemPasswordChar = False
        Me.txtCarNo.WidthDef = 164
        Me.txtCarNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleHaiso
        '
        Me.lblTitleHaiso.AutoSizeDef = False
        Me.lblTitleHaiso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHaiso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleHaiso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleHaiso.EnableStatus = False
        Me.lblTitleHaiso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHaiso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleHaiso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHaiso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleHaiso.HeightDef = 13
        Me.lblTitleHaiso.Location = New System.Drawing.Point(553, 106)
        Me.lblTitleHaiso.Name = "lblTitleHaiso"
        Me.lblTitleHaiso.Size = New System.Drawing.Size(110, 13)
        Me.lblTitleHaiso.TabIndex = 292
        Me.lblTitleHaiso.Text = "配送区分"
        Me.lblTitleHaiso.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleHaiso.TextValue = "配送区分"
        Me.lblTitleHaiso.WidthDef = 110
        '
        'cmbHaiso
        '
        Me.cmbHaiso.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHaiso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbHaiso.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbHaiso.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbHaiso.DataCode = "H016"
        Me.cmbHaiso.DataSource = Nothing
        Me.cmbHaiso.DisplayMember = Nothing
        Me.cmbHaiso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHaiso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbHaiso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHaiso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbHaiso.HeightDef = 18
        Me.cmbHaiso.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbHaiso.HissuLabelVisible = False
        Me.cmbHaiso.InsertWildCard = True
        Me.cmbHaiso.IsForbiddenWordsCheck = False
        Me.cmbHaiso.IsHissuCheck = False
        Me.cmbHaiso.ItemName = ""
        Me.cmbHaiso.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbHaiso.Location = New System.Drawing.Point(663, 103)
        Me.cmbHaiso.Name = "cmbHaiso"
        Me.cmbHaiso.ReadOnly = False
        Me.cmbHaiso.SelectedIndex = -1
        Me.cmbHaiso.SelectedItem = Nothing
        Me.cmbHaiso.SelectedText = ""
        Me.cmbHaiso.SelectedValue = ""
        Me.cmbHaiso.Size = New System.Drawing.Size(113, 18)
        Me.cmbHaiso.TabIndex = 291
        Me.cmbHaiso.TabStopSetting = True
        Me.cmbHaiso.TextValue = ""
        Me.cmbHaiso.Value1 = Nothing
        Me.cmbHaiso.Value2 = Nothing
        Me.cmbHaiso.Value3 = Nothing
        Me.cmbHaiso.ValueMember = Nothing
        Me.cmbHaiso.WidthDef = 113
        '
        'numUnsoNb
        '
        Me.numUnsoNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoNb.HeightDef = 18
        Me.numUnsoNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoNb.HissuLabelVisible = False
        Me.numUnsoNb.IsHissuCheck = False
        Me.numUnsoNb.IsRangeCheck = False
        Me.numUnsoNb.ItemName = ""
        Me.numUnsoNb.Location = New System.Drawing.Point(163, 168)
        Me.numUnsoNb.Name = "numUnsoNb"
        Me.numUnsoNb.ReadOnly = True
        Me.numUnsoNb.Size = New System.Drawing.Size(164, 18)
        Me.numUnsoNb.TabIndex = 290
        Me.numUnsoNb.TabStop = False
        Me.numUnsoNb.TabStopSetting = False
        Me.numUnsoNb.TextValue = "99,999"
        Me.numUnsoNb.Value = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numUnsoNb.WidthDef = 164
        '
        'lblTitleUnsoNb
        '
        Me.lblTitleUnsoNb.AutoSizeDef = False
        Me.lblTitleUnsoNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoNb.EnableStatus = False
        Me.lblTitleUnsoNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoNb.HeightDef = 16
        Me.lblTitleUnsoNb.Location = New System.Drawing.Point(81, 168)
        Me.lblTitleUnsoNb.Name = "lblTitleUnsoNb"
        Me.lblTitleUnsoNb.Size = New System.Drawing.Size(82, 16)
        Me.lblTitleUnsoNb.TabIndex = 289
        Me.lblTitleUnsoNb.Text = "運送個数"
        Me.lblTitleUnsoNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoNb.TextValue = "運送個数"
        Me.lblTitleUnsoNb.WidthDef = 82
        '
        'lblTitleYen2
        '
        Me.lblTitleYen2.AutoSize = True
        Me.lblTitleYen2.AutoSizeDef = True
        Me.lblTitleYen2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen2.EnableStatus = False
        Me.lblTitleYen2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen2.HeightDef = 13
        Me.lblTitleYen2.Location = New System.Drawing.Point(1004, 170)
        Me.lblTitleYen2.Name = "lblTitleYen2"
        Me.lblTitleYen2.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen2.TabIndex = 288
        Me.lblTitleYen2.Text = "円"
        Me.lblTitleYen2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen2.TextValue = "円"
        Me.lblTitleYen2.WidthDef = 21
        '
        'lblTitleYen1
        '
        Me.lblTitleYen1.AutoSize = True
        Me.lblTitleYen1.AutoSizeDef = True
        Me.lblTitleYen1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen1.EnableStatus = False
        Me.lblTitleYen1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen1.HeightDef = 13
        Me.lblTitleYen1.Location = New System.Drawing.Point(1004, 150)
        Me.lblTitleYen1.Name = "lblTitleYen1"
        Me.lblTitleYen1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen1.TabIndex = 287
        Me.lblTitleYen1.Text = "円"
        Me.lblTitleYen1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen1.TextValue = "円"
        Me.lblTitleYen1.WidthDef = 21
        '
        'lblTitleUnderUnsoInfor
        '
        Me.lblTitleUnderUnsoInfor.AutoSize = True
        Me.lblTitleUnderUnsoInfor.AutoSizeDef = True
        Me.lblTitleUnderUnsoInfor.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnderUnsoInfor.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnderUnsoInfor.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnderUnsoInfor.EnableStatus = False
        Me.lblTitleUnderUnsoInfor.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnderUnsoInfor.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnderUnsoInfor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnderUnsoInfor.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnderUnsoInfor.HeightDef = 13
        Me.lblTitleUnderUnsoInfor.Location = New System.Drawing.Point(4, 171)
        Me.lblTitleUnderUnsoInfor.Name = "lblTitleUnderUnsoInfor"
        Me.lblTitleUnderUnsoInfor.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleUnderUnsoInfor.TabIndex = 286
        Me.lblTitleUnderUnsoInfor.Text = "（配下運送）"
        Me.lblTitleUnderUnsoInfor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnderUnsoInfor.TextValue = "（配下運送）"
        Me.lblTitleUnderUnsoInfor.WidthDef = 91
        '
        'lblTitleUncoInfor
        '
        Me.lblTitleUncoInfor.AutoSize = True
        Me.lblTitleUncoInfor.AutoSizeDef = True
        Me.lblTitleUncoInfor.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUncoInfor.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUncoInfor.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUncoInfor.EnableStatus = False
        Me.lblTitleUncoInfor.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUncoInfor.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUncoInfor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUncoInfor.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUncoInfor.HeightDef = 13
        Me.lblTitleUncoInfor.Location = New System.Drawing.Point(5, 152)
        Me.lblTitleUncoInfor.Name = "lblTitleUncoInfor"
        Me.lblTitleUncoInfor.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleUncoInfor.TabIndex = 285
        Me.lblTitleUncoInfor.Text = "（運行）"
        Me.lblTitleUncoInfor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUncoInfor.TextValue = "（運行）"
        Me.lblTitleUncoInfor.WidthDef = 63
        '
        'numRevUnchin
        '
        Me.numRevUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRevUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRevUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numRevUnchin.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numRevUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRevUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numRevUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRevUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numRevUnchin.HeightDef = 18
        Me.numRevUnchin.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numRevUnchin.HissuLabelVisible = False
        Me.numRevUnchin.IsHissuCheck = False
        Me.numRevUnchin.IsRangeCheck = False
        Me.numRevUnchin.ItemName = ""
        Me.numRevUnchin.Location = New System.Drawing.Point(855, 168)
        Me.numRevUnchin.Name = "numRevUnchin"
        Me.numRevUnchin.ReadOnly = True
        Me.numRevUnchin.Size = New System.Drawing.Size(164, 18)
        Me.numRevUnchin.TabIndex = 284
        Me.numRevUnchin.TabStop = False
        Me.numRevUnchin.TabStopSetting = False
        Me.numRevUnchin.TextValue = "0"
        Me.numRevUnchin.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numRevUnchin.WidthDef = 164
        '
        'lblTitleRevUnchin
        '
        Me.lblTitleRevUnchin.AutoSizeDef = False
        Me.lblTitleRevUnchin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRevUnchin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRevUnchin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRevUnchin.EnableStatus = False
        Me.lblTitleRevUnchin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRevUnchin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRevUnchin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRevUnchin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRevUnchin.HeightDef = 13
        Me.lblTitleRevUnchin.Location = New System.Drawing.Point(760, 171)
        Me.lblTitleRevUnchin.Name = "lblTitleRevUnchin"
        Me.lblTitleRevUnchin.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleRevUnchin.TabIndex = 283
        Me.lblTitleRevUnchin.Text = "請求運賃"
        Me.lblTitleRevUnchin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRevUnchin.TextValue = "請求運賃"
        Me.lblTitleRevUnchin.WidthDef = 91
        '
        'numPayAmt
        '
        Me.numPayAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPayAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPayAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPayAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPayAmt.HeightDef = 18
        Me.numPayAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPayAmt.HissuLabelVisible = False
        Me.numPayAmt.IsHissuCheck = False
        Me.numPayAmt.IsRangeCheck = False
        Me.numPayAmt.ItemName = ""
        Me.numPayAmt.Location = New System.Drawing.Point(855, 147)
        Me.numPayAmt.Name = "numPayAmt"
        Me.numPayAmt.ReadOnly = True
        Me.numPayAmt.Size = New System.Drawing.Size(164, 18)
        Me.numPayAmt.TabIndex = 282
        Me.numPayAmt.TabStop = False
        Me.numPayAmt.TabStopSetting = False
        Me.numPayAmt.TextValue = "0"
        Me.numPayAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPayAmt.WidthDef = 164
        '
        'lblTitlePayAmt
        '
        Me.lblTitlePayAmt.AutoSizeDef = False
        Me.lblTitlePayAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitlePayAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitlePayAmt.EnableStatus = False
        Me.lblTitlePayAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitlePayAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitlePayAmt.HeightDef = 13
        Me.lblTitlePayAmt.Location = New System.Drawing.Point(788, 150)
        Me.lblTitlePayAmt.Name = "lblTitlePayAmt"
        Me.lblTitlePayAmt.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePayAmt.TabIndex = 281
        Me.lblTitlePayAmt.Text = "下払金額"
        Me.lblTitlePayAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePayAmt.TextValue = "下払金額"
        Me.lblTitlePayAmt.WidthDef = 63
        '
        'lblTitleDo3
        '
        Me.lblTitleDo3.AutoSize = True
        Me.lblTitleDo3.AutoSizeDef = True
        Me.lblTitleDo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDo3.EnableStatus = False
        Me.lblTitleDo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo3.HeightDef = 13
        Me.lblTitleDo3.Location = New System.Drawing.Point(744, 150)
        Me.lblTitleDo3.Name = "lblTitleDo3"
        Me.lblTitleDo3.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleDo3.TabIndex = 280
        Me.lblTitleDo3.Text = "℃"
        Me.lblTitleDo3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDo3.TextValue = "℃"
        Me.lblTitleDo3.WidthDef = 21
        '
        'lblTitleUnsoOndo
        '
        Me.lblTitleUnsoOndo.AutoSizeDef = False
        Me.lblTitleUnsoOndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoOndo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoOndo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoOndo.EnableStatus = False
        Me.lblTitleUnsoOndo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoOndo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoOndo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoOndo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoOndo.HeightDef = 13
        Me.lblTitleUnsoOndo.Location = New System.Drawing.Point(594, 150)
        Me.lblTitleUnsoOndo.Name = "lblTitleUnsoOndo"
        Me.lblTitleUnsoOndo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleUnsoOndo.TabIndex = 279
        Me.lblTitleUnsoOndo.Text = "運送管理温度"
        Me.lblTitleUnsoOndo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoOndo.TextValue = "運送管理温度"
        Me.lblTitleUnsoOndo.WidthDef = 91
        '
        'numUnsoOndo
        '
        Me.numUnsoOndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoOndo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numUnsoOndo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numUnsoOndo.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numUnsoOndo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoOndo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numUnsoOndo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoOndo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numUnsoOndo.HeightDef = 18
        Me.numUnsoOndo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numUnsoOndo.HissuLabelVisible = False
        Me.numUnsoOndo.IsHissuCheck = False
        Me.numUnsoOndo.IsRangeCheck = False
        Me.numUnsoOndo.ItemName = ""
        Me.numUnsoOndo.Location = New System.Drawing.Point(685, 147)
        Me.numUnsoOndo.Name = "numUnsoOndo"
        Me.numUnsoOndo.ReadOnly = False
        Me.numUnsoOndo.Size = New System.Drawing.Size(74, 18)
        Me.numUnsoOndo.TabIndex = 278
        Me.numUnsoOndo.TabStopSetting = True
        Me.numUnsoOndo.TextValue = "0"
        Me.numUnsoOndo.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numUnsoOndo.WidthDef = 74
        '
        'lblTitleKg2
        '
        Me.lblTitleKg2.AutoSize = True
        Me.lblTitleKg2.AutoSizeDef = True
        Me.lblTitleKg2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg2.EnableStatus = False
        Me.lblTitleKg2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg2.HeightDef = 13
        Me.lblTitleKg2.Location = New System.Drawing.Point(550, 170)
        Me.lblTitleKg2.Name = "lblTitleKg2"
        Me.lblTitleKg2.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg2.TabIndex = 277
        Me.lblTitleKg2.Text = "KG"
        Me.lblTitleKg2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg2.TextValue = "KG"
        Me.lblTitleKg2.WidthDef = 21
        '
        'numTripWt
        '
        Me.numTripWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTripWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTripWt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numTripWt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numTripWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTripWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numTripWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTripWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numTripWt.HeightDef = 18
        Me.numTripWt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numTripWt.HissuLabelVisible = False
        Me.numTripWt.IsHissuCheck = False
        Me.numTripWt.IsRangeCheck = False
        Me.numTripWt.ItemName = ""
        Me.numTripWt.Location = New System.Drawing.Point(401, 168)
        Me.numTripWt.Name = "numTripWt"
        Me.numTripWt.ReadOnly = True
        Me.numTripWt.Size = New System.Drawing.Size(164, 18)
        Me.numTripWt.TabIndex = 276
        Me.numTripWt.TabStop = False
        Me.numTripWt.TabStopSetting = False
        Me.numTripWt.TextValue = "99,999"
        Me.numTripWt.Value = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numTripWt.WidthDef = 164
        '
        'lblTitleTripWt
        '
        Me.lblTitleTripWt.AutoSizeDef = False
        Me.lblTitleTripWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripWt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTripWt.EnableStatus = False
        Me.lblTitleTripWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripWt.HeightDef = 13
        Me.lblTitleTripWt.Location = New System.Drawing.Point(314, 171)
        Me.lblTitleTripWt.Name = "lblTitleTripWt"
        Me.lblTitleTripWt.Size = New System.Drawing.Size(87, 13)
        Me.lblTitleTripWt.TabIndex = 275
        Me.lblTitleTripWt.Text = "運送重量"
        Me.lblTitleTripWt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTripWt.TextValue = "運送重量"
        Me.lblTitleTripWt.WidthDef = 87
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
        Me.lblTitleKg1.Location = New System.Drawing.Point(550, 149)
        Me.lblTitleKg1.Name = "lblTitleKg1"
        Me.lblTitleKg1.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg1.TabIndex = 274
        Me.lblTitleKg1.Text = "KG"
        Me.lblTitleKg1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg1.TextValue = "KG"
        Me.lblTitleKg1.WidthDef = 21
        '
        'numLoadWt
        '
        Me.numLoadWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numLoadWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numLoadWt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numLoadWt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numLoadWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numLoadWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numLoadWt.HeightDef = 18
        Me.numLoadWt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numLoadWt.HissuLabelVisible = False
        Me.numLoadWt.IsHissuCheck = False
        Me.numLoadWt.IsRangeCheck = False
        Me.numLoadWt.ItemName = ""
        Me.numLoadWt.Location = New System.Drawing.Point(401, 147)
        Me.numLoadWt.Name = "numLoadWt"
        Me.numLoadWt.ReadOnly = True
        Me.numLoadWt.Size = New System.Drawing.Size(164, 18)
        Me.numLoadWt.TabIndex = 273
        Me.numLoadWt.TabStop = False
        Me.numLoadWt.TabStopSetting = False
        Me.numLoadWt.TextValue = "99,999"
        Me.numLoadWt.Value = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numLoadWt.WidthDef = 164
        '
        'lblTitleLoadWt
        '
        Me.lblTitleLoadWt.AutoSizeDef = False
        Me.lblTitleLoadWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoadWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoadWt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLoadWt.EnableStatus = False
        Me.lblTitleLoadWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoadWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoadWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoadWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoadWt.HeightDef = 13
        Me.lblTitleLoadWt.Location = New System.Drawing.Point(311, 150)
        Me.lblTitleLoadWt.Name = "lblTitleLoadWt"
        Me.lblTitleLoadWt.Size = New System.Drawing.Size(90, 13)
        Me.lblTitleLoadWt.TabIndex = 272
        Me.lblTitleLoadWt.Text = "積載重量"
        Me.lblTitleLoadWt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLoadWt.TextValue = "積載重量"
        Me.lblTitleLoadWt.WidthDef = 90
        '
        'lblTitleRem
        '
        Me.lblTitleRem.AutoSizeDef = False
        Me.lblTitleRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRem.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRem.EnableStatus = False
        Me.lblTitleRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRem.HeightDef = 13
        Me.lblTitleRem.Location = New System.Drawing.Point(92, 126)
        Me.lblTitleRem.Name = "lblTitleRem"
        Me.lblTitleRem.Size = New System.Drawing.Size(71, 13)
        Me.lblTitleRem.TabIndex = 270
        Me.lblTitleRem.Text = "備考"
        Me.lblTitleRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRem.TextValue = "備考"
        Me.lblTitleRem.WidthDef = 71
        '
        'txtRem
        '
        Me.txtRem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRem.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRem.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRem.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRem.CountWrappedLine = False
        Me.txtRem.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRem.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRem.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRem.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRem.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRem.HeightDef = 18
        Me.txtRem.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRem.HissuLabelVisible = False
        Me.txtRem.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRem.IsByteCheck = 100
        Me.txtRem.IsCalendarCheck = False
        Me.txtRem.IsDakutenCheck = False
        Me.txtRem.IsEisuCheck = False
        Me.txtRem.IsForbiddenWordsCheck = False
        Me.txtRem.IsFullByteCheck = 0
        Me.txtRem.IsHankakuCheck = False
        Me.txtRem.IsHissuCheck = False
        Me.txtRem.IsKanaCheck = False
        Me.txtRem.IsMiddleSpace = False
        Me.txtRem.IsNumericCheck = False
        Me.txtRem.IsSujiCheck = False
        Me.txtRem.IsZenkakuCheck = False
        Me.txtRem.ItemName = ""
        Me.txtRem.LineSpace = 0
        Me.txtRem.Location = New System.Drawing.Point(163, 124)
        Me.txtRem.MaxLength = 100
        Me.txtRem.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRem.MaxLineCount = 0
        Me.txtRem.Multiline = False
        Me.txtRem.Name = "txtRem"
        Me.txtRem.ReadOnly = False
        Me.txtRem.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRem.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRem.Size = New System.Drawing.Size(749, 18)
        Me.txtRem.TabIndex = 269
        Me.txtRem.TabStopSetting = True
        Me.txtRem.TextValue = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" & _
    "XXXXXXXXXXXXXXXXXXX"
        Me.txtRem.UseSystemPasswordChar = False
        Me.txtRem.WidthDef = 749
        Me.txtRem.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblDriverNm.Location = New System.Drawing.Point(760, 82)
        Me.lblDriverNm.MaxLength = 0
        Me.lblDriverNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblDriverNm.MaxLineCount = 0
        Me.lblDriverNm.Multiline = False
        Me.lblDriverNm.Name = "lblDriverNm"
        Me.lblDriverNm.ReadOnly = True
        Me.lblDriverNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblDriverNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblDriverNm.Size = New System.Drawing.Size(175, 18)
        Me.lblDriverNm.TabIndex = 268
        Me.lblDriverNm.TabStop = False
        Me.lblDriverNm.TabStopSetting = False
        Me.lblDriverNm.TextValue = "XXXXXXXXXXXXXXXXXXXX"
        Me.lblDriverNm.UseSystemPasswordChar = False
        Me.lblDriverNm.WidthDef = 175
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
        Me.lblTitleDriver.Location = New System.Drawing.Point(614, 85)
        Me.lblTitleDriver.Name = "lblTitleDriver"
        Me.lblTitleDriver.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleDriver.TabIndex = 267
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
        Me.txtDriverCd.Location = New System.Drawing.Point(663, 82)
        Me.txtDriverCd.MaxLength = 5
        Me.txtDriverCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDriverCd.MaxLineCount = 0
        Me.txtDriverCd.Multiline = False
        Me.txtDriverCd.Name = "txtDriverCd"
        Me.txtDriverCd.ReadOnly = False
        Me.txtDriverCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDriverCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDriverCd.Size = New System.Drawing.Size(113, 18)
        Me.txtDriverCd.TabIndex = 266
        Me.txtDriverCd.TabStopSetting = True
        Me.txtDriverCd.TextValue = "XXXXX"
        Me.txtDriverCd.UseSystemPasswordChar = False
        Me.txtDriverCd.WidthDef = 113
        Me.txtDriverCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleJshaKbn
        '
        Me.lblTitleJshaKbn.AutoSizeDef = False
        Me.lblTitleJshaKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJshaKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJshaKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJshaKbn.EnableStatus = False
        Me.lblTitleJshaKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJshaKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJshaKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJshaKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJshaKbn.HeightDef = 13
        Me.lblTitleJshaKbn.Location = New System.Drawing.Point(64, 106)
        Me.lblTitleJshaKbn.Name = "lblTitleJshaKbn"
        Me.lblTitleJshaKbn.Size = New System.Drawing.Size(99, 13)
        Me.lblTitleJshaKbn.TabIndex = 265
        Me.lblTitleJshaKbn.Text = "自傭車区分"
        Me.lblTitleJshaKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJshaKbn.TextValue = "自傭車区分"
        Me.lblTitleJshaKbn.WidthDef = 99
        '
        'cmbJshaKbn
        '
        Me.cmbJshaKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJshaKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJshaKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbJshaKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbJshaKbn.DataCode = "J002"
        Me.cmbJshaKbn.DataSource = Nothing
        Me.cmbJshaKbn.DisplayMember = Nothing
        Me.cmbJshaKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJshaKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJshaKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJshaKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJshaKbn.HeightDef = 18
        Me.cmbJshaKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJshaKbn.HissuLabelVisible = True
        Me.cmbJshaKbn.InsertWildCard = True
        Me.cmbJshaKbn.IsForbiddenWordsCheck = False
        Me.cmbJshaKbn.IsHissuCheck = True
        Me.cmbJshaKbn.ItemName = ""
        Me.cmbJshaKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbJshaKbn.Location = New System.Drawing.Point(163, 103)
        Me.cmbJshaKbn.Name = "cmbJshaKbn"
        Me.cmbJshaKbn.ReadOnly = False
        Me.cmbJshaKbn.SelectedIndex = -1
        Me.cmbJshaKbn.SelectedItem = Nothing
        Me.cmbJshaKbn.SelectedText = ""
        Me.cmbJshaKbn.SelectedValue = ""
        Me.cmbJshaKbn.Size = New System.Drawing.Size(90, 18)
        Me.cmbJshaKbn.TabIndex = 264
        Me.cmbJshaKbn.TabStopSetting = True
        Me.cmbJshaKbn.TextValue = ""
        Me.cmbJshaKbn.Value1 = Nothing
        Me.cmbJshaKbn.Value2 = Nothing
        Me.cmbJshaKbn.Value3 = Nothing
        Me.cmbJshaKbn.ValueMember = Nothing
        Me.cmbJshaKbn.WidthDef = 90
        '
        'lblUnsocoNm
        '
        Me.lblUnsocoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnsocoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUnsocoNm.CountWrappedLine = False
        Me.lblUnsocoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblUnsocoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnsocoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnsocoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblUnsocoNm.HeightDef = 18
        Me.lblUnsocoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblUnsocoNm.HissuLabelVisible = True
        Me.lblUnsocoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblUnsocoNm.IsByteCheck = 0
        Me.lblUnsocoNm.IsCalendarCheck = False
        Me.lblUnsocoNm.IsDakutenCheck = False
        Me.lblUnsocoNm.IsEisuCheck = False
        Me.lblUnsocoNm.IsForbiddenWordsCheck = False
        Me.lblUnsocoNm.IsFullByteCheck = 0
        Me.lblUnsocoNm.IsHankakuCheck = False
        Me.lblUnsocoNm.IsHissuCheck = True
        Me.lblUnsocoNm.IsKanaCheck = False
        Me.lblUnsocoNm.IsMiddleSpace = False
        Me.lblUnsocoNm.IsNumericCheck = False
        Me.lblUnsocoNm.IsSujiCheck = False
        Me.lblUnsocoNm.IsZenkakuCheck = False
        Me.lblUnsocoNm.ItemName = ""
        Me.lblUnsocoNm.LineSpace = 0
        Me.lblUnsocoNm.Location = New System.Drawing.Point(237, 82)
        Me.lblUnsocoNm.MaxLength = 0
        Me.lblUnsocoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblUnsocoNm.MaxLineCount = 0
        Me.lblUnsocoNm.Multiline = False
        Me.lblUnsocoNm.Name = "lblUnsocoNm"
        Me.lblUnsocoNm.ReadOnly = True
        Me.lblUnsocoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblUnsocoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblUnsocoNm.Size = New System.Drawing.Size(307, 18)
        Me.lblUnsocoNm.TabIndex = 263
        Me.lblUnsocoNm.TabStop = False
        Me.lblUnsocoNm.TabStopSetting = False
        Me.lblUnsocoNm.TextValue = "XXXXXXXXX"
        Me.lblUnsocoNm.UseSystemPasswordChar = False
        Me.lblUnsocoNm.WidthDef = 307
        Me.lblUnsocoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoBrCd
        '
        Me.txtUnsocoBrCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoBrCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoBrCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoBrCd.CountWrappedLine = False
        Me.txtUnsocoBrCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoBrCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoBrCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoBrCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoBrCd.HeightDef = 18
        Me.txtUnsocoBrCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoBrCd.HissuLabelVisible = False
        Me.txtUnsocoBrCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoBrCd.IsByteCheck = 3
        Me.txtUnsocoBrCd.IsCalendarCheck = False
        Me.txtUnsocoBrCd.IsDakutenCheck = False
        Me.txtUnsocoBrCd.IsEisuCheck = False
        Me.txtUnsocoBrCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoBrCd.IsFullByteCheck = 0
        Me.txtUnsocoBrCd.IsHankakuCheck = False
        Me.txtUnsocoBrCd.IsHissuCheck = False
        Me.txtUnsocoBrCd.IsKanaCheck = False
        Me.txtUnsocoBrCd.IsMiddleSpace = False
        Me.txtUnsocoBrCd.IsNumericCheck = False
        Me.txtUnsocoBrCd.IsSujiCheck = False
        Me.txtUnsocoBrCd.IsZenkakuCheck = False
        Me.txtUnsocoBrCd.ItemName = ""
        Me.txtUnsocoBrCd.LineSpace = 0
        Me.txtUnsocoBrCd.Location = New System.Drawing.Point(207, 82)
        Me.txtUnsocoBrCd.MaxLength = 3
        Me.txtUnsocoBrCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoBrCd.MaxLineCount = 0
        Me.txtUnsocoBrCd.Multiline = False
        Me.txtUnsocoBrCd.Name = "txtUnsocoBrCd"
        Me.txtUnsocoBrCd.ReadOnly = False
        Me.txtUnsocoBrCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoBrCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoBrCd.Size = New System.Drawing.Size(46, 18)
        Me.txtUnsocoBrCd.TabIndex = 262
        Me.txtUnsocoBrCd.TabStopSetting = True
        Me.txtUnsocoBrCd.TextValue = "XXX"
        Me.txtUnsocoBrCd.UseSystemPasswordChar = False
        Me.txtUnsocoBrCd.WidthDef = 46
        Me.txtUnsocoBrCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUnsocoCd
        '
        Me.txtUnsocoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtUnsocoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnsocoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUnsocoCd.CountWrappedLine = False
        Me.txtUnsocoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUnsocoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUnsocoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUnsocoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUnsocoCd.HeightDef = 18
        Me.txtUnsocoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUnsocoCd.HissuLabelVisible = False
        Me.txtUnsocoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtUnsocoCd.IsByteCheck = 5
        Me.txtUnsocoCd.IsCalendarCheck = False
        Me.txtUnsocoCd.IsDakutenCheck = False
        Me.txtUnsocoCd.IsEisuCheck = False
        Me.txtUnsocoCd.IsForbiddenWordsCheck = False
        Me.txtUnsocoCd.IsFullByteCheck = 0
        Me.txtUnsocoCd.IsHankakuCheck = False
        Me.txtUnsocoCd.IsHissuCheck = False
        Me.txtUnsocoCd.IsKanaCheck = False
        Me.txtUnsocoCd.IsMiddleSpace = False
        Me.txtUnsocoCd.IsNumericCheck = False
        Me.txtUnsocoCd.IsSujiCheck = False
        Me.txtUnsocoCd.IsZenkakuCheck = False
        Me.txtUnsocoCd.ItemName = ""
        Me.txtUnsocoCd.LineSpace = 0
        Me.txtUnsocoCd.Location = New System.Drawing.Point(163, 82)
        Me.txtUnsocoCd.MaxLength = 5
        Me.txtUnsocoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUnsocoCd.MaxLineCount = 0
        Me.txtUnsocoCd.Multiline = False
        Me.txtUnsocoCd.Name = "txtUnsocoCd"
        Me.txtUnsocoCd.ReadOnly = False
        Me.txtUnsocoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUnsocoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUnsocoCd.Size = New System.Drawing.Size(60, 18)
        Me.txtUnsocoCd.TabIndex = 261
        Me.txtUnsocoCd.TabStopSetting = True
        Me.txtUnsocoCd.TextValue = "XXXXX"
        Me.txtUnsocoCd.UseSystemPasswordChar = False
        Me.txtUnsocoCd.WidthDef = 60
        Me.txtUnsocoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleUnsoco
        '
        Me.lblTitleUnsoco.AutoSizeDef = False
        Me.lblTitleUnsoco.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleUnsoco.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleUnsoco.EnableStatus = False
        Me.lblTitleUnsoco.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleUnsoco.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleUnsoco.HeightDef = 13
        Me.lblTitleUnsoco.Location = New System.Drawing.Point(75, 85)
        Me.lblTitleUnsoco.Name = "lblTitleUnsoco"
        Me.lblTitleUnsoco.Size = New System.Drawing.Size(88, 13)
        Me.lblTitleUnsoco.TabIndex = 260
        Me.lblTitleUnsoco.Text = "運送会社"
        Me.lblTitleUnsoco.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleUnsoco.TextValue = "運送会社"
        Me.lblTitleUnsoco.WidthDef = 88
        '
        'lblTitleDo2
        '
        Me.lblTitleDo2.AutoSize = True
        Me.lblTitleDo2.AutoSizeDef = True
        Me.lblTitleDo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDo2.EnableStatus = False
        Me.lblTitleDo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo2.HeightDef = 13
        Me.lblTitleDo2.Location = New System.Drawing.Point(825, 64)
        Me.lblTitleDo2.Name = "lblTitleDo2"
        Me.lblTitleDo2.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleDo2.TabIndex = 259
        Me.lblTitleDo2.Text = "℃"
        Me.lblTitleDo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDo2.TextValue = "℃"
        Me.lblTitleDo2.WidthDef = 21
        '
        'numOndoMm
        '
        Me.numOndoMm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numOndoMm.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numOndoMm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOndoMm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOndoMm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOndoMm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOndoMm.HeightDef = 18
        Me.numOndoMm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMm.HissuLabelVisible = False
        Me.numOndoMm.IsHissuCheck = False
        Me.numOndoMm.IsRangeCheck = False
        Me.numOndoMm.ItemName = ""
        Me.numOndoMm.Location = New System.Drawing.Point(766, 61)
        Me.numOndoMm.Name = "numOndoMm"
        Me.numOndoMm.ReadOnly = True
        Me.numOndoMm.Size = New System.Drawing.Size(74, 18)
        Me.numOndoMm.TabIndex = 258
        Me.numOndoMm.TabStop = False
        Me.numOndoMm.TabStopSetting = False
        Me.numOndoMm.TextValue = "0"
        Me.numOndoMm.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numOndoMm.WidthDef = 74
        '
        'lblTitleDo1
        '
        Me.lblTitleDo1.AutoSize = True
        Me.lblTitleDo1.AutoSizeDef = True
        Me.lblTitleDo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDo1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDo1.EnableStatus = False
        Me.lblTitleDo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDo1.HeightDef = 13
        Me.lblTitleDo1.Location = New System.Drawing.Point(722, 63)
        Me.lblTitleDo1.Name = "lblTitleDo1"
        Me.lblTitleDo1.Size = New System.Drawing.Size(42, 13)
        Me.lblTitleDo1.TabIndex = 257
        Me.lblTitleDo1.Text = "℃ ～"
        Me.lblTitleDo1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDo1.TextValue = "℃ ～"
        Me.lblTitleDo1.WidthDef = 42
        '
        'numOndoMx
        '
        Me.numOndoMx.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMx.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMx.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numOndoMx.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numOndoMx.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOndoMx.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numOndoMx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOndoMx.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numOndoMx.HeightDef = 18
        Me.numOndoMx.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numOndoMx.HissuLabelVisible = False
        Me.numOndoMx.IsHissuCheck = False
        Me.numOndoMx.IsRangeCheck = False
        Me.numOndoMx.ItemName = ""
        Me.numOndoMx.Location = New System.Drawing.Point(663, 61)
        Me.numOndoMx.Name = "numOndoMx"
        Me.numOndoMx.ReadOnly = True
        Me.numOndoMx.Size = New System.Drawing.Size(74, 18)
        Me.numOndoMx.TabIndex = 254
        Me.numOndoMx.TabStop = False
        Me.numOndoMx.TabStopSetting = False
        Me.numOndoMx.TextValue = "0"
        Me.numOndoMx.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numOndoMx.WidthDef = 74
        '
        'lblTitleCarOndo
        '
        Me.lblTitleCarOndo.AutoSizeDef = False
        Me.lblTitleCarOndo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCarOndo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCarOndo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCarOndo.EnableStatus = False
        Me.lblTitleCarOndo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCarOndo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCarOndo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCarOndo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCarOndo.HeightDef = 13
        Me.lblTitleCarOndo.Location = New System.Drawing.Point(572, 64)
        Me.lblTitleCarOndo.Name = "lblTitleCarOndo"
        Me.lblTitleCarOndo.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleCarOndo.TabIndex = 130
        Me.lblTitleCarOndo.Text = "車輌設定温度"
        Me.lblTitleCarOndo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCarOndo.TextValue = "車輌設定温度"
        Me.lblTitleCarOndo.WidthDef = 91
        '
        'lblTitleCarNo
        '
        Me.lblTitleCarNo.AutoSizeDef = False
        Me.lblTitleCarNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCarNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleCarNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleCarNo.EnableStatus = False
        Me.lblTitleCarNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCarNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleCarNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCarNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleCarNo.HeightDef = 13
        Me.lblTitleCarNo.Location = New System.Drawing.Point(103, 64)
        Me.lblTitleCarNo.Name = "lblTitleCarNo"
        Me.lblTitleCarNo.Size = New System.Drawing.Size(60, 13)
        Me.lblTitleCarNo.TabIndex = 127
        Me.lblTitleCarNo.Text = "車番"
        Me.lblTitleCarNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCarNo.TextValue = "車番"
        Me.lblTitleCarNo.WidthDef = 60
        '
        'cmbBinKbn
        '
        Me.cmbBinKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBinKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBinKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBinKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBinKbn.DataCode = "U001"
        Me.cmbBinKbn.DataSource = Nothing
        Me.cmbBinKbn.DisplayMember = Nothing
        Me.cmbBinKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBinKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBinKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBinKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBinKbn.HeightDef = 18
        Me.cmbBinKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBinKbn.HissuLabelVisible = True
        Me.cmbBinKbn.InsertWildCard = True
        Me.cmbBinKbn.IsForbiddenWordsCheck = False
        Me.cmbBinKbn.IsHissuCheck = True
        Me.cmbBinKbn.ItemName = ""
        Me.cmbBinKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbBinKbn.Location = New System.Drawing.Point(663, 40)
        Me.cmbBinKbn.Name = "cmbBinKbn"
        Me.cmbBinKbn.ReadOnly = False
        Me.cmbBinKbn.SelectedIndex = -1
        Me.cmbBinKbn.SelectedItem = Nothing
        Me.cmbBinKbn.SelectedText = ""
        Me.cmbBinKbn.SelectedValue = ""
        Me.cmbBinKbn.Size = New System.Drawing.Size(113, 18)
        Me.cmbBinKbn.TabIndex = 126
        Me.cmbBinKbn.TabStopSetting = True
        Me.cmbBinKbn.TextValue = ""
        Me.cmbBinKbn.Value1 = Nothing
        Me.cmbBinKbn.Value2 = Nothing
        Me.cmbBinKbn.Value3 = Nothing
        Me.cmbBinKbn.ValueMember = Nothing
        Me.cmbBinKbn.WidthDef = 113
        '
        'lblTitleBinKbn
        '
        Me.lblTitleBinKbn.AutoSizeDef = False
        Me.lblTitleBinKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBinKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleBinKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleBinKbn.EnableStatus = False
        Me.lblTitleBinKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBinKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleBinKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBinKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleBinKbn.HeightDef = 13
        Me.lblTitleBinKbn.Location = New System.Drawing.Point(575, 43)
        Me.lblTitleBinKbn.Name = "lblTitleBinKbn"
        Me.lblTitleBinKbn.Size = New System.Drawing.Size(88, 13)
        Me.lblTitleBinKbn.TabIndex = 125
        Me.lblTitleBinKbn.Text = "便区分"
        Me.lblTitleBinKbn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleBinKbn.TextValue = "便区分"
        Me.lblTitleBinKbn.WidthDef = 88
        '
        'lblTripNo
        '
        Me.lblTripNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTripNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTripNo.CountWrappedLine = False
        Me.lblTripNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTripNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTripNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTripNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTripNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTripNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTripNo.HeightDef = 18
        Me.lblTripNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripNo.HissuLabelVisible = False
        Me.lblTripNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTripNo.IsByteCheck = 0
        Me.lblTripNo.IsCalendarCheck = False
        Me.lblTripNo.IsDakutenCheck = False
        Me.lblTripNo.IsEisuCheck = False
        Me.lblTripNo.IsForbiddenWordsCheck = False
        Me.lblTripNo.IsFullByteCheck = 0
        Me.lblTripNo.IsHankakuCheck = False
        Me.lblTripNo.IsHissuCheck = False
        Me.lblTripNo.IsKanaCheck = False
        Me.lblTripNo.IsMiddleSpace = False
        Me.lblTripNo.IsNumericCheck = False
        Me.lblTripNo.IsSujiCheck = False
        Me.lblTripNo.IsZenkakuCheck = False
        Me.lblTripNo.ItemName = ""
        Me.lblTripNo.LineSpace = 0
        Me.lblTripNo.Location = New System.Drawing.Point(663, 19)
        Me.lblTripNo.MaxLength = 0
        Me.lblTripNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTripNo.MaxLineCount = 0
        Me.lblTripNo.Multiline = False
        Me.lblTripNo.Name = "lblTripNo"
        Me.lblTripNo.ReadOnly = True
        Me.lblTripNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTripNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTripNo.Size = New System.Drawing.Size(113, 18)
        Me.lblTripNo.TabIndex = 124
        Me.lblTripNo.TabStop = False
        Me.lblTripNo.TabStopSetting = False
        Me.lblTripNo.TextValue = "XXXXXXXXX"
        Me.lblTripNo.UseSystemPasswordChar = False
        Me.lblTripNo.WidthDef = 113
        Me.lblTripNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleTripNo
        '
        Me.lblTitleTripNo.AutoSize = True
        Me.lblTitleTripNo.AutoSizeDef = True
        Me.lblTitleTripNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTripNo.EnableStatus = False
        Me.lblTitleTripNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripNo.HeightDef = 13
        Me.lblTitleTripNo.Location = New System.Drawing.Point(600, 22)
        Me.lblTitleTripNo.Name = "lblTitleTripNo"
        Me.lblTitleTripNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTripNo.TabIndex = 123
        Me.lblTitleTripNo.Text = "運行番号"
        Me.lblTitleTripNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTripNo.TextValue = "運行番号"
        Me.lblTitleTripNo.WidthDef = 63
        '
        'lblTitleTripDate
        '
        Me.lblTitleTripDate.AutoSizeDef = False
        Me.lblTitleTripDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTripDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTripDate.EnableStatus = False
        Me.lblTitleTripDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTripDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTripDate.HeightDef = 13
        Me.lblTitleTripDate.Location = New System.Drawing.Point(61, 43)
        Me.lblTitleTripDate.Name = "lblTitleTripDate"
        Me.lblTitleTripDate.Size = New System.Drawing.Size(102, 13)
        Me.lblTitleTripDate.TabIndex = 121
        Me.lblTitleTripDate.Text = "運行予定日"
        Me.lblTitleTripDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTripDate.TextValue = "運行予定日"
        Me.lblTitleTripDate.WidthDef = 102
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
        Me.cmbEigyo.Location = New System.Drawing.Point(163, 19)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 120
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
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
        Me.lblTitleEigyo.HeightDef = 13
        Me.lblTitleEigyo.Location = New System.Drawing.Point(89, 22)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(74, 13)
        Me.lblTitleEigyo.TabIndex = 4
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 74
        '
        'lblTripDate
        '
        Me.lblTripDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTripDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTripDate.CountWrappedLine = False
        Me.lblTripDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTripDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTripDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTripDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTripDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTripDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTripDate.HeightDef = 18
        Me.lblTripDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTripDate.HissuLabelVisible = False
        Me.lblTripDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTripDate.IsByteCheck = 0
        Me.lblTripDate.IsCalendarCheck = False
        Me.lblTripDate.IsDakutenCheck = False
        Me.lblTripDate.IsEisuCheck = False
        Me.lblTripDate.IsForbiddenWordsCheck = False
        Me.lblTripDate.IsFullByteCheck = 0
        Me.lblTripDate.IsHankakuCheck = False
        Me.lblTripDate.IsHissuCheck = False
        Me.lblTripDate.IsKanaCheck = False
        Me.lblTripDate.IsMiddleSpace = False
        Me.lblTripDate.IsNumericCheck = False
        Me.lblTripDate.IsSujiCheck = False
        Me.lblTripDate.IsZenkakuCheck = False
        Me.lblTripDate.ItemName = ""
        Me.lblTripDate.LineSpace = 0
        Me.lblTripDate.Location = New System.Drawing.Point(163, 40)
        Me.lblTripDate.MaxLength = 0
        Me.lblTripDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTripDate.MaxLineCount = 0
        Me.lblTripDate.Multiline = False
        Me.lblTripDate.Name = "lblTripDate"
        Me.lblTripDate.ReadOnly = True
        Me.lblTripDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTripDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTripDate.Size = New System.Drawing.Size(118, 18)
        Me.lblTripDate.TabIndex = 293
        Me.lblTripDate.TabStop = False
        Me.lblTripDate.TabStopSetting = False
        Me.lblTripDate.TextValue = "XXXX"
        Me.lblTripDate.UseSystemPasswordChar = False
        Me.lblTripDate.WidthDef = 118
        Me.lblTripDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.btnRowAdd.Location = New System.Drawing.Point(15, 344)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 257
        Me.btnRowAdd.TabStopSetting = True
        Me.btnRowAdd.Text = "行追加"
        Me.btnRowAdd.TextValue = "行追加"
        Me.btnRowAdd.UseVisualStyleBackColor = True
        Me.btnRowAdd.WidthDef = 70
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
        Me.btnRowDel.Location = New System.Drawing.Point(91, 344)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 256
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
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
        Me.sprDetail.HeightDef = 492
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 372)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1244, 492)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 258
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
        '
        'pnlShiharai
        '
        Me.pnlShiharai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShiharai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShiharai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShiharai.Controls.Add(Me.lblTitleYen4)
        Me.pnlShiharai.Controls.Add(Me.numShiharaiKingaku)
        Me.pnlShiharai.Controls.Add(Me.lblTitleShiharaiKingaku)
        Me.pnlShiharai.Controls.Add(Me.cmbTariffKbn)
        Me.pnlShiharai.Controls.Add(Me.lblTitleTariffKb)
        Me.pnlShiharai.Controls.Add(Me.btnShiharaiKeisan)
        Me.pnlShiharai.Controls.Add(Me.lblTitleKensu)
        Me.pnlShiharai.Controls.Add(Me.numKensu)
        Me.pnlShiharai.Controls.Add(Me.lblTitleShiharaiKensu)
        Me.pnlShiharai.Controls.Add(Me.lblTitleKg3)
        Me.pnlShiharai.Controls.Add(Me.numShiharaiWt)
        Me.pnlShiharai.Controls.Add(Me.lblTitleShiharaiWt)
        Me.pnlShiharai.Controls.Add(Me.lblExtcNm)
        Me.pnlShiharai.Controls.Add(Me.txtExtcCd)
        Me.pnlShiharai.Controls.Add(Me.lblTitleExtc)
        Me.pnlShiharai.Controls.Add(Me.lblTariffNm)
        Me.pnlShiharai.Controls.Add(Me.txtTariffCd)
        Me.pnlShiharai.Controls.Add(Me.lblTitleTariff)
        Me.pnlShiharai.Controls.Add(Me.lblTitleYen3)
        Me.pnlShiharai.Controls.Add(Me.numKingaku)
        Me.pnlShiharai.Controls.Add(Me.lblTitleKingaku)
        Me.pnlShiharai.Controls.Add(Me.lblTitleShiharai)
        Me.pnlShiharai.Controls.Add(Me.cmbShiharai)
        Me.pnlShiharai.EnableStatus = False
        Me.pnlShiharai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShiharai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShiharai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShiharai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShiharai.HeightDef = 105
        Me.pnlShiharai.Location = New System.Drawing.Point(16, 233)
        Me.pnlShiharai.Name = "pnlShiharai"
        Me.pnlShiharai.Size = New System.Drawing.Size(1243, 105)
        Me.pnlShiharai.TabIndex = 297
        Me.pnlShiharai.TabStop = False
        Me.pnlShiharai.Text = "支払情報"
        Me.pnlShiharai.TextValue = "支払情報"
        Me.pnlShiharai.WidthDef = 1243
        '
        'lblTitleYen4
        '
        Me.lblTitleYen4.AutoSize = True
        Me.lblTitleYen4.AutoSizeDef = True
        Me.lblTitleYen4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen4.EnableStatus = False
        Me.lblTitleYen4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen4.HeightDef = 13
        Me.lblTitleYen4.Location = New System.Drawing.Point(1003, 60)
        Me.lblTitleYen4.Name = "lblTitleYen4"
        Me.lblTitleYen4.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen4.TabIndex = 358
        Me.lblTitleYen4.Text = "円"
        Me.lblTitleYen4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen4.TextValue = "円"
        Me.lblTitleYen4.WidthDef = 21
        '
        'numShiharaiKingaku
        '
        Me.numShiharaiKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numShiharaiKingaku.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numShiharaiKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numShiharaiKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numShiharaiKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numShiharaiKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numShiharaiKingaku.HeightDef = 18
        Me.numShiharaiKingaku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiKingaku.HissuLabelVisible = False
        Me.numShiharaiKingaku.IsHissuCheck = False
        Me.numShiharaiKingaku.IsRangeCheck = False
        Me.numShiharaiKingaku.ItemName = ""
        Me.numShiharaiKingaku.Location = New System.Drawing.Point(852, 58)
        Me.numShiharaiKingaku.Name = "numShiharaiKingaku"
        Me.numShiharaiKingaku.ReadOnly = True
        Me.numShiharaiKingaku.Size = New System.Drawing.Size(164, 18)
        Me.numShiharaiKingaku.TabIndex = 357
        Me.numShiharaiKingaku.TabStop = False
        Me.numShiharaiKingaku.TabStopSetting = False
        Me.numShiharaiKingaku.TextValue = "0"
        Me.numShiharaiKingaku.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numShiharaiKingaku.WidthDef = 164
        '
        'lblTitleShiharaiKingaku
        '
        Me.lblTitleShiharaiKingaku.AutoSizeDef = False
        Me.lblTitleShiharaiKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShiharaiKingaku.EnableStatus = False
        Me.lblTitleShiharaiKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiKingaku.HeightDef = 13
        Me.lblTitleShiharaiKingaku.Location = New System.Drawing.Point(737, 61)
        Me.lblTitleShiharaiKingaku.Name = "lblTitleShiharaiKingaku"
        Me.lblTitleShiharaiKingaku.Size = New System.Drawing.Size(109, 13)
        Me.lblTitleShiharaiKingaku.TabIndex = 356
        Me.lblTitleShiharaiKingaku.Text = "支払金額"
        Me.lblTitleShiharaiKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShiharaiKingaku.TextValue = "支払金額"
        Me.lblTitleShiharaiKingaku.WidthDef = 109
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
        Me.cmbTariffKbn.Location = New System.Drawing.Point(121, 37)
        Me.cmbTariffKbn.Name = "cmbTariffKbn"
        Me.cmbTariffKbn.ReadOnly = False
        Me.cmbTariffKbn.SelectedIndex = -1
        Me.cmbTariffKbn.SelectedItem = Nothing
        Me.cmbTariffKbn.SelectedText = ""
        Me.cmbTariffKbn.SelectedValue = ""
        Me.cmbTariffKbn.Size = New System.Drawing.Size(118, 18)
        Me.cmbTariffKbn.TabIndex = 355
        Me.cmbTariffKbn.TabStopSetting = True
        Me.cmbTariffKbn.TextValue = ""
        Me.cmbTariffKbn.Value1 = ""
        Me.cmbTariffKbn.Value2 = "='1.000'"
        Me.cmbTariffKbn.Value3 = ""
        Me.cmbTariffKbn.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.V2
        Me.cmbTariffKbn.ValueMember = "1"
        Me.cmbTariffKbn.WidthDef = 118
        '
        'lblTitleTariffKb
        '
        Me.lblTitleTariffKb.AutoSizeDef = False
        Me.lblTitleTariffKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariffKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTariffKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTariffKb.EnableStatus = False
        Me.lblTitleTariffKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariffKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTariffKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariffKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTariffKb.HeightDef = 13
        Me.lblTitleTariffKb.Location = New System.Drawing.Point(41, 40)
        Me.lblTitleTariffKb.Name = "lblTitleTariffKb"
        Me.lblTitleTariffKb.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleTariffKb.TabIndex = 354
        Me.lblTitleTariffKb.Text = "タリフ分類"
        Me.lblTitleTariffKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariffKb.TextValue = "タリフ分類"
        Me.lblTitleTariffKb.WidthDef = 77
        '
        'btnShiharaiKeisan
        '
        Me.btnShiharaiKeisan.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShiharaiKeisan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShiharaiKeisan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnShiharaiKeisan.EnableStatus = True
        Me.btnShiharaiKeisan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShiharaiKeisan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShiharaiKeisan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShiharaiKeisan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShiharaiKeisan.HeightDef = 22
        Me.btnShiharaiKeisan.Location = New System.Drawing.Point(781, 77)
        Me.btnShiharaiKeisan.Name = "btnShiharaiKeisan"
        Me.btnShiharaiKeisan.Size = New System.Drawing.Size(70, 22)
        Me.btnShiharaiKeisan.TabIndex = 353
        Me.btnShiharaiKeisan.TabStopSetting = True
        Me.btnShiharaiKeisan.Text = "計算"
        Me.btnShiharaiKeisan.TextValue = "計算"
        Me.btnShiharaiKeisan.UseVisualStyleBackColor = True
        Me.btnShiharaiKeisan.WidthDef = 70
        '
        'lblTitleKensu
        '
        Me.lblTitleKensu.AutoSize = True
        Me.lblTitleKensu.AutoSizeDef = True
        Me.lblTitleKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKensu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKensu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKensu.EnableStatus = False
        Me.lblTitleKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKensu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKensu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKensu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKensu.HeightDef = 13
        Me.lblTitleKensu.Location = New System.Drawing.Point(862, 17)
        Me.lblTitleKensu.Name = "lblTitleKensu"
        Me.lblTitleKensu.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKensu.TabIndex = 352
        Me.lblTitleKensu.Text = "件"
        Me.lblTitleKensu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKensu.TextValue = "件"
        Me.lblTitleKensu.WidthDef = 21
        '
        'numKensu
        '
        Me.numKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKensu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKensu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKensu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKensu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKensu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKensu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKensu.HeightDef = 18
        Me.numKensu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKensu.HissuLabelVisible = False
        Me.numKensu.IsHissuCheck = False
        Me.numKensu.IsRangeCheck = False
        Me.numKensu.ItemName = ""
        Me.numKensu.Location = New System.Drawing.Point(828, 14)
        Me.numKensu.Name = "numKensu"
        Me.numKensu.ReadOnly = True
        Me.numKensu.Size = New System.Drawing.Size(46, 18)
        Me.numKensu.TabIndex = 351
        Me.numKensu.TabStop = False
        Me.numKensu.TabStopSetting = False
        Me.numKensu.TextValue = "99,999"
        Me.numKensu.Value = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numKensu.WidthDef = 46
        '
        'lblTitleShiharaiKensu
        '
        Me.lblTitleShiharaiKensu.AutoSize = True
        Me.lblTitleShiharaiKensu.AutoSizeDef = True
        Me.lblTitleShiharaiKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiKensu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiKensu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShiharaiKensu.EnableStatus = False
        Me.lblTitleShiharaiKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiKensu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiKensu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiKensu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiKensu.HeightDef = 13
        Me.lblTitleShiharaiKensu.Location = New System.Drawing.Point(792, 17)
        Me.lblTitleShiharaiKensu.Name = "lblTitleShiharaiKensu"
        Me.lblTitleShiharaiKensu.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleShiharaiKensu.TabIndex = 350
        Me.lblTitleShiharaiKensu.Text = "件数"
        Me.lblTitleShiharaiKensu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShiharaiKensu.TextValue = "件数"
        Me.lblTitleShiharaiKensu.WidthDef = 35
        '
        'lblTitleKg3
        '
        Me.lblTitleKg3.AutoSize = True
        Me.lblTitleKg3.AutoSizeDef = True
        Me.lblTitleKg3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKg3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKg3.EnableStatus = False
        Me.lblTitleKg3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKg3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKg3.HeightDef = 13
        Me.lblTitleKg3.Location = New System.Drawing.Point(751, 17)
        Me.lblTitleKg3.Name = "lblTitleKg3"
        Me.lblTitleKg3.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleKg3.TabIndex = 349
        Me.lblTitleKg3.Text = "KG"
        Me.lblTitleKg3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKg3.TextValue = "KG"
        Me.lblTitleKg3.WidthDef = 21
        '
        'numShiharaiWt
        '
        Me.numShiharaiWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiWt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numShiharaiWt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numShiharaiWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numShiharaiWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numShiharaiWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numShiharaiWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numShiharaiWt.HeightDef = 18
        Me.numShiharaiWt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numShiharaiWt.HissuLabelVisible = False
        Me.numShiharaiWt.IsHissuCheck = False
        Me.numShiharaiWt.IsRangeCheck = False
        Me.numShiharaiWt.ItemName = ""
        Me.numShiharaiWt.Location = New System.Drawing.Point(602, 15)
        Me.numShiharaiWt.Name = "numShiharaiWt"
        Me.numShiharaiWt.ReadOnly = True
        Me.numShiharaiWt.Size = New System.Drawing.Size(164, 18)
        Me.numShiharaiWt.TabIndex = 348
        Me.numShiharaiWt.TabStop = False
        Me.numShiharaiWt.TabStopSetting = False
        Me.numShiharaiWt.TextValue = "99,999"
        Me.numShiharaiWt.Value = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.numShiharaiWt.WidthDef = 164
        '
        'lblTitleShiharaiWt
        '
        Me.lblTitleShiharaiWt.AutoSizeDef = False
        Me.lblTitleShiharaiWt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiWt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharaiWt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShiharaiWt.EnableStatus = False
        Me.lblTitleShiharaiWt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiWt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharaiWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiWt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharaiWt.HeightDef = 13
        Me.lblTitleShiharaiWt.Location = New System.Drawing.Point(515, 18)
        Me.lblTitleShiharaiWt.Name = "lblTitleShiharaiWt"
        Me.lblTitleShiharaiWt.Size = New System.Drawing.Size(86, 13)
        Me.lblTitleShiharaiWt.TabIndex = 347
        Me.lblTitleShiharaiWt.Text = "支払重量"
        Me.lblTitleShiharaiWt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShiharaiWt.TextValue = "支払重量"
        Me.lblTitleShiharaiWt.WidthDef = 86
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
        Me.lblExtcNm.Location = New System.Drawing.Point(228, 79)
        Me.lblExtcNm.MaxLength = 0
        Me.lblExtcNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblExtcNm.MaxLineCount = 0
        Me.lblExtcNm.Multiline = False
        Me.lblExtcNm.Name = "lblExtcNm"
        Me.lblExtcNm.ReadOnly = True
        Me.lblExtcNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblExtcNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblExtcNm.Size = New System.Drawing.Size(473, 18)
        Me.lblExtcNm.TabIndex = 346
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
        Me.txtExtcCd.Location = New System.Drawing.Point(121, 79)
        Me.txtExtcCd.MaxLength = 10
        Me.txtExtcCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtExtcCd.MaxLineCount = 0
        Me.txtExtcCd.Multiline = False
        Me.txtExtcCd.Name = "txtExtcCd"
        Me.txtExtcCd.ReadOnly = False
        Me.txtExtcCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtExtcCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtExtcCd.Size = New System.Drawing.Size(125, 18)
        Me.txtExtcCd.TabIndex = 345
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
        Me.lblTitleExtc.Location = New System.Drawing.Point(3, 82)
        Me.lblTitleExtc.Name = "lblTitleExtc"
        Me.lblTitleExtc.Size = New System.Drawing.Size(115, 13)
        Me.lblTitleExtc.TabIndex = 344
        Me.lblTitleExtc.Text = "支払割増タリフ"
        Me.lblTitleExtc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleExtc.TextValue = "支払割増タリフ"
        Me.lblTitleExtc.WidthDef = 115
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
        Me.lblTariffNm.Location = New System.Drawing.Point(228, 58)
        Me.lblTariffNm.MaxLength = 0
        Me.lblTariffNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTariffNm.MaxLineCount = 0
        Me.lblTariffNm.Multiline = False
        Me.lblTariffNm.Name = "lblTariffNm"
        Me.lblTariffNm.ReadOnly = True
        Me.lblTariffNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTariffNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTariffNm.Size = New System.Drawing.Size(473, 18)
        Me.lblTariffNm.TabIndex = 326
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
        Me.txtTariffCd.Location = New System.Drawing.Point(121, 58)
        Me.txtTariffCd.MaxLength = 10
        Me.txtTariffCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtTariffCd.MaxLineCount = 0
        Me.txtTariffCd.Multiline = False
        Me.txtTariffCd.Name = "txtTariffCd"
        Me.txtTariffCd.ReadOnly = False
        Me.txtTariffCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtTariffCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTariffCd.Size = New System.Drawing.Size(125, 18)
        Me.txtTariffCd.TabIndex = 325
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
        Me.lblTitleTariff.Location = New System.Drawing.Point(16, 61)
        Me.lblTitleTariff.Name = "lblTitleTariff"
        Me.lblTitleTariff.Size = New System.Drawing.Size(102, 13)
        Me.lblTitleTariff.TabIndex = 324
        Me.lblTitleTariff.Text = "支払タリフ"
        Me.lblTitleTariff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTariff.TextValue = "支払タリフ"
        Me.lblTitleTariff.WidthDef = 102
        '
        'lblTitleYen3
        '
        Me.lblTitleYen3.AutoSize = True
        Me.lblTitleYen3.AutoSizeDef = True
        Me.lblTitleYen3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleYen3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleYen3.EnableStatus = False
        Me.lblTitleYen3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleYen3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleYen3.HeightDef = 13
        Me.lblTitleYen3.Location = New System.Drawing.Point(495, 18)
        Me.lblTitleYen3.Name = "lblTitleYen3"
        Me.lblTitleYen3.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleYen3.TabIndex = 289
        Me.lblTitleYen3.Text = "円"
        Me.lblTitleYen3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleYen3.TextValue = "円"
        Me.lblTitleYen3.WidthDef = 21
        '
        'numKingaku
        '
        Me.numKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numKingaku.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numKingaku.HeightDef = 18
        Me.numKingaku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numKingaku.HissuLabelVisible = False
        Me.numKingaku.IsHissuCheck = False
        Me.numKingaku.IsRangeCheck = False
        Me.numKingaku.ItemName = ""
        Me.numKingaku.Location = New System.Drawing.Point(345, 16)
        Me.numKingaku.Name = "numKingaku"
        Me.numKingaku.ReadOnly = True
        Me.numKingaku.Size = New System.Drawing.Size(164, 18)
        Me.numKingaku.TabIndex = 286
        Me.numKingaku.TabStop = False
        Me.numKingaku.TabStopSetting = False
        Me.numKingaku.TextValue = "0"
        Me.numKingaku.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numKingaku.WidthDef = 164
        '
        'lblTitleKingaku
        '
        Me.lblTitleKingaku.AutoSizeDef = False
        Me.lblTitleKingaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKingaku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKingaku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKingaku.EnableStatus = False
        Me.lblTitleKingaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKingaku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKingaku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKingaku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKingaku.HeightDef = 13
        Me.lblTitleKingaku.Location = New System.Drawing.Point(272, 19)
        Me.lblTitleKingaku.Name = "lblTitleKingaku"
        Me.lblTitleKingaku.Size = New System.Drawing.Size(70, 13)
        Me.lblTitleKingaku.TabIndex = 285
        Me.lblTitleKingaku.Text = "金額"
        Me.lblTitleKingaku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKingaku.TextValue = "金額"
        Me.lblTitleKingaku.WidthDef = 70
        '
        'lblTitleShiharai
        '
        Me.lblTitleShiharai.AutoSizeDef = False
        Me.lblTitleShiharai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleShiharai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleShiharai.EnableStatus = False
        Me.lblTitleShiharai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleShiharai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleShiharai.HeightDef = 13
        Me.lblTitleShiharai.Location = New System.Drawing.Point(0, 18)
        Me.lblTitleShiharai.Name = "lblTitleShiharai"
        Me.lblTitleShiharai.Size = New System.Drawing.Size(118, 13)
        Me.lblTitleShiharai.TabIndex = 267
        Me.lblTitleShiharai.Text = "支払運賃計算"
        Me.lblTitleShiharai.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleShiharai.TextValue = "支払運賃計算"
        Me.lblTitleShiharai.WidthDef = 118
        '
        'cmbShiharai
        '
        Me.cmbShiharai.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShiharai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbShiharai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbShiharai.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbShiharai.DataCode = "S079"
        Me.cmbShiharai.DataSource = Nothing
        Me.cmbShiharai.DisplayMember = Nothing
        Me.cmbShiharai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShiharai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbShiharai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShiharai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbShiharai.HeightDef = 18
        Me.cmbShiharai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbShiharai.HissuLabelVisible = True
        Me.cmbShiharai.InsertWildCard = True
        Me.cmbShiharai.IsForbiddenWordsCheck = False
        Me.cmbShiharai.IsHissuCheck = True
        Me.cmbShiharai.ItemName = ""
        Me.cmbShiharai.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbShiharai.Location = New System.Drawing.Point(121, 16)
        Me.cmbShiharai.Name = "cmbShiharai"
        Me.cmbShiharai.ReadOnly = False
        Me.cmbShiharai.SelectedIndex = -1
        Me.cmbShiharai.SelectedItem = Nothing
        Me.cmbShiharai.SelectedText = ""
        Me.cmbShiharai.SelectedValue = ""
        Me.cmbShiharai.Size = New System.Drawing.Size(145, 18)
        Me.cmbShiharai.TabIndex = 266
        Me.cmbShiharai.TabStopSetting = True
        Me.cmbShiharai.TextValue = ""
        Me.cmbShiharai.Value1 = Nothing
        Me.cmbShiharai.Value2 = Nothing
        Me.cmbShiharai.Value3 = Nothing
        Me.cmbShiharai.ValueMember = Nothing
        Me.cmbShiharai.WidthDef = 145
        '
        'txtIsEditRowFlg
        '
        Me.txtIsEditRowFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIsEditRowFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtIsEditRowFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIsEditRowFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtIsEditRowFlg.CountWrappedLine = False
        Me.txtIsEditRowFlg.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtIsEditRowFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIsEditRowFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtIsEditRowFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIsEditRowFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtIsEditRowFlg.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtIsEditRowFlg.HeightDef = 18
        Me.txtIsEditRowFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtIsEditRowFlg.HissuLabelVisible = False
        Me.txtIsEditRowFlg.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtIsEditRowFlg.IsByteCheck = 3
        Me.txtIsEditRowFlg.IsCalendarCheck = False
        Me.txtIsEditRowFlg.IsDakutenCheck = False
        Me.txtIsEditRowFlg.IsEisuCheck = False
        Me.txtIsEditRowFlg.IsForbiddenWordsCheck = False
        Me.txtIsEditRowFlg.IsFullByteCheck = 0
        Me.txtIsEditRowFlg.IsHankakuCheck = False
        Me.txtIsEditRowFlg.IsHissuCheck = False
        Me.txtIsEditRowFlg.IsKanaCheck = False
        Me.txtIsEditRowFlg.IsMiddleSpace = False
        Me.txtIsEditRowFlg.IsNumericCheck = False
        Me.txtIsEditRowFlg.IsSujiCheck = False
        Me.txtIsEditRowFlg.IsZenkakuCheck = False
        Me.txtIsEditRowFlg.ItemName = ""
        Me.txtIsEditRowFlg.LineSpace = 0
        Me.txtIsEditRowFlg.Location = New System.Drawing.Point(193, 348)
        Me.txtIsEditRowFlg.MaxLength = 3
        Me.txtIsEditRowFlg.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtIsEditRowFlg.MaxLineCount = 0
        Me.txtIsEditRowFlg.Multiline = False
        Me.txtIsEditRowFlg.Name = "txtIsEditRowFlg"
        Me.txtIsEditRowFlg.ReadOnly = False
        Me.txtIsEditRowFlg.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtIsEditRowFlg.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtIsEditRowFlg.Size = New System.Drawing.Size(46, 18)
        Me.txtIsEditRowFlg.TabIndex = 298
        Me.txtIsEditRowFlg.TabStopSetting = True
        Me.txtIsEditRowFlg.TextValue = "0"
        Me.txtIsEditRowFlg.UseSystemPasswordChar = False
        Me.txtIsEditRowFlg.Visible = False
        Me.txtIsEditRowFlg.WidthDef = 46
        Me.txtIsEditRowFlg.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMF030F
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMF030F"
        Me.Text = "【LMF030】 運行情報入力"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlUnso.ResumeLayout(False)
        Me.pnlUnso.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlShiharai.ResumeLayout(False)
        Me.pnlShiharai.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents pnlUnso As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleTripDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTripNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTripNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCarNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCarNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbBinKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleBinKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCarOndo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCarKey As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numOndoMx As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleDo1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleDo2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numOndoMm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbJshaKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblUnsocoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsocoBrCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsocoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleUnsoco As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleDriver As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDriverCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleJshaKbn As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleLoadWt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleRem As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRem As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblDriverNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numLoadWt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKg2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numTripWt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleTripWt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnsoOndo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoOndo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleDo3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPayAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitlePayAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numRevUnchin As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleRevUnchin As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUnderUnsoInfor As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleUncoInfor As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numUnsoNb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleUnsoNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleYen2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleYen1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents lblTitleHaiso As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbHaiso As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTripDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCarType As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSyakenTruck As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSyakenTrailer As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents pnlShiharai As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleShiharai As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbShiharai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents numKingaku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleKingaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleYen3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTariffNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtTariffCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleTariff As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblExtcNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtExtcCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleExtc As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKg3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numShiharaiWt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleShiharaiWt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleKensu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numKensu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleShiharaiKensu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnShiharaiKeisan As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbTariffKbn As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTariffKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleYen4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numShiharaiKingaku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleShiharaiKingaku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtUnsocoBrCdOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUnsocoCdOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtIsEditRowFlg As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnTehaiCreate As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbTehaisyubetsu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTehaisyubetsu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
End Class
