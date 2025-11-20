<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LME050F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LME050F))
        Me.sprZaiko = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.lblGoods = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblReserveNO = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtRsvNO = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblLotNO = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblSerialNO = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblIrime = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblIrimeTani = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblSoko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNM_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustCD_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustCD_L = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblCustNM_M = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblGoodsCD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblGoodsNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtLotNO = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtSerialNO = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.frmCnt = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblSyukkaSouCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtSyukkaHasu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numSyukkaHasu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblSyukkaHasu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblHikiCntSum = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiCntSum = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.txtSyukkaKosu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblHikiZanCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiZanCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.numSyukkaSouCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblSyukkaKosu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numSyukkaKosu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblHikiSumiCnt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiSumiCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblIrisu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numIrisu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.frmAmount = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblHikiAmtSum = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiAmtSum = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblSyukkaSouAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblHikiZanAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiZanAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblSyukkaSouAmtl = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numSyukkaSouAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblHikiSumiAmt = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.numHikiSumiAmt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
        Me.lblGoodsNRS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.numIrime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprZaiko, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frmCnt.SuspendLayout()
        Me.frmAmount.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNRS)
        Me.pnlViewAria.Controls.Add(Me.cmbSoko)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblIrimeTani)
        Me.pnlViewAria.Controls.Add(Me.frmAmount)
        Me.pnlViewAria.Controls.Add(Me.txtSerialNO)
        Me.pnlViewAria.Controls.Add(Me.txtLotNO)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsNM)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsCD)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM_M)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM_L)
        Me.pnlViewAria.Controls.Add(Me.lblCustCD_M)
        Me.pnlViewAria.Controls.Add(Me.lblCustCD_L)
        Me.pnlViewAria.Controls.Add(Me.lblCust)
        Me.pnlViewAria.Controls.Add(Me.lblEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblSoko)
        Me.pnlViewAria.Controls.Add(Me.sprZaiko)
        Me.pnlViewAria.Controls.Add(Me.lblIrime)
        Me.pnlViewAria.Controls.Add(Me.lblSerialNO)
        Me.pnlViewAria.Controls.Add(Me.lblLotNO)
        Me.pnlViewAria.Controls.Add(Me.txtRsvNO)
        Me.pnlViewAria.Controls.Add(Me.lblReserveNO)
        Me.pnlViewAria.Controls.Add(Me.lblGoods)
        Me.pnlViewAria.Controls.Add(Me.frmCnt)
        Me.pnlViewAria.Controls.Add(Me.numIrime)
        '
        'sprZaiko
        '
        Me.sprZaiko.AccessibleDescription = ""
        Me.sprZaiko.AllowUserZoom = False
        Me.sprZaiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZaiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprZaiko.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprZaiko.CellClickEventArgs = Nothing
        Me.sprZaiko.CheckToCheckBox = True
        Me.sprZaiko.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprZaiko.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZaiko.EditModeReplace = True
        Me.sprZaiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZaiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprZaiko.ForeColorDef = System.Drawing.Color.Empty
        Me.sprZaiko.HeightDef = 650
        Me.sprZaiko.KeyboardCheckBoxOn = False
        Me.sprZaiko.Location = New System.Drawing.Point(12, 213)
        Me.sprZaiko.Name = "sprZaiko"
        Me.sprZaiko.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprZaiko.SetViewportTopRow(0, 0, 1)
        Me.sprZaiko.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprZaiko.SetViewportTopRow(0, 0, 1)
        Me.sprZaiko.SetActiveViewport(0, -1, 0)
        Me.sprZaiko.Size = New System.Drawing.Size(1250, 650)
        Me.sprZaiko.SortColumn = True
        Me.sprZaiko.SpanColumnLock = True
        Me.sprZaiko.SpreadDoubleClicked = False
        Me.sprZaiko.TabIndex = 19
        Me.sprZaiko.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprZaiko.TextValue = Nothing
        Me.sprZaiko.WidthDef = 1250
        '
        'lblGoods
        '
        Me.lblGoods.AutoSize = True
        Me.lblGoods.AutoSizeDef = True
        Me.lblGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoods.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoods.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblGoods.EnableStatus = False
        Me.lblGoods.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoods.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoods.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoods.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoods.HeightDef = 13
        Me.lblGoods.Location = New System.Drawing.Point(58, 53)
        Me.lblGoods.Name = "lblGoods"
        Me.lblGoods.Size = New System.Drawing.Size(35, 13)
        Me.lblGoods.TabIndex = 41
        Me.lblGoods.Text = "商品"
        Me.lblGoods.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblGoods.TextValue = "商品"
        Me.lblGoods.WidthDef = 35
        '
        'lblReserveNO
        '
        Me.lblReserveNO.AutoSize = True
        Me.lblReserveNO.AutoSizeDef = True
        Me.lblReserveNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblReserveNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblReserveNO.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblReserveNO.EnableStatus = False
        Me.lblReserveNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblReserveNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblReserveNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblReserveNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblReserveNO.HeightDef = 13
        Me.lblReserveNO.Location = New System.Drawing.Point(30, 75)
        Me.lblReserveNO.Name = "lblReserveNO"
        Me.lblReserveNO.Size = New System.Drawing.Size(63, 13)
        Me.lblReserveNO.TabIndex = 44
        Me.lblReserveNO.Text = "予約番号"
        Me.lblReserveNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblReserveNO.TextValue = "予約番号"
        Me.lblReserveNO.WidthDef = 63
        '
        'txtRsvNO
        '
        Me.txtRsvNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRsvNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRsvNO.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRsvNO.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRsvNO.CountWrappedLine = False
        Me.txtRsvNO.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRsvNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRsvNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRsvNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRsvNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRsvNO.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRsvNO.HeightDef = 18
        Me.txtRsvNO.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRsvNO.HissuLabelVisible = False
        Me.txtRsvNO.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtRsvNO.IsByteCheck = 10
        Me.txtRsvNO.IsCalendarCheck = False
        Me.txtRsvNO.IsDakutenCheck = False
        Me.txtRsvNO.IsEisuCheck = False
        Me.txtRsvNO.IsForbiddenWordsCheck = False
        Me.txtRsvNO.IsFullByteCheck = 0
        Me.txtRsvNO.IsHankakuCheck = False
        Me.txtRsvNO.IsHissuCheck = False
        Me.txtRsvNO.IsKanaCheck = False
        Me.txtRsvNO.IsMiddleSpace = False
        Me.txtRsvNO.IsNumericCheck = False
        Me.txtRsvNO.IsSujiCheck = False
        Me.txtRsvNO.IsZenkakuCheck = False
        Me.txtRsvNO.ItemName = ""
        Me.txtRsvNO.LineSpace = 0
        Me.txtRsvNO.Location = New System.Drawing.Point(99, 72)
        Me.txtRsvNO.MaxLength = 10
        Me.txtRsvNO.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRsvNO.MaxLineCount = 0
        Me.txtRsvNO.Multiline = False
        Me.txtRsvNO.Name = "txtRsvNO"
        Me.txtRsvNO.ReadOnly = False
        Me.txtRsvNO.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRsvNO.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRsvNO.Size = New System.Drawing.Size(106, 18)
        Me.txtRsvNO.TabIndex = 1
        Me.txtRsvNO.TabStopSetting = True
        Me.txtRsvNO.TextValue = "X---10---X"
        Me.txtRsvNO.UseSystemPasswordChar = False
        Me.txtRsvNO.WidthDef = 106
        Me.txtRsvNO.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblLotNO
        '
        Me.lblLotNO.AutoSize = True
        Me.lblLotNO.AutoSizeDef = True
        Me.lblLotNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNO.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblLotNO.EnableStatus = False
        Me.lblLotNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNO.HeightDef = 13
        Me.lblLotNO.Location = New System.Drawing.Point(366, 75)
        Me.lblLotNO.Name = "lblLotNO"
        Me.lblLotNO.Size = New System.Drawing.Size(63, 13)
        Me.lblLotNO.TabIndex = 46
        Me.lblLotNO.Text = "ロット№"
        Me.lblLotNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLotNO.TextValue = "ロット№"
        Me.lblLotNO.WidthDef = 63
        '
        'lblSerialNO
        '
        Me.lblSerialNO.AutoSize = True
        Me.lblSerialNO.AutoSizeDef = True
        Me.lblSerialNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSerialNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSerialNO.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSerialNO.EnableStatus = False
        Me.lblSerialNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSerialNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSerialNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSerialNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSerialNO.HeightDef = 13
        Me.lblSerialNO.Location = New System.Drawing.Point(765, 54)
        Me.lblSerialNO.Name = "lblSerialNO"
        Me.lblSerialNO.Size = New System.Drawing.Size(77, 13)
        Me.lblSerialNO.TabIndex = 48
        Me.lblSerialNO.Text = "シリアル№"
        Me.lblSerialNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSerialNO.TextValue = "シリアル№"
        Me.lblSerialNO.WidthDef = 77
        '
        'lblIrime
        '
        Me.lblIrime.AutoSize = True
        Me.lblIrime.AutoSizeDef = True
        Me.lblIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrime.EnableStatus = False
        Me.lblIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrime.HeightDef = 13
        Me.lblIrime.Location = New System.Drawing.Point(807, 75)
        Me.lblIrime.Name = "lblIrime"
        Me.lblIrime.Size = New System.Drawing.Size(35, 13)
        Me.lblIrime.TabIndex = 50
        Me.lblIrime.Text = "入目"
        Me.lblIrime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrime.TextValue = "入目"
        Me.lblIrime.WidthDef = 35
        '
        'lblIrimeTani
        '
        Me.lblIrimeTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeTani.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIrimeTani.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIrimeTani.CountWrappedLine = False
        Me.lblIrimeTani.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblIrimeTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrimeTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrimeTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrimeTani.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblIrimeTani.HeightDef = 18
        Me.lblIrimeTani.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrimeTani.HissuLabelVisible = False
        Me.lblIrimeTani.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblIrimeTani.IsByteCheck = 0
        Me.lblIrimeTani.IsCalendarCheck = False
        Me.lblIrimeTani.IsDakutenCheck = False
        Me.lblIrimeTani.IsEisuCheck = False
        Me.lblIrimeTani.IsForbiddenWordsCheck = False
        Me.lblIrimeTani.IsFullByteCheck = 0
        Me.lblIrimeTani.IsHankakuCheck = False
        Me.lblIrimeTani.IsHissuCheck = False
        Me.lblIrimeTani.IsKanaCheck = False
        Me.lblIrimeTani.IsMiddleSpace = False
        Me.lblIrimeTani.IsNumericCheck = False
        Me.lblIrimeTani.IsSujiCheck = False
        Me.lblIrimeTani.IsZenkakuCheck = False
        Me.lblIrimeTani.ItemName = ""
        Me.lblIrimeTani.LineSpace = 0
        Me.lblIrimeTani.Location = New System.Drawing.Point(930, 72)
        Me.lblIrimeTani.MaxLength = 0
        Me.lblIrimeTani.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblIrimeTani.MaxLineCount = 0
        Me.lblIrimeTani.Multiline = False
        Me.lblIrimeTani.Name = "lblIrimeTani"
        Me.lblIrimeTani.ReadOnly = True
        Me.lblIrimeTani.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblIrimeTani.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblIrimeTani.Size = New System.Drawing.Size(125, 18)
        Me.lblIrimeTani.TabIndex = 53
        Me.lblIrimeTani.TabStop = False
        Me.lblIrimeTani.TabStopSetting = False
        Me.lblIrimeTani.TextValue = "ＮＮＮＮＮＮＮ"
        Me.lblIrimeTani.UseSystemPasswordChar = False
        Me.lblIrimeTani.WidthDef = 125
        Me.lblIrimeTani.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblEigyo
        '
        Me.lblEigyo.AutoSize = True
        Me.lblEigyo.AutoSizeDef = True
        Me.lblEigyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblEigyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblEigyo.EnableStatus = False
        Me.lblEigyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblEigyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblEigyo.HeightDef = 13
        Me.lblEigyo.Location = New System.Drawing.Point(44, 12)
        Me.lblEigyo.Name = "lblEigyo"
        Me.lblEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblEigyo.TabIndex = 182
        Me.lblEigyo.Text = "営業所"
        Me.lblEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblEigyo.TextValue = "営業所"
        Me.lblEigyo.WidthDef = 49
        '
        'lblSoko
        '
        Me.lblSoko.AutoSize = True
        Me.lblSoko.AutoSizeDef = True
        Me.lblSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSoko.EnableStatus = False
        Me.lblSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSoko.HeightDef = 13
        Me.lblSoko.Location = New System.Drawing.Point(388, 12)
        Me.lblSoko.Name = "lblSoko"
        Me.lblSoko.Size = New System.Drawing.Size(35, 13)
        Me.lblSoko.TabIndex = 181
        Me.lblSoko.Text = "倉庫"
        Me.lblSoko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSoko.TextValue = "倉庫"
        Me.lblSoko.WidthDef = 35
        '
        'lblCustNM_L
        '
        Me.lblCustNM_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_L.CountWrappedLine = False
        Me.lblCustNM_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_L.HeightDef = 18
        Me.lblCustNM_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_L.HissuLabelVisible = False
        Me.lblCustNM_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_L.IsByteCheck = 0
        Me.lblCustNM_L.IsCalendarCheck = False
        Me.lblCustNM_L.IsDakutenCheck = False
        Me.lblCustNM_L.IsEisuCheck = False
        Me.lblCustNM_L.IsForbiddenWordsCheck = False
        Me.lblCustNM_L.IsFullByteCheck = 0
        Me.lblCustNM_L.IsHankakuCheck = False
        Me.lblCustNM_L.IsHissuCheck = False
        Me.lblCustNM_L.IsKanaCheck = False
        Me.lblCustNM_L.IsMiddleSpace = False
        Me.lblCustNM_L.IsNumericCheck = False
        Me.lblCustNM_L.IsSujiCheck = False
        Me.lblCustNM_L.IsZenkakuCheck = False
        Me.lblCustNM_L.ItemName = ""
        Me.lblCustNM_L.LineSpace = 0
        Me.lblCustNM_L.Location = New System.Drawing.Point(189, 30)
        Me.lblCustNM_L.MaxLength = 0
        Me.lblCustNM_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_L.MaxLineCount = 0
        Me.lblCustNM_L.Multiline = False
        Me.lblCustNM_L.Name = "lblCustNM_L"
        Me.lblCustNM_L.ReadOnly = True
        Me.lblCustNM_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_L.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNM_L.TabIndex = 140
        Me.lblCustNM_L.TabStop = False
        Me.lblCustNM_L.TabStopSetting = False
        Me.lblCustNM_L.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_L.UseSystemPasswordChar = False
        Me.lblCustNM_L.WidthDef = 473
        Me.lblCustNM_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCD_M
        '
        Me.lblCustCD_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCD_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCD_M.CountWrappedLine = False
        Me.lblCustCD_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCD_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCD_M.HeightDef = 18
        Me.lblCustCD_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_M.HissuLabelVisible = False
        Me.lblCustCD_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustCD_M.IsByteCheck = 0
        Me.lblCustCD_M.IsCalendarCheck = False
        Me.lblCustCD_M.IsDakutenCheck = False
        Me.lblCustCD_M.IsEisuCheck = False
        Me.lblCustCD_M.IsForbiddenWordsCheck = False
        Me.lblCustCD_M.IsFullByteCheck = 0
        Me.lblCustCD_M.IsHankakuCheck = False
        Me.lblCustCD_M.IsHissuCheck = False
        Me.lblCustCD_M.IsKanaCheck = False
        Me.lblCustCD_M.IsMiddleSpace = False
        Me.lblCustCD_M.IsNumericCheck = False
        Me.lblCustCD_M.IsSujiCheck = False
        Me.lblCustCD_M.IsZenkakuCheck = False
        Me.lblCustCD_M.ItemName = ""
        Me.lblCustCD_M.LineSpace = 0
        Me.lblCustCD_M.Location = New System.Drawing.Point(153, 30)
        Me.lblCustCD_M.MaxLength = 0
        Me.lblCustCD_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCD_M.MaxLineCount = 0
        Me.lblCustCD_M.Multiline = False
        Me.lblCustCD_M.Name = "lblCustCD_M"
        Me.lblCustCD_M.ReadOnly = True
        Me.lblCustCD_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCD_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCD_M.Size = New System.Drawing.Size(52, 18)
        Me.lblCustCD_M.TabIndex = 130
        Me.lblCustCD_M.TabStop = False
        Me.lblCustCD_M.TabStopSetting = False
        Me.lblCustCD_M.TextValue = "XXX"
        Me.lblCustCD_M.UseSystemPasswordChar = False
        Me.lblCustCD_M.WidthDef = 52
        Me.lblCustCD_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustCD_L
        '
        Me.lblCustCD_L.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustCD_L.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustCD_L.CountWrappedLine = False
        Me.lblCustCD_L.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustCD_L.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_L.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustCD_L.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_L.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustCD_L.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustCD_L.HeightDef = 18
        Me.lblCustCD_L.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustCD_L.HissuLabelVisible = False
        Me.lblCustCD_L.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustCD_L.IsByteCheck = 0
        Me.lblCustCD_L.IsCalendarCheck = False
        Me.lblCustCD_L.IsDakutenCheck = False
        Me.lblCustCD_L.IsEisuCheck = False
        Me.lblCustCD_L.IsForbiddenWordsCheck = False
        Me.lblCustCD_L.IsFullByteCheck = 0
        Me.lblCustCD_L.IsHankakuCheck = False
        Me.lblCustCD_L.IsHissuCheck = False
        Me.lblCustCD_L.IsKanaCheck = False
        Me.lblCustCD_L.IsMiddleSpace = False
        Me.lblCustCD_L.IsNumericCheck = False
        Me.lblCustCD_L.IsSujiCheck = False
        Me.lblCustCD_L.IsZenkakuCheck = False
        Me.lblCustCD_L.ItemName = ""
        Me.lblCustCD_L.LineSpace = 0
        Me.lblCustCD_L.Location = New System.Drawing.Point(99, 30)
        Me.lblCustCD_L.MaxLength = 0
        Me.lblCustCD_L.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustCD_L.MaxLineCount = 0
        Me.lblCustCD_L.Multiline = False
        Me.lblCustCD_L.Name = "lblCustCD_L"
        Me.lblCustCD_L.ReadOnly = True
        Me.lblCustCD_L.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustCD_L.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustCD_L.Size = New System.Drawing.Size(70, 18)
        Me.lblCustCD_L.TabIndex = 120
        Me.lblCustCD_L.TabStop = False
        Me.lblCustCD_L.TabStopSetting = False
        Me.lblCustCD_L.TextValue = "XXXXX"
        Me.lblCustCD_L.UseSystemPasswordChar = False
        Me.lblCustCD_L.WidthDef = 70
        Me.lblCustCD_L.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.AutoSizeDef = True
        Me.lblCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblCust.EnableStatus = False
        Me.lblCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCust.HeightDef = 13
        Me.lblCust.Location = New System.Drawing.Point(58, 33)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.Size = New System.Drawing.Size(35, 13)
        Me.lblCust.TabIndex = 185
        Me.lblCust.Text = "荷主"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCust.TextValue = "荷主"
        Me.lblCust.WidthDef = 35
        '
        'lblCustNM_M
        '
        Me.lblCustNM_M.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM_M.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM_M.CountWrappedLine = False
        Me.lblCustNM_M.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM_M.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM_M.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM_M.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM_M.HeightDef = 18
        Me.lblCustNM_M.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM_M.HissuLabelVisible = False
        Me.lblCustNM_M.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM_M.IsByteCheck = 0
        Me.lblCustNM_M.IsCalendarCheck = False
        Me.lblCustNM_M.IsDakutenCheck = False
        Me.lblCustNM_M.IsEisuCheck = False
        Me.lblCustNM_M.IsForbiddenWordsCheck = False
        Me.lblCustNM_M.IsFullByteCheck = 0
        Me.lblCustNM_M.IsHankakuCheck = False
        Me.lblCustNM_M.IsHissuCheck = False
        Me.lblCustNM_M.IsKanaCheck = False
        Me.lblCustNM_M.IsMiddleSpace = False
        Me.lblCustNM_M.IsNumericCheck = False
        Me.lblCustNM_M.IsSujiCheck = False
        Me.lblCustNM_M.IsZenkakuCheck = False
        Me.lblCustNM_M.ItemName = ""
        Me.lblCustNM_M.LineSpace = 0
        Me.lblCustNM_M.Location = New System.Drawing.Point(646, 30)
        Me.lblCustNM_M.MaxLength = 0
        Me.lblCustNM_M.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM_M.MaxLineCount = 0
        Me.lblCustNM_M.Multiline = False
        Me.lblCustNM_M.Name = "lblCustNM_M"
        Me.lblCustNM_M.ReadOnly = True
        Me.lblCustNM_M.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM_M.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM_M.Size = New System.Drawing.Size(528, 18)
        Me.lblCustNM_M.TabIndex = 150
        Me.lblCustNM_M.TabStop = False
        Me.lblCustNM_M.TabStopSetting = False
        Me.lblCustNM_M.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblCustNM_M.UseSystemPasswordChar = False
        Me.lblCustNM_M.WidthDef = 528
        Me.lblCustNM_M.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsCD
        '
        Me.lblGoodsCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCD.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsCD.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCD.CountWrappedLine = False
        Me.lblGoodsCD.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCD.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCD.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCD.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsCD.HeightDef = 18
        Me.lblGoodsCD.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCD.HissuLabelVisible = False
        Me.lblGoodsCD.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsCD.IsByteCheck = 20
        Me.lblGoodsCD.IsCalendarCheck = False
        Me.lblGoodsCD.IsDakutenCheck = False
        Me.lblGoodsCD.IsEisuCheck = False
        Me.lblGoodsCD.IsForbiddenWordsCheck = False
        Me.lblGoodsCD.IsFullByteCheck = 0
        Me.lblGoodsCD.IsHankakuCheck = False
        Me.lblGoodsCD.IsHissuCheck = False
        Me.lblGoodsCD.IsKanaCheck = False
        Me.lblGoodsCD.IsMiddleSpace = False
        Me.lblGoodsCD.IsNumericCheck = False
        Me.lblGoodsCD.IsSujiCheck = False
        Me.lblGoodsCD.IsZenkakuCheck = False
        Me.lblGoodsCD.ItemName = ""
        Me.lblGoodsCD.LineSpace = 0
        Me.lblGoodsCD.Location = New System.Drawing.Point(99, 51)
        Me.lblGoodsCD.MaxLength = 20
        Me.lblGoodsCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsCD.MaxLineCount = 0
        Me.lblGoodsCD.Multiline = False
        Me.lblGoodsCD.Name = "lblGoodsCD"
        Me.lblGoodsCD.ReadOnly = True
        Me.lblGoodsCD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsCD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsCD.Size = New System.Drawing.Size(167, 18)
        Me.lblGoodsCD.TabIndex = 160
        Me.lblGoodsCD.TabStop = False
        Me.lblGoodsCD.TabStopSetting = False
        Me.lblGoodsCD.Tag = ""
        Me.lblGoodsCD.TextValue = "X---10---XX---10---X"
        Me.lblGoodsCD.UseSystemPasswordChar = False
        Me.lblGoodsCD.WidthDef = 167
        Me.lblGoodsCD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblGoodsNM
        '
        Me.lblGoodsNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNM.CountWrappedLine = False
        Me.lblGoodsNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNM.HeightDef = 18
        Me.lblGoodsNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNM.HissuLabelVisible = False
        Me.lblGoodsNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsNM.IsByteCheck = 0
        Me.lblGoodsNM.IsCalendarCheck = False
        Me.lblGoodsNM.IsDakutenCheck = False
        Me.lblGoodsNM.IsEisuCheck = False
        Me.lblGoodsNM.IsForbiddenWordsCheck = False
        Me.lblGoodsNM.IsFullByteCheck = 0
        Me.lblGoodsNM.IsHankakuCheck = False
        Me.lblGoodsNM.IsHissuCheck = False
        Me.lblGoodsNM.IsKanaCheck = False
        Me.lblGoodsNM.IsMiddleSpace = False
        Me.lblGoodsNM.IsNumericCheck = False
        Me.lblGoodsNM.IsSujiCheck = False
        Me.lblGoodsNM.IsZenkakuCheck = False
        Me.lblGoodsNM.ItemName = ""
        Me.lblGoodsNM.LineSpace = 0
        Me.lblGoodsNM.Location = New System.Drawing.Point(250, 51)
        Me.lblGoodsNM.MaxLength = 0
        Me.lblGoodsNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNM.MaxLineCount = 0
        Me.lblGoodsNM.Multiline = False
        Me.lblGoodsNM.Name = "lblGoodsNM"
        Me.lblGoodsNM.ReadOnly = True
        Me.lblGoodsNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNM.Size = New System.Drawing.Size(505, 18)
        Me.lblGoodsNM.TabIndex = 170
        Me.lblGoodsNM.TabStop = False
        Me.lblGoodsNM.TabStopSetting = False
        Me.lblGoodsNM.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblGoodsNM.UseSystemPasswordChar = False
        Me.lblGoodsNM.WidthDef = 505
        Me.lblGoodsNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtLotNO
        '
        Me.txtLotNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNO.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLotNO.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLotNO.CountWrappedLine = False
        Me.txtLotNO.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLotNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNO.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLotNO.HeightDef = 18
        Me.txtLotNO.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLotNO.HissuLabelVisible = False
        Me.txtLotNO.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLotNO.IsByteCheck = 40
        Me.txtLotNO.IsCalendarCheck = False
        Me.txtLotNO.IsDakutenCheck = False
        Me.txtLotNO.IsEisuCheck = False
        Me.txtLotNO.IsForbiddenWordsCheck = False
        Me.txtLotNO.IsFullByteCheck = 0
        Me.txtLotNO.IsHankakuCheck = False
        Me.txtLotNO.IsHissuCheck = False
        Me.txtLotNO.IsKanaCheck = False
        Me.txtLotNO.IsMiddleSpace = False
        Me.txtLotNO.IsNumericCheck = False
        Me.txtLotNO.IsSujiCheck = False
        Me.txtLotNO.IsZenkakuCheck = False
        Me.txtLotNO.ItemName = ""
        Me.txtLotNO.LineSpace = 0
        Me.txtLotNO.Location = New System.Drawing.Point(429, 72)
        Me.txtLotNO.MaxLength = 40
        Me.txtLotNO.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLotNO.MaxLineCount = 0
        Me.txtLotNO.Multiline = False
        Me.txtLotNO.Name = "txtLotNO"
        Me.txtLotNO.ReadOnly = False
        Me.txtLotNO.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLotNO.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLotNO.Size = New System.Drawing.Size(326, 18)
        Me.txtLotNO.TabIndex = 2
        Me.txtLotNO.TabStopSetting = True
        Me.txtLotNO.TextValue = "X---10---XX---10---XX---10---XX---10---X"
        Me.txtLotNO.UseSystemPasswordChar = False
        Me.txtLotNO.WidthDef = 326
        Me.txtLotNO.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSerialNO
        '
        Me.txtSerialNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSerialNO.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerialNO.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSerialNO.CountWrappedLine = False
        Me.txtSerialNO.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSerialNO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSerialNO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSerialNO.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSerialNO.HeightDef = 18
        Me.txtSerialNO.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSerialNO.HissuLabelVisible = False
        Me.txtSerialNO.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtSerialNO.IsByteCheck = 40
        Me.txtSerialNO.IsCalendarCheck = False
        Me.txtSerialNO.IsDakutenCheck = False
        Me.txtSerialNO.IsEisuCheck = False
        Me.txtSerialNO.IsForbiddenWordsCheck = False
        Me.txtSerialNO.IsFullByteCheck = 0
        Me.txtSerialNO.IsHankakuCheck = False
        Me.txtSerialNO.IsHissuCheck = False
        Me.txtSerialNO.IsKanaCheck = False
        Me.txtSerialNO.IsMiddleSpace = False
        Me.txtSerialNO.IsNumericCheck = False
        Me.txtSerialNO.IsSujiCheck = False
        Me.txtSerialNO.IsZenkakuCheck = False
        Me.txtSerialNO.ItemName = ""
        Me.txtSerialNO.LineSpace = 0
        Me.txtSerialNO.Location = New System.Drawing.Point(848, 51)
        Me.txtSerialNO.MaxLength = 40
        Me.txtSerialNO.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSerialNO.MaxLineCount = 0
        Me.txtSerialNO.Multiline = False
        Me.txtSerialNO.Name = "txtSerialNO"
        Me.txtSerialNO.ReadOnly = False
        Me.txtSerialNO.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSerialNO.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSerialNO.Size = New System.Drawing.Size(326, 18)
        Me.txtSerialNO.TabIndex = 0
        Me.txtSerialNO.TabStopSetting = True
        Me.txtSerialNO.TextValue = "X---10---XX---10---XX---10---XX---10---X"
        Me.txtSerialNO.UseSystemPasswordChar = False
        Me.txtSerialNO.WidthDef = 326
        Me.txtSerialNO.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'frmCnt
        '
        Me.frmCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.frmCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.frmCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.frmCnt.Controls.Add(Me.lblSyukkaSouCnt)
        Me.frmCnt.Controls.Add(Me.txtSyukkaHasu)
        Me.frmCnt.Controls.Add(Me.numSyukkaHasu)
        Me.frmCnt.Controls.Add(Me.lblSyukkaHasu)
        Me.frmCnt.Controls.Add(Me.lblHikiCntSum)
        Me.frmCnt.Controls.Add(Me.numHikiCntSum)
        Me.frmCnt.Controls.Add(Me.txtSyukkaKosu)
        Me.frmCnt.Controls.Add(Me.lblHikiZanCnt)
        Me.frmCnt.Controls.Add(Me.numHikiZanCnt)
        Me.frmCnt.Controls.Add(Me.numSyukkaSouCnt)
        Me.frmCnt.Controls.Add(Me.lblSyukkaKosu)
        Me.frmCnt.Controls.Add(Me.numSyukkaKosu)
        Me.frmCnt.Controls.Add(Me.lblHikiSumiCnt)
        Me.frmCnt.Controls.Add(Me.numHikiSumiCnt)
        Me.frmCnt.Controls.Add(Me.lblIrisu)
        Me.frmCnt.Controls.Add(Me.numIrisu)
        Me.frmCnt.EnableStatus = False
        Me.frmCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.frmCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.frmCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.frmCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.frmCnt.HeightDef = 109
        Me.frmCnt.Location = New System.Drawing.Point(33, 96)
        Me.frmCnt.Name = "frmCnt"
        Me.frmCnt.Size = New System.Drawing.Size(496, 109)
        Me.frmCnt.TabIndex = 5
        Me.frmCnt.TabStop = False
        Me.frmCnt.Text = "個数"
        Me.frmCnt.TextValue = "個数"
        Me.frmCnt.WidthDef = 496
        '
        'lblSyukkaSouCnt
        '
        Me.lblSyukkaSouCnt.AutoSize = True
        Me.lblSyukkaSouCnt.AutoSizeDef = True
        Me.lblSyukkaSouCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSyukkaSouCnt.EnableStatus = False
        Me.lblSyukkaSouCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouCnt.HeightDef = 13
        Me.lblSyukkaSouCnt.Location = New System.Drawing.Point(216, 65)
        Me.lblSyukkaSouCnt.Name = "lblSyukkaSouCnt"
        Me.lblSyukkaSouCnt.Size = New System.Drawing.Size(63, 13)
        Me.lblSyukkaSouCnt.TabIndex = 115
        Me.lblSyukkaSouCnt.Text = "作業個数"
        Me.lblSyukkaSouCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSyukkaSouCnt.TextValue = "作業個数"
        Me.lblSyukkaSouCnt.WidthDef = 63
        '
        'txtSyukkaHasu
        '
        Me.txtSyukkaHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSyukkaHasu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSyukkaHasu.CountWrappedLine = False
        Me.txtSyukkaHasu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSyukkaHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaHasu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSyukkaHasu.HeightDef = 18
        Me.txtSyukkaHasu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaHasu.HissuLabelVisible = False
        Me.txtSyukkaHasu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSyukkaHasu.IsByteCheck = 0
        Me.txtSyukkaHasu.IsCalendarCheck = False
        Me.txtSyukkaHasu.IsDakutenCheck = False
        Me.txtSyukkaHasu.IsEisuCheck = False
        Me.txtSyukkaHasu.IsForbiddenWordsCheck = False
        Me.txtSyukkaHasu.IsFullByteCheck = 0
        Me.txtSyukkaHasu.IsHankakuCheck = False
        Me.txtSyukkaHasu.IsHissuCheck = False
        Me.txtSyukkaHasu.IsKanaCheck = False
        Me.txtSyukkaHasu.IsMiddleSpace = False
        Me.txtSyukkaHasu.IsNumericCheck = False
        Me.txtSyukkaHasu.IsSujiCheck = False
        Me.txtSyukkaHasu.IsZenkakuCheck = False
        Me.txtSyukkaHasu.ItemName = ""
        Me.txtSyukkaHasu.LineSpace = 0
        Me.txtSyukkaHasu.Location = New System.Drawing.Point(366, 41)
        Me.txtSyukkaHasu.MaxLength = 0
        Me.txtSyukkaHasu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSyukkaHasu.MaxLineCount = 0
        Me.txtSyukkaHasu.Multiline = False
        Me.txtSyukkaHasu.Name = "txtSyukkaHasu"
        Me.txtSyukkaHasu.ReadOnly = True
        Me.txtSyukkaHasu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSyukkaHasu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSyukkaHasu.Size = New System.Drawing.Size(125, 18)
        Me.txtSyukkaHasu.TabIndex = 216
        Me.txtSyukkaHasu.TabStop = False
        Me.txtSyukkaHasu.TabStopSetting = False
        Me.txtSyukkaHasu.TextValue = "ＮＮＮＮＮＮＮ"
        Me.txtSyukkaHasu.UseSystemPasswordChar = False
        Me.txtSyukkaHasu.WidthDef = 125
        Me.txtSyukkaHasu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numSyukkaHasu
        '
        Me.numSyukkaHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSyukkaHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSyukkaHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSyukkaHasu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSyukkaHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaHasu.HeightDef = 18
        Me.numSyukkaHasu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaHasu.HissuLabelVisible = False
        Me.numSyukkaHasu.IsHissuCheck = False
        Me.numSyukkaHasu.IsRangeCheck = False
        Me.numSyukkaHasu.ItemName = ""
        Me.numSyukkaHasu.Location = New System.Drawing.Point(285, 41)
        Me.numSyukkaHasu.Name = "numSyukkaHasu"
        Me.numSyukkaHasu.ReadOnly = False
        Me.numSyukkaHasu.Size = New System.Drawing.Size(97, 18)
        Me.numSyukkaHasu.TabIndex = 215
        Me.numSyukkaHasu.TabStopSetting = True
        Me.numSyukkaHasu.TextValue = "0"
        Me.numSyukkaHasu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSyukkaHasu.WidthDef = 97
        '
        'lblSyukkaHasu
        '
        Me.lblSyukkaHasu.AutoSize = True
        Me.lblSyukkaHasu.AutoSizeDef = True
        Me.lblSyukkaHasu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaHasu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaHasu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSyukkaHasu.EnableStatus = False
        Me.lblSyukkaHasu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaHasu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaHasu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaHasu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaHasu.HeightDef = 13
        Me.lblSyukkaHasu.Location = New System.Drawing.Point(244, 44)
        Me.lblSyukkaHasu.Name = "lblSyukkaHasu"
        Me.lblSyukkaHasu.Size = New System.Drawing.Size(35, 13)
        Me.lblSyukkaHasu.TabIndex = 214
        Me.lblSyukkaHasu.Text = "端数"
        Me.lblSyukkaHasu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSyukkaHasu.TextValue = "端数"
        Me.lblSyukkaHasu.WidthDef = 35
        '
        'lblHikiCntSum
        '
        Me.lblHikiCntSum.AutoSize = True
        Me.lblHikiCntSum.AutoSizeDef = True
        Me.lblHikiCntSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiCntSum.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiCntSum.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiCntSum.EnableStatus = False
        Me.lblHikiCntSum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiCntSum.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiCntSum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiCntSum.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiCntSum.HeightDef = 13
        Me.lblHikiCntSum.Location = New System.Drawing.Point(188, 86)
        Me.lblHikiCntSum.Name = "lblHikiCntSum"
        Me.lblHikiCntSum.Size = New System.Drawing.Size(91, 13)
        Me.lblHikiCntSum.TabIndex = 120
        Me.lblHikiCntSum.Text = "引当個数合計"
        Me.lblHikiCntSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiCntSum.TextValue = "引当個数合計"
        Me.lblHikiCntSum.WidthDef = 91
        '
        'numHikiCntSum
        '
        Me.numHikiCntSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiCntSum.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiCntSum.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiCntSum.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiCntSum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiCntSum.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiCntSum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiCntSum.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiCntSum.HeightDef = 18
        Me.numHikiCntSum.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiCntSum.HissuLabelVisible = False
        Me.numHikiCntSum.IsHissuCheck = False
        Me.numHikiCntSum.IsRangeCheck = False
        Me.numHikiCntSum.ItemName = ""
        Me.numHikiCntSum.Location = New System.Drawing.Point(285, 83)
        Me.numHikiCntSum.Name = "numHikiCntSum"
        Me.numHikiCntSum.ReadOnly = True
        Me.numHikiCntSum.Size = New System.Drawing.Size(97, 18)
        Me.numHikiCntSum.TabIndex = 14
        Me.numHikiCntSum.TabStop = False
        Me.numHikiCntSum.TabStopSetting = False
        Me.numHikiCntSum.TextValue = "0"
        Me.numHikiCntSum.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiCntSum.WidthDef = 97
        '
        'txtSyukkaKosu
        '
        Me.txtSyukkaKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSyukkaKosu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSyukkaKosu.CountWrappedLine = False
        Me.txtSyukkaKosu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSyukkaKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSyukkaKosu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSyukkaKosu.HeightDef = 18
        Me.txtSyukkaKosu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSyukkaKosu.HissuLabelVisible = False
        Me.txtSyukkaKosu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSyukkaKosu.IsByteCheck = 0
        Me.txtSyukkaKosu.IsCalendarCheck = False
        Me.txtSyukkaKosu.IsDakutenCheck = False
        Me.txtSyukkaKosu.IsEisuCheck = False
        Me.txtSyukkaKosu.IsForbiddenWordsCheck = False
        Me.txtSyukkaKosu.IsFullByteCheck = 0
        Me.txtSyukkaKosu.IsHankakuCheck = False
        Me.txtSyukkaKosu.IsHissuCheck = False
        Me.txtSyukkaKosu.IsKanaCheck = False
        Me.txtSyukkaKosu.IsMiddleSpace = False
        Me.txtSyukkaKosu.IsNumericCheck = False
        Me.txtSyukkaKosu.IsSujiCheck = False
        Me.txtSyukkaKosu.IsZenkakuCheck = False
        Me.txtSyukkaKosu.ItemName = ""
        Me.txtSyukkaKosu.LineSpace = 0
        Me.txtSyukkaKosu.Location = New System.Drawing.Point(366, 20)
        Me.txtSyukkaKosu.MaxLength = 0
        Me.txtSyukkaKosu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSyukkaKosu.MaxLineCount = 0
        Me.txtSyukkaKosu.Multiline = False
        Me.txtSyukkaKosu.Name = "txtSyukkaKosu"
        Me.txtSyukkaKosu.ReadOnly = True
        Me.txtSyukkaKosu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSyukkaKosu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSyukkaKosu.Size = New System.Drawing.Size(125, 18)
        Me.txtSyukkaKosu.TabIndex = 210
        Me.txtSyukkaKosu.TabStop = False
        Me.txtSyukkaKosu.TabStopSetting = False
        Me.txtSyukkaKosu.TextValue = "ＮＮＮＮＮＮＮ"
        Me.txtSyukkaKosu.UseSystemPasswordChar = False
        Me.txtSyukkaKosu.WidthDef = 125
        Me.txtSyukkaKosu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblHikiZanCnt
        '
        Me.lblHikiZanCnt.AutoSize = True
        Me.lblHikiZanCnt.AutoSizeDef = True
        Me.lblHikiZanCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiZanCnt.EnableStatus = False
        Me.lblHikiZanCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanCnt.HeightDef = 13
        Me.lblHikiZanCnt.Location = New System.Drawing.Point(12, 65)
        Me.lblHikiZanCnt.Name = "lblHikiZanCnt"
        Me.lblHikiZanCnt.Size = New System.Drawing.Size(77, 13)
        Me.lblHikiZanCnt.TabIndex = 117
        Me.lblHikiZanCnt.Text = "引当残個数"
        Me.lblHikiZanCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiZanCnt.TextValue = "引当残個数"
        Me.lblHikiZanCnt.WidthDef = 77
        '
        'numHikiZanCnt
        '
        Me.numHikiZanCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiZanCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiZanCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiZanCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiZanCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiZanCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiZanCnt.HeightDef = 18
        Me.numHikiZanCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanCnt.HissuLabelVisible = False
        Me.numHikiZanCnt.IsHissuCheck = False
        Me.numHikiZanCnt.IsRangeCheck = False
        Me.numHikiZanCnt.ItemName = ""
        Me.numHikiZanCnt.Location = New System.Drawing.Point(95, 62)
        Me.numHikiZanCnt.Name = "numHikiZanCnt"
        Me.numHikiZanCnt.ReadOnly = True
        Me.numHikiZanCnt.Size = New System.Drawing.Size(97, 18)
        Me.numHikiZanCnt.TabIndex = 13
        Me.numHikiZanCnt.TabStop = False
        Me.numHikiZanCnt.TabStopSetting = False
        Me.numHikiZanCnt.TextValue = "0"
        Me.numHikiZanCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiZanCnt.WidthDef = 97
        '
        'numSyukkaSouCnt
        '
        Me.numSyukkaSouCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSyukkaSouCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSyukkaSouCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaSouCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaSouCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaSouCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaSouCnt.HeightDef = 18
        Me.numSyukkaSouCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouCnt.HissuLabelVisible = False
        Me.numSyukkaSouCnt.IsHissuCheck = False
        Me.numSyukkaSouCnt.IsRangeCheck = False
        Me.numSyukkaSouCnt.ItemName = ""
        Me.numSyukkaSouCnt.Location = New System.Drawing.Point(285, 62)
        Me.numSyukkaSouCnt.Name = "numSyukkaSouCnt"
        Me.numSyukkaSouCnt.ReadOnly = True
        Me.numSyukkaSouCnt.Size = New System.Drawing.Size(97, 18)
        Me.numSyukkaSouCnt.TabIndex = 11
        Me.numSyukkaSouCnt.TabStop = False
        Me.numSyukkaSouCnt.TabStopSetting = False
        Me.numSyukkaSouCnt.TextValue = "0"
        Me.numSyukkaSouCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSyukkaSouCnt.WidthDef = 97
        '
        'lblSyukkaKosu
        '
        Me.lblSyukkaKosu.AutoSize = True
        Me.lblSyukkaKosu.AutoSizeDef = True
        Me.lblSyukkaKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSyukkaKosu.EnableStatus = False
        Me.lblSyukkaKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaKosu.HeightDef = 13
        Me.lblSyukkaKosu.Location = New System.Drawing.Point(244, 22)
        Me.lblSyukkaKosu.Name = "lblSyukkaKosu"
        Me.lblSyukkaKosu.Size = New System.Drawing.Size(35, 13)
        Me.lblSyukkaKosu.TabIndex = 113
        Me.lblSyukkaKosu.Text = "梱数"
        Me.lblSyukkaKosu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSyukkaKosu.TextValue = "梱数"
        Me.lblSyukkaKosu.WidthDef = 35
        '
        'numSyukkaKosu
        '
        Me.numSyukkaKosu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSyukkaKosu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSyukkaKosu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSyukkaKosu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSyukkaKosu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaKosu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaKosu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaKosu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaKosu.HeightDef = 18
        Me.numSyukkaKosu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaKosu.HissuLabelVisible = False
        Me.numSyukkaKosu.IsHissuCheck = False
        Me.numSyukkaKosu.IsRangeCheck = False
        Me.numSyukkaKosu.ItemName = ""
        Me.numSyukkaKosu.Location = New System.Drawing.Point(285, 20)
        Me.numSyukkaKosu.Name = "numSyukkaKosu"
        Me.numSyukkaKosu.ReadOnly = False
        Me.numSyukkaKosu.Size = New System.Drawing.Size(97, 18)
        Me.numSyukkaKosu.TabIndex = 9
        Me.numSyukkaKosu.TabStopSetting = True
        Me.numSyukkaKosu.TextValue = "0"
        Me.numSyukkaKosu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSyukkaKosu.WidthDef = 97
        '
        'lblHikiSumiCnt
        '
        Me.lblHikiSumiCnt.AutoSize = True
        Me.lblHikiSumiCnt.AutoSizeDef = True
        Me.lblHikiSumiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiSumiCnt.EnableStatus = False
        Me.lblHikiSumiCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiCnt.HeightDef = 13
        Me.lblHikiSumiCnt.Location = New System.Drawing.Point(12, 44)
        Me.lblHikiSumiCnt.Name = "lblHikiSumiCnt"
        Me.lblHikiSumiCnt.Size = New System.Drawing.Size(77, 13)
        Me.lblHikiSumiCnt.TabIndex = 111
        Me.lblHikiSumiCnt.Text = "引当済個数"
        Me.lblHikiSumiCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiSumiCnt.TextValue = "引当済個数"
        Me.lblHikiSumiCnt.WidthDef = 77
        '
        'numHikiSumiCnt
        '
        Me.numHikiSumiCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiSumiCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiSumiCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiSumiCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiSumiCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiSumiCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiSumiCnt.HeightDef = 18
        Me.numHikiSumiCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiCnt.HissuLabelVisible = False
        Me.numHikiSumiCnt.IsHissuCheck = False
        Me.numHikiSumiCnt.IsRangeCheck = False
        Me.numHikiSumiCnt.ItemName = ""
        Me.numHikiSumiCnt.Location = New System.Drawing.Point(95, 41)
        Me.numHikiSumiCnt.Name = "numHikiSumiCnt"
        Me.numHikiSumiCnt.ReadOnly = True
        Me.numHikiSumiCnt.Size = New System.Drawing.Size(97, 18)
        Me.numHikiSumiCnt.TabIndex = 12
        Me.numHikiSumiCnt.TabStop = False
        Me.numHikiSumiCnt.TabStopSetting = False
        Me.numHikiSumiCnt.TextValue = "0"
        Me.numHikiSumiCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiSumiCnt.WidthDef = 97
        '
        'lblIrisu
        '
        Me.lblIrisu.AutoSize = True
        Me.lblIrisu.AutoSizeDef = True
        Me.lblIrisu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblIrisu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblIrisu.EnableStatus = False
        Me.lblIrisu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblIrisu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIrisu.HeightDef = 13
        Me.lblIrisu.Location = New System.Drawing.Point(54, 22)
        Me.lblIrisu.Name = "lblIrisu"
        Me.lblIrisu.Size = New System.Drawing.Size(35, 13)
        Me.lblIrisu.TabIndex = 109
        Me.lblIrisu.Text = "入数"
        Me.lblIrisu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblIrisu.TextValue = "入数"
        Me.lblIrisu.WidthDef = 35
        '
        'numIrisu
        '
        Me.numIrisu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrisu.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrisu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrisu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrisu.HeightDef = 18
        Me.numIrisu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrisu.HissuLabelVisible = False
        Me.numIrisu.IsHissuCheck = False
        Me.numIrisu.IsRangeCheck = False
        Me.numIrisu.ItemName = ""
        Me.numIrisu.Location = New System.Drawing.Point(95, 20)
        Me.numIrisu.Name = "numIrisu"
        Me.numIrisu.ReadOnly = True
        Me.numIrisu.Size = New System.Drawing.Size(97, 18)
        Me.numIrisu.TabIndex = 10
        Me.numIrisu.TabStop = False
        Me.numIrisu.TabStopSetting = False
        Me.numIrisu.TextValue = "0"
        Me.numIrisu.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrisu.WidthDef = 97
        '
        'frmAmount
        '
        Me.frmAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.frmAmount.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.frmAmount.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.frmAmount.Controls.Add(Me.lblHikiAmtSum)
        Me.frmAmount.Controls.Add(Me.numHikiAmtSum)
        Me.frmAmount.Controls.Add(Me.lblSyukkaSouAmt)
        Me.frmAmount.Controls.Add(Me.lblHikiZanAmt)
        Me.frmAmount.Controls.Add(Me.numHikiZanAmt)
        Me.frmAmount.Controls.Add(Me.lblSyukkaSouAmtl)
        Me.frmAmount.Controls.Add(Me.numSyukkaSouAmt)
        Me.frmAmount.Controls.Add(Me.lblHikiSumiAmt)
        Me.frmAmount.Controls.Add(Me.numHikiSumiAmt)
        Me.frmAmount.EnableStatus = False
        Me.frmAmount.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.frmAmount.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.frmAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.frmAmount.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.frmAmount.HeightDef = 109
        Me.frmAmount.Location = New System.Drawing.Point(535, 96)
        Me.frmAmount.Name = "frmAmount"
        Me.frmAmount.Size = New System.Drawing.Size(359, 109)
        Me.frmAmount.TabIndex = 6
        Me.frmAmount.TabStop = False
        Me.frmAmount.Text = "数量"
        Me.frmAmount.TextValue = "数量"
        Me.frmAmount.WidthDef = 359
        '
        'lblHikiAmtSum
        '
        Me.lblHikiAmtSum.AutoSize = True
        Me.lblHikiAmtSum.AutoSizeDef = True
        Me.lblHikiAmtSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiAmtSum.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiAmtSum.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiAmtSum.EnableStatus = False
        Me.lblHikiAmtSum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiAmtSum.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiAmtSum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiAmtSum.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiAmtSum.HeightDef = 13
        Me.lblHikiAmtSum.Location = New System.Drawing.Point(6, 86)
        Me.lblHikiAmtSum.Name = "lblHikiAmtSum"
        Me.lblHikiAmtSum.Size = New System.Drawing.Size(91, 13)
        Me.lblHikiAmtSum.TabIndex = 127
        Me.lblHikiAmtSum.Text = "引当数量合計"
        Me.lblHikiAmtSum.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiAmtSum.TextValue = "引当数量合計"
        Me.lblHikiAmtSum.WidthDef = 91
        '
        'numHikiAmtSum
        '
        Me.numHikiAmtSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiAmtSum.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiAmtSum.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiAmtSum.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiAmtSum.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiAmtSum.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiAmtSum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiAmtSum.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiAmtSum.HeightDef = 18
        Me.numHikiAmtSum.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiAmtSum.HissuLabelVisible = False
        Me.numHikiAmtSum.IsHissuCheck = False
        Me.numHikiAmtSum.IsRangeCheck = False
        Me.numHikiAmtSum.ItemName = ""
        Me.numHikiAmtSum.Location = New System.Drawing.Point(103, 83)
        Me.numHikiAmtSum.Name = "numHikiAmtSum"
        Me.numHikiAmtSum.ReadOnly = True
        Me.numHikiAmtSum.Size = New System.Drawing.Size(140, 18)
        Me.numHikiAmtSum.TabIndex = 18
        Me.numHikiAmtSum.TabStop = False
        Me.numHikiAmtSum.TabStopSetting = False
        Me.numHikiAmtSum.TextValue = "0"
        Me.numHikiAmtSum.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiAmtSum.WidthDef = 140
        '
        'lblSyukkaSouAmt
        '
        Me.lblSyukkaSouAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyukkaSouAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSyukkaSouAmt.CountWrappedLine = False
        Me.lblSyukkaSouAmt.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSyukkaSouAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouAmt.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSyukkaSouAmt.HeightDef = 18
        Me.lblSyukkaSouAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouAmt.HissuLabelVisible = False
        Me.lblSyukkaSouAmt.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSyukkaSouAmt.IsByteCheck = 0
        Me.lblSyukkaSouAmt.IsCalendarCheck = False
        Me.lblSyukkaSouAmt.IsDakutenCheck = False
        Me.lblSyukkaSouAmt.IsEisuCheck = False
        Me.lblSyukkaSouAmt.IsForbiddenWordsCheck = False
        Me.lblSyukkaSouAmt.IsFullByteCheck = 0
        Me.lblSyukkaSouAmt.IsHankakuCheck = False
        Me.lblSyukkaSouAmt.IsHissuCheck = False
        Me.lblSyukkaSouAmt.IsKanaCheck = False
        Me.lblSyukkaSouAmt.IsMiddleSpace = False
        Me.lblSyukkaSouAmt.IsNumericCheck = False
        Me.lblSyukkaSouAmt.IsSujiCheck = False
        Me.lblSyukkaSouAmt.IsZenkakuCheck = False
        Me.lblSyukkaSouAmt.ItemName = ""
        Me.lblSyukkaSouAmt.LineSpace = 0
        Me.lblSyukkaSouAmt.Location = New System.Drawing.Point(227, 20)
        Me.lblSyukkaSouAmt.MaxLength = 0
        Me.lblSyukkaSouAmt.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSyukkaSouAmt.MaxLineCount = 0
        Me.lblSyukkaSouAmt.Multiline = False
        Me.lblSyukkaSouAmt.Name = "lblSyukkaSouAmt"
        Me.lblSyukkaSouAmt.ReadOnly = True
        Me.lblSyukkaSouAmt.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSyukkaSouAmt.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSyukkaSouAmt.Size = New System.Drawing.Size(125, 18)
        Me.lblSyukkaSouAmt.TabIndex = 220
        Me.lblSyukkaSouAmt.TabStop = False
        Me.lblSyukkaSouAmt.TabStopSetting = False
        Me.lblSyukkaSouAmt.TextValue = "ＮＮＮＮＮＮＮ"
        Me.lblSyukkaSouAmt.UseSystemPasswordChar = False
        Me.lblSyukkaSouAmt.WidthDef = 125
        Me.lblSyukkaSouAmt.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblHikiZanAmt
        '
        Me.lblHikiZanAmt.AutoSize = True
        Me.lblHikiZanAmt.AutoSizeDef = True
        Me.lblHikiZanAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiZanAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiZanAmt.EnableStatus = False
        Me.lblHikiZanAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiZanAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiZanAmt.HeightDef = 13
        Me.lblHikiZanAmt.Location = New System.Drawing.Point(20, 64)
        Me.lblHikiZanAmt.Name = "lblHikiZanAmt"
        Me.lblHikiZanAmt.Size = New System.Drawing.Size(77, 13)
        Me.lblHikiZanAmt.TabIndex = 124
        Me.lblHikiZanAmt.Text = "引当残数量"
        Me.lblHikiZanAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiZanAmt.TextValue = "引当残数量"
        Me.lblHikiZanAmt.WidthDef = 77
        '
        'numHikiZanAmt
        '
        Me.numHikiZanAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiZanAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiZanAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiZanAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiZanAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiZanAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiZanAmt.HeightDef = 18
        Me.numHikiZanAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiZanAmt.HissuLabelVisible = False
        Me.numHikiZanAmt.IsHissuCheck = False
        Me.numHikiZanAmt.IsRangeCheck = False
        Me.numHikiZanAmt.ItemName = ""
        Me.numHikiZanAmt.Location = New System.Drawing.Point(103, 62)
        Me.numHikiZanAmt.Name = "numHikiZanAmt"
        Me.numHikiZanAmt.ReadOnly = True
        Me.numHikiZanAmt.Size = New System.Drawing.Size(140, 18)
        Me.numHikiZanAmt.TabIndex = 17
        Me.numHikiZanAmt.TabStop = False
        Me.numHikiZanAmt.TabStopSetting = False
        Me.numHikiZanAmt.TextValue = "0"
        Me.numHikiZanAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiZanAmt.WidthDef = 140
        '
        'lblSyukkaSouAmtl
        '
        Me.lblSyukkaSouAmtl.AutoSize = True
        Me.lblSyukkaSouAmtl.AutoSizeDef = True
        Me.lblSyukkaSouAmtl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouAmtl.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSyukkaSouAmtl.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSyukkaSouAmtl.EnableStatus = False
        Me.lblSyukkaSouAmtl.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouAmtl.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaSouAmtl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouAmtl.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSyukkaSouAmtl.HeightDef = 13
        Me.lblSyukkaSouAmtl.Location = New System.Drawing.Point(34, 23)
        Me.lblSyukkaSouAmtl.Name = "lblSyukkaSouAmtl"
        Me.lblSyukkaSouAmtl.Size = New System.Drawing.Size(63, 13)
        Me.lblSyukkaSouAmtl.TabIndex = 122
        Me.lblSyukkaSouAmtl.Text = "作業数量"
        Me.lblSyukkaSouAmtl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSyukkaSouAmtl.TextValue = "作業数量"
        Me.lblSyukkaSouAmtl.WidthDef = 63
        '
        'numSyukkaSouAmt
        '
        Me.numSyukkaSouAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSyukkaSouAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSyukkaSouAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaSouAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyukkaSouAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaSouAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSyukkaSouAmt.HeightDef = 18
        Me.numSyukkaSouAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSyukkaSouAmt.HissuLabelVisible = False
        Me.numSyukkaSouAmt.IsHissuCheck = False
        Me.numSyukkaSouAmt.IsRangeCheck = False
        Me.numSyukkaSouAmt.ItemName = ""
        Me.numSyukkaSouAmt.Location = New System.Drawing.Point(103, 20)
        Me.numSyukkaSouAmt.Name = "numSyukkaSouAmt"
        Me.numSyukkaSouAmt.ReadOnly = True
        Me.numSyukkaSouAmt.Size = New System.Drawing.Size(140, 18)
        Me.numSyukkaSouAmt.TabIndex = 15
        Me.numSyukkaSouAmt.TabStop = False
        Me.numSyukkaSouAmt.TabStopSetting = False
        Me.numSyukkaSouAmt.TextValue = "0"
        Me.numSyukkaSouAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSyukkaSouAmt.WidthDef = 140
        '
        'lblHikiSumiAmt
        '
        Me.lblHikiSumiAmt.AutoSize = True
        Me.lblHikiSumiAmt.AutoSizeDef = True
        Me.lblHikiSumiAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblHikiSumiAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblHikiSumiAmt.EnableStatus = False
        Me.lblHikiSumiAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHikiSumiAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblHikiSumiAmt.HeightDef = 13
        Me.lblHikiSumiAmt.Location = New System.Drawing.Point(20, 43)
        Me.lblHikiSumiAmt.Name = "lblHikiSumiAmt"
        Me.lblHikiSumiAmt.Size = New System.Drawing.Size(77, 13)
        Me.lblHikiSumiAmt.TabIndex = 120
        Me.lblHikiSumiAmt.Text = "引当済数量"
        Me.lblHikiSumiAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblHikiSumiAmt.TextValue = "引当済数量"
        Me.lblHikiSumiAmt.WidthDef = 77
        '
        'numHikiSumiAmt
        '
        Me.numHikiSumiAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiAmt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiAmt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numHikiSumiAmt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numHikiSumiAmt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiSumiAmt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numHikiSumiAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiSumiAmt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numHikiSumiAmt.HeightDef = 18
        Me.numHikiSumiAmt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numHikiSumiAmt.HissuLabelVisible = False
        Me.numHikiSumiAmt.IsHissuCheck = False
        Me.numHikiSumiAmt.IsRangeCheck = False
        Me.numHikiSumiAmt.ItemName = ""
        Me.numHikiSumiAmt.Location = New System.Drawing.Point(103, 41)
        Me.numHikiSumiAmt.Name = "numHikiSumiAmt"
        Me.numHikiSumiAmt.ReadOnly = True
        Me.numHikiSumiAmt.Size = New System.Drawing.Size(140, 18)
        Me.numHikiSumiAmt.TabIndex = 16
        Me.numHikiSumiAmt.TabStop = False
        Me.numHikiSumiAmt.TabStopSetting = False
        Me.numHikiSumiAmt.TextValue = "0"
        Me.numHikiSumiAmt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numHikiSumiAmt.WidthDef = 140
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
        Me.cmbEigyo.Location = New System.Drawing.Point(99, 9)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(250, 18)
        Me.cmbEigyo.TabIndex = 273
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 250
        '
        'cmbSoko
        '
        Me.cmbSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSoko.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSoko.DataSource = Nothing
        Me.cmbSoko.DisplayMember = Nothing
        Me.cmbSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSoko.HeightDef = 18
        Me.cmbSoko.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSoko.HissuLabelVisible = False
        Me.cmbSoko.InsertWildCard = True
        Me.cmbSoko.IsForbiddenWordsCheck = False
        Me.cmbSoko.IsHissuCheck = False
        Me.cmbSoko.ItemName = ""
        Me.cmbSoko.Location = New System.Drawing.Point(429, 9)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = True
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(250, 18)
        Me.cmbSoko.TabIndex = 274
        Me.cmbSoko.TabStop = False
        Me.cmbSoko.TabStopSetting = False
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 250
        '
        'lblGoodsNRS
        '
        Me.lblGoodsNRS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNRS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNRS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNRS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNRS.CountWrappedLine = False
        Me.lblGoodsNRS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNRS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNRS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNRS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNRS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNRS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNRS.HeightDef = 18
        Me.lblGoodsNRS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNRS.HissuLabelVisible = False
        Me.lblGoodsNRS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsNRS.IsByteCheck = 20
        Me.lblGoodsNRS.IsCalendarCheck = False
        Me.lblGoodsNRS.IsDakutenCheck = False
        Me.lblGoodsNRS.IsEisuCheck = False
        Me.lblGoodsNRS.IsForbiddenWordsCheck = False
        Me.lblGoodsNRS.IsFullByteCheck = 0
        Me.lblGoodsNRS.IsHankakuCheck = False
        Me.lblGoodsNRS.IsHissuCheck = False
        Me.lblGoodsNRS.IsKanaCheck = False
        Me.lblGoodsNRS.IsMiddleSpace = False
        Me.lblGoodsNRS.IsNumericCheck = False
        Me.lblGoodsNRS.IsSujiCheck = False
        Me.lblGoodsNRS.IsZenkakuCheck = False
        Me.lblGoodsNRS.ItemName = ""
        Me.lblGoodsNRS.LineSpace = 0
        Me.lblGoodsNRS.Location = New System.Drawing.Point(1026, 113)
        Me.lblGoodsNRS.MaxLength = 20
        Me.lblGoodsNRS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNRS.MaxLineCount = 0
        Me.lblGoodsNRS.Multiline = False
        Me.lblGoodsNRS.Name = "lblGoodsNRS"
        Me.lblGoodsNRS.ReadOnly = True
        Me.lblGoodsNRS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNRS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNRS.Size = New System.Drawing.Size(167, 18)
        Me.lblGoodsNRS.TabIndex = 275
        Me.lblGoodsNRS.TabStop = False
        Me.lblGoodsNRS.TabStopSetting = False
        Me.lblGoodsNRS.Tag = ""
        Me.lblGoodsNRS.TextValue = "X---10---XX---10---X"
        Me.lblGoodsNRS.UseSystemPasswordChar = False
        Me.lblGoodsNRS.Visible = False
        Me.lblGoodsNRS.WidthDef = 167
        Me.lblGoodsNRS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numIrime
        '
        Me.numIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numIrime.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numIrime.HeightDef = 18
        Me.numIrime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numIrime.HissuLabelVisible = False
        Me.numIrime.IsHissuCheck = False
        Me.numIrime.IsRangeCheck = False
        Me.numIrime.ItemName = ""
        Me.numIrime.Location = New System.Drawing.Point(848, 72)
        Me.numIrime.Name = "numIrime"
        Me.numIrime.ReadOnly = False
        Me.numIrime.Size = New System.Drawing.Size(98, 18)
        Me.numIrime.TabIndex = 3
        Me.numIrime.TabStopSetting = True
        Me.numIrime.TextValue = "0"
        Me.numIrime.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numIrime.WidthDef = 98
        '
        'LME050F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LME050F"
        Me.Text = "【LME050】 在庫引当"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprZaiko, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frmCnt.ResumeLayout(False)
        Me.frmCnt.PerformLayout()
        Me.frmAmount.ResumeLayout(False)
        Me.frmAmount.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprZaiko As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents lblIrimeTani As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblIrime As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSerialNO As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblLotNO As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRsvNO As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblReserveNO As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblGoods As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSoko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCD_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustCD_L As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM_M As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsCD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtLotNO As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSerialNO As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents frmAmount As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents frmCnt As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblIrisu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numIrisu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblHikiSumiCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiSumiCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtSyukkaKosu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblHikiZanCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiZanCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSyukkaSouCnt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSyukkaSouCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSyukkaKosu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSyukkaKosu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSyukkaSouAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblHikiZanAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiZanAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSyukkaSouAmtl As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSyukkaSouAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblHikiSumiAmt As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiSumiAmt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblHikiCntSum As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiCntSum As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblHikiAmtSum As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numHikiAmtSum As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents txtSyukkaHasu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numSyukkaHasu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblSyukkaHasu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents lblGoodsNRS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents numIrime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber

End Class
