<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LME020F
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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        Me.lblTitleSoko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSagyoRecNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoRecNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyoComp = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSkyuChk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.lblTitleSagyoSijiNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoSijiNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbIozsKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleIozsKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleInOutkaNoLM = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtInOutkaNoLM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyoCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleDestCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtDestNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtDestCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsCdCust = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleLotNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeiqtoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSeiqto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbTaxKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleTaxKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSagyoNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSagyoNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSagyoUp = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSagyoUp = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.numSagyoGk = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblTitleSagyoGk = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbInvTani = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleInvTani = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSagyoCompDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdSagyoCompDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.txtSagyoCompNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoCompCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyCompCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemarkZai = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRemarkZai = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemarkSkyu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRemarkSkyu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsCdKey = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtGoodsCdCustHide = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbSagyoCompKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbSkyuChk = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSagyoUpCurr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblSagyoGkCurr = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.pnlViewAria.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblSagyoGkCurr)
        Me.pnlViewAria.Controls.Add(Me.lblSagyoUpCurr)
        Me.pnlViewAria.Controls.Add(Me.cmbSkyuChk)
        Me.pnlViewAria.Controls.Add(Me.cmbSagyoCompKb)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCdCustHide)
        Me.pnlViewAria.Controls.Add(Me.txtUpdTime)
        Me.pnlViewAria.Controls.Add(Me.txtUpdDate)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCdKey)
        Me.pnlViewAria.Controls.Add(Me.txtRemarkSkyu)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRemarkSkyu)
        Me.pnlViewAria.Controls.Add(Me.txtRemarkZai)
        Me.pnlViewAria.Controls.Add(Me.lblTitleRemarkZai)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoCompNm)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoCompCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyCompCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoCompDate)
        Me.pnlViewAria.Controls.Add(Me.imdSagyoCompDate)
        Me.pnlViewAria.Controls.Add(Me.cmbInvTani)
        Me.pnlViewAria.Controls.Add(Me.lblTitleInvTani)
        Me.pnlViewAria.Controls.Add(Me.numSagyoGk)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoGk)
        Me.pnlViewAria.Controls.Add(Me.numSagyoUp)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoUp)
        Me.pnlViewAria.Controls.Add(Me.numSagyoNb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoNb)
        Me.pnlViewAria.Controls.Add(Me.cmbTaxKb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleTaxKb)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoNm)
        Me.pnlViewAria.Controls.Add(Me.txtSeiqtoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSeiqto)
        Me.pnlViewAria.Controls.Add(Me.lblTitleLotNo)
        Me.pnlViewAria.Controls.Add(Me.txtLotNo)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsNm)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCdCust)
        Me.pnlViewAria.Controls.Add(Me.lblTitleGoodsCd)
        Me.pnlViewAria.Controls.Add(Me.txtDestNm)
        Me.pnlViewAria.Controls.Add(Me.txtDestCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleDestCd)
        Me.pnlViewAria.Controls.Add(Me.txtCustNm)
        Me.pnlViewAria.Controls.Add(Me.lblTitleCustCd)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoNm)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoCd)
        Me.pnlViewAria.Controls.Add(Me.lblTitleInOutkaNoLM)
        Me.pnlViewAria.Controls.Add(Me.txtInOutkaNoLM)
        Me.pnlViewAria.Controls.Add(Me.cmbIozsKb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleIozsKb)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoSijiNo)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoSijiNo)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSkyuChk)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoComp)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSagyoRecNo)
        Me.pnlViewAria.Controls.Add(Me.txtSagyoRecNo)
        Me.pnlViewAria.Controls.Add(Me.cmbSoko)
        Me.pnlViewAria.Controls.Add(Me.lblTitleSoko)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdM)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdL)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.F10ButtonName = "マスタ参照"
        Me.FunctionKey.F11ButtonName = "保　存"
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F1ButtonName = "新　規"
        Me.FunctionKey.F2ButtonName = "編　集"
        Me.FunctionKey.F3ButtonName = "複　写"
        Me.FunctionKey.F4ButtonName = "削　除"
        Me.FunctionKey.F7ButtonName = "スキップ"
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
        Me.cmbEigyo.Location = New System.Drawing.Point(150, 31)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 251
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(98, 34)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.cmbSoko.HissuLabelVisible = True
        Me.cmbSoko.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cmbSoko.InsertWildCard = True
        Me.cmbSoko.IsForbiddenWordsCheck = False
        Me.cmbSoko.IsHissuCheck = True
        Me.cmbSoko.ItemName = ""
        Me.cmbSoko.Location = New System.Drawing.Point(536, 31)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = True
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(250, 18)
        Me.cmbSoko.TabIndex = 442
        Me.cmbSoko.TabStop = False
        Me.cmbSoko.TabStopSetting = False
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 250
        '
        'lblTitleSoko
        '
        Me.lblTitleSoko.AutoSize = True
        Me.lblTitleSoko.AutoSizeDef = True
        Me.lblTitleSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSoko.EnableStatus = False
        Me.lblTitleSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSoko.HeightDef = 13
        Me.lblTitleSoko.Location = New System.Drawing.Point(501, 34)
        Me.lblTitleSoko.Name = "lblTitleSoko"
        Me.lblTitleSoko.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSoko.TabIndex = 441
        Me.lblTitleSoko.Text = "倉庫"
        Me.lblTitleSoko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSoko.TextValue = "倉庫"
        Me.lblTitleSoko.WidthDef = 35
        '
        'lblTitleSagyoRecNo
        '
        Me.lblTitleSagyoRecNo.AutoSize = True
        Me.lblTitleSagyoRecNo.AutoSizeDef = True
        Me.lblTitleSagyoRecNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoRecNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoRecNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoRecNo.EnableStatus = False
        Me.lblTitleSagyoRecNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoRecNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoRecNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoRecNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoRecNo.HeightDef = 13
        Me.lblTitleSagyoRecNo.Location = New System.Drawing.Point(28, 57)
        Me.lblTitleSagyoRecNo.Name = "lblTitleSagyoRecNo"
        Me.lblTitleSagyoRecNo.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleSagyoRecNo.TabIndex = 443
        Me.lblTitleSagyoRecNo.Text = "作業レコード番号"
        Me.lblTitleSagyoRecNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoRecNo.TextValue = "作業レコード番号"
        Me.lblTitleSagyoRecNo.WidthDef = 119
        '
        'txtSagyoRecNo
        '
        Me.txtSagyoRecNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRecNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRecNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRecNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRecNo.CountWrappedLine = False
        Me.txtSagyoRecNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRecNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRecNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRecNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRecNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRecNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRecNo.HeightDef = 18
        Me.txtSagyoRecNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRecNo.HissuLabelVisible = False
        Me.txtSagyoRecNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRecNo.IsByteCheck = 10
        Me.txtSagyoRecNo.IsCalendarCheck = False
        Me.txtSagyoRecNo.IsDakutenCheck = False
        Me.txtSagyoRecNo.IsEisuCheck = False
        Me.txtSagyoRecNo.IsForbiddenWordsCheck = False
        Me.txtSagyoRecNo.IsFullByteCheck = 0
        Me.txtSagyoRecNo.IsHankakuCheck = False
        Me.txtSagyoRecNo.IsHissuCheck = False
        Me.txtSagyoRecNo.IsKanaCheck = False
        Me.txtSagyoRecNo.IsMiddleSpace = False
        Me.txtSagyoRecNo.IsNumericCheck = False
        Me.txtSagyoRecNo.IsSujiCheck = False
        Me.txtSagyoRecNo.IsZenkakuCheck = False
        Me.txtSagyoRecNo.ItemName = ""
        Me.txtSagyoRecNo.LineSpace = 0
        Me.txtSagyoRecNo.Location = New System.Drawing.Point(150, 54)
        Me.txtSagyoRecNo.MaxLength = 10
        Me.txtSagyoRecNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRecNo.MaxLineCount = 0
        Me.txtSagyoRecNo.Multiline = False
        Me.txtSagyoRecNo.Name = "txtSagyoRecNo"
        Me.txtSagyoRecNo.ReadOnly = True
        Me.txtSagyoRecNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRecNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRecNo.Size = New System.Drawing.Size(198, 18)
        Me.txtSagyoRecNo.TabIndex = 444
        Me.txtSagyoRecNo.TabStop = False
        Me.txtSagyoRecNo.TabStopSetting = False
        Me.txtSagyoRecNo.TextValue = ""
        Me.txtSagyoRecNo.UseSystemPasswordChar = False
        Me.txtSagyoRecNo.WidthDef = 198
        Me.txtSagyoRecNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyoComp
        '
        Me.lblTitleSagyoComp.AutoSize = True
        Me.lblTitleSagyoComp.AutoSizeDef = True
        Me.lblTitleSagyoComp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoComp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoComp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoComp.EnableStatus = False
        Me.lblTitleSagyoComp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoComp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoComp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoComp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoComp.HeightDef = 13
        Me.lblTitleSagyoComp.Location = New System.Drawing.Point(444, 59)
        Me.lblTitleSagyoComp.Name = "lblTitleSagyoComp"
        Me.lblTitleSagyoComp.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleSagyoComp.TabIndex = 445
        Me.lblTitleSagyoComp.Text = "作業完了区分"
        Me.lblTitleSagyoComp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoComp.TextValue = "作業完了区分"
        Me.lblTitleSagyoComp.WidthDef = 91
        '
        'lblTitleSkyuChk
        '
        Me.lblTitleSkyuChk.AutoSize = True
        Me.lblTitleSkyuChk.AutoSizeDef = True
        Me.lblTitleSkyuChk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSkyuChk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSkyuChk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSkyuChk.EnableStatus = False
        Me.lblTitleSkyuChk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSkyuChk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSkyuChk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSkyuChk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSkyuChk.HeightDef = 13
        Me.lblTitleSkyuChk.Location = New System.Drawing.Point(815, 61)
        Me.lblTitleSkyuChk.Name = "lblTitleSkyuChk"
        Me.lblTitleSkyuChk.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSkyuChk.TabIndex = 447
        Me.lblTitleSkyuChk.Text = "請求確認フラグ"
        Me.lblTitleSkyuChk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSkyuChk.TextValue = "請求確認フラグ"
        Me.lblTitleSkyuChk.WidthDef = 105
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1128, 14)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 449
        Me.lblSituation.TabStop = False
        '
        'lblTitleSagyoSijiNo
        '
        Me.lblTitleSagyoSijiNo.AutoSize = True
        Me.lblTitleSagyoSijiNo.AutoSizeDef = True
        Me.lblTitleSagyoSijiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSijiNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSijiNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoSijiNo.EnableStatus = False
        Me.lblTitleSagyoSijiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSijiNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSijiNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSijiNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSijiNo.HeightDef = 13
        Me.lblTitleSagyoSijiNo.Location = New System.Drawing.Point(42, 103)
        Me.lblTitleSagyoSijiNo.Name = "lblTitleSagyoSijiNo"
        Me.lblTitleSagyoSijiNo.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSagyoSijiNo.TabIndex = 450
        Me.lblTitleSagyoSijiNo.Text = "作業指示書番号"
        Me.lblTitleSagyoSijiNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoSijiNo.TextValue = "作業指示書番号"
        Me.lblTitleSagyoSijiNo.WidthDef = 105
        '
        'txtSagyoSijiNo
        '
        Me.txtSagyoSijiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoSijiNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoSijiNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoSijiNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoSijiNo.CountWrappedLine = False
        Me.txtSagyoSijiNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoSijiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSijiNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSijiNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSijiNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSijiNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoSijiNo.HeightDef = 18
        Me.txtSagyoSijiNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoSijiNo.HissuLabelVisible = False
        Me.txtSagyoSijiNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoSijiNo.IsByteCheck = 10
        Me.txtSagyoSijiNo.IsCalendarCheck = False
        Me.txtSagyoSijiNo.IsDakutenCheck = False
        Me.txtSagyoSijiNo.IsEisuCheck = False
        Me.txtSagyoSijiNo.IsForbiddenWordsCheck = False
        Me.txtSagyoSijiNo.IsFullByteCheck = 0
        Me.txtSagyoSijiNo.IsHankakuCheck = False
        Me.txtSagyoSijiNo.IsHissuCheck = False
        Me.txtSagyoSijiNo.IsKanaCheck = False
        Me.txtSagyoSijiNo.IsMiddleSpace = False
        Me.txtSagyoSijiNo.IsNumericCheck = False
        Me.txtSagyoSijiNo.IsSujiCheck = False
        Me.txtSagyoSijiNo.IsZenkakuCheck = False
        Me.txtSagyoSijiNo.ItemName = ""
        Me.txtSagyoSijiNo.LineSpace = 0
        Me.txtSagyoSijiNo.Location = New System.Drawing.Point(150, 100)
        Me.txtSagyoSijiNo.MaxLength = 10
        Me.txtSagyoSijiNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoSijiNo.MaxLineCount = 0
        Me.txtSagyoSijiNo.Multiline = False
        Me.txtSagyoSijiNo.Name = "txtSagyoSijiNo"
        Me.txtSagyoSijiNo.ReadOnly = True
        Me.txtSagyoSijiNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoSijiNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoSijiNo.Size = New System.Drawing.Size(198, 18)
        Me.txtSagyoSijiNo.TabIndex = 451
        Me.txtSagyoSijiNo.TabStop = False
        Me.txtSagyoSijiNo.TabStopSetting = False
        Me.txtSagyoSijiNo.TextValue = ""
        Me.txtSagyoSijiNo.UseSystemPasswordChar = False
        Me.txtSagyoSijiNo.WidthDef = 198
        Me.txtSagyoSijiNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbIozsKb
        '
        Me.cmbIozsKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIozsKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbIozsKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbIozsKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbIozsKb.DataCode = "M010"
        Me.cmbIozsKb.DataSource = Nothing
        Me.cmbIozsKb.DisplayMember = Nothing
        Me.cmbIozsKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIozsKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbIozsKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIozsKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbIozsKb.HeightDef = 18
        Me.cmbIozsKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIozsKb.HissuLabelVisible = True
        Me.cmbIozsKb.InsertWildCard = True
        Me.cmbIozsKb.IsForbiddenWordsCheck = False
        Me.cmbIozsKb.IsHissuCheck = True
        Me.cmbIozsKb.ItemName = ""
        Me.cmbIozsKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbIozsKb.Location = New System.Drawing.Point(536, 102)
        Me.cmbIozsKb.Name = "cmbIozsKb"
        Me.cmbIozsKb.ReadOnly = False
        Me.cmbIozsKb.SelectedIndex = -1
        Me.cmbIozsKb.SelectedItem = Nothing
        Me.cmbIozsKb.SelectedText = ""
        Me.cmbIozsKb.SelectedValue = ""
        Me.cmbIozsKb.Size = New System.Drawing.Size(117, 18)
        Me.cmbIozsKb.TabIndex = 452
        Me.cmbIozsKb.TabStopSetting = True
        Me.cmbIozsKb.TextValue = ""
        Me.cmbIozsKb.Value1 = Nothing
        Me.cmbIozsKb.Value2 = Nothing
        Me.cmbIozsKb.Value3 = Nothing
        Me.cmbIozsKb.ValueMember = Nothing
        Me.cmbIozsKb.WidthDef = 117
        '
        'lblTitleIozsKb
        '
        Me.lblTitleIozsKb.AutoSize = True
        Me.lblTitleIozsKb.AutoSizeDef = True
        Me.lblTitleIozsKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIozsKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleIozsKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleIozsKb.EnableStatus = False
        Me.lblTitleIozsKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIozsKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleIozsKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIozsKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleIozsKb.HeightDef = 13
        Me.lblTitleIozsKb.Location = New System.Drawing.Point(416, 105)
        Me.lblTitleIozsKb.Name = "lblTitleIozsKb"
        Me.lblTitleIozsKb.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleIozsKb.TabIndex = 453
        Me.lblTitleIozsKb.Text = "入出在その他区分"
        Me.lblTitleIozsKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIozsKb.TextValue = "入出在その他区分"
        Me.lblTitleIozsKb.WidthDef = 119
        '
        'lblTitleInOutkaNoLM
        '
        Me.lblTitleInOutkaNoLM.AutoSize = True
        Me.lblTitleInOutkaNoLM.AutoSizeDef = True
        Me.lblTitleInOutkaNoLM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInOutkaNoLM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInOutkaNoLM.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInOutkaNoLM.EnableStatus = False
        Me.lblTitleInOutkaNoLM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInOutkaNoLM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInOutkaNoLM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInOutkaNoLM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInOutkaNoLM.HeightDef = 13
        Me.lblTitleInOutkaNoLM.Location = New System.Drawing.Point(815, 107)
        Me.lblTitleInOutkaNoLM.Name = "lblTitleInOutkaNoLM"
        Me.lblTitleInOutkaNoLM.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleInOutkaNoLM.TabIndex = 454
        Me.lblTitleInOutkaNoLM.Text = "入出荷管理番号"
        Me.lblTitleInOutkaNoLM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInOutkaNoLM.TextValue = "入出荷管理番号"
        Me.lblTitleInOutkaNoLM.WidthDef = 105
        '
        'txtInOutkaNoLM
        '
        Me.txtInOutkaNoLM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInOutkaNoLM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInOutkaNoLM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInOutkaNoLM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtInOutkaNoLM.CountWrappedLine = False
        Me.txtInOutkaNoLM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtInOutkaNoLM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInOutkaNoLM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInOutkaNoLM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInOutkaNoLM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInOutkaNoLM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtInOutkaNoLM.HeightDef = 18
        Me.txtInOutkaNoLM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInOutkaNoLM.HissuLabelVisible = False
        Me.txtInOutkaNoLM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtInOutkaNoLM.IsByteCheck = 13
        Me.txtInOutkaNoLM.IsCalendarCheck = False
        Me.txtInOutkaNoLM.IsDakutenCheck = False
        Me.txtInOutkaNoLM.IsEisuCheck = False
        Me.txtInOutkaNoLM.IsForbiddenWordsCheck = False
        Me.txtInOutkaNoLM.IsFullByteCheck = 0
        Me.txtInOutkaNoLM.IsHankakuCheck = False
        Me.txtInOutkaNoLM.IsHissuCheck = False
        Me.txtInOutkaNoLM.IsKanaCheck = False
        Me.txtInOutkaNoLM.IsMiddleSpace = False
        Me.txtInOutkaNoLM.IsNumericCheck = False
        Me.txtInOutkaNoLM.IsSujiCheck = False
        Me.txtInOutkaNoLM.IsZenkakuCheck = False
        Me.txtInOutkaNoLM.ItemName = ""
        Me.txtInOutkaNoLM.LineSpace = 0
        Me.txtInOutkaNoLM.Location = New System.Drawing.Point(923, 104)
        Me.txtInOutkaNoLM.MaxLength = 13
        Me.txtInOutkaNoLM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtInOutkaNoLM.MaxLineCount = 0
        Me.txtInOutkaNoLM.Multiline = False
        Me.txtInOutkaNoLM.Name = "txtInOutkaNoLM"
        Me.txtInOutkaNoLM.ReadOnly = True
        Me.txtInOutkaNoLM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtInOutkaNoLM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtInOutkaNoLM.Size = New System.Drawing.Size(198, 18)
        Me.txtInOutkaNoLM.TabIndex = 455
        Me.txtInOutkaNoLM.TabStop = False
        Me.txtInOutkaNoLM.TabStopSetting = False
        Me.txtInOutkaNoLM.TextValue = ""
        Me.txtInOutkaNoLM.UseSystemPasswordChar = False
        Me.txtInOutkaNoLM.WidthDef = 198
        Me.txtInOutkaNoLM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoNm
        '
        Me.txtSagyoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoNm.CountWrappedLine = False
        Me.txtSagyoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoNm.HeightDef = 18
        Me.txtSagyoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoNm.HissuLabelVisible = True
        Me.txtSagyoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoNm.IsByteCheck = 60
        Me.txtSagyoNm.IsCalendarCheck = False
        Me.txtSagyoNm.IsDakutenCheck = False
        Me.txtSagyoNm.IsEisuCheck = False
        Me.txtSagyoNm.IsForbiddenWordsCheck = False
        Me.txtSagyoNm.IsFullByteCheck = 0
        Me.txtSagyoNm.IsHankakuCheck = False
        Me.txtSagyoNm.IsHissuCheck = True
        Me.txtSagyoNm.IsKanaCheck = False
        Me.txtSagyoNm.IsMiddleSpace = False
        Me.txtSagyoNm.IsNumericCheck = False
        Me.txtSagyoNm.IsSujiCheck = False
        Me.txtSagyoNm.IsZenkakuCheck = False
        Me.txtSagyoNm.ItemName = ""
        Me.txtSagyoNm.LineSpace = 0
        Me.txtSagyoNm.Location = New System.Drawing.Point(267, 124)
        Me.txtSagyoNm.MaxLength = 60
        Me.txtSagyoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoNm.MaxLineCount = 0
        Me.txtSagyoNm.Multiline = False
        Me.txtSagyoNm.Name = "txtSagyoNm"
        Me.txtSagyoNm.ReadOnly = False
        Me.txtSagyoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoNm.Size = New System.Drawing.Size(523, 18)
        Me.txtSagyoNm.TabIndex = 458
        Me.txtSagyoNm.TabStopSetting = True
        Me.txtSagyoNm.TextValue = ""
        Me.txtSagyoNm.UseSystemPasswordChar = False
        Me.txtSagyoNm.WidthDef = 523
        Me.txtSagyoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoCd
        '
        Me.txtSagyoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCd.CountWrappedLine = False
        Me.txtSagyoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCd.HeightDef = 18
        Me.txtSagyoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCd.HissuLabelVisible = False
        Me.txtSagyoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtSagyoCd.IsByteCheck = 15
        Me.txtSagyoCd.IsCalendarCheck = False
        Me.txtSagyoCd.IsDakutenCheck = False
        Me.txtSagyoCd.IsEisuCheck = False
        Me.txtSagyoCd.IsForbiddenWordsCheck = False
        Me.txtSagyoCd.IsFullByteCheck = 0
        Me.txtSagyoCd.IsHankakuCheck = False
        Me.txtSagyoCd.IsHissuCheck = False
        Me.txtSagyoCd.IsKanaCheck = False
        Me.txtSagyoCd.IsMiddleSpace = False
        Me.txtSagyoCd.IsNumericCheck = False
        Me.txtSagyoCd.IsSujiCheck = False
        Me.txtSagyoCd.IsZenkakuCheck = False
        Me.txtSagyoCd.ItemName = ""
        Me.txtSagyoCd.LineSpace = 0
        Me.txtSagyoCd.Location = New System.Drawing.Point(150, 124)
        Me.txtSagyoCd.MaxLength = 15
        Me.txtSagyoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCd.MaxLineCount = 0
        Me.txtSagyoCd.Multiline = False
        Me.txtSagyoCd.Name = "txtSagyoCd"
        Me.txtSagyoCd.ReadOnly = True
        Me.txtSagyoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCd.Size = New System.Drawing.Size(133, 18)
        Me.txtSagyoCd.TabIndex = 457
        Me.txtSagyoCd.TabStop = False
        Me.txtSagyoCd.TabStopSetting = False
        Me.txtSagyoCd.TextValue = ""
        Me.txtSagyoCd.UseSystemPasswordChar = False
        Me.txtSagyoCd.WidthDef = 133
        Me.txtSagyoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyoCd
        '
        Me.lblTitleSagyoCd.AutoSize = True
        Me.lblTitleSagyoCd.AutoSizeDef = True
        Me.lblTitleSagyoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoCd.EnableStatus = False
        Me.lblTitleSagyoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoCd.HeightDef = 13
        Me.lblTitleSagyoCd.Location = New System.Drawing.Point(112, 127)
        Me.lblTitleSagyoCd.Name = "lblTitleSagyoCd"
        Me.lblTitleSagyoCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSagyoCd.TabIndex = 456
        Me.lblTitleSagyoCd.Text = "作業"
        Me.lblTitleSagyoCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoCd.TextValue = "作業"
        Me.lblTitleSagyoCd.WidthDef = 35
        '
        'txtCustNm
        '
        Me.txtCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustNm.CountWrappedLine = False
        Me.txtCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustNm.HeightDef = 18
        Me.txtCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustNm.HissuLabelVisible = True
        Me.txtCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtCustNm.IsByteCheck = 0
        Me.txtCustNm.IsCalendarCheck = False
        Me.txtCustNm.IsDakutenCheck = False
        Me.txtCustNm.IsEisuCheck = False
        Me.txtCustNm.IsForbiddenWordsCheck = False
        Me.txtCustNm.IsFullByteCheck = 0
        Me.txtCustNm.IsHankakuCheck = False
        Me.txtCustNm.IsHissuCheck = True
        Me.txtCustNm.IsKanaCheck = False
        Me.txtCustNm.IsMiddleSpace = False
        Me.txtCustNm.IsNumericCheck = False
        Me.txtCustNm.IsSujiCheck = False
        Me.txtCustNm.IsZenkakuCheck = False
        Me.txtCustNm.ItemName = ""
        Me.txtCustNm.LineSpace = 0
        Me.txtCustNm.Location = New System.Drawing.Point(267, 147)
        Me.txtCustNm.MaxLength = 0
        Me.txtCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustNm.MaxLineCount = 0
        Me.txtCustNm.Multiline = False
        Me.txtCustNm.Name = "txtCustNm"
        Me.txtCustNm.ReadOnly = True
        Me.txtCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustNm.Size = New System.Drawing.Size(523, 18)
        Me.txtCustNm.TabIndex = 462
        Me.txtCustNm.TabStop = False
        Me.txtCustNm.TabStopSetting = False
        Me.txtCustNm.TextValue = ""
        Me.txtCustNm.UseSystemPasswordChar = False
        Me.txtCustNm.WidthDef = 523
        Me.txtCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCustCd.Location = New System.Drawing.Point(112, 150)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustCd.TabIndex = 459
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
        Me.txtCustCdL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
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
        Me.txtCustCdL.Location = New System.Drawing.Point(150, 147)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 460
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleDestCd
        '
        Me.lblTitleDestCd.AutoSize = True
        Me.lblTitleDestCd.AutoSizeDef = True
        Me.lblTitleDestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDestCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDestCd.EnableStatus = False
        Me.lblTitleDestCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDestCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDestCd.HeightDef = 13
        Me.lblTitleDestCd.Location = New System.Drawing.Point(112, 172)
        Me.lblTitleDestCd.Name = "lblTitleDestCd"
        Me.lblTitleDestCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleDestCd.TabIndex = 463
        Me.lblTitleDestCd.Text = "届先"
        Me.lblTitleDestCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDestCd.TextValue = "届先"
        Me.lblTitleDestCd.WidthDef = 35
        '
        'txtDestNm
        '
        Me.txtDestNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestNm.CountWrappedLine = False
        Me.txtDestNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestNm.HeightDef = 18
        Me.txtDestNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestNm.HissuLabelVisible = False
        Me.txtDestNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtDestNm.IsByteCheck = 80
        Me.txtDestNm.IsCalendarCheck = False
        Me.txtDestNm.IsDakutenCheck = False
        Me.txtDestNm.IsEisuCheck = False
        Me.txtDestNm.IsForbiddenWordsCheck = False
        Me.txtDestNm.IsFullByteCheck = 0
        Me.txtDestNm.IsHankakuCheck = False
        Me.txtDestNm.IsHissuCheck = False
        Me.txtDestNm.IsKanaCheck = False
        Me.txtDestNm.IsMiddleSpace = False
        Me.txtDestNm.IsNumericCheck = False
        Me.txtDestNm.IsSujiCheck = False
        Me.txtDestNm.IsZenkakuCheck = False
        Me.txtDestNm.ItemName = ""
        Me.txtDestNm.LineSpace = 0
        Me.txtDestNm.Location = New System.Drawing.Point(267, 170)
        Me.txtDestNm.MaxLength = 80
        Me.txtDestNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestNm.MaxLineCount = 0
        Me.txtDestNm.Multiline = False
        Me.txtDestNm.Name = "txtDestNm"
        Me.txtDestNm.ReadOnly = False
        Me.txtDestNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestNm.Size = New System.Drawing.Size(434, 18)
        Me.txtDestNm.TabIndex = 465
        Me.txtDestNm.TabStopSetting = True
        Me.txtDestNm.TextValue = ""
        Me.txtDestNm.UseSystemPasswordChar = False
        Me.txtDestNm.WidthDef = 434
        Me.txtDestNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtDestCd.Location = New System.Drawing.Point(150, 170)
        Me.txtDestCd.MaxLength = 15
        Me.txtDestCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestCd.MaxLineCount = 0
        Me.txtDestCd.Multiline = False
        Me.txtDestCd.Name = "txtDestCd"
        Me.txtDestCd.ReadOnly = False
        Me.txtDestCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestCd.Size = New System.Drawing.Size(133, 18)
        Me.txtDestCd.TabIndex = 464
        Me.txtDestCd.TabStopSetting = True
        Me.txtDestCd.TextValue = ""
        Me.txtDestCd.UseSystemPasswordChar = False
        Me.txtDestCd.WidthDef = 133
        Me.txtDestCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsNm
        '
        Me.txtGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsNm.CountWrappedLine = False
        Me.txtGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsNm.HeightDef = 18
        Me.txtGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm.HissuLabelVisible = True
        Me.txtGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsNm.IsByteCheck = 60
        Me.txtGoodsNm.IsCalendarCheck = False
        Me.txtGoodsNm.IsDakutenCheck = False
        Me.txtGoodsNm.IsEisuCheck = False
        Me.txtGoodsNm.IsForbiddenWordsCheck = False
        Me.txtGoodsNm.IsFullByteCheck = 0
        Me.txtGoodsNm.IsHankakuCheck = False
        Me.txtGoodsNm.IsHissuCheck = True
        Me.txtGoodsNm.IsKanaCheck = False
        Me.txtGoodsNm.IsMiddleSpace = False
        Me.txtGoodsNm.IsNumericCheck = False
        Me.txtGoodsNm.IsSujiCheck = False
        Me.txtGoodsNm.IsZenkakuCheck = False
        Me.txtGoodsNm.ItemName = ""
        Me.txtGoodsNm.LineSpace = 0
        Me.txtGoodsNm.Location = New System.Drawing.Point(267, 193)
        Me.txtGoodsNm.MaxLength = 60
        Me.txtGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNm.MaxLineCount = 0
        Me.txtGoodsNm.Multiline = False
        Me.txtGoodsNm.Name = "txtGoodsNm"
        Me.txtGoodsNm.ReadOnly = False
        Me.txtGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNm.Size = New System.Drawing.Size(434, 18)
        Me.txtGoodsNm.TabIndex = 470
        Me.txtGoodsNm.TabStopSetting = True
        Me.txtGoodsNm.TextValue = ""
        Me.txtGoodsNm.UseSystemPasswordChar = False
        Me.txtGoodsNm.WidthDef = 434
        Me.txtGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCdCust
        '
        Me.txtGoodsCdCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGoodsCdCust.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdCust.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdCust.CountWrappedLine = False
        Me.txtGoodsCdCust.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCust.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdCust.HeightDef = 18
        Me.txtGoodsCdCust.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCust.HissuLabelVisible = False
        Me.txtGoodsCdCust.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtGoodsCdCust.IsByteCheck = 20
        Me.txtGoodsCdCust.IsCalendarCheck = False
        Me.txtGoodsCdCust.IsDakutenCheck = False
        Me.txtGoodsCdCust.IsEisuCheck = False
        Me.txtGoodsCdCust.IsForbiddenWordsCheck = False
        Me.txtGoodsCdCust.IsFullByteCheck = 0
        Me.txtGoodsCdCust.IsHankakuCheck = False
        Me.txtGoodsCdCust.IsHissuCheck = False
        Me.txtGoodsCdCust.IsKanaCheck = False
        Me.txtGoodsCdCust.IsMiddleSpace = False
        Me.txtGoodsCdCust.IsNumericCheck = False
        Me.txtGoodsCdCust.IsSujiCheck = False
        Me.txtGoodsCdCust.IsZenkakuCheck = False
        Me.txtGoodsCdCust.ItemName = ""
        Me.txtGoodsCdCust.LineSpace = 0
        Me.txtGoodsCdCust.Location = New System.Drawing.Point(150, 193)
        Me.txtGoodsCdCust.MaxLength = 20
        Me.txtGoodsCdCust.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdCust.MaxLineCount = 0
        Me.txtGoodsCdCust.Multiline = False
        Me.txtGoodsCdCust.Name = "txtGoodsCdCust"
        Me.txtGoodsCdCust.ReadOnly = False
        Me.txtGoodsCdCust.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdCust.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdCust.Size = New System.Drawing.Size(133, 18)
        Me.txtGoodsCdCust.TabIndex = 469
        Me.txtGoodsCdCust.TabStopSetting = True
        Me.txtGoodsCdCust.TextValue = ""
        Me.txtGoodsCdCust.UseSystemPasswordChar = False
        Me.txtGoodsCdCust.WidthDef = 133
        Me.txtGoodsCdCust.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsCd
        '
        Me.lblTitleGoodsCd.AutoSize = True
        Me.lblTitleGoodsCd.AutoSizeDef = True
        Me.lblTitleGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsCd.EnableStatus = False
        Me.lblTitleGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsCd.HeightDef = 13
        Me.lblTitleGoodsCd.Location = New System.Drawing.Point(112, 196)
        Me.lblTitleGoodsCd.Name = "lblTitleGoodsCd"
        Me.lblTitleGoodsCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleGoodsCd.TabIndex = 468
        Me.lblTitleGoodsCd.Text = "商品"
        Me.lblTitleGoodsCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsCd.TextValue = "商品"
        Me.lblTitleGoodsCd.WidthDef = 35
        '
        'lblTitleLotNo
        '
        Me.lblTitleLotNo.AutoSize = True
        Me.lblTitleLotNo.AutoSizeDef = True
        Me.lblTitleLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLotNo.EnableStatus = False
        Me.lblTitleLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLotNo.HeightDef = 13
        Me.lblTitleLotNo.Location = New System.Drawing.Point(857, 197)
        Me.lblTitleLotNo.Name = "lblTitleLotNo"
        Me.lblTitleLotNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleLotNo.TabIndex = 471
        Me.lblTitleLotNo.Text = "ロット№"
        Me.lblTitleLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLotNo.TextValue = "ロット№"
        Me.lblTitleLotNo.WidthDef = 63
        '
        'txtLotNo
        '
        Me.txtLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLotNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtLotNo.CountWrappedLine = False
        Me.txtLotNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLotNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtLotNo.HeightDef = 18
        Me.txtLotNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLotNo.HissuLabelVisible = False
        Me.txtLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX_IME_OFF
        Me.txtLotNo.IsByteCheck = 40
        Me.txtLotNo.IsCalendarCheck = False
        Me.txtLotNo.IsDakutenCheck = False
        Me.txtLotNo.IsEisuCheck = False
        Me.txtLotNo.IsForbiddenWordsCheck = False
        Me.txtLotNo.IsFullByteCheck = 0
        Me.txtLotNo.IsHankakuCheck = False
        Me.txtLotNo.IsHissuCheck = False
        Me.txtLotNo.IsKanaCheck = False
        Me.txtLotNo.IsMiddleSpace = False
        Me.txtLotNo.IsNumericCheck = False
        Me.txtLotNo.IsSujiCheck = False
        Me.txtLotNo.IsZenkakuCheck = False
        Me.txtLotNo.ItemName = ""
        Me.txtLotNo.LineSpace = 0
        Me.txtLotNo.Location = New System.Drawing.Point(923, 194)
        Me.txtLotNo.MaxLength = 40
        Me.txtLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLotNo.MaxLineCount = 0
        Me.txtLotNo.Multiline = False
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ReadOnly = False
        Me.txtLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLotNo.Size = New System.Drawing.Size(198, 18)
        Me.txtLotNo.TabIndex = 472
        Me.txtLotNo.TabStopSetting = True
        Me.txtLotNo.TextValue = ""
        Me.txtLotNo.UseSystemPasswordChar = False
        Me.txtLotNo.WidthDef = 198
        Me.txtLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqtoNm
        '
        Me.txtSeiqtoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoNm.CountWrappedLine = False
        Me.txtSeiqtoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoNm.HeightDef = 18
        Me.txtSeiqtoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoNm.HissuLabelVisible = True
        Me.txtSeiqtoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSeiqtoNm.IsByteCheck = 0
        Me.txtSeiqtoNm.IsCalendarCheck = False
        Me.txtSeiqtoNm.IsDakutenCheck = False
        Me.txtSeiqtoNm.IsEisuCheck = False
        Me.txtSeiqtoNm.IsForbiddenWordsCheck = False
        Me.txtSeiqtoNm.IsFullByteCheck = 0
        Me.txtSeiqtoNm.IsHankakuCheck = False
        Me.txtSeiqtoNm.IsHissuCheck = True
        Me.txtSeiqtoNm.IsKanaCheck = False
        Me.txtSeiqtoNm.IsMiddleSpace = False
        Me.txtSeiqtoNm.IsNumericCheck = False
        Me.txtSeiqtoNm.IsSujiCheck = False
        Me.txtSeiqtoNm.IsZenkakuCheck = False
        Me.txtSeiqtoNm.ItemName = ""
        Me.txtSeiqtoNm.LineSpace = 0
        Me.txtSeiqtoNm.Location = New System.Drawing.Point(267, 216)
        Me.txtSeiqtoNm.MaxLength = 0
        Me.txtSeiqtoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoNm.MaxLineCount = 0
        Me.txtSeiqtoNm.Multiline = False
        Me.txtSeiqtoNm.Name = "txtSeiqtoNm"
        Me.txtSeiqtoNm.ReadOnly = True
        Me.txtSeiqtoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoNm.Size = New System.Drawing.Size(523, 18)
        Me.txtSeiqtoNm.TabIndex = 475
        Me.txtSeiqtoNm.TabStop = False
        Me.txtSeiqtoNm.TabStopSetting = False
        Me.txtSeiqtoNm.TextValue = ""
        Me.txtSeiqtoNm.UseSystemPasswordChar = False
        Me.txtSeiqtoNm.WidthDef = 523
        Me.txtSeiqtoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSeiqtoCd
        '
        Me.txtSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSeiqtoCd.CountWrappedLine = False
        Me.txtSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSeiqtoCd.HeightDef = 18
        Me.txtSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSeiqtoCd.HissuLabelVisible = False
        Me.txtSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtSeiqtoCd.IsByteCheck = 7
        Me.txtSeiqtoCd.IsCalendarCheck = False
        Me.txtSeiqtoCd.IsDakutenCheck = False
        Me.txtSeiqtoCd.IsEisuCheck = False
        Me.txtSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtSeiqtoCd.IsFullByteCheck = 0
        Me.txtSeiqtoCd.IsHankakuCheck = False
        Me.txtSeiqtoCd.IsHissuCheck = False
        Me.txtSeiqtoCd.IsKanaCheck = False
        Me.txtSeiqtoCd.IsMiddleSpace = False
        Me.txtSeiqtoCd.IsNumericCheck = False
        Me.txtSeiqtoCd.IsSujiCheck = False
        Me.txtSeiqtoCd.IsZenkakuCheck = False
        Me.txtSeiqtoCd.ItemName = ""
        Me.txtSeiqtoCd.LineSpace = 0
        Me.txtSeiqtoCd.Location = New System.Drawing.Point(150, 216)
        Me.txtSeiqtoCd.MaxLength = 7
        Me.txtSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSeiqtoCd.MaxLineCount = 0
        Me.txtSeiqtoCd.Multiline = False
        Me.txtSeiqtoCd.Name = "txtSeiqtoCd"
        Me.txtSeiqtoCd.ReadOnly = False
        Me.txtSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSeiqtoCd.Size = New System.Drawing.Size(133, 18)
        Me.txtSeiqtoCd.TabIndex = 474
        Me.txtSeiqtoCd.TabStopSetting = True
        Me.txtSeiqtoCd.TextValue = ""
        Me.txtSeiqtoCd.UseSystemPasswordChar = False
        Me.txtSeiqtoCd.WidthDef = 133
        Me.txtSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeiqto
        '
        Me.lblTitleSeiqto.AutoSize = True
        Me.lblTitleSeiqto.AutoSizeDef = True
        Me.lblTitleSeiqto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeiqto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeiqto.EnableStatus = False
        Me.lblTitleSeiqto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeiqto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeiqto.HeightDef = 13
        Me.lblTitleSeiqto.Location = New System.Drawing.Point(98, 218)
        Me.lblTitleSeiqto.Name = "lblTitleSeiqto"
        Me.lblTitleSeiqto.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeiqto.TabIndex = 473
        Me.lblTitleSeiqto.Text = "請求先"
        Me.lblTitleSeiqto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeiqto.TextValue = "請求先"
        Me.lblTitleSeiqto.WidthDef = 49
        '
        'cmbTaxKb
        '
        Me.cmbTaxKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTaxKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbTaxKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbTaxKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbTaxKb.DataCode = "Z001"
        Me.cmbTaxKb.DataSource = Nothing
        Me.cmbTaxKb.DisplayMember = Nothing
        Me.cmbTaxKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbTaxKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbTaxKb.HeightDef = 18
        Me.cmbTaxKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbTaxKb.HissuLabelVisible = True
        Me.cmbTaxKb.InsertWildCard = True
        Me.cmbTaxKb.IsForbiddenWordsCheck = False
        Me.cmbTaxKb.IsHissuCheck = True
        Me.cmbTaxKb.ItemName = ""
        Me.cmbTaxKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbTaxKb.Location = New System.Drawing.Point(923, 217)
        Me.cmbTaxKb.Name = "cmbTaxKb"
        Me.cmbTaxKb.ReadOnly = False
        Me.cmbTaxKb.SelectedIndex = -1
        Me.cmbTaxKb.SelectedItem = Nothing
        Me.cmbTaxKb.SelectedText = ""
        Me.cmbTaxKb.SelectedValue = ""
        Me.cmbTaxKb.Size = New System.Drawing.Size(117, 18)
        Me.cmbTaxKb.TabIndex = 476
        Me.cmbTaxKb.TabStopSetting = True
        Me.cmbTaxKb.TextValue = ""
        Me.cmbTaxKb.Value1 = Nothing
        Me.cmbTaxKb.Value2 = Nothing
        Me.cmbTaxKb.Value3 = Nothing
        Me.cmbTaxKb.ValueMember = Nothing
        Me.cmbTaxKb.WidthDef = 117
        '
        'lblTitleTaxKb
        '
        Me.lblTitleTaxKb.AutoSize = True
        Me.lblTitleTaxKb.AutoSizeDef = True
        Me.lblTitleTaxKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTaxKb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTaxKb.EnableStatus = False
        Me.lblTitleTaxKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTaxKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTaxKb.HeightDef = 13
        Me.lblTitleTaxKb.Location = New System.Drawing.Point(857, 221)
        Me.lblTitleTaxKb.Name = "lblTitleTaxKb"
        Me.lblTitleTaxKb.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleTaxKb.TabIndex = 477
        Me.lblTitleTaxKb.Text = "課税区分"
        Me.lblTitleTaxKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTaxKb.TextValue = "課税区分"
        Me.lblTitleTaxKb.WidthDef = 63
        '
        'numSagyoNb
        '
        Me.numSagyoNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoNb.HeightDef = 18
        Me.numSagyoNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoNb.HissuLabelVisible = False
        Me.numSagyoNb.IsHissuCheck = False
        Me.numSagyoNb.IsRangeCheck = False
        Me.numSagyoNb.ItemName = ""
        Me.numSagyoNb.Location = New System.Drawing.Point(150, 239)
        Me.numSagyoNb.Name = "numSagyoNb"
        Me.numSagyoNb.ReadOnly = False
        Me.numSagyoNb.Size = New System.Drawing.Size(133, 18)
        Me.numSagyoNb.TabIndex = 478
        Me.numSagyoNb.TabStopSetting = True
        Me.numSagyoNb.TextValue = "0"
        Me.numSagyoNb.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoNb.WidthDef = 133
        '
        'lblTitleSagyoNb
        '
        Me.lblTitleSagyoNb.AutoSize = True
        Me.lblTitleSagyoNb.AutoSizeDef = True
        Me.lblTitleSagyoNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoNb.EnableStatus = False
        Me.lblTitleSagyoNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoNb.HeightDef = 13
        Me.lblTitleSagyoNb.Location = New System.Drawing.Point(98, 242)
        Me.lblTitleSagyoNb.Name = "lblTitleSagyoNb"
        Me.lblTitleSagyoNb.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyoNb.TabIndex = 479
        Me.lblTitleSagyoNb.Text = "請求数"
        Me.lblTitleSagyoNb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoNb.TextValue = "請求数"
        Me.lblTitleSagyoNb.WidthDef = 49
        '
        'numSagyoUp
        '
        Me.numSagyoUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoUp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoUp.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoUp.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoUp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoUp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoUp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoUp.HeightDef = 18
        Me.numSagyoUp.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoUp.HissuLabelVisible = False
        Me.numSagyoUp.IsHissuCheck = False
        Me.numSagyoUp.IsRangeCheck = False
        Me.numSagyoUp.ItemName = ""
        Me.numSagyoUp.Location = New System.Drawing.Point(402, 239)
        Me.numSagyoUp.Name = "numSagyoUp"
        Me.numSagyoUp.ReadOnly = False
        Me.numSagyoUp.Size = New System.Drawing.Size(105, 18)
        Me.numSagyoUp.TabIndex = 480
        Me.numSagyoUp.TabStopSetting = True
        Me.numSagyoUp.TextValue = "0"
        Me.numSagyoUp.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoUp.WidthDef = 105
        '
        'lblTitleSagyoUp
        '
        Me.lblTitleSagyoUp.AutoSize = True
        Me.lblTitleSagyoUp.AutoSizeDef = True
        Me.lblTitleSagyoUp.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoUp.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoUp.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoUp.EnableStatus = False
        Me.lblTitleSagyoUp.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoUp.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoUp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoUp.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoUp.HeightDef = 13
        Me.lblTitleSagyoUp.Location = New System.Drawing.Point(336, 242)
        Me.lblTitleSagyoUp.Name = "lblTitleSagyoUp"
        Me.lblTitleSagyoUp.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSagyoUp.TabIndex = 481
        Me.lblTitleSagyoUp.Text = "請求単価"
        Me.lblTitleSagyoUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoUp.TextValue = "請求単価"
        Me.lblTitleSagyoUp.WidthDef = 63
        '
        'numSagyoGk
        '
        Me.numSagyoGk.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoGk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numSagyoGk.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numSagyoGk.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numSagyoGk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoGk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSagyoGk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoGk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numSagyoGk.HeightDef = 18
        Me.numSagyoGk.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numSagyoGk.HissuLabelVisible = False
        Me.numSagyoGk.IsHissuCheck = False
        Me.numSagyoGk.IsRangeCheck = False
        Me.numSagyoGk.ItemName = ""
        Me.numSagyoGk.Location = New System.Drawing.Point(655, 240)
        Me.numSagyoGk.Name = "numSagyoGk"
        Me.numSagyoGk.ReadOnly = False
        Me.numSagyoGk.Size = New System.Drawing.Size(105, 18)
        Me.numSagyoGk.TabIndex = 482
        Me.numSagyoGk.TabStopSetting = True
        Me.numSagyoGk.TextValue = "0"
        Me.numSagyoGk.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numSagyoGk.WidthDef = 105
        '
        'lblTitleSagyoGk
        '
        Me.lblTitleSagyoGk.AutoSize = True
        Me.lblTitleSagyoGk.AutoSizeDef = True
        Me.lblTitleSagyoGk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoGk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoGk.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoGk.EnableStatus = False
        Me.lblTitleSagyoGk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoGk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoGk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoGk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoGk.HeightDef = 13
        Me.lblTitleSagyoGk.Location = New System.Drawing.Point(589, 243)
        Me.lblTitleSagyoGk.Name = "lblTitleSagyoGk"
        Me.lblTitleSagyoGk.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleSagyoGk.TabIndex = 483
        Me.lblTitleSagyoGk.Text = "作業金額"
        Me.lblTitleSagyoGk.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoGk.TextValue = "作業金額"
        Me.lblTitleSagyoGk.WidthDef = 63
        '
        'cmbInvTani
        '
        Me.cmbInvTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInvTani.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbInvTani.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbInvTani.DataCode = "S027"
        Me.cmbInvTani.DataSource = Nothing
        Me.cmbInvTani.DisplayMember = Nothing
        Me.cmbInvTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInvTani.HeightDef = 18
        Me.cmbInvTani.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbInvTani.HissuLabelVisible = False
        Me.cmbInvTani.InsertWildCard = True
        Me.cmbInvTani.IsForbiddenWordsCheck = False
        Me.cmbInvTani.IsHissuCheck = False
        Me.cmbInvTani.ItemName = ""
        Me.cmbInvTani.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbInvTani.Location = New System.Drawing.Point(150, 262)
        Me.cmbInvTani.Name = "cmbInvTani"
        Me.cmbInvTani.ReadOnly = False
        Me.cmbInvTani.SelectedIndex = -1
        Me.cmbInvTani.SelectedItem = Nothing
        Me.cmbInvTani.SelectedText = ""
        Me.cmbInvTani.SelectedValue = ""
        Me.cmbInvTani.Size = New System.Drawing.Size(118, 18)
        Me.cmbInvTani.TabIndex = 484
        Me.cmbInvTani.TabStopSetting = True
        Me.cmbInvTani.TextValue = ""
        Me.cmbInvTani.Value1 = Nothing
        Me.cmbInvTani.Value2 = Nothing
        Me.cmbInvTani.Value3 = Nothing
        Me.cmbInvTani.ValueMember = Nothing
        Me.cmbInvTani.WidthDef = 118
        '
        'lblTitleInvTani
        '
        Me.lblTitleInvTani.AutoSize = True
        Me.lblTitleInvTani.AutoSizeDef = True
        Me.lblTitleInvTani.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvTani.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleInvTani.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleInvTani.EnableStatus = False
        Me.lblTitleInvTani.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvTani.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleInvTani.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvTani.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleInvTani.HeightDef = 13
        Me.lblTitleInvTani.Location = New System.Drawing.Point(84, 265)
        Me.lblTitleInvTani.Name = "lblTitleInvTani"
        Me.lblTitleInvTani.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleInvTani.TabIndex = 485
        Me.lblTitleInvTani.Text = "請求単位"
        Me.lblTitleInvTani.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleInvTani.TextValue = "請求単位"
        Me.lblTitleInvTani.WidthDef = 63
        '
        'lblTitleSagyoCompDate
        '
        Me.lblTitleSagyoCompDate.AutoSize = True
        Me.lblTitleSagyoCompDate.AutoSizeDef = True
        Me.lblTitleSagyoCompDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoCompDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoCompDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoCompDate.EnableStatus = False
        Me.lblTitleSagyoCompDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoCompDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoCompDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoCompDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoCompDate.HeightDef = 13
        Me.lblTitleSagyoCompDate.Location = New System.Drawing.Point(98, 80)
        Me.lblTitleSagyoCompDate.Name = "lblTitleSagyoCompDate"
        Me.lblTitleSagyoCompDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyoCompDate.TabIndex = 486
        Me.lblTitleSagyoCompDate.Text = "作業日"
        Me.lblTitleSagyoCompDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoCompDate.TextValue = "作業日"
        Me.lblTitleSagyoCompDate.WidthDef = 49
        '
        'imdSagyoCompDate
        '
        Me.imdSagyoCompDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSagyoCompDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSagyoCompDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSagyoCompDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSagyoCompDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSagyoCompDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSagyoCompDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSagyoCompDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSagyoCompDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSagyoCompDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSagyoCompDate.HeightDef = 18
        Me.imdSagyoCompDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSagyoCompDate.HissuLabelVisible = False
        Me.imdSagyoCompDate.Holiday = True
        Me.imdSagyoCompDate.IsAfterDateCheck = False
        Me.imdSagyoCompDate.IsBeforeDateCheck = False
        Me.imdSagyoCompDate.IsHissuCheck = False
        Me.imdSagyoCompDate.IsMinDateCheck = "1900/01/01"
        Me.imdSagyoCompDate.ItemName = ""
        Me.imdSagyoCompDate.Location = New System.Drawing.Point(150, 77)
        Me.imdSagyoCompDate.Name = "imdSagyoCompDate"
        Me.imdSagyoCompDate.Number = CType(10101000000, Long)
        Me.imdSagyoCompDate.ReadOnly = False
        Me.imdSagyoCompDate.Size = New System.Drawing.Size(118, 18)
        Me.imdSagyoCompDate.TabIndex = 487
        Me.imdSagyoCompDate.TabStopSetting = True
        Me.imdSagyoCompDate.TextValue = ""
        Me.imdSagyoCompDate.Value = New Date(CType(0, Long))
        Me.imdSagyoCompDate.WidthDef = 118
        '
        'txtSagyoCompNm
        '
        Me.txtSagyoCompNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCompNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCompNm.CountWrappedLine = False
        Me.txtSagyoCompNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCompNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCompNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCompNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCompNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCompNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCompNm.HeightDef = 18
        Me.txtSagyoCompNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompNm.HissuLabelVisible = False
        Me.txtSagyoCompNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoCompNm.IsByteCheck = 0
        Me.txtSagyoCompNm.IsCalendarCheck = False
        Me.txtSagyoCompNm.IsDakutenCheck = False
        Me.txtSagyoCompNm.IsEisuCheck = False
        Me.txtSagyoCompNm.IsForbiddenWordsCheck = False
        Me.txtSagyoCompNm.IsFullByteCheck = 0
        Me.txtSagyoCompNm.IsHankakuCheck = False
        Me.txtSagyoCompNm.IsHissuCheck = False
        Me.txtSagyoCompNm.IsKanaCheck = False
        Me.txtSagyoCompNm.IsMiddleSpace = False
        Me.txtSagyoCompNm.IsNumericCheck = False
        Me.txtSagyoCompNm.IsSujiCheck = False
        Me.txtSagyoCompNm.IsZenkakuCheck = False
        Me.txtSagyoCompNm.ItemName = ""
        Me.txtSagyoCompNm.LineSpace = 0
        Me.txtSagyoCompNm.Location = New System.Drawing.Point(637, 78)
        Me.txtSagyoCompNm.MaxLength = 0
        Me.txtSagyoCompNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCompNm.MaxLineCount = 0
        Me.txtSagyoCompNm.Multiline = False
        Me.txtSagyoCompNm.Name = "txtSagyoCompNm"
        Me.txtSagyoCompNm.ReadOnly = True
        Me.txtSagyoCompNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCompNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCompNm.Size = New System.Drawing.Size(177, 18)
        Me.txtSagyoCompNm.TabIndex = 490
        Me.txtSagyoCompNm.TabStop = False
        Me.txtSagyoCompNm.TabStopSetting = False
        Me.txtSagyoCompNm.TextValue = ""
        Me.txtSagyoCompNm.UseSystemPasswordChar = False
        Me.txtSagyoCompNm.WidthDef = 177
        Me.txtSagyoCompNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoCompCd
        '
        Me.txtSagyoCompCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoCompCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoCompCd.CountWrappedLine = False
        Me.txtSagyoCompCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoCompCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCompCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoCompCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCompCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoCompCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoCompCd.HeightDef = 18
        Me.txtSagyoCompCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoCompCd.HissuLabelVisible = False
        Me.txtSagyoCompCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtSagyoCompCd.IsByteCheck = 15
        Me.txtSagyoCompCd.IsCalendarCheck = False
        Me.txtSagyoCompCd.IsDakutenCheck = False
        Me.txtSagyoCompCd.IsEisuCheck = False
        Me.txtSagyoCompCd.IsForbiddenWordsCheck = False
        Me.txtSagyoCompCd.IsFullByteCheck = 0
        Me.txtSagyoCompCd.IsHankakuCheck = False
        Me.txtSagyoCompCd.IsHissuCheck = False
        Me.txtSagyoCompCd.IsKanaCheck = False
        Me.txtSagyoCompCd.IsMiddleSpace = False
        Me.txtSagyoCompCd.IsNumericCheck = False
        Me.txtSagyoCompCd.IsSujiCheck = False
        Me.txtSagyoCompCd.IsZenkakuCheck = False
        Me.txtSagyoCompCd.ItemName = ""
        Me.txtSagyoCompCd.LineSpace = 0
        Me.txtSagyoCompCd.Location = New System.Drawing.Point(536, 78)
        Me.txtSagyoCompCd.MaxLength = 15
        Me.txtSagyoCompCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoCompCd.MaxLineCount = 0
        Me.txtSagyoCompCd.Multiline = False
        Me.txtSagyoCompCd.Name = "txtSagyoCompCd"
        Me.txtSagyoCompCd.ReadOnly = True
        Me.txtSagyoCompCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoCompCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoCompCd.Size = New System.Drawing.Size(117, 18)
        Me.txtSagyoCompCd.TabIndex = 489
        Me.txtSagyoCompCd.TabStop = False
        Me.txtSagyoCompCd.TabStopSetting = False
        Me.txtSagyoCompCd.TextValue = ""
        Me.txtSagyoCompCd.UseSystemPasswordChar = False
        Me.txtSagyoCompCd.WidthDef = 117
        Me.txtSagyoCompCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyCompCd
        '
        Me.lblTitleSagyCompCd.AutoSize = True
        Me.lblTitleSagyCompCd.AutoSizeDef = True
        Me.lblTitleSagyCompCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyCompCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyCompCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyCompCd.EnableStatus = False
        Me.lblTitleSagyCompCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyCompCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyCompCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyCompCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyCompCd.HeightDef = 13
        Me.lblTitleSagyCompCd.Location = New System.Drawing.Point(456, 81)
        Me.lblTitleSagyCompCd.Name = "lblTitleSagyCompCd"
        Me.lblTitleSagyCompCd.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSagyCompCd.TabIndex = 488
        Me.lblTitleSagyCompCd.Text = "作業確認者"
        Me.lblTitleSagyCompCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyCompCd.TextValue = "作業確認者"
        Me.lblTitleSagyCompCd.WidthDef = 77
        '
        'txtRemarkZai
        '
        Me.txtRemarkZai.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemarkZai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemarkZai.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarkZai.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemarkZai.CountWrappedLine = False
        Me.txtRemarkZai.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemarkZai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemarkZai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemarkZai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemarkZai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemarkZai.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemarkZai.HeightDef = 18
        Me.txtRemarkZai.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemarkZai.HissuLabelVisible = False
        Me.txtRemarkZai.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemarkZai.IsByteCheck = 100
        Me.txtRemarkZai.IsCalendarCheck = False
        Me.txtRemarkZai.IsDakutenCheck = False
        Me.txtRemarkZai.IsEisuCheck = False
        Me.txtRemarkZai.IsForbiddenWordsCheck = False
        Me.txtRemarkZai.IsFullByteCheck = 0
        Me.txtRemarkZai.IsHankakuCheck = False
        Me.txtRemarkZai.IsHissuCheck = False
        Me.txtRemarkZai.IsKanaCheck = False
        Me.txtRemarkZai.IsMiddleSpace = False
        Me.txtRemarkZai.IsNumericCheck = False
        Me.txtRemarkZai.IsSujiCheck = False
        Me.txtRemarkZai.IsZenkakuCheck = False
        Me.txtRemarkZai.ItemName = ""
        Me.txtRemarkZai.LineSpace = 0
        Me.txtRemarkZai.Location = New System.Drawing.Point(150, 285)
        Me.txtRemarkZai.MaxLength = 100
        Me.txtRemarkZai.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemarkZai.MaxLineCount = 0
        Me.txtRemarkZai.Multiline = False
        Me.txtRemarkZai.Name = "txtRemarkZai"
        Me.txtRemarkZai.ReadOnly = False
        Me.txtRemarkZai.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemarkZai.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemarkZai.Size = New System.Drawing.Size(640, 18)
        Me.txtRemarkZai.TabIndex = 492
        Me.txtRemarkZai.TabStopSetting = True
        Me.txtRemarkZai.TextValue = ""
        Me.txtRemarkZai.UseSystemPasswordChar = False
        Me.txtRemarkZai.WidthDef = 640
        Me.txtRemarkZai.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRemarkZai
        '
        Me.lblTitleRemarkZai.AutoSize = True
        Me.lblTitleRemarkZai.AutoSizeDef = True
        Me.lblTitleRemarkZai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemarkZai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemarkZai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRemarkZai.EnableStatus = False
        Me.lblTitleRemarkZai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemarkZai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemarkZai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemarkZai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemarkZai.HeightDef = 13
        Me.lblTitleRemarkZai.Location = New System.Drawing.Point(70, 288)
        Me.lblTitleRemarkZai.Name = "lblTitleRemarkZai"
        Me.lblTitleRemarkZai.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleRemarkZai.TabIndex = 491
        Me.lblTitleRemarkZai.Text = "在庫用備考"
        Me.lblTitleRemarkZai.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRemarkZai.TextValue = "在庫用備考"
        Me.lblTitleRemarkZai.WidthDef = 77
        '
        'txtRemarkSkyu
        '
        Me.txtRemarkSkyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemarkSkyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemarkSkyu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarkSkyu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemarkSkyu.CountWrappedLine = False
        Me.txtRemarkSkyu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemarkSkyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemarkSkyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemarkSkyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemarkSkyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemarkSkyu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemarkSkyu.HeightDef = 18
        Me.txtRemarkSkyu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemarkSkyu.HissuLabelVisible = False
        Me.txtRemarkSkyu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemarkSkyu.IsByteCheck = 100
        Me.txtRemarkSkyu.IsCalendarCheck = False
        Me.txtRemarkSkyu.IsDakutenCheck = False
        Me.txtRemarkSkyu.IsEisuCheck = False
        Me.txtRemarkSkyu.IsForbiddenWordsCheck = False
        Me.txtRemarkSkyu.IsFullByteCheck = 0
        Me.txtRemarkSkyu.IsHankakuCheck = False
        Me.txtRemarkSkyu.IsHissuCheck = False
        Me.txtRemarkSkyu.IsKanaCheck = False
        Me.txtRemarkSkyu.IsMiddleSpace = False
        Me.txtRemarkSkyu.IsNumericCheck = False
        Me.txtRemarkSkyu.IsSujiCheck = False
        Me.txtRemarkSkyu.IsZenkakuCheck = False
        Me.txtRemarkSkyu.ItemName = ""
        Me.txtRemarkSkyu.LineSpace = 0
        Me.txtRemarkSkyu.Location = New System.Drawing.Point(150, 308)
        Me.txtRemarkSkyu.MaxLength = 100
        Me.txtRemarkSkyu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemarkSkyu.MaxLineCount = 0
        Me.txtRemarkSkyu.Multiline = False
        Me.txtRemarkSkyu.Name = "txtRemarkSkyu"
        Me.txtRemarkSkyu.ReadOnly = False
        Me.txtRemarkSkyu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemarkSkyu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemarkSkyu.Size = New System.Drawing.Size(640, 18)
        Me.txtRemarkSkyu.TabIndex = 494
        Me.txtRemarkSkyu.TabStopSetting = True
        Me.txtRemarkSkyu.TextValue = ""
        Me.txtRemarkSkyu.UseSystemPasswordChar = False
        Me.txtRemarkSkyu.WidthDef = 640
        Me.txtRemarkSkyu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRemarkSkyu
        '
        Me.lblTitleRemarkSkyu.AutoSize = True
        Me.lblTitleRemarkSkyu.AutoSizeDef = True
        Me.lblTitleRemarkSkyu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemarkSkyu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemarkSkyu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRemarkSkyu.EnableStatus = False
        Me.lblTitleRemarkSkyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemarkSkyu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemarkSkyu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemarkSkyu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemarkSkyu.HeightDef = 13
        Me.lblTitleRemarkSkyu.Location = New System.Drawing.Point(70, 311)
        Me.lblTitleRemarkSkyu.Name = "lblTitleRemarkSkyu"
        Me.lblTitleRemarkSkyu.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleRemarkSkyu.TabIndex = 493
        Me.lblTitleRemarkSkyu.Text = "請求用備考"
        Me.lblTitleRemarkSkyu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRemarkSkyu.TextValue = "請求用備考"
        Me.lblTitleRemarkSkyu.WidthDef = 77
        '
        'txtGoodsCdKey
        '
        Me.txtGoodsCdKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdKey.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdKey.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdKey.CountWrappedLine = False
        Me.txtGoodsCdKey.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdKey.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdKey.HeightDef = 18
        Me.txtGoodsCdKey.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdKey.HissuLabelVisible = False
        Me.txtGoodsCdKey.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCdKey.IsByteCheck = 100
        Me.txtGoodsCdKey.IsCalendarCheck = False
        Me.txtGoodsCdKey.IsDakutenCheck = False
        Me.txtGoodsCdKey.IsEisuCheck = False
        Me.txtGoodsCdKey.IsForbiddenWordsCheck = False
        Me.txtGoodsCdKey.IsFullByteCheck = 0
        Me.txtGoodsCdKey.IsHankakuCheck = False
        Me.txtGoodsCdKey.IsHissuCheck = False
        Me.txtGoodsCdKey.IsKanaCheck = False
        Me.txtGoodsCdKey.IsMiddleSpace = False
        Me.txtGoodsCdKey.IsNumericCheck = False
        Me.txtGoodsCdKey.IsSujiCheck = False
        Me.txtGoodsCdKey.IsZenkakuCheck = False
        Me.txtGoodsCdKey.ItemName = ""
        Me.txtGoodsCdKey.LineSpace = 0
        Me.txtGoodsCdKey.Location = New System.Drawing.Point(384, 365)
        Me.txtGoodsCdKey.MaxLength = 100
        Me.txtGoodsCdKey.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdKey.MaxLineCount = 0
        Me.txtGoodsCdKey.Multiline = False
        Me.txtGoodsCdKey.Name = "txtGoodsCdKey"
        Me.txtGoodsCdKey.ReadOnly = True
        Me.txtGoodsCdKey.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdKey.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdKey.Size = New System.Drawing.Size(98, 18)
        Me.txtGoodsCdKey.TabIndex = 497
        Me.txtGoodsCdKey.TabStop = False
        Me.txtGoodsCdKey.TabStopSetting = False
        Me.txtGoodsCdKey.TextValue = ""
        Me.txtGoodsCdKey.UseSystemPasswordChar = False
        Me.txtGoodsCdKey.Visible = False
        Me.txtGoodsCdKey.WidthDef = 98
        Me.txtGoodsCdKey.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUpdDate
        '
        Me.txtUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUpdDate.CountWrappedLine = False
        Me.txtUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUpdDate.HeightDef = 18
        Me.txtUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdDate.HissuLabelVisible = False
        Me.txtUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtUpdDate.IsByteCheck = 100
        Me.txtUpdDate.IsCalendarCheck = False
        Me.txtUpdDate.IsDakutenCheck = False
        Me.txtUpdDate.IsEisuCheck = False
        Me.txtUpdDate.IsForbiddenWordsCheck = False
        Me.txtUpdDate.IsFullByteCheck = 0
        Me.txtUpdDate.IsHankakuCheck = False
        Me.txtUpdDate.IsHissuCheck = False
        Me.txtUpdDate.IsKanaCheck = False
        Me.txtUpdDate.IsMiddleSpace = False
        Me.txtUpdDate.IsNumericCheck = False
        Me.txtUpdDate.IsSujiCheck = False
        Me.txtUpdDate.IsZenkakuCheck = False
        Me.txtUpdDate.ItemName = ""
        Me.txtUpdDate.LineSpace = 0
        Me.txtUpdDate.Location = New System.Drawing.Point(488, 365)
        Me.txtUpdDate.MaxLength = 100
        Me.txtUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUpdDate.MaxLineCount = 0
        Me.txtUpdDate.Multiline = False
        Me.txtUpdDate.Name = "txtUpdDate"
        Me.txtUpdDate.ReadOnly = True
        Me.txtUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUpdDate.Size = New System.Drawing.Size(98, 18)
        Me.txtUpdDate.TabIndex = 498
        Me.txtUpdDate.TabStop = False
        Me.txtUpdDate.TabStopSetting = False
        Me.txtUpdDate.TextValue = ""
        Me.txtUpdDate.UseSystemPasswordChar = False
        Me.txtUpdDate.Visible = False
        Me.txtUpdDate.WidthDef = 98
        Me.txtUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtUpdTime
        '
        Me.txtUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtUpdTime.CountWrappedLine = False
        Me.txtUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtUpdTime.HeightDef = 18
        Me.txtUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtUpdTime.HissuLabelVisible = False
        Me.txtUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtUpdTime.IsByteCheck = 100
        Me.txtUpdTime.IsCalendarCheck = False
        Me.txtUpdTime.IsDakutenCheck = False
        Me.txtUpdTime.IsEisuCheck = False
        Me.txtUpdTime.IsForbiddenWordsCheck = False
        Me.txtUpdTime.IsFullByteCheck = 0
        Me.txtUpdTime.IsHankakuCheck = False
        Me.txtUpdTime.IsHissuCheck = False
        Me.txtUpdTime.IsKanaCheck = False
        Me.txtUpdTime.IsMiddleSpace = False
        Me.txtUpdTime.IsNumericCheck = False
        Me.txtUpdTime.IsSujiCheck = False
        Me.txtUpdTime.IsZenkakuCheck = False
        Me.txtUpdTime.ItemName = ""
        Me.txtUpdTime.LineSpace = 0
        Me.txtUpdTime.Location = New System.Drawing.Point(592, 365)
        Me.txtUpdTime.MaxLength = 100
        Me.txtUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtUpdTime.MaxLineCount = 0
        Me.txtUpdTime.Multiline = False
        Me.txtUpdTime.Name = "txtUpdTime"
        Me.txtUpdTime.ReadOnly = True
        Me.txtUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUpdTime.Size = New System.Drawing.Size(98, 18)
        Me.txtUpdTime.TabIndex = 499
        Me.txtUpdTime.TabStop = False
        Me.txtUpdTime.TabStopSetting = False
        Me.txtUpdTime.TextValue = ""
        Me.txtUpdTime.UseSystemPasswordChar = False
        Me.txtUpdTime.Visible = False
        Me.txtUpdTime.WidthDef = 98
        Me.txtUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
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
        Me.txtCustCdM.Location = New System.Drawing.Point(231, 147)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 500
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtGoodsCdCustHide
        '
        Me.txtGoodsCdCustHide.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCustHide.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCustHide.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdCustHide.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdCustHide.CountWrappedLine = False
        Me.txtGoodsCdCustHide.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdCustHide.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCustHide.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdCustHide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCustHide.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdCustHide.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdCustHide.HeightDef = 18
        Me.txtGoodsCdCustHide.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdCustHide.HissuLabelVisible = False
        Me.txtGoodsCdCustHide.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCdCustHide.IsByteCheck = 100
        Me.txtGoodsCdCustHide.IsCalendarCheck = False
        Me.txtGoodsCdCustHide.IsDakutenCheck = False
        Me.txtGoodsCdCustHide.IsEisuCheck = False
        Me.txtGoodsCdCustHide.IsForbiddenWordsCheck = False
        Me.txtGoodsCdCustHide.IsFullByteCheck = 0
        Me.txtGoodsCdCustHide.IsHankakuCheck = False
        Me.txtGoodsCdCustHide.IsHissuCheck = False
        Me.txtGoodsCdCustHide.IsKanaCheck = False
        Me.txtGoodsCdCustHide.IsMiddleSpace = False
        Me.txtGoodsCdCustHide.IsNumericCheck = False
        Me.txtGoodsCdCustHide.IsSujiCheck = False
        Me.txtGoodsCdCustHide.IsZenkakuCheck = False
        Me.txtGoodsCdCustHide.ItemName = ""
        Me.txtGoodsCdCustHide.LineSpace = 0
        Me.txtGoodsCdCustHide.Location = New System.Drawing.Point(277, 365)
        Me.txtGoodsCdCustHide.MaxLength = 100
        Me.txtGoodsCdCustHide.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdCustHide.MaxLineCount = 0
        Me.txtGoodsCdCustHide.Multiline = False
        Me.txtGoodsCdCustHide.Name = "txtGoodsCdCustHide"
        Me.txtGoodsCdCustHide.ReadOnly = True
        Me.txtGoodsCdCustHide.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdCustHide.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdCustHide.Size = New System.Drawing.Size(98, 18)
        Me.txtGoodsCdCustHide.TabIndex = 501
        Me.txtGoodsCdCustHide.TabStop = False
        Me.txtGoodsCdCustHide.TabStopSetting = False
        Me.txtGoodsCdCustHide.TextValue = ""
        Me.txtGoodsCdCustHide.UseSystemPasswordChar = False
        Me.txtGoodsCdCustHide.Visible = False
        Me.txtGoodsCdCustHide.WidthDef = 98
        Me.txtGoodsCdCustHide.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbSagyoCompKb
        '
        Me.cmbSagyoCompKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoCompKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoCompKb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSagyoCompKb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSagyoCompKb.DataCode = "S052"
        Me.cmbSagyoCompKb.DataSource = Nothing
        Me.cmbSagyoCompKb.DisplayMember = Nothing
        Me.cmbSagyoCompKb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSagyoCompKb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSagyoCompKb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSagyoCompKb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSagyoCompKb.HeightDef = 18
        Me.cmbSagyoCompKb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoCompKb.HissuLabelVisible = False
        Me.cmbSagyoCompKb.InsertWildCard = True
        Me.cmbSagyoCompKb.IsForbiddenWordsCheck = False
        Me.cmbSagyoCompKb.IsHissuCheck = False
        Me.cmbSagyoCompKb.ItemName = ""
        Me.cmbSagyoCompKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSagyoCompKb.Location = New System.Drawing.Point(536, 55)
        Me.cmbSagyoCompKb.Name = "cmbSagyoCompKb"
        Me.cmbSagyoCompKb.ReadOnly = True
        Me.cmbSagyoCompKb.SelectedIndex = -1
        Me.cmbSagyoCompKb.SelectedItem = Nothing
        Me.cmbSagyoCompKb.SelectedText = ""
        Me.cmbSagyoCompKb.SelectedValue = ""
        Me.cmbSagyoCompKb.Size = New System.Drawing.Size(117, 18)
        Me.cmbSagyoCompKb.TabIndex = 502
        Me.cmbSagyoCompKb.TabStop = False
        Me.cmbSagyoCompKb.TabStopSetting = False
        Me.cmbSagyoCompKb.TextValue = ""
        Me.cmbSagyoCompKb.Value1 = Nothing
        Me.cmbSagyoCompKb.Value2 = Nothing
        Me.cmbSagyoCompKb.Value3 = Nothing
        Me.cmbSagyoCompKb.ValueMember = Nothing
        Me.cmbSagyoCompKb.WidthDef = 117
        '
        'cmbSkyuChk
        '
        Me.cmbSkyuChk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuChk.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuChk.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSkyuChk.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSkyuChk.DataCode = "S053"
        Me.cmbSkyuChk.DataSource = Nothing
        Me.cmbSkyuChk.DisplayMember = Nothing
        Me.cmbSkyuChk.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSkyuChk.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSkyuChk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSkyuChk.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSkyuChk.HeightDef = 18
        Me.cmbSkyuChk.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSkyuChk.HissuLabelVisible = False
        Me.cmbSkyuChk.InsertWildCard = True
        Me.cmbSkyuChk.IsForbiddenWordsCheck = False
        Me.cmbSkyuChk.IsHissuCheck = False
        Me.cmbSkyuChk.ItemName = ""
        Me.cmbSkyuChk.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSkyuChk.Location = New System.Drawing.Point(923, 59)
        Me.cmbSkyuChk.Name = "cmbSkyuChk"
        Me.cmbSkyuChk.ReadOnly = True
        Me.cmbSkyuChk.SelectedIndex = -1
        Me.cmbSkyuChk.SelectedItem = Nothing
        Me.cmbSkyuChk.SelectedText = ""
        Me.cmbSkyuChk.SelectedValue = ""
        Me.cmbSkyuChk.Size = New System.Drawing.Size(117, 18)
        Me.cmbSkyuChk.TabIndex = 503
        Me.cmbSkyuChk.TabStop = False
        Me.cmbSkyuChk.TabStopSetting = False
        Me.cmbSkyuChk.TextValue = ""
        Me.cmbSkyuChk.Value1 = Nothing
        Me.cmbSkyuChk.Value2 = Nothing
        Me.cmbSkyuChk.Value3 = Nothing
        Me.cmbSkyuChk.ValueMember = Nothing
        Me.cmbSkyuChk.WidthDef = 117
        '
        'lblSagyoUpCurr
        '
        Me.lblSagyoUpCurr.AutoSize = True
        Me.lblSagyoUpCurr.AutoSizeDef = True
        Me.lblSagyoUpCurr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoUpCurr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoUpCurr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoUpCurr.EnableStatus = False
        Me.lblSagyoUpCurr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoUpCurr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoUpCurr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoUpCurr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoUpCurr.HeightDef = 13
        Me.lblSagyoUpCurr.Location = New System.Drawing.Point(495, 242)
        Me.lblSagyoUpCurr.Name = "lblSagyoUpCurr"
        Me.lblSagyoUpCurr.Size = New System.Drawing.Size(28, 13)
        Me.lblSagyoUpCurr.TabIndex = 504
        Me.lblSagyoUpCurr.Text = "XXX"
        Me.lblSagyoUpCurr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSagyoUpCurr.TextValue = "XXX"
        Me.lblSagyoUpCurr.WidthDef = 28
        '
        'lblSagyoGkCurr
        '
        Me.lblSagyoGkCurr.AutoSize = True
        Me.lblSagyoGkCurr.AutoSizeDef = True
        Me.lblSagyoGkCurr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoGkCurr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoGkCurr.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoGkCurr.EnableStatus = False
        Me.lblSagyoGkCurr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoGkCurr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoGkCurr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoGkCurr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoGkCurr.HeightDef = 13
        Me.lblSagyoGkCurr.Location = New System.Drawing.Point(746, 242)
        Me.lblSagyoGkCurr.Name = "lblSagyoGkCurr"
        Me.lblSagyoGkCurr.Size = New System.Drawing.Size(28, 13)
        Me.lblSagyoGkCurr.TabIndex = 505
        Me.lblSagyoGkCurr.Text = "XXX"
        Me.lblSagyoGkCurr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSagyoGkCurr.TextValue = "XXX"
        Me.lblSagyoGkCurr.WidthDef = 28
        '
        'LME020F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LME020F"
        Me.Text = "【LME020】   作業料明細編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents lblTitleSoko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSkyuChk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyoComp As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyoRecNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoRecNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents lblTitleSagyoSijiNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoSijiNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleInOutkaNoLM As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtInOutkaNoLM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbIozsKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleIozsKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyoCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleDestCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDestNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtDestCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCdCust As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleLotNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtLotNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeiqtoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeiqto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbTaxKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleTaxKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSagyoNb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSagyoNb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSagyoGk As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSagyoGk As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numSagyoUp As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblTitleSagyoUp As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbInvTani As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleInvTani As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyoCompDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSagyoCompDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents txtRemarkSkyu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRemarkSkyu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemarkZai As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRemarkZai As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoCompNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoCompCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyCompCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtGoodsCdKey As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUpdTime As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtUpdDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtGoodsCdCustHide As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbSagyoCompKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbSkyuChk As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSagyoGkCurr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSagyoUpCurr As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
