<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LME040F
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
        Dim sprDetails_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetails_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.grpSagyoSiji = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.cmbSagyoSijiStatus = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblSagyoStatus = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbWHSagyoSintyoku = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblWHSagyoSintyoku = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyoSijiNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbIozsKb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.lblTitleSagyoSijiNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleIozsKb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemark3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSokoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRemark3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSokoCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemark2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyoSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleRemark2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOyaSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtRemark1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleRemark1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleOyaSeiqtoCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdSagyoDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.sprDetails = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblTitlePrint = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.btnRowAdd = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.btnRowDel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.grpSagyo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtSagyoRemark5 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoRemark4 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoRemark3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoRemark2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyoRemark1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyo1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyo2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyo3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyo4 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSagyo5 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSagyo5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyo1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSagyo3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyo2 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleSagyo2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtSagyo3 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyo4 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSagyo5 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.grpZaiko = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblTitleKeyNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtKeyNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.grpZaikoMeisai = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.lblLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleLotNo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTou = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSitu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblZone = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblLoca = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblTitleGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleLoca = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleZone = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleSitu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.lblTitleTou = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnJikkou = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbJikkou = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        sprDetails_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetails_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetails_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.grpSagyoSiji.SuspendLayout()
        CType(Me.sprDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSagyo.SuspendLayout()
        Me.grpZaikoMeisai.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.btnJikkou)
        Me.pnlViewAria.Controls.Add(Me.cmbJikkou)
        Me.pnlViewAria.Controls.Add(Me.grpZaikoMeisai)
        Me.pnlViewAria.Controls.Add(Me.lblTitleKeyNo)
        Me.pnlViewAria.Controls.Add(Me.txtKeyNo)
        Me.pnlViewAria.Controls.Add(Me.grpSagyo)
        Me.pnlViewAria.Controls.Add(Me.btnRowDel)
        Me.pnlViewAria.Controls.Add(Me.btnRowAdd)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.lblTitlePrint)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.sprDetails)
        Me.pnlViewAria.Controls.Add(Me.grpSagyoSiji)
        Me.pnlViewAria.Controls.Add(Me.grpZaiko)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'grpSagyoSiji
        '
        Me.grpSagyoSiji.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSagyoSiji.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSagyoSiji.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSagyoSiji.Controls.Add(Me.cmbSagyoSijiStatus)
        Me.grpSagyoSiji.Controls.Add(Me.lblSagyoStatus)
        Me.grpSagyoSiji.Controls.Add(Me.cmbWHSagyoSintyoku)
        Me.grpSagyoSiji.Controls.Add(Me.lblWHSagyoSintyoku)
        Me.grpSagyoSiji.Controls.Add(Me.txtSagyoSijiNo)
        Me.grpSagyoSiji.Controls.Add(Me.cmbIozsKb)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleSagyoSijiNo)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleIozsKb)
        Me.grpSagyoSiji.Controls.Add(Me.txtRemark3)
        Me.grpSagyoSiji.Controls.Add(Me.txtSokoCd)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleRemark3)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleSokoCd)
        Me.grpSagyoSiji.Controls.Add(Me.txtRemark2)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleSagyoSeiqtoCd)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleRemark2)
        Me.grpSagyoSiji.Controls.Add(Me.txtOyaSeiqtoCd)
        Me.grpSagyoSiji.Controls.Add(Me.txtRemark1)
        Me.grpSagyoSiji.Controls.Add(Me.txtSagyoSeiqtoCd)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleRemark1)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleOyaSeiqtoCd)
        Me.grpSagyoSiji.Controls.Add(Me.lblCustNmM)
        Me.grpSagyoSiji.Controls.Add(Me.lblCustNmL)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleCust)
        Me.grpSagyoSiji.Controls.Add(Me.txtCustCdM)
        Me.grpSagyoSiji.Controls.Add(Me.txtCustCdL)
        Me.grpSagyoSiji.Controls.Add(Me.cmbEigyo)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleEigyo)
        Me.grpSagyoSiji.Controls.Add(Me.lblTitleDate)
        Me.grpSagyoSiji.Controls.Add(Me.imdSagyoDate)
        Me.grpSagyoSiji.EnableStatus = False
        Me.grpSagyoSiji.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSagyoSiji.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSagyoSiji.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSagyoSiji.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSagyoSiji.HeightDef = 176
        Me.grpSagyoSiji.Location = New System.Drawing.Point(3, 30)
        Me.grpSagyoSiji.Name = "grpSagyoSiji"
        Me.grpSagyoSiji.Size = New System.Drawing.Size(1259, 176)
        Me.grpSagyoSiji.TabIndex = 238
        Me.grpSagyoSiji.TabStop = False
        Me.grpSagyoSiji.Text = "作業指示書情報"
        Me.grpSagyoSiji.TextValue = "作業指示書情報"
        Me.grpSagyoSiji.WidthDef = 1259
        '
        'cmbSagyoSijiStatus
        '
        Me.cmbSagyoSijiStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoSijiStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoSijiStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSagyoSijiStatus.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSagyoSijiStatus.DataCode = "S122"
        Me.cmbSagyoSijiStatus.DataSource = Nothing
        Me.cmbSagyoSijiStatus.DisplayMember = Nothing
        Me.cmbSagyoSijiStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSagyoSijiStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSagyoSijiStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSagyoSijiStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSagyoSijiStatus.HeightDef = 18
        Me.cmbSagyoSijiStatus.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSagyoSijiStatus.HissuLabelVisible = False
        Me.cmbSagyoSijiStatus.InsertWildCard = True
        Me.cmbSagyoSijiStatus.IsForbiddenWordsCheck = False
        Me.cmbSagyoSijiStatus.IsHissuCheck = False
        Me.cmbSagyoSijiStatus.ItemName = ""
        Me.cmbSagyoSijiStatus.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSagyoSijiStatus.Location = New System.Drawing.Point(747, 62)
        Me.cmbSagyoSijiStatus.Name = "cmbSagyoSijiStatus"
        Me.cmbSagyoSijiStatus.ReadOnly = True
        Me.cmbSagyoSijiStatus.SelectedIndex = -1
        Me.cmbSagyoSijiStatus.SelectedItem = Nothing
        Me.cmbSagyoSijiStatus.SelectedText = ""
        Me.cmbSagyoSijiStatus.SelectedValue = ""
        Me.cmbSagyoSijiStatus.Size = New System.Drawing.Size(100, 18)
        Me.cmbSagyoSijiStatus.TabIndex = 513
        Me.cmbSagyoSijiStatus.TabStop = False
        Me.cmbSagyoSijiStatus.TabStopSetting = False
        Me.cmbSagyoSijiStatus.TextValue = ""
        Me.cmbSagyoSijiStatus.Value1 = Nothing
        Me.cmbSagyoSijiStatus.Value2 = Nothing
        Me.cmbSagyoSijiStatus.Value3 = Nothing
        Me.cmbSagyoSijiStatus.ValueMember = Nothing
        Me.cmbSagyoSijiStatus.WidthDef = 100
        '
        'lblSagyoStatus
        '
        Me.lblSagyoStatus.AutoSize = True
        Me.lblSagyoStatus.AutoSizeDef = True
        Me.lblSagyoStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyoStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblSagyoStatus.EnableStatus = False
        Me.lblSagyoStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyoStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyoStatus.HeightDef = 13
        Me.lblSagyoStatus.Location = New System.Drawing.Point(678, 64)
        Me.lblSagyoStatus.Name = "lblSagyoStatus"
        Me.lblSagyoStatus.Size = New System.Drawing.Size(63, 13)
        Me.lblSagyoStatus.TabIndex = 512
        Me.lblSagyoStatus.Text = "作業進捗"
        Me.lblSagyoStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblSagyoStatus.TextValue = "作業進捗"
        Me.lblSagyoStatus.WidthDef = 63
        '
        'cmbWHSagyoSintyoku
        '
        Me.cmbWHSagyoSintyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWHSagyoSintyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWHSagyoSintyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbWHSagyoSintyoku.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbWHSagyoSintyoku.DataCode = "S118"
        Me.cmbWHSagyoSintyoku.DataSource = Nothing
        Me.cmbWHSagyoSintyoku.DisplayMember = Nothing
        Me.cmbWHSagyoSintyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWHSagyoSintyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWHSagyoSintyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWHSagyoSintyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWHSagyoSintyoku.HeightDef = 18
        Me.cmbWHSagyoSintyoku.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWHSagyoSintyoku.HissuLabelVisible = False
        Me.cmbWHSagyoSintyoku.InsertWildCard = True
        Me.cmbWHSagyoSintyoku.IsForbiddenWordsCheck = False
        Me.cmbWHSagyoSintyoku.IsHissuCheck = False
        Me.cmbWHSagyoSintyoku.ItemName = ""
        Me.cmbWHSagyoSintyoku.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbWHSagyoSintyoku.Location = New System.Drawing.Point(747, 83)
        Me.cmbWHSagyoSintyoku.Name = "cmbWHSagyoSintyoku"
        Me.cmbWHSagyoSintyoku.ReadOnly = True
        Me.cmbWHSagyoSintyoku.SelectedIndex = -1
        Me.cmbWHSagyoSintyoku.SelectedItem = Nothing
        Me.cmbWHSagyoSintyoku.SelectedText = ""
        Me.cmbWHSagyoSintyoku.SelectedValue = ""
        Me.cmbWHSagyoSintyoku.Size = New System.Drawing.Size(100, 18)
        Me.cmbWHSagyoSintyoku.TabIndex = 511
        Me.cmbWHSagyoSintyoku.TabStop = False
        Me.cmbWHSagyoSintyoku.TabStopSetting = False
        Me.cmbWHSagyoSintyoku.TextValue = ""
        Me.cmbWHSagyoSintyoku.Value1 = Nothing
        Me.cmbWHSagyoSintyoku.Value2 = Nothing
        Me.cmbWHSagyoSintyoku.Value3 = Nothing
        Me.cmbWHSagyoSintyoku.ValueMember = Nothing
        Me.cmbWHSagyoSintyoku.WidthDef = 100
        '
        'lblWHSagyoSintyoku
        '
        Me.lblWHSagyoSintyoku.AutoSize = True
        Me.lblWHSagyoSintyoku.AutoSizeDef = True
        Me.lblWHSagyoSintyoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWHSagyoSintyoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblWHSagyoSintyoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblWHSagyoSintyoku.EnableStatus = False
        Me.lblWHSagyoSintyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWHSagyoSintyoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWHSagyoSintyoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWHSagyoSintyoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWHSagyoSintyoku.HeightDef = 13
        Me.lblWHSagyoSintyoku.Location = New System.Drawing.Point(650, 86)
        Me.lblWHSagyoSintyoku.Name = "lblWHSagyoSintyoku"
        Me.lblWHSagyoSintyoku.Size = New System.Drawing.Size(91, 13)
        Me.lblWHSagyoSintyoku.TabIndex = 510
        Me.lblWHSagyoSintyoku.Text = "現場作業指示"
        Me.lblWHSagyoSintyoku.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblWHSagyoSintyoku.TextValue = "現場作業指示"
        Me.lblWHSagyoSintyoku.WidthDef = 91
        '
        'txtSagyoSijiNo
        '
        Me.txtSagyoSijiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoSijiNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.txtSagyoSijiNo.IsByteCheck = 100
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
        Me.txtSagyoSijiNo.Location = New System.Drawing.Point(549, 18)
        Me.txtSagyoSijiNo.MaxLength = 100
        Me.txtSagyoSijiNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoSijiNo.MaxLineCount = 0
        Me.txtSagyoSijiNo.Multiline = False
        Me.txtSagyoSijiNo.Name = "txtSagyoSijiNo"
        Me.txtSagyoSijiNo.ReadOnly = False
        Me.txtSagyoSijiNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoSijiNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoSijiNo.Size = New System.Drawing.Size(95, 18)
        Me.txtSagyoSijiNo.TabIndex = 508
        Me.txtSagyoSijiNo.TabStopSetting = True
        Me.txtSagyoSijiNo.TextValue = ""
        Me.txtSagyoSijiNo.UseSystemPasswordChar = False
        Me.txtSagyoSijiNo.WidthDef = 95
        Me.txtSagyoSijiNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbIozsKb
        '
        Me.cmbIozsKb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbIozsKb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.cmbIozsKb.HissuLabelVisible = False
        Me.cmbIozsKb.InsertWildCard = True
        Me.cmbIozsKb.IsForbiddenWordsCheck = False
        Me.cmbIozsKb.IsHissuCheck = False
        Me.cmbIozsKb.ItemName = ""
        Me.cmbIozsKb.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbIozsKb.Location = New System.Drawing.Point(527, 83)
        Me.cmbIozsKb.Name = "cmbIozsKb"
        Me.cmbIozsKb.ReadOnly = True
        Me.cmbIozsKb.SelectedIndex = -1
        Me.cmbIozsKb.SelectedItem = Nothing
        Me.cmbIozsKb.SelectedText = ""
        Me.cmbIozsKb.SelectedValue = ""
        Me.cmbIozsKb.Size = New System.Drawing.Size(117, 18)
        Me.cmbIozsKb.TabIndex = 499
        Me.cmbIozsKb.TabStop = False
        Me.cmbIozsKb.TabStopSetting = False
        Me.cmbIozsKb.TextValue = ""
        Me.cmbIozsKb.Value1 = Nothing
        Me.cmbIozsKb.Value2 = Nothing
        Me.cmbIozsKb.Value3 = Nothing
        Me.cmbIozsKb.ValueMember = Nothing
        Me.cmbIozsKb.WidthDef = 117
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
        Me.lblTitleSagyoSijiNo.Location = New System.Drawing.Point(441, 20)
        Me.lblTitleSagyoSijiNo.Name = "lblTitleSagyoSijiNo"
        Me.lblTitleSagyoSijiNo.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleSagyoSijiNo.TabIndex = 507
        Me.lblTitleSagyoSijiNo.Text = "作業指示書番号"
        Me.lblTitleSagyoSijiNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoSijiNo.TextValue = "作業指示書番号"
        Me.lblTitleSagyoSijiNo.WidthDef = 105
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
        Me.lblTitleIozsKb.Location = New System.Drawing.Point(405, 86)
        Me.lblTitleIozsKb.Name = "lblTitleIozsKb"
        Me.lblTitleIozsKb.Size = New System.Drawing.Size(119, 13)
        Me.lblTitleIozsKb.TabIndex = 500
        Me.lblTitleIozsKb.Text = "入出在その他区分"
        Me.lblTitleIozsKb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleIozsKb.TextValue = "入出在その他区分"
        Me.lblTitleIozsKb.WidthDef = 119
        '
        'txtRemark3
        '
        Me.txtRemark3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemark3.CountWrappedLine = False
        Me.txtRemark3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark3.HeightDef = 18
        Me.txtRemark3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark3.HissuLabelVisible = False
        Me.txtRemark3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark3.IsByteCheck = 100
        Me.txtRemark3.IsCalendarCheck = False
        Me.txtRemark3.IsDakutenCheck = False
        Me.txtRemark3.IsEisuCheck = False
        Me.txtRemark3.IsForbiddenWordsCheck = False
        Me.txtRemark3.IsFullByteCheck = 0
        Me.txtRemark3.IsHankakuCheck = False
        Me.txtRemark3.IsHissuCheck = False
        Me.txtRemark3.IsKanaCheck = False
        Me.txtRemark3.IsMiddleSpace = False
        Me.txtRemark3.IsNumericCheck = False
        Me.txtRemark3.IsSujiCheck = False
        Me.txtRemark3.IsZenkakuCheck = False
        Me.txtRemark3.ItemName = ""
        Me.txtRemark3.LineSpace = 0
        Me.txtRemark3.Location = New System.Drawing.Point(90, 146)
        Me.txtRemark3.MaxLength = 100
        Me.txtRemark3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark3.MaxLineCount = 0
        Me.txtRemark3.Multiline = False
        Me.txtRemark3.Name = "txtRemark3"
        Me.txtRemark3.ReadOnly = False
        Me.txtRemark3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark3.Size = New System.Drawing.Size(640, 18)
        Me.txtRemark3.TabIndex = 498
        Me.txtRemark3.TabStopSetting = True
        Me.txtRemark3.TextValue = ""
        Me.txtRemark3.UseSystemPasswordChar = False
        Me.txtRemark3.WidthDef = 640
        Me.txtRemark3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSokoCd
        '
        Me.txtSokoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSokoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSokoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSokoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSokoCd.CountWrappedLine = False
        Me.txtSokoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSokoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSokoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSokoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSokoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSokoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSokoCd.HeightDef = 18
        Me.txtSokoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSokoCd.HissuLabelVisible = False
        Me.txtSokoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSokoCd.IsByteCheck = 100
        Me.txtSokoCd.IsCalendarCheck = False
        Me.txtSokoCd.IsDakutenCheck = False
        Me.txtSokoCd.IsEisuCheck = False
        Me.txtSokoCd.IsForbiddenWordsCheck = False
        Me.txtSokoCd.IsFullByteCheck = 0
        Me.txtSokoCd.IsHankakuCheck = False
        Me.txtSokoCd.IsHissuCheck = False
        Me.txtSokoCd.IsKanaCheck = False
        Me.txtSokoCd.IsMiddleSpace = False
        Me.txtSokoCd.IsNumericCheck = False
        Me.txtSokoCd.IsSujiCheck = False
        Me.txtSokoCd.IsZenkakuCheck = False
        Me.txtSokoCd.ItemName = ""
        Me.txtSokoCd.LineSpace = 0
        Me.txtSokoCd.Location = New System.Drawing.Point(1125, 142)
        Me.txtSokoCd.MaxLength = 100
        Me.txtSokoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSokoCd.MaxLineCount = 0
        Me.txtSokoCd.Multiline = False
        Me.txtSokoCd.Name = "txtSokoCd"
        Me.txtSokoCd.ReadOnly = False
        Me.txtSokoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSokoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSokoCd.Size = New System.Drawing.Size(95, 18)
        Me.txtSokoCd.TabIndex = 506
        Me.txtSokoCd.TabStopSetting = True
        Me.txtSokoCd.TextValue = ""
        Me.txtSokoCd.UseSystemPasswordChar = False
        Me.txtSokoCd.Visible = False
        Me.txtSokoCd.WidthDef = 95
        Me.txtSokoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRemark3
        '
        Me.lblTitleRemark3.AutoSize = True
        Me.lblTitleRemark3.AutoSizeDef = True
        Me.lblTitleRemark3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRemark3.EnableStatus = False
        Me.lblTitleRemark3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark3.HeightDef = 13
        Me.lblTitleRemark3.Location = New System.Drawing.Point(10, 149)
        Me.lblTitleRemark3.Name = "lblTitleRemark3"
        Me.lblTitleRemark3.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleRemark3.TabIndex = 497
        Me.lblTitleRemark3.Text = "注意事項３"
        Me.lblTitleRemark3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRemark3.TextValue = "注意事項３"
        Me.lblTitleRemark3.WidthDef = 77
        '
        'lblTitleSokoCd
        '
        Me.lblTitleSokoCd.AutoSize = True
        Me.lblTitleSokoCd.AutoSizeDef = True
        Me.lblTitleSokoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSokoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSokoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSokoCd.EnableStatus = False
        Me.lblTitleSokoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSokoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSokoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSokoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSokoCd.HeightDef = 13
        Me.lblTitleSokoCd.Location = New System.Drawing.Point(1045, 145)
        Me.lblTitleSokoCd.Name = "lblTitleSokoCd"
        Me.lblTitleSokoCd.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleSokoCd.TabIndex = 505
        Me.lblTitleSokoCd.Text = "倉庫コード"
        Me.lblTitleSokoCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSokoCd.TextValue = "倉庫コード"
        Me.lblTitleSokoCd.Visible = False
        Me.lblTitleSokoCd.WidthDef = 77
        '
        'txtRemark2
        '
        Me.txtRemark2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemark2.CountWrappedLine = False
        Me.txtRemark2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark2.HeightDef = 18
        Me.txtRemark2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark2.HissuLabelVisible = False
        Me.txtRemark2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark2.IsByteCheck = 100
        Me.txtRemark2.IsCalendarCheck = False
        Me.txtRemark2.IsDakutenCheck = False
        Me.txtRemark2.IsEisuCheck = False
        Me.txtRemark2.IsForbiddenWordsCheck = False
        Me.txtRemark2.IsFullByteCheck = 0
        Me.txtRemark2.IsHankakuCheck = False
        Me.txtRemark2.IsHissuCheck = False
        Me.txtRemark2.IsKanaCheck = False
        Me.txtRemark2.IsMiddleSpace = False
        Me.txtRemark2.IsNumericCheck = False
        Me.txtRemark2.IsSujiCheck = False
        Me.txtRemark2.IsZenkakuCheck = False
        Me.txtRemark2.ItemName = ""
        Me.txtRemark2.LineSpace = 0
        Me.txtRemark2.Location = New System.Drawing.Point(90, 125)
        Me.txtRemark2.MaxLength = 100
        Me.txtRemark2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark2.MaxLineCount = 0
        Me.txtRemark2.Multiline = False
        Me.txtRemark2.Name = "txtRemark2"
        Me.txtRemark2.ReadOnly = False
        Me.txtRemark2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark2.Size = New System.Drawing.Size(640, 18)
        Me.txtRemark2.TabIndex = 496
        Me.txtRemark2.TabStopSetting = True
        Me.txtRemark2.TextValue = ""
        Me.txtRemark2.UseSystemPasswordChar = False
        Me.txtRemark2.WidthDef = 640
        Me.txtRemark2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyoSeiqtoCd
        '
        Me.lblTitleSagyoSeiqtoCd.AutoSize = True
        Me.lblTitleSagyoSeiqtoCd.AutoSizeDef = True
        Me.lblTitleSagyoSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyoSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyoSeiqtoCd.EnableStatus = False
        Me.lblTitleSagyoSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyoSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyoSeiqtoCd.HeightDef = 13
        Me.lblTitleSagyoSeiqtoCd.Location = New System.Drawing.Point(933, 107)
        Me.lblTitleSagyoSeiqtoCd.Name = "lblTitleSagyoSeiqtoCd"
        Me.lblTitleSagyoSeiqtoCd.Size = New System.Drawing.Size(189, 13)
        Me.lblTitleSagyoSeiqtoCd.TabIndex = 501
        Me.lblTitleSagyoSeiqtoCd.Text = "作業料請求先マスターコード"
        Me.lblTitleSagyoSeiqtoCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyoSeiqtoCd.TextValue = "作業料請求先マスターコード"
        Me.lblTitleSagyoSeiqtoCd.Visible = False
        Me.lblTitleSagyoSeiqtoCd.WidthDef = 189
        '
        'lblTitleRemark2
        '
        Me.lblTitleRemark2.AutoSize = True
        Me.lblTitleRemark2.AutoSizeDef = True
        Me.lblTitleRemark2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRemark2.EnableStatus = False
        Me.lblTitleRemark2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark2.HeightDef = 13
        Me.lblTitleRemark2.Location = New System.Drawing.Point(10, 128)
        Me.lblTitleRemark2.Name = "lblTitleRemark2"
        Me.lblTitleRemark2.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleRemark2.TabIndex = 495
        Me.lblTitleRemark2.Text = "注意事項２"
        Me.lblTitleRemark2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRemark2.TextValue = "注意事項２"
        Me.lblTitleRemark2.WidthDef = 77
        '
        'txtOyaSeiqtoCd
        '
        Me.txtOyaSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOyaSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtOyaSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOyaSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOyaSeiqtoCd.CountWrappedLine = False
        Me.txtOyaSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOyaSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOyaSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOyaSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOyaSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOyaSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOyaSeiqtoCd.HeightDef = 18
        Me.txtOyaSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOyaSeiqtoCd.HissuLabelVisible = False
        Me.txtOyaSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOyaSeiqtoCd.IsByteCheck = 100
        Me.txtOyaSeiqtoCd.IsCalendarCheck = False
        Me.txtOyaSeiqtoCd.IsDakutenCheck = False
        Me.txtOyaSeiqtoCd.IsEisuCheck = False
        Me.txtOyaSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtOyaSeiqtoCd.IsFullByteCheck = 0
        Me.txtOyaSeiqtoCd.IsHankakuCheck = False
        Me.txtOyaSeiqtoCd.IsHissuCheck = False
        Me.txtOyaSeiqtoCd.IsKanaCheck = False
        Me.txtOyaSeiqtoCd.IsMiddleSpace = False
        Me.txtOyaSeiqtoCd.IsNumericCheck = False
        Me.txtOyaSeiqtoCd.IsSujiCheck = False
        Me.txtOyaSeiqtoCd.IsZenkakuCheck = False
        Me.txtOyaSeiqtoCd.ItemName = ""
        Me.txtOyaSeiqtoCd.LineSpace = 0
        Me.txtOyaSeiqtoCd.Location = New System.Drawing.Point(1125, 123)
        Me.txtOyaSeiqtoCd.MaxLength = 100
        Me.txtOyaSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOyaSeiqtoCd.MaxLineCount = 0
        Me.txtOyaSeiqtoCd.Multiline = False
        Me.txtOyaSeiqtoCd.Name = "txtOyaSeiqtoCd"
        Me.txtOyaSeiqtoCd.ReadOnly = False
        Me.txtOyaSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOyaSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOyaSeiqtoCd.Size = New System.Drawing.Size(95, 18)
        Me.txtOyaSeiqtoCd.TabIndex = 504
        Me.txtOyaSeiqtoCd.TabStopSetting = True
        Me.txtOyaSeiqtoCd.TextValue = ""
        Me.txtOyaSeiqtoCd.UseSystemPasswordChar = False
        Me.txtOyaSeiqtoCd.Visible = False
        Me.txtOyaSeiqtoCd.WidthDef = 95
        Me.txtOyaSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtRemark1
        '
        Me.txtRemark1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRemark1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemark1.CountWrappedLine = False
        Me.txtRemark1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark1.HeightDef = 18
        Me.txtRemark1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark1.HissuLabelVisible = False
        Me.txtRemark1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark1.IsByteCheck = 100
        Me.txtRemark1.IsCalendarCheck = False
        Me.txtRemark1.IsDakutenCheck = False
        Me.txtRemark1.IsEisuCheck = False
        Me.txtRemark1.IsForbiddenWordsCheck = False
        Me.txtRemark1.IsFullByteCheck = 0
        Me.txtRemark1.IsHankakuCheck = False
        Me.txtRemark1.IsHissuCheck = False
        Me.txtRemark1.IsKanaCheck = False
        Me.txtRemark1.IsMiddleSpace = False
        Me.txtRemark1.IsNumericCheck = False
        Me.txtRemark1.IsSujiCheck = False
        Me.txtRemark1.IsZenkakuCheck = False
        Me.txtRemark1.ItemName = ""
        Me.txtRemark1.LineSpace = 0
        Me.txtRemark1.Location = New System.Drawing.Point(90, 104)
        Me.txtRemark1.MaxLength = 100
        Me.txtRemark1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark1.MaxLineCount = 0
        Me.txtRemark1.Multiline = False
        Me.txtRemark1.Name = "txtRemark1"
        Me.txtRemark1.ReadOnly = False
        Me.txtRemark1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark1.Size = New System.Drawing.Size(640, 18)
        Me.txtRemark1.TabIndex = 494
        Me.txtRemark1.TabStopSetting = True
        Me.txtRemark1.TextValue = ""
        Me.txtRemark1.UseSystemPasswordChar = False
        Me.txtRemark1.WidthDef = 640
        Me.txtRemark1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoSeiqtoCd
        '
        Me.txtSagyoSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoSeiqtoCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoSeiqtoCd.CountWrappedLine = False
        Me.txtSagyoSeiqtoCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoSeiqtoCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoSeiqtoCd.HeightDef = 18
        Me.txtSagyoSeiqtoCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoSeiqtoCd.HissuLabelVisible = False
        Me.txtSagyoSeiqtoCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoSeiqtoCd.IsByteCheck = 100
        Me.txtSagyoSeiqtoCd.IsCalendarCheck = False
        Me.txtSagyoSeiqtoCd.IsDakutenCheck = False
        Me.txtSagyoSeiqtoCd.IsEisuCheck = False
        Me.txtSagyoSeiqtoCd.IsForbiddenWordsCheck = False
        Me.txtSagyoSeiqtoCd.IsFullByteCheck = 0
        Me.txtSagyoSeiqtoCd.IsHankakuCheck = False
        Me.txtSagyoSeiqtoCd.IsHissuCheck = False
        Me.txtSagyoSeiqtoCd.IsKanaCheck = False
        Me.txtSagyoSeiqtoCd.IsMiddleSpace = False
        Me.txtSagyoSeiqtoCd.IsNumericCheck = False
        Me.txtSagyoSeiqtoCd.IsSujiCheck = False
        Me.txtSagyoSeiqtoCd.IsZenkakuCheck = False
        Me.txtSagyoSeiqtoCd.ItemName = ""
        Me.txtSagyoSeiqtoCd.LineSpace = 0
        Me.txtSagyoSeiqtoCd.Location = New System.Drawing.Point(1125, 104)
        Me.txtSagyoSeiqtoCd.MaxLength = 100
        Me.txtSagyoSeiqtoCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoSeiqtoCd.MaxLineCount = 0
        Me.txtSagyoSeiqtoCd.Multiline = False
        Me.txtSagyoSeiqtoCd.Name = "txtSagyoSeiqtoCd"
        Me.txtSagyoSeiqtoCd.ReadOnly = False
        Me.txtSagyoSeiqtoCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoSeiqtoCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoSeiqtoCd.Size = New System.Drawing.Size(95, 18)
        Me.txtSagyoSeiqtoCd.TabIndex = 502
        Me.txtSagyoSeiqtoCd.TabStopSetting = True
        Me.txtSagyoSeiqtoCd.TextValue = ""
        Me.txtSagyoSeiqtoCd.UseSystemPasswordChar = False
        Me.txtSagyoSeiqtoCd.Visible = False
        Me.txtSagyoSeiqtoCd.WidthDef = 95
        Me.txtSagyoSeiqtoCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleRemark1
        '
        Me.lblTitleRemark1.AutoSize = True
        Me.lblTitleRemark1.AutoSizeDef = True
        Me.lblTitleRemark1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleRemark1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleRemark1.EnableStatus = False
        Me.lblTitleRemark1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleRemark1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleRemark1.HeightDef = 13
        Me.lblTitleRemark1.Location = New System.Drawing.Point(10, 107)
        Me.lblTitleRemark1.Name = "lblTitleRemark1"
        Me.lblTitleRemark1.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleRemark1.TabIndex = 493
        Me.lblTitleRemark1.Text = "注意事項１"
        Me.lblTitleRemark1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleRemark1.TextValue = "注意事項１"
        Me.lblTitleRemark1.WidthDef = 77
        '
        'lblTitleOyaSeiqtoCd
        '
        Me.lblTitleOyaSeiqtoCd.AutoSize = True
        Me.lblTitleOyaSeiqtoCd.AutoSizeDef = True
        Me.lblTitleOyaSeiqtoCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOyaSeiqtoCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleOyaSeiqtoCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleOyaSeiqtoCd.EnableStatus = False
        Me.lblTitleOyaSeiqtoCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOyaSeiqtoCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleOyaSeiqtoCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOyaSeiqtoCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleOyaSeiqtoCd.HeightDef = 13
        Me.lblTitleOyaSeiqtoCd.Location = New System.Drawing.Point(961, 126)
        Me.lblTitleOyaSeiqtoCd.Name = "lblTitleOyaSeiqtoCd"
        Me.lblTitleOyaSeiqtoCd.Size = New System.Drawing.Size(161, 13)
        Me.lblTitleOyaSeiqtoCd.TabIndex = 503
        Me.lblTitleOyaSeiqtoCd.Text = "主請求先マスターコード"
        Me.lblTitleOyaSeiqtoCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleOyaSeiqtoCd.TextValue = "主請求先マスターコード"
        Me.lblTitleOyaSeiqtoCd.Visible = False
        Me.lblTitleOyaSeiqtoCd.WidthDef = 161
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
        Me.lblCustNmM.HissuLabelVisible = True
        Me.lblCustNmM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmM.IsByteCheck = 0
        Me.lblCustNmM.IsCalendarCheck = False
        Me.lblCustNmM.IsDakutenCheck = False
        Me.lblCustNmM.IsEisuCheck = False
        Me.lblCustNmM.IsForbiddenWordsCheck = False
        Me.lblCustNmM.IsFullByteCheck = 0
        Me.lblCustNmM.IsHankakuCheck = False
        Me.lblCustNmM.IsHissuCheck = True
        Me.lblCustNmM.IsKanaCheck = False
        Me.lblCustNmM.IsMiddleSpace = False
        Me.lblCustNmM.IsNumericCheck = False
        Me.lblCustNmM.IsSujiCheck = False
        Me.lblCustNmM.IsZenkakuCheck = False
        Me.lblCustNmM.ItemName = ""
        Me.lblCustNmM.LineSpace = 0
        Me.lblCustNmM.Location = New System.Drawing.Point(171, 62)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmM.TabIndex = 269
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
        Me.lblCustNmL.HissuLabelVisible = True
        Me.lblCustNmL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNmL.IsByteCheck = 0
        Me.lblCustNmL.IsCalendarCheck = False
        Me.lblCustNmL.IsDakutenCheck = False
        Me.lblCustNmL.IsEisuCheck = False
        Me.lblCustNmL.IsForbiddenWordsCheck = False
        Me.lblCustNmL.IsFullByteCheck = 0
        Me.lblCustNmL.IsHankakuCheck = False
        Me.lblCustNmL.IsHissuCheck = True
        Me.lblCustNmL.IsKanaCheck = False
        Me.lblCustNmL.IsMiddleSpace = False
        Me.lblCustNmL.IsNumericCheck = False
        Me.lblCustNmL.IsSujiCheck = False
        Me.lblCustNmL.IsZenkakuCheck = False
        Me.lblCustNmL.ItemName = ""
        Me.lblCustNmL.LineSpace = 0
        Me.lblCustNmL.Location = New System.Drawing.Point(171, 41)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 268
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
        Me.lblTitleCust.Location = New System.Drawing.Point(52, 44)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 267
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
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
        Me.txtCustCdM.Location = New System.Drawing.Point(135, 62)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = True
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 266
        Me.txtCustCdM.TabStop = False
        Me.txtCustCdM.TabStopSetting = False
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.txtCustCdL.Location = New System.Drawing.Point(90, 41)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = True
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 265
        Me.txtCustCdL.TabStop = False
        Me.txtCustCdL.TabStopSetting = False
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.cmbEigyo.Location = New System.Drawing.Point(90, 20)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 1
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(39, 23)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'lblTitleDate
        '
        Me.lblTitleDate.AutoSize = True
        Me.lblTitleDate.AutoSizeDef = True
        Me.lblTitleDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleDate.EnableStatus = False
        Me.lblTitleDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleDate.HeightDef = 13
        Me.lblTitleDate.Location = New System.Drawing.Point(39, 86)
        Me.lblTitleDate.Name = "lblTitleDate"
        Me.lblTitleDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleDate.TabIndex = 264
        Me.lblTitleDate.Text = "作業日"
        Me.lblTitleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleDate.TextValue = "作業日"
        Me.lblTitleDate.WidthDef = 49
        '
        'imdSagyoDate
        '
        Me.imdSagyoDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSagyoDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdSagyoDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdSagyoDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdSagyoDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdSagyoDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSagyoDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdSagyoDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSagyoDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdSagyoDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdSagyoDate.HeightDef = 18
        Me.imdSagyoDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdSagyoDate.HissuLabelVisible = True
        Me.imdSagyoDate.Holiday = True
        Me.imdSagyoDate.IsAfterDateCheck = False
        Me.imdSagyoDate.IsBeforeDateCheck = False
        Me.imdSagyoDate.IsHissuCheck = True
        Me.imdSagyoDate.IsMinDateCheck = "1900/01/01"
        Me.imdSagyoDate.ItemName = ""
        Me.imdSagyoDate.Location = New System.Drawing.Point(90, 83)
        Me.imdSagyoDate.Name = "imdSagyoDate"
        Me.imdSagyoDate.Number = CType(10101000000, Long)
        Me.imdSagyoDate.ReadOnly = False
        Me.imdSagyoDate.Size = New System.Drawing.Size(118, 18)
        Me.imdSagyoDate.TabIndex = 263
        Me.imdSagyoDate.TabStopSetting = True
        Me.imdSagyoDate.TextValue = ""
        Me.imdSagyoDate.Value = New Date(CType(0, Long))
        Me.imdSagyoDate.WidthDef = 118
        '
        'sprDetails
        '
        Me.sprDetails.AccessibleDescription = ""
        Me.sprDetails.AllowUserZoom = False
        Me.sprDetails.AutoImeMode = False
        Me.sprDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetails.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprDetails.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprDetails.CellClickEventArgs = Nothing
        Me.sprDetails.CheckToCheckBox = True
        Me.sprDetails.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprDetails.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetails.EditModeReplace = True
        Me.sprDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetails.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprDetails.ForeColorDef = System.Drawing.Color.Empty
        Me.sprDetails.HeightDef = 407
        Me.sprDetails.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetails.KeyboardCheckBoxOn = False
        Me.sprDetails.Location = New System.Drawing.Point(8, 261)
        Me.sprDetails.Name = "sprDetails"
        Me.sprDetails.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetails.Size = New System.Drawing.Size(1248, 407)
        Me.sprDetails.SortColumn = True
        Me.sprDetails.SpanColumnLock = True
        Me.sprDetails.SpreadDoubleClicked = False
        Me.sprDetails.TabIndex = 262
        Me.sprDetails.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetails.TextValue = Nothing
        Me.sprDetails.UseGrouping = False
        Me.sprDetails.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded
        Me.sprDetails.WidthDef = 1248
        sprDetails_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Back, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StartEditing)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(Global.Microsoft.VisualBasic.ChrW(61)), FarPoint.Win.Spread.SpreadActions.StartEditingFormula)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.C, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.V, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.X, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCopy)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardCut)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ClipboardPasteAll)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectRow)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Z, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Undo)
        sprDetails_InputMapWhenFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Y, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.Redo)
        Me.sprDetails.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetails_InputMapWhenFocusedNormal)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Insert, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Delete, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F10, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.None)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextRow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Left, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Right, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextColumnVisual)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfRows)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfRows)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousPageOfColumns)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToNextPageOfColumns)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfRows)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfRows)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.PageUp, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToPreviousPageOfColumns)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Next], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToNextPageOfColumns)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToFirstColumn)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToLastColumn)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToFirstCell)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToLastCell)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstColumn)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastColumn)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Home, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToFirstCell)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[End], CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ExtendToLastCell)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectColumn)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Space, CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.SelectSheet)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Escape, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.CancelEditing)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.[Return], System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.StopEditing)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Tab, CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.MoveToPreviousColumnWrap)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ClearCell)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F3, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.DateTimeNow)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys.None), FarPoint.Win.Spread.SpreadActions.ShowSubEditor)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Down, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        sprDetails_InputMapWhenAncestorOfFocusedNormal.Parent.Put(New FarPoint.Win.Spread.Keystroke(System.Windows.Forms.Keys.Up, CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.None), System.Windows.Forms.Keys)), FarPoint.Win.Spread.SpreadActions.ComboShowList)
        Me.sprDetails.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, FarPoint.Win.Spread.OperationMode.Normal, sprDetails_InputMapWhenAncestorOfFocusedNormal)
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1114, 13)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 450
        Me.lblSituation.TabStop = False
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
        Me.btnPrint.Location = New System.Drawing.Point(1029, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 453
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 70
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
        Me.lblTitlePrint.Location = New System.Drawing.Point(733, 16)
        Me.lblTitlePrint.Name = "lblTitlePrint"
        Me.lblTitlePrint.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePrint.TabIndex = 451
        Me.lblTitlePrint.Text = "印刷種別"
        Me.lblTitlePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrint.TextValue = "印刷種別"
        Me.lblTitlePrint.WidthDef = 63
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "P021"
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
        Me.cmbPrint.Location = New System.Drawing.Point(799, 13)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(226, 18)
        Me.cmbPrint.TabIndex = 452
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = ""
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 226
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
        Me.btnRowAdd.Location = New System.Drawing.Point(17, 233)
        Me.btnRowAdd.Name = "btnRowAdd"
        Me.btnRowAdd.Size = New System.Drawing.Size(70, 22)
        Me.btnRowAdd.TabIndex = 454
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
        Me.btnRowDel.Location = New System.Drawing.Point(93, 233)
        Me.btnRowDel.Name = "btnRowDel"
        Me.btnRowDel.Size = New System.Drawing.Size(70, 22)
        Me.btnRowDel.TabIndex = 455
        Me.btnRowDel.TabStopSetting = True
        Me.btnRowDel.Text = "行削除"
        Me.btnRowDel.TextValue = "行削除"
        Me.btnRowDel.UseVisualStyleBackColor = True
        Me.btnRowDel.WidthDef = 70
        '
        'grpSagyo
        '
        Me.grpSagyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSagyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSagyo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSagyo.Controls.Add(Me.txtSagyoRemark5)
        Me.grpSagyo.Controls.Add(Me.txtSagyoRemark4)
        Me.grpSagyo.Controls.Add(Me.txtSagyoRemark3)
        Me.grpSagyo.Controls.Add(Me.txtSagyoRemark2)
        Me.grpSagyo.Controls.Add(Me.txtSagyoRemark1)
        Me.grpSagyo.Controls.Add(Me.lblSagyo1)
        Me.grpSagyo.Controls.Add(Me.lblSagyo2)
        Me.grpSagyo.Controls.Add(Me.lblSagyo3)
        Me.grpSagyo.Controls.Add(Me.lblSagyo4)
        Me.grpSagyo.Controls.Add(Me.lblSagyo5)
        Me.grpSagyo.Controls.Add(Me.lblTitleSagyo1)
        Me.grpSagyo.Controls.Add(Me.lblTitleSagyo5)
        Me.grpSagyo.Controls.Add(Me.txtSagyo1)
        Me.grpSagyo.Controls.Add(Me.lblTitleSagyo4)
        Me.grpSagyo.Controls.Add(Me.lblTitleSagyo3)
        Me.grpSagyo.Controls.Add(Me.txtSagyo2)
        Me.grpSagyo.Controls.Add(Me.lblTitleSagyo2)
        Me.grpSagyo.Controls.Add(Me.txtSagyo3)
        Me.grpSagyo.Controls.Add(Me.txtSagyo4)
        Me.grpSagyo.Controls.Add(Me.txtSagyo5)
        Me.grpSagyo.EnableStatus = False
        Me.grpSagyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSagyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSagyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSagyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSagyo.HeightDef = 129
        Me.grpSagyo.Location = New System.Drawing.Point(8, 686)
        Me.grpSagyo.Name = "grpSagyo"
        Me.grpSagyo.Size = New System.Drawing.Size(901, 129)
        Me.grpSagyo.TabIndex = 456
        Me.grpSagyo.TabStop = False
        Me.grpSagyo.Text = "作業情報"
        Me.grpSagyo.TextValue = "作業情報"
        Me.grpSagyo.WidthDef = 901
        '
        'txtSagyoRemark5
        '
        Me.txtSagyoRemark5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark5.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRemark5.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRemark5.CountWrappedLine = False
        Me.txtSagyoRemark5.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRemark5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark5.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRemark5.HeightDef = 18
        Me.txtSagyoRemark5.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRemark5.HissuLabelVisible = False
        Me.txtSagyoRemark5.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRemark5.IsByteCheck = 100
        Me.txtSagyoRemark5.IsCalendarCheck = False
        Me.txtSagyoRemark5.IsDakutenCheck = False
        Me.txtSagyoRemark5.IsEisuCheck = False
        Me.txtSagyoRemark5.IsForbiddenWordsCheck = False
        Me.txtSagyoRemark5.IsFullByteCheck = 0
        Me.txtSagyoRemark5.IsHankakuCheck = False
        Me.txtSagyoRemark5.IsHissuCheck = False
        Me.txtSagyoRemark5.IsKanaCheck = False
        Me.txtSagyoRemark5.IsMiddleSpace = False
        Me.txtSagyoRemark5.IsNumericCheck = False
        Me.txtSagyoRemark5.IsSujiCheck = False
        Me.txtSagyoRemark5.IsZenkakuCheck = False
        Me.txtSagyoRemark5.ItemName = ""
        Me.txtSagyoRemark5.LineSpace = 0
        Me.txtSagyoRemark5.Location = New System.Drawing.Point(339, 101)
        Me.txtSagyoRemark5.MaxLength = 100
        Me.txtSagyoRemark5.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRemark5.MaxLineCount = 0
        Me.txtSagyoRemark5.Multiline = False
        Me.txtSagyoRemark5.Name = "txtSagyoRemark5"
        Me.txtSagyoRemark5.ReadOnly = False
        Me.txtSagyoRemark5.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRemark5.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRemark5.Size = New System.Drawing.Size(556, 18)
        Me.txtSagyoRemark5.TabIndex = 513
        Me.txtSagyoRemark5.TabStopSetting = True
        Me.txtSagyoRemark5.TextValue = ""
        Me.txtSagyoRemark5.UseSystemPasswordChar = False
        Me.txtSagyoRemark5.WidthDef = 556
        Me.txtSagyoRemark5.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoRemark4
        '
        Me.txtSagyoRemark4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark4.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRemark4.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRemark4.CountWrappedLine = False
        Me.txtSagyoRemark4.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRemark4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark4.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRemark4.HeightDef = 18
        Me.txtSagyoRemark4.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRemark4.HissuLabelVisible = False
        Me.txtSagyoRemark4.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRemark4.IsByteCheck = 100
        Me.txtSagyoRemark4.IsCalendarCheck = False
        Me.txtSagyoRemark4.IsDakutenCheck = False
        Me.txtSagyoRemark4.IsEisuCheck = False
        Me.txtSagyoRemark4.IsForbiddenWordsCheck = False
        Me.txtSagyoRemark4.IsFullByteCheck = 0
        Me.txtSagyoRemark4.IsHankakuCheck = False
        Me.txtSagyoRemark4.IsHissuCheck = False
        Me.txtSagyoRemark4.IsKanaCheck = False
        Me.txtSagyoRemark4.IsMiddleSpace = False
        Me.txtSagyoRemark4.IsNumericCheck = False
        Me.txtSagyoRemark4.IsSujiCheck = False
        Me.txtSagyoRemark4.IsZenkakuCheck = False
        Me.txtSagyoRemark4.ItemName = ""
        Me.txtSagyoRemark4.LineSpace = 0
        Me.txtSagyoRemark4.Location = New System.Drawing.Point(339, 80)
        Me.txtSagyoRemark4.MaxLength = 100
        Me.txtSagyoRemark4.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRemark4.MaxLineCount = 0
        Me.txtSagyoRemark4.Multiline = False
        Me.txtSagyoRemark4.Name = "txtSagyoRemark4"
        Me.txtSagyoRemark4.ReadOnly = False
        Me.txtSagyoRemark4.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRemark4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRemark4.Size = New System.Drawing.Size(556, 18)
        Me.txtSagyoRemark4.TabIndex = 512
        Me.txtSagyoRemark4.TabStopSetting = True
        Me.txtSagyoRemark4.TextValue = ""
        Me.txtSagyoRemark4.UseSystemPasswordChar = False
        Me.txtSagyoRemark4.WidthDef = 556
        Me.txtSagyoRemark4.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoRemark3
        '
        Me.txtSagyoRemark3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRemark3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRemark3.CountWrappedLine = False
        Me.txtSagyoRemark3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRemark3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRemark3.HeightDef = 18
        Me.txtSagyoRemark3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRemark3.HissuLabelVisible = False
        Me.txtSagyoRemark3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRemark3.IsByteCheck = 100
        Me.txtSagyoRemark3.IsCalendarCheck = False
        Me.txtSagyoRemark3.IsDakutenCheck = False
        Me.txtSagyoRemark3.IsEisuCheck = False
        Me.txtSagyoRemark3.IsForbiddenWordsCheck = False
        Me.txtSagyoRemark3.IsFullByteCheck = 0
        Me.txtSagyoRemark3.IsHankakuCheck = False
        Me.txtSagyoRemark3.IsHissuCheck = False
        Me.txtSagyoRemark3.IsKanaCheck = False
        Me.txtSagyoRemark3.IsMiddleSpace = False
        Me.txtSagyoRemark3.IsNumericCheck = False
        Me.txtSagyoRemark3.IsSujiCheck = False
        Me.txtSagyoRemark3.IsZenkakuCheck = False
        Me.txtSagyoRemark3.ItemName = ""
        Me.txtSagyoRemark3.LineSpace = 0
        Me.txtSagyoRemark3.Location = New System.Drawing.Point(339, 59)
        Me.txtSagyoRemark3.MaxLength = 100
        Me.txtSagyoRemark3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRemark3.MaxLineCount = 0
        Me.txtSagyoRemark3.Multiline = False
        Me.txtSagyoRemark3.Name = "txtSagyoRemark3"
        Me.txtSagyoRemark3.ReadOnly = False
        Me.txtSagyoRemark3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRemark3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRemark3.Size = New System.Drawing.Size(556, 18)
        Me.txtSagyoRemark3.TabIndex = 511
        Me.txtSagyoRemark3.TabStopSetting = True
        Me.txtSagyoRemark3.TextValue = ""
        Me.txtSagyoRemark3.UseSystemPasswordChar = False
        Me.txtSagyoRemark3.WidthDef = 556
        Me.txtSagyoRemark3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoRemark2
        '
        Me.txtSagyoRemark2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRemark2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRemark2.CountWrappedLine = False
        Me.txtSagyoRemark2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRemark2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRemark2.HeightDef = 18
        Me.txtSagyoRemark2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRemark2.HissuLabelVisible = False
        Me.txtSagyoRemark2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRemark2.IsByteCheck = 100
        Me.txtSagyoRemark2.IsCalendarCheck = False
        Me.txtSagyoRemark2.IsDakutenCheck = False
        Me.txtSagyoRemark2.IsEisuCheck = False
        Me.txtSagyoRemark2.IsForbiddenWordsCheck = False
        Me.txtSagyoRemark2.IsFullByteCheck = 0
        Me.txtSagyoRemark2.IsHankakuCheck = False
        Me.txtSagyoRemark2.IsHissuCheck = False
        Me.txtSagyoRemark2.IsKanaCheck = False
        Me.txtSagyoRemark2.IsMiddleSpace = False
        Me.txtSagyoRemark2.IsNumericCheck = False
        Me.txtSagyoRemark2.IsSujiCheck = False
        Me.txtSagyoRemark2.IsZenkakuCheck = False
        Me.txtSagyoRemark2.ItemName = ""
        Me.txtSagyoRemark2.LineSpace = 0
        Me.txtSagyoRemark2.Location = New System.Drawing.Point(339, 38)
        Me.txtSagyoRemark2.MaxLength = 100
        Me.txtSagyoRemark2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRemark2.MaxLineCount = 0
        Me.txtSagyoRemark2.Multiline = False
        Me.txtSagyoRemark2.Name = "txtSagyoRemark2"
        Me.txtSagyoRemark2.ReadOnly = False
        Me.txtSagyoRemark2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRemark2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRemark2.Size = New System.Drawing.Size(556, 18)
        Me.txtSagyoRemark2.TabIndex = 510
        Me.txtSagyoRemark2.TabStopSetting = True
        Me.txtSagyoRemark2.TextValue = ""
        Me.txtSagyoRemark2.UseSystemPasswordChar = False
        Me.txtSagyoRemark2.WidthDef = 556
        Me.txtSagyoRemark2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyoRemark1
        '
        Me.txtSagyoRemark1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyoRemark1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyoRemark1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyoRemark1.CountWrappedLine = False
        Me.txtSagyoRemark1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyoRemark1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyoRemark1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyoRemark1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyoRemark1.HeightDef = 18
        Me.txtSagyoRemark1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyoRemark1.HissuLabelVisible = False
        Me.txtSagyoRemark1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtSagyoRemark1.IsByteCheck = 100
        Me.txtSagyoRemark1.IsCalendarCheck = False
        Me.txtSagyoRemark1.IsDakutenCheck = False
        Me.txtSagyoRemark1.IsEisuCheck = False
        Me.txtSagyoRemark1.IsForbiddenWordsCheck = False
        Me.txtSagyoRemark1.IsFullByteCheck = 0
        Me.txtSagyoRemark1.IsHankakuCheck = False
        Me.txtSagyoRemark1.IsHissuCheck = False
        Me.txtSagyoRemark1.IsKanaCheck = False
        Me.txtSagyoRemark1.IsMiddleSpace = False
        Me.txtSagyoRemark1.IsNumericCheck = False
        Me.txtSagyoRemark1.IsSujiCheck = False
        Me.txtSagyoRemark1.IsZenkakuCheck = False
        Me.txtSagyoRemark1.ItemName = ""
        Me.txtSagyoRemark1.LineSpace = 0
        Me.txtSagyoRemark1.Location = New System.Drawing.Point(339, 17)
        Me.txtSagyoRemark1.MaxLength = 100
        Me.txtSagyoRemark1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyoRemark1.MaxLineCount = 0
        Me.txtSagyoRemark1.Multiline = False
        Me.txtSagyoRemark1.Name = "txtSagyoRemark1"
        Me.txtSagyoRemark1.ReadOnly = False
        Me.txtSagyoRemark1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyoRemark1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyoRemark1.Size = New System.Drawing.Size(556, 18)
        Me.txtSagyoRemark1.TabIndex = 509
        Me.txtSagyoRemark1.TabStopSetting = True
        Me.txtSagyoRemark1.TextValue = ""
        Me.txtSagyoRemark1.UseSystemPasswordChar = False
        Me.txtSagyoRemark1.WidthDef = 556
        Me.txtSagyoRemark1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyo1
        '
        Me.lblSagyo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyo1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyo1.CountWrappedLine = False
        Me.lblSagyo1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyo1.HeightDef = 18
        Me.lblSagyo1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo1.HissuLabelVisible = False
        Me.lblSagyo1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyo1.IsByteCheck = 0
        Me.lblSagyo1.IsCalendarCheck = False
        Me.lblSagyo1.IsDakutenCheck = False
        Me.lblSagyo1.IsEisuCheck = False
        Me.lblSagyo1.IsForbiddenWordsCheck = False
        Me.lblSagyo1.IsFullByteCheck = 0
        Me.lblSagyo1.IsHankakuCheck = False
        Me.lblSagyo1.IsHissuCheck = False
        Me.lblSagyo1.IsKanaCheck = False
        Me.lblSagyo1.IsMiddleSpace = False
        Me.lblSagyo1.IsNumericCheck = False
        Me.lblSagyo1.IsSujiCheck = False
        Me.lblSagyo1.IsZenkakuCheck = False
        Me.lblSagyo1.ItemName = ""
        Me.lblSagyo1.LineSpace = 0
        Me.lblSagyo1.Location = New System.Drawing.Point(114, 17)
        Me.lblSagyo1.MaxLength = 0
        Me.lblSagyo1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyo1.MaxLineCount = 0
        Me.lblSagyo1.Multiline = False
        Me.lblSagyo1.Name = "lblSagyo1"
        Me.lblSagyo1.ReadOnly = True
        Me.lblSagyo1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyo1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyo1.Size = New System.Drawing.Size(242, 18)
        Me.lblSagyo1.TabIndex = 194
        Me.lblSagyo1.TabStop = False
        Me.lblSagyo1.TabStopSetting = False
        Me.lblSagyo1.TextValue = "ＮＮＮＮＮＮ"
        Me.lblSagyo1.UseSystemPasswordChar = False
        Me.lblSagyo1.WidthDef = 242
        Me.lblSagyo1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyo2
        '
        Me.lblSagyo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyo2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyo2.CountWrappedLine = False
        Me.lblSagyo2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyo2.HeightDef = 18
        Me.lblSagyo2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo2.HissuLabelVisible = False
        Me.lblSagyo2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyo2.IsByteCheck = 0
        Me.lblSagyo2.IsCalendarCheck = False
        Me.lblSagyo2.IsDakutenCheck = False
        Me.lblSagyo2.IsEisuCheck = False
        Me.lblSagyo2.IsForbiddenWordsCheck = False
        Me.lblSagyo2.IsFullByteCheck = 0
        Me.lblSagyo2.IsHankakuCheck = False
        Me.lblSagyo2.IsHissuCheck = False
        Me.lblSagyo2.IsKanaCheck = False
        Me.lblSagyo2.IsMiddleSpace = False
        Me.lblSagyo2.IsNumericCheck = False
        Me.lblSagyo2.IsSujiCheck = False
        Me.lblSagyo2.IsZenkakuCheck = False
        Me.lblSagyo2.ItemName = ""
        Me.lblSagyo2.LineSpace = 0
        Me.lblSagyo2.Location = New System.Drawing.Point(114, 38)
        Me.lblSagyo2.MaxLength = 0
        Me.lblSagyo2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyo2.MaxLineCount = 0
        Me.lblSagyo2.Multiline = False
        Me.lblSagyo2.Name = "lblSagyo2"
        Me.lblSagyo2.ReadOnly = True
        Me.lblSagyo2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyo2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyo2.Size = New System.Drawing.Size(242, 18)
        Me.lblSagyo2.TabIndex = 197
        Me.lblSagyo2.TabStop = False
        Me.lblSagyo2.TabStopSetting = False
        Me.lblSagyo2.TextValue = ""
        Me.lblSagyo2.UseSystemPasswordChar = False
        Me.lblSagyo2.WidthDef = 242
        Me.lblSagyo2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyo3
        '
        Me.lblSagyo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyo3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyo3.CountWrappedLine = False
        Me.lblSagyo3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyo3.HeightDef = 18
        Me.lblSagyo3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo3.HissuLabelVisible = False
        Me.lblSagyo3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyo3.IsByteCheck = 0
        Me.lblSagyo3.IsCalendarCheck = False
        Me.lblSagyo3.IsDakutenCheck = False
        Me.lblSagyo3.IsEisuCheck = False
        Me.lblSagyo3.IsForbiddenWordsCheck = False
        Me.lblSagyo3.IsFullByteCheck = 0
        Me.lblSagyo3.IsHankakuCheck = False
        Me.lblSagyo3.IsHissuCheck = False
        Me.lblSagyo3.IsKanaCheck = False
        Me.lblSagyo3.IsMiddleSpace = False
        Me.lblSagyo3.IsNumericCheck = False
        Me.lblSagyo3.IsSujiCheck = False
        Me.lblSagyo3.IsZenkakuCheck = False
        Me.lblSagyo3.ItemName = ""
        Me.lblSagyo3.LineSpace = 0
        Me.lblSagyo3.Location = New System.Drawing.Point(114, 59)
        Me.lblSagyo3.MaxLength = 0
        Me.lblSagyo3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyo3.MaxLineCount = 0
        Me.lblSagyo3.Multiline = False
        Me.lblSagyo3.Name = "lblSagyo3"
        Me.lblSagyo3.ReadOnly = True
        Me.lblSagyo3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyo3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyo3.Size = New System.Drawing.Size(242, 18)
        Me.lblSagyo3.TabIndex = 200
        Me.lblSagyo3.TabStop = False
        Me.lblSagyo3.TabStopSetting = False
        Me.lblSagyo3.TextValue = ""
        Me.lblSagyo3.UseSystemPasswordChar = False
        Me.lblSagyo3.WidthDef = 242
        Me.lblSagyo3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyo4
        '
        Me.lblSagyo4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo4.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyo4.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyo4.CountWrappedLine = False
        Me.lblSagyo4.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyo4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo4.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyo4.HeightDef = 18
        Me.lblSagyo4.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo4.HissuLabelVisible = False
        Me.lblSagyo4.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyo4.IsByteCheck = 0
        Me.lblSagyo4.IsCalendarCheck = False
        Me.lblSagyo4.IsDakutenCheck = False
        Me.lblSagyo4.IsEisuCheck = False
        Me.lblSagyo4.IsForbiddenWordsCheck = False
        Me.lblSagyo4.IsFullByteCheck = 0
        Me.lblSagyo4.IsHankakuCheck = False
        Me.lblSagyo4.IsHissuCheck = False
        Me.lblSagyo4.IsKanaCheck = False
        Me.lblSagyo4.IsMiddleSpace = False
        Me.lblSagyo4.IsNumericCheck = False
        Me.lblSagyo4.IsSujiCheck = False
        Me.lblSagyo4.IsZenkakuCheck = False
        Me.lblSagyo4.ItemName = ""
        Me.lblSagyo4.LineSpace = 0
        Me.lblSagyo4.Location = New System.Drawing.Point(114, 80)
        Me.lblSagyo4.MaxLength = 0
        Me.lblSagyo4.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyo4.MaxLineCount = 0
        Me.lblSagyo4.Multiline = False
        Me.lblSagyo4.Name = "lblSagyo4"
        Me.lblSagyo4.ReadOnly = True
        Me.lblSagyo4.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyo4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyo4.Size = New System.Drawing.Size(242, 18)
        Me.lblSagyo4.TabIndex = 203
        Me.lblSagyo4.TabStop = False
        Me.lblSagyo4.TabStopSetting = False
        Me.lblSagyo4.TextValue = ""
        Me.lblSagyo4.UseSystemPasswordChar = False
        Me.lblSagyo4.WidthDef = 242
        Me.lblSagyo4.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSagyo5
        '
        Me.lblSagyo5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo5.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSagyo5.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSagyo5.CountWrappedLine = False
        Me.lblSagyo5.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSagyo5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSagyo5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSagyo5.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSagyo5.HeightDef = 18
        Me.lblSagyo5.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSagyo5.HissuLabelVisible = False
        Me.lblSagyo5.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSagyo5.IsByteCheck = 0
        Me.lblSagyo5.IsCalendarCheck = False
        Me.lblSagyo5.IsDakutenCheck = False
        Me.lblSagyo5.IsEisuCheck = False
        Me.lblSagyo5.IsForbiddenWordsCheck = False
        Me.lblSagyo5.IsFullByteCheck = 0
        Me.lblSagyo5.IsHankakuCheck = False
        Me.lblSagyo5.IsHissuCheck = False
        Me.lblSagyo5.IsKanaCheck = False
        Me.lblSagyo5.IsMiddleSpace = False
        Me.lblSagyo5.IsNumericCheck = False
        Me.lblSagyo5.IsSujiCheck = False
        Me.lblSagyo5.IsZenkakuCheck = False
        Me.lblSagyo5.ItemName = ""
        Me.lblSagyo5.LineSpace = 0
        Me.lblSagyo5.Location = New System.Drawing.Point(114, 101)
        Me.lblSagyo5.MaxLength = 0
        Me.lblSagyo5.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSagyo5.MaxLineCount = 0
        Me.lblSagyo5.Multiline = False
        Me.lblSagyo5.Name = "lblSagyo5"
        Me.lblSagyo5.ReadOnly = True
        Me.lblSagyo5.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSagyo5.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSagyo5.Size = New System.Drawing.Size(242, 18)
        Me.lblSagyo5.TabIndex = 206
        Me.lblSagyo5.TabStop = False
        Me.lblSagyo5.TabStopSetting = False
        Me.lblSagyo5.TextValue = ""
        Me.lblSagyo5.UseSystemPasswordChar = False
        Me.lblSagyo5.WidthDef = 242
        Me.lblSagyo5.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo1
        '
        Me.lblTitleSagyo1.AutoSize = True
        Me.lblTitleSagyo1.AutoSizeDef = True
        Me.lblTitleSagyo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo1.EnableStatus = False
        Me.lblTitleSagyo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo1.HeightDef = 13
        Me.lblTitleSagyo1.Location = New System.Drawing.Point(10, 20)
        Me.lblTitleSagyo1.Name = "lblTitleSagyo1"
        Me.lblTitleSagyo1.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo1.TabIndex = 215
        Me.lblTitleSagyo1.Text = "作業①"
        Me.lblTitleSagyo1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo1.TextValue = "作業①"
        Me.lblTitleSagyo1.WidthDef = 49
        '
        'lblTitleSagyo5
        '
        Me.lblTitleSagyo5.AutoSize = True
        Me.lblTitleSagyo5.AutoSizeDef = True
        Me.lblTitleSagyo5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo5.EnableStatus = False
        Me.lblTitleSagyo5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo5.HeightDef = 13
        Me.lblTitleSagyo5.Location = New System.Drawing.Point(10, 104)
        Me.lblTitleSagyo5.Name = "lblTitleSagyo5"
        Me.lblTitleSagyo5.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo5.TabIndex = 219
        Me.lblTitleSagyo5.Text = "作業⑤"
        Me.lblTitleSagyo5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo5.TextValue = "作業⑤"
        Me.lblTitleSagyo5.WidthDef = 49
        '
        'txtSagyo1
        '
        Me.txtSagyo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyo1.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyo1.CountWrappedLine = False
        Me.txtSagyo1.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo1.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyo1.HeightDef = 18
        Me.txtSagyo1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyo1.HissuLabelVisible = False
        Me.txtSagyo1.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyo1.IsByteCheck = 5
        Me.txtSagyo1.IsCalendarCheck = False
        Me.txtSagyo1.IsDakutenCheck = False
        Me.txtSagyo1.IsEisuCheck = False
        Me.txtSagyo1.IsForbiddenWordsCheck = False
        Me.txtSagyo1.IsFullByteCheck = 0
        Me.txtSagyo1.IsHankakuCheck = False
        Me.txtSagyo1.IsHissuCheck = False
        Me.txtSagyo1.IsKanaCheck = False
        Me.txtSagyo1.IsMiddleSpace = False
        Me.txtSagyo1.IsNumericCheck = False
        Me.txtSagyo1.IsSujiCheck = False
        Me.txtSagyo1.IsZenkakuCheck = False
        Me.txtSagyo1.ItemName = ""
        Me.txtSagyo1.LineSpace = 0
        Me.txtSagyo1.Location = New System.Drawing.Point(62, 17)
        Me.txtSagyo1.MaxLength = 5
        Me.txtSagyo1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyo1.MaxLineCount = 0
        Me.txtSagyo1.Multiline = False
        Me.txtSagyo1.Name = "txtSagyo1"
        Me.txtSagyo1.ReadOnly = False
        Me.txtSagyo1.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyo1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyo1.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyo1.TabIndex = 244
        Me.txtSagyo1.TabStopSetting = True
        Me.txtSagyo1.TextValue = "XXXXX"
        Me.txtSagyo1.UseSystemPasswordChar = False
        Me.txtSagyo1.WidthDef = 68
        Me.txtSagyo1.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo4
        '
        Me.lblTitleSagyo4.AutoSize = True
        Me.lblTitleSagyo4.AutoSizeDef = True
        Me.lblTitleSagyo4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo4.EnableStatus = False
        Me.lblTitleSagyo4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo4.HeightDef = 13
        Me.lblTitleSagyo4.Location = New System.Drawing.Point(10, 83)
        Me.lblTitleSagyo4.Name = "lblTitleSagyo4"
        Me.lblTitleSagyo4.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo4.TabIndex = 218
        Me.lblTitleSagyo4.Text = "作業④"
        Me.lblTitleSagyo4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo4.TextValue = "作業④"
        Me.lblTitleSagyo4.WidthDef = 49
        '
        'lblTitleSagyo3
        '
        Me.lblTitleSagyo3.AutoSize = True
        Me.lblTitleSagyo3.AutoSizeDef = True
        Me.lblTitleSagyo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo3.EnableStatus = False
        Me.lblTitleSagyo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo3.HeightDef = 13
        Me.lblTitleSagyo3.Location = New System.Drawing.Point(10, 62)
        Me.lblTitleSagyo3.Name = "lblTitleSagyo3"
        Me.lblTitleSagyo3.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo3.TabIndex = 217
        Me.lblTitleSagyo3.Text = "作業③"
        Me.lblTitleSagyo3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo3.TextValue = "作業③"
        Me.lblTitleSagyo3.WidthDef = 49
        '
        'txtSagyo2
        '
        Me.txtSagyo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo2.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyo2.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyo2.CountWrappedLine = False
        Me.txtSagyo2.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo2.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyo2.HeightDef = 18
        Me.txtSagyo2.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyo2.HissuLabelVisible = False
        Me.txtSagyo2.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyo2.IsByteCheck = 5
        Me.txtSagyo2.IsCalendarCheck = False
        Me.txtSagyo2.IsDakutenCheck = False
        Me.txtSagyo2.IsEisuCheck = False
        Me.txtSagyo2.IsForbiddenWordsCheck = False
        Me.txtSagyo2.IsFullByteCheck = 0
        Me.txtSagyo2.IsHankakuCheck = False
        Me.txtSagyo2.IsHissuCheck = False
        Me.txtSagyo2.IsKanaCheck = False
        Me.txtSagyo2.IsMiddleSpace = False
        Me.txtSagyo2.IsNumericCheck = False
        Me.txtSagyo2.IsSujiCheck = False
        Me.txtSagyo2.IsZenkakuCheck = False
        Me.txtSagyo2.ItemName = ""
        Me.txtSagyo2.LineSpace = 0
        Me.txtSagyo2.Location = New System.Drawing.Point(62, 38)
        Me.txtSagyo2.MaxLength = 5
        Me.txtSagyo2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyo2.MaxLineCount = 0
        Me.txtSagyo2.Multiline = False
        Me.txtSagyo2.Name = "txtSagyo2"
        Me.txtSagyo2.ReadOnly = False
        Me.txtSagyo2.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyo2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyo2.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyo2.TabIndex = 245
        Me.txtSagyo2.TabStopSetting = True
        Me.txtSagyo2.TextValue = ""
        Me.txtSagyo2.UseSystemPasswordChar = False
        Me.txtSagyo2.WidthDef = 68
        Me.txtSagyo2.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSagyo2
        '
        Me.lblTitleSagyo2.AutoSize = True
        Me.lblTitleSagyo2.AutoSizeDef = True
        Me.lblTitleSagyo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSagyo2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSagyo2.EnableStatus = False
        Me.lblTitleSagyo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSagyo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSagyo2.HeightDef = 13
        Me.lblTitleSagyo2.Location = New System.Drawing.Point(10, 41)
        Me.lblTitleSagyo2.Name = "lblTitleSagyo2"
        Me.lblTitleSagyo2.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSagyo2.TabIndex = 216
        Me.lblTitleSagyo2.Text = "作業②"
        Me.lblTitleSagyo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSagyo2.TextValue = "作業②"
        Me.lblTitleSagyo2.WidthDef = 49
        '
        'txtSagyo3
        '
        Me.txtSagyo3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo3.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyo3.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyo3.CountWrappedLine = False
        Me.txtSagyo3.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo3.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyo3.HeightDef = 18
        Me.txtSagyo3.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyo3.HissuLabelVisible = False
        Me.txtSagyo3.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyo3.IsByteCheck = 5
        Me.txtSagyo3.IsCalendarCheck = False
        Me.txtSagyo3.IsDakutenCheck = False
        Me.txtSagyo3.IsEisuCheck = False
        Me.txtSagyo3.IsForbiddenWordsCheck = False
        Me.txtSagyo3.IsFullByteCheck = 0
        Me.txtSagyo3.IsHankakuCheck = False
        Me.txtSagyo3.IsHissuCheck = False
        Me.txtSagyo3.IsKanaCheck = False
        Me.txtSagyo3.IsMiddleSpace = False
        Me.txtSagyo3.IsNumericCheck = False
        Me.txtSagyo3.IsSujiCheck = False
        Me.txtSagyo3.IsZenkakuCheck = False
        Me.txtSagyo3.ItemName = ""
        Me.txtSagyo3.LineSpace = 0
        Me.txtSagyo3.Location = New System.Drawing.Point(62, 59)
        Me.txtSagyo3.MaxLength = 5
        Me.txtSagyo3.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyo3.MaxLineCount = 0
        Me.txtSagyo3.Multiline = False
        Me.txtSagyo3.Name = "txtSagyo3"
        Me.txtSagyo3.ReadOnly = False
        Me.txtSagyo3.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyo3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyo3.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyo3.TabIndex = 246
        Me.txtSagyo3.TabStopSetting = True
        Me.txtSagyo3.TextValue = ""
        Me.txtSagyo3.UseSystemPasswordChar = False
        Me.txtSagyo3.WidthDef = 68
        Me.txtSagyo3.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyo4
        '
        Me.txtSagyo4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo4.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyo4.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyo4.CountWrappedLine = False
        Me.txtSagyo4.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyo4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo4.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyo4.HeightDef = 18
        Me.txtSagyo4.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyo4.HissuLabelVisible = False
        Me.txtSagyo4.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyo4.IsByteCheck = 5
        Me.txtSagyo4.IsCalendarCheck = False
        Me.txtSagyo4.IsDakutenCheck = False
        Me.txtSagyo4.IsEisuCheck = False
        Me.txtSagyo4.IsForbiddenWordsCheck = False
        Me.txtSagyo4.IsFullByteCheck = 0
        Me.txtSagyo4.IsHankakuCheck = False
        Me.txtSagyo4.IsHissuCheck = False
        Me.txtSagyo4.IsKanaCheck = False
        Me.txtSagyo4.IsMiddleSpace = False
        Me.txtSagyo4.IsNumericCheck = False
        Me.txtSagyo4.IsSujiCheck = False
        Me.txtSagyo4.IsZenkakuCheck = False
        Me.txtSagyo4.ItemName = ""
        Me.txtSagyo4.LineSpace = 0
        Me.txtSagyo4.Location = New System.Drawing.Point(62, 80)
        Me.txtSagyo4.MaxLength = 5
        Me.txtSagyo4.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyo4.MaxLineCount = 0
        Me.txtSagyo4.Multiline = False
        Me.txtSagyo4.Name = "txtSagyo4"
        Me.txtSagyo4.ReadOnly = False
        Me.txtSagyo4.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyo4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyo4.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyo4.TabIndex = 247
        Me.txtSagyo4.TabStopSetting = True
        Me.txtSagyo4.TextValue = ""
        Me.txtSagyo4.UseSystemPasswordChar = False
        Me.txtSagyo4.WidthDef = 68
        Me.txtSagyo4.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSagyo5
        '
        Me.txtSagyo5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSagyo5.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSagyo5.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSagyo5.CountWrappedLine = False
        Me.txtSagyo5.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSagyo5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSagyo5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSagyo5.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSagyo5.HeightDef = 18
        Me.txtSagyo5.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSagyo5.HissuLabelVisible = False
        Me.txtSagyo5.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA_U
        Me.txtSagyo5.IsByteCheck = 5
        Me.txtSagyo5.IsCalendarCheck = False
        Me.txtSagyo5.IsDakutenCheck = False
        Me.txtSagyo5.IsEisuCheck = False
        Me.txtSagyo5.IsForbiddenWordsCheck = False
        Me.txtSagyo5.IsFullByteCheck = 0
        Me.txtSagyo5.IsHankakuCheck = False
        Me.txtSagyo5.IsHissuCheck = False
        Me.txtSagyo5.IsKanaCheck = False
        Me.txtSagyo5.IsMiddleSpace = False
        Me.txtSagyo5.IsNumericCheck = False
        Me.txtSagyo5.IsSujiCheck = False
        Me.txtSagyo5.IsZenkakuCheck = False
        Me.txtSagyo5.ItemName = ""
        Me.txtSagyo5.LineSpace = 0
        Me.txtSagyo5.Location = New System.Drawing.Point(62, 101)
        Me.txtSagyo5.MaxLength = 5
        Me.txtSagyo5.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSagyo5.MaxLineCount = 0
        Me.txtSagyo5.Multiline = False
        Me.txtSagyo5.Name = "txtSagyo5"
        Me.txtSagyo5.ReadOnly = False
        Me.txtSagyo5.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSagyo5.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSagyo5.Size = New System.Drawing.Size(68, 18)
        Me.txtSagyo5.TabIndex = 248
        Me.txtSagyo5.TabStopSetting = True
        Me.txtSagyo5.TextValue = ""
        Me.txtSagyo5.UseSystemPasswordChar = False
        Me.txtSagyo5.WidthDef = 68
        Me.txtSagyo5.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpZaiko
        '
        Me.grpZaiko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaiko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaiko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpZaiko.EnableStatus = False
        Me.grpZaiko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaiko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaiko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaiko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaiko.HeightDef = 468
        Me.grpZaiko.Location = New System.Drawing.Point(3, 212)
        Me.grpZaiko.Name = "grpZaiko"
        Me.grpZaiko.Size = New System.Drawing.Size(1259, 468)
        Me.grpZaiko.TabIndex = 457
        Me.grpZaiko.TabStop = False
        Me.grpZaiko.Text = "在庫情報"
        Me.grpZaiko.TextValue = "在庫情報"
        Me.grpZaiko.WidthDef = 1259
        '
        'lblTitleKeyNo
        '
        Me.lblTitleKeyNo.AutoSize = True
        Me.lblTitleKeyNo.AutoSizeDef = True
        Me.lblTitleKeyNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeyNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleKeyNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleKeyNo.EnableStatus = False
        Me.lblTitleKeyNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeyNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleKeyNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeyNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleKeyNo.HeightDef = 13
        Me.lblTitleKeyNo.Location = New System.Drawing.Point(0, 846)
        Me.lblTitleKeyNo.Name = "lblTitleKeyNo"
        Me.lblTitleKeyNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleKeyNo.TabIndex = 503
        Me.lblTitleKeyNo.Text = "キー番号"
        Me.lblTitleKeyNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleKeyNo.TextValue = "キー番号"
        Me.lblTitleKeyNo.Visible = False
        Me.lblTitleKeyNo.WidthDef = 63
        '
        'txtKeyNo
        '
        Me.txtKeyNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKeyNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtKeyNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKeyNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtKeyNo.CountWrappedLine = False
        Me.txtKeyNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtKeyNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKeyNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtKeyNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtKeyNo.HeightDef = 18
        Me.txtKeyNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtKeyNo.HissuLabelVisible = False
        Me.txtKeyNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtKeyNo.IsByteCheck = 100
        Me.txtKeyNo.IsCalendarCheck = False
        Me.txtKeyNo.IsDakutenCheck = False
        Me.txtKeyNo.IsEisuCheck = False
        Me.txtKeyNo.IsForbiddenWordsCheck = False
        Me.txtKeyNo.IsFullByteCheck = 0
        Me.txtKeyNo.IsHankakuCheck = False
        Me.txtKeyNo.IsHissuCheck = False
        Me.txtKeyNo.IsKanaCheck = False
        Me.txtKeyNo.IsMiddleSpace = False
        Me.txtKeyNo.IsNumericCheck = False
        Me.txtKeyNo.IsSujiCheck = False
        Me.txtKeyNo.IsZenkakuCheck = False
        Me.txtKeyNo.ItemName = ""
        Me.txtKeyNo.LineSpace = 0
        Me.txtKeyNo.Location = New System.Drawing.Point(69, 844)
        Me.txtKeyNo.MaxLength = 100
        Me.txtKeyNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtKeyNo.MaxLineCount = 0
        Me.txtKeyNo.Multiline = False
        Me.txtKeyNo.Name = "txtKeyNo"
        Me.txtKeyNo.ReadOnly = False
        Me.txtKeyNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtKeyNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtKeyNo.Size = New System.Drawing.Size(95, 18)
        Me.txtKeyNo.TabIndex = 504
        Me.txtKeyNo.TabStopSetting = True
        Me.txtKeyNo.TextValue = ""
        Me.txtKeyNo.UseSystemPasswordChar = False
        Me.txtKeyNo.Visible = False
        Me.txtKeyNo.WidthDef = 95
        Me.txtKeyNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpZaikoMeisai
        '
        Me.grpZaikoMeisai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaikoMeisai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpZaikoMeisai.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpZaikoMeisai.Controls.Add(Me.lblLotNo)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleLotNo)
        Me.grpZaikoMeisai.Controls.Add(Me.lblGoodsNm)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTou)
        Me.grpZaikoMeisai.Controls.Add(Me.lblSitu)
        Me.grpZaikoMeisai.Controls.Add(Me.lblZone)
        Me.grpZaikoMeisai.Controls.Add(Me.lblLoca)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleGoodsNm)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleLoca)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleZone)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleSitu)
        Me.grpZaikoMeisai.Controls.Add(Me.lblTitleTou)
        Me.grpZaikoMeisai.EnableStatus = False
        Me.grpZaikoMeisai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaikoMeisai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpZaikoMeisai.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaikoMeisai.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpZaikoMeisai.HeightDef = 149
        Me.grpZaikoMeisai.Location = New System.Drawing.Point(915, 686)
        Me.grpZaikoMeisai.Name = "grpZaikoMeisai"
        Me.grpZaikoMeisai.Size = New System.Drawing.Size(347, 149)
        Me.grpZaikoMeisai.TabIndex = 505
        Me.grpZaikoMeisai.TabStop = False
        Me.grpZaikoMeisai.Text = "在庫情報"
        Me.grpZaikoMeisai.TextValue = "在庫情報"
        Me.grpZaikoMeisai.WidthDef = 347
        '
        'lblLotNo
        '
        Me.lblLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLotNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLotNo.CountWrappedLine = False
        Me.lblLotNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLotNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblLotNo.HeightDef = 18
        Me.lblLotNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLotNo.HissuLabelVisible = False
        Me.lblLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblLotNo.IsByteCheck = 0
        Me.lblLotNo.IsCalendarCheck = False
        Me.lblLotNo.IsDakutenCheck = False
        Me.lblLotNo.IsEisuCheck = False
        Me.lblLotNo.IsForbiddenWordsCheck = False
        Me.lblLotNo.IsFullByteCheck = 0
        Me.lblLotNo.IsHankakuCheck = False
        Me.lblLotNo.IsHissuCheck = False
        Me.lblLotNo.IsKanaCheck = False
        Me.lblLotNo.IsMiddleSpace = False
        Me.lblLotNo.IsNumericCheck = False
        Me.lblLotNo.IsSujiCheck = False
        Me.lblLotNo.IsZenkakuCheck = False
        Me.lblLotNo.ItemName = ""
        Me.lblLotNo.LineSpace = 0
        Me.lblLotNo.Location = New System.Drawing.Point(104, 122)
        Me.lblLotNo.MaxLength = 0
        Me.lblLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblLotNo.MaxLineCount = 0
        Me.lblLotNo.Multiline = False
        Me.lblLotNo.Name = "lblLotNo"
        Me.lblLotNo.ReadOnly = True
        Me.lblLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblLotNo.Size = New System.Drawing.Size(242, 18)
        Me.lblLotNo.TabIndex = 220
        Me.lblLotNo.TabStop = False
        Me.lblLotNo.TabStopSetting = False
        Me.lblLotNo.TextValue = ""
        Me.lblLotNo.UseSystemPasswordChar = False
        Me.lblLotNo.WidthDef = 242
        Me.lblLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleLotNo.Location = New System.Drawing.Point(39, 125)
        Me.lblTitleLotNo.Name = "lblTitleLotNo"
        Me.lblTitleLotNo.Size = New System.Drawing.Size(63, 13)
        Me.lblTitleLotNo.TabIndex = 221
        Me.lblTitleLotNo.Text = "ロット№"
        Me.lblTitleLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLotNo.TextValue = "ロット№"
        Me.lblTitleLotNo.WidthDef = 63
        '
        'lblGoodsNm
        '
        Me.lblGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGoodsNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsNm.CountWrappedLine = False
        Me.lblGoodsNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblGoodsNm.HeightDef = 18
        Me.lblGoodsNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsNm.HissuLabelVisible = False
        Me.lblGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblGoodsNm.IsByteCheck = 0
        Me.lblGoodsNm.IsCalendarCheck = False
        Me.lblGoodsNm.IsDakutenCheck = False
        Me.lblGoodsNm.IsEisuCheck = False
        Me.lblGoodsNm.IsForbiddenWordsCheck = False
        Me.lblGoodsNm.IsFullByteCheck = 0
        Me.lblGoodsNm.IsHankakuCheck = False
        Me.lblGoodsNm.IsHissuCheck = False
        Me.lblGoodsNm.IsKanaCheck = False
        Me.lblGoodsNm.IsMiddleSpace = False
        Me.lblGoodsNm.IsNumericCheck = False
        Me.lblGoodsNm.IsSujiCheck = False
        Me.lblGoodsNm.IsZenkakuCheck = False
        Me.lblGoodsNm.ItemName = ""
        Me.lblGoodsNm.LineSpace = 0
        Me.lblGoodsNm.Location = New System.Drawing.Point(104, 17)
        Me.lblGoodsNm.MaxLength = 0
        Me.lblGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblGoodsNm.MaxLineCount = 0
        Me.lblGoodsNm.Multiline = False
        Me.lblGoodsNm.Name = "lblGoodsNm"
        Me.lblGoodsNm.ReadOnly = True
        Me.lblGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblGoodsNm.Size = New System.Drawing.Size(242, 18)
        Me.lblGoodsNm.TabIndex = 194
        Me.lblGoodsNm.TabStop = False
        Me.lblGoodsNm.TabStopSetting = False
        Me.lblGoodsNm.TextValue = "ＮＮＮＮＮＮ"
        Me.lblGoodsNm.UseSystemPasswordChar = False
        Me.lblGoodsNm.WidthDef = 242
        Me.lblGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTou
        '
        Me.lblTou.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTou.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTou.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTou.CountWrappedLine = False
        Me.lblTou.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblTou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTou.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblTou.HeightDef = 18
        Me.lblTou.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTou.HissuLabelVisible = False
        Me.lblTou.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblTou.IsByteCheck = 0
        Me.lblTou.IsCalendarCheck = False
        Me.lblTou.IsDakutenCheck = False
        Me.lblTou.IsEisuCheck = False
        Me.lblTou.IsForbiddenWordsCheck = False
        Me.lblTou.IsFullByteCheck = 0
        Me.lblTou.IsHankakuCheck = False
        Me.lblTou.IsHissuCheck = False
        Me.lblTou.IsKanaCheck = False
        Me.lblTou.IsMiddleSpace = False
        Me.lblTou.IsNumericCheck = False
        Me.lblTou.IsSujiCheck = False
        Me.lblTou.IsZenkakuCheck = False
        Me.lblTou.ItemName = ""
        Me.lblTou.LineSpace = 0
        Me.lblTou.Location = New System.Drawing.Point(104, 38)
        Me.lblTou.MaxLength = 0
        Me.lblTou.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblTou.MaxLineCount = 0
        Me.lblTou.Multiline = False
        Me.lblTou.Name = "lblTou"
        Me.lblTou.ReadOnly = True
        Me.lblTou.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblTou.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblTou.Size = New System.Drawing.Size(48, 18)
        Me.lblTou.TabIndex = 197
        Me.lblTou.TabStop = False
        Me.lblTou.TabStopSetting = False
        Me.lblTou.TextValue = ""
        Me.lblTou.UseSystemPasswordChar = False
        Me.lblTou.WidthDef = 48
        Me.lblTou.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSitu
        '
        Me.lblSitu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSitu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSitu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSitu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSitu.CountWrappedLine = False
        Me.lblSitu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSitu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSitu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSitu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSitu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSitu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSitu.HeightDef = 18
        Me.lblSitu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSitu.HissuLabelVisible = False
        Me.lblSitu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSitu.IsByteCheck = 0
        Me.lblSitu.IsCalendarCheck = False
        Me.lblSitu.IsDakutenCheck = False
        Me.lblSitu.IsEisuCheck = False
        Me.lblSitu.IsForbiddenWordsCheck = False
        Me.lblSitu.IsFullByteCheck = 0
        Me.lblSitu.IsHankakuCheck = False
        Me.lblSitu.IsHissuCheck = False
        Me.lblSitu.IsKanaCheck = False
        Me.lblSitu.IsMiddleSpace = False
        Me.lblSitu.IsNumericCheck = False
        Me.lblSitu.IsSujiCheck = False
        Me.lblSitu.IsZenkakuCheck = False
        Me.lblSitu.ItemName = ""
        Me.lblSitu.LineSpace = 0
        Me.lblSitu.Location = New System.Drawing.Point(104, 59)
        Me.lblSitu.MaxLength = 0
        Me.lblSitu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSitu.MaxLineCount = 0
        Me.lblSitu.Multiline = False
        Me.lblSitu.Name = "lblSitu"
        Me.lblSitu.ReadOnly = True
        Me.lblSitu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSitu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSitu.Size = New System.Drawing.Size(48, 18)
        Me.lblSitu.TabIndex = 200
        Me.lblSitu.TabStop = False
        Me.lblSitu.TabStopSetting = False
        Me.lblSitu.TextValue = ""
        Me.lblSitu.UseSystemPasswordChar = False
        Me.lblSitu.WidthDef = 48
        Me.lblSitu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblZone
        '
        Me.lblZone.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZone.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZone.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZone.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblZone.CountWrappedLine = False
        Me.lblZone.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblZone.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZone.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZone.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblZone.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblZone.HeightDef = 18
        Me.lblZone.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblZone.HissuLabelVisible = False
        Me.lblZone.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblZone.IsByteCheck = 0
        Me.lblZone.IsCalendarCheck = False
        Me.lblZone.IsDakutenCheck = False
        Me.lblZone.IsEisuCheck = False
        Me.lblZone.IsForbiddenWordsCheck = False
        Me.lblZone.IsFullByteCheck = 0
        Me.lblZone.IsHankakuCheck = False
        Me.lblZone.IsHissuCheck = False
        Me.lblZone.IsKanaCheck = False
        Me.lblZone.IsMiddleSpace = False
        Me.lblZone.IsNumericCheck = False
        Me.lblZone.IsSujiCheck = False
        Me.lblZone.IsZenkakuCheck = False
        Me.lblZone.ItemName = ""
        Me.lblZone.LineSpace = 0
        Me.lblZone.Location = New System.Drawing.Point(104, 80)
        Me.lblZone.MaxLength = 0
        Me.lblZone.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblZone.MaxLineCount = 0
        Me.lblZone.Multiline = False
        Me.lblZone.Name = "lblZone"
        Me.lblZone.ReadOnly = True
        Me.lblZone.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblZone.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblZone.Size = New System.Drawing.Size(48, 18)
        Me.lblZone.TabIndex = 203
        Me.lblZone.TabStop = False
        Me.lblZone.TabStopSetting = False
        Me.lblZone.TextValue = ""
        Me.lblZone.UseSystemPasswordChar = False
        Me.lblZone.WidthDef = 48
        Me.lblZone.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblLoca
        '
        Me.lblLoca.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoca.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoca.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLoca.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLoca.CountWrappedLine = False
        Me.lblLoca.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblLoca.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLoca.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLoca.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoca.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoca.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblLoca.HeightDef = 18
        Me.lblLoca.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblLoca.HissuLabelVisible = False
        Me.lblLoca.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblLoca.IsByteCheck = 0
        Me.lblLoca.IsCalendarCheck = False
        Me.lblLoca.IsDakutenCheck = False
        Me.lblLoca.IsEisuCheck = False
        Me.lblLoca.IsForbiddenWordsCheck = False
        Me.lblLoca.IsFullByteCheck = 0
        Me.lblLoca.IsHankakuCheck = False
        Me.lblLoca.IsHissuCheck = False
        Me.lblLoca.IsKanaCheck = False
        Me.lblLoca.IsMiddleSpace = False
        Me.lblLoca.IsNumericCheck = False
        Me.lblLoca.IsSujiCheck = False
        Me.lblLoca.IsZenkakuCheck = False
        Me.lblLoca.ItemName = ""
        Me.lblLoca.LineSpace = 0
        Me.lblLoca.Location = New System.Drawing.Point(104, 101)
        Me.lblLoca.MaxLength = 0
        Me.lblLoca.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblLoca.MaxLineCount = 0
        Me.lblLoca.Multiline = False
        Me.lblLoca.Name = "lblLoca"
        Me.lblLoca.ReadOnly = True
        Me.lblLoca.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblLoca.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblLoca.Size = New System.Drawing.Size(115, 18)
        Me.lblLoca.TabIndex = 206
        Me.lblLoca.TabStop = False
        Me.lblLoca.TabStopSetting = False
        Me.lblLoca.TextValue = ""
        Me.lblLoca.UseSystemPasswordChar = False
        Me.lblLoca.WidthDef = 115
        Me.lblLoca.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleGoodsNm
        '
        Me.lblTitleGoodsNm.AutoSize = True
        Me.lblTitleGoodsNm.AutoSizeDef = True
        Me.lblTitleGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleGoodsNm.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleGoodsNm.EnableStatus = False
        Me.lblTitleGoodsNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleGoodsNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleGoodsNm.HeightDef = 13
        Me.lblTitleGoodsNm.Location = New System.Drawing.Point(52, 20)
        Me.lblTitleGoodsNm.Name = "lblTitleGoodsNm"
        Me.lblTitleGoodsNm.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleGoodsNm.TabIndex = 215
        Me.lblTitleGoodsNm.Text = "商品名"
        Me.lblTitleGoodsNm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleGoodsNm.TextValue = "商品名"
        Me.lblTitleGoodsNm.WidthDef = 49
        '
        'lblTitleLoca
        '
        Me.lblTitleLoca.AutoSize = True
        Me.lblTitleLoca.AutoSizeDef = True
        Me.lblTitleLoca.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoca.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLoca.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLoca.EnableStatus = False
        Me.lblTitleLoca.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoca.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLoca.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoca.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLoca.HeightDef = 13
        Me.lblTitleLoca.Location = New System.Drawing.Point(10, 104)
        Me.lblTitleLoca.Name = "lblTitleLoca"
        Me.lblTitleLoca.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleLoca.TabIndex = 219
        Me.lblTitleLoca.Text = "ロケーション"
        Me.lblTitleLoca.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLoca.TextValue = "ロケーション"
        Me.lblTitleLoca.WidthDef = 91
        '
        'lblTitleZone
        '
        Me.lblTitleZone.AutoSize = True
        Me.lblTitleZone.AutoSizeDef = True
        Me.lblTitleZone.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZone.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleZone.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleZone.EnableStatus = False
        Me.lblTitleZone.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZone.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleZone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZone.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleZone.HeightDef = 13
        Me.lblTitleZone.Location = New System.Drawing.Point(66, 83)
        Me.lblTitleZone.Name = "lblTitleZone"
        Me.lblTitleZone.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleZone.TabIndex = 218
        Me.lblTitleZone.Text = "ZONE"
        Me.lblTitleZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleZone.TextValue = "ZONE"
        Me.lblTitleZone.WidthDef = 35
        '
        'lblTitleSitu
        '
        Me.lblTitleSitu.AutoSize = True
        Me.lblTitleSitu.AutoSizeDef = True
        Me.lblTitleSitu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSitu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSitu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSitu.EnableStatus = False
        Me.lblTitleSitu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSitu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSitu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSitu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSitu.HeightDef = 13
        Me.lblTitleSitu.Location = New System.Drawing.Point(80, 62)
        Me.lblTitleSitu.Name = "lblTitleSitu"
        Me.lblTitleSitu.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleSitu.TabIndex = 217
        Me.lblTitleSitu.Text = "室"
        Me.lblTitleSitu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSitu.TextValue = "室"
        Me.lblTitleSitu.WidthDef = 21
        '
        'lblTitleTou
        '
        Me.lblTitleTou.AutoSize = True
        Me.lblTitleTou.AutoSizeDef = True
        Me.lblTitleTou.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleTou.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleTou.EnableStatus = False
        Me.lblTitleTou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleTou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleTou.HeightDef = 13
        Me.lblTitleTou.Location = New System.Drawing.Point(80, 41)
        Me.lblTitleTou.Name = "lblTitleTou"
        Me.lblTitleTou.Size = New System.Drawing.Size(21, 13)
        Me.lblTitleTou.TabIndex = 216
        Me.lblTitleTou.Text = "棟"
        Me.lblTitleTou.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleTou.TextValue = "棟"
        Me.lblTitleTou.WidthDef = 21
        '
        'btnJikkou
        '
        Me.btnJikkou.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnJikkou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnJikkou.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnJikkou.EnableStatus = True
        Me.btnJikkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJikkou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnJikkou.HeightDef = 22
        Me.btnJikkou.Location = New System.Drawing.Point(618, 11)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(70, 22)
        Me.btnJikkou.TabIndex = 506
        Me.btnJikkou.TabStop = False
        Me.btnJikkou.TabStopSetting = False
        Me.btnJikkou.Text = "実行"
        Me.btnJikkou.TextValue = "実行"
        Me.btnJikkou.UseVisualStyleBackColor = True
        Me.btnJikkou.WidthDef = 70
        '
        'cmbJikkou
        '
        Me.cmbJikkou.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJikkou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJikkou.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbJikkou.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbJikkou.DataCode = "J024"
        Me.cmbJikkou.DataSource = Nothing
        Me.cmbJikkou.DisplayMember = Nothing
        Me.cmbJikkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJikkou.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJikkou.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJikkou.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJikkou.HeightDef = 18
        Me.cmbJikkou.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJikkou.HissuLabelVisible = False
        Me.cmbJikkou.InsertWildCard = True
        Me.cmbJikkou.IsForbiddenWordsCheck = False
        Me.cmbJikkou.IsHissuCheck = False
        Me.cmbJikkou.ItemName = ""
        Me.cmbJikkou.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbJikkou.Location = New System.Drawing.Point(465, 13)
        Me.cmbJikkou.Name = "cmbJikkou"
        Me.cmbJikkou.ReadOnly = False
        Me.cmbJikkou.SelectedIndex = -1
        Me.cmbJikkou.SelectedItem = Nothing
        Me.cmbJikkou.SelectedText = ""
        Me.cmbJikkou.SelectedValue = ""
        Me.cmbJikkou.Size = New System.Drawing.Size(157, 18)
        Me.cmbJikkou.TabIndex = 507
        Me.cmbJikkou.TabStopSetting = True
        Me.cmbJikkou.TextValue = ""
        Me.cmbJikkou.Value1 = Nothing
        Me.cmbJikkou.Value2 = Nothing
        Me.cmbJikkou.Value3 = Nothing
        Me.cmbJikkou.ValueMember = Nothing
        Me.cmbJikkou.WidthDef = 157
        '
        'LME040F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LME040F"
        Me.Text = "【LME040】   作業指示書編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpSagyoSiji.ResumeLayout(False)
        Me.grpSagyoSiji.PerformLayout()
        CType(Me.sprDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSagyo.ResumeLayout(False)
        Me.grpSagyo.PerformLayout()
        Me.grpZaikoMeisai.ResumeLayout(False)
        Me.grpZaikoMeisai.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSagyoSiji As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents sprDetails As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents lblTitleDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdSagyoDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtRemark1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRemark1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemark3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRemark3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtRemark2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleRemark2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbIozsKb As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleIozsKb As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSokoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSokoCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtOyaSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleOyaSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyoSeiqtoCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyoSijiNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyoSijiNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSituation As Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitlePrint As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents btnRowDel As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnRowAdd As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents grpZaiko As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents grpSagyo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblSagyo1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyo2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyo3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyo4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSagyo5 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyo5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyo1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSagyo3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyo2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSagyo2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSagyo3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyo4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyo5 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleKeyNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtKeyNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents grpZaikoMeisai As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblGoodsNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTou As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblSitu As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblZone As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblLoca As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleGoodsNm As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleLoca As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleZone As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleSitu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleTou As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblLotNo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleLotNo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnJikkou As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbJikkou As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents txtSagyoRemark5 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoRemark4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoRemark3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoRemark2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtSagyoRemark1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblWHSagyoSintyoku As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbWHSagyoSintyoku As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbSagyoSijiStatus As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblSagyoStatus As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel

End Class
