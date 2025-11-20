<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMN060F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMN060F))
        Me.grpSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbCustCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.lblKakushiCustCd = New System.Windows.Forms.TextBox
        Me.chkDispSoko = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.chkKeppinOnly = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.LmTitleLabel11 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.lblZaikoNissuSanshutsuDate = New System.Windows.Forms.TextBox
        Me.pnlViewAria.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.lblZaikoNissuSanshutsuDate)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.grpSearch)
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSearch.Controls.Add(Me.cmbCustCd)
        Me.grpSearch.Controls.Add(Me.lblKakushiCustCd)
        Me.grpSearch.Controls.Add(Me.chkDispSoko)
        Me.grpSearch.Controls.Add(Me.chkKeppinOnly)
        Me.grpSearch.Controls.Add(Me.LmTitleLabel11)
        Me.grpSearch.EnableStatus = False
        Me.grpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.HeightDef = 45
        Me.grpSearch.Location = New System.Drawing.Point(12, 6)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(1248, 45)
        Me.grpSearch.TabIndex = 2
        Me.grpSearch.TabStop = False
        Me.grpSearch.TextValue = ""
        Me.grpSearch.WidthDef = 1248
        '
        'cmbCustCd
        '
        Me.cmbCustCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCustCd.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbCustCd.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbCustCd.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbCustCd.DataSource = Nothing
        Me.cmbCustCd.DisplayMember = Nothing
        Me.cmbCustCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCustCd.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCustCd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCustCd.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbCustCd.HeightDef = 22
        Me.cmbCustCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCustCd.HissuLabelVisible = False
        Me.cmbCustCd.InsertWildCard = True
        Me.cmbCustCd.IsForbiddenWordsCheck = False
        Me.cmbCustCd.IsHissuCheck = False
        Me.cmbCustCd.ItemName = ""
        Me.cmbCustCd.Location = New System.Drawing.Point(87, 12)
        Me.cmbCustCd.Name = "cmbCustCd"
        Me.cmbCustCd.ReadOnly = False
        Me.cmbCustCd.SelectedIndex = -1
        Me.cmbCustCd.SelectedItem = Nothing
        Me.cmbCustCd.SelectedText = ""
        Me.cmbCustCd.SelectedValue = ""
        Me.cmbCustCd.Size = New System.Drawing.Size(210, 22)
        Me.cmbCustCd.TabIndex = 82
        Me.cmbCustCd.TabStopSetting = True
        Me.cmbCustCd.TextValue = ""
        Me.cmbCustCd.ValueMember = Nothing
        Me.cmbCustCd.WidthDef = 210
        '
        'lblKakushiCustCd
        '
        Me.lblKakushiCustCd.Enabled = False
        Me.lblKakushiCustCd.Location = New System.Drawing.Point(995, 13)
        Me.lblKakushiCustCd.Name = "lblKakushiCustCd"
        Me.lblKakushiCustCd.ReadOnly = True
        Me.lblKakushiCustCd.Size = New System.Drawing.Size(148, 20)
        Me.lblKakushiCustCd.TabIndex = 81
        Me.lblKakushiCustCd.TabStop = False
        Me.lblKakushiCustCd.Visible = False
        '
        'chkDispSoko
        '
        Me.chkDispSoko.AutoSize = True
        Me.chkDispSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDispSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkDispSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkDispSoko.Checked = True
        Me.chkDispSoko.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDispSoko.EnableStatus = True
        Me.chkDispSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDispSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkDispSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDispSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkDispSoko.HeightDef = 17
        Me.chkDispSoko.Location = New System.Drawing.Point(560, 15)
        Me.chkDispSoko.Name = "chkDispSoko"
        Me.chkDispSoko.Size = New System.Drawing.Size(278, 17)
        Me.chkDispSoko.TabIndex = 17
        Me.chkDispSoko.TabStopSetting = True
        Me.chkDispSoko.Text = "オーダー・在庫のない倉庫は表示しない"
        Me.chkDispSoko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkDispSoko.TextValue = "オーダー・在庫のない倉庫は表示しない"
        Me.chkDispSoko.UseVisualStyleBackColor = True
        Me.chkDispSoko.WidthDef = 278
        '
        'chkKeppinOnly
        '
        Me.chkKeppinOnly.AutoSize = True
        Me.chkKeppinOnly.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeppinOnly.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkKeppinOnly.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkKeppinOnly.EnableStatus = True
        Me.chkKeppinOnly.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeppinOnly.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkKeppinOnly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeppinOnly.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkKeppinOnly.HeightDef = 17
        Me.chkKeppinOnly.Location = New System.Drawing.Point(304, 15)
        Me.chkKeppinOnly.Name = "chkKeppinOnly"
        Me.chkKeppinOnly.Size = New System.Drawing.Size(250, 17)
        Me.chkKeppinOnly.TabIndex = 3
        Me.chkKeppinOnly.TabStopSetting = True
        Me.chkKeppinOnly.Text = "欠品・欠品危惧を含む品目のみ表示"
        Me.chkKeppinOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkKeppinOnly.TextValue = "欠品・欠品危惧を含む品目のみ表示"
        Me.chkKeppinOnly.UseVisualStyleBackColor = True
        Me.chkKeppinOnly.WidthDef = 250
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
        Me.LmTitleLabel11.Location = New System.Drawing.Point(49, 16)
        Me.LmTitleLabel11.Name = "LmTitleLabel11"
        Me.LmTitleLabel11.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel11.TabIndex = 3
        Me.LmTitleLabel11.Text = "荷主"
        Me.LmTitleLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel11.TextValue = "荷主"
        Me.LmTitleLabel11.WidthDef = 35
        '
        'sprDetail
        '
        Me.sprDetail.AccessibleDescription = ""
        Me.sprDetail.AllowUserZoom = False
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
        Me.sprDetail.HeightDef = 785
        Me.sprDetail.Location = New System.Drawing.Point(5, 99)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        '
        '
        '
        Reset()
        Me.sprDetail.SetViewportTopRow(0, 0, 1)
        Me.sprDetail.SetActiveViewport(0, -1, 0)
        Me.sprDetail.Size = New System.Drawing.Size(1257, 785)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1257
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(987, 79)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(119, 13)
        Me.LmTitleLabel1.TabIndex = 16
        Me.LmTitleLabel1.Text = "在庫日数算出日時"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "在庫日数算出日時"
        Me.LmTitleLabel1.WidthDef = 119
        '
        'lblZaikoNissuSanshutsuDate
        '
        Me.lblZaikoNissuSanshutsuDate.Location = New System.Drawing.Point(1112, 76)
        Me.lblZaikoNissuSanshutsuDate.Name = "lblZaikoNissuSanshutsuDate"
        Me.lblZaikoNissuSanshutsuDate.ReadOnly = True
        Me.lblZaikoNissuSanshutsuDate.Size = New System.Drawing.Size(148, 20)
        Me.lblZaikoNissuSanshutsuDate.TabIndex = 17
        '
        'LMN060F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMN060F"
        Me.Text = "【LMN060】 拠点別在庫一覧"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents LmTitleLabel11 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents chkKeppinOnly As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents chkDispSoko As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents lblZaikoNissuSanshutsuDate As System.Windows.Forms.TextBox
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents lblKakushiCustCd As System.Windows.Forms.TextBox
    Friend WithEvents cmbCustCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo

End Class
