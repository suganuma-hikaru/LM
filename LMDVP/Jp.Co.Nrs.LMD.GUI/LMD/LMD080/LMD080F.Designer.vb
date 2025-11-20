<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMD080F
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
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.pnlShogoTaisho = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbLayout = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.lblTitleLayout = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleJisshiDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.imdJisshiDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblCustNmM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNmL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCust = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlTorikomi = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnTorikomi = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblFile = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleFile = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnCheck = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitleFolder = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.pnlShukei = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.btnShukei = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblFolder = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlShogo = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.chkWriteFlg = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.pnlShogoKey = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.chkIrimeUt = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkIrime = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkSerialNo = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkLotNo = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkGoodsCdCust = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.btnShogo = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.txtFileTextBox = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdLOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdMOld = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.pnlViewAria.SuspendLayout()
        Me.pnlShogoTaisho.SuspendLayout()
        Me.pnlTorikomi.SuspendLayout()
        Me.pnlShukei.SuspendLayout()
        Me.pnlShogo.SuspendLayout()
        Me.pnlShogoKey.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtCustCdMOld)
        Me.pnlViewAria.Controls.Add(Me.txtCustCdLOld)
        Me.pnlViewAria.Controls.Add(Me.txtFileTextBox)
        Me.pnlViewAria.Controls.Add(Me.pnlShogo)
        Me.pnlViewAria.Controls.Add(Me.lblFolder)
        Me.pnlViewAria.Controls.Add(Me.pnlShukei)
        Me.pnlViewAria.Controls.Add(Me.pnlTorikomi)
        Me.pnlViewAria.Controls.Add(Me.pnlShogoTaisho)
        '
        'pnlShogoTaisho
        '
        Me.pnlShogoTaisho.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogoTaisho.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogoTaisho.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShogoTaisho.Controls.Add(Me.cmbLayout)
        Me.pnlShogoTaisho.Controls.Add(Me.lblTitleLayout)
        Me.pnlShogoTaisho.Controls.Add(Me.lblTitleJisshiDate)
        Me.pnlShogoTaisho.Controls.Add(Me.imdJisshiDate)
        Me.pnlShogoTaisho.Controls.Add(Me.cmbEigyo)
        Me.pnlShogoTaisho.Controls.Add(Me.lblCustNmM)
        Me.pnlShogoTaisho.Controls.Add(Me.lblCustNmL)
        Me.pnlShogoTaisho.Controls.Add(Me.lblTitleCust)
        Me.pnlShogoTaisho.Controls.Add(Me.txtCustCdM)
        Me.pnlShogoTaisho.Controls.Add(Me.txtCustCdL)
        Me.pnlShogoTaisho.Controls.Add(Me.lblTitleEigyo)
        Me.pnlShogoTaisho.EnableStatus = False
        Me.pnlShogoTaisho.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogoTaisho.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogoTaisho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogoTaisho.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogoTaisho.HeightDef = 121
        Me.pnlShogoTaisho.Location = New System.Drawing.Point(3, 19)
        Me.pnlShogoTaisho.Name = "pnlShogoTaisho"
        Me.pnlShogoTaisho.Size = New System.Drawing.Size(669, 121)
        Me.pnlShogoTaisho.TabIndex = 238
        Me.pnlShogoTaisho.TabStop = False
        Me.pnlShogoTaisho.Text = "照合対象"
        Me.pnlShogoTaisho.TextValue = "照合対象"
        Me.pnlShogoTaisho.WidthDef = 669
        '
        'cmbLayout
        '
        Me.cmbLayout.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLayout.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbLayout.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbLayout.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbLayout.DataSource = Nothing
        Me.cmbLayout.DisplayMember = Nothing
        Me.cmbLayout.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLayout.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbLayout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLayout.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbLayout.HeightDef = 18
        Me.cmbLayout.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbLayout.HissuLabelVisible = True
        Me.cmbLayout.InsertWildCard = True
        Me.cmbLayout.IsForbiddenWordsCheck = False
        Me.cmbLayout.IsHissuCheck = True
        Me.cmbLayout.ItemName = ""
        Me.cmbLayout.Location = New System.Drawing.Point(377, 88)
        Me.cmbLayout.Name = "cmbLayout"
        Me.cmbLayout.ReadOnly = False
        Me.cmbLayout.SelectedIndex = -1
        Me.cmbLayout.SelectedItem = Nothing
        Me.cmbLayout.SelectedText = ""
        Me.cmbLayout.SelectedValue = ""
        Me.cmbLayout.Size = New System.Drawing.Size(276, 18)
        Me.cmbLayout.TabIndex = 411
        Me.cmbLayout.TabStopSetting = True
        Me.cmbLayout.TextValue = ""
        Me.cmbLayout.ValueMember = Nothing
        Me.cmbLayout.WidthDef = 276
        '
        'lblTitleLayout
        '
        Me.lblTitleLayout.AutoSize = True
        Me.lblTitleLayout.AutoSizeDef = True
        Me.lblTitleLayout.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLayout.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleLayout.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleLayout.EnableStatus = False
        Me.lblTitleLayout.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLayout.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleLayout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLayout.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleLayout.HeightDef = 13
        Me.lblTitleLayout.Location = New System.Drawing.Point(241, 90)
        Me.lblTitleLayout.Name = "lblTitleLayout"
        Me.lblTitleLayout.Size = New System.Drawing.Size(133, 13)
        Me.lblTitleLayout.TabIndex = 410
        Me.lblTitleLayout.Text = "荷主在庫レイアウト"
        Me.lblTitleLayout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleLayout.TextValue = "荷主在庫レイアウト"
        Me.lblTitleLayout.WidthDef = 133
        '
        'lblTitleJisshiDate
        '
        Me.lblTitleJisshiDate.AutoSize = True
        Me.lblTitleJisshiDate.AutoSizeDef = True
        Me.lblTitleJisshiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJisshiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleJisshiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleJisshiDate.EnableStatus = False
        Me.lblTitleJisshiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJisshiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleJisshiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJisshiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleJisshiDate.HeightDef = 13
        Me.lblTitleJisshiDate.Location = New System.Drawing.Point(47, 90)
        Me.lblTitleJisshiDate.Name = "lblTitleJisshiDate"
        Me.lblTitleJisshiDate.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleJisshiDate.TabIndex = 253
        Me.lblTitleJisshiDate.Text = "実施日"
        Me.lblTitleJisshiDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleJisshiDate.TextValue = "実施日"
        Me.lblTitleJisshiDate.WidthDef = 49
        '
        'imdJisshiDate
        '
        Me.imdJisshiDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdJisshiDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdJisshiDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdJisshiDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdJisshiDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdJisshiDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdJisshiDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdJisshiDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdJisshiDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdJisshiDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdJisshiDate.HeightDef = 18
        Me.imdJisshiDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdJisshiDate.HissuLabelVisible = True
        Me.imdJisshiDate.Holiday = True
        Me.imdJisshiDate.IsAfterDateCheck = False
        Me.imdJisshiDate.IsBeforeDateCheck = False
        Me.imdJisshiDate.IsHissuCheck = True
        Me.imdJisshiDate.IsMinDateCheck = "1900/01/01"
        Me.imdJisshiDate.ItemName = ""
        Me.imdJisshiDate.Location = New System.Drawing.Point(99, 88)
        Me.imdJisshiDate.Name = "imdJisshiDate"
        Me.imdJisshiDate.Number = CType(10101000000, Long)
        Me.imdJisshiDate.ReadOnly = False
        Me.imdJisshiDate.Size = New System.Drawing.Size(118, 18)
        Me.imdJisshiDate.TabIndex = 252
        Me.imdJisshiDate.TabStopSetting = True
        Me.imdJisshiDate.TextValue = ""
        Me.imdJisshiDate.Value = New Date(CType(0, Long))
        Me.imdJisshiDate.WidthDef = 118
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
        Me.cmbEigyo.Location = New System.Drawing.Point(99, 25)
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
        Me.lblCustNmM.Location = New System.Drawing.Point(180, 67)
        Me.lblCustNmM.MaxLength = 0
        Me.lblCustNmM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmM.MaxLineCount = 0
        Me.lblCustNmM.Multiline = False
        Me.lblCustNmM.Name = "lblCustNmM"
        Me.lblCustNmM.ReadOnly = True
        Me.lblCustNmM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmM.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmM.TabIndex = 224
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
        Me.lblCustNmL.Location = New System.Drawing.Point(180, 46)
        Me.lblCustNmL.MaxLength = 0
        Me.lblCustNmL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNmL.MaxLineCount = 0
        Me.lblCustNmL.Multiline = False
        Me.lblCustNmL.Name = "lblCustNmL"
        Me.lblCustNmL.ReadOnly = True
        Me.lblCustNmL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNmL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNmL.Size = New System.Drawing.Size(473, 18)
        Me.lblCustNmL.TabIndex = 223
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
        Me.lblTitleCust.Location = New System.Drawing.Point(61, 49)
        Me.lblTitleCust.Name = "lblTitleCust"
        Me.lblTitleCust.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCust.TabIndex = 222
        Me.lblTitleCust.Text = "荷主"
        Me.lblTitleCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCust.TextValue = "荷主"
        Me.lblTitleCust.WidthDef = 35
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
        Me.txtCustCdM.Location = New System.Drawing.Point(144, 67)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdM.TabIndex = 221
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 52
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
        Me.txtCustCdL.Location = New System.Drawing.Point(99, 46)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdL.TabIndex = 220
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = ""
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 97
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(47, 28)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 219
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
        '
        'pnlTorikomi
        '
        Me.pnlTorikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlTorikomi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlTorikomi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlTorikomi.Controls.Add(Me.btnTorikomi)
        Me.pnlTorikomi.Controls.Add(Me.lblFile)
        Me.pnlTorikomi.Controls.Add(Me.lblTitleFile)
        Me.pnlTorikomi.Controls.Add(Me.btnCheck)
        Me.pnlTorikomi.Controls.Add(Me.lblTitleFolder)
        Me.pnlTorikomi.EnableStatus = False
        Me.pnlTorikomi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlTorikomi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlTorikomi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlTorikomi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlTorikomi.HeightDef = 134
        Me.pnlTorikomi.Location = New System.Drawing.Point(6, 146)
        Me.pnlTorikomi.Name = "pnlTorikomi"
        Me.pnlTorikomi.Size = New System.Drawing.Size(515, 134)
        Me.pnlTorikomi.TabIndex = 260
        Me.pnlTorikomi.TabStop = False
        Me.pnlTorikomi.Text = "荷主在庫取込"
        Me.pnlTorikomi.TextValue = "荷主在庫取込"
        Me.pnlTorikomi.WidthDef = 515
        '
        'btnTorikomi
        '
        Me.btnTorikomi.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnTorikomi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnTorikomi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnTorikomi.EnableStatus = True
        Me.btnTorikomi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTorikomi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTorikomi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTorikomi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnTorikomi.HeightDef = 22
        Me.btnTorikomi.Location = New System.Drawing.Point(8, 105)
        Me.btnTorikomi.Name = "btnTorikomi"
        Me.btnTorikomi.Size = New System.Drawing.Size(119, 22)
        Me.btnTorikomi.TabIndex = 263
        Me.btnTorikomi.TabStopSetting = True
        Me.btnTorikomi.Text = "荷主在庫取込"
        Me.btnTorikomi.TextValue = "荷主在庫取込"
        Me.btnTorikomi.UseVisualStyleBackColor = True
        Me.btnTorikomi.WidthDef = 119
        '
        'lblFile
        '
        Me.lblFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFile.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFile.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFile.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFile.CountWrappedLine = False
        Me.lblFile.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFile.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFile.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFile.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblFile.HeightDef = 18
        Me.lblFile.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFile.HissuLabelVisible = False
        Me.lblFile.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblFile.IsByteCheck = 0
        Me.lblFile.IsCalendarCheck = False
        Me.lblFile.IsDakutenCheck = False
        Me.lblFile.IsEisuCheck = False
        Me.lblFile.IsForbiddenWordsCheck = False
        Me.lblFile.IsFullByteCheck = 0
        Me.lblFile.IsHankakuCheck = False
        Me.lblFile.IsHissuCheck = False
        Me.lblFile.IsKanaCheck = False
        Me.lblFile.IsMiddleSpace = False
        Me.lblFile.IsNumericCheck = False
        Me.lblFile.IsSujiCheck = False
        Me.lblFile.IsZenkakuCheck = False
        Me.lblFile.ItemName = ""
        Me.lblFile.LineSpace = 0
        Me.lblFile.Location = New System.Drawing.Point(115, 81)
        Me.lblFile.MaxLength = 0
        Me.lblFile.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblFile.MaxLineCount = 0
        Me.lblFile.Multiline = False
        Me.lblFile.Name = "lblFile"
        Me.lblFile.ReadOnly = True
        Me.lblFile.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblFile.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblFile.Size = New System.Drawing.Size(396, 18)
        Me.lblFile.TabIndex = 261
        Me.lblFile.TabStop = False
        Me.lblFile.TabStopSetting = False
        Me.lblFile.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblFile.UseSystemPasswordChar = False
        Me.lblFile.WidthDef = 396
        Me.lblFile.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleFile
        '
        Me.lblTitleFile.AutoSize = True
        Me.lblTitleFile.AutoSizeDef = True
        Me.lblTitleFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFile.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFile.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFile.EnableStatus = False
        Me.lblTitleFile.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFile.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFile.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFile.HeightDef = 13
        Me.lblTitleFile.Location = New System.Drawing.Point(7, 83)
        Me.lblTitleFile.Name = "lblTitleFile"
        Me.lblTitleFile.Size = New System.Drawing.Size(105, 13)
        Me.lblTitleFile.TabIndex = 260
        Me.lblTitleFile.Text = "取込ファイル名"
        Me.lblTitleFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFile.TextValue = "取込ファイル名"
        Me.lblTitleFile.WidthDef = 105
        '
        'btnCheck
        '
        Me.btnCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCheck.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCheck.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnCheck.EnableStatus = True
        Me.btnCheck.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCheck.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCheck.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCheck.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCheck.HeightDef = 22
        Me.btnCheck.Location = New System.Drawing.Point(8, 23)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(119, 22)
        Me.btnCheck.TabIndex = 258
        Me.btnCheck.TabStopSetting = True
        Me.btnCheck.Text = "取込済チェック"
        Me.btnCheck.TextValue = "取込済チェック"
        Me.btnCheck.UseVisualStyleBackColor = True
        Me.btnCheck.WidthDef = 119
        '
        'lblTitleFolder
        '
        Me.lblTitleFolder.AutoSize = True
        Me.lblTitleFolder.AutoSizeDef = True
        Me.lblTitleFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFolder.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleFolder.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleFolder.EnableStatus = False
        Me.lblTitleFolder.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFolder.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFolder.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleFolder.HeightDef = 13
        Me.lblTitleFolder.Location = New System.Drawing.Point(21, 58)
        Me.lblTitleFolder.Name = "lblTitleFolder"
        Me.lblTitleFolder.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleFolder.TabIndex = 251
        Me.lblTitleFolder.Text = "取込フォルダ"
        Me.lblTitleFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleFolder.TextValue = "取込フォルダ"
        Me.lblTitleFolder.WidthDef = 91
        '
        'pnlShukei
        '
        Me.pnlShukei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShukei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShukei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShukei.Controls.Add(Me.btnShukei)
        Me.pnlShukei.EnableStatus = False
        Me.pnlShukei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShukei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShukei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShukei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShukei.HeightDef = 57
        Me.pnlShukei.Location = New System.Drawing.Point(537, 146)
        Me.pnlShukei.Name = "pnlShukei"
        Me.pnlShukei.Size = New System.Drawing.Size(135, 57)
        Me.pnlShukei.TabIndex = 262
        Me.pnlShukei.TabStop = False
        Me.pnlShukei.Text = "NRS在庫集計"
        Me.pnlShukei.TextValue = "NRS在庫集計"
        Me.pnlShukei.WidthDef = 135
        '
        'btnShukei
        '
        Me.btnShukei.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShukei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShukei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnShukei.EnableStatus = True
        Me.btnShukei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShukei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShukei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShukei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShukei.HeightDef = 22
        Me.btnShukei.Location = New System.Drawing.Point(8, 23)
        Me.btnShukei.Name = "btnShukei"
        Me.btnShukei.Size = New System.Drawing.Size(119, 22)
        Me.btnShukei.TabIndex = 259
        Me.btnShukei.TabStopSetting = True
        Me.btnShukei.Text = "NRS在庫集計"
        Me.btnShukei.TextValue = "NRS在庫集計"
        Me.btnShukei.UseVisualStyleBackColor = True
        Me.btnShukei.WidthDef = 119
        '
        'lblFolder
        '
        Me.lblFolder.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFolder.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFolder.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFolder.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFolder.CountWrappedLine = False
        Me.lblFolder.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblFolder.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFolder.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblFolder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFolder.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFolder.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblFolder.HeightDef = 18
        Me.lblFolder.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblFolder.HissuLabelVisible = False
        Me.lblFolder.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblFolder.IsByteCheck = 0
        Me.lblFolder.IsCalendarCheck = False
        Me.lblFolder.IsDakutenCheck = False
        Me.lblFolder.IsEisuCheck = False
        Me.lblFolder.IsForbiddenWordsCheck = False
        Me.lblFolder.IsFullByteCheck = 0
        Me.lblFolder.IsHankakuCheck = False
        Me.lblFolder.IsHissuCheck = False
        Me.lblFolder.IsKanaCheck = False
        Me.lblFolder.IsMiddleSpace = False
        Me.lblFolder.IsNumericCheck = False
        Me.lblFolder.IsSujiCheck = False
        Me.lblFolder.IsZenkakuCheck = False
        Me.lblFolder.ItemName = ""
        Me.lblFolder.LineSpace = 0
        Me.lblFolder.Location = New System.Drawing.Point(121, 201)
        Me.lblFolder.MaxLength = 0
        Me.lblFolder.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblFolder.MaxLineCount = 0
        Me.lblFolder.Multiline = False
        Me.lblFolder.Name = "lblFolder"
        Me.lblFolder.ReadOnly = True
        Me.lblFolder.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblFolder.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblFolder.Size = New System.Drawing.Size(396, 18)
        Me.lblFolder.TabIndex = 259
        Me.lblFolder.TabStop = False
        Me.lblFolder.TabStopSetting = False
        Me.lblFolder.TextValue = "Ｎ－－－１０－－－ＮＮ－－－１０－－－ＮＮ－－－１０－－－Ｎ"
        Me.lblFolder.UseSystemPasswordChar = False
        Me.lblFolder.WidthDef = 396
        Me.lblFolder.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'pnlShogo
        '
        Me.pnlShogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShogo.Controls.Add(Me.chkWriteFlg)
        Me.pnlShogo.Controls.Add(Me.pnlShogoKey)
        Me.pnlShogo.Controls.Add(Me.btnShogo)
        Me.pnlShogo.EnableStatus = False
        Me.pnlShogo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogo.HeightDef = 212
        Me.pnlShogo.Location = New System.Drawing.Point(537, 212)
        Me.pnlShogo.Name = "pnlShogo"
        Me.pnlShogo.Size = New System.Drawing.Size(317, 212)
        Me.pnlShogo.TabIndex = 263
        Me.pnlShogo.TabStop = False
        Me.pnlShogo.Text = "在庫数照合"
        Me.pnlShogo.TextValue = "在庫数照合"
        Me.pnlShogo.WidthDef = 317
        '
        'chkWriteFlg
        '
        Me.chkWriteFlg.AutoSize = True
        Me.chkWriteFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkWriteFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkWriteFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkWriteFlg.EnableStatus = True
        Me.chkWriteFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkWriteFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkWriteFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkWriteFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkWriteFlg.HeightDef = 17
        Me.chkWriteFlg.Location = New System.Drawing.Point(30, 159)
        Me.chkWriteFlg.Name = "chkWriteFlg"
        Me.chkWriteFlg.Size = New System.Drawing.Size(236, 17)
        Me.chkWriteFlg.TabIndex = 415
        Me.chkWriteFlg.TabStopSetting = True
        Me.chkWriteFlg.Text = "個数が一致した在庫は出力しない"
        Me.chkWriteFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkWriteFlg.TextValue = "個数が一致した在庫は出力しない"
        Me.chkWriteFlg.UseVisualStyleBackColor = True
        Me.chkWriteFlg.WidthDef = 236
        '
        'pnlShogoKey
        '
        Me.pnlShogoKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogoKey.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.pnlShogoKey.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.pnlShogoKey.Controls.Add(Me.chkIrimeUt)
        Me.pnlShogoKey.Controls.Add(Me.chkIrime)
        Me.pnlShogoKey.Controls.Add(Me.chkSerialNo)
        Me.pnlShogoKey.Controls.Add(Me.chkLotNo)
        Me.pnlShogoKey.Controls.Add(Me.chkGoodsCdCust)
        Me.pnlShogoKey.EnableStatus = False
        Me.pnlShogoKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogoKey.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.pnlShogoKey.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogoKey.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.pnlShogoKey.HeightDef = 123
        Me.pnlShogoKey.Location = New System.Drawing.Point(8, 29)
        Me.pnlShogoKey.Name = "pnlShogoKey"
        Me.pnlShogoKey.Size = New System.Drawing.Size(300, 123)
        Me.pnlShogoKey.TabIndex = 264
        Me.pnlShogoKey.TabStop = False
        Me.pnlShogoKey.Text = "照合キー"
        Me.pnlShogoKey.TextValue = "照合キー"
        Me.pnlShogoKey.WidthDef = 300
        '
        'chkIrimeUt
        '
        Me.chkIrimeUt.AutoSize = True
        Me.chkIrimeUt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkIrimeUt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkIrimeUt.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkIrimeUt.EnableStatus = True
        Me.chkIrimeUt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkIrimeUt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkIrimeUt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkIrimeUt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkIrimeUt.HeightDef = 17
        Me.chkIrimeUt.Location = New System.Drawing.Point(22, 98)
        Me.chkIrimeUt.Name = "chkIrimeUt"
        Me.chkIrimeUt.Size = New System.Drawing.Size(82, 17)
        Me.chkIrimeUt.TabIndex = 414
        Me.chkIrimeUt.TabStopSetting = True
        Me.chkIrimeUt.Text = "入目単位"
        Me.chkIrimeUt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkIrimeUt.TextValue = "入目単位"
        Me.chkIrimeUt.UseVisualStyleBackColor = True
        Me.chkIrimeUt.WidthDef = 82
        '
        'chkIrime
        '
        Me.chkIrime.AutoSize = True
        Me.chkIrime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkIrime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkIrime.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkIrime.EnableStatus = True
        Me.chkIrime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkIrime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkIrime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkIrime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkIrime.HeightDef = 17
        Me.chkIrime.Location = New System.Drawing.Point(22, 79)
        Me.chkIrime.Name = "chkIrime"
        Me.chkIrime.Size = New System.Drawing.Size(54, 17)
        Me.chkIrime.TabIndex = 413
        Me.chkIrime.TabStopSetting = True
        Me.chkIrime.Text = "入目"
        Me.chkIrime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkIrime.TextValue = "入目"
        Me.chkIrime.UseVisualStyleBackColor = True
        Me.chkIrime.WidthDef = 54
        '
        'chkSerialNo
        '
        Me.chkSerialNo.AutoSize = True
        Me.chkSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSerialNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSerialNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSerialNo.EnableStatus = True
        Me.chkSerialNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSerialNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSerialNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSerialNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSerialNo.HeightDef = 17
        Me.chkSerialNo.Location = New System.Drawing.Point(22, 60)
        Me.chkSerialNo.Name = "chkSerialNo"
        Me.chkSerialNo.Size = New System.Drawing.Size(96, 17)
        Me.chkSerialNo.TabIndex = 412
        Me.chkSerialNo.TabStopSetting = True
        Me.chkSerialNo.Text = "シリアル№"
        Me.chkSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSerialNo.TextValue = "シリアル№"
        Me.chkSerialNo.UseVisualStyleBackColor = True
        Me.chkSerialNo.WidthDef = 96
        '
        'chkLotNo
        '
        Me.chkLotNo.AutoSize = True
        Me.chkLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkLotNo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkLotNo.EnableStatus = True
        Me.chkLotNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkLotNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkLotNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkLotNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkLotNo.HeightDef = 17
        Me.chkLotNo.Location = New System.Drawing.Point(22, 41)
        Me.chkLotNo.Name = "chkLotNo"
        Me.chkLotNo.Size = New System.Drawing.Size(82, 17)
        Me.chkLotNo.TabIndex = 411
        Me.chkLotNo.TabStopSetting = True
        Me.chkLotNo.Text = "ロット№"
        Me.chkLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkLotNo.TextValue = "ロット№"
        Me.chkLotNo.UseVisualStyleBackColor = True
        Me.chkLotNo.WidthDef = 82
        '
        'chkGoodsCdCust
        '
        Me.chkGoodsCdCust.AutoSize = True
        Me.chkGoodsCdCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkGoodsCdCust.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkGoodsCdCust.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkGoodsCdCust.EnableStatus = True
        Me.chkGoodsCdCust.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkGoodsCdCust.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkGoodsCdCust.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkGoodsCdCust.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkGoodsCdCust.HeightDef = 17
        Me.chkGoodsCdCust.Location = New System.Drawing.Point(22, 22)
        Me.chkGoodsCdCust.Name = "chkGoodsCdCust"
        Me.chkGoodsCdCust.Size = New System.Drawing.Size(124, 17)
        Me.chkGoodsCdCust.TabIndex = 410
        Me.chkGoodsCdCust.TabStopSetting = True
        Me.chkGoodsCdCust.Text = "荷主商品コード"
        Me.chkGoodsCdCust.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkGoodsCdCust.TextValue = "荷主商品コード"
        Me.chkGoodsCdCust.UseVisualStyleBackColor = True
        Me.chkGoodsCdCust.WidthDef = 124
        '
        'btnShogo
        '
        Me.btnShogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShogo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShogo.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnShogo.EnableStatus = True
        Me.btnShogo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShogo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShogo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShogo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShogo.HeightDef = 22
        Me.btnShogo.Location = New System.Drawing.Point(8, 179)
        Me.btnShogo.Name = "btnShogo"
        Me.btnShogo.Size = New System.Drawing.Size(119, 22)
        Me.btnShogo.TabIndex = 259
        Me.btnShogo.TabStopSetting = True
        Me.btnShogo.Text = "在庫照合"
        Me.btnShogo.TextValue = "在庫照合"
        Me.btnShogo.UseVisualStyleBackColor = True
        Me.btnShogo.WidthDef = 119
        '
        'txtFileTextBox
        '
        Me.txtFileTextBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFileTextBox.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtFileTextBox.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileTextBox.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtFileTextBox.CountWrappedLine = False
        Me.txtFileTextBox.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtFileTextBox.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileTextBox.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFileTextBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileTextBox.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFileTextBox.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtFileTextBox.HeightDef = 18
        Me.txtFileTextBox.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtFileTextBox.HissuLabelVisible = False
        Me.txtFileTextBox.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtFileTextBox.IsByteCheck = 999
        Me.txtFileTextBox.IsCalendarCheck = False
        Me.txtFileTextBox.IsDakutenCheck = False
        Me.txtFileTextBox.IsEisuCheck = False
        Me.txtFileTextBox.IsForbiddenWordsCheck = False
        Me.txtFileTextBox.IsFullByteCheck = 0
        Me.txtFileTextBox.IsHankakuCheck = False
        Me.txtFileTextBox.IsHissuCheck = False
        Me.txtFileTextBox.IsKanaCheck = False
        Me.txtFileTextBox.IsMiddleSpace = False
        Me.txtFileTextBox.IsNumericCheck = False
        Me.txtFileTextBox.IsSujiCheck = False
        Me.txtFileTextBox.IsZenkakuCheck = False
        Me.txtFileTextBox.ItemName = ""
        Me.txtFileTextBox.LineSpace = 0
        Me.txtFileTextBox.Location = New System.Drawing.Point(3, 286)
        Me.txtFileTextBox.MaxLength = 999
        Me.txtFileTextBox.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtFileTextBox.MaxLineCount = 0
        Me.txtFileTextBox.Multiline = False
        Me.txtFileTextBox.Name = "txtFileTextBox"
        Me.txtFileTextBox.ReadOnly = False
        Me.txtFileTextBox.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtFileTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFileTextBox.Size = New System.Drawing.Size(65, 18)
        Me.txtFileTextBox.TabIndex = 264
        Me.txtFileTextBox.TabStopSetting = True
        Me.txtFileTextBox.TextValue = ""
        Me.txtFileTextBox.UseSystemPasswordChar = False
        Me.txtFileTextBox.Visible = False
        Me.txtFileTextBox.WidthDef = 65
        Me.txtFileTextBox.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdLOld
        '
        Me.txtCustCdLOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdLOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdLOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdLOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdLOld.CountWrappedLine = False
        Me.txtCustCdLOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdLOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdLOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdLOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdLOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdLOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdLOld.HeightDef = 18
        Me.txtCustCdLOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdLOld.HissuLabelVisible = False
        Me.txtCustCdLOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdLOld.IsByteCheck = 5
        Me.txtCustCdLOld.IsCalendarCheck = False
        Me.txtCustCdLOld.IsDakutenCheck = False
        Me.txtCustCdLOld.IsEisuCheck = False
        Me.txtCustCdLOld.IsForbiddenWordsCheck = False
        Me.txtCustCdLOld.IsFullByteCheck = 0
        Me.txtCustCdLOld.IsHankakuCheck = False
        Me.txtCustCdLOld.IsHissuCheck = False
        Me.txtCustCdLOld.IsKanaCheck = False
        Me.txtCustCdLOld.IsMiddleSpace = False
        Me.txtCustCdLOld.IsNumericCheck = False
        Me.txtCustCdLOld.IsSujiCheck = False
        Me.txtCustCdLOld.IsZenkakuCheck = False
        Me.txtCustCdLOld.ItemName = ""
        Me.txtCustCdLOld.LineSpace = 0
        Me.txtCustCdLOld.Location = New System.Drawing.Point(678, 63)
        Me.txtCustCdLOld.MaxLength = 5
        Me.txtCustCdLOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdLOld.MaxLineCount = 0
        Me.txtCustCdLOld.Multiline = False
        Me.txtCustCdLOld.Name = "txtCustCdLOld"
        Me.txtCustCdLOld.ReadOnly = False
        Me.txtCustCdLOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdLOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdLOld.Size = New System.Drawing.Size(97, 18)
        Me.txtCustCdLOld.TabIndex = 265
        Me.txtCustCdLOld.TabStopSetting = True
        Me.txtCustCdLOld.TextValue = ""
        Me.txtCustCdLOld.UseSystemPasswordChar = False
        Me.txtCustCdLOld.Visible = False
        Me.txtCustCdLOld.WidthDef = 97
        Me.txtCustCdLOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdMOld
        '
        Me.txtCustCdMOld.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdMOld.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdMOld.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdMOld.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdMOld.CountWrappedLine = False
        Me.txtCustCdMOld.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdMOld.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdMOld.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdMOld.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdMOld.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdMOld.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdMOld.HeightDef = 18
        Me.txtCustCdMOld.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdMOld.HissuLabelVisible = False
        Me.txtCustCdMOld.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdMOld.IsByteCheck = 2
        Me.txtCustCdMOld.IsCalendarCheck = False
        Me.txtCustCdMOld.IsDakutenCheck = False
        Me.txtCustCdMOld.IsEisuCheck = False
        Me.txtCustCdMOld.IsForbiddenWordsCheck = False
        Me.txtCustCdMOld.IsFullByteCheck = 0
        Me.txtCustCdMOld.IsHankakuCheck = False
        Me.txtCustCdMOld.IsHissuCheck = False
        Me.txtCustCdMOld.IsKanaCheck = False
        Me.txtCustCdMOld.IsMiddleSpace = False
        Me.txtCustCdMOld.IsNumericCheck = False
        Me.txtCustCdMOld.IsSujiCheck = False
        Me.txtCustCdMOld.IsZenkakuCheck = False
        Me.txtCustCdMOld.ItemName = ""
        Me.txtCustCdMOld.LineSpace = 0
        Me.txtCustCdMOld.Location = New System.Drawing.Point(678, 86)
        Me.txtCustCdMOld.MaxLength = 2
        Me.txtCustCdMOld.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdMOld.MaxLineCount = 0
        Me.txtCustCdMOld.Multiline = False
        Me.txtCustCdMOld.Name = "txtCustCdMOld"
        Me.txtCustCdMOld.ReadOnly = False
        Me.txtCustCdMOld.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdMOld.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdMOld.Size = New System.Drawing.Size(52, 18)
        Me.txtCustCdMOld.TabIndex = 266
        Me.txtCustCdMOld.TabStopSetting = True
        Me.txtCustCdMOld.TextValue = ""
        Me.txtCustCdMOld.UseSystemPasswordChar = False
        Me.txtCustCdMOld.Visible = False
        Me.txtCustCdMOld.WidthDef = 52
        Me.txtCustCdMOld.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMD080F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMD080F"
        Me.Text = "【LMD080】   荷主システム在庫数とNRS在庫数との照合"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlShogoTaisho.ResumeLayout(False)
        Me.pnlShogoTaisho.PerformLayout()
        Me.pnlTorikomi.ResumeLayout(False)
        Me.pnlTorikomi.PerformLayout()
        Me.pnlShukei.ResumeLayout(False)
        Me.pnlShogo.ResumeLayout(False)
        Me.pnlShogo.PerformLayout()
        Me.pnlShogoKey.ResumeLayout(False)
        Me.pnlShogoKey.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlShogoTaisho As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleCust As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblCustNmM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleJisshiDate As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents imdJisshiDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents pnlTorikomi As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnCheck As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitleFolder As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlShukei As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblFolder As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblFile As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleFile As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnTorikomi As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnShukei As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents pnlShogo As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents btnShogo As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents txtFileTextBox As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdMOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdLOld As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbLayout As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents lblTitleLayout As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents pnlShogoKey As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkWriteFlg As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkIrimeUt As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkIrime As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkSerialNo As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkLotNo As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkGoodsCdCust As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox

End Class
