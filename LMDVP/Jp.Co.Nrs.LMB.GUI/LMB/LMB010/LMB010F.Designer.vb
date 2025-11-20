<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMB010F
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
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim sprDetail_InputMapWhenFocusedNormal As FarPoint.Win.Spread.InputMap
        Dim sprDetail_InputMapWhenAncestorOfFocusedNormal As FarPoint.Win.Spread.InputMap
        Me.lblStatus = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpSTATUS = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.chkStaKenpin = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkStaYotei = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkStaPrint = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkStaUketsuke = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkStaNyuka = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkStaHoukoku = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtCustCD = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblCustNM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdNyukaDate_From = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdNyukaDate_To = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.grpUNSO = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.chkTranUnso = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkTranYoko = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.chkTranALL = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch()
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko()
        Me.grpInkaNoL = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.txtInkaNoL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblInkaNoL = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnJikkou = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.cmbJikkou = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbLabelTYpe = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.chkSelectByNrsB = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox()
        sprDetail_InputMapWhenFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal = New FarPoint.Win.Spread.InputMap()
        sprDetail_InputMapWhenAncestorOfFocusedNormal.Parent = New FarPoint.Win.Spread.InputMap()
        Me.pnlViewAria.SuspendLayout()
        Me.grpSTATUS.SuspendLayout()
        Me.grpUNSO.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInkaNoL.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.chkSelectByNrsB)
        Me.pnlViewAria.Controls.Add(Me.btnPrint)
        Me.pnlViewAria.Controls.Add(Me.cmbPrint)
        Me.pnlViewAria.Controls.Add(Me.cmbLabelTYpe)
        Me.pnlViewAria.Controls.Add(Me.btnJikkou)
        Me.pnlViewAria.Controls.Add(Me.cmbJikkou)
        Me.pnlViewAria.Controls.Add(Me.grpInkaNoL)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.lblCustNM)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.grpUNSO)
        Me.pnlViewAria.Controls.Add(Me.imdNyukaDate_To)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel5)
        Me.pnlViewAria.Controls.Add(Me.imdNyukaDate_From)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.txtCustCD)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel3)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.grpSTATUS)
        Me.pnlViewAria.Controls.Add(Me.lblStatus)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.cmbSoko)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.AutoSizeDef = True
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStatus.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblStatus.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblStatus.EnableStatus = False
        Me.lblStatus.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStatus.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblStatus.HeightDef = 13
        Me.lblStatus.Location = New System.Drawing.Point(23, 19)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(63, 13)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "進捗区分"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.TextValue = "進捗区分"
        Me.lblStatus.WidthDef = 63
        '
        'grpSTATUS
        '
        Me.grpSTATUS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSTATUS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSTATUS.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSTATUS.Controls.Add(Me.chkStaKenpin)
        Me.grpSTATUS.Controls.Add(Me.chkStaYotei)
        Me.grpSTATUS.Controls.Add(Me.chkStaPrint)
        Me.grpSTATUS.Controls.Add(Me.chkStaUketsuke)
        Me.grpSTATUS.Controls.Add(Me.chkStaNyuka)
        Me.grpSTATUS.Controls.Add(Me.chkStaHoukoku)
        Me.grpSTATUS.EnableStatus = False
        Me.grpSTATUS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSTATUS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSTATUS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSTATUS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSTATUS.HeightDef = 34
        Me.grpSTATUS.Location = New System.Drawing.Point(97, 6)
        Me.grpSTATUS.Name = "grpSTATUS"
        Me.grpSTATUS.Size = New System.Drawing.Size(579, 34)
        Me.grpSTATUS.TabIndex = 2
        Me.grpSTATUS.TabStop = False
        Me.grpSTATUS.TextValue = ""
        Me.grpSTATUS.WidthDef = 579
        '
        'chkStaKenpin
        '
        Me.chkStaKenpin.AutoSize = True
        Me.chkStaKenpin.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaKenpin.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaKenpin.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaKenpin.EnableStatus = True
        Me.chkStaKenpin.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaKenpin.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaKenpin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaKenpin.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaKenpin.HeightDef = 17
        Me.chkStaKenpin.Location = New System.Drawing.Point(294, 13)
        Me.chkStaKenpin.Name = "chkStaKenpin"
        Me.chkStaKenpin.Size = New System.Drawing.Size(68, 17)
        Me.chkStaKenpin.TabIndex = 3
        Me.chkStaKenpin.TabStopSetting = True
        Me.chkStaKenpin.Text = "検品済"
        Me.chkStaKenpin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaKenpin.TextValue = "検品済"
        Me.chkStaKenpin.UseVisualStyleBackColor = True
        Me.chkStaKenpin.WidthDef = 68
        '
        'chkStaYotei
        '
        Me.chkStaYotei.AutoSize = True
        Me.chkStaYotei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaYotei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaYotei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaYotei.EnableStatus = True
        Me.chkStaYotei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaYotei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaYotei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaYotei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaYotei.HeightDef = 17
        Me.chkStaYotei.Location = New System.Drawing.Point(6, 13)
        Me.chkStaYotei.Name = "chkStaYotei"
        Me.chkStaYotei.Size = New System.Drawing.Size(96, 17)
        Me.chkStaYotei.TabIndex = 3
        Me.chkStaYotei.TabStopSetting = True
        Me.chkStaYotei.Text = "予定入力済"
        Me.chkStaYotei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaYotei.TextValue = "予定入力済"
        Me.chkStaYotei.UseVisualStyleBackColor = True
        Me.chkStaYotei.WidthDef = 96
        '
        'chkStaPrint
        '
        Me.chkStaPrint.AutoSize = True
        Me.chkStaPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaPrint.EnableStatus = True
        Me.chkStaPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaPrint.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaPrint.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaPrint.HeightDef = 17
        Me.chkStaPrint.Location = New System.Drawing.Point(109, 13)
        Me.chkStaPrint.Name = "chkStaPrint"
        Me.chkStaPrint.Size = New System.Drawing.Size(96, 17)
        Me.chkStaPrint.TabIndex = 3
        Me.chkStaPrint.TabStopSetting = True
        Me.chkStaPrint.Text = "受付表印刷"
        Me.chkStaPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaPrint.TextValue = "受付表印刷"
        Me.chkStaPrint.UseVisualStyleBackColor = True
        Me.chkStaPrint.WidthDef = 96
        '
        'chkStaUketsuke
        '
        Me.chkStaUketsuke.AutoSize = True
        Me.chkStaUketsuke.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaUketsuke.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaUketsuke.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaUketsuke.EnableStatus = True
        Me.chkStaUketsuke.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaUketsuke.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaUketsuke.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaUketsuke.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaUketsuke.HeightDef = 17
        Me.chkStaUketsuke.Location = New System.Drawing.Point(213, 13)
        Me.chkStaUketsuke.Name = "chkStaUketsuke"
        Me.chkStaUketsuke.Size = New System.Drawing.Size(68, 17)
        Me.chkStaUketsuke.TabIndex = 3
        Me.chkStaUketsuke.TabStopSetting = True
        Me.chkStaUketsuke.Text = "受付済"
        Me.chkStaUketsuke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaUketsuke.TextValue = "受付済"
        Me.chkStaUketsuke.UseVisualStyleBackColor = True
        Me.chkStaUketsuke.WidthDef = 68
        '
        'chkStaNyuka
        '
        Me.chkStaNyuka.AutoSize = True
        Me.chkStaNyuka.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaNyuka.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaNyuka.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaNyuka.EnableStatus = True
        Me.chkStaNyuka.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaNyuka.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaNyuka.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaNyuka.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaNyuka.HeightDef = 17
        Me.chkStaNyuka.Location = New System.Drawing.Point(397, 13)
        Me.chkStaNyuka.Name = "chkStaNyuka"
        Me.chkStaNyuka.Size = New System.Drawing.Size(68, 17)
        Me.chkStaNyuka.TabIndex = 3
        Me.chkStaNyuka.TabStopSetting = True
        Me.chkStaNyuka.Text = "入荷済"
        Me.chkStaNyuka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaNyuka.TextValue = "入荷済"
        Me.chkStaNyuka.UseVisualStyleBackColor = True
        Me.chkStaNyuka.WidthDef = 68
        '
        'chkStaHoukoku
        '
        Me.chkStaHoukoku.AutoSize = True
        Me.chkStaHoukoku.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaHoukoku.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkStaHoukoku.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkStaHoukoku.EnableStatus = True
        Me.chkStaHoukoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaHoukoku.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkStaHoukoku.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaHoukoku.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkStaHoukoku.HeightDef = 17
        Me.chkStaHoukoku.Location = New System.Drawing.Point(487, 13)
        Me.chkStaHoukoku.Name = "chkStaHoukoku"
        Me.chkStaHoukoku.Size = New System.Drawing.Size(68, 17)
        Me.chkStaHoukoku.TabIndex = 3
        Me.chkStaHoukoku.TabStopSetting = True
        Me.chkStaHoukoku.Text = "報告済"
        Me.chkStaHoukoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkStaHoukoku.TextValue = "報告済"
        Me.chkStaHoukoku.UseVisualStyleBackColor = True
        Me.chkStaHoukoku.WidthDef = 68
        '
        'LmTitleLabel1
        '
        Me.LmTitleLabel1.AutoSize = True
        Me.LmTitleLabel1.AutoSizeDef = True
        Me.LmTitleLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel1.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel1.EnableStatus = False
        Me.LmTitleLabel1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel1.HeightDef = 13
        Me.LmTitleLabel1.Location = New System.Drawing.Point(748, 20)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel1.TabIndex = 3
        Me.LmTitleLabel1.Text = "営業所"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "営業所"
        Me.LmTitleLabel1.WidthDef = 49
        '
        'LmTitleLabel2
        '
        Me.LmTitleLabel2.AutoSize = True
        Me.LmTitleLabel2.AutoSizeDef = True
        Me.LmTitleLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel2.EnableStatus = False
        Me.LmTitleLabel2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel2.HeightDef = 13
        Me.LmTitleLabel2.Location = New System.Drawing.Point(762, 41)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel2.TabIndex = 3
        Me.LmTitleLabel2.Text = "倉庫"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "倉庫"
        Me.LmTitleLabel2.WidthDef = 35
        '
        'LmTitleLabel3
        '
        Me.LmTitleLabel3.AutoSize = True
        Me.LmTitleLabel3.AutoSizeDef = True
        Me.LmTitleLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel3.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel3.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel3.EnableStatus = False
        Me.LmTitleLabel3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel3.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel3.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel3.HeightDef = 13
        Me.LmTitleLabel3.Location = New System.Drawing.Point(51, 46)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel3.TabIndex = 5
        Me.LmTitleLabel3.Text = "荷主"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "荷主"
        Me.LmTitleLabel3.WidthDef = 35
        '
        'txtCustCD
        '
        Me.txtCustCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCD.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCD.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCD.CountWrappedLine = False
        Me.txtCustCD.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCD.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCD.HeightDef = 18
        Me.txtCustCD.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCD.HissuLabelVisible = False
        Me.txtCustCD.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCD.IsByteCheck = 5
        Me.txtCustCD.IsCalendarCheck = False
        Me.txtCustCD.IsDakutenCheck = False
        Me.txtCustCD.IsEisuCheck = False
        Me.txtCustCD.IsForbiddenWordsCheck = False
        Me.txtCustCD.IsFullByteCheck = 0
        Me.txtCustCD.IsHankakuCheck = False
        Me.txtCustCD.IsHissuCheck = False
        Me.txtCustCD.IsKanaCheck = False
        Me.txtCustCD.IsMiddleSpace = False
        Me.txtCustCD.IsNumericCheck = False
        Me.txtCustCD.IsSujiCheck = False
        Me.txtCustCD.IsZenkakuCheck = False
        Me.txtCustCD.ItemName = ""
        Me.txtCustCD.LineSpace = 0
        Me.txtCustCD.Location = New System.Drawing.Point(97, 46)
        Me.txtCustCD.MaxLength = 5
        Me.txtCustCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCD.MaxLineCount = 0
        Me.txtCustCD.Multiline = False
        Me.txtCustCD.Name = "txtCustCD"
        Me.txtCustCD.ReadOnly = False
        Me.txtCustCD.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCD.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCD.Size = New System.Drawing.Size(93, 18)
        Me.txtCustCD.TabIndex = 6
        Me.txtCustCD.TabStopSetting = True
        Me.txtCustCD.TextValue = ""
        Me.txtCustCD.UseSystemPasswordChar = False
        Me.txtCustCD.WidthDef = 93
        Me.txtCustCD.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblCustNM
        '
        Me.lblCustNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNM.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCustNM.CountWrappedLine = False
        Me.lblCustNM.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblCustNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustNM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCustNM.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblCustNM.HeightDef = 18
        Me.lblCustNM.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblCustNM.HissuLabelVisible = False
        Me.lblCustNM.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblCustNM.IsByteCheck = 0
        Me.lblCustNM.IsCalendarCheck = False
        Me.lblCustNM.IsDakutenCheck = False
        Me.lblCustNM.IsEisuCheck = False
        Me.lblCustNM.IsForbiddenWordsCheck = False
        Me.lblCustNM.IsFullByteCheck = 0
        Me.lblCustNM.IsHankakuCheck = False
        Me.lblCustNM.IsHissuCheck = False
        Me.lblCustNM.IsKanaCheck = False
        Me.lblCustNM.IsMiddleSpace = False
        Me.lblCustNM.IsNumericCheck = False
        Me.lblCustNM.IsSujiCheck = False
        Me.lblCustNM.IsZenkakuCheck = False
        Me.lblCustNM.ItemName = ""
        Me.lblCustNM.LineSpace = 0
        Me.lblCustNM.Location = New System.Drawing.Point(174, 46)
        Me.lblCustNM.MaxLength = 0
        Me.lblCustNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNM.MaxLineCount = 0
        Me.lblCustNM.Multiline = False
        Me.lblCustNM.Name = "lblCustNM"
        Me.lblCustNM.ReadOnly = True
        Me.lblCustNM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNM.Size = New System.Drawing.Size(516, 18)
        Me.lblCustNM.TabIndex = 7
        Me.lblCustNM.TabStop = False
        Me.lblCustNM.TabStopSetting = False
        Me.lblCustNM.TextValue = ""
        Me.lblCustNM.UseSystemPasswordChar = False
        Me.lblCustNM.WidthDef = 516
        Me.lblCustNM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSizeDef = False
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 18
        Me.LmTitleLabel4.Location = New System.Drawing.Point(37, 67)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(49, 18)
        Me.LmTitleLabel4.TabIndex = 8
        Me.LmTitleLabel4.Text = "入荷日"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "入荷日"
        Me.LmTitleLabel4.WidthDef = 49
        '
        'imdNyukaDate_From
        '
        Me.imdNyukaDate_From.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_From.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_From.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate_From.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdNyukaDate_From.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate_From.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_From.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_From.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_From.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_From.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate_From.HeightDef = 18
        Me.imdNyukaDate_From.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate_From.HissuLabelVisible = False
        Me.imdNyukaDate_From.Holiday = True
        Me.imdNyukaDate_From.IsAfterDateCheck = False
        Me.imdNyukaDate_From.IsBeforeDateCheck = False
        Me.imdNyukaDate_From.IsHissuCheck = False
        Me.imdNyukaDate_From.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate_From.ItemName = ""
        Me.imdNyukaDate_From.Location = New System.Drawing.Point(97, 68)
        Me.imdNyukaDate_From.Name = "imdNyukaDate_From"
        Me.imdNyukaDate_From.Number = CType(0, Long)
        Me.imdNyukaDate_From.ReadOnly = False
        Me.imdNyukaDate_From.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate_From.TabIndex = 9
        Me.imdNyukaDate_From.TabStopSetting = True
        Me.imdNyukaDate_From.TextValue = ""
        Me.imdNyukaDate_From.Value = New Date(CType(0, Long))
        Me.imdNyukaDate_From.WidthDef = 118
        '
        'LmTitleLabel5
        '
        Me.LmTitleLabel5.AutoSize = True
        Me.LmTitleLabel5.AutoSizeDef = True
        Me.LmTitleLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel5.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel5.EnableStatus = False
        Me.LmTitleLabel5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel5.HeightDef = 13
        Me.LmTitleLabel5.Location = New System.Drawing.Point(207, 73)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(21, 13)
        Me.LmTitleLabel5.TabIndex = 10
        Me.LmTitleLabel5.Text = "～"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "～"
        Me.LmTitleLabel5.WidthDef = 21
        '
        'imdNyukaDate_To
        '
        Me.imdNyukaDate_To.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_To.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdNyukaDate_To.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdNyukaDate_To.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdNyukaDate_To.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdNyukaDate_To.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_To.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdNyukaDate_To.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_To.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdNyukaDate_To.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdNyukaDate_To.HeightDef = 18
        Me.imdNyukaDate_To.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdNyukaDate_To.HissuLabelVisible = False
        Me.imdNyukaDate_To.Holiday = True
        Me.imdNyukaDate_To.IsAfterDateCheck = False
        Me.imdNyukaDate_To.IsBeforeDateCheck = False
        Me.imdNyukaDate_To.IsHissuCheck = False
        Me.imdNyukaDate_To.IsMinDateCheck = "1900/01/01"
        Me.imdNyukaDate_To.ItemName = ""
        Me.imdNyukaDate_To.Location = New System.Drawing.Point(233, 68)
        Me.imdNyukaDate_To.Name = "imdNyukaDate_To"
        Me.imdNyukaDate_To.Number = CType(0, Long)
        Me.imdNyukaDate_To.ReadOnly = False
        Me.imdNyukaDate_To.Size = New System.Drawing.Size(118, 18)
        Me.imdNyukaDate_To.TabIndex = 11
        Me.imdNyukaDate_To.TabStopSetting = True
        Me.imdNyukaDate_To.TextValue = ""
        Me.imdNyukaDate_To.Value = New Date(CType(0, Long))
        Me.imdNyukaDate_To.WidthDef = 118
        '
        'grpUNSO
        '
        Me.grpUNSO.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpUNSO.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpUNSO.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpUNSO.Controls.Add(Me.chkTranUnso)
        Me.grpUNSO.Controls.Add(Me.chkTranYoko)
        Me.grpUNSO.Controls.Add(Me.chkTranALL)
        Me.grpUNSO.EnableStatus = False
        Me.grpUNSO.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpUNSO.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpUNSO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpUNSO.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpUNSO.HeightDef = 34
        Me.grpUNSO.Location = New System.Drawing.Point(481, 64)
        Me.grpUNSO.Name = "grpUNSO"
        Me.grpUNSO.Size = New System.Drawing.Size(195, 34)
        Me.grpUNSO.TabIndex = 12
        Me.grpUNSO.TabStop = False
        Me.grpUNSO.TextValue = ""
        Me.grpUNSO.WidthDef = 195
        '
        'chkTranUnso
        '
        Me.chkTranUnso.AutoSize = True
        Me.chkTranUnso.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranUnso.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranUnso.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkTranUnso.EnableStatus = True
        Me.chkTranUnso.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranUnso.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranUnso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranUnso.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranUnso.HeightDef = 17
        Me.chkTranUnso.Location = New System.Drawing.Point(6, 13)
        Me.chkTranUnso.Name = "chkTranUnso"
        Me.chkTranUnso.Size = New System.Drawing.Size(54, 17)
        Me.chkTranUnso.TabIndex = 3
        Me.chkTranUnso.TabStopSetting = True
        Me.chkTranUnso.Text = "運送"
        Me.chkTranUnso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTranUnso.TextValue = "運送"
        Me.chkTranUnso.UseVisualStyleBackColor = True
        Me.chkTranUnso.WidthDef = 54
        '
        'chkTranYoko
        '
        Me.chkTranYoko.AutoSize = True
        Me.chkTranYoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranYoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranYoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkTranYoko.EnableStatus = True
        Me.chkTranYoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranYoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranYoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranYoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranYoko.HeightDef = 17
        Me.chkTranYoko.Location = New System.Drawing.Point(64, 13)
        Me.chkTranYoko.Name = "chkTranYoko"
        Me.chkTranYoko.Size = New System.Drawing.Size(68, 17)
        Me.chkTranYoko.TabIndex = 3
        Me.chkTranYoko.TabStopSetting = True
        Me.chkTranYoko.Text = "横持ち"
        Me.chkTranYoko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTranYoko.TextValue = "横持ち"
        Me.chkTranYoko.UseVisualStyleBackColor = True
        Me.chkTranYoko.WidthDef = 68
        '
        'chkTranALL
        '
        Me.chkTranALL.AutoSize = True
        Me.chkTranALL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranALL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkTranALL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkTranALL.EnableStatus = True
        Me.chkTranALL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranALL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTranALL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranALL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkTranALL.HeightDef = 17
        Me.chkTranALL.Location = New System.Drawing.Point(136, 13)
        Me.chkTranALL.Name = "chkTranALL"
        Me.chkTranALL.Size = New System.Drawing.Size(54, 17)
        Me.chkTranALL.TabIndex = 3
        Me.chkTranALL.TabStopSetting = True
        Me.chkTranALL.Text = "全部"
        Me.chkTranALL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkTranALL.TextValue = "全部"
        Me.chkTranALL.UseVisualStyleBackColor = True
        Me.chkTranALL.WidthDef = 54
        '
        'LmTitleLabel6
        '
        Me.LmTitleLabel6.AutoSize = True
        Me.LmTitleLabel6.AutoSizeDef = True
        Me.LmTitleLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel6.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel6.EnableStatus = False
        Me.LmTitleLabel6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel6.HeightDef = 13
        Me.LmTitleLabel6.Location = New System.Drawing.Point(440, 77)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel6.TabIndex = 13
        Me.LmTitleLabel6.Text = "運送"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "運送"
        Me.LmTitleLabel6.WidthDef = 35
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
        Me.sprDetail.HeightDef = 749
        Me.sprDetail.KeyboardCheckBoxOn = False
        Me.sprDetail.Location = New System.Drawing.Point(15, 115)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.Size = New System.Drawing.Size(1240, 749)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.UseGrouping = False
        Me.sprDetail.WidthDef = 1240
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
        Me.cmbEigyo.Location = New System.Drawing.Point(803, 17)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = False
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 16
        Me.cmbEigyo.TabStopSetting = True
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'cmbSoko
        '
        Me.cmbSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
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
        Me.cmbSoko.Location = New System.Drawing.Point(803, 38)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = False
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(300, 18)
        Me.cmbSoko.TabIndex = 17
        Me.cmbSoko.TabStopSetting = True
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 300
        '
        'grpInkaNoL
        '
        Me.grpInkaNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpInkaNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpInkaNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpInkaNoL.Controls.Add(Me.txtInkaNoL)
        Me.grpInkaNoL.Controls.Add(Me.lblInkaNoL)
        Me.grpInkaNoL.EnableStatus = False
        Me.grpInkaNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpInkaNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpInkaNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpInkaNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpInkaNoL.HeightDef = 45
        Me.grpInkaNoL.Location = New System.Drawing.Point(751, 64)
        Me.grpInkaNoL.Name = "grpInkaNoL"
        Me.grpInkaNoL.Size = New System.Drawing.Size(229, 45)
        Me.grpInkaNoL.TabIndex = 249
        Me.grpInkaNoL.TabStop = False
        Me.grpInkaNoL.Text = "ダイレクト検索"
        Me.grpInkaNoL.TextValue = "ダイレクト検索"
        Me.grpInkaNoL.WidthDef = 229
        '
        'txtInkaNoL
        '
        Me.txtInkaNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtInkaNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtInkaNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInkaNoL.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtInkaNoL.CountWrappedLine = False
        Me.txtInkaNoL.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtInkaNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaNoL.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtInkaNoL.HeightDef = 18
        Me.txtInkaNoL.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaNoL.HissuLabelVisible = False
        Me.txtInkaNoL.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtInkaNoL.IsByteCheck = 9
        Me.txtInkaNoL.IsCalendarCheck = False
        Me.txtInkaNoL.IsDakutenCheck = False
        Me.txtInkaNoL.IsEisuCheck = False
        Me.txtInkaNoL.IsForbiddenWordsCheck = False
        Me.txtInkaNoL.IsFullByteCheck = 0
        Me.txtInkaNoL.IsHankakuCheck = False
        Me.txtInkaNoL.IsHissuCheck = False
        Me.txtInkaNoL.IsKanaCheck = False
        Me.txtInkaNoL.IsMiddleSpace = False
        Me.txtInkaNoL.IsNumericCheck = False
        Me.txtInkaNoL.IsSujiCheck = False
        Me.txtInkaNoL.IsZenkakuCheck = False
        Me.txtInkaNoL.ItemName = ""
        Me.txtInkaNoL.LineSpace = 0
        Me.txtInkaNoL.Location = New System.Drawing.Point(129, 19)
        Me.txtInkaNoL.MaxLength = 9
        Me.txtInkaNoL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtInkaNoL.MaxLineCount = 0
        Me.txtInkaNoL.Multiline = False
        Me.txtInkaNoL.Name = "txtInkaNoL"
        Me.txtInkaNoL.ReadOnly = False
        Me.txtInkaNoL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtInkaNoL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtInkaNoL.Size = New System.Drawing.Size(97, 18)
        Me.txtInkaNoL.TabIndex = 64
        Me.txtInkaNoL.TabStopSetting = True
        Me.txtInkaNoL.TextValue = ""
        Me.txtInkaNoL.UseSystemPasswordChar = False
        Me.txtInkaNoL.WidthDef = 97
        Me.txtInkaNoL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblInkaNoL
        '
        Me.lblInkaNoL.AutoSize = True
        Me.lblInkaNoL.AutoSizeDef = True
        Me.lblInkaNoL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaNoL.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblInkaNoL.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblInkaNoL.EnableStatus = False
        Me.lblInkaNoL.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaNoL.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInkaNoL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaNoL.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblInkaNoL.HeightDef = 13
        Me.lblInkaNoL.Location = New System.Drawing.Point(8, 22)
        Me.lblInkaNoL.Name = "lblInkaNoL"
        Me.lblInkaNoL.Size = New System.Drawing.Size(119, 13)
        Me.lblInkaNoL.TabIndex = 63
        Me.lblInkaNoL.Text = "入荷管理番号(大)"
        Me.lblInkaNoL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblInkaNoL.TextValue = "入荷管理番号(大)"
        Me.lblInkaNoL.WidthDef = 119
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
        Me.btnJikkou.HeightDef = 24
        Me.btnJikkou.Location = New System.Drawing.Point(1167, 79)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(77, 24)
        Me.btnJikkou.TabIndex = 250
        Me.btnJikkou.TabStopSetting = True
        Me.btnJikkou.Text = "実行"
        Me.btnJikkou.TextValue = "実行"
        Me.btnJikkou.UseVisualStyleBackColor = True
        Me.btnJikkou.WidthDef = 77
        '
        'cmbJikkou
        '
        Me.cmbJikkou.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJikkou.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbJikkou.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbJikkou.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbJikkou.DataCode = "S074"
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
        Me.cmbJikkou.Location = New System.Drawing.Point(1002, 82)
        Me.cmbJikkou.Name = "cmbJikkou"
        Me.cmbJikkou.ReadOnly = False
        Me.cmbJikkou.SelectedIndex = -1
        Me.cmbJikkou.SelectedItem = Nothing
        Me.cmbJikkou.SelectedText = ""
        Me.cmbJikkou.SelectedValue = ""
        Me.cmbJikkou.Size = New System.Drawing.Size(172, 18)
        Me.cmbJikkou.TabIndex = 251
        Me.cmbJikkou.TabStopSetting = True
        Me.cmbJikkou.TextValue = ""
        Me.cmbJikkou.Value1 = ""
        Me.cmbJikkou.Value2 = Nothing
        Me.cmbJikkou.Value3 = Nothing
        Me.cmbJikkou.ValueMember = Nothing
        Me.cmbJikkou.WidthDef = 172
        '
        'cmbLabelTYpe
        '
        Me.cmbLabelTYpe.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLabelTYpe.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLabelTYpe.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbLabelTYpe.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbLabelTYpe.DataCode = "P024"
        Me.cmbLabelTYpe.DataSource = Nothing
        Me.cmbLabelTYpe.DisplayMember = Nothing
        Me.cmbLabelTYpe.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLabelTYpe.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLabelTYpe.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLabelTYpe.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLabelTYpe.HeightDef = 18
        Me.cmbLabelTYpe.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLabelTYpe.HissuLabelVisible = False
        Me.cmbLabelTYpe.InsertWildCard = True
        Me.cmbLabelTYpe.IsForbiddenWordsCheck = False
        Me.cmbLabelTYpe.IsHissuCheck = False
        Me.cmbLabelTYpe.ItemName = ""
        Me.cmbLabelTYpe.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbLabelTYpe.Location = New System.Drawing.Point(12, 92)
        Me.cmbLabelTYpe.Name = "cmbLabelTYpe"
        Me.cmbLabelTYpe.ReadOnly = False
        Me.cmbLabelTYpe.SelectedIndex = -1
        Me.cmbLabelTYpe.SelectedItem = Nothing
        Me.cmbLabelTYpe.SelectedText = ""
        Me.cmbLabelTYpe.SelectedValue = ""
        Me.cmbLabelTYpe.Size = New System.Drawing.Size(94, 18)
        Me.cmbLabelTYpe.TabIndex = 260
        Me.cmbLabelTYpe.TabStopSetting = True
        Me.cmbLabelTYpe.TextValue = ""
        Me.cmbLabelTYpe.Value1 = Nothing
        Me.cmbLabelTYpe.Value2 = Nothing
        Me.cmbLabelTYpe.Value3 = Nothing
        Me.cmbLabelTYpe.ValueMember = Nothing
        Me.cmbLabelTYpe.Visible = False
        Me.cmbLabelTYpe.WidthDef = 94
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "N015"
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
        Me.cmbPrint.Location = New System.Drawing.Point(97, 92)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(181, 18)
        Me.cmbPrint.TabIndex = 261
        Me.cmbPrint.TabStopSetting = True
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 181
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
        Me.btnPrint.HeightDef = 24
        Me.btnPrint.Location = New System.Drawing.Point(274, 88)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(77, 24)
        Me.btnPrint.TabIndex = 262
        Me.btnPrint.TabStopSetting = True
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.TextValue = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.WidthDef = 77
        '
        'chkSelectByNrsB
        '
        Me.chkSelectByNrsB.AutoSize = True
        Me.chkSelectByNrsB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSelectByNrsB.EnableStatus = True
        Me.chkSelectByNrsB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.HeightDef = 17
        Me.chkSelectByNrsB.Location = New System.Drawing.Point(1148, 20)
        Me.chkSelectByNrsB.Name = "chkSelectByNrsB"
        Me.chkSelectByNrsB.Size = New System.Drawing.Size(96, 17)
        Me.chkSelectByNrsB.TabIndex = 4
        Me.chkSelectByNrsB.TabStopSetting = True
        Me.chkSelectByNrsB.Text = "私の作成分"
        Me.chkSelectByNrsB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSelectByNrsB.TextValue = "私の作成分"
        Me.chkSelectByNrsB.UseVisualStyleBackColor = True
        Me.chkSelectByNrsB.WidthDef = 96
        '
        'LMB010F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMB010F"
        Me.Text = "【LMB010】 入荷データ検索"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpSTATUS.ResumeLayout(False)
        Me.grpSTATUS.PerformLayout()
        Me.grpUNSO.ResumeLayout(False)
        Me.grpUNSO.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInkaNoL.ResumeLayout(False)
        Me.grpInkaNoL.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imdNyukaDate_To As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel5 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdNyukaDate_From As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel4 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblCustNM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCD As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSTATUS As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkStaYotei As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkStaHoukoku As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkStaNyuka As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkStaPrint As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkStaUketsuke As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkStaKenpin As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblStatus As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmTitleLabel6 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpUNSO As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkTranUnso As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkTranYoko As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkTranALL As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
    Friend WithEvents grpInkaNoL As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents txtInkaNoL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblInkaNoL As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnJikkou As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents cmbJikkou As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents cmbLabelTYpe As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents chkSelectByNrsB As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox

End Class
