<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LMM350F
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LMM350F))
        Me.grpSearch = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbCustCd = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.TitlelblZip = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.txtDestZip = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
        Me.TitlelblNinushi = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.chkSoukoMisettei = New Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
        Me.TitlelblSoko = New Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
        Me.grpSokoSet = New Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
        Me.cmbSoko = New Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
        Me.btnIkkatsu = New Jp.Co.Nrs.LM.GUI.Win.LMButton
        Me.sprDetail = New Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
        Me.pnlViewAria.SuspendLayout()
        Me.grpSearch.SuspendLayout()
        Me.grpSokoSet.SuspendLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlViewAria
        '
        Me.pnlViewAria.Controls.Add(Me.sprDetail)
        Me.pnlViewAria.Controls.Add(Me.grpSokoSet)
        Me.pnlViewAria.Controls.Add(Me.grpSearch)
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSearch.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSearch.Controls.Add(Me.cmbCustCd)
        Me.grpSearch.Controls.Add(Me.TitlelblZip)
        Me.grpSearch.Controls.Add(Me.txtDestZip)
        Me.grpSearch.Controls.Add(Me.TitlelblNinushi)
        Me.grpSearch.Controls.Add(Me.chkSoukoMisettei)
        Me.grpSearch.EnableStatus = False
        Me.grpSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSearch.HeightDef = 51
        Me.grpSearch.Location = New System.Drawing.Point(12, 6)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Size = New System.Drawing.Size(1245, 51)
        Me.grpSearch.TabIndex = 2
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "検索条件"
        Me.grpSearch.TextValue = "検索条件"
        Me.grpSearch.WidthDef = 1245
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
        Me.cmbCustCd.HeightDef = 18
        Me.cmbCustCd.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.cmbCustCd.HissuLabelVisible = True
        Me.cmbCustCd.InsertWildCard = True
        Me.cmbCustCd.IsForbiddenWordsCheck = False
        Me.cmbCustCd.IsHissuCheck = True
        Me.cmbCustCd.ItemName = ""
        Me.cmbCustCd.Location = New System.Drawing.Point(82, 18)
        Me.cmbCustCd.Name = "cmbCustCd"
        Me.cmbCustCd.ReadOnly = False
        Me.cmbCustCd.SelectedIndex = -1
        Me.cmbCustCd.SelectedItem = Nothing
        Me.cmbCustCd.SelectedText = ""
        Me.cmbCustCd.SelectedValue = ""
        Me.cmbCustCd.Size = New System.Drawing.Size(211, 18)
        Me.cmbCustCd.TabIndex = 76
        Me.cmbCustCd.TabStopSetting = True
        Me.cmbCustCd.TextValue = ""
        Me.cmbCustCd.ValueMember = Nothing
        Me.cmbCustCd.WidthDef = 211
        '
        'TitlelblZip
        '
        Me.TitlelblZip.AutoSize = True
        Me.TitlelblZip.AutoSizeDef = True
        Me.TitlelblZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblZip.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblZip.EnableStatus = False
        Me.TitlelblZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblZip.HeightDef = 13
        Me.TitlelblZip.Location = New System.Drawing.Point(470, 21)
        Me.TitlelblZip.Name = "TitlelblZip"
        Me.TitlelblZip.Size = New System.Drawing.Size(63, 13)
        Me.TitlelblZip.TabIndex = 75
        Me.TitlelblZip.Text = "郵便番号"
        Me.TitlelblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblZip.TextValue = "郵便番号"
        Me.TitlelblZip.WidthDef = 63
        '
        'txtDestZip
        '
        Me.txtDestZip.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestZip.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDestZip.BorderStyleDef = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestZip.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDestZip.CountWrappedLine = False
        Me.txtDestZip.DisplayAlignment = GrapeCity.Win.Editors.DisplayAlignment.None
        Me.txtDestZip.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZip.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDestZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZip.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDestZip.GridLine = GrapeCity.Win.Editors.LineStyle.None
        Me.txtDestZip.HeightDef = 18
        Me.txtDestZip.HissuLabelBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.txtDestZip.HissuLabelVisible = False
        Me.txtDestZip.InputType = Jp.Co.Nrs.Com.[Const].InputControl.ALL_HANKAKU
        Me.txtDestZip.IsByteCheck = 10
        Me.txtDestZip.IsCalendarCheck = False
        Me.txtDestZip.IsDakutenCheck = False
        Me.txtDestZip.IsEisuCheck = False
        Me.txtDestZip.IsForbiddenWordsCheck = False
        Me.txtDestZip.IsFullByteCheck = 0
        Me.txtDestZip.IsHankakuCheck = False
        Me.txtDestZip.IsHissuCheck = False
        Me.txtDestZip.IsKanaCheck = False
        Me.txtDestZip.IsMiddleSpace = False
        Me.txtDestZip.IsNumericCheck = False
        Me.txtDestZip.IsSujiCheck = False
        Me.txtDestZip.IsZenkakuCheck = False
        Me.txtDestZip.ItemName = ""
        Me.txtDestZip.LineSpace = 0
        Me.txtDestZip.Location = New System.Drawing.Point(535, 18)
        Me.txtDestZip.MaxLength = 10
        Me.txtDestZip.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Char]
        Me.txtDestZip.MaxLineCount = 0
        Me.txtDestZip.Multiline = False
        Me.txtDestZip.Name = "txtDestZip"
        Me.txtDestZip.ReadOnly = False
        Me.txtDestZip.ScrollBarMode = GrapeCity.Win.Editors.ScrollBarMode.Fixed
        Me.txtDestZip.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDestZip.Size = New System.Drawing.Size(100, 18)
        Me.txtDestZip.TabIndex = 74
        Me.txtDestZip.TabStopSetting = True
        Me.txtDestZip.TextValue = ""
        Me.txtDestZip.UseSystemPasswordChar = False
        Me.txtDestZip.WidthDef = 100
        Me.txtDestZip.WrapMode = GrapeCity.Win.Editors.WrapMode.WordWrap
        '
        'TitlelblNinushi
        '
        Me.TitlelblNinushi.AutoSize = True
        Me.TitlelblNinushi.AutoSizeDef = True
        Me.TitlelblNinushi.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNinushi.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblNinushi.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblNinushi.EnableStatus = False
        Me.TitlelblNinushi.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNinushi.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblNinushi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNinushi.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblNinushi.HeightDef = 13
        Me.TitlelblNinushi.Location = New System.Drawing.Point(45, 20)
        Me.TitlelblNinushi.Name = "TitlelblNinushi"
        Me.TitlelblNinushi.Size = New System.Drawing.Size(35, 13)
        Me.TitlelblNinushi.TabIndex = 17
        Me.TitlelblNinushi.Text = "荷主"
        Me.TitlelblNinushi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblNinushi.TextValue = "荷主"
        Me.TitlelblNinushi.WidthDef = 35
        '
        'chkSoukoMisettei
        '
        Me.chkSoukoMisettei.AutoSize = True
        Me.chkSoukoMisettei.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSoukoMisettei.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.chkSoukoMisettei.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.chkSoukoMisettei.EnableStatus = True
        Me.chkSoukoMisettei.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSoukoMisettei.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkSoukoMisettei.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSoukoMisettei.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkSoukoMisettei.HeightDef = 17
        Me.chkSoukoMisettei.Location = New System.Drawing.Point(300, 20)
        Me.chkSoukoMisettei.Name = "chkSoukoMisettei"
        Me.chkSoukoMisettei.Size = New System.Drawing.Size(124, 17)
        Me.chkSoukoMisettei.TabIndex = 3
        Me.chkSoukoMisettei.TabStopSetting = True
        Me.chkSoukoMisettei.Text = "倉庫未設定のみ"
        Me.chkSoukoMisettei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkSoukoMisettei.TextValue = "倉庫未設定のみ"
        Me.chkSoukoMisettei.UseVisualStyleBackColor = True
        Me.chkSoukoMisettei.WidthDef = 124
        '
        'TitlelblSoko
        '
        Me.TitlelblSoko.AutoSize = True
        Me.TitlelblSoko.AutoSizeDef = True
        Me.TitlelblSoko.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSoko.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.TitlelblSoko.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.TitlelblSoko.EnableStatus = False
        Me.TitlelblSoko.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSoko.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitlelblSoko.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSoko.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.TitlelblSoko.HeightDef = 13
        Me.TitlelblSoko.Location = New System.Drawing.Point(45, 19)
        Me.TitlelblSoko.Name = "TitlelblSoko"
        Me.TitlelblSoko.Size = New System.Drawing.Size(35, 13)
        Me.TitlelblSoko.TabIndex = 3
        Me.TitlelblSoko.Text = "倉庫"
        Me.TitlelblSoko.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TitlelblSoko.TextValue = "倉庫"
        Me.TitlelblSoko.WidthDef = 35
        '
        'grpSokoSet
        '
        Me.grpSokoSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSokoSet.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.grpSokoSet.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.grpSokoSet.Controls.Add(Me.cmbSoko)
        Me.grpSokoSet.Controls.Add(Me.btnIkkatsu)
        Me.grpSokoSet.Controls.Add(Me.TitlelblSoko)
        Me.grpSokoSet.EnableStatus = False
        Me.grpSokoSet.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSokoSet.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpSokoSet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSokoSet.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpSokoSet.HeightDef = 51
        Me.grpSokoSet.Location = New System.Drawing.Point(12, 64)
        Me.grpSokoSet.Name = "grpSokoSet"
        Me.grpSokoSet.Size = New System.Drawing.Size(445, 51)
        Me.grpSokoSet.TabIndex = 12
        Me.grpSokoSet.TabStop = False
        Me.grpSokoSet.Text = "設定項目"
        Me.grpSokoSet.TextValue = "設定項目"
        Me.grpSokoSet.WidthDef = 445
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
        Me.cmbSoko.Location = New System.Drawing.Point(82, 17)
        Me.cmbSoko.Name = "cmbSoko"
        Me.cmbSoko.ReadOnly = False
        Me.cmbSoko.SelectedIndex = -1
        Me.cmbSoko.SelectedItem = Nothing
        Me.cmbSoko.SelectedText = ""
        Me.cmbSoko.SelectedValue = ""
        Me.cmbSoko.Size = New System.Drawing.Size(211, 18)
        Me.cmbSoko.TabIndex = 17
        Me.cmbSoko.TabStopSetting = True
        Me.cmbSoko.TextValue = ""
        Me.cmbSoko.ValueMember = Nothing
        Me.cmbSoko.WidthDef = 211
        '
        'btnIkkatsu
        '
        Me.btnIkkatsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatsu.BackColorDef = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnIkkatsu.BorderStyleDef = System.Windows.Forms.BorderStyle.None
        Me.btnIkkatsu.EnableStatus = True
        Me.btnIkkatsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatsu.FontDef = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnIkkatsu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatsu.ForeColorDef = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnIkkatsu.HeightDef = 22
        Me.btnIkkatsu.Location = New System.Drawing.Point(300, 15)
        Me.btnIkkatsu.Name = "btnIkkatsu"
        Me.btnIkkatsu.Size = New System.Drawing.Size(62, 22)
        Me.btnIkkatsu.TabIndex = 5
        Me.btnIkkatsu.TabStopSetting = True
        Me.btnIkkatsu.Text = "設定"
        Me.btnIkkatsu.TextValue = "設定"
        Me.btnIkkatsu.UseVisualStyleBackColor = True
        Me.btnIkkatsu.WidthDef = 62
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
        Me.sprDetail.HeightDef = 745
        Me.sprDetail.Location = New System.Drawing.Point(12, 121)
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
        Me.sprDetail.Size = New System.Drawing.Size(1245, 745)
        Me.sprDetail.SortColumn = True
        Me.sprDetail.SpanColumnLock = True
        Me.sprDetail.SpreadDoubleClicked = False
        Me.sprDetail.TabIndex = 15
        Me.sprDetail.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never
        Me.sprDetail.TextValue = Nothing
        Me.sprDetail.WidthDef = 1245
        '
        'LMM350F
        '
        Me.ClientSize = New System.Drawing.Size(1274, 962)
        Me.Name = "LMM350F"
        Me.pnlViewAria.ResumeLayout(False)
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.grpSokoSet.ResumeLayout(False)
        Me.grpSokoSet.PerformLayout()
        CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TitlelblSoko As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents grpSearch As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents chkSoukoMisettei As Jp.Co.Nrs.LM.GUI.Win.LMCheckBox
    Friend WithEvents grpSokoSet As Jp.Co.Nrs.LM.GUI.Win.LMGroupBox
    Friend WithEvents sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpreadSearch
    Friend WithEvents btnIkkatsu As Jp.Co.Nrs.LM.GUI.Win.LMButton
    Friend WithEvents TitlelblNinushi As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents TitlelblZip As Jp.Co.Nrs.LM.GUI.Win.LMTitleLabel
    Friend WithEvents txtDestZip As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox
    Friend WithEvents cmbCustCd As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo
    Friend WithEvents cmbSoko As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImCombo

End Class
