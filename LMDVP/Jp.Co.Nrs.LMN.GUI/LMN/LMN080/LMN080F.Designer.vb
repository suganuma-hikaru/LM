<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMN080F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMN080F))
        Me.LmTitleLabel11 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.sprSokoDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Me.LmTitleLabel1 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.LmImNumber1 = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
        Me.LmTitleLabel2 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbCustCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.LmTitleLabel3 = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.cmbWare = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko
        Me.pnlViewAria.SuspendLayout()
        CType(Me.sprSokoDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.cmbWare)
        Me.pnlViewAria.Controls.Add(Me.grpSearch)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel1)
        Me.pnlViewAria.Controls.Add(Me.LmImNumber1)
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.sprSokoDetail)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel2)
        Me.pnlViewAria.Controls.Add(Me.LmTitleLabel11)
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
        Me.LmTitleLabel11.Location = New System.Drawing.Point(77, 215)
        Me.LmTitleLabel11.Name = "LmTitleLabel11"
        Me.LmTitleLabel11.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel11.TabIndex = 3
        Me.LmTitleLabel11.Text = "倉庫"
        Me.LmTitleLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel11.TextValue = "倉庫"
        Me.LmTitleLabel11.WidthDef = 35
        '
        'sprSokoDetail
        '
        Me.sprSokoDetail.AccessibleDescription = ""
        Me.sprSokoDetail.AllowUserZoom = False
        Me.sprSokoDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprSokoDetail.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.sprSokoDetail.BorderStyleDef = System.Windows.Forms.BorderStyle.Fixed3D
        Me.sprSokoDetail.CellClickEventArgs = Nothing
        Me.sprSokoDetail.CheckToCheckBox = True
        Me.sprSokoDetail.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders
        Me.sprSokoDetail.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprSokoDetail.EditModeReplace = True
        Me.sprSokoDetail.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSokoDetail.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sprSokoDetail.ForeColorDef = System.Drawing.Color.Empty
        Me.sprSokoDetail.HeightDef = 133
        Me.sprSokoDetail.Location = New System.Drawing.Point(12, 70)
        Me.sprSokoDetail.Name = "sprSokoDetail"
        Me.sprSokoDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprSokoDetail.Size = New System.Drawing.Size(1248, 133)
        Me.sprSokoDetail.SortColumn = True
        Me.sprSokoDetail.SpanColumnLock = True
        Me.sprSokoDetail.SpreadDoubleClicked = False
        Me.sprSokoDetail.TabIndex = 16
        Me.sprSokoDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprSokoDetail.TextValue = Nothing
        Me.sprSokoDetail.WidthDef = 1248
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
        Me.sprDetail.HeightDef = 633
        Me.sprDetail.Location = New System.Drawing.Point(12, 263)
        Me.sprDetail.Name = "sprDetail"
        Me.sprDetail.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        '
        '
        '
        Reset()
        Me.sprDetail.Size = New System.Drawing.Size(1257, 633)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 17
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
        Me.LmTitleLabel1.Location = New System.Drawing.Point(152, 243)
        Me.LmTitleLabel1.Name = "LmTitleLabel1"
        Me.LmTitleLabel1.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel1.TabIndex = 3
        Me.LmTitleLabel1.Text = "品目"
        Me.LmTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel1.TextValue = "品目"
        Me.LmTitleLabel1.WidthDef = 35
        '
        'LmImNumber1
        '
        Me.LmImNumber1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LmImNumber1.ContentAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.LmImNumber1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LmImNumber1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LmImNumber1.HeightDef = 17
        Me.LmImNumber1.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.LmImNumber1.HissuLabelVisible = False
        Me.LmImNumber1.IsHissuCheck = False
        Me.LmImNumber1.IsRangeCheck = False
        Me.LmImNumber1.ItemName = ""
        Me.LmImNumber1.Location = New System.Drawing.Point(116, 240)
        Me.LmImNumber1.Name = "LmImNumber1"
        Me.LmImNumber1.ReadOnly = True
        Me.LmImNumber1.Size = New System.Drawing.Size(49, 17)
        Me.LmImNumber1.TabIndex = 18
        Me.LmImNumber1.TabStop = False
        Me.LmImNumber1.TabStopSetting = False
        Me.LmImNumber1.TextValue = "0"
        Me.LmImNumber1.Value = New Decimal(New Integer() {0, 0, 0, 0})
        Me.LmImNumber1.WidthDef = 49
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
        Me.LmTitleLabel2.Location = New System.Drawing.Point(21, 243)
        Me.LmTitleLabel2.Name = "LmTitleLabel2"
        Me.LmTitleLabel2.Size = New System.Drawing.Size(91, 13)
        Me.LmTitleLabel2.TabIndex = 3
        Me.LmTitleLabel2.Text = "欠品品目件数"
        Me.LmTitleLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel2.TextValue = "欠品品目件数"
        Me.LmTitleLabel2.WidthDef = 91
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSearch.Controls.Add(Me.cmbCustCd)
        Me.grpSearch.Controls.Add(Me.LmTitleLabel3)
        Me.grpSearch.EnableStatus = False
        Me.grpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.HeightDef = 49
        Me.grpSearch.Location = New System.Drawing.Point(12, 15)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(505, 49)
        Me.grpSearch.TabIndex = 19
        Me.grpSearch.TabStop = False
        Me.grpSearch.TextValue = ""
        Me.grpSearch.WidthDef = 505
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
        Me.cmbCustCd.Location = New System.Drawing.Point(104, 17)
        Me.cmbCustCd.Name = "cmbCustCd"
        Me.cmbCustCd.ReadOnly = False
        Me.cmbCustCd.SelectedIndex = -1
        Me.cmbCustCd.SelectedItem = Nothing
        Me.cmbCustCd.SelectedText = ""
        Me.cmbCustCd.SelectedValue = ""
        Me.cmbCustCd.Size = New System.Drawing.Size(210, 22)
        Me.cmbCustCd.TabIndex = 18
        Me.cmbCustCd.TabStopSetting = True
        Me.cmbCustCd.TextValue = ""
        Me.cmbCustCd.ValueMember = Nothing
        Me.cmbCustCd.WidthDef = 210
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
        Me.LmTitleLabel3.Location = New System.Drawing.Point(65, 22)
        Me.LmTitleLabel3.Name = "LmTitleLabel3"
        Me.LmTitleLabel3.Size = New System.Drawing.Size(35, 13)
        Me.LmTitleLabel3.TabIndex = 17
        Me.LmTitleLabel3.Text = "荷主"
        Me.LmTitleLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LmTitleLabel3.TextValue = "荷主"
        Me.LmTitleLabel3.WidthDef = 35
        '
        'cmbWare
        '
        Me.cmbWare.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWare.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWare.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cmbWare.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmbWare.DataSource = Nothing
        Me.cmbWare.DisplayMember = Nothing
        Me.cmbWare.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWare.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbWare.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWare.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmbWare.HeightDef = 22
        Me.cmbWare.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbWare.HissuLabelVisible = False
        Me.cmbWare.InsertWildCard = True
        Me.cmbWare.IsForbiddenWordsCheck = False
        Me.cmbWare.IsHissuCheck = False
        Me.cmbWare.ItemName = ""
        Me.cmbWare.Location = New System.Drawing.Point(116, 210)
        Me.cmbWare.Name = "cmbWare"
        Me.cmbWare.ReadOnly = True
        Me.cmbWare.SelectedIndex = -1
        Me.cmbWare.SelectedItem = Nothing
        Me.cmbWare.SelectedText = ""
        Me.cmbWare.SelectedValue = ""
        Me.cmbWare.Size = New System.Drawing.Size(210, 22)
        Me.cmbWare.TabIndex = 20
        Me.cmbWare.TabStop = False
        Me.cmbWare.TabStopSetting = False
        Me.cmbWare.TextValue = ""
        Me.cmbWare.ValueMember = Nothing
        Me.cmbWare.WidthDef = 210
        '
        'LMN080F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMN080F"
        Me.Text = "【LMN080】 欠品警告"
        Me.pnlViewAria.ResumeLayout(False)
        Me.pnlViewAria.PerformLayout()
        CType(Me.sprSokoDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LmTitleLabel11 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents sprSokoDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
    Friend WithEvents LmTitleLabel1 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents LmImNumber1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImNumber
    Friend WithEvents LmTitleLabel2 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents cmbCustCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents LmTitleLabel3 As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents cmbWare As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMComboSoko

End Class
