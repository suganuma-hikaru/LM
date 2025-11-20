<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMI150F
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
        Me.cmbEigyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr()
        Me.lblTitleEigyo2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel4 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtNrsProcNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.cmbProcType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel5 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbProcKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel6 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbJissekiFuyo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel7 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdProcDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.lblProcDate = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.btnZaikoSel = New Jp.Co.Nrs.LM.GUI.Win.LMButton()
        Me.lblGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel15 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsCdNrs = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel16 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtGoodsNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel17 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtLotNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.numNb = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber()
        Me.lblNb = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.imdLtDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate()
        Me.LmTitleLabel19 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel20 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtRemark = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.grpGoodsRank = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.cmbAfterGoodsRank = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.cmbBeforeGoodsRank = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.lblAfterGoodsRank = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpWh = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.cmbInkaWhType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.cmbOutkaWhType = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo()
        Me.LmTitleLabel23 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtInkaCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel24 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtInkaCustNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel25 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOutkaCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel21 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtOutkaCustNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblOutkaWhType = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.cmbJissekiShoriFlg = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun()
        Me.LmTitleLabel8 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.grpObic = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox()
        Me.LmTitleLabel13 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtObicDetailNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel12 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtObicGyoNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel11 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtObicDenpNo = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel10 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtObicTorihikiKbn = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.LmTitleLabel9 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel()
        Me.txtObicShubetu = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.lblSituation = New Jp.Co.Nrs.LM.GUI.Win.LMSituationLabel()
        Me.txtSysUpdDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.txtSysUpdTime = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox()
        Me.pnlViewAria.SuspendLayout()
        Me.grpGoodsRank.SuspendLayout()
        Me.grpWh.SuspendLayout()
        Me.grpObic.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.txtSysUpdTime)
        Me.pnlViewAria.Controls.Add(Me.txtSysUpdDate)
        Me.pnlViewAria.Controls.Add(Me.lblSituation)
        Me.pnlViewAria.Controls.Add(Me.grpObic)
        Me.pnlViewAria.Controls.Add(Me.cmbJissekiShoriFlg)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel8)
        Me.pnlViewAria.Controls.Add(Me.grpWh)
        Me.pnlViewAria.Controls.Add(Me.grpGoodsRank)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel20)
        Me.pnlViewAria.Controls.Add(Me.txtRemark)
        Me.pnlViewAria.Controls.Add(Me.imdLtDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel19)
        Me.pnlViewAria.Controls.Add(Me.lblNb)
        Me.pnlViewAria.Controls.Add(Me.numNb)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel17)
        Me.pnlViewAria.Controls.Add(Me.txtLotNo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel16)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsNm)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel15)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCdNrs)
        Me.pnlViewAria.Controls.Add(Me.lblGoodsCd)
        Me.pnlViewAria.Controls.Add(Me.txtGoodsCd)
        Me.pnlViewAria.Controls.Add(Me.btnZaikoSel)
        Me.pnlViewAria.Controls.Add(Me.imdProcDate)
        Me.pnlViewAria.Controls.Add(Me.lblProcDate)
        Me.pnlViewAria.Controls.Add(Me.cmbJissekiFuyo)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel7)
        Me.pnlViewAria.Controls.Add(Me.cmbProcKbn)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel6)
        Me.pnlViewAria.Controls.Add(Me.cmbProcType)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel5)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel4)
        Me.pnlViewAria.Controls.Add(Me.txtNrsProcNo)
        Me.pnlViewAria.Controls.Add(Me.cmbEigyo)
        Me.pnlViewAria.Controls.Add(Me.lblTitleEigyo2)
        Me.pnlViewAria.Size = New System.Drawing.Size(1274, 882)
        '
        'FunctionKey
        '
        Me.FunctionKey.F11ButtonName = "保存"
        Me.FunctionKey.F12ButtonName = "閉じる"
        Me.FunctionKey.F2ButtonName = "編集"
        Me.FunctionKey.Size = New System.Drawing.Size(1274, 40)
        Me.FunctionKey.WidthDef = 1274
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
        Me.cmbEigyo.Location = New System.Drawing.Point(126, 48)
        Me.cmbEigyo.Name = "cmbEigyo"
        Me.cmbEigyo.ReadOnly = True
        Me.cmbEigyo.SelectedIndex = -1
        Me.cmbEigyo.SelectedItem = Nothing
        Me.cmbEigyo.SelectedText = ""
        Me.cmbEigyo.SelectedValue = ""
        Me.cmbEigyo.Size = New System.Drawing.Size(300, 18)
        Me.cmbEigyo.TabIndex = 622
        Me.cmbEigyo.TabStop = False
        Me.cmbEigyo.TabStopSetting = False
        Me.cmbEigyo.TextValue = ""
        Me.cmbEigyo.ValueMember = Nothing
        Me.cmbEigyo.WidthDef = 300
        '
        'lblTitleEigyo2
        '
        Me.lblTitleEigyo2.AutoSize = True
        Me.lblTitleEigyo2.AutoSizeDef = True
        Me.lblTitleEigyo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo2.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleEigyo2.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleEigyo2.EnableStatus = False
        Me.lblTitleEigyo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo2.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleEigyo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo2.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleEigyo2.HeightDef = 13
        Me.lblTitleEigyo2.Location = New System.Drawing.Point(64, 51)
        Me.lblTitleEigyo2.Name = "lblTitleEigyo2"
        Me.lblTitleEigyo2.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo2.TabIndex = 621
        Me.lblTitleEigyo2.Text = "営業所"
        Me.lblTitleEigyo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo2.TextValue = "営業所"
        Me.lblTitleEigyo2.WidthDef = 49
        '
        'LmTitleLabel4
        '
        Me.LmTitleLabel4.AutoSize = True
        Me.LmTitleLabel4.AutoSizeDef = True
        Me.LmTitleLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel4.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel4.EnableStatus = False
        Me.LmTitleLabel4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel4.HeightDef = 13
        Me.LmTitleLabel4.Location = New System.Drawing.Point(29, 27)
        Me.LmTitleLabel4.Name = "LmTitleLabel4"
        Me.LmTitleLabel4.Size = New System.Drawing.Size(84, 13)
        Me.LmTitleLabel4.TabIndex = 649
        Me.LmTitleLabel4.Text = "NRS処理番号"
        Me.LmTitleLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel4.TextValue = "NRS処理番号"
        Me.LmTitleLabel4.WidthDef = 84
        '
        'txtNrsProcNo
        '
        Me.txtNrsProcNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNrsProcNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNrsProcNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNrsProcNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtNrsProcNo.CountWrappedLine = False
        Me.txtNrsProcNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtNrsProcNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsProcNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNrsProcNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsProcNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtNrsProcNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtNrsProcNo.HeightDef = 18
        Me.txtNrsProcNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtNrsProcNo.HissuLabelVisible = False
        Me.txtNrsProcNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtNrsProcNo.IsByteCheck = 300
        Me.txtNrsProcNo.IsCalendarCheck = False
        Me.txtNrsProcNo.IsDakutenCheck = False
        Me.txtNrsProcNo.IsEisuCheck = False
        Me.txtNrsProcNo.IsForbiddenWordsCheck = False
        Me.txtNrsProcNo.IsFullByteCheck = 0
        Me.txtNrsProcNo.IsHankakuCheck = False
        Me.txtNrsProcNo.IsHissuCheck = False
        Me.txtNrsProcNo.IsKanaCheck = False
        Me.txtNrsProcNo.IsMiddleSpace = False
        Me.txtNrsProcNo.IsNumericCheck = False
        Me.txtNrsProcNo.IsSujiCheck = False
        Me.txtNrsProcNo.IsZenkakuCheck = False
        Me.txtNrsProcNo.ItemName = ""
        Me.txtNrsProcNo.LineSpace = 0
        Me.txtNrsProcNo.Location = New System.Drawing.Point(126, 24)
        Me.txtNrsProcNo.MaxLength = 300
        Me.txtNrsProcNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtNrsProcNo.MaxLineCount = 0
        Me.txtNrsProcNo.Multiline = False
        Me.txtNrsProcNo.Name = "txtNrsProcNo"
        Me.txtNrsProcNo.ReadOnly = True
        Me.txtNrsProcNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtNrsProcNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNrsProcNo.Size = New System.Drawing.Size(113, 18)
        Me.txtNrsProcNo.TabIndex = 648
        Me.txtNrsProcNo.TabStop = False
        Me.txtNrsProcNo.TabStopSetting = False
        Me.txtNrsProcNo.Tag = ""
        Me.txtNrsProcNo.TextValue = ""
        Me.txtNrsProcNo.UseSystemPasswordChar = False
        Me.txtNrsProcNo.WidthDef = 113
        Me.txtNrsProcNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbProcType
        '
        Me.cmbProcType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbProcType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbProcType.DataCode = "H036"
        Me.cmbProcType.DataSource = Nothing
        Me.cmbProcType.DisplayMember = Nothing
        Me.cmbProcType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbProcType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbProcType.HeightDef = 18
        Me.cmbProcType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcType.HissuLabelVisible = False
        Me.cmbProcType.InsertWildCard = True
        Me.cmbProcType.IsForbiddenWordsCheck = False
        Me.cmbProcType.IsHissuCheck = False
        Me.cmbProcType.ItemName = ""
        Me.cmbProcType.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbProcType.Location = New System.Drawing.Point(126, 102)
        Me.cmbProcType.Name = "cmbProcType"
        Me.cmbProcType.ReadOnly = True
        Me.cmbProcType.SelectedIndex = -1
        Me.cmbProcType.SelectedItem = Nothing
        Me.cmbProcType.SelectedText = ""
        Me.cmbProcType.SelectedValue = ""
        Me.cmbProcType.Size = New System.Drawing.Size(163, 18)
        Me.cmbProcType.TabIndex = 651
        Me.cmbProcType.TabStop = False
        Me.cmbProcType.TabStopSetting = False
        Me.cmbProcType.TextValue = ""
        Me.cmbProcType.Value1 = Nothing
        Me.cmbProcType.Value2 = Nothing
        Me.cmbProcType.Value3 = Nothing
        Me.cmbProcType.ValueMember = Nothing
        Me.cmbProcType.WidthDef = 163
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
        Me.LmTitleLabel5.Location = New System.Drawing.Point(36, 105)
        Me.LmTitleLabel5.Name = "LmTitleLabel5"
        Me.LmTitleLabel5.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel5.TabIndex = 650
        Me.LmTitleLabel5.Text = "処理タイプ"
        Me.LmTitleLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel5.TextValue = "処理タイプ"
        Me.LmTitleLabel5.WidthDef = 77
        '
        'cmbProcKbn
        '
        Me.cmbProcKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbProcKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbProcKbn.DataCode = "H037"
        Me.cmbProcKbn.DataSource = Nothing
        Me.cmbProcKbn.DisplayMember = Nothing
        Me.cmbProcKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbProcKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbProcKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbProcKbn.HeightDef = 18
        Me.cmbProcKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbProcKbn.HissuLabelVisible = False
        Me.cmbProcKbn.InsertWildCard = True
        Me.cmbProcKbn.IsForbiddenWordsCheck = False
        Me.cmbProcKbn.IsHissuCheck = False
        Me.cmbProcKbn.ItemName = ""
        Me.cmbProcKbn.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbProcKbn.Location = New System.Drawing.Point(126, 126)
        Me.cmbProcKbn.Name = "cmbProcKbn"
        Me.cmbProcKbn.ReadOnly = True
        Me.cmbProcKbn.SelectedIndex = -1
        Me.cmbProcKbn.SelectedItem = Nothing
        Me.cmbProcKbn.SelectedText = ""
        Me.cmbProcKbn.SelectedValue = ""
        Me.cmbProcKbn.Size = New System.Drawing.Size(163, 18)
        Me.cmbProcKbn.TabIndex = 653
        Me.cmbProcKbn.TabStop = False
        Me.cmbProcKbn.TabStopSetting = False
        Me.cmbProcKbn.TextValue = ""
        Me.cmbProcKbn.Value1 = Nothing
        Me.cmbProcKbn.Value2 = Nothing
        Me.cmbProcKbn.Value3 = Nothing
        Me.cmbProcKbn.ValueMember = Nothing
        Me.cmbProcKbn.WidthDef = 163
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
        Me.LmTitleLabel6.Location = New System.Drawing.Point(50, 129)
        Me.LmTitleLabel6.Name = "LmTitleLabel6"
        Me.LmTitleLabel6.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel6.TabIndex = 652
        Me.LmTitleLabel6.Text = "処理区分"
        Me.LmTitleLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel6.TextValue = "処理区分"
        Me.LmTitleLabel6.WidthDef = 63
        '
        'cmbJissekiFuyo
        '
        Me.cmbJissekiFuyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiFuyo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiFuyo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbJissekiFuyo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbJissekiFuyo.DataCode = "WZ03"
        Me.cmbJissekiFuyo.DataSource = Nothing
        Me.cmbJissekiFuyo.DisplayMember = Nothing
        Me.cmbJissekiFuyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJissekiFuyo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJissekiFuyo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJissekiFuyo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJissekiFuyo.HeightDef = 18
        Me.cmbJissekiFuyo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiFuyo.HissuLabelVisible = False
        Me.cmbJissekiFuyo.InsertWildCard = True
        Me.cmbJissekiFuyo.IsForbiddenWordsCheck = False
        Me.cmbJissekiFuyo.IsHissuCheck = False
        Me.cmbJissekiFuyo.ItemName = ""
        Me.cmbJissekiFuyo.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbJissekiFuyo.Location = New System.Drawing.Point(662, 48)
        Me.cmbJissekiFuyo.Name = "cmbJissekiFuyo"
        Me.cmbJissekiFuyo.ReadOnly = True
        Me.cmbJissekiFuyo.SelectedIndex = -1
        Me.cmbJissekiFuyo.SelectedItem = Nothing
        Me.cmbJissekiFuyo.SelectedText = ""
        Me.cmbJissekiFuyo.SelectedValue = ""
        Me.cmbJissekiFuyo.Size = New System.Drawing.Size(132, 18)
        Me.cmbJissekiFuyo.TabIndex = 655
        Me.cmbJissekiFuyo.TabStop = False
        Me.cmbJissekiFuyo.TabStopSetting = False
        Me.cmbJissekiFuyo.TextValue = ""
        Me.cmbJissekiFuyo.Value1 = Nothing
        Me.cmbJissekiFuyo.Value2 = Nothing
        Me.cmbJissekiFuyo.Value3 = Nothing
        Me.cmbJissekiFuyo.ValueMember = Nothing
        Me.cmbJissekiFuyo.WidthDef = 132
        '
        'LmTitleLabel7
        '
        Me.LmTitleLabel7.AutoSize = True
        Me.LmTitleLabel7.AutoSizeDef = True
        Me.LmTitleLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel7.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel7.EnableStatus = False
        Me.LmTitleLabel7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel7.HeightDef = 13
        Me.LmTitleLabel7.Location = New System.Drawing.Point(586, 51)
        Me.LmTitleLabel7.Name = "LmTitleLabel7"
        Me.LmTitleLabel7.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel7.TabIndex = 654
        Me.LmTitleLabel7.Text = "実績要否"
        Me.LmTitleLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel7.TextValue = "実績要否"
        Me.LmTitleLabel7.WidthDef = 63
        '
        'imdProcDate
        '
        Me.imdProcDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdProcDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdProcDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdProcDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.imdProcDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField3, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField4, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdProcDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdProcDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdProcDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdProcDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdProcDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField2, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField2, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdProcDate.HeightDef = 18
        Me.imdProcDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdProcDate.HissuLabelVisible = False
        Me.imdProcDate.Holiday = True
        Me.imdProcDate.IsAfterDateCheck = False
        Me.imdProcDate.IsBeforeDateCheck = False
        Me.imdProcDate.IsHissuCheck = False
        Me.imdProcDate.IsMinDateCheck = "1900/01/01"
        Me.imdProcDate.ItemName = ""
        Me.imdProcDate.Location = New System.Drawing.Point(126, 150)
        Me.imdProcDate.Name = "imdProcDate"
        Me.imdProcDate.Number = CType(0, Long)
        Me.imdProcDate.ReadOnly = False
        Me.imdProcDate.Size = New System.Drawing.Size(118, 18)
        Me.imdProcDate.TabIndex = 658
        Me.imdProcDate.TabStopSetting = True
        Me.imdProcDate.TextValue = ""
        Me.imdProcDate.Value = New Date(CType(0, Long))
        Me.imdProcDate.WidthDef = 118
        '
        'lblProcDate
        '
        Me.lblProcDate.AutoSize = True
        Me.lblProcDate.AutoSizeDef = True
        Me.lblProcDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblProcDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblProcDate.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblProcDate.EnableStatus = False
        Me.lblProcDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblProcDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblProcDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProcDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProcDate.HeightDef = 13
        Me.lblProcDate.Location = New System.Drawing.Point(64, 153)
        Me.lblProcDate.Name = "lblProcDate"
        Me.lblProcDate.Size = New System.Drawing.Size(49, 13)
        Me.lblProcDate.TabIndex = 659
        Me.lblProcDate.Text = "処理日"
        Me.lblProcDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblProcDate.TextValue = "処理日"
        Me.lblProcDate.WidthDef = 49
        '
        'btnZaikoSel
        '
        Me.btnZaikoSel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnZaikoSel.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnZaikoSel.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnZaikoSel.EnableStatus = True
        Me.btnZaikoSel.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZaikoSel.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZaikoSel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnZaikoSel.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnZaikoSel.HeightDef = 22
        Me.btnZaikoSel.Location = New System.Drawing.Point(32, 419)
        Me.btnZaikoSel.Name = "btnZaikoSel"
        Me.btnZaikoSel.Size = New System.Drawing.Size(115, 22)
        Me.btnZaikoSel.TabIndex = 660
        Me.btnZaikoSel.TabStopSetting = True
        Me.btnZaikoSel.Text = "在庫選択"
        Me.btnZaikoSel.TextValue = "在庫選択"
        Me.btnZaikoSel.UseVisualStyleBackColor = True
        Me.btnZaikoSel.WidthDef = 115
        '
        'lblGoodsCd
        '
        Me.lblGoodsCd.AutoSize = True
        Me.lblGoodsCd.AutoSizeDef = True
        Me.lblGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblGoodsCd.EnableStatus = False
        Me.lblGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblGoodsCd.HeightDef = 13
        Me.lblGoodsCd.Location = New System.Drawing.Point(40, 460)
        Me.lblGoodsCd.Name = "lblGoodsCd"
        Me.lblGoodsCd.Size = New System.Drawing.Size(77, 13)
        Me.lblGoodsCd.TabIndex = 662
        Me.lblGoodsCd.Text = "顧客商品CD"
        Me.lblGoodsCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblGoodsCd.TextValue = "顧客商品CD"
        Me.lblGoodsCd.WidthDef = 77
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCd.CountWrappedLine = False
        Me.txtGoodsCd.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCd.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCd.HeightDef = 18
        Me.txtGoodsCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCd.HissuLabelVisible = False
        Me.txtGoodsCd.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCd.IsByteCheck = 300
        Me.txtGoodsCd.IsCalendarCheck = False
        Me.txtGoodsCd.IsDakutenCheck = False
        Me.txtGoodsCd.IsEisuCheck = False
        Me.txtGoodsCd.IsForbiddenWordsCheck = False
        Me.txtGoodsCd.IsFullByteCheck = 0
        Me.txtGoodsCd.IsHankakuCheck = False
        Me.txtGoodsCd.IsHissuCheck = False
        Me.txtGoodsCd.IsKanaCheck = False
        Me.txtGoodsCd.IsMiddleSpace = False
        Me.txtGoodsCd.IsNumericCheck = False
        Me.txtGoodsCd.IsSujiCheck = False
        Me.txtGoodsCd.IsZenkakuCheck = False
        Me.txtGoodsCd.ItemName = ""
        Me.txtGoodsCd.LineSpace = 0
        Me.txtGoodsCd.Location = New System.Drawing.Point(126, 458)
        Me.txtGoodsCd.MaxLength = 300
        Me.txtGoodsCd.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCd.MaxLineCount = 0
        Me.txtGoodsCd.Multiline = False
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.ReadOnly = True
        Me.txtGoodsCd.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCd.Size = New System.Drawing.Size(113, 18)
        Me.txtGoodsCd.TabIndex = 661
        Me.txtGoodsCd.TabStop = False
        Me.txtGoodsCd.TabStopSetting = False
        Me.txtGoodsCd.Tag = ""
        Me.txtGoodsCd.TextValue = ""
        Me.txtGoodsCd.UseSystemPasswordChar = False
        Me.txtGoodsCd.WidthDef = 113
        Me.txtGoodsCd.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel15
        '
        Me.LmTitleLabel15.AutoSize = True
        Me.LmTitleLabel15.AutoSizeDef = True
        Me.LmTitleLabel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel15.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel15.EnableStatus = False
        Me.LmTitleLabel15.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel15.HeightDef = 13
        Me.LmTitleLabel15.Location = New System.Drawing.Point(286, 460)
        Me.LmTitleLabel15.Name = "LmTitleLabel15"
        Me.LmTitleLabel15.Size = New System.Drawing.Size(70, 13)
        Me.LmTitleLabel15.TabIndex = 664
        Me.LmTitleLabel15.Text = "NRS商品CD"
        Me.LmTitleLabel15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel15.TextValue = "NRS商品CD"
        Me.LmTitleLabel15.WidthDef = 70
        '
        'txtGoodsCdNrs
        '
        Me.txtGoodsCdNrs.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdNrs.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdNrs.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsCdNrs.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtGoodsCdNrs.CountWrappedLine = False
        Me.txtGoodsCdNrs.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtGoodsCdNrs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdNrs.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtGoodsCdNrs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdNrs.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtGoodsCdNrs.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtGoodsCdNrs.HeightDef = 18
        Me.txtGoodsCdNrs.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsCdNrs.HissuLabelVisible = False
        Me.txtGoodsCdNrs.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsCdNrs.IsByteCheck = 300
        Me.txtGoodsCdNrs.IsCalendarCheck = False
        Me.txtGoodsCdNrs.IsDakutenCheck = False
        Me.txtGoodsCdNrs.IsEisuCheck = False
        Me.txtGoodsCdNrs.IsForbiddenWordsCheck = False
        Me.txtGoodsCdNrs.IsFullByteCheck = 0
        Me.txtGoodsCdNrs.IsHankakuCheck = False
        Me.txtGoodsCdNrs.IsHissuCheck = False
        Me.txtGoodsCdNrs.IsKanaCheck = False
        Me.txtGoodsCdNrs.IsMiddleSpace = False
        Me.txtGoodsCdNrs.IsNumericCheck = False
        Me.txtGoodsCdNrs.IsSujiCheck = False
        Me.txtGoodsCdNrs.IsZenkakuCheck = False
        Me.txtGoodsCdNrs.ItemName = ""
        Me.txtGoodsCdNrs.LineSpace = 0
        Me.txtGoodsCdNrs.Location = New System.Drawing.Point(365, 458)
        Me.txtGoodsCdNrs.MaxLength = 300
        Me.txtGoodsCdNrs.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsCdNrs.MaxLineCount = 0
        Me.txtGoodsCdNrs.Multiline = False
        Me.txtGoodsCdNrs.Name = "txtGoodsCdNrs"
        Me.txtGoodsCdNrs.ReadOnly = True
        Me.txtGoodsCdNrs.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsCdNrs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsCdNrs.Size = New System.Drawing.Size(285, 18)
        Me.txtGoodsCdNrs.TabIndex = 663
        Me.txtGoodsCdNrs.TabStop = False
        Me.txtGoodsCdNrs.TabStopSetting = False
        Me.txtGoodsCdNrs.Tag = ""
        Me.txtGoodsCdNrs.TextValue = ""
        Me.txtGoodsCdNrs.UseSystemPasswordChar = False
        Me.txtGoodsCdNrs.WidthDef = 285
        Me.txtGoodsCdNrs.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel16
        '
        Me.LmTitleLabel16.AutoSize = True
        Me.LmTitleLabel16.AutoSizeDef = True
        Me.LmTitleLabel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel16.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel16.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel16.EnableStatus = False
        Me.LmTitleLabel16.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel16.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel16.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel16.HeightDef = 13
        Me.LmTitleLabel16.Location = New System.Drawing.Point(68, 485)
        Me.LmTitleLabel16.Name = "LmTitleLabel16"
        Me.LmTitleLabel16.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel16.TabIndex = 666
        Me.LmTitleLabel16.Text = "商品名"
        Me.LmTitleLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel16.TextValue = "商品名"
        Me.LmTitleLabel16.WidthDef = 49
        '
        'txtGoodsNm
        '
        Me.txtGoodsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtGoodsNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtGoodsNm.HissuLabelVisible = False
        Me.txtGoodsNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtGoodsNm.IsByteCheck = 300
        Me.txtGoodsNm.IsCalendarCheck = False
        Me.txtGoodsNm.IsDakutenCheck = False
        Me.txtGoodsNm.IsEisuCheck = False
        Me.txtGoodsNm.IsForbiddenWordsCheck = False
        Me.txtGoodsNm.IsFullByteCheck = 0
        Me.txtGoodsNm.IsHankakuCheck = False
        Me.txtGoodsNm.IsHissuCheck = False
        Me.txtGoodsNm.IsKanaCheck = False
        Me.txtGoodsNm.IsMiddleSpace = False
        Me.txtGoodsNm.IsNumericCheck = False
        Me.txtGoodsNm.IsSujiCheck = False
        Me.txtGoodsNm.IsZenkakuCheck = False
        Me.txtGoodsNm.ItemName = ""
        Me.txtGoodsNm.LineSpace = 0
        Me.txtGoodsNm.Location = New System.Drawing.Point(126, 482)
        Me.txtGoodsNm.MaxLength = 300
        Me.txtGoodsNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtGoodsNm.MaxLineCount = 0
        Me.txtGoodsNm.Multiline = False
        Me.txtGoodsNm.Name = "txtGoodsNm"
        Me.txtGoodsNm.ReadOnly = True
        Me.txtGoodsNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtGoodsNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtGoodsNm.Size = New System.Drawing.Size(524, 18)
        Me.txtGoodsNm.TabIndex = 665
        Me.txtGoodsNm.TabStop = False
        Me.txtGoodsNm.TabStopSetting = False
        Me.txtGoodsNm.Tag = ""
        Me.txtGoodsNm.TextValue = ""
        Me.txtGoodsNm.UseSystemPasswordChar = False
        Me.txtGoodsNm.WidthDef = 524
        Me.txtGoodsNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel17
        '
        Me.LmTitleLabel17.AutoSize = True
        Me.LmTitleLabel17.AutoSizeDef = True
        Me.LmTitleLabel17.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel17.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel17.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel17.EnableStatus = False
        Me.LmTitleLabel17.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel17.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel17.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel17.HeightDef = 13
        Me.LmTitleLabel17.Location = New System.Drawing.Point(672, 485)
        Me.LmTitleLabel17.Name = "LmTitleLabel17"
        Me.LmTitleLabel17.Size = New System.Drawing.Size(28, 13)
        Me.LmTitleLabel17.TabIndex = 668
        Me.LmTitleLabel17.Text = "LOT"
        Me.LmTitleLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel17.TextValue = "LOT"
        Me.LmTitleLabel17.WidthDef = 28
        '
        'txtLotNo
        '
        Me.txtLotNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtLotNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
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
        Me.txtLotNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtLotNo.IsByteCheck = 300
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
        Me.txtLotNo.Location = New System.Drawing.Point(708, 482)
        Me.txtLotNo.MaxLength = 300
        Me.txtLotNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtLotNo.MaxLineCount = 0
        Me.txtLotNo.Multiline = False
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ReadOnly = True
        Me.txtLotNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtLotNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtLotNo.Size = New System.Drawing.Size(113, 18)
        Me.txtLotNo.TabIndex = 667
        Me.txtLotNo.TabStop = False
        Me.txtLotNo.TabStopSetting = False
        Me.txtLotNo.Tag = ""
        Me.txtLotNo.TextValue = ""
        Me.txtLotNo.UseSystemPasswordChar = False
        Me.txtLotNo.WidthDef = 113
        Me.txtLotNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'numNb
        '
        Me.numNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNb.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numNb.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numNb.HeightDef = 18
        Me.numNb.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numNb.HissuLabelVisible = False
        Me.numNb.IsHissuCheck = False
        Me.numNb.IsRangeCheck = False
        Me.numNb.ItemName = ""
        Me.numNb.Location = New System.Drawing.Point(126, 506)
        Me.numNb.Name = "numNb"
        Me.numNb.ReadOnly = True
        Me.numNb.Size = New System.Drawing.Size(135, 18)
        Me.numNb.TabIndex = 669
        Me.numNb.TabStop = False
        Me.numNb.TabStopSetting = False
        Me.numNb.TextValue = "0"
        Me.numNb.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numNb.WidthDef = 135
        '
        'lblNb
        '
        Me.lblNb.AutoSize = True
        Me.lblNb.AutoSizeDef = True
        Me.lblNb.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNb.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblNb.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblNb.EnableStatus = False
        Me.lblNb.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNb.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblNb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNb.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNb.HeightDef = 13
        Me.lblNb.Location = New System.Drawing.Point(82, 509)
        Me.lblNb.Name = "lblNb"
        Me.lblNb.Size = New System.Drawing.Size(35, 13)
        Me.lblNb.TabIndex = 670
        Me.lblNb.Text = "個数"
        Me.lblNb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNb.TextValue = "個数"
        Me.lblNb.WidthDef = 35
        '
        'imdLtDate
        '
        Me.imdLtDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdLtDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdLtDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdLtDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdLtDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdLtDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdLtDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdLtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdLtDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdLtDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdLtDate.HeightDef = 18
        Me.imdLtDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdLtDate.HissuLabelVisible = False
        Me.imdLtDate.Holiday = True
        Me.imdLtDate.IsAfterDateCheck = False
        Me.imdLtDate.IsBeforeDateCheck = False
        Me.imdLtDate.IsHissuCheck = False
        Me.imdLtDate.IsMinDateCheck = "1900/01/01"
        Me.imdLtDate.ItemName = ""
        Me.imdLtDate.Location = New System.Drawing.Point(427, 506)
        Me.imdLtDate.Name = "imdLtDate"
        Me.imdLtDate.Number = CType(0, Long)
        Me.imdLtDate.ReadOnly = True
        Me.imdLtDate.Size = New System.Drawing.Size(118, 18)
        Me.imdLtDate.TabIndex = 671
        Me.imdLtDate.TabStop = False
        Me.imdLtDate.TabStopSetting = False
        Me.imdLtDate.TextValue = ""
        Me.imdLtDate.Value = New Date(CType(0, Long))
        Me.imdLtDate.WidthDef = 118
        '
        'LmTitleLabel19
        '
        Me.LmTitleLabel19.AutoSize = True
        Me.LmTitleLabel19.AutoSizeDef = True
        Me.LmTitleLabel19.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel19.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel19.EnableStatus = False
        Me.LmTitleLabel19.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel19.HeightDef = 13
        Me.LmTitleLabel19.Location = New System.Drawing.Point(360, 509)
        Me.LmTitleLabel19.Name = "LmTitleLabel19"
        Me.LmTitleLabel19.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel19.TabIndex = 672
        Me.LmTitleLabel19.Text = "使用期限"
        Me.LmTitleLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel19.TextValue = "使用期限"
        Me.LmTitleLabel19.WidthDef = 63
        '
        'LmTitleLabel20
        '
        Me.LmTitleLabel20.AutoSize = True
        Me.LmTitleLabel20.AutoSizeDef = True
        Me.LmTitleLabel20.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel20.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel20.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel20.EnableStatus = False
        Me.LmTitleLabel20.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel20.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel20.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel20.HeightDef = 13
        Me.LmTitleLabel20.Location = New System.Drawing.Point(54, 533)
        Me.LmTitleLabel20.Name = "LmTitleLabel20"
        Me.LmTitleLabel20.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel20.TabIndex = 674
        Me.LmTitleLabel20.Text = "明細摘要"
        Me.LmTitleLabel20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel20.TextValue = "明細摘要"
        Me.LmTitleLabel20.WidthDef = 63
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtRemark.CountWrappedLine = False
        Me.txtRemark.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtRemark.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtRemark.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtRemark.HeightDef = 18
        Me.txtRemark.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtRemark.HissuLabelVisible = False
        Me.txtRemark.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtRemark.IsByteCheck = 300
        Me.txtRemark.IsCalendarCheck = False
        Me.txtRemark.IsDakutenCheck = False
        Me.txtRemark.IsEisuCheck = False
        Me.txtRemark.IsForbiddenWordsCheck = False
        Me.txtRemark.IsFullByteCheck = 0
        Me.txtRemark.IsHankakuCheck = False
        Me.txtRemark.IsHissuCheck = False
        Me.txtRemark.IsKanaCheck = False
        Me.txtRemark.IsMiddleSpace = False
        Me.txtRemark.IsNumericCheck = False
        Me.txtRemark.IsSujiCheck = False
        Me.txtRemark.IsZenkakuCheck = False
        Me.txtRemark.ItemName = ""
        Me.txtRemark.LineSpace = 0
        Me.txtRemark.Location = New System.Drawing.Point(126, 530)
        Me.txtRemark.MaxLength = 300
        Me.txtRemark.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtRemark.MaxLineCount = 0
        Me.txtRemark.Multiline = False
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = True
        Me.txtRemark.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemark.Size = New System.Drawing.Size(524, 18)
        Me.txtRemark.TabIndex = 673
        Me.txtRemark.TabStop = False
        Me.txtRemark.TabStopSetting = False
        Me.txtRemark.Tag = ""
        Me.txtRemark.TextValue = ""
        Me.txtRemark.UseSystemPasswordChar = False
        Me.txtRemark.WidthDef = 524
        Me.txtRemark.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'grpGoodsRank
        '
        Me.grpGoodsRank.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpGoodsRank.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpGoodsRank.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpGoodsRank.Controls.Add(Me.cmbAfterGoodsRank)
        Me.grpGoodsRank.Controls.Add(Me.cmbBeforeGoodsRank)
        Me.grpGoodsRank.Controls.Add(Me.lblAfterGoodsRank)
        Me.grpGoodsRank.Controls.Add(Me.LmTitleLabel3)
        Me.grpGoodsRank.EnableStatus = False
        Me.grpGoodsRank.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpGoodsRank.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpGoodsRank.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpGoodsRank.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpGoodsRank.HeightDef = 75
        Me.grpGoodsRank.Location = New System.Drawing.Point(39, 312)
        Me.grpGoodsRank.Name = "grpGoodsRank"
        Me.grpGoodsRank.Size = New System.Drawing.Size(643, 75)
        Me.grpGoodsRank.TabIndex = 675
        Me.grpGoodsRank.TabStop = False
        Me.grpGoodsRank.Text = "ステータス変更"
        Me.grpGoodsRank.TextValue = "ステータス変更"
        Me.grpGoodsRank.WidthDef = 643
        '
        'cmbAfterGoodsRank
        '
        Me.cmbAfterGoodsRank.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAfterGoodsRank.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbAfterGoodsRank.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbAfterGoodsRank.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbAfterGoodsRank.DataSource = Nothing
        Me.cmbAfterGoodsRank.DisplayMember = Nothing
        Me.cmbAfterGoodsRank.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAfterGoodsRank.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbAfterGoodsRank.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAfterGoodsRank.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbAfterGoodsRank.HeightDef = 18
        Me.cmbAfterGoodsRank.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbAfterGoodsRank.HissuLabelVisible = False
        Me.cmbAfterGoodsRank.InsertWildCard = True
        Me.cmbAfterGoodsRank.IsForbiddenWordsCheck = False
        Me.cmbAfterGoodsRank.IsHissuCheck = False
        Me.cmbAfterGoodsRank.ItemName = ""
        Me.cmbAfterGoodsRank.Location = New System.Drawing.Point(486, 31)
        Me.cmbAfterGoodsRank.Name = "cmbAfterGoodsRank"
        Me.cmbAfterGoodsRank.ReadOnly = False
        Me.cmbAfterGoodsRank.SelectedIndex = -1
        Me.cmbAfterGoodsRank.SelectedItem = Nothing
        Me.cmbAfterGoodsRank.SelectedText = ""
        Me.cmbAfterGoodsRank.SelectedValue = ""
        Me.cmbAfterGoodsRank.Size = New System.Drawing.Size(156, 18)
        Me.cmbAfterGoodsRank.TabIndex = 666
        Me.cmbAfterGoodsRank.TabStopSetting = True
        Me.cmbAfterGoodsRank.TextValue = ""
        Me.cmbAfterGoodsRank.ValueMember = Nothing
        Me.cmbAfterGoodsRank.WidthDef = 156
        '
        'cmbBeforeGoodsRank
        '
        Me.cmbBeforeGoodsRank.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBeforeGoodsRank.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBeforeGoodsRank.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBeforeGoodsRank.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBeforeGoodsRank.DataSource = Nothing
        Me.cmbBeforeGoodsRank.DisplayMember = Nothing
        Me.cmbBeforeGoodsRank.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBeforeGoodsRank.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBeforeGoodsRank.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBeforeGoodsRank.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBeforeGoodsRank.HeightDef = 18
        Me.cmbBeforeGoodsRank.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBeforeGoodsRank.HissuLabelVisible = False
        Me.cmbBeforeGoodsRank.InsertWildCard = True
        Me.cmbBeforeGoodsRank.IsForbiddenWordsCheck = False
        Me.cmbBeforeGoodsRank.IsHissuCheck = False
        Me.cmbBeforeGoodsRank.ItemName = ""
        Me.cmbBeforeGoodsRank.Location = New System.Drawing.Point(161, 31)
        Me.cmbBeforeGoodsRank.Name = "cmbBeforeGoodsRank"
        Me.cmbBeforeGoodsRank.ReadOnly = False
        Me.cmbBeforeGoodsRank.SelectedIndex = -1
        Me.cmbBeforeGoodsRank.SelectedItem = Nothing
        Me.cmbBeforeGoodsRank.SelectedText = ""
        Me.cmbBeforeGoodsRank.SelectedValue = ""
        Me.cmbBeforeGoodsRank.Size = New System.Drawing.Size(156, 18)
        Me.cmbBeforeGoodsRank.TabIndex = 665
        Me.cmbBeforeGoodsRank.TabStopSetting = True
        Me.cmbBeforeGoodsRank.TextValue = ""
        Me.cmbBeforeGoodsRank.ValueMember = Nothing
        Me.cmbBeforeGoodsRank.WidthDef = 156
        '
        'lblAfterGoodsRank
        '
        Me.lblAfterGoodsRank.AutoSize = True
        Me.lblAfterGoodsRank.AutoSizeDef = True
        Me.lblAfterGoodsRank.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAfterGoodsRank.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblAfterGoodsRank.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblAfterGoodsRank.EnableStatus = False
        Me.lblAfterGoodsRank.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAfterGoodsRank.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblAfterGoodsRank.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAfterGoodsRank.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAfterGoodsRank.HeightDef = 13
        Me.lblAfterGoodsRank.Location = New System.Drawing.Point(361, 34)
        Me.lblAfterGoodsRank.Name = "lblAfterGoodsRank"
        Me.lblAfterGoodsRank.Size = New System.Drawing.Size(119, 13)
        Me.lblAfterGoodsRank.TabIndex = 654
        Me.lblAfterGoodsRank.Text = "変更後商品ランク"
        Me.lblAfterGoodsRank.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAfterGoodsRank.TextValue = "変更後商品ランク"
        Me.lblAfterGoodsRank.WidthDef = 119
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(36, 34)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel3.TabIndex = 652
        Me.LmTitleLabel3.Text = "変更前商品ランク"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "変更前商品ランク"
        Me.LmTitleLabel3.WidthDef = 119
        '
        'grpWh
        '
        Me.grpWh.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpWh.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpWh.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpWh.Controls.Add(Me.cmbInkaWhType)
        Me.grpWh.Controls.Add(Me.cmbOutkaWhType)
        Me.grpWh.Controls.Add(Me.LmTitleLabel23)
        Me.grpWh.Controls.Add(Me.txtInkaCustNm)
        Me.grpWh.Controls.Add(Me.LmTitleLabel24)
        Me.grpWh.Controls.Add(Me.txtInkaCustNo)
        Me.grpWh.Controls.Add(Me.LmTitleLabel25)
        Me.grpWh.Controls.Add(Me.LmTitleLabel2)
        Me.grpWh.Controls.Add(Me.txtOutkaCustNm)
        Me.grpWh.Controls.Add(Me.LmTitleLabel21)
        Me.grpWh.Controls.Add(Me.txtOutkaCustNo)
        Me.grpWh.Controls.Add(Me.lblOutkaWhType)
        Me.grpWh.EnableStatus = False
        Me.grpWh.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpWh.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpWh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpWh.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpWh.HeightDef = 119
        Me.grpWh.Location = New System.Drawing.Point(39, 187)
        Me.grpWh.Name = "grpWh"
        Me.grpWh.Size = New System.Drawing.Size(1065, 119)
        Me.grpWh.TabIndex = 676
        Me.grpWh.TabStop = False
        Me.grpWh.Text = "倉庫(在庫移動)"
        Me.grpWh.TextValue = "倉庫(在庫移動)"
        Me.grpWh.WidthDef = 1065
        '
        'cmbInkaWhType
        '
        Me.cmbInkaWhType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInkaWhType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbInkaWhType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbInkaWhType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbInkaWhType.DataSource = Nothing
        Me.cmbInkaWhType.DisplayMember = Nothing
        Me.cmbInkaWhType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInkaWhType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInkaWhType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInkaWhType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbInkaWhType.HeightDef = 18
        Me.cmbInkaWhType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbInkaWhType.HissuLabelVisible = False
        Me.cmbInkaWhType.InsertWildCard = True
        Me.cmbInkaWhType.IsForbiddenWordsCheck = False
        Me.cmbInkaWhType.IsHissuCheck = False
        Me.cmbInkaWhType.ItemName = ""
        Me.cmbInkaWhType.Location = New System.Drawing.Point(669, 31)
        Me.cmbInkaWhType.Name = "cmbInkaWhType"
        Me.cmbInkaWhType.ReadOnly = False
        Me.cmbInkaWhType.SelectedIndex = -1
        Me.cmbInkaWhType.SelectedItem = Nothing
        Me.cmbInkaWhType.SelectedText = ""
        Me.cmbInkaWhType.SelectedValue = ""
        Me.cmbInkaWhType.Size = New System.Drawing.Size(216, 18)
        Me.cmbInkaWhType.TabIndex = 665
        Me.cmbInkaWhType.TabStopSetting = True
        Me.cmbInkaWhType.TextValue = ""
        Me.cmbInkaWhType.ValueMember = Nothing
        Me.cmbInkaWhType.WidthDef = 216
        '
        'cmbOutkaWhType
        '
        Me.cmbOutkaWhType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbOutkaWhType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbOutkaWhType.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbOutkaWhType.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbOutkaWhType.DataSource = Nothing
        Me.cmbOutkaWhType.DisplayMember = Nothing
        Me.cmbOutkaWhType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOutkaWhType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbOutkaWhType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOutkaWhType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbOutkaWhType.HeightDef = 18
        Me.cmbOutkaWhType.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbOutkaWhType.HissuLabelVisible = False
        Me.cmbOutkaWhType.InsertWildCard = True
        Me.cmbOutkaWhType.IsForbiddenWordsCheck = False
        Me.cmbOutkaWhType.IsHissuCheck = False
        Me.cmbOutkaWhType.ItemName = ""
        Me.cmbOutkaWhType.Location = New System.Drawing.Point(137, 31)
        Me.cmbOutkaWhType.Name = "cmbOutkaWhType"
        Me.cmbOutkaWhType.ReadOnly = False
        Me.cmbOutkaWhType.SelectedIndex = -1
        Me.cmbOutkaWhType.SelectedItem = Nothing
        Me.cmbOutkaWhType.SelectedText = ""
        Me.cmbOutkaWhType.SelectedValue = ""
        Me.cmbOutkaWhType.Size = New System.Drawing.Size(216, 18)
        Me.cmbOutkaWhType.TabIndex = 664
        Me.cmbOutkaWhType.TabStopSetting = True
        Me.cmbOutkaWhType.TextValue = ""
        Me.cmbOutkaWhType.ValueMember = Nothing
        Me.cmbOutkaWhType.WidthDef = 216
        '
        'LmTitleLabel23
        '
        Me.LmTitleLabel23.AutoSize = True
        Me.LmTitleLabel23.AutoSizeDef = True
        Me.LmTitleLabel23.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel23.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel23.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel23.EnableStatus = False
        Me.LmTitleLabel23.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel23.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel23.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel23.HeightDef = 13
        Me.LmTitleLabel23.Location = New System.Drawing.Point(582, 82)
        Me.LmTitleLabel23.Name = "LmTitleLabel23"
        Me.LmTitleLabel23.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel23.TabIndex = 663
        Me.LmTitleLabel23.Text = "入庫荷主名"
        Me.LmTitleLabel23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel23.TextValue = "入庫荷主名"
        Me.LmTitleLabel23.WidthDef = 77
        '
        'txtInkaCustNm
        '
        Me.txtInkaCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInkaCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtInkaCustNm.CountWrappedLine = False
        Me.txtInkaCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtInkaCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtInkaCustNm.HeightDef = 18
        Me.txtInkaCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNm.HissuLabelVisible = False
        Me.txtInkaCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtInkaCustNm.IsByteCheck = 300
        Me.txtInkaCustNm.IsCalendarCheck = False
        Me.txtInkaCustNm.IsDakutenCheck = False
        Me.txtInkaCustNm.IsEisuCheck = False
        Me.txtInkaCustNm.IsForbiddenWordsCheck = False
        Me.txtInkaCustNm.IsFullByteCheck = 0
        Me.txtInkaCustNm.IsHankakuCheck = False
        Me.txtInkaCustNm.IsHissuCheck = False
        Me.txtInkaCustNm.IsKanaCheck = False
        Me.txtInkaCustNm.IsMiddleSpace = False
        Me.txtInkaCustNm.IsNumericCheck = False
        Me.txtInkaCustNm.IsSujiCheck = False
        Me.txtInkaCustNm.IsZenkakuCheck = False
        Me.txtInkaCustNm.ItemName = ""
        Me.txtInkaCustNm.LineSpace = 0
        Me.txtInkaCustNm.Location = New System.Drawing.Point(669, 79)
        Me.txtInkaCustNm.MaxLength = 300
        Me.txtInkaCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtInkaCustNm.MaxLineCount = 0
        Me.txtInkaCustNm.Multiline = False
        Me.txtInkaCustNm.Name = "txtInkaCustNm"
        Me.txtInkaCustNm.ReadOnly = True
        Me.txtInkaCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtInkaCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtInkaCustNm.Size = New System.Drawing.Size(388, 18)
        Me.txtInkaCustNm.TabIndex = 662
        Me.txtInkaCustNm.TabStop = False
        Me.txtInkaCustNm.TabStopSetting = False
        Me.txtInkaCustNm.Tag = ""
        Me.txtInkaCustNm.TextValue = ""
        Me.txtInkaCustNm.UseSystemPasswordChar = False
        Me.txtInkaCustNm.WidthDef = 388
        Me.txtInkaCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel24
        '
        Me.LmTitleLabel24.AutoSize = True
        Me.LmTitleLabel24.AutoSizeDef = True
        Me.LmTitleLabel24.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel24.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel24.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel24.EnableStatus = False
        Me.LmTitleLabel24.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel24.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel24.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel24.HeightDef = 13
        Me.LmTitleLabel24.Location = New System.Drawing.Point(554, 58)
        Me.LmTitleLabel24.Name = "LmTitleLabel24"
        Me.LmTitleLabel24.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel24.TabIndex = 661
        Me.LmTitleLabel24.Text = "入庫荷主コード"
        Me.LmTitleLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel24.TextValue = "入庫荷主コード"
        Me.LmTitleLabel24.WidthDef = 105
        '
        'txtInkaCustNo
        '
        Me.txtInkaCustNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInkaCustNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtInkaCustNo.CountWrappedLine = False
        Me.txtInkaCustNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtInkaCustNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaCustNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtInkaCustNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaCustNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtInkaCustNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtInkaCustNo.HeightDef = 18
        Me.txtInkaCustNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtInkaCustNo.HissuLabelVisible = False
        Me.txtInkaCustNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtInkaCustNo.IsByteCheck = 300
        Me.txtInkaCustNo.IsCalendarCheck = False
        Me.txtInkaCustNo.IsDakutenCheck = False
        Me.txtInkaCustNo.IsEisuCheck = False
        Me.txtInkaCustNo.IsForbiddenWordsCheck = False
        Me.txtInkaCustNo.IsFullByteCheck = 0
        Me.txtInkaCustNo.IsHankakuCheck = False
        Me.txtInkaCustNo.IsHissuCheck = False
        Me.txtInkaCustNo.IsKanaCheck = False
        Me.txtInkaCustNo.IsMiddleSpace = False
        Me.txtInkaCustNo.IsNumericCheck = False
        Me.txtInkaCustNo.IsSujiCheck = False
        Me.txtInkaCustNo.IsZenkakuCheck = False
        Me.txtInkaCustNo.ItemName = ""
        Me.txtInkaCustNo.LineSpace = 0
        Me.txtInkaCustNo.Location = New System.Drawing.Point(669, 55)
        Me.txtInkaCustNo.MaxLength = 300
        Me.txtInkaCustNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtInkaCustNo.MaxLineCount = 0
        Me.txtInkaCustNo.Multiline = False
        Me.txtInkaCustNo.Name = "txtInkaCustNo"
        Me.txtInkaCustNo.ReadOnly = True
        Me.txtInkaCustNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtInkaCustNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtInkaCustNo.Size = New System.Drawing.Size(113, 18)
        Me.txtInkaCustNo.TabIndex = 660
        Me.txtInkaCustNo.TabStop = False
        Me.txtInkaCustNo.TabStopSetting = False
        Me.txtInkaCustNo.Tag = ""
        Me.txtInkaCustNo.TextValue = ""
        Me.txtInkaCustNo.UseSystemPasswordChar = False
        Me.txtInkaCustNo.WidthDef = 113
        Me.txtInkaCustNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel25
        '
        Me.LmTitleLabel25.AutoSize = True
        Me.LmTitleLabel25.AutoSizeDef = True
        Me.LmTitleLabel25.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel25.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel25.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel25.EnableStatus = False
        Me.LmTitleLabel25.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel25.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel25.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel25.HeightDef = 13
        Me.LmTitleLabel25.Location = New System.Drawing.Point(568, 34)
        Me.LmTitleLabel25.Name = "LmTitleLabel25"
        Me.LmTitleLabel25.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel25.TabIndex = 658
        Me.LmTitleLabel25.Text = "入庫倉庫種類"
        Me.LmTitleLabel25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel25.TextValue = "入庫倉庫種類"
        Me.LmTitleLabel25.WidthDef = 91
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(50, 82)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel2.TabIndex = 657
        Me.LmTitleLabel2.Text = "出庫荷主名"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel2.TextValue = "出庫荷主名"
        Me.LmTitleLabel2.WidthDef = 77
        '
        'txtOutkaCustNm
        '
        Me.txtOutkaCustNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaCustNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaCustNm.CountWrappedLine = False
        Me.txtOutkaCustNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaCustNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaCustNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaCustNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaCustNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaCustNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaCustNm.HeightDef = 18
        Me.txtOutkaCustNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNm.HissuLabelVisible = False
        Me.txtOutkaCustNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOutkaCustNm.IsByteCheck = 300
        Me.txtOutkaCustNm.IsCalendarCheck = False
        Me.txtOutkaCustNm.IsDakutenCheck = False
        Me.txtOutkaCustNm.IsEisuCheck = False
        Me.txtOutkaCustNm.IsForbiddenWordsCheck = False
        Me.txtOutkaCustNm.IsFullByteCheck = 0
        Me.txtOutkaCustNm.IsHankakuCheck = False
        Me.txtOutkaCustNm.IsHissuCheck = False
        Me.txtOutkaCustNm.IsKanaCheck = False
        Me.txtOutkaCustNm.IsMiddleSpace = False
        Me.txtOutkaCustNm.IsNumericCheck = False
        Me.txtOutkaCustNm.IsSujiCheck = False
        Me.txtOutkaCustNm.IsZenkakuCheck = False
        Me.txtOutkaCustNm.ItemName = ""
        Me.txtOutkaCustNm.LineSpace = 0
        Me.txtOutkaCustNm.Location = New System.Drawing.Point(137, 79)
        Me.txtOutkaCustNm.MaxLength = 300
        Me.txtOutkaCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaCustNm.MaxLineCount = 0
        Me.txtOutkaCustNm.Multiline = False
        Me.txtOutkaCustNm.Name = "txtOutkaCustNm"
        Me.txtOutkaCustNm.ReadOnly = True
        Me.txtOutkaCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaCustNm.Size = New System.Drawing.Size(388, 18)
        Me.txtOutkaCustNm.TabIndex = 656
        Me.txtOutkaCustNm.TabStop = False
        Me.txtOutkaCustNm.TabStopSetting = False
        Me.txtOutkaCustNm.Tag = ""
        Me.txtOutkaCustNm.TextValue = ""
        Me.txtOutkaCustNm.UseSystemPasswordChar = False
        Me.txtOutkaCustNm.WidthDef = 388
        Me.txtOutkaCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel21
        '
        Me.LmTitleLabel21.AutoSize = True
        Me.LmTitleLabel21.AutoSizeDef = True
        Me.LmTitleLabel21.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel21.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel21.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel21.EnableStatus = False
        Me.LmTitleLabel21.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel21.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel21.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel21.HeightDef = 13
        Me.LmTitleLabel21.Location = New System.Drawing.Point(22, 58)
        Me.LmTitleLabel21.Name = "LmTitleLabel21"
        Me.LmTitleLabel21.Size = New System.Drawing.Size(105, 13)
        Me.LmTitleLabel21.TabIndex = 655
        Me.LmTitleLabel21.Text = "出庫荷主コード"
        Me.LmTitleLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel21.TextValue = "出庫荷主コード"
        Me.LmTitleLabel21.WidthDef = 105
        '
        'txtOutkaCustNo
        '
        Me.txtOutkaCustNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutkaCustNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtOutkaCustNo.CountWrappedLine = False
        Me.txtOutkaCustNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtOutkaCustNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaCustNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtOutkaCustNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaCustNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtOutkaCustNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtOutkaCustNo.HeightDef = 18
        Me.txtOutkaCustNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtOutkaCustNo.HissuLabelVisible = False
        Me.txtOutkaCustNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtOutkaCustNo.IsByteCheck = 300
        Me.txtOutkaCustNo.IsCalendarCheck = False
        Me.txtOutkaCustNo.IsDakutenCheck = False
        Me.txtOutkaCustNo.IsEisuCheck = False
        Me.txtOutkaCustNo.IsForbiddenWordsCheck = False
        Me.txtOutkaCustNo.IsFullByteCheck = 0
        Me.txtOutkaCustNo.IsHankakuCheck = False
        Me.txtOutkaCustNo.IsHissuCheck = False
        Me.txtOutkaCustNo.IsKanaCheck = False
        Me.txtOutkaCustNo.IsMiddleSpace = False
        Me.txtOutkaCustNo.IsNumericCheck = False
        Me.txtOutkaCustNo.IsSujiCheck = False
        Me.txtOutkaCustNo.IsZenkakuCheck = False
        Me.txtOutkaCustNo.ItemName = ""
        Me.txtOutkaCustNo.LineSpace = 0
        Me.txtOutkaCustNo.Location = New System.Drawing.Point(137, 55)
        Me.txtOutkaCustNo.MaxLength = 300
        Me.txtOutkaCustNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtOutkaCustNo.MaxLineCount = 0
        Me.txtOutkaCustNo.Multiline = False
        Me.txtOutkaCustNo.Name = "txtOutkaCustNo"
        Me.txtOutkaCustNo.ReadOnly = True
        Me.txtOutkaCustNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtOutkaCustNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOutkaCustNo.Size = New System.Drawing.Size(113, 18)
        Me.txtOutkaCustNo.TabIndex = 654
        Me.txtOutkaCustNo.TabStop = False
        Me.txtOutkaCustNo.TabStopSetting = False
        Me.txtOutkaCustNo.Tag = ""
        Me.txtOutkaCustNo.TextValue = ""
        Me.txtOutkaCustNo.UseSystemPasswordChar = False
        Me.txtOutkaCustNo.WidthDef = 113
        Me.txtOutkaCustNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblOutkaWhType
        '
        Me.lblOutkaWhType.AutoSize = True
        Me.lblOutkaWhType.AutoSizeDef = True
        Me.lblOutkaWhType.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOutkaWhType.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblOutkaWhType.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblOutkaWhType.EnableStatus = False
        Me.lblOutkaWhType.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOutkaWhType.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOutkaWhType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOutkaWhType.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOutkaWhType.HeightDef = 13
        Me.lblOutkaWhType.Location = New System.Drawing.Point(36, 34)
        Me.lblOutkaWhType.Name = "lblOutkaWhType"
        Me.lblOutkaWhType.Size = New System.Drawing.Size(91, 13)
        Me.lblOutkaWhType.TabIndex = 652
        Me.lblOutkaWhType.Text = "出庫倉庫種類"
        Me.lblOutkaWhType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblOutkaWhType.TextValue = "出庫倉庫種類"
        Me.lblOutkaWhType.WidthDef = 91
        '
        'cmbJissekiShoriFlg
        '
        Me.cmbJissekiShoriFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiShoriFlg.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiShoriFlg.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbJissekiShoriFlg.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbJissekiShoriFlg.DataCode = "H038"
        Me.cmbJissekiShoriFlg.DataSource = Nothing
        Me.cmbJissekiShoriFlg.DisplayMember = Nothing
        Me.cmbJissekiShoriFlg.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJissekiShoriFlg.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbJissekiShoriFlg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJissekiShoriFlg.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbJissekiShoriFlg.HeightDef = 18
        Me.cmbJissekiShoriFlg.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbJissekiShoriFlg.HissuLabelVisible = False
        Me.cmbJissekiShoriFlg.InsertWildCard = True
        Me.cmbJissekiShoriFlg.IsForbiddenWordsCheck = False
        Me.cmbJissekiShoriFlg.IsHissuCheck = False
        Me.cmbJissekiShoriFlg.ItemName = ""
        Me.cmbJissekiShoriFlg.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbJissekiShoriFlg.Location = New System.Drawing.Point(662, 72)
        Me.cmbJissekiShoriFlg.Name = "cmbJissekiShoriFlg"
        Me.cmbJissekiShoriFlg.ReadOnly = True
        Me.cmbJissekiShoriFlg.SelectedIndex = -1
        Me.cmbJissekiShoriFlg.SelectedItem = Nothing
        Me.cmbJissekiShoriFlg.SelectedText = ""
        Me.cmbJissekiShoriFlg.SelectedValue = ""
        Me.cmbJissekiShoriFlg.Size = New System.Drawing.Size(132, 18)
        Me.cmbJissekiShoriFlg.TabIndex = 678
        Me.cmbJissekiShoriFlg.TabStop = False
        Me.cmbJissekiShoriFlg.TabStopSetting = False
        Me.cmbJissekiShoriFlg.TextValue = ""
        Me.cmbJissekiShoriFlg.Value1 = Nothing
        Me.cmbJissekiShoriFlg.Value2 = Nothing
        Me.cmbJissekiShoriFlg.Value3 = Nothing
        Me.cmbJissekiShoriFlg.ValueMember = Nothing
        Me.cmbJissekiShoriFlg.WidthDef = 132
        '
        'LmTitleLabel8
        '
        Me.LmTitleLabel8.AutoSize = True
        Me.LmTitleLabel8.AutoSizeDef = True
        Me.LmTitleLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel8.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel8.EnableStatus = False
        Me.LmTitleLabel8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel8.HeightDef = 13
        Me.LmTitleLabel8.Location = New System.Drawing.Point(572, 75)
        Me.LmTitleLabel8.Name = "LmTitleLabel8"
        Me.LmTitleLabel8.Size = New System.Drawing.Size(77, 13)
        Me.LmTitleLabel8.TabIndex = 677
        Me.LmTitleLabel8.Text = "ステータス"
        Me.LmTitleLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel8.TextValue = "ステータス"
        Me.LmTitleLabel8.WidthDef = 77
        '
        'grpObic
        '
        Me.grpObic.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpObic.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpObic.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpObic.Controls.Add(Me.LmTitleLabel13)
        Me.grpObic.Controls.Add(Me.txtObicDetailNo)
        Me.grpObic.Controls.Add(Me.LmTitleLabel12)
        Me.grpObic.Controls.Add(Me.txtObicGyoNo)
        Me.grpObic.Controls.Add(Me.LmTitleLabel11)
        Me.grpObic.Controls.Add(Me.txtObicDenpNo)
        Me.grpObic.Controls.Add(Me.LmTitleLabel10)
        Me.grpObic.Controls.Add(Me.txtObicTorihikiKbn)
        Me.grpObic.Controls.Add(Me.LmTitleLabel9)
        Me.grpObic.Controls.Add(Me.txtObicShubetu)
        Me.grpObic.EnableStatus = False
        Me.grpObic.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpObic.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpObic.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpObic.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpObic.HeightDef = 94
        Me.grpObic.Location = New System.Drawing.Point(39, 614)
        Me.grpObic.Name = "grpObic"
        Me.grpObic.Size = New System.Drawing.Size(782, 94)
        Me.grpObic.TabIndex = 679
        Me.grpObic.TabStop = False
        Me.grpObic.Text = "OBIC管理項目"
        Me.grpObic.TextValue = "OBIC管理項目"
        Me.grpObic.WidthDef = 782
        '
        'LmTitleLabel13
        '
        Me.LmTitleLabel13.AutoSize = True
        Me.LmTitleLabel13.AutoSizeDef = True
        Me.LmTitleLabel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel13.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel13.EnableStatus = False
        Me.LmTitleLabel13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel13.HeightDef = 13
        Me.LmTitleLabel13.Location = New System.Drawing.Point(524, 56)
        Me.LmTitleLabel13.Name = "LmTitleLabel13"
        Me.LmTitleLabel13.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel13.TabIndex = 672
        Me.LmTitleLabel13.Text = "詳細番号"
        Me.LmTitleLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel13.TextValue = "詳細番号"
        Me.LmTitleLabel13.WidthDef = 63
        '
        'txtObicDetailNo
        '
        Me.txtObicDetailNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDetailNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDetailNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObicDetailNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtObicDetailNo.CountWrappedLine = False
        Me.txtObicDetailNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtObicDetailNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicDetailNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicDetailNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicDetailNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicDetailNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtObicDetailNo.HeightDef = 18
        Me.txtObicDetailNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDetailNo.HissuLabelVisible = False
        Me.txtObicDetailNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtObicDetailNo.IsByteCheck = 300
        Me.txtObicDetailNo.IsCalendarCheck = False
        Me.txtObicDetailNo.IsDakutenCheck = False
        Me.txtObicDetailNo.IsEisuCheck = False
        Me.txtObicDetailNo.IsForbiddenWordsCheck = False
        Me.txtObicDetailNo.IsFullByteCheck = 0
        Me.txtObicDetailNo.IsHankakuCheck = False
        Me.txtObicDetailNo.IsHissuCheck = False
        Me.txtObicDetailNo.IsKanaCheck = False
        Me.txtObicDetailNo.IsMiddleSpace = False
        Me.txtObicDetailNo.IsNumericCheck = False
        Me.txtObicDetailNo.IsSujiCheck = False
        Me.txtObicDetailNo.IsZenkakuCheck = False
        Me.txtObicDetailNo.ItemName = ""
        Me.txtObicDetailNo.LineSpace = 0
        Me.txtObicDetailNo.Location = New System.Drawing.Point(599, 53)
        Me.txtObicDetailNo.MaxLength = 300
        Me.txtObicDetailNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtObicDetailNo.MaxLineCount = 0
        Me.txtObicDetailNo.Multiline = False
        Me.txtObicDetailNo.Name = "txtObicDetailNo"
        Me.txtObicDetailNo.ReadOnly = True
        Me.txtObicDetailNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtObicDetailNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtObicDetailNo.Size = New System.Drawing.Size(113, 18)
        Me.txtObicDetailNo.TabIndex = 671
        Me.txtObicDetailNo.TabStop = False
        Me.txtObicDetailNo.TabStopSetting = False
        Me.txtObicDetailNo.Tag = ""
        Me.txtObicDetailNo.TextValue = ""
        Me.txtObicDetailNo.UseSystemPasswordChar = False
        Me.txtObicDetailNo.WidthDef = 113
        Me.txtObicDetailNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel12
        '
        Me.LmTitleLabel12.AutoSize = True
        Me.LmTitleLabel12.AutoSizeDef = True
        Me.LmTitleLabel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel12.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel12.EnableStatus = False
        Me.LmTitleLabel12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel12.HeightDef = 13
        Me.LmTitleLabel12.Location = New System.Drawing.Point(304, 56)
        Me.LmTitleLabel12.Name = "LmTitleLabel12"
        Me.LmTitleLabel12.Size = New System.Drawing.Size(49, 13)
        Me.LmTitleLabel12.TabIndex = 670
        Me.LmTitleLabel12.Text = "行番号"
        Me.LmTitleLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel12.TextValue = "行番号"
        Me.LmTitleLabel12.WidthDef = 49
        '
        'txtObicGyoNo
        '
        Me.txtObicGyoNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicGyoNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicGyoNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObicGyoNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtObicGyoNo.CountWrappedLine = False
        Me.txtObicGyoNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtObicGyoNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicGyoNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicGyoNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicGyoNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicGyoNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtObicGyoNo.HeightDef = 18
        Me.txtObicGyoNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicGyoNo.HissuLabelVisible = False
        Me.txtObicGyoNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtObicGyoNo.IsByteCheck = 300
        Me.txtObicGyoNo.IsCalendarCheck = False
        Me.txtObicGyoNo.IsDakutenCheck = False
        Me.txtObicGyoNo.IsEisuCheck = False
        Me.txtObicGyoNo.IsForbiddenWordsCheck = False
        Me.txtObicGyoNo.IsFullByteCheck = 0
        Me.txtObicGyoNo.IsHankakuCheck = False
        Me.txtObicGyoNo.IsHissuCheck = False
        Me.txtObicGyoNo.IsKanaCheck = False
        Me.txtObicGyoNo.IsMiddleSpace = False
        Me.txtObicGyoNo.IsNumericCheck = False
        Me.txtObicGyoNo.IsSujiCheck = False
        Me.txtObicGyoNo.IsZenkakuCheck = False
        Me.txtObicGyoNo.ItemName = ""
        Me.txtObicGyoNo.LineSpace = 0
        Me.txtObicGyoNo.Location = New System.Drawing.Point(361, 53)
        Me.txtObicGyoNo.MaxLength = 300
        Me.txtObicGyoNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtObicGyoNo.MaxLineCount = 0
        Me.txtObicGyoNo.Multiline = False
        Me.txtObicGyoNo.Name = "txtObicGyoNo"
        Me.txtObicGyoNo.ReadOnly = True
        Me.txtObicGyoNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtObicGyoNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtObicGyoNo.Size = New System.Drawing.Size(113, 18)
        Me.txtObicGyoNo.TabIndex = 669
        Me.txtObicGyoNo.TabStop = False
        Me.txtObicGyoNo.TabStopSetting = False
        Me.txtObicGyoNo.Tag = ""
        Me.txtObicGyoNo.TextValue = ""
        Me.txtObicGyoNo.UseSystemPasswordChar = False
        Me.txtObicGyoNo.WidthDef = 113
        Me.txtObicGyoNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel11
        '
        Me.LmTitleLabel11.AutoSize = True
        Me.LmTitleLabel11.AutoSizeDef = True
        Me.LmTitleLabel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel11.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel11.EnableStatus = False
        Me.LmTitleLabel11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel11.HeightDef = 13
        Me.LmTitleLabel11.Location = New System.Drawing.Point(51, 56)
        Me.LmTitleLabel11.Name = "LmTitleLabel11"
        Me.LmTitleLabel11.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel11.TabIndex = 668
        Me.LmTitleLabel11.Text = "伝票番号"
        Me.LmTitleLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel11.TextValue = "伝票番号"
        Me.LmTitleLabel11.WidthDef = 63
        '
        'txtObicDenpNo
        '
        Me.txtObicDenpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDenpNo.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDenpNo.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObicDenpNo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtObicDenpNo.CountWrappedLine = False
        Me.txtObicDenpNo.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtObicDenpNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicDenpNo.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicDenpNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicDenpNo.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicDenpNo.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtObicDenpNo.HeightDef = 18
        Me.txtObicDenpNo.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicDenpNo.HissuLabelVisible = False
        Me.txtObicDenpNo.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtObicDenpNo.IsByteCheck = 300
        Me.txtObicDenpNo.IsCalendarCheck = False
        Me.txtObicDenpNo.IsDakutenCheck = False
        Me.txtObicDenpNo.IsEisuCheck = False
        Me.txtObicDenpNo.IsForbiddenWordsCheck = False
        Me.txtObicDenpNo.IsFullByteCheck = 0
        Me.txtObicDenpNo.IsHankakuCheck = False
        Me.txtObicDenpNo.IsHissuCheck = False
        Me.txtObicDenpNo.IsKanaCheck = False
        Me.txtObicDenpNo.IsMiddleSpace = False
        Me.txtObicDenpNo.IsNumericCheck = False
        Me.txtObicDenpNo.IsSujiCheck = False
        Me.txtObicDenpNo.IsZenkakuCheck = False
        Me.txtObicDenpNo.ItemName = ""
        Me.txtObicDenpNo.LineSpace = 0
        Me.txtObicDenpNo.Location = New System.Drawing.Point(120, 53)
        Me.txtObicDenpNo.MaxLength = 300
        Me.txtObicDenpNo.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtObicDenpNo.MaxLineCount = 0
        Me.txtObicDenpNo.Multiline = False
        Me.txtObicDenpNo.Name = "txtObicDenpNo"
        Me.txtObicDenpNo.ReadOnly = True
        Me.txtObicDenpNo.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtObicDenpNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtObicDenpNo.Size = New System.Drawing.Size(113, 18)
        Me.txtObicDenpNo.TabIndex = 667
        Me.txtObicDenpNo.TabStop = False
        Me.txtObicDenpNo.TabStopSetting = False
        Me.txtObicDenpNo.Tag = ""
        Me.txtObicDenpNo.TextValue = ""
        Me.txtObicDenpNo.UseSystemPasswordChar = False
        Me.txtObicDenpNo.WidthDef = 113
        Me.txtObicDenpNo.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel10
        '
        Me.LmTitleLabel10.AutoSize = True
        Me.LmTitleLabel10.AutoSizeDef = True
        Me.LmTitleLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel10.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel10.EnableStatus = False
        Me.LmTitleLabel10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel10.HeightDef = 13
        Me.LmTitleLabel10.Location = New System.Drawing.Point(290, 32)
        Me.LmTitleLabel10.Name = "LmTitleLabel10"
        Me.LmTitleLabel10.Size = New System.Drawing.Size(63, 13)
        Me.LmTitleLabel10.TabIndex = 666
        Me.LmTitleLabel10.Text = "取引区分"
        Me.LmTitleLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel10.TextValue = "取引区分"
        Me.LmTitleLabel10.WidthDef = 63
        '
        'txtObicTorihikiKbn
        '
        Me.txtObicTorihikiKbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicTorihikiKbn.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicTorihikiKbn.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObicTorihikiKbn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtObicTorihikiKbn.CountWrappedLine = False
        Me.txtObicTorihikiKbn.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtObicTorihikiKbn.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicTorihikiKbn.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicTorihikiKbn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicTorihikiKbn.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicTorihikiKbn.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtObicTorihikiKbn.HeightDef = 18
        Me.txtObicTorihikiKbn.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicTorihikiKbn.HissuLabelVisible = False
        Me.txtObicTorihikiKbn.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtObicTorihikiKbn.IsByteCheck = 300
        Me.txtObicTorihikiKbn.IsCalendarCheck = False
        Me.txtObicTorihikiKbn.IsDakutenCheck = False
        Me.txtObicTorihikiKbn.IsEisuCheck = False
        Me.txtObicTorihikiKbn.IsForbiddenWordsCheck = False
        Me.txtObicTorihikiKbn.IsFullByteCheck = 0
        Me.txtObicTorihikiKbn.IsHankakuCheck = False
        Me.txtObicTorihikiKbn.IsHissuCheck = False
        Me.txtObicTorihikiKbn.IsKanaCheck = False
        Me.txtObicTorihikiKbn.IsMiddleSpace = False
        Me.txtObicTorihikiKbn.IsNumericCheck = False
        Me.txtObicTorihikiKbn.IsSujiCheck = False
        Me.txtObicTorihikiKbn.IsZenkakuCheck = False
        Me.txtObicTorihikiKbn.ItemName = ""
        Me.txtObicTorihikiKbn.LineSpace = 0
        Me.txtObicTorihikiKbn.Location = New System.Drawing.Point(361, 29)
        Me.txtObicTorihikiKbn.MaxLength = 300
        Me.txtObicTorihikiKbn.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtObicTorihikiKbn.MaxLineCount = 0
        Me.txtObicTorihikiKbn.Multiline = False
        Me.txtObicTorihikiKbn.Name = "txtObicTorihikiKbn"
        Me.txtObicTorihikiKbn.ReadOnly = True
        Me.txtObicTorihikiKbn.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtObicTorihikiKbn.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtObicTorihikiKbn.Size = New System.Drawing.Size(113, 18)
        Me.txtObicTorihikiKbn.TabIndex = 665
        Me.txtObicTorihikiKbn.TabStop = False
        Me.txtObicTorihikiKbn.TabStopSetting = False
        Me.txtObicTorihikiKbn.Tag = ""
        Me.txtObicTorihikiKbn.TextValue = ""
        Me.txtObicTorihikiKbn.UseSystemPasswordChar = False
        Me.txtObicTorihikiKbn.WidthDef = 113
        Me.txtObicTorihikiKbn.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LmTitleLabel9
        '
        Me.LmTitleLabel9.AutoSize = True
        Me.LmTitleLabel9.AutoSizeDef = True
        Me.LmTitleLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmTitleLabel9.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.LmTitleLabel9.EnableStatus = False
        Me.LmTitleLabel9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmTitleLabel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmTitleLabel9.HeightDef = 13
        Me.LmTitleLabel9.Location = New System.Drawing.Point(79, 32)
        Me.LmTitleLabel9.Name = "LmTitleLabel9"
        Me.LmTitleLabel9.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel9.TabIndex = 664
        Me.LmTitleLabel9.Text = "種別"
        Me.LmTitleLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LmTitleLabel9.TextValue = "種別"
        Me.LmTitleLabel9.WidthDef = 35
        '
        'txtObicShubetu
        '
        Me.txtObicShubetu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicShubetu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicShubetu.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObicShubetu.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtObicShubetu.CountWrappedLine = False
        Me.txtObicShubetu.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtObicShubetu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicShubetu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtObicShubetu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicShubetu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtObicShubetu.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtObicShubetu.HeightDef = 18
        Me.txtObicShubetu.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtObicShubetu.HissuLabelVisible = False
        Me.txtObicShubetu.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.txtObicShubetu.IsByteCheck = 300
        Me.txtObicShubetu.IsCalendarCheck = False
        Me.txtObicShubetu.IsDakutenCheck = False
        Me.txtObicShubetu.IsEisuCheck = False
        Me.txtObicShubetu.IsForbiddenWordsCheck = False
        Me.txtObicShubetu.IsFullByteCheck = 0
        Me.txtObicShubetu.IsHankakuCheck = False
        Me.txtObicShubetu.IsHissuCheck = False
        Me.txtObicShubetu.IsKanaCheck = False
        Me.txtObicShubetu.IsMiddleSpace = False
        Me.txtObicShubetu.IsNumericCheck = False
        Me.txtObicShubetu.IsSujiCheck = False
        Me.txtObicShubetu.IsZenkakuCheck = False
        Me.txtObicShubetu.ItemName = ""
        Me.txtObicShubetu.LineSpace = 0
        Me.txtObicShubetu.Location = New System.Drawing.Point(120, 29)
        Me.txtObicShubetu.MaxLength = 300
        Me.txtObicShubetu.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtObicShubetu.MaxLineCount = 0
        Me.txtObicShubetu.Multiline = False
        Me.txtObicShubetu.Name = "txtObicShubetu"
        Me.txtObicShubetu.ReadOnly = True
        Me.txtObicShubetu.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtObicShubetu.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtObicShubetu.Size = New System.Drawing.Size(113, 18)
        Me.txtObicShubetu.TabIndex = 663
        Me.txtObicShubetu.TabStop = False
        Me.txtObicShubetu.TabStopSetting = False
        Me.txtObicShubetu.Tag = ""
        Me.txtObicShubetu.TextValue = ""
        Me.txtObicShubetu.UseSystemPasswordChar = False
        Me.txtObicShubetu.WidthDef = 113
        Me.txtObicShubetu.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblSituation
        '
        Me.lblSituation.DispMode = "9"
        Me.lblSituation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSituation.Location = New System.Drawing.Point(1116, 24)
        Me.lblSituation.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.lblSituation.Name = "lblSituation"
        Me.lblSituation.RecordStatus = "9"
        Me.lblSituation.Size = New System.Drawing.Size(135, 18)
        Me.lblSituation.TabIndex = 680
        Me.lblSituation.TabStop = False
        '
        'txtSysUpdDate
        '
        Me.txtSysUpdDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSysUpdDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSysUpdDate.CountWrappedLine = False
        Me.txtSysUpdDate.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSysUpdDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSysUpdDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSysUpdDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSysUpdDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSysUpdDate.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSysUpdDate.HeightDef = 18
        Me.txtSysUpdDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdDate.HissuLabelVisible = False
        Me.txtSysUpdDate.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSysUpdDate.IsByteCheck = 300
        Me.txtSysUpdDate.IsCalendarCheck = False
        Me.txtSysUpdDate.IsDakutenCheck = False
        Me.txtSysUpdDate.IsEisuCheck = False
        Me.txtSysUpdDate.IsForbiddenWordsCheck = False
        Me.txtSysUpdDate.IsFullByteCheck = 0
        Me.txtSysUpdDate.IsHankakuCheck = False
        Me.txtSysUpdDate.IsHissuCheck = False
        Me.txtSysUpdDate.IsKanaCheck = False
        Me.txtSysUpdDate.IsMiddleSpace = False
        Me.txtSysUpdDate.IsNumericCheck = False
        Me.txtSysUpdDate.IsSujiCheck = False
        Me.txtSysUpdDate.IsZenkakuCheck = False
        Me.txtSysUpdDate.ItemName = ""
        Me.txtSysUpdDate.LineSpace = 0
        Me.txtSysUpdDate.Location = New System.Drawing.Point(1138, 460)
        Me.txtSysUpdDate.MaxLength = 300
        Me.txtSysUpdDate.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSysUpdDate.MaxLineCount = 0
        Me.txtSysUpdDate.Multiline = False
        Me.txtSysUpdDate.Name = "txtSysUpdDate"
        Me.txtSysUpdDate.ReadOnly = True
        Me.txtSysUpdDate.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSysUpdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSysUpdDate.Size = New System.Drawing.Size(113, 18)
        Me.txtSysUpdDate.TabIndex = 681
        Me.txtSysUpdDate.TabStop = False
        Me.txtSysUpdDate.TabStopSetting = False
        Me.txtSysUpdDate.Tag = ""
        Me.txtSysUpdDate.TextValue = ""
        Me.txtSysUpdDate.UseSystemPasswordChar = False
        Me.txtSysUpdDate.Visible = False
        Me.txtSysUpdDate.WidthDef = 113
        Me.txtSysUpdDate.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtSysUpdTime
        '
        Me.txtSysUpdTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdTime.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdTime.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSysUpdTime.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSysUpdTime.CountWrappedLine = False
        Me.txtSysUpdTime.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSysUpdTime.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSysUpdTime.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSysUpdTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSysUpdTime.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSysUpdTime.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSysUpdTime.HeightDef = 18
        Me.txtSysUpdTime.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSysUpdTime.HissuLabelVisible = False
        Me.txtSysUpdTime.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSysUpdTime.IsByteCheck = 300
        Me.txtSysUpdTime.IsCalendarCheck = False
        Me.txtSysUpdTime.IsDakutenCheck = False
        Me.txtSysUpdTime.IsEisuCheck = False
        Me.txtSysUpdTime.IsForbiddenWordsCheck = False
        Me.txtSysUpdTime.IsFullByteCheck = 0
        Me.txtSysUpdTime.IsHankakuCheck = False
        Me.txtSysUpdTime.IsHissuCheck = False
        Me.txtSysUpdTime.IsKanaCheck = False
        Me.txtSysUpdTime.IsMiddleSpace = False
        Me.txtSysUpdTime.IsNumericCheck = False
        Me.txtSysUpdTime.IsSujiCheck = False
        Me.txtSysUpdTime.IsZenkakuCheck = False
        Me.txtSysUpdTime.ItemName = ""
        Me.txtSysUpdTime.LineSpace = 0
        Me.txtSysUpdTime.Location = New System.Drawing.Point(1138, 485)
        Me.txtSysUpdTime.MaxLength = 300
        Me.txtSysUpdTime.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSysUpdTime.MaxLineCount = 0
        Me.txtSysUpdTime.Multiline = False
        Me.txtSysUpdTime.Name = "txtSysUpdTime"
        Me.txtSysUpdTime.ReadOnly = True
        Me.txtSysUpdTime.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSysUpdTime.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSysUpdTime.Size = New System.Drawing.Size(113, 18)
        Me.txtSysUpdTime.TabIndex = 682
        Me.txtSysUpdTime.TabStop = False
        Me.txtSysUpdTime.TabStopSetting = False
        Me.txtSysUpdTime.Tag = ""
        Me.txtSysUpdTime.TextValue = ""
        Me.txtSysUpdTime.UseSystemPasswordChar = False
        Me.txtSysUpdTime.Visible = False
        Me.txtSysUpdTime.WidthDef = 113
        Me.txtSysUpdTime.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'LMI150F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMI150F"
        Me.Text = "【LMI150】  物産アニマルヘルス倉庫内処理編集"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpGoodsRank.ResumeLayout(False)
        Me.grpGoodsRank.PerformLayout()
        Me.grpWh.ResumeLayout(False)
        Me.grpWh.PerformLayout()
        Me.grpObic.ResumeLayout(False)
        Me.grpObic.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbEigyo As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbJissekiFuyo As Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel7 As Win.LMTitleLabel
    Friend WithEvents cmbProcKbn As Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel6 As Win.LMTitleLabel
    Friend WithEvents cmbProcType As Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel5 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel4 As Win.LMTitleLabel
    Friend WithEvents txtNrsProcNo As Win.InputMan.LMImTextBox
    Friend WithEvents imdProcDate As Win.InputMan.LMImDate
    Friend WithEvents lblProcDate As Win.LMTitleLabel
    Friend WithEvents btnZaikoSel As Win.LMButton
    Friend WithEvents LmTitleLabel17 As Win.LMTitleLabel
    Friend WithEvents txtLotNo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel16 As Win.LMTitleLabel
    Friend WithEvents txtGoodsNm As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel15 As Win.LMTitleLabel
    Friend WithEvents txtGoodsCdNrs As Win.InputMan.LMImTextBox
    Friend WithEvents lblGoodsCd As Win.LMTitleLabel
    Friend WithEvents txtGoodsCd As Win.InputMan.LMImTextBox
    Friend WithEvents imdLtDate As Win.InputMan.LMImDate
    Friend WithEvents LmTitleLabel19 As Win.LMTitleLabel
    Friend WithEvents lblNb As Win.LMTitleLabel
    Friend WithEvents numNb As Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel20 As Win.LMTitleLabel
    Friend WithEvents txtRemark As Win.InputMan.LMImTextBox
    Friend WithEvents grpGoodsRank As Win.LMGroupBox
    Friend WithEvents lblAfterGoodsRank As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel3 As Win.LMTitleLabel
    Friend WithEvents grpWh As Win.LMGroupBox
    Friend WithEvents LmTitleLabel23 As Win.LMTitleLabel
    Friend WithEvents txtInkaCustNm As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel24 As Win.LMTitleLabel
    Friend WithEvents txtInkaCustNo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel25 As Win.LMTitleLabel
    Friend WithEvents LmTitleLabel2 As Win.LMTitleLabel
    Friend WithEvents txtOutkaCustNm As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel21 As Win.LMTitleLabel
    Friend WithEvents txtOutkaCustNo As Win.InputMan.LMImTextBox
    Friend WithEvents lblOutkaWhType As Win.LMTitleLabel
    Friend WithEvents cmbJissekiShoriFlg As Win.InputMan.LMComboKubun
    Friend WithEvents LmTitleLabel8 As Win.LMTitleLabel
    Friend WithEvents grpObic As Win.LMGroupBox
    Friend WithEvents LmTitleLabel13 As Win.LMTitleLabel
    Friend WithEvents txtObicDetailNo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel12 As Win.LMTitleLabel
    Friend WithEvents txtObicGyoNo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel11 As Win.LMTitleLabel
    Friend WithEvents txtObicDenpNo As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel10 As Win.LMTitleLabel
    Friend WithEvents txtObicTorihikiKbn As Win.InputMan.LMImTextBox
    Friend WithEvents LmTitleLabel9 As Win.LMTitleLabel
    Friend WithEvents txtObicShubetu As Win.InputMan.LMImTextBox
    Friend WithEvents lblSituation As Win.LMSituationLabel
    Friend WithEvents cmbInkaWhType As Win.InputMan.LMImCombo
    Friend WithEvents cmbOutkaWhType As Win.InputMan.LMImCombo
    Friend WithEvents cmbAfterGoodsRank As Win.InputMan.LMImCombo
    Friend WithEvents cmbBeforeGoodsRank As Win.InputMan.LMImCombo
    Friend WithEvents txtSysUpdTime As Win.InputMan.LMImTextBox
    Friend WithEvents txtSysUpdDate As Win.InputMan.LMImTextBox
End Class
