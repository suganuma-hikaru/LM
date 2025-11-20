<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMG020F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMG020F))
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField
        Me.sprMeisai = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.grpSelect = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.chkMeisaiPrev = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.btnPrint = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.lblTitlePrint = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.btnSeqtoSet = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.btnCustSet = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.numPrintCnt = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.lblBu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblTitleMeisaiBusu = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblSeqtoNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleSeqto = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtSekySaki = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblCustNm = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdSs = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.txtCustCdS = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.imdInvDate = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
        Me.txtCustCdM = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.cmbBr = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
        Me.lblTitleEigyo = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtCustCdL = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.lblTitleCustCd = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkSelectByNrsB = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.lblTitleSimebi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbPrint = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.cmbSimebi = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
        Me.lblTitleSeikyuKikan = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpMode = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.lblTitleMode = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.optSeikyuC = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.optSeikyuH = New Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprMeisai, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSelect.SuspendLayout()
        Me.grpMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.grpMode)
        Me.pnlViewAria.Controls.Add(Me.grpSelect)
        Me.pnlViewAria.Controls.Add(Me.sprMeisai)
        '
        'sprMeisai
        '
        Me.sprMeisai.AccessibleDescription = ""
        Me.sprMeisai.AllowUserZoom = False
        Me.sprMeisai.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprMeisai.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprMeisai.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprMeisai.CellClickEventArgs = Nothing
        Me.sprMeisai.CheckToCheckBox = True
        Me.sprMeisai.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprMeisai.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisai.EditModeReplace = True
        Me.sprMeisai.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisai.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprMeisai.ForeColorDef = System.Drawing.Color.Empty
        Me.sprMeisai.HeightDef = 656
        Me.sprMeisai.Location = New System.Drawing.Point(12, 204)
        Me.sprMeisai.Name = "sprMeisai"
        Me.sprMeisai.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisai.SetViewportTopRow(0, 0, 1)
        Me.sprMeisai.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        'SheetName = "Sheet1"
        'RowCount = 1
        Me.sprMeisai.SetViewportTopRow(0, 0, 1)
        Me.sprMeisai.SetActiveViewport(0, -1, 0)
        Me.sprMeisai.Size = New System.Drawing.Size(1257, 656)
        Me.sprMeisai.SortColumn = True
        Me.sprMeisai.SpanColumnLock = True
        Me.sprMeisai.SpreadDoubleClicked = False
        Me.sprMeisai.TabIndex = 15
        Me.sprMeisai.TabStripPlacement = FarPoint.Win.Spread.TabStripPlacement.Bottom
        Me.sprMeisai.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprMeisai.TextValue = Nothing
        Me.sprMeisai.WidthDef = 1257
        '
        'grpSelect
        '
        Me.grpSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSelect.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSelect.Controls.Add(Me.chkMeisaiPrev)
        Me.grpSelect.Controls.Add(Me.btnPrint)
        Me.grpSelect.Controls.Add(Me.lblTitlePrint)
        Me.grpSelect.Controls.Add(Me.btnSeqtoSet)
        Me.grpSelect.Controls.Add(Me.btnCustSet)
        Me.grpSelect.Controls.Add(Me.numPrintCnt)
        Me.grpSelect.Controls.Add(Me.lblBu)
        Me.grpSelect.Controls.Add(Me.lblTitleMeisaiBusu)
        Me.grpSelect.Controls.Add(Me.lblSeqtoNm)
        Me.grpSelect.Controls.Add(Me.lblTitleSeqto)
        Me.grpSelect.Controls.Add(Me.txtSekySaki)
        Me.grpSelect.Controls.Add(Me.lblCustNm)
        Me.grpSelect.Controls.Add(Me.txtCustCdSs)
        Me.grpSelect.Controls.Add(Me.txtCustCdS)
        Me.grpSelect.Controls.Add(Me.imdInvDate)
        Me.grpSelect.Controls.Add(Me.txtCustCdM)
        Me.grpSelect.Controls.Add(Me.cmbBr)
        Me.grpSelect.Controls.Add(Me.lblTitleEigyo)
        Me.grpSelect.Controls.Add(Me.txtCustCdL)
        Me.grpSelect.Controls.Add(Me.lblTitleCustCd)
        Me.grpSelect.Controls.Add(Me.chkSelectByNrsB)
        Me.grpSelect.Controls.Add(Me.lblTitleSimebi)
        Me.grpSelect.Controls.Add(Me.cmbPrint)
        Me.grpSelect.Controls.Add(Me.cmbSimebi)
        Me.grpSelect.Controls.Add(Me.lblTitleSeikyuKikan)
        Me.grpSelect.EnableStatus = False
        Me.grpSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSelect.HeightDef = 135
        Me.grpSelect.Location = New System.Drawing.Point(12, 52)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(934, 135)
        Me.grpSelect.TabIndex = 132
        Me.grpSelect.TabStop = False
        Me.grpSelect.Text = "検索・印刷条件"
        Me.grpSelect.TextValue = "検索・印刷条件"
        Me.grpSelect.WidthDef = 934
        '
        'chkMeisaiPrev
        '
        Me.chkMeisaiPrev.AutoSize = True
        Me.chkMeisaiPrev.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMeisaiPrev.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkMeisaiPrev.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkMeisaiPrev.EnableStatus = True
        Me.chkMeisaiPrev.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMeisaiPrev.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMeisaiPrev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMeisaiPrev.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkMeisaiPrev.HeightDef = 17
        Me.chkMeisaiPrev.Location = New System.Drawing.Point(442, 108)
        Me.chkMeisaiPrev.Name = "chkMeisaiPrev"
        Me.chkMeisaiPrev.Size = New System.Drawing.Size(138, 17)
        Me.chkMeisaiPrev.TabIndex = 145
        Me.chkMeisaiPrev.TabStopSetting = True
        Me.chkMeisaiPrev.Text = "プレビューを表示"
        Me.chkMeisaiPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkMeisaiPrev.TextValue = "プレビューを表示"
        Me.chkMeisaiPrev.UseVisualStyleBackColor = True
        Me.chkMeisaiPrev.WidthDef = 138
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
        Me.btnPrint.Location = New System.Drawing.Point(757, 105)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 22)
        Me.btnPrint.TabIndex = 144
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
        Me.lblTitlePrint.Location = New System.Drawing.Point(45, 110)
        Me.lblTitlePrint.Name = "lblTitlePrint"
        Me.lblTitlePrint.Size = New System.Drawing.Size(63, 13)
        Me.lblTitlePrint.TabIndex = 143
        Me.lblTitlePrint.Text = "印刷種別"
        Me.lblTitlePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitlePrint.TextValue = "印刷種別"
        Me.lblTitlePrint.WidthDef = 63
        '
        'btnSeqtoSet
        '
        Me.btnSeqtoSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSeqtoSet.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSeqtoSet.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnSeqtoSet.EnableStatus = True
        Me.btnSeqtoSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSeqtoSet.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSeqtoSet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSeqtoSet.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSeqtoSet.HeightDef = 22
        Me.btnSeqtoSet.Location = New System.Drawing.Point(726, 59)
        Me.btnSeqtoSet.Name = "btnSeqtoSet"
        Me.btnSeqtoSet.Size = New System.Drawing.Size(101, 22)
        Me.btnSeqtoSet.TabIndex = 142
        Me.btnSeqtoSet.TabStopSetting = True
        Me.btnSeqtoSet.Text = "請求先設定"
        Me.btnSeqtoSet.TextValue = "請求先設定"
        Me.btnSeqtoSet.UseVisualStyleBackColor = True
        Me.btnSeqtoSet.WidthDef = 101
        '
        'btnCustSet
        '
        Me.btnCustSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCustSet.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCustSet.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnCustSet.EnableStatus = True
        Me.btnCustSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCustSet.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCustSet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustSet.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCustSet.HeightDef = 22
        Me.btnCustSet.Location = New System.Drawing.Point(726, 36)
        Me.btnCustSet.Name = "btnCustSet"
        Me.btnCustSet.Size = New System.Drawing.Size(101, 22)
        Me.btnCustSet.TabIndex = 141
        Me.btnCustSet.TabStopSetting = True
        Me.btnCustSet.Text = "荷主設定"
        Me.btnCustSet.TextValue = "荷主設定"
        Me.btnCustSet.UseVisualStyleBackColor = True
        Me.btnCustSet.WidthDef = 101
        '
        'numPrintCnt
        '
        Me.numPrintCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrintCnt.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPrintCnt.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintCnt.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.numPrintCnt.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrintCnt.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numPrintCnt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrintCnt.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPrintCnt.HeightDef = 18
        Me.numPrintCnt.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.numPrintCnt.HissuLabelVisible = False
        Me.numPrintCnt.IsHissuCheck = False
        Me.numPrintCnt.IsRangeCheck = False
        Me.numPrintCnt.ItemName = ""
        Me.numPrintCnt.Location = New System.Drawing.Point(686, 107)
        Me.numPrintCnt.Name = "numPrintCnt"
        Me.numPrintCnt.ReadOnly = False
        Me.numPrintCnt.Size = New System.Drawing.Size(45, 18)
        Me.numPrintCnt.TabIndex = 138
        Me.numPrintCnt.TabStopSetting = True
        Me.numPrintCnt.TextValue = "0"
        Me.numPrintCnt.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.numPrintCnt.WidthDef = 45
        '
        'lblBu
        '
        Me.lblBu.AutoSize = True
        Me.lblBu.AutoSizeDef = True
        Me.lblBu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblBu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblBu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblBu.EnableStatus = False
        Me.lblBu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBu.HeightDef = 13
        Me.lblBu.Location = New System.Drawing.Point(729, 109)
        Me.lblBu.Name = "lblBu"
        Me.lblBu.Size = New System.Drawing.Size(21, 13)
        Me.lblBu.TabIndex = 136
        Me.lblBu.Text = "部"
        Me.lblBu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBu.TextValue = "部"
        Me.lblBu.WidthDef = 21
        '
        'lblTitleMeisaiBusu
        '
        Me.lblTitleMeisaiBusu.AutoSize = True
        Me.lblTitleMeisaiBusu.AutoSizeDef = True
        Me.lblTitleMeisaiBusu.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMeisaiBusu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMeisaiBusu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMeisaiBusu.EnableStatus = False
        Me.lblTitleMeisaiBusu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMeisaiBusu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMeisaiBusu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMeisaiBusu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMeisaiBusu.HeightDef = 13
        Me.lblTitleMeisaiBusu.Location = New System.Drawing.Point(590, 110)
        Me.lblTitleMeisaiBusu.Name = "lblTitleMeisaiBusu"
        Me.lblTitleMeisaiBusu.Size = New System.Drawing.Size(91, 13)
        Me.lblTitleMeisaiBusu.TabIndex = 137
        Me.lblTitleMeisaiBusu.Text = "明細印刷部数"
        Me.lblTitleMeisaiBusu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMeisaiBusu.TextValue = "明細印刷部数"
        Me.lblTitleMeisaiBusu.WidthDef = 91
        '
        'lblSeqtoNm
        '
        Me.lblSeqtoNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeqtoNm.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeqtoNm.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeqtoNm.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSeqtoNm.CountWrappedLine = False
        Me.lblSeqtoNm.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.lblSeqtoNm.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeqtoNm.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeqtoNm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeqtoNm.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSeqtoNm.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.lblSeqtoNm.HeightDef = 18
        Me.lblSeqtoNm.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblSeqtoNm.HissuLabelVisible = False
        Me.lblSeqtoNm.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_MIX
        Me.lblSeqtoNm.IsByteCheck = 0
        Me.lblSeqtoNm.IsCalendarCheck = False
        Me.lblSeqtoNm.IsDakutenCheck = False
        Me.lblSeqtoNm.IsEisuCheck = False
        Me.lblSeqtoNm.IsForbiddenWordsCheck = False
        Me.lblSeqtoNm.IsFullByteCheck = 0
        Me.lblSeqtoNm.IsHankakuCheck = False
        Me.lblSeqtoNm.IsHissuCheck = False
        Me.lblSeqtoNm.IsKanaCheck = False
        Me.lblSeqtoNm.IsMiddleSpace = False
        Me.lblSeqtoNm.IsNumericCheck = False
        Me.lblSeqtoNm.IsSujiCheck = False
        Me.lblSeqtoNm.IsZenkakuCheck = False
        Me.lblSeqtoNm.ItemName = ""
        Me.lblSeqtoNm.LineSpace = 0
        Me.lblSeqtoNm.Location = New System.Drawing.Point(193, 61)
        Me.lblSeqtoNm.MaxLength = 0
        Me.lblSeqtoNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblSeqtoNm.MaxLineCount = 0
        Me.lblSeqtoNm.Multiline = False
        Me.lblSeqtoNm.Name = "lblSeqtoNm"
        Me.lblSeqtoNm.ReadOnly = True
        Me.lblSeqtoNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblSeqtoNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblSeqtoNm.Size = New System.Drawing.Size(538, 18)
        Me.lblSeqtoNm.TabIndex = 135
        Me.lblSeqtoNm.TabStop = False
        Me.lblSeqtoNm.TabStopSetting = False
        Me.lblSeqtoNm.TextValue = ""
        Me.lblSeqtoNm.UseSystemPasswordChar = False
        Me.lblSeqtoNm.WidthDef = 538
        Me.lblSeqtoNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'lblTitleSeqto
        '
        Me.lblTitleSeqto.AutoSize = True
        Me.lblTitleSeqto.AutoSizeDef = True
        Me.lblTitleSeqto.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeqto.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeqto.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeqto.EnableStatus = False
        Me.lblTitleSeqto.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeqto.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeqto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeqto.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeqto.HeightDef = 13
        Me.lblTitleSeqto.Location = New System.Drawing.Point(59, 64)
        Me.lblTitleSeqto.Name = "lblTitleSeqto"
        Me.lblTitleSeqto.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeqto.TabIndex = 134
        Me.lblTitleSeqto.Text = "請求先"
        Me.lblTitleSeqto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeqto.TextValue = "請求先"
        Me.lblTitleSeqto.WidthDef = 49
        '
        'txtSekySaki
        '
        Me.txtSekySaki.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSekySaki.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSekySaki.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSekySaki.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSekySaki.CountWrappedLine = False
        Me.txtSekySaki.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtSekySaki.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSekySaki.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSekySaki.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSekySaki.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtSekySaki.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtSekySaki.HeightDef = 18
        Me.txtSekySaki.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtSekySaki.HissuLabelVisible = False
        Me.txtSekySaki.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtSekySaki.IsByteCheck = 7
        Me.txtSekySaki.IsCalendarCheck = False
        Me.txtSekySaki.IsDakutenCheck = False
        Me.txtSekySaki.IsEisuCheck = False
        Me.txtSekySaki.IsForbiddenWordsCheck = False
        Me.txtSekySaki.IsFullByteCheck = 0
        Me.txtSekySaki.IsHankakuCheck = False
        Me.txtSekySaki.IsHissuCheck = False
        Me.txtSekySaki.IsKanaCheck = False
        Me.txtSekySaki.IsMiddleSpace = False
        Me.txtSekySaki.IsNumericCheck = False
        Me.txtSekySaki.IsSujiCheck = False
        Me.txtSekySaki.IsZenkakuCheck = False
        Me.txtSekySaki.ItemName = ""
        Me.txtSekySaki.LineSpace = 0
        Me.txtSekySaki.Location = New System.Drawing.Point(112, 61)
        Me.txtSekySaki.MaxLength = 7
        Me.txtSekySaki.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtSekySaki.MaxLineCount = 0
        Me.txtSekySaki.Multiline = False
        Me.txtSekySaki.Name = "txtSekySaki"
        Me.txtSekySaki.ReadOnly = False
        Me.txtSekySaki.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtSekySaki.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSekySaki.Size = New System.Drawing.Size(97, 18)
        Me.txtSekySaki.TabIndex = 133
        Me.txtSekySaki.TabStopSetting = True
        Me.txtSekySaki.TextValue = ""
        Me.txtSekySaki.UseSystemPasswordChar = False
        Me.txtSekySaki.WidthDef = 97
        Me.txtSekySaki.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblCustNm.Location = New System.Drawing.Point(251, 38)
        Me.lblCustNm.MaxLength = 0
        Me.lblCustNm.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.lblCustNm.MaxLineCount = 0
        Me.lblCustNm.Multiline = False
        Me.lblCustNm.Name = "lblCustNm"
        Me.lblCustNm.ReadOnly = True
        Me.lblCustNm.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.lblCustNm.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.lblCustNm.Size = New System.Drawing.Size(480, 18)
        Me.lblCustNm.TabIndex = 7
        Me.lblCustNm.TabStop = False
        Me.lblCustNm.TabStopSetting = False
        Me.lblCustNm.TextValue = ""
        Me.lblCustNm.UseSystemPasswordChar = False
        Me.lblCustNm.WidthDef = 480
        Me.lblCustNm.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdSs
        '
        Me.txtCustCdSs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdSs.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdSs.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdSs.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdSs.CountWrappedLine = False
        Me.txtCustCdSs.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdSs.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdSs.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdSs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdSs.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdSs.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdSs.HeightDef = 18
        Me.txtCustCdSs.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdSs.HissuLabelVisible = False
        Me.txtCustCdSs.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdSs.IsByteCheck = 2
        Me.txtCustCdSs.IsCalendarCheck = False
        Me.txtCustCdSs.IsDakutenCheck = False
        Me.txtCustCdSs.IsEisuCheck = False
        Me.txtCustCdSs.IsForbiddenWordsCheck = False
        Me.txtCustCdSs.IsFullByteCheck = 0
        Me.txtCustCdSs.IsHankakuCheck = False
        Me.txtCustCdSs.IsHissuCheck = False
        Me.txtCustCdSs.IsKanaCheck = False
        Me.txtCustCdSs.IsMiddleSpace = False
        Me.txtCustCdSs.IsNumericCheck = False
        Me.txtCustCdSs.IsSujiCheck = False
        Me.txtCustCdSs.IsZenkakuCheck = False
        Me.txtCustCdSs.ItemName = ""
        Me.txtCustCdSs.LineSpace = 0
        Me.txtCustCdSs.Location = New System.Drawing.Point(222, 38)
        Me.txtCustCdSs.MaxLength = 2
        Me.txtCustCdSs.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdSs.MaxLineCount = 0
        Me.txtCustCdSs.Multiline = False
        Me.txtCustCdSs.Name = "txtCustCdSs"
        Me.txtCustCdSs.ReadOnly = False
        Me.txtCustCdSs.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdSs.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdSs.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdSs.TabIndex = 132
        Me.txtCustCdSs.TabStopSetting = True
        Me.txtCustCdSs.TextValue = ""
        Me.txtCustCdSs.UseSystemPasswordChar = False
        Me.txtCustCdSs.WidthDef = 45
        Me.txtCustCdSs.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'txtCustCdS
        '
        Me.txtCustCdS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdS.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCustCdS.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustCdS.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtCustCdS.CountWrappedLine = False
        Me.txtCustCdS.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtCustCdS.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdS.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCustCdS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdS.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCustCdS.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtCustCdS.HeightDef = 18
        Me.txtCustCdS.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtCustCdS.HissuLabelVisible = False
        Me.txtCustCdS.InputType = Jp.Co.Nrs.Com.[Const].InputControl.HAN_NUM_ALPHA
        Me.txtCustCdS.IsByteCheck = 2
        Me.txtCustCdS.IsCalendarCheck = False
        Me.txtCustCdS.IsDakutenCheck = False
        Me.txtCustCdS.IsEisuCheck = False
        Me.txtCustCdS.IsForbiddenWordsCheck = False
        Me.txtCustCdS.IsFullByteCheck = 0
        Me.txtCustCdS.IsHankakuCheck = False
        Me.txtCustCdS.IsHissuCheck = False
        Me.txtCustCdS.IsKanaCheck = False
        Me.txtCustCdS.IsMiddleSpace = False
        Me.txtCustCdS.IsNumericCheck = False
        Me.txtCustCdS.IsSujiCheck = False
        Me.txtCustCdS.IsZenkakuCheck = False
        Me.txtCustCdS.ItemName = ""
        Me.txtCustCdS.LineSpace = 0
        Me.txtCustCdS.Location = New System.Drawing.Point(193, 38)
        Me.txtCustCdS.MaxLength = 2
        Me.txtCustCdS.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdS.MaxLineCount = 0
        Me.txtCustCdS.Multiline = False
        Me.txtCustCdS.Name = "txtCustCdS"
        Me.txtCustCdS.ReadOnly = False
        Me.txtCustCdS.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdS.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdS.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdS.TabIndex = 131
        Me.txtCustCdS.TabStopSetting = True
        Me.txtCustCdS.TextValue = ""
        Me.txtCustCdS.UseSystemPasswordChar = False
        Me.txtCustCdS.WidthDef = 45
        Me.txtCustCdS.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'imdInvDate
        '
        Me.imdInvDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.imdInvDate.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imdInvDate.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.imdInvDate.DisplayFormat = New GrapeCity.Win.Editors.Fields.DateDisplayField() {CType(DateYearDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateMonthDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateLiteralDisplayField2, GrapeCity.Win.Editors.Fields.DateDisplayField), CType(DateDayDisplayField1, GrapeCity.Win.Editors.Fields.DateDisplayField)}
        Me.imdInvDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.imdInvDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.imdInvDate.Format = New GrapeCity.Win.Editors.Fields.DateField() {CType(DateYearField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateMonthField1, GrapeCity.Win.Editors.Fields.DateField), CType(DateDayField1, GrapeCity.Win.Editors.Fields.DateField)}
        Me.imdInvDate.HeightDef = 18
        Me.imdInvDate.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.imdInvDate.HissuLabelVisible = False
        Me.imdInvDate.Holiday = True
        Me.imdInvDate.IsAfterDateCheck = False
        Me.imdInvDate.IsBeforeDateCheck = False
        Me.imdInvDate.IsHissuCheck = False
        Me.imdInvDate.IsMinDateCheck = "1900/01/01"
        Me.imdInvDate.ItemName = ""
        Me.imdInvDate.Location = New System.Drawing.Point(309, 84)
        Me.imdInvDate.Name = "imdInvDate"
        Me.imdInvDate.Number = CType(10101000000, Long)
        Me.imdInvDate.ReadOnly = False
        Me.imdInvDate.Size = New System.Drawing.Size(115, 18)
        Me.imdInvDate.TabIndex = 130
        Me.imdInvDate.TabStopSetting = True
        Me.imdInvDate.TextValue = ""
        Me.imdInvDate.Value = New Date(CType(0, Long))
        Me.imdInvDate.WidthDef = 115
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
        Me.txtCustCdM.Location = New System.Drawing.Point(164, 38)
        Me.txtCustCdM.MaxLength = 2
        Me.txtCustCdM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdM.MaxLineCount = 0
        Me.txtCustCdM.Multiline = False
        Me.txtCustCdM.Name = "txtCustCdM"
        Me.txtCustCdM.ReadOnly = False
        Me.txtCustCdM.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdM.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdM.Size = New System.Drawing.Size(45, 18)
        Me.txtCustCdM.TabIndex = 18
        Me.txtCustCdM.TabStopSetting = True
        Me.txtCustCdM.TextValue = ""
        Me.txtCustCdM.UseSystemPasswordChar = False
        Me.txtCustCdM.WidthDef = 45
        Me.txtCustCdM.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBr.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbBr.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbBr.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbBr.DataSource = Nothing
        Me.cmbBr.DisplayMember = Nothing
        Me.cmbBr.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbBr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbBr.HeightDef = 18
        Me.cmbBr.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbBr.HissuLabelVisible = True
        Me.cmbBr.InsertWildCard = True
        Me.cmbBr.IsForbiddenWordsCheck = False
        Me.cmbBr.IsHissuCheck = True
        Me.cmbBr.ItemName = ""
        Me.cmbBr.Location = New System.Drawing.Point(112, 15)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.ReadOnly = False
        Me.cmbBr.SelectedIndex = -1
        Me.cmbBr.SelectedItem = Nothing
        Me.cmbBr.SelectedText = ""
        Me.cmbBr.SelectedValue = ""
        Me.cmbBr.Size = New System.Drawing.Size(300, 18)
        Me.cmbBr.TabIndex = 128
        Me.cmbBr.TabStopSetting = True
        Me.cmbBr.TextValue = ""
        Me.cmbBr.ValueMember = Nothing
        Me.cmbBr.WidthDef = 300
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
        Me.lblTitleEigyo.Location = New System.Drawing.Point(59, 18)
        Me.lblTitleEigyo.Name = "lblTitleEigyo"
        Me.lblTitleEigyo.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleEigyo.TabIndex = 1
        Me.lblTitleEigyo.Text = "営業所"
        Me.lblTitleEigyo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleEigyo.TextValue = "営業所"
        Me.lblTitleEigyo.WidthDef = 49
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
        Me.txtCustCdL.Location = New System.Drawing.Point(112, 38)
        Me.txtCustCdL.MaxLength = 5
        Me.txtCustCdL.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtCustCdL.MaxLineCount = 0
        Me.txtCustCdL.Multiline = False
        Me.txtCustCdL.Name = "txtCustCdL"
        Me.txtCustCdL.ReadOnly = False
        Me.txtCustCdL.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtCustCdL.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCustCdL.Size = New System.Drawing.Size(68, 18)
        Me.txtCustCdL.TabIndex = 6
        Me.txtCustCdL.TabStopSetting = True
        Me.txtCustCdL.TextValue = "XXXXX"
        Me.txtCustCdL.UseSystemPasswordChar = False
        Me.txtCustCdL.WidthDef = 68
        Me.txtCustCdL.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
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
        Me.lblTitleCustCd.Location = New System.Drawing.Point(73, 41)
        Me.lblTitleCustCd.Name = "lblTitleCustCd"
        Me.lblTitleCustCd.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleCustCd.TabIndex = 5
        Me.lblTitleCustCd.Text = "荷主"
        Me.lblTitleCustCd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleCustCd.TextValue = "荷主"
        Me.lblTitleCustCd.WidthDef = 35
        '
        'chkSelectByNrsB
        '
        Me.chkSelectByNrsB.AutoSize = True
        Me.chkSelectByNrsB.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSelectByNrsB.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSelectByNrsB.Checked = True
        Me.chkSelectByNrsB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSelectByNrsB.EnableStatus = True
        Me.chkSelectByNrsB.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSelectByNrsB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSelectByNrsB.HeightDef = 17
        Me.chkSelectByNrsB.Location = New System.Drawing.Point(442, 85)
        Me.chkSelectByNrsB.Name = "chkSelectByNrsB"
        Me.chkSelectByNrsB.Size = New System.Drawing.Size(96, 17)
        Me.chkSelectByNrsB.TabIndex = 3
        Me.chkSelectByNrsB.TabStopSetting = True
        Me.chkSelectByNrsB.Text = "私の作成分"
        Me.chkSelectByNrsB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSelectByNrsB.TextValue = "私の作成分"
        Me.chkSelectByNrsB.UseVisualStyleBackColor = True
        Me.chkSelectByNrsB.WidthDef = 96
        '
        'lblTitleSimebi
        '
        Me.lblTitleSimebi.AutoSize = True
        Me.lblTitleSimebi.AutoSizeDef = True
        Me.lblTitleSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSimebi.EnableStatus = False
        Me.lblTitleSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSimebi.HeightDef = 13
        Me.lblTitleSimebi.Location = New System.Drawing.Point(73, 87)
        Me.lblTitleSimebi.Name = "lblTitleSimebi"
        Me.lblTitleSimebi.Size = New System.Drawing.Size(35, 13)
        Me.lblTitleSimebi.TabIndex = 21
        Me.lblTitleSimebi.Text = "締日"
        Me.lblTitleSimebi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSimebi.TextValue = "締日"
        Me.lblTitleSimebi.WidthDef = 35
        '
        'cmbPrint
        '
        Me.cmbPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbPrint.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbPrint.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbPrint.DataCode = "S066"
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
        Me.cmbPrint.Location = New System.Drawing.Point(112, 107)
        Me.cmbPrint.Name = "cmbPrint"
        Me.cmbPrint.ReadOnly = False
        Me.cmbPrint.SelectedIndex = -1
        Me.cmbPrint.SelectedItem = Nothing
        Me.cmbPrint.SelectedText = ""
        Me.cmbPrint.SelectedValue = ""
        Me.cmbPrint.Size = New System.Drawing.Size(312, 18)
        Me.cmbPrint.TabIndex = 27
        Me.cmbPrint.TabStop = False
        Me.cmbPrint.TabStopSetting = False
        Me.cmbPrint.TextValue = ""
        Me.cmbPrint.Value1 = Nothing
        Me.cmbPrint.Value2 = Nothing
        Me.cmbPrint.Value3 = Nothing
        Me.cmbPrint.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbPrint.ValueMember = Nothing
        Me.cmbPrint.WidthDef = 312
        '
        'cmbSimebi
        '
        Me.cmbSimebi.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbSimebi.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbSimebi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbSimebi.DataCode = "S008"
        Me.cmbSimebi.DataSource = Nothing
        Me.cmbSimebi.DisplayMember = Nothing
        Me.cmbSimebi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbSimebi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbSimebi.HeightDef = 18
        Me.cmbSimebi.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbSimebi.HissuLabelVisible = True
        Me.cmbSimebi.InsertWildCard = True
        Me.cmbSimebi.IsForbiddenWordsCheck = False
        Me.cmbSimebi.IsHissuCheck = True
        Me.cmbSimebi.ItemName = ""
        Me.cmbSimebi.KubunNmColumn = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.DISP_MEMBERS.KBN_NM1
        Me.cmbSimebi.Location = New System.Drawing.Point(112, 84)
        Me.cmbSimebi.Name = "cmbSimebi"
        Me.cmbSimebi.ReadOnly = False
        Me.cmbSimebi.SelectedIndex = -1
        Me.cmbSimebi.SelectedItem = Nothing
        Me.cmbSimebi.SelectedText = ""
        Me.cmbSimebi.SelectedValue = ""
        Me.cmbSimebi.Size = New System.Drawing.Size(127, 18)
        Me.cmbSimebi.TabIndex = 27
        Me.cmbSimebi.TabStopSetting = True
        Me.cmbSimebi.TextValue = ""
        Me.cmbSimebi.Value1 = Nothing
        Me.cmbSimebi.Value2 = Nothing
        Me.cmbSimebi.Value3 = Nothing
        Me.cmbSimebi.ValueExtractPattern = Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun.VALUE_EXTRACT_PATTERN.NONE
        Me.cmbSimebi.ValueMember = Nothing
        Me.cmbSimebi.WidthDef = 127
        '
        'lblTitleSeikyuKikan
        '
        Me.lblTitleSeikyuKikan.AutoSize = True
        Me.lblTitleSeikyuKikan.AutoSizeDef = True
        Me.lblTitleSeikyuKikan.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuKikan.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleSeikyuKikan.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleSeikyuKikan.EnableStatus = False
        Me.lblTitleSeikyuKikan.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuKikan.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleSeikyuKikan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuKikan.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleSeikyuKikan.HeightDef = 13
        Me.lblTitleSeikyuKikan.Location = New System.Drawing.Point(256, 87)
        Me.lblTitleSeikyuKikan.Name = "lblTitleSeikyuKikan"
        Me.lblTitleSeikyuKikan.Size = New System.Drawing.Size(49, 13)
        Me.lblTitleSeikyuKikan.TabIndex = 19
        Me.lblTitleSeikyuKikan.Text = "請求月"
        Me.lblTitleSeikyuKikan.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleSeikyuKikan.TextValue = "請求月"
        Me.lblTitleSeikyuKikan.WidthDef = 49
        '
        'grpMode
        '
        Me.grpMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpMode.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpMode.Controls.Add(Me.lblTitleMode)
        Me.grpMode.Controls.Add(Me.optSeikyuC)
        Me.grpMode.Controls.Add(Me.optSeikyuH)
        Me.grpMode.EnableStatus = False
        Me.grpMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpMode.HeightDef = 39
        Me.grpMode.Location = New System.Drawing.Point(12, 7)
        Me.grpMode.Name = "grpMode"
        Me.grpMode.Size = New System.Drawing.Size(280, 39)
        Me.grpMode.TabIndex = 133
        Me.grpMode.TabStop = False
        Me.grpMode.Text = "モード条件"
        Me.grpMode.TextValue = "モード条件"
        Me.grpMode.WidthDef = 280
        '
        'lblTitleMode
        '
        Me.lblTitleMode.AutoSize = True
        Me.lblTitleMode.AutoSizeDef = True
        Me.lblTitleMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMode.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.lblTitleMode.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.lblTitleMode.EnableStatus = False
        Me.lblTitleMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMode.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitleMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMode.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTitleMode.HeightDef = 13
        Me.lblTitleMode.Location = New System.Drawing.Point(28, 17)
        Me.lblTitleMode.Name = "lblTitleMode"
        Me.lblTitleMode.Size = New System.Drawing.Size(77, 13)
        Me.lblTitleMode.TabIndex = 28
        Me.lblTitleMode.Text = "実行モード"
        Me.lblTitleMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTitleMode.TextValue = "実行モード"
        Me.lblTitleMode.WidthDef = 77
        '
        'optSeikyuC
        '
        Me.optSeikyuC.AutoSize = True
        Me.optSeikyuC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuC.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuC.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSeikyuC.EnableStatus = True
        Me.optSeikyuC.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuC.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuC.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuC.HeightDef = 17
        Me.optSeikyuC.Location = New System.Drawing.Point(179, 15)
        Me.optSeikyuC.Name = "optSeikyuC"
        Me.optSeikyuC.Size = New System.Drawing.Size(81, 17)
        Me.optSeikyuC.TabIndex = 1
        Me.optSeikyuC.TabStop = True
        Me.optSeikyuC.TabStopSetting = True
        Me.optSeikyuC.Text = "チェック"
        Me.optSeikyuC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSeikyuC.TextValue = "チェック"
        Me.optSeikyuC.UseVisualStyleBackColor = True
        Me.optSeikyuC.WidthDef = 81
        '
        'optSeikyuH
        '
        Me.optSeikyuH.AutoSize = True
        Me.optSeikyuH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuH.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.optSeikyuH.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.optSeikyuH.EnableStatus = True
        Me.optSeikyuH.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuH.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.optSeikyuH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuH.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.optSeikyuH.HeightDef = 17
        Me.optSeikyuH.Location = New System.Drawing.Point(120, 15)
        Me.optSeikyuH.Name = "optSeikyuH"
        Me.optSeikyuH.Size = New System.Drawing.Size(53, 17)
        Me.optSeikyuH.TabIndex = 2
        Me.optSeikyuH.TabStop = True
        Me.optSeikyuH.TabStopSetting = True
        Me.optSeikyuH.Text = "本番"
        Me.optSeikyuH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optSeikyuH.TextValue = "本番"
        Me.optSeikyuH.UseVisualStyleBackColor = True
        Me.optSeikyuH.WidthDef = 53
        '
        'LMG020F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMG020F"
        Me.Text = "【LMG020】 保管料・荷役料計算 [明細検索]"
        Me.pnlViewAria.ResumeLayout(False)
        CType(Me.sprMeisai, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSelect.ResumeLayout(False)
        Me.grpSelect.PerformLayout()
        Me.grpMode.ResumeLayout(False)
        Me.grpMode.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sprMeisai As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents grpSelect As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents imdInvDate As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate
    Friend WithEvents lblCustNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdM As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbBr As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboNrsBr
    Friend WithEvents lblTitleEigyo As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtCustCdL As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleCustCd As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkSelectByNrsB As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblTitleSimebi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbSimebi As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents lblTitleSeikyuKikan As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents numPrintCnt As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents lblBu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblTitleMeisaiBusu As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblSeqtoNm As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents lblTitleSeqto As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtSekySaki As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdSs As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents txtCustCdS As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents btnPrint As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents lblTitlePrint As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents btnSeqtoSet As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents btnCustSet As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents chkMeisaiPrev As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents cmbPrint As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboKubun
    Friend WithEvents grpMode As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents lblTitleMode As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents optSeikyuC As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton
    Friend WithEvents optSeikyuH As Jp.Co.Nrs.LM.GUI.Win.LMOptionButton

End Class
